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
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Collections;
using System.Drawing;

namespace TabControl {
    public class TabItemCollection : IList, IEnumerable, ICollection {
        #region Fields

        private TabHost owner;
        private List<TabItem> innerList;

        #endregion

        #region Constructor

        public TabItemCollection(TabHost collectionOwner) {
            if(collectionOwner == null) {
                throw new ArgumentNullException("collectionOwner");
            }

            owner = collectionOwner;
            innerList = new List<TabItem>();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return innerList.GetEnumerator();
        }

        #endregion

        #region IList Members

        public int Add(object value) {
            innerList.Add(value as TabItem);
            owner.ItemListChanged(value as TabItem, false);
            return 0;
        }

        public void Clear() {
            TabItem[] items = innerList.ToArray();
            innerList.Clear();
            owner.ItemListChanged(items, true);
        }

        public bool Contains(object value) {
            return innerList.Contains(value as TabItem);
        }

        public int IndexOf(object value) {
            return innerList.IndexOf(value as TabItem);
        }

        public void Insert(int index, object value) {
            innerList.Insert(index, value as TabItem);
            owner.ItemListChanged(value as TabItem, false);
        }

        public bool IsFixedSize {
            get { return false; }
        }

        public bool IsReadOnly {
            get { return false; }
        }

        public void Remove(object value) {
            innerList.Remove(value as TabItem);
            owner.ItemListChanged(value as TabItem, true);
        }

        public void RemoveAt(int index) {
            TabItem removedItem = innerList[index];
            innerList.RemoveAt(index);
            owner.ItemListChanged(removedItem, true);
        }

        object IList.this[int index] {
            get {
                return innerList[index];
            }
            set {
                innerList[index] = (TabItem)value;
            }
        }

        #endregion

        #region ICollection Members

        public void CopyTo(Array array, int index) {
            innerList.CopyTo(array as TabItem[], index);
        }

        public int Count {
            get { return innerList.Count; }
        }

        public bool IsSynchronized {
            get { return false; }
        }

        public object SyncRoot {
            get { return false; }
        }

        #endregion

        #region Public methods

        public void Add(string tabText) {
            TabItem tab = new TabItem();
            tab.TabText = tabText;

            Add(tab);
        }

        public void Add(string tabText, Image image) {
            TabItem tab = new TabItem();

            tab.TabText = tabText;
            tab.Image = image;

            Add(tab);
        }

        public bool Contains(TabItem tab) {
            return innerList.Contains(tab);
        }

        public int IndexOf(TabItem tab) {
            return innerList.IndexOf(tab);
        }

        public void Insert(int index, TabItem tab) {
            innerList.Insert(index, tab);
            owner.ItemListChanged(tab, false);
        }

        public void Remove(TabItem tab) {
            innerList.Remove(tab);
        }

        public TabItem this[int index] {
            get {
                return innerList[index];
            }
            set {
                innerList[index] = value;
            }
        }

        #endregion
    }
}
