using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SharpCAD.DatabaseServices;
using SharpCAD.Windows;

namespace SharpCAD.Commands
{
    /// <summary>
    /// 修改图层命令
    /// </summary>
    internal class ModifyLayerCmd : Command
    {
        private readonly Layer _layer = null;
        private Layer _originalLayerCopy = null;
        private Layer _resultLayer = null;

        public ModifyLayerCmd(Layer layer)
        {
            _layer = layer;
            _originalLayerCopy = _layer.Clone() as Layer;
            _resultLayer = _layer.Clone() as Layer;
        }

        public override void Initialize()
        {
            LayerItemForm dlg = new LayerItemForm(LayerItemForm.Mode.Modify, _layer, this.database);
            dlg.StartPosition = FormStartPosition.CenterParent;
            DialogResult dlgRet = dlg.ShowDialog();
            if (dlgRet == DialogResult.OK)
            {
                _resultLayer = dlg.layer;
                if (_resultLayer.name == _layer.name
                    && _resultLayer.description == _layer.description
                    && _resultLayer.color == _layer.color)
                {
                    _mgr.CancelCurrentCommand();
                }
                else
                {
                    _mgr.FinishCurrentCommand();
                }
            }
            else
            {
                _mgr.CancelCurrentCommand();
            }
        }

        public override void Undo()
        {
            _layer.name = _originalLayerCopy.name;
            _layer.color = _originalLayerCopy.color;
            _layer.description = _originalLayerCopy.description;
        }

        public override void Redo()
        {
            this.Commit();
        }

        private void Commit()
        {
            _layer.name = _resultLayer.name;
            _layer.color = _resultLayer.color;
            _layer.description = _resultLayer.description;
        }

        public override void Finish()
        {
            this.Commit();

            base.Finish();
        }

        public override void Cancel()
        {
            base.Cancel();
        }
    }
}
