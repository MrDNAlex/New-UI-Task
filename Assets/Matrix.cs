using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix 
{

    // List<Equation> EQList;


    List<List<float>> matrix = new List<List<float>>();

   // List<Equation> matrix = new List<Equation>();
    List<float> answerColumn = new List<float>();


    bool linear = true;
    //These matrices will always assume a format where the number
    //Will need a derive with respect to function 

    //Shit alright so this only works for linear equations we will have to keep that in mind

    //Yeah I guess we are going to rework this shit

    public Matrix (List<Equation> EQs)
    {
       // this.EQList = EQs;

        
      
        //Create a list


        //Check if system is linear
        foreach (Equation i in EQs)
        {
           // i.polyClean();
            foreach (Polynomial j in i.cleanPoly)
            {
               if (j.power == 1 || j.power == 0)
                {
                } else
                {
                    linear = false;
                }
            }
        }


        if (linear)
        {
            List<Polynomial> columns = new List<Polynomial>();


            //Make a section to add all the 0 power stuff to the answer column


            //Place all 0 power coefficients in the answer list
            for (int i = 0; i < EQs.Count; i ++)
            {
                answerColumn.Add(0);

                for (int j = 0; j < EQs[i].polynomials.Count; j ++)
                {
                    if (EQs[i].polynomials[j].power == 0)
                    {
                        //Minus because we flip it to the other side
                        answerColumn[i] -= EQs[i].polynomials[j].coefficient;

                        EQs[i].removePolynomial(EQs[i].polynomials[j]);
                    }
                }
            }


            //Add all unique types of polynomials (This will determine the order in the matrix) 
            foreach (Equation i in EQs)
            {
               // i.polyClean();
                foreach (Polynomial j in i.cleanPoly)
                {
                    Polynomial newPoly = new Polynomial(0, j.power, j.variable);

                    bool contained = false;
                   // Debug.Log(j.variable + "^" + j.power);

                    if (!Equation.containsPoly(columns, newPoly))
                    {
                        columns.Add(newPoly);
                        //Debug.Log("added");
                    }

                    /*
                    foreach (Polynomial k in columns)
                    {
                        if (k.variable == newPoly.variable)
                        {
                            if (k.power == newPoly.power)
                            {
                                contained = true;
                            }
                        }
                    }

                    if (!contained)
                    {
                        columns.Add(newPoly);
                        Debug.Log("added");
                    }
                    */


                }
            }

            matrix = new List<List<float>>();

            foreach (Equation i in EQs)
            {
                //Just in case
                i.polyClean();
                List<float> row = new List<float>();

                //Go in the list of the columns
                foreach (Polynomial j in columns)
                {
                    foreach (Polynomial k in i.polynomials)
                    {
                        if (j.variable == k.variable)
                        {
                            if (j.power == k.power)
                            {
                               // Debug.Log("Adding: " + k.coefficient);
                                row.Add(k.coefficient);
                            }
                        }
                    }
                }
                matrix.Add(row);
            }

            displayMatrix(matrix, answerColumn);


            displayMatrix(gaussianedMatrix(), answerColumn);







            /*
            
            //Create base equation
            Equation eq = new Equation();
            foreach (Polynomial i in columns)
            {
                eq.addPolynomial(new Polynomial(0, i.power, i.variable));
              
            }

            eq.polyClean();

            for (int i = 0; i < eq.cleanPoly.Count; i++)
            {
                Debug.Log("Poly " + i + " : " + eq.cleanPoly[i].coefficient + eq.cleanPoly[i].variable + " Pow : " + eq.cleanPoly[i].power);
            }
            */


            /*
            foreach (Equation i in EQList)
            {
                Equation temp = new Equation();
                temp.setEquation(eq.getEQ());


                matrix.Add(temp);
            }

           
            for (int i = 0; i < matrix.Count; i ++)
            {
                foreach (Polynomial j in EQList[i].polynomials)
                {
                    Debug.Log("Adding : " + j.coefficient + j.variable + "^" + j.power);
                    matrix[i].addPolynomial(j);
                }

                //Clean it
                matrix[i].polyClean();

                for (int k = 0; k < matrix[i].cleanPoly.Count; k++)
                {
                    Debug.Log("Poly " + k + " : " + matrix[i].cleanPoly[k].coefficient + matrix[i].cleanPoly[k].variable + " Pow : " + matrix[i].cleanPoly[k].power);
                }
            }
            
            
            //Fill out the matrix
            for (int i = 0; i < matrix.Count; i ++)
            {
                foreach (Polynomial j in matrix[i].polynomials)
                {
                    foreach (Polynomial k in EQList[i].cleanPoly)
                    {
                        if (j.variable == k.variable)
                        {
                            j.coefficient = k.coefficient;
                        }
                    }
                }
            } 
            



            for (int i = 0; i < matrix.Count; i ++)
            {
                string idk = "";
                for (int j = 0; j < matrix[i].polynomials.Count; j ++)
                {
                    idk += " " + matrix[i].polynomials[j].coefficient;
                }
                idk += " | " + answerColumn[i];
                Debug.Log("Equation : " + idk);
            }

            */
            
        } else
        {
            Debug.Log("System is not linear, we cannot solve it");
        }

        getInverseMatrix(this);

    }

    
    
    public void getInverseMatrix (Matrix mat)
    {

        // i = 1
        // 1 2 3 | 1 0 0
        // 4 5 6 | 0 1 0  --> R2 - 4*r1 =      4 - 4/1 * 1    and 5 - 4/1 *2
        // 7 8 9 | 0 0 1
        //
        //
        // 1 2 3 | 1 0 0
        // 0 -3 -6 | 0 1 0  --> R2 - 4*r1 = 
        // 7 8 9 | 0 0 1

        // i >>
        // j controls how far down 
        //

        //num(i, j) - num () / og (i,i) 


        //Check if linear 

        //J controls height, i controls width


        //Determine if matrix is square
        //Determine if determinant is zero, if so inverse doesn't exist

        //If matrix was linear
        if (linear)
        {
            //If matrix is square
            if (matrix.Count == matrix[0].Count)
            {


                float det = getDeterminant();

                Debug.Log("Determinant : " + det);



                if (det == 0)
                {
                    //Can't get inverse
                } else
                {
                    //Get inverse

                    List<List<float>> matCopy = getMatrixCopy(matrix);

                    List<List<float>> unitMat = getUnitMatrix(matrix.Count);


                    //Divide the row until the diagonal is equal to 1

                    //Do gaussian elimination above and under while also adding 
                    Debug.Log("Before inverse");
                    displayMatrix(matCopy, answerColumn);

                    for (int i = 0; i < matCopy[0].Count; i++)
                    {
                        //Divide row


                        for (int j = 0; j < matCopy.Count; j++)
                        {

                            //Top becomes whatever height and width is 
                            //Denom becomes the diagonal
                            //Current num is the height and width and 
                            //Multiply is same as bottom 


                            for (int k = 0; k < matCopy[0].Count; k++)
                            {
                                if (i == j)
                                {

                                } else
                                {
                                    matCopy[j][i] = matCopy[j][i] - (matCopy[j][i] / matCopy[i][i]) * matCopy[j][k];
                                }
                               

                            }
                        }
                    }

                    displayMatrix(matCopy, answerColumn);


                    /*

                    for (int e = 0; e < matCopy.Count; e ++)
                    {

                        divideRow(matCopy, e, matCopy[e][e]);

                        divideRow(unitMat, e, matCopy[e][e]);


                        //Do gaussian elimination on both sides 


                        //Careful for first and last edge case


                        //
                        //Oh boy am I doing this wrong, let's get back to it later once I'm home and once I get a bit of sleep and can think straight
                        //



                        //Alright so first row it's all down as normal




                        //Ok so go left to right   --->

                        //At every 


                        //Top becomes


                       








                        //
                        //Bottom Gaussian 
                        //

                        for (int i = 0; i < matCopy[0].Count; i++)
                        {
                            //Loop through height
                            for (int j = i + 1; j < matCopy.Count; j++)
                            {
                                float OGTop = matCopy[j][i];
                                float OGDenom = matCopy[i][i];
                                //Loop through width again

                                //We could simplify this
                                matCopy[j][i] = matCopy[j][i] - (OGTop / OGDenom) * matCopy[i][i];
                                //Also add whatever it is to the unit matrix
                               
                                //Remove this section
                                for (int k = i; k < matrix[0].Count; k++)
                                {
                                    matCopy[j][k] = matCopy[j][k] - (OGTop / OGDenom) * matCopy[i][k];

                                    unitMat[j][k] -= (OGTop / OGDenom) * unitMat[i][k];

                                    //  Debug.Log("I = " + i + " " + "j = " + j + " " + "k = " + k + " ");
                                    // Debug.Log(matCopy[j][k] + "- (" + OGTop + "/" + OGDenom + ")" + "*" + matCopy[i][k]);
                                }
                            }

                            displayMatrix(matCopy, answerColumn);
                            displayMatrix(unitMat, answerColumn);
                        }

                        //
                        //Top Gaussian
                        //

                        for (int i = 0; i < matCopy[0].Count; i++)
                        {
                            //Loop through height
                            for (int j = i - 1; j >= 0; j--)
                            {
                                Debug.Log(j);
                                float OGTop = matCopy[j][i];
                                float OGDenom = matCopy[i][i];
                                //Loop through width again

                                //We could simplify this
                                matCopy[j][i] = matCopy[j][i] - (OGTop / OGDenom) * matCopy[i][i];
                                //Also add whatever it is to the unit matrix

                                //Remove this section
                                for (int k = 0; k < matrix[0].Count; k++)
                                {
                                    matCopy[j][k] = matCopy[j][k] - (OGTop / OGDenom) * matCopy[i][k];

                                    unitMat[j][k] -= (OGTop / OGDenom) * unitMat[i][k];

                                    //  Debug.Log("I = " + i + " " + "j = " + j + " " + "k = " + k + " ");
                                    // Debug.Log(matCopy[j][k] + "- (" + OGTop + "/" + OGDenom + ")" + "*" + matCopy[i][k]);
                                }
                            }
                            displayMatrix(matCopy, answerColumn);
                            displayMatrix(unitMat, answerColumn);
                        }

                    }
                    */

















                }
                    


                /*

                string matrixStr = "";

                for (int i = 0; i < matCopy.Count; i++)
                {
                    string line = "";
                    foreach (float j in matCopy[i])
                    {
                        line += j.ToString() + " ";
                    }

                    matrixStr += line + " | " + answerColumn[i] + "\n";
                }

                Debug.Log(matrixStr);

                */

            }
            else
            {
                //Matrix is not square
                Debug.Log("Matrix is not square");
            }
        }

    }
    

    public float getDeterminant ()
    {
        float det = 0;
        if (linear)
        {
            //If matrix is square
            if (matrix.Count == matrix[0].Count)
            {

                List<List<float>> matCopy = gaussianedMatrix();


                
                for (int i = 0; i < matCopy.Count; i++)
                {
                    if (i == 0)
                    {
                        det = matCopy[i][i];

                    }
                    else
                    {
                        det = det * matCopy[i][i];
                    }
                }

                Debug.Log("Determinant : " + det);

            }
            else
            {
                //Matrix is not square
                Debug.Log("Matrix is not square");
            }



        }
        return det;
    }

    public List<List<float>> gaussianedMatrix ()
    {


        //Get Determinant
        List<List<float>> matCopy = getMatrixCopy(matrix);


        //Gauss Jordan Elimination
        //Loop through width
        for (int i = 0; i < matCopy[0].Count; i++)
        {
            //Loop through height
            for (int j = i + 1; j < matCopy.Count; j++)
            {
                float OGTop = matCopy[j][i];
                float OGDenom = matCopy[i][i];
                //Loop through width again

                for (int k = i; k < matrix[0].Count; k++)
                {
                    matCopy[j][k] = matCopy[j][k] - (OGTop / OGDenom) * matCopy[i][k];

                  //  Debug.Log("I = " + i + " " + "j = " + j + " " + "k = " + k + " ");
                   // Debug.Log(matCopy[j][k] + "- (" + OGTop + "/" + OGDenom + ")" + "*" + matCopy[i][k]);
                }
            }
        }
        return matCopy;
    }

    public List<List<float>> getMatrixCopy (List<List<float>> mat)
    {
        List<List<float>> copy = new List<List<float>>();
        foreach (List<float> i in mat)
        {
            List<float> row = new List<float>();
            foreach (float j in i)
            {
                row.Add(j);
            }
            copy.Add(row);
        }

        return copy;
    }

    public List<List<float>> getUnitMatrix (int size)
    {
        List<List<float>> mat = new List<List<float>>();

        for (int i = 0; i < size; i ++)
        {
            List<float> row = new List<float>();
            for (int j = 0; j < size; j ++)
            {
                if (j == i)
                {
                    row.Add(1);
                } else
                {
                    row.Add(0);
                }
            }
            mat.Add(row);
        }

        return mat;
    }

    public void displayMatrix (List<List<float>> mat, List<float> ans)
    {
        string matrixStr = "";

        for (int i = 0; i < mat.Count; i++)
        {
            string line = "";
            foreach (float j in mat[i])
            {
                line += j.ToString() + " ";
            }

            matrixStr += line + " | " + ans[i] + "\n";
        }

        Debug.Log(matrixStr);
    }


    public void divideRow (List<List<float>> mat, int row, float denom)
    {
        for (int i = 0; i < mat[row].Count; i ++)
        {
            mat[row][i] = mat[row][i] / denom;
        }
    }



}
