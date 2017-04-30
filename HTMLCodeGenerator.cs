using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLTableGenerator
{
    public class HTMLCodeBuilder
    {
        public string SourceFolder { get; set; }
        public string Template { get; internal set; }
        public int ImageWidth { get; set; }
        public int ColumnsNumber { get; set; }
        public string ImageURLRoot { get; set; }
        public bool UsingTable { get; set; }
        public string ErrorMessages { get; set; }

        // isTable
        // ColumnsNumber

        internal string Builder()
        {
            string result = "";
            string[] allFiles = null;

            try
            {
                allFiles = Directory.GetFiles(SourceFolder);

            } catch (Exception e)
            {
                result = "Source folder not valid";
                return result;
            }

            if (UsingTable)
            {
                result += "<table style=\"table-layout: fixed;\"><colgroup><col width=\"50%\"></col><col width=\"50%\"></col></colgroup><tbody>";
                result += "\n<tr>\n";
            }

            int count = 0;
            int columnIndex = 1;

            for (int i = 0; i < allFiles.Length; i++)
            {
                count++;
                if (columnIndex % ColumnsNumber == 1)
                {
                    result += "\n</tr>\n<tr>\n";
                }
                columnIndex++;

                string currentTDCell = Template;
                string imgPath = allFiles[i];
                string imgNameExt = imgPath.Substring(imgPath.LastIndexOf("\\") + 1);
                string imgName = imgNameExt.Substring(0, imgNameExt.LastIndexOf("."));

                currentTDCell = currentTDCell.Replace("__IMG_NAME__", imgName);
                currentTDCell = currentTDCell.Replace("__SIZE__", ImageWidth + "");
                currentTDCell = currentTDCell.Replace("__LINK__", ImageURLRoot + imgNameExt);
                currentTDCell = currentTDCell.Replace("__COUNT__", count + "");

                result += currentTDCell;

            }

            if (UsingTable)
            { 
                result += "\n</tbody></table>";
            }
            return result;
        }
    }
}
