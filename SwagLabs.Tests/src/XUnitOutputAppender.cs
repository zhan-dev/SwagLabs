using log4net.Appender;
using log4net.Core;
using Xunit.Abstractions;

namespace SwagLabs.Tests.src
{
    internal class XUnitOutputAppender : AppenderSkeleton
    {
        private readonly ITestOutputHelper output;

        public XUnitOutputAppender(ITestOutputHelper output)
        {
            this.output = output;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            var message = RenderLoggingEvent(loggingEvent);
            output.WriteLine(message);
        }
    }
}
