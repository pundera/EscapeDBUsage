using EscapeDBUsage.ModelClasses;
using EscapeDBUsage.UIClasses;
using Newtonsoft.Json;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeDBUsage.Helpers
{
    public static class LoadHelper
    {
        public static void Load(ref ObservableCollection<NodeExcel> result, IEventAggregator evAgg)
        {
            var exePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var dataPath = $"{exePath}\\Data";

            var data = $"{dataPath}\\data.json";
            var fi = new FileInfo(data);

            using (var stream = fi.OpenText())
            {
                var json = stream.ReadToEnd();
                var rr = JsonConvert.DeserializeObject<List<Excel>>(json);

                var nodeRoot = new NodeRoot(evAgg);

                result = new ObservableCollection<NodeExcel>();
                foreach (var r in rr)
                {
                    var ne = new NodeExcel(evAgg, nodeRoot) { Name = r.Name, Description = r.Description };
                    result.Add(ne);
                    nodeRoot.Nodes = new ObservableCollection<NodeExcel>() { ne };
                    ne.Nodes = new ObservableCollection<NodeTab>();
                    if (r.Nodes == null) continue;
                    foreach (var t in r.Nodes)
                    {
                        var nt = new NodeTab(evAgg, ne) { Name = t.Name, Description = t.Description };
                        ne.Nodes.Add(nt);
                        nt.Nodes = new ObservableCollection<NodeDbTable>();
                        if (t.Nodes == null) continue;
                        foreach (var table in t.Nodes)
                        {
                            var ntable = new NodeDbTable(evAgg, nt) { Name = table.Name, Description = table.Description };
                            nt.Nodes.Add(ntable);
                            ntable.Nodes = new ObservableCollection<NodeDbColumn>();
                            if (table.Nodes == null) continue;
                            foreach (var c in table.Nodes)
                            {
                                var nc = new NodeDbColumn(evAgg, ntable) { DBColumnName = c.DbColumnName, Name = c.Name, Description = c.Description };
                                ntable.Nodes.Add(nc);
                            }
                        }

                    }
                }

            }
        }
    }
}
