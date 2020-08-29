using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Unicode;

namespace unidump {
    class Program {
        private static string EMPTY = "";
        private const string RS = "\n";
        private const string FS = "\t";
        private const string ITEM_JOINER = ",";
        private const string DEFAULT_NONE_STRING_VALUE = "-";
        private const string GRP = "Grp";
        private const string GRPSEQ = "Grpseq";
        private const string NAME = "Name";
        private const string NUMERIC_VALUE = "NumericValue";
        private const string CODEPOINT = "codepoint";
        private const string GRAPH = "Graph";
        private const string BIDIRECTIONAL_CLASS = "BidirectionalClass";
        private const string BIDIRECTIONAL_MIRRORED = "BidirectionalMirrored";
        private const string BLOCK = "Block";
        private const string CANONICAL_COMBINING_CLASS = "CanonicalCombiningClass";
        private const string CANTONESE_READING = "CantoneseReading";
        private const string CATEGORY = "Category";
        private const string DECOMPOSITION_MAPPING = "DecompositionMapping";
        private const string DECOMPOSITION_TYPE = "DecompositionType";
        private const string DEFINITION = "Definition";
        private const string HANGUL_READING = "HangulReading";
        private const string JAPANESE_KUN_READING = "JapaneseKunReading";
        private const string JAPANESE_ON_READING = "JapaneseOnReading";
        private const string KOREAN_READING = "KoreanReading";
        private const string MANDARIN_READING = "MandarinReading";
        private const string ALIAS_NAME = "AliasName";
        private const string ALIAS_KIND = "AliasKind";
        private const string NUMERIC_TYPE = "NumericType";
        private const string OLD_NAME = "OldName";
        private const string SIMPLE_LOWER_CASE_MAPPING = "SimpleLowerCaseMapping";
        private const string SIMPLE_TITLE_CASE_MAPPING = "SimpleTitleCaseMapping";
        private const string SIMPLE_UPPER_CASE_MAPPING = "SimpleUpperCaseMapping";
        private const string SIMPLIFIED_VARIANT = "SimplifiedVariant";
        private const string TRADITIONAL_VARIANT = "TraditionalVariant";
        private const string UNICODE_RADICAL_STROKE_COUNTS = "UnicodeRadicalStrokeCounts";
        private const string VIETNAMESE_READING = "VietnameseReading";
        private const string IS_ASCII = "IsAscii";
        private const string IS_BMP = "IsBmp";
        private const string PLANE = "Plane";

        private static void Usage (string appName) {
            Console.WriteLine (
                RS +
                "Usage:" +
                RS +
                RS +
                "CMD: " + appName + " [codePointStart] [codePointEnd]" +
                RS
            );

            Environment.Exit (0);
        }

        static void Main (string[] args) {

            string appName = Environment.GetCommandLineArgs () [0];
            appName = Regex.Replace (appName, ".*/", EMPTY); //ファイル名のみにする
            appName = Regex.Replace (appName, "\\..*", EMPTY); //拡張子を取り除く

            List<string> cmdLineArgs = args.ToList ();

            if (cmdLineArgs.Count != 2) {
                Usage (appName);
            }

            int s = int.Parse (cmdLineArgs[0]);
            int e = int.Parse (cmdLineArgs[1]);

            if (s > e) {
                Usage (appName);
            }

            int c = e - s + 1;

            if (s < 0) {
                Usage (appName);
            }
            if (e < 0 || e > 1114112) {
                Usage (appName);
            }
            if (c < 0 || c > 1114112) {
                Usage (appName);
            }

            List<int> lowerCodePointList = Enumerable.Range (s, c).Where (i => i < 55296).ToList ();
            List<int> upperCodePointList = Enumerable.Range (s, c).Where (i => i > 57343).ToList ();

            List<int> codePointList = new List<int> ();

            if (0 != lowerCodePointList.Count) {
                codePointList.AddRange (lowerCodePointList);
            }
            if (0 != upperCodePointList.Count) {
                codePointList.AddRange (upperCodePointList);
            }

            if (0 == codePointList.Count) {
                Usage (appName);
            }

            int grp = 0;

            List<Dictionary<string, string>> summaryList = new List<Dictionary<string, string>> ();

            foreach (var codePoint in codePointList) {

                grp++;

                string item = Char.ConvertFromUtf32 (codePoint);

                List<char> charList = item.ToCharArray ().ToList ();

                var charInfo = UnicodeInfo.GetCharInfo (codePoint);

                int grpseq = 0;

                foreach (Char ch in charList) {

                    Dictionary<string, string> detailDict = new Dictionary<string, string> ();

                    grpseq++;

                    detailDict.Add (GRP, grp.ToString ());
                    detailDict.Add (GRPSEQ, grpseq.ToString ());
                    detailDict.Add (CODEPOINT, codePoint.ToString ());
                    detailDict.Add (GRAPH, item);
                    detailDict.Add (NAME, charInfo.Name);
                    detailDict.Add (NUMERIC_VALUE, charInfo.NumericValue.ToString () == "" ? DEFAULT_NONE_STRING_VALUE : charInfo.NumericValue.ToString ());
                    detailDict.Add (BIDIRECTIONAL_CLASS, charInfo.BidirectionalClass.ToString () == "" ? DEFAULT_NONE_STRING_VALUE : charInfo.BidirectionalClass.ToString ());
                    detailDict.Add (BIDIRECTIONAL_MIRRORED, charInfo.BidirectionalMirrored.ToString () == "" ? DEFAULT_NONE_STRING_VALUE : charInfo.BidirectionalMirrored.ToString ());
                    detailDict.Add (BLOCK, charInfo.Block.ToString () == "" ? DEFAULT_NONE_STRING_VALUE : charInfo.Block.ToString ());
                    detailDict.Add (CANONICAL_COMBINING_CLASS, charInfo.CanonicalCombiningClass.ToString () == "" ? DEFAULT_NONE_STRING_VALUE : charInfo.CanonicalCombiningClass.ToString ());
                    detailDict.Add (CANTONESE_READING, charInfo.CantoneseReading == null ? DEFAULT_NONE_STRING_VALUE : charInfo.CantoneseReading);
                    detailDict.Add (CATEGORY, charInfo.Category.ToString () == "" ? DEFAULT_NONE_STRING_VALUE : charInfo.Category.ToString ());
                    detailDict.Add (DECOMPOSITION_MAPPING, charInfo.DecompositionMapping == null ? DEFAULT_NONE_STRING_VALUE : charInfo.DecompositionMapping);
                    detailDict.Add (DECOMPOSITION_TYPE, charInfo.DecompositionType.ToString () == "" ? DEFAULT_NONE_STRING_VALUE : charInfo.DecompositionType.ToString ());
                    detailDict.Add (DEFINITION, charInfo.Definition == null ? DEFAULT_NONE_STRING_VALUE : charInfo.Definition);
                    detailDict.Add (HANGUL_READING, charInfo.HangulReading == null ? DEFAULT_NONE_STRING_VALUE : charInfo.HangulReading);
                    detailDict.Add (JAPANESE_KUN_READING, charInfo.JapaneseKunReading == null ? DEFAULT_NONE_STRING_VALUE : charInfo.JapaneseKunReading);
                    detailDict.Add (JAPANESE_ON_READING, charInfo.JapaneseOnReading == null ? DEFAULT_NONE_STRING_VALUE : charInfo.JapaneseOnReading);
                    detailDict.Add (KOREAN_READING, charInfo.KoreanReading == null ? DEFAULT_NONE_STRING_VALUE : charInfo.KoreanReading);
                    detailDict.Add (MANDARIN_READING, charInfo.MandarinReading == null ? DEFAULT_NONE_STRING_VALUE : charInfo.MandarinReading);
                    detailDict.Add (ALIAS_NAME, String.Join (ITEM_JOINER, charInfo.NameAliases.Select (unicodeNameAlias => unicodeNameAlias.Name == null ? DEFAULT_NONE_STRING_VALUE : unicodeNameAlias.Name).ToArray ()));
                    detailDict.Add (ALIAS_KIND, String.Join (ITEM_JOINER, charInfo.NameAliases.Select (unicodeNameAlias => unicodeNameAlias.Kind.ToString () == "" ? DEFAULT_NONE_STRING_VALUE : unicodeNameAlias.Kind.ToString ()).ToArray ()));
                    detailDict.Add (NUMERIC_TYPE, charInfo.NumericType.ToString () == "" ? DEFAULT_NONE_STRING_VALUE : charInfo.NumericType.ToString ());
                    detailDict.Add (OLD_NAME, charInfo.OldName == null ? DEFAULT_NONE_STRING_VALUE : charInfo.OldName);
                    detailDict.Add (SIMPLE_LOWER_CASE_MAPPING, charInfo.SimpleLowerCaseMapping == null ? DEFAULT_NONE_STRING_VALUE : charInfo.SimpleLowerCaseMapping);
                    detailDict.Add (SIMPLE_TITLE_CASE_MAPPING, charInfo.SimpleTitleCaseMapping == null ? DEFAULT_NONE_STRING_VALUE : charInfo.SimpleTitleCaseMapping);
                    detailDict.Add (SIMPLE_UPPER_CASE_MAPPING, charInfo.SimpleUpperCaseMapping == null ? DEFAULT_NONE_STRING_VALUE : charInfo.SimpleUpperCaseMapping);
                    detailDict.Add (SIMPLIFIED_VARIANT, charInfo.SimplifiedVariant == null ? DEFAULT_NONE_STRING_VALUE : charInfo.SimplifiedVariant);
                    detailDict.Add (TRADITIONAL_VARIANT, charInfo.TraditionalVariant == null ? DEFAULT_NONE_STRING_VALUE : charInfo.TraditionalVariant);
                    detailDict.Add (UNICODE_RADICAL_STROKE_COUNTS, String.Join (ITEM_JOINER, charInfo.UnicodeRadicalStrokeCounts.Select (unicodeRadicalStrokeCount => unicodeRadicalStrokeCount.Radical.ToString () == "" ? DEFAULT_NONE_STRING_VALUE : unicodeRadicalStrokeCount.Radical.ToString ()).ToArray ()));
                    detailDict.Add (VIETNAMESE_READING, charInfo.VietnameseReading == null ? DEFAULT_NONE_STRING_VALUE : charInfo.VietnameseReading);
                    detailDict.Add (IS_ASCII, String.Join (ITEM_JOINER, item.EnumerateRunes ().Select (rune => rune.IsAscii.ToString () == "" ? DEFAULT_NONE_STRING_VALUE : rune.IsAscii.ToString ()).ToArray ()));
                    detailDict.Add (IS_BMP, String.Join (ITEM_JOINER, item.EnumerateRunes ().Select (rune => rune.IsBmp.ToString () == "" ? DEFAULT_NONE_STRING_VALUE : rune.IsBmp.ToString ()).ToArray ()));
                    detailDict.Add (PLANE, String.Join (ITEM_JOINER, item.EnumerateRunes ().Select (rune => rune.Plane.ToString () == "" ? DEFAULT_NONE_STRING_VALUE : rune.Plane.ToString ()).ToArray ()));

                    summaryList.Add (detailDict);
                }

            }

            //header
            Console.WriteLine (String.Join (FS, summaryList[0].Keys.ToArray ()));

            //body
            foreach (var itemDict in summaryList) {
                List<string> keyList = itemDict.Keys.ToList ();
                int cnt = keyList.Count;
                for (int i = 0; i < cnt; i++) {
                    {
                        Console.Write (itemDict[keyList[i]]);
                        if (i != cnt - 1) {
                            Console.Write (FS);
                        }
                    }
                }

                Console.WriteLine ();
            }
        }
    }
}
