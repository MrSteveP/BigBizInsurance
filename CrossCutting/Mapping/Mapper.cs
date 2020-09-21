using System;

namespace CrossCutting.Mapping
{
    public abstract class Mapper<TMapper, TViewModel, TModel> : IMapper<TViewModel, TModel>
        where TMapper : class, new()
    {
        private static readonly Lazy<TMapper> mapper = new Lazy<TMapper>(() => new TMapper());

        public static TMapper Map => mapper.Value;

        protected Mapper()
        {

        }

        public abstract TViewModel ToViewModel(TModel model);

        public abstract TModel ToModel(TViewModel viewModel);

    }
}
