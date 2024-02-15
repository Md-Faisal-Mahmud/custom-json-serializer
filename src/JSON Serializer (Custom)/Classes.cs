using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JSON_Serializer__Custom_
{
    public class Course
    {

        public object Title;

        public List<Topic> Top { get; set; }
        public Instructor Teacher { get; set; }
        public List<Topic> Topics { get; set; }
        public float Fees1 { get; set; }
        public double Fees2 { get; set; }
        public decimal Fees3 { get; set; }

        public float Fees4;
        public double Fees5;
        public decimal Fees6;

        public bool IsValid1;
        public bool IsValid2 { get; set; }

        public enum EnumValues
        {
            yes = 1,
            no,
            okays
        }

        public EnumValues myEnumYes { get; set; }
        public EnumValues myEnumOkay;

        public StructData myStruct;


        public List<AdmissionTest> Tests { get; set; }
    }

    public struct StructData
    {
        public int x;
        public int y;
    }

    public class AdmissionTest
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double TestFees { get; set; }
    }

    public class Topic
    {
        public sbyte SBYTE1 { get; set; }
        public sbyte SBYTE2;
        public byte BYTE1 { get; set; }
        public byte BYTE2;

        public short short1 { get; set; }
        public short short2;


        public ushort ushort1 { get; set; }
        public ushort ushort2;


        public uint uint1 { get; set; }
        public uint uint2;

        public float float1 { get; set; }
        public float float2;


        public char char2;

        public char char1 { get; set; }


        public string Title { get; set; }
        public string id;
        public string Description { get; set; }
        public List<Session> Sessions { get; set; }
    }

    public class Session
    {
        public int idx;
        public int DurationInHour { get; set; }
        public string LearningObjective { get; set; }

    }

    public class Instructor
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Address PresentAddress { get; set; }
        public Address PermanentAddress { get; set; }
        public List<Phone> PhoneNumbers { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public char State { get; set; }
    }

    public class Phone
    {
        public string Number { get; set; }
        public string Extension { get; set; }
        public string CountryCode { get; set; }
    }

}
