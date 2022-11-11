namespace Suf.UI
{
    public interface IPanel
    {
        public abstract void OnInit(UIData data);

        public abstract void OnOpen();
        
        public abstract void OnClose();

        public abstract void OnPause();

        public abstract void OnResume();
    }
}