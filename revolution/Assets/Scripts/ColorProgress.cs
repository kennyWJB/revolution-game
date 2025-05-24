using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorProgress
{
    public float speed;
    public double multiplier;
    public double costToLevelUp;

    public ColorProgress(float speed, double multiplier, float costToLevelUp)
    {
        this.speed = speed;
        this.multiplier = multiplier;
        this.costToLevelUp = costToLevelUp;
    }
    public void increaseSpeed(float speed)
    {
        this.speed += speed;
    }
    public void increaseMultiplier(double multiplier)
    {
        this.multiplier += multiplier;
    }
    public void increaseCostToLevelUp(double costToLevelUp)
    {
        this.costToLevelUp += costToLevelUp;
    }

}
