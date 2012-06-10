using UnityEngine;
using System;

public class MainMenuGui : MonoBehaviour {

    private string ip = ""; //ip address sent from the user
    private string connect = "Connect"; //connect or disconnect in Settings
    private string connected = "NOT Connected"; //Connection label Top-Right
    private string calibrated = "NOT Calibrated"; //Calibration label Top-Right

    GUIStyle myStyle, connectStyle, calibrateStyle, posLeft, posRight, posBot, posTop, calibrateText; //GUI Styles for Box, Top Right Labels,  Calibration Marks, and calibrating text

    private int menuScene = 0; //0 = Main | 1 = Settings | 2 = Calibrate | 3 = Credits | 4 = Connection Error | 5  = Calibration Error

    private bool isConnected; //connection status
    public static bool isCalibrated = false; //calibration status
	public static bool isCalibrated2 = false;

    public static float posXG = 0, posYG = 0;

    /*
     *Constructs the Basic GUI Settings
     *and Instantiates nesecary files for MoveMe
     */
    void Start()
    {

        myStyle = new GUIStyle();

        myStyle.fontSize = 80;
        myStyle.alignment = TextAnchor.UpperCenter;
        myStyle.normal.textColor = Color.white;
		
        connectStyle = new GUIStyle();
        calibrateStyle = new GUIStyle();

        connectStyle.fontSize = 10;
        connectStyle.normal.textColor = Color.red;

        calibrateStyle.fontSize = 10;
        calibrateStyle.normal.textColor = Color.red;

        posLeft = new GUIStyle();
        posRight = new GUIStyle();
        posBot = new GUIStyle();
        posTop = new GUIStyle();

        posLeft.normal.textColor = Color.red;
        posRight.normal.textColor = Color.red;
        posBot.normal.textColor = Color.red;
        posTop.normal.textColor = Color.red;

        posLeft.alignment = TextAnchor.MiddleCenter;
        posRight.alignment = TextAnchor.MiddleCenter;
        posBot.alignment = TextAnchor.MiddleCenter;
        posTop.alignment = TextAnchor.MiddleCenter;

        calibrateText = new GUIStyle();

        calibrateText.alignment = TextAnchor.MiddleCenter;
        calibrateText.normal.textColor = Color.white;

        calibrationIcons();
    }

	/*
     * Checks for which screen to display
     * and posts constant overlays seen
     * through entire Menu System
     */
    void OnGUI()
    {
		Screen.showCursor = true;
		
        GUI.Label(new Rect(Screen.width - 80, 0, 80, 20), connected, connectStyle);
        GUI.Label(new Rect(Screen.width - 80, 10, 80, 20), calibrated, calibrateStyle);

        if (MoveMeConnect.client_connected)
        {
            connected = "Connected";
            connect = "Disconnect";
            isConnected = true;
            connectStyle.normal.textColor = Color.green;
        }
        else
        {
            connected = "NOT Connected";
            connect = "Connect";
            isConnected = false;
            connectStyle.normal.textColor = Color.red;
        }

        if (isCalibrated)
        {
			state = MoveMeConnect.getState();
			
			GUI.Label(new Rect(10, 0, 80, 40), state.pointerStates[0].normalized_x.ToString("N3"), calibrateStyle);
        	GUI.Label(new Rect(10, 10, 80, 40), state.pointerStates[0].normalized_y.ToString("N3"), calibrateStyle);
			
            calibrated = "Calibrated";
            calibrateStyle.normal.textColor = Color.green;
        }
        else
        {
            calibrated = "NOT Calibrated";
            calibrateStyle.normal.textColor = Color.red;
        }

        switch (menuScene)
        {
            case 0:
                mainMenuScreen();
                break;
            case 1:
                settingsScreen();
                break;
            case 2:
                calibrateScreen();
                break;
            case 4:
                connectionError();
                break;
			case 5:
				calibrateScreen2();
				break;
            default:
                mainMenuScreen();
                break;
        }
    }

    /*
     * First Gui Screen Shown / Default Screen
     * Main Menu allows to select different menus
     */
    void mainMenuScreen()
    {
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Main Menu", myStyle);

        if (GUI.Button(new Rect(Screen.width / 2 - 75, 100, 150, 100), "Play Game"))
        {
            Application.LoadLevel(1);
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 75, 220, 150, 100), "Settings"))
        {
            menuScene = 1;
        }
		
		if (GUI.Button(new Rect(Screen.width / 2 - 75, 340, 150, 100), "Credits"))
        {
            Application.LoadLevel(2);
        }
		
		if (GUI.Button(new Rect(Screen.width / 2 - 75, 460, 150, 100), "Quit"))
        {
            Application.Quit();
        }
    }

    /*
     * Settings allows the user to set difficulty
     * and set up the MoveMe Protocall
     */
    void settingsScreen()
    {
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Settings", myStyle);

        if (GUI.Button(new Rect(Screen.width / 2 - 75, 100, 150, 100), connect))
        {
            if (MoveMeConnect.client_connected)
            {
                MoveMeConnect.client_disconnect();
            }
            else
            {
                MoveMeConnect.client_connect(ip, 7899);
            }
        }

        ip = GUI.TextField(new Rect(Screen.width / 2 - 75, 220, 150, 20), ip);

        if (GUI.Button(new Rect(Screen.width / 2 - 75, 260, 150, 100), "Calibrate"))
        {
            if (isConnected)
                menuScene = 2; 
            else
                menuScene = 4; //should be 4 change to 2 for debugging
        }
		
		if (GUI.Button(new Rect(Screen.width / 2 - 75, 370, 150, 100), "Calibrate 2"))
        {
            if (isConnected)
                menuScene = 5; 
            else
                menuScene = 4; //should be 4 change to 2 for debugging
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 75, 480, 50, 100), "1 Player"))
        {
			if(isCalibrated)
            	ScoreKeeper.setPlayers(1);
			else
				ScoreKeeper.setPlayers(0);
        }
		
		if (GUI.Button(new Rect(Screen.width / 2 - 25, 480, 50, 100), "2 Player"))
        {
            if(isCalibrated)
				ScoreKeeper.setPlayers(2);
			if(isCalibrated && isCalibrated2)
				ScoreKeeper.setPlayers(3);
        }
		
		if (GUI.Button(new Rect(Screen.width / 2 + 25, 480, 50, 100), "3 Player"))
        {
			if(isCalibrated && isCalibrated2)
				ScoreKeeper.setPlayers(4);
        }
		
		if (GUI.Button(new Rect(Screen.width / 2 - 75, 590, 150, 100), "Back"))
        {
            menuScene = 0;
        }
    }


    //Global Variables for Calibration
    private int calibrateStep = 0; //Calibrated Sides 0 = None | 1 = Left | 2 = Right | 3 = Bottom | 4 = Top | 5 = All Done
    private UInt16 last_buttons;
    private string centerText = "";
    private PSMoveSharpState state;
    /*
     * Calibration for positioning of the pointer
     */
    void calibrateScreen()
    {
        state = MoveMeConnect.getState();
        GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 50, 150, 100), centerText, calibrateText);

        GUI.Label(new Rect(0,0,Screen.width, Screen.height), "[+]", leftCross);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "[+]", botCross);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "[+]", topCross);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "[+]", rightCross);

        switch (calibrateStep)
        {
            case 0:
                if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 50, 150, 100), "Start" + "\n" + "Calibration"))
                {
                    calibrateStep = 1;
                    MoveMeConnect.calibrate(0);
                }
                break;

            case 1:
                centerText = "Left";
                leftCross.normal.textColor = Color.white;
                break;

            case 2:
                centerText = "Right";
                leftCross.normal.textColor = Color.green;
                rightCross.normal.textColor = Color.white;
                break;

            case 3:
                centerText = "Bottom";
                rightCross.normal.textColor = Color.green;
                botCross.normal.textColor = Color.white;
                break;

            case 4:
                centerText = "Top";
                botCross.normal.textColor = Color.green;
                topCross.normal.textColor = Color.white;
                break;

            case 5:
                centerText = "";
                if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 50, 150, 100), "Done" + "\n" + "Go Back"))
                {
                    menuScene = 1;
                    MoveMeConnect.moveClient.SendRequestPacket(PSMoveClient.ClientRequest.PSMoveClientRequestPointerEnable, Convert.ToUInt32(0));
                }
                topCross.normal.textColor = Color.green;
                isCalibrated = true;
                break;
        }

        int i = 0; //i = controller to track

        UInt16 just_pressed;
        {
            UInt16 changed_buttons = (UInt16)(state.gemStates[0].pad.digitalbuttons ^ last_buttons);
            just_pressed = (UInt16)(changed_buttons & state.gemStates[0].pad.digitalbuttons);
            last_buttons = state.gemStates[0].pad.digitalbuttons;
        }

        const int PadTick = 1 << 2;
        const int PadTrigger = 1 << 1;
        if ((just_pressed & PadTick) == PadTick && (last_buttons & PadTrigger) == PadTrigger)
        {

            //Switch case written backwards in order to prevent 
            //extra cases from running
            switch (calibrateStep)
            {
                case 4:
                    MoveMeConnect.moveClient.SendRequestPacket(PSMoveClient.ClientRequest.PSMoveClientRequestPointerSetTop, Convert.ToUInt32(0));
                    calibrateStep = 5;
                    break;

                case 3:
                    MoveMeConnect.moveClient.SendRequestPacket(PSMoveClient.ClientRequest.PSMoveClientRequestPointerSetBottom, Convert.ToUInt32(0));
                    calibrateStep = 4;
                    break;

                case 2:
                    MoveMeConnect.moveClient.SendRequestPacket(PSMoveClient.ClientRequest.PSMoveClientRequestPointerSetRight, Convert.ToUInt32(0));
                    calibrateStep = 3;
                    break;

                case 1:
                    MoveMeConnect.moveClient.SendRequestPacket(PSMoveClient.ClientRequest.PSMoveClientRequestPointerSetLeft, Convert.ToUInt32(0));
                    calibrateStep = 2;
                    break;
                default:
                    break;
            }
        }
    }
	
	private int calibrateStep2 = 0; //Calibrated Sides 0 = None | 1 = Left | 2 = Right | 3 = Bottom | 4 = Top | 5 = All Done
    private string centerText2 = "";
	
	void calibrateScreen2()
    {
        state = MoveMeConnect.getState();
        GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 50, 150, 100), centerText2, calibrateText);

        GUI.Label(new Rect(0,0,Screen.width, Screen.height), "[+]", leftCross);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "[+]", botCross);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "[+]", topCross);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "[+]", rightCross);

        switch (calibrateStep2)
        {
            case 0:
                if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 50, 150, 100), "Start" + "\n" + "Calibration"))
                {
                    calibrateStep2 = 1;
                    MoveMeConnect.calibrate(1);
                }
                break;

            case 1:
                centerText2 = "Left";
                leftCross.normal.textColor = Color.white;
                break;

            case 2:
                centerText2 = "Right";
                leftCross.normal.textColor = Color.green;
                rightCross.normal.textColor = Color.white;
                break;

            case 3:
                centerText2 = "Bottom";
                rightCross.normal.textColor = Color.green;
                botCross.normal.textColor = Color.white;
                break;

            case 4:
                centerText2 = "Top";
                botCross.normal.textColor = Color.green;
                topCross.normal.textColor = Color.white;
                break;

            case 5:
                centerText = "";
                if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 50, 150, 100), "Done" + "\n" + "Go Back"))
                {
                    menuScene = 1;
                    MoveMeConnect.moveClient.SendRequestPacket(PSMoveClient.ClientRequest.PSMoveClientRequestPointerEnable, Convert.ToUInt32(1));
                }
                topCross.normal.textColor = Color.green;
                isCalibrated2 = true;
                break;
        }

        int i = 0; //i = controller to track

        UInt16 just_pressed;
        {
            UInt16 changed_buttons = (UInt16)(state.gemStates[1].pad.digitalbuttons ^ last_buttons);
            just_pressed = (UInt16)(changed_buttons & state.gemStates[1].pad.digitalbuttons);
            last_buttons = state.gemStates[1].pad.digitalbuttons;
        }

        const int PadTick = 1 << 2;
        const int PadTrigger = 1 << 1;
        if ((just_pressed & PadTick) == PadTick && (last_buttons & PadTrigger) == PadTrigger)
        {

            //Switch case written backwards in order to prevent 
            //extra cases from running
            switch (calibrateStep2)
            {
                case 4:
                    MoveMeConnect.moveClient.SendRequestPacket(PSMoveClient.ClientRequest.PSMoveClientRequestPointerSetTop, Convert.ToUInt32(1));
                    calibrateStep2 = 5;
                    break;

                case 3:
                    MoveMeConnect.moveClient.SendRequestPacket(PSMoveClient.ClientRequest.PSMoveClientRequestPointerSetBottom, Convert.ToUInt32(1));
                    calibrateStep2 = 4;
                    break;

                case 2:
                    MoveMeConnect.moveClient.SendRequestPacket(PSMoveClient.ClientRequest.PSMoveClientRequestPointerSetRight, Convert.ToUInt32(1));
                    calibrateStep2 = 3;
                    break;

                case 1:
                    MoveMeConnect.moveClient.SendRequestPacket(PSMoveClient.ClientRequest.PSMoveClientRequestPointerSetLeft, Convert.ToUInt32(1));
                    calibrateStep2 = 2;
                    break;
                default:
                    break;
            }
        }
    }
	
    //Error Dialogue to tell user to connect before calibrating
    void connectionError()
    {
        GUI.Label(new Rect(Screen.width / 2 - 450, Screen.height / 2 - 10, 900 , 200), "PLEASE CONNECT\nBEFORE CALIBRATING", myStyle);
        if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 100, 150, 50), "Continue"))
            menuScene = 1;
    }

    /*
     * Calibration crosses to click when calibrating;
     */
    GUIStyle leftCross, rightCross, botCross, topCross;
    void calibrationIcons()
    {
        leftCross = new GUIStyle();
        rightCross = new GUIStyle();
        topCross = new GUIStyle();
        botCross = new GUIStyle();

        leftCross.alignment = TextAnchor.MiddleLeft;
        rightCross.alignment = TextAnchor.MiddleRight;
        botCross.alignment = TextAnchor.LowerCenter;
        topCross.alignment = TextAnchor.UpperCenter;

        leftCross.normal.textColor = Color.red;
        rightCross.normal.textColor = Color.red;
        botCross.normal.textColor = Color.red;
        topCross.normal.textColor = Color.red;
    }
}