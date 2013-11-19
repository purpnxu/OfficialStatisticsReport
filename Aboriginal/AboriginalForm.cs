using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using K12.Data;
using SHSchool.Data;
using Aspose.Cells;

namespace Aboriginal
{
    public partial class AboriginalForm : BaseForm
    {
        BackgroundWorker _bgWorker; //建立個別執行緒背景工作
        DataTable _dt;      //建立資料表
        public AboriginalForm()   //2.由主程式呼叫，執行AboriginalForm視窗
        {
            InitializeComponent();
            _bgWorker = new BackgroundWorker();//生成背景工作物件
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork); //呼叫背景工作方法 ，將自訂方法代入 =>3
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
            //lvData.Items.Clear();
        }

        private void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnPrint.Enabled = true;
        }

        private void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 讀取資料，由在DAO資料夾QueryData類別 GetStudentTagData()這個方法
            _dt = DAO.QueryData.GetStudentTagData();
        }

        private void AboriginalForm_Load(object sender, EventArgs e)
        {
            btnPrint.Enabled = false;
            _bgWorker.RunWorkerAsync();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ////移除不必要欄位(如果需要重覆使用dt，就需要將用不到的欄位移除)
            //List<string> rmStringList = new List<string>();
            //foreach (DataColumn dc in _dt.Columns)
            //    rmStringList.Add(dc.Caption);

            ////移除沒選
            //foreach (string str in rmStringList)//|| !(str == "性別") || !(str == "類別字首") || !(str == "類別名稱")
            //    if ((str == "id") || (str == "status") || (str == "gender") || (str == "class_name") || (str == "grade_year") || (str == "dept_name") || (str == "prefix") || (str == "name") || (str == "tag_id"))
            //    _dt.Columns.Remove(str);
            
            Workbook wb = new Workbook();     //建立一個workbook物件，需using Aspose.Cells; 加入參考
            Utility.CompletedXls("原住民族別統計報表", _dt, wb);  //使用Utility類別(內部為excel輸出)==>至Utility類別執行
            btnPrint.Enabled = true;

            
        }

        

        /// <summary>
        /// 取得學生類別字典
        /// </summary>
        /// <returns></returns>
        //private Dictionary<string, StudentTagRecord> GetStudentTag()
        //{
        //    Dictionary<string, StudentTagRecord> dic = new Dictionary<string, StudentTagRecord>();//生成學生類別字典物件
        //    foreach (StudentTagRecord stag in StudentTag.SelectAll())//利用foreach將所有類別，一筆一筆取出，存至stag
        //    {
        //        //判斷條件，假如不包含學生類別ID，就存到字典中
        //        if (!dic.ContainsKey(stag.ID))
        //        {
        //            dic.Add(stag.ID, stag);//(key-族別ID,value-族別)
        //        }
        //    }
        //    return dic;
        //}

        
        /// <summary>
        /// 取得科別字典
        /// </summary>
        
        //private Dictionary<string, SHDepartmentRecord> GetDepartment()
        //{
        //    Dictionary<string, SHDepartmentRecord> dic = new Dictionary<string, SHDepartmentRecord>(); //生成科別字典物件
        //    foreach (SHDepartmentRecord depa in SHDepartment.SelectAll())//利用foreach將所有科別，一筆一筆取出，存至depa
        //    {
        //        //判斷條件，假如不包含科別ID，就存到字典中
        //        if (!dic.ContainsKey(depa.ID)) 
        //        {
        //            dic.Add(depa.ID,depa);//(key-科別ID,value-科別)
        //        }
        //    }
        //    return dic;
        //}

        //只需打三個/ 就會出現以下可以說明
        /// <summary>
        /// 取得全校班級字典
        /// </summary>
        /// <returns></returns>
        //private Dictionary<string, SHClassRecord> GetClassDic() //自訂方法
        //{
        //    Dictionary<string, SHClassRecord> dic = new Dictionary<string,SHClassRecord>();//生成dic字典物件
        //    foreach (SHClassRecord _class in Class.SelectAll()) //利用foreach將所有班級，一筆一筆取出
        //    { 
        //        //條件判斷，如果字典中沒有這個班級ID，就存到字典中
        //        if (!dic.ContainsKey(_class.ID))
        //        {
        //            dic.Add(_class.ID,_class); //(key-班級ID,value-班級)
        //        }
        //    }
        //    return dic;
        //}
        /// <summary>
        /// 取得一般生與延修生
        /// </summary>
        /// <returns></returns>
        //private List<SHStudentRecord> GetAllStudent()  //自訂方法
        //{
        //    List<SHStudentRecord> list = new List<SHStudentRecord>(); //生成一個空的list

        //    foreach (SHStudentRecord stud in Student.SelectAll())   //利用foreach將所有學生資料 一筆一筆取出放至stud
        //    {
        //        //判斷條件，符合一般生跟延修生，就存至list中，就是需要的
        //        if (stud.Status == SHStudentRecord.StudentStatus.一般 || stud.Status == SHStudentRecord.StudentStatus.延修)
        //        { 
        //            list.Add(stud);
        //        }
        //    }
        //    return list; //將list回傳給呼叫此方法的StudentList
        //}
    }
}
