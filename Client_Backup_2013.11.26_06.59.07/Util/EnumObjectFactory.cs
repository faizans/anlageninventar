using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Model;

namespace Client.Util {
    public class EnumObjectFactory{

        /// <summary>
        /// Generates a list with EnumObjects from an enumeration.
        /// This can be used if you want to bind enums as datasource but handle the enums like objects with additional attributes.
        /// Attributes can be extended in the EnumObjectFactorys inner class -> EnumObject
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<EnumObject> GenerateEnumObjectDataSource(Type enumType) {
            List<EnumObject> enumObjecList = new List<EnumObject>();
            Array enumValues = Enum.GetValues(enumType);
            foreach(var enumValue in enumValues){
                enumObjecList.Add(new EnumObject(enumValue.ToString()));
            }
            return enumObjecList;
        }

        /// <summary>
        /// The EnumObject with attributes
        /// </summary>
        public class EnumObject {
            public string Value { get; set; }
            public string Title { get; set; }
            public string ImageUrl { get; set; }

            public EnumObject(String value) {
                this.Value = value;
                this.Title = ""; //Retourn text from Apptext by passing value as key
            }
        }
    }
}
