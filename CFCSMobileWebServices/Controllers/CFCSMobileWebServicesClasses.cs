using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CFCSMobileWebServices.Controllers
{
    #region Extra Classes


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

    public class LoginResult
    {
        public bool Success = false;
        public string UserName = "";
        public string FirstName = "";
        public string LastName = "";
        public string Address1 = "";
        public string Address2 = "";
        public string City = "";
        public string State = "";
        public string ZipCode = "";
        public string logtype = "";
        public string MemberID = "";
    }

    public class UserLogins
    {
        public string ContactNum = "";
        public string CreatedBy = "";
        public DateTime CreatedDate;
        public string Deactive = "";
        public string Email = "";
        public string FirstName = "";
        public string GlobalOrganization = "";
        public List<CodedDescriptor> Groups;
        public List<CodedDescriptor> Hierarchy;
        public string IpAddress = "";
        public bool IsAuthRO = false;
        public bool IsGroupAccessGlobal = false;
        public bool IsMemberDemoRO = false;
        public bool IsOrgAReferralTarget = false;
        public bool IsProgressNotesRO = false;
        public string LastName = "";
        public string Note = "";
        public string Organization = "";
        public string Password = "";
        public DateTime PasswordChangeDate;
        public List<CodedDescriptor> Regions;
        public List<CodedDescriptor> Roles;
        public List<CodedDescriptor> CostCenters;
        public List<CodedDescriptorWID> ServiceIDS;
        public string UpdatedBy = "";
        public DateTime UpdatedDate;
        public int UserID = 0;
        public string Username = "";
        public string Address1 = "";
        public string Address2 = "";
        public string City = "";
        public string State = "";
        public string ZipCode = "";
        public DateTime LastGlobalMesssageDate = Convert.ToDateTime(null);
        public DateTime LastWhatsNewDate = Convert.ToDateTime(null);
        public string County = "";
        public string Email2 = "";
        public string Gender = "";
        public string GenderDescription = "";
        public string Religion = "";
        public string ReligionDescription = "";
        public string Race = "";
        public string RaceDescription = "";
        public List<CodedDescriptor> Languages;
        public string LanguageDescription = "";
        public string ServiceDescription = "";
        public string CountyDescription = "";
        public List<CodedDescriptor> Competencies;
        public string CompetencyDescription = "";

        public string CP1 = "";
        public string CP2 = "";
        public string CP3 = "";
        public string CP4 = "";
        public string CP5 = "";
        public string CP6 = "";
        public string CP7 = "";
        public string CP8 = "";
        public string CP9 = "";
        public string CP10 = "";


        public string CredentialType1 = "";
        public string CredentialType1Description = "";
        public string CredentialType2 = "";
        public string CredentialType2Description = "";
        public string ClinicalLicenseNumber = "";
        public string BCBANumber = "";
        public string IndividualNPINumber = "";
        public bool PhysicalAbuse = false;
        public bool SexualAbuse = false;
        public bool ADHD = false;
        public bool AdoptionFoster = false;
        public bool AngerManagement = false;
        public bool AppliedBehavior = false;
        public bool ArtTherapy = false;
        public bool Autism = false;
        public bool BehaviorModification = false;
        public bool CognitiveBehavior = false;
        public bool DevDisabled = false;
        public bool DomesticViolence = false;
        public bool EatingDisorder = false;
        public bool EMDR = false;
        public bool EvidenceBased = false;
        public bool FaithChrisitian = false;
        public bool FaithJewish = false;
        public bool FaithOther = false;
        public bool FamilyTherapy = false;
        public bool GangInvolvement = false;
        public bool JuvenileJustice = false;
        public bool LearningDisability = false;
        public bool LGBTIssues = false;
        public bool ParentingSkills = false;
        public bool PlayTherapy = false;
        public bool PTSD = false;
        public bool SelfMutilatition = false;
        public bool SexOffenders = false;
        public bool SexualBoundaryIssues = false;
        public bool SocialSkillsTraining = false;
        public bool SubstanceAbuse = false;
        public bool TraumaIssues = false;
        public bool VocationalSkillsTraining = false;
        public bool AcceptTexts = false;
        public double LATITUDE = 0.0;
        public double LONGITUDE = 0.0;
        public string MAPGROUP = "";
        public string TOOLTIP = "";
        public string GEO = "";
        public string OON = "";
        public string MEMBERID = ""; // for member/consumer logins


        //public IHtmlString ToJson = JsonHelpers.ToJson<UserLogins>((object) this);
    }

    public class MemberDetailsShort
    {
        public string CaseManager = "";
        public string CreatedBy = "";
        public DateTime CreatedDate = Convert.ToDateTime(null);
        public string CurrentCaseManager = "";
        public string DOB;
        public string Email = "";
        public string Ethnicity = "";
        public string FirstName = "";
        public string Gender = "";
        public string LastActiveStatus = "";
        public string LastName = "";
        public MemberAddress memberAddress;
        public List<MemberAddress> memberAddressHistory;
        //public List<ihsisMemberAdmissionEntry> MemberAdmissions;
        //public List<ihsisMemberAlert> MemberAlerts;
        //public List<AuthorizedService> memberAuths;
        //public List<objtblMemberCommunitySupports> MemberCommunitySupports;
        //public List<ihsisMEMBERCONTACTINFO> MemberContactList;
        //public List<IhsisMemberContactEntry> MemberContacts;
        //public List<ihsisMemberDMEProvider> MemberDMEProviders;
        //public List<ihsisMemberEligibilityEntry> MemberEligibilityList;
        //public List<MemberOtherSystemIDs> MemberOtherSystemIDs;
        //public List<ihsisGroupInformation> MemberGroups;
        //public List<ihsisImmunizationScreen> MemberImmunizations;
        //public List<ihsisMemberLabs> MemberLabs;
        //public List<IhsisMemberLanguageEntry> MemberLanguages;
        //public List<ihsisMemberMeds> MemberMeds;
        //public List<ihsisOtherSystemIDs> MemberOtherSystems;
        //public List<ihsisMemberProgramMembershipEntry> MemberPrograms;
        //public List<ProviderDesignation> memberProviders;
        //public List<ihsisROI> MemberROIs;
        //public List<ihsisBPRSGAFBundle> MemberScores;
        //public List<ihsisMemberProvider> MemberServiceProviders;
        //public List<MemberServices> MemberServices;
        //public List<MemberRelationship> MemberSupports;
        //public List<MemberTherapies> MemberTherapies;
        //public List<ihsisMEMBERWATCHLISTENTRY> MemberWatchList;
        public string MiddleName = "";
        public Int64 MMID = 0;
        public string ParentGuardian = "";
        public string ParentGuardPhone = "";
        public string Phone1 = "";
        public string Phone1Ext = "";
        public string Phone1Type = "";
        public string Phone2 = "";
        public string Phone2Ext = "";
        public string Phone2Type = "";
        public string Race = "";
        public string SSN = "";
        public string UpdatedBy = "";
        public DateTime UpdatedDate = Convert.ToDateTime(null);
        public DateTime LocationDate = Convert.ToDateTime(null);
        public string MEMBERSTATUS = "";
        public long MEMBERADDRESSHISTORYID = 0;
        public long MEMBERADDRESSID = 0;

    }

    public class MemberAddress
    {
        public string Address1 = "";
        public string Address2 = "";
        public string Address3 = "";
        public string AddressType = "";
        public string ApartmentSuite = "";
        public string City = "";
        public string County = "";
        public string CreateBy = "";
        public DateTime CreateDate;
        public Int64 MAID = 0;
        public string SSN = "";
        public string State = "";
        public DateTime UpdateDate;
        public string UpdatedBy = "";
        public string ZipCode = "";
    }

    public class SvcForAuth
    {
        public string SVCID = "";
        public string COSTCENTER = "";
        public string SVCCODE = "";
        public string SVCDESCRIPTION = "";
        public string UNITTYPE = "";
        public string UBNITTYPEDESCRIPTION = "";
        public string BCBANOTEREQ = "";

        // Needed on the UI side
        //public override string ToString()
        //{
        //    return SVCDESCRIPTION;
        //}

    }

    public class AuthorizedService
    {
        public string ACCEPTED = "";
        public string COSTCENTER = "";
        public string COSTCENTERDESC = "";
        //public DateTime CREATEDATE = Convert.ToDateTime(null);
        public string CREATEUSER = "";

        public double DOLLARS = 0.0;
        public DateTime ENDDATE = Convert.ToDateTime(null);
        public string FUNDER = "";
        public string FUNDERID = "";
        public long mauthID = 0;
        public string PROVIDERID = "";
        public string PROVIDERNAME = "";
        public int REMAININGUNITS = 0;
        public string SSN = "";
        public DateTime STARTDATE = Convert.ToDateTime(null);
        public int UNITS = 0;
        public string AUTHNUMBER = "";
        public string CONNECTEDNPI = "";
        public int HPW = 0;
        public string CASEMANAGER = "";
        public string LINKEDAUTHNUMBER = "";
        public int UNITTYPE = 0;
        public string RelatedSplitCode = "";
        public string ServiceCode = "";
        public int ROUNDLIMIT = 0;
        public bool ISLINKED = false;
        public string OLDAUTHNUMBER = "";
        public string OLDSERVICE = "";
        public bool ISSPLIT = false;
        public string VALID = "";
        public string strSTARTDATE = "";
        public string strENDDATE = "";
        public double DSPRATE = 0.0;
    }

    public class MemberObservers
    {
        public string AUTHOR = "";
        public DateTime CREATEDATE = Convert.ToDateTime(null);
        public DateTime EDATE = Convert.ToDateTime(null);
        public long ID = -1;
        public string OBSERVER = "";
        public string OBSTYPE = "";
        public DateTime SDATE = Convert.ToDateTime(null);
        public string SSN = "";
        public string PHONE = "";
        public string EMAIL = "";
        public string CREDENTIALONE = "";
        public string CREDENTIALTWO = "";
        public string USERNAME = "";
        public string strSDATE = "";
        public string strEDATE = "";
    }

    public class MemberReferralSource
    {
        public long RSID { get; set; }
        public string RSFIRSTNAME { get; set; }
        public string RSLASTNAME { get; set; }
        public string RSROLE { get; set; }
        public string RSROLEDESCRIPTION { get; set; }
        public bool RSISAGENCY { get; set; }
        public long RSAGENCYID { get; set; }
        public string RSADDRESS1 { get; set; }
        public string RSADDRESS2 { get; set; }
        public string RSADDRESS3 { get; set; }
        public string RSCITY { get; set; }
        public string RSSTATE { get; set; }
        public string RSZIP { get; set; }
        public string RSCOUNTY { get; set; }
        public string RSEMAIL { get; set; }
        public string RSHOMEPHONE { get; set; }
        public string RSWORKPHONE { get; set; }
        public bool RSACTIVE { get; set; }
        public string RSCREATEDBY { get; set; }
        public DateTime RSCREATEDDATE { get; set; }
        public string SSN { get; set; }
        public long RSCASEMANAGER { get; set; }
        public string AGENCYDESC { get; set; }
        public DateTime RSSTARTDATE { get; set; }
        public DateTime RSENDDATE { get; set; }
        public string RSFULLNAME { get; set; }
        public string ROLEFULLNAME { get; set; }
        public string RSUSERNAME { get; set; }
        public string ISEDITPERSON { get; set; }
        public string strRSSTARTDATE { get; set; }
        public string strRSENDDATE { get; set; }

        public string RSEXTENSION { get; set; }

        public MemberReferralSource()
        {
            RSID = 0;
            RSCASEMANAGER = 0;
            RSFIRSTNAME = "";
            RSLASTNAME = "";
            RSROLE = "";
            RSROLEDESCRIPTION = "";
            RSISAGENCY = false;
            RSAGENCYID = 0;
            RSADDRESS1 = "";
            RSADDRESS2 = "";
            RSADDRESS3 = "";
            RSCITY = "";
            RSSTATE = "";
            RSZIP = "";
            RSCOUNTY = "";
            RSEMAIL = "";
            RSHOMEPHONE = "";
            RSWORKPHONE = "";
            RSACTIVE = false;
            RSCREATEDBY = "";
            RSCREATEDDATE = Convert.ToDateTime(null);
            SSN = "";
            AGENCYDESC = "";
            RSSTARTDATE = Convert.ToDateTime(null);
            RSENDDATE = Convert.ToDateTime(null);
            RSFULLNAME = "";
            ROLEFULLNAME = "";
            RSUSERNAME = "";
            ISEDITPERSON = "";
            strRSSTARTDATE = "";
            strRSENDDATE = "";
            RSEXTENSION = "";
        }

    }

    public class MemberProgressNotes
    {
        public string AUTHOR = "";
        public DateTime CONTACTDATE = DateTime.MinValue;
        public string strCONTACTDATE = "";
        public int CONTACTMINUTES = 0;
        public DateTime CREATEDATE = DateTime.MinValue;
        public string MEMBERNAME = "";
        public long mpnID = -1;
        public string NOTATION = "";
        public string NOTATIONSHORT = "";
        public string NOTECONTACTDESC = "";
        public string NOTETYPEDESC = "";
        public DateTime SAFETYASSESSMENT = DateTime.MinValue;
        public string SAFETYASSESSMENTLVL = "";
        public string SAFETYASSESSMENTLVLDESC = "";
        public string SIGNED = "";
        public DateTime SIGNEDDATE = DateTime.MinValue;
        public string SSN = "";
        public string SUPERAPPROV = "";
        public DateTime SUPERAPPROVEDATE = DateTime.MinValue;
        public string SUPERVISORACK1 = "";
        public string SUPERVISORACK2 = "";
        public string SUPERVISORNOTATION = "";
        public DateTime SUPERVISORNOTATIONDATE = DateTime.MinValue;
        public int TRAVELMINUTES = 0;
        public string SERVICECODE = "";
        public string SERVICEDESCRIPTION = "";
        public string NEWSSN = "";
        public int ENCOUNTERID = 0;
        public string CONTACTTYPEDESCRIPTION = "";
        public string strCREATEDATE = "";
    }

    public class ServiceDescription
    {
        public string ACTIVE = "";
        public string AUTHREQ = "";
        public string COSTCENTER = "";
        public double COSTPERUNIT = 0.0;
        public string FUNDER = "";
        public string FUNDERDESCRIPTION = "";
        public long ID = -1;
        public string SVCCODE = "";
        public string SVCDESCRIPTION = "";
        public string UNITTYPE = "";
        public string UNITTYPEDESCRIPTION = "";
        public int AUTOUNIT = -1;
        public int ROUNDRULE = -1;
        public string MOD1 = "";
        public string MOD2 = "";
        public string MOD3 = "";
        public string MOD4 = "";
        public string COPAY = "";
        public string RELATEDSPLITCODE = "";
        public bool BCBANOTE = false;
    }

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

    public class LookupServices
    {
        public long svcID = 0;
        public string Funder = "";
        public string CostCenter = "";
        public string SvcCode = "";
        public string SvcDescription = "";
        public string UnitType = "";
        public double CostPerUnit = 0.0;
        public string ACTIVE = "";
        public string AUTHREQ = "";
        public string RelatedSplitCode = "";
        public bool BCBANoteRequired = false;
        public bool BANote = false;
    }
    
    #endregion

}

