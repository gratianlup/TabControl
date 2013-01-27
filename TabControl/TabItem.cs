// Copyright (c) 2007 Gratian Lup. All rights reserved.
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are
// met:
//
// * Redistributions of source code must retain the above copyright
// notice, this list of conditions and the following disclaimer.
//
// * Redistributions in binary form must reproduce the above
// copyright notice, this list of conditions and the following
// disclaimer in the documentation and/or other materials provided
// with the distribution.
//
// * The name "TabControl" must not be used to endorse or promote
// products derived from this software without prior written permission.
//
// * Products derived from this software may not be called "TabControl" nor
// may "TabControl" appear in their names without prior written
// permission of the author.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
// OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Windows.Forms.Design.Behavior;
using System.Drawing.Imaging;

namespace TabControl {
    public enum CloseButtonState {
        Normal, Over, Pressed
    }


    [DefaultEvent("TabSelected")]
    //[DesignerAttribute(typeof(TabItemDesigner))]
    public partial class TabItem : UserControl {
        #region Constants

        private const int CornerRoundness = 5;
        private const int CloseButtonWidth = 12;
        private const int CloseButtonHeight = 12;
        private const int CloseButtonDistance = 12;

        #endregion

        #region Static data

        private static ColorMatrix grayscaleMatrix;

        private static ColorMatrix DisabledImageMatrix {
            get {
                if(grayscaleMatrix == null) {
                    float[][] numArray = new float[5][];
                    numArray[0] = new float[] { 0.2125f, 0.2125f, 0.2125f, 0f, 0f };
                    numArray[1] = new float[] { 0.2577f, 0.2577f, 0.2577f, 0f, 0f };
                    numArray[2] = new float[] { 0.0361f, 0.0361f, 0.0361f, 0f, 0f };

                    float[] numArray3 = new float[5];
                    numArray3[3] = 1f;
                    numArray[3] = numArray3;
                    numArray[4] = new float[] { 0.38f, 0.38f, 0.38f, 0f, 1f };

                    float[][] numArray2 = new float[5][];
                    float[] numArray4 = new float[5];
                    numArray4[0] = 1f;
                    numArray2[0] = numArray4;

                    float[] numArray5 = new float[5];
                    numArray5[1] = 1f;
                    numArray2[1] = numArray5;

                    float[] numArray6 = new float[5];
                    numArray6[2] = 1f;
                    numArray2[2] = numArray6;

                    float[] numArray7 = new float[5];
                    numArray7[3] = 0.7f;
                    numArray2[3] = numArray7;
                    numArray2[4] = new float[5];

                    grayscaleMatrix = MultiplyColorMatrix(numArray2, numArray);
                }

                return grayscaleMatrix;
            }
        }

        private static ColorMatrix MultiplyColorMatrix(float[][] matrix1, float[][] matrix2) {
            int num = 5;
            float[][] newColorMatrix = new float[num][];

            for(int i = 0; i < num; i++) {
                newColorMatrix[i] = new float[num];
            }

            float[] numArray2 = new float[num];

            for(int j = 0; j < num; j++) {
                for(int k = 0; k < num; k++) {
                    numArray2[k] = matrix1[k][j];
                }

                for(int m = 0; m < num; m++) {
                    float[] numArray3 = matrix2[m];
                    float num6 = 0f;

                    for(int n = 0; n < num; n++) {
                        num6 += numArray3[n] * numArray2[n];
                    }

                    newColorMatrix[m][j] = num6;
                }
            }

            return new ColorMatrix(newColorMatrix);
        }

        #endregion

        #region Fields

        private Brush backBrush;
        private Brush foreBrush;
        private Brush currentBackBrush;
        private Brush currentForeBrush;
        private Pen borderPen;
        private GraphicsPath backgroundPath;

        private CloseButtonState closeButtonState;
        private Rectangle closeButtonLocation;

        private bool reordering;
        private Point lastMouseDownLocation;

        // designer support
        private bool ignoreNextWidthValue;
        private bool ignoreNextHeightValue;

        #endregion

        #region Properties

        private TabHost _owner;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public TabHost Owner {
            get { return _owner; }
            set { _owner = value; }
        }


        private Color _borderColor;
        public Color BorderColor {
            get { return _borderColor; }
            set {
                if(_borderColor != value) {
                    _borderColor = value;

                    if(_borderColor != null && _borderColor != Color.Empty) {
                        borderPen = new Pen(_borderColor);
                    }
                    else {
                        borderPen = null;
                    }

                    ForceUpdate();
                }
            }
        }


        private Color _highlightBackColor;
        [Category("Appearance")]
        [Browsable(true)]
        public virtual Color HighlightBackColor {
            get { return _highlightBackColor; }
            set { _highlightBackColor = value; }
        }


        private Color _highlightForeColor;
        [Category("Appearance")]
        [Browsable(true)]
        public virtual Color HighlightForeColor {
            get { return _highlightForeColor; }
            set { _highlightForeColor = value; }
        }


        private Color _selectedBackColor;
        [Category("Appearance")]
        [Browsable(true)]
        public virtual Color SelectedBackColor {
            get { return _selectedBackColor; }
            set {
                if(_selectedBackColor.ToArgb() != value.ToArgb()) {
                    _selectedBackColor = value;
                    UpdateSelectedState();
                }

            }
        }


        private Color _selectedForeColor;
        [Category("Appearance")]
        [Browsable(true)]
        public virtual Color SelectedForeColor {
            get { return _selectedForeColor; }
            set {
                if(_selectedForeColor.ToArgb() != value.ToArgb()) {
                    _selectedForeColor = value;
                    UpdateSelectedState();
                }
            }
        }


        private string _tabText;
        [Category("Appearance")]
        [Browsable(true)]
        public string TabText {
            get { return _tabText; }
            set {
                if(_tabText != value) {
                    _tabText = value;
                    ForceUpdate();
                }
            }
        }


        private ContentAlignment _textAlignment;
        [Category("Appearance")]
        [Browsable(true)]
        public virtual ContentAlignment TextAlignment {
            get { return _textAlignment; }
            set {
                if(_textAlignment != value) {
                    _textAlignment = value;
                    ForceUpdate();
                }
            }
        }


        private Image _image;
        [Category("Appearance")]
        [Browsable(true)]
        public virtual Image Image {
            get { return _image; }
            set {
                if(_image != value) {
                    _image = value;
                    ForceUpdate();
                }
            }
        }


        private TextImageRelation _textImageRelation;
        [Category("Appearance")]
        [Browsable(true)]
        public virtual TextImageRelation TextImageRelation {
            get { return _textImageRelation; }
            set {
                if(_textImageRelation != value) {
                    _textImageRelation = value;
                    ForceUpdate();
                }
            }
        }

        private int _tabWidth;
        [Category("Appearance")]
        [Browsable(true)]
        public virtual int TabWidth {
            get { return _tabWidth; }
            set {
                if(_tabWidth != value) {
                    _tabWidth = value;
                    backgroundPath = null;

                    if(_owner != null) {
                        _owner.Relayout();
                    }
                }
            }
        }

        private int _tabHeigth;
        [Category("Appearance")]
        [Browsable(true)]
        public virtual int TabHeigth {
            get { return _tabHeigth; }
            set {
                if(_tabHeigth != value) {
                    _tabHeigth = value;
                    backgroundPath = null;

                    if(_owner != null) {
                        _owner.Relayout();
                    }
                }
            }
        }


        private int _selectedTabHeigth;
        [Category("Appearance")]
        [Browsable(true)]
        public virtual int SelectedTabHeigth {
            get { return _selectedTabHeigth; }
            set {
                if(_selectedTabHeigth != value) {
                    _selectedTabHeigth = value;
                    backgroundPath = null;

                    if(_owner != null && _selected) {
                        _owner.Relayout();
                    }
                }
            }
        }


        // hide default Width and Height properties
        [Browsable(false)]
        public int Width {
            get { return base.Width; }
            set { base.Width = value; }
        }


        [Browsable(false)]
        public int Height {
            get { return base.Height; }
            set { base.Height = value; }
        }


        private bool _selected;
        [Category("Behavior")]
        [Browsable(true)]
        public bool Selected {
            get { return _selected; }
            set {
                if(value == _selected) {
                    return;
                }

                _selected = value;
                backgroundPath = null;

                if(_selected) {
                    CancelEventArgs callSelectedEvent = new CancelEventArgs();
                    OnBeforeTabSelected(callSelectedEvent);

                    if(callSelectedEvent.Cancel == false) {
                        UpdateSelectedState();

                        // notify parent
                        if(_owner != null) {
                            _owner.TabItemSelected(this);
                        }

                        // call event
                        OnTabSelected(EventArgs.Empty);
                    }
                }
                else {
                    // just update the state
                    UpdateSelectedState();
                }
            }
        }


        private bool _autoEllipsis;
        public bool AutoEllipsis {
            get { return _autoEllipsis; }
            set {
                if(value != _autoEllipsis) {
                    _autoEllipsis = value;
                    ForceUpdate();
                }
            }
        }
        #endregion

        #region Events

        public event CancelEventHandler BeforeTabSelected;
        public event EventHandler TabSelected;
        public event CancelEventHandler BeforeCloseButtonPressed;
        public event EventHandler CloseButtonPressed;

        #endregion

        #region Constructor

        public TabItem() {
            InitializeComponent();
            TabWidth = TabHost.DefaultTabWidth;

            // set default colors
            BackColor = Color.SlateGray;
            ForeColor = Color.White;
            HighlightBackColor = Color.Gold;
            HighlightForeColor = Color.Black;
            SelectedBackColor = Color.DeepPink;
            SelectedForeColor = Color.White;
            TextAlignment = ContentAlignment.MiddleLeft;
            TextImageRelation = TextImageRelation.ImageBeforeText;
            TabText = Name;

            // enable double-buffering
            this.SetStyle(ControlStyles.DoubleBuffer |
                          ControlStyles.UserPaint    |
                          ControlStyles.AllPaintingInWmPaint,
                          true);

            this.UpdateStyles();
        }

        public TabItem(string name)
            : this() {
            Name = name;
            Text = name;
        }

        #endregion

        #region Protected methods (event helpers)

        protected void OnBeforeTabSelected(CancelEventArgs e) {
            CancelEventHandler temp = BeforeTabSelected;

            if(temp != null) {
                temp(this, e);
            }
        }

        protected void OnTabSelected(EventArgs e) {
            EventHandler temp = TabSelected;

            if(temp != null) {
                temp(this, e);
            }
        }

        protected void OnBeforeCloseButtonPressed(CancelEventArgs e) {
            CancelEventHandler temp = BeforeCloseButtonPressed;

            if(temp != null) {
                temp(this, e);
            }
        }

        protected void OnCloseButtonPressed(EventArgs e) {
            EventHandler temp = CloseButtonPressed;

            if(temp != null) {
                temp(this, e);
            }
        }

        #endregion

        #region Overriden methods

        protected override void OnBackColorChanged(EventArgs e) {
            backBrush = new SolidBrush(BackColor);
            UpdateSelectedState(); // ForceUpdate is called automatically
        }

        protected override void OnForeColorChanged(EventArgs e) {
            foreBrush = new SolidBrush(ForeColor);
            UpdateSelectedState(); // ForceUpdate is called automatically
        }

        protected override void OnEnabledChanged(EventArgs e) {
            ForceUpdate();
            base.OnEnabledChanged(e);
        }

        protected override void OnPaddingChanged(EventArgs e) {
            ForceUpdate();

            base.OnPaddingChanged(e);
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            if(_owner.ShowCloseButtons && closeButtonState == CloseButtonState.Over) {
                EnterPressedCloseButtonState();
            }
            else {
                // tab reordering
                if(_owner.AllowTabReordering) {
                    lastMouseDownLocation = e.Location;
                }

                // tab selection
                if(e.Button != MouseButtons.Right || this.ContextMenuStrip == null) {
                    // don't select if a menu strip should be shown
                    Selected = true;
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            if(_owner.ShowCloseButtons && closeButtonState == CloseButtonState.Pressed) {
                CancelEventArgs callCloseEvent = new CancelEventArgs();
                OnBeforeCloseButtonPressed(callCloseEvent);

                if(callCloseEvent.Cancel == false) {
                    OnCloseButtonPressed(EventArgs.Empty);
                }

                ExitPressedCloseButtonState();
            }

            if(reordering) {
                this.Capture = false;
                reordering = false;

                // notify the host
                _owner.TabEndDrag(this, e.Location);
            }

            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            // tab reordering
            if(CanReorder(e.Button)) {
                if(reordering) {
                    // notify the host
                    _owner.TabUpdateDrag(this, e.Location);
                }
                else {
                    // determine if this is a drag operation
                    // DragSize values are very small (4 on Vista), so we don't divide it by 2,
                    // the way it should be done.
                    if((Math.Abs(e.Location.X - lastMouseDownLocation.X) >= SystemInformation.DragSize.Width) ||
                       (Math.Abs(e.Location.Y - lastMouseDownLocation.Y) >= SystemInformation.DragSize.Height)) {
                        // begin reordering
                        reordering = true;
                        this.Capture = true; // capture the mouse

                        // notify the host
                        _owner.TabStartDrag(this, e.Location);
                    }
                }
            }
            else if(_owner.ShowCloseButtons) {
                // check if the mouse is over the close button
                if(closeButtonLocation.Contains(e.X, e.Y)) {
                    EnterOverCloseButtonState();
                }
                else {
                    ExitOverCloseButtonState();
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseEnter(EventArgs e) {
            EnterHighlightState();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e) {
            ExitHighlightState();
            EnterNormalCloseButtonState();

            base.OnMouseLeave(e);
        }

        #endregion

        #region State management methods

        private void EnterHighlightState() {
            if(_selected == false) {
                currentBackBrush = new SolidBrush(_highlightBackColor);
                currentForeBrush = new SolidBrush(_highlightForeColor);

                ForceUpdate();
            }
        }


        private void ExitHighlightState() {
            UpdateSelectedState(); // ForceUpdate is called automatically
        }


        private void EnterOverCloseButtonState() {
            if(closeButtonState == CloseButtonState.Normal) {
                closeButtonState = CloseButtonState.Over;
                ForceUpdate();
            }
        }


        private void ExitOverCloseButtonState() {
            if(closeButtonState == CloseButtonState.Over) {
                closeButtonState = CloseButtonState.Normal;
                ForceUpdate();
            }
        }


        private void EnterPressedCloseButtonState() {
            if(closeButtonState == CloseButtonState.Over) {
                closeButtonState = CloseButtonState.Pressed;
                ForceUpdate();
            }
        }


        private void ExitPressedCloseButtonState() {
            if(closeButtonState == CloseButtonState.Pressed) {
                closeButtonState = CloseButtonState.Normal;
                ForceUpdate();
            }
        }


        private void EnterNormalCloseButtonState() {
            if(_owner.ShowCloseButtons && closeButtonState != CloseButtonState.Normal) {
                closeButtonState = CloseButtonState.Normal;
                ForceUpdate();
            }
        }


        private void UpdateSelectedState() {
            if(_selected) {
                // choose the colors for the "selected" state
                currentBackBrush = new SolidBrush(_selectedBackColor);
                currentForeBrush = new SolidBrush(_selectedForeColor);
            }
            else {
                // choose the normal colors
                currentBackBrush = backBrush;
                currentForeBrush = foreBrush;
            }

            ForceUpdate();
        }

        #endregion

        #region Helper methods

        private void ForceUpdate() {
            this.Invalidate();
        }


        private bool CanReorder(MouseButtons button) {
            return _owner.AllowTabReordering &&
                    button != MouseButtons.None &&
                    closeButtonState == CloseButtonState.Normal;
        }


        private Color MakeTransparentColor(Color baseColor, int transparentValue) {
            return Color.FromArgb(transparentValue,
                                  baseColor.R,
                                  baseColor.G,
                                  baseColor.B);
        }


        private void DrawCloseButtonInternal(int x, int y, Pen linePen, Brush backBrush, Graphics g) {
            // fill background
            g.FillRectangle(backBrush, x, y, CloseButtonWidth, CloseButtonHeight);

            // draw border
            g.DrawRectangle(linePen, x, y, CloseButtonWidth, CloseButtonHeight);

            // draw the "X"
            g.DrawLine(linePen, x + 3, y + 3,
                                x + CloseButtonWidth - 3,
                                y + CloseButtonHeight - 3);

            g.DrawLine(linePen, x + CloseButtonWidth - 3, y + 3,
                                x + 3, y + CloseButtonHeight - 3);
        }

        #endregion

        #region Protected methods (drawing helpers)

        protected Rectangle ComputeCloseButtonBounds(ref Rectangle availableSpace) {
            if(_owner.ShowCloseButtons == false ||
                (_selected == false && _owner.ShowCloseButtons && _owner.CloseButtonsOnlyForSelected)) {
                return availableSpace;
            }

            Rectangle bounds = Rectangle.Empty;

            if((_owner.TabAlignment == TabAlignment.Top) || 
               (_owner.TabAlignment == TabAlignment.Bottom)) {
                if(_owner.CloseButtonAlignment == CloseButtonAlignment.Left) {
                    bounds = new Rectangle(availableSpace.Left,
                                           availableSpace.Top,
                                           CloseButtonWidth + CloseButtonDistance,
                                           availableSpace.Height);

                    // compute remaining space
                    availableSpace = new Rectangle(CloseButtonWidth + CloseButtonDistance,
                                                   availableSpace.Top,
                                                   Math.Max(1, availableSpace.Width - CloseButtonWidth - CloseButtonDistance),
                                                   availableSpace.Height);
                }
                else {
                    bounds = new Rectangle(availableSpace.Left + availableSpace.Width - CloseButtonWidth - CloseButtonDistance,
                                           availableSpace.Top,
                                           CloseButtonWidth + CloseButtonDistance,
                                           availableSpace.Height);

                    availableSpace = new Rectangle(availableSpace.Left,
                                                   availableSpace.Top,
                                                   Math.Max(1, availableSpace.Width - CloseButtonWidth - CloseButtonDistance),
                                                   availableSpace.Height);
                }
            }
            else if((_owner.TabAlignment == TabAlignment.Left) || 
                    (_owner.TabAlignment == TabAlignment.Right)) {
                if(_owner.CloseButtonAlignment == CloseButtonAlignment.Left) {
                    bounds = new Rectangle(availableSpace.Left,
                                           availableSpace.Top,
                                           availableSpace.Width,
                                           CloseButtonHeight + CloseButtonDistance);

                    // compute remaining space
                    availableSpace = new Rectangle(availableSpace.Left,
                                                   availableSpace.Top + CloseButtonHeight + CloseButtonDistance,
                                                   availableSpace.Width,
                                                   Math.Max(1, availableSpace.Height - CloseButtonHeight - CloseButtonDistance));
                }
                else {
                    bounds = new Rectangle(availableSpace.Left,
                                           availableSpace.Top + availableSpace.Height - CloseButtonHeight - CloseButtonDistance,
                                           availableSpace.Width,
                                           CloseButtonWidth + CloseButtonDistance);

                    availableSpace = new Rectangle(availableSpace.Left,
                                                   availableSpace.Top,
                                                   availableSpace.Width,
                                                   Math.Max(1, availableSpace.Height - CloseButtonHeight - CloseButtonDistance));
                }
            }

            return bounds;
        }


        protected Rectangle ComputeImageBounds(ref Rectangle availableSpace) {
            Rectangle bounds = Rectangle.Empty;

            if(_image == null) {
                return bounds;
            }

            int imageWidth = _image.Width;
            int imageHeight = _image.Height;

            if((_owner.TabAlignment == TabAlignment.Top) ||
               (_owner.TabAlignment == TabAlignment.Bottom)) {
                switch(_textImageRelation) {
                    case TextImageRelation.ImageBeforeText: {
                            bounds = new Rectangle(availableSpace.Left,
                                                   availableSpace.Top,
                                                   imageWidth,
                                                   availableSpace.Height);

                            availableSpace = new Rectangle(availableSpace.Left + imageWidth,
                                                           availableSpace.Top,
                                                           Math.Max(1, availableSpace.Width - imageWidth),
                                                           availableSpace.Height);
                            break;
                        }
                    case TextImageRelation.TextBeforeImage: {
                            bounds = new Rectangle(availableSpace.Left + availableSpace.Width - imageWidth,
                                                   availableSpace.Top,
                                                   imageWidth,
                                                   availableSpace.Height);

                            availableSpace = new Rectangle(availableSpace.Left,
                                                           availableSpace.Top,
                                                           Math.Max(1, availableSpace.Width - imageWidth),
                                                           availableSpace.Height);
                            break;
                        }
                }
            }
            else if((_owner.TabAlignment == TabAlignment.Left) || 
                    (_owner.TabAlignment == TabAlignment.Right)) {
                switch(_textImageRelation) {
                    case TextImageRelation.ImageBeforeText: {
                            bounds = new Rectangle(availableSpace.Left,
                                                   availableSpace.Top,
                                                   availableSpace.Width,
                                                   imageHeight);

                            availableSpace = new Rectangle(availableSpace.Left,
                                                           availableSpace.Top + imageHeight,
                                                           availableSpace.Width,
                                                           Math.Max(1, availableSpace.Height - imageHeight));
                            break;
                        }
                    case TextImageRelation.TextBeforeImage: {
                            bounds = new Rectangle(availableSpace.Left,
                                                   availableSpace.Top + availableSpace.Height - imageHeight,
                                                   availableSpace.Width,
                                                   imageHeight);

                            availableSpace = new Rectangle(availableSpace.Left,
                                                           availableSpace.Top,
                                                           availableSpace.Width,
                                                           Math.Max(1, availableSpace.Height - imageHeight));
                            break;
                        }
                }
            }

            return bounds;
        }


        protected Rectangle ComputeTextBounds(ref Rectangle availableSpace) {
            // just use all the remaining space
            return availableSpace;
        }


        protected void DrawCloseButton(Rectangle bounds, CloseButtonState state, Graphics g) {
            if(_owner.ShowCloseButtons == false ||
                (_selected == false && _owner.ShowCloseButtons && _owner.CloseButtonsOnlyForSelected) ||
                currentBackBrush == null || currentForeBrush == null) {
                return;
            }

            int x;
            int y;

            // set button position 
            if((_owner.TabAlignment == TabAlignment.Top) || 
               (_owner.TabAlignment == TabAlignment.Bottom)) {
                if(_owner.CloseButtonAlignment == CloseButtonAlignment.Left) {
                    x = bounds.Left;
                }
                else {
                    x = bounds.Left + bounds.Width - CloseButtonWidth - CloseButtonDistance / 2;
                }

                // center vertically
                y = bounds.Top + bounds.Height / 2 - CloseButtonHeight / 2;
            }
            else {
                if(_owner.CloseButtonAlignment == CloseButtonAlignment.Left) {
                    y = bounds.Top;
                }
                else {
                    y = bounds.Top + bounds.Height - CloseButtonHeight - CloseButtonDistance / 2;
                }

                // center horizontally
                x = bounds.Left + bounds.Width / 2 - CloseButtonWidth / 2 - 1; // -1 is required
            }

            // set location
            if(state == CloseButtonState.Normal) {
                closeButtonLocation = new Rectangle(x, y, CloseButtonWidth, CloseButtonHeight);
            }

            // draw the button accordingly to the state
            switch(state) {
                case CloseButtonState.Normal: {
                        Brush borderBrush = this.Enabled ? currentForeBrush : Brushes.Silver;
                        Brush backgroundBrush = new SolidBrush(_selected ? _owner.CloseButtonColorSelected :
                                                                           _owner.CloseButtonColor);

                        int borderOpacity = _selected ? _owner.CloseButtonBorderOpacitySelected :
                                                        _owner.CloseButtonBorderOpacity;

                        Pen linePen = new Pen(MakeTransparentColor(((SolidBrush)borderBrush).Color, borderOpacity));
                        DrawCloseButtonInternal(x, y, linePen, backgroundBrush, g);
                        break;
                    }
                case CloseButtonState.Over: {
                        Pen linePen = new Pen(((SolidBrush)currentForeBrush).Color);
                        Brush backgroundBrush = new SolidBrush(_selected ? _owner.CloseButtonOverColorSelected :
                                                                           _owner.CloseButtonOverColor);

                        DrawCloseButtonInternal(x, y, linePen, backgroundBrush, g);
                        break;
                    }
                case CloseButtonState.Pressed: {
                        Pen linePen = new Pen(((SolidBrush)currentForeBrush).Color);
                        DrawCloseButtonInternal(x, y, linePen, new SolidBrush(_owner.CloseButtonPressedColor), g);
                        break;
                    }
            }
        }


        private int GetRealWidth(Rectangle rect) {
            if(_owner != null) {
                if((_owner.TabAlignment == TabAlignment.Top) || 
                   (_owner.TabAlignment == TabAlignment.Bottom)) {
                    return rect.Width;
                }
                else {
                    return rect.Height;
                }
            }

            return rect.Width;
        }


        private int GetReaHeight(Rectangle rect) {
            if(_owner != null) {
                if((_owner.TabAlignment == TabAlignment.Top) || 
                   (_owner.TabAlignment == TabAlignment.Bottom)) {
                    return rect.Height;
                }
                else {
                    return rect.Width;
                }
            }

            return rect.Height;
        }


        protected void DrawText(Rectangle bounds, Graphics g) {
            if(currentForeBrush == null || Text == null) {
                return;
            }

            // get text width and height of the text
            SizeF textSize;
            textSize = g.MeasureString(_tabText, this.Font);

            // compute the left and top coordinates
            float x = bounds.Left;
            float y = bounds.Top;
            float width = GetRealWidth(bounds);
            float height = GetReaHeight(bounds);

            switch(_textAlignment) {
                case ContentAlignment.BottomCenter: {
                    x += width / 2 - textSize.Width / 2;
                    y += height - textSize.Height;
                    break;
                }
                case ContentAlignment.BottomLeft: {
                    x += 0;
                    y += height - textSize.Height;
                    break;
                }
                case ContentAlignment.BottomRight: {
                    x += width - textSize.Width;
                    y += height - textSize.Height;
                    break;
                }
                case ContentAlignment.MiddleCenter: {
                    x += width / 2 - textSize.Width / 2;
                    y += height / 2 - textSize.Height / 2;
                    break;
                }
                case ContentAlignment.MiddleLeft: {
                    x += 0;
                    y += height / 2 - textSize.Height / 2;
                    break;
                }
                case ContentAlignment.MiddleRight: {
                    x += width - textSize.Width;
                    y += height / 2 - textSize.Height / 2;
                    break;
                }
                case ContentAlignment.TopCenter: {
                    x += width / 2 - textSize.Width / 2;
                    y += 0;
                    break;
                }
                case ContentAlignment.TopLeft: {
                    x += 0;
                    y += 0;
                    break;
                }
                case ContentAlignment.TopRight: {
                    x += width - textSize.Width;
                    y += 0;
                    break;
                }
            }

            // draw the string
            Brush textBrush = currentForeBrush;

            if(this.Enabled == false) {
                // use a gray brush
                textBrush = Brushes.Silver;
            }

            if((_owner.TabAlignment == TabAlignment.Top) || 
               (_owner.TabAlignment == TabAlignment.Bottom)) {
                if(_autoEllipsis) {
                    StringFormat format = new StringFormat();
                    format.Trimming = StringTrimming.EllipsisCharacter;
                    RectangleF textBounds = new RectangleF(x, y, bounds.Width, textSize.Height);

                    g.DrawString(_tabText, this.Font, textBrush, textBounds, format);
                }
                else {
                    g.DrawString(_tabText, this.Font, textBrush, x, y);
                }
            }
            else if((_owner.TabAlignment == TabAlignment.Left) || 
                    (_owner.TabAlignment == TabAlignment.Right)) {
                // draw the string on a bitmap and rotate it
                using(Bitmap bufferImage = new Bitmap((int)width, (int)height, 
                                                      System.Drawing.Imaging.PixelFormat.Format32bppArgb)) {
                    using(Graphics graphics = Graphics.FromImage(bufferImage)) {
                        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        TextFormatFlags textFlags = TextFormatFlags.Default;
                        Color textColor = ((SolidBrush)currentForeBrush).Color;

                        // set ellipsis
                        if(_autoEllipsis) {
                            textFlags = TextFormatFlags.EndEllipsis;
                        }

                        // draw the text
                        // uWe use the TextRenderer object because DrawString from Graphics
                        // doesn't work right when drawing on bitmaps
                        Rectangle bouds = new Rectangle((int)x - bounds.Left, (int)y - bounds.Top,
                                                        (int)width, (int)height);
                        TextRenderer.DrawText(graphics, _tabText, this.Font,
                                              bounds, textColor, textFlags);

                        // rotate the text
                        if(_owner.TabAlignment == TabAlignment.Left) {
                            bufferImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            g.DrawImage(bufferImage, bounds.Left, bounds.Top, bufferImage.Width, bufferImage.Height);
                        }
                        else {
                            // on the right
                            bufferImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            g.DrawImage(bufferImage, bounds.Left, bounds.Top, bufferImage.Width, bufferImage.Height);
                        }
                    }
                }
            }
        }


        protected void DrawImage(Rectangle bounds, Graphics g) {
            if(_image != null) {
                Image tabImage = _image;

                // convert the image to grayscale if the tab is not Enabled
                if(Enabled == false) {
                    Bitmap buffer = new Bitmap(_image);

                    using(Graphics helperGraphics = Graphics.FromImage(buffer)) {
                        ImageAttributes attributes = new ImageAttributes();

                        attributes.SetColorMatrix(DisabledImageMatrix);
                        helperGraphics.DrawImage(_image,
                                                 new Rectangle(0, 0, _image.Width, _image.Height),
                                                 0, 0, _image.Width, _image.Height,
                                                 GraphicsUnit.Pixel, attributes);
                    }

                    tabImage = buffer;
                }

                // center the image in the bounds
                int x;
                int y;
                int imageWidth = _image.Width;
                int imageHeight = _image.Height;

                // draw the image
                if(imageHeight > bounds.Height) {
                    // scale the image to fit the control
                    imageWidth = (int)((double)imageWidth * ((double)imageHeight / (double)bounds.Height));
                    imageHeight = bounds.Height;

                    x = bounds.Left + bounds.Width / 2 - imageWidth / 2;
                    y = bounds.Top + bounds.Height / 2 - imageHeight / 2;

                    // draw the image
                    g.DrawImage(tabImage, x, y, imageWidth, imageHeight);
                }
                else {
                    // image fits, don't scale
                    x = bounds.Left + bounds.Width / 2 - imageWidth / 2;
                    y = bounds.Top + bounds.Height / 2 - imageHeight / 2;

                    // draw the image
                    g.DrawImageUnscaled(tabImage, x, y);
                }

                // dispose the image (if Enabled is false)
                if(Enabled == false) {
                    tabImage.Dispose();
                }
            }
        }


        protected void DrawTabBackground(Rectangle bounds, Brush brush, Graphics g) {
            // create the background (a filled path)
            GraphicsPath path = backgroundPath;

            int x = bounds.Left;
            int y = bounds.Top;
            int width = GetRealWidth(bounds);
            int height = GetReaHeight(bounds);

            // create the path only when needed
            if(path == null) {
                path = new GraphicsPath();

                path.AddLine(x,
                             y + CornerRoundness,
                             x,
                             y + height);

                path.AddBezier(x, y + CornerRoundness,
                               x + CornerRoundness / 5, y + CornerRoundness / 5,
                               x + CornerRoundness / 5, y + CornerRoundness / 5,
                               x + CornerRoundness, y);

                path.AddLine(x + CornerRoundness,
                             y,
                             x + width - CornerRoundness,
                             y);

                path.AddBezier(x + width - CornerRoundness, y,
                               x + width - CornerRoundness / 5, y + CornerRoundness / 5,
                               x + width - CornerRoundness / 5, y + CornerRoundness / 5,
                               x + width, y + CornerRoundness);

                path.AddLine(x + width,
                             y + CornerRoundness,
                             x + width,
                             y + height);

                path.AddLine(x + width,
                             y + height,
                             x,
                             y + height);
            }


            // draw the path according to the TabAlignment
            switch(_owner.TabAlignment) {
                case TabAlignment.Top: {
                    // just fill the paths	
                    g.FillPath(brush, path);

                    if(borderPen != null) {
                        g.DrawPath(borderPen, path);
                        g.DrawLine(new Pen(brush), x + 1, y + height, x + width - 1, y + height);
                    }
                    break;
                }
                case TabAlignment.Bottom: {
                    g.TranslateTransform(bounds.Width, bounds.Height);
                    g.RotateTransform(180);
                    g.FillPath(brush, path);

                    if(borderPen != null) {
                        g.DrawPath(borderPen, path);
                        g.DrawLine(new Pen(brush), x + 1, y + height, x + width - 1, y + height);
                    }

                    g.ResetTransform();
                    break;
                }
                case TabAlignment.Left: {
                    g.TranslateTransform(bounds.Width, 0);
                    g.RotateTransform(90);
                    g.FillPath(brush, path);

                    if(borderPen != null) {
                        g.DrawPath(borderPen, path);
                        g.DrawLine(new Pen(brush), x + 1, y + height, x + width - 1, y + height);
                    }

                    g.ResetTransform();
                    break;
                }
                case TabAlignment.Right: {
                    g.TranslateTransform(0, bounds.Height);
                    g.RotateTransform(-90);
                    g.FillPath(brush, path);

                    if(borderPen != null) {
                        g.DrawPath(borderPen, path);
                    }

                    g.ResetTransform();
                    break;
                }
            }
        }


        protected Rectangle ComputeTabBackgroundBounds() {
            if(_borderColor != null && _borderColor != Color.Empty) {
                // no border color is set
                return new Rectangle(Padding.Left,
                                     Padding.Top,
                                     this.Width - Padding.Right - Padding.Left - 1,
                                     this.Height - Padding.Bottom - Padding.Top - 1);
            }

            // use all available space by default
            return new Rectangle(Padding.Left,
                                 Padding.Top,
                                 this.Width - Padding.Right - Padding.Left,
                                 this.Height - Padding.Bottom - Padding.Top);
        }


        protected Rectangle ComputeElementSpace(Rectangle bounds) {
            Rectangle availableSpace = Rectangle.Empty;

            if((_owner.TabAlignment == TabAlignment.Top) || 
               (_owner.TabAlignment == TabAlignment.Bottom)) {
                availableSpace = new Rectangle(bounds.Left + CornerRoundness,
                                               bounds.Top,
                                               bounds.Width - CornerRoundness * 2,
                                               bounds.Height);
            }
            else if((_owner.TabAlignment == TabAlignment.Left) || 
                    (_owner.TabAlignment == TabAlignment.Right)) {
                availableSpace = new Rectangle(bounds.Left,
                                               bounds.Top + CornerRoundness,
                                               bounds.Width,
                                               bounds.Height - CornerRoundness * 2);
            }

            return availableSpace;
        }

        #endregion

        #region Drawing

        private void TabItem_Paint(object sender, PaintEventArgs e) {
            if(_owner == null) {
                return;
            }

            // clear
            e.Graphics.FillRectangle(new SolidBrush(_owner.BackColor),
                                     0, 0, this.Width, this.Height);

            // compute the bounds of the background, taking the Padding property into account
            Rectangle availableSpace = ComputeTabBackgroundBounds();
            DrawTabBackground(availableSpace, currentBackBrush, e.Graphics);

            // compute the available space for other elements (image, text, close button);
            Rectangle elementSpace = ComputeElementSpace(availableSpace);

            Rectangle closeButtonBounds = ComputeCloseButtonBounds(ref elementSpace);
            DrawCloseButton(closeButtonBounds, closeButtonState, e.Graphics);

            Rectangle imageBounds = ComputeImageBounds(ref elementSpace);
            DrawImage(imageBounds, e.Graphics);

            Rectangle textBounds = ComputeTextBounds(ref elementSpace);
            DrawText(textBounds, e.Graphics);
        }

        #endregion
    }


    /// <summary>
    /// Provides functionality for the WinForms Designer.
    /// </summary>
    public class TabItemDesigner : ControlDesigner {
        #region Fields

        private DesignerActionListCollection actionList;
        private ISelectionService selectionService;
        private IComponentChangeService changeService;
        private TabItem tabItem;
        private Adorner adorner;
        private Glyph selectionGlyph;
        private Glyph resizeGlyph;

        #endregion

        #region Public methods

        public override void Initialize(IComponent component) {
            base.Initialize(component);

            tabItem = component as TabItem;
            InitializeServices();

            // initialize adorners
            adorner = new Adorner();

            //BehaviorService.Adorners.Clear();
            BehaviorService.Adorners.Add(adorner);
            AutoResizeHandles = true;
            // add glyphs
            selectionGlyph = new TabItemResizeGlyph(BehaviorService, tabItem,
                                                    adorner, selectionService, changeService);
            adorner.Glyphs.Add(selectionGlyph);
        }


        public void ReselectTab() {
            List<TabItem> list = new List<TabItem>(1);

            list.Add(tabItem);
            selectionService.SetSelectedComponents(null);
            selectionService.SetSelectedComponents(list);
        }


        public override DesignerActionListCollection ActionLists {
            get {
                if(actionList == null) {
                    actionList = new DesignerActionListCollection();
                    actionList.Add(new TabItemActionList(this.Component));
                }

                return actionList;
            }
        }

        #endregion

        #region Private methods

        private void InitializeServices() {
            selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
            changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        }

        #endregion
    }


    public class TabItemResizeGlyph : Glyph {
        #region Constants

        private const int GlyphWidth = 26;
        private const int GlyphHeight = 26;

        #endregion

        #region Fields

        private TabItem tab;
        private BehaviorService service;
        private Adorner adorner;
        private ISelectionService selectionService;
        private IComponentChangeService changeService;
        private Rectangle glyphBounds;
        private bool selected;

        #endregion

        public TabItemResizeGlyph(BehaviorService behaviorSvc, Control control, Adorner glyphAdorner,
                                  ISelectionService selectionService, IComponentChangeService changeService)
            : base(new TabItemResizeBehavior(control as TabItem)) {
            service = behaviorSvc;
            tab = control as TabItem;
            adorner = glyphAdorner;
            this.selectionService = selectionService;
            this.changeService = changeService;

            // add events
            selectionService.SelectionChanged += OnSelectionChanged;
            changeService.ComponentChanged += OnComponentChanged;
        }

        #region Properties

        public TabItem Tab {
            get { return tab; }
        }

        public Adorner Adorner {
            get { return adorner; }
        }

        #endregion

        #region Private methods

        private void ComputeBounds() {
            Point edge = service.ControlToAdornerWindow(tab);
            TabAlignment alignment = TabAlignment.Top;

            // determine the tab alignment
            if(tab.Owner != null) {
                alignment = tab.Owner.TabAlignment;
            }

            if(alignment == TabAlignment.Top || alignment == TabAlignment.Bottom) {
                glyphBounds = new Rectangle(edge.X + tab.Width - GlyphWidth / 2 + 1, 
                                            edge.Y + tab.Height / 2 - GlyphHeight / 2,
                                            GlyphWidth, GlyphHeight);
            }
            else {
                glyphBounds = new Rectangle(edge.X + tab.Width / 2 - GlyphWidth / 2, 
                                            edge.Y + tab.Height - GlyphHeight / 2 + 1,
                                            GlyphWidth, GlyphHeight);
            }
        }

        private void OnSelectionChanged(object sender, EventArgs e) {
            if(object.ReferenceEquals(selectionService.PrimarySelection, tab)) {
                // activate the adorner
                ComputeBounds();
                adorner.Enabled = true;
                selected = true;
            }
            else {
                adorner.Enabled = false;
                selected = false;
            }
        }


        private void OnComponentChanged(object sender, ComponentChangedEventArgs e) {
            if(object.ReferenceEquals(e.Component, tab)) {
                // check changed property
                if(e.Member.Name == "TabWidth"  ||
                   e.Member.Name == "TabHeight" ||
                   e.Member.Name == "Location") {
                    //redraw the adorner
                    ComputeBounds();
                    adorner.Invalidate();
                }
            }
        }

        #endregion

        #region Public methods

        public override Cursor GetHitTest(Point p) {
            if(Bounds.Contains(p)) {
                TabAlignment alignment = TabAlignment.Top;

                if(tab.Owner != null) {
                    alignment = tab.Owner.TabAlignment;
                }

                if((alignment == TabAlignment.Top) || (alignment == TabAlignment.Bottom)) {
                    return Cursors.SizeWE;
                }
                else {
                    return Cursors.SizeNS;
                }
            }

            return null;
        }


        public override Rectangle Bounds {
            get {
                return glyphBounds;
            }
        }


        public override void Paint(PaintEventArgs pe) {
            /*if(adorner.Enabled && selected) {
                int x = glyphBounds.Left + glyphBounds.Width / 2 - Properties.Resources.bullet_blue.Width / 2;
                int y = glyphBounds.Top + glyphBounds.Height / 2 - Properties.Resources.bullet_blue.Height / 2;

                pe.Graphics.DrawImageUnscaled(Properties.Resources.bullet_blue, x, y);
            }*/
        }

        #endregion
    }


    public class TabItemResizeBehavior : Behavior {
        #region Fields

        private TabItem tab;
        private bool resizing;
        private int startWidth;
        private Point startLocation;

        #endregion

        public TabItemResizeBehavior(TabItem tab) {
            this.tab = tab;
        }

        public override bool OnMouseDown(Glyph g, MouseButtons button, Point mouseLocation) {
            // start resizing operation
            startLocation = mouseLocation;
            startWidth = tab.TabWidth;
            resizing = true;

            return true;
        }


        public override bool OnMouseMove(Glyph g, MouseButtons button, Point mouseLocation) {
            if(resizing) {
                TabAlignment alignment = TabAlignment.Top;
                PropertyDescriptor tabWidthProperty = TypeDescriptor.GetProperties(tab)["TabWidth"];

                if(tab.Owner != null) {
                    alignment = tab.Owner.TabAlignment;
                }

                // compute the width
                int width;

                if((alignment == TabAlignment.Top) || (alignment == TabAlignment.Bottom)) {
                    width = Math.Max(1, startWidth + (mouseLocation.X - startLocation.X));
                }
                else {
                    width = Math.Max(1, startWidth + (mouseLocation.Y - startLocation.Y));
                }

                // set the property only if there is a change in width
                if(width != tab.TabWidth) {
                    tabWidthProperty.SetValue(tab, width);
                }
            }

            return true;
        }


        public override bool OnMouseUp(Glyph g, MouseButtons button) {
            // stop resizing operation
            resizing = false;

            return true;
        }
    }


    public class TabItemActionList : DesignerActionList {
        #region Fields

        private TabItem tabItem;
        private DesignerActionUIService service;
        private TabItemDesigner tabItemDesigner;

        #endregion

        #region Private methods

        private PropertyDescriptor GetPropertyByName(string name) {
            return TypeDescriptor.GetProperties(tabItem)[name];
        }

        #endregion

        #region Properties

        public bool AutoEllipsis {
            get { return tabItem.AutoEllipsis; }
            set { GetPropertyByName("AutoEllipsis").SetValue(tabItem, value); }
        }

        public Color BackColor {
            get { return tabItem.BackColor; }
            set { GetPropertyByName("BackColor").SetValue(tabItem, value); }
        }

        public Color ForeColor {
            get { return tabItem.ForeColor; }
            set { GetPropertyByName("ForeColor").SetValue(tabItem, value); }
        }

        public Color SelectedBackColor {
            get { return tabItem.SelectedBackColor; }
            set { GetPropertyByName("SelectedBackColor").SetValue(tabItem, value); }
        }

        public Color SelectedForeColor {
            get { return tabItem.SelectedForeColor; }
            set { GetPropertyByName("SelectedForeColor").SetValue(tabItem, value); }
        }

        public Color HighlightBackColor {
            get { return tabItem.HighlightBackColor; }
            set { GetPropertyByName("HighlightBackColor").SetValue(tabItem, value); }
        }

        public Color HighlightForeColor {
            get { return tabItem.HighlightForeColor; }
            set { GetPropertyByName("HighlightForeColor").SetValue(tabItem, value); }
        }

        public Image Image {
            get { return tabItem.Image; }
            set { GetPropertyByName("Image").SetValue(tabItem, value); }
        }

        #endregion

        #region Constructor

        public TabItemActionList(IComponent component)
            : base(component) {
            tabItem = component as TabItem;
            service = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;

            IDesignerHost host = Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
            tabItemDesigner = host.GetDesigner(component) as TabItemDesigner;
        }

        #endregion

        #region Public methods

        public override DesignerActionItemCollection GetSortedActionItems() {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            items.Add(new DesignerActionHeaderItem("Normal State"));
            items.Add(new DesignerActionHeaderItem("Selected State"));
            items.Add(new DesignerActionHeaderItem("Highlighted State"));
            items.Add(new DesignerActionHeaderItem("Misc"));
            items.Add(new DesignerActionHeaderItem("Tab Order"));


            // add close button properties
            items.Add(new DesignerActionPropertyItem("BackColor", "Back Color", "Normal State"));
            items.Add(new DesignerActionPropertyItem("ForeColor", "Fore Color", "Normal State"));
            items.Add(new DesignerActionPropertyItem("SelectedBackColor", "Back Color", "Selected State"));
            items.Add(new DesignerActionPropertyItem("SelectedForeColor", "Fore Color", "Selected State"));
            items.Add(new DesignerActionPropertyItem("HighlightBackColor", "Back Color", "Highlighted State"));
            items.Add(new DesignerActionPropertyItem("HighlightForeColor", "Fore Color", "Highlighted State"));
            items.Add(new DesignerActionPropertyItem("Image", "Image", "Misc"));

            // add methods
            items.Add(new DesignerActionMethodItem(this, "MoveLeft", "Move Left", "Tab Order"));
            items.Add(new DesignerActionMethodItem(this, "MoveRight", "Move Right", "Tab Order"));

            return items;
        }


        public void MoveLeft() {
            TabHost host = tabItem.Owner;

            if(host != null) {
                int oldIndex = host.Tabs.IndexOf(tabItem);

                if(oldIndex > 0) {
                    int newIndex = oldIndex - 1;

                    host.Tabs.RemoveAt(oldIndex);
                    host.Tabs.Insert(newIndex, tabItem);
                    host.Refresh();

                    // set the Tabs property so that the designer saves the changes
                    TypeDescriptor.GetProperties(host)["Tabs"].SetValue(host, host.Tabs);

                    tabItemDesigner.ReselectTab();
                    service.HideUI(tabItem);
                    service.ShowUI(tabItem);
                }
            }
        }


        public void MoveRight() {
            TabHost host = tabItem.Owner;

            if(host != null) {
                int oldIndex = host.Tabs.IndexOf(tabItem);

                if(oldIndex < host.Tabs.Count - 1) {
                    int newIndex = oldIndex + 1;

                    host.Tabs.RemoveAt(oldIndex);
                    host.Tabs.Insert(newIndex, tabItem);
                    host.Refresh();

                    // set the Tabs property so that the designer saves the changes
                    TypeDescriptor.GetProperties(host)["Tabs"].SetValue(host, host.Tabs);

                    tabItemDesigner.ReselectTab();
                    service.HideUI(tabItem);
                    service.ShowUI(tabItem);
                }
            }
        }

        #endregion
    }
}
