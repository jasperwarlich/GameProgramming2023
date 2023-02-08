using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
using System.Drawing;
using System;

public class Sign : Sprite
{
    public string text;
    EasyDraw sign;

    public Sign(string pText, TiledObject obj = null) : base("sign.png")
    {
        text = pText;
        SetScaleXY(0.05f, 0.05f);
        sign = new EasyDraw(400, 100, false);
        sign.SetOrigin(sign.width/2, -60);
        sign.SetScaleXY(10, 10);
        collider.isTrigger = true;
        AddChild(sign);


    }

    void Update()
    {
        sign.ClearTransparent();
    }

    public void WriteText()
    {
        Console.WriteLine(text);
        sign.Fill(Color.Black, 100);
        sign.Rect(0, 0, sign.width * 2, sign.height * 2);
        sign.Fill(Color.White, 255);
        sign.TextSize(22f);
        sign.TextAlign(CenterMode.Min, CenterMode.Min);
        sign.Text(text, 0, 0);
        sign.SetXY(600, -1500);
    }
}