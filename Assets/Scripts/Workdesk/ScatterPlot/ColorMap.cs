using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMap: MonoBehaviour
{
    static Gradient g = new Gradient();
    static bool gradientSet = false;
    public static void setGradient(Color32 startColor, Color32 endColor)
    {
        gradientSet = true;
        GradientColorKey[] gck = new GradientColorKey[2];
        GradientAlphaKey[] gak = new GradientAlphaKey[2];
        gck[0].color = startColor;
        gck[0].time = 0.0F;
        gck[1].color = endColor;
        gck[1].time = 1.0F;
        gak[0].alpha = 1.0F;
        gak[0].time = 0.0F;
        gak[1].alpha = 1.0F;
        gak[1].time = 1.0F;
        g.SetKeys(gck, gak);
    }

    public static Color getColorForValue(float value)
    {
        if (gradientSet)
        {
            return g.Evaluate(value);
        }
        else
        {
            Debug.LogError("Der Gradient muss zuerst gesetzt werden (setgradient(..)");
            return Color.white;
        }
        
    }

}
