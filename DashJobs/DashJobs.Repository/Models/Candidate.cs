namespace Dashclass.Model
{
    public class Candidate
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Seniority { get; init; }

        public Guid VacancyId { get; init; }

        public string Specialty { get; init; }
    }
}
