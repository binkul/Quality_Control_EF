﻿using System;
using System.Windows.Input;
using Quality_Control_EF.Forms.Quality.ModelView;

namespace Quality_Control_EF.Forms.Quality.Command
{
    internal class AddNewButton : ICommand
    {
        private readonly QualityMV _modelView;

        public AddNewButton(QualityMV modelView)
        {
            if (modelView == null) throw new ArgumentNullException("Model widoku jest null");
            _modelView = modelView;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _modelView.AddNew();
        }
    }
}