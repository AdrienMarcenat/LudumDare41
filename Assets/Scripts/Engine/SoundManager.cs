using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : Singleton<SoundManager>
{
	[SerializeField] AudioSource efxSource;
	[SerializeField] AudioSource musicSource;
	private string m_CurrentlyPlayingMusic = "";

	private Dictionary<string, AudioClip> m_Musics;

	void Start ()
	{
		m_Musics = new Dictionary<string, AudioClip> ();
		Object[] musics = Resources.LoadAll ("Audio/Music", typeof(AudioClip));
		foreach (Object music in musics) {
			AudioClip clip = music as AudioClip;
			m_Musics.Add (clip.name, clip);
		}
	}

	static public void PlaySingle (AudioClip clip)
	{
		instance.efxSource.clip = clip;
		instance.efxSource.Play ();
	}

	static public void PlayMultiple (AudioClip clip)
	{
		instance.efxSource.PlayOneShot (clip);
	}

	static public void PlayMusic (AudioClip clip)
	{
		if (instance.musicSource.clip != clip || !instance.musicSource.isPlaying) {
			instance.musicSource.clip = clip;
			instance.musicSource.Play ();
			instance.musicSource.loop = true;
		}
	}

	public void PlayMusicFromName (string name)
	{
		if (name != m_CurrentlyPlayingMusic) {
			musicSource.Stop ();
			m_CurrentlyPlayingMusic = name;
			if (!m_Musics.ContainsKey (name)) {
				Debug.Log ("unknonw music : " + name);
			}
			musicSource.clip = m_Musics [name];
			musicSource.loop = true;
			musicSource.Play ();
		}
	}

	static public void StopMusic ()
	{
		instance.musicSource.Stop ();
		instance.StopAllCoroutines ();
	}

	static public void RestartMusicAt (float time)
	{
		instance.musicSource.time = time;
	}

	static public void PlayMusicRange (string name, float beginTime, float endTime)
	{
		instance.PlayMusicRangeInternal (name, beginTime, endTime);
	}

	private void PlayMusicRangeInternal (string name, float beginTime, float endTime)
	{
		if (name != m_CurrentlyPlayingMusic) {
			musicSource.Stop ();
			m_CurrentlyPlayingMusic = name;
			if (!m_Musics.ContainsKey (name)) {
				Debug.Log ("unknonw music : " + name);
			}
			musicSource.clip = m_Musics [name];
			musicSource.time = beginTime;
			musicSource.Play ();
		}
		StopAllCoroutines ();
		instance.StartCoroutine (instance.PlayMusicRangeRoutine (beginTime, endTime));
	}

	IEnumerator PlayMusicRangeRoutine (float beginTime, float endTime)
	{
		while (true) {
			yield return null;
			if (musicSource.time > endTime)
				musicSource.time = beginTime;
		}
	}
}