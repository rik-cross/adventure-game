﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

using AdventureGame.Engine;

namespace AdventureGame
{
    public static class ItemEntity
    {
        public static Engine.Entity Create(int x, int y, string assetName,
            //int width = default, int height = default,
            bool animation = false, HashSet<string> collectableByTag = default)
        {

            Entity itemEntity = EngineGlobals.entityManager.CreateEntity();

            itemEntity.Tags.Name = "item1"; // REMOVE
            itemEntity.Tags.AddTag("item");

            //Vector2 imageSize = new Vector2(34, 34);

            // How to handle sprite sheets dynamically?
            // How to add optional params? e.g. size
            if (!animation)
            {
                itemEntity.AddComponent(new Engine.SpritesComponent("idle",
                    new Engine.Sprite(Globals.content.Load<Texture2D>(assetName))
                    ));
            }

            Texture2D texture = itemEntity.GetComponent<SpritesComponent>().GetSprite("idle").textureList[0];
            Vector2 imageSize = new Vector2(texture.Width, texture.Height);
            Console.WriteLine($"Item image width {imageSize.X} height {imageSize.Y}");
            //Console.WriteLine($"Item image width {itemImage.Width} height {itemImage.Height}");

            itemEntity.AddComponent(new Engine.TransformComponent(new Vector2(x, y), imageSize));
            itemEntity.AddComponent(new Engine.ItemComponent(collectableByTag));
            itemEntity.AddComponent(new Engine.ColliderComponent(imageSize));

            return itemEntity;

        }

    }
}
