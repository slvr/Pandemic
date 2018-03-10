using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

	public int cardsToDraw;
    public Pawn[] aPanws;
    public DiseaseHub aDiseaseHub;
    public CardHub aCardHub;
    public CityHub aCityHub;
    public List<Event> activeEventList;

    public EventTransferManager ETManager = null;


    public Board()
    {
        cardsToDraw = 2;
        aDiseaseHub = new DiseaseHub();
        aCardHub = new CardHub();
        aCityHub = new CityHub();
        activeEventList = new List<Event>();


    }

    private void Start()
    {

        cardsToDraw = 2;
        aDiseaseHub = new DiseaseHub();
        aCardHub = new CardHub();
        aCityHub = new CityHub();

        activeEventList = new List<Event>();


        GameObject cityNodes = GameObject.Find("CityNodes");
        GameObject Pawns = GameObject.Find("Pawns");
        Debug.Log(cityNodes != null);

       // Pawn pawn1 = new Pawn(Role.ARCHIVIST);
       // Pawn pawn2 = new Pawn(Role.BIOTERRORIT);

        ////////////////////////////////////////////////////////////////////////////////////
        // Places Pawns
        CityNode[] myscript = cityNodes.GetComponentsInChildren<CityNode>();
        Pawn[] myPawns = Pawns.GetComponentsInChildren<Pawn>();
        myscript[0].AddPawn(myPawns[0]);
        myscript[1].AddPawn(myPawns[1]);
        ////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////
        // Places Diseases
        myscript[0].AddDisease(Colour.BLACK, 1);
        myscript[1].AddDisease(Colour.BLUE, 1);
        myscript[2].AddDisease(Colour.RED, 2);
        myscript[3].AddDisease(Colour.YELLOW, 3);

        ////////////////////////////////////////////////////////////////////////////////////
        // Buttons

        



    }

    public List<Event> getEvents()
    {
        return activeEventList;
    }

    public CardHub GetCardHub()
    {
        return aCardHub;
    }

    public CityHub GetCityHub()
    {
        return aCityHub;
    }

    public DiseaseHub GetDiseaseHub()
    {
        return aDiseaseHub;
    }

    // TODO code later
    public void Epidemic()
    {

    }
    public void EndTurn()
    {

    }

    ////////////////////////////////////////////////////////////////////////////////////
    // Buttons functions
    // TODO get rid of these functions once we don't need them anymore

    public IEnumerator TreatADisease()
    {
        GameObject cityNodes = GameObject.Find("CityNodes");
        CityNode[] myscript = cityNodes.GetComponentsInChildren<CityNode>();
        
        foreach(CityNode cn in myscript)
        {
            foreach(Colour c in Enum.GetValues(typeof(Colour))){
                Debug.Log(c.ToString());
                if(cn.GetDisease(c) != 0)
                {
                    cn.RemoveDisease(c, 1);
                }
            }
        }

        ETManager.onTreatDisease();

        Debug.Log("No more disease to treat");

        while (EventTransferManager.instance.waitingForPlayer)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator TrvelToMontreal()
    {
        GameObject cityNodes = GameObject.Find("CityNodes");
        CityNode[] myscript = cityNodes.GetComponentsInChildren<CityNode>();
        CityNode SaoPaulo = myscript[0];
            

        foreach (CityNode cn in myscript)
        {
            if (cn.GetCity().GetName() == "Montreal")
            {
                SaoPaulo = cn;
            }
        }

        
        foreach (CityNode cn in myscript)
        {
            if(cn.GetCity().GetName() == "Montreal")
            {
                continue;
            }
            if (cn.HasPawns())
            {
                Pawn p = cn.GetPawns()[0];
                cn.RemovePawn(p);
                SaoPaulo.AddPawn(p);
               // return;
            }
        }

        ETManager.onDrive();

        Debug.Log("No more pawns to move");

        while (EventTransferManager.instance.waitingForPlayer)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    // tryToMethods will call the network which in turn will call
    // the correspending method for each player.
    public void tryToDrive()
    {
        // just tells the network to tell everyone to drive
    //    ETManager.Instance.onDrive(senderPlayer, CityDestination))
    }

    public void drive()
    {
    }
}
