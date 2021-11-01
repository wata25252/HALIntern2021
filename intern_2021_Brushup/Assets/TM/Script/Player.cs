using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TM
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private uint _crewCount = 0;   // 乗員数

        public uint CrewCount 
        { 
            get => _crewCount; 
            set => _crewCount = value;
        }
    }
}