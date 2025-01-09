namespace DrawingForm
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.shapeGridView = new System.Windows.Forms.DataGridView();
            this.刪除 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.形狀 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.文字 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.selectedShapeType = new System.Windows.Forms.ComboBox();
            this.TextBoxText = new System.Windows.Forms.TextBox();
            this.TextBoxX = new System.Windows.Forms.TextBox();
            this.TextBoxY = new System.Windows.Forms.TextBox();
            this.TextBoxH = new System.Windows.Forms.TextBox();
            this.TextBoxW = new System.Windows.Forms.TextBox();
            this.labelText = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.labelH = new System.Windows.Forms.Label();
            this.labelW = new System.Windows.Forms.Label();
            this.ShapeAddButton = new System.Windows.Forms.Button();
            this.dataDisplay = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.說明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.關於ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripStartButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripTerminatorButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripProcessButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripDecisionButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLineButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSelectButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripRedoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripUndoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSaveButton = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.shapeGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // shapeGridView
            // 
            this.shapeGridView.AccessibleName = "shapeGridView";
            this.shapeGridView.AllowUserToAddRows = false;
            this.shapeGridView.AllowUserToDeleteRows = false;
            this.shapeGridView.AllowUserToResizeColumns = false;
            this.shapeGridView.AllowUserToResizeRows = false;
            this.shapeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.shapeGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.刪除,
            this.ID,
            this.形狀,
            this.文字,
            this.X,
            this.Y,
            this.H,
            this.W});
            this.shapeGridView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.shapeGridView.Location = new System.Drawing.Point(807, 138);
            this.shapeGridView.Name = "shapeGridView";
            this.shapeGridView.RowHeadersWidth = 51;
            this.shapeGridView.RowTemplate.Height = 27;
            this.shapeGridView.Size = new System.Drawing.Size(358, 358);
            this.shapeGridView.TabIndex = 0;
            this.shapeGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.shapeGridView_CellContentClick);
            // 
            // 刪除
            // 
            this.刪除.HeaderText = "刪除";
            this.刪除.MinimumWidth = 6;
            this.刪除.Name = "刪除";
            this.刪除.Width = 48;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 6;
            this.ID.Name = "ID";
            this.ID.Width = 32;
            // 
            // 形狀
            // 
            this.形狀.HeaderText = "形狀";
            this.形狀.MinimumWidth = 6;
            this.形狀.Name = "形狀";
            this.形狀.Width = 48;
            // 
            // 文字
            // 
            this.文字.HeaderText = "文字";
            this.文字.MinimumWidth = 6;
            this.文字.Name = "文字";
            this.文字.Width = 48;
            // 
            // X
            // 
            this.X.HeaderText = "X";
            this.X.MinimumWidth = 6;
            this.X.Name = "X";
            this.X.Width = 32;
            // 
            // Y
            // 
            this.Y.HeaderText = "Y";
            this.Y.MinimumWidth = 6;
            this.Y.Name = "Y";
            this.Y.Width = 32;
            // 
            // H
            // 
            this.H.HeaderText = "H";
            this.H.MinimumWidth = 6;
            this.H.Name = "H";
            this.H.Width = 32;
            // 
            // W
            // 
            this.W.HeaderText = "W";
            this.W.MinimumWidth = 6;
            this.W.Name = "W";
            this.W.Width = 32;
            // 
            // selectedShapeType
            // 
            this.selectedShapeType.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.selectedShapeType.FormattingEnabled = true;
            this.selectedShapeType.Items.AddRange(new object[] {
            "Start",
            "Terminator",
            "Process",
            "Decision"});
            this.selectedShapeType.Location = new System.Drawing.Point(868, 109);
            this.selectedShapeType.Name = "selectedShapeType";
            this.selectedShapeType.Size = new System.Drawing.Size(73, 23);
            this.selectedShapeType.TabIndex = 1;
            this.selectedShapeType.Text = "形狀";
            // 
            // TextBoxText
            // 
            this.TextBoxText.Location = new System.Drawing.Point(944, 107);
            this.TextBoxText.Name = "TextBoxText";
            this.TextBoxText.Size = new System.Drawing.Size(76, 25);
            this.TextBoxText.TabIndex = 2;
            this.TextBoxText.Text = " ";
            // 
            // TextBoxX
            // 
            this.TextBoxX.Location = new System.Drawing.Point(1023, 107);
            this.TextBoxX.Name = "TextBoxX";
            this.TextBoxX.Size = new System.Drawing.Size(32, 25);
            this.TextBoxX.TabIndex = 3;
            this.TextBoxX.Text = " ";
            this.TextBoxX.TextChanged += new System.EventHandler(this.TextBoxX_TextChanged);
            // 
            // TextBoxY
            // 
            this.TextBoxY.Location = new System.Drawing.Point(1058, 107);
            this.TextBoxY.Name = "TextBoxY";
            this.TextBoxY.Size = new System.Drawing.Size(32, 25);
            this.TextBoxY.TabIndex = 4;
            this.TextBoxY.Text = " ";
            this.TextBoxY.TextChanged += new System.EventHandler(this.TextBoxY_TextChanged);
            // 
            // TextBoxH
            // 
            this.TextBoxH.Location = new System.Drawing.Point(1093, 107);
            this.TextBoxH.Name = "TextBoxH";
            this.TextBoxH.Size = new System.Drawing.Size(32, 25);
            this.TextBoxH.TabIndex = 5;
            this.TextBoxH.Text = " ";
            this.TextBoxH.TextChanged += new System.EventHandler(this.TextBoxH_TextChanged);
            // 
            // TextBoxW
            // 
            this.TextBoxW.Location = new System.Drawing.Point(1128, 107);
            this.TextBoxW.Name = "TextBoxW";
            this.TextBoxW.Size = new System.Drawing.Size(32, 25);
            this.TextBoxW.TabIndex = 6;
            this.TextBoxW.Text = " ";
            this.TextBoxW.TextChanged += new System.EventHandler(this.TextBoxW_TextChanged);
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelText.Location = new System.Drawing.Point(963, 92);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(37, 15);
            this.labelText.TabIndex = 7;
            this.labelText.Text = "文字";
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.ForeColor = System.Drawing.Color.Red;
            this.labelX.Location = new System.Drawing.Point(1031, 92);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(17, 15);
            this.labelX.TabIndex = 8;
            this.labelX.Text = "X";
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.ForeColor = System.Drawing.Color.Red;
            this.labelY.Location = new System.Drawing.Point(1067, 92);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(17, 15);
            this.labelY.TabIndex = 9;
            this.labelY.Text = "Y";
            // 
            // labelH
            // 
            this.labelH.AutoSize = true;
            this.labelH.ForeColor = System.Drawing.Color.Red;
            this.labelH.Location = new System.Drawing.Point(1100, 92);
            this.labelH.Name = "labelH";
            this.labelH.Size = new System.Drawing.Size(17, 15);
            this.labelH.TabIndex = 10;
            this.labelH.Text = "H";
            // 
            // labelW
            // 
            this.labelW.AutoSize = true;
            this.labelW.ForeColor = System.Drawing.Color.Red;
            this.labelW.Location = new System.Drawing.Point(1134, 92);
            this.labelW.Name = "labelW";
            this.labelW.Size = new System.Drawing.Size(20, 15);
            this.labelW.TabIndex = 11;
            this.labelW.Text = "W";
            // 
            // ShapeAddButton
            // 
            this.ShapeAddButton.Location = new System.Drawing.Point(807, 92);
            this.ShapeAddButton.Name = "ShapeAddButton";
            this.ShapeAddButton.Size = new System.Drawing.Size(58, 45);
            this.ShapeAddButton.TabIndex = 12;
            this.ShapeAddButton.Text = "新增";
            this.ShapeAddButton.UseVisualStyleBackColor = true;
            this.ShapeAddButton.Click += new System.EventHandler(this.ShapeAddButton_Click);
            // 
            // dataDisplay
            // 
            this.dataDisplay.Location = new System.Drawing.Point(784, 54);
            this.dataDisplay.Name = "dataDisplay";
            this.dataDisplay.Size = new System.Drawing.Size(409, 590);
            this.dataDisplay.TabIndex = 13;
            this.dataDisplay.TabStop = false;
            this.dataDisplay.Text = "資料顯示";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 73);
            this.button1.TabIndex = 14;
            this.button1.Text = "page1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(24, 114);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 73);
            this.button2.TabIndex = 15;
            this.button2.Text = "page2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.說明ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1193, 27);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 說明ToolStripMenuItem
            // 
            this.說明ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.關於ToolStripMenuItem});
            this.說明ToolStripMenuItem.Name = "說明ToolStripMenuItem";
            this.說明ToolStripMenuItem.Size = new System.Drawing.Size(53, 23);
            this.說明ToolStripMenuItem.Text = "說明";
            // 
            // 關於ToolStripMenuItem
            // 
            this.關於ToolStripMenuItem.Name = "關於ToolStripMenuItem";
            this.關於ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.關於ToolStripMenuItem.Text = "關於";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStartButton,
            this.toolStripTerminatorButton,
            this.toolStripProcessButton,
            this.toolStripDecisionButton,
            this.toolStripLineButton,
            this.toolStripSelectButton,
            this.toolStripRedoButton,
            this.toolStripUndoButton,
            this.toolStripSaveButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 27);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1193, 27);
            this.toolStrip1.TabIndex = 17;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripStartButton
            // 
            this.toolStripStartButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStartButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStartButton.Image")));
            this.toolStripStartButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripStartButton.Name = "toolStripStartButton";
            this.toolStripStartButton.Size = new System.Drawing.Size(29, 24);
            this.toolStripStartButton.Text = "Start";
            this.toolStripStartButton.Click += new System.EventHandler(this.toolStripStartButton_Click);
            // 
            // toolStripTerminatorButton
            // 
            this.toolStripTerminatorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripTerminatorButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripTerminatorButton.Image")));
            this.toolStripTerminatorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripTerminatorButton.Name = "toolStripTerminatorButton";
            this.toolStripTerminatorButton.Size = new System.Drawing.Size(29, 24);
            this.toolStripTerminatorButton.Text = "Terminator";
            this.toolStripTerminatorButton.Click += new System.EventHandler(this.toolStripTerminatorButton_Click);
            // 
            // toolStripProcessButton
            // 
            this.toolStripProcessButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripProcessButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripProcessButton.Image")));
            this.toolStripProcessButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripProcessButton.Name = "toolStripProcessButton";
            this.toolStripProcessButton.Size = new System.Drawing.Size(29, 24);
            this.toolStripProcessButton.Text = "Process";
            this.toolStripProcessButton.Click += new System.EventHandler(this.toolStripProcessButton_Click);
            // 
            // toolStripDecisionButton
            // 
            this.toolStripDecisionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDecisionButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDecisionButton.Image")));
            this.toolStripDecisionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDecisionButton.Name = "toolStripDecisionButton";
            this.toolStripDecisionButton.Size = new System.Drawing.Size(29, 24);
            this.toolStripDecisionButton.Text = "Decision";
            this.toolStripDecisionButton.Click += new System.EventHandler(this.toolStripDecisionButton_Click);
            // 
            // toolStripLineButton
            // 
            this.toolStripLineButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLineButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLineButton.Image")));
            this.toolStripLineButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLineButton.Name = "toolStripLineButton";
            this.toolStripLineButton.Size = new System.Drawing.Size(29, 24);
            this.toolStripLineButton.Text = "Line";
            this.toolStripLineButton.Click += new System.EventHandler(this.toolStripLineButton_Click);
            // 
            // toolStripSelectButton
            // 
            this.toolStripSelectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSelectButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSelectButton.Image")));
            this.toolStripSelectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSelectButton.Name = "toolStripSelectButton";
            this.toolStripSelectButton.Size = new System.Drawing.Size(29, 24);
            this.toolStripSelectButton.Text = "Select";
            this.toolStripSelectButton.ToolTipText = "Select";
            this.toolStripSelectButton.Click += new System.EventHandler(this.toolStripSelectButton_Click);
            // 
            // toolStripRedoButton
            // 
            this.toolStripRedoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripRedoButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripRedoButton.Image")));
            this.toolStripRedoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRedoButton.Name = "toolStripRedoButton";
            this.toolStripRedoButton.Size = new System.Drawing.Size(29, 24);
            this.toolStripRedoButton.Text = "Redo";
            this.toolStripRedoButton.Click += new System.EventHandler(this.toolStripRedoButton_Click);
            // 
            // toolStripUndoButton
            // 
            this.toolStripUndoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripUndoButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripUndoButton.Image")));
            this.toolStripUndoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripUndoButton.Name = "toolStripUndoButton";
            this.toolStripUndoButton.Size = new System.Drawing.Size(29, 24);
            this.toolStripUndoButton.Text = "Undo";
            this.toolStripUndoButton.Click += new System.EventHandler(this.toolStripUndoButton_Click);
            // 
            // toolStripSaveButton
            // 
            this.toolStripSaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSaveButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSaveButton.Image")));
            this.toolStripSaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSaveButton.Name = "toolStripSaveButton";
            this.toolStripSaveButton.Size = new System.Drawing.Size(29, 24);
            this.toolStripSaveButton.Text = "Save";
            this.toolStripSaveButton.Click += new System.EventHandler(this.toolStripSaveButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(0, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(176, 608);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "頁數列表";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 630);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.ShapeAddButton);
            this.Controls.Add(this.labelW);
            this.Controls.Add(this.labelH);
            this.Controls.Add(this.labelY);
            this.Controls.Add(this.labelX);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.TextBoxW);
            this.Controls.Add(this.TextBoxH);
            this.Controls.Add(this.TextBoxY);
            this.Controls.Add(this.TextBoxX);
            this.Controls.Add(this.TextBoxText);
            this.Controls.Add(this.selectedShapeType);
            this.Controls.Add(this.shapeGridView);
            this.Controls.Add(this.dataDisplay);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "DrawingForm";
            ((System.ComponentModel.ISupportInitialize)(this.shapeGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox selectedShapeType;
        private System.Windows.Forms.TextBox TextBoxText;
        private System.Windows.Forms.TextBox TextBoxX;
        private System.Windows.Forms.TextBox TextBoxY;
        private System.Windows.Forms.TextBox TextBoxH;
        private System.Windows.Forms.TextBox TextBoxW;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelH;
        private System.Windows.Forms.Label labelW;
        private System.Windows.Forms.DataGridViewTextBoxColumn 刪除;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn 形狀;
        private System.Windows.Forms.DataGridViewTextBoxColumn 文字;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn H;
        private System.Windows.Forms.DataGridViewTextBoxColumn W;
        private System.Windows.Forms.Button ShapeAddButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 說明ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 關於ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripStartButton;
        private System.Windows.Forms.ToolStripButton toolStripTerminatorButton;
        private System.Windows.Forms.ToolStripButton toolStripProcessButton;
        private System.Windows.Forms.ToolStripButton toolStripDecisionButton;
        private System.Windows.Forms.ToolStripButton toolStripSelectButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripButton toolStripUndoButton;
        private System.Windows.Forms.ToolStripButton toolStripRedoButton;
        private System.Windows.Forms.ToolStripButton toolStripLineButton;
        public System.Windows.Forms.DataGridView shapeGridView;
        public System.Windows.Forms.GroupBox dataDisplay;
        private System.Windows.Forms.ToolStripButton toolStripSaveButton;
    }
}

