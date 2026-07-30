using System.Collections.Generic;
using Core.Services;

namespace Game
{
    public class LevelProgress : ILevelProgress
    {
        private readonly List<CollectionProgress> _collectionItems = new ();
        private readonly int _duration;

        IReadOnlyList<ICollectionProgress> ILevelProgress.CollectionItems => _collectionItems;
        public int Duration => _duration;

        public LevelProgress(LevelConfig levelConfig)
        {
            _duration = levelConfig.Duration;
            
            foreach (ILevelCollectableConfig collectable in levelConfig.Collectables)
            {
                _collectionItems.Add(new CollectionProgress(collectable));
            }
        }
    }
}