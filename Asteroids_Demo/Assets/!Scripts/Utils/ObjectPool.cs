using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class ObjectPool : Singleton<ObjectPool>
    {
        private List<GameObject> _itemsPool = new List<GameObject>();

        public T GetItem<T>(T obj, Vector3 position, Quaternion rotation) where T : MonoBehaviour
        {
            for (int i = 0; i < _itemsPool.Count; i++)
            {
                var poolItem = _itemsPool[i];

                if (!poolItem.activeInHierarchy && poolItem.TryGetComponent<T>(out var type))
                {
                    //poolItem.SetActive(true);
                    poolItem.transform.SetPositionAndRotation(position, rotation);
                    return poolItem.GetComponent<T>();
                }
            }

            var instance = Instantiate(obj, position, rotation, transform);
            _itemsPool.Add(instance.gameObject);

            return instance;
        }

        public void Recycle(GameObject obj)
        {
            obj.SetActive(false);
        }

        public void CleanPool()
        {
            foreach (var item in _itemsPool)
                Destroy(item);

            _itemsPool.Clear();
        }
    }
}