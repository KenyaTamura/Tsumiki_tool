using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tsumiki_tool.Body {
    interface Base {
        // クリック処理
        void Clicked(MouseEventArgs e);
        // 移動処理
        void Moved(int x, int y);
        // ラベルの表示
        void Labeling();
    }
}
