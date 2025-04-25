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
   public class trn_memowwgetfilterdata : GXProcedure
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
            return "trn_memoww_Services_Execute" ;
         }

      }

      public trn_memowwgetfilterdata( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public trn_memowwgetfilterdata( IGxContext context )
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
         this.AV39DDOName = aP0_DDOName;
         this.AV40SearchTxtParms = aP1_SearchTxtParms;
         this.AV41SearchTxtTo = aP2_SearchTxtTo;
         this.AV42OptionsJson = "" ;
         this.AV43OptionsDescJson = "" ;
         this.AV44OptionIndexesJson = "" ;
         initialize();
         ExecuteImpl();
         aP3_OptionsJson=this.AV42OptionsJson;
         aP4_OptionsDescJson=this.AV43OptionsDescJson;
         aP5_OptionIndexesJson=this.AV44OptionIndexesJson;
      }

      public string executeUdp( string aP0_DDOName ,
                                string aP1_SearchTxtParms ,
                                string aP2_SearchTxtTo ,
                                out string aP3_OptionsJson ,
                                out string aP4_OptionsDescJson )
      {
         execute(aP0_DDOName, aP1_SearchTxtParms, aP2_SearchTxtTo, out aP3_OptionsJson, out aP4_OptionsDescJson, out aP5_OptionIndexesJson);
         return AV44OptionIndexesJson ;
      }

      public void executeSubmit( string aP0_DDOName ,
                                 string aP1_SearchTxtParms ,
                                 string aP2_SearchTxtTo ,
                                 out string aP3_OptionsJson ,
                                 out string aP4_OptionsDescJson ,
                                 out string aP5_OptionIndexesJson )
      {
         this.AV39DDOName = aP0_DDOName;
         this.AV40SearchTxtParms = aP1_SearchTxtParms;
         this.AV41SearchTxtTo = aP2_SearchTxtTo;
         this.AV42OptionsJson = "" ;
         this.AV43OptionsDescJson = "" ;
         this.AV44OptionIndexesJson = "" ;
         SubmitImpl();
         aP3_OptionsJson=this.AV42OptionsJson;
         aP4_OptionsDescJson=this.AV43OptionsDescJson;
         aP5_OptionIndexesJson=this.AV44OptionIndexesJson;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV29Options = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV31OptionsDesc = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV32OptionIndexes = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV26MaxItems = 10;
         AV25PageIndex = (short)((String.IsNullOrEmpty(StringUtil.RTrim( AV40SearchTxtParms)) ? 0 : (long)(Math.Round(NumberUtil.Val( StringUtil.Substring( AV40SearchTxtParms, 1, 2), "."), 18, MidpointRounding.ToEven))));
         AV23SearchTxt = (String.IsNullOrEmpty(StringUtil.RTrim( AV40SearchTxtParms)) ? "" : StringUtil.Substring( AV40SearchTxtParms, 3, -1));
         AV24SkipItems = (short)(AV25PageIndex*AV26MaxItems);
         new GeneXus.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV9WWPContext) ;
         /* Execute user subroutine: 'LOADGRIDSTATE' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         if ( StringUtil.StrCmp(StringUtil.Upper( AV39DDOName), "DDO_MEMOCATEGORYNAME") == 0 )
         {
            /* Execute user subroutine: 'LOADMEMOCATEGORYNAMEOPTIONS' */
            S121 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         else if ( StringUtil.StrCmp(StringUtil.Upper( AV39DDOName), "DDO_MEMOTITLE") == 0 )
         {
            /* Execute user subroutine: 'LOADMEMOTITLEOPTIONS' */
            S131 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         AV42OptionsJson = AV29Options.ToJSonString(false);
         AV43OptionsDescJson = AV31OptionsDesc.ToJSonString(false);
         AV44OptionIndexesJson = AV32OptionIndexes.ToJSonString(false);
         cleanup();
      }

      protected void S111( )
      {
         /* 'LOADGRIDSTATE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV34Session.Get("Trn_MemoWWGridState"), "") == 0 )
         {
            AV36GridState.FromXml(new WorkWithPlus.workwithplus_web.loadgridstate(context).executeUdp(  "Trn_MemoWWGridState"), null, "", "");
         }
         else
         {
            AV36GridState.FromXml(AV34Session.Get("Trn_MemoWWGridState"), null, "", "");
         }
         AV46GXV1 = 1;
         while ( AV46GXV1 <= AV36GridState.gxTpr_Filtervalues.Count )
         {
            AV37GridStateFilterValue = ((WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue)AV36GridState.gxTpr_Filtervalues.Item(AV46GXV1));
            if ( StringUtil.StrCmp(AV37GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV45FilterFullText = AV37GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV37GridStateFilterValue.gxTpr_Name, "TFMEMOCATEGORYNAME") == 0 )
            {
               AV11TFMemoCategoryName = AV37GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV37GridStateFilterValue.gxTpr_Name, "TFMEMOCATEGORYNAME_SEL") == 0 )
            {
               AV12TFMemoCategoryName_Sel = AV37GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV37GridStateFilterValue.gxTpr_Name, "TFMEMOTITLE") == 0 )
            {
               AV13TFMemoTitle = AV37GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV37GridStateFilterValue.gxTpr_Name, "TFMEMOTITLE_SEL") == 0 )
            {
               AV14TFMemoTitle_Sel = AV37GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV37GridStateFilterValue.gxTpr_Name, "TFMEMOSTARTDATETIME") == 0 )
            {
               AV15TFMemoStartDateTime = context.localUtil.CToT( AV37GridStateFilterValue.gxTpr_Value, 2);
               AV16TFMemoStartDateTime_To = context.localUtil.CToT( AV37GridStateFilterValue.gxTpr_Valueto, 2);
            }
            else if ( StringUtil.StrCmp(AV37GridStateFilterValue.gxTpr_Name, "TFMEMOENDDATETIME") == 0 )
            {
               AV17TFMemoEndDateTime = context.localUtil.CToT( AV37GridStateFilterValue.gxTpr_Value, 2);
               AV18TFMemoEndDateTime_To = context.localUtil.CToT( AV37GridStateFilterValue.gxTpr_Valueto, 2);
            }
            else if ( StringUtil.StrCmp(AV37GridStateFilterValue.gxTpr_Name, "TFMEMODURATION") == 0 )
            {
               AV19TFMemoDuration = (short)(Math.Round(NumberUtil.Val( AV37GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV20TFMemoDuration_To = (short)(Math.Round(NumberUtil.Val( AV37GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV37GridStateFilterValue.gxTpr_Name, "TFMEMOREMOVEDATE") == 0 )
            {
               AV21TFMemoRemoveDate = context.localUtil.CToD( AV37GridStateFilterValue.gxTpr_Value, 2);
               AV22TFMemoRemoveDate_To = context.localUtil.CToD( AV37GridStateFilterValue.gxTpr_Valueto, 2);
            }
            AV46GXV1 = (int)(AV46GXV1+1);
         }
      }

      protected void S121( )
      {
         /* 'LOADMEMOCATEGORYNAMEOPTIONS' Routine */
         returnInSub = false;
         AV11TFMemoCategoryName = AV23SearchTxt;
         AV12TFMemoCategoryName_Sel = "";
         AV48Trn_memowwds_1_filterfulltext = AV45FilterFullText;
         AV49Trn_memowwds_2_tfmemocategoryname = AV11TFMemoCategoryName;
         AV50Trn_memowwds_3_tfmemocategoryname_sel = AV12TFMemoCategoryName_Sel;
         AV51Trn_memowwds_4_tfmemotitle = AV13TFMemoTitle;
         AV52Trn_memowwds_5_tfmemotitle_sel = AV14TFMemoTitle_Sel;
         AV53Trn_memowwds_6_tfmemostartdatetime = AV15TFMemoStartDateTime;
         AV54Trn_memowwds_7_tfmemostartdatetime_to = AV16TFMemoStartDateTime_To;
         AV55Trn_memowwds_8_tfmemoenddatetime = AV17TFMemoEndDateTime;
         AV56Trn_memowwds_9_tfmemoenddatetime_to = AV18TFMemoEndDateTime_To;
         AV57Trn_memowwds_10_tfmemoduration = AV19TFMemoDuration;
         AV58Trn_memowwds_11_tfmemoduration_to = AV20TFMemoDuration_To;
         AV59Trn_memowwds_12_tfmemoremovedate = AV21TFMemoRemoveDate;
         AV60Trn_memowwds_13_tfmemoremovedate_to = AV22TFMemoRemoveDate_To;
         pr_default.dynParam(0, new Object[]{ new Object[]{
                                              AV48Trn_memowwds_1_filterfulltext ,
                                              AV50Trn_memowwds_3_tfmemocategoryname_sel ,
                                              AV49Trn_memowwds_2_tfmemocategoryname ,
                                              AV52Trn_memowwds_5_tfmemotitle_sel ,
                                              AV51Trn_memowwds_4_tfmemotitle ,
                                              AV53Trn_memowwds_6_tfmemostartdatetime ,
                                              AV54Trn_memowwds_7_tfmemostartdatetime_to ,
                                              AV55Trn_memowwds_8_tfmemoenddatetime ,
                                              AV56Trn_memowwds_9_tfmemoenddatetime_to ,
                                              AV57Trn_memowwds_10_tfmemoduration ,
                                              AV58Trn_memowwds_11_tfmemoduration_to ,
                                              AV59Trn_memowwds_12_tfmemoremovedate ,
                                              AV60Trn_memowwds_13_tfmemoremovedate_to ,
                                              A543MemoCategoryName ,
                                              A550MemoTitle ,
                                              A563MemoDuration ,
                                              A561MemoStartDateTime ,
                                              A562MemoEndDateTime ,
                                              A564MemoRemoveDate } ,
                                              new int[]{
                                              TypeConstants.DATE, TypeConstants.DATE, TypeConstants.DATE, TypeConstants.DATE, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.DATE, TypeConstants.DATE, TypeConstants.SHORT, TypeConstants.BOOLEAN,
                                              TypeConstants.DATE, TypeConstants.BOOLEAN, TypeConstants.DATE, TypeConstants.BOOLEAN, TypeConstants.DATE
                                              }
         });
         lV48Trn_memowwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV48Trn_memowwds_1_filterfulltext), "%", "");
         lV48Trn_memowwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV48Trn_memowwds_1_filterfulltext), "%", "");
         lV48Trn_memowwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV48Trn_memowwds_1_filterfulltext), "%", "");
         lV49Trn_memowwds_2_tfmemocategoryname = StringUtil.Concat( StringUtil.RTrim( AV49Trn_memowwds_2_tfmemocategoryname), "%", "");
         lV51Trn_memowwds_4_tfmemotitle = StringUtil.Concat( StringUtil.RTrim( AV51Trn_memowwds_4_tfmemotitle), "%", "");
         /* Using cursor P00CJ2 */
         pr_default.execute(0, new Object[] {lV48Trn_memowwds_1_filterfulltext, lV48Trn_memowwds_1_filterfulltext, lV48Trn_memowwds_1_filterfulltext, lV49Trn_memowwds_2_tfmemocategoryname, AV50Trn_memowwds_3_tfmemocategoryname_sel, lV51Trn_memowwds_4_tfmemotitle, AV52Trn_memowwds_5_tfmemotitle_sel, AV53Trn_memowwds_6_tfmemostartdatetime, AV54Trn_memowwds_7_tfmemostartdatetime_to, AV55Trn_memowwds_8_tfmemoenddatetime, AV56Trn_memowwds_9_tfmemoenddatetime_to, AV57Trn_memowwds_10_tfmemoduration, AV58Trn_memowwds_11_tfmemoduration_to, AV59Trn_memowwds_12_tfmemoremovedate, AV60Trn_memowwds_13_tfmemoremovedate_to});
         while ( (pr_default.getStatus(0) != 101) )
         {
            BRKCJ2 = false;
            A542MemoCategoryId = P00CJ2_A542MemoCategoryId[0];
            A564MemoRemoveDate = P00CJ2_A564MemoRemoveDate[0];
            A563MemoDuration = P00CJ2_A563MemoDuration[0];
            n563MemoDuration = P00CJ2_n563MemoDuration[0];
            A562MemoEndDateTime = P00CJ2_A562MemoEndDateTime[0];
            n562MemoEndDateTime = P00CJ2_n562MemoEndDateTime[0];
            A561MemoStartDateTime = P00CJ2_A561MemoStartDateTime[0];
            n561MemoStartDateTime = P00CJ2_n561MemoStartDateTime[0];
            A550MemoTitle = P00CJ2_A550MemoTitle[0];
            A543MemoCategoryName = P00CJ2_A543MemoCategoryName[0];
            A549MemoId = P00CJ2_A549MemoId[0];
            A543MemoCategoryName = P00CJ2_A543MemoCategoryName[0];
            AV33count = 0;
            while ( (pr_default.getStatus(0) != 101) && ( P00CJ2_A542MemoCategoryId[0] == A542MemoCategoryId ) )
            {
               BRKCJ2 = false;
               A549MemoId = P00CJ2_A549MemoId[0];
               AV33count = (long)(AV33count+1);
               BRKCJ2 = true;
               pr_default.readNext(0);
            }
            AV28Option = (String.IsNullOrEmpty(StringUtil.RTrim( A543MemoCategoryName)) ? "<#Empty#>" : A543MemoCategoryName);
            AV27InsertIndex = 1;
            while ( ( StringUtil.StrCmp(AV28Option, "<#Empty#>") != 0 ) && ( AV27InsertIndex <= AV29Options.Count ) && ( ( StringUtil.StrCmp(((string)AV29Options.Item(AV27InsertIndex)), AV28Option) < 0 ) || ( StringUtil.StrCmp(((string)AV29Options.Item(AV27InsertIndex)), "<#Empty#>") == 0 ) ) )
            {
               AV27InsertIndex = (int)(AV27InsertIndex+1);
            }
            AV29Options.Add(AV28Option, AV27InsertIndex);
            AV32OptionIndexes.Add(StringUtil.Trim( context.localUtil.Format( (decimal)(AV33count), "Z,ZZZ,ZZZ,ZZ9")), AV27InsertIndex);
            if ( AV29Options.Count == AV24SkipItems + 11 )
            {
               AV29Options.RemoveItem(AV29Options.Count);
               AV32OptionIndexes.RemoveItem(AV32OptionIndexes.Count);
            }
            if ( ! BRKCJ2 )
            {
               BRKCJ2 = true;
               pr_default.readNext(0);
            }
         }
         pr_default.close(0);
         while ( AV24SkipItems > 0 )
         {
            AV29Options.RemoveItem(1);
            AV32OptionIndexes.RemoveItem(1);
            AV24SkipItems = (short)(AV24SkipItems-1);
         }
      }

      protected void S131( )
      {
         /* 'LOADMEMOTITLEOPTIONS' Routine */
         returnInSub = false;
         AV13TFMemoTitle = AV23SearchTxt;
         AV14TFMemoTitle_Sel = "";
         AV48Trn_memowwds_1_filterfulltext = AV45FilterFullText;
         AV49Trn_memowwds_2_tfmemocategoryname = AV11TFMemoCategoryName;
         AV50Trn_memowwds_3_tfmemocategoryname_sel = AV12TFMemoCategoryName_Sel;
         AV51Trn_memowwds_4_tfmemotitle = AV13TFMemoTitle;
         AV52Trn_memowwds_5_tfmemotitle_sel = AV14TFMemoTitle_Sel;
         AV53Trn_memowwds_6_tfmemostartdatetime = AV15TFMemoStartDateTime;
         AV54Trn_memowwds_7_tfmemostartdatetime_to = AV16TFMemoStartDateTime_To;
         AV55Trn_memowwds_8_tfmemoenddatetime = AV17TFMemoEndDateTime;
         AV56Trn_memowwds_9_tfmemoenddatetime_to = AV18TFMemoEndDateTime_To;
         AV57Trn_memowwds_10_tfmemoduration = AV19TFMemoDuration;
         AV58Trn_memowwds_11_tfmemoduration_to = AV20TFMemoDuration_To;
         AV59Trn_memowwds_12_tfmemoremovedate = AV21TFMemoRemoveDate;
         AV60Trn_memowwds_13_tfmemoremovedate_to = AV22TFMemoRemoveDate_To;
         pr_default.dynParam(1, new Object[]{ new Object[]{
                                              AV48Trn_memowwds_1_filterfulltext ,
                                              AV50Trn_memowwds_3_tfmemocategoryname_sel ,
                                              AV49Trn_memowwds_2_tfmemocategoryname ,
                                              AV52Trn_memowwds_5_tfmemotitle_sel ,
                                              AV51Trn_memowwds_4_tfmemotitle ,
                                              AV53Trn_memowwds_6_tfmemostartdatetime ,
                                              AV54Trn_memowwds_7_tfmemostartdatetime_to ,
                                              AV55Trn_memowwds_8_tfmemoenddatetime ,
                                              AV56Trn_memowwds_9_tfmemoenddatetime_to ,
                                              AV57Trn_memowwds_10_tfmemoduration ,
                                              AV58Trn_memowwds_11_tfmemoduration_to ,
                                              AV59Trn_memowwds_12_tfmemoremovedate ,
                                              AV60Trn_memowwds_13_tfmemoremovedate_to ,
                                              A543MemoCategoryName ,
                                              A550MemoTitle ,
                                              A563MemoDuration ,
                                              A561MemoStartDateTime ,
                                              A562MemoEndDateTime ,
                                              A564MemoRemoveDate } ,
                                              new int[]{
                                              TypeConstants.DATE, TypeConstants.DATE, TypeConstants.DATE, TypeConstants.DATE, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.DATE, TypeConstants.DATE, TypeConstants.SHORT, TypeConstants.BOOLEAN,
                                              TypeConstants.DATE, TypeConstants.BOOLEAN, TypeConstants.DATE, TypeConstants.BOOLEAN, TypeConstants.DATE
                                              }
         });
         lV48Trn_memowwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV48Trn_memowwds_1_filterfulltext), "%", "");
         lV48Trn_memowwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV48Trn_memowwds_1_filterfulltext), "%", "");
         lV48Trn_memowwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV48Trn_memowwds_1_filterfulltext), "%", "");
         lV49Trn_memowwds_2_tfmemocategoryname = StringUtil.Concat( StringUtil.RTrim( AV49Trn_memowwds_2_tfmemocategoryname), "%", "");
         lV51Trn_memowwds_4_tfmemotitle = StringUtil.Concat( StringUtil.RTrim( AV51Trn_memowwds_4_tfmemotitle), "%", "");
         /* Using cursor P00CJ3 */
         pr_default.execute(1, new Object[] {lV48Trn_memowwds_1_filterfulltext, lV48Trn_memowwds_1_filterfulltext, lV48Trn_memowwds_1_filterfulltext, lV49Trn_memowwds_2_tfmemocategoryname, AV50Trn_memowwds_3_tfmemocategoryname_sel, lV51Trn_memowwds_4_tfmemotitle, AV52Trn_memowwds_5_tfmemotitle_sel, AV53Trn_memowwds_6_tfmemostartdatetime, AV54Trn_memowwds_7_tfmemostartdatetime_to, AV55Trn_memowwds_8_tfmemoenddatetime, AV56Trn_memowwds_9_tfmemoenddatetime_to, AV57Trn_memowwds_10_tfmemoduration, AV58Trn_memowwds_11_tfmemoduration_to, AV59Trn_memowwds_12_tfmemoremovedate, AV60Trn_memowwds_13_tfmemoremovedate_to});
         while ( (pr_default.getStatus(1) != 101) )
         {
            BRKCJ4 = false;
            A542MemoCategoryId = P00CJ3_A542MemoCategoryId[0];
            A550MemoTitle = P00CJ3_A550MemoTitle[0];
            A564MemoRemoveDate = P00CJ3_A564MemoRemoveDate[0];
            A563MemoDuration = P00CJ3_A563MemoDuration[0];
            n563MemoDuration = P00CJ3_n563MemoDuration[0];
            A562MemoEndDateTime = P00CJ3_A562MemoEndDateTime[0];
            n562MemoEndDateTime = P00CJ3_n562MemoEndDateTime[0];
            A561MemoStartDateTime = P00CJ3_A561MemoStartDateTime[0];
            n561MemoStartDateTime = P00CJ3_n561MemoStartDateTime[0];
            A543MemoCategoryName = P00CJ3_A543MemoCategoryName[0];
            A549MemoId = P00CJ3_A549MemoId[0];
            A543MemoCategoryName = P00CJ3_A543MemoCategoryName[0];
            AV33count = 0;
            while ( (pr_default.getStatus(1) != 101) && ( StringUtil.StrCmp(P00CJ3_A550MemoTitle[0], A550MemoTitle) == 0 ) )
            {
               BRKCJ4 = false;
               A549MemoId = P00CJ3_A549MemoId[0];
               AV33count = (long)(AV33count+1);
               BRKCJ4 = true;
               pr_default.readNext(1);
            }
            if ( (0==AV24SkipItems) )
            {
               AV28Option = (String.IsNullOrEmpty(StringUtil.RTrim( A550MemoTitle)) ? "<#Empty#>" : A550MemoTitle);
               AV29Options.Add(AV28Option, 0);
               AV32OptionIndexes.Add(StringUtil.Trim( context.localUtil.Format( (decimal)(AV33count), "Z,ZZZ,ZZZ,ZZ9")), 0);
               if ( AV29Options.Count == 10 )
               {
                  /* Exit For each command. Update data (if necessary), close cursors & exit. */
                  if (true) break;
               }
            }
            else
            {
               AV24SkipItems = (short)(AV24SkipItems-1);
            }
            if ( ! BRKCJ4 )
            {
               BRKCJ4 = true;
               pr_default.readNext(1);
            }
         }
         pr_default.close(1);
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
         AV42OptionsJson = "";
         AV43OptionsDescJson = "";
         AV44OptionIndexesJson = "";
         AV29Options = new GxSimpleCollection<string>();
         AV31OptionsDesc = new GxSimpleCollection<string>();
         AV32OptionIndexes = new GxSimpleCollection<string>();
         AV23SearchTxt = "";
         AV9WWPContext = new GeneXus.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV34Session = context.GetSession();
         AV36GridState = new WorkWithPlus.workwithplus_web.SdtWWPGridState(context);
         AV37GridStateFilterValue = new WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue(context);
         AV45FilterFullText = "";
         AV11TFMemoCategoryName = "";
         AV12TFMemoCategoryName_Sel = "";
         AV13TFMemoTitle = "";
         AV14TFMemoTitle_Sel = "";
         AV15TFMemoStartDateTime = (DateTime)(DateTime.MinValue);
         AV16TFMemoStartDateTime_To = (DateTime)(DateTime.MinValue);
         AV17TFMemoEndDateTime = (DateTime)(DateTime.MinValue);
         AV18TFMemoEndDateTime_To = (DateTime)(DateTime.MinValue);
         AV21TFMemoRemoveDate = DateTime.MinValue;
         AV22TFMemoRemoveDate_To = DateTime.MinValue;
         AV48Trn_memowwds_1_filterfulltext = "";
         AV49Trn_memowwds_2_tfmemocategoryname = "";
         AV50Trn_memowwds_3_tfmemocategoryname_sel = "";
         AV51Trn_memowwds_4_tfmemotitle = "";
         AV52Trn_memowwds_5_tfmemotitle_sel = "";
         AV53Trn_memowwds_6_tfmemostartdatetime = (DateTime)(DateTime.MinValue);
         AV54Trn_memowwds_7_tfmemostartdatetime_to = (DateTime)(DateTime.MinValue);
         AV55Trn_memowwds_8_tfmemoenddatetime = (DateTime)(DateTime.MinValue);
         AV56Trn_memowwds_9_tfmemoenddatetime_to = (DateTime)(DateTime.MinValue);
         AV59Trn_memowwds_12_tfmemoremovedate = DateTime.MinValue;
         AV60Trn_memowwds_13_tfmemoremovedate_to = DateTime.MinValue;
         lV48Trn_memowwds_1_filterfulltext = "";
         lV49Trn_memowwds_2_tfmemocategoryname = "";
         lV51Trn_memowwds_4_tfmemotitle = "";
         A543MemoCategoryName = "";
         A550MemoTitle = "";
         A561MemoStartDateTime = (DateTime)(DateTime.MinValue);
         A562MemoEndDateTime = (DateTime)(DateTime.MinValue);
         A564MemoRemoveDate = DateTime.MinValue;
         P00CJ2_A542MemoCategoryId = new Guid[] {Guid.Empty} ;
         P00CJ2_A564MemoRemoveDate = new DateTime[] {DateTime.MinValue} ;
         P00CJ2_A563MemoDuration = new short[1] ;
         P00CJ2_n563MemoDuration = new bool[] {false} ;
         P00CJ2_A562MemoEndDateTime = new DateTime[] {DateTime.MinValue} ;
         P00CJ2_n562MemoEndDateTime = new bool[] {false} ;
         P00CJ2_A561MemoStartDateTime = new DateTime[] {DateTime.MinValue} ;
         P00CJ2_n561MemoStartDateTime = new bool[] {false} ;
         P00CJ2_A550MemoTitle = new string[] {""} ;
         P00CJ2_A543MemoCategoryName = new string[] {""} ;
         P00CJ2_A549MemoId = new Guid[] {Guid.Empty} ;
         A542MemoCategoryId = Guid.Empty;
         A549MemoId = Guid.Empty;
         AV28Option = "";
         P00CJ3_A542MemoCategoryId = new Guid[] {Guid.Empty} ;
         P00CJ3_A550MemoTitle = new string[] {""} ;
         P00CJ3_A564MemoRemoveDate = new DateTime[] {DateTime.MinValue} ;
         P00CJ3_A563MemoDuration = new short[1] ;
         P00CJ3_n563MemoDuration = new bool[] {false} ;
         P00CJ3_A562MemoEndDateTime = new DateTime[] {DateTime.MinValue} ;
         P00CJ3_n562MemoEndDateTime = new bool[] {false} ;
         P00CJ3_A561MemoStartDateTime = new DateTime[] {DateTime.MinValue} ;
         P00CJ3_n561MemoStartDateTime = new bool[] {false} ;
         P00CJ3_A543MemoCategoryName = new string[] {""} ;
         P00CJ3_A549MemoId = new Guid[] {Guid.Empty} ;
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.trn_memowwgetfilterdata__default(),
            new Object[][] {
                new Object[] {
               P00CJ2_A542MemoCategoryId, P00CJ2_A564MemoRemoveDate, P00CJ2_A563MemoDuration, P00CJ2_n563MemoDuration, P00CJ2_A562MemoEndDateTime, P00CJ2_n562MemoEndDateTime, P00CJ2_A561MemoStartDateTime, P00CJ2_n561MemoStartDateTime, P00CJ2_A550MemoTitle, P00CJ2_A543MemoCategoryName,
               P00CJ2_A549MemoId
               }
               , new Object[] {
               P00CJ3_A542MemoCategoryId, P00CJ3_A550MemoTitle, P00CJ3_A564MemoRemoveDate, P00CJ3_A563MemoDuration, P00CJ3_n563MemoDuration, P00CJ3_A562MemoEndDateTime, P00CJ3_n562MemoEndDateTime, P00CJ3_A561MemoStartDateTime, P00CJ3_n561MemoStartDateTime, P00CJ3_A543MemoCategoryName,
               P00CJ3_A549MemoId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV26MaxItems ;
      private short AV25PageIndex ;
      private short AV24SkipItems ;
      private short AV19TFMemoDuration ;
      private short AV20TFMemoDuration_To ;
      private short AV57Trn_memowwds_10_tfmemoduration ;
      private short AV58Trn_memowwds_11_tfmemoduration_to ;
      private short A563MemoDuration ;
      private int AV46GXV1 ;
      private int AV27InsertIndex ;
      private long AV33count ;
      private DateTime AV15TFMemoStartDateTime ;
      private DateTime AV16TFMemoStartDateTime_To ;
      private DateTime AV17TFMemoEndDateTime ;
      private DateTime AV18TFMemoEndDateTime_To ;
      private DateTime AV53Trn_memowwds_6_tfmemostartdatetime ;
      private DateTime AV54Trn_memowwds_7_tfmemostartdatetime_to ;
      private DateTime AV55Trn_memowwds_8_tfmemoenddatetime ;
      private DateTime AV56Trn_memowwds_9_tfmemoenddatetime_to ;
      private DateTime A561MemoStartDateTime ;
      private DateTime A562MemoEndDateTime ;
      private DateTime AV21TFMemoRemoveDate ;
      private DateTime AV22TFMemoRemoveDate_To ;
      private DateTime AV59Trn_memowwds_12_tfmemoremovedate ;
      private DateTime AV60Trn_memowwds_13_tfmemoremovedate_to ;
      private DateTime A564MemoRemoveDate ;
      private bool returnInSub ;
      private bool BRKCJ2 ;
      private bool n563MemoDuration ;
      private bool n562MemoEndDateTime ;
      private bool n561MemoStartDateTime ;
      private bool BRKCJ4 ;
      private string AV42OptionsJson ;
      private string AV43OptionsDescJson ;
      private string AV44OptionIndexesJson ;
      private string AV39DDOName ;
      private string AV40SearchTxtParms ;
      private string AV41SearchTxtTo ;
      private string AV23SearchTxt ;
      private string AV45FilterFullText ;
      private string AV11TFMemoCategoryName ;
      private string AV12TFMemoCategoryName_Sel ;
      private string AV13TFMemoTitle ;
      private string AV14TFMemoTitle_Sel ;
      private string AV48Trn_memowwds_1_filterfulltext ;
      private string AV49Trn_memowwds_2_tfmemocategoryname ;
      private string AV50Trn_memowwds_3_tfmemocategoryname_sel ;
      private string AV51Trn_memowwds_4_tfmemotitle ;
      private string AV52Trn_memowwds_5_tfmemotitle_sel ;
      private string lV48Trn_memowwds_1_filterfulltext ;
      private string lV49Trn_memowwds_2_tfmemocategoryname ;
      private string lV51Trn_memowwds_4_tfmemotitle ;
      private string A543MemoCategoryName ;
      private string A550MemoTitle ;
      private string AV28Option ;
      private Guid A542MemoCategoryId ;
      private Guid A549MemoId ;
      private IGxSession AV34Session ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GxSimpleCollection<string> AV29Options ;
      private GxSimpleCollection<string> AV31OptionsDesc ;
      private GxSimpleCollection<string> AV32OptionIndexes ;
      private GeneXus.Programs.wwpbaseobjects.SdtWWPContext AV9WWPContext ;
      private WorkWithPlus.workwithplus_web.SdtWWPGridState AV36GridState ;
      private WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue AV37GridStateFilterValue ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00CJ2_A542MemoCategoryId ;
      private DateTime[] P00CJ2_A564MemoRemoveDate ;
      private short[] P00CJ2_A563MemoDuration ;
      private bool[] P00CJ2_n563MemoDuration ;
      private DateTime[] P00CJ2_A562MemoEndDateTime ;
      private bool[] P00CJ2_n562MemoEndDateTime ;
      private DateTime[] P00CJ2_A561MemoStartDateTime ;
      private bool[] P00CJ2_n561MemoStartDateTime ;
      private string[] P00CJ2_A550MemoTitle ;
      private string[] P00CJ2_A543MemoCategoryName ;
      private Guid[] P00CJ2_A549MemoId ;
      private Guid[] P00CJ3_A542MemoCategoryId ;
      private string[] P00CJ3_A550MemoTitle ;
      private DateTime[] P00CJ3_A564MemoRemoveDate ;
      private short[] P00CJ3_A563MemoDuration ;
      private bool[] P00CJ3_n563MemoDuration ;
      private DateTime[] P00CJ3_A562MemoEndDateTime ;
      private bool[] P00CJ3_n562MemoEndDateTime ;
      private DateTime[] P00CJ3_A561MemoStartDateTime ;
      private bool[] P00CJ3_n561MemoStartDateTime ;
      private string[] P00CJ3_A543MemoCategoryName ;
      private Guid[] P00CJ3_A549MemoId ;
      private string aP3_OptionsJson ;
      private string aP4_OptionsDescJson ;
      private string aP5_OptionIndexesJson ;
   }

   public class trn_memowwgetfilterdata__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P00CJ2( IGxContext context ,
                                             string AV48Trn_memowwds_1_filterfulltext ,
                                             string AV50Trn_memowwds_3_tfmemocategoryname_sel ,
                                             string AV49Trn_memowwds_2_tfmemocategoryname ,
                                             string AV52Trn_memowwds_5_tfmemotitle_sel ,
                                             string AV51Trn_memowwds_4_tfmemotitle ,
                                             DateTime AV53Trn_memowwds_6_tfmemostartdatetime ,
                                             DateTime AV54Trn_memowwds_7_tfmemostartdatetime_to ,
                                             DateTime AV55Trn_memowwds_8_tfmemoenddatetime ,
                                             DateTime AV56Trn_memowwds_9_tfmemoenddatetime_to ,
                                             short AV57Trn_memowwds_10_tfmemoduration ,
                                             short AV58Trn_memowwds_11_tfmemoduration_to ,
                                             DateTime AV59Trn_memowwds_12_tfmemoremovedate ,
                                             DateTime AV60Trn_memowwds_13_tfmemoremovedate_to ,
                                             string A543MemoCategoryName ,
                                             string A550MemoTitle ,
                                             short A563MemoDuration ,
                                             DateTime A561MemoStartDateTime ,
                                             DateTime A562MemoEndDateTime ,
                                             DateTime A564MemoRemoveDate )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int1 = new short[15];
         Object[] GXv_Object2 = new Object[2];
         scmdbuf = "SELECT T1.MemoCategoryId, T1.MemoRemoveDate, T1.MemoDuration, T1.MemoEndDateTime, T1.MemoStartDateTime, T1.MemoTitle, T2.MemoCategoryName, T1.MemoId FROM (Trn_Memo T1 INNER JOIN Trn_MemoCategory T2 ON T2.MemoCategoryId = T1.MemoCategoryId)";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV48Trn_memowwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( LOWER(T2.MemoCategoryName) like '%' || LOWER(:lV48Trn_memowwds_1_filterfulltext)) or ( LOWER(T1.MemoTitle) like '%' || LOWER(:lV48Trn_memowwds_1_filterfulltext)) or ( SUBSTR(TO_CHAR(T1.MemoDuration,'9999'), 2) like '%' || :lV48Trn_memowwds_1_filterfulltext))");
         }
         else
         {
            GXv_int1[0] = 1;
            GXv_int1[1] = 1;
            GXv_int1[2] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV50Trn_memowwds_3_tfmemocategoryname_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV49Trn_memowwds_2_tfmemocategoryname)) ) )
         {
            AddWhere(sWhereString, "(T2.MemoCategoryName like :lV49Trn_memowwds_2_tfmemocategoryname)");
         }
         else
         {
            GXv_int1[3] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV50Trn_memowwds_3_tfmemocategoryname_sel)) && ! ( StringUtil.StrCmp(AV50Trn_memowwds_3_tfmemocategoryname_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(T2.MemoCategoryName = ( :AV50Trn_memowwds_3_tfmemocategoryname_sel))");
         }
         else
         {
            GXv_int1[4] = 1;
         }
         if ( StringUtil.StrCmp(AV50Trn_memowwds_3_tfmemocategoryname_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T2.MemoCategoryName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV52Trn_memowwds_5_tfmemotitle_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV51Trn_memowwds_4_tfmemotitle)) ) )
         {
            AddWhere(sWhereString, "(T1.MemoTitle like :lV51Trn_memowwds_4_tfmemotitle)");
         }
         else
         {
            GXv_int1[5] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV52Trn_memowwds_5_tfmemotitle_sel)) && ! ( StringUtil.StrCmp(AV52Trn_memowwds_5_tfmemotitle_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(T1.MemoTitle = ( :AV52Trn_memowwds_5_tfmemotitle_sel))");
         }
         else
         {
            GXv_int1[6] = 1;
         }
         if ( StringUtil.StrCmp(AV52Trn_memowwds_5_tfmemotitle_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.MemoTitle))=0))");
         }
         if ( ! (DateTime.MinValue==AV53Trn_memowwds_6_tfmemostartdatetime) )
         {
            AddWhere(sWhereString, "(T1.MemoStartDateTime >= :AV53Trn_memowwds_6_tfmemostartdatetime)");
         }
         else
         {
            GXv_int1[7] = 1;
         }
         if ( ! (DateTime.MinValue==AV54Trn_memowwds_7_tfmemostartdatetime_to) )
         {
            AddWhere(sWhereString, "(T1.MemoStartDateTime <= :AV54Trn_memowwds_7_tfmemostartdatetime_to)");
         }
         else
         {
            GXv_int1[8] = 1;
         }
         if ( ! (DateTime.MinValue==AV55Trn_memowwds_8_tfmemoenddatetime) )
         {
            AddWhere(sWhereString, "(T1.MemoEndDateTime >= :AV55Trn_memowwds_8_tfmemoenddatetime)");
         }
         else
         {
            GXv_int1[9] = 1;
         }
         if ( ! (DateTime.MinValue==AV56Trn_memowwds_9_tfmemoenddatetime_to) )
         {
            AddWhere(sWhereString, "(T1.MemoEndDateTime <= :AV56Trn_memowwds_9_tfmemoenddatetime_to)");
         }
         else
         {
            GXv_int1[10] = 1;
         }
         if ( ! (0==AV57Trn_memowwds_10_tfmemoduration) )
         {
            AddWhere(sWhereString, "(T1.MemoDuration >= :AV57Trn_memowwds_10_tfmemoduration)");
         }
         else
         {
            GXv_int1[11] = 1;
         }
         if ( ! (0==AV58Trn_memowwds_11_tfmemoduration_to) )
         {
            AddWhere(sWhereString, "(T1.MemoDuration <= :AV58Trn_memowwds_11_tfmemoduration_to)");
         }
         else
         {
            GXv_int1[12] = 1;
         }
         if ( ! (DateTime.MinValue==AV59Trn_memowwds_12_tfmemoremovedate) )
         {
            AddWhere(sWhereString, "(T1.MemoRemoveDate >= :AV59Trn_memowwds_12_tfmemoremovedate)");
         }
         else
         {
            GXv_int1[13] = 1;
         }
         if ( ! (DateTime.MinValue==AV60Trn_memowwds_13_tfmemoremovedate_to) )
         {
            AddWhere(sWhereString, "(T1.MemoRemoveDate <= :AV60Trn_memowwds_13_tfmemoremovedate_to)");
         }
         else
         {
            GXv_int1[14] = 1;
         }
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY T1.MemoCategoryId";
         GXv_Object2[0] = scmdbuf;
         GXv_Object2[1] = GXv_int1;
         return GXv_Object2 ;
      }

      protected Object[] conditional_P00CJ3( IGxContext context ,
                                             string AV48Trn_memowwds_1_filterfulltext ,
                                             string AV50Trn_memowwds_3_tfmemocategoryname_sel ,
                                             string AV49Trn_memowwds_2_tfmemocategoryname ,
                                             string AV52Trn_memowwds_5_tfmemotitle_sel ,
                                             string AV51Trn_memowwds_4_tfmemotitle ,
                                             DateTime AV53Trn_memowwds_6_tfmemostartdatetime ,
                                             DateTime AV54Trn_memowwds_7_tfmemostartdatetime_to ,
                                             DateTime AV55Trn_memowwds_8_tfmemoenddatetime ,
                                             DateTime AV56Trn_memowwds_9_tfmemoenddatetime_to ,
                                             short AV57Trn_memowwds_10_tfmemoduration ,
                                             short AV58Trn_memowwds_11_tfmemoduration_to ,
                                             DateTime AV59Trn_memowwds_12_tfmemoremovedate ,
                                             DateTime AV60Trn_memowwds_13_tfmemoremovedate_to ,
                                             string A543MemoCategoryName ,
                                             string A550MemoTitle ,
                                             short A563MemoDuration ,
                                             DateTime A561MemoStartDateTime ,
                                             DateTime A562MemoEndDateTime ,
                                             DateTime A564MemoRemoveDate )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int3 = new short[15];
         Object[] GXv_Object4 = new Object[2];
         scmdbuf = "SELECT T1.MemoCategoryId, T1.MemoTitle, T1.MemoRemoveDate, T1.MemoDuration, T1.MemoEndDateTime, T1.MemoStartDateTime, T2.MemoCategoryName, T1.MemoId FROM (Trn_Memo T1 INNER JOIN Trn_MemoCategory T2 ON T2.MemoCategoryId = T1.MemoCategoryId)";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV48Trn_memowwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( LOWER(T2.MemoCategoryName) like '%' || LOWER(:lV48Trn_memowwds_1_filterfulltext)) or ( LOWER(T1.MemoTitle) like '%' || LOWER(:lV48Trn_memowwds_1_filterfulltext)) or ( SUBSTR(TO_CHAR(T1.MemoDuration,'9999'), 2) like '%' || :lV48Trn_memowwds_1_filterfulltext))");
         }
         else
         {
            GXv_int3[0] = 1;
            GXv_int3[1] = 1;
            GXv_int3[2] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV50Trn_memowwds_3_tfmemocategoryname_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV49Trn_memowwds_2_tfmemocategoryname)) ) )
         {
            AddWhere(sWhereString, "(T2.MemoCategoryName like :lV49Trn_memowwds_2_tfmemocategoryname)");
         }
         else
         {
            GXv_int3[3] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV50Trn_memowwds_3_tfmemocategoryname_sel)) && ! ( StringUtil.StrCmp(AV50Trn_memowwds_3_tfmemocategoryname_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(T2.MemoCategoryName = ( :AV50Trn_memowwds_3_tfmemocategoryname_sel))");
         }
         else
         {
            GXv_int3[4] = 1;
         }
         if ( StringUtil.StrCmp(AV50Trn_memowwds_3_tfmemocategoryname_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T2.MemoCategoryName))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV52Trn_memowwds_5_tfmemotitle_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV51Trn_memowwds_4_tfmemotitle)) ) )
         {
            AddWhere(sWhereString, "(T1.MemoTitle like :lV51Trn_memowwds_4_tfmemotitle)");
         }
         else
         {
            GXv_int3[5] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV52Trn_memowwds_5_tfmemotitle_sel)) && ! ( StringUtil.StrCmp(AV52Trn_memowwds_5_tfmemotitle_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(T1.MemoTitle = ( :AV52Trn_memowwds_5_tfmemotitle_sel))");
         }
         else
         {
            GXv_int3[6] = 1;
         }
         if ( StringUtil.StrCmp(AV52Trn_memowwds_5_tfmemotitle_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T1.MemoTitle))=0))");
         }
         if ( ! (DateTime.MinValue==AV53Trn_memowwds_6_tfmemostartdatetime) )
         {
            AddWhere(sWhereString, "(T1.MemoStartDateTime >= :AV53Trn_memowwds_6_tfmemostartdatetime)");
         }
         else
         {
            GXv_int3[7] = 1;
         }
         if ( ! (DateTime.MinValue==AV54Trn_memowwds_7_tfmemostartdatetime_to) )
         {
            AddWhere(sWhereString, "(T1.MemoStartDateTime <= :AV54Trn_memowwds_7_tfmemostartdatetime_to)");
         }
         else
         {
            GXv_int3[8] = 1;
         }
         if ( ! (DateTime.MinValue==AV55Trn_memowwds_8_tfmemoenddatetime) )
         {
            AddWhere(sWhereString, "(T1.MemoEndDateTime >= :AV55Trn_memowwds_8_tfmemoenddatetime)");
         }
         else
         {
            GXv_int3[9] = 1;
         }
         if ( ! (DateTime.MinValue==AV56Trn_memowwds_9_tfmemoenddatetime_to) )
         {
            AddWhere(sWhereString, "(T1.MemoEndDateTime <= :AV56Trn_memowwds_9_tfmemoenddatetime_to)");
         }
         else
         {
            GXv_int3[10] = 1;
         }
         if ( ! (0==AV57Trn_memowwds_10_tfmemoduration) )
         {
            AddWhere(sWhereString, "(T1.MemoDuration >= :AV57Trn_memowwds_10_tfmemoduration)");
         }
         else
         {
            GXv_int3[11] = 1;
         }
         if ( ! (0==AV58Trn_memowwds_11_tfmemoduration_to) )
         {
            AddWhere(sWhereString, "(T1.MemoDuration <= :AV58Trn_memowwds_11_tfmemoduration_to)");
         }
         else
         {
            GXv_int3[12] = 1;
         }
         if ( ! (DateTime.MinValue==AV59Trn_memowwds_12_tfmemoremovedate) )
         {
            AddWhere(sWhereString, "(T1.MemoRemoveDate >= :AV59Trn_memowwds_12_tfmemoremovedate)");
         }
         else
         {
            GXv_int3[13] = 1;
         }
         if ( ! (DateTime.MinValue==AV60Trn_memowwds_13_tfmemoremovedate_to) )
         {
            AddWhere(sWhereString, "(T1.MemoRemoveDate <= :AV60Trn_memowwds_13_tfmemoremovedate_to)");
         }
         else
         {
            GXv_int3[14] = 1;
         }
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY T1.MemoTitle";
         GXv_Object4[0] = scmdbuf;
         GXv_Object4[1] = GXv_int3;
         return GXv_Object4 ;
      }

      public override Object [] getDynamicStatement( int cursor ,
                                                     IGxContext context ,
                                                     Object [] dynConstraints )
      {
         switch ( cursor )
         {
               case 0 :
                     return conditional_P00CJ2(context, (string)dynConstraints[0] , (string)dynConstraints[1] , (string)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (DateTime)dynConstraints[5] , (DateTime)dynConstraints[6] , (DateTime)dynConstraints[7] , (DateTime)dynConstraints[8] , (short)dynConstraints[9] , (short)dynConstraints[10] , (DateTime)dynConstraints[11] , (DateTime)dynConstraints[12] , (string)dynConstraints[13] , (string)dynConstraints[14] , (short)dynConstraints[15] , (DateTime)dynConstraints[16] , (DateTime)dynConstraints[17] , (DateTime)dynConstraints[18] );
               case 1 :
                     return conditional_P00CJ3(context, (string)dynConstraints[0] , (string)dynConstraints[1] , (string)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (DateTime)dynConstraints[5] , (DateTime)dynConstraints[6] , (DateTime)dynConstraints[7] , (DateTime)dynConstraints[8] , (short)dynConstraints[9] , (short)dynConstraints[10] , (DateTime)dynConstraints[11] , (DateTime)dynConstraints[12] , (string)dynConstraints[13] , (string)dynConstraints[14] , (short)dynConstraints[15] , (DateTime)dynConstraints[16] , (DateTime)dynConstraints[17] , (DateTime)dynConstraints[18] );
         }
         return base.getDynamicStatement(cursor, context, dynConstraints);
      }

      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
         ,new ForEachCursor(def[1])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP00CJ2;
          prmP00CJ2 = new Object[] {
          new ParDef("lV48Trn_memowwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV48Trn_memowwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV48Trn_memowwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV49Trn_memowwds_2_tfmemocategoryname",GXType.VarChar,100,0) ,
          new ParDef("AV50Trn_memowwds_3_tfmemocategoryname_sel",GXType.VarChar,100,0) ,
          new ParDef("lV51Trn_memowwds_4_tfmemotitle",GXType.VarChar,100,0) ,
          new ParDef("AV52Trn_memowwds_5_tfmemotitle_sel",GXType.VarChar,100,0) ,
          new ParDef("AV53Trn_memowwds_6_tfmemostartdatetime",GXType.DateTime,8,5) ,
          new ParDef("AV54Trn_memowwds_7_tfmemostartdatetime_to",GXType.DateTime,8,5) ,
          new ParDef("AV55Trn_memowwds_8_tfmemoenddatetime",GXType.DateTime,8,5) ,
          new ParDef("AV56Trn_memowwds_9_tfmemoenddatetime_to",GXType.DateTime,8,5) ,
          new ParDef("AV57Trn_memowwds_10_tfmemoduration",GXType.Int16,4,0) ,
          new ParDef("AV58Trn_memowwds_11_tfmemoduration_to",GXType.Int16,4,0) ,
          new ParDef("AV59Trn_memowwds_12_tfmemoremovedate",GXType.Date,8,0) ,
          new ParDef("AV60Trn_memowwds_13_tfmemoremovedate_to",GXType.Date,8,0)
          };
          Object[] prmP00CJ3;
          prmP00CJ3 = new Object[] {
          new ParDef("lV48Trn_memowwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV48Trn_memowwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV48Trn_memowwds_1_filterfulltext",GXType.VarChar,100,0) ,
          new ParDef("lV49Trn_memowwds_2_tfmemocategoryname",GXType.VarChar,100,0) ,
          new ParDef("AV50Trn_memowwds_3_tfmemocategoryname_sel",GXType.VarChar,100,0) ,
          new ParDef("lV51Trn_memowwds_4_tfmemotitle",GXType.VarChar,100,0) ,
          new ParDef("AV52Trn_memowwds_5_tfmemotitle_sel",GXType.VarChar,100,0) ,
          new ParDef("AV53Trn_memowwds_6_tfmemostartdatetime",GXType.DateTime,8,5) ,
          new ParDef("AV54Trn_memowwds_7_tfmemostartdatetime_to",GXType.DateTime,8,5) ,
          new ParDef("AV55Trn_memowwds_8_tfmemoenddatetime",GXType.DateTime,8,5) ,
          new ParDef("AV56Trn_memowwds_9_tfmemoenddatetime_to",GXType.DateTime,8,5) ,
          new ParDef("AV57Trn_memowwds_10_tfmemoduration",GXType.Int16,4,0) ,
          new ParDef("AV58Trn_memowwds_11_tfmemoduration_to",GXType.Int16,4,0) ,
          new ParDef("AV59Trn_memowwds_12_tfmemoremovedate",GXType.Date,8,0) ,
          new ParDef("AV60Trn_memowwds_13_tfmemoremovedate_to",GXType.Date,8,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00CJ2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00CJ2,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00CJ3", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00CJ3,100, GxCacheFrequency.OFF ,true,false )
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
                ((DateTime[]) buf[1])[0] = rslt.getGXDate(2);
                ((short[]) buf[2])[0] = rslt.getShort(3);
                ((bool[]) buf[3])[0] = rslt.wasNull(3);
                ((DateTime[]) buf[4])[0] = rslt.getGXDateTime(4);
                ((bool[]) buf[5])[0] = rslt.wasNull(4);
                ((DateTime[]) buf[6])[0] = rslt.getGXDateTime(5);
                ((bool[]) buf[7])[0] = rslt.wasNull(5);
                ((string[]) buf[8])[0] = rslt.getVarchar(6);
                ((string[]) buf[9])[0] = rslt.getVarchar(7);
                ((Guid[]) buf[10])[0] = rslt.getGuid(8);
                return;
             case 1 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((string[]) buf[1])[0] = rslt.getVarchar(2);
                ((DateTime[]) buf[2])[0] = rslt.getGXDate(3);
                ((short[]) buf[3])[0] = rslt.getShort(4);
                ((bool[]) buf[4])[0] = rslt.wasNull(4);
                ((DateTime[]) buf[5])[0] = rslt.getGXDateTime(5);
                ((bool[]) buf[6])[0] = rslt.wasNull(5);
                ((DateTime[]) buf[7])[0] = rslt.getGXDateTime(6);
                ((bool[]) buf[8])[0] = rslt.wasNull(6);
                ((string[]) buf[9])[0] = rslt.getVarchar(7);
                ((Guid[]) buf[10])[0] = rslt.getGuid(8);
                return;
       }
    }

 }

}
