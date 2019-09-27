using System;
using System.Collections.Generic;

using SharpCAD.Colors;

namespace SharpCAD.ApplicationServices
{
    /// <summary>
    /// 常用颜色集合
    /// </summary>
    internal class CommonColors : IEnumerable<Color>
    {
        private Dictionary<Color, string> _predefinedColors = new Dictionary<Color, string>();
        private List<Color> _commonColors = new List<Color>();

        public CommonColors()
        {
            InitPredefinedColors();
        }

        /// <summary>
        /// Initialize predefined colors
        /// </summary>
        private void InitPredefinedColors()
        {
            _predefinedColors.Add(Color.ByLayer, Color.ByLayer.Name);
            _predefinedColors.Add(Color.ByBlock, Color.ByBlock.Name);

            // Red
            _predefinedColors.Add(Color.FromRGB(255, 0, 0), GlobalData.GlobalLanguage.Color_Red);
            // Yellow
            _predefinedColors.Add(Color.FromRGB(255, 255, 0), GlobalData.GlobalLanguage.Color_Yellow);
            // Green
            _predefinedColors.Add(Color.FromRGB(0, 255, 0), GlobalData.GlobalLanguage.Color_Green);
            // Cyan
            _predefinedColors.Add(Color.FromRGB(0, 255, 255), GlobalData.GlobalLanguage.Color_Cyan);
            // Blue
            _predefinedColors.Add(Color.FromRGB(0, 0, 255), GlobalData.GlobalLanguage.Color_Blue);
            // Magenta
            _predefinedColors.Add(Color.FromRGB(255, 0, 255), GlobalData.GlobalLanguage.Color_Magenta);
            // White
            _predefinedColors.Add(Color.FromRGB(255, 255, 255), GlobalData.GlobalLanguage.Color_White);
        }

        public string GetColorName(Color color)
        {
            if (_predefinedColors.ContainsKey(color))
            {
                return _predefinedColors[color];
            }
            else
            {
                return color.Name;
            }
        }

        public bool Add(Color color)
        {
            if (_predefinedColors.ContainsKey(color)
                || _commonColors.Contains(color))
            {
                return false;
            }
            else
            {
                _commonColors.Add(color);
                return true;
            }
        }

        public void Clear()
        {
            _commonColors.Clear();
        }

        #region IEnumerable<Color>
        public IEnumerator<Color> GetEnumerator()
        {
            List<Color> colors = new List<Color>(_predefinedColors.Count + _commonColors.Count);
            foreach (Color color in _predefinedColors.Keys)
            {
                colors.Add(color);
            }
            foreach (Color color in _commonColors)
            {
                colors.Add(color);
            }

            return colors.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
