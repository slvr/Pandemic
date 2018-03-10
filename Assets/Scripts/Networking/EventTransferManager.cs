using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class EventTransferManager : Photon.MonoBehaviour {

    public static EventTransferManager instance = null;

    /**
     * DECLARE GAMEOBJECT ATTRIBUTES
     * */
    public GameObject boardPrefab;
    public GameObject settingsPrefab;
    public GameObject pandemicPlayersPrefab;
    public GameObject pandemicCanvasPrefab;
    public GameObject playerCardsManagerPrefab;
    public GameObject infectionCardsManagerPrefab;

    /**
     * DECLARE TURN ATTRIBUTES
     * */
    public int currentPlayerTurn = 0;
    public int currentActionsRemaining = 4;
    public bool setupPhase = true;
    public bool waitingForPlayer = false;

    /**
     * DECLARE PERSISTENCE ATTRIBUTES
     * */
    public byte outbreakCounter = 0;
    public byte infectionRate = 0;
    public byte numResearchStations = 0;
    public byte redRem = 24;
    public byte blueRem = 24;
    public byte yellowRem = 24;
    public byte blackRem = 24;
    public byte purpleRem = 12;
    public bool yellowCured = false;
    public bool blueCured = false;
    public bool redCured = false;
    public bool blackCured = false;
    public bool purpleCured = false;
    public bool isGovernmentInterferenceActive = false;
    public bool isSlipperySlopeActive = false;
    public bool isRateEffectActive = false;
    public bool isChronicEffectActive = false;
    public bool isComplexMolecularStructurActive = false;
    public short turnCommercialTravelBanActivated = -1;

    /**
     * DECLARE SETUP ATTRIBUTES
     * */
    public bool startedSetupPhase = false;
    public bool waitingForPlayers = false;
    public bool[] playerChecks;

    void Awake(){
        if (instance == null) {
            instance = this;
        }
        playerChecks = new bool[PhotonNetwork.playerList.Length];
    }

    // Update is called once per frame
    void Update () {
		
	}

    /**
     * METHODS FOR GENERAL USE AND CONNECTION
     * */
    public void OnReadyToPlay() {
        if (GameManager.instance.LoadGameMode == true){
            GetComponent<PhotonView>().RPC("GenerateBoardForClientFromSavedGame", PhotonTargets.All, new object[] { });
            GetComponent<PhotonView>().RPC("GenerateInfectionCardsForClientFromSavedGame", PhotonTargets.All, new object[] { });
            GetComponent<PhotonView>().RPC("GeneratePlayerCardsForClientFromSavedGame", PhotonTargets.All, new object[] { });
            GetComponent<PhotonView>().RPC("GeneratePandemicManager", PhotonTargets.All, new object[] { });
        }
        else {
            GetComponent<PhotonView>().RPC("GenerateBoardForClient", PhotonTargets.All, new object[] { });
            GetComponent<PhotonView>().RPC("GenerateInfectionCardsForClient", PhotonTargets.All, new object[] { });
            GetComponent<PhotonView>().RPC("GeneratePlayerCardsForClient", PhotonTargets.All, new object[] { });
        }
    }
    [PunRPC]
    void GenerateBoardForClientFromSavedGame() {
        GameObject clientBoardGO = Instantiate(boardPrefab);
        GameBoard clientBoard = clientBoardGO.GetComponent<GameBoard>();
        Persistence.pe_board pe_board = GameManager.instance.pe_board;
        /* Deserialize board settings */
    }
    [PunRPC]
    void GenerateInfectionCardsForClientFromSavedGame(){
        Destroy(GameObject.FindGameObjectWithTag("InfectionCardStackManager"));
        GameObject infectionCardsStackGO = Instantiate(infectionCardsManagerPrefab);
        InfectionCardStackManager clientInfectionCards = infectionCardsStackGO.GetComponent<InfectionCardStackManager>();
        Persistence.pe_infectionCardStack pe_infectionCardStack = GameManager.instance.pe_infectionCardStack;
    }
    [PunRPC]
    void GeneratePlayerCardsForClientFromSavedGame(){
        Destroy(GameObject.FindGameObjectWithTag("PlayerCardStackManager"));
        GameObject playerCardsStackGO = Instantiate(playerCardsManagerPrefab);
        PlayerCardStackManager clientPlayerCards = playerCardsStackGO.GetComponent<PlayerCardStackManager>();
        Persistence.pe_playerCardStack pe_playerCardStack = GameManager.instance.pe_playerCardStack;
       // clientPlayerCards.loadCardState(/*  Load each player's cards  */);
    }
    [PunRPC]
    IEnumerator GeneratePandemicManager(){
        yield return new WaitForSeconds(3f);
        PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
        Persistence.pe_ETM pe_etm = GameManager.instance.pe_eventTransferManager;
        GetComponent<PhotonView>().RPC("SetPlayerTurn", PhotonTargets.Others, new object[] { pe_etm.currentPlayerTurn });
        this.currentActionsRemaining = pe_etm.currentActionsRemaining;
        this.setupPhase = pe_etm.setupPhase;
        this.waitingForPlayer = pe_etm.waitingForPlayer;
        clientPandemicManager.currentPlayerTurn = pe_etm.currentPlayerTurn;
        clientPandemicManager.EnableSave();
    }
    [PunRPC]
    void SetPlayerTurn(int currentTurnPlayer) {
        PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
        clientPandemicManager.SetCurrentTurn(currentTurnPlayer);
        EventTransferManager.instance.currentPlayerTurn = currentTurnPlayer;
    }
    [PunRPC]
    void GenerateBoardForClient(){
        GameObject clientBoardGO = Instantiate(boardPrefab);
        GameBoard clientBoard = clientBoardGO.GetComponent<GameBoard>();

        clientBoard.GenerateCities(clientBoard.transform.GetChild(0));
        clientBoard.GenerateConnections(clientBoard.transform.GetChild(1));
    }
    [PunRPC]
    public IEnumerator GenerateInfectionCardsForClient(){
        Destroy(GameObject.FindGameObjectWithTag("InfectionCardStackManager"));
        GameObject infectionCardsStackGO = Instantiate(infectionCardsManagerPrefab);
        InfectionCardStackManager clientInfectionCards = infectionCardsStackGO.GetComponent<InfectionCardStackManager>();
        if (PhotonNetwork.isMasterClient) {
            yield return new WaitForSeconds(5f);
            clientInfectionCards.shuffleCards();
            GetComponent<PhotonView>().RPC("GenerateInfectionCardDeque", PhotonTargets.All, new object[] { });
        }
    }
    [PunRPC]
    void GenerateInfectionCardDeque(){
        InfectionCardStackManager clientInfectionCards = GameObject.FindGameObjectsWithTag("InfectionCardStackManager").GetComponent<InfectionCardStackManager>();
        clientInfectionCards.infectionCardDeque.Clear();
        clientInfectionCards.generateDeque();
    }
    [PunRPC]
    public IEnumerator GeneratePlayerCardsForClient(){
        Destroy(GameObject.FindGameObjectWithTag("PlayerCardStackManager"));
        GameObject playerCardsStackGO = Instantiate(playerCardsManagerPrefab);
        PlayerCardStackManager clientPlayerCards = playerCardsStackGO.GetComponent<PlayerCardStackManager>();
        if (PhotonNetwork.isMasterClient){
            yield return new WaitForSeconds(5f);
            clientPlayerCards.shuffleCards();
            GetComponent<PhotonView>().RPC("GeneratePlayerCardQueue", PhotonTargets.All, new object[] { });
        }
    }
    [PunRPC]
    void GeneratePlayerCardQueue(){
        PlayerCardStackManager clientPlayerCards = GameObject.FindGameObjectWithTag("PlayerCardStackManager").GetComponent<PlayerCardStackManager>();
        clientPlayerCards.playerCardQueue.Clear();
        clientPlayerCards.generateQueue();
    }

    public void OnEndTurn() {
        GetComponent<PhotonView>().RPC("EndTurn", PhotonTargets.All, new object[] { });
        currentPlayerTurn = (currentPlayerTurn + 1) % PhotonNetwork.playerList.Length;
        currentActionsRemaining = 4;
    }
    [PunRPC]
    void EndTurn() {
        PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
        /*  draw infection cards, infect cities  */
    }

    public void OnEndGame() {
        GetComponent<PhotonView>().RPC("GameOver", PhotonTargets.All, new object[] { });
    }
    [PunRPC]
    void GameOver() {
        EventTransferManager.instance.waitingForPlayer = true;
        EventTransferManager.instance.waitingForPlayers = true;
    }

    public void OnOperationFailure() {
        GetComponent<PhotonView>().RPC("HandleOperationFailure", PhotonTargets.All, new object[] { });
    }
    [PunRPC]
    void HandleOperationFailure() {
        EventTransferManager.instance.waitingForPlayer = false;
        EventTransferManager.instance.waitingForPlayers = false;
    }

    /**
     * METHODS FOR GENERAL TURN ACTIONS
     * */

    public IEnumerator onDrive(int senderPlayer, City dest) {
        GetComponent<PhotonView>().RPC("MoveSelfPawn", PhotonTargets.All, new object[] { senderPlayer, dest });
        currentActionsRemaining -= 1;
        while (EventTransferManager.instance.waitingForPlayer) {
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator onDirectFlight(int senderPlayer, City dest, CityCard cc) {
        CityCard[] ccs = new CityCard[1];
        ccs[0] = cc;
        GetComponent<PhotonView>().RPC("discardFromPlayerHand", PhotonTargets.All, new object[] { senderPlayer, ccs });
        GetComponent<PhotonView>().RPC("MoveSelfPawn", PhotonTargets.All, new object[] { senderPlayer, dest });
        currentActionsRemaining -= 1;
        while (EventTransferManager.instance.waitingForPlayer){
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator onCharterFlight(int senderPlayer, City dest, CityCard cc) {
        CityCard[] ccs = new CityCard[1];
        ccs[0] = cc;
        GetComponent<PhotonView>().RPC("discardFromPlayerHand", PhotonTargets.All, new object[] { senderPlayer, ccs });
        GetComponent<PhotonView>().RPC("MoveSelfPawn", PhotonTargets.All, new object[] { senderPlayer, dest });
        currentActionsRemaining -= 1;
        while (EventTransferManager.instance.waitingForPlayer){
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator onShuttleFlight(int senderPlayer, City dest) {
        GetComponent<PhotonView>().RPC("MoveSelfPawn", PhotonTargets.All, new object[] { senderPlayer, dest });
        currentActionsRemaining -= 1;
        while (EventTransferManager.instance.waitingForPlayer){
            yield return new WaitForEndOfFrame();
        }
    }
    [PunRPC]
    void MoveSelfPawn(int sender, City dest) {
        PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
        StartCoroutine(clientPandemicManager.movePawn(sender, dest));
    }
    [PunRPC]
    void discardFromPlayerHand(int sender, CityCard[] ccs) {
        PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
        StartCoroutine(clientPandemicManager.discardFromHand(sender, ccs));
    }

    public IEnumerator onBuildResearchStation(int senderPlayer, CityCard cc) {
        City c = cc.getCity();
        GetComponent<PhotonView>().RPC("discardFromPlayerHand", PhotonTargets.All, new object[] { senderPlayer, cc });
        GetComponent<PhotonView>().RPC("BuildResearchStation", PhotonTargets.All, new object[] { c });
        currentActionsRemaining -= 1;
        numResearchStations += 1;
        while (EventTransferManager.instance.waitingForPlayer){
            yield return new WaitForEndOfFrame();
        }
    }
    [PunRPC]
    void BuildResearchStation(City c) {
        PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
        StartCoroutine(clientPandemicManager.buildResearchStation(c));
    }

    public IEnumerator onTreatDisease(int senderPlayer, Colour c) {
        City city = senderPlayer.getLocation();
        GetComponent<PhotonView>().RPC("treatDisease", PhotonTargets.All, new object[] { city, c });
        currentActionsRemaining -= 1;
        switch ((Colour)c) {
            case Colour.blue:
                blueRem += 1;
                break;
            case Colour.black:
                blackRem += 1;
                break;
            case Colour.purple:
                purpleRem += 1;
                break;
            case Colour.red:
                redRem += 1;
                break;
            case Colour.yellow:
                yellowRem += 1;
                break;
        }
        while (EventTransferManager.instance.waitingForPlayer){
            yield return new WaitForEndOfFrame();
        }
    }
    [PunRPC]
    void treatDisease(City city, Colour c) {
        PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
        StartCoroutine(clientPandemicManager.treatDisease(city, c));
    }

    public void onGiveKnowledgeOffer(CityCard cc, int senderNumber, int receiverNumber) {
        Debug.Log(LevelManager.instance.players[senderNumber].playerName + " wants to give the " + cc.name + " card to " + LevelManager.instance.players[receiverNumber].playerName);
        StartCoroutine(giveOffer(senderNumber, receiverNumber, cc));
    }
    public void onGiveKnowledgeResponse(bool active) {
        GetComponent<PhotonView>().RPC("GiveKnowledgePanelActivation", PhotonTargets.All, new object[] { active });
    }
    public void onGiveKnowledgeEnd(int to, bool result) {
        Debug.Log("Sending notification to: " + LevelManager.instance.players[to].playerName);
        GetComponent<PhotonView>().RPC("SignalGiveKnowledgeEnd", PhotonTargets.All, new object[] { to, result });
    }
    public IEnumerator giveOffer(int senderNumber, int receiverNumber, CityCard cc) {
        Debug.Log("Sending offer to: " + LevelManager.instance.players[receiverNumber].playerName);
        GetComponent<PhotonView>().RPC("SignalGiveKnowledge", PhotonTargets.All, new object[] { senderNumber, receiverNumber, cc });
        int[] waitingForPlayer = new int[1];
        waitingForPlayer[0] = receiverNumber;
        onWaitForPlayers(waitingForPlayer);
        for (int i = 0; i < playerChecks.Length; i++) {
            Debug.Log("playerChecks[" + i +"] = " + playerChecks[i]);
        }
        while (EventTransferManager.instance.waitingForPlayers) {
            Debug.Log("Waiting...");
            CheckIfPlayersReady();
            yield return new WaitForEndOfFrame();
        }
    }
    [PunRPC]
    void SignalGiveKnowledge(int senderNumber, int receiverNumber, CityCard cc) {
        Debug.Log("Receiving the notification: " + LevelManager.instance.players[receiverNumber].playerName);
        if (PhotonNetwork.player.ID - 1 == receiverNumber){
            PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
            clientPandemicManager.uiManager.giveKnowledgePlayerPanel.waiting.gameObject.SetActive(false);
            clientPandemicManager.uiManager.giveKnowledgePlayerPanel.OpenRespond(clientPandemicManager.players[senderNumber], cc);
        }
    }
    [PunRPC]
    void GiveKnowledgePanelActivation(bool active) {
        PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
        GiveKnowledgePlayerPanel panel = clientPandemicManager.uiManager.giveKnowledgePlayerPanel;

        panel.accept.onClick.RemoveAllListeners();
        panel.refuse.onClick.RemoveAllListeners();

        clientPandemicManager.uiManager.giveKnowledgePlayerPanel.gameObject.SetActive(false);
    }
    [PunRPC]
    void SignalGiveKnowledgeEnd(int to, bool result) {
        if (PhotonNetwork.player.ID - 1 == to){
            Debug.Log("Give knowledge: " + LevelManager.instance.players[to].playerName + " notified");
            PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
            clientPandemicManager.uiManager.giveKnowledgePlayerPanel.notification.gameObject.SetActive(true);
            if (result){
                clientPandemicManager.uiManage.giveKnowledgePlayerPanel.notificationText.text = "Accepted";
                currentActionsRemaining -= 1;
            }
            else{
                clientPandemicManager.uiManage.giveKnowledgePlayerPanel.notificationText.text = "Rejected";
            }
        }
        GetComponent<PhotonView>().RPC("GiveKnowledgePanelActivation", PhotonTargets.All, new object[] { false });
    }

    public void onReceiveKnowledgeOffer(CityCard cc, int senderNumber, int receiverNumber) {
        Debug.Log(LevelManager.instance.players[receiverNumber].playerName + " want to receive the " + cc.name + " card from " + LevelManager.instance.players[senderNumber].playerName);
        StartCoroutine(receiveOffer(senderNumber, receiverNumber, cc));
    }
    public void onReceiveKnowledgeResponse(bool active) {
        GetComponent<PhotonView>().RPC("ReceiveKnowledgePanelActivation", PhotonTargets.All, new object[] { active });
    }
    public void onReceiveKnowledgeEnd(int to, bool result) {
        Debug.Log("Sending notification to: " + LevelManager.instance.players[to].playerName);
        GetComponent<PhotonView>().RPC("SignalReceiveKnowledgeEnd", PhotonTargets.All, new object[] { to, result });
    }
    public IEnumerator receiveOffer(int senderNumber, int receiverNumber, CityCard cc) {
        Debug.Log("Sending offer to: " + LevelManager.instance.players[receiverNumber].playerName);
        GetComponent<PhotonView>().RPC("SignalReceiveKnowledge", PhotonTargets.All, new object[] { senderNumber, receiverNumber, cc });
        int[] waitingForPlayer = new int[1];
        waitingForPlayer[0] = receiverNumber;
        onWaitForPlayers(waitingForPlayer);
        for (int i = 0; i < playerChecks.Length; i++){
            Debug.Log("playerChecks[" + i + "] = " + playerChecks[i]);
        }
        while (EventTransferManager.instance.waitingForPlayers){
            Debug.Log("Waiting...");
            CheckIfPlayersReady();
            yield return new WaitForEndOfFrame();
        }
    }
    [PunRPC]
    void SignalReceiveKnowledge(int senderNumber, int receiverNumber, CityCard cc){
        Debug.Log("Receiving the notification: " + LevelManager.instance.players[receiverNumber].playerName);
        if (PhotonNetwork.player.ID - 1 == receiverNumber) {
            PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
            clientPandemicManager.uiManager.receiveKnowledgePlayerPanel.waiting.gameObject.SetActive(false);
            clientPandemicManager.uiManager.receiveKnowledgePlayerPanel.OpenRespond(clientPandemicManager.players[senderNumber], cc);
        }
    }
    [PunRPC]
    void ReceiveKnowledgePanelActivation(bool active) {
        PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
        ReceiveKnowledgePlayerPanel panel = clientPandemicManager.uiManager.receiveKnowledgePlayerPanel;

        panel.accept.onClick.RemoveAllListeners();
        panel.refuse.onClick.RemoveAllListeners();

        clientPandemicManager.uiManager.receiveKnowledgePlayerPanel.gameObject.SetActive(false);
    }
    [PunRPC]
    void SignalReceiveKnowledgeEnd(int to, bool result) {
        if (PhotonNetwork.player.ID - 1 == to) {
            Debug.Log("Receive knowledge: " + LevelManager.instance.players[to].playerName + " notified");
            PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
            clientPandemicManager.uiManager.receiveKnowledgePlayerPanel.notification.gameObject.SetActive(true);
            if (result){
                clientPandemicManager.uiManage.receiveKnowledgePlayerPanel.notificationText.text = "Accepted";
                currentActionsRemaining -= 1;
            }
            else {
                clientPandemicManager.uiManage.receiveKnowledgePlayerPanel.notificationText.text = "Rejected";
            }
        }
        GetComponent<PhotonView>().RPC("ReceiveKnowledgePanelActivation", PhotonTargets.All, new object[] { false });
    }

    void CheckIfPlayersReady() {
        bool ready = true;
        for (int i = 0; i < playerChecks.Length; i++) {
            if (!playerChecks[i]) {
                ready = false;
            }
        }
        EventTransferManager.instance.waitingForPlayer = !ready;
        EventTransferManager.instance.waitingForPlayers = !ready;
    }
    void onWaitForPlayers(int[] playerNums) {
        GetComponent<PhotonView>().RPC("BeginWaitForPlayers", PhotonTargets.All, new object[] { playerNums });
    }
    [PunRPC]
    void BeginWaitForPlayers(int[] playerNums) {
        for (int i = 0; i < playerChecks.Length; i++) {
            EventTransferManager.instance.playerChecks[i] = true;
        }
        for (int i = 0; i < playerChecks.Length; i++) {
            if (playerNums[i] < playerChecks.Length) {
                EventTransferManager.instance.playerChecks[playerNums[i]] = false;
            }
        }
        EventTransferManager.instance.waitingForPlayers = true;
    }

    public IEnumerator onDiscoverCure(int senderPlayer, Colour c, CityCard[] ccs) {
        GetComponent<PhotonView>().RPC("discardFromPlayerHand", PhotonTargets.All, new object[] { senderPlayer, ccs });
        GetComponent<PhotonView>().RPC("discoverCure", PhotonTargets.All, new object[] { senderPlayer, c });
        currentActionsRemaining -= 1;
        switch (c) {
            case Colour.blue:
                blueCured = true;
                break;
            case Colour.black:
                blackCured = true;
                break;
            case Colour.purple:
                purpleCured = true;
                break;
            case Colour.red:
                redCured = true;
                break;
            case Colour.yellow:
                yellowCured = true;
                break;
        }
        while (EventTransferManager.instance.waitingForPlayer){
            yield return new WaitForEndOfFrame();
        }
    }
    [PunRPC]
    void discoverCure(int sender, Colour c) {
        PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
        StartCoroutine(clientPandemicManager.discoverCure(sender, c));
    }

    /**
     * METHODS FOR SPECIALIZED TURN ACTIONS
     * */

    public void onOEBuildResearchStation() {

    }

    public void onMedicTreatDisease(Colour c) {

    }

    public void onScientistDiscoverCure(Colour c, CityCard[] ccs) {

    }

    public void onTroubleShooterCheckICs() {

    }

    public void onTroubleShooterDirectFlight(City dest) {

    }

    public void onDispatcherDrive(City dest, Player p){

    }

    public void onDispatcherDirectFlight(City dest, Player p){

    }

    public void onDispatcherCharterFlight(City dest, Player p){

    }

    public void onDispatcherShuttleFlight(City dest, Player p){

    }

    public void onDispatcherMoveToOtherPawn(Player moving, Player target) {

    }

    public void onEpidemiologistReceiveKnowledge(CityCard cc, Player sender) {

    }

    public void onGeneraliststartTurn() {

    }

    public void onContingencyPlannerRetrieveEventCard(EventCard e) {

    }

    public void onFieldOperativeRetriveDisease(Colour c) {

    }

    public void onFieldOperativeDiscoverCure(Colour c) {

    }

    public void onArchivistRetriveCityCard() {

    }

    public void onBioTerroristStartTurn() {

    }

    public void onBioTerroristDrive(City dest) {

    }

    public void onBioTerroristDirectFlight(City dest) {

    }

    public void onBioTerroristCharterFlight(City dest) {

    }

    public void onCarptureBioTerrorist() {

    }

    public void onBioTerroristEscape(City dest) {

    }

    public void onBioTerroristDrawCard() {

    }

    public void onBioTerroristInfectLocally() {

    }

    public void onBioTerroristInfectRemotely(City dest) {

    }

    public void onBioTerroristSabotage(InfectionCard discard) {

    }

    /**
     * METHODS FOR END OF TURN EVENTS
     * */

    public IEnumerator onDrawPlayerCard(int senderPlayer) {
        GetComponent<PhotonView>().RPC("drawPlayerCard", PhotonTargets.All, new object[] { senderPlayer });
        while (EventTransferManager.instance.waitingForPlayer){
            yield return new WaitForEndOfFrame();
        }
    }
    [PunRPC]
    void drawPlayerCard(int sender){
        PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
        StartCoroutine(clientPandemicManager.drawPlayerCard(sender));
    }

    public IEnumerator onBasicEpidemic() {
        GetComponent<PhotonView>().RPC("basicEpidemic", PhotonTargets.All, new object[] {  });
        infectionRate += 1;
        while (EventTransferManager.instance.waitingForPlayer){
            yield return new WaitForEndOfFrame();
        }
    }
    [PunRPC]
    void basicEpidemic() {
        PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
        StartCoroutine(clientPandemicManager.basicEpidemic());
    }

    public IEnumerator onInfectNextCity() {
        GetComponent<PhotonView>().RPC("infectNextCity", PhotonTargets.All, new object[] {  });
        while (EventTransferManager.instance.waitingForPlayer) {
            yield return new WaitForEndOfFrame();
        }
    }
    [PunRPC]
    void infectNextCity() {
        PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
        StartCoroutine(clientPandemicManager.infectNextCity());
    }

    public IEnumerator onOutbreak() {
        outbreakCounter += 1;
        while (EventTransferManager.instance.waitingForPlayer){
            yield return new WaitForEndOfFrame();
        }
    }

    public void onUncountedPopulationsEpidemic() {

    }

    public void onUnacceptableLossEpidemic() {

    }

    public void onHiddenPocketEpidemic() {

    }

    public void onComplexMolecularStructureEpidemic() {

    }

    public void onChronicEffectEpidemic() {

    }

    public void onRateEffectEpidemic() {

    }

    public void onSlipperySlopeEpidemic() {

    }

    public void onGovernmentInterferenceEpidemic() {

    }

    public void onMutationThreatens() {

    }

    public void onMutationIntensifies() {

    }

    public void onMutationSpreads() {

    }

    public void onMutationCardDrawn() {

    }

    /**
     * METHODS FOR EVENT CARDS
     * */

    public void onCommercialTravelBan() {

    }

    public void onGovernmentGrant(City c) {

    }

    public void onOneQuietNight() {

    }

    public void onForecast() {

    }

    public void onResilientPopulation(CityCard cc) {

    }

    public void onAirlift(City dest, Player target) {

    }

    public void onRapidVaccineDeployment() {

    }

    public void onRemoteTreatment(City c1, Colour co1, City c2, Colour co2) {

    }

    public void onBorrowedTime() {

    }

    public void onReexaminedResearch(Player target, CityCard cc) { //NOT OK

    }

    public void onSpecialOrders(Player target) {

    }

    public void onNewAssignment(Player target) {

    }

    public void onMobileHospital() {

    }

    /**
     * MISC METHODS
     * */

    public void onHighlightConnection(int senderPlayer, bool turnOn, Connection c) {
        GetComponent<PhotonView>().RPC("highlightConnection", PhotonTargets.All, new object[] { senderPlayer, turnOn, c });
    }
    [PunRPC]
    void highlightConnection(int senderPlayer, bool turnOn, Connection c) {
        PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
        StartCoroutine(clientPandemicManager.highlightConnection(senderPlayer, turnOn, c));
    }

    public void onHighlightCity(int senderPlayer, bool turnOn, City c) {
        GetComponent<PhotonView>().RPC("highlightCity", PhotonTargets.All, new object[] { senderPlayer, turnOn, c });
    }
    [PunRPC]
    void highlightCity(int senderPlayer, bool turnOn, City c) {
        PandemicManager clientPandemicManager = GameObject.FindGameObjectWithTag("PandemicManager").GetComponent<PandemicManager>();
        StartCoroutine(clientPandemicManager.highlightCity(senderPlayer, turnOn, c));
    }

}

public enum MoveType {
    Drive,
    DirectFlight,
    CharterFlight,
    ShuttleFlight,
    BuildResearchStation,
    TreatDisease,
    GiveKnowledge,
    ReceiveKnowledge,
    DiscoverCure
}

public enum SpecialMoveType {

}


