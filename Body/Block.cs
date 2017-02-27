using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsumiki_tool.Body {
    class Block {
        // 向き
        public enum Dir {
            NORTH = 0,
            EAST,
            SOUTH,
            WEST
        }

        // 色
        public enum Color {
            RED = 0,
            BLUE,
            GREEN,
            YELLOW,
            NONE
        }

        // 形、位置、向き、色の情報
        private bool[] mShape;
        private int mX;
        private int mY;
        private Dir mDir;
        private Color mColor;

        public Block(int shape) {
            mX = -1;
            mY = -1;
            mDir = Dir.NORTH;
            mColor = Color.RED;
            mShape = new bool[Manager.Block_num];
            if (shape < 0 || shape > 255) {
                return;
            }
            int num = 0;
            while (num != Manager.Block_num) {
                // ビットを調べる
                if (shape % 2 == 1) {
                    mShape[num] = true;
                }
                else {
                    mShape[num] = false;
                }
                shape /= 2;
                ++num;
            }
        }

        // マス座標でブロックの状態を獲得
        public bool Get_shape(int x, int y) {
            if (Range_check(x,y)) {
                return mShape[y * Manager.Block_width + x];
            }
            return false;
            
        }

        public Color Get_color {
            get {
                return mColor;
            }
        }

        // マス座標XYのブロックの状態を変える
        public void Change_shape(int x, int y) {
            if (Range_check(x,y)) {
                mShape[y * Manager.Block_width + x] = !mShape[y * Manager.Block_width + x];
            }
        }

        // 色替え
        public void Change_color() {
            ++mColor; 
            if (mColor == Color.NONE) {
                mColor = Color.RED;
            }
        }

        // 範囲外ならfalse
        private bool Range_check(int x, int y) {
            if (x < 0 || x >= Manager.Block_width && y < 0 || y >= Manager.Body_height) {
                return false;
            }
            return true;
        }
        
    }
}
