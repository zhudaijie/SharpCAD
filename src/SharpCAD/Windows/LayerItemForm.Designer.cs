namespace SharpCAD.Windows
{
    partial class LayerItemForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textboxName = new System.Windows.Forms.TextBox();
            this.lblLayerItemLayerName = new System.Windows.Forms.Label();
            this.lblLayerItemLayerColor = new System.Windows.Forms.Label();
            this.comboColor = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblLayerItemLayerDesc = new System.Windows.Forms.Label();
            this.textboxDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textboxName
            // 
            this.textboxName.Location = new System.Drawing.Point(105, 17);
            this.textboxName.Name = "textboxName";
            this.textboxName.Size = new System.Drawing.Size(185, 25);
            this.textboxName.TabIndex = 0;
            // 
            // label1
            // 
            this.lblLayerItemLayerName.AutoSize = true;
            this.lblLayerItemLayerName.Location = new System.Drawing.Point(10, 22);
            this.lblLayerItemLayerName.Name = "label1";
            this.lblLayerItemLayerName.Size = new System.Drawing.Size(67, 15);
            this.lblLayerItemLayerName.TabIndex = 1;
            this.lblLayerItemLayerName.Text = "图层名称";
            this.lblLayerItemLayerName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.lblLayerItemLayerColor.AutoSize = true;
            this.lblLayerItemLayerColor.Location = new System.Drawing.Point(10, 91);
            this.lblLayerItemLayerColor.Name = "label2";
            this.lblLayerItemLayerColor.Size = new System.Drawing.Size(37, 15);
            this.lblLayerItemLayerColor.TabIndex = 3;
            this.lblLayerItemLayerColor.Text = "颜色";
            this.lblLayerItemLayerColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboColor
            // 
            this.comboColor.FormattingEnabled = true;
            this.comboColor.Location = new System.Drawing.Point(105, 87);
            this.comboColor.Name = "comboColor";
            this.comboColor.Size = new System.Drawing.Size(185, 23);
            this.comboColor.TabIndex = 4;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(56, 166);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(92, 34);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(168, 166);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 34);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.lblLayerItemLayerDesc.AutoSize = true;
            this.lblLayerItemLayerDesc.Location = new System.Drawing.Point(10, 58);
            this.lblLayerItemLayerDesc.Name = "label3";
            this.lblLayerItemLayerDesc.Size = new System.Drawing.Size(37, 15);
            this.lblLayerItemLayerDesc.TabIndex = 8;
            this.lblLayerItemLayerDesc.Text = "描述";
            this.lblLayerItemLayerDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textboxDescription
            // 
            this.textboxDescription.Location = new System.Drawing.Point(105, 53);
            this.textboxDescription.Name = "textboxDescription";
            this.textboxDescription.Size = new System.Drawing.Size(185, 25);
            this.textboxDescription.TabIndex = 7;
            // 
            // LayerItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 207);
            this.Controls.Add(this.lblLayerItemLayerDesc);
            this.Controls.Add(this.textboxDescription);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.comboColor);
            this.Controls.Add(this.lblLayerItemLayerColor);
            this.Controls.Add(this.lblLayerItemLayerName);
            this.Controls.Add(this.textboxName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LayerItemForm";
            this.Text = "图层";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textboxName;
        private System.Windows.Forms.Label lblLayerItemLayerName;
        private System.Windows.Forms.Label lblLayerItemLayerColor;
        private System.Windows.Forms.ComboBox comboColor;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblLayerItemLayerDesc;
        private System.Windows.Forms.TextBox textboxDescription;
    }
}