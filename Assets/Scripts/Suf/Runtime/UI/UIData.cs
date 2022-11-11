namespace Suf.UI
{
    public struct UIData
    {
        public readonly string key;
        public readonly LayerType layer;

        public UIData(string key, LayerType layer)
        {
            this.key = key;
            this.layer = layer;
        }
    }
}