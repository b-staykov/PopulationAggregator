﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend;

public interface IStatService
{
    List<Tuple<string, int>> GetCountryPopulations();
    Task<List<Tuple<string, int>>> GetCountryPopulationsAsync();
}
