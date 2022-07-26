namespace StudentManagementWebApp.Models
{
    public class Subject
    {
        #region Properties
        /// <summary>
        /// Mã môn học
        /// </summary>
        public virtual string SubjectId { get; set; }
        /// <summary>
        /// Tên môn học
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// Số tiết học
        /// </summary>
        public virtual int NumOfLessons { get; set; }

        #endregion
        #region Constructors
        public Subject() { }
        public Subject(string subjectid, string name, int numOfLessons)
        {
            SubjectId = subjectid;
            Name = name;
            NumOfLessons = numOfLessons;
        }
        public Subject(Subject x)
        {
            this.SubjectId = x.SubjectId;
            this.Name = x.Name;
            this.NumOfLessons = x.NumOfLessons;
        }
        #endregion
    }
}