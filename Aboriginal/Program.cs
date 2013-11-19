using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Permission;

namespace Aboriginal
{
    public class Program
    {
        [FISCA.MainMethod]
        public static void Main()//1.主程式
        {

            string regCode = "原住民族別統計";//做為權限名稱
            FISCA.Presentation.RibbonBarItem report = FISCA.Presentation.MotherForm.RibbonBarItems["教務作業", "公務統計"]; //設定功能列，相同時~會在原位置加入，不同時~會新增一個
            report["報表"].Image = Properties.Resources.Report;    //設定選項圖片
            report["報表"].Size = FISCA.Presentation.RibbonBarButton.MenuButtonSize.Large;  //設定按鈕大小
            report["報表"]["原住民族別統計"].Enable = UserAcl.Current[regCode].Executable;
            report["報表"]["原住民族別統計"].Click += delegate  //當按下按鈕時，觸發事件
            {
                AboriginalForm f = new AboriginalForm();   //生成表單物件
                f.ShowDialog();   //顯示表單，執行至此，會呼叫AboriginalForm ==>2.
            };
            // 權限設定
            Catalog catalog1a = RoleAclSource.Instance["教務作業"]["功能按鈕"];
            catalog1a.Add(new RibbonFeature(regCode, "原住民族別統計"));
        }
    }
}
