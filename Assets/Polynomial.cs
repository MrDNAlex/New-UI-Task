using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polynomial 
{
    public float coefficient;
    public float power;


    public Polynomial (float coef, float pow)
    {
        //Coefficient
        //Power
        this.coefficient = coef;
        this.power = pow;
    }

    //Make a function to return output (when x input), func for derivative

    public float output (float x)
    {
        return coefficient * Mathf.Pow(x, power);
    }

    public void derive ()
    {
        coefficient = coefficient * power;
        power -= 1;
    }








  
}
