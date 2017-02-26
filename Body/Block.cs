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

        // 形、位置、向きの情報
        private bool[] mShape;
        private int mX;
        private int mY;
        private Dir mDir;

        public Block(int shape) {
            mX = -1;
            mY = -1;
            mDir = Dir.NORTH;
            mShape = new bool[Root.BLOCK_NUM];
            if (shape < 0 || shape > 255) {
                return;
            }
            int num = 0;
            while (num != Root.BLOCK_NUM) {
                if (shape % Root.BLOCK_HIGH == 1) {
                    mShape[num] = true;
                }
                else {
                    mShape[num] = false;
                }
                shape /= Root.BLOCK_HIGH;
                ++num;
            }
        }
    }
}
