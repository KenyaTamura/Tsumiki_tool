﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tsumiki_tool {
    class Component {
        // 新規作成
        static public void B_new(object sender, EventArgs e) {
            string s = Manager.New_stage();
            Manager.Box_file = s;
            Body.Root.Filename = Manager.Path + s + ".xml";
            Body.Root.New_stage();
        }

        // 保存
        static public void B_save(object sender, EventArgs e) {
            Body.Root.Save();
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

        // 編集フォームのマウス操作
        static public void M_edit(object sender, MouseEventArgs e) {
            // 左クリックでブロックを付けたり消したり
            if(e.Button == MouseButtons.Left) {
                // Rootに渡してそっちで処理
                Body.Root.Click_on_edit(e.X, e.Y);
            }
        }

        // 本体フォームのマウス操作
        static public void M_body(object sender, MouseEventArgs e) {

        }
    }
}
