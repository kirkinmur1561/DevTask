namespace DevTask.Test.NUnit3;

public class TestMathCurrency
{
       private CurrencyBuild[][] _currencyMath;
       [SetUp]
       public void SetUp()
       {
              _currencyMath = new[]
              {
                     new []
                     {
                            CurrencyBuild.Build(10, '€'),
                            CurrencyBuild.Build(35, '¥'),
                     },
                     new []
                     {
                            CurrencyBuild.Build(100, '₽'),
                            CurrencyBuild.Build(75, '₽'),       
                     },
                     new []
                     {
                            CurrencyBuild.Build(7, '$'),
                            CurrencyBuild.Build(5, '£'),
                     },
                     new []
                     {
                            CurrencyBuild.Build(5, '€'),
                            CurrencyBuild.Build(5, '€'),
                     }
              };
              
       }
       
       [Test]
       public void SuccessAdd()
       {
              Assert.That((_currencyMath[1][0] + _currencyMath[1][1]).Num, Is.EqualTo(175m));
              Assert.That((_currencyMath[0][0] + _currencyMath[0][1]).Num, Is.EqualTo(31.73m));
              
       }
       
       [Test]
       public void SuccessSubtraction()
       {
              Assert.That((_currencyMath[1][0] - _currencyMath[1][1]).Num, Is.EqualTo(25m));
              Assert.That((_currencyMath[0][0] - _currencyMath[0][1]).Num, Is.EqualTo(-11.73m));
       }
       
       [Test]
       public void SuccessMultiplication()
       {
              Assert.That((_currencyMath[1][0] * _currencyMath[1][1]).Num, Is.EqualTo(7500m));
              Assert.That((_currencyMath[0][0] * _currencyMath[0][1]).Num, Is.EqualTo(217.34m));
       }
       
       [Test]
       public void SuccessDivision()
       {
              Assert.That((_currencyMath[1][0] / _currencyMath[1][1]).Num, Is.EqualTo(1.33m));
              Assert.That((_currencyMath[0][0] / _currencyMath[0][1]).Num, Is.EqualTo(9.30m));
       }
       
       [Test]
       public void SuccessEquality()
       {
              Assert.That(_currencyMath[3][0] == _currencyMath[3][1],Is.EqualTo(true));
       }
       
       [Test]
       public void SuccessInequality()
       {
              Assert.That(_currencyMath[3][0] != _currencyMath[0][1],Is.EqualTo(true));
       }
       
       [Test]
       public void SuccessGreater()
       {
              Assert.That(_currencyMath[2][0] > _currencyMath[1][1],Is.EqualTo(true));
       }
       
       [Test]
       public void SuccessLess()
       {
              Assert.That(_currencyMath[1][0] < _currencyMath[0][0],Is.EqualTo(true));
       }
       
       [Test]
       public void SuccessGreaterOrEqual()
       {
              Assert.That(_currencyMath[3][0] >= _currencyMath[1][1],Is.EqualTo(true));
              Assert.That(_currencyMath[0][0] >= _currencyMath[3][0],Is.EqualTo(true));
       }
       
       [Test]
       public void SuccessLessOrEqual()
       {
              Assert.That(_currencyMath[1][0] <= _currencyMath[0][1],Is.EqualTo(true));
              Assert.That(_currencyMath[3][0] <= _currencyMath[2][1],Is.EqualTo(true));
       }
}