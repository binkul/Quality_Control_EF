using Quality_Control_EF.Forms.Modification.Command;
using Quality_Control_EF.Forms.Modification.Model;
using Quality_Control_EF.Models;
using Quality_Control_EF.Service;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Quality_Control_EF.Forms.Modification.ModelView
{
    public class ModificationMV : INotifyPropertyChanged
    {
        private ICommand _deleteButton;
        private ICommand _setButton;
        private ICommand _stdButton;
        private ICommand _stdpHButton;

        public QualityControl Quality { get; set; }
        private readonly ModificationService _service = new ModificationService();
        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged(params string[] names)
        {
            if (PropertyChanged != null)
            {
                foreach (string name in names)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public List<ModificationModel> Fields => _service.Fields;

        internal bool Modified => _service.Modified;

        internal bool IsAnySet => _service.IsAnySet;

        internal bool IsAnyUnset => _service.IsAnyUnSet;

        public ICommand DeleteButton
        {
            get
            {
                if (_deleteButton == null) _deleteButton = new DeleteButton(this);
                return _deleteButton;
            }
        }

        public ICommand SetButton
        {
            get
            {
                if (_setButton == null) _setButton = new SetButton(this);
                return _setButton;
            }
        }

        public ICommand StdButton
        {
            get
            {
                if (_stdButton == null) _stdButton = new StandardButton(this);
                return _stdButton;
            }
        }

        public ICommand StdWithpHButton
        {
            get
            {
                if (_stdpHButton == null) _stdpHButton = new StandardpHButton(this);
                return _stdpHButton;
            }
        }

        internal void InitiateFields()
        {
            _service.CheckFieldsInList(Quality.ActiveFields);
            _service.CancelModify();
        }

        internal string SettingFields => _service.RecalculateFields();

        internal void Delete()
        {
            _service.UnsetFields();
        }

        internal void Set()
        {
            _service.SetFields();
        }

        internal void Standard()
        {
            _service.UnsetFields();
            _service.SetStandard();
        }

        internal void StandardpH()
        {
            _service.UnsetFields();
            _service.SetStandardWithpH();
        }
    }
}
