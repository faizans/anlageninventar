
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Diagram
{
    public partial class LatestBarCode
    {

        public static LatestBarCode Get() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            List<LatestBarCode> barcodes = ctx.LatestBarCodes.ToList();
            return barcodes.ElementAtOrDefault(0) ;
        }

        public static string GenerateFullBarCode() {
            LatestBarCode latest = Get();
            string latestBarCode = latest != null ? latest.BarCode : null;

            if (latestBarCode != null) {

                String groupBarCode = latestBarCode.Split('.')[0];
                String articleBarCode = latestBarCode.Split('.')[1];

                int groupCodeInt = int.Parse(groupBarCode) + 1;

                if (groupBarCode.Length < groupCodeInt.ToString().Length) {
                    groupBarCode = groupCodeInt.ToString();
                }

                groupBarCode = groupBarCode.Remove(groupBarCode.Length - groupCodeInt.ToString().Length) + groupCodeInt.ToString();
                latestBarCode = groupBarCode + "." + articleBarCode;
            }

            return latestBarCode;
        }

        public static void Set(String latestsBarCode){
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            ctx.LatestBarCodes.Where(b => b.BarCode != null).FirstOrDefault().BarCode = latestsBarCode;
        }
    }
}
