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




        //Settings
        settingsUI();

        saveUI();

        equation();

        /*
        [SerializeField] RectTransform settingsSec;
        [SerializeField] RectTransform settingTitle;
        [SerializeField] RectTransform setting1;
        [SerializeField] RectTransform setting2;
        [SerializeField] RectTransform volumeTitle;
        [SerializeField] RectTransform volumeSlide;

        */
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

       // float spacing = (secHeight - (settingsBtnHeight * 4 + settingsTitleHeight + settingsTitleHeight * subTextFac)) / 5;

       // Debug.Log(settingsBtnHeight);
        //Debug.Log(settingsTitleHeight);
       
        //Debug.Log(settingsBtnHeight);



        float settingsSpacing = (secHeight - (settingsTitleHeight + (settingsBtnHeight * 4) + (settingsTitleHeight * subTextFac))) / settingsSec.gameObject.transform.childCount - 1;

        //Debug.Log(settingsSpacing);

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

       // Debug.Log(secHeight * settingsFillFac);

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










    }






}
