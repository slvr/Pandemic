using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour{

    public Role aRole;

    public Pawn(Role pRole)
    {
        aRole = pRole;
    }

    public void setRole(Role pRole)
    {
        aRole = pRole;
    }

    public Role GetRole()
    {
        return aRole;
    }

    public void SetPosition(Vector2 pPosition)
    {
        transform.position = pPosition;
    }
}
