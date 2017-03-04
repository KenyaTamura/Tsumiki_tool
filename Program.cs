using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tsumiki_tool
{
    class Program{
        static Form1 mRoot;
        // 開始
        [STAThread]
        static void Main() {
            mRoot = new Form1();
            Application.Run(mRoot);
        }

        // 動かしてるフォーム
        static public Form1 Root {
            get {
                return mRoot;
            }
        }
    }
}
