using System.Globalization;

using Money.Extensions;

namespace Money.Tests;

[TestFixture]
public class DateTimeExtensionsTests
{
    [TestCase("2023.02.06", "2023.02.06", "2023.02.12")]
    [TestCase("2023.02.07", "2023.02.06", "2023.02.12")]
    [TestCase("2023.02.08", "2023.02.06", "2023.02.12")]
    [TestCase("2023.02.09", "2023.02.06", "2023.02.12")]
    [TestCase("2023.02.10", "2023.02.06", "2023.02.12")]
    [TestCase("2023.02.11", "2023.02.06", "2023.02.12")]
    [TestCase("2023.02.12", "2023.02.06", "2023.02.12")]
    public void TestGetWeekDays_HU(string input, string expectedStart, string expcectedEnd)
    {
        (DateOnly firstDay, DateOnly lastDay) = DateTime.Parse(input).GetWeekDays(new CultureInfo("Hu-hu"));
        DateOnly start = DateOnly.Parse(expectedStart);
        DateOnly end = DateOnly.Parse(expcectedEnd);

        Assert.Multiple(() =>
        {
            Assert.That(firstDay, Is.EqualTo(start));
            Assert.That(lastDay, Is.EqualTo(end));
        });
    }

    [TestCase("2023.02.05", "2023.02.05", "2023.02.11")]
    [TestCase("2023.02.06", "2023.02.05", "2023.02.11")]
    [TestCase("2023.02.07", "2023.02.05", "2023.02.11")]
    [TestCase("2023.02.08", "2023.02.05", "2023.02.11")]
    [TestCase("2023.02.09", "2023.02.05", "2023.02.11")]
    [TestCase("2023.02.10", "2023.02.05", "2023.02.11")]
    [TestCase("2023.02.11", "2023.02.05", "2023.02.11")]
    public void TestGetWeekDays_USA(string input, string expectedStart, string expcectedEnd)
    {
        (DateOnly firstDay, DateOnly lastDay) = DateTime.Parse(input).GetWeekDays(new CultureInfo("En-us"));
        DateOnly start = DateOnly.Parse(expectedStart);
        DateOnly end = DateOnly.Parse(expcectedEnd);

        Assert.Multiple(() =>
        {
            Assert.That(firstDay, Is.EqualTo(start));
            Assert.That(lastDay, Is.EqualTo(end));
        });
    }
}
