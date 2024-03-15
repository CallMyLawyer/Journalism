using Journalism.Entites.NewsPapers;
using Journalism.Services.News.Contracts.Exceptions;
using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.NewsPapers.Contracts.Dtos;
using Journalism.Services.NewsPapers.Contracts.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Journalism.Persistence.EF.NewsPapers;

public class EFNewsPaperRepository : AuthorNewsPapersRepository
{
   private readonly EFDataContext _context;

   public EFNewsPaperRepository(EFDataContext context)
   {
      _context = context;
   }

   public void Add(NewsPaper newsPaper)
   {
      _context.NewsPapers.Add(newsPaper);
   }

   public bool DuplicateTitle(string title)
   {
      if (_context.NewsPapers.Any(_=>_.Title==title))
      {
         return true;
      }

      return false;
   }

   public List<GetNewsPapersDto> GetAll()
   {
      var newsPapers = _context.NewsPapers.Select(_ => new GetNewsPapersDto()
      {
         Id = _.Id,
         Title = _.Title,
         Views = _.Views,
         Weight = _.Weight,
        Categories = _.Categories,
        NewsWeight = _.NewsWeight,
         PublishedAt = _.PublishedAt,
         NewsList = _.NewsList,
      }).ToList();
      


      return newsPapers;
   }

   public void Publish(NewsPaper newsPaper)
   {
      
   }

   public bool IsWeight100OrNot(int id)
   {
      var newspaper = _context.NewsPapers.First(_ => _.Id == id);
      if (newspaper.Weight!=100)
      {
         return true;
      }

      return false;
   }

   public void AddCategoryToNewsPaper(int newsPaperId, int categoryId)
   {
      var newspaper = _context.NewsPapers.Include(_=>_.Categories).First(_ => _.Id == newsPaperId);
      var category = _context.Categories.Include(_=>_.Tags).First(_ => _.Id == categoryId);
      newspaper.Categories?.Add(category);
   }

   public NewsPaper FindNewsPaper(int id)
   {
      var newspaper = _context.NewsPapers.First(_ => _.Id == id);
      return newspaper;
   }

   public IQueryable<GetNewsPapersDto> GetOne(int id)
   {
      var newsPapers = _context.NewsPapers.Select(_ => new GetNewsPapersDto()
      {
         Id = _.Id,
         Title = _.Title,
         Views = _.Views,
         Weight = _.Weight,
         Categories = _.Categories,
         NewsWeight = _.NewsWeight,
         PublishedAt = _.PublishedAt,
         NewsList = _.NewsList,
      });
      var newspaper = newsPapers.Where(_ => _.Id == id);
      return newspaper;
   }

   public bool IsExistNewsPaperId(int id)
   {
      if (_context.NewsPapers.Any(_=>_.Id==id))
      {
         return false;
      }

      return true;
   }

   public bool IsNewsPaperNewsWeightsFull(int id)
   {
      var newsPaper = _context.NewsPapers.First(_ => _.Id == id);
      if (newsPaper.NewsWeight!=100)
      {
         return true;
      }

      return false;
   }
}