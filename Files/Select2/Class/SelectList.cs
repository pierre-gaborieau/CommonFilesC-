using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Defontaine.Common
{
    public class SelectList<T, U>
    {
        public IEnumerable<SelectListItem> GetSelectList(IEnumerable<U> items)
        {
            var selectList = new List<SelectListItem>();

            foreach (var item in items)
            {
                selectList.Add(new SelectListItemSub
                {
                    Value = item.ToString(),
                    Text = item.ToString(),
                    Sub = item.ToString()
                }) ;
            }

            return selectList;
        }

        public IEnumerable<Select2> ListSelect2(IEnumerable<U> items, string text, string id, string search, string sub)
        {
            List<Select2> tamponsSelect2 = new List<Select2>();

            foreach (U item in items)
            {
                tamponsSelect2.Add(InitializerObject(text, id, item, sub));
            }

            if (!(string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)))
            {
                tamponsSelect2 = tamponsSelect2.Where(x => x.text.ToLower().Contains(search.ToLower())).ToList();
            }

            return tamponsSelect2;
        }

        private Select2 InitializerObject(string text, string id, object item, string sub)
        {
            var valueText = item.GetType().GetProperty(text).GetValue(item, null).ToString();
            var valueId = item.GetType().GetProperty(id).GetValue(item, null).ToString();
            if(sub != null)
            {
                var valueSub = item.GetType().GetProperty(sub).GetValue(item, null).ToString();
                return new Select2
                {
                    text = valueText,
                    id = valueId,
                    sub = valueSub
                };
            }

            return new Select2
            {
                text = valueText,
                id = valueId
                
            };
        }
    }
}
