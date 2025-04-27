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
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace GeneXus.Programs {
   public class trn_memo_bc : GxSilentTrn, IGxSilentTrn
   {
      public trn_memo_bc( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public trn_memo_bc( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      protected void INITTRN( )
      {
      }

      public void GetInsDefault( )
      {
         ReadRow1P100( ) ;
         standaloneNotModal( ) ;
         InitializeNonKey1P100( ) ;
         standaloneModal( ) ;
         AddRow1P100( ) ;
         Gx_mode = "INS";
         return  ;
      }

      protected void AfterTrn( )
      {
         if ( trnEnded == 1 )
         {
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( endTrnMsgTxt)) )
            {
               GX_msglist.addItem(endTrnMsgTxt, endTrnMsgCod, 0, "", true);
            }
            /* Execute user event: After Trn */
            E111P2 ();
            trnEnded = 0;
            standaloneNotModal( ) ;
            standaloneModal( ) ;
            if ( IsIns( )  )
            {
               Z549MemoId = A549MemoId;
               SetMode( "UPD") ;
            }
         }
         endTrnMsgTxt = "";
      }

      public override string ToString( )
      {
         return "" ;
      }

      public GxContentInfo GetContentInfo( )
      {
         return (GxContentInfo)(null) ;
      }

      public bool Reindex( )
      {
         return true ;
      }

      protected void CONFIRM_1P0( )
      {
         BeforeValidate1P100( ) ;
         if ( AnyError == 0 )
         {
            if ( IsDlt( ) )
            {
               OnDeleteControls1P100( ) ;
            }
            else
            {
               CheckExtendedTable1P100( ) ;
               if ( AnyError == 0 )
               {
                  ZM1P100( 10) ;
                  ZM1P100( 11) ;
               }
               CloseExtendedTableCursors1P100( ) ;
            }
         }
         if ( AnyError == 0 )
         {
         }
      }

      protected void E121P2( )
      {
         /* Start Routine */
         returnInSub = false;
         new GeneXus.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV8WWPContext) ;
         AV11TrnContext.FromXml(AV12WebSession.Get("TrnContext"), null, "", "");
         if ( ( StringUtil.StrCmp(AV11TrnContext.gxTpr_Transactionname, AV31Pgmname) == 0 ) && ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) )
         {
            AV32GXV1 = 1;
            while ( AV32GXV1 <= AV11TrnContext.gxTpr_Attributes.Count )
            {
               AV15TrnContextAtt = ((WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext_Attribute)AV11TrnContext.gxTpr_Attributes.Item(AV32GXV1));
               if ( StringUtil.StrCmp(AV15TrnContextAtt.gxTpr_Attributename, "MemoCategoryId") == 0 )
               {
                  AV14Insert_MemoCategoryId = StringUtil.StrToGuid( AV15TrnContextAtt.gxTpr_Attributevalue);
               }
               else if ( StringUtil.StrCmp(AV15TrnContextAtt.gxTpr_Attributename, "ResidentId") == 0 )
               {
                  AV26Insert_ResidentId = StringUtil.StrToGuid( AV15TrnContextAtt.gxTpr_Attributevalue);
               }
               else if ( StringUtil.StrCmp(AV15TrnContextAtt.gxTpr_Attributename, "SG_OrganisationId") == 0 )
               {
                  AV29Insert_SG_OrganisationId = StringUtil.StrToGuid( AV15TrnContextAtt.gxTpr_Attributevalue);
               }
               else if ( StringUtil.StrCmp(AV15TrnContextAtt.gxTpr_Attributename, "SG_LocationId") == 0 )
               {
                  AV30Insert_SG_LocationId = StringUtil.StrToGuid( AV15TrnContextAtt.gxTpr_Attributevalue);
               }
               AV32GXV1 = (int)(AV32GXV1+1);
            }
         }
      }

      protected void E111P2( )
      {
         /* After Trn Routine */
         returnInSub = false;
      }

      protected void ZM1P100( short GX_JID )
      {
         if ( ( GX_JID == 9 ) || ( GX_JID == 0 ) )
         {
            Z550MemoTitle = A550MemoTitle;
            Z551MemoDescription = A551MemoDescription;
            Z552MemoImage = A552MemoImage;
            Z553MemoDocument = A553MemoDocument;
            Z561MemoStartDateTime = A561MemoStartDateTime;
            Z562MemoEndDateTime = A562MemoEndDateTime;
            Z563MemoDuration = A563MemoDuration;
            Z564MemoRemoveDate = A564MemoRemoveDate;
            Z566MemoBgColorCode = A566MemoBgColorCode;
            Z567MemoForm = A567MemoForm;
            Z542MemoCategoryId = A542MemoCategoryId;
            Z62ResidentId = A62ResidentId;
            Z528SG_LocationId = A528SG_LocationId;
            Z529SG_OrganisationId = A529SG_OrganisationId;
         }
         if ( ( GX_JID == 10 ) || ( GX_JID == 0 ) )
         {
            Z543MemoCategoryName = A543MemoCategoryName;
         }
         if ( ( GX_JID == 11 ) || ( GX_JID == 0 ) )
         {
            Z72ResidentSalutation = A72ResidentSalutation;
            Z64ResidentGivenName = A64ResidentGivenName;
            Z65ResidentLastName = A65ResidentLastName;
            Z71ResidentGUID = A71ResidentGUID;
         }
         if ( GX_JID == -9 )
         {
            Z549MemoId = A549MemoId;
            Z550MemoTitle = A550MemoTitle;
            Z551MemoDescription = A551MemoDescription;
            Z552MemoImage = A552MemoImage;
            Z553MemoDocument = A553MemoDocument;
            Z561MemoStartDateTime = A561MemoStartDateTime;
            Z562MemoEndDateTime = A562MemoEndDateTime;
            Z563MemoDuration = A563MemoDuration;
            Z564MemoRemoveDate = A564MemoRemoveDate;
            Z566MemoBgColorCode = A566MemoBgColorCode;
            Z567MemoForm = A567MemoForm;
            Z542MemoCategoryId = A542MemoCategoryId;
            Z62ResidentId = A62ResidentId;
            Z528SG_LocationId = A528SG_LocationId;
            Z529SG_OrganisationId = A529SG_OrganisationId;
            Z543MemoCategoryName = A543MemoCategoryName;
            Z29LocationId = A29LocationId;
            Z11OrganisationId = A11OrganisationId;
            Z72ResidentSalutation = A72ResidentSalutation;
            Z64ResidentGivenName = A64ResidentGivenName;
            Z65ResidentLastName = A65ResidentLastName;
            Z71ResidentGUID = A71ResidentGUID;
         }
      }

      protected void standaloneNotModal( )
      {
         AV31Pgmname = "Trn_Memo_BC";
      }

      protected void standaloneModal( )
      {
         if ( IsIns( )  && (Guid.Empty==A549MemoId) )
         {
            A549MemoId = Guid.NewGuid( );
         }
         if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) && ( Gx_BScreen == 0 ) )
         {
         }
      }

      protected void Load1P100( )
      {
         /* Using cursor BC001P6 */
         pr_default.execute(4, new Object[] {A549MemoId});
         if ( (pr_default.getStatus(4) != 101) )
         {
            RcdFound100 = 1;
            A29LocationId = BC001P6_A29LocationId[0];
            A11OrganisationId = BC001P6_A11OrganisationId[0];
            A543MemoCategoryName = BC001P6_A543MemoCategoryName[0];
            A550MemoTitle = BC001P6_A550MemoTitle[0];
            A551MemoDescription = BC001P6_A551MemoDescription[0];
            A552MemoImage = BC001P6_A552MemoImage[0];
            n552MemoImage = BC001P6_n552MemoImage[0];
            A553MemoDocument = BC001P6_A553MemoDocument[0];
            n553MemoDocument = BC001P6_n553MemoDocument[0];
            A561MemoStartDateTime = BC001P6_A561MemoStartDateTime[0];
            n561MemoStartDateTime = BC001P6_n561MemoStartDateTime[0];
            A562MemoEndDateTime = BC001P6_A562MemoEndDateTime[0];
            n562MemoEndDateTime = BC001P6_n562MemoEndDateTime[0];
            A563MemoDuration = BC001P6_A563MemoDuration[0];
            n563MemoDuration = BC001P6_n563MemoDuration[0];
            A564MemoRemoveDate = BC001P6_A564MemoRemoveDate[0];
            A72ResidentSalutation = BC001P6_A72ResidentSalutation[0];
            A64ResidentGivenName = BC001P6_A64ResidentGivenName[0];
            A65ResidentLastName = BC001P6_A65ResidentLastName[0];
            A71ResidentGUID = BC001P6_A71ResidentGUID[0];
            A566MemoBgColorCode = BC001P6_A566MemoBgColorCode[0];
            A567MemoForm = BC001P6_A567MemoForm[0];
            A542MemoCategoryId = BC001P6_A542MemoCategoryId[0];
            A62ResidentId = BC001P6_A62ResidentId[0];
            A528SG_LocationId = BC001P6_A528SG_LocationId[0];
            A529SG_OrganisationId = BC001P6_A529SG_OrganisationId[0];
            ZM1P100( -9) ;
         }
         pr_default.close(4);
         OnLoadActions1P100( ) ;
      }

      protected void OnLoadActions1P100( )
      {
      }

      protected void CheckExtendedTable1P100( )
      {
         standaloneModal( ) ;
         /* Using cursor BC001P4 */
         pr_default.execute(2, new Object[] {A542MemoCategoryId});
         if ( (pr_default.getStatus(2) == 101) )
         {
            GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXSPC_ForeignKeyNotFound", ""), context.GetMessage( "Trn_MemoCategory", ""), "", "", "", "", "", "", "", ""), "ForeignKeyNotFound", 1, "MEMOCATEGORYID");
            AnyError = 1;
         }
         A543MemoCategoryName = BC001P4_A543MemoCategoryName[0];
         pr_default.close(2);
         /* Using cursor BC001P5 */
         pr_default.execute(3, new Object[] {A62ResidentId, A528SG_LocationId, A529SG_OrganisationId});
         if ( (pr_default.getStatus(3) == 101) )
         {
            GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXSPC_ForeignKeyNotFound", ""), context.GetMessage( "Trn_Resident", ""), "", "", "", "", "", "", "", ""), "ForeignKeyNotFound", 1, "SG_ORGANISATIONID");
            AnyError = 1;
         }
         A72ResidentSalutation = BC001P5_A72ResidentSalutation[0];
         A64ResidentGivenName = BC001P5_A64ResidentGivenName[0];
         A65ResidentLastName = BC001P5_A65ResidentLastName[0];
         A71ResidentGUID = BC001P5_A71ResidentGUID[0];
         pr_default.close(3);
      }

      protected void CloseExtendedTableCursors1P100( )
      {
         pr_default.close(2);
         pr_default.close(3);
      }

      protected void enableDisable( )
      {
      }

      protected void GetKey1P100( )
      {
         /* Using cursor BC001P7 */
         pr_default.execute(5, new Object[] {A549MemoId});
         if ( (pr_default.getStatus(5) != 101) )
         {
            RcdFound100 = 1;
         }
         else
         {
            RcdFound100 = 0;
         }
         pr_default.close(5);
      }

      protected void getByPrimaryKey( )
      {
         /* Using cursor BC001P3 */
         pr_default.execute(1, new Object[] {A549MemoId});
         if ( (pr_default.getStatus(1) != 101) )
         {
            ZM1P100( 9) ;
            RcdFound100 = 1;
            A549MemoId = BC001P3_A549MemoId[0];
            A550MemoTitle = BC001P3_A550MemoTitle[0];
            A551MemoDescription = BC001P3_A551MemoDescription[0];
            A552MemoImage = BC001P3_A552MemoImage[0];
            n552MemoImage = BC001P3_n552MemoImage[0];
            A553MemoDocument = BC001P3_A553MemoDocument[0];
            n553MemoDocument = BC001P3_n553MemoDocument[0];
            A561MemoStartDateTime = BC001P3_A561MemoStartDateTime[0];
            n561MemoStartDateTime = BC001P3_n561MemoStartDateTime[0];
            A562MemoEndDateTime = BC001P3_A562MemoEndDateTime[0];
            n562MemoEndDateTime = BC001P3_n562MemoEndDateTime[0];
            A563MemoDuration = BC001P3_A563MemoDuration[0];
            n563MemoDuration = BC001P3_n563MemoDuration[0];
            A564MemoRemoveDate = BC001P3_A564MemoRemoveDate[0];
            A566MemoBgColorCode = BC001P3_A566MemoBgColorCode[0];
            A567MemoForm = BC001P3_A567MemoForm[0];
            A542MemoCategoryId = BC001P3_A542MemoCategoryId[0];
            A62ResidentId = BC001P3_A62ResidentId[0];
            A528SG_LocationId = BC001P3_A528SG_LocationId[0];
            A529SG_OrganisationId = BC001P3_A529SG_OrganisationId[0];
            Z549MemoId = A549MemoId;
            sMode100 = Gx_mode;
            Gx_mode = "DSP";
            standaloneModal( ) ;
            Load1P100( ) ;
            if ( AnyError == 1 )
            {
               RcdFound100 = 0;
               InitializeNonKey1P100( ) ;
            }
            Gx_mode = sMode100;
         }
         else
         {
            RcdFound100 = 0;
            InitializeNonKey1P100( ) ;
            sMode100 = Gx_mode;
            Gx_mode = "DSP";
            standaloneModal( ) ;
            Gx_mode = sMode100;
         }
         pr_default.close(1);
      }

      protected void getEqualNoModal( )
      {
         GetKey1P100( ) ;
         if ( RcdFound100 == 0 )
         {
            Gx_mode = "INS";
         }
         else
         {
            Gx_mode = "UPD";
         }
         getByPrimaryKey( ) ;
      }

      protected void insert_Check( )
      {
         CONFIRM_1P0( ) ;
      }

      protected void update_Check( )
      {
         insert_Check( ) ;
      }

      protected void delete_Check( )
      {
         insert_Check( ) ;
      }

      protected void CheckOptimisticConcurrency1P100( )
      {
         if ( ! IsIns( ) )
         {
            /* Using cursor BC001P2 */
            pr_default.execute(0, new Object[] {A549MemoId});
            if ( (pr_default.getStatus(0) == 103) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"Trn_Memo"}), "RecordIsLocked", 1, "");
               AnyError = 1;
               return  ;
            }
            Gx_longc = false;
            if ( (pr_default.getStatus(0) == 101) || ( StringUtil.StrCmp(Z550MemoTitle, BC001P2_A550MemoTitle[0]) != 0 ) || ( StringUtil.StrCmp(Z551MemoDescription, BC001P2_A551MemoDescription[0]) != 0 ) || ( StringUtil.StrCmp(Z552MemoImage, BC001P2_A552MemoImage[0]) != 0 ) || ( StringUtil.StrCmp(Z553MemoDocument, BC001P2_A553MemoDocument[0]) != 0 ) || ( Z561MemoStartDateTime != BC001P2_A561MemoStartDateTime[0] ) )
            {
               Gx_longc = true;
            }
            if ( Gx_longc || ( Z562MemoEndDateTime != BC001P2_A562MemoEndDateTime[0] ) || ( Z563MemoDuration != BC001P2_A563MemoDuration[0] ) || ( DateTimeUtil.ResetTime ( Z564MemoRemoveDate ) != DateTimeUtil.ResetTime ( BC001P2_A564MemoRemoveDate[0] ) ) || ( StringUtil.StrCmp(Z566MemoBgColorCode, BC001P2_A566MemoBgColorCode[0]) != 0 ) || ( StringUtil.StrCmp(Z567MemoForm, BC001P2_A567MemoForm[0]) != 0 ) )
            {
               Gx_longc = true;
            }
            if ( Gx_longc || ( Z542MemoCategoryId != BC001P2_A542MemoCategoryId[0] ) || ( Z62ResidentId != BC001P2_A62ResidentId[0] ) || ( Z528SG_LocationId != BC001P2_A528SG_LocationId[0] ) || ( Z529SG_OrganisationId != BC001P2_A529SG_OrganisationId[0] ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_waschg", new   object[]  {"Trn_Memo"}), "RecordWasChanged", 1, "");
               AnyError = 1;
               return  ;
            }
         }
      }

      protected void Insert1P100( )
      {
         BeforeValidate1P100( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable1P100( ) ;
         }
         if ( AnyError == 0 )
         {
            ZM1P100( 0) ;
            CheckOptimisticConcurrency1P100( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm1P100( ) ;
               if ( AnyError == 0 )
               {
                  BeforeInsert1P100( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor BC001P8 */
                     pr_default.execute(6, new Object[] {A549MemoId, A550MemoTitle, A551MemoDescription, n552MemoImage, A552MemoImage, n553MemoDocument, A553MemoDocument, n561MemoStartDateTime, A561MemoStartDateTime, n562MemoEndDateTime, A562MemoEndDateTime, n563MemoDuration, A563MemoDuration, A564MemoRemoveDate, A566MemoBgColorCode, A567MemoForm, A542MemoCategoryId, A62ResidentId, A528SG_LocationId, A529SG_OrganisationId});
                     pr_default.close(6);
                     pr_default.SmartCacheProvider.SetUpdated("Trn_Memo");
                     if ( (pr_default.getStatus(6) == 1) )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_noupdate", ""), "DuplicatePrimaryKey", 1, "");
                        AnyError = 1;
                     }
                     if ( AnyError == 0 )
                     {
                        /* Start of After( Insert) rules */
                        /* End of After( Insert) rules */
                        if ( AnyError == 0 )
                        {
                           /* Save values for previous() function. */
                           endTrnMsgTxt = context.GetMessage( "GXM_sucadded", "");
                           endTrnMsgCod = "SuccessfullyAdded";
                        }
                     }
                  }
                  else
                  {
                     GX_msglist.addItem(context.GetMessage( "GXM_unexp", ""), 1, "");
                     AnyError = 1;
                  }
               }
            }
            else
            {
               Load1P100( ) ;
            }
            EndLevel1P100( ) ;
         }
         CloseExtendedTableCursors1P100( ) ;
      }

      protected void Update1P100( )
      {
         BeforeValidate1P100( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable1P100( ) ;
         }
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency1P100( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm1P100( ) ;
               if ( AnyError == 0 )
               {
                  BeforeUpdate1P100( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor BC001P9 */
                     pr_default.execute(7, new Object[] {A550MemoTitle, A551MemoDescription, n552MemoImage, A552MemoImage, n553MemoDocument, A553MemoDocument, n561MemoStartDateTime, A561MemoStartDateTime, n562MemoEndDateTime, A562MemoEndDateTime, n563MemoDuration, A563MemoDuration, A564MemoRemoveDate, A566MemoBgColorCode, A567MemoForm, A542MemoCategoryId, A62ResidentId, A528SG_LocationId, A529SG_OrganisationId, A549MemoId});
                     pr_default.close(7);
                     pr_default.SmartCacheProvider.SetUpdated("Trn_Memo");
                     if ( (pr_default.getStatus(7) == 103) )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"Trn_Memo"}), "RecordIsLocked", 1, "");
                        AnyError = 1;
                     }
                     DeferredUpdate1P100( ) ;
                     if ( AnyError == 0 )
                     {
                        /* Start of After( update) rules */
                        /* End of After( update) rules */
                        if ( AnyError == 0 )
                        {
                           getByPrimaryKey( ) ;
                           endTrnMsgTxt = context.GetMessage( "GXM_sucupdated", "");
                           endTrnMsgCod = "SuccessfullyUpdated";
                        }
                     }
                     else
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_unexp", ""), 1, "");
                        AnyError = 1;
                     }
                  }
               }
            }
            EndLevel1P100( ) ;
         }
         CloseExtendedTableCursors1P100( ) ;
      }

      protected void DeferredUpdate1P100( )
      {
      }

      protected void delete( )
      {
         Gx_mode = "DLT";
         BeforeValidate1P100( ) ;
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency1P100( ) ;
         }
         if ( AnyError == 0 )
         {
            OnDeleteControls1P100( ) ;
            AfterConfirm1P100( ) ;
            if ( AnyError == 0 )
            {
               BeforeDelete1P100( ) ;
               if ( AnyError == 0 )
               {
                  /* No cascading delete specified. */
                  /* Using cursor BC001P10 */
                  pr_default.execute(8, new Object[] {A549MemoId});
                  pr_default.close(8);
                  pr_default.SmartCacheProvider.SetUpdated("Trn_Memo");
                  if ( AnyError == 0 )
                  {
                     /* Start of After( delete) rules */
                     /* End of After( delete) rules */
                     if ( AnyError == 0 )
                     {
                        endTrnMsgTxt = context.GetMessage( "GXM_sucdeleted", "");
                        endTrnMsgCod = "SuccessfullyDeleted";
                     }
                  }
                  else
                  {
                     GX_msglist.addItem(context.GetMessage( "GXM_unexp", ""), 1, "");
                     AnyError = 1;
                  }
               }
            }
         }
         sMode100 = Gx_mode;
         Gx_mode = "DLT";
         EndLevel1P100( ) ;
         Gx_mode = sMode100;
      }

      protected void OnDeleteControls1P100( )
      {
         standaloneModal( ) ;
         if ( AnyError == 0 )
         {
            /* Delete mode formulas */
            /* Using cursor BC001P11 */
            pr_default.execute(9, new Object[] {A542MemoCategoryId});
            A543MemoCategoryName = BC001P11_A543MemoCategoryName[0];
            pr_default.close(9);
            /* Using cursor BC001P12 */
            pr_default.execute(10, new Object[] {A62ResidentId, A528SG_LocationId, A529SG_OrganisationId});
            A72ResidentSalutation = BC001P12_A72ResidentSalutation[0];
            A64ResidentGivenName = BC001P12_A64ResidentGivenName[0];
            A65ResidentLastName = BC001P12_A65ResidentLastName[0];
            A71ResidentGUID = BC001P12_A71ResidentGUID[0];
            pr_default.close(10);
         }
      }

      protected void EndLevel1P100( )
      {
         if ( ! IsIns( ) )
         {
            pr_default.close(0);
         }
         if ( AnyError == 0 )
         {
            BeforeComplete1P100( ) ;
         }
         if ( AnyError == 0 )
         {
            /* After transaction rules */
            /* Execute 'After Trn' event if defined. */
            trnEnded = 1;
         }
         else
         {
         }
         if ( AnyError != 0 )
         {
            context.wjLoc = "";
            context.nUserReturn = 0;
         }
      }

      public void ScanKeyStart1P100( )
      {
         /* Scan By routine */
         /* Using cursor BC001P13 */
         pr_default.execute(11, new Object[] {A549MemoId});
         RcdFound100 = 0;
         if ( (pr_default.getStatus(11) != 101) )
         {
            RcdFound100 = 1;
            A29LocationId = BC001P13_A29LocationId[0];
            A11OrganisationId = BC001P13_A11OrganisationId[0];
            A549MemoId = BC001P13_A549MemoId[0];
            A543MemoCategoryName = BC001P13_A543MemoCategoryName[0];
            A550MemoTitle = BC001P13_A550MemoTitle[0];
            A551MemoDescription = BC001P13_A551MemoDescription[0];
            A552MemoImage = BC001P13_A552MemoImage[0];
            n552MemoImage = BC001P13_n552MemoImage[0];
            A553MemoDocument = BC001P13_A553MemoDocument[0];
            n553MemoDocument = BC001P13_n553MemoDocument[0];
            A561MemoStartDateTime = BC001P13_A561MemoStartDateTime[0];
            n561MemoStartDateTime = BC001P13_n561MemoStartDateTime[0];
            A562MemoEndDateTime = BC001P13_A562MemoEndDateTime[0];
            n562MemoEndDateTime = BC001P13_n562MemoEndDateTime[0];
            A563MemoDuration = BC001P13_A563MemoDuration[0];
            n563MemoDuration = BC001P13_n563MemoDuration[0];
            A564MemoRemoveDate = BC001P13_A564MemoRemoveDate[0];
            A72ResidentSalutation = BC001P13_A72ResidentSalutation[0];
            A64ResidentGivenName = BC001P13_A64ResidentGivenName[0];
            A65ResidentLastName = BC001P13_A65ResidentLastName[0];
            A71ResidentGUID = BC001P13_A71ResidentGUID[0];
            A566MemoBgColorCode = BC001P13_A566MemoBgColorCode[0];
            A567MemoForm = BC001P13_A567MemoForm[0];
            A542MemoCategoryId = BC001P13_A542MemoCategoryId[0];
            A62ResidentId = BC001P13_A62ResidentId[0];
            A528SG_LocationId = BC001P13_A528SG_LocationId[0];
            A529SG_OrganisationId = BC001P13_A529SG_OrganisationId[0];
         }
         /* Load Subordinate Levels */
      }

      protected void ScanKeyNext1P100( )
      {
         /* Scan next routine */
         pr_default.readNext(11);
         RcdFound100 = 0;
         ScanKeyLoad1P100( ) ;
      }

      protected void ScanKeyLoad1P100( )
      {
         sMode100 = Gx_mode;
         Gx_mode = "DSP";
         if ( (pr_default.getStatus(11) != 101) )
         {
            RcdFound100 = 1;
            A29LocationId = BC001P13_A29LocationId[0];
            A11OrganisationId = BC001P13_A11OrganisationId[0];
            A549MemoId = BC001P13_A549MemoId[0];
            A543MemoCategoryName = BC001P13_A543MemoCategoryName[0];
            A550MemoTitle = BC001P13_A550MemoTitle[0];
            A551MemoDescription = BC001P13_A551MemoDescription[0];
            A552MemoImage = BC001P13_A552MemoImage[0];
            n552MemoImage = BC001P13_n552MemoImage[0];
            A553MemoDocument = BC001P13_A553MemoDocument[0];
            n553MemoDocument = BC001P13_n553MemoDocument[0];
            A561MemoStartDateTime = BC001P13_A561MemoStartDateTime[0];
            n561MemoStartDateTime = BC001P13_n561MemoStartDateTime[0];
            A562MemoEndDateTime = BC001P13_A562MemoEndDateTime[0];
            n562MemoEndDateTime = BC001P13_n562MemoEndDateTime[0];
            A563MemoDuration = BC001P13_A563MemoDuration[0];
            n563MemoDuration = BC001P13_n563MemoDuration[0];
            A564MemoRemoveDate = BC001P13_A564MemoRemoveDate[0];
            A72ResidentSalutation = BC001P13_A72ResidentSalutation[0];
            A64ResidentGivenName = BC001P13_A64ResidentGivenName[0];
            A65ResidentLastName = BC001P13_A65ResidentLastName[0];
            A71ResidentGUID = BC001P13_A71ResidentGUID[0];
            A566MemoBgColorCode = BC001P13_A566MemoBgColorCode[0];
            A567MemoForm = BC001P13_A567MemoForm[0];
            A542MemoCategoryId = BC001P13_A542MemoCategoryId[0];
            A62ResidentId = BC001P13_A62ResidentId[0];
            A528SG_LocationId = BC001P13_A528SG_LocationId[0];
            A529SG_OrganisationId = BC001P13_A529SG_OrganisationId[0];
         }
         Gx_mode = sMode100;
      }

      protected void ScanKeyEnd1P100( )
      {
         pr_default.close(11);
      }

      protected void AfterConfirm1P100( )
      {
         /* After Confirm Rules */
      }

      protected void BeforeInsert1P100( )
      {
         /* Before Insert Rules */
      }

      protected void BeforeUpdate1P100( )
      {
         /* Before Update Rules */
      }

      protected void BeforeDelete1P100( )
      {
         /* Before Delete Rules */
      }

      protected void BeforeComplete1P100( )
      {
         /* Before Complete Rules */
      }

      protected void BeforeValidate1P100( )
      {
         /* Before Validate Rules */
      }

      protected void DisableAttributes1P100( )
      {
      }

      protected void send_integrity_lvl_hashes1P100( )
      {
      }

      protected void AddRow1P100( )
      {
         VarsToRow100( bcTrn_Memo) ;
      }

      protected void ReadRow1P100( )
      {
         RowToVars100( bcTrn_Memo, 1) ;
      }

      protected void InitializeNonKey1P100( )
      {
         A11OrganisationId = Guid.Empty;
         A29LocationId = Guid.Empty;
         A542MemoCategoryId = Guid.Empty;
         A543MemoCategoryName = "";
         A550MemoTitle = "";
         A551MemoDescription = "";
         A552MemoImage = "";
         n552MemoImage = false;
         A553MemoDocument = "";
         n553MemoDocument = false;
         A561MemoStartDateTime = (DateTime)(DateTime.MinValue);
         n561MemoStartDateTime = false;
         A562MemoEndDateTime = (DateTime)(DateTime.MinValue);
         n562MemoEndDateTime = false;
         A563MemoDuration = 0;
         n563MemoDuration = false;
         A564MemoRemoveDate = DateTime.MinValue;
         A62ResidentId = Guid.Empty;
         A72ResidentSalutation = "";
         A64ResidentGivenName = "";
         A65ResidentLastName = "";
         A71ResidentGUID = "";
         A566MemoBgColorCode = "";
         A567MemoForm = "";
         A529SG_OrganisationId = Guid.Empty;
         A528SG_LocationId = Guid.Empty;
         Z550MemoTitle = "";
         Z551MemoDescription = "";
         Z552MemoImage = "";
         Z553MemoDocument = "";
         Z561MemoStartDateTime = (DateTime)(DateTime.MinValue);
         Z562MemoEndDateTime = (DateTime)(DateTime.MinValue);
         Z563MemoDuration = 0;
         Z564MemoRemoveDate = DateTime.MinValue;
         Z566MemoBgColorCode = "";
         Z567MemoForm = "";
         Z542MemoCategoryId = Guid.Empty;
         Z62ResidentId = Guid.Empty;
         Z528SG_LocationId = Guid.Empty;
         Z529SG_OrganisationId = Guid.Empty;
      }

      protected void InitAll1P100( )
      {
         A549MemoId = Guid.NewGuid( );
         InitializeNonKey1P100( ) ;
      }

      protected void StandaloneModalInsert( )
      {
      }

      protected bool IsIns( )
      {
         return ((StringUtil.StrCmp(Gx_mode, "INS")==0) ? true : false) ;
      }

      protected bool IsDlt( )
      {
         return ((StringUtil.StrCmp(Gx_mode, "DLT")==0) ? true : false) ;
      }

      protected bool IsUpd( )
      {
         return ((StringUtil.StrCmp(Gx_mode, "UPD")==0) ? true : false) ;
      }

      protected bool IsDsp( )
      {
         return ((StringUtil.StrCmp(Gx_mode, "DSP")==0) ? true : false) ;
      }

      public void VarsToRow100( SdtTrn_Memo obj100 )
      {
         obj100.gxTpr_Mode = Gx_mode;
         obj100.gxTpr_Memocategoryid = A542MemoCategoryId;
         obj100.gxTpr_Memocategoryname = A543MemoCategoryName;
         obj100.gxTpr_Memotitle = A550MemoTitle;
         obj100.gxTpr_Memodescription = A551MemoDescription;
         obj100.gxTpr_Memoimage = A552MemoImage;
         obj100.gxTpr_Memodocument = A553MemoDocument;
         obj100.gxTpr_Memostartdatetime = A561MemoStartDateTime;
         obj100.gxTpr_Memoenddatetime = A562MemoEndDateTime;
         obj100.gxTpr_Memoduration = A563MemoDuration;
         obj100.gxTpr_Memoremovedate = A564MemoRemoveDate;
         obj100.gxTpr_Residentid = A62ResidentId;
         obj100.gxTpr_Residentsalutation = A72ResidentSalutation;
         obj100.gxTpr_Residentgivenname = A64ResidentGivenName;
         obj100.gxTpr_Residentlastname = A65ResidentLastName;
         obj100.gxTpr_Residentguid = A71ResidentGUID;
         obj100.gxTpr_Memobgcolorcode = A566MemoBgColorCode;
         obj100.gxTpr_Memoform = A567MemoForm;
         obj100.gxTpr_Sg_organisationid = A529SG_OrganisationId;
         obj100.gxTpr_Sg_locationid = A528SG_LocationId;
         obj100.gxTpr_Memoid = A549MemoId;
         obj100.gxTpr_Memoid_Z = Z549MemoId;
         obj100.gxTpr_Memocategoryid_Z = Z542MemoCategoryId;
         obj100.gxTpr_Memocategoryname_Z = Z543MemoCategoryName;
         obj100.gxTpr_Memotitle_Z = Z550MemoTitle;
         obj100.gxTpr_Memodescription_Z = Z551MemoDescription;
         obj100.gxTpr_Memoimage_Z = Z552MemoImage;
         obj100.gxTpr_Memodocument_Z = Z553MemoDocument;
         obj100.gxTpr_Memostartdatetime_Z = Z561MemoStartDateTime;
         obj100.gxTpr_Memoenddatetime_Z = Z562MemoEndDateTime;
         obj100.gxTpr_Memoduration_Z = Z563MemoDuration;
         obj100.gxTpr_Memoremovedate_Z = Z564MemoRemoveDate;
         obj100.gxTpr_Residentid_Z = Z62ResidentId;
         obj100.gxTpr_Residentsalutation_Z = Z72ResidentSalutation;
         obj100.gxTpr_Residentgivenname_Z = Z64ResidentGivenName;
         obj100.gxTpr_Residentlastname_Z = Z65ResidentLastName;
         obj100.gxTpr_Residentguid_Z = Z71ResidentGUID;
         obj100.gxTpr_Memobgcolorcode_Z = Z566MemoBgColorCode;
         obj100.gxTpr_Memoform_Z = Z567MemoForm;
         obj100.gxTpr_Sg_organisationid_Z = Z529SG_OrganisationId;
         obj100.gxTpr_Sg_locationid_Z = Z528SG_LocationId;
         obj100.gxTpr_Memoimage_N = (short)(Convert.ToInt16(n552MemoImage));
         obj100.gxTpr_Memodocument_N = (short)(Convert.ToInt16(n553MemoDocument));
         obj100.gxTpr_Memostartdatetime_N = (short)(Convert.ToInt16(n561MemoStartDateTime));
         obj100.gxTpr_Memoenddatetime_N = (short)(Convert.ToInt16(n562MemoEndDateTime));
         obj100.gxTpr_Memoduration_N = (short)(Convert.ToInt16(n563MemoDuration));
         obj100.gxTpr_Mode = Gx_mode;
         return  ;
      }

      public void KeyVarsToRow100( SdtTrn_Memo obj100 )
      {
         obj100.gxTpr_Memoid = A549MemoId;
         return  ;
      }

      public void RowToVars100( SdtTrn_Memo obj100 ,
                                int forceLoad )
      {
         Gx_mode = obj100.gxTpr_Mode;
         A542MemoCategoryId = obj100.gxTpr_Memocategoryid;
         A543MemoCategoryName = obj100.gxTpr_Memocategoryname;
         A550MemoTitle = obj100.gxTpr_Memotitle;
         A551MemoDescription = obj100.gxTpr_Memodescription;
         A552MemoImage = obj100.gxTpr_Memoimage;
         n552MemoImage = false;
         A553MemoDocument = obj100.gxTpr_Memodocument;
         n553MemoDocument = false;
         A561MemoStartDateTime = obj100.gxTpr_Memostartdatetime;
         n561MemoStartDateTime = false;
         A562MemoEndDateTime = obj100.gxTpr_Memoenddatetime;
         n562MemoEndDateTime = false;
         A563MemoDuration = obj100.gxTpr_Memoduration;
         n563MemoDuration = false;
         A564MemoRemoveDate = obj100.gxTpr_Memoremovedate;
         A62ResidentId = obj100.gxTpr_Residentid;
         A72ResidentSalutation = obj100.gxTpr_Residentsalutation;
         A64ResidentGivenName = obj100.gxTpr_Residentgivenname;
         A65ResidentLastName = obj100.gxTpr_Residentlastname;
         A71ResidentGUID = obj100.gxTpr_Residentguid;
         A566MemoBgColorCode = obj100.gxTpr_Memobgcolorcode;
         A567MemoForm = obj100.gxTpr_Memoform;
         A529SG_OrganisationId = obj100.gxTpr_Sg_organisationid;
         A528SG_LocationId = obj100.gxTpr_Sg_locationid;
         A549MemoId = obj100.gxTpr_Memoid;
         Z549MemoId = obj100.gxTpr_Memoid_Z;
         Z542MemoCategoryId = obj100.gxTpr_Memocategoryid_Z;
         Z543MemoCategoryName = obj100.gxTpr_Memocategoryname_Z;
         Z550MemoTitle = obj100.gxTpr_Memotitle_Z;
         Z551MemoDescription = obj100.gxTpr_Memodescription_Z;
         Z552MemoImage = obj100.gxTpr_Memoimage_Z;
         Z553MemoDocument = obj100.gxTpr_Memodocument_Z;
         Z561MemoStartDateTime = obj100.gxTpr_Memostartdatetime_Z;
         Z562MemoEndDateTime = obj100.gxTpr_Memoenddatetime_Z;
         Z563MemoDuration = obj100.gxTpr_Memoduration_Z;
         Z564MemoRemoveDate = obj100.gxTpr_Memoremovedate_Z;
         Z62ResidentId = obj100.gxTpr_Residentid_Z;
         Z72ResidentSalutation = obj100.gxTpr_Residentsalutation_Z;
         Z64ResidentGivenName = obj100.gxTpr_Residentgivenname_Z;
         Z65ResidentLastName = obj100.gxTpr_Residentlastname_Z;
         Z71ResidentGUID = obj100.gxTpr_Residentguid_Z;
         Z566MemoBgColorCode = obj100.gxTpr_Memobgcolorcode_Z;
         Z567MemoForm = obj100.gxTpr_Memoform_Z;
         Z529SG_OrganisationId = obj100.gxTpr_Sg_organisationid_Z;
         Z528SG_LocationId = obj100.gxTpr_Sg_locationid_Z;
         n552MemoImage = (bool)(Convert.ToBoolean(obj100.gxTpr_Memoimage_N));
         n553MemoDocument = (bool)(Convert.ToBoolean(obj100.gxTpr_Memodocument_N));
         n561MemoStartDateTime = (bool)(Convert.ToBoolean(obj100.gxTpr_Memostartdatetime_N));
         n562MemoEndDateTime = (bool)(Convert.ToBoolean(obj100.gxTpr_Memoenddatetime_N));
         n563MemoDuration = (bool)(Convert.ToBoolean(obj100.gxTpr_Memoduration_N));
         Gx_mode = obj100.gxTpr_Mode;
         return  ;
      }

      public void LoadKey( Object[] obj )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         A549MemoId = (Guid)getParm(obj,0);
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         InitializeNonKey1P100( ) ;
         ScanKeyStart1P100( ) ;
         if ( RcdFound100 == 0 )
         {
            Gx_mode = "INS";
         }
         else
         {
            Gx_mode = "UPD";
            Z549MemoId = A549MemoId;
         }
         ZM1P100( -9) ;
         OnLoadActions1P100( ) ;
         AddRow1P100( ) ;
         ScanKeyEnd1P100( ) ;
         if ( RcdFound100 == 0 )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_keynfound", ""), "PrimaryKeyNotFound", 1, "");
            AnyError = 1;
         }
         context.GX_msglist = BackMsgLst;
      }

      public void Load( )
      {
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         RowToVars100( bcTrn_Memo, 0) ;
         ScanKeyStart1P100( ) ;
         if ( RcdFound100 == 0 )
         {
            Gx_mode = "INS";
         }
         else
         {
            Gx_mode = "UPD";
            Z549MemoId = A549MemoId;
         }
         ZM1P100( -9) ;
         OnLoadActions1P100( ) ;
         AddRow1P100( ) ;
         ScanKeyEnd1P100( ) ;
         if ( RcdFound100 == 0 )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_keynfound", ""), "PrimaryKeyNotFound", 1, "");
            AnyError = 1;
         }
         context.GX_msglist = BackMsgLst;
      }

      protected void SaveImpl( )
      {
         GetKey1P100( ) ;
         if ( IsIns( ) )
         {
            /* Insert record */
            Insert1P100( ) ;
         }
         else
         {
            if ( RcdFound100 == 1 )
            {
               if ( A549MemoId != Z549MemoId )
               {
                  A549MemoId = Z549MemoId;
                  GX_msglist.addItem(context.GetMessage( "GXM_getbeforeupd", ""), "CandidateKeyNotFound", 1, "");
                  AnyError = 1;
               }
               else if ( IsDlt( ) )
               {
                  delete( ) ;
                  AfterTrn( ) ;
               }
               else
               {
                  Gx_mode = "UPD";
                  /* Update record */
                  Update1P100( ) ;
               }
            }
            else
            {
               if ( IsDlt( ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_getbeforeupd", ""), "CandidateKeyNotFound", 1, "");
                  AnyError = 1;
               }
               else
               {
                  if ( A549MemoId != Z549MemoId )
                  {
                     if ( IsUpd( ) )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_getbeforeupd", ""), "DuplicatePrimaryKey", 1, "");
                        AnyError = 1;
                     }
                     else
                     {
                        Gx_mode = "INS";
                        /* Insert record */
                        Insert1P100( ) ;
                     }
                  }
                  else
                  {
                     if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_recdeleted", ""), 1, "");
                        AnyError = 1;
                     }
                     else
                     {
                        Gx_mode = "INS";
                        /* Insert record */
                        Insert1P100( ) ;
                     }
                  }
               }
            }
         }
         AfterTrn( ) ;
      }

      public void Save( )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         RowToVars100( bcTrn_Memo, 1) ;
         SaveImpl( ) ;
         VarsToRow100( bcTrn_Memo) ;
         context.GX_msglist = BackMsgLst;
         return  ;
      }

      public bool Insert( )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         RowToVars100( bcTrn_Memo, 1) ;
         Gx_mode = "INS";
         /* Insert record */
         Insert1P100( ) ;
         AfterTrn( ) ;
         VarsToRow100( bcTrn_Memo) ;
         context.GX_msglist = BackMsgLst;
         return (AnyError==0) ;
      }

      protected void UpdateImpl( )
      {
         if ( IsUpd( ) )
         {
            SaveImpl( ) ;
            VarsToRow100( bcTrn_Memo) ;
         }
         else
         {
            SdtTrn_Memo auxBC = new SdtTrn_Memo(context);
            IGxSilentTrn auxTrn = auxBC.getTransaction();
            auxBC.Load(A549MemoId);
            if ( auxTrn.Errors() == 0 )
            {
               auxBC.UpdateDirties(bcTrn_Memo);
               auxBC.Save();
               bcTrn_Memo.Copy((GxSilentTrnSdt)(auxBC));
            }
            LclMsgLst = (msglist)(auxTrn.GetMessages());
            AnyError = (short)(auxTrn.Errors());
            context.GX_msglist = LclMsgLst;
            if ( auxTrn.Errors() == 0 )
            {
               Gx_mode = auxTrn.GetMode();
               AfterTrn( ) ;
            }
         }
      }

      public bool Update( )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         RowToVars100( bcTrn_Memo, 1) ;
         UpdateImpl( ) ;
         context.GX_msglist = BackMsgLst;
         return (AnyError==0) ;
      }

      public bool InsertOrUpdate( )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         RowToVars100( bcTrn_Memo, 1) ;
         Gx_mode = "INS";
         /* Insert record */
         Insert1P100( ) ;
         if ( AnyError == 1 )
         {
            if ( StringUtil.StrCmp(context.GX_msglist.getItemValue(1), "DuplicatePrimaryKey") == 0 )
            {
               AnyError = 0;
               context.GX_msglist.removeAllItems();
               UpdateImpl( ) ;
            }
            else
            {
               VarsToRow100( bcTrn_Memo) ;
            }
         }
         else
         {
            AfterTrn( ) ;
            VarsToRow100( bcTrn_Memo) ;
         }
         context.GX_msglist = BackMsgLst;
         return (AnyError==0) ;
      }

      public void Check( )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         RowToVars100( bcTrn_Memo, 0) ;
         GetKey1P100( ) ;
         if ( RcdFound100 == 1 )
         {
            if ( IsIns( ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_noupdate", ""), "DuplicatePrimaryKey", 1, "");
               AnyError = 1;
            }
            else if ( A549MemoId != Z549MemoId )
            {
               A549MemoId = Z549MemoId;
               GX_msglist.addItem(context.GetMessage( "GXM_getbeforeupd", ""), "DuplicatePrimaryKey", 1, "");
               AnyError = 1;
            }
            else if ( IsDlt( ) )
            {
               delete_Check( ) ;
            }
            else
            {
               Gx_mode = "UPD";
               update_Check( ) ;
            }
         }
         else
         {
            if ( A549MemoId != Z549MemoId )
            {
               Gx_mode = "INS";
               insert_Check( ) ;
            }
            else
            {
               if ( IsUpd( ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_recdeleted", ""), 1, "");
                  AnyError = 1;
               }
               else
               {
                  Gx_mode = "INS";
                  insert_Check( ) ;
               }
            }
         }
         context.RollbackDataStores("trn_memo_bc",pr_default);
         VarsToRow100( bcTrn_Memo) ;
         context.GX_msglist = BackMsgLst;
         return  ;
      }

      public int Errors( )
      {
         if ( AnyError == 0 )
         {
            return (int)(0) ;
         }
         return (int)(1) ;
      }

      public msglist GetMessages( )
      {
         return LclMsgLst ;
      }

      public string GetMode( )
      {
         Gx_mode = bcTrn_Memo.gxTpr_Mode;
         return Gx_mode ;
      }

      public void SetMode( string lMode )
      {
         Gx_mode = lMode;
         bcTrn_Memo.gxTpr_Mode = Gx_mode;
         return  ;
      }

      public void SetSDT( GxSilentTrnSdt sdt ,
                          short sdtToBc )
      {
         if ( sdt != bcTrn_Memo )
         {
            bcTrn_Memo = (SdtTrn_Memo)(sdt);
            if ( StringUtil.StrCmp(bcTrn_Memo.gxTpr_Mode, "") == 0 )
            {
               bcTrn_Memo.gxTpr_Mode = "INS";
            }
            if ( sdtToBc == 1 )
            {
               VarsToRow100( bcTrn_Memo) ;
            }
            else
            {
               RowToVars100( bcTrn_Memo, 1) ;
            }
         }
         else
         {
            if ( StringUtil.StrCmp(bcTrn_Memo.gxTpr_Mode, "") == 0 )
            {
               bcTrn_Memo.gxTpr_Mode = "INS";
            }
         }
         return  ;
      }

      public void ReloadFromSDT( )
      {
         RowToVars100( bcTrn_Memo, 1) ;
         return  ;
      }

      public void ForceCommitOnExit( )
      {
         return  ;
      }

      public SdtTrn_Memo Trn_Memo_BC
      {
         get {
            return bcTrn_Memo ;
         }

      }

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
            return "trn_memo_Execute" ;
         }

      }

      public void webExecute( )
      {
         createObjects();
         initialize();
      }

      public bool isMasterPage( )
      {
         return false;
      }

      protected void createObjects( )
      {
      }

      protected void Process( )
      {
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
      }

      protected override void CloseCursors( )
      {
         pr_default.close(1);
         pr_default.close(9);
         pr_default.close(10);
      }

      public override void initialize( )
      {
         Gx_mode = "";
         endTrnMsgTxt = "";
         endTrnMsgCod = "";
         Z549MemoId = Guid.Empty;
         A549MemoId = Guid.Empty;
         AV8WWPContext = new GeneXus.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV11TrnContext = new WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext(context);
         AV12WebSession = context.GetSession();
         AV31Pgmname = "";
         AV15TrnContextAtt = new WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext_Attribute(context);
         AV14Insert_MemoCategoryId = Guid.Empty;
         AV26Insert_ResidentId = Guid.Empty;
         AV29Insert_SG_OrganisationId = Guid.Empty;
         AV30Insert_SG_LocationId = Guid.Empty;
         Z550MemoTitle = "";
         A550MemoTitle = "";
         Z551MemoDescription = "";
         A551MemoDescription = "";
         Z552MemoImage = "";
         A552MemoImage = "";
         Z553MemoDocument = "";
         A553MemoDocument = "";
         Z561MemoStartDateTime = (DateTime)(DateTime.MinValue);
         A561MemoStartDateTime = (DateTime)(DateTime.MinValue);
         Z562MemoEndDateTime = (DateTime)(DateTime.MinValue);
         A562MemoEndDateTime = (DateTime)(DateTime.MinValue);
         Z564MemoRemoveDate = DateTime.MinValue;
         A564MemoRemoveDate = DateTime.MinValue;
         Z566MemoBgColorCode = "";
         A566MemoBgColorCode = "";
         Z567MemoForm = "";
         A567MemoForm = "";
         Z542MemoCategoryId = Guid.Empty;
         A542MemoCategoryId = Guid.Empty;
         Z62ResidentId = Guid.Empty;
         A62ResidentId = Guid.Empty;
         Z528SG_LocationId = Guid.Empty;
         A528SG_LocationId = Guid.Empty;
         Z529SG_OrganisationId = Guid.Empty;
         A529SG_OrganisationId = Guid.Empty;
         Z543MemoCategoryName = "";
         A543MemoCategoryName = "";
         Z72ResidentSalutation = "";
         A72ResidentSalutation = "";
         Z64ResidentGivenName = "";
         A64ResidentGivenName = "";
         Z65ResidentLastName = "";
         A65ResidentLastName = "";
         Z71ResidentGUID = "";
         A71ResidentGUID = "";
         Z29LocationId = Guid.Empty;
         A29LocationId = Guid.Empty;
         Z11OrganisationId = Guid.Empty;
         A11OrganisationId = Guid.Empty;
         BC001P6_A29LocationId = new Guid[] {Guid.Empty} ;
         BC001P6_A11OrganisationId = new Guid[] {Guid.Empty} ;
         BC001P6_A549MemoId = new Guid[] {Guid.Empty} ;
         BC001P6_A543MemoCategoryName = new string[] {""} ;
         BC001P6_A550MemoTitle = new string[] {""} ;
         BC001P6_A551MemoDescription = new string[] {""} ;
         BC001P6_A552MemoImage = new string[] {""} ;
         BC001P6_n552MemoImage = new bool[] {false} ;
         BC001P6_A553MemoDocument = new string[] {""} ;
         BC001P6_n553MemoDocument = new bool[] {false} ;
         BC001P6_A561MemoStartDateTime = new DateTime[] {DateTime.MinValue} ;
         BC001P6_n561MemoStartDateTime = new bool[] {false} ;
         BC001P6_A562MemoEndDateTime = new DateTime[] {DateTime.MinValue} ;
         BC001P6_n562MemoEndDateTime = new bool[] {false} ;
         BC001P6_A563MemoDuration = new short[1] ;
         BC001P6_n563MemoDuration = new bool[] {false} ;
         BC001P6_A564MemoRemoveDate = new DateTime[] {DateTime.MinValue} ;
         BC001P6_A72ResidentSalutation = new string[] {""} ;
         BC001P6_A64ResidentGivenName = new string[] {""} ;
         BC001P6_A65ResidentLastName = new string[] {""} ;
         BC001P6_A71ResidentGUID = new string[] {""} ;
         BC001P6_A566MemoBgColorCode = new string[] {""} ;
         BC001P6_A567MemoForm = new string[] {""} ;
         BC001P6_A542MemoCategoryId = new Guid[] {Guid.Empty} ;
         BC001P6_A62ResidentId = new Guid[] {Guid.Empty} ;
         BC001P6_A528SG_LocationId = new Guid[] {Guid.Empty} ;
         BC001P6_A529SG_OrganisationId = new Guid[] {Guid.Empty} ;
         BC001P4_A543MemoCategoryName = new string[] {""} ;
         BC001P5_A29LocationId = new Guid[] {Guid.Empty} ;
         BC001P5_A11OrganisationId = new Guid[] {Guid.Empty} ;
         BC001P5_A72ResidentSalutation = new string[] {""} ;
         BC001P5_A64ResidentGivenName = new string[] {""} ;
         BC001P5_A65ResidentLastName = new string[] {""} ;
         BC001P5_A71ResidentGUID = new string[] {""} ;
         BC001P7_A549MemoId = new Guid[] {Guid.Empty} ;
         BC001P3_A549MemoId = new Guid[] {Guid.Empty} ;
         BC001P3_A550MemoTitle = new string[] {""} ;
         BC001P3_A551MemoDescription = new string[] {""} ;
         BC001P3_A552MemoImage = new string[] {""} ;
         BC001P3_n552MemoImage = new bool[] {false} ;
         BC001P3_A553MemoDocument = new string[] {""} ;
         BC001P3_n553MemoDocument = new bool[] {false} ;
         BC001P3_A561MemoStartDateTime = new DateTime[] {DateTime.MinValue} ;
         BC001P3_n561MemoStartDateTime = new bool[] {false} ;
         BC001P3_A562MemoEndDateTime = new DateTime[] {DateTime.MinValue} ;
         BC001P3_n562MemoEndDateTime = new bool[] {false} ;
         BC001P3_A563MemoDuration = new short[1] ;
         BC001P3_n563MemoDuration = new bool[] {false} ;
         BC001P3_A564MemoRemoveDate = new DateTime[] {DateTime.MinValue} ;
         BC001P3_A566MemoBgColorCode = new string[] {""} ;
         BC001P3_A567MemoForm = new string[] {""} ;
         BC001P3_A542MemoCategoryId = new Guid[] {Guid.Empty} ;
         BC001P3_A62ResidentId = new Guid[] {Guid.Empty} ;
         BC001P3_A528SG_LocationId = new Guid[] {Guid.Empty} ;
         BC001P3_A529SG_OrganisationId = new Guid[] {Guid.Empty} ;
         sMode100 = "";
         BC001P2_A549MemoId = new Guid[] {Guid.Empty} ;
         BC001P2_A550MemoTitle = new string[] {""} ;
         BC001P2_A551MemoDescription = new string[] {""} ;
         BC001P2_A552MemoImage = new string[] {""} ;
         BC001P2_n552MemoImage = new bool[] {false} ;
         BC001P2_A553MemoDocument = new string[] {""} ;
         BC001P2_n553MemoDocument = new bool[] {false} ;
         BC001P2_A561MemoStartDateTime = new DateTime[] {DateTime.MinValue} ;
         BC001P2_n561MemoStartDateTime = new bool[] {false} ;
         BC001P2_A562MemoEndDateTime = new DateTime[] {DateTime.MinValue} ;
         BC001P2_n562MemoEndDateTime = new bool[] {false} ;
         BC001P2_A563MemoDuration = new short[1] ;
         BC001P2_n563MemoDuration = new bool[] {false} ;
         BC001P2_A564MemoRemoveDate = new DateTime[] {DateTime.MinValue} ;
         BC001P2_A566MemoBgColorCode = new string[] {""} ;
         BC001P2_A567MemoForm = new string[] {""} ;
         BC001P2_A542MemoCategoryId = new Guid[] {Guid.Empty} ;
         BC001P2_A62ResidentId = new Guid[] {Guid.Empty} ;
         BC001P2_A528SG_LocationId = new Guid[] {Guid.Empty} ;
         BC001P2_A529SG_OrganisationId = new Guid[] {Guid.Empty} ;
         BC001P11_A543MemoCategoryName = new string[] {""} ;
         BC001P12_A72ResidentSalutation = new string[] {""} ;
         BC001P12_A64ResidentGivenName = new string[] {""} ;
         BC001P12_A65ResidentLastName = new string[] {""} ;
         BC001P12_A71ResidentGUID = new string[] {""} ;
         BC001P13_A29LocationId = new Guid[] {Guid.Empty} ;
         BC001P13_A11OrganisationId = new Guid[] {Guid.Empty} ;
         BC001P13_A549MemoId = new Guid[] {Guid.Empty} ;
         BC001P13_A543MemoCategoryName = new string[] {""} ;
         BC001P13_A550MemoTitle = new string[] {""} ;
         BC001P13_A551MemoDescription = new string[] {""} ;
         BC001P13_A552MemoImage = new string[] {""} ;
         BC001P13_n552MemoImage = new bool[] {false} ;
         BC001P13_A553MemoDocument = new string[] {""} ;
         BC001P13_n553MemoDocument = new bool[] {false} ;
         BC001P13_A561MemoStartDateTime = new DateTime[] {DateTime.MinValue} ;
         BC001P13_n561MemoStartDateTime = new bool[] {false} ;
         BC001P13_A562MemoEndDateTime = new DateTime[] {DateTime.MinValue} ;
         BC001P13_n562MemoEndDateTime = new bool[] {false} ;
         BC001P13_A563MemoDuration = new short[1] ;
         BC001P13_n563MemoDuration = new bool[] {false} ;
         BC001P13_A564MemoRemoveDate = new DateTime[] {DateTime.MinValue} ;
         BC001P13_A72ResidentSalutation = new string[] {""} ;
         BC001P13_A64ResidentGivenName = new string[] {""} ;
         BC001P13_A65ResidentLastName = new string[] {""} ;
         BC001P13_A71ResidentGUID = new string[] {""} ;
         BC001P13_A566MemoBgColorCode = new string[] {""} ;
         BC001P13_A567MemoForm = new string[] {""} ;
         BC001P13_A542MemoCategoryId = new Guid[] {Guid.Empty} ;
         BC001P13_A62ResidentId = new Guid[] {Guid.Empty} ;
         BC001P13_A528SG_LocationId = new Guid[] {Guid.Empty} ;
         BC001P13_A529SG_OrganisationId = new Guid[] {Guid.Empty} ;
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         pr_datastore1 = new DataStoreProvider(context, new GeneXus.Programs.trn_memo_bc__datastore1(),
            new Object[][] {
            }
         );
         pr_gam = new DataStoreProvider(context, new GeneXus.Programs.trn_memo_bc__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.trn_memo_bc__default(),
            new Object[][] {
                new Object[] {
               BC001P2_A549MemoId, BC001P2_A550MemoTitle, BC001P2_A551MemoDescription, BC001P2_A552MemoImage, BC001P2_n552MemoImage, BC001P2_A553MemoDocument, BC001P2_n553MemoDocument, BC001P2_A561MemoStartDateTime, BC001P2_n561MemoStartDateTime, BC001P2_A562MemoEndDateTime,
               BC001P2_n562MemoEndDateTime, BC001P2_A563MemoDuration, BC001P2_n563MemoDuration, BC001P2_A564MemoRemoveDate, BC001P2_A566MemoBgColorCode, BC001P2_A567MemoForm, BC001P2_A542MemoCategoryId, BC001P2_A62ResidentId, BC001P2_A528SG_LocationId, BC001P2_A529SG_OrganisationId
               }
               , new Object[] {
               BC001P3_A549MemoId, BC001P3_A550MemoTitle, BC001P3_A551MemoDescription, BC001P3_A552MemoImage, BC001P3_n552MemoImage, BC001P3_A553MemoDocument, BC001P3_n553MemoDocument, BC001P3_A561MemoStartDateTime, BC001P3_n561MemoStartDateTime, BC001P3_A562MemoEndDateTime,
               BC001P3_n562MemoEndDateTime, BC001P3_A563MemoDuration, BC001P3_n563MemoDuration, BC001P3_A564MemoRemoveDate, BC001P3_A566MemoBgColorCode, BC001P3_A567MemoForm, BC001P3_A542MemoCategoryId, BC001P3_A62ResidentId, BC001P3_A528SG_LocationId, BC001P3_A529SG_OrganisationId
               }
               , new Object[] {
               BC001P4_A543MemoCategoryName
               }
               , new Object[] {
               BC001P5_A29LocationId, BC001P5_A11OrganisationId, BC001P5_A72ResidentSalutation, BC001P5_A64ResidentGivenName, BC001P5_A65ResidentLastName, BC001P5_A71ResidentGUID
               }
               , new Object[] {
               BC001P6_A29LocationId, BC001P6_A11OrganisationId, BC001P6_A549MemoId, BC001P6_A543MemoCategoryName, BC001P6_A550MemoTitle, BC001P6_A551MemoDescription, BC001P6_A552MemoImage, BC001P6_n552MemoImage, BC001P6_A553MemoDocument, BC001P6_n553MemoDocument,
               BC001P6_A561MemoStartDateTime, BC001P6_n561MemoStartDateTime, BC001P6_A562MemoEndDateTime, BC001P6_n562MemoEndDateTime, BC001P6_A563MemoDuration, BC001P6_n563MemoDuration, BC001P6_A564MemoRemoveDate, BC001P6_A72ResidentSalutation, BC001P6_A64ResidentGivenName, BC001P6_A65ResidentLastName,
               BC001P6_A71ResidentGUID, BC001P6_A566MemoBgColorCode, BC001P6_A567MemoForm, BC001P6_A542MemoCategoryId, BC001P6_A62ResidentId, BC001P6_A528SG_LocationId, BC001P6_A529SG_OrganisationId
               }
               , new Object[] {
               BC001P7_A549MemoId
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               BC001P11_A543MemoCategoryName
               }
               , new Object[] {
               BC001P12_A72ResidentSalutation, BC001P12_A64ResidentGivenName, BC001P12_A65ResidentLastName, BC001P12_A71ResidentGUID
               }
               , new Object[] {
               BC001P13_A29LocationId, BC001P13_A11OrganisationId, BC001P13_A549MemoId, BC001P13_A543MemoCategoryName, BC001P13_A550MemoTitle, BC001P13_A551MemoDescription, BC001P13_A552MemoImage, BC001P13_n552MemoImage, BC001P13_A553MemoDocument, BC001P13_n553MemoDocument,
               BC001P13_A561MemoStartDateTime, BC001P13_n561MemoStartDateTime, BC001P13_A562MemoEndDateTime, BC001P13_n562MemoEndDateTime, BC001P13_A563MemoDuration, BC001P13_n563MemoDuration, BC001P13_A564MemoRemoveDate, BC001P13_A72ResidentSalutation, BC001P13_A64ResidentGivenName, BC001P13_A65ResidentLastName,
               BC001P13_A71ResidentGUID, BC001P13_A566MemoBgColorCode, BC001P13_A567MemoForm, BC001P13_A542MemoCategoryId, BC001P13_A62ResidentId, BC001P13_A528SG_LocationId, BC001P13_A529SG_OrganisationId
               }
            }
         );
         Z549MemoId = Guid.NewGuid( );
         A549MemoId = Guid.NewGuid( );
         AV31Pgmname = "Trn_Memo_BC";
         INITTRN();
         /* Execute Start event if defined. */
         /* Execute user event: Start */
         E121P2 ();
         standaloneNotModal( ) ;
      }

      private short AnyError ;
      private short Z563MemoDuration ;
      private short A563MemoDuration ;
      private short Gx_BScreen ;
      private short RcdFound100 ;
      private int trnEnded ;
      private int AV32GXV1 ;
      private string Gx_mode ;
      private string endTrnMsgTxt ;
      private string endTrnMsgCod ;
      private string AV31Pgmname ;
      private string Z567MemoForm ;
      private string A567MemoForm ;
      private string Z72ResidentSalutation ;
      private string A72ResidentSalutation ;
      private string sMode100 ;
      private DateTime Z561MemoStartDateTime ;
      private DateTime A561MemoStartDateTime ;
      private DateTime Z562MemoEndDateTime ;
      private DateTime A562MemoEndDateTime ;
      private DateTime Z564MemoRemoveDate ;
      private DateTime A564MemoRemoveDate ;
      private bool returnInSub ;
      private bool n552MemoImage ;
      private bool n553MemoDocument ;
      private bool n561MemoStartDateTime ;
      private bool n562MemoEndDateTime ;
      private bool n563MemoDuration ;
      private bool Gx_longc ;
      private string Z550MemoTitle ;
      private string A550MemoTitle ;
      private string Z551MemoDescription ;
      private string A551MemoDescription ;
      private string Z552MemoImage ;
      private string A552MemoImage ;
      private string Z553MemoDocument ;
      private string A553MemoDocument ;
      private string Z566MemoBgColorCode ;
      private string A566MemoBgColorCode ;
      private string Z543MemoCategoryName ;
      private string A543MemoCategoryName ;
      private string Z64ResidentGivenName ;
      private string A64ResidentGivenName ;
      private string Z65ResidentLastName ;
      private string A65ResidentLastName ;
      private string Z71ResidentGUID ;
      private string A71ResidentGUID ;
      private Guid Z549MemoId ;
      private Guid A549MemoId ;
      private Guid AV14Insert_MemoCategoryId ;
      private Guid AV26Insert_ResidentId ;
      private Guid AV29Insert_SG_OrganisationId ;
      private Guid AV30Insert_SG_LocationId ;
      private Guid Z542MemoCategoryId ;
      private Guid A542MemoCategoryId ;
      private Guid Z62ResidentId ;
      private Guid A62ResidentId ;
      private Guid Z528SG_LocationId ;
      private Guid A528SG_LocationId ;
      private Guid Z529SG_OrganisationId ;
      private Guid A529SG_OrganisationId ;
      private Guid Z29LocationId ;
      private Guid A29LocationId ;
      private Guid Z11OrganisationId ;
      private Guid A11OrganisationId ;
      private IGxSession AV12WebSession ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GeneXus.Programs.wwpbaseobjects.SdtWWPContext AV8WWPContext ;
      private WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext AV11TrnContext ;
      private WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext_Attribute AV15TrnContextAtt ;
      private IDataStoreProvider pr_default ;
      private Guid[] BC001P6_A29LocationId ;
      private Guid[] BC001P6_A11OrganisationId ;
      private Guid[] BC001P6_A549MemoId ;
      private string[] BC001P6_A543MemoCategoryName ;
      private string[] BC001P6_A550MemoTitle ;
      private string[] BC001P6_A551MemoDescription ;
      private string[] BC001P6_A552MemoImage ;
      private bool[] BC001P6_n552MemoImage ;
      private string[] BC001P6_A553MemoDocument ;
      private bool[] BC001P6_n553MemoDocument ;
      private DateTime[] BC001P6_A561MemoStartDateTime ;
      private bool[] BC001P6_n561MemoStartDateTime ;
      private DateTime[] BC001P6_A562MemoEndDateTime ;
      private bool[] BC001P6_n562MemoEndDateTime ;
      private short[] BC001P6_A563MemoDuration ;
      private bool[] BC001P6_n563MemoDuration ;
      private DateTime[] BC001P6_A564MemoRemoveDate ;
      private string[] BC001P6_A72ResidentSalutation ;
      private string[] BC001P6_A64ResidentGivenName ;
      private string[] BC001P6_A65ResidentLastName ;
      private string[] BC001P6_A71ResidentGUID ;
      private string[] BC001P6_A566MemoBgColorCode ;
      private string[] BC001P6_A567MemoForm ;
      private Guid[] BC001P6_A542MemoCategoryId ;
      private Guid[] BC001P6_A62ResidentId ;
      private Guid[] BC001P6_A528SG_LocationId ;
      private Guid[] BC001P6_A529SG_OrganisationId ;
      private string[] BC001P4_A543MemoCategoryName ;
      private Guid[] BC001P5_A29LocationId ;
      private Guid[] BC001P5_A11OrganisationId ;
      private string[] BC001P5_A72ResidentSalutation ;
      private string[] BC001P5_A64ResidentGivenName ;
      private string[] BC001P5_A65ResidentLastName ;
      private string[] BC001P5_A71ResidentGUID ;
      private Guid[] BC001P7_A549MemoId ;
      private Guid[] BC001P3_A549MemoId ;
      private string[] BC001P3_A550MemoTitle ;
      private string[] BC001P3_A551MemoDescription ;
      private string[] BC001P3_A552MemoImage ;
      private bool[] BC001P3_n552MemoImage ;
      private string[] BC001P3_A553MemoDocument ;
      private bool[] BC001P3_n553MemoDocument ;
      private DateTime[] BC001P3_A561MemoStartDateTime ;
      private bool[] BC001P3_n561MemoStartDateTime ;
      private DateTime[] BC001P3_A562MemoEndDateTime ;
      private bool[] BC001P3_n562MemoEndDateTime ;
      private short[] BC001P3_A563MemoDuration ;
      private bool[] BC001P3_n563MemoDuration ;
      private DateTime[] BC001P3_A564MemoRemoveDate ;
      private string[] BC001P3_A566MemoBgColorCode ;
      private string[] BC001P3_A567MemoForm ;
      private Guid[] BC001P3_A542MemoCategoryId ;
      private Guid[] BC001P3_A62ResidentId ;
      private Guid[] BC001P3_A528SG_LocationId ;
      private Guid[] BC001P3_A529SG_OrganisationId ;
      private Guid[] BC001P2_A549MemoId ;
      private string[] BC001P2_A550MemoTitle ;
      private string[] BC001P2_A551MemoDescription ;
      private string[] BC001P2_A552MemoImage ;
      private bool[] BC001P2_n552MemoImage ;
      private string[] BC001P2_A553MemoDocument ;
      private bool[] BC001P2_n553MemoDocument ;
      private DateTime[] BC001P2_A561MemoStartDateTime ;
      private bool[] BC001P2_n561MemoStartDateTime ;
      private DateTime[] BC001P2_A562MemoEndDateTime ;
      private bool[] BC001P2_n562MemoEndDateTime ;
      private short[] BC001P2_A563MemoDuration ;
      private bool[] BC001P2_n563MemoDuration ;
      private DateTime[] BC001P2_A564MemoRemoveDate ;
      private string[] BC001P2_A566MemoBgColorCode ;
      private string[] BC001P2_A567MemoForm ;
      private Guid[] BC001P2_A542MemoCategoryId ;
      private Guid[] BC001P2_A62ResidentId ;
      private Guid[] BC001P2_A528SG_LocationId ;
      private Guid[] BC001P2_A529SG_OrganisationId ;
      private string[] BC001P11_A543MemoCategoryName ;
      private string[] BC001P12_A72ResidentSalutation ;
      private string[] BC001P12_A64ResidentGivenName ;
      private string[] BC001P12_A65ResidentLastName ;
      private string[] BC001P12_A71ResidentGUID ;
      private Guid[] BC001P13_A29LocationId ;
      private Guid[] BC001P13_A11OrganisationId ;
      private Guid[] BC001P13_A549MemoId ;
      private string[] BC001P13_A543MemoCategoryName ;
      private string[] BC001P13_A550MemoTitle ;
      private string[] BC001P13_A551MemoDescription ;
      private string[] BC001P13_A552MemoImage ;
      private bool[] BC001P13_n552MemoImage ;
      private string[] BC001P13_A553MemoDocument ;
      private bool[] BC001P13_n553MemoDocument ;
      private DateTime[] BC001P13_A561MemoStartDateTime ;
      private bool[] BC001P13_n561MemoStartDateTime ;
      private DateTime[] BC001P13_A562MemoEndDateTime ;
      private bool[] BC001P13_n562MemoEndDateTime ;
      private short[] BC001P13_A563MemoDuration ;
      private bool[] BC001P13_n563MemoDuration ;
      private DateTime[] BC001P13_A564MemoRemoveDate ;
      private string[] BC001P13_A72ResidentSalutation ;
      private string[] BC001P13_A64ResidentGivenName ;
      private string[] BC001P13_A65ResidentLastName ;
      private string[] BC001P13_A71ResidentGUID ;
      private string[] BC001P13_A566MemoBgColorCode ;
      private string[] BC001P13_A567MemoForm ;
      private Guid[] BC001P13_A542MemoCategoryId ;
      private Guid[] BC001P13_A62ResidentId ;
      private Guid[] BC001P13_A528SG_LocationId ;
      private Guid[] BC001P13_A529SG_OrganisationId ;
      private SdtTrn_Memo bcTrn_Memo ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_datastore1 ;
      private IDataStoreProvider pr_gam ;
   }

   public class trn_memo_bc__datastore1 : DataStoreHelperBase, IDataStoreHelper
   {
      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          def= new CursorDef[] {
          };
       }
    }

    public void getResults( int cursor ,
                            IFieldGetter rslt ,
                            Object[] buf )
    {
    }

    public override string getDataStoreName( )
    {
       return "DATASTORE1";
    }

 }

 public class trn_memo_bc__gam : DataStoreHelperBase, IDataStoreHelper
 {
    public ICursor[] getCursors( )
    {
       cursorDefinitions();
       return new Cursor[] {
     };
  }

  private static CursorDef[] def;
  private void cursorDefinitions( )
  {
     if ( def == null )
     {
        def= new CursorDef[] {
        };
     }
  }

  public void getResults( int cursor ,
                          IFieldGetter rslt ,
                          Object[] buf )
  {
  }

  public override string getDataStoreName( )
  {
     return "GAM";
  }

}

public class trn_memo_bc__default : DataStoreHelperBase, IDataStoreHelper
{
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
      ,new UpdateCursor(def[6])
      ,new UpdateCursor(def[7])
      ,new UpdateCursor(def[8])
      ,new ForEachCursor(def[9])
      ,new ForEachCursor(def[10])
      ,new ForEachCursor(def[11])
    };
 }

 private static CursorDef[] def;
 private void cursorDefinitions( )
 {
    if ( def == null )
    {
       Object[] prmBC001P2;
       prmBC001P2 = new Object[] {
       new ParDef("MemoId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmBC001P3;
       prmBC001P3 = new Object[] {
       new ParDef("MemoId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmBC001P4;
       prmBC001P4 = new Object[] {
       new ParDef("MemoCategoryId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmBC001P5;
       prmBC001P5 = new Object[] {
       new ParDef("ResidentId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("SG_LocationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("SG_OrganisationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmBC001P6;
       prmBC001P6 = new Object[] {
       new ParDef("MemoId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmBC001P7;
       prmBC001P7 = new Object[] {
       new ParDef("MemoId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmBC001P8;
       prmBC001P8 = new Object[] {
       new ParDef("MemoId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("MemoTitle",GXType.VarChar,100,0) ,
       new ParDef("MemoDescription",GXType.VarChar,200,0) ,
       new ParDef("MemoImage",GXType.VarChar,200,0){Nullable=true} ,
       new ParDef("MemoDocument",GXType.VarChar,200,0){Nullable=true} ,
       new ParDef("MemoStartDateTime",GXType.DateTime,8,5){Nullable=true} ,
       new ParDef("MemoEndDateTime",GXType.DateTime,8,5){Nullable=true} ,
       new ParDef("MemoDuration",GXType.Int16,4,0){Nullable=true} ,
       new ParDef("MemoRemoveDate",GXType.Date,8,0) ,
       new ParDef("MemoBgColorCode",GXType.VarChar,100,0) ,
       new ParDef("MemoForm",GXType.Char,20,0) ,
       new ParDef("MemoCategoryId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("ResidentId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("SG_LocationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("SG_OrganisationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmBC001P9;
       prmBC001P9 = new Object[] {
       new ParDef("MemoTitle",GXType.VarChar,100,0) ,
       new ParDef("MemoDescription",GXType.VarChar,200,0) ,
       new ParDef("MemoImage",GXType.VarChar,200,0){Nullable=true} ,
       new ParDef("MemoDocument",GXType.VarChar,200,0){Nullable=true} ,
       new ParDef("MemoStartDateTime",GXType.DateTime,8,5){Nullable=true} ,
       new ParDef("MemoEndDateTime",GXType.DateTime,8,5){Nullable=true} ,
       new ParDef("MemoDuration",GXType.Int16,4,0){Nullable=true} ,
       new ParDef("MemoRemoveDate",GXType.Date,8,0) ,
       new ParDef("MemoBgColorCode",GXType.VarChar,100,0) ,
       new ParDef("MemoForm",GXType.Char,20,0) ,
       new ParDef("MemoCategoryId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("ResidentId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("SG_LocationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("SG_OrganisationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("MemoId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmBC001P10;
       prmBC001P10 = new Object[] {
       new ParDef("MemoId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmBC001P11;
       prmBC001P11 = new Object[] {
       new ParDef("MemoCategoryId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmBC001P12;
       prmBC001P12 = new Object[] {
       new ParDef("ResidentId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("SG_LocationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("SG_OrganisationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmBC001P13;
       prmBC001P13 = new Object[] {
       new ParDef("MemoId",GXType.UniqueIdentifier,36,0)
       };
       def= new CursorDef[] {
           new CursorDef("BC001P2", "SELECT MemoId, MemoTitle, MemoDescription, MemoImage, MemoDocument, MemoStartDateTime, MemoEndDateTime, MemoDuration, MemoRemoveDate, MemoBgColorCode, MemoForm, MemoCategoryId, ResidentId, SG_LocationId, SG_OrganisationId FROM Trn_Memo WHERE MemoId = :MemoId  FOR UPDATE OF Trn_Memo",true, GxErrorMask.GX_NOMASK, false, this,prmBC001P2,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC001P3", "SELECT MemoId, MemoTitle, MemoDescription, MemoImage, MemoDocument, MemoStartDateTime, MemoEndDateTime, MemoDuration, MemoRemoveDate, MemoBgColorCode, MemoForm, MemoCategoryId, ResidentId, SG_LocationId, SG_OrganisationId FROM Trn_Memo WHERE MemoId = :MemoId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC001P3,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC001P4", "SELECT MemoCategoryName FROM Trn_MemoCategory WHERE MemoCategoryId = :MemoCategoryId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC001P4,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC001P5", "SELECT LocationId, OrganisationId, ResidentSalutation, ResidentGivenName, ResidentLastName, ResidentGUID FROM Trn_Resident WHERE ResidentId = :ResidentId AND LocationId = :SG_LocationId AND OrganisationId = :SG_OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC001P5,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC001P6", "SELECT T3.LocationId, T3.OrganisationId, TM1.MemoId, T2.MemoCategoryName, TM1.MemoTitle, TM1.MemoDescription, TM1.MemoImage, TM1.MemoDocument, TM1.MemoStartDateTime, TM1.MemoEndDateTime, TM1.MemoDuration, TM1.MemoRemoveDate, T3.ResidentSalutation, T3.ResidentGivenName, T3.ResidentLastName, T3.ResidentGUID, TM1.MemoBgColorCode, TM1.MemoForm, TM1.MemoCategoryId, TM1.ResidentId, TM1.SG_LocationId, TM1.SG_OrganisationId FROM ((Trn_Memo TM1 INNER JOIN Trn_MemoCategory T2 ON T2.MemoCategoryId = TM1.MemoCategoryId) INNER JOIN Trn_Resident T3 ON T3.ResidentId = TM1.ResidentId AND T3.LocationId = TM1.SG_LocationId AND T3.OrganisationId = TM1.SG_OrganisationId) WHERE TM1.MemoId = :MemoId ORDER BY TM1.MemoId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC001P6,100, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC001P7", "SELECT MemoId FROM Trn_Memo WHERE MemoId = :MemoId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC001P7,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC001P8", "SAVEPOINT gxupdate;INSERT INTO Trn_Memo(MemoId, MemoTitle, MemoDescription, MemoImage, MemoDocument, MemoStartDateTime, MemoEndDateTime, MemoDuration, MemoRemoveDate, MemoBgColorCode, MemoForm, MemoCategoryId, ResidentId, SG_LocationId, SG_OrganisationId) VALUES(:MemoId, :MemoTitle, :MemoDescription, :MemoImage, :MemoDocument, :MemoStartDateTime, :MemoEndDateTime, :MemoDuration, :MemoRemoveDate, :MemoBgColorCode, :MemoForm, :MemoCategoryId, :ResidentId, :SG_LocationId, :SG_OrganisationId);RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT,prmBC001P8)
          ,new CursorDef("BC001P9", "SAVEPOINT gxupdate;UPDATE Trn_Memo SET MemoTitle=:MemoTitle, MemoDescription=:MemoDescription, MemoImage=:MemoImage, MemoDocument=:MemoDocument, MemoStartDateTime=:MemoStartDateTime, MemoEndDateTime=:MemoEndDateTime, MemoDuration=:MemoDuration, MemoRemoveDate=:MemoRemoveDate, MemoBgColorCode=:MemoBgColorCode, MemoForm=:MemoForm, MemoCategoryId=:MemoCategoryId, ResidentId=:ResidentId, SG_LocationId=:SG_LocationId, SG_OrganisationId=:SG_OrganisationId  WHERE MemoId = :MemoId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK,prmBC001P9)
          ,new CursorDef("BC001P10", "SAVEPOINT gxupdate;DELETE FROM Trn_Memo  WHERE MemoId = :MemoId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK,prmBC001P10)
          ,new CursorDef("BC001P11", "SELECT MemoCategoryName FROM Trn_MemoCategory WHERE MemoCategoryId = :MemoCategoryId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC001P11,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC001P12", "SELECT ResidentSalutation, ResidentGivenName, ResidentLastName, ResidentGUID FROM Trn_Resident WHERE ResidentId = :ResidentId AND LocationId = :SG_LocationId AND OrganisationId = :SG_OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC001P12,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC001P13", "SELECT T3.LocationId, T3.OrganisationId, TM1.MemoId, T2.MemoCategoryName, TM1.MemoTitle, TM1.MemoDescription, TM1.MemoImage, TM1.MemoDocument, TM1.MemoStartDateTime, TM1.MemoEndDateTime, TM1.MemoDuration, TM1.MemoRemoveDate, T3.ResidentSalutation, T3.ResidentGivenName, T3.ResidentLastName, T3.ResidentGUID, TM1.MemoBgColorCode, TM1.MemoForm, TM1.MemoCategoryId, TM1.ResidentId, TM1.SG_LocationId, TM1.SG_OrganisationId FROM ((Trn_Memo TM1 INNER JOIN Trn_MemoCategory T2 ON T2.MemoCategoryId = TM1.MemoCategoryId) INNER JOIN Trn_Resident T3 ON T3.ResidentId = TM1.ResidentId AND T3.LocationId = TM1.SG_LocationId AND T3.OrganisationId = TM1.SG_OrganisationId) WHERE TM1.MemoId = :MemoId ORDER BY TM1.MemoId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC001P13,100, GxCacheFrequency.OFF ,true,false )
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
             ((string[]) buf[1])[0] = rslt.getVarchar(2);
             ((string[]) buf[2])[0] = rslt.getVarchar(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((bool[]) buf[4])[0] = rslt.wasNull(4);
             ((string[]) buf[5])[0] = rslt.getVarchar(5);
             ((bool[]) buf[6])[0] = rslt.wasNull(5);
             ((DateTime[]) buf[7])[0] = rslt.getGXDateTime(6);
             ((bool[]) buf[8])[0] = rslt.wasNull(6);
             ((DateTime[]) buf[9])[0] = rslt.getGXDateTime(7);
             ((bool[]) buf[10])[0] = rslt.wasNull(7);
             ((short[]) buf[11])[0] = rslt.getShort(8);
             ((bool[]) buf[12])[0] = rslt.wasNull(8);
             ((DateTime[]) buf[13])[0] = rslt.getGXDate(9);
             ((string[]) buf[14])[0] = rslt.getVarchar(10);
             ((string[]) buf[15])[0] = rslt.getString(11, 20);
             ((Guid[]) buf[16])[0] = rslt.getGuid(12);
             ((Guid[]) buf[17])[0] = rslt.getGuid(13);
             ((Guid[]) buf[18])[0] = rslt.getGuid(14);
             ((Guid[]) buf[19])[0] = rslt.getGuid(15);
             return;
          case 1 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((string[]) buf[1])[0] = rslt.getVarchar(2);
             ((string[]) buf[2])[0] = rslt.getVarchar(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((bool[]) buf[4])[0] = rslt.wasNull(4);
             ((string[]) buf[5])[0] = rslt.getVarchar(5);
             ((bool[]) buf[6])[0] = rslt.wasNull(5);
             ((DateTime[]) buf[7])[0] = rslt.getGXDateTime(6);
             ((bool[]) buf[8])[0] = rslt.wasNull(6);
             ((DateTime[]) buf[9])[0] = rslt.getGXDateTime(7);
             ((bool[]) buf[10])[0] = rslt.wasNull(7);
             ((short[]) buf[11])[0] = rslt.getShort(8);
             ((bool[]) buf[12])[0] = rslt.wasNull(8);
             ((DateTime[]) buf[13])[0] = rslt.getGXDate(9);
             ((string[]) buf[14])[0] = rslt.getVarchar(10);
             ((string[]) buf[15])[0] = rslt.getString(11, 20);
             ((Guid[]) buf[16])[0] = rslt.getGuid(12);
             ((Guid[]) buf[17])[0] = rslt.getGuid(13);
             ((Guid[]) buf[18])[0] = rslt.getGuid(14);
             ((Guid[]) buf[19])[0] = rslt.getGuid(15);
             return;
          case 2 :
             ((string[]) buf[0])[0] = rslt.getVarchar(1);
             return;
          case 3 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             ((string[]) buf[2])[0] = rslt.getString(3, 20);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((string[]) buf[4])[0] = rslt.getVarchar(5);
             ((string[]) buf[5])[0] = rslt.getVarchar(6);
             return;
          case 4 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             ((Guid[]) buf[2])[0] = rslt.getGuid(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((string[]) buf[4])[0] = rslt.getVarchar(5);
             ((string[]) buf[5])[0] = rslt.getVarchar(6);
             ((string[]) buf[6])[0] = rslt.getVarchar(7);
             ((bool[]) buf[7])[0] = rslt.wasNull(7);
             ((string[]) buf[8])[0] = rslt.getVarchar(8);
             ((bool[]) buf[9])[0] = rslt.wasNull(8);
             ((DateTime[]) buf[10])[0] = rslt.getGXDateTime(9);
             ((bool[]) buf[11])[0] = rslt.wasNull(9);
             ((DateTime[]) buf[12])[0] = rslt.getGXDateTime(10);
             ((bool[]) buf[13])[0] = rslt.wasNull(10);
             ((short[]) buf[14])[0] = rslt.getShort(11);
             ((bool[]) buf[15])[0] = rslt.wasNull(11);
             ((DateTime[]) buf[16])[0] = rslt.getGXDate(12);
             ((string[]) buf[17])[0] = rslt.getString(13, 20);
             ((string[]) buf[18])[0] = rslt.getVarchar(14);
             ((string[]) buf[19])[0] = rslt.getVarchar(15);
             ((string[]) buf[20])[0] = rslt.getVarchar(16);
             ((string[]) buf[21])[0] = rslt.getVarchar(17);
             ((string[]) buf[22])[0] = rslt.getString(18, 20);
             ((Guid[]) buf[23])[0] = rslt.getGuid(19);
             ((Guid[]) buf[24])[0] = rslt.getGuid(20);
             ((Guid[]) buf[25])[0] = rslt.getGuid(21);
             ((Guid[]) buf[26])[0] = rslt.getGuid(22);
             return;
          case 5 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             return;
          case 9 :
             ((string[]) buf[0])[0] = rslt.getVarchar(1);
             return;
          case 10 :
             ((string[]) buf[0])[0] = rslt.getString(1, 20);
             ((string[]) buf[1])[0] = rslt.getVarchar(2);
             ((string[]) buf[2])[0] = rslt.getVarchar(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             return;
          case 11 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             ((Guid[]) buf[2])[0] = rslt.getGuid(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((string[]) buf[4])[0] = rslt.getVarchar(5);
             ((string[]) buf[5])[0] = rslt.getVarchar(6);
             ((string[]) buf[6])[0] = rslt.getVarchar(7);
             ((bool[]) buf[7])[0] = rslt.wasNull(7);
             ((string[]) buf[8])[0] = rslt.getVarchar(8);
             ((bool[]) buf[9])[0] = rslt.wasNull(8);
             ((DateTime[]) buf[10])[0] = rslt.getGXDateTime(9);
             ((bool[]) buf[11])[0] = rslt.wasNull(9);
             ((DateTime[]) buf[12])[0] = rslt.getGXDateTime(10);
             ((bool[]) buf[13])[0] = rslt.wasNull(10);
             ((short[]) buf[14])[0] = rslt.getShort(11);
             ((bool[]) buf[15])[0] = rslt.wasNull(11);
             ((DateTime[]) buf[16])[0] = rslt.getGXDate(12);
             ((string[]) buf[17])[0] = rslt.getString(13, 20);
             ((string[]) buf[18])[0] = rslt.getVarchar(14);
             ((string[]) buf[19])[0] = rslt.getVarchar(15);
             ((string[]) buf[20])[0] = rslt.getVarchar(16);
             ((string[]) buf[21])[0] = rslt.getVarchar(17);
             ((string[]) buf[22])[0] = rslt.getString(18, 20);
             ((Guid[]) buf[23])[0] = rslt.getGuid(19);
             ((Guid[]) buf[24])[0] = rslt.getGuid(20);
             ((Guid[]) buf[25])[0] = rslt.getGuid(21);
             ((Guid[]) buf[26])[0] = rslt.getGuid(22);
             return;
    }
 }

}

}
