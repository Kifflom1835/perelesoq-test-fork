using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;
    private static readonly object _lock = new object();
    private static bool _isApplicationQuitting = false;

    public static T Instance
    {
        get
        {
            if (_isApplicationQuitting)
            {
                Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' already destroyed. Returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    // Search for existing instance
                    _instance = Object.FindAnyObjectByType<T>();

                    // Create new instance if none exists
                    if (_instance == null)
                    {
                        var singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = $"{typeof(T)} (Singleton)";

                        // Make persistent if marked with attribute
                        if (typeof(T).GetCustomAttributes(typeof(PersistentSingletonAttribute), true).Length > 0)
                        {
                            DontDestroyOnLoad(singletonObject);
                        }
                    }
                }
                return _instance;
            }
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this as T;

            // Auto-configure as persistent if marked
            if (GetType().GetCustomAttributes(typeof(PersistentSingletonAttribute), true).Length > 0)
            {
                DontDestroyOnLoad(gameObject);
            }

            Initialize();
        }
    }

    public virtual void Initialize() { }

    private void OnApplicationQuit()
    {
        _isApplicationQuitting = true;
    }
}

// Optional attribute to mark persistent singletons
public class PersistentSingletonAttribute : System.Attribute { }