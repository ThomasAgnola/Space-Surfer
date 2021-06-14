using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SDD.Events;

public class HighScoreManager : MonoBehaviour, IEventHandler
{
    [SerializeField] Text HighScore_menu;

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameHighScoreEvent>(HighScoreEvent);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameHighScoreEvent>(HighScoreEvent);
    }

    void HighScore()
    {
        //Debug.Log("contenu de highscore : " + GameManager.Instance.highScore.ToString());
        HighScore_menu.text = GameManager.Instance.highScore.ToString();
        //Debug.Log("placement de highscore dans zone de text");
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    void HighScoreEvent(GameHighScoreEvent e)
    {
        //Debug.Log("Arrivez dans highscore button");
        HighScore(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
