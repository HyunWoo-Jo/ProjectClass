using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignStruct;

public class SoundManager : Singleton<SoundManager> {
    private SortedDictionary<string, AudioClip> audioDictionary = new SortedDictionary<string, AudioClip>();
    [SerializeField]
    private GameObject audioSource;
    [SerializeField]
    private int audioSourceSize = 10;
    private List<AudioSource> audioList = new List<AudioSource>();

    private int currentAudioIndex = 0;

    protected override void Awake() {
        base.Awake();
        InstanceAudio();
        LoadClip();
    }

    private void InstanceAudio() {
        for(int i =0; i< audioSourceSize; i++) {
            GameObject obj = Instantiate(audioSource);
            obj.transform.SetParent(this.gameObject.transform);
            audioList.Add(obj.GetComponent<AudioSource>());
        }
    }

    private void LoadClip() {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("EFF");

        for(int i =0; i < clips.Length; i++) {
            audioDictionary.Add(clips[i].name, clips[i]);
        }
    }

    public void AddClip(string name, AudioClip clip) {
        audioDictionary.Add(name, clip);
    }

    public void DeleteClip(string name) {
        audioDictionary.Remove(name);
    }

    public void PlayEFF(string name) {
        AudioClip clip = audioDictionary[name];
        if(clip == null) return;

        if(currentAudioIndex >= audioList.Count) currentAudioIndex = 0;

        audioList[currentAudioIndex].clip = clip;
        audioList[currentAudioIndex].Play();
        
        currentAudioIndex++;
    }

    public static void Play_EFF(string name) {
        instance.PlayEFF(name);
    }
    

}
