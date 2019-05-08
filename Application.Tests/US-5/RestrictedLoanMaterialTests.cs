using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Application.UseCases.Loans.Commands.CreateLoan;
using GTL.Domain.Entities;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class RestrictedLoanMaterialTests
    {
        private readonly Mock<ILoanRepository> _loanRepo;
        private readonly Mock<IMemberRepository> _memberRepo;
        private readonly Mock<ICopyRepository> _copyRepo;
        private readonly Mock<ILibraryRepository> _libraryRepo;
        private readonly CreateLoanCommand _command;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IGTLContext> _context;

        public RestrictedLoanMaterialTests()
        {
            _loanRepo = new Mock<ILoanRepository>();
            _memberRepo = new Mock<IMemberRepository>();
            _copyRepo = new Mock<ICopyRepository>();
            _libraryRepo = new Mock<ILibraryRepository>();
            _command = new Mock<CreateLoanCommand>().Object;
            _context = new Mock<IGTLContext>();
            _uow = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task ReturnsErrorIfNotAllowedForLoan()
        {
            _context.Setup(x => x.CreateUnitOfWork()).Returns(_uow.Object);

            _loanRepo.Setup(x => x.Add(It.IsAny<Loan>())).Throws(new NotAllowedForLoan(It.IsAny<string>()));

            var sut = new CreateLoanHandler(_context.Object, _loanRepo.Object, _memberRepo.Object, _copyRepo.Object, _libraryRepo.Object);

            //Act
            var response = await sut.Handle(_command, default);

            Assert.True(response.HasRequestError);
        }
    }
}
