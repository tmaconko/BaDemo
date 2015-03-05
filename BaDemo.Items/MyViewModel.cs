using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace BaDemo.Items
{

    public class MyViewModel : BindableBase
    {
        private string _someText;

        public string SomeText
        {
            get { return _someText; }
            set { SetProperty(ref _someText, value); }
        }

        private ICommand _doSomeWorkCommand;
        public ICommand DoSomeWorkCommand
        {
            get
            {
                return _doSomeWorkCommand ?? (_doSomeWorkCommand = new DelegateCommand(() =>
                {
                    //DOING WORK
                }));
            }
        }

        private ICommand _doAnotherSomeWorkCommand;
        public ICommand DoAnotherSomeWorkCommand
        {
            get
            {
                return _doAnotherSomeWorkCommand ?? (_doAnotherSomeWorkCommand = new DelegateCommand(() =>
                {
                    //DOING ANOTHER SOME WORK
                }));
            }
        }

        private ICommand _doSomeCompositeWorkCommand;
        public ICommand DoSomeCompositeWorkCommand
        {
            get
            {
                if (_doSomeCompositeWorkCommand != null)
                    return _doSomeCompositeWorkCommand;

                var doSomeCompositeWork = new CompositeCommand();
                doSomeCompositeWork.RegisterCommand(DoSomeWorkCommand);
                doSomeCompositeWork.RegisterCommand(DoAnotherSomeWorkCommand);

                return _doSomeCompositeWorkCommand = doSomeCompositeWork;
            }
        }

    }

}
