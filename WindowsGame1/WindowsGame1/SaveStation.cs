﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Spaceman
{
	public class SaveStation: Object, IMapItem
	{
        SoundFX dialUp;
		private bool recentlyActivated;
		public SaveStation(double worldX, double worldY, Texture2D texture, Vector2 mapCoordinates, int numFrames, int frameNum)
			: base(worldX, worldY, texture, new Vector2((float)worldX - mapCoordinates.X, (float)worldY - mapCoordinates.Y), numFrames, frameNum, false)
		{
            dialUp = new SoundFX();
		}

		void IMapItem.UpdateSprite(Game1 game)
		{
            CollisionState result = CollisionDetector.PerPixelSprite(this, game.player, game.graphics);
            if (result == CollisionState.Hurtbox || result == CollisionState.Standard)
			{
				this.timer++;
				if (this.timer >= 3 * this.FRAME_OFFSET)
				{
					if (this.frameNum == numFrames - 1)
					{
						if (!this.recentlyActivated)
						{
							game.OpenSaveStationMenu();
							this.recentlyActivated = true;
                            dialUp.Play("dialUp");

						}
					}
					else
					{
						this.frameNum++;
					}
					this.timer = 1;
				}
			}
			else
			{
				this.recentlyActivated = false;
				this.timer--;
				if (this.timer <= 0)
				{
					this.timer = 3 * this.FRAME_OFFSET;
					if (this.frameNum <= 0) this.frameNum = 0;
					else this.frameNum--;
				}
			}
			this.UpdateSprite(game.worldMap[game.currentRoom]);
			if (this.onScreen)
				game.AddObjectToDraw(this);
			else
				game.RemoveObjectToDraw(this);
		}

		double IMapItem.GetX()
		{
			return this.worldX;
		}

		double IMapItem.GetY()
		{
			return this.worldY;
		}

		Boolean IMapItem.GetIsOnScreen()
		{
			return this.onScreen;
		}

		public String GetObjectType()
		{
			return "MapItem";
		}
	}
}
