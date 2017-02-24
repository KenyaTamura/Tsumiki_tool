using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_cs{
    class Element{
        // 子エレメント
        private Element[] mChildren;
        // アトリビュート
        private Attribute[] mAttribute;
        // エレメント名
        private string mName;
      
        // 名前から作成
        public Element(string name) {
            mName = name;
        }

        // データから作成
        public Element(Tag begin, string data, ref int point) {
            mName = begin.get_name();
            mAttribute = begin.get_attribute();
            // 次のタグを用意
            for (; point < data.Length; ++point) {
                // 次のタグ
                if (data[point] == '<') {
                    ++point;
                    // コンストラクタでタグ読み込み
                    Tag tag = new Tag(data, ref point);
                    Tag.Type t = tag.get_type();
                    // 開始タグ
                    if (t == Tag.Type.BEGIN) {
                        // 子を1つ増やす                        
                        if (mChildren == null) {
                            mChildren = new Element[1];
                        }
                        else {
                            Array.Resize(ref mChildren, mChildren.Length + 1);
                        }
                        // 再帰構造
                        // 末尾に追加
                        mChildren[mChildren.Length - 1] = new Element(tag, data, ref point);                        
                    }
                    // 終了タグ
                    else if (t == Tag.Type.END) {
                        break;  // タグが終わったらこのエレメントは読み込み完了
                    }
                }
            }
        }

        // 名前のプロパティ
        public string name{
            get { return mName; }
            set { mName = value; }
        }

        // 子のプロパティ
        public Element[] children{
            get { return mChildren; }
        }

        // アトリビュートのプロパティ
        public Attribute[] attribute{
            get { return mAttribute; }
        }

        // 末尾に子エレメントを追加
        public void add_child(Element e) {
            if (mChildren == null) {
                mChildren = new Element[1];
            }
            else {
                Array.Resize(ref mChildren, mChildren.Length + 1);
            }
            mChildren[mChildren.Length - 1] = e;
        }

        // i番目のエレメントを削除
        public void del_child(int i) {
            if (mChildren == null || i < 0) {
                return;
            }
            int len = mChildren.Length;
            // 要素がひとつ
            if(i == 0 && len == 1) {
                mChildren = null;
            }
            if(i < len) {
                // (n=i)番目をn+1で上書き，n=length-1まで
                for(int n = i + 1; n < len; ++n) {
                    mChildren[n - 1] = mChildren[n];
                }
                // サイズを1つ小さくする
                Array.Resize(ref mChildren, len - 1);
            }
        }

        // i番目とj番目の子を入れ替え
        public void swap_children(int i, int j) {
            if (mChildren == null || i < 0 || j < 0) {
                return;
            }
            int len = mChildren.Length;
            if (i < len && j < len) {
                Element tmp = mChildren[i];
                mChildren[i] = mChildren[j];
                mChildren[j] = tmp;
            }
        }
        
        // 末尾にアトリビュートの追加
        public void add_attribute(Attribute a) {
            if (mAttribute == null) {
                mAttribute = new Attribute[1];
            }
            else {
                 Array.Resize(ref mAttribute, mAttribute.Length + 1);
            }
            mAttribute[mAttribute.Length - 1] = a;
        }
        
        // i番目のアトリビュートを削除
        public void del_attribute(int i) {
            if (mAttribute == null || i < 0) {
                return;
            }
            int len = mAttribute.Length;
            // 要素がひとつ
            if (i == 0 && len== 1) {
                mChildren = null;
            }
            if (i < len) {
                // (n=i)番目をn+1で上書き，n=length-1まで
                for (int n = i + 1; n < len; ++n) {
                    mAttribute[n - 1] = mAttribute[n];
                }
                // サイズを1つ小さくする
                Array.Resize(ref mAttribute, len - 1);
            }
        }

        // i番目とj番目のアトリビュートを入れ替え
        public void swap_attribute(int i, int j) {
            if(mAttribute == null || i < 0 || i < 0) {
                return;
            }
            int len = mAttribute.Length;
            if (i < len && j < len) {
                Attribute tmp = mAttribute[i];
                mAttribute[i] = mAttribute[j];
                mAttribute[j] = tmp;
            }
        }

        // 書込み用のデータ変換
        public void convert_data_string(ref string data){
            data += '<';    // タグ開始
            data += mName;  // エレメント名
            // アトリビュート
            if (mAttribute != null) {
                for (int i = 0; i < mAttribute.Length; ++i) {
                    data += ' ';    // スペース
                    data += mAttribute[i].name;  // アトリビュート名
                    data += '=';    // イコール
                    data += '"';    // ダブルコーテション
                    data += mAttribute[i].val;   // アトリビュート要素
                    data += '"';    // 終了
                }
            }
            data += '>';	// タグ終了
            data += '\n';   // タグごとに改行
            // 子に再帰
            if (mChildren != null) {
                for (int i = 0; i < mChildren.Length; ++i) {
                    mChildren[i].convert_data_string(ref data);
                }
            }
            // 終了タグ
            data += '<';
            data += '/';
            data += mName;
            data += '>';
            data += '\n';
		}

    }
}
