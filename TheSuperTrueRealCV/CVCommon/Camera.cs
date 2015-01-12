﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace CVCommon
{
    public class Camera
    {
        private Vector2 worldPosition;
        private Vector2 cameraSpeed;

        public Vector2 CameraVeiwSize
        {
            get { return Settings.gameSize; }
        }

        public Camera(Vector2 pos)
        {
            worldPosition = pos;
            cameraSpeed = new Vector2(300, 300);
        }

        public Vector2 Position
        {
            get { return worldPosition; }
            set { worldPosition = value; }
        }

        public Rectangle CameraViewSpace
        {
            get { return new Rectangle((int)worldPosition.X, (int)worldPosition.Y, (int)Settings.gameSize.X, (int)Settings.gameSize.Y); }
        }

        public Rectangle CameraRightSpace
        {
            get { return new Rectangle((int)worldPosition.X + (int)Settings.gameSize.X - ((int)Settings.gameSize.X/5), (int)worldPosition.Y, (int)Settings.gameSize.X/5, (int)Settings.gameSize.Y); }
        }

        public Rectangle CameraLeftSpace
        {
            get { return new Rectangle((int)worldPosition.X, (int)worldPosition.Y, (int)Settings.gameSize.X/5, (int)Settings.gameSize.Y); }
        }

        public void Update(Vector2 playerpos)
        {
            worldPosition.X = playerpos.X + Settings.playerSize.X/2 - Settings.gameSize.X / 2;
            worldPosition.Y = playerpos.Y + Settings.playerSize.Y - Settings.gameSize.Y / 2;
        }

        /// <summary>
        /// Used by the editor
        /// </summary>
        public void UpdateEditorCamera(Vector2 piStepSize)
        {
            if (KeyMouseReader.KeyPressed(Keys.W))
                worldPosition.Y -= piStepSize.Y;
            if (KeyMouseReader.KeyPressed(Keys.S))
                worldPosition.Y += piStepSize.Y;
            if (KeyMouseReader.KeyPressed(Keys.A))
                worldPosition.X -= piStepSize.X;
            if (KeyMouseReader.KeyPressed(Keys.D))
                worldPosition.X += piStepSize.X;

            if (KeyMouseReader.KeyPressed(Keys.Space))
                worldPosition = Vector2.Zero;
        }

        public Vector2 GetObjectScreenPosition(Vector2 pos)
        {
            return pos - worldPosition;
        }

        public Vector2 GetObjectWorldPosition(Vector2 pos)
        {
            return worldPosition + pos;
        }

        public bool IsInsideVeiwSpace(Rectangle rec)
        {
            return CameraViewSpace.Intersects(rec);
        }
    }
}
