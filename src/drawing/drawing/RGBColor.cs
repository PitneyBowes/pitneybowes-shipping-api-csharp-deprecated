using System;
using System.Collections.Generic;


namespace PitneyBowes.Developer.Drawing
{
    public struct RGBColor
    {
        private static bool _initialized;
        private static Dictionary<string, RGBColor> _colorPallet = new Dictionary<string, RGBColor>();

        public static void AddColor(string name, byte red, byte green, byte blue)
        {
            if (!_colorPallet.ContainsKey(name)) _colorPallet.Add(name, new RGBColor(red, green, blue)); ;
        }
        public static RGBColor Color(string name)
        {
            InitializeColors();
            if (_colorPallet.ContainsKey(name)) return _colorPallet[name];
            throw new Exception("Unknown color");
        }
        private static void InitializeColors()
        {
            if (_initialized) return;
            AddColor("Black", 0, 0, 0);
            AddColor("White", 0xFF, 0xFF, 0xFF);
            _initialized = true;
        }
        public RGBColor(RGBColor color)
        {
            Red = color.Red;
            Green = color.Green;
            Blue = color.Blue;
        }
        public RGBColor(string name) : this( RGBColor.Color(name))
        {
        }
        public RGBColor(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public byte Red { get; }
        public byte Green { get; }
        public byte Blue { get; }
    }
}
