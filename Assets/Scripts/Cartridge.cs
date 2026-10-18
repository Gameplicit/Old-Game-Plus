using System;
using UnityEngine;

public class Cartridge : MonoBehaviour
{
    [SerializeField] GameCartridge gameCartridge;

    [Serializable]
    public class GameCartridge
    {
        public string name;
        public RenderTexture game;
    }
}
