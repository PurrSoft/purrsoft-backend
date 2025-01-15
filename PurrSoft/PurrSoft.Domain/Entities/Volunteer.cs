using PurrSoft.Domain.Entities.Enums;
using System;
using System.Collections.Generic;

namespace PurrSoft.Domain.Entities;

public class Volunteer
{
	public string UserId { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public VolunteerStatus Status { get; set; }
	public TierLevel Tier { get; set; }
	public DateTime? LastShiftDate { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public IList<string>? Tasks { get; set; }
    public string AvailableHours { get; set; }
    public DateTime? TrainingStartDate { get; set; }
    public virtual ApplicationUser? Supervisor { get; set; }
    public virtual IList<ApplicationUser>? Trainers { get; set; }
    public virtual ApplicationUser User { get; set; }
	public virtual IList<Shift> Shifts { get; set; }
	// public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
	public virtual ICollection<EventVolunteerMap>? EventVolunteerMaps { get; set; }
	public Volunteer()
	{
		Tasks = new List<string>();
		Shifts = new List<Shift>();
        Trainers = new List<ApplicationUser>();
        // LeaveRequests = new List<LeaveRequest>();
    }
}