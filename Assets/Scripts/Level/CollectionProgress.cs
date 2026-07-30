using Core.Services;

namespace Game
{
    public class CollectionProgress : ICollectionProgress
    {
        private readonly CollectionItemType _type;
        private readonly string _name;
        private int _count;
        
        CollectionItemType ICollectionProgress.Type => _type;
        string ICollectionProgress.Name => _name;
        int ICollectionProgress.Count => _count;

        public CollectionProgress(ILevelCollectableConfig config)
        {
            _type = config.Type;
            _name = config.Name;
            _count = config.Count;
        }

        void ICollectionProgress.AddProgress(int progress)
        {
            if (_count == 0) return;
            if (_count <= progress)
            {
                _count = 0;
                return;
            }

            _count -= progress;
        }
    }
}