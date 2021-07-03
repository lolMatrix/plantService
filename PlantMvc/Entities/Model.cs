using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Model
    {
        public int Id { get; set; }

        public void UpdateModel(Model model)
        {
            Type t = model.GetType();

            var props = t.GetProperties();
            var myProps = t.GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                myProps[i].SetValue(this, props[i].GetValue(model));
            }
        }
    }
}
