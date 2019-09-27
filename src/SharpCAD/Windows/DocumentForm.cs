using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SharpCAD.DatabaseServices;
using SharpCAD.ApplicationServices;

namespace SharpCAD.Windows
{
    public partial class DocumentForm : Form
    {
        private ToolStripMgr _toolStripMgr = new ToolStripMgr();
        public ToolStripMgr toolstripMgr
        {
            get { return _toolStripMgr; }
        }

        private Canvas _canvas = null;
        private Document _document = null;
        internal Document document
        {
            get { return _document; }
        }
        private Presenter _presenter = null;
        internal Presenter presenter
        {
            get { return _presenter; }
        }

        private Database database
        {
            get { return _document.database; }
        }

        public DocumentForm()
        {
            InitializeComponent();

            //
            _canvas = new Canvas();
            _document = new Document();
            _presenter = new Presenter(_canvas, _document);

            _canvas.Dock = DockStyle.Fill;
            this.Controls.Add(_canvas);

            //
            SetupToolStripUI();

            //
            _document.database.layerTable.itemAdded += this.OnAddLayer;
            _document.database.layerTable.itemRemoved += this.OnRemoveLayer;
            _document.currentLayerChanged += this.OnDocumentCurrLayerChanged;
            _document.currentColorChanged += this.OnDocumentCurrColorChanged;
            
            //
            //Layer layer = new Layer("test");
            //layer.color = SharpCAD.Colors.Color.FromRGB(255, 0, 0);
            //_document.database.layerTable.Add(layer);
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        internal void Open(string fileFullPath)
        {
            _document.database.Open(fileFullPath);
            if (_document.database != null
                && _document.database.fileName != null)
            {
                this.Text = _document.database.fileName;
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        internal void Save()
        {
            _document.database.Save();
            if (_document.database != null
                && _document.database.fileName != null)
            {
                this.Text = _document.database.fileName;
            }
        }

        /// <summary>
        /// 另存为
        /// </summary>
        internal void SaveAs(string fileFullPath, bool rename = false)
        {
            _document.database.SaveAs(fileFullPath, rename);
            if (_document.database != null
                && _document.database.fileName != null)
            {
                this.Text = _document.database.fileName;
            }
        }

        /// <summary>
        /// 文件全路径
        /// </summary>
        internal string fileFullPath
        {
            get
            {
                if (_document.database != null
                    && _document.database.fileName != null)
                {
                    return _document.database.fileName;
                }
                else
                {
                    return this.Text;
                }
            }
        }

        /// <summary>
        /// 设置菜单、工具条、状态条
        /// </summary>
        private void SetupToolStripUI()
        {
            //
            MenuStrip menuMain = SetupMainMenu();

            menuMain.Visible = false;
            this.MainMenuStrip = menuMain;
            this.Controls.Add(menuMain);

            //
            SetupToolbar();
        }

        /// <summary>
        /// 设置主菜单
        /// </summary>
        /// <returns></returns>
        private MenuStrip SetupMainMenu()
        {
            MenuStrip mainMenu = _toolStripMgr.GetMenuStrip("Main", true);

            // 菜单: 编辑
            ToolStripMenuItem menuEdit = SetupMainMenu_Edit();
            mainMenu.Items.Add(menuEdit);

            // 菜单: 格式
            ToolStripMenuItem menuFormat = SetupMainMenu_Format();
            mainMenu.Items.Add(menuFormat);

            // 菜单: 绘图
            ToolStripMenuItem menuDraw = SetupMainMenu_Draw();
            mainMenu.Items.Add(menuDraw);

            // 菜单: 修改
            ToolStripMenuItem menuModify = SetupMainMenu_Modify();
            mainMenu.Items.Add(menuModify);

            return mainMenu;
        }

        /// <summary>
        /// 设置菜单: 编辑
        /// </summary>
        private ToolStripMenuItem SetupMainMenu_Edit()
        {
            ToolStripMenuItem menuEdit = new ToolStripMenuItem();
            menuEdit.Text = GlobalData.GlobalLanguage.Menu_Edit;

            // 撤销
            ToolStripMenuItem undo = _toolStripMgr.NewMenuItem(
                "edit_undo",
                GlobalData.GlobalLanguage.MenuItem_Undo,
                Resource1.edit_undo.ToBitmap(),
                this.OnEditUndo);
            undo.ShortcutKeys = Keys.Control | Keys.Z;
            menuEdit.DropDownItems.Add(undo);

            // 重做
            ToolStripMenuItem redo = _toolStripMgr.NewMenuItem(
                "edit_redo",
                GlobalData.GlobalLanguage.MenuItem_Redo,
                Resource1.edit_redo.ToBitmap(),
                this.OnEditRedo);
            redo.ShortcutKeys = Keys.Control | Keys.Y;
            menuEdit.DropDownItems.Add(redo);

            return menuEdit;
        }

        /// <summary>
        /// 设置菜单: 格式
        /// </summary>
        private ToolStripMenuItem SetupMainMenu_Format()
        {
            ToolStripMenuItem menuFormat = new ToolStripMenuItem();
            menuFormat.Text = GlobalData.GlobalLanguage.Menu_Format;

            ToolStripMenuItem menuLayer = _toolStripMgr.NewMenuItem(
                            "format_layer",
                            GlobalData.GlobalLanguage.MenuItem_Layer,
                            Resource1.format_layer,
                            this.OnFormatLayer);
            menuFormat.DropDownItems.Add(menuLayer);

            return menuFormat;
        }

        /// <summary>
        /// 设置菜单: 绘图
        /// </summary>
        private ToolStripMenuItem SetupMainMenu_Draw()
        {
            ToolStripMenuItem menuDraw = new ToolStripMenuItem();
            menuDraw.Text = GlobalData.GlobalLanguage.Menu_Draw;
            // 点
            ToolStripMenuItem point = _toolStripMgr.NewMenuItem(
                "draw_point",
                GlobalData.GlobalLanguage.MenuItem_Point,
                Resource1.draw_point,
                this.OnDrawPoint);
            menuDraw.DropDownItems.Add(point);

            // 直线
            ToolStripMenuItem lines = _toolStripMgr.NewMenuItem(
                "draw_lines",
                GlobalData.GlobalLanguage.MenuItem_Line,
                Resource1.draw_line,
                this.OnDrawLines);
            menuDraw.DropDownItems.Add(lines);

            // 射线
            ToolStripMenuItem ray = _toolStripMgr.NewMenuItem(
                "draw_ray",
                GlobalData.GlobalLanguage.MenuItem_Ray,
                Resource1.draw_ray,
                this.OnDrawRay);
            menuDraw.DropDownItems.Add(ray);

            // 构造线
            ToolStripMenuItem xline = _toolStripMgr.NewMenuItem(
                "draw_xline",
                GlobalData.GlobalLanguage.MenuItem_XLine,
                Resource1.draw_xline,
                this.OnDrawXLine);
            menuDraw.DropDownItems.Add(xline);

            // 多段线
            ToolStripMenuItem polyline = _toolStripMgr.NewMenuItem(
                "draw_polyline",
                GlobalData.GlobalLanguage.MenuItem_Polyline,
                Resource1.draw_polyline,
                this.OnDrawPolyline);
            menuDraw.DropDownItems.Add(polyline);

            // 正多边形
            ToolStripMenuItem polygon = _toolStripMgr.NewMenuItem(
                "draw_polygon",
                GlobalData.GlobalLanguage.MenuItem_Polygon,
                Resource1.draw_polygon,
                this.OnDrawPolygon);
            menuDraw.DropDownItems.Add(polygon);

            // 矩形
            ToolStripMenuItem rectangle = _toolStripMgr.NewMenuItem(
                "draw_rectangle",
                GlobalData.GlobalLanguage.MenuItem_Rectangle,
                Resource1.draw_rectangle,
                this.OnDrawRectangle);
            menuDraw.DropDownItems.Add(rectangle);

            // 圆
            ToolStripMenuItem circle = _toolStripMgr.NewMenuItem(
                "draw_circle",
                GlobalData.GlobalLanguage.MenuItem_Circle,
                Resource1.draw_circle_cr,
                this.OnDrawCircle);
            menuDraw.DropDownItems.Add(circle);

            // 椭圆
            ToolStripMenuItem ellipse = _toolStripMgr.NewMenuItem(
                "draw_ellipse",
                GlobalData.GlobalLanguage.MenuItem_Ellipse,
                Resource1.draw_ellipse_cenax,
                this.OnDrawEllipse);
            menuDraw.DropDownItems.Add(ellipse);

            // 圆弧
            ToolStripMenuItem arc = _toolStripMgr.NewMenuItem(
                "draw_arc",
                GlobalData.GlobalLanguage.MenuItem_Arc,
                Resource1.draw_arc_cse,
                this.OnDrawArc);
            menuDraw.DropDownItems.Add(arc);

            return menuDraw;
        }

        /// <summary>
        /// 设置菜单: 修改
        /// </summary>
        private ToolStripMenuItem SetupMainMenu_Modify()
        {
            ToolStripMenuItem menuModify = new ToolStripMenuItem();
            menuModify.Text = GlobalData.GlobalLanguage.Menu_Modify;

            // 删除
            ToolStripMenuItem erase = _toolStripMgr.NewMenuItem(
                "modify_erase",
                GlobalData.GlobalLanguage.MenuItem_Erase,
                Resource1.modify_erase.ToBitmap(),
                this.OnModifyErase);
            menuModify.DropDownItems.Add(erase);

            // 复制
            ToolStripMenuItem copy = _toolStripMgr.NewMenuItem(
                "modify_copy",
                GlobalData.GlobalLanguage.MenuItem_Copy,
                Resource1.modify_copy.ToBitmap(),
                this.OnModifyCopy);
            menuModify.DropDownItems.Add(copy);

            // 镜像
            ToolStripMenuItem mirror = _toolStripMgr.NewMenuItem(
                "modify_mirror",
                GlobalData.GlobalLanguage.MenuItem_Mirror,
                Resource1.modify_mirror.ToBitmap(),
                this.OnModifyMirror);
            menuModify.DropDownItems.Add(mirror);

            // 偏移
            ToolStripMenuItem offset = _toolStripMgr.NewMenuItem(
                "modify_offset",
                GlobalData.GlobalLanguage.MenuItem_Offset,
                Resource1.modify_offset.ToBitmap(),
                this.OnModifyOffset);
            menuModify.DropDownItems.Add(offset);

            // 移动
            ToolStripMenuItem move = _toolStripMgr.NewMenuItem(
                "modify_move",
                GlobalData.GlobalLanguage.MenuItem_Move,
                Resource1.modify_move.ToBitmap(),
                this.OnModifyMove);
            menuModify.DropDownItems.Add(move);

            return menuModify;
        }

        /// <summary>
        /// 设置工具条
        /// </summary>
        private void SetupToolbar()
        {
            SetupToolbar_Edit();
            SetupToolbar_Draw();
            SetupToolbar_Modify();
            SetupToolbar_Layer();
            SetupToolbar_Property();
        }

        /// <summary>
        /// 设置工具条: 编辑
        /// </summary>
        private ToolStripButton _undoToolstripItem = null;
        private ToolStripButton _redoToolstripItem = null;
        private ToolStrip SetupToolbar_Edit()
        {
            ToolStrip editToolstrip = _toolStripMgr.GetToolStrip("Edit");

            // 撤销
            ToolStripButton undo = _toolStripMgr.NewToolStripButton("edit_undo");
            undo.ToolTipText = undo.Text;
            undo.Text = "";
            editToolstrip.Items.Add(undo);
            _undoToolstripItem = undo;

            // 重做
            ToolStripButton redo = _toolStripMgr.NewToolStripButton("edit_redo");
            redo.ToolTipText = redo.Text;
            redo.Text = "";
            editToolstrip.Items.Add(redo);
            _redoToolstripItem = redo;

            return editToolstrip;
        }

        /// <summary>
        /// 设置工具条: 绘制
        /// </summary>
        private ToolStrip SetupToolbar_Draw()
        {
            ToolStrip drawToolstrip = _toolStripMgr.GetToolStrip("Draw");

            // 点
            ToolStripButton point = _toolStripMgr.NewToolStripButton("draw_point");
            point.ToolTipText = point.Text;
            point.Text = "";
            drawToolstrip.Items.Add(point);

            // 直线
            ToolStripButton lines = _toolStripMgr.NewToolStripButton("draw_lines");
            lines.ToolTipText = lines.Text;
            lines.Text = "";
            drawToolstrip.Items.Add(lines);

            // 射线
            ToolStripButton ray = _toolStripMgr.NewToolStripButton("draw_ray");
            ray.ToolTipText = ray.Text;
            ray.Text = "";
            drawToolstrip.Items.Add(ray);

            // 构造线
            ToolStripButton xline = _toolStripMgr.NewToolStripButton("draw_xline");
            xline.ToolTipText = xline.Text;
            xline.Text = "";
            drawToolstrip.Items.Add(xline);

            // 多段线
            ToolStripButton polyline = _toolStripMgr.NewToolStripButton("draw_polyline");
            polyline.ToolTipText = polyline.Text;
            polyline.Text = "";
            drawToolstrip.Items.Add(polyline);

            // 正多边形
            ToolStripButton polygon = _toolStripMgr.NewToolStripButton("draw_polygon");
            polygon.ToolTipText = polygon.Text;
            polygon.Text = "";
            drawToolstrip.Items.Add(polygon);

            // 矩形
            ToolStripButton rectangle = _toolStripMgr.NewToolStripButton("draw_rectangle");
            rectangle.ToolTipText = rectangle.Text;
            rectangle.Text = "";
            drawToolstrip.Items.Add(rectangle);

            // 圆
            ToolStripButton circle = _toolStripMgr.NewToolStripButton("draw_circle");
            circle.ToolTipText = circle.Text;
            circle.Text = "";
            drawToolstrip.Items.Add(circle);

            // 椭圆
            ToolStripButton ellipse = _toolStripMgr.NewToolStripButton("draw_ellipse");
            ellipse.ToolTipText = ellipse.Text;
            ellipse.Text = "";
            drawToolstrip.Items.Add(ellipse);

            // 圆弧
            ToolStripButton arc = _toolStripMgr.NewToolStripButton("draw_arc");
            arc.ToolTipText = arc.Text;
            arc.Text = "";
            drawToolstrip.Items.Add(arc);

            return drawToolstrip;
        }

        /// <summary>
        /// 设置工具条: 修改
        /// </summary>
        private ToolStrip SetupToolbar_Modify()
        {
            ToolStrip modifyToolstrip = _toolStripMgr.GetToolStrip("Modify");

            // 删除
            ToolStripButton erase = _toolStripMgr.NewToolStripButton("modify_erase");
            erase.ToolTipText = erase.Text;
            erase.Text = "";
            modifyToolstrip.Items.Add(erase);

            // 复制
            ToolStripButton copy = _toolStripMgr.NewToolStripButton("modify_copy");
            copy.ToolTipText = copy.Text;
            copy.Text = "";
            modifyToolstrip.Items.Add(copy);

            // 镜像
            ToolStripButton mirror = _toolStripMgr.NewToolStripButton("modify_mirror");
            mirror.ToolTipText = mirror.Text;
            mirror.Text = "";
            modifyToolstrip.Items.Add(mirror);

            // 偏移
            ToolStripButton offset = _toolStripMgr.NewToolStripButton("modify_offset");
            offset.ToolTipText = offset.Text;
            offset.Text = "";
            modifyToolstrip.Items.Add(offset);

            // 移动
            ToolStripButton move = _toolStripMgr.NewToolStripButton("modify_move");
            move.ToolTipText = move.Text;
            move.Text = "";
            modifyToolstrip.Items.Add(move);

            return modifyToolstrip;
        }

        /// <summary>
        /// 设置工具条: 图层
        /// </summary>
        private ToolStripComboBox _toolstripLayerCombo;
        private ToolStrip SetupToolbar_Layer()
        {
            ToolStrip layerToolstrip = _toolStripMgr.GetToolStrip("Layer", true);

            // Layer management
            ToolStripButton tsbtnLayerMgr = _toolStripMgr.NewToolStripButton("format_layer");
            tsbtnLayerMgr.ToolTipText = tsbtnLayerMgr.Text;
            tsbtnLayerMgr.Text = "";
            layerToolstrip.Items.Add(tsbtnLayerMgr);

            // Layer combobox
             _toolstripLayerCombo = new ToolStripComboBox();
             _toolstripLayerCombo.DropDownStyle = ComboBoxStyle.DropDownList;
             _toolStripMgr.AddToolStripItem(_toolstripLayerCombo);
             layerToolstrip.Items.Add(_toolstripLayerCombo);

            foreach (DBTableRecord item in _document.database.layerTable)
            {
                Layer layer = item as Layer;
                ToolStripButton layerBtn = new ToolStripButton(layer.name);
                layerBtn.Tag = layer.id;
                _toolstripLayerCombo.Items.Add(layerBtn);
            }

            this.SetLayerComboValue(_document.currentLayerId);
            
            _toolstripLayerCombo.SelectedIndexChanged += this.OnLayerComboSelectedIndexChanged;
            
            return layerToolstrip;
        }

        private void OnLayerComboSelectedIndexChanged(object sender, EventArgs e)
        {
            if (_toolstripLayerCombo.SelectedItem == null)
            {
                return;
            }

            ToolStripButton layerButton = _toolstripLayerCombo.SelectedItem as ToolStripButton;
            ObjectId layerId = (ObjectId)layerButton.Tag;
            if (_document.database.layerTable.Has(layerId))
            {
                _document.currentLayerId = layerId;
            }
        }

        private void OnDocumentCurrLayerChanged(ObjectId last, ObjectId current)
        {
            this.SetLayerComboValue(current);
        }

        private int SetLayerComboValue(ObjectId layerId)
        {
            int index = -1;
            for (int i = 0; i < _toolstripLayerCombo.Items.Count; ++i)
            {
                ToolStripButton layerBtn = _toolstripLayerCombo.Items[i] as ToolStripButton;
                ObjectId oid = (ObjectId)layerBtn.Tag;
                if (oid == layerId)
                {
                    index = i;
                    break;
                }
            }

            _toolstripLayerCombo.SelectedIndex = index;
            return index;
        }

        private void AddLayer(Layer layer)
        {
            ToolStripButton layerBtn = new ToolStripButton(layer.name);
            layerBtn.Tag = layer.id;
            _toolstripLayerCombo.Items.Add(layerBtn);
        }

        private void RemoveLayer(Layer layer)
        {
            for (int i = 0; i < _toolstripLayerCombo.Items.Count; ++i)
            {
                ToolStripButton layerBtn = _toolstripLayerCombo.Items[i] as ToolStripButton;
                ObjectId layerId = (ObjectId)layerBtn.Tag;
                if (layerId == layer.id)
                {
                    _toolstripLayerCombo.Items.RemoveAt(i);
                    return;
                }
            }
        }

        private void OnAddLayer(DBTableRecord item)
        {
            Layer layer = item as Layer;
            this.AddLayer(layer);
        }

        private void OnRemoveLayer(DBTableRecord item)
        {
            Layer layer = item as Layer;
            this.RemoveLayer(layer);
        }

        /// <summary>
        /// 设置工具条: 特性
        /// </summary>
        private ToolStripComboBox _toolstripColorCombo;
        private int _colorComboCustomColorIndex = -1;
        private ToolStrip SetupToolbar_Property()
        {
            ToolStrip propertyToolstrip = _toolStripMgr.GetToolStrip("Property", true);

            _toolstripColorCombo = new ToolStripComboBox();
            _toolstripColorCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _toolStripMgr.AddToolStripItem(_toolstripColorCombo);
            propertyToolstrip.Items.Add(_toolstripColorCombo);

            foreach (SharpCAD.Colors.Color color in _document.commonColors)
            {
                ToolStripButton colorBtn = new ToolStripButton(_document.commonColors.GetColorName(color));
                colorBtn.Tag = color;
                _toolstripColorCombo.Items.Add(colorBtn);
            }

            _colorComboCustomColorIndex = _toolstripColorCombo.Items.Count;
            ToolStripButton selectColorBtn = new ToolStripButton(GlobalData.GlobalLanguage.Color_Choose);
            selectColorBtn.Tag = null;
            _toolstripColorCombo.Items.Add(selectColorBtn);

            this.SetColorComboValue(_document.currentColor);

            _toolstripColorCombo.SelectedIndexChanged += this.OnColorComboSelectedIndexChanged;

            return propertyToolstrip;
        }

        private void OnColorComboSelectedIndexChanged(object sender, EventArgs e)
        {
            if (_toolstripColorCombo.SelectedItem == null)
            {
                return;
            }

            ToolStripButton colorButton = _toolstripColorCombo.SelectedItem as ToolStripButton;
            if (colorButton.Tag == null)
            {
                ColorDialog colorDlg = new ColorDialog();
                colorDlg.AllowFullOpen = true;
                colorDlg.SolidColorOnly = true;
                DialogResult dlgRet = colorDlg.ShowDialog();
                if (dlgRet == DialogResult.OK)
                {
                    SharpCAD.Colors.Color color = SharpCAD.Colors.Color.FromColor(colorDlg.Color);
                    _document.currentColor = color;
                }
                else
                {
                    this.SetColorComboValue(_document.currentColor);
                }
            }
            else
            {
                SharpCAD.Colors.Color color = (SharpCAD.Colors.Color)colorButton.Tag;
                _document.currentColor = color;
            }
        }

        private void OnDocumentCurrColorChanged(SharpCAD.Colors.Color last, SharpCAD.Colors.Color current)
        {
            this.SetColorComboValue(current);
        }

        private int SetColorComboValue(SharpCAD.Colors.Color color)
        {
            int index = -1;
            for (int i = 0; i < _toolstripColorCombo.Items.Count; ++i)
            {
                ToolStripButton colorBtn = _toolstripColorCombo.Items[i] as ToolStripButton;
                if (colorBtn.Tag != null)
                {
                    SharpCAD.Colors.Color itemColor = (SharpCAD.Colors.Color)colorBtn.Tag;
                    if (itemColor == color)
                    {
                        index = i;
                        break;
                    }
                }
            }

            if (index == -1)
            {
                ToolStripButton colorBtn = new ToolStripButton(color.Name);
                colorBtn.Tag = color;
                _toolstripColorCombo.Items.Insert(_colorComboCustomColorIndex, colorBtn);
                index = _colorComboCustomColorIndex;
            }

            _toolstripColorCombo.SelectedIndex = index;
            return index;
        }

        private void OnEditUndo(object sender, EventArgs e)
        {
            Commands.Edit.UndoCmd cmd = new Commands.Edit.UndoCmd();
            _presenter.OnCommand(cmd);
        }

        private void OnEditRedo(object sender, EventArgs e)
        {
            Commands.Edit.RedoCmd cmd = new Commands.Edit.RedoCmd();
            _presenter.OnCommand(cmd);
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void OnModifyErase(object sender, EventArgs e)
        {
            Commands.Modify.DeleteCmd cmd = new Commands.Modify.DeleteCmd();
            _presenter.OnCommand(cmd);
        }

        /// <summary>
        /// 复制
        /// </summary>
        private void OnModifyCopy(object sender, EventArgs e)
        {
            Commands.Modify.CopyCmd cmd = new Commands.Modify.CopyCmd();
            _presenter.OnCommand(cmd);
        }

        /// <summary>
        /// 镜像
        /// </summary>
        private void OnModifyMirror(object sender, EventArgs e)
        {
            Commands.Modify.MirrorCmd cmd = new Commands.Modify.MirrorCmd();
            _presenter.OnCommand(cmd);
        }

        /// <summary>
        /// 偏移
        /// </summary>
        private void OnModifyOffset(object sender, EventArgs e)
        {
            Commands.Modify.OffsetCmd cmd = new Commands.Modify.OffsetCmd();
            _presenter.OnCommand(cmd);
        }

        /// <summary>
        /// 阵列
        /// </summary>
        private void OnModifyArray(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 移动
        /// </summary>
        private void OnModifyMove(object sender, EventArgs e)
        {
            Commands.Modify.MoveCmd cmd = new Commands.Modify.MoveCmd();
            _presenter.OnCommand(cmd);
        }

        /// <summary>
        /// 旋转
        /// </summary>
        private void OnModifyRotate(object sender, EventArgs e)
        {
        }

        private void OnDrawPoint(object sender, EventArgs e)
        {
            Commands.Draw.PointCmd cmd = new Commands.Draw.PointCmd();
            _presenter.OnCommand(cmd);
        }

        private void OnDrawLines(object sender, EventArgs e)
        {
            Commands.Draw.LinesChainCmd cmd = new Commands.Draw.LinesChainCmd();
            _presenter.OnCommand(cmd);
        }

        private void OnDrawXLine(object sender, EventArgs e)
        {
            Commands.Draw.XlineCmd cmd = new Commands.Draw.XlineCmd();
            _presenter.OnCommand(cmd);
        }

        private void OnDrawRay(object sender, EventArgs e)
        {
            Commands.Draw.RayCmd cmd = new Commands.Draw.RayCmd();
            _presenter.OnCommand(cmd);
        }

        private void OnDrawPolyline(object sender, EventArgs e)
        {
            Commands.Draw.PolylineCmd cmd = new Commands.Draw.PolylineCmd();
            _presenter.OnCommand(cmd);
        }

        private void OnDrawPolygon(object sender, EventArgs e)
        {
            Commands.Draw.PolygonCmd cmd = new Commands.Draw.PolygonCmd();
            _presenter.OnCommand(cmd);
        }

        private void OnDrawRectangle(object sender, EventArgs e)
        {
            Commands.Draw.RectangleCmd cmd = new Commands.Draw.RectangleCmd();
            _presenter.OnCommand(cmd);
        }

        private void OnDrawCircle(object sender, EventArgs e)
        {
            Commands.Draw.CircleCmd cmd = new Commands.Draw.CircleCmd();
            _presenter.OnCommand(cmd);
        }

        private void OnDrawEllipse(object sender, EventArgs e)
        {
            Commands.Draw.EllipseCmd cmd = new Commands.Draw.EllipseCmd();
            _presenter.OnCommand(cmd);
        }

        private void OnDrawArc(object sender, EventArgs e)
        {
            Commands.Draw.ArcCmd cmd = new Commands.Draw.ArcCmd();
            _presenter.OnCommand(cmd);
        }

        /// <summary>
        /// 图层特性管理器
        /// </summary>
        private void OnFormatLayer(object sender, EventArgs e)
        {
            LayersManagementForm.Instance.ShowDialog();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //if (m_data.IsDirty)
            //{
            //    string s = "Save Changes to " + Path.GetFileName(m_filename) + "?";
            //    DialogResult result = MessageBox.Show(this, s, Program.AppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //    if (result == DialogResult.Cancel)
            //    {
            //        e.Cancel = true;
            //        return;
            //    }
            //    if (result == DialogResult.Yes)
            //        Save();
            //}
            _toolStripMgr.DisableAll();
            base.OnFormClosing(e);
        }

        internal void UpdateUI()
        {
            ToolStripMenuItem menuUndo = _toolStripMgr.GetMenuItem("edit_undo");
            menuUndo.Enabled = _presenter.canUndo;
            _undoToolstripItem.Enabled = _presenter.canUndo;

            ToolStripMenuItem menuRedo = _toolStripMgr.GetMenuItem("edit_redo");
            menuRedo.Enabled = _presenter.canRedo;
            _redoToolstripItem.Enabled = _presenter.canRedo;
        }
    }
}
