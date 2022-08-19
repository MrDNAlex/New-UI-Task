using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix
{

    // List<Equation> EQList;


   public List<List<float>> matrix = new List<List<float>>();
    public List<List<float>> inverseMat = new List<List<float>>();

    // List<Equation> matrix = new List<Equation>();
   public List<float> answerColumn = new List<float>();


    bool linear = true;
    //These matrices will always assume a format where the number
    //Will need a derive with respect to function 

    //Shit alright so this only works for linear equations we will have to keep that in mind

    //Yeah I guess we are going to rework this shit

    public Matrix(List<Equation> EQs)
    {
        // this.EQList = EQs;



        //Create a list


        //Check if system is linear
        foreach (Equation i in EQs)
        {
             i.polyClean();
            foreach (Polynomial j in i.cleanPoly)
            {
                if (j.power == 1 || j.power == 0)
                {
                }
                else
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
            for (int i = 0; i < EQs.Count; i++)
            {
                answerColumn.Add(0);

                for (int j = 0; j < EQs[i].polynomials.Count; j++)
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
                                row.Add((float)k.coefficient);
                            }
                        }
                    }
                }
                matrix.Add(row);
            }

          //  displayMatrix(matrix, answerColumn);


          //  displayMatrix(gaussianedMatrix(), answerColumn);




            inverseMat = getInverseMatrix();


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

        }
        else
        {
            Debug.Log("System is not linear, we cannot solve it");
        }



    }



    public List<List<float>> getInverseMatrix()
    {
        List<List<float>> invMat = new List<List<float>>();
        //Determine if matrix is square
        //Determine if determinant is zero, if so inverse doesn't exist

        //If matrix was linear
        if (linear)
        {
            displayMatrix(matrix, answerColumn);

            //If matrix is square
            if (matrix.Count == matrix[0].Count)
            {


                float det = getDeterminant();

                Debug.Log("Determinant : " + det);

                if (det == 0)
                {
                    //Can't get inverse
                }
                else
                {
                    //Get inverse

                    List<List<float>> matCopy = getMatrixCopy(matrix);

                    List<List<float>> unitMat = getUnitMatrix(matrix.Count);


                    //Divide the row until the diagonal is equal to 1

                    //Do gaussian elimination above and under while also adding 

                   // displayMatrix(matCopy, answerColumn);

                    for (int i = 0; i < matCopy[0].Count; i++)
                    {
                        divideRow(unitMat, i, matCopy[i][i]);
                        divideRow(matCopy, i, matCopy[i][i]);

                       // displayMatrix(matCopy, answerColumn);
                       // displayMatrix(unitMat, answerColumn);
                        //Loop through height
                        for (int j = 0; j < matCopy.Count; j++)
                        {
                            float OGTop = matCopy[j][i];
                            float OGDenom = matCopy[i][i];
                            //Loop through width again

                            for (int k = 0; k < matrix[0].Count; k++)
                            {

                                if (i == j)
                                {

                                }
                                else
                                {
                                    //  Debug.Log((unitMat[j][k] + "-" + "(" +matCopy[j][i] + "/" + matCopy[i][i] + ") *" + unitMat[i][k]));
                                    unitMat[j][k] = unitMat[j][k] - (OGTop / OGDenom) * unitMat[i][k];
                                    matCopy[j][k] = matCopy[j][k] - (OGTop / OGDenom) * matCopy[i][k];
                                }


                                //  Debug.Log("I = " + i + " " + "j = " + j + " " + "k = " + k + " ");
                                // Debug.Log(matCopy[j][k] + "- (" + OGTop + "/" + OGDenom + ")" + "*" + matCopy[i][k]);
                            }
                        }
                    }

                    //Clean up the numbers by snapping them

                    for (int i = 0; i < matCopy.Count; i++)
                    {
                        for (int j = 0; j < matCopy[i].Count; j++)
                        {
                            matCopy[i][j] = DNAMath.snapToUnit(matCopy[i][j], 0.0001f);
                        }
                    }

                    for (int i = 0; i < unitMat.Count; i++)
                    {
                        for (int j = 0; j < unitMat[i].Count; j++)
                        {
                            unitMat[i][j] = DNAMath.snapToUnit(unitMat[i][j], 0.0001f);
                        }
                    }

                    Debug.Log("Final Matrix Inverse");
                    displayMatrix(matCopy, answerColumn);
                    displayMatrix(unitMat, answerColumn);

                    invMat = unitMat;


                }
            }
            else
            {
                //Debug.Log()
                //Matrix is not square
                Debug.Log("Matrix is not square");
            }
        }

        return invMat;

    }


    public float getDeterminant()
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

                // Debug.Log("Determinant : " + det);

            }
            else
            {
                //Matrix is not square
                Debug.Log("Matrix is not square");
            }



        }
        return det;
    }

    public List<List<float>> gaussianedMatrix()
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

    public List<List<float>> getMatrixCopy(List<List<float>> mat)
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

    public List<List<float>> getUnitMatrix(int size)
    {
        List<List<float>> mat = new List<List<float>>();

        for (int i = 0; i < size; i++)
        {
            List<float> row = new List<float>();
            for (int j = 0; j < size; j++)
            {
                if (j == i)
                {
                    row.Add(1);
                }
                else
                {
                    row.Add(0);
                }
            }
            mat.Add(row);
        }

        return mat;
    }

    public void displayMatrix(List<List<float>> mat, List<float> ans)
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


    public void divideRow(List<List<float>> mat, int row, float denom)
    {
        List<float> idk = new List<float>();

        for (int i = 0; i < mat[row].Count; i++)
        {
            // Debug.Log(mat[row][i].GetType());
            // Debug.Log(denom.GetType());

            //idk.Add(divideNum(mat[row][i], denom));

            // Debug.Log(divideNum(mat[row][i], denom));
            // Debug.Log(divideNum(mat[row][i], denom).GetType());

            // Debug.Log(Mathf.Approximately(mat[row][i] / (denom), ((float)mat[row][i]) / ((float)denom)) );


            // Debug.Log((mat[row][i] / denom).GetType());
            //Debug.Log(((float)(mat[row][i] / denom)).GetType());

            mat[row][i] = divideNum(mat[row][i], denom);

            // Debug.Log(mat[row][i].GetType());
        }

        foreach (float i in idk)
        {
            // Debug.Log("Idk " + i);
        }
        // mat[row] = idk;

        foreach (float i in mat[row])
        {
            // Debug.Log("mat " + i);
        }
    }


    public float divideNum(double a, double b)
    {
        // Debug.Log((float)(a / b));
        return (float)(a / b);
    }


    public List<List<float>> matrixMulti(List<List<float>> matA, List<List<float>> matB)
    {
        //Check if right dimensions first

        //Let's assume the case where B is the smaller matrix always

        List<List<float>> outMat = new List<List<float>>();

        if (matA[0].Count == matB.Count)
        {
            //Correct dimensions are met


            //MatA is horizontal
            //MatB is vertical


            //Mat a

            //
            // 1 2 3
            // 4 5 6
            // 7 8 9 
            //


            //Mat B 

            //
            // 1 2
            // 3 4
            // 5 6
            //

            for (int i = 0; i < matB.Count; i++)
            {

                List<float> row = new List<float>();
                for (int j = 0; j < matB[i].Count; j++)
                {

                    //Create the vectors
                    //Vec 1 = vec a
                    List<float> vec1 = new List<float>();
                    List<float> vec2 = new List<float>();

                    vec1 = getVector(matA, false, i);
                    vec2 = getVector(matB, true, j);

                    row.Add(vectorMulti(vec1, vec2));

                }
                outMat.Add(row);
            }
        }

       // displayMatrix(outMat, answerColumn);
        return outMat;
    }

    public float vectorMulti(List<float> Vec1, List<float> Vec2)
    {
        float sum = 0;
        if (Vec1.Count == Vec2.Count)
        {
            for (int i = 0; i < Vec1.Count; i++)
            {
                sum += Vec1[i] * Vec2[i];
            }
        }

        return sum;
    }

    public List<float> getVector(List<List<float>> Mat, bool vert, int number)
    {
        List<float> vec = new List<float>();


        if (vert)
        {
            for (int i = 0; i < Mat.Count; i++)
            {
                vec.Add(Mat[i][number]);
            }


        }
        else
        {
            //Horizontal

            for (int i = 0; i < Mat[number].Count; i++)
            {
                vec.Add(Mat[number][i]);
            }



        }


        return vec;


    }





}
