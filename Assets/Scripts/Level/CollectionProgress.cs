using Core.Services;

namespace Game
{
    public class CollectionProgress : ICollectionProgress
    {
        private readonly CollectionItemType _type;
        private readonly string _name;
        private readonly int _target;
        private int _count;

        CollectionItemType ICollectionProgress.Type => _type;
        string ICollectionProgress.Name => _name;
        public int Target => _target;
        int ICollectionProgress.Count => _count;
        bool ICollectionProgress.InProgress => _count < _target;

        public CollectionProgress(ILevelCollectableConfig config)
        {
            _type = config.Type;
            _name = config.Name;
            _target = config.Count;
            _count = 0;
        }

        void ICollectionProgress.AddProgress(int progress)
        {
            if (_count == _target) return;

            _count += progress;
            
            if(_count > _target) _count = _target;
        }
    }
}