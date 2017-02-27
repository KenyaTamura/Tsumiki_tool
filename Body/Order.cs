using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsumiki_tool.Body {
    class Order : Base{
        public void Clicked() {

        }

        // 何もしない
        public void Moved(int x, int y, Block b) {

        }

        public void Labeling() {
            Manager.Change_label_state("状態： 順序");
        }
    }
}
