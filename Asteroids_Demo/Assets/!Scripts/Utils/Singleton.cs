using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                _instance = FindObjectOfType<T>();
                if (_instance != null)
                {
                    return _instance;
                }

                var obj = new GameObject();
                obj.hideFlags = HideFlags.DontSave;
                _instance = obj.AddComponent<T>();
                obj.name = typeof(T).Name;
                return _instance;
            }
        }

        public virtual void Awake()
        {
            //DontDestroyOnLoad(this.gameObject);
            if (_instance != null && !ReferenceEquals(_instance, this))
            {
                Destroy(gameObject);
                return;
            }

            _instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            if (object.ReferenceEquals(_instance, this))
            {
                _instance = null;
            }
        }
    }
}
