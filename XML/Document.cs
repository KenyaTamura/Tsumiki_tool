using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
使用例:
<Low_fanction_xml>
<Config width="960" height="720" title="XML test">
</Config>
</Low_fanction_xml>

エラー処理をまともに行ってないので使用注意
フォーマットを守ってください
*/

namespace XML_cs
{
    class Document{
        // Low_fanction_xmlのエレメント
        private Element mRoot;
        // 新規作成
        public Document() {
            mRoot = new Element("Low_fanction_xml");
        }

        // XMLファイル名から読み込み
        public Document(string filename) {
            string data;
            // ファイル読み込み
            try {
                System.IO.StreamReader sr = new System.IO.StreamReader(filename);
                data = sr.ReadToEnd();
                sr.Close();
            }
            catch {
                data = "<FileNotFound></FileNotFound>";
            }
            // 最初の読み込み位置
            int point = 1;
            Tag begin = new Tag(data, ref point);
            mRoot = new Element(begin, data, ref point);
        }

        // ルートノードを返す
        public Element get_root() {
            return mRoot;
        }

        // 現在の状態をファイル出力
        public void write(string filename) {
            string data = "";
            // 変換
            mRoot.convert_data_string(ref data);
            // ファイル書き込み
            System.IO.StreamWriter sw = new System.IO.StreamWriter(filename);
            sw.Write(data);
            sw.Close();
        }
    }
}
