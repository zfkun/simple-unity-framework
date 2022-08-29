namespace Suf.UI
{
    public interface IPanel
    {
        public abstract void OnInit();

        public abstract void OnClose();

        public abstract void OnPause();

        public abstract void OnResume();
    }
}