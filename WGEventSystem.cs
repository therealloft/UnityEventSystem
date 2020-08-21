using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WelcomeGames.Events;
using UnityEngine.SceneManagement;
using System;

namespace WelcomeGames.Events{

	public class WGEventSystem : MonoBehaviour
	{
		private static WGEventSystem instance;
		public static WGEventSystem Instance {
			get {
				if (instance == null) instance = GameObject.FindObjectOfType<WGEventSystem>();
				return instance;
			}
		}
		delegate void EventListener(BaseEvent be);
		Dictionary<System.Type, List<EventListener>> eventListeners;
		private void Awake()
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
			SceneManager.activeSceneChanged += OnSceneChanged;
		}
		private void OnSceneChanged(Scene arg0, Scene arg1)
		{
			UnregisterAllListeners();
		}
		private void UnregisterAllListeners()
		{
			eventListeners = null;
		}
		
		public void RegisterListener<T>(System.Action<T> listener) where T : BaseEvent
		{
			System.Type eventType = typeof(T);
			if (eventListeners == null) eventListeners = new Dictionary<System.Type, List<EventListener>>();
			if (!eventListeners.ContainsKey(eventType) || eventListeners[eventType] == null) eventListeners[eventType] = new List<EventListener>();
			EventListener wrapper = (be) => { listener((T)be); };
			eventListeners[eventType].Add(wrapper);
		}
		
		public void UnregisterListener<T>(System.Action<T> listener) where T : BaseEvent
		{
			System.Type eventType = typeof(T);
			if (eventListeners != null && eventListeners[eventType] != null)
			{
				EventListener wrapper = (be) => { listener((T)be); };
				if (eventListeners[eventType].Contains(wrapper)) eventListeners[eventType].Remove(wrapper);
			}
		}
		
		public void TriggerEvent(BaseEvent evt)
		{
			System.Type eventType = evt.GetType();
			if (eventListeners != null)
			{
				if (eventListeners.ContainsKey(eventType))
				{
					foreach (EventListener el in eventListeners[eventType]) el(evt);
				}
			}
		}
	}
}