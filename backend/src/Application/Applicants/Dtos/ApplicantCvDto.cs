namespace Application.Common.Files.Dtos
{
    public class ApplicantCvDto
    {
        public string Id { get; }
        public string Name { get; }
        public string Url { get; }

        public ApplicantCvDto(string id, string url, string name)
        {
            Id = id;
            Url = url;
            Name = name;
        }
    }
}
