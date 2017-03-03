using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tsumiki_tool.Body {
    class Order : Base{
        public void Clicked(MouseEventArgs e) {
            // 何もないなら終了
            if (Root.Blocks.Count == 0) {
                return;
            }
            Swap(e);
        }

        // 何もしない
        public void Moved(int x, int y) {

        }

        public void Labeling() {
            Manager.Change_label_state("状態： 順序");
        }

        private void Swap(MouseEventArgs e) {
            // マス座標に変換
            int x = e.X / (Manager.Field_width / Manager.Field_X);
            int y = e.Y / (Manager.Field_height / Manager.Field_Y);
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
                    // 入れ替え
                    int i = Root.Blocks.IndexOf(b);
                    // 番号を上げる
                    if (e.Button == MouseButtons.Left) {
                        if(i == Root.Blocks.Count - 1) {
                            return;
                        }
                        Block tmp = b;
                        Root.Blocks[i] = Root.Blocks[i + 1];
                        Root.Blocks[i + 1] = b;
                    }
                    // 番号を下げる
                    else if (e.Button == MouseButtons.Right) {
                        if(i == 0) {
                            return;
                        }
                        Block tmp = b;
                        Root.Blocks[i] = Root.Blocks[i - 1];
                        Root.Blocks[i - 1] = b;
                    }
                    // 再描画
                    Root.Redraw_order();
                    break;
                }
            }
        }
    }
}
