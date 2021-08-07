using Shared.Application.Interfaces;
using System;

namespace Common.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
