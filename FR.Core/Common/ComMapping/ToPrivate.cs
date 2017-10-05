using System.Reflection;

namespace FR.Core.ComMapping
{
    public class ToPrivate
    {
        public static FieldInfo GetFieldInfo(object obj, string name)
        {
            var field = obj.GetType().GetField(name
                , BindingFlags.Instance | BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.ExactBinding);

            return field;
        }

        public static void SetFieldValue(object obj, string name, object val)
        {
            var field = GetFieldInfo(obj, name);

            field.SetValue(obj, val);
        }

        public static object GetFieldValue(object obj, string name)
        {
            var field = GetFieldInfo(obj, name);

            if (field == null)
                return null;

            return field.GetValue(obj);
        }
    }
}
