using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WildBunch.Business.DataTransferObjects;

namespace WildBunch.Models.ViewModels
{
    public class CharacterViewModel
    {
        public string CharacterId { get; set; }
        public string Name { get; set; }
        public int HealthPoints { get; set; }
        public double Dollars { get; set; }

        public BagViewModel Bag { get; set; }
    }

    public class BagViewModel
    {
        public BagViewModel(CharacterBagDto bag) 
        {
            BagId = bag.CharacterBagId;

            foreach(var item in bag.Items)
            {
                ItemsCarried.Add(new BagItemViewModel
                {
                    ItemId = (int)item.Type,
                    ItemName = Enum.GetName(item.Type),
                    Carried = item.QuantityCarried
                });
            }
        }

        public string BagId { get; set; }
        public List<BagItemViewModel> ItemsCarried { get; set; } = new List<BagItemViewModel>();
        //public Dictionary<string, int> ItemsCarried { get; set; } = new Dictionary<string, int>();
    }

    public class BagItemViewModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Carried { get; set; }
    }
}
