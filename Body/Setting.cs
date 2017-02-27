using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsumiki_tool.Body {
    class Setting : Base{
        public Setting() {

        }

        public void Clicked() {

        }

        // ブロックをフィールドフォームで表示
        public void Moved(int x, int y, Block b) {
            b.X = x;
            b.Y = y;
            for(int num = 0; num < Manager.Block_num; ++num) {
                if (b.Get_shape(num)) {
                    if (Root.Get_field(b.Get_X(num), b.Get_Y(num)) == Block.Color.NONE) {
                        Root.Set_field(b.Get_X(num), b.Get_Y(num), b.Get_color);
                    }
                }
            }
            Root.Redraw_field();
            for (int num = 0; num < Manager.Block_num; ++num) {
                if (b.Get_shape(num)) {
                    Root.Set_field(b.Get_X(num), b.Get_Y(num), Block.Color.NONE);
                }
            }

        }

        public void Labeling() {
            Manager.Change_label_state("状態： 編集");
        }
    }
}
