using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumsClass : MonoBehaviour {

}
public enum DiseaseStatus
    {
        ACTIVE,
        CURED,
        ERADICATED
    }

    public enum ConnectionType
    {
        LANDCONNECTION,
        WATERCONNECTION,
    }

    public enum Role
    {
        ARCHIVIST,
        BIOTERRORIT,
        CONTAINMENTSPECIALIST,
        CONTINGENCYPLANNER,
        DISPATCHER,
        EPIDEMOLOGIST,
        FIELDOPERATIVE,
        GENERALIST,
        MEDIC,
        OPERATIONEXPERT,
        QUARANTINESPECIALST,
        RESEARCHER,
        SCIENTIST,
        TROUBLESHOOTER,

    }

    public enum Scenario
    {
        NORMALSCENARIO,
        VIRULENTSCENARIO,
        BIOTERRORIST,
        MUTATIONSCENARIO,
    }

    public enum GamePhase
    {
        SETTINGUP,
        ACTIONPHASE,
        DRAWPHASE,
        INFECTIONPHASE,
        BIOTERRORISTPHASE,
        ENDGAME,
    }

    public enum PlayerStatus
    {
        OFFLINE,
        ONLINE,
        AFK,
    }

    public enum Colour
    {
        BLACK = 0,
        BLUE = 1,
        RED = 2,
        YELLOW = 3,
        PURPLE = 4,
    }

    public enum MutationEvent
    {
        MUTATIONINTENSIFIES,
        MUTATIONSPREADS,
        MUTATIONTHREATENS,
    }

    public enum ActionEvent
    {
        AIRLIFT,
        BORROWEDTIME,
        COMMERCIALTRAVELBAN,
        GOVERNMENTGRANT,
        FORECAST,
        MOBILEHOSPITAL,
        NEWASSIGNMENT,
        ONEQUIETNIGHT,
        RAPIDVACCINEDEPLOYMENT,
        REEXAMINERESEARCH,
        REMOTETREATMENT,
        RESILIENTPOPULATION,
        SPECIALORDERS,
    }

    public enum VirulentStrain
    {
        CHRONICEFFECT,
        COMPLEXMOLECULARSTRUCTURE,
        GOVERMENTINTERFERENCE,
        HIDDENPOCKET,
        RATEEFFECT,
        SLIPPERYSLOPE,
        UNACCEPTABLELOSS,
        UNCOUNTEDPOPULATION,
    }
