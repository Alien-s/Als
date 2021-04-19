using Als.Infrastructure.Commands.BaseCommands;
using System;


namespace Als.Infrastructure.Commands.LambdaCommands
{
    internal class LambdaCommandAsync : Command
    {
        private readonly ActionAsync<object> _Execute;
        private readonly Func<object, bool> _CanExecute;

        public LambdaCommandAsync(ActionAsync<object> Execute, Func<object, bool> CanExecute = null)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }

        public override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => _Execute(parameter);
    }
}