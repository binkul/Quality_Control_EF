using System;
using System.ComponentModel;

namespace Quality_Control_EF.Forms.Modification.Model
{
    public class ModificationModel : INotifyPropertyChanged, IComparable<ModificationModel>
    {
        private bool _visible;
        public event PropertyChangedEventHandler PropertyChanged;
        public int Number { get; set; }
        public string Name { get; set; }
        public string DbName { get; set; }
        public bool Modify { get; set; }

        public ModificationModel(int number, string name, string dbName)
        {
            Number = number;
            Name = name;
            DbName = dbName;
            Modify = false;
        }

        protected void OnPropertyChanged(params string[] names)
        {
            if (PropertyChanged != null)
            {
                foreach (string name in names)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public bool Visible
        {
            get => _visible;
            set
            {
                _visible = value;
                Modify = true;
                OnPropertyChanged(nameof(Visible));
            }
        }

        public int CompareTo(ModificationModel other)
        {
            return Number.CompareTo(other.Number);
        }
    }
}
