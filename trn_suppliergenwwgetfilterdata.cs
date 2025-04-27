using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using System.Data;
using GeneXus.Data;
using com.genexus;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.WebControls;
using GeneXus.Http;
using GeneXus.Procedure;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace GeneXus.Programs {
   public class trn_suppliergenwwgetfilterdata : GXProcedure
   {
      protected override bool IntegratedSecurityEnabled
      {
         get {
            return true ;
         }

      }

      protected override GAMSecurityLevel IntegratedSecurityLevel
      {
         get {
            return GAMSecurityLevel.SecurityHigh ;
         }

      }

      protected override string ExecutePermissionPrefix
      {
         get {
            return "trn_suppliergenww_Services_Execute" ;
         }

      }

      public trn_suppliergenwwgetfilterdata( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public trn_suppliergenwwgetfilterdata( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_DDOName ,
                           string aP1_SearchTxtParms ,
                           string aP2_SearchTxtTo ,
                           out string aP3_OptionsJson ,
                           out string aP4_OptionsDescJson ,
                           out string aP5_OptionIndexesJson )
      {
         this.AV37DDOName = aP0_DDOName;
         this.AV38SearchTxtParms = aP1_SearchTxtParms;
         this.AV39SearchTxtTo = aP2_SearchTxtTo;
         this.AV40OptionsJson = "" ;
         this.AV41OptionsDescJson = "" ;
         this.AV42OptionIndexesJson = "" ;
         initialize();
         ExecuteImpl();
         aP3_OptionsJson=this.AV40OptionsJson;
         aP4_OptionsDescJson=this.AV41OptionsDescJson;
         aP5_OptionIndexesJson=this.AV42OptionIndexesJson;
      }

      public string executeUdp( string aP0_DDOName ,
                                string aP1_SearchTxtParms ,
                                string aP2_SearchTxtTo ,
                                out string aP3_OptionsJson ,
                                out string aP4_OptionsDescJson )
      {
         execute(aP0_DDOName, aP1_SearchTxtParms, aP2_SearchTxtTo, out aP3_OptionsJson, out aP4_OptionsDescJson, out aP5_OptionIndexesJson);
         return AV42OptionIndexesJson ;
      }

      public void executeSubmit( string aP0_DDOName ,
                                 string aP1_SearchTxtParms ,
                                 string aP2_SearchTxtTo ,
                                 out string aP3_OptionsJson ,
                                 out string aP4_OptionsDescJson ,
                                 out string aP5_OptionIndexesJson )
      {
         this.AV37DDOName = aP0_DDOName;
         this.AV38SearchTxtParms = aP1_SearchTxtParms;
         this.AV39SearchTxtTo = aP2_SearchTxtTo;
         this.AV40OptionsJson = "" ;
         this.AV41OptionsDescJson = "" ;
         this.AV42OptionIndexesJson = "" ;
         SubmitImpl();
         aP3_OptionsJson=this.AV40OptionsJson;
         aP4_OptionsDescJson=this.AV41OptionsDescJson;
         aP5_OptionIndexesJson=this.AV42OptionIndexesJson;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV27Options = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV29OptionsDesc = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV30OptionIndexes = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV24MaxItems = 10;
         AV23PageIndex = (short)((String.IsNullOrEmpty(StringUtil.RTrim( AV38SearchTxtParms)) ? 0 : (long)(Math.Round(NumberUtil.Val( StringUtil.Substring( AV38SearchTxtParms, 1, 2), "."), 18, MidpointRounding.ToEven))));
         AV21SearchTxt = (String.IsNullOrEmpty(StringUtil.RTrim( AV38SearchTxtParms)) ? "" : StringUtil.Substring( AV38SearchTxtParms, 3, -1));
         AV22SkipItems = (short)(AV23PageIndex*AV24MaxItems);
         new GeneXus.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV9WWPContext) ;
         /* Execute user subroutine: 'LOADGRIDSTATE' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         if ( StringUtil.StrCmp(StringUtil.Upper( AV37DDOName), "DDO_SUPPLIERGENCOMPANYNAME") == 0 )
         {
            /* Execute user subroutine: 'LOADSUPPLIERGENCOMPANYNAMEOPTIONS' */
            S121 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         else if ( StringUtil.StrCmp(StringUtil.Upper( AV37DDOName), "DDO_SUPPLIERGENTYPENAME") == 0 )
         {
            /* Execute user subroutine: 'LOADSUPPLIERGENTYPENAMEOPTIONS' */
            S131 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         else if ( StringUtil.StrCmp(StringUtil.Upper( AV37DDOName), "DDO_SUPPLIERGENCONTACTNAME") == 0 )
         {
            /* Execute user subroutine: 'LOADSUPPLIERGENCONTACTNAMEOPTIONS' */
            S141 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         else if ( StringUtil.StrCmp(StringUtil.Upper( AV37DDOName), "DDO_SUPPLIERGENCONTACTPHONE") == 0 )
         {
            /* Execute user subroutine: 'LOADSUPPLIERGENCONTACTPHONEOPTIONS' */
            S151 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         else if ( StringUtil.StrCmp(StringUtil.Upper( AV37DDOName), "DDO_SUPPLIERGENDESCRIPTION") == 0 )
         {
            /* Execute user subroutine: 'LOADSUPPLIERGENDESCRIPTIONOPTIONS' */
            S161 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         AV40OptionsJson = AV27Options.ToJSonString(false);
         AV41OptionsDescJson = AV29OptionsDesc.ToJSonString(false);
         AV42OptionIndexesJson = AV30OptionIndexes.ToJSonString(false);
         cleanup();
      }

      protected void S111( )
      {
         /* 'LOADGRIDSTATE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV32Session.Get("Trn_SupplierGenWWGridState"), "") == 0 )
         {
            AV34GridState.FromXml(new WorkWithPlus.workwithplus_web.loadgridstate(context).executeUdp(  "Trn_SupplierGenWWGridState"), null, "", "");
         }
         else
         {
            AV34GridState.FromXml(AV32Session.Get("Trn_SupplierGenWWGridState"), null, "", "");
         }
         AV65GXV1 = 1;
         while ( AV65GXV1 <= AV34GridState.gxTpr_Filtervalues.Count )
         {
            AV35GridStateFilterValue = ((WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue)AV34GridState.gxTpr_Filtervalues.Item(AV65GXV1));
            if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV43FilterFullText = AV35GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "TFSUPPLIERGENCOMPANYNAME") == 0 )
            {
               AV15TFSupplierGenCompanyName = AV35GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "TFSUPPLIERGENCOMPANYNAME_SEL") == 0 )
            {
               AV16TFSupplierGenCompanyName_Sel = AV35GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "TFSUPPLIERGENTYPENAME") == 0 )
            {
               AV13TFSupplierGenTypeName = AV35GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "TFSUPPLIERGENTYPENAME_SEL") == 0 )
            {
               AV14TFSupplierGenTypeName_Sel = AV35GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "TFSUPPLIERGENCONTACTNAME") == 0 )
            {
               AV17TFSupplierGenContactName = AV35GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "TFSUPPLIERGENCONTACTNAME_SEL") == 0 )
            {
               AV18TFSupplierGenContactName_Sel = AV35GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "TFSUPPLIERGENCONTACTPHONE") == 0 )
            {
               AV19TFSupplierGenContactPhone = AV35GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "TFSUPPLIERGENCONTACTPHONE_SEL") == 0 )
            {
               AV20TFSupplierGenContactPhone_Sel = AV35GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "TFSUPPLIERGENDESCRIPTION") == 0 )
            {
               AV63TFSupplierGenDescription = AV35GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "TFSUPPLIERGENDESCRIPTION_SEL") == 0 )
            {
               AV64TFSupplierGenDescription_Sel = AV35GridStateFilterValue.gxTpr_Value;
            }
            AV65GXV1 = (int)(AV65GXV1+1);
         }
      }

      protected void S121( )
      {
         /* 'LOADSUPPLIERGENCOMPANYNAMEOPTIONS' Routine */
         returnInSub = false;
         AV15TFSupplierGenCompanyName = AV21SearchTxt;
         AV16TFSupplierGenCompanyName_Sel = "";
         AV67Trn_suppliergenwwds_1_filterfulltext = AV43FilterFullText;
         AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname = AV15TFSupplierGenCompanyName;
         AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel = AV16TFSupplierGenCompanyName_Sel;
         AV70Trn_suppliergenwwds_4_tfsuppliergentypename = AV13TFSupplierGenTypeName;
         AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel = AV14TFSupplierGenTypeName_Sel;
         AV72Trn_suppliergenwwds_6_tfsuppliergencontactname = AV17TFSupplierGenContactName;
         AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel = AV18TFSupplierGenContactName_Sel;
         AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone = AV19TFSupplierGenContactPhone;
         AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel = AV20TFSupplierGenContactPhone_Sel;
         AV76Trn_suppliergenwwds_10_tfsuppliergendescription = AV63TFSupplierGenDescription;
         AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel = AV64TFSupplierGenDescription_Sel;
         pr_default.dynParam(0, new Object[]{ new Object[]{
                                              AV67Trn_suppliergenwwds_1_filterfulltext ,
                                              AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel ,
                                              AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname ,
                                              AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel ,
                                              AV70Trn_suppliergenwwds_4_tfsuppliergentypename ,
                                              AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel ,
                                              AV72Trn_suppliergenwwds_6_tfsuppliergencontactname ,
                                              AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel ,
                                              AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone ,
                                              AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel ,
                                              AV76Trn_suppliergenwwds_10_tfsuppliergendescription ,
                                              A44SupplierGenCompanyName ,
                                              A254SupplierGenTypeName ,
                                              A47SupplierGenContactName ,
                                              A48SupplierGenContactPhone ,
                                              A604SupplierGenDescription ,
                                              A11OrganisationId } ,
                                              new int[]{
                                              TypeConstants.BOOLEAN
                                              }
         });
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname = StringUtil.Concat( StringUtil.RTrim( AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname), "%", "");
         lV70Trn_suppliergenwwds_4_tfsuppliergentypename = StringUtil.Concat( StringUtil.RTrim( AV70Trn_suppliergenwwds_4_tfsuppliergentypename), "%", "");
         lV72Trn_suppliergenwwds_6_tfsuppliergencontactname = StringUtil.Concat( StringUtil.RTrim( AV72Trn_suppliergenwwds_6_tfsuppliergencontactname), "%", "");
         lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone = StringUtil.PadR( StringUtil.RTrim( AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone), 20, "%");
         lV76Trn_suppliergenwwds_10_tfsuppliergendescription = StringUtil.Concat( StringUtil.RTrim( AV76Trn_suppliergenwwds_10_tfsuppliergendescription), "%", "");
         /* Using cursor P00672 */
         pr_default.execute(0, new Object[] {lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname, AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel, lV70Trn_suppliergenwwds_4_tfsuppliergentypename, AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel, lV72Trn_suppliergenwwds_6_tfsuppliergencontactname, AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel, lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone, AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel, lV76Trn_suppliergenwwds_10_tfsuppliergendescription, AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel});
         while ( (pr_default.getStatus(0) != 101) )
         {
            BRK672 = false;
            A253SupplierGenTypeId = P00672_A253SupplierGenTypeId[0];
            A602SG_LocationSupplierOrganisatio = P00672_A602SG_LocationSupplierOrganisatio[0];
            n602SG_LocationSupplierOrganisatio = P00672_n602SG_LocationSupplierOrganisatio[0];
            A601SG_LocationSupplierLocationId = P00672_A601SG_LocationSupplierLocationId[0];
            n601SG_LocationSupplierLocationId = P00672_n601SG_LocationSupplierLocationId[0];
            A584ActiveAppVersionId = P00672_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P00672_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = P00672_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P00672_n598PublishedActiveAppVersionId[0];
            A44SupplierGenCompanyName = P00672_A44SupplierGenCompanyName[0];
            A604SupplierGenDescription = P00672_A604SupplierGenDescription[0];
            A48SupplierGenContactPhone = P00672_A48SupplierGenContactPhone[0];
            A47SupplierGenContactName = P00672_A47SupplierGenContactName[0];
            A254SupplierGenTypeName = P00672_A254SupplierGenTypeName[0];
            A42SupplierGenId = P00672_A42SupplierGenId[0];
            A254SupplierGenTypeName = P00672_A254SupplierGenTypeName[0];
            A584ActiveAppVersionId = P00672_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P00672_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = P00672_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P00672_n598PublishedActiveAppVersionId[0];
            /* Using cursor P00673 */
            pr_default.execute(1, new Object[] {n602SG_LocationSupplierOrganisatio, A602SG_LocationSupplierOrganisatio});
            pr_default.close(1);
            /* Using cursor P00674 */
            pr_default.execute(2, new Object[] {n584ActiveAppVersionId, A584ActiveAppVersionId});
            pr_default.close(2);
            /* Using cursor P00675 */
            pr_default.execute(3, new Object[] {n598PublishedActiveAppVersionId, A598PublishedActiveAppVersionId});
            pr_default.close(3);
            AV31count = 0;
            while ( (pr_default.getStatus(0) != 101) && ( StringUtil.StrCmp(P00672_A44SupplierGenCompanyName[0], A44SupplierGenCompanyName) == 0 ) )
            {
               BRK672 = false;
               A42SupplierGenId = P00672_A42SupplierGenId[0];
               AV31count = (long)(AV31count+1);
               BRK672 = true;
               pr_default.readNext(0);
            }
            if ( (0==AV22SkipItems) )
            {
               AV26Option = (String.IsNullOrEmpty(StringUtil.RTrim( A44SupplierGenCompanyName)) ? "<#Empty#>" : A44SupplierGenCompanyName);
               AV27Options.Add(AV26Option, 0);
               AV30OptionIndexes.Add(StringUtil.Trim( context.localUtil.Format( (decimal)(AV31count), "Z,ZZZ,ZZZ,ZZ9")), 0);
               if ( AV27Options.Count == 10 )
               {
                  /* Exit For each command. Update data (if necessary), close cursors & exit. */
                  if (true) break;
               }
            }
            else
            {
               AV22SkipItems = (short)(AV22SkipItems-1);
            }
            if ( ! BRK672 )
            {
               BRK672 = true;
               pr_default.readNext(0);
            }
         }
         pr_default.close(0);
         pr_default.close(1);
         pr_default.close(3);
      }

      protected void S131( )
      {
         /* 'LOADSUPPLIERGENTYPENAMEOPTIONS' Routine */
         returnInSub = false;
         AV13TFSupplierGenTypeName = AV21SearchTxt;
         AV14TFSupplierGenTypeName_Sel = "";
         AV67Trn_suppliergenwwds_1_filterfulltext = AV43FilterFullText;
         AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname = AV15TFSupplierGenCompanyName;
         AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel = AV16TFSupplierGenCompanyName_Sel;
         AV70Trn_suppliergenwwds_4_tfsuppliergentypename = AV13TFSupplierGenTypeName;
         AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel = AV14TFSupplierGenTypeName_Sel;
         AV72Trn_suppliergenwwds_6_tfsuppliergencontactname = AV17TFSupplierGenContactName;
         AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel = AV18TFSupplierGenContactName_Sel;
         AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone = AV19TFSupplierGenContactPhone;
         AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel = AV20TFSupplierGenContactPhone_Sel;
         AV76Trn_suppliergenwwds_10_tfsuppliergendescription = AV63TFSupplierGenDescription;
         AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel = AV64TFSupplierGenDescription_Sel;
         pr_default.dynParam(4, new Object[]{ new Object[]{
                                              AV67Trn_suppliergenwwds_1_filterfulltext ,
                                              AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel ,
                                              AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname ,
                                              AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel ,
                                              AV70Trn_suppliergenwwds_4_tfsuppliergentypename ,
                                              AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel ,
                                              AV72Trn_suppliergenwwds_6_tfsuppliergencontactname ,
                                              AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel ,
                                              AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone ,
                                              AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel ,
                                              AV76Trn_suppliergenwwds_10_tfsuppliergendescription ,
                                              A44SupplierGenCompanyName ,
                                              A254SupplierGenTypeName ,
                                              A47SupplierGenContactName ,
                                              A48SupplierGenContactPhone ,
                                              A604SupplierGenDescription ,
                                              A11OrganisationId } ,
                                              new int[]{
                                              TypeConstants.BOOLEAN
                                              }
         });
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname = StringUtil.Concat( StringUtil.RTrim( AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname), "%", "");
         lV70Trn_suppliergenwwds_4_tfsuppliergentypename = StringUtil.Concat( StringUtil.RTrim( AV70Trn_suppliergenwwds_4_tfsuppliergentypename), "%", "");
         lV72Trn_suppliergenwwds_6_tfsuppliergencontactname = StringUtil.Concat( StringUtil.RTrim( AV72Trn_suppliergenwwds_6_tfsuppliergencontactname), "%", "");
         lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone = StringUtil.PadR( StringUtil.RTrim( AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone), 20, "%");
         lV76Trn_suppliergenwwds_10_tfsuppliergendescription = StringUtil.Concat( StringUtil.RTrim( AV76Trn_suppliergenwwds_10_tfsuppliergendescription), "%", "");
         /* Using cursor P00676 */
         pr_default.execute(4, new Object[] {lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname, AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel, lV70Trn_suppliergenwwds_4_tfsuppliergentypename, AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel, lV72Trn_suppliergenwwds_6_tfsuppliergencontactname, AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel, lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone, AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel, lV76Trn_suppliergenwwds_10_tfsuppliergendescription, AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel});
         while ( (pr_default.getStatus(4) != 101) )
         {
            BRK674 = false;
            A602SG_LocationSupplierOrganisatio = P00676_A602SG_LocationSupplierOrganisatio[0];
            n602SG_LocationSupplierOrganisatio = P00676_n602SG_LocationSupplierOrganisatio[0];
            A601SG_LocationSupplierLocationId = P00676_A601SG_LocationSupplierLocationId[0];
            n601SG_LocationSupplierLocationId = P00676_n601SG_LocationSupplierLocationId[0];
            A584ActiveAppVersionId = P00676_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P00676_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = P00676_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P00676_n598PublishedActiveAppVersionId[0];
            A253SupplierGenTypeId = P00676_A253SupplierGenTypeId[0];
            A604SupplierGenDescription = P00676_A604SupplierGenDescription[0];
            A48SupplierGenContactPhone = P00676_A48SupplierGenContactPhone[0];
            A47SupplierGenContactName = P00676_A47SupplierGenContactName[0];
            A254SupplierGenTypeName = P00676_A254SupplierGenTypeName[0];
            A44SupplierGenCompanyName = P00676_A44SupplierGenCompanyName[0];
            A42SupplierGenId = P00676_A42SupplierGenId[0];
            A584ActiveAppVersionId = P00676_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P00676_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = P00676_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P00676_n598PublishedActiveAppVersionId[0];
            A254SupplierGenTypeName = P00676_A254SupplierGenTypeName[0];
            /* Using cursor P00677 */
            pr_default.execute(5, new Object[] {n602SG_LocationSupplierOrganisatio, A602SG_LocationSupplierOrganisatio});
            pr_default.close(5);
            /* Using cursor P00678 */
            pr_default.execute(6, new Object[] {n584ActiveAppVersionId, A584ActiveAppVersionId});
            pr_default.close(6);
            /* Using cursor P00679 */
            pr_default.execute(7, new Object[] {n598PublishedActiveAppVersionId, A598PublishedActiveAppVersionId});
            pr_default.close(7);
            AV31count = 0;
            while ( (pr_default.getStatus(4) != 101) && ( P00676_A253SupplierGenTypeId[0] == A253SupplierGenTypeId ) )
            {
               BRK674 = false;
               A42SupplierGenId = P00676_A42SupplierGenId[0];
               AV31count = (long)(AV31count+1);
               BRK674 = true;
               pr_default.readNext(4);
            }
            AV26Option = (String.IsNullOrEmpty(StringUtil.RTrim( A254SupplierGenTypeName)) ? "<#Empty#>" : A254SupplierGenTypeName);
            AV25InsertIndex = 1;
            while ( ( StringUtil.StrCmp(AV26Option, "<#Empty#>") != 0 ) && ( AV25InsertIndex <= AV27Options.Count ) && ( ( StringUtil.StrCmp(((string)AV27Options.Item(AV25InsertIndex)), AV26Option) < 0 ) || ( StringUtil.StrCmp(((string)AV27Options.Item(AV25InsertIndex)), "<#Empty#>") == 0 ) ) )
            {
               AV25InsertIndex = (int)(AV25InsertIndex+1);
            }
            AV27Options.Add(AV26Option, AV25InsertIndex);
            AV30OptionIndexes.Add(StringUtil.Trim( context.localUtil.Format( (decimal)(AV31count), "Z,ZZZ,ZZZ,ZZ9")), AV25InsertIndex);
            if ( AV27Options.Count == AV22SkipItems + 11 )
            {
               AV27Options.RemoveItem(AV27Options.Count);
               AV30OptionIndexes.RemoveItem(AV30OptionIndexes.Count);
            }
            if ( ! BRK674 )
            {
               BRK674 = true;
               pr_default.readNext(4);
            }
         }
         pr_default.close(4);
         pr_default.close(5);
         pr_default.close(7);
         while ( AV22SkipItems > 0 )
         {
            AV27Options.RemoveItem(1);
            AV30OptionIndexes.RemoveItem(1);
            AV22SkipItems = (short)(AV22SkipItems-1);
         }
      }

      protected void S141( )
      {
         /* 'LOADSUPPLIERGENCONTACTNAMEOPTIONS' Routine */
         returnInSub = false;
         AV17TFSupplierGenContactName = AV21SearchTxt;
         AV18TFSupplierGenContactName_Sel = "";
         AV67Trn_suppliergenwwds_1_filterfulltext = AV43FilterFullText;
         AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname = AV15TFSupplierGenCompanyName;
         AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel = AV16TFSupplierGenCompanyName_Sel;
         AV70Trn_suppliergenwwds_4_tfsuppliergentypename = AV13TFSupplierGenTypeName;
         AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel = AV14TFSupplierGenTypeName_Sel;
         AV72Trn_suppliergenwwds_6_tfsuppliergencontactname = AV17TFSupplierGenContactName;
         AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel = AV18TFSupplierGenContactName_Sel;
         AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone = AV19TFSupplierGenContactPhone;
         AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel = AV20TFSupplierGenContactPhone_Sel;
         AV76Trn_suppliergenwwds_10_tfsuppliergendescription = AV63TFSupplierGenDescription;
         AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel = AV64TFSupplierGenDescription_Sel;
         pr_default.dynParam(8, new Object[]{ new Object[]{
                                              AV67Trn_suppliergenwwds_1_filterfulltext ,
                                              AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel ,
                                              AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname ,
                                              AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel ,
                                              AV70Trn_suppliergenwwds_4_tfsuppliergentypename ,
                                              AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel ,
                                              AV72Trn_suppliergenwwds_6_tfsuppliergencontactname ,
                                              AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel ,
                                              AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone ,
                                              AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel ,
                                              AV76Trn_suppliergenwwds_10_tfsuppliergendescription ,
                                              A44SupplierGenCompanyName ,
                                              A254SupplierGenTypeName ,
                                              A47SupplierGenContactName ,
                                              A48SupplierGenContactPhone ,
                                              A604SupplierGenDescription ,
                                              A11OrganisationId } ,
                                              new int[]{
                                              TypeConstants.BOOLEAN
                                              }
         });
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname = StringUtil.Concat( StringUtil.RTrim( AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname), "%", "");
         lV70Trn_suppliergenwwds_4_tfsuppliergentypename = StringUtil.Concat( StringUtil.RTrim( AV70Trn_suppliergenwwds_4_tfsuppliergentypename), "%", "");
         lV72Trn_suppliergenwwds_6_tfsuppliergencontactname = StringUtil.Concat( StringUtil.RTrim( AV72Trn_suppliergenwwds_6_tfsuppliergencontactname), "%", "");
         lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone = StringUtil.PadR( StringUtil.RTrim( AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone), 20, "%");
         lV76Trn_suppliergenwwds_10_tfsuppliergendescription = StringUtil.Concat( StringUtil.RTrim( AV76Trn_suppliergenwwds_10_tfsuppliergendescription), "%", "");
         /* Using cursor P006710 */
         pr_default.execute(8, new Object[] {lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname, AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel, lV70Trn_suppliergenwwds_4_tfsuppliergentypename, AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel, lV72Trn_suppliergenwwds_6_tfsuppliergencontactname, AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel, lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone, AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel, lV76Trn_suppliergenwwds_10_tfsuppliergendescription, AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel});
         while ( (pr_default.getStatus(8) != 101) )
         {
            BRK676 = false;
            A253SupplierGenTypeId = P006710_A253SupplierGenTypeId[0];
            A602SG_LocationSupplierOrganisatio = P006710_A602SG_LocationSupplierOrganisatio[0];
            n602SG_LocationSupplierOrganisatio = P006710_n602SG_LocationSupplierOrganisatio[0];
            A601SG_LocationSupplierLocationId = P006710_A601SG_LocationSupplierLocationId[0];
            n601SG_LocationSupplierLocationId = P006710_n601SG_LocationSupplierLocationId[0];
            A584ActiveAppVersionId = P006710_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P006710_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = P006710_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P006710_n598PublishedActiveAppVersionId[0];
            A47SupplierGenContactName = P006710_A47SupplierGenContactName[0];
            A604SupplierGenDescription = P006710_A604SupplierGenDescription[0];
            A48SupplierGenContactPhone = P006710_A48SupplierGenContactPhone[0];
            A254SupplierGenTypeName = P006710_A254SupplierGenTypeName[0];
            A44SupplierGenCompanyName = P006710_A44SupplierGenCompanyName[0];
            A42SupplierGenId = P006710_A42SupplierGenId[0];
            A254SupplierGenTypeName = P006710_A254SupplierGenTypeName[0];
            A584ActiveAppVersionId = P006710_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P006710_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = P006710_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P006710_n598PublishedActiveAppVersionId[0];
            /* Using cursor P006711 */
            pr_default.execute(9, new Object[] {n602SG_LocationSupplierOrganisatio, A602SG_LocationSupplierOrganisatio});
            pr_default.close(9);
            /* Using cursor P006712 */
            pr_default.execute(10, new Object[] {n584ActiveAppVersionId, A584ActiveAppVersionId});
            pr_default.close(10);
            /* Using cursor P006713 */
            pr_default.execute(11, new Object[] {n598PublishedActiveAppVersionId, A598PublishedActiveAppVersionId});
            pr_default.close(11);
            AV31count = 0;
            while ( (pr_default.getStatus(8) != 101) && ( StringUtil.StrCmp(P006710_A47SupplierGenContactName[0], A47SupplierGenContactName) == 0 ) )
            {
               BRK676 = false;
               A42SupplierGenId = P006710_A42SupplierGenId[0];
               AV31count = (long)(AV31count+1);
               BRK676 = true;
               pr_default.readNext(8);
            }
            if ( (0==AV22SkipItems) )
            {
               AV26Option = (String.IsNullOrEmpty(StringUtil.RTrim( A47SupplierGenContactName)) ? "<#Empty#>" : A47SupplierGenContactName);
               AV27Options.Add(AV26Option, 0);
               AV30OptionIndexes.Add(StringUtil.Trim( context.localUtil.Format( (decimal)(AV31count), "Z,ZZZ,ZZZ,ZZ9")), 0);
               if ( AV27Options.Count == 10 )
               {
                  /* Exit For each command. Update data (if necessary), close cursors & exit. */
                  if (true) break;
               }
            }
            else
            {
               AV22SkipItems = (short)(AV22SkipItems-1);
            }
            if ( ! BRK676 )
            {
               BRK676 = true;
               pr_default.readNext(8);
            }
         }
         pr_default.close(8);
         pr_default.close(9);
         pr_default.close(11);
      }

      protected void S151( )
      {
         /* 'LOADSUPPLIERGENCONTACTPHONEOPTIONS' Routine */
         returnInSub = false;
         AV19TFSupplierGenContactPhone = AV21SearchTxt;
         AV20TFSupplierGenContactPhone_Sel = "";
         AV67Trn_suppliergenwwds_1_filterfulltext = AV43FilterFullText;
         AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname = AV15TFSupplierGenCompanyName;
         AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel = AV16TFSupplierGenCompanyName_Sel;
         AV70Trn_suppliergenwwds_4_tfsuppliergentypename = AV13TFSupplierGenTypeName;
         AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel = AV14TFSupplierGenTypeName_Sel;
         AV72Trn_suppliergenwwds_6_tfsuppliergencontactname = AV17TFSupplierGenContactName;
         AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel = AV18TFSupplierGenContactName_Sel;
         AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone = AV19TFSupplierGenContactPhone;
         AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel = AV20TFSupplierGenContactPhone_Sel;
         AV76Trn_suppliergenwwds_10_tfsuppliergendescription = AV63TFSupplierGenDescription;
         AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel = AV64TFSupplierGenDescription_Sel;
         pr_default.dynParam(12, new Object[]{ new Object[]{
                                              AV67Trn_suppliergenwwds_1_filterfulltext ,
                                              AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel ,
                                              AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname ,
                                              AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel ,
                                              AV70Trn_suppliergenwwds_4_tfsuppliergentypename ,
                                              AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel ,
                                              AV72Trn_suppliergenwwds_6_tfsuppliergencontactname ,
                                              AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel ,
                                              AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone ,
                                              AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel ,
                                              AV76Trn_suppliergenwwds_10_tfsuppliergendescription ,
                                              A44SupplierGenCompanyName ,
                                              A254SupplierGenTypeName ,
                                              A47SupplierGenContactName ,
                                              A48SupplierGenContactPhone ,
                                              A604SupplierGenDescription ,
                                              A11OrganisationId } ,
                                              new int[]{
                                              TypeConstants.BOOLEAN
                                              }
         });
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname = StringUtil.Concat( StringUtil.RTrim( AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname), "%", "");
         lV70Trn_suppliergenwwds_4_tfsuppliergentypename = StringUtil.Concat( StringUtil.RTrim( AV70Trn_suppliergenwwds_4_tfsuppliergentypename), "%", "");
         lV72Trn_suppliergenwwds_6_tfsuppliergencontactname = StringUtil.Concat( StringUtil.RTrim( AV72Trn_suppliergenwwds_6_tfsuppliergencontactname), "%", "");
         lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone = StringUtil.PadR( StringUtil.RTrim( AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone), 20, "%");
         lV76Trn_suppliergenwwds_10_tfsuppliergendescription = StringUtil.Concat( StringUtil.RTrim( AV76Trn_suppliergenwwds_10_tfsuppliergendescription), "%", "");
         /* Using cursor P006714 */
         pr_default.execute(12, new Object[] {lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname, AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel, lV70Trn_suppliergenwwds_4_tfsuppliergentypename, AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel, lV72Trn_suppliergenwwds_6_tfsuppliergencontactname, AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel, lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone, AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel, lV76Trn_suppliergenwwds_10_tfsuppliergendescription, AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel});
         while ( (pr_default.getStatus(12) != 101) )
         {
            BRK678 = false;
            A253SupplierGenTypeId = P006714_A253SupplierGenTypeId[0];
            A602SG_LocationSupplierOrganisatio = P006714_A602SG_LocationSupplierOrganisatio[0];
            n602SG_LocationSupplierOrganisatio = P006714_n602SG_LocationSupplierOrganisatio[0];
            A601SG_LocationSupplierLocationId = P006714_A601SG_LocationSupplierLocationId[0];
            n601SG_LocationSupplierLocationId = P006714_n601SG_LocationSupplierLocationId[0];
            A584ActiveAppVersionId = P006714_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P006714_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = P006714_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P006714_n598PublishedActiveAppVersionId[0];
            A48SupplierGenContactPhone = P006714_A48SupplierGenContactPhone[0];
            A604SupplierGenDescription = P006714_A604SupplierGenDescription[0];
            A47SupplierGenContactName = P006714_A47SupplierGenContactName[0];
            A254SupplierGenTypeName = P006714_A254SupplierGenTypeName[0];
            A44SupplierGenCompanyName = P006714_A44SupplierGenCompanyName[0];
            A42SupplierGenId = P006714_A42SupplierGenId[0];
            A254SupplierGenTypeName = P006714_A254SupplierGenTypeName[0];
            A584ActiveAppVersionId = P006714_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P006714_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = P006714_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P006714_n598PublishedActiveAppVersionId[0];
            /* Using cursor P006715 */
            pr_default.execute(13, new Object[] {n602SG_LocationSupplierOrganisatio, A602SG_LocationSupplierOrganisatio});
            pr_default.close(13);
            /* Using cursor P006716 */
            pr_default.execute(14, new Object[] {n584ActiveAppVersionId, A584ActiveAppVersionId});
            pr_default.close(14);
            /* Using cursor P006717 */
            pr_default.execute(15, new Object[] {n598PublishedActiveAppVersionId, A598PublishedActiveAppVersionId});
            pr_default.close(15);
            AV31count = 0;
            while ( (pr_default.getStatus(12) != 101) && ( StringUtil.StrCmp(P006714_A48SupplierGenContactPhone[0], A48SupplierGenContactPhone) == 0 ) )
            {
               BRK678 = false;
               A42SupplierGenId = P006714_A42SupplierGenId[0];
               AV31count = (long)(AV31count+1);
               BRK678 = true;
               pr_default.readNext(12);
            }
            if ( (0==AV22SkipItems) )
            {
               AV26Option = (String.IsNullOrEmpty(StringUtil.RTrim( A48SupplierGenContactPhone)) ? "<#Empty#>" : A48SupplierGenContactPhone);
               AV27Options.Add(AV26Option, 0);
               AV30OptionIndexes.Add(StringUtil.Trim( context.localUtil.Format( (decimal)(AV31count), "Z,ZZZ,ZZZ,ZZ9")), 0);
               if ( AV27Options.Count == 10 )
               {
                  /* Exit For each command. Update data (if necessary), close cursors & exit. */
                  if (true) break;
               }
            }
            else
            {
               AV22SkipItems = (short)(AV22SkipItems-1);
            }
            if ( ! BRK678 )
            {
               BRK678 = true;
               pr_default.readNext(12);
            }
         }
         pr_default.close(12);
         pr_default.close(13);
         pr_default.close(15);
      }

      protected void S161( )
      {
         /* 'LOADSUPPLIERGENDESCRIPTIONOPTIONS' Routine */
         returnInSub = false;
         AV63TFSupplierGenDescription = AV21SearchTxt;
         AV64TFSupplierGenDescription_Sel = "";
         AV67Trn_suppliergenwwds_1_filterfulltext = AV43FilterFullText;
         AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname = AV15TFSupplierGenCompanyName;
         AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel = AV16TFSupplierGenCompanyName_Sel;
         AV70Trn_suppliergenwwds_4_tfsuppliergentypename = AV13TFSupplierGenTypeName;
         AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel = AV14TFSupplierGenTypeName_Sel;
         AV72Trn_suppliergenwwds_6_tfsuppliergencontactname = AV17TFSupplierGenContactName;
         AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel = AV18TFSupplierGenContactName_Sel;
         AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone = AV19TFSupplierGenContactPhone;
         AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel = AV20TFSupplierGenContactPhone_Sel;
         AV76Trn_suppliergenwwds_10_tfsuppliergendescription = AV63TFSupplierGenDescription;
         AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel = AV64TFSupplierGenDescription_Sel;
         pr_default.dynParam(16, new Object[]{ new Object[]{
                                              AV67Trn_suppliergenwwds_1_filterfulltext ,
                                              AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel ,
                                              AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname ,
                                              AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel ,
                                              AV70Trn_suppliergenwwds_4_tfsuppliergentypename ,
                                              AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel ,
                                              AV72Trn_suppliergenwwds_6_tfsuppliergencontactname ,
                                              AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel ,
                                              AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone ,
                                              AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel ,
                                              AV76Trn_suppliergenwwds_10_tfsuppliergendescription ,
                                              A44SupplierGenCompanyName ,
                                              A254SupplierGenTypeName ,
                                              A47SupplierGenContactName ,
                                              A48SupplierGenContactPhone ,
                                              A604SupplierGenDescription ,
                                              A11OrganisationId } ,
                                              new int[]{
                                              TypeConstants.BOOLEAN
                                              }
         });
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV67Trn_suppliergenwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext), "%", "");
         lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname = StringUtil.Concat( StringUtil.RTrim( AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname), "%", "");
         lV70Trn_suppliergenwwds_4_tfsuppliergentypename = StringUtil.Concat( StringUtil.RTrim( AV70Trn_suppliergenwwds_4_tfsuppliergentypename), "%", "");
         lV72Trn_suppliergenwwds_6_tfsuppliergencontactname = StringUtil.Concat( StringUtil.RTrim( AV72Trn_suppliergenwwds_6_tfsuppliergencontactname), "%", "");
         lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone = StringUtil.PadR( StringUtil.RTrim( AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone), 20, "%");
         lV76Trn_suppliergenwwds_10_tfsuppliergendescription = StringUtil.Concat( StringUtil.RTrim( AV76Trn_suppliergenwwds_10_tfsuppliergendescription), "%", "");
         /* Using cursor P006718 */
         pr_default.execute(16, new Object[] {lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV67Trn_suppliergenwwds_1_filterfulltext, lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname, AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel, lV70Trn_suppliergenwwds_4_tfsuppliergentypename, AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel, lV72Trn_suppliergenwwds_6_tfsuppliergencontactname, AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel, lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone, AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel, lV76Trn_suppliergenwwds_10_tfsuppliergendescription, AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel});
         while ( (pr_default.getStatus(16) != 101) )
         {
            BRK6710 = false;
            A253SupplierGenTypeId = P006718_A253SupplierGenTypeId[0];
            A602SG_LocationSupplierOrganisatio = P006718_A602SG_LocationSupplierOrganisatio[0];
            n602SG_LocationSupplierOrganisatio = P006718_n602SG_LocationSupplierOrganisatio[0];
            A601SG_LocationSupplierLocationId = P006718_A601SG_LocationSupplierLocationId[0];
            n601SG_LocationSupplierLocationId = P006718_n601SG_LocationSupplierLocationId[0];
            A584ActiveAppVersionId = P006718_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P006718_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = P006718_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P006718_n598PublishedActiveAppVersionId[0];
            A604SupplierGenDescription = P006718_A604SupplierGenDescription[0];
            A48SupplierGenContactPhone = P006718_A48SupplierGenContactPhone[0];
            A47SupplierGenContactName = P006718_A47SupplierGenContactName[0];
            A254SupplierGenTypeName = P006718_A254SupplierGenTypeName[0];
            A44SupplierGenCompanyName = P006718_A44SupplierGenCompanyName[0];
            A42SupplierGenId = P006718_A42SupplierGenId[0];
            A254SupplierGenTypeName = P006718_A254SupplierGenTypeName[0];
            A584ActiveAppVersionId = P006718_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P006718_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = P006718_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P006718_n598PublishedActiveAppVersionId[0];
            /* Using cursor P006719 */
            pr_default.execute(17, new Object[] {n602SG_LocationSupplierOrganisatio, A602SG_LocationSupplierOrganisatio});
            pr_default.close(17);
            /* Using cursor P006720 */
            pr_default.execute(18, new Object[] {n584ActiveAppVersionId, A584ActiveAppVersionId});
            pr_default.close(18);
            /* Using cursor P006721 */
            pr_default.execute(19, new Object[] {n598PublishedActiveAppVersionId, A598PublishedActiveAppVersionId});
            pr_default.close(19);
            AV31count = 0;
            while ( (pr_default.getStatus(16) != 101) && ( StringUtil.StrCmp(P006718_A604SupplierGenDescription[0], A604SupplierGenDescription) == 0 ) )
            {
               BRK6710 = false;
               A42SupplierGenId = P006718_A42SupplierGenId[0];
               AV31count = (long)(AV31count+1);
               BRK6710 = true;
               pr_default.readNext(16);
            }
            if ( (0==AV22SkipItems) )
            {
               AV26Option = (String.IsNullOrEmpty(StringUtil.RTrim( A604SupplierGenDescription)) ? "<#Empty#>" : A604SupplierGenDescription);
               AV27Options.Add(AV26Option, 0);
               AV30OptionIndexes.Add(StringUtil.Trim( context.localUtil.Format( (decimal)(AV31count), "Z,ZZZ,ZZZ,ZZ9")), 0);
               if ( AV27Options.Count == 10 )
               {
                  /* Exit For each command. Update data (if necessary), close cursors & exit. */
                  if (true) break;
               }
            }
            else
            {
               AV22SkipItems = (short)(AV22SkipItems-1);
            }
            if ( ! BRK6710 )
            {
               BRK6710 = true;
               pr_default.readNext(16);
            }
         }
         pr_default.close(16);
         pr_default.close(17);
         pr_default.close(19);
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
         ExitApp();
      }

      protected override void CloseCursors( )
      {
         pr_default.close(18);
         pr_default.close(14);
         pr_default.close(10);
         pr_default.close(6);
         pr_default.close(2);
      }

      public override void initialize( )
      {
         AV40OptionsJson = "";
         AV41OptionsDescJson = "";
         AV42OptionIndexesJson = "";
         AV27Options = new GxSimpleCollection<string>();
         AV29OptionsDesc = new GxSimpleCollection<string>();
         AV30OptionIndexes = new GxSimpleCollection<string>();
         AV21SearchTxt = "";
         AV9WWPContext = new GeneXus.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV32Session = context.GetSession();
         AV34GridState = new WorkWithPlus.workwithplus_web.SdtWWPGridState(context);
         AV35GridStateFilterValue = new WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue(context);
         AV43FilterFullText = "";
         AV15TFSupplierGenCompanyName = "";
         AV16TFSupplierGenCompanyName_Sel = "";
         AV13TFSupplierGenTypeName = "";
         AV14TFSupplierGenTypeName_Sel = "";
         AV17TFSupplierGenContactName = "";
         AV18TFSupplierGenContactName_Sel = "";
         AV19TFSupplierGenContactPhone = "";
         AV20TFSupplierGenContactPhone_Sel = "";
         AV63TFSupplierGenDescription = "";
         AV64TFSupplierGenDescription_Sel = "";
         AV67Trn_suppliergenwwds_1_filterfulltext = "";
         AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname = "";
         AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel = "";
         AV70Trn_suppliergenwwds_4_tfsuppliergentypename = "";
         AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel = "";
         AV72Trn_suppliergenwwds_6_tfsuppliergencontactname = "";
         AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel = "";
         AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone = "";
         AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel = "";
         AV76Trn_suppliergenwwds_10_tfsuppliergendescription = "";
         AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel = "";
         lV67Trn_suppliergenwwds_1_filterfulltext = "";
         lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname = "";
         lV70Trn_suppliergenwwds_4_tfsuppliergentypename = "";
         lV72Trn_suppliergenwwds_6_tfsuppliergencontactname = "";
         lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone = "";
         lV76Trn_suppliergenwwds_10_tfsuppliergendescription = "";
         A44SupplierGenCompanyName = "";
         A254SupplierGenTypeName = "";
         A47SupplierGenContactName = "";
         A48SupplierGenContactPhone = "";
         A604SupplierGenDescription = "";
         A11OrganisationId = Guid.Empty;
         P00672_A253SupplierGenTypeId = new Guid[] {Guid.Empty} ;
         P00672_A602SG_LocationSupplierOrganisatio = new Guid[] {Guid.Empty} ;
         P00672_n602SG_LocationSupplierOrganisatio = new bool[] {false} ;
         P00672_A601SG_LocationSupplierLocationId = new Guid[] {Guid.Empty} ;
         P00672_n601SG_LocationSupplierLocationId = new bool[] {false} ;
         P00672_A584ActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P00672_n584ActiveAppVersionId = new bool[] {false} ;
         P00672_A598PublishedActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P00672_n598PublishedActiveAppVersionId = new bool[] {false} ;
         P00672_A44SupplierGenCompanyName = new string[] {""} ;
         P00672_A604SupplierGenDescription = new string[] {""} ;
         P00672_A48SupplierGenContactPhone = new string[] {""} ;
         P00672_A47SupplierGenContactName = new string[] {""} ;
         P00672_A254SupplierGenTypeName = new string[] {""} ;
         P00672_A42SupplierGenId = new Guid[] {Guid.Empty} ;
         A253SupplierGenTypeId = Guid.Empty;
         A602SG_LocationSupplierOrganisatio = Guid.Empty;
         A601SG_LocationSupplierLocationId = Guid.Empty;
         A584ActiveAppVersionId = Guid.Empty;
         A598PublishedActiveAppVersionId = Guid.Empty;
         A42SupplierGenId = Guid.Empty;
         P00673_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00673_n11OrganisationId = new bool[] {false} ;
         P00674_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00675_A523AppVersionId = new Guid[] {Guid.Empty} ;
         AV26Option = "";
         P00676_A602SG_LocationSupplierOrganisatio = new Guid[] {Guid.Empty} ;
         P00676_n602SG_LocationSupplierOrganisatio = new bool[] {false} ;
         P00676_A601SG_LocationSupplierLocationId = new Guid[] {Guid.Empty} ;
         P00676_n601SG_LocationSupplierLocationId = new bool[] {false} ;
         P00676_A584ActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P00676_n584ActiveAppVersionId = new bool[] {false} ;
         P00676_A598PublishedActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P00676_n598PublishedActiveAppVersionId = new bool[] {false} ;
         P00676_A253SupplierGenTypeId = new Guid[] {Guid.Empty} ;
         P00676_A604SupplierGenDescription = new string[] {""} ;
         P00676_A48SupplierGenContactPhone = new string[] {""} ;
         P00676_A47SupplierGenContactName = new string[] {""} ;
         P00676_A254SupplierGenTypeName = new string[] {""} ;
         P00676_A44SupplierGenCompanyName = new string[] {""} ;
         P00676_A42SupplierGenId = new Guid[] {Guid.Empty} ;
         P00677_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00677_n11OrganisationId = new bool[] {false} ;
         P00678_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00679_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P006710_A253SupplierGenTypeId = new Guid[] {Guid.Empty} ;
         P006710_A602SG_LocationSupplierOrganisatio = new Guid[] {Guid.Empty} ;
         P006710_n602SG_LocationSupplierOrganisatio = new bool[] {false} ;
         P006710_A601SG_LocationSupplierLocationId = new Guid[] {Guid.Empty} ;
         P006710_n601SG_LocationSupplierLocationId = new bool[] {false} ;
         P006710_A584ActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P006710_n584ActiveAppVersionId = new bool[] {false} ;
         P006710_A598PublishedActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P006710_n598PublishedActiveAppVersionId = new bool[] {false} ;
         P006710_A47SupplierGenContactName = new string[] {""} ;
         P006710_A604SupplierGenDescription = new string[] {""} ;
         P006710_A48SupplierGenContactPhone = new string[] {""} ;
         P006710_A254SupplierGenTypeName = new string[] {""} ;
         P006710_A44SupplierGenCompanyName = new string[] {""} ;
         P006710_A42SupplierGenId = new Guid[] {Guid.Empty} ;
         P006711_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P006711_n11OrganisationId = new bool[] {false} ;
         P006712_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P006713_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P006714_A253SupplierGenTypeId = new Guid[] {Guid.Empty} ;
         P006714_A602SG_LocationSupplierOrganisatio = new Guid[] {Guid.Empty} ;
         P006714_n602SG_LocationSupplierOrganisatio = new bool[] {false} ;
         P006714_A601SG_LocationSupplierLocationId = new Guid[] {Guid.Empty} ;
         P006714_n601SG_LocationSupplierLocationId = new bool[] {false} ;
         P006714_A584ActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P006714_n584ActiveAppVersionId = new bool[] {false} ;
         P006714_A598PublishedActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P006714_n598PublishedActiveAppVersionId = new bool[] {false} ;
         P006714_A48SupplierGenContactPhone = new string[] {""} ;
         P006714_A604SupplierGenDescription = new string[] {""} ;
         P006714_A47SupplierGenContactName = new string[] {""} ;
         P006714_A254SupplierGenTypeName = new string[] {""} ;
         P006714_A44SupplierGenCompanyName = new string[] {""} ;
         P006714_A42SupplierGenId = new Guid[] {Guid.Empty} ;
         P006715_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P006715_n11OrganisationId = new bool[] {false} ;
         P006716_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P006717_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P006718_A253SupplierGenTypeId = new Guid[] {Guid.Empty} ;
         P006718_A602SG_LocationSupplierOrganisatio = new Guid[] {Guid.Empty} ;
         P006718_n602SG_LocationSupplierOrganisatio = new bool[] {false} ;
         P006718_A601SG_LocationSupplierLocationId = new Guid[] {Guid.Empty} ;
         P006718_n601SG_LocationSupplierLocationId = new bool[] {false} ;
         P006718_A584ActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P006718_n584ActiveAppVersionId = new bool[] {false} ;
         P006718_A598PublishedActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P006718_n598PublishedActiveAppVersionId = new bool[] {false} ;
         P006718_A604SupplierGenDescription = new string[] {""} ;
         P006718_A48SupplierGenContactPhone = new string[] {""} ;
         P006718_A47SupplierGenContactName = new string[] {""} ;
         P006718_A254SupplierGenTypeName = new string[] {""} ;
         P006718_A44SupplierGenCompanyName = new string[] {""} ;
         P006718_A42SupplierGenId = new Guid[] {Guid.Empty} ;
         P006719_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P006719_n11OrganisationId = new bool[] {false} ;
         P006720_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P006721_A523AppVersionId = new Guid[] {Guid.Empty} ;
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.trn_suppliergenwwgetfilterdata__default(),
            new Object[][] {
                new Object[] {
               P00672_A253SupplierGenTypeId, P00672_A602SG_LocationSupplierOrganisatio, P00672_n602SG_LocationSupplierOrganisatio, P00672_A601SG_LocationSupplierLocationId, P00672_n601SG_LocationSupplierLocationId, P00672_A584ActiveAppVersionId, P00672_n584ActiveAppVersionId, P00672_A598PublishedActiveAppVersionId, P00672_n598PublishedActiveAppVersionId, P00672_A44SupplierGenCompanyName,
               P00672_A604SupplierGenDescription, P00672_A48SupplierGenContactPhone, P00672_A47SupplierGenContactName, P00672_A254SupplierGenTypeName, P00672_A42SupplierGenId
               }
               , new Object[] {
               P00673_A11OrganisationId
               }
               , new Object[] {
               P00674_A523AppVersionId
               }
               , new Object[] {
               P00675_A523AppVersionId
               }
               , new Object[] {
               P00676_A602SG_LocationSupplierOrganisatio, P00676_n602SG_LocationSupplierOrganisatio, P00676_A601SG_LocationSupplierLocationId, P00676_n601SG_LocationSupplierLocationId, P00676_A584ActiveAppVersionId, P00676_n584ActiveAppVersionId, P00676_A598PublishedActiveAppVersionId, P00676_n598PublishedActiveAppVersionId, P00676_A253SupplierGenTypeId, P00676_A604SupplierGenDescription,
               P00676_A48SupplierGenContactPhone, P00676_A47SupplierGenContactName, P00676_A254SupplierGenTypeName, P00676_A44SupplierGenCompanyName, P00676_A42SupplierGenId
               }
               , new Object[] {
               P00677_A11OrganisationId
               }
               , new Object[] {
               P00678_A523AppVersionId
               }
               , new Object[] {
               P00679_A523AppVersionId
               }
               , new Object[] {
               P006710_A253SupplierGenTypeId, P006710_A602SG_LocationSupplierOrganisatio, P006710_n602SG_LocationSupplierOrganisatio, P006710_A601SG_LocationSupplierLocationId, P006710_n601SG_LocationSupplierLocationId, P006710_A584ActiveAppVersionId, P006710_n584ActiveAppVersionId, P006710_A598PublishedActiveAppVersionId, P006710_n598PublishedActiveAppVersionId, P006710_A47SupplierGenContactName,
               P006710_A604SupplierGenDescription, P006710_A48SupplierGenContactPhone, P006710_A254SupplierGenTypeName, P006710_A44SupplierGenCompanyName, P006710_A42SupplierGenId
               }
               , new Object[] {
               P006711_A11OrganisationId
               }
               , new Object[] {
               P006712_A523AppVersionId
               }
               , new Object[] {
               P006713_A523AppVersionId
               }
               , new Object[] {
               P006714_A253SupplierGenTypeId, P006714_A602SG_LocationSupplierOrganisatio, P006714_n602SG_LocationSupplierOrganisatio, P006714_A601SG_LocationSupplierLocationId, P006714_n601SG_LocationSupplierLocationId, P006714_A584ActiveAppVersionId, P006714_n584ActiveAppVersionId, P006714_A598PublishedActiveAppVersionId, P006714_n598PublishedActiveAppVersionId, P006714_A48SupplierGenContactPhone,
               P006714_A604SupplierGenDescription, P006714_A47SupplierGenContactName, P006714_A254SupplierGenTypeName, P006714_A44SupplierGenCompanyName, P006714_A42SupplierGenId
               }
               , new Object[] {
               P006715_A11OrganisationId
               }
               , new Object[] {
               P006716_A523AppVersionId
               }
               , new Object[] {
               P006717_A523AppVersionId
               }
               , new Object[] {
               P006718_A253SupplierGenTypeId, P006718_A602SG_LocationSupplierOrganisatio, P006718_n602SG_LocationSupplierOrganisatio, P006718_A601SG_LocationSupplierLocationId, P006718_n601SG_LocationSupplierLocationId, P006718_A584ActiveAppVersionId, P006718_n584ActiveAppVersionId, P006718_A598PublishedActiveAppVersionId, P006718_n598PublishedActiveAppVersionId, P006718_A604SupplierGenDescription,
               P006718_A48SupplierGenContactPhone, P006718_A47SupplierGenContactName, P006718_A254SupplierGenTypeName, P006718_A44SupplierGenCompanyName, P006718_A42SupplierGenId
               }
               , new Object[] {
               P006719_A11OrganisationId
               }
               , new Object[] {
               P006720_A523AppVersionId
               }
               , new Object[] {
               P006721_A523AppVersionId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV24MaxItems ;
      private short AV23PageIndex ;
      private short AV22SkipItems ;
      private int AV65GXV1 ;
      private int AV25InsertIndex ;
      private long AV31count ;
      private string AV19TFSupplierGenContactPhone ;
      private string AV20TFSupplierGenContactPhone_Sel ;
      private string AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone ;
      private string AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel ;
      private string lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone ;
      private string A48SupplierGenContactPhone ;
      private bool returnInSub ;
      private bool BRK672 ;
      private bool n602SG_LocationSupplierOrganisatio ;
      private bool n601SG_LocationSupplierLocationId ;
      private bool n584ActiveAppVersionId ;
      private bool n598PublishedActiveAppVersionId ;
      private bool BRK674 ;
      private bool BRK676 ;
      private bool BRK678 ;
      private bool BRK6710 ;
      private string AV40OptionsJson ;
      private string AV41OptionsDescJson ;
      private string AV42OptionIndexesJson ;
      private string A604SupplierGenDescription ;
      private string AV37DDOName ;
      private string AV38SearchTxtParms ;
      private string AV39SearchTxtTo ;
      private string AV21SearchTxt ;
      private string AV43FilterFullText ;
      private string AV15TFSupplierGenCompanyName ;
      private string AV16TFSupplierGenCompanyName_Sel ;
      private string AV13TFSupplierGenTypeName ;
      private string AV14TFSupplierGenTypeName_Sel ;
      private string AV17TFSupplierGenContactName ;
      private string AV18TFSupplierGenContactName_Sel ;
      private string AV63TFSupplierGenDescription ;
      private string AV64TFSupplierGenDescription_Sel ;
      private string AV67Trn_suppliergenwwds_1_filterfulltext ;
      private string AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname ;
      private string AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel ;
      private string AV70Trn_suppliergenwwds_4_tfsuppliergentypename ;
      private string AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel ;
      private string AV72Trn_suppliergenwwds_6_tfsuppliergencontactname ;
      private string AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel ;
      private string AV76Trn_suppliergenwwds_10_tfsuppliergendescription ;
      private string AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel ;
      private string lV67Trn_suppliergenwwds_1_filterfulltext ;
      private string lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname ;
      private string lV70Trn_suppliergenwwds_4_tfsuppliergentypename ;
      private string lV72Trn_suppliergenwwds_6_tfsuppliergencontactname ;
      private string lV76Trn_suppliergenwwds_10_tfsuppliergendescription ;
      private string A44SupplierGenCompanyName ;
      private string A254SupplierGenTypeName ;
      private string A47SupplierGenContactName ;
      private string AV26Option ;
      private Guid A11OrganisationId ;
      private Guid A253SupplierGenTypeId ;
      private Guid A602SG_LocationSupplierOrganisatio ;
      private Guid A601SG_LocationSupplierLocationId ;
      private Guid A584ActiveAppVersionId ;
      private Guid A598PublishedActiveAppVersionId ;
      private Guid A42SupplierGenId ;
      private IGxSession AV32Session ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GxSimpleCollection<string> AV27Options ;
      private GxSimpleCollection<string> AV29OptionsDesc ;
      private GxSimpleCollection<string> AV30OptionIndexes ;
      private GeneXus.Programs.wwpbaseobjects.SdtWWPContext AV9WWPContext ;
      private WorkWithPlus.workwithplus_web.SdtWWPGridState AV34GridState ;
      private WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue AV35GridStateFilterValue ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00672_A253SupplierGenTypeId ;
      private Guid[] P00672_A602SG_LocationSupplierOrganisatio ;
      private bool[] P00672_n602SG_LocationSupplierOrganisatio ;
      private Guid[] P00672_A601SG_LocationSupplierLocationId ;
      private bool[] P00672_n601SG_LocationSupplierLocationId ;
      private Guid[] P00672_A584ActiveAppVersionId ;
      private bool[] P00672_n584ActiveAppVersionId ;
      private Guid[] P00672_A598PublishedActiveAppVersionId ;
      private bool[] P00672_n598PublishedActiveAppVersionId ;
      private string[] P00672_A44SupplierGenCompanyName ;
      private string[] P00672_A604SupplierGenDescription ;
      private string[] P00672_A48SupplierGenContactPhone ;
      private string[] P00672_A47SupplierGenContactName ;
      private string[] P00672_A254SupplierGenTypeName ;
      private Guid[] P00672_A42SupplierGenId ;
      private Guid[] P00673_A11OrganisationId ;
      private bool[] P00673_n11OrganisationId ;
      private Guid[] P00674_A523AppVersionId ;
      private Guid[] P00675_A523AppVersionId ;
      private Guid[] P00676_A602SG_LocationSupplierOrganisatio ;
      private bool[] P00676_n602SG_LocationSupplierOrganisatio ;
      private Guid[] P00676_A601SG_LocationSupplierLocationId ;
      private bool[] P00676_n601SG_LocationSupplierLocationId ;
      private Guid[] P00676_A584ActiveAppVersionId ;
      private bool[] P00676_n584ActiveAppVersionId ;
      private Guid[] P00676_A598PublishedActiveAppVersionId ;
      private bool[] P00676_n598PublishedActiveAppVersionId ;
      private Guid[] P00676_A253SupplierGenTypeId ;
      private string[] P00676_A604SupplierGenDescription ;
      private string[] P00676_A48SupplierGenContactPhone ;
      private string[] P00676_A47SupplierGenContactName ;
      private string[] P00676_A254SupplierGenTypeName ;
      private string[] P00676_A44SupplierGenCompanyName ;
      private Guid[] P00676_A42SupplierGenId ;
      private Guid[] P00677_A11OrganisationId ;
      private bool[] P00677_n11OrganisationId ;
      private Guid[] P00678_A523AppVersionId ;
      private Guid[] P00679_A523AppVersionId ;
      private Guid[] P006710_A253SupplierGenTypeId ;
      private Guid[] P006710_A602SG_LocationSupplierOrganisatio ;
      private bool[] P006710_n602SG_LocationSupplierOrganisatio ;
      private Guid[] P006710_A601SG_LocationSupplierLocationId ;
      private bool[] P006710_n601SG_LocationSupplierLocationId ;
      private Guid[] P006710_A584ActiveAppVersionId ;
      private bool[] P006710_n584ActiveAppVersionId ;
      private Guid[] P006710_A598PublishedActiveAppVersionId ;
      private bool[] P006710_n598PublishedActiveAppVersionId ;
      private string[] P006710_A47SupplierGenContactName ;
      private string[] P006710_A604SupplierGenDescription ;
      private string[] P006710_A48SupplierGenContactPhone ;
      private string[] P006710_A254SupplierGenTypeName ;
      private string[] P006710_A44SupplierGenCompanyName ;
      private Guid[] P006710_A42SupplierGenId ;
      private Guid[] P006711_A11OrganisationId ;
      private bool[] P006711_n11OrganisationId ;
      private Guid[] P006712_A523AppVersionId ;
      private Guid[] P006713_A523AppVersionId ;
      private Guid[] P006714_A253SupplierGenTypeId ;
      private Guid[] P006714_A602SG_LocationSupplierOrganisatio ;
      private bool[] P006714_n602SG_LocationSupplierOrganisatio ;
      private Guid[] P006714_A601SG_LocationSupplierLocationId ;
      private bool[] P006714_n601SG_LocationSupplierLocationId ;
      private Guid[] P006714_A584ActiveAppVersionId ;
      private bool[] P006714_n584ActiveAppVersionId ;
      private Guid[] P006714_A598PublishedActiveAppVersionId ;
      private bool[] P006714_n598PublishedActiveAppVersionId ;
      private string[] P006714_A48SupplierGenContactPhone ;
      private string[] P006714_A604SupplierGenDescription ;
      private string[] P006714_A47SupplierGenContactName ;
      private string[] P006714_A254SupplierGenTypeName ;
      private string[] P006714_A44SupplierGenCompanyName ;
      private Guid[] P006714_A42SupplierGenId ;
      private Guid[] P006715_A11OrganisationId ;
      private bool[] P006715_n11OrganisationId ;
      private Guid[] P006716_A523AppVersionId ;
      private Guid[] P006717_A523AppVersionId ;
      private Guid[] P006718_A253SupplierGenTypeId ;
      private Guid[] P006718_A602SG_LocationSupplierOrganisatio ;
      private bool[] P006718_n602SG_LocationSupplierOrganisatio ;
      private Guid[] P006718_A601SG_LocationSupplierLocationId ;
      private bool[] P006718_n601SG_LocationSupplierLocationId ;
      private Guid[] P006718_A584ActiveAppVersionId ;
      private bool[] P006718_n584ActiveAppVersionId ;
      private Guid[] P006718_A598PublishedActiveAppVersionId ;
      private bool[] P006718_n598PublishedActiveAppVersionId ;
      private string[] P006718_A604SupplierGenDescription ;
      private string[] P006718_A48SupplierGenContactPhone ;
      private string[] P006718_A47SupplierGenContactName ;
      private string[] P006718_A254SupplierGenTypeName ;
      private string[] P006718_A44SupplierGenCompanyName ;
      private Guid[] P006718_A42SupplierGenId ;
      private Guid[] P006719_A11OrganisationId ;
      private bool[] P006719_n11OrganisationId ;
      private Guid[] P006720_A523AppVersionId ;
      private Guid[] P006721_A523AppVersionId ;
      private string aP3_OptionsJson ;
      private string aP4_OptionsDescJson ;
      private string aP5_OptionIndexesJson ;
   }

   public class trn_suppliergenwwgetfilterdata__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P00672( IGxContext context ,
                                             string AV67Trn_suppliergenwwds_1_filterfulltext ,
                                             string AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel ,
                                             string AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname ,
                                             string AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel ,
                                             string AV70Trn_suppliergenwwds_4_tfsuppliergentypename ,
                                             string AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel ,
                                             string AV72Trn_suppliergenwwds_6_tfsuppliergencontactname ,
                                             string AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel ,
                                             string AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone ,
                                             string AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel ,
                                             string AV76Trn_suppliergenwwds_10_tfsuppliergendescription ,
                                             string A44SupplierGenCompanyName ,
                                             string A254SupplierGenTypeName ,
                                             string A47SupplierGenContactName ,
                                             string A48SupplierGenContactPhone ,
                                             string A604SupplierGenDescription ,
                                             Guid A11OrganisationId )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int1 = new short[15];
         Object[] GXv_Object2 = new Object[2];
         scmdbuf = "SELECT T1.SupplierGenTypeId, T1.SG_LocationSupplierOrganisatio AS SG_LocationSupplierOrganisatio, T1.SG_LocationSupplierLocationId AS SG_LocationSupplierLocationId, T3.ActiveAppVersionId, T3.PublishedActiveAppVersionId, T1.SupplierGenCompanyName, T1.SupplierGenDescription, T1.SupplierGenContactPhone, T1.SupplierGenContactName, T2.SupplierGenTypeName, T1.SupplierGenId FROM ((Trn_SupplierGen T1 INNER JOIN Trn_SupplierGenType T2 ON T2.SupplierGenTypeId = T1.SupplierGenTypeId) LEFT JOIN Trn_Location T3 ON T3.LocationId = T1.SG_LocationSupplierLocationId AND T3.OrganisationId = T1.SG_LocationSupplierOrganisatio)";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( LOWER(T1.SupplierGenCompanyName) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T2.SupplierGenTypeName) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T1.SupplierGenContactName) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T1.SupplierGenContactPhone) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T1.SupplierGenDescription) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int1[0] = 1;
            GXv_int1[1] = 1;
            GXv_int1[2] = 1;
            GXv_int1[3] = 1;
            GXv_int1[4] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenCompanyName like :lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname)");
         }
         else
         {
            GXv_int1[5] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel)) && ! ( StringUtil.StrCmp(AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenCompanyName = ( :AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel))");
         }
         else
         {
            GXv_int1[6] = 1;
         }
         if ( StringUtil.StrCmp(AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenCompanyName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV70Trn_suppliergenwwds_4_tfsuppliergentypename)) ) )
         {
            AddWhere(sWhereString, "(T2.SupplierGenTypeName like :lV70Trn_suppliergenwwds_4_tfsuppliergentypename)");
         }
         else
         {
            GXv_int1[7] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel)) && ! ( StringUtil.StrCmp(AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T2.SupplierGenTypeName = ( :AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel))");
         }
         else
         {
            GXv_int1[8] = 1;
         }
         if ( StringUtil.StrCmp(AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T2.SupplierGenTypeName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV72Trn_suppliergenwwds_6_tfsuppliergencontactname)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactName like :lV72Trn_suppliergenwwds_6_tfsuppliergencontactname)");
         }
         else
         {
            GXv_int1[9] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel)) && ! ( StringUtil.StrCmp(AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactName = ( :AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel))");
         }
         else
         {
            GXv_int1[10] = 1;
         }
         if ( StringUtil.StrCmp(AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenContactName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactPhone like :lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone)");
         }
         else
         {
            GXv_int1[11] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel)) && ! ( StringUtil.StrCmp(AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactPhone = ( :AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel))");
         }
         else
         {
            GXv_int1[12] = 1;
         }
         if ( StringUtil.StrCmp(AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenContactPhone))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV76Trn_suppliergenwwds_10_tfsuppliergendescription)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenDescription like :lV76Trn_suppliergenwwds_10_tfsuppliergendescription)");
         }
         else
         {
            GXv_int1[13] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel)) && ! ( StringUtil.StrCmp(AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenDescription = ( :AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel))");
         }
         else
         {
            GXv_int1[14] = 1;
         }
         if ( StringUtil.StrCmp(AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenDescription))=0))");
         }
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY T1.SupplierGenCompanyName";
         GXv_Object2[0] = scmdbuf;
         GXv_Object2[1] = GXv_int1;
         return GXv_Object2 ;
      }

      protected Object[] conditional_P00676( IGxContext context ,
                                             string AV67Trn_suppliergenwwds_1_filterfulltext ,
                                             string AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel ,
                                             string AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname ,
                                             string AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel ,
                                             string AV70Trn_suppliergenwwds_4_tfsuppliergentypename ,
                                             string AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel ,
                                             string AV72Trn_suppliergenwwds_6_tfsuppliergencontactname ,
                                             string AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel ,
                                             string AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone ,
                                             string AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel ,
                                             string AV76Trn_suppliergenwwds_10_tfsuppliergendescription ,
                                             string A44SupplierGenCompanyName ,
                                             string A254SupplierGenTypeName ,
                                             string A47SupplierGenContactName ,
                                             string A48SupplierGenContactPhone ,
                                             string A604SupplierGenDescription ,
                                             Guid A11OrganisationId )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int3 = new short[15];
         Object[] GXv_Object4 = new Object[2];
         scmdbuf = "SELECT T1.SG_LocationSupplierOrganisatio AS SG_LocationSupplierOrganisatio, T1.SG_LocationSupplierLocationId AS SG_LocationSupplierLocationId, T2.ActiveAppVersionId, T2.PublishedActiveAppVersionId, T1.SupplierGenTypeId, T1.SupplierGenDescription, T1.SupplierGenContactPhone, T1.SupplierGenContactName, T3.SupplierGenTypeName, T1.SupplierGenCompanyName, T1.SupplierGenId FROM ((Trn_SupplierGen T1 LEFT JOIN Trn_Location T2 ON T2.LocationId = T1.SG_LocationSupplierLocationId AND T2.OrganisationId = T1.SG_LocationSupplierOrganisatio) INNER JOIN Trn_SupplierGenType T3 ON T3.SupplierGenTypeId = T1.SupplierGenTypeId)";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( LOWER(T1.SupplierGenCompanyName) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T3.SupplierGenTypeName) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T1.SupplierGenContactName) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T1.SupplierGenContactPhone) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T1.SupplierGenDescription) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int3[0] = 1;
            GXv_int3[1] = 1;
            GXv_int3[2] = 1;
            GXv_int3[3] = 1;
            GXv_int3[4] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenCompanyName like :lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname)");
         }
         else
         {
            GXv_int3[5] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel)) && ! ( StringUtil.StrCmp(AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenCompanyName = ( :AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel))");
         }
         else
         {
            GXv_int3[6] = 1;
         }
         if ( StringUtil.StrCmp(AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenCompanyName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV70Trn_suppliergenwwds_4_tfsuppliergentypename)) ) )
         {
            AddWhere(sWhereString, "(T3.SupplierGenTypeName like :lV70Trn_suppliergenwwds_4_tfsuppliergentypename)");
         }
         else
         {
            GXv_int3[7] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel)) && ! ( StringUtil.StrCmp(AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T3.SupplierGenTypeName = ( :AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel))");
         }
         else
         {
            GXv_int3[8] = 1;
         }
         if ( StringUtil.StrCmp(AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T3.SupplierGenTypeName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV72Trn_suppliergenwwds_6_tfsuppliergencontactname)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactName like :lV72Trn_suppliergenwwds_6_tfsuppliergencontactname)");
         }
         else
         {
            GXv_int3[9] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel)) && ! ( StringUtil.StrCmp(AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactName = ( :AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel))");
         }
         else
         {
            GXv_int3[10] = 1;
         }
         if ( StringUtil.StrCmp(AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenContactName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactPhone like :lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone)");
         }
         else
         {
            GXv_int3[11] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel)) && ! ( StringUtil.StrCmp(AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactPhone = ( :AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel))");
         }
         else
         {
            GXv_int3[12] = 1;
         }
         if ( StringUtil.StrCmp(AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenContactPhone))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV76Trn_suppliergenwwds_10_tfsuppliergendescription)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenDescription like :lV76Trn_suppliergenwwds_10_tfsuppliergendescription)");
         }
         else
         {
            GXv_int3[13] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel)) && ! ( StringUtil.StrCmp(AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenDescription = ( :AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel))");
         }
         else
         {
            GXv_int3[14] = 1;
         }
         if ( StringUtil.StrCmp(AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenDescription))=0))");
         }
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY T1.SupplierGenTypeId";
         GXv_Object4[0] = scmdbuf;
         GXv_Object4[1] = GXv_int3;
         return GXv_Object4 ;
      }

      protected Object[] conditional_P006710( IGxContext context ,
                                              string AV67Trn_suppliergenwwds_1_filterfulltext ,
                                              string AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel ,
                                              string AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname ,
                                              string AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel ,
                                              string AV70Trn_suppliergenwwds_4_tfsuppliergentypename ,
                                              string AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel ,
                                              string AV72Trn_suppliergenwwds_6_tfsuppliergencontactname ,
                                              string AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel ,
                                              string AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone ,
                                              string AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel ,
                                              string AV76Trn_suppliergenwwds_10_tfsuppliergendescription ,
                                              string A44SupplierGenCompanyName ,
                                              string A254SupplierGenTypeName ,
                                              string A47SupplierGenContactName ,
                                              string A48SupplierGenContactPhone ,
                                              string A604SupplierGenDescription ,
                                              Guid A11OrganisationId )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int5 = new short[15];
         Object[] GXv_Object6 = new Object[2];
         scmdbuf = "SELECT T1.SupplierGenTypeId, T1.SG_LocationSupplierOrganisatio AS SG_LocationSupplierOrganisatio, T1.SG_LocationSupplierLocationId AS SG_LocationSupplierLocationId, T3.ActiveAppVersionId, T3.PublishedActiveAppVersionId, T1.SupplierGenContactName, T1.SupplierGenDescription, T1.SupplierGenContactPhone, T2.SupplierGenTypeName, T1.SupplierGenCompanyName, T1.SupplierGenId FROM ((Trn_SupplierGen T1 INNER JOIN Trn_SupplierGenType T2 ON T2.SupplierGenTypeId = T1.SupplierGenTypeId) LEFT JOIN Trn_Location T3 ON T3.LocationId = T1.SG_LocationSupplierLocationId AND T3.OrganisationId = T1.SG_LocationSupplierOrganisatio)";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( LOWER(T1.SupplierGenCompanyName) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T2.SupplierGenTypeName) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T1.SupplierGenContactName) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T1.SupplierGenContactPhone) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T1.SupplierGenDescription) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int5[0] = 1;
            GXv_int5[1] = 1;
            GXv_int5[2] = 1;
            GXv_int5[3] = 1;
            GXv_int5[4] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenCompanyName like :lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname)");
         }
         else
         {
            GXv_int5[5] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel)) && ! ( StringUtil.StrCmp(AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenCompanyName = ( :AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel))");
         }
         else
         {
            GXv_int5[6] = 1;
         }
         if ( StringUtil.StrCmp(AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenCompanyName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV70Trn_suppliergenwwds_4_tfsuppliergentypename)) ) )
         {
            AddWhere(sWhereString, "(T2.SupplierGenTypeName like :lV70Trn_suppliergenwwds_4_tfsuppliergentypename)");
         }
         else
         {
            GXv_int5[7] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel)) && ! ( StringUtil.StrCmp(AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T2.SupplierGenTypeName = ( :AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel))");
         }
         else
         {
            GXv_int5[8] = 1;
         }
         if ( StringUtil.StrCmp(AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T2.SupplierGenTypeName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV72Trn_suppliergenwwds_6_tfsuppliergencontactname)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactName like :lV72Trn_suppliergenwwds_6_tfsuppliergencontactname)");
         }
         else
         {
            GXv_int5[9] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel)) && ! ( StringUtil.StrCmp(AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactName = ( :AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel))");
         }
         else
         {
            GXv_int5[10] = 1;
         }
         if ( StringUtil.StrCmp(AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenContactName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactPhone like :lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone)");
         }
         else
         {
            GXv_int5[11] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel)) && ! ( StringUtil.StrCmp(AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactPhone = ( :AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel))");
         }
         else
         {
            GXv_int5[12] = 1;
         }
         if ( StringUtil.StrCmp(AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenContactPhone))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV76Trn_suppliergenwwds_10_tfsuppliergendescription)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenDescription like :lV76Trn_suppliergenwwds_10_tfsuppliergendescription)");
         }
         else
         {
            GXv_int5[13] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel)) && ! ( StringUtil.StrCmp(AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenDescription = ( :AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel))");
         }
         else
         {
            GXv_int5[14] = 1;
         }
         if ( StringUtil.StrCmp(AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenDescription))=0))");
         }
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY T1.SupplierGenContactName";
         GXv_Object6[0] = scmdbuf;
         GXv_Object6[1] = GXv_int5;
         return GXv_Object6 ;
      }

      protected Object[] conditional_P006714( IGxContext context ,
                                              string AV67Trn_suppliergenwwds_1_filterfulltext ,
                                              string AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel ,
                                              string AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname ,
                                              string AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel ,
                                              string AV70Trn_suppliergenwwds_4_tfsuppliergentypename ,
                                              string AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel ,
                                              string AV72Trn_suppliergenwwds_6_tfsuppliergencontactname ,
                                              string AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel ,
                                              string AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone ,
                                              string AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel ,
                                              string AV76Trn_suppliergenwwds_10_tfsuppliergendescription ,
                                              string A44SupplierGenCompanyName ,
                                              string A254SupplierGenTypeName ,
                                              string A47SupplierGenContactName ,
                                              string A48SupplierGenContactPhone ,
                                              string A604SupplierGenDescription ,
                                              Guid A11OrganisationId )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int7 = new short[15];
         Object[] GXv_Object8 = new Object[2];
         scmdbuf = "SELECT T1.SupplierGenTypeId, T1.SG_LocationSupplierOrganisatio AS SG_LocationSupplierOrganisatio, T1.SG_LocationSupplierLocationId AS SG_LocationSupplierLocationId, T3.ActiveAppVersionId, T3.PublishedActiveAppVersionId, T1.SupplierGenContactPhone, T1.SupplierGenDescription, T1.SupplierGenContactName, T2.SupplierGenTypeName, T1.SupplierGenCompanyName, T1.SupplierGenId FROM ((Trn_SupplierGen T1 INNER JOIN Trn_SupplierGenType T2 ON T2.SupplierGenTypeId = T1.SupplierGenTypeId) LEFT JOIN Trn_Location T3 ON T3.LocationId = T1.SG_LocationSupplierLocationId AND T3.OrganisationId = T1.SG_LocationSupplierOrganisatio)";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( LOWER(T1.SupplierGenCompanyName) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T2.SupplierGenTypeName) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T1.SupplierGenContactName) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T1.SupplierGenContactPhone) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T1.SupplierGenDescription) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int7[0] = 1;
            GXv_int7[1] = 1;
            GXv_int7[2] = 1;
            GXv_int7[3] = 1;
            GXv_int7[4] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenCompanyName like :lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname)");
         }
         else
         {
            GXv_int7[5] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel)) && ! ( StringUtil.StrCmp(AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenCompanyName = ( :AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel))");
         }
         else
         {
            GXv_int7[6] = 1;
         }
         if ( StringUtil.StrCmp(AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenCompanyName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV70Trn_suppliergenwwds_4_tfsuppliergentypename)) ) )
         {
            AddWhere(sWhereString, "(T2.SupplierGenTypeName like :lV70Trn_suppliergenwwds_4_tfsuppliergentypename)");
         }
         else
         {
            GXv_int7[7] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel)) && ! ( StringUtil.StrCmp(AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T2.SupplierGenTypeName = ( :AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel))");
         }
         else
         {
            GXv_int7[8] = 1;
         }
         if ( StringUtil.StrCmp(AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T2.SupplierGenTypeName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV72Trn_suppliergenwwds_6_tfsuppliergencontactname)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactName like :lV72Trn_suppliergenwwds_6_tfsuppliergencontactname)");
         }
         else
         {
            GXv_int7[9] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel)) && ! ( StringUtil.StrCmp(AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactName = ( :AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel))");
         }
         else
         {
            GXv_int7[10] = 1;
         }
         if ( StringUtil.StrCmp(AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenContactName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactPhone like :lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone)");
         }
         else
         {
            GXv_int7[11] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel)) && ! ( StringUtil.StrCmp(AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactPhone = ( :AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel))");
         }
         else
         {
            GXv_int7[12] = 1;
         }
         if ( StringUtil.StrCmp(AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenContactPhone))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV76Trn_suppliergenwwds_10_tfsuppliergendescription)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenDescription like :lV76Trn_suppliergenwwds_10_tfsuppliergendescription)");
         }
         else
         {
            GXv_int7[13] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel)) && ! ( StringUtil.StrCmp(AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenDescription = ( :AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel))");
         }
         else
         {
            GXv_int7[14] = 1;
         }
         if ( StringUtil.StrCmp(AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenDescription))=0))");
         }
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY T1.SupplierGenContactPhone";
         GXv_Object8[0] = scmdbuf;
         GXv_Object8[1] = GXv_int7;
         return GXv_Object8 ;
      }

      protected Object[] conditional_P006718( IGxContext context ,
                                              string AV67Trn_suppliergenwwds_1_filterfulltext ,
                                              string AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel ,
                                              string AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname ,
                                              string AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel ,
                                              string AV70Trn_suppliergenwwds_4_tfsuppliergentypename ,
                                              string AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel ,
                                              string AV72Trn_suppliergenwwds_6_tfsuppliergencontactname ,
                                              string AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel ,
                                              string AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone ,
                                              string AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel ,
                                              string AV76Trn_suppliergenwwds_10_tfsuppliergendescription ,
                                              string A44SupplierGenCompanyName ,
                                              string A254SupplierGenTypeName ,
                                              string A47SupplierGenContactName ,
                                              string A48SupplierGenContactPhone ,
                                              string A604SupplierGenDescription ,
                                              Guid A11OrganisationId )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int9 = new short[15];
         Object[] GXv_Object10 = new Object[2];
         scmdbuf = "SELECT T1.SupplierGenTypeId, T1.SG_LocationSupplierOrganisatio AS SG_LocationSupplierOrganisatio, T1.SG_LocationSupplierLocationId AS SG_LocationSupplierLocationId, T3.ActiveAppVersionId, T3.PublishedActiveAppVersionId, T1.SupplierGenDescription, T1.SupplierGenContactPhone, T1.SupplierGenContactName, T2.SupplierGenTypeName, T1.SupplierGenCompanyName, T1.SupplierGenId FROM ((Trn_SupplierGen T1 INNER JOIN Trn_SupplierGenType T2 ON T2.SupplierGenTypeId = T1.SupplierGenTypeId) LEFT JOIN Trn_Location T3 ON T3.LocationId = T1.SG_LocationSupplierLocationId AND T3.OrganisationId = T1.SG_LocationSupplierOrganisatio)";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV67Trn_suppliergenwwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( LOWER(T1.SupplierGenCompanyName) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T2.SupplierGenTypeName) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T1.SupplierGenContactName) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T1.SupplierGenContactPhone) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)) or ( LOWER(T1.SupplierGenDescription) like '%' || LOWER(:lV67Trn_suppliergenwwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int9[0] = 1;
            GXv_int9[1] = 1;
            GXv_int9[2] = 1;
            GXv_int9[3] = 1;
            GXv_int9[4] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV68Trn_suppliergenwwds_2_tfsuppliergencompanyname)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenCompanyName like :lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname)");
         }
         else
         {
            GXv_int9[5] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel)) && ! ( StringUtil.StrCmp(AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenCompanyName = ( :AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel))");
         }
         else
         {
            GXv_int9[6] = 1;
         }
         if ( StringUtil.StrCmp(AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenCompanyName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV70Trn_suppliergenwwds_4_tfsuppliergentypename)) ) )
         {
            AddWhere(sWhereString, "(T2.SupplierGenTypeName like :lV70Trn_suppliergenwwds_4_tfsuppliergentypename)");
         }
         else
         {
            GXv_int9[7] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel)) && ! ( StringUtil.StrCmp(AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T2.SupplierGenTypeName = ( :AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel))");
         }
         else
         {
            GXv_int9[8] = 1;
         }
         if ( StringUtil.StrCmp(AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T2.SupplierGenTypeName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV72Trn_suppliergenwwds_6_tfsuppliergencontactname)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactName like :lV72Trn_suppliergenwwds_6_tfsuppliergencontactname)");
         }
         else
         {
            GXv_int9[9] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel)) && ! ( StringUtil.StrCmp(AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactName = ( :AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel))");
         }
         else
         {
            GXv_int9[10] = 1;
         }
         if ( StringUtil.StrCmp(AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenContactName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV74Trn_suppliergenwwds_8_tfsuppliergencontactphone)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactPhone like :lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone)");
         }
         else
         {
            GXv_int9[11] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel)) && ! ( StringUtil.StrCmp(AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenContactPhone = ( :AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel))");
         }
         else
         {
            GXv_int9[12] = 1;
         }
         if ( StringUtil.StrCmp(AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenContactPhone))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV76Trn_suppliergenwwds_10_tfsuppliergendescription)) ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenDescription like :lV76Trn_suppliergenwwds_10_tfsuppliergendescription)");
         }
         else
         {
            GXv_int9[13] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel)) && ! ( StringUtil.StrCmp(AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenDescription = ( :AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel))");
         }
         else
         {
            GXv_int9[14] = 1;
         }
         if ( StringUtil.StrCmp(AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.SupplierGenDescription))=0))");
         }
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY T1.SupplierGenDescription";
         GXv_Object10[0] = scmdbuf;
         GXv_Object10[1] = GXv_int9;
         return GXv_Object10 ;
      }

      public override Object [] getDynamicStatement( int cursor ,
                                                     IGxContext context ,
                                                     Object [] dynConstraints )
      {
         switch ( cursor )
         {
               case 0 :
                     return conditional_P00672(context, (string)dynConstraints[0] , (string)dynConstraints[1] , (string)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (string)dynConstraints[5] , (string)dynConstraints[6] , (string)dynConstraints[7] , (string)dynConstraints[8] , (string)dynConstraints[9] , (string)dynConstraints[10] , (string)dynConstraints[11] , (string)dynConstraints[12] , (string)dynConstraints[13] , (string)dynConstraints[14] , (string)dynConstraints[15] , (Guid)dynConstraints[16] );
               case 4 :
                     return conditional_P00676(context, (string)dynConstraints[0] , (string)dynConstraints[1] , (string)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (string)dynConstraints[5] , (string)dynConstraints[6] , (string)dynConstraints[7] , (string)dynConstraints[8] , (string)dynConstraints[9] , (string)dynConstraints[10] , (string)dynConstraints[11] , (string)dynConstraints[12] , (string)dynConstraints[13] , (string)dynConstraints[14] , (string)dynConstraints[15] , (Guid)dynConstraints[16] );
               case 8 :
                     return conditional_P006710(context, (string)dynConstraints[0] , (string)dynConstraints[1] , (string)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (string)dynConstraints[5] , (string)dynConstraints[6] , (string)dynConstraints[7] , (string)dynConstraints[8] , (string)dynConstraints[9] , (string)dynConstraints[10] , (string)dynConstraints[11] , (string)dynConstraints[12] , (string)dynConstraints[13] , (string)dynConstraints[14] , (string)dynConstraints[15] , (Guid)dynConstraints[16] );
               case 12 :
                     return conditional_P006714(context, (string)dynConstraints[0] , (string)dynConstraints[1] , (string)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (string)dynConstraints[5] , (string)dynConstraints[6] , (string)dynConstraints[7] , (string)dynConstraints[8] , (string)dynConstraints[9] , (string)dynConstraints[10] , (string)dynConstraints[11] , (string)dynConstraints[12] , (string)dynConstraints[13] , (string)dynConstraints[14] , (string)dynConstraints[15] , (Guid)dynConstraints[16] );
               case 16 :
                     return conditional_P006718(context, (string)dynConstraints[0] , (string)dynConstraints[1] , (string)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (string)dynConstraints[5] , (string)dynConstraints[6] , (string)dynConstraints[7] , (string)dynConstraints[8] , (string)dynConstraints[9] , (string)dynConstraints[10] , (string)dynConstraints[11] , (string)dynConstraints[12] , (string)dynConstraints[13] , (string)dynConstraints[14] , (string)dynConstraints[15] , (Guid)dynConstraints[16] );
         }
         return base.getDynamicStatement(cursor, context, dynConstraints);
      }

      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
         ,new ForEachCursor(def[1])
         ,new ForEachCursor(def[2])
         ,new ForEachCursor(def[3])
         ,new ForEachCursor(def[4])
         ,new ForEachCursor(def[5])
         ,new ForEachCursor(def[6])
         ,new ForEachCursor(def[7])
         ,new ForEachCursor(def[8])
         ,new ForEachCursor(def[9])
         ,new ForEachCursor(def[10])
         ,new ForEachCursor(def[11])
         ,new ForEachCursor(def[12])
         ,new ForEachCursor(def[13])
         ,new ForEachCursor(def[14])
         ,new ForEachCursor(def[15])
         ,new ForEachCursor(def[16])
         ,new ForEachCursor(def[17])
         ,new ForEachCursor(def[18])
         ,new ForEachCursor(def[19])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP00673;
          prmP00673 = new Object[] {
          new ParDef("SG_LocationSupplierOrganisatio",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP00674;
          prmP00674 = new Object[] {
          new ParDef("ActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP00675;
          prmP00675 = new Object[] {
          new ParDef("PublishedActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP00677;
          prmP00677 = new Object[] {
          new ParDef("SG_LocationSupplierOrganisatio",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP00678;
          prmP00678 = new Object[] {
          new ParDef("ActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP00679;
          prmP00679 = new Object[] {
          new ParDef("PublishedActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP006711;
          prmP006711 = new Object[] {
          new ParDef("SG_LocationSupplierOrganisatio",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP006712;
          prmP006712 = new Object[] {
          new ParDef("ActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP006713;
          prmP006713 = new Object[] {
          new ParDef("PublishedActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP006715;
          prmP006715 = new Object[] {
          new ParDef("SG_LocationSupplierOrganisatio",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP006716;
          prmP006716 = new Object[] {
          new ParDef("ActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP006717;
          prmP006717 = new Object[] {
          new ParDef("PublishedActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP006719;
          prmP006719 = new Object[] {
          new ParDef("SG_LocationSupplierOrganisatio",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP006720;
          prmP006720 = new Object[] {
          new ParDef("ActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP006721;
          prmP006721 = new Object[] {
          new ParDef("PublishedActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP00672;
          prmP00672 = new Object[] {
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname",GXType.VarChar,100,0) ,
          new ParDef("AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel",GXType.VarChar,100,0) ,
          new ParDef("lV70Trn_suppliergenwwds_4_tfsuppliergentypename",GXType.VarChar,100,0) ,
          new ParDef("AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel",GXType.VarChar,100,0) ,
          new ParDef("lV72Trn_suppliergenwwds_6_tfsuppliergencontactname",GXType.VarChar,100,0) ,
          new ParDef("AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel",GXType.VarChar,100,0) ,
          new ParDef("lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone",GXType.Char,20,0) ,
          new ParDef("AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel",GXType.Char,20,0) ,
          new ParDef("lV76Trn_suppliergenwwds_10_tfsuppliergendescription",GXType.VarChar,200,0) ,
          new ParDef("AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel",GXType.VarChar,200,0)
          };
          Object[] prmP00676;
          prmP00676 = new Object[] {
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname",GXType.VarChar,100,0) ,
          new ParDef("AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel",GXType.VarChar,100,0) ,
          new ParDef("lV70Trn_suppliergenwwds_4_tfsuppliergentypename",GXType.VarChar,100,0) ,
          new ParDef("AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel",GXType.VarChar,100,0) ,
          new ParDef("lV72Trn_suppliergenwwds_6_tfsuppliergencontactname",GXType.VarChar,100,0) ,
          new ParDef("AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel",GXType.VarChar,100,0) ,
          new ParDef("lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone",GXType.Char,20,0) ,
          new ParDef("AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel",GXType.Char,20,0) ,
          new ParDef("lV76Trn_suppliergenwwds_10_tfsuppliergendescription",GXType.VarChar,200,0) ,
          new ParDef("AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel",GXType.VarChar,200,0)
          };
          Object[] prmP006710;
          prmP006710 = new Object[] {
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname",GXType.VarChar,100,0) ,
          new ParDef("AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel",GXType.VarChar,100,0) ,
          new ParDef("lV70Trn_suppliergenwwds_4_tfsuppliergentypename",GXType.VarChar,100,0) ,
          new ParDef("AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel",GXType.VarChar,100,0) ,
          new ParDef("lV72Trn_suppliergenwwds_6_tfsuppliergencontactname",GXType.VarChar,100,0) ,
          new ParDef("AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel",GXType.VarChar,100,0) ,
          new ParDef("lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone",GXType.Char,20,0) ,
          new ParDef("AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel",GXType.Char,20,0) ,
          new ParDef("lV76Trn_suppliergenwwds_10_tfsuppliergendescription",GXType.VarChar,200,0) ,
          new ParDef("AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel",GXType.VarChar,200,0)
          };
          Object[] prmP006714;
          prmP006714 = new Object[] {
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname",GXType.VarChar,100,0) ,
          new ParDef("AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel",GXType.VarChar,100,0) ,
          new ParDef("lV70Trn_suppliergenwwds_4_tfsuppliergentypename",GXType.VarChar,100,0) ,
          new ParDef("AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel",GXType.VarChar,100,0) ,
          new ParDef("lV72Trn_suppliergenwwds_6_tfsuppliergencontactname",GXType.VarChar,100,0) ,
          new ParDef("AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel",GXType.VarChar,100,0) ,
          new ParDef("lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone",GXType.Char,20,0) ,
          new ParDef("AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel",GXType.Char,20,0) ,
          new ParDef("lV76Trn_suppliergenwwds_10_tfsuppliergendescription",GXType.VarChar,200,0) ,
          new ParDef("AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel",GXType.VarChar,200,0)
          };
          Object[] prmP006718;
          prmP006718 = new Object[] {
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV67Trn_suppliergenwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV68Trn_suppliergenwwds_2_tfsuppliergencompanyname",GXType.VarChar,100,0) ,
          new ParDef("AV69Trn_suppliergenwwds_3_tfsuppliergencompanyname_sel",GXType.VarChar,100,0) ,
          new ParDef("lV70Trn_suppliergenwwds_4_tfsuppliergentypename",GXType.VarChar,100,0) ,
          new ParDef("AV71Trn_suppliergenwwds_5_tfsuppliergentypename_sel",GXType.VarChar,100,0) ,
          new ParDef("lV72Trn_suppliergenwwds_6_tfsuppliergencontactname",GXType.VarChar,100,0) ,
          new ParDef("AV73Trn_suppliergenwwds_7_tfsuppliergencontactname_sel",GXType.VarChar,100,0) ,
          new ParDef("lV74Trn_suppliergenwwds_8_tfsuppliergencontactphone",GXType.Char,20,0) ,
          new ParDef("AV75Trn_suppliergenwwds_9_tfsuppliergencontactphone_sel",GXType.Char,20,0) ,
          new ParDef("lV76Trn_suppliergenwwds_10_tfsuppliergendescription",GXType.VarChar,200,0) ,
          new ParDef("AV77Trn_suppliergenwwds_11_tfsuppliergendescription_sel",GXType.VarChar,200,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00672", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00672,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00673", "SELECT OrganisationId FROM Trn_Organisation WHERE OrganisationId = :SG_LocationSupplierOrganisatio ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00673,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00674", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :ActiveAppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00674,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00675", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :PublishedActiveAppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00675,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00676", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00676,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00677", "SELECT OrganisationId FROM Trn_Organisation WHERE OrganisationId = :SG_LocationSupplierOrganisatio ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00677,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00678", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :ActiveAppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00678,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00679", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :PublishedActiveAppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00679,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P006710", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP006710,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P006711", "SELECT OrganisationId FROM Trn_Organisation WHERE OrganisationId = :SG_LocationSupplierOrganisatio ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP006711,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P006712", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :ActiveAppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP006712,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P006713", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :PublishedActiveAppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP006713,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P006714", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP006714,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P006715", "SELECT OrganisationId FROM Trn_Organisation WHERE OrganisationId = :SG_LocationSupplierOrganisatio ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP006715,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P006716", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :ActiveAppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP006716,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P006717", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :PublishedActiveAppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP006717,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P006718", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP006718,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P006719", "SELECT OrganisationId FROM Trn_Organisation WHERE OrganisationId = :SG_LocationSupplierOrganisatio ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP006719,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P006720", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :ActiveAppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP006720,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P006721", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :PublishedActiveAppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP006721,1, GxCacheFrequency.OFF ,true,false )
          };
       }
    }

    public void getResults( int cursor ,
                            IFieldGetter rslt ,
                            Object[] buf )
    {
       switch ( cursor )
       {
             case 0 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((Guid[]) buf[1])[0] = rslt.getGuid(2);
                ((bool[]) buf[2])[0] = rslt.wasNull(2);
                ((Guid[]) buf[3])[0] = rslt.getGuid(3);
                ((bool[]) buf[4])[0] = rslt.wasNull(3);
                ((Guid[]) buf[5])[0] = rslt.getGuid(4);
                ((bool[]) buf[6])[0] = rslt.wasNull(4);
                ((Guid[]) buf[7])[0] = rslt.getGuid(5);
                ((bool[]) buf[8])[0] = rslt.wasNull(5);
                ((string[]) buf[9])[0] = rslt.getVarchar(6);
                ((string[]) buf[10])[0] = rslt.getLongVarchar(7);
                ((string[]) buf[11])[0] = rslt.getString(8, 20);
                ((string[]) buf[12])[0] = rslt.getVarchar(9);
                ((string[]) buf[13])[0] = rslt.getVarchar(10);
                ((Guid[]) buf[14])[0] = rslt.getGuid(11);
                return;
             case 1 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
             case 2 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
             case 3 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
             case 4 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((bool[]) buf[1])[0] = rslt.wasNull(1);
                ((Guid[]) buf[2])[0] = rslt.getGuid(2);
                ((bool[]) buf[3])[0] = rslt.wasNull(2);
                ((Guid[]) buf[4])[0] = rslt.getGuid(3);
                ((bool[]) buf[5])[0] = rslt.wasNull(3);
                ((Guid[]) buf[6])[0] = rslt.getGuid(4);
                ((bool[]) buf[7])[0] = rslt.wasNull(4);
                ((Guid[]) buf[8])[0] = rslt.getGuid(5);
                ((string[]) buf[9])[0] = rslt.getLongVarchar(6);
                ((string[]) buf[10])[0] = rslt.getString(7, 20);
                ((string[]) buf[11])[0] = rslt.getVarchar(8);
                ((string[]) buf[12])[0] = rslt.getVarchar(9);
                ((string[]) buf[13])[0] = rslt.getVarchar(10);
                ((Guid[]) buf[14])[0] = rslt.getGuid(11);
                return;
             case 5 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
             case 6 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
             case 7 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
             case 8 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((Guid[]) buf[1])[0] = rslt.getGuid(2);
                ((bool[]) buf[2])[0] = rslt.wasNull(2);
                ((Guid[]) buf[3])[0] = rslt.getGuid(3);
                ((bool[]) buf[4])[0] = rslt.wasNull(3);
                ((Guid[]) buf[5])[0] = rslt.getGuid(4);
                ((bool[]) buf[6])[0] = rslt.wasNull(4);
                ((Guid[]) buf[7])[0] = rslt.getGuid(5);
                ((bool[]) buf[8])[0] = rslt.wasNull(5);
                ((string[]) buf[9])[0] = rslt.getVarchar(6);
                ((string[]) buf[10])[0] = rslt.getLongVarchar(7);
                ((string[]) buf[11])[0] = rslt.getString(8, 20);
                ((string[]) buf[12])[0] = rslt.getVarchar(9);
                ((string[]) buf[13])[0] = rslt.getVarchar(10);
                ((Guid[]) buf[14])[0] = rslt.getGuid(11);
                return;
             case 9 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
             case 10 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
             case 11 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
             case 12 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((Guid[]) buf[1])[0] = rslt.getGuid(2);
                ((bool[]) buf[2])[0] = rslt.wasNull(2);
                ((Guid[]) buf[3])[0] = rslt.getGuid(3);
                ((bool[]) buf[4])[0] = rslt.wasNull(3);
                ((Guid[]) buf[5])[0] = rslt.getGuid(4);
                ((bool[]) buf[6])[0] = rslt.wasNull(4);
                ((Guid[]) buf[7])[0] = rslt.getGuid(5);
                ((bool[]) buf[8])[0] = rslt.wasNull(5);
                ((string[]) buf[9])[0] = rslt.getString(6, 20);
                ((string[]) buf[10])[0] = rslt.getLongVarchar(7);
                ((string[]) buf[11])[0] = rslt.getVarchar(8);
                ((string[]) buf[12])[0] = rslt.getVarchar(9);
                ((string[]) buf[13])[0] = rslt.getVarchar(10);
                ((Guid[]) buf[14])[0] = rslt.getGuid(11);
                return;
             case 13 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
             case 14 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
             case 15 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
             case 16 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((Guid[]) buf[1])[0] = rslt.getGuid(2);
                ((bool[]) buf[2])[0] = rslt.wasNull(2);
                ((Guid[]) buf[3])[0] = rslt.getGuid(3);
                ((bool[]) buf[4])[0] = rslt.wasNull(3);
                ((Guid[]) buf[5])[0] = rslt.getGuid(4);
                ((bool[]) buf[6])[0] = rslt.wasNull(4);
                ((Guid[]) buf[7])[0] = rslt.getGuid(5);
                ((bool[]) buf[8])[0] = rslt.wasNull(5);
                ((string[]) buf[9])[0] = rslt.getLongVarchar(6);
                ((string[]) buf[10])[0] = rslt.getString(7, 20);
                ((string[]) buf[11])[0] = rslt.getVarchar(8);
                ((string[]) buf[12])[0] = rslt.getVarchar(9);
                ((string[]) buf[13])[0] = rslt.getVarchar(10);
                ((Guid[]) buf[14])[0] = rslt.getGuid(11);
                return;
             case 17 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
             case 18 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
             case 19 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
       }
    }

 }

}
