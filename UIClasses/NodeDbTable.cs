﻿using Prism.Commands;
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
    public class NodeDbTable : NodeBase
    {
        public NodeDbTable(IEventAggregator eventAggregator, NodeTab nodeTab) : base(eventAggregator)
        {
            NodeTab = nodeTab;
            AddColumn = new DelegateCommand(DoAddColumn);
        }

        public ICommand AddColumn { get; private set; }

        private void DoAddColumn()
        {
            if (Nodes == null) Nodes = new ObservableCollection<NodeDbColumn>();
            Nodes.Insert(0, new NodeDbColumn(EventAggregator, this));
        }


        private NodeTab nodeTab;
        public NodeTab NodeTab
        {
            get { return nodeTab; }
            set { SetProperty(ref nodeTab, value); }
        }

        private ObservableCollection<NodeDbColumn> nodes;
        public ObservableCollection<NodeDbColumn> Nodes
        {
            get { return nodes; }
            set { SetProperty(ref nodes, value); }
        }

    }
}
