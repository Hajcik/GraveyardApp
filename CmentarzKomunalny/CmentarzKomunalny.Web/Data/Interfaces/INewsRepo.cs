using CmentarzKomunalny.Web.Models.Cmentarz;
using System;
using System.Collections.Generic;

namespace CmentarzKomunalny.Web.Data.Interfaces
{
    public interface INewsRepo
    {
        News GetNewsById(int id); // used to get each news in each window
        IEnumerable<News> GetAllNews();
        void AddNews(News news);
        void DeleteNews(News news);
        void UpdateNews(News news);
        bool SaveChanges();
    }
}
