using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public UIManager instance = null;

    public int offset = 0;

    public Image currentTurnColor;
    public Image currentTurnAvatar;

  //  public PlayerHUD playerHUD;
  //  public othersHUD[] opponentHUDs;

    public Button[] uiButtons;

  //  public PlayerCardHolder playerCardHolder;
  //  public PlayerCardPanel playerCardPanel;
  //  public GiveKnowledgePlayerPanel giveKnowledgePlayerPanel;
  //  public ReceiveKnowledgePlayerPanel receiveKnowledgePlayerPanel;

    //no script needed for this
    //public GameObject robberOrPiratePanel;
    public GameObject costspanel;
    public GameObject notificationpanel;
    public Text notificationtext;

 //   public NotificationPanel notificationPanel;
    public GameObject savePanel;
 //   public CardSelectPanel cardSelectPanel;
    public Button saveButton;
    public InputField filenameInput;
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start()
    {
        InitializeUIManager();
        SetupPlayers();
    }

    void Update(){
        currentTurnAvatar.sprite = PandemicSystem.instance.aPlayers[PandemicSystem.instance.currentPlayerTurn].avatar;
    }

    #region initializers

    void InitializeUIManager(){
        playerCardHolder.UIinstance = this;
        playerCardPanel.cardHolder = playerCardHolder;
        playerCardPanel.uiManager = this;
    }

    void SetupPlayers(){
        int key1 = (1 + offset) % PhotonNetwork.playerList.Length;

        Player player1 = LevelManager.instance.players[(0 + offset) % PhotonNetwork.playerList.Length];
        Player player2 = LevelManager.instance.players[(1 + offset) % PhotonNetwork.playerList.Length];

        if (PhotonNetwork.playerList.Length >= 3)
        {
            Player player3 = LevelManager.instance.players[(2 + offset) % PhotonNetwork.playerList.Length];

            if (PhotonNetwork.playerList.Length == 4)
            {
                Player player4 = LevelManager.instance.players[(3 + offset) % PhotonNetwork.playerList.Length];

                opponentHUDs[2].GetComponent<OpponentHUD>().setPlayer(player4);
            }
            else
            {
                opponentHUDs[2].gameObject.SetActive(false);
            }
            opponentHUDs[1].GetComponent<OpponentHUD>().setPlayer(player3);
        }
        else
        {
            opponentHUDs[1].gameObject.SetActive(false);
            opponentHUDs[2].gameObject.SetActive(false);
        }
        opponentHUDs[0].GetComponent<OpponentHUD>().setPlayer(player2);
        playerHUD.GetComponent<PlayerHUD>().setPlayer(player1);
    }

    #endregion

    #region Button Action Listener Methods

    /*  Addmethods for all buttons
     *  
     public void driveEvent(){
     
     }

     public void endTurnEvent(){
     
     }
         
     ... */

    public void saveGameEvent()
    {
        Debug.Log("saveGameEvent()");
        string fileName = filenameInput == null ? "savefile" : filenameInput.text;
        //		if (!EventTransferManager.instance.waitingForPlayer) {
        EventTransferManager.instance.saveFile(fileName);
        //		}
        cancelSaveGamePanel();
    }

    public void cancelSaveGamePanel()
    {
        savePanel.SetActive(false);
    }

    public IEnumerator ShowNotification(string message, params int[] players)
    {
        if (players.Contains(PhotonNetwork.player.ID - 1))
        {
            notificationPanel.gameObject.SetActive(true);
        }
        yield return StartCoroutine(notificationPanel.ShowNotification(message, players));
    }

    #endregion
}