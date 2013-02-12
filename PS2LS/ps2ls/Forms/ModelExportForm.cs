using ps2ls.Assets.Dme;
using ps2ls.Assets.Pack;
using ps2ls.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace ps2ls.Forms
{
    public partial class ModelExportForm : Form
    {
        public ModelExportForm()
        {
            InitializeComponent();
        }

        public List<String> FileNames { get; set; }

        private GenericLoadingForm loadingForm;
        private BackgroundWorker exportBackgroundWorker = new BackgroundWorker();
        private ModelExporterStatic.ExportOptions exportOptions = new ModelExporterStatic.ExportOptions();

        private void exportDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = exportModel(sender, e.Argument);
        }

        private void exportRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loadingForm.Close();

            Close();

            MessageBox.Show("Successfully exported " + (Int32)e.Result + " models.");
        }

        private void exportProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (loadingForm != null)
            {
                loadingForm.SetLabelText((String)e.UserState);
                loadingForm.SetProgressBarPercent(e.ProgressPercentage);
            }
        }

        private Int32 exportModel(object sender, object argument)
        {
            List<object> arguments = (List<object>)argument;

            String directory = (String)arguments[0];
            List<String> fileNames = (List<String>)arguments[1];
            ModelExporterStatic.ExportOptions exportOptions = (ModelExporterStatic.ExportOptions)arguments[2];

            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;

            Int32 result = 0;

            for (Int32 i = 0; i < fileNames.Count; ++i)
            {
                String fileName = fileNames[i];

                MemoryStream memoryStream = AssetManager.Instance.CreateAssetMemoryStreamByName(fileName);

                if (memoryStream == null)
                {
                    continue;
                }

                Model model = Model.LoadFromStream(fileName, memoryStream);

                if (model == null)
                {
                    continue;
                }

                ModelExporterStatic.ExportModelToDirectory(model, directory, exportOptions);

                Int32 percent = (Int32)(((Single)i / (Single)fileNames.Count) * 100);

                backgroundWorker.ReportProgress(percent, fileName);

                ++result;
            }

            return result;
        }

        private void applyExportFormatInfo(ModelExporterStatic.ExportFormatInfo exportFormat)
        {
            normalsCheckBox.Checked = exportFormat.CanExportNormals;
            normalsCheckBox.Enabled = exportFormat.CanExportNormals;

            textureCoordinatesCheckBox.Checked = exportFormat.CanExportTextureCoordinates;
            textureCoordinatesCheckBox.Enabled = exportFormat.CanExportTextureCoordinates;
        }

        private void loadModelFormatComboBox()
        {
            modelFormatComboBox.Items.Clear();

            foreach (ModelExporterStatic.ExportFormatInfo exportFormatInfo in ModelExporterStatic.ExportFormatInfos.Values)
            {
                modelFormatComboBox.Items.Add(exportFormatInfo);
            }

            modelFormatComboBox.SelectedIndex = modelFormatComboBox.Items.Count > 0 ? 0 : -1;
        }

        private void loadModelAxesPresetComboBox()
        {
            modelAxesPresetComboBox.Items.Clear();

            foreach (ModelExporterStatic.ModelAxesPreset modelAxesPreset in ModelExporterStatic.ModelAxesPresets)
            {
                modelAxesPresetComboBox.Items.Add(modelAxesPreset);
            }

            modelAxesPresetComboBox.SelectedIndex = modelAxesPresetComboBox.Items.Count > 0 ? 0 : -1;
        }

        private void loadTextureFormatComboBox()
        {
            textureFormatComboBox.Items.Clear();

            foreach (TextureExporter.TextureFormatInfo textureFormat in TextureExporter.TextureFormats)
            {
                textureFormatComboBox.Items.Add(textureFormat);
            }

            textureFormatComboBox.SelectedIndex = textureFormatComboBox.Items.Count > 0 ? 0 : -1;
        }

        private void scaleLinkAxesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            yScaleNumericUpDown.Enabled = !scaleLinkAxesCheckBox.Checked;
            zScaleNumericUpDown.Enabled = !scaleLinkAxesCheckBox.Checked;

            if (scaleLinkAxesCheckBox.Checked)
            {
                yScaleNumericUpDown.Value = xScaleNumericUpDown.Value;
                zScaleNumericUpDown.Value = xScaleNumericUpDown.Value;
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            applyCurrentStateToExportOptions();

            if (exportFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                List<object> argument = new List<object>()
                {
                    exportFolderBrowserDialog.SelectedPath,
                    FileNames,
                    exportOptions
                };

                loadingForm = new GenericLoadingForm();
                loadingForm.Show();

                exportBackgroundWorker.RunWorkerAsync(argument);
            }
        }

        private void formatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modelFormatComboBox.SelectedItem == null)
                return;

            ModelExporterStatic.ExportFormatInfo exportFormatInfo = (ModelExporterStatic.ExportFormatInfo)modelFormatComboBox.SelectedItem;

            applyExportFormatInfo(exportFormatInfo);

        }

        private void ModelExportForm_Load(object sender, EventArgs e)
        {
            exportFolderBrowserDialog.SelectedPath = Application.StartupPath;

            exportBackgroundWorker.WorkerReportsProgress = true;
            exportBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(exportProgressChanged);
            exportBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(exportRunWorkerCompleted);
            exportBackgroundWorker.DoWork += new DoWorkEventHandler(exportDoWork);

            loadModelFormatComboBox();
            loadModelAxesPresetComboBox();
            loadTextureFormatComboBox();

            upAxisComboBox.SelectedIndex = 1;
            leftAxisComboBox.SelectedIndex = 0;

            packageToolTip.SetToolTip(packageCheckBox, "When checked, assets will be exported into their own directory.");
        }

        private void exportTexturesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            textureFormatComboBox.Enabled = texturesCheckBox.Checked;
            exportOptions.Textures = texturesCheckBox.Checked;
        }

        private void normalsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            exportOptions.Normals = normalsCheckBox.Checked;
        }

        private void textureCoordinatesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            exportOptions.TextureCoordinates = textureCoordinatesCheckBox.Checked;
        }

        private void upAxisComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            exportOptions.UpAxis = (Axes)upAxisComboBox.SelectedIndex;
        }

        private void leftAxisComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            exportOptions.LeftAxis = (Axes)leftAxisComboBox.SelectedIndex;
        }

        private void xScaleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (scaleLinkAxesCheckBox.Checked)
            {
                yScaleNumericUpDown.Value = xScaleNumericUpDown.Value;
                zScaleNumericUpDown.Value = xScaleNumericUpDown.Value;
            }

            exportOptions.Scale.X = (Single)xScaleNumericUpDown.Value;
        }

        private void yScaleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            exportOptions.Scale.Y = (Single)yScaleNumericUpDown.Value;
        }

        private void zScaleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            exportOptions.Scale.Z = (Single)zScaleNumericUpDown.Value;
        }

        private void packageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            exportOptions.Package = packageCheckBox.Checked;
        }

        private void modelAxesPresetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModelExporterStatic.ModelAxesPreset modelAxesPreset = ModelExporterStatic.ModelAxesPresets[modelAxesPresetComboBox.SelectedIndex];

            applyModelAxesPreset(modelAxesPreset);
        }

        private void applyModelAxesPreset(ModelExporterStatic.ModelAxesPreset modelAxesPreset)
        {
            leftAxisComboBox.SelectedIndex = (Int32)modelAxesPreset.LeftAxis;
            upAxisComboBox.SelectedIndex = (Int32)modelAxesPreset.UpAxis;
        }

        private void applyCurrentStateToExportOptions()
        {
            exportOptions.LeftAxis = (Axes)leftAxisComboBox.SelectedIndex;
            exportOptions.Normals = normalsCheckBox.Checked;
            exportOptions.Package = packageCheckBox.Checked;
            exportOptions.Scale.X = (Single)xScaleNumericUpDown.Value;
            exportOptions.Scale.Y = (Single)yScaleNumericUpDown.Value;
            exportOptions.Scale.Z = (Single)zScaleNumericUpDown.Value;
            exportOptions.TextureCoordinates = textureCoordinatesCheckBox.Checked;
            exportOptions.Textures = texturesCheckBox.Checked;
            exportOptions.UpAxis = (Axes)upAxisComboBox.SelectedIndex;
            exportOptions.TextureFormat = (TextureExporter.TextureFormatInfo)textureFormatComboBox.SelectedItem;
        }

        private void textureFormatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            exportOptions.TextureFormat = (TextureExporter.TextureFormatInfo)textureFormatComboBox.SelectedItem;
        }
    }
}
