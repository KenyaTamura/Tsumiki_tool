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
        private int mX_offset;  // 回転のずれに対応するため
        private int mY_offset;
        private Dir mDir;
        private Color mColor;

        public Block(int shape) {
            mX = -1;
            mY = -1;
            mX_offset = 0;
            mY_offset = 0;
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

        // 基準点
        public int X {
            get {
                return mX + mX_offset;
            }
            set {
                mX = value;
            }
        }

        public int Y {
            get {
                return mY + mY_offset;
            }
            set {
                mY = value;
            }
        }

        // マス座標でブロックの状態を獲得
        public bool Get_shape(int x, int y) {
            if (Range_check(x, y)) {
                return mShape[y * Manager.Block_width + x];
            }
            return false;

        }

        // 番号でブロックの状態を獲得
        public bool Get_shape(int num) {
            if (Range_check(num)) {
                return mShape[num];
            }
            return false;

        }

        // ブロック状態の数字
        public int Get_shape() {
            int shape = 0;
            for (int i = 0; i < Manager.Block_num; ++i) {
                if (mShape[i]) {
                    shape += (int)(System.Math.Pow(2, i));
                }
            }
            return shape;
        }

        // 色情報
        public Color Get_color {
            get {
                return mColor;
            }
        }

        // 色の文字列返し
        public string Get_color_string {
            get {
                switch (mColor) {
                    case Color.RED:
                        return "RED";
                    case Color.BLUE:
                        return "BLUE";
                    case Color.GREEN:
                        return "GREEN";
                    case Color.YELLOW:
                        return "YELLOW";
                }
                return null;
            }
            set {
                switch (value) {
                    case "RED":
                        mColor = Color.RED;
                        return;
                    case "BLUE":
                        mColor = Color.BLUE;
                        return;
                    case "GREEN":
                        mColor = Color.GREEN;
                        return;
                    case "YELLOW":
                        mColor = Color.YELLOW;
                        return;
                }
            }
        }

        // 向きの文字列返し、文字列代入
        public string Get_dir {
            get {
                switch (mDir) {
                    case Dir.NORTH:
                        return "NORTH";
                    case Dir.EAST:
                        return "EAST";
                    case Dir.SOUTH:
                        return "SOUTH";
                    case Dir.WEST:
                        return "WEST";
                }
                return null;
            }
            set {
                switch (value) {
                    case "NORTH":
                        mDir = Dir.NORTH;
                        return;
                    case "EAST":
                        mDir = Dir.SOUTH;
                        return;
                    case "SOUTH":
                        mDir = Dir.SOUTH;
                        return;
                    case "WEST":
                        mDir = Dir.WEST;
                        return;
                }
            }
        }


        // マス座標XYのブロックの状態を変える
        public void Change_shape(int x, int y) {
            if (Range_check(x, y)) {
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
            if (x < 0 || x >= Manager.Block_width || y < 0 || y >= Manager.Block_height) {
                return false;
            }
            return true;
        }

        private bool Range_check(int num) {
            if (num < 0 || num >= Manager.Block_num) {
                return false;
            }
            return true;
        }

        // 回転
        public void Lotation() {
            // 方向の数の剰余、東西南北以外ないはずだから4
            mDir = (Dir)((int)(mDir + 1) % 4);
            // 基準点を中心に回す
            switch (mDir) {
                case Dir.NORTH:
                    mY_offset += 3;
                    break;
                case Dir.EAST:
                    mX_offset -= 1;
                    break;
                case Dir.SOUTH:
                    mX_offset -= 2;
                    mY_offset -= 1;
                    break;
                case Dir.WEST:
                    mX_offset += 3;
                    mY_offset -= 2;
                    break;
            }
        }

        // num番目のブロック位置、マス座標X
        public int Get_X(int num) {
            if (!Range_check(num)) {
                return -1;
            }
            switch (mDir) {
                case Dir.NORTH:
                    return (mX + mX_offset) + (num % Manager.Block_width);
                case Dir.SOUTH:
                    return (mX + mX_offset) + (((Manager.Block_num - 1) - num) % Manager.Block_width);
                case Dir.EAST:
                    return (mX + mX_offset) + (((Manager.Block_num - 1) - num) / Manager.Block_width);
                case Dir.WEST:
                    return (mX + mX_offset) + (num / Manager.Block_width);
            }
            return -1;
        }

        // num番目のブロック位置、マス座標Y
        public int Get_Y(int num) {
            if (!Range_check(num)) {
                return -1;
            }
            switch (mDir) {
                case Dir.NORTH:
                    return (mY + mY_offset) + (num / Manager.Block_width);
                case Dir.SOUTH:
                    return (mY + mY_offset) + (((Manager.Block_num - 1) - num) / Manager.Block_width);
                case Dir.EAST:
                    return (mY + mY_offset) + (num % Manager.Block_width);
                case Dir.WEST:
                    return (mY + mY_offset) + (((Manager.Block_num - 1) - num) % Manager.Block_width);
            }
            return -1;
        }

    }
}
