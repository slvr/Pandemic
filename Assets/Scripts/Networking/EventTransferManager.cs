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

    }

    public void OnEndTurn() {

    }

    public void OnEndGame() {

    }

    void GameOver() {

    }

    public void OnOperationFailure() {

    }

    /**
     * METHODS FOR GAME CREATION FROM SAVE FILE
     * */

    /**
     * METHODS FOR GENERAL TURN ACTIONS
     * */

    public void onDrive(City dest) {

    }

    public void onDirectFlight(City dest) {

    }

    public void onCharterFlight(City dest) {

    }

    public void onShuttleFlight(City dest) {

    }

    public void onBuildResearchStation(CityCard cc) {

    }

    public void onTreatDisease(Colour c) {

    }

    public void onGiveKnowledge(CityCard cc, Player receiver) {

    }

    public void onReceiveKnowledge(CityCard cc, Player sender) {

    }

    public void onDiscoverCure(Colour c, CityCard[] ccs) {

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

    public void onDrawCard() {

    }

    public void onBasicEpidemic() {

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
     * METHODS FOR COMMUNICATION
     * */

}

public enum MoveType {

}
