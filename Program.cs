using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tsumiki_tool
{
    class Program
    {
        static Form1 root;
        // 開始
        [STAThread]
        static void Main() {
            root = new Form1();
            Application.Run(root);
        }

        public static Form1 Root {
            get {
                return root;
            }
        }
    }
}
