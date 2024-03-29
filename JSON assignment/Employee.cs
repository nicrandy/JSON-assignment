﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using JSON_assignment;
//
//    var employee = Employee.FromJson(jsonString);

namespace JSON_assignment
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Employee
    {
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Age")]
        public long Age { get; set; }

        [JsonProperty("EmployeeType")]
        public string EmployeeType { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Salary")]
        public long Salary { get; set; }
    }

    public partial class Employee
    {
        public static Employee[] FromJson(string json) => JsonConvert.DeserializeObject<Employee[]>(json, JSON_assignment.Converter.Settings);
    }


    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
