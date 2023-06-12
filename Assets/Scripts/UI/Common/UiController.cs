using VContainer;

namespace UI.Common
{
    public abstract class UiController<T> : IUiController
        where T : UiView
    {
        [Inject] protected readonly T View;

        public void Close()
        {
            View.gameObject.SetActive(false);
        }

        public void Open()
        {
            View.gameObject.SetActive(true);
        }
    }
}
