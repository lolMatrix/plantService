using System;

namespace Entities
{
    public class Model
    {
        public int Id { get; set; }

        public void UpdateModel(Model model)
        {
            Type t = model.GetType();

            var props = t.GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                props[i].SetValue(this, props[i].GetValue(model));
            }
        }
    }
}
