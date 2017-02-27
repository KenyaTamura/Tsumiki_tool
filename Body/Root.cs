using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XML_cs;

namespace Tsumiki_tool.Body {
    static class Root {
        // XMLデータ
        static private string mFilename = null;
        static private Document mXml_data = null;

        // フィールド情報
        static private bool[] mFields = null;    // trueなら埋まっている
        static private Block[] mBlocks = null;
        static private Block mForm_block = null;

        // フィールド操作を行うクラス
        static private Base mBase = new Setting();

        // 新規作成
        static public void New_stage() {
            // 初期値を代入
            string s = Manager.New_stage();
            Manager.Box_file = s;
            mFilename = Manager.Path + s + ".xml";
            mXml_data = new Document();
            mFields = new bool[Manager.Field_X * Manager.Field_Y];
            mForm_block = new Block(0);
        }

        // 保存
        static public void Save() {
            if (mFilename != null) {
                mXml_data.write(mFilename);
            }
        }

        // 編集フォームでクリック
        // 編集フォーム上ピクセル座標
        static public void Click_on_edit(int x, int y) {
            if (mForm_block == null) {
                return;
            }
            int mass_x = x / (Manager.Edit_Width / Manager.Block_width);
            int mass_y = y / (Manager.Edit_Height / Manager.Block_height);
            // ある場所の形を変更
            mForm_block.Change_shape(mass_x, mass_y);
            // 描画
            Redraw_edit();
        }

        // 編集フォームの色替え
        static public void Change_color() {
            if (mForm_block == null) {
                return;
            }
            mForm_block.Change_color();
            Redraw_edit();
        }

        // 形を調べてフォームの再描画
        static private void Redraw_edit() {
            for (int i = 0; i < Manager.Block_height; ++i) {
                for (int j = 0; j < Manager.Block_width; ++j) {
                    if (mForm_block.Get_shape(j, i)) {
                        Manager.Draw_edit(j, i, mForm_block.Get_color);
                    }
                    else {
                        Manager.Draw_edit(j, i, Block.Color.NONE);
                    }
                }
            }
        }

        // フィールドフォームでクリック
        static public void Click_on_field(int x, int y) {
            // Baseに任せる
            mBase.Clicked();
        }

        // フィールドフォームでカーソル移動
        static public void Move_on_field(int x, int y) {

        }

        // モード変更
        static public Base Change_mode{
            set {
                if (mFilename != null) {
                    mBase = value;
                    mBase.Labeling();
                }
            }
        }

    }
}
