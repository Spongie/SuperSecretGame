namespace CVLevelEditor
{
    partial class LevelOptions
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
            this.comboBox_Types = new System.Windows.Forms.ComboBox();
            this.label_Type = new System.Windows.Forms.Label();
            this.comboBox_Status = new System.Windows.Forms.ComboBox();
            this.checkBox_Collision = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_load = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.checkBox_mouseMode = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_Types
            // 
            this.comboBox_Types.FormattingEnabled = true;
            this.comboBox_Types.Location = new System.Drawing.Point(54, 9);
            this.comboBox_Types.Name = "comboBox_Types";
            this.comboBox_Types.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Types.TabIndex = 0;
            this.comboBox_Types.SelectedIndexChanged += new System.EventHandler(this.comboBox_Types_SelectedIndexChanged);
            // 
            // label_Type
            // 
            this.label_Type.AutoSize = true;
            this.label_Type.Location = new System.Drawing.Point(13, 12);
            this.label_Type.Name = "label_Type";
            this.label_Type.Size = new System.Drawing.Size(31, 13);
            this.label_Type.TabIndex = 1;
            this.label_Type.Text = "Type";
            // 
            // comboBox_Status
            // 
            this.comboBox_Status.FormattingEnabled = true;
            this.comboBox_Status.Location = new System.Drawing.Point(181, 9);
            this.comboBox_Status.Name = "comboBox_Status";
            this.comboBox_Status.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Status.TabIndex = 2;
            // 
            // checkBox_Collision
            // 
            this.checkBox_Collision.AutoSize = true;
            this.checkBox_Collision.Location = new System.Drawing.Point(16, 36);
            this.checkBox_Collision.Name = "checkBox_Collision";
            this.checkBox_Collision.Size = new System.Drawing.Size(64, 17);
            this.checkBox_Collision.TabIndex = 3;
            this.checkBox_Collision.Text = "Collision";
            this.checkBox_Collision.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_mouseMode);
            this.groupBox1.Controls.Add(this.button_load);
            this.groupBox1.Controls.Add(this.button_save);
            this.groupBox1.Location = new System.Drawing.Point(16, 276);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 48);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Global Actions";
            // 
            // button_load
            // 
            this.button_load.Location = new System.Drawing.Point(87, 19);
            this.button_load.Name = "button_load";
            this.button_load.Size = new System.Drawing.Size(72, 23);
            this.button_load.TabIndex = 1;
            this.button_load.Text = "Load Map";
            this.button_load.UseVisualStyleBackColor = true;
            this.button_load.Click += new System.EventHandler(this.button_load_Click);
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(6, 19);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 0;
            this.button_save.Text = "SaveMap";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // checkBox_mouseMode
            // 
            this.checkBox_mouseMode.AutoSize = true;
            this.checkBox_mouseMode.Location = new System.Drawing.Point(193, 25);
            this.checkBox_mouseMode.Name = "checkBox_mouseMode";
            this.checkBox_mouseMode.Size = new System.Drawing.Size(115, 17);
            this.checkBox_mouseMode.TabIndex = 2;
            this.checkBox_mouseMode.Text = "On=Click Off=Drag";
            this.checkBox_mouseMode.UseVisualStyleBackColor = true;
            // 
            // LevelOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 333);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox_Collision);
            this.Controls.Add(this.comboBox_Status);
            this.Controls.Add(this.label_Type);
            this.Controls.Add(this.comboBox_Types);
            this.Name = "LevelOptions";
            this.Text = "LevelOptions";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Type;
        private System.Windows.Forms.ComboBox comboBox_Types;
        private System.Windows.Forms.ComboBox comboBox_Status;
        private System.Windows.Forms.CheckBox checkBox_Collision;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_load;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.CheckBox checkBox_mouseMode;
    }
}