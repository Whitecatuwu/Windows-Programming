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
using DrawingForm;
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

            _model._modelChanged += HandleModelChanged;
            _model._modelRemovedShape += HandleModelChanged;
            _model._modelAddedShape += AddGridViewRow;
            _model._modelAddedShape += HandleModelChanged;
            _model._modelAddedShape += delegate { Cursor = Cursors.Default; };
            _model._modelNullShapeType += delegate { MessageBox.Show("請選擇形狀!"); };
            _model._modelInputError += delegate { MessageBox.Show("資料輸入有誤!"); };
            _model._modelPointerDragging += delegate { Cursor = Cursors.Cross; };

            _pModel = new PresentationModel.PresentationModel(_model, _canvas);
            shapeGridView.CellClick += RemoveShape;
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

        private void RemoveShape(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                shapeGridView.Rows.RemoveAt(e.RowIndex);
                _model.RemoveShape(e.RowIndex);
            }
        }
        private void AddGridViewRow()
        {
            string[] row = new string[] { "刪除" }.Concat(_model.ShapeDatas.Last()).ToArray();
            shapeGridView.Rows.Add(row);
        }

        /*public void HandleClearButtonClick(object sender, System.EventArgs e)
        {
            _canvasModel.Clear();
        }*/

        private void HandleCanvasPointerPressed(object sender, MouseEventArgs e)
        {
            _model.PointerPressed(e.X, e.Y);
        }

        private void HandleCanvasPointerReleased(object sender, MouseEventArgs e)
        {
            _model.PointerReleased(e.X, e.Y);
        }

        private void HandleCanvasPointerMoved(object sender, MouseEventArgs e)
        {
            _model.PointerMoved(e.X, e.Y);
        }

        private void HandleCanvasPaint(object sender, PaintEventArgs e)
        {
            _pModel.Draw(new PresentationModel.WindowsFormsGraphicsAdaptor(e.Graphics));
        }

        private void HandleModelChanged()
        {
            Invalidate(true);
        }

        private void RefreshToolStrip()
        {
            toolStripStartButton.Checked = _pModel.IsStartEnable;
            toolStripTerminatorButton.Checked = _pModel.IsTerminatorEnable;
            toolStripProcessButton.Checked = _pModel.IsProcessEnable;
            toolStripDecisionButton.Checked = _pModel.IsDecisionEnable;
        }
        private void toolStripStartButton_Click(object sender, EventArgs e)
        {
            _pModel.setDrewingShape(ShapeType.START);
            RefreshToolStrip();
        }

        private void toolStripTerminatorButton_Click(object sender, EventArgs e)
        {
            _pModel.setDrewingShape(ShapeType.TERMINATOR);
            RefreshToolStrip();
        }
        private void toolStripProcessButton_Click(object sender, EventArgs e)
        {
            _pModel.setDrewingShape(ShapeType.PROCESS);
            RefreshToolStrip();
        }
        private void toolStripDecisionButton_Click(object sender, EventArgs e)
        {
            _pModel.setDrewingShape(ShapeType.DECISION);
            RefreshToolStrip();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
