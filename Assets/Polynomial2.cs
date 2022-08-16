using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polynomial2 
{
    public float coefficient;
    public float power;
    public string variable;


    public Polynomial2(float coef, float pow, string variable = null)
    {
        //Coefficient
        //Power
        this.coefficient = coef;
        this.power = pow;

        if (variable != null)
        {
            this.variable = variable;
        } else if (pow >= 1)
        {
            this.variable = "x"; //First variable type
        }
    }

    //Make a function to return output (when x input), func for derivative

    public float output(float x)
    {
        return coefficient * Mathf.Pow(x, power);
    }

    public void derive()
    {
        this.coefficient = coefficient * power;
        this.power = this.power - 1;
    }

}
