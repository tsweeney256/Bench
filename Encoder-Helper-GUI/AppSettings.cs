﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Encoder_Helper_GUI
{
    [Serializable]
    class AppSettings
    {
        public string[] x264Args { get; set; }
        public int[] encoder { get; set; }
        public string videoTrackName { get; set; }
        public string videoLanguageCode { get; set; }
        public bool avisynthTemplate { get; set; }
        public decimal[] quality { get; set; }
        public string[] audioTrackName { get; set; }
        public string[] audioLanguageCode { get; set; }
        public string x264_x86_8bit_location { get; set; }
        public string x264_x86_10bit_location { get; set; }
        public string x264_x64_8bit_location { get; set; }
        public string x264_x64_10bit_location { get; set; }
        public string MKVMergeLocation { get; set; }
        public string NeroAACLocation { get; set; }
        public string BePipeLocation { get; set; }

        private const string settingsFile = "settings.bin";

        public AppSettings()
        {
            AppSettings settings;
            if (!File.Exists(settingsFile))
            {
                initialize();
            }
            try
            {
                using (Stream stream = File.Open(settingsFile, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    settings = (AppSettings)bformatter.Deserialize(stream);
                }
            }
            catch
            {
                var result = MessageBox.Show("Settings file contains errors. A new settings file will be created and the old one will be backed up.",
                    "Error", MessageBoxButtons.OK);
                File.Move(settingsFile, settingsFile + ".bak");
                initialize();
                using (Stream stream = File.Open(settingsFile, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    settings = (AppSettings)bformatter.Deserialize(stream);
                }
            }
            x264Args = settings.x264Args;
            encoder = settings.encoder;
            videoTrackName = settings.videoTrackName;
            videoLanguageCode = settings.videoLanguageCode;
            avisynthTemplate = settings.avisynthTemplate;
            quality = settings.quality;
            audioTrackName = settings.audioTrackName;
            audioLanguageCode = settings.audioLanguageCode;
            x264_x86_8bit_location = settings.x264_x86_8bit_location;
            x264_x86_10bit_location = settings.x264_x86_10bit_location;
            x264_x64_8bit_location = settings.x264_x64_8bit_location;
            x264_x64_10bit_location = settings.x264_x64_10bit_location;
            MKVMergeLocation = settings.MKVMergeLocation;
            NeroAACLocation = settings.NeroAACLocation;
            BePipeLocation = settings.BePipeLocation;
        }

        public void save()
        {
            using(Stream stream = File.Open(settingsFile, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, this);
            }
        }

        private void initialize()
        {

            x264Args = new string[1];
            encoder = new int[1];
            quality = new decimal[1];
            audioTrackName = new string[1];
            audioLanguageCode = new string[1];
            save();
        }
    }
}