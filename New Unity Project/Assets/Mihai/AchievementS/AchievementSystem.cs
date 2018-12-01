using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSystem : MonoBehaviour {

    [HideInInspector]
    public static AchievementSystem instance;

    [Header("Linking")]
    public GameObject achievementBox;
    public Text achieventText;
    public Image achievementImage;
    public Text achievementName;

    [Header("Sets")]

    public AchievementSet killTenEnemiesSet;
    private bool _killTenEnemiesFinished = false;
    private int _enemiesKilled = 0;
    private int _desiredKills = 1;

    public AchievementSet CollectCoinsSet;
    private bool _coinsFinished = false;
    private int _coinsCollected = 0;
    private int _desiredCoins = 10;

    private Animator _anim;
    private float _maxTimeOnScreen = 4;
    private float _timeLeftOnScreen = 0;
    private bool _isOnScreen = false;

    


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start () {
        _anim = achievementBox.GetComponent<Animator>();
	}
	
    public void KillEnemy()
    {
        if(_killTenEnemiesFinished == true)
        {
            return;
        }

        _enemiesKilled++;
        if(_enemiesKilled >= _desiredKills)
        {
            _killTenEnemiesFinished = true;
            DisplayAchievement(killTenEnemiesSet);
        }

    }

    public void PickUpCoin()
    {
        if(_coinsFinished == true)
        {
            return; 
        }
        _coinsCollected++;

        if(_coinsCollected >= _desiredCoins)
        {
            _coinsFinished = true;
            DisplayAchievement(CollectCoinsSet);
        }
    }


	// Update is called once per frame
	void Update () {

      //  Debug.Log(_anim.GetBool("isOnScreen"))  ;
        if(Input.GetKeyDown(KeyCode.T))
        {
            KillEnemy();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            PickUpCoin();
        }


        if (_isOnScreen == true)
        {
            if(_timeLeftOnScreen > 0 )
            {
                _timeLeftOnScreen -= Time.deltaTime;
            }
            else
            {
                _isOnScreen = false;
                HideAchievement();
            }
        }
	}

    private void DisplayAchievement(AchievementSet pAS)
    {
        _isOnScreen = true;
        _timeLeftOnScreen = _maxTimeOnScreen;
        achievementImage.sprite = pAS.aImage;
        achieventText.text = pAS.aLine;
        achievementName.text = pAS.aName;
        _anim.SetBool("isOnScreen", true);

    }

    private void HideAchievement()
    {
        _anim.SetBool("isOnScreen", false);
    }

    public void ResetSystem()
    {
        _enemiesKilled = 0;
        _coinsCollected = 0;
        _killTenEnemiesFinished = false;
        _coinsFinished = false;

    }

 public bool GetResultKilling()
    {
        return _killTenEnemiesFinished;
    }
    public bool GetResultCoins()
    {
        return _coinsFinished;
    }

    public bool GetResultTime()
    {
        return false;
    }

    public bool GetResultHealth()
    {
        if(Unit.Player == null)
        {
            return false;
        }
        return Unit.Player.Stats.CurrentHealth >= 50 ? true : false;
    }

}
