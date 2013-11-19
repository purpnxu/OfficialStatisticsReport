using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FISCA.Data;

namespace Aboriginal.DAO
{
    class QueryData
    {
        
        public static DataTable GetStudentTagData()
        {
            DataTable dt = new DataTable();   //生成DataTable物件 dt，需using System.Data;
            DataTable dt2 = new DataTable();    //生成DataTable物件 dt2，需using System.Data，為了存放需要的資料;
            QueryHelper qh = new QueryHelper();  //生成資料查詢引擎qh，需using FISCA.Data;
            dt = qh.Select("select student.id,student.status,student.gender,class_name,class.grade_year,dept.name as dept_name,tag.prefix,tag.name,tag.id as tag_id from student left join class on student.ref_class_id=class.id left join dept on class.ref_dept_id=dept.id left join tag_student on student.id=ref_student_id left join tag on tag_student.ref_tag_id=tag.id where prefix='原住民生' order by grade_year,tag.prefix,tag.name");

            dt2.Columns.Add("代碼");
            dt2.Columns.Add("類別字首");
            dt2.Columns.Add("類別名稱");
            dt2.Columns.Add("年級");  //新增dt2欄位
            dt2.Columns.Add("性別");
            
            
            

            //整理資料   ==>會加入一個StudTagData的類別
            Dictionary<string, StudTagData> studTagDic = new Dictionary<string, StudTagData>(); //生成類別字典物件studTagDic
            foreach (DataRow dRow in dt.Rows)  //取得dt.Rows資料表合集，一筆一筆取出，存至dRow(以學生id為主)
            {
                //DataRow dr = dt.NewRow();  //以資料表相同結構生成dr物件

                string id = dRow["id"].ToString();   //將dRow的 id 字串 存至id(學生id)

                if (!studTagDic.ContainsKey(id))    //假如不包含這個學生
                {
                    
                    studTagDic.Add(id, new StudTagData());  //加入字典(key-學生id,value-學生類別物件)
                }
                studTagDic[id].tag_id = dRow["tag_id"].ToString();
                studTagDic[id].Prefix = dRow["Prefix"].ToString();
                studTagDic[id].name = dRow["name"].ToString();
                studTagDic[id].GradeYear = dRow["grade_year"].ToString();
                studTagDic[id].Gender = dRow["Gender"].ToString();
                
                
            }
            // 處理DataTable 新增欄位
            List<string> addColumnsList = new List<string>();
            foreach (StudTagData std in studTagDic.Values)
            {
                foreach (string name in std.TagDict.Keys)
                    if (!addColumnsList.Contains(name))
                        addColumnsList.Add(name);
            }
            addColumnsList.Sort();
            foreach (string name in addColumnsList)
                dt.Columns.Add(name);

            dt.Clear();
            

            // 資料填入 DataTable
            foreach (StudTagData std in studTagDic.Values)  //利用foreach ，以字典中的值(學生類別)當集合，一筆一筆取出放至std
            {
                DataRow dr = dt2.NewRow();  //以資料表dt2相同結構生成dr物件
                int count_boy = 0;
                int count_girl = 0;
                //dr = std;

                switch (std.tag_id) //將DB的代碼轉成表格所需代碼。統計阿美族~男女總計、一年級、二年級、三年級、四年級、延修生
                { 
                    case "759":
                        std.tag_id = "21";
                        break;
                    case "822":
                        std.tag_id = "22";
                        break;
                    case "823":
                        std.tag_id = "23";
                        break;
                    case "819":
                        std.tag_id = "24";
                        break;
                    case "824":
                        std.tag_id = "28";
                        break;
                    default:
                        break; 
                }
                dr["代碼"] = std.tag_id;
                dr["類別字首"] = std.Prefix;
                dr["類別名稱"] = std.name;
                dr["年級"] = std.GradeYear; //將dt1的欄位值存至dt2
                
                if (std.Gender == "1")   //將DB的男女數字 轉成中文
                {
                    std.Gender = "男";
                    count_boy++;
                }
                else
                {
                    std.Gender = "女";
                    count_girl++;
                }
                dr["性別"] = std.Gender;
                
                
                
                    dt2.Rows.Add(dr); //將dr 的row加至資料表dt的row 
                
            }

            return dt2;
        }
    }
}
