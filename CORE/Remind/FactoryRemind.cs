using System;
using System.IO;
namespace Candidate_Management.CORE.Remind
{
    public class FactoryRemind
    {
        public static Iremind createRemind(string remindString){
            try{
                string remindToLoad = $"Candidate_Management.CORE.Remind.{remindString}";
                Console.WriteLine(remindToLoad);
                return (Iremind)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(remindToLoad);
                
            }catch(TypeLoadException type){
                Console.WriteLine("In type remind exception");
                throw new Exception($"Il est impossible de charger votre type de remind : {remindString} - {type.Message}");
            }catch(FileNotFoundException exce){
                Console.WriteLine("In type FileNotFoundException exception");
                throw new Exception($"Aucun remind existe pour votre action : {remindString}");
            }catch(Exception exc){
                Console.WriteLine("In regular exception");
                throw exc;
            }
        }
    }
}