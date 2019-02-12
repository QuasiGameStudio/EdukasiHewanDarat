using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FreeDraw
{
    // Helper methods used to set drawing settings
    public class DrawingSettings : MonoBehaviour
    {
        public static bool isCursorOverUI = false;
        public float Transparency = 1f;

        // Changing pen settings is easy as changing the static properties Drawable.Pen_Colour and Drawable.Pen_Width
        public void SetMarkerColour(Color new_color)
        {
            Drawable.Pen_Colour = new_color;
        }
        // new_width is radius in pixels
        public void SetMarkerWidth(int new_width)
        {
            Drawable.Pen_Width = new_width;
        }
        public void SetMarkerWidth(float new_width)
        {
            SetMarkerWidth((int)new_width);
        }

        public void SetTransparency(float amount)
        {
            Transparency = amount;
            Color c = Drawable.Pen_Colour;
            c.a = amount;
            Drawable.Pen_Colour = c;
        }

        public void SetColor(int i){

            Color c = Color.red;
            Color violet = new Color(126,47,144,225);

            switch (i)
            {
                case 0:
                    c = Color.black;
                    break;

                case 1:
                    c = Color.blue;
                    break;

                case 2:
                    c = Color.cyan;
                    break;
                    
                case 3:
                    c = Color.gray;
                    break;                                              
                case 4:
                    c = Color.green;
                    break;                
                case 5:
                    c = Color.magenta;
                    break;
                case 6:
                    c = Color.red;
                    break;                    
                case 7:
                    c = Color.yellow;
                    break;                
                case 8:
                    c = Color.white;
                    break;                                    
                default:
                    c = Color.black;
                    break;
            }

            SetMarkerColour(c);
        }

        // Call these these to change the pen settings
        public void SetMarkerRed()
        {
            Color c = Color.red;
            c.a = Transparency;
            SetMarkerColour(c);
            Drawable.drawable.SetPenBrush();
        }
        public void SetMarkerGreen()
        {
            Color c = Color.green;
            c.a = Transparency;
            SetMarkerColour(c);
            Drawable.drawable.SetPenBrush();
        }
        public void SetMarkerBlue()
        {
            Color c = Color.blue;
            c.a = Transparency;
            SetMarkerColour(c);
            Drawable.drawable.SetPenBrush();
        }
        public void SetEraser()
        {
            SetMarkerColour(new Color(255f, 255f, 255f, 0f));
        }

        public void PartialSetEraser()
        {
            SetMarkerColour(new Color(255f, 255f, 255f, 0.5f));
        }
    }
}