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

    public bool fillHorizontal;
    public bool fillVertical;


    //Applies only to objects with 
    public float padUpFlex;
    public float padDownFlex;
    public float padLeftFlex;
    public float padRightFlex;

    public float spacingFlex;

    public Vector2 size;

    public UIObject (RectTransform UIItem, float flex)
    {

        this.UI = UIItem;
        this.flex = flex;
       


        children = new List<UIObject>();

        if (UIItem.gameObject.GetComponent<HorizontalLayoutGroup>())
        {
            layoutGroup = true;
            layoutGroupVert = false;
        } else if  (UIItem.gameObject.GetComponent<VerticalLayoutGroup>())
        {
            layoutGroup = true;
            layoutGroupVert = true;
        } else
        {
            layoutGroup = false;
        }
    }

    public void addChild (UIObject child)
    {
        children.Add(child);
    }

    public void setSpacingFlex (float flex)
    {
        spacingFlex = flex;
    }

    public void setHorizontalPadding (float leftFlex, float rightFlex)
    {
        padLeftFlex = leftFlex;
        padRightFlex = rightFlex;
    }

    public void setVerticalPadding (float upFlex, float downFlex)
    {
        padUpFlex = upFlex;
        padDownFlex = downFlex;
    }

    public void setSize (Vector2 thisSize)
    {

        size = thisSize;

        //Set the size and all the children

        //UI.sizeDelta = size;

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
                eqY.addPolynomial2(children[i].flex, 1);
            }
        } else
        {
            eqY.addPolynomial2(1, 1);
        }
       

        float hVal = eqY.solveX(size.y);


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
                eqX.addPolynomial2(children[i].flex, 1);
            }
        } else
        {
            eqX.addPolynomial2(1, 1);
        }

        float wVal = eqX.solveX(size.x);


        if (layoutGroup)
        {
            if (layoutGroupVert)
            {
                UI.gameObject.GetComponent<VerticalLayoutGroup>().padding.top =  (int)(padUpFlex * hVal);
                UI.gameObject.GetComponent<VerticalLayoutGroup>().padding.bottom = (int)(padDownFlex * hVal);
                UI.gameObject.GetComponent<VerticalLayoutGroup>().padding.left = (int)(padLeftFlex * wVal);
                UI.gameObject.GetComponent<VerticalLayoutGroup>().padding.right = (int)(padRightFlex * wVal);

                UI.gameObject.GetComponent<VerticalLayoutGroup>().spacing = (int)((spacingFlex * hVal)/(children.Count-1));

            } else
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
        for (int i = 0; i < children.Count; i ++)
        {
            if (layoutGroupVert)
            {
                children[i].setSize(new Vector2(wVal, hVal*children[i].flex));
            } else
            {
                children[i].setSize(new Vector2(wVal * children[i].flex, hVal));
            }
        }

        //Now this should theoretically be ready to test




    }

}