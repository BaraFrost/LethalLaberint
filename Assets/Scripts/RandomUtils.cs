using System;
using System.Collections.Generic;

namespace Utils {

    public static class RandomUtils {

        private static readonly System.Random _rnd = new System.Random();


        public static int Next(int maxExclusive) {
            return _rnd.Next(maxExclusive);
        }

        public static float Next(float maxExclusive) {
            return (maxExclusive * (float)_rnd.NextDouble());
        }

        public static int Next(int minInclusive, int maxExclusive) {
            return _rnd.Next(minInclusive, maxExclusive);
        }

        public static float Next(float minInclusive, float maxExclusive) {
            return ((float)_rnd.NextDouble()) * (maxExclusive - minInclusive) + minInclusive;
        }
        public static bool NextBool() {
            return _rnd.Next(2) == 0;
        }

        public static T PickRandom<T>(IList<T> array) {
            return array[Next(array.Count)];
        }

        public static T PickRandom<T>(this IEnumerable<T> enumerable) {
            T current = default(T);
            int count = 0;
            foreach (T element in enumerable) {
                count++;
                if (Next(count) == 0) {
                    current = element;
                }
            }
            if (count == 0) {
                throw new System.InvalidOperationException("Sequence was empty");
            }
            return current;
        }

        public static T[] Shuffle<T>(T[] array) => Shuffle(array, 0, array.Length);

        public static T[] Shuffle<T>(T[] array, int count) => Shuffle(array, 0, count);

        public static T[] Shuffle<T>(T[] array, int start, int count) {
            start = Math.Max(start, 0);
            var finish = Math.Min(start + count, array.Length);
            for (var i = start + 1; i < finish; i++) {
                var j = Next(i - start + 1) + start;
                (array[i], array[j]) = (array[j], array[i]);
            }
            return array;
        }

        public static T PickWeightedRandom<T>(IEnumerable<T> enumerable, Func<T, int> weightGetter) {
            var totalWeight = 0;
            foreach (var item in enumerable) {
                totalWeight += weightGetter(item);
            }

            var randomNumber = Next(totalWeight);
            foreach (var item in enumerable) {
                var itemWeight = weightGetter(item);
                if (randomNumber < itemWeight) {
                    return item;
                }
                randomNumber -= itemWeight;
            }

            return default;
        }
    }
}