using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Converters;

namespace HealthCatalyst.Assessment.API.Formatters
{
    /// <summary>
    /// Converts the json serialized date as specified.
    /// </summary>
    public class DateFormatConverter : IsoDateTimeConverter
    { 
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}