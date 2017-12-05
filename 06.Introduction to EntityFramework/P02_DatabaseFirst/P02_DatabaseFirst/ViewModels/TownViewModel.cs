namespace P02_DatabaseFirst.ViewModels
{
    public class TownViewModel
    {
        public TownViewModel()
        {
            
        }
        
        public TownViewModel(string name,int residentCount)
        {
            this.Name = name;
            this.ResidentCount = residentCount;
        }

        public string Name { get; set; }

        public int ResidentCount { get; set; }

    }
}
