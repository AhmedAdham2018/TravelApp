﻿using System;
using System.Collections.Generic;
using System.Text;
using Travel.Domain.Enums;

namespace Travel.Domain.Entities
{
    public class TourPackage
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public TourList List { get; set; }
        public string Name { get; set; }
        public string WhatToExpect { get; set; }
        public string MapLocation { get; set; }
        public float Price { get; set; }
        public int Duration { get; set; }
        public bool InstantConfirmation { get; set; }
        public Currency Currency { get; set; }

    }
}
