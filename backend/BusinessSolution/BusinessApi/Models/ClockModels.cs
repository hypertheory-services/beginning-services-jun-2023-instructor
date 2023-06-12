namespace BusinessApi.Models;

public record GetClockResponse(bool IsOpen, DateTime? NextOpenTime);