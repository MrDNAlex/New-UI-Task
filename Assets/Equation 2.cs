using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equation2 
{
    //Equation
    public List<Polynomial2> cleanPoly;

    public List<Polynomial2> polynomials;

    public Equation2()
    {
        polynomials = new List<Polynomial2>();
    }

    public void addPolynomial(Polynomial2 poly)
    {
        polynomials.Add(poly);
    }

    public void derive()
    {
        //Add this for the polyclean aswell

        polyClean();

        foreach (Polynomial2 i in cleanPoly)
        {
            i.derive();  
        }

        polynomials = cleanPoly;

       
    }


    //
    //Doesn't Apply
    //

    public float output(List<PolyOutput> vals)
    {
      
        polyClean();
        float value = 0;

        //Loop through to add the 
        value = cleanPoly[0].output(0);

        foreach (PolyOutput i in vals)
        {
            foreach (Polynomial2 j in cleanPoly)
            {
                if (j.variable == i.variable)
                {
                   // Debug.Log("Add: " + j.coefficient + " * " + j.variable + "^" + j.power + " (" + j.variable + "= " + i.value + ") = "+  j.output(i.value));
                    value += j.output(i.value);
                }
            }
        }

       // Debug.Log("Output Value: " + value);
        return value;
    }
    
    //Add extra variables such as initial guess and stuff
    public float solveX(float ans)
    {
        polyClean();

        int varNum = getVarCount();

        float value = 0;
        switch (varNum)
        {
            case 0:
                //Either only 0 degree polynomial or only x, x^2 ect

                //Do I just return the value itself? yeah probably now that I think about it

                value = ans;
               
                break;
            case 1:

                //Ok gotta add the answer to the equation too as a negative aswell

                //Idk fix this later
                this.addPolynomial2(-1*ans, 0, polynomials[1].variable);

                polyClean();

                //One variable only, 

                //Honestly just need to do newton raphson for this basically

                Equation2 derivative = new Equation2();
                derivative.setEquation(cleanPoly);
                derivative.derive();

                //loop for 100 or until change is under 0.1%

                //X initial guess = 1
                float x = 1;
                float newX = 0;
                float diffPer = 0;
               

                for (int i = 0; i < 100; i ++)
                {
                    List<PolyOutput> vals = new List<PolyOutput>();

                    vals.Add(new PolyOutput(this.cleanPoly[1].variable, x));
                    //x1 = x - f(x)/f'(x)
                 //   Debug.Log("x = " + x);
                  //Debug.Log(this.output(vals));
                   // Debug.Log(derivative.output(vals));

                    //Actually calculate the new X
                    newX = x - (this.output(vals) / derivative.output(vals));

                    
                    diffPer = Mathf.Abs((((float)newX - (float)x) / (float)newX) * 100);

                    x = newX;

                    Debug.Log(diffPer);

                    if (diffPer < 0.0001f && i > 5)
                    {
                        i = 100;
                    }


                }

                Debug.Log(x);
                value = x;



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
                    case 3:
                        //Quadratic formula

                        break;
                }

                break;
            default:
                //For any other number of variables inside it 
                //Will have to use either gauss Siedel Method, or Multivariable Newton Raphson
                //I'm thinking we do multivariable Newton raphson, will be hella hard though 




                break;
        }

        return value;
    }
    
    public void polyClean()
    {
        cleanPoly = new List<Polynomial2>();

        //Kinda need a double dimension array

        //Save variable type and it's powers in order


        List<float> pows = new List<float>();
        foreach (Polynomial2 i in polynomials)
        {
            if (!pows.Contains(i.power))
            {
                pows.Add(i.power);
            }
        }

        pows.Sort();

        int variableNums = getVarCount();
        List<string> varNames = getVarList();
      
        cleanPoly.Clear();
        cleanPoly = new List<Polynomial2>();

        cleanPoly.Add(new Polynomial2(0, 0, ""));

        //Create every possible variable and power polynomials
        //Loop through every variable
        foreach (string var in varNames)
        {
            //Loop through every power to add them up
            foreach (float pow in pows)
            {
                if (pow != 0)
                {
                    cleanPoly.Add(new Polynomial2(0, pow, var));
                }
            }
        }

        //Add the polynomials to the list

        foreach (Polynomial2 i in cleanPoly)
        {
            foreach (Polynomial2 j in polynomials)
            {
                //Check if the power is 0, if it is add all the polynomials of power 0 into it since they would just be regular numbers that can be added
                if (i.power == 0)
                {
                    if (j.power == 0)
                    {
                        i.coefficient += j.coefficient;
                    }
                }
                else if (j.variable == i.variable)
                {
                    if (j.power == i.power)
                    {
                        i.coefficient += j.coefficient;
                    }
                }
            }
        }


        /*
        //Gotta find a way to make this cleanup
        //clean up
        foreach (Polynomial2 i in cleanPoly)
        {
            if (i.coefficient == 0)
            {
                cleanPoly.Remove(i);
            }
        }
        */

        //
        //Gotta check for the 0 powers, combine those into one polynomial 
        //

        //Maybe also have a cleanup section where it just loops and removes all the polynomaisl with coefficient 0?
    }

    public void addPolynomial2(float flex, float pow, string variable)
    {
        Polynomial2 poly = new Polynomial2(flex, pow, variable);

        polynomials.Add(poly);
    }

    //
    //Doesn't Apply
    //

    public float getMaxPow()
    {


        polyClean();
        float highPow = 0;
        foreach (Polynomial2 i in cleanPoly)
        {
            if (i.power >= highPow)
            {
                highPow = i.power;
            }
        }

        return highPow;

        //return cleanPoly[cleanPoly.Count - 1].power;
    }

    public int getVarCount ()
    {
        List<string> varNames = new List<string>();
        
        foreach (Polynomial2 i in polynomials)
        {
            if (!varNames.Contains(i.variable))
            {
                varNames.Add(i.variable);
            }
        }

        if (varNames.Count == 1)
        {
            if (varNames[0] == "" || varNames[0] == " " || varNames[0] == null)
            {
                return 0;
            } else
            {
                return 1;
            }
        } else
        {
            return varNames.Count;
        }
    }

    public List<string> getVarList ()
    {
        List<string> varNames = new List<string>();

        foreach (Polynomial2 i in polynomials)
        {
            if (!varNames.Contains(i.variable))
            {
                varNames.Add(i.variable);
            }
        }

        return varNames;
    }

    //So to clean this equation up, we will need to check if the variable name is the same aswell as the powers

    public void setEquation (List<Polynomial2> eq)
    {
        this.polynomials = eq;
    }

    public void deriveWRespect ()
    {

    }


}
