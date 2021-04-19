using Als.Infrastructure.Commands.BaseCommands;
using System;
using System.Windows;


namespace Als.Infrastructure.Commands
{
    class OpenWindowCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            Type type = Type.GetType(parameter.ToString());
            var window = (Window)Activator.CreateInstance(type);

            window.ShowDialog();
        }
    }
}
