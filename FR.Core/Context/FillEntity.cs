namespace FR.Core
{
    public sealed class FillEntity
    {
        public static IFillEntity Instance
        {
            get { return SingletonCreator.instance; }
        }

        class SingletonCreator
        {
            internal static string[] assName = ComConfig.AppSettings["FR.Core.FillEntity"].Split(',');

            internal static readonly IFillEntity instance =
                InstanceBase.CreateInstance<IFillEntity>(assName[0], assName[1], assName[2]);

            //Config配置:
            //<add key="XUL.SystemCloud.Core.FillEntity" value="XUL.SystemCloud.Core,XUL.SystemCloud.Core,XUL.SystemCloud.Core.FillEntity" />
        }
    }
}
