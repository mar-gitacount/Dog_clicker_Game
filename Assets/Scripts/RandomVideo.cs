using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
public class RandomVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClips;
    // [SerializeField]private Text TextObject;

    // ?テストコード
    [SerializeField] public Button PuseButton;

    void Start()
    {
        PlayRandomVideo();
        // TextObject.text = "ランダムビデオテスト";
        Debug.Log("次のビデオへ");
        videoPlayer.loopPointReached += OnVideoEnded;
        PuseButton.onClick.AddListener(() => OnVideoPrepared(videoPlayer));
    }

    void Update()
    {
        Debug.Log("ランダムビデオテスト");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayRandomVideo(); // スペースキーで動画を切り替える
        }
    }

    void PlayRandomVideo()
    {
        if (videoClips.Length == 0) return;

        int index = Random.Range(0, videoClips.Length);
        videoPlayer.clip = videoClips[index];
        videoPlayer.isLooping = false; // ループしない
        // videoPlayer.prepareCompleted -= OnVideoPrepared; // 重複防止
        // videoPlayer.prepareCompleted += OnVideoPrepared;
        // videoPlayer.Prepare();
        videoPlayer.Play(); // 直接再生
    }





    void OnVideoPrepared(VideoPlayer vp)
    {
        if (vp.isPlaying)
        {
            vp.Stop();
            
        }
        else
        {
            vp.Play();
        }
    }
    
    void OnVideoEnded(VideoPlayer vp)
    {
        PlayRandomVideo();

    }

    // 動画が終わったら次の動画を再生

}
