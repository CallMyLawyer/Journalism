using Journalism.Entites.NewsPapers;
using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.NewsPapers.Contracts.Dtos;

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
         Category = _.Category,
         PublishedAt = _.PublishedAt,
         NewsList = _.NewsList,
      }).ToList();
      return newsPapers;
   }

   public void Publish(NewsPaper newsPaper)
   {
      
   }
}