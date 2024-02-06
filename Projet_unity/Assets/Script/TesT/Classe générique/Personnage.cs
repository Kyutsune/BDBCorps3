using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnage{

    private float positionX = 0;
    private float positionY = 0;

    // Propriétés pour accéder aux positions
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
}
