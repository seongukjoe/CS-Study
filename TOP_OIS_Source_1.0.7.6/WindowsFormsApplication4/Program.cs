using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace XModule
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // Unique한 뮤텍스명을 위해 주로 GUID를 사용한다.
                string mtxName = "SIC_M_CG";

                // 뮤텍스명으로 뮤텍스 객체 생성 
                // 만약 뮤텍스를 얻으면, createdNew = true
                bool createdNew;
                Mutex mtx = new Mutex(true, mtxName, out createdNew);

                // 뮤텍스를 얻지 못하면 에러
                if (!createdNew)
                {
                    MessageBox.Show("에러: 프로그램 이미 실행중");
                    return;
                }

                
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain());
            }
            catch(Exception ex)
            {

            }
        }
    }
}
