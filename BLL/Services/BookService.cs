using AutoMapper;
using BLL.Interface;
using DAL.Interface;
using DAL.ViewModel;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<BookViewModel>> GetAllBooks()
        {
            var booklist = await _unitOfWork.GetRepository<Book>().GetAllByPropertyAsync(includeProperties: "BookCates.Category");
            var result = _mapper.Map<List<BookViewModel>>(booklist);
            return result;
        }

        public async Task<BookViewModel> GetBookById(int id)
        {
            var book = await _unitOfWork.GetRepository<Book>().GetByPropertyAsync(b=>b.Id == id,includeProperties: "BookCates.Category");
            if (book == null) return null;
            var result = _mapper.Map<BookViewModel>(book);
            return result;
        }
    }
}
