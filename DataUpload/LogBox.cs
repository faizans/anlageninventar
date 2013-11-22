using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpload {
    interface LogBox {
        void insert(string text);
        void clear();
        void counterUp(string text);
        void setLockState(Boolean lockState);
    }
}
