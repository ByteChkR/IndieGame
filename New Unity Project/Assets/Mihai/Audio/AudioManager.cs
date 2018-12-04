using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [HideInInspector]
    static public AudioManager instance;

    public enum SoundEffect {
        PlayerHit, Click, Explosion, EnemyHit, Achievement, Dash, NPC,
        Beam, PickUp, Buy, NotEnoughMoney, FirstBossWave, FirstBossSpecialAttack,
        Glitch, LaserBeam, LaserCharge, Rocket, ElectricSound, FootSteps, SpiderFootSteps, LightSwing, HeavySwing
    }
    public enum BackgroundMusic { Menu, Stage1, Boss1, Stage2, Boss2, Result }

    [Header("VolumeMixer")]
    public float MasterVolume;
    public float SoundEffectVolume;
    public float BackgroundMusicVolume;

    [Header("AudioSources")]
    public AudioSource BackgroundAudioSource;
    public AudioSource SoundEffectSource;

    [Header("OneShots")]
    public AudioClip PlayerHit;
    public AudioClip  Click, Explosion, EnemyHit, Achievement, Dash, NPC,
        Beam, PickUp, Buy, NotEnoughMoney, FirstBossWave,FirstBossSpecialAttack, Glitch, LaserBeam, LaserCharge, Rocket, ElectricSound, FootSteps, SpiderFootSteps, LightSwing, HeavySwing;


    [Header("BackgroundClips")]
    public AudioClip Menu;
    public AudioClip Stage1, Boss1, Stage2, Boss2, Result,GameOverLoop;

    private BackgroundMusic _currentBackgroundMusic;
    private BackgroundMusic _nextBackgroundMusic;

    private float _crossFader = 0;
    private float _secondsforCrossFader = 4;
    private bool _isClipChanged = true;
    private bool _isInGameOverScreen = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Use this for initialization
    void Start()
    {
        //  instance.PlaySoundEffect(SoundEffect.Click);
        _currentBackgroundMusic = BackgroundMusic.Menu;
        _nextBackgroundMusic = BackgroundMusic.Menu;
    }

    public void PlaySoundEffect(int effect)
    {
        PlaySoundEffect((SoundEffect)effect);
    }

    public void ChangeBackgroundMusic(int music)
    {
        ChangeBackgroundMusic((BackgroundMusic)music);
    }

    public void PlaySoundEffect(SoundEffect soundEffect)
    {
        switch (soundEffect)
        {
      
            case SoundEffect.PlayerHit:
                SoundEffectSource.PlayOneShot(PlayerHit);
                break;

            case SoundEffect.Click:
                SoundEffectSource.PlayOneShot(Click);
                break;

            case SoundEffect.Explosion:

                SoundEffectSource.PlayOneShot(Explosion);

                break;

            case SoundEffect.EnemyHit:

                SoundEffectSource.PlayOneShot(EnemyHit);

                break;

            case SoundEffect.Achievement:

                SoundEffectSource.PlayOneShot(Achievement);

                break;

            case SoundEffect.Dash:

                SoundEffectSource.PlayOneShot(Dash);

                break;

            case SoundEffect.NPC:

                SoundEffectSource.PlayOneShot(NPC);

                break;
            case SoundEffect.Beam:

                SoundEffectSource.PlayOneShot(Beam);

                break;
            case SoundEffect.PickUp:

                SoundEffectSource.PlayOneShot(PickUp);

                break;

            case SoundEffect.Buy:

                SoundEffectSource.PlayOneShot(Buy);

                break;
            case SoundEffect.NotEnoughMoney:

                SoundEffectSource.PlayOneShot(NotEnoughMoney);

                break;

            case SoundEffect.FirstBossWave:
                SoundEffectSource.PlayOneShot(FirstBossWave);
                break;
            case SoundEffect.FirstBossSpecialAttack:
                SoundEffectSource.PlayOneShot(FirstBossSpecialAttack);
                break;
            case SoundEffect.Glitch:
                SoundEffectSource.PlayOneShot(Glitch);
                break;
            case SoundEffect.LaserBeam:
                SoundEffectSource.PlayOneShot(LaserBeam);
                break;
            case SoundEffect.LaserCharge:
                SoundEffectSource.PlayOneShot(LaserCharge);
                break;
            case SoundEffect.Rocket:

                SoundEffectSource.PlayOneShot(Rocket);

                break;
            case SoundEffect.ElectricSound:
                SoundEffectSource.PlayOneShot(ElectricSound);
                break;
            case SoundEffect.FootSteps:
                SoundEffectSource.PlayOneShot(FootSteps);
                break;
            case SoundEffect.SpiderFootSteps:

                SoundEffectSource.PlayOneShot(SpiderFootSteps);

                break;
            // LightSwing, HeavySwing
            case SoundEffect.LightSwing:

                SoundEffectSource.PlayOneShot(LightSwing);

                break;
            case SoundEffect.HeavySwing:

                SoundEffectSource.PlayOneShot(HeavySwing);

                break;
        }

    }

    void FixedUpdate()
    {
        // Debug.Log(MusicVolume+ "     "+ MainVolume+"         "+ EffectsVolume);
        // BackgroundAudioSource.volume = MusicVolume / MainVolume;
        // SoundEffectAudioSource.volume = EffectsVolume / MainVolume;

        //Debug.Log(_crossFader);

        if(_isInGameOverScreen == true)
        {
            return;
        }
        BackgroundMusicControl();

    }


    private void BackgroundMusicControl()
    {
        if (_currentBackgroundMusic != _nextBackgroundMusic)
        {
            if (_crossFader > _secondsforCrossFader / 2)
            {
                BackgroundAudioSource.volume = ((_crossFader - (_secondsforCrossFader / 2)) / (_secondsforCrossFader / 2)) * MasterVolume * BackgroundMusicVolume;
            }
            else
            if (_crossFader <= _secondsforCrossFader / 2 && _crossFader > 0)
            {
                ChangeClips();
                BackgroundAudioSource.volume = (1 - (_crossFader / (_secondsforCrossFader / 2))) * MasterVolume * SoundEffectVolume;
            }
            else
            {
                _currentBackgroundMusic = _nextBackgroundMusic;
                return;
            }


        }
        else
        {
            _crossFader = 0;
        }

        if (_crossFader > 0)
        {
            _crossFader -= Time.deltaTime;
        }

    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeBackgroundMusic(BackgroundMusic.Tutorial);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeBackgroundMusic(BackgroundMusic.Menu);
        }
        */
      /*  if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeBackgroundMusic(BackgroundMusic.Tutorial);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeBackgroundMusic(BackgroundMusic.Stage1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeBackgroundMusic(BackgroundMusic.Menu);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Restart();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            GameOverScreen();
        }*/
    }

    private void ChangeClips()
    {
        if (_isClipChanged == false)
        {
            _isClipChanged = true;
            switch (_nextBackgroundMusic)
            {
                case BackgroundMusic.Menu:
                    BackgroundAudioSource.clip = Menu;
                    break;
    

                case BackgroundMusic.Stage1:
                    BackgroundAudioSource.clip = Stage1;
                    break;

                case BackgroundMusic.Boss1:
                    BackgroundAudioSource.clip = Boss1;
                    break;

                case BackgroundMusic.Stage2:
                    BackgroundAudioSource.clip = Stage2;
                    break;

                case BackgroundMusic.Boss2:
                    BackgroundAudioSource.clip = Boss2;
                    break;
                case BackgroundMusic.Result:
                    BackgroundAudioSource.clip = Result;
                    break;

            }

            BackgroundAudioSource.Play();
        }
    }

    public void ChangeBackgroundMusic(BackgroundMusic pMusic)
    {
        /* if (pMusic == BackgroundMusic.Stage1)
         {
             BackgroundAudioSource.clip = Stage1;
             BackgroundAudioSource.Play();
             return;
         }*/

        if (_crossFader > 0)
        {
            //Debug.LogError("Clip was in transition, you changed the background music too fast.");
            return;
        }
        _isClipChanged = false;
        _crossFader = _secondsforCrossFader;
        _nextBackgroundMusic = pMusic;

    }

    public void Restart()
    {
        BackgroundAudioSource.volume = MasterVolume * BackgroundMusicVolume;
        BackgroundAudioSource.clip = Menu;
        _currentBackgroundMusic = BackgroundMusic.Menu;
        _nextBackgroundMusic = BackgroundMusic.Menu;
        _isClipChanged = true;
        _crossFader = 0;
        _isInGameOverScreen = false;
        BackgroundAudioSource.Play();
    }

    public void GameOverScreen()
    {
        _isInGameOverScreen = true;
        BackgroundAudioSource.volume = MasterVolume * BackgroundMusicVolume;
        BackgroundAudioSource.clip = GameOverLoop;
        BackgroundAudioSource.Play();
    }
}
