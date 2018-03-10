using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityNode : MonoBehaviour {

    Material m_Material;

    public City aCity;
    public Dictionary<Colour, int> aDiseases;
    public bool hasResearchStation;
    public List<Connection> aConnections;
    public List<Pawn> aPawns;


    CityNode(City pCity){
        aCity = pCity;
        aDiseases = new Dictionary<Colour, int>();
        aConnections = new List<Connection>();
        aPawns = new List<Pawn>();
        aDiseases.Add(Colour.BLUE, 0);
        aDiseases.Add(Colour.RED, 0);
        aDiseases.Add(Colour.BLACK, 0);
        aDiseases.Add(Colour.YELLOW, 0);
        aDiseases.Add(Colour.PURPLE, 0);
        hasResearchStation = false;

        // m_Material = GetComponent<Renderer>().material;
        m_Material = GetComponent<Renderer>().material;

        Debug.Log(m_Material);
        SetColor();

    }

    public void Start()
    {
        aDiseases = new Dictionary<Colour, int>();
        aConnections = new List<Connection>();
        aPawns = new List<Pawn>();
        aDiseases.Add(Colour.BLUE, 0);
        aDiseases.Add(Colour.RED, 0);
        aDiseases.Add(Colour.BLACK, 0);
        aDiseases.Add(Colour.YELLOW, 0);
        aDiseases.Add(Colour.PURPLE, 0);
        hasResearchStation = false;

        // m_Material = GetComponent<Renderer>().material;
        m_Material = GetComponent<Renderer>().material;
        
        SetColor();
    }


    private void OnGUI()
    {
        //  Displays the name of city
        Vector3 getPixelPos = Camera.main.WorldToScreenPoint(this.transform.position);
        getPixelPos.y = Screen.height - getPixelPos.y + 30.0f;

        GUI.Label(new Rect(getPixelPos.x, getPixelPos.y, 200f, 100f), aCity.GetName());

        // Displays any Pawns in City
        Vector3 posi = this.GetComponent<Transform>().position;
        float i = 1.0f;
        Vector3 posi2;
        foreach (Pawn p in aPawns)
        {
            posi2 = new Vector3(posi.x - 1, posi.y- i, posi.z);
            p.SetPosition(posi2);
            i += 1.0f;
        }
        
        // Debug.Log("Blue Disease: " + aDiseases[Colors.BLUE]);
        // Display Diseases

        float j = 10.0f;
        GUI.Label(new Rect(getPixelPos.x, getPixelPos.y + j, 200f, 100f), "Blue: " + aDiseases[Colour.BLUE].ToString() );
        j += 10.0f;
        GUI.Label(new Rect(getPixelPos.x, getPixelPos.y + j, 200f, 100f), "Black: " + aDiseases[Colour.BLACK].ToString() );
        j += 10.0f;
        GUI.Label(new Rect(getPixelPos.x, getPixelPos.y + j, 200f, 100f), "Red: " + aDiseases[Colour.RED].ToString() );
        j += 10.0f;
        GUI.Label(new Rect(getPixelPos.x, getPixelPos.y + j, 200f, 100f), "Yellow: " + aDiseases[Colour.YELLOW].ToString() );
        
    }

    public City GetCity()
    {
        return aCity;
    }

    public void addResearchStation()
    {
        hasResearchStation = true;
    }

    public void RemoveResearchStation()
    {
        hasResearchStation = false;
    }

    public bool GetResearchStation()
    {
        return hasResearchStation;
    }
    
    public void AddDisease(Colour pDisease, int i)
    {
        for (int a = 0; a < i; a++)
        {
            if (aDiseases[pDisease] < 3)
            {
                aDiseases[pDisease]++;
            }
            else
            {
                break;
            }
        }
    }

    public void RemoveDisease(Colour pDisease, int i)
    {
        for (int a = 0; a < i; a++)
        {
            if (aDiseases[pDisease] > 0)
            {
                aDiseases[pDisease]--;
                Debug.Log(aDiseases[pDisease]);
            }
            else
            {
                break;
            }
        }
    }

    public int GetDisease(Colour pDisease)
    {
        return aDiseases[pDisease];
    }
    
    public Dictionary<Colour, int> GetDisease()
    {
        return aDiseases;
    }

    public void AddConnection(Connection pConnection)
    {
        aConnections.Add(pConnection);
    }
    

    public List<Pawn> GetPawns()
    {
        return aPawns;
    }

    public bool HasPawns()
    {
        if(aPawns.Count == 0)
        {
            return false;
        }
        return true;
    }

    public void RemovePawn(Pawn pPawn)
    {
        if (aPawns.Contains(pPawn))
        {
            aPawns.Remove(pPawn);
            Debug.Log("Pawn removed to " + aCity.GetName());
        }
        else
        {
            Debug.Log("Pawn not in the list");
        }
    }

    public void AddPawn(Pawn pPawn)
    {
        if (!aPawns.Contains(pPawn))
        {
            aPawns.Add(pPawn);
            Debug.Log("Pawn added to " + aCity.GetName());

        }
        else
        {
            Debug.Log("Pawn already in the list");
        }
    }
    

    //Outbreak cannot be done whjile some calsses are missing
    // TODO
    public void Outbreak()
    {

    }

    private void SetColor()
    {
        Colour c = aCity.GetColor();
        switch (c)
        {
            case Colour.BLACK:
                m_Material.color = Color.black;
                break;
            case Colour.RED:
                m_Material.color = Color.red;
                break;
            case Colour.BLUE:
                m_Material.color = Color.blue;
                break;
            case Colour.YELLOW:
                m_Material.color = Color.yellow;
                break;
            default:
                m_Material.color = Color.white;
                Debug.Log("Something wrong when loading color.");
                break;
        }
    }
}
