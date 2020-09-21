namespace CrossCutting.Mapping
{
    public interface IMapper<TViewModel,TModel>
    {
        TViewModel ToViewModel(TModel model);
        TModel ToModel(TViewModel viewModel);
    
    }
}
