namespace ReactiveBlazor;

public interface IConfirmationService: ITransient
{
        Task<bool> ConfirmAsync(string message);
        void Confirm(bool confirmed);
        event Action<string> OnShow;
        event Action OnHide;
}
