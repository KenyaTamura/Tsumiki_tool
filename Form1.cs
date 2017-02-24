using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using XML_cs;

namespace Tsumiki_tool {
    // ウィンドウの設定
    class Form1 : Form {
        // コンポーネント
        private Button button_new;
        private Button button_save;
 //       private Button button_load;
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

        public Form1() {
            Initialize_component();
        }

        private void Initialize_component() {
            this.button_new = new Button();
            this.button_save = new Button();
 //           this.button_load = new Button();
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
            Document doc = new Document("Data\\config.xml");
            Element config = doc.get_root().children[0];
            // コンポーネント（ボタン）
            Element cmp = doc.get_root().children[1].children[0];   
            // ボタンのサイズ
            string[] tmp_s_array = cmp.attribute[0].val.Split(',');
            Size b_size = new Size(int.Parse(tmp_s_array[0]), int.Parse(tmp_s_array[1]));
            // ピクチャ
            Element pic = doc.get_root().children[2];
            Element e;  // 一時変数、コンポーネントそれぞれに利用
            string[] xy;    // コンマ区切りを格納、位置

            // button_new
            e = cmp.children[0];
            xy = e.attribute[0].val.Split(',');
            this.button_new.Location = new Point(int.Parse(xy[0]), int.Parse(xy[1]));
            this.button_new.Size = b_size;
            this.button_new.Text = e.attribute[1].val;
            this.button_new.UseVisualStyleBackColor = true;
            this.button_new.Click += new EventHandler(Component.B_new);

            // button_save
            this.button_save.Location = new Point(585, 48);
            this.button_save.Size = new Size(75, 23);
            this.button_save.Text = "保存";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new EventHandler(Component.B_save);

            // button_load
            // TODO
            /*
            this.button_load.Location = new Point(585, 77);
            this.button_load.Size = new Size(75, 23);
            this.button_load.Text = "ロード";
            this.button_load.UseVisualStyleBackColor = true;
            */

            // button_set
            this.button_set.Location = new Point(246, 199);
            this.button_set.Size = new Size(75, 23);
            this.button_set.Text = "設置";
            this.button_set.UseVisualStyleBackColor = true;
            this.button_set.Click += new EventHandler(Component.B_set); 

            // button_color 
            this.button_color.Location = new Point(327, 199);
            this.button_color.Size = new Size(75, 23);
            this.button_color.Text = "色替え";
            this.button_color.UseVisualStyleBackColor = true;
            this.button_color.Click += new EventHandler(Component.B_color);

            // button_edit 
            this.button_edit.Location = new Point(220, 304);
            this.button_edit.Size = new Size(75, 23);
            this.button_edit.Text = "編集";
            this.button_edit.UseVisualStyleBackColor = true;
            this.button_edit.Click += new EventHandler(Component.B_edit); 

            // button_del 
            this.button_del.Location = new Point(301, 304);
            this.button_del.Size = new Size(75, 23);
            this.button_del.Text = "削除";
            this.button_del.UseVisualStyleBackColor = true;
            this.button_del.Click += new EventHandler(Component.B_edit);

            // button_order
            this.button_order.Location = new Point(382, 304);
            this.button_order.Size = new Size(75, 23);
            this.button_order.Text = "順序";
            this.button_order.UseVisualStyleBackColor = true;
            this.button_order.Click += new EventHandler(Component.B_order); 

            // label_state 
            this.label_state.AutoSize = true;
            this.label_state.Location = new Point(260, 255);
            this.label_state.Text = "初期";

            // box_file
            this.box_file.FormattingEnabled = true;
            this.box_file.Location = new Point(562, 121);
            this.box_file.Size = new Size(121, 20);

            // picture_edit
            this.picture_edit.Location = new Point(480, 100);
            this.picture_edit.Size = new Size(256, 128);

            // picture_body
            this.picture_body.Location = new Point(10, 100);
            this.picture_body.Size = new Size(384, 512);
           
            // コンポーネント設置
            // ウィンドウのコンフィグ
            this.ClientSize = new Size(int.Parse(config.attribute[0].val), int.Parse(config.attribute[1].val));
            this.Text = config.attribute[2].val;
            // ボタンの追加
            this.Controls.Add(this.button_new);
            this.Controls.Add(this.button_save);
         //   this.Controls.Add(this.button_load);
            this.Controls.Add(this.button_set);
            this.Controls.Add(this.button_color);
            this.Controls.Add(this.button_edit);
            this.Controls.Add(this.button_del);
            this.Controls.Add(this.button_order);
            // ラベル
            this.Controls.Add(this.label_state);
            // コンボボックス
            this.Controls.Add(this.box_file);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
    }
}
