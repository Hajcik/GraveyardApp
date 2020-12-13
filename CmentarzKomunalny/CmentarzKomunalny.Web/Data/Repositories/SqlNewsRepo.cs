using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.Models.Cmentarz;
using CmentarzKomunalny.Web.Data.Contexts;
using System.Collections.Generic;
using System.Linq;
using System;


namespace CmentarzKomunalny.Web.Data.Repositories
{
    public class SqlNewsRepo : INewsRepo
    {
        private readonly CmentarzContext _context;
        public SqlNewsRepo(CmentarzContext context)
        {
            _context = context;
        }
        public void AddNews(News news)
        {
            if (news == null)
                throw new ArgumentException(nameof(news));

            _context.News.Add(news);
        }

        public void DeleteNews(News news)
        {
            if (news == null)
                throw new ArgumentException(nameof(news));

            _context.News.Remove(news);
        }

        public IEnumerable<News> GetAllNews()
        {
            return _context.News.ToList();
        }

        public News GetNewsById(int id)
        {
            return _context.News.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateNews(News news)
        {
           
        }
    }
}
