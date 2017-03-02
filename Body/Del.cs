using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tsumiki_tool.Body {
    class Del : Base{
        public Del() {

        }

        public void Clicked(MouseEventArgs e) {

        }

        // 何もしない
        public void Moved(int x, int y) {

        }

        public void Labeling() {
            Manager.Change_label_state("状態： 削除");
        }
    }
}
