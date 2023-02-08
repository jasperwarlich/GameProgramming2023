using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Diagnostics.Contracts;
using TiledMapParser;
using System.Collections.Generic;

public class MyGame : Game {

    /*public Player player { get; private set; }*/

    public string levelName = "mainmenu.tmx";
    public Level level;

    string nextLevel = null;

    public string currentLevel;

    public int coinsCollected = 0;
    public int coinsNeeded;

    public bool triggerCoinFailure = false;

    Sound soundtrack;
    SoundChannel soundChannel;





    public MyGame() : base(1280, 720, false)     // Create a window that's 800x600 and NOT fullscreen
	{
        soundtrack = new Sound("bgmusic.mp3", true, true);
        soundChannel = soundtrack.Play();
        OnAfterStep += CheckLoadLevel;
        LoadLevel(levelName);
        
      
    }

    void Update() {
   

    }

    void CheckLoadLevel()
    {
        if(nextLevel != null)
        {
            DestroyLevel();
            level = new Level(nextLevel);
            AddChild(level);
            nextLevel = null;
        }
    }

    public void LoadLevel(string filename)
    {
        nextLevel = filename;
    }

    public void DestroyLevel()
    {
        List<GameObject> children = GetChildren();
        foreach(GameObject child in children)
        {
            child.Destroy();
        }
    }


	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}

	
}