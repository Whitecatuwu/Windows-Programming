using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using DrawingModel;
using DrawingForm.PresentationModel;
using DrawingShape;
using DrawingState;

namespace DrawingForm
{
    public partial class Form1 : Form
    {
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

            _model.AddedShapeEvent += AddGridViewRow;
            _model.AddedShapeEvent += UpdateView;
            _model.RemovedShapeEvent += UpdateView;
            _model.RemovedShapeEvent += RemoveGridViewRow;
            _model.SelectingCompletedEvent += UpdateView;
            _model.SelectingCompletedEvent += delegate { Cursor = Cursors.Default; };
            _model.SelectingEvent += UpdateView;
            _model.SelectingEvent += delegate { Cursor = Cursors.Cross; };
            _model.SelectedShapeEvent += UpdateView;
            _model.MovedShapesEvent += UpdateGridView;
            _model.MovingShapesEvent += UpdateView;

            _pModel = new PresentationModel.PresentationModel(_model);
            _pModel.ChangedModeEvent += RefreshToolStrip;
            _pModel.GotErrorInputEvent += delegate { MessageBox.Show("資料輸入有誤!"); };
            _pModel.GotNullShapeTypeEvent += delegate { MessageBox.Show("請選擇形狀!"); };

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
            _model.OnPaint(new WindowsFormsGraphicsAdaptor(e.Graphics));
        }

        private void UpdateView()
        {
            Invalidate(true);
        }

        private void RefreshToolStrip()
        {
            toolStripStartButton.Checked = _pModel.IsStartEnable;
            toolStripTerminatorButton.Checked = _pModel.IsTerminatorEnable;
            toolStripProcessButton.Checked = _pModel.IsProcessEnable;
            toolStripDecisionButton.Checked = _pModel.IsDecisionEnable;
            toolStripSelectButton.Checked = _pModel.IsSelectEnable;
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

        private void TextBoxText_TextChanged(object sender, EventArgs e)
        {
            //nothing
        }

        private void TextBoxX_TextChanged(object sender, EventArgs e)
        {
            if (_pModel.CheckInput(1, ((TextBox)sender).Text))
                labelX.ForeColor = Color.Black;
            else
                labelX.ForeColor = Color.Red;
        }

        private void TextBoxY_TextChanged(object sender, EventArgs e)
        {
            if (_pModel.CheckInput(2, ((TextBox)sender).Text))
                labelY.ForeColor = Color.Black;
            else
                labelY.ForeColor = Color.Red;
        }

        private void TextBoxH_TextChanged(object sender, EventArgs e)
        {
            if (_pModel.CheckInput(3, ((TextBox)sender).Text))
                labelH.ForeColor = Color.Black;
            else
                labelH.ForeColor = Color.Red;
        }

        private void TextBoxW_TextChanged(object sender, EventArgs e)
        {
            if (_pModel.CheckInput(4, ((TextBox)sender).Text))
                labelW.ForeColor = Color.Black;
            else
                labelW.ForeColor = Color.Red;
        }
    }
}
