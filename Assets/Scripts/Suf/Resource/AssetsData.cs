using System.Linq;

namespace Suf.Resource
{
    public class AssetsData
    {
        private string _name;
        private string _path;
        private UnityEngine.Object[] _assets;

        public AssetsData(string path)
        {
            _path = path;
            _name = System.IO.Path.GetFileNameWithoutExtension(path);
        }

        public string Name
        {
            get => _name;
        }

        public string Path
        {
            get => _path;
        }

        public int RefCount { get; set; }

        public UnityEngine.Object[] Assets { get => _assets; }

        public T Asset<T>() where T : UnityEngine.Object
        {
            return _assets.Where(asset => asset.GetType() == typeof(T)).Cast<T>().FirstOrDefault();
        }
    }
}