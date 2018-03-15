using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeDBUsage.UIClasses
{
    public class NodeRoot: NodeBase
    {
        public NodeRoot(IEventAggregator evAgg):base(evAgg)
        {

        }

        private ObservableCollection<NodeExcel> nodes;
        public ObservableCollection<NodeExcel> Nodes
        {
            get { return nodes; }
            set { SetProperty(ref nodes, value); }
        }

        public NodeRoot Copy()
        {
            var newRoot = new NodeRoot(EventAggregator)
            {
                Nodes = new ObservableCollection<NodeExcel>()
            };


            newRoot.Nodes = new ObservableCollection<NodeExcel>(Nodes.Select(n => new NodeExcel(EventAggregator, newRoot) {
                Name = n.Name,
                Description = n.Description,
                IsExpanded = true,
                Nodes = new ObservableCollection<NodeTab>(n.Nodes.ToList().Select(t => new NodeTab(EventAggregator, n) {
                    Name = t.Name,
                    Description = t.Description,
                    Nodes = new ObservableCollection<NodeDbTable>(t.Nodes.ToList().Select(table => new NodeDbTable(EventAggregator, t)
                    {
                        Name = table.Name,
                        Description = table.Description,
                        Nodes = new ObservableCollection<NodeDbColumn>(table.Nodes.ToList().Select(c => new NodeDbColumn(EventAggregator, table)
                        {
                            Name = c.Name,
                            Description = c.Description
                        }))
                    }))
                }))
            }));

            return newRoot;
        }
    }
}
