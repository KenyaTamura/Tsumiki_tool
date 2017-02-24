using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_cs{
    class Tag{
        // このタグのアトリビュート
        private Attribute[] mAttribute;
        // エレメント名
        private string mName;
        // 開始タグか終端タグか
        private Type mType = Type.BEGIN;
        public enum Type{
            BEGIN,
            END
        }

        // エレメント名だけで作成
        public Tag(string name) {
            mName = name;
        }

        // データと読み込み位置を受け取る
        public Tag(string data, ref int point) {
            // 一時格納
            string buf_name = "";
            string buf_val = "";
            // 状態
            int m = 0;
            // ループ脱出
            bool end_flag = false;
            for (; point < data.Length; ++point) {
                char c = data[point];
                switch (m) {
                    case 0: // 初期
                        if (c == '/') {
                            // 終了タグ
                            mType = Type.END;
                        }
                        else if (is_normal_char(c)) {
                            mName += c;
                            m = 1;  // 1へ遷移
                        }
                        break;
                    case 1: // エレメント名
                        if (c == '>') {
                            end_flag = true;    // 終了
                        }
                        else if (is_normal_char(c)) {
                            mName += c;
                        }
                        else {
                            m = 2;  // 2へ遷移
                        }
                        break;
                    case 2: // エレメント名 or アトリビュート終了
                        if (c == '>') {
                            end_flag = true;    // 終了
                        }
                        else if (is_normal_char(c)) {
                            buf_name += c;  // アトリビュート名
                            m = 3;  // 3へ遷移
                        }
                        break;
                    case 3: // アトリビュート名
                        if (c == '=') {
                            m = 4;  // 4へ遷移
                        }
                        else if (is_normal_char(c)) {
                            buf_name += c;
                        }
                        break;
                    case 4: // アトリビュート名終了
                        if (c == '"') {
                            m = 5;  // 5へ遷移
                        }
                        break;
                    case 5: // アトリビュート要素
                        if (c == '"') {
                            m = 2;  // 2へ遷移
                            // サイズを1増やす
                            if (mAttribute == null) {
                                mAttribute = new Attribute[1];
                            }
                            else {
                                Array.Resize(ref mAttribute, mAttribute.Length + 1);
                            }
                            // 末尾に入れる
                            mAttribute[mAttribute.Length - 1] = new Attribute(buf_name, buf_val);
                            //名前と値を初期化
                            buf_name = "";
                            buf_val = "";
                        }
                        else {
                            buf_val += c;
                        }
                        break;
                    default:
                        break;
                }
                if (end_flag) { break; }
            }
        }

        // 利用できる文字
        bool is_normal_char(char c) {
            if (c >= '0' && c <= '9') {
                return true;
            }
            if (c >= 'a' && c <= 'z') {
                return true;
            }
            if (c >= 'A' && c <= 'Z') {
                return true;
            }
            if (c == '_') {
                return true;
            }
            return false;
        }
        
        // エレメント名
       public string get_name() {
            return mName;
        }

        public Type get_type() {
            return mType;
        } 

        // アトリビュート
        public Attribute[] get_attribute() {
            return mAttribute;
        }
    }
}
