using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C__PW_1;

namespace PolygonTests
{
    [TestClass]
    public class UnitTest1
    {
        // ===== Area =====

        // Тест 1: площа квадрата 2x2, вершини за годинниковою стрілкою, очікується 4
        [TestMethod]
        public void Area_Square2x2Clockwise_Returns4()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 0, 0, 2, 2, 2, 2, 0 });
            double expected = 4;

            // Act
            double actual = p.Area();

            // Assert
            Assert.AreEqual(expected, actual, 1e-9);
        }

        // Тест 2: площа того ж квадрата, вершини проти годинникової стрілки, очікується 4
        [TestMethod]
        public void Area_Square2x2CounterClockwise_Returns4()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 0, 2, 0, 2, 2, 0, 2 });
            double expected = 4;

            // Act
            double actual = p.Area();

            // Assert
            Assert.AreEqual(expected, actual, 1e-9);
        }

        // Тест 3: площа прямокутного трикутника з катетами 4 і 3, очікується 6
        [TestMethod]
        public void Area_RightTriangle_Returns6()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 0, 4, 0, 0, 3 });
            double expected = 6;

            // Act
            double actual = p.Area();

            // Assert
            Assert.AreEqual(expected, actual, 1e-9);
        }

        // Тест 4: площа неопуклого Г-подібного многокутника, очікується 3
        [TestMethod]
        public void Area_NonConvexLShape_Returns3()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 0, 2, 0, 2, 1, 1, 1, 1, 2, 0, 2 });
            double expected = 3;

            // Act
            double actual = p.Area();

            // Assert
            Assert.AreEqual(expected, actual, 1e-9);
        }

        // Тест 5: три вершини на одній прямій, площа 0
        [TestMethod]
        public void Area_CollinearPoints_Returns0()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 0, 1, 1, 2, 2 });
            double expected = 0;

            // Act
            double actual = p.Area();

            // Assert
            Assert.AreEqual(expected, actual, 1e-9);
        }

        // ===== IsTriangle =====

        // Тест 6: конструктор за замовчуванням створює трикутник
        [TestMethod]
        public void IsTriangle_DefaultConstructor_ReturnsTrue()
        {
            // Arrange
            Polygon p = new Polygon();

            // Act
            bool actual = p.IsTriangle();

            // Assert
            Assert.IsTrue(actual);
        }

        // Тест 7: многокутник з чотирма вершинами не є трикутником
        [TestMethod]
        public void IsTriangle_FourVertices_ReturnsFalse()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 0, 0, 2, 2, 2, 2, 0 });

            // Act
            bool actual = p.IsTriangle();

            // Assert
            Assert.IsFalse(actual);
        }

        // ===== IsRectangle =====

        // Тест 8: прямокутник 3x2 є прямокутником
        [TestMethod]
        public void IsRectangle_Rectangle3x2_ReturnsTrue()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 0, 0, 2, 3, 2, 3, 0 });

            // Act
            bool actual = p.IsRectangle();

            // Assert
            Assert.IsTrue(actual);
        }

        // Тест 9: квадрат теж є прямокутником
        [TestMethod]
        public void IsRectangle_Square_ReturnsTrue()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 0, 0, 2, 2, 2, 2, 0 });

            // Act
            bool actual = p.IsRectangle();

            // Assert
            Assert.IsTrue(actual);
        }

        // Тест 10: ромб (кути не прямі) не є прямокутником
        [TestMethod]
        public void IsRectangle_Rhombus_ReturnsFalse()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 1, 2, 0, 4, 1, 2, 2 });

            // Act
            bool actual = p.IsRectangle();

            // Assert
            Assert.IsFalse(actual);
        }

        // Тест 11: трикутник не є прямокутником
        [TestMethod]
        public void IsRectangle_Triangle_ReturnsFalse()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 0, 4, 0, 0, 3 });

            // Act
            bool actual = p.IsRectangle();

            // Assert
            Assert.IsFalse(actual);
        }

        // ===== IsSquare =====

        // Тест 12: квадрат 2x2 є квадратом
        [TestMethod]
        public void IsSquare_Square2x2_ReturnsTrue()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 0, 0, 2, 2, 2, 2, 0 });

            // Act
            bool actual = p.IsSquare();

            // Assert
            Assert.IsTrue(actual);
        }

        // Тест 13: прямокутник 3x2 не є квадратом
        [TestMethod]
        public void IsSquare_Rectangle3x2_ReturnsFalse()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 0, 0, 2, 3, 2, 3, 0 });

            // Act
            bool actual = p.IsSquare();

            // Assert
            Assert.IsFalse(actual);
        }

        // Тест 14: ромб (сторони рівні, кути не прямі) не є квадратом
        [TestMethod]
        public void IsSquare_Rhombus_ReturnsFalse()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 1, 2, 0, 4, 1, 2, 2 });

            // Act
            bool actual = p.IsSquare();

            // Assert
            Assert.IsFalse(actual);
        }

        // ===== IsRegular =====

        // Тест 15: рівносторонній трикутник є правильним многокутником
        [TestMethod]
        public void IsRegular_EquilateralTriangle_ReturnsTrue()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 0, 2, 0, 1, Math.Sqrt(3) });

            // Act
            bool actual = p.IsRegular();

            // Assert
            Assert.IsTrue(actual);
        }

        // Тест 16: прямокутник 3x2 не є правильним многокутником
        [TestMethod]
        public void IsRegular_Rectangle3x2_ReturnsFalse()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 0, 0, 2, 3, 2, 3, 0 });

            // Act
            bool actual = p.IsRegular();

            // Assert
            Assert.IsFalse(actual);
        }

        // Тест 17: ромб (сторони рівні, кути різні) не є правильним многокутником
        [TestMethod]
        public void IsRegular_Rhombus_ReturnsFalse()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 1, 2, 0, 4, 1, 2, 2 });

            // Act
            bool actual = p.IsRegular();

            // Assert
            Assert.IsFalse(actual);
        }

        // ===== Конструктор =====

        // Тест 18: null у конструкторі викликає ArgumentNullException
        [TestMethod]
        public void Constructor_NullArray_ThrowsArgumentNullException()
        {
            // Arrange
            double[] coords = null;

            // Act + Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Polygon(coords));
        }

        // Тест 19: непарна кількість чисел у масиві викликає ArgumentException
        [TestMethod]
        public void Constructor_OddNumberOfCoordinates_ThrowsArgumentException()
        {
            // Arrange
            double[] coords = { 0, 0, 1, 0, 0 };

            // Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new Polygon(coords));
        }

        // Тест 20: менше трьох вершин викликає ArgumentException
        [TestMethod]
        public void Constructor_LessThanThreeVertices_ThrowsArgumentException()
        {
            // Arrange
            double[] coords = { 0, 0, 1, 1 };

            // Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new Polygon(coords));
        }

        // ===== Equals / GetHashCode =====

        // Тест 21: многокутники з однаковими вершинами рівні
        [TestMethod]
        public void Equals_SameVertices_ReturnsTrue()
        {
            // Arrange
            Polygon p1 = new Polygon(new double[] { 0, 0, 4, 0, 0, 3 });
            Polygon p2 = new Polygon(new double[] { 0, 0, 4, 0, 0, 3 });

            // Act
            bool actual = p1.Equals(p2);

            // Assert
            Assert.IsTrue(actual);
        }

        // Тест 22: многокутники з різними вершинами не рівні
        [TestMethod]
        public void Equals_DifferentVertices_ReturnsFalse()
        {
            // Arrange
            Polygon p1 = new Polygon(new double[] { 0, 0, 4, 0, 0, 3 });
            Polygon p2 = new Polygon(new double[] { 0, 0, 5, 0, 0, 3 });

            // Act
            bool actual = p1.Equals(p2);

            // Assert
            Assert.IsFalse(actual);
        }

        // Тест 23: порівняння з null повертає false
        [TestMethod]
        public void Equals_Null_ReturnsFalse()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 0, 4, 0, 0, 3 });

            // Act
            bool actual = p.Equals(null);

            // Assert
            Assert.IsFalse(actual);
        }

        // Тест 24: копія, створена копіюючим конструктором, рівна оригіналу
        [TestMethod]
        public void Equals_CopyConstructor_ReturnsTrue()
        {
            // Arrange
            Polygon original = new Polygon(new double[] { 0, 0, 4, 0, 0, 3 });
            Polygon copy = new Polygon(original);

            // Act
            bool actual = copy.Equals(original);

            // Assert
            Assert.IsTrue(actual);
        }

        // Тест 25: рівні многокутники мають однаковий хеш-код
        [TestMethod]
        public void GetHashCode_EqualPolygons_ReturnsSameHash()
        {
            // Arrange
            Polygon p1 = new Polygon(new double[] { 0, 0, 4, 0, 0, 3 });
            Polygon p2 = new Polygon(new double[] { 0, 0, 4, 0, 0, 3 });

            // Act
            int hash1 = p1.GetHashCode();
            int hash2 = p2.GetHashCode();

            // Assert
            Assert.AreEqual(hash1, hash2);
        }

        // ===== ToString =====

        // Тест 26: рядкове представлення трикутника
        [TestMethod]
        public void ToString_Triangle_ReturnsVerticesString()
        {
            // Arrange
            Polygon p = new Polygon(new double[] { 0, 0, 4, 0, 0, 3 });
            string expected = "[(0; 0), (4; 0), (0; 3)]";

            // Act
            string actual = p.ToString();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}