namespace StudentManagementWebApp.Models
{
    public class Result
    {
        #region Properties
        /// <summary>
        /// Chi tiết môn học
        /// </summary>
        public virtual Subject SubjectDetail { get; set; }
        /// <summary>
        /// Chi tiết điểm
        /// </summary>
        public virtual Score ScoreDetail { get; set; }

        #endregion
        #region Constructors
        public Result(Subject subjectDetail, Score scoreDetail)
        {
            SubjectDetail = subjectDetail;
            ScoreDetail = scoreDetail;
        }
        #endregion
    }
}