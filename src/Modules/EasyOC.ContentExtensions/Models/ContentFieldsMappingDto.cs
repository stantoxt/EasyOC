﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyOC.ContentExtensions.Models
{
    public class ContentFieldsMappingDto
    {
        private string _keyPath;

        public string KeyPath
        {
            get
            {
                return _keyPath ?? FieldName;
            }
            set { _keyPath = value; }
        }

        /// <summary>
        /// 内容项的直接属性，默认False,比如 Published，Latest，DisplayText,
        /// </summary>
        public bool IsSelf { get; set; } = false;

        public string FieldName { get; set; }
        public string FieldNameCamelCase => FieldName.ToCamelCase();
        public string DisplayName { get; set; }
        public string PartDisplayName { get; set; }
        // public GraphqlFieldOptions GraphqlFieldOptions { get; set; }
        public string GraphqlValuePath  { get; set; }

        public string PartName { get; set; }
        public JObject FieldSettings { get; set; }
        public string FieldType { get; set; }
        public string Description { get; set; }

        public string LastValueKey { get; set; }
        public bool IsBasic { get; set; }
    }

    // public class GraphqlFieldOptions
    // {
    //     public string ValuePath { get; set; }
    //     public string ArrayValuePath { get; set; }
    //     public string DisplayValuePath { get; set; }
    // }
}
