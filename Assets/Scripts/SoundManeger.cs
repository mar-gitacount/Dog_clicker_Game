using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManeger : MonoBehaviour
{
    // 以下の用にすると、Inspectorから値をセットできるようになる。
    [System.Serializable]public class SoudData
    {
        public string name;
        public AudioClip audioClip;
        // 前回再生した時間
        public float playedTime;
    }
    // 一度再生してから、次再生するまでの間隔(秒)
    [SerializeField]private float playableDistance = 0.2f;
    [SerializeField]private SoudData[] soudDatas;

    // AudioSource(スピーカー)を同時に鳴らしたい音の数だけ用意。
    private AudioSource[] audioSourceList = new AudioSource[20];
    // 別名(name)をキーとした管理用Dicitionary
    private Dictionary<string,SoudData> soundDictionay = new Dictionary<string, SoudData>();

    // 1つであることを保障するため＆グローバルアクセス用
    public static SoundManeger Instance
    {
        private set;
        get;
    } 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
            return;
        }
      for (var i = 0; i < audioSourceList.Length; ++i)
      {
        // 配列が現段階だと20個まで格納できるので、その分ループし、AudioSourcesを格納する。
        audioSourceList[i] = gameObject.AddComponent<AudioSource>();
      }
    //   soundDictionaryにセット
    foreach(var soundData in soudDatas)
    {
        soundDictionay.Add(soundData.name,soundData);
    }
    }
    // 未使用のAudioSourceの取得全て使用中の場合はnullを返却
    private AudioSource  GetUnusedAudioSource()
    {
        for(var i =0; i < audioSourceList.Length; ++i)
        {
            if(audioSourceList[i].isPlaying == false) return audioSourceList[i];
            
        }
        return null;
    }

    public void Play(AudioClip clip)
    {
        var audioSource = GetUnusedAudioSource();
        // 再生できない
        if(audioSource == null) return;
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void Play(String name)
    {
        if(soundDictionay.TryGetValue(name,out var soudData))
        {
            // まだ再生する。
            if(Time.realtimeSinceStartup - soudData.playedTime < playableDistance) return;
            // 次回用に今回の再生時間の保持
            soudData.playedTime = Time.realtimeSinceStartup;
            // 見つかったら再生
            Play(soudData.audioClip);
        }
        else
        {
            Debug.LogWarning($"その別名は登録されていません{name}");
        }
    }


}
