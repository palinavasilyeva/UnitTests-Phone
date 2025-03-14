using ClassLibrary;

namespace TestProjectPhone
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Konstruktor_DaniePoprawnie_OK()
        {
            var wlasciciel = "Jan Kowalski";
            var numer = "123456789";

            var telefon = new Phone(wlasciciel, numer);

            Assert.AreEqual(wlasciciel, telefon.Owner);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Konstruktor_ZaKrotiNumerTelefonu_ArgumentException()
        {
            var wlasciciel = "Jan Kowalski";
            var numer = "12345678";

            var telefon = new Phone(wlasciciel, numer);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Konstruktor_PustyNumerTelefonu_ArgumentException()
        {
            var telefon = new Phone("Jan", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Konstruktor_PustyWlasciciel_ArgumentException()
        {
            var telefon = new Phone("", "123456789");
        }

        [TestMethod]
        public void AddContact_DodajeKontakt_OK()
        {
            var telefon = new Phone("Jan", "123456789");
            var wynik = telefon.AddContact("Adam", "987654321");

            Assert.IsTrue(wynik);
            Assert.AreEqual(1, telefon.Count);
        }

        [TestMethod]
        public void AddContact_Duplikat_NieDodaje()
        {
            var telefon = new Phone("Jan", "123456789");
            telefon.AddContact("Adam", "987654321");
            var wynik = telefon.AddContact("Adam", "111222333");

            Assert.IsFalse(wynik);
            Assert.AreEqual(1, telefon.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddContact_PrzekroczonyLimit_Wyjatek()
        {
            var telefon = new Phone("Jan", "123456789");

            for (int i = 0; i < telefon.PhoneBookCapacity; i++)
            {
                telefon.AddContact($"Osoba{i}", $"{i:D9}");
            }

            // Przekroczenie limitu
            telefon.AddContact("Extra", "999999999");
        }

        [TestMethod]
        public void Call_IstniejacyKontakt_OK()
        {
            var telefon = new Phone("Jan", "123456789");
            telefon.AddContact("Adam", "987654321");

            var wynik = telefon.Call("Adam");

            Assert.AreEqual("Calling 987654321 (Adam) ...", wynik);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Call_NieistniejacyKontakt_Wyjatek()
        {
            var telefon = new Phone("Jan", "123456789");
            telefon.Call("NieMa");
        }
    }
}