using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

interface IPresenter
{
    void ChangeView(View view);
    void ChangeView(string viewName);
}
    

