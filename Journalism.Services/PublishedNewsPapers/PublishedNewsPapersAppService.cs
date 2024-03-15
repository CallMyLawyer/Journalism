using Journalism.Entites.PublishedNewsPaper;
using Journalism.Services.Categories.Contracts;
using Journalism.Services.News.Contracts.Exceptions;
using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.NewsPapers.Contracts.Exceptions;
using Journalism.Services.PublishedNewsPapers.Contracts;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.Services.PublishedNewsPapers.Contracts.Exceptions;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.PublishedNewsPapers;

public class PublishedNewsPapersAppService : PublishedNewsPapersService
{
   private readonly UnitOfWork _unitOfWork;
   private readonly AuthorCategoryRepository _authorCategoryRepository;
   private readonly PublishedNewsPapersRepository _publishedNewsPapersRepository;
   private readonly AuthorNewsPapersRepository _authorNewsPapersRepository;

   public PublishedNewsPapersAppService(UnitOfWork unitOfWork
   , AuthorCategoryRepository authorCategoryRepository,
   PublishedNewsPapersRepository publishedNewsPapersRepository
   , AuthorNewsPapersRepository authorNewsPapersRepository)
   {
      _authorNewsPapersRepository = authorNewsPapersRepository;
      _unitOfWork = unitOfWork;
      _authorCategoryRepository = authorCategoryRepository;
      _publishedNewsPapersRepository = publishedNewsPapersRepository;

   }

   public async Task Add(AddPublishedNewsPaperDto dto)
   {
      if (_authorNewsPapersRepository.IsExistNewsPaperId(dto.NewsPaperId))
      {
         throw new NewsPaperIdDoesNotExistException();
      }
      if (_authorNewsPapersRepository.IsWeight100OrNot(dto.NewsPaperId))
      {
         throw new NewsPaperWeightMustBe100ForPublishException();
      }

      if (_authorNewsPapersRepository.IsNewsPaperNewsWeightsFull(dto.NewsPaperId))
      {
         throw new NewsPaperNewsWeightsShouldBe100ForPublishingException();
      }
      var publishedNewsPaper = new PublishedNewsPaper()
      {
         NewsPaper = _authorNewsPapersRepository.FindNewsPaper(dto.NewsPaperId),
      };
      if (_publishedNewsPapersRepository.NewsPaperPublishedBefore(dto.NewsPaperId))
      {
         throw new ThisNewsPaperHasPublishedBeforeAndItCantPublishAgainWriteANewOneException();
      }
      if (_publishedNewsPapersRepository.IsAnyNewsPaperPublishedToDay())
      {
         throw new ToDaysNewsPaperHasAlreadyPublishedGetReadyForTomorrowException();
      }
      var newsPaper = _authorNewsPapersRepository.FindNewsPaper(dto.NewsPaperId);
      newsPaper.PublishedAt = DateTime.Today;
      publishedNewsPaper.Published = true;
      _publishedNewsPapersRepository.Add(publishedNewsPaper);
      await _unitOfWork.Complete();
   }

}