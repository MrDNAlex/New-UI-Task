using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [Header("Main Panel")]
    [SerializeField] RectTransform mainPanel;
    [SerializeField] RectTransform buttons;
    [SerializeField] RectTransform play;
    [SerializeField] RectTransform settings;
    [SerializeField] RectTransform quit;
    [SerializeField] RectTransform title;

    [SerializeField] RectTransform help;

    [SerializeField] VerticalLayoutGroup buttonsLayout;

    [Header("Settings Panel")]
    [SerializeField] RectTransform settingsPanel;

    [SerializeField] RectTransform settingsSec;
    [SerializeField] RectTransform settingTitle;
    [SerializeField] RectTransform setting1;
    [SerializeField] RectTransform setting2;
    [SerializeField] RectTransform volumeTitle;
    [SerializeField] RectTransform volumeSlide;
    [SerializeField] RectTransform settingsBack;

    [SerializeField] VerticalLayoutGroup settingsBtnLayout;

    [Header("Play Panel")]
    [SerializeField] RectTransform playPanel;
    [SerializeField] RectTransform saveButtons;
    [SerializeField] RectTransform saveTitle;
    [SerializeField] RectTransform saveSaves;
    [SerializeField] RectTransform save1;
    [SerializeField] RectTransform save2;
    [SerializeField] RectTransform save3;
    [SerializeField] RectTransform saveBack;

    [SerializeField] HorizontalLayoutGroup savesLayout;
    [SerializeField] VerticalLayoutGroup savesButtonLayout;





    [Header("Modifyable Values")]
    [SerializeField] float commonFactor;
    [SerializeField] float secondFactor;
    [SerializeField] float menuFillFac;
    [SerializeField] float settingsFillFac;
    [SerializeField] float subTextFac;
    [SerializeField] float titleFac;


    [Header("Flexes")]
    [SerializeField] float titleFlex;
    [SerializeField] float buttonFlex;
    [SerializeField] float padUpFlex;
    [SerializeField] float padDownFlex;
    [SerializeField] float padLeftFlex;
    [SerializeField] float padRightFlex;
    [SerializeField] float spacingFlex;


    // Start is called before the first frame update
    void Start()
    {
        setUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUI ()
    {
        /*

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

       // float panelWidth = screenWidth * commonFactor;
      //  float panelHeight = screenHeight * commonFactor;

        //Main panel stuff 

        float buttonsSecHeight = screenHeight * commonFactor;
        float buttonsSecWidth = screenWidth * commonFactor;

        float buttonFill = menuFillFac;

        float textHeight = buttonsSecHeight * 0.2f;

        float buttonHeight = ((buttonsSecHeight * buttonFill) - textHeight) / buttons.gameObject.transform.childCount;
        float spacing = (buttonsSecHeight - (textHeight + buttonsSecHeight * buttonFill))/ (buttons.gameObject.transform.childCount - 1);

        buttonsLayout.spacing = spacing;

        buttons.sizeDelta = new Vector2(buttonsSecWidth, buttonsSecHeight);

        play.sizeDelta = new Vector2(buttonsSecWidth, buttonHeight);
        settings.sizeDelta = new Vector2(buttonsSecWidth, buttonHeight);
        quit.sizeDelta = new Vector2(buttonsSecWidth, buttonHeight);

        help.sizeDelta = new Vector2(buttonHeight, buttonHeight);

        title.sizeDelta = new Vector2(buttonsSecWidth, textHeight);

        settings.gameObject.GetComponent<Button>().onClick.AddListener(settingsFunc);
        play.gameObject.GetComponent<Button>().onClick.AddListener(menuToPlay);
        */



        //Settings
       //  settingsUI();

        //saveUI();

                settingsFlex();
              saveFlex();

            equation();

        //testEq2();
      //  solveCircuit();
    }

    public void settingsUI ()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float secHeight = screenHeight * commonFactor;
        float SecWidth = screenWidth * commonFactor;

        float everythingWidth = SecWidth * commonFactor;

        
        float settingsBtnHeight = (secHeight * settingsFillFac) / 6;

        float settingsTitleHeight = ((secHeight - (secHeight * settingsFillFac)) * titleFac) /2;

        float settingsSpacing = (secHeight - (settingsTitleHeight + (settingsBtnHeight * 4) + (settingsTitleHeight * subTextFac))) / settingsSec.gameObject.transform.childCount - 1;

        settingsBtnLayout.spacing = settingsSpacing;

        settingsSec.sizeDelta = new Vector2(SecWidth, secHeight);

        settingTitle.sizeDelta = new Vector2(everythingWidth, settingsTitleHeight);
        setting1.sizeDelta = new Vector2(everythingWidth, settingsBtnHeight);
        setting2.sizeDelta = new Vector2(everythingWidth, settingsBtnHeight);
        volumeTitle.sizeDelta = new Vector2(everythingWidth, settingsTitleHeight * subTextFac);
        volumeSlide.sizeDelta = new Vector2(everythingWidth, settingsBtnHeight);
        settingsBack.sizeDelta = new Vector2(everythingWidth, settingsBtnHeight);

        //Handle size
        RectTransform handle = volumeSlide.gameObject.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>();
        handle.sizeDelta = new Vector2(settingsBtnHeight, 0);

        settingsBack.gameObject.GetComponent<Button>().onClick.AddListener(backSettToMenu);

    }

    public void settingsFlex()
    {

        UIObject settingsSecUI = new UIObject(settingsSec, 1);

        UIObject setTitle = new UIObject(settingTitle, titleFlex);
        UIObject setBTN1 = new UIObject(setting1, buttonFlex);
        UIObject setBTN2 = new UIObject(setting2, buttonFlex);
        UIObject setVolTit= new UIObject(volumeTitle, titleFlex);
        UIObject setVol = new UIObject(volumeSlide, buttonFlex);
        UIObject setBack = new UIObject(settingsBack, buttonFlex);

        settingsSecUI.setHorizontalPadding(padLeftFlex, padRightFlex);
        settingsSecUI.setVerticalPadding(padUpFlex, padDownFlex);

        settingsSecUI.setSpacingFlex(spacingFlex);

        settingsSecUI.addChild(setTitle);
        settingsSecUI.addChild(setBTN1);
        settingsSecUI.addChild(setBTN2);
        settingsSecUI.addChild(setVolTit);
        settingsSecUI.addChild(setVol);
        settingsSecUI.addChild(setBack);

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        settingsSecUI.setSize(new Vector2(screenWidth * 0.8f, screenHeight * 0.8f));

        //Handle size
        RectTransform handle = volumeSlide.gameObject.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>();
        handle.sizeDelta = new Vector2(setVol.size.y, 0);

        settingsBack.gameObject.GetComponent<Button>().onClick.AddListener(backSettToMenu);


    }

    public void settingsFunc ()
    {
        mainPanel.gameObject.SetActive(false);

        settingsPanel.gameObject.SetActive(true);
    }

    public void backSettToMenu ()
    {
        settingsPanel.gameObject.SetActive(false);
        mainPanel.gameObject.SetActive(true);
    }

    public void saveUI ()
    {

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float secHeight = screenHeight * commonFactor;
        float secWidth = screenWidth * commonFactor;

        float saveSlotH = secHeight * secondFactor;
        float saveSlotW = secWidth * commonFactor / 3;

        float titleH = (secHeight - saveSlotH) / 3;
        float buttonH = (secHeight - saveSlotH) / 3;
        float everythingWidth = secWidth * secondFactor;
        float btnsSpacing = (secHeight - (saveSlotH + titleH + buttonH)) / 2;

        float spacing = (secWidth - (secWidth * commonFactor))/2;

        float imageDim = saveSlotW;

        float textH = (saveSlotH - imageDim) / 3;


        saveButtons.sizeDelta = new Vector2(secWidth, secHeight);

        saveTitle.sizeDelta = new Vector2(secWidth, titleH);
        saveSaves.sizeDelta = new Vector2(secWidth, saveSlotH);
        //Saves
        save1.sizeDelta = new Vector2(saveSlotW, saveSlotH);

        save1.gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(imageDim, imageDim);
        save1.gameObject.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(imageDim, saveSlotH - imageDim);
        save1.gameObject.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(imageDim, textH);
        save1.gameObject.transform.GetChild(1).GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(imageDim, textH);
        save1.gameObject.transform.GetChild(1).GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(imageDim, textH);

        //Save 2
        save2.sizeDelta = new Vector2(saveSlotW, saveSlotH);

        save2.gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(imageDim, imageDim);
        save2.gameObject.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(imageDim, saveSlotH - imageDim);
        save2.gameObject.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(imageDim, textH);
        save2.gameObject.transform.GetChild(1).GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(imageDim, textH);
        save2.gameObject.transform.GetChild(1).GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(imageDim, textH);

        //Save 3
        save3.sizeDelta = new Vector2(saveSlotW, saveSlotH);

        save3.gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(imageDim, imageDim);
        save3.gameObject.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(imageDim, saveSlotH - imageDim);
        save3.gameObject.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(imageDim, textH);
        save3.gameObject.transform.GetChild(1).GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(imageDim, textH);
        save3.gameObject.transform.GetChild(1).GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(imageDim, textH);

        savesLayout.spacing = spacing;
        savesButtonLayout.spacing = btnsSpacing;

        saveBack.sizeDelta = new Vector2(saveSlotH, buttonH);

        saveBack.gameObject.GetComponent<Button>().onClick.AddListener(saveToMenu);

    }

    public void saveFlex ()
    {
        UIObject saveBTNLayout = new UIObject(saveButtons, 1);

        //Set padding for this and stuff

        saveBTNLayout.setSpacingFlex(3);

        UIObject saveTit = new UIObject(saveTitle, 2);
        UIObject saveSecs = new UIObject(saveSaves, 6);
        UIObject saveB = new UIObject(saveBack, 1);

      
        //Develop a branching system maybe that grabs all children underneath for the UI
      
        saveSecs.addChild(SaveCard(save1, 3, 1.5f, 1));
       // saveSecs.addChild(SaveCard(save2, 3, 1.5f, 1));
       // saveSecs.addChild(SaveCard(save3, 3, 1.5f, 1));

        
        saveSecs.addChild(SaveCard(save2,2, 0.5f, 1));
        saveSecs.addChild(SaveCard(save3, 5, 1.5f, 1));

       // saveSecs.setSpacingFlex(1);

        saveBTNLayout.addChild(saveTit);
        saveBTNLayout.addChild(saveSecs);
        saveBTNLayout.addChild(saveB);

        saveBTNLayout.setSize(new Vector2(1000, 800));

        saveBack.gameObject.GetComponent<Button>().onClick.AddListener(saveToMenu);

    }

    public UIObject SaveCard (RectTransform save, float imageFlex, float textFlex, float text2flex)
    {
        UIObject saveItem = new UIObject(save, 1);


        //Ok there seems to be problems with setSquare with this new Method of things



        //saveItem.setSquare();

        saveItem.setHorizontalPadding(0.1f, 0.1f);

        UIObject image = new UIObject(save.gameObject.transform.GetChild(0).GetComponent<RectTransform>(), imageFlex);
        UIObject textSec = new UIObject(save.gameObject.transform.GetChild(1).GetComponent<RectTransform>(), textFlex);

        UIObject Name = new UIObject(save.gameObject.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>(), text2flex);
        UIObject LastP = new UIObject(save.gameObject.transform.GetChild(1).GetChild(1).GetComponent<RectTransform>(), text2flex);
        UIObject Prog = new UIObject(save.gameObject.transform.GetChild(1).GetChild(2).GetComponent<RectTransform>(), text2flex);

        image.setSquare();

        textSec.addChild(Name);
        textSec.addChild(LastP);
        textSec.addChild(Prog);

        saveItem.addChild(image);
        saveItem.addChild(textSec);

        return saveItem;
    }


    public void saveToMenu ()
    {
        playPanel.gameObject.SetActive(false);

        mainPanel.gameObject.SetActive(true);
    }

    public void menuToPlay()
    {
        mainPanel.gameObject.SetActive(false);
        playPanel.gameObject.SetActive(true);
    }



    public void equation ()
    {

        /*
        Equation test = new Equation();

        Polynomial p1 = new Polynomial(1, 1);
        Polynomial p2 = new Polynomial(2, 1);
        Polynomial p3 = new Polynomial(2, 1);
        Polynomial p4 = new Polynomial(1, 1);

        test.addPolynomial(p1);
        test.addPolynomial(p2);
        test.addPolynomial(p3);
        test.addPolynomial(p4);

        Debug.Log(test.solveX(200));

        Equation test2 = new Equation();

        Polynomial t2p1 = new Polynomial(1, 1);
        Polynomial t2p2 = new Polynomial(2, 1);
        Polynomial t2p3 = new Polynomial(3, 1);
        //Polynomial t2p4 = new Polynomial(1, 1);

        test2.addPolynomial(t2p1);
        test2.addPolynomial(t2p2);
        test2.addPolynomial(t2p3);
       // test.addPolynomial(t2p4);

        Debug.Log(test2.solveX(300));

        */


        UIObject panelUI = new UIObject(buttons, 1);

        UIObject titleUI = new UIObject(title, titleFlex);
        UIObject playUI = new UIObject(play,buttonFlex);
        UIObject settingsUI = new UIObject(settings, buttonFlex);
        UIObject quitUI = new UIObject(quit, buttonFlex);


        panelUI.addChild(titleUI);
        panelUI.addChild(playUI);
        panelUI.addChild(settingsUI);
        panelUI.addChild(quitUI);
        
        panelUI.setVerticalPadding(padUpFlex, padDownFlex);

        panelUI.setHorizontalPadding(padLeftFlex, padRightFlex);
        panelUI.setSpacingFlex(spacingFlex);

        panelUI.setSize(new Vector2(1000, 800));

        settings.gameObject.GetComponent<Button>().onClick.AddListener(settingsFunc);
        play.gameObject.GetComponent<Button>().onClick.AddListener(menuToPlay);
    }

    public void testEq ()
    {

        Equation test = new Equation();

        Polynomial p1 = new Polynomial(3, 1);
        Polynomial p2 = new Polynomial(4, 1);
        Polynomial p3 = new Polynomial(2, 1);
        Polynomial p4 = new Polynomial(6, 1);
        Polynomial p5 = new Polynomial(3, 0);

        test.addPolynomial(p1);
        test.addPolynomial(p2);
        test.addPolynomial(p3);
        test.addPolynomial(p4);
        test.addPolynomial(p5);

        test.polyClean();
        for (int i = 0; i < test.cleanPoly.Count; i ++)
        {
            Debug.Log("Poly " + i + ": " + test.cleanPoly[i].coefficient);
        }

        Debug.Log(test.solveSingleEQ(20));


    }

    public void testEq2 ()
    {
        Equation2 testEq = new Equation2();

        //x = 6
        //y = 2

        /*
        Polynomial2 p1 = new Polynomial2(10, 0, "x");
       // Polynomial2 p2 = new Polynomial2(10, 0, "y");
        Polynomial2 p3 = new Polynomial2(5, 1, "x");
        Polynomial2 p4 = new Polynomial2(2, 2, "x");
        //Polynomial2 p5 = new Polynomial2(-3, 1, "y");
       // Polynomial2 p6 = new Polynomial2(4, 2, "y");
        */

        /*
        Polynomial2 p1 = new Polynomial2(3, 1, "x"); //X = 12, y = 11, z = 10
        Polynomial2 p2 = new Polynomial2(5, 1, "x");
        Polynomial2 p3 = new Polynomial2(4, 1, "x");
        Polynomial2 p4 = new Polynomial2(5, 1, "y");
        Polynomial2 p5 = new Polynomial2(3, 1, "y");
        Polynomial2 p6 = new Polynomial2(3, 1, "y");
        Polynomial2 p7 = new Polynomial2(8, 1, "z");
        Polynomial2 p8 = new Polynomial2(2, 1, "z");

        Polynomial2 p9 = new Polynomial2(3, 2, "x"); // x= 5
        Polynomial2 p10 = new Polynomial2(2, 2, "x"); // y = 9
        Polynomial2 p11 = new Polynomial2(7, 2, "y");
        Polynomial2 p12 = new Polynomial2(2, 2, "y");

        Polynomial2 p13 = new Polynomial2(1, 0, "x"); // x = 6
        Polynomial2 p14 = new Polynomial2(2, 0, "x");
        Polynomial2 p15 = new Polynomial2(3, 0, "x"); // y = 8
        Polynomial2 p16 = new Polynomial2(4, 0, "y");
        Polynomial2 p17 = new Polynomial2(4, 0, "y");
        
        */


        /*

        testEq.addPolynomial(p1);
        testEq.addPolynomial(p2);
        testEq.addPolynomial(p3);
        testEq.addPolynomial(p4);
         testEq.addPolynomial(p5);
         testEq.addPolynomial(p6);

        
        testEq.addPolynomial(p7);
        testEq.addPolynomial(p8);
        testEq.addPolynomial(p9);
        testEq.addPolynomial(p10);
        testEq.addPolynomial(p11);
        testEq.addPolynomial(p12);
        testEq.addPolynomial(p13);
        testEq.addPolynomial(p14);
        testEq.addPolynomial(p15);
        testEq.addPolynomial(p16);
        testEq.addPolynomial(p17);


        */



        // List<PolyOutput> values = new List<PolyOutput>();

        // values.Add(new PolyOutput("x", 6));
        // values.Add(new PolyOutput("y", 2));
        /*
        for (int i = 0; i < testEq.polynomials.Count; i++)
        {
            Debug.Log("Poly " + i + " : " + testEq.polynomials[i].coefficient + testEq.polynomials[i].variable + " Pow : " + testEq.polynomials[i].power);
        }

          testEq.polyClean();


        for (int i = 0; i < testEq.cleanPoly.Count; i++)
        {
            Debug.Log("Poly " + i + " : " + testEq.cleanPoly[i].coefficient + testEq.cleanPoly[i].variable + " Pow : " + testEq.cleanPoly[i].power);
        }
        */

        //testEq.output(values);
        /*
                testEq.derive();

                Debug.Log("Derived Version");
                for (int i = 0; i < testEq.cleanPoly.Count; i++)
                {
                    Debug.Log("Poly " + i + " : " + testEq.cleanPoly[i].coefficient + testEq.cleanPoly[i].variable + " Pow : " + testEq.cleanPoly[i].power);
                }


                Debug.Log(testEq.solveX(20));
              */


        //
        //Alright to solve this we will need to make a matrix class
        //We will need to make a multivariable equation solver function (using multivariable Newton raphson) 

        
        Equation eq1 = new Equation();


        Polynomial p1 = new Polynomial(3f, 1, "x");
        //Polynomial p2 = new Polynomial(4f, 1, "x");
        Polynomial p3 = new Polynomial(-1f, 1, "y");//2
        //Polynomial p4 = new Polynomial(3f, 1, "y");//3
       // Polynomial p5 = new Polynomial(4f, 1, "z");//4
        Polynomial p6 = new Polynomial(0f, 1, "z");//5
        Polynomial p7 = new Polynomial(-10f, 0, "x");
       // Polynomial p8 = new Polynomial(2f, 0, "x");


        //6x, 11y, 5

        eq1.addPolynomial(p1);
       // eq1.addPolynomial(p2);
        eq1.addPolynomial(p3);
       // eq1.addPolynomial(p4);
        //eq1.addPolynomial(p5);
        eq1.addPolynomial(p6);
        eq1.addPolynomial(p7);
       // eq1.addPolynomial(p8);

        eq1.polyClean();


        Equation eq2 = new Equation();

        Polynomial e2p1 = new Polynomial(-2f, 1, "x");//4
        Polynomial e2p2 = new Polynomial(3f, 1, "y");
        Polynomial e2p3 = new Polynomial(-1f, 1, "z");//3
        Polynomial e2p4 = new Polynomial(0f, 0, "x");

        eq2.addPolynomial(e2p1);
        eq2.addPolynomial(e2p2);
        eq2.addPolynomial(e2p3);
        eq2.addPolynomial(e2p4);


        eq2.polyClean();

        Equation eq3 = new Equation();

        Polynomial e3p1 = new Polynomial(0f, 1, "x");//13
        Polynomial e3p2 = new Polynomial(-1.5f, 1, "y");//15
        Polynomial e3p3 = new Polynomial(2.5f, 1, "z");
        Polynomial e3p4 = new Polynomial(0f, 0, "x");

        eq3.addPolynomial(e3p1);
        eq3.addPolynomial(e3p2);
        eq3.addPolynomial(e3p3);
        eq3.addPolynomial(e3p4);

        eq3.polyClean();
        
        /*
        for (int i = 0; i < eq1.cleanPoly.Count; i++)
        {
            Debug.Log("Poly " + i + " : " + eq1.cleanPoly[i].coefficient + eq1.cleanPoly[i].variable + " Pow : " + eq1.cleanPoly[i].power);
        }

        eq1.polyClean();

        for (int i = 0; i < eq1.cleanPoly.Count; i++)
        {
            Debug.Log("Poly " + i + " : " + eq1.cleanPoly[i].coefficient + eq1.cleanPoly[i].variable + " Pow : " + eq1.cleanPoly[i].power);
        }
        */

        //eq1.setEquation(eq1.cleanPoly);
       // Equation eq1 = createRandomEQ();
       // Equation eq2 = createRandomEQ();
       // Equation eq3 = createRandomEQ();
       // Equation eq4 = createRandomEQ();
        //  Equation eq2 = eq1;

      
        // mat.Add(eq4);

      





        /*
        Matrix matrix = new Matrix(derMat);


        List<List<float>> mat2 = new List<List<float>>();


        for (int i = 0; i < 3; i ++)
        {
            List<float> row = new List<float>();

            row.Add(i);

            mat2.Add(row);
        }
        */

     //   matrix.displayMatrix(matrix.matrix, matrix.answerColumn);
     //   matrix.displayMatrix(mat2, matrix.answerColumn);


      //  List<List<float>> multiMat = matrix.matrixMulti(matrix.matrix, mat2);


      //  matrix.displayMatrix(multiMat, matrix.answerColumn);

        Debug.Log("Reach the end");


        List<Equation> mat = new List<Equation>();
        mat.Add(eq1);
        mat.Add(eq2);
        mat.Add(eq3);

        List<Equation> derMat = derived();

     



        float diffPerX;
        float diffPerY;
        float diffPerZ;

        float newX = 0;
        float newY = 0;
        float newZ = 0;


        //Initial Guesses
        float x = 10;
        float y = 10;
        float z = 10;



        //Create matrix 
        Matrix invMatrix = new Matrix(derMat);

        Matrix regMat = new Matrix(mat);

        Debug.Log("Matrix");
        regMat.displayMatrix(regMat.matrix, regMat.answerColumn);
       


        for (int i = 0; i < 100; i ++)
        {
            //This will act as a vector
            List<List<float>> outputVec = new List<List<float>>();

            List<PolyOutput> vals = new List<PolyOutput>();
            vals.Add(new PolyOutput("x", x));
            vals.Add(new PolyOutput("y", y));
            vals.Add(new PolyOutput("z", z));
           

            for (int j = 0; j < 3; j ++)
            {
                List<float> row = new List<float>();
                mat[j].displayAllPoly();
                Debug.Log(mat[j].output(vals));
                row.Add(mat[j].output(vals));
                outputVec.Add(row);
            }

            // invMatrix.displayMatrix(outputVec, invMatrix.answerColumn);
            Debug.Log("Matrix Multi");
            invMatrix.displayMatrix(invMatrix.matrixMulti(invMatrix.inverseMat, outputVec), invMatrix.answerColumn);
            newX = x - invMatrix.matrixMulti(invMatrix.inverseMat, outputVec)[0][0];
            newY = y - invMatrix.matrixMulti(invMatrix.inverseMat, outputVec)[1][0];
            newZ = z - invMatrix.matrixMulti(invMatrix.inverseMat, outputVec)[2][0];



            diffPerX = Mathf.Abs((((float)newX - (float)x) / (float)newX) * 100);
            diffPerY = Mathf.Abs((((float)newY - (float)y) / (float)newY) * 100);
            diffPerZ = Mathf.Abs((((float)newZ - (float)z) / (float)newZ) * 100);



           // Debug.Log(diffPerX + " | " + diffPerY + " | " + diffPerZ + " | ");

            x = newX;
            y = newY;
            z = newZ;

        }

        Debug.Log("X: " + x);
        Debug.Log("Y: " + y);
        Debug.Log("Z: " + z);


        //Oh right, completely forgot, we need to grab the partial derivatives




        /*
        Equation EQ = new Equation();


        EQ.addPolynomial(new Polynomial(3, 1, "x"));
        EQ.addPolynomial(new Polynomial(5, 2, "x"));
        EQ.addPolynomial(new Polynomial(5, 2, "y"));

        Equation partial = EQ.deriveWithRespect("x");

        partial.displayAllPoly();
        */





        



    }


    public List<Equation> derived ()
    {
        Equation eq1 = new Equation();


        Polynomial p1 = new Polynomial(3f, 1, "x");
       // Polynomial p2 = new Polynomial(4f, 1, "x");
        Polynomial p3 = new Polynomial(0f, 1, "y");//2
        Polynomial p4 = new Polynomial(0f, 1, "y");//3
        Polynomial p5 = new Polynomial(0f, 1, "z");//4
        Polynomial p6 = new Polynomial(0f, 1, "z");//5
      //  Polynomial p7 = new Polynomial(3f, 0, "x");
      //  Polynomial p8 = new Polynomial(2f, 0, "x");


        //6x, 11y, 5

        eq1.addPolynomial(p1);
       // eq1.addPolynomial(p2);
        eq1.addPolynomial(p3);
        eq1.addPolynomial(p4);
        eq1.addPolynomial(p5);
        eq1.addPolynomial(p6);
        //eq1.addPolynomial(p7);
        //eq1.addPolynomial(p8);

        eq1.polyClean();


        Equation eq2 = new Equation();

        Polynomial e2p1 = new Polynomial(0f, 1, "x");//4
        Polynomial e2p2 = new Polynomial(3f, 1, "y");
        Polynomial e2p3 = new Polynomial(0f, 1, "z");//3
       // Polynomial e2p4 = new Polynomial(0f, 0, "x");

        eq2.addPolynomial(e2p1);
        eq2.addPolynomial(e2p2);
        eq2.addPolynomial(e2p3);
       // eq2.addPolynomial(e2p4);


        eq2.polyClean();

        Equation eq3 = new Equation();
       

        Polynomial e3p1 = new Polynomial(0f, 1, "x");//13
        Polynomial e3p2 = new Polynomial(0f, 1, "y");//15
        Polynomial e3p3 = new Polynomial(2.5f, 1, "z");
       // Polynomial e3p4 = new Polynomial(0f, 0, "x");

        eq3.addPolynomial(e3p1);
        eq3.addPolynomial(e3p2);
        eq3.addPolynomial(e3p3);
      //  eq3.addPolynomial(e3p4);

        eq3.polyClean();

        List<Equation> derMat = new List<Equation>();

        derMat.Add(eq1);
        derMat.Add(eq2);
        derMat.Add(eq3);

        return derMat;
    }

    public Equation createRandomEQ ()
    {

        Equation eq = new Equation();

        //Generate Pow 0
        for (int i = 0; i < Mathf.FloorToInt(Random.Range(1, 5)); i++)
        {
            Polynomial j = new Polynomial(Mathf.Floor(Random.Range(-10f, 10f)), 0);
            eq.addPolynomial(j);
        }

        //Generate X
        for (int i = 0; i < Mathf.FloorToInt(Random.Range(1, 5)); i ++)
        {
            Polynomial j = new Polynomial(Mathf.Floor(Random.Range(1f, 10f)), 1, "x");
            eq.addPolynomial(j);
        }

        //Generate Y
        for (int i = 0; i < Mathf.FloorToInt(Random.Range(1, 5)); i++)
        {
            Polynomial j = new Polynomial(Mathf.Floor(Random.Range(1f, 10f)), 1, "y");
            eq.addPolynomial(j);
        }

        
        //Generate Z
        for (int i = 0; i < Mathf.FloorToInt(Random.Range(1, 5)); i++)
        {
            Polynomial j = new Polynomial(Mathf.Floor(Random.Range(1f, 10f)), 1, "z");
            eq.addPolynomial(j);
        }
        

        eq.polyClean();

        //eq.displayAllPoly();

        return eq;
    }


    public void solveCircuit ()
    {

        Equation norm1 = new Equation();

        Polynomial n1p1 = new Polynomial(3, 1, "x");
        Polynomial n1p2 = new Polynomial(-1, 1, "y");
        Polynomial n1p3 = new Polynomial(0, 1, "z");
        Polynomial n1p4 = new Polynomial(-10, 0, "x");

        norm1.addPolynomial(n1p1);
        norm1.addPolynomial(n1p2);
        norm1.addPolynomial(n1p3);
        norm1.addPolynomial(n1p4);

        norm1.polyClean();

        Debug.Log("EQ 1");
        norm1.displayAllPoly();

        Equation norm2 = new Equation();

        Polynomial n2p1 = new Polynomial(-2, 1, "x");
        Polynomial n2p2 = new Polynomial(3, 1, "y");
        Polynomial n2p3 = new Polynomial(-1, 1, "z");
        Polynomial n2p4 = new Polynomial(0, 0, "x");

        norm2.addPolynomial(n2p1);
        norm2.addPolynomial(n2p2);
        norm2.addPolynomial(n2p3);
        norm2.addPolynomial(n2p4);

        norm2.polyClean();

        Debug.Log("EQ 2");
        norm2.displayAllPoly();

        Equation norm3 = new Equation();

        Polynomial n3p1 = new Polynomial(0, 1, "x");
        Polynomial n3p2 = new Polynomial(-1.5f, 1, "y");
        Polynomial n3p3 = new Polynomial(2.5f, 1, "z");
        Polynomial n3p4 = new Polynomial(0, 0, "x");

        norm3.addPolynomial(n3p1);
        norm3.addPolynomial(n3p2);
        norm3.addPolynomial(n3p3);
        norm3.addPolynomial(n3p4);

        norm3.polyClean();

        Debug.Log("EQ 3");
        norm3.displayAllPoly();

        List<Equation> idk = new List<Equation>();

        idk.Add(norm1);
        idk.Add(norm2);
        idk.Add(norm3);

        List<PolyOutput> vals1 = new List<PolyOutput>();
        vals1.Add(new PolyOutput("x", 1));
        vals1.Add(new PolyOutput("y", 1));
        vals1.Add(new PolyOutput("z", 1));

        Debug.Log("Norm 1 " + norm1.output(vals1));

        Debug.Log("Norm 2 " + norm2.output(vals1));
        Debug.Log("Norm 3 " + norm3.output(vals1));


        List<Equation> der = new List<Equation>();

        Equation dnorm1 = new Equation();

        Polynomial dn1p1 = new Polynomial(3, 1, "x");
        Polynomial dn1p2 = new Polynomial(0, 1, "y");
        Polynomial dn1p3 = new Polynomial(0, 1, "z");
        Polynomial dn1p4 = new Polynomial(0, 0, "x");

        dnorm1.addPolynomial(dn1p1);
        dnorm1.addPolynomial(dn1p2);
        dnorm1.addPolynomial(dn1p3);
        dnorm1.addPolynomial(dn1p4);

        dnorm1.polyClean();

        Debug.Log("EQ 1");
        dnorm1.displayAllPoly();

        Equation dnorm2 = new Equation();

        Polynomial dn2p1 = new Polynomial(0, 1, "x");
        Polynomial dn2p2 = new Polynomial(3, 1, "y");
        Polynomial dn2p3 = new Polynomial(0, 1, "z");
        Polynomial dn2p4 = new Polynomial(0, 0, "x");

        dnorm2.addPolynomial(dn2p1);
        dnorm2.addPolynomial(dn2p2);
        dnorm2.addPolynomial(dn2p3);
        dnorm2.addPolynomial(dn2p4);

        dnorm2.polyClean();

        Debug.Log("EQ 2");
        dnorm2.displayAllPoly();

        Equation dnorm3 = new Equation();

        Polynomial dn3p1 = new Polynomial(0, 1, "x");
        Polynomial dn3p2 = new Polynomial(0, 1, "y");
        Polynomial dn3p3 = new Polynomial(2.5f, 1, "z");
        Polynomial dn3p4 = new Polynomial(0, 0, "x");

        dnorm3.addPolynomial(dn3p1);
        dnorm3.addPolynomial(dn3p2);
        dnorm3.addPolynomial(dn3p3);
        dnorm3.addPolynomial(dn3p4);

        dnorm3.polyClean();

        Debug.Log("EQ 3");
        dnorm3.displayAllPoly();

        der.Add(dnorm1);
        der.Add(dnorm2);
        der.Add(dnorm3);


        Matrix inv = new Matrix(der);

       // Matrix normal = new Matrix(idk);

        float x = 1;
        float y = 1;
        float z = 1;

        float newX = 0;
        float newY = 0;
        float newZ = 0;


        for (int i = 0; i < 100; i ++)
        {
            List<List<float>> outputVec = new List<List<float>>();

            List<PolyOutput> vals = new List<PolyOutput>();
            vals.Add(new PolyOutput("x", x));
            vals.Add(new PolyOutput("y", y));
            vals.Add(new PolyOutput("z", z));


            for (int j = 0; j < 3; j++)
            {
                List<float> row = new List<float>();

                idk[j].displayAllPoly();

                row.Add(idk[j].output(vals));
                Debug.Log(idk[j].output(vals));
                outputVec.Add(row);
            }

            newX = x - inv.matrixMulti(inv.inverseMat, outputVec)[0][0];
            newY = y - inv.matrixMulti(inv.inverseMat, outputVec)[1][0];
            newZ = z - inv.matrixMulti(inv.inverseMat, outputVec)[2][0];





            x = newX;
            y = newY;
            z = newZ;


        }


        Debug.Log("X: " + x);
        Debug.Log("Y: " + y);
        Debug.Log("Z: " + z);



    }



    /*
        Equation norm1 = new Equation();

        Polynomial n1p1 = new Polynomial(3, 1, "x");
        Polynomial n1p2 = new Polynomial(-1, 1, "y");
        Polynomial n1p3 = new Polynomial(0, 1, "z");
        Polynomial n1p4 = new Polynomial(-10, 0, "x");

        norm1.addPolynomial(n1p1);
        norm1.addPolynomial(n1p2);
        norm1.addPolynomial(n1p3);
        norm1.addPolynomial(n1p4);

        norm1.polyClean();

        Equation norm2 = new Equation();

        Polynomial n2p1 = new Polynomial(-1, 1, "x");
        Polynomial n2p2 = new Polynomial(3, 1, "y");
        Polynomial n2p3 = new Polynomial(0, 0, "x");

        norm2.addPolynomial(n2p1);
        norm2.addPolynomial(n2p2);
        norm2.addPolynomial(n2p3);

        Equation norm23 = new Equation();

        Polynomial n3p1 = new Polynomial(-1, 1, "x");
        Polynomial n3p2 = new Polynomial(3, 1, "y");
        Polynomial n3p3 = new Polynomial(0, 0, "x");

        norm2.addPolynomial(n2p1);
        norm2.addPolynomial(n2p2);
        norm2.addPolynomial(n2p3);




        norm2.polyClean();
        List<Equation> idk = new List<Equation>();

        idk.Add(norm1);
        idk.Add(norm2);

        List<PolyOutput> vals1 = new List<PolyOutput>();
        vals1.Add(new PolyOutput("x", 1));
        vals1.Add(new PolyOutput("y", 1));

        Debug.Log("Norm 1 " + norm1.output(vals1));

        Debug.Log("Norm 2 " + norm2.output(vals1));


        List<Equation> der = new List<Equation>();


        Equation dnorm1 = new Equation();

        Polynomial dn1p1 = new Polynomial(3, 1, "x");
        Polynomial dn1p2 = new Polynomial(0, 1, "y");
        Polynomial dn1p3 = new Polynomial(0, 0, "x");

        dnorm1.addPolynomial(dn1p1);
        dnorm1.addPolynomial(dn1p2);
        dnorm1.addPolynomial(dn1p3);

        dnorm1.polyClean();

        Equation dnorm2 = new Equation();

        Polynomial dn2p1 = new Polynomial(0, 1, "x");
        Polynomial dn2p2 = new Polynomial(3, 1, "y");
        Polynomial dn2p3 = new Polynomial(0, 0, "x");

        dnorm2.addPolynomial(dn2p1);
        dnorm2.addPolynomial(dn2p2);
        dnorm2.addPolynomial(dn2p3);

        dnorm2.polyClean();

        der.Add(dnorm1);
        der.Add(dnorm2);


        Matrix inv = new Matrix(der); 
  */




}
