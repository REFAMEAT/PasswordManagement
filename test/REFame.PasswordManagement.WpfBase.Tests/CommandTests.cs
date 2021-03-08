using NUnit.Framework;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.WpfBaseTests
{
    [TestFixture()]
    public class CommandTests
    {
        [Test]
        public void CommandExecutesCommandTest()
        {
            bool called = false;
            Command command = new Command(x => called = true);

            command.Execute(null);

            Assert.That(called, Is.True);
        }

        [Test]
        public void CommandDoesNotExecuteWhenCanNotBeExecutedTest()
        {
            var called = false;
            var canCall = false;
            var command = new Command(x => called = true, x => canCall);

            command.Execute(null);
            Assert.That(called, Is.False);
            Assert.That(command.CanExecute(null), Is.False);

            canCall = true;

            command.Execute(null);
            Assert.That(called, Is.True);
            Assert.That(command.CanExecute(null), Is.True);
        }
    }
}
