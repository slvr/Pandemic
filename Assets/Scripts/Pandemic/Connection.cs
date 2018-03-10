using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour {

    protected City[] aCities = new City[2];
    protected ConnectionType aType;

    Connection(City pCity1, City pCity2, ConnectionType pType)
    {
        aCities[0] = pCity1;
        aCities[1] = pCity2;
        aType = pType;
    }

    public City GetCity(int i)
    {
        if(i == 1)
        {
            return aCities[1];
        }
        return aCities[0];
    }

    public ConnectionType GetConnectionType()
    {
        return aType;
    }

}
