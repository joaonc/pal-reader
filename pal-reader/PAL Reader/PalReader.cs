using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace PALReader
{
    /// <summary>
    /// Reads Microsoft .PAL files, which contain palette information.
    /// The format is as follows:
    /// 'RIFF' (4 bytes)
    /// file length - 8 (4 bytes)
    /// 'PAL ' (4 bytes)
    /// 'data' (4 bytes)
    /// data size (filelength - 20) (4 bytes)
    /// version number (always 00 03) (2 bytes)
    /// number of colors in the image (2 bytes)
    /// 
    /// Then colors come in RGBA come in 4 bytes
    /// 
    /// Info taken from http://willperone.net/Code/codereadingpal.php
    /// 
    /// NOTE: Alpha representation in the Color object is inverse of that in PAL, ie
    ///       0 in PAL is 255 in Color.
    ///       This conversion is done in the code.
    /// </summary>
    public class PalReader
    {
        /// <summary>
        /// Version number.
        /// </summary>
        public int version { get; private set; }

        /// <summary>
        /// Number of colors in the palette.
        /// </summary>
        public int colors { get; private set; }

        /// <summary>
        /// Color palette.
        /// </summary>
        public List<Color> palette { get; private set; }

        public PalReader(string fileName)
        {
            // Read the file
            FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read);

            try
            {
                // Get the initial data
                int headerSize = 24;
                byte[] header = new byte[headerSize];
                fs.Read(header, 0, headerSize);

                version = BitConverter.ToInt16(new byte[2] { header[21], header[20] }, 0);
                colors = BitConverter.ToInt16(new byte[2] { header[22], header[23] }, 0);

                palette = new List<Color>();
                byte[] rgbaColor = new byte[4];
                while (fs.Position < fs.Length)
                {
                    fs.Read(rgbaColor, 0, 4);
                    // Alpha is inverted
                    Color color = Color.FromArgb(255 - rgbaColor[3], rgbaColor[0], rgbaColor[1], rgbaColor[2]);

                    palette.Add(color);
                }
            }
            finally
            {
                fs.Close();
            }
        }

        public string GetPalette()
        {
            StringBuilder sb = new StringBuilder();
            
            foreach(Color color in palette)
            {
                sb.AppendLine(string.Format("{0:000}, {1:000}, {2:000}, {3:000}", color.R, color.G, color.B, color.A));
            }

            return sb.ToString();
        }
    }
}
