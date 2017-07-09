using System;
using System.Linq;
namespace core.mlcandidat{

    public class MlCandidat{
        /*
        public string session_id{get;set;}
        public int id{get;set;}
         */
        public double satisfaction_level{get;set;} // 0 - 1
        public double last_evaluation{get;set;} // 0 - 1
        public int number_project{get;set;} // 0 - Infini
        public int average_montly_hours{get;set;} //  0 - 720
        public int time_spend_company{get;set;} // 0 - 50
        public int Work_accident{get;set;} // 0 - 1
        public int left_work{get;set;} // 0 - 1
        public int promotion_last_5years{get;set;} // 0-1
        public string sales{get;set;}// Present dans le champ
        public string salary{get;set;}//  low - middle - high

        private string[] salesChoices = { "sales", "accounting", "hr","technical","management","IT","product_mng","marketing","RandD"};
        private string[] salaryChoices = {"low","middle","high"};

        private bool containSalesChoice(string choice){
            return salesChoices.Any(choice.Equals);
        }

        private bool containSalaryChoices(string salaryType){
            return salaryChoices.Any(salary.Equals);
        }

        public void checkCandidateProperties(){
            
            if(satisfaction_level < 0 || satisfaction_level > 1){
                throw new Exception("Le level de satisfaction doit etre en 0 et 1");
            }
            if(last_evaluation<0 || last_evaluation > 1){
                throw new Exception("La dernière evaluation doit etre en 0 et 1");
            }
            if(number_project<0){
                throw new Exception("Le nombre de projet ne peut pas etre inférrieur à 0");
            }
            if(average_montly_hours < 0 || average_montly_hours > 720){
                throw new Exception("Le nombre d'heures ne peut excéder 720 par semaine");
            }
            if(time_spend_company < 0 || time_spend_company > 50){
                throw new Exception("Le nombre de temps resté en entreprise ne peut etre inférieur à 0 ou suppérieur à 50");
            }
            if(Work_accident < 0 || Work_accident > 1){
                throw new Exception("Le choi doit etre 0 si il n'ya jammais eu d'accident ou 1 si il ya eu un accident");
            }
            if(left_work < 0 || left_work > 1){
                throw new Exception("Soit 0 (le candidat a quitté l'entreprise) ou 1 (Le candidat n'a pas quitté l'entreprise)");
            }
            if(promotion_last_5years < 0 || promotion_last_5years > 1){
                throw new Exception("Le choi est soit 0 ou 1 ");
            }
            if(!containSalesChoice(sales)){
                throw new Exception($"Le choi {sales} n'existe pas");
            }
            if(!containSalaryChoices(salary)){
                throw new Exception($"Le type de salaire {salary} n'existe pas");
            }

        }

    }

}