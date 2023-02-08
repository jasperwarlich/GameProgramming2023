using GXPEngine;
using System;
using TiledMapParser;
using System.Collections.Generic;

public class Level : GameObject
{
    public Player player { get; private set; }

    private TiledLoader loader;

    LevelHud hud;

    public String currentLevelName;
    int currentLevelInt;
    
    public Level(string pFilename)
    {
        loader = new TiledLoader(pFilename);
        currentLevelName = pFilename;
        

        CreateLevel();
    }

    void Update()
    {

    }

    void CreateLevel()
    {
        switch(currentLevelName)
        {
            case "mainmenu.tmx":
                loader.autoInstance = true;
                loader.addColliders = false;
                loader.LoadTileLayers(0);
                loader.LoadTileLayers(1);
                loader.LoadImageLayers();
                loader.addColliders = true;
                loader.AddManualType("Button");
                loader.OnObjectCreated += TiledLoader_OnObjectCreated;
                loader.LoadObjectGroups();

                break;
            case "level.tmx":
                currentLevelInt = 1;
                loader.autoInstance = true;
                loader.addColliders = false;
                loader.LoadTileLayers(0);
                loader.LoadTileLayers(1);
                loader.addColliders = true;
                loader.LoadTileLayers(2);
                loader.AddManualType("Player", "EatEnemy", "Star", "Sign", "Portal");
                loader.OnObjectCreated += TiledLoader_OnObjectCreated;
                loader.LoadObjectGroups();
                ((MyGame)game).currentLevel = "level.tmx";
                ((MyGame)game).coinsNeeded = 5;

                
                break;
            case "level2.tmx":
                currentLevelInt = 2;
                loader.autoInstance = true;
                loader.addColliders=false;
                loader.LoadTileLayers(0);
                loader.addColliders = true;
                loader.LoadTileLayers(1);
                loader.AddManualType("Player", "PlantEnemy", "Portal", "Star", "EatEnemy");
                loader.OnObjectCreated += TiledLoader_OnObjectCreated;
                loader.LoadObjectGroups();
                ((MyGame)game).currentLevel = "level2.tmx";
                ((MyGame)game).coinsNeeded = 7;
                break;

            case "death.tmx":
                loader.autoInstance = true;
                loader.addColliders = false;
                loader.LoadTileLayers(0);
                loader.LoadImageLayers();
                loader.addColliders = true;
                loader.AddManualType("Button");
                loader.OnObjectCreated += TiledLoader_OnObjectCreated;
                loader.LoadObjectGroups();
                break;
            case "win.tmx":
                loader.autoInstance = true;
                loader.addColliders = false;
                loader.LoadTileLayers(0);
                loader.LoadImageLayers();
                loader.addColliders = true;
                loader.AddManualType("Button");
                loader.OnObjectCreated += TiledLoader_OnObjectCreated;
                loader.LoadObjectGroups();
                break;
        }

        if(currentLevelName != "mainmenu.tmx")
        {
            player = FindObjectOfType<Player>();
            if(player != null)
            {
                HandleHUD();
            }
        }
        

    }

    void TiledLoader_OnObjectCreated(Sprite sprite, TiledObject obj)
    {
        if (obj.Type == "Player")
        {
            Player player = new Player();
            player.SetXY(obj.X, obj.Y);
            AddChild(player);
        }

        if (obj.Type == "EatEnemy")
        {
            EatEnemy eatEnemy = new EatEnemy(obj.GetIntProperty("startX"), obj.GetIntProperty("endX"));
            eatEnemy.SetXY(obj.X, obj.Y);
            AddChild(eatEnemy);
        }

        if (obj.Type == "Star")
        {
            Star star = new Star();
            star.SetXY(obj.X, obj.Y);
            AddChild(star);
        }

        if (obj.Type == "Sign")
        {
            Sign sign = new Sign(obj.GetStringProperty("text"));
            sign.SetXY(obj.X, obj.Y);
            AddChild(sign);
        }

        if(obj.Type == "Portal")
        {
            Portal portal = new Portal(obj.GetStringProperty("levelTarget"));
            portal.SetXY(obj.X, obj.Y);
            AddChild(portal);
        }

        if(obj.Type == "PlantEnemy")
        {
            PlantEnemy plant = new PlantEnemy();
            plant.SetXY(obj.X, obj.Y);
            AddChild(plant);
        }

        if(obj.Type == "Button")
        {
            Button b = new Button(obj.GetStringProperty("filename_sprite"), obj.GetStringProperty("target"), obj.GetStringProperty("function"));
            b.SetXY(obj.X, obj.Y);
            AddChild(b);
        }
    }

    void HandleHUD()
    {
        if(player == null)
        {
            return;
        } else
        {
            hud = new LevelHud(player);
            hud.currentLevel = currentLevelInt;
            hud.coinsCollected = player.starsCollected;
            game.AddChild(hud);
        }
    }

}