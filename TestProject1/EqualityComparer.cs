using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class FileInformationEqualityComparer : IEqualityComparer<FileInformation>
    {
        public bool Equals(FileInformation? x, FileInformation? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.Description == y.Description
                && x.Size == y.Size
                && x.Path == y.Path
                && x.AccessLevel == y.AccessLevel
                && x.CreationDate == y.CreationDate
                && x.CreatorId == y.CreatorId
                && x.FileTypeId == y.FileTypeId;
        }

        public int GetHashCode([DisallowNull] FileInformation obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class FileTypeEqualityComparer : IEqualityComparer<FileType>
    {
        public bool Equals(FileType? x, FileType? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Extension == y.Extension
                && x.MIMEType == y.MIMEType;
        }

        public int GetHashCode([DisallowNull] FileType obj)
        {
            return obj.GetHashCode();
        }
    }
}