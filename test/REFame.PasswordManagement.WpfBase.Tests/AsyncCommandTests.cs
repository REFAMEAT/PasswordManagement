using System.Threading.Tasks;
using NUnit.Framework;

namespace REFame.PasswordManagement.WpfBase.Tests
{
    public class AsyncCommandTests
    {
        [Test]
        public void CommandExecutesCommandTest()
        {
            bool called = false;
            var command = new AsyncCommand(async x => called = true);

            command.Execute(null);

            Assert.That(called, Is.True);
        }

        [Test]
        public void CommandDoesNotExecuteWhenCanNotBeExecutedTest()
        {
            var called = false;
            var canCall = false;
            var command = new AsyncCommand(async () => called = true, () => canCall);

            command.Execute(null);
            Assert.That(called, Is.False);
            Assert.That(command.CanExecute(null), Is.False);

            canCall = true;

            command.Execute(null);
            Assert.That(called, Is.True);
            Assert.That(command.CanExecute(null), Is.True);
        }

        [Test]
        public void CanExecuteReturnsFalseWhenTaskIsRunning()
        {
            var command = new AsyncCommand(async () => await Task.Delay(500));

            command.Execute(null);

            Assert.That(command.CanExecute(null), Is.False);
        }
    }
}