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
        static private Block.Color[] mFields = null;    // NONE以外なら埋まっている
        static private Block.Color[] mFields_prev = null;    // 前回の描画
        static private List<Block> mBlocks = null;
        static private Block mForm_block = null;

        // フィールド操作を行うクラス
        static private Base mBase = new Setting();

        // 編集フォームブロックのプロパティ
        static public ref Block Form_block {
            get {
                return ref mForm_block;
            }
        }

        // フィールドに配置してあるブロック情報
        static public ref List<Block> Blocks {
            get {
                return ref mBlocks;
            }
        }

        // 新規作成
        static public void New_stage() {
            // 初期値を代入
            string s = Manager.New_stage();
            if (Manager.Box_file != s) {
                Manager.Box_file = s;
            }
            // ボックスファイルと同じ名前、新しくない
            else {
                return;
            }
            Init(s);
        }

        // 保存
        static public void Save() {
            if (mFilename == null) {
                return;
            }
            // 数、高さ、ブロック、位置を保存
            mXml_data = new Document();
            mXml_data.get_root().add_child(new Element("Stage"));
            // 数
            mXml_data.get_root().children[0].add_attribute(new XML_cs.Attribute("num", mBlocks.Count.ToString()));
            // 高さ
            int num = 0;
            foreach (Block.Color c in mFields) {
                if (c != Block.Color.NONE) {
                    break;
                }
                ++num;
            }
            int height = Manager.Field_Y - (num / Manager.Field_X);
            mXml_data.get_root().children[0].add_attribute(new XML_cs.Attribute("height", height.ToString()));
            // ブロックとその位置
            num = 0;
            foreach(Block b in mBlocks) {
                mXml_data.get_root().children[0].add_child(new Element("Data"));
                mXml_data.get_root().children[0].children[num].add_attribute(new XML_cs.Attribute("shape", b.Get_shape().ToString()));
                mXml_data.get_root().children[0].children[num].add_attribute(new XML_cs.Attribute("color", b.Get_color_string));
                mXml_data.get_root().children[0].children[num].add_attribute(new XML_cs.Attribute("x", b.X.ToString()));
                mXml_data.get_root().children[0].children[num].add_attribute(new XML_cs.Attribute("y", b.Y.ToString()));
                mXml_data.get_root().children[0].children[num].add_attribute(new XML_cs.Attribute("dir", b.Get_dir));
                ++num;
            }
            mXml_data.write(mFilename);
        }

        // ロード
        static public void Load() {
            string s = Manager.Box_file;
            if(s == null) {
                return;
            }
            Init(s);
            mXml_data = new Document(mFilename);
            // XMLデータから情報を読み込み、値を入力
            Element stage = mXml_data.get_root().children[0];   // <Stage>
            foreach(Element data in stage.children) {
                foreach (XML_cs.Attribute a in data.attribute) {
                    switch (a.name) {
                        case "shape":
                            mBlocks.Add(new Block(int.Parse(a.val)));
                            break;
                        case "color":
                            mBlocks[mBlocks.Count - 1].Get_color_string = a.val;
                            break;
                        case "x":
                            mBlocks[mBlocks.Count - 1].X = int.Parse(a.val);
                            break;
                        case "y":
                            mBlocks[mBlocks.Count - 1].Y = int.Parse(a.val);
                            break;
                        case "dir":
                            mBlocks[mBlocks.Count - 1].Get_dir = a.val;
                            break;
                    }
                }
            }
            // 描画
            foreach(Block b in mBlocks) {
                for(int num = 0;num < Manager.Block_num; ++num) {
                    if (b.Get_shape(num)) {
                        mFields[b.Get_X(num) + (b.Get_Y(num) * Manager.Field_X)] = b.Get_color;
                    }
                }
            }
            Redraw_field();
            Redraw_order();
            Draw_score();
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
        static public void Redraw_edit() {
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

        // 番号の再描画
        static public void Redraw_order() {
            if (mFields == null) {
                return;
            }
            // 一度上書き
            for(int y = 0; y < Manager.Field_Y; ++y) {
                for(int x = 0; x < Manager.Field_X; ++x) {
                    int point = x + y * Manager.Field_X;
                    if(mFields[point] != Block.Color.NONE) {
                        Manager.Draw_field(x, y, mFields[point]);
                    }
                }
            }
            // 番号を描画
            int order = 1;
            foreach (Block b in Blocks) {
                for (int num = 0; num < Manager.Block_num; ++num) {
                    if (b.Get_shape(num)) {
                        Manager.Draw_string_field(b.Get_X(num), b.Get_Y(num), order.ToString());
                    }
                }
                ++order;
            }
        }

        // フィールドフォームでクリック
        static public void Click_on_field(System.Windows.Forms.MouseEventArgs e) {
            // 初期化されていないなら終了
            if (Root.Blocks == null) {
                return;
            }
            // Baseに任せる
            mBase.Clicked(e);
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
            mBase.Moved(mass_x, mass_y);
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

        // ブロック情報の表示
        static public void Draw_score() {
            // 高さを調べる
            int num = 0;
            foreach (Block.Color c in mFields) {
                if (c != Block.Color.NONE) {
                    break;
                }
                ++num;
            }
            int height = Manager.Field_Y - (num / Manager.Field_X);
            Manager.Change_label_score("ブロック数：" + mBlocks.Count.ToString() + "  高さ：" + height.ToString());
        }

        // 白く塗る
        static private void Reset() {
            for (int y = 0; y < Manager.Field_Y; ++y) {
                for (int x = 0; x < Manager.Field_X; ++x) {
                    int point = x + y * Manager.Field_X;
                    Manager.Draw_field(x, y, Block.Color.NONE);
                }
            }
            for (int i = 0; i < Manager.Block_height; ++i) {
                for (int j = 0; j < Manager.Block_width; ++j) {
                    Manager.Draw_edit(j, i, Block.Color.NONE);
                }
            }
        }

        // 初期化、ファイル名
        static private void Init(string s) {
            mFilename = Manager.Path + s + ".xml";
            mFields = Enumerable.Repeat<Block.Color>(Block.Color.NONE, Manager.Field_X * Manager.Field_Y).ToArray();
            mFields_prev = Enumerable.Repeat<Block.Color>(Block.Color.NONE, Manager.Field_X * Manager.Field_Y).ToArray();
            mBlocks = new List<Block>();
            mForm_block = new Block(0);
            Reset();
        }

    }
}
