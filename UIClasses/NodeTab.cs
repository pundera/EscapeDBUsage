using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EscapeDBUsage.UIClasses
{
    public class NodeTab: NodeBase
    {
        public NodeTab(IEventAggregator eventAggregator, NodeExcel nodeExcel) : base(eventAggregator)
        {
            NodeExcel = nodeExcel;
            AddTable = new DelegateCommand(DoAddTable);
        }

        public ICommand AddTable { get; private set; }

        private void DoAddTable()
        {
            if (Nodes == null) Nodes = new ObservableCollection<NodeDbTable>();
            Nodes.Insert(0, new NodeDbTable(EventAggregator, this));
        }



        private NodeExcel nodeExcel;
        public NodeExcel NodeExcel
        {
            get { return nodeExcel; }
            set { SetProperty(ref nodeExcel, value); }
        }

        private ObservableCollection<NodeDbTable> nodes;
        public ObservableCollection<NodeDbTable> Nodes
        {
            get { return nodes; }
            set { SetProperty(ref nodes, value); }
        }

    }
}
