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
    }
}
