using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tsumiki_tool.Body {
    class Del : Base{
        public Del() {

        }

        public void Clicked(MouseEventArgs e) {
            // 何もないなら終了
            if(Root.Blocks.Count == 0) {
                return;
            }
            if (e.Button == MouseButtons.Left) {
                // マス座標に変換
                int x = e.X / (Manager.Field_width / Manager.Field_X);
                int y = e.Y / (Manager.Field_height / Manager.Field_Y);
                Removing(x, y);
            }
        }

        // 何もしない
        public void Moved(int x, int y) {

        }

        public void Labeling() {
            Manager.Change_label_state("状態： 削除");
        }

        private void Removing(int x, int y) {
            // 空白なら終了
            if (Root.Get_field(x, y) == Block.Color.NONE) {
                return;
            }
            bool found = false;
            foreach (Block b in Root.Blocks) {
                if (x < b.X || x > b.X + Manager.Block_width ||
                   y < b.Y && y > b.Y + Manager.Block_height) {
                    // このブロックではない
                    continue;
                }
                for (int num = 0; num < Manager.Block_num; ++num) {
                    if (b.Get_shape(num)) {
                        if (x == b.Get_X(num) && y == b.Get_Y(num)) {
                            // 発見
                            found = true;
                            break;
                        }
                    }
                }
                if (found) {
                    // フィールドの更新
                    for (int num = 0; num < Manager.Block_num; ++num) {
                        if (b.Get_shape(num)) {
                            Root.Set_field(b.Get_X(num), b.Get_Y(num), Block.Color.NONE);
                        }
                    }
                    // 削除
                    Root.Blocks.Remove(b);
                    // 再描画
                    Root.Redraw_field();
                    Root.Redraw_order();
                    break;
                }
            }                 
        }
    }
}
