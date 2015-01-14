﻿using UnityEngine;
using System.Collections;

namespace VoxelEngine
{
    public static class TextureFinder
    {
        public enum TextureFace { East, West, Top, Bottom, North, South };

        public const float TEXTURE_SPACING = 0.125f;

        public static Texture regionTexture;
        private static Vector2[] _textureOrigins = new Vector2[256];
        private static Vector2[] _textureDimensions = new Vector2[256];

        private static void SetTextureDetails(byte block, Vector2 position, Vector2 dimensions)
        {
            _textureOrigins[block] = position;
            _textureDimensions[block] = dimensions;
        }

        public static void AssignAllTextureDetails()
        {
            regionTexture = (Texture)Resources.Load("TileSheet");
            SetTextureDetails(2, new Vector2(0, 4), new Vector2(8, 4));
        }

        public static TextureDetails TextureDetailsFor(byte block)
        {
            return new TextureDetails(_textureOrigins[block], _textureDimensions[block]);
        }

        public static Vector2 AdjustForPosition(int x, int y, int z, TextureFace face, TextureDetails details)
        {
            Vector2 texturePosition = details.origin;
            switch (face)
            {
                case TextureFace.Top:
                case TextureFace.Bottom:
                    texturePosition.x += z;
                    texturePosition.y += x;
                    break;
                case TextureFace.North:
                case TextureFace.South:
                    texturePosition.x += x;
                    texturePosition.y += y;
                    break;
                case TextureFace.East:
                case TextureFace.West:
                    texturePosition.x += z;
                    texturePosition.y += y;
                    break;
                default:
                    Debug.LogWarning("Invalid texture face provided: Unexpected texturing results may occur.");
                    break;
            }
            while (texturePosition.x >= details.origin.x + details.dimensions.x)
            {
                texturePosition.x -= details.dimensions.x;
            }
            while (texturePosition.y >= details.origin.y + details.dimensions.y)
            {
                texturePosition.y -= details.dimensions.y;
            }
            return texturePosition;
        }
    }
}