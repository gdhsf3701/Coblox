using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;

    private static bool IsDestroyed = false;

    public static T Instance
    {
        get
        {
            if (IsDestroyed)
                _instance = null;
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
                if (_instance == null)
                    Debug.LogError($"{typeof(T).Name}singleton is not exist");
                else
                    IsDestroyed = false;
            }

            return _instance;

        }
    }

    private void OnDisable()
    {
        IsDestroyed = true;
    }
}
