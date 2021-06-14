using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class MenuManager : MonoBehaviour, IEventHandler
{
    [SerializeField] GameObject m_MainMenuPanel;
    [SerializeField] GameObject m_VictoryPanel;
    [SerializeField] GameObject m_GameOverPanel;
    [SerializeField] GameObject m_HighScorePanel;
    [SerializeField] GameObject m_CreditPanel;

    private List<GameObject> m_AllPanels = new List<GameObject>();

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.AddListener<GameVictoryEvent>(GameVictory);
        EventManager.Instance.AddListener<GameOverEvent>(GameOver);
        EventManager.Instance.AddListener<GameHighScoreEvent>(HighScore);
        EventManager.Instance.AddListener<GameCreditEvent>(Credit);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.RemoveListener<GameVictoryEvent>(GameVictory);
        EventManager.Instance.RemoveListener<GameOverEvent>(GameOver);
        EventManager.Instance.RemoveListener<GameHighScoreEvent>(HighScore);
        EventManager.Instance.RemoveListener<GameCreditEvent>(Credit);
    }
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #region Events callbacks
    void GameMenu(GameMenuEvent e)
    {
        Debug.Log("event GameMenu received by + " + name);
        DisplayPanel(m_MainMenuPanel);
    }
    void GamePlay(GamePlayEvent e)
    {
        Debug.Log("event GamePlay received by + " + name);
        DisplayPanel(null);
    }
    void GameVictory(GameVictoryEvent e)
    {
        DisplayPanel(m_VictoryPanel);
    }
    void GameOver(GameOverEvent e)
    {
        DisplayPanel(m_GameOverPanel);
    }
    void HighScore(GameHighScoreEvent e)
    {
        Debug.Log("event HighScore received by + " + name);
        DisplayPanel(m_HighScorePanel);
    }
    void Credit(GameCreditEvent e)
    {
        DisplayPanel(m_CreditPanel);
    }
    #endregion

    private void Awake()
    {
        m_AllPanels.Add(m_MainMenuPanel);
        m_AllPanels.Add(m_VictoryPanel);
        m_AllPanels.Add(m_GameOverPanel);
        m_AllPanels.Add(m_HighScorePanel);
        m_AllPanels.Add(m_CreditPanel);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void DisplayPanel(GameObject panel)
    {
        m_AllPanels.ForEach(item => item.SetActive(item == panel));
    }
    // Update is called once per frame
    void Update()
    {

    }

    #region UI Callbacks
    public void PlayButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new PlayButtonClickedEvent());
    }

    public void MainMenuButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MainMenuButtonClickedEvent());
    }

    public void HighScoreButtonClicked()
    {
        EventManager.Instance.Raise(new HighScoreButtonClickedEvent());
    }

    public void EscapeButtonClicked()
    {
        EventManager.Instance.Raise(new EscapeButtonClickedEvent());
    }

    public void CreditButtonClicked()
    {
        EventManager.Instance.Raise(new GameCreditEvent());
    }
    #endregion
}
