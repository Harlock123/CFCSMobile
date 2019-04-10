using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

namespace CFCSMobileWebServices.Controllers
{
    #region Extra Classes

    public class MemberEncounter
    {
        public long tblMemberEncountersID { get; set; }
        public string SSN { get; set; }
        public DateTime EncounterDate { get; set; }
        public DateTime EncounterStartTime { get; set; }
        public DateTime EncounterEndTime { get; set; }
        public string TypeOfServiceDeliverySite { get; set; }
        public string ServiceDelivered { get; set; }
        public bool IsGroupService { get; set; }
        public bool IsIndividualService { get; set; }
        public string DeliverySiteAddress1 { get; set; }
        public string DeliverySiteAddress2 { get; set; }
        public string DeliverySiteAddress3 { get; set; }
        public string DeliverySiteCity { get; set; }
        public string DeliverySiteState { get; set; }
        public string DeliverySiteZipCode { get; set; }
        public string DeliverySiteCounty { get; set; }
        public string DeliverySitePhone { get; set; }
        public bool IsGuardian { get; set; }
        public bool IsResponsibleParty { get; set; }
        public string GuardianResponsiblePerson { get; set; }
        public string GuardianPersonAddress1 { get; set; }
        public string GuardianPersonAddress2 { get; set; }
        public string GuardianPersonAddress3 { get; set; }
        public string GuardianPersonCity { get; set; }
        public string GuardianPersonState { get; set; }
        public string GuardianPersonZipCode { get; set; }
        public string GuardianPersonCounty { get; set; }
        public string GuardianPersonRelationship { get; set; }
        public DateTime DateEncounterSigned { get; set; }
        public string EncounterStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime BilledDate { get; set; }
        public DateTime PaidDate { get; set; }
        public string CheckNumber { get; set; }
        public string AuthNumber { get; set; }
        public int ProgressNoteID { get; set; }
        public string NeedsFixingComment { get; set; }
        public string Notation { get; set; }
        public long BANoteID { get; set; }
        public string ACTID { get; set; }
        public double ChargedAmount { get; set; }
        public double PaidAmount { get; set; }
        public DateTime ConsumerBilledDate { get; set; }
        public DateTime ConsumerPaidDate { get; set; }
        public string ConsumerCheckNumber { get; set; }
        public double ConsumerChargedAmount { get; set; }
        public double ConsumerPaidAmount { get; set; }
        public bool SUBMITTED { get; set; }
        public DateTime SUBMITTEDDATE { get; set; }
        public long ROLLUPID { get; set; }
        public int BCBAID { get; set; }


        public MemberEncounter()
        {
            tblMemberEncountersID = 0;
            SSN = "";
            EncounterDate = Convert.ToDateTime(null);
            EncounterStartTime = Convert.ToDateTime(null);
            EncounterEndTime = Convert.ToDateTime(null);
            TypeOfServiceDeliverySite = "";
            ServiceDelivered = "";
            IsGroupService = false;
            IsIndividualService = false;
            DeliverySiteAddress1 = "";
            DeliverySiteAddress2 = "";
            DeliverySiteAddress3 = "";
            DeliverySiteCity = "";
            DeliverySiteState = "";
            DeliverySiteZipCode = "";
            DeliverySiteCounty = "";
            DeliverySitePhone = "";
            IsGuardian = false;
            IsResponsibleParty = false;
            GuardianResponsiblePerson = "";
            GuardianPersonAddress1 = "";
            GuardianPersonAddress2 = "";
            GuardianPersonAddress3 = "";
            GuardianPersonCity = "";
            GuardianPersonState = "";
            GuardianPersonZipCode = "";
            GuardianPersonCounty = "";
            GuardianPersonRelationship = "";
            DateEncounterSigned = Convert.ToDateTime(null);
            EncounterStatus = "";
            CreatedDate = Convert.ToDateTime(null);
            CreatedBy = "";
            BilledDate = Convert.ToDateTime(null);
            PaidDate = Convert.ToDateTime(null);
            CheckNumber = "";
            AuthNumber = "";
            ProgressNoteID = 0;
            NeedsFixingComment = "";
            Notation = "";
            BANoteID = 0;
            ACTID = "";
            ChargedAmount = 0.0;
            PaidAmount = 0.0;
            ConsumerBilledDate = Convert.ToDateTime(null);
            ConsumerPaidDate = Convert.ToDateTime(null);
            ConsumerCheckNumber = "";
            ConsumerChargedAmount = 0.0;
            ConsumerPaidAmount = 0.0;
            SUBMITTED = false;
            SUBMITTEDDATE = Convert.ToDateTime(null);
            ROLLUPID = 0;
            BCBAID = 0;
        }

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

    public class MobileSubmittedEncounter
    {
        public string WHO = "";
        public string FORWHO = "";
        public string AUTH = "";
        public string SVC = "";
        public DateTime WHEN;
        public int MINUTES = 0;
        public string NARRATIVE = "";
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

    public class UserMessage
    {
        public string BODY { get; set; }
        public string BODYSHORT { get; set; }
        public DateTime DATECREATED { get; set; }
        public string DESTINATION { get; set; }
        public string MESSAGETYPE { get; set; }
        public long msgID { get; set; }
        public string READSTATUS { get; set; }
        public string SOURCE { get; set; }
        public List<Client> SELECTEDCLIENTS { get; set; }
    }

    public class Client
    {
        public int ID { get; set; }
        public string UserName { get; set; }
    }

    public partial class tblMemberAuthorizedServices : INotifyPropertyChanged
    {

        #region Declarations
        string _classDatabaseConnectionString = "";
        string _bulkinsertPath = "";

        SqlConnection _cn = new SqlConnection();
        SqlCommand _cmd = new SqlCommand();

        // Backing Variables for Properties
        long _mauthID = 0;
        string _SSN = "";
        string _PROVIDERID = "";
        DateTime _STARTDATE = Convert.ToDateTime(null);
        DateTime _ENDDATE = Convert.ToDateTime(null);
        string _COSTCENTER = "";
        int _UNITS = 0;
        double _DOLLARS = 0.0;
        DateTime _CREATEDATE = Convert.ToDateTime(null);
        string _CREATEUSER = "";
        string _ACCEPTED = "";
        int _REMAININGUNITS = 0;
        string _FUNDER = "";
        string _PROGRAM = "";
        string _AUTHNUMBER = "";
        string _CONNECTEDNPI = "";
        int _HPW = 0;
        string _CASEMANAGER = "";

        #endregion

        #region Properties

        public string classDatabaseConnectionString
        {
            get { return _classDatabaseConnectionString; }
            set { _classDatabaseConnectionString = value; }
        }

        public string bulkinsertPath
        {
            get { return _bulkinsertPath; }
            set { _bulkinsertPath = value; }
        }

        public long mauthID
        {
            get { return _mauthID; }
            set
            {
                _mauthID = value;
                RaisePropertyChanged("mauthID");
            }
        }

        public string SSN
        {
            get { return _SSN; }
            set
            {
                if (value != null && value.Length > 20)
                { _SSN = value.Substring(0, 20); }
                else
                {
                    _SSN = value;
                    RaisePropertyChanged("SSN");
                }
            }
        }

        public string PROVIDERID
        {
            get { return _PROVIDERID; }
            set
            {
                if (value != null && value.Length > 20)
                { _PROVIDERID = value.Substring(0, 20); }
                else
                {
                    _PROVIDERID = value;
                    RaisePropertyChanged("PROVIDERID");
                }
            }
        }

        public DateTime STARTDATE
        {
            get { return _STARTDATE; }
            set
            {
                _STARTDATE = value;
                RaisePropertyChanged("STARTDATE");
            }
        }

        public DateTime ENDDATE
        {
            get { return _ENDDATE; }
            set
            {
                _ENDDATE = value;
                RaisePropertyChanged("ENDDATE");
            }
        }

        public string COSTCENTER
        {
            get { return _COSTCENTER; }
            set
            {
                if (value != null && value.Length > 5)
                { _COSTCENTER = value.Substring(0, 5); }
                else
                {
                    _COSTCENTER = value;
                    RaisePropertyChanged("COSTCENTER");
                }
            }
        }

        public int UNITS
        {
            get { return _UNITS; }
            set
            {
                _UNITS = value;
                RaisePropertyChanged("UNITS");
            }
        }

        public double DOLLARS
        {
            get { return _DOLLARS; }
            set
            {
                _DOLLARS = value;
                RaisePropertyChanged("DOLLARS");
            }
        }

        public DateTime CREATEDATE
        {
            get { return _CREATEDATE; }
            set
            {
                _CREATEDATE = value;
                RaisePropertyChanged("CREATEDATE");
            }
        }

        public string CREATEUSER
        {
            get { return _CREATEUSER; }
            set
            {
                if (value != null && value.Length > 20)
                { _CREATEUSER = value.Substring(0, 20); }
                else
                {
                    _CREATEUSER = value;
                    RaisePropertyChanged("CREATEUSER");
                }
            }
        }

        public string ACCEPTED
        {
            get { return _ACCEPTED; }
            set
            {
                if (value != null && value.Length > 1)
                { _ACCEPTED = value.Substring(0, 1); }
                else
                {
                    _ACCEPTED = value;
                    RaisePropertyChanged("ACCEPTED");
                }
            }
        }

        public int REMAININGUNITS
        {
            get { return _REMAININGUNITS; }
            set
            {
                _REMAININGUNITS = value;
                RaisePropertyChanged("REMAININGUNITS");
            }
        }

        public string FUNDER
        {
            get { return _FUNDER; }
            set
            {
                if (value != null && value.Length > 5)
                { _FUNDER = value.Substring(0, 5); }
                else
                {
                    _FUNDER = value;
                    RaisePropertyChanged("FUNDER");
                }
            }
        }

        public string PROGRAM
        {
            get { return _PROGRAM; }
            set
            {
                if (value != null && value.Length > 5)
                { _PROGRAM = value.Substring(0, 5); }
                else
                {
                    _PROGRAM = value;
                    RaisePropertyChanged("PROGRAM");
                }
            }
        }

        public string AUTHNUMBER
        {
            get { return _AUTHNUMBER; }
            set
            {
                if (value != null && value.Length > 50)
                { _AUTHNUMBER = value.Substring(0, 50); }
                else
                {
                    _AUTHNUMBER = value;
                    RaisePropertyChanged("AUTHNUMBER");
                }
            }
        }

        public string CONNECTEDNPI
        {
            get { return _CONNECTEDNPI; }
            set
            {
                if (value != null && value.Length > 50)
                { _CONNECTEDNPI = value.Substring(0, 50); }
                else
                {
                    _CONNECTEDNPI = value;
                    RaisePropertyChanged("CONNECTEDNPI");
                }
            }
        }

        public int HPW
        {
            get { return _HPW; }
            set
            {
                _HPW = value;
                RaisePropertyChanged("HPW");
            }
        }

        public string CASEMANAGER
        {
            get { return _CASEMANAGER; }
            set
            {
                if (value != null && value.Length > 20)
                { _CASEMANAGER = value.Substring(0, 20); }
                else
                {
                    _CASEMANAGER = value;
                    RaisePropertyChanged("CASEMANAGER");
                }
            }
        }


        #endregion

        #region Implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Constructor

        public tblMemberAuthorizedServices()
        {
            // Constructor code goes here.
            Initialize();
        }

        public tblMemberAuthorizedServices(string DSN)
        {
            // Constructor code goes here.
            Initialize();
            classDatabaseConnectionString = DSN;
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            _mauthID = 0;
            _SSN = "";
            _PROVIDERID = "";
            _STARTDATE = Convert.ToDateTime(null);
            _ENDDATE = Convert.ToDateTime(null);
            _COSTCENTER = "";
            _UNITS = 0;
            _DOLLARS = 0.0;
            _CREATEDATE = Convert.ToDateTime(null);
            _CREATEUSER = "";
            _ACCEPTED = "";
            _REMAININGUNITS = 0;
            _FUNDER = "";
            _PROGRAM = "";
            _AUTHNUMBER = "";
            _CONNECTEDNPI = "";
            _HPW = 0;
            _CASEMANAGER = "";
        }

        public void CopyFields(SqlDataReader r)
        {
            try
            {
                if (!Convert.IsDBNull(r["mauthID"]))
                {
                    _mauthID = Convert.ToInt64(r["mauthID"]);
                }
                if (!Convert.IsDBNull(r["SSN"]))
                {
                    _SSN = r["SSN"] + "";
                }
                if (!Convert.IsDBNull(r["PROVIDERID"]))
                {
                    _PROVIDERID = r["PROVIDERID"] + "";
                }
                if (!Convert.IsDBNull(r["STARTDATE"]))
                {
                    _STARTDATE = Convert.ToDateTime(r["STARTDATE"]);
                }
                if (!Convert.IsDBNull(r["ENDDATE"]))
                {
                    _ENDDATE = Convert.ToDateTime(r["ENDDATE"]);
                }
                if (!Convert.IsDBNull(r["COSTCENTER"]))
                {
                    _COSTCENTER = r["COSTCENTER"] + "";
                }
                if (!Convert.IsDBNull(r["UNITS"]))
                {
                    _UNITS = Convert.ToInt32(r["UNITS"]);
                }
                if (!Convert.IsDBNull(r["DOLLARS"]))
                {
                    _DOLLARS = Convert.ToDouble(r["DOLLARS"]);
                }
                if (!Convert.IsDBNull(r["CREATEDATE"]))
                {
                    _CREATEDATE = Convert.ToDateTime(r["CREATEDATE"]);
                }
                if (!Convert.IsDBNull(r["CREATEUSER"]))
                {
                    _CREATEUSER = r["CREATEUSER"] + "";
                }
                if (!Convert.IsDBNull(r["ACCEPTED"]))
                {
                    _ACCEPTED = r["ACCEPTED"] + "";
                }
                if (!Convert.IsDBNull(r["REMAININGUNITS"]))
                {
                    _REMAININGUNITS = Convert.ToInt32(r["REMAININGUNITS"]);
                }
                if (!Convert.IsDBNull(r["FUNDER"]))
                {
                    _FUNDER = r["FUNDER"] + "";
                }
                if (!Convert.IsDBNull(r["PROGRAM"]))
                {
                    _PROGRAM = r["PROGRAM"] + "";
                }
                if (!Convert.IsDBNull(r["AUTHNUMBER"]))
                {
                    _AUTHNUMBER = r["AUTHNUMBER"] + "";
                }
                if (!Convert.IsDBNull(r["CONNECTEDNPI"]))
                {
                    _CONNECTEDNPI = r["CONNECTEDNPI"] + "";
                }
                if (!Convert.IsDBNull(r["HPW"]))
                {
                    _HPW = Convert.ToInt32(r["HPW"]);
                }
                if (!Convert.IsDBNull(r["CASEMANAGER"]))
                {
                    _CASEMANAGER = r["CASEMANAGER"] + "";
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberAuthorizedServices.CopyFields " + ex.ToString()));
            }
        }

        public void Add()
        {
            try
            {
                string sql = GetParameterSQL();
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (this._SSN == null || this._SSN == "" || this._SSN == string.Empty)
                {
                    cmd.Parameters.Add("@SSN", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SSN", System.Data.SqlDbType.VarChar).Value = this._SSN;
                }
                if (this._PROVIDERID == null || this._PROVIDERID == "" || this._PROVIDERID == string.Empty)
                {
                    cmd.Parameters.Add("@PROVIDERID", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@PROVIDERID", System.Data.SqlDbType.VarChar).Value = this._PROVIDERID;
                }
                cmd.Parameters.Add("@STARTDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._STARTDATE);
                cmd.Parameters.Add("@ENDDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._ENDDATE);
                if (this._COSTCENTER == null || this._COSTCENTER == "" || this._COSTCENTER == string.Empty)
                {
                    cmd.Parameters.Add("@COSTCENTER", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@COSTCENTER", System.Data.SqlDbType.VarChar).Value = this._COSTCENTER;
                }
                cmd.Parameters.Add("@UNITS", System.Data.SqlDbType.Int).Value = this._UNITS;
                cmd.Parameters.Add("@DOLLARS", System.Data.SqlDbType.Money).Value = this._DOLLARS;
                cmd.Parameters.Add("@CREATEDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._CREATEDATE);
                if (this._CREATEUSER == null || this._CREATEUSER == "" || this._CREATEUSER == string.Empty)
                {
                    cmd.Parameters.Add("@CREATEUSER", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@CREATEUSER", System.Data.SqlDbType.VarChar).Value = this._CREATEUSER;
                }
                if (this._ACCEPTED == null || this._ACCEPTED == "" || this._ACCEPTED == string.Empty)
                {
                    cmd.Parameters.Add("@ACCEPTED", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@ACCEPTED", System.Data.SqlDbType.VarChar).Value = this._ACCEPTED;
                }
                cmd.Parameters.Add("@REMAININGUNITS", System.Data.SqlDbType.Int).Value = this._REMAININGUNITS;
                if (this._FUNDER == null || this._FUNDER == "" || this._FUNDER == string.Empty)
                {
                    cmd.Parameters.Add("@FUNDER", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@FUNDER", System.Data.SqlDbType.VarChar).Value = this._FUNDER;
                }
                if (this._PROGRAM == null || this._PROGRAM == "" || this._PROGRAM == string.Empty)
                {
                    cmd.Parameters.Add("@PROGRAM", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@PROGRAM", System.Data.SqlDbType.VarChar).Value = this._PROGRAM;
                }
                if (this._AUTHNUMBER == null || this._AUTHNUMBER == "" || this._AUTHNUMBER == string.Empty)
                {
                    cmd.Parameters.Add("@AUTHNUMBER", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@AUTHNUMBER", System.Data.SqlDbType.VarChar).Value = this._AUTHNUMBER;
                }
                if (this._CONNECTEDNPI == null || this._CONNECTEDNPI == "" || this._CONNECTEDNPI == string.Empty)
                {
                    cmd.Parameters.Add("@CONNECTEDNPI", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@CONNECTEDNPI", System.Data.SqlDbType.VarChar).Value = this._CONNECTEDNPI;
                }
                cmd.Parameters.Add("@HPW", System.Data.SqlDbType.Int).Value = this._HPW;
                if (this._CASEMANAGER == null || this._CASEMANAGER == "" || this._CASEMANAGER == string.Empty)
                {
                    cmd.Parameters.Add("@CASEMANAGER", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@CASEMANAGER", System.Data.SqlDbType.VarChar).Value = this._CASEMANAGER;
                }
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                if (mauthID < 1)
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT @@IDENTITY", cn);
                    System.Int64 ii = Convert.ToInt64(cmd2.ExecuteScalar());
                    cmd2.Cancel();
                    cmd2.Dispose();
                    _mauthID = ii;
                }
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberAuthorizedServices.Add " + ex.ToString()));
            }
        }

        public void Update()
        {
            try
            {
                Add();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberAuthorizedServices.Update " + ex.ToString()));
            }
        }

        public void Delete()
        {
            try
            {
                string sql = "Delete from tblMemberAuthorizedServices WHERE mauthID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = this._mauthID;
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberAuthorizedServices.Delete " + ex.ToString()));
            }
        }

        public void Read(System.Int64 idx)
        {
            try
            {
                string sql = "Select * from tblMemberAuthorizedServices WHERE mauthID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    this.CopyFields(r);
                }
                r.Close();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberAuthorizedServices.Read " + ex.ToString()));
            }
        }

        public DataSet ReadAsDataSet(System.Int64 idx)
        {
            try
            {
                string sql = "Select * from tblMemberAuthorizedServices WHERE mauthID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "tblMemberAuthorizedServices");
                da.Dispose();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberAuthorizedServices.ReadAsDataSet " + ex.ToString()));
            }
        }

        public void Read(string AuthNumber)
        {
            try
            {
                string sql = "Select * from tblMemberAuthorizedServices WHERE AUTHNUMBER = @AuthNumber";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@AuthNumber", System.Data.SqlDbType.VarChar).Value = AuthNumber;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    this.CopyFields(r);
                }
                r.Close();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberAuthorizedServices.Read " + ex.ToString()));
            }
        }

        public DataSet ReadAsDataSet(string AuthNumber)
        {
            try
            {
                string sql = "Select * from tblMemberAuthorizedServices WHERE AuthNumber = @AuthNumber";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@AuthNumber", System.Data.SqlDbType.VarChar).Value = AuthNumber;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "tblMemberAuthorizedServices");
                da.Dispose();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberAuthorizedServices.ReadAsDataSet " + ex.ToString()));
            }
        }
        #endregion

        #region Private Methods

        private string GetParameterSQL()
        {
            string sql = "";
            if (_mauthID < 1)
            {
                sql = "INSERT INTO tblMemberAuthorizedServices";
                sql += "(";
                sql += "[SSN], [PROVIDERID], [STARTDATE], [ENDDATE], [COSTCENTER], [UNITS], [DOLLARS],";
                sql += "[CREATEDATE], [CREATEUSER], [ACCEPTED], [REMAININGUNITS], [FUNDER], [PROGRAM],";
                sql += "[AUTHNUMBER], [CONNECTEDNPI], [HPW], [CASEMANAGER])";
                sql += "VALUES (";
                sql += "@SSN,@PROVIDERID,@STARTDATE,@ENDDATE,@COSTCENTER,@UNITS,@DOLLARS,@CREATEDATE,";
                sql += "@CREATEUSER,@ACCEPTED,@REMAININGUNITS,@FUNDER,@PROGRAM,@AUTHNUMBER,@CONNECTEDNPI,";
                sql += "@HPW,@CASEMANAGER)";
            }
            else
            {
                sql = "UPDATE tblMemberAuthorizedServices SET ";
                sql += "[SSN] = @SSN, [PROVIDERID] = @PROVIDERID, [STARTDATE] = @STARTDATE, [ENDDATE] = @ENDDATE,";
                sql += "[COSTCENTER] = @COSTCENTER, [UNITS] = @UNITS, [DOLLARS] = @DOLLARS, [CREATEDATE] = @CREATEDATE,";
                sql += "[CREATEUSER] = @CREATEUSER, [ACCEPTED] = @ACCEPTED, [REMAININGUNITS] = @REMAININGUNITS,";
                sql += "[FUNDER] = @FUNDER, [PROGRAM] = @PROGRAM, [AUTHNUMBER] = @AUTHNUMBER, [CONNECTEDNPI] = @CONNECTEDNPI,";
                sql += "[HPW] = @HPW, [CASEMANAGER] = @CASEMANAGER";
                sql += " WHERE mauthID = " + _mauthID.ToString();
            }
            return sql;
        }

        private object getDateOrNull(DateTime d)
        {
            if (d == Convert.ToDateTime(null))
            {
                return DBNull.Value;
            }
            else
            {
                return d;
            }
        }
        #endregion
    }

    public partial class tblLOOKUPSERVICES : INotifyPropertyChanged
    {

        #region Declarations
        string _classDatabaseConnectionString = "";
        string _bulkinsertPath = "";

        SqlConnection _cn = new SqlConnection();
        SqlCommand _cmd = new SqlCommand();

        // Backing Variables for Properties
        long _SvcID = 0;
        string _Funder = "";
        string _CostCenter = "";
        string _SvcCode = "";
        string _SvcDescription = "";
        string _UnitType = "";
        double _CostPerUnit = 0.0;
        string _ACTIVE = "";
        string _AUTHREQ = "";
        string _COPAY = "";
        string _Modifier1 = "";
        string _Modifier2 = "";
        string _Modifier3 = "";
        string _Modifier4 = "";
        int _AUTOUNIT = 0;
        int _ROUNDRULE = 0;
        string _RelatedSplitCode = "";
        bool _BCBANOTEREQUIRED = false;

        #endregion

        #region Properties

        public string classDatabaseConnectionString
        {
            get { return _classDatabaseConnectionString; }
            set { _classDatabaseConnectionString = value; }
        }

        public string bulkinsertPath
        {
            get { return _bulkinsertPath; }
            set { _bulkinsertPath = value; }
        }

        public long SvcID
        {
            get { return _SvcID; }
            set
            {
                _SvcID = value;
                RaisePropertyChanged("SvcID");
            }
        }

        public string Funder
        {
            get { return _Funder; }
            set
            {
                if (value != null && value.Length > 10)
                { _Funder = value.Substring(0, 10); }
                else
                {
                    _Funder = value;
                    RaisePropertyChanged("Funder");
                }
            }
        }

        public string CostCenter
        {
            get { return _CostCenter; }
            set
            {
                if (value != null && value.Length > 10)
                { _CostCenter = value.Substring(0, 10); }
                else
                {
                    _CostCenter = value;
                    RaisePropertyChanged("CostCenter");
                }
            }
        }

        public string SvcCode
        {
            get { return _SvcCode; }
            set
            {
                if (value != null && value.Length > 10)
                { _SvcCode = value.Substring(0, 10); }
                else
                {
                    _SvcCode = value;
                    RaisePropertyChanged("SvcCode");
                }
            }
        }

        public string SvcDescription
        {
            get { return _SvcDescription; }
            set
            {
                if (value != null && value.Length > 100)
                { _SvcDescription = value.Substring(0, 100); }
                else
                {
                    _SvcDescription = value;
                    RaisePropertyChanged("SvcDescription");
                }
            }
        }

        public string UnitType
        {
            get { return _UnitType; }
            set
            {
                if (value != null && value.Length > 10)
                { _UnitType = value.Substring(0, 10); }
                else
                {
                    _UnitType = value;
                    RaisePropertyChanged("UnitType");
                }
            }
        }

        public double CostPerUnit
        {
            get { return _CostPerUnit; }
            set
            {
                _CostPerUnit = value;
                RaisePropertyChanged("CostPerUnit");
            }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set
            {
                if (value != null && value.Length > 1)
                { _ACTIVE = value.Substring(0, 1); }
                else
                {
                    _ACTIVE = value;
                    RaisePropertyChanged("ACTIVE");
                }
            }
        }

        public string AUTHREQ
        {
            get { return _AUTHREQ; }
            set
            {
                if (value != null && value.Length > 1)
                { _AUTHREQ = value.Substring(0, 1); }
                else
                {
                    _AUTHREQ = value;
                    RaisePropertyChanged("AUTHREQ");
                }
            }
        }

        public string COPAY
        {
            get { return _COPAY; }
            set
            {
                if (value != null && value.Length > 1)
                { _COPAY = value.Substring(0, 1); }
                else
                {
                    _COPAY = value;
                    RaisePropertyChanged("COPAY");
                }
            }
        }

        public string Modifier1
        {
            get { return _Modifier1; }
            set
            {
                if (value != null && value.Length > 10)
                { _Modifier1 = value.Substring(0, 10); }
                else
                {
                    _Modifier1 = value;
                    RaisePropertyChanged("Modifier1");
                }
            }
        }

        public string Modifier2
        {
            get { return _Modifier2; }
            set
            {
                if (value != null && value.Length > 10)
                { _Modifier2 = value.Substring(0, 10); }
                else
                {
                    _Modifier2 = value;
                    RaisePropertyChanged("Modifier2");
                }
            }
        }

        public string Modifier3
        {
            get { return _Modifier3; }
            set
            {
                if (value != null && value.Length > 10)
                { _Modifier3 = value.Substring(0, 10); }
                else
                {
                    _Modifier3 = value;
                    RaisePropertyChanged("Modifier3");
                }
            }
        }

        public string Modifier4
        {
            get { return _Modifier4; }
            set
            {
                if (value != null && value.Length > 10)
                { _Modifier4 = value.Substring(0, 10); }
                else
                {
                    _Modifier4 = value;
                    RaisePropertyChanged("Modifier4");
                }
            }
        }

        public int AUTOUNIT
        {
            get { return _AUTOUNIT; }
            set
            {
                _AUTOUNIT = value;
                RaisePropertyChanged("AUTOUNIT");
            }
        }

        public int ROUNDRULE
        {
            get { return _ROUNDRULE; }
            set
            {
                _ROUNDRULE = value;
                RaisePropertyChanged("ROUNDRULE");
            }
        }

        public string RelatedSplitCode
        {
            get { return _RelatedSplitCode; }
            set
            {
                if (value != null && value.Length > 10)
                { _RelatedSplitCode = value.Substring(0, 10); }
                else
                {
                    _RelatedSplitCode = value;
                    RaisePropertyChanged("RelatedSplitCode");
                }
            }
        }

        public bool BCBANOTEREQUIRED
        {
            get { return _BCBANOTEREQUIRED; }
            set
            {
                _BCBANOTEREQUIRED = value;
                RaisePropertyChanged("BCBANOTEREQUIRED");
            }
        }


        #endregion

        #region Implement INotifyPropertyChanged 

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Constructor

        public tblLOOKUPSERVICES()
        {
            // Constructor code goes here.
            Initialize();
        }

        public tblLOOKUPSERVICES(string DSN)
        {
            // Constructor code goes here.
            Initialize();
            classDatabaseConnectionString = DSN;
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            _SvcID = 0;
            _Funder = "";
            _CostCenter = "";
            _SvcCode = "";
            _SvcDescription = "";
            _UnitType = "";
            _CostPerUnit = 0.0;
            _ACTIVE = "";
            _AUTHREQ = "";
            _COPAY = "";
            _Modifier1 = "";
            _Modifier2 = "";
            _Modifier3 = "";
            _Modifier4 = "";
            _AUTOUNIT = 0;
            _ROUNDRULE = 0;
            _RelatedSplitCode = "";
            _BCBANOTEREQUIRED = false;
        }

        public void CopyFields(SqlDataReader r)
        {
            try
            {
                if (!Convert.IsDBNull(r["SvcID"]))
                {
                    _SvcID = Convert.ToInt64(r["SvcID"]);
                }
                if (!Convert.IsDBNull(r["Funder"]))
                {
                    _Funder = r["Funder"] + "";
                }
                if (!Convert.IsDBNull(r["CostCenter"]))
                {
                    _CostCenter = r["CostCenter"] + "";
                }
                if (!Convert.IsDBNull(r["SvcCode"]))
                {
                    _SvcCode = r["SvcCode"] + "";
                }
                if (!Convert.IsDBNull(r["SvcDescription"]))
                {
                    _SvcDescription = r["SvcDescription"] + "";
                }
                if (!Convert.IsDBNull(r["UnitType"]))
                {
                    _UnitType = r["UnitType"] + "";
                }
                if (!Convert.IsDBNull(r["CostPerUnit"]))
                {
                    _CostPerUnit = Convert.ToDouble(r["CostPerUnit"]);
                }
                if (!Convert.IsDBNull(r["ACTIVE"]))
                {
                    _ACTIVE = r["ACTIVE"] + "";
                }
                if (!Convert.IsDBNull(r["AUTHREQ"]))
                {
                    _AUTHREQ = r["AUTHREQ"] + "";
                }
                if (!Convert.IsDBNull(r["COPAY"]))
                {
                    _COPAY = r["COPAY"] + "";
                }
                if (!Convert.IsDBNull(r["Modifier1"]))
                {
                    _Modifier1 = r["Modifier1"] + "";
                }
                if (!Convert.IsDBNull(r["Modifier2"]))
                {
                    _Modifier2 = r["Modifier2"] + "";
                }
                if (!Convert.IsDBNull(r["Modifier3"]))
                {
                    _Modifier3 = r["Modifier3"] + "";
                }
                if (!Convert.IsDBNull(r["Modifier4"]))
                {
                    _Modifier4 = r["Modifier4"] + "";
                }
                if (!Convert.IsDBNull(r["AUTOUNIT"]))
                {
                    _AUTOUNIT = Convert.ToInt32(r["AUTOUNIT"]);
                }
                if (!Convert.IsDBNull(r["ROUNDRULE"]))
                {
                    _ROUNDRULE = Convert.ToInt32(r["ROUNDRULE"]);
                }
                if (!Convert.IsDBNull(r["RelatedSplitCode"]))
                {
                    _RelatedSplitCode = r["RelatedSplitCode"] + "";
                }
                if (!Convert.IsDBNull(r["BCBANOTEREQUIRED"]))
                {
                    _BCBANOTEREQUIRED = Convert.ToBoolean(r["BCBANOTEREQUIRED"]);
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("tblLOOKUPSERVICES.CopyFields " + ex.ToString()));
            }
        }

        public bool RecExists(System.Int64 idx)
        {
            bool Result = false;
            try
            {
                string sql = "Select count(*) from tblLOOKUPSERVICES WHERE SvcID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    if (r.GetInt32(0) > 0)
                    {
                        Result = true;
                    }
                }
                r.Close();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblLOOKUPSERVICES.RecExists " + ex.ToString()));
            }

            return Result;
        }

        public void Add()
        {
            try
            {
                string sql = GetParameterSQL();
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (this._Funder == null || this._Funder == "" || this._Funder == string.Empty)
                {
                    cmd.Parameters.Add("@Funder", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Funder", System.Data.SqlDbType.VarChar).Value = this._Funder;
                }
                if (this._CostCenter == null || this._CostCenter == "" || this._CostCenter == string.Empty)
                {
                    cmd.Parameters.Add("@CostCenter", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@CostCenter", System.Data.SqlDbType.VarChar).Value = this._CostCenter;
                }
                if (this._SvcCode == null || this._SvcCode == "" || this._SvcCode == string.Empty)
                {
                    cmd.Parameters.Add("@SvcCode", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SvcCode", System.Data.SqlDbType.VarChar).Value = this._SvcCode;
                }
                if (this._SvcDescription == null || this._SvcDescription == "" || this._SvcDescription == string.Empty)
                {
                    cmd.Parameters.Add("@SvcDescription", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SvcDescription", System.Data.SqlDbType.VarChar).Value = this._SvcDescription;
                }
                if (this._UnitType == null || this._UnitType == "" || this._UnitType == string.Empty)
                {
                    cmd.Parameters.Add("@UnitType", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@UnitType", System.Data.SqlDbType.VarChar).Value = this._UnitType;
                }
                cmd.Parameters.Add("@CostPerUnit", System.Data.SqlDbType.Money).Value = this._CostPerUnit;
                if (this._ACTIVE == null || this._ACTIVE == "" || this._ACTIVE == string.Empty)
                {
                    cmd.Parameters.Add("@ACTIVE", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@ACTIVE", System.Data.SqlDbType.VarChar).Value = this._ACTIVE;
                }
                if (this._AUTHREQ == null || this._AUTHREQ == "" || this._AUTHREQ == string.Empty)
                {
                    cmd.Parameters.Add("@AUTHREQ", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@AUTHREQ", System.Data.SqlDbType.VarChar).Value = this._AUTHREQ;
                }
                if (this._COPAY == null || this._COPAY == "" || this._COPAY == string.Empty)
                {
                    cmd.Parameters.Add("@COPAY", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@COPAY", System.Data.SqlDbType.VarChar).Value = this._COPAY;
                }
                if (this._Modifier1 == null || this._Modifier1 == "" || this._Modifier1 == string.Empty)
                {
                    cmd.Parameters.Add("@Modifier1", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Modifier1", System.Data.SqlDbType.VarChar).Value = this._Modifier1;
                }
                if (this._Modifier2 == null || this._Modifier2 == "" || this._Modifier2 == string.Empty)
                {
                    cmd.Parameters.Add("@Modifier2", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Modifier2", System.Data.SqlDbType.VarChar).Value = this._Modifier2;
                }
                if (this._Modifier3 == null || this._Modifier3 == "" || this._Modifier3 == string.Empty)
                {
                    cmd.Parameters.Add("@Modifier3", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Modifier3", System.Data.SqlDbType.VarChar).Value = this._Modifier3;
                }
                if (this._Modifier4 == null || this._Modifier4 == "" || this._Modifier4 == string.Empty)
                {
                    cmd.Parameters.Add("@Modifier4", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Modifier4", System.Data.SqlDbType.VarChar).Value = this._Modifier4;
                }
                cmd.Parameters.Add("@AUTOUNIT", System.Data.SqlDbType.Int).Value = this._AUTOUNIT;
                cmd.Parameters.Add("@ROUNDRULE", System.Data.SqlDbType.Int).Value = this._ROUNDRULE;
                if (this._RelatedSplitCode == null || this._RelatedSplitCode == "" || this._RelatedSplitCode == string.Empty)
                {
                    cmd.Parameters.Add("@RelatedSplitCode", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@RelatedSplitCode", System.Data.SqlDbType.VarChar).Value = this._RelatedSplitCode;
                }
                cmd.Parameters.Add("@BCBANOTEREQUIRED", System.Data.SqlDbType.Bit).Value = this._BCBANOTEREQUIRED;
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                if (SvcID < 1)
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT @@IDENTITY", cn);
                    System.Int64 ii = Convert.ToInt64(cmd2.ExecuteScalar());
                    cmd2.Cancel();
                    cmd2.Dispose();
                    _SvcID = ii;
                }
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblLOOKUPSERVICES.Add " + ex.ToString()));
            }
        }

        public void Update()
        {
            try
            {
                string sql = GetParameterSQL();
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (this._Funder == null || this._Funder == "" || this._Funder == string.Empty)
                {
                    cmd.Parameters.Add("@Funder", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Funder", System.Data.SqlDbType.VarChar).Value = this._Funder;
                }
                if (this._CostCenter == null || this._CostCenter == "" || this._CostCenter == string.Empty)
                {
                    cmd.Parameters.Add("@CostCenter", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@CostCenter", System.Data.SqlDbType.VarChar).Value = this._CostCenter;
                }
                if (this._SvcCode == null || this._SvcCode == "" || this._SvcCode == string.Empty)
                {
                    cmd.Parameters.Add("@SvcCode", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SvcCode", System.Data.SqlDbType.VarChar).Value = this._SvcCode;
                }
                if (this._SvcDescription == null || this._SvcDescription == "" || this._SvcDescription == string.Empty)
                {
                    cmd.Parameters.Add("@SvcDescription", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SvcDescription", System.Data.SqlDbType.VarChar).Value = this._SvcDescription;
                }
                if (this._UnitType == null || this._UnitType == "" || this._UnitType == string.Empty)
                {
                    cmd.Parameters.Add("@UnitType", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@UnitType", System.Data.SqlDbType.VarChar).Value = this._UnitType;
                }
                cmd.Parameters.Add("@CostPerUnit", System.Data.SqlDbType.Money).Value = this._CostPerUnit;
                if (this._ACTIVE == null || this._ACTIVE == "" || this._ACTIVE == string.Empty)
                {
                    cmd.Parameters.Add("@ACTIVE", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@ACTIVE", System.Data.SqlDbType.VarChar).Value = this._ACTIVE;
                }
                if (this._AUTHREQ == null || this._AUTHREQ == "" || this._AUTHREQ == string.Empty)
                {
                    cmd.Parameters.Add("@AUTHREQ", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@AUTHREQ", System.Data.SqlDbType.VarChar).Value = this._AUTHREQ;
                }
                if (this._COPAY == null || this._COPAY == "" || this._COPAY == string.Empty)
                {
                    cmd.Parameters.Add("@COPAY", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@COPAY", System.Data.SqlDbType.VarChar).Value = this._COPAY;
                }
                if (this._Modifier1 == null || this._Modifier1 == "" || this._Modifier1 == string.Empty)
                {
                    cmd.Parameters.Add("@Modifier1", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Modifier1", System.Data.SqlDbType.VarChar).Value = this._Modifier1;
                }
                if (this._Modifier2 == null || this._Modifier2 == "" || this._Modifier2 == string.Empty)
                {
                    cmd.Parameters.Add("@Modifier2", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Modifier2", System.Data.SqlDbType.VarChar).Value = this._Modifier2;
                }
                if (this._Modifier3 == null || this._Modifier3 == "" || this._Modifier3 == string.Empty)
                {
                    cmd.Parameters.Add("@Modifier3", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Modifier3", System.Data.SqlDbType.VarChar).Value = this._Modifier3;
                }
                if (this._Modifier4 == null || this._Modifier4 == "" || this._Modifier4 == string.Empty)
                {
                    cmd.Parameters.Add("@Modifier4", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Modifier4", System.Data.SqlDbType.VarChar).Value = this._Modifier4;
                }
                cmd.Parameters.Add("@AUTOUNIT", System.Data.SqlDbType.Int).Value = this._AUTOUNIT;
                cmd.Parameters.Add("@ROUNDRULE", System.Data.SqlDbType.Int).Value = this._ROUNDRULE;
                if (this._RelatedSplitCode == null || this._RelatedSplitCode == "" || this._RelatedSplitCode == string.Empty)
                {
                    cmd.Parameters.Add("@RelatedSplitCode", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@RelatedSplitCode", System.Data.SqlDbType.VarChar).Value = this._RelatedSplitCode;
                }
                cmd.Parameters.Add("@BCBANOTEREQUIRED", System.Data.SqlDbType.Bit).Value = this._BCBANOTEREQUIRED;
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                if (SvcID < 1)
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT @@IDENTITY", cn);
                    System.Int64 ii = Convert.ToInt64(cmd2.ExecuteScalar());
                    cmd2.Cancel();
                    cmd2.Dispose();
                    _SvcID = ii;
                }
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblLOOKUPSERVICES.Update " + ex.ToString()));
            }
        }

        public void Delete()
        {
            try
            {
                string sql = "Delete from tblLOOKUPSERVICES WHERE SvcID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = this._SvcID;
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblLOOKUPSERVICES.Delete " + ex.ToString()));
            }
        }

        public void Read(System.Int64 idx)
        {
            try
            {
                string sql = "Select * from tblLOOKUPSERVICES WHERE SvcID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    this.CopyFields(r);
                }
                r.Close();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblLOOKUPSERVICES.Read " + ex.ToString()));
            }
        }

        public void Read(string idx)
        {
            try
            {
                System.Int64 theidx = -1;

                if (long.TryParse(idx, out theidx))
                {
                    Read(theidx);
                }
                else
                {
                    Initialize();
                }

            }
            catch
            {
                Initialize();

            }
        }

        public DataSet ReadAsDataSet(System.Int64 idx)
        {
            try
            {
                string sql = "Select * from tblLOOKUPSERVICES WHERE SvcID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "tblLOOKUPSERVICES");
                da.Dispose();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                throw (new Exception("tblLOOKUPSERVICES.ReadAsDataSet " + ex.ToString()));
            }
        }

        #endregion

        #region Private Methods

        private string GetParameterSQL()
        {
            string sql = "";
            if (_SvcID < 1)
            {
                sql = "INSERT INTO tblLOOKUPSERVICES";
                sql += "(";
                sql += "[Funder], [CostCenter], [SvcCode], [SvcDescription], [UnitType], [CostPerUnit],";
                sql += "[ACTIVE], [AUTHREQ], [COPAY], [Modifier1], [Modifier2], [Modifier3], [Modifier4],";
                sql += "[AUTOUNIT], [ROUNDRULE], [RelatedSplitCode], [BCBANOTEREQUIRED])";
                sql += " VALUES (";
                sql += "@Funder,@CostCenter,@SvcCode,@SvcDescription,@UnitType,@CostPerUnit,@ACTIVE,";
                sql += "@AUTHREQ,@COPAY,@Modifier1,@Modifier2,@Modifier3,@Modifier4,@AUTOUNIT,@ROUNDRULE,";
                sql += "@RelatedSplitCode,@BCBANOTEREQUIRED)";
            }
            else
            {
                sql = "UPDATE tblLOOKUPSERVICES SET ";
                sql += "[Funder] = @Funder, [CostCenter] = @CostCenter, [SvcCode] = @SvcCode, [SvcDescription] = @SvcDescription,";
                sql += "[UnitType] = @UnitType, [CostPerUnit] = @CostPerUnit, [ACTIVE] = @ACTIVE,";
                sql += "[AUTHREQ] = @AUTHREQ, [COPAY] = @COPAY, [Modifier1] = @Modifier1, [Modifier2] = @Modifier2,";
                sql += "[Modifier3] = @Modifier3, [Modifier4] = @Modifier4, [AUTOUNIT] = @AUTOUNIT,";
                sql += "[ROUNDRULE] = @ROUNDRULE, [RelatedSplitCode] = @RelatedSplitCode, [BCBANOTEREQUIRED] = @BCBANOTEREQUIRED";
                sql += "";
                sql += " WHERE SvcID = " + _SvcID.ToString();
            }
            return sql;
        }

        private object getDateOrNull(DateTime d)
        {
            if (d == Convert.ToDateTime(null))
            {
                return DBNull.Value;
            }
            else
            {
                return d;
            }
        }
        #endregion
    }

    public partial class tblUserMessages : INotifyPropertyChanged
    {

        #region Declarations
        string _classDatabaseConnectionString = "";
        string _bulkinsertPath = "";

        SqlConnection _cn = new SqlConnection();
        SqlCommand _cmd = new SqlCommand();

        // Backing Variables for Properties
        long _msgID = 0;
        string _SOURCE = "";
        string _DESTINATION = "";
        DateTime _DATECREATED = Convert.ToDateTime(null);
        string _MESSAGETYPE = "";
        string _READSTATUS = "";
        string _BODY = "";

        #endregion

        #region Properties

        public string classDatabaseConnectionString
        {
            get { return _classDatabaseConnectionString; }
            set { _classDatabaseConnectionString = value; }
        }

        public string bulkinsertPath
        {
            get { return _bulkinsertPath; }
            set { _bulkinsertPath = value; }
        }

        public long msgID
        {
            get { return _msgID; }
            set
            {
                _msgID = value;
                RaisePropertyChanged("msgID");
            }
        }

        public string SOURCE
        {
            get { return _SOURCE; }
            set
            {
                if (value != null && value.Length > 20)
                { _SOURCE = value.Substring(0, 20); }
                else
                {
                    _SOURCE = value;
                    RaisePropertyChanged("SOURCE");
                }
            }
        }

        public string DESTINATION
        {
            get { return _DESTINATION; }
            set
            {
                if (value != null && value.Length > 20)
                { _DESTINATION = value.Substring(0, 20); }
                else
                {
                    _DESTINATION = value;
                    RaisePropertyChanged("DESTINATION");
                }
            }
        }

        public DateTime DATECREATED
        {
            get { return _DATECREATED; }
            set
            {
                _DATECREATED = value;
                RaisePropertyChanged("DATECREATED");
            }
        }

        public string MESSAGETYPE
        {
            get { return _MESSAGETYPE; }
            set
            {
                if (value != null && value.Length > 5)
                { _MESSAGETYPE = value.Substring(0, 5); }
                else
                {
                    _MESSAGETYPE = value;
                    RaisePropertyChanged("MESSAGETYPE");
                }
            }
        }

        public string READSTATUS
        {
            get { return _READSTATUS; }
            set
            {
                if (value != null && value.Length > 1)
                { _READSTATUS = value.Substring(0, 1); }
                else
                {
                    _READSTATUS = value;
                    RaisePropertyChanged("READSTATUS");
                }
            }
        }

        public string BODY
        {
            get { return _BODY; }
            set
            {
                if (value != null && value.Length > 2000)
                { _BODY = value.Substring(0, 2000); }
                else
                {
                    _BODY = value;
                    RaisePropertyChanged("BODY");
                }
            }
        }


        #endregion

        #region Implement INotifyPropertyChanged 

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Constructor

        public tblUserMessages()
        {
            // Constructor code goes here.
            Initialize();
        }

        public tblUserMessages(string DSN)
        {
            // Constructor code goes here.
            Initialize();
            classDatabaseConnectionString = DSN;
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            _msgID = 0;
            _SOURCE = "";
            _DESTINATION = "";
            _DATECREATED = Convert.ToDateTime(null);
            _MESSAGETYPE = "";
            _READSTATUS = "";
            _BODY = "";
        }

        public void CopyFields(SqlDataReader r)
        {
            try
            {
                if (!Convert.IsDBNull(r["msgID"]))
                {
                    _msgID = Convert.ToInt64(r["msgID"]);
                }
                if (!Convert.IsDBNull(r["SOURCE"]))
                {
                    _SOURCE = r["SOURCE"] + "";
                }
                if (!Convert.IsDBNull(r["DESTINATION"]))
                {
                    _DESTINATION = r["DESTINATION"] + "";
                }
                if (!Convert.IsDBNull(r["DATECREATED"]))
                {
                    _DATECREATED = Convert.ToDateTime(r["DATECREATED"]);
                }
                if (!Convert.IsDBNull(r["MESSAGETYPE"]))
                {
                    _MESSAGETYPE = r["MESSAGETYPE"] + "";
                }
                if (!Convert.IsDBNull(r["READSTATUS"]))
                {
                    _READSTATUS = r["READSTATUS"] + "";
                }
                if (!Convert.IsDBNull(r["BODY"]))
                {
                    _BODY = r["BODY"] + "";
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("tblUserMessages.CopyFields " + ex.ToString()));
            }
        }

        public bool RecExists(System.Int64 idx)
        {
            bool Result = false;
            try
            {
                string sql = "Select count(*) from tblUserMessages WHERE msgID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    if (r.GetInt32(0) > 0)
                    {
                        Result = true;
                    }
                }
                r.Close();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblUserMessages.RecExists " + ex.ToString()));
            }

            return Result;
        }

        public void Add()
        {
            try
            {
                string sql = GetParameterSQL();
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (this._SOURCE == null || this._SOURCE == "" || this._SOURCE == string.Empty)
                {
                    cmd.Parameters.Add("@SOURCE", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SOURCE", System.Data.SqlDbType.VarChar).Value = this._SOURCE;
                }
                if (this._DESTINATION == null || this._DESTINATION == "" || this._DESTINATION == string.Empty)
                {
                    cmd.Parameters.Add("@DESTINATION", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DESTINATION", System.Data.SqlDbType.VarChar).Value = this._DESTINATION;
                }
                cmd.Parameters.Add("@DATECREATED", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._DATECREATED);
                if (this._MESSAGETYPE == null || this._MESSAGETYPE == "" || this._MESSAGETYPE == string.Empty)
                {
                    cmd.Parameters.Add("@MESSAGETYPE", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@MESSAGETYPE", System.Data.SqlDbType.VarChar).Value = this._MESSAGETYPE;
                }
                if (this._READSTATUS == null || this._READSTATUS == "" || this._READSTATUS == string.Empty)
                {
                    cmd.Parameters.Add("@READSTATUS", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@READSTATUS", System.Data.SqlDbType.VarChar).Value = this._READSTATUS;
                }
                if (this._BODY == null || this._BODY == "" || this._BODY == string.Empty)
                {
                    cmd.Parameters.Add("@BODY", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@BODY", System.Data.SqlDbType.VarChar).Value = this._BODY;
                }
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                if (msgID < 1)
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT @@IDENTITY", cn);
                    System.Int64 ii = Convert.ToInt64(cmd2.ExecuteScalar());
                    cmd2.Cancel();
                    cmd2.Dispose();
                    _msgID = ii;
                }
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblUserMessages.Add " + ex.ToString()));
            }
        }

        public void Update()
        {
            try
            {
                string sql = GetParameterSQL();
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (this._SOURCE == null || this._SOURCE == "" || this._SOURCE == string.Empty)
                {
                    cmd.Parameters.Add("@SOURCE", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SOURCE", System.Data.SqlDbType.VarChar).Value = this._SOURCE;
                }
                if (this._DESTINATION == null || this._DESTINATION == "" || this._DESTINATION == string.Empty)
                {
                    cmd.Parameters.Add("@DESTINATION", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DESTINATION", System.Data.SqlDbType.VarChar).Value = this._DESTINATION;
                }
                cmd.Parameters.Add("@DATECREATED", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._DATECREATED);
                if (this._MESSAGETYPE == null || this._MESSAGETYPE == "" || this._MESSAGETYPE == string.Empty)
                {
                    cmd.Parameters.Add("@MESSAGETYPE", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@MESSAGETYPE", System.Data.SqlDbType.VarChar).Value = this._MESSAGETYPE;
                }
                if (this._READSTATUS == null || this._READSTATUS == "" || this._READSTATUS == string.Empty)
                {
                    cmd.Parameters.Add("@READSTATUS", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@READSTATUS", System.Data.SqlDbType.VarChar).Value = this._READSTATUS;
                }
                if (this._BODY == null || this._BODY == "" || this._BODY == string.Empty)
                {
                    cmd.Parameters.Add("@BODY", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@BODY", System.Data.SqlDbType.VarChar).Value = this._BODY;
                }
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                if (msgID < 1)
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT @@IDENTITY", cn);
                    System.Int64 ii = Convert.ToInt64(cmd2.ExecuteScalar());
                    cmd2.Cancel();
                    cmd2.Dispose();
                    _msgID = ii;
                }
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblUserMessages.Update " + ex.ToString()));
            }
        }

        public void Delete()
        {
            try
            {
                string sql = "Delete from tblUserMessages WHERE msgID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = this._msgID;
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblUserMessages.Delete " + ex.ToString()));
            }
        }

        public void Read(System.Int64 idx)
        {
            try
            {
                string sql = "Select * from tblUserMessages WHERE msgID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    this.CopyFields(r);
                }
                r.Close();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblUserMessages.Read " + ex.ToString()));
            }
        }

        public DataSet ReadAsDataSet(System.Int64 idx)
        {
            try
            {
                string sql = "Select * from tblUserMessages WHERE msgID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "tblUserMessages");
                da.Dispose();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                throw (new Exception("tblUserMessages.ReadAsDataSet " + ex.ToString()));
            }
        }

        #endregion

        #region Private Methods

        private string GetParameterSQL()
        {
            string sql = "";
            if (_msgID < 1)
            {
                sql = "INSERT INTO tblUserMessages";
                sql += "(";
                sql += "[SOURCE], [DESTINATION], [DATECREATED], [MESSAGETYPE], [READSTATUS], [BODY])";
                sql += " VALUES (";
                sql += "@SOURCE,@DESTINATION,@DATECREATED,@MESSAGETYPE,@READSTATUS,@BODY)";
            }
            else
            {
                sql = "UPDATE tblUserMessages SET ";
                sql += "[SOURCE] = @SOURCE, [DESTINATION] = @DESTINATION, [DATECREATED] = @DATECREATED,";
                sql += "[MESSAGETYPE] = @MESSAGETYPE, [READSTATUS] = @READSTATUS, [BODY] = @BODY";
                sql += "";
                sql += " WHERE msgID = " + _msgID.ToString();
            }
            return sql;
        }

        private object getDateOrNull(DateTime d)
        {
            if (d == Convert.ToDateTime(null))
            {
                return DBNull.Value;
            }
            else
            {
                return d;
            }
        }
        #endregion
    }




    #endregion

}

