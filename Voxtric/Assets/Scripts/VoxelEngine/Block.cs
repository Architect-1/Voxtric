﻿using System;

namespace VoxelEngine
{
    public struct Block
    {
        public readonly byte ID;
        public readonly byte visible;
        public byte health;

        public Block(ushort data)
        {
            ID = (byte)(data & 255);
            visible = (byte)((data & 256) >> 8);
            health = (byte)((data & 65024) >> 9);
        }

        public Block(byte ID, byte visible, byte health)
        {
            if (visible > 1)
            {
                throw new ArgumentOutOfRangeException("visible", "Visible must be either 0 or 1");
            }
            else if (health > 127)
            {
                throw new ArgumentOutOfRangeException("health", "Health must be within range 0 - 127");
            }
            this.ID = ID;
            this.visible = visible;
            this.health = health;
        }

        public static Block empty
        {
            get { return new Block(); }
        }

        public static implicit operator ushort(Block block)
        {
            ushort data = (ushort)0;
            data = (ushort)((data | block.health) << 1);
            data = (ushort)((data | block.visible) << 8);
            data = (ushort)(data | block.ID);
            return data;
        }

        public static explicit operator string(Block block)
        {
            return string.Format("ID:{0}, Visible:{1}, Health:{2}", block.ID, block.visible, block.health);
        }
    }
}