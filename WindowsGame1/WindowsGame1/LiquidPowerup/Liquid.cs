﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Spaceman
{

    public class Liquid
    {
        List<Droplet> dropletList;
        Texture2D pixel;
        int X;
        int Y;
        Game1 game;
        const int ROTATION_FRAMES = 3;
        Cooldown rotationCooldown = new Cooldown(ROTATION_FRAMES);

        public Cooldown GetRotationCooldown()
        {
            return this.rotationCooldown;
        }

        public Liquid(Texture2D pixel, int X, int Y, Game1 game)
        {
            this.pixel = pixel;
            this.X = X;
            this.Y = Y;
            this.game = game;
            dropletList = new List<Droplet>();
            for (int h = 0; h <= 1; h++)
            {
                for (int w = 0; w < 9; w++)
                {
                    if (h == 0)
                    {
                        dropletList.Add(new Droplet(pixel, new Vector2(X + w + 1, Y + 13 + h), 18, w, new Vector2(X,Y)));
                    }
                    else
                    {
                        dropletList.Add(new Droplet(pixel, new Vector2(X + 9 - w, Y + 13 + h), 18, w + 9, new Vector2(X, Y)));
                    }
                }
            }
        }


        public Droplet Pixel(int i)
        {
            return dropletList[i];
        }

        //public void UpdateLiquid(double dist, double movespeed, Directions dir)
        //{
        //    double rotation = 0;
        //    {
        //        if (dir == Directions.right && dist - rotation >= 1)
        //        {
        //            {
        //                for (int i = 0; i < 17; i++)
        //                {
        //                    Vector2 drop = CheckDrop(dropletList[i], dropletList[i + 1]);
        //                    dropletList[i].OffsetDestCoord(drop);
        //                    dropletList[17].OffsetDestCoord(CheckDrop(dropletList[17], dropletList[0]));
        //                }
        //                rotation = dist;
        //            }
        //        }
        //        if (dir == Directions.left)
        //        {
        //            {
        //                for (int i = 17; i > 0; i--)
        //                {
        //                    Vector2 drop = CheckDrop(dropletList[i], dropletList[i - 1]);
        //                    dropletList[i].OffsetDestCoord(drop);
        //                    dropletList[0].OffsetDestCoord(CheckDrop(dropletList[0], dropletList[17]));
        //                }
        //            }
        //        }
        //    }
        //}

        public void Rotate(Directions dir, Vector2 mapOffset)
        {
            if (dir == Directions.right)
            {
                {
                    for (int i = 0; i < 17; i++)
                    {
                        Vector2 drop = CheckDrop(dropletList[i], dropletList[i + 1]);
                        dropletList[i].OffsetDestCoord(drop);
                        dropletList[17].OffsetDestCoord(CheckDrop(dropletList[17], dropletList[0]));
                    }
                }
            }
            if (dir == Directions.left)
            {
                {
                    for (int i = 17; i > 0; i--)
                    {
                        Vector2 drop = CheckDrop(dropletList[i], dropletList[i - 1]);
                        dropletList[i].OffsetDestCoord(drop);
                        dropletList[0].OffsetDestCoord(CheckDrop(dropletList[0], dropletList[17]));
                    }
                }
            }
            
        }
        
        //returns immediate drop in y between two pixels
        private Vector2 CheckDrop(Droplet d1, Droplet d2)
        {
            return (d2.GetDestRect() - d1.GetDestRect());
        }

        //returns largest change in Y across all 
        private int CheckBigDrop()
        {
            int max = 0;
            for (int i = 0; i <dropletList.Count; i++)
            {
                int next = (int) Math.Abs(CheckDrop(dropletList[i], dropletList[i + 1]).Y);
                if (next > max)
                {
                    max = next;
                }

            }
            if (Math.Abs(CheckDrop(dropletList[17], dropletList[0]).Y) > max)
            {
                max = (int) Math.Abs(CheckDrop(dropletList[17], dropletList[0]).Y);
            }

            return max;
        }
    }
}
