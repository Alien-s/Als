using Als.Infrastructure.Commands.BaseCommands;
using System.Windows;


namespace Als.Infrastructure.Commands
{
    /// <summary>Command for Exit from Application</summary>
    class ExitFromApplicationCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}
