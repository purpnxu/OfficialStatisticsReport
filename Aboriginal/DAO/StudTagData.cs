using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aboriginal.DAO
{
    /// <summary>
    /// 學生類別資料
    /// </summary>
    public class StudTagData
    {
        public string StudentID { get; set; }

        /// <summary>
        /// 年級
        /// </summary>
        public string GradeYear { get; set; }

        /// <summary>
        /// 學生性別
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 學生類別
        /// </summary>
        public Dictionary<string, List<string>> TagDict = new Dictionary<string, List<string>>();

        /// <summary>
        /// 族別代號
        /// </summary>
        public string tag_id { get; set; }

        /// <summary>
        /// 類別字首
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 類別名稱
        /// </summary>
        public string name { get; set; }

        public int 學生數_總計 { get; set; }
        public int 學生數_計 { get; set; }
        public int 學生數_男 { get; set; }
        public int 學生數_女 { get; set; }

        public int 學生數_一年級_男 { get; set; }
        public int 學生數_一年級_女 { get; set; }

        public int 學生數_二年級_男 { get; set; }
        public int 學生數_二年級_女 { get; set; }

        public int 學生數_三年級_男 { get; set; }
        public int 學生數_三年級_女 { get; set; }

        public int 學生數_四年級_男 { get; set; }
        public int 學生數_四年級_女 { get; set; }

        public int 學生數_延修生_男 { get; set; }
        public int 學生數_延修生_女 { get; set; }

        public int 上學年度畢業生數_男 { get; set; }
        public int 上學年度畢業生數_女 { get; set; }


    }
}
