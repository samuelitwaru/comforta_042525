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
   public class trn_appversion : GXDataArea
   {
      protected void INITENV( )
      {
         if ( GxWebError != 0 )
         {
            return  ;
         }
      }

      protected void INITTRN( )
      {
         initialize_properties( ) ;
         entryPointCalled = false;
         gxfirstwebparm = GetFirstPar( "Mode");
         gxfirstwebparm_bkp = gxfirstwebparm;
         gxfirstwebparm = DecryptAjaxCall( gxfirstwebparm);
         toggleJsOutput = isJsOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         if ( StringUtil.StrCmp(gxfirstwebparm, "dyncall") == 0 )
         {
            setAjaxCallMode();
            if ( ! IsValidAjaxCall( true) )
            {
               GxWebError = 1;
               return  ;
            }
            dyncall( GetNextPar( )) ;
            return  ;
         }
         else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxExecAct_"+"gxLoad_21") == 0 )
         {
            A29LocationId = StringUtil.StrToGuid( GetPar( "LocationId"));
            n29LocationId = false;
            AssignAttri("", false, "A29LocationId", A29LocationId.ToString());
            A11OrganisationId = StringUtil.StrToGuid( GetPar( "OrganisationId"));
            n11OrganisationId = false;
            AssignAttri("", false, "A11OrganisationId", A11OrganisationId.ToString());
            setAjaxCallMode();
            if ( ! IsValidAjaxCall( true) )
            {
               GxWebError = 1;
               return  ;
            }
            gxLoad_21( A29LocationId, A11OrganisationId) ;
            return  ;
         }
         else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxExecAct_"+"gxLoad_20") == 0 )
         {
            A11OrganisationId = StringUtil.StrToGuid( GetPar( "OrganisationId"));
            n11OrganisationId = false;
            AssignAttri("", false, "A11OrganisationId", A11OrganisationId.ToString());
            setAjaxCallMode();
            if ( ! IsValidAjaxCall( true) )
            {
               GxWebError = 1;
               return  ;
            }
            gxLoad_20( A11OrganisationId) ;
            return  ;
         }
         else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxEvt") == 0 )
         {
            setAjaxEventMode();
            if ( ! IsValidAjaxCall( true) )
            {
               GxWebError = 1;
               return  ;
            }
            gxfirstwebparm = GetFirstPar( "Mode");
         }
         else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
         {
            if ( ! IsValidAjaxCall( true) )
            {
               GxWebError = 1;
               return  ;
            }
            gxfirstwebparm = GetFirstPar( "Mode");
         }
         else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridlevel_page") == 0 )
         {
            gxnrGridlevel_page_newrow_invoke( ) ;
            return  ;
         }
         else
         {
            if ( ! IsValidAjaxCall( false) )
            {
               GxWebError = 1;
               return  ;
            }
            gxfirstwebparm = gxfirstwebparm_bkp;
         }
         if ( toggleJsOutput )
         {
            if ( context.isSpaRequest( ) )
            {
               enableJsOutput();
            }
         }
         GXKey = Crypto.GetSiteKey( );
         if ( ( StringUtil.StrCmp(context.GetRequestQueryString( ), "") != 0 ) && ( GxWebError == 0 ) && ! ( isAjaxCallMode( ) || isFullAjaxMode( ) ) )
         {
            GXDecQS = UriDecrypt64( context.GetRequestQueryString( ), GXKey);
            if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "trn_appversion.aspx")), "trn_appversion.aspx") == 0 ) )
            {
               SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "trn_appversion.aspx")))) ;
            }
            else
            {
               GxWebError = 1;
               context.HttpContext.Response.StatusCode = 403;
               context.WriteHtmlText( "<title>403 Forbidden</title>") ;
               context.WriteHtmlText( "<h1>403 Forbidden</h1>") ;
               context.WriteHtmlText( "<p /><hr />") ;
               GXUtil.WriteLog("send_http_error_code " + 403.ToString());
            }
         }
         if ( ! ( isAjaxCallMode( ) || isFullAjaxMode( ) ) )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetFirstPar( "Mode");
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            if ( ! entryPointCalled && ! ( isAjaxCallMode( ) || isFullAjaxMode( ) ) )
            {
               Gx_mode = gxfirstwebparm;
               AssignAttri("", false, "Gx_mode", Gx_mode);
               if ( StringUtil.StrCmp(gxfirstwebparm, "viewer") != 0 )
               {
                  AV8AppVersionId = StringUtil.StrToGuid( GetPar( "AppVersionId"));
                  AssignAttri("", false, "AV8AppVersionId", AV8AppVersionId.ToString());
                  GxWebStd.gx_hidden_field( context, "gxhash_vAPPVERSIONID", GetSecureSignedToken( "", AV8AppVersionId, context));
               }
            }
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
         }
         toggleJsOutput = isJsOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_web_controls( ) ;
         if ( toggleJsOutput )
         {
            if ( context.isSpaRequest( ) )
            {
               enableJsOutput();
            }
         }
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_10-184260", 0) ;
            }
         }
         Form.Meta.addItem("description", context.GetMessage( "Trn_App Version", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         if ( ! context.isAjaxRequest( ) )
         {
            GX_FocusControl = edtAppVersionId_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         wbErr = false;
         context.SetDefaultTheme("WorkWithPlusDS", true);
         if ( ! context.IsLocalStorageSupported( ) )
         {
            context.PushCurrentUrl();
         }
      }

      protected void gxnrGridlevel_page_newrow_invoke( )
      {
         nRC_GXsfl_59 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_59"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_59_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_59_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_59_idx = GetPar( "sGXsfl_59_idx");
         Gx_BScreen = (short)(Math.Round(NumberUtil.Val( GetPar( "Gx_BScreen"), "."), 18, MidpointRounding.ToEven));
         Gx_mode = GetPar( "Mode");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridlevel_page_newrow( ) ;
         /* End function gxnrGridlevel_page_newrow_invoke */
      }

      public trn_appversion( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public trn_appversion( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_Gx_mode ,
                           Guid aP1_AppVersionId )
      {
         this.Gx_mode = aP0_Gx_mode;
         this.AV8AppVersionId = aP1_AppVersionId;
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      protected override void createObjects( )
      {
         chkIsActive = new GXCheckbox();
         chkIsPredefined = new GXCheckbox();
         cmbPageType = new GXCombobox();
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
            return "trn_appversion_Execute" ;
         }

      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITENV( ) ;
         INITTRN( ) ;
         if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
         {
            MasterPageObj = (GXMasterPage) ClassLoader.GetInstance("wwpbaseobjects.workwithplusmasterpage", "GeneXus.Programs.wwpbaseobjects.workwithplusmasterpage", new Object[] {context});
            MasterPageObj.setDataArea(this,false);
            ValidateSpaRequest();
            MasterPageObj.webExecute();
            if ( ( GxWebError == 0 ) && context.isAjaxRequest( ) )
            {
               enableOutput();
               if ( ! context.isAjaxRequest( ) )
               {
                  context.GX_webresponse.AppendHeader("Cache-Control", "no-store");
               }
               if ( ! context.WillRedirect( ) )
               {
                  AddString( context.getJSONResponse( )) ;
               }
               else
               {
                  if ( context.isAjaxRequest( ) )
                  {
                     disableOutput();
                  }
                  RenderHtmlHeaders( ) ;
                  context.Redirect( context.wjLoc );
                  context.DispatchAjaxCommands();
               }
            }
         }
         cleanup();
      }

      protected void fix_multi_value_controls( )
      {
         A535IsActive = StringUtil.StrToBool( StringUtil.BoolToStr( A535IsActive));
         AssignAttri("", false, "A535IsActive", A535IsActive);
      }

      protected void Draw( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! GxWebStd.gx_redirect( context) )
         {
            disable_std_buttons( ) ;
            enableDisable( ) ;
            set_caption( ) ;
            /* Form start */
            DrawControls( ) ;
            fix_multi_value_controls( ) ;
         }
         /* Execute Exit event if defined. */
      }

      protected void DrawControls( )
      {
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"bootstrapv3\""+" "+"data-abstract-form"+" ", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divLayoutmaintable_Internalname, 1, 0, "px", 0, "px", divLayoutmaintable_Class, "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divTablemain_Internalname, 1, 0, "px", 0, "px", "TableMainTransaction", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
         ClassString = "ErrorViewer";
         StyleString = "";
         GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, StyleString, ClassString, "", "false");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
         /* Control Group */
         GxWebStd.gx_group_start( context, grpUnnamedgroup1_Internalname, context.GetMessage( "WWP_TemplateDataPanelTitle", ""), 1, 0, "px", 0, "px", "Group", "", "HLP_Trn_AppVersion.htm");
         /* Div Control */
         GxWebStd.gx_div_start( context, divTablecontent_Internalname, 1, 0, "px", 0, "px", "CellMarginTop10", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divTableattributes_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtAppVersionId_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtAppVersionId_Internalname, context.GetMessage( "Version Id", ""), "col-sm-4 AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-8 gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 21,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtAppVersionId_Internalname, A523AppVersionId.ToString(), A523AppVersionId.ToString(), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,21);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtAppVersionId_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtAppVersionId_Enabled, 1, "text", "", 36, "chr", 1, "row", 36, 0, 0, 0, 0, 0, 0, true, "Id", "", false, "", "HLP_Trn_AppVersion.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtAppVersionName_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtAppVersionName_Internalname, context.GetMessage( "Version Name", ""), "col-sm-4 AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-8 gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 26,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtAppVersionName_Internalname, A524AppVersionName, StringUtil.RTrim( context.localUtil.Format( A524AppVersionName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,26);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtAppVersionName_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtAppVersionName_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, -1, -1, true, "Name", "start", true, "", "HLP_Trn_AppVersion.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell ExtendedComboCell", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divTablesplittedlocationid_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4 MergeLabelCell", "start", "top", "", "", "div");
         /* Text block */
         GxWebStd.gx_label_ctrl( context, lblTextblocklocationid_Internalname, context.GetMessage( "Location", ""), "", "", lblTextblocklocationid_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "Label", 0, "", 1, 1, 0, 0, "HLP_Trn_AppVersion.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-8", "start", "top", "", "", "div");
         /* User Defined Control */
         ucCombo_locationid.SetProperty("Caption", Combo_locationid_Caption);
         ucCombo_locationid.SetProperty("Cls", Combo_locationid_Cls);
         ucCombo_locationid.SetProperty("DataListProc", Combo_locationid_Datalistproc);
         ucCombo_locationid.SetProperty("DropDownOptionsTitleSettingsIcons", AV18DDO_TitleSettingsIcons);
         ucCombo_locationid.SetProperty("DropDownOptionsData", AV17LocationId_Data);
         ucCombo_locationid.Render(context, "dvelop.gxbootstrap.ddoextendedcombo", Combo_locationid_Internalname, "COMBO_LOCATIONIDContainer");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 Invisible", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtLocationId_Internalname, context.GetMessage( "Location Id", ""), "col-sm-3 AttributeLabel", 0, true, "");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 37,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtLocationId_Internalname, A29LocationId.ToString(), A29LocationId.ToString(), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,37);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtLocationId_Jsonclick, 0, "Attribute", "", "", "", "", edtLocationId_Visible, edtLocationId_Enabled, 1, "text", "", 36, "chr", 1, "row", 36, 0, 0, 0, 0, 0, 0, true, "Id", "", false, "", "HLP_Trn_AppVersion.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell ExtendedComboCell", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divTablesplittedorganisationid_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4 MergeLabelCell", "start", "top", "", "", "div");
         /* Text block */
         GxWebStd.gx_label_ctrl( context, lblTextblockorganisationid_Internalname, context.GetMessage( "Organisations", ""), "", "", lblTextblockorganisationid_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "Label", 0, "", 1, 1, 0, 0, "HLP_Trn_AppVersion.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-8", "start", "top", "", "", "div");
         /* User Defined Control */
         ucCombo_organisationid.SetProperty("Caption", Combo_organisationid_Caption);
         ucCombo_organisationid.SetProperty("Cls", Combo_organisationid_Cls);
         ucCombo_organisationid.SetProperty("DataListProc", Combo_organisationid_Datalistproc);
         ucCombo_organisationid.SetProperty("DataListProcParametersPrefix", Combo_organisationid_Datalistprocparametersprefix);
         ucCombo_organisationid.SetProperty("DropDownOptionsTitleSettingsIcons", AV18DDO_TitleSettingsIcons);
         ucCombo_organisationid.SetProperty("DropDownOptionsData", AV26OrganisationId_Data);
         ucCombo_organisationid.Render(context, "dvelop.gxbootstrap.ddoextendedcombo", Combo_organisationid_Internalname, "COMBO_ORGANISATIONIDContainer");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 Invisible", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtOrganisationId_Internalname, context.GetMessage( "Organisation Id", ""), "col-sm-3 AttributeLabel", 0, true, "");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 48,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtOrganisationId_Internalname, A11OrganisationId.ToString(), A11OrganisationId.ToString(), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,48);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtOrganisationId_Jsonclick, 0, "Attribute", "", "", "", "", edtOrganisationId_Visible, edtOrganisationId_Enabled, 1, "text", "", 36, "chr", 1, "row", 36, 0, 0, 0, 0, 0, 0, true, "Id", "", false, "", "HLP_Trn_AppVersion.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkIsActive_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, chkIsActive_Internalname, context.GetMessage( "Active", ""), "col-sm-4 AttributeCheckBoxLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-8 gx-attribute", "start", "top", "", "", "div");
         /* Check box */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 53,'',false,'',0)\"";
         ClassString = "AttributeCheckBox";
         StyleString = "";
         GxWebStd.gx_checkbox_ctrl( context, chkIsActive_Internalname, StringUtil.BoolToStr( A535IsActive), "", context.GetMessage( "Active", ""), 1, chkIsActive.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(53, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,53);\"");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         context.WriteHtmlText( "</fieldset>") ;
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-lg-9 CellMarginTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divTableleaflevel_page_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 SectionGrid GridNoBorderCell GridFixedColumnBorders", "start", "top", "", "", "div");
         gxdraw_Gridlevel_page( ) ;
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group CellMarginTop10", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 70,'',false,'',0)\"";
         ClassString = "ButtonMaterial";
         StyleString = "";
         GxWebStd.gx_button_ctrl( context, bttBtntrn_enter_Internalname, "", context.GetMessage( "GX_BtnEnter", ""), bttBtntrn_enter_Jsonclick, 5, context.GetMessage( "GX_BtnEnter", ""), "", StyleString, ClassString, bttBtntrn_enter_Visible, bttBtntrn_enter_Enabled, "standard", "'"+""+"'"+",false,"+"'"+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_Trn_AppVersion.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 72,'',false,'',0)\"";
         ClassString = "ButtonMaterialDefault";
         StyleString = "";
         GxWebStd.gx_button_ctrl( context, bttBtntrn_cancel_Internalname, "", context.GetMessage( "GX_BtnCancel", ""), bttBtntrn_cancel_Jsonclick, 1, context.GetMessage( "GX_BtnCancel", ""), "", StyleString, ClassString, bttBtntrn_cancel_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"ECANCEL."+"'", TempTags, "", context.GetButtonType( ), "HLP_Trn_AppVersion.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 74,'',false,'',0)\"";
         ClassString = "ButtonMaterialDefault";
         StyleString = "";
         GxWebStd.gx_button_ctrl( context, bttBtntrn_delete_Internalname, "", context.GetMessage( "GX_BtnDelete", ""), bttBtntrn_delete_Jsonclick, 5, context.GetMessage( "GX_BtnDelete", ""), "", StyleString, ClassString, bttBtntrn_delete_Visible, bttBtntrn_delete_Enabled, "standard", "'"+""+"'"+",false,"+"'"+"EDELETE."+"'", TempTags, "", context.GetButtonType( ), "HLP_Trn_AppVersion.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divHtml_bottomauxiliarcontrols_Internalname, 1, 0, "px", 0, "px", "Section", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divSectionattribute_locationid_Internalname, 1, 0, "px", 0, "px", "Section", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 79,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtavCombolocationid_Internalname, AV22ComboLocationId.ToString(), AV22ComboLocationId.ToString(), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,79);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCombolocationid_Jsonclick, 0, "Attribute", "", "", "", "", edtavCombolocationid_Visible, edtavCombolocationid_Enabled, 0, "text", "", 36, "chr", 1, "row", 36, 0, 0, 0, 0, 0, 0, true, "", "", false, "", "HLP_Trn_AppVersion.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divSectionattribute_organisationid_Internalname, 1, 0, "px", 0, "px", "Section", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 81,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtavComboorganisationid_Internalname, AV27ComboOrganisationId.ToString(), AV27ComboOrganisationId.ToString(), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,81);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavComboorganisationid_Jsonclick, 0, "Attribute", "", "", "", "", edtavComboorganisationid_Visible, edtavComboorganisationid_Enabled, 0, "text", "", 36, "chr", 1, "row", 36, 0, 0, 0, 0, 0, 0, true, "", "", false, "", "HLP_Trn_AppVersion.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
      }

      protected void gxdraw_Gridlevel_page( )
      {
         /*  Grid Control  */
         StartGridControl59( ) ;
         nGXsfl_59_idx = 0;
         if ( ( nKeyPressed == 1 ) && ( AnyError == 0 ) )
         {
            /* Enter key processing. */
            nBlankRcdCount95 = 5;
            if ( ! IsIns( ) )
            {
               /* Display confirmed (stored) records */
               nRcdExists_95 = 1;
               ScanStart1L95( ) ;
               while ( RcdFound95 != 0 )
               {
                  init_level_properties95( ) ;
                  getByPrimaryKey1L95( ) ;
                  AddRow1L95( ) ;
                  ScanNext1L95( ) ;
               }
               ScanEnd1L95( ) ;
               nBlankRcdCount95 = 5;
            }
         }
         else if ( ( nKeyPressed == 3 ) || ( nKeyPressed == 4 ) || ( ( nKeyPressed == 1 ) && ( AnyError != 0 ) ) )
         {
            /* Button check  or addlines. */
            standaloneNotModal1L95( ) ;
            standaloneModal1L95( ) ;
            sMode95 = Gx_mode;
            while ( nGXsfl_59_idx < nRC_GXsfl_59 )
            {
               bGXsfl_59_Refreshing = true;
               ReadRow1L95( ) ;
               edtPageId_Enabled = (int)(Math.Round(context.localUtil.CToN( cgiGet( "PAGEID_"+sGXsfl_59_idx+"Enabled"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignProp("", false, edtPageId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtPageId_Enabled), 5, 0), !bGXsfl_59_Refreshing);
               edtPageName_Enabled = (int)(Math.Round(context.localUtil.CToN( cgiGet( "PAGENAME_"+sGXsfl_59_idx+"Enabled"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignProp("", false, edtPageName_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtPageName_Enabled), 5, 0), !bGXsfl_59_Refreshing);
               edtPageStructure_Enabled = (int)(Math.Round(context.localUtil.CToN( cgiGet( "PAGESTRUCTURE_"+sGXsfl_59_idx+"Enabled"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignProp("", false, edtPageStructure_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtPageStructure_Enabled), 5, 0), !bGXsfl_59_Refreshing);
               edtPagePublishedStructure_Enabled = (int)(Math.Round(context.localUtil.CToN( cgiGet( "PAGEPUBLISHEDSTRUCTURE_"+sGXsfl_59_idx+"Enabled"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignProp("", false, edtPagePublishedStructure_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtPagePublishedStructure_Enabled), 5, 0), !bGXsfl_59_Refreshing);
               chkIsPredefined.Enabled = (int)(Math.Round(context.localUtil.CToN( cgiGet( "ISPREDEFINED_"+sGXsfl_59_idx+"Enabled"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignProp("", false, chkIsPredefined_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkIsPredefined.Enabled), 5, 0), !bGXsfl_59_Refreshing);
               cmbPageType.Enabled = (int)(Math.Round(context.localUtil.CToN( cgiGet( "PAGETYPE_"+sGXsfl_59_idx+"Enabled"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignProp("", false, cmbPageType_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbPageType.Enabled), 5, 0), !bGXsfl_59_Refreshing);
               if ( ( nRcdExists_95 == 0 ) && ! IsIns( ) )
               {
                  Gx_mode = "INS";
                  AssignAttri("", false, "Gx_mode", Gx_mode);
                  standaloneModal1L95( ) ;
               }
               SendRow1L95( ) ;
               bGXsfl_59_Refreshing = false;
            }
            Gx_mode = sMode95;
            AssignAttri("", false, "Gx_mode", Gx_mode);
         }
         else
         {
            /* Get or get-alike key processing. */
            nBlankRcdCount95 = 5;
            nRcdExists_95 = 1;
            if ( ! IsIns( ) )
            {
               ScanStart1L95( ) ;
               while ( RcdFound95 != 0 )
               {
                  sGXsfl_59_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_59_idx+1), 4, 0), 4, "0");
                  SubsflControlProps_5995( ) ;
                  init_level_properties95( ) ;
                  standaloneNotModal1L95( ) ;
                  getByPrimaryKey1L95( ) ;
                  standaloneModal1L95( ) ;
                  AddRow1L95( ) ;
                  ScanNext1L95( ) ;
               }
               ScanEnd1L95( ) ;
            }
         }
         /* Initialize fields for 'new' records and send them. */
         if ( ! IsDsp( ) && ! IsDlt( ) )
         {
            sMode95 = Gx_mode;
            Gx_mode = "INS";
            AssignAttri("", false, "Gx_mode", Gx_mode);
            sGXsfl_59_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_59_idx+1), 4, 0), 4, "0");
            SubsflControlProps_5995( ) ;
            InitAll1L95( ) ;
            init_level_properties95( ) ;
            nRcdExists_95 = 0;
            nIsMod_95 = 0;
            nRcdDeleted_95 = 0;
            nBlankRcdCount95 = (short)(nBlankRcdUsr95+nBlankRcdCount95);
            fRowAdded = 0;
            while ( nBlankRcdCount95 > 0 )
            {
               A516PageId = Guid.Empty;
               standaloneNotModal1L95( ) ;
               standaloneModal1L95( ) ;
               AddRow1L95( ) ;
               if ( ( nKeyPressed == 4 ) && ( fRowAdded == 0 ) )
               {
                  fRowAdded = 1;
                  GX_FocusControl = edtPageId_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               }
               nBlankRcdCount95 = (short)(nBlankRcdCount95-1);
            }
            Gx_mode = sMode95;
            AssignAttri("", false, "Gx_mode", Gx_mode);
         }
         sStyleString = "";
         context.WriteHtmlText( "<div id=\""+"Gridlevel_pageContainer"+"Div\" "+sStyleString+">"+"</div>") ;
         context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridlevel_page", Gridlevel_pageContainer, subGridlevel_page_Internalname);
         if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
         {
            GxWebStd.gx_hidden_field( context, "Gridlevel_pageContainerData", Gridlevel_pageContainer.ToJavascriptSource());
         }
         if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
         {
            GxWebStd.gx_hidden_field( context, "Gridlevel_pageContainerData"+"V", Gridlevel_pageContainer.GridValuesHidden());
         }
         else
         {
            context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"Gridlevel_pageContainerData"+"V"+"\" value='"+Gridlevel_pageContainer.GridValuesHidden()+"'/>") ;
         }
      }

      protected void UserMain( )
      {
         standaloneStartup( ) ;
      }

      protected void UserMainFullajax( )
      {
         INITENV( ) ;
         INITTRN( ) ;
         UserMain( ) ;
         Draw( ) ;
         SendCloseFormHiddens( ) ;
      }

      protected void standaloneStartup( )
      {
         standaloneStartupServer( ) ;
         disable_std_buttons( ) ;
         enableDisable( ) ;
         Process( ) ;
      }

      protected void standaloneStartupServer( )
      {
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E111L2 ();
         context.wbGlbDoneStart = 1;
         assign_properties_default( ) ;
         if ( AnyError == 0 )
         {
            if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
            {
               /* Read saved SDTs. */
               ajax_req_read_hidden_sdt(cgiGet( "vDDO_TITLESETTINGSICONS"), AV18DDO_TitleSettingsIcons);
               ajax_req_read_hidden_sdt(cgiGet( "vLOCATIONID_DATA"), AV17LocationId_Data);
               ajax_req_read_hidden_sdt(cgiGet( "vORGANISATIONID_DATA"), AV26OrganisationId_Data);
               /* Read saved values. */
               Z523AppVersionId = StringUtil.StrToGuid( cgiGet( "Z523AppVersionId"));
               Z524AppVersionName = cgiGet( "Z524AppVersionName");
               Z535IsActive = StringUtil.StrToBool( cgiGet( "Z535IsActive"));
               Z29LocationId = StringUtil.StrToGuid( cgiGet( "Z29LocationId"));
               n29LocationId = ((Guid.Empty==A29LocationId) ? true : false);
               Z11OrganisationId = StringUtil.StrToGuid( cgiGet( "Z11OrganisationId"));
               n11OrganisationId = ((Guid.Empty==A11OrganisationId) ? true : false);
               IsConfirmed = (short)(Math.Round(context.localUtil.CToN( cgiGet( "IsConfirmed"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               IsModified = (short)(Math.Round(context.localUtil.CToN( cgiGet( "IsModified"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Gx_mode = cgiGet( "Mode");
               nRC_GXsfl_59 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_59"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               N29LocationId = StringUtil.StrToGuid( cgiGet( "N29LocationId"));
               n29LocationId = ((Guid.Empty==A29LocationId) ? true : false);
               N11OrganisationId = StringUtil.StrToGuid( cgiGet( "N11OrganisationId"));
               n11OrganisationId = ((Guid.Empty==A11OrganisationId) ? true : false);
               AV8AppVersionId = StringUtil.StrToGuid( cgiGet( "vAPPVERSIONID"));
               Gx_BScreen = (short)(Math.Round(context.localUtil.CToN( cgiGet( "vGXBSCREEN"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AV14Insert_LocationId = StringUtil.StrToGuid( cgiGet( "vINSERT_LOCATIONID"));
               AV15Insert_OrganisationId = StringUtil.StrToGuid( cgiGet( "vINSERT_ORGANISATIONID"));
               AV30Pgmname = cgiGet( "vPGMNAME");
               A599PageThumbnail = cgiGet( "PAGETHUMBNAIL");
               n599PageThumbnail = false;
               n599PageThumbnail = (String.IsNullOrEmpty(StringUtil.RTrim( A599PageThumbnail)) ? true : false);
               A40000PageThumbnail_GXI = cgiGet( "PAGETHUMBNAIL_GXI");
               n40000PageThumbnail_GXI = false;
               n40000PageThumbnail_GXI = (String.IsNullOrEmpty(StringUtil.RTrim( A40000PageThumbnail_GXI))&&String.IsNullOrEmpty(StringUtil.RTrim( A599PageThumbnail)) ? true : false);
               Combo_locationid_Objectcall = cgiGet( "COMBO_LOCATIONID_Objectcall");
               Combo_locationid_Class = cgiGet( "COMBO_LOCATIONID_Class");
               Combo_locationid_Icontype = cgiGet( "COMBO_LOCATIONID_Icontype");
               Combo_locationid_Icon = cgiGet( "COMBO_LOCATIONID_Icon");
               Combo_locationid_Caption = cgiGet( "COMBO_LOCATIONID_Caption");
               Combo_locationid_Tooltip = cgiGet( "COMBO_LOCATIONID_Tooltip");
               Combo_locationid_Cls = cgiGet( "COMBO_LOCATIONID_Cls");
               Combo_locationid_Selectedvalue_set = cgiGet( "COMBO_LOCATIONID_Selectedvalue_set");
               Combo_locationid_Selectedvalue_get = cgiGet( "COMBO_LOCATIONID_Selectedvalue_get");
               Combo_locationid_Selectedtext_set = cgiGet( "COMBO_LOCATIONID_Selectedtext_set");
               Combo_locationid_Selectedtext_get = cgiGet( "COMBO_LOCATIONID_Selectedtext_get");
               Combo_locationid_Gamoauthtoken = cgiGet( "COMBO_LOCATIONID_Gamoauthtoken");
               Combo_locationid_Ddointernalname = cgiGet( "COMBO_LOCATIONID_Ddointernalname");
               Combo_locationid_Titlecontrolalign = cgiGet( "COMBO_LOCATIONID_Titlecontrolalign");
               Combo_locationid_Dropdownoptionstype = cgiGet( "COMBO_LOCATIONID_Dropdownoptionstype");
               Combo_locationid_Enabled = StringUtil.StrToBool( cgiGet( "COMBO_LOCATIONID_Enabled"));
               Combo_locationid_Visible = StringUtil.StrToBool( cgiGet( "COMBO_LOCATIONID_Visible"));
               Combo_locationid_Titlecontrolidtoreplace = cgiGet( "COMBO_LOCATIONID_Titlecontrolidtoreplace");
               Combo_locationid_Datalisttype = cgiGet( "COMBO_LOCATIONID_Datalisttype");
               Combo_locationid_Allowmultipleselection = StringUtil.StrToBool( cgiGet( "COMBO_LOCATIONID_Allowmultipleselection"));
               Combo_locationid_Datalistfixedvalues = cgiGet( "COMBO_LOCATIONID_Datalistfixedvalues");
               Combo_locationid_Isgriditem = StringUtil.StrToBool( cgiGet( "COMBO_LOCATIONID_Isgriditem"));
               Combo_locationid_Hasdescription = StringUtil.StrToBool( cgiGet( "COMBO_LOCATIONID_Hasdescription"));
               Combo_locationid_Datalistproc = cgiGet( "COMBO_LOCATIONID_Datalistproc");
               Combo_locationid_Datalistprocparametersprefix = cgiGet( "COMBO_LOCATIONID_Datalistprocparametersprefix");
               Combo_locationid_Remoteservicesparameters = cgiGet( "COMBO_LOCATIONID_Remoteservicesparameters");
               Combo_locationid_Datalistupdateminimumcharacters = (int)(Math.Round(context.localUtil.CToN( cgiGet( "COMBO_LOCATIONID_Datalistupdateminimumcharacters"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Combo_locationid_Includeonlyselectedoption = StringUtil.StrToBool( cgiGet( "COMBO_LOCATIONID_Includeonlyselectedoption"));
               Combo_locationid_Includeselectalloption = StringUtil.StrToBool( cgiGet( "COMBO_LOCATIONID_Includeselectalloption"));
               Combo_locationid_Emptyitem = StringUtil.StrToBool( cgiGet( "COMBO_LOCATIONID_Emptyitem"));
               Combo_locationid_Includeaddnewoption = StringUtil.StrToBool( cgiGet( "COMBO_LOCATIONID_Includeaddnewoption"));
               Combo_locationid_Htmltemplate = cgiGet( "COMBO_LOCATIONID_Htmltemplate");
               Combo_locationid_Multiplevaluestype = cgiGet( "COMBO_LOCATIONID_Multiplevaluestype");
               Combo_locationid_Loadingdata = cgiGet( "COMBO_LOCATIONID_Loadingdata");
               Combo_locationid_Noresultsfound = cgiGet( "COMBO_LOCATIONID_Noresultsfound");
               Combo_locationid_Emptyitemtext = cgiGet( "COMBO_LOCATIONID_Emptyitemtext");
               Combo_locationid_Onlyselectedvalues = cgiGet( "COMBO_LOCATIONID_Onlyselectedvalues");
               Combo_locationid_Selectalltext = cgiGet( "COMBO_LOCATIONID_Selectalltext");
               Combo_locationid_Multiplevaluesseparator = cgiGet( "COMBO_LOCATIONID_Multiplevaluesseparator");
               Combo_locationid_Addnewoptiontext = cgiGet( "COMBO_LOCATIONID_Addnewoptiontext");
               Combo_locationid_Gxcontroltype = (int)(Math.Round(context.localUtil.CToN( cgiGet( "COMBO_LOCATIONID_Gxcontroltype"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Combo_organisationid_Objectcall = cgiGet( "COMBO_ORGANISATIONID_Objectcall");
               Combo_organisationid_Class = cgiGet( "COMBO_ORGANISATIONID_Class");
               Combo_organisationid_Icontype = cgiGet( "COMBO_ORGANISATIONID_Icontype");
               Combo_organisationid_Icon = cgiGet( "COMBO_ORGANISATIONID_Icon");
               Combo_organisationid_Caption = cgiGet( "COMBO_ORGANISATIONID_Caption");
               Combo_organisationid_Tooltip = cgiGet( "COMBO_ORGANISATIONID_Tooltip");
               Combo_organisationid_Cls = cgiGet( "COMBO_ORGANISATIONID_Cls");
               Combo_organisationid_Selectedvalue_set = cgiGet( "COMBO_ORGANISATIONID_Selectedvalue_set");
               Combo_organisationid_Selectedvalue_get = cgiGet( "COMBO_ORGANISATIONID_Selectedvalue_get");
               Combo_organisationid_Selectedtext_set = cgiGet( "COMBO_ORGANISATIONID_Selectedtext_set");
               Combo_organisationid_Selectedtext_get = cgiGet( "COMBO_ORGANISATIONID_Selectedtext_get");
               Combo_organisationid_Gamoauthtoken = cgiGet( "COMBO_ORGANISATIONID_Gamoauthtoken");
               Combo_organisationid_Ddointernalname = cgiGet( "COMBO_ORGANISATIONID_Ddointernalname");
               Combo_organisationid_Titlecontrolalign = cgiGet( "COMBO_ORGANISATIONID_Titlecontrolalign");
               Combo_organisationid_Dropdownoptionstype = cgiGet( "COMBO_ORGANISATIONID_Dropdownoptionstype");
               Combo_organisationid_Enabled = StringUtil.StrToBool( cgiGet( "COMBO_ORGANISATIONID_Enabled"));
               Combo_organisationid_Visible = StringUtil.StrToBool( cgiGet( "COMBO_ORGANISATIONID_Visible"));
               Combo_organisationid_Titlecontrolidtoreplace = cgiGet( "COMBO_ORGANISATIONID_Titlecontrolidtoreplace");
               Combo_organisationid_Datalisttype = cgiGet( "COMBO_ORGANISATIONID_Datalisttype");
               Combo_organisationid_Allowmultipleselection = StringUtil.StrToBool( cgiGet( "COMBO_ORGANISATIONID_Allowmultipleselection"));
               Combo_organisationid_Datalistfixedvalues = cgiGet( "COMBO_ORGANISATIONID_Datalistfixedvalues");
               Combo_organisationid_Isgriditem = StringUtil.StrToBool( cgiGet( "COMBO_ORGANISATIONID_Isgriditem"));
               Combo_organisationid_Hasdescription = StringUtil.StrToBool( cgiGet( "COMBO_ORGANISATIONID_Hasdescription"));
               Combo_organisationid_Datalistproc = cgiGet( "COMBO_ORGANISATIONID_Datalistproc");
               Combo_organisationid_Datalistprocparametersprefix = cgiGet( "COMBO_ORGANISATIONID_Datalistprocparametersprefix");
               Combo_organisationid_Remoteservicesparameters = cgiGet( "COMBO_ORGANISATIONID_Remoteservicesparameters");
               Combo_organisationid_Datalistupdateminimumcharacters = (int)(Math.Round(context.localUtil.CToN( cgiGet( "COMBO_ORGANISATIONID_Datalistupdateminimumcharacters"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Combo_organisationid_Includeonlyselectedoption = StringUtil.StrToBool( cgiGet( "COMBO_ORGANISATIONID_Includeonlyselectedoption"));
               Combo_organisationid_Includeselectalloption = StringUtil.StrToBool( cgiGet( "COMBO_ORGANISATIONID_Includeselectalloption"));
               Combo_organisationid_Emptyitem = StringUtil.StrToBool( cgiGet( "COMBO_ORGANISATIONID_Emptyitem"));
               Combo_organisationid_Includeaddnewoption = StringUtil.StrToBool( cgiGet( "COMBO_ORGANISATIONID_Includeaddnewoption"));
               Combo_organisationid_Htmltemplate = cgiGet( "COMBO_ORGANISATIONID_Htmltemplate");
               Combo_organisationid_Multiplevaluestype = cgiGet( "COMBO_ORGANISATIONID_Multiplevaluestype");
               Combo_organisationid_Loadingdata = cgiGet( "COMBO_ORGANISATIONID_Loadingdata");
               Combo_organisationid_Noresultsfound = cgiGet( "COMBO_ORGANISATIONID_Noresultsfound");
               Combo_organisationid_Emptyitemtext = cgiGet( "COMBO_ORGANISATIONID_Emptyitemtext");
               Combo_organisationid_Onlyselectedvalues = cgiGet( "COMBO_ORGANISATIONID_Onlyselectedvalues");
               Combo_organisationid_Selectalltext = cgiGet( "COMBO_ORGANISATIONID_Selectalltext");
               Combo_organisationid_Multiplevaluesseparator = cgiGet( "COMBO_ORGANISATIONID_Multiplevaluesseparator");
               Combo_organisationid_Addnewoptiontext = cgiGet( "COMBO_ORGANISATIONID_Addnewoptiontext");
               Combo_organisationid_Gxcontroltype = (int)(Math.Round(context.localUtil.CToN( cgiGet( "COMBO_ORGANISATIONID_Gxcontroltype"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               /* Read variables values. */
               if ( StringUtil.StrCmp(cgiGet( edtAppVersionId_Internalname), "") == 0 )
               {
                  A523AppVersionId = Guid.Empty;
                  AssignAttri("", false, "A523AppVersionId", A523AppVersionId.ToString());
               }
               else
               {
                  try
                  {
                     A523AppVersionId = StringUtil.StrToGuid( cgiGet( edtAppVersionId_Internalname));
                     AssignAttri("", false, "A523AppVersionId", A523AppVersionId.ToString());
                  }
                  catch ( Exception  )
                  {
                     GX_msglist.addItem(context.GetMessage( "GXM_invalidguid", ""), 1, "APPVERSIONID");
                     AnyError = 1;
                     GX_FocusControl = edtAppVersionId_Internalname;
                     AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                     wbErr = true;
                  }
               }
               A524AppVersionName = cgiGet( edtAppVersionName_Internalname);
               AssignAttri("", false, "A524AppVersionName", A524AppVersionName);
               if ( StringUtil.StrCmp(cgiGet( edtLocationId_Internalname), "") == 0 )
               {
                  A29LocationId = Guid.Empty;
                  n29LocationId = false;
                  AssignAttri("", false, "A29LocationId", A29LocationId.ToString());
               }
               else
               {
                  try
                  {
                     A29LocationId = StringUtil.StrToGuid( cgiGet( edtLocationId_Internalname));
                     n29LocationId = false;
                     AssignAttri("", false, "A29LocationId", A29LocationId.ToString());
                  }
                  catch ( Exception  )
                  {
                     GX_msglist.addItem(context.GetMessage( "GXM_invalidguid", ""), 1, "LOCATIONID");
                     AnyError = 1;
                     GX_FocusControl = edtLocationId_Internalname;
                     AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                     wbErr = true;
                  }
               }
               n29LocationId = ((Guid.Empty==A29LocationId) ? true : false);
               if ( StringUtil.StrCmp(cgiGet( edtOrganisationId_Internalname), "") == 0 )
               {
                  A11OrganisationId = Guid.Empty;
                  n11OrganisationId = false;
                  AssignAttri("", false, "A11OrganisationId", A11OrganisationId.ToString());
               }
               else
               {
                  try
                  {
                     A11OrganisationId = StringUtil.StrToGuid( cgiGet( edtOrganisationId_Internalname));
                     n11OrganisationId = false;
                     AssignAttri("", false, "A11OrganisationId", A11OrganisationId.ToString());
                  }
                  catch ( Exception  )
                  {
                     GX_msglist.addItem(context.GetMessage( "GXM_invalidguid", ""), 1, "ORGANISATIONID");
                     AnyError = 1;
                     GX_FocusControl = edtOrganisationId_Internalname;
                     AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                     wbErr = true;
                  }
               }
               n11OrganisationId = ((Guid.Empty==A11OrganisationId) ? true : false);
               A535IsActive = StringUtil.StrToBool( cgiGet( chkIsActive_Internalname));
               AssignAttri("", false, "A535IsActive", A535IsActive);
               AV22ComboLocationId = StringUtil.StrToGuid( cgiGet( edtavCombolocationid_Internalname));
               AssignAttri("", false, "AV22ComboLocationId", AV22ComboLocationId.ToString());
               AV27ComboOrganisationId = StringUtil.StrToGuid( cgiGet( edtavComboorganisationid_Internalname));
               AssignAttri("", false, "AV27ComboOrganisationId", AV27ComboOrganisationId.ToString());
               /* Read subfile selected row values. */
               /* Read hidden variables. */
               GXKey = Crypto.GetSiteKey( );
               forbiddenHiddens = new GXProperties();
               forbiddenHiddens.Add("hshsalt", "hsh"+"Trn_AppVersion");
               forbiddenHiddens.Add("Gx_mode", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")));
               hsh = cgiGet( "hsh");
               if ( ( ! ( ( A523AppVersionId != Z523AppVersionId ) ) || ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) ) && ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
               {
                  GXUtil.WriteLogError("trn_appversion:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
                  GxWebError = 1;
                  context.HttpContext.Response.StatusCode = 403;
                  context.WriteHtmlText( "<title>403 Forbidden</title>") ;
                  context.WriteHtmlText( "<h1>403 Forbidden</h1>") ;
                  context.WriteHtmlText( "<p /><hr />") ;
                  GXUtil.WriteLog("send_http_error_code " + 403.ToString());
                  AnyError = 1;
                  return  ;
               }
               /* Check if conditions changed and reset current page numbers */
               standaloneNotModal( ) ;
            }
            else
            {
               standaloneNotModal( ) ;
               if ( StringUtil.StrCmp(gxfirstwebparm, "viewer") == 0 )
               {
                  Gx_mode = "DSP";
                  AssignAttri("", false, "Gx_mode", Gx_mode);
                  A523AppVersionId = StringUtil.StrToGuid( GetPar( "AppVersionId"));
                  AssignAttri("", false, "A523AppVersionId", A523AppVersionId.ToString());
                  getEqualNoModal( ) ;
                  if ( ! (Guid.Empty==AV8AppVersionId) )
                  {
                     A523AppVersionId = AV8AppVersionId;
                     AssignAttri("", false, "A523AppVersionId", A523AppVersionId.ToString());
                  }
                  else
                  {
                     if ( IsIns( )  && (Guid.Empty==A523AppVersionId) && ( Gx_BScreen == 0 ) )
                     {
                        A523AppVersionId = Guid.NewGuid( );
                        AssignAttri("", false, "A523AppVersionId", A523AppVersionId.ToString());
                     }
                  }
                  Gx_mode = "DSP";
                  AssignAttri("", false, "Gx_mode", Gx_mode);
                  disable_std_buttons( ) ;
                  standaloneModal( ) ;
               }
               else
               {
                  if ( IsDsp( ) )
                  {
                     sMode6 = Gx_mode;
                     Gx_mode = "UPD";
                     AssignAttri("", false, "Gx_mode", Gx_mode);
                     if ( ! (Guid.Empty==AV8AppVersionId) )
                     {
                        A523AppVersionId = AV8AppVersionId;
                        AssignAttri("", false, "A523AppVersionId", A523AppVersionId.ToString());
                     }
                     else
                     {
                        if ( IsIns( )  && (Guid.Empty==A523AppVersionId) && ( Gx_BScreen == 0 ) )
                        {
                           A523AppVersionId = Guid.NewGuid( );
                           AssignAttri("", false, "A523AppVersionId", A523AppVersionId.ToString());
                        }
                     }
                     Gx_mode = sMode6;
                     AssignAttri("", false, "Gx_mode", Gx_mode);
                  }
                  standaloneModal( ) ;
                  if ( ! IsIns( ) )
                  {
                     getByPrimaryKey( ) ;
                     if ( RcdFound6 == 1 )
                     {
                        if ( IsDlt( ) )
                        {
                           /* Confirm record */
                           CONFIRM_1L0( ) ;
                           if ( AnyError == 0 )
                           {
                              GX_FocusControl = bttBtntrn_enter_Internalname;
                              AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                           }
                        }
                     }
                     else
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_noinsert", ""), 1, "APPVERSIONID");
                        AnyError = 1;
                        GX_FocusControl = edtAppVersionId_Internalname;
                        AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                     }
                  }
               }
            }
         }
      }

      protected void Process( )
      {
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read Transaction buttons. */
            sEvt = cgiGet( "_EventName");
            EvtGridId = cgiGet( "_EventGridId");
            EvtRowId = cgiGet( "_EventRowId");
            if ( StringUtil.Len( sEvt) > 0 )
            {
               sEvtType = StringUtil.Left( sEvt, 1);
               sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-1));
               if ( StringUtil.StrCmp(sEvtType, "M") != 0 )
               {
                  if ( StringUtil.StrCmp(sEvtType, "E") == 0 )
                  {
                     sEvtType = StringUtil.Right( sEvt, 1);
                     if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                     {
                        sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                        if ( StringUtil.StrCmp(sEvt, "COMBO_LOCATIONID.ONOPTIONCLICKED") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: Combo_locationid.Onoptionclicked */
                           E121L2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "COMBO_ORGANISATIONID.ONOPTIONCLICKED") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: Combo_organisationid.Onoptionclicked */
                           E131L2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: Start */
                           E111L2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "AFTER TRN") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: After Trn */
                           E141L2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                        {
                           context.wbHandled = 1;
                           if ( ! IsDsp( ) )
                           {
                              btn_enter( ) ;
                           }
                           /* No code required for Cancel button. It is implemented as the Reset button. */
                        }
                     }
                     else
                     {
                        sEvtType = StringUtil.Right( sEvt, 4);
                        sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                     }
                  }
                  context.wbHandled = 1;
               }
            }
         }
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
            E141L2 ();
            trnEnded = 0;
            standaloneNotModal( ) ;
            standaloneModal( ) ;
            if ( IsIns( )  )
            {
               /* Clear variables for new insertion. */
               InitAll1L6( ) ;
               standaloneNotModal( ) ;
               standaloneModal( ) ;
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

      protected void disable_std_buttons( )
      {
         bttBtntrn_delete_Visible = 0;
         AssignProp("", false, bttBtntrn_delete_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtntrn_delete_Visible), 5, 0), true);
         if ( IsDsp( ) || IsDlt( ) )
         {
            bttBtntrn_delete_Visible = 0;
            AssignProp("", false, bttBtntrn_delete_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtntrn_delete_Visible), 5, 0), true);
            if ( IsDsp( ) )
            {
               bttBtntrn_enter_Visible = 0;
               AssignProp("", false, bttBtntrn_enter_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtntrn_enter_Visible), 5, 0), true);
            }
            DisableAttributes1L6( ) ;
         }
         AssignProp("", false, edtavCombolocationid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCombolocationid_Enabled), 5, 0), true);
         AssignProp("", false, edtavComboorganisationid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavComboorganisationid_Enabled), 5, 0), true);
      }

      protected void set_caption( )
      {
         if ( ( IsConfirmed == 1 ) && ( AnyError == 0 ) )
         {
            if ( IsDlt( ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_confdelete", ""), 0, "", true);
            }
            else
            {
               GX_msglist.addItem(context.GetMessage( "GXM_mustconfirm", ""), 0, "", true);
            }
         }
      }

      protected void CONFIRM_1L0( )
      {
         BeforeValidate1L6( ) ;
         if ( AnyError == 0 )
         {
            if ( IsDlt( ) )
            {
               OnDeleteControls1L6( ) ;
            }
            else
            {
               CheckExtendedTable1L6( ) ;
               CloseExtendedTableCursors1L6( ) ;
            }
         }
         if ( AnyError == 0 )
         {
            /* Save parent mode. */
            sMode6 = Gx_mode;
            CONFIRM_1L95( ) ;
            if ( AnyError == 0 )
            {
               /* Restore parent mode. */
               Gx_mode = sMode6;
               AssignAttri("", false, "Gx_mode", Gx_mode);
               IsConfirmed = 1;
               AssignAttri("", false, "IsConfirmed", StringUtil.LTrimStr( (decimal)(IsConfirmed), 4, 0));
            }
            /* Restore parent mode. */
            Gx_mode = sMode6;
            AssignAttri("", false, "Gx_mode", Gx_mode);
         }
      }

      protected void CONFIRM_1L95( )
      {
         nGXsfl_59_idx = 0;
         while ( nGXsfl_59_idx < nRC_GXsfl_59 )
         {
            ReadRow1L95( ) ;
            if ( ( nRcdExists_95 != 0 ) || ( nIsMod_95 != 0 ) )
            {
               GetKey1L95( ) ;
               if ( ( nRcdExists_95 == 0 ) && ( nRcdDeleted_95 == 0 ) )
               {
                  if ( RcdFound95 == 0 )
                  {
                     Gx_mode = "INS";
                     AssignAttri("", false, "Gx_mode", Gx_mode);
                     BeforeValidate1L95( ) ;
                     if ( AnyError == 0 )
                     {
                        CheckExtendedTable1L95( ) ;
                        CloseExtendedTableCursors1L95( ) ;
                        if ( AnyError == 0 )
                        {
                           IsConfirmed = 1;
                           AssignAttri("", false, "IsConfirmed", StringUtil.LTrimStr( (decimal)(IsConfirmed), 4, 0));
                        }
                     }
                  }
                  else
                  {
                     GXCCtl = "PAGEID_" + sGXsfl_59_idx;
                     GX_msglist.addItem(context.GetMessage( "GXM_noupdate", ""), "DuplicatePrimaryKey", 1, GXCCtl);
                     AnyError = 1;
                     GX_FocusControl = edtPageId_Internalname;
                     AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  }
               }
               else
               {
                  if ( RcdFound95 != 0 )
                  {
                     if ( nRcdDeleted_95 != 0 )
                     {
                        Gx_mode = "DLT";
                        AssignAttri("", false, "Gx_mode", Gx_mode);
                        getByPrimaryKey1L95( ) ;
                        Load1L95( ) ;
                        BeforeValidate1L95( ) ;
                        if ( AnyError == 0 )
                        {
                           OnDeleteControls1L95( ) ;
                        }
                     }
                     else
                     {
                        if ( nIsMod_95 != 0 )
                        {
                           Gx_mode = "UPD";
                           AssignAttri("", false, "Gx_mode", Gx_mode);
                           BeforeValidate1L95( ) ;
                           if ( AnyError == 0 )
                           {
                              CheckExtendedTable1L95( ) ;
                              CloseExtendedTableCursors1L95( ) ;
                              if ( AnyError == 0 )
                              {
                                 IsConfirmed = 1;
                                 AssignAttri("", false, "IsConfirmed", StringUtil.LTrimStr( (decimal)(IsConfirmed), 4, 0));
                              }
                           }
                        }
                     }
                  }
                  else
                  {
                     if ( nRcdDeleted_95 == 0 )
                     {
                        GXCCtl = "PAGEID_" + sGXsfl_59_idx;
                        GX_msglist.addItem(context.GetMessage( "GXM_recdeleted", ""), 1, GXCCtl);
                        AnyError = 1;
                        GX_FocusControl = edtPageId_Internalname;
                        AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                     }
                  }
               }
            }
            ChangePostValue( edtPageId_Internalname, A516PageId.ToString()) ;
            ChangePostValue( edtPageName_Internalname, A517PageName) ;
            ChangePostValue( edtPageStructure_Internalname, A518PageStructure) ;
            ChangePostValue( edtPagePublishedStructure_Internalname, A536PagePublishedStructure) ;
            ChangePostValue( chkIsPredefined_Internalname, StringUtil.BoolToStr( A541IsPredefined)) ;
            ChangePostValue( cmbPageType_Internalname, A525PageType) ;
            ChangePostValue( "ZT_"+"Z516PageId_"+sGXsfl_59_idx, Z516PageId.ToString()) ;
            ChangePostValue( "ZT_"+"Z541IsPredefined_"+sGXsfl_59_idx, StringUtil.BoolToStr( Z541IsPredefined)) ;
            ChangePostValue( "ZT_"+"Z517PageName_"+sGXsfl_59_idx, Z517PageName) ;
            ChangePostValue( "ZT_"+"Z525PageType_"+sGXsfl_59_idx, Z525PageType) ;
            ChangePostValue( "nRcdDeleted_95_"+sGXsfl_59_idx, StringUtil.LTrim( StringUtil.NToC( (decimal)(nRcdDeleted_95), 4, 0, context.GetLanguageProperty( "decimal_point"), ""))) ;
            ChangePostValue( "nRcdExists_95_"+sGXsfl_59_idx, StringUtil.LTrim( StringUtil.NToC( (decimal)(nRcdExists_95), 4, 0, context.GetLanguageProperty( "decimal_point"), ""))) ;
            ChangePostValue( "nIsMod_95_"+sGXsfl_59_idx, StringUtil.LTrim( StringUtil.NToC( (decimal)(nIsMod_95), 4, 0, context.GetLanguageProperty( "decimal_point"), ""))) ;
            if ( nIsMod_95 != 0 )
            {
               ChangePostValue( "PAGEID_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtPageId_Enabled), 5, 0, ".", ""))) ;
               ChangePostValue( "PAGENAME_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtPageName_Enabled), 5, 0, ".", ""))) ;
               ChangePostValue( "PAGESTRUCTURE_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtPageStructure_Enabled), 5, 0, ".", ""))) ;
               ChangePostValue( "PAGEPUBLISHEDSTRUCTURE_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtPagePublishedStructure_Enabled), 5, 0, ".", ""))) ;
               ChangePostValue( "ISPREDEFINED_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(chkIsPredefined.Enabled), 5, 0, ".", ""))) ;
               ChangePostValue( "PAGETYPE_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(cmbPageType.Enabled), 5, 0, ".", ""))) ;
            }
         }
         /* Start of After( level) rules */
         /* End of After( level) rules */
      }

      protected void ResetCaption1L0( )
      {
      }

      protected void E111L2( )
      {
         /* Start Routine */
         returnInSub = false;
         divLayoutmaintable_Class = divLayoutmaintable_Class+" "+"EditForm";
         AssignProp("", false, divLayoutmaintable_Internalname, "Class", divLayoutmaintable_Class, true);
         new GeneXus.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV9WWPContext) ;
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 = AV18DDO_TitleSettingsIcons;
         new GeneXus.Programs.wwpbaseobjects.getwwptitlesettingsicons(context ).execute( out  GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1) ;
         AV18DDO_TitleSettingsIcons = GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1;
         AV24GAMSession = new GeneXus.Programs.genexussecurity.SdtGAMSession(context).get(out  AV25GAMErrors);
         Combo_organisationid_Gamoauthtoken = AV24GAMSession.gxTpr_Token;
         ucCombo_organisationid.SendProperty(context, "", false, Combo_organisationid_Internalname, "GAMOAuthToken", Combo_organisationid_Gamoauthtoken);
         edtOrganisationId_Visible = 0;
         AssignProp("", false, edtOrganisationId_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtOrganisationId_Visible), 5, 0), true);
         AV27ComboOrganisationId = Guid.Empty;
         AssignAttri("", false, "AV27ComboOrganisationId", AV27ComboOrganisationId.ToString());
         edtavComboorganisationid_Visible = 0;
         AssignProp("", false, edtavComboorganisationid_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavComboorganisationid_Visible), 5, 0), true);
         Combo_locationid_Gamoauthtoken = AV24GAMSession.gxTpr_Token;
         ucCombo_locationid.SendProperty(context, "", false, Combo_locationid_Internalname, "GAMOAuthToken", Combo_locationid_Gamoauthtoken);
         edtLocationId_Visible = 0;
         AssignProp("", false, edtLocationId_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtLocationId_Visible), 5, 0), true);
         AV22ComboLocationId = Guid.Empty;
         AssignAttri("", false, "AV22ComboLocationId", AV22ComboLocationId.ToString());
         edtavCombolocationid_Visible = 0;
         AssignProp("", false, edtavCombolocationid_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavCombolocationid_Visible), 5, 0), true);
         /* Execute user subroutine: 'LOADCOMBOLOCATIONID' */
         S112 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         /* Execute user subroutine: 'LOADCOMBOORGANISATIONID' */
         S122 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         AV12TrnContext.FromXml(AV13WebSession.Get("TrnContext"), null, "", "");
         if ( ( StringUtil.StrCmp(AV12TrnContext.gxTpr_Transactionname, AV30Pgmname) == 0 ) && ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) )
         {
            AV31GXV1 = 1;
            AssignAttri("", false, "AV31GXV1", StringUtil.LTrimStr( (decimal)(AV31GXV1), 8, 0));
            while ( AV31GXV1 <= AV12TrnContext.gxTpr_Attributes.Count )
            {
               AV16TrnContextAtt = ((WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext_Attribute)AV12TrnContext.gxTpr_Attributes.Item(AV31GXV1));
               if ( StringUtil.StrCmp(AV16TrnContextAtt.gxTpr_Attributename, "LocationId") == 0 )
               {
                  AV14Insert_LocationId = StringUtil.StrToGuid( AV16TrnContextAtt.gxTpr_Attributevalue);
                  AssignAttri("", false, "AV14Insert_LocationId", AV14Insert_LocationId.ToString());
                  if ( ! (Guid.Empty==AV14Insert_LocationId) )
                  {
                     AV22ComboLocationId = AV14Insert_LocationId;
                     AssignAttri("", false, "AV22ComboLocationId", AV22ComboLocationId.ToString());
                     Combo_locationid_Selectedvalue_set = StringUtil.Trim( AV22ComboLocationId.ToString());
                     ucCombo_locationid.SendProperty(context, "", false, Combo_locationid_Internalname, "SelectedValue_set", Combo_locationid_Selectedvalue_set);
                     GXt_char2 = AV21Combo_DataJson;
                     new trn_appversionloaddvcombo(context ).execute(  "LocationId",  "GET",  false,  AV8AppVersionId,  AV15Insert_OrganisationId,  AV16TrnContextAtt.gxTpr_Attributevalue, out  AV19ComboSelectedValue, out  AV20ComboSelectedText, out  GXt_char2) ;
                     AssignAttri("", false, "AV19ComboSelectedValue", AV19ComboSelectedValue);
                     AssignAttri("", false, "AV20ComboSelectedText", AV20ComboSelectedText);
                     AV21Combo_DataJson = GXt_char2;
                     AssignAttri("", false, "AV21Combo_DataJson", AV21Combo_DataJson);
                     Combo_locationid_Selectedtext_set = AV20ComboSelectedText;
                     ucCombo_locationid.SendProperty(context, "", false, Combo_locationid_Internalname, "SelectedText_set", Combo_locationid_Selectedtext_set);
                     Combo_locationid_Enabled = false;
                     ucCombo_locationid.SendProperty(context, "", false, Combo_locationid_Internalname, "Enabled", StringUtil.BoolToStr( Combo_locationid_Enabled));
                  }
               }
               else if ( StringUtil.StrCmp(AV16TrnContextAtt.gxTpr_Attributename, "OrganisationId") == 0 )
               {
                  AV15Insert_OrganisationId = StringUtil.StrToGuid( AV16TrnContextAtt.gxTpr_Attributevalue);
                  AssignAttri("", false, "AV15Insert_OrganisationId", AV15Insert_OrganisationId.ToString());
                  if ( ! (Guid.Empty==AV15Insert_OrganisationId) )
                  {
                     AV27ComboOrganisationId = AV15Insert_OrganisationId;
                     AssignAttri("", false, "AV27ComboOrganisationId", AV27ComboOrganisationId.ToString());
                     Combo_organisationid_Selectedvalue_set = StringUtil.Trim( AV27ComboOrganisationId.ToString());
                     ucCombo_organisationid.SendProperty(context, "", false, Combo_organisationid_Internalname, "SelectedValue_set", Combo_organisationid_Selectedvalue_set);
                     GXt_char2 = AV21Combo_DataJson;
                     new trn_appversionloaddvcombo(context ).execute(  "OrganisationId",  "GET",  false,  AV8AppVersionId,  A11OrganisationId,  AV16TrnContextAtt.gxTpr_Attributevalue, out  AV19ComboSelectedValue, out  AV20ComboSelectedText, out  GXt_char2) ;
                     AssignAttri("", false, "AV19ComboSelectedValue", AV19ComboSelectedValue);
                     AssignAttri("", false, "AV20ComboSelectedText", AV20ComboSelectedText);
                     AV21Combo_DataJson = GXt_char2;
                     AssignAttri("", false, "AV21Combo_DataJson", AV21Combo_DataJson);
                     Combo_organisationid_Selectedtext_set = AV20ComboSelectedText;
                     ucCombo_organisationid.SendProperty(context, "", false, Combo_organisationid_Internalname, "SelectedText_set", Combo_organisationid_Selectedtext_set);
                     Combo_organisationid_Enabled = false;
                     ucCombo_organisationid.SendProperty(context, "", false, Combo_organisationid_Internalname, "Enabled", StringUtil.BoolToStr( Combo_organisationid_Enabled));
                  }
               }
               AV31GXV1 = (int)(AV31GXV1+1);
               AssignAttri("", false, "AV31GXV1", StringUtil.LTrimStr( (decimal)(AV31GXV1), 8, 0));
            }
         }
      }

      protected void E141L2( )
      {
         /* After Trn Routine */
         returnInSub = false;
         if ( ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 ) && ! AV12TrnContext.gxTpr_Callerondelete )
         {
            CallWebObject(formatLink("trn_appversionww.aspx") );
            context.wjLocDisableFrm = 1;
         }
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
      }

      protected void E131L2( )
      {
         /* Combo_organisationid_Onoptionclicked Routine */
         returnInSub = false;
         AV23Cond_OrganisationId = A11OrganisationId;
         AV27ComboOrganisationId = StringUtil.StrToGuid( Combo_organisationid_Selectedvalue_get);
         AssignAttri("", false, "AV27ComboOrganisationId", AV27ComboOrganisationId.ToString());
         if ( AV23Cond_OrganisationId != AV27ComboOrganisationId )
         {
            AV22ComboLocationId = Guid.Empty;
            AssignAttri("", false, "AV22ComboLocationId", AV22ComboLocationId.ToString());
            Combo_locationid_Selectedvalue_set = "";
            ucCombo_locationid.SendProperty(context, "", false, Combo_locationid_Internalname, "SelectedValue_set", Combo_locationid_Selectedvalue_set);
            Combo_locationid_Selectedtext_set = "";
            ucCombo_locationid.SendProperty(context, "", false, Combo_locationid_Internalname, "SelectedText_set", Combo_locationid_Selectedtext_set);
         }
         /*  Sending Event outputs  */
      }

      protected void E121L2( )
      {
         /* Combo_locationid_Onoptionclicked Routine */
         returnInSub = false;
         AV22ComboLocationId = StringUtil.StrToGuid( Combo_locationid_Selectedvalue_get);
         AssignAttri("", false, "AV22ComboLocationId", AV22ComboLocationId.ToString());
         /*  Sending Event outputs  */
      }

      protected void S122( )
      {
         /* 'LOADCOMBOORGANISATIONID' Routine */
         returnInSub = false;
         GXt_char2 = AV21Combo_DataJson;
         new trn_appversionloaddvcombo(context ).execute(  "OrganisationId",  Gx_mode,  false,  AV8AppVersionId,  A11OrganisationId,  "", out  AV19ComboSelectedValue, out  AV20ComboSelectedText, out  GXt_char2) ;
         AssignAttri("", false, "AV19ComboSelectedValue", AV19ComboSelectedValue);
         AssignAttri("", false, "AV20ComboSelectedText", AV20ComboSelectedText);
         AV21Combo_DataJson = GXt_char2;
         AssignAttri("", false, "AV21Combo_DataJson", AV21Combo_DataJson);
         Combo_organisationid_Selectedvalue_set = AV19ComboSelectedValue;
         ucCombo_organisationid.SendProperty(context, "", false, Combo_organisationid_Internalname, "SelectedValue_set", Combo_organisationid_Selectedvalue_set);
         Combo_organisationid_Selectedtext_set = AV20ComboSelectedText;
         ucCombo_organisationid.SendProperty(context, "", false, Combo_organisationid_Internalname, "SelectedText_set", Combo_organisationid_Selectedtext_set);
         AV27ComboOrganisationId = StringUtil.StrToGuid( AV19ComboSelectedValue);
         AssignAttri("", false, "AV27ComboOrganisationId", AV27ComboOrganisationId.ToString());
         if ( ( StringUtil.StrCmp(Gx_mode, "DSP") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 ) )
         {
            Combo_organisationid_Enabled = false;
            ucCombo_organisationid.SendProperty(context, "", false, Combo_organisationid_Internalname, "Enabled", StringUtil.BoolToStr( Combo_organisationid_Enabled));
         }
      }

      protected void S112( )
      {
         /* 'LOADCOMBOLOCATIONID' Routine */
         returnInSub = false;
         Combo_locationid_Datalistprocparametersprefix = StringUtil.Format( " \"ComboName\": \"LocationId\", \"TrnMode\": \"INS\", \"IsDynamicCall\": true, \"AppVersionId\": \"00000000-0000-0000-0000-000000000000\", \"Cond_OrganisationId\": \"#%1#\"", edtOrganisationId_Internalname, "", "", "", "", "", "", "", "");
         ucCombo_locationid.SendProperty(context, "", false, Combo_locationid_Internalname, "DataListProcParametersPrefix", Combo_locationid_Datalistprocparametersprefix);
         GXt_char2 = AV21Combo_DataJson;
         new trn_appversionloaddvcombo(context ).execute(  "LocationId",  Gx_mode,  false,  AV8AppVersionId,  A11OrganisationId,  "", out  AV19ComboSelectedValue, out  AV20ComboSelectedText, out  GXt_char2) ;
         AssignAttri("", false, "AV19ComboSelectedValue", AV19ComboSelectedValue);
         AssignAttri("", false, "AV20ComboSelectedText", AV20ComboSelectedText);
         AV21Combo_DataJson = GXt_char2;
         AssignAttri("", false, "AV21Combo_DataJson", AV21Combo_DataJson);
         Combo_locationid_Selectedvalue_set = AV19ComboSelectedValue;
         ucCombo_locationid.SendProperty(context, "", false, Combo_locationid_Internalname, "SelectedValue_set", Combo_locationid_Selectedvalue_set);
         Combo_locationid_Selectedtext_set = AV20ComboSelectedText;
         ucCombo_locationid.SendProperty(context, "", false, Combo_locationid_Internalname, "SelectedText_set", Combo_locationid_Selectedtext_set);
         AV22ComboLocationId = StringUtil.StrToGuid( AV19ComboSelectedValue);
         AssignAttri("", false, "AV22ComboLocationId", AV22ComboLocationId.ToString());
         if ( ( StringUtil.StrCmp(Gx_mode, "DSP") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 ) )
         {
            Combo_locationid_Enabled = false;
            ucCombo_locationid.SendProperty(context, "", false, Combo_locationid_Internalname, "Enabled", StringUtil.BoolToStr( Combo_locationid_Enabled));
         }
      }

      protected void ZM1L6( short GX_JID )
      {
         if ( ( GX_JID == 19 ) || ( GX_JID == 0 ) )
         {
            if ( ! IsIns( ) )
            {
               Z524AppVersionName = T001L5_A524AppVersionName[0];
               Z535IsActive = T001L5_A535IsActive[0];
               Z29LocationId = T001L5_A29LocationId[0];
               Z11OrganisationId = T001L5_A11OrganisationId[0];
            }
            else
            {
               Z524AppVersionName = A524AppVersionName;
               Z535IsActive = A535IsActive;
               Z29LocationId = A29LocationId;
               Z11OrganisationId = A11OrganisationId;
            }
         }
         if ( GX_JID == -19 )
         {
            Z523AppVersionId = A523AppVersionId;
            Z524AppVersionName = A524AppVersionName;
            Z535IsActive = A535IsActive;
            Z29LocationId = A29LocationId;
            Z11OrganisationId = A11OrganisationId;
         }
      }

      protected void standaloneNotModal( )
      {
         AV30Pgmname = "Trn_AppVersion";
         AssignAttri("", false, "AV30Pgmname", AV30Pgmname);
         Gx_BScreen = 0;
         AssignAttri("", false, "Gx_BScreen", StringUtil.Str( (decimal)(Gx_BScreen), 1, 0));
         bttBtntrn_delete_Enabled = 0;
         AssignProp("", false, bttBtntrn_delete_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(bttBtntrn_delete_Enabled), 5, 0), true);
         if ( ! (Guid.Empty==AV8AppVersionId) )
         {
            edtAppVersionId_Enabled = 0;
            AssignProp("", false, edtAppVersionId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtAppVersionId_Enabled), 5, 0), true);
         }
         else
         {
            edtAppVersionId_Enabled = 1;
            AssignProp("", false, edtAppVersionId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtAppVersionId_Enabled), 5, 0), true);
         }
         if ( ! (Guid.Empty==AV8AppVersionId) )
         {
            edtAppVersionId_Enabled = 0;
            AssignProp("", false, edtAppVersionId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtAppVersionId_Enabled), 5, 0), true);
         }
         if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) && ! (Guid.Empty==AV14Insert_LocationId) )
         {
            edtLocationId_Enabled = 0;
            AssignProp("", false, edtLocationId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtLocationId_Enabled), 5, 0), true);
         }
         else
         {
            edtLocationId_Enabled = 1;
            AssignProp("", false, edtLocationId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtLocationId_Enabled), 5, 0), true);
         }
         if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) && ! (Guid.Empty==AV15Insert_OrganisationId) )
         {
            edtOrganisationId_Enabled = 0;
            AssignProp("", false, edtOrganisationId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtOrganisationId_Enabled), 5, 0), true);
         }
         else
         {
            edtOrganisationId_Enabled = 1;
            AssignProp("", false, edtOrganisationId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtOrganisationId_Enabled), 5, 0), true);
         }
      }

      protected void standaloneModal( )
      {
         if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) && ! (Guid.Empty==AV14Insert_LocationId) )
         {
            A29LocationId = AV14Insert_LocationId;
            n29LocationId = false;
            AssignAttri("", false, "A29LocationId", A29LocationId.ToString());
         }
         else
         {
            if ( (Guid.Empty==AV22ComboLocationId) )
            {
               A29LocationId = Guid.Empty;
               n29LocationId = false;
               AssignAttri("", false, "A29LocationId", A29LocationId.ToString());
               n29LocationId = true;
               AssignAttri("", false, "A29LocationId", A29LocationId.ToString());
            }
            else
            {
               if ( ! (Guid.Empty==AV22ComboLocationId) )
               {
                  A29LocationId = AV22ComboLocationId;
                  n29LocationId = false;
                  AssignAttri("", false, "A29LocationId", A29LocationId.ToString());
               }
            }
         }
         if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) && ! (Guid.Empty==AV15Insert_OrganisationId) )
         {
            A11OrganisationId = AV15Insert_OrganisationId;
            n11OrganisationId = false;
            AssignAttri("", false, "A11OrganisationId", A11OrganisationId.ToString());
         }
         else
         {
            if ( (Guid.Empty==AV27ComboOrganisationId) )
            {
               A11OrganisationId = Guid.Empty;
               n11OrganisationId = false;
               AssignAttri("", false, "A11OrganisationId", A11OrganisationId.ToString());
               n11OrganisationId = true;
               AssignAttri("", false, "A11OrganisationId", A11OrganisationId.ToString());
            }
            else
            {
               if ( ! (Guid.Empty==AV27ComboOrganisationId) )
               {
                  A11OrganisationId = AV27ComboOrganisationId;
                  n11OrganisationId = false;
                  AssignAttri("", false, "A11OrganisationId", A11OrganisationId.ToString());
               }
            }
         }
         if ( StringUtil.StrCmp(Gx_mode, "DSP") == 0 )
         {
            bttBtntrn_enter_Enabled = 0;
            AssignProp("", false, bttBtntrn_enter_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(bttBtntrn_enter_Enabled), 5, 0), true);
         }
         else
         {
            bttBtntrn_enter_Enabled = 1;
            AssignProp("", false, bttBtntrn_enter_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(bttBtntrn_enter_Enabled), 5, 0), true);
         }
         if ( ! (Guid.Empty==AV8AppVersionId) )
         {
            A523AppVersionId = AV8AppVersionId;
            AssignAttri("", false, "A523AppVersionId", A523AppVersionId.ToString());
         }
         else
         {
            if ( IsIns( )  && (Guid.Empty==A523AppVersionId) && ( Gx_BScreen == 0 ) )
            {
               A523AppVersionId = Guid.NewGuid( );
               AssignAttri("", false, "A523AppVersionId", A523AppVersionId.ToString());
            }
         }
         if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) && ( Gx_BScreen == 0 ) )
         {
         }
      }

      protected void Load1L6( )
      {
         /* Using cursor T001L8 */
         pr_default.execute(6, new Object[] {A523AppVersionId});
         if ( (pr_default.getStatus(6) != 101) )
         {
            RcdFound6 = 1;
            A524AppVersionName = T001L8_A524AppVersionName[0];
            AssignAttri("", false, "A524AppVersionName", A524AppVersionName);
            A535IsActive = T001L8_A535IsActive[0];
            AssignAttri("", false, "A535IsActive", A535IsActive);
            A29LocationId = T001L8_A29LocationId[0];
            n29LocationId = T001L8_n29LocationId[0];
            AssignAttri("", false, "A29LocationId", A29LocationId.ToString());
            A11OrganisationId = T001L8_A11OrganisationId[0];
            n11OrganisationId = T001L8_n11OrganisationId[0];
            AssignAttri("", false, "A11OrganisationId", A11OrganisationId.ToString());
            ZM1L6( -19) ;
         }
         pr_default.close(6);
         OnLoadActions1L6( ) ;
      }

      protected void OnLoadActions1L6( )
      {
      }

      protected void CheckExtendedTable1L6( )
      {
         Gx_BScreen = 1;
         AssignAttri("", false, "Gx_BScreen", StringUtil.Str( (decimal)(Gx_BScreen), 1, 0));
         standaloneModal( ) ;
         /* Using cursor T001L7 */
         pr_default.execute(5, new Object[] {n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
         if ( (pr_default.getStatus(5) == 101) )
         {
            if ( ! ( (Guid.Empty==A29LocationId) || (Guid.Empty==A11OrganisationId) ) )
            {
               GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXSPC_ForeignKeyNotFound", ""), context.GetMessage( "Locations", ""), "", "", "", "", "", "", "", ""), "ForeignKeyNotFound", 1, "ORGANISATIONID");
               AnyError = 1;
               GX_FocusControl = edtLocationId_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
         }
         pr_default.close(5);
         /* Using cursor T001L6 */
         pr_default.execute(4, new Object[] {n11OrganisationId, A11OrganisationId});
         if ( (pr_default.getStatus(4) == 101) )
         {
            if ( ! ( (Guid.Empty==A11OrganisationId) ) )
            {
               GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXSPC_ForeignKeyNotFound", ""), context.GetMessage( "Trn_Organisation", ""), "", "", "", "", "", "", "", ""), "ForeignKeyNotFound", 1, "ORGANISATIONID");
               AnyError = 1;
               GX_FocusControl = edtOrganisationId_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
         }
         pr_default.close(4);
      }

      protected void CloseExtendedTableCursors1L6( )
      {
         pr_default.close(5);
         pr_default.close(4);
      }

      protected void enableDisable( )
      {
      }

      protected void gxLoad_21( Guid A29LocationId ,
                                Guid A11OrganisationId )
      {
         /* Using cursor T001L9 */
         pr_default.execute(7, new Object[] {n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
         if ( (pr_default.getStatus(7) == 101) )
         {
            if ( ! ( (Guid.Empty==A29LocationId) || (Guid.Empty==A11OrganisationId) ) )
            {
               GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXSPC_ForeignKeyNotFound", ""), context.GetMessage( "Locations", ""), "", "", "", "", "", "", "", ""), "ForeignKeyNotFound", 1, "ORGANISATIONID");
               AnyError = 1;
               GX_FocusControl = edtLocationId_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
         }
         GxWebStd.set_html_headers( context, 0, "", "");
         AddString( "[[") ;
         AddString( "]") ;
         if ( (pr_default.getStatus(7) == 101) )
         {
            AddString( ",") ;
            AddString( "101") ;
         }
         AddString( "]") ;
         pr_default.close(7);
      }

      protected void gxLoad_20( Guid A11OrganisationId )
      {
         /* Using cursor T001L10 */
         pr_default.execute(8, new Object[] {n11OrganisationId, A11OrganisationId});
         if ( (pr_default.getStatus(8) == 101) )
         {
            if ( ! ( (Guid.Empty==A11OrganisationId) ) )
            {
               GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXSPC_ForeignKeyNotFound", ""), context.GetMessage( "Trn_Organisation", ""), "", "", "", "", "", "", "", ""), "ForeignKeyNotFound", 1, "ORGANISATIONID");
               AnyError = 1;
               GX_FocusControl = edtOrganisationId_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
         }
         GxWebStd.set_html_headers( context, 0, "", "");
         AddString( "[[") ;
         AddString( "]") ;
         if ( (pr_default.getStatus(8) == 101) )
         {
            AddString( ",") ;
            AddString( "101") ;
         }
         AddString( "]") ;
         pr_default.close(8);
      }

      protected void GetKey1L6( )
      {
         /* Using cursor T001L11 */
         pr_default.execute(9, new Object[] {A523AppVersionId});
         if ( (pr_default.getStatus(9) != 101) )
         {
            RcdFound6 = 1;
         }
         else
         {
            RcdFound6 = 0;
         }
         pr_default.close(9);
      }

      protected void getByPrimaryKey( )
      {
         /* Using cursor T001L5 */
         pr_default.execute(3, new Object[] {A523AppVersionId});
         if ( (pr_default.getStatus(3) != 101) )
         {
            ZM1L6( 19) ;
            RcdFound6 = 1;
            A523AppVersionId = T001L5_A523AppVersionId[0];
            AssignAttri("", false, "A523AppVersionId", A523AppVersionId.ToString());
            A524AppVersionName = T001L5_A524AppVersionName[0];
            AssignAttri("", false, "A524AppVersionName", A524AppVersionName);
            A535IsActive = T001L5_A535IsActive[0];
            AssignAttri("", false, "A535IsActive", A535IsActive);
            A29LocationId = T001L5_A29LocationId[0];
            n29LocationId = T001L5_n29LocationId[0];
            AssignAttri("", false, "A29LocationId", A29LocationId.ToString());
            A11OrganisationId = T001L5_A11OrganisationId[0];
            n11OrganisationId = T001L5_n11OrganisationId[0];
            AssignAttri("", false, "A11OrganisationId", A11OrganisationId.ToString());
            Z523AppVersionId = A523AppVersionId;
            sMode6 = Gx_mode;
            Gx_mode = "DSP";
            AssignAttri("", false, "Gx_mode", Gx_mode);
            Load1L6( ) ;
            if ( AnyError == 1 )
            {
               RcdFound6 = 0;
               InitializeNonKey1L6( ) ;
            }
            Gx_mode = sMode6;
            AssignAttri("", false, "Gx_mode", Gx_mode);
         }
         else
         {
            RcdFound6 = 0;
            InitializeNonKey1L6( ) ;
            sMode6 = Gx_mode;
            Gx_mode = "DSP";
            AssignAttri("", false, "Gx_mode", Gx_mode);
            standaloneModal( ) ;
            Gx_mode = sMode6;
            AssignAttri("", false, "Gx_mode", Gx_mode);
         }
         pr_default.close(3);
      }

      protected void getEqualNoModal( )
      {
         GetKey1L6( ) ;
         if ( RcdFound6 == 0 )
         {
         }
         else
         {
         }
         getByPrimaryKey( ) ;
      }

      protected void move_next( )
      {
         RcdFound6 = 0;
         /* Using cursor T001L12 */
         pr_default.execute(10, new Object[] {A523AppVersionId});
         if ( (pr_default.getStatus(10) != 101) )
         {
            while ( (pr_default.getStatus(10) != 101) && ( ( GuidUtil.Compare(T001L12_A523AppVersionId[0], A523AppVersionId, 0) < 0 ) ) )
            {
               pr_default.readNext(10);
            }
            if ( (pr_default.getStatus(10) != 101) && ( ( GuidUtil.Compare(T001L12_A523AppVersionId[0], A523AppVersionId, 0) > 0 ) ) )
            {
               A523AppVersionId = T001L12_A523AppVersionId[0];
               AssignAttri("", false, "A523AppVersionId", A523AppVersionId.ToString());
               RcdFound6 = 1;
            }
         }
         pr_default.close(10);
      }

      protected void move_previous( )
      {
         RcdFound6 = 0;
         /* Using cursor T001L13 */
         pr_default.execute(11, new Object[] {A523AppVersionId});
         if ( (pr_default.getStatus(11) != 101) )
         {
            while ( (pr_default.getStatus(11) != 101) && ( ( GuidUtil.Compare(T001L13_A523AppVersionId[0], A523AppVersionId, 0) > 0 ) ) )
            {
               pr_default.readNext(11);
            }
            if ( (pr_default.getStatus(11) != 101) && ( ( GuidUtil.Compare(T001L13_A523AppVersionId[0], A523AppVersionId, 0) < 0 ) ) )
            {
               A523AppVersionId = T001L13_A523AppVersionId[0];
               AssignAttri("", false, "A523AppVersionId", A523AppVersionId.ToString());
               RcdFound6 = 1;
            }
         }
         pr_default.close(11);
      }

      protected void btn_enter( )
      {
         nKeyPressed = 1;
         GetKey1L6( ) ;
         if ( IsIns( ) )
         {
            /* Insert record */
            GX_FocusControl = edtAppVersionId_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            Insert1L6( ) ;
            if ( AnyError == 1 )
            {
               GX_FocusControl = "";
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
         }
         else
         {
            if ( RcdFound6 == 1 )
            {
               if ( A523AppVersionId != Z523AppVersionId )
               {
                  A523AppVersionId = Z523AppVersionId;
                  AssignAttri("", false, "A523AppVersionId", A523AppVersionId.ToString());
                  GX_msglist.addItem(context.GetMessage( "GXM_getbeforeupd", ""), "CandidateKeyNotFound", 1, "APPVERSIONID");
                  AnyError = 1;
                  GX_FocusControl = edtAppVersionId_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               }
               else if ( IsDlt( ) )
               {
                  delete( ) ;
                  AfterTrn( ) ;
                  GX_FocusControl = edtAppVersionId_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               }
               else
               {
                  /* Update record */
                  Update1L6( ) ;
                  GX_FocusControl = edtAppVersionId_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               }
            }
            else
            {
               if ( A523AppVersionId != Z523AppVersionId )
               {
                  /* Insert record */
                  GX_FocusControl = edtAppVersionId_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  Insert1L6( ) ;
                  if ( AnyError == 1 )
                  {
                     GX_FocusControl = "";
                     AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  }
               }
               else
               {
                  if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
                  {
                     GX_msglist.addItem(context.GetMessage( "GXM_recdeleted", ""), 1, "APPVERSIONID");
                     AnyError = 1;
                     GX_FocusControl = edtAppVersionId_Internalname;
                     AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  }
                  else
                  {
                     /* Insert record */
                     GX_FocusControl = edtAppVersionId_Internalname;
                     AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                     Insert1L6( ) ;
                     if ( AnyError == 1 )
                     {
                        GX_FocusControl = "";
                        AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                     }
                  }
               }
            }
         }
         AfterTrn( ) ;
         if ( IsIns( ) || IsUpd( ) || IsDlt( ) )
         {
            if ( AnyError == 0 )
            {
               context.nUserReturn = 1;
            }
         }
      }

      protected void btn_delete( )
      {
         if ( A523AppVersionId != Z523AppVersionId )
         {
            A523AppVersionId = Z523AppVersionId;
            AssignAttri("", false, "A523AppVersionId", A523AppVersionId.ToString());
            GX_msglist.addItem(context.GetMessage( "GXM_getbeforedlt", ""), 1, "APPVERSIONID");
            AnyError = 1;
            GX_FocusControl = edtAppVersionId_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         else
         {
            delete( ) ;
            AfterTrn( ) ;
            GX_FocusControl = edtAppVersionId_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         if ( AnyError != 0 )
         {
         }
      }

      protected void CheckOptimisticConcurrency1L6( )
      {
         if ( ! IsIns( ) )
         {
            /* Using cursor T001L4 */
            pr_default.execute(2, new Object[] {A523AppVersionId});
            if ( (pr_default.getStatus(2) == 103) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"Trn_AppVersion"}), "RecordIsLocked", 1, "");
               AnyError = 1;
               return  ;
            }
            if ( (pr_default.getStatus(2) == 101) || ( StringUtil.StrCmp(Z524AppVersionName, T001L4_A524AppVersionName[0]) != 0 ) || ( Z535IsActive != T001L4_A535IsActive[0] ) || ( Z29LocationId != T001L4_A29LocationId[0] ) || ( Z11OrganisationId != T001L4_A11OrganisationId[0] ) )
            {
               if ( StringUtil.StrCmp(Z524AppVersionName, T001L4_A524AppVersionName[0]) != 0 )
               {
                  GXUtil.WriteLog("trn_appversion:[seudo value changed for attri]"+"AppVersionName");
                  GXUtil.WriteLogRaw("Old: ",Z524AppVersionName);
                  GXUtil.WriteLogRaw("Current: ",T001L4_A524AppVersionName[0]);
               }
               if ( Z535IsActive != T001L4_A535IsActive[0] )
               {
                  GXUtil.WriteLog("trn_appversion:[seudo value changed for attri]"+"IsActive");
                  GXUtil.WriteLogRaw("Old: ",Z535IsActive);
                  GXUtil.WriteLogRaw("Current: ",T001L4_A535IsActive[0]);
               }
               if ( Z29LocationId != T001L4_A29LocationId[0] )
               {
                  GXUtil.WriteLog("trn_appversion:[seudo value changed for attri]"+"LocationId");
                  GXUtil.WriteLogRaw("Old: ",Z29LocationId);
                  GXUtil.WriteLogRaw("Current: ",T001L4_A29LocationId[0]);
               }
               if ( Z11OrganisationId != T001L4_A11OrganisationId[0] )
               {
                  GXUtil.WriteLog("trn_appversion:[seudo value changed for attri]"+"OrganisationId");
                  GXUtil.WriteLogRaw("Old: ",Z11OrganisationId);
                  GXUtil.WriteLogRaw("Current: ",T001L4_A11OrganisationId[0]);
               }
               GX_msglist.addItem(context.GetMessage( "GXM_waschg", new   object[]  {"Trn_AppVersion"}), "RecordWasChanged", 1, "");
               AnyError = 1;
               return  ;
            }
         }
      }

      protected void Insert1L6( )
      {
         if ( ! IsAuthorized("trn_appversion_Insert") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         BeforeValidate1L6( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable1L6( ) ;
         }
         if ( AnyError == 0 )
         {
            ZM1L6( 0) ;
            CheckOptimisticConcurrency1L6( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm1L6( ) ;
               if ( AnyError == 0 )
               {
                  BeforeInsert1L6( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor T001L14 */
                     pr_default.execute(12, new Object[] {A523AppVersionId, A524AppVersionName, A535IsActive, n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
                     pr_default.close(12);
                     pr_default.SmartCacheProvider.SetUpdated("Trn_AppVersion");
                     if ( (pr_default.getStatus(12) == 1) )
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
                           ProcessLevel1L6( ) ;
                           if ( AnyError == 0 )
                           {
                              if ( IsIns( ) || IsUpd( ) || IsDlt( ) )
                              {
                                 if ( AnyError == 0 )
                                 {
                                    context.nUserReturn = 1;
                                 }
                              }
                           }
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
               Load1L6( ) ;
            }
            EndLevel1L6( ) ;
         }
         CloseExtendedTableCursors1L6( ) ;
      }

      protected void Update1L6( )
      {
         if ( ! IsAuthorized("trn_appversion_Update") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         BeforeValidate1L6( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable1L6( ) ;
         }
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency1L6( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm1L6( ) ;
               if ( AnyError == 0 )
               {
                  BeforeUpdate1L6( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor T001L15 */
                     pr_default.execute(13, new Object[] {A524AppVersionName, A535IsActive, n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId, A523AppVersionId});
                     pr_default.close(13);
                     pr_default.SmartCacheProvider.SetUpdated("Trn_AppVersion");
                     if ( (pr_default.getStatus(13) == 103) )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"Trn_AppVersion"}), "RecordIsLocked", 1, "");
                        AnyError = 1;
                     }
                     DeferredUpdate1L6( ) ;
                     if ( AnyError == 0 )
                     {
                        /* Start of After( update) rules */
                        /* End of After( update) rules */
                        if ( AnyError == 0 )
                        {
                           ProcessLevel1L6( ) ;
                           if ( AnyError == 0 )
                           {
                              if ( IsIns( ) || IsUpd( ) || IsDlt( ) )
                              {
                                 if ( AnyError == 0 )
                                 {
                                    context.nUserReturn = 1;
                                 }
                              }
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
            }
            EndLevel1L6( ) ;
         }
         CloseExtendedTableCursors1L6( ) ;
      }

      protected void DeferredUpdate1L6( )
      {
      }

      protected void delete( )
      {
         if ( ! IsAuthorized("trn_appversion_Delete") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         BeforeValidate1L6( ) ;
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency1L6( ) ;
         }
         if ( AnyError == 0 )
         {
            OnDeleteControls1L6( ) ;
            AfterConfirm1L6( ) ;
            if ( AnyError == 0 )
            {
               BeforeDelete1L6( ) ;
               if ( AnyError == 0 )
               {
                  ScanStart1L95( ) ;
                  while ( RcdFound95 != 0 )
                  {
                     getByPrimaryKey1L95( ) ;
                     Delete1L95( ) ;
                     ScanNext1L95( ) ;
                  }
                  ScanEnd1L95( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor T001L16 */
                     pr_default.execute(14, new Object[] {A523AppVersionId});
                     pr_default.close(14);
                     pr_default.SmartCacheProvider.SetUpdated("Trn_AppVersion");
                     if ( AnyError == 0 )
                     {
                        /* Start of After( delete) rules */
                        /* End of After( delete) rules */
                        if ( AnyError == 0 )
                        {
                           if ( IsIns( ) || IsUpd( ) || IsDlt( ) )
                           {
                              if ( AnyError == 0 )
                              {
                                 context.nUserReturn = 1;
                              }
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
            }
         }
         sMode6 = Gx_mode;
         Gx_mode = "DLT";
         AssignAttri("", false, "Gx_mode", Gx_mode);
         EndLevel1L6( ) ;
         Gx_mode = sMode6;
         AssignAttri("", false, "Gx_mode", Gx_mode);
      }

      protected void OnDeleteControls1L6( )
      {
         standaloneModal( ) ;
         /* No delete mode formulas found. */
         if ( AnyError == 0 )
         {
            /* Using cursor T001L17 */
            pr_default.execute(15, new Object[] {A523AppVersionId});
            if ( (pr_default.getStatus(15) != 101) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_del", new   object[]  {context.GetMessage( "Locations", "")}), "CannotDeleteReferencedRecord", 1, "");
               AnyError = 1;
            }
            pr_default.close(15);
            /* Using cursor T001L18 */
            pr_default.execute(16, new Object[] {A523AppVersionId});
            if ( (pr_default.getStatus(16) != 101) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_del", new   object[]  {context.GetMessage( "Locations", "")}), "CannotDeleteReferencedRecord", 1, "");
               AnyError = 1;
            }
            pr_default.close(16);
         }
      }

      protected void ProcessNestedLevel1L95( )
      {
         nGXsfl_59_idx = 0;
         while ( nGXsfl_59_idx < nRC_GXsfl_59 )
         {
            ReadRow1L95( ) ;
            if ( ( nRcdExists_95 != 0 ) || ( nIsMod_95 != 0 ) )
            {
               standaloneNotModal1L95( ) ;
               GetKey1L95( ) ;
               if ( ( nRcdExists_95 == 0 ) && ( nRcdDeleted_95 == 0 ) )
               {
                  Gx_mode = "INS";
                  AssignAttri("", false, "Gx_mode", Gx_mode);
                  Insert1L95( ) ;
               }
               else
               {
                  if ( RcdFound95 != 0 )
                  {
                     if ( ( nRcdDeleted_95 != 0 ) && ( nRcdExists_95 != 0 ) )
                     {
                        Gx_mode = "DLT";
                        AssignAttri("", false, "Gx_mode", Gx_mode);
                        Delete1L95( ) ;
                     }
                     else
                     {
                        if ( nRcdExists_95 != 0 )
                        {
                           Gx_mode = "UPD";
                           AssignAttri("", false, "Gx_mode", Gx_mode);
                           Update1L95( ) ;
                        }
                     }
                  }
                  else
                  {
                     if ( nRcdDeleted_95 == 0 )
                     {
                        GXCCtl = "PAGEID_" + sGXsfl_59_idx;
                        GX_msglist.addItem(context.GetMessage( "GXM_recdeleted", ""), 1, GXCCtl);
                        AnyError = 1;
                        GX_FocusControl = edtPageId_Internalname;
                        AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                     }
                  }
               }
            }
            ChangePostValue( edtPageId_Internalname, A516PageId.ToString()) ;
            ChangePostValue( edtPageName_Internalname, A517PageName) ;
            ChangePostValue( edtPageStructure_Internalname, A518PageStructure) ;
            ChangePostValue( edtPagePublishedStructure_Internalname, A536PagePublishedStructure) ;
            ChangePostValue( chkIsPredefined_Internalname, StringUtil.BoolToStr( A541IsPredefined)) ;
            ChangePostValue( cmbPageType_Internalname, A525PageType) ;
            ChangePostValue( "ZT_"+"Z516PageId_"+sGXsfl_59_idx, Z516PageId.ToString()) ;
            ChangePostValue( "ZT_"+"Z541IsPredefined_"+sGXsfl_59_idx, StringUtil.BoolToStr( Z541IsPredefined)) ;
            ChangePostValue( "ZT_"+"Z517PageName_"+sGXsfl_59_idx, Z517PageName) ;
            ChangePostValue( "ZT_"+"Z525PageType_"+sGXsfl_59_idx, Z525PageType) ;
            ChangePostValue( "nRcdDeleted_95_"+sGXsfl_59_idx, StringUtil.LTrim( StringUtil.NToC( (decimal)(nRcdDeleted_95), 4, 0, context.GetLanguageProperty( "decimal_point"), ""))) ;
            ChangePostValue( "nRcdExists_95_"+sGXsfl_59_idx, StringUtil.LTrim( StringUtil.NToC( (decimal)(nRcdExists_95), 4, 0, context.GetLanguageProperty( "decimal_point"), ""))) ;
            ChangePostValue( "nIsMod_95_"+sGXsfl_59_idx, StringUtil.LTrim( StringUtil.NToC( (decimal)(nIsMod_95), 4, 0, context.GetLanguageProperty( "decimal_point"), ""))) ;
            if ( nIsMod_95 != 0 )
            {
               ChangePostValue( "PAGEID_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtPageId_Enabled), 5, 0, ".", ""))) ;
               ChangePostValue( "PAGENAME_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtPageName_Enabled), 5, 0, ".", ""))) ;
               ChangePostValue( "PAGESTRUCTURE_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtPageStructure_Enabled), 5, 0, ".", ""))) ;
               ChangePostValue( "PAGEPUBLISHEDSTRUCTURE_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtPagePublishedStructure_Enabled), 5, 0, ".", ""))) ;
               ChangePostValue( "ISPREDEFINED_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(chkIsPredefined.Enabled), 5, 0, ".", ""))) ;
               ChangePostValue( "PAGETYPE_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(cmbPageType.Enabled), 5, 0, ".", ""))) ;
            }
         }
         /* Start of After( level) rules */
         /* End of After( level) rules */
         InitAll1L95( ) ;
         if ( AnyError != 0 )
         {
         }
         nRcdExists_95 = 0;
         nIsMod_95 = 0;
         nRcdDeleted_95 = 0;
      }

      protected void ProcessLevel1L6( )
      {
         /* Save parent mode. */
         sMode6 = Gx_mode;
         ProcessNestedLevel1L95( ) ;
         if ( AnyError != 0 )
         {
         }
         /* Restore parent mode. */
         Gx_mode = sMode6;
         AssignAttri("", false, "Gx_mode", Gx_mode);
         /* ' Update level parameters */
      }

      protected void EndLevel1L6( )
      {
         if ( ! IsIns( ) )
         {
            pr_default.close(2);
         }
         if ( AnyError == 0 )
         {
            BeforeComplete1L6( ) ;
         }
         if ( AnyError == 0 )
         {
            context.CommitDataStores("trn_appversion",pr_default);
            if ( AnyError == 0 )
            {
               ConfirmValues1L0( ) ;
            }
            /* After transaction rules */
            /* Execute 'After Trn' event if defined. */
            trnEnded = 1;
         }
         else
         {
            context.RollbackDataStores("trn_appversion",pr_default);
         }
         IsModified = 0;
         if ( AnyError != 0 )
         {
            context.wjLoc = "";
            context.nUserReturn = 0;
         }
      }

      public void ScanStart1L6( )
      {
         /* Scan By routine */
         /* Using cursor T001L19 */
         pr_default.execute(17);
         RcdFound6 = 0;
         if ( (pr_default.getStatus(17) != 101) )
         {
            RcdFound6 = 1;
            A523AppVersionId = T001L19_A523AppVersionId[0];
            AssignAttri("", false, "A523AppVersionId", A523AppVersionId.ToString());
         }
         /* Load Subordinate Levels */
      }

      protected void ScanNext1L6( )
      {
         /* Scan next routine */
         pr_default.readNext(17);
         RcdFound6 = 0;
         if ( (pr_default.getStatus(17) != 101) )
         {
            RcdFound6 = 1;
            A523AppVersionId = T001L19_A523AppVersionId[0];
            AssignAttri("", false, "A523AppVersionId", A523AppVersionId.ToString());
         }
      }

      protected void ScanEnd1L6( )
      {
         pr_default.close(17);
      }

      protected void AfterConfirm1L6( )
      {
         /* After Confirm Rules */
      }

      protected void BeforeInsert1L6( )
      {
         /* Before Insert Rules */
      }

      protected void BeforeUpdate1L6( )
      {
         /* Before Update Rules */
      }

      protected void BeforeDelete1L6( )
      {
         /* Before Delete Rules */
      }

      protected void BeforeComplete1L6( )
      {
         /* Before Complete Rules */
      }

      protected void BeforeValidate1L6( )
      {
         /* Before Validate Rules */
      }

      protected void DisableAttributes1L6( )
      {
         edtAppVersionId_Enabled = 0;
         AssignProp("", false, edtAppVersionId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtAppVersionId_Enabled), 5, 0), true);
         edtAppVersionName_Enabled = 0;
         AssignProp("", false, edtAppVersionName_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtAppVersionName_Enabled), 5, 0), true);
         edtLocationId_Enabled = 0;
         AssignProp("", false, edtLocationId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtLocationId_Enabled), 5, 0), true);
         edtOrganisationId_Enabled = 0;
         AssignProp("", false, edtOrganisationId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtOrganisationId_Enabled), 5, 0), true);
         chkIsActive.Enabled = 0;
         AssignProp("", false, chkIsActive_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkIsActive.Enabled), 5, 0), true);
         edtavCombolocationid_Enabled = 0;
         AssignProp("", false, edtavCombolocationid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCombolocationid_Enabled), 5, 0), true);
         edtavComboorganisationid_Enabled = 0;
         AssignProp("", false, edtavComboorganisationid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavComboorganisationid_Enabled), 5, 0), true);
      }

      protected void ZM1L95( short GX_JID )
      {
         if ( ( GX_JID == 22 ) || ( GX_JID == 0 ) )
         {
            if ( ! IsIns( ) )
            {
               Z541IsPredefined = T001L3_A541IsPredefined[0];
               Z517PageName = T001L3_A517PageName[0];
               Z525PageType = T001L3_A525PageType[0];
            }
            else
            {
               Z541IsPredefined = A541IsPredefined;
               Z517PageName = A517PageName;
               Z525PageType = A525PageType;
            }
         }
         if ( GX_JID == -22 )
         {
            Z523AppVersionId = A523AppVersionId;
            Z516PageId = A516PageId;
            Z541IsPredefined = A541IsPredefined;
            Z517PageName = A517PageName;
            Z518PageStructure = A518PageStructure;
            Z536PagePublishedStructure = A536PagePublishedStructure;
            Z599PageThumbnail = A599PageThumbnail;
            Z40000PageThumbnail_GXI = A40000PageThumbnail_GXI;
            Z525PageType = A525PageType;
         }
      }

      protected void standaloneNotModal1L95( )
      {
      }

      protected void standaloneModal1L95( )
      {
         if ( IsIns( )  && (Guid.Empty==A516PageId) && ( Gx_BScreen == 0 ) )
         {
            A516PageId = Guid.NewGuid( );
         }
         if ( IsIns( )  && (false==A541IsPredefined) && ( Gx_BScreen == 0 ) )
         {
            A541IsPredefined = false;
         }
         if ( StringUtil.StrCmp(Gx_mode, "INS") != 0 )
         {
            edtPageId_Enabled = 0;
            AssignProp("", false, edtPageId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtPageId_Enabled), 5, 0), !bGXsfl_59_Refreshing);
         }
         else
         {
            edtPageId_Enabled = 1;
            AssignProp("", false, edtPageId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtPageId_Enabled), 5, 0), !bGXsfl_59_Refreshing);
         }
         if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) && ( Gx_BScreen == 0 ) )
         {
         }
      }

      protected void Load1L95( )
      {
         /* Using cursor T001L20 */
         pr_default.execute(18, new Object[] {A523AppVersionId, A516PageId});
         if ( (pr_default.getStatus(18) != 101) )
         {
            RcdFound95 = 1;
            A541IsPredefined = T001L20_A541IsPredefined[0];
            A517PageName = T001L20_A517PageName[0];
            A518PageStructure = T001L20_A518PageStructure[0];
            A536PagePublishedStructure = T001L20_A536PagePublishedStructure[0];
            A40000PageThumbnail_GXI = T001L20_A40000PageThumbnail_GXI[0];
            n40000PageThumbnail_GXI = T001L20_n40000PageThumbnail_GXI[0];
            A525PageType = T001L20_A525PageType[0];
            A599PageThumbnail = T001L20_A599PageThumbnail[0];
            n599PageThumbnail = T001L20_n599PageThumbnail[0];
            ZM1L95( -22) ;
         }
         pr_default.close(18);
         OnLoadActions1L95( ) ;
      }

      protected void OnLoadActions1L95( )
      {
      }

      protected void CheckExtendedTable1L95( )
      {
         nIsDirty_95 = 0;
         Gx_BScreen = 1;
         AssignAttri("", false, "Gx_BScreen", StringUtil.Str( (decimal)(Gx_BScreen), 1, 0));
         standaloneModal1L95( ) ;
         if ( ! ( ( StringUtil.StrCmp(A525PageType, "Menu") == 0 ) || ( StringUtil.StrCmp(A525PageType, "Content") == 0 ) || ( StringUtil.StrCmp(A525PageType, "WebLink") == 0 ) || ( StringUtil.StrCmp(A525PageType, "DynamicForm") == 0 ) || ( StringUtil.StrCmp(A525PageType, "Calendar") == 0 ) || ( StringUtil.StrCmp(A525PageType, "MyActivity") == 0 ) || ( StringUtil.StrCmp(A525PageType, "Map") == 0 ) || ( StringUtil.StrCmp(A525PageType, "Reception") == 0 ) || ( StringUtil.StrCmp(A525PageType, "Location") == 0 ) || ( StringUtil.StrCmp(A525PageType, "MyCare") == 0 ) || ( StringUtil.StrCmp(A525PageType, "MyLiving") == 0 ) || ( StringUtil.StrCmp(A525PageType, "MyService") == 0 ) || ( StringUtil.StrCmp(A525PageType, "Information") == 0 ) ) )
         {
            GXCCtl = "PAGETYPE_" + sGXsfl_59_idx;
            GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXSPC_OutOfRange", ""), context.GetMessage( "Page Type", ""), "", "", "", "", "", "", "", ""), "OutOfRange", 1, GXCCtl);
            AnyError = 1;
            GX_FocusControl = cmbPageType_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
      }

      protected void CloseExtendedTableCursors1L95( )
      {
      }

      protected void enableDisable1L95( )
      {
      }

      protected void GetKey1L95( )
      {
         /* Using cursor T001L21 */
         pr_default.execute(19, new Object[] {A523AppVersionId, A516PageId});
         if ( (pr_default.getStatus(19) != 101) )
         {
            RcdFound95 = 1;
         }
         else
         {
            RcdFound95 = 0;
         }
         pr_default.close(19);
      }

      protected void getByPrimaryKey1L95( )
      {
         /* Using cursor T001L3 */
         pr_default.execute(1, new Object[] {A523AppVersionId, A516PageId});
         if ( (pr_default.getStatus(1) != 101) )
         {
            ZM1L95( 22) ;
            RcdFound95 = 1;
            InitializeNonKey1L95( ) ;
            A516PageId = T001L3_A516PageId[0];
            A541IsPredefined = T001L3_A541IsPredefined[0];
            A517PageName = T001L3_A517PageName[0];
            A518PageStructure = T001L3_A518PageStructure[0];
            A536PagePublishedStructure = T001L3_A536PagePublishedStructure[0];
            A40000PageThumbnail_GXI = T001L3_A40000PageThumbnail_GXI[0];
            n40000PageThumbnail_GXI = T001L3_n40000PageThumbnail_GXI[0];
            A525PageType = T001L3_A525PageType[0];
            A599PageThumbnail = T001L3_A599PageThumbnail[0];
            n599PageThumbnail = T001L3_n599PageThumbnail[0];
            Z523AppVersionId = A523AppVersionId;
            Z516PageId = A516PageId;
            sMode95 = Gx_mode;
            Gx_mode = "DSP";
            AssignAttri("", false, "Gx_mode", Gx_mode);
            Load1L95( ) ;
            Gx_mode = sMode95;
            AssignAttri("", false, "Gx_mode", Gx_mode);
         }
         else
         {
            RcdFound95 = 0;
            InitializeNonKey1L95( ) ;
            sMode95 = Gx_mode;
            Gx_mode = "DSP";
            AssignAttri("", false, "Gx_mode", Gx_mode);
            standaloneModal1L95( ) ;
            Gx_mode = sMode95;
            AssignAttri("", false, "Gx_mode", Gx_mode);
         }
         if ( IsDsp( ) || IsDlt( ) )
         {
            DisableAttributes1L95( ) ;
         }
         pr_default.close(1);
      }

      protected void CheckOptimisticConcurrency1L95( )
      {
         if ( ! IsIns( ) )
         {
            /* Using cursor T001L2 */
            pr_default.execute(0, new Object[] {A523AppVersionId, A516PageId});
            if ( (pr_default.getStatus(0) == 103) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"Trn_AppVersionPage"}), "RecordIsLocked", 1, "");
               AnyError = 1;
               return  ;
            }
            if ( (pr_default.getStatus(0) == 101) || ( Z541IsPredefined != T001L2_A541IsPredefined[0] ) || ( StringUtil.StrCmp(Z517PageName, T001L2_A517PageName[0]) != 0 ) || ( StringUtil.StrCmp(Z525PageType, T001L2_A525PageType[0]) != 0 ) )
            {
               if ( Z541IsPredefined != T001L2_A541IsPredefined[0] )
               {
                  GXUtil.WriteLog("trn_appversion:[seudo value changed for attri]"+"IsPredefined");
                  GXUtil.WriteLogRaw("Old: ",Z541IsPredefined);
                  GXUtil.WriteLogRaw("Current: ",T001L2_A541IsPredefined[0]);
               }
               if ( StringUtil.StrCmp(Z517PageName, T001L2_A517PageName[0]) != 0 )
               {
                  GXUtil.WriteLog("trn_appversion:[seudo value changed for attri]"+"PageName");
                  GXUtil.WriteLogRaw("Old: ",Z517PageName);
                  GXUtil.WriteLogRaw("Current: ",T001L2_A517PageName[0]);
               }
               if ( StringUtil.StrCmp(Z525PageType, T001L2_A525PageType[0]) != 0 )
               {
                  GXUtil.WriteLog("trn_appversion:[seudo value changed for attri]"+"PageType");
                  GXUtil.WriteLogRaw("Old: ",Z525PageType);
                  GXUtil.WriteLogRaw("Current: ",T001L2_A525PageType[0]);
               }
               GX_msglist.addItem(context.GetMessage( "GXM_waschg", new   object[]  {"Trn_AppVersionPage"}), "RecordWasChanged", 1, "");
               AnyError = 1;
               return  ;
            }
         }
      }

      protected void Insert1L95( )
      {
         if ( ! IsAuthorized("trn_appversion_Insert") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         BeforeValidate1L95( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable1L95( ) ;
         }
         if ( AnyError == 0 )
         {
            ZM1L95( 0) ;
            CheckOptimisticConcurrency1L95( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm1L95( ) ;
               if ( AnyError == 0 )
               {
                  BeforeInsert1L95( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor T001L22 */
                     pr_default.execute(20, new Object[] {A523AppVersionId, A516PageId, A541IsPredefined, A517PageName, A518PageStructure, A536PagePublishedStructure, n599PageThumbnail, A599PageThumbnail, n40000PageThumbnail_GXI, A40000PageThumbnail_GXI, A525PageType});
                     pr_default.close(20);
                     pr_default.SmartCacheProvider.SetUpdated("Trn_AppVersionPage");
                     if ( (pr_default.getStatus(20) == 1) )
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
               Load1L95( ) ;
            }
            EndLevel1L95( ) ;
         }
         CloseExtendedTableCursors1L95( ) ;
      }

      protected void Update1L95( )
      {
         if ( ! IsAuthorized("trn_appversion_Update") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         BeforeValidate1L95( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable1L95( ) ;
         }
         if ( ( nIsMod_95 != 0 ) || ( nIsDirty_95 != 0 ) )
         {
            if ( AnyError == 0 )
            {
               CheckOptimisticConcurrency1L95( ) ;
               if ( AnyError == 0 )
               {
                  AfterConfirm1L95( ) ;
                  if ( AnyError == 0 )
                  {
                     BeforeUpdate1L95( ) ;
                     if ( AnyError == 0 )
                     {
                        /* Using cursor T001L23 */
                        pr_default.execute(21, new Object[] {A541IsPredefined, A517PageName, A518PageStructure, A536PagePublishedStructure, A525PageType, A523AppVersionId, A516PageId});
                        pr_default.close(21);
                        pr_default.SmartCacheProvider.SetUpdated("Trn_AppVersionPage");
                        if ( (pr_default.getStatus(21) == 103) )
                        {
                           GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"Trn_AppVersionPage"}), "RecordIsLocked", 1, "");
                           AnyError = 1;
                        }
                        DeferredUpdate1L95( ) ;
                        if ( AnyError == 0 )
                        {
                           /* Start of After( update) rules */
                           /* End of After( update) rules */
                           if ( AnyError == 0 )
                           {
                              getByPrimaryKey1L95( ) ;
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
               EndLevel1L95( ) ;
            }
         }
         CloseExtendedTableCursors1L95( ) ;
      }

      protected void DeferredUpdate1L95( )
      {
         if ( AnyError == 0 )
         {
            /* Using cursor T001L24 */
            pr_default.execute(22, new Object[] {n599PageThumbnail, A599PageThumbnail, n40000PageThumbnail_GXI, A40000PageThumbnail_GXI, A523AppVersionId, A516PageId});
            pr_default.close(22);
            pr_default.SmartCacheProvider.SetUpdated("Trn_AppVersionPage");
         }
      }

      protected void Delete1L95( )
      {
         if ( ! IsAuthorized("trn_appversion_Delete") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         Gx_mode = "DLT";
         AssignAttri("", false, "Gx_mode", Gx_mode);
         BeforeValidate1L95( ) ;
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency1L95( ) ;
         }
         if ( AnyError == 0 )
         {
            OnDeleteControls1L95( ) ;
            AfterConfirm1L95( ) ;
            if ( AnyError == 0 )
            {
               BeforeDelete1L95( ) ;
               if ( AnyError == 0 )
               {
                  /* No cascading delete specified. */
                  /* Using cursor T001L25 */
                  pr_default.execute(23, new Object[] {A523AppVersionId, A516PageId});
                  pr_default.close(23);
                  pr_default.SmartCacheProvider.SetUpdated("Trn_AppVersionPage");
                  if ( AnyError == 0 )
                  {
                     /* Start of After( delete) rules */
                     /* End of After( delete) rules */
                  }
                  else
                  {
                     GX_msglist.addItem(context.GetMessage( "GXM_unexp", ""), 1, "");
                     AnyError = 1;
                  }
               }
            }
         }
         sMode95 = Gx_mode;
         Gx_mode = "DLT";
         AssignAttri("", false, "Gx_mode", Gx_mode);
         EndLevel1L95( ) ;
         Gx_mode = sMode95;
         AssignAttri("", false, "Gx_mode", Gx_mode);
      }

      protected void OnDeleteControls1L95( )
      {
         standaloneModal1L95( ) ;
         /* No delete mode formulas found. */
      }

      protected void EndLevel1L95( )
      {
         if ( ! IsIns( ) )
         {
            pr_default.close(0);
         }
         if ( AnyError != 0 )
         {
            context.wjLoc = "";
            context.nUserReturn = 0;
         }
      }

      public void ScanStart1L95( )
      {
         /* Scan By routine */
         /* Using cursor T001L26 */
         pr_default.execute(24, new Object[] {A523AppVersionId});
         RcdFound95 = 0;
         if ( (pr_default.getStatus(24) != 101) )
         {
            RcdFound95 = 1;
            A516PageId = T001L26_A516PageId[0];
         }
         /* Load Subordinate Levels */
      }

      protected void ScanNext1L95( )
      {
         /* Scan next routine */
         pr_default.readNext(24);
         RcdFound95 = 0;
         if ( (pr_default.getStatus(24) != 101) )
         {
            RcdFound95 = 1;
            A516PageId = T001L26_A516PageId[0];
         }
      }

      protected void ScanEnd1L95( )
      {
         pr_default.close(24);
      }

      protected void AfterConfirm1L95( )
      {
         /* After Confirm Rules */
      }

      protected void BeforeInsert1L95( )
      {
         /* Before Insert Rules */
      }

      protected void BeforeUpdate1L95( )
      {
         /* Before Update Rules */
      }

      protected void BeforeDelete1L95( )
      {
         /* Before Delete Rules */
      }

      protected void BeforeComplete1L95( )
      {
         /* Before Complete Rules */
      }

      protected void BeforeValidate1L95( )
      {
         /* Before Validate Rules */
      }

      protected void DisableAttributes1L95( )
      {
         edtPageId_Enabled = 0;
         AssignProp("", false, edtPageId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtPageId_Enabled), 5, 0), !bGXsfl_59_Refreshing);
         edtPageName_Enabled = 0;
         AssignProp("", false, edtPageName_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtPageName_Enabled), 5, 0), !bGXsfl_59_Refreshing);
         edtPageStructure_Enabled = 0;
         AssignProp("", false, edtPageStructure_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtPageStructure_Enabled), 5, 0), !bGXsfl_59_Refreshing);
         edtPagePublishedStructure_Enabled = 0;
         AssignProp("", false, edtPagePublishedStructure_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtPagePublishedStructure_Enabled), 5, 0), !bGXsfl_59_Refreshing);
         chkIsPredefined.Enabled = 0;
         AssignProp("", false, chkIsPredefined_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkIsPredefined.Enabled), 5, 0), !bGXsfl_59_Refreshing);
         cmbPageType.Enabled = 0;
         AssignProp("", false, cmbPageType_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbPageType.Enabled), 5, 0), !bGXsfl_59_Refreshing);
      }

      protected void send_integrity_lvl_hashes1L95( )
      {
      }

      protected void send_integrity_lvl_hashes1L6( )
      {
      }

      protected void SubsflControlProps_5995( )
      {
         edtPageId_Internalname = "PAGEID_"+sGXsfl_59_idx;
         edtPageName_Internalname = "PAGENAME_"+sGXsfl_59_idx;
         edtPageStructure_Internalname = "PAGESTRUCTURE_"+sGXsfl_59_idx;
         edtPagePublishedStructure_Internalname = "PAGEPUBLISHEDSTRUCTURE_"+sGXsfl_59_idx;
         chkIsPredefined_Internalname = "ISPREDEFINED_"+sGXsfl_59_idx;
         cmbPageType_Internalname = "PAGETYPE_"+sGXsfl_59_idx;
      }

      protected void SubsflControlProps_fel_5995( )
      {
         edtPageId_Internalname = "PAGEID_"+sGXsfl_59_fel_idx;
         edtPageName_Internalname = "PAGENAME_"+sGXsfl_59_fel_idx;
         edtPageStructure_Internalname = "PAGESTRUCTURE_"+sGXsfl_59_fel_idx;
         edtPagePublishedStructure_Internalname = "PAGEPUBLISHEDSTRUCTURE_"+sGXsfl_59_fel_idx;
         chkIsPredefined_Internalname = "ISPREDEFINED_"+sGXsfl_59_fel_idx;
         cmbPageType_Internalname = "PAGETYPE_"+sGXsfl_59_fel_idx;
      }

      protected void AddRow1L95( )
      {
         nGXsfl_59_idx = (int)(nGXsfl_59_idx+1);
         sGXsfl_59_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_59_idx), 4, 0), 4, "0");
         SubsflControlProps_5995( ) ;
         SendRow1L95( ) ;
      }

      protected void SendRow1L95( )
      {
         Gridlevel_pageRow = GXWebRow.GetNew(context);
         if ( subGridlevel_page_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGridlevel_page_Backstyle = 0;
            if ( StringUtil.StrCmp(subGridlevel_page_Class, "") != 0 )
            {
               subGridlevel_page_Linesclass = subGridlevel_page_Class+"Odd";
            }
         }
         else if ( subGridlevel_page_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGridlevel_page_Backstyle = 0;
            subGridlevel_page_Backcolor = subGridlevel_page_Allbackcolor;
            if ( StringUtil.StrCmp(subGridlevel_page_Class, "") != 0 )
            {
               subGridlevel_page_Linesclass = subGridlevel_page_Class+"Uniform";
            }
         }
         else if ( subGridlevel_page_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGridlevel_page_Backstyle = 1;
            if ( StringUtil.StrCmp(subGridlevel_page_Class, "") != 0 )
            {
               subGridlevel_page_Linesclass = subGridlevel_page_Class+"Odd";
            }
            subGridlevel_page_Backcolor = (int)(0x0);
         }
         else if ( subGridlevel_page_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGridlevel_page_Backstyle = 1;
            if ( ((int)((nGXsfl_59_idx) % (2))) == 0 )
            {
               subGridlevel_page_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridlevel_page_Class, "") != 0 )
               {
                  subGridlevel_page_Linesclass = subGridlevel_page_Class+"Even";
               }
            }
            else
            {
               subGridlevel_page_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridlevel_page_Class, "") != 0 )
               {
                  subGridlevel_page_Linesclass = subGridlevel_page_Class+"Odd";
               }
            }
         }
         /* Subfile cell */
         /* Single line edit */
         TempTags = " data-gxoch1=\"gx.fn.setControlValue('nIsMod_95_" + sGXsfl_59_idx + "',1);\"  onfocus=\"gx.evt.onfocus(this, 60,'',false,'" + sGXsfl_59_idx + "',59)\"";
         ROClassString = "Attribute";
         Gridlevel_pageRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtPageId_Internalname,A516PageId.ToString(),A516PageId.ToString(),TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,60);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtPageId_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"TrnColumn",(string)"",(short)-1,(int)edtPageId_Enabled,(short)1,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)36,(short)0,(short)0,(short)59,(short)0,(short)0,(short)0,(bool)true,(string)"Id",(string)"",(bool)false,(string)""});
         /* Subfile cell */
         /* Single line edit */
         TempTags = " data-gxoch1=\"gx.fn.setControlValue('nIsMod_95_" + sGXsfl_59_idx + "',1);\"  onfocus=\"gx.evt.onfocus(this, 61,'',false,'" + sGXsfl_59_idx + "',59)\"";
         ROClassString = "Attribute";
         Gridlevel_pageRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtPageName_Internalname,(string)A517PageName,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,61);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtPageName_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"TrnColumn",(string)"",(short)-1,(int)edtPageName_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)100,(short)0,(short)0,(short)59,(short)0,(short)-1,(short)-1,(bool)true,(string)"Name",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         /* Single line edit */
         TempTags = " data-gxoch1=\"gx.fn.setControlValue('nIsMod_95_" + sGXsfl_59_idx + "',1);\"  onfocus=\"gx.evt.onfocus(this, 62,'',false,'" + sGXsfl_59_idx + "',59)\"";
         ROClassString = "Attribute";
         Gridlevel_pageRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtPageStructure_Internalname,(string)A518PageStructure,(string)A518PageStructure,TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,62);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtPageStructure_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"TrnColumn",(string)"",(short)-1,(int)edtPageStructure_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(int)2097152,(short)0,(short)0,(short)59,(short)0,(short)0,(short)-1,(bool)true,(string)"",(string)"start",(bool)false,(string)""});
         /* Subfile cell */
         /* Single line edit */
         TempTags = " data-gxoch1=\"gx.fn.setControlValue('nIsMod_95_" + sGXsfl_59_idx + "',1);\"  onfocus=\"gx.evt.onfocus(this, 63,'',false,'" + sGXsfl_59_idx + "',59)\"";
         ROClassString = "Attribute";
         Gridlevel_pageRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtPagePublishedStructure_Internalname,(string)A536PagePublishedStructure,(string)A536PagePublishedStructure,TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,63);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtPagePublishedStructure_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"TrnColumn",(string)"",(short)-1,(int)edtPagePublishedStructure_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(int)2097152,(short)0,(short)0,(short)59,(short)0,(short)0,(short)-1,(bool)true,(string)"",(string)"start",(bool)false,(string)""});
         /* Subfile cell */
         /* Check box */
         TempTags = " data-gxoch1=\"gx.fn.setControlValue('nIsMod_95_" + sGXsfl_59_idx + "',1);\"  onfocus=\"gx.evt.onfocus(this, 64,'',false,'" + sGXsfl_59_idx + "',59)\"";
         ClassString = "AttributeCheckBox";
         StyleString = "";
         GXCCtl = "ISPREDEFINED_" + sGXsfl_59_idx;
         chkIsPredefined.Name = GXCCtl;
         chkIsPredefined.WebTags = "";
         chkIsPredefined.Caption = "";
         AssignProp("", false, chkIsPredefined_Internalname, "TitleCaption", chkIsPredefined.Caption, !bGXsfl_59_Refreshing);
         chkIsPredefined.CheckedValue = "false";
         if ( IsIns( ) && (false==A541IsPredefined) )
         {
            A541IsPredefined = false;
         }
         Gridlevel_pageRow.AddColumnProperties("checkbox", 1, isAjaxCallMode( ), new Object[] {(string)chkIsPredefined_Internalname,StringUtil.BoolToStr( A541IsPredefined),(string)"",(string)"",(short)-1,chkIsPredefined.Enabled,(string)"true",(string)"",(string)StyleString,(string)ClassString,(string)"TrnColumn",(string)"",TempTags+" onclick="+"\"gx.fn.checkboxClick(64, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,64);\""});
         /* Subfile cell */
         TempTags = " data-gxoch1=\"gx.fn.setControlValue('nIsMod_95_" + sGXsfl_59_idx + "',1);\"  onfocus=\"gx.evt.onfocus(this, 65,'',false,'" + sGXsfl_59_idx + "',59)\"";
         if ( ( cmbPageType.ItemCount == 0 ) && isAjaxCallMode( ) )
         {
            GXCCtl = "PAGETYPE_" + sGXsfl_59_idx;
            cmbPageType.Name = GXCCtl;
            cmbPageType.WebTags = "";
            cmbPageType.addItem("Menu", context.GetMessage( "Menu", ""), 0);
            cmbPageType.addItem("Content", context.GetMessage( "Content", ""), 0);
            cmbPageType.addItem("WebLink", context.GetMessage( "Web Link", ""), 0);
            cmbPageType.addItem("DynamicForm", context.GetMessage( "Dynamic Form", ""), 0);
            cmbPageType.addItem("Calendar", context.GetMessage( "Calendar", ""), 0);
            cmbPageType.addItem("MyActivity", context.GetMessage( "My Activity", ""), 0);
            cmbPageType.addItem("Map", context.GetMessage( "Map", ""), 0);
            cmbPageType.addItem("Reception", context.GetMessage( "Reception", ""), 0);
            cmbPageType.addItem("Location", context.GetMessage( "Location", ""), 0);
            cmbPageType.addItem("MyCare", context.GetMessage( "My Care", ""), 0);
            cmbPageType.addItem("MyLiving", context.GetMessage( "My Living", ""), 0);
            cmbPageType.addItem("MyService", context.GetMessage( "My Service", ""), 0);
            cmbPageType.addItem("Information", context.GetMessage( "Information", ""), 0);
            if ( cmbPageType.ItemCount > 0 )
            {
               A525PageType = cmbPageType.getValidValue(A525PageType);
            }
         }
         /* ComboBox */
         Gridlevel_pageRow.AddColumnProperties("combobox", 2, isAjaxCallMode( ), new Object[] {(GXCombobox)cmbPageType,(string)cmbPageType_Internalname,StringUtil.RTrim( A525PageType),(short)1,(string)cmbPageType_Jsonclick,(short)0,(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"svchar",(string)"",(short)-1,cmbPageType.Enabled,(short)0,(short)0,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"Attribute",(string)"TrnColumn",(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,65);\"",(string)"",(bool)true,(short)0});
         cmbPageType.CurrentValue = StringUtil.RTrim( A525PageType);
         AssignProp("", false, cmbPageType_Internalname, "Values", (string)(cmbPageType.ToJavascriptSource()), !bGXsfl_59_Refreshing);
         ajax_sending_grid_row(Gridlevel_pageRow);
         send_integrity_lvl_hashes1L95( ) ;
         GXCCtl = "Z516PageId_" + sGXsfl_59_idx;
         GxWebStd.gx_hidden_field( context, GXCCtl, Z516PageId.ToString());
         GXCCtl = "Z541IsPredefined_" + sGXsfl_59_idx;
         GxWebStd.gx_boolean_hidden_field( context, GXCCtl, Z541IsPredefined);
         GXCCtl = "Z517PageName_" + sGXsfl_59_idx;
         GxWebStd.gx_hidden_field( context, GXCCtl, Z517PageName);
         GXCCtl = "Z525PageType_" + sGXsfl_59_idx;
         GxWebStd.gx_hidden_field( context, GXCCtl, Z525PageType);
         GXCCtl = "nRcdDeleted_95_" + sGXsfl_59_idx;
         GxWebStd.gx_hidden_field( context, GXCCtl, StringUtil.LTrim( StringUtil.NToC( (decimal)(nRcdDeleted_95), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GXCCtl = "nRcdExists_95_" + sGXsfl_59_idx;
         GxWebStd.gx_hidden_field( context, GXCCtl, StringUtil.LTrim( StringUtil.NToC( (decimal)(nRcdExists_95), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GXCCtl = "nIsMod_95_" + sGXsfl_59_idx;
         GxWebStd.gx_hidden_field( context, GXCCtl, StringUtil.LTrim( StringUtil.NToC( (decimal)(nIsMod_95), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GXCCtl = "vMODE_" + sGXsfl_59_idx;
         GxWebStd.gx_hidden_field( context, GXCCtl, StringUtil.RTrim( Gx_mode));
         GXCCtl = "vTRNCONTEXT_" + sGXsfl_59_idx;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, GXCCtl, AV12TrnContext);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(GXCCtl, AV12TrnContext);
         }
         GXCCtl = "vAPPVERSIONID_" + sGXsfl_59_idx;
         GxWebStd.gx_hidden_field( context, GXCCtl, AV8AppVersionId.ToString());
         GxWebStd.gx_hidden_field( context, "PAGEID_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtPageId_Enabled), 5, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "PAGENAME_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtPageName_Enabled), 5, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "PAGESTRUCTURE_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtPageStructure_Enabled), 5, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "PAGEPUBLISHEDSTRUCTURE_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtPagePublishedStructure_Enabled), 5, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "ISPREDEFINED_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(chkIsPredefined.Enabled), 5, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "PAGETYPE_"+sGXsfl_59_idx+"Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(cmbPageType.Enabled), 5, 0, ".", "")));
         ajax_sending_grid_row(null);
         Gridlevel_pageContainer.AddRow(Gridlevel_pageRow);
      }

      protected void ReadRow1L95( )
      {
         nGXsfl_59_idx = (int)(nGXsfl_59_idx+1);
         sGXsfl_59_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_59_idx), 4, 0), 4, "0");
         SubsflControlProps_5995( ) ;
         edtPageId_Enabled = (int)(Math.Round(context.localUtil.CToN( cgiGet( "PAGEID_"+sGXsfl_59_idx+"Enabled"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
         edtPageName_Enabled = (int)(Math.Round(context.localUtil.CToN( cgiGet( "PAGENAME_"+sGXsfl_59_idx+"Enabled"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
         edtPageStructure_Enabled = (int)(Math.Round(context.localUtil.CToN( cgiGet( "PAGESTRUCTURE_"+sGXsfl_59_idx+"Enabled"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
         edtPagePublishedStructure_Enabled = (int)(Math.Round(context.localUtil.CToN( cgiGet( "PAGEPUBLISHEDSTRUCTURE_"+sGXsfl_59_idx+"Enabled"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
         chkIsPredefined.Enabled = (int)(Math.Round(context.localUtil.CToN( cgiGet( "ISPREDEFINED_"+sGXsfl_59_idx+"Enabled"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
         cmbPageType.Enabled = (int)(Math.Round(context.localUtil.CToN( cgiGet( "PAGETYPE_"+sGXsfl_59_idx+"Enabled"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
         if ( StringUtil.StrCmp(cgiGet( edtPageId_Internalname), "") == 0 )
         {
            A516PageId = Guid.Empty;
         }
         else
         {
            try
            {
               A516PageId = StringUtil.StrToGuid( cgiGet( edtPageId_Internalname));
            }
            catch ( Exception  )
            {
               GXCCtl = "PAGEID_" + sGXsfl_59_idx;
               GX_msglist.addItem(context.GetMessage( "GXM_invalidguid", ""), 1, GXCCtl);
               AnyError = 1;
               GX_FocusControl = edtPageId_Internalname;
               wbErr = true;
            }
         }
         A517PageName = cgiGet( edtPageName_Internalname);
         A518PageStructure = cgiGet( edtPageStructure_Internalname);
         A536PagePublishedStructure = cgiGet( edtPagePublishedStructure_Internalname);
         A541IsPredefined = StringUtil.StrToBool( cgiGet( chkIsPredefined_Internalname));
         cmbPageType.Name = cmbPageType_Internalname;
         cmbPageType.CurrentValue = cgiGet( cmbPageType_Internalname);
         A525PageType = cgiGet( cmbPageType_Internalname);
         GXCCtl = "Z516PageId_" + sGXsfl_59_idx;
         Z516PageId = StringUtil.StrToGuid( cgiGet( GXCCtl));
         GXCCtl = "Z541IsPredefined_" + sGXsfl_59_idx;
         Z541IsPredefined = StringUtil.StrToBool( cgiGet( GXCCtl));
         GXCCtl = "Z517PageName_" + sGXsfl_59_idx;
         Z517PageName = cgiGet( GXCCtl);
         GXCCtl = "Z525PageType_" + sGXsfl_59_idx;
         Z525PageType = cgiGet( GXCCtl);
         GXCCtl = "nRcdDeleted_95_" + sGXsfl_59_idx;
         nRcdDeleted_95 = (short)(Math.Round(context.localUtil.CToN( cgiGet( GXCCtl), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
         GXCCtl = "nRcdExists_95_" + sGXsfl_59_idx;
         nRcdExists_95 = (short)(Math.Round(context.localUtil.CToN( cgiGet( GXCCtl), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
         GXCCtl = "nIsMod_95_" + sGXsfl_59_idx;
         nIsMod_95 = (short)(Math.Round(context.localUtil.CToN( cgiGet( GXCCtl), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
      }

      protected void assign_properties_default( )
      {
         defedtPageId_Enabled = edtPageId_Enabled;
      }

      protected void ConfirmValues1L0( )
      {
         nGXsfl_59_idx = 0;
         sGXsfl_59_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_59_idx), 4, 0), 4, "0");
         SubsflControlProps_5995( ) ;
         while ( nGXsfl_59_idx < nRC_GXsfl_59 )
         {
            nGXsfl_59_idx = (int)(nGXsfl_59_idx+1);
            sGXsfl_59_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_59_idx), 4, 0), 4, "0");
            SubsflControlProps_5995( ) ;
            ChangePostValue( "Z516PageId_"+sGXsfl_59_idx, cgiGet( "ZT_"+"Z516PageId_"+sGXsfl_59_idx)) ;
            DeletePostValue( "ZT_"+"Z516PageId_"+sGXsfl_59_idx) ;
            ChangePostValue( "Z541IsPredefined_"+sGXsfl_59_idx, cgiGet( "ZT_"+"Z541IsPredefined_"+sGXsfl_59_idx)) ;
            DeletePostValue( "ZT_"+"Z541IsPredefined_"+sGXsfl_59_idx) ;
            ChangePostValue( "Z517PageName_"+sGXsfl_59_idx, cgiGet( "ZT_"+"Z517PageName_"+sGXsfl_59_idx)) ;
            DeletePostValue( "ZT_"+"Z517PageName_"+sGXsfl_59_idx) ;
            ChangePostValue( "Z525PageType_"+sGXsfl_59_idx, cgiGet( "ZT_"+"Z525PageType_"+sGXsfl_59_idx)) ;
            DeletePostValue( "ZT_"+"Z525PageType_"+sGXsfl_59_idx) ;
         }
      }

      public override void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      public override void RenderHtmlOpenForm( )
      {
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.WriteHtmlText( "<title>") ;
         context.SendWebValue( Form.Caption) ;
         context.WriteHtmlTextNl( "</title>") ;
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         if ( StringUtil.Len( sDynURL) > 0 )
         {
            context.WriteHtmlText( "<BASE href=\""+sDynURL+"\" />") ;
         }
         define_styles( ) ;
         MasterPageObj.master_styles();
         CloseStyles();
         if ( ( ( context.GetBrowserType( ) == 1 ) || ( context.GetBrowserType( ) == 5 ) ) && ( StringUtil.StrCmp(context.GetBrowserVersion( ), "7.0") == 0 ) )
         {
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1918140), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1918140), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1918140), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.WriteHtmlText( Form.Headerrawhtml) ;
         context.CloseHtmlHeader();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         FormProcess = " data-HasEnter=\"true\" data-Skiponenter=\"false\"";
         context.WriteHtmlText( "<body ") ;
         if ( StringUtil.StrCmp(context.GetLanguageProperty( "rtl"), "true") == 0 )
         {
            context.WriteHtmlText( " dir=\"rtl\" ") ;
         }
         bodyStyle = "" + "background-color:" + context.BuildHTMLColor( Form.Backcolor) + ";color:" + context.BuildHTMLColor( Form.Textcolor) + ";";
         bodyStyle += "-moz-opacity:0;opacity:0;";
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( Form.Background)) ) )
         {
            bodyStyle += " background-image:url(" + context.convertURL( Form.Background) + ")";
         }
         context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
         context.WriteHtmlText( FormProcess+">") ;
         context.skipLines(1);
         GXKey = Crypto.GetSiteKey( );
         GXEncryptionTmp = "trn_appversion.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(AV8AppVersionId.ToString());
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("trn_appversion.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
         GxWebStd.gx_hidden_field( context, "_EventName", "");
         GxWebStd.gx_hidden_field( context, "_EventGridId", "");
         GxWebStd.gx_hidden_field( context, "_EventRowId", "");
         context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
         AssignProp("", false, "FORM", "Class", "form-horizontal Form", true);
         toggleJsOutput = isJsOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         GXKey = Crypto.GetSiteKey( );
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", "hsh"+"Trn_AppVersion");
         forbiddenHiddens.Add("Gx_mode", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")));
         GxWebStd.gx_hidden_field( context, "hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("trn_appversion:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "Z523AppVersionId", Z523AppVersionId.ToString());
         GxWebStd.gx_hidden_field( context, "Z524AppVersionName", Z524AppVersionName);
         GxWebStd.gx_boolean_hidden_field( context, "Z535IsActive", Z535IsActive);
         GxWebStd.gx_hidden_field( context, "Z29LocationId", Z29LocationId.ToString());
         GxWebStd.gx_hidden_field( context, "Z11OrganisationId", Z11OrganisationId.ToString());
         GxWebStd.gx_hidden_field( context, "IsConfirmed", StringUtil.LTrim( StringUtil.NToC( (decimal)(IsConfirmed), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "IsModified", StringUtil.LTrim( StringUtil.NToC( (decimal)(IsModified), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Mode", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_Mode", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_59", StringUtil.LTrim( StringUtil.NToC( (decimal)(nGXsfl_59_idx), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "N29LocationId", A29LocationId.ToString());
         GxWebStd.gx_hidden_field( context, "N11OrganisationId", A11OrganisationId.ToString());
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vDDO_TITLESETTINGSICONS", AV18DDO_TitleSettingsIcons);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vDDO_TITLESETTINGSICONS", AV18DDO_TitleSettingsIcons);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vLOCATIONID_DATA", AV17LocationId_Data);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vLOCATIONID_DATA", AV17LocationId_Data);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vORGANISATIONID_DATA", AV26OrganisationId_Data);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vORGANISATIONID_DATA", AV26OrganisationId_Data);
         }
         GxWebStd.gx_hidden_field( context, "vMODE", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_vMODE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vTRNCONTEXT", AV12TrnContext);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vTRNCONTEXT", AV12TrnContext);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vTRNCONTEXT", GetSecureSignedToken( "", AV12TrnContext, context));
         GxWebStd.gx_hidden_field( context, "vAPPVERSIONID", AV8AppVersionId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vAPPVERSIONID", GetSecureSignedToken( "", AV8AppVersionId, context));
         GxWebStd.gx_hidden_field( context, "vGXBSCREEN", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gx_BScreen), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vINSERT_LOCATIONID", AV14Insert_LocationId.ToString());
         GxWebStd.gx_hidden_field( context, "vINSERT_ORGANISATIONID", AV15Insert_OrganisationId.ToString());
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV30Pgmname));
         GxWebStd.gx_hidden_field( context, "PAGETHUMBNAIL", A599PageThumbnail);
         GxWebStd.gx_hidden_field( context, "PAGETHUMBNAIL_GXI", A40000PageThumbnail_GXI);
         GxWebStd.gx_hidden_field( context, "COMBO_LOCATIONID_Objectcall", StringUtil.RTrim( Combo_locationid_Objectcall));
         GxWebStd.gx_hidden_field( context, "COMBO_LOCATIONID_Cls", StringUtil.RTrim( Combo_locationid_Cls));
         GxWebStd.gx_hidden_field( context, "COMBO_LOCATIONID_Selectedvalue_set", StringUtil.RTrim( Combo_locationid_Selectedvalue_set));
         GxWebStd.gx_hidden_field( context, "COMBO_LOCATIONID_Selectedtext_set", StringUtil.RTrim( Combo_locationid_Selectedtext_set));
         GxWebStd.gx_hidden_field( context, "COMBO_LOCATIONID_Gamoauthtoken", StringUtil.RTrim( Combo_locationid_Gamoauthtoken));
         GxWebStd.gx_hidden_field( context, "COMBO_LOCATIONID_Enabled", StringUtil.BoolToStr( Combo_locationid_Enabled));
         GxWebStd.gx_hidden_field( context, "COMBO_LOCATIONID_Datalistproc", StringUtil.RTrim( Combo_locationid_Datalistproc));
         GxWebStd.gx_hidden_field( context, "COMBO_LOCATIONID_Datalistprocparametersprefix", StringUtil.RTrim( Combo_locationid_Datalistprocparametersprefix));
         GxWebStd.gx_hidden_field( context, "COMBO_ORGANISATIONID_Objectcall", StringUtil.RTrim( Combo_organisationid_Objectcall));
         GxWebStd.gx_hidden_field( context, "COMBO_ORGANISATIONID_Cls", StringUtil.RTrim( Combo_organisationid_Cls));
         GxWebStd.gx_hidden_field( context, "COMBO_ORGANISATIONID_Selectedvalue_set", StringUtil.RTrim( Combo_organisationid_Selectedvalue_set));
         GxWebStd.gx_hidden_field( context, "COMBO_ORGANISATIONID_Selectedtext_set", StringUtil.RTrim( Combo_organisationid_Selectedtext_set));
         GxWebStd.gx_hidden_field( context, "COMBO_ORGANISATIONID_Gamoauthtoken", StringUtil.RTrim( Combo_organisationid_Gamoauthtoken));
         GxWebStd.gx_hidden_field( context, "COMBO_ORGANISATIONID_Enabled", StringUtil.BoolToStr( Combo_organisationid_Enabled));
         GxWebStd.gx_hidden_field( context, "COMBO_ORGANISATIONID_Datalistproc", StringUtil.RTrim( Combo_organisationid_Datalistproc));
         GxWebStd.gx_hidden_field( context, "COMBO_ORGANISATIONID_Datalistprocparametersprefix", StringUtil.RTrim( Combo_organisationid_Datalistprocparametersprefix));
      }

      public override void RenderHtmlCloseForm( )
      {
         SendCloseFormHiddens( ) ;
         GxWebStd.gx_hidden_field( context, "GX_FocusControl", GX_FocusControl);
         SendAjaxEncryptionKey();
         SendSecurityToken(sPrefix);
         SendComponentObjects();
         SendServerCommands();
         SendState();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         context.WriteHtmlTextNl( "</form>") ;
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         include_jscripts( ) ;
         context.WriteHtmlText( "<script type=\"text/javascript\">") ;
         context.WriteHtmlText( "gx.setLanguageCode(\""+context.GetLanguageProperty( "code")+"\");") ;
         if ( ! context.isSpaRequest( ) )
         {
            context.WriteHtmlText( "gx.setDateFormat(\""+context.GetLanguageProperty( "date_fmt")+"\");") ;
            context.WriteHtmlText( "gx.setTimeFormat("+context.GetLanguageProperty( "time_fmt")+");") ;
            context.WriteHtmlText( "gx.setCenturyFirstYear("+40+");") ;
            context.WriteHtmlText( "gx.setDecimalPoint(\""+context.GetLanguageProperty( "decimal_point")+"\");") ;
            context.WriteHtmlText( "gx.setThousandSeparator(\""+context.GetLanguageProperty( "thousand_sep")+"\");") ;
            context.WriteHtmlText( "gx.StorageTimeZone = "+1+";") ;
         }
         context.WriteHtmlText( "</script>") ;
      }

      public override short ExecuteStartEvent( )
      {
         standaloneStartup( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         return gxajaxcallmode ;
      }

      public override void RenderHtmlContent( )
      {
         context.WriteHtmlText( "<div") ;
         GxWebStd.ClassAttribute( context, "gx-ct-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
         context.WriteHtmlText( ">") ;
         Draw( ) ;
         context.WriteHtmlText( "</div>") ;
      }

      public override void DispatchEvents( )
      {
         Process( ) ;
      }

      public override bool HasEnterEvent( )
      {
         return true ;
      }

      public override GXWebForm GetForm( )
      {
         return Form ;
      }

      public override string GetSelfLink( )
      {
         GXKey = Crypto.GetSiteKey( );
         GXEncryptionTmp = "trn_appversion.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(AV8AppVersionId.ToString());
         return formatLink("trn_appversion.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey) ;
      }

      public override string GetPgmname( )
      {
         return "Trn_AppVersion" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Trn_App Version", "") ;
      }

      protected void InitializeNonKey1L6( )
      {
         A29LocationId = Guid.Empty;
         n29LocationId = false;
         AssignAttri("", false, "A29LocationId", A29LocationId.ToString());
         n29LocationId = ((Guid.Empty==A29LocationId) ? true : false);
         A11OrganisationId = Guid.Empty;
         n11OrganisationId = false;
         AssignAttri("", false, "A11OrganisationId", A11OrganisationId.ToString());
         n11OrganisationId = ((Guid.Empty==A11OrganisationId) ? true : false);
         A524AppVersionName = "";
         AssignAttri("", false, "A524AppVersionName", A524AppVersionName);
         A535IsActive = false;
         AssignAttri("", false, "A535IsActive", A535IsActive);
         Z524AppVersionName = "";
         Z535IsActive = false;
         Z29LocationId = Guid.Empty;
         Z11OrganisationId = Guid.Empty;
      }

      protected void InitAll1L6( )
      {
         A523AppVersionId = Guid.NewGuid( );
         AssignAttri("", false, "A523AppVersionId", A523AppVersionId.ToString());
         InitializeNonKey1L6( ) ;
      }

      protected void StandaloneModalInsert( )
      {
      }

      protected void InitializeNonKey1L95( )
      {
         A517PageName = "";
         A518PageStructure = "";
         A536PagePublishedStructure = "";
         A599PageThumbnail = "";
         n599PageThumbnail = false;
         AssignAttri("", false, "A599PageThumbnail", A599PageThumbnail);
         A40000PageThumbnail_GXI = "";
         n40000PageThumbnail_GXI = false;
         AssignAttri("", false, "A40000PageThumbnail_GXI", A40000PageThumbnail_GXI);
         A525PageType = "";
         A541IsPredefined = false;
         Z541IsPredefined = false;
         Z517PageName = "";
         Z525PageType = "";
      }

      protected void InitAll1L95( )
      {
         A516PageId = Guid.NewGuid( );
         InitializeNonKey1L95( ) ;
      }

      protected void StandaloneModalInsert1L95( )
      {
         A541IsPredefined = i541IsPredefined;
      }

      protected void define_styles( )
      {
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20254281303734", true, true);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         context.AddJavascriptSource("messages."+StringUtil.Lower( context.GetLanguageProperty( "code"))+".js", "?"+GetCacheInvalidationToken( ), false, true);
         context.AddJavascriptSource("trn_appversion.js", "?20254281303739", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void init_level_properties95( )
      {
         edtPageId_Enabled = defedtPageId_Enabled;
         AssignProp("", false, edtPageId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtPageId_Enabled), 5, 0), !bGXsfl_59_Refreshing);
      }

      protected void StartGridControl59( )
      {
         Gridlevel_pageContainer.AddObjectProperty("GridName", "Gridlevel_page");
         Gridlevel_pageContainer.AddObjectProperty("Header", subGridlevel_page_Header);
         Gridlevel_pageContainer.AddObjectProperty("Class", "WorkWith");
         Gridlevel_pageContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         Gridlevel_pageContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         Gridlevel_pageContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlevel_page_Backcolorstyle), 1, 0, ".", "")));
         Gridlevel_pageContainer.AddObjectProperty("CmpContext", "");
         Gridlevel_pageContainer.AddObjectProperty("InMasterPage", "false");
         Gridlevel_pageColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         Gridlevel_pageColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A516PageId.ToString()));
         Gridlevel_pageColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtPageId_Enabled), 5, 0, ".", "")));
         Gridlevel_pageContainer.AddColumnProperties(Gridlevel_pageColumn);
         Gridlevel_pageColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         Gridlevel_pageColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A517PageName));
         Gridlevel_pageColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtPageName_Enabled), 5, 0, ".", "")));
         Gridlevel_pageContainer.AddColumnProperties(Gridlevel_pageColumn);
         Gridlevel_pageColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         Gridlevel_pageColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A518PageStructure));
         Gridlevel_pageColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtPageStructure_Enabled), 5, 0, ".", "")));
         Gridlevel_pageContainer.AddColumnProperties(Gridlevel_pageColumn);
         Gridlevel_pageColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         Gridlevel_pageColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A536PagePublishedStructure));
         Gridlevel_pageColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtPagePublishedStructure_Enabled), 5, 0, ".", "")));
         Gridlevel_pageContainer.AddColumnProperties(Gridlevel_pageColumn);
         Gridlevel_pageColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         Gridlevel_pageColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.BoolToStr( A541IsPredefined)));
         Gridlevel_pageColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(chkIsPredefined.Enabled), 5, 0, ".", "")));
         Gridlevel_pageContainer.AddColumnProperties(Gridlevel_pageColumn);
         Gridlevel_pageColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         Gridlevel_pageColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A525PageType));
         Gridlevel_pageColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(cmbPageType.Enabled), 5, 0, ".", "")));
         Gridlevel_pageContainer.AddColumnProperties(Gridlevel_pageColumn);
         Gridlevel_pageContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlevel_page_Selectedindex), 4, 0, ".", "")));
         Gridlevel_pageContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlevel_page_Allowselection), 1, 0, ".", "")));
         Gridlevel_pageContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlevel_page_Selectioncolor), 9, 0, ".", "")));
         Gridlevel_pageContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlevel_page_Allowhovering), 1, 0, ".", "")));
         Gridlevel_pageContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlevel_page_Hoveringcolor), 9, 0, ".", "")));
         Gridlevel_pageContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlevel_page_Allowcollapsing), 1, 0, ".", "")));
         Gridlevel_pageContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlevel_page_Collapsed), 1, 0, ".", "")));
      }

      protected void init_default_properties( )
      {
         edtAppVersionId_Internalname = "APPVERSIONID";
         edtAppVersionName_Internalname = "APPVERSIONNAME";
         lblTextblocklocationid_Internalname = "TEXTBLOCKLOCATIONID";
         Combo_locationid_Internalname = "COMBO_LOCATIONID";
         edtLocationId_Internalname = "LOCATIONID";
         divTablesplittedlocationid_Internalname = "TABLESPLITTEDLOCATIONID";
         lblTextblockorganisationid_Internalname = "TEXTBLOCKORGANISATIONID";
         Combo_organisationid_Internalname = "COMBO_ORGANISATIONID";
         edtOrganisationId_Internalname = "ORGANISATIONID";
         divTablesplittedorganisationid_Internalname = "TABLESPLITTEDORGANISATIONID";
         chkIsActive_Internalname = "ISACTIVE";
         divTableattributes_Internalname = "TABLEATTRIBUTES";
         divTablecontent_Internalname = "TABLECONTENT";
         grpUnnamedgroup1_Internalname = "UNNAMEDGROUP1";
         edtPageId_Internalname = "PAGEID";
         edtPageName_Internalname = "PAGENAME";
         edtPageStructure_Internalname = "PAGESTRUCTURE";
         edtPagePublishedStructure_Internalname = "PAGEPUBLISHEDSTRUCTURE";
         chkIsPredefined_Internalname = "ISPREDEFINED";
         cmbPageType_Internalname = "PAGETYPE";
         divTableleaflevel_page_Internalname = "TABLELEAFLEVEL_PAGE";
         bttBtntrn_enter_Internalname = "BTNTRN_ENTER";
         bttBtntrn_cancel_Internalname = "BTNTRN_CANCEL";
         bttBtntrn_delete_Internalname = "BTNTRN_DELETE";
         divTablemain_Internalname = "TABLEMAIN";
         edtavCombolocationid_Internalname = "vCOMBOLOCATIONID";
         divSectionattribute_locationid_Internalname = "SECTIONATTRIBUTE_LOCATIONID";
         edtavComboorganisationid_Internalname = "vCOMBOORGANISATIONID";
         divSectionattribute_organisationid_Internalname = "SECTIONATTRIBUTE_ORGANISATIONID";
         divHtml_bottomauxiliarcontrols_Internalname = "HTML_BOTTOMAUXILIARCONTROLS";
         divLayoutmaintable_Internalname = "LAYOUTMAINTABLE";
         Form.Internalname = "FORM";
         subGridlevel_page_Internalname = "GRIDLEVEL_PAGE";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("WorkWithPlusDS", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subGridlevel_page_Allowcollapsing = 0;
         subGridlevel_page_Allowselection = 0;
         subGridlevel_page_Header = "";
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "Trn_App Version", "");
         cmbPageType_Jsonclick = "";
         chkIsPredefined.Caption = "";
         edtPagePublishedStructure_Jsonclick = "";
         edtPageStructure_Jsonclick = "";
         edtPageName_Jsonclick = "";
         edtPageId_Jsonclick = "";
         subGridlevel_page_Class = "WorkWith";
         subGridlevel_page_Backcolorstyle = 0;
         Combo_locationid_Datalistprocparametersprefix = "";
         cmbPageType.Enabled = 1;
         chkIsPredefined.Enabled = 1;
         edtPagePublishedStructure_Enabled = 1;
         edtPageStructure_Enabled = 1;
         edtPageName_Enabled = 1;
         edtPageId_Enabled = 1;
         edtavComboorganisationid_Jsonclick = "";
         edtavComboorganisationid_Enabled = 0;
         edtavComboorganisationid_Visible = 1;
         edtavCombolocationid_Jsonclick = "";
         edtavCombolocationid_Enabled = 0;
         edtavCombolocationid_Visible = 1;
         bttBtntrn_delete_Enabled = 0;
         bttBtntrn_delete_Visible = 1;
         bttBtntrn_cancel_Visible = 1;
         bttBtntrn_enter_Enabled = 1;
         bttBtntrn_enter_Visible = 1;
         chkIsActive.Enabled = 1;
         edtOrganisationId_Jsonclick = "";
         edtOrganisationId_Enabled = 1;
         edtOrganisationId_Visible = 1;
         Combo_organisationid_Datalistprocparametersprefix = " \"ComboName\": \"OrganisationId\", \"TrnMode\": \"INS\", \"IsDynamicCall\": true, \"AppVersionId\": \"00000000-0000-0000-0000-000000000000\"";
         Combo_organisationid_Datalistproc = "Trn_AppVersionLoadDVCombo";
         Combo_organisationid_Cls = "ExtendedCombo Attribute";
         Combo_organisationid_Caption = "";
         Combo_organisationid_Enabled = Convert.ToBoolean( -1);
         edtLocationId_Jsonclick = "";
         edtLocationId_Enabled = 1;
         edtLocationId_Visible = 1;
         Combo_locationid_Datalistproc = "Trn_AppVersionLoadDVCombo";
         Combo_locationid_Cls = "ExtendedCombo Attribute";
         Combo_locationid_Caption = "";
         Combo_locationid_Enabled = Convert.ToBoolean( -1);
         edtAppVersionName_Jsonclick = "";
         edtAppVersionName_Enabled = 1;
         edtAppVersionId_Jsonclick = "";
         edtAppVersionId_Enabled = 1;
         divLayoutmaintable_Class = "Table";
         context.GX_msglist.DisplayMode = 1;
         if ( context.isSpaRequest( ) )
         {
            enableJsOutput();
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridlevel_page_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         Gx_mode = "INS";
         AssignAttri("", false, "Gx_mode", Gx_mode);
         SubsflControlProps_5995( ) ;
         while ( nGXsfl_59_idx <= nRC_GXsfl_59 )
         {
            standaloneNotModal( ) ;
            standaloneModal( ) ;
            standaloneNotModal1L95( ) ;
            standaloneModal1L95( ) ;
            init_web_controls( ) ;
            dynload_actions( ) ;
            SendRow1L95( ) ;
            nGXsfl_59_idx = (int)(nGXsfl_59_idx+1);
            sGXsfl_59_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_59_idx), 4, 0), 4, "0");
            SubsflControlProps_5995( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( Gridlevel_pageContainer)) ;
         /* End function gxnrGridlevel_page_newrow */
      }

      protected void init_web_controls( )
      {
         chkIsActive.Name = "ISACTIVE";
         chkIsActive.WebTags = "";
         chkIsActive.Caption = context.GetMessage( "Active", "");
         AssignProp("", false, chkIsActive_Internalname, "TitleCaption", chkIsActive.Caption, true);
         chkIsActive.CheckedValue = "false";
         A535IsActive = StringUtil.StrToBool( StringUtil.BoolToStr( A535IsActive));
         AssignAttri("", false, "A535IsActive", A535IsActive);
         GXCCtl = "ISPREDEFINED_" + sGXsfl_59_idx;
         chkIsPredefined.Name = GXCCtl;
         chkIsPredefined.WebTags = "";
         chkIsPredefined.Caption = "";
         AssignProp("", false, chkIsPredefined_Internalname, "TitleCaption", chkIsPredefined.Caption, !bGXsfl_59_Refreshing);
         chkIsPredefined.CheckedValue = "false";
         if ( IsIns( ) && (false==A541IsPredefined) )
         {
            A541IsPredefined = false;
         }
         GXCCtl = "PAGETYPE_" + sGXsfl_59_idx;
         cmbPageType.Name = GXCCtl;
         cmbPageType.WebTags = "";
         cmbPageType.addItem("Menu", context.GetMessage( "Menu", ""), 0);
         cmbPageType.addItem("Content", context.GetMessage( "Content", ""), 0);
         cmbPageType.addItem("WebLink", context.GetMessage( "Web Link", ""), 0);
         cmbPageType.addItem("DynamicForm", context.GetMessage( "Dynamic Form", ""), 0);
         cmbPageType.addItem("Calendar", context.GetMessage( "Calendar", ""), 0);
         cmbPageType.addItem("MyActivity", context.GetMessage( "My Activity", ""), 0);
         cmbPageType.addItem("Map", context.GetMessage( "Map", ""), 0);
         cmbPageType.addItem("Reception", context.GetMessage( "Reception", ""), 0);
         cmbPageType.addItem("Location", context.GetMessage( "Location", ""), 0);
         cmbPageType.addItem("MyCare", context.GetMessage( "My Care", ""), 0);
         cmbPageType.addItem("MyLiving", context.GetMessage( "My Living", ""), 0);
         cmbPageType.addItem("MyService", context.GetMessage( "My Service", ""), 0);
         cmbPageType.addItem("Information", context.GetMessage( "Information", ""), 0);
         if ( cmbPageType.ItemCount > 0 )
         {
            A525PageType = cmbPageType.getValidValue(A525PageType);
         }
         /* End function init_web_controls */
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

      public void Valid_Organisationid( )
      {
         n11OrganisationId = false;
         n29LocationId = false;
         /* Using cursor T001L27 */
         pr_default.execute(25, new Object[] {n11OrganisationId, A11OrganisationId});
         if ( (pr_default.getStatus(25) == 101) )
         {
            if ( ! ( (Guid.Empty==A11OrganisationId) ) )
            {
               GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXSPC_ForeignKeyNotFound", ""), context.GetMessage( "Trn_Organisation", ""), "", "", "", "", "", "", "", ""), "ForeignKeyNotFound", 1, "ORGANISATIONID");
               AnyError = 1;
               GX_FocusControl = edtOrganisationId_Internalname;
            }
         }
         pr_default.close(25);
         /* Using cursor T001L28 */
         pr_default.execute(26, new Object[] {n29LocationId, A29LocationId, n11OrganisationId, A11OrganisationId});
         if ( (pr_default.getStatus(26) == 101) )
         {
            if ( ! ( (Guid.Empty==A29LocationId) || (Guid.Empty==A11OrganisationId) ) )
            {
               GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXSPC_ForeignKeyNotFound", ""), context.GetMessage( "Locations", ""), "", "", "", "", "", "", "", ""), "ForeignKeyNotFound", 1, "ORGANISATIONID");
               AnyError = 1;
               GX_FocusControl = edtLocationId_Internalname;
            }
         }
         pr_default.close(26);
         dynload_actions( ) ;
         /*  Sending validation outputs */
      }

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("ENTER","""{"handler":"UserMainFullajax","iparms":[{"postForm":true},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV8AppVersionId","fld":"vAPPVERSIONID","hsh":true},{"av":"A535IsActive","fld":"ISACTIVE"}]""");
         setEventMetadata("ENTER",""","oparms":[{"av":"A535IsActive","fld":"ISACTIVE"}]}""");
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV12TrnContext","fld":"vTRNCONTEXT","hsh":true},{"av":"AV8AppVersionId","fld":"vAPPVERSIONID","hsh":true},{"av":"A535IsActive","fld":"ISACTIVE"}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"A535IsActive","fld":"ISACTIVE"}]}""");
         setEventMetadata("AFTER TRN","""{"handler":"E141L2","iparms":[{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV12TrnContext","fld":"vTRNCONTEXT","hsh":true},{"av":"A535IsActive","fld":"ISACTIVE"}]""");
         setEventMetadata("AFTER TRN",""","oparms":[{"av":"A535IsActive","fld":"ISACTIVE"}]}""");
         setEventMetadata("COMBO_ORGANISATIONID.ONOPTIONCLICKED","""{"handler":"E131L2","iparms":[{"av":"A11OrganisationId","fld":"ORGANISATIONID"},{"av":"Combo_organisationid_Selectedvalue_get","ctrl":"COMBO_ORGANISATIONID","prop":"SelectedValue_get"},{"av":"A535IsActive","fld":"ISACTIVE"}]""");
         setEventMetadata("COMBO_ORGANISATIONID.ONOPTIONCLICKED",""","oparms":[{"av":"AV27ComboOrganisationId","fld":"vCOMBOORGANISATIONID"},{"av":"AV22ComboLocationId","fld":"vCOMBOLOCATIONID"},{"av":"Combo_locationid_Selectedvalue_set","ctrl":"COMBO_LOCATIONID","prop":"SelectedValue_set"},{"av":"Combo_locationid_Selectedtext_set","ctrl":"COMBO_LOCATIONID","prop":"SelectedText_set"},{"av":"A535IsActive","fld":"ISACTIVE"}]}""");
         setEventMetadata("COMBO_LOCATIONID.ONOPTIONCLICKED","""{"handler":"E121L2","iparms":[{"av":"Combo_locationid_Selectedvalue_get","ctrl":"COMBO_LOCATIONID","prop":"SelectedValue_get"},{"av":"A535IsActive","fld":"ISACTIVE"}]""");
         setEventMetadata("COMBO_LOCATIONID.ONOPTIONCLICKED",""","oparms":[{"av":"AV22ComboLocationId","fld":"vCOMBOLOCATIONID"},{"av":"A535IsActive","fld":"ISACTIVE"}]}""");
         setEventMetadata("VALID_APPVERSIONID","""{"handler":"Valid_Appversionid","iparms":[{"av":"A535IsActive","fld":"ISACTIVE"}]""");
         setEventMetadata("VALID_APPVERSIONID",""","oparms":[{"av":"A535IsActive","fld":"ISACTIVE"}]}""");
         setEventMetadata("VALID_LOCATIONID","""{"handler":"Valid_Locationid","iparms":[{"av":"A535IsActive","fld":"ISACTIVE"}]""");
         setEventMetadata("VALID_LOCATIONID",""","oparms":[{"av":"A535IsActive","fld":"ISACTIVE"}]}""");
         setEventMetadata("VALID_ORGANISATIONID","""{"handler":"Valid_Organisationid","iparms":[{"av":"A11OrganisationId","fld":"ORGANISATIONID"},{"av":"A29LocationId","fld":"LOCATIONID"},{"av":"A535IsActive","fld":"ISACTIVE"}]""");
         setEventMetadata("VALID_ORGANISATIONID",""","oparms":[{"av":"A535IsActive","fld":"ISACTIVE"}]}""");
         setEventMetadata("VALIDV_COMBOLOCATIONID","""{"handler":"Validv_Combolocationid","iparms":[{"av":"A535IsActive","fld":"ISACTIVE"}]""");
         setEventMetadata("VALIDV_COMBOLOCATIONID",""","oparms":[{"av":"A535IsActive","fld":"ISACTIVE"}]}""");
         setEventMetadata("VALIDV_COMBOORGANISATIONID","""{"handler":"Validv_Comboorganisationid","iparms":[{"av":"A535IsActive","fld":"ISACTIVE"}]""");
         setEventMetadata("VALIDV_COMBOORGANISATIONID",""","oparms":[{"av":"A535IsActive","fld":"ISACTIVE"}]}""");
         setEventMetadata("VALID_PAGEID","""{"handler":"Valid_Pageid","iparms":[{"av":"A535IsActive","fld":"ISACTIVE"}]""");
         setEventMetadata("VALID_PAGEID",""","oparms":[{"av":"A535IsActive","fld":"ISACTIVE"}]}""");
         setEventMetadata("VALID_PAGETYPE","""{"handler":"Valid_Pagetype","iparms":[{"av":"A535IsActive","fld":"ISACTIVE"}]""");
         setEventMetadata("VALID_PAGETYPE",""","oparms":[{"av":"A535IsActive","fld":"ISACTIVE"}]}""");
         return  ;
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
         pr_default.close(3);
         pr_default.close(26);
         pr_default.close(25);
      }

      public override void initialize( )
      {
         sPrefix = "";
         wcpOGx_mode = "";
         wcpOAV8AppVersionId = Guid.Empty;
         Z523AppVersionId = Guid.Empty;
         Z524AppVersionName = "";
         Z29LocationId = Guid.Empty;
         Z11OrganisationId = Guid.Empty;
         N29LocationId = Guid.Empty;
         N11OrganisationId = Guid.Empty;
         Combo_organisationid_Selectedvalue_get = "";
         Combo_locationid_Selectedvalue_get = "";
         Z516PageId = Guid.Empty;
         Z517PageName = "";
         Z525PageType = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         A29LocationId = Guid.Empty;
         A11OrganisationId = Guid.Empty;
         GXKey = "";
         GXDecQS = "";
         PreviousTooltip = "";
         PreviousCaption = "";
         Form = new GXWebForm();
         GX_FocusControl = "";
         ClassString = "";
         StyleString = "";
         TempTags = "";
         A523AppVersionId = Guid.Empty;
         A524AppVersionName = "";
         lblTextblocklocationid_Jsonclick = "";
         ucCombo_locationid = new GXUserControl();
         AV18DDO_TitleSettingsIcons = new WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV17LocationId_Data = new GXBaseCollection<WorkWithPlus.workwithplus_web.SdtDVB_SDTComboData_Item>( context, "Item", "");
         lblTextblockorganisationid_Jsonclick = "";
         ucCombo_organisationid = new GXUserControl();
         AV26OrganisationId_Data = new GXBaseCollection<WorkWithPlus.workwithplus_web.SdtDVB_SDTComboData_Item>( context, "Item", "");
         bttBtntrn_enter_Jsonclick = "";
         bttBtntrn_cancel_Jsonclick = "";
         bttBtntrn_delete_Jsonclick = "";
         AV22ComboLocationId = Guid.Empty;
         AV27ComboOrganisationId = Guid.Empty;
         Gridlevel_pageContainer = new GXWebGrid( context);
         sMode95 = "";
         A516PageId = Guid.Empty;
         sStyleString = "";
         AV14Insert_LocationId = Guid.Empty;
         AV15Insert_OrganisationId = Guid.Empty;
         AV30Pgmname = "";
         A599PageThumbnail = "";
         A40000PageThumbnail_GXI = "";
         Combo_locationid_Objectcall = "";
         Combo_locationid_Class = "";
         Combo_locationid_Icontype = "";
         Combo_locationid_Icon = "";
         Combo_locationid_Tooltip = "";
         Combo_locationid_Selectedvalue_set = "";
         Combo_locationid_Selectedtext_set = "";
         Combo_locationid_Selectedtext_get = "";
         Combo_locationid_Gamoauthtoken = "";
         Combo_locationid_Ddointernalname = "";
         Combo_locationid_Titlecontrolalign = "";
         Combo_locationid_Dropdownoptionstype = "";
         Combo_locationid_Titlecontrolidtoreplace = "";
         Combo_locationid_Datalisttype = "";
         Combo_locationid_Datalistfixedvalues = "";
         Combo_locationid_Remoteservicesparameters = "";
         Combo_locationid_Htmltemplate = "";
         Combo_locationid_Multiplevaluestype = "";
         Combo_locationid_Loadingdata = "";
         Combo_locationid_Noresultsfound = "";
         Combo_locationid_Emptyitemtext = "";
         Combo_locationid_Onlyselectedvalues = "";
         Combo_locationid_Selectalltext = "";
         Combo_locationid_Multiplevaluesseparator = "";
         Combo_locationid_Addnewoptiontext = "";
         Combo_organisationid_Objectcall = "";
         Combo_organisationid_Class = "";
         Combo_organisationid_Icontype = "";
         Combo_organisationid_Icon = "";
         Combo_organisationid_Tooltip = "";
         Combo_organisationid_Selectedvalue_set = "";
         Combo_organisationid_Selectedtext_set = "";
         Combo_organisationid_Selectedtext_get = "";
         Combo_organisationid_Gamoauthtoken = "";
         Combo_organisationid_Ddointernalname = "";
         Combo_organisationid_Titlecontrolalign = "";
         Combo_organisationid_Dropdownoptionstype = "";
         Combo_organisationid_Titlecontrolidtoreplace = "";
         Combo_organisationid_Datalisttype = "";
         Combo_organisationid_Datalistfixedvalues = "";
         Combo_organisationid_Remoteservicesparameters = "";
         Combo_organisationid_Htmltemplate = "";
         Combo_organisationid_Multiplevaluestype = "";
         Combo_organisationid_Loadingdata = "";
         Combo_organisationid_Noresultsfound = "";
         Combo_organisationid_Emptyitemtext = "";
         Combo_organisationid_Onlyselectedvalues = "";
         Combo_organisationid_Selectalltext = "";
         Combo_organisationid_Multiplevaluesseparator = "";
         Combo_organisationid_Addnewoptiontext = "";
         forbiddenHiddens = new GXProperties();
         hsh = "";
         sMode6 = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         endTrnMsgTxt = "";
         endTrnMsgCod = "";
         GXCCtl = "";
         A517PageName = "";
         A518PageStructure = "";
         A536PagePublishedStructure = "";
         A525PageType = "";
         AV9WWPContext = new GeneXus.Programs.wwpbaseobjects.SdtWWPContext(context);
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 = new WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV24GAMSession = new GeneXus.Programs.genexussecurity.SdtGAMSession(context);
         AV25GAMErrors = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "GeneXus.Programs");
         AV12TrnContext = new WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext(context);
         AV13WebSession = context.GetSession();
         AV16TrnContextAtt = new WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext_Attribute(context);
         AV21Combo_DataJson = "";
         AV19ComboSelectedValue = "";
         AV20ComboSelectedText = "";
         AV23Cond_OrganisationId = Guid.Empty;
         GXt_char2 = "";
         T001L8_A523AppVersionId = new Guid[] {Guid.Empty} ;
         T001L8_A524AppVersionName = new string[] {""} ;
         T001L8_A535IsActive = new bool[] {false} ;
         T001L8_A29LocationId = new Guid[] {Guid.Empty} ;
         T001L8_n29LocationId = new bool[] {false} ;
         T001L8_A11OrganisationId = new Guid[] {Guid.Empty} ;
         T001L8_n11OrganisationId = new bool[] {false} ;
         T001L7_A29LocationId = new Guid[] {Guid.Empty} ;
         T001L7_n29LocationId = new bool[] {false} ;
         T001L6_A11OrganisationId = new Guid[] {Guid.Empty} ;
         T001L6_n11OrganisationId = new bool[] {false} ;
         T001L9_A29LocationId = new Guid[] {Guid.Empty} ;
         T001L9_n29LocationId = new bool[] {false} ;
         T001L10_A11OrganisationId = new Guid[] {Guid.Empty} ;
         T001L10_n11OrganisationId = new bool[] {false} ;
         T001L11_A523AppVersionId = new Guid[] {Guid.Empty} ;
         T001L5_A523AppVersionId = new Guid[] {Guid.Empty} ;
         T001L5_A524AppVersionName = new string[] {""} ;
         T001L5_A535IsActive = new bool[] {false} ;
         T001L5_A29LocationId = new Guid[] {Guid.Empty} ;
         T001L5_n29LocationId = new bool[] {false} ;
         T001L5_A11OrganisationId = new Guid[] {Guid.Empty} ;
         T001L5_n11OrganisationId = new bool[] {false} ;
         T001L12_A523AppVersionId = new Guid[] {Guid.Empty} ;
         T001L13_A523AppVersionId = new Guid[] {Guid.Empty} ;
         T001L4_A523AppVersionId = new Guid[] {Guid.Empty} ;
         T001L4_A524AppVersionName = new string[] {""} ;
         T001L4_A535IsActive = new bool[] {false} ;
         T001L4_A29LocationId = new Guid[] {Guid.Empty} ;
         T001L4_n29LocationId = new bool[] {false} ;
         T001L4_A11OrganisationId = new Guid[] {Guid.Empty} ;
         T001L4_n11OrganisationId = new bool[] {false} ;
         T001L17_A29LocationId = new Guid[] {Guid.Empty} ;
         T001L17_n29LocationId = new bool[] {false} ;
         T001L17_A11OrganisationId = new Guid[] {Guid.Empty} ;
         T001L17_n11OrganisationId = new bool[] {false} ;
         T001L18_A29LocationId = new Guid[] {Guid.Empty} ;
         T001L18_n29LocationId = new bool[] {false} ;
         T001L18_A11OrganisationId = new Guid[] {Guid.Empty} ;
         T001L18_n11OrganisationId = new bool[] {false} ;
         T001L19_A523AppVersionId = new Guid[] {Guid.Empty} ;
         Z518PageStructure = "";
         Z536PagePublishedStructure = "";
         Z599PageThumbnail = "";
         Z40000PageThumbnail_GXI = "";
         T001L20_A523AppVersionId = new Guid[] {Guid.Empty} ;
         T001L20_A516PageId = new Guid[] {Guid.Empty} ;
         T001L20_A541IsPredefined = new bool[] {false} ;
         T001L20_A517PageName = new string[] {""} ;
         T001L20_A518PageStructure = new string[] {""} ;
         T001L20_A536PagePublishedStructure = new string[] {""} ;
         T001L20_A40000PageThumbnail_GXI = new string[] {""} ;
         T001L20_n40000PageThumbnail_GXI = new bool[] {false} ;
         T001L20_A525PageType = new string[] {""} ;
         T001L20_A599PageThumbnail = new string[] {""} ;
         T001L20_n599PageThumbnail = new bool[] {false} ;
         T001L21_A523AppVersionId = new Guid[] {Guid.Empty} ;
         T001L21_A516PageId = new Guid[] {Guid.Empty} ;
         T001L3_A523AppVersionId = new Guid[] {Guid.Empty} ;
         T001L3_A516PageId = new Guid[] {Guid.Empty} ;
         T001L3_A541IsPredefined = new bool[] {false} ;
         T001L3_A517PageName = new string[] {""} ;
         T001L3_A518PageStructure = new string[] {""} ;
         T001L3_A536PagePublishedStructure = new string[] {""} ;
         T001L3_A40000PageThumbnail_GXI = new string[] {""} ;
         T001L3_n40000PageThumbnail_GXI = new bool[] {false} ;
         T001L3_A525PageType = new string[] {""} ;
         T001L3_A599PageThumbnail = new string[] {""} ;
         T001L3_n599PageThumbnail = new bool[] {false} ;
         T001L2_A523AppVersionId = new Guid[] {Guid.Empty} ;
         T001L2_A516PageId = new Guid[] {Guid.Empty} ;
         T001L2_A541IsPredefined = new bool[] {false} ;
         T001L2_A517PageName = new string[] {""} ;
         T001L2_A518PageStructure = new string[] {""} ;
         T001L2_A536PagePublishedStructure = new string[] {""} ;
         T001L2_A40000PageThumbnail_GXI = new string[] {""} ;
         T001L2_n40000PageThumbnail_GXI = new bool[] {false} ;
         T001L2_A525PageType = new string[] {""} ;
         T001L2_A599PageThumbnail = new string[] {""} ;
         T001L2_n599PageThumbnail = new bool[] {false} ;
         T001L26_A523AppVersionId = new Guid[] {Guid.Empty} ;
         T001L26_A516PageId = new Guid[] {Guid.Empty} ;
         Gridlevel_pageRow = new GXWebRow();
         subGridlevel_page_Linesclass = "";
         ROClassString = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXEncryptionTmp = "";
         Gridlevel_pageColumn = new GXWebColumn();
         T001L27_A11OrganisationId = new Guid[] {Guid.Empty} ;
         T001L27_n11OrganisationId = new bool[] {false} ;
         T001L28_A29LocationId = new Guid[] {Guid.Empty} ;
         T001L28_n29LocationId = new bool[] {false} ;
         pr_datastore1 = new DataStoreProvider(context, new GeneXus.Programs.trn_appversion__datastore1(),
            new Object[][] {
            }
         );
         pr_gam = new DataStoreProvider(context, new GeneXus.Programs.trn_appversion__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.trn_appversion__default(),
            new Object[][] {
                new Object[] {
               T001L2_A523AppVersionId, T001L2_A516PageId, T001L2_A541IsPredefined, T001L2_A517PageName, T001L2_A518PageStructure, T001L2_A536PagePublishedStructure, T001L2_A40000PageThumbnail_GXI, T001L2_n40000PageThumbnail_GXI, T001L2_A525PageType, T001L2_A599PageThumbnail,
               T001L2_n599PageThumbnail
               }
               , new Object[] {
               T001L3_A523AppVersionId, T001L3_A516PageId, T001L3_A541IsPredefined, T001L3_A517PageName, T001L3_A518PageStructure, T001L3_A536PagePublishedStructure, T001L3_A40000PageThumbnail_GXI, T001L3_n40000PageThumbnail_GXI, T001L3_A525PageType, T001L3_A599PageThumbnail,
               T001L3_n599PageThumbnail
               }
               , new Object[] {
               T001L4_A523AppVersionId, T001L4_A524AppVersionName, T001L4_A535IsActive, T001L4_A29LocationId, T001L4_n29LocationId, T001L4_A11OrganisationId, T001L4_n11OrganisationId
               }
               , new Object[] {
               T001L5_A523AppVersionId, T001L5_A524AppVersionName, T001L5_A535IsActive, T001L5_A29LocationId, T001L5_n29LocationId, T001L5_A11OrganisationId, T001L5_n11OrganisationId
               }
               , new Object[] {
               T001L6_A11OrganisationId
               }
               , new Object[] {
               T001L7_A29LocationId
               }
               , new Object[] {
               T001L8_A523AppVersionId, T001L8_A524AppVersionName, T001L8_A535IsActive, T001L8_A29LocationId, T001L8_n29LocationId, T001L8_A11OrganisationId, T001L8_n11OrganisationId
               }
               , new Object[] {
               T001L9_A29LocationId
               }
               , new Object[] {
               T001L10_A11OrganisationId
               }
               , new Object[] {
               T001L11_A523AppVersionId
               }
               , new Object[] {
               T001L12_A523AppVersionId
               }
               , new Object[] {
               T001L13_A523AppVersionId
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               T001L17_A29LocationId, T001L17_A11OrganisationId
               }
               , new Object[] {
               T001L18_A29LocationId, T001L18_A11OrganisationId
               }
               , new Object[] {
               T001L19_A523AppVersionId
               }
               , new Object[] {
               T001L20_A523AppVersionId, T001L20_A516PageId, T001L20_A541IsPredefined, T001L20_A517PageName, T001L20_A518PageStructure, T001L20_A536PagePublishedStructure, T001L20_A40000PageThumbnail_GXI, T001L20_n40000PageThumbnail_GXI, T001L20_A525PageType, T001L20_A599PageThumbnail,
               T001L20_n599PageThumbnail
               }
               , new Object[] {
               T001L21_A523AppVersionId, T001L21_A516PageId
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
               T001L26_A523AppVersionId, T001L26_A516PageId
               }
               , new Object[] {
               T001L27_A11OrganisationId
               }
               , new Object[] {
               T001L28_A29LocationId
               }
            }
         );
         Z516PageId = Guid.NewGuid( );
         A516PageId = Guid.NewGuid( );
         Z523AppVersionId = Guid.NewGuid( );
         A523AppVersionId = Guid.NewGuid( );
         AV30Pgmname = "Trn_AppVersion";
         Z541IsPredefined = false;
         A541IsPredefined = false;
         i541IsPredefined = false;
      }

      private short nRcdDeleted_95 ;
      private short nRcdExists_95 ;
      private short nIsMod_95 ;
      private short GxWebError ;
      private short AnyError ;
      private short IsModified ;
      private short IsConfirmed ;
      private short nKeyPressed ;
      private short Gx_BScreen ;
      private short nBlankRcdCount95 ;
      private short RcdFound95 ;
      private short nBlankRcdUsr95 ;
      private short RcdFound6 ;
      private short nIsDirty_95 ;
      private short subGridlevel_page_Backcolorstyle ;
      private short subGridlevel_page_Backstyle ;
      private short gxajaxcallmode ;
      private short subGridlevel_page_Allowselection ;
      private short subGridlevel_page_Allowhovering ;
      private short subGridlevel_page_Allowcollapsing ;
      private short subGridlevel_page_Collapsed ;
      private int nRC_GXsfl_59 ;
      private int nGXsfl_59_idx=1 ;
      private int trnEnded ;
      private int edtAppVersionId_Enabled ;
      private int edtAppVersionName_Enabled ;
      private int edtLocationId_Visible ;
      private int edtLocationId_Enabled ;
      private int edtOrganisationId_Visible ;
      private int edtOrganisationId_Enabled ;
      private int bttBtntrn_enter_Visible ;
      private int bttBtntrn_enter_Enabled ;
      private int bttBtntrn_cancel_Visible ;
      private int bttBtntrn_delete_Visible ;
      private int bttBtntrn_delete_Enabled ;
      private int edtavCombolocationid_Visible ;
      private int edtavCombolocationid_Enabled ;
      private int edtavComboorganisationid_Visible ;
      private int edtavComboorganisationid_Enabled ;
      private int edtPageId_Enabled ;
      private int edtPageName_Enabled ;
      private int edtPageStructure_Enabled ;
      private int edtPagePublishedStructure_Enabled ;
      private int fRowAdded ;
      private int Combo_locationid_Datalistupdateminimumcharacters ;
      private int Combo_locationid_Gxcontroltype ;
      private int Combo_organisationid_Datalistupdateminimumcharacters ;
      private int Combo_organisationid_Gxcontroltype ;
      private int AV31GXV1 ;
      private int subGridlevel_page_Backcolor ;
      private int subGridlevel_page_Allbackcolor ;
      private int defedtPageId_Enabled ;
      private int idxLst ;
      private int subGridlevel_page_Selectedindex ;
      private int subGridlevel_page_Selectioncolor ;
      private int subGridlevel_page_Hoveringcolor ;
      private long GRIDLEVEL_PAGE_nFirstRecordOnPage ;
      private string sPrefix ;
      private string wcpOGx_mode ;
      private string Combo_organisationid_Selectedvalue_get ;
      private string Combo_locationid_Selectedvalue_get ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string GXKey ;
      private string GXDecQS ;
      private string Gx_mode ;
      private string PreviousTooltip ;
      private string PreviousCaption ;
      private string GX_FocusControl ;
      private string edtAppVersionId_Internalname ;
      private string sGXsfl_59_idx="0001" ;
      private string divLayoutmaintable_Internalname ;
      private string divLayoutmaintable_Class ;
      private string divTablemain_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string grpUnnamedgroup1_Internalname ;
      private string divTablecontent_Internalname ;
      private string divTableattributes_Internalname ;
      private string TempTags ;
      private string edtAppVersionId_Jsonclick ;
      private string edtAppVersionName_Internalname ;
      private string edtAppVersionName_Jsonclick ;
      private string divTablesplittedlocationid_Internalname ;
      private string lblTextblocklocationid_Internalname ;
      private string lblTextblocklocationid_Jsonclick ;
      private string Combo_locationid_Caption ;
      private string Combo_locationid_Cls ;
      private string Combo_locationid_Datalistproc ;
      private string Combo_locationid_Internalname ;
      private string edtLocationId_Internalname ;
      private string edtLocationId_Jsonclick ;
      private string divTablesplittedorganisationid_Internalname ;
      private string lblTextblockorganisationid_Internalname ;
      private string lblTextblockorganisationid_Jsonclick ;
      private string Combo_organisationid_Caption ;
      private string Combo_organisationid_Cls ;
      private string Combo_organisationid_Datalistproc ;
      private string Combo_organisationid_Datalistprocparametersprefix ;
      private string Combo_organisationid_Internalname ;
      private string edtOrganisationId_Internalname ;
      private string edtOrganisationId_Jsonclick ;
      private string chkIsActive_Internalname ;
      private string divTableleaflevel_page_Internalname ;
      private string bttBtntrn_enter_Internalname ;
      private string bttBtntrn_enter_Jsonclick ;
      private string bttBtntrn_cancel_Internalname ;
      private string bttBtntrn_cancel_Jsonclick ;
      private string bttBtntrn_delete_Internalname ;
      private string bttBtntrn_delete_Jsonclick ;
      private string divHtml_bottomauxiliarcontrols_Internalname ;
      private string divSectionattribute_locationid_Internalname ;
      private string edtavCombolocationid_Internalname ;
      private string edtavCombolocationid_Jsonclick ;
      private string divSectionattribute_organisationid_Internalname ;
      private string edtavComboorganisationid_Internalname ;
      private string edtavComboorganisationid_Jsonclick ;
      private string sMode95 ;
      private string edtPageId_Internalname ;
      private string edtPageName_Internalname ;
      private string edtPageStructure_Internalname ;
      private string edtPagePublishedStructure_Internalname ;
      private string chkIsPredefined_Internalname ;
      private string cmbPageType_Internalname ;
      private string sStyleString ;
      private string subGridlevel_page_Internalname ;
      private string AV30Pgmname ;
      private string Combo_locationid_Objectcall ;
      private string Combo_locationid_Class ;
      private string Combo_locationid_Icontype ;
      private string Combo_locationid_Icon ;
      private string Combo_locationid_Tooltip ;
      private string Combo_locationid_Selectedvalue_set ;
      private string Combo_locationid_Selectedtext_set ;
      private string Combo_locationid_Selectedtext_get ;
      private string Combo_locationid_Gamoauthtoken ;
      private string Combo_locationid_Ddointernalname ;
      private string Combo_locationid_Titlecontrolalign ;
      private string Combo_locationid_Dropdownoptionstype ;
      private string Combo_locationid_Titlecontrolidtoreplace ;
      private string Combo_locationid_Datalisttype ;
      private string Combo_locationid_Datalistfixedvalues ;
      private string Combo_locationid_Datalistprocparametersprefix ;
      private string Combo_locationid_Remoteservicesparameters ;
      private string Combo_locationid_Htmltemplate ;
      private string Combo_locationid_Multiplevaluestype ;
      private string Combo_locationid_Loadingdata ;
      private string Combo_locationid_Noresultsfound ;
      private string Combo_locationid_Emptyitemtext ;
      private string Combo_locationid_Onlyselectedvalues ;
      private string Combo_locationid_Selectalltext ;
      private string Combo_locationid_Multiplevaluesseparator ;
      private string Combo_locationid_Addnewoptiontext ;
      private string Combo_organisationid_Objectcall ;
      private string Combo_organisationid_Class ;
      private string Combo_organisationid_Icontype ;
      private string Combo_organisationid_Icon ;
      private string Combo_organisationid_Tooltip ;
      private string Combo_organisationid_Selectedvalue_set ;
      private string Combo_organisationid_Selectedtext_set ;
      private string Combo_organisationid_Selectedtext_get ;
      private string Combo_organisationid_Gamoauthtoken ;
      private string Combo_organisationid_Ddointernalname ;
      private string Combo_organisationid_Titlecontrolalign ;
      private string Combo_organisationid_Dropdownoptionstype ;
      private string Combo_organisationid_Titlecontrolidtoreplace ;
      private string Combo_organisationid_Datalisttype ;
      private string Combo_organisationid_Datalistfixedvalues ;
      private string Combo_organisationid_Remoteservicesparameters ;
      private string Combo_organisationid_Htmltemplate ;
      private string Combo_organisationid_Multiplevaluestype ;
      private string Combo_organisationid_Loadingdata ;
      private string Combo_organisationid_Noresultsfound ;
      private string Combo_organisationid_Emptyitemtext ;
      private string Combo_organisationid_Onlyselectedvalues ;
      private string Combo_organisationid_Selectalltext ;
      private string Combo_organisationid_Multiplevaluesseparator ;
      private string Combo_organisationid_Addnewoptiontext ;
      private string hsh ;
      private string sMode6 ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string endTrnMsgTxt ;
      private string endTrnMsgCod ;
      private string GXCCtl ;
      private string GXt_char2 ;
      private string sGXsfl_59_fel_idx="0001" ;
      private string subGridlevel_page_Class ;
      private string subGridlevel_page_Linesclass ;
      private string ROClassString ;
      private string edtPageId_Jsonclick ;
      private string edtPageName_Jsonclick ;
      private string edtPageStructure_Jsonclick ;
      private string edtPagePublishedStructure_Jsonclick ;
      private string cmbPageType_Jsonclick ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXEncryptionTmp ;
      private string subGridlevel_page_Header ;
      private bool Z535IsActive ;
      private bool Z541IsPredefined ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool n29LocationId ;
      private bool n11OrganisationId ;
      private bool wbErr ;
      private bool A535IsActive ;
      private bool bGXsfl_59_Refreshing=false ;
      private bool n599PageThumbnail ;
      private bool n40000PageThumbnail_GXI ;
      private bool Combo_locationid_Enabled ;
      private bool Combo_locationid_Visible ;
      private bool Combo_locationid_Allowmultipleselection ;
      private bool Combo_locationid_Isgriditem ;
      private bool Combo_locationid_Hasdescription ;
      private bool Combo_locationid_Includeonlyselectedoption ;
      private bool Combo_locationid_Includeselectalloption ;
      private bool Combo_locationid_Emptyitem ;
      private bool Combo_locationid_Includeaddnewoption ;
      private bool Combo_organisationid_Enabled ;
      private bool Combo_organisationid_Visible ;
      private bool Combo_organisationid_Allowmultipleselection ;
      private bool Combo_organisationid_Isgriditem ;
      private bool Combo_organisationid_Hasdescription ;
      private bool Combo_organisationid_Includeonlyselectedoption ;
      private bool Combo_organisationid_Includeselectalloption ;
      private bool Combo_organisationid_Emptyitem ;
      private bool Combo_organisationid_Includeaddnewoption ;
      private bool A541IsPredefined ;
      private bool returnInSub ;
      private bool i541IsPredefined ;
      private string A518PageStructure ;
      private string A536PagePublishedStructure ;
      private string AV21Combo_DataJson ;
      private string Z518PageStructure ;
      private string Z536PagePublishedStructure ;
      private string Z524AppVersionName ;
      private string Z517PageName ;
      private string Z525PageType ;
      private string A524AppVersionName ;
      private string A40000PageThumbnail_GXI ;
      private string A517PageName ;
      private string A525PageType ;
      private string AV19ComboSelectedValue ;
      private string AV20ComboSelectedText ;
      private string Z40000PageThumbnail_GXI ;
      private string A599PageThumbnail ;
      private string Z599PageThumbnail ;
      private Guid wcpOAV8AppVersionId ;
      private Guid Z523AppVersionId ;
      private Guid Z29LocationId ;
      private Guid Z11OrganisationId ;
      private Guid N29LocationId ;
      private Guid N11OrganisationId ;
      private Guid Z516PageId ;
      private Guid A29LocationId ;
      private Guid A11OrganisationId ;
      private Guid AV8AppVersionId ;
      private Guid A523AppVersionId ;
      private Guid AV22ComboLocationId ;
      private Guid AV27ComboOrganisationId ;
      private Guid A516PageId ;
      private Guid AV14Insert_LocationId ;
      private Guid AV15Insert_OrganisationId ;
      private Guid AV23Cond_OrganisationId ;
      private IGxSession AV13WebSession ;
      private GXProperties forbiddenHiddens ;
      private GXWebGrid Gridlevel_pageContainer ;
      private GXWebRow Gridlevel_pageRow ;
      private GXWebColumn Gridlevel_pageColumn ;
      private GXUserControl ucCombo_locationid ;
      private GXUserControl ucCombo_organisationid ;
      private GXWebForm Form ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXCheckbox chkIsActive ;
      private GXCheckbox chkIsPredefined ;
      private GXCombobox cmbPageType ;
      private WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsTitleSettingsIcons AV18DDO_TitleSettingsIcons ;
      private GXBaseCollection<WorkWithPlus.workwithplus_web.SdtDVB_SDTComboData_Item> AV17LocationId_Data ;
      private GXBaseCollection<WorkWithPlus.workwithplus_web.SdtDVB_SDTComboData_Item> AV26OrganisationId_Data ;
      private GeneXus.Programs.wwpbaseobjects.SdtWWPContext AV9WWPContext ;
      private WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsTitleSettingsIcons GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 ;
      private GeneXus.Programs.genexussecurity.SdtGAMSession AV24GAMSession ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV25GAMErrors ;
      private WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext AV12TrnContext ;
      private WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext_Attribute AV16TrnContextAtt ;
      private IDataStoreProvider pr_default ;
      private Guid[] T001L8_A523AppVersionId ;
      private string[] T001L8_A524AppVersionName ;
      private bool[] T001L8_A535IsActive ;
      private Guid[] T001L8_A29LocationId ;
      private bool[] T001L8_n29LocationId ;
      private Guid[] T001L8_A11OrganisationId ;
      private bool[] T001L8_n11OrganisationId ;
      private Guid[] T001L7_A29LocationId ;
      private bool[] T001L7_n29LocationId ;
      private Guid[] T001L6_A11OrganisationId ;
      private bool[] T001L6_n11OrganisationId ;
      private Guid[] T001L9_A29LocationId ;
      private bool[] T001L9_n29LocationId ;
      private Guid[] T001L10_A11OrganisationId ;
      private bool[] T001L10_n11OrganisationId ;
      private Guid[] T001L11_A523AppVersionId ;
      private Guid[] T001L5_A523AppVersionId ;
      private string[] T001L5_A524AppVersionName ;
      private bool[] T001L5_A535IsActive ;
      private Guid[] T001L5_A29LocationId ;
      private bool[] T001L5_n29LocationId ;
      private Guid[] T001L5_A11OrganisationId ;
      private bool[] T001L5_n11OrganisationId ;
      private Guid[] T001L12_A523AppVersionId ;
      private Guid[] T001L13_A523AppVersionId ;
      private Guid[] T001L4_A523AppVersionId ;
      private string[] T001L4_A524AppVersionName ;
      private bool[] T001L4_A535IsActive ;
      private Guid[] T001L4_A29LocationId ;
      private bool[] T001L4_n29LocationId ;
      private Guid[] T001L4_A11OrganisationId ;
      private bool[] T001L4_n11OrganisationId ;
      private Guid[] T001L17_A29LocationId ;
      private bool[] T001L17_n29LocationId ;
      private Guid[] T001L17_A11OrganisationId ;
      private bool[] T001L17_n11OrganisationId ;
      private Guid[] T001L18_A29LocationId ;
      private bool[] T001L18_n29LocationId ;
      private Guid[] T001L18_A11OrganisationId ;
      private bool[] T001L18_n11OrganisationId ;
      private Guid[] T001L19_A523AppVersionId ;
      private Guid[] T001L20_A523AppVersionId ;
      private Guid[] T001L20_A516PageId ;
      private bool[] T001L20_A541IsPredefined ;
      private string[] T001L20_A517PageName ;
      private string[] T001L20_A518PageStructure ;
      private string[] T001L20_A536PagePublishedStructure ;
      private string[] T001L20_A40000PageThumbnail_GXI ;
      private bool[] T001L20_n40000PageThumbnail_GXI ;
      private string[] T001L20_A525PageType ;
      private string[] T001L20_A599PageThumbnail ;
      private bool[] T001L20_n599PageThumbnail ;
      private Guid[] T001L21_A523AppVersionId ;
      private Guid[] T001L21_A516PageId ;
      private Guid[] T001L3_A523AppVersionId ;
      private Guid[] T001L3_A516PageId ;
      private bool[] T001L3_A541IsPredefined ;
      private string[] T001L3_A517PageName ;
      private string[] T001L3_A518PageStructure ;
      private string[] T001L3_A536PagePublishedStructure ;
      private string[] T001L3_A40000PageThumbnail_GXI ;
      private bool[] T001L3_n40000PageThumbnail_GXI ;
      private string[] T001L3_A525PageType ;
      private string[] T001L3_A599PageThumbnail ;
      private bool[] T001L3_n599PageThumbnail ;
      private Guid[] T001L2_A523AppVersionId ;
      private Guid[] T001L2_A516PageId ;
      private bool[] T001L2_A541IsPredefined ;
      private string[] T001L2_A517PageName ;
      private string[] T001L2_A518PageStructure ;
      private string[] T001L2_A536PagePublishedStructure ;
      private string[] T001L2_A40000PageThumbnail_GXI ;
      private bool[] T001L2_n40000PageThumbnail_GXI ;
      private string[] T001L2_A525PageType ;
      private string[] T001L2_A599PageThumbnail ;
      private bool[] T001L2_n599PageThumbnail ;
      private Guid[] T001L26_A523AppVersionId ;
      private Guid[] T001L26_A516PageId ;
      private Guid[] T001L27_A11OrganisationId ;
      private bool[] T001L27_n11OrganisationId ;
      private Guid[] T001L28_A29LocationId ;
      private bool[] T001L28_n29LocationId ;
      private IDataStoreProvider pr_datastore1 ;
      private IDataStoreProvider pr_gam ;
   }

   public class trn_appversion__datastore1 : DataStoreHelperBase, IDataStoreHelper
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

 public class trn_appversion__gam : DataStoreHelperBase, IDataStoreHelper
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

public class trn_appversion__default : DataStoreHelperBase, IDataStoreHelper
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
      ,new ForEachCursor(def[9])
      ,new ForEachCursor(def[10])
      ,new ForEachCursor(def[11])
      ,new UpdateCursor(def[12])
      ,new UpdateCursor(def[13])
      ,new UpdateCursor(def[14])
      ,new ForEachCursor(def[15])
      ,new ForEachCursor(def[16])
      ,new ForEachCursor(def[17])
      ,new ForEachCursor(def[18])
      ,new ForEachCursor(def[19])
      ,new UpdateCursor(def[20])
      ,new UpdateCursor(def[21])
      ,new UpdateCursor(def[22])
      ,new UpdateCursor(def[23])
      ,new ForEachCursor(def[24])
      ,new ForEachCursor(def[25])
      ,new ForEachCursor(def[26])
    };
 }

 private static CursorDef[] def;
 private void cursorDefinitions( )
 {
    if ( def == null )
    {
       Object[] prmT001L2;
       prmT001L2 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("PageId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L3;
       prmT001L3 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("PageId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L4;
       prmT001L4 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L5;
       prmT001L5 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L6;
       prmT001L6 = new Object[] {
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmT001L7;
       prmT001L7 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmT001L8;
       prmT001L8 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L9;
       prmT001L9 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmT001L10;
       prmT001L10 = new Object[] {
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmT001L11;
       prmT001L11 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L12;
       prmT001L12 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L13;
       prmT001L13 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L14;
       prmT001L14 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("AppVersionName",GXType.VarChar,100,0) ,
       new ParDef("IsActive",GXType.Boolean,4,0) ,
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmT001L15;
       prmT001L15 = new Object[] {
       new ParDef("AppVersionName",GXType.VarChar,100,0) ,
       new ParDef("IsActive",GXType.Boolean,4,0) ,
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L16;
       prmT001L16 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L17;
       prmT001L17 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L18;
       prmT001L18 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L19;
       prmT001L19 = new Object[] {
       };
       Object[] prmT001L20;
       prmT001L20 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("PageId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L21;
       prmT001L21 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("PageId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L22;
       prmT001L22 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("PageId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("IsPredefined",GXType.Boolean,4,0) ,
       new ParDef("PageName",GXType.VarChar,100,0) ,
       new ParDef("PageStructure",GXType.LongVarChar,2097152,0) ,
       new ParDef("PagePublishedStructure",GXType.LongVarChar,2097152,0) ,
       new ParDef("PageThumbnail",GXType.Byte,1024,0){Nullable=true,InDB=false} ,
       new ParDef("PageThumbnail_GXI",GXType.VarChar,2048,0){Nullable=true,AddAtt=true, ImgIdx=6, Tbl="Trn_AppVersionPage", Fld="PageThumbnail"} ,
       new ParDef("PageType",GXType.VarChar,40,0)
       };
       Object[] prmT001L23;
       prmT001L23 = new Object[] {
       new ParDef("IsPredefined",GXType.Boolean,4,0) ,
       new ParDef("PageName",GXType.VarChar,100,0) ,
       new ParDef("PageStructure",GXType.LongVarChar,2097152,0) ,
       new ParDef("PagePublishedStructure",GXType.LongVarChar,2097152,0) ,
       new ParDef("PageType",GXType.VarChar,40,0) ,
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("PageId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L24;
       prmT001L24 = new Object[] {
       new ParDef("PageThumbnail",GXType.Byte,1024,0){Nullable=true,InDB=false} ,
       new ParDef("PageThumbnail_GXI",GXType.VarChar,2048,0){Nullable=true,AddAtt=true, ImgIdx=0, Tbl="Trn_AppVersionPage", Fld="PageThumbnail"} ,
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("PageId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L25;
       prmT001L25 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("PageId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L26;
       prmT001L26 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001L27;
       prmT001L27 = new Object[] {
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       Object[] prmT001L28;
       prmT001L28 = new Object[] {
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0){Nullable=true} ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0){Nullable=true}
       };
       def= new CursorDef[] {
           new CursorDef("T001L2", "SELECT AppVersionId, PageId, IsPredefined, PageName, PageStructure, PagePublishedStructure, PageThumbnail_GXI, PageType, PageThumbnail FROM Trn_AppVersionPage WHERE AppVersionId = :AppVersionId AND PageId = :PageId  FOR UPDATE OF Trn_AppVersionPage NOWAIT",true, GxErrorMask.GX_NOMASK, false, this,prmT001L2,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001L3", "SELECT AppVersionId, PageId, IsPredefined, PageName, PageStructure, PagePublishedStructure, PageThumbnail_GXI, PageType, PageThumbnail FROM Trn_AppVersionPage WHERE AppVersionId = :AppVersionId AND PageId = :PageId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L3,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001L4", "SELECT AppVersionId, AppVersionName, IsActive, LocationId, OrganisationId FROM Trn_AppVersion WHERE AppVersionId = :AppVersionId  FOR UPDATE OF Trn_AppVersion NOWAIT",true, GxErrorMask.GX_NOMASK, false, this,prmT001L4,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001L5", "SELECT AppVersionId, AppVersionName, IsActive, LocationId, OrganisationId FROM Trn_AppVersion WHERE AppVersionId = :AppVersionId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L5,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001L6", "SELECT OrganisationId FROM Trn_Organisation WHERE OrganisationId = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L6,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001L7", "SELECT LocationId FROM Trn_Location WHERE LocationId = :LocationId AND OrganisationId = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L7,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001L8", "SELECT TM1.AppVersionId, TM1.AppVersionName, TM1.IsActive, TM1.LocationId, TM1.OrganisationId FROM Trn_AppVersion TM1 WHERE TM1.AppVersionId = :AppVersionId ORDER BY TM1.AppVersionId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L8,100, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001L9", "SELECT LocationId FROM Trn_Location WHERE LocationId = :LocationId AND OrganisationId = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L9,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001L10", "SELECT OrganisationId FROM Trn_Organisation WHERE OrganisationId = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L10,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001L11", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :AppVersionId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L11,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001L12", "SELECT AppVersionId FROM Trn_AppVersion WHERE ( AppVersionId > :AppVersionId) ORDER BY AppVersionId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L12,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("T001L13", "SELECT AppVersionId FROM Trn_AppVersion WHERE ( AppVersionId < :AppVersionId) ORDER BY AppVersionId DESC ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L13,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("T001L14", "SAVEPOINT gxupdate;INSERT INTO Trn_AppVersion(AppVersionId, AppVersionName, IsActive, LocationId, OrganisationId) VALUES(:AppVersionId, :AppVersionName, :IsActive, :LocationId, :OrganisationId);RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK,prmT001L14)
          ,new CursorDef("T001L15", "SAVEPOINT gxupdate;UPDATE Trn_AppVersion SET AppVersionName=:AppVersionName, IsActive=:IsActive, LocationId=:LocationId, OrganisationId=:OrganisationId  WHERE AppVersionId = :AppVersionId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK,prmT001L15)
          ,new CursorDef("T001L16", "SAVEPOINT gxupdate;DELETE FROM Trn_AppVersion  WHERE AppVersionId = :AppVersionId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK,prmT001L16)
          ,new CursorDef("T001L17", "SELECT LocationId, OrganisationId FROM Trn_Location WHERE PublishedActiveAppVersionId = :AppVersionId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L17,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("T001L18", "SELECT LocationId, OrganisationId FROM Trn_Location WHERE ActiveAppVersionId = :AppVersionId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L18,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("T001L19", "SELECT AppVersionId FROM Trn_AppVersion ORDER BY AppVersionId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L19,100, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001L20", "SELECT AppVersionId, PageId, IsPredefined, PageName, PageStructure, PagePublishedStructure, PageThumbnail_GXI, PageType, PageThumbnail FROM Trn_AppVersionPage WHERE AppVersionId = :AppVersionId and PageId = :PageId ORDER BY AppVersionId, PageId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L20,11, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001L21", "SELECT AppVersionId, PageId FROM Trn_AppVersionPage WHERE AppVersionId = :AppVersionId AND PageId = :PageId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L21,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001L22", "SAVEPOINT gxupdate;INSERT INTO Trn_AppVersionPage(AppVersionId, PageId, IsPredefined, PageName, PageStructure, PagePublishedStructure, PageThumbnail, PageThumbnail_GXI, PageType) VALUES(:AppVersionId, :PageId, :IsPredefined, :PageName, :PageStructure, :PagePublishedStructure, :PageThumbnail, :PageThumbnail_GXI, :PageType);RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT,prmT001L22)
          ,new CursorDef("T001L23", "SAVEPOINT gxupdate;UPDATE Trn_AppVersionPage SET IsPredefined=:IsPredefined, PageName=:PageName, PageStructure=:PageStructure, PagePublishedStructure=:PagePublishedStructure, PageType=:PageType  WHERE AppVersionId = :AppVersionId AND PageId = :PageId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK,prmT001L23)
          ,new CursorDef("T001L24", "SAVEPOINT gxupdate;UPDATE Trn_AppVersionPage SET PageThumbnail=:PageThumbnail, PageThumbnail_GXI=:PageThumbnail_GXI  WHERE AppVersionId = :AppVersionId AND PageId = :PageId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK,prmT001L24)
          ,new CursorDef("T001L25", "SAVEPOINT gxupdate;DELETE FROM Trn_AppVersionPage  WHERE AppVersionId = :AppVersionId AND PageId = :PageId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK,prmT001L25)
          ,new CursorDef("T001L26", "SELECT AppVersionId, PageId FROM Trn_AppVersionPage WHERE AppVersionId = :AppVersionId ORDER BY AppVersionId, PageId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L26,11, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001L27", "SELECT OrganisationId FROM Trn_Organisation WHERE OrganisationId = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L27,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001L28", "SELECT LocationId FROM Trn_Location WHERE LocationId = :LocationId AND OrganisationId = :OrganisationId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001L28,1, GxCacheFrequency.OFF ,true,false )
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
             ((bool[]) buf[2])[0] = rslt.getBool(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
             ((string[]) buf[5])[0] = rslt.getLongVarchar(6);
             ((string[]) buf[6])[0] = rslt.getMultimediaUri(7);
             ((bool[]) buf[7])[0] = rslt.wasNull(7);
             ((string[]) buf[8])[0] = rslt.getVarchar(8);
             ((string[]) buf[9])[0] = rslt.getMultimediaFile(9, rslt.getVarchar(7));
             ((bool[]) buf[10])[0] = rslt.wasNull(9);
             return;
          case 1 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             ((bool[]) buf[2])[0] = rslt.getBool(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
             ((string[]) buf[5])[0] = rslt.getLongVarchar(6);
             ((string[]) buf[6])[0] = rslt.getMultimediaUri(7);
             ((bool[]) buf[7])[0] = rslt.wasNull(7);
             ((string[]) buf[8])[0] = rslt.getVarchar(8);
             ((string[]) buf[9])[0] = rslt.getMultimediaFile(9, rslt.getVarchar(7));
             ((bool[]) buf[10])[0] = rslt.wasNull(9);
             return;
          case 2 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((string[]) buf[1])[0] = rslt.getVarchar(2);
             ((bool[]) buf[2])[0] = rslt.getBool(3);
             ((Guid[]) buf[3])[0] = rslt.getGuid(4);
             ((bool[]) buf[4])[0] = rslt.wasNull(4);
             ((Guid[]) buf[5])[0] = rslt.getGuid(5);
             ((bool[]) buf[6])[0] = rslt.wasNull(5);
             return;
          case 3 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((string[]) buf[1])[0] = rslt.getVarchar(2);
             ((bool[]) buf[2])[0] = rslt.getBool(3);
             ((Guid[]) buf[3])[0] = rslt.getGuid(4);
             ((bool[]) buf[4])[0] = rslt.wasNull(4);
             ((Guid[]) buf[5])[0] = rslt.getGuid(5);
             ((bool[]) buf[6])[0] = rslt.wasNull(5);
             return;
          case 4 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             return;
          case 5 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             return;
          case 6 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((string[]) buf[1])[0] = rslt.getVarchar(2);
             ((bool[]) buf[2])[0] = rslt.getBool(3);
             ((Guid[]) buf[3])[0] = rslt.getGuid(4);
             ((bool[]) buf[4])[0] = rslt.wasNull(4);
             ((Guid[]) buf[5])[0] = rslt.getGuid(5);
             ((bool[]) buf[6])[0] = rslt.wasNull(5);
             return;
          case 7 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             return;
          case 8 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
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
          case 15 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             return;
          case 16 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             return;
          case 17 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             return;
          case 18 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             ((bool[]) buf[2])[0] = rslt.getBool(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
             ((string[]) buf[5])[0] = rslt.getLongVarchar(6);
             ((string[]) buf[6])[0] = rslt.getMultimediaUri(7);
             ((bool[]) buf[7])[0] = rslt.wasNull(7);
             ((string[]) buf[8])[0] = rslt.getVarchar(8);
             ((string[]) buf[9])[0] = rslt.getMultimediaFile(9, rslt.getVarchar(7));
             ((bool[]) buf[10])[0] = rslt.wasNull(9);
             return;
          case 19 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             return;
          case 24 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             return;
          case 25 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             return;
          case 26 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             return;
    }
 }

}

}
