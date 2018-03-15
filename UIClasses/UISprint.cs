using Prism.Commands;
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
    public class UISprint: BindableBase
    {
        public UISprint(ObservableCollection<UISprint> sprints)
        {
            Sprints = sprints;
            InsertSprint = new DelegateCommand(() => DoInsertSprint());
            RemoveSprint = new DelegateCommand(() => DoRemoveSprint());
            CopySprint = new DelegateCommand(() => DoCopySprint());

        }

        public ICommand InsertSprint { get; private set; }
        public ICommand RemoveSprint { get; private set; }
        public ICommand CopySprint { get; private set; }

        public ObservableCollection<UISprint> Sprints { get; private set; }

        public NodeRoot Root { get; set; }

        private void DoCopySprint()
        {
            if (Sprints.Count > Sprints.IndexOf(this) + 1) Sprints[Sprints.IndexOf(this) + 1].Root = this.Root.Copy();
        }

        private void DoRemoveSprint()
        {
            Sprints.Remove(this);
        }

        private void DoInsertSprint()
        {
            Sprints.Insert(Sprints.IndexOf(this)+1, new UISprint(Sprints) { Name = "NAME", Number = 99, Version = "9.9.9.9" });
        }

        private Guid guid;
        public Guid Guid
        {
            get { return guid; }
            set { SetProperty(ref guid, value); }
        }

        private int number;
        public int Number
        {
            get { return number; }
            set { SetProperty(ref number, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string version;
        public string Version
        {
            get { return version; }
            set { SetProperty(ref version, value); }
        }

        public override string ToString()
        {
            return Number + ".) " + Name ?? "<no name>"+ " -> ver.:" + Version ?? "<no version>";
        }

    }
}
