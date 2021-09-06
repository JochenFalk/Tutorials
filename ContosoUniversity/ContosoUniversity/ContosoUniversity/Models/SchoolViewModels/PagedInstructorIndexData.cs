using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models.SchoolViewModels
{
    public class PagedInstructorIndexData<Instructor> : IEnumerable<Instructor>
    {
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }

        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int? Instructorid { get; private set; }

        public PagedInstructorIndexData(IEnumerable<Instructor> items, int count, int pageIndex, int pageSize, int? id)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Instructors = items;
            Instructorid = id;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1 && Instructorid == null);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages && Instructorid == null);
            }
        }

        public static PagedInstructorIndexData<Instructor> Create(IEnumerable<Instructor> source, int pageIndex, int pageSize, int? id)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            
            return new PagedInstructorIndexData<Instructor>(items, count, pageIndex, pageSize, id);
        }

        public IEnumerator<Instructor> GetEnumerator()
        {
            return Instructors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Instructors).GetEnumerator();
        }
    }
}