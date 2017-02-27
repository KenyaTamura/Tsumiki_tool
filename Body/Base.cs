using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsumiki_tool.Body {
    interface Base {
        // クリック処理
        void Clicked();
        // 移動処理
        void Moved(int x, int y, Block b);
        // ラベルの表示
        void Labeling();
    }
}
