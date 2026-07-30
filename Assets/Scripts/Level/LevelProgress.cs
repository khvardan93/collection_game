using System.Collections.Generic;
using Core.Services;

namespace Game
{
    public class LevelProgress : ILevelProgress
    {
        private readonly List<CollectionProgress> _collectionItems = new ();

        IReadOnlyList<ICollectionProgress> ILevelProgress.CollectionItems => _collectionItems;

        public LevelProgress(LevelConfig levelConfig)
        {
            foreach (ILevelCollectableConfig collectable in levelConfig.Collectables)
            {
                _collectionItems.Add(new CollectionProgress(collectable));
            }
        }
    }
}