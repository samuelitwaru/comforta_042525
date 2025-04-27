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
using GeneXus.Http.Server;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace GeneXus.Programs {
   public class wp_dynamicformtrn_locationdynamicformwc : GXWebComponent
   {
      public wp_dynamicformtrn_locationdynamicformwc( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
            context.SetDefaultTheme("WorkWithPlusDS", true);
         }
      }

      public wp_dynamicformtrn_locationdynamicformwc( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( short aP0_WWPFormType ,
                           bool aP1_WWPFormIsForDynamicValidations )
      {
         this.AV9WWPFormType = aP0_WWPFormType;
         this.AV76WWPFormIsForDynamicValidations = aP1_WWPFormIsForDynamicValidations;
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      public override void SetPrefix( string sPPrefix )
      {
         sPrefix = sPPrefix;
      }

      protected override void createObjects( )
      {
         dynavOrganisationidfilter = new GXCombobox();
         dynavLocationidfilter = new GXCombobox();
         cmbavActiongroup = new GXCombobox();
         cmbWWPFormType = new GXCombobox();
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
            if ( nGotPars == 0 )
            {
               entryPointCalled = false;
               gxfirstwebparm = GetFirstPar( "WWPFormType");
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
               else if ( StringUtil.StrCmp(gxfirstwebparm, "dyncomponent") == 0 )
               {
                  setAjaxEventMode();
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  nDynComponent = 1;
                  sCompPrefix = GetPar( "sCompPrefix");
                  sSFPrefix = GetPar( "sSFPrefix");
                  AV9WWPFormType = (short)(Math.Round(NumberUtil.Val( GetPar( "WWPFormType"), "."), 18, MidpointRounding.ToEven));
                  AssignAttri(sPrefix, false, "AV9WWPFormType", StringUtil.Str( (decimal)(AV9WWPFormType), 1, 0));
                  AV76WWPFormIsForDynamicValidations = StringUtil.StrToBool( GetPar( "WWPFormIsForDynamicValidations"));
                  AssignAttri(sPrefix, false, "AV76WWPFormIsForDynamicValidations", AV76WWPFormIsForDynamicValidations);
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix,(short)AV9WWPFormType,(bool)AV76WWPFormIsForDynamicValidations});
                  componentstart();
                  context.httpAjaxContext.ajax_rspStartCmp(sPrefix);
                  componentdraw();
                  context.httpAjaxContext.ajax_rspEndCmp();
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxCallCrl"+"_"+"vORGANISATIONIDFILTER") == 0 )
               {
                  setAjaxCallMode();
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  GXDLVvORGANISATIONIDFILTERBB2( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxCallCrl"+"_"+"vLOCATIONIDFILTER") == 0 )
               {
                  AV53OrganisationIdFilter = StringUtil.StrToGuid( GetPar( "OrganisationIdFilter"));
                  AssignAttri(sPrefix, false, "AV53OrganisationIdFilter", AV53OrganisationIdFilter.ToString());
                  setAjaxCallMode();
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  GXDLVvLOCATIONIDFILTERBB2( AV53OrganisationIdFilter) ;
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
                  gxfirstwebparm = GetFirstPar( "WWPFormType");
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
               {
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  gxfirstwebparm = GetFirstPar( "WWPFormType");
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Grid") == 0 )
               {
                  gxnrGrid_newrow_invoke( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Grid") == 0 )
               {
                  gxgrGrid_refresh_invoke( ) ;
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
            }
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.IsLocalStorageSupported( ) )
            {
               context.PushCurrentUrl();
            }
         }
      }

      protected void gxnrGrid_newrow_invoke( )
      {
         nRC_GXsfl_47 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_47"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_47_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_47_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_47_idx = GetPar( "sGXsfl_47_idx");
         sPrefix = GetPar( "sPrefix");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGrid_newrow( ) ;
         /* End function gxnrGrid_newrow_invoke */
      }

      protected void gxgrGrid_refresh_invoke( )
      {
         subGrid_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subGrid_Rows"), "."), 18, MidpointRounding.ToEven));
         AV50OrderedBy = (short)(Math.Round(NumberUtil.Val( GetPar( "OrderedBy"), "."), 18, MidpointRounding.ToEven));
         AV52OrderedDsc = StringUtil.StrToBool( GetPar( "OrderedDsc"));
         AV22FilterFullText = GetPar( "FilterFullText");
         AV9WWPFormType = (short)(Math.Round(NumberUtil.Val( GetPar( "WWPFormType"), "."), 18, MidpointRounding.ToEven));
         dynavLocationidfilter.FromJSonString( GetNextPar( ));
         AV43LocationIdFilter = StringUtil.StrToGuid( GetPar( "LocationIdFilter"));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV73WWPContext);
         AV45ManageFiltersExecutionStep = (short)(Math.Round(NumberUtil.Val( GetPar( "ManageFiltersExecutionStep"), "."), 18, MidpointRounding.ToEven));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV11ColumnsSelector);
         AV78Pgmname = GetPar( "Pgmname");
         AV65TFWWPFormTitle = GetPar( "TFWWPFormTitle");
         AV66TFWWPFormTitle_Sel = GetPar( "TFWWPFormTitle_Sel");
         AV63TFWWPFormReferenceName = GetPar( "TFWWPFormReferenceName");
         AV64TFWWPFormReferenceName_Sel = GetPar( "TFWWPFormReferenceName_Sel");
         AV59TFWWPFormDate = context.localUtil.ParseDTimeParm( GetPar( "TFWWPFormDate"));
         AV60TFWWPFormDate_To = context.localUtil.ParseDTimeParm( GetPar( "TFWWPFormDate_To"));
         AV67TFWWPFormVersionNumber = (short)(Math.Round(NumberUtil.Val( GetPar( "TFWWPFormVersionNumber"), "."), 18, MidpointRounding.ToEven));
         AV68TFWWPFormVersionNumber_To = (short)(Math.Round(NumberUtil.Val( GetPar( "TFWWPFormVersionNumber_To"), "."), 18, MidpointRounding.ToEven));
         AV61TFWWPFormLatestVersionNumber = (short)(Math.Round(NumberUtil.Val( GetPar( "TFWWPFormLatestVersionNumber"), "."), 18, MidpointRounding.ToEven));
         AV62TFWWPFormLatestVersionNumber_To = (short)(Math.Round(NumberUtil.Val( GetPar( "TFWWPFormLatestVersionNumber_To"), "."), 18, MidpointRounding.ToEven));
         AV76WWPFormIsForDynamicValidations = StringUtil.StrToBool( GetPar( "WWPFormIsForDynamicValidations"));
         AV39IsAuthorized_UserActionEdit = StringUtil.StrToBool( GetPar( "IsAuthorized_UserActionEdit"));
         AV38IsAuthorized_UserActionDisplay = StringUtil.StrToBool( GetPar( "IsAuthorized_UserActionDisplay"));
         AV36IsAuthorized_UserActionCopy = StringUtil.StrToBool( GetPar( "IsAuthorized_UserActionCopy"));
         AV37IsAuthorized_UserActionDelete = StringUtil.StrToBool( GetPar( "IsAuthorized_UserActionDelete"));
         AV40IsAuthorized_UserActionFilledForms = StringUtil.StrToBool( GetPar( "IsAuthorized_UserActionFilledForms"));
         AV41IsAuthorized_UserActionFillOutForm = StringUtil.StrToBool( GetPar( "IsAuthorized_UserActionFillOutForm"));
         dynavOrganisationidfilter.FromJSonString( GetNextPar( ));
         AV53OrganisationIdFilter = StringUtil.StrToGuid( GetPar( "OrganisationIdFilter"));
         A366LocationDynamicFormId = StringUtil.StrToGuid( GetPar( "LocationDynamicFormId"));
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGrid_refresh( subGrid_Rows, AV50OrderedBy, AV52OrderedDsc, AV22FilterFullText, AV9WWPFormType, AV43LocationIdFilter, AV73WWPContext, AV45ManageFiltersExecutionStep, AV11ColumnsSelector, AV78Pgmname, AV65TFWWPFormTitle, AV66TFWWPFormTitle_Sel, AV63TFWWPFormReferenceName, AV64TFWWPFormReferenceName_Sel, AV59TFWWPFormDate, AV60TFWWPFormDate_To, AV67TFWWPFormVersionNumber, AV68TFWWPFormVersionNumber_To, AV61TFWWPFormLatestVersionNumber, AV62TFWWPFormLatestVersionNumber_To, AV76WWPFormIsForDynamicValidations, AV39IsAuthorized_UserActionEdit, AV38IsAuthorized_UserActionDisplay, AV36IsAuthorized_UserActionCopy, AV37IsAuthorized_UserActionDelete, AV40IsAuthorized_UserActionFilledForms, AV41IsAuthorized_UserActionFillOutForm, AV53OrganisationIdFilter, A366LocationDynamicFormId, sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGrid_refresh_invoke */
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               ValidateSpaRequest();
            }
            PABB2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               AV78Pgmname = "WP_DynamicFormTrn_LocationDynamicFormWC";
               GXVvORGANISATIONIDFILTER_htmlBB2( ) ;
               WSBB2( ) ;
               if ( ! isAjaxCallMode( ) )
               {
                  if ( nDynComponent == 0 )
                  {
                     throw new System.Net.WebException("WebComponent is not allowed to run") ;
                  }
               }
            }
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

      protected void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      protected void RenderHtmlOpenForm( )
      {
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
            context.WriteHtmlText( "<title>") ;
            context.SendWebValue( context.GetMessage( "WP_Dynamic Form Trn_Location Dynamic Form WC", "")) ;
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
         }
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
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 1918140), false, true);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 1918140), false, true);
         context.AddJavascriptSource("calendar-"+StringUtil.Substring( context.GetLanguageProperty( "culture"), 1, 2)+".js", "?"+context.GetBuildNumber( 1918140), false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/DVPaginationBar/DVPaginationBarRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/ConfirmPanel/BootstrapConfirmPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/GridEmpowerer/GridEmpowererRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/daterangepicker/locales.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/daterangepicker/wwp-daterangepicker.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/daterangepicker/moment.min.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/daterangepicker/daterangepicker.min.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/DateRangePicker/DateRangePickerRender.js", "", false, true);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            context.CloseHtmlHeader();
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
            FormProcess = " data-HasEnter=\"false\" data-Skiponenter=\"false\"";
            context.WriteHtmlText( "<body ") ;
            if ( StringUtil.StrCmp(context.GetLanguageProperty( "rtl"), "true") == 0 )
            {
               context.WriteHtmlText( " dir=\"rtl\" ") ;
            }
            bodyStyle = "";
            if ( nGXWrapped == 0 )
            {
               bodyStyle += "-moz-opacity:0;opacity:0;";
            }
            context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
            context.WriteHtmlText( FormProcess+">") ;
            context.skipLines(1);
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wp_dynamicformtrn_locationdynamicformwc.aspx", new object[] {UrlEncode(StringUtil.LTrimStr(AV9WWPFormType,1,0)),UrlEncode(StringUtil.BoolToStr(AV76WWPFormIsForDynamicValidations))}, new string[] {"WWPFormType","WWPFormIsForDynamicValidations"}) +"\">") ;
            GxWebStd.gx_hidden_field( context, "_EventName", "");
            GxWebStd.gx_hidden_field( context, "_EventGridId", "");
            GxWebStd.gx_hidden_field( context, "_EventRowId", "");
            context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
            AssignProp(sPrefix, false, "FORM", "Class", "form-horizontal Form", true);
         }
         else
         {
            bool toggleHtmlOutput = isOutputEnabled( );
            if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableOutput();
               }
            }
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gxwebcomponent-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            if ( toggleHtmlOutput )
            {
               if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableOutput();
                  }
               }
            }
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWWPCONTEXT", AV73WWPContext);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWWPCONTEXT", AV73WWPContext);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWWPCONTEXT", GetSecureSignedToken( sPrefix, AV73WWPContext, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vPGMNAME", StringUtil.RTrim( AV78Pgmname));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPGMNAME", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( AV78Pgmname, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONEDIT", AV39IsAuthorized_UserActionEdit);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONEDIT", GetSecureSignedToken( sPrefix, AV39IsAuthorized_UserActionEdit, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONDISPLAY", AV38IsAuthorized_UserActionDisplay);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONDISPLAY", GetSecureSignedToken( sPrefix, AV38IsAuthorized_UserActionDisplay, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONCOPY", AV36IsAuthorized_UserActionCopy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONCOPY", GetSecureSignedToken( sPrefix, AV36IsAuthorized_UserActionCopy, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONDELETE", AV37IsAuthorized_UserActionDelete);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONDELETE", GetSecureSignedToken( sPrefix, AV37IsAuthorized_UserActionDelete, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONFILLEDFORMS", AV40IsAuthorized_UserActionFilledForms);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONFILLEDFORMS", GetSecureSignedToken( sPrefix, AV40IsAuthorized_UserActionFilledForms, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONFILLOUTFORM", AV41IsAuthorized_UserActionFillOutForm);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONFILLOUTFORM", GetSecureSignedToken( sPrefix, AV41IsAuthorized_UserActionFillOutForm, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"LOCATIONDYNAMICFORMID", A366LocationDynamicFormId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_LOCATIONDYNAMICFORMID", GetSecureSignedToken( sPrefix, A366LocationDynamicFormId, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         GxWebStd.gx_hidden_field( context, sPrefix+"GXH_vORDEREDBY", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV50OrderedBy), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"GXH_vORDEREDDSC", StringUtil.BoolToStr( AV52OrderedDsc));
         GxWebStd.gx_hidden_field( context, sPrefix+"GXH_vFILTERFULLTEXT", AV22FilterFullText);
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_47", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_47), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vMANAGEFILTERSDATA", AV44ManageFiltersData);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vMANAGEFILTERSDATA", AV44ManageFiltersData);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vGRIDCURRENTPAGE", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV26GridCurrentPage), 10, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vGRIDPAGECOUNT", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV27GridPageCount), 10, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vGRIDAPPLIEDFILTERS", AV25GridAppliedFilters);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vDDO_TITLESETTINGSICONS", AV15DDO_TitleSettingsIcons);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vDDO_TITLESETTINGSICONS", AV15DDO_TitleSettingsIcons);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vCOLUMNSSELECTOR", AV11ColumnsSelector);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vCOLUMNSSELECTOR", AV11ColumnsSelector);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vDDO_WWPFORMDATEAUXDATE", context.localUtil.DToC( AV16DDO_WWPFormDateAuxDate, 0, "/"));
         GxWebStd.gx_hidden_field( context, sPrefix+"vDDO_WWPFORMDATEAUXDATETO", context.localUtil.DToC( AV18DDO_WWPFormDateAuxDateTo, 0, "/"));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV9WWPFormType", StringUtil.LTrim( StringUtil.NToC( (decimal)(wcpOAV9WWPFormType), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"wcpOAV76WWPFormIsForDynamicValidations", wcpOAV76WWPFormIsForDynamicValidations);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWWPCONTEXT", AV73WWPContext);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWWPCONTEXT", AV73WWPContext);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWWPCONTEXT", GetSecureSignedToken( sPrefix, AV73WWPContext, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vMANAGEFILTERSEXECUTIONSTEP", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV45ManageFiltersExecutionStep), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vPGMNAME", StringUtil.RTrim( AV78Pgmname));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPGMNAME", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( AV78Pgmname, "")), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vORDEREDBY", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV50OrderedBy), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vORDEREDDSC", AV52OrderedDsc);
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMTITLE", AV65TFWWPFormTitle);
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMTITLE_SEL", AV66TFWWPFormTitle_Sel);
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMREFERENCENAME", AV63TFWWPFormReferenceName);
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMREFERENCENAME_SEL", AV64TFWWPFormReferenceName_Sel);
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMDATE", context.localUtil.TToC( AV59TFWWPFormDate, 10, 8, 0, 0, "/", ":", " "));
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMDATE_TO", context.localUtil.TToC( AV60TFWWPFormDate_To, 10, 8, 0, 0, "/", ":", " "));
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMVERSIONNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV67TFWWPFormVersionNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMVERSIONNUMBER_TO", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV68TFWWPFormVersionNumber_To), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMLATESTVERSIONNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV61TFWWPFormLatestVersionNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMLATESTVERSIONNUMBER_TO", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV62TFWWPFormLatestVersionNumber_To), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vWWPFORMTYPE", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV9WWPFormType), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vWWPFORMISFORDYNAMICVALIDATIONS", AV76WWPFormIsForDynamicValidations);
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONEDIT", AV39IsAuthorized_UserActionEdit);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONEDIT", GetSecureSignedToken( sPrefix, AV39IsAuthorized_UserActionEdit, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONDISPLAY", AV38IsAuthorized_UserActionDisplay);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONDISPLAY", GetSecureSignedToken( sPrefix, AV38IsAuthorized_UserActionDisplay, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONCOPY", AV36IsAuthorized_UserActionCopy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONCOPY", GetSecureSignedToken( sPrefix, AV36IsAuthorized_UserActionCopy, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONDELETE", AV37IsAuthorized_UserActionDelete);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONDELETE", GetSecureSignedToken( sPrefix, AV37IsAuthorized_UserActionDelete, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONFILLEDFORMS", AV40IsAuthorized_UserActionFilledForms);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONFILLEDFORMS", GetSecureSignedToken( sPrefix, AV40IsAuthorized_UserActionFilledForms, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONFILLOUTFORM", AV41IsAuthorized_UserActionFillOutForm);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONFILLOUTFORM", GetSecureSignedToken( sPrefix, AV41IsAuthorized_UserActionFillOutForm, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vGRIDSTATE", AV28GridState);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vGRIDSTATE", AV28GridState);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vLOCATIONID", AV5LocationId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"vORGANISATIONID", AV6OrganisationId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"vRESULTMSG", AV56ResultMsg);
         GxWebStd.gx_hidden_field( context, sPrefix+"LOCATIONDYNAMICFORMID", A366LocationDynamicFormId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_LOCATIONDYNAMICFORMID", GetSecureSignedToken( sPrefix, A366LocationDynamicFormId, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_MANAGEFILTERS_Icontype", StringUtil.RTrim( Ddo_managefilters_Icontype));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_MANAGEFILTERS_Icon", StringUtil.RTrim( Ddo_managefilters_Icon));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_MANAGEFILTERS_Tooltip", StringUtil.RTrim( Ddo_managefilters_Tooltip));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_MANAGEFILTERS_Cls", StringUtil.RTrim( Ddo_managefilters_Cls));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Class", StringUtil.RTrim( Gridpaginationbar_Class));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Showfirst", StringUtil.BoolToStr( Gridpaginationbar_Showfirst));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Showprevious", StringUtil.BoolToStr( Gridpaginationbar_Showprevious));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Shownext", StringUtil.BoolToStr( Gridpaginationbar_Shownext));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Showlast", StringUtil.BoolToStr( Gridpaginationbar_Showlast));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Pagestoshow", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridpaginationbar_Pagestoshow), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Pagingbuttonsposition", StringUtil.RTrim( Gridpaginationbar_Pagingbuttonsposition));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Pagingcaptionposition", StringUtil.RTrim( Gridpaginationbar_Pagingcaptionposition));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Emptygridclass", StringUtil.RTrim( Gridpaginationbar_Emptygridclass));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Rowsperpageselector", StringUtil.BoolToStr( Gridpaginationbar_Rowsperpageselector));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Rowsperpageselectedvalue", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Rowsperpageoptions", StringUtil.RTrim( Gridpaginationbar_Rowsperpageoptions));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Previous", StringUtil.RTrim( Gridpaginationbar_Previous));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Next", StringUtil.RTrim( Gridpaginationbar_Next));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Caption", StringUtil.RTrim( Gridpaginationbar_Caption));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Emptygridcaption", StringUtil.RTrim( Gridpaginationbar_Emptygridcaption));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Rowsperpagecaption", StringUtil.RTrim( Gridpaginationbar_Rowsperpagecaption));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDC_SUBSCRIPTIONS_Icontype", StringUtil.RTrim( Ddc_subscriptions_Icontype));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDC_SUBSCRIPTIONS_Icon", StringUtil.RTrim( Ddc_subscriptions_Icon));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDC_SUBSCRIPTIONS_Caption", StringUtil.RTrim( Ddc_subscriptions_Caption));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDC_SUBSCRIPTIONS_Tooltip", StringUtil.RTrim( Ddc_subscriptions_Tooltip));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDC_SUBSCRIPTIONS_Cls", StringUtil.RTrim( Ddc_subscriptions_Cls));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDC_SUBSCRIPTIONS_Titlecontrolidtoreplace", StringUtil.RTrim( Ddc_subscriptions_Titlecontrolidtoreplace));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Caption", StringUtil.RTrim( Ddo_grid_Caption));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Filteredtext_set", StringUtil.RTrim( Ddo_grid_Filteredtext_set));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Filteredtextto_set", StringUtil.RTrim( Ddo_grid_Filteredtextto_set));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Selectedvalue_set", StringUtil.RTrim( Ddo_grid_Selectedvalue_set));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Gamoauthtoken", StringUtil.RTrim( Ddo_grid_Gamoauthtoken));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Gridinternalname", StringUtil.RTrim( Ddo_grid_Gridinternalname));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Columnids", StringUtil.RTrim( Ddo_grid_Columnids));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Columnssortvalues", StringUtil.RTrim( Ddo_grid_Columnssortvalues));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Includesortasc", StringUtil.RTrim( Ddo_grid_Includesortasc));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Sortedstatus", StringUtil.RTrim( Ddo_grid_Sortedstatus));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Includefilter", StringUtil.RTrim( Ddo_grid_Includefilter));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Filtertype", StringUtil.RTrim( Ddo_grid_Filtertype));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Filterisrange", StringUtil.RTrim( Ddo_grid_Filterisrange));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Includedatalist", StringUtil.RTrim( Ddo_grid_Includedatalist));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Datalisttype", StringUtil.RTrim( Ddo_grid_Datalisttype));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Datalistproc", StringUtil.RTrim( Ddo_grid_Datalistproc));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Format", StringUtil.RTrim( Ddo_grid_Format));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Icontype", StringUtil.RTrim( Ddo_gridcolumnsselector_Icontype));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Icon", StringUtil.RTrim( Ddo_gridcolumnsselector_Icon));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Caption", StringUtil.RTrim( Ddo_gridcolumnsselector_Caption));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Tooltip", StringUtil.RTrim( Ddo_gridcolumnsselector_Tooltip));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Cls", StringUtil.RTrim( Ddo_gridcolumnsselector_Cls));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Dropdownoptionstype", StringUtil.RTrim( Ddo_gridcolumnsselector_Dropdownoptionstype));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Gridinternalname", StringUtil.RTrim( Ddo_gridcolumnsselector_Gridinternalname));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Titlecontrolidtoreplace", StringUtil.RTrim( Ddo_gridcolumnsselector_Titlecontrolidtoreplace));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE_Title", StringUtil.RTrim( Dvelop_confirmpanel_useractiondelete_Title));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE_Confirmationtext", StringUtil.RTrim( Dvelop_confirmpanel_useractiondelete_Confirmationtext));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE_Yesbuttoncaption", StringUtil.RTrim( Dvelop_confirmpanel_useractiondelete_Yesbuttoncaption));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE_Nobuttoncaption", StringUtil.RTrim( Dvelop_confirmpanel_useractiondelete_Nobuttoncaption));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE_Cancelbuttoncaption", StringUtil.RTrim( Dvelop_confirmpanel_useractiondelete_Cancelbuttoncaption));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE_Yesbuttonposition", StringUtil.RTrim( Dvelop_confirmpanel_useractiondelete_Yesbuttonposition));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE_Confirmtype", StringUtil.RTrim( Dvelop_confirmpanel_useractiondelete_Confirmtype));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_EMPOWERER_Gridinternalname", StringUtil.RTrim( Grid_empowerer_Gridinternalname));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_EMPOWERER_Hastitlesettings", StringUtil.BoolToStr( Grid_empowerer_Hastitlesettings));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_EMPOWERER_Hascolumnsselector", StringUtil.BoolToStr( Grid_empowerer_Hascolumnsselector));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Selectedpage", StringUtil.RTrim( Gridpaginationbar_Selectedpage));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Rowsperpageselectedvalue", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Activeeventkey", StringUtil.RTrim( Ddo_grid_Activeeventkey));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Selectedvalue_get", StringUtil.RTrim( Ddo_grid_Selectedvalue_get));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Selectedcolumn", StringUtil.RTrim( Ddo_grid_Selectedcolumn));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Filteredtext_get", StringUtil.RTrim( Ddo_grid_Filteredtext_get));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Filteredtextto_get", StringUtil.RTrim( Ddo_grid_Filteredtextto_get));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Columnsselectorvalues", StringUtil.RTrim( Ddo_gridcolumnsselector_Columnsselectorvalues));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_MANAGEFILTERS_Activeeventkey", StringUtil.RTrim( Ddo_managefilters_Activeeventkey));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE_Result", StringUtil.RTrim( Dvelop_confirmpanel_useractiondelete_Result));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Selectedpage", StringUtil.RTrim( Gridpaginationbar_Selectedpage));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDPAGINATIONBAR_Rowsperpageselectedvalue", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Activeeventkey", StringUtil.RTrim( Ddo_grid_Activeeventkey));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Selectedvalue_get", StringUtil.RTrim( Ddo_grid_Selectedvalue_get));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Selectedcolumn", StringUtil.RTrim( Ddo_grid_Selectedcolumn));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Filteredtext_get", StringUtil.RTrim( Ddo_grid_Filteredtext_get));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRID_Filteredtextto_get", StringUtil.RTrim( Ddo_grid_Filteredtextto_get));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Columnsselectorvalues", StringUtil.RTrim( Ddo_gridcolumnsselector_Columnsselectorvalues));
         GxWebStd.gx_hidden_field( context, sPrefix+"DDO_MANAGEFILTERS_Activeeventkey", StringUtil.RTrim( Ddo_managefilters_Activeeventkey));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE_Result", StringUtil.RTrim( Dvelop_confirmpanel_useractiondelete_Result));
      }

      protected void RenderHtmlCloseFormBB2( )
      {
         SendCloseFormHiddens( ) ;
         if ( ( StringUtil.Len( sPrefix) != 0 ) && ( context.isAjaxRequest( ) || context.isSpaRequest( ) ) )
         {
            componentjscripts();
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GX_FocusControl", GX_FocusControl);
         define_styles( ) ;
         SendSecurityToken(sPrefix);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            SendAjaxEncryptionKey();
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
            if ( ! ( WebComp_Wwpaux_wc == null ) )
            {
               WebComp_Wwpaux_wc.componentjscripts();
            }
            context.WriteHtmlTextNl( "</body>") ;
            context.WriteHtmlTextNl( "</html>") ;
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
         }
         else
         {
            SendWebComponentState();
            context.WriteHtmlText( "</div>") ;
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
         }
      }

      public override string GetPgmname( )
      {
         return "WP_DynamicFormTrn_LocationDynamicFormWC" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "WP_Dynamic Form Trn_Location Dynamic Form WC", "") ;
      }

      protected void WBBB0( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               RenderHtmlHeaders( ) ;
            }
            RenderHtmlOpenForm( ) ;
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wp_dynamicformtrn_locationdynamicformwc.aspx");
               context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
               context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
               context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
               context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
               context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
               context.AddJavascriptSource("DVelop/DVPaginationBar/DVPaginationBarRender.js", "", false, true);
               context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
               context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
               context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
               context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
               context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
               context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
               context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
               context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
               context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
               context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
               context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
               context.AddJavascriptSource("DVelop/Bootstrap/ConfirmPanel/BootstrapConfirmPanelRender.js", "", false, true);
               context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
               context.AddJavascriptSource("DVelop/GridEmpowerer/GridEmpowererRender.js", "", false, true);
               context.AddJavascriptSource("DVelop/Shared/daterangepicker/locales.js", "", false, true);
               context.AddJavascriptSource("DVelop/Shared/daterangepicker/wwp-daterangepicker.js", "", false, true);
               context.AddJavascriptSource("DVelop/Shared/daterangepicker/moment.min.js", "", false, true);
               context.AddJavascriptSource("DVelop/Shared/daterangepicker/daterangepicker.min.js", "", false, true);
               context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
               context.AddJavascriptSource("DVelop/DateRangePicker/DateRangePickerRender.js", "", false, true);
            }
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"bootstrapv3\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divLayoutmaintable_Internalname, 1, 0, "px", 0, "px", "Table TableWithSelectableGrid", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablegridheader_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingBottom", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableheader_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "flex-direction:column;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableheadercontent_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "flex-wrap:wrap;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableactions_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "align-self:center;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group ActionGroupColoredActions", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnuseractioninsert_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(47), 2, 0)+","+"null"+");", context.GetMessage( "Insert", ""), bttBtnuseractioninsert_Jsonclick, 7, context.GetMessage( "Insert", ""), "", StyleString, ClassString, bttBtnuseractioninsert_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+"e11bb1_client"+"'", TempTags, "", 2, "HLP_WP_DynamicFormTrn_LocationDynamicFormWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'" + sPrefix + "',false,'',0)\"";
            ClassString = "hidden-xs";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtneditcolumns_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(47), 2, 0)+","+"null"+");", context.GetMessage( "WWP_EditColumnsCaption", ""), bttBtneditcolumns_Jsonclick, 0, context.GetMessage( "WWP_EditColumnsTooltip", ""), "", StyleString, ClassString, 1, 0, "standard", "'"+sPrefix+"'"+",false,"+"'"+""+"'", TempTags, "", context.GetButtonType( ), "HLP_WP_DynamicFormTrn_LocationDynamicFormWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 21,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnsubscriptions_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(47), 2, 0)+","+"null"+");", "", bttBtnsubscriptions_Jsonclick, 0, context.GetMessage( "WWP_Subscriptions_Tooltip", ""), "", StyleString, ClassString, bttBtnsubscriptions_Visible, 0, "standard", "'"+sPrefix+"'"+",false,"+"'"+""+"'", TempTags, "", context.GetButtonType( ), "HLP_WP_DynamicFormTrn_LocationDynamicFormWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "align-self:center;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablerightheader_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            /* User Defined Control */
            ucDdo_managefilters.SetProperty("IconType", Ddo_managefilters_Icontype);
            ucDdo_managefilters.SetProperty("Icon", Ddo_managefilters_Icon);
            ucDdo_managefilters.SetProperty("Caption", Ddo_managefilters_Caption);
            ucDdo_managefilters.SetProperty("Tooltip", Ddo_managefilters_Tooltip);
            ucDdo_managefilters.SetProperty("Cls", Ddo_managefilters_Cls);
            ucDdo_managefilters.SetProperty("DropDownOptionsData", AV44ManageFiltersData);
            ucDdo_managefilters.Render(context, "dvelop.gxbootstrap.ddoregular", Ddo_managefilters_Internalname, sPrefix+"DDO_MANAGEFILTERSContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;align-self:center;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablefilters_Internalname, 1, 0, "px", 0, "px", "TableFilters", "start", "top", " "+"data-gx-flex"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavFilterfulltext_Internalname, context.GetMessage( "Filter Full Text", ""), "gx-form-item AttributeLabel", 0, true, "width: 25%;");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 30,'" + sPrefix + "',false,'" + sGXsfl_47_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavFilterfulltext_Internalname, AV22FilterFullText, StringUtil.RTrim( context.localUtil.Format( AV22FilterFullText, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,30);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", context.GetMessage( "WWP_Search", ""), edtavFilterfulltext_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavFilterfulltext_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, -1, -1, true, "WorkWithPlus_Web\\WWPFullTextFilter", "start", true, "", "HLP_WP_DynamicFormTrn_LocationDynamicFormWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divOrganisationidfilter_cell_Internalname, 1, 0, "px", 0, "px", divOrganisationidfilter_cell_Class, "start", "top", "", "flex-grow:1;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", dynavOrganisationidfilter.Visible, 0, "px", 0, "px", "form-group gx-form-group gx-default-form-group", "start", "top", ""+" data-gx-for=\""+dynavOrganisationidfilter_Internalname+"\"", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 75, "%", 0, "px", "gx-form-item gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 34,'" + sPrefix + "',false,'" + sGXsfl_47_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, dynavOrganisationidfilter, dynavOrganisationidfilter_Internalname, AV53OrganisationIdFilter.ToString(), 1, dynavOrganisationidfilter_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "guid", "", dynavOrganisationidfilter.Visible, dynavOrganisationidfilter.Enabled, 0, 0, 20, "em", 0, "", "", "AddressAttribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,34);\"", "", true, 0, "HLP_WP_DynamicFormTrn_LocationDynamicFormWC.htm");
            dynavOrganisationidfilter.CurrentValue = AV53OrganisationIdFilter.ToString();
            AssignProp(sPrefix, false, dynavOrganisationidfilter_Internalname, "Values", (string)(dynavOrganisationidfilter.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divLocationidfilter_cell_Internalname, 1, 0, "px", 0, "px", divLocationidfilter_cell_Class, "start", "top", "", "flex-grow:1;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", dynavLocationidfilter.Visible, 0, "px", 0, "px", "form-group gx-form-group gx-default-form-group", "start", "top", ""+" data-gx-for=\""+dynavLocationidfilter_Internalname+"\"", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 75, "%", 0, "px", "gx-form-item gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 38,'" + sPrefix + "',false,'" + sGXsfl_47_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, dynavLocationidfilter, dynavLocationidfilter_Internalname, AV43LocationIdFilter.ToString(), 1, dynavLocationidfilter_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "guid", "", dynavLocationidfilter.Visible, dynavLocationidfilter.Enabled, 0, 0, 20, "em", 0, "", "", "AddressAttribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,38);\"", "", true, 0, "HLP_WP_DynamicFormTrn_LocationDynamicFormWC.htm");
            dynavLocationidfilter.CurrentValue = AV43LocationIdFilter.ToString();
            AssignProp(sPrefix, false, dynavLocationidfilter_Internalname, "Values", (string)(dynavLocationidfilter.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
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
            ClassString = "ErrorViewer";
            StyleString = "";
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, StyleString, ClassString, sPrefix, "false");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 SectionGrid GridNoBorderCell GridFixedColumnBorders GridHover HasGridEmpowerer", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGridtablewithpaginationbar_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridContainer.SetWrapped(nGXWrapped);
            StartGridControl47( ) ;
         }
         if ( wbEnd == 47 )
         {
            wbEnd = 0;
            nRC_GXsfl_47 = (int)(nGXsfl_47_idx-1);
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+sPrefix+"GridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Grid", GridContainer, subGrid_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridContainerData", GridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridContainerData"+"V", GridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridContainerData"+"V"+"\" value='"+GridContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* User Defined Control */
            ucGridpaginationbar.SetProperty("Class", Gridpaginationbar_Class);
            ucGridpaginationbar.SetProperty("ShowFirst", Gridpaginationbar_Showfirst);
            ucGridpaginationbar.SetProperty("ShowPrevious", Gridpaginationbar_Showprevious);
            ucGridpaginationbar.SetProperty("ShowNext", Gridpaginationbar_Shownext);
            ucGridpaginationbar.SetProperty("ShowLast", Gridpaginationbar_Showlast);
            ucGridpaginationbar.SetProperty("PagesToShow", Gridpaginationbar_Pagestoshow);
            ucGridpaginationbar.SetProperty("PagingButtonsPosition", Gridpaginationbar_Pagingbuttonsposition);
            ucGridpaginationbar.SetProperty("PagingCaptionPosition", Gridpaginationbar_Pagingcaptionposition);
            ucGridpaginationbar.SetProperty("EmptyGridClass", Gridpaginationbar_Emptygridclass);
            ucGridpaginationbar.SetProperty("RowsPerPageSelector", Gridpaginationbar_Rowsperpageselector);
            ucGridpaginationbar.SetProperty("RowsPerPageOptions", Gridpaginationbar_Rowsperpageoptions);
            ucGridpaginationbar.SetProperty("Previous", Gridpaginationbar_Previous);
            ucGridpaginationbar.SetProperty("Next", Gridpaginationbar_Next);
            ucGridpaginationbar.SetProperty("Caption", Gridpaginationbar_Caption);
            ucGridpaginationbar.SetProperty("EmptyGridCaption", Gridpaginationbar_Emptygridcaption);
            ucGridpaginationbar.SetProperty("RowsPerPageCaption", Gridpaginationbar_Rowsperpagecaption);
            ucGridpaginationbar.SetProperty("CurrentPage", AV26GridCurrentPage);
            ucGridpaginationbar.SetProperty("PageCount", AV27GridPageCount);
            ucGridpaginationbar.SetProperty("AppliedFilters", AV25GridAppliedFilters);
            ucGridpaginationbar.Render(context, "dvelop.dvpaginationbar", Gridpaginationbar_Internalname, sPrefix+"GRIDPAGINATIONBARContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
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
            /* User Defined Control */
            ucDdc_subscriptions.SetProperty("IconType", Ddc_subscriptions_Icontype);
            ucDdc_subscriptions.SetProperty("Icon", Ddc_subscriptions_Icon);
            ucDdc_subscriptions.SetProperty("Caption", Ddc_subscriptions_Caption);
            ucDdc_subscriptions.SetProperty("Tooltip", Ddc_subscriptions_Tooltip);
            ucDdc_subscriptions.SetProperty("Cls", Ddc_subscriptions_Cls);
            ucDdc_subscriptions.Render(context, "dvelop.gxbootstrap.ddcomponent", Ddc_subscriptions_Internalname, sPrefix+"DDC_SUBSCRIPTIONSContainer");
            /* User Defined Control */
            ucDdo_grid.SetProperty("Caption", Ddo_grid_Caption);
            ucDdo_grid.SetProperty("ColumnIds", Ddo_grid_Columnids);
            ucDdo_grid.SetProperty("ColumnsSortValues", Ddo_grid_Columnssortvalues);
            ucDdo_grid.SetProperty("IncludeSortASC", Ddo_grid_Includesortasc);
            ucDdo_grid.SetProperty("IncludeFilter", Ddo_grid_Includefilter);
            ucDdo_grid.SetProperty("FilterType", Ddo_grid_Filtertype);
            ucDdo_grid.SetProperty("FilterIsRange", Ddo_grid_Filterisrange);
            ucDdo_grid.SetProperty("IncludeDataList", Ddo_grid_Includedatalist);
            ucDdo_grid.SetProperty("DataListType", Ddo_grid_Datalisttype);
            ucDdo_grid.SetProperty("DataListProc", Ddo_grid_Datalistproc);
            ucDdo_grid.SetProperty("Format", Ddo_grid_Format);
            ucDdo_grid.SetProperty("DropDownOptionsTitleSettingsIcons", AV15DDO_TitleSettingsIcons);
            ucDdo_grid.Render(context, "dvelop.gxbootstrap.ddogridtitlesettingsm", Ddo_grid_Internalname, sPrefix+"DDO_GRIDContainer");
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbWWPFormType, cmbWWPFormType_Internalname, StringUtil.Trim( StringUtil.Str( (decimal)(A240WWPFormType), 1, 0)), 1, cmbWWPFormType_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "int", "", cmbWWPFormType.Visible, 0, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", "", "", true, 0, "HLP_WP_DynamicFormTrn_LocationDynamicFormWC.htm");
            cmbWWPFormType.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(A240WWPFormType), 1, 0));
            AssignProp(sPrefix, false, cmbWWPFormType_Internalname, "Values", (string)(cmbWWPFormType.ToJavascriptSource()), true);
            /* User Defined Control */
            ucDdo_gridcolumnsselector.SetProperty("IconType", Ddo_gridcolumnsselector_Icontype);
            ucDdo_gridcolumnsselector.SetProperty("Icon", Ddo_gridcolumnsselector_Icon);
            ucDdo_gridcolumnsselector.SetProperty("Caption", Ddo_gridcolumnsselector_Caption);
            ucDdo_gridcolumnsselector.SetProperty("Tooltip", Ddo_gridcolumnsselector_Tooltip);
            ucDdo_gridcolumnsselector.SetProperty("Cls", Ddo_gridcolumnsselector_Cls);
            ucDdo_gridcolumnsselector.SetProperty("DropDownOptionsType", Ddo_gridcolumnsselector_Dropdownoptionstype);
            ucDdo_gridcolumnsselector.SetProperty("DropDownOptionsTitleSettingsIcons", AV15DDO_TitleSettingsIcons);
            ucDdo_gridcolumnsselector.SetProperty("DropDownOptionsData", AV11ColumnsSelector);
            ucDdo_gridcolumnsselector.Render(context, "dvelop.gxbootstrap.ddogridcolumnsselector", Ddo_gridcolumnsselector_Internalname, sPrefix+"DDO_GRIDCOLUMNSSELECTORContainer");
            wb_table1_67_BB2( true) ;
         }
         else
         {
            wb_table1_67_BB2( false) ;
         }
         return  ;
      }

      protected void wb_table1_67_BB2e( bool wbgen )
      {
         if ( wbgen )
         {
            /* User Defined Control */
            ucGrid_empowerer.SetProperty("HasTitleSettings", Grid_empowerer_Hastitlesettings);
            ucGrid_empowerer.SetProperty("HasColumnsSelector", Grid_empowerer_Hascolumnsselector);
            ucGrid_empowerer.Render(context, "wwp.gridempowerer", Grid_empowerer_Internalname, sPrefix+"GRID_EMPOWERERContainer");
            /* Div Control */
            GxWebStd.gx_div_start( context, divDiv_wwpauxwc_Internalname, 1, 0, "px", 0, "px", "Invisible", "start", "top", "", "", "div");
            if ( ! isFullAjaxMode( ) )
            {
               /* WebComponent */
               GxWebStd.gx_hidden_field( context, sPrefix+"W0074"+"", StringUtil.RTrim( WebComp_Wwpaux_wc_Component));
               context.WriteHtmlText( "<div") ;
               GxWebStd.ClassAttribute( context, "gxwebcomponent");
               context.WriteHtmlText( " id=\""+sPrefix+"gxHTMLWrpW0074"+""+"\""+"") ;
               context.WriteHtmlText( ">") ;
               if ( bGXsfl_47_Refreshing )
               {
                  if ( StringUtil.Len( WebComp_Wwpaux_wc_Component) != 0 )
                  {
                     if ( StringUtil.StrCmp(StringUtil.Lower( OldWwpaux_wc), StringUtil.Lower( WebComp_Wwpaux_wc_Component)) != 0 )
                     {
                        context.httpAjaxContext.ajax_rspStartCmp(sPrefix+"gxHTMLWrpW0074"+"");
                     }
                     WebComp_Wwpaux_wc.componentdraw();
                     if ( StringUtil.StrCmp(StringUtil.Lower( OldWwpaux_wc), StringUtil.Lower( WebComp_Wwpaux_wc_Component)) != 0 )
                     {
                        context.httpAjaxContext.ajax_rspEndCmp();
                     }
                  }
               }
               context.WriteHtmlText( "</div>") ;
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divDdo_wwpformdateauxdates_Internalname, 1, 0, "px", 0, "px", "Invisible", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 76,'" + sPrefix + "',false,'" + sGXsfl_47_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavDdo_wwpformdateauxdatetext_Internalname, AV17DDO_WWPFormDateAuxDateText, StringUtil.RTrim( context.localUtil.Format( AV17DDO_WWPFormDateAuxDateText, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,76);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavDdo_wwpformdateauxdatetext_Jsonclick, 0, "Attribute", "", "", "", "", 1, 1, 0, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_WP_DynamicFormTrn_LocationDynamicFormWC.htm");
            /* User Defined Control */
            ucTfwwpformdate_rangepicker.SetProperty("Start Date", AV16DDO_WWPFormDateAuxDate);
            ucTfwwpformdate_rangepicker.SetProperty("End Date", AV18DDO_WWPFormDateAuxDateTo);
            ucTfwwpformdate_rangepicker.Render(context, "wwp.daterangepicker", Tfwwpformdate_rangepicker_Internalname, sPrefix+"TFWWPFORMDATE_RANGEPICKERContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 47 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"GridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Grid", GridContainer, subGrid_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridContainerData", GridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridContainerData"+"V", GridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridContainerData"+"V"+"\" value='"+GridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void STARTBB2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.isSpaRequest( ) )
            {
               if ( context.ExposeMetadata( ) )
               {
                  Form.Meta.addItem("generator", "GeneXus .NET 18_0_10-184260", 0) ;
               }
            }
            Form.Meta.addItem("description", context.GetMessage( "WP_Dynamic Form Trn_Location Dynamic Form WC", ""), 0) ;
            context.wjLoc = "";
            context.nUserReturn = 0;
            context.wbHandled = 0;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               sXEvt = cgiGet( "_EventName");
               if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
               {
               }
            }
         }
         wbErr = false;
         if ( ( StringUtil.Len( sPrefix) == 0 ) || ( nDraw == 1 ) )
         {
            if ( nDoneStart == 0 )
            {
               STRUPBB0( ) ;
            }
         }
      }

      protected void WSBB2( )
      {
         STARTBB2( ) ;
         EVTBB2( ) ;
      }

      protected void EVTBB2( )
      {
         sXEvt = cgiGet( "_EventName");
         if ( ( ( ( StringUtil.Len( sPrefix) == 0 ) ) || ( StringUtil.StringSearch( sXEvt, sPrefix, 1) > 0 ) ) && ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) && ! wbErr )
            {
               /* Read Web Panel buttons. */
               if ( context.wbHandled == 0 )
               {
                  if ( StringUtil.Len( sPrefix) == 0 )
                  {
                     sEvt = cgiGet( "_EventName");
                     EvtGridId = cgiGet( "_EventGridId");
                     EvtRowId = cgiGet( "_EventRowId");
                  }
                  if ( StringUtil.Len( sEvt) > 0 )
                  {
                     sEvtType = StringUtil.Left( sEvt, 1);
                     sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-1));
                     if ( StringUtil.StrCmp(sEvtType, "E") == 0 )
                     {
                        sEvtType = StringUtil.Right( sEvt, 1);
                        if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                        {
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                           if ( StringUtil.StrCmp(sEvt, "RFR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPBB0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_MANAGEFILTERS.ONOPTIONCLICKED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPBB0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Ddo_managefilters.Onoptionclicked */
                                    E12BB2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDPAGINATIONBAR.CHANGEPAGE") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPBB0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridpaginationbar.Changepage */
                                    E13BB2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDPAGINATIONBAR.CHANGEROWSPERPAGE") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPBB0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridpaginationbar.Changerowsperpage */
                                    E14BB2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_GRID.ONOPTIONCLICKED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPBB0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Ddo_grid.Onoptionclicked */
                                    E15BB2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPBB0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Ddo_gridcolumnsselector.Oncolumnschanged */
                                    E16BB2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DVELOP_CONFIRMPANEL_USERACTIONDELETE.CLOSE") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPBB0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Dvelop_confirmpanel_useractiondelete.Close */
                                    E17BB2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VLOCATIONIDFILTER.CONTROLVALUECHANGED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPBB0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E18BB2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPBB0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = cmbavActiongroup_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 9), "GRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 18), "VACTIONGROUP.CLICK") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 18), "VACTIONGROUP.CLICK") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPBB0( ) ;
                              }
                              nGXsfl_47_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_47_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_47_idx), 4, 0), 4, "0");
                              SubsflControlProps_472( ) ;
                              A11OrganisationId = StringUtil.StrToGuid( cgiGet( edtOrganisationId_Internalname));
                              A29LocationId = StringUtil.StrToGuid( cgiGet( edtLocationId_Internalname));
                              A206WWPFormId = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtWWPFormId_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                              A209WWPFormTitle = cgiGet( edtWWPFormTitle_Internalname);
                              A208WWPFormReferenceName = cgiGet( edtWWPFormReferenceName_Internalname);
                              A231WWPFormDate = context.localUtil.CToT( cgiGet( edtWWPFormDate_Internalname), 0);
                              A207WWPFormVersionNumber = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtWWPFormVersionNumber_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                              A219WWPFormLatestVersionNumber = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtWWPFormLatestVersionNumber_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                              cmbavActiongroup.Name = cmbavActiongroup_Internalname;
                              cmbavActiongroup.CurrentValue = cgiGet( cmbavActiongroup_Internalname);
                              AV10ActionGroup = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbavActiongroup_Internalname), "."), 18, MidpointRounding.ToEven));
                              AssignAttri(sPrefix, false, cmbavActiongroup_Internalname, StringUtil.LTrimStr( (decimal)(AV10ActionGroup), 4, 0));
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = cmbavActiongroup_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Start */
                                          E19BB2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = cmbavActiongroup_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Refresh */
                                          E20BB2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRID.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = cmbavActiongroup_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Grid.Load */
                                          E21BB2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "VACTIONGROUP.CLICK") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = cmbavActiongroup_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          E22BB2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          if ( ! wbErr )
                                          {
                                             Rfr0gs = false;
                                             /* Set Refresh If Orderedby Changed */
                                             if ( ( context.localUtil.CToN( cgiGet( sPrefix+"GXH_vORDEREDBY"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) != Convert.ToDecimal( AV50OrderedBy )) )
                                             {
                                                Rfr0gs = true;
                                             }
                                             /* Set Refresh If Ordereddsc Changed */
                                             if ( StringUtil.StrToBool( cgiGet( sPrefix+"GXH_vORDEREDDSC")) != AV52OrderedDsc )
                                             {
                                                Rfr0gs = true;
                                             }
                                             /* Set Refresh If Filterfulltext Changed */
                                             if ( StringUtil.StrCmp(cgiGet( sPrefix+"GXH_vFILTERFULLTEXT"), AV22FilterFullText) != 0 )
                                             {
                                                Rfr0gs = true;
                                             }
                                             if ( ! Rfr0gs )
                                             {
                                             }
                                             dynload_actions( ) ;
                                          }
                                       }
                                    }
                                    /* No code required for Cancel button. It is implemented as the Reset button. */
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                                 {
                                    if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                                    {
                                       STRUPBB0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = cmbavActiongroup_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                       }
                                    }
                                 }
                              }
                              else
                              {
                              }
                           }
                        }
                     }
                     else if ( StringUtil.StrCmp(sEvtType, "W") == 0 )
                     {
                        sEvtType = StringUtil.Left( sEvt, 4);
                        sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-4));
                        nCmpId = (short)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                        if ( nCmpId == 74 )
                        {
                           OldWwpaux_wc = cgiGet( sPrefix+"W0074");
                           if ( ( StringUtil.Len( OldWwpaux_wc) == 0 ) || ( StringUtil.StrCmp(OldWwpaux_wc, WebComp_Wwpaux_wc_Component) != 0 ) )
                           {
                              WebComp_Wwpaux_wc = getWebComponent(GetType(), "GeneXus.Programs", OldWwpaux_wc, new Object[] {context} );
                              WebComp_Wwpaux_wc.ComponentInit();
                              WebComp_Wwpaux_wc.Name = "OldWwpaux_wc";
                              WebComp_Wwpaux_wc_Component = OldWwpaux_wc;
                           }
                           if ( StringUtil.Len( WebComp_Wwpaux_wc_Component) != 0 )
                           {
                              WebComp_Wwpaux_wc.componentprocess(sPrefix+"W0074", "", sEvt);
                           }
                           WebComp_Wwpaux_wc_Component = OldWwpaux_wc;
                        }
                     }
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WEBB2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseFormBB2( ) ;
            }
         }
      }

      protected void PABB2( )
      {
         if ( nDonePA == 0 )
         {
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               initialize_properties( ) ;
            }
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
               {
                  gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
               }
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            toggleJsOutput = isJsOutputEnabled( );
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableJsOutput();
               }
            }
            init_web_controls( ) ;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( toggleJsOutput )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableJsOutput();
                  }
               }
            }
            if ( ! context.isAjaxRequest( ) )
            {
               GX_FocusControl = edtavFilterfulltext_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void GXDLVvORGANISATIONIDFILTERBB2( )
      {
         if ( ! context.isAjaxRequest( ) )
         {
            context.GX_webresponse.AppendHeader("Cache-Control", "no-store");
         }
         AddString( "[[") ;
         GXDLVvORGANISATIONIDFILTER_dataBB2( ) ;
         gxdynajaxindex = 1;
         while ( gxdynajaxindex <= gxdynajaxctrlcodr.Count )
         {
            AddString( gxwrpcisep+"{\"c\":\""+GXUtil.EncodeJSConstant( ((string)gxdynajaxctrlcodr.Item(gxdynajaxindex)))+"\",\"d\":\""+GXUtil.EncodeJSConstant( ((string)gxdynajaxctrldescr.Item(gxdynajaxindex)))+"\"}") ;
            gxdynajaxindex = (int)(gxdynajaxindex+1);
            gxwrpcisep = ",";
         }
         AddString( "]") ;
         if ( gxdynajaxctrlcodr.Count == 0 )
         {
            AddString( ",101") ;
         }
         AddString( "]") ;
      }

      protected void GXVvORGANISATIONIDFILTER_htmlBB2( )
      {
         Guid gxdynajaxvalue;
         GXDLVvORGANISATIONIDFILTER_dataBB2( ) ;
         gxdynajaxindex = 1;
         if ( ! ( gxdyncontrolsrefreshing && context.isAjaxRequest( ) ) )
         {
            dynavOrganisationidfilter.removeAllItems();
         }
         while ( gxdynajaxindex <= gxdynajaxctrlcodr.Count )
         {
            gxdynajaxvalue = StringUtil.StrToGuid( ((string)gxdynajaxctrlcodr.Item(gxdynajaxindex)));
            dynavOrganisationidfilter.addItem(gxdynajaxvalue.ToString(), ((string)gxdynajaxctrldescr.Item(gxdynajaxindex)), 0);
            gxdynajaxindex = (int)(gxdynajaxindex+1);
         }
      }

      protected void GXDLVvORGANISATIONIDFILTER_dataBB2( )
      {
         gxdynajaxctrlcodr.Clear();
         gxdynajaxctrldescr.Clear();
         gxdynajaxctrlcodr.Add(Guid.Empty.ToString());
         gxdynajaxctrldescr.Add(context.GetMessage( "Select Organisation", ""));
         /* Using cursor H00BB2 */
         pr_default.execute(0);
         while ( (pr_default.getStatus(0) != 101) )
         {
            gxdynajaxctrlcodr.Add(H00BB2_A11OrganisationId[0].ToString());
            gxdynajaxctrldescr.Add(H00BB2_A13OrganisationName[0]);
            pr_default.readNext(0);
         }
         pr_default.close(0);
      }

      protected void GXDLVvLOCATIONIDFILTERBB2( Guid AV53OrganisationIdFilter )
      {
         if ( ! context.isAjaxRequest( ) )
         {
            context.GX_webresponse.AppendHeader("Cache-Control", "no-store");
         }
         AddString( "[[") ;
         GXDLVvLOCATIONIDFILTER_dataBB2( AV53OrganisationIdFilter) ;
         gxdynajaxindex = 1;
         while ( gxdynajaxindex <= gxdynajaxctrlcodr.Count )
         {
            AddString( gxwrpcisep+"{\"c\":\""+GXUtil.EncodeJSConstant( ((string)gxdynajaxctrlcodr.Item(gxdynajaxindex)))+"\",\"d\":\""+GXUtil.EncodeJSConstant( ((string)gxdynajaxctrldescr.Item(gxdynajaxindex)))+"\"}") ;
            gxdynajaxindex = (int)(gxdynajaxindex+1);
            gxwrpcisep = ",";
         }
         AddString( "]") ;
         if ( gxdynajaxctrlcodr.Count == 0 )
         {
            AddString( ",101") ;
         }
         AddString( "]") ;
      }

      protected void GXVvLOCATIONIDFILTER_htmlBB2( Guid AV53OrganisationIdFilter )
      {
         Guid gxdynajaxvalue;
         GXDLVvLOCATIONIDFILTER_dataBB2( AV53OrganisationIdFilter) ;
         gxdynajaxindex = 1;
         if ( ! ( gxdyncontrolsrefreshing && context.isAjaxRequest( ) ) )
         {
            dynavLocationidfilter.removeAllItems();
         }
         while ( gxdynajaxindex <= gxdynajaxctrlcodr.Count )
         {
            gxdynajaxvalue = StringUtil.StrToGuid( ((string)gxdynajaxctrlcodr.Item(gxdynajaxindex)));
            dynavLocationidfilter.addItem(gxdynajaxvalue.ToString(), ((string)gxdynajaxctrldescr.Item(gxdynajaxindex)), 0);
            gxdynajaxindex = (int)(gxdynajaxindex+1);
         }
      }

      protected void GXDLVvLOCATIONIDFILTER_dataBB2( Guid AV53OrganisationIdFilter )
      {
         gxdynajaxctrlcodr.Clear();
         gxdynajaxctrldescr.Clear();
         gxdynajaxctrlcodr.Add(Guid.Empty.ToString());
         gxdynajaxctrldescr.Add(context.GetMessage( "Select Location", ""));
         /* Using cursor H00BB3 */
         pr_default.execute(1, new Object[] {AV53OrganisationIdFilter});
         while ( (pr_default.getStatus(1) != 101) )
         {
            gxdynajaxctrlcodr.Add(H00BB3_A29LocationId[0].ToString());
            gxdynajaxctrldescr.Add(H00BB3_A31LocationName[0]);
            pr_default.readNext(1);
         }
         pr_default.close(1);
      }

      protected void gxnrGrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_472( ) ;
         while ( nGXsfl_47_idx <= nRC_GXsfl_47 )
         {
            sendrow_472( ) ;
            nGXsfl_47_idx = ((subGrid_Islastpage==1)&&(nGXsfl_47_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_47_idx+1);
            sGXsfl_47_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_47_idx), 4, 0), 4, "0");
            SubsflControlProps_472( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridContainer)) ;
         /* End function gxnrGrid_newrow */
      }

      protected void gxgrGrid_refresh( int subGrid_Rows ,
                                       short AV50OrderedBy ,
                                       bool AV52OrderedDsc ,
                                       string AV22FilterFullText ,
                                       short AV9WWPFormType ,
                                       Guid AV43LocationIdFilter ,
                                       GeneXus.Programs.wwpbaseobjects.SdtWWPContext AV73WWPContext ,
                                       short AV45ManageFiltersExecutionStep ,
                                       WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector AV11ColumnsSelector ,
                                       string AV78Pgmname ,
                                       string AV65TFWWPFormTitle ,
                                       string AV66TFWWPFormTitle_Sel ,
                                       string AV63TFWWPFormReferenceName ,
                                       string AV64TFWWPFormReferenceName_Sel ,
                                       DateTime AV59TFWWPFormDate ,
                                       DateTime AV60TFWWPFormDate_To ,
                                       short AV67TFWWPFormVersionNumber ,
                                       short AV68TFWWPFormVersionNumber_To ,
                                       short AV61TFWWPFormLatestVersionNumber ,
                                       short AV62TFWWPFormLatestVersionNumber_To ,
                                       bool AV76WWPFormIsForDynamicValidations ,
                                       bool AV39IsAuthorized_UserActionEdit ,
                                       bool AV38IsAuthorized_UserActionDisplay ,
                                       bool AV36IsAuthorized_UserActionCopy ,
                                       bool AV37IsAuthorized_UserActionDelete ,
                                       bool AV40IsAuthorized_UserActionFilledForms ,
                                       bool AV41IsAuthorized_UserActionFillOutForm ,
                                       Guid AV53OrganisationIdFilter ,
                                       Guid A366LocationDynamicFormId ,
                                       string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRID_nCurrentRecord = 0;
         RFBB2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGrid_refresh */
      }

      protected void send_integrity_hashes( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMID", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(A206WWPFormId), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"WWPFORMID", StringUtil.LTrim( StringUtil.NToC( (decimal)(A206WWPFormId), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMVERSIONNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(A207WWPFormVersionNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"WWPFORMVERSIONNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(A207WWPFormVersionNumber), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_ORGANISATIONID", GetSecureSignedToken( sPrefix, A11OrganisationId, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"ORGANISATIONID", A11OrganisationId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_LOCATIONID", GetSecureSignedToken( sPrefix, A29LocationId, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"LOCATIONID", A29LocationId.ToString());
      }

      protected void clear_multi_value_controls( )
      {
         if ( context.isAjaxRequest( ) )
         {
            GXVvORGANISATIONIDFILTER_htmlBB2( ) ;
            dynload_actions( ) ;
            before_start_formulas( ) ;
         }
      }

      protected void fix_multi_value_controls( )
      {
         if ( dynavOrganisationidfilter.ItemCount > 0 )
         {
            AV53OrganisationIdFilter = StringUtil.StrToGuid( dynavOrganisationidfilter.getValidValue(AV53OrganisationIdFilter.ToString()));
            AssignAttri(sPrefix, false, "AV53OrganisationIdFilter", AV53OrganisationIdFilter.ToString());
         }
         if ( context.isAjaxRequest( ) )
         {
            dynavOrganisationidfilter.CurrentValue = AV53OrganisationIdFilter.ToString();
            AssignProp(sPrefix, false, dynavOrganisationidfilter_Internalname, "Values", dynavOrganisationidfilter.ToJavascriptSource(), true);
         }
         if ( dynavLocationidfilter.ItemCount > 0 )
         {
            AV43LocationIdFilter = StringUtil.StrToGuid( dynavLocationidfilter.getValidValue(AV43LocationIdFilter.ToString()));
            AssignAttri(sPrefix, false, "AV43LocationIdFilter", AV43LocationIdFilter.ToString());
         }
         if ( context.isAjaxRequest( ) )
         {
            dynavLocationidfilter.CurrentValue = AV43LocationIdFilter.ToString();
            AssignProp(sPrefix, false, dynavLocationidfilter_Internalname, "Values", dynavLocationidfilter.ToJavascriptSource(), true);
         }
         if ( cmbWWPFormType.ItemCount > 0 )
         {
            A240WWPFormType = (short)(Math.Round(NumberUtil.Val( cmbWWPFormType.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(A240WWPFormType), 1, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri(sPrefix, false, "A240WWPFormType", StringUtil.Str( (decimal)(A240WWPFormType), 1, 0));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbWWPFormType.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(A240WWPFormType), 1, 0));
            AssignProp(sPrefix, false, cmbWWPFormType_Internalname, "Values", cmbWWPFormType.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RFBB2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         AV78Pgmname = "WP_DynamicFormTrn_LocationDynamicFormWC";
      }

      protected int subGridclient_rec_count_fnc( )
      {
         GRID_nRecordCount = 0;
         pr_default.dynParam(2, new Object[]{ new Object[]{
                                              AV66TFWWPFormTitle_Sel ,
                                              AV65TFWWPFormTitle ,
                                              AV64TFWWPFormReferenceName_Sel ,
                                              AV63TFWWPFormReferenceName ,
                                              AV59TFWWPFormDate ,
                                              AV60TFWWPFormDate_To ,
                                              AV67TFWWPFormVersionNumber ,
                                              AV68TFWWPFormVersionNumber_To ,
                                              A209WWPFormTitle ,
                                              A208WWPFormReferenceName ,
                                              A231WWPFormDate ,
                                              A207WWPFormVersionNumber ,
                                              AV50OrderedBy ,
                                              AV52OrderedDsc ,
                                              A240WWPFormType ,
                                              AV9WWPFormType ,
                                              AV22FilterFullText ,
                                              A219WWPFormLatestVersionNumber ,
                                              AV61TFWWPFormLatestVersionNumber ,
                                              AV62TFWWPFormLatestVersionNumber_To ,
                                              A29LocationId ,
                                              AV5LocationId } ,
                                              new int[]{
                                              TypeConstants.DATE, TypeConstants.DATE, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.DATE, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.BOOLEAN, TypeConstants.SHORT, TypeConstants.SHORT,
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT
                                              }
         });
         lV65TFWWPFormTitle = StringUtil.Concat( StringUtil.RTrim( AV65TFWWPFormTitle), "%", "");
         lV63TFWWPFormReferenceName = StringUtil.Concat( StringUtil.RTrim( AV63TFWWPFormReferenceName), "%", "");
         /* Using cursor H00BB4 */
         pr_default.execute(2, new Object[] {AV9WWPFormType, AV5LocationId, lV65TFWWPFormTitle, AV66TFWWPFormTitle_Sel, lV63TFWWPFormReferenceName, AV64TFWWPFormReferenceName_Sel, AV59TFWWPFormDate, AV60TFWWPFormDate_To, AV67TFWWPFormVersionNumber, AV68TFWWPFormVersionNumber_To});
         while ( (pr_default.getStatus(2) != 101) )
         {
            A366LocationDynamicFormId = H00BB4_A366LocationDynamicFormId[0];
            A240WWPFormType = H00BB4_A240WWPFormType[0];
            AssignAttri(sPrefix, false, "A240WWPFormType", StringUtil.Str( (decimal)(A240WWPFormType), 1, 0));
            A207WWPFormVersionNumber = H00BB4_A207WWPFormVersionNumber[0];
            A231WWPFormDate = H00BB4_A231WWPFormDate[0];
            A208WWPFormReferenceName = H00BB4_A208WWPFormReferenceName[0];
            A209WWPFormTitle = H00BB4_A209WWPFormTitle[0];
            A29LocationId = H00BB4_A29LocationId[0];
            A11OrganisationId = H00BB4_A11OrganisationId[0];
            A206WWPFormId = H00BB4_A206WWPFormId[0];
            A240WWPFormType = H00BB4_A240WWPFormType[0];
            AssignAttri(sPrefix, false, "A240WWPFormType", StringUtil.Str( (decimal)(A240WWPFormType), 1, 0));
            A231WWPFormDate = H00BB4_A231WWPFormDate[0];
            A208WWPFormReferenceName = H00BB4_A208WWPFormReferenceName[0];
            A209WWPFormTitle = H00BB4_A209WWPFormTitle[0];
            GXt_int1 = A219WWPFormLatestVersionNumber;
            new GeneXus.Programs.workwithplus.dynamicforms.wwp_df_getlatestversionofform(context ).execute(  A206WWPFormId, out  GXt_int1) ;
            A219WWPFormLatestVersionNumber = GXt_int1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV22FilterFullText)) || ( ( StringUtil.Like( StringUtil.Lower( A209WWPFormTitle) , StringUtil.PadR( "%" + StringUtil.Lower( AV22FilterFullText) , 255 , "%"),  ' ' ) ) || ( StringUtil.Like( StringUtil.Lower( A208WWPFormReferenceName) , StringUtil.PadR( "%" + StringUtil.Lower( AV22FilterFullText) , 255 , "%"),  ' ' ) ) || ( StringUtil.Like( StringUtil.Str( (decimal)(A207WWPFormVersionNumber), 4, 0) , StringUtil.PadR( "%" + AV22FilterFullText , 254 , "%"),  ' ' ) ) || ( StringUtil.Like( StringUtil.Str( (decimal)(A219WWPFormLatestVersionNumber), 4, 0) , StringUtil.PadR( "%" + AV22FilterFullText , 254 , "%"),  ' ' ) ) ) )
            {
               if ( (0==AV61TFWWPFormLatestVersionNumber) || ( ( A219WWPFormLatestVersionNumber >= AV61TFWWPFormLatestVersionNumber ) ) )
               {
                  if ( (0==AV62TFWWPFormLatestVersionNumber_To) || ( ( A219WWPFormLatestVersionNumber <= AV62TFWWPFormLatestVersionNumber_To ) ) )
                  {
                     if ( A207WWPFormVersionNumber == A219WWPFormLatestVersionNumber )
                     {
                        GRID_nRecordCount = (long)(GRID_nRecordCount+1);
                     }
                  }
               }
            }
            pr_default.readNext(2);
         }
         GRID_nEOF = (short)(((pr_default.getStatus(2) == 101) ? 1 : 0));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
         pr_default.close(2);
         return (int)(GRID_nRecordCount) ;
      }

      protected void RFBB2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridContainer.ClearRows();
         }
         wbStart = 47;
         /* Execute user event: Refresh */
         E20BB2 ();
         nGXsfl_47_idx = 1;
         sGXsfl_47_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_47_idx), 4, 0), 4, "0");
         SubsflControlProps_472( ) ;
         bGXsfl_47_Refreshing = true;
         GridContainer.AddObjectProperty("GridName", "Grid");
         GridContainer.AddObjectProperty("CmpContext", sPrefix);
         GridContainer.AddObjectProperty("InMasterPage", "false");
         GridContainer.AddObjectProperty("Class", "GridWithPaginationBar WorkWithSelection WorkWith");
         GridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Backcolorstyle), 1, 0, ".", "")));
         GridContainer.AddObjectProperty("Sortable", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Sortable), 1, 0, ".", "")));
         GridContainer.PageSize = subGrid_fnc_Recordsperpage( );
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            if ( 1 != 0 )
            {
               if ( StringUtil.Len( WebComp_Wwpaux_wc_Component) != 0 )
               {
                  WebComp_Wwpaux_wc.componentstart();
               }
            }
         }
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_472( ) ;
            pr_default.dynParam(3, new Object[]{ new Object[]{
                                                 AV66TFWWPFormTitle_Sel ,
                                                 AV65TFWWPFormTitle ,
                                                 AV64TFWWPFormReferenceName_Sel ,
                                                 AV63TFWWPFormReferenceName ,
                                                 AV59TFWWPFormDate ,
                                                 AV60TFWWPFormDate_To ,
                                                 AV67TFWWPFormVersionNumber ,
                                                 AV68TFWWPFormVersionNumber_To ,
                                                 A209WWPFormTitle ,
                                                 A208WWPFormReferenceName ,
                                                 A231WWPFormDate ,
                                                 A207WWPFormVersionNumber ,
                                                 AV50OrderedBy ,
                                                 AV52OrderedDsc ,
                                                 A240WWPFormType ,
                                                 AV9WWPFormType ,
                                                 AV22FilterFullText ,
                                                 A219WWPFormLatestVersionNumber ,
                                                 AV61TFWWPFormLatestVersionNumber ,
                                                 AV62TFWWPFormLatestVersionNumber_To ,
                                                 A29LocationId ,
                                                 AV5LocationId } ,
                                                 new int[]{
                                                 TypeConstants.DATE, TypeConstants.DATE, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.DATE, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.BOOLEAN, TypeConstants.SHORT, TypeConstants.SHORT,
                                                 TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT
                                                 }
            });
            lV65TFWWPFormTitle = StringUtil.Concat( StringUtil.RTrim( AV65TFWWPFormTitle), "%", "");
            lV63TFWWPFormReferenceName = StringUtil.Concat( StringUtil.RTrim( AV63TFWWPFormReferenceName), "%", "");
            /* Using cursor H00BB5 */
            pr_default.execute(3, new Object[] {AV9WWPFormType, AV5LocationId, lV65TFWWPFormTitle, AV66TFWWPFormTitle_Sel, lV63TFWWPFormReferenceName, AV64TFWWPFormReferenceName_Sel, AV59TFWWPFormDate, AV60TFWWPFormDate_To, AV67TFWWPFormVersionNumber, AV68TFWWPFormVersionNumber_To});
            nGXsfl_47_idx = 1;
            sGXsfl_47_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_47_idx), 4, 0), 4, "0");
            SubsflControlProps_472( ) ;
            GRID_nEOF = 0;
            GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
            while ( ( (pr_default.getStatus(3) != 101) ) && ( ( ( subGrid_Rows == 0 ) || ( GRID_nCurrentRecord < GRID_nFirstRecordOnPage + subGrid_fnc_Recordsperpage( ) ) ) ) )
            {
               A366LocationDynamicFormId = H00BB5_A366LocationDynamicFormId[0];
               A240WWPFormType = H00BB5_A240WWPFormType[0];
               AssignAttri(sPrefix, false, "A240WWPFormType", StringUtil.Str( (decimal)(A240WWPFormType), 1, 0));
               A207WWPFormVersionNumber = H00BB5_A207WWPFormVersionNumber[0];
               A231WWPFormDate = H00BB5_A231WWPFormDate[0];
               A208WWPFormReferenceName = H00BB5_A208WWPFormReferenceName[0];
               A209WWPFormTitle = H00BB5_A209WWPFormTitle[0];
               A29LocationId = H00BB5_A29LocationId[0];
               A11OrganisationId = H00BB5_A11OrganisationId[0];
               A206WWPFormId = H00BB5_A206WWPFormId[0];
               A240WWPFormType = H00BB5_A240WWPFormType[0];
               AssignAttri(sPrefix, false, "A240WWPFormType", StringUtil.Str( (decimal)(A240WWPFormType), 1, 0));
               A231WWPFormDate = H00BB5_A231WWPFormDate[0];
               A208WWPFormReferenceName = H00BB5_A208WWPFormReferenceName[0];
               A209WWPFormTitle = H00BB5_A209WWPFormTitle[0];
               GXt_int1 = A219WWPFormLatestVersionNumber;
               new GeneXus.Programs.workwithplus.dynamicforms.wwp_df_getlatestversionofform(context ).execute(  A206WWPFormId, out  GXt_int1) ;
               A219WWPFormLatestVersionNumber = GXt_int1;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV22FilterFullText)) || ( ( StringUtil.Like( StringUtil.Lower( A209WWPFormTitle) , StringUtil.PadR( "%" + StringUtil.Lower( AV22FilterFullText) , 255 , "%"),  ' ' ) ) || ( StringUtil.Like( StringUtil.Lower( A208WWPFormReferenceName) , StringUtil.PadR( "%" + StringUtil.Lower( AV22FilterFullText) , 255 , "%"),  ' ' ) ) || ( StringUtil.Like( StringUtil.Str( (decimal)(A207WWPFormVersionNumber), 4, 0) , StringUtil.PadR( "%" + AV22FilterFullText , 254 , "%"),  ' ' ) ) || ( StringUtil.Like( StringUtil.Str( (decimal)(A219WWPFormLatestVersionNumber), 4, 0) , StringUtil.PadR( "%" + AV22FilterFullText , 254 , "%"),  ' ' ) ) ) )
               {
                  if ( (0==AV61TFWWPFormLatestVersionNumber) || ( ( A219WWPFormLatestVersionNumber >= AV61TFWWPFormLatestVersionNumber ) ) )
                  {
                     if ( (0==AV62TFWWPFormLatestVersionNumber_To) || ( ( A219WWPFormLatestVersionNumber <= AV62TFWWPFormLatestVersionNumber_To ) ) )
                     {
                        if ( A207WWPFormVersionNumber == A219WWPFormLatestVersionNumber )
                        {
                           /* Execute user event: Grid.Load */
                           E21BB2 ();
                        }
                     }
                  }
               }
               pr_default.readNext(3);
            }
            GRID_nEOF = (short)(((pr_default.getStatus(3) == 101) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
            pr_default.close(3);
            wbEnd = 47;
            WBBB0( ) ;
         }
         bGXsfl_47_Refreshing = true;
      }

      protected void send_integrity_lvl_hashesBB2( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWWPCONTEXT", AV73WWPContext);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWWPCONTEXT", AV73WWPContext);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWWPCONTEXT", GetSecureSignedToken( sPrefix, AV73WWPContext, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vPGMNAME", StringUtil.RTrim( AV78Pgmname));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPGMNAME", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( AV78Pgmname, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONEDIT", AV39IsAuthorized_UserActionEdit);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONEDIT", GetSecureSignedToken( sPrefix, AV39IsAuthorized_UserActionEdit, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONDISPLAY", AV38IsAuthorized_UserActionDisplay);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONDISPLAY", GetSecureSignedToken( sPrefix, AV38IsAuthorized_UserActionDisplay, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONCOPY", AV36IsAuthorized_UserActionCopy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONCOPY", GetSecureSignedToken( sPrefix, AV36IsAuthorized_UserActionCopy, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONDELETE", AV37IsAuthorized_UserActionDelete);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONDELETE", GetSecureSignedToken( sPrefix, AV37IsAuthorized_UserActionDelete, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONFILLEDFORMS", AV40IsAuthorized_UserActionFilledForms);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONFILLEDFORMS", GetSecureSignedToken( sPrefix, AV40IsAuthorized_UserActionFilledForms, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_USERACTIONFILLOUTFORM", AV41IsAuthorized_UserActionFillOutForm);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONFILLOUTFORM", GetSecureSignedToken( sPrefix, AV41IsAuthorized_UserActionFillOutForm, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMID"+"_"+sGXsfl_47_idx, GetSecureSignedToken( sPrefix+sGXsfl_47_idx, context.localUtil.Format( (decimal)(A206WWPFormId), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMVERSIONNUMBER"+"_"+sGXsfl_47_idx, GetSecureSignedToken( sPrefix+sGXsfl_47_idx, context.localUtil.Format( (decimal)(A207WWPFormVersionNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"LOCATIONDYNAMICFORMID", A366LocationDynamicFormId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_LOCATIONDYNAMICFORMID", GetSecureSignedToken( sPrefix, A366LocationDynamicFormId, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_ORGANISATIONID"+"_"+sGXsfl_47_idx, GetSecureSignedToken( sPrefix+sGXsfl_47_idx, A11OrganisationId, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_LOCATIONID"+"_"+sGXsfl_47_idx, GetSecureSignedToken( sPrefix+sGXsfl_47_idx, A29LocationId, context));
      }

      protected int subGrid_fnc_Pagecount( )
      {
         GRID_nRecordCount = subGrid_fnc_Recordcount( );
         if ( ((int)((GRID_nRecordCount) % (subGrid_fnc_Recordsperpage( )))) == 0 )
         {
            return (int)(NumberUtil.Int( (long)(Math.Round(GRID_nRecordCount/ (decimal)(subGrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))) ;
         }
         return (int)(NumberUtil.Int( (long)(Math.Round(GRID_nRecordCount/ (decimal)(subGrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected int subGrid_fnc_Recordcount( )
      {
         return (int)(subGridclient_rec_count_fnc()) ;
      }

      protected int subGrid_fnc_Recordsperpage( )
      {
         if ( subGrid_Rows > 0 )
         {
            return subGrid_Rows*1 ;
         }
         else
         {
            return (int)(-1) ;
         }
      }

      protected int subGrid_fnc_Currentpage( )
      {
         return (int)(NumberUtil.Int( (long)(Math.Round(GRID_nFirstRecordOnPage/ (decimal)(subGrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected short subgrid_firstpage( )
      {
         GRID_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV50OrderedBy, AV52OrderedDsc, AV22FilterFullText, AV9WWPFormType, AV43LocationIdFilter, AV73WWPContext, AV45ManageFiltersExecutionStep, AV11ColumnsSelector, AV78Pgmname, AV65TFWWPFormTitle, AV66TFWWPFormTitle_Sel, AV63TFWWPFormReferenceName, AV64TFWWPFormReferenceName_Sel, AV59TFWWPFormDate, AV60TFWWPFormDate_To, AV67TFWWPFormVersionNumber, AV68TFWWPFormVersionNumber_To, AV61TFWWPFormLatestVersionNumber, AV62TFWWPFormLatestVersionNumber_To, AV76WWPFormIsForDynamicValidations, AV39IsAuthorized_UserActionEdit, AV38IsAuthorized_UserActionDisplay, AV36IsAuthorized_UserActionCopy, AV37IsAuthorized_UserActionDelete, AV40IsAuthorized_UserActionFilledForms, AV41IsAuthorized_UserActionFillOutForm, AV53OrganisationIdFilter, A366LocationDynamicFormId, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgrid_nextpage( )
      {
         if ( GRID_nEOF == 0 )
         {
            GRID_nFirstRecordOnPage = (long)(GRID_nFirstRecordOnPage+subGrid_fnc_Recordsperpage( ));
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         GridContainer.AddObjectProperty("GRID_nFirstRecordOnPage", GRID_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV50OrderedBy, AV52OrderedDsc, AV22FilterFullText, AV9WWPFormType, AV43LocationIdFilter, AV73WWPContext, AV45ManageFiltersExecutionStep, AV11ColumnsSelector, AV78Pgmname, AV65TFWWPFormTitle, AV66TFWWPFormTitle_Sel, AV63TFWWPFormReferenceName, AV64TFWWPFormReferenceName_Sel, AV59TFWWPFormDate, AV60TFWWPFormDate_To, AV67TFWWPFormVersionNumber, AV68TFWWPFormVersionNumber_To, AV61TFWWPFormLatestVersionNumber, AV62TFWWPFormLatestVersionNumber_To, AV76WWPFormIsForDynamicValidations, AV39IsAuthorized_UserActionEdit, AV38IsAuthorized_UserActionDisplay, AV36IsAuthorized_UserActionCopy, AV37IsAuthorized_UserActionDelete, AV40IsAuthorized_UserActionFilledForms, AV41IsAuthorized_UserActionFillOutForm, AV53OrganisationIdFilter, A366LocationDynamicFormId, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((GRID_nEOF==0) ? 0 : 2)) ;
      }

      protected short subgrid_previouspage( )
      {
         if ( GRID_nFirstRecordOnPage >= subGrid_fnc_Recordsperpage( ) )
         {
            GRID_nFirstRecordOnPage = (long)(GRID_nFirstRecordOnPage-subGrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV50OrderedBy, AV52OrderedDsc, AV22FilterFullText, AV9WWPFormType, AV43LocationIdFilter, AV73WWPContext, AV45ManageFiltersExecutionStep, AV11ColumnsSelector, AV78Pgmname, AV65TFWWPFormTitle, AV66TFWWPFormTitle_Sel, AV63TFWWPFormReferenceName, AV64TFWWPFormReferenceName_Sel, AV59TFWWPFormDate, AV60TFWWPFormDate_To, AV67TFWWPFormVersionNumber, AV68TFWWPFormVersionNumber_To, AV61TFWWPFormLatestVersionNumber, AV62TFWWPFormLatestVersionNumber_To, AV76WWPFormIsForDynamicValidations, AV39IsAuthorized_UserActionEdit, AV38IsAuthorized_UserActionDisplay, AV36IsAuthorized_UserActionCopy, AV37IsAuthorized_UserActionDelete, AV40IsAuthorized_UserActionFilledForms, AV41IsAuthorized_UserActionFillOutForm, AV53OrganisationIdFilter, A366LocationDynamicFormId, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgrid_lastpage( )
      {
         GRID_nRecordCount = subGrid_fnc_Recordcount( );
         if ( GRID_nRecordCount > subGrid_fnc_Recordsperpage( ) )
         {
            if ( ((int)((GRID_nRecordCount) % (subGrid_fnc_Recordsperpage( )))) == 0 )
            {
               GRID_nFirstRecordOnPage = (long)(GRID_nRecordCount-subGrid_fnc_Recordsperpage( ));
            }
            else
            {
               GRID_nFirstRecordOnPage = (long)(GRID_nRecordCount-((int)((GRID_nRecordCount) % (subGrid_fnc_Recordsperpage( )))));
            }
         }
         else
         {
            GRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV50OrderedBy, AV52OrderedDsc, AV22FilterFullText, AV9WWPFormType, AV43LocationIdFilter, AV73WWPContext, AV45ManageFiltersExecutionStep, AV11ColumnsSelector, AV78Pgmname, AV65TFWWPFormTitle, AV66TFWWPFormTitle_Sel, AV63TFWWPFormReferenceName, AV64TFWWPFormReferenceName_Sel, AV59TFWWPFormDate, AV60TFWWPFormDate_To, AV67TFWWPFormVersionNumber, AV68TFWWPFormVersionNumber_To, AV61TFWWPFormLatestVersionNumber, AV62TFWWPFormLatestVersionNumber_To, AV76WWPFormIsForDynamicValidations, AV39IsAuthorized_UserActionEdit, AV38IsAuthorized_UserActionDisplay, AV36IsAuthorized_UserActionCopy, AV37IsAuthorized_UserActionDelete, AV40IsAuthorized_UserActionFilledForms, AV41IsAuthorized_UserActionFillOutForm, AV53OrganisationIdFilter, A366LocationDynamicFormId, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subgrid_gotopage( int nPageNo )
      {
         if ( nPageNo > 0 )
         {
            GRID_nFirstRecordOnPage = (long)(subGrid_fnc_Recordsperpage( )*(nPageNo-1));
         }
         else
         {
            GRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV50OrderedBy, AV52OrderedDsc, AV22FilterFullText, AV9WWPFormType, AV43LocationIdFilter, AV73WWPContext, AV45ManageFiltersExecutionStep, AV11ColumnsSelector, AV78Pgmname, AV65TFWWPFormTitle, AV66TFWWPFormTitle_Sel, AV63TFWWPFormReferenceName, AV64TFWWPFormReferenceName_Sel, AV59TFWWPFormDate, AV60TFWWPFormDate_To, AV67TFWWPFormVersionNumber, AV68TFWWPFormVersionNumber_To, AV61TFWWPFormLatestVersionNumber, AV62TFWWPFormLatestVersionNumber_To, AV76WWPFormIsForDynamicValidations, AV39IsAuthorized_UserActionEdit, AV38IsAuthorized_UserActionDisplay, AV36IsAuthorized_UserActionCopy, AV37IsAuthorized_UserActionDelete, AV40IsAuthorized_UserActionFilledForms, AV41IsAuthorized_UserActionFillOutForm, AV53OrganisationIdFilter, A366LocationDynamicFormId, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         AV78Pgmname = "WP_DynamicFormTrn_LocationDynamicFormWC";
         GXVvORGANISATIONIDFILTER_htmlBB2( ) ;
         edtOrganisationId_Enabled = 0;
         edtLocationId_Enabled = 0;
         edtWWPFormId_Enabled = 0;
         edtWWPFormTitle_Enabled = 0;
         edtWWPFormReferenceName_Enabled = 0;
         edtWWPFormDate_Enabled = 0;
         edtWWPFormVersionNumber_Enabled = 0;
         edtWWPFormLatestVersionNumber_Enabled = 0;
         cmbWWPFormType.Enabled = 0;
         AssignProp(sPrefix, false, cmbWWPFormType_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbWWPFormType.Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUPBB0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E19BB2 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         GXVvLOCATIONIDFILTER_htmlBB2( AV53OrganisationIdFilter) ;
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vMANAGEFILTERSDATA"), AV44ManageFiltersData);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vDDO_TITLESETTINGSICONS"), AV15DDO_TitleSettingsIcons);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vCOLUMNSSELECTOR"), AV11ColumnsSelector);
            /* Read saved values. */
            nRC_GXsfl_47 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_47"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV26GridCurrentPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"vGRIDCURRENTPAGE"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV27GridPageCount = (long)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"vGRIDPAGECOUNT"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV25GridAppliedFilters = cgiGet( sPrefix+"vGRIDAPPLIEDFILTERS");
            AV16DDO_WWPFormDateAuxDate = context.localUtil.CToD( cgiGet( sPrefix+"vDDO_WWPFORMDATEAUXDATE"), 0);
            AV18DDO_WWPFormDateAuxDateTo = context.localUtil.CToD( cgiGet( sPrefix+"vDDO_WWPFORMDATEAUXDATETO"), 0);
            wcpOAV9WWPFormType = (short)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"wcpOAV9WWPFormType"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            wcpOAV76WWPFormIsForDynamicValidations = StringUtil.StrToBool( cgiGet( sPrefix+"wcpOAV76WWPFormIsForDynamicValidations"));
            AV6OrganisationId = StringUtil.StrToGuid( cgiGet( sPrefix+"vORGANISATIONID"));
            AV9WWPFormType = (short)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"vWWPFORMTYPE"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GRID_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRID_nFirstRecordOnPage"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GRID_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRID_nEOF"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            subGrid_Rows = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRID_Rows"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
            Ddo_managefilters_Icontype = cgiGet( sPrefix+"DDO_MANAGEFILTERS_Icontype");
            Ddo_managefilters_Icon = cgiGet( sPrefix+"DDO_MANAGEFILTERS_Icon");
            Ddo_managefilters_Tooltip = cgiGet( sPrefix+"DDO_MANAGEFILTERS_Tooltip");
            Ddo_managefilters_Cls = cgiGet( sPrefix+"DDO_MANAGEFILTERS_Cls");
            Gridpaginationbar_Class = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Class");
            Gridpaginationbar_Showfirst = StringUtil.StrToBool( cgiGet( sPrefix+"GRIDPAGINATIONBAR_Showfirst"));
            Gridpaginationbar_Showprevious = StringUtil.StrToBool( cgiGet( sPrefix+"GRIDPAGINATIONBAR_Showprevious"));
            Gridpaginationbar_Shownext = StringUtil.StrToBool( cgiGet( sPrefix+"GRIDPAGINATIONBAR_Shownext"));
            Gridpaginationbar_Showlast = StringUtil.StrToBool( cgiGet( sPrefix+"GRIDPAGINATIONBAR_Showlast"));
            Gridpaginationbar_Pagestoshow = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRIDPAGINATIONBAR_Pagestoshow"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Gridpaginationbar_Pagingbuttonsposition = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Pagingbuttonsposition");
            Gridpaginationbar_Pagingcaptionposition = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Pagingcaptionposition");
            Gridpaginationbar_Emptygridclass = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Emptygridclass");
            Gridpaginationbar_Rowsperpageselector = StringUtil.StrToBool( cgiGet( sPrefix+"GRIDPAGINATIONBAR_Rowsperpageselector"));
            Gridpaginationbar_Rowsperpageselectedvalue = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRIDPAGINATIONBAR_Rowsperpageselectedvalue"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Gridpaginationbar_Rowsperpageoptions = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Rowsperpageoptions");
            Gridpaginationbar_Previous = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Previous");
            Gridpaginationbar_Next = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Next");
            Gridpaginationbar_Caption = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Caption");
            Gridpaginationbar_Emptygridcaption = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Emptygridcaption");
            Gridpaginationbar_Rowsperpagecaption = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Rowsperpagecaption");
            Ddc_subscriptions_Icontype = cgiGet( sPrefix+"DDC_SUBSCRIPTIONS_Icontype");
            Ddc_subscriptions_Icon = cgiGet( sPrefix+"DDC_SUBSCRIPTIONS_Icon");
            Ddc_subscriptions_Caption = cgiGet( sPrefix+"DDC_SUBSCRIPTIONS_Caption");
            Ddc_subscriptions_Tooltip = cgiGet( sPrefix+"DDC_SUBSCRIPTIONS_Tooltip");
            Ddc_subscriptions_Cls = cgiGet( sPrefix+"DDC_SUBSCRIPTIONS_Cls");
            Ddc_subscriptions_Titlecontrolidtoreplace = cgiGet( sPrefix+"DDC_SUBSCRIPTIONS_Titlecontrolidtoreplace");
            Ddo_grid_Caption = cgiGet( sPrefix+"DDO_GRID_Caption");
            Ddo_grid_Filteredtext_set = cgiGet( sPrefix+"DDO_GRID_Filteredtext_set");
            Ddo_grid_Filteredtextto_set = cgiGet( sPrefix+"DDO_GRID_Filteredtextto_set");
            Ddo_grid_Selectedvalue_set = cgiGet( sPrefix+"DDO_GRID_Selectedvalue_set");
            Ddo_grid_Gamoauthtoken = cgiGet( sPrefix+"DDO_GRID_Gamoauthtoken");
            Ddo_grid_Gridinternalname = cgiGet( sPrefix+"DDO_GRID_Gridinternalname");
            Ddo_grid_Columnids = cgiGet( sPrefix+"DDO_GRID_Columnids");
            Ddo_grid_Columnssortvalues = cgiGet( sPrefix+"DDO_GRID_Columnssortvalues");
            Ddo_grid_Includesortasc = cgiGet( sPrefix+"DDO_GRID_Includesortasc");
            Ddo_grid_Sortedstatus = cgiGet( sPrefix+"DDO_GRID_Sortedstatus");
            Ddo_grid_Includefilter = cgiGet( sPrefix+"DDO_GRID_Includefilter");
            Ddo_grid_Filtertype = cgiGet( sPrefix+"DDO_GRID_Filtertype");
            Ddo_grid_Filterisrange = cgiGet( sPrefix+"DDO_GRID_Filterisrange");
            Ddo_grid_Includedatalist = cgiGet( sPrefix+"DDO_GRID_Includedatalist");
            Ddo_grid_Datalisttype = cgiGet( sPrefix+"DDO_GRID_Datalisttype");
            Ddo_grid_Datalistproc = cgiGet( sPrefix+"DDO_GRID_Datalistproc");
            Ddo_grid_Format = cgiGet( sPrefix+"DDO_GRID_Format");
            Ddo_gridcolumnsselector_Icontype = cgiGet( sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Icontype");
            Ddo_gridcolumnsselector_Icon = cgiGet( sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Icon");
            Ddo_gridcolumnsselector_Caption = cgiGet( sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Caption");
            Ddo_gridcolumnsselector_Tooltip = cgiGet( sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Tooltip");
            Ddo_gridcolumnsselector_Cls = cgiGet( sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Cls");
            Ddo_gridcolumnsselector_Dropdownoptionstype = cgiGet( sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Dropdownoptionstype");
            Ddo_gridcolumnsselector_Gridinternalname = cgiGet( sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Gridinternalname");
            Ddo_gridcolumnsselector_Titlecontrolidtoreplace = cgiGet( sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Titlecontrolidtoreplace");
            Dvelop_confirmpanel_useractiondelete_Title = cgiGet( sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE_Title");
            Dvelop_confirmpanel_useractiondelete_Confirmationtext = cgiGet( sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE_Confirmationtext");
            Dvelop_confirmpanel_useractiondelete_Yesbuttoncaption = cgiGet( sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE_Yesbuttoncaption");
            Dvelop_confirmpanel_useractiondelete_Nobuttoncaption = cgiGet( sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE_Nobuttoncaption");
            Dvelop_confirmpanel_useractiondelete_Cancelbuttoncaption = cgiGet( sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE_Cancelbuttoncaption");
            Dvelop_confirmpanel_useractiondelete_Yesbuttonposition = cgiGet( sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE_Yesbuttonposition");
            Dvelop_confirmpanel_useractiondelete_Confirmtype = cgiGet( sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE_Confirmtype");
            Grid_empowerer_Gridinternalname = cgiGet( sPrefix+"GRID_EMPOWERER_Gridinternalname");
            Grid_empowerer_Hastitlesettings = StringUtil.StrToBool( cgiGet( sPrefix+"GRID_EMPOWERER_Hastitlesettings"));
            Grid_empowerer_Hascolumnsselector = StringUtil.StrToBool( cgiGet( sPrefix+"GRID_EMPOWERER_Hascolumnsselector"));
            subGrid_Rows = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRID_Rows"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
            Gridpaginationbar_Selectedpage = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Selectedpage");
            Gridpaginationbar_Rowsperpageselectedvalue = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRIDPAGINATIONBAR_Rowsperpageselectedvalue"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Ddo_grid_Activeeventkey = cgiGet( sPrefix+"DDO_GRID_Activeeventkey");
            Ddo_grid_Selectedvalue_get = cgiGet( sPrefix+"DDO_GRID_Selectedvalue_get");
            Ddo_grid_Selectedcolumn = cgiGet( sPrefix+"DDO_GRID_Selectedcolumn");
            Ddo_grid_Filteredtext_get = cgiGet( sPrefix+"DDO_GRID_Filteredtext_get");
            Ddo_grid_Filteredtextto_get = cgiGet( sPrefix+"DDO_GRID_Filteredtextto_get");
            Ddo_gridcolumnsselector_Columnsselectorvalues = cgiGet( sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Columnsselectorvalues");
            Ddo_managefilters_Activeeventkey = cgiGet( sPrefix+"DDO_MANAGEFILTERS_Activeeventkey");
            subGrid_Rows = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRID_Rows"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
            Dvelop_confirmpanel_useractiondelete_Result = cgiGet( sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE_Result");
            /* Read variables values. */
            AV22FilterFullText = cgiGet( edtavFilterfulltext_Internalname);
            AssignAttri(sPrefix, false, "AV22FilterFullText", AV22FilterFullText);
            dynavOrganisationidfilter.Name = dynavOrganisationidfilter_Internalname;
            dynavOrganisationidfilter.CurrentValue = cgiGet( dynavOrganisationidfilter_Internalname);
            AV53OrganisationIdFilter = StringUtil.StrToGuid( cgiGet( dynavOrganisationidfilter_Internalname));
            AssignAttri(sPrefix, false, "AV53OrganisationIdFilter", AV53OrganisationIdFilter.ToString());
            dynavLocationidfilter.Name = dynavLocationidfilter_Internalname;
            dynavLocationidfilter.CurrentValue = cgiGet( dynavLocationidfilter_Internalname);
            AV43LocationIdFilter = StringUtil.StrToGuid( cgiGet( dynavLocationidfilter_Internalname));
            AssignAttri(sPrefix, false, "AV43LocationIdFilter", AV43LocationIdFilter.ToString());
            cmbWWPFormType.Name = cmbWWPFormType_Internalname;
            cmbWWPFormType.CurrentValue = cgiGet( cmbWWPFormType_Internalname);
            A240WWPFormType = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbWWPFormType_Internalname), "."), 18, MidpointRounding.ToEven));
            AssignAttri(sPrefix, false, "A240WWPFormType", StringUtil.Str( (decimal)(A240WWPFormType), 1, 0));
            AV17DDO_WWPFormDateAuxDateText = cgiGet( edtavDdo_wwpformdateauxdatetext_Internalname);
            AssignAttri(sPrefix, false, "AV17DDO_WWPFormDateAuxDateText", AV17DDO_WWPFormDateAuxDateText);
            /* Read subfile selected row values. */
            nGXsfl_47_idx = (int)(Math.Round(context.localUtil.CToN( cgiGet( subGrid_Internalname+"_ROW"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            sGXsfl_47_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_47_idx), 4, 0), 4, "0");
            SubsflControlProps_472( ) ;
            if ( nGXsfl_47_idx > 0 )
            {
               A11OrganisationId = StringUtil.StrToGuid( cgiGet( edtOrganisationId_Internalname));
               A29LocationId = StringUtil.StrToGuid( cgiGet( edtLocationId_Internalname));
               A206WWPFormId = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtWWPFormId_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               A209WWPFormTitle = cgiGet( edtWWPFormTitle_Internalname);
               A208WWPFormReferenceName = cgiGet( edtWWPFormReferenceName_Internalname);
               A231WWPFormDate = context.localUtil.CToT( cgiGet( edtWWPFormDate_Internalname));
               A207WWPFormVersionNumber = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtWWPFormVersionNumber_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               A219WWPFormLatestVersionNumber = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtWWPFormLatestVersionNumber_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               cmbavActiongroup.Name = cmbavActiongroup_Internalname;
               cmbavActiongroup.CurrentValue = cgiGet( cmbavActiongroup_Internalname);
               AV10ActionGroup = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbavActiongroup_Internalname), "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, cmbavActiongroup_Internalname, StringUtil.LTrimStr( (decimal)(AV10ActionGroup), 4, 0));
            }
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            /* Check if conditions changed and reset current page numbers */
            if ( ( context.localUtil.CToN( cgiGet( sPrefix+"GXH_vORDEREDBY"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) != Convert.ToDecimal( AV50OrderedBy )) )
            {
               GRID_nFirstRecordOnPage = 0;
            }
            if ( StringUtil.StrToBool( cgiGet( sPrefix+"GXH_vORDEREDDSC")) != AV52OrderedDsc )
            {
               GRID_nFirstRecordOnPage = 0;
            }
            if ( StringUtil.StrCmp(cgiGet( sPrefix+"GXH_vFILTERFULLTEXT"), AV22FilterFullText) != 0 )
            {
               GRID_nFirstRecordOnPage = 0;
            }
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void GXStart( )
      {
         /* Execute user event: Start */
         E19BB2 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
      }

      protected void E19BB2( )
      {
         /* Start Routine */
         returnInSub = false;
         new GeneXus.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV73WWPContext) ;
         if ( ! (Guid.Empty==AV73WWPContext.gxTpr_Organisationid) )
         {
            AV53OrganisationIdFilter = AV73WWPContext.gxTpr_Organisationid;
            AssignAttri(sPrefix, false, "AV53OrganisationIdFilter", AV53OrganisationIdFilter.ToString());
            AV6OrganisationId = AV73WWPContext.gxTpr_Organisationid;
            AssignAttri(sPrefix, false, "AV6OrganisationId", AV6OrganisationId.ToString());
         }
         if ( ! (Guid.Empty==AV73WWPContext.gxTpr_Locationid) )
         {
            AV43LocationIdFilter = AV73WWPContext.gxTpr_Locationid;
            AssignAttri(sPrefix, false, "AV43LocationIdFilter", AV43LocationIdFilter.ToString());
            AV5LocationId = AV73WWPContext.gxTpr_Locationid;
            AssignAttri(sPrefix, false, "AV5LocationId", AV5LocationId.ToString());
         }
         this.executeUsercontrolMethod(sPrefix, false, "TFWWPFORMDATE_RANGEPICKERContainer", "Attach", "", new Object[] {(string)edtavDdo_wwpformdateauxdatetext_Internalname});
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S112 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         subGrid_Rows = 10;
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         Grid_empowerer_Gridinternalname = subGrid_Internalname;
         ucGrid_empowerer.SendProperty(context, sPrefix, false, Grid_empowerer_Internalname, "GridInternalName", Grid_empowerer_Gridinternalname);
         Ddo_gridcolumnsselector_Gridinternalname = subGrid_Internalname;
         ucDdo_gridcolumnsselector.SendProperty(context, sPrefix, false, Ddo_gridcolumnsselector_Internalname, "GridInternalName", Ddo_gridcolumnsselector_Gridinternalname);
         /* Execute user subroutine: 'LOADSAVEDFILTERS' */
         S122 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         Ddc_subscriptions_Titlecontrolidtoreplace = bttBtnsubscriptions_Internalname;
         ucDdc_subscriptions.SendProperty(context, sPrefix, false, Ddc_subscriptions_Internalname, "TitleControlIdToReplace", Ddc_subscriptions_Titlecontrolidtoreplace);
         AV24GAMSession = new GeneXus.Programs.genexussecurity.SdtGAMSession(context).get(out  AV23GAMErrors);
         Ddo_grid_Gridinternalname = subGrid_Internalname;
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "GridInternalName", Ddo_grid_Gridinternalname);
         Ddo_grid_Gamoauthtoken = AV24GAMSession.gxTpr_Token;
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "GAMOAuthToken", Ddo_grid_Gamoauthtoken);
         cmbWWPFormType.Visible = 0;
         AssignProp(sPrefix, false, cmbWWPFormType_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbWWPFormType.Visible), 5, 0), true);
         /* Execute user subroutine: 'PREPARETRANSACTION' */
         S132 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         /* Execute user subroutine: 'LOADGRIDSTATE' */
         S142 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         if ( AV50OrderedBy < 1 )
         {
            AV50OrderedBy = 1;
            AssignAttri(sPrefix, false, "AV50OrderedBy", StringUtil.LTrimStr( (decimal)(AV50OrderedBy), 4, 0));
            /* Execute user subroutine: 'SETDDOSORTEDSTATUS' */
            S152 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons2 = AV15DDO_TitleSettingsIcons;
         new GeneXus.Programs.wwpbaseobjects.getwwptitlesettingsicons(context ).execute( out  GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons2) ;
         AV15DDO_TitleSettingsIcons = GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons2;
         Ddo_gridcolumnsselector_Titlecontrolidtoreplace = bttBtneditcolumns_Internalname;
         ucDdo_gridcolumnsselector.SendProperty(context, sPrefix, false, Ddo_gridcolumnsselector_Internalname, "TitleControlIdToReplace", Ddo_gridcolumnsselector_Titlecontrolidtoreplace);
         Gridpaginationbar_Rowsperpageselectedvalue = subGrid_Rows;
         ucGridpaginationbar.SendProperty(context, sPrefix, false, Gridpaginationbar_Internalname, "RowsPerPageSelectedValue", StringUtil.LTrimStr( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0));
      }

      protected void E20BB2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         if ( (Guid.Empty==AV43LocationIdFilter) )
         {
            AV5LocationId = AV73WWPContext.gxTpr_Locationid;
            AssignAttri(sPrefix, false, "AV5LocationId", AV5LocationId.ToString());
         }
         else
         {
            AV5LocationId = AV43LocationIdFilter;
            AssignAttri(sPrefix, false, "AV5LocationId", AV5LocationId.ToString());
         }
         new GeneXus.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV73WWPContext) ;
         /* Execute user subroutine: 'CHECKSECURITYFORACTIONS' */
         S162 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         if ( AV45ManageFiltersExecutionStep == 1 )
         {
            AV45ManageFiltersExecutionStep = 2;
            AssignAttri(sPrefix, false, "AV45ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV45ManageFiltersExecutionStep), 1, 0));
         }
         else if ( AV45ManageFiltersExecutionStep == 2 )
         {
            AV45ManageFiltersExecutionStep = 0;
            AssignAttri(sPrefix, false, "AV45ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV45ManageFiltersExecutionStep), 1, 0));
            /* Execute user subroutine: 'LOADSAVEDFILTERS' */
            S122 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         /* Execute user subroutine: 'SAVEGRIDSTATE' */
         S172 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         if ( StringUtil.StrCmp(AV58Session.Get("WP_DynamicFormTrn_LocationDynamicFormWCColumnsSelector"), "") != 0 )
         {
            AV13ColumnsSelectorXML = AV58Session.Get("WP_DynamicFormTrn_LocationDynamicFormWCColumnsSelector");
            AV11ColumnsSelector.FromXml(AV13ColumnsSelectorXML, null, "", "");
         }
         else
         {
            /* Execute user subroutine: 'INITIALIZECOLUMNSSELECTOR' */
            S182 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         edtWWPFormTitle_Visible = (((WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector_Column)AV11ColumnsSelector.gxTpr_Columns.Item(1)).gxTpr_Isvisible ? 1 : 0);
         AssignProp(sPrefix, false, edtWWPFormTitle_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtWWPFormTitle_Visible), 5, 0), !bGXsfl_47_Refreshing);
         edtWWPFormReferenceName_Visible = (((WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector_Column)AV11ColumnsSelector.gxTpr_Columns.Item(2)).gxTpr_Isvisible ? 1 : 0);
         AssignProp(sPrefix, false, edtWWPFormReferenceName_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtWWPFormReferenceName_Visible), 5, 0), !bGXsfl_47_Refreshing);
         edtWWPFormDate_Visible = (((WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector_Column)AV11ColumnsSelector.gxTpr_Columns.Item(3)).gxTpr_Isvisible ? 1 : 0);
         AssignProp(sPrefix, false, edtWWPFormDate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtWWPFormDate_Visible), 5, 0), !bGXsfl_47_Refreshing);
         edtWWPFormVersionNumber_Visible = (((WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector_Column)AV11ColumnsSelector.gxTpr_Columns.Item(4)).gxTpr_Isvisible ? 1 : 0);
         AssignProp(sPrefix, false, edtWWPFormVersionNumber_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtWWPFormVersionNumber_Visible), 5, 0), !bGXsfl_47_Refreshing);
         edtWWPFormLatestVersionNumber_Visible = (((WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector_Column)AV11ColumnsSelector.gxTpr_Columns.Item(5)).gxTpr_Isvisible ? 1 : 0);
         AssignProp(sPrefix, false, edtWWPFormLatestVersionNumber_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtWWPFormLatestVersionNumber_Visible), 5, 0), !bGXsfl_47_Refreshing);
         AV26GridCurrentPage = subGrid_fnc_Currentpage( );
         AssignAttri(sPrefix, false, "AV26GridCurrentPage", StringUtil.LTrimStr( (decimal)(AV26GridCurrentPage), 10, 0));
         AV27GridPageCount = subGrid_fnc_Pagecount( );
         AssignAttri(sPrefix, false, "AV27GridPageCount", StringUtil.LTrimStr( (decimal)(AV27GridPageCount), 10, 0));
         GXt_char3 = AV25GridAppliedFilters;
         new WorkWithPlus.workwithplus_web.wwp_getappliedfiltersdescription(context ).execute(  AV78Pgmname, out  GXt_char3) ;
         AV25GridAppliedFilters = GXt_char3;
         AssignAttri(sPrefix, false, "AV25GridAppliedFilters", AV25GridAppliedFilters);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV73WWPContext", AV73WWPContext);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV11ColumnsSelector", AV11ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV44ManageFiltersData", AV44ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV28GridState", AV28GridState);
      }

      protected void E13BB2( )
      {
         /* Gridpaginationbar_Changepage Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(Gridpaginationbar_Selectedpage, "Previous") == 0 )
         {
            subgrid_previouspage( ) ;
         }
         else if ( StringUtil.StrCmp(Gridpaginationbar_Selectedpage, "Next") == 0 )
         {
            subgrid_nextpage( ) ;
         }
         else
         {
            AV55PageToGo = (int)(Math.Round(NumberUtil.Val( Gridpaginationbar_Selectedpage, "."), 18, MidpointRounding.ToEven));
            subgrid_gotopage( AV55PageToGo) ;
         }
      }

      protected void E14BB2( )
      {
         /* Gridpaginationbar_Changerowsperpage Routine */
         returnInSub = false;
         subGrid_Rows = Gridpaginationbar_Rowsperpageselectedvalue;
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         subgrid_firstpage( ) ;
         /*  Sending Event outputs  */
      }

      protected void E15BB2( )
      {
         /* Ddo_grid_Onoptionclicked Routine */
         returnInSub = false;
         if ( ( StringUtil.StrCmp(Ddo_grid_Activeeventkey, "<#OrderASC#>") == 0 ) || ( StringUtil.StrCmp(Ddo_grid_Activeeventkey, "<#OrderDSC#>") == 0 ) )
         {
            AV50OrderedBy = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Selectedvalue_get, "."), 18, MidpointRounding.ToEven));
            AssignAttri(sPrefix, false, "AV50OrderedBy", StringUtil.LTrimStr( (decimal)(AV50OrderedBy), 4, 0));
            AV52OrderedDsc = ((StringUtil.StrCmp(Ddo_grid_Activeeventkey, "<#OrderDSC#>")==0) ? true : false);
            AssignAttri(sPrefix, false, "AV52OrderedDsc", AV52OrderedDsc);
            /* Execute user subroutine: 'SETDDOSORTEDSTATUS' */
            S152 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
            subgrid_firstpage( ) ;
         }
         else if ( StringUtil.StrCmp(Ddo_grid_Activeeventkey, "<#Filter#>") == 0 )
         {
            if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "WWPFormTitle") == 0 )
            {
               AV65TFWWPFormTitle = Ddo_grid_Filteredtext_get;
               AssignAttri(sPrefix, false, "AV65TFWWPFormTitle", AV65TFWWPFormTitle);
               AV66TFWWPFormTitle_Sel = Ddo_grid_Selectedvalue_get;
               AssignAttri(sPrefix, false, "AV66TFWWPFormTitle_Sel", AV66TFWWPFormTitle_Sel);
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "WWPFormReferenceName") == 0 )
            {
               AV63TFWWPFormReferenceName = Ddo_grid_Filteredtext_get;
               AssignAttri(sPrefix, false, "AV63TFWWPFormReferenceName", AV63TFWWPFormReferenceName);
               AV64TFWWPFormReferenceName_Sel = Ddo_grid_Selectedvalue_get;
               AssignAttri(sPrefix, false, "AV64TFWWPFormReferenceName_Sel", AV64TFWWPFormReferenceName_Sel);
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "WWPFormDate") == 0 )
            {
               AV59TFWWPFormDate = context.localUtil.CToT( Ddo_grid_Filteredtext_get, DateTimeUtil.MapDateFormat( context.GetLanguageProperty( "date_fmt")));
               AssignAttri(sPrefix, false, "AV59TFWWPFormDate", context.localUtil.TToC( AV59TFWWPFormDate, 8, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
               AV60TFWWPFormDate_To = context.localUtil.CToT( Ddo_grid_Filteredtextto_get, DateTimeUtil.MapDateFormat( context.GetLanguageProperty( "date_fmt")));
               AssignAttri(sPrefix, false, "AV60TFWWPFormDate_To", context.localUtil.TToC( AV60TFWWPFormDate_To, 8, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
               if ( ! (DateTime.MinValue==AV60TFWWPFormDate_To) )
               {
                  AV60TFWWPFormDate_To = context.localUtil.YMDHMSToT( (short)(DateTimeUtil.Year( AV60TFWWPFormDate_To)), (short)(DateTimeUtil.Month( AV60TFWWPFormDate_To)), (short)(DateTimeUtil.Day( AV60TFWWPFormDate_To)), 23, 59, 59);
                  AssignAttri(sPrefix, false, "AV60TFWWPFormDate_To", context.localUtil.TToC( AV60TFWWPFormDate_To, 8, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
               }
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "WWPFormVersionNumber") == 0 )
            {
               AV67TFWWPFormVersionNumber = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtext_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV67TFWWPFormVersionNumber", StringUtil.LTrimStr( (decimal)(AV67TFWWPFormVersionNumber), 4, 0));
               AV68TFWWPFormVersionNumber_To = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtextto_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV68TFWWPFormVersionNumber_To", StringUtil.LTrimStr( (decimal)(AV68TFWWPFormVersionNumber_To), 4, 0));
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "WWPFormLatestVersionNumber") == 0 )
            {
               AV61TFWWPFormLatestVersionNumber = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtext_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV61TFWWPFormLatestVersionNumber", StringUtil.LTrimStr( (decimal)(AV61TFWWPFormLatestVersionNumber), 4, 0));
               AV62TFWWPFormLatestVersionNumber_To = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtextto_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV62TFWWPFormLatestVersionNumber_To", StringUtil.LTrimStr( (decimal)(AV62TFWWPFormLatestVersionNumber_To), 4, 0));
            }
            subgrid_firstpage( ) ;
         }
         /*  Sending Event outputs  */
      }

      private void E21BB2( )
      {
         if ( ( subGrid_Islastpage == 1 ) || ( subGrid_Rows == 0 ) || ( ( GRID_nCurrentRecord >= GRID_nFirstRecordOnPage ) && ( GRID_nCurrentRecord < GRID_nFirstRecordOnPage + subGrid_fnc_Recordsperpage( ) ) ) )
         {
            /* Grid_Load Routine */
            returnInSub = false;
            cmbavActiongroup.removeAllItems();
            cmbavActiongroup.addItem("0", ";fas fa-bars", 0);
            if ( AV39IsAuthorized_UserActionEdit )
            {
               cmbavActiongroup.addItem("1", StringUtil.Format( "%1;%2", context.GetMessage( "Edit", ""), "fas fa-pen", "", "", "", "", "", "", ""), 0);
            }
            if ( AV38IsAuthorized_UserActionDisplay )
            {
               cmbavActiongroup.addItem("2", StringUtil.Format( "%1;%2", context.GetMessage( "Display", ""), "fas fa-magnifying-glass", "", "", "", "", "", "", ""), 0);
            }
            if ( AV36IsAuthorized_UserActionCopy )
            {
               cmbavActiongroup.addItem("3", StringUtil.Format( "%1;%2", context.GetMessage( "Copy", ""), "fa-clone far", "", "", "", "", "", "", ""), 0);
            }
            if ( AV37IsAuthorized_UserActionDelete )
            {
               cmbavActiongroup.addItem("4", StringUtil.Format( "%1;%2", context.GetMessage( "Delete", ""), "fas fa-xmark", "", "", "", "", "", "", ""), 0);
            }
            if ( AV40IsAuthorized_UserActionFilledForms )
            {
               cmbavActiongroup.addItem("5", StringUtil.Format( "%1;%2", context.GetMessage( "Filled forms", ""), "far fa-eye", "", "", "", "", "", "", ""), 0);
            }
            if ( AV41IsAuthorized_UserActionFillOutForm )
            {
               cmbavActiongroup.addItem("6", StringUtil.Format( "%1;%2", context.GetMessage( "fill out form", ""), "fas fa-file-circle-plus", "", "", "", "", "", "", ""), 0);
            }
            if ( cmbavActiongroup.ItemCount == 1 )
            {
               cmbavActiongroup_Class = "Invisible";
            }
            else
            {
               cmbavActiongroup_Class = "ConvertToDDO";
            }
            edtWWPFormTitle_Link = formatLink("workwithplus.dynamicforms.wwp_createdynamicform.aspx", new object[] {UrlEncode(StringUtil.LTrimStr(A206WWPFormId,4,0)),UrlEncode(StringUtil.RTrim("DSP")),UrlEncode(StringUtil.RTrim("")),UrlEncode(StringUtil.LTrimStr(A240WWPFormType,1,0))}, new string[] {"WWPFormId","WWPDynamicFormMode","DefaultFormType","WWPFormType"}) ;
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 47;
            }
            sendrow_472( ) ;
         }
         GRID_nEOF = (short)(((GRID_nCurrentRecord<GRID_nFirstRecordOnPage+subGrid_fnc_Recordsperpage( )) ? 1 : 0));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
         GRID_nCurrentRecord = (long)(GRID_nCurrentRecord+1);
         if ( isFullAjaxMode( ) && ! bGXsfl_47_Refreshing )
         {
            DoAjaxLoad(47, GridRow);
         }
         /*  Sending Event outputs  */
         cmbavActiongroup.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV10ActionGroup), 4, 0));
      }

      protected void E16BB2( )
      {
         /* Ddo_gridcolumnsselector_Oncolumnschanged Routine */
         returnInSub = false;
         AV13ColumnsSelectorXML = Ddo_gridcolumnsselector_Columnsselectorvalues;
         AV11ColumnsSelector.FromJSonString(AV13ColumnsSelectorXML, null);
         new WorkWithPlus.workwithplus_web.savecolumnsselectorstate(context ).execute(  "WP_DynamicFormTrn_LocationDynamicFormWCColumnsSelector",  (String.IsNullOrEmpty(StringUtil.RTrim( AV13ColumnsSelectorXML)) ? "" : AV11ColumnsSelector.ToXml(false, true, "", ""))) ;
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV11ColumnsSelector", AV11ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV73WWPContext", AV73WWPContext);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV44ManageFiltersData", AV44ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV28GridState", AV28GridState);
      }

      protected void E12BB2( )
      {
         /* Ddo_managefilters_Onoptionclicked Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(Ddo_managefilters_Activeeventkey, "<#Clean#>") == 0 )
         {
            /* Execute user subroutine: 'CLEANFILTERS' */
            S192 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
            subgrid_firstpage( ) ;
         }
         else if ( StringUtil.StrCmp(Ddo_managefilters_Activeeventkey, "<#Save#>") == 0 )
         {
            /* Execute user subroutine: 'SAVEGRIDSTATE' */
            S172 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
            context.PopUp(formatLink("wwpbaseobjects.savefilteras.aspx", new object[] {UrlEncode(StringUtil.RTrim("WP_DynamicFormTrn_LocationDynamicFormWCFilters")),UrlEncode(StringUtil.RTrim(AV78Pgmname+"GridState"))}, new string[] {"UserKey","GridStateKey"}) , new Object[] {});
            AV45ManageFiltersExecutionStep = 2;
            AssignAttri(sPrefix, false, "AV45ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV45ManageFiltersExecutionStep), 1, 0));
            context.DoAjaxRefreshCmp(sPrefix);
         }
         else if ( StringUtil.StrCmp(Ddo_managefilters_Activeeventkey, "<#Manage#>") == 0 )
         {
            context.PopUp(formatLink("wwpbaseobjects.managefilters.aspx", new object[] {UrlEncode(StringUtil.RTrim("WP_DynamicFormTrn_LocationDynamicFormWCFilters"))}, new string[] {"UserKey"}) , new Object[] {});
            AV45ManageFiltersExecutionStep = 2;
            AssignAttri(sPrefix, false, "AV45ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV45ManageFiltersExecutionStep), 1, 0));
            context.DoAjaxRefreshCmp(sPrefix);
         }
         else
         {
            GXt_char3 = AV46ManageFiltersXml;
            new WorkWithPlus.workwithplus_web.getfilterbyname(context ).execute(  "WP_DynamicFormTrn_LocationDynamicFormWCFilters",  Ddo_managefilters_Activeeventkey, out  GXt_char3) ;
            AV46ManageFiltersXml = GXt_char3;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV46ManageFiltersXml)) )
            {
               GX_msglist.addItem(context.GetMessage( "WWP_FilterNotExist", ""));
            }
            else
            {
               /* Execute user subroutine: 'CLEANFILTERS' */
               S192 ();
               if ( returnInSub )
               {
                  returnInSub = true;
                  if (true) return;
               }
               new WorkWithPlus.workwithplus_web.savegridstate(context ).execute(  AV78Pgmname+"GridState",  AV46ManageFiltersXml) ;
               AV28GridState.FromXml(AV46ManageFiltersXml, null, "", "");
               AV50OrderedBy = AV28GridState.gxTpr_Orderedby;
               AssignAttri(sPrefix, false, "AV50OrderedBy", StringUtil.LTrimStr( (decimal)(AV50OrderedBy), 4, 0));
               AV52OrderedDsc = AV28GridState.gxTpr_Ordereddsc;
               AssignAttri(sPrefix, false, "AV52OrderedDsc", AV52OrderedDsc);
               /* Execute user subroutine: 'SETDDOSORTEDSTATUS' */
               S152 ();
               if ( returnInSub )
               {
                  returnInSub = true;
                  if (true) return;
               }
               /* Execute user subroutine: 'LOADREGFILTERSSTATE' */
               S202 ();
               if ( returnInSub )
               {
                  returnInSub = true;
                  if (true) return;
               }
               subgrid_firstpage( ) ;
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV28GridState", AV28GridState);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV73WWPContext", AV73WWPContext);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV11ColumnsSelector", AV11ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV44ManageFiltersData", AV44ManageFiltersData);
      }

      protected void E22BB2( )
      {
         /* Actiongroup_Click Routine */
         returnInSub = false;
         if ( AV10ActionGroup == 1 )
         {
            /* Execute user subroutine: 'DO USERACTIONEDIT' */
            S212 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV10ActionGroup == 2 )
         {
            /* Execute user subroutine: 'DO USERACTIONDISPLAY' */
            S222 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV10ActionGroup == 3 )
         {
            /* Execute user subroutine: 'DO USERACTIONCOPY' */
            S232 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV10ActionGroup == 4 )
         {
            /* Execute user subroutine: 'DO USERACTIONDELETE' */
            S242 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV10ActionGroup == 5 )
         {
            /* Execute user subroutine: 'DO USERACTIONFILLEDFORMS' */
            S252 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV10ActionGroup == 6 )
         {
            /* Execute user subroutine: 'DO USERACTIONFILLOUTFORM' */
            S262 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         AV10ActionGroup = 0;
         AssignAttri(sPrefix, false, cmbavActiongroup_Internalname, StringUtil.LTrimStr( (decimal)(AV10ActionGroup), 4, 0));
         /*  Sending Event outputs  */
         cmbavActiongroup.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV10ActionGroup), 4, 0));
         AssignProp(sPrefix, false, cmbavActiongroup_Internalname, "Values", cmbavActiongroup.ToJavascriptSource(), true);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV73WWPContext", AV73WWPContext);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV11ColumnsSelector", AV11ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV44ManageFiltersData", AV44ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV28GridState", AV28GridState);
      }

      protected void E17BB2( )
      {
         /* Dvelop_confirmpanel_useractiondelete_Close Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(Dvelop_confirmpanel_useractiondelete_Result, "Yes") == 0 )
         {
            /* Execute user subroutine: 'DO ACTION USERACTIONDELETE' */
            S272 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV73WWPContext", AV73WWPContext);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV11ColumnsSelector", AV11ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV44ManageFiltersData", AV44ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV28GridState", AV28GridState);
      }

      protected void S152( )
      {
         /* 'SETDDOSORTEDSTATUS' Routine */
         returnInSub = false;
         Ddo_grid_Sortedstatus = StringUtil.Trim( StringUtil.Str( (decimal)(AV50OrderedBy), 4, 0))+":"+(AV52OrderedDsc ? "DSC" : "ASC");
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "SortedStatus", Ddo_grid_Sortedstatus);
      }

      protected void S182( )
      {
         /* 'INITIALIZECOLUMNSSELECTOR' Routine */
         returnInSub = false;
         AV11ColumnsSelector = new WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector(context);
         new WorkWithPlus.workwithplus_web.wwp_columnsselector_add(context ).execute( ref  AV11ColumnsSelector,  "WWPFormTitle",  "",  "Title",  true,  "") ;
         new WorkWithPlus.workwithplus_web.wwp_columnsselector_add(context ).execute( ref  AV11ColumnsSelector,  "WWPFormReferenceName",  "",  "Reference Name",  true,  "") ;
         new WorkWithPlus.workwithplus_web.wwp_columnsselector_add(context ).execute( ref  AV11ColumnsSelector,  "WWPFormDate",  "",  "Date",  true,  "") ;
         new WorkWithPlus.workwithplus_web.wwp_columnsselector_add(context ).execute( ref  AV11ColumnsSelector,  "WWPFormVersionNumber",  "",  "Form Version #",  true,  "") ;
         new WorkWithPlus.workwithplus_web.wwp_columnsselector_add(context ).execute( ref  AV11ColumnsSelector,  "WWPFormLatestVersionNumber",  "",  "Latest Version",  true,  "") ;
         GXt_char3 = AV72UserCustomValue;
         new WorkWithPlus.workwithplus_web.loadcolumnsselectorstate(context ).execute(  "WP_DynamicFormTrn_LocationDynamicFormWCColumnsSelector", out  GXt_char3) ;
         AV72UserCustomValue = GXt_char3;
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( AV72UserCustomValue)) ) )
         {
            AV12ColumnsSelectorAux.FromXml(AV72UserCustomValue, null, "", "");
            new WorkWithPlus.workwithplus_web.wwp_columnselector_updatecolumns(context ).execute( ref  AV12ColumnsSelectorAux, ref  AV11ColumnsSelector) ;
         }
      }

      protected void S162( )
      {
         /* 'CHECKSECURITYFORACTIONS' Routine */
         returnInSub = false;
         GXt_boolean4 = AV39IsAuthorized_UserActionEdit;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "wp_locationdynamicform_edit", out  GXt_boolean4) ;
         AV39IsAuthorized_UserActionEdit = GXt_boolean4;
         AssignAttri(sPrefix, false, "AV39IsAuthorized_UserActionEdit", AV39IsAuthorized_UserActionEdit);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONEDIT", GetSecureSignedToken( sPrefix, AV39IsAuthorized_UserActionEdit, context));
         GXt_boolean4 = AV38IsAuthorized_UserActionDisplay;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "wwp_createdynamicform_Execute", out  GXt_boolean4) ;
         AV38IsAuthorized_UserActionDisplay = GXt_boolean4;
         AssignAttri(sPrefix, false, "AV38IsAuthorized_UserActionDisplay", AV38IsAuthorized_UserActionDisplay);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONDISPLAY", GetSecureSignedToken( sPrefix, AV38IsAuthorized_UserActionDisplay, context));
         GXt_boolean4 = AV36IsAuthorized_UserActionCopy;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "wp_locationdynamicform_copy", out  GXt_boolean4) ;
         AV36IsAuthorized_UserActionCopy = GXt_boolean4;
         AssignAttri(sPrefix, false, "AV36IsAuthorized_UserActionCopy", AV36IsAuthorized_UserActionCopy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONCOPY", GetSecureSignedToken( sPrefix, AV36IsAuthorized_UserActionCopy, context));
         GXt_boolean4 = AV37IsAuthorized_UserActionDelete;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "wp_locationdynamicform_delete", out  GXt_boolean4) ;
         AV37IsAuthorized_UserActionDelete = GXt_boolean4;
         AssignAttri(sPrefix, false, "AV37IsAuthorized_UserActionDelete", AV37IsAuthorized_UserActionDelete);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONDELETE", GetSecureSignedToken( sPrefix, AV37IsAuthorized_UserActionDelete, context));
         GXt_boolean4 = AV40IsAuthorized_UserActionFilledForms;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "wp_locationdynamicform_filledforms", out  GXt_boolean4) ;
         AV40IsAuthorized_UserActionFilledForms = GXt_boolean4;
         AssignAttri(sPrefix, false, "AV40IsAuthorized_UserActionFilledForms", AV40IsAuthorized_UserActionFilledForms);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONFILLEDFORMS", GetSecureSignedToken( sPrefix, AV40IsAuthorized_UserActionFilledForms, context));
         GXt_boolean4 = AV41IsAuthorized_UserActionFillOutForm;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "wp_locationdynamicform_filloutform", out  GXt_boolean4) ;
         AV41IsAuthorized_UserActionFillOutForm = GXt_boolean4;
         AssignAttri(sPrefix, false, "AV41IsAuthorized_UserActionFillOutForm", AV41IsAuthorized_UserActionFillOutForm);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_USERACTIONFILLOUTFORM", GetSecureSignedToken( sPrefix, AV41IsAuthorized_UserActionFillOutForm, context));
         GXt_boolean4 = AV42IsAuthorized_UserActionInsert;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "wp_locationdynamicform_insert", out  GXt_boolean4) ;
         AV42IsAuthorized_UserActionInsert = GXt_boolean4;
         if ( ! ( AV42IsAuthorized_UserActionInsert ) )
         {
            bttBtnuseractioninsert_Visible = 0;
            AssignProp(sPrefix, false, bttBtnuseractioninsert_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnuseractioninsert_Visible), 5, 0), true);
         }
         if ( ! ( new GeneXus.Programs.wwpbaseobjects.subscriptions.wwp_hassubscriptionstodisplay(context).executeUdp(  "WWP_Form",  1) ) )
         {
            bttBtnsubscriptions_Visible = 0;
            AssignProp(sPrefix, false, bttBtnsubscriptions_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnsubscriptions_Visible), 5, 0), true);
         }
      }

      protected void S122( )
      {
         /* 'LOADSAVEDFILTERS' Routine */
         returnInSub = false;
         GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item5 = AV44ManageFiltersData;
         new WorkWithPlus.workwithplus_web.wwp_managefiltersloadsavedfilters(context ).execute(  "WP_DynamicFormTrn_LocationDynamicFormWCFilters",  "",  "",  false, out  GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item5) ;
         AV44ManageFiltersData = GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item5;
      }

      protected void S192( )
      {
         /* 'CLEANFILTERS' Routine */
         returnInSub = false;
         AV22FilterFullText = "";
         AssignAttri(sPrefix, false, "AV22FilterFullText", AV22FilterFullText);
         AV65TFWWPFormTitle = "";
         AssignAttri(sPrefix, false, "AV65TFWWPFormTitle", AV65TFWWPFormTitle);
         AV66TFWWPFormTitle_Sel = "";
         AssignAttri(sPrefix, false, "AV66TFWWPFormTitle_Sel", AV66TFWWPFormTitle_Sel);
         AV63TFWWPFormReferenceName = "";
         AssignAttri(sPrefix, false, "AV63TFWWPFormReferenceName", AV63TFWWPFormReferenceName);
         AV64TFWWPFormReferenceName_Sel = "";
         AssignAttri(sPrefix, false, "AV64TFWWPFormReferenceName_Sel", AV64TFWWPFormReferenceName_Sel);
         AV59TFWWPFormDate = (DateTime)(DateTime.MinValue);
         AssignAttri(sPrefix, false, "AV59TFWWPFormDate", context.localUtil.TToC( AV59TFWWPFormDate, 8, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
         AV60TFWWPFormDate_To = (DateTime)(DateTime.MinValue);
         AssignAttri(sPrefix, false, "AV60TFWWPFormDate_To", context.localUtil.TToC( AV60TFWWPFormDate_To, 8, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
         AV67TFWWPFormVersionNumber = 0;
         AssignAttri(sPrefix, false, "AV67TFWWPFormVersionNumber", StringUtil.LTrimStr( (decimal)(AV67TFWWPFormVersionNumber), 4, 0));
         AV68TFWWPFormVersionNumber_To = 0;
         AssignAttri(sPrefix, false, "AV68TFWWPFormVersionNumber_To", StringUtil.LTrimStr( (decimal)(AV68TFWWPFormVersionNumber_To), 4, 0));
         AV61TFWWPFormLatestVersionNumber = 0;
         AssignAttri(sPrefix, false, "AV61TFWWPFormLatestVersionNumber", StringUtil.LTrimStr( (decimal)(AV61TFWWPFormLatestVersionNumber), 4, 0));
         AV62TFWWPFormLatestVersionNumber_To = 0;
         AssignAttri(sPrefix, false, "AV62TFWWPFormLatestVersionNumber_To", StringUtil.LTrimStr( (decimal)(AV62TFWWPFormLatestVersionNumber_To), 4, 0));
         Ddo_grid_Selectedvalue_set = "";
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "SelectedValue_set", Ddo_grid_Selectedvalue_set);
         Ddo_grid_Filteredtext_set = "";
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "FilteredText_set", Ddo_grid_Filteredtext_set);
         Ddo_grid_Filteredtextto_set = "";
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "FilteredTextTo_set", Ddo_grid_Filteredtextto_set);
      }

      protected void S212( )
      {
         /* 'DO USERACTIONEDIT' Routine */
         returnInSub = false;
         if ( AV39IsAuthorized_UserActionEdit )
         {
            CallWebObject(formatLink("workwithplus.dynamicforms.wwp_createdynamicform.aspx", new object[] {UrlEncode(StringUtil.LTrimStr(A206WWPFormId,4,0)),UrlEncode(StringUtil.RTrim("UPD")),UrlEncode(StringUtil.RTrim("")),UrlEncode(StringUtil.LTrimStr(A240WWPFormType,1,0))}, new string[] {"WWPFormId","WWPDynamicFormMode","DefaultFormType","WWPFormType"}) );
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem(context.GetMessage( "WWP_ActionNoLongerAvailable", ""));
            context.DoAjaxRefreshCmp(sPrefix);
         }
      }

      protected void S222( )
      {
         /* 'DO USERACTIONDISPLAY' Routine */
         returnInSub = false;
         if ( AV38IsAuthorized_UserActionDisplay )
         {
            CallWebObject(formatLink("workwithplus.dynamicforms.wwp_createdynamicform.aspx", new object[] {UrlEncode(StringUtil.LTrimStr(A206WWPFormId,4,0)),UrlEncode(StringUtil.RTrim("DSP")),UrlEncode(StringUtil.RTrim("")),UrlEncode(StringUtil.LTrimStr(A240WWPFormType,1,0))}, new string[] {"WWPFormId","WWPDynamicFormMode","DefaultFormType","WWPFormType"}) );
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem(context.GetMessage( "WWP_ActionNoLongerAvailable", ""));
            context.DoAjaxRefreshCmp(sPrefix);
         }
      }

      protected void S232( )
      {
         /* 'DO USERACTIONCOPY' Routine */
         returnInSub = false;
         AV74WWPForm.Load(A206WWPFormId, A207WWPFormVersionNumber);
         AV14CopyNumber = 1;
         AV8WWPFormReferenceName = StringUtil.Format( "%1Copy%2", A208WWPFormReferenceName, StringUtil.Trim( StringUtil.Str( (decimal)(AV14CopyNumber), 4, 0)), "", "", "", "", "", "", "");
         while ( ! new GeneXus.Programs.workwithplus.dynamicforms.wwp_df_validateuniquereferencename(context).executeUdp(  0,  AV8WWPFormReferenceName) )
         {
            AV14CopyNumber = (short)(AV14CopyNumber+1);
            AV8WWPFormReferenceName = StringUtil.Format( "%1Copy%2", A208WWPFormReferenceName, StringUtil.Trim( StringUtil.Str( (decimal)(AV14CopyNumber), 4, 0)), "", "", "", "", "", "", "");
         }
         AV49NewWWPForm = new SdtUForm(context);
         /* Using cursor H00BB6 */
         pr_default.execute(4);
         while ( (pr_default.getStatus(4) != 101) )
         {
            A206WWPFormId = H00BB6_A206WWPFormId[0];
            AV49NewWWPForm.gxTpr_Wwpformid = A206WWPFormId;
            /* Exit For each command. Update data (if necessary), close cursors & exit. */
            if (true) break;
            pr_default.readNext(4);
         }
         pr_default.close(4);
         AV49NewWWPForm.gxTpr_Wwpformid = (short)(AV49NewWWPForm.gxTpr_Wwpformid+1);
         AV49NewWWPForm.gxTpr_Wwpformversionnumber = 1;
         AV49NewWWPForm.gxTpr_Wwpformreferencename = AV8WWPFormReferenceName;
         AV49NewWWPForm.gxTpr_Wwpformtitle = AV74WWPForm.gxTpr_Wwpformtitle;
         AV49NewWWPForm.gxTpr_Wwpformiswizard = AV74WWPForm.gxTpr_Wwpformiswizard;
         AV49NewWWPForm.gxTpr_Wwpformdate = DateTimeUtil.Now( context);
         AV49NewWWPForm.gxTpr_Wwpformvalidations = AV74WWPForm.gxTpr_Wwpformvalidations;
         AV49NewWWPForm.gxTpr_Wwpformresume = AV74WWPForm.gxTpr_Wwpformresume;
         AV49NewWWPForm.gxTpr_Wwpformresumemessage = AV74WWPForm.gxTpr_Wwpformresumemessage;
         AV80GXV1 = 1;
         while ( AV80GXV1 <= AV74WWPForm.gxTpr_Element.Count )
         {
            AV21Element = ((SdtUForm_Element)AV74WWPForm.gxTpr_Element.Item(AV80GXV1));
            if ( AV21Element.gxTpr_Wwpformelementparentid >= 0 )
            {
               AV49NewWWPForm.gxTpr_Element.Add(AV21Element, 0);
            }
            AV80GXV1 = (int)(AV80GXV1+1);
         }
         if ( AV49NewWWPForm.Insert() )
         {
            context.CommitDataStores("wp_dynamicformtrn_locationdynamicformwc",pr_default);
            if ( ! (Guid.Empty==AV5LocationId) )
            {
               AV7Trn_LocationDynamicForm = new SdtTrn_LocationDynamicForm(context);
               AV7Trn_LocationDynamicForm.gxTpr_Locationdynamicformid = Guid.NewGuid( );
               AV7Trn_LocationDynamicForm.gxTpr_Locationid = AV5LocationId;
               AV7Trn_LocationDynamicForm.gxTpr_Organisationid = AV6OrganisationId;
               AV7Trn_LocationDynamicForm.gxTpr_Wwpformid = AV49NewWWPForm.gxTpr_Wwpformid;
               AV7Trn_LocationDynamicForm.gxTpr_Wwpformversionnumber = AV49NewWWPForm.gxTpr_Wwpformversionnumber;
               if ( AV7Trn_LocationDynamicForm.Insert() )
               {
                  context.CommitDataStores("wp_dynamicformtrn_locationdynamicformwc",pr_default);
               }
               else
               {
                  AV82GXV3 = 1;
                  AV81GXV2 = AV7Trn_LocationDynamicForm.GetMessages();
                  while ( AV82GXV3 <= AV81GXV2.Count )
                  {
                     AV47Message = ((GeneXus.Utils.SdtMessages_Message)AV81GXV2.Item(AV82GXV3));
                     GX_msglist.addItem(AV47Message.gxTpr_Description);
                     AV82GXV3 = (int)(AV82GXV3+1);
                  }
               }
            }
            GX_msglist.addItem(new WorkWithPlus.workwithplus_web.dvmessagegetbasicnotificationmsg(context).executeUdp(  context.GetMessage( "WWP_DF_Copy_SuccessTitle", ""),  context.GetMessage( "WWP_DF_Copy_Success", ""),  "success",  "",  "na",  ""));
            gxgrGrid_refresh( subGrid_Rows, AV50OrderedBy, AV52OrderedDsc, AV22FilterFullText, AV9WWPFormType, AV43LocationIdFilter, AV73WWPContext, AV45ManageFiltersExecutionStep, AV11ColumnsSelector, AV78Pgmname, AV65TFWWPFormTitle, AV66TFWWPFormTitle_Sel, AV63TFWWPFormReferenceName, AV64TFWWPFormReferenceName_Sel, AV59TFWWPFormDate, AV60TFWWPFormDate_To, AV67TFWWPFormVersionNumber, AV68TFWWPFormVersionNumber_To, AV61TFWWPFormLatestVersionNumber, AV62TFWWPFormLatestVersionNumber_To, AV76WWPFormIsForDynamicValidations, AV39IsAuthorized_UserActionEdit, AV38IsAuthorized_UserActionDisplay, AV36IsAuthorized_UserActionCopy, AV37IsAuthorized_UserActionDelete, AV40IsAuthorized_UserActionFilledForms, AV41IsAuthorized_UserActionFillOutForm, AV53OrganisationIdFilter, A366LocationDynamicFormId, sPrefix) ;
         }
         else
         {
            AV84GXV5 = 1;
            AV83GXV4 = AV49NewWWPForm.GetMessages();
            while ( AV84GXV5 <= AV83GXV4.Count )
            {
               AV47Message = ((GeneXus.Utils.SdtMessages_Message)AV83GXV4.Item(AV84GXV5));
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV56ResultMsg)) )
               {
                  AV56ResultMsg += StringUtil.NewLine( );
                  AssignAttri(sPrefix, false, "AV56ResultMsg", AV56ResultMsg);
               }
               AV56ResultMsg += AV47Message.gxTpr_Description;
               AssignAttri(sPrefix, false, "AV56ResultMsg", AV56ResultMsg);
               AV84GXV5 = (int)(AV84GXV5+1);
            }
            GX_msglist.addItem(new WorkWithPlus.workwithplus_web.dvmessagegetbasicnotificationmsg(context).executeUdp(  context.GetMessage( "WWP_DF_ErrorCloning", ""),  AV56ResultMsg,  "error",  "",  "false",  ""));
         }
      }

      protected void S242( )
      {
         /* 'DO USERACTIONDELETE' Routine */
         returnInSub = false;
         if ( AV37IsAuthorized_UserActionDelete )
         {
            AV85Locationdynamicformid_selected = A366LocationDynamicFormId;
            AV86Organisationid_selected = A11OrganisationId;
            AV87Locationid_selected = A29LocationId;
            this.executeUsercontrolMethod(sPrefix, false, "DVELOP_CONFIRMPANEL_USERACTIONDELETEContainer", "Confirm", "", new Object[] {});
         }
         else
         {
            GX_msglist.addItem(context.GetMessage( "WWP_ActionNoLongerAvailable", ""));
            context.DoAjaxRefreshCmp(sPrefix);
         }
      }

      protected void S272( )
      {
         /* 'DO ACTION USERACTIONDELETE' Routine */
         returnInSub = false;
         new prc_deletelocationform(context ).execute(  A206WWPFormId,  A207WWPFormVersionNumber,  A366LocationDynamicFormId,  A11OrganisationId,  A29LocationId, out  AV48Messages) ;
         if ( AV48Messages.Count > 0 )
         {
            AV88GXV6 = 1;
            while ( AV88GXV6 <= AV48Messages.Count )
            {
               AV47Message = ((GeneXus.Utils.SdtMessages_Message)AV48Messages.Item(AV88GXV6));
               GX_msglist.addItem(AV47Message.gxTpr_Description);
               AV88GXV6 = (int)(AV88GXV6+1);
            }
         }
         else
         {
            gxgrGrid_refresh( subGrid_Rows, AV50OrderedBy, AV52OrderedDsc, AV22FilterFullText, AV9WWPFormType, AV43LocationIdFilter, AV73WWPContext, AV45ManageFiltersExecutionStep, AV11ColumnsSelector, AV78Pgmname, AV65TFWWPFormTitle, AV66TFWWPFormTitle_Sel, AV63TFWWPFormReferenceName, AV64TFWWPFormReferenceName_Sel, AV59TFWWPFormDate, AV60TFWWPFormDate_To, AV67TFWWPFormVersionNumber, AV68TFWWPFormVersionNumber_To, AV61TFWWPFormLatestVersionNumber, AV62TFWWPFormLatestVersionNumber_To, AV76WWPFormIsForDynamicValidations, AV39IsAuthorized_UserActionEdit, AV38IsAuthorized_UserActionDisplay, AV36IsAuthorized_UserActionCopy, AV37IsAuthorized_UserActionDelete, AV40IsAuthorized_UserActionFilledForms, AV41IsAuthorized_UserActionFillOutForm, AV53OrganisationIdFilter, A366LocationDynamicFormId, sPrefix) ;
         }
      }

      protected void S252( )
      {
         /* 'DO USERACTIONFILLEDFORMS' Routine */
         returnInSub = false;
         if ( AV40IsAuthorized_UserActionFilledForms )
         {
            CallWebObject(formatLink("ufilledoutforms.aspx", new object[] {UrlEncode(StringUtil.LTrimStr(A206WWPFormId,4,0)),UrlEncode(StringUtil.RTrim(A209WWPFormTitle))}, new string[] {"WWPFormId","WWPFormTitle"}) );
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem(context.GetMessage( "WWP_ActionNoLongerAvailable", ""));
            context.DoAjaxRefreshCmp(sPrefix);
         }
      }

      protected void S262( )
      {
         /* 'DO USERACTIONFILLOUTFORM' Routine */
         returnInSub = false;
         if ( AV41IsAuthorized_UserActionFillOutForm )
         {
            CallWebObject(formatLink("workwithplus.dynamicforms.wwp_dynamicform.aspx", new object[] {UrlEncode(StringUtil.RTrim(A208WWPFormReferenceName)),UrlEncode(StringUtil.LTrimStr(0,1,0)),UrlEncode(StringUtil.RTrim("INS"))}, new string[] {"WWPFormReferenceName","WWPFormInstanceId","WWPDynamicFormMode","isLinkingDiscussion"}) );
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem(context.GetMessage( "WWP_ActionNoLongerAvailable", ""));
            context.DoAjaxRefreshCmp(sPrefix);
         }
      }

      protected void S142( )
      {
         /* 'LOADGRIDSTATE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV58Session.Get(AV78Pgmname+"GridState"), "") == 0 )
         {
            AV28GridState.FromXml(new WorkWithPlus.workwithplus_web.loadgridstate(context).executeUdp(  AV78Pgmname+"GridState"), null, "", "");
         }
         else
         {
            AV28GridState.FromXml(AV58Session.Get(AV78Pgmname+"GridState"), null, "", "");
         }
         AV50OrderedBy = AV28GridState.gxTpr_Orderedby;
         AssignAttri(sPrefix, false, "AV50OrderedBy", StringUtil.LTrimStr( (decimal)(AV50OrderedBy), 4, 0));
         AV52OrderedDsc = AV28GridState.gxTpr_Ordereddsc;
         AssignAttri(sPrefix, false, "AV52OrderedDsc", AV52OrderedDsc);
         /* Execute user subroutine: 'SETDDOSORTEDSTATUS' */
         S152 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         /* Execute user subroutine: 'LOADREGFILTERSSTATE' */
         S202 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV28GridState.gxTpr_Pagesize))) )
         {
            subGrid_Rows = (int)(Math.Round(NumberUtil.Val( AV28GridState.gxTpr_Pagesize, "."), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         }
      }

      protected void S202( )
      {
         /* 'LOADREGFILTERSSTATE' Routine */
         returnInSub = false;
         AV89GXV7 = 1;
         while ( AV89GXV7 <= AV28GridState.gxTpr_Filtervalues.Count )
         {
            AV29GridStateFilterValue = ((WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue)AV28GridState.gxTpr_Filtervalues.Item(AV89GXV7));
            if ( StringUtil.StrCmp(AV29GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV22FilterFullText = AV29GridStateFilterValue.gxTpr_Value;
               AssignAttri(sPrefix, false, "AV22FilterFullText", AV22FilterFullText);
            }
            else if ( StringUtil.StrCmp(AV29GridStateFilterValue.gxTpr_Name, "TFWWPFORMTITLE") == 0 )
            {
               AV65TFWWPFormTitle = AV29GridStateFilterValue.gxTpr_Value;
               AssignAttri(sPrefix, false, "AV65TFWWPFormTitle", AV65TFWWPFormTitle);
            }
            else if ( StringUtil.StrCmp(AV29GridStateFilterValue.gxTpr_Name, "TFWWPFORMTITLE_SEL") == 0 )
            {
               AV66TFWWPFormTitle_Sel = AV29GridStateFilterValue.gxTpr_Value;
               AssignAttri(sPrefix, false, "AV66TFWWPFormTitle_Sel", AV66TFWWPFormTitle_Sel);
            }
            else if ( StringUtil.StrCmp(AV29GridStateFilterValue.gxTpr_Name, "TFWWPFORMREFERENCENAME") == 0 )
            {
               AV63TFWWPFormReferenceName = AV29GridStateFilterValue.gxTpr_Value;
               AssignAttri(sPrefix, false, "AV63TFWWPFormReferenceName", AV63TFWWPFormReferenceName);
            }
            else if ( StringUtil.StrCmp(AV29GridStateFilterValue.gxTpr_Name, "TFWWPFORMREFERENCENAME_SEL") == 0 )
            {
               AV64TFWWPFormReferenceName_Sel = AV29GridStateFilterValue.gxTpr_Value;
               AssignAttri(sPrefix, false, "AV64TFWWPFormReferenceName_Sel", AV64TFWWPFormReferenceName_Sel);
            }
            else if ( StringUtil.StrCmp(AV29GridStateFilterValue.gxTpr_Name, "TFWWPFORMDATE") == 0 )
            {
               AV59TFWWPFormDate = context.localUtil.CToT( AV29GridStateFilterValue.gxTpr_Value, DateTimeUtil.MapDateFormat( context.GetLanguageProperty( "date_fmt")));
               AssignAttri(sPrefix, false, "AV59TFWWPFormDate", context.localUtil.TToC( AV59TFWWPFormDate, 8, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
               AV60TFWWPFormDate_To = context.localUtil.CToT( AV29GridStateFilterValue.gxTpr_Valueto, DateTimeUtil.MapDateFormat( context.GetLanguageProperty( "date_fmt")));
               AssignAttri(sPrefix, false, "AV60TFWWPFormDate_To", context.localUtil.TToC( AV60TFWWPFormDate_To, 8, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
               AV16DDO_WWPFormDateAuxDate = DateTimeUtil.ResetTime(AV59TFWWPFormDate);
               AssignAttri(sPrefix, false, "AV16DDO_WWPFormDateAuxDate", context.localUtil.Format(AV16DDO_WWPFormDateAuxDate, "99/99/99"));
               AV18DDO_WWPFormDateAuxDateTo = DateTimeUtil.ResetTime(AV60TFWWPFormDate_To);
               AssignAttri(sPrefix, false, "AV18DDO_WWPFormDateAuxDateTo", context.localUtil.Format(AV18DDO_WWPFormDateAuxDateTo, "99/99/99"));
            }
            else if ( StringUtil.StrCmp(AV29GridStateFilterValue.gxTpr_Name, "TFWWPFORMVERSIONNUMBER") == 0 )
            {
               AV67TFWWPFormVersionNumber = (short)(Math.Round(NumberUtil.Val( AV29GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV67TFWWPFormVersionNumber", StringUtil.LTrimStr( (decimal)(AV67TFWWPFormVersionNumber), 4, 0));
               AV68TFWWPFormVersionNumber_To = (short)(Math.Round(NumberUtil.Val( AV29GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV68TFWWPFormVersionNumber_To", StringUtil.LTrimStr( (decimal)(AV68TFWWPFormVersionNumber_To), 4, 0));
            }
            else if ( StringUtil.StrCmp(AV29GridStateFilterValue.gxTpr_Name, "TFWWPFORMLATESTVERSIONNUMBER") == 0 )
            {
               AV61TFWWPFormLatestVersionNumber = (short)(Math.Round(NumberUtil.Val( AV29GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV61TFWWPFormLatestVersionNumber", StringUtil.LTrimStr( (decimal)(AV61TFWWPFormLatestVersionNumber), 4, 0));
               AV62TFWWPFormLatestVersionNumber_To = (short)(Math.Round(NumberUtil.Val( AV29GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV62TFWWPFormLatestVersionNumber_To", StringUtil.LTrimStr( (decimal)(AV62TFWWPFormLatestVersionNumber_To), 4, 0));
            }
            AV89GXV7 = (int)(AV89GXV7+1);
         }
         GXt_char3 = "";
         new WorkWithPlus.workwithplus_web.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV66TFWWPFormTitle_Sel)),  AV66TFWWPFormTitle_Sel, out  GXt_char3) ;
         GXt_char6 = "";
         new WorkWithPlus.workwithplus_web.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV64TFWWPFormReferenceName_Sel)),  AV64TFWWPFormReferenceName_Sel, out  GXt_char6) ;
         Ddo_grid_Selectedvalue_set = GXt_char3+"|"+GXt_char6+"|||";
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "SelectedValue_set", Ddo_grid_Selectedvalue_set);
         GXt_char6 = "";
         new WorkWithPlus.workwithplus_web.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV65TFWWPFormTitle)),  AV65TFWWPFormTitle, out  GXt_char6) ;
         GXt_char3 = "";
         new WorkWithPlus.workwithplus_web.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV63TFWWPFormReferenceName)),  AV63TFWWPFormReferenceName, out  GXt_char3) ;
         Ddo_grid_Filteredtext_set = GXt_char6+"|"+GXt_char3+"|"+((DateTime.MinValue==AV59TFWWPFormDate) ? "" : context.localUtil.DToC( AV16DDO_WWPFormDateAuxDate, (short)(DateTimeUtil.MapDateFormat( context.GetLanguageProperty( "date_fmt"))), "/"))+"|"+((0==AV67TFWWPFormVersionNumber) ? "" : StringUtil.Str( (decimal)(AV67TFWWPFormVersionNumber), 4, 0))+"|"+((0==AV61TFWWPFormLatestVersionNumber) ? "" : StringUtil.Str( (decimal)(AV61TFWWPFormLatestVersionNumber), 4, 0));
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "FilteredText_set", Ddo_grid_Filteredtext_set);
         Ddo_grid_Filteredtextto_set = "||"+((DateTime.MinValue==AV60TFWWPFormDate_To) ? "" : context.localUtil.DToC( AV18DDO_WWPFormDateAuxDateTo, (short)(DateTimeUtil.MapDateFormat( context.GetLanguageProperty( "date_fmt"))), "/"))+"|"+((0==AV68TFWWPFormVersionNumber_To) ? "" : StringUtil.Str( (decimal)(AV68TFWWPFormVersionNumber_To), 4, 0))+"|"+((0==AV62TFWWPFormLatestVersionNumber_To) ? "" : StringUtil.Str( (decimal)(AV62TFWWPFormLatestVersionNumber_To), 4, 0));
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "FilteredTextTo_set", Ddo_grid_Filteredtextto_set);
      }

      protected void S172( )
      {
         /* 'SAVEGRIDSTATE' Routine */
         returnInSub = false;
         AV28GridState.FromXml(AV58Session.Get(AV78Pgmname+"GridState"), null, "", "");
         AV28GridState.gxTpr_Orderedby = AV50OrderedBy;
         AV28GridState.gxTpr_Ordereddsc = AV52OrderedDsc;
         AV28GridState.gxTpr_Filtervalues.Clear();
         new WorkWithPlus.workwithplus_web.wwp_gridstateaddfiltervalue(context ).execute( ref  AV28GridState,  "FILTERFULLTEXT",  context.GetMessage( "WWP_FullTextFilterDescription", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV22FilterFullText)),  0,  AV22FilterFullText,  AV22FilterFullText,  false,  "",  "") ;
         new WorkWithPlus.workwithplus_web.wwp_gridstateaddfiltervalueandsel(context ).execute( ref  AV28GridState,  "TFWWPFORMTITLE",  context.GetMessage( "Title", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV65TFWWPFormTitle)),  0,  AV65TFWWPFormTitle,  AV65TFWWPFormTitle,  false,  "",  "",  !String.IsNullOrEmpty(StringUtil.RTrim( AV66TFWWPFormTitle_Sel)),  AV66TFWWPFormTitle_Sel,  AV66TFWWPFormTitle_Sel) ;
         new WorkWithPlus.workwithplus_web.wwp_gridstateaddfiltervalueandsel(context ).execute( ref  AV28GridState,  "TFWWPFORMREFERENCENAME",  context.GetMessage( "Reference Name", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV63TFWWPFormReferenceName)),  0,  AV63TFWWPFormReferenceName,  AV63TFWWPFormReferenceName,  false,  "",  "",  !String.IsNullOrEmpty(StringUtil.RTrim( AV64TFWWPFormReferenceName_Sel)),  AV64TFWWPFormReferenceName_Sel,  AV64TFWWPFormReferenceName_Sel) ;
         new WorkWithPlus.workwithplus_web.wwp_gridstateaddfiltervalue(context ).execute( ref  AV28GridState,  "TFWWPFORMDATE",  context.GetMessage( "Date", ""),  !((DateTime.MinValue==AV59TFWWPFormDate)&&(DateTime.MinValue==AV60TFWWPFormDate_To)),  0,  StringUtil.Trim( context.localUtil.TToC( AV59TFWWPFormDate, 8, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " ")),  ((DateTime.MinValue==AV59TFWWPFormDate) ? "" : StringUtil.Trim( context.localUtil.Format( AV59TFWWPFormDate, "99/99/99 99:99"))),  true,  StringUtil.Trim( context.localUtil.TToC( AV60TFWWPFormDate_To, 8, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " ")),  ((DateTime.MinValue==AV60TFWWPFormDate_To) ? "" : StringUtil.Trim( context.localUtil.Format( AV60TFWWPFormDate_To, "99/99/99 99:99")))) ;
         new WorkWithPlus.workwithplus_web.wwp_gridstateaddfiltervalue(context ).execute( ref  AV28GridState,  "TFWWPFORMVERSIONNUMBER",  context.GetMessage( "Form Version #", ""),  !((0==AV67TFWWPFormVersionNumber)&&(0==AV68TFWWPFormVersionNumber_To)),  0,  StringUtil.Trim( StringUtil.Str( (decimal)(AV67TFWWPFormVersionNumber), 4, 0)),  ((0==AV67TFWWPFormVersionNumber) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV67TFWWPFormVersionNumber), "ZZZ9"))),  true,  StringUtil.Trim( StringUtil.Str( (decimal)(AV68TFWWPFormVersionNumber_To), 4, 0)),  ((0==AV68TFWWPFormVersionNumber_To) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV68TFWWPFormVersionNumber_To), "ZZZ9")))) ;
         new WorkWithPlus.workwithplus_web.wwp_gridstateaddfiltervalue(context ).execute( ref  AV28GridState,  "TFWWPFORMLATESTVERSIONNUMBER",  context.GetMessage( "Latest Version", ""),  !((0==AV61TFWWPFormLatestVersionNumber)&&(0==AV62TFWWPFormLatestVersionNumber_To)),  0,  StringUtil.Trim( StringUtil.Str( (decimal)(AV61TFWWPFormLatestVersionNumber), 4, 0)),  ((0==AV61TFWWPFormLatestVersionNumber) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV61TFWWPFormLatestVersionNumber), "ZZZ9"))),  true,  StringUtil.Trim( StringUtil.Str( (decimal)(AV62TFWWPFormLatestVersionNumber_To), 4, 0)),  ((0==AV62TFWWPFormLatestVersionNumber_To) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV62TFWWPFormLatestVersionNumber_To), "ZZZ9")))) ;
         if ( ! (0==AV9WWPFormType) )
         {
            AV29GridStateFilterValue = new WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue(context);
            AV29GridStateFilterValue.gxTpr_Name = "PARM_&WWPFORMTYPE";
            AV29GridStateFilterValue.gxTpr_Value = StringUtil.Str( (decimal)(AV9WWPFormType), 1, 0);
            AV28GridState.gxTpr_Filtervalues.Add(AV29GridStateFilterValue, 0);
         }
         if ( ! (false==AV76WWPFormIsForDynamicValidations) )
         {
            AV29GridStateFilterValue = new WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue(context);
            AV29GridStateFilterValue.gxTpr_Name = "PARM_&WWPFORMISFORDYNAMICVALIDATIONS";
            AV29GridStateFilterValue.gxTpr_Value = StringUtil.BoolToStr( AV76WWPFormIsForDynamicValidations);
            AV28GridState.gxTpr_Filtervalues.Add(AV29GridStateFilterValue, 0);
         }
         AV28GridState.gxTpr_Pagesize = StringUtil.Str( (decimal)(subGrid_Rows), 10, 0);
         new WorkWithPlus.workwithplus_web.savegridstate(context ).execute(  AV78Pgmname+"GridState",  AV28GridState.ToXml(false, true, "", "")) ;
      }

      protected void S132( )
      {
         /* 'PREPARETRANSACTION' Routine */
         returnInSub = false;
         AV69TrnContext = new WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext(context);
         AV69TrnContext.gxTpr_Callerobject = AV78Pgmname;
         AV69TrnContext.gxTpr_Callerondelete = true;
         AV69TrnContext.gxTpr_Callerurl = AV30HTTPRequest.ScriptName+"?"+AV30HTTPRequest.QueryString;
         AV69TrnContext.gxTpr_Transactionname = "Trn_LocationDynamicForm";
         AV70TrnContextAtt = new WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext_Attribute(context);
         AV70TrnContextAtt.gxTpr_Attributename = "WWPFormType";
         AV70TrnContextAtt.gxTpr_Attributevalue = StringUtil.Str( (decimal)(AV9WWPFormType), 1, 0);
         AV69TrnContext.gxTpr_Attributes.Add(AV70TrnContextAtt, 0);
         AV58Session.Set("TrnContext", AV69TrnContext.ToXml(false, true, "", ""));
      }

      protected void S112( )
      {
         /* 'ATTRIBUTESSECURITYCODE' Routine */
         returnInSub = false;
         if ( ! ( (Guid.Empty==AV73WWPContext.gxTpr_Organisationid) ) )
         {
            dynavOrganisationidfilter.Visible = 0;
            AssignProp(sPrefix, false, dynavOrganisationidfilter_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(dynavOrganisationidfilter.Visible), 5, 0), true);
            divOrganisationidfilter_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divOrganisationidfilter_cell_Internalname, "Class", divOrganisationidfilter_cell_Class, true);
         }
         else
         {
            dynavOrganisationidfilter.Visible = 1;
            AssignProp(sPrefix, false, dynavOrganisationidfilter_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(dynavOrganisationidfilter.Visible), 5, 0), true);
            divOrganisationidfilter_cell_Class = "";
            AssignProp(sPrefix, false, divOrganisationidfilter_cell_Internalname, "Class", divOrganisationidfilter_cell_Class, true);
         }
         if ( ! ( (Guid.Empty==AV73WWPContext.gxTpr_Locationid) ) )
         {
            dynavLocationidfilter.Visible = 0;
            AssignProp(sPrefix, false, dynavLocationidfilter_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(dynavLocationidfilter.Visible), 5, 0), true);
            divLocationidfilter_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divLocationidfilter_cell_Internalname, "Class", divLocationidfilter_cell_Class, true);
         }
         else
         {
            dynavLocationidfilter.Visible = 1;
            AssignProp(sPrefix, false, dynavLocationidfilter_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(dynavLocationidfilter.Visible), 5, 0), true);
            divLocationidfilter_cell_Class = "";
            AssignProp(sPrefix, false, divLocationidfilter_cell_Internalname, "Class", divLocationidfilter_cell_Class, true);
         }
      }

      protected void E18BB2( )
      {
         /* Locationidfilter_Controlvaluechanged Routine */
         returnInSub = false;
         AV5LocationId = AV43LocationIdFilter;
         AssignAttri(sPrefix, false, "AV5LocationId", AV5LocationId.ToString());
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV73WWPContext", AV73WWPContext);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV11ColumnsSelector", AV11ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV44ManageFiltersData", AV44ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV28GridState", AV28GridState);
      }

      protected void wb_table1_67_BB2( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblTabledvelop_confirmpanel_useractiondelete_Internalname, tblTabledvelop_confirmpanel_useractiondelete_Internalname, "", "Table", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tbody>") ;
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td data-align=\"center\"  style=\""+CSSHelper.Prettify( "text-align:-khtml-center;text-align:-moz-center;text-align:-webkit-center")+"\">") ;
            /* User Defined Control */
            ucDvelop_confirmpanel_useractiondelete.SetProperty("Title", Dvelop_confirmpanel_useractiondelete_Title);
            ucDvelop_confirmpanel_useractiondelete.SetProperty("ConfirmationText", Dvelop_confirmpanel_useractiondelete_Confirmationtext);
            ucDvelop_confirmpanel_useractiondelete.SetProperty("YesButtonCaption", Dvelop_confirmpanel_useractiondelete_Yesbuttoncaption);
            ucDvelop_confirmpanel_useractiondelete.SetProperty("NoButtonCaption", Dvelop_confirmpanel_useractiondelete_Nobuttoncaption);
            ucDvelop_confirmpanel_useractiondelete.SetProperty("CancelButtonCaption", Dvelop_confirmpanel_useractiondelete_Cancelbuttoncaption);
            ucDvelop_confirmpanel_useractiondelete.SetProperty("YesButtonPosition", Dvelop_confirmpanel_useractiondelete_Yesbuttonposition);
            ucDvelop_confirmpanel_useractiondelete.SetProperty("ConfirmType", Dvelop_confirmpanel_useractiondelete_Confirmtype);
            ucDvelop_confirmpanel_useractiondelete.Render(context, "dvelop.gxbootstrap.confirmpanel", Dvelop_confirmpanel_useractiondelete_Internalname, sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETEContainer");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETEContainer"+"Body"+"\" style=\"display:none;\">") ;
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            context.WriteHtmlText( "</tbody>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table1_67_BB2e( true) ;
         }
         else
         {
            wb_table1_67_BB2e( false) ;
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV9WWPFormType = Convert.ToInt16(getParm(obj,0));
         AssignAttri(sPrefix, false, "AV9WWPFormType", StringUtil.Str( (decimal)(AV9WWPFormType), 1, 0));
         AV76WWPFormIsForDynamicValidations = (bool)getParm(obj,1);
         AssignAttri(sPrefix, false, "AV76WWPFormIsForDynamicValidations", AV76WWPFormIsForDynamicValidations);
      }

      public override string getresponse( string sGXDynURL )
      {
         initialize_properties( ) ;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         sDynURL = sGXDynURL;
         nGotPars = (short)(1);
         nGXWrapped = (short)(1);
         context.SetWrapped(true);
         PABB2( ) ;
         WSBB2( ) ;
         WEBB2( ) ;
         cleanup();
         context.SetWrapped(false);
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      public override void componentbind( Object[] obj )
      {
         if ( IsUrlCreated( ) )
         {
            return  ;
         }
         sCtrlAV9WWPFormType = (string)((string)getParm(obj,0));
         sCtrlAV76WWPFormIsForDynamicValidations = (string)((string)getParm(obj,1));
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PABB2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wp_dynamicformtrn_locationdynamicformwc", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PABB2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
            AV9WWPFormType = Convert.ToInt16(getParm(obj,2));
            AssignAttri(sPrefix, false, "AV9WWPFormType", StringUtil.Str( (decimal)(AV9WWPFormType), 1, 0));
            AV76WWPFormIsForDynamicValidations = (bool)getParm(obj,3);
            AssignAttri(sPrefix, false, "AV76WWPFormIsForDynamicValidations", AV76WWPFormIsForDynamicValidations);
         }
         wcpOAV9WWPFormType = (short)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"wcpOAV9WWPFormType"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
         wcpOAV76WWPFormIsForDynamicValidations = StringUtil.StrToBool( cgiGet( sPrefix+"wcpOAV76WWPFormIsForDynamicValidations"));
         if ( ! GetJustCreated( ) && ( ( AV9WWPFormType != wcpOAV9WWPFormType ) || ( AV76WWPFormIsForDynamicValidations != wcpOAV76WWPFormIsForDynamicValidations ) ) )
         {
            setjustcreated();
         }
         wcpOAV9WWPFormType = AV9WWPFormType;
         wcpOAV76WWPFormIsForDynamicValidations = AV76WWPFormIsForDynamicValidations;
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
         sCtrlAV9WWPFormType = cgiGet( sPrefix+"AV9WWPFormType_CTRL");
         if ( StringUtil.Len( sCtrlAV9WWPFormType) > 0 )
         {
            AV9WWPFormType = (short)(Math.Round(context.localUtil.CToN( cgiGet( sCtrlAV9WWPFormType), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AssignAttri(sPrefix, false, "AV9WWPFormType", StringUtil.Str( (decimal)(AV9WWPFormType), 1, 0));
         }
         else
         {
            AV9WWPFormType = (short)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"AV9WWPFormType_PARM"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
         }
         sCtrlAV76WWPFormIsForDynamicValidations = cgiGet( sPrefix+"AV76WWPFormIsForDynamicValidations_CTRL");
         if ( StringUtil.Len( sCtrlAV76WWPFormIsForDynamicValidations) > 0 )
         {
            AV76WWPFormIsForDynamicValidations = StringUtil.StrToBool( cgiGet( sCtrlAV76WWPFormIsForDynamicValidations));
            AssignAttri(sPrefix, false, "AV76WWPFormIsForDynamicValidations", AV76WWPFormIsForDynamicValidations);
         }
         else
         {
            AV76WWPFormIsForDynamicValidations = StringUtil.StrToBool( cgiGet( sPrefix+"AV76WWPFormIsForDynamicValidations_PARM"));
         }
      }

      public override void componentprocess( string sPPrefix ,
                                             string sPSFPrefix ,
                                             string sCompEvt )
      {
         sCompPrefix = sPPrefix;
         sSFPrefix = sPSFPrefix;
         sPrefix = sCompPrefix + sSFPrefix;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         INITWEB( ) ;
         nDraw = 0;
         PABB2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WSBB2( ) ;
         if ( isFullAjaxMode( ) )
         {
            componentdraw();
         }
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override void componentstart( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
      }

      protected void WCStart( )
      {
         nDraw = 1;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WSBB2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"AV9WWPFormType_PARM", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV9WWPFormType), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV9WWPFormType)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV9WWPFormType_CTRL", StringUtil.RTrim( sCtrlAV9WWPFormType));
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"AV76WWPFormIsForDynamicValidations_PARM", StringUtil.BoolToStr( AV76WWPFormIsForDynamicValidations));
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV76WWPFormIsForDynamicValidations)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV76WWPFormIsForDynamicValidations_CTRL", StringUtil.RTrim( sCtrlAV76WWPFormIsForDynamicValidations));
         }
      }

      public override void componentdraw( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WCParametersSet( ) ;
         WEBB2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override string getstring( string sGXControl )
      {
         string sCtrlName;
         if ( StringUtil.StrCmp(StringUtil.Substring( sGXControl, 1, 1), "&") == 0 )
         {
            sCtrlName = StringUtil.Substring( sGXControl, 2, StringUtil.Len( sGXControl)-1);
         }
         else
         {
            sCtrlName = sGXControl;
         }
         return cgiGet( sPrefix+"v"+StringUtil.Upper( sCtrlName)) ;
      }

      public override void componentjscripts( )
      {
         include_jscripts( ) ;
         if ( ! ( WebComp_Wwpaux_wc == null ) )
         {
            WebComp_Wwpaux_wc.componentjscripts();
         }
      }

      public override void componentthemes( )
      {
         define_styles( ) ;
      }

      protected void define_styles( )
      {
         AddStyleSheetFile("DVelop/DVPaginationBar/DVPaginationBar.css", "");
         AddStyleSheetFile("DVelop/Bootstrap/Shared/DVelopBootstrap.css", "");
         AddStyleSheetFile("DVelop/Shared/daterangepicker/daterangepicker.css", "");
         AddStyleSheetFile("calendar-system.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         if ( ! ( WebComp_Wwpaux_wc == null ) )
         {
            if ( StringUtil.Len( WebComp_Wwpaux_wc_Component) != 0 )
            {
               WebComp_Wwpaux_wc.componentthemes();
            }
         }
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202542718121168", true, true);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         CloseStyles();
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         context.AddJavascriptSource("wp_dynamicformtrn_locationdynamicformwc.js", "?202542718121174", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/DVPaginationBar/DVPaginationBarRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/ConfirmPanel/BootstrapConfirmPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/GridEmpowerer/GridEmpowererRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/daterangepicker/locales.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/daterangepicker/wwp-daterangepicker.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/daterangepicker/moment.min.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/daterangepicker/daterangepicker.min.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/DateRangePicker/DateRangePickerRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_472( )
      {
         edtOrganisationId_Internalname = sPrefix+"ORGANISATIONID_"+sGXsfl_47_idx;
         edtLocationId_Internalname = sPrefix+"LOCATIONID_"+sGXsfl_47_idx;
         edtWWPFormId_Internalname = sPrefix+"WWPFORMID_"+sGXsfl_47_idx;
         edtWWPFormTitle_Internalname = sPrefix+"WWPFORMTITLE_"+sGXsfl_47_idx;
         edtWWPFormReferenceName_Internalname = sPrefix+"WWPFORMREFERENCENAME_"+sGXsfl_47_idx;
         edtWWPFormDate_Internalname = sPrefix+"WWPFORMDATE_"+sGXsfl_47_idx;
         edtWWPFormVersionNumber_Internalname = sPrefix+"WWPFORMVERSIONNUMBER_"+sGXsfl_47_idx;
         edtWWPFormLatestVersionNumber_Internalname = sPrefix+"WWPFORMLATESTVERSIONNUMBER_"+sGXsfl_47_idx;
         cmbavActiongroup_Internalname = sPrefix+"vACTIONGROUP_"+sGXsfl_47_idx;
      }

      protected void SubsflControlProps_fel_472( )
      {
         edtOrganisationId_Internalname = sPrefix+"ORGANISATIONID_"+sGXsfl_47_fel_idx;
         edtLocationId_Internalname = sPrefix+"LOCATIONID_"+sGXsfl_47_fel_idx;
         edtWWPFormId_Internalname = sPrefix+"WWPFORMID_"+sGXsfl_47_fel_idx;
         edtWWPFormTitle_Internalname = sPrefix+"WWPFORMTITLE_"+sGXsfl_47_fel_idx;
         edtWWPFormReferenceName_Internalname = sPrefix+"WWPFORMREFERENCENAME_"+sGXsfl_47_fel_idx;
         edtWWPFormDate_Internalname = sPrefix+"WWPFORMDATE_"+sGXsfl_47_fel_idx;
         edtWWPFormVersionNumber_Internalname = sPrefix+"WWPFORMVERSIONNUMBER_"+sGXsfl_47_fel_idx;
         edtWWPFormLatestVersionNumber_Internalname = sPrefix+"WWPFORMLATESTVERSIONNUMBER_"+sGXsfl_47_fel_idx;
         cmbavActiongroup_Internalname = sPrefix+"vACTIONGROUP_"+sGXsfl_47_fel_idx;
      }

      protected void sendrow_472( )
      {
         sGXsfl_47_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_47_idx), 4, 0), 4, "0");
         SubsflControlProps_472( ) ;
         WBBB0( ) ;
         if ( ( subGrid_Rows * 1 == 0 ) || ( nGXsfl_47_idx <= subGrid_fnc_Recordsperpage( ) * 1 ) )
         {
            GridRow = GXWebRow.GetNew(context,GridContainer);
            if ( subGrid_Backcolorstyle == 0 )
            {
               /* None style subfile background logic. */
               subGrid_Backstyle = 0;
               if ( StringUtil.StrCmp(subGrid_Class, "") != 0 )
               {
                  subGrid_Linesclass = subGrid_Class+"Odd";
               }
            }
            else if ( subGrid_Backcolorstyle == 1 )
            {
               /* Uniform style subfile background logic. */
               subGrid_Backstyle = 0;
               subGrid_Backcolor = subGrid_Allbackcolor;
               if ( StringUtil.StrCmp(subGrid_Class, "") != 0 )
               {
                  subGrid_Linesclass = subGrid_Class+"Uniform";
               }
            }
            else if ( subGrid_Backcolorstyle == 2 )
            {
               /* Header style subfile background logic. */
               subGrid_Backstyle = 1;
               if ( StringUtil.StrCmp(subGrid_Class, "") != 0 )
               {
                  subGrid_Linesclass = subGrid_Class+"Odd";
               }
               subGrid_Backcolor = (int)(0x0);
            }
            else if ( subGrid_Backcolorstyle == 3 )
            {
               /* Report style subfile background logic. */
               subGrid_Backstyle = 1;
               if ( ((int)((nGXsfl_47_idx) % (2))) == 0 )
               {
                  subGrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGrid_Class, "") != 0 )
                  {
                     subGrid_Linesclass = subGrid_Class+"Even";
                  }
               }
               else
               {
                  subGrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGrid_Class, "") != 0 )
                  {
                     subGrid_Linesclass = subGrid_Class+"Odd";
                  }
               }
            }
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<tr ") ;
               context.WriteHtmlText( " class=\""+"GridWithPaginationBar WorkWithSelection WorkWith"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_47_idx+"\">") ;
            }
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+"display:none;"+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtOrganisationId_Internalname,A11OrganisationId.ToString(),A11OrganisationId.ToString(),(string)"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtOrganisationId_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(short)0,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)36,(short)0,(short)0,(short)47,(short)0,(short)0,(short)0,(bool)true,(string)"Id",(string)"",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+"display:none;"+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtLocationId_Internalname,A29LocationId.ToString(),A29LocationId.ToString(),(string)"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtLocationId_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(short)0,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)36,(short)0,(short)0,(short)47,(short)0,(short)0,(short)0,(bool)true,(string)"Id",(string)"",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+"display:none;"+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtWWPFormId_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(A206WWPFormId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( (decimal)(A206WWPFormId), "ZZZ9")),(string)" dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtWWPFormId_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(short)0,(short)0,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)47,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtWWPFormTitle_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtWWPFormTitle_Internalname,(string)A209WWPFormTitle,(string)"",(string)"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)edtWWPFormTitle_Link,(string)"",(string)"",(string)"",(string)edtWWPFormTitle_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtWWPFormTitle_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)100,(short)0,(short)0,(short)47,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtWWPFormReferenceName_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtWWPFormReferenceName_Internalname,(string)A208WWPFormReferenceName,(string)"",(string)"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtWWPFormReferenceName_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtWWPFormReferenceName_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)100,(short)0,(short)0,(short)47,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtWWPFormDate_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtWWPFormDate_Internalname,context.localUtil.TToC( A231WWPFormDate, 10, 8, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "),context.localUtil.Format( A231WWPFormDate, "99/99/99 99:99"),(string)"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtWWPFormDate_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtWWPFormDate_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)47,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtWWPFormVersionNumber_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtWWPFormVersionNumber_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(A207WWPFormVersionNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( (decimal)(A207WWPFormVersionNumber), "ZZZ9")),(string)" dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtWWPFormVersionNumber_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtWWPFormVersionNumber_Visible,(short)0,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)47,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtWWPFormLatestVersionNumber_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtWWPFormLatestVersionNumber_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(A219WWPFormLatestVersionNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( (decimal)(A219WWPFormLatestVersionNumber), "ZZZ9")),(string)" dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtWWPFormLatestVersionNumber_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtWWPFormLatestVersionNumber_Visible,(short)0,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)47,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 56,'" + sPrefix + "',false,'" + sGXsfl_47_idx + "',47)\"";
            if ( ( cmbavActiongroup.ItemCount == 0 ) && isAjaxCallMode( ) )
            {
               GXCCtl = "vACTIONGROUP_" + sGXsfl_47_idx;
               cmbavActiongroup.Name = GXCCtl;
               cmbavActiongroup.WebTags = "";
               if ( cmbavActiongroup.ItemCount > 0 )
               {
                  AV10ActionGroup = (short)(Math.Round(NumberUtil.Val( cmbavActiongroup.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV10ActionGroup), 4, 0))), "."), 18, MidpointRounding.ToEven));
                  AssignAttri(sPrefix, false, cmbavActiongroup_Internalname, StringUtil.LTrimStr( (decimal)(AV10ActionGroup), 4, 0));
               }
            }
            /* ComboBox */
            GridRow.AddColumnProperties("combobox", 2, isAjaxCallMode( ), new Object[] {(GXCombobox)cmbavActiongroup,(string)cmbavActiongroup_Internalname,StringUtil.Trim( StringUtil.Str( (decimal)(AV10ActionGroup), 4, 0)),(short)1,(string)cmbavActiongroup_Jsonclick,(short)5,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"EVACTIONGROUP.CLICK."+sGXsfl_47_idx+"'",(string)"int",(string)"",(short)-1,(short)1,(short)0,(short)0,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)cmbavActiongroup_Class,(string)"WWActionGroupColumn",(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,56);\"",(string)"",(bool)true,(short)0});
            cmbavActiongroup.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV10ActionGroup), 4, 0));
            AssignProp(sPrefix, false, cmbavActiongroup_Internalname, "Values", (string)(cmbavActiongroup.ToJavascriptSource()), !bGXsfl_47_Refreshing);
            send_integrity_lvl_hashesBB2( ) ;
            GridContainer.AddRow(GridRow);
            nGXsfl_47_idx = ((subGrid_Islastpage==1)&&(nGXsfl_47_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_47_idx+1);
            sGXsfl_47_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_47_idx), 4, 0), 4, "0");
            SubsflControlProps_472( ) ;
         }
         /* End function sendrow_472 */
      }

      protected void init_web_controls( )
      {
         dynavOrganisationidfilter.Name = "vORGANISATIONIDFILTER";
         dynavOrganisationidfilter.WebTags = "";
         dynavLocationidfilter.Name = "vLOCATIONIDFILTER";
         dynavLocationidfilter.WebTags = "";
         GXCCtl = "vACTIONGROUP_" + sGXsfl_47_idx;
         cmbavActiongroup.Name = GXCCtl;
         cmbavActiongroup.WebTags = "";
         if ( cmbavActiongroup.ItemCount > 0 )
         {
         }
         cmbWWPFormType.Name = "WWPFORMTYPE";
         cmbWWPFormType.WebTags = "";
         cmbWWPFormType.addItem("0", context.GetMessage( "WWP_DF_Type_DynamicForm", ""), 0);
         cmbWWPFormType.addItem("1", context.GetMessage( "WWP_DF_Type_DynamicSection", ""), 0);
         if ( cmbWWPFormType.ItemCount > 0 )
         {
         }
         /* End function init_web_controls */
      }

      protected void StartGridControl47( )
      {
         if ( GridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"GridContainer"+"DivS\" data-gxgridid=\"47\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGrid_Internalname, subGrid_Internalname, "", "GridWithPaginationBar WorkWithSelection WorkWith", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGrid_Backcolorstyle == 0 )
            {
               subGrid_Titlebackstyle = 0;
               if ( StringUtil.Len( subGrid_Class) > 0 )
               {
                  subGrid_Linesclass = subGrid_Class+"Title";
               }
            }
            else
            {
               subGrid_Titlebackstyle = 1;
               if ( subGrid_Backcolorstyle == 1 )
               {
                  subGrid_Titlebackcolor = subGrid_Allbackcolor;
                  if ( StringUtil.Len( subGrid_Class) > 0 )
                  {
                     subGrid_Linesclass = subGrid_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGrid_Class) > 0 )
                  {
                     subGrid_Linesclass = subGrid_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtWWPFormTitle_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Title", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtWWPFormReferenceName_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Reference Name", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtWWPFormDate_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Date", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtWWPFormVersionNumber_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Form Version #", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtWWPFormLatestVersionNumber_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Latest Version", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+cmbavActiongroup_Class+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridContainer.AddObjectProperty("GridName", "Grid");
         }
         else
         {
            if ( isAjaxCallMode( ) )
            {
               GridContainer = new GXWebGrid( context);
            }
            else
            {
               GridContainer.Clear();
            }
            GridContainer.SetWrapped(nGXWrapped);
            GridContainer.AddObjectProperty("GridName", "Grid");
            GridContainer.AddObjectProperty("Header", subGrid_Header);
            GridContainer.AddObjectProperty("Class", "GridWithPaginationBar WorkWithSelection WorkWith");
            GridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Backcolorstyle), 1, 0, ".", "")));
            GridContainer.AddObjectProperty("Sortable", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Sortable), 1, 0, ".", "")));
            GridContainer.AddObjectProperty("CmpContext", sPrefix);
            GridContainer.AddObjectProperty("InMasterPage", "false");
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A11OrganisationId.ToString()));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A29LocationId.ToString()));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( (decimal)(A206WWPFormId), 4, 0, ".", ""))));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A209WWPFormTitle));
            GridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtWWPFormTitle_Link));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtWWPFormTitle_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A208WWPFormReferenceName));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtWWPFormReferenceName_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( context.localUtil.TToC( A231WWPFormDate, 10, 8, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " ")));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtWWPFormDate_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( (decimal)(A207WWPFormVersionNumber), 4, 0, ".", ""))));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtWWPFormVersionNumber_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( (decimal)(A219WWPFormLatestVersionNumber), 4, 0, ".", ""))));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtWWPFormLatestVersionNumber_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( (decimal)(AV10ActionGroup), 4, 0, ".", ""))));
            GridColumn.AddObjectProperty("Class", StringUtil.RTrim( cmbavActiongroup_Class));
            GridContainer.AddColumnProperties(GridColumn);
            GridContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Selectedindex), 4, 0, ".", "")));
            GridContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Allowselection), 1, 0, ".", "")));
            GridContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Selectioncolor), 9, 0, ".", "")));
            GridContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Allowhovering), 1, 0, ".", "")));
            GridContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Hoveringcolor), 9, 0, ".", "")));
            GridContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Allowcollapsing), 1, 0, ".", "")));
            GridContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         bttBtnuseractioninsert_Internalname = sPrefix+"BTNUSERACTIONINSERT";
         bttBtneditcolumns_Internalname = sPrefix+"BTNEDITCOLUMNS";
         bttBtnsubscriptions_Internalname = sPrefix+"BTNSUBSCRIPTIONS";
         divTableactions_Internalname = sPrefix+"TABLEACTIONS";
         Ddo_managefilters_Internalname = sPrefix+"DDO_MANAGEFILTERS";
         edtavFilterfulltext_Internalname = sPrefix+"vFILTERFULLTEXT";
         dynavOrganisationidfilter_Internalname = sPrefix+"vORGANISATIONIDFILTER";
         divOrganisationidfilter_cell_Internalname = sPrefix+"ORGANISATIONIDFILTER_CELL";
         dynavLocationidfilter_Internalname = sPrefix+"vLOCATIONIDFILTER";
         divLocationidfilter_cell_Internalname = sPrefix+"LOCATIONIDFILTER_CELL";
         divTablefilters_Internalname = sPrefix+"TABLEFILTERS";
         divTablerightheader_Internalname = sPrefix+"TABLERIGHTHEADER";
         divTableheadercontent_Internalname = sPrefix+"TABLEHEADERCONTENT";
         divTableheader_Internalname = sPrefix+"TABLEHEADER";
         edtOrganisationId_Internalname = sPrefix+"ORGANISATIONID";
         edtLocationId_Internalname = sPrefix+"LOCATIONID";
         edtWWPFormId_Internalname = sPrefix+"WWPFORMID";
         edtWWPFormTitle_Internalname = sPrefix+"WWPFORMTITLE";
         edtWWPFormReferenceName_Internalname = sPrefix+"WWPFORMREFERENCENAME";
         edtWWPFormDate_Internalname = sPrefix+"WWPFORMDATE";
         edtWWPFormVersionNumber_Internalname = sPrefix+"WWPFORMVERSIONNUMBER";
         edtWWPFormLatestVersionNumber_Internalname = sPrefix+"WWPFORMLATESTVERSIONNUMBER";
         cmbavActiongroup_Internalname = sPrefix+"vACTIONGROUP";
         Gridpaginationbar_Internalname = sPrefix+"GRIDPAGINATIONBAR";
         divGridtablewithpaginationbar_Internalname = sPrefix+"GRIDTABLEWITHPAGINATIONBAR";
         divTablegridheader_Internalname = sPrefix+"TABLEGRIDHEADER";
         Ddc_subscriptions_Internalname = sPrefix+"DDC_SUBSCRIPTIONS";
         Ddo_grid_Internalname = sPrefix+"DDO_GRID";
         cmbWWPFormType_Internalname = sPrefix+"WWPFORMTYPE";
         Ddo_gridcolumnsselector_Internalname = sPrefix+"DDO_GRIDCOLUMNSSELECTOR";
         Dvelop_confirmpanel_useractiondelete_Internalname = sPrefix+"DVELOP_CONFIRMPANEL_USERACTIONDELETE";
         tblTabledvelop_confirmpanel_useractiondelete_Internalname = sPrefix+"TABLEDVELOP_CONFIRMPANEL_USERACTIONDELETE";
         Grid_empowerer_Internalname = sPrefix+"GRID_EMPOWERER";
         divDiv_wwpauxwc_Internalname = sPrefix+"DIV_WWPAUXWC";
         edtavDdo_wwpformdateauxdatetext_Internalname = sPrefix+"vDDO_WWPFORMDATEAUXDATETEXT";
         Tfwwpformdate_rangepicker_Internalname = sPrefix+"TFWWPFORMDATE_RANGEPICKER";
         divDdo_wwpformdateauxdates_Internalname = sPrefix+"DDO_WWPFORMDATEAUXDATES";
         divHtml_bottomauxiliarcontrols_Internalname = sPrefix+"HTML_BOTTOMAUXILIARCONTROLS";
         divLayoutmaintable_Internalname = sPrefix+"LAYOUTMAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subGrid_Internalname = sPrefix+"GRID";
      }

      public override void initialize_properties( )
      {
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            context.SetDefaultTheme("WorkWithPlusDS", true);
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         init_default_properties( ) ;
         subGrid_Allowcollapsing = 0;
         subGrid_Allowhovering = -1;
         subGrid_Allowselection = 1;
         subGrid_Header = "";
         cmbavActiongroup_Jsonclick = "";
         cmbavActiongroup_Class = "ConvertToDDO";
         edtWWPFormLatestVersionNumber_Jsonclick = "";
         edtWWPFormVersionNumber_Jsonclick = "";
         edtWWPFormDate_Jsonclick = "";
         edtWWPFormReferenceName_Jsonclick = "";
         edtWWPFormTitle_Jsonclick = "";
         edtWWPFormTitle_Link = "";
         edtWWPFormId_Jsonclick = "";
         edtLocationId_Jsonclick = "";
         edtOrganisationId_Jsonclick = "";
         subGrid_Class = "GridWithPaginationBar WorkWithSelection WorkWith";
         subGrid_Backcolorstyle = 0;
         edtWWPFormLatestVersionNumber_Visible = -1;
         edtWWPFormVersionNumber_Visible = -1;
         edtWWPFormDate_Visible = -1;
         edtWWPFormReferenceName_Visible = -1;
         edtWWPFormTitle_Visible = -1;
         cmbWWPFormType.Enabled = 0;
         edtWWPFormLatestVersionNumber_Enabled = 0;
         edtWWPFormVersionNumber_Enabled = 0;
         edtWWPFormDate_Enabled = 0;
         edtWWPFormReferenceName_Enabled = 0;
         edtWWPFormTitle_Enabled = 0;
         edtWWPFormId_Enabled = 0;
         edtLocationId_Enabled = 0;
         edtOrganisationId_Enabled = 0;
         subGrid_Sortable = 0;
         edtavDdo_wwpformdateauxdatetext_Jsonclick = "";
         cmbWWPFormType_Jsonclick = "";
         cmbWWPFormType.Visible = 1;
         dynavLocationidfilter_Jsonclick = "";
         dynavLocationidfilter.Enabled = 1;
         dynavLocationidfilter.Visible = 1;
         divLocationidfilter_cell_Class = "";
         dynavOrganisationidfilter_Jsonclick = "";
         dynavOrganisationidfilter.Enabled = 1;
         dynavOrganisationidfilter.Visible = 1;
         divOrganisationidfilter_cell_Class = "";
         edtavFilterfulltext_Jsonclick = "";
         edtavFilterfulltext_Enabled = 1;
         bttBtnsubscriptions_Visible = 1;
         bttBtnuseractioninsert_Visible = 1;
         Grid_empowerer_Hascolumnsselector = Convert.ToBoolean( -1);
         Grid_empowerer_Hastitlesettings = Convert.ToBoolean( -1);
         Dvelop_confirmpanel_useractiondelete_Confirmtype = "1";
         Dvelop_confirmpanel_useractiondelete_Yesbuttonposition = "left";
         Dvelop_confirmpanel_useractiondelete_Cancelbuttoncaption = "WWP_ConfirmTextCancel";
         Dvelop_confirmpanel_useractiondelete_Nobuttoncaption = "WWP_ConfirmTextNo";
         Dvelop_confirmpanel_useractiondelete_Yesbuttoncaption = "WWP_ConfirmTextYes";
         Dvelop_confirmpanel_useractiondelete_Confirmationtext = "Are you sure you want to delete form";
         Dvelop_confirmpanel_useractiondelete_Title = context.GetMessage( "Delete form", "");
         Ddo_gridcolumnsselector_Titlecontrolidtoreplace = "";
         Ddo_gridcolumnsselector_Dropdownoptionstype = "GridColumnsSelector";
         Ddo_gridcolumnsselector_Cls = "ColumnsSelector hidden-xs";
         Ddo_gridcolumnsselector_Tooltip = "WWP_EditColumnsTooltip";
         Ddo_gridcolumnsselector_Caption = context.GetMessage( "WWP_EditColumnsCaption", "");
         Ddo_gridcolumnsselector_Icon = "fas fa-cog";
         Ddo_gridcolumnsselector_Icontype = "FontIcon";
         Ddo_grid_Format = "|||4.0|4.0";
         Ddo_grid_Datalistproc = "WP_DynamicFormTrn_LocationDynamicFormWCGetFilterData";
         Ddo_grid_Datalisttype = "Dynamic|Dynamic|||";
         Ddo_grid_Includedatalist = "T|T|||";
         Ddo_grid_Filterisrange = "||P|T|T";
         Ddo_grid_Filtertype = "Character|Character|Date|Numeric|Numeric";
         Ddo_grid_Includefilter = "T";
         Ddo_grid_Includesortasc = "T|T|T|T|";
         Ddo_grid_Columnssortvalues = "1|2|3|4|";
         Ddo_grid_Columnids = "3:WWPFormTitle|4:WWPFormReferenceName|5:WWPFormDate|6:WWPFormVersionNumber|7:WWPFormLatestVersionNumber";
         Ddo_grid_Gridinternalname = "";
         Ddc_subscriptions_Titlecontrolidtoreplace = "";
         Ddc_subscriptions_Cls = "ColumnsSelector";
         Ddc_subscriptions_Tooltip = "WWP_Subscriptions_Tooltip";
         Ddc_subscriptions_Caption = "";
         Ddc_subscriptions_Icon = "fas fa-rss";
         Ddc_subscriptions_Icontype = "FontIcon";
         Gridpaginationbar_Rowsperpagecaption = "WWP_PagingRowsPerPage";
         Gridpaginationbar_Emptygridcaption = "WWP_PagingEmptyGridCaption";
         Gridpaginationbar_Caption = context.GetMessage( "WWP_PagingCaption", "");
         Gridpaginationbar_Next = "WWP_PagingNextCaption";
         Gridpaginationbar_Previous = "WWP_PagingPreviousCaption";
         Gridpaginationbar_Rowsperpageoptions = "5:WWP_Rows5,10:WWP_Rows10,20:WWP_Rows20,50:WWP_Rows50";
         Gridpaginationbar_Rowsperpageselectedvalue = 10;
         Gridpaginationbar_Rowsperpageselector = Convert.ToBoolean( -1);
         Gridpaginationbar_Emptygridclass = "PaginationBarEmptyGrid";
         Gridpaginationbar_Pagingcaptionposition = "Left";
         Gridpaginationbar_Pagingbuttonsposition = "Right";
         Gridpaginationbar_Pagestoshow = 5;
         Gridpaginationbar_Showlast = Convert.ToBoolean( 0);
         Gridpaginationbar_Shownext = Convert.ToBoolean( -1);
         Gridpaginationbar_Showprevious = Convert.ToBoolean( -1);
         Gridpaginationbar_Showfirst = Convert.ToBoolean( 0);
         Gridpaginationbar_Class = "PaginationBar";
         Ddo_managefilters_Cls = "ManageFilters";
         Ddo_managefilters_Tooltip = "WWP_ManageFiltersTooltip";
         Ddo_managefilters_Icon = "fas fa-filter";
         Ddo_managefilters_Icontype = "FontIcon";
         subGrid_Rows = 0;
         context.GX_msglist.DisplayMode = 1;
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableJsOutput();
            }
         }
      }

      public void Validv_Organisationidfilter( )
      {
         AV53OrganisationIdFilter = StringUtil.StrToGuid( dynavOrganisationidfilter.CurrentValue);
         AV43LocationIdFilter = StringUtil.StrToGuid( dynavLocationidfilter.CurrentValue);
         GXVvLOCATIONIDFILTER_htmlBB2( AV53OrganisationIdFilter) ;
         dynload_actions( ) ;
         if ( dynavLocationidfilter.ItemCount > 0 )
         {
            AV43LocationIdFilter = StringUtil.StrToGuid( dynavLocationidfilter.getValidValue(AV43LocationIdFilter.ToString()));
         }
         if ( context.isAjaxRequest( ) )
         {
            dynavLocationidfilter.CurrentValue = AV43LocationIdFilter.ToString();
         }
         /*  Sending validation outputs */
         AssignAttri(sPrefix, false, "AV43LocationIdFilter", AV43LocationIdFilter.ToString());
         dynavLocationidfilter.CurrentValue = AV43LocationIdFilter.ToString();
         AssignProp(sPrefix, false, dynavLocationidfilter_Internalname, "Values", dynavLocationidfilter.ToJavascriptSource(), true);
      }

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"sPrefix"},{"av":"dynavLocationidfilter"},{"av":"AV43LocationIdFilter","fld":"vLOCATIONIDFILTER"},{"av":"AV45ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV11ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV50OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV52OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV22FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV9WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"AV73WWPContext","fld":"vWWPCONTEXT","hsh":true},{"av":"AV78Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV65TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV66TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV63TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV64TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV59TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV60TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV67TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV68TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV61TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV62TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV76WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV39IsAuthorized_UserActionEdit","fld":"vISAUTHORIZED_USERACTIONEDIT","hsh":true},{"av":"AV38IsAuthorized_UserActionDisplay","fld":"vISAUTHORIZED_USERACTIONDISPLAY","hsh":true},{"av":"AV36IsAuthorized_UserActionCopy","fld":"vISAUTHORIZED_USERACTIONCOPY","hsh":true},{"av":"AV37IsAuthorized_UserActionDelete","fld":"vISAUTHORIZED_USERACTIONDELETE","hsh":true},{"av":"AV40IsAuthorized_UserActionFilledForms","fld":"vISAUTHORIZED_USERACTIONFILLEDFORMS","hsh":true},{"av":"AV41IsAuthorized_UserActionFillOutForm","fld":"vISAUTHORIZED_USERACTIONFILLOUTFORM","hsh":true},{"av":"dynavOrganisationidfilter"},{"av":"AV53OrganisationIdFilter","fld":"vORGANISATIONIDFILTER"},{"av":"A366LocationDynamicFormId","fld":"LOCATIONDYNAMICFORMID","hsh":true}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV5LocationId","fld":"vLOCATIONID"},{"av":"AV73WWPContext","fld":"vWWPCONTEXT","hsh":true},{"av":"AV45ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV11ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPFormTitle_Visible","ctrl":"WWPFORMTITLE","prop":"Visible"},{"av":"edtWWPFormReferenceName_Visible","ctrl":"WWPFORMREFERENCENAME","prop":"Visible"},{"av":"edtWWPFormDate_Visible","ctrl":"WWPFORMDATE","prop":"Visible"},{"av":"edtWWPFormVersionNumber_Visible","ctrl":"WWPFORMVERSIONNUMBER","prop":"Visible"},{"av":"edtWWPFormLatestVersionNumber_Visible","ctrl":"WWPFORMLATESTVERSIONNUMBER","prop":"Visible"},{"av":"AV26GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV27GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV25GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV39IsAuthorized_UserActionEdit","fld":"vISAUTHORIZED_USERACTIONEDIT","hsh":true},{"av":"AV38IsAuthorized_UserActionDisplay","fld":"vISAUTHORIZED_USERACTIONDISPLAY","hsh":true},{"av":"AV36IsAuthorized_UserActionCopy","fld":"vISAUTHORIZED_USERACTIONCOPY","hsh":true},{"av":"AV37IsAuthorized_UserActionDelete","fld":"vISAUTHORIZED_USERACTIONDELETE","hsh":true},{"av":"AV40IsAuthorized_UserActionFilledForms","fld":"vISAUTHORIZED_USERACTIONFILLEDFORMS","hsh":true},{"av":"AV41IsAuthorized_UserActionFillOutForm","fld":"vISAUTHORIZED_USERACTIONFILLOUTFORM","hsh":true},{"ctrl":"BTNUSERACTIONINSERT","prop":"Visible"},{"ctrl":"BTNSUBSCRIPTIONS","prop":"Visible"},{"av":"AV44ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV28GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEPAGE","""{"handler":"E13BB2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV50OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV52OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV22FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV9WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"dynavLocationidfilter"},{"av":"AV43LocationIdFilter","fld":"vLOCATIONIDFILTER"},{"av":"AV73WWPContext","fld":"vWWPCONTEXT","hsh":true},{"av":"AV45ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV11ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV78Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV65TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV66TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV63TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV64TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV59TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV60TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV67TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV68TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV61TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV62TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV76WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV39IsAuthorized_UserActionEdit","fld":"vISAUTHORIZED_USERACTIONEDIT","hsh":true},{"av":"AV38IsAuthorized_UserActionDisplay","fld":"vISAUTHORIZED_USERACTIONDISPLAY","hsh":true},{"av":"AV36IsAuthorized_UserActionCopy","fld":"vISAUTHORIZED_USERACTIONCOPY","hsh":true},{"av":"AV37IsAuthorized_UserActionDelete","fld":"vISAUTHORIZED_USERACTIONDELETE","hsh":true},{"av":"AV40IsAuthorized_UserActionFilledForms","fld":"vISAUTHORIZED_USERACTIONFILLEDFORMS","hsh":true},{"av":"AV41IsAuthorized_UserActionFillOutForm","fld":"vISAUTHORIZED_USERACTIONFILLOUTFORM","hsh":true},{"av":"dynavOrganisationidfilter"},{"av":"AV53OrganisationIdFilter","fld":"vORGANISATIONIDFILTER"},{"av":"A366LocationDynamicFormId","fld":"LOCATIONDYNAMICFORMID","hsh":true},{"av":"sPrefix"},{"av":"Gridpaginationbar_Selectedpage","ctrl":"GRIDPAGINATIONBAR","prop":"SelectedPage"}]}""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEROWSPERPAGE","""{"handler":"E14BB2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV50OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV52OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV22FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV9WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"dynavLocationidfilter"},{"av":"AV43LocationIdFilter","fld":"vLOCATIONIDFILTER"},{"av":"AV73WWPContext","fld":"vWWPCONTEXT","hsh":true},{"av":"AV45ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV11ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV78Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV65TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV66TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV63TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV64TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV59TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV60TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV67TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV68TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV61TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV62TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV76WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV39IsAuthorized_UserActionEdit","fld":"vISAUTHORIZED_USERACTIONEDIT","hsh":true},{"av":"AV38IsAuthorized_UserActionDisplay","fld":"vISAUTHORIZED_USERACTIONDISPLAY","hsh":true},{"av":"AV36IsAuthorized_UserActionCopy","fld":"vISAUTHORIZED_USERACTIONCOPY","hsh":true},{"av":"AV37IsAuthorized_UserActionDelete","fld":"vISAUTHORIZED_USERACTIONDELETE","hsh":true},{"av":"AV40IsAuthorized_UserActionFilledForms","fld":"vISAUTHORIZED_USERACTIONFILLEDFORMS","hsh":true},{"av":"AV41IsAuthorized_UserActionFillOutForm","fld":"vISAUTHORIZED_USERACTIONFILLOUTFORM","hsh":true},{"av":"dynavOrganisationidfilter"},{"av":"AV53OrganisationIdFilter","fld":"vORGANISATIONIDFILTER"},{"av":"A366LocationDynamicFormId","fld":"LOCATIONDYNAMICFORMID","hsh":true},{"av":"sPrefix"},{"av":"Gridpaginationbar_Rowsperpageselectedvalue","ctrl":"GRIDPAGINATIONBAR","prop":"RowsPerPageSelectedValue"}]""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEROWSPERPAGE",""","oparms":[{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"}]}""");
         setEventMetadata("DDO_GRID.ONOPTIONCLICKED","""{"handler":"E15BB2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV50OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV52OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV22FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV9WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"dynavLocationidfilter"},{"av":"AV43LocationIdFilter","fld":"vLOCATIONIDFILTER"},{"av":"AV73WWPContext","fld":"vWWPCONTEXT","hsh":true},{"av":"AV45ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV11ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV78Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV65TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV66TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV63TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV64TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV59TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV60TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV67TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV68TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV61TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV62TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV76WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV39IsAuthorized_UserActionEdit","fld":"vISAUTHORIZED_USERACTIONEDIT","hsh":true},{"av":"AV38IsAuthorized_UserActionDisplay","fld":"vISAUTHORIZED_USERACTIONDISPLAY","hsh":true},{"av":"AV36IsAuthorized_UserActionCopy","fld":"vISAUTHORIZED_USERACTIONCOPY","hsh":true},{"av":"AV37IsAuthorized_UserActionDelete","fld":"vISAUTHORIZED_USERACTIONDELETE","hsh":true},{"av":"AV40IsAuthorized_UserActionFilledForms","fld":"vISAUTHORIZED_USERACTIONFILLEDFORMS","hsh":true},{"av":"AV41IsAuthorized_UserActionFillOutForm","fld":"vISAUTHORIZED_USERACTIONFILLOUTFORM","hsh":true},{"av":"dynavOrganisationidfilter"},{"av":"AV53OrganisationIdFilter","fld":"vORGANISATIONIDFILTER"},{"av":"A366LocationDynamicFormId","fld":"LOCATIONDYNAMICFORMID","hsh":true},{"av":"sPrefix"},{"av":"Ddo_grid_Activeeventkey","ctrl":"DDO_GRID","prop":"ActiveEventKey"},{"av":"Ddo_grid_Selectedvalue_get","ctrl":"DDO_GRID","prop":"SelectedValue_get"},{"av":"Ddo_grid_Selectedcolumn","ctrl":"DDO_GRID","prop":"SelectedColumn"},{"av":"Ddo_grid_Filteredtext_get","ctrl":"DDO_GRID","prop":"FilteredText_get"},{"av":"Ddo_grid_Filteredtextto_get","ctrl":"DDO_GRID","prop":"FilteredTextTo_get"}]""");
         setEventMetadata("DDO_GRID.ONOPTIONCLICKED",""","oparms":[{"av":"AV50OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV52OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV65TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV66TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV63TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV64TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV59TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV60TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV67TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV68TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV61TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV62TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"Ddo_grid_Sortedstatus","ctrl":"DDO_GRID","prop":"SortedStatus"}]}""");
         setEventMetadata("GRID.LOAD","""{"handler":"E21BB2","iparms":[{"av":"AV39IsAuthorized_UserActionEdit","fld":"vISAUTHORIZED_USERACTIONEDIT","hsh":true},{"av":"AV38IsAuthorized_UserActionDisplay","fld":"vISAUTHORIZED_USERACTIONDISPLAY","hsh":true},{"av":"AV36IsAuthorized_UserActionCopy","fld":"vISAUTHORIZED_USERACTIONCOPY","hsh":true},{"av":"AV37IsAuthorized_UserActionDelete","fld":"vISAUTHORIZED_USERACTIONDELETE","hsh":true},{"av":"AV40IsAuthorized_UserActionFilledForms","fld":"vISAUTHORIZED_USERACTIONFILLEDFORMS","hsh":true},{"av":"AV41IsAuthorized_UserActionFillOutForm","fld":"vISAUTHORIZED_USERACTIONFILLOUTFORM","hsh":true},{"av":"A206WWPFormId","fld":"WWPFORMID","pic":"ZZZ9","hsh":true},{"av":"cmbWWPFormType"},{"av":"A240WWPFormType","fld":"WWPFORMTYPE","pic":"9"}]""");
         setEventMetadata("GRID.LOAD",""","oparms":[{"av":"cmbavActiongroup"},{"av":"AV10ActionGroup","fld":"vACTIONGROUP","pic":"ZZZ9"},{"av":"edtWWPFormTitle_Link","ctrl":"WWPFORMTITLE","prop":"Link"}]}""");
         setEventMetadata("DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED","""{"handler":"E16BB2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV50OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV52OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV22FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV9WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"dynavLocationidfilter"},{"av":"AV43LocationIdFilter","fld":"vLOCATIONIDFILTER"},{"av":"AV73WWPContext","fld":"vWWPCONTEXT","hsh":true},{"av":"AV45ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV11ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV78Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV65TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV66TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV63TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV64TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV59TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV60TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV67TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV68TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV61TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV62TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV76WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV39IsAuthorized_UserActionEdit","fld":"vISAUTHORIZED_USERACTIONEDIT","hsh":true},{"av":"AV38IsAuthorized_UserActionDisplay","fld":"vISAUTHORIZED_USERACTIONDISPLAY","hsh":true},{"av":"AV36IsAuthorized_UserActionCopy","fld":"vISAUTHORIZED_USERACTIONCOPY","hsh":true},{"av":"AV37IsAuthorized_UserActionDelete","fld":"vISAUTHORIZED_USERACTIONDELETE","hsh":true},{"av":"AV40IsAuthorized_UserActionFilledForms","fld":"vISAUTHORIZED_USERACTIONFILLEDFORMS","hsh":true},{"av":"AV41IsAuthorized_UserActionFillOutForm","fld":"vISAUTHORIZED_USERACTIONFILLOUTFORM","hsh":true},{"av":"dynavOrganisationidfilter"},{"av":"AV53OrganisationIdFilter","fld":"vORGANISATIONIDFILTER"},{"av":"A366LocationDynamicFormId","fld":"LOCATIONDYNAMICFORMID","hsh":true},{"av":"sPrefix"},{"av":"Ddo_gridcolumnsselector_Columnsselectorvalues","ctrl":"DDO_GRIDCOLUMNSSELECTOR","prop":"ColumnsSelectorValues"}]""");
         setEventMetadata("DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED",""","oparms":[{"av":"AV11ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV5LocationId","fld":"vLOCATIONID"},{"av":"AV73WWPContext","fld":"vWWPCONTEXT","hsh":true},{"av":"AV45ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"edtWWPFormTitle_Visible","ctrl":"WWPFORMTITLE","prop":"Visible"},{"av":"edtWWPFormReferenceName_Visible","ctrl":"WWPFORMREFERENCENAME","prop":"Visible"},{"av":"edtWWPFormDate_Visible","ctrl":"WWPFORMDATE","prop":"Visible"},{"av":"edtWWPFormVersionNumber_Visible","ctrl":"WWPFORMVERSIONNUMBER","prop":"Visible"},{"av":"edtWWPFormLatestVersionNumber_Visible","ctrl":"WWPFORMLATESTVERSIONNUMBER","prop":"Visible"},{"av":"AV26GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV27GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV25GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV39IsAuthorized_UserActionEdit","fld":"vISAUTHORIZED_USERACTIONEDIT","hsh":true},{"av":"AV38IsAuthorized_UserActionDisplay","fld":"vISAUTHORIZED_USERACTIONDISPLAY","hsh":true},{"av":"AV36IsAuthorized_UserActionCopy","fld":"vISAUTHORIZED_USERACTIONCOPY","hsh":true},{"av":"AV37IsAuthorized_UserActionDelete","fld":"vISAUTHORIZED_USERACTIONDELETE","hsh":true},{"av":"AV40IsAuthorized_UserActionFilledForms","fld":"vISAUTHORIZED_USERACTIONFILLEDFORMS","hsh":true},{"av":"AV41IsAuthorized_UserActionFillOutForm","fld":"vISAUTHORIZED_USERACTIONFILLOUTFORM","hsh":true},{"ctrl":"BTNUSERACTIONINSERT","prop":"Visible"},{"ctrl":"BTNSUBSCRIPTIONS","prop":"Visible"},{"av":"AV44ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV28GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("DDO_MANAGEFILTERS.ONOPTIONCLICKED","""{"handler":"E12BB2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV50OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV52OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV22FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV9WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"dynavLocationidfilter"},{"av":"AV43LocationIdFilter","fld":"vLOCATIONIDFILTER"},{"av":"AV73WWPContext","fld":"vWWPCONTEXT","hsh":true},{"av":"AV45ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV11ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV78Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV65TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV66TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV63TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV64TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV59TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV60TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV67TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV68TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV61TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV62TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV76WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV39IsAuthorized_UserActionEdit","fld":"vISAUTHORIZED_USERACTIONEDIT","hsh":true},{"av":"AV38IsAuthorized_UserActionDisplay","fld":"vISAUTHORIZED_USERACTIONDISPLAY","hsh":true},{"av":"AV36IsAuthorized_UserActionCopy","fld":"vISAUTHORIZED_USERACTIONCOPY","hsh":true},{"av":"AV37IsAuthorized_UserActionDelete","fld":"vISAUTHORIZED_USERACTIONDELETE","hsh":true},{"av":"AV40IsAuthorized_UserActionFilledForms","fld":"vISAUTHORIZED_USERACTIONFILLEDFORMS","hsh":true},{"av":"AV41IsAuthorized_UserActionFillOutForm","fld":"vISAUTHORIZED_USERACTIONFILLOUTFORM","hsh":true},{"av":"dynavOrganisationidfilter"},{"av":"AV53OrganisationIdFilter","fld":"vORGANISATIONIDFILTER"},{"av":"A366LocationDynamicFormId","fld":"LOCATIONDYNAMICFORMID","hsh":true},{"av":"sPrefix"},{"av":"Ddo_managefilters_Activeeventkey","ctrl":"DDO_MANAGEFILTERS","prop":"ActiveEventKey"},{"av":"AV28GridState","fld":"vGRIDSTATE"},{"av":"AV16DDO_WWPFormDateAuxDate","fld":"vDDO_WWPFORMDATEAUXDATE"},{"av":"AV18DDO_WWPFormDateAuxDateTo","fld":"vDDO_WWPFORMDATEAUXDATETO"}]""");
         setEventMetadata("DDO_MANAGEFILTERS.ONOPTIONCLICKED",""","oparms":[{"av":"AV45ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV28GridState","fld":"vGRIDSTATE"},{"av":"AV50OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV52OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV22FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV65TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV66TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV63TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV64TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV59TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV60TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV67TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV68TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV61TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV62TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"Ddo_grid_Selectedvalue_set","ctrl":"DDO_GRID","prop":"SelectedValue_set"},{"av":"Ddo_grid_Filteredtext_set","ctrl":"DDO_GRID","prop":"FilteredText_set"},{"av":"Ddo_grid_Filteredtextto_set","ctrl":"DDO_GRID","prop":"FilteredTextTo_set"},{"av":"Ddo_grid_Sortedstatus","ctrl":"DDO_GRID","prop":"SortedStatus"},{"av":"AV16DDO_WWPFormDateAuxDate","fld":"vDDO_WWPFORMDATEAUXDATE"},{"av":"AV18DDO_WWPFormDateAuxDateTo","fld":"vDDO_WWPFORMDATEAUXDATETO"},{"av":"AV5LocationId","fld":"vLOCATIONID"},{"av":"AV73WWPContext","fld":"vWWPCONTEXT","hsh":true},{"av":"AV11ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPFormTitle_Visible","ctrl":"WWPFORMTITLE","prop":"Visible"},{"av":"edtWWPFormReferenceName_Visible","ctrl":"WWPFORMREFERENCENAME","prop":"Visible"},{"av":"edtWWPFormDate_Visible","ctrl":"WWPFORMDATE","prop":"Visible"},{"av":"edtWWPFormVersionNumber_Visible","ctrl":"WWPFORMVERSIONNUMBER","prop":"Visible"},{"av":"edtWWPFormLatestVersionNumber_Visible","ctrl":"WWPFORMLATESTVERSIONNUMBER","prop":"Visible"},{"av":"AV26GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV27GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV25GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV39IsAuthorized_UserActionEdit","fld":"vISAUTHORIZED_USERACTIONEDIT","hsh":true},{"av":"AV38IsAuthorized_UserActionDisplay","fld":"vISAUTHORIZED_USERACTIONDISPLAY","hsh":true},{"av":"AV36IsAuthorized_UserActionCopy","fld":"vISAUTHORIZED_USERACTIONCOPY","hsh":true},{"av":"AV37IsAuthorized_UserActionDelete","fld":"vISAUTHORIZED_USERACTIONDELETE","hsh":true},{"av":"AV40IsAuthorized_UserActionFilledForms","fld":"vISAUTHORIZED_USERACTIONFILLEDFORMS","hsh":true},{"av":"AV41IsAuthorized_UserActionFillOutForm","fld":"vISAUTHORIZED_USERACTIONFILLOUTFORM","hsh":true},{"ctrl":"BTNUSERACTIONINSERT","prop":"Visible"},{"ctrl":"BTNSUBSCRIPTIONS","prop":"Visible"},{"av":"AV44ManageFiltersData","fld":"vMANAGEFILTERSDATA"}]}""");
         setEventMetadata("VACTIONGROUP.CLICK","""{"handler":"E22BB2","iparms":[{"av":"cmbavActiongroup"},{"av":"AV10ActionGroup","fld":"vACTIONGROUP","pic":"ZZZ9"},{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV50OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV52OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV22FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV9WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"dynavLocationidfilter"},{"av":"AV43LocationIdFilter","fld":"vLOCATIONIDFILTER"},{"av":"AV73WWPContext","fld":"vWWPCONTEXT","hsh":true},{"av":"AV45ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV11ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV78Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV65TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV66TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV63TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV64TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV59TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV60TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV67TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV68TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV61TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV62TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV76WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV39IsAuthorized_UserActionEdit","fld":"vISAUTHORIZED_USERACTIONEDIT","hsh":true},{"av":"AV38IsAuthorized_UserActionDisplay","fld":"vISAUTHORIZED_USERACTIONDISPLAY","hsh":true},{"av":"AV36IsAuthorized_UserActionCopy","fld":"vISAUTHORIZED_USERACTIONCOPY","hsh":true},{"av":"AV37IsAuthorized_UserActionDelete","fld":"vISAUTHORIZED_USERACTIONDELETE","hsh":true},{"av":"AV40IsAuthorized_UserActionFilledForms","fld":"vISAUTHORIZED_USERACTIONFILLEDFORMS","hsh":true},{"av":"AV41IsAuthorized_UserActionFillOutForm","fld":"vISAUTHORIZED_USERACTIONFILLOUTFORM","hsh":true},{"av":"dynavOrganisationidfilter"},{"av":"AV53OrganisationIdFilter","fld":"vORGANISATIONIDFILTER"},{"av":"A366LocationDynamicFormId","fld":"LOCATIONDYNAMICFORMID","hsh":true},{"av":"sPrefix"},{"av":"A206WWPFormId","fld":"WWPFORMID","pic":"ZZZ9","hsh":true},{"av":"cmbWWPFormType"},{"av":"A240WWPFormType","fld":"WWPFORMTYPE","pic":"9"},{"av":"A207WWPFormVersionNumber","fld":"WWPFORMVERSIONNUMBER","pic":"ZZZ9","hsh":true},{"av":"A208WWPFormReferenceName","fld":"WWPFORMREFERENCENAME"},{"av":"AV5LocationId","fld":"vLOCATIONID"},{"av":"AV6OrganisationId","fld":"vORGANISATIONID"},{"av":"AV56ResultMsg","fld":"vRESULTMSG"},{"av":"A11OrganisationId","fld":"ORGANISATIONID","hsh":true},{"av":"A29LocationId","fld":"LOCATIONID","hsh":true},{"av":"A209WWPFormTitle","fld":"WWPFORMTITLE"}]""");
         setEventMetadata("VACTIONGROUP.CLICK",""","oparms":[{"av":"cmbavActiongroup"},{"av":"AV10ActionGroup","fld":"vACTIONGROUP","pic":"ZZZ9"},{"av":"AV56ResultMsg","fld":"vRESULTMSG"},{"av":"AV5LocationId","fld":"vLOCATIONID"},{"av":"AV73WWPContext","fld":"vWWPCONTEXT","hsh":true},{"av":"AV45ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV11ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPFormTitle_Visible","ctrl":"WWPFORMTITLE","prop":"Visible"},{"av":"edtWWPFormReferenceName_Visible","ctrl":"WWPFORMREFERENCENAME","prop":"Visible"},{"av":"edtWWPFormDate_Visible","ctrl":"WWPFORMDATE","prop":"Visible"},{"av":"edtWWPFormVersionNumber_Visible","ctrl":"WWPFORMVERSIONNUMBER","prop":"Visible"},{"av":"edtWWPFormLatestVersionNumber_Visible","ctrl":"WWPFORMLATESTVERSIONNUMBER","prop":"Visible"},{"av":"AV26GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV27GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV25GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV39IsAuthorized_UserActionEdit","fld":"vISAUTHORIZED_USERACTIONEDIT","hsh":true},{"av":"AV38IsAuthorized_UserActionDisplay","fld":"vISAUTHORIZED_USERACTIONDISPLAY","hsh":true},{"av":"AV36IsAuthorized_UserActionCopy","fld":"vISAUTHORIZED_USERACTIONCOPY","hsh":true},{"av":"AV37IsAuthorized_UserActionDelete","fld":"vISAUTHORIZED_USERACTIONDELETE","hsh":true},{"av":"AV40IsAuthorized_UserActionFilledForms","fld":"vISAUTHORIZED_USERACTIONFILLEDFORMS","hsh":true},{"av":"AV41IsAuthorized_UserActionFillOutForm","fld":"vISAUTHORIZED_USERACTIONFILLOUTFORM","hsh":true},{"ctrl":"BTNUSERACTIONINSERT","prop":"Visible"},{"ctrl":"BTNSUBSCRIPTIONS","prop":"Visible"},{"av":"AV44ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV28GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("DVELOP_CONFIRMPANEL_USERACTIONDELETE.CLOSE","""{"handler":"E17BB2","iparms":[{"av":"Dvelop_confirmpanel_useractiondelete_Result","ctrl":"DVELOP_CONFIRMPANEL_USERACTIONDELETE","prop":"Result"},{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV50OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV52OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV22FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV9WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"dynavLocationidfilter"},{"av":"AV43LocationIdFilter","fld":"vLOCATIONIDFILTER"},{"av":"AV73WWPContext","fld":"vWWPCONTEXT","hsh":true},{"av":"AV45ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV11ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV78Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV65TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV66TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV63TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV64TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV59TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV60TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV67TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV68TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV61TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV62TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV76WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV39IsAuthorized_UserActionEdit","fld":"vISAUTHORIZED_USERACTIONEDIT","hsh":true},{"av":"AV38IsAuthorized_UserActionDisplay","fld":"vISAUTHORIZED_USERACTIONDISPLAY","hsh":true},{"av":"AV36IsAuthorized_UserActionCopy","fld":"vISAUTHORIZED_USERACTIONCOPY","hsh":true},{"av":"AV37IsAuthorized_UserActionDelete","fld":"vISAUTHORIZED_USERACTIONDELETE","hsh":true},{"av":"AV40IsAuthorized_UserActionFilledForms","fld":"vISAUTHORIZED_USERACTIONFILLEDFORMS","hsh":true},{"av":"AV41IsAuthorized_UserActionFillOutForm","fld":"vISAUTHORIZED_USERACTIONFILLOUTFORM","hsh":true},{"av":"dynavOrganisationidfilter"},{"av":"AV53OrganisationIdFilter","fld":"vORGANISATIONIDFILTER"},{"av":"A366LocationDynamicFormId","fld":"LOCATIONDYNAMICFORMID","hsh":true},{"av":"sPrefix"},{"av":"A206WWPFormId","fld":"WWPFORMID","pic":"ZZZ9","hsh":true},{"av":"A207WWPFormVersionNumber","fld":"WWPFORMVERSIONNUMBER","pic":"ZZZ9","hsh":true},{"av":"A11OrganisationId","fld":"ORGANISATIONID","hsh":true},{"av":"A29LocationId","fld":"LOCATIONID","hsh":true}]""");
         setEventMetadata("DVELOP_CONFIRMPANEL_USERACTIONDELETE.CLOSE",""","oparms":[{"av":"AV5LocationId","fld":"vLOCATIONID"},{"av":"AV73WWPContext","fld":"vWWPCONTEXT","hsh":true},{"av":"AV45ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV11ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPFormTitle_Visible","ctrl":"WWPFORMTITLE","prop":"Visible"},{"av":"edtWWPFormReferenceName_Visible","ctrl":"WWPFORMREFERENCENAME","prop":"Visible"},{"av":"edtWWPFormDate_Visible","ctrl":"WWPFORMDATE","prop":"Visible"},{"av":"edtWWPFormVersionNumber_Visible","ctrl":"WWPFORMVERSIONNUMBER","prop":"Visible"},{"av":"edtWWPFormLatestVersionNumber_Visible","ctrl":"WWPFORMLATESTVERSIONNUMBER","prop":"Visible"},{"av":"AV26GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV27GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV25GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV39IsAuthorized_UserActionEdit","fld":"vISAUTHORIZED_USERACTIONEDIT","hsh":true},{"av":"AV38IsAuthorized_UserActionDisplay","fld":"vISAUTHORIZED_USERACTIONDISPLAY","hsh":true},{"av":"AV36IsAuthorized_UserActionCopy","fld":"vISAUTHORIZED_USERACTIONCOPY","hsh":true},{"av":"AV37IsAuthorized_UserActionDelete","fld":"vISAUTHORIZED_USERACTIONDELETE","hsh":true},{"av":"AV40IsAuthorized_UserActionFilledForms","fld":"vISAUTHORIZED_USERACTIONFILLEDFORMS","hsh":true},{"av":"AV41IsAuthorized_UserActionFillOutForm","fld":"vISAUTHORIZED_USERACTIONFILLOUTFORM","hsh":true},{"ctrl":"BTNUSERACTIONINSERT","prop":"Visible"},{"ctrl":"BTNSUBSCRIPTIONS","prop":"Visible"},{"av":"AV44ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV28GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("'DOUSERACTIONINSERT'","""{"handler":"E11BB1","iparms":[{"av":"AV9WWPFormType","fld":"vWWPFORMTYPE","pic":"9"}]}""");
         setEventMetadata("VLOCATIONIDFILTER.CONTROLVALUECHANGED","""{"handler":"E18BB2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV50OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV52OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV22FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV9WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"dynavLocationidfilter"},{"av":"AV43LocationIdFilter","fld":"vLOCATIONIDFILTER"},{"av":"AV73WWPContext","fld":"vWWPCONTEXT","hsh":true},{"av":"AV45ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV11ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV78Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV65TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV66TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV63TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV64TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV59TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV60TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV67TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV68TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV61TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV62TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV76WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV39IsAuthorized_UserActionEdit","fld":"vISAUTHORIZED_USERACTIONEDIT","hsh":true},{"av":"AV38IsAuthorized_UserActionDisplay","fld":"vISAUTHORIZED_USERACTIONDISPLAY","hsh":true},{"av":"AV36IsAuthorized_UserActionCopy","fld":"vISAUTHORIZED_USERACTIONCOPY","hsh":true},{"av":"AV37IsAuthorized_UserActionDelete","fld":"vISAUTHORIZED_USERACTIONDELETE","hsh":true},{"av":"AV40IsAuthorized_UserActionFilledForms","fld":"vISAUTHORIZED_USERACTIONFILLEDFORMS","hsh":true},{"av":"AV41IsAuthorized_UserActionFillOutForm","fld":"vISAUTHORIZED_USERACTIONFILLOUTFORM","hsh":true},{"av":"dynavOrganisationidfilter"},{"av":"AV53OrganisationIdFilter","fld":"vORGANISATIONIDFILTER"},{"av":"A366LocationDynamicFormId","fld":"LOCATIONDYNAMICFORMID","hsh":true},{"av":"sPrefix"}]""");
         setEventMetadata("VLOCATIONIDFILTER.CONTROLVALUECHANGED",""","oparms":[{"av":"AV5LocationId","fld":"vLOCATIONID"},{"av":"AV73WWPContext","fld":"vWWPCONTEXT","hsh":true},{"av":"AV45ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV11ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPFormTitle_Visible","ctrl":"WWPFORMTITLE","prop":"Visible"},{"av":"edtWWPFormReferenceName_Visible","ctrl":"WWPFORMREFERENCENAME","prop":"Visible"},{"av":"edtWWPFormDate_Visible","ctrl":"WWPFORMDATE","prop":"Visible"},{"av":"edtWWPFormVersionNumber_Visible","ctrl":"WWPFORMVERSIONNUMBER","prop":"Visible"},{"av":"edtWWPFormLatestVersionNumber_Visible","ctrl":"WWPFORMLATESTVERSIONNUMBER","prop":"Visible"},{"av":"AV26GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV27GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV25GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV39IsAuthorized_UserActionEdit","fld":"vISAUTHORIZED_USERACTIONEDIT","hsh":true},{"av":"AV38IsAuthorized_UserActionDisplay","fld":"vISAUTHORIZED_USERACTIONDISPLAY","hsh":true},{"av":"AV36IsAuthorized_UserActionCopy","fld":"vISAUTHORIZED_USERACTIONCOPY","hsh":true},{"av":"AV37IsAuthorized_UserActionDelete","fld":"vISAUTHORIZED_USERACTIONDELETE","hsh":true},{"av":"AV40IsAuthorized_UserActionFilledForms","fld":"vISAUTHORIZED_USERACTIONFILLEDFORMS","hsh":true},{"av":"AV41IsAuthorized_UserActionFillOutForm","fld":"vISAUTHORIZED_USERACTIONFILLOUTFORM","hsh":true},{"ctrl":"BTNUSERACTIONINSERT","prop":"Visible"},{"ctrl":"BTNSUBSCRIPTIONS","prop":"Visible"},{"av":"AV44ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV28GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("VALIDV_ORGANISATIONIDFILTER","""{"handler":"Validv_Organisationidfilter","iparms":[{"av":"dynavOrganisationidfilter"},{"av":"AV53OrganisationIdFilter","fld":"vORGANISATIONIDFILTER"},{"av":"dynavLocationidfilter"},{"av":"AV43LocationIdFilter","fld":"vLOCATIONIDFILTER"}]""");
         setEventMetadata("VALIDV_ORGANISATIONIDFILTER",""","oparms":[{"av":"dynavLocationidfilter"},{"av":"AV43LocationIdFilter","fld":"vLOCATIONIDFILTER"}]}""");
         setEventMetadata("VALIDV_LOCATIONIDFILTER","""{"handler":"Validv_Locationidfilter","iparms":[]}""");
         setEventMetadata("VALID_WWPFORMID","""{"handler":"Valid_Wwpformid","iparms":[]}""");
         setEventMetadata("VALID_WWPFORMTITLE","""{"handler":"Valid_Wwpformtitle","iparms":[]}""");
         setEventMetadata("VALID_WWPFORMREFERENCENAME","""{"handler":"Valid_Wwpformreferencename","iparms":[]}""");
         setEventMetadata("VALID_WWPFORMVERSIONNUMBER","""{"handler":"Valid_Wwpformversionnumber","iparms":[]}""");
         setEventMetadata("VALID_WWPFORMLATESTVERSIONNUMBER","""{"handler":"Valid_Wwpformlatestversionnumber","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Actiongroup","iparms":[]}""");
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

      public override void initialize( )
      {
         Gridpaginationbar_Selectedpage = "";
         Ddo_grid_Activeeventkey = "";
         Ddo_grid_Selectedvalue_get = "";
         Ddo_grid_Selectedcolumn = "";
         Ddo_grid_Filteredtext_get = "";
         Ddo_grid_Filteredtextto_get = "";
         Ddo_gridcolumnsselector_Columnsselectorvalues = "";
         Ddo_managefilters_Activeeventkey = "";
         Dvelop_confirmpanel_useractiondelete_Result = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         AV53OrganisationIdFilter = Guid.Empty;
         AV22FilterFullText = "";
         AV43LocationIdFilter = Guid.Empty;
         AV73WWPContext = new GeneXus.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV11ColumnsSelector = new WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector(context);
         AV78Pgmname = "";
         AV65TFWWPFormTitle = "";
         AV66TFWWPFormTitle_Sel = "";
         AV63TFWWPFormReferenceName = "";
         AV64TFWWPFormReferenceName_Sel = "";
         AV59TFWWPFormDate = (DateTime)(DateTime.MinValue);
         AV60TFWWPFormDate_To = (DateTime)(DateTime.MinValue);
         A366LocationDynamicFormId = Guid.Empty;
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV44ManageFiltersData = new GXBaseCollection<WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV25GridAppliedFilters = "";
         AV15DDO_TitleSettingsIcons = new WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV16DDO_WWPFormDateAuxDate = DateTime.MinValue;
         AV18DDO_WWPFormDateAuxDateTo = DateTime.MinValue;
         AV28GridState = new WorkWithPlus.workwithplus_web.SdtWWPGridState(context);
         AV5LocationId = Guid.Empty;
         AV6OrganisationId = Guid.Empty;
         AV56ResultMsg = "";
         Ddo_grid_Caption = "";
         Ddo_grid_Filteredtext_set = "";
         Ddo_grid_Filteredtextto_set = "";
         Ddo_grid_Selectedvalue_set = "";
         Ddo_grid_Gamoauthtoken = "";
         Ddo_grid_Sortedstatus = "";
         Ddo_gridcolumnsselector_Gridinternalname = "";
         Grid_empowerer_Gridinternalname = "";
         GX_FocusControl = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttBtnuseractioninsert_Jsonclick = "";
         bttBtneditcolumns_Jsonclick = "";
         bttBtnsubscriptions_Jsonclick = "";
         ucDdo_managefilters = new GXUserControl();
         Ddo_managefilters_Caption = "";
         GridContainer = new GXWebGrid( context);
         sStyleString = "";
         ucGridpaginationbar = new GXUserControl();
         ucDdc_subscriptions = new GXUserControl();
         ucDdo_grid = new GXUserControl();
         ucDdo_gridcolumnsselector = new GXUserControl();
         ucGrid_empowerer = new GXUserControl();
         WebComp_Wwpaux_wc_Component = "";
         OldWwpaux_wc = "";
         AV17DDO_WWPFormDateAuxDateText = "";
         ucTfwwpformdate_rangepicker = new GXUserControl();
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         A11OrganisationId = Guid.Empty;
         A29LocationId = Guid.Empty;
         A209WWPFormTitle = "";
         A208WWPFormReferenceName = "";
         A231WWPFormDate = (DateTime)(DateTime.MinValue);
         gxdynajaxctrlcodr = new GeneXus.Utils.GxStringCollection();
         gxdynajaxctrldescr = new GeneXus.Utils.GxStringCollection();
         gxwrpcisep = "";
         H00BB2_A11OrganisationId = new Guid[] {Guid.Empty} ;
         H00BB2_A13OrganisationName = new string[] {""} ;
         H00BB3_A29LocationId = new Guid[] {Guid.Empty} ;
         H00BB3_A31LocationName = new string[] {""} ;
         H00BB3_A11OrganisationId = new Guid[] {Guid.Empty} ;
         lV65TFWWPFormTitle = "";
         lV63TFWWPFormReferenceName = "";
         H00BB4_A366LocationDynamicFormId = new Guid[] {Guid.Empty} ;
         H00BB4_A240WWPFormType = new short[1] ;
         H00BB4_A207WWPFormVersionNumber = new short[1] ;
         H00BB4_A231WWPFormDate = new DateTime[] {DateTime.MinValue} ;
         H00BB4_A208WWPFormReferenceName = new string[] {""} ;
         H00BB4_A209WWPFormTitle = new string[] {""} ;
         H00BB4_A29LocationId = new Guid[] {Guid.Empty} ;
         H00BB4_A11OrganisationId = new Guid[] {Guid.Empty} ;
         H00BB4_A206WWPFormId = new short[1] ;
         H00BB5_A366LocationDynamicFormId = new Guid[] {Guid.Empty} ;
         H00BB5_A240WWPFormType = new short[1] ;
         H00BB5_A207WWPFormVersionNumber = new short[1] ;
         H00BB5_A231WWPFormDate = new DateTime[] {DateTime.MinValue} ;
         H00BB5_A208WWPFormReferenceName = new string[] {""} ;
         H00BB5_A209WWPFormTitle = new string[] {""} ;
         H00BB5_A29LocationId = new Guid[] {Guid.Empty} ;
         H00BB5_A11OrganisationId = new Guid[] {Guid.Empty} ;
         H00BB5_A206WWPFormId = new short[1] ;
         AV24GAMSession = new GeneXus.Programs.genexussecurity.SdtGAMSession(context);
         AV23GAMErrors = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "GeneXus.Programs");
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons2 = new WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV58Session = context.GetSession();
         AV13ColumnsSelectorXML = "";
         GridRow = new GXWebRow();
         AV46ManageFiltersXml = "";
         AV72UserCustomValue = "";
         AV12ColumnsSelectorAux = new WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector(context);
         GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item5 = new GXBaseCollection<WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV74WWPForm = new SdtUForm(context);
         AV8WWPFormReferenceName = "";
         AV49NewWWPForm = new SdtUForm(context);
         H00BB6_A207WWPFormVersionNumber = new short[1] ;
         H00BB6_A206WWPFormId = new short[1] ;
         AV21Element = new SdtUForm_Element(context);
         AV7Trn_LocationDynamicForm = new SdtTrn_LocationDynamicForm(context);
         AV81GXV2 = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         AV47Message = new GeneXus.Utils.SdtMessages_Message(context);
         AV83GXV4 = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         AV85Locationdynamicformid_selected = Guid.Empty;
         AV86Organisationid_selected = Guid.Empty;
         AV87Locationid_selected = Guid.Empty;
         AV48Messages = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         AV29GridStateFilterValue = new WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue(context);
         GXt_char6 = "";
         GXt_char3 = "";
         AV69TrnContext = new WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext(context);
         AV30HTTPRequest = new GxHttpRequest( context);
         AV70TrnContextAtt = new WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext_Attribute(context);
         ucDvelop_confirmpanel_useractiondelete = new GXUserControl();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlAV9WWPFormType = "";
         sCtrlAV76WWPFormIsForDynamicValidations = "";
         subGrid_Linesclass = "";
         ROClassString = "";
         GXCCtl = "";
         GridColumn = new GXWebColumn();
         ZV43LocationIdFilter = Guid.Empty;
         pr_datastore1 = new DataStoreProvider(context, new GeneXus.Programs.wp_dynamicformtrn_locationdynamicformwc__datastore1(),
            new Object[][] {
            }
         );
         pr_gam = new DataStoreProvider(context, new GeneXus.Programs.wp_dynamicformtrn_locationdynamicformwc__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.wp_dynamicformtrn_locationdynamicformwc__default(),
            new Object[][] {
                new Object[] {
               H00BB2_A11OrganisationId, H00BB2_A13OrganisationName
               }
               , new Object[] {
               H00BB3_A29LocationId, H00BB3_A31LocationName, H00BB3_A11OrganisationId
               }
               , new Object[] {
               H00BB4_A366LocationDynamicFormId, H00BB4_A240WWPFormType, H00BB4_A207WWPFormVersionNumber, H00BB4_A231WWPFormDate, H00BB4_A208WWPFormReferenceName, H00BB4_A209WWPFormTitle, H00BB4_A29LocationId, H00BB4_A11OrganisationId, H00BB4_A206WWPFormId
               }
               , new Object[] {
               H00BB5_A366LocationDynamicFormId, H00BB5_A240WWPFormType, H00BB5_A207WWPFormVersionNumber, H00BB5_A231WWPFormDate, H00BB5_A208WWPFormReferenceName, H00BB5_A209WWPFormTitle, H00BB5_A29LocationId, H00BB5_A11OrganisationId, H00BB5_A206WWPFormId
               }
               , new Object[] {
               H00BB6_A207WWPFormVersionNumber, H00BB6_A206WWPFormId
               }
            }
         );
         WebComp_Wwpaux_wc = new GeneXus.Http.GXNullWebComponent();
         AV78Pgmname = "WP_DynamicFormTrn_LocationDynamicFormWC";
         /* GeneXus formulas. */
         AV78Pgmname = "WP_DynamicFormTrn_LocationDynamicFormWC";
      }

      private short AV9WWPFormType ;
      private short wcpOAV9WWPFormType ;
      private short GRID_nEOF ;
      private short nRcdExists_3 ;
      private short nIsMod_3 ;
      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short AV50OrderedBy ;
      private short AV45ManageFiltersExecutionStep ;
      private short AV67TFWWPFormVersionNumber ;
      private short AV68TFWWPFormVersionNumber_To ;
      private short AV61TFWWPFormLatestVersionNumber ;
      private short AV62TFWWPFormLatestVersionNumber_To ;
      private short wbEnd ;
      private short wbStart ;
      private short A240WWPFormType ;
      private short nDraw ;
      private short nDoneStart ;
      private short A206WWPFormId ;
      private short A207WWPFormVersionNumber ;
      private short A219WWPFormLatestVersionNumber ;
      private short AV10ActionGroup ;
      private short nCmpId ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGrid_Backcolorstyle ;
      private short subGrid_Sortable ;
      private short GXt_int1 ;
      private short AV14CopyNumber ;
      private short nGXWrapped ;
      private short subGrid_Backstyle ;
      private short subGrid_Titlebackstyle ;
      private short subGrid_Allowselection ;
      private short subGrid_Allowhovering ;
      private short subGrid_Allowcollapsing ;
      private short subGrid_Collapsed ;
      private int subGrid_Rows ;
      private int Gridpaginationbar_Rowsperpageselectedvalue ;
      private int nRC_GXsfl_47 ;
      private int nGXsfl_47_idx=1 ;
      private int Gridpaginationbar_Pagestoshow ;
      private int bttBtnuseractioninsert_Visible ;
      private int bttBtnsubscriptions_Visible ;
      private int edtavFilterfulltext_Enabled ;
      private int gxdynajaxindex ;
      private int subGrid_Islastpage ;
      private int edtOrganisationId_Enabled ;
      private int edtLocationId_Enabled ;
      private int edtWWPFormId_Enabled ;
      private int edtWWPFormTitle_Enabled ;
      private int edtWWPFormReferenceName_Enabled ;
      private int edtWWPFormDate_Enabled ;
      private int edtWWPFormVersionNumber_Enabled ;
      private int edtWWPFormLatestVersionNumber_Enabled ;
      private int edtWWPFormTitle_Visible ;
      private int edtWWPFormReferenceName_Visible ;
      private int edtWWPFormDate_Visible ;
      private int edtWWPFormVersionNumber_Visible ;
      private int edtWWPFormLatestVersionNumber_Visible ;
      private int AV55PageToGo ;
      private int AV80GXV1 ;
      private int AV82GXV3 ;
      private int AV84GXV5 ;
      private int AV88GXV6 ;
      private int AV89GXV7 ;
      private int idxLst ;
      private int subGrid_Backcolor ;
      private int subGrid_Allbackcolor ;
      private int subGrid_Titlebackcolor ;
      private int subGrid_Selectedindex ;
      private int subGrid_Selectioncolor ;
      private int subGrid_Hoveringcolor ;
      private long GRID_nFirstRecordOnPage ;
      private long AV26GridCurrentPage ;
      private long AV27GridPageCount ;
      private long GRID_nCurrentRecord ;
      private long GRID_nRecordCount ;
      private string Gridpaginationbar_Selectedpage ;
      private string Ddo_grid_Activeeventkey ;
      private string Ddo_grid_Selectedvalue_get ;
      private string Ddo_grid_Selectedcolumn ;
      private string Ddo_grid_Filteredtext_get ;
      private string Ddo_grid_Filteredtextto_get ;
      private string Ddo_gridcolumnsselector_Columnsselectorvalues ;
      private string Ddo_managefilters_Activeeventkey ;
      private string Dvelop_confirmpanel_useractiondelete_Result ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_47_idx="0001" ;
      private string AV78Pgmname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string Ddo_managefilters_Icontype ;
      private string Ddo_managefilters_Icon ;
      private string Ddo_managefilters_Tooltip ;
      private string Ddo_managefilters_Cls ;
      private string Gridpaginationbar_Class ;
      private string Gridpaginationbar_Pagingbuttonsposition ;
      private string Gridpaginationbar_Pagingcaptionposition ;
      private string Gridpaginationbar_Emptygridclass ;
      private string Gridpaginationbar_Rowsperpageoptions ;
      private string Gridpaginationbar_Previous ;
      private string Gridpaginationbar_Next ;
      private string Gridpaginationbar_Caption ;
      private string Gridpaginationbar_Emptygridcaption ;
      private string Gridpaginationbar_Rowsperpagecaption ;
      private string Ddc_subscriptions_Icontype ;
      private string Ddc_subscriptions_Icon ;
      private string Ddc_subscriptions_Caption ;
      private string Ddc_subscriptions_Tooltip ;
      private string Ddc_subscriptions_Cls ;
      private string Ddc_subscriptions_Titlecontrolidtoreplace ;
      private string Ddo_grid_Caption ;
      private string Ddo_grid_Filteredtext_set ;
      private string Ddo_grid_Filteredtextto_set ;
      private string Ddo_grid_Selectedvalue_set ;
      private string Ddo_grid_Gamoauthtoken ;
      private string Ddo_grid_Gridinternalname ;
      private string Ddo_grid_Columnids ;
      private string Ddo_grid_Columnssortvalues ;
      private string Ddo_grid_Includesortasc ;
      private string Ddo_grid_Sortedstatus ;
      private string Ddo_grid_Includefilter ;
      private string Ddo_grid_Filtertype ;
      private string Ddo_grid_Filterisrange ;
      private string Ddo_grid_Includedatalist ;
      private string Ddo_grid_Datalisttype ;
      private string Ddo_grid_Datalistproc ;
      private string Ddo_grid_Format ;
      private string Ddo_gridcolumnsselector_Icontype ;
      private string Ddo_gridcolumnsselector_Icon ;
      private string Ddo_gridcolumnsselector_Caption ;
      private string Ddo_gridcolumnsselector_Tooltip ;
      private string Ddo_gridcolumnsselector_Cls ;
      private string Ddo_gridcolumnsselector_Dropdownoptionstype ;
      private string Ddo_gridcolumnsselector_Gridinternalname ;
      private string Ddo_gridcolumnsselector_Titlecontrolidtoreplace ;
      private string Dvelop_confirmpanel_useractiondelete_Title ;
      private string Dvelop_confirmpanel_useractiondelete_Confirmationtext ;
      private string Dvelop_confirmpanel_useractiondelete_Yesbuttoncaption ;
      private string Dvelop_confirmpanel_useractiondelete_Nobuttoncaption ;
      private string Dvelop_confirmpanel_useractiondelete_Cancelbuttoncaption ;
      private string Dvelop_confirmpanel_useractiondelete_Yesbuttonposition ;
      private string Dvelop_confirmpanel_useractiondelete_Confirmtype ;
      private string Grid_empowerer_Gridinternalname ;
      private string GX_FocusControl ;
      private string divLayoutmaintable_Internalname ;
      private string divTablegridheader_Internalname ;
      private string divTableheader_Internalname ;
      private string divTableheadercontent_Internalname ;
      private string divTableactions_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttBtnuseractioninsert_Internalname ;
      private string bttBtnuseractioninsert_Jsonclick ;
      private string bttBtneditcolumns_Internalname ;
      private string bttBtneditcolumns_Jsonclick ;
      private string bttBtnsubscriptions_Internalname ;
      private string bttBtnsubscriptions_Jsonclick ;
      private string divTablerightheader_Internalname ;
      private string Ddo_managefilters_Caption ;
      private string Ddo_managefilters_Internalname ;
      private string divTablefilters_Internalname ;
      private string edtavFilterfulltext_Internalname ;
      private string edtavFilterfulltext_Jsonclick ;
      private string divOrganisationidfilter_cell_Internalname ;
      private string divOrganisationidfilter_cell_Class ;
      private string dynavOrganisationidfilter_Internalname ;
      private string dynavOrganisationidfilter_Jsonclick ;
      private string divLocationidfilter_cell_Internalname ;
      private string divLocationidfilter_cell_Class ;
      private string dynavLocationidfilter_Internalname ;
      private string dynavLocationidfilter_Jsonclick ;
      private string divGridtablewithpaginationbar_Internalname ;
      private string sStyleString ;
      private string subGrid_Internalname ;
      private string Gridpaginationbar_Internalname ;
      private string divHtml_bottomauxiliarcontrols_Internalname ;
      private string Ddc_subscriptions_Internalname ;
      private string Ddo_grid_Internalname ;
      private string cmbWWPFormType_Internalname ;
      private string cmbWWPFormType_Jsonclick ;
      private string Ddo_gridcolumnsselector_Internalname ;
      private string Grid_empowerer_Internalname ;
      private string divDiv_wwpauxwc_Internalname ;
      private string WebComp_Wwpaux_wc_Component ;
      private string OldWwpaux_wc ;
      private string divDdo_wwpformdateauxdates_Internalname ;
      private string edtavDdo_wwpformdateauxdatetext_Internalname ;
      private string edtavDdo_wwpformdateauxdatetext_Jsonclick ;
      private string Tfwwpformdate_rangepicker_Internalname ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string cmbavActiongroup_Internalname ;
      private string edtOrganisationId_Internalname ;
      private string edtLocationId_Internalname ;
      private string edtWWPFormId_Internalname ;
      private string edtWWPFormTitle_Internalname ;
      private string edtWWPFormReferenceName_Internalname ;
      private string edtWWPFormDate_Internalname ;
      private string edtWWPFormVersionNumber_Internalname ;
      private string edtWWPFormLatestVersionNumber_Internalname ;
      private string gxwrpcisep ;
      private string cmbavActiongroup_Class ;
      private string edtWWPFormTitle_Link ;
      private string GXt_char6 ;
      private string GXt_char3 ;
      private string tblTabledvelop_confirmpanel_useractiondelete_Internalname ;
      private string Dvelop_confirmpanel_useractiondelete_Internalname ;
      private string sCtrlAV9WWPFormType ;
      private string sCtrlAV76WWPFormIsForDynamicValidations ;
      private string sGXsfl_47_fel_idx="0001" ;
      private string subGrid_Class ;
      private string subGrid_Linesclass ;
      private string ROClassString ;
      private string edtOrganisationId_Jsonclick ;
      private string edtLocationId_Jsonclick ;
      private string edtWWPFormId_Jsonclick ;
      private string edtWWPFormTitle_Jsonclick ;
      private string edtWWPFormReferenceName_Jsonclick ;
      private string edtWWPFormDate_Jsonclick ;
      private string edtWWPFormVersionNumber_Jsonclick ;
      private string edtWWPFormLatestVersionNumber_Jsonclick ;
      private string GXCCtl ;
      private string cmbavActiongroup_Jsonclick ;
      private string subGrid_Header ;
      private DateTime AV59TFWWPFormDate ;
      private DateTime AV60TFWWPFormDate_To ;
      private DateTime A231WWPFormDate ;
      private DateTime AV16DDO_WWPFormDateAuxDate ;
      private DateTime AV18DDO_WWPFormDateAuxDateTo ;
      private bool AV76WWPFormIsForDynamicValidations ;
      private bool wcpOAV76WWPFormIsForDynamicValidations ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV52OrderedDsc ;
      private bool AV39IsAuthorized_UserActionEdit ;
      private bool AV38IsAuthorized_UserActionDisplay ;
      private bool AV36IsAuthorized_UserActionCopy ;
      private bool AV37IsAuthorized_UserActionDelete ;
      private bool AV40IsAuthorized_UserActionFilledForms ;
      private bool AV41IsAuthorized_UserActionFillOutForm ;
      private bool Gridpaginationbar_Showfirst ;
      private bool Gridpaginationbar_Showprevious ;
      private bool Gridpaginationbar_Shownext ;
      private bool Gridpaginationbar_Showlast ;
      private bool Gridpaginationbar_Rowsperpageselector ;
      private bool Grid_empowerer_Hastitlesettings ;
      private bool Grid_empowerer_Hascolumnsselector ;
      private bool wbLoad ;
      private bool bGXsfl_47_Refreshing=false ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_refresh_fired ;
      private bool AV42IsAuthorized_UserActionInsert ;
      private bool GXt_boolean4 ;
      private string AV13ColumnsSelectorXML ;
      private string AV46ManageFiltersXml ;
      private string AV72UserCustomValue ;
      private string AV22FilterFullText ;
      private string AV65TFWWPFormTitle ;
      private string AV66TFWWPFormTitle_Sel ;
      private string AV63TFWWPFormReferenceName ;
      private string AV64TFWWPFormReferenceName_Sel ;
      private string AV25GridAppliedFilters ;
      private string AV56ResultMsg ;
      private string AV17DDO_WWPFormDateAuxDateText ;
      private string A209WWPFormTitle ;
      private string A208WWPFormReferenceName ;
      private string lV65TFWWPFormTitle ;
      private string lV63TFWWPFormReferenceName ;
      private string AV8WWPFormReferenceName ;
      private Guid AV53OrganisationIdFilter ;
      private Guid AV43LocationIdFilter ;
      private Guid A366LocationDynamicFormId ;
      private Guid AV5LocationId ;
      private Guid AV6OrganisationId ;
      private Guid A11OrganisationId ;
      private Guid A29LocationId ;
      private Guid AV85Locationdynamicformid_selected ;
      private Guid AV86Organisationid_selected ;
      private Guid AV87Locationid_selected ;
      private Guid ZV43LocationIdFilter ;
      private IGxSession AV58Session ;
      private GXWebComponent WebComp_Wwpaux_wc ;
      private GeneXus.Utils.GxStringCollection gxdynajaxctrlcodr ;
      private GeneXus.Utils.GxStringCollection gxdynajaxctrldescr ;
      private GXWebGrid GridContainer ;
      private GXWebRow GridRow ;
      private GXWebColumn GridColumn ;
      private GXUserControl ucDdo_managefilters ;
      private GXUserControl ucGridpaginationbar ;
      private GXUserControl ucDdc_subscriptions ;
      private GXUserControl ucDdo_grid ;
      private GXUserControl ucDdo_gridcolumnsselector ;
      private GXUserControl ucGrid_empowerer ;
      private GXUserControl ucTfwwpformdate_rangepicker ;
      private GXUserControl ucDvelop_confirmpanel_useractiondelete ;
      private GXWebForm Form ;
      private GxHttpRequest AV30HTTPRequest ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXCombobox dynavOrganisationidfilter ;
      private GXCombobox dynavLocationidfilter ;
      private GXCombobox cmbavActiongroup ;
      private GXCombobox cmbWWPFormType ;
      private GeneXus.Programs.wwpbaseobjects.SdtWWPContext AV73WWPContext ;
      private WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector AV11ColumnsSelector ;
      private GXBaseCollection<WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsData_Item> AV44ManageFiltersData ;
      private WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsTitleSettingsIcons AV15DDO_TitleSettingsIcons ;
      private WorkWithPlus.workwithplus_web.SdtWWPGridState AV28GridState ;
      private IDataStoreProvider pr_default ;
      private Guid[] H00BB2_A11OrganisationId ;
      private string[] H00BB2_A13OrganisationName ;
      private Guid[] H00BB3_A29LocationId ;
      private string[] H00BB3_A31LocationName ;
      private Guid[] H00BB3_A11OrganisationId ;
      private Guid[] H00BB4_A366LocationDynamicFormId ;
      private short[] H00BB4_A240WWPFormType ;
      private short[] H00BB4_A207WWPFormVersionNumber ;
      private DateTime[] H00BB4_A231WWPFormDate ;
      private string[] H00BB4_A208WWPFormReferenceName ;
      private string[] H00BB4_A209WWPFormTitle ;
      private Guid[] H00BB4_A29LocationId ;
      private Guid[] H00BB4_A11OrganisationId ;
      private short[] H00BB4_A206WWPFormId ;
      private Guid[] H00BB5_A366LocationDynamicFormId ;
      private short[] H00BB5_A240WWPFormType ;
      private short[] H00BB5_A207WWPFormVersionNumber ;
      private DateTime[] H00BB5_A231WWPFormDate ;
      private string[] H00BB5_A208WWPFormReferenceName ;
      private string[] H00BB5_A209WWPFormTitle ;
      private Guid[] H00BB5_A29LocationId ;
      private Guid[] H00BB5_A11OrganisationId ;
      private short[] H00BB5_A206WWPFormId ;
      private GeneXus.Programs.genexussecurity.SdtGAMSession AV24GAMSession ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV23GAMErrors ;
      private WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsTitleSettingsIcons GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons2 ;
      private WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector AV12ColumnsSelectorAux ;
      private GXBaseCollection<WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsData_Item> GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item5 ;
      private SdtUForm AV74WWPForm ;
      private SdtUForm AV49NewWWPForm ;
      private short[] H00BB6_A207WWPFormVersionNumber ;
      private short[] H00BB6_A206WWPFormId ;
      private SdtUForm_Element AV21Element ;
      private SdtTrn_LocationDynamicForm AV7Trn_LocationDynamicForm ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV81GXV2 ;
      private GeneXus.Utils.SdtMessages_Message AV47Message ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV83GXV4 ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV48Messages ;
      private WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue AV29GridStateFilterValue ;
      private WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext AV69TrnContext ;
      private WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext_Attribute AV70TrnContextAtt ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_datastore1 ;
      private IDataStoreProvider pr_gam ;
   }

   public class wp_dynamicformtrn_locationdynamicformwc__datastore1 : DataStoreHelperBase, IDataStoreHelper
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

 public class wp_dynamicformtrn_locationdynamicformwc__gam : DataStoreHelperBase, IDataStoreHelper
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

public class wp_dynamicformtrn_locationdynamicformwc__default : DataStoreHelperBase, IDataStoreHelper
{
   protected Object[] conditional_H00BB4( IGxContext context ,
                                          string AV66TFWWPFormTitle_Sel ,
                                          string AV65TFWWPFormTitle ,
                                          string AV64TFWWPFormReferenceName_Sel ,
                                          string AV63TFWWPFormReferenceName ,
                                          DateTime AV59TFWWPFormDate ,
                                          DateTime AV60TFWWPFormDate_To ,
                                          short AV67TFWWPFormVersionNumber ,
                                          short AV68TFWWPFormVersionNumber_To ,
                                          string A209WWPFormTitle ,
                                          string A208WWPFormReferenceName ,
                                          DateTime A231WWPFormDate ,
                                          short A207WWPFormVersionNumber ,
                                          short AV50OrderedBy ,
                                          bool AV52OrderedDsc ,
                                          short A240WWPFormType ,
                                          short AV9WWPFormType ,
                                          string AV22FilterFullText ,
                                          short A219WWPFormLatestVersionNumber ,
                                          short AV61TFWWPFormLatestVersionNumber ,
                                          short AV62TFWWPFormLatestVersionNumber_To ,
                                          Guid A29LocationId ,
                                          Guid AV5LocationId )
   {
      System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
      string scmdbuf;
      short[] GXv_int7 = new short[10];
      Object[] GXv_Object8 = new Object[2];
      scmdbuf = "SELECT T1.LocationDynamicFormId, T2.WWPFormType, T1.WWPFormVersionNumber, T2.WWPFormDate, T2.WWPFormReferenceName, T2.WWPFormTitle, T1.LocationId, T1.OrganisationId, T1.WWPFormId FROM (Trn_LocationDynamicForm T1 INNER JOIN WWP_Form T2 ON T2.WWPFormId = T1.WWPFormId AND T2.WWPFormVersionNumber = T1.WWPFormVersionNumber)";
      AddWhere(sWhereString, "(T2.WWPFormType = :AV9WWPFormType)");
      AddWhere(sWhereString, "(T1.LocationId = :AV5LocationId)");
      if ( String.IsNullOrEmpty(StringUtil.RTrim( AV66TFWWPFormTitle_Sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV65TFWWPFormTitle)) ) )
      {
         AddWhere(sWhereString, "(T2.WWPFormTitle like :lV65TFWWPFormTitle)");
      }
      else
      {
         GXv_int7[2] = 1;
      }
      if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV66TFWWPFormTitle_Sel)) && ! ( StringUtil.StrCmp(AV66TFWWPFormTitle_Sel, "<#Empty#>") == 0 ) )
      {
         AddWhere(sWhereString, "(T2.WWPFormTitle = ( :AV66TFWWPFormTitle_Sel))");
      }
      else
      {
         GXv_int7[3] = 1;
      }
      if ( StringUtil.StrCmp(AV66TFWWPFormTitle_Sel, "<#Empty#>") == 0 )
      {
         AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T2.WWPFormTitle))=0))");
      }
      if ( String.IsNullOrEmpty(StringUtil.RTrim( AV64TFWWPFormReferenceName_Sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV63TFWWPFormReferenceName)) ) )
      {
         AddWhere(sWhereString, "(T2.WWPFormReferenceName like :lV63TFWWPFormReferenceName)");
      }
      else
      {
         GXv_int7[4] = 1;
      }
      if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV64TFWWPFormReferenceName_Sel)) && ! ( StringUtil.StrCmp(AV64TFWWPFormReferenceName_Sel, "<#Empty#>") == 0 ) )
      {
         AddWhere(sWhereString, "(T2.WWPFormReferenceName = ( :AV64TFWWPFormReferenceName_Sel))");
      }
      else
      {
         GXv_int7[5] = 1;
      }
      if ( StringUtil.StrCmp(AV64TFWWPFormReferenceName_Sel, "<#Empty#>") == 0 )
      {
         AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T2.WWPFormReferenceName))=0))");
      }
      if ( ! (DateTime.MinValue==AV59TFWWPFormDate) )
      {
         AddWhere(sWhereString, "(T2.WWPFormDate >= :AV59TFWWPFormDate)");
      }
      else
      {
         GXv_int7[6] = 1;
      }
      if ( ! (DateTime.MinValue==AV60TFWWPFormDate_To) )
      {
         AddWhere(sWhereString, "(T2.WWPFormDate <= :AV60TFWWPFormDate_To)");
      }
      else
      {
         GXv_int7[7] = 1;
      }
      if ( ! (0==AV67TFWWPFormVersionNumber) )
      {
         AddWhere(sWhereString, "(T1.WWPFormVersionNumber >= :AV67TFWWPFormVersionNumber)");
      }
      else
      {
         GXv_int7[8] = 1;
      }
      if ( ! (0==AV68TFWWPFormVersionNumber_To) )
      {
         AddWhere(sWhereString, "(T1.WWPFormVersionNumber <= :AV68TFWWPFormVersionNumber_To)");
      }
      else
      {
         GXv_int7[9] = 1;
      }
      scmdbuf += sWhereString;
      if ( ( AV50OrderedBy == 1 ) && ! AV52OrderedDsc )
      {
         scmdbuf += " ORDER BY T2.WWPFormType, T2.WWPFormTitle, T1.LocationDynamicFormId, T1.OrganisationId, T1.LocationId";
      }
      else if ( ( AV50OrderedBy == 1 ) && ( AV52OrderedDsc ) )
      {
         scmdbuf += " ORDER BY T2.WWPFormType DESC, T2.WWPFormTitle DESC, T1.LocationDynamicFormId, T1.OrganisationId, T1.LocationId";
      }
      else if ( ( AV50OrderedBy == 2 ) && ! AV52OrderedDsc )
      {
         scmdbuf += " ORDER BY T2.WWPFormType, T2.WWPFormReferenceName, T1.LocationDynamicFormId, T1.OrganisationId, T1.LocationId";
      }
      else if ( ( AV50OrderedBy == 2 ) && ( AV52OrderedDsc ) )
      {
         scmdbuf += " ORDER BY T2.WWPFormType DESC, T2.WWPFormReferenceName DESC, T1.LocationDynamicFormId, T1.OrganisationId, T1.LocationId";
      }
      else if ( ( AV50OrderedBy == 3 ) && ! AV52OrderedDsc )
      {
         scmdbuf += " ORDER BY T2.WWPFormType, T2.WWPFormDate, T1.LocationDynamicFormId, T1.OrganisationId, T1.LocationId";
      }
      else if ( ( AV50OrderedBy == 3 ) && ( AV52OrderedDsc ) )
      {
         scmdbuf += " ORDER BY T2.WWPFormType DESC, T2.WWPFormDate DESC, T1.LocationDynamicFormId, T1.OrganisationId, T1.LocationId";
      }
      else if ( ( AV50OrderedBy == 4 ) && ! AV52OrderedDsc )
      {
         scmdbuf += " ORDER BY T2.WWPFormType, T1.WWPFormVersionNumber, T1.LocationDynamicFormId, T1.OrganisationId, T1.LocationId";
      }
      else if ( ( AV50OrderedBy == 4 ) && ( AV52OrderedDsc ) )
      {
         scmdbuf += " ORDER BY T2.WWPFormType DESC, T1.WWPFormVersionNumber DESC, T1.LocationDynamicFormId, T1.OrganisationId, T1.LocationId";
      }
      GXv_Object8[0] = scmdbuf;
      GXv_Object8[1] = GXv_int7;
      return GXv_Object8 ;
   }

   protected Object[] conditional_H00BB5( IGxContext context ,
                                          string AV66TFWWPFormTitle_Sel ,
                                          string AV65TFWWPFormTitle ,
                                          string AV64TFWWPFormReferenceName_Sel ,
                                          string AV63TFWWPFormReferenceName ,
                                          DateTime AV59TFWWPFormDate ,
                                          DateTime AV60TFWWPFormDate_To ,
                                          short AV67TFWWPFormVersionNumber ,
                                          short AV68TFWWPFormVersionNumber_To ,
                                          string A209WWPFormTitle ,
                                          string A208WWPFormReferenceName ,
                                          DateTime A231WWPFormDate ,
                                          short A207WWPFormVersionNumber ,
                                          short AV50OrderedBy ,
                                          bool AV52OrderedDsc ,
                                          short A240WWPFormType ,
                                          short AV9WWPFormType ,
                                          string AV22FilterFullText ,
                                          short A219WWPFormLatestVersionNumber ,
                                          short AV61TFWWPFormLatestVersionNumber ,
                                          short AV62TFWWPFormLatestVersionNumber_To ,
                                          Guid A29LocationId ,
                                          Guid AV5LocationId )
   {
      System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
      string scmdbuf;
      short[] GXv_int9 = new short[10];
      Object[] GXv_Object10 = new Object[2];
      scmdbuf = "SELECT T1.LocationDynamicFormId, T2.WWPFormType, T1.WWPFormVersionNumber, T2.WWPFormDate, T2.WWPFormReferenceName, T2.WWPFormTitle, T1.LocationId, T1.OrganisationId, T1.WWPFormId FROM (Trn_LocationDynamicForm T1 INNER JOIN WWP_Form T2 ON T2.WWPFormId = T1.WWPFormId AND T2.WWPFormVersionNumber = T1.WWPFormVersionNumber)";
      AddWhere(sWhereString, "(T2.WWPFormType = :AV9WWPFormType)");
      AddWhere(sWhereString, "(T1.LocationId = :AV5LocationId)");
      if ( String.IsNullOrEmpty(StringUtil.RTrim( AV66TFWWPFormTitle_Sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV65TFWWPFormTitle)) ) )
      {
         AddWhere(sWhereString, "(T2.WWPFormTitle like :lV65TFWWPFormTitle)");
      }
      else
      {
         GXv_int9[2] = 1;
      }
      if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV66TFWWPFormTitle_Sel)) && ! ( StringUtil.StrCmp(AV66TFWWPFormTitle_Sel, "<#Empty#>") == 0 ) )
      {
         AddWhere(sWhereString, "(T2.WWPFormTitle = ( :AV66TFWWPFormTitle_Sel))");
      }
      else
      {
         GXv_int9[3] = 1;
      }
      if ( StringUtil.StrCmp(AV66TFWWPFormTitle_Sel, "<#Empty#>") == 0 )
      {
         AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T2.WWPFormTitle))=0))");
      }
      if ( String.IsNullOrEmpty(StringUtil.RTrim( AV64TFWWPFormReferenceName_Sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV63TFWWPFormReferenceName)) ) )
      {
         AddWhere(sWhereString, "(T2.WWPFormReferenceName like :lV63TFWWPFormReferenceName)");
      }
      else
      {
         GXv_int9[4] = 1;
      }
      if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV64TFWWPFormReferenceName_Sel)) && ! ( StringUtil.StrCmp(AV64TFWWPFormReferenceName_Sel, "<#Empty#>") == 0 ) )
      {
         AddWhere(sWhereString, "(T2.WWPFormReferenceName = ( :AV64TFWWPFormReferenceName_Sel))");
      }
      else
      {
         GXv_int9[5] = 1;
      }
      if ( StringUtil.StrCmp(AV64TFWWPFormReferenceName_Sel, "<#Empty#>") == 0 )
      {
         AddWhere(sWhereString, "((char_length(trim(trailing ' ' from T2.WWPFormReferenceName))=0))");
      }
      if ( ! (DateTime.MinValue==AV59TFWWPFormDate) )
      {
         AddWhere(sWhereString, "(T2.WWPFormDate >= :AV59TFWWPFormDate)");
      }
      else
      {
         GXv_int9[6] = 1;
      }
      if ( ! (DateTime.MinValue==AV60TFWWPFormDate_To) )
      {
         AddWhere(sWhereString, "(T2.WWPFormDate <= :AV60TFWWPFormDate_To)");
      }
      else
      {
         GXv_int9[7] = 1;
      }
      if ( ! (0==AV67TFWWPFormVersionNumber) )
      {
         AddWhere(sWhereString, "(T1.WWPFormVersionNumber >= :AV67TFWWPFormVersionNumber)");
      }
      else
      {
         GXv_int9[8] = 1;
      }
      if ( ! (0==AV68TFWWPFormVersionNumber_To) )
      {
         AddWhere(sWhereString, "(T1.WWPFormVersionNumber <= :AV68TFWWPFormVersionNumber_To)");
      }
      else
      {
         GXv_int9[9] = 1;
      }
      scmdbuf += sWhereString;
      if ( ( AV50OrderedBy == 1 ) && ! AV52OrderedDsc )
      {
         scmdbuf += " ORDER BY T2.WWPFormType, T2.WWPFormTitle, T1.LocationDynamicFormId, T1.OrganisationId, T1.LocationId";
      }
      else if ( ( AV50OrderedBy == 1 ) && ( AV52OrderedDsc ) )
      {
         scmdbuf += " ORDER BY T2.WWPFormType DESC, T2.WWPFormTitle DESC, T1.LocationDynamicFormId, T1.OrganisationId, T1.LocationId";
      }
      else if ( ( AV50OrderedBy == 2 ) && ! AV52OrderedDsc )
      {
         scmdbuf += " ORDER BY T2.WWPFormType, T2.WWPFormReferenceName, T1.LocationDynamicFormId, T1.OrganisationId, T1.LocationId";
      }
      else if ( ( AV50OrderedBy == 2 ) && ( AV52OrderedDsc ) )
      {
         scmdbuf += " ORDER BY T2.WWPFormType DESC, T2.WWPFormReferenceName DESC, T1.LocationDynamicFormId, T1.OrganisationId, T1.LocationId";
      }
      else if ( ( AV50OrderedBy == 3 ) && ! AV52OrderedDsc )
      {
         scmdbuf += " ORDER BY T2.WWPFormType, T2.WWPFormDate, T1.LocationDynamicFormId, T1.OrganisationId, T1.LocationId";
      }
      else if ( ( AV50OrderedBy == 3 ) && ( AV52OrderedDsc ) )
      {
         scmdbuf += " ORDER BY T2.WWPFormType DESC, T2.WWPFormDate DESC, T1.LocationDynamicFormId, T1.OrganisationId, T1.LocationId";
      }
      else if ( ( AV50OrderedBy == 4 ) && ! AV52OrderedDsc )
      {
         scmdbuf += " ORDER BY T2.WWPFormType, T1.WWPFormVersionNumber, T1.LocationDynamicFormId, T1.OrganisationId, T1.LocationId";
      }
      else if ( ( AV50OrderedBy == 4 ) && ( AV52OrderedDsc ) )
      {
         scmdbuf += " ORDER BY T2.WWPFormType DESC, T1.WWPFormVersionNumber DESC, T1.LocationDynamicFormId, T1.OrganisationId, T1.LocationId";
      }
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
            case 2 :
                  return conditional_H00BB4(context, (string)dynConstraints[0] , (string)dynConstraints[1] , (string)dynConstraints[2] , (string)dynConstraints[3] , (DateTime)dynConstraints[4] , (DateTime)dynConstraints[5] , (short)dynConstraints[6] , (short)dynConstraints[7] , (string)dynConstraints[8] , (string)dynConstraints[9] , (DateTime)dynConstraints[10] , (short)dynConstraints[11] , (short)dynConstraints[12] , (bool)dynConstraints[13] , (short)dynConstraints[14] , (short)dynConstraints[15] , (string)dynConstraints[16] , (short)dynConstraints[17] , (short)dynConstraints[18] , (short)dynConstraints[19] , (Guid)dynConstraints[20] , (Guid)dynConstraints[21] );
            case 3 :
                  return conditional_H00BB5(context, (string)dynConstraints[0] , (string)dynConstraints[1] , (string)dynConstraints[2] , (string)dynConstraints[3] , (DateTime)dynConstraints[4] , (DateTime)dynConstraints[5] , (short)dynConstraints[6] , (short)dynConstraints[7] , (string)dynConstraints[8] , (string)dynConstraints[9] , (DateTime)dynConstraints[10] , (short)dynConstraints[11] , (short)dynConstraints[12] , (bool)dynConstraints[13] , (short)dynConstraints[14] , (short)dynConstraints[15] , (string)dynConstraints[16] , (short)dynConstraints[17] , (short)dynConstraints[18] , (short)dynConstraints[19] , (Guid)dynConstraints[20] , (Guid)dynConstraints[21] );
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
    };
 }

 private static CursorDef[] def;
 private void cursorDefinitions( )
 {
    if ( def == null )
    {
       Object[] prmH00BB2;
       prmH00BB2 = new Object[] {
       };
       Object[] prmH00BB3;
       prmH00BB3 = new Object[] {
       new ParDef("AV53OrganisationIdFilter",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmH00BB6;
       prmH00BB6 = new Object[] {
       };
       Object[] prmH00BB4;
       prmH00BB4 = new Object[] {
       new ParDef("AV9WWPFormType",GXType.Int16,1,0) ,
       new ParDef("AV5LocationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("lV65TFWWPFormTitle",GXType.VarChar,100,0) ,
       new ParDef("AV66TFWWPFormTitle_Sel",GXType.VarChar,100,0) ,
       new ParDef("lV63TFWWPFormReferenceName",GXType.VarChar,100,0) ,
       new ParDef("AV64TFWWPFormReferenceName_Sel",GXType.VarChar,100,0) ,
       new ParDef("AV59TFWWPFormDate",GXType.DateTime,8,5) ,
       new ParDef("AV60TFWWPFormDate_To",GXType.DateTime,8,5) ,
       new ParDef("AV67TFWWPFormVersionNumber",GXType.Int16,4,0) ,
       new ParDef("AV68TFWWPFormVersionNumber_To",GXType.Int16,4,0)
       };
       Object[] prmH00BB5;
       prmH00BB5 = new Object[] {
       new ParDef("AV9WWPFormType",GXType.Int16,1,0) ,
       new ParDef("AV5LocationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("lV65TFWWPFormTitle",GXType.VarChar,100,0) ,
       new ParDef("AV66TFWWPFormTitle_Sel",GXType.VarChar,100,0) ,
       new ParDef("lV63TFWWPFormReferenceName",GXType.VarChar,100,0) ,
       new ParDef("AV64TFWWPFormReferenceName_Sel",GXType.VarChar,100,0) ,
       new ParDef("AV59TFWWPFormDate",GXType.DateTime,8,5) ,
       new ParDef("AV60TFWWPFormDate_To",GXType.DateTime,8,5) ,
       new ParDef("AV67TFWWPFormVersionNumber",GXType.Int16,4,0) ,
       new ParDef("AV68TFWWPFormVersionNumber_To",GXType.Int16,4,0)
       };
       def= new CursorDef[] {
           new CursorDef("H00BB2", "SELECT OrganisationId, OrganisationName FROM Trn_Organisation ORDER BY OrganisationName ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH00BB2,0, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("H00BB3", "SELECT LocationId, LocationName, OrganisationId FROM Trn_Location WHERE OrganisationId = :AV53OrganisationIdFilter ORDER BY LocationName ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH00BB3,0, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("H00BB4", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH00BB4,11, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("H00BB5", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH00BB5,11, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("H00BB6", "SELECT WWPFormVersionNumber, WWPFormId FROM WWP_Form ORDER BY WWPFormId DESC ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH00BB6,1, GxCacheFrequency.OFF ,true,true )
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
             return;
          case 1 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((string[]) buf[1])[0] = rslt.getVarchar(2);
             ((Guid[]) buf[2])[0] = rslt.getGuid(3);
             return;
          case 2 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((short[]) buf[1])[0] = rslt.getShort(2);
             ((short[]) buf[2])[0] = rslt.getShort(3);
             ((DateTime[]) buf[3])[0] = rslt.getGXDateTime(4);
             ((string[]) buf[4])[0] = rslt.getVarchar(5);
             ((string[]) buf[5])[0] = rslt.getVarchar(6);
             ((Guid[]) buf[6])[0] = rslt.getGuid(7);
             ((Guid[]) buf[7])[0] = rslt.getGuid(8);
             ((short[]) buf[8])[0] = rslt.getShort(9);
             return;
          case 3 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((short[]) buf[1])[0] = rslt.getShort(2);
             ((short[]) buf[2])[0] = rslt.getShort(3);
             ((DateTime[]) buf[3])[0] = rslt.getGXDateTime(4);
             ((string[]) buf[4])[0] = rslt.getVarchar(5);
             ((string[]) buf[5])[0] = rslt.getVarchar(6);
             ((Guid[]) buf[6])[0] = rslt.getGuid(7);
             ((Guid[]) buf[7])[0] = rslt.getGuid(8);
             ((short[]) buf[8])[0] = rslt.getShort(9);
             return;
          case 4 :
             ((short[]) buf[0])[0] = rslt.getShort(1);
             ((short[]) buf[1])[0] = rslt.getShort(2);
             return;
    }
 }

}

}
