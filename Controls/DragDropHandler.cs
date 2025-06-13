using System;
using System.Drawing;
using System.Windows.Forms;
using DistintaTecnica.Models;

namespace DistintaTecnica.Controls
{
    /// <summary>
    /// Gestisce le operazioni di Drag & Drop per il TreeView della distinta tecnica
    /// </summary>
    public class DragDropHandler
    {
        private TreeView treeView;
        private TreeNode draggedNode;
        private TreeNode targetNode;
        private System.Windows.Forms.Timer scrollTimer;
        private bool isDragging = false;

        public event EventHandler<DragDropEventArgs> ElementMoved;

        public DragDropHandler(TreeView treeView)
        {
            this.treeView = treeView;
            InitializeDragDrop();
        }

        private void InitializeDragDrop()
        {
            treeView.AllowDrop = true;
            treeView.ItemDrag += TreeView_ItemDrag;
            treeView.DragEnter += TreeView_DragEnter;
            treeView.DragOver += TreeView_DragOver;
            treeView.DragDrop += TreeView_DragDrop;
            treeView.DragLeave += TreeView_DragLeave;

            // Timer per lo scroll automatico durante il drag
            scrollTimer = new System.Windows.Forms.Timer();
            scrollTimer.Interval = 200;
            scrollTimer.Tick += ScrollTimer_Tick;
        }

        private void TreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            try
            {
                draggedNode = (TreeNode)e.Item;

                // Verifica se il nodo può essere trascinato
                if (!CanDragNode(draggedNode))
                {
                    return;
                }

                isDragging = true;
                treeView.DoDragDrop(draggedNode, DragDropEffects.Move);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'avvio del drag: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isDragging = false;
                draggedNode = null;
                targetNode = null;
                scrollTimer.Stop();
            }
        }

        private void TreeView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void TreeView_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                if (!e.Data.GetDataPresent(typeof(TreeNode)))
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }

                // Converti coordinate schermo in coordinate TreeView
                Point targetPoint = treeView.PointToClient(new Point(e.X, e.Y));
                targetNode = treeView.GetNodeAt(targetPoint);

                // Gestisci scroll automatico
                HandleAutoScroll(targetPoint);

                // Verifica se il drop è possibile
                if (targetNode != null && CanDropOnNode(draggedNode, targetNode))
                {
                    e.Effect = DragDropEffects.Move;

                    // Evidenzia il nodo target
                    if (treeView.SelectedNode != targetNode)
                    {
                        treeView.SelectedNode = targetNode;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                    targetNode = null;
                }
            }
            catch (Exception ex)
            {
                e.Effect = DragDropEffects.None;
                System.Diagnostics.Debug.WriteLine($"Errore DragOver: {ex.Message}");
            }
        }

        private void TreeView_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (!e.Data.GetDataPresent(typeof(TreeNode)) || targetNode == null)
                {
                    return;
                }

                TreeNode droppedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

                if (droppedNode == null || !CanDropOnNode(droppedNode, targetNode))
                {
                    return;
                }

                // Conferma spostamento
                if (!ConfirmMove(droppedNode, targetNode))
                {
                    return;
                }

                // Esegui lo spostamento
                PerformMove(droppedNode, targetNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il drop: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                scrollTimer.Stop();
            }
        }

        private void TreeView_DragLeave(object sender, EventArgs e)
        {
            scrollTimer.Stop();
        }

        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            // Implementa scroll automatico durante il drag
            // (implementazione di base)
        }

        private void HandleAutoScroll(Point point)
        {
            const int scrollMargin = 30;

            if (point.Y < scrollMargin)
            {
                // Scroll verso l'alto
                if (!scrollTimer.Enabled)
                {
                    scrollTimer.Start();
                }
            }
            else if (point.Y > treeView.Height - scrollMargin)
            {
                // Scroll verso il basso
                if (!scrollTimer.Enabled)
                {
                    scrollTimer.Start();
                }
            }
            else
            {
                scrollTimer.Stop();
            }
        }

        /// <summary>
        /// Verifica se un nodo può essere trascinato
        /// </summary>
        private bool CanDragNode(TreeNode node)
        {
            if (node?.Tag is not TreeNodeData nodeData)
                return false;

            // I progetti non possono essere spostati
            if (nodeData.Tipo.ToUpper() == "PROGETTO")
                return false;

            return true;
        }

        /// <summary>
        /// Verifica se un nodo può essere rilasciato su un target
        /// </summary>
        private bool CanDropOnNode(TreeNode dragNode, TreeNode dropNode)
        {
            if (dragNode == null || dropNode == null)
                return false;

            // Non può essere rilasciato su se stesso
            if (dragNode == dropNode)
                return false;

            // Non può essere rilasciato sui suoi discendenti
            if (IsDescendant(dragNode, dropNode))
                return false;

            if (dragNode.Tag is not TreeNodeData dragData ||
                dropNode.Tag is not TreeNodeData dropData)
                return false;

            // Verifica compatibilità dei tipi
            return IsValidHierarchy(dragData.Tipo, dropData.Tipo);
        }

        /// <summary>
        /// Verifica se la gerarchia è valida
        /// </summary>
        private bool IsValidHierarchy(string childType, string parentType)
        {
            return (childType.ToUpper(), parentType.ToUpper()) switch
            {
                ("PARTE_MACCHINA", "PROGETTO") => true,
                ("SEZIONE", "PARTE_MACCHINA") => true,
                ("SOTTOSEZIONE", "SEZIONE") => true,
                ("MONTAGGIO", "SOTTOSEZIONE") => true,
                ("GRUPPO", "MONTAGGIO") => true,
                _ => false
            };
        }

        /// <summary>
        /// Verifica se un nodo è discendente di un altro
        /// </summary>
        private bool IsDescendant(TreeNode ancestor, TreeNode potential)
        {
            TreeNode parent = potential.Parent;
            while (parent != null)
            {
                if (parent == ancestor)
                    return true;
                parent = parent.Parent;
            }
            return false;
        }

        /// <summary>
        /// Conferma lo spostamento con l'utente
        /// </summary>
        private bool ConfirmMove(TreeNode dragNode, TreeNode dropNode)
        {
            if (dragNode.Tag is not TreeNodeData dragData ||
                dropNode.Tag is not TreeNodeData dropData)
                return false;

            string dragText = GetNodeDisplayText(dragData);
            string dropText = GetNodeDisplayText(dropData);

            var result = MessageBox.Show(
                $"Vuoi spostare:\n'{dragText}'\n\nSotto:\n'{dropText}'?\n\n" +
                "Questa operazione modificherà la struttura della distinta.",
                "Conferma Spostamento",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            return result == DialogResult.Yes;
        }

        /// <summary>
        /// Esegue lo spostamento effettivo
        /// </summary>
        private void PerformMove(TreeNode dragNode, TreeNode dropNode)
        {
            try
            {
                if (dragNode.Tag is not TreeNodeData dragData ||
                    dropNode.Tag is not TreeNodeData dropData)
                    return;

                // Rimuovi il nodo dalla posizione corrente
                TreeNode clonedNode = (TreeNode)dragNode.Clone();
                dragNode.Remove();

                // Aggiungi alla nuova posizione
                dropNode.Nodes.Add(clonedNode);
                dropNode.Expand();

                // Seleziona il nodo spostato
                treeView.SelectedNode = clonedNode;

                // Notifica lo spostamento
                ElementMoved?.Invoke(this, new DragDropEventArgs
                {
                    MovedElement = dragData,
                    NewParent = dropData,
                    MovedNode = clonedNode
                });

                MessageBox.Show("Elemento spostato con successo!\n\n" +
                              "NOTA: Per rendere permanente questa modifica, " +
                              "sarà necessario implementare l'aggiornamento del database.",
                              "Spostamento Completato",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante lo spostamento: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetNodeDisplayText(TreeNodeData nodeData)
        {
            return nodeData.Data switch
            {
                Progetto p => $"Progetto {p.NumeroCommessa}",
                ParteMacchina pm => $"Parte {pm.CodiceParteMacchina}",
                Sezione s => $"Sezione {s.CodiceSezione}",
                Sottosezione ss => $"Sottosezione {ss.CodiceSottosezione}",
                Montaggio m => $"Montaggio {m.CodiceMontaggio}",
                Gruppo g => $"Gruppo {g.CodiceGruppo}",
                _ => "Elemento sconosciuto"
            };
        }

        public void Dispose()
        {
            scrollTimer?.Dispose();
        }
    }

    /// <summary>
    /// Argomenti per l'evento di spostamento elemento
    /// </summary>
    public class DragDropEventArgs : EventArgs
    {
        public TreeNodeData MovedElement { get; set; }
        public TreeNodeData NewParent { get; set; }
        public TreeNode MovedNode { get; set; }
    }
}