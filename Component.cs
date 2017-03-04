using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tsumiki_tool {
    class Component {
        // 新規作成
        static public void B_new(object sender, EventArgs e) {
            // Root側で処理
            Body.Root.New_stage();
        }

        // 保存
        static public void B_save(object sender, EventArgs e) {
            Body.Root.Save();
        }
        
        // 読込み
        static public void B_load(object sender, EventArgs e) {
            Body.Root.Load();
        }

        /*
        // 設置
        static public void B_set(object sender, EventArgs e) {

        }
        */

        // 色替え
        static public void B_color(object sender, EventArgs e) {
            // Root側で処理
            Body.Root.Change_color();
        }

        // 編集
        static public void B_edit(object sender, EventArgs e) {
            // 編集モードに変更
            Body.Root.Change_mode = new Body.Setting();
        }

        // 削除
        static public void B_del(object sender, EventArgs e) {
            // 削除モードに変更
            Body.Root.Change_mode = new Body.Del();
        }

        // 順序
        static public void B_order(object sender, EventArgs e) {
            // 順序モードに変更
            Body.Root.Change_mode = new Body.Order();
        }

        // 編集フォームのマウス操作
        static public void M_edit(object sender, MouseEventArgs e) {
            // 左クリックでブロックを付けたり消したり
            if (e.Button == MouseButtons.Left) {
                // Rootに渡してそっちで処理
                Body.Root.Click_on_edit(e.X, e.Y);
            }
        }

        // フィールドフォームのマウス操作
        // フィールドフォームでクリック
        static public void M_field_click(object sender, MouseEventArgs e) {
            // Rootでモードごとの処理
            Body.Root.Click_on_field(e);
        }

        // マウスカーソルがフィールドフォームを移動
        static public void M_field_move(object sender, MouseEventArgs e) {
            // Rootでモードごとの処理
            Body.Root.Move_on_field(e.X, e.Y);
        }

        // フィールドフォームをマウスが離れる
        static public void M_field_leave(object sender, EventArgs e) {
            // 再描画
            Body.Root.Redraw_field();
        }

    }
}
