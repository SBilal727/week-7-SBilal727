namespace ClassSchedule.Models
{

    // Code the class to implement the interface
    public class ClassScheduleUnitOfWork : IClassScheduleUnitOfWork
    {

        //  a private ClassScheduleContext object

        private ClassScheduleContext context;

        // Create private fields for repositories

        private IRepository<Class>? classData;
        private IRepository<Teacher>? teacherData;
        private IRepository<Day>? dayData;

        //  Create constructor 
        public ClassScheduleUnitOfWork(ClassScheduleContext ctx)
        {
            context = ctx;
        }

        // initialize and return Repository objects

        // Classes

        public IRepository<Class> Classes
        {
            get
            {
                if (classData == null)
                    classData = new Repository<Class>(context);

                return classData;
            }
        }
        // Days

        public IRepository<Day> Days
        {
            get
            {
                if (dayData == null)
                    dayData = new Repository<Day>(context);

                return dayData;
            }
        }

        //Teachers

        public IRepository<Teacher> Teachers
        {
            get
            {
                if (teacherData == null)
                    teacherData = new Repository<Teacher>(context);

                return teacherData;
            }
        }
        // call the context object’s SaveChanges() method in the Save() method
        public void Save() => context.SaveChanges();

    }
}
