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
            this.checkBox_mouseMode = new System.Windows.Forms.CheckBox();
            this.button_load = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_Platforms = new System.Windows.Forms.TabPage();
            this.tabPage_Props = new System.Windows.Forms.TabPage();
            this.trackBar_Intensity = new System.Windows.Forms.TrackBar();
            this.label_intensity = new System.Windows.Forms.Label();
            this.comboBox_PropTypes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage_Edit = new System.Windows.Forms.TabPage();
            this.tabPage_Spawning = new System.Windows.Forms.TabPage();
            this.ivComboBoxSpawn = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage_Platforms.SuspendLayout();
            this.tabPage_Props.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Intensity)).BeginInit();
            this.tabPage_Spawning.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_Types
            // 
            this.comboBox_Types.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Types.FormattingEnabled = true;
            this.comboBox_Types.Location = new System.Drawing.Point(43, 3);
            this.comboBox_Types.Name = "comboBox_Types";
            this.comboBox_Types.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Types.TabIndex = 0;
            this.comboBox_Types.SelectedIndexChanged += new System.EventHandler(this.comboBox_Types_SelectedIndexChanged);
            // 
            // label_Type
            // 
            this.label_Type.AutoSize = true;
            this.label_Type.Location = new System.Drawing.Point(6, 3);
            this.label_Type.Name = "label_Type";
            this.label_Type.Size = new System.Drawing.Size(31, 13);
            this.label_Type.TabIndex = 1;
            this.label_Type.Text = "Type";
            // 
            // comboBox_Status
            // 
            this.comboBox_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Status.FormattingEnabled = true;
            this.comboBox_Status.Location = new System.Drawing.Point(170, 3);
            this.comboBox_Status.Name = "comboBox_Status";
            this.comboBox_Status.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Status.TabIndex = 2;
            // 
            // checkBox_Collision
            // 
            this.checkBox_Collision.AutoSize = true;
            this.checkBox_Collision.Location = new System.Drawing.Point(9, 30);
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
            // checkBox_mouseMode
            // 
            this.checkBox_mouseMode.AutoSize = true;
            this.checkBox_mouseMode.Location = new System.Drawing.Point(174, 23);
            this.checkBox_mouseMode.Name = "checkBox_mouseMode";
            this.checkBox_mouseMode.Size = new System.Drawing.Size(115, 17);
            this.checkBox_mouseMode.TabIndex = 2;
            this.checkBox_mouseMode.Text = "On=Click Off=Drag";
            this.checkBox_mouseMode.UseVisualStyleBackColor = true;
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
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_Platforms);
            this.tabControl.Controls.Add(this.tabPage_Props);
            this.tabControl.Controls.Add(this.tabPage_Edit);
            this.tabControl.Controls.Add(this.tabPage_Spawning);
            this.tabControl.Location = new System.Drawing.Point(16, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(308, 258);
            this.tabControl.TabIndex = 5;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            // 
            // tabPage_Platforms
            // 
            this.tabPage_Platforms.Controls.Add(this.label_Type);
            this.tabPage_Platforms.Controls.Add(this.comboBox_Types);
            this.tabPage_Platforms.Controls.Add(this.checkBox_Collision);
            this.tabPage_Platforms.Controls.Add(this.comboBox_Status);
            this.tabPage_Platforms.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Platforms.Name = "tabPage_Platforms";
            this.tabPage_Platforms.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Platforms.Size = new System.Drawing.Size(300, 232);
            this.tabPage_Platforms.TabIndex = 0;
            this.tabPage_Platforms.Text = "Platforms";
            this.tabPage_Platforms.UseVisualStyleBackColor = true;
            // 
            // tabPage_Props
            // 
            this.tabPage_Props.Controls.Add(this.trackBar_Intensity);
            this.tabPage_Props.Controls.Add(this.label_intensity);
            this.tabPage_Props.Controls.Add(this.comboBox_PropTypes);
            this.tabPage_Props.Controls.Add(this.label1);
            this.tabPage_Props.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Props.Name = "tabPage_Props";
            this.tabPage_Props.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Props.Size = new System.Drawing.Size(300, 232);
            this.tabPage_Props.TabIndex = 1;
            this.tabPage_Props.Text = "Props";
            this.tabPage_Props.UseVisualStyleBackColor = true;
            // 
            // trackBar_Intensity
            // 
            this.trackBar_Intensity.Location = new System.Drawing.Point(63, 34);
            this.trackBar_Intensity.Maximum = 100;
            this.trackBar_Intensity.Name = "trackBar_Intensity";
            this.trackBar_Intensity.Size = new System.Drawing.Size(104, 45);
            this.trackBar_Intensity.TabIndex = 3;
            // 
            // label_intensity
            // 
            this.label_intensity.AutoSize = true;
            this.label_intensity.Location = new System.Drawing.Point(11, 50);
            this.label_intensity.Name = "label_intensity";
            this.label_intensity.Size = new System.Drawing.Size(46, 13);
            this.label_intensity.TabIndex = 2;
            this.label_intensity.Text = "Intensity";
            // 
            // comboBox_PropTypes
            // 
            this.comboBox_PropTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_PropTypes.FormattingEnabled = true;
            this.comboBox_PropTypes.Location = new System.Drawing.Point(50, 4);
            this.comboBox_PropTypes.Name = "comboBox_PropTypes";
            this.comboBox_PropTypes.Size = new System.Drawing.Size(121, 21);
            this.comboBox_PropTypes.TabIndex = 1;
            this.comboBox_PropTypes.SelectedIndexChanged += new System.EventHandler(this.comboBox_PropTypes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type: ";
            // 
            // tabPage_Edit
            // 
            this.tabPage_Edit.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Edit.Name = "tabPage_Edit";
            this.tabPage_Edit.Size = new System.Drawing.Size(300, 232);
            this.tabPage_Edit.TabIndex = 2;
            this.tabPage_Edit.Text = "Edit";
            this.tabPage_Edit.UseVisualStyleBackColor = true;
            // 
            // tabPage_Spawning
            // 
            this.tabPage_Spawning.Controls.Add(this.ivComboBoxSpawn);
            this.tabPage_Spawning.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Spawning.Name = "tabPage_Spawning";
            this.tabPage_Spawning.Size = new System.Drawing.Size(300, 232);
            this.tabPage_Spawning.TabIndex = 3;
            this.tabPage_Spawning.Text = "Spawning";
            this.tabPage_Spawning.UseVisualStyleBackColor = true;
            // 
            // ivComboBoxSpawn
            // 
            this.ivComboBoxSpawn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ivComboBoxSpawn.FormattingEnabled = true;
            this.ivComboBoxSpawn.Items.AddRange(new object[] {
            "PlayerSpawn",
            "Bat",
            "Skeleton",
            "SkeletonArmor",
            "SkeletonMusician"});
            this.ivComboBoxSpawn.Location = new System.Drawing.Point(83, 3);
            this.ivComboBoxSpawn.Name = "ivComboBoxSpawn";
            this.ivComboBoxSpawn.Size = new System.Drawing.Size(121, 21);
            this.ivComboBoxSpawn.TabIndex = 0;
            // 
            // LevelOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 333);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.groupBox1);
            this.Name = "LevelOptions";
            this.Text = "LevelOptions";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage_Platforms.ResumeLayout(false);
            this.tabPage_Platforms.PerformLayout();
            this.tabPage_Props.ResumeLayout(false);
            this.tabPage_Props.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Intensity)).EndInit();
            this.tabPage_Spawning.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_Platforms;
        private System.Windows.Forms.TabPage tabPage_Props;
        private System.Windows.Forms.TabPage tabPage_Edit;
        private System.Windows.Forms.ComboBox comboBox_PropTypes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar_Intensity;
        private System.Windows.Forms.Label label_intensity;
        private System.Windows.Forms.TabPage tabPage_Spawning;
        private System.Windows.Forms.ComboBox ivComboBoxSpawn;
    }
}