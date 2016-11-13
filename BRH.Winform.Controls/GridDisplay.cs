using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BRH.Winform.Controls
{
    public partial class GridDisplay : Control
    {
        #region private member
        private int _CellWidth = 1;
        private int _CellHeight = 1;
        private int _BorderWidth = 1;
        private int _GridLineWidth = 1;
        private int _GridLeftMargin = 0;
        private int _GridTopMargin = 0;
        private int _ColumnCount = 10;
        private int _RowCount = 10;
        private bool _UniformCell = true;
        private Color _BorderColor = Color.Black;
        private Color _GridLineColor = Color.Black;
        private Rectangle[,] _Cells = null;
        #endregion

        #region property
        /// <summary>
        /// outer border width, must be >=0, wiil invoke redraw-all
        /// </summary>
        public int BorderWidth
        {
            get { return this._BorderWidth; }
            set
            {
                if (value >= 0 && value != this._BorderWidth)
                {
                    this._BorderWidth = value;
                    this.Refresh();
                }
            }
        }

        /// <summary>
        /// grid line with, must be >=0, wiil invoke redraw all
        /// </summary>
        public int GridLineWidth
        {
            get { return this._GridLineWidth; }
            set
            {
                if (value >= 0 && value != this._GridLineWidth)
                {
                    this._GridLineWidth = value;
                    this.Refresh();
                }
            }
        }

        /// <summary>
        /// outer border color, will invoke border redraw
        /// </summary>
        public Color BorderColor
        {
            get { return this._BorderColor; }
            set
            {
                if (value != this._BorderColor)
                {
                    this._BorderColor = value;
                    this.RedrawBorder();
                }
            }
        }

        /// <summary>
        /// column count, must be >0
        /// </summary>
        public int ColumnCount
        {
            get { return this._ColumnCount; }
            set
            {
                if (value > 0 && value != this._ColumnCount)
                {
                    this._ColumnCount = value;
                    this.Refresh();
                }
            }
        }

        /// <summary>
        /// row count, must be >0
        /// </summary>
        public int RowCount
        {
            get { return this._RowCount; }
            set
            {
                if (value > 0 && value != this._RowCount)
                {
                    this._RowCount = value;
                    this.Refresh();
                }
            }
        }

        /// <summary>
        /// grid line color, will invode grid line redraw
        /// </summary>
        public Color GridLineColor
        {
            get { return this._GridLineColor; }
            set
            {
                if (value != this._GridLineColor)
                {
                    this._GridLineColor = value;
                    this.RedrawGridLines();
                }
            }
        }

        /// <summary>
        /// if true, cell will be square, will invoke redraw all
        /// </summary>
        public bool UniformCell
        {
            get { return this._UniformCell; }
            set
            {
                if (value != this._UniformCell)
                {
                    this._UniformCell = value;
                    this.Refresh();
                }
            }
        }
        #endregion

        #region readonly property
        public int CellWidth
        {
            get { return this._CellWidth; }
        }
        public int CellHeight
        {
            get { return this._CellHeight; }
        }
        #endregion

        public GridDisplay()
        {
        }

        #region private method
        /// <summary>
        /// recalculate all data and redraw all elements
        /// </summary>
        private void RedrawAll()
        {
            this.CalculateDrawingData();
            this.RedrawBorder();
            this.RedrawGridLines();
        }

        /// <summary>
        /// recalculate all data
        /// </summary>
        private void CalculateDrawingData()
        {
            var totalWidth = this.ClientRectangle.Width;
            var totalHeight = this.ClientRectangle.Height;
            var needMinWidth = this._BorderWidth * 2 + this._ColumnCount * 1 + (this._ColumnCount - 1) * this._GridLineWidth;
            var needMinHeight = this._BorderWidth * 2 + this._RowCount * 1 + (this._RowCount - 1) * this._GridLineWidth;

            this.MinimumSize = new Size(needMinWidth, needMinHeight);

            #region calc cell width & height
            //calc cell width
            this._CellWidth = (totalWidth
                               - (this._BorderWidth * 2) //border left & right
                               - (this._GridLineWidth * (this._ColumnCount - 1)) // width of all vertical grid lines
                              ) / this._ColumnCount;
            //calc cell height
            this._CellHeight = (totalHeight
                                - (this._BorderWidth * 2)
                                - (this._GridLineWidth * (this._RowCount - 1))
                                ) / this._RowCount;
            //uniform or not?
            if (this._UniformCell)
            {
                this._CellHeight = this._CellWidth = Math.Min(this._CellHeight, this._CellWidth);
            }
            #endregion

            #region calc grid margin left & top
            this._GridLeftMargin = (totalWidth
                                    - (this._CellWidth * this._ColumnCount)
                                    - (this._BorderWidth * 2)
                                    - (this._GridLineWidth * (this._ColumnCount - 1))
                                    ) / 2;
            this._GridTopMargin = (totalHeight
                                   - (this._CellHeight * this._RowCount)
                                   - (this._BorderWidth * 2)
                                   - (this._GridLineWidth * (this._RowCount - 1))
                                    ) / 2;
            #endregion

            #region generate all cells
            var y = this._GridTopMargin + this._BorderWidth;
            this._Cells = new Rectangle[this._RowCount, this._ColumnCount];
            for (int i = 0; i < this._RowCount; i++)
            {
                var x = this._GridLeftMargin + this._BorderWidth;
                for (int j = 0; j < this._ColumnCount; j++)
                {
                    this._Cells[i, j] = new Rectangle { X = x, Y = y, Width = this._CellWidth, Height = this._CellHeight };
                    x += this._CellWidth + this._GridLineWidth;
                }
                y += this._CellHeight + this._GridLineWidth;
            }
            #endregion
        }

        /// <summary>
        /// redraw border only
        /// </summary>
        private void RedrawBorder()
        {
            var g = this.CreateGraphics();
            var pen = new Pen(this.BorderColor, this._BorderWidth);
            var fix = this._BorderWidth / 2;
            var p_lt = new Point(this._GridLeftMargin + fix, this._GridTopMargin + fix);
            var p_rt = new Point(p_lt.X + this._CellWidth * this._ColumnCount + this._GridLineWidth * (this._ColumnCount - 1) + this._BorderWidth, p_lt.Y);
            var p_lb = new Point(p_lt.X, this._GridTopMargin + this._BorderWidth + fix + this._GridLineWidth * (this._RowCount - 1) + this._CellHeight * this._RowCount);
            var p_rb = new Point(p_rt.X, p_lb.Y);
            //top
            g.DrawLine(pen, p_lt.X - fix, p_lt.Y, p_rt.X + fix, p_rt.Y);
            //left
            g.DrawLine(pen, p_lt.X, p_lt.Y - fix, p_lb.X, p_lb.Y + fix);
            //bottom
            g.DrawLine(pen, p_lb.X - fix, p_lb.Y, p_rb.X + fix, p_rb.Y);
            //right
            g.DrawLine(pen, p_rt.X, p_rt.Y - fix, p_rb.X, p_rb.Y + fix);
        }

        /// <summary>
        /// redraw grid lines only
        /// </summary>
        private void RedrawGridLines()
        {
            var pen = new Pen(this._GridLineColor, this._GridLineWidth);
            var g = this.CreateGraphics();
            var fix = this.GridLineWidth / 2;
            //horizontal lines
            if (this.CellHeight > 0)
            {
                var left = this._GridLeftMargin + this._BorderWidth;
                var right = left + this._CellWidth * this._ColumnCount + this._GridLineWidth * (this._ColumnCount - 1);
                var y = this._GridTopMargin + this._BorderWidth + this._CellHeight + fix;
                for (int i = 1; i < this._RowCount; i++)
                {
                    g.DrawLine(pen, left, y, right, y);
                    y += this.CellHeight + this._GridLineWidth;
                }
            }
            //vertical lines
            if (this.CellWidth > 0)
            {
                var top = this._GridTopMargin + this._BorderWidth;
                var bottom = top + this._CellHeight * this._RowCount + this._GridLineWidth * (this._RowCount - 1);
                var x = this._GridLeftMargin + this._BorderWidth + this._CellWidth + fix;
                for (int i = 1; i < this._ColumnCount; i++)
                {
                    g.DrawLine(pen, x, top, x, bottom);
                    x += this.CellWidth + this._GridLineWidth;
                }
            }
        }
        #endregion

        #region override
        protected override void OnPaint(PaintEventArgs e)
        {
            this.RedrawAll();
            base.OnPaint(e);
        }
        protected override void OnResize(EventArgs e)
        {
            this.Invalidate();
            base.OnResize(e);
        }
        #endregion
    }
}
