using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SysThreading = System.Threading.Tasks;
using System.Data.Entity;
using KP.TimeSheets.Domain;

namespace KP.TimeSheets.Persistance
{
    internal class RASContext : DbContext
    {


        public RASContext()
          : base("RASConnectionString")
        {

        }
        public RASContext(string ConnectionString)
        {
            this.Database.Connection.ConnectionString = ConnectionString;
        }


        private string _SchemaName = "tsm";

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Calendar> Calendars { get; set; }

        public virtual DbSet<Holiday> Holidays { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<Task> Tasks { get; set; }

        public virtual DbSet<Assignment> Assignments { get; set; }

        public virtual DbSet<OrganizationUnit> OrganizationUnits { get; set; }

        public virtual DbSet<PresenceHour> PresenceHours { get; set; }

        public virtual DbSet<WorkHour> WorkHours { get; set; }

        public virtual DbSet<DisplayPeriod> DisplayPeriods { get; set; }

        public virtual DbSet<WorkflowStage> WorkflowStages { get; set; }

        public virtual DbSet<WorkHourHistory> WorkHourHistories { get; set; }

        public virtual DbSet<HourlyLeave> HourlyLeaves { get; set; }

        public virtual DbSet<DailyLeave> DailyLeaves { get; set; }

        public virtual DbSet<HourlyMission> HourlyMissions { get; set; }

     


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            #region Configuration of Users' Table

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey<Guid>(s => s.ID);
            modelBuilder.Entity<User>().Property(s => s.ID).IsRequired();
            modelBuilder.Entity<User>().Property(s => s.UserName).IsOptional();
            modelBuilder.Entity<User>().Property(s => s.UserTitle).IsOptional();
            modelBuilder.Entity<User>().Property(s => s.Code).HasMaxLength(50);

            #endregion


            #region Configuration of Calendar' Table

            modelBuilder.Entity<Calendar>().ToTable("Calendars", _SchemaName);
            modelBuilder.Entity<Calendar>().HasKey<Guid>(s => s.ID);
            modelBuilder.Entity<Calendar>().Property(s => s.ID).IsRequired();
            modelBuilder.Entity<Calendar>().Property(s => s.Title).IsRequired();
            modelBuilder.Entity<Calendar>().Property(s => s.IsSaturdayWD).IsOptional();
            modelBuilder.Entity<Calendar>().Property(s => s.IsSundayWD).IsOptional();
            modelBuilder.Entity<Calendar>().Property(s => s.IsMondayWD).IsOptional();
            modelBuilder.Entity<Calendar>().Property(s => s.IsTuesdayWD).IsOptional();
            modelBuilder.Entity<Calendar>().Property(s => s.IsWednesdayWD).IsOptional();
            modelBuilder.Entity<Calendar>().Property(s => s.IsThursdayWD).IsOptional();
            modelBuilder.Entity<Calendar>().Property(s => s.IsFridayWD).IsOptional();
            //modelBuilder.Entity<Calendar>().Property(s => s.Created).IsOptional();


            #endregion

            #region Configuration of Holidays' Table

            modelBuilder.Entity<Holiday>().ToTable("Holidays", _SchemaName);
            modelBuilder.Entity<Holiday>().HasKey<Guid>(s => s.ID);
            modelBuilder.Entity<Holiday>().Property(s => s.Date).IsRequired();


            #endregion

            #region Configuration of Parojects' Table

            modelBuilder.Entity<Project>().ToTable("Projects");
            modelBuilder.Entity<Project>().HasKey<Guid>(p => p.ID);

            #endregion

            #region Configuration of Tasks' Table

            modelBuilder.Entity<Task>().ToTable("Tasks");
            modelBuilder.Entity<Task>().HasKey<Guid>(t => t.ID);

            #endregion

            #region Configuration of Assignments' Table

            modelBuilder.Entity<Assignment>().ToTable("Assignments");
            modelBuilder.Entity<Assignment>().HasKey<Guid>(t => t.ID);

            #endregion

            #region Configuration of OrganizationUnits' Table

            modelBuilder.Entity<OrganizationUnit>().ToTable("OrganizationUnits");
            modelBuilder.Entity<OrganizationUnit>().HasKey<Guid>(ou => ou.ID);
            modelBuilder.Entity<OrganizationUnit>().HasOptional(ou => ou.Manager);

            #endregion

            #region Configuration of PresenceHours' Table

            modelBuilder.Entity<PresenceHour>().ToTable("PresenceHours", _SchemaName);
            modelBuilder.Entity<PresenceHour>().HasKey<Guid>(x => x.ID);
            modelBuilder.Entity<PresenceHour>().Property(ph => ph.InTime).IsOptional();
            modelBuilder.Entity<PresenceHour>().Property(ph => ph.OutTime).IsOptional();

            #endregion

            #region Configuration of WorkHours' Table

            modelBuilder.Entity<WorkHour>().ToTable("WorkHours", _SchemaName);
            modelBuilder.Entity<WorkHour>().HasKey<Guid>(wh => wh.ID);
            modelBuilder.Entity<WorkHour>().Property(wh => wh.Description).IsOptional();


            #endregion

            #region Configuration of History' Table
            modelBuilder.Entity<WorkHourHistory>().ToTable("WorkHourHistories", _SchemaName);
            modelBuilder.Entity<WorkHourHistory>().HasKey<Guid>(p => p.ID);
            #endregion



            #region Configuration of DisplayPeriods' Table
            modelBuilder.Entity<DisplayPeriod>().ToTable("DisplayPeriods", _SchemaName);
            modelBuilder.Entity<DisplayPeriod>().HasKey<Guid>(dp => dp.ID);
            modelBuilder.Entity<DisplayPeriod>().Property(dp => dp.IsWeekly).HasColumnAnnotation("DefaultValue", true);
            modelBuilder.Entity<DisplayPeriod>().Property(dp => dp.NumOfDays).HasColumnAnnotation("DefaultValue", 7);

            #endregion

            #region Configuration of WorkflowStages' Table
            modelBuilder.Entity<WorkflowStage>().ToTable("WorkflowStages", _SchemaName);
            modelBuilder.Entity<WorkflowStage>().HasKey<Guid>(ws => ws.ID);

            #endregion

            #region Configuration of Relations between tables

            modelBuilder.Entity<User>()
                .HasMany(x => x.Organisations).
                WithOptional(y => y.Manager)
                .HasForeignKey(m => m.ManagerID);


            modelBuilder.Entity<User>()
               .HasMany(x => x.WorkHourHistories).
               WithOptional(y => y.Manager)
               .HasForeignKey(m => m.ManagerID);

            modelBuilder.Entity<WorkflowStage>()
              .HasMany(x => x.WorkHourHistories).
              WithOptional(y => y.Stage)
              .HasForeignKey(m => m.StageID);

            modelBuilder.Entity<WorkHour>()
              .HasMany(x => x.WorkHourHistories).
              WithOptional(y => y.WorkHuor)
              .HasForeignKey(m => m.WorkHourID);


            modelBuilder.Entity<OrganizationUnit>()
                .HasMany(x => x.Employees)
                .WithOptional(y => y.OrganizationUnit)
                .HasForeignKey(m => m.OrganizationUnitID);


            //Calendars to Holidays(one-many)
            //ارتباط یک به چند میان جدول تقویم و روزهای تعطیل آن
            modelBuilder.Entity<Calendar>()
                .HasMany(c => c.Holidays)
                .WithRequired(h => h.Calendar)
                .HasForeignKey(h => h.CalendarID);


            //Calendars to Projects(one-many)
            //ارتباط یک به چند میان تقویم و پروژه
            modelBuilder.Entity<Calendar>()
                .HasMany(c => c.Projects)
                .WithOptional(p => p.Calendar)
                .HasForeignKey(p => p.CalendarID);

            //Users to Projects(one-many)
            //ارتباط یک به چند میان کاربر و پروژه
            modelBuilder.Entity<User>()
                .HasMany(u => u.Projects)
                .WithOptional(p => p.Owner)
                .HasForeignKey(p => p.OwnerID);

            //Projects to Tasks(one-many)
            //ارتباط یک به چند میان پروژه و کاربر
            modelBuilder.Entity<Project>()
                .HasMany(u => u.Tasks)
                .WithOptional(p => p.Project)
                .HasForeignKey(p => p.ProjectID);




            //Tasks to itself(one-many)
            //ارتباط یک به چند میان فعالیت با خودش
            modelBuilder.Entity<Task>()
               .HasMany(t => t.Childs)
               .WithOptional(t => t.ParentTask)
               .HasForeignKey(t => t.ParentTaskID);

            //Tasks to Assignments(one-many)
            //ارتباط یک به چند میان فعالیت با انتساب
            modelBuilder.Entity<Task>()
               .HasMany(t => t.Assignments)
               .WithRequired(a => a.Task)
               .HasForeignKey(a => a.TaskID);


            //Tasks to Assignments(one-many)
            //ارتباط یک به چند میان فعالیت با انتساب
            modelBuilder.Entity<Project>()
               .HasMany(t => t.WorkHours)
               .WithRequired(a => a.Project)
               .HasForeignKey(a => a.ProjectId);


            //OrganizationUnits to Users(one-many)
            //ارتباط یک به چند میان واحد سازمانی و کاربر
            modelBuilder.Entity<OrganizationUnit>()
                .HasMany(ou => ou.Employees)
                .WithOptional(em => em.OrganizationUnit)
                .HasForeignKey(em => em.OrganizationUnitID);





            //Users to PresenceHours(one-many)
            //ارتباط یک به چند میان کاربر و ساعات حضور
            modelBuilder.Entity<User>()
                .HasMany(u => u.PresenceHours)
                .WithRequired(em => em.Employee)
                .HasForeignKey(em => em.EmployeeID);


            

            //Users to WorkHours(one-many)
            //ارتباط یک به چند میان کاربر و ساعات کاری
            modelBuilder.Entity<User>()
                .HasMany(u => u.WorkHours)
                .WithRequired(em => em.Employee)
                .HasForeignKey(em => em.EmployeeID);

            //Users to Assignments(one-many)
            //ارتباط یک به چند میان کاربر با انتساب
            modelBuilder.Entity<User>()
               .HasMany(t => t.Assignments)
               .WithRequired(a => a.Resource)
               .HasForeignKey(a => a.ResourceID);

            //Tasks to WorkHours(one-many)
            //ارتباط یک به چند میان فعالیت و ساعات کاری
            modelBuilder.Entity<Task>()
                .HasMany(t => t.WorkHours)
                .WithRequired(wh => wh.Task)
                .HasForeignKey(wh => wh.TaskID);

            //Users to DisplayPeriods(one-many)
            //ارتباط یک به چند میان کاربر و دوره نمایش
            modelBuilder.Entity<User>()
                .HasMany(t => t.DisplayPeriods)
                .WithRequired(wh => wh.Employee)
                .HasForeignKey(wh => wh.EmployeeID);

            //WorkflowStages to WorkHours(one-many)
            //ارتباط یک به چند میان مرحله گردش کار و ساعت کارکرد
            modelBuilder.Entity<WorkflowStage>()
                .HasMany(wh => wh.WorkHours)
                .WithRequired(u => u.WorkflowStage)
                .HasForeignKey(u => u.WorkflowStageID);

            #endregion

        }



    }
}
