using NewlineRemover.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NewlineRemover.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public string TextBoxContent { get; set; } = "";
        public string Notification { get; set; } = "";

        private ClickRemoveButtonCommand _clickRemoveButtonCommand;
        public ClickRemoveButtonCommand ClickRemoveButtonCommand
        {
            get
            {
                if (_clickRemoveButtonCommand == null)
                    _clickRemoveButtonCommand = new ClickRemoveButtonCommand(this);
                return _clickRemoveButtonCommand;
            }
            set { _clickRemoveButtonCommand = value; }
        }


        public void AddNotification(string notice_message)
        {
            Notification = Notification + notice_message + "\n";
            RaisePropertyChanged("Notification");
        }

        public void RemoveNewlineAndCopy()
        {
            Sentence target = new Sentence(TextBoxContent);
            //改行文字の除去
            Sentence removed = target.RemoveNewline();
            TextBoxContent = removed.ToString();
            RaisePropertyChanged("TextBoxContent");
            //コピー
            removed.CopyToClipboard();
            AddNotification("Remove and Copy.");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }



    class ClickRemoveButtonCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;
        protected void RaiseCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        MainWindowViewModel vm;

        public ClickRemoveButtonCommand(MainWindowViewModel vm)
        {
            this.vm = vm;
            RaiseCanExecuteChanged();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            vm.RemoveNewlineAndCopy();
        }
    }

}
