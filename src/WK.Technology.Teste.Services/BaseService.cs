using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;

namespace WK.Technology.Teste.Services
{
    public abstract class BaseService
    {
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected BaseService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
    }
}
