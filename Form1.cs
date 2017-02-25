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
        private Button button_new;
        private Button button_save;
        private Button button_load;
        private Button button_set;
        private Button button_color;
        private Button button_edit;
        private Button button_del;
        private Button button_order;
        private Label label_state;
        private ComboBox box_file;

        // ピクチャー
        private PictureBox picture_edit;
        private PictureBox picture_body;

        // ボタンのサイズ
        private Size b_size;

        // ボタンサイズなどの情報(XML)
        private Document doc;

        public const string path = "Data\\";

        // ボタンの名前の列挙型、XMLの順番に対応している
        private enum B_name {
            NEW = 0,
            SAVE,
            LOAD,
            SET,
            COLOR,
            EDIT,
            DEL,
            ORDER
        }

        public Form1() {
            Initialize_component();
        }

        // 初期設定
        private void Initialize_component() {
            this.button_new = new Button();
            this.button_save = new Button();
            this.button_load = new Button();
            this.button_set = new Button();
            this.button_color = new Button();
            this.button_edit = new Button();
            this.button_del = new Button();
            this.button_order = new Button();
            this.label_state = new Label();
            this.box_file = new ComboBox();
            this.picture_edit = new PictureBox();
            this.picture_body = new PictureBox();

            // XMLから位置情報を受け取る
            doc = new Document(path + "config.xml");
            // ウィンドウのコンフィグ
            Element config = doc.get_root().children[0];
            // コンポーネント
            Element cmp = doc.get_root().children[1];
            // ピクチャ
            Element pic = doc.get_root().children[2];
            // ラベルのエレメント
            Element label = cmp.children[1];
            // コンボボックスのエレメント
            Element box = cmp.children[2];
            // ボタンのサイズ
            string[] b_xy = cmp.children[0].attribute[0].val.Split(',');
            b_size = new Size(int.Parse(b_xy[0]), int.Parse(b_xy[1]));


            // ボタン
            // button_new
            Button_setting(ref this.button_new, B_name.NEW);
            this.button_new.Click += new EventHandler(Component.B_new);

            // button_save
            Button_setting(ref this.button_save, B_name.SAVE);
            this.button_save.Click += new EventHandler(Component.B_save);

            // button_load          
            Button_setting(ref this.button_load, B_name.LOAD);
            this.button_load.Click += new EventHandler(Component.B_load);

            // button_set
            Button_setting(ref this.button_set, B_name.SET);
            this.button_set.Click += new EventHandler(Component.B_set);

            // button_color 
            Button_setting(ref this.button_color, B_name.COLOR);
            this.button_color.Click += new EventHandler(Component.B_color);

            // button_edit 
            Button_setting(ref this.button_edit, B_name.EDIT);
            this.button_edit.Click += new EventHandler(Component.B_edit);

            // button_del 
            Button_setting(ref this.button_del, B_name.DEL);
            this.button_del.Click += new EventHandler(Component.B_del);

            // button_order
            Button_setting(ref this.button_order, B_name.ORDER);
            this.button_order.Click += new EventHandler(Component.B_order);

            // ラベル
            // label_state 
            this.label_state.AutoSize = true;
            string[] label_xy = label.children[0].attribute[0].val.Split(',');
            this.label_state.Location = new Point(int.Parse(label_xy[0]), int.Parse(label_xy[1]));
            this.label_state.Text = label.children[0].attribute[1].val;

            // コンボボックス
            // box_file
            this.box_file.FormattingEnabled = true;
            string[] box_xy = box.children[0].attribute[0].val.Split(',');
            this.box_file.Size = new Size(int.Parse(box_xy[0]), int.Parse(box_xy[1]));
            box_xy = box.children[0].attribute[1].val.Split(',');
            this.box_file.Location = new Point(int.Parse(box_xy[0]), int.Parse(box_xy[1]));
            Add_box();

            // ピクチャ
            // picture_edit
            string[] picture_xy = pic.children[0].attribute[0].val.Split(',');
            this.picture_edit.Size = new Size(int.Parse(picture_xy[0]), int.Parse(picture_xy[1]));
            picture_xy = pic.children[0].attribute[1].val.Split(',');
            this.picture_edit.Location = new Point(int.Parse(picture_xy[0]), int.Parse(picture_xy[1]));

            Bitmap canvas = new Bitmap(600, 600);
            picture_edit.Image = canvas;
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(canvas);
            // 塗りつぶした四角形
            g.FillRectangle(Brushes.Red, 0, 0, 600, 600);
            g.Dispose();

            // picture_body
            picture_xy = pic.children[1].attribute[0].val.Split(',');
            this.picture_body.Size = new Size(int.Parse(picture_xy[0]), int.Parse(picture_xy[1]));
            picture_xy = pic.children[1].attribute[1].val.Split(',');
            this.picture_body.Location = new Point(int.Parse(picture_xy[0]), int.Parse(picture_xy[1]));
            canvas = new Bitmap(640, 640);
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            g = Graphics.FromImage(canvas);
            // 塗りつぶした四角形
            g.FillRectangle(Brushes.Red, 0, 0, 600, 640);
            picture_body.Image = canvas;
            g.Dispose();

            // コンポーネント設置
            // ウィンドウのコンフィグ
            this.MinimumSize = new Size(int.Parse(config.attribute[0].val), int.Parse(config.attribute[1].val));
            this.MaximumSize = new Size(int.Parse(config.attribute[0].val), int.Parse(config.attribute[1].val));
            this.Text = config.attribute[2].val;
            // ボタンの追加
            this.Controls.Add(this.button_new);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.button_load);
            this.Controls.Add(this.button_set);
            this.Controls.Add(this.button_color);
            this.Controls.Add(this.button_edit);
            this.Controls.Add(this.button_del);
            this.Controls.Add(this.button_order);
            // ラベルの追加
            this.Controls.Add(this.label_state);
            // コンボボックスの追加
            this.Controls.Add(this.box_file);
            // ピクチャの追加
            this.Controls.Add(this.picture_edit);
            this.Controls.Add(this.picture_body);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // ボタン、対応する列挙型
        private void Button_setting(ref Button b, B_name name) {
            int i = (int)(name);
            // コンポーネント（ボタン）
            Element cmp_b = doc.get_root().children[1].children[0];
            // 列挙型に対応したエレメント
            Element e = cmp_b.children[i];
            string[] xy = e.attribute[0].val.Split(',');
            b.Location = new Point(int.Parse(xy[0]), int.Parse(xy[1]));
            b.Size = b_size;
            b.Text = e.attribute[1].val;
            b.UseVisualStyleBackColor = true;
        }

        // コンボボックスに要素を追加
        private void Add_box() {
            // ファイルパスからファイル名を取得する
            string[] filename = System.IO.Directory.GetFiles(path, "stage*");
            for (int i = 0; i < filename.Length; ++i) {
                filename[i] = filename[i].Replace(path, "");
                filename[i] = filename[i].Replace(".xml", "");
                box_file.Items.Add(filename[i]);
            }
        }

        public string Box_file {
            // 指定中の要素
            get {
                int index = box_file.SelectedIndex;
                return box_file.Items[index].ToString();
            }
            // 末尾に追加
            set {
                // 重複排除
                int flag = box_file.Items.IndexOf(value);
                if (flag == -1) {
                    box_file.Items.Add(value);
                    box_file.SelectedIndex = box_file.Items.Count - 1;
                }
            }
        }

        public string new_filename() {
            string[] filename = System.IO.Directory.GetFiles(path, "stage*");
            return "stage" + filename.Length.ToString();
        }

    }
}
