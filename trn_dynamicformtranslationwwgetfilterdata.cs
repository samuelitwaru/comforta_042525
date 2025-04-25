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
   public class trn_dynamicformtranslationwwgetfilterdata : GXProcedure
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
            return "trn_dynamicformtranslationww_Services_Execute" ;
         }

      }

      public trn_dynamicformtranslationwwgetfilterdata( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public trn_dynamicformtranslationwwgetfilterdata( IGxContext context )
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
         this.AV41DDOName = aP0_DDOName;
         this.AV42SearchTxtParms = aP1_SearchTxtParms;
         this.AV43SearchTxtTo = aP2_SearchTxtTo;
         this.AV44OptionsJson = "" ;
         this.AV45OptionsDescJson = "" ;
         this.AV46OptionIndexesJson = "" ;
         initialize();
         ExecuteImpl();
         aP3_OptionsJson=this.AV44OptionsJson;
         aP4_OptionsDescJson=this.AV45OptionsDescJson;
         aP5_OptionIndexesJson=this.AV46OptionIndexesJson;
      }

      public string executeUdp( string aP0_DDOName ,
                                string aP1_SearchTxtParms ,
                                string aP2_SearchTxtTo ,
                                out string aP3_OptionsJson ,
                                out string aP4_OptionsDescJson )
      {
         execute(aP0_DDOName, aP1_SearchTxtParms, aP2_SearchTxtTo, out aP3_OptionsJson, out aP4_OptionsDescJson, out aP5_OptionIndexesJson);
         return AV46OptionIndexesJson ;
      }

      public void executeSubmit( string aP0_DDOName ,
                                 string aP1_SearchTxtParms ,
                                 string aP2_SearchTxtTo ,
                                 out string aP3_OptionsJson ,
                                 out string aP4_OptionsDescJson ,
                                 out string aP5_OptionIndexesJson )
      {
         this.AV41DDOName = aP0_DDOName;
         this.AV42SearchTxtParms = aP1_SearchTxtParms;
         this.AV43SearchTxtTo = aP2_SearchTxtTo;
         this.AV44OptionsJson = "" ;
         this.AV45OptionsDescJson = "" ;
         this.AV46OptionIndexesJson = "" ;
         SubmitImpl();
         aP3_OptionsJson=this.AV44OptionsJson;
         aP4_OptionsDescJson=this.AV45OptionsDescJson;
         aP5_OptionIndexesJson=this.AV46OptionIndexesJson;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV31Options = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV33OptionsDesc = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV34OptionIndexes = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV28MaxItems = 10;
         AV27PageIndex = (short)((String.IsNullOrEmpty(StringUtil.RTrim( AV42SearchTxtParms)) ? 0 : (long)(Math.Round(NumberUtil.Val( StringUtil.Substring( AV42SearchTxtParms, 1, 2), "."), 18, MidpointRounding.ToEven))));
         AV25SearchTxt = (String.IsNullOrEmpty(StringUtil.RTrim( AV42SearchTxtParms)) ? "" : StringUtil.Substring( AV42SearchTxtParms, 3, -1));
         AV26SkipItems = (short)(AV27PageIndex*AV28MaxItems);
         new GeneXus.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV9WWPContext) ;
         /* Execute user subroutine: 'LOADGRIDSTATE' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         if ( StringUtil.StrCmp(StringUtil.Upper( AV41DDOName), "DDO_DYNAMICFORMTRANSLATIONTRNNAME") == 0 )
         {
            /* Execute user subroutine: 'LOADDYNAMICFORMTRANSLATIONTRNNAMEOPTIONS' */
            S121 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         else if ( StringUtil.StrCmp(StringUtil.Upper( AV41DDOName), "DDO_DYNAMICFORMTRANSLATIONATTRIBUTENAME") == 0 )
         {
            /* Execute user subroutine: 'LOADDYNAMICFORMTRANSLATIONATTRIBUTENAMEOPTIONS' */
            S131 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         else if ( StringUtil.StrCmp(StringUtil.Upper( AV41DDOName), "DDO_DYNAMICFORMTRANSLATIONENGLISH") == 0 )
         {
            /* Execute user subroutine: 'LOADDYNAMICFORMTRANSLATIONENGLISHOPTIONS' */
            S141 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         else if ( StringUtil.StrCmp(StringUtil.Upper( AV41DDOName), "DDO_DYNAMICFORMTRANSLATIONDUTCH") == 0 )
         {
            /* Execute user subroutine: 'LOADDYNAMICFORMTRANSLATIONDUTCHOPTIONS' */
            S151 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         AV44OptionsJson = AV31Options.ToJSonString(false);
         AV45OptionsDescJson = AV33OptionsDesc.ToJSonString(false);
         AV46OptionIndexesJson = AV34OptionIndexes.ToJSonString(false);
         cleanup();
      }

      protected void S111( )
      {
         /* 'LOADGRIDSTATE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV36Session.Get("Trn_DynamicFormTranslationWWGridState"), "") == 0 )
         {
            AV38GridState.FromXml(new WorkWithPlus.workwithplus_web.loadgridstate(context).executeUdp(  "Trn_DynamicFormTranslationWWGridState"), null, "", "");
         }
         else
         {
            AV38GridState.FromXml(AV36Session.Get("Trn_DynamicFormTranslationWWGridState"), null, "", "");
         }
         AV48GXV1 = 1;
         while ( AV48GXV1 <= AV38GridState.gxTpr_Filtervalues.Count )
         {
            AV39GridStateFilterValue = ((WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue)AV38GridState.gxTpr_Filtervalues.Item(AV48GXV1));
            if ( StringUtil.StrCmp(AV39GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV47FilterFullText = AV39GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV39GridStateFilterValue.gxTpr_Name, "TFDYNAMICFORMTRANSLATIONWWPFORMID") == 0 )
            {
               AV11TFDynamicFormTranslationWWpFormId = (int)(Math.Round(NumberUtil.Val( AV39GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV12TFDynamicFormTranslationWWpFormId_To = (int)(Math.Round(NumberUtil.Val( AV39GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV39GridStateFilterValue.gxTpr_Name, "TFDYNAMICFORMTRANSLATIONWWPFORMVERSIONNUMBER") == 0 )
            {
               AV13TFDynamicFormTranslationWWPFormVersionNumber = (int)(Math.Round(NumberUtil.Val( AV39GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV14TFDynamicFormTranslationWWPFormVersionNumber_To = (int)(Math.Round(NumberUtil.Val( AV39GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV39GridStateFilterValue.gxTpr_Name, "TFDYNAMICFORMTRANSLATIONWWPFORMELEMENTID") == 0 )
            {
               AV15TFDynamicFormTranslationWWPFormElementId = (int)(Math.Round(NumberUtil.Val( AV39GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV16TFDynamicFormTranslationWWPFormElementId_To = (int)(Math.Round(NumberUtil.Val( AV39GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV39GridStateFilterValue.gxTpr_Name, "TFDYNAMICFORMTRANSLATIONTRNNAME") == 0 )
            {
               AV17TFDynamicFormTranslationTrnName = AV39GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV39GridStateFilterValue.gxTpr_Name, "TFDYNAMICFORMTRANSLATIONTRNNAME_SEL") == 0 )
            {
               AV18TFDynamicFormTranslationTrnName_Sel = AV39GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV39GridStateFilterValue.gxTpr_Name, "TFDYNAMICFORMTRANSLATIONATTRIBUTENAME") == 0 )
            {
               AV19TFDynamicFormTranslationAttributeName = AV39GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV39GridStateFilterValue.gxTpr_Name, "TFDYNAMICFORMTRANSLATIONATTRIBUTENAME_SEL") == 0 )
            {
               AV20TFDynamicFormTranslationAttributeName_Sel = AV39GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV39GridStateFilterValue.gxTpr_Name, "TFDYNAMICFORMTRANSLATIONENGLISH") == 0 )
            {
               AV21TFDynamicFormTranslationEnglish = AV39GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV39GridStateFilterValue.gxTpr_Name, "TFDYNAMICFORMTRANSLATIONENGLISH_SEL") == 0 )
            {
               AV22TFDynamicFormTranslationEnglish_Sel = AV39GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV39GridStateFilterValue.gxTpr_Name, "TFDYNAMICFORMTRANSLATIONDUTCH") == 0 )
            {
               AV23TFDynamicFormTranslationDutch = AV39GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV39GridStateFilterValue.gxTpr_Name, "TFDYNAMICFORMTRANSLATIONDUTCH_SEL") == 0 )
            {
               AV24TFDynamicFormTranslationDutch_Sel = AV39GridStateFilterValue.gxTpr_Value;
            }
            AV48GXV1 = (int)(AV48GXV1+1);
         }
      }

      protected void S121( )
      {
         /* 'LOADDYNAMICFORMTRANSLATIONTRNNAMEOPTIONS' Routine */
         returnInSub = false;
         AV17TFDynamicFormTranslationTrnName = AV25SearchTxt;
         AV18TFDynamicFormTranslationTrnName_Sel = "";
         AV50Trn_dynamicformtranslationwwds_1_filterfulltext = AV47FilterFullText;
         AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid = AV11TFDynamicFormTranslationWWpFormId;
         AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to = AV12TFDynamicFormTranslationWWpFormId_To;
         AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber = AV13TFDynamicFormTranslationWWPFormVersionNumber;
         AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to = AV14TFDynamicFormTranslationWWPFormVersionNumber_To;
         AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid = AV15TFDynamicFormTranslationWWPFormElementId;
         AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to = AV16TFDynamicFormTranslationWWPFormElementId_To;
         AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname = AV17TFDynamicFormTranslationTrnName;
         AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel = AV18TFDynamicFormTranslationTrnName_Sel;
         AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename = AV19TFDynamicFormTranslationAttributeName;
         AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel = AV20TFDynamicFormTranslationAttributeName_Sel;
         AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish = AV21TFDynamicFormTranslationEnglish;
         AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel = AV22TFDynamicFormTranslationEnglish_Sel;
         AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch = AV23TFDynamicFormTranslationDutch;
         AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel = AV24TFDynamicFormTranslationDutch_Sel;
         pr_default.dynParam(0, new Object[]{ new Object[]{
                                              AV50Trn_dynamicformtranslationwwds_1_filterfulltext ,
                                              AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid ,
                                              AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to ,
                                              AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber ,
                                              AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to ,
                                              AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid ,
                                              AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to ,
                                              AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel ,
                                              AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname ,
                                              AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel ,
                                              AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename ,
                                              AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel ,
                                              AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish ,
                                              AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel ,
                                              AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch ,
                                              A588DynamicFormTranslationWWpFormI ,
                                              A589DynamicFormTranslationWWPFormV ,
                                              A590DynamicFormTranslationWWPFormE ,
                                              A591DynamicFormTranslationTrnName ,
                                              A592DynamicFormTranslationAttribut ,
                                              A593DynamicFormTranslationEnglish ,
                                              A594DynamicFormTranslationDutch } ,
                                              new int[]{
                                              TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT
                                              }
         });
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname = StringUtil.Concat( StringUtil.RTrim( AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname), "%", "");
         lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename = StringUtil.Concat( StringUtil.RTrim( AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename), "%", "");
         lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish = StringUtil.Concat( StringUtil.RTrim( AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish), "%", "");
         lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch = StringUtil.Concat( StringUtil.RTrim( AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch), "%", "");
         /* Using cursor P00EH2 */
         pr_default.execute(0, new Object[] {lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid, AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to, AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber, AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to, AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid, AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to, lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname, AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel, lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename, AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel, lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish, AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel, lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch, AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel});
         while ( (pr_default.getStatus(0) != 101) )
         {
            BRKEH2 = false;
            A591DynamicFormTranslationTrnName = P00EH2_A591DynamicFormTranslationTrnName[0];
            A594DynamicFormTranslationDutch = P00EH2_A594DynamicFormTranslationDutch[0];
            A593DynamicFormTranslationEnglish = P00EH2_A593DynamicFormTranslationEnglish[0];
            A592DynamicFormTranslationAttribut = P00EH2_A592DynamicFormTranslationAttribut[0];
            A590DynamicFormTranslationWWPFormE = P00EH2_A590DynamicFormTranslationWWPFormE[0];
            A589DynamicFormTranslationWWPFormV = P00EH2_A589DynamicFormTranslationWWPFormV[0];
            A588DynamicFormTranslationWWpFormI = P00EH2_A588DynamicFormTranslationWWpFormI[0];
            A587DynamicFormTranslationId = P00EH2_A587DynamicFormTranslationId[0];
            AV35count = 0;
            while ( (pr_default.getStatus(0) != 101) && ( StringUtil.StrCmp(P00EH2_A591DynamicFormTranslationTrnName[0], A591DynamicFormTranslationTrnName) == 0 ) )
            {
               BRKEH2 = false;
               A587DynamicFormTranslationId = P00EH2_A587DynamicFormTranslationId[0];
               AV35count = (long)(AV35count+1);
               BRKEH2 = true;
               pr_default.readNext(0);
            }
            if ( (0==AV26SkipItems) )
            {
               AV30Option = (String.IsNullOrEmpty(StringUtil.RTrim( A591DynamicFormTranslationTrnName)) ? "<#Empty#>" : A591DynamicFormTranslationTrnName);
               AV31Options.Add(AV30Option, 0);
               AV34OptionIndexes.Add(StringUtil.Trim( context.localUtil.Format( (decimal)(AV35count), "Z,ZZZ,ZZZ,ZZ9")), 0);
               if ( AV31Options.Count == 10 )
               {
                  /* Exit For each command. Update data (if necessary), close cursors & exit. */
                  if (true) break;
               }
            }
            else
            {
               AV26SkipItems = (short)(AV26SkipItems-1);
            }
            if ( ! BRKEH2 )
            {
               BRKEH2 = true;
               pr_default.readNext(0);
            }
         }
         pr_default.close(0);
      }

      protected void S131( )
      {
         /* 'LOADDYNAMICFORMTRANSLATIONATTRIBUTENAMEOPTIONS' Routine */
         returnInSub = false;
         AV19TFDynamicFormTranslationAttributeName = AV25SearchTxt;
         AV20TFDynamicFormTranslationAttributeName_Sel = "";
         AV50Trn_dynamicformtranslationwwds_1_filterfulltext = AV47FilterFullText;
         AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid = AV11TFDynamicFormTranslationWWpFormId;
         AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to = AV12TFDynamicFormTranslationWWpFormId_To;
         AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber = AV13TFDynamicFormTranslationWWPFormVersionNumber;
         AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to = AV14TFDynamicFormTranslationWWPFormVersionNumber_To;
         AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid = AV15TFDynamicFormTranslationWWPFormElementId;
         AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to = AV16TFDynamicFormTranslationWWPFormElementId_To;
         AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname = AV17TFDynamicFormTranslationTrnName;
         AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel = AV18TFDynamicFormTranslationTrnName_Sel;
         AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename = AV19TFDynamicFormTranslationAttributeName;
         AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel = AV20TFDynamicFormTranslationAttributeName_Sel;
         AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish = AV21TFDynamicFormTranslationEnglish;
         AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel = AV22TFDynamicFormTranslationEnglish_Sel;
         AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch = AV23TFDynamicFormTranslationDutch;
         AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel = AV24TFDynamicFormTranslationDutch_Sel;
         pr_default.dynParam(1, new Object[]{ new Object[]{
                                              AV50Trn_dynamicformtranslationwwds_1_filterfulltext ,
                                              AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid ,
                                              AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to ,
                                              AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber ,
                                              AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to ,
                                              AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid ,
                                              AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to ,
                                              AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel ,
                                              AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname ,
                                              AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel ,
                                              AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename ,
                                              AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel ,
                                              AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish ,
                                              AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel ,
                                              AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch ,
                                              A588DynamicFormTranslationWWpFormI ,
                                              A589DynamicFormTranslationWWPFormV ,
                                              A590DynamicFormTranslationWWPFormE ,
                                              A591DynamicFormTranslationTrnName ,
                                              A592DynamicFormTranslationAttribut ,
                                              A593DynamicFormTranslationEnglish ,
                                              A594DynamicFormTranslationDutch } ,
                                              new int[]{
                                              TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT
                                              }
         });
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname = StringUtil.Concat( StringUtil.RTrim( AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname), "%", "");
         lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename = StringUtil.Concat( StringUtil.RTrim( AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename), "%", "");
         lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish = StringUtil.Concat( StringUtil.RTrim( AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish), "%", "");
         lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch = StringUtil.Concat( StringUtil.RTrim( AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch), "%", "");
         /* Using cursor P00EH3 */
         pr_default.execute(1, new Object[] {lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid, AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to, AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber, AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to, AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid, AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to, lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname, AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel, lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename, AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel, lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish, AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel, lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch, AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel});
         while ( (pr_default.getStatus(1) != 101) )
         {
            BRKEH4 = false;
            A592DynamicFormTranslationAttribut = P00EH3_A592DynamicFormTranslationAttribut[0];
            A594DynamicFormTranslationDutch = P00EH3_A594DynamicFormTranslationDutch[0];
            A593DynamicFormTranslationEnglish = P00EH3_A593DynamicFormTranslationEnglish[0];
            A591DynamicFormTranslationTrnName = P00EH3_A591DynamicFormTranslationTrnName[0];
            A590DynamicFormTranslationWWPFormE = P00EH3_A590DynamicFormTranslationWWPFormE[0];
            A589DynamicFormTranslationWWPFormV = P00EH3_A589DynamicFormTranslationWWPFormV[0];
            A588DynamicFormTranslationWWpFormI = P00EH3_A588DynamicFormTranslationWWpFormI[0];
            A587DynamicFormTranslationId = P00EH3_A587DynamicFormTranslationId[0];
            AV35count = 0;
            while ( (pr_default.getStatus(1) != 101) && ( StringUtil.StrCmp(P00EH3_A592DynamicFormTranslationAttribut[0], A592DynamicFormTranslationAttribut) == 0 ) )
            {
               BRKEH4 = false;
               A587DynamicFormTranslationId = P00EH3_A587DynamicFormTranslationId[0];
               AV35count = (long)(AV35count+1);
               BRKEH4 = true;
               pr_default.readNext(1);
            }
            if ( (0==AV26SkipItems) )
            {
               AV30Option = (String.IsNullOrEmpty(StringUtil.RTrim( A592DynamicFormTranslationAttribut)) ? "<#Empty#>" : A592DynamicFormTranslationAttribut);
               AV31Options.Add(AV30Option, 0);
               AV34OptionIndexes.Add(StringUtil.Trim( context.localUtil.Format( (decimal)(AV35count), "Z,ZZZ,ZZZ,ZZ9")), 0);
               if ( AV31Options.Count == 10 )
               {
                  /* Exit For each command. Update data (if necessary), close cursors & exit. */
                  if (true) break;
               }
            }
            else
            {
               AV26SkipItems = (short)(AV26SkipItems-1);
            }
            if ( ! BRKEH4 )
            {
               BRKEH4 = true;
               pr_default.readNext(1);
            }
         }
         pr_default.close(1);
      }

      protected void S141( )
      {
         /* 'LOADDYNAMICFORMTRANSLATIONENGLISHOPTIONS' Routine */
         returnInSub = false;
         AV21TFDynamicFormTranslationEnglish = AV25SearchTxt;
         AV22TFDynamicFormTranslationEnglish_Sel = "";
         AV50Trn_dynamicformtranslationwwds_1_filterfulltext = AV47FilterFullText;
         AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid = AV11TFDynamicFormTranslationWWpFormId;
         AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to = AV12TFDynamicFormTranslationWWpFormId_To;
         AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber = AV13TFDynamicFormTranslationWWPFormVersionNumber;
         AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to = AV14TFDynamicFormTranslationWWPFormVersionNumber_To;
         AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid = AV15TFDynamicFormTranslationWWPFormElementId;
         AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to = AV16TFDynamicFormTranslationWWPFormElementId_To;
         AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname = AV17TFDynamicFormTranslationTrnName;
         AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel = AV18TFDynamicFormTranslationTrnName_Sel;
         AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename = AV19TFDynamicFormTranslationAttributeName;
         AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel = AV20TFDynamicFormTranslationAttributeName_Sel;
         AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish = AV21TFDynamicFormTranslationEnglish;
         AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel = AV22TFDynamicFormTranslationEnglish_Sel;
         AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch = AV23TFDynamicFormTranslationDutch;
         AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel = AV24TFDynamicFormTranslationDutch_Sel;
         pr_default.dynParam(2, new Object[]{ new Object[]{
                                              AV50Trn_dynamicformtranslationwwds_1_filterfulltext ,
                                              AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid ,
                                              AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to ,
                                              AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber ,
                                              AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to ,
                                              AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid ,
                                              AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to ,
                                              AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel ,
                                              AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname ,
                                              AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel ,
                                              AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename ,
                                              AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel ,
                                              AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish ,
                                              AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel ,
                                              AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch ,
                                              A588DynamicFormTranslationWWpFormI ,
                                              A589DynamicFormTranslationWWPFormV ,
                                              A590DynamicFormTranslationWWPFormE ,
                                              A591DynamicFormTranslationTrnName ,
                                              A592DynamicFormTranslationAttribut ,
                                              A593DynamicFormTranslationEnglish ,
                                              A594DynamicFormTranslationDutch } ,
                                              new int[]{
                                              TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT
                                              }
         });
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname = StringUtil.Concat( StringUtil.RTrim( AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname), "%", "");
         lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename = StringUtil.Concat( StringUtil.RTrim( AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename), "%", "");
         lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish = StringUtil.Concat( StringUtil.RTrim( AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish), "%", "");
         lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch = StringUtil.Concat( StringUtil.RTrim( AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch), "%", "");
         /* Using cursor P00EH4 */
         pr_default.execute(2, new Object[] {lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid, AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to, AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber, AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to, AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid, AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to, lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname, AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel, lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename, AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel, lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish, AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel, lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch, AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel});
         while ( (pr_default.getStatus(2) != 101) )
         {
            BRKEH6 = false;
            A593DynamicFormTranslationEnglish = P00EH4_A593DynamicFormTranslationEnglish[0];
            A594DynamicFormTranslationDutch = P00EH4_A594DynamicFormTranslationDutch[0];
            A592DynamicFormTranslationAttribut = P00EH4_A592DynamicFormTranslationAttribut[0];
            A591DynamicFormTranslationTrnName = P00EH4_A591DynamicFormTranslationTrnName[0];
            A590DynamicFormTranslationWWPFormE = P00EH4_A590DynamicFormTranslationWWPFormE[0];
            A589DynamicFormTranslationWWPFormV = P00EH4_A589DynamicFormTranslationWWPFormV[0];
            A588DynamicFormTranslationWWpFormI = P00EH4_A588DynamicFormTranslationWWpFormI[0];
            A587DynamicFormTranslationId = P00EH4_A587DynamicFormTranslationId[0];
            AV35count = 0;
            while ( (pr_default.getStatus(2) != 101) && ( StringUtil.StrCmp(P00EH4_A593DynamicFormTranslationEnglish[0], A593DynamicFormTranslationEnglish) == 0 ) )
            {
               BRKEH6 = false;
               A587DynamicFormTranslationId = P00EH4_A587DynamicFormTranslationId[0];
               AV35count = (long)(AV35count+1);
               BRKEH6 = true;
               pr_default.readNext(2);
            }
            if ( (0==AV26SkipItems) )
            {
               AV30Option = (String.IsNullOrEmpty(StringUtil.RTrim( A593DynamicFormTranslationEnglish)) ? "<#Empty#>" : A593DynamicFormTranslationEnglish);
               AV31Options.Add(AV30Option, 0);
               AV34OptionIndexes.Add(StringUtil.Trim( context.localUtil.Format( (decimal)(AV35count), "Z,ZZZ,ZZZ,ZZ9")), 0);
               if ( AV31Options.Count == 10 )
               {
                  /* Exit For each command. Update data (if necessary), close cursors & exit. */
                  if (true) break;
               }
            }
            else
            {
               AV26SkipItems = (short)(AV26SkipItems-1);
            }
            if ( ! BRKEH6 )
            {
               BRKEH6 = true;
               pr_default.readNext(2);
            }
         }
         pr_default.close(2);
      }

      protected void S151( )
      {
         /* 'LOADDYNAMICFORMTRANSLATIONDUTCHOPTIONS' Routine */
         returnInSub = false;
         AV23TFDynamicFormTranslationDutch = AV25SearchTxt;
         AV24TFDynamicFormTranslationDutch_Sel = "";
         AV50Trn_dynamicformtranslationwwds_1_filterfulltext = AV47FilterFullText;
         AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid = AV11TFDynamicFormTranslationWWpFormId;
         AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to = AV12TFDynamicFormTranslationWWpFormId_To;
         AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber = AV13TFDynamicFormTranslationWWPFormVersionNumber;
         AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to = AV14TFDynamicFormTranslationWWPFormVersionNumber_To;
         AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid = AV15TFDynamicFormTranslationWWPFormElementId;
         AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to = AV16TFDynamicFormTranslationWWPFormElementId_To;
         AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname = AV17TFDynamicFormTranslationTrnName;
         AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel = AV18TFDynamicFormTranslationTrnName_Sel;
         AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename = AV19TFDynamicFormTranslationAttributeName;
         AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel = AV20TFDynamicFormTranslationAttributeName_Sel;
         AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish = AV21TFDynamicFormTranslationEnglish;
         AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel = AV22TFDynamicFormTranslationEnglish_Sel;
         AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch = AV23TFDynamicFormTranslationDutch;
         AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel = AV24TFDynamicFormTranslationDutch_Sel;
         pr_default.dynParam(3, new Object[]{ new Object[]{
                                              AV50Trn_dynamicformtranslationwwds_1_filterfulltext ,
                                              AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid ,
                                              AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to ,
                                              AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber ,
                                              AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to ,
                                              AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid ,
                                              AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to ,
                                              AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel ,
                                              AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname ,
                                              AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel ,
                                              AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename ,
                                              AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel ,
                                              AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish ,
                                              AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel ,
                                              AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch ,
                                              A588DynamicFormTranslationWWpFormI ,
                                              A589DynamicFormTranslationWWPFormV ,
                                              A590DynamicFormTranslationWWPFormE ,
                                              A591DynamicFormTranslationTrnName ,
                                              A592DynamicFormTranslationAttribut ,
                                              A593DynamicFormTranslationEnglish ,
                                              A594DynamicFormTranslationDutch } ,
                                              new int[]{
                                              TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT, TypeConstants.INT
                                              }
         });
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext), "%", "");
         lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname = StringUtil.Concat( StringUtil.RTrim( AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname), "%", "");
         lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename = StringUtil.Concat( StringUtil.RTrim( AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename), "%", "");
         lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish = StringUtil.Concat( StringUtil.RTrim( AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish), "%", "");
         lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch = StringUtil.Concat( StringUtil.RTrim( AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch), "%", "");
         /* Using cursor P00EH5 */
         pr_default.execute(3, new Object[] {lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, lV50Trn_dynamicformtranslationwwds_1_filterfulltext, AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid, AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to, AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber, AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to, AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid, AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to, lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname, AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel, lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename, AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel, lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish, AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel, lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch, AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel});
         while ( (pr_default.getStatus(3) != 101) )
         {
            BRKEH8 = false;
            A594DynamicFormTranslationDutch = P00EH5_A594DynamicFormTranslationDutch[0];
            A593DynamicFormTranslationEnglish = P00EH5_A593DynamicFormTranslationEnglish[0];
            A592DynamicFormTranslationAttribut = P00EH5_A592DynamicFormTranslationAttribut[0];
            A591DynamicFormTranslationTrnName = P00EH5_A591DynamicFormTranslationTrnName[0];
            A590DynamicFormTranslationWWPFormE = P00EH5_A590DynamicFormTranslationWWPFormE[0];
            A589DynamicFormTranslationWWPFormV = P00EH5_A589DynamicFormTranslationWWPFormV[0];
            A588DynamicFormTranslationWWpFormI = P00EH5_A588DynamicFormTranslationWWpFormI[0];
            A587DynamicFormTranslationId = P00EH5_A587DynamicFormTranslationId[0];
            AV35count = 0;
            while ( (pr_default.getStatus(3) != 101) && ( StringUtil.StrCmp(P00EH5_A594DynamicFormTranslationDutch[0], A594DynamicFormTranslationDutch) == 0 ) )
            {
               BRKEH8 = false;
               A587DynamicFormTranslationId = P00EH5_A587DynamicFormTranslationId[0];
               AV35count = (long)(AV35count+1);
               BRKEH8 = true;
               pr_default.readNext(3);
            }
            if ( (0==AV26SkipItems) )
            {
               AV30Option = (String.IsNullOrEmpty(StringUtil.RTrim( A594DynamicFormTranslationDutch)) ? "<#Empty#>" : A594DynamicFormTranslationDutch);
               AV31Options.Add(AV30Option, 0);
               AV34OptionIndexes.Add(StringUtil.Trim( context.localUtil.Format( (decimal)(AV35count), "Z,ZZZ,ZZZ,ZZ9")), 0);
               if ( AV31Options.Count == 10 )
               {
                  /* Exit For each command. Update data (if necessary), close cursors & exit. */
                  if (true) break;
               }
            }
            else
            {
               AV26SkipItems = (short)(AV26SkipItems-1);
            }
            if ( ! BRKEH8 )
            {
               BRKEH8 = true;
               pr_default.readNext(3);
            }
         }
         pr_default.close(3);
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

      public override void initialize( )
      {
         AV44OptionsJson = "";
         AV45OptionsDescJson = "";
         AV46OptionIndexesJson = "";
         AV31Options = new GxSimpleCollection<string>();
         AV33OptionsDesc = new GxSimpleCollection<string>();
         AV34OptionIndexes = new GxSimpleCollection<string>();
         AV25SearchTxt = "";
         AV9WWPContext = new GeneXus.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV36Session = context.GetSession();
         AV38GridState = new WorkWithPlus.workwithplus_web.SdtWWPGridState(context);
         AV39GridStateFilterValue = new WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue(context);
         AV47FilterFullText = "";
         AV17TFDynamicFormTranslationTrnName = "";
         AV18TFDynamicFormTranslationTrnName_Sel = "";
         AV19TFDynamicFormTranslationAttributeName = "";
         AV20TFDynamicFormTranslationAttributeName_Sel = "";
         AV21TFDynamicFormTranslationEnglish = "";
         AV22TFDynamicFormTranslationEnglish_Sel = "";
         AV23TFDynamicFormTranslationDutch = "";
         AV24TFDynamicFormTranslationDutch_Sel = "";
         AV50Trn_dynamicformtranslationwwds_1_filterfulltext = "";
         AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname = "";
         AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel = "";
         AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename = "";
         AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel = "";
         AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish = "";
         AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel = "";
         AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch = "";
         AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel = "";
         lV50Trn_dynamicformtranslationwwds_1_filterfulltext = "";
         lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname = "";
         lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename = "";
         lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish = "";
         lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch = "";
         A591DynamicFormTranslationTrnName = "";
         A592DynamicFormTranslationAttribut = "";
         A593DynamicFormTranslationEnglish = "";
         A594DynamicFormTranslationDutch = "";
         P00EH2_A591DynamicFormTranslationTrnName = new string[] {""} ;
         P00EH2_A594DynamicFormTranslationDutch = new string[] {""} ;
         P00EH2_A593DynamicFormTranslationEnglish = new string[] {""} ;
         P00EH2_A592DynamicFormTranslationAttribut = new string[] {""} ;
         P00EH2_A590DynamicFormTranslationWWPFormE = new int[1] ;
         P00EH2_A589DynamicFormTranslationWWPFormV = new int[1] ;
         P00EH2_A588DynamicFormTranslationWWpFormI = new int[1] ;
         P00EH2_A587DynamicFormTranslationId = new Guid[] {Guid.Empty} ;
         A587DynamicFormTranslationId = Guid.Empty;
         AV30Option = "";
         P00EH3_A592DynamicFormTranslationAttribut = new string[] {""} ;
         P00EH3_A594DynamicFormTranslationDutch = new string[] {""} ;
         P00EH3_A593DynamicFormTranslationEnglish = new string[] {""} ;
         P00EH3_A591DynamicFormTranslationTrnName = new string[] {""} ;
         P00EH3_A590DynamicFormTranslationWWPFormE = new int[1] ;
         P00EH3_A589DynamicFormTranslationWWPFormV = new int[1] ;
         P00EH3_A588DynamicFormTranslationWWpFormI = new int[1] ;
         P00EH3_A587DynamicFormTranslationId = new Guid[] {Guid.Empty} ;
         P00EH4_A593DynamicFormTranslationEnglish = new string[] {""} ;
         P00EH4_A594DynamicFormTranslationDutch = new string[] {""} ;
         P00EH4_A592DynamicFormTranslationAttribut = new string[] {""} ;
         P00EH4_A591DynamicFormTranslationTrnName = new string[] {""} ;
         P00EH4_A590DynamicFormTranslationWWPFormE = new int[1] ;
         P00EH4_A589DynamicFormTranslationWWPFormV = new int[1] ;
         P00EH4_A588DynamicFormTranslationWWpFormI = new int[1] ;
         P00EH4_A587DynamicFormTranslationId = new Guid[] {Guid.Empty} ;
         P00EH5_A594DynamicFormTranslationDutch = new string[] {""} ;
         P00EH5_A593DynamicFormTranslationEnglish = new string[] {""} ;
         P00EH5_A592DynamicFormTranslationAttribut = new string[] {""} ;
         P00EH5_A591DynamicFormTranslationTrnName = new string[] {""} ;
         P00EH5_A590DynamicFormTranslationWWPFormE = new int[1] ;
         P00EH5_A589DynamicFormTranslationWWPFormV = new int[1] ;
         P00EH5_A588DynamicFormTranslationWWpFormI = new int[1] ;
         P00EH5_A587DynamicFormTranslationId = new Guid[] {Guid.Empty} ;
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.trn_dynamicformtranslationwwgetfilterdata__default(),
            new Object[][] {
                new Object[] {
               P00EH2_A591DynamicFormTranslationTrnName, P00EH2_A594DynamicFormTranslationDutch, P00EH2_A593DynamicFormTranslationEnglish, P00EH2_A592DynamicFormTranslationAttribut, P00EH2_A590DynamicFormTranslationWWPFormE, P00EH2_A589DynamicFormTranslationWWPFormV, P00EH2_A588DynamicFormTranslationWWpFormI, P00EH2_A587DynamicFormTranslationId
               }
               , new Object[] {
               P00EH3_A592DynamicFormTranslationAttribut, P00EH3_A594DynamicFormTranslationDutch, P00EH3_A593DynamicFormTranslationEnglish, P00EH3_A591DynamicFormTranslationTrnName, P00EH3_A590DynamicFormTranslationWWPFormE, P00EH3_A589DynamicFormTranslationWWPFormV, P00EH3_A588DynamicFormTranslationWWpFormI, P00EH3_A587DynamicFormTranslationId
               }
               , new Object[] {
               P00EH4_A593DynamicFormTranslationEnglish, P00EH4_A594DynamicFormTranslationDutch, P00EH4_A592DynamicFormTranslationAttribut, P00EH4_A591DynamicFormTranslationTrnName, P00EH4_A590DynamicFormTranslationWWPFormE, P00EH4_A589DynamicFormTranslationWWPFormV, P00EH4_A588DynamicFormTranslationWWpFormI, P00EH4_A587DynamicFormTranslationId
               }
               , new Object[] {
               P00EH5_A594DynamicFormTranslationDutch, P00EH5_A593DynamicFormTranslationEnglish, P00EH5_A592DynamicFormTranslationAttribut, P00EH5_A591DynamicFormTranslationTrnName, P00EH5_A590DynamicFormTranslationWWPFormE, P00EH5_A589DynamicFormTranslationWWPFormV, P00EH5_A588DynamicFormTranslationWWpFormI, P00EH5_A587DynamicFormTranslationId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV28MaxItems ;
      private short AV27PageIndex ;
      private short AV26SkipItems ;
      private int AV48GXV1 ;
      private int AV11TFDynamicFormTranslationWWpFormId ;
      private int AV12TFDynamicFormTranslationWWpFormId_To ;
      private int AV13TFDynamicFormTranslationWWPFormVersionNumber ;
      private int AV14TFDynamicFormTranslationWWPFormVersionNumber_To ;
      private int AV15TFDynamicFormTranslationWWPFormElementId ;
      private int AV16TFDynamicFormTranslationWWPFormElementId_To ;
      private int AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid ;
      private int AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to ;
      private int AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber ;
      private int AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to ;
      private int AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid ;
      private int AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to ;
      private int A588DynamicFormTranslationWWpFormI ;
      private int A589DynamicFormTranslationWWPFormV ;
      private int A590DynamicFormTranslationWWPFormE ;
      private long AV35count ;
      private bool returnInSub ;
      private bool BRKEH2 ;
      private bool BRKEH4 ;
      private bool BRKEH6 ;
      private bool BRKEH8 ;
      private string AV44OptionsJson ;
      private string AV45OptionsDescJson ;
      private string AV46OptionIndexesJson ;
      private string A593DynamicFormTranslationEnglish ;
      private string A594DynamicFormTranslationDutch ;
      private string AV41DDOName ;
      private string AV42SearchTxtParms ;
      private string AV43SearchTxtTo ;
      private string AV25SearchTxt ;
      private string AV47FilterFullText ;
      private string AV17TFDynamicFormTranslationTrnName ;
      private string AV18TFDynamicFormTranslationTrnName_Sel ;
      private string AV19TFDynamicFormTranslationAttributeName ;
      private string AV20TFDynamicFormTranslationAttributeName_Sel ;
      private string AV21TFDynamicFormTranslationEnglish ;
      private string AV22TFDynamicFormTranslationEnglish_Sel ;
      private string AV23TFDynamicFormTranslationDutch ;
      private string AV24TFDynamicFormTranslationDutch_Sel ;
      private string AV50Trn_dynamicformtranslationwwds_1_filterfulltext ;
      private string AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname ;
      private string AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel ;
      private string AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename ;
      private string AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel ;
      private string AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish ;
      private string AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel ;
      private string AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch ;
      private string AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel ;
      private string lV50Trn_dynamicformtranslationwwds_1_filterfulltext ;
      private string lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname ;
      private string lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename ;
      private string lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish ;
      private string lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch ;
      private string A591DynamicFormTranslationTrnName ;
      private string A592DynamicFormTranslationAttribut ;
      private string AV30Option ;
      private Guid A587DynamicFormTranslationId ;
      private IGxSession AV36Session ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GxSimpleCollection<string> AV31Options ;
      private GxSimpleCollection<string> AV33OptionsDesc ;
      private GxSimpleCollection<string> AV34OptionIndexes ;
      private GeneXus.Programs.wwpbaseobjects.SdtWWPContext AV9WWPContext ;
      private WorkWithPlus.workwithplus_web.SdtWWPGridState AV38GridState ;
      private WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue AV39GridStateFilterValue ;
      private IDataStoreProvider pr_default ;
      private string[] P00EH2_A591DynamicFormTranslationTrnName ;
      private string[] P00EH2_A594DynamicFormTranslationDutch ;
      private string[] P00EH2_A593DynamicFormTranslationEnglish ;
      private string[] P00EH2_A592DynamicFormTranslationAttribut ;
      private int[] P00EH2_A590DynamicFormTranslationWWPFormE ;
      private int[] P00EH2_A589DynamicFormTranslationWWPFormV ;
      private int[] P00EH2_A588DynamicFormTranslationWWpFormI ;
      private Guid[] P00EH2_A587DynamicFormTranslationId ;
      private string[] P00EH3_A592DynamicFormTranslationAttribut ;
      private string[] P00EH3_A594DynamicFormTranslationDutch ;
      private string[] P00EH3_A593DynamicFormTranslationEnglish ;
      private string[] P00EH3_A591DynamicFormTranslationTrnName ;
      private int[] P00EH3_A590DynamicFormTranslationWWPFormE ;
      private int[] P00EH3_A589DynamicFormTranslationWWPFormV ;
      private int[] P00EH3_A588DynamicFormTranslationWWpFormI ;
      private Guid[] P00EH3_A587DynamicFormTranslationId ;
      private string[] P00EH4_A593DynamicFormTranslationEnglish ;
      private string[] P00EH4_A594DynamicFormTranslationDutch ;
      private string[] P00EH4_A592DynamicFormTranslationAttribut ;
      private string[] P00EH4_A591DynamicFormTranslationTrnName ;
      private int[] P00EH4_A590DynamicFormTranslationWWPFormE ;
      private int[] P00EH4_A589DynamicFormTranslationWWPFormV ;
      private int[] P00EH4_A588DynamicFormTranslationWWpFormI ;
      private Guid[] P00EH4_A587DynamicFormTranslationId ;
      private string[] P00EH5_A594DynamicFormTranslationDutch ;
      private string[] P00EH5_A593DynamicFormTranslationEnglish ;
      private string[] P00EH5_A592DynamicFormTranslationAttribut ;
      private string[] P00EH5_A591DynamicFormTranslationTrnName ;
      private int[] P00EH5_A590DynamicFormTranslationWWPFormE ;
      private int[] P00EH5_A589DynamicFormTranslationWWPFormV ;
      private int[] P00EH5_A588DynamicFormTranslationWWpFormI ;
      private Guid[] P00EH5_A587DynamicFormTranslationId ;
      private string aP3_OptionsJson ;
      private string aP4_OptionsDescJson ;
      private string aP5_OptionIndexesJson ;
   }

   public class trn_dynamicformtranslationwwgetfilterdata__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P00EH2( IGxContext context ,
                                             string AV50Trn_dynamicformtranslationwwds_1_filterfulltext ,
                                             int AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid ,
                                             int AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to ,
                                             int AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber ,
                                             int AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to ,
                                             int AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid ,
                                             int AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to ,
                                             string AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel ,
                                             string AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname ,
                                             string AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel ,
                                             string AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename ,
                                             string AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel ,
                                             string AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish ,
                                             string AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel ,
                                             string AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch ,
                                             int A588DynamicFormTranslationWWpFormI ,
                                             int A589DynamicFormTranslationWWPFormV ,
                                             int A590DynamicFormTranslationWWPFormE ,
                                             string A591DynamicFormTranslationTrnName ,
                                             string A592DynamicFormTranslationAttribut ,
                                             string A593DynamicFormTranslationEnglish ,
                                             string A594DynamicFormTranslationDutch )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int1 = new short[21];
         Object[] GXv_Object2 = new Object[2];
         scmdbuf = "SELECT DynamicFormTranslationTrnName, DynamicFormTranslationDutch, DynamicFormTranslationEnglish, DynamicFormTranslationAttribut, DynamicFormTranslationWWPFormE, DynamicFormTranslationWWPFormV, DynamicFormTranslationWWpFormI, DynamicFormTranslationId FROM Trn_DynamicFormTranslation";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( SUBSTR(TO_CHAR(DynamicFormTranslationWWpFormI,'999999'), 2) like '%' || :lV50Trn_dynamicformtranslationwwds_1_filterfulltext) or ( SUBSTR(TO_CHAR(DynamicFormTranslationWWPFormV,'999999'), 2) like '%' || :lV50Trn_dynamicformtranslationwwds_1_filterfulltext) or ( SUBSTR(TO_CHAR(DynamicFormTranslationWWPFormE,'999999'), 2) like '%' || :lV50Trn_dynamicformtranslationwwds_1_filterfulltext) or ( LOWER(DynamicFormTranslationTrnName) like '%' || LOWER(:lV50Trn_dynamicformtranslationwwds_1_filterfulltext)) or ( LOWER(DynamicFormTranslationAttribut) like '%' || LOWER(:lV50Trn_dynamicformtranslationwwds_1_filterfulltext)) or ( LOWER(DynamicFormTranslationEnglish) like '%' || LOWER(:lV50Trn_dynamicformtranslationwwds_1_filterfulltext)) or ( LOWER(DynamicFormTranslationDutch) like '%' || LOWER(:lV50Trn_dynamicformtranslationwwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int1[0] = 1;
            GXv_int1[1] = 1;
            GXv_int1[2] = 1;
            GXv_int1[3] = 1;
            GXv_int1[4] = 1;
            GXv_int1[5] = 1;
            GXv_int1[6] = 1;
         }
         if ( ! (0==AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWpFormI >= :AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int1[7] = 1;
         }
         if ( ! (0==AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWpFormI <= :AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int1[8] = 1;
         }
         if ( ! (0==AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWPFormV >= :AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int1[9] = 1;
         }
         if ( ! (0==AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWPFormV <= :AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int1[10] = 1;
         }
         if ( ! (0==AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWPFormE >= :AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int1[11] = 1;
         }
         if ( ! (0==AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWPFormE <= :AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int1[12] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname)) ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationTrnName like :lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtr)");
         }
         else
         {
            GXv_int1[13] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel)) && ! ( StringUtil.StrCmp(AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationTrnName = ( :AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtr))");
         }
         else
         {
            GXv_int1[14] = 1;
         }
         if ( StringUtil.StrCmp(AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from DynamicFormTranslationTrnName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename)) ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationAttribut like :lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationa)");
         }
         else
         {
            GXv_int1[15] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel)) && ! ( StringUtil.StrCmp(AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationAttribut = ( :AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationa))");
         }
         else
         {
            GXv_int1[16] = 1;
         }
         if ( StringUtil.StrCmp(AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from DynamicFormTranslationAttribut))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish)) ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationEnglish like :lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslatione)");
         }
         else
         {
            GXv_int1[17] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel)) && ! ( StringUtil.StrCmp(AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationEnglish = ( :AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslatione))");
         }
         else
         {
            GXv_int1[18] = 1;
         }
         if ( StringUtil.StrCmp(AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from DynamicFormTranslationEnglish))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch)) ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationDutch like :lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationd)");
         }
         else
         {
            GXv_int1[19] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel)) && ! ( StringUtil.StrCmp(AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationDutch = ( :AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationd))");
         }
         else
         {
            GXv_int1[20] = 1;
         }
         if ( StringUtil.StrCmp(AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from DynamicFormTranslationDutch))=0))");
         }
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY DynamicFormTranslationTrnName";
         GXv_Object2[0] = scmdbuf;
         GXv_Object2[1] = GXv_int1;
         return GXv_Object2 ;
      }

      protected Object[] conditional_P00EH3( IGxContext context ,
                                             string AV50Trn_dynamicformtranslationwwds_1_filterfulltext ,
                                             int AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid ,
                                             int AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to ,
                                             int AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber ,
                                             int AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to ,
                                             int AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid ,
                                             int AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to ,
                                             string AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel ,
                                             string AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname ,
                                             string AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel ,
                                             string AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename ,
                                             string AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel ,
                                             string AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish ,
                                             string AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel ,
                                             string AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch ,
                                             int A588DynamicFormTranslationWWpFormI ,
                                             int A589DynamicFormTranslationWWPFormV ,
                                             int A590DynamicFormTranslationWWPFormE ,
                                             string A591DynamicFormTranslationTrnName ,
                                             string A592DynamicFormTranslationAttribut ,
                                             string A593DynamicFormTranslationEnglish ,
                                             string A594DynamicFormTranslationDutch )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int3 = new short[21];
         Object[] GXv_Object4 = new Object[2];
         scmdbuf = "SELECT DynamicFormTranslationAttribut, DynamicFormTranslationDutch, DynamicFormTranslationEnglish, DynamicFormTranslationTrnName, DynamicFormTranslationWWPFormE, DynamicFormTranslationWWPFormV, DynamicFormTranslationWWpFormI, DynamicFormTranslationId FROM Trn_DynamicFormTranslation";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( SUBSTR(TO_CHAR(DynamicFormTranslationWWpFormI,'999999'), 2) like '%' || :lV50Trn_dynamicformtranslationwwds_1_filterfulltext) or ( SUBSTR(TO_CHAR(DynamicFormTranslationWWPFormV,'999999'), 2) like '%' || :lV50Trn_dynamicformtranslationwwds_1_filterfulltext) or ( SUBSTR(TO_CHAR(DynamicFormTranslationWWPFormE,'999999'), 2) like '%' || :lV50Trn_dynamicformtranslationwwds_1_filterfulltext) or ( LOWER(DynamicFormTranslationTrnName) like '%' || LOWER(:lV50Trn_dynamicformtranslationwwds_1_filterfulltext)) or ( LOWER(DynamicFormTranslationAttribut) like '%' || LOWER(:lV50Trn_dynamicformtranslationwwds_1_filterfulltext)) or ( LOWER(DynamicFormTranslationEnglish) like '%' || LOWER(:lV50Trn_dynamicformtranslationwwds_1_filterfulltext)) or ( LOWER(DynamicFormTranslationDutch) like '%' || LOWER(:lV50Trn_dynamicformtranslationwwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int3[0] = 1;
            GXv_int3[1] = 1;
            GXv_int3[2] = 1;
            GXv_int3[3] = 1;
            GXv_int3[4] = 1;
            GXv_int3[5] = 1;
            GXv_int3[6] = 1;
         }
         if ( ! (0==AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWpFormI >= :AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int3[7] = 1;
         }
         if ( ! (0==AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWpFormI <= :AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int3[8] = 1;
         }
         if ( ! (0==AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWPFormV >= :AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int3[9] = 1;
         }
         if ( ! (0==AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWPFormV <= :AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int3[10] = 1;
         }
         if ( ! (0==AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWPFormE >= :AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int3[11] = 1;
         }
         if ( ! (0==AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWPFormE <= :AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int3[12] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname)) ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationTrnName like :lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtr)");
         }
         else
         {
            GXv_int3[13] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel)) && ! ( StringUtil.StrCmp(AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationTrnName = ( :AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtr))");
         }
         else
         {
            GXv_int3[14] = 1;
         }
         if ( StringUtil.StrCmp(AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from DynamicFormTranslationTrnName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename)) ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationAttribut like :lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationa)");
         }
         else
         {
            GXv_int3[15] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel)) && ! ( StringUtil.StrCmp(AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationAttribut = ( :AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationa))");
         }
         else
         {
            GXv_int3[16] = 1;
         }
         if ( StringUtil.StrCmp(AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from DynamicFormTranslationAttribut))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish)) ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationEnglish like :lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslatione)");
         }
         else
         {
            GXv_int3[17] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel)) && ! ( StringUtil.StrCmp(AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationEnglish = ( :AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslatione))");
         }
         else
         {
            GXv_int3[18] = 1;
         }
         if ( StringUtil.StrCmp(AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from DynamicFormTranslationEnglish))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch)) ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationDutch like :lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationd)");
         }
         else
         {
            GXv_int3[19] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel)) && ! ( StringUtil.StrCmp(AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationDutch = ( :AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationd))");
         }
         else
         {
            GXv_int3[20] = 1;
         }
         if ( StringUtil.StrCmp(AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from DynamicFormTranslationDutch))=0))");
         }
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY DynamicFormTranslationAttribut";
         GXv_Object4[0] = scmdbuf;
         GXv_Object4[1] = GXv_int3;
         return GXv_Object4 ;
      }

      protected Object[] conditional_P00EH4( IGxContext context ,
                                             string AV50Trn_dynamicformtranslationwwds_1_filterfulltext ,
                                             int AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid ,
                                             int AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to ,
                                             int AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber ,
                                             int AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to ,
                                             int AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid ,
                                             int AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to ,
                                             string AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel ,
                                             string AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname ,
                                             string AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel ,
                                             string AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename ,
                                             string AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel ,
                                             string AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish ,
                                             string AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel ,
                                             string AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch ,
                                             int A588DynamicFormTranslationWWpFormI ,
                                             int A589DynamicFormTranslationWWPFormV ,
                                             int A590DynamicFormTranslationWWPFormE ,
                                             string A591DynamicFormTranslationTrnName ,
                                             string A592DynamicFormTranslationAttribut ,
                                             string A593DynamicFormTranslationEnglish ,
                                             string A594DynamicFormTranslationDutch )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int5 = new short[21];
         Object[] GXv_Object6 = new Object[2];
         scmdbuf = "SELECT DynamicFormTranslationEnglish, DynamicFormTranslationDutch, DynamicFormTranslationAttribut, DynamicFormTranslationTrnName, DynamicFormTranslationWWPFormE, DynamicFormTranslationWWPFormV, DynamicFormTranslationWWpFormI, DynamicFormTranslationId FROM Trn_DynamicFormTranslation";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( SUBSTR(TO_CHAR(DynamicFormTranslationWWpFormI,'999999'), 2) like '%' || :lV50Trn_dynamicformtranslationwwds_1_filterfulltext) or ( SUBSTR(TO_CHAR(DynamicFormTranslationWWPFormV,'999999'), 2) like '%' || :lV50Trn_dynamicformtranslationwwds_1_filterfulltext) or ( SUBSTR(TO_CHAR(DynamicFormTranslationWWPFormE,'999999'), 2) like '%' || :lV50Trn_dynamicformtranslationwwds_1_filterfulltext) or ( LOWER(DynamicFormTranslationTrnName) like '%' || LOWER(:lV50Trn_dynamicformtranslationwwds_1_filterfulltext)) or ( LOWER(DynamicFormTranslationAttribut) like '%' || LOWER(:lV50Trn_dynamicformtranslationwwds_1_filterfulltext)) or ( LOWER(DynamicFormTranslationEnglish) like '%' || LOWER(:lV50Trn_dynamicformtranslationwwds_1_filterfulltext)) or ( LOWER(DynamicFormTranslationDutch) like '%' || LOWER(:lV50Trn_dynamicformtranslationwwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int5[0] = 1;
            GXv_int5[1] = 1;
            GXv_int5[2] = 1;
            GXv_int5[3] = 1;
            GXv_int5[4] = 1;
            GXv_int5[5] = 1;
            GXv_int5[6] = 1;
         }
         if ( ! (0==AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWpFormI >= :AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int5[7] = 1;
         }
         if ( ! (0==AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWpFormI <= :AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int5[8] = 1;
         }
         if ( ! (0==AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWPFormV >= :AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int5[9] = 1;
         }
         if ( ! (0==AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWPFormV <= :AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int5[10] = 1;
         }
         if ( ! (0==AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWPFormE >= :AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int5[11] = 1;
         }
         if ( ! (0==AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWPFormE <= :AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int5[12] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname)) ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationTrnName like :lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtr)");
         }
         else
         {
            GXv_int5[13] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel)) && ! ( StringUtil.StrCmp(AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationTrnName = ( :AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtr))");
         }
         else
         {
            GXv_int5[14] = 1;
         }
         if ( StringUtil.StrCmp(AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from DynamicFormTranslationTrnName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename)) ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationAttribut like :lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationa)");
         }
         else
         {
            GXv_int5[15] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel)) && ! ( StringUtil.StrCmp(AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationAttribut = ( :AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationa))");
         }
         else
         {
            GXv_int5[16] = 1;
         }
         if ( StringUtil.StrCmp(AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from DynamicFormTranslationAttribut))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish)) ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationEnglish like :lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslatione)");
         }
         else
         {
            GXv_int5[17] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel)) && ! ( StringUtil.StrCmp(AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationEnglish = ( :AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslatione))");
         }
         else
         {
            GXv_int5[18] = 1;
         }
         if ( StringUtil.StrCmp(AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from DynamicFormTranslationEnglish))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch)) ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationDutch like :lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationd)");
         }
         else
         {
            GXv_int5[19] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel)) && ! ( StringUtil.StrCmp(AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationDutch = ( :AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationd))");
         }
         else
         {
            GXv_int5[20] = 1;
         }
         if ( StringUtil.StrCmp(AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from DynamicFormTranslationDutch))=0))");
         }
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY DynamicFormTranslationEnglish";
         GXv_Object6[0] = scmdbuf;
         GXv_Object6[1] = GXv_int5;
         return GXv_Object6 ;
      }

      protected Object[] conditional_P00EH5( IGxContext context ,
                                             string AV50Trn_dynamicformtranslationwwds_1_filterfulltext ,
                                             int AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid ,
                                             int AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to ,
                                             int AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber ,
                                             int AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to ,
                                             int AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid ,
                                             int AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to ,
                                             string AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel ,
                                             string AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname ,
                                             string AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel ,
                                             string AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename ,
                                             string AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel ,
                                             string AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish ,
                                             string AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel ,
                                             string AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch ,
                                             int A588DynamicFormTranslationWWpFormI ,
                                             int A589DynamicFormTranslationWWPFormV ,
                                             int A590DynamicFormTranslationWWPFormE ,
                                             string A591DynamicFormTranslationTrnName ,
                                             string A592DynamicFormTranslationAttribut ,
                                             string A593DynamicFormTranslationEnglish ,
                                             string A594DynamicFormTranslationDutch )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int7 = new short[21];
         Object[] GXv_Object8 = new Object[2];
         scmdbuf = "SELECT DynamicFormTranslationDutch, DynamicFormTranslationEnglish, DynamicFormTranslationAttribut, DynamicFormTranslationTrnName, DynamicFormTranslationWWPFormE, DynamicFormTranslationWWPFormV, DynamicFormTranslationWWpFormI, DynamicFormTranslationId FROM Trn_DynamicFormTranslation";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV50Trn_dynamicformtranslationwwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( SUBSTR(TO_CHAR(DynamicFormTranslationWWpFormI,'999999'), 2) like '%' || :lV50Trn_dynamicformtranslationwwds_1_filterfulltext) or ( SUBSTR(TO_CHAR(DynamicFormTranslationWWPFormV,'999999'), 2) like '%' || :lV50Trn_dynamicformtranslationwwds_1_filterfulltext) or ( SUBSTR(TO_CHAR(DynamicFormTranslationWWPFormE,'999999'), 2) like '%' || :lV50Trn_dynamicformtranslationwwds_1_filterfulltext) or ( LOWER(DynamicFormTranslationTrnName) like '%' || LOWER(:lV50Trn_dynamicformtranslationwwds_1_filterfulltext)) or ( LOWER(DynamicFormTranslationAttribut) like '%' || LOWER(:lV50Trn_dynamicformtranslationwwds_1_filterfulltext)) or ( LOWER(DynamicFormTranslationEnglish) like '%' || LOWER(:lV50Trn_dynamicformtranslationwwds_1_filterfulltext)) or ( LOWER(DynamicFormTranslationDutch) like '%' || LOWER(:lV50Trn_dynamicformtranslationwwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int7[0] = 1;
            GXv_int7[1] = 1;
            GXv_int7[2] = 1;
            GXv_int7[3] = 1;
            GXv_int7[4] = 1;
            GXv_int7[5] = 1;
            GXv_int7[6] = 1;
         }
         if ( ! (0==AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationwwpformid) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWpFormI >= :AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int7[7] = 1;
         }
         if ( ! (0==AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationwwpformid_to) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWpFormI <= :AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int7[8] = 1;
         }
         if ( ! (0==AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationwwpformversionnumber) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWPFormV >= :AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int7[9] = 1;
         }
         if ( ! (0==AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationwwpformversionnumber_to) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWPFormV <= :AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int7[10] = 1;
         }
         if ( ! (0==AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationwwpformelementid) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWPFormE >= :AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int7[11] = 1;
         }
         if ( ! (0==AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationwwpformelementid_to) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationWWPFormE <= :AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationww)");
         }
         else
         {
            GXv_int7[12] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtrnname)) ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationTrnName like :lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtr)");
         }
         else
         {
            GXv_int7[13] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel)) && ! ( StringUtil.StrCmp(AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationTrnName = ( :AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtr))");
         }
         else
         {
            GXv_int7[14] = 1;
         }
         if ( StringUtil.StrCmp(AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtrnname_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from DynamicFormTranslationTrnName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationattributename)) ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationAttribut like :lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationa)");
         }
         else
         {
            GXv_int7[15] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel)) && ! ( StringUtil.StrCmp(AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationAttribut = ( :AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationa))");
         }
         else
         {
            GXv_int7[16] = 1;
         }
         if ( StringUtil.StrCmp(AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationattributename_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from DynamicFormTranslationAttribut))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslationenglish)) ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationEnglish like :lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslatione)");
         }
         else
         {
            GXv_int7[17] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel)) && ! ( StringUtil.StrCmp(AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationEnglish = ( :AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslatione))");
         }
         else
         {
            GXv_int7[18] = 1;
         }
         if ( StringUtil.StrCmp(AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslationenglish_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from DynamicFormTranslationEnglish))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationdutch)) ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationDutch like :lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationd)");
         }
         else
         {
            GXv_int7[19] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel)) && ! ( StringUtil.StrCmp(AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(DynamicFormTranslationDutch = ( :AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationd))");
         }
         else
         {
            GXv_int7[20] = 1;
         }
         if ( StringUtil.StrCmp(AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationdutch_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from DynamicFormTranslationDutch))=0))");
         }
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY DynamicFormTranslationDutch";
         GXv_Object8[0] = scmdbuf;
         GXv_Object8[1] = GXv_int7;
         return GXv_Object8 ;
      }

      public override Object [] getDynamicStatement( int cursor ,
                                                     IGxContext context ,
                                                     Object [] dynConstraints )
      {
         switch ( cursor )
         {
               case 0 :
                     return conditional_P00EH2(context, (string)dynConstraints[0] , (int)dynConstraints[1] , (int)dynConstraints[2] , (int)dynConstraints[3] , (int)dynConstraints[4] , (int)dynConstraints[5] , (int)dynConstraints[6] , (string)dynConstraints[7] , (string)dynConstraints[8] , (string)dynConstraints[9] , (string)dynConstraints[10] , (string)dynConstraints[11] , (string)dynConstraints[12] , (string)dynConstraints[13] , (string)dynConstraints[14] , (int)dynConstraints[15] , (int)dynConstraints[16] , (int)dynConstraints[17] , (string)dynConstraints[18] , (string)dynConstraints[19] , (string)dynConstraints[20] , (string)dynConstraints[21] );
               case 1 :
                     return conditional_P00EH3(context, (string)dynConstraints[0] , (int)dynConstraints[1] , (int)dynConstraints[2] , (int)dynConstraints[3] , (int)dynConstraints[4] , (int)dynConstraints[5] , (int)dynConstraints[6] , (string)dynConstraints[7] , (string)dynConstraints[8] , (string)dynConstraints[9] , (string)dynConstraints[10] , (string)dynConstraints[11] , (string)dynConstraints[12] , (string)dynConstraints[13] , (string)dynConstraints[14] , (int)dynConstraints[15] , (int)dynConstraints[16] , (int)dynConstraints[17] , (string)dynConstraints[18] , (string)dynConstraints[19] , (string)dynConstraints[20] , (string)dynConstraints[21] );
               case 2 :
                     return conditional_P00EH4(context, (string)dynConstraints[0] , (int)dynConstraints[1] , (int)dynConstraints[2] , (int)dynConstraints[3] , (int)dynConstraints[4] , (int)dynConstraints[5] , (int)dynConstraints[6] , (string)dynConstraints[7] , (string)dynConstraints[8] , (string)dynConstraints[9] , (string)dynConstraints[10] , (string)dynConstraints[11] , (string)dynConstraints[12] , (string)dynConstraints[13] , (string)dynConstraints[14] , (int)dynConstraints[15] , (int)dynConstraints[16] , (int)dynConstraints[17] , (string)dynConstraints[18] , (string)dynConstraints[19] , (string)dynConstraints[20] , (string)dynConstraints[21] );
               case 3 :
                     return conditional_P00EH5(context, (string)dynConstraints[0] , (int)dynConstraints[1] , (int)dynConstraints[2] , (int)dynConstraints[3] , (int)dynConstraints[4] , (int)dynConstraints[5] , (int)dynConstraints[6] , (string)dynConstraints[7] , (string)dynConstraints[8] , (string)dynConstraints[9] , (string)dynConstraints[10] , (string)dynConstraints[11] , (string)dynConstraints[12] , (string)dynConstraints[13] , (string)dynConstraints[14] , (int)dynConstraints[15] , (int)dynConstraints[16] , (int)dynConstraints[17] , (string)dynConstraints[18] , (string)dynConstraints[19] , (string)dynConstraints[20] , (string)dynConstraints[21] );
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
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP00EH2;
          prmP00EH2 = new Object[] {
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtr",GXType.VarChar,400,0) ,
          new ParDef("AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtr",GXType.VarChar,400,0) ,
          new ParDef("lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationa",GXType.VarChar,40,0) ,
          new ParDef("AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationa",GXType.VarChar,40,0) ,
          new ParDef("lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslatione",GXType.VarChar,200,0) ,
          new ParDef("AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslatione",GXType.VarChar,200,0) ,
          new ParDef("lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationd",GXType.VarChar,200,0) ,
          new ParDef("AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationd",GXType.VarChar,200,0)
          };
          Object[] prmP00EH3;
          prmP00EH3 = new Object[] {
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtr",GXType.VarChar,400,0) ,
          new ParDef("AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtr",GXType.VarChar,400,0) ,
          new ParDef("lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationa",GXType.VarChar,40,0) ,
          new ParDef("AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationa",GXType.VarChar,40,0) ,
          new ParDef("lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslatione",GXType.VarChar,200,0) ,
          new ParDef("AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslatione",GXType.VarChar,200,0) ,
          new ParDef("lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationd",GXType.VarChar,200,0) ,
          new ParDef("AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationd",GXType.VarChar,200,0)
          };
          Object[] prmP00EH4;
          prmP00EH4 = new Object[] {
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtr",GXType.VarChar,400,0) ,
          new ParDef("AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtr",GXType.VarChar,400,0) ,
          new ParDef("lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationa",GXType.VarChar,40,0) ,
          new ParDef("AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationa",GXType.VarChar,40,0) ,
          new ParDef("lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslatione",GXType.VarChar,200,0) ,
          new ParDef("AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslatione",GXType.VarChar,200,0) ,
          new ParDef("lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationd",GXType.VarChar,200,0) ,
          new ParDef("AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationd",GXType.VarChar,200,0)
          };
          Object[] prmP00EH5;
          prmP00EH5 = new Object[] {
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV50Trn_dynamicformtranslationwwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("AV51Trn_dynamicformtranslationwwds_2_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV52Trn_dynamicformtranslationwwds_3_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV53Trn_dynamicformtranslationwwds_4_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV54Trn_dynamicformtranslationwwds_5_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV55Trn_dynamicformtranslationwwds_6_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("AV56Trn_dynamicformtranslationwwds_7_tfdynamicformtranslationww",GXType.Int32,6,0) ,
          new ParDef("lV57Trn_dynamicformtranslationwwds_8_tfdynamicformtranslationtr",GXType.VarChar,400,0) ,
          new ParDef("AV58Trn_dynamicformtranslationwwds_9_tfdynamicformtranslationtr",GXType.VarChar,400,0) ,
          new ParDef("lV59Trn_dynamicformtranslationwwds_10_tfdynamicformtranslationa",GXType.VarChar,40,0) ,
          new ParDef("AV60Trn_dynamicformtranslationwwds_11_tfdynamicformtranslationa",GXType.VarChar,40,0) ,
          new ParDef("lV61Trn_dynamicformtranslationwwds_12_tfdynamicformtranslatione",GXType.VarChar,200,0) ,
          new ParDef("AV62Trn_dynamicformtranslationwwds_13_tfdynamicformtranslatione",GXType.VarChar,200,0) ,
          new ParDef("lV63Trn_dynamicformtranslationwwds_14_tfdynamicformtranslationd",GXType.VarChar,200,0) ,
          new ParDef("AV64Trn_dynamicformtranslationwwds_15_tfdynamicformtranslationd",GXType.VarChar,200,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00EH2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00EH2,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00EH3", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00EH3,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00EH4", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00EH4,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00EH5", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00EH5,100, GxCacheFrequency.OFF ,true,false )
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
                ((string[]) buf[0])[0] = rslt.getVarchar(1);
                ((string[]) buf[1])[0] = rslt.getLongVarchar(2);
                ((string[]) buf[2])[0] = rslt.getLongVarchar(3);
                ((string[]) buf[3])[0] = rslt.getVarchar(4);
                ((int[]) buf[4])[0] = rslt.getInt(5);
                ((int[]) buf[5])[0] = rslt.getInt(6);
                ((int[]) buf[6])[0] = rslt.getInt(7);
                ((Guid[]) buf[7])[0] = rslt.getGuid(8);
                return;
             case 1 :
                ((string[]) buf[0])[0] = rslt.getVarchar(1);
                ((string[]) buf[1])[0] = rslt.getLongVarchar(2);
                ((string[]) buf[2])[0] = rslt.getLongVarchar(3);
                ((string[]) buf[3])[0] = rslt.getVarchar(4);
                ((int[]) buf[4])[0] = rslt.getInt(5);
                ((int[]) buf[5])[0] = rslt.getInt(6);
                ((int[]) buf[6])[0] = rslt.getInt(7);
                ((Guid[]) buf[7])[0] = rslt.getGuid(8);
                return;
             case 2 :
                ((string[]) buf[0])[0] = rslt.getLongVarchar(1);
                ((string[]) buf[1])[0] = rslt.getLongVarchar(2);
                ((string[]) buf[2])[0] = rslt.getVarchar(3);
                ((string[]) buf[3])[0] = rslt.getVarchar(4);
                ((int[]) buf[4])[0] = rslt.getInt(5);
                ((int[]) buf[5])[0] = rslt.getInt(6);
                ((int[]) buf[6])[0] = rslt.getInt(7);
                ((Guid[]) buf[7])[0] = rslt.getGuid(8);
                return;
             case 3 :
                ((string[]) buf[0])[0] = rslt.getLongVarchar(1);
                ((string[]) buf[1])[0] = rslt.getLongVarchar(2);
                ((string[]) buf[2])[0] = rslt.getVarchar(3);
                ((string[]) buf[3])[0] = rslt.getVarchar(4);
                ((int[]) buf[4])[0] = rslt.getInt(5);
                ((int[]) buf[5])[0] = rslt.getInt(6);
                ((int[]) buf[6])[0] = rslt.getInt(7);
                ((Guid[]) buf[7])[0] = rslt.getGuid(8);
                return;
       }
    }

 }

}
