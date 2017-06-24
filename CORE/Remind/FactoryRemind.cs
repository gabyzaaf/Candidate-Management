using System;
namespace Candidate_Management.CORE.Remind
{
    public class FactoryRemind
    {
        public static Iremind createRemind(string remindString){
            try{
                string remindToLoad = $"Candidate_Management.CORE.Remind.{remindString}";
                
                return (Iremind)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(remindToLoad);
                
            }catch(TypeLoadException type){
                throw new Exception($"Il est impossible de charger votre type de remind : {remindString} - {type.Message}");
            }catch(Exception exc){
                throw new Exception($"Il est impossible de charger votre type de remind : {remindString} - {exc.Message}");
            }  
        }
    }
}