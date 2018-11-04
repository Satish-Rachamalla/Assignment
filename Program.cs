using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution
{
    class Assignment
    {
        List<int> Storage = new List<int>();//To temporatily store the minutes/hours
        Random random = new Random();//creating Random object to calculate random numbers
        static void Main(string[] args)
        {
        l:
            {
             
            }
            try
            {
                Console.WriteLine("Enter Total no. of Rewards");
                int rewarditems = int.Parse(Console.ReadLine());//no. of rewards(total)
                Console.WriteLine("Enter no. of CampaignDays");
                int campaigndays = int.Parse(Console.ReadLine());//no. of campaigndays(total)
                int dailylimit = 0, left, overallextras, flag = 0;
                dailylimit = rewarditems / campaigndays;//Rewards that should be distributed per day
                overallextras = rewarditems % campaigndays;//If dailylimit has a decimal value then we need to store extras which should be distributed
                if (dailylimit > 720)//If an hour consists of more than 30 wins then random becomes criticial or useless 
                {
                    Console.WriteLine("You can't distribute these many wins in a day in an optimal manner");
                    goto l;//Resending user to try again
                }
                Assignment obj = new Assignment();
                for (int i = 0; i < campaigndays; i++)
                {
                    if (overallextras > 0)//This will set flag until overallextras are exhausted
                        flag = 1;
                    else
                        flag = 0;
                    Console.WriteLine("Enter no. of Rewards Left");
                    left = int.Parse(Console.ReadLine());//Number of Rewards left for that day
                    Console.WriteLine("Day" + (i + 1));//Prints the Day number
                    obj.DailyWinsPerMinute(dailylimit + left + flag);/*Calling the method that calculates random minute in a particular hour for a particular day
                                            arguments include total dailylimit along with extra that were left previous day */
                    overallextras--;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Please Try Again");
                goto l;//Resending user to try again
            }
        }
        public void DailyWinsPerMinute(int dailylimit)
        {          
            int y = 0;//flag
            int perhour = dailylimit/ 24;//No. of wins/hour
            int extrasperday = dailylimit % 24;//If dailylimit is not exactly divisbile by 24 then we have remainder stored in extra
            int totalwins = perhour + extrasperday;//Total no. of wins(including extra)                     
            if (perhour<1)//This condition exists if the rewards/hr is too low i.e not even 1reward/hr
            {               
                    PerHourDistribution(0,extrasperday,dailylimit);//we call the distribution/hr with the dailylimit as argument
                    Print(dailylimit);//This method is to print the data with dailylimit as argument       
                extrasperday--;
            }            
            else//This executes when rewards/hr are greater than 1 i.e, atleast 1reward/every hour
            {
                for (int k = 1; k <= 24; k++)//Iterated for 24 hours
                {
                    Console.WriteLine(k + "Hour");//To print the Day value eg. Day 1,Day 2 etc
                    if (extrasperday > 0)//If extra is greater than 0 we put a flag of 1
                    {
                        y = 1;
                    }
                    else//After all the extras are added to a particular hour we set it to zero
                    {
                        y = 0;
                    }
                    PerHourDistribution(perhour, y);//This calls the distribution/hr with flag and perhour as arguments
                    Print(dailylimit,perhour+y);//This method is to print dailywins/minute
                    extrasperday--;//extra is decreased after every hour execution
                }
            }
        }
        /// <summary>
        /// This is a generic method which is common for calculating /hr and /minute based on input so all the parameters are optional
        /// </summary>
        public  void PerHourDistribution(int perhour=0,int extra=0,int perday=0)
        {
            int flag1,max,randomnum;
            if(perhour!=0)//It will become 0 if wins/minute<1 ie. we get few wins in a whole day(wins/day<24)
            {
                flag1 = (perhour + extra);
                max = 60;//This variable is to set the maximum limit for random calculator(60because in an hour we have 60 minutes)
            }
            else
            {
                flag1 = perday + extra;//flag is set to no. of hours added with extrahours
                while (extra > 0)
                {
                    flag1++;//If extra hours are there then we will increment the flag by 1
                    break;
                }
                
                max = 24;//This variable is to set the maximum limit for random calculator(In a day we have 24 hours)
            }
            for (int i = 0; i <flag1 ; i++)//This loop is to iterate over perday or perhour(based on input)
            {
                do
                {
                    randomnum = random.Next(1,max);//Random number calculator

                } while (Storage.Contains(randomnum));//To select list of unique random numbers
                Storage.Add(randomnum);//Adding the numbers to list(These are the minutes or hours where user wins,it is based on dailylimit)
            }
        }
        ///It is a generic print function common for perhour and perday
        public void Print(int perday,int minute=0)
        {
            int flag;
            string s;
            if (perday < 24)
            {
                flag = perday;//flag will be set for perday
                s = " hour";//To represent hour to user           
            }
            else
            {
                flag = minute;//flag will be set for perhour including extra wins
                s = " minute";
                Storage.Sort();//Sorting of all the random minutes/hours so that it's easy to understand
            }
            for (int k = 1; k <= flag; k++)
            {
                if(flag!=perday)
                Console.WriteLine(Storage[k - 1]+s);//Iteration based on flag and printing the wins/hour in terms of minutes
                else
                    Console.WriteLine(Storage[k - 1]+s+" "+random.Next(1,60)+" minute");  //Iteration based on flag and printing the wins/day in terms of hours and minutes
            }
            Storage.Clear();//clearing the list for reusability
        }
    }
}
