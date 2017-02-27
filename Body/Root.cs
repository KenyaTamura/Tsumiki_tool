﻿using System;
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
        static private Block.Color[] mFields = null;    // NONE以外なら埋まっている
        static private Block.Color[] mFields_prev = null;    // 前回の描画
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
            mFields = Enumerable.Repeat<Block.Color>(Block.Color.NONE, Manager.Field_X * Manager.Field_Y).ToArray();
            mFields_prev = Enumerable.Repeat<Block.Color>(Block.Color.NONE, Manager.Field_X * Manager.Field_Y).ToArray();
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
            int mass_x = x / (Manager.Edit_width / Manager.Block_width);
            int mass_y = y / (Manager.Edit_height / Manager.Block_height);
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

        // フィールドを調べて再描画
        static public void Redraw_field() {
            if(mFields == null) {
                return;
            }
            // フィールド情報を基に描画
            // 前回から変化があったら再描画
            for(int y = 0; y < Manager.Field_Y; ++y) {
                for(int x = 0; x < Manager.Field_X; ++x) {
                    int point = x + y * Manager.Field_X;
                    if (mFields[point] != mFields_prev[point]) {
                        Manager.Draw_field(x, y, mFields[point]);
                        mFields_prev[point] = mFields[point];
                    }
                }
            }
        }

        // フィールドフォームでクリック
        static public void Click_on_field(System.Windows.Forms.MouseEventArgs e) {
            // Baseに任せる
            mBase.Clicked();
        }

        // フィールドフォームでカーソル移動
        static public void Move_on_field(int x, int y) {
            // マス座標に変換
            if (mForm_block == null) {
                return;
            }
            int mass_x = x / (Manager.Field_width / Manager.Field_X);
            int mass_y = y / (Manager.Field_height / Manager.Field_Y);
            // Baseに任せる
            mBase.Moved(mass_x, mass_y, mForm_block);
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

        // フィールドに色をセット
        static public void Set_field(int x, int y, Block.Color c) {
            if (x < 0 || x >= Manager.Field_X || y < 0 || y >= Manager.Field_Y) {
                return;
            }
            mFields[x + y * Manager.Field_X] = c;
        }

        // フィールドの色を獲得
        static public Block.Color Get_field(int x, int y) {
            if (x < 0 || x >= Manager.Field_X || y < 0 || y >= Manager.Field_Y) {
                return Block.Color.NONE;
            }
            return mFields[x + y * Manager.Field_X];
        }

    }
}
