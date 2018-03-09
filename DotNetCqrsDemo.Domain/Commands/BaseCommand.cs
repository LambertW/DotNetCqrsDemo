using CQRSlite.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCqrsDemo.Domain.Commands
{
    public class BaseCommand : ICommand
    {
        public Guid Id { get; set; }

        public int ExpectedVersion { get; set; }
    }
}
