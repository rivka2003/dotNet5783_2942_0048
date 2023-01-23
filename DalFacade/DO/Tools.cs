using System.Reflection;

namespace DO
{
    static internal class Tools
    {
        //generic ToString method thatt returns a nice string describing the recieved type +values
        internal static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in t!.GetType().GetProperties())
                str += "\n" + item.Name +
                ": " + item.GetValue(t, null);
            return str;
        }
    }
}
