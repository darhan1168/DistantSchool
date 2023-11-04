using DistantSchool.Models;

namespace DistantSchool.ViewModels;

public class LessonViewModel
{
    public int LessonId { get; set; }

    public int ClassId { get; set; }

    public int TeacherId { get; set; }

    public int SubjectId { get; set; }
    public int TeacherClassSubjectId { get; set; }

    public DateTime Date { get; set; }

    public string URL { get; set; }
    
    public List<Class> Classes { get; set; }

    public List<Subject> Subjects { get; set; }
    public List<TeachersClassesSubject> TeachersClassesSubjects { get; set; }
}
