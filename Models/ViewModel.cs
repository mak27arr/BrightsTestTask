using System.Collections.Generic;

namespace BrightsTestTask.Models
{
    public class ViewModel<T>
    {
            public IEnumerable<T> Statistics { get; set; }
            public PageViewModel PageViewModel { get; set; }
    }
}
