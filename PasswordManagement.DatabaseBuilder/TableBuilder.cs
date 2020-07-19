﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PasswordManagement.DatabaseBuilder
{
    public class TableBuilder
    {
        internal List<Type> GetModels<T>(Assembly[] sourceAssemblies) where T : class
        {
            List<Type> types = new List<Type>();

            foreach (Assembly assembly in sourceAssemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (!types.Contains(type)
                        && type.BaseType == typeof(T))
                    {
                        types.Add(type);
                    }
                }
            }

            return types;
        }

        internal ModelBuilder BuildTables(List<Type> models, ModelBuilder builder)
        {
            foreach (Type type in models)
            {
                BuildTable(type, builder);
            }

            return builder;
        }

        internal ModelBuilder BuildTable(Type model, ModelBuilder builder)
        {
            EntityTypeBuilder entityBuilder = builder.Entity(model);
            string[] entityKey = GetEntityKeys(model);

            if (entityKey.Length > 0)
            {
                entityBuilder.HasKey(entityKey);
            }
            else
            {
                entityBuilder.HasNoKey();
            }
            return builder;
        }

        private string[] GetEntityKeys(Type model)
        {
            PropertyInfo[] allProperties = model.GetProperties();
            List<string> keys = new List<string>();

            foreach (PropertyInfo info in allProperties)
            {
                IEnumerable<Attribute> attributes = info.GetCustomAttributes();
                if (attributes.Contains(new KeyAttribute()))
                {
                    keys.Add(info.Name);
                }
            }

            return keys.ToArray();
        }
    }
}