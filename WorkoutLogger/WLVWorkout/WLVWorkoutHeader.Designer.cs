namespace WLVWorkout
{
	partial class WLVWorkoutHeader
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.WorkoutNameTextField = new System.Windows.Forms.TextBox();
			this.WorkoutNameLabel = new System.Windows.Forms.Label();
			this.SchemePicker = new System.Windows.Forms.ComboBox();
			this.SchemeLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// WorkoutNameTextField
			// 
			this.WorkoutNameTextField.Location = new System.Drawing.Point(88, 6);
			this.WorkoutNameTextField.Name = "WorkoutNameTextField";
			this.WorkoutNameTextField.Size = new System.Drawing.Size(224, 20);
			this.WorkoutNameTextField.TabIndex = 0;
			// 
			// WorkoutNameLabel
			// 
			this.WorkoutNameLabel.AutoSize = true;
			this.WorkoutNameLabel.Location = new System.Drawing.Point(3, 10);
			this.WorkoutNameLabel.Name = "WorkoutNameLabel";
			this.WorkoutNameLabel.Size = new System.Drawing.Size(79, 13);
			this.WorkoutNameLabel.TabIndex = 1;
			this.WorkoutNameLabel.Text = "Workout Name";
			// 
			// SchemePicker
			// 
			this.SchemePicker.FormattingEnabled = true;
			this.SchemePicker.Location = new System.Drawing.Point(370, 6);
			this.SchemePicker.Name = "SchemePicker";
			this.SchemePicker.Size = new System.Drawing.Size(97, 21);
			this.SchemePicker.TabIndex = 2;
			// 
			// SchemeLabel
			// 
			this.SchemeLabel.AutoSize = true;
			this.SchemeLabel.Location = new System.Drawing.Point(318, 10);
			this.SchemeLabel.Name = "SchemeLabel";
			this.SchemeLabel.Size = new System.Drawing.Size(46, 13);
			this.SchemeLabel.TabIndex = 3;
			this.SchemeLabel.Text = "Scheme";
			// 
			// WLVWorkoutHeader
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.SchemeLabel);
			this.Controls.Add(this.SchemePicker);
			this.Controls.Add(this.WorkoutNameLabel);
			this.Controls.Add(this.WorkoutNameTextField);
			this.MinimumSize = new System.Drawing.Size(0, 25);
			this.Name = "WLVWorkoutHeader";
			this.Size = new System.Drawing.Size(800, 32);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox WorkoutNameTextField;
		private System.Windows.Forms.Label WorkoutNameLabel;
		private System.Windows.Forms.ComboBox SchemePicker;
		private System.Windows.Forms.Label SchemeLabel;
	}
}
