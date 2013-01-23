// Copyright (c) Gratian Lup. All rights reserved.
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Windows.Forms.Design.Behavior;
using System.Drawing.Drawing2D;

namespace TabControl {
    public enum CloseButtonAlignment {
        Left, Right
    }


    [DesignerAttribute(typeof(TabHostDesigner))]
    public partial class TabHost : UserControl {
        #region Nested types

        private enum InsertDirection {
            Left, Rigth, None
        }

        #endregion

        #region Constants

        public const int DefaultTabWidth = 100;
        private const int DefaultTabHeight = 26;
        private const int DefaultSelectedTabHeight = 30;
        private const int DefaultTabDistance = 1;
        private const int DefaultCloseButtonBorderOpacity = 150;
        private const int InsertionMarkerWidth = 10;
        private const int InsertionMarkerHeight = 5;

        #endregion

        #region Constructor

        public TabHost() {
            InitializeComponent();

            tabs = new TabItemCollection(this);
            TabAlignment = TabAlignment.Top;
            TabDistance = DefaultTabDistance;
            TabHeight = DefaultTabHeight;
            SelectedTabHeight = DefaultSelectedTabHeight;

            // enable double-buffering
            this.SetStyle(ControlStyles.DoubleBuffer |
                          ControlStyles.UserPaint    |
                          ControlStyles.AllPaintingInWmPaint,
                          true);

            this.UpdateStyles();
        }

        #endregion

        #region Fields

        private TabItemCollection tabs;
        private bool reordering;
        private DragDropTabVisual reorderingVisual;
        private TabItem reorderingTab;
        private int lastInsertIndex;
        private InsertDirection lastInsertDirection;

        #endregion

        #region Properties

        private bool _showCloseButtons;
        [Category("Appearance")]
        [Browsable(true)]
        public bool ShowCloseButtons {
            get { return _showCloseButtons; }
            set {
                if(_showCloseButtons != value) {
                    _showCloseButtons = value;
                    ForceUpdate();
                }
            }
        }


        private bool _closeButtonsOnlyForSelected;
        [Category("Appearance")]
        [Browsable(true)]
        public bool CloseButtonsOnlyForSelected {
            get { return _closeButtonsOnlyForSelected; }
            set {
                if(_closeButtonsOnlyForSelected != value) {
                    _closeButtonsOnlyForSelected = value;
                    ForceUpdate();
                }
            }
        }


        private CloseButtonAlignment _closeButtonAlignment;
        [Category("Appearance")]
        [Browsable(true)]
        public CloseButtonAlignment CloseButtonAlignment {
            get { return _closeButtonAlignment; }
            set {
                if(_closeButtonAlignment != value) {
                    _closeButtonAlignment = value;

                    if(_showCloseButtons) {
                        ForceUpdate();
                    }
                }
            }
        }


        private Color _closeButtonColor;
        [Category("Appearance")]
        [Browsable(true)]
        public Color CloseButtonColor {
            get { return _closeButtonColor; }
            set {
                if(_closeButtonColor != value) {
                    _closeButtonColor = value;

                    if(_showCloseButtons) {
                        ForceUpdate();
                    }
                }
            }
        }


        private Color _closeButtonColorSelected;
        [Category("Appearance")]
        [Browsable(true)]
        public Color CloseButtonColorSelected {
            get { return _closeButtonColorSelected; }
            set {
                if(_closeButtonColorSelected != value) {
                    _closeButtonColorSelected = value;

                    if(_showCloseButtons) {
                        ForceUpdate();
                    }
                }
            }
        }


        private Color _closeButtonOverColor;
        [Category("Appearance")]
        [Browsable(true)]
        public Color CloseButtonOverColor {
            get { return _closeButtonOverColor; }
            set { _closeButtonOverColor = value; }
        }


        private Color _closeButtonOverColorSelected;
        [Category("Appearance")]
        [Browsable(true)]
        public Color CloseButtonOverColorSelected {
            get { return _closeButtonOverColorSelected; }
            set { _closeButtonOverColorSelected = value; }
        }


        private Color _closeButtonPressedColor;
        [Category("Appearance")]
        [Browsable(true)]
        public Color CloseButtonPressedColor {
            get { return _closeButtonPressedColor; }
            set { _closeButtonPressedColor = value; }
        }


        private int _closeButtonBorderOpacity;
        [Category("Appearance")]
        [Browsable(true)]
        public int CloseButtonBorderOpacity {
            get { return _closeButtonBorderOpacity; }
            set {
                if(_closeButtonBorderOpacity != value) {
                    _closeButtonBorderOpacity = value;

                    if(_showCloseButtons) {
                        ForceUpdate();
                    }
                }
            }
        }


        private int _closeButtonBorderOpacitySelected;
        [Category("Appearance")]
        [Browsable(true)]
        public int CloseButtonBorderOpacitySelected {
            get { return _closeButtonBorderOpacitySelected; }
            set {
                if(_closeButtonBorderOpacitySelected != value) {
                    _closeButtonBorderOpacitySelected = value;

                    if(_showCloseButtons) {
                        ForceUpdate();
                    }
                }
            }
        }


        private int _tabHeight;
        [Category("Appearance")]
        [Browsable(true)]
        public int TabHeight {
            get { return _tabHeight; }
            set {
                if(_tabHeight != value) {
                    _tabHeight = value;
                    Relayout();
                }
            }
        }


        private int _selectedTabHeight;
        [Category("Appearance")]
        [Browsable(true)]
        public int SelectedTabHeight {
            get { return _selectedTabHeight; }
            set {
                if(_selectedTabHeight != value) {
                    _selectedTabHeight = value;
                    Relayout();
                }
            }
        }


        private int _tabDistance;
        [Category("Appearance")]
        [Browsable(true)]
        public int TabDistance {
            get { return _tabDistance; }
            set {
                if(_tabDistance != value) {
                    _tabDistance = value;
                    Relayout();
                }
            }
        }


        private TabAlignment _tabAlignment;
        [Category("Appearance")]
        [Browsable(true)]
        public TabAlignment TabAlignment {
            get { return _tabAlignment; }
            set {
                if(_tabAlignment != value) {
                    _tabAlignment = value;

                    Relayout();
                }
            }
        }


        private bool _allowTabReordering;
        [Category("Behavior")]
        [Browsable(true)]
        public bool AllowTabReordering {
            get { return _allowTabReordering; }
            set { _allowTabReordering = value; }
        }


        private Color _insertionMarkerColor;
        [Category("Appearance")]
        [Browsable(true)]
        public Color InsertionMarkerColor {
            get { return _insertionMarkerColor; }
            set { _insertionMarkerColor = value; }
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), 
                         typeof(System.Drawing.Design.UITypeEditor))]
        [Browsable(true)]
        public TabItemCollection Tabs {
            get { return tabs; }
            set { tabs = value; }
        }

        #endregion

        #region Overriden methods

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);

            LayoutTabs();
            ForceUpdate();
        }

        protected override void OnBackColorChanged(EventArgs e) {
            base.OnBackColorChanged(e);
            ForceUpdate();
        }

        protected override void OnPaddingChanged(EventArgs e) {
            base.OnPaddingChanged(e);
            Relayout();
        }

        #endregion

        #region Private methods

        private int GetTabStartPosition() {
            if(_tabAlignment == TabAlignment.Top) {
                return this.Padding.Left;
            }
            else if(_tabAlignment == TabAlignment.Bottom) {
                return this.Padding.Left;
            }
            else if(_tabAlignment == TabAlignment.Left) {
                return this.Padding.Top;
            }
            else if(_tabAlignment == TabAlignment.Right) {
                return this.Padding.Top;
            }

            return 0;
        }


        private int GetTabHeight(TabItem item) {
            if(item.Selected) {
                return item.SelectedTabHeigth == 0 ? _selectedTabHeight : item.SelectedTabHeigth;
            }
            else {
                return item.TabHeigth == 0 ? _tabHeight : item.TabHeigth;
            }
        }


        private void LayoutTabs() {
            // set tab position based on TabAlignment
            int tabPosition = GetTabStartPosition();

            for(int i = 0; i < tabs.Count; i++) {
                TabItem tabItem = tabs[i];
                int tabWidth = tabItem.TabWidth;
                int tabHeight = GetTabHeight(tabItem);

                // set position
                switch(_tabAlignment) {
                    case TabAlignment.Top: {
                        tabItem.Left = tabPosition;
                        tabItem.Top = this.Height - tabHeight - Padding.Bottom;
                        tabItem.Width = tabWidth;
                        tabItem.Height = tabHeight;
                        break;
                    }
                    case TabAlignment.Bottom: {
                        tabItem.Left = tabPosition;
                        tabItem.Top = Padding.Top;
                        tabItem.Width = tabWidth;
                        tabItem.Height = tabHeight;
                        break;
                    }
                    case TabAlignment.Left: {
                        tabItem.Left = Padding.Left;
                        tabItem.Top = tabPosition;
                        tabItem.Width = tabHeight;
                        tabItem.Height = tabWidth;
                        break;
                    }
                    case TabAlignment.Right: {
                        tabItem.Left = this.Width - tabHeight - Padding.Right;
                        tabItem.Top = tabPosition;
                        tabItem.Width = tabHeight;
                        tabItem.Height = tabWidth;
                        break;
                    }
                }

                // increment position values
                tabPosition += tabWidth + _tabDistance;
            }
        }


        private void ForceUpdate() {
            this.Refresh();
        }


        private Point GetVisualLocation(TabItem tab, Point mousePosition) {
            Point location = Point.Empty;

            switch(_tabAlignment) {
            case TabAlignment.Top: {
                    location = new Point(KeepInHostWidth(tab.Left + mousePosition.X - tab.Width / 2, tab.Width),
                                         this.Height - tab.Height);
                    break;
                }
                case TabAlignment.Bottom: {
                    location = new Point(KeepInHostWidth(tab.Left + mousePosition.X - tab.Width / 2, tab.Width), 0);
                    break;
                }
                case TabAlignment.Left: {
                        location = new Point(0, KeepInHostHeight(tab.Top + mousePosition.Y - tab.Height / 2, tab.Height));
                        break;
                    }
                case TabAlignment.Right: {
                    location = new Point(this.Width - tab.Width,
                                         KeepInHostHeight(tab.Top + mousePosition.Y - tab.Height / 2, tab.Height));
                    break;
                }
            }

            // return the location in screen coordinates
            return this.PointToScreen(location);
        }


        private Point MousePositionToHost(TabItem tab, Point mousePosition) {
            Point location = mousePosition;

            if((_tabAlignment == TabAlignment.Top) || 
               (_tabAlignment == TabAlignment.Bottom)) {
                location = new Point(mousePosition.X + tab.Left,
                                     mousePosition.Y);
            }
            else {
                location = new Point(mousePosition.X,
                                     mousePosition.Y + tab.Top);
            }

            return location;
        }


        private Point ConstrainPositionToHost(Point mousePosition) {
            TabItem lastTab = tabs[tabs.Count - 1];

            switch(_tabAlignment) {
            case TabAlignment.Top: {
                    return new Point(Math.Max(this.Padding.Left + 1, 
                                              Math.Min(lastTab.Left + lastTab.Width - 1, mousePosition.X)),
                                     this.Height - 1);
                }
            case TabAlignment.Bottom: {
                    return new Point(Math.Max(this.Padding.Left + 1, 
                                              Math.Min(lastTab.Left + lastTab.Width - 1, mousePosition.X)),
                                     1);
                }
            case TabAlignment.Left: {
                    return new Point(1,
                                     Math.Max(this.Padding.Top + 1, 
                                              Math.Min(lastTab.Top + lastTab.Height - 1, mousePosition.Y)));
                }
            case TabAlignment.Right: {
                    return new Point(this.Width - 1,
                                     Math.Max(this.Padding.Top + 1, 
                                              Math.Min(lastTab.Top + lastTab.Height - 1, mousePosition.Y)));
                }
            }

            return Point.Empty;
        }


        private int GetReorderInsertLocation(TabItem tab, Point mousePosition) {
            Point location = MousePositionToHost(tab, mousePosition);

            // constrain the location in the bounds of the host
            location = ConstrainPositionToHost(location);
            TabItem hoveredTab = this.GetChildAtPoint(location) as TabItem;

            if(hoveredTab != null) {
                return tabs.IndexOf(hoveredTab);
            }

            return -1; // no tab found under cursor
        }


        private InsertDirection GetInsertDirection(TabItem tab, Point mousePosition, int insertIndex) {
            Point location = MousePositionToHost(tab, mousePosition);
            int tabIndex = tabs.IndexOf(tab);

            // check the type of insertion
            if(insertIndex == tabIndex || insertIndex == -1) {
                // invalid insertion type
                return InsertDirection.None;
            }
            else {
                TabItem targetTab = tabs[insertIndex];

                if((_tabAlignment == TabAlignment.Top) || 
                   (_tabAlignment == TabAlignment.Bottom)) {
                    if((location.X - targetTab.Left) <= targetTab.Width / 2) {
                        // left side of the tab, check if valid
                        if(insertIndex != tabIndex + 1) {
                            return InsertDirection.Left;
                        }
                        else {
                            return InsertDirection.None;
                        }
                    }
                    else {
                        // right side of the tab, check if valid
                        if(insertIndex != tabIndex - 1) {
                            return InsertDirection.Rigth;
                        }
                        else {
                            return InsertDirection.None;
                        }
                    }
                }
                else {
                    if((location.Y - targetTab.Top) <= targetTab.Height / 2) {
                        // left side of the tab, check if valid
                        if(insertIndex != tabIndex + 1) {
                            return InsertDirection.Left;
                        }
                        else {
                            return InsertDirection.None;
                        }
                    }
                    else {
                        // right side of the tab, check if valid
                        if(insertIndex != tabIndex - 1) {
                            return InsertDirection.Rigth;
                        }
                        else {
                            return InsertDirection.None;
                        }
                    }
                }
            }
        }


        private Color GetCurrentTabBackColor(TabItem tab) {
            if(tab.Selected) {
                return tab.SelectedBackColor;
            }
            else {
                return tab.BackColor;
            }
        }


        private int KeepInHostWidth(int left, int width) {
            return Math.Max(0, Math.Min(0 + this.Width - width, left));
        }


        private int KeepInHostHeight(int top, int height) {
            return Math.Max(0, Math.Min(this.Height - height, top));
        }


        private void InsertTabAt(TabItem tab, int index) {
            int oldIndex = tabs.IndexOf(tab);
            int newIndex = index;

            tabs.RemoveAt(oldIndex);

            if(newIndex > oldIndex) {
                tabs.Insert(index - 1, tab);
            }
            else {
                tabs.Insert(index, tab);
            }

            // force relayout
            Relayout();
        }


        private void TabHost_Paint(object sender, PaintEventArgs e) {
            if(reordering == false) {
                return;
            }

            if(lastInsertDirection == InsertDirection.None) {
                // just fill the background and return
                e.Graphics.FillRectangle(new SolidBrush(this.BackColor), this.Bounds);
                return;
            }

            // we have a valid insert direction
            int x = 0;
            int y = 0;
            TabItem targetTab = tabs[lastInsertIndex];

            if((_tabAlignment == TabAlignment.Top) ||
               (_tabAlignment == TabAlignment.Bottom)) {
                // set left position
                if(lastInsertDirection == InsertDirection.Left) {
                    x = KeepInHostWidth(targetTab.Left - InsertionMarkerWidth / 2 - _tabDistance / 2, 
                                        InsertionMarkerWidth);
                }
                else {
                    x = KeepInHostWidth(targetTab.Left + targetTab.Width - InsertionMarkerWidth / 2 + 
                                        _tabDistance / 2 + (_tabDistance % 2 == 1 ? 1 : 0),
                                        InsertionMarkerWidth);
                }

                // set top position
                if(_tabAlignment == TabAlignment.Top) {
                    y = 0;
                }
                else {
                    y = this.Height - InsertionMarkerHeight;
                }
            }
            else {
                // set top position
                if(lastInsertDirection == InsertDirection.Left) {
                    y = KeepInHostHeight(targetTab.Top - InsertionMarkerHeight / 2 - _tabDistance / 2, 
                                         InsertionMarkerHeight);
                }
                else {
                    y = KeepInHostHeight(targetTab.Top + targetTab.Height - InsertionMarkerHeight / 2 + 
                                         _tabDistance / 2 + (_tabDistance % 2 == 1 ? 1 : 0), 
                                         InsertionMarkerHeight);
                }

                // set left position
                if(_tabAlignment == TabAlignment.Left) {
                    x = this.Width - InsertionMarkerHeight;
                }
                else {
                    x = 0;
                }
            }

            Rectangle insertioMarkerBounds = new Rectangle(x, y, 
                                                           InsertionMarkerWidth, 
                                                           InsertionMarkerHeight);
            DrawInsertionMarker(insertioMarkerBounds, e.Graphics);
        }

        #endregion

        #region Protected methods

        protected void DrawInsertionMarker(Rectangle bounds, Graphics g) {
            // set the brush
            Color markerColor = _insertionMarkerColor != Color.Empty ? 
                                _insertionMarkerColor : Color.Black;
            Brush markerBrush = new SolidBrush(markerColor);

            // create the triangle
            GraphicsPath path = new GraphicsPath();

            path.AddLine(0, 0, InsertionMarkerWidth, 0);
            path.AddLine(InsertionMarkerWidth, 0, InsertionMarkerWidth / 2, InsertionMarkerHeight);
            path.AddLine(InsertionMarkerWidth / 2, InsertionMarkerHeight, 0, 0);

            // We draw the triangle in a bitmap first. So it is possible to use the
            // same path to represent all insertion markers by rotating
            // the bitmap.
            using(Bitmap buffer = new Bitmap(InsertionMarkerWidth, InsertionMarkerHeight,
                                             System.Drawing.Imaging.PixelFormat.Format32bppArgb)) {
                using(Graphics helperGraphics = Graphics.FromImage(buffer)) {
                    helperGraphics.FillPath(markerBrush, path);
                }

                // draw the path according the TabAlignment
                switch(_tabAlignment) {
                    case TabAlignment.Top: {
                        // just draw it
                        g.DrawImageUnscaled(buffer, bounds.Left, bounds.Top);
                        break;
                    }
                    case TabAlignment.Bottom: {
                        // rotate the image 180 degrees
                        buffer.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        g.DrawImageUnscaled(buffer, bounds.Left, bounds.Top);
                        break;
                    }
                    case TabAlignment.Left: {
                        // rotate the image 180 degrees
                        buffer.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        g.DrawImageUnscaled(buffer, bounds.Left, bounds.Top);
                        break;
                    }
                    case TabAlignment.Right: {
                        // rotate the image 180 degrees
                        buffer.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        g.DrawImageUnscaled(buffer, bounds.Left, bounds.Top);
                        break;
                    }
                }
            }
        }

        #endregion

        #region Public methods

        public void ItemListChanged(TabItem item, bool remove) {
            if(remove) {
                // remove from control list
                Controls.Remove(item);
            }
            else {
                item.Owner = this;

                // add to control list
                if(this.Controls.Contains(item) == false) {
                    this.Controls.Add(item);
                }

                this.PerformLayout();
            }

            LayoutTabs();
        }


        public void ItemListChanged(TabItem[] items, bool remove) {
            // remove from control list
            foreach(TabItem tabItem in items) {
                if(remove) {
                    this.Controls.Remove(tabItem);
                }
                else {
                    if(Controls.Contains(tabItem) == false) {
                        this.Controls.Add(tabItem);
                    }
                }
            }

            LayoutTabs();
        }


        public void TabItemSelected(TabItem item) {
            // deselect all other tabs
            for(int i = 0; i < tabs.Count; i++) {
                if(tabs[i] != item) {
                    tabs[i].Selected = false;
                }
            }

            Relayout();
        }


        public void Relayout() {
            LayoutTabs();
            ForceUpdate();
        }


        public void TabStartDrag(TabItem tab, Point mousePosition) {
            reorderingVisual = new DragDropTabVisual(tab);

            // set visual location and show it
            reorderingVisual.Location = GetVisualLocation(tab, mousePosition);
            reorderingVisual.Show();

            // refocus the main form
            Form parentForm = this.FindForm();

            if(parentForm != null) {
                parentForm.Activate();
            }

            // set the tab that is reordered
            reorderingTab = tab;
            reordering = true;
        }


        public void TabEndDrag(TabItem tab, Point mousePosition) {
            if(reorderingVisual != null) {
                // destroy the visual
                reorderingVisual.Hide();
                reorderingVisual.Dispose();
                reorderingVisual = null;
            }

            // do the reordering
            // get the index of the tab below the mouse
            int insertIndex = GetReorderInsertLocation(tab, mousePosition);

            // get the insert direction
            lastInsertDirection = GetInsertDirection(tab, mousePosition, insertIndex);

            if(lastInsertDirection != InsertDirection.None) {
                if(lastInsertDirection == InsertDirection.Left) {
                    InsertTabAt(reorderingTab, insertIndex);
                }
                else {
                    InsertTabAt(reorderingTab, insertIndex + 1);
                }
            }

            reorderingTab = null;

            // remove insertion marker
            lastInsertDirection = InsertDirection.None;
            this.Invalidate();
            reordering = false;
        }


        public void TabUpdateDrag(TabItem tab, Point mousePosition) {
            if(reorderingVisual != null) {
                // update the position of the visual
                reorderingVisual.Location = GetVisualLocation(tab, mousePosition);
            }

            // get the index of the tab below the mouse
            int insertIndex = GetReorderInsertLocation(tab, mousePosition);
            lastInsertIndex = insertIndex == -1 ? lastInsertIndex : insertIndex;

            // get the insert direction
            lastInsertDirection = GetInsertDirection(tab, mousePosition, lastInsertIndex);
            this.Invalidate(); // force the host to draw the insertion marker
        }

        #endregion
    }


    /// <summary>
    /// Provides support for WinForms Designer.
    /// </summary>
    public class TabHostDesigner : ControlDesigner {
        private DesignerActionListCollection actionList;
        private IEventBindingService eventBindingService;
        private IDesignerHost host;
        private IComponentChangeService changeService;
        private ISelectionService selectionService;
        private TabHost tabHost;
        private Adorner adorner;
        private TabHostGlyph hostGlyph;

        public override void Initialize(IComponent component) {
            base.Initialize(component);

            tabHost = component as TabHost;
            InitializeServices();

            // initialize adorners
            adorner = new Adorner();
            BehaviorService.Adorners.Add(adorner);

            // add glyphs
            hostGlyph = new TabHostGlyph(BehaviorService, tabHost, adorner, selectionService);
            adorner.Glyphs.Add(hostGlyph);

            // add add/remove tab handlers
            changeService.ComponentRemoved += OnComponentRemoved;
        }

        protected override void Dispose(bool disposing) {
            if(disposing && adorner != null) {
                BehaviorService service = BehaviorService;

                if(service != null && adorner != null) {
                    service.Adorners.Remove(adorner);
                }

                // add/remove events
                changeService.ComponentRemoved -= OnComponentRemoved;
            }

            base.Dispose(disposing);
        }

        private void InitializeServices() {
            // event binding service
            host = GetService(typeof(IDesignerHost)) as IDesignerHost;
            changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
            selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
        }


        private void OnComponentRemoved(object sender, ComponentEventArgs ce) {
            TabItem item = ce.Component as TabItem;

            if(item != null) {
                // announce the tab host
                tabHost.Tabs.Remove(item);
                hostGlyph.ComputeBounds();
                adorner.Invalidate();
            }
        }


        public override DesignerActionListCollection ActionLists {
            get {
                if(actionList == null) {
                    actionList = new DesignerActionListCollection();
                    actionList.Add(new TabHostActionList(this.Component));
                }

                return actionList;
            }
        }

    }


    public class TabHostGlyph : Glyph {
        #region Constants

        private const int GlyphDistance = 10;
        private const int GlyphWidth = 16;
        private const int GlyphHeight = 16;

        #endregion

        #region Fields

        private TabHost tabHost;
        private BehaviorService service;
        private Adorner adorner;
        private ISelectionService selectionService;
        private Rectangle glyphBounds;
        private bool selected;

        #endregion

        public TabHostGlyph(BehaviorService behaviorSvc, Control control,
                            Adorner glyphAdorner, ISelectionService selectionService)
            : base(new TabHostBehavior()) {
            service = behaviorSvc;
            tabHost = control as TabHost;
            adorner = glyphAdorner;
            this.selectionService = selectionService;

            // add events
            selectionService.SelectionChanged += OnSelectionChanged;
        }

        #region Properties

        public TabHost TabHost {
            get { return tabHost; }
        }

        public Adorner Adorner {
            get { return adorner; }
        }

        #endregion

        private void OnSelectionChanged(object sender, EventArgs e) {
            if(object.ReferenceEquals(selectionService.PrimarySelection, tabHost)) {
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

        public override Cursor GetHitTest(Point p) {
            if(Bounds.Contains(p)) {
                return Cursors.Hand;
            }

            return null;
        }


        public override Rectangle Bounds {
            get {
                return glyphBounds;
            }
        }


        public void ComputeBounds() {
            Point edge = service.ControlToAdornerWindow(tabHost);
            int x = 0;
            int y = 0;

            if(tabHost.Tabs.Count > 0) {
                if(tabHost.TabAlignment == TabAlignment.Top) {
                    x = edge.X + tabHost.Tabs[tabHost.Tabs.Count - 1].Left +
                                 tabHost.Tabs[tabHost.Tabs.Count - 1].Width +
                                 GlyphDistance;

                    y = edge.Y + tabHost.Height - GlyphHeight - (tabHost.TabHeight - GlyphHeight) / 2;
                }
                else if(tabHost.TabAlignment == TabAlignment.Bottom) {
                    x = edge.X + tabHost.Tabs[tabHost.Tabs.Count - 1].Left +
                                 tabHost.Tabs[tabHost.Tabs.Count - 1].Width +
                                 GlyphDistance;

                    y = edge.Y + tabHost.TabHeight / 2 - GlyphHeight / 2;
                }
                else if(tabHost.TabAlignment == TabAlignment.Left) {
                    x = edge.X + tabHost.TabHeight / 2 - GlyphWidth / 2;

                    y = edge.Y + tabHost.Tabs[tabHost.Tabs.Count - 1].Top +
                        tabHost.Tabs[tabHost.Tabs.Count - 1].Height + GlyphDistance;
                }
                else {
                    x = edge.X + tabHost.Width - GlyphWidth - (tabHost.TabHeight - GlyphWidth) / 2;

                    y = edge.Y + tabHost.Tabs[tabHost.Tabs.Count - 1].Top +
                        tabHost.Tabs[tabHost.Tabs.Count - 1].Height + GlyphDistance;
                }
            }

            glyphBounds = new Rectangle(x, y, GlyphWidth, GlyphHeight);
        }


        public override void Paint(PaintEventArgs pe) {
            if(adorner.Enabled && selected) {
                //pe.Graphics.DrawImage(Properties.Resources.add, Bounds);
            }
        }
    }


    public class TabHostBehavior : Behavior {
        public override bool OnMouseDown(Glyph g, MouseButtons button, Point mouseLoc) {
            TabHostGlyph glyph = g as TabHostGlyph;

            if(glyph == null) {
                return base.OnMouseDown(g, button, mouseLoc);
            }

            // add a new tab
            IDesignerHost host = glyph.TabHost.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
            TabItem item = host.CreateComponent(typeof(TabItem)) as TabItem;

            // add the item to the tab host
            glyph.TabHost.Tabs.Add(item);

            // refresh the adorner
            glyph.ComputeBounds();
            glyph.Adorner.Invalidate();
            return true;
        }
    }


    public class TabHostActionList : DesignerActionList {
        #region Fields

        private TabHost tabHost;
        private DesignerActionUIService service;

        #endregion

        #region Private methods

        private PropertyDescriptor GetPropertyByName(string name) {
            return TypeDescriptor.GetProperties(tabHost)[name];
        }

        #endregion

        #region Properties

        public bool ShowCloseButtons {
            get { return tabHost.ShowCloseButtons; }
            set { GetPropertyByName("ShowCloseButtons").SetValue(tabHost, value); }
        }

        public Color CloseButtonColor {
            get { return tabHost.CloseButtonColor; }
            set { GetPropertyByName("CloseButtonColor").SetValue(tabHost, value); }
        }

        public Color CloseButtonColorSelected {
            get { return tabHost.CloseButtonColorSelected; }
            set { GetPropertyByName("CloseButtonColorSelected").SetValue(tabHost, value); }
        }

        public Color CloseButtonOverColor {
            get { return tabHost.CloseButtonOverColor; }
            set { GetPropertyByName("CloseButtonOverColor").SetValue(tabHost, value); }
        }

        public Color CloseButtonOverColorSelected {
            get { return tabHost.CloseButtonOverColorSelected; }
            set { GetPropertyByName("CloseButtonOverColorSelected").SetValue(tabHost, value); }
        }

        public Color CloseButtonPressedColor {
            get { return tabHost.CloseButtonPressedColor; }
            set { GetPropertyByName("CloseButtonPressedColor").SetValue(tabHost, value); }
        }

        public CloseButtonAlignment CloseButtonAlignment {
            get { return tabHost.CloseButtonAlignment; }
            set { GetPropertyByName("CloseButtonAlignment").SetValue(tabHost, value); }
        }

        public int CloseButtonBorderOpacity {
            get { return tabHost.CloseButtonBorderOpacity; }
            set { GetPropertyByName("CloseButtonBorderOpacity").SetValue(tabHost, value); }
        }

        public int CloseButtonBorderOpacitySelected {
            get { return tabHost.CloseButtonBorderOpacitySelected; }
            set { GetPropertyByName("CloseButtonBorderOpacitySelected").SetValue(tabHost, value); }
        }

        public TabItemCollection Tabs {
            get { return tabHost.Tabs; }
            set { tabHost.Tabs = value; }
        }

        #endregion

        #region Constructor

        public TabHostActionList(IComponent component)
            : base(component) {
            tabHost = component as TabHost;
            service = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        #endregion

        #region Public methods

        public override DesignerActionItemCollection GetSortedActionItems() {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            items.Add(new DesignerActionHeaderItem("Close Buttons"));
            items.Add(new DesignerActionHeaderItem("Behavior"));

            // add close button properties
            items.Add(new DesignerActionPropertyItem("ShowCloseButtons", "Show Close Buttons", "Close Buttons"));
            items.Add(new DesignerActionPropertyItem("CloseButtonColor", "Color", "Close Buttons"));
            items.Add(new DesignerActionPropertyItem("CloseButtonColorSelected", "Color (selected)", "Close Buttons"));
            items.Add(new DesignerActionPropertyItem("CloseButtonOverColor", "Over Color", "Close Buttons"));
            items.Add(new DesignerActionPropertyItem("CloseButtonOverColorSelected", "Over Color (selected)", "Close Buttons"));
            items.Add(new DesignerActionPropertyItem("CloseButtonPressedColor", "Pressed Color", "Close Buttons"));
            items.Add(new DesignerActionPropertyItem("CloseButtonAlignment", "Alignment", "Close Buttons"));
            items.Add(new DesignerActionPropertyItem("CloseButtonBorderOpacity", "Border Opacity", "Close Buttons"));
            items.Add(new DesignerActionPropertyItem("CloseButtonBorderOpacitySelected", "Border Opacity (selected)", "Close Buttons"));

            // add tabs
            items.Add(new DesignerActionPropertyItem("Tabs", "Tab Items", "Behavior"));

            return items;
        }

        #endregion
    }
}
