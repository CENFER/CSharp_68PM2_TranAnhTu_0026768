namespace QuanLySV
{
    public partial class databaseDataContext
    {
        public databaseDataContext()
            : this(System.Configuration.ConfigurationManager.ConnectionStrings["QLSVConnectionString"].ConnectionString)
        {
        }
    }
}
