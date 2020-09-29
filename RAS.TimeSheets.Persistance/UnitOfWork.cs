using RAS.TimeSheets.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAS.TimeSheets.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {

        #region Attributes & Properties
        
        private string _PWAConnString;
       
      
        private RASContext _Context;

        public IUserRepository UserRepository
        {
            get
            {
                return new UserRepository( _PWAConnString, _Context);
            }
        }

        public ICalendarRepository CalendarRepository
        {
            get
            {
                return new CalendarRepository(_PWAConnString, _Context);
            }
        }

        public IHolidayRepository HolidayRepository
        {
            get
            {
                return new HolidayRepository( _PWAConnString, _Context);
            }
        }

        public IPWARepository PWARepository
        {
            get
            {
                return new PWARepository( _PWAConnString, _Context);
            }
        }

        public IProjectRepository ProjectRepository
        {
            get
            {
                return new ProjectRepository( _PWAConnString, _Context);
            }
        }

        public ITaskRepository TaskRepository
        {
            get
            {
                return new TaskRepository(_PWAConnString, _Context);
            }
        }

        public IAssignmentRepository AssignmentRepository
        {
            get
            {
                return new AssignmentRepository(_PWAConnString, _Context);
            }
        }

        public IOrgUnitRepository OrgUnitRepository
        {
            get
            {
                return new OrgUnitRepository(_PWAConnString, _Context);
            }
        }

        public IPresHourRepository PresHourRepository
        {
            get
            {
                return new PresHourRepository(_PWAConnString, _Context);
            }
        }

        public IWorkHourRepository WorkHourRepository
        {
            get
            {
                return new WorkHourRepository( _PWAConnString, _Context);
            }
        }

        public IDisplayPeriodRepository DisplayPeriodRepository
        {
            get
            {
                return new DisplayPeriodRepository(_PWAConnString, _Context);
            }
        }





        public IWorkflowStageRepository WorkflowStageRepository
        {
            get
            {
                return new WorkflowStageRepository( _Context);
            }
        }




        public IWorkHourHistoryRepository WorkHourHistoryRepository
        {
            get
            {
                return new WorkHourHistoryRepository(_Context);
            }
        }


        public IHourlyLeaveRepository HourlyLeavesRepository
        {
            get
            {
                return new HourlyLeaveRepository(_Context);
            }
        }





        public IDailyLeaveRepository DailyLeaveRepository
        {
            get
            {
                return new DailyLeaveRepository(_Context);
            }
        }


        public IHourlyMissionRepository HourlyMissionRepository
        {
            get
            {
                return new HourlyMissionRepository(_Context);
            }
        }
       








        #endregion

        #region Constructors

        public UnitOfWork()
        {
           
               
                _PWAConnString = ConfigurationManager.ConnectionStrings["PWAConnectionString"].ConnectionString.ToString();
               
                _Context = new RASContext();
               
          
        }

    

        #endregion

        #region Public Methods

        ///// <summary>
        ///// ایجاد مخزن داده برای مدیریت موجودیت کاربر
        ///// </summary>
        ///// <returns>بازگرداندن مخزن داده مدیریت موجودیت کاربر در قالب یک رابط تعریف شده در لایه Domain</returns>
        //public IUserRepository CreateUserRepository()
        //{
        //    IUserRepository result = new UserRepository(_WSSConnString, _PWAConnString, _Context);
        //    return result;
        //}

        ///// <summary>
        ///// ایجاد مخزن داده برای مدیریت موجودیت تقویم
        ///// </summary>
        ///// <returns>بازگرداندن مخزن داده مدیریت موجودیت تقویم در قالب یک رابط تعریف شده در لایه Domain</returns>        
        //public ICalendarRepository CreateCalendarRepository()
        //{
        //    ICalendarRepository result = new CalendarRepository(_Context);
        //    return result;
        //}

        ///// <summary>
        ///// ایجاد مخزن داده برای مدیریت موجودیت روزهای تعطیل
        ///// </summary>
        ///// <returns>بازگرداندن مخزن داده مدیریت موجودیت روزهای تعطیل در قالب یک رابط تعریف شده در لایه Domain</returns>      
        //public IHolidayRepository CreateHolidayRepository()
        //{
        //    var result = new HolidayRepository(_Context);
        //    return result;

        //}

        ///// <summary>
        ///// ایجاد مخزن داده برای مدیریت موجودیت پروژه
        ///// </summary>
        ///// <returns>بازگرداندن مخزن داده مدیریت موجودیت پروژه در قالب یک رابط تعریف شده در لایه Domain</returns>        
        //public IProjectRepository CreateProjectRepository()
        //{
        //    return new ProjectRepository(_Context);
        //}

        ///// <summary>
        ///// ایجاد مخزن داده برای مدیریت موارد مرتبط با PWA
        ///// </summary>
        ///// <returns>بازگرداندن یک مخزن داده مدیریت موارد پراجکت سرور در قالب یک رابط تعریف شده در لایه Domain</returns>
        //public IPWARepository CreatePWARepository()
        //{
        //    IPWARepository result = new PWARepository(_PWAConnString, _Context);
        //    return result;
        //}

        ///// <summary>
        ///// ایجاد مخزن داده برای مدیریت موجودیت واحد سازمانی
        ///// </summary>
        ///// <returns>بازگرداندن مخزن داده مدیریت موجودیت واحد سازمانی در قالب یک رابط تعریف شده در لایه Domain</returns>      
        //public IOrgUnitRepository CreateOrgUnitRepository()
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// ثبت تغییرات انجام شده در شئی محتوایی
        /// </summary>
        public void SaveChanges()
        {
            _Context.SaveChanges();
        }

        #endregion

        #region Public Methods were comment

        ///// <summary>
        ///// ایجاد مخزن داده برای مدیریت موجودیت کاربر
        ///// </summary>
        ///// <returns>بازگرداندن مخزن داده مدیریت موجودیت کاربر در قالب یک رابط تعریف شده در لایه Domain</returns>
        //public IUserRepository CreateUserRepository()
        //{
        //    IUserRepository result = new UserRepository(_WSSConnString, _PWAConnString, _Context);
        //    return result;
        //}

        ///// <summary>
        ///// ایجاد مخزن داده برای مدیریت موجودیت تقویم
        ///// </summary>
        ///// <returns>بازگرداندن مخزن داده مدیریت موجودیت تقویم در قالب یک رابط تعریف شده در لایه Domain</returns>        
        //public ICalendarRepository CreateCalendarRepository()
        //{
        //    ICalendarRepository result = new CalendarRepository(_Context);
        //    return result;
        //}

        ///// <summary>
        ///// ایجاد مخزن داده برای مدیریت موجودیت روزهای تعطیل
        ///// </summary>
        ///// <returns>بازگرداندن مخزن داده مدیریت موجودیت روزهای تعطیل در قالب یک رابط تعریف شده در لایه Domain</returns>      
        //public IHolidayRepository CreateHolidayRepository()
        //{
        //    var result = new HolidayRepository(_Context);
        //    return result;

        //}

        ///// <summary>
        ///// ایجاد مخزن داده برای مدیریت موجودیت پروژه
        ///// </summary>
        ///// <returns>بازگرداندن مخزن داده مدیریت موجودیت پروژه در قالب یک رابط تعریف شده در لایه Domain</returns>        
        //public IProjectRepository CreateProjectRepository()
        //{
        //    return new ProjectRepository(_Context);
        //}

        ///// <summary>
        ///// ایجاد مخزن داده برای مدیریت موارد مرتبط با PWA
        ///// </summary>
        ///// <returns>بازگرداندن یک مخزن داده مدیریت موارد پراجکت سرور در قالب یک رابط تعریف شده در لایه Domain</returns>
        //public IPWARepository CreatePWARepository()
        //{
        //    IPWARepository result = new PWARepository(_PWAConnString, _Context);
        //    return result;
        //}

        ///// <summary>
        ///// ایجاد مخزن داده برای مدیریت موجودیت واحد سازمانی
        ///// </summary>
        ///// <returns>بازگرداندن مخزن داده مدیریت موجودیت واحد سازمانی در قالب یک رابط تعریف شده در لایه Domain</returns>      
        //public IOrgUnitRepository CreateOrgUnitRepository()
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}
