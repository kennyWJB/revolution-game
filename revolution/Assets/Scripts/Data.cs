using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public double money;
    public ColorProgress red;
    public Data()
    {
        money = 0;
        red = new ColorProgress(0.63f,1,9.95f);
    }

}
