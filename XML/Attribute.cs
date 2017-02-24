using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_cs{
    class Attribute {
        // アトリビュート名
        private string mName;
        // アトリビュート値
        private string mVal;

        // 名前と値から作成
        public Attribute(string name, string val) {
            mName = name;
            mVal = val;
        }

        // 値のプロパティ
        public string val{
            get { return mVal; }
            set { mVal = value; }
        }

        // 名前のプロパティ
        public string name{
            get { return mName; }
            set { mName = value; }
        }
    }
}
