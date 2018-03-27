using EscapeDBUsage.UIClasses.DatabaseSchema;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeDBUsage.ViewModels
{
    public class DatabaseSchemaViewModel: BindableBase
    {
        public DatabaseSchemaViewModel()
        {
            Nodes = new ObservableCollection<NodeDbSchemaTable>()
            {
                new NodeDbSchemaTable()
                {
                    Name = "XXX", Description = "IHD h dw ejkh fkjrh vjrfhv fjvh fjh vfjh vjfkvh fkj hvkfh ",
                    Columns = new ObservableCollection<NodeDbSchemaColumn>()
                    {
                        new NodeDbSchemaColumn()
                        {
                            Name = "My super truper COLUMN", Description = "ljdfhcjfe vhjkefvh kfeh vkjefdh kjdfh vkefjdh"
                        },
                        new NodeDbSchemaColumn()
                        {
                            Name = "COLUMN 002", Description = "Aaaaa aaaa"
                        },
                        new NodeDbSchemaColumn()
                        {
                            Name = "COLUMN 003", Description = "XXXXXXX xxxxxx"
                        },
                    }
                },
                new NodeDbSchemaTable()
                {
                    Name = "YYY", Description = "IJD LD:JE EJKJ EKJ ELKj l EJkjdekl jIHD h dw ejkh fkjrh vjrfhv fjvh fjh vfjh vjfkvh fkj hvkfh ",
                    Columns = new ObservableCollection<NodeDbSchemaColumn>()
                    {
                        new NodeDbSchemaColumn()
                        {
                            Name = "My truper COLUMN", Description = "mlns cd jjcdljchdjcd clj ldckj dlcj dklj cldkj cljdfhcjfe vhjkefvh kfeh vkjefdh kjdfh vkefjdh"
                        },
                        new NodeDbSchemaColumn()
                        {
                            Name = "COLUMN x002", Description = "Cosi s cd jjcdljchdjcd clj ldckj dlcj dklj cldkj cljdfhcjfe vhjkefvh kfeh vkjefdh kjdfh vkefjdh"
                        },
                        new NodeDbSchemaColumn()
                        {
                            Name = "COLUMN x003", Description = "KKkkkkk .... dlcj dklj cldkj cljdfhcjfe vhjkefvh kfeh vkjefdh kjdfh vkefjdh"
                        },
                        new NodeDbSchemaColumn()
                        {
                            Name = "COLUMN x004", Description = "MMMMmmmmm ldckj dlcj dklj cldkj cljdfhcjfe vhjkefvh kfeh vkjefdh kjdfh vkefjdh"
                        },
                    }

                }
            };
        }

        private ObservableCollection<NodeDbSchemaTable> nodes;
        public ObservableCollection<NodeDbSchemaTable> Nodes
        {
            get { return nodes; }
            set { SetProperty(ref nodes, value); }
        }
    }
}
