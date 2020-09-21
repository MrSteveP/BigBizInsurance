using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Core.ViewModels
{
  public  class DayViewModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public DayOfWeek DayOfWeek { set; get; }
    }
}
