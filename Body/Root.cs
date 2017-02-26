using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XML_cs;

namespace Tsumiki_tool.Body {
    static class Root {
        // ブロック情報の定数
        public const int BLOCK_NUM = 8;
        public const int BLOCK_HIGH = 2;
        public const int BLOCK_WIDTH = 4;
        // フィールド情報の定数
        public const int FIELD_X = 10;
        public const int FIELD_Y = 20;

        // XMLデータ
        static private string filename = null;
        static private Document xml_data = null;

        // フィールド情報
        static private bool[] field;    // trueなら埋まっている
        static Block[] blocks;
        

        // ファイル名の操作
        static public string Filename {
            get {
                return filename;
            }
            set {
                filename = value;
            }
        }

        // 新規作成
        static public void New_stage() {
            // 初期値を代入
            xml_data = new Document();
            field = new bool[FIELD_X * FIELD_Y];
        }

        // 保存
        static public void Save() {
            xml_data.write(filename);
        }
    }
}
