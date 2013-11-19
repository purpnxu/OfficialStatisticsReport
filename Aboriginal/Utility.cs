using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Aspose.Cells;
using System.IO;
using System.Windows.Forms;
using FISCA.Presentation.Controls;

namespace Aboriginal
{
    class Utility
    {
        /// <summary>
        /// 匯出至 Excel 檔案
        /// </summary>
        /// <param name="inputReportName"></param>
        /// <param name="dt"></param>
        /// <param name="inputXls"></param>       
                                         //傳入("原住民族別統計報表", _dt, wb)三個參數
        public static void CompletedXls(string inputReportName, DataTable dt, Workbook inputXls)  //使用到DataTable、Workbook，需引用using System.Data;using Aspose.Cells;二個
        {
            string reportName = inputReportName;

            string path = Path.Combine(Application.StartupPath, "Reports");//Path與IO有關，需using System.IO; Application需 using System.Windows.Forms; 將應用程式可執行路徑與"Reports"合併成一個路徑
            if (!Directory.Exists(path))  //判斷指定目錄是否在現在路徑
                Directory.CreateDirectory(path);  //按指定目錄來建目錄
            path = Path.Combine(path, reportName + ".xls");  //將列印檔名與副檔名合併成一個路徑

            Workbook wb = inputXls; //設定變數 存放傳入的參數，統一名稱

            wb.Worksheets[0].Cells.ImportDataTable(dt, true, "A1"); //將dt資料表匯入工作表中，由A1開始
            wb.Worksheets[0].Name = inputReportName; //將原住民族別統計報表名稱，存至工作表名稱
            wb.Worksheets[0].AutoFitColumns(); //自動加欄位
            if (File.Exists(path))  //假如檔案存在
            {
                int i = 1;
                while (true)
                {
                    string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);  //抓存在檔的現在路徑
                    if (!File.Exists(newPath))  //假如路徑不存在 ，就將新的路徑存下來
                    {
                        path = newPath;
                        break;
                    }
                }
            }

            try
            {
                wb.Save(path, Aspose.Cells.FileFormatType.Excel2003);  //工作表以excel2003型式存放
                System.Diagnostics.Process.Start(path);     //啟動指定文件
            }
            catch
            {
                SaveFileDialog sd = new SaveFileDialog();  //建立一個檔案儲存位置sd的提示
                sd.Title = "另存新檔";             //標題
                sd.FileName = reportName + ".xls";    //列印檔名
                sd.Filter = "XLS檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";   //設定副檔名字串
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd.FileName, Aspose.Cells.FileFormatType.Excel2003);

                    }
                    catch
                    {
                        MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);  //MsgBox需using FISCA.Presentation.Controls;
                        return;
                    }
                }
            }
        }
    }
}
