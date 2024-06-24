using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        public IFileTypeRepository FileTypeRepository { get; }

        public IFileInformationRepository FileInformationRepository { get; }

        public IAppUserRepository AppUserRepository { get; }

        public IFileManager FileManager { get; }

        public Task SaveAllAsync();
    }
}
