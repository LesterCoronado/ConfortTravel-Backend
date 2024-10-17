namespace BackendConfortTravel.Servicios
{
    public class NumberToWordsConverter
    {
        private static readonly string[] UnitsMap =
   {
        "", "UNO", "DOS", "TRES", "CUATRO", "CINCO", "SEIS", "SIETE", "OCHO", "NUEVE", "DIEZ",
        "ONCE", "DOCE", "TRECE", "CATORCE", "QUINCE", "DIECISEIS", "DIECISIETE", "DIECIOCHO", "DIECINUEVE",
        "VEINTE", "VEINTIUNO", "VEINTIDOS", "VEINTITRES", "VEINTICUATRO", "VEINTICINCO", "VEINTISEIS", "VEINTISIETE", "VEINTIOCHO", "VEINTINUEVE",
        "TREINTA", "CUARENTA", "CINCUENTA", "SESENTA", "SETENTA", "OCHENTA", "NOVENTA", "CIEN"
    };

        private static readonly string[] TensMap =
        {
        "", "", "CIENTO", "DOSCIENTOS", "TRESCIENTOS", "CUATROCIENTOS", "QUINIENTOS", "SEISCIENTOS", "SETECIENTOS", "OCHOCIENTOS", "NOVECIENTOS"
    };

        public static string ConvertToWords(decimal number)
        {
            if (number == 0) return "CERO QUETZALES CON 00/100";

            int entero = (int)number;
            int centavos = (int)((number - entero) * 100);

            string words = "";

            if (entero > 0)
            {
                words += ConvertWholeNumberToWords(entero);
            }

            words += " QUETZALES";

            if (centavos > 0)
            {
                words += " CON " + ConvertWholeNumberToWords(centavos);
            }
            else
            {
                words += " CON 00";
            }

            return words;
        }

        private static string ConvertWholeNumberToWords(int number)
        {
            if (number == 0) return "CERO";

            if (number < 0) return "MENOS " + ConvertWholeNumberToWords(-number);

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += ConvertWholeNumberToWords(number / 1000000) + " MILLONES ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertWholeNumberToWords(number / 1000) + " MIL ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += TensMap[number / 100] + " ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "") words += "Y ";

                if (number < 30)
                    words += UnitsMap[number];
                else
                    words += UnitsMap[number % 10];
            }

            return words.Trim();
        }
    }
}
