namespace WLVWorkout
{
	partial class WLVWorkoutContainer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.WorkoutHeader = new WLVWorkout.WLVWorkoutHeader();
			this.SuspendLayout();
			// 
			// WorkoutHeader
			// 
			this.WorkoutHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.WorkoutHeader.Location = new System.Drawing.Point(0, 0);
			this.WorkoutHeader.MinimumSize = new System.Drawing.Size(0, 25);
			this.WorkoutHeader.Name = "WorkoutHeader";
			this.WorkoutHeader.Size = new System.Drawing.Size(800, 32);
			this.WorkoutHeader.TabIndex = 0;
			// 
			// WLVWorkoutContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.WorkoutHeader);
			this.Name = "WLVWorkoutContainer";
			this.ResumeLayout(false);

		}

		#endregion

		private WLVWorkoutHeader WorkoutHeader;
	}
}
