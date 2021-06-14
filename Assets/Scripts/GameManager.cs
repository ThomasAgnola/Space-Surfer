using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public enum GAMESTATE { menu, play, pause, victory, gameover, highscore, credit }

public class GameManager : MonoBehaviour, IEventHandler
{
    private static GameManager m_Instance;

    public static GameManager Instance { get { return m_Instance; } }

    private GAMESTATE m_State;

    public bool IsPlaying { get { return m_State == GAMESTATE.play; } }

    [SerializeField] int m_ScoreToVictory;
    int m_Score;

    [SerializeField] float m_CountDownStartValue;
    float m_CountDown;

    [SerializeField] GameObject[] asteroids;
    GameObject[] m_Asteroids;

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.AddListener<BallHitSomethingEvent>(BallHitSomething);
        EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
        EventManager.Instance.AddListener<HighScoreButtonClickedEvent>(HighScoreButtonClicked);
        EventManager.Instance.AddListener<EscapeButtonClickedEvent>(EscapeButtonClicked);
        EventManager.Instance.AddListener<CreditButtonClickedEvent>(CreditButtonClicked);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked); 
        EventManager.Instance.RemoveListener<BallHitSomethingEvent>(BallHitSomething);
        EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
        EventManager.Instance.RemoveListener<HighScoreButtonClickedEvent>(HighScoreButtonClicked);
        EventManager.Instance.RemoveListener<EscapeButtonClickedEvent>(EscapeButtonClicked);
        EventManager.Instance.RemoveListener<CreditButtonClickedEvent>(CreditButtonClicked);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    void SetStatistics(int score, float countdown)
    {
        m_Score = score;
        m_CountDown = countdown;
        EventManager.Instance.Raise(new GameStatisticsChangedEvent() { eScore = score, eCountDownValue = Mathf.Max(countdown,0) });
    }

    void InitGame()
    {
        SetStatistics(0, m_CountDownStartValue);
    }

    void Victory()
    {
        m_State = GAMESTATE.victory;
        EventManager.Instance.Raise(new GameVictoryEvent());
    }
    void GameOver()
    {
        m_State = GAMESTATE.gameover;
        EventManager.Instance.Raise(new GameOverEvent());
    }

    void IncrementScore(int scoreIncrement)
    {
        m_Score += scoreIncrement;
        SetStatistics(m_Score, m_CountDown);

        if(m_Score >= m_ScoreToVictory)
        {
            Victory();
        }
    }

    #region Events callbacks
    void PlayButtonClicked(PlayButtonClickedEvent e)
    {
        Debug.Log("event PlayButtonClicked received by + " + name);
        m_State = GAMESTATE.play;

        InitGame();
        EventManager.Instance.Raise(new GamePlayEvent());
    }
    void MainMenuButtonClicked(MainMenuButtonClickedEvent e)
    {
        m_State = GAMESTATE.menu;
        EventManager.Instance.Raise(new GameMenuEvent());
    }

    void HighScoreButtonClicked(HighScoreButtonClickedEvent e)
    {
        m_State = GAMESTATE.highscore;
        EventManager.Instance.Raise(new GameHighScoreEvent());
    }

    void EscapeButtonClicked(EscapeButtonClickedEvent e)
    {
        m_State = GAMESTATE.menu;
        if (m_State == GAMESTATE.menu)
        {
            Application.Quit();
        }
    }
    void CreditButtonClicked(CreditButtonClickedEvent e)
    {
        m_State = GAMESTATE.credit;
        EventManager.Instance.Raise(new GameCreditEvent());
    }
    void BallHitSomething(BallHitSomethingEvent e)
    {
        if (!IsPlaying) return;
        
        IScore score = e.eHitGO.GetComponentInChildren<IScore>();
        if(score != null)
        {
            IncrementScore(score.Score);
        }
    }
    #endregion


    private void Awake()
    {
        if(m_Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Instance = this;
        }
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        //yield return new WaitForSeconds(4);

        m_State = GAMESTATE.menu;
        EventManager.Instance.Raise(new GameMenuEvent());

        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying)
        {
            SetStatistics(m_Score, m_CountDown - Time.deltaTime);

            if(m_CountDown < 0)
            {
                GameOver();
            }

            if(asteroids != null)
            {
                m_Asteroids = asteroids;
                for (int i =0; i < m_Asteroids.Length; i++)
                            {
                                m_Asteroids[i].transform.position = transform.position - transform.forward;
                            }
            }
            
        }
    }
}
