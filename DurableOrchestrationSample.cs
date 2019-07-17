using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace Hollan.Function
{
    public class Session
    {
        public int attendees;
        public IDictionary<string, double> reviews = new Dictionary<string, double>();
        public void ScanIn() => attendees++;
        public void SubmitEval(Evaluation eval) 
        {
            reviews.Add(eval.Attendee, eval.Score);
        }
        public double AverageScore { 
            get {
                return reviews.Count > 0 ? reviews.Values.Average() : 0.0;
            }
        }

        [FunctionName(nameof(Session))]
        public Task Run([EntityTrigger] IDurableEntityContext ctx) 
                => ctx.DispatchAsync<Session>();   
    }





    public class Evaluation
    {
        public string Attendee { get; set; }
        public double Score { get; set; }
    }
}