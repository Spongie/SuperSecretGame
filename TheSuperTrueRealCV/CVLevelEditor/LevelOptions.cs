using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CVCommon;
using CVStorage;

namespace CVLevelEditor
{
    public partial class LevelOptions : Form
    {
        public event EventHandler onMapLoaded;

        public LevelOptions()
        {
            InitializeComponent();
            comboBox_Types.DataSource = Enum.GetNames(typeof(PlatformType));
            comboBox_Status.DataSource = Enum.GetNames(typeof(PlatformStatus));
        }

        private void comboBox_Types_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Types.SelectedIndex == (int)PlatformType.CastleFloor)
                comboBox_Status.Visible = true;
            else
                comboBox_Status.Visible = false;

            checkBox_Collision.Checked = SelectedPlatformType == PlatformType.CastleFloor ? true : false;
        }

        public PlatformType SelectedPlatformType
        {
            get { return (PlatformType)comboBox_Types.SelectedIndex; }
        }

        public bool CollisionEnabled 
        { 
            get { return checkBox_Collision.Checked; } 
        }

        public bool ClickMode
        {
            get { return checkBox_mouseMode.Checked; }
        }

        public PlatformStatus SelectedPlatformStatus
        {
            get
            {
                if (!comboBox_Status.Visible)
                    return PlatformStatus.Normal;

                return (PlatformStatus)comboBox_Status.SelectedIndex;
            }
        }

        public Map GameMap { get; set; }

        private void button_save_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Map file|*.cmp";
            saveFileDialog.Title = "Save the map";
            saveFileDialog.ShowDialog();

            if(saveFileDialog.FileName != string.Empty)
            {
                GameMap.Name = GetMapName(saveFileDialog.FileName);
                GameMap.NormalizePlatforms();
                ObjectSerializer.Serialize<Map>(GameMap, saveFileDialog.FileName);
                GameMap.DeNormalizePlatforms();
            }
        }

        public string GetMapName(string piFileName)
        {
            int indexOfNameStart = piFileName.LastIndexOf("\\");
            int indexOfFileTypeStart = piFileName.LastIndexOf(".");

            return piFileName.Substring(indexOfNameStart + 1, indexOfFileTypeStart - indexOfNameStart - 1);
        }

        private void button_load_Click(object sender, EventArgs e)
        {
            var loadFileDialog = new OpenFileDialog();
            loadFileDialog.Filter = "Map file|*.cmp";
            loadFileDialog.Title = "Save the map";
            loadFileDialog.ShowDialog();

            if(loadFileDialog.FileName != string.Empty)
            {
                GameMap = ObjectSerializer.DeSerialize<Map>(loadFileDialog.FileName);
                if (onMapLoaded != null)
                    onMapLoaded(this, null);
            }
        }
    }
}
