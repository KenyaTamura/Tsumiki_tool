﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsumiki_tool.Body {
    class Setting : Base{
        public Setting() {

        }

        public void Clicked() {

        }

        public void Moved() {

        }

        public void Labeling() {
            Manager.Change_label_state("状態： 編集");
        }
    }
}
