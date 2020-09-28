﻿using ImageEditorLib;
using System;
using System.Windows.Forms;

namespace WinFormsPhotoEditor
{
    public partial class MainForm : Form
    {
        private ImageEditor _imageEditor;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MenuItemOpenImage_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter =
                @"Image files (*.jpg, *.jpeg, *.jpe, *.png) | *.jpg; *.jpeg; *.jpe; *.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string pathToSelectedFile = openFileDialog.FileName;

                _imageEditor = new ImageEditor(pathToSelectedFile);
                var image = _imageEditor.Image;

                if (image.Width < PbImage.Width && image.Height < PbImage.Height)
                {
                    PbImage.SizeMode = PictureBoxSizeMode.CenterImage;
                }
                else
                {
                    PbImage.SizeMode = PictureBoxSizeMode.Zoom;
                }

                PbImage.Image = image;
            }
        }

        #region Image rotation

        private void BtnRotateTo90DegreesRight_Click(object sender, EventArgs e)
        {
            PbImage.Image = _imageEditor?.Rotate(90);
        }

        private void BtnRotateTo90DegreesLeft_Click(object sender, EventArgs e)
        {
            PbImage.Image = _imageEditor?.Rotate(-90);
        }

        private void BtnRotateToSpecDegreesRight_Click(object sender, EventArgs e)
        {
            PbImage.Image = _imageEditor?.RotateManyTimes((float)NudDegreeToRotate.Value);
        }


        #endregion

        private void TbHue_Scroll(object sender, EventArgs e)
        {
            PbImage.Image = _imageEditor?.ChangeHue(TbHue.Value);
        }

        private void TbBrightness_Scroll(object sender, EventArgs e)
        {
            PbImage.Image = _imageEditor?.ChangeBrightness(TbBrightness.Value);
        }

        private void ApplyChanges_Click(object sender, EventArgs e)
        {
            _imageEditor?.SaveToTempImage();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _imageEditor?.Dispose();
            _imageEditor = null;
        }

    }
}
