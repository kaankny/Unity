using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
	public static SaveManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void Save<T>(string key, T data)
	{
		string json = JsonUtility.ToJson(data);
		PlayerPrefs.SetString(key, json);
	}

	public T Load<T>(string key)
	{
		string json = PlayerPrefs.GetString(key);
		return JsonUtility.FromJson<T>(json);
	}
}
