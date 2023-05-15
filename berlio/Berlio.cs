using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mekus.berlio
{
    public class Berlio
    {
        public string RealisationDate { get; set; }
        public string RealisationTime { get; set; }
        public int RealisationCenterNumber { get; set; }
        public int RealisationObjectNumber { get; set; }
        public int RealisationCardNumber { get; set; }
        public int RealisationProductNumber { get; set; }
        public string RealisationProductName { get; set; }
        public float RealisationQuantity { get; set; }
        public float RealisationCost { get; set; }
        public float RealisationCostVat { get; set; }
        public float RealisationRoznPrice { get; set; }
        public float RealisationServiceSumma { get; set; }
        public float RealisationFixDiscountPercentMul { get; set; }
        public float RealisationVarDiscountPercentMul { get; set; }
        public float RealisationFixDiscountPrice { get; set; }
        public float RealisationVarDiscountPrice { get; set; }
        public float RealisationDiscountSumma { get; set; }
        public int CheckNumber { get; set; }
    }

}
