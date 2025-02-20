﻿using EasyOC.DynamicTypeIndex.Models;
using FreeSql.Extensions.EntityUtil;
using FreeSql.Internal.Model;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyOC
{
    public static class ContentItemExtensions
    {
        public static Dictionary<string, object> ToDictModel(this ContentItem doc, DynamicIndexConfigModel config, bool useUnderline = true)
        {
            var docContent = doc.Content as JObject;
            var dictModel = new Dictionary<string, object>();
            dictModel.Add("Id", doc.Id);
            dictModel.Add("DocumentId", doc.Id);
            dictModel.Add("ContentItemVersionId", doc.ContentItemVersionId);
            dictModel.Add("ContentItemId", doc.ContentItemId);
            dictModel.Add("Published", doc.Published);
            dictModel.Add("Latest", doc.Latest);
            dictModel.Add("DisplayText", doc.DisplayText);

            foreach (var fConfig in config.Fields)
            {
                var valueKey = fConfig.Name;
                if (!useUnderline)
                {
                    valueKey = valueKey.Replace("_", String.Empty);
                }
                JToken valueToken;
                if (!fConfig.ContentFieldOption.ValuePath.IsNullOrWhiteSpace())
                {
                    valueToken = docContent.SelectToken(fConfig.ContentFieldOption.ValueFullPath);
                    if (valueToken != null)
                    {
                        try
                        {
                            dictModel.Add(valueKey,
                            valueToken.GetOCFieldValue(fConfig.ContentFieldOption));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }
                }
                else
                {
                    valueToken = docContent.SelectToken(fConfig.Name);
                    if (valueToken != null)
                    {
                        object value = null;
                        switch (valueToken.Type)
                        {
                            case JTokenType.Integer:
                                value = valueToken.Value<int?>();
                                break;
                            case JTokenType.Float:
                                value = valueToken.Value<float?>();
                                break;
                            case JTokenType.String:
                                value = valueToken.Value<string>();
                                break;
                            case JTokenType.Boolean:
                                value = valueToken.Value<bool?>();
                                break;
                            case JTokenType.Date:
                                value = valueToken.Value<DateTime?>();
                                break;
                            case JTokenType.TimeSpan:
                                value = valueToken.Value<string?>();
                                break;
                            default:
                                break;
                        }
                        dictModel.Add(valueKey, value);
                    }
                }
            }
            return dictModel;
        }


        public static List<Dictionary<string, object>> ToDictModel(this IEnumerable<ContentItem> docs, DynamicIndexConfigModel config, bool useUnderline = true)
        {
            var list = new List<Dictionary<string, object>>();
            foreach (var doc in docs)
            {
                list.Add(doc.ToDictModel(config, useUnderline));
            }
            return list;
        }

        public static List<object> ToModel(this IEnumerable<ContentItem> docs, DynamicIndexConfigModel config, Type type, TableInfo table, bool useUnderline = true)
        {

            var modleList = docs.Select(dict => dict.ToModel(config, type, table, useUnderline)).ToList();
            return modleList;
        }

        public static object ToModel(this ContentItem doc, DynamicIndexConfigModel config, Type type, TableInfo table, bool useUnderline = true)
        {
            var dict = doc.ToDictModel(config, useUnderline);
            var model = type.CreateInstanceGetDefaultValue();
            foreach (var kv in dict)
            {
                if (table.Columns.ContainsKey(kv.Key))
                {
                    // var value = Utils.GetDataReaderValue(table.Columns[kv.Key].CsType, kv.Value);
                    table.SetPropertyValue(model, kv.Key, kv.Value);
                }
            }
            return model;
        }

    }
}
