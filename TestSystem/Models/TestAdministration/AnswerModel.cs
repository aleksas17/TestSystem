namespace TestSystem.Models.TestAdministration
{
    public class AnswerModel
    {
        /// <summary>
        /// Answer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Shows if answer is correct (0/1)
        /// </summary>
        public int IsCorrect { get; set; }
    }
}