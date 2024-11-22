using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using static SoundManager;

public class SoundManager : MonoBehaviour
{
    //BGM�� audio �ͼ��� ������� �ʰ�
    //source�� �ϳ����̹Ƿ� �װ��� volume�� ��ü �����ϴ� ������� ����-
    AudioClip[] BGMClips = new AudioClip[System.Enum.GetValues(typeof(EBGM)).Length];
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
                Debug.LogError("init�Լ��� ���۵��� �ʾ���!!");
            }
            return instance;
        }
    }

    public static SoundManager Init()
    {
        //�߰��� ���ǵ����� load
        instance = new GameObject().AddComponent<SoundManager>();
        instance.BGMPlayer = instance.AddComponent<AudioSource>();

        instance.BGMPlayer.loop = true;
        DontDestroyOnLoad(instance);

        //���� ����
        SetSound(ESoundEffect.SE_Beat_01);


        return instance;
    }

    private static void SetSound(ESoundEffect soundEffect)
    {
        instance.SFXClips[(int)soundEffect] = Resources.Load("Sound/" + soundEffect.ToString()) as AudioClip;
    }

    private static void SetSound(ESoundEffect soundEffect, string path)
    {
        instance.SFXClips[(int)soundEffect] = Resources.Load<AudioClip>(path);
    }

    private static void SetSound(EBGM bgm)
    {
        instance.BGMClips[(int)bgm] = Resources.Load<AudioClip>("Sound/" + bgm.ToString());
    }
    private static void SetSound(EBGM bgm, string path)
    {
        instance.BGMClips[(int)bgm] = Resources.Load<AudioClip>(path);
    }


    void PlayBGM(EBGM? bgm)
    {
        BGMPlayer.Stop();
        if (bgm == null)
        {
            //bgm ����
            Debug.Log("bgm ������");
        } else
        {
            
            BGMPlayer.clip = BGMClips[(int)bgm];
            BGMPlayer.Play();
            Debug.Log("���ǽ��� : " + bgm.ToString());
        }
    }

    public void PlaySE(ESoundEffect soundEffect)
    {
        AudioSource audioSource = new GameObject().AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = SFXAudioMixer;
        audioSource.clip = SFXClips[(int)soundEffect];
        audioSource.Play();
        Destroy(audioSource.gameObject, SFXClips[(int)soundEffect].length);
        //ȿ������ ���� ������Ʈ�� ������ ����
        Debug.Log("ȿ���� ���� : " + soundEffect.ToString());
    }

    //BGM�� 1.5�ʵ��� õõ�� ���� �ڷ�ƾ�Լ�
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


    //�����߰��ϴ� ���
    public enum ESoundEffect
    {
        SE_Beat_01,
        sound2,

    }
    public enum EBGM
    {
        sound1,
        sound2,

    }

}
