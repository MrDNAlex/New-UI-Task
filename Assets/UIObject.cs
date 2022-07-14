using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObject 
{
  public RectTransform UI;
    public List<UIObject> children;
    public float vertFlex;
    public float horFlex;

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

    public UIObject (RectTransform UIItem, float vertFlex, float horFlex)
    {

        this.UI = UIItem;
        this.vertFlex = vertFlex;
        this.horFlex = horFlex;


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
        for (int i = 0; i < children.Count; i ++)
        {
            eqY.addPolynomial2(children[i].vertFlex, 1);
        }

        float hVal = eqY.solveX(size.y);




        float wVal = 0;


        if (layoutGroup)
        {
            if (layoutGroupVert)
            {
                UI.gameObject.GetComponent<VerticalLayoutGroup>().padding.top =  (int)(padUpFlex * hVal);
                UI.gameObject.GetComponent<VerticalLayoutGroup>().padding.bottom = (int)(padDownFlex * hVal);
            } else
            {
                UI.gameObject.GetComponent<VerticalLayoutGroup>().padding.left = (int)(padLeftFlex * wVal);
                UI.gameObject.GetComponent<VerticalLayoutGroup>().padding.right = (int)(padRightFlex * wVal);
            }
        }


        //Horizontal

        //IDK
        //Well
        






    }

}