using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObject
{
    public RectTransform UI;
    public List<UIObject> children;
    public float flex;

    bool layoutGroup;
    bool layoutGroupVert;
    bool square;
    bool fillParent;
    bool dontModify;
    bool customDim;
    bool useChildMulti;


    //x Vals for what one flex unit is equal to in pixels in vertical and horizontal direction
    float hVal;
    float wVal;

    //Applies only to objects Underneath (Children)
    public float padUpFlex;
    public float padDownFlex;
    public float padLeftFlex;
    public float padRightFlex;

    //Applies to self
    public float selfpadUpFlex;
    public float selfpadDownFlex;
    public float selfpadLeftFlex;
    public float selfpadRightFlex;

    //Flex Number
    public float spacingFlex;

    public float childMultiW;
    public float childMultiH;


    public Vector2 size;
    public Vector2 customSize;

    public UIObject(RectTransform UIItem, float flex)
    {
        // Debug.Log(UIItem);
        // Debug.Log(UIItem.sizeDelta);


        this.UI = UIItem;
        this.flex = flex;

        this.size = UIItem.sizeDelta;

        children = new List<UIObject>();

        if (UIItem.gameObject.GetComponent<HorizontalLayoutGroup>())
        {
            layoutGroup = true;
            layoutGroupVert = false;
        }
        else if (UIItem.gameObject.GetComponent<VerticalLayoutGroup>())
        {
            layoutGroup = true;
            layoutGroupVert = true;
        }
        else
        {
            layoutGroup = false;
        }
    }

    public void addChild(UIObject child)
    {
        children.Add(child);
    }

    public void setSpacingFlex(float flex)
    {
        spacingFlex = flex;
    }

    public void setHorizontalPadding(float leftFlex, float rightFlex)
    {
        padLeftFlex = leftFlex;
        padRightFlex = rightFlex;
    }

    public void setVerticalPadding(float upFlex, float downFlex)
    {
        padUpFlex = upFlex;
        padDownFlex = downFlex;
    }

    public void setSelfHorizontalPadding(float leftFlex, float rightFlex)
    {
        selfpadLeftFlex = leftFlex;
        selfpadRightFlex = rightFlex;
    }

    public void setSelfVerticalPadding(float upFlex, float downFlex)
    {
        selfpadUpFlex = upFlex;
        selfpadDownFlex = downFlex;
    }

    public void setSize(Vector2 thisSize)
    {
        //Debug.Log(UI);
        if (dontModify)
        {
            // Debug.Log("Here");
            // Debug.Log(size);
            // Debug.Log(UI.sizeDelta);

            size = UI.sizeDelta;
        }
        else if (customDim)
        {
            if (customSize.magnitude > 0)
            {

                //Use Custom Size
                //Debug.Log("here");
                size = customSize;
            }
            else
            {
                defaultMethod(thisSize);
            }

        }
        else if (useChildMulti)
        {
            //Apply child Multi
            if (childMultiH != 0)
            {
                size = new Vector2(defaultWidthCalc(thisSize.x), children.Count * childMultiH);
            }
            if (childMultiW != 0)
            {
                Debug.Log("Hello");
                size = new Vector2(children.Count * childMultiW, defaultHeightCalc(thisSize.y));
            }

        }
        else
        {

            defaultMethod(thisSize);
        }
        //Recalc size using new Equations and self padding

        if (layoutGroupVert)
        {
            //Calc Horizontal first
            wVal = solveW();
            hVal = solveH();
        }
        else
        {
            //Calc vertical first
            hVal = solveH();
            wVal = solveW();
        }

        if (layoutGroup)
        {
            if (layoutGroupVert)
            {
                UI.gameObject.GetComponent<VerticalLayoutGroup>().padding.top = (int)(padUpFlex * hVal);
                UI.gameObject.GetComponent<VerticalLayoutGroup>().padding.bottom = (int)(padDownFlex * hVal);
                UI.gameObject.GetComponent<VerticalLayoutGroup>().padding.left = (int)(padLeftFlex * wVal);
                UI.gameObject.GetComponent<VerticalLayoutGroup>().padding.right = (int)(padRightFlex * wVal);

                UI.gameObject.GetComponent<VerticalLayoutGroup>().spacing = (int)((spacingFlex * hVal) / (children.Count - 1));

            }
            else
            {
                UI.gameObject.GetComponent<HorizontalLayoutGroup>().padding.top = (int)(padUpFlex * hVal);
                UI.gameObject.GetComponent<HorizontalLayoutGroup>().padding.bottom = (int)(padDownFlex * hVal);
                UI.gameObject.GetComponent<HorizontalLayoutGroup>().padding.left = (int)(padLeftFlex * wVal);
                UI.gameObject.GetComponent<HorizontalLayoutGroup>().padding.right = (int)(padRightFlex * wVal);

                UI.gameObject.GetComponent<HorizontalLayoutGroup>().spacing = (int)((spacingFlex * wVal) / (children.Count - 1));
            }
        }



        //Set current object and all children
        UI.sizeDelta = size;

        //Loop through children
        for (int i = 0; i < children.Count; i++)
        {
            if (layoutGroupVert)
            {
                if (children[i].fillParent)
                {
                    children[i].setSize(size);
                }
                else if (children[i].square)
                {

                    //Set wVal
                    // Debug.Log("Square Vert");
                    children[i].setSize(new Vector2(wVal, wVal));

                }
                else
                {
                    children[i].setSize(new Vector2(wVal, hVal * children[i].flex));
                }

                //Debug.Log(children[i].UI);
                // Debug.Log(children[i].size);
            }
            else
            {

                if (children[i].fillParent)
                {
                    children[i].setSize(size);
                }
                else if (children[i].square)
                {
                    // Debug.Log("Setting child with square value");

                    // Debug.Log("Square Hor");
                    //Set hVal
                    children[i].setSize(new Vector2(hVal, hVal));
                    // }
                }
                else
                {
                    children[i].setSize(new Vector2(wVal * children[i].flex, hVal));
                }

                // Debug.Log(children[i].UI);
                // Debug.Log(children[i].size);
            }
        }

        //Now this should theoretically be ready to test
    }

    public float solveH()
    {
        //Vertical
        Equation eqY = new Equation();

        //Add padding flexes
        eqY.addPolynomial2(padUpFlex, 1);
        eqY.addPolynomial2(padDownFlex, 1);

        //Add spacing Flexes
        if (layoutGroupVert && layoutGroup)
        {
            eqY.addPolynomial2(spacingFlex, 1);
        }

        //Add children flexes
        if (layoutGroupVert)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].square)
                {
                    //Debug.Log("Child is square");

                    // Debug.Log("Make equal to width");
                    // Debug.Log(wVal);
                    // Debug.Log(size);
                    eqY.addPolynomial2(Mathf.Min(size.y, size.x), 0); //wVal

                }
                else
                {
                    eqY.addPolynomial2(children[i].flex, 1);
                }
            }
        }
        else
        {
            eqY.addPolynomial2(1, 1);
        }


        return eqY.solveSingleEQ(size.y);
    }

    public float solveW()
    {
        Equation eqX = new Equation();

        //Adding padding Flexes 
        eqX.addPolynomial2(padLeftFlex, 1);
        eqX.addPolynomial2(padRightFlex, 1);

        //Add spacing flex
        if (!layoutGroupVert && layoutGroup)
        {
            eqX.addPolynomial2(spacingFlex, 1);
        }
        //Add children
        if (!layoutGroupVert)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].square)
                {
                    // Debug.Log("Child is square");
                    // Debug.Log("Make equal to height");
                    // Debug.Log(hVal);
                    //  Debug.Log(size);
                    // Debug.Log(Mathf.Min(size.y, size.x));
                    eqX.addPolynomial2(Mathf.Min(size.y, size.x), 0); //hval
                }
                else
                {
                    eqX.addPolynomial2(children[i].flex, 1);
                }

            }
        }
        else
        {
            eqX.addPolynomial2(1, 1);
        }

        return eqX.solveSingleEQ(size.x);
    }

    //Next week or if we get to use this create a system that allows you to make a blueprint or prefab type thing and then you can input a list of rectTransforms to instance which things will be affected

    public static UIObject newObj(RectTransform UI, float flex)
    {
        return new UIObject(UI, flex);
    }

    public void setSquare()
    {
        square = true;
    }

    public static RectTransform getChildRect(RectTransform rect, int childNum)
    {
        return rect.gameObject.transform.GetChild(childNum).GetComponent<RectTransform>();
    }

    public void setLayoutType(bool Vert)
    {
        layoutGroupVert = Vert;
    }

    public void setFillParent(bool fill)
    {
        fillParent = fill;
    }

    public void setAllPadSame(float pad)
    {
        //Add a option to do it in pixels for all padding and spacing for later
        padUpFlex = pad;
        padDownFlex = pad;
        padLeftFlex = pad;
        padRightFlex = pad;
    }

    public void setDontModify()
    {
        dontModify = true;
    }

    public void setCustomSize(Vector2 size)
    {
        customDim = true;
        customSize = size;
    }

    public void setChildMulti(float WidthMulti, float HeightMulti)
    {
        useChildMulti = true;
        childMultiW = WidthMulti;
        childMultiH = HeightMulti;
    }


    public void defaultMethod(Vector2 thisSize)
    {
        // size = new Vector2(width.solveX(thisSize.x), height.solveX(thisSize.y));

        size = new Vector2(defaultWidthCalc(thisSize.x), defaultHeightCalc(thisSize.y));
    }

    public float defaultWidthCalc(float fullSize)
    {
        Equation width = new Equation();
        //Padding
        width.addPolynomial2(selfpadLeftFlex, 1);
        width.addPolynomial2(selfpadRightFlex, 1);
        //Self flex
        width.addPolynomial2(1, 1);

        return width.solveSingleEQ(fullSize);
    }

    public float defaultHeightCalc(float fullSize)
    {
        Equation height = new Equation();
        //Padding
        height.addPolynomial2(selfpadUpFlex, 1);
        height.addPolynomial2(selfpadDownFlex, 1);
        //Self flex
        height.addPolynomial2(1, 1);


        return height.solveSingleEQ(fullSize);
    }





}
