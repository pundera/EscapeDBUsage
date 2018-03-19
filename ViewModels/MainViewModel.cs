using EscapeDBUsage.Events;
using EscapeDBUsage.Helpers;
using EscapeDBUsage.UIClasses;
using EscapeDBUsage.UIClasses.OtherViews;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EscapeDBUsage.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel(IEventAggregator evAgg)
        {
            eventAgg = evAgg;
            rootTables = new NodeDbTableRoot(evAgg)
            {
                Name = "ROOT for DB Tables",
                Description = "just helper instance...",
                Nodes = new ObservableCollection<NodeDbTableToExcel>()
            };

            evAgg.GetEvent<EventSelectedChanged>().Subscribe((n) => {


                SelectedExcel = null;
                SelectedTab = null;
                SelectedDbTable = null;
                SelectedDbColumn = null;

                ExcelVisible = false;
                TabVisible = false;
                TableVisible = false;
                ColumnVisible = false;

                if (n is NodeExcel)
                {
                    SelectedExcel = n as NodeExcel;
                    ExcelVisible = true;
                }
                if (n is NodeTab)
                {
                    SelectedTab = n as NodeTab;
                    SelectedExcel = (n as NodeTab).NodeExcel;
                    ExcelVisible = true;
                    TabVisible = true;
                }
                if (n is NodeDbTable)
                {
                    SelectedDbTable = n as NodeDbTable;
                    SelectedTab = (n as NodeDbTable).NodeTab;
                    SelectedExcel = (n as NodeDbTable).NodeTab.NodeExcel;
                    ExcelVisible = true;
                    TabVisible = true;
                    TableVisible = true;
                }
                if (n is NodeDbColumn)
                {
                    SelectedDbColumn = n as NodeDbColumn;
                    SelectedDbTable = (n  as NodeDbColumn).NodeDbTable;
                    SelectedTab = (n as NodeDbColumn).NodeDbTable.NodeTab;
                    SelectedExcel = (n as NodeDbColumn).NodeDbTable.NodeTab.NodeExcel;
                    ExcelVisible = true;
                    TabVisible = true;
                    TableVisible = true;
                    ColumnVisible = true;
                }
            });
            Import = new DelegateCommand(() => DoImport());
            Save = new DelegateCommand(() => DoSave());
            Load = new DelegateCommand(() => DoLoad());
            Refresh = new DelegateCommand(() => DoRefresh());
            RefreshColumns = new DelegateCommand(() => DoRefreshColumns());

            ExpandAll = new DelegateCommand(() => DoExpandAll());
            CollapseAll = new DelegateCommand(() => DoCollapseAll());

            SaveSprints = new DelegateCommand(() => DoSaveSprints());

            AddExcel = new DelegateCommand(() => DoAddExcel());

            Task.Run(() =>
            {
                DoLoad();
            });
        }

        private void DoAddExcel()
        {
            var newExcel = new NodeExcel(eventAgg, root); 
            NodesExcel.Add(newExcel);
        }

        private void DoSaveSprints()
        {
            var b = SaveHelper.SaveSprints(Sprints);
        }

        private UISprint selectedSprint;
        public UISprint SelectedSprint
        {
            get { return selectedSprint; }
            set
            {
                SetProperty(ref selectedSprint, value);
                if (selectedSprint != null)
                {
                    root = selectedSprint.Root;
                    NodesExcel = selectedSprint.Root.Nodes;
                    if (NodesExcel!=null && NodesExcel.Count>0) SelectedExcel = NodesExcel.First();
                }
            }
        }

        private NodeDbTableRoot rootTables;
        private NodeRoot root;

        private void DoRefresh()
        {
            var list = NodesTable = new ObservableCollection<NodeDbTableToExcel>();
            RefreshHelper.RefreshTables(eventAgg, root, ref list);
            NodesTable = list;
        }

        private void DoRefreshColumns()
        {
            var list = NodesTableColumns = new ObservableCollection<NodeDbTableColumnsToExcel>();
            RefreshHelper.RefreshTableColumns(eventAgg, root, ref list);
            NodesTableColumns = list;
        }

        private void DoSave()
        {
            var b = SaveHelper.SaveSprints(Sprints);
        }

        private void DoLoad()
        {
            //var list = nodesExcel = new ObservableCollection<NodeExcel>();
            var listOfSprints = new ObservableCollection<UISprint>();

            //root = new NodeRoot(eventAgg) { Name = "ROOT", Description = "just help instance (node)..." };
            LoadHelper.Load(ref listOfSprints, eventAgg);
            //NodesExcel = list;
            //root.Nodes = NodesExcel;
            Sprints = listOfSprints;
            SelectedSprint = listOfSprints.Last();
        }

        private bool excelVisible;
        public bool ExcelVisible
        {
            get { return excelVisible; }
            set { SetProperty(ref excelVisible, value); }
        }

        private bool tabVisible;
        public bool TabVisible
        {
            get { return tabVisible; }
            set { SetProperty(ref tabVisible, value); }
        }

        private bool tableVisible;
        public bool TableVisible
        {
            get { return tableVisible; }
            set { SetProperty(ref tableVisible, value); }
        }

        private bool columnVisible;
        public bool ColumnVisible
        {
            get { return columnVisible; }
            set { SetProperty(ref columnVisible, value); }
        }

        private IEventAggregator eventAgg;

        private NodeExcel selecedExcel;
        public NodeExcel SelectedExcel
        {
            get { return selecedExcel; }
            set { SetProperty(ref selecedExcel, value); }
        }

        private NodeTab selecedTab;
        public NodeTab SelectedTab
        {
            get { return selecedTab; }
            set { SetProperty(ref selecedTab, value); }
        }

        private NodeDbTable selecedDbTable;
        public NodeDbTable SelectedDbTable
        {
            get { return selecedDbTable; }
            set { SetProperty(ref selecedDbTable, value); }
        }

        private NodeDbColumn selecedDbColumn;
        public NodeDbColumn SelectedDbColumn
        {
            get { return selecedDbColumn; }
            set { SetProperty(ref selecedDbColumn, value); }
        }

        private void DoCollapseAll()
        {
            if (NodesExcel == null) return;
            foreach (var e in NodesExcel)
            {
                e.IsExpanded = false;
                if (e.Nodes != null)
                    foreach (var tab in e.Nodes)
                    {
                        tab.IsExpanded = false;
                        if (tab.Nodes != null)
                            foreach (var table in tab.Nodes)
                            {
                                table.IsExpanded = false;
                                if (table.Nodes != null)
                                    foreach (var c in table.Nodes)
                                    {
                                        c.IsExpanded = false;
                                    }
                            }
                    }
            }
        }

        private void DoImport()
        {
            var exePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var dataPath = string.Format("{0}\\Data", exePath);

            var data = string.Format("{0}\\data.json", dataPath);
            var guide = string.Format("{0}\\guide.txt", dataPath);

            var fi = new FileInfo(guide);

            if (fi.Exists)
            {

                using (var stream = fi.OpenText())
                {
                    var text = stream.ReadToEnd();
                    stream.Close();

                    var lines = text.Split('\n');

                    var nodeRoot = new NodeRoot(eventAgg)
                    {
                        Name = "ROOT",
                        Description = "just help instance..."
                    };
                    NodesExcel = new ObservableCollection<NodeExcel>();
                    var masterDataNode = new NodeExcel(eventAgg,  nodeRoot) { Name = "MasterData" };
                    nodeRoot.Nodes = new ObservableCollection<NodeExcel>();
                    nodeRoot.Nodes.Add(masterDataNode);
                    masterDataNode.Nodes = new ObservableCollection<NodeTab>();
                    NodesExcel.Add(masterDataNode);

                    NodeTab tab = null;

                    NodeDbTable dbTable = null;

                    foreach (var l in lines)
                    {
                        if (string.IsNullOrEmpty(l) || string.IsNullOrWhiteSpace(l))
                        {
                            continue;
                        }

                      
                        if (l[0] == '#')
                        {
                            // starting... -> 
                            tab = new NodeTab(eventAgg, masterDataNode) { Name = l.Substring(1, l.IndexOf('-') - 2), Description = l.Substring(l.IndexOf('-') + 2) };
                            dbTable = new NodeDbTable(eventAgg, tab) { Name = l.Substring(1, l.IndexOf('-') - 2).Replace(" ", "") };
                            tab.Nodes = new ObservableCollection<NodeDbTable>
                            {
                                dbTable
                            };
                            NodesExcel[0].Nodes.Add(tab);
                        }
                        else
                        {
                            var column = new NodeDbColumn(eventAgg, dbTable) { Name = (l.Substring(0, l.IndexOf('-') - 1)), DBColumnName = (l.Substring(0, l.IndexOf('-') - 1)).Replace(" ", "_").ToUpperInvariant(), Description = l.Substring(l.IndexOf('-') + 2) };
                            if (tab.Nodes[0].Nodes == null)
                            {
                                tab.Nodes[0].Nodes = new ObservableCollection<NodeDbColumn>();
                            }

                            tab.Nodes[0].Nodes.Add(column);
                        }
                    }

                }
            }

        }

        public ICommand Import { get; private set; }
        public ICommand Save { get; private set; }
        public ICommand Load { get; private set; }
        public ICommand Refresh { get; private set; }
        public ICommand RefreshColumns { get; private set; }
        public ICommand ExpandAll { get; private set; }
        public ICommand CollapseAll { get; private set; }

        public ICommand SaveSprints { get; private set; }

        //AddExcel
        public ICommand AddExcel { get; private set; }

        private void DoExpandAll()
        {
            if (NodesExcel == null) return;
            foreach (var e in NodesExcel)
            {
                e.IsExpanded = true;
                if (e.Nodes != null)
                    foreach (var tab in e.Nodes)
                    {
                        tab.IsExpanded = true;
                        if (tab.Nodes != null)
                            foreach (var table in tab.Nodes)
                            {
                                table.IsExpanded = true;
                                if (table.Nodes != null)
                                    foreach (var c in table.Nodes)
                                    {
                                        c.IsExpanded = true;
                                    }
                            }
                    }
            }
        }


        private ObservableCollection<NodeExcel> nodesExcel;
        public ObservableCollection<NodeExcel> NodesExcel
        {
            get { return nodesExcel; }
            set { SetProperty(ref nodesExcel, value); }
        }

        private string fullTextColumnName;
        public string FullTextColumnName
        {
            get { return fullTextColumnName; }
            set
            {
                SetProperty(ref fullTextColumnName, value);
                DoFilter(FilterType.Name);
            }
        }

        private void DoFilter(FilterType name)
        {
            switch (name)
            {
                case FilterType.Name:
                    if (string.IsNullOrEmpty(fullTextColumnName))
                    {
                        CommonErasingFilter();
                    }
                    else
                    {
                        CommonFiltering(FilterType.Name);
                    }
                    break;
                case FilterType.Description:
                    if (string.IsNullOrEmpty(fullTextColumnDescription))
                    {
                        CommonErasingFilter();
                    }
                    else
                    {
                        CommonFiltering(FilterType.Description);
                    }
                    break;
            }
        }

        private void CommonErasingFilter()
        {
            foreach (var e in NodesExcel)
            {
                if (e.Nodes != null) foreach (var tab in e.Nodes)
                    {
                        if (tab.Nodes != null) foreach (var table in tab.Nodes)
                            {
                                if (table.Nodes != null) foreach (var c in table.Nodes)
                                    {
                                        c.IsVisible = true;
                                    }
                            }
                    }
            }
        }

        private void CommonFiltering(FilterType type)
        {
            foreach (var e in NodesExcel)
            {
                var bExcel = false;
                if (e.Nodes != null) foreach (var tab in e.Nodes)
                    {
                        var bTab = false;
                        if (tab.Nodes != null) foreach (var table in tab.Nodes)
                            {
                                var bTable = false;
                                if (table.Nodes != null) foreach (var c in table.Nodes)
                                    {
                                        if ((type == FilterType.Name && c.Name.ToUpperInvariant().IndexOf(fullTextColumnName.ToUpperInvariant()) > -1)
                                            ||
                                            (type == FilterType.Description && c.Description.ToUpperInvariant().IndexOf(fullTextColumnDescription.ToUpperInvariant()) > -1))
                                        {
                                            c.IsVisible = true;
                                            bExcel = true;
                                            bTab = true;
                                            bTable = true;
                                        }
                                        else
                                        {
                                            c.IsVisible = false;
                                        }
                                    }
                                e.IsVisible = bExcel;
                                tab.IsVisible = bTab;
                                table.IsVisible = bTable;
                        }

                }
            }
        }

        private bool areDescsShown = true;
        public bool AreDescsShown
        {
            get
            {
                return areDescsShown;
            }
            set
            {
                SetProperty(ref areDescsShown, value);

                foreach (var n in NodesExcel)
                {
                    n.AreDescsShown = value;
                    foreach (var tab in n.Nodes)
                    {
                        tab.AreDescsShown = value;
                        foreach (var table in tab.Nodes)
                        {
                            table.AreDescsShown = value;
                            foreach (var c in table.Nodes)
                            {
                                c.AreDescsShown = value;
                            }
                        }
                    } 
                }
            }
        }

        private string fullTextColumnDescription;
        public string FullTextColumnDescription
        {
            get { return fullTextColumnDescription; }
            set
            {
                SetProperty(ref fullTextColumnDescription, value);
                DoFilter(FilterType.Description);
            }
        }

        private ObservableCollection<NodeDbTableToExcel> nodesTable;
        public ObservableCollection<NodeDbTableToExcel> NodesTable { get { return nodesTable; } set { SetProperty(ref nodesTable, value); } }

        private ObservableCollection<NodeDbTableColumnsToExcel> nodesTableColumns;
        public ObservableCollection<NodeDbTableColumnsToExcel> NodesTableColumns { get { return nodesTableColumns; } set { SetProperty(ref nodesTableColumns, value); } }

        private ObservableCollection<UISprint> sprints;
        public ObservableCollection<UISprint> Sprints { get { return sprints; } set { SetProperty(ref sprints, value); } }

    }

    public enum FilterType
    {
        Name,
        Description
    }
}
