namespace StudentManagementWebApp.Models
{
    public class Score
    {
        #region Properties
        /// <summary>
        /// Điểm quá trình
        /// </summary>
        public double QT { get; set; }
        /// <summary>
        /// Điểm thành phần
        /// </summary>
        public double TP { get; set; }
        #endregion
        #region Constructors
        public Score() 
        {
            QT = 0;
            TP = 0;
        }
        public Score(double qT, double tP)
        {
            QT = qT;
            TP = tP;
        }
        public Score(Score x)
        {
            this.QT = x.QT;
            this.TP = x.TP;
        }
        #endregion
    }
}