using Drack.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Drack.Extensions
{
    public class CustomUtils
    {
        public string GetUserFullName(string id)
        {
            ApplicationDbContext AspDb;
            AspDb = new ApplicationDbContext();

            if (!string.IsNullOrEmpty(id) || !string.IsNullOrWhiteSpace(id))
            {
                ApplicationUser user = AspDb.Users.Where(x => x.Id == id).FirstOrDefault();
                return user.FullName;
            }
            else
            {
                return null;
            }
        }
        public List<string> ModelStateError(ModelStateDictionary ModelState)
        {
            return ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList();
        }
        public List<string> GetModelProperties(System.Type t)
        {
            return t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite).Select(x => { return Regex.Replace(x.Name, "(?!^)([A-Z])", " $1"); }).ToList();
        }
        public bool CanCovert(string value, System.Type type)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(type);
            return converter.IsValid(value);
        }
        public void CopyValues<T>(T target, T source)
        {
            System.Type t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null)
                    prop.SetValue(target, value, null);
            }
        }
        public Dictionary<string, string> ValidateExcelData(List<Dictionary<string, string>> excelData, System.Type PropertiesInterface)
        {
            Dictionary<string, string> validationReport = new Dictionary<string, string>();
            excelData.RemoveAt(0);
            foreach (var row in excelData)
            {
                int rowCount = excelData.IndexOf(row) + 2;
                int i = 0;
                foreach (var prop in PropertiesInterface.GetProperties())
                {
                    var column = row[i.ToString()];
                    if (!string.IsNullOrEmpty(column))
                    {
                        if (!CanCovert(column, prop.PropertyType))
                        {
                            validationReport.Add("status", false.ToString());
                            validationReport.Add("message", "Column '" + Regex.Replace(prop.Name, "(?!^)([A-Z])", " $1") + "' has a value type mismatched. Please check row " + rowCount + " of the sheet.");
                            return validationReport;
                        }
                    }
                    i++;
                }
            }
            return validationReport;
        }

    }
}