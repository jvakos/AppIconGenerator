using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppIconGenerator {
    class IconDimension {
        public int Width { get; set; }
        public int Height { get; set; }

        public IconDimension() {

        }
        public IconDimension(int uniform) {
            Width = uniform;
            Height = uniform;
        }

        public override string ToString() {
            return $"{Width}x{Height}";
        }

    }
}
