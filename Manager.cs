using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tsumiki_tool {
    // Form1で定義した機能を利用
    class Manager {
        // ブロック情報の定数
        static int BLOCK_NUM = 8;
        static int BLOCK_HEIGHT = 2;
        static int BLOCK_WIDTH = 4;
        // フィールド情報の定数
        static int FIELD_X = 10;
        static int FIELD_Y = 20;
        // パス
        static string PATH = "Data\\";

        Manager() {

        }

        // コンボボックスの操作
        static public string Box_file {
            // 指定中の要素
            get {
                return Program.Root.Box_file;
            }
            // 末尾に追加
            set {
                Program.Root.Box_file = value;
            }
        }

        // ステージ番号を返す
        static public string New_stage() {
            return Program.Root.New_stage();
        }

        // パス
        static public string Path {
            get {
                return PATH;
            }
        }

        // 1ブロック数
        static public int Block_num {
            get {
                return BLOCK_NUM;
            }
        }

        // 1ブロックの高さ
        static public int Block_height {
            get {
                return BLOCK_HEIGHT;
            }
        }

        // 1ブロックの幅
        static public int Block_width {
            get {
                return BLOCK_WIDTH;
            }
        }

        // フィールドの幅（マス）
        static public int Field_X {
            get {
                return FIELD_X;
            }
        }

        // フィールドの高さ（マス）
        static public int Field_Y {
            get {
                return FIELD_Y;
            }
        }
        
        // 編集フォームに描画、マス座標指定
        static public void Draw_edit(int x, int y, Body.Block.Color c) {
            Program.Root.Draw_edit(x, y, c);
        }

        static public void Draw_field(int x, int y, Body.Block.Color c) {
            Program.Root.Draw_field(x, y, c);
        }

        // 編集フォームの幅（ピクセル）
        static public int Edit_width {
            get {
                return Program.Root.Edit_width;
            }
        }

        // 編集フォームの高さ（ピクセル）
        static public int Edit_height {
            get {
                return Program.Root.Edit_height;
            }
        }

        // フィールドフォームの幅（ピクセル）
        static public int Field_width {
            get {
                return Program.Root.Field_width;
            }
        }

        // フィールドフォームの高さ（ピクセル）
        static public int Field_height {
            get {
                return Program.Root.Field_height;
            }
        }

        // モード表示ラベルの変更
        static public void Change_label_state(string s) {
            Program.Root.Change_label_state(s);
        }

        // スコア表示ラベルの変更
        static public void Change_label_score(string s) {
            Program.Root.Change_label_score(s);
        }

        // フィールドに文字出力
        static public void Draw_string_field(int x, int y, string s) {
            Program.Root.Draw_string_field(x, y, s);
        }

        
    }
}
