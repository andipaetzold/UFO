namespace UFO.Commander.Commands.Login
{
    using UFO.Commander.ViewModels;

    internal class LoginCommand : CommandBase<LoginViewModel>
    {
        public LoginCommand(LoginViewModel viewModel)
            : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            ViewModel.Login();
        }
    }
}
