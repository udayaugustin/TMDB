using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDB.ViewModels;

namespace TMDB.Helpers
{
    public class ViewModelLocator
    {
        private readonly IServiceProvider _serviceProvider;

        public ViewModelLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public DetailPageViewModel DetailPageViewModel => _serviceProvider.GetRequiredService<DetailPageViewModel>();
    }

}
