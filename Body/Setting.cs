using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tsumiki_tool.Body {
    class Setting : Base{
        public Setting() {

        }

        // クリック時の操作
        public void Clicked(MouseEventArgs e) {
            // フィールドに追加
            if(e.Button == MouseButtons.Left) {
                Addition();
            }
            // 回転
            else if(e.Button == MouseButtons.Right) {
                Root.Form_block.Lotation();
            }
        }

        // ブロックをフィールドフォームで表示
        public void Moved(int x, int y) {            
            Block b = Root.Form_block;
            b.X = x;
            b.Y = y;
            bool[] drawn = new bool[Manager.Block_num];
            for (int num = 0; num < Manager.Block_num; ++num) {
                if (b.Get_shape(num)) {
                    // 空白なら上書き
                    if (Root.Get_field(b.Get_X(num), b.Get_Y(num)) == Block.Color.NONE) {
                        Root.Set_field(b.Get_X(num), b.Get_Y(num), b.Get_color);
                        drawn[num] = true;  // 上書きした
                    }
                }
            }
            Root.Redraw_field();
            for (int num = 0; num < Manager.Block_num; ++num) {
                if (drawn[num]) {
                    // 本当は無いから消しておく
                    Root.Set_field(b.Get_X(num), b.Get_Y(num), Block.Color.NONE);
                    int c = Root.Form_block.Get_X(num);
                }
            }

        }

        public void Labeling() {
            Manager.Change_label_state("状態： 編集");
        }

        // フィールドに編集のブロックを追加
        private void Addition() {
            // フィールド情報とブロック位置の確認
            Block b = Root.Form_block;
            for(int num = 0; num < Manager.Block_num; ++num) {
                if (b.Get_shape(num)) {
                    int x = b.Get_X(num);
                    int y = b.Get_Y(num);
                    // 範囲外
                    if (x < 0 || x >= Manager.Field_X ||
                        y < 0 || y >= Manager.Field_Y) {
                        return;
                    }
                    // 既にある
                    if(Root.Get_field(x,y) != Block.Color.NONE) {
                        return;
                    }
                }
            }
            // ブロック群に追加
            if (Root.Blocks == null) {
                Root.Blocks = new Block[1];
            }
            else {
                Array.Resize<Block>(ref Root.Blocks, Root.Blocks.Length + 1);
            }
            Root.Blocks[Root.Blocks.Length - 1] = b;
            // フィールド情報の更新
            for (int num = 0; num < Manager.Block_num; ++num) {
                if (b.Get_shape(num)) {
                    Root.Set_field(b.Get_X(num), b.Get_Y(num), b.Get_color);
                }
            }
            // 編集フォームのリセット          
            Root.Form_block = new Block(0);
            Root.Redraw_edit();
            Root.Redraw_field();
        }
    }
}
