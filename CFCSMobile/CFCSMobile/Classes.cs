using System;
using System.Collections.Generic;
using System.Text;

namespace CFCSMobile
{
    public class TheLookups
    {
        public List<CodedDescriptor> LOOKUP_V_DETOX;
        public List<CodedDescriptor> LOOKUP_V_EMERSTAB;
        public List<CodedDescriptor> LOOKUP_V_INCIDENTAL;
        public List<CodedDescriptor> LOOKUP_V_PREVENTION;
        public List<CodedDescriptor> LOOKUP_V_RECOVERY;
        public List<CodedDescriptor> LOOKUP_V_RECOVERYCCST;
        public List<CodedDescriptor> LOOKUP_V_RECOVERYFACT;
        public List<CodedDescriptor> LOOKUP_V_RECOVERYINCIDENTAL;
        public List<CodedDescriptor> LOOKUP_V_TREATMENTANDAFTERCARE;
        public List<ProviderDesignation> LOOKUP211REFERRALPROVIDERS;
        public List<CodedDescriptor> LOOKUPADDRESSTYPE;
        public List<CodedDescriptor> LOOKUPADMISSIONTYPE;
        public List<CodedDescriptor> LOOKUPALERTTYPE;
        public List<CodedDescriptor> LOOKUPASSEMENTKINDS;
        public List<CodedDescriptor> LOOKUPASSMTTYPE;
        public List<CodedDescriptor> LOOKUPCOMMERCIALINSURANCE;
        public List<CodedDescriptor> LOOKUPCOMMUNITYSUPPORTSTYPE;
        public List<CodedDescriptor> LOOKUPCONCURRENT211REFERRALTARGETS;
        public List<CodedDescriptor> LOOKUPCONTACTTYPE;
        public List<CodedDescriptorExt> LOOKUPUNITTYPE;
        public List<CodedDescriptorExt> LOOKUPCOSTCENTER;
        public List<CodedDescriptor> LOOKUPCOUNTYNAME;
        public List<CodedDescriptor> LOOKUPDISCHARGEREASON;
        public List<CodedDescriptor> LOOKUPDISCHARGETYPE;
        public List<CodedDescriptor> LOOKUPDISPOSITIONS;
        public List<CodedDescriptor> LOOKUPDOCUMENTTYPES;
        public List<CodedDescriptor> LOOKUPDRUGOFCHOICE;
        public List<CodedDescriptor> LOOKUPEDUCATION;
        public List<CodedDescriptor> LOOKUPEMPLOYMENT;
        public List<CodedDescriptor> LOOKUPENCOUNTERRELATION;
        public List<CodedDescriptor> LOOKUPENCOUNTERSDS;
        public List<CodedDescriptor> LOOKUPENCOUNTERSTATUS;
        public List<CodedDescriptor> LOOKUPENCOUNTERSTATUSNOBILLED;
        public List<CodedDescriptor> LOOKUPENCOUNTERSTATUSALL;
        public List<CodedDescriptor> LOOKUPETHNICITY;
        public List<CodedDescriptorWPrograms> LOOKUPFACILITY;
        public List<CodedDescriptor> LOOKUPFACILITYTYPE;
        public List<CodedDescriptor> LOOKUPFOLLOWUPPROVTYPE;
        public List<CodedDescriptor> LOOKUPFOLLOWUPREASONS;
        public List<CodedDescriptor> LOOKUPFREQ;
        public List<CodedDescriptor> LOOKUPFUNDERS;
        public List<CodedDescriptor> LOOKUPFUNDERSFLEXFUND;
        public List<CodedDescriptor> LOOKUPFUNDERSGROUPHEALTH;
        public List<CodedDescriptor> LOOKUPFUNDING;
        public List<CodedDescriptor> LOOKUPGENDER;
        public List<GMRRegions> LOOKUPGMRegions;
        public List<CodedDescriptor> LOOKUPGROUPINFORMATION;
        public List<CodedDescriptor> LOOKUPHEALTHSTATUS;
        public List<CodedDescriptor> LOOKUPHIERARCHY;
        public List<CodedDescriptor> LOOKUPINCIDENTALEXPENSECATEGORIES;
        public List<CodedDescriptor> LOOKUPINCOMESOURCE;
        public List<CodedDescriptor> LOOKUPLANGUAGES;
        public List<CodedDescriptor> LOOKUPLEGALSTATUS;
        public List<CodedDescriptor> LOOKUPLIVINGSIT;
        public List<CodedDescriptor> LOOKUPLOOKUPTABLES;
        public List<CodedDescriptor> LOOKUPMARCHMANACT;
        public List<CodedDescriptor> LOOKUPMARITALSTATUS;
        public List<CodedDescriptor> LOOKUPMEDCARECOVERAGEGROUPS;
        public List<CodedDescriptor> LOOKUPMEDICAIDTYPE;
        public List<NumericDescriptor> LOOKUPMEDS;
        public List<CodedDescriptor> LOOKUPMEMBERWATCHLIST;
        public List<CodedDescriptor> LOOKUPMESSAGETYPE;
        public List<CodedDescriptor> LOOKUPMHPROBLEM;
        public List<CodedDescriptor> LOOKUPNOTECONTACTTYPE;
        public List<CodedDescriptor> LOOKUPOPFUTYPE;
        public List<CodedDescriptor> LOOKUPOTHERSYSTEMS;
        public List<CodedDescriptor> LOOKUPPARTICIPANTTYPE;
        public List<CodedDescriptor> LOOKUPPHONETYPES;
        public List<CodedDescriptor> LOOKUPPREGTRIMESTER;
        public List<CodedDescriptor> LOOKUPPROGRAM;
        public List<CodedDescriptor> LOOKUPPROGRAMDISCHARGEREASONS;
        public List<CodedDescriptor> LOOKUPPROGRAMEVALPURPOSE;
        public List<CodedDescriptor> LOOKUPPROGRAMMEMBERSHIP;
        public List<CodedDescriptor> LOOKUPPROGRAMTYPE;
        public List<CodedDescriptor> LOOKUPPROGRESSNOTETYPES;

        public List<ProviderDesignation> LOOKUPPROVIDERS;
        public List<ProviderDesignation> LOOKUPPROVIDERSFORTRACKINGELEMENTS;
        public List<CodedDescriptor> LOOKUPPROVIDERSPECIALTY;
        public List<CodedDescriptor> LOOKUPPROVIDERTYPE;

        public List<CodedDescriptor> LOOKUPPURPOSEASAM;
        public List<CodedDescriptor> LOOKUPPURPOSEOFEVAL;
        public List<CodedDescriptor> LOOKUPPURPOSEOFEVALDCF;
        public List<CodedDescriptor> LOOKUPRACE;
        public List<CodedDescriptor> LOOKUPRECLEVELOFCARE;
        public List<CodedDescriptor> LOOKUPREFERRALREASON;
        public List<CodedDescriptor> LOOKUPREFERRALSOURCE;
        public List<CodedDescriptorWithOther> LOOKUPREFSOURCEBLUEBOOK;
        public List<CodedDescriptor> LOOKUPREMCONTACTWITH;
        public List<CodedDescriptor> LOOKUPREMNOTETYPE;
        public List<CodedDescriptor> LOOKUPRESIDENTSTATUS;

        //public List<CodedDescriptor> LOOKUPROI;
        public List<CodedDescriptorWithActive> LOOKUPROI;

        public List<CodedDescriptor> LOOKUPROLES;
        public List<CodedDescriptor> LOOKUPSAFETYASSESSMENTLVL;
        public List<CodedDescriptor> LOOKUPSCALE;
        public List<CodedDescriptor> LOOKUPSCREENINGPROGRAM;
        public List<ServiceGapDescriptor> LOOKUPSERVICEGAPS;
        public List<CodedDescriptor> LOOKUPSERVICEGAPVERIFICATIONMETHODS;
        public List<ServiceDescription> LOOKUPSERVICES;
        public List<ServiceDescription> LOOKUPSERVICESNOID;
        public List<ServiceDescription> LOOKUPSERVICESNOIDNOSPLIT;
        public List<CodedDescriptor> LOOKUPSETARGETTYPE;
        public List<CodedDescriptor> LOOKUPSETYPE;
        public List<CodedDescriptor> LOOKUPSUPPORTSRELATIONSHIP;
        public List<CodedDescriptor> LOOKUPSUPPORTSRELATIONSHIPTEAM;
        public List<CodedDescriptor> LOOKUPSUPPORTSRELATIONSHIPFAMILY;
        public List<CodedDescriptor> LOOKUPTANFSTATUS;
        public List<CodedDescriptor> LOOKUPTHERAPIES;
        public List<CodedDescriptor> LOOKUPTREATMENTPLAN_NEEDPROGRESS;
        public List<ZipCodeDescriptor> LOOKUPZIPS;

        public List<CodedDescriptorWID> LOOKUPSUPERVISORS;
        public List<CodedDescriptorWID> LOOKUPUSERSWITHID;
        public List<CodedDescriptor> LOOKUPMEMBERAUTHS;
        public List<CodedDescriptor> LOOKUPCOUNTIES;
        public List<CodedDescriptor> LOOKUPPAYER;
        public List<CodedDescriptor> LOOKUPDSP;
        public List<CodedDescriptor> LOOKUPCASESTATUS;
        public List<CodedDescriptor> LOOKUPCASESETTING;
        public List<CodedDescriptor> LOOKUPTASKS;
        public List<CodedDescriptor> LOOKUPTASKSTEPS;
        public List<CodedDescriptor> LOOKUPTASKSTATUS;
        public List<CodedDescriptor> LOOKUPFUNDERSFORENCOUNTERS;
        public List<CodedDescriptor> LOOKUPDISTINCTSERVICES;
        public List<CodedDescriptorWID> LOOKUPDISTINCTSERVICESWITHID;
        public List<CodedDescriptor> LOOKUPRELIGION;
        public List<CodedDescriptor> LOOKUPCOMPETENCIES;
        public List<CodedDescriptor> LOOKUPCREDENTIALS;
        public List<CodedDescriptor> LOOKUPACTIVEMEMBERS;
        public List<CodedDescriptor> LOOKUPWAITLISTREASON;
        // Added for HealthIntech

        public List<CodedDescriptor> LOOKUPSLEEPPROBLEM;
        public List<CodedDescriptor> LOOKUPINSIGHTDEGREE;
        public List<CodedDescriptor> LOOKUPALCOHOLCONSUMED30LIST;
        public List<CodedDescriptor> LOOKUPINTOXICSUBSTANCETIME;
        public List<CodedDescriptor> LOOKUPINJECTIONDRUGUSE;
        public List<CodedDescriptor> LOOKUPWITHDRAWALSYMPTOMS;
        public List<CodedDescriptor> LOOKUPINJURIOUSATTEMPT;
        public List<CodedDescriptor> LOOKUPSELFINJURIOUSTOKILL;
        public List<CodedDescriptor> LOOKUPVIOLENCE;
        public List<CodedDescriptor> LOOKUPBEHAVIOURDISTURBANCE;
        public List<CodedDescriptor> LOOKUPPOLICEINTERVENTION;
        public List<CodedDescriptor> LOOKUPDISORDEREDTHINKING;
        public List<CodedDescriptor> LOOKUPDECISIONMAKING90;
        public List<CodedDescriptor> LOOKUPIADLCAPACITY;
        public List<CodedDescriptor> LOOKUPADLSTATUS90;
        public List<CodedDescriptor> LOOKUPLIFEEVENTS;
        public List<CodedDescriptor> LOOKUPINTENSEFEAREVENTS;
        public List<CodedDescriptor> LOOKUPSELFUNDERSTOOD;
        public List<CodedDescriptor> LOOKUPUNDERSTANDOTHERS;
        public List<CodedDescriptor> LOOKUPPSYCHIATRICCONTACT;
        public List<CodedDescriptor> LOOKUPDISCHARGEFROMPSYCHIATRIC;
        public List<CodedDescriptor> LOOKUPPSYCHIATRICDURATION;
        public List<CodedDescriptor> LOOKUPPSYCHIATRICADMISSION2YEAR;
        public List<CodedDescriptor> LOOKUPPSYCHIATRICADMISSIONLIFETIME;
        public List<CodedDescriptor> LOOKUPFIRSTPSYCHIATRICSTAYAGE;
        public List<CodedDescriptor> LOOKUPBEHAVIORSYMPTOMS;
        public List<CodedDescriptor> LOOKUPTREATMENTMODALITIES;
        public List<CodedDescriptor> LOOKUPINTERVENTIONFOCUS;
        public List<CodedDescriptor> LOOKUPELECTROCONVULSIVETHERAPY;
        public List<CodedDescriptor> LOOKUPCONTROLINTERVENTION;
        public List<CodedDescriptor> LOOKUPAUTHORIZEDACTIVITIES;
        public List<CodedDescriptor> LOOKUPDISTURBEDRELATIONSHIP;
        public List<CodedDescriptor> LOOKUPSOCIALLIST;
        public List<CodedDescriptor> LOOKUPEMPLOYMENTSTATUS;
        public List<CodedDescriptor> LOOKUPEMPLOYMENTARRANGEMENT;
        public List<CodedDescriptor> LOOKUPFORMALEDUCATION;
        public List<CodedDescriptor> LOOKUPUNEMPLOYMENTRISK;
        public List<CodedDescriptor> LOOKUPSOCIALSUPPORT;
        public List<CodedDescriptor> LOOKUPEXPECTATIONOFSTAY;
        public List<CodedDescriptor> LOOKUPPSYCHIATRICSYMPTOMS;
        public List<CodedDescriptor> LOOKUPPROVISIONALDSMCATEGORY;
        public List<CodedDescriptor> LOOKUPMEDICALDIAGNOSES;
        public List<CodedDescriptor> LOOKUPDISCHARGEASSESSMENT;
        public List<CodedDescriptor> LOOKUPTYPEOFADMISSION;
        public List<CodedDescriptor> LOOKUPCONTACTPREFERENCES;
        public List<CodedDescriptor> LOOKUPMARITIALSTATUS;
        public List<CodedDescriptor> LOOKUPLIVINGSTATUS;
        public List<CodedDescriptor> LOOKUPLIVINGARRANGEMENT;
        public List<CodedDescriptor> LOOKUPHOSPITALSTAY;
        public List<CodedDescriptor> LOOKUPCOGNITIVESKILLSLIST;
        public List<CodedDescriptor> LOOKUPBEHAVIOURPRESENT;
        public List<CodedDescriptor> LOOKUPASSESSMENTREASON;
        public List<CodedDescriptor> LOOKUPPATIENTSTATUSLIST;
        public List<CodedDescriptor> LOOKUPUNDERSTOODLIST;
        public List<CodedDescriptor> LOOKUPPRIMARYLANGUAGELIST;
        public List<CodedDescriptor> LOOKUPRESIDENCENEEDSTOCHANGELIST;
        public List<CodedDescriptor> LOOKUPVISIONDIFFICULTYLIST;
        public List<CodedDescriptor> LOOKUPHEARINGDIFFICULTYLIST;
        public List<CodedDescriptor> LOOKUPFARTHESTWALKEDDISTANCELIST;
        public List<CodedDescriptor> LOOKUPADLSELFPERFORMANCELIST;
        public List<CodedDescriptor> LOOKUPCHANGEIN90LIST;
        public List<CodedDescriptor> LOOKUPBEHAVIOURLIST;
        public List<CodedDescriptor> LOOKUPINDICATORLIST;

        public List<CodedDescriptor> DECLINELIST;
        public List<CodedDescriptor> LOOKUPALONELIST;
        public List<CodedDescriptor> LOOKUPSELFPERFORMANCELIST;
        public List<CodedDescriptor> LOOKUPLOCOMOTIONWALKINGLIST;
        public List<CodedDescriptor> LOOKUPLOCOMOTIONWALKINGDISTANCELIST;
        public List<CodedDescriptor> LOOKUPFARTHESTWALKINGDISTANCELIST;
        public List<CodedDescriptor> LOOKUPFARTHESTWHEELEDDISTANCELIST;

        public List<CodedDescriptor> LOOKUPACTIVITYTLEVELLIST;
        public List<CodedDescriptor> LOOKUPGOINGOUTLIST;
        public List<CodedDescriptor> LOOKUPCHANGEINADLLIST;
        public List<CodedDescriptor> LOOKUPBLADDERLIST;
        public List<CodedDescriptor> LOOKUPURINARYLIST;
        public List<CodedDescriptor> LOOKUPBOWELLIST;

        public List<CodedDescriptor> LOOKUPDIAGPRESENTLIST;
        public List<CodedDescriptor> LOOKUPFALLSLIST;
        public List<CodedDescriptor> LOOKUPPROBLEMFREQUENCYLIST;
        public List<CodedDescriptor> LOOKUPDYSPNEALIST;
        public List<CodedDescriptor> LOOKUPFATIGUELIST;
        public List<CodedDescriptor> LOOKUPPAIN1LIST;
        public List<CodedDescriptor> LOOKUPPAIN2LIST;

        public List<CodedDescriptor> LOOKUPPAIN3LIST;
        public List<CodedDescriptor> LOOKUPPAIN4LIST;
        public List<CodedDescriptor> LOOKUPSELFREPORTEDHEALTHLIST;
        public List<CodedDescriptor> LOOKUPTOBACCOLIST;
        public List<CodedDescriptor> LOOKUPALCOHOLLIST;
        public List<CodedDescriptor> LOOKUPNUTRITIONLIST;
        public List<CodedDescriptor> LOOKUPPRESSUREULCERLIST;
        public List<CodedDescriptor> LOOKUPFOOTPROBLEMLIST;

        public List<CodedDescriptor> RELASHIPSHIPTOPERSONLIST;
        public List<CodedDescriptor> LIVEWITHPERSONLIST;
        public List<CodedDescriptor> INFORMALHELPLIST;
        public List<CodedDescriptor> TREATMENTLIST;
        public List<CodedDescriptor> SELFSUFFICIENCYLIST;
        public List<CodedDescriptor> PROBLEMRELATEDTODETERIORATIONLIST;
        public List<CodedDescriptor> LOOKUPMEDICATIONADHERENCE;

        public List<CodedDescriptor> LOOKUP_CA_PARENTS_MARITAL_STATUS;
        public List<CodedDescriptor> LOOKUP_CA_ASSESSMENT_REASONS;
        public List<CodedDescriptor> LOOKUP_CA_LIVING_STATUS;
        public List<CodedDescriptor> LOOKUP_CA_LIVING_ARRANGEMENT;
        public List<CodedDescriptor> LOOKUP_CA_LAST_HOSPITAL_STAY;
        public List<CodedDescriptor> LOOKUP_CA_PRIMARY_LANGUAGE;
        public List<CodedDescriptor> LOOKUP_CA_EDUCATION_STATUS;
        public List<CodedDescriptor> LOOKUP_CA_SERVICES_PROVIDED_AT_SCHOOL;
        public List<CodedDescriptor> LOOKUP_CA_PRENATAL_HISTORY;
        public List<CodedDescriptor> LOOKUP_CA_COGNITIVIE_SKILLS;
        public List<CodedDescriptor> LOOKUP_CA_BEHAVIOR_PRESENT;
        public List<CodedDescriptor> LOOKUP_CA_UNDERSTOOD;
        public List<CodedDescriptor> LOOKUP_CA_HEARING_DIFFICULTY;
        public List<CodedDescriptor> LOOKUP_CA_VISION_DIFFICULTY;
        public List<CodedDescriptor> LOOKUP_CA_CHANGE_IN_DECISION;
        public List<CodedDescriptor> LOOKUP_CA_INDICATOR_BEHAVIOR;
        public List<CodedDescriptor> LOOKUP_CA_MOOD;
        public List<CodedDescriptor> LOOKUP_CA_SELF_INJURIOUS;
        public List<CodedDescriptor> LOOKUP_CA_INTENT;
        public List<CodedDescriptor> LOOKUP_CA_SOCIAL;

        public List<CodedDescriptor> LOOKUP_CA_ADAPTABILITY;
        public List<CodedDescriptor> LOOKUP_CA_ADHERENT_WITH_MEDICATIONS;
        public List<CodedDescriptor> LOOKUP_CA_ADL_SELF_PERFORMANCE;
        public List<CodedDescriptor> LOOKUP_CA_ALCOHOL;
        public List<CodedDescriptor> LOOKUP_CA_BEHAVIOR;
        public List<CodedDescriptor> LOOKUP_CA_BLADDER_CONTINENCE;
        public List<CodedDescriptor> LOOKUP_CA_BOWEL_COLLECTION_DEVICE;
        public List<CodedDescriptor> LOOKUP_CA_BOWEL_CONTINENCE;
        public List<CodedDescriptor> LOOKUP_CA_CARE_GOALS;
        public List<CodedDescriptor> LOOKUP_CA_CAREGIVER_HELP;
        public List<CodedDescriptor> LOOKUP_CA_CHANGE_IN_ADL;
        public List<CodedDescriptor> LOOKUP_CA_DAYS_WENT_OUT;
        public List<CodedDescriptor> LOOKUP_CA_DEGREE_COMPLETED;
        public List<CodedDescriptor> LOOKUP_CA_DISEASE_CODE;
        public List<CodedDescriptor> LOOKUP_CA_DYSPNEA;
        public List<CodedDescriptor> LOOKUP_CA_EFFECT;
        public List<CodedDescriptor> LOOKUP_CA_EXPECTED_SERVICES;
        public List<CodedDescriptor> LOOKUP_CA_FATIGUE;
        public List<CodedDescriptor> LOOKUP_CA_FOOT_PROBLEMS;
        public List<CodedDescriptor> LOOKUP_CA_FORMAL_CARE;
        public List<CodedDescriptor> LOOKUP_CA_FORMAL_TREATMENTS;
        public List<CodedDescriptor> LOOKUP_CA_FUTURE_NEEDS;
        public List<CodedDescriptor> LOOKUP_CA_GENDER;
        public List<CodedDescriptor> LOOKUP_CA_HOME_ENVIRONMENT;
        public List<CodedDescriptor> LOOKUP_CA_IADL_SELF_PERFORMANCE;
        public List<CodedDescriptor> LOOKUP_CA_INTELLECTUAL_DISABILITY;
        public List<CodedDescriptor> LOOKUP_CA_LEGAL_GUARDIANSHIP;
        public List<CodedDescriptor> LOOKUP_CA_LIVES_WITH_CHILD_YOUTH;
        public List<CodedDescriptor> LOOKUP_CA_LOCOMOTION;
        public List<CodedDescriptor> LOOKUP_CA_NUTRITION_INTAKE;
        public List<CodedDescriptor> LOOKUP_CA_PAIN1;
        public List<CodedDescriptor> LOOKUP_CA_PAIN2;
        public List<CodedDescriptor> LOOKUP_CA_PAIN3;
        public List<CodedDescriptor> LOOKUP_CA_PAIN4;
        public List<CodedDescriptor> LOOKUP_CA_PHYSICAL_ACTIVITY_HOURS;
        public List<CodedDescriptor> LOOKUP_CA_PRESSURE_ULCER;
        public List<CodedDescriptor> LOOKUP_CA_PROBLEM_FREQUENCY;
        public List<CodedDescriptor> LOOKUP_CA_RELATIONSHIP;
        public List<CodedDescriptor> LOOKUP_CA_SELF_REPORTED_MOOD;
        public List<CodedDescriptor> LOOKUP_CA_SELF_SUFFICIENCY;
        public List<CodedDescriptor> LOOKUP_CA_UNDERSTANDS;
        public List<CodedDescriptor> LOOKUP_CA_URINARY;

        public List<CodedDescriptor> LOOKUP_HCA_ADHERENT_WITH_MEDICATIONS;
        public List<CodedDescriptor> LOOKUP_HCA_ADL_SELF_PERFORMANCE;
        public List<CodedDescriptor> LOOKUP_HCA_ALCOHOL;
        public List<CodedDescriptor> LOOKUP_HCA_ASSESSMENT_REASONS;
        public List<CodedDescriptor> LOOKUP_HCA_BEHAVIOR;
        public List<CodedDescriptor> LOOKUP_HCA_BEHAVIOR_PRESENT;
        public List<CodedDescriptor> LOOKUP_HCA_BLADDER_CONTINENCE;
        public List<CodedDescriptor> LOOKUP_HCA_BOWEL_CONTINENCE;
        public List<CodedDescriptor> LOOKUP_HCA_CHANGE_IN_ADL;
        public List<CodedDescriptor> LOOKUP_HCA_CHANGE_IN_DECISION;
        public List<CodedDescriptor> LOOKUP_HCA_COGNITIVIE_SKILLS;
        public List<CodedDescriptor> LOOKUP_HCA_DAYS_WENT_OUT;
        public List<CodedDescriptor> LOOKUP_HCA_DISEASE_CODE;
        public List<CodedDescriptor> LOOKUP_HCA_DISTANCE_WALKED;
        public List<CodedDescriptor> LOOKUP_HCA_DISTANCE_WHEELED;
        public List<CodedDescriptor> LOOKUP_HCA_DYSPNEA;
        public List<CodedDescriptor> LOOKUP_HCA_FALLS;
        public List<CodedDescriptor> LOOKUP_HCA_FATIGUE;
        public List<CodedDescriptor> LOOKUP_HCA_FOOT_PROBLEMS;
        public List<CodedDescriptor> LOOKUP_HCA_FORMAL_TREATMENTS;
        public List<CodedDescriptor> LOOKUP_HCA_HEARING_DIFFICULTY;
        public List<CodedDescriptor> LOOKUP_HCA_IADL_SELF_PERFORMANCE;
        public List<CodedDescriptor> LOOKUP_HCA_INFORMAL_HELP;
        public List<CodedDescriptor> LOOKUP_HCA_LAST_HOSPITAL_STAY;
        public List<CodedDescriptor> LOOKUP_HCA_LIVES_WITH_PERSON;
        public List<CodedDescriptor> LOOKUP_HCA_LIVING_ARRANGEMENT;
        public List<CodedDescriptor> LOOKUP_HCA_LIVING_BETTER_OFF;
        public List<CodedDescriptor> LOOKUP_HCA_LIVING_STATUS;
        public List<CodedDescriptor> LOOKUP_HCA_LOCOMOTION;
        public List<CodedDescriptor> LOOKUP_HCA_LOCOMOTION_TIMED;
        public List<CodedDescriptor> LOOKUP_HCA_MARITAL_STATUS;
        public List<CodedDescriptor> LOOKUP_HCA_MOOD;
        public List<CodedDescriptor> LOOKUP_HCA_NUTRITION_INTAKE;
        public List<CodedDescriptor> LOOKUP_HCA_PAIN1;
        public List<CodedDescriptor> LOOKUP_HCA_PAIN2;
        public List<CodedDescriptor> LOOKUP_HCA_PAIN3;
        public List<CodedDescriptor> LOOKUP_HCA_PAIN4;
        public List<CodedDescriptor> LOOKUP_HCA_PHYSICAL_ACTIVITY_HOURS;
        public List<CodedDescriptor> LOOKUP_HCA_PRECIPITATING_EVENT;
        public List<CodedDescriptor> LOOKUP_HCA_PRESSURE_ULCER;
        public List<CodedDescriptor> LOOKUP_HCA_PRIMARY_LANGUAGE;
        public List<CodedDescriptor> LOOKUP_HCA_PROBLEM_FREQUENCY;
        public List<CodedDescriptor> LOOKUP_HCA_RELATIONSHIP;
        public List<CodedDescriptor> LOOKUP_HCA_SELF_REPORTED_HEALTH;
        public List<CodedDescriptor> LOOKUP_HCA_SELF_REPORTED_MOOD;
        public List<CodedDescriptor> LOOKUP_HCA_SELF_SUFFICIENCY;
        public List<CodedDescriptor> LOOKUP_HCA_SOCIAL;
        public List<CodedDescriptor> LOOKUP_HCA_SOCIAL_ACTIVITES_CHANGE;
        public List<CodedDescriptor> LOOKUP_HCA_TIME_ALONE;
        public List<CodedDescriptor> LOOKUP_HCA_TOBACCO;
        public List<CodedDescriptor> LOOKUP_HCA_TREATMENTS;
        public List<CodedDescriptor> LOOKUP_HCA_UNDERSTANDS;
        public List<CodedDescriptor> LOOKUP_HCA_UNDERSTOOD;
        public List<CodedDescriptor> LOOKUP_HCA_URINARY;
        public List<CodedDescriptor> LOOKUP_HCA_VISION_DIFFICULTY;
    }

    public class ProviderDesignation
    {
        public DateTime Edate = Convert.ToDateTime(null);
        public long mpcID = -1;
        public string ProviderID = "";
        public string ProviderName = "";
        public DateTime Sdate = Convert.ToDateTime(null);
        public string ProviderNPI = "";
        public string ContractNPI = "";
    }

    public class GMRRegions
    {
        public string DESC = "";
        public string ID = "";
        public double LAT = 0.0;
        public double LON = 0.0;
        public string NAME = "";
        public int ZOOM = 0;
    }

    public class NumericDescriptor
    {
        public string description = "";
        public long number = -1;
    }

    public class ServiceGapDescriptor
    {
        public string CMSID = "";
        public string GAPDESCRIPTION = "";
        public string GAPDOMAIN = "";
        public string MEASURE = "";
        public string TYPE = "";
        public string CPT = "";
        public string HCPCS = "";
        public string UB = "";
        public string DIAG = "";
        public string LOINC = "";

    }

    public class ZipCodeDescriptor
    {
        public string zipcode = "";
        public string zipcodecity = "";
        public string zipcodestate = "";
        public string zipcounty = "";
        public double ziplat = 0.0;
        public double ziplong = 0.0;
    }

    public class CodedDescriptor
    {
        public string code = "";
        public string description = "";
    }

    public class CodedDescriptorWList
    {
        public string code = "";
        public string description = "";
        public List<string> theList = new List<string>();
    }

    public class CodedDescriptorExt
    {
        public string authreq = "";
        public string code = "";
        public string description = "";
    }

    public class CodedDescriptorWithActive
    {
        public string active = "";
        public string code = "";
        public string description = "";
    }

    public class CodedDescriptorWField
    {
        public string code = "";
        public string description = "";
        public string field = "";
    }

    public class CodedDescriptorWPrograms
    {
        public string active = "";
        public string code = "";
        public string description = "";
        public string program = "";
    }

    public class CodedDescriptorWID
    {
        public long id = 0;
        public string code = "";
        public string description = "";
    }

    public class CodedDescriptorWithOther
    {
        public long id = 0;
        public string code = "";
        public string description = "";
        public string other = "";
    }

}
