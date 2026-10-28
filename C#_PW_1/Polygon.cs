using System;

namespace C__PW_1
{
    // Довільний многокутник у двовимірному просторі
    public class Polygon : IPolygon
    {
        // Похибка для порівняння дійсних чисел
        private const double Eps = 1e-9;

        // Координати вершин у вигляді x0, y0, x1, y1, ...
        private double[] coords;

        // Кількість вершин
        public int VertexCount { get; private set; }

        // Конструктор за замовчуванням: трикутник (0,0), (1,0), (0,1)
        public Polygon() : this(new double[] { 0, 0, 1, 0, 0, 1 })
        {
        }

        // Конструктор з масивом координат
        public Polygon(double[] coordinates)
        {
            if (coordinates == null)
                throw new ArgumentNullException(nameof(coordinates));
            if (coordinates.Length % 2 != 0)
                throw new ArgumentException("Кількість координат має бути парною", nameof(coordinates));
            if (coordinates.Length < 6)
                throw new ArgumentException("Многокутник повинен мати щонайменше 3 вершини", nameof(coordinates));

            coords = new double[coordinates.Length];
            Array.Copy(coordinates, coords, coordinates.Length);
            VertexCount = coordinates.Length / 2;
        }

        // Копіюючий конструктор
        public Polygon(Polygon other) : this(other.coords)
        {
        }

        // ===== Допоміжні закриті методи =====

        // Координати вершини з номером i (номер береться по колу)
        private double X(int i)
        {
            return coords[2 * (i % VertexCount)];
        }

        private double Y(int i)
        {
            return coords[2 * (i % VertexCount) + 1];
        }

        // Вектор ребра від вершини i до вершини i+1
        private double EdgeX(int i)
        {
            return X(i + 1) - X(i);
        }

        private double EdgeY(int i)
        {
            return Y(i + 1) - Y(i);
        }

        // Квадрат довжини ребра i
        private double SideSquared(int i)
        {
            return EdgeX(i) * EdgeX(i) + EdgeY(i) * EdgeY(i);
        }

        // Скалярний добуток ребер i та i+1 (кут у вершині i+1)
        private double Dot(int i)
        {
            return EdgeX(i) * EdgeX(i + 1) + EdgeY(i) * EdgeY(i + 1);
        }

        // Векторний добуток ребер i та i+1 (напрям повороту у вершині i+1)
        private double Cross(int i)
        {
            return EdgeX(i) * EdgeY(i + 1) - EdgeY(i) * EdgeX(i + 1);
        }

        // Чи всі сторони однакової довжини
        private bool AllSidesEqual()
        {
            for (int i = 1; i < VertexCount; i++)
                if (Math.Abs(SideSquared(i) - SideSquared(0)) > Eps)
                    return false;
            return true;
        }

        // ===== Функції інтерфейсу =====

        // Площа за формулою Гаусса (формула шнурування)
        public double Area()
        {
            double sum = 0;
            for (int i = 0; i < VertexCount; i++)
                sum += X(i) * Y(i + 1) - X(i + 1) * Y(i);
            return Math.Abs(sum) / 2;
        }

        // Трикутник: 3 вершини, що не лежать на одній прямій
        public bool IsTriangle()
        {
            return VertexCount == 3 && Area() > Eps;
        }

        // Прямокутник: 4 вершини, ненульові сторони, усі кути прямі
        public bool IsRectangle()
        {
            if (VertexCount != 4)
                return false;
            for (int i = 0; i < 4; i++)
            {
                if (SideSquared(i) < Eps)
                    return false;
                if (Math.Abs(Dot(i)) > Eps)
                    return false;
            }
            return true;
        }

        // Квадрат: прямокутник з рівними сторонами
        public bool IsSquare()
        {
            return IsRectangle() && AllSidesEqual();
        }

        // Правильний многокутник: рівні сторони та однакові кути
        public bool IsRegular()
        {
            if (SideSquared(0) < Eps || !AllSidesEqual())
                return false;
            for (int i = 1; i < VertexCount; i++)
            {
                if (Math.Abs(Dot(i) - Dot(0)) > Eps)
                    return false;
                if (Math.Abs(Cross(i) - Cross(0)) > Eps)
                    return false;
            }
            return true;
        }

        // ===== Перевизначені функції класу object =====

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Polygon))
                return false;
            Polygon other = (Polygon)obj;
            if (VertexCount != other.VertexCount)
                return false;
            for (int i = 0; i < coords.Length; i++)
                if (Math.Abs(coords[i] - other.coords[i]) > Eps)
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            string result = "[";
            for (int i = 0; i < VertexCount; i++)
            {
                if (i > 0)
                    result += ", ";
                result += "(" + X(i) + "; " + Y(i) + ")";
            }
            return result + "]";
        }
    }
}