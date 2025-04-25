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
   public class trn_location_bc : GxSilentTrn, IGxSilentTrn
   {
      public trn_location_bc( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public trn_location_bc( IGxContext context )
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
         ReadRow04103( ) ;
         standaloneNotModal( ) ;
         InitializeNonKey04103( ) ;
         standaloneModal( ) ;
         AddRow04103( ) ;
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
            E11042 ();
            trnEnded = 0;
            standaloneNotModal( ) ;
            standaloneModal( ) ;
            if ( IsIns( )  )
            {
               Z29LocationId = A29LocationId;
               Z11OrganisationId = A11OrganisationId;
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

      protected void CONFIRM_040( )
      {
         BeforeValidate04103( ) ;
         if ( AnyError == 0 )
         {
            if ( IsDlt( ) )
            {
               OnDeleteControls04103( ) ;
            }
            else
            {
               CheckExtendedTable04103( ) ;
               if ( AnyError == 0 )
               {
                  ZM04103( 32) ;
                  ZM04103( 33) ;
                  ZM04103( 34) ;
                  ZM04103( 35) ;
                  ZM04103( 36) ;
               }
               CloseExtendedTableCursors04103( ) ;
            }
         }
         if ( AnyError == 0 )
         {
         }
      }

      protected void E12042( )
      {
         /* Start Routine */
         returnInSub = false;
         AV39trnName = "Trn_Location";
         AV37AttributeDescription = "LocationDescription";
         AV31ReceptionDescriptionVar = "Welkom bij de receptie van onze app. Hier kunt u al uw vragen stellen en krijgt u direct hulp van ons team. Of het nu gaat om technische ondersteuning, informatie over diensten, of algemene vragen, wij zijn er om u te helpen.";
         imgReceptionimagevar_gximage = "ReceptionImageFile";
         AV30ReceptionImageVar = context.GetImagePath( "7a779875-7e6f-4e4f-8ef6-6c9464d2a2f0", "", context.GetTheme( ));
         AV44Receptionimagevar_GXI = GXDbFile.PathToUrl( context.GetImagePath( "7a779875-7e6f-4e4f-8ef6-6c9464d2a2f0", "", context.GetTheme( )), context);
         new GeneXus.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV9WWPContext) ;
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S112 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         AV12TrnContext.FromXml(AV13WebSession.Get("TrnContext"), null, "", "");
         if ( ( StringUtil.StrCmp(AV12TrnContext.gxTpr_Transactionname, AV45Pgmname) == 0 ) && ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) )
         {
            AV46GXV1 = 1;
            while ( AV46GXV1 <= AV12TrnContext.gxTpr_Attributes.Count )
            {
               AV26TrnContextAtt = ((WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext_Attribute)AV12TrnContext.gxTpr_Attributes.Item(AV46GXV1));
               if ( StringUtil.StrCmp(AV26TrnContextAtt.gxTpr_Attributename, "ActiveAppVersionId") == 0 )
               {
                  AV34Insert_ActiveAppVersionId = StringUtil.StrToGuid( AV26TrnContextAtt.gxTpr_Attributevalue);
               }
               else if ( StringUtil.StrCmp(AV26TrnContextAtt.gxTpr_Attributename, "PublishedActiveAppVersionId") == 0 )
               {
                  AV40Insert_PublishedActiveAppVersionId = StringUtil.StrToGuid( AV26TrnContextAtt.gxTpr_Attributevalue);
               }
               else if ( StringUtil.StrCmp(AV26TrnContextAtt.gxTpr_Attributename, "Trn_ThemeId") == 0 )
               {
                  AV25Insert_Trn_ThemeId = StringUtil.StrToGuid( AV26TrnContextAtt.gxTpr_Attributevalue);
               }
               else if ( StringUtil.StrCmp(AV26TrnContextAtt.gxTpr_Attributename, "LocationThemeId") == 0 )
               {
                  AV32Insert_LocationThemeId = StringUtil.StrToGuid( AV26TrnContextAtt.gxTpr_Attributevalue);
               }
               AV46GXV1 = (int)(AV46GXV1+1);
            }
         }
         if ( StringUtil.StrCmp(Gx_mode, "INS") == 0 )
         {
         }
         if ( StringUtil.StrCmp(Gx_mode, "INS") != 0 )
         {
            GXt_objcol_SdtSDT_FileUploadData1 = AV41FilesToUpdate;
            new prc_getlocationimages(context ).execute(  AV7LocationId, out  GXt_objcol_SdtSDT_FileUploadData1) ;
            AV41FilesToUpdate = GXt_objcol_SdtSDT_FileUploadData1;
         }
      }

      protected void E11042( )
      {
         /* After Trn Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
         {
            new prc_updatelocationimages(context ).execute(  AV7LocationId,  AV42UploadedFiles,  AV41FilesToUpdate) ;
            AV13WebSession.Set("NotificationMessage", "Location Updated successfully");
         }
         if ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 )
         {
            AV13WebSession.Set("NotificationMessage", "Location Deleted successfully");
         }
         if ( StringUtil.StrCmp(Gx_mode, "INS") == 0 )
         {
            AV13WebSession.Set("NotificationMessage", "Location Inserted successfully");
         }
      }

      protected void S112( )
      {
         /* 'ATTRIBUTESSECURITYCODE' Routine */
         returnInSub = false;
      }

      protected void ZM04103( short GX_JID )
      {
         if ( ( GX_JID == 31 ) || ( GX_JID == 0 ) )
         {
            Z35LocationPhone = A35LocationPhone;
            Z329LocationZipCode = A329LocationZipCode;
            Z31LocationName = A31LocationName;
            Z327LocationCountry = A327LocationCountry;
            Z328LocationCity = A328LocationCity;
            Z330LocationAddressLine1 = A330LocationAddressLine1;
            Z331LocationAddressLine2 = A331LocationAddressLine2;
            Z34LocationEmail = A34LocationEmail;
            Z355LocationPhoneCode = A355LocationPhoneCode;
            Z356LocationPhoneNumber = A356LocationPhoneNumber;
            Z570LocationHasMyCare = A570LocationHasMyCare;
            Z571LocationHasMyServices = A571LocationHasMyServices;
            Z572LocationHasMyLiving = A572LocationHasMyLiving;
            Z573LocationHasOwnBrand = A573LocationHasOwnBrand;
            Z504ToolBoxDefaultProfileImage = A504ToolBoxDefaultProfileImage;
            Z503ToolBoxDefaultLogo = A503ToolBoxDefaultLogo;
            Z575ReceptionDescription = A575ReceptionDescription;
            Z273Trn_ThemeId = A273Trn_ThemeId;
            Z576LocationThemeId = A576LocationThemeId;
            Z584ActiveAppVersionId = A584ActiveAppVersionId;
            Z598PublishedActiveAppVersionId = A598PublishedActiveAppVersionId;
         }
         if ( ( GX_JID == 32 ) || ( GX_JID == 0 ) )
         {
         }
         if ( ( GX_JID == 33 ) || ( GX_JID == 0 ) )
         {
         }
         if ( ( GX_JID == 34 ) || ( GX_JID == 0 ) )
         {
         }
         if ( ( GX_JID == 35 ) || ( GX_JID == 0 ) )
         {
            Z598PublishedActiveAppVersionId = A523AppVersionId;
         }
         if ( ( GX_JID == 36 ) || ( GX_JID == 0 ) )
         {
            Z584ActiveAppVersionId = A523AppVersionId;
         }
         if ( GX_JID == -31 )
         {
            Z29LocationId = A29LocationId;
            Z35LocationPhone = A35LocationPhone;
            Z329LocationZipCode = A329LocationZipCode;
            Z31LocationName = A31LocationName;
            Z494LocationImage = A494LocationImage;
            Z40000LocationImage_GXI = A40000LocationImage_GXI;
            Z327LocationCountry = A327LocationCountry;
            Z328LocationCity = A328LocationCity;
            Z330LocationAddressLine1 = A330LocationAddressLine1;
            Z331LocationAddressLine2 = A331LocationAddressLine2;
            Z34LocationEmail = A34LocationEmail;
            Z355LocationPhoneCode = A355LocationPhoneCode;
            Z356LocationPhoneNumber = A356LocationPhoneNumber;
            Z36LocationDescription = A36LocationDescription;
            Z568LocationBrandTheme = A568LocationBrandTheme;
            Z569LocationCtaTheme = A569LocationCtaTheme;
            Z570LocationHasMyCare = A570LocationHasMyCare;
            Z571LocationHasMyServices = A571LocationHasMyServices;
            Z572LocationHasMyLiving = A572LocationHasMyLiving;
            Z573LocationHasOwnBrand = A573LocationHasOwnBrand;
            Z504ToolBoxDefaultProfileImage = A504ToolBoxDefaultProfileImage;
            Z503ToolBoxDefaultLogo = A503ToolBoxDefaultLogo;
            Z574ReceptionImage = A574ReceptionImage;
            Z40001ReceptionImage_GXI = A40001ReceptionImage_GXI;
            Z575ReceptionDescription = A575ReceptionDescription;
            Z11OrganisationId = A11OrganisationId;
            Z273Trn_ThemeId = A273Trn_ThemeId;
            Z576LocationThemeId = A576LocationThemeId;
            Z584ActiveAppVersionId = A584ActiveAppVersionId;
            Z598PublishedActiveAppVersionId = A598PublishedActiveAppVersionId;
         }
      }

      protected void standaloneNotModal( )
      {
         AV45Pgmname = "Trn_Location_BC";
         Gx_BScreen = 0;
      }

      protected void standaloneModal( )
      {
         if ( IsIns( )  && (Guid.Empty==A29LocationId) )
         {
            A29LocationId = Guid.NewGuid( );
            n29LocationId = false;
         }
         if ( IsIns( )  && (false==A573LocationHasOwnBrand) && ( Gx_BScreen == 0 ) )
         {
            A573LocationHasOwnBrand = false;
         }
         if ( IsIns( )  && (false==A572LocationHasMyLiving) && ( Gx_BScreen == 0 ) )
         {
            A572LocationHasMyLiving = false;
         }
         if ( IsIns( )  && (false==A571LocationHasMyServices) && ( Gx_BScreen == 0 ) )
         {
            A571LocationHasMyServices = false;
         }
         if ( IsIns( )  && (false==A570LocationHasMyCare) && ( Gx_BScreen == 0 ) )
         {
            A570LocationHasMyCare = false;
         }
         if ( IsIns( )  && String.IsNullOrEmpty(StringUtil.RTrim( A575ReceptionDescription)) && ( Gx_BScreen == 0 ) )
         {
            A575ReceptionDescription = AV31ReceptionDescriptionVar;
            n575ReceptionDescription = false;
         }
         if ( IsIns( )  && String.IsNullOrEmpty(StringUtil.RTrim( A574ReceptionImage)) && ( Gx_BScreen == 0 ) )
         {
            A574ReceptionImage = AV30ReceptionImageVar;
            n574ReceptionImage = false;
         }
         if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) && ( Gx_BScreen == 0 ) )
         {
         }
      }

      protected void Load04103( )
      {
         /* Using cursor BC00049 */
         pr_default.execute(7, new Object[] {n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
         if ( (pr_default.getStatus(7) != 101) )
         {
            RcdFound103 = 1;
            A35LocationPhone = BC00049_A35LocationPhone[0];
            A329LocationZipCode = BC00049_A329LocationZipCode[0];
            A31LocationName = BC00049_A31LocationName[0];
            A40000LocationImage_GXI = BC00049_A40000LocationImage_GXI[0];
            A327LocationCountry = BC00049_A327LocationCountry[0];
            A328LocationCity = BC00049_A328LocationCity[0];
            A330LocationAddressLine1 = BC00049_A330LocationAddressLine1[0];
            A331LocationAddressLine2 = BC00049_A331LocationAddressLine2[0];
            A34LocationEmail = BC00049_A34LocationEmail[0];
            A355LocationPhoneCode = BC00049_A355LocationPhoneCode[0];
            A356LocationPhoneNumber = BC00049_A356LocationPhoneNumber[0];
            A36LocationDescription = BC00049_A36LocationDescription[0];
            A568LocationBrandTheme = BC00049_A568LocationBrandTheme[0];
            n568LocationBrandTheme = BC00049_n568LocationBrandTheme[0];
            A569LocationCtaTheme = BC00049_A569LocationCtaTheme[0];
            n569LocationCtaTheme = BC00049_n569LocationCtaTheme[0];
            A570LocationHasMyCare = BC00049_A570LocationHasMyCare[0];
            A571LocationHasMyServices = BC00049_A571LocationHasMyServices[0];
            A572LocationHasMyLiving = BC00049_A572LocationHasMyLiving[0];
            A573LocationHasOwnBrand = BC00049_A573LocationHasOwnBrand[0];
            A504ToolBoxDefaultProfileImage = BC00049_A504ToolBoxDefaultProfileImage[0];
            n504ToolBoxDefaultProfileImage = BC00049_n504ToolBoxDefaultProfileImage[0];
            A503ToolBoxDefaultLogo = BC00049_A503ToolBoxDefaultLogo[0];
            n503ToolBoxDefaultLogo = BC00049_n503ToolBoxDefaultLogo[0];
            A40001ReceptionImage_GXI = BC00049_A40001ReceptionImage_GXI[0];
            n40001ReceptionImage_GXI = BC00049_n40001ReceptionImage_GXI[0];
            A575ReceptionDescription = BC00049_A575ReceptionDescription[0];
            n575ReceptionDescription = BC00049_n575ReceptionDescription[0];
            A273Trn_ThemeId = BC00049_A273Trn_ThemeId[0];
            n273Trn_ThemeId = BC00049_n273Trn_ThemeId[0];
            A576LocationThemeId = BC00049_A576LocationThemeId[0];
            n576LocationThemeId = BC00049_n576LocationThemeId[0];
            A584ActiveAppVersionId = BC00049_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = BC00049_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = BC00049_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = BC00049_n598PublishedActiveAppVersionId[0];
            A494LocationImage = BC00049_A494LocationImage[0];
            A574ReceptionImage = BC00049_A574ReceptionImage[0];
            n574ReceptionImage = BC00049_n574ReceptionImage[0];
            ZM04103( -31) ;
         }
         pr_default.close(7);
         OnLoadActions04103( ) ;
      }

      protected void OnLoadActions04103( )
      {
         GXt_char2 = AV38LocationDescriptionVar;
         new prc_getdisplayvalue(context ).execute(  A36LocationDescription,  AV39trnName,  AV37AttributeDescription,  A29LocationId, out  GXt_char2) ;
         AV38LocationDescriptionVar = GXt_char2;
         A329LocationZipCode = StringUtil.Upper( A329LocationZipCode);
         GXt_char2 = A35LocationPhone;
         new prc_concatenateintlphone(context ).execute(  A355LocationPhoneCode,  A356LocationPhoneNumber, out  GXt_char2) ;
         A35LocationPhone = GXt_char2;
         if ( (Guid.Empty==A584ActiveAppVersionId) )
         {
            A584ActiveAppVersionId = Guid.Empty;
            n584ActiveAppVersionId = false;
            n584ActiveAppVersionId = true;
         }
         /* Using cursor BC00047 */
         pr_default.execute(5, new Object[] {n584ActiveAppVersionId, A584ActiveAppVersionId});
         pr_default.close(5);
         if ( (Guid.Empty==A598PublishedActiveAppVersionId) )
         {
            A598PublishedActiveAppVersionId = Guid.Empty;
            n598PublishedActiveAppVersionId = false;
            n598PublishedActiveAppVersionId = true;
         }
         /* Using cursor BC00048 */
         pr_default.execute(6, new Object[] {n598PublishedActiveAppVersionId, A598PublishedActiveAppVersionId});
         pr_default.close(6);
         if ( (Guid.Empty==A576LocationThemeId) )
         {
            A576LocationThemeId = Guid.Empty;
            n576LocationThemeId = false;
            n576LocationThemeId = true;
         }
         if ( (Guid.Empty==A273Trn_ThemeId) )
         {
            A273Trn_ThemeId = Guid.Empty;
            n273Trn_ThemeId = false;
            n273Trn_ThemeId = true;
         }
         else
         {
            if ( IsIns( )  && (Guid.Empty==A273Trn_ThemeId) && ( Gx_BScreen == 0 ) )
            {
               GXt_guid3 = A273Trn_ThemeId;
               new prc_getdefaulttheme(context ).execute( out  GXt_guid3) ;
               A273Trn_ThemeId = GXt_guid3;
               n273Trn_ThemeId = false;
            }
         }
      }

      protected void CheckExtendedTable04103( )
      {
         standaloneModal( ) ;
         GXt_char2 = AV38LocationDescriptionVar;
         new prc_getdisplayvalue(context ).execute(  A36LocationDescription,  AV39trnName,  AV37AttributeDescription,  A29LocationId, out  GXt_char2) ;
         AV38LocationDescriptionVar = GXt_char2;
         /* Using cursor BC00044 */
         pr_default.execute(2, new Object[] {n11OrganisationId, A11OrganisationId});
         if ( (pr_default.getStatus(2) == 101) )
         {
            GX_msglist.addItem("No matching 'Trn_Organisation'.", "ForeignKeyNotFound", 1, "ORGANISATIONID");
            AnyError = 1;
         }
         pr_default.close(2);
         A329LocationZipCode = StringUtil.Upper( A329LocationZipCode);
         if ( ! GxRegex.IsMatch(A329LocationZipCode,"^\\d{4}\\s?[A-Z]{2}$") && ! String.IsNullOrEmpty(StringUtil.RTrim( A329LocationZipCode)) )
         {
            GX_msglist.addItem("Zip Code is incorrect", 1, "");
            AnyError = 1;
         }
         if ( ! ( GxRegex.IsMatch(A34LocationEmail,"^((\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*)|(\\s*))$") ) )
         {
            GX_msglist.addItem("Invalid email pattern", "OutOfRange", 1, "");
            AnyError = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( A34LocationEmail)) && ! GxRegex.IsMatch(A34LocationEmail,"^((\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*)|(\\s*))$") )
         {
            GX_msglist.addItem("Email format is invalid", 1, "");
            AnyError = 1;
         }
         GXt_char2 = A35LocationPhone;
         new prc_concatenateintlphone(context ).execute(  A355LocationPhoneCode,  A356LocationPhoneNumber, out  GXt_char2) ;
         A35LocationPhone = GXt_char2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( A356LocationPhoneNumber)) && ! GxRegex.IsMatch(A356LocationPhoneNumber,"^\\d{9}$") )
         {
            GX_msglist.addItem("Phone contains 9 digits", 1, "");
            AnyError = 1;
         }
         if ( (Guid.Empty==A584ActiveAppVersionId) )
         {
            A584ActiveAppVersionId = Guid.Empty;
            n584ActiveAppVersionId = false;
            n584ActiveAppVersionId = true;
         }
         /* Using cursor BC00047 */
         pr_default.execute(5, new Object[] {n584ActiveAppVersionId, A584ActiveAppVersionId});
         if ( (pr_default.getStatus(5) == 101) )
         {
            if ( ! ( (Guid.Empty==A584ActiveAppVersionId) ) )
            {
               GX_msglist.addItem("No matching ''.", "ForeignKeyNotFound", 1, "ACTIVEAPPVERSIONID");
               AnyError = 1;
            }
         }
         pr_default.close(5);
         if ( (Guid.Empty==A598PublishedActiveAppVersionId) )
         {
            A598PublishedActiveAppVersionId = Guid.Empty;
            n598PublishedActiveAppVersionId = false;
            n598PublishedActiveAppVersionId = true;
         }
         /* Using cursor BC00048 */
         pr_default.execute(6, new Object[] {n598PublishedActiveAppVersionId, A598PublishedActiveAppVersionId});
         if ( (pr_default.getStatus(6) == 101) )
         {
            if ( ! ( (Guid.Empty==A598PublishedActiveAppVersionId) ) )
            {
               GX_msglist.addItem("No matching ''.", "ForeignKeyNotFound", 1, "PUBLISHEDACTIVEAPPVERSIONID");
               AnyError = 1;
            }
         }
         pr_default.close(6);
         if ( (Guid.Empty==A576LocationThemeId) )
         {
            A576LocationThemeId = Guid.Empty;
            n576LocationThemeId = false;
            n576LocationThemeId = true;
         }
         /* Using cursor BC00046 */
         pr_default.execute(4, new Object[] {n576LocationThemeId, A576LocationThemeId});
         if ( (pr_default.getStatus(4) == 101) )
         {
            if ( ! ( (Guid.Empty==A576LocationThemeId) ) )
            {
               GX_msglist.addItem("No matching 'SG_Location Theme'.", "ForeignKeyNotFound", 1, "LOCATIONTHEMEID");
               AnyError = 1;
            }
         }
         pr_default.close(4);
         if ( (Guid.Empty==A273Trn_ThemeId) )
         {
            A273Trn_ThemeId = Guid.Empty;
            n273Trn_ThemeId = false;
            n273Trn_ThemeId = true;
         }
         else
         {
            if ( IsIns( )  && (Guid.Empty==A273Trn_ThemeId) && ( Gx_BScreen == 0 ) )
            {
               GXt_guid3 = A273Trn_ThemeId;
               new prc_getdefaulttheme(context ).execute( out  GXt_guid3) ;
               A273Trn_ThemeId = GXt_guid3;
               n273Trn_ThemeId = false;
            }
         }
         /* Using cursor BC00045 */
         pr_default.execute(3, new Object[] {n273Trn_ThemeId, A273Trn_ThemeId});
         if ( (pr_default.getStatus(3) == 101) )
         {
            if ( ! ( (Guid.Empty==A273Trn_ThemeId) ) )
            {
               GX_msglist.addItem("No matching ''.", "ForeignKeyNotFound", 1, "TRN_THEMEID");
               AnyError = 1;
            }
         }
         pr_default.close(3);
      }

      protected void CloseExtendedTableCursors04103( )
      {
         pr_default.close(2);
         pr_default.close(5);
         pr_default.close(6);
         pr_default.close(4);
         pr_default.close(3);
      }

      protected void enableDisable( )
      {
      }

      protected void GetKey04103( )
      {
         /* Using cursor BC000410 */
         pr_default.execute(8, new Object[] {n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
         if ( (pr_default.getStatus(8) != 101) )
         {
            RcdFound103 = 1;
         }
         else
         {
            RcdFound103 = 0;
         }
         pr_default.close(8);
      }

      protected void getByPrimaryKey( )
      {
         /* Using cursor BC00043 */
         pr_default.execute(1, new Object[] {n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
         if ( (pr_default.getStatus(1) != 101) )
         {
            ZM04103( 31) ;
            RcdFound103 = 1;
            A29LocationId = BC00043_A29LocationId[0];
            n29LocationId = BC00043_n29LocationId[0];
            A35LocationPhone = BC00043_A35LocationPhone[0];
            A329LocationZipCode = BC00043_A329LocationZipCode[0];
            A31LocationName = BC00043_A31LocationName[0];
            A40000LocationImage_GXI = BC00043_A40000LocationImage_GXI[0];
            A327LocationCountry = BC00043_A327LocationCountry[0];
            A328LocationCity = BC00043_A328LocationCity[0];
            A330LocationAddressLine1 = BC00043_A330LocationAddressLine1[0];
            A331LocationAddressLine2 = BC00043_A331LocationAddressLine2[0];
            A34LocationEmail = BC00043_A34LocationEmail[0];
            A355LocationPhoneCode = BC00043_A355LocationPhoneCode[0];
            A356LocationPhoneNumber = BC00043_A356LocationPhoneNumber[0];
            A36LocationDescription = BC00043_A36LocationDescription[0];
            A568LocationBrandTheme = BC00043_A568LocationBrandTheme[0];
            n568LocationBrandTheme = BC00043_n568LocationBrandTheme[0];
            A569LocationCtaTheme = BC00043_A569LocationCtaTheme[0];
            n569LocationCtaTheme = BC00043_n569LocationCtaTheme[0];
            A570LocationHasMyCare = BC00043_A570LocationHasMyCare[0];
            A571LocationHasMyServices = BC00043_A571LocationHasMyServices[0];
            A572LocationHasMyLiving = BC00043_A572LocationHasMyLiving[0];
            A573LocationHasOwnBrand = BC00043_A573LocationHasOwnBrand[0];
            A504ToolBoxDefaultProfileImage = BC00043_A504ToolBoxDefaultProfileImage[0];
            n504ToolBoxDefaultProfileImage = BC00043_n504ToolBoxDefaultProfileImage[0];
            A503ToolBoxDefaultLogo = BC00043_A503ToolBoxDefaultLogo[0];
            n503ToolBoxDefaultLogo = BC00043_n503ToolBoxDefaultLogo[0];
            A40001ReceptionImage_GXI = BC00043_A40001ReceptionImage_GXI[0];
            n40001ReceptionImage_GXI = BC00043_n40001ReceptionImage_GXI[0];
            A575ReceptionDescription = BC00043_A575ReceptionDescription[0];
            n575ReceptionDescription = BC00043_n575ReceptionDescription[0];
            A11OrganisationId = BC00043_A11OrganisationId[0];
            n11OrganisationId = BC00043_n11OrganisationId[0];
            A273Trn_ThemeId = BC00043_A273Trn_ThemeId[0];
            n273Trn_ThemeId = BC00043_n273Trn_ThemeId[0];
            A576LocationThemeId = BC00043_A576LocationThemeId[0];
            n576LocationThemeId = BC00043_n576LocationThemeId[0];
            A584ActiveAppVersionId = BC00043_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = BC00043_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = BC00043_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = BC00043_n598PublishedActiveAppVersionId[0];
            A494LocationImage = BC00043_A494LocationImage[0];
            A574ReceptionImage = BC00043_A574ReceptionImage[0];
            n574ReceptionImage = BC00043_n574ReceptionImage[0];
            Z29LocationId = A29LocationId;
            Z11OrganisationId = A11OrganisationId;
            sMode103 = Gx_mode;
            Gx_mode = "DSP";
            standaloneModal( ) ;
            Load04103( ) ;
            if ( AnyError == 1 )
            {
               RcdFound103 = 0;
               InitializeNonKey04103( ) ;
            }
            Gx_mode = sMode103;
         }
         else
         {
            RcdFound103 = 0;
            InitializeNonKey04103( ) ;
            sMode103 = Gx_mode;
            Gx_mode = "DSP";
            standaloneModal( ) ;
            Gx_mode = sMode103;
         }
         pr_default.close(1);
      }

      protected void getEqualNoModal( )
      {
         GetKey04103( ) ;
         if ( RcdFound103 == 0 )
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
         CONFIRM_040( ) ;
      }

      protected void update_Check( )
      {
         insert_Check( ) ;
      }

      protected void delete_Check( )
      {
         insert_Check( ) ;
      }

      protected void CheckOptimisticConcurrency04103( )
      {
         if ( ! IsIns( ) )
         {
            /* Using cursor BC00042 */
            pr_default.execute(0, new Object[] {n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
            if ( (pr_default.getStatus(0) == 103) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"Trn_Location"}), "RecordIsLocked", 1, "");
               AnyError = 1;
               return  ;
            }
            Gx_longc = false;
            if ( (pr_default.getStatus(0) == 101) || ( StringUtil.StrCmp(Z35LocationPhone, BC00042_A35LocationPhone[0]) != 0 ) || ( StringUtil.StrCmp(Z329LocationZipCode, BC00042_A329LocationZipCode[0]) != 0 ) || ( StringUtil.StrCmp(Z31LocationName, BC00042_A31LocationName[0]) != 0 ) || ( StringUtil.StrCmp(Z327LocationCountry, BC00042_A327LocationCountry[0]) != 0 ) || ( StringUtil.StrCmp(Z328LocationCity, BC00042_A328LocationCity[0]) != 0 ) )
            {
               Gx_longc = true;
            }
            if ( Gx_longc || ( StringUtil.StrCmp(Z330LocationAddressLine1, BC00042_A330LocationAddressLine1[0]) != 0 ) || ( StringUtil.StrCmp(Z331LocationAddressLine2, BC00042_A331LocationAddressLine2[0]) != 0 ) || ( StringUtil.StrCmp(Z34LocationEmail, BC00042_A34LocationEmail[0]) != 0 ) || ( StringUtil.StrCmp(Z355LocationPhoneCode, BC00042_A355LocationPhoneCode[0]) != 0 ) || ( StringUtil.StrCmp(Z356LocationPhoneNumber, BC00042_A356LocationPhoneNumber[0]) != 0 ) )
            {
               Gx_longc = true;
            }
            if ( Gx_longc || ( Z570LocationHasMyCare != BC00042_A570LocationHasMyCare[0] ) || ( Z571LocationHasMyServices != BC00042_A571LocationHasMyServices[0] ) || ( Z572LocationHasMyLiving != BC00042_A572LocationHasMyLiving[0] ) || ( Z573LocationHasOwnBrand != BC00042_A573LocationHasOwnBrand[0] ) || ( StringUtil.StrCmp(Z504ToolBoxDefaultProfileImage, BC00042_A504ToolBoxDefaultProfileImage[0]) != 0 ) )
            {
               Gx_longc = true;
            }
            if ( Gx_longc || ( StringUtil.StrCmp(Z503ToolBoxDefaultLogo, BC00042_A503ToolBoxDefaultLogo[0]) != 0 ) || ( StringUtil.StrCmp(Z575ReceptionDescription, BC00042_A575ReceptionDescription[0]) != 0 ) || ( Z273Trn_ThemeId != BC00042_A273Trn_ThemeId[0] ) || ( Z576LocationThemeId != BC00042_A576LocationThemeId[0] ) || ( Z584ActiveAppVersionId != BC00042_A584ActiveAppVersionId[0] ) )
            {
               Gx_longc = true;
            }
            if ( Gx_longc || ( Z598PublishedActiveAppVersionId != BC00042_A598PublishedActiveAppVersionId[0] ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_waschg", new   object[]  {"Trn_Location"}), "RecordWasChanged", 1, "");
               AnyError = 1;
               return  ;
            }
         }
      }

      protected void Insert04103( )
      {
         BeforeValidate04103( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable04103( ) ;
         }
         if ( AnyError == 0 )
         {
            ZM04103( 0) ;
            CheckOptimisticConcurrency04103( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm04103( ) ;
               if ( AnyError == 0 )
               {
                  BeforeInsert04103( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor BC000411 */
                     pr_default.execute(9, new Object[] {n29LocationId, A29LocationId, A35LocationPhone, A329LocationZipCode, A31LocationName, A494LocationImage, A40000LocationImage_GXI, A327LocationCountry, A328LocationCity, A330LocationAddressLine1, A331LocationAddressLine2, A34LocationEmail, A355LocationPhoneCode, A356LocationPhoneNumber, A36LocationDescription, n568LocationBrandTheme, A568LocationBrandTheme, n569LocationCtaTheme, A569LocationCtaTheme, A570LocationHasMyCare, A571LocationHasMyServices, A572LocationHasMyLiving, A573LocationHasOwnBrand, n504ToolBoxDefaultProfileImage, A504ToolBoxDefaultProfileImage, n503ToolBoxDefaultLogo, A503ToolBoxDefaultLogo, n574ReceptionImage, A574ReceptionImage, n40001ReceptionImage_GXI, A40001ReceptionImage_GXI, n575ReceptionDescription, A575ReceptionDescription, n11OrganisationId, A11OrganisationId, n273Trn_ThemeId, A273Trn_ThemeId, n576LocationThemeId, A576LocationThemeId, n584ActiveAppVersionId, A584ActiveAppVersionId, n598PublishedActiveAppVersionId, A598PublishedActiveAppVersionId});
                     pr_default.close(9);
                     pr_default.SmartCacheProvider.SetUpdated("Trn_Location");
                     if ( (pr_default.getStatus(9) == 1) )
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
               Load04103( ) ;
            }
            EndLevel04103( ) ;
         }
         CloseExtendedTableCursors04103( ) ;
      }

      protected void Update04103( )
      {
         BeforeValidate04103( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable04103( ) ;
         }
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency04103( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm04103( ) ;
               if ( AnyError == 0 )
               {
                  BeforeUpdate04103( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor BC000412 */
                     pr_default.execute(10, new Object[] {A35LocationPhone, A329LocationZipCode, A31LocationName, A327LocationCountry, A328LocationCity, A330LocationAddressLine1, A331LocationAddressLine2, A34LocationEmail, A355LocationPhoneCode, A356LocationPhoneNumber, A36LocationDescription, n568LocationBrandTheme, A568LocationBrandTheme, n569LocationCtaTheme, A569LocationCtaTheme, A570LocationHasMyCare, A571LocationHasMyServices, A572LocationHasMyLiving, A573LocationHasOwnBrand, n504ToolBoxDefaultProfileImage, A504ToolBoxDefaultProfileImage, n503ToolBoxDefaultLogo, A503ToolBoxDefaultLogo, n575ReceptionDescription, A575ReceptionDescription, n273Trn_ThemeId, A273Trn_ThemeId, n576LocationThemeId, A576LocationThemeId, n584ActiveAppVersionId, A584ActiveAppVersionId, n598PublishedActiveAppVersionId, A598PublishedActiveAppVersionId, n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
                     pr_default.close(10);
                     pr_default.SmartCacheProvider.SetUpdated("Trn_Location");
                     if ( (pr_default.getStatus(10) == 103) )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"Trn_Location"}), "RecordIsLocked", 1, "");
                        AnyError = 1;
                     }
                     DeferredUpdate04103( ) ;
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
            EndLevel04103( ) ;
         }
         CloseExtendedTableCursors04103( ) ;
      }

      protected void DeferredUpdate04103( )
      {
         if ( AnyError == 0 )
         {
            /* Using cursor BC000413 */
            pr_default.execute(11, new Object[] {A494LocationImage, A40000LocationImage_GXI, n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
            pr_default.close(11);
            pr_default.SmartCacheProvider.SetUpdated("Trn_Location");
         }
         if ( AnyError == 0 )
         {
            /* Using cursor BC000414 */
            pr_default.execute(12, new Object[] {n574ReceptionImage, A574ReceptionImage, n40001ReceptionImage_GXI, A40001ReceptionImage_GXI, n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
            pr_default.close(12);
            pr_default.SmartCacheProvider.SetUpdated("Trn_Location");
         }
      }

      protected void delete( )
      {
         Gx_mode = "DLT";
         BeforeValidate04103( ) ;
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency04103( ) ;
         }
         if ( AnyError == 0 )
         {
            OnDeleteControls04103( ) ;
            AfterConfirm04103( ) ;
            if ( AnyError == 0 )
            {
               BeforeDelete04103( ) ;
               if ( AnyError == 0 )
               {
                  /* No cascading delete specified. */
                  /* Using cursor BC000415 */
                  pr_default.execute(13, new Object[] {n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
                  pr_default.close(13);
                  pr_default.SmartCacheProvider.SetUpdated("Trn_Location");
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
         sMode103 = Gx_mode;
         Gx_mode = "DLT";
         EndLevel04103( ) ;
         Gx_mode = sMode103;
      }

      protected void OnDeleteControls04103( )
      {
         standaloneModal( ) ;
         if ( AnyError == 0 )
         {
            /* Delete mode formulas */
            GXt_char2 = AV38LocationDescriptionVar;
            new prc_getdisplayvalue(context ).execute(  A36LocationDescription,  AV39trnName,  AV37AttributeDescription,  A29LocationId, out  GXt_char2) ;
            AV38LocationDescriptionVar = GXt_char2;
            /* Using cursor BC000416 */
            pr_default.execute(14, new Object[] {n584ActiveAppVersionId, A584ActiveAppVersionId});
            pr_default.close(14);
            /* Using cursor BC000417 */
            pr_default.execute(15, new Object[] {n598PublishedActiveAppVersionId, A598PublishedActiveAppVersionId});
            pr_default.close(15);
         }
         if ( AnyError == 0 )
         {
            /* Using cursor BC000418 */
            pr_default.execute(16, new Object[] {n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
            if ( (pr_default.getStatus(16) != 101) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_del", new   object[]  {"General Suppliers"}), "CannotDeleteReferencedRecord", 1, "");
               AnyError = 1;
            }
            pr_default.close(16);
            /* Using cursor BC000419 */
            pr_default.execute(17, new Object[] {n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
            if ( (pr_default.getStatus(17) != 101) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_del", new   object[]  {"Trn_ResidentPackage"}), "CannotDeleteReferencedRecord", 1, "");
               AnyError = 1;
            }
            pr_default.close(17);
            /* Using cursor BC000420 */
            pr_default.execute(18, new Object[] {n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
            if ( (pr_default.getStatus(18) != 101) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_del", new   object[]  {"Agenda/Calendar"}), "CannotDeleteReferencedRecord", 1, "");
               AnyError = 1;
            }
            pr_default.close(18);
            /* Using cursor BC000421 */
            pr_default.execute(19, new Object[] {n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
            if ( (pr_default.getStatus(19) != 101) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_del", new   object[]  {""}), "CannotDeleteReferencedRecord", 1, "");
               AnyError = 1;
            }
            pr_default.close(19);
            /* Using cursor BC000422 */
            pr_default.execute(20, new Object[] {n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
            if ( (pr_default.getStatus(20) != 101) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_del", new   object[]  {"Location Dynamic Forms"}), "CannotDeleteReferencedRecord", 1, "");
               AnyError = 1;
            }
            pr_default.close(20);
            /* Using cursor BC000423 */
            pr_default.execute(21, new Object[] {n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
            if ( (pr_default.getStatus(21) != 101) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_del", new   object[]  {"Services"}), "CannotDeleteReferencedRecord", 1, "");
               AnyError = 1;
            }
            pr_default.close(21);
            /* Using cursor BC000424 */
            pr_default.execute(22, new Object[] {n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
            if ( (pr_default.getStatus(22) != 101) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_del", new   object[]  {"Trn_Resident"}), "CannotDeleteReferencedRecord", 1, "");
               AnyError = 1;
            }
            pr_default.close(22);
            /* Using cursor BC000425 */
            pr_default.execute(23, new Object[] {n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
            if ( (pr_default.getStatus(23) != 101) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_del", new   object[]  {"Receptionists"}), "CannotDeleteReferencedRecord", 1, "");
               AnyError = 1;
            }
            pr_default.close(23);
         }
      }

      protected void EndLevel04103( )
      {
         if ( ! IsIns( ) )
         {
            pr_default.close(0);
         }
         if ( AnyError == 0 )
         {
            BeforeComplete04103( ) ;
         }
         if ( AnyError == 0 )
         {
            /* After transaction rules */
            new prc_addtodynamictransalation(context ).execute(  AV33SDT_TrnAttributes) ;
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

      public void ScanKeyStart04103( )
      {
         /* Scan By routine */
         /* Using cursor BC000426 */
         pr_default.execute(24, new Object[] {n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
         RcdFound103 = 0;
         if ( (pr_default.getStatus(24) != 101) )
         {
            RcdFound103 = 1;
            A29LocationId = BC000426_A29LocationId[0];
            n29LocationId = BC000426_n29LocationId[0];
            A35LocationPhone = BC000426_A35LocationPhone[0];
            A329LocationZipCode = BC000426_A329LocationZipCode[0];
            A31LocationName = BC000426_A31LocationName[0];
            A40000LocationImage_GXI = BC000426_A40000LocationImage_GXI[0];
            A327LocationCountry = BC000426_A327LocationCountry[0];
            A328LocationCity = BC000426_A328LocationCity[0];
            A330LocationAddressLine1 = BC000426_A330LocationAddressLine1[0];
            A331LocationAddressLine2 = BC000426_A331LocationAddressLine2[0];
            A34LocationEmail = BC000426_A34LocationEmail[0];
            A355LocationPhoneCode = BC000426_A355LocationPhoneCode[0];
            A356LocationPhoneNumber = BC000426_A356LocationPhoneNumber[0];
            A36LocationDescription = BC000426_A36LocationDescription[0];
            A568LocationBrandTheme = BC000426_A568LocationBrandTheme[0];
            n568LocationBrandTheme = BC000426_n568LocationBrandTheme[0];
            A569LocationCtaTheme = BC000426_A569LocationCtaTheme[0];
            n569LocationCtaTheme = BC000426_n569LocationCtaTheme[0];
            A570LocationHasMyCare = BC000426_A570LocationHasMyCare[0];
            A571LocationHasMyServices = BC000426_A571LocationHasMyServices[0];
            A572LocationHasMyLiving = BC000426_A572LocationHasMyLiving[0];
            A573LocationHasOwnBrand = BC000426_A573LocationHasOwnBrand[0];
            A504ToolBoxDefaultProfileImage = BC000426_A504ToolBoxDefaultProfileImage[0];
            n504ToolBoxDefaultProfileImage = BC000426_n504ToolBoxDefaultProfileImage[0];
            A503ToolBoxDefaultLogo = BC000426_A503ToolBoxDefaultLogo[0];
            n503ToolBoxDefaultLogo = BC000426_n503ToolBoxDefaultLogo[0];
            A40001ReceptionImage_GXI = BC000426_A40001ReceptionImage_GXI[0];
            n40001ReceptionImage_GXI = BC000426_n40001ReceptionImage_GXI[0];
            A575ReceptionDescription = BC000426_A575ReceptionDescription[0];
            n575ReceptionDescription = BC000426_n575ReceptionDescription[0];
            A11OrganisationId = BC000426_A11OrganisationId[0];
            n11OrganisationId = BC000426_n11OrganisationId[0];
            A273Trn_ThemeId = BC000426_A273Trn_ThemeId[0];
            n273Trn_ThemeId = BC000426_n273Trn_ThemeId[0];
            A576LocationThemeId = BC000426_A576LocationThemeId[0];
            n576LocationThemeId = BC000426_n576LocationThemeId[0];
            A584ActiveAppVersionId = BC000426_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = BC000426_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = BC000426_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = BC000426_n598PublishedActiveAppVersionId[0];
            A494LocationImage = BC000426_A494LocationImage[0];
            A574ReceptionImage = BC000426_A574ReceptionImage[0];
            n574ReceptionImage = BC000426_n574ReceptionImage[0];
         }
         /* Load Subordinate Levels */
      }

      protected void ScanKeyNext04103( )
      {
         /* Scan next routine */
         pr_default.readNext(24);
         RcdFound103 = 0;
         ScanKeyLoad04103( ) ;
      }

      protected void ScanKeyLoad04103( )
      {
         sMode103 = Gx_mode;
         Gx_mode = "DSP";
         if ( (pr_default.getStatus(24) != 101) )
         {
            RcdFound103 = 1;
            A29LocationId = BC000426_A29LocationId[0];
            n29LocationId = BC000426_n29LocationId[0];
            A35LocationPhone = BC000426_A35LocationPhone[0];
            A329LocationZipCode = BC000426_A329LocationZipCode[0];
            A31LocationName = BC000426_A31LocationName[0];
            A40000LocationImage_GXI = BC000426_A40000LocationImage_GXI[0];
            A327LocationCountry = BC000426_A327LocationCountry[0];
            A328LocationCity = BC000426_A328LocationCity[0];
            A330LocationAddressLine1 = BC000426_A330LocationAddressLine1[0];
            A331LocationAddressLine2 = BC000426_A331LocationAddressLine2[0];
            A34LocationEmail = BC000426_A34LocationEmail[0];
            A355LocationPhoneCode = BC000426_A355LocationPhoneCode[0];
            A356LocationPhoneNumber = BC000426_A356LocationPhoneNumber[0];
            A36LocationDescription = BC000426_A36LocationDescription[0];
            A568LocationBrandTheme = BC000426_A568LocationBrandTheme[0];
            n568LocationBrandTheme = BC000426_n568LocationBrandTheme[0];
            A569LocationCtaTheme = BC000426_A569LocationCtaTheme[0];
            n569LocationCtaTheme = BC000426_n569LocationCtaTheme[0];
            A570LocationHasMyCare = BC000426_A570LocationHasMyCare[0];
            A571LocationHasMyServices = BC000426_A571LocationHasMyServices[0];
            A572LocationHasMyLiving = BC000426_A572LocationHasMyLiving[0];
            A573LocationHasOwnBrand = BC000426_A573LocationHasOwnBrand[0];
            A504ToolBoxDefaultProfileImage = BC000426_A504ToolBoxDefaultProfileImage[0];
            n504ToolBoxDefaultProfileImage = BC000426_n504ToolBoxDefaultProfileImage[0];
            A503ToolBoxDefaultLogo = BC000426_A503ToolBoxDefaultLogo[0];
            n503ToolBoxDefaultLogo = BC000426_n503ToolBoxDefaultLogo[0];
            A40001ReceptionImage_GXI = BC000426_A40001ReceptionImage_GXI[0];
            n40001ReceptionImage_GXI = BC000426_n40001ReceptionImage_GXI[0];
            A575ReceptionDescription = BC000426_A575ReceptionDescription[0];
            n575ReceptionDescription = BC000426_n575ReceptionDescription[0];
            A11OrganisationId = BC000426_A11OrganisationId[0];
            n11OrganisationId = BC000426_n11OrganisationId[0];
            A273Trn_ThemeId = BC000426_A273Trn_ThemeId[0];
            n273Trn_ThemeId = BC000426_n273Trn_ThemeId[0];
            A576LocationThemeId = BC000426_A576LocationThemeId[0];
            n576LocationThemeId = BC000426_n576LocationThemeId[0];
            A584ActiveAppVersionId = BC000426_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = BC000426_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = BC000426_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = BC000426_n598PublishedActiveAppVersionId[0];
            A494LocationImage = BC000426_A494LocationImage[0];
            A574ReceptionImage = BC000426_A574ReceptionImage[0];
            n574ReceptionImage = BC000426_n574ReceptionImage[0];
         }
         Gx_mode = sMode103;
      }

      protected void ScanKeyEnd04103( )
      {
         pr_default.close(24);
      }

      protected void AfterConfirm04103( )
      {
         /* After Confirm Rules */
      }

      protected void BeforeInsert04103( )
      {
         /* Before Insert Rules */
      }

      protected void BeforeUpdate04103( )
      {
         /* Before Update Rules */
      }

      protected void BeforeDelete04103( )
      {
         /* Before Delete Rules */
         new trn_deletelocationpages(context ).execute(  A29LocationId) ;
      }

      protected void BeforeComplete04103( )
      {
         /* Before Complete Rules */
         GXt_SdtSDT_TrnAttributes4 = AV33SDT_TrnAttributes;
         new prc_addlocationattributestosdt(context ).execute(  A29LocationId, out  GXt_SdtSDT_TrnAttributes4) ;
         AV33SDT_TrnAttributes = GXt_SdtSDT_TrnAttributes4;
      }

      protected void BeforeValidate04103( )
      {
         /* Before Validate Rules */
         A36LocationDescription = AV38LocationDescriptionVar;
      }

      protected void DisableAttributes04103( )
      {
      }

      protected void send_integrity_lvl_hashes04103( )
      {
      }

      protected void AddRow04103( )
      {
         VarsToRow103( bcTrn_Location) ;
      }

      protected void ReadRow04103( )
      {
         RowToVars103( bcTrn_Location, 1) ;
      }

      protected void InitializeNonKey04103( )
      {
         A523AppVersionId = Guid.Empty;
         A35LocationPhone = "";
         A329LocationZipCode = "";
         AV33SDT_TrnAttributes = new SdtSDT_TrnAttributes(context);
         AV38LocationDescriptionVar = "";
         A31LocationName = "";
         A494LocationImage = "";
         A40000LocationImage_GXI = "";
         A327LocationCountry = "";
         A328LocationCity = "";
         A330LocationAddressLine1 = "";
         A331LocationAddressLine2 = "";
         A34LocationEmail = "";
         A355LocationPhoneCode = "";
         A356LocationPhoneNumber = "";
         A36LocationDescription = "";
         A568LocationBrandTheme = "";
         n568LocationBrandTheme = false;
         A569LocationCtaTheme = "";
         n569LocationCtaTheme = false;
         A504ToolBoxDefaultProfileImage = "";
         n504ToolBoxDefaultProfileImage = false;
         A503ToolBoxDefaultLogo = "";
         n503ToolBoxDefaultLogo = false;
         A40001ReceptionImage_GXI = "";
         n40001ReceptionImage_GXI = false;
         A584ActiveAppVersionId = Guid.Empty;
         n584ActiveAppVersionId = false;
         A598PublishedActiveAppVersionId = Guid.Empty;
         n598PublishedActiveAppVersionId = false;
         A576LocationThemeId = Guid.Empty;
         n576LocationThemeId = false;
         A570LocationHasMyCare = false;
         A571LocationHasMyServices = false;
         A572LocationHasMyLiving = false;
         A573LocationHasOwnBrand = false;
         A574ReceptionImage = AV30ReceptionImageVar;
         n574ReceptionImage = false;
         A575ReceptionDescription = AV31ReceptionDescriptionVar;
         n575ReceptionDescription = false;
         A273Trn_ThemeId = new prc_getdefaulttheme(context).executeUdp( );
         n273Trn_ThemeId = false;
         Z35LocationPhone = "";
         Z329LocationZipCode = "";
         Z31LocationName = "";
         Z327LocationCountry = "";
         Z328LocationCity = "";
         Z330LocationAddressLine1 = "";
         Z331LocationAddressLine2 = "";
         Z34LocationEmail = "";
         Z355LocationPhoneCode = "";
         Z356LocationPhoneNumber = "";
         Z570LocationHasMyCare = false;
         Z571LocationHasMyServices = false;
         Z572LocationHasMyLiving = false;
         Z573LocationHasOwnBrand = false;
         Z504ToolBoxDefaultProfileImage = "";
         Z503ToolBoxDefaultLogo = "";
         Z575ReceptionDescription = "";
         Z273Trn_ThemeId = Guid.Empty;
         Z576LocationThemeId = Guid.Empty;
         Z584ActiveAppVersionId = Guid.Empty;
         Z598PublishedActiveAppVersionId = Guid.Empty;
      }

      protected void InitAll04103( )
      {
         A29LocationId = Guid.NewGuid( );
         n29LocationId = false;
         A11OrganisationId = Guid.Empty;
         n11OrganisationId = false;
         InitializeNonKey04103( ) ;
      }

      protected void StandaloneModalInsert( )
      {
         A573LocationHasOwnBrand = i573LocationHasOwnBrand;
         A572LocationHasMyLiving = i572LocationHasMyLiving;
         A571LocationHasMyServices = i571LocationHasMyServices;
         A570LocationHasMyCare = i570LocationHasMyCare;
         A575ReceptionDescription = i575ReceptionDescription;
         n575ReceptionDescription = false;
         A574ReceptionImage = i574ReceptionImage;
         n574ReceptionImage = false;
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

      public void VarsToRow103( SdtTrn_Location obj103 )
      {
         obj103.gxTpr_Mode = Gx_mode;
         obj103.gxTpr_Locationphone = A35LocationPhone;
         obj103.gxTpr_Locationzipcode = A329LocationZipCode;
         obj103.gxTpr_Locationname = A31LocationName;
         obj103.gxTpr_Locationimage = A494LocationImage;
         obj103.gxTpr_Locationimage_gxi = A40000LocationImage_GXI;
         obj103.gxTpr_Locationcountry = A327LocationCountry;
         obj103.gxTpr_Locationcity = A328LocationCity;
         obj103.gxTpr_Locationaddressline1 = A330LocationAddressLine1;
         obj103.gxTpr_Locationaddressline2 = A331LocationAddressLine2;
         obj103.gxTpr_Locationemail = A34LocationEmail;
         obj103.gxTpr_Locationphonecode = A355LocationPhoneCode;
         obj103.gxTpr_Locationphonenumber = A356LocationPhoneNumber;
         obj103.gxTpr_Locationdescription = A36LocationDescription;
         obj103.gxTpr_Locationbrandtheme = A568LocationBrandTheme;
         obj103.gxTpr_Locationctatheme = A569LocationCtaTheme;
         obj103.gxTpr_Toolboxdefaultprofileimage = A504ToolBoxDefaultProfileImage;
         obj103.gxTpr_Toolboxdefaultlogo = A503ToolBoxDefaultLogo;
         obj103.gxTpr_Receptionimage_gxi = A40001ReceptionImage_GXI;
         obj103.gxTpr_Activeappversionid = A584ActiveAppVersionId;
         obj103.gxTpr_Publishedactiveappversionid = A598PublishedActiveAppVersionId;
         obj103.gxTpr_Locationthemeid = A576LocationThemeId;
         obj103.gxTpr_Locationhasmycare = A570LocationHasMyCare;
         obj103.gxTpr_Locationhasmyservices = A571LocationHasMyServices;
         obj103.gxTpr_Locationhasmyliving = A572LocationHasMyLiving;
         obj103.gxTpr_Locationhasownbrand = A573LocationHasOwnBrand;
         obj103.gxTpr_Receptionimage = A574ReceptionImage;
         obj103.gxTpr_Receptiondescription = A575ReceptionDescription;
         obj103.gxTpr_Trn_themeid = A273Trn_ThemeId;
         obj103.gxTpr_Locationid = A29LocationId;
         obj103.gxTpr_Organisationid = A11OrganisationId;
         obj103.gxTpr_Locationid_Z = Z29LocationId;
         obj103.gxTpr_Organisationid_Z = Z11OrganisationId;
         obj103.gxTpr_Locationname_Z = Z31LocationName;
         obj103.gxTpr_Locationcountry_Z = Z327LocationCountry;
         obj103.gxTpr_Locationcity_Z = Z328LocationCity;
         obj103.gxTpr_Locationzipcode_Z = Z329LocationZipCode;
         obj103.gxTpr_Locationaddressline1_Z = Z330LocationAddressLine1;
         obj103.gxTpr_Locationaddressline2_Z = Z331LocationAddressLine2;
         obj103.gxTpr_Locationemail_Z = Z34LocationEmail;
         obj103.gxTpr_Locationphonecode_Z = Z355LocationPhoneCode;
         obj103.gxTpr_Locationphonenumber_Z = Z356LocationPhoneNumber;
         obj103.gxTpr_Locationphone_Z = Z35LocationPhone;
         obj103.gxTpr_Locationhasmycare_Z = Z570LocationHasMyCare;
         obj103.gxTpr_Locationhasmyservices_Z = Z571LocationHasMyServices;
         obj103.gxTpr_Locationhasmyliving_Z = Z572LocationHasMyLiving;
         obj103.gxTpr_Locationhasownbrand_Z = Z573LocationHasOwnBrand;
         obj103.gxTpr_Toolboxdefaultprofileimage_Z = Z504ToolBoxDefaultProfileImage;
         obj103.gxTpr_Toolboxdefaultlogo_Z = Z503ToolBoxDefaultLogo;
         obj103.gxTpr_Receptiondescription_Z = Z575ReceptionDescription;
         obj103.gxTpr_Activeappversionid_Z = Z584ActiveAppVersionId;
         obj103.gxTpr_Publishedactiveappversionid_Z = Z598PublishedActiveAppVersionId;
         obj103.gxTpr_Trn_themeid_Z = Z273Trn_ThemeId;
         obj103.gxTpr_Locationthemeid_Z = Z576LocationThemeId;
         obj103.gxTpr_Locationimage_gxi_Z = Z40000LocationImage_GXI;
         obj103.gxTpr_Receptionimage_gxi_Z = Z40001ReceptionImage_GXI;
         obj103.gxTpr_Locationid_N = (short)(Convert.ToInt16(n29LocationId));
         obj103.gxTpr_Organisationid_N = (short)(Convert.ToInt16(n11OrganisationId));
         obj103.gxTpr_Locationbrandtheme_N = (short)(Convert.ToInt16(n568LocationBrandTheme));
         obj103.gxTpr_Locationctatheme_N = (short)(Convert.ToInt16(n569LocationCtaTheme));
         obj103.gxTpr_Toolboxdefaultprofileimage_N = (short)(Convert.ToInt16(n504ToolBoxDefaultProfileImage));
         obj103.gxTpr_Toolboxdefaultlogo_N = (short)(Convert.ToInt16(n503ToolBoxDefaultLogo));
         obj103.gxTpr_Receptionimage_N = (short)(Convert.ToInt16(n574ReceptionImage));
         obj103.gxTpr_Receptiondescription_N = (short)(Convert.ToInt16(n575ReceptionDescription));
         obj103.gxTpr_Activeappversionid_N = (short)(Convert.ToInt16(n584ActiveAppVersionId));
         obj103.gxTpr_Publishedactiveappversionid_N = (short)(Convert.ToInt16(n598PublishedActiveAppVersionId));
         obj103.gxTpr_Trn_themeid_N = (short)(Convert.ToInt16(n273Trn_ThemeId));
         obj103.gxTpr_Locationthemeid_N = (short)(Convert.ToInt16(n576LocationThemeId));
         obj103.gxTpr_Receptionimage_gxi_N = (short)(Convert.ToInt16(n40001ReceptionImage_GXI));
         obj103.gxTpr_Mode = Gx_mode;
         return  ;
      }

      public void KeyVarsToRow103( SdtTrn_Location obj103 )
      {
         obj103.gxTpr_Locationid = A29LocationId;
         obj103.gxTpr_Organisationid = A11OrganisationId;
         return  ;
      }

      public void RowToVars103( SdtTrn_Location obj103 ,
                                int forceLoad )
      {
         Gx_mode = obj103.gxTpr_Mode;
         A35LocationPhone = obj103.gxTpr_Locationphone;
         A329LocationZipCode = obj103.gxTpr_Locationzipcode;
         A31LocationName = obj103.gxTpr_Locationname;
         A494LocationImage = obj103.gxTpr_Locationimage;
         A40000LocationImage_GXI = obj103.gxTpr_Locationimage_gxi;
         A327LocationCountry = obj103.gxTpr_Locationcountry;
         A328LocationCity = obj103.gxTpr_Locationcity;
         A330LocationAddressLine1 = obj103.gxTpr_Locationaddressline1;
         A331LocationAddressLine2 = obj103.gxTpr_Locationaddressline2;
         A34LocationEmail = obj103.gxTpr_Locationemail;
         A355LocationPhoneCode = obj103.gxTpr_Locationphonecode;
         A356LocationPhoneNumber = obj103.gxTpr_Locationphonenumber;
         A36LocationDescription = obj103.gxTpr_Locationdescription;
         A568LocationBrandTheme = obj103.gxTpr_Locationbrandtheme;
         n568LocationBrandTheme = false;
         A569LocationCtaTheme = obj103.gxTpr_Locationctatheme;
         n569LocationCtaTheme = false;
         A504ToolBoxDefaultProfileImage = obj103.gxTpr_Toolboxdefaultprofileimage;
         n504ToolBoxDefaultProfileImage = false;
         A503ToolBoxDefaultLogo = obj103.gxTpr_Toolboxdefaultlogo;
         n503ToolBoxDefaultLogo = false;
         A40001ReceptionImage_GXI = obj103.gxTpr_Receptionimage_gxi;
         n40001ReceptionImage_GXI = false;
         A584ActiveAppVersionId = obj103.gxTpr_Activeappversionid;
         n584ActiveAppVersionId = false;
         A598PublishedActiveAppVersionId = obj103.gxTpr_Publishedactiveappversionid;
         n598PublishedActiveAppVersionId = false;
         A576LocationThemeId = obj103.gxTpr_Locationthemeid;
         n576LocationThemeId = false;
         A570LocationHasMyCare = obj103.gxTpr_Locationhasmycare;
         A571LocationHasMyServices = obj103.gxTpr_Locationhasmyservices;
         A572LocationHasMyLiving = obj103.gxTpr_Locationhasmyliving;
         A573LocationHasOwnBrand = obj103.gxTpr_Locationhasownbrand;
         A574ReceptionImage = obj103.gxTpr_Receptionimage;
         n574ReceptionImage = false;
         A575ReceptionDescription = obj103.gxTpr_Receptiondescription;
         n575ReceptionDescription = false;
         A273Trn_ThemeId = obj103.gxTpr_Trn_themeid;
         n273Trn_ThemeId = false;
         A29LocationId = obj103.gxTpr_Locationid;
         n29LocationId = false;
         A11OrganisationId = obj103.gxTpr_Organisationid;
         n11OrganisationId = false;
         Z29LocationId = obj103.gxTpr_Locationid_Z;
         Z11OrganisationId = obj103.gxTpr_Organisationid_Z;
         Z31LocationName = obj103.gxTpr_Locationname_Z;
         Z327LocationCountry = obj103.gxTpr_Locationcountry_Z;
         Z328LocationCity = obj103.gxTpr_Locationcity_Z;
         Z329LocationZipCode = obj103.gxTpr_Locationzipcode_Z;
         Z330LocationAddressLine1 = obj103.gxTpr_Locationaddressline1_Z;
         Z331LocationAddressLine2 = obj103.gxTpr_Locationaddressline2_Z;
         Z34LocationEmail = obj103.gxTpr_Locationemail_Z;
         Z355LocationPhoneCode = obj103.gxTpr_Locationphonecode_Z;
         Z356LocationPhoneNumber = obj103.gxTpr_Locationphonenumber_Z;
         Z35LocationPhone = obj103.gxTpr_Locationphone_Z;
         Z570LocationHasMyCare = obj103.gxTpr_Locationhasmycare_Z;
         Z571LocationHasMyServices = obj103.gxTpr_Locationhasmyservices_Z;
         Z572LocationHasMyLiving = obj103.gxTpr_Locationhasmyliving_Z;
         Z573LocationHasOwnBrand = obj103.gxTpr_Locationhasownbrand_Z;
         Z504ToolBoxDefaultProfileImage = obj103.gxTpr_Toolboxdefaultprofileimage_Z;
         Z503ToolBoxDefaultLogo = obj103.gxTpr_Toolboxdefaultlogo_Z;
         Z575ReceptionDescription = obj103.gxTpr_Receptiondescription_Z;
         Z584ActiveAppVersionId = obj103.gxTpr_Activeappversionid_Z;
         Z598PublishedActiveAppVersionId = obj103.gxTpr_Publishedactiveappversionid_Z;
         Z273Trn_ThemeId = obj103.gxTpr_Trn_themeid_Z;
         Z576LocationThemeId = obj103.gxTpr_Locationthemeid_Z;
         Z40000LocationImage_GXI = obj103.gxTpr_Locationimage_gxi_Z;
         Z40001ReceptionImage_GXI = obj103.gxTpr_Receptionimage_gxi_Z;
         n29LocationId = (bool)(Convert.ToBoolean(obj103.gxTpr_Locationid_N));
         n11OrganisationId = (bool)(Convert.ToBoolean(obj103.gxTpr_Organisationid_N));
         n568LocationBrandTheme = (bool)(Convert.ToBoolean(obj103.gxTpr_Locationbrandtheme_N));
         n569LocationCtaTheme = (bool)(Convert.ToBoolean(obj103.gxTpr_Locationctatheme_N));
         n504ToolBoxDefaultProfileImage = (bool)(Convert.ToBoolean(obj103.gxTpr_Toolboxdefaultprofileimage_N));
         n503ToolBoxDefaultLogo = (bool)(Convert.ToBoolean(obj103.gxTpr_Toolboxdefaultlogo_N));
         n574ReceptionImage = (bool)(Convert.ToBoolean(obj103.gxTpr_Receptionimage_N));
         n575ReceptionDescription = (bool)(Convert.ToBoolean(obj103.gxTpr_Receptiondescription_N));
         n584ActiveAppVersionId = (bool)(Convert.ToBoolean(obj103.gxTpr_Activeappversionid_N));
         n598PublishedActiveAppVersionId = (bool)(Convert.ToBoolean(obj103.gxTpr_Publishedactiveappversionid_N));
         n273Trn_ThemeId = (bool)(Convert.ToBoolean(obj103.gxTpr_Trn_themeid_N));
         n576LocationThemeId = (bool)(Convert.ToBoolean(obj103.gxTpr_Locationthemeid_N));
         n40001ReceptionImage_GXI = (bool)(Convert.ToBoolean(obj103.gxTpr_Receptionimage_gxi_N));
         Gx_mode = obj103.gxTpr_Mode;
         return  ;
      }

      public void LoadKey( Object[] obj )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         A29LocationId = (Guid)getParm(obj,0);
         n29LocationId = false;
         A11OrganisationId = (Guid)getParm(obj,1);
         n11OrganisationId = false;
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         InitializeNonKey04103( ) ;
         ScanKeyStart04103( ) ;
         if ( RcdFound103 == 0 )
         {
            Gx_mode = "INS";
            /* Using cursor BC000427 */
            pr_default.execute(25, new Object[] {n11OrganisationId, A11OrganisationId});
            if ( (pr_default.getStatus(25) == 101) )
            {
               GX_msglist.addItem("No matching 'Trn_Organisation'.", "ForeignKeyNotFound", 1, "ORGANISATIONID");
               AnyError = 1;
            }
            pr_default.close(25);
         }
         else
         {
            Gx_mode = "UPD";
            Z29LocationId = A29LocationId;
            Z11OrganisationId = A11OrganisationId;
         }
         ZM04103( -31) ;
         OnLoadActions04103( ) ;
         AddRow04103( ) ;
         ScanKeyEnd04103( ) ;
         if ( RcdFound103 == 0 )
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
         RowToVars103( bcTrn_Location, 0) ;
         ScanKeyStart04103( ) ;
         if ( RcdFound103 == 0 )
         {
            Gx_mode = "INS";
            /* Using cursor BC000427 */
            pr_default.execute(25, new Object[] {n11OrganisationId, A11OrganisationId});
            if ( (pr_default.getStatus(25) == 101) )
            {
               GX_msglist.addItem("No matching 'Trn_Organisation'.", "ForeignKeyNotFound", 1, "ORGANISATIONID");
               AnyError = 1;
            }
            pr_default.close(25);
         }
         else
         {
            Gx_mode = "UPD";
            Z29LocationId = A29LocationId;
            Z11OrganisationId = A11OrganisationId;
         }
         ZM04103( -31) ;
         OnLoadActions04103( ) ;
         AddRow04103( ) ;
         ScanKeyEnd04103( ) ;
         if ( RcdFound103 == 0 )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_keynfound", ""), "PrimaryKeyNotFound", 1, "");
            AnyError = 1;
         }
         context.GX_msglist = BackMsgLst;
      }

      protected void SaveImpl( )
      {
         GetKey04103( ) ;
         if ( IsIns( ) )
         {
            /* Insert record */
            Insert04103( ) ;
         }
         else
         {
            if ( RcdFound103 == 1 )
            {
               if ( ( A29LocationId != Z29LocationId ) || ( A11OrganisationId != Z11OrganisationId ) )
               {
                  A29LocationId = Z29LocationId;
                  n29LocationId = false;
                  A11OrganisationId = Z11OrganisationId;
                  n11OrganisationId = false;
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
                  Update04103( ) ;
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
                  if ( ( A29LocationId != Z29LocationId ) || ( A11OrganisationId != Z11OrganisationId ) )
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
                        Insert04103( ) ;
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
                        Insert04103( ) ;
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
         RowToVars103( bcTrn_Location, 1) ;
         SaveImpl( ) ;
         VarsToRow103( bcTrn_Location) ;
         context.GX_msglist = BackMsgLst;
         return  ;
      }

      public bool Insert( )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         RowToVars103( bcTrn_Location, 1) ;
         Gx_mode = "INS";
         /* Insert record */
         Insert04103( ) ;
         AfterTrn( ) ;
         VarsToRow103( bcTrn_Location) ;
         context.GX_msglist = BackMsgLst;
         return (AnyError==0) ;
      }

      protected void UpdateImpl( )
      {
         if ( IsUpd( ) )
         {
            SaveImpl( ) ;
            VarsToRow103( bcTrn_Location) ;
         }
         else
         {
            SdtTrn_Location auxBC = new SdtTrn_Location(context);
            IGxSilentTrn auxTrn = auxBC.getTransaction();
            auxBC.Load(A29LocationId, A11OrganisationId);
            if ( auxTrn.Errors() == 0 )
            {
               auxBC.UpdateDirties(bcTrn_Location);
               auxBC.Save();
               bcTrn_Location.Copy((GxSilentTrnSdt)(auxBC));
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
         RowToVars103( bcTrn_Location, 1) ;
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
         RowToVars103( bcTrn_Location, 1) ;
         Gx_mode = "INS";
         /* Insert record */
         Insert04103( ) ;
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
               VarsToRow103( bcTrn_Location) ;
            }
         }
         else
         {
            AfterTrn( ) ;
            VarsToRow103( bcTrn_Location) ;
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
         RowToVars103( bcTrn_Location, 0) ;
         GetKey04103( ) ;
         if ( RcdFound103 == 1 )
         {
            if ( IsIns( ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_noupdate", ""), "DuplicatePrimaryKey", 1, "");
               AnyError = 1;
            }
            else if ( ( A29LocationId != Z29LocationId ) || ( A11OrganisationId != Z11OrganisationId ) )
            {
               A29LocationId = Z29LocationId;
               n29LocationId = false;
               A11OrganisationId = Z11OrganisationId;
               n11OrganisationId = false;
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
            if ( ( A29LocationId != Z29LocationId ) || ( A11OrganisationId != Z11OrganisationId ) )
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
         context.RollbackDataStores("trn_location_bc",pr_default);
         VarsToRow103( bcTrn_Location) ;
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
         Gx_mode = bcTrn_Location.gxTpr_Mode;
         return Gx_mode ;
      }

      public void SetMode( string lMode )
      {
         Gx_mode = lMode;
         bcTrn_Location.gxTpr_Mode = Gx_mode;
         return  ;
      }

      public void SetSDT( GxSilentTrnSdt sdt ,
                          short sdtToBc )
      {
         if ( sdt != bcTrn_Location )
         {
            bcTrn_Location = (SdtTrn_Location)(sdt);
            if ( StringUtil.StrCmp(bcTrn_Location.gxTpr_Mode, "") == 0 )
            {
               bcTrn_Location.gxTpr_Mode = "INS";
            }
            if ( sdtToBc == 1 )
            {
               VarsToRow103( bcTrn_Location) ;
            }
            else
            {
               RowToVars103( bcTrn_Location, 1) ;
            }
         }
         else
         {
            if ( StringUtil.StrCmp(bcTrn_Location.gxTpr_Mode, "") == 0 )
            {
               bcTrn_Location.gxTpr_Mode = "INS";
            }
         }
         return  ;
      }

      public void ReloadFromSDT( )
      {
         RowToVars103( bcTrn_Location, 1) ;
         return  ;
      }

      public void ForceCommitOnExit( )
      {
         return  ;
      }

      public SdtTrn_Location Trn_Location_BC
      {
         get {
            return bcTrn_Location ;
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
            return "trn_location_Execute" ;
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
         pr_default.close(25);
         pr_default.close(15);
         pr_default.close(14);
      }

      public override void initialize( )
      {
         Gx_mode = "";
         endTrnMsgTxt = "";
         endTrnMsgCod = "";
         Z29LocationId = Guid.Empty;
         A29LocationId = Guid.Empty;
         Z11OrganisationId = Guid.Empty;
         A11OrganisationId = Guid.Empty;
         AV39trnName = "";
         AV37AttributeDescription = "";
         AV31ReceptionDescriptionVar = "";
         AV30ReceptionImageVar = "";
         imgReceptionimagevar_gximage = "";
         AV44Receptionimagevar_GXI = "";
         AV9WWPContext = new GeneXus.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV12TrnContext = new WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext(context);
         AV13WebSession = context.GetSession();
         AV45Pgmname = "";
         AV26TrnContextAtt = new WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext_Attribute(context);
         AV34Insert_ActiveAppVersionId = Guid.Empty;
         AV40Insert_PublishedActiveAppVersionId = Guid.Empty;
         AV25Insert_Trn_ThemeId = Guid.Empty;
         AV32Insert_LocationThemeId = Guid.Empty;
         AV41FilesToUpdate = new GXBaseCollection<SdtSDT_FileUploadData>( context, "SDT_FileUploadData", "Comforta_version20");
         GXt_objcol_SdtSDT_FileUploadData1 = new GXBaseCollection<SdtSDT_FileUploadData>( context, "SDT_FileUploadData", "Comforta_version20");
         AV7LocationId = Guid.Empty;
         AV42UploadedFiles = new GXBaseCollection<SdtSDT_FileUploadData>( context, "SDT_FileUploadData", "Comforta_version20");
         Z35LocationPhone = "";
         A35LocationPhone = "";
         Z329LocationZipCode = "";
         A329LocationZipCode = "";
         Z31LocationName = "";
         A31LocationName = "";
         Z327LocationCountry = "";
         A327LocationCountry = "";
         Z328LocationCity = "";
         A328LocationCity = "";
         Z330LocationAddressLine1 = "";
         A330LocationAddressLine1 = "";
         Z331LocationAddressLine2 = "";
         A331LocationAddressLine2 = "";
         Z34LocationEmail = "";
         A34LocationEmail = "";
         Z355LocationPhoneCode = "";
         A355LocationPhoneCode = "";
         Z356LocationPhoneNumber = "";
         A356LocationPhoneNumber = "";
         Z504ToolBoxDefaultProfileImage = "";
         A504ToolBoxDefaultProfileImage = "";
         Z503ToolBoxDefaultLogo = "";
         A503ToolBoxDefaultLogo = "";
         Z575ReceptionDescription = "";
         A575ReceptionDescription = "";
         Z273Trn_ThemeId = Guid.Empty;
         A273Trn_ThemeId = Guid.Empty;
         Z576LocationThemeId = Guid.Empty;
         A576LocationThemeId = Guid.Empty;
         Z584ActiveAppVersionId = Guid.Empty;
         A584ActiveAppVersionId = Guid.Empty;
         Z598PublishedActiveAppVersionId = Guid.Empty;
         A598PublishedActiveAppVersionId = Guid.Empty;
         A523AppVersionId = Guid.Empty;
         Z494LocationImage = "";
         A494LocationImage = "";
         Z40000LocationImage_GXI = "";
         A40000LocationImage_GXI = "";
         Z36LocationDescription = "";
         A36LocationDescription = "";
         Z568LocationBrandTheme = "";
         A568LocationBrandTheme = "";
         Z569LocationCtaTheme = "";
         A569LocationCtaTheme = "";
         Z574ReceptionImage = "";
         A574ReceptionImage = "";
         Z40001ReceptionImage_GXI = "";
         A40001ReceptionImage_GXI = "";
         BC00049_A29LocationId = new Guid[] {Guid.Empty} ;
         BC00049_n29LocationId = new bool[] {false} ;
         BC00049_A35LocationPhone = new string[] {""} ;
         BC00049_A329LocationZipCode = new string[] {""} ;
         BC00049_A31LocationName = new string[] {""} ;
         BC00049_A40000LocationImage_GXI = new string[] {""} ;
         BC00049_A327LocationCountry = new string[] {""} ;
         BC00049_A328LocationCity = new string[] {""} ;
         BC00049_A330LocationAddressLine1 = new string[] {""} ;
         BC00049_A331LocationAddressLine2 = new string[] {""} ;
         BC00049_A34LocationEmail = new string[] {""} ;
         BC00049_A355LocationPhoneCode = new string[] {""} ;
         BC00049_A356LocationPhoneNumber = new string[] {""} ;
         BC00049_A36LocationDescription = new string[] {""} ;
         BC00049_A568LocationBrandTheme = new string[] {""} ;
         BC00049_n568LocationBrandTheme = new bool[] {false} ;
         BC00049_A569LocationCtaTheme = new string[] {""} ;
         BC00049_n569LocationCtaTheme = new bool[] {false} ;
         BC00049_A570LocationHasMyCare = new bool[] {false} ;
         BC00049_A571LocationHasMyServices = new bool[] {false} ;
         BC00049_A572LocationHasMyLiving = new bool[] {false} ;
         BC00049_A573LocationHasOwnBrand = new bool[] {false} ;
         BC00049_A504ToolBoxDefaultProfileImage = new string[] {""} ;
         BC00049_n504ToolBoxDefaultProfileImage = new bool[] {false} ;
         BC00049_A503ToolBoxDefaultLogo = new string[] {""} ;
         BC00049_n503ToolBoxDefaultLogo = new bool[] {false} ;
         BC00049_A40001ReceptionImage_GXI = new string[] {""} ;
         BC00049_n40001ReceptionImage_GXI = new bool[] {false} ;
         BC00049_A575ReceptionDescription = new string[] {""} ;
         BC00049_n575ReceptionDescription = new bool[] {false} ;
         BC00049_A11OrganisationId = new Guid[] {Guid.Empty} ;
         BC00049_n11OrganisationId = new bool[] {false} ;
         BC00049_A273Trn_ThemeId = new Guid[] {Guid.Empty} ;
         BC00049_n273Trn_ThemeId = new bool[] {false} ;
         BC00049_A576LocationThemeId = new Guid[] {Guid.Empty} ;
         BC00049_n576LocationThemeId = new bool[] {false} ;
         BC00049_A584ActiveAppVersionId = new Guid[] {Guid.Empty} ;
         BC00049_n584ActiveAppVersionId = new bool[] {false} ;
         BC00049_A598PublishedActiveAppVersionId = new Guid[] {Guid.Empty} ;
         BC00049_n598PublishedActiveAppVersionId = new bool[] {false} ;
         BC00049_A494LocationImage = new string[] {""} ;
         BC00049_A574ReceptionImage = new string[] {""} ;
         BC00049_n574ReceptionImage = new bool[] {false} ;
         AV38LocationDescriptionVar = "";
         BC00047_A523AppVersionId = new Guid[] {Guid.Empty} ;
         BC00048_A523AppVersionId = new Guid[] {Guid.Empty} ;
         BC00044_A11OrganisationId = new Guid[] {Guid.Empty} ;
         BC00044_n11OrganisationId = new bool[] {false} ;
         BC00046_A576LocationThemeId = new Guid[] {Guid.Empty} ;
         BC00046_n576LocationThemeId = new bool[] {false} ;
         GXt_guid3 = Guid.Empty;
         BC00045_A273Trn_ThemeId = new Guid[] {Guid.Empty} ;
         BC00045_n273Trn_ThemeId = new bool[] {false} ;
         BC000410_A29LocationId = new Guid[] {Guid.Empty} ;
         BC000410_n29LocationId = new bool[] {false} ;
         BC000410_A11OrganisationId = new Guid[] {Guid.Empty} ;
         BC000410_n11OrganisationId = new bool[] {false} ;
         BC00043_A29LocationId = new Guid[] {Guid.Empty} ;
         BC00043_n29LocationId = new bool[] {false} ;
         BC00043_A35LocationPhone = new string[] {""} ;
         BC00043_A329LocationZipCode = new string[] {""} ;
         BC00043_A31LocationName = new string[] {""} ;
         BC00043_A40000LocationImage_GXI = new string[] {""} ;
         BC00043_A327LocationCountry = new string[] {""} ;
         BC00043_A328LocationCity = new string[] {""} ;
         BC00043_A330LocationAddressLine1 = new string[] {""} ;
         BC00043_A331LocationAddressLine2 = new string[] {""} ;
         BC00043_A34LocationEmail = new string[] {""} ;
         BC00043_A355LocationPhoneCode = new string[] {""} ;
         BC00043_A356LocationPhoneNumber = new string[] {""} ;
         BC00043_A36LocationDescription = new string[] {""} ;
         BC00043_A568LocationBrandTheme = new string[] {""} ;
         BC00043_n568LocationBrandTheme = new bool[] {false} ;
         BC00043_A569LocationCtaTheme = new string[] {""} ;
         BC00043_n569LocationCtaTheme = new bool[] {false} ;
         BC00043_A570LocationHasMyCare = new bool[] {false} ;
         BC00043_A571LocationHasMyServices = new bool[] {false} ;
         BC00043_A572LocationHasMyLiving = new bool[] {false} ;
         BC00043_A573LocationHasOwnBrand = new bool[] {false} ;
         BC00043_A504ToolBoxDefaultProfileImage = new string[] {""} ;
         BC00043_n504ToolBoxDefaultProfileImage = new bool[] {false} ;
         BC00043_A503ToolBoxDefaultLogo = new string[] {""} ;
         BC00043_n503ToolBoxDefaultLogo = new bool[] {false} ;
         BC00043_A40001ReceptionImage_GXI = new string[] {""} ;
         BC00043_n40001ReceptionImage_GXI = new bool[] {false} ;
         BC00043_A575ReceptionDescription = new string[] {""} ;
         BC00043_n575ReceptionDescription = new bool[] {false} ;
         BC00043_A11OrganisationId = new Guid[] {Guid.Empty} ;
         BC00043_n11OrganisationId = new bool[] {false} ;
         BC00043_A273Trn_ThemeId = new Guid[] {Guid.Empty} ;
         BC00043_n273Trn_ThemeId = new bool[] {false} ;
         BC00043_A576LocationThemeId = new Guid[] {Guid.Empty} ;
         BC00043_n576LocationThemeId = new bool[] {false} ;
         BC00043_A584ActiveAppVersionId = new Guid[] {Guid.Empty} ;
         BC00043_n584ActiveAppVersionId = new bool[] {false} ;
         BC00043_A598PublishedActiveAppVersionId = new Guid[] {Guid.Empty} ;
         BC00043_n598PublishedActiveAppVersionId = new bool[] {false} ;
         BC00043_A494LocationImage = new string[] {""} ;
         BC00043_A574ReceptionImage = new string[] {""} ;
         BC00043_n574ReceptionImage = new bool[] {false} ;
         sMode103 = "";
         BC00042_A29LocationId = new Guid[] {Guid.Empty} ;
         BC00042_n29LocationId = new bool[] {false} ;
         BC00042_A35LocationPhone = new string[] {""} ;
         BC00042_A329LocationZipCode = new string[] {""} ;
         BC00042_A31LocationName = new string[] {""} ;
         BC00042_A40000LocationImage_GXI = new string[] {""} ;
         BC00042_A327LocationCountry = new string[] {""} ;
         BC00042_A328LocationCity = new string[] {""} ;
         BC00042_A330LocationAddressLine1 = new string[] {""} ;
         BC00042_A331LocationAddressLine2 = new string[] {""} ;
         BC00042_A34LocationEmail = new string[] {""} ;
         BC00042_A355LocationPhoneCode = new string[] {""} ;
         BC00042_A356LocationPhoneNumber = new string[] {""} ;
         BC00042_A36LocationDescription = new string[] {""} ;
         BC00042_A568LocationBrandTheme = new string[] {""} ;
         BC00042_n568LocationBrandTheme = new bool[] {false} ;
         BC00042_A569LocationCtaTheme = new string[] {""} ;
         BC00042_n569LocationCtaTheme = new bool[] {false} ;
         BC00042_A570LocationHasMyCare = new bool[] {false} ;
         BC00042_A571LocationHasMyServices = new bool[] {false} ;
         BC00042_A572LocationHasMyLiving = new bool[] {false} ;
         BC00042_A573LocationHasOwnBrand = new bool[] {false} ;
         BC00042_A504ToolBoxDefaultProfileImage = new string[] {""} ;
         BC00042_n504ToolBoxDefaultProfileImage = new bool[] {false} ;
         BC00042_A503ToolBoxDefaultLogo = new string[] {""} ;
         BC00042_n503ToolBoxDefaultLogo = new bool[] {false} ;
         BC00042_A40001ReceptionImage_GXI = new string[] {""} ;
         BC00042_n40001ReceptionImage_GXI = new bool[] {false} ;
         BC00042_A575ReceptionDescription = new string[] {""} ;
         BC00042_n575ReceptionDescription = new bool[] {false} ;
         BC00042_A11OrganisationId = new Guid[] {Guid.Empty} ;
         BC00042_n11OrganisationId = new bool[] {false} ;
         BC00042_A273Trn_ThemeId = new Guid[] {Guid.Empty} ;
         BC00042_n273Trn_ThemeId = new bool[] {false} ;
         BC00042_A576LocationThemeId = new Guid[] {Guid.Empty} ;
         BC00042_n576LocationThemeId = new bool[] {false} ;
         BC00042_A584ActiveAppVersionId = new Guid[] {Guid.Empty} ;
         BC00042_n584ActiveAppVersionId = new bool[] {false} ;
         BC00042_A598PublishedActiveAppVersionId = new Guid[] {Guid.Empty} ;
         BC00042_n598PublishedActiveAppVersionId = new bool[] {false} ;
         BC00042_A494LocationImage = new string[] {""} ;
         BC00042_A574ReceptionImage = new string[] {""} ;
         BC00042_n574ReceptionImage = new bool[] {false} ;
         GXt_char2 = "";
         BC000416_A523AppVersionId = new Guid[] {Guid.Empty} ;
         BC000417_A523AppVersionId = new Guid[] {Guid.Empty} ;
         BC000418_A42SupplierGenId = new Guid[] {Guid.Empty} ;
         BC000419_A527ResidentPackageId = new Guid[] {Guid.Empty} ;
         BC000420_A268AgendaCalendarId = new Guid[] {Guid.Empty} ;
         BC000421_A523AppVersionId = new Guid[] {Guid.Empty} ;
         BC000422_A366LocationDynamicFormId = new Guid[] {Guid.Empty} ;
         BC000422_A11OrganisationId = new Guid[] {Guid.Empty} ;
         BC000422_n11OrganisationId = new bool[] {false} ;
         BC000422_A29LocationId = new Guid[] {Guid.Empty} ;
         BC000422_n29LocationId = new bool[] {false} ;
         BC000423_A58ProductServiceId = new Guid[] {Guid.Empty} ;
         BC000423_A29LocationId = new Guid[] {Guid.Empty} ;
         BC000423_n29LocationId = new bool[] {false} ;
         BC000423_A11OrganisationId = new Guid[] {Guid.Empty} ;
         BC000423_n11OrganisationId = new bool[] {false} ;
         BC000424_A62ResidentId = new Guid[] {Guid.Empty} ;
         BC000424_A29LocationId = new Guid[] {Guid.Empty} ;
         BC000424_n29LocationId = new bool[] {false} ;
         BC000424_A11OrganisationId = new Guid[] {Guid.Empty} ;
         BC000424_n11OrganisationId = new bool[] {false} ;
         BC000425_A89ReceptionistId = new Guid[] {Guid.Empty} ;
         BC000425_A11OrganisationId = new Guid[] {Guid.Empty} ;
         BC000425_n11OrganisationId = new bool[] {false} ;
         BC000425_A29LocationId = new Guid[] {Guid.Empty} ;
         BC000425_n29LocationId = new bool[] {false} ;
         AV33SDT_TrnAttributes = new SdtSDT_TrnAttributes(context);
         BC000426_A29LocationId = new Guid[] {Guid.Empty} ;
         BC000426_n29LocationId = new bool[] {false} ;
         BC000426_A35LocationPhone = new string[] {""} ;
         BC000426_A329LocationZipCode = new string[] {""} ;
         BC000426_A31LocationName = new string[] {""} ;
         BC000426_A40000LocationImage_GXI = new string[] {""} ;
         BC000426_A327LocationCountry = new string[] {""} ;
         BC000426_A328LocationCity = new string[] {""} ;
         BC000426_A330LocationAddressLine1 = new string[] {""} ;
         BC000426_A331LocationAddressLine2 = new string[] {""} ;
         BC000426_A34LocationEmail = new string[] {""} ;
         BC000426_A355LocationPhoneCode = new string[] {""} ;
         BC000426_A356LocationPhoneNumber = new string[] {""} ;
         BC000426_A36LocationDescription = new string[] {""} ;
         BC000426_A568LocationBrandTheme = new string[] {""} ;
         BC000426_n568LocationBrandTheme = new bool[] {false} ;
         BC000426_A569LocationCtaTheme = new string[] {""} ;
         BC000426_n569LocationCtaTheme = new bool[] {false} ;
         BC000426_A570LocationHasMyCare = new bool[] {false} ;
         BC000426_A571LocationHasMyServices = new bool[] {false} ;
         BC000426_A572LocationHasMyLiving = new bool[] {false} ;
         BC000426_A573LocationHasOwnBrand = new bool[] {false} ;
         BC000426_A504ToolBoxDefaultProfileImage = new string[] {""} ;
         BC000426_n504ToolBoxDefaultProfileImage = new bool[] {false} ;
         BC000426_A503ToolBoxDefaultLogo = new string[] {""} ;
         BC000426_n503ToolBoxDefaultLogo = new bool[] {false} ;
         BC000426_A40001ReceptionImage_GXI = new string[] {""} ;
         BC000426_n40001ReceptionImage_GXI = new bool[] {false} ;
         BC000426_A575ReceptionDescription = new string[] {""} ;
         BC000426_n575ReceptionDescription = new bool[] {false} ;
         BC000426_A11OrganisationId = new Guid[] {Guid.Empty} ;
         BC000426_n11OrganisationId = new bool[] {false} ;
         BC000426_A273Trn_ThemeId = new Guid[] {Guid.Empty} ;
         BC000426_n273Trn_ThemeId = new bool[] {false} ;
         BC000426_A576LocationThemeId = new Guid[] {Guid.Empty} ;
         BC000426_n576LocationThemeId = new bool[] {false} ;
         BC000426_A584ActiveAppVersionId = new Guid[] {Guid.Empty} ;
         BC000426_n584ActiveAppVersionId = new bool[] {false} ;
         BC000426_A598PublishedActiveAppVersionId = new Guid[] {Guid.Empty} ;
         BC000426_n598PublishedActiveAppVersionId = new bool[] {false} ;
         BC000426_A494LocationImage = new string[] {""} ;
         BC000426_A574ReceptionImage = new string[] {""} ;
         BC000426_n574ReceptionImage = new bool[] {false} ;
         GXt_SdtSDT_TrnAttributes4 = new SdtSDT_TrnAttributes(context);
         i575ReceptionDescription = "";
         i574ReceptionImage = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         BC000427_A11OrganisationId = new Guid[] {Guid.Empty} ;
         BC000427_n11OrganisationId = new bool[] {false} ;
         pr_datastore1 = new DataStoreProvider(context, new GeneXus.Programs.trn_location_bc__datastore1(),
            new Object[][] {
            }
         );
         pr_gam = new DataStoreProvider(context, new GeneXus.Programs.trn_location_bc__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.trn_location_bc__default(),
            new Object[][] {
                new Object[] {
               BC00042_A29LocationId, BC00042_A35LocationPhone, BC00042_A329LocationZipCode, BC00042_A31LocationName, BC00042_A40000LocationImage_GXI, BC00042_A327LocationCountry, BC00042_A328LocationCity, BC00042_A330LocationAddressLine1, BC00042_A331LocationAddressLine2, BC00042_A34LocationEmail,
               BC00042_A355LocationPhoneCode, BC00042_A356LocationPhoneNumber, BC00042_A36LocationDescription, BC00042_A568LocationBrandTheme, BC00042_n568LocationBrandTheme, BC00042_A569LocationCtaTheme, BC00042_n569LocationCtaTheme, BC00042_A570LocationHasMyCare, BC00042_A571LocationHasMyServices, BC00042_A572LocationHasMyLiving,
               BC00042_A573LocationHasOwnBrand, BC00042_A504ToolBoxDefaultProfileImage, BC00042_n504ToolBoxDefaultProfileImage, BC00042_A503ToolBoxDefaultLogo, BC00042_n503ToolBoxDefaultLogo, BC00042_A40001ReceptionImage_GXI, BC00042_n40001ReceptionImage_GXI, BC00042_A575ReceptionDescription, BC00042_n575ReceptionDescription, BC00042_A11OrganisationId,
               BC00042_A273Trn_ThemeId, BC00042_n273Trn_ThemeId, BC00042_A576LocationThemeId, BC00042_n576LocationThemeId, BC00042_A584ActiveAppVersionId, BC00042_n584ActiveAppVersionId, BC00042_A598PublishedActiveAppVersionId, BC00042_n598PublishedActiveAppVersionId, BC00042_A494LocationImage, BC00042_A574ReceptionImage,
               BC00042_n574ReceptionImage
               }
               , new Object[] {
               BC00043_A29LocationId, BC00043_A35LocationPhone, BC00043_A329LocationZipCode, BC00043_A31LocationName, BC00043_A40000LocationImage_GXI, BC00043_A327LocationCountry, BC00043_A328LocationCity, BC00043_A330LocationAddressLine1, BC00043_A331LocationAddressLine2, BC00043_A34LocationEmail,
               BC00043_A355LocationPhoneCode, BC00043_A356LocationPhoneNumber, BC00043_A36LocationDescription, BC00043_A568LocationBrandTheme, BC00043_n568LocationBrandTheme, BC00043_A569LocationCtaTheme, BC00043_n569LocationCtaTheme, BC00043_A570LocationHasMyCare, BC00043_A571LocationHasMyServices, BC00043_A572LocationHasMyLiving,
               BC00043_A573LocationHasOwnBrand, BC00043_A504ToolBoxDefaultProfileImage, BC00043_n504ToolBoxDefaultProfileImage, BC00043_A503ToolBoxDefaultLogo, BC00043_n503ToolBoxDefaultLogo, BC00043_A40001ReceptionImage_GXI, BC00043_n40001ReceptionImage_GXI, BC00043_A575ReceptionDescription, BC00043_n575ReceptionDescription, BC00043_A11OrganisationId,
               BC00043_A273Trn_ThemeId, BC00043_n273Trn_ThemeId, BC00043_A576LocationThemeId, BC00043_n576LocationThemeId, BC00043_A584ActiveAppVersionId, BC00043_n584ActiveAppVersionId, BC00043_A598PublishedActiveAppVersionId, BC00043_n598PublishedActiveAppVersionId, BC00043_A494LocationImage, BC00043_A574ReceptionImage,
               BC00043_n574ReceptionImage
               }
               , new Object[] {
               BC00044_A11OrganisationId
               }
               , new Object[] {
               BC00045_A273Trn_ThemeId
               }
               , new Object[] {
               BC00046_A576LocationThemeId
               }
               , new Object[] {
               BC00047_A523AppVersionId
               }
               , new Object[] {
               BC00048_A523AppVersionId
               }
               , new Object[] {
               BC00049_A29LocationId, BC00049_A35LocationPhone, BC00049_A329LocationZipCode, BC00049_A31LocationName, BC00049_A40000LocationImage_GXI, BC00049_A327LocationCountry, BC00049_A328LocationCity, BC00049_A330LocationAddressLine1, BC00049_A331LocationAddressLine2, BC00049_A34LocationEmail,
               BC00049_A355LocationPhoneCode, BC00049_A356LocationPhoneNumber, BC00049_A36LocationDescription, BC00049_A568LocationBrandTheme, BC00049_n568LocationBrandTheme, BC00049_A569LocationCtaTheme, BC00049_n569LocationCtaTheme, BC00049_A570LocationHasMyCare, BC00049_A571LocationHasMyServices, BC00049_A572LocationHasMyLiving,
               BC00049_A573LocationHasOwnBrand, BC00049_A504ToolBoxDefaultProfileImage, BC00049_n504ToolBoxDefaultProfileImage, BC00049_A503ToolBoxDefaultLogo, BC00049_n503ToolBoxDefaultLogo, BC00049_A40001ReceptionImage_GXI, BC00049_n40001ReceptionImage_GXI, BC00049_A575ReceptionDescription, BC00049_n575ReceptionDescription, BC00049_A11OrganisationId,
               BC00049_A273Trn_ThemeId, BC00049_n273Trn_ThemeId, BC00049_A576LocationThemeId, BC00049_n576LocationThemeId, BC00049_A584ActiveAppVersionId, BC00049_n584ActiveAppVersionId, BC00049_A598PublishedActiveAppVersionId, BC00049_n598PublishedActiveAppVersionId, BC00049_A494LocationImage, BC00049_A574ReceptionImage,
               BC00049_n574ReceptionImage
               }
               , new Object[] {
               BC000410_A29LocationId, BC000410_A11OrganisationId
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               BC000416_A523AppVersionId
               }
               , new Object[] {
               BC000417_A523AppVersionId
               }
               , new Object[] {
               BC000418_A42SupplierGenId
               }
               , new Object[] {
               BC000419_A527ResidentPackageId
               }
               , new Object[] {
               BC000420_A268AgendaCalendarId
               }
               , new Object[] {
               BC000421_A523AppVersionId
               }
               , new Object[] {
               BC000422_A366LocationDynamicFormId, BC000422_A11OrganisationId, BC000422_A29LocationId
               }
               , new Object[] {
               BC000423_A58ProductServiceId, BC000423_A29LocationId, BC000423_A11OrganisationId
               }
               , new Object[] {
               BC000424_A62ResidentId, BC000424_A29LocationId, BC000424_A11OrganisationId
               }
               , new Object[] {
               BC000425_A89ReceptionistId, BC000425_A11OrganisationId, BC000425_A29LocationId
               }
               , new Object[] {
               BC000426_A29LocationId, BC000426_A35LocationPhone, BC000426_A329LocationZipCode, BC000426_A31LocationName, BC000426_A40000LocationImage_GXI, BC000426_A327LocationCountry, BC000426_A328LocationCity, BC000426_A330LocationAddressLine1, BC000426_A331LocationAddressLine2, BC000426_A34LocationEmail,
               BC000426_A355LocationPhoneCode, BC000426_A356LocationPhoneNumber, BC000426_A36LocationDescription, BC000426_A568LocationBrandTheme, BC000426_n568LocationBrandTheme, BC000426_A569LocationCtaTheme, BC000426_n569LocationCtaTheme, BC000426_A570LocationHasMyCare, BC000426_A571LocationHasMyServices, BC000426_A572LocationHasMyLiving,
               BC000426_A573LocationHasOwnBrand, BC000426_A504ToolBoxDefaultProfileImage, BC000426_n504ToolBoxDefaultProfileImage, BC000426_A503ToolBoxDefaultLogo, BC000426_n503ToolBoxDefaultLogo, BC000426_A40001ReceptionImage_GXI, BC000426_n40001ReceptionImage_GXI, BC000426_A575ReceptionDescription, BC000426_n575ReceptionDescription, BC000426_A11OrganisationId,
               BC000426_A273Trn_ThemeId, BC000426_n273Trn_ThemeId, BC000426_A576LocationThemeId, BC000426_n576LocationThemeId, BC000426_A584ActiveAppVersionId, BC000426_n584ActiveAppVersionId, BC000426_A598PublishedActiveAppVersionId, BC000426_n598PublishedActiveAppVersionId, BC000426_A494LocationImage, BC000426_A574ReceptionImage,
               BC000426_n574ReceptionImage
               }
               , new Object[] {
               BC000427_A11OrganisationId
               }
            }
         );
         Z573LocationHasOwnBrand = false;
         A573LocationHasOwnBrand = false;
         i573LocationHasOwnBrand = false;
         Z572LocationHasMyLiving = false;
         A572LocationHasMyLiving = false;
         i572LocationHasMyLiving = false;
         Z571LocationHasMyServices = false;
         A571LocationHasMyServices = false;
         i571LocationHasMyServices = false;
         Z570LocationHasMyCare = false;
         A570LocationHasMyCare = false;
         i570LocationHasMyCare = false;
         Z29LocationId = Guid.NewGuid( );
         n29LocationId = false;
         A29LocationId = Guid.NewGuid( );
         n29LocationId = false;
         AV45Pgmname = "Trn_Location_BC";
         Z574ReceptionImage = "";
         n574ReceptionImage = false;
         A574ReceptionImage = "";
         n574ReceptionImage = false;
         i574ReceptionImage = "";
         n574ReceptionImage = false;
         Z575ReceptionDescription = "";
         n575ReceptionDescription = false;
         A575ReceptionDescription = "";
         n575ReceptionDescription = false;
         i575ReceptionDescription = "";
         n575ReceptionDescription = false;
         Z273Trn_ThemeId = new prc_getdefaulttheme(context).executeUdp( );
         n273Trn_ThemeId = false;
         A273Trn_ThemeId = new prc_getdefaulttheme(context).executeUdp( );
         n273Trn_ThemeId = false;
         INITTRN();
         /* Execute Start event if defined. */
         /* Execute user event: Start */
         E12042 ();
         standaloneNotModal( ) ;
      }

      private short AnyError ;
      private short Gx_BScreen ;
      private short RcdFound103 ;
      private int trnEnded ;
      private int AV46GXV1 ;
      private string Gx_mode ;
      private string endTrnMsgTxt ;
      private string endTrnMsgCod ;
      private string imgReceptionimagevar_gximage ;
      private string AV45Pgmname ;
      private string Z35LocationPhone ;
      private string A35LocationPhone ;
      private string sMode103 ;
      private string GXt_char2 ;
      private bool returnInSub ;
      private bool Z570LocationHasMyCare ;
      private bool A570LocationHasMyCare ;
      private bool Z571LocationHasMyServices ;
      private bool A571LocationHasMyServices ;
      private bool Z572LocationHasMyLiving ;
      private bool A572LocationHasMyLiving ;
      private bool Z573LocationHasOwnBrand ;
      private bool A573LocationHasOwnBrand ;
      private bool n29LocationId ;
      private bool n575ReceptionDescription ;
      private bool n574ReceptionImage ;
      private bool n11OrganisationId ;
      private bool n568LocationBrandTheme ;
      private bool n569LocationCtaTheme ;
      private bool n504ToolBoxDefaultProfileImage ;
      private bool n503ToolBoxDefaultLogo ;
      private bool n40001ReceptionImage_GXI ;
      private bool n273Trn_ThemeId ;
      private bool n576LocationThemeId ;
      private bool n584ActiveAppVersionId ;
      private bool n598PublishedActiveAppVersionId ;
      private bool Gx_longc ;
      private bool i573LocationHasOwnBrand ;
      private bool i572LocationHasMyLiving ;
      private bool i571LocationHasMyServices ;
      private bool i570LocationHasMyCare ;
      private string Z36LocationDescription ;
      private string A36LocationDescription ;
      private string Z568LocationBrandTheme ;
      private string A568LocationBrandTheme ;
      private string Z569LocationCtaTheme ;
      private string A569LocationCtaTheme ;
      private string AV38LocationDescriptionVar ;
      private string AV39trnName ;
      private string AV37AttributeDescription ;
      private string AV31ReceptionDescriptionVar ;
      private string AV44Receptionimagevar_GXI ;
      private string Z329LocationZipCode ;
      private string A329LocationZipCode ;
      private string Z31LocationName ;
      private string A31LocationName ;
      private string Z327LocationCountry ;
      private string A327LocationCountry ;
      private string Z328LocationCity ;
      private string A328LocationCity ;
      private string Z330LocationAddressLine1 ;
      private string A330LocationAddressLine1 ;
      private string Z331LocationAddressLine2 ;
      private string A331LocationAddressLine2 ;
      private string Z34LocationEmail ;
      private string A34LocationEmail ;
      private string Z355LocationPhoneCode ;
      private string A355LocationPhoneCode ;
      private string Z356LocationPhoneNumber ;
      private string A356LocationPhoneNumber ;
      private string Z504ToolBoxDefaultProfileImage ;
      private string A504ToolBoxDefaultProfileImage ;
      private string Z503ToolBoxDefaultLogo ;
      private string A503ToolBoxDefaultLogo ;
      private string Z575ReceptionDescription ;
      private string A575ReceptionDescription ;
      private string Z40000LocationImage_GXI ;
      private string A40000LocationImage_GXI ;
      private string Z40001ReceptionImage_GXI ;
      private string A40001ReceptionImage_GXI ;
      private string i575ReceptionDescription ;
      private string AV30ReceptionImageVar ;
      private string Z494LocationImage ;
      private string A494LocationImage ;
      private string Z574ReceptionImage ;
      private string A574ReceptionImage ;
      private string i574ReceptionImage ;
      private Guid Z29LocationId ;
      private Guid A29LocationId ;
      private Guid Z11OrganisationId ;
      private Guid A11OrganisationId ;
      private Guid AV34Insert_ActiveAppVersionId ;
      private Guid AV40Insert_PublishedActiveAppVersionId ;
      private Guid AV25Insert_Trn_ThemeId ;
      private Guid AV32Insert_LocationThemeId ;
      private Guid AV7LocationId ;
      private Guid Z273Trn_ThemeId ;
      private Guid A273Trn_ThemeId ;
      private Guid Z576LocationThemeId ;
      private Guid A576LocationThemeId ;
      private Guid Z584ActiveAppVersionId ;
      private Guid A584ActiveAppVersionId ;
      private Guid Z598PublishedActiveAppVersionId ;
      private Guid A598PublishedActiveAppVersionId ;
      private Guid A523AppVersionId ;
      private Guid GXt_guid3 ;
      private IGxSession AV13WebSession ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GeneXus.Programs.wwpbaseobjects.SdtWWPContext AV9WWPContext ;
      private WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext AV12TrnContext ;
      private WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext_Attribute AV26TrnContextAtt ;
      private GXBaseCollection<SdtSDT_FileUploadData> AV41FilesToUpdate ;
      private GXBaseCollection<SdtSDT_FileUploadData> GXt_objcol_SdtSDT_FileUploadData1 ;
      private GXBaseCollection<SdtSDT_FileUploadData> AV42UploadedFiles ;
      private IDataStoreProvider pr_default ;
      private Guid[] BC00049_A29LocationId ;
      private bool[] BC00049_n29LocationId ;
      private string[] BC00049_A35LocationPhone ;
      private string[] BC00049_A329LocationZipCode ;
      private string[] BC00049_A31LocationName ;
      private string[] BC00049_A40000LocationImage_GXI ;
      private string[] BC00049_A327LocationCountry ;
      private string[] BC00049_A328LocationCity ;
      private string[] BC00049_A330LocationAddressLine1 ;
      private string[] BC00049_A331LocationAddressLine2 ;
      private string[] BC00049_A34LocationEmail ;
      private string[] BC00049_A355LocationPhoneCode ;
      private string[] BC00049_A356LocationPhoneNumber ;
      private string[] BC00049_A36LocationDescription ;
      private string[] BC00049_A568LocationBrandTheme ;
      private bool[] BC00049_n568LocationBrandTheme ;
      private string[] BC00049_A569LocationCtaTheme ;
      private bool[] BC00049_n569LocationCtaTheme ;
      private bool[] BC00049_A570LocationHasMyCare ;
      private bool[] BC00049_A571LocationHasMyServices ;
      private bool[] BC00049_A572LocationHasMyLiving ;
      private bool[] BC00049_A573LocationHasOwnBrand ;
      private string[] BC00049_A504ToolBoxDefaultProfileImage ;
      private bool[] BC00049_n504ToolBoxDefaultProfileImage ;
      private string[] BC00049_A503ToolBoxDefaultLogo ;
      private bool[] BC00049_n503ToolBoxDefaultLogo ;
      private string[] BC00049_A40001ReceptionImage_GXI ;
      private bool[] BC00049_n40001ReceptionImage_GXI ;
      private string[] BC00049_A575ReceptionDescription ;
      private bool[] BC00049_n575ReceptionDescription ;
      private Guid[] BC00049_A11OrganisationId ;
      private bool[] BC00049_n11OrganisationId ;
      private Guid[] BC00049_A273Trn_ThemeId ;
      private bool[] BC00049_n273Trn_ThemeId ;
      private Guid[] BC00049_A576LocationThemeId ;
      private bool[] BC00049_n576LocationThemeId ;
      private Guid[] BC00049_A584ActiveAppVersionId ;
      private bool[] BC00049_n584ActiveAppVersionId ;
      private Guid[] BC00049_A598PublishedActiveAppVersionId ;
      private bool[] BC00049_n598PublishedActiveAppVersionId ;
      private string[] BC00049_A494LocationImage ;
      private string[] BC00049_A574ReceptionImage ;
      private bool[] BC00049_n574ReceptionImage ;
      private Guid[] BC00047_A523AppVersionId ;
      private Guid[] BC00048_A523AppVersionId ;
      private Guid[] BC00044_A11OrganisationId ;
      private bool[] BC00044_n11OrganisationId ;
      private Guid[] BC00046_A576LocationThemeId ;
      private bool[] BC00046_n576LocationThemeId ;
      private Guid[] BC00045_A273Trn_ThemeId ;
      private bool[] BC00045_n273Trn_ThemeId ;
      private Guid[] BC000410_A29LocationId ;
      private bool[] BC000410_n29LocationId ;
      private Guid[] BC000410_A11OrganisationId ;
      private bool[] BC000410_n11OrganisationId ;
      private Guid[] BC00043_A29LocationId ;
      private bool[] BC00043_n29LocationId ;
      private string[] BC00043_A35LocationPhone ;
      private string[] BC00043_A329LocationZipCode ;
      private string[] BC00043_A31LocationName ;
      private string[] BC00043_A40000LocationImage_GXI ;
      private string[] BC00043_A327LocationCountry ;
      private string[] BC00043_A328LocationCity ;
      private string[] BC00043_A330LocationAddressLine1 ;
      private string[] BC00043_A331LocationAddressLine2 ;
      private string[] BC00043_A34LocationEmail ;
      private string[] BC00043_A355LocationPhoneCode ;
      private string[] BC00043_A356LocationPhoneNumber ;
      private string[] BC00043_A36LocationDescription ;
      private string[] BC00043_A568LocationBrandTheme ;
      private bool[] BC00043_n568LocationBrandTheme ;
      private string[] BC00043_A569LocationCtaTheme ;
      private bool[] BC00043_n569LocationCtaTheme ;
      private bool[] BC00043_A570LocationHasMyCare ;
      private bool[] BC00043_A571LocationHasMyServices ;
      private bool[] BC00043_A572LocationHasMyLiving ;
      private bool[] BC00043_A573LocationHasOwnBrand ;
      private string[] BC00043_A504ToolBoxDefaultProfileImage ;
      private bool[] BC00043_n504ToolBoxDefaultProfileImage ;
      private string[] BC00043_A503ToolBoxDefaultLogo ;
      private bool[] BC00043_n503ToolBoxDefaultLogo ;
      private string[] BC00043_A40001ReceptionImage_GXI ;
      private bool[] BC00043_n40001ReceptionImage_GXI ;
      private string[] BC00043_A575ReceptionDescription ;
      private bool[] BC00043_n575ReceptionDescription ;
      private Guid[] BC00043_A11OrganisationId ;
      private bool[] BC00043_n11OrganisationId ;
      private Guid[] BC00043_A273Trn_ThemeId ;
      private bool[] BC00043_n273Trn_ThemeId ;
      private Guid[] BC00043_A576LocationThemeId ;
      private bool[] BC00043_n576LocationThemeId ;
      private Guid[] BC00043_A584ActiveAppVersionId ;
      private bool[] BC00043_n584ActiveAppVersionId ;
      private Guid[] BC00043_A598PublishedActiveAppVersionId ;
      private bool[] BC00043_n598PublishedActiveAppVersionId ;
      private string[] BC00043_A494LocationImage ;
      private string[] BC00043_A574ReceptionImage ;
      private bool[] BC00043_n574ReceptionImage ;
      private Guid[] BC00042_A29LocationId ;
      private bool[] BC00042_n29LocationId ;
      private string[] BC00042_A35LocationPhone ;
      private string[] BC00042_A329LocationZipCode ;
      private string[] BC00042_A31LocationName ;
      private string[] BC00042_A40000LocationImage_GXI ;
      private string[] BC00042_A327LocationCountry ;
      private string[] BC00042_A328LocationCity ;
      private string[] BC00042_A330LocationAddressLine1 ;
      private string[] BC00042_A331LocationAddressLine2 ;
      private string[] BC00042_A34LocationEmail ;
      private string[] BC00042_A355LocationPhoneCode ;
      private string[] BC00042_A356LocationPhoneNumber ;
      private string[] BC00042_A36LocationDescription ;
      private string[] BC00042_A568LocationBrandTheme ;
      private bool[] BC00042_n568LocationBrandTheme ;
      private string[] BC00042_A569LocationCtaTheme ;
      private bool[] BC00042_n569LocationCtaTheme ;
      private bool[] BC00042_A570LocationHasMyCare ;
      private bool[] BC00042_A571LocationHasMyServices ;
      private bool[] BC00042_A572LocationHasMyLiving ;
      private bool[] BC00042_A573LocationHasOwnBrand ;
      private string[] BC00042_A504ToolBoxDefaultProfileImage ;
      private bool[] BC00042_n504ToolBoxDefaultProfileImage ;
      private string[] BC00042_A503ToolBoxDefaultLogo ;
      private bool[] BC00042_n503ToolBoxDefaultLogo ;
      private string[] BC00042_A40001ReceptionImage_GXI ;
      private bool[] BC00042_n40001ReceptionImage_GXI ;
      private string[] BC00042_A575ReceptionDescription ;
      private bool[] BC00042_n575ReceptionDescription ;
      private Guid[] BC00042_A11OrganisationId ;
      private bool[] BC00042_n11OrganisationId ;
      private Guid[] BC00042_A273Trn_ThemeId ;
      private bool[] BC00042_n273Trn_ThemeId ;
      private Guid[] BC00042_A576LocationThemeId ;
      private bool[] BC00042_n576LocationThemeId ;
      private Guid[] BC00042_A584ActiveAppVersionId ;
      private bool[] BC00042_n584ActiveAppVersionId ;
      private Guid[] BC00042_A598PublishedActiveAppVersionId ;
      private bool[] BC00042_n598PublishedActiveAppVersionId ;
      private string[] BC00042_A494LocationImage ;
      private string[] BC00042_A574ReceptionImage ;
      private bool[] BC00042_n574ReceptionImage ;
      private Guid[] BC000416_A523AppVersionId ;
      private Guid[] BC000417_A523AppVersionId ;
      private Guid[] BC000418_A42SupplierGenId ;
      private Guid[] BC000419_A527ResidentPackageId ;
      private Guid[] BC000420_A268AgendaCalendarId ;
      private Guid[] BC000421_A523AppVersionId ;
      private Guid[] BC000422_A366LocationDynamicFormId ;
      private Guid[] BC000422_A11OrganisationId ;
      private bool[] BC000422_n11OrganisationId ;
      private Guid[] BC000422_A29LocationId ;
      private bool[] BC000422_n29LocationId ;
      private Guid[] BC000423_A58ProductServiceId ;
      private Guid[] BC000423_A29LocationId ;
      private bool[] BC000423_n29LocationId ;
      private Guid[] BC000423_A11OrganisationId ;
      private bool[] BC000423_n11OrganisationId ;
      private Guid[] BC000424_A62ResidentId ;
      private Guid[] BC000424_A29LocationId ;
      private bool[] BC000424_n29LocationId ;
      private Guid[] BC000424_A11OrganisationId ;
      private bool[] BC000424_n11OrganisationId ;
      private Guid[] BC000425_A89ReceptionistId ;
      private Guid[] BC000425_A11OrganisationId ;
      private bool[] BC000425_n11OrganisationId ;
      private Guid[] BC000425_A29LocationId ;
      private bool[] BC000425_n29LocationId ;
      private SdtSDT_TrnAttributes AV33SDT_TrnAttributes ;
      private Guid[] BC000426_A29LocationId ;
      private bool[] BC000426_n29LocationId ;
      private string[] BC000426_A35LocationPhone ;
      private string[] BC000426_A329LocationZipCode ;
      private string[] BC000426_A31LocationName ;
      private string[] BC000426_A40000LocationImage_GXI ;
      private string[] BC000426_A327LocationCountry ;
      private string[] BC000426_A328LocationCity ;
      private string[] BC000426_A330LocationAddressLine1 ;
      private string[] BC000426_A331LocationAddressLine2 ;
      private string[] BC000426_A34LocationEmail ;
      private string[] BC000426_A355LocationPhoneCode ;
      private string[] BC000426_A356LocationPhoneNumber ;
      private string[] BC000426_A36LocationDescription ;
      private string[] BC000426_A568LocationBrandTheme ;
      private bool[] BC000426_n568LocationBrandTheme ;
      private string[] BC000426_A569LocationCtaTheme ;
      private bool[] BC000426_n569LocationCtaTheme ;
      private bool[] BC000426_A570LocationHasMyCare ;
      private bool[] BC000426_A571LocationHasMyServices ;
      private bool[] BC000426_A572LocationHasMyLiving ;
      private bool[] BC000426_A573LocationHasOwnBrand ;
      private string[] BC000426_A504ToolBoxDefaultProfileImage ;
      private bool[] BC000426_n504ToolBoxDefaultProfileImage ;
      private string[] BC000426_A503ToolBoxDefaultLogo ;
      private bool[] BC000426_n503ToolBoxDefaultLogo ;
      private string[] BC000426_A40001ReceptionImage_GXI ;
      private bool[] BC000426_n40001ReceptionImage_GXI ;
      private string[] BC000426_A575ReceptionDescription ;
      private bool[] BC000426_n575ReceptionDescription ;
      private Guid[] BC000426_A11OrganisationId ;
      private bool[] BC000426_n11OrganisationId ;
      private Guid[] BC000426_A273Trn_ThemeId ;
      private bool[] BC000426_n273Trn_ThemeId ;
      private Guid[] BC000426_A576LocationThemeId ;
      private bool[] BC000426_n576LocationThemeId ;
      private Guid[] BC000426_A584ActiveAppVersionId ;
      private bool[] BC000426_n584ActiveAppVersionId ;
      private Guid[] BC000426_A598PublishedActiveAppVersionId ;
      private bool[] BC000426_n598PublishedActiveAppVersionId ;
      private string[] BC000426_A494LocationImage ;
      private string[] BC000426_A574ReceptionImage ;
      private bool[] BC000426_n574ReceptionImage ;
      private SdtSDT_TrnAttributes GXt_SdtSDT_TrnAttributes4 ;
      private SdtTrn_Location bcTrn_Location ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private Guid[] BC000427_A11OrganisationId ;
      private bool[] BC000427_n11OrganisationId ;
      private IDataStoreProvider pr_datastore1 ;
      private IDataStoreProvider pr_gam ;
   }

   public class trn_location_bc__datastore1 : DataStoreHelperBase, IDataStoreHelper
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

 public class trn_location_bc__gam : DataStoreHelperBase, IDataStoreHelper
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

public class trn_location_bc__default : DataStoreHelperBase, IDataStoreHelper
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
      ,new ForEachCursor(def[6])
      ,new ForEachCursor(def[7])
      ,new ForEachCursor(def[8])
      ,new UpdateCursor(def[9])
      ,new UpdateCursor(def[10])
      ,new UpdateCursor(def[11])
      ,new UpdateCursor(def[12])
      ,new UpdateCursor(def[13])
      ,new ForEachCursor(def[14])
      ,new ForEachCursor(def[15])
      ,new ForEachCursor(def[16])
      ,new ForEachCursor(def[17])
      ,new ForEachCursor(def[18])
      ,new ForEachCursor(def[19])
      ,new ForEachCursor(def[20])
      ,new ForEachCursor(def[21])
      ,new ForEachCursor(def[22])
      ,new ForEachCursor(def[23])
      ,new ForEachCursor(def[24])
      ,new ForEachCursor(def[25])
    };
 }

 private static CursorDef[] def;
 private void cursorDefinitions( )
 {
    if ( def == null )
    {
       Object[] prmBC00042;
       prmBC00042 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC00043;
       prmBC00043 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC00044;
       prmBC00044 = new Object[] {
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC00045;
       prmBC00045 = new Object[] {
       new ParDef("Trn_ThemeId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC00046;
       prmBC00046 = new Object[] {
       new ParDef("LocationThemeId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC00047;
       prmBC00047 = new Object[] {
       new ParDef("ActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC00048;
       prmBC00048 = new Object[] {
       new ParDef("PublishedActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC00049;
       prmBC00049 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000410;
       prmBC000410 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000411;
       prmBC000411 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("LocationPhone",GXType.Char,20,0) ,
       new ParDef("LocationZipCode",GXType.VarChar,100,0) ,
       new ParDef("LocationName",GXType.VarChar,100,0) ,
       new ParDef("LocationImage",GXType.Byte,1024,0){InDB=false} ,
       new ParDef("LocationImage_GXI",GXType.VarChar,2048,0){AddAtt=true, ImgIdx=4, Tbl="Trn_Location", Fld="LocationImage"} ,
       new ParDef("LocationCountry",GXType.VarChar,100,0) ,
       new ParDef("LocationCity",GXType.VarChar,100,0) ,
       new ParDef("LocationAddressLine1",GXType.VarChar,100,0) ,
       new ParDef("LocationAddressLine2",GXType.VarChar,100,0) ,
       new ParDef("LocationEmail",GXType.VarChar,100,0) ,
       new ParDef("LocationPhoneCode",GXType.VarChar,40,0) ,
       new ParDef("LocationPhoneNumber",GXType.VarChar,9,0) ,
       new ParDef("LocationDescription",GXType.LongVarChar,2097152,0) ,
       new ParDef("LocationBrandTheme",GXType.LongVarChar,2097152,0){Nullable=true} ,
       new ParDef("LocationCtaTheme",GXType.LongVarChar,1000,0){Nullable=true} ,
       new ParDef("LocationHasMyCare",GXType.Boolean,4,0) ,
       new ParDef("LocationHasMyServices",GXType.Boolean,4,0) ,
       new ParDef("LocationHasMyLiving",GXType.Boolean,4,0) ,
       new ParDef("LocationHasOwnBrand",GXType.Boolean,4,0) ,
       new ParDef("ToolBoxDefaultProfileImage",GXType.VarChar,200,0){Nullable=true} ,
       new ParDef("ToolBoxDefaultLogo",GXType.VarChar,200,0){Nullable=true} ,
       new ParDef("ReceptionImage",GXType.Byte,1024,0){Nullable=true,InDB=false} ,
       new ParDef("ReceptionImage_GXI",GXType.VarChar,2048,0){Nullable=true,AddAtt=true, ImgIdx=22, Tbl="Trn_Location", Fld="ReceptionImage"} ,
       new ParDef("ReceptionDescription",GXType.VarChar,200,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("Trn_ThemeId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("LocationThemeId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("ActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("PublishedActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000412;
       prmBC000412 = new Object[] {
       new ParDef("LocationPhone",GXType.Char,20,0) ,
       new ParDef("LocationZipCode",GXType.VarChar,100,0) ,
       new ParDef("LocationName",GXType.VarChar,100,0) ,
       new ParDef("LocationCountry",GXType.VarChar,100,0) ,
       new ParDef("LocationCity",GXType.VarChar,100,0) ,
       new ParDef("LocationAddressLine1",GXType.VarChar,100,0) ,
       new ParDef("LocationAddressLine2",GXType.VarChar,100,0) ,
       new ParDef("LocationEmail",GXType.VarChar,100,0) ,
       new ParDef("LocationPhoneCode",GXType.VarChar,40,0) ,
       new ParDef("LocationPhoneNumber",GXType.VarChar,9,0) ,
       new ParDef("LocationDescription",GXType.LongVarChar,2097152,0) ,
       new ParDef("LocationBrandTheme",GXType.LongVarChar,2097152,0){Nullable=true} ,
       new ParDef("LocationCtaTheme",GXType.LongVarChar,1000,0){Nullable=true} ,
       new ParDef("LocationHasMyCare",GXType.Boolean,4,0) ,
       new ParDef("LocationHasMyServices",GXType.Boolean,4,0) ,
       new ParDef("LocationHasMyLiving",GXType.Boolean,4,0) ,
       new ParDef("LocationHasOwnBrand",GXType.Boolean,4,0) ,
       new ParDef("ToolBoxDefaultProfileImage",GXType.VarChar,200,0){Nullable=true} ,
       new ParDef("ToolBoxDefaultLogo",GXType.VarChar,200,0){Nullable=true} ,
       new ParDef("ReceptionDescription",GXType.VarChar,200,0){Nullable=true} ,
       new ParDef("Trn_ThemeId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("LocationThemeId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("ActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("PublishedActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000413;
       prmBC000413 = new Object[] {
       new ParDef("LocationImage",GXType.Byte,1024,0){InDB=false} ,
       new ParDef("LocationImage_GXI",GXType.VarChar,2048,0){AddAtt=true, ImgIdx=0, Tbl="Trn_Location", Fld="LocationImage"} ,
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000414;
       prmBC000414 = new Object[] {
       new ParDef("ReceptionImage",GXType.Byte,1024,0){Nullable=true,InDB=false} ,
       new ParDef("ReceptionImage_GXI",GXType.VarChar,2048,0){Nullable=true,AddAtt=true, ImgIdx=0, Tbl="Trn_Location", Fld="ReceptionImage"} ,
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000415;
       prmBC000415 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000416;
       prmBC000416 = new Object[] {
       new ParDef("ActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000417;
       prmBC000417 = new Object[] {
       new ParDef("PublishedActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000418;
       prmBC000418 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000419;
       prmBC000419 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000420;
       prmBC000420 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000421;
       prmBC000421 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000422;
       prmBC000422 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000423;
       prmBC000423 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000424;
       prmBC000424 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000425;
       prmBC000425 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000426;
       prmBC000426 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmBC000427;
       prmBC000427 = new Object[] {
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       def= new CursorDef[] {
           new CursorDef("BC00042", "SELECT LocationId, LocationPhone, LocationZipCode, LocationName, LocationImage_GXI, LocationCountry, LocationCity, LocationAddressLine1, LocationAddressLine2, LocationEmail, LocationPhoneCode, LocationPhoneNumber, LocationDescription, LocationBrandTheme, LocationCtaTheme, LocationHasMyCare, LocationHasMyServices, LocationHasMyLiving, LocationHasOwnBrand, ToolBoxDefaultProfileImage, ToolBoxDefaultLogo, ReceptionImage_GXI, ReceptionDescription, OrganisationId, Trn_ThemeId, LocationThemeId, ActiveAppVersionId, PublishedActiveAppVersionId, LocationImage, ReceptionImage FROM Trn_Location WHERE LocationId = :LocationId AND OrganisationId = :OrganisationId  FOR UPDATE OF Trn_Location",true, GxErrorMask.GX_NOMASK, false, this,prmBC00042,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC00043", "SELECT LocationId, LocationPhone, LocationZipCode, LocationName, LocationImage_GXI, LocationCountry, LocationCity, LocationAddressLine1, LocationAddressLine2, LocationEmail, LocationPhoneCode, LocationPhoneNumber, LocationDescription, LocationBrandTheme, LocationCtaTheme, LocationHasMyCare, LocationHasMyServices, LocationHasMyLiving, LocationHasOwnBrand, ToolBoxDefaultProfileImage, ToolBoxDefaultLogo, ReceptionImage_GXI, ReceptionDescription, OrganisationId, Trn_ThemeId, LocationThemeId, ActiveAppVersionId, PublishedActiveAppVersionId, LocationImage, ReceptionImage FROM Trn_Location WHERE LocationId = :LocationId AND OrganisationId = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00043,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC00044", "SELECT OrganisationId FROM Trn_Organisation WHERE OrganisationId = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00044,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC00045", "SELECT Trn_ThemeId FROM Trn_Theme WHERE Trn_ThemeId = :Trn_ThemeId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00045,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC00046", "SELECT Trn_ThemeId AS LocationThemeId FROM Trn_Theme WHERE Trn_ThemeId = :LocationThemeId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00046,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC00047", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :ActiveAppVersionId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00047,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC00048", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :PublishedActiveAppVersionId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00048,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC00049", "SELECT TM1.LocationId, TM1.LocationPhone, TM1.LocationZipCode, TM1.LocationName, TM1.LocationImage_GXI, TM1.LocationCountry, TM1.LocationCity, TM1.LocationAddressLine1, TM1.LocationAddressLine2, TM1.LocationEmail, TM1.LocationPhoneCode, TM1.LocationPhoneNumber, TM1.LocationDescription, TM1.LocationBrandTheme, TM1.LocationCtaTheme, TM1.LocationHasMyCare, TM1.LocationHasMyServices, TM1.LocationHasMyLiving, TM1.LocationHasOwnBrand, TM1.ToolBoxDefaultProfileImage, TM1.ToolBoxDefaultLogo, TM1.ReceptionImage_GXI, TM1.ReceptionDescription, TM1.OrganisationId, TM1.Trn_ThemeId, TM1.LocationThemeId AS LocationThemeId, TM1.ActiveAppVersionId, TM1.PublishedActiveAppVersionId, TM1.LocationImage, TM1.ReceptionImage FROM Trn_Location TM1 WHERE TM1.LocationId = :LocationId and TM1.OrganisationId = :OrganisationId ORDER BY TM1.LocationId, TM1.OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00049,100, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC000410", "SELECT LocationId, OrganisationId FROM Trn_Location WHERE LocationId = :LocationId AND OrganisationId = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC000410,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC000411", "SAVEPOINT gxupdate;INSERT INTO Trn_Location(LocationId, LocationPhone, LocationZipCode, LocationName, LocationImage, LocationImage_GXI, LocationCountry, LocationCity, LocationAddressLine1, LocationAddressLine2, LocationEmail, LocationPhoneCode, LocationPhoneNumber, LocationDescription, LocationBrandTheme, LocationCtaTheme, LocationHasMyCare, LocationHasMyServices, LocationHasMyLiving, LocationHasOwnBrand, ToolBoxDefaultProfileImage, ToolBoxDefaultLogo, ReceptionImage, ReceptionImage_GXI, ReceptionDescription, OrganisationId, Trn_ThemeId, LocationThemeId, ActiveAppVersionId, PublishedActiveAppVersionId) VALUES(:LocationId, :LocationPhone, :LocationZipCode, :LocationName, :LocationImage, :LocationImage_GXI, :LocationCountry, :LocationCity, :LocationAddressLine1, :LocationAddressLine2, :LocationEmail, :LocationPhoneCode, :LocationPhoneNumber, :LocationDescription, :LocationBrandTheme, :LocationCtaTheme, :LocationHasMyCare, :LocationHasMyServices, :LocationHasMyLiving, :LocationHasOwnBrand, :ToolBoxDefaultProfileImage, :ToolBoxDefaultLogo, :ReceptionImage, :ReceptionImage_GXI, :ReceptionDescription, :OrganisationId, :Trn_ThemeId, :LocationThemeId, :ActiveAppVersionId, :PublishedActiveAppVersionId);RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK,prmBC000411)
          ,new CursorDef("BC000412", "SAVEPOINT gxupdate;UPDATE Trn_Location SET LocationPhone=:LocationPhone, LocationZipCode=:LocationZipCode, LocationName=:LocationName, LocationCountry=:LocationCountry, LocationCity=:LocationCity, LocationAddressLine1=:LocationAddressLine1, LocationAddressLine2=:LocationAddressLine2, LocationEmail=:LocationEmail, LocationPhoneCode=:LocationPhoneCode, LocationPhoneNumber=:LocationPhoneNumber, LocationDescription=:LocationDescription, LocationBrandTheme=:LocationBrandTheme, LocationCtaTheme=:LocationCtaTheme, LocationHasMyCare=:LocationHasMyCare, LocationHasMyServices=:LocationHasMyServices, LocationHasMyLiving=:LocationHasMyLiving, LocationHasOwnBrand=:LocationHasOwnBrand, ToolBoxDefaultProfileImage=:ToolBoxDefaultProfileImage, ToolBoxDefaultLogo=:ToolBoxDefaultLogo, ReceptionDescription=:ReceptionDescription, Trn_ThemeId=:Trn_ThemeId, LocationThemeId=:LocationThemeId, ActiveAppVersionId=:ActiveAppVersionId, PublishedActiveAppVersionId=:PublishedActiveAppVersionId  WHERE LocationId = :LocationId AND OrganisationId = :OrganisationId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK,prmBC000412)
          ,new CursorDef("BC000413", "SAVEPOINT gxupdate;UPDATE Trn_Location SET LocationImage=:LocationImage, LocationImage_GXI=:LocationImage_GXI  WHERE LocationId = :LocationId AND OrganisationId = :OrganisationId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK,prmBC000413)
          ,new CursorDef("BC000414", "SAVEPOINT gxupdate;UPDATE Trn_Location SET ReceptionImage=:ReceptionImage, ReceptionImage_GXI=:ReceptionImage_GXI  WHERE LocationId = :LocationId AND OrganisationId = :OrganisationId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK,prmBC000414)
          ,new CursorDef("BC000415", "SAVEPOINT gxupdate;DELETE FROM Trn_Location  WHERE LocationId = :LocationId AND OrganisationId = :OrganisationId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK,prmBC000415)
          ,new CursorDef("BC000416", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :ActiveAppVersionId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC000416,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC000417", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :PublishedActiveAppVersionId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC000417,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC000418", "SELECT SupplierGenId FROM Trn_SupplierGen WHERE SG_LocationSupplierLocationId = :LocationId AND SG_LocationSupplierOrganisatio = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC000418,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("BC000419", "SELECT ResidentPackageId FROM Trn_ResidentPackage WHERE SG_LocationId = :LocationId AND SG_OrganisationId = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC000419,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("BC000420", "SELECT AgendaCalendarId FROM Trn_AgendaCalendar WHERE LocationId = :LocationId AND OrganisationId = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC000420,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("BC000421", "SELECT AppVersionId FROM Trn_AppVersion WHERE LocationId = :LocationId AND OrganisationId = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC000421,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("BC000422", "SELECT LocationDynamicFormId, OrganisationId, LocationId FROM Trn_LocationDynamicForm WHERE LocationId = :LocationId AND OrganisationId = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC000422,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("BC000423", "SELECT ProductServiceId, LocationId, OrganisationId FROM Trn_ProductService WHERE LocationId = :LocationId AND OrganisationId = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC000423,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("BC000424", "SELECT ResidentId, LocationId, OrganisationId FROM Trn_Resident WHERE LocationId = :LocationId AND OrganisationId = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC000424,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("BC000425", "SELECT ReceptionistId, OrganisationId, LocationId FROM Trn_Receptionist WHERE LocationId = :LocationId AND OrganisationId = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC000425,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("BC000426", "SELECT TM1.LocationId, TM1.LocationPhone, TM1.LocationZipCode, TM1.LocationName, TM1.LocationImage_GXI, TM1.LocationCountry, TM1.LocationCity, TM1.LocationAddressLine1, TM1.LocationAddressLine2, TM1.LocationEmail, TM1.LocationPhoneCode, TM1.LocationPhoneNumber, TM1.LocationDescription, TM1.LocationBrandTheme, TM1.LocationCtaTheme, TM1.LocationHasMyCare, TM1.LocationHasMyServices, TM1.LocationHasMyLiving, TM1.LocationHasOwnBrand, TM1.ToolBoxDefaultProfileImage, TM1.ToolBoxDefaultLogo, TM1.ReceptionImage_GXI, TM1.ReceptionDescription, TM1.OrganisationId, TM1.Trn_ThemeId, TM1.LocationThemeId AS LocationThemeId, TM1.ActiveAppVersionId, TM1.PublishedActiveAppVersionId, TM1.LocationImage, TM1.ReceptionImage FROM Trn_Location TM1 WHERE TM1.LocationId = :LocationId and TM1.OrganisationId = :OrganisationId ORDER BY TM1.LocationId, TM1.OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC000426,100, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("BC000427", "SELECT OrganisationId FROM Trn_Organisation WHERE OrganisationId = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC000427,1, GxCacheFrequency.OFF ,true,false )
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
             ((string[]) buf[1])[0] = rslt.getString(2, 20);
             ((string[]) buf[2])[0] = rslt.getVarchar(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((string[]) buf[4])[0] = rslt.getMultimediaUri(5);
             ((string[]) buf[5])[0] = rslt.getVarchar(6);
             ((string[]) buf[6])[0] = rslt.getVarchar(7);
             ((string[]) buf[7])[0] = rslt.getVarchar(8);
             ((string[]) buf[8])[0] = rslt.getVarchar(9);
             ((string[]) buf[9])[0] = rslt.getVarchar(10);
             ((string[]) buf[10])[0] = rslt.getVarchar(11);
             ((string[]) buf[11])[0] = rslt.getVarchar(12);
             ((string[]) buf[12])[0] = rslt.getLongVarchar(13);
             ((string[]) buf[13])[0] = rslt.getLongVarchar(14);
             ((bool[]) buf[14])[0] = rslt.wasNull(14);
             ((string[]) buf[15])[0] = rslt.getLongVarchar(15);
             ((bool[]) buf[16])[0] = rslt.wasNull(15);
             ((bool[]) buf[17])[0] = rslt.getBool(16);
             ((bool[]) buf[18])[0] = rslt.getBool(17);
             ((bool[]) buf[19])[0] = rslt.getBool(18);
             ((bool[]) buf[20])[0] = rslt.getBool(19);
             ((string[]) buf[21])[0] = rslt.getVarchar(20);
             ((bool[]) buf[22])[0] = rslt.wasNull(20);
             ((string[]) buf[23])[0] = rslt.getVarchar(21);
             ((bool[]) buf[24])[0] = rslt.wasNull(21);
             ((string[]) buf[25])[0] = rslt.getMultimediaUri(22);
             ((bool[]) buf[26])[0] = rslt.wasNull(22);
             ((string[]) buf[27])[0] = rslt.getVarchar(23);
             ((bool[]) buf[28])[0] = rslt.wasNull(23);
             ((Guid[]) buf[29])[0] = rslt.getGuid(24);
             ((Guid[]) buf[30])[0] = rslt.getGuid(25);
             ((bool[]) buf[31])[0] = rslt.wasNull(25);
             ((Guid[]) buf[32])[0] = rslt.getGuid(26);
             ((bool[]) buf[33])[0] = rslt.wasNull(26);
             ((Guid[]) buf[34])[0] = rslt.getGuid(27);
             ((bool[]) buf[35])[0] = rslt.wasNull(27);
             ((Guid[]) buf[36])[0] = rslt.getGuid(28);
             ((bool[]) buf[37])[0] = rslt.wasNull(28);
             ((string[]) buf[38])[0] = rslt.getMultimediaFile(29, rslt.getVarchar(5));
             ((string[]) buf[39])[0] = rslt.getMultimediaFile(30, rslt.getVarchar(22));
             ((bool[]) buf[40])[0] = rslt.wasNull(30);
             return;
          case 1 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((string[]) buf[1])[0] = rslt.getString(2, 20);
             ((string[]) buf[2])[0] = rslt.getVarchar(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((string[]) buf[4])[0] = rslt.getMultimediaUri(5);
             ((string[]) buf[5])[0] = rslt.getVarchar(6);
             ((string[]) buf[6])[0] = rslt.getVarchar(7);
             ((string[]) buf[7])[0] = rslt.getVarchar(8);
             ((string[]) buf[8])[0] = rslt.getVarchar(9);
             ((string[]) buf[9])[0] = rslt.getVarchar(10);
             ((string[]) buf[10])[0] = rslt.getVarchar(11);
             ((string[]) buf[11])[0] = rslt.getVarchar(12);
             ((string[]) buf[12])[0] = rslt.getLongVarchar(13);
             ((string[]) buf[13])[0] = rslt.getLongVarchar(14);
             ((bool[]) buf[14])[0] = rslt.wasNull(14);
             ((string[]) buf[15])[0] = rslt.getLongVarchar(15);
             ((bool[]) buf[16])[0] = rslt.wasNull(15);
             ((bool[]) buf[17])[0] = rslt.getBool(16);
             ((bool[]) buf[18])[0] = rslt.getBool(17);
             ((bool[]) buf[19])[0] = rslt.getBool(18);
             ((bool[]) buf[20])[0] = rslt.getBool(19);
             ((string[]) buf[21])[0] = rslt.getVarchar(20);
             ((bool[]) buf[22])[0] = rslt.wasNull(20);
             ((string[]) buf[23])[0] = rslt.getVarchar(21);
             ((bool[]) buf[24])[0] = rslt.wasNull(21);
             ((string[]) buf[25])[0] = rslt.getMultimediaUri(22);
             ((bool[]) buf[26])[0] = rslt.wasNull(22);
             ((string[]) buf[27])[0] = rslt.getVarchar(23);
             ((bool[]) buf[28])[0] = rslt.wasNull(23);
             ((Guid[]) buf[29])[0] = rslt.getGuid(24);
             ((Guid[]) buf[30])[0] = rslt.getGuid(25);
             ((bool[]) buf[31])[0] = rslt.wasNull(25);
             ((Guid[]) buf[32])[0] = rslt.getGuid(26);
             ((bool[]) buf[33])[0] = rslt.wasNull(26);
             ((Guid[]) buf[34])[0] = rslt.getGuid(27);
             ((bool[]) buf[35])[0] = rslt.wasNull(27);
             ((Guid[]) buf[36])[0] = rslt.getGuid(28);
             ((bool[]) buf[37])[0] = rslt.wasNull(28);
             ((string[]) buf[38])[0] = rslt.getMultimediaFile(29, rslt.getVarchar(5));
             ((string[]) buf[39])[0] = rslt.getMultimediaFile(30, rslt.getVarchar(22));
             ((bool[]) buf[40])[0] = rslt.wasNull(30);
             return;
          case 2 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             return;
          case 3 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             return;
          case 4 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             return;
          case 5 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             return;
          case 6 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             return;
          case 7 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((string[]) buf[1])[0] = rslt.getString(2, 20);
             ((string[]) buf[2])[0] = rslt.getVarchar(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((string[]) buf[4])[0] = rslt.getMultimediaUri(5);
             ((string[]) buf[5])[0] = rslt.getVarchar(6);
             ((string[]) buf[6])[0] = rslt.getVarchar(7);
             ((string[]) buf[7])[0] = rslt.getVarchar(8);
             ((string[]) buf[8])[0] = rslt.getVarchar(9);
             ((string[]) buf[9])[0] = rslt.getVarchar(10);
             ((string[]) buf[10])[0] = rslt.getVarchar(11);
             ((string[]) buf[11])[0] = rslt.getVarchar(12);
             ((string[]) buf[12])[0] = rslt.getLongVarchar(13);
             ((string[]) buf[13])[0] = rslt.getLongVarchar(14);
             ((bool[]) buf[14])[0] = rslt.wasNull(14);
             ((string[]) buf[15])[0] = rslt.getLongVarchar(15);
             ((bool[]) buf[16])[0] = rslt.wasNull(15);
             ((bool[]) buf[17])[0] = rslt.getBool(16);
             ((bool[]) buf[18])[0] = rslt.getBool(17);
             ((bool[]) buf[19])[0] = rslt.getBool(18);
             ((bool[]) buf[20])[0] = rslt.getBool(19);
             ((string[]) buf[21])[0] = rslt.getVarchar(20);
             ((bool[]) buf[22])[0] = rslt.wasNull(20);
             ((string[]) buf[23])[0] = rslt.getVarchar(21);
             ((bool[]) buf[24])[0] = rslt.wasNull(21);
             ((string[]) buf[25])[0] = rslt.getMultimediaUri(22);
             ((bool[]) buf[26])[0] = rslt.wasNull(22);
             ((string[]) buf[27])[0] = rslt.getVarchar(23);
             ((bool[]) buf[28])[0] = rslt.wasNull(23);
             ((Guid[]) buf[29])[0] = rslt.getGuid(24);
             ((Guid[]) buf[30])[0] = rslt.getGuid(25);
             ((bool[]) buf[31])[0] = rslt.wasNull(25);
             ((Guid[]) buf[32])[0] = rslt.getGuid(26);
             ((bool[]) buf[33])[0] = rslt.wasNull(26);
             ((Guid[]) buf[34])[0] = rslt.getGuid(27);
             ((bool[]) buf[35])[0] = rslt.wasNull(27);
             ((Guid[]) buf[36])[0] = rslt.getGuid(28);
             ((bool[]) buf[37])[0] = rslt.wasNull(28);
             ((string[]) buf[38])[0] = rslt.getMultimediaFile(29, rslt.getVarchar(5));
             ((string[]) buf[39])[0] = rslt.getMultimediaFile(30, rslt.getVarchar(22));
             ((bool[]) buf[40])[0] = rslt.wasNull(30);
             return;
          case 8 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             return;
          case 14 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             return;
          case 15 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             return;
          case 16 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
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
          case 20 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             ((Guid[]) buf[2])[0] = rslt.getGuid(3);
             return;
          case 21 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             ((Guid[]) buf[2])[0] = rslt.getGuid(3);
             return;
          case 22 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             ((Guid[]) buf[2])[0] = rslt.getGuid(3);
             return;
          case 23 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             ((Guid[]) buf[2])[0] = rslt.getGuid(3);
             return;
          case 24 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((string[]) buf[1])[0] = rslt.getString(2, 20);
             ((string[]) buf[2])[0] = rslt.getVarchar(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((string[]) buf[4])[0] = rslt.getMultimediaUri(5);
             ((string[]) buf[5])[0] = rslt.getVarchar(6);
             ((string[]) buf[6])[0] = rslt.getVarchar(7);
             ((string[]) buf[7])[0] = rslt.getVarchar(8);
             ((string[]) buf[8])[0] = rslt.getVarchar(9);
             ((string[]) buf[9])[0] = rslt.getVarchar(10);
             ((string[]) buf[10])[0] = rslt.getVarchar(11);
             ((string[]) buf[11])[0] = rslt.getVarchar(12);
             ((string[]) buf[12])[0] = rslt.getLongVarchar(13);
             ((string[]) buf[13])[0] = rslt.getLongVarchar(14);
             ((bool[]) buf[14])[0] = rslt.wasNull(14);
             ((string[]) buf[15])[0] = rslt.getLongVarchar(15);
             ((bool[]) buf[16])[0] = rslt.wasNull(15);
             ((bool[]) buf[17])[0] = rslt.getBool(16);
             ((bool[]) buf[18])[0] = rslt.getBool(17);
             ((bool[]) buf[19])[0] = rslt.getBool(18);
             ((bool[]) buf[20])[0] = rslt.getBool(19);
             ((string[]) buf[21])[0] = rslt.getVarchar(20);
             ((bool[]) buf[22])[0] = rslt.wasNull(20);
             ((string[]) buf[23])[0] = rslt.getVarchar(21);
             ((bool[]) buf[24])[0] = rslt.wasNull(21);
             ((string[]) buf[25])[0] = rslt.getMultimediaUri(22);
             ((bool[]) buf[26])[0] = rslt.wasNull(22);
             ((string[]) buf[27])[0] = rslt.getVarchar(23);
             ((bool[]) buf[28])[0] = rslt.wasNull(23);
             ((Guid[]) buf[29])[0] = rslt.getGuid(24);
             ((Guid[]) buf[30])[0] = rslt.getGuid(25);
             ((bool[]) buf[31])[0] = rslt.wasNull(25);
             ((Guid[]) buf[32])[0] = rslt.getGuid(26);
             ((bool[]) buf[33])[0] = rslt.wasNull(26);
             ((Guid[]) buf[34])[0] = rslt.getGuid(27);
             ((bool[]) buf[35])[0] = rslt.wasNull(27);
             ((Guid[]) buf[36])[0] = rslt.getGuid(28);
             ((bool[]) buf[37])[0] = rslt.wasNull(28);
             ((string[]) buf[38])[0] = rslt.getMultimediaFile(29, rslt.getVarchar(5));
             ((string[]) buf[39])[0] = rslt.getMultimediaFile(30, rslt.getVarchar(22));
             ((bool[]) buf[40])[0] = rslt.wasNull(30);
             return;
          case 25 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             return;
    }
 }

}

}
