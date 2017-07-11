using System;
using System.Collections.Generic;
using System.IO;
using core.mlcandidat;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace core.prediction{
/*
    Author : LAPERT Christian
    Version : 1.0
 */
public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }

    }

public class Resultat{
    public string valeurAzure{get; set;}
    public string probAzure{get; set;}
    private static Resultat currentResultat = null;
   
    public static Resultat getcurrentResultat(){
        if (currentResultat == null){
            currentResultat = new Resultat();
        }
        return currentResultat;
    }
    public static void setCurrentResultat(Resultat rst){
        currentResultat = rst;
    }
}

class Prediction
    {   

        public static async Task InvokeRequestResponseService(MlCandidat prediCandidate)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"satisfaction_level", "last_evaluation", "number_project", "average_montly_hours", "time_spend_company", "Work_accident", "promotion_last_5years", "sales", "salary"},
                                Values = new string[,] {  { prediCandidate.satisfaction_level.ToString(), prediCandidate.last_evaluation.ToString(), prediCandidate.number_project.ToString(), prediCandidate.average_montly_hours.ToString(), prediCandidate.time_spend_company.ToString(), prediCandidate.Work_accident.ToString(), prediCandidate.promotion_last_5years.ToString(), prediCandidate.sales, prediCandidate.salary }  }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                        
                    }
                };
                const string apiKey = "lvoOpIc0yr72Hozn3/+MLJtCuH8Ki8se1pxwNESj/CK/H0RLqDoI8SblrgWQ0e5wHLB+1dRliLPMwH2n3ktKYA=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/f976db5dc6194a59a479f37fc96dffd4/services/ce01fbb1b995484582eaaa8639bd3884/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    string resultValue = result.Substring(147,1);
                    string resultProb = result.Substring(151,4);
                    Resultat rst = Resultat.getcurrentResultat();
                    rst.valeurAzure = result.Substring(147,1);
                    rst.probAzure = result.Substring(151,4);
                    Resultat.setCurrentResultat(rst);
                   
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }
    }
}