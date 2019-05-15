using System;
using System.Collections.Generic;
using System.Text;
using GTL.Domain.Entities;

namespace GTL.Application.Interfaces.Repositories
{
    public interface IMemberRepository
    {
        Member GetByLoanerCard(string barcode);
    }
}
