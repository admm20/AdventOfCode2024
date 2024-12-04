﻿class Day4
{
    private static char[/*row*/][/*col*/] StringTo2DArray(string text)
    {
        var split = text.Split("\r\n");
        var res = split.Select(s => s.ToArray()).ToArray();

        return res;
    }

    private static int GetXMAS(char[][] table, int row, int col)
    {
        var res = 0;
        var width = table[0].Length;
        var height = table.Length;
        var getCharSafe = (int y, int x) =>
        {
            if (0 <= y && y < height)
            {
                if (0 <= x && x < width)
                {
                    return table[y][x];
                }
            }
            return 'o';
        };

        // -
        if (getCharSafe(row, col + 1) == 'M' &&
            getCharSafe(row, col + 2) == 'A' &&
            getCharSafe(row, col + 3) == 'S')
            res++;
        if (getCharSafe(row, col - 1) == 'M' &&
            getCharSafe(row, col - 2) == 'A' &&
            getCharSafe(row, col - 3) == 'S')
            res++;

        // |
        if (getCharSafe(row + 1, col) == 'M' &&
            getCharSafe(row + 2, col) == 'A' &&
            getCharSafe(row + 3, col) == 'S')
            res++;
        if (getCharSafe(row - 1, col) == 'M' &&
            getCharSafe(row - 2, col) == 'A' &&
            getCharSafe(row - 3, col) == 'S')
            res++;

        // \
        if (getCharSafe(row + 1, col + 1) == 'M' &&
            getCharSafe(row + 2, col + 2) == 'A' &&
            getCharSafe(row + 3, col + 3) == 'S')
            res++;
        if (getCharSafe(row - 1, col - 1) == 'M' &&
            getCharSafe(row - 2, col - 2) == 'A' &&
            getCharSafe(row - 3, col - 3) == 'S')
            res++;

        // /
        if (getCharSafe(row + 1, col - 1) == 'M' &&
            getCharSafe(row + 2, col - 2) == 'A' &&
            getCharSafe(row + 3, col - 3) == 'S')
            res++;
        if (getCharSafe(row - 1, col + 1) == 'M' &&
            getCharSafe(row - 2, col + 2) == 'A' &&
            getCharSafe(row - 3, col + 3) == 'S')
            res++;

        return res;
    }

    private static int GetMAS(char[][] table, int row, int col)
    {
        var countMAS = 0;
        var width = table[0].Length;
        var height = table.Length;
        var getCharSafe = (int y, int x) =>
        {
            if (0 <= y && y < height)
            {
                if (0 <= x && x < width)
                {
                    return table[y][x];
                }
            }
            return 'o';
        };

        // \
        if (getCharSafe(row - 1, col - 1) == 'M' &&
            getCharSafe(row + 1, col + 1) == 'S')
            countMAS++;
        if (getCharSafe(row - 1, col - 1) == 'S' &&
            getCharSafe(row + 1, col + 1) == 'M')
            countMAS++;

        // /
        if (getCharSafe(row + 1, col - 1) == 'M' &&
            getCharSafe(row - 1, col + 1) == 'S')
            countMAS++;
        if (getCharSafe(row + 1, col - 1) == 'S' &&
            getCharSafe(row - 1, col + 1) == 'M')
            countMAS++;

        return countMAS == 2 ? 1 : 0;
    }

    public static void Main()
    {
        var table = StringTo2DArray(_input);

        var sum = 0;

        for (int row = 0; row < table.Length; row++)
        {
            for (int col = 0; col < table[row].Length; col++)
            {
                var c = table[row][col];

                if (c == 'X')
                {
                    sum += GetXMAS(table, row, col);
                }
            }
        }
        Console.WriteLine(sum);

        // part 2
        var sumPart2 = 0;

        for (int row = 0; row < table.Length; row++)
        {
            for (int col = 0; col < table[row].Length; col++)
            {
                var c = table[row][col];

                if (c == 'A')
                {
                    sumPart2 += GetMAS(table, row, col);
                }
            }
        }
        Console.WriteLine(sumPart2);

    }

    private const string _test =
"""
MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX
""";

    private const string _input =
"""
AAMXMASASMXSAMXXSAMSAMXXSXMMSMMSXSASXSXSSSXSAMXXXSXMASXMAMMASAMXAXAXMXMSSMMSXSASXSAMXXMASMXSMMMMMXXMAXXSXXXAXMSAMXSMXMSXSAMXMXMSXMMXSMXMXSMS
AXMASXMAASAMMMSMSAMSAMXMAXMAAAMXMASAAMMMAAAMMXMASMAMAMASMSAASXMAXMMXASAMXAAAMSAMMMAMMMXMASASAXXASMMMMSAMMMXMASXMASMSMMMAMMMMMAXXAMXAXMAMSAAA
XMSMMAMXMMSMAAAAMAMSAMXAMAMSSSMXAMMMSMAMMMMMSASXAXAMASXMAAMXXMSMXSAMXSMMSMMSMMAMASAMMAAXSMMSAMSMMAAAMMAMAASMMMXMMXMASAMAMSMSSSSSSMMXSXMXAMXM
SAMXSAMSXMASMSSMSMMMMMMAMMMMAMASMXSAAMSMMAMXSASMSSXSASMMSMMMSMAAAXAAAXAAMAMXASMMMSASXSMSMMXMXMAXMSMSSSSMSMXAASMMAASAMXSASAAXAAMXAASMMASAMXAX
ASAASXMMAXAXMAXXAXAXASAMXAAXMMMMMAMXSXMASXSMMMMAAMXMASAAMXSAAMMAMSSMSMMMSSMSMMXMXSXMAXXAAXMMSSMSXMAMAAAAXMSSMXASXXMMSMSMSMSMMMMSSMMASAMAXSSS
MMMMSAXSAMMXSASMMSMSAXMASMSMXAXAMXMXXASAMXAMXXXMAXAMXSMMMXMXSMXAXMAAXXSAAXMAMXXMMSAMXMSSXMAAMAAAMMAMMMMMMMAMAMXMXAXXAASAMXAAXXXXMAMMMMSXMAAX
XMAMXMXMASXAMMSAXAAMAMSASMXXSXSXSAMXMAMSMSAMXSMSASXSAMXSMASAXASMMSMMMAMMSSSMSMSMASXMAMMAMSMMSMMMMSASXXAAAMAXXMXASMMMMSMAMSMSMMMXMMMSXMSMSMAM
SSMSAXSMASMMMASMMMMMXMMASAMASAAMSASAMXMAMMAXXAAAAMASXSASMXMASAXXAAAAMAMAAAMXAASMAMASMSMAMXAAMXSXASASASXSSSSMSXMASXAAXAXSMSMAAAMMASASAAXAXXAX
AAAMXMXMAXAXMXXXXXXXXXMMMXMAMXMXMASXMXSASMSMXMSMSMXMAMASAMMMMXMMSSSMSXSXMMSXMSSXMSMMMAMMSMMMSAMMMMAMAMAAAAXAXMXAXMSMSAMXAMMSSMAAXMASXMMSMSAS
MMMMSSXMASMXMASMMMXSXMMMAXMSMSXAMMMXMASASMMMAAAMXMXMXMXMMMXAMASXMAMAXMMMXMAASAXXXAXASASMSXSAMMSAXMAMAMMMMAMMMAMMMXAXMMMMAMAAAMXSXMXMAMAXXSSM
XMAAMAAMAMMAXAAAAXAMMAAMAMMMAXMXSAAAMAMMMAAXXMMMAMAXMASMMSMASAMMMMMMMAAAMMSSMASMSMSXSAMAMSMASXSMSSSSSMSAMXMXASAMXMSMMSASASMMXXMMMXAXAMMSMMAS
MSMSSSSMMSXMMMSSXMMSASXSSMSMMMMMMMSMMASASMMSSMXSASXXSASAAXSXMMXMAAMXSSSXSAXXMAMXAAAXMAMMMASMMMXAXAMAAASXSMXASXXAAAAAXSXSASAMSAAAAMMSXXXAAXAM
XAAAAAXXAAMSXMAMXXMAMMAAAAAAAAAAXAXAXXXXMAAAAAASAMAAMASMMXAMXSASMMSAAAAAMXMXMXSSMSMXMAMAMAMMAMMMMAMXMMMMMAMXXMSSMSSSMSMMAMAAASXMSXMAASMSSMXS
SMSMMMMSMXMXAMASMMSSMMMMMMMXMSSSMSSMMSSSSMMMMMMSAMMMMAMAXSAMXSASAAMMMMMMMMMXMAAAAAXSSMXSMSSSMMAAMAMXXAAAMXXAMXAAMMAAXXAMXMMMMMXMAASMMMAAXXXX
SAMXMAMSMSMXAMAMAAXASAAXXXSAAAAAAAXMAXAAMXXXXAXMASXAMASXMMASXMAMMMMSASXMAAXAMXXMSMSAAXAMXMAAASXMSMSASMSMSAMSSMSSMMSMMSSMAXXAXXAMSXMAAMMMMMSM
SAMXSASXASAASMSMMMMMSXXSAASMMMSMMMSMSMMMMXMMSMSXAXXXSAMXASAMAXSXXXASMSASXSSSXSXXAXMMMMMSAMMMMSAAXAAXAAMAMMSAAAAXAXAXAXMMSMSAXSMMMMSSMSXSXAAA
MAMXSSSMXMAMMAAXSXMXXXXMXMMASAXXSXAAXMMMXMXXAMXMMSSMMMMSXMASMMMMMMAMASXMXAAAASXXXXXAMAMMAMXSASMMMXMSMMMAMSMXSMMSSMMMSSSXMASXMXMAAMAAAXAXMASM
SSMMMAXAMXMMMMMMMAXAXSSMMXSAMXMMSSMMMAAXAXXSSSSXSAMXSAAMMXAMMXMAMMSMAMXSSMMMMMAXSMSSSSXSASXMASAMSAMXAASMSSXMMAXXMAAAMAMAMXMMMASXSSSMXMXMAMXM
MASXMMMXAAMAXMASMMMXSAAAAXMAMXMAMAXXSXMXMXMXMAMSMASAMMSMAMSMSAMMSAAMAMAXAAXXXMSMAAAAAAASAMXMXMAMSXSSXMASAXAAXMXSSSMSMASXMAXAMXSAMAMXSMMSXMAX
SAMXMXMXSXSAMAAAAAAXAMMMMXAXMAMSSSSMXAMASAMAMAMAXSMXSAXMAXMASXSAMXSMMMSSMSXSMMASMXMMMMMMAMMSASAMXAAMMSMMMSMMXMAMAXMXXMMMXMMSMMMAMXMASXAAAMXM
MASASASMMASMSMMXSMMXMXMMMMMMMXSXAAXMAXSASASASXSMMMAXMAMSSSMAMAMMSAMAXAXXXXAAASASXSXAAXXMAAMSXSASMSMMMMMAMMAMXMASMSMSMSMSASMXAASAMAXMXSXSAMXA
MXSASAMAXAXXAXMXMAMXMAXSAAXAXMMMMMMMXXMASXAASAAXAMXMXAXXAXMAMAMXMAMSMMXSAMMMMMMSASXSXSXMASXMASXMXAMXAAMAXSAMXSASXAAAAAXXAMASMMMASMSXAMMAMSSS
XXMMMMSSMMSSMXXAXMAXXAXXXMXMSAMMMXMSXAMAXXMSMMMMXXMASMXXMASMSXSASXAMAMAMASMSMAXMXMXMXMAXAMAMAMXMASXSSSMSXSXSXSXSXMSMSMMMAMAMAASMAMMMASAMXAAM
XMASAMXMAXAAAAMSMSMSMSXSASAXSAMAXSAMSXMASMXAASXMXSXXAXSXXXMASASASAMSAMXSAMXASMSMXMASXMXSASXMMXMXAMXMAAAMASMSXXAMMXAXXAXSMMMSXMMSXAMMMSXSMMXM
XSAMXSASMMMSMMMXAAAXAAAXASMXXSMAMMAMXXXXXAMMXMASAMXSXMASXXXXSXMXMAXSAMAMASXMMMAMAMAAAAASAMXXSASXSSSMMMMMASAMMMMMMXAMXMMXAAASASMMSXASASASASAS
MMASXMASXAXAXXAMSMSMSMAMXMXXMXMMSSSMSSSMXMAXASMMXXMXAMMMMMSASXSASAMXAMSXMMAAASASXMASMMMMAXAXXASXAAXXXMXMAMMMAXMAMMSMXMAMMMXXAMAAASMMASASAMAA
AXAMAMAMMSXMSMSMAXXAXXMAXMMMXXAMAMAMAAAXSXXSAMAMSMSAMMXSAAMMMAXASAXXSAXAXSSMMSMSASXXMASXSMMSMSMMMMMMAXAMAXMXSXMAMMXAAMMMSMSMASXMMAXMMMAMMMSM
SMSSMMASXMAXXAAXMSMXMAMSMXAASMSMASMMMSMMXAAXASMMAAAAMSXMMMXAMAMXMXSAMASAMXXAAXASAMXMSASMXAAAXAAXMAAXXSXSASMMMASMSSMMMSXAAAMSAMASXMASMMSMSAMX
XMAAASXSASAMMSMXSAXAXXMAXSMSMAASAMAAAAXMMSMSAMASMXMSMXAMASXMMASAAXMXMMMMMMSMMSXMMMAAMASXSSSMSSSMMMSMXXAXAAAASAMXAAXSAMXXMMMMXSAMXXAMAMMAMASA
MMMSMMXMMMMMXXAXSAMMMSSMXXXXMSMMSSSMSSSMAAXMASMMXMSAMSXMAXAXSAXMSMMXSAAAMMAAAXAAXSSSMAMXAMXMAXAASAXAXMSMSSSMXAMMMMMMASMSXSASMMASMMMXAMMAMMMM
SAAXAMAMXAMXXMSMMAMXMAASAMXSAMXSAXMAXXMMSSMSAMXMAXMAMSAMSSMMMMMMAAXASMSMSMSSMSSMMXXAMASXMXAXMMSMMASAMAAAMMXMSMMAXXXSAMXAXSAMAXMMXAASXSSSSSXS
MMXSAMAXSASAMXXASAMXMSSMMAAMAMXMASMMMAMXAAAMMSSXMSSMMSAMAAXAASASMSMMSAXXAMMAMMAASXSXMXMASAMXMAMAMMMXASMMMXSMASMMSAMMMXMMMMMMSXMMMMMSXAAAAAAX
XXASAMAMXXMASASMSASXMXMASMSXMASXMAXASAMMMSMSXXMAMMAAAMAMSSMMMSAXAAAXMAMMMXXAMSXMMAMSMASMMMXSMASAMXAXMXXSAMASASMAXXMASAMXAAXAMMMMASXMMXMMMMSS
SMMSXMMMSMSXMASAXMMXMASMMAAXXAXAXMAMMASMAMMMXASXMSSMMSAMXAASMMMMSSMMMAMSXSSMMSMMMXMAMAMAMXXMXXMAMMSXMAXMAMAMXSXAXXSAMASXSSMMSMAAMMAAXMXAXXAX
SAXXMAMAAASMMAMMSXMASASAMMMMMSSSXMMSSXSMMSAAMXAMXAMAMSMSSXMXAAXXAAASXSXSAMXMAMAXAXSAMSSSMSMAMMSSMXXAMXMXMMXXAMXMAMMMSXMMXAMAAXXSXSSMMASXSMMM
SXMASAMSSMMSMXSMAASASMSAMMXSXMAMAAAXMXSAMSMMSAMAMMSSMXAAXMASMMMMXSMMXMAMXMAMAMXMXAMXSMAXAXAAMMAMXASXMAAASMXSASAMMAAXAAMXSAMSMSXMMMASAMXAAMXS
MAMXMAMAAAASXXMMSMMAMAMAMXMMAMAMXMMXXAMAMXSAMXMXSAAASMMMXSAMXAASMMMXSMMMAMMXSSMMXMASAMAMSMXSSMXSMMSASMSMSAASASXSXSMMXAAAMAMXXSASASAMSSMMMMAX
SAMXSSMSSMMSMXXAMMMMSMSSMMAXXMSSXSMXMASAMXMSMSAMMMSSXXMAMMASMXXXAMXAMAASXMSAAAAXASMSAMXSAAAXAMAXMXXAMXAXMMMMXMMAMAAXSMMXSMMXAMAMAMASXSAAAMXS
SASAAAAAAMXSASMMSAAAAXXMASMXMXXMASAASXMASXMAMAAAXXMAMXMASXXMASMSMMMSMSMSMAMMXMMSMSAXXMXMMMMMAMXSMSMSMSXMXXXMASAMXSAMXAMMSAMMSMSMMMXMAMSMSSMS
SAMXSMMSSMAMXSAASMMSMXXMMMSAMSAMMMSMSASXMMSASMMMMMMMSXSMSAMXAAAAASAXAMMXMAMMXSAMXMAMXSAMXAMSXMAAAXAAXAMXMXAMMASXAMAMXXMAXAMAMXMAMMMMXMAXXXAM
MXMXXXMXMMXSAMMMSAMAMSMMSASXMASMAMAXSAMAAAMXSXAAXAMXMASXSXMAMMMSXMAMXMAXXXSAMXASMSXMXAMMSAXXMMSSSMSMSMXSAMXMXMMMXSAMXSSSSSMXXAXMMAASMXXSMMSM
SMMMXMMAAMMMMXXXXAMAXSAXMASXMAMSXSXXMMMMMMXSXMSMSXXAMXMASASXXMAXAMAMMMMSMAMXMSMMAMMMXMMAMSAAXAAMAMXAXAMXSMAMAXXAAXMMXMAMXAASMSSMSXXSAMXAAMAM
AAAAAXSSMSAAMASXMSMSSMSMMSMXMAMMMXMMAMSXAXMXAMXAAMXMMAMMMAMXASAMXXMSAAAAMAMMMSSMAMAXASMXXMSMMSXSAMMMMSMAMXSSSMMMSMMSXMAMMMMMXAAMMXASMASXMSAS
SSMXMXAXAXXMSAMAAMAMAMXMXXAMMMMSMMASXMASMSSMAMMMMSAMSSSXMAMXMMXMMMMMXMSXMASXAMAXXMMMAXAMXMAMXMASXSASAMMXMAXAMAMAAAAAXSASXXMSMSMMMMXSXMAAASAM
MAAASMMMSMSXMASMMMXMAMXMAMXMASAAAAXAMXMMXAASXMSMAXMAAMAASMSASMSAXSAXAMXMXMSMMSAMMASMSMAMSSSMXMAMXSAMASASMSMAMXXSXXMXMMAMXMAXAAAAXMASMSMMXMMS
MMMMXAMAMXMASAMXMASMXMXMASMMAMSXMXSXSAXMMSXMXAAMASXMMSSMMASASAAMMSASXMMSAAXXXMASXAXAAMAMXAAMAMASMMXMSMAMAXMAMXXMMSSMMSSMMMASMSSMSSXMASXMXXXA
SASMSSMASAXMMSAMXMSXSAMSASAMAXAMSMSASAMXAMAMMMMMXAXMAAXAMXMAMAMXAMMMXAASMSMMMSAMMMMMMSAMMMMMAMAMAMXMAMXMAMSSSMSAAAAXAAAAXMASAAXAXXAMSMAMSMXM
SASXAASMMMXXAXMAAXMAMMAMAMXMSMSMSAMXMAMMXSAMSSMSAMXMMMSXMMAXXAXMSXSAMMMMAXXAAMMXMAXAASXXAMAXMMSXSXAXASMMXMMASASAMSSMMXSMMXAMAMMMMXMMXSMXAAAX
MXMMSMMMAMMMSMMSMSMXMAXMSMSMXAAAMMMMSAMXXMAMXAMXMASXAAXAXAMMXAMXXXMASMSMXMMMXSMASASMMMASXSSSMAMAXXASAMXAMXXAMMMAMAXAMAMMMMMSXSAAAASAMAXSMSMS
XSXAAMMMAAAAAAXMAXMMASMAAAAAMSMMMXSASASMMMSSMMMSMAMSMSSSMSAMSMSMSXSAMXAMMXMAXXXAMXAMXMAMXAAXMAMMMMMAMXAMXMMSMXSAMXSXMAMAAAMAASMXSAXAXSMSAAAX
MAMSXSASXSMSSSMMAMAXAAMXMXMMMAAXMASMSAMAAAAAAAAXXAXXSAAAAXAAAAAAAMMAMSXSMASMSMSMMSSSSMMMXMMMMXSAMMXAXMAMAMMMAAAXSMXMMMSSMSMMMMAXMXSMMMAMSMMM
AMXXAMXSXAXAMMMSSMMMMSMXMXMASMSSMASAMMSSMMSSMMXSSMSXMMSMMXMSMSMMMMSMMSXMXMMAAAAAMAAAXAMMXXSXMAMASMMMMXAMMSASMMMXSAMXAAXXAAXXMMMMMASAAMAMAMXA
SXMMMSMSMSMXSAAAAAXXMAXAAASASAAAMAMAXAAAXMAXAXMMAAXMMAXMAMXMAAMXSAMAAXAMASMXMXSSMMSMSXMASMMMMASMMXAAASMSAMXSMSXMMMMSMSSMSMSMMAAAMAMMMMASAMMS
AMXAAAXXAXAAAMMSXMMXSASMSMSAMMSSMMSMMMXXMMASMMMSAMSAMMMMSAMMXMSASAMMMSASASMMSAMXAXXMAXXXAAAMMMSXASMSXMSAMXXMXSAMXXXXAAMAAMXASMXSMMSAXSASASAX
MASMSSSMSMSMXMAXXSAXMASXAASXXXAXXXAMAMXSXMAMXAXAAMXMMXASASMAMSMMMAMXMXMMASAASAMXSMXAASMMSMXMAAMMMMXXAMXMASMSASAMSSMMMMMXMSSMMSMAAMAMXMASXMXS
XMXMAMAMMAMXXXAMXMASMMSAMXMMXMASXSXSMSXAAMSSSSSSSSSXMSMSAXMMSAAXSMMMMASMSMMMSAMAMMXSAMXAAAXSMMSAXSMSXXAMXMASASMMMMAXMASAMAAXAAMSMMXXAMAMAXAX
SAAMMSAMMXMMSAMXSMASAMXXXXXASMMSXMASASMSMMAAAXMXAAMXMAAMXMAMSXSMSAAASASXAAMAXAMAXAMXAMMMMXMAMXMASXAXMSMXXMXMXMXAASMMSASASXMMMSXMSAMXMMSSSMSA
XSXMAXASMAXAXAMXMMMSAMASMMASAAASAMMMAMAAMXMXMAMMMMMMSMSAASXMSXMASMMMMAXMSSMXSXMSSSXSAMXMASMSMSSSSMMMXAAMSMMSSMSXMXAMMMXXMAXAAMAMXMASXMAAAAAM
MAXMSSMMXMSSMMMXXAXMMMASASMXSMMSAMAMAMSMSAXSXMXSAMXAXMXMMXASXMMAMSSSMSMAXXMMSXAXAMASXMMMMSAAAAXAXAAASMSMAAASAMXMSSSMXASXMSMSXMAMMXXSAMMSMMMX
AMXXAAXMAMAAAXAXSMSAAMXMAMXAMXMSAMXMSMMASMSSMMMSAMXSMSMMMSMMAXMSMAXAAAMSMSAAXMMMAMAMAAAXAMXMMMSMSSMMSAAMMMMMMMSXAAAMMMSAAXAMXXXMXMASAMAXAXXM
SXMMSSMSMMSXMMSMMXSXMXMMAMMSSSXXAXAXXASAXMAMAAAXXAXMXSAAAAASMMXXAMSMMMXAASMSSXSSMMASMMXMMSMXXXAXAAAAMXMSXSSSMASMMSMMAXMMMMAMXMSSSMXSAMXSMMMA
XAMXMAMAXAXAMAMAMAMXXAAMAMXMAMAXAMSXSAMXSMMSMMMSASMSASXMSSMMMXSMSMAXMXSMMMAAAXMASMXSAMXSXMMMSSMSSSMMMXXAAAAMMASXMAXSMXMXMSAMASAAXAASMMAMASAS
SAXSAMXXMMMMMAMMMXSAMXSMAMMMAMXSAMXMMXMMXAASMMMMAMAMAXXXAXMASMXAAMXMXAAAXXXMMMSAMXAXAXASASXAMAMAMXMMSSSMMMMMMMSMXMXSMSMAMSXSXMMSMMMSAMXSAXSM
SAMXXSMMXAASMMSXSAMXXAAXXMAXXSAAXSAMXMASXMMSAXXMAMXMSMSMMXSAMXMSMSSMMMSAMXMMXMMAMMXMAMASAMMSSMMMSMXXAAXXAAAMXMSMMSAMXAXAXSMMAAXMAMAXMAXMXMAM
XXAXAAAAMXXSAXSAMASAMSSMMSXSAAMMMSXXSAXXSAAMXMMXXSXAAAAAAAMASXXMMAAAAAMAMMAXASMSMSMXMAXMMMAXXXXXXXMMMSMXSSMSAMXAAMASXMSSMMASMMSSSMASXSMASMMM
MASMSMMMSMAMXMMASXMAMAAAXAMXMMSXAXXMASAAXMMMXXSAAAMSMMMMMXMAMXMAMMSAMXXAMSSSXSAXASAAMSMSASMMSMMMXMASAAAXMXMAMXMMMSXMMAAMSSXMMAXAMXAMXAMAMAMX
AMXAXXMAXMAMAXXAMXMXASXMMMSSXXXMMSMXAMMXMMMSMAAMSMMXMMSMMMMSSMSAMMMMMSMAMXAAMMAMAMXMXAAXASAASAAAASMMSSXMMAXAMXMSAMXMMMMSAXAXSXMXMASAMMMASAMS
XXMXMAMXXSMSMSMMSMAAXASXSAAXSASMMAXMASXASAAAMXMAMXAMXAAAAAMAAXSASXSMAAXAMMSMAMAMMMASMMXMAMMSMSMSXSAMXXAASMMMSAAMASASMMMMMSMMMAMMMMMAMXSASMSA
SSMSMSMMMMAMAAXSMMSMSASMXMASMXMASAMSAMXAMMSMMXXASAMMMXMMMSMMSMSXMMMMSSSMSAMXSSXSMMMSAAMSAMXAAAMMASMMAXSMMAAASMSMSSXSAASAXAAAXAMAAASAMXMASXMM
XMAAMAMAAMAMSMSAXAAAMXMAMXMXMXSAMXMMSSMMSXMASMMXSASXXSXSAAASMMXXAAAAAAMXMMSMAMMMXAAMMXMSASMMSMXSAMXMAMMXMSXMXSAXAXAXMMSMSSSSSSSSMMSXMAAASMSX
MMSMSASMSXXXAMXMMMSXMAMSMASAMMSASXAAAXAAAASAMASMMAMAAAAXSSXMAMMMSSMSMMMXMASXXMASMMSSXMXSAMAMXXAMASXMXMMAXXMASMXXMMASMMMMAAMAAXMASMXMXXSAXAAX
AAMAMXSXMXSSXXAXMXAASMXMMXXASASAMMMMMSMMMMMAXXMAMXMMMMSMMMMMAMAXXMAMAMXMMMSMMXMXMAXXAXAXMSAMXMXSAMAXAMXMSAXMMMSAXMAMAASMMSMMSMMSMSASXMAXMXMX
MMSSMASAMAMAMSXSAMMMMSASMSSMMMSXMAXXXMAMSASAMSSMMAMXAXMAXAAMSXSMSXMXAXAXSXMASASMMSMSMMXMASMSAMASASXMASAMSAMXAAMMXMASXMMAAXAAXAXMAMMMAAAMSMSM
SXAAMAMAMAMAMASMASMAASXMAMMXMXMXMAXMXMAMAMMAMXAASAMSMSSSMMSAMXMMSASMSMSSXASAMAMAAXMXAXXAMSXSASMSAMXMASAXSMXSMSXSASXSAASMMSMMSMMMSMAXSMMSXAXA
AMSSMSSMXSSXSSMSAMMMMMAMMMSASAAXSAMMMMASMMSXMSSMMXASAMAXXAMXSMMASAMAMAXMMMMMSSSMMSASMMMSAMASAMMMAMXMASMMXAASAMASAMASMMMMXXAXMAAAMMXMAAXSMMMS
MAMAMXAAAMXMXAAMXXXMMMXMXXSASXSXMASAAMXXXMMSAAAXAMXMAMMMMXSMSAMAMXMAMSMSXXAMAMXMAXMAAAAAAMXMMMXSXSMMASMMMMMMAMMMSMMMXSASXSSSMMMMXAXXMMMSAMXX
XXSAMSMMXSAAXMMMSMMSAMAXXMMMMMMAXASMSSMSSMAMMSSMAAXSXMXMXMAASMMASASXMAAAXSXMASMSSMSSSMXMXMXMAMMMMMAMASAXXAMSSMMAXXAMMMAXAMAAAXMAMSMSAAXSAMAS
SMSAMXMAXMAMXXSAAAASASXMASAAAAXXMASXAAAAXMASXXXAMSMMXSAXAAXXSASASMXMSMSMMMMSXMXAAAAXAXXSSSMSASAXASXMXSXSSMXAXAMXSXMSAMSMSMSSMMMASAAMSMMMMMAS
MASXMMMSSMXSAAMXSMMMAMXASMSSSSMSMXXXMMMMXSAMXSSSMMMMASASXMSASAMXMXAAAAXXAAAAMXMMMMMSXMAXAAXSASXSXSXXXMXMASMASXMXMXXMXMXAXAAAMXSXSMSMMMSMMMXM
SMSXXSAAXMAMMSMMXAMMAMMMXAAMAAAAMSMSXASAMMMSAMXMAAAMMMAMAAMMMAMAMSSSMMMSSSSMMXMAAAAMASMMSMMMAMASMMMMAMAMMMXXAMXAMASXSSMMMMSSMASAXXXAXAAMSMSX
MAMAMMMMSMMSMMAXSAMSSMSAMMMSXMSMXAAMXXMXMAAMASAMMSMSAMXMXMMAMAMXXAAAXSAMXMAAXAXSMMASMMSAAXAMMMMMAASMMXXXSAMXXSSXSAXMAAAXAXAAMXSMMMMSMSMSAAXM
MSMSMASXMAMXASXMMSMSAMMMSAAXXXAMMMXMMMSMMSXSAMASXMASMSMMMMSXSMMSXMSMMMMSSXSMMMMMASXMAAMSMMSXMASXSMSAXMSMMMXMXAAAMMSSSMMXMSXMAMMASXAXXMSSMMMA
AXAXAASMSAMXMMSMAMXSSMAXSMSSSSXSAMAASAMMAXAMXSAMAMAMAAXXAAAAAAAAAMMMXXMAXXAXASAMXSAMMMMASMXMSASXAASAMXXAAAASMMAMMSAMXXXXXAMXMASAMMSSXMAMAASM
SMXMMMSXAXXXAAXMASXMAMSXXAMAMAASAMSAMASMMXAMASXMAMSMSMSSMSXMSMMSXMASXSMASMMMMMMMAMAMSMSASXXAMMSXSMSASAMSMSXXAXMASMMSSMMXAASXXMMMSAAMMMXMMMSA
MAMXAMXMMASXMXSMSSMSAMXAMSMSMMMMXMXASAMASMMMAMASASXAAAXXAMMAXXAXAMXMMAMASAXAXAMMSSMMMXMXMXSSMXXAMXSXMAMMAXASMMMSXMAMAAMSSMMXAXAAMMXMASMMXAMX
SMAXXSAMXMSAMASMAXAMASMAMXAMXAXMAXXAMAMAMXAMSSMMSMMXMMMSXXASMMSSXMAMSXMAMMMSSMSAMAXMSAMASASAMXMXMAXASMMSAXXAASXMASMSXMMAMSSSMMSXSAMSMSAASMMM
SXSMAXMMAMXAMAXMXMMSAMXMSMSMSSMSAMMSMMSAMXMXMAMMAXAMMSMAXSMMAAAMASMMMMMMSXAXAAMASMMXSASXMXSXMXAAMXMXMSAMXMMSMMASAMAMMMMMSAXAAAXAXMMAXMMMMAAX
SAMMSXXXASXSMSSSMMXMXSXMAAAAMAXMASXXAMXAMAMASMMSMSMASAMXMMASMMXSAMMAMXMXAMXXMMSMMAXASAMXMAMXXASXSSXSAMXSAAXAXMAMXMMMAMSXMMSSMMMMMMSMSSXXSSMS
SAXAXAMSMMAXAMAXAMAMXMASMSMSMXMSSMXMMMSXXXSASXAAMAMXMXXSASAMAMXMASMXMXMAAAMMSASXSSMMMXMAMMSSMAMXAASMXSXSXSMXXMXSAMXSXSMAAXXAMXAXAXAXSMAXMAAA
MAMAMSMAAMAMAMAMMMAMASXMAXXXMMMXAAXXSAMXSXMXSMSSSMMSMSMMAMAMAMASAMMMMAMMXSAAMAMXAAAMAMSAMSAMXAMXSMSSMSAMAMASMMMMASAXMAXXMSXMASASMSMMSAMMSMMM
SXSXMAXMMMXSAMASXXXXAMXMAMMMSAMSMMMMMASXAAMASAAMAMMSAAXMSMMSASMMMSMASAMXAMMMMAMSSMMMAXMAMMAXSXSXXXMAMXAMXMAAAAASMMASXSSMMXMMXAAXAAXASAMXXASX
AMMAXASMSXMMAMAXAASMSMSMMXSASXMXMMSXSAMXMMXAMMMXSMAMXMAMAAXAAXXAMAXMXAXAAMXXXAXMAXMMXMSAXSXMMMMMMMSMMMMMXMMSSMXAMXXXXAAXXAXXAMSMMMSXSXMASMMM
SMMXMXXMAASMSMSSMXMAMAXXMAMMMMXASASXMAMMXMMMXMSAXMSSMXMSSSMMMMSAMXSXSAMSXMXMSSXSMMMSAXSMMSMAMASAAMAXMAXAMXAAXMASXMSMMSMMMMSMMSMXAAMMMMMXXMAS
MAASMMSMSMMAMAMXXASMMMMXMASXSXSAMASMSSMAAMASAAMASXAXMAMMAAAXXAXXASAMMAMMASXMAMAAXAAMMMMXAXAXSASXSMMSMSMAMMMXSMMXAAAXAAMAAAAXXAMXMSXMAAXASMXX
MMMSAAXAMMMAMAMMMMASMMSSSMSXAXMSMXMMAMMSASASMSMMMMMSXSSMMSMMMXSMMMASMXSMMMAMASMMMMMSXSSMSXSAMXSAMAXSAMSAMXAXSAMSMMMMSXSSMMAXMAMXXMAMSMSASMMS
XSXSMMMXMXMAMAMAAXMAXAMXAAMMMAAAXMAMAMAXMAMMXAMMMSXAMMAMAAAASMXAXSSMMASAMSXMASAAXMXSAMXAAAMSMMMAMMAMAMSXMMSXSAMMASXAMAXASXMXSAXSSSSMAAMMMMAA
MSAXMAAASXSXSMSSSSXSAMSXMMMAXSMMMSASXSSSSSSSXXXAAMMSXSAMXSAMXMASAMXAMASXMXMMASAMXMAMXMASMMMAXXMAMXMSAMXSMXXASXMXAMXSMXMAMXAAMMSXAAXMMMMSAMSS
AMXMMMMMMAMXAAMAAMAMMASXSASXMXAXXSASAAXAAAXMASMMMSAMMMAMAAXMASAXASMXMAMAMXAMXSXMAMASAMAXXMXASXXAXXASASAAMAMMMMSMMMXXXMMXMMMMSMXMMMMMXSXMAXXX
XMMSXSXSMMMSMMMMXMAMXMMASASMMSXMAMMMMMMMMMMMMMAMMMMSASXMASMSMSMSMMMAMMSMMXMSASAMASMSAMASASXASMSSXMMSMMMMMAXAAASASAXMMMXASASAMMXMXMAMASASMMMS
XSAMXMAXAAAAAMXSMMAMAXMAMSMAXMAMXSSMMXSAMXAXASXMXMASAMXMSMMXAXMAMAMXXAAXSMMMASAMASXSXMASAMMAMAAAASXSAAXXSXSMXMXAMMMSAAMSSXMASMSMASAMAMMMAXAM
XMASMMAMMMSSSMXMASXSSSMSSMSMAMAMXAAAAASASMSSMSAMAMMMAXXMAAMMAMSASXMXXXSXMAXMXMAMASXMASMMXMSSMMMSMMAMMXSAXXAXSXMMMXAXSXSXMASXMAAXAXMMAMXMMMSS
XSAMASXSXAXXMXMXAXAAAXXMSAASMSXXXMSMMXXXMAAMXSASXSMSMMSSSSMMMMMASMSMSXXASXMMMSMMXMASAMXAAXAAXMAXAMXMAXMAMXXMAMMSAMXMMXAXMASAMXMMSSSMMMSAXMXX
MMASXMAXMMMXMAAMXSMMMMSMMMMSMAMSMXAMXMSMMMMMAMMMMXAAXXXAMAMXAAMMXAAAXASMMAAAAAXXASAMAAMXMMSAMXXSXMMMMSMXMSSMXSASASXAAXMMMAMXXSAXAAAAAAMAMXXX
XSMMMMSMMAAAMMSAXAXAXAXAAXXXMAAAAXMSAMAAAXXMAXMAMMSSSSMXMASMSSSMMSMMMMMXMMXMMXSAMMASMMXSXMASMSXMAAMAAAXXMXAAAMXSAMMMAMXSAMXAXXAMXSSMMMSASMSM
MSAMXAAASMSSSMXAMMSMMXSMMXSASXSSSMASASMSMSSMAXSASAAAMAMXMASXAMXMAMAXAXAAMSSSXMMAXAASASASXSAMMSASXMSMSSMSMSMMXXAMXMASXMAXMSMXSSXXAMAXAASAMAAA
AMAXMAMMMMAAXAXXAXAMMMXAAAMMMAAAAMXMAMMMAMXMAMXAMMSMSAMXXAMMXMXMAMXSMSMXSAAXMASXMSXSAMASAMAXAMAMAAXAXMAAMXXXSAMXXAAMAMMXXMAMMAMXMMMMAMMMMSMX
MSMMXSSSMMXSMMMMSSXSXMMMMXSMMSMSMMAMAMXMAMXMASMAMMMXSASXMASAASXSMMXSAAXSMMSMXAAXAMAMXMAMMMSMMMAMMMMSMMSMSXMASXMMMMMXSASMMMSMSASASAXSSXMAMASA
MMASAMXMASXMAXAAMAMMASXMMXAMXXAAXSAMXMASAMXSAXXAMXAXXMAXSSSMMSAAASASMMMXASXAMMSMAMXMSXMXSAMAXSASXSMMAMXXMAMXXXXAAAAXXAAAAAAXSASASMMAAMXXSASX
MSAMXMASXMASAMMSSMASAXMASMMMMMSMMMASAMXSAMAXXMMMMMXMAXMAMXMAXMXMMMASXAXSXMXXAMAAXSSSMASMMMSAMSASAAAMMMSMMSXMASMSSXSMMXMSMSSXMAMAMMSMMSSMMXMX
AAXMMMMSASAAASAAAXAMMMMAMXMAXMAMMXXMAMASAMXXSMSASXMXXMSMMSSMMMMXXMMMMXXSAMXMMSASAMXASAMAAXMMMMAMMMSXMASAAAMMMAMAMXMXSAXAXAXXMAMXSAMXXAAASMMX
MSMMXSASAMMSMMMSSMMSXXMXSASMMSASAAMMXMXSAMXAMASXSAMSSMSAAAAAAMAMSMSASMMSAMXSXAMXMXSMMMSSXMASXSMSXAMAMASMMXXAMXMXMAMASASXSMMXXXXAMASAMXSSMAAX
AAAXASAMXMXAXAXXXASAMSMMSASAASMMMXSAXSMSXMMXXAMMSAMXAASMMSSSMMXAAAMASXAXMMAXMAMAMMSXMMMMXASXMAASMSXXMMXXMASMSMMMSMMMSAMMAMASXMSMSXMASXMAMSMS
SSSMXSASMSSSSMMSAXMAMXAAMXMMMSXMAAMMMXAXAXMMMMSAMAMXMMMMXXAAMXMSMSMAMMMSMMMSXSMASAMAXMAMXMAAXMMMMAMSXSSSMXSAAXMASAAASAMXAMXXAAAASASXSMXSXMAS
MMAMXSAMAAAAAAAAMMSMMSMMSASXAXMMMMSMAXSMMAXAAXMXSSMSXSASXSMMMMMAAXMSAXMAMASXAMSAMASXMMMSSMSMMMXXMAMXAXAAXAMXASXSSSMXMAMSXSXSMMMMSAMAMAAMAMAM
SXSMMMMMMMMMMMMSMAXAMXAASXSMSSXAASAMAMXAXSSXSASMXMAAAMAMAAAAAXMXSXXAMMSSSMSMMMMXMAMMSAMAXMAXAMXMSMSMMMSMMMSAMXMAMAMASXMMMMMAXAAMMMMAMSMXAMXS
AAMXXAASAMXAXAAXMSSMMSMMXMMXMAXSMSASMSSXMXAXMASMMMSMMMAMXMMMXMMSAMXMAXAMAMXAXAMMXASXSXMXSXSSSSMXAAAAMXMXXAAXXAMMMMSAXXAAAXMMSSMSASMSXMMSMMAA
MSMASMMSXMSMSMSSMAXMAXMXMASAXMAMXSAMXAMMMMMMMAMAXMXMXSSSXSAMSSMAAMMXXMXSAMXSMAASXXMAXAMAMMAMXAXXSSSSMAMXMASMMMMSAAMXSSSMXMAAAAXSASXMAXXAXMXM
XAXAAAAXAMAMAMXAXAMMSMAASXMXSMXXMXXSMMMMAAAMMMSMMXAMMMAAXMAXAAXXMMXSMMXSXSAASXMAMMMXMASAMASASMMXMAMXXAXAXMAXAXASMSSMMXAAAMMMMSMMMMASAMSMSSSS
SMSMSMMMXSASMSSSMXSAAASMSAMXMASMMMMXAXMMSSMMAMAMMSMXAAMMMSSMSSMSXSXMAXAXXSAMSSSMSMAASXMASAXMMASXMMASXMXSSMXSSMXSAAMAASMMMMMAAXAAASAMAXXAMAAA
SXXAMXSXAXAMXMAXXMMMSMXXSAMSAMXAAAXMSMMMAXMSMSASXMMSSSXXXAAMXAAXXSXXSMMSAMAXXAMAAMSMSAXXMAXASMMXSAAXXAXXAMAAMXMMXMAMMXAMAMSAMXSSMSXMMMMSMMMM
MXSXMAMMSSMXMXXMMMAAMAXAMAMXAMSMSXXAAAXMAMXAXXXXAAAAXXXMMMMSSMMMMMSAXAMAMSMMMMMSMXMXXMASMMSAMMXXXMAMMSMSAMMSMMMAMXXMSSSMAXAAXMAXAXMAAAXXAMXX
MAMMMXSAASAASMSAAMASXMMSMSMSMMSXAAMSMSMMAMSSSMASXMMSSMSMMSAXMASXMAMXXMMMXAAAAXMXXAXAMXXXAXMMMXMMMMSMAAAMSMXMASMAMSMXAAXMMMMAMXXMXMASMSSSSMSM
MXSASXMMSXSXSAASXMAXAMXAAAAXMAMMMMMXAXXXAXXAAMAMXAXAAAXAAMASMXMXMASMSSMXSSSMSSMAMSSMSMSMMMMASAAMSAAMSMSMAXMMAMXAMXMMMSMSMAXSXMASASXXMAXAAAAA
SSMXSAAXAMMMMXMMMMMSAASMSMSMMMXSXXSMMMSSMXMXMMSSSXMXMXMMMSSMMAMXSASAAAXXMAXXXAMAMXAMAXAAAMMAMASXMSXXAAXXMSASMSSMSAMXMAMAMSMXAXASASXMMSMMMSMS
MAMASXMMSAAAMAXAXAXSMMXMAMAAMMASMMMASAMASXAASMMMMAASMSAAMXAASMSMSMSMSMMSMMMMSMMMXXAMMSSXMSMXSMMAMXSSMSMMXXAAAAMMSMMASASASAXSXMMMAMXMAMAAAAXA
SMMASASMASMSXMSMMMXXSXXMXSMSMMASAXSAMXSAAMSMMAAAXMXAAXXMSSSMMXSASASXXAMXAXXAAXAAMSAMXAAASAXMAXSAMSXMMAAMSMSMMMSAMASASASXSMXSAMXMXMXMAXSMSMSX
AXMXMAXAMXXMXAAAMSMMAMSMMSAXXMASXMMXSMMMMXMASXMSXXMMMMSSXMMAXXMAMAMAMASMMMMSMSMXMAMXMMMMSASMMMMXMMAXXMMMAAAXAXMASMMMMAMXXMASAMASAMAMSXMXXASM
MMSSMMMSXSASMSMSMAAMMAAAAMAMXSXMXAMAXMASXMMMMXAAASAMXAXAAXSSMXMAMAMAMAMAAAXAAAASMMMXXXXXMXMMXAAAMMMMMSASMSMSMSSMMXXXMAMSSMMSXMASASMSMASAMASM
XAAAAXMAAMAMAXMAMXMMXSSSMMSMAXASMSMSMSAMAASXSMXMASASMMSSSMAXASMMSMSMSAMSMMMXXMMMAAMXSAMSSMMSMSMXSAMAAMXMXAXAMMAAXSMMXSSXAXXXAMMSAMXAMXMAMASM
MMMXMMMMXMMMSMSMSSMSXMXAAXXMAXXAAAAAMMMSMMMAXXXAASAMAMXAMMXMMMMAAXAMMXMMAAASXSASMMMSMAMAAMXSAXAASAMMXMAASMSSSSMMMSAAAXMXMSMMAMXMMMSXMMXXMAMX
MSSXMAAXAAXXMXMXAAXSASMSASMSASMMSMSMSXAAAAMMMMSMMMMSSMMAMMMSXAXSMXMSASMSXMMSAMMXAAXXMAMSSMXMAMAMMXMAXXMMAMAAMAASASMMMSAMXSAXSXMASAMXMAXSMSMS
AAAAMSSXMSSMSAMXSMMSXMAXAAAAAAXXAXXAXMSSSMMXAAAXXAAMAMMSSXAASXMASXXMXSXMASXMAMXXXMMSSMXXAAAMXMAXSSMMMASXSMMSMSMMASAAXAAMASMMAASAMXXSXXXAAAMA
MMSSMAMMXMAXSXSAMAAMMMSMMMSMSMSSXXMXMXAXXXXSMXSMMMSSMMAMAASMAMSAMXXMASASASASXMMSAAXXAMMMMMMMAMMSMXASXMSAMSAAXAXMXMMSSSMMXSXXSAMMSAXSASAMSMSM
XXAXMAMMASMMXMMAMMXSAAAAXAMXXAAAMSMSAMXSMSMSAAXMXAMAMMMMSAMXMMXAMXMXASAMASXMXAASXMMSMMSAAAMXSXSAMMAMAXMAMMMMSXXXMMSAMXAXAXXXXXMAMMSMAMMXMAMX
MMMSMMMSASXAAASMMAXSMMSSMMSMMXMMMAAAMAMXAXAMMMMAMSMMMXSAMAXXXXMSAMXSXMXMAXMMMMMSASAAXASXXMSAMMSASMSSMMMSMSSMMMMAAXMASXMMXSMMMXMASXXMAMXMMAMM
AAMXMAMMAMXXMMAAXAAXAAXXAMXAAXMSSMMMASAMXMSMSASMMXAAAASXSSMSMSAMASASAMXMSSMXAAAMMMAMMASAXSMXSASXMMAAAXMAAAAAAXSASMSMMMMSAXAAAXSXMMMSXXXAMXMA
SXSASXMXASXSMMSMMXXSMMMSSMMSMMSAMXXXAMAMSAMASAMXAMSMMMSAXXAAASASAMASXMAAMAXASMSXXXMXSXMMMAASMMMSAMXSMMSMSMSMMMSAXAAAMXAMMMSSSMMAMMMAMMMSXASM
XASASAXSASASXAAAXMXMASXAXXAAASMMXSAMXSAMMSSMMSMMMMXMSAMXMMSMXMMMXMXMASMSMMMAXAMMSXMASXMAXMMMAXAXXSAMXAXAMXXAMXMAMSMSMMXSXAXMAMSAMAMASAAXSMMX
MAMAMAMMAMAMMSMSAMMSAMMXMMSSSMAAXMSAASASAASMAAXASAMXMASASAXSAASAXMASAMSAASMXMAMAMXMASASMSXMSAMXSAMMSMASXMASXMAMMMXMXAXAMMSXSAMXASXSASMSMSXAM
MSMSMAMMAMXMAMMXMAMXSSXAXXXXXXMMSAMXMSSMMSSMSSSXSXAXSMMMAXMASXMMXSAMXSMSSXAMSXMASAMXSAMXSAMXMAAAXSXXXMAMXMASXXSMXMASAMXXSAMXXSSMMXMXSMMAMXXS
""";

}
