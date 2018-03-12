using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeDBUsage.UIClasses
{
    public class NodeDbColumn: NodeBase
    {
        public NodeDbColumn(IEventAggregator eventAggregator, NodeDbTable dbTable) : base(eventAggregator)
        {
            NodeDbTable = dbTable;
        }

        private NodeDbTable nodeDbTable;
        public NodeDbTable NodeDbTable
        {
            get { return nodeDbTable; }
            set { SetProperty(ref nodeDbTable, value); }
        }

        private string dbColumnName;
        public string DBColumnName
        {
            get { return dbColumnName; }
            set { SetProperty(ref dbColumnName, value); }
        }
    }
}
