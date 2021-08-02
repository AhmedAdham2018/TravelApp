using System;
using System.Collections.Generic;


namespace Travel.Application.Common.Interfaces
{
    public interface IDateTime
    {
        DateTime NowUtc { get; }
    }
}
