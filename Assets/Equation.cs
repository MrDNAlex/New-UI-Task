using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equation
{

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
        switch (cleanPoly.Count)
        {
            case 1:
                value = ans / cleanPoly[0].coefficient;
                break;

            case 2:
                //Square root
                break;
        }
        return value;
    }

    public void polyClean()
    {
        cleanPoly = new List<Polynomial>();
        cleanPoly.Add(new Polynomial(0, 1));

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
    }

    public void addPolynomial2 (float flex, float pow)
    {
        Polynomial poly = new Polynomial(flex, pow);

        polynomials.Add(poly);
        
    }  





}
