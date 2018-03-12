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
    public class NodeExcel: NodeBase
    {
        public NodeExcel(IEventAggregator eventAggregator, NodeRoot root): base(eventAggregator)
        {
            AddTab = new DelegateCommand(DoAddTab);
            Root = root;
        }

        public ICommand AddTab { get; private set; }
        public NodeRoot Root { get; private set; }

        private void DoAddTab()
        {
            if (Nodes == null) Nodes = new ObservableCollection<NodeTab>();
            Nodes.Insert(0, new NodeTab(EventAggregator, this));
        }

        private ObservableCollection<NodeTab> nodes;
        public ObservableCollection<NodeTab> Nodes
        {
            get { return nodes; }
            set { SetProperty(ref nodes, value); }
        }

    }
}
