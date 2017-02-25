using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsumiki_tool {
    class Component {
        // 新規作成
        static public void B_new(object sender, EventArgs e) {
            string s = Manager.new_filename();
            Manager.Box_file = s;
        }

        // 保存
        static public void B_save(object sender, EventArgs e) {

        }
        
        // 読込み
        static public void B_load(object sender, EventArgs e) {

        }

        // 設置
        static public void B_set(object sender, EventArgs e) {

        }

        // 色替え
        static public void B_color(object sender, EventArgs e) {

        }

        // 編集
        static public void B_edit(object sender, EventArgs e) {

        }

        // 削除
        static public void B_del(object sender, EventArgs e) {

        }

        // 順序
        static public void B_order(object sender, EventArgs e) {

        }
    }
}
