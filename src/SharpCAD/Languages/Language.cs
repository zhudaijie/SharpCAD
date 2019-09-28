using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace SharpCAD
{
    public class Language
    {
        #region 界面UI字段
        public string Menu_File = "";
        public string MenuItem_New = "";
        public string MenuItem_Open = "";
        public string MenuItem_Save = "";
        public string MenuItem_SaveAs = "";

        public string Menu_Edit = "";
        public string MenuItem_Undo = "";
        public string MenuItem_Redo = "";

        public string Menu_Format = "";
        public string MenuItem_Layer = "";

        public string Menu_Draw = "";
        public string MenuItem_Point = "";
        public string MenuItem_Line = "";
        public string MenuItem_Ray = "";
        public string MenuItem_XLine = "";
        public string MenuItem_Polyline = "";
        public string MenuItem_Polygon = "";
        public string MenuItem_Rectangle = "";
        public string MenuItem_Circle = "";
        public string MenuItem_Ellipse = "";
        public string MenuItem_Arc = "";

        public string Menu_Modify = "";
        public string MenuItem_Erase = "";
        public string MenuItem_Copy = "";
        public string MenuItem_Mirror = "";
        public string MenuItem_Offset = "";
        public string MenuItem_Move = "";

        public string Document_New = "";
        public string Document_LayerManger = "";
        public string Document_LayerName = "";
        public string Document_LayerDesc = "";
        public string Document_LayerColor = "";
        public string Document_LayerLineType = "";
        public string Document_LayerLock = "";
        public string Document_LayerItemTitle = "";
        public string Document_LayerItemLayerName = "";
        public string Document_LayerItemLayerDesc = "";
        public string Document_LayerItemLayerColor = "";
        public string Document_SaveFilter = "";

        public string Color_Red = "";
        public string Color_Yellow = "";
        public string Color_Green = "";
        public string Color_Cyan = "";
        public string Color_Blue = "";
        public string Color_Magenta = "";
        public string Color_White = "";
        public string Color_Choose = "";

        public string Button_Modify = "";
        public string Button_Add = "";
        public string Button_Delete = "";

        public string Menu_Tool = "";
        public string Menu_Help = "";
        #endregion


        protected Dictionary<string, string> DicLanguage = new Dictionary<string, string>();
        public Language()
        {
            XmlLoad(GlobalData.SystemLanguage);
            BindLanguageText();
        }

        /// <summary>
        /// Read XML and put it in memory
        /// </summary>
        /// <param name="language"></param>
        protected void XmlLoad(string language)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                string address = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Languages\" + language + ".xml");
                doc.Load(address);
                XmlElement root = doc.DocumentElement;

                XmlNodeList nodeLst1 = root.ChildNodes;
                foreach (XmlNode item in nodeLst1)
                {
                    DicLanguage.Add(item.Name, item.InnerText);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }

        public void BindLanguageText()
        {
            Menu_File = DicLanguage["Menu_File"];
            MenuItem_New = DicLanguage["MenuItem_New"];
            MenuItem_Open = DicLanguage["MenuItem_Open"];
            MenuItem_Save = DicLanguage["MenuItem_Save"];
            MenuItem_SaveAs = DicLanguage["MenuItem_SaveAs"];

            Menu_Edit = DicLanguage["Menu_Edit"];
            MenuItem_Undo = DicLanguage["MenuItem_Undo"];
            MenuItem_Redo = DicLanguage["MenuItem_Redo"];

            Menu_Format = DicLanguage["Menu_Format"];
            MenuItem_Layer = DicLanguage["MenuItem_Layer"];

            Menu_Draw = DicLanguage["Menu_Draw"];
            MenuItem_Point = DicLanguage["MenuItem_Point"];
            MenuItem_Line = DicLanguage["MenuItem_Line"];
            MenuItem_Ray = DicLanguage["MenuItem_Ray"];
            MenuItem_XLine = DicLanguage["MenuItem_XLine"];
            MenuItem_Polyline = DicLanguage["MenuItem_Polyline"];
            MenuItem_Polygon = DicLanguage["MenuItem_Polygon"];
            MenuItem_Rectangle = DicLanguage["MenuItem_Rectangle"];
            MenuItem_Circle = DicLanguage["MenuItem_Circle"];
            MenuItem_Ellipse = DicLanguage["MenuItem_Ellipse"];
            MenuItem_Arc = DicLanguage["MenuItem_Arc"];

            Menu_Modify = DicLanguage["Menu_Modify"];
            MenuItem_Erase = DicLanguage["MenuItem_Erase"];
            MenuItem_Copy = DicLanguage["MenuItem_Copy"];
            MenuItem_Mirror = DicLanguage["MenuItem_Mirror"];
            MenuItem_Offset = DicLanguage["MenuItem_Offset"];
            MenuItem_Move = DicLanguage["MenuItem_Move"];

            Document_New = DicLanguage["Document_New"];
            Document_LayerManger = DicLanguage["Document_LayerManger"];
            Document_LayerName = DicLanguage["Document_LayerName"];
            Document_LayerDesc = DicLanguage["Document_LayerDesc"];
            Document_LayerColor = DicLanguage["Document_LayerColor"];
            Document_LayerLineType = DicLanguage["Document_LayerLineType"];
            Document_LayerLock = DicLanguage["Document_LayerLock"];
            Document_LayerItemTitle = DicLanguage["Document_LayerItemTitle"];
            Document_LayerItemLayerName = DicLanguage["Document_LayerItemLayerName"];
            Document_LayerItemLayerDesc = DicLanguage["Document_LayerItemLayerDesc"];
            Document_LayerItemLayerColor = DicLanguage["Document_LayerItemLayerColor"];
            Document_SaveFilter = DicLanguage["Document_SaveFilter"];

            Color_Red = DicLanguage["Color_Red"];
            Color_Yellow = DicLanguage["Color_Yellow"];
            Color_Green = DicLanguage["Color_Green"];
            Color_Cyan = DicLanguage["Color_Cyan"];
            Color_Blue = DicLanguage["Color_Blue"];
            Color_Magenta = DicLanguage["Color_Magenta"];
            Color_White = DicLanguage["Color_White"];
            Color_Choose = DicLanguage["Color_Choose"];

            Button_Modify = DicLanguage["Button_Modify"];
            Button_Add = DicLanguage["Button_Add"];
            Button_Delete = DicLanguage["Button_Delete"];

            Menu_Tool = DicLanguage["Menu_Tool"];
            Menu_Help = DicLanguage["Menu_Help"];
        }
    }
}
