using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T I
    {
		get
		{
			if (instance == null)
				instance = FindObjectOfType<T>();
			return instance;
		}
	}

	public static T AutoInstance
	{
		get
		{
			if (instance == null)
				instance = FindObjectOfType<T>();
			if (instance == null || !instance.gameObject)
			{
				GameObject instObject = new GameObject(typeof(T).FullName, typeof(T));
				instance = instObject.GetComponent<T>();
				//instance.tag = "Persistent";
			}
			return instance;
		}
	}

	protected static T instance;

    public void SetInstance(T i)
    {
        instance = i;
    }


    protected bool CheckInstance()
    {
        return instance != null;
    }
}