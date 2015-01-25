using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class StringComparators
    {
        //Hmm... should probably pass in queryValue first... so when/if we cache stuff it makes more sense.
        public static double LevensteinDist(this string fullPhrase, string queryValue, bool prefixAllowed = false)
        {
            queryValue = queryValue ?? "";

            //Hmm... we can do levenshtein distance... but say:
            //1) Spring Into Action 10K
            //2) Spring Into Action
            //3) Sports Into Action
            //1 to 2 and 1 to 3 are the same levenshtein dist, but
            //  obviously the 10K at the end is less different than a different first word...
            //So we do a modified levenshtein distance, 

            //Short variable names so I can type it faster... don't even read this crap
            //Just read http://en.wikipedia.org/wiki/Levenshtein_distance
            var x = queryValue;
            var y = fullPhrase;

            var cx = x.Length;
            var cy = y.Length;

            double[,] costMatrix = new double[cx, cy];

            //A deletion and insertion are equivalent (like in reality,
            //  any time you have to do one, you could just do the other
            //  on the opposite string).

            Func<int, double> deleteXCost = (index) =>
            {
                return 1;
                //return 1 - index / (double)cx * 0.1;
            };
            var deleteYCost = 1;

            var matchCost = 0;// -0.15;
            var substitutionCost = 2;

            var maxChCost = Math.Max(deleteYCost, substitutionCost);

            Func<int, int, double> costLookup = (int ix, int iy) =>
                ix == -1 && iy == -1 ? 0 :
                ix < 0 ? Math.Max(maxChCost, iy * maxChCost) :
                iy < 0 ? Math.Max(maxChCost, ix * maxChCost) :
                costMatrix[ix, iy];

            for (var ix = 0; ix < cx; ix++)
            {
                for (var iy = 0; iy < cy; iy++)
                {
                    double chCost = x[ix] == y[iy] ?
                        matchCost :
                        substitutionCost;

                    var delXCost = deleteXCost(ix);

                    //Just to weight exact matches a bit more earlier on in some searches
                    if (ix != iy) chCost += 0.01;

                    costMatrix[ix, iy] = Math.Min(
                        Math.Min(
                            costLookup(ix - 1, iy) + delXCost,
                            costLookup(ix, iy - 1) + deleteYCost
                        ),
                        costLookup(ix - 1, iy - 1) + chCost
                    );
                }
            }

            var maxCost =
                Math.Max(
                    deleteYCost * (cy),
                    maxChCost * (cy)
                );

            var cost = costLookup(cx - 1, cy - 1);
            if (prefixAllowed)
            {
                var prefixDeletionCost = 0;
                //Go down the entire x length, we must use up the whole y word
                //  (query word) but not the x word (the database word), as we may
                //  just be the beginning of it, which is okay.
                for (var ix = 0; ix < cx; ix++)
                {
                    cost = Math.Min(cost,
                        costLookup(ix, cy - 1) + (cx - ix - 1) * prefixDeletionCost //Every deletion only costs x
                    );
                }
            }

            return cost;
        }
    }
}
