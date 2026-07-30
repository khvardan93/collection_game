using Core.Services;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "TimeCollectionItem", menuName = "Configs/TimeCollectionItem")]
    public class TimeCollectionItem : BaseCollectionItem
    {
        [SerializeField] private int _timeToAdd;
        
        public  int TimeToAdd => _timeToAdd;
    }
}