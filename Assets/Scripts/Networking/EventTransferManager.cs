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
            GetComponent<PhotonView>().RPC("GenerateBoard", PhotonTargets.All, new object[] { });
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
        Board clientBoard = clientBoardGO.GetComponent<Board>();
        Persistence.pe_board pe_board = GameManager.instance.pe_board;
        /* Deserialize board settings */
    }
    [PunRPC]
    void GenerateInfectionCardsForClientFromSavedGame(){
        Destroy(GameObject.FindGameObjectWithTag("InfectionCardStackManager"));
        GameObject infectionCardsStackGO = Instantiate(infectionCardsManagerPrefab);
        CardHub clientInfectionCards = infectionCardsStackGO.GetComponent<CardHub>();
        Persistence.pe_infectionCardStack pe_infectionCardStack = GameManager.instance.pe_infectionCardStack;
    }
    [PunRPC]
    void GeneratePlayerCardsForClientFromSavedGame(){
        Destroy(GameObject.FindGameObjectWithTag("PlayerCardStackManager"));
        GameObject playerCardsStackGO = Instantiate(playerCardsManagerPrefab);
        CardHub clientPlayerCards = playerCardsStackGO.GetComponent<CardHub>();
        Persistence.pe_playerCardStack pe_playerCardStack = GameManager.instance.pe_playerCardStack;
       // clientPlayerCards.loadCardState(/*  Load each player's cards  */);
    }
    [PunRPC]
    IEnumerator GenerateBoard(){
        yield return new WaitForSeconds(3f);
        Board clientBoard = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        Persistence.pe_ETM pe_etm = GameManager.instance.pe_eventTransferManager;
        GetComponent<PhotonView>().RPC("SetPlayerTurn", PhotonTargets.Others, new object[] { pe_etm.currentPlayerTurn });
        this.currentActionsRemaining = pe_etm.currentActionsRemaining;
        this.setupPhase = pe_etm.setupPhase;
        this.waitingForPlayer = pe_etm.waitingForPlayer;
        clientBoard.currentPlayerTurn = pe_etm.currentPlayerTurn;
        clientBoard.EnableSave();
    }
    [PunRPC]
    void SetPlayerTurn(int currentTurnPlayer) {
        Board clientBoard = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        clientBoard.SetCurrentTurn(currentTurnPlayer);
        EventTransferManager.instance.currentPlayerTurn = currentTurnPlayer;
    }
    [PunRPC]
    void GenerateBoardForClient(){
        GameObject clientBoardGO = Instantiate(boardPrefab);
        Board clientBoard = clientBoardGO.GetComponent<Board>();

        clientBoard.GenerateCities();
        clientBoard.GenerateConnections();
    }
    [PunRPC]
    public IEnumerator GenerateInfectionCardsForClient(){
        GameObject infectionCardsStackGO = Instantiate(infectionCardsManagerPrefab);
        CardHub clientInfectionCards = infectionCardsStackGO.GetComponent<CardHub>();
        if (PhotonNetwork.isMasterClient) {
            yield return new WaitForSeconds(5f);
            GetComponent<PhotonView>().RPC("GenerateInfectionCardDeque", PhotonTargets.All, new object[] { });
        }
    }
    [PunRPC]
    void GenerateInfectionCardDeque(){
        CardHub clientInfectionCards = GameObject.FindGameObjectWithTag("CardHub").GetComponent<CardHub>();
        clientInfectionCards.aInfectionDeck.Clear();
        clientInfectionCards.initializeInfectionDeck();
    }
    [PunRPC]
    public IEnumerator GeneratePlayerCardsForClient(){
        Destroy(GameObject.FindGameObjectWithTag("PlayerCardStackManager"));
        GameObject playerCardsStackGO = Instantiate(playerCardsManagerPrefab);
        CardHub clientPlayerCards = playerCardsStackGO.GetComponent<CardHub>();
        if (PhotonNetwork.isMasterClient){
            yield return new WaitForSeconds(5f);
            GetComponent<PhotonView>().RPC("GeneratePlayerCardQueue", PhotonTargets.All, new object[] { });
        }
    }
    [PunRPC]
    void GeneratePlayerCardQueue(){
        CardHub clientPlayerCards = GameObject.FindGameObjectWithTag("CardHub").GetComponent<CardHub>();
        clientPlayerCards.aPlayerDeck.Clear();
        clientPlayerCards.initializePlayerDeck();
    }

    public void OnEndTurn() {
        GetComponent<PhotonView>().RPC("EndTurn", PhotonTargets.All, new object[] { });
        currentPlayerTurn = (currentPlayerTurn + 1) % PhotonNetwork.playerList.Length;
        currentActionsRemaining = 4;
    }
    [PunRPC]
    void EndTurn() {
        Board clientBoard = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
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

    public void saveFile(string fileName){
        GetComponent<PhotonView>().RPC("saveFileEvent", PhotonTargets.All, new object[] {
            fileName
        });
    }
    [PunRPC]
    private void saveFileEvent(string fileName){
        SaveJson.saveJson(fileName);
    }

    /**
     * METHODS FOR GENERAL TURN ACTIONS
     * */

    public IEnumerator onDrive(int senderPlayer, CityNode dest) {
        GetComponent<PhotonView>().RPC("MoveSelfPawn", PhotonTargets.All, new object[] { senderPlayer, dest });
        currentActionsRemaining -= 1;
        while (EventTransferManager.instance.waitingForPlayer) {
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator onDirectFlight(int senderPlayer, CityNode dest, CityCard cc) {
        CityCard[] ccs = new CityCard[1];
        ccs[0] = cc;
        GetComponent<PhotonView>().RPC("discardFromPlayerHand", PhotonTargets.All, new object[] { senderPlayer, ccs });
        GetComponent<PhotonView>().RPC("MoveSelfPawn", PhotonTargets.All, new object[] { senderPlayer, dest });
        currentActionsRemaining -= 1;
        while (EventTransferManager.instance.waitingForPlayer){
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator onCharterFlight(int senderPlayer, CityNode dest, CityCard cc) {
        CityCard[] ccs = new CityCard[1];
        ccs[0] = cc;
        GetComponent<PhotonView>().RPC("discardFromPlayerHand", PhotonTargets.All, new object[] { senderPlayer, ccs });
        GetComponent<PhotonView>().RPC("MoveSelfPawn", PhotonTargets.All, new object[] { senderPlayer, dest });
        currentActionsRemaining -= 1;
        while (EventTransferManager.instance.waitingForPlayer){
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator onShuttleFlight(int senderPlayer, CityNode dest) {
        GetComponent<PhotonView>().RPC("MoveSelfPawn", PhotonTargets.All, new object[] { senderPlayer, dest });
        currentActionsRemaining -= 1;
        while (EventTransferManager.instance.waitingForPlayer){
            yield return new WaitForEndOfFrame();
        }
    }
    [PunRPC]
    void MoveSelfPawn(int sender, CityNode dest) {
        Board clientBoard = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        StartCoroutine(clientBoard.drive(sender, dest));
    }
    [PunRPC]
    void discardFromPlayerHand(int sender, CityCard[] ccs) {
        Board clientBoard = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        StartCoroutine(clientBoard.discardFromHand(sender, ccs));
    }

    public IEnumerator onBuildResearchStation(int senderPlayer, CityCard cc) {
        GameObject c = cc.getCityNode();
        GetComponent<PhotonView>().RPC("discardFromPlayerHand", PhotonTargets.All, new object[] { senderPlayer, cc });
        GetComponent<PhotonView>().RPC("BuildResearchStation", PhotonTargets.All, new object[] { c });
        currentActionsRemaining -= 1;
        numResearchStations += 1;
        while (EventTransferManager.instance.waitingForPlayer){
            yield return new WaitForEndOfFrame();
        }
    }
    [PunRPC]
    void BuildResearchStation(CityNode c) {
        Board clientBoard = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        StartCoroutine(clientBoard.buildResearchStation(c));
    }

    public IEnumerator onTreatDisease(int senderPlayer, Colour c) {
        CityNode city = LevelManager.instance.players[senderPlayer].getLocation();
        GetComponent<PhotonView>().RPC("treatDisease", PhotonTargets.All, new object[] { city, c });
        currentActionsRemaining -= 1;
        switch ((Colour)c) {
            case Colour.BLUE:
                blueRem += 1;
                break;
            case Colour.BLACK:
                blackRem += 1;
                break;
            case Colour.PURPLE:
                purpleRem += 1;
                break;
            case Colour.RED:
                redRem += 1;
                break;
            case Colour.YELLOW:
                yellowRem += 1;
                break;
        }
        while (EventTransferManager.instance.waitingForPlayer){
            yield return new WaitForEndOfFrame();
        }
    }
    [PunRPC]
    void treatDisease(CityNode city, Colour c) {
        GameEngine clientBoard = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
        StartCoroutine(clientBoard.treatDisease(city, c));
    }

    public void onGiveKnowledgeOffer(CityCard cc, int senderNumber, int receiverNumber) {
        Debug.Log(LevelManager.instance.players[senderNumber].aPlayerName + " wants to give the " + cc.name + " card to " + LevelManager.instance.players[receiverNumber].aPlayerName);
        StartCoroutine(giveOffer(senderNumber, receiverNumber, cc));
    }
    public void onGiveKnowledgeResponse(bool active) {
        GetComponent<PhotonView>().RPC("GiveKnowledgePanelActivation", PhotonTargets.All, new object[] { active });
    }
    public void onGiveKnowledgeEnd(int to, bool result) {
        Debug.Log("Sending notification to: " + LevelManager.instance.players[to].aPlayerName);
        GetComponent<PhotonView>().RPC("SignalGiveKnowledgeEnd", PhotonTargets.All, new object[] { to, result });
    }
    public IEnumerator giveOffer(int senderNumber, int receiverNumber, CityCard cc) {
        Debug.Log("Sending offer to: " + LevelManager.instance.players[receiverNumber].aPlayerName);
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
        Debug.Log("Receiving the notification: " + LevelManager.instance.players[receiverNumber].aPlayerName);
        if (PhotonNetwork.player.ID - 1 == receiverNumber){
            GameEngine clientBoard = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
            clientBoard.uiManager.giveKnowledgePlayerPanel.waiting.gameObject.SetActive(false);
            clientBoard.uiManager.giveKnowledgePlayerPanel.OpenRespond(clientBoard.players[senderNumber], cc);
        }
    }
    [PunRPC]
    void GiveKnowledgePanelActivation(bool active) {
        GameEngine clientBoard = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
        GiveKnowledgePlayerPanel panel = clientBoard.uiManager.giveKnowledgePlayerPanel;

        panel.accept.onClick.RemoveAllListeners();
        panel.refuse.onClick.RemoveAllListeners();

        clientBoard.uiManager.giveKnowledgePlayerPanel.gameObject.SetActive(false);
    }
    [PunRPC]
    void SignalGiveKnowledgeEnd(int to, bool result) {
        if (PhotonNetwork.player.ID - 1 == to){
            Debug.Log("Give knowledge: " + LevelManager.instance.players[to].aPlayerName + " notified");
            GameEngine clientBoard = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
            clientBoard.uiManager.giveKnowledgePlayerPanel.notification.gameObject.SetActive(true);
            if (result){
                clientBoard.uiManager.giveKnowledgePlayerPanel.notificationText.text = "Accepted";
                currentActionsRemaining -= 1;
            }
            else{
                clientBoard.uiManager.giveKnowledgePlayerPanel.notificationText.text = "Rejected";
            }
        }
        GetComponent<PhotonView>().RPC("GiveKnowledgePanelActivation", PhotonTargets.All, new object[] { false });
    }

    public void onReceiveKnowledgeOffer(CityCard cc, int senderNumber, int receiverNumber) {
        Debug.Log(LevelManager.instance.players[receiverNumber].aPlayerName + " want to receive the " + cc.name + " card from " + LevelManager.instance.players[senderNumber].aPlayerName);
        StartCoroutine(receiveOffer(senderNumber, receiverNumber, cc));
    }
    public void onReceiveKnowledgeResponse(bool active) {
        GetComponent<PhotonView>().RPC("ReceiveKnowledgePanelActivation", PhotonTargets.All, new object[] { active });
    }
    public void onReceiveKnowledgeEnd(int to, bool result) {
        Debug.Log("Sending notification to: " + LevelManager.instance.players[to].aPlayerName);
        GetComponent<PhotonView>().RPC("SignalReceiveKnowledgeEnd", PhotonTargets.All, new object[] { to, result });
    }
    public IEnumerator receiveOffer(int senderNumber, int receiverNumber, CityCard cc) {
        Debug.Log("Sending offer to: " + LevelManager.instance.players[receiverNumber].aPlayerName);
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
        Debug.Log("Receiving the notification: " + LevelManager.instance.players[receiverNumber].aPlayerName);
        if (PhotonNetwork.player.ID - 1 == receiverNumber) {
            GameEngine clientBoard = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
            clientBoard.uiManager.receiveKnowledgePlayerPanel.waiting.gameObject.SetActive(false);
            clientBoard.uiManager.receiveKnowledgePlayerPanel.OpenRespond(clientBoard.players[senderNumber], cc);
        }
    }
    [PunRPC]
    void ReceiveKnowledgePanelActivation(bool active) {
        GameEngine clientBoard = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
        ReceiveKnowledgePlayerPanel panel = clientBoard.uiManager.receiveKnowledgePlayerPanel;

        panel.accept.onClick.RemoveAllListeners();
        panel.refuse.onClick.RemoveAllListeners();

        clientBoard.uiManager.receiveKnowledgePlayerPanel.gameObject.SetActive(false);
    }
    [PunRPC]
    void SignalReceiveKnowledgeEnd(int to, bool result) {
        if (PhotonNetwork.player.ID - 1 == to) {
            Debug.Log("Receive knowledge: " + LevelManager.instance.players[to].aPlayerName + " notified");
            Board clientBoard = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
            clientBoard.uiManager.receiveKnowledgePlayerPanel.notification.gameObject.SetActive(true);
            if (result){
                clientBoard.uiManager.receiveKnowledgePlayerPanel.notificationText.text = "Accepted";
                currentActionsRemaining -= 1;
            }
            else {
                clientBoard.uiManager.receiveKnowledgePlayerPanel.notificationText.text = "Rejected";
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
            case Colour.BLUE:
                blueCured = true;
                break;
            case Colour.BLACK:
                blackCured = true;
                break;
            case Colour.PURPLE:
                purpleCured = true;
                break;
            case Colour.RED:
                redCured = true;
                break;
            case Colour.YELLOW:
                yellowCured = true;
                break;
        }
        while (EventTransferManager.instance.waitingForPlayer){
            yield return new WaitForEndOfFrame();
        }
    }
    [PunRPC]
    void discoverCure(int sender, Colour c) {
        Board clientBoard = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        StartCoroutine(clientBoard.discoverCure(sender, c));
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

    public void onTroubleShooterDirectFlight(CityNode dest) {

    }

    public void onDispatcherDrive(CityNode dest, Player p){

    }

    public void onDispatcherDirectFlight(CityNode dest, Player p){

    }

    public void onDispatcherCharterFlight(CityNode dest, Player p){

    }

    public void onDispatcherShuttleFlight(CityNode dest, Player p){

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

    public void onBioTerroristDrive(CityNode dest) {

    }

    public void onBioTerroristDirectFlight(CityNode dest) {

    }

    public void onBioTerroristCharterFlight(CityNode dest) {

    }

    public void onCarptureBioTerrorist() {

    }

    public void onBioTerroristEscape(CityNode dest) {

    }

    public void onBioTerroristDrawCard() {

    }

    public void onBioTerroristInfectLocally() {

    }

    public void onBioTerroristInfectRemotely(CityNode dest) {

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
        Board clientBoard = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        StartCoroutine(clientBoard.drawPlayerCard(sender));
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
        Board clientBoard = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        StartCoroutine(clientBoard.basicEpidemic());
    }

    public IEnumerator onInfectNextCity() {
        GetComponent<PhotonView>().RPC("infectNextCity", PhotonTargets.All, new object[] {  });
        while (EventTransferManager.instance.waitingForPlayer) {
            yield return new WaitForEndOfFrame();
        }
    }
    [PunRPC]
    void infectNextCity() {
        Board clientBoard = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        StartCoroutine(clientBoard.infectNextCity());
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

    public void onGovernmentGrant(CityNode c) {

    }

    public void onOneQuietNight() {

    }

    public void onForecast() {

    }

    public void onResilientPopulation(CityCard cc) {

    }

    public void onAirlift(CityNode dest, Player target) {

    }

    public void onRapidVaccineDeployment() {

    }

    public void onRemoteTreatment(CityNode c1, Colour co1, CityNode c2, Colour co2) {

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
        Board clientBoard = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        StartCoroutine(clientBoard.highlightConnection(senderPlayer, turnOn, c));
    }

    public void onHighlightCity(int senderPlayer, bool turnOn, CityNode c) {
        GetComponent<PhotonView>().RPC("highlightCity", PhotonTargets.All, new object[] { senderPlayer, turnOn, c });
    }
    [PunRPC]
    void highlightCity(int senderPlayer, bool turnOn, CityNode c) {
        Board clientBoard = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        StartCoroutine(clientBoard.highlightCity(senderPlayer, turnOn, c));
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


