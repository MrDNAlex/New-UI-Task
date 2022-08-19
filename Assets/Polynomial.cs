using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polynomial
{
    public float coefficient;
    public float power;
    public string variable;


    public Polynomial(float coef, float pow, string variable = "x")
    {
        //Coefficient
        //Power
        this.coefficient = coef;
        this.power = pow;

        if (variable != null)
        {
            this.variable = variable;
        }
        else if (pow >= 1)
        {
            this.variable = "x"; //First variable type
        }

        if (power == 0)
        {
            this.variable = "";
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

    public Polynomial deriveWRespect (string var)
    {
        if (var == this.variable)
        {
            return new Polynomial(coefficient * power, power - 1, variable);     
        } else
        {
           
            return new Polynomial(0, power, variable);
        }
    }


}
