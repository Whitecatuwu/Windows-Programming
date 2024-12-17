using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DrawingModel;
using DrawingForm.PresentationModel;

namespace DrawingForm
{
    public partial class Form1 : Form
    {
        Pen _pen = new Pen(Color.DarkBlue, 1);
        Pen _framePen = new Pen(Color.Red, 2);
        Panel _canvas = new DoubleBufferedPanel();
        Model _model = new Model();
        PresentationModel.PresentationModel _pModel;

        public Form1()
        {
            InitializeComponent();

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
            _model._selectingCompletedEvent += UpdateView;
            _model._selectingCompletedEvent += delegate { Cursor = Cursors.Default; };
            _model._selectingEvent += UpdateView;
            _model._selectingEvent += delegate { Cursor = Cursors.Cross; };
            _model._selectedShapeEvent += UpdateView;
            _model._movedShapesEvent += UpdateGridView;
            _model._movingShapesEvent += UpdateView;
            _model._editShapeTextEvent += delegate {};

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
                _model.RemoveShape(e.RowIndex);
            }
        }

        private void RemoveGridViewRow()
        {
            shapeGridView.Rows.RemoveAt(_model.RemovedShapeIndex);
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

        /*public void HandleClearButtonClick(object sender, System.EventArgs e)
        {
            _canvasModel.Clear();
        }*/

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
        }

        private void RefreshToolStrip()
        {
            toolStripStartButton.Checked = _pModel.IsStartEnable;
            toolStripTerminatorButton.Checked = _pModel.IsTerminatorEnable;
            toolStripProcessButton.Checked = _pModel.IsProcessEnable;
            toolStripDecisionButton.Checked = _pModel.IsDecisionEnable;
            toolStripSelectButton.Checked = _pModel.IsSelectEnable;
            toolStripLineButton.Checked = _pModel.IsLineEnable;
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

        }

        private void toolStripUndoButton_Click(object sender, EventArgs e)
        {

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
    }
}
