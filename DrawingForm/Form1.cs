﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DrawingModel;
using DrawingForm.PresentationModel;
using DrawingCommand;
using Hw2;
using System.Timers;

namespace DrawingForm
{
    public partial class Form1 : Form
    {
        Pen _pen = new Pen(Color.DarkBlue, 1);
        Pen _framePen = new Pen(Color.Red, 2);
        Panel _canvas = new DoubleBufferedPanel();
        Model _model = new Model();
        PresentationModel.PresentationModel _pModel;
        System.Threading.Timer _autoSaveTimer;

        const string TITLE = "DrawingForm";
        const string SAVING_TITLE = "DrawingForm (Auto Saving...)";

        public Form1()
        {
            InitializeComponent();

            _autoSaveTimer = new System.Threading.Timer(AutoSave, null, 0, 30000);

            _canvas.Parent = this;
            _canvas.Dock = DockStyle.Fill;
            _canvas.BackColor = Color.LightYellow;
            _canvas.MouseDown += HandleCanvasPointerPressed;
            _canvas.MouseUp += HandleCanvasPointerReleased;
            _canvas.MouseMove += HandleCanvasPointerMoved;
            _canvas.Paint += HandleCanvasPaint;
            _canvas.MouseDoubleClick += HandleCanvasPointerDoubleClick;

            _model._addedShapeEvent += AddGridViewRow;
            _model._addedShapeEvent += UpdateView;
            _model._removedShapeEvent += UpdateView;
            _model._removedShapeEvent += RemoveGridViewRow;
            _model._insertedShapeEvent += InsertGridView;
            _model._insertedShapeEvent += UpdateView;
            _model._selectingCompletedEvent += UpdateView;
            _model._selectingCompletedEvent += delegate { Cursor = Cursors.Default; };
            _model._selectingFailedEvent += UpdateView;
            _model._selectingEvent += UpdateView;
            _model._selectingEvent += delegate { Cursor = Cursors.Cross; };
            _model._selectedShapeEvent += UpdateView;
            _model._movedShapesEvent += UpdateGridView;
            _model._movedShapesEvent += UpdateView;
            _model._movingShapesEvent += UpdateView;
            _model._editShapeTextEvent += TextEdit;
            _model._shapeTextEditedEvent += UpdateView;
            _model._shapeTextEditedEvent += UpdateGridView;
            _model._commandExecutedEvent += RefreshToolStrip;
            _model._addedLineEvent += UpdateView;
            _model._removedLineEvent += UpdateView;
            _model._touchedShapeEvent += UpdateView;
            _model._saveEvent += RefreshToolStrip;
            _model._saveFailedEvent += RefreshToolStrip;
            _model._saveFailedEvent += delegate { MessageBox.Show("存檔失敗 !"); };
            _model._autoSaveEvent += delegate { this.Invoke(new Action(() => this.Text = SAVING_TITLE)); };
            _model._autoSaveFinishEvent += delegate { this.Invoke(new Action(() => this.Text = TITLE)); };
            _model._loadEvent += ClearGridView;

            this._pModel = new PresentationModel.PresentationModel(_model);
            _pModel._changedModeEvent += RefreshToolStrip;
            _pModel._gotErrorInputEvent += delegate { MessageBox.Show("資料輸入有誤!"); };
            _pModel._gotNullShapeTypeEvent += delegate { MessageBox.Show("請選擇形狀!"); };

            ShapeAddButton.DataBindings.Add("Enabled", _pModel, "IsAddButtonEnabled");

            RefreshToolStrip();
        }

        private void ShapeAddButton_Click(object sender, EventArgs e)
        {
            _pModel.InputDatas = new string[]
            {
                TextBoxText.Text,
                TextBoxX.Text,
                TextBoxY.Text,
                TextBoxH.Text,
                TextBoxW.Text
            };
            _pModel.AddShape(selectedShapeType.SelectedItem);
        }

        private void shapeGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                //_model.RemoveShape(e.RowIndex);
                _model.ExeCommand(new DeleteShapeCommand(_model, e.RowIndex));
            }
        }

        private void RemoveGridViewRow()
        {
            shapeGridView.Rows.RemoveAt(_model.RemovedShapeIndex);
        }

        private void ClearGridView()
        {
            shapeGridView.Rows.Clear();
        }

        private void AddGridViewRow()
        {
            int index = _model.ShapesSize - 1;
            string[] row = new string[] { "刪除" }.Concat(_pModel.GetShapeData(index)).ToArray();
            shapeGridView.Rows.Add(row);
        }

        private void UpdateGridView()
        {
            int index = _model.UpdatedShapeIndex;
            string[] row = new string[] { "刪除" }.Concat(_pModel.GetShapeData(index)).ToArray();
            shapeGridView.Rows.RemoveAt(index);
            shapeGridView.Rows.Insert(index, row);
        }

        private void InsertGridView()
        {
            int index = _model.InsertedShapeIndex;
            string[] row = new string[] { "刪除" }.Concat(_pModel.GetShapeData(index)).ToArray();
            shapeGridView.Rows.Insert(index, row);
        }

        private void HandleCanvasPointerPressed(object sender, MouseEventArgs e)
        {
            _model.MouseDown(e.X, e.Y);
        }

        private void HandleCanvasPointerReleased(object sender, MouseEventArgs e)
        {
            _model.MouseUp(e.X, e.Y);
        }

        private void HandleCanvasPointerMoved(object sender, MouseEventArgs e)
        {
            _model.MouseMove(e.X, e.Y);
        }

        private void HandleCanvasPaint(object sender, PaintEventArgs e)
        {
            _model.OnPaint(new WindowsFormsGraphicsAdaptor(e.Graphics, _pen, _framePen));
        }
        private void HandleCanvasPointerDoubleClick(object sender, MouseEventArgs e)
        {
            _model.MouseDoubleClick(e.X, e.Y);
        }

        private void UpdateView()
        {
            //Invalidate(true);
            _canvas.Invalidate(true);
            _model.Changed = true;
        }

        private void RefreshToolStrip()
        {
            toolStripStartButton.Checked = _pModel.IsStartEnable;
            toolStripTerminatorButton.Checked = _pModel.IsTerminatorEnable;
            toolStripProcessButton.Checked = _pModel.IsProcessEnable;
            toolStripDecisionButton.Checked = _pModel.IsDecisionEnable;
            toolStripSelectButton.Checked = _pModel.IsSelectEnable;
            toolStripLineButton.Checked = _pModel.IsLineEnable;
            toolStripRedoButton.Enabled = _pModel.IsRedoEnable;
            toolStripUndoButton.Enabled = _pModel.IsUndoEnable;
            toolStripSaveButton.Enabled = _pModel.IsSaveEnable;
        }

        private void toolStripStartButton_Click(object sender, EventArgs e)
        {
            _pModel.SetDrawingMode(DrawingMode.START);
        }

        private void toolStripTerminatorButton_Click(object sender, EventArgs e)
        {
            _pModel.SetDrawingMode(DrawingMode.TERMINATOR);
        }

        private void toolStripProcessButton_Click(object sender, EventArgs e)
        {
            _pModel.SetDrawingMode(DrawingMode.PROCESS);
        }

        private void toolStripDecisionButton_Click(object sender, EventArgs e)
        {
            _pModel.SetDrawingMode(DrawingMode.DECISION);
        }

        private void toolStripSelectButton_Click(object sender, EventArgs e)
        {
            _pModel.SetDrawingMode(DrawingMode.POINTER);
        }
        private void toolStripLineButton_Click(object sender, EventArgs e)
        {
            _pModel.SetDrawingMode(DrawingMode.LINE);
        }

        private void toolStripRedoButton_Click(object sender, EventArgs e)
        {
            _model.Redo();
        }

        private void toolStripUndoButton_Click(object sender, EventArgs e)
        {
            _model.Undo();
        }

        private void TextBoxText_TextChanged(object sender, EventArgs e)
        {
            //nothing
        }

        private void TextBoxX_TextChanged(object sender, EventArgs e)
        {
            _pModel.InputData(1, ((TextBox)sender).Text);
            labelX.ForeColor = Color.FromArgb(_pModel.GetXStateColor);
        }

        private void TextBoxY_TextChanged(object sender, EventArgs e)
        {
            _pModel.InputData(2, ((TextBox)sender).Text);
            labelY.ForeColor = Color.FromArgb(_pModel.GetYStateColor);
        }

        private void TextBoxH_TextChanged(object sender, EventArgs e)
        {
            _pModel.InputData(3, ((TextBox)sender).Text);
            labelH.ForeColor = Color.FromArgb(_pModel.GetHStateColor);
        }

        private void TextBoxW_TextChanged(object sender, EventArgs e)
        {
            _pModel.InputData(4, ((TextBox)sender).Text);
            labelW.ForeColor = Color.FromArgb(_pModel.GetWStateColor);
        }

        private void TextEdit()
        {
            TextEditForm textEditForm = new TextEditForm();
            DialogResult dialogResult = textEditForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                _model.ExeCommand(new TextChangeCommand(_model, textEditForm.GetText()));
            }
        }

        [STAThread]
        private void toolStripSaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "儲存檔案",
                Filter = "(*.mydrawing)|*.mydrawing|文字檔案(*.txt)|*.txt|所有檔案(*.*)|*.*",
                DefaultExt = "mydrawing",
                FileName = "Unnamed"
            };
            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _model.Save(saveFileDialog.FileName);
            }
        }

        [STAThread]
        private void toolStripLoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "選擇檔案",
                Filter = "(*.mydrawing)|*.mydrawing|文字檔案(*.txt)|*.txt|所有檔案(*.*)|*.*",
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            DialogResult result = MessageBox.Show(
                "將會覆蓋目前進度，且無法撤銷，是否確定要執行該操作？",  // 訊息內容
                "確認操作",              // 視窗標題
                MessageBoxButtons.OKCancel, // 按鈕選項
                MessageBoxIcon.Question     // 圖示樣式
            );

            if (result == DialogResult.OK)
            {
                _model.Load(openFileDialog.FileName);
            }
        }
        private void AutoSave(object state)
        {
            _model.AutoSave();
        }
    }
}
