using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TabControlTester {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void tabItem2_CloseButtonPressed(object sender, EventArgs e) {
            tabHost1.Tabs.Remove(sender);
            tabHost1.Tabs[0].Selected = true;
        }
    }
}
