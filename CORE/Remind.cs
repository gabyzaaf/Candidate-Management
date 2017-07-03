namespace core.remind{

    public class Remind{

        public string date{get;set;}
        public int candidateId {get;set;}
        public bool finish{get;set;}

        public override string ToString(){
            return $"the date is {date} - the id is {candidateId} - finish {finish} ";
        }

    }

}