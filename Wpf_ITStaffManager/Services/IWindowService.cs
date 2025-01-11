using System;
using System.Collections.Generic;
using System.Linq;
namespace Wpf_ITStaffManager.Services
{
    public interface IWindowService
    {
        public bool ShowEmployeeDialog(ref string firstName, ref string lastName, ref string position, ref string department);
    } 
}
