using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleche
{
    private float positionX;
    private float positionY;
    private float positionZ;

    public Unite autreUnite;

    public float PositionX
    {
        get { return positionX; }
        set { positionX = value; }
    }

    public float PositionY
    {
        get { return positionY; }
        set { positionY = value; }
    }

     public float PositionZ
    {   
        get { return positionZ; }
        set { positionZ = value; }
    }

    public Fleche(float posX, float posY, float posZ,Unite newAutreUnite){
        this.positionX = posX;
        this.positionY = posY;
        this.positionZ = posZ;

        this.autreUnite = newAutreUnite;
    }
}
