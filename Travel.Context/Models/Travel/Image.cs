﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Context.Models
{
    public class Image
    {
        private Guid id;
        private string name;
        private long size;
        private string extension;
        private string filePath;
        private Guid idService;

        public Guid Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public long Size { get => size; set => size = value; }
        
        public string FilePath { get => filePath; set => filePath = value; }
        public Guid IdService { get => idService; set => idService = value; }
        public string Extension { get => extension; set => extension = value; }
    }
}
