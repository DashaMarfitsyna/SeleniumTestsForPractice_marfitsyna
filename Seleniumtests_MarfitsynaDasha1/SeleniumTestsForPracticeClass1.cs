using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Seleniumtests_MarfitsynaDasha1;

public class SeleniumTestsForPracticeClass1
{
    public ChromeDriver driver;

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
        driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        Autorization();
    }

    public void Profile()
    {
        var avatar = driver.FindElement(By.CssSelector("[data-tid='Avatar']"));
        avatar.Click();
        var profile = driver.FindElement(By.CssSelector("[data-tid='Profile']"));
        profile.Click();
    }
    
    [Test]
    public void Authorization()
    {
        var news = driver.FindElement(By.CssSelector("[data-tid='Title']"));
        var currentUrl = driver.Url;
        //currentUrl.Should().Be("https://staff-testing.testkontur.ru/news");
        Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news", 
            "мы ожидали получить https://staff-testing.testkontur.ru/news, а получили " + currentUrl);
    }

    [Test]
    public void NavigationTest()
    {
        var community = driver.FindElements(By.CssSelector("[data-tid='Community']"))
            .First(element => element.Displayed);
        community.Click();
        var communityTitle = driver.FindElement(By.CssSelector("[data-tid='Title']"));
        Assert.That(driver.Url == "https://staff-testing.testkontur.ru/communities"
            , "Ожидали получить https://staff-testing.testkontur.ru/communities, а получили " + driver.Url);
    }

    public void Autorization()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("marfitsyna@skbkontur.ru");
        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("605478152Qq_");
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
    }

    [Test]
    public void GoToTheEditProfilePage()
    {
        Profile();
        var pencil = driver.FindElement(By.CssSelector("[class='sc-juXuNZ eiJUrb']"));
        pencil.Click();
        var editprofile = driver.FindElement(By.CssSelector("[data-tid='Title']"));
        driver.Url.Should().Be("https://staff-testing.testkontur.ru/profile/settings/edit");
    }

    [Test]
    public void CancelProfileEditing()
    {
        Profile();
        var pencil = driver.FindElement(By.CssSelector("[class='sc-juXuNZ eiJUrb']"));
        pencil.Click();
        var cancel = driver.FindElement(By.CssSelector("[class='sc-juXuNZ kVHSha']"));
        cancel.Click();
        var employeename = driver.FindElement(By.CssSelector("[data-tid='EmployeeName']"));
        Assert.That(driver.Url == "https://staff-testing.testkontur.ru/profile/92c373db-afa7-499f-81a4-36e686641db5"
            , "Ожидали получить https://staff-testing.testkontur.ru/profile/92c373db-afa7-499f-81a4-36e686641db5, а получили " 
            + driver.Url);
    }

    [Test]
    public void LogoutFromAccount()
    {
        var avatar = driver.FindElement(By.CssSelector("[data-tid='Avatar']"));
        avatar.Click();
        var logout = driver.FindElement(By.CssSelector("[data-tid='Logout']"));
        logout.Click();
        var portal = driver.FindElement(By.CssSelector("[class='PostLogoutRedirectUri']"));
        portal.Click();
        var login = driver.FindElement(By.Id("Username"));
        //это такой ассерт - проверка наличия элемента на странице. если он есть, значит мы попали куда нужно
        //к урлу не привязалась, т.к. он динамический
    }

    [TearDown]
    public void TearDown()
    {
        driver.Close();
        driver.Quit();
    }
    
}