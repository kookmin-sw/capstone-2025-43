using System;
using System.Collections.Generic;
using UnityEngine;

//include using MyProject.Utils;

namespace MyProject.Utils
{
    [System.Serializable]
    public struct Boundary1D<T> where T : IComparable<T>
    {
        public T min;
        public T max;
        public Boundary1D(T low, T high)
        {
            min = low;
            max = high;
        }
    }

    public enum E_FieldType
    {
        Empty,
        Test,
        Forest,
        Snow,
        Desert,
        Castle,
        Max
    }
    public enum E_GridPosition
    {
        Empty,
        LeftUp,
        Up,
        RightUp,
        Left,
        Central,
        Right,
        LeftDown,
        Down,
        RightDown,
        Max // EnumSize
    }

    public enum SFXType
    {
        Attack,
        Hit,
        Skill,
        Die,
        FootStep,
        Max
    }

    public static class GridPositionUtil
    {
        private static readonly Vector3Int[] GridOffsets = new Vector3Int[(int)E_GridPosition.Max]
        {
        new Vector3Int( 0, 0, -2), // Empty
        new Vector3Int(-1, 0, 1), // LeftUp
        new Vector3Int( 0, 0, 1), // Up
        new Vector3Int( 1, 0, 1), // RightUp
        new Vector3Int(-1, 0, 0), // Left
        new Vector3Int( 0, 0, 0), // Central
        new Vector3Int( 1, 0, 0), // Right
        new Vector3Int(-1, 0,-1), // LeftDown
        new Vector3Int( 0, 0,-1), // Down
        new Vector3Int( 1, 0,-1), // RightDown
        };
        //WorldPosition
        public static Vector3 GetGridPosition(E_GridPosition gridPosition, float cellSize, Vector3 origin)
        {
            if (gridPosition < 0 || gridPosition >= E_GridPosition.Max)
            {
                Debug.LogError($"Unsupported grid position: {gridPosition}");
                return origin;
            }

            Vector3Int offset = GridOffsets[(int)gridPosition];
            return origin + new Vector3(offset.x * cellSize , 0 , offset.z * cellSize);
        }
        //Relative Position
        public static Vector3 GetGridPosition(E_GridPosition gridPosition, float cellSize, Transform originTransform)
        {
            if (gridPosition < 0 || gridPosition >= E_GridPosition.Max)
            {
                Debug.LogError($"Unsupported grid position: {gridPosition}");
                return originTransform.position;
            }

            Vector3Int offset = GridOffsets[(int)gridPosition];
            Vector3 localOffset = new Vector3(offset.x * cellSize, 0, offset.z * cellSize);

            Vector3 rotatedOffset = originTransform.rotation * localOffset;
            return originTransform.position + rotatedOffset;
        }
    }

    [System.Serializable]
    public class HeroSpawnData
    {
        public string heroID;       // ex: "Knight", "Archer"
        public E_GridPosition gridPosition; //
    }



}