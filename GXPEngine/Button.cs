using System;
using GXPEngine;
using GXPEngine.Core;

public class Button : Sprite
{
    string function;
    string target;
    public string currentLevel;

    public Button(string pFilename, string pTarget, string pFunction) : base (pFilename)
    {
        target = pTarget;
        function = pFunction;
        SetScaleXY(0.4f, 0.4f);
        SetOrigin(width / 2 + 300, height / 2);
    }

    void Update()
    {
        if(HitTestPoint(Input.mouseX, Input.mouseY))
        {
            
            if(Input.GetMouseButtonDown(0))
            {
                
                switch (function)
                {
                    case "restart":
                        target = ((MyGame)game).currentLevel;
                        ((MyGame)game).LoadLevel(target);
                        break;
                    case "play":
                        Console.WriteLine("Hit");
                        ((MyGame)game).LoadLevel(target);
                        break;
                    case "quit":
                        ((MyGame)game).Destroy();
                        break;
                    case "goto":
                        ((MyGame)game).LoadLevel(target);
                        break;
                }
            }
        }
        

        
    }
}
