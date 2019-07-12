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

        private ClickClearButtonCommand _clickClearButtonCommand;
        public ClickClearButtonCommand ClickClearButtonCommand
        {
            get
            {
                if (_clickClearButtonCommand == null)
                    _clickClearButtonCommand = new ClickClearButtonCommand(this);
                return _clickClearButtonCommand;
            }
            set { _clickClearButtonCommand = value; }
        }

        private ClickCopyButtonCommand _clickCopyButtonCommand;
        public ClickCopyButtonCommand ClickCopyButtonCommand
        {
            get
            {
                if (_clickCopyButtonCommand == null)
                    _clickCopyButtonCommand = new ClickCopyButtonCommand(this);
                return _clickCopyButtonCommand;
            }
            set { _clickCopyButtonCommand = value; }
        }

        private ClickRemoveOnClipboardButtonCommand _clickRemoveOnClipboardButtonCommand;
        public ClickRemoveOnClipboardButtonCommand ClickRemoveOnClipboardButtonCommand
        {
            get
            {
                if (_clickRemoveOnClipboardButtonCommand == null)
                    _clickRemoveOnClipboardButtonCommand = new ClickRemoveOnClipboardButtonCommand(this);
                return _clickRemoveOnClipboardButtonCommand;
            }
            set { _clickRemoveOnClipboardButtonCommand = value; }
        }


        // functions

        public void AddNotification(string notice_message)
        {
            Notification = notice_message + "\n" + Notification;
            RaisePropertyChanged("Notification");
        }

        public void PasteClipboardToTextBox()
        {
            Sentence target = Sentence.CreateSentenceFromClipboard();
            TextBoxContent = target.ToString();
            RaisePropertyChanged("TextBoxContent");
            AddNotification("Paste from clipboard. (" + target.GetTextHead() + ")");
        }

        public void RemoveNewlineInTextBox()
        {
            Sentence target = new Sentence(TextBoxContent);
            //改行文字の除去
            Sentence removed = target.RemoveNewline();
            TextBoxContent = removed.ToString();
            RaisePropertyChanged("TextBoxContent");
            AddNotification("Remove newline. (" + removed.GetTextHead()+")");
        }

        public void CopyTextBoxToClipboard()
        {
            Sentence target = new Sentence(TextBoxContent);
            target.CopyToClipboard();
            AddNotification("Copy to clipboard. (" + target.GetTextHead() + ")");
        }

        public void ClearTextBox()
        {
            TextBoxContent = "";
            RaisePropertyChanged("TextBoxContent");
            AddNotification("Clear.");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    //Command class for binding

    class ClickClearButtonCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;
        protected void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        MainWindowViewModel vm;

        public ClickClearButtonCommand(MainWindowViewModel vm)
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
            vm.ClearTextBox();
        }
    }

    class ClickRemoveButtonCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;
        protected void RaiseCanExecuteChanged()
        {
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
            vm.RemoveNewlineInTextBox();
        }
    }

    class ClickCopyButtonCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;
        protected void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        MainWindowViewModel vm;

        public ClickCopyButtonCommand(MainWindowViewModel vm)
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
            vm.CopyTextBoxToClipboard();
        }
    }

    class ClickRemoveOnClipboardButtonCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;
        protected void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        MainWindowViewModel vm;

        public ClickRemoveOnClipboardButtonCommand(MainWindowViewModel vm)
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
            vm.PasteClipboardToTextBox();
            vm.RemoveNewlineInTextBox();
            vm.CopyTextBoxToClipboard();
        }
    }
}
