namespace configuracion_ms.Controllers
{
    public class DataTime
    {
        public static String GetGTM5()
        {
            TimeZoneInfo gtm5 = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime gtm5Now = TimeZoneInfo.ConvertTime(DateTime.Now, gtm5);
            return gtm5Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
