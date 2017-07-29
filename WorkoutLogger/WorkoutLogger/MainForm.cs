using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WLVEntry {
	public partial class MainForm : Form {
		public MainForm() {
			InitializeComponent();
			
		}

		private void newWorkoutToolStripMenuItem_Click(object sender, EventArgs e) {
			//this.wlvSidePane1.BackColor = Color.Beige;

			System.Windows.Forms.Label tLabel = new Label();
			tLabel.AutoSize = true;
			tLabel.Location = new System.Drawing.Point(12, 24);
			tLabel.Name = "tLabel";
			tLabel.Size = new System.Drawing.Size(35, 13);
			tLabel.TabIndex = 1;
			tLabel.Text = "Fuck this shit";
			this.label1.Dispose();
			this.label1 = tLabel;
			this.Controls.Add(label1);
			label1.Text = "Something";
			this.DebugLabel.Text = label1.Text;
		}
    }
}
