﻿using System;
using System.IO;
using System.Text;

namespace VoxelEngine.Hidden
{
    public sealed class VoxelData
    {
        public const int SIZE = 16;

        private ushort[,,] _data = new ushort[SIZE, SIZE, SIZE];
        private IntVec3 _dataPosition;

        public VoxelData(IntVec3 dataPosition, string loadFrom)
        {
            _dataPosition = dataPosition;
            LoadData(loadFrom);
        }

        public IntVec3 GetDataPosition()
        {
            return _dataPosition;
        }

        public ushort GetData(int x, int y, int z)
        {
            try
            {
                return _data[x, y, z];
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("X, Y, Z co-ordinates", string.Format("{0} is not a valid data position", (string)new IntVec3(x, y, z)));
            }
        }

        public void SetData(int x, int y, int z, ushort data)
        {
            try
            {
                _data[x, y, z] = data;
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("X, Y, Z co-ordinates", string.Format("{0} is not a valid data position", (string)new IntVec3(x, y, z)));
            }
        }

        public void SaveData(string collectionDirectory)
        {
            string fullPath = string.Format(@"{0}\{1}.vdat", collectionDirectory, (string)_dataPosition);
            StringBuilder dataStore = new StringBuilder();
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    for (int z = 0; z < SIZE; z++)
                    {
                        dataStore.Append(_data[x, y, z].ToString("X4"));
                    }
                    dataStore.AppendLine();
                }
            }
            StreamWriter saveFile = new StreamWriter(fullPath);
            saveFile.Write(dataStore.ToString());
            saveFile.Close();
        }

        public void LoadData(string collectionDirectory)
        {
            string fullPath = string.Format(@"{0}\{1}.vdat", collectionDirectory, (string)_dataPosition);
            string dataLine;
            int index;
            if (File.Exists(fullPath))
            {
                StreamReader saveFile = new StreamReader(fullPath);
                for (int x = 0; x < SIZE; x++)
                {
                    for (int y = 0; y < SIZE; y++)
                    {
                        index = 0;
                        dataLine = saveFile.ReadLine();
                        for (int z = 0; z < SIZE; z++)
                        {
                            _data[x, y, z] = (Convert.ToUInt16(dataLine.Substring(index, 4), 16));
                            index += 4;
                        }
                    }
                }
                saveFile.Close();
            }
            //The following is purely for debugging purposes.
            /*else
            {
                for (int x = 0; x < SIZE; x++)
                {
                    for (int y = 0; y < SIZE; y++)
                    {
                        for (int z = 0; z < SIZE; z++)
                        {
                            _data[x, y, z] = (ushort)new Block(2, 1, 40);
                        }
                    }
                }
            }*/
        }
    }
}