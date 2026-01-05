using System;
using System.Collections.Generic;
using SchoolAs.Domain.Common;
using SchoolAs.Domain.Enums;

namespace SchoolAs.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public CourseStatus Status { get; set; } = CourseStatus.Draft;
        
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
