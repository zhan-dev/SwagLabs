using Xunit.Abstractions;

namespace SwagLabs.Tests.src
{
    internal class TestLogger
    {
        private readonly ITestOutputHelper output;
        public TestLogger(ITestOutputHelper output)
        {
            this.output = output;
        }

        public void Info(string message)
        {
            this.output.WriteLine($"[INFO] {message}");
        }

        public void Step(string message)
        {
            this.output.WriteLine($"[STEP] {message}");
        }

        public void Result(string message)
        {
            this.output.WriteLine($"[RESULT] {message}");
        }
    }
}
