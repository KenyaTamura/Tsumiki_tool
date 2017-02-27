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
        static Block[] mBlocks = null;
        static Block mForm_block = null;

        // ファイル名の操作
        static public string Filename {
            get {
                return mFilename;
            }
            set {
                mFilename = value;
            }
        }

        // 新規作成
        static public void New_stage() {
            // 初期値を代入
            mXml_data = new Document();
            mFields = new bool[Manager.Field_X * Manager.Field_Y];
            mForm_block = new Block(0);
        }

        // 保存
        static public void Save() {
            mXml_data.write(mFilename);
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
            // 形を調べて再描画
            for(int i = 0; i < Manager.Block_height; ++i) {
                for(int j = 0; j < Manager.Block_width; ++j) {
                    if (mForm_block.Get_shape(j, i)) {
                        Manager.Draw_edit(j, i, mForm_block.Get_color);
                    }
                    else {
                        Manager.Draw_edit(j, i, Block.Color.NONE);
                    }                           
                }
            }
        }
    }
}
