using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;
using System.Diagnostics;

namespace ContosoUniversity.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly SchoolContext _context;

        public InstructorsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Instructors
        public async Task<IActionResult> Index(
            int? id,
            int? courseID,
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["LastNameSortParm"] = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewData["FirstNameSortParm"] = sortOrder == "FirstMidName" ? "FirstMidName_desc" : "FirstMidName";
            ViewData["EmailAddressSortParm"] = sortOrder == "EmailAddress" ? "EmailAddress_desc" : "EmailAddress";
            ViewData["OfficeSortParm"] = sortOrder == "Location" ? "Location_desc" : "Location";
            ViewData["CourseSortParm"] = sortOrder == "Title" ? "Title_desc" : "Title";
            ViewData["DateSortParm"] = sortOrder == "HireDate" ? "HireDate_desc" : "HireDate";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var instructors = new List<Instructor>();

            bool officeAssignment = false;
            bool courseAssignment = false;
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "LastName";
            }
            else if (sortOrder.Equals("Location_desc") || sortOrder.Equals("Location"))
            {
                officeAssignment = true;
            }
            else if (sortOrder.Equals("Title_desc") || sortOrder.Equals("Title"))
            {
                courseAssignment = true;
            }

            Debug.WriteLine(sortOrder);

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                if (descending)
                {
                    if (officeAssignment)
                    {
                        instructors = await _context.Instructors
                        .Include(i => i.OfficeAssignment)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Enrollments)
                                .ThenInclude(i => i.Student)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Department)
                        .Where(s => s.LastName.Contains(searchString) || s.FirstMidName.Contains(searchString))
                        .OrderByDescending(e => EF.Property<object>(e.OfficeAssignment, sortOrder))
                        .ToListAsync();
                    }
                    else if (courseAssignment)
                    {
                        instructors = await _context.Instructors
                        .Include(i => i.OfficeAssignment)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Enrollments)
                                .ThenInclude(i => i.Student)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Department)
                        .Where(s => s.LastName.Contains(searchString) || s.FirstMidName.Contains(searchString))
                        .ToListAsync();

                        instructors.OrderByDescending(e => EF.Property<object>(e.CourseAssignments.AsQueryable().Include(i => i.Course), sortOrder));
                    }
                    else
                    {
                        instructors = await _context.Instructors
                        .Include(i => i.OfficeAssignment)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Enrollments)
                                .ThenInclude(i => i.Student)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Department)
                        .Where(s => s.LastName.Contains(searchString) || s.FirstMidName.Contains(searchString))
                        .OrderByDescending(e => EF.Property<object>(e, sortOrder))
                        .ToListAsync();
                    }
                }
                else
                {
                    if (officeAssignment)
                    {
                        instructors = await _context.Instructors
                        .Include(i => i.OfficeAssignment)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Enrollments)
                                .ThenInclude(i => i.Student)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Department)
                        .Where(s => s.LastName.Contains(searchString) || s.FirstMidName.Contains(searchString))
                        .OrderBy(e => EF.Property<object>(e.OfficeAssignment, sortOrder))
                        .ToListAsync();
                    }
                    else if (courseAssignment)
                    {
                        instructors = await _context.Instructors
                        .Include(i => i.OfficeAssignment)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Enrollments)
                                .ThenInclude(i => i.Student)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Department)
                        .Where(s => s.LastName.Contains(searchString) || s.FirstMidName.Contains(searchString))
                        .ToListAsync();

                        instructors.OrderBy(e => EF.Property<object>(e.CourseAssignments.AsQueryable().Include(i => i.Course), sortOrder));
                    }
                    else
                    {
                        instructors = await _context.Instructors
                        .Include(i => i.OfficeAssignment)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Enrollments)
                                .ThenInclude(i => i.Student)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Department)
                        .Where(s => s.LastName.Contains(searchString) || s.FirstMidName.Contains(searchString))
                        .OrderBy(e => EF.Property<object>(e, sortOrder))
                        .ToListAsync();
                    }
                }
            }
            else
            {
                if (descending)
                {
                    if (officeAssignment)
                    {
                        instructors = await _context.Instructors
                        .Include(i => i.OfficeAssignment)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Enrollments)
                                .ThenInclude(i => i.Student)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Department)
                        .OrderByDescending(e => EF.Property<object>(e.OfficeAssignment, sortOrder))
                        .ToListAsync();
                    }
                    else if (courseAssignment)
                    {
                        instructors = await _context.Instructors
                            .Include(i => i.OfficeAssignment)
                            .Include(i => i.CourseAssignments)
                            .ThenInclude(i => i.Course)
                                .ThenInclude(i => i.Enrollments)
                                    .ThenInclude(i => i.Student)
                            .Include(i => i.CourseAssignments)
                            .ThenInclude(i => i.Course)
                                .ThenInclude(i => i.Department)
                            .Include(i => i.CourseAssignments)
                            .ThenInclude(i => i.Course)
                            .ToListAsync();

                        instructors.OrderByDescending(e => EF.Property<object>(e.CourseAssignments.AsQueryable().Include(i => i.Course), sortOrder));
                    }
                    else
                    {
                        instructors = await _context.Instructors
                        .Include(i => i.OfficeAssignment)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Enrollments)
                                .ThenInclude(i => i.Student)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Department)
                        .OrderByDescending(e => EF.Property<object>(e, sortOrder))
                        .ToListAsync();
                    }
                }
                else
                {
                    if (officeAssignment)
                    {
                        instructors = await _context.Instructors
                        .Include(i => i.OfficeAssignment)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Enrollments)
                                .ThenInclude(i => i.Student)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Department)
                        .OrderBy(e => EF.Property<object>(e.OfficeAssignment, sortOrder))
                        .ToListAsync();
                    }
                    else if (courseAssignment)
                    {
                        instructors = await _context.Instructors
                        .Include(i => i.OfficeAssignment)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Enrollments)
                                .ThenInclude(i => i.Student)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Department)
                        .Include(i => i.CourseAssignments)
                            .ThenInclude(i => i.Course)
                        .ToListAsync();

                        instructors.OrderBy(e => EF.Property<object>(e.CourseAssignments.AsQueryable().Include(i => i.Course), sortOrder));
                    }
                    else
                    {
                        instructors = await _context.Instructors
                        .Include(i => i.OfficeAssignment)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Enrollments)
                                .ThenInclude(i => i.Student)
                        .Include(i => i.CourseAssignments)
                        .ThenInclude(i => i.Course)
                            .ThenInclude(i => i.Department)
                        .OrderBy(e => EF.Property<object>(e, sortOrder))
                        .ToListAsync();
                    }
                }
            }

            int pageSize = 3;
            var viewModel = PagedInstructorIndexData<Instructor>.Create(instructors, pageNumber ?? 1, pageSize, id);

            if (id != null && id != 0)
            {
                ViewData["InstructorID"] = id.Value;
                Instructor instructor = viewModel.Instructors.Where(
                    i => i.ID == id.Value).Single();
                viewModel.Courses = instructor.CourseAssignments.Select(s => s.Course);
            }

            if (courseID != null && courseID != 0)
            {
                ViewData["CourseID"] = courseID.Value;
                var selectedCourse = viewModel.Courses.Where(x => x.CourseID == courseID).Single();
                await _context.Entry(selectedCourse).Collection(x => x.Enrollments).LoadAsync();
                foreach (Enrollment enrollment in selectedCourse.Enrollments)
                {
                    await _context.Entry(enrollment).Reference(x => x.Student).LoadAsync();
                }
                viewModel.Enrollments = selectedCourse.Enrollments;
            }

            return View(viewModel);
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName,HireDate")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstMidName,HireDate")] Instructor instructor)
        {
            if (id != instructor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.ID == id);
        }
    }
}
