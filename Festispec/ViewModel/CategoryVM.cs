using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public class CategoryVM
    {
        private Category _category;

        public CategoryVM(Category c)
        {
            _category = c;
        }

        public string Name
        {
            get { return _category.Name; }
        }
    }
}
