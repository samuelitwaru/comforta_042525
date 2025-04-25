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
   public class trn_dynamictranslation_bc : GxSilentTrn, IGxSilentTrn
   {
      public trn_dynamictranslation_bc( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public trn_dynamictranslation_bc( IGxContext context )
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
         ReadRow1Q104( ) ;
         standaloneNotModal( ) ;
         InitializeNonKey1Q104( ) ;
         standaloneModal( ) ;
         AddRow1Q104( ) ;
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
            E111Q2 ();
            trnEnded = 0;
            standaloneNotModal( ) ;
            standaloneModal( ) ;
            if ( IsIns( )  )
            {
               Z578DynamicTranslationId = A578DynamicTranslationId;
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

      protected void CONFIRM_1Q0( )
      {
         BeforeValidate1Q104( ) ;
         if ( AnyError == 0 )
         {
            if ( IsDlt( ) )
            {
               OnDeleteControls1Q104( ) ;
            }
            else
            {
               CheckExtendedTable1Q104( ) ;
               if ( AnyError == 0 )
               {
               }
               CloseExtendedTableCursors1Q104( ) ;
            }
         }
         if ( AnyError == 0 )
         {
         }
      }

      protected void E121Q2( )
      {
         /* Start Routine */
         returnInSub = false;
         new GeneXus.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV8WWPContext) ;
         AV11TrnContext.FromXml(AV12WebSession.Get("TrnContext"), null, "", "");
      }

      protected void E111Q2( )
      {
         /* After Trn Routine */
         returnInSub = false;
      }

      protected void ZM1Q104( short GX_JID )
      {
         if ( ( GX_JID == 5 ) || ( GX_JID == 0 ) )
         {
            Z580DynamicTranslationPrimaryKey = A580DynamicTranslationPrimaryKey;
            Z579DynamicTranslationTrnName = A579DynamicTranslationTrnName;
            Z581DynamicTranslationAttributeNam = A581DynamicTranslationAttributeNam;
         }
         if ( GX_JID == -5 )
         {
            Z578DynamicTranslationId = A578DynamicTranslationId;
            Z580DynamicTranslationPrimaryKey = A580DynamicTranslationPrimaryKey;
            Z579DynamicTranslationTrnName = A579DynamicTranslationTrnName;
            Z581DynamicTranslationAttributeNam = A581DynamicTranslationAttributeNam;
            Z582DynamicTranslationEnglish = A582DynamicTranslationEnglish;
            Z583DynamicTranslationDutch = A583DynamicTranslationDutch;
         }
      }

      protected void standaloneNotModal( )
      {
      }

      protected void standaloneModal( )
      {
         if ( IsIns( )  && (Guid.Empty==A580DynamicTranslationPrimaryKey) )
         {
            A580DynamicTranslationPrimaryKey = Guid.NewGuid( );
         }
         if ( IsIns( )  && (Guid.Empty==A578DynamicTranslationId) )
         {
            A578DynamicTranslationId = Guid.NewGuid( );
         }
         if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) && ( Gx_BScreen == 0 ) )
         {
         }
      }

      protected void Load1Q104( )
      {
         /* Using cursor BC001Q4 */
         pr_default.execute(2, new Object[] {A578DynamicTranslationId});
         if ( (pr_default.getStatus(2) != 101) )
         {
            RcdFound104 = 1;
            A580DynamicTranslationPrimaryKey = BC001Q4_A580DynamicTranslationPrimaryKey[0];
            A579DynamicTranslationTrnName = BC001Q4_A579DynamicTranslationTrnName[0];
            A581DynamicTranslationAttributeNam = BC001Q4_A581DynamicTranslationAttributeNam[0];
            A582DynamicTranslationEnglish = BC001Q4_A582DynamicTranslationEnglish[0];
            A583DynamicTranslationDutch = BC001Q4_A583DynamicTranslationDutch[0];
            ZM1Q104( -5) ;
         }
         pr_default.close(2);
         OnLoadActions1Q104( ) ;
      }

      protected void OnLoadActions1Q104( )
      {
      }

      protected void CheckExtendedTable1Q104( )
      {
         standaloneModal( ) ;
      }

      protected void CloseExtendedTableCursors1Q104( )
      {
      }

      protected void enableDisable( )
      {
      }

      protected void GetKey1Q104( )
      {
         /* Using cursor BC001Q5 */
         pr_default.execute(3, new Object[] {A578DynamicTranslationId});
         if ( (pr_default.getStatus(3) != 101) )
         {
            RcdFound104 = 1;
         }
         else
         {
            RcdFound104 = 0;
         }
         pr_default.close(3);
      }

      protected void getByPrimaryKey( )
      {
         /* Using cursor BC001Q3 */
         pr_default.execute(1, new Object[] {A578DynamicTranslationId});
         if ( (pr_default.getStatus(1) != 101) )
         {
            ZM1Q104( 5) ;
            RcdFound104 = 1;
            A578DynamicTranslationId = BC001Q3_A578DynamicTranslationId[0];
            A580DynamicTranslationPrimaryKey = BC001Q3_A580DynamicTranslationPrimaryKey[0];
            A579DynamicTranslationTrnName = BC001Q3_A579DynamicTranslationTrnName[0];
            A581DynamicTranslationAttributeNam = BC001Q3_A581DynamicTranslationAttributeNam[0];
            A582DynamicTranslationEnglish = BC001Q3_A582DynamicTranslationEnglish[0];
            A583DynamicTranslationDutch = BC001Q3_A583DynamicTranslationDutch[0];
            Z578DynamicTranslationId = A578DynamicTranslationId;
            sMode104 = Gx_mode;
            Gx_mode = "DSP";
            standaloneModal( ) ;
            Load1Q104( ) ;
            if ( AnyError == 1 )
            {
               RcdFound104 = 0;
               InitializeNonKey1Q104( ) ;
            }
            Gx_mode = sMode104;
         }
         else
         {
            RcdFound104 = 0;
            InitializeNonKey1Q104( ) ;
            sMode104 = Gx_mode;
            Gx_mode = "DSP";
            standaloneModal( ) ;
            Gx_mode = sMode104;
         }
         pr_default.close(1);
      }

      protected void getEqualNoModal( )
      {
         GetKey1Q104( ) ;
         if ( RcdFound104 == 0 )
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
         CONFIRM_1Q0( ) ;
      }

      protected void update_Check( )
      {
         insert_Check( ) ;
      }

      protected void delete_Check( )
      {
         insert_Check( ) ;
      }

      protected void CheckOptimisticConcurrency1Q104( )
      {
         if ( ! IsIns( ) )
         {
            /* Using cursor BC001Q2 */
            pr_default.execute(0, new Object[] {A578DynamicTranslationId});
            if ( (pr_default.getStatus(0) == 103) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"Trn_DynamicTranslation"}), "RecordIsLocked", 1, "");
               AnyError = 1;
               return  ;
            }
            if ( (pr_default.getStatus(0) == 101) || ( Z580DynamicTranslationPrimaryKey != BC001Q2_A580DynamicTranslationPrimaryKey[0] ) || ( StringUtil.StrCmp(Z579DynamicTranslationTrnName, BC001Q2_A579DynamicTranslationTrnName[0]) != 0 ) || ( StringUtil.StrCmp(Z581DynamicTranslationAttributeNam, BC001Q2_A581DynamicTranslationAttributeNam[0]) != 0 ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_waschg", new   object[]  {"Trn_DynamicTranslation"}), "RecordWasChanged", 1, "");
               AnyError = 1;
               return  ;
            }
         }
      }

      protected void Insert1Q104( )
      {
         BeforeValidate1Q104( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable1Q104( ) ;
         }
         if ( AnyError == 0 )
         {
            ZM1Q104( 0) ;
            CheckOptimisticConcurrency1Q104( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm1Q104( ) ;
               if ( AnyError == 0 )
               {
                  BeforeInsert1Q104( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor BC001Q6 */
                     pr_default.execute(4, new Object[] {A578DynamicTranslationId, A580DynamicTranslationPrimaryKey, A579DynamicTranslationTrnName, A581DynamicTranslationAttributeNam, A582DynamicTranslationEnglish, A583DynamicTranslationDutch});
                     pr_default.close(4);
                     pr_default.SmartCacheProvider.SetUpdated("Trn_DynamicTranslation");
                     if ( (pr_default.getStatus(4) == 1) )
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
               Load1Q104( ) ;
            }
            EndLevel1Q104( ) ;
         }
         CloseExtendedTableCursors1Q104( ) ;
      }

      protected void Update1Q104( )
      {
         BeforeValidate1Q104( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable1Q104( ) ;
         }
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency1Q104( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm1Q104( ) ;
               if ( AnyError == 0 )
               {
                  BeforeUpdate1Q104( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor BC001Q7 */
                     pr_default.execute(5, new Object[] {A580DynamicTranslationPrimaryKey, A579DynamicTranslationTrnName, A581DynamicTranslationAttributeNam, A582DynamicTranslationEnglish, A583DynamicTranslationDutch, A578DynamicTranslationId});
                     pr_default.close(5);
                     pr_default.SmartCacheProvider.SetUpdated("Trn_DynamicTranslation");
                     if ( (pr_default.getStatus(5) == 103) )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"Trn_DynamicTranslation"}), "RecordIsLocked", 1, "");
                        AnyError = 1;
                     }
                     DeferredUpdate1Q104( ) ;
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
            EndLevel1Q104( ) ;
         }
         CloseExtendedTableCursors1Q104( ) ;
      }

      protected void DeferredUpdate1Q104( )
      {
      }

      protected void delete( )
      {
         Gx_mode = "DLT";
         BeforeValidate1Q104( ) ;
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency1Q104( ) ;
         }
         if ( AnyError == 0 )
         {
            OnDeleteControls1Q104( ) ;
            AfterConfirm1Q104( ) ;
            if ( AnyError == 0 )
            {
               BeforeDelete1Q104( ) ;
               if ( AnyError == 0 )
               {
                  /* No cascading delete specified. */
                  /* Using cursor BC001Q8 */
                  pr_default.execute(6, new Object[] {A578DynamicTranslationId});
                  pr_default.close(6);
                  pr_default.SmartCacheProvider.SetUpdated("Trn_DynamicTranslation");
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
         sMode104 = Gx_mode;
         Gx_mode = "DLT";
         EndLevel1Q104( ) ;
         Gx_mode = sMode104;
      }

      protected void OnDeleteControls1Q104( )
      {
         standaloneModal( ) ;
         /* No delete mode formulas found. */
      }

      protected void EndLevel1Q104( )
      {
         if ( ! IsIns( ) )
         {
            pr_default.close(0);
         }
         if ( AnyError == 0 )
         {
            BeforeComplete1Q104( ) ;
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

      public void ScanKeyStart1Q104( )
      {
         /* Scan By routine */
         /* Using cursor BC001Q9 */
         pr_default.execute(7, new Object[] {A578DynamicTranslationId});
         RcdFound104 = 0;
         if ( (pr_default.getStatus(7) != 101) )
         {
            RcdFound104 = 1;
            A578DynamicTranslationId = BC001Q9_A578DynamicTranslationId[0];
            A580DynamicTranslationPrimaryKey = BC001Q9_A580DynamicTranslationPrimaryKey[0];
            A579DynamicTranslationTrnName = BC001Q9_A579DynamicTranslationTrnName[0];
            A581DynamicTranslationAttributeNam = BC001Q9_A581DynamicTranslationAttributeNam[0];
            A582DynamicTranslationEnglish = BC001Q9_A582DynamicTranslationEnglish[0];
            A583DynamicTranslationDutch = BC001Q9_A583DynamicTranslationDutch[0];
         }
         /* Load Subordinate Levels */
      }

      protected void ScanKeyNext1Q104( )
      {
         /* Scan next routine */
         pr_default.readNext(7);
         RcdFound104 = 0;
         ScanKeyLoad1Q104( ) ;
      }

      protected void ScanKeyLoad1Q104( )
      {
         sMode104 = Gx_mode;
         Gx_mode = "DSP";
         if ( (pr_default.getStatus(7) != 101) )
         {
            RcdFound104 = 1;
            A578DynamicTranslationId = BC001Q9_A578DynamicTranslationId[0];
            A580DynamicTranslationPrimaryKey = BC001Q9_A580DynamicTranslationPrimaryKey[0];
            A579DynamicTranslationTrnName = BC001Q9_A579DynamicTranslationTrnName[0];
            A581DynamicTranslationAttributeNam = BC001Q9_A581DynamicTranslationAttributeNam[0];
            A582DynamicTranslationEnglish = BC001Q9_A582DynamicTranslationEnglish[0];
            A583DynamicTranslationDutch = BC001Q9_A583DynamicTranslationDutch[0];
         }
         Gx_mode = sMode104;
      }

      protected void ScanKeyEnd1Q104( )
      {
         pr_default.close(7);
      }

      protected void AfterConfirm1Q104( )
      {
         /* After Confirm Rules */
      }

      protected void BeforeInsert1Q104( )
      {
         /* Before Insert Rules */
      }

      protected void BeforeUpdate1Q104( )
      {
         /* Before Update Rules */
      }

      protected void BeforeDelete1Q104( )
      {
         /* Before Delete Rules */
      }

      protected void BeforeComplete1Q104( )
      {
         /* Before Complete Rules */
      }

      protected void BeforeValidate1Q104( )
      {
         /* Before Validate Rules */
      }

      protected void DisableAttributes1Q104( )
      {
      }

      protected void send_integrity_lvl_hashes1Q104( )
      {
      }

      protected void AddRow1Q104( )
      {
         VarsToRow104( bcTrn_DynamicTranslation) ;
      }

      protected void ReadRow1Q104( )
      {
         RowToVars104( bcTrn_DynamicTranslation, 1) ;
      }

      protected void InitializeNonKey1Q104( )
      {
         A579DynamicTranslationTrnName = "";
         A581DynamicTranslationAttributeNam = "";
         A582DynamicTranslationEnglish = "";
         A583DynamicTranslationDutch = "";
         A580DynamicTranslationPrimaryKey = Guid.NewGuid( );
         Z580DynamicTranslationPrimaryKey = Guid.Empty;
         Z579DynamicTranslationTrnName = "";
         Z581DynamicTranslationAttributeNam = "";
      }

      protected void InitAll1Q104( )
      {
         A578DynamicTranslationId = Guid.NewGuid( );
         InitializeNonKey1Q104( ) ;
      }

      protected void StandaloneModalInsert( )
      {
         A580DynamicTranslationPrimaryKey = i580DynamicTranslationPrimaryKey;
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

      public void VarsToRow104( SdtTrn_DynamicTranslation obj104 )
      {
         obj104.gxTpr_Mode = Gx_mode;
         obj104.gxTpr_Dynamictranslationtrnname = A579DynamicTranslationTrnName;
         obj104.gxTpr_Dynamictranslationattributename = A581DynamicTranslationAttributeNam;
         obj104.gxTpr_Dynamictranslationenglish = A582DynamicTranslationEnglish;
         obj104.gxTpr_Dynamictranslationdutch = A583DynamicTranslationDutch;
         obj104.gxTpr_Dynamictranslationprimarykey = A580DynamicTranslationPrimaryKey;
         obj104.gxTpr_Dynamictranslationid = A578DynamicTranslationId;
         obj104.gxTpr_Dynamictranslationid_Z = Z578DynamicTranslationId;
         obj104.gxTpr_Dynamictranslationtrnname_Z = Z579DynamicTranslationTrnName;
         obj104.gxTpr_Dynamictranslationprimarykey_Z = Z580DynamicTranslationPrimaryKey;
         obj104.gxTpr_Dynamictranslationattributename_Z = Z581DynamicTranslationAttributeNam;
         obj104.gxTpr_Mode = Gx_mode;
         return  ;
      }

      public void KeyVarsToRow104( SdtTrn_DynamicTranslation obj104 )
      {
         obj104.gxTpr_Dynamictranslationid = A578DynamicTranslationId;
         return  ;
      }

      public void RowToVars104( SdtTrn_DynamicTranslation obj104 ,
                                int forceLoad )
      {
         Gx_mode = obj104.gxTpr_Mode;
         A579DynamicTranslationTrnName = obj104.gxTpr_Dynamictranslationtrnname;
         A581DynamicTranslationAttributeNam = obj104.gxTpr_Dynamictranslationattributename;
         A582DynamicTranslationEnglish = obj104.gxTpr_Dynamictranslationenglish;
         A583DynamicTranslationDutch = obj104.gxTpr_Dynamictranslationdutch;
         A580DynamicTranslationPrimaryKey = obj104.gxTpr_Dynamictranslationprimarykey;
         A578DynamicTranslationId = obj104.gxTpr_Dynamictranslationid;
         Z578DynamicTranslationId = obj104.gxTpr_Dynamictranslationid_Z;
         Z579DynamicTranslationTrnName = obj104.gxTpr_Dynamictranslationtrnname_Z;
         Z580DynamicTranslationPrimaryKey = obj104.gxTpr_Dynamictranslationprimarykey_Z;
         Z581DynamicTranslationAttributeNam = obj104.gxTpr_Dynamictranslationattributename_Z;
         Gx_mode = obj104.gxTpr_Mode;
         return  ;
      }

      public void LoadKey( Object[] obj )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         A578DynamicTranslationId = (Guid)getParm(obj,0);
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         InitializeNonKey1Q104( ) ;
         ScanKeyStart1Q104( ) ;
         if ( RcdFound104 == 0 )
         {
            Gx_mode = "INS";
         }
         else
         {
            Gx_mode = "UPD";
            Z578DynamicTranslationId = A578DynamicTranslationId;
         }
         ZM1Q104( -5) ;
         OnLoadActions1Q104( ) ;
         AddRow1Q104( ) ;
         ScanKeyEnd1Q104( ) ;
         if ( RcdFound104 == 0 )
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
         RowToVars104( bcTrn_DynamicTranslation, 0) ;
         ScanKeyStart1Q104( ) ;
         if ( RcdFound104 == 0 )
         {
            Gx_mode = "INS";
         }
         else
         {
            Gx_mode = "UPD";
            Z578DynamicTranslationId = A578DynamicTranslationId;
         }
         ZM1Q104( -5) ;
         OnLoadActions1Q104( ) ;
         AddRow1Q104( ) ;
         ScanKeyEnd1Q104( ) ;
         if ( RcdFound104 == 0 )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_keynfound", ""), "PrimaryKeyNotFound", 1, "");
            AnyError = 1;
         }
         context.GX_msglist = BackMsgLst;
      }

      protected void SaveImpl( )
      {
         GetKey1Q104( ) ;
         if ( IsIns( ) )
         {
            /* Insert record */
            Insert1Q104( ) ;
         }
         else
         {
            if ( RcdFound104 == 1 )
            {
               if ( A578DynamicTranslationId != Z578DynamicTranslationId )
               {
                  A578DynamicTranslationId = Z578DynamicTranslationId;
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
                  Update1Q104( ) ;
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
                  if ( A578DynamicTranslationId != Z578DynamicTranslationId )
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
                        Insert1Q104( ) ;
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
                        Insert1Q104( ) ;
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
         RowToVars104( bcTrn_DynamicTranslation, 1) ;
         SaveImpl( ) ;
         VarsToRow104( bcTrn_DynamicTranslation) ;
         context.GX_msglist = BackMsgLst;
         return  ;
      }

      public bool Insert( )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         RowToVars104( bcTrn_DynamicTranslation, 1) ;
         Gx_mode = "INS";
         /* Insert record */
         Insert1Q104( ) ;
         AfterTrn( ) ;
         VarsToRow104( bcTrn_DynamicTranslation) ;
         context.GX_msglist = BackMsgLst;
         return (AnyError==0) ;
      }

      protected void UpdateImpl( )
      {
         if ( IsUpd( ) )
         {
            SaveImpl( ) ;
            VarsToRow104( bcTrn_DynamicTranslation) ;
         }
         else
         {
            SdtTrn_DynamicTranslation auxBC = new SdtTrn_DynamicTranslation(context);
            IGxSilentTrn auxTrn = auxBC.getTransaction();
            auxBC.Load(A578DynamicTranslationId);
            if ( auxTrn.Errors() == 0 )
            {
               auxBC.UpdateDirties(bcTrn_DynamicTranslation);
               auxBC.Save();
               bcTrn_DynamicTranslation.Copy((GxSilentTrnSdt)(auxBC));
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
         RowToVars104( bcTrn_DynamicTranslation, 1) ;
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
         RowToVars104( bcTrn_DynamicTranslation, 1) ;
         Gx_mode = "INS";
         /* Insert record */
         Insert1Q104( ) ;
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
               VarsToRow104( bcTrn_DynamicTranslation) ;
            }
         }
         else
         {
            AfterTrn( ) ;
            VarsToRow104( bcTrn_DynamicTranslation) ;
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
         RowToVars104( bcTrn_DynamicTranslation, 0) ;
         GetKey1Q104( ) ;
         if ( RcdFound104 == 1 )
         {
            if ( IsIns( ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_noupdate", ""), "DuplicatePrimaryKey", 1, "");
               AnyError = 1;
            }
            else if ( A578DynamicTranslationId != Z578DynamicTranslationId )
            {
               A578DynamicTranslationId = Z578DynamicTranslationId;
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
            if ( A578DynamicTranslationId != Z578DynamicTranslationId )
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
         context.RollbackDataStores("trn_dynamictranslation_bc",pr_default);
         VarsToRow104( bcTrn_DynamicTranslation) ;
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
         Gx_mode = bcTrn_DynamicTranslation.gxTpr_Mode;
         return Gx_mode ;
      }

      public void SetMode( string lMode )
      {
         Gx_mode = lMode;
         bcTrn_DynamicTranslation.gxTpr_Mode = Gx_mode;
         return  ;
      }

      public void SetSDT( GxSilentTrnSdt sdt ,
                          short sdtToBc )
      {
         if ( sdt != bcTrn_DynamicTranslation )
         {
            bcTrn_DynamicTranslation = (SdtTrn_DynamicTranslation)(sdt);
            if ( StringUtil.StrCmp(bcTrn_DynamicTranslation.gxTpr_Mode, "") == 0 )
            {
               bcTrn_DynamicTranslation.gxTpr_Mode = "INS";
            }
            if ( sdtToBc == 1 )
            {
               VarsToRow104( bcTrn_DynamicTranslation) ;
            }
            else
            {
               RowToVars104( bcTrn_DynamicTranslation, 1) ;
            }
         }
         else
         {
            if ( StringUtil.StrCmp(bcTrn_DynamicTranslation.gxTpr_Mode, "") == 0 )
            {
               bcTrn_DynamicTranslation.gxTpr_Mode = "INS";
            }
         }
         return  ;
      }

      public void ReloadFromSDT( )
      {
         RowToVars104( bcTrn_DynamicTranslation, 1) ;
         return  ;
      }

      public void ForceCommitOnExit( )
      {
         return  ;
      }

      public SdtTrn_DynamicTranslation Trn_DynamicTranslation_BC
      {
         get {
            return bcTrn_DynamicTranslation ;
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
            return "trn_dynamictranslation_Execute" ;
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
      }

      public override void initialize( )
      {
         Gx_mode = "";
         endTrnMsgTxt = "";
         endTrnMsgCod = "";
         Z578DynamicTranslationId = Guid.Empty;
         A578DynamicTranslationId = Guid.Empty;
         AV8WWPContext = new GeneXus.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV11TrnContext = new WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext(context);
         AV12WebSession = context.GetSession();
         Z580DynamicTranslationPrimaryKey = Guid.Empty;
         A580DynamicTranslationPrimaryKey = Guid.Empty;
         Z579DynamicTranslationTrnName = "";
         A579DynamicTranslationTrnName = "";
         Z581DynamicTranslationAttributeNam = "";
         A581DynamicTranslationAttributeNam = "";
         Z582DynamicTranslationEnglish = "";
         A582DynamicTranslationEnglish = "";
         Z583DynamicTranslationDutch = "";
         A583DynamicTranslationDutch = "";
         BC001Q4_A578DynamicTranslationId = new Guid[] {Guid.Empty} ;
         BC001Q4_A580DynamicTranslationPrimaryKey = new Guid[] {Guid.Empty} ;
         BC001Q4_A579DynamicTranslationTrnName = new string[] {""} ;
         BC001Q4_A581DynamicTranslationAttributeNam = new string[] {""} ;
         BC001Q4_A582DynamicTranslationEnglish = new string[] {""} ;
         BC001Q4_A583DynamicTranslationDutch = new string[] {""} ;
         BC001Q5_A578DynamicTranslationId = new Guid[] {Guid.Empty} ;
         BC001Q3_A578DynamicTranslationId = new Guid[] {Guid.Empty} ;
         BC001Q3_A580DynamicTranslationPrimaryKey = new Guid[] {Guid.Empty} ;
         BC001Q3_A579DynamicTranslationTrnName = new string[] {""} ;
         BC001Q3_A581DynamicTranslationAttributeNam = new string[] {""} ;
         BC001Q3_A582DynamicTranslationEnglish = new string[] {""} ;
         BC001Q3_A583DynamicTranslationDutch = new string[] {""} ;
         sMode104 = "";
         BC001Q2_A578DynamicTranslationId = new Guid[] {Guid.Empty} ;
         BC001Q2_A580DynamicTranslationPrimaryKey = new Guid[] {Guid.Empty} ;
         BC001Q2_A579DynamicTranslationTrnName = new string[] {""} ;
         BC001Q2_A581DynamicTranslationAttributeNam = new string[] {""} ;
         BC001Q2_A582DynamicTranslationEnglish = new string[] {""} ;
         BC001Q2_A583DynamicTranslationDutch = new string[] {""} ;
         BC001Q9_A578DynamicTranslationId = new Guid[] {Guid.Empty} ;
         BC001Q9_A580DynamicTranslationPrimaryKey = new Guid[] {Guid.Empty} ;
         BC001Q9_A579DynamicTranslationTrnName = new string[] {""} ;
         BC001Q9_A581DynamicTranslationAttributeNam = new string[] {""} ;
         BC001Q9_A582DynamicTranslationEnglish = new string[] {""} ;
         BC001Q9_A583DynamicTranslationDutch = new string[] {""} ;
         i580DynamicTranslationPrimaryKey = Guid.Empty;
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         pr_datastore1 = new DataStoreProvider(context, new GeneXus.Programs.trn_dynamictranslation_bc__datastore1(),
            new Object[][] {
            }
         );
         pr_gam = new DataStoreProvider(context, new GeneXus.Programs.trn_dynamictranslation_bc__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.trn_dynamictranslation_bc__default(),
            new Object[][] {
                new Object[] {
               BC001Q2_A578DynamicTranslationId, BC001Q2_A580DynamicTranslationPrimaryKey, BC001Q2_A579DynamicTranslationTrnName, BC001Q2_A581DynamicTranslationAttributeNam, BC001Q2_A582DynamicTranslationEnglish, BC001Q2_A583DynamicTranslationDutch
               }
               , new Object[] {
               BC001Q3_A578DynamicTranslationId, BC001Q3_A580DynamicTranslationPrimaryKey, BC001Q3_A579DynamicTranslationTrnName, BC001Q3_A581DynamicTranslationAttributeNam, BC001Q3_A582DynamicTranslationEnglish, BC001Q3_A583DynamicTranslationDutch
               }
               , new Object[] {
               BC001Q4_A578DynamicTranslationId, BC001Q4_A580DynamicTranslationPrimaryKey, BC001Q4_A579DynamicTranslationTrnName, BC001Q4_A581DynamicTranslationAttributeNam, BC001Q4_A582DynamicTranslationEnglish, BC001Q4_A583DynamicTranslationDutch
               }
               , new Object[] {
               BC001Q5_A578DynamicTranslationId
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               BC001Q9_A578DynamicTranslationId, BC001Q9_A580DynamicTranslationPrimaryKey, BC001Q9_A579DynamicTranslationTrnName, BC001Q9_A581DynamicTranslationAttributeNam, BC001Q9_A582DynamicTranslationEnglish, BC001Q9_A583DynamicTranslationDutch
               }
            }
         );
         Z580DynamicTranslationPrimaryKey = Guid.NewGuid( );
         A580DynamicTranslationPrimaryKey = Guid.NewGuid( );
         i580DynamicTranslationPrimaryKey = Guid.NewGuid( );
         Z578DynamicTranslationId = Guid.NewGuid( );
         A578DynamicTranslationId = Guid.NewGuid( );
         INITTRN();
         /* Execute Start event if defined. */
         /* Execute user event: Start */
         E121Q2 ();
         standaloneNotModal( ) ;
      }

      private short AnyError ;
      private short Gx_BScreen ;
      private short RcdFound104 ;
      private int trnEnded ;
      private string Gx_mode ;
      private string endTrnMsgTxt ;
      private string endTrnMsgCod ;
      private string sMode104 ;
      private bool returnInSub ;
      private string Z582DynamicTranslationEnglish ;
      private string A582DynamicTranslationEnglish ;
      private string Z583DynamicTranslationDutch ;
      private string A583DynamicTranslationDutch ;
      private string Z579DynamicTranslationTrnName ;
      private string A579DynamicTranslationTrnName ;
      private string Z581DynamicTranslationAttributeNam ;
      private string A581DynamicTranslationAttributeNam ;
      private Guid Z578DynamicTranslationId ;
      private Guid A578DynamicTranslationId ;
      private Guid Z580DynamicTranslationPrimaryKey ;
      private Guid A580DynamicTranslationPrimaryKey ;
      private Guid i580DynamicTranslationPrimaryKey ;
      private IGxSession AV12WebSession ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GeneXus.Programs.wwpbaseobjects.SdtWWPContext AV8WWPContext ;
      private WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext AV11TrnContext ;
      private IDataStoreProvider pr_default ;
      private Guid[] BC001Q4_A578DynamicTranslationId ;
      private Guid[] BC001Q4_A580DynamicTranslationPrimaryKey ;
      private string[] BC001Q4_A579DynamicTranslationTrnName ;
      private string[] BC001Q4_A581DynamicTranslationAttributeNam ;
      private string[] BC001Q4_A582DynamicTranslationEnglish ;
      private string[] BC001Q4_A583DynamicTranslationDutch ;
      private Guid[] BC001Q5_A578DynamicTranslationId ;
      private Guid[] BC001Q3_A578DynamicTranslationId ;
      private Guid[] BC001Q3_A580DynamicTranslationPrimaryKey ;
      private string[] BC001Q3_A579DynamicTranslationTrnName ;
      private string[] BC001Q3_A581DynamicTranslationAttributeNam ;
      private string[] BC001Q3_A582DynamicTranslationEnglish ;
      private string[] BC001Q3_A583DynamicTranslationDutch ;
      private Guid[] BC001Q2_A578DynamicTranslationId ;
      private Guid[] BC001Q2_A580DynamicTranslationPrimaryKey ;
      private string[] BC001Q2_A579DynamicTranslationTrnName ;
      private string[] BC001Q2_A581DynamicTranslationAttributeNam ;
      private string[] BC001Q2_A582DynamicTranslationEnglish ;
      private string[] BC001Q2_A583DynamicTranslationDutch ;
      private Guid[] BC001Q9_A578DynamicTranslationId ;
      private Guid[] BC001Q9_A580DynamicTranslationPrimaryKey ;
      private string[] BC001Q9_A579DynamicTranslationTrnName ;
      private string[] BC001Q9_A581DynamicTranslationAttributeNam ;
      private string[] BC001Q9_A582DynamicTranslationEnglish ;
      private string[] BC001Q9_A583DynamicTranslationDutch ;
      private SdtTrn_DynamicTranslation bcTrn_DynamicTranslation ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_datastore1 ;
      private IDataStoreProvider pr_gam ;
   }

   public class trn_dynamictranslation_bc__datastore1 : DataStoreHelperBase, IDataStoreHelper
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

 public class trn_dynamictranslation_bc__gam : DataStoreHelperBase, IDataStoreHelper
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

public class trn_dynamictranslation_bc__default : DataStoreHelperBase, IDataStoreHelper
{
   public ICursor[] getCursors( )
   {
      cursorDefinitions();
      return new Cursor[] {
       new ForEachCursor(def[0])
      ,new ForEachCursor(def[1])
      ,new ForEachCursor(def[2])
      ,new ForEachCursor(def[3])
      ,new UpdateCursor(def[4])
      ,new UpdateCursor(def[5])
      ,new UpdateCursor(def[6])
      ,new ForEachCursor(def[7])
    };
 }

 private static CursorDef[] def;
 private void cursorDefinitions( )
 {
    if ( def == null )
    {
       Object[] prmBC001Q2;
       prmBC001Q2 = new Object[] {
       new ParDef("DynamicTranslationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmBC001Q3;
       prmBC001Q3 = new Object[] {
       new ParDef("DynamicTranslationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmBC001Q4;
       prmBC001Q4 = new Object[] {
       new ParDef("DynamicTranslationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmBC001Q5;
       prmBC001Q5 = new Object[] {
       new ParDef("DynamicTranslationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmBC001Q6;
       prmBC001Q6 = new Object[] {
       new ParDef("DynamicTranslationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("DynamicTranslationPrimaryKey",GXType.UniqueIdentifier,36,0) ,
       new ParDef("DynamicTranslationTrnName",GXType.VarChar,100,0) ,
       new ParDef("DynamicTranslationAttributeNam",GXType.VarChar,100,0) ,
       new ParDef("DynamicTranslationEnglish",GXType.LongVarChar,2097152,0) ,
       new ParDef("DynamicTranslationDutch",GXType.LongVarChar,2097152,0)
       };
       Object[] prmBC001Q7;
       prmBC001Q7 = new Object[] {
       new ParDef("DynamicTranslationPrimaryKey",GXType.UniqueIdentifier,36,0) ,
       new ParDef("DynamicTranslationTrnName",GXType.VarChar,100,0) ,
       new ParDef("DynamicTranslationAttributeNam",GXType.VarChar,100,0) ,
       new ParDef("DynamicTranslationEnglish",GXType.LongVarChar,2097152,0) ,
       new ParDef("DynamicTranslationDutch",GXType.LongVarChar,2097152,0) ,
       new ParDef("DynamicTranslationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmBC001Q8;
       prmBC001Q8 = new Object[] {
       new ParDef("DynamicTranslationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmBC001Q9;
       prmBC001Q9 = new Object[] {
       new ParDef("DynamicTranslationId",GXType.UniqueIdentifier,36,0)
       };
       def= new CursorDef[] {
           new CursorDef("BC001Q2", "SELECT DynamicTranslationId, DynamicTranslationPrimaryKey, DynamicTranslationTrnName, DynamicTranslationAttributeNam, DynamicTranslationEnglish, DynamicTranslationDutch FROM Trn_DynamicTranslation WHERE DynamicTranslationId = :DynamicTranslationId  FOR UPDATE OF Trn_DynamicTranslation",true, GxErrorMask.GX_NOMASK, false, this,prmBC001Q2,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC001Q3", "SELECT DynamicTranslationId, DynamicTranslationPrimaryKey, DynamicTranslationTrnName, DynamicTranslationAttributeNam, DynamicTranslationEnglish, DynamicTranslationDutch FROM Trn_DynamicTranslation WHERE DynamicTranslationId = :DynamicTranslationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC001Q3,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC001Q4", "SELECT TM1.DynamicTranslationId, TM1.DynamicTranslationPrimaryKey, TM1.DynamicTranslationTrnName, TM1.DynamicTranslationAttributeNam, TM1.DynamicTranslationEnglish, TM1.DynamicTranslationDutch FROM Trn_DynamicTranslation TM1 WHERE TM1.DynamicTranslationId = :DynamicTranslationId ORDER BY TM1.DynamicTranslationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC001Q4,100, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC001Q5", "SELECT DynamicTranslationId FROM Trn_DynamicTranslation WHERE DynamicTranslationId = :DynamicTranslationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC001Q5,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC001Q6", "SAVEPOINT gxupdate;INSERT INTO Trn_DynamicTranslation(DynamicTranslationId, DynamicTranslationPrimaryKey, DynamicTranslationTrnName, DynamicTranslationAttributeNam, DynamicTranslationEnglish, DynamicTranslationDutch) VALUES(:DynamicTranslationId, :DynamicTranslationPrimaryKey, :DynamicTranslationTrnName, :DynamicTranslationAttributeNam, :DynamicTranslationEnglish, :DynamicTranslationDutch);RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT,prmBC001Q6)
          ,new CursorDef("BC001Q7", "SAVEPOINT gxupdate;UPDATE Trn_DynamicTranslation SET DynamicTranslationPrimaryKey=:DynamicTranslationPrimaryKey, DynamicTranslationTrnName=:DynamicTranslationTrnName, DynamicTranslationAttributeNam=:DynamicTranslationAttributeNam, DynamicTranslationEnglish=:DynamicTranslationEnglish, DynamicTranslationDutch=:DynamicTranslationDutch  WHERE DynamicTranslationId = :DynamicTranslationId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK,prmBC001Q7)
          ,new CursorDef("BC001Q8", "SAVEPOINT gxupdate;DELETE FROM Trn_DynamicTranslation  WHERE DynamicTranslationId = :DynamicTranslationId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK,prmBC001Q8)
          ,new CursorDef("BC001Q9", "SELECT TM1.DynamicTranslationId, TM1.DynamicTranslationPrimaryKey, TM1.DynamicTranslationTrnName, TM1.DynamicTranslationAttributeNam, TM1.DynamicTranslationEnglish, TM1.DynamicTranslationDutch FROM Trn_DynamicTranslation TM1 WHERE TM1.DynamicTranslationId = :DynamicTranslationId ORDER BY TM1.DynamicTranslationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC001Q9,100, GxCacheFrequency.OFF ,true,false )
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
             ((string[]) buf[2])[0] = rslt.getVarchar(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
             ((string[]) buf[5])[0] = rslt.getLongVarchar(6);
             return;
          case 1 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             ((string[]) buf[2])[0] = rslt.getVarchar(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
             ((string[]) buf[5])[0] = rslt.getLongVarchar(6);
             return;
          case 2 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             ((string[]) buf[2])[0] = rslt.getVarchar(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
             ((string[]) buf[5])[0] = rslt.getLongVarchar(6);
             return;
          case 3 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             return;
          case 7 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             ((string[]) buf[2])[0] = rslt.getVarchar(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
             ((string[]) buf[5])[0] = rslt.getLongVarchar(6);
             return;
    }
 }

}

}
