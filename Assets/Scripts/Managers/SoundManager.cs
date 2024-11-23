using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using static SoundManager;

public class SoundManager : MonoBehaviour
{
    //BGM은 audio 믹서를 사용하지 않고
    //source가 하나뿐이므로 그것의 volume을 자체 조절하는 방식으로 하자-
    AudioClip[] BGMClips = new AudioClip[System.Enum.GetValues(typeof(Ebgm)).Length];
    AudioSource BGMPlayer;

    
    AudioClip[] SFXClips = new AudioClip[System.Enum.GetValues(typeof(ESoundEffect)).Length];
    [SerializeField] AudioMixerGroup SFXAudioMixer;

    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = new GameObject().AddComponent<SoundManager>();
                instance.BGMPlayer = instance.AddComponent<AudioSource>();
                instance.BGMPlayer.volume = 0.2f;

                instance.BGMPlayer.loop = true;
                DontDestroyOnLoad(instance);

                //사운드 세팅
                SetSound(ESoundEffect.SE_BEAT_01);
                SetSound(ESoundEffect.SE_APPEAR_01);
                SetSound(ESoundEffect.SE_ANSWER_01);
                SetSound(ESoundEffect.SE_BEEP_01);
                SetSound(ESoundEffect.Exp);

                SetSound(Ebgm.SUMMER);
                SetSound(Ebgm.WINTER);
                SetSound(Ebgm.FALL);
                SetSound(Ebgm.SPRING);
            }
            return instance;
        }
    }


    private static void SetSound(ESoundEffect soundEffect)
    {
        instance.SFXClips[(int)soundEffect] = Resources.Load("Sound/" + soundEffect.ToString()) as AudioClip;
    }

    private static void SetSound(ESoundEffect soundEffect, string path)
    {
        instance.SFXClips[(int)soundEffect] = Resources.Load<AudioClip>(path);
    }

    private static void SetSound(Ebgm bgm)
    {
        instance.BGMClips[(int)bgm] = Resources.Load<AudioClip>("Sound/BGM/" + bgm.ToString());
    }
    private static void SetSound(Ebgm bgm, string path)
    {
        instance.BGMClips[(int)bgm] = Resources.Load<AudioClip>(path);
    }


    public void PlayBGM(Ebgm? bgm)
    {
        BGMPlayer.Stop();
        if (bgm == null)
        {
            //bgm 중지
            Debug.Log("bgm 중지됨");
        } else
        {
            
            BGMPlayer.clip = BGMClips[(int)bgm];
            BGMPlayer.Play();
            Debug.Log("음악시작 : " + bgm.ToString());
        }
    }

    public void PlaySE(ESoundEffect soundEffect)
    {
        AudioSource audioSource = new GameObject().AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = SFXAudioMixer;
        audioSource.clip = SFXClips[(int)soundEffect];
        audioSource.Play();
        Destroy(audioSource.gameObject, SFXClips[(int)soundEffect].length);
        //효과음이 끝난 오브젝트를 적당히 삭제
        Debug.Log("효과음 시작 : " + soundEffect.ToString());
    }

    //BGM을 1.5초동안 천천히 끄는 코루틴함수
    public IEnumerator StopSound()
    {

        float timer = 0;
        while (timer <= 1)
        {
            timer += Time.deltaTime / 1.5f;
            BGMPlayer.volume = Mathf.Lerp(1, 0, timer);
            yield return null;
        }
        BGMPlayer.Stop();
    }


    //음악추가하는 방법
    public enum ESoundEffect
    {
        SE_BEAT_01,
        SE_ANSWER_01,
        SE_APPEAR_01,
        SE_BEEP_01,
        Exp

    }
    public enum Ebgm
    {
        SUMMER,
        WINTER,
        SPRING,
        FALL
    }

}
