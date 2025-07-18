﻿using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IBookService
    {
        Task<List<BookViewModel>> GetAllBooks();
        Task<BookViewModel> GetBookById (int id);
    }
}
