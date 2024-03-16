using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.NewsPapers.Contracts.Exceptions;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.Services.Users.Contracts;
using Journalism.Services.Users.Contracts.Dtos;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.Users;

public class UserAppService : UserService
{
    private readonly UserRepository _repository;
    private readonly UnitOfWork _unitOfWork;
    private readonly AuthorNewsPapersRepository _authorNewsPapers;

    public UserAppService(
        UserRepository repository , UnitOfWork unitOfWork,
        AuthorNewsPapersRepository authorNewsPapers)
    {
        _authorNewsPapers = authorNewsPapers;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public List<GetPublishedNewspapersDto> GetAll()
    {
        return _repository.GetAll();
    }

    public GetPublishedNewspapersDto GetOne(FilterByIdDto title)
    {
        if (_authorNewsPapers.IsExistNewsPaperId(title.Id))
        {
            throw new NewsPaperIdDoesNotExistException();
        }
        var newspaper = _authorNewsPapers.FindNewsPaper(title.Id);
        var newsPaper = _authorNewsPapers.FindNewsPaper(title.Id);
        var news = newsPaper.NewsList;
        if (news != null)
            foreach (var item in news)
            {
                if (item != null) item.Views = item.Views + 1;
            }

        newsPaper.Views = newsPaper.Views ++;
        if (newsPaper.Categories != null)
            foreach (var category in newsPaper.Categories)
            {
                if (category != null) category.Views = category.Views + 1;
            }



        newspaper.Views = newspaper.Views + 1;
        _unitOfWork.Complete();
        return _repository.GetOne(title);
    }
}