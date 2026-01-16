namespace HelpdeskSystem.DTOs
{
    public class DashboardSummaryDto
    {
        public int TotalTickets { get; set; }
        public int OpenTickets { get; set; }
        public int ResolvedTickets { get; set; }
        public int TotalUsers { get; set; }
        public bool IsAdmin { get; set; }
    }
}