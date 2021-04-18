using Als.Infrastructure.Commands.BaseCommand;
using System.Windows;


namespace Als.Infrastructure.Commands
{
    class ExitFromApplicationCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}
