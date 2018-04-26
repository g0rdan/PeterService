using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace PeterService.Droid.UITests
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            // TODO: If the Android app being tested is included in the solution then open
            // the Unit Tests window, right click Test Apps, select Add App Project
            // and select the app projects that should be tested.
            app = ConfigureApp
                .Android
                // TODO: Update this path to point to your Android app and uncomment the
                // code if the app is not included in the solution.
                //.ApkFile ("../../../Android/bin/Debug/UITestsAndroid.apk")
                .StartApp();
        }

        [Test]
        public void CheckCorrectTranslate()
        {
            app.WaitForElement(c => c.Marked("titleView").Text("Введите слово для перевода:"));
            app.EnterText(c => c.Marked("inputEditText"), "test");
            app.Tap(c => c.Marked("langFromButton"));
            app.WaitForElement(c => c.Marked("select_dialog_listview"));
            app.Tap(c => c.Marked("select_dialog_listview").Child().Index(6)); // choosing "en"
            app.Tap(c => c.Marked("langToButton"));
            app.WaitForElement(c => c.Marked("select_dialog_listview"));
            app.ScrollDown(
                b => b.Marked("select_dialog_listview"),
                ScrollStrategy.Gesture,
                swipePercentage: 0.67,
                swipeSpeed: 2000);
            app.Tap(c => c.Marked("select_dialog_listview").Child().Index(9)); // choosing "ru"
            //app.WaitForElement(c => c.Marked("translatedTextView"));
            app.Tap(c => c.Marked("translateButton"));
            app.WaitForElement(c => c.Marked("translatedTextView").Text("испытание"));
        }

        [Test]
        public void CheckIncorrectSetupTranslate()
        {
            app.WaitForElement(c => c.Marked("titleView").Text("Введите слово для перевода:"));
            app.EnterText(c => c.Marked("inputEditText"), "test");
            app.Tap(c => c.Marked("translateButton"));
            app.WaitForElement(c => c.Marked("message").Text("Не выбранно направление перевода"));
        }

        [Test]
        public void CheckEmptyTextTranslate()
        {
            app.WaitForElement(c => c.Marked("titleView").Text("Введите слово для перевода:"));
            app.EnterText(c => c.Marked("inputEditText"), string.Empty);
            app.Tap(c => c.Marked("translateButton"));
            app.WaitForElement(c => c.Marked("message").Text("Введите слово для перевода"));
        }
    }
}
