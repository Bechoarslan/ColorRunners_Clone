using Runtime.Data.UnityObject;
using UnityEngine;

namespace Runtime.Commands.Pool
{
    public class RestartPoolCommand
    {
        private readonly CD_Pool _poolData;
        private readonly Transform _poolManagerG;
        private readonly GameObject _levelHolder;
        public RestartPoolCommand(ref CD_Pool poolData, ref Transform poolManagerG, ref GameObject levelHolder)
        {
            _poolData = poolData;
            _poolManagerG = poolManagerG;
            _levelHolder = levelHolder;
        }

        public void Execute()
        {
            var pool = _poolData.PoolList;
            for (var i = 0; i < pool.Count; i++)
            {
                var _child = _poolManagerG.GetChild(i);
                if (_child.transform.childCount > pool[i].ObjectCount)
                {
                    var count = _child.transform.childCount;
                    for (int j = pool[i].ObjectCount; j < count ; j++)
                    {
                        _child.GetChild(pool[i].ObjectCount).SetParent(_levelHolder.transform.GetChild(0));
                        
                    }
                }
                
            }
        }
    }
}