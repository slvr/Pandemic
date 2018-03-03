using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Persistence {
    public class pe_SavefileNames {
        public string[] pe_saveFileNames;
    }

    public class pe_GameState {
        public pe_ETM eventTransferManager;
        public pe_players players;
        public pe_board board;
        public pe_playerCardStack playerCardStack;
        public pe_playerDiscardStack playerDiscardStack;
        public pe_infectionCardStack infectionCardStack;
        public pe_infectionDiscardStack infectionDiscardStack;
    }

    public class pe_ETM {
        public int currentPlayerTurn;
        public int currentActionsPlayed;
        /* ... */
    }

    public class pe_players {
        public int total;
        public pe_player[] playerArray;
    }

    public class pe_player {
        public string playerName;
        public pe_role playerRole;
        public int playerNumber;
        public pe_playerCardStack playerCards;
        public pe_city playerLocation;
        /* ... */
    }

    public class pe_role {
        public string roleName;
    }

    public class pe_board {
        public pe_cities boardCities;
        public int outbreakCount;
        public int infectionRate;
        public int numResearchStations;
        public bool isRedCured;
        public bool isBlueCured;
        public bool isYellowCured;
        public bool isBlackCured;
        public bool isPurpleCured;
    }

    public class pe_cities {
        public pe_city[] allCities;
    }

    public class pe_city {
        public pe_city[] adjacentCities;
        public int redDisease;
        public int blueDisease;
        public int yellowDisease;
        public int blackDisease;
        public int purpleDisease;
        public bool isResearchStation;
        public string cityName;
        public int cityPopulation;
        public Colour cityColour;
    }

    public enum Colour {
        Red,
        Blue,
        Yellow,
        Black
    }

    public class pe_playerCardStack {
        public pe_epidemicCard[] epidemicCards;
        public pe_cityCard[] cityCards;
        public pe_eventCard[] eventCards;
    }

    public class pe_playerDiscardStack {
        public pe_epidemicCard[] epidemicCards;
        public pe_cityCard[] cityCards;
        public pe_eventCard[] eventCards;
    }

    public class pe_epidemicCard {
        public string effectName;
        public bool isContinuous;
    }

    public class pe_cityCard {
        public pe_city city;
    }

    public class pe_eventCard {
        public string effectName;
        public bool isContinuous;
    }

    public class pe_infectionCardStack {
        public pe_infectionCard[] infectionCards;
        public pe_mutationCard[] mutationCards;
    }

    public class pe_infectionDiscardStack {
        public pe_infectionCard[] infectionCards;
        public pe_mutationCard[] mutationCards;
    }

    public class pe_infectionCard {
        public pe_city city;
    }

    public class pe_mutationCard {

    }
}