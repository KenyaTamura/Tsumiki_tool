using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using XML_cs;

namespace Tsumiki_tool {

    class Form1 : Form {
        // コンポーネント
        private Button mButton_new;
        private Button mButton_save;
        private Button mButton_load;
//        private Button mButton_set;
        private Button mButton_color;
        private Button mButton_edit;
        private Button mButton_del;
        private Button mButton_order;
        private Label mLabel_state;
        private Label mLabel_score;
        private ComboBox mBox_file;

        // ピクチャー
        private PictureBox mPicture_edit;
        private Bitmap mBitmap_edit;
        private PictureBox mPicture_field;
        private Bitmap mBitmap_field;
        
        // ボタンのサイズ
        private Size mB_size;

        // ボタンサイズなどの情報(XML)
        private Document mDoc;

        // 初期データの識別、XMLの格納順
        private enum Init_define {
            CONFIG = 0,
            COMPONENT,
            PICTURE
        }

        // コンポーネントの識別
        private enum Cmp_define {
            BUTTON = 0,
            LABEL,
            COMBO_BOX
        }

        // ボタンの名前の列挙型、XMLの順番に対応している
        private enum B_name {
            NEW = 0,
            SAVE,
            LOAD,
 //           SET,
            COLOR,
            EDIT,
            DEL,
            ORDER
        }

        // ピクチャの名前の列挙型、XMLの順番に対応している
        private enum P_name {
            EDIT = 0,
            FIELD
        }
        public Form1() {
            Initialize_component();
        }

        // 初期設定
        private void Initialize_component() {
            this.mButton_new = new Button();
            this.mButton_save = new Button();
            this.mButton_load = new Button();
 //           this.mButton_set = new Button();
            this.mButton_color = new Button();
            this.mButton_edit = new Button();
            this.mButton_del = new Button();
            this.mButton_order = new Button();
            this.mLabel_state = new Label();
            this.mLabel_score = new Label();
            this.mBox_file = new ComboBox();
            this.mPicture_edit = new PictureBox();
            this.mPicture_field = new PictureBox();

            // XMLから位置情報を受け取る
            mDoc = new Document(Manager.Path + "config.xml");
            // ウィンドウのコンフィグ
            Element config = mDoc.get_root().children[(int)Init_define.CONFIG];
            // コンポーネント
            Element cmp = mDoc.get_root().children[(int)Init_define.COMPONENT];
            // ラベルのエレメント
            Element label = cmp.children[(int)Cmp_define.LABEL];
            // コンボボックスのエレメント
            Element box = cmp.children[(int)Cmp_define.COMBO_BOX];
            // ボタンのサイズ
            string[] b_xy = cmp.children[(int)Cmp_define.BUTTON].attribute[0].val.Split(',');
            mB_size = new Size(int.Parse(b_xy[0]), int.Parse(b_xy[1]));


            // ボタン
            // button_new
            Button_setting(ref this.mButton_new, B_name.NEW);
            this.mButton_new.Click += new EventHandler(Component.B_new);

            // button_save
            Button_setting(ref this.mButton_save, B_name.SAVE);
            this.mButton_save.Click += new EventHandler(Component.B_save);

            // button_load          
            Button_setting(ref this.mButton_load, B_name.LOAD);
            this.mButton_load.Click += new EventHandler(Component.B_load);

            /*
            // button_set
            Button_setting(ref this.mButton_set, B_name.SET);
            this.mButton_set.Click += new EventHandler(Component.B_set);
            */

            // button_color 
            Button_setting(ref this.mButton_color, B_name.COLOR);
            this.mButton_color.Click += new EventHandler(Component.B_color);

            // button_edit 
            Button_setting(ref this.mButton_edit, B_name.EDIT);
            this.mButton_edit.Click += new EventHandler(Component.B_edit);

            // button_del 
            Button_setting(ref this.mButton_del, B_name.DEL);
            this.mButton_del.Click += new EventHandler(Component.B_del);

            // button_order
            Button_setting(ref this.mButton_order, B_name.ORDER);
            this.mButton_order.Click += new EventHandler(Component.B_order);

            // ラベル
            // label_state 
            this.mLabel_state.AutoSize = true;
            string[] label_xy = label.children[0].attribute[0].val.Split(',');
            this.mLabel_state.Location = new Point(int.Parse(label_xy[0]), int.Parse(label_xy[1]));
            this.mLabel_state.Text = label.children[0].attribute[1].val;

            // label_score
            this.mLabel_score.AutoSize = true;
            label_xy = label.children[1].attribute[0].val.Split(',');
            this.mLabel_score.Location = new Point(int.Parse(label_xy[0]), int.Parse(label_xy[1]));
            this.mLabel_score.Text = label.children[1].attribute[1].val;

            // コンボボックス
            // box_file
            this.mBox_file.FormattingEnabled = true;
            string[] box_xy = box.children[0].attribute[0].val.Split(',');
            this.mBox_file.Size = new Size(int.Parse(box_xy[0]), int.Parse(box_xy[1]));
            box_xy = box.children[0].attribute[1].val.Split(',');
            this.mBox_file.Location = new Point(int.Parse(box_xy[0]), int.Parse(box_xy[1]));
            Add_box();
            this.mBox_file.DropDownStyle = ComboBoxStyle.DropDownList;

            // ピクチャ
            // picture_edit
            Picture_setting(ref mPicture_edit, ref mBitmap_edit, P_name.EDIT);
            mPicture_edit.MouseUp += new MouseEventHandler(Component.M_edit);

            // picture_field
            Picture_setting(ref mPicture_field, ref mBitmap_field, P_name.FIELD);
            mPicture_field.MouseUp += new MouseEventHandler(Component.M_field_click);
            mPicture_field.MouseMove += new MouseEventHandler(Component.M_field_move);
            mPicture_field.MouseLeave += new EventHandler(Component.M_field_leave);

            // コンポーネント設置
            // ウィンドウのコンフィグ
            this.MinimumSize = new Size(int.Parse(config.attribute[0].val), int.Parse(config.attribute[1].val));
            this.MaximumSize = new Size(int.Parse(config.attribute[0].val), int.Parse(config.attribute[1].val));
            this.Text = config.attribute[2].val;
            // ボタンの追加
            this.Controls.Add(this.mButton_new);
            this.Controls.Add(this.mButton_save);
            this.Controls.Add(this.mButton_load);
 //           this.Controls.Add(this.mButton_set);
            this.Controls.Add(this.mButton_color);
            this.Controls.Add(this.mButton_edit);
            this.Controls.Add(this.mButton_del);
            this.Controls.Add(this.mButton_order);
            // ラベルの追加
            this.Controls.Add(this.mLabel_state);
            this.Controls.Add(this.mLabel_score);
            // コンボボックスの追加
            this.Controls.Add(this.mBox_file);
            // ピクチャの追加
            this.Controls.Add(this.mPicture_edit);
            this.Controls.Add(this.mPicture_field);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // ボタンのセット
        // ボタン、対応する列挙型
        private void Button_setting(ref Button b, B_name name) {
            int i = (int)(name);
            // コンポーネント（ボタン）
            Element cmp_b = mDoc.get_root().children[1].children[0];
            // 列挙型に対応したエレメント
            Element e = cmp_b.children[i];
            string[] xy = e.attribute[0].val.Split(',');
            b.Location = new Point(int.Parse(xy[0]), int.Parse(xy[1]));
            b.Size = mB_size;
            b.Text = e.attribute[1].val;
            b.UseVisualStyleBackColor = true;
        }

        // コンボボックスに要素を追加
        private void Add_box() {
            // ファイルパスからファイル名を取得する
            string[] filename = System.IO.Directory.GetFiles(Manager.Path, "stage*");
            for (int i = 0; i < filename.Length; ++i) {
                filename[i] = filename[i].Replace(Manager.Path, "");
                filename[i] = filename[i].Replace(".xml", "");
                mBox_file.Items.Add(filename[i]);
            }
        }

        // ピクチャのセット
        // ピクチャ、ビットマップ、対応する列挙型
        private void Picture_setting(ref PictureBox p, ref Bitmap b, P_name name) {
            // ピクチャのエレメント
            // ピクチャとビットマップの作成
            Element pic = mDoc.get_root().children[(int)Init_define.PICTURE].children[(int)name];
            string[] picture_xy = pic.attribute[0].val.Split(',');
            p.Size = new Size(int.Parse(picture_xy[0]), int.Parse(picture_xy[1]));
            b = new Bitmap(p.Width, p.Height);
            picture_xy = pic.attribute[1].val.Split(',');
            p.Location = new Point(int.Parse(picture_xy[0]), int.Parse(picture_xy[1]));           
            p.Image = b;
            // 描画
            Graphics g = Graphics.FromImage(b);
            // 下地の色
            g.FillRectangle(Brushes.White, 0, 0, p.Width, p.Height);
            g.Dispose();
            // 背景
            Draw_back(p, b, name);
        }

        // 背景の描画
        private void Draw_back(PictureBox p, Bitmap b, P_name name) {
            Element pic = mDoc.get_root().children[(int)Init_define.PICTURE].children[(int)name];
            Graphics g = Graphics.FromImage(b);
            // 外枠の長方形
            Pen pen = new Pen(Color.Black, 3);
            g.DrawRectangle(pen, 0, 0, p.Width - 1, p.Height - 1);
            // 内枠の長方形
            pen = new Pen(Color.Gray, 1);
            int row = int.Parse(pic.attribute[2].val);
            int column = int.Parse(pic.attribute[3].val);
            int w = p.Width / column;
            int h = p.Height / row;
            for (int i = 0; i < row; ++i) {
                for (int j = 0; j < column; ++j) {
                    g.DrawRectangle(pen, w * j, h * i, w, h);
                }
            }
            g.Dispose();
        }

        // コンボボックスの操作
        public string Box_file {
            // 指定中の要素
            get {
                int index = mBox_file.SelectedIndex;
                return mBox_file.Items[index].ToString();
            }
            // 末尾に追加
            set {
                // 重複排除
                int flag = mBox_file.Items.IndexOf(value);
                if (flag == -1) {
                    mBox_file.Items.Add(value);
                    mBox_file.SelectedIndex = mBox_file.Items.Count - 1;
                }
            }
        }

        // ステージ番号を返す
        public string New_stage() {
            string[] filename = System.IO.Directory.GetFiles(Manager.Path, "stage*");
            return "stage" + filename.Length.ToString();
        }

        // 編集フォームにブロックを描画
        // マスのXY座標、色
        public void Draw_edit(int x, int y, Body.Block.Color c) {
            if(x < 0 || x >= Manager.Block_width && y < 0 || y >= Manager.Block_height) {
                return;
            }
            Brush b;
            switch (c) {
                case Body.Block.Color.RED:
                    b = Brushes.Red;
                    break;
                case Body.Block.Color.BLUE:
                    b = Brushes.Blue;
                    break;
                case Body.Block.Color.GREEN:
                    b = Brushes.LimeGreen;
                    break;
                case Body.Block.Color.YELLOW:
                    b = Brushes.Yellow;
                    break;
                default:
                    b = Brushes.White;
                    break;
            }
           
            // 描画
            Graphics g = Graphics.FromImage(mBitmap_edit);
            mPicture_edit.Image = mBitmap_edit;
            int w = mPicture_edit.Width / Manager.Block_width;
            int h = mPicture_edit.Height / Manager.Block_height;
            g.FillRectangle(b, w * x, h * y, w, h);
            g.Dispose();
            // 背景
            Draw_back(mPicture_edit, mBitmap_edit, P_name.EDIT);
        }


        // フィールドフォームにブロックを描画
        // マスのXY座標、色
        public void Draw_field(int x, int y, Body.Block.Color c) {
            if (x < 0 || x >= Manager.Field_X && y < 0 || y >= Manager.Field_Y) {
                return;
            }
            Brush b;
            switch (c) {
                case Body.Block.Color.RED:
                    b = Brushes.Red;
                    break;
                case Body.Block.Color.BLUE:
                    b = Brushes.Blue;
                    break;
                case Body.Block.Color.GREEN:
                    b = Brushes.LimeGreen;
                    break;
                case Body.Block.Color.YELLOW:
                    b = Brushes.Yellow;
                    break;
                default:
                    b = Brushes.White;
                    break;
            }
            Graphics g = Graphics.FromImage(mBitmap_field);
            mPicture_field.Image = mBitmap_field;
            int w = mPicture_field.Width / Manager.Field_X;
            int h = mPicture_field.Height / Manager.Field_Y;
            g.FillRectangle(b, w * x, h * y, w, h);            
            g.Dispose();
            Draw_back(mPicture_field, mBitmap_field, P_name.FIELD);
        }

        // 編集フォームの幅（ピクセル）
        public int Edit_width {
            get {
                return mPicture_edit.Width;
            }
        }

        // 編集フォームの高さ（ピクセル）
        public int Edit_height {
            get {
                return mPicture_edit.Height;
            }
        }

        // フィールドフォームの幅（ピクセル）
        public int Field_width {
            get {
                return mPicture_field.Width;
            }
        }

        // フィールドフォームの高さ（ピクセル）
        public int Field_height {
            get {
                return mPicture_field.Height;
            }
        }

        // モード表示ラベルの変更
        public void Change_label_state(string s) {
            mLabel_state.Text = s;
        }

        // フィールドフォームに文字描画
        public void Draw_string_field(int x, int y, string s) {
            Graphics g = Graphics.FromImage(mBitmap_field);
            mPicture_field.Image = mBitmap_field;
            int w = mPicture_field.Width / Manager.Field_X;
            int h = mPicture_field.Height / Manager.Field_Y;
            g.DrawString(s, new Font("",12), Brushes.Black, w * x, h * y);
            g.Dispose();
        }
    }
}
