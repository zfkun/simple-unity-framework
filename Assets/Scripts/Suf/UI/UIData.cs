namespace Suf.UI
{
    public class UIData
    {
        private string _key;
        private LayerType _layer;
        
        public UIData(string key, LayerType layer)
        {
            _key = key;
            _layer = layer;
        }

        public string Key => _key;
        public LayerType layer => layer;
    }
}