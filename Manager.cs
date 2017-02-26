using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tsumiki_tool {
    // Form1で定義した機能を利用
    class Manager {

        static public string path = Form1.path;

        Manager() {

        }

        // コンボボックスの操作
        public static string Box_file {
            // 指定中の要素
            get {
                return Program.Root.Box_file;
            }
            // 末尾に追加
            set {
                Program.Root.Box_file = value;
            }
        }

        // ステージ番号を返す
        public static string new_stage() {
            return Program.Root.new_stage();
        }

    }
}
