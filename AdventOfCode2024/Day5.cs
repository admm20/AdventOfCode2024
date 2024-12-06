﻿class Day5
{
    public static void Swap(IList<int> list, int indexA, int indexB)
    {
        int tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
    }

    public static void Main1()
    {
        var rules = _orders
            .Split("\r\n")
            .Select(o => o.Split("|"))
            .GroupBy(o => o[0])
            .ToDictionary(
                o => int.Parse(o.Key), 
                o => o.Select(x => int.Parse(x[1]))
            .ToList()
        );

        var updates = _updates
            .Split("\r\n")
            .Select(o => o.Split(',').Select(int.Parse).ToList())
            .ToList();

        var updatesInvalid = new List<List<int>>();

        var sum = 0;

        foreach (var update in updates)
        {
            var isValid = true;
            for (var current = 0; current < update.Count - 1; current++)
            {
                var currentNumber = update[current];
                for (var future = current+1; future < update.Count; future++)
                {
                    var futureNumber = update[future];
                    var rule = rules.GetValueOrDefault(futureNumber, new());
                    
                    if (rule.Count > 0 && rule.Contains(currentNumber))
                    {
                        updatesInvalid.Add(update);
                        isValid = false;
                        //Console.WriteLine($"Error because {currentNumber} is behind {futureNumber}. Entry: {string.Join(",", update)}. Rule: {futureNumber}|{currentNumber}");
                        break;
                    }

                }

                if (!isValid)
                {
                    break;
                }
            }

            if (isValid)
            {
                sum += update[(update.Count / 2)];
            }
        }

        Console.WriteLine(sum);

       // part2
       var sumPart2 = 0;

        foreach (var update in updatesInvalid)
        {
            //Console.WriteLine($"Start: {string.Join(",", update)}");
            for (var current = 0; current < update.Count - 1; current++)
            {
                var currentNumber = update[current];
                for (var future = current + 1; future < update.Count; future++)
                {
                    var futureNumber = update[future];
                    var rule = rules.GetValueOrDefault(futureNumber, new());

                    if (rule.Count > 0 && rule.Contains(currentNumber))
                    {
                        //Console.WriteLine($"Error because {currentNumber} is behind {futureNumber}. Entry: {string.Join(",", update)}. Rule: {futureNumber}|{currentNumber}");
                        //Console.WriteLine($"Changing: {currentNumber}->{futureNumber} because of rule {futureNumber}|{currentNumber}");
                        Swap(update, current, future);
                        current = -1;
                        break;
                    }

                }
            }
            //Console.WriteLine($"End: {string.Join(",", update)}");

            sumPart2 += update[(update.Count / 2)];
        }

        Console.WriteLine(sumPart2);
    }

    private const string _testOrder =
"""
47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13
""";

    private const string _testUpdate =
"""
75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47
""";

    private const string _orders =
"""
84|39
48|11
48|79
88|98
88|82
88|12
69|67
69|77
69|22
69|61
41|75
41|56
41|59
41|95
41|83
24|97
24|98
24|54
24|48
24|47
24|16
33|59
33|92
33|47
33|16
33|82
33|88
33|78
92|82
92|59
92|88
92|78
92|97
92|63
92|56
92|74
11|35
11|39
11|27
11|75
11|47
11|24
11|77
11|79
11|43
36|44
36|33
36|46
36|26
36|83
36|35
36|96
36|39
36|72
36|76
59|55
59|78
59|84
59|36
59|69
59|61
59|97
59|17
59|12
59|67
59|76
98|55
98|66
98|32
98|97
98|61
98|67
98|63
98|26
98|46
98|84
98|22
98|45
72|54
72|48
72|45
72|31
72|12
72|99
72|87
72|75
72|88
72|63
72|78
72|69
72|59
35|87
35|83
35|59
35|75
35|82
35|88
35|99
35|98
35|45
35|47
35|74
35|69
35|97
35|56
43|59
43|33
43|75
43|72
43|54
43|31
43|95
43|24
43|83
43|39
43|16
43|47
43|92
43|44
43|32
17|43
17|26
17|75
17|41
17|79
17|72
17|83
17|39
17|22
17|95
17|46
17|32
17|92
17|47
17|27
17|24
31|16
31|78
31|67
31|22
31|12
31|45
31|17
31|55
31|11
31|82
31|99
31|98
31|63
31|79
31|97
31|56
31|61
85|56
85|36
85|66
85|22
85|48
85|98
85|76
85|43
85|96
85|61
85|67
85|55
85|82
85|84
85|69
85|11
85|17
85|78
75|16
75|63
75|99
75|59
75|97
75|55
75|54
75|82
75|74
75|96
75|88
75|98
75|12
75|36
75|69
75|85
75|45
75|31
75|61
46|75
46|24
46|72
46|77
46|83
46|59
46|88
46|32
46|16
46|39
46|44
46|74
46|31
46|99
46|54
46|87
46|47
46|27
46|35
46|95
39|88
39|78
39|85
39|59
39|83
39|47
39|82
39|92
39|97
39|48
39|72
39|75
39|63
39|45
39|56
39|16
39|87
39|35
39|31
39|99
39|95
96|66
96|35
96|26
96|79
96|87
96|84
96|27
96|11
96|33
96|77
96|95
96|46
96|76
96|39
96|17
96|44
96|47
96|83
96|43
96|41
96|22
96|72
95|75
95|54
95|92
95|98
95|59
95|87
95|16
95|47
95|63
95|12
95|72
95|97
95|85
95|99
95|56
95|48
95|83
95|69
95|74
95|88
95|78
95|31
95|82
47|92
47|45
47|61
47|31
47|54
47|16
47|97
47|98
47|63
47|85
47|78
47|48
47|82
47|12
47|69
47|87
47|88
47|75
47|72
47|83
47|56
47|74
47|99
47|59
74|11
74|12
74|82
74|17
74|16
74|22
74|78
74|85
74|56
74|45
74|96
74|55
74|99
74|76
74|63
74|97
74|69
74|48
74|84
74|31
74|98
74|36
74|61
74|67
22|32
22|87
22|39
22|72
22|43
22|47
22|59
22|79
22|41
22|83
22|35
22|77
22|26
22|88
22|54
22|95
22|24
22|46
22|27
22|44
22|33
22|66
22|75
22|92
78|26
78|96
78|84
78|46
78|22
78|44
78|63
78|45
78|76
78|67
78|55
78|56
78|82
78|61
78|97
78|11
78|43
78|69
78|98
78|12
78|79
78|36
78|17
78|66
27|74
27|75
27|24
27|77
27|54
27|92
27|48
27|31
27|99
27|41
27|59
27|83
27|82
27|72
27|85
27|88
27|16
27|78
27|98
27|39
27|87
27|47
27|95
27|35
45|77
45|44
45|11
45|27
45|79
45|41
45|84
45|96
45|17
45|22
45|55
45|69
45|43
45|61
45|66
45|36
45|67
45|33
45|76
45|46
45|26
45|24
45|32
45|12
76|75
76|87
76|54
76|33
76|41
76|32
76|46
76|35
76|88
76|92
76|22
76|47
76|83
76|95
76|72
76|77
76|44
76|26
76|43
76|39
76|79
76|27
76|24
76|66
83|85
83|69
83|99
83|56
83|63
83|87
83|88
83|98
83|61
83|45
83|48
83|92
83|55
83|16
83|54
83|59
83|74
83|31
83|67
83|12
83|75
83|82
83|97
83|78
56|79
56|11
56|61
56|77
56|69
56|45
56|12
56|84
56|67
56|66
56|63
56|44
56|33
56|17
56|96
56|36
56|43
56|46
56|32
56|27
56|26
56|76
56|55
56|22
63|41
63|96
63|22
63|32
63|36
63|12
63|44
63|67
63|55
63|61
63|33
63|79
63|45
63|26
63|27
63|17
63|11
63|84
63|46
63|69
63|76
63|66
63|77
63|43
16|26
16|99
16|85
16|61
16|56
16|98
16|22
16|82
16|78
16|12
16|84
16|17
16|96
16|36
16|45
16|63
16|76
16|11
16|69
16|48
16|55
16|79
16|97
16|67
82|67
82|76
82|45
82|17
82|36
82|46
82|79
82|11
82|69
82|84
82|55
82|96
82|22
82|66
82|44
82|97
82|26
82|12
82|56
82|98
82|43
82|63
82|32
82|61
79|35
79|83
79|54
79|47
79|59
79|24
79|92
79|39
79|88
79|77
79|33
79|26
79|44
79|66
79|46
79|87
79|95
79|43
79|75
79|74
79|41
79|32
79|27
79|72
54|61
54|69
54|45
54|99
54|82
54|88
54|17
54|16
54|98
54|78
54|56
54|36
54|63
54|11
54|31
54|59
54|85
54|67
54|74
54|55
54|48
54|12
54|97
54|96
32|54
32|85
32|33
32|92
32|59
32|72
32|75
32|95
32|31
32|39
32|87
32|83
32|35
32|16
32|78
32|24
32|88
32|48
32|47
32|77
32|74
32|99
32|41
32|27
44|88
44|85
44|83
44|32
44|47
44|48
44|95
44|31
44|99
44|59
44|41
44|27
44|35
44|54
44|16
44|24
44|75
44|72
44|92
44|77
44|39
44|33
44|74
44|87
87|45
87|75
87|59
87|88
87|61
87|92
87|69
87|97
87|82
87|31
87|99
87|12
87|48
87|54
87|74
87|36
87|85
87|56
87|63
87|67
87|55
87|98
87|78
87|16
55|44
55|66
55|96
55|76
55|39
55|24
55|77
55|22
55|43
55|84
55|17
55|11
55|46
55|33
55|67
55|95
55|27
55|79
55|35
55|32
55|47
55|41
55|36
55|26
67|46
67|17
67|33
67|72
67|96
67|35
67|36
67|79
67|39
67|66
67|41
67|27
67|11
67|44
67|43
67|26
67|76
67|47
67|84
67|22
67|95
67|77
67|32
67|24
97|17
97|69
97|12
97|84
97|67
97|56
97|11
97|32
97|26
97|27
97|45
97|96
97|22
97|55
97|43
97|46
97|36
97|61
97|33
97|66
97|63
97|44
97|79
97|76
66|32
66|99
66|33
66|87
66|83
66|92
66|74
66|44
66|59
66|41
66|77
66|54
66|16
66|39
66|31
66|27
66|95
66|47
66|35
66|72
66|24
66|75
66|88
66|46
26|27
26|33
26|66
26|32
26|44
26|41
26|72
26|88
26|75
26|74
26|46
26|95
26|54
26|43
26|83
26|47
26|77
26|39
26|92
26|24
26|35
26|31
26|59
26|87
99|79
99|63
99|36
99|43
99|96
99|48
99|26
99|78
99|85
99|12
99|69
99|76
99|17
99|82
99|56
99|61
99|97
99|45
99|67
99|84
99|11
99|98
99|22
99|55
12|77
12|22
12|33
12|36
12|17
12|96
12|41
12|11
12|27
12|26
12|76
12|39
12|84
12|35
12|43
12|55
12|24
12|66
12|61
12|79
12|46
12|44
12|32
12|67
77|39
77|48
77|41
77|99
77|59
77|82
77|85
77|75
77|72
77|54
77|47
77|95
77|78
77|31
77|16
77|83
77|24
77|92
77|35
77|74
77|87
77|97
77|98
77|88
61|17
61|43
61|36
61|95
61|41
61|46
61|66
61|33
61|67
61|55
61|27
61|22
61|77
61|24
61|35
61|96
61|39
61|79
61|32
61|76
61|84
61|44
61|26
61|11
84|83
84|35
84|26
84|47
84|22
84|46
84|27
84|92
84|54
84|33
84|44
84|43
84|66
84|32
84|77
84|87
84|79
84|72
84|76
84|95
84|75
84|41
84|24
48|69
48|82
48|97
48|61
48|96
48|12
48|67
48|22
48|78
48|45
48|66
48|76
48|98
48|17
48|84
48|55
48|46
48|36
48|56
48|43
48|63
48|26
88|16
88|11
88|96
88|59
88|85
88|45
88|31
88|74
88|69
88|36
88|63
88|48
88|55
88|78
88|67
88|61
88|99
88|97
88|84
88|17
88|56
69|24
69|79
69|12
69|96
69|66
69|44
69|33
69|26
69|32
69|36
69|84
69|41
69|17
69|11
69|43
69|39
69|27
69|55
69|76
69|46
41|98
41|85
41|99
41|35
41|24
41|16
41|88
41|97
41|92
41|54
41|48
41|39
41|72
41|87
41|47
41|74
41|78
41|82
41|31
24|88
24|59
24|78
24|99
24|74
24|72
24|85
24|39
24|56
24|35
24|75
24|95
24|92
24|63
24|31
24|82
24|87
24|83
33|99
33|24
33|85
33|41
33|27
33|74
33|77
33|48
33|83
33|39
33|95
33|31
33|75
33|54
33|72
33|35
33|87
92|55
92|85
92|16
92|96
92|12
92|36
92|31
92|45
92|98
92|69
92|99
92|11
92|61
92|48
92|67
92|54
11|66
11|72
11|17
11|87
11|95
11|46
11|44
11|26
11|33
11|22
11|84
11|76
11|41
11|32
11|83
36|95
36|77
36|43
36|84
36|24
36|17
36|11
36|27
36|32
36|79
36|47
36|41
36|22
36|66
59|56
59|48
59|16
59|11
59|82
59|85
59|63
59|99
59|96
59|45
59|74
59|98
59|31
98|12
98|44
98|36
98|33
98|56
98|76
98|11
98|96
98|69
98|79
98|43
98|17
72|56
72|55
72|74
72|16
72|97
72|82
72|85
72|83
72|92
72|98
72|61
35|63
35|16
35|92
35|31
35|85
35|78
35|48
35|95
35|72
35|54
43|35
43|74
43|88
43|77
43|66
43|27
43|41
43|46
43|87
17|35
17|84
17|77
17|44
17|87
17|33
17|76
17|66
31|48
31|84
31|36
31|69
31|96
31|76
31|85
85|12
85|97
85|63
85|26
85|45
85|79
75|78
75|92
75|67
75|56
75|48
46|92
46|33
46|41
46|85
39|98
39|74
39|54
96|32
96|24
95|45
""";

    private const string _updates =
"""
26,46,44,33,77,41,39,95,47,83,75,54,88,59,74
63,61,11,79,26,44,32,33,77
72,92,16,48,98
48,82,59,85,55
66,84,76,33,79,75,92,41,44,46,35
63,69,61,55,67,36,11,84,76,22,26,46,44,32,33,27,77
33,77,17,32,24,26,11,61,43,79,46,76,84,22,12,41,96,27,55,67,36,44,39
11,17,84,76,79,26,43,66,46,44,32,33,77,41,24,95,47,83,87
27,77,41,24,39,35,95,72,83,87,75,92,54,88,74,31,99,85,48,78,82
87,75,54,88,59,74,31,99,48,78,97,56,63,45,69,12,61,55,67
46,17,66,76,77,67,26,35,61,96,22,44,11,24,27
39,35,72,83,87,75,92,59,74,31,99,85,48,78,82,98,63
24,27,17,76,79,44,26,32,35
98,88,63,45,56,72,78,85,82,12,69,59,87,54,47,75,31,83,48,16,74,99,92
67,17,84,76,22,79,43,46,32,33,24,35,47
78,27,33,24,85,74,92,54,75,95,48,99,39
95,47,72,83,87,75,92,54,88,74,31,16,99,85,48,98,97,56,63,45,69
83,16,77,35,59,31,46,27,74,92,39,66,87,54,41,33,75,44,95
72,33,39,88,54,26,27,24,41,22,92,35,46,87,44,83,43,77,75,32,79,95,66
79,36,41,46,44,72,35,27,17
84,26,66,32,33,35,95,72,92
31,16,45,69,61,12,74,17,82,85,11,88,48,98,67,63,99,56,96,78,36
78,87,16,95,35,45,85
61,55,67,36,96,11,17,84,76,26,43,66,46,33,27,77,41,24,39
56,78,99,69,47,16,98,92,72,83,75,74,82,45,31,85,63,12,48
54,59,95,31,92,87,16
72,87,24,92,59,95,47,75,88,41,99,82,16,85,39
17,22,26,84,67,55,36,11,61,79,69,97,76
77,26,11,32,43,66,63,33,44,67,76
77,85,47,99,44,31,95,27,87,35,54,16,74,39,83,59,41,32,24
47,72,83,87,75,92,54,59,74,85,48,78,82,98,97,56,63,69,12
74,31,48,78,98,56,45,69,61,11,84
26,11,97,56,67,66,69,32,17,43,55,36,33,22,79,76,96,61,45,44,12,84,63
72,83,77,92,27,75,41,35,44,33,54,43,46,79,26,47,22,66,24
95,41,44,59,66,88,35,77,27,79,43,72,26,47,92,46,54,33,83
45,69,12,55,67,36,96,11,17,84,76,22,26,43,66,46,44,32,27,77,41
48,98,56,69,36,76,79,43,66
84,77,35,83,44,39,66,75,72,27,79,26,17,24,43,76,87,22,46,47,95,32,33
69,74,92,98,59,63,99,83,72,45,47,95,54,87,48
92,56,96,59,74,69,85,99,98
11,84,76,26,43,66,46,44,32,33,27,77,41,24,39,35,95,83,87
97,67,11,76,79,43,66,32,33
66,44,27,77,24,39,35,95,72,83,75,88,74,31,16
36,11,76,43,44
88,97,63,98,56,87,75,99,59,35,48,92,95,83,78,54,47
76,77,32,55,27,79,46,66,17,12,69,43,45,84,44,41,33
77,41,27,92,99,75,24,44,46,59,32
76,24,12,32,96,79,77,36,84
77,24,35,72,83,87,75,92,88,59,31,16,85,82,98
67,77,39,22,84,47,46,41,44,26,95,35,27,76,66,11,79,96,33,24,43
69,12,61,36,96,84,76,22,79,26,43,46,33,41,24
46,96,78,22,26,11,76
47,72,92,88,59,74,16,78,82,98,56,63,12
75,92,54,59,74,31,99,85,48,56,63,69,12,67,36
43,54,44,35,77,26,92,76,47
87,92,54,88,59,74,31,16,99,85,48,78,82,98,56,63,45,69,12,61,55
36,11,17,24,66,32,84,61,77
33,24,22,35,47,83,76,77,92,66,54,44,75,32,41,26,46,87,79
63,36,96,11,17,26,46,44,77
87,92,47,75,32,27,16,33,39,44,41,72,88,46,77,59,95,54,99,35,24
22,26,43,66,46,32,33,27,95,83,75,54,88
76,96,82,97,98,61,44
84,82,17,99,67,48,11,85,12,22,69,96,55
46,32,41,39,95,75,54,16,99
87,92,16,99,78,98,56
45,61,74,12,11,76,67
46,27,32,77,92,88,47,31,99
63,85,26,97,82,36,17,56,48,22,45,99,61,84,78
41,48,95,77,82,31,74,85,72,88,35,99,27,39,78,24,92
61,87,92,55,31,69,67,56,88,16,48,97,74,54,63
44,32,27,77,41,24,35,95,47,72,83,87,75,54,88,59,74,31,16,99,85
79,66,46,32,27
54,56,97,83,95,24,82,31,85,88,35,16,92
26,17,45,12,43,96,48
99,48,82,56,63,45,69,61,55,67,17,76,79
66,16,72,27,88,77,44
98,97,56,63,45,12,61,55,36,96,11,17,22,79,26,66,46,44,32
39,35,95,47,72,83,87,75,92,54,88,59,74,31,99,85,48,78,82,98,97,56,63
56,69,85,67,11,43,96,17,55,76,26,84,45
54,59,16,99,78,82,11
63,84,61,97,11,79,16,67,99,82,96,22,76,56,36
39,59,72,33,83,95,48,74,87,16,41,54,77,85,35,92,47,78,31,88,99
98,97,56,12,61,36,96,11,17,84,22,79,26,43,46,44,32
84,76,22,79,26,43,32,27,77,41,47,72,92
69,12,61,55,96,11,17,84,22,79,26,43,66,46,44,32,33,27,77,41,24
79,82,66,48,76,22,17,55,45,67,36,63,43,98,11,61,78
54,88,59,74,31,16,99,85,48,78,82,98,97,63,45,69,12,61,55,67,36,96,11
45,61,97,99,72,74,12
95,47,72,54,88,59,31,16,99,85,48,98,97,56,69
83,87,75,92,54,88,59,74,31,16,99,85,48,82,98,97,63,45,69,12,55
56,98,31,54,16,83,48,35,92,24,95,78,87
82,98,97,45,12,61,55,67,36,96,11,17,84,76,22,79,43,66,44
79,26,43,46,33,27,77,41,24,39,95,47,83,87,75,88,59
78,16,97,56,61,67,11,48,99,22,31,82,17,69,96
56,98,69,63,78,55,31,16,83
46,76,79,44,77,55,84,36,26,33,12,39,43,67,17
26,92,41,83,87,75,35,43,46,88,32,33,44,47,77,24,27,72,39,95,59
85,96,97,74,48,11,55,69,36,31,59,63,82,16,45,17,99,67,61,12,98
46,66,92,33,39,27,47,44,72
11,99,67,55,63,97,48,69,74
88,32,75,41,72,74,33,44,39,31,95,47,54,87,83,16,59
72,83,92,48,98,97,61
31,69,12,36,98,97,85,11,61,48,82
77,59,27,26,32,24,92,83,35,87,66,39,41,46,74,88,54,95,44
82,56,69,36,11
55,56,74,96,45
46,17,69,32,76,97,43,33,22
36,96,17,84,76,22,79,26,46,44,32,27,77,41,24,39,35,47,72
69,55,26,66,77,41,24
98,75,78,95,16,35,87,88,47,99,39,82,48,56,63,59,92,97,31,83,54
36,17,85,59,55,78,12,56,96,11,84,67,63,61,74,45,99,31,97
99,97,87,82,16,63,88,12,74,98,56,45,92,69,75,78,59,55,61,54,67,31,48
79,67,55,11,63,46,17,69,43,44,26,97,45,61,76,36,98
17,36,84,16,74,12,67,11,76,78,96,97,82,61,45
11,66,96,79,67,35,39,22,84,33,76,55,43,27,61,44,24,36,32
27,72,78,77,33,85,39
27,17,32,67,36,33,69,43,12,61,66,24,11,77,96
78,56,31,82,39,74,92
78,82,98,97,36,84,46
31,99,78,12,96,11,17,84,22
77,35,95,54,83,87,43,41,72,39,92,22,79,44,66,26,46
66,24,84,27,61
43,77,33,54,95,88,46,26,75,92,32,74,66,24,47,72,83,59,35,44,41
27,77,41,24,39,35,95,47,72,83,87,75,92,54,88,59,31,16,99,85,48,78,82
88,59,74,31,16,99,48,82,98,56,63,45,69,12,61,67,96,11,17
85,88,36,75,56,98,82,45,59,78,54,74,92,16,67
95,76,24,55,84,27,77,33,26,35,67,44,11,17,43
97,22,11,78,84,69,26,85,82,55,98,17,48,12,36,61,76,96,43,56,45,67,79
78,97,56,45,69,61,67,96,17,76,22,79,46
59,97,31,61,98,88,74,54,85,55,48,12,83,87,16,99,78,75,45
24,39,35,95,47,72,83,87,75,92,54,88,59,74,31,16,99,48,78,82,98,97,56
43,66,46,44,32,27,77,41,24,39,35,95,47,72,83,87,92,88,59,74,31
82,98,97,56,63,45,61,55,67,36,96,11,17,84,76,22,26,43,66,46,44
88,54,27,83,41
69,75,31,47,56,45,95
11,84,22,66,27
59,16,99,48,82,98,97,56,63,69,12,61,55,67,96,17,84
31,98,39,16,78,24,85,72,87,56,48,95,99
32,33,27,35,72,87,54,74,31
43,33,27,24,39,72,31
87,74,47,16,54,72,45,56,97,82,31,75,83,59,35,48,98,92,63
61,55,67,36,96,11,17,76,22,79,26,43,66,46,44,32,33,27,77,41,24,39,35
33,84,72,79,24,95,41,36,27
41,24,35,95,47,87,92,54,88,59,31,16,99,85,82
97,56,45,79,76,16,36,17,61,11,22
82,45,97,98,16,61,67,54,88,48,63,78,74,12,99,96,31,85,11
17,22,66,44,77,41,35,95,75
96,17,84,76,22,79,43,46,44,32,27,77,41,24,39,35,95,47,83
85,98,56,11,45,96,78,63,69,31,22,67,36
72,16,99,24,27,87,41,47,39,54,75,44,92,77,33,74,31,59,88,32,46,35,95
26,43,32,33,27,39,72,54,88,59,74
69,61,55,67,96,11,84,22,79,66,46,32,33
88,48,75,59,31,54,56,82,74,92,45,78,12,55,87
74,31,16,99,85,48,78,82,98,97,56,63,45,69,12,61,55,67,96,11,17,84,76
11,43,78,46,61,55,96
72,83,92,54,88,74,31,16,85,48,78,98,63,69,61
76,41,33,36,11,77,24,39,84,66,61,27,67,55,17,22,35,44,32,79,26
48,11,82,96,97,45,16
32,33,27,35,47,72,83,92,54,88,74,31,99,85,48
48,82,98,56,63,45,69,61,67,96,76,79,26,43,66
66,41,33,32,27,36,96,43,11,79,24,17,95,46,84,44,35,76,39,67,47
54,74,85,78,97
39,35,95,47,72,83,87,75,54,88,59,74,31,16,99,85,48,78,82,98,97,56,63
88,31,16,48,78,98,97,63,12,61,36,96,17
47,31,39,43,54,77,66,35,44,59,72,92,24,32,95,88,33,27,46
11,17,84,26,66,46,44,32,33,27,41,24,39,47,72,83,87
82,97,55,17,84,43,44
67,82,99,87,63,61,88,55,48,69,45
67,69,84,24,55,41,11
75,16,45,12,55,67,36
74,31,48,97,12,96,17,84,76
83,72,66,24,46,96,17,35,79,22,43,44,32
35,95,47,72,83,92,54,88,74,85,78,97,56,63,45
46,77,87,39,35,22,47,95,33,44,92
75,41,59,74,43,95,24,72,26,77,32,44,66
36,96,84,79,26,66,46,32,33,27,77,41,39,35,95,47,72
35,87,33,46,22,92,88,79,47,41,24,44,32,72,54,43,27,83,39,95,66
36,11,17,26,41
84,76,22,79,43,33,27,77,24,35,95,75,92
11,79,66,27,39,95,87
39,24,32,26,12,17,44
84,43,83,35,41,76,22,32,46,27,79,87,92,47,24
61,43,22,84,67,36,46,78,63,12,11,96,17
77,41,24,39,95,83,87,54,74,31,48,82,98
59,98,78,75,97,63,45,61,88,72,99,82,69
78,67,55,74,63,56,31,36,92,12,45
63,45,67,96,26,43,66,44,77
96,36,77,22,76,61,27,12,55,39,33,79,26,84,32,43,67
82,98,69,16,75,85,78,67,87,12,63,56,99
82,96,54,63,69,99,55,74,11
95,47,72,83,87,92,54,88,59,74,31,16,99,48,97,63,69
11,84,22,79,26,66,46,44,32,33,27,77,41,24,39,35,95,47,72,83,87
41,92,99,31,82,72,24,98,88,48,16,47,78,74,59,95,97,75,85,35,87
72,83,75,92,54,59,74,31,16,85,48,82,98,97,63,45,69,12,61
""";
}