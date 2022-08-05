using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equation
{
    //Equation
    public List<Polynomial> cleanPoly;


    public List<Polynomial> polynomials;

    public Equation()
    {
        polynomials = new List<Polynomial>();
    }

    public void addPolynomial(Polynomial poly)
    {
        polynomials.Add(poly);
    }

    public void derive()
    {
        for (int i = 0; i < polynomials.Count; i++)
        {
            polynomials[i].derive();
        }
    }

    public float output(float x)
    {
        float value = 0;
        for (int i = 0; i < polynomials.Count; i++)
        {
            value = value + polynomials[i].output(x);
        }
        return value;
    }

    public float solveX(float ans)
    {
        polyClean();
        float value = 0;
        //Gonna have to add new features soon, tbh the bst way of doing this might be newton raphson method for everthing
        switch (cleanPoly.Count)
        {
            case 1:
                //Either only 0 degree polynomial or only 
                if (cleanPoly[0].power != 0)
                {
                    value = Mathf.Pow(ans / cleanPoly[0].coefficient, 1 / cleanPoly[0].power);
                }
                else
                {
                    value = ans / cleanPoly[0].coefficient;
                }

                break;

            case 2:
                //basic number and 1 power


                //This assumes only one of them have a power
                value = Mathf.Pow((ans - cleanPoly[0].coefficient) / cleanPoly[1].coefficient, 1 / cleanPoly[1].power);

                break;
        }
        return value;
    }

    public void polyClean()
    {
        cleanPoly = new List<Polynomial>();
        // cleanPoly.Add(new Polynomial(0, 1));

        //Make a list of all the powers in order (smallest to highest)
        List<float> pows = new List<float>();
        for (int i = 0; i < polynomials.Count; i++)
        {
            if (!pows.Contains(polynomials[i].power))
            {
                pows.Add(polynomials[i].power);
            }
        }

        pows.Sort();


        //Add all powers to clean poly in order
        for (int i = 0; i < pows.Count; i++)
        {
            cleanPoly.Add(new Polynomial(0, pows[i]));
        }

        for (int i = 0; i < polynomials.Count; i++)
        {
            for (int j = 0; j < cleanPoly.Count; j++)
            {
                if (cleanPoly[j].power == polynomials[i].power)
                {
                    cleanPoly[j].coefficient += polynomials[i].coefficient;
                }
            }
        }

        /*

         for (int i = 0; i < polynomials.Count; i++)
         {
             bool added = false;
             for (int j = 0; j < cleanPoly.Count; j++)
             {
                 if (cleanPoly[j].power == polynomials[i].power)
                 {
                     cleanPoly[j].coefficient = cleanPoly[j].coefficient + polynomials[i].coefficient;
                     added = true;
                 }
             }
             if (!added)
             {
                 cleanPoly.Add(new Polynomial(polynomials[i].coefficient, polynomials[i].power));
             }
         }
        */
    }

    public void addPolynomial2(float flex, float pow)
    {
        Polynomial poly = new Polynomial(flex, pow);

        polynomials.Add(poly);
    }

    public float getMaxPow()
    {
        polyClean();
        return cleanPoly[cleanPoly.Count - 1].power;
    }








}
