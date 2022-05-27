using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexTester
{
    public static class Help
    {
        public static readonly string[] text =
        {
            ".\t\t\t\tDowolny znak (za wyjątkiem znaku nowej linii)",
            "\\s\t\t\t\tSpacja (lub znak nowej linii)",
            "\\S\t\t\t\tNie spacja",
            "\\n\t\t\t\tZnak powrotu do początku linii",
            "\\r\t\t\t\tZnak nowej linii",
            "\\d\t\t\t\tCyfra",
            "\\D\t\t\t\tKażdy znak, który nie jest cyfrą",
            "\\w\t\t\t\tSłowo złożone z liter, cyfr i znaku podkreślenia",
            "\\W\t\t\t\tSłowo złożone z różnych znaków",
            "\\b\t\t\t\tGranica słowa",
            "\\B\t\t\t\tNie granica słowa",
            "{x,y}\t\t\t\tCo najmniej x, ale nie więcej niż y wystąpień poprzedzającego wzorca",
            "{x,}\t\t\t\tCo najmniej x wystąpień poprzedzającego wzorca",
            "{,y}\t\t\t\tCo najwyżej y wystąpień poprzedzającego wzorca",
            "{x}\t\t\t\tDokładnie x wystąpień poprzedzającego wzorca",
            "?\t\t\t\tZero lub jedno wystąpienie poprzedzającego wzorca. Równoważne z {0,1}",
            "+\t\t\t\tJedno lub więcej wystąpień poprzedzającego wzorca. Równoważne z {1,}",
            "*\t\t\t\tZero, jedno lub więcej wystąpień poprzedzającego wzorca. Równoważne z {0,}",
            "[]\t\t\t\tZakres",
            "()\t\t\t\tPodwyrażenie, grupowanie wzorców",
            "(?<name>)\t\t\tPodwyrażenie, grupowanie wzorców (grupa nazwana)",
            "^\t\t\t\tUżyty na początku zakresu - negator. Użyty na początku wyrażenia - początek linii.",
            "$\t\t\t\tKoniec linii",
            "\\A\t\t\t\tPoczątek ciagu znaków",
            "\\Z\t\t\t\tKoniec ciągu znaków",
            "|\t\t\t\tAlternatywa",
            "(a)?b(?(1)b|c)\t\tDopsowanie warunkowe",
            "(?>group)\t\t\tAtomic grouping",
            "(?<=a)b\t\t\tPositive lookbehind - dopasowuje \"b\", które jest poprzedzone \"a\"",
            "(?<!a)b\t\t\tNegative lookbehind - dopasowuje \"b\", które nie jest poprzedzone \"a\"",
            "b(?=a)\t\t\tPositive lookahead - dopasowuje \"b\", po którym następuje \"a\"",
            "b(?!a)\t\t\tNegative lookahead - dopasowuje \"b\", po którym nie następuje \"a\"",
            "\\1..\\9\t\t\tOdwołanie do backreference",
            "\\k<name>\t\t\tOdwołanie do backreference dla grupy nazwanej",
            "?:\t\t\t\tPominięcie backreference dla grupy",
            "$1..$9\t\t\tPrzechwycone łancuchy odpowiadające grupom (przy zastępowaniu tekstu)",
            "${name}\t\t\tPrzechwycone łancuchy odpowiadające grupom nazwanym (przy zastępowaniu tekstu)",
            "$0\t\t\t\tPrzechwycone całe dopasowanie (przy zastępowaniu tekstu)",
            "$`\t\t\t\tPrzechwycony lewy kontekst dopasowania (przy zastępowaniu tekstu)",
            "$'\t\t\t\tPrzechwycony prawy kontekst dopasowania (przy zastępowaniu tekstu)",
            "(?i)\t\t\t\tIgnorecase",
            "(?-i)\t\t\t\tCasesensitive",
            "(?i:a)\t\t\tIgnorecase dla \"a\"",
            "(?#a)\t\t\t\tKomentarz inline"
        };
    }
}
