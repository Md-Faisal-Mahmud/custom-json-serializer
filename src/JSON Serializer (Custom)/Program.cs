using JSON_Serializer__Custom_;

class Program
{
    public static void Main(string[] args)
    {
        Course course = new Course();

        //course.Title = ".net";
        //course.Title = 111111111;
        course.Title = 743894.343;

        course.Top = new List<Topic>()
        {
            new Topic
                    {
                         SBYTE1 = 1,
                         SBYTE2 = 1,

                         BYTE1 = 100,
                         BYTE2 = 78,

                         char1 = 'a',
                         char2 = 'b',

                        Title= "Title456",
                        id = null,
                        Description= "Description",
                        Sessions = new List<Session>
                        {
                            new Session
                            {
                                DurationInHour = 2,
                                LearningObjective = null
                            }
                        }
                    }
            ,
            new Topic
                    {
                         SBYTE1 = -1,
                         SBYTE2 = -11,
                        id = null,
                        Title= "Title 123",
                        Description= "Description",
                        Sessions = new List<Session>
                        {
                            new Session
                            {
                                idx = 1,
                                DurationInHour = 2,
                                LearningObjective = null
                            }
                        }
                    }
        };

        course.Fees1 = 30000;
        course.Fees2 = 30000;
        course.Fees3 = 30000;
        course.Fees4 = (float)30000.34;
        course.Fees5 = 30000;
        course.Fees6 = 30000.343M;

        course.IsValid1 = true;
        course.IsValid2 = false;
        //course.myEnumYes = Course.EnumValues.yes;
        course.myEnumOkay = Course.EnumValues.okays;

        course.myStruct = new StructData()
        {
            x = 3,
            y = 4
        };

        course.Teacher = new Instructor()
        {
            Name = "Jalaluddin",
            Email = "jalalsir.exe@gmail.com",
            PresentAddress = new Address()
            {
                Street = "4/2",
                City = "Dhaka",
                Country = "Bangladesh",
                State = 's'
            },
            PermanentAddress = new Address()
            {
                Street = "5/2",
                City = "Mirpur",
                Country = "Bangladesh"
            },
            PhoneNumbers = new List<Phone>
            {
                new Phone()
                {
                    Number = "12343435",
                    Extension = "017",
                    CountryCode = "+880"
                },
                new Phone()
                {
                    Number = "12367891",
                    Extension = null,
                    CountryCode = "+880"
                }
            }
        };

        course.Topics = new List<Topic>
                {
                    new Topic
                    {
                        id = null,
                        Title= "Title",
                        Description= "Description",
                        Sessions = new List<Session>
                        {
                            new Session
                            {
                                idx = 222,
                                DurationInHour = 2,
                                LearningObjective = "Reflection"
                            }
                        }
                    }
                };

        course.Tests = new List<AdmissionTest>
                {
                    new AdmissionTest
                    {
                        TestFees = 100,
                        StartDateTime = new DateTime(2023,01,25,9,00,00),
                        EndDateTime= new DateTime(2023,01,25,11,00,00)
                    },
                    new AdmissionTest
                    {
                        TestFees = 100,
                        StartDateTime = new DateTime(2023, 1, 25, 9, 0, 0),
                        EndDateTime = new DateTime(2023, 1, 25, 15, 0, 0)
                    }
                };

        string json = JsonFormatter.Convert(course);
        Console.WriteLine(json);
    }
}