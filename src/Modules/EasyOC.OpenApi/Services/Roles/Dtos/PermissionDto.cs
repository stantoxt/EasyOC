﻿using AutoMapper;
using OrchardCore.Security.Permissions;
using System.Collections.Generic;

namespace EasyOC.OpenApi.Dto
{
    [AutoMap(typeof(Permission))]
    public class PermissionDto
    {

        public string Name
        {
            get; set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Category
        {
            get;
            set;
        }

        public IEnumerable<PermissionDto> ImpliedBy
        {
            get; set;
        }
    }
}
