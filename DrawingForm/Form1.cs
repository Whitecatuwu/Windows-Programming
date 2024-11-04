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

            _model._modelRemovedShape += UpdateView;
            _model._modelRemovedShape += RemoveGridViewRow;
            _model._modelAddedShape += AddGridViewRow;        
            _model._modelDrawing += UpdateView;
            _model._modelDrawing += delegate { Cursor = Cursors.Cross; };
            _model._modelDrawingCompleted += UpdateView;
            _model._modelDrawingCompleted += delegate { Cursor = Cursors.Default; };

            _pModel = new PresentationModel.PresentationModel(_model, _canvas);
            _pModel._pModelChangedMode += RefreshToolStrip;
            _pModel._pModelGetErrorInput += delegate { MessageBox.Show("資料輸入有誤!"); };
            _pModel._pModelGetNullShapeType += delegate { MessageBox.Show("請選擇形狀!"); };

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
            _pModel.ClickedAt(e.RowIndex, e.ColumnIndex);
        }

        private void RemoveGridViewRow()
        {
            shapeGridView.Rows.RemoveAt(_pModel.RemovedShapeIndex);
        }

        private void AddGridViewRow()
        {
            string[] row = new string[] { "刪除" }.Concat(_pModel.LastShapeData).ToArray();
            shapeGridView.Rows.Add(row);
        }

        /*public void HandleClearButtonClick(object sender, System.EventArgs e)
        {
            _canvasModel.Clear();
        }*/

        private void HandleCanvasPointerPressed(object sender, MouseEventArgs e)
        {
            _pModel.PointerPressed(e.X, e.Y);
        }

        private void HandleCanvasPointerReleased(object sender, MouseEventArgs e)
        {
            _pModel.PointerReleased(e.X, e.Y);
        }

        private void HandleCanvasPointerMoved(object sender, MouseEventArgs e)
        {
            _pModel.PointerMoved(e.X, e.Y);
        }

        private void HandleCanvasPaint(object sender, PaintEventArgs e)
        {
            _pModel.Draw(new WindowsFormsGraphicsAdaptor(e.Graphics));
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
            _pModel.SetDrawingMode(DrawingMode.SELECT);
        }
    }
}
