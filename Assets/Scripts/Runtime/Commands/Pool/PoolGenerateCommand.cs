using Runtime.Controllers.Collectable;
using Runtime.Data.UnityObject;
using UnityEngine;

namespace Runtime.Commands.Pool
{
    public class PoolGenerateCommand
    {
        private  CD_Pool _poolData;
        private  Transform _poolManagerG;
        private GameObject _emptyGameObject;
        public PoolGenerateCommand(ref CD_Pool poolData, ref Transform poolManagerG, ref GameObject emptyGameObject)
        {
            _poolData = poolData;
            _poolManagerG = poolManagerG;
            _emptyGameObject = emptyGameObject;
        }

        public void Execute()
        {
            var pool = _poolData.PoolList;
            for (var i = 0; i < pool.Count; i++)
            {
                _emptyGameObject = new GameObject();
                _emptyGameObject.transform.parent = _poolManagerG;
                _emptyGameObject.name = pool[i].ObjName;

                for (var j = 0; j < pool[i].ObjectCount; j++)
                {
                    var obj = Object.Instantiate(pool[i].Pref,_poolManagerG.GetChild(i));
                    obj.SetActive(false);
                    

                }

            }

        }
    }
}