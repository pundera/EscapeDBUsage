using EscapeDBUsage.ModelClasses;
using EscapeDBUsage.UIClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeDBUsage.Helpers
{
    public static class SaveHelper
    {
        public static bool Save(IList<NodeExcel> rootList)
        {

            var list = new List<Excel>();

            foreach (var e in rootList)
            {
                var ex = new Excel() { Name = e.Name, Description = e.Description };
                ex.Nodes = new List<Tab>();
                list.Add(ex);

                if (e.Nodes == null) continue;
                foreach (var t in e.Nodes)
                {
                    var tab = new Tab() { Name = t.Name, Description = t.Description };
                    tab.Nodes = new List<DbTable>();
                    ex.Nodes.Add(tab);

                    if (t.Nodes == null) continue;
                    foreach (var table in t.Nodes)
                    {
                        var dbTable = new DbTable() { Name = table.Name, Description = table.Description };
                        dbTable.Nodes = new List<DbColumn>();
                        tab.Nodes.Add(dbTable);

                        if (table.Nodes == null) continue;
                        foreach (var column in table.Nodes)
                        {
                            var dbColumn = new DbColumn() { DbColumnName = column.DBColumnName, Name = column.Name, Description = column.Description };
                            dbTable.Nodes.Add(dbColumn);
                        }

                    }
                }
            }

            var exePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var dataPath = $"{exePath}\\Data";

            var data = $"{dataPath}\\data.json";

            var json = JsonConvert.SerializeObject(list, Formatting.Indented);

            var fi = new FileInfo(data);

            using (var stream = fi.CreateText())
            {
                stream.Write(json);
            }

            return true;
        }
    }
}
