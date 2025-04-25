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
   public class wp_dynamicformelementuformwc : GXWebComponent
   {
      public wp_dynamicformelementuformwc( )
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

      public wp_dynamicformelementuformwc( IGxContext context )
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
         this.AV79WWPFormType = aP0_WWPFormType;
         this.AV78WWPFormIsForDynamicValidations = aP1_WWPFormIsForDynamicValidations;
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
         cmbavGridactiongroup1 = new GXCombobox();
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
                  AV79WWPFormType = (short)(Math.Round(NumberUtil.Val( GetPar( "WWPFormType"), "."), 18, MidpointRounding.ToEven));
                  AssignAttri(sPrefix, false, "AV79WWPFormType", StringUtil.Str( (decimal)(AV79WWPFormType), 1, 0));
                  AV78WWPFormIsForDynamicValidations = StringUtil.StrToBool( GetPar( "WWPFormIsForDynamicValidations"));
                  AssignAttri(sPrefix, false, "AV78WWPFormIsForDynamicValidations", AV78WWPFormIsForDynamicValidations);
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix,(short)AV79WWPFormType,(bool)AV78WWPFormIsForDynamicValidations});
                  componentstart();
                  context.httpAjaxContext.ajax_rspStartCmp(sPrefix);
                  componentdraw();
                  context.httpAjaxContext.ajax_rspEndCmp();
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
         nRC_GXsfl_41 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_41"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_41_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_41_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_41_idx = GetPar( "sGXsfl_41_idx");
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
         AV53OrderedBy = (short)(Math.Round(NumberUtil.Val( GetPar( "OrderedBy"), "."), 18, MidpointRounding.ToEven));
         AV55OrderedDsc = StringUtil.StrToBool( GetPar( "OrderedDsc"));
         AV21FilterFullText = GetPar( "FilterFullText");
         AV79WWPFormType = (short)(Math.Round(NumberUtil.Val( GetPar( "WWPFormType"), "."), 18, MidpointRounding.ToEven));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV24GeneralDynamicFormids);
         AV48ManageFiltersExecutionStep = (short)(Math.Round(NumberUtil.Val( GetPar( "ManageFiltersExecutionStep"), "."), 18, MidpointRounding.ToEven));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV9ColumnsSelector);
         AV81Pgmname = GetPar( "Pgmname");
         AV67TFWWPFormTitle = GetPar( "TFWWPFormTitle");
         AV68TFWWPFormTitle_Sel = GetPar( "TFWWPFormTitle_Sel");
         AV65TFWWPFormReferenceName = GetPar( "TFWWPFormReferenceName");
         AV66TFWWPFormReferenceName_Sel = GetPar( "TFWWPFormReferenceName_Sel");
         AV61TFWWPFormDate = context.localUtil.ParseDTimeParm( GetPar( "TFWWPFormDate"));
         AV62TFWWPFormDate_To = context.localUtil.ParseDTimeParm( GetPar( "TFWWPFormDate_To"));
         AV69TFWWPFormVersionNumber = (short)(Math.Round(NumberUtil.Val( GetPar( "TFWWPFormVersionNumber"), "."), 18, MidpointRounding.ToEven));
         AV70TFWWPFormVersionNumber_To = (short)(Math.Round(NumberUtil.Val( GetPar( "TFWWPFormVersionNumber_To"), "."), 18, MidpointRounding.ToEven));
         AV63TFWWPFormLatestVersionNumber = (short)(Math.Round(NumberUtil.Val( GetPar( "TFWWPFormLatestVersionNumber"), "."), 18, MidpointRounding.ToEven));
         AV64TFWWPFormLatestVersionNumber_To = (short)(Math.Round(NumberUtil.Val( GetPar( "TFWWPFormLatestVersionNumber_To"), "."), 18, MidpointRounding.ToEven));
         AV78WWPFormIsForDynamicValidations = StringUtil.StrToBool( GetPar( "WWPFormIsForDynamicValidations"));
         AV46IsAuthorized_UUpdate = StringUtil.StrToBool( GetPar( "IsAuthorized_UUpdate"));
         AV42IsAuthorized_UDelete = StringUtil.StrToBool( GetPar( "IsAuthorized_UDelete"));
         AV20FilledOutForms = (short)(Math.Round(NumberUtil.Val( GetPar( "FilledOutForms"), "."), 18, MidpointRounding.ToEven));
         AV35IsAuthorized_Display = StringUtil.StrToBool( GetPar( "IsAuthorized_Display"));
         AV36IsAuthorized_ExportForm = StringUtil.StrToBool( GetPar( "IsAuthorized_ExportForm"));
         AV38IsAuthorized_FillOutAForm = StringUtil.StrToBool( GetPar( "IsAuthorized_FillOutAForm"));
         AV37IsAuthorized_FilledOutForms = StringUtil.StrToBool( GetPar( "IsAuthorized_FilledOutForms"));
         AV33IsAuthorized_Copy = StringUtil.StrToBool( GetPar( "IsAuthorized_Copy"));
         AV41IsAuthorized_UCopyToLocation = StringUtil.StrToBool( GetPar( "IsAuthorized_UCopyToLocation"));
         AV43IsAuthorized_UDirectCopyToLocation = StringUtil.StrToBool( GetPar( "IsAuthorized_UDirectCopyToLocation"));
         AV77WWPFormId = (short)(Math.Round(NumberUtil.Val( GetPar( "WWPFormId"), "."), 18, MidpointRounding.ToEven));
         AV5LocationId = StringUtil.StrToGuid( GetPar( "LocationId"));
         AV6OrganisationId = StringUtil.StrToGuid( GetPar( "OrganisationId"));
         AV44IsAuthorized_UInsert = StringUtil.StrToBool( GetPar( "IsAuthorized_UInsert"));
         AV40IsAuthorized_Insert = StringUtil.StrToBool( GetPar( "IsAuthorized_Insert"));
         AV39IsAuthorized_ImportForm = StringUtil.StrToBool( GetPar( "IsAuthorized_ImportForm"));
         cmbWWPFormType.FromJSonString( GetNextPar( ));
         A240WWPFormType = (short)(Math.Round(NumberUtil.Val( GetPar( "WWPFormType"), "."), 18, MidpointRounding.ToEven));
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGrid_refresh( subGrid_Rows, AV53OrderedBy, AV55OrderedDsc, AV21FilterFullText, AV79WWPFormType, AV24GeneralDynamicFormids, AV48ManageFiltersExecutionStep, AV9ColumnsSelector, AV81Pgmname, AV67TFWWPFormTitle, AV68TFWWPFormTitle_Sel, AV65TFWWPFormReferenceName, AV66TFWWPFormReferenceName_Sel, AV61TFWWPFormDate, AV62TFWWPFormDate_To, AV69TFWWPFormVersionNumber, AV70TFWWPFormVersionNumber_To, AV63TFWWPFormLatestVersionNumber, AV64TFWWPFormLatestVersionNumber_To, AV78WWPFormIsForDynamicValidations, AV46IsAuthorized_UUpdate, AV42IsAuthorized_UDelete, AV20FilledOutForms, AV35IsAuthorized_Display, AV36IsAuthorized_ExportForm, AV38IsAuthorized_FillOutAForm, AV37IsAuthorized_FilledOutForms, AV33IsAuthorized_Copy, AV41IsAuthorized_UCopyToLocation, AV43IsAuthorized_UDirectCopyToLocation, AV77WWPFormId, AV5LocationId, AV6OrganisationId, AV44IsAuthorized_UInsert, AV40IsAuthorized_Insert, AV39IsAuthorized_ImportForm, A240WWPFormType, sPrefix) ;
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
            PAB92( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               AV81Pgmname = "WP_DynamicFormElementUFormWC";
               edtavFilledoutforms_Enabled = 0;
               AssignProp(sPrefix, false, edtavFilledoutforms_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavFilledoutforms_Enabled), 5, 0), !bGXsfl_41_Refreshing);
               WSB92( ) ;
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
            context.SendWebValue( "WP_Dynamic Form Element UForm WC") ;
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
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 1918140), false, true);
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
         context.AddJavascriptSource("DVelop/Bootstrap/ConfirmPanel/BootstrapConfirmPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/ConfirmPanel/BootstrapConfirmPanelRender.js", "", false, true);
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
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wp_dynamicformelementuformwc.aspx", new object[] {UrlEncode(StringUtil.LTrimStr(AV79WWPFormType,1,0)),UrlEncode(StringUtil.BoolToStr(AV78WWPFormIsForDynamicValidations))}, new string[] {"WWPFormType","WWPFormIsForDynamicValidations"}) +"\">") ;
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
         GxWebStd.gx_hidden_field( context, sPrefix+"vPGMNAME", StringUtil.RTrim( AV81Pgmname));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPGMNAME", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( AV81Pgmname, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_UUPDATE", AV46IsAuthorized_UUpdate);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UUPDATE", GetSecureSignedToken( sPrefix, AV46IsAuthorized_UUpdate, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_UDELETE", AV42IsAuthorized_UDelete);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UDELETE", GetSecureSignedToken( sPrefix, AV42IsAuthorized_UDelete, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_DISPLAY", AV35IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( sPrefix, AV35IsAuthorized_Display, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_EXPORTFORM", AV36IsAuthorized_ExportForm);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_EXPORTFORM", GetSecureSignedToken( sPrefix, AV36IsAuthorized_ExportForm, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_FILLOUTAFORM", AV38IsAuthorized_FillOutAForm);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_FILLOUTAFORM", GetSecureSignedToken( sPrefix, AV38IsAuthorized_FillOutAForm, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_FILLEDOUTFORMS", AV37IsAuthorized_FilledOutForms);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_FILLEDOUTFORMS", GetSecureSignedToken( sPrefix, AV37IsAuthorized_FilledOutForms, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_COPY", AV33IsAuthorized_Copy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_COPY", GetSecureSignedToken( sPrefix, AV33IsAuthorized_Copy, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_UCOPYTOLOCATION", AV41IsAuthorized_UCopyToLocation);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UCOPYTOLOCATION", GetSecureSignedToken( sPrefix, AV41IsAuthorized_UCopyToLocation, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_UDIRECTCOPYTOLOCATION", AV43IsAuthorized_UDirectCopyToLocation);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UDIRECTCOPYTOLOCATION", GetSecureSignedToken( sPrefix, AV43IsAuthorized_UDirectCopyToLocation, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vWWPFORMID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV77WWPFormId), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWWPFORMID", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV77WWPFormId), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vLOCATIONID", AV5LocationId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vLOCATIONID", GetSecureSignedToken( sPrefix, AV5LocationId, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vORGANISATIONID", AV6OrganisationId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vORGANISATIONID", GetSecureSignedToken( sPrefix, AV6OrganisationId, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_UINSERT", AV44IsAuthorized_UInsert);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UINSERT", GetSecureSignedToken( sPrefix, AV44IsAuthorized_UInsert, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_INSERT", AV40IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( sPrefix, AV40IsAuthorized_Insert, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_IMPORTFORM", AV39IsAuthorized_ImportForm);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_IMPORTFORM", GetSecureSignedToken( sPrefix, AV39IsAuthorized_ImportForm, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMTYPE", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(A240WWPFormType), "9"), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", sPrefix+"hsh"+"WP_DynamicFormElementUFormWC");
         forbiddenHiddens.Add("WWPFormType", context.localUtil.Format( (decimal)(A240WWPFormType), "9"));
         GxWebStd.gx_hidden_field( context, sPrefix+"hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("wp_dynamicformelementuformwc:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         GxWebStd.gx_hidden_field( context, sPrefix+"GXH_vORDEREDBY", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV53OrderedBy), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"GXH_vORDEREDDSC", StringUtil.BoolToStr( AV55OrderedDsc));
         GxWebStd.gx_hidden_field( context, sPrefix+"GXH_vFILTERFULLTEXT", AV21FilterFullText);
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_41", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_41), 8, 0, ".", "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vMANAGEFILTERSDATA", AV47ManageFiltersData);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vMANAGEFILTERSDATA", AV47ManageFiltersData);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vGRIDCURRENTPAGE", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV27GridCurrentPage), 10, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vGRIDPAGECOUNT", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV28GridPageCount), 10, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vGRIDAPPLIEDFILTERS", AV26GridAppliedFilters);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vDDO_TITLESETTINGSICONS", AV13DDO_TitleSettingsIcons);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vDDO_TITLESETTINGSICONS", AV13DDO_TitleSettingsIcons);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vCOLUMNSSELECTOR", AV9ColumnsSelector);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vCOLUMNSSELECTOR", AV9ColumnsSelector);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vDDO_WWPFORMDATEAUXDATE", context.localUtil.DToC( AV14DDO_WWPFormDateAuxDate, 0, "/"));
         GxWebStd.gx_hidden_field( context, sPrefix+"vDDO_WWPFORMDATEAUXDATETO", context.localUtil.DToC( AV16DDO_WWPFormDateAuxDateTo, 0, "/"));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV79WWPFormType", StringUtil.LTrim( StringUtil.NToC( (decimal)(wcpOAV79WWPFormType), 1, 0, ".", "")));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"wcpOAV78WWPFormIsForDynamicValidations", wcpOAV78WWPFormIsForDynamicValidations);
         GxWebStd.gx_hidden_field( context, sPrefix+"vMANAGEFILTERSEXECUTIONSTEP", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV48ManageFiltersExecutionStep), 1, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vPGMNAME", StringUtil.RTrim( AV81Pgmname));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPGMNAME", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( AV81Pgmname, "")), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vWWPFORMTYPE", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV79WWPFormType), 1, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vORDEREDBY", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV53OrderedBy), 4, 0, ".", "")));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vORDEREDDSC", AV55OrderedDsc);
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMTITLE", AV67TFWWPFormTitle);
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMTITLE_SEL", AV68TFWWPFormTitle_Sel);
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMREFERENCENAME", AV65TFWWPFormReferenceName);
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMREFERENCENAME_SEL", AV66TFWWPFormReferenceName_Sel);
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMDATE", context.localUtil.TToC( AV61TFWWPFormDate, 10, 8, 0, 0, "/", ":", " "));
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMDATE_TO", context.localUtil.TToC( AV62TFWWPFormDate_To, 10, 8, 0, 0, "/", ":", " "));
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMVERSIONNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV69TFWWPFormVersionNumber), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMVERSIONNUMBER_TO", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV70TFWWPFormVersionNumber_To), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMLATESTVERSIONNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV63TFWWPFormLatestVersionNumber), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vTFWWPFORMLATESTVERSIONNUMBER_TO", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV64TFWWPFormLatestVersionNumber_To), 4, 0, ".", "")));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vWWPFORMISFORDYNAMICVALIDATIONS", AV78WWPFormIsForDynamicValidations);
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_UUPDATE", AV46IsAuthorized_UUpdate);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UUPDATE", GetSecureSignedToken( sPrefix, AV46IsAuthorized_UUpdate, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_UDELETE", AV42IsAuthorized_UDelete);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UDELETE", GetSecureSignedToken( sPrefix, AV42IsAuthorized_UDelete, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_DISPLAY", AV35IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( sPrefix, AV35IsAuthorized_Display, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_EXPORTFORM", AV36IsAuthorized_ExportForm);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_EXPORTFORM", GetSecureSignedToken( sPrefix, AV36IsAuthorized_ExportForm, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_FILLOUTAFORM", AV38IsAuthorized_FillOutAForm);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_FILLOUTAFORM", GetSecureSignedToken( sPrefix, AV38IsAuthorized_FillOutAForm, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_FILLEDOUTFORMS", AV37IsAuthorized_FilledOutForms);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_FILLEDOUTFORMS", GetSecureSignedToken( sPrefix, AV37IsAuthorized_FilledOutForms, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_COPY", AV33IsAuthorized_Copy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_COPY", GetSecureSignedToken( sPrefix, AV33IsAuthorized_Copy, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_UCOPYTOLOCATION", AV41IsAuthorized_UCopyToLocation);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UCOPYTOLOCATION", GetSecureSignedToken( sPrefix, AV41IsAuthorized_UCopyToLocation, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_UDIRECTCOPYTOLOCATION", AV43IsAuthorized_UDirectCopyToLocation);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UDIRECTCOPYTOLOCATION", GetSecureSignedToken( sPrefix, AV43IsAuthorized_UDirectCopyToLocation, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vWWPFORMID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV77WWPFormId), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWWPFORMID", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV77WWPFormId), "ZZZ9"), context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vGRIDSTATE", AV29GridState);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vGRIDSTATE", AV29GridState);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vRESULTMSG", AV58ResultMsg);
         GxWebStd.gx_hidden_field( context, sPrefix+"vLOCATIONID", AV5LocationId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vLOCATIONID", GetSecureSignedToken( sPrefix, AV5LocationId, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vORGANISATIONID", AV6OrganisationId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vORGANISATIONID", GetSecureSignedToken( sPrefix, AV6OrganisationId, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vWWPFORMID_SELECTED", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV82Wwpformid_selected), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vWWPFORMVERSIONNUMBER_SELECTED", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV83Wwpformversionnumber_selected), 4, 0, ".", "")));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_UINSERT", AV44IsAuthorized_UInsert);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UINSERT", GetSecureSignedToken( sPrefix, AV44IsAuthorized_UInsert, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_INSERT", AV40IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( sPrefix, AV40IsAuthorized_Insert, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_IMPORTFORM", AV39IsAuthorized_ImportForm);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_IMPORTFORM", GetSecureSignedToken( sPrefix, AV39IsAuthorized_ImportForm, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vGENERALDYNAMICFORMIDS", AV24GeneralDynamicFormids);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vGENERALDYNAMICFORMIDS", AV24GeneralDynamicFormids);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
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
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_UDELETE_Title", StringUtil.RTrim( Dvelop_confirmpanel_udelete_Title));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_UDELETE_Confirmationtext", StringUtil.RTrim( Dvelop_confirmpanel_udelete_Confirmationtext));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_UDELETE_Yesbuttoncaption", StringUtil.RTrim( Dvelop_confirmpanel_udelete_Yesbuttoncaption));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_UDELETE_Nobuttoncaption", StringUtil.RTrim( Dvelop_confirmpanel_udelete_Nobuttoncaption));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_UDELETE_Cancelbuttoncaption", StringUtil.RTrim( Dvelop_confirmpanel_udelete_Cancelbuttoncaption));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_UDELETE_Yesbuttonposition", StringUtil.RTrim( Dvelop_confirmpanel_udelete_Yesbuttonposition));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_UDELETE_Confirmtype", StringUtil.RTrim( Dvelop_confirmpanel_udelete_Confirmtype));
         GxWebStd.gx_hidden_field( context, sPrefix+"UCOPYTOLOCATION_MODAL_Width", StringUtil.RTrim( Ucopytolocation_modal_Width));
         GxWebStd.gx_hidden_field( context, sPrefix+"UCOPYTOLOCATION_MODAL_Title", StringUtil.RTrim( Ucopytolocation_modal_Title));
         GxWebStd.gx_hidden_field( context, sPrefix+"UCOPYTOLOCATION_MODAL_Confirmtype", StringUtil.RTrim( Ucopytolocation_modal_Confirmtype));
         GxWebStd.gx_hidden_field( context, sPrefix+"UCOPYTOLOCATION_MODAL_Bodytype", StringUtil.RTrim( Ucopytolocation_modal_Bodytype));
         GxWebStd.gx_hidden_field( context, sPrefix+"IMPORTFORM_MODAL_Width", StringUtil.RTrim( Importform_modal_Width));
         GxWebStd.gx_hidden_field( context, sPrefix+"IMPORTFORM_MODAL_Title", StringUtil.RTrim( Importform_modal_Title));
         GxWebStd.gx_hidden_field( context, sPrefix+"IMPORTFORM_MODAL_Confirmtype", StringUtil.RTrim( Importform_modal_Confirmtype));
         GxWebStd.gx_hidden_field( context, sPrefix+"IMPORTFORM_MODAL_Bodytype", StringUtil.RTrim( Importform_modal_Bodytype));
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
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_UDELETE_Result", StringUtil.RTrim( Dvelop_confirmpanel_udelete_Result));
         GxWebStd.gx_hidden_field( context, sPrefix+"UCOPYTOLOCATION_MODAL_Result", StringUtil.RTrim( Ucopytolocation_modal_Result));
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
         GxWebStd.gx_hidden_field( context, sPrefix+"DVELOP_CONFIRMPANEL_UDELETE_Result", StringUtil.RTrim( Dvelop_confirmpanel_udelete_Result));
         GxWebStd.gx_hidden_field( context, sPrefix+"UCOPYTOLOCATION_MODAL_Result", StringUtil.RTrim( Ucopytolocation_modal_Result));
      }

      protected void RenderHtmlCloseFormB92( )
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
         return "WP_DynamicFormElementUFormWC" ;
      }

      public override string GetPgmdesc( )
      {
         return "WP_Dynamic Form Element UForm WC" ;
      }

      protected void WBB90( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wp_dynamicformelementuformwc.aspx");
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
               context.AddJavascriptSource("DVelop/Bootstrap/ConfirmPanel/BootstrapConfirmPanelRender.js", "", false, true);
               context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
               context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
               context.AddJavascriptSource("DVelop/Bootstrap/ConfirmPanel/BootstrapConfirmPanelRender.js", "", false, true);
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group ActionGroupColoredActions", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnuinsert_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(41), 2, 0)+","+"null"+");", "Insert", bttBtnuinsert_Jsonclick, 5, "Insert", "", StyleString, ClassString, bttBtnuinsert_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'DOUINSERT\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_WP_DynamicFormElementUFormWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button ButtonColor hidden-xs hidden-sm hidden-md hidden-lg";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtninsert_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(41), 2, 0)+","+"null"+");", "Insert", bttBtninsert_Jsonclick, 5, "Insert", "", StyleString, ClassString, bttBtninsert_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'DOINSERT\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_WP_DynamicFormElementUFormWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 21,'" + sPrefix + "',false,'',0)\"";
            ClassString = "hidden-xs";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtneditcolumns_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(41), 2, 0)+","+"null"+");", "Select columns", bttBtneditcolumns_Jsonclick, 0, "Select columns", "", StyleString, ClassString, 1, 0, "standard", "'"+sPrefix+"'"+",false,"+"'"+""+"'", TempTags, "", context.GetButtonType( ), "HLP_WP_DynamicFormElementUFormWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 23,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnimportform_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(41), 2, 0)+","+"null"+");", "Import Form", bttBtnimportform_Jsonclick, 5, "Import Form", "", StyleString, ClassString, bttBtnimportform_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'DOIMPORTFORM\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_WP_DynamicFormElementUFormWC.htm");
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
            ucDdo_managefilters.SetProperty("DropDownOptionsData", AV47ManageFiltersData);
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
            GxWebStd.gx_label_element( context, edtavFilterfulltext_Internalname, "Filter Full Text", "gx-form-item AttributeLabel", 0, true, "width: 25%;");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 32,'" + sPrefix + "',false,'" + sGXsfl_41_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavFilterfulltext_Internalname, AV21FilterFullText, StringUtil.RTrim( context.localUtil.Format( AV21FilterFullText, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,32);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "Search", edtavFilterfulltext_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavFilterfulltext_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, -1, -1, true, "WorkWithPlus_Web\\WWPFullTextFilter", "start", true, "", "HLP_WP_DynamicFormElementUFormWC.htm");
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
            StartGridControl41( ) ;
         }
         if ( wbEnd == 41 )
         {
            wbEnd = 0;
            nRC_GXsfl_41 = (int)(nGXsfl_41_idx-1);
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
            ucGridpaginationbar.SetProperty("CurrentPage", AV27GridCurrentPage);
            ucGridpaginationbar.SetProperty("PageCount", AV28GridPageCount);
            ucGridpaginationbar.SetProperty("AppliedFilters", AV26GridAppliedFilters);
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
            ucDdo_grid.SetProperty("DropDownOptionsTitleSettingsIcons", AV13DDO_TitleSettingsIcons);
            ucDdo_grid.Render(context, "dvelop.gxbootstrap.ddogridtitlesettingsm", Ddo_grid_Internalname, sPrefix+"DDO_GRIDContainer");
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbWWPFormType, cmbWWPFormType_Internalname, StringUtil.Trim( StringUtil.Str( (decimal)(A240WWPFormType), 1, 0)), 1, cmbWWPFormType_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "int", "", cmbWWPFormType.Visible, 0, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", "", "", true, 0, "HLP_WP_DynamicFormElementUFormWC.htm");
            cmbWWPFormType.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(A240WWPFormType), 1, 0));
            AssignProp(sPrefix, false, cmbWWPFormType_Internalname, "Values", (string)(cmbWWPFormType.ToJavascriptSource()), true);
            /* User Defined Control */
            ucDdo_gridcolumnsselector.SetProperty("IconType", Ddo_gridcolumnsselector_Icontype);
            ucDdo_gridcolumnsselector.SetProperty("Icon", Ddo_gridcolumnsselector_Icon);
            ucDdo_gridcolumnsselector.SetProperty("Caption", Ddo_gridcolumnsselector_Caption);
            ucDdo_gridcolumnsselector.SetProperty("Tooltip", Ddo_gridcolumnsselector_Tooltip);
            ucDdo_gridcolumnsselector.SetProperty("Cls", Ddo_gridcolumnsselector_Cls);
            ucDdo_gridcolumnsselector.SetProperty("DropDownOptionsType", Ddo_gridcolumnsselector_Dropdownoptionstype);
            ucDdo_gridcolumnsselector.SetProperty("DropDownOptionsTitleSettingsIcons", AV13DDO_TitleSettingsIcons);
            ucDdo_gridcolumnsselector.SetProperty("DropDownOptionsData", AV9ColumnsSelector);
            ucDdo_gridcolumnsselector.Render(context, "dvelop.gxbootstrap.ddogridcolumnsselector", Ddo_gridcolumnsselector_Internalname, sPrefix+"DDO_GRIDCOLUMNSSELECTORContainer");
            wb_table1_59_B92( true) ;
         }
         else
         {
            wb_table1_59_B92( false) ;
         }
         return  ;
      }

      protected void wb_table1_59_B92e( bool wbgen )
      {
         if ( wbgen )
         {
            wb_table2_64_B92( true) ;
         }
         else
         {
            wb_table2_64_B92( false) ;
         }
         return  ;
      }

      protected void wb_table2_64_B92e( bool wbgen )
      {
         if ( wbgen )
         {
            wb_table3_69_B92( true) ;
         }
         else
         {
            wb_table3_69_B92( false) ;
         }
         return  ;
      }

      protected void wb_table3_69_B92e( bool wbgen )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"W0076"+"", StringUtil.RTrim( WebComp_Wwpaux_wc_Component));
               context.WriteHtmlText( "<div") ;
               GxWebStd.ClassAttribute( context, "gxwebcomponent");
               context.WriteHtmlText( " id=\""+sPrefix+"gxHTMLWrpW0076"+""+"\""+"") ;
               context.WriteHtmlText( ">") ;
               if ( bGXsfl_41_Refreshing )
               {
                  if ( StringUtil.Len( WebComp_Wwpaux_wc_Component) != 0 )
                  {
                     if ( StringUtil.StrCmp(StringUtil.Lower( OldWwpaux_wc), StringUtil.Lower( WebComp_Wwpaux_wc_Component)) != 0 )
                     {
                        context.httpAjaxContext.ajax_rspStartCmp(sPrefix+"gxHTMLWrpW0076"+"");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 78,'" + sPrefix + "',false,'" + sGXsfl_41_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavDdo_wwpformdateauxdatetext_Internalname, AV15DDO_WWPFormDateAuxDateText, StringUtil.RTrim( context.localUtil.Format( AV15DDO_WWPFormDateAuxDateText, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,78);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavDdo_wwpformdateauxdatetext_Jsonclick, 0, "Attribute", "", "", "", "", 1, 1, 0, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_WP_DynamicFormElementUFormWC.htm");
            /* User Defined Control */
            ucTfwwpformdate_rangepicker.SetProperty("Start Date", AV14DDO_WWPFormDateAuxDate);
            ucTfwwpformdate_rangepicker.SetProperty("End Date", AV16DDO_WWPFormDateAuxDateTo);
            ucTfwwpformdate_rangepicker.Render(context, "wwp.daterangepicker", Tfwwpformdate_rangepicker_Internalname, sPrefix+"TFWWPFORMDATE_RANGEPICKERContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 41 )
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

      protected void STARTB92( )
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
            Form.Meta.addItem("description", "WP_Dynamic Form Element UForm WC", 0) ;
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
               STRUPB90( ) ;
            }
         }
      }

      protected void WSB92( )
      {
         STARTB92( ) ;
         EVTB92( ) ;
      }

      protected void EVTB92( )
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
                                 STRUPB90( ) ;
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
                                 STRUPB90( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Ddo_managefilters.Onoptionclicked */
                                    E11B92 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDPAGINATIONBAR.CHANGEPAGE") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPB90( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridpaginationbar.Changepage */
                                    E12B92 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDPAGINATIONBAR.CHANGEROWSPERPAGE") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPB90( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridpaginationbar.Changerowsperpage */
                                    E13B92 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_GRID.ONOPTIONCLICKED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPB90( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Ddo_grid.Onoptionclicked */
                                    E14B92 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPB90( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Ddo_gridcolumnsselector.Oncolumnschanged */
                                    E15B92 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DVELOP_CONFIRMPANEL_UDELETE.CLOSE") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPB90( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Dvelop_confirmpanel_udelete.Close */
                                    E16B92 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "UCOPYTOLOCATION_MODAL.CLOSE") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPB90( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Ucopytolocation_modal.Close */
                                    E17B92 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "IMPORTFORM_MODAL.CLOSE") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPB90( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Importform_modal.Close */
                                    E18B92 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOUINSERT'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPB90( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'DoUInsert' */
                                    E19B92 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOINSERT'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPB90( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'DoInsert' */
                                    E20B92 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOIMPORTFORM'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPB90( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'DoImportForm' */
                                    E21B92 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPB90( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavFilledoutforms_Internalname;
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
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 9), "GRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 23), "VGRIDACTIONGROUP1.CLICK") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 23), "VGRIDACTIONGROUP1.CLICK") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUPB90( ) ;
                              }
                              nGXsfl_41_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_41_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_41_idx), 4, 0), 4, "0");
                              SubsflControlProps_412( ) ;
                              A206WWPFormId = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtWWPFormId_Internalname), ".", ","), 18, MidpointRounding.ToEven));
                              A209WWPFormTitle = cgiGet( edtWWPFormTitle_Internalname);
                              A208WWPFormReferenceName = cgiGet( edtWWPFormReferenceName_Internalname);
                              A231WWPFormDate = context.localUtil.CToT( cgiGet( edtWWPFormDate_Internalname), 0);
                              A207WWPFormVersionNumber = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtWWPFormVersionNumber_Internalname), ".", ","), 18, MidpointRounding.ToEven));
                              A219WWPFormLatestVersionNumber = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtWWPFormLatestVersionNumber_Internalname), ".", ","), 18, MidpointRounding.ToEven));
                              if ( ( ( context.localUtil.CToN( cgiGet( edtavFilledoutforms_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavFilledoutforms_Internalname), ".", ",") > Convert.ToDecimal( 9999 )) ) )
                              {
                                 GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vFILLEDOUTFORMS");
                                 GX_FocusControl = edtavFilledoutforms_Internalname;
                                 AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 wbErr = true;
                                 AV20FilledOutForms = 0;
                                 AssignAttri(sPrefix, false, edtavFilledoutforms_Internalname, StringUtil.LTrimStr( (decimal)(AV20FilledOutForms), 4, 0));
                                 GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFILLEDOUTFORMS"+"_"+sGXsfl_41_idx, GetSecureSignedToken( sPrefix+sGXsfl_41_idx, context.localUtil.Format( (decimal)(AV20FilledOutForms), "ZZZ9"), context));
                              }
                              else
                              {
                                 AV20FilledOutForms = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavFilledoutforms_Internalname), ".", ","), 18, MidpointRounding.ToEven));
                                 AssignAttri(sPrefix, false, edtavFilledoutforms_Internalname, StringUtil.LTrimStr( (decimal)(AV20FilledOutForms), 4, 0));
                                 GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFILLEDOUTFORMS"+"_"+sGXsfl_41_idx, GetSecureSignedToken( sPrefix+sGXsfl_41_idx, context.localUtil.Format( (decimal)(AV20FilledOutForms), "ZZZ9"), context));
                              }
                              cmbavGridactiongroup1.Name = cmbavGridactiongroup1_Internalname;
                              cmbavGridactiongroup1.CurrentValue = cgiGet( cmbavGridactiongroup1_Internalname);
                              AV25GridActionGroup1 = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbavGridactiongroup1_Internalname), "."), 18, MidpointRounding.ToEven));
                              AssignAttri(sPrefix, false, cmbavGridactiongroup1_Internalname, StringUtil.LTrimStr( (decimal)(AV25GridActionGroup1), 4, 0));
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
                                          GX_FocusControl = edtavFilledoutforms_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Start */
                                          E22B92 ();
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
                                          GX_FocusControl = edtavFilledoutforms_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Refresh */
                                          E23B92 ();
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
                                          GX_FocusControl = edtavFilledoutforms_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Grid.Load */
                                          E24B92 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "VGRIDACTIONGROUP1.CLICK") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavFilledoutforms_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          E25B92 ();
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
                                             if ( ( context.localUtil.CToN( cgiGet( sPrefix+"GXH_vORDEREDBY"), ".", ",") != Convert.ToDecimal( AV53OrderedBy )) )
                                             {
                                                Rfr0gs = true;
                                             }
                                             /* Set Refresh If Ordereddsc Changed */
                                             if ( StringUtil.StrToBool( cgiGet( sPrefix+"GXH_vORDEREDDSC")) != AV55OrderedDsc )
                                             {
                                                Rfr0gs = true;
                                             }
                                             /* Set Refresh If Filterfulltext Changed */
                                             if ( StringUtil.StrCmp(cgiGet( sPrefix+"GXH_vFILTERFULLTEXT"), AV21FilterFullText) != 0 )
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
                                       STRUPB90( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavFilledoutforms_Internalname;
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
                        if ( nCmpId == 76 )
                        {
                           OldWwpaux_wc = cgiGet( sPrefix+"W0076");
                           if ( ( StringUtil.Len( OldWwpaux_wc) == 0 ) || ( StringUtil.StrCmp(OldWwpaux_wc, WebComp_Wwpaux_wc_Component) != 0 ) )
                           {
                              WebComp_Wwpaux_wc = getWebComponent(GetType(), "GeneXus.Programs", OldWwpaux_wc, new Object[] {context} );
                              WebComp_Wwpaux_wc.ComponentInit();
                              WebComp_Wwpaux_wc.Name = "OldWwpaux_wc";
                              WebComp_Wwpaux_wc_Component = OldWwpaux_wc;
                           }
                           if ( StringUtil.Len( WebComp_Wwpaux_wc_Component) != 0 )
                           {
                              WebComp_Wwpaux_wc.componentprocess(sPrefix+"W0076", "", sEvt);
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

      protected void WEB92( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseFormB92( ) ;
            }
         }
      }

      protected void PAB92( )
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

      protected void gxnrGrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_412( ) ;
         while ( nGXsfl_41_idx <= nRC_GXsfl_41 )
         {
            sendrow_412( ) ;
            nGXsfl_41_idx = ((subGrid_Islastpage==1)&&(nGXsfl_41_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_41_idx+1);
            sGXsfl_41_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_41_idx), 4, 0), 4, "0");
            SubsflControlProps_412( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridContainer)) ;
         /* End function gxnrGrid_newrow */
      }

      protected void gxgrGrid_refresh( int subGrid_Rows ,
                                       short AV53OrderedBy ,
                                       bool AV55OrderedDsc ,
                                       string AV21FilterFullText ,
                                       short AV79WWPFormType ,
                                       GxSimpleCollection<short> AV24GeneralDynamicFormids ,
                                       short AV48ManageFiltersExecutionStep ,
                                       WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector AV9ColumnsSelector ,
                                       string AV81Pgmname ,
                                       string AV67TFWWPFormTitle ,
                                       string AV68TFWWPFormTitle_Sel ,
                                       string AV65TFWWPFormReferenceName ,
                                       string AV66TFWWPFormReferenceName_Sel ,
                                       DateTime AV61TFWWPFormDate ,
                                       DateTime AV62TFWWPFormDate_To ,
                                       short AV69TFWWPFormVersionNumber ,
                                       short AV70TFWWPFormVersionNumber_To ,
                                       short AV63TFWWPFormLatestVersionNumber ,
                                       short AV64TFWWPFormLatestVersionNumber_To ,
                                       bool AV78WWPFormIsForDynamicValidations ,
                                       bool AV46IsAuthorized_UUpdate ,
                                       bool AV42IsAuthorized_UDelete ,
                                       short AV20FilledOutForms ,
                                       bool AV35IsAuthorized_Display ,
                                       bool AV36IsAuthorized_ExportForm ,
                                       bool AV38IsAuthorized_FillOutAForm ,
                                       bool AV37IsAuthorized_FilledOutForms ,
                                       bool AV33IsAuthorized_Copy ,
                                       bool AV41IsAuthorized_UCopyToLocation ,
                                       bool AV43IsAuthorized_UDirectCopyToLocation ,
                                       short AV77WWPFormId ,
                                       Guid AV5LocationId ,
                                       Guid AV6OrganisationId ,
                                       bool AV44IsAuthorized_UInsert ,
                                       bool AV40IsAuthorized_Insert ,
                                       bool AV39IsAuthorized_ImportForm ,
                                       short A240WWPFormType ,
                                       string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRID_nCurrentRecord = 0;
         RFB92( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", sPrefix+"hsh"+"WP_DynamicFormElementUFormWC");
         forbiddenHiddens.Add("WWPFormType", context.localUtil.Format( (decimal)(A240WWPFormType), "9"));
         GxWebStd.gx_hidden_field( context, sPrefix+"hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("wp_dynamicformelementuformwc:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
         /* End function gxgrGrid_refresh */
      }

      protected void send_integrity_hashes( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFILLEDOUTFORMS", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV20FilledOutForms), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vFILLEDOUTFORMS", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV20FilledOutForms), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMID", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(A206WWPFormId), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"WWPFORMID", StringUtil.LTrim( StringUtil.NToC( (decimal)(A206WWPFormId), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMTITLE", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( A209WWPFormTitle, "")), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"WWPFORMTITLE", A209WWPFormTitle);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMVERSIONNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(A207WWPFormVersionNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"WWPFORMVERSIONNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(A207WWPFormVersionNumber), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMREFERENCENAME", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( A208WWPFormReferenceName, "")), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"WWPFORMREFERENCENAME", A208WWPFormReferenceName);
      }

      protected void clear_multi_value_controls( )
      {
         if ( context.isAjaxRequest( ) )
         {
            dynload_actions( ) ;
            before_start_formulas( ) ;
         }
      }

      protected void fix_multi_value_controls( )
      {
         if ( cmbWWPFormType.ItemCount > 0 )
         {
            A240WWPFormType = (short)(Math.Round(NumberUtil.Val( cmbWWPFormType.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(A240WWPFormType), 1, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri(sPrefix, false, "A240WWPFormType", StringUtil.Str( (decimal)(A240WWPFormType), 1, 0));
            GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMTYPE", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(A240WWPFormType), "9"), context));
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
         RFB92( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         AV81Pgmname = "WP_DynamicFormElementUFormWC";
         edtavFilledoutforms_Enabled = 0;
      }

      protected int subGridclient_rec_count_fnc( )
      {
         GRID_nRecordCount = 0;
         pr_default.dynParam(0, new Object[]{ new Object[]{
                                              A206WWPFormId ,
                                              AV24GeneralDynamicFormids ,
                                              AV68TFWWPFormTitle_Sel ,
                                              AV67TFWWPFormTitle ,
                                              AV66TFWWPFormReferenceName_Sel ,
                                              AV65TFWWPFormReferenceName ,
                                              AV61TFWWPFormDate ,
                                              AV62TFWWPFormDate_To ,
                                              AV69TFWWPFormVersionNumber ,
                                              AV70TFWWPFormVersionNumber_To ,
                                              A209WWPFormTitle ,
                                              A208WWPFormReferenceName ,
                                              A231WWPFormDate ,
                                              A207WWPFormVersionNumber ,
                                              AV53OrderedBy ,
                                              AV55OrderedDsc ,
                                              A240WWPFormType ,
                                              AV79WWPFormType ,
                                              AV21FilterFullText ,
                                              A219WWPFormLatestVersionNumber ,
                                              AV63TFWWPFormLatestVersionNumber ,
                                              AV64TFWWPFormLatestVersionNumber_To } ,
                                              new int[]{
                                              TypeConstants.SHORT, TypeConstants.DATE, TypeConstants.DATE, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.DATE, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.BOOLEAN, TypeConstants.SHORT,
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT
                                              }
         });
         lV67TFWWPFormTitle = StringUtil.Concat( StringUtil.RTrim( AV67TFWWPFormTitle), "%", "");
         lV65TFWWPFormReferenceName = StringUtil.Concat( StringUtil.RTrim( AV65TFWWPFormReferenceName), "%", "");
         /* Using cursor H00B92 */
         pr_default.execute(0, new Object[] {AV79WWPFormType, lV67TFWWPFormTitle, AV68TFWWPFormTitle_Sel, lV65TFWWPFormReferenceName, AV66TFWWPFormReferenceName_Sel, AV61TFWWPFormDate, AV62TFWWPFormDate_To, AV69TFWWPFormVersionNumber, AV70TFWWPFormVersionNumber_To});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A240WWPFormType = H00B92_A240WWPFormType[0];
            AssignAttri(sPrefix, false, "A240WWPFormType", StringUtil.Str( (decimal)(A240WWPFormType), 1, 0));
            GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMTYPE", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(A240WWPFormType), "9"), context));
            A207WWPFormVersionNumber = H00B92_A207WWPFormVersionNumber[0];
            A231WWPFormDate = H00B92_A231WWPFormDate[0];
            A208WWPFormReferenceName = H00B92_A208WWPFormReferenceName[0];
            A209WWPFormTitle = H00B92_A209WWPFormTitle[0];
            A206WWPFormId = H00B92_A206WWPFormId[0];
            GXt_int1 = A219WWPFormLatestVersionNumber;
            new GeneXus.Programs.workwithplus.dynamicforms.wwp_df_getlatestversionofform(context ).execute(  A206WWPFormId, out  GXt_int1) ;
            A219WWPFormLatestVersionNumber = GXt_int1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV21FilterFullText)) || ( ( StringUtil.Like( StringUtil.Lower( A209WWPFormTitle) , StringUtil.PadR( "%" + StringUtil.Lower( AV21FilterFullText) , 255 , "%"),  ' ' ) ) || ( StringUtil.Like( StringUtil.Lower( A208WWPFormReferenceName) , StringUtil.PadR( "%" + StringUtil.Lower( AV21FilterFullText) , 255 , "%"),  ' ' ) ) || ( StringUtil.Like( StringUtil.Str( (decimal)(A207WWPFormVersionNumber), 4, 0) , StringUtil.PadR( "%" + AV21FilterFullText , 254 , "%"),  ' ' ) ) || ( StringUtil.Like( StringUtil.Str( (decimal)(A219WWPFormLatestVersionNumber), 4, 0) , StringUtil.PadR( "%" + AV21FilterFullText , 254 , "%"),  ' ' ) ) ) )
            {
               if ( (0==AV63TFWWPFormLatestVersionNumber) || ( ( A219WWPFormLatestVersionNumber >= AV63TFWWPFormLatestVersionNumber ) ) )
               {
                  if ( (0==AV64TFWWPFormLatestVersionNumber_To) || ( ( A219WWPFormLatestVersionNumber <= AV64TFWWPFormLatestVersionNumber_To ) ) )
                  {
                     GRID_nRecordCount = (long)(GRID_nRecordCount+1);
                  }
               }
            }
            pr_default.readNext(0);
         }
         GRID_nEOF = (short)(((pr_default.getStatus(0) == 101) ? 1 : 0));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
         pr_default.close(0);
         return (int)(GRID_nRecordCount) ;
      }

      protected void RFB92( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridContainer.ClearRows();
         }
         wbStart = 41;
         /* Execute user event: Refresh */
         E23B92 ();
         nGXsfl_41_idx = 1;
         sGXsfl_41_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_41_idx), 4, 0), 4, "0");
         SubsflControlProps_412( ) ;
         bGXsfl_41_Refreshing = true;
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
            SubsflControlProps_412( ) ;
            pr_default.dynParam(1, new Object[]{ new Object[]{
                                                 A206WWPFormId ,
                                                 AV24GeneralDynamicFormids ,
                                                 AV68TFWWPFormTitle_Sel ,
                                                 AV67TFWWPFormTitle ,
                                                 AV66TFWWPFormReferenceName_Sel ,
                                                 AV65TFWWPFormReferenceName ,
                                                 AV61TFWWPFormDate ,
                                                 AV62TFWWPFormDate_To ,
                                                 AV69TFWWPFormVersionNumber ,
                                                 AV70TFWWPFormVersionNumber_To ,
                                                 A209WWPFormTitle ,
                                                 A208WWPFormReferenceName ,
                                                 A231WWPFormDate ,
                                                 A207WWPFormVersionNumber ,
                                                 AV53OrderedBy ,
                                                 AV55OrderedDsc ,
                                                 A240WWPFormType ,
                                                 AV79WWPFormType ,
                                                 AV21FilterFullText ,
                                                 A219WWPFormLatestVersionNumber ,
                                                 AV63TFWWPFormLatestVersionNumber ,
                                                 AV64TFWWPFormLatestVersionNumber_To } ,
                                                 new int[]{
                                                 TypeConstants.SHORT, TypeConstants.DATE, TypeConstants.DATE, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.DATE, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.BOOLEAN, TypeConstants.SHORT,
                                                 TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT
                                                 }
            });
            lV67TFWWPFormTitle = StringUtil.Concat( StringUtil.RTrim( AV67TFWWPFormTitle), "%", "");
            lV65TFWWPFormReferenceName = StringUtil.Concat( StringUtil.RTrim( AV65TFWWPFormReferenceName), "%", "");
            /* Using cursor H00B93 */
            pr_default.execute(1, new Object[] {AV79WWPFormType, lV67TFWWPFormTitle, AV68TFWWPFormTitle_Sel, lV65TFWWPFormReferenceName, AV66TFWWPFormReferenceName_Sel, AV61TFWWPFormDate, AV62TFWWPFormDate_To, AV69TFWWPFormVersionNumber, AV70TFWWPFormVersionNumber_To});
            nGXsfl_41_idx = 1;
            sGXsfl_41_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_41_idx), 4, 0), 4, "0");
            SubsflControlProps_412( ) ;
            GRID_nEOF = 0;
            GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
            while ( ( (pr_default.getStatus(1) != 101) ) && ( ( ( subGrid_Rows == 0 ) || ( GRID_nCurrentRecord < GRID_nFirstRecordOnPage + subGrid_fnc_Recordsperpage( ) ) ) ) )
            {
               A240WWPFormType = H00B93_A240WWPFormType[0];
               AssignAttri(sPrefix, false, "A240WWPFormType", StringUtil.Str( (decimal)(A240WWPFormType), 1, 0));
               GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMTYPE", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(A240WWPFormType), "9"), context));
               A207WWPFormVersionNumber = H00B93_A207WWPFormVersionNumber[0];
               A231WWPFormDate = H00B93_A231WWPFormDate[0];
               A208WWPFormReferenceName = H00B93_A208WWPFormReferenceName[0];
               A209WWPFormTitle = H00B93_A209WWPFormTitle[0];
               A206WWPFormId = H00B93_A206WWPFormId[0];
               GXt_int1 = A219WWPFormLatestVersionNumber;
               new GeneXus.Programs.workwithplus.dynamicforms.wwp_df_getlatestversionofform(context ).execute(  A206WWPFormId, out  GXt_int1) ;
               A219WWPFormLatestVersionNumber = GXt_int1;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV21FilterFullText)) || ( ( StringUtil.Like( StringUtil.Lower( A209WWPFormTitle) , StringUtil.PadR( "%" + StringUtil.Lower( AV21FilterFullText) , 255 , "%"),  ' ' ) ) || ( StringUtil.Like( StringUtil.Lower( A208WWPFormReferenceName) , StringUtil.PadR( "%" + StringUtil.Lower( AV21FilterFullText) , 255 , "%"),  ' ' ) ) || ( StringUtil.Like( StringUtil.Str( (decimal)(A207WWPFormVersionNumber), 4, 0) , StringUtil.PadR( "%" + AV21FilterFullText , 254 , "%"),  ' ' ) ) || ( StringUtil.Like( StringUtil.Str( (decimal)(A219WWPFormLatestVersionNumber), 4, 0) , StringUtil.PadR( "%" + AV21FilterFullText , 254 , "%"),  ' ' ) ) ) )
               {
                  if ( (0==AV63TFWWPFormLatestVersionNumber) || ( ( A219WWPFormLatestVersionNumber >= AV63TFWWPFormLatestVersionNumber ) ) )
                  {
                     if ( (0==AV64TFWWPFormLatestVersionNumber_To) || ( ( A219WWPFormLatestVersionNumber <= AV64TFWWPFormLatestVersionNumber_To ) ) )
                     {
                        /* Execute user event: Grid.Load */
                        E24B92 ();
                     }
                  }
               }
               pr_default.readNext(1);
            }
            GRID_nEOF = (short)(((pr_default.getStatus(1) == 101) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
            pr_default.close(1);
            wbEnd = 41;
            WBB90( ) ;
         }
         bGXsfl_41_Refreshing = true;
      }

      protected void send_integrity_lvl_hashesB92( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"vPGMNAME", StringUtil.RTrim( AV81Pgmname));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPGMNAME", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( AV81Pgmname, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_UUPDATE", AV46IsAuthorized_UUpdate);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UUPDATE", GetSecureSignedToken( sPrefix, AV46IsAuthorized_UUpdate, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_UDELETE", AV42IsAuthorized_UDelete);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UDELETE", GetSecureSignedToken( sPrefix, AV42IsAuthorized_UDelete, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFILLEDOUTFORMS"+"_"+sGXsfl_41_idx, GetSecureSignedToken( sPrefix+sGXsfl_41_idx, context.localUtil.Format( (decimal)(AV20FilledOutForms), "ZZZ9"), context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_DISPLAY", AV35IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( sPrefix, AV35IsAuthorized_Display, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_EXPORTFORM", AV36IsAuthorized_ExportForm);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_EXPORTFORM", GetSecureSignedToken( sPrefix, AV36IsAuthorized_ExportForm, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_FILLOUTAFORM", AV38IsAuthorized_FillOutAForm);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_FILLOUTAFORM", GetSecureSignedToken( sPrefix, AV38IsAuthorized_FillOutAForm, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_FILLEDOUTFORMS", AV37IsAuthorized_FilledOutForms);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_FILLEDOUTFORMS", GetSecureSignedToken( sPrefix, AV37IsAuthorized_FilledOutForms, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_COPY", AV33IsAuthorized_Copy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_COPY", GetSecureSignedToken( sPrefix, AV33IsAuthorized_Copy, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_UCOPYTOLOCATION", AV41IsAuthorized_UCopyToLocation);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UCOPYTOLOCATION", GetSecureSignedToken( sPrefix, AV41IsAuthorized_UCopyToLocation, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_UDIRECTCOPYTOLOCATION", AV43IsAuthorized_UDirectCopyToLocation);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UDIRECTCOPYTOLOCATION", GetSecureSignedToken( sPrefix, AV43IsAuthorized_UDirectCopyToLocation, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vWWPFORMID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV77WWPFormId), 4, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWWPFORMID", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV77WWPFormId), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMID"+"_"+sGXsfl_41_idx, GetSecureSignedToken( sPrefix+sGXsfl_41_idx, context.localUtil.Format( (decimal)(A206WWPFormId), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMTITLE"+"_"+sGXsfl_41_idx, GetSecureSignedToken( sPrefix+sGXsfl_41_idx, StringUtil.RTrim( context.localUtil.Format( A209WWPFormTitle, "")), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMVERSIONNUMBER"+"_"+sGXsfl_41_idx, GetSecureSignedToken( sPrefix+sGXsfl_41_idx, context.localUtil.Format( (decimal)(A207WWPFormVersionNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMREFERENCENAME"+"_"+sGXsfl_41_idx, GetSecureSignedToken( sPrefix+sGXsfl_41_idx, StringUtil.RTrim( context.localUtil.Format( A208WWPFormReferenceName, "")), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vLOCATIONID", AV5LocationId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vLOCATIONID", GetSecureSignedToken( sPrefix, AV5LocationId, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vORGANISATIONID", AV6OrganisationId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vORGANISATIONID", GetSecureSignedToken( sPrefix, AV6OrganisationId, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_UINSERT", AV44IsAuthorized_UInsert);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UINSERT", GetSecureSignedToken( sPrefix, AV44IsAuthorized_UInsert, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_INSERT", AV40IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( sPrefix, AV40IsAuthorized_Insert, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISAUTHORIZED_IMPORTFORM", AV39IsAuthorized_ImportForm);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_IMPORTFORM", GetSecureSignedToken( sPrefix, AV39IsAuthorized_ImportForm, context));
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
            gxgrGrid_refresh( subGrid_Rows, AV53OrderedBy, AV55OrderedDsc, AV21FilterFullText, AV79WWPFormType, AV24GeneralDynamicFormids, AV48ManageFiltersExecutionStep, AV9ColumnsSelector, AV81Pgmname, AV67TFWWPFormTitle, AV68TFWWPFormTitle_Sel, AV65TFWWPFormReferenceName, AV66TFWWPFormReferenceName_Sel, AV61TFWWPFormDate, AV62TFWWPFormDate_To, AV69TFWWPFormVersionNumber, AV70TFWWPFormVersionNumber_To, AV63TFWWPFormLatestVersionNumber, AV64TFWWPFormLatestVersionNumber_To, AV78WWPFormIsForDynamicValidations, AV46IsAuthorized_UUpdate, AV42IsAuthorized_UDelete, AV20FilledOutForms, AV35IsAuthorized_Display, AV36IsAuthorized_ExportForm, AV38IsAuthorized_FillOutAForm, AV37IsAuthorized_FilledOutForms, AV33IsAuthorized_Copy, AV41IsAuthorized_UCopyToLocation, AV43IsAuthorized_UDirectCopyToLocation, AV77WWPFormId, AV5LocationId, AV6OrganisationId, AV44IsAuthorized_UInsert, AV40IsAuthorized_Insert, AV39IsAuthorized_ImportForm, A240WWPFormType, sPrefix) ;
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
            gxgrGrid_refresh( subGrid_Rows, AV53OrderedBy, AV55OrderedDsc, AV21FilterFullText, AV79WWPFormType, AV24GeneralDynamicFormids, AV48ManageFiltersExecutionStep, AV9ColumnsSelector, AV81Pgmname, AV67TFWWPFormTitle, AV68TFWWPFormTitle_Sel, AV65TFWWPFormReferenceName, AV66TFWWPFormReferenceName_Sel, AV61TFWWPFormDate, AV62TFWWPFormDate_To, AV69TFWWPFormVersionNumber, AV70TFWWPFormVersionNumber_To, AV63TFWWPFormLatestVersionNumber, AV64TFWWPFormLatestVersionNumber_To, AV78WWPFormIsForDynamicValidations, AV46IsAuthorized_UUpdate, AV42IsAuthorized_UDelete, AV20FilledOutForms, AV35IsAuthorized_Display, AV36IsAuthorized_ExportForm, AV38IsAuthorized_FillOutAForm, AV37IsAuthorized_FilledOutForms, AV33IsAuthorized_Copy, AV41IsAuthorized_UCopyToLocation, AV43IsAuthorized_UDirectCopyToLocation, AV77WWPFormId, AV5LocationId, AV6OrganisationId, AV44IsAuthorized_UInsert, AV40IsAuthorized_Insert, AV39IsAuthorized_ImportForm, A240WWPFormType, sPrefix) ;
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
            gxgrGrid_refresh( subGrid_Rows, AV53OrderedBy, AV55OrderedDsc, AV21FilterFullText, AV79WWPFormType, AV24GeneralDynamicFormids, AV48ManageFiltersExecutionStep, AV9ColumnsSelector, AV81Pgmname, AV67TFWWPFormTitle, AV68TFWWPFormTitle_Sel, AV65TFWWPFormReferenceName, AV66TFWWPFormReferenceName_Sel, AV61TFWWPFormDate, AV62TFWWPFormDate_To, AV69TFWWPFormVersionNumber, AV70TFWWPFormVersionNumber_To, AV63TFWWPFormLatestVersionNumber, AV64TFWWPFormLatestVersionNumber_To, AV78WWPFormIsForDynamicValidations, AV46IsAuthorized_UUpdate, AV42IsAuthorized_UDelete, AV20FilledOutForms, AV35IsAuthorized_Display, AV36IsAuthorized_ExportForm, AV38IsAuthorized_FillOutAForm, AV37IsAuthorized_FilledOutForms, AV33IsAuthorized_Copy, AV41IsAuthorized_UCopyToLocation, AV43IsAuthorized_UDirectCopyToLocation, AV77WWPFormId, AV5LocationId, AV6OrganisationId, AV44IsAuthorized_UInsert, AV40IsAuthorized_Insert, AV39IsAuthorized_ImportForm, A240WWPFormType, sPrefix) ;
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
            gxgrGrid_refresh( subGrid_Rows, AV53OrderedBy, AV55OrderedDsc, AV21FilterFullText, AV79WWPFormType, AV24GeneralDynamicFormids, AV48ManageFiltersExecutionStep, AV9ColumnsSelector, AV81Pgmname, AV67TFWWPFormTitle, AV68TFWWPFormTitle_Sel, AV65TFWWPFormReferenceName, AV66TFWWPFormReferenceName_Sel, AV61TFWWPFormDate, AV62TFWWPFormDate_To, AV69TFWWPFormVersionNumber, AV70TFWWPFormVersionNumber_To, AV63TFWWPFormLatestVersionNumber, AV64TFWWPFormLatestVersionNumber_To, AV78WWPFormIsForDynamicValidations, AV46IsAuthorized_UUpdate, AV42IsAuthorized_UDelete, AV20FilledOutForms, AV35IsAuthorized_Display, AV36IsAuthorized_ExportForm, AV38IsAuthorized_FillOutAForm, AV37IsAuthorized_FilledOutForms, AV33IsAuthorized_Copy, AV41IsAuthorized_UCopyToLocation, AV43IsAuthorized_UDirectCopyToLocation, AV77WWPFormId, AV5LocationId, AV6OrganisationId, AV44IsAuthorized_UInsert, AV40IsAuthorized_Insert, AV39IsAuthorized_ImportForm, A240WWPFormType, sPrefix) ;
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
            gxgrGrid_refresh( subGrid_Rows, AV53OrderedBy, AV55OrderedDsc, AV21FilterFullText, AV79WWPFormType, AV24GeneralDynamicFormids, AV48ManageFiltersExecutionStep, AV9ColumnsSelector, AV81Pgmname, AV67TFWWPFormTitle, AV68TFWWPFormTitle_Sel, AV65TFWWPFormReferenceName, AV66TFWWPFormReferenceName_Sel, AV61TFWWPFormDate, AV62TFWWPFormDate_To, AV69TFWWPFormVersionNumber, AV70TFWWPFormVersionNumber_To, AV63TFWWPFormLatestVersionNumber, AV64TFWWPFormLatestVersionNumber_To, AV78WWPFormIsForDynamicValidations, AV46IsAuthorized_UUpdate, AV42IsAuthorized_UDelete, AV20FilledOutForms, AV35IsAuthorized_Display, AV36IsAuthorized_ExportForm, AV38IsAuthorized_FillOutAForm, AV37IsAuthorized_FilledOutForms, AV33IsAuthorized_Copy, AV41IsAuthorized_UCopyToLocation, AV43IsAuthorized_UDirectCopyToLocation, AV77WWPFormId, AV5LocationId, AV6OrganisationId, AV44IsAuthorized_UInsert, AV40IsAuthorized_Insert, AV39IsAuthorized_ImportForm, A240WWPFormType, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         AV81Pgmname = "WP_DynamicFormElementUFormWC";
         edtavFilledoutforms_Enabled = 0;
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

      protected void STRUPB90( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E22B92 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vMANAGEFILTERSDATA"), AV47ManageFiltersData);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vDDO_TITLESETTINGSICONS"), AV13DDO_TitleSettingsIcons);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vCOLUMNSSELECTOR"), AV9ColumnsSelector);
            /* Read saved values. */
            nRC_GXsfl_41 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_41"), ".", ","), 18, MidpointRounding.ToEven));
            AV27GridCurrentPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"vGRIDCURRENTPAGE"), ".", ","), 18, MidpointRounding.ToEven));
            AV28GridPageCount = (long)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"vGRIDPAGECOUNT"), ".", ","), 18, MidpointRounding.ToEven));
            AV26GridAppliedFilters = cgiGet( sPrefix+"vGRIDAPPLIEDFILTERS");
            AV14DDO_WWPFormDateAuxDate = context.localUtil.CToD( cgiGet( sPrefix+"vDDO_WWPFORMDATEAUXDATE"), 0);
            AV16DDO_WWPFormDateAuxDateTo = context.localUtil.CToD( cgiGet( sPrefix+"vDDO_WWPFORMDATEAUXDATETO"), 0);
            wcpOAV79WWPFormType = (short)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"wcpOAV79WWPFormType"), ".", ","), 18, MidpointRounding.ToEven));
            wcpOAV78WWPFormIsForDynamicValidations = StringUtil.StrToBool( cgiGet( sPrefix+"wcpOAV78WWPFormIsForDynamicValidations"));
            GRID_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRID_nFirstRecordOnPage"), ".", ","), 18, MidpointRounding.ToEven));
            GRID_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRID_nEOF"), ".", ","), 18, MidpointRounding.ToEven));
            subGrid_Rows = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRID_Rows"), ".", ","), 18, MidpointRounding.ToEven));
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
            Gridpaginationbar_Pagestoshow = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRIDPAGINATIONBAR_Pagestoshow"), ".", ","), 18, MidpointRounding.ToEven));
            Gridpaginationbar_Pagingbuttonsposition = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Pagingbuttonsposition");
            Gridpaginationbar_Pagingcaptionposition = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Pagingcaptionposition");
            Gridpaginationbar_Emptygridclass = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Emptygridclass");
            Gridpaginationbar_Rowsperpageselector = StringUtil.StrToBool( cgiGet( sPrefix+"GRIDPAGINATIONBAR_Rowsperpageselector"));
            Gridpaginationbar_Rowsperpageselectedvalue = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRIDPAGINATIONBAR_Rowsperpageselectedvalue"), ".", ","), 18, MidpointRounding.ToEven));
            Gridpaginationbar_Rowsperpageoptions = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Rowsperpageoptions");
            Gridpaginationbar_Previous = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Previous");
            Gridpaginationbar_Next = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Next");
            Gridpaginationbar_Caption = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Caption");
            Gridpaginationbar_Emptygridcaption = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Emptygridcaption");
            Gridpaginationbar_Rowsperpagecaption = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Rowsperpagecaption");
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
            Dvelop_confirmpanel_udelete_Title = cgiGet( sPrefix+"DVELOP_CONFIRMPANEL_UDELETE_Title");
            Dvelop_confirmpanel_udelete_Confirmationtext = cgiGet( sPrefix+"DVELOP_CONFIRMPANEL_UDELETE_Confirmationtext");
            Dvelop_confirmpanel_udelete_Yesbuttoncaption = cgiGet( sPrefix+"DVELOP_CONFIRMPANEL_UDELETE_Yesbuttoncaption");
            Dvelop_confirmpanel_udelete_Nobuttoncaption = cgiGet( sPrefix+"DVELOP_CONFIRMPANEL_UDELETE_Nobuttoncaption");
            Dvelop_confirmpanel_udelete_Cancelbuttoncaption = cgiGet( sPrefix+"DVELOP_CONFIRMPANEL_UDELETE_Cancelbuttoncaption");
            Dvelop_confirmpanel_udelete_Yesbuttonposition = cgiGet( sPrefix+"DVELOP_CONFIRMPANEL_UDELETE_Yesbuttonposition");
            Dvelop_confirmpanel_udelete_Confirmtype = cgiGet( sPrefix+"DVELOP_CONFIRMPANEL_UDELETE_Confirmtype");
            Ucopytolocation_modal_Width = cgiGet( sPrefix+"UCOPYTOLOCATION_MODAL_Width");
            Ucopytolocation_modal_Title = cgiGet( sPrefix+"UCOPYTOLOCATION_MODAL_Title");
            Ucopytolocation_modal_Confirmtype = cgiGet( sPrefix+"UCOPYTOLOCATION_MODAL_Confirmtype");
            Ucopytolocation_modal_Bodytype = cgiGet( sPrefix+"UCOPYTOLOCATION_MODAL_Bodytype");
            Importform_modal_Width = cgiGet( sPrefix+"IMPORTFORM_MODAL_Width");
            Importform_modal_Title = cgiGet( sPrefix+"IMPORTFORM_MODAL_Title");
            Importform_modal_Confirmtype = cgiGet( sPrefix+"IMPORTFORM_MODAL_Confirmtype");
            Importform_modal_Bodytype = cgiGet( sPrefix+"IMPORTFORM_MODAL_Bodytype");
            Grid_empowerer_Gridinternalname = cgiGet( sPrefix+"GRID_EMPOWERER_Gridinternalname");
            Grid_empowerer_Hastitlesettings = StringUtil.StrToBool( cgiGet( sPrefix+"GRID_EMPOWERER_Hastitlesettings"));
            Grid_empowerer_Hascolumnsselector = StringUtil.StrToBool( cgiGet( sPrefix+"GRID_EMPOWERER_Hascolumnsselector"));
            subGrid_Rows = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRID_Rows"), ".", ","), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
            Gridpaginationbar_Selectedpage = cgiGet( sPrefix+"GRIDPAGINATIONBAR_Selectedpage");
            Gridpaginationbar_Rowsperpageselectedvalue = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRIDPAGINATIONBAR_Rowsperpageselectedvalue"), ".", ","), 18, MidpointRounding.ToEven));
            Ddo_grid_Activeeventkey = cgiGet( sPrefix+"DDO_GRID_Activeeventkey");
            Ddo_grid_Selectedvalue_get = cgiGet( sPrefix+"DDO_GRID_Selectedvalue_get");
            Ddo_grid_Selectedcolumn = cgiGet( sPrefix+"DDO_GRID_Selectedcolumn");
            Ddo_grid_Filteredtext_get = cgiGet( sPrefix+"DDO_GRID_Filteredtext_get");
            Ddo_grid_Filteredtextto_get = cgiGet( sPrefix+"DDO_GRID_Filteredtextto_get");
            Ddo_gridcolumnsselector_Columnsselectorvalues = cgiGet( sPrefix+"DDO_GRIDCOLUMNSSELECTOR_Columnsselectorvalues");
            Ddo_managefilters_Activeeventkey = cgiGet( sPrefix+"DDO_MANAGEFILTERS_Activeeventkey");
            subGrid_Rows = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRID_Rows"), ".", ","), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
            Dvelop_confirmpanel_udelete_Result = cgiGet( sPrefix+"DVELOP_CONFIRMPANEL_UDELETE_Result");
            Ucopytolocation_modal_Result = cgiGet( sPrefix+"UCOPYTOLOCATION_MODAL_Result");
            /* Read variables values. */
            AV21FilterFullText = cgiGet( edtavFilterfulltext_Internalname);
            AssignAttri(sPrefix, false, "AV21FilterFullText", AV21FilterFullText);
            cmbWWPFormType.Name = cmbWWPFormType_Internalname;
            cmbWWPFormType.CurrentValue = cgiGet( cmbWWPFormType_Internalname);
            A240WWPFormType = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbWWPFormType_Internalname), "."), 18, MidpointRounding.ToEven));
            AssignAttri(sPrefix, false, "A240WWPFormType", StringUtil.Str( (decimal)(A240WWPFormType), 1, 0));
            GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMTYPE", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(A240WWPFormType), "9"), context));
            AV15DDO_WWPFormDateAuxDateText = cgiGet( edtavDdo_wwpformdateauxdatetext_Internalname);
            AssignAttri(sPrefix, false, "AV15DDO_WWPFormDateAuxDateText", AV15DDO_WWPFormDateAuxDateText);
            /* Read subfile selected row values. */
            nGXsfl_41_idx = (int)(Math.Round(context.localUtil.CToN( cgiGet( subGrid_Internalname+"_ROW"), ".", ","), 18, MidpointRounding.ToEven));
            sGXsfl_41_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_41_idx), 4, 0), 4, "0");
            SubsflControlProps_412( ) ;
            if ( nGXsfl_41_idx > 0 )
            {
               A206WWPFormId = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtWWPFormId_Internalname), ".", ","), 18, MidpointRounding.ToEven));
               A209WWPFormTitle = cgiGet( edtWWPFormTitle_Internalname);
               A208WWPFormReferenceName = cgiGet( edtWWPFormReferenceName_Internalname);
               A231WWPFormDate = context.localUtil.CToT( cgiGet( edtWWPFormDate_Internalname));
               A207WWPFormVersionNumber = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtWWPFormVersionNumber_Internalname), ".", ","), 18, MidpointRounding.ToEven));
               A219WWPFormLatestVersionNumber = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtWWPFormLatestVersionNumber_Internalname), ".", ","), 18, MidpointRounding.ToEven));
               if ( ( ( context.localUtil.CToN( cgiGet( edtavFilledoutforms_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavFilledoutforms_Internalname), ".", ",") > Convert.ToDecimal( 9999 )) ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vFILLEDOUTFORMS");
                  GX_FocusControl = edtavFilledoutforms_Internalname;
                  AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                  wbErr = true;
                  AV20FilledOutForms = 0;
                  AssignAttri(sPrefix, false, edtavFilledoutforms_Internalname, StringUtil.LTrimStr( (decimal)(AV20FilledOutForms), 4, 0));
                  GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFILLEDOUTFORMS"+"_"+sGXsfl_41_idx, GetSecureSignedToken( sPrefix+sGXsfl_41_idx, context.localUtil.Format( (decimal)(AV20FilledOutForms), "ZZZ9"), context));
               }
               else
               {
                  AV20FilledOutForms = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavFilledoutforms_Internalname), ".", ","), 18, MidpointRounding.ToEven));
                  AssignAttri(sPrefix, false, edtavFilledoutforms_Internalname, StringUtil.LTrimStr( (decimal)(AV20FilledOutForms), 4, 0));
                  GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFILLEDOUTFORMS"+"_"+sGXsfl_41_idx, GetSecureSignedToken( sPrefix+sGXsfl_41_idx, context.localUtil.Format( (decimal)(AV20FilledOutForms), "ZZZ9"), context));
               }
               cmbavGridactiongroup1.Name = cmbavGridactiongroup1_Internalname;
               cmbavGridactiongroup1.CurrentValue = cgiGet( cmbavGridactiongroup1_Internalname);
               AV25GridActionGroup1 = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbavGridactiongroup1_Internalname), "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, cmbavGridactiongroup1_Internalname, StringUtil.LTrimStr( (decimal)(AV25GridActionGroup1), 4, 0));
            }
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            forbiddenHiddens = new GXProperties();
            forbiddenHiddens.Add("hshsalt", sPrefix+"hsh"+"WP_DynamicFormElementUFormWC");
            A240WWPFormType = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbWWPFormType_Internalname), "."), 18, MidpointRounding.ToEven));
            AssignAttri(sPrefix, false, "A240WWPFormType", StringUtil.Str( (decimal)(A240WWPFormType), 1, 0));
            GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_WWPFORMTYPE", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(A240WWPFormType), "9"), context));
            forbiddenHiddens.Add("WWPFormType", context.localUtil.Format( (decimal)(A240WWPFormType), "9"));
            hsh = cgiGet( sPrefix+"hsh");
            if ( ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
            {
               GXUtil.WriteLogError("wp_dynamicformelementuformwc:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
               GxWebError = 1;
               context.HttpContext.Response.StatusCode = 403;
               context.WriteHtmlText( "<title>403 Forbidden</title>") ;
               context.WriteHtmlText( "<h1>403 Forbidden</h1>") ;
               context.WriteHtmlText( "<p /><hr />") ;
               GXUtil.WriteLog("send_http_error_code " + 403.ToString());
               return  ;
            }
            /* Check if conditions changed and reset current page numbers */
            if ( ( context.localUtil.CToN( cgiGet( sPrefix+"GXH_vORDEREDBY"), ".", ",") != Convert.ToDecimal( AV53OrderedBy )) )
            {
               GRID_nFirstRecordOnPage = 0;
            }
            if ( StringUtil.StrToBool( cgiGet( sPrefix+"GXH_vORDEREDDSC")) != AV55OrderedDsc )
            {
               GRID_nFirstRecordOnPage = 0;
            }
            if ( StringUtil.StrCmp(cgiGet( sPrefix+"GXH_vFILTERFULLTEXT"), AV21FilterFullText) != 0 )
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
         E22B92 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
      }

      protected void E22B92( )
      {
         /* Start Routine */
         returnInSub = false;
         GXt_guid2 = AV5LocationId;
         new prc_getuserlocationid(context ).execute( out  GXt_guid2) ;
         AV5LocationId = GXt_guid2;
         AssignAttri(sPrefix, false, "AV5LocationId", AV5LocationId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vLOCATIONID", GetSecureSignedToken( sPrefix, AV5LocationId, context));
         GXt_guid2 = AV6OrganisationId;
         new prc_getuserorganisationid(context ).execute( out  GXt_guid2) ;
         AV6OrganisationId = GXt_guid2;
         AssignAttri(sPrefix, false, "AV6OrganisationId", AV6OrganisationId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vORGANISATIONID", GetSecureSignedToken( sPrefix, AV6OrganisationId, context));
         GXt_objcol_int3 = AV24GeneralDynamicFormids;
         new prc_generaldynamicform(context ).execute( out  GXt_objcol_int3) ;
         AV24GeneralDynamicFormids = GXt_objcol_int3;
         this.executeUsercontrolMethod(sPrefix, false, "TFWWPFORMDATE_RANGEPICKERContainer", "Attach", "", new Object[] {(string)edtavDdo_wwpformdateauxdatetext_Internalname});
         subGrid_Rows = 10;
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         Grid_empowerer_Gridinternalname = subGrid_Internalname;
         ucGrid_empowerer.SendProperty(context, sPrefix, false, Grid_empowerer_Internalname, "GridInternalName", Grid_empowerer_Gridinternalname);
         Ddo_gridcolumnsselector_Gridinternalname = subGrid_Internalname;
         ucDdo_gridcolumnsselector.SendProperty(context, sPrefix, false, Ddo_gridcolumnsselector_Internalname, "GridInternalName", Ddo_gridcolumnsselector_Gridinternalname);
         /* Execute user subroutine: 'LOADSAVEDFILTERS' */
         S112 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         AV23GAMSession = new GeneXus.Programs.genexussecurity.SdtGAMSession(context).get(out  AV22GAMErrors);
         Ddo_grid_Gridinternalname = subGrid_Internalname;
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "GridInternalName", Ddo_grid_Gridinternalname);
         Ddo_grid_Gamoauthtoken = AV23GAMSession.gxTpr_Token;
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "GAMOAuthToken", Ddo_grid_Gamoauthtoken);
         cmbWWPFormType.Visible = 0;
         AssignProp(sPrefix, false, cmbWWPFormType_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbWWPFormType.Visible), 5, 0), true);
         /* Execute user subroutine: 'PREPARETRANSACTION' */
         S122 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         /* Execute user subroutine: 'LOADGRIDSTATE' */
         S132 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         if ( AV53OrderedBy < 1 )
         {
            AV53OrderedBy = 1;
            AssignAttri(sPrefix, false, "AV53OrderedBy", StringUtil.LTrimStr( (decimal)(AV53OrderedBy), 4, 0));
            /* Execute user subroutine: 'SETDDOSORTEDSTATUS' */
            S142 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons4 = AV13DDO_TitleSettingsIcons;
         new GeneXus.Programs.wwpbaseobjects.getwwptitlesettingsicons(context ).execute( out  GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons4) ;
         AV13DDO_TitleSettingsIcons = GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons4;
         Ddo_gridcolumnsselector_Titlecontrolidtoreplace = bttBtneditcolumns_Internalname;
         ucDdo_gridcolumnsselector.SendProperty(context, sPrefix, false, Ddo_gridcolumnsselector_Internalname, "TitleControlIdToReplace", Ddo_gridcolumnsselector_Titlecontrolidtoreplace);
         Gridpaginationbar_Rowsperpageselectedvalue = subGrid_Rows;
         ucGridpaginationbar.SendProperty(context, sPrefix, false, Gridpaginationbar_Internalname, "RowsPerPageSelectedValue", StringUtil.LTrimStr( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0));
      }

      protected void E23B92( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         new GeneXus.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV75WWPContext) ;
         /* Execute user subroutine: 'CHECKSECURITYFORACTIONS' */
         S152 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         if ( AV48ManageFiltersExecutionStep == 1 )
         {
            AV48ManageFiltersExecutionStep = 2;
            AssignAttri(sPrefix, false, "AV48ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV48ManageFiltersExecutionStep), 1, 0));
         }
         else if ( AV48ManageFiltersExecutionStep == 2 )
         {
            AV48ManageFiltersExecutionStep = 0;
            AssignAttri(sPrefix, false, "AV48ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV48ManageFiltersExecutionStep), 1, 0));
            /* Execute user subroutine: 'LOADSAVEDFILTERS' */
            S112 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         /* Execute user subroutine: 'SAVEGRIDSTATE' */
         S162 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         if ( StringUtil.StrCmp(AV60Session.Get("WP_DynamicFormElementUFormWCColumnsSelector"), "") != 0 )
         {
            AV11ColumnsSelectorXML = AV60Session.Get("WP_DynamicFormElementUFormWCColumnsSelector");
            AV9ColumnsSelector.FromXml(AV11ColumnsSelectorXML, null, "", "");
         }
         else
         {
            /* Execute user subroutine: 'INITIALIZECOLUMNSSELECTOR' */
            S172 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         edtWWPFormTitle_Visible = (((WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector_Column)AV9ColumnsSelector.gxTpr_Columns.Item(1)).gxTpr_Isvisible ? 1 : 0);
         AssignProp(sPrefix, false, edtWWPFormTitle_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtWWPFormTitle_Visible), 5, 0), !bGXsfl_41_Refreshing);
         edtWWPFormReferenceName_Visible = (((WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector_Column)AV9ColumnsSelector.gxTpr_Columns.Item(2)).gxTpr_Isvisible ? 1 : 0);
         AssignProp(sPrefix, false, edtWWPFormReferenceName_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtWWPFormReferenceName_Visible), 5, 0), !bGXsfl_41_Refreshing);
         edtWWPFormDate_Visible = (((WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector_Column)AV9ColumnsSelector.gxTpr_Columns.Item(3)).gxTpr_Isvisible ? 1 : 0);
         AssignProp(sPrefix, false, edtWWPFormDate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtWWPFormDate_Visible), 5, 0), !bGXsfl_41_Refreshing);
         edtWWPFormVersionNumber_Visible = (((WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector_Column)AV9ColumnsSelector.gxTpr_Columns.Item(4)).gxTpr_Isvisible ? 1 : 0);
         AssignProp(sPrefix, false, edtWWPFormVersionNumber_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtWWPFormVersionNumber_Visible), 5, 0), !bGXsfl_41_Refreshing);
         edtWWPFormLatestVersionNumber_Visible = (((WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector_Column)AV9ColumnsSelector.gxTpr_Columns.Item(5)).gxTpr_Isvisible ? 1 : 0);
         AssignProp(sPrefix, false, edtWWPFormLatestVersionNumber_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtWWPFormLatestVersionNumber_Visible), 5, 0), !bGXsfl_41_Refreshing);
         AV27GridCurrentPage = subGrid_fnc_Currentpage( );
         AssignAttri(sPrefix, false, "AV27GridCurrentPage", StringUtil.LTrimStr( (decimal)(AV27GridCurrentPage), 10, 0));
         AV28GridPageCount = subGrid_fnc_Pagecount( );
         AssignAttri(sPrefix, false, "AV28GridPageCount", StringUtil.LTrimStr( (decimal)(AV28GridPageCount), 10, 0));
         GXt_char5 = AV26GridAppliedFilters;
         new WorkWithPlus.workwithplus_web.wwp_getappliedfiltersdescription(context ).execute(  AV81Pgmname, out  GXt_char5) ;
         AV26GridAppliedFilters = GXt_char5;
         AssignAttri(sPrefix, false, "AV26GridAppliedFilters", AV26GridAppliedFilters);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV9ColumnsSelector", AV9ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV47ManageFiltersData", AV47ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV29GridState", AV29GridState);
      }

      protected void E12B92( )
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
            AV57PageToGo = (int)(Math.Round(NumberUtil.Val( Gridpaginationbar_Selectedpage, "."), 18, MidpointRounding.ToEven));
            subgrid_gotopage( AV57PageToGo) ;
         }
      }

      protected void E13B92( )
      {
         /* Gridpaginationbar_Changerowsperpage Routine */
         returnInSub = false;
         subGrid_Rows = Gridpaginationbar_Rowsperpageselectedvalue;
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         subgrid_firstpage( ) ;
         /*  Sending Event outputs  */
      }

      protected void E14B92( )
      {
         /* Ddo_grid_Onoptionclicked Routine */
         returnInSub = false;
         if ( ( StringUtil.StrCmp(Ddo_grid_Activeeventkey, "<#OrderASC#>") == 0 ) || ( StringUtil.StrCmp(Ddo_grid_Activeeventkey, "<#OrderDSC#>") == 0 ) )
         {
            AV53OrderedBy = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Selectedvalue_get, "."), 18, MidpointRounding.ToEven));
            AssignAttri(sPrefix, false, "AV53OrderedBy", StringUtil.LTrimStr( (decimal)(AV53OrderedBy), 4, 0));
            AV55OrderedDsc = ((StringUtil.StrCmp(Ddo_grid_Activeeventkey, "<#OrderDSC#>")==0) ? true : false);
            AssignAttri(sPrefix, false, "AV55OrderedDsc", AV55OrderedDsc);
            /* Execute user subroutine: 'SETDDOSORTEDSTATUS' */
            S142 ();
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
               AV67TFWWPFormTitle = Ddo_grid_Filteredtext_get;
               AssignAttri(sPrefix, false, "AV67TFWWPFormTitle", AV67TFWWPFormTitle);
               AV68TFWWPFormTitle_Sel = Ddo_grid_Selectedvalue_get;
               AssignAttri(sPrefix, false, "AV68TFWWPFormTitle_Sel", AV68TFWWPFormTitle_Sel);
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "WWPFormReferenceName") == 0 )
            {
               AV65TFWWPFormReferenceName = Ddo_grid_Filteredtext_get;
               AssignAttri(sPrefix, false, "AV65TFWWPFormReferenceName", AV65TFWWPFormReferenceName);
               AV66TFWWPFormReferenceName_Sel = Ddo_grid_Selectedvalue_get;
               AssignAttri(sPrefix, false, "AV66TFWWPFormReferenceName_Sel", AV66TFWWPFormReferenceName_Sel);
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "WWPFormDate") == 0 )
            {
               AV61TFWWPFormDate = context.localUtil.CToT( Ddo_grid_Filteredtext_get, 2);
               AssignAttri(sPrefix, false, "AV61TFWWPFormDate", context.localUtil.TToC( AV61TFWWPFormDate, 8, 5, 0, 3, "/", ":", " "));
               AV62TFWWPFormDate_To = context.localUtil.CToT( Ddo_grid_Filteredtextto_get, 2);
               AssignAttri(sPrefix, false, "AV62TFWWPFormDate_To", context.localUtil.TToC( AV62TFWWPFormDate_To, 8, 5, 0, 3, "/", ":", " "));
               if ( ! (DateTime.MinValue==AV62TFWWPFormDate_To) )
               {
                  AV62TFWWPFormDate_To = context.localUtil.YMDHMSToT( (short)(DateTimeUtil.Year( AV62TFWWPFormDate_To)), (short)(DateTimeUtil.Month( AV62TFWWPFormDate_To)), (short)(DateTimeUtil.Day( AV62TFWWPFormDate_To)), 23, 59, 59);
                  AssignAttri(sPrefix, false, "AV62TFWWPFormDate_To", context.localUtil.TToC( AV62TFWWPFormDate_To, 8, 5, 0, 3, "/", ":", " "));
               }
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "WWPFormVersionNumber") == 0 )
            {
               AV69TFWWPFormVersionNumber = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtext_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV69TFWWPFormVersionNumber", StringUtil.LTrimStr( (decimal)(AV69TFWWPFormVersionNumber), 4, 0));
               AV70TFWWPFormVersionNumber_To = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtextto_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV70TFWWPFormVersionNumber_To", StringUtil.LTrimStr( (decimal)(AV70TFWWPFormVersionNumber_To), 4, 0));
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "WWPFormLatestVersionNumber") == 0 )
            {
               AV63TFWWPFormLatestVersionNumber = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtext_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV63TFWWPFormLatestVersionNumber", StringUtil.LTrimStr( (decimal)(AV63TFWWPFormLatestVersionNumber), 4, 0));
               AV64TFWWPFormLatestVersionNumber_To = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtextto_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV64TFWWPFormLatestVersionNumber_To", StringUtil.LTrimStr( (decimal)(AV64TFWWPFormLatestVersionNumber_To), 4, 0));
            }
            subgrid_firstpage( ) ;
         }
         /*  Sending Event outputs  */
      }

      private void E24B92( )
      {
         if ( ( subGrid_Islastpage == 1 ) || ( subGrid_Rows == 0 ) || ( ( GRID_nCurrentRecord >= GRID_nFirstRecordOnPage ) && ( GRID_nCurrentRecord < GRID_nFirstRecordOnPage + subGrid_fnc_Recordsperpage( ) ) ) )
         {
            /* Grid_Load Routine */
            returnInSub = false;
            if ( AV79WWPFormType == 0 )
            {
               AV77WWPFormId = A206WWPFormId;
               AssignAttri(sPrefix, false, "AV77WWPFormId", StringUtil.LTrimStr( (decimal)(AV77WWPFormId), 4, 0));
               GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWWPFORMID", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV77WWPFormId), "ZZZ9"), context));
               /* Execute user subroutine: 'COUNTFILLEDOUTFORMS' */
               S182 ();
               if ( returnInSub )
               {
                  returnInSub = true;
                  if (true) return;
               }
            }
            cmbavGridactiongroup1.removeAllItems();
            cmbavGridactiongroup1.addItem("0", ";fas fa-bars", 0);
            if ( AV46IsAuthorized_UUpdate )
            {
               cmbavGridactiongroup1.addItem("1", StringUtil.Format( "%1;%2", "Update", "fas fa-pen", "", "", "", "", "", "", ""), 0);
            }
            if ( AV42IsAuthorized_UDelete )
            {
               if ( ( AV20FilledOutForms == 0 ) && ( AV79WWPFormType == 0 ) )
               {
                  cmbavGridactiongroup1.addItem("2", StringUtil.Format( "%1;%2", "Delete", "fas fa-xmark", "", "", "", "", "", "", ""), 0);
               }
            }
            if ( AV35IsAuthorized_Display )
            {
               cmbavGridactiongroup1.addItem("3", StringUtil.Format( "%1;%2", "Display", "fa fa-search", "", "", "", "", "", "", ""), 0);
            }
            if ( AV36IsAuthorized_ExportForm )
            {
               cmbavGridactiongroup1.addItem("4", StringUtil.Format( "%1;%2", "Export Form", "fas fa-file-export", "", "", "", "", "", "", ""), 0);
            }
            if ( AV38IsAuthorized_FillOutAForm )
            {
               if ( AV79WWPFormType == 0 )
               {
                  cmbavGridactiongroup1.addItem("5", StringUtil.Format( "%1;%2", "Fill out a form", "fas fa-file-circle-plus", "", "", "", "", "", "", ""), 0);
               }
            }
            if ( AV37IsAuthorized_FilledOutForms )
            {
               if ( ( AV79WWPFormType == 0 ) && ( AV20FilledOutForms > 0 ) )
               {
                  cmbavGridactiongroup1.addItem("6", StringUtil.Format( "%1;%2", "View filled out forms", "fab fa-wpforms", "", "", "", "", "", "", ""), 0);
               }
            }
            if ( AV33IsAuthorized_Copy )
            {
               cmbavGridactiongroup1.addItem("7", StringUtil.Format( "%1;%2", "Copy Form", "far fa-clone", "", "", "", "", "", "", ""), 0);
            }
            if ( AV41IsAuthorized_UCopyToLocation )
            {
               cmbavGridactiongroup1.addItem("8", StringUtil.Format( "%1;%2", "Copy To Location", "fa-copy far", "", "", "", "", "", "", ""), 0);
            }
            if ( AV43IsAuthorized_UDirectCopyToLocation )
            {
               cmbavGridactiongroup1.addItem("9", StringUtil.Format( "%1;%2", "Copy To Location", "fa-copy far", "", "", "", "", "", "", ""), 0);
            }
            if ( cmbavGridactiongroup1.ItemCount == 1 )
            {
               cmbavGridactiongroup1_Class = "Invisible";
            }
            else
            {
               cmbavGridactiongroup1_Class = "ConvertToDDO";
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 41;
            }
            sendrow_412( ) ;
         }
         GRID_nEOF = (short)(((GRID_nCurrentRecord<GRID_nFirstRecordOnPage+subGrid_fnc_Recordsperpage( )) ? 1 : 0));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
         GRID_nCurrentRecord = (long)(GRID_nCurrentRecord+1);
         if ( isFullAjaxMode( ) && ! bGXsfl_41_Refreshing )
         {
            DoAjaxLoad(41, GridRow);
         }
         /*  Sending Event outputs  */
         cmbavGridactiongroup1.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV25GridActionGroup1), 4, 0));
      }

      protected void E15B92( )
      {
         /* Ddo_gridcolumnsselector_Oncolumnschanged Routine */
         returnInSub = false;
         AV11ColumnsSelectorXML = Ddo_gridcolumnsselector_Columnsselectorvalues;
         AV9ColumnsSelector.FromJSonString(AV11ColumnsSelectorXML, null);
         new WorkWithPlus.workwithplus_web.savecolumnsselectorstate(context ).execute(  "WP_DynamicFormElementUFormWCColumnsSelector",  (String.IsNullOrEmpty(StringUtil.RTrim( AV11ColumnsSelectorXML)) ? "" : AV9ColumnsSelector.ToXml(false, true, "", ""))) ;
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV9ColumnsSelector", AV9ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV47ManageFiltersData", AV47ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV29GridState", AV29GridState);
      }

      protected void E11B92( )
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
            S162 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
            context.PopUp(formatLink("wwpbaseobjects.savefilteras.aspx", new object[] {UrlEncode(StringUtil.RTrim("WP_DynamicFormElementUFormWCFilters")),UrlEncode(StringUtil.RTrim(AV81Pgmname+"GridState"))}, new string[] {"UserKey","GridStateKey"}) , new Object[] {});
            AV48ManageFiltersExecutionStep = 2;
            AssignAttri(sPrefix, false, "AV48ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV48ManageFiltersExecutionStep), 1, 0));
            context.DoAjaxRefreshCmp(sPrefix);
         }
         else if ( StringUtil.StrCmp(Ddo_managefilters_Activeeventkey, "<#Manage#>") == 0 )
         {
            context.PopUp(formatLink("wwpbaseobjects.managefilters.aspx", new object[] {UrlEncode(StringUtil.RTrim("WP_DynamicFormElementUFormWCFilters"))}, new string[] {"UserKey"}) , new Object[] {});
            AV48ManageFiltersExecutionStep = 2;
            AssignAttri(sPrefix, false, "AV48ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV48ManageFiltersExecutionStep), 1, 0));
            context.DoAjaxRefreshCmp(sPrefix);
         }
         else
         {
            GXt_char5 = AV49ManageFiltersXml;
            new WorkWithPlus.workwithplus_web.getfilterbyname(context ).execute(  "WP_DynamicFormElementUFormWCFilters",  Ddo_managefilters_Activeeventkey, out  GXt_char5) ;
            AV49ManageFiltersXml = GXt_char5;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV49ManageFiltersXml)) )
            {
               GX_msglist.addItem("The selected filter no longer exist.");
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
               new WorkWithPlus.workwithplus_web.savegridstate(context ).execute(  AV81Pgmname+"GridState",  AV49ManageFiltersXml) ;
               AV29GridState.FromXml(AV49ManageFiltersXml, null, "", "");
               AV53OrderedBy = AV29GridState.gxTpr_Orderedby;
               AssignAttri(sPrefix, false, "AV53OrderedBy", StringUtil.LTrimStr( (decimal)(AV53OrderedBy), 4, 0));
               AV55OrderedDsc = AV29GridState.gxTpr_Ordereddsc;
               AssignAttri(sPrefix, false, "AV55OrderedDsc", AV55OrderedDsc);
               /* Execute user subroutine: 'SETDDOSORTEDSTATUS' */
               S142 ();
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
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV29GridState", AV29GridState);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV9ColumnsSelector", AV9ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV47ManageFiltersData", AV47ManageFiltersData);
      }

      protected void E25B92( )
      {
         /* Gridactiongroup1_Click Routine */
         returnInSub = false;
         if ( AV25GridActionGroup1 == 1 )
         {
            /* Execute user subroutine: 'DO UUPDATE' */
            S212 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV25GridActionGroup1 == 2 )
         {
            /* Execute user subroutine: 'DO UDELETE' */
            S222 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV25GridActionGroup1 == 3 )
         {
            /* Execute user subroutine: 'DO DISPLAY' */
            S232 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV25GridActionGroup1 == 4 )
         {
            /* Execute user subroutine: 'DO EXPORTFORM' */
            S242 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV25GridActionGroup1 == 5 )
         {
            /* Execute user subroutine: 'DO FILLOUTAFORM' */
            S252 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV25GridActionGroup1 == 6 )
         {
            /* Execute user subroutine: 'DO FILLEDOUTFORMS' */
            S262 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV25GridActionGroup1 == 7 )
         {
            /* Execute user subroutine: 'DO COPY' */
            S272 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV25GridActionGroup1 == 8 )
         {
            /* Execute user subroutine: 'DO UCOPYTOLOCATION' */
            S282 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV25GridActionGroup1 == 9 )
         {
            /* Execute user subroutine: 'DO UDIRECTCOPYTOLOCATION' */
            S292 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         AV25GridActionGroup1 = 0;
         AssignAttri(sPrefix, false, cmbavGridactiongroup1_Internalname, StringUtil.LTrimStr( (decimal)(AV25GridActionGroup1), 4, 0));
         /*  Sending Event outputs  */
         cmbavGridactiongroup1.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV25GridActionGroup1), 4, 0));
         AssignProp(sPrefix, false, cmbavGridactiongroup1_Internalname, "Values", cmbavGridactiongroup1.ToJavascriptSource(), true);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV9ColumnsSelector", AV9ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV47ManageFiltersData", AV47ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV29GridState", AV29GridState);
      }

      protected void E16B92( )
      {
         /* Dvelop_confirmpanel_udelete_Close Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(Dvelop_confirmpanel_udelete_Result, "Yes") == 0 )
         {
            /* Execute user subroutine: 'DO ACTION UDELETE' */
            S302 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV9ColumnsSelector", AV9ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV47ManageFiltersData", AV47ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV29GridState", AV29GridState);
      }

      protected void E19B92( )
      {
         /* 'DoUInsert' Routine */
         returnInSub = false;
         if ( AV44IsAuthorized_UInsert )
         {
            CallWebObject(formatLink("workwithplus.dynamicforms.wwp_createdynamicform.aspx", new object[] {UrlEncode(StringUtil.LTrimStr(0,1,0)),UrlEncode(StringUtil.RTrim("INS")),UrlEncode(StringUtil.RTrim("")),UrlEncode(StringUtil.LTrimStr(AV79WWPFormType,1,0))}, new string[] {"WWPFormId","WWPDynamicFormMode","DefaultFormType","WWPFormType"}) );
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem("Action no longer available");
            context.DoAjaxRefreshCmp(sPrefix);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV9ColumnsSelector", AV9ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV47ManageFiltersData", AV47ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV29GridState", AV29GridState);
      }

      protected void E20B92( )
      {
         /* 'DoInsert' Routine */
         returnInSub = false;
         if ( AV40IsAuthorized_Insert )
         {
            CallWebObject(formatLink("workwithplus.dynamicforms.wwp_createdynamicform.aspx", new object[] {UrlEncode(StringUtil.LTrimStr(0,1,0)),UrlEncode(StringUtil.RTrim("INS")),UrlEncode(StringUtil.RTrim("")),UrlEncode(StringUtil.LTrimStr(AV79WWPFormType,1,0))}, new string[] {"WWPFormId","WWPDynamicFormMode","DefaultFormType","WWPFormType"}) );
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem("Action no longer available");
            context.DoAjaxRefreshCmp(sPrefix);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV9ColumnsSelector", AV9ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV47ManageFiltersData", AV47ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV29GridState", AV29GridState);
      }

      protected void E21B92( )
      {
         /* 'DoImportForm' Routine */
         returnInSub = false;
         if ( AV39IsAuthorized_ImportForm )
         {
            this.executeUsercontrolMethod(sPrefix, false, "IMPORTFORM_MODALContainer", "Confirm", "", new Object[] {});
         }
         else
         {
            GX_msglist.addItem("Action no longer available");
            context.DoAjaxRefreshCmp(sPrefix);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV9ColumnsSelector", AV9ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV47ManageFiltersData", AV47ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV29GridState", AV29GridState);
      }

      protected void E18B92( )
      {
         /* Importform_modal_Close Routine */
         returnInSub = false;
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV9ColumnsSelector", AV9ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV47ManageFiltersData", AV47ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV29GridState", AV29GridState);
      }

      protected void S142( )
      {
         /* 'SETDDOSORTEDSTATUS' Routine */
         returnInSub = false;
         Ddo_grid_Sortedstatus = StringUtil.Trim( StringUtil.Str( (decimal)(AV53OrderedBy), 4, 0))+":"+(AV55OrderedDsc ? "DSC" : "ASC");
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "SortedStatus", Ddo_grid_Sortedstatus);
      }

      protected void S172( )
      {
         /* 'INITIALIZECOLUMNSSELECTOR' Routine */
         returnInSub = false;
         AV9ColumnsSelector = new WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector(context);
         if ( AV79WWPFormType == 0 )
         {
            new WorkWithPlus.workwithplus_web.wwp_columnsselector_add(context ).execute( ref  AV9ColumnsSelector,  "WWPFormTitle",  "",  "WWP_DF_Title",  true,  "") ;
         }
         else
         {
            new WorkWithPlus.workwithplus_web.wwp_columnsselector_add(context ).execute( ref  AV9ColumnsSelector,  "",  "",  "",  false,  "") ;
            AV67TFWWPFormTitle = "";
            AssignAttri(sPrefix, false, "AV67TFWWPFormTitle", AV67TFWWPFormTitle);
            AV68TFWWPFormTitle_Sel = "";
            AssignAttri(sPrefix, false, "AV68TFWWPFormTitle_Sel", AV68TFWWPFormTitle_Sel);
         }
         new WorkWithPlus.workwithplus_web.wwp_columnsselector_add(context ).execute( ref  AV9ColumnsSelector,  "WWPFormReferenceName",  "",  "WWP_DF_ReferenceName",  true,  "") ;
         new WorkWithPlus.workwithplus_web.wwp_columnsselector_add(context ).execute( ref  AV9ColumnsSelector,  "WWPFormDate",  "",  "Date",  true,  "") ;
         new WorkWithPlus.workwithplus_web.wwp_columnsselector_add(context ).execute( ref  AV9ColumnsSelector,  "WWPFormVersionNumber",  "",  "WWP_DF_FormVersion",  true,  "") ;
         new WorkWithPlus.workwithplus_web.wwp_columnsselector_add(context ).execute( ref  AV9ColumnsSelector,  "WWPFormLatestVersionNumber",  "",  "Latest Version",  true,  "") ;
         GXt_char5 = AV74UserCustomValue;
         new WorkWithPlus.workwithplus_web.loadcolumnsselectorstate(context ).execute(  "WP_DynamicFormElementUFormWCColumnsSelector", out  GXt_char5) ;
         AV74UserCustomValue = GXt_char5;
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( AV74UserCustomValue)) ) )
         {
            AV10ColumnsSelectorAux.FromXml(AV74UserCustomValue, null, "", "");
            new WorkWithPlus.workwithplus_web.wwp_columnselector_updatecolumns(context ).execute( ref  AV10ColumnsSelectorAux, ref  AV9ColumnsSelector) ;
         }
      }

      protected void S152( )
      {
         /* 'CHECKSECURITYFORACTIONS' Routine */
         returnInSub = false;
         GXt_boolean6 = AV46IsAuthorized_UUpdate;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "uform_uupdate", out  GXt_boolean6) ;
         AV46IsAuthorized_UUpdate = GXt_boolean6;
         AssignAttri(sPrefix, false, "AV46IsAuthorized_UUpdate", AV46IsAuthorized_UUpdate);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UUPDATE", GetSecureSignedToken( sPrefix, AV46IsAuthorized_UUpdate, context));
         GXt_boolean6 = AV42IsAuthorized_UDelete;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "uform_udelete", out  GXt_boolean6) ;
         AV42IsAuthorized_UDelete = GXt_boolean6;
         AssignAttri(sPrefix, false, "AV42IsAuthorized_UDelete", AV42IsAuthorized_UDelete);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UDELETE", GetSecureSignedToken( sPrefix, AV42IsAuthorized_UDelete, context));
         GXt_boolean6 = AV35IsAuthorized_Display;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "wwp_createdynamicform_Execute", out  GXt_boolean6) ;
         AV35IsAuthorized_Display = GXt_boolean6;
         AssignAttri(sPrefix, false, "AV35IsAuthorized_Display", AV35IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( sPrefix, AV35IsAuthorized_Display, context));
         GXt_boolean6 = AV36IsAuthorized_ExportForm;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "uform_uexport", out  GXt_boolean6) ;
         AV36IsAuthorized_ExportForm = GXt_boolean6;
         AssignAttri(sPrefix, false, "AV36IsAuthorized_ExportForm", AV36IsAuthorized_ExportForm);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_EXPORTFORM", GetSecureSignedToken( sPrefix, AV36IsAuthorized_ExportForm, context));
         GXt_boolean6 = AV38IsAuthorized_FillOutAForm;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "uform_ufill", out  GXt_boolean6) ;
         AV38IsAuthorized_FillOutAForm = GXt_boolean6;
         AssignAttri(sPrefix, false, "AV38IsAuthorized_FillOutAForm", AV38IsAuthorized_FillOutAForm);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_FILLOUTAFORM", GetSecureSignedToken( sPrefix, AV38IsAuthorized_FillOutAForm, context));
         GXt_boolean6 = AV37IsAuthorized_FilledOutForms;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "uform_ufilledoutform", out  GXt_boolean6) ;
         AV37IsAuthorized_FilledOutForms = GXt_boolean6;
         AssignAttri(sPrefix, false, "AV37IsAuthorized_FilledOutForms", AV37IsAuthorized_FilledOutForms);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_FILLEDOUTFORMS", GetSecureSignedToken( sPrefix, AV37IsAuthorized_FilledOutForms, context));
         GXt_boolean6 = AV33IsAuthorized_Copy;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "uform_ucopy", out  GXt_boolean6) ;
         AV33IsAuthorized_Copy = GXt_boolean6;
         AssignAttri(sPrefix, false, "AV33IsAuthorized_Copy", AV33IsAuthorized_Copy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_COPY", GetSecureSignedToken( sPrefix, AV33IsAuthorized_Copy, context));
         GXt_boolean6 = AV41IsAuthorized_UCopyToLocation;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "uform_copytolocation", out  GXt_boolean6) ;
         AV41IsAuthorized_UCopyToLocation = GXt_boolean6;
         AssignAttri(sPrefix, false, "AV41IsAuthorized_UCopyToLocation", AV41IsAuthorized_UCopyToLocation);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UCOPYTOLOCATION", GetSecureSignedToken( sPrefix, AV41IsAuthorized_UCopyToLocation, context));
         GXt_boolean6 = AV43IsAuthorized_UDirectCopyToLocation;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "uform_directcopytolocation", out  GXt_boolean6) ;
         AV43IsAuthorized_UDirectCopyToLocation = GXt_boolean6;
         AssignAttri(sPrefix, false, "AV43IsAuthorized_UDirectCopyToLocation", AV43IsAuthorized_UDirectCopyToLocation);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UDIRECTCOPYTOLOCATION", GetSecureSignedToken( sPrefix, AV43IsAuthorized_UDirectCopyToLocation, context));
         GXt_boolean6 = AV44IsAuthorized_UInsert;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "uform_uinsert", out  GXt_boolean6) ;
         AV44IsAuthorized_UInsert = GXt_boolean6;
         AssignAttri(sPrefix, false, "AV44IsAuthorized_UInsert", AV44IsAuthorized_UInsert);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_UINSERT", GetSecureSignedToken( sPrefix, AV44IsAuthorized_UInsert, context));
         if ( ! ( AV44IsAuthorized_UInsert && ( ( AV79WWPFormType == 0 ) ) ) )
         {
            bttBtnuinsert_Visible = 0;
            AssignProp(sPrefix, false, bttBtnuinsert_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnuinsert_Visible), 5, 0), true);
         }
         GXt_boolean6 = AV40IsAuthorized_Insert;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "wwp_createdynamicform_Execute", out  GXt_boolean6) ;
         AV40IsAuthorized_Insert = GXt_boolean6;
         AssignAttri(sPrefix, false, "AV40IsAuthorized_Insert", AV40IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( sPrefix, AV40IsAuthorized_Insert, context));
         if ( ! ( AV40IsAuthorized_Insert && ( ( AV79WWPFormType == 0 ) ) ) )
         {
            bttBtninsert_Visible = 0;
            AssignProp(sPrefix, false, bttBtninsert_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtninsert_Visible), 5, 0), true);
         }
         GXt_boolean6 = AV39IsAuthorized_ImportForm;
         new WorkWithPlus.workwithplus_commongam.secgamisauthbyfunctionalitykey(context ).execute(  "uform_uimport", out  GXt_boolean6) ;
         AV39IsAuthorized_ImportForm = GXt_boolean6;
         AssignAttri(sPrefix, false, "AV39IsAuthorized_ImportForm", AV39IsAuthorized_ImportForm);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vISAUTHORIZED_IMPORTFORM", GetSecureSignedToken( sPrefix, AV39IsAuthorized_ImportForm, context));
         if ( ! ( AV39IsAuthorized_ImportForm ) )
         {
            bttBtnimportform_Visible = 0;
            AssignProp(sPrefix, false, bttBtnimportform_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnimportform_Visible), 5, 0), true);
         }
      }

      protected void S112( )
      {
         /* 'LOADSAVEDFILTERS' Routine */
         returnInSub = false;
         GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item7 = AV47ManageFiltersData;
         new WorkWithPlus.workwithplus_web.wwp_managefiltersloadsavedfilters(context ).execute(  "WP_DynamicFormElementUFormWCFilters",  "",  "",  false, out  GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item7) ;
         AV47ManageFiltersData = GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item7;
      }

      protected void S192( )
      {
         /* 'CLEANFILTERS' Routine */
         returnInSub = false;
         AV21FilterFullText = "";
         AssignAttri(sPrefix, false, "AV21FilterFullText", AV21FilterFullText);
         AV67TFWWPFormTitle = "";
         AssignAttri(sPrefix, false, "AV67TFWWPFormTitle", AV67TFWWPFormTitle);
         AV68TFWWPFormTitle_Sel = "";
         AssignAttri(sPrefix, false, "AV68TFWWPFormTitle_Sel", AV68TFWWPFormTitle_Sel);
         AV65TFWWPFormReferenceName = "";
         AssignAttri(sPrefix, false, "AV65TFWWPFormReferenceName", AV65TFWWPFormReferenceName);
         AV66TFWWPFormReferenceName_Sel = "";
         AssignAttri(sPrefix, false, "AV66TFWWPFormReferenceName_Sel", AV66TFWWPFormReferenceName_Sel);
         AV61TFWWPFormDate = (DateTime)(DateTime.MinValue);
         AssignAttri(sPrefix, false, "AV61TFWWPFormDate", context.localUtil.TToC( AV61TFWWPFormDate, 8, 5, 0, 3, "/", ":", " "));
         AV62TFWWPFormDate_To = (DateTime)(DateTime.MinValue);
         AssignAttri(sPrefix, false, "AV62TFWWPFormDate_To", context.localUtil.TToC( AV62TFWWPFormDate_To, 8, 5, 0, 3, "/", ":", " "));
         AV69TFWWPFormVersionNumber = 0;
         AssignAttri(sPrefix, false, "AV69TFWWPFormVersionNumber", StringUtil.LTrimStr( (decimal)(AV69TFWWPFormVersionNumber), 4, 0));
         AV70TFWWPFormVersionNumber_To = 0;
         AssignAttri(sPrefix, false, "AV70TFWWPFormVersionNumber_To", StringUtil.LTrimStr( (decimal)(AV70TFWWPFormVersionNumber_To), 4, 0));
         AV63TFWWPFormLatestVersionNumber = 0;
         AssignAttri(sPrefix, false, "AV63TFWWPFormLatestVersionNumber", StringUtil.LTrimStr( (decimal)(AV63TFWWPFormLatestVersionNumber), 4, 0));
         AV64TFWWPFormLatestVersionNumber_To = 0;
         AssignAttri(sPrefix, false, "AV64TFWWPFormLatestVersionNumber_To", StringUtil.LTrimStr( (decimal)(AV64TFWWPFormLatestVersionNumber_To), 4, 0));
         Ddo_grid_Selectedvalue_set = "";
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "SelectedValue_set", Ddo_grid_Selectedvalue_set);
         Ddo_grid_Filteredtext_set = "";
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "FilteredText_set", Ddo_grid_Filteredtext_set);
         Ddo_grid_Filteredtextto_set = "";
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "FilteredTextTo_set", Ddo_grid_Filteredtextto_set);
      }

      protected void S212( )
      {
         /* 'DO UUPDATE' Routine */
         returnInSub = false;
         if ( AV46IsAuthorized_UUpdate )
         {
            CallWebObject(formatLink("workwithplus.dynamicforms.wwp_createdynamicform.aspx", new object[] {UrlEncode(StringUtil.LTrimStr(A206WWPFormId,4,0)),UrlEncode(StringUtil.RTrim("UPD")),UrlEncode(StringUtil.RTrim("")),UrlEncode(StringUtil.LTrimStr(A240WWPFormType,1,0))}, new string[] {"WWPFormId","WWPDynamicFormMode","DefaultFormType","WWPFormType"}) );
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem("Action no longer available");
            context.DoAjaxRefreshCmp(sPrefix);
         }
      }

      protected void S222( )
      {
         /* 'DO UDELETE' Routine */
         returnInSub = false;
         Dvelop_confirmpanel_udelete_Confirmationtext = StringUtil.Format( "Are you sure that you want to delete the form?", A209WWPFormTitle, "", "", "", "", "", "", "", "");
         ucDvelop_confirmpanel_udelete.SendProperty(context, sPrefix, false, Dvelop_confirmpanel_udelete_Internalname, "ConfirmationText", Dvelop_confirmpanel_udelete_Confirmationtext);
         if ( AV42IsAuthorized_UDelete )
         {
            AV82Wwpformid_selected = A206WWPFormId;
            AssignAttri(sPrefix, false, "AV82Wwpformid_selected", StringUtil.LTrimStr( (decimal)(AV82Wwpformid_selected), 4, 0));
            AV83Wwpformversionnumber_selected = A207WWPFormVersionNumber;
            AssignAttri(sPrefix, false, "AV83Wwpformversionnumber_selected", StringUtil.LTrimStr( (decimal)(AV83Wwpformversionnumber_selected), 4, 0));
            this.executeUsercontrolMethod(sPrefix, false, "DVELOP_CONFIRMPANEL_UDELETEContainer", "Confirm", "", new Object[] {});
         }
         else
         {
            GX_msglist.addItem("Action no longer available");
            context.DoAjaxRefreshCmp(sPrefix);
         }
      }

      protected void S302( )
      {
         /* 'DO ACTION UDELETE' Routine */
         returnInSub = false;
         new GeneXus.Programs.workwithplus.dynamicforms.wwp_df_deleteform(context ).execute(  AV82Wwpformid_selected,  AV83Wwpformversionnumber_selected, out  AV51Messages) ;
         if ( AV51Messages.Count > 0 )
         {
            AV84GXV1 = 1;
            while ( AV84GXV1 <= AV51Messages.Count )
            {
               AV50Message = ((GeneXus.Utils.SdtMessages_Message)AV51Messages.Item(AV84GXV1));
               GX_msglist.addItem(new WorkWithPlus.workwithplus_web.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  AV50Message.gxTpr_Description,  "error",  "",  "false",  ""));
               AV84GXV1 = (int)(AV84GXV1+1);
            }
         }
         gxgrGrid_refresh( subGrid_Rows, AV53OrderedBy, AV55OrderedDsc, AV21FilterFullText, AV79WWPFormType, AV24GeneralDynamicFormids, AV48ManageFiltersExecutionStep, AV9ColumnsSelector, AV81Pgmname, AV67TFWWPFormTitle, AV68TFWWPFormTitle_Sel, AV65TFWWPFormReferenceName, AV66TFWWPFormReferenceName_Sel, AV61TFWWPFormDate, AV62TFWWPFormDate_To, AV69TFWWPFormVersionNumber, AV70TFWWPFormVersionNumber_To, AV63TFWWPFormLatestVersionNumber, AV64TFWWPFormLatestVersionNumber_To, AV78WWPFormIsForDynamicValidations, AV46IsAuthorized_UUpdate, AV42IsAuthorized_UDelete, AV20FilledOutForms, AV35IsAuthorized_Display, AV36IsAuthorized_ExportForm, AV38IsAuthorized_FillOutAForm, AV37IsAuthorized_FilledOutForms, AV33IsAuthorized_Copy, AV41IsAuthorized_UCopyToLocation, AV43IsAuthorized_UDirectCopyToLocation, AV77WWPFormId, AV5LocationId, AV6OrganisationId, AV44IsAuthorized_UInsert, AV40IsAuthorized_Insert, AV39IsAuthorized_ImportForm, A240WWPFormType, sPrefix) ;
      }

      protected void S232( )
      {
         /* 'DO DISPLAY' Routine */
         returnInSub = false;
         if ( AV35IsAuthorized_Display )
         {
            CallWebObject(formatLink("workwithplus.dynamicforms.wwp_createdynamicform.aspx", new object[] {UrlEncode(StringUtil.LTrimStr(A206WWPFormId,4,0)),UrlEncode(StringUtil.RTrim("DSP")),UrlEncode(StringUtil.RTrim("")),UrlEncode(StringUtil.LTrimStr(AV79WWPFormType,1,0))}, new string[] {"WWPFormId","WWPDynamicFormMode","DefaultFormType","WWPFormType"}) );
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem("Action no longer available");
            context.DoAjaxRefreshCmp(sPrefix);
         }
      }

      protected void S242( )
      {
         /* 'DO EXPORTFORM' Routine */
         returnInSub = false;
         if ( AV36IsAuthorized_ExportForm )
         {
            CallWebObject(formatLink("workwithplus.dynamicforms.wwp_df_export.aspx", new object[] {UrlEncode(StringUtil.LTrimStr(A206WWPFormId,4,0))}, new string[] {"WWPFormId"}) );
            context.wjLocDisableFrm = 2;
         }
         else
         {
            GX_msglist.addItem("Action no longer available");
            context.DoAjaxRefreshCmp(sPrefix);
         }
      }

      protected void S252( )
      {
         /* 'DO FILLOUTAFORM' Routine */
         returnInSub = false;
         if ( AV38IsAuthorized_FillOutAForm )
         {
            CallWebObject(formatLink("udynamicform.aspx", new object[] {UrlEncode(StringUtil.RTrim(A208WWPFormReferenceName)),UrlEncode(StringUtil.LTrimStr(0,1,0)),UrlEncode(StringUtil.RTrim("INS"))}, new string[] {"WWPFormReferenceName","WWPFormInstanceId","WWPDynamicFormMode","isLinkingDiscussion"}) );
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem("Action no longer available");
            context.DoAjaxRefreshCmp(sPrefix);
         }
      }

      protected void S262( )
      {
         /* 'DO FILLEDOUTFORMS' Routine */
         returnInSub = false;
         if ( AV37IsAuthorized_FilledOutForms )
         {
            CallWebObject(formatLink("ufilledoutforms.aspx", new object[] {UrlEncode(StringUtil.LTrimStr(A206WWPFormId,4,0)),UrlEncode(StringUtil.RTrim(A209WWPFormTitle))}, new string[] {"WWPFormId","WWPFormTitle"}) );
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem("Action no longer available");
            context.DoAjaxRefreshCmp(sPrefix);
         }
      }

      protected void S272( )
      {
         /* 'DO COPY' Routine */
         returnInSub = false;
         AV76WWPForm.Load(A206WWPFormId, A207WWPFormVersionNumber);
         AV12CopyNumber = 1;
         AV8WWPFormReferenceName = StringUtil.Format( "%1Copy%2", A208WWPFormReferenceName, StringUtil.Trim( StringUtil.Str( (decimal)(AV12CopyNumber), 4, 0)), "", "", "", "", "", "", "");
         while ( ! new GeneXus.Programs.workwithplus.dynamicforms.wwp_df_validateuniquereferencename(context).executeUdp(  0,  AV8WWPFormReferenceName) )
         {
            AV12CopyNumber = (short)(AV12CopyNumber+1);
            AV8WWPFormReferenceName = StringUtil.Format( "%1Copy%2", A208WWPFormReferenceName, StringUtil.Trim( StringUtil.Str( (decimal)(AV12CopyNumber), 4, 0)), "", "", "", "", "", "", "");
         }
         AV52NewWWPForm = new SdtUForm(context);
         /* Using cursor H00B94 */
         pr_default.execute(2);
         while ( (pr_default.getStatus(2) != 101) )
         {
            A206WWPFormId = H00B94_A206WWPFormId[0];
            AV52NewWWPForm.gxTpr_Wwpformid = A206WWPFormId;
            /* Exit For each command. Update data (if necessary), close cursors & exit. */
            if (true) break;
            pr_default.readNext(2);
         }
         pr_default.close(2);
         AV52NewWWPForm.gxTpr_Wwpformid = (short)(AV52NewWWPForm.gxTpr_Wwpformid+1);
         AV52NewWWPForm.gxTpr_Wwpformversionnumber = 1;
         AV52NewWWPForm.gxTpr_Wwpformreferencename = AV8WWPFormReferenceName;
         AV52NewWWPForm.gxTpr_Wwpformtitle = AV76WWPForm.gxTpr_Wwpformtitle;
         AV52NewWWPForm.gxTpr_Wwpformdate = DateTimeUtil.Now( context);
         AV52NewWWPForm.gxTpr_Wwpformiswizard = AV76WWPForm.gxTpr_Wwpformiswizard;
         AV52NewWWPForm.gxTpr_Wwpformvalidations = AV76WWPForm.gxTpr_Wwpformvalidations;
         AV52NewWWPForm.gxTpr_Wwpformresume = AV76WWPForm.gxTpr_Wwpformresume;
         AV52NewWWPForm.gxTpr_Wwpformresumemessage = AV76WWPForm.gxTpr_Wwpformresumemessage;
         AV86GXV2 = 1;
         while ( AV86GXV2 <= AV76WWPForm.gxTpr_Element.Count )
         {
            AV19Element = ((SdtUForm_Element)AV76WWPForm.gxTpr_Element.Item(AV86GXV2));
            if ( AV19Element.gxTpr_Wwpformelementparentid >= 0 )
            {
               AV52NewWWPForm.gxTpr_Element.Add(AV19Element, 0);
            }
            AV86GXV2 = (int)(AV86GXV2+1);
         }
         if ( AV52NewWWPForm.Insert() )
         {
            context.CommitDataStores("wp_dynamicformelementuformwc",pr_default);
            GX_msglist.addItem(new WorkWithPlus.workwithplus_web.dvmessagegetbasicnotificationmsg(context).executeUdp(  "Copy form",  "The form was successfully copied.",  "success",  "",  "na",  ""));
            gxgrGrid_refresh( subGrid_Rows, AV53OrderedBy, AV55OrderedDsc, AV21FilterFullText, AV79WWPFormType, AV24GeneralDynamicFormids, AV48ManageFiltersExecutionStep, AV9ColumnsSelector, AV81Pgmname, AV67TFWWPFormTitle, AV68TFWWPFormTitle_Sel, AV65TFWWPFormReferenceName, AV66TFWWPFormReferenceName_Sel, AV61TFWWPFormDate, AV62TFWWPFormDate_To, AV69TFWWPFormVersionNumber, AV70TFWWPFormVersionNumber_To, AV63TFWWPFormLatestVersionNumber, AV64TFWWPFormLatestVersionNumber_To, AV78WWPFormIsForDynamicValidations, AV46IsAuthorized_UUpdate, AV42IsAuthorized_UDelete, AV20FilledOutForms, AV35IsAuthorized_Display, AV36IsAuthorized_ExportForm, AV38IsAuthorized_FillOutAForm, AV37IsAuthorized_FilledOutForms, AV33IsAuthorized_Copy, AV41IsAuthorized_UCopyToLocation, AV43IsAuthorized_UDirectCopyToLocation, AV77WWPFormId, AV5LocationId, AV6OrganisationId, AV44IsAuthorized_UInsert, AV40IsAuthorized_Insert, AV39IsAuthorized_ImportForm, A240WWPFormType, sPrefix) ;
         }
         else
         {
            AV88GXV4 = 1;
            AV87GXV3 = AV52NewWWPForm.GetMessages();
            while ( AV88GXV4 <= AV87GXV3.Count )
            {
               AV50Message = ((GeneXus.Utils.SdtMessages_Message)AV87GXV3.Item(AV88GXV4));
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV58ResultMsg)) )
               {
                  AV58ResultMsg += StringUtil.NewLine( );
                  AssignAttri(sPrefix, false, "AV58ResultMsg", AV58ResultMsg);
               }
               AV58ResultMsg += AV50Message.gxTpr_Description;
               AssignAttri(sPrefix, false, "AV58ResultMsg", AV58ResultMsg);
               AV88GXV4 = (int)(AV88GXV4+1);
            }
            GX_msglist.addItem(new WorkWithPlus.workwithplus_web.dvmessagegetbasicnotificationmsg(context).executeUdp(  "Error cloning a form",  AV58ResultMsg,  "error",  "",  "false",  ""));
         }
      }

      protected void S282( )
      {
         /* 'DO UCOPYTOLOCATION' Routine */
         returnInSub = false;
         if ( AV41IsAuthorized_UCopyToLocation )
         {
            this.executeUsercontrolMethod(sPrefix, false, "UCOPYTOLOCATION_MODALContainer", "Confirm", "", new Object[] {});
         }
         else
         {
            GX_msglist.addItem("Action no longer available");
            context.DoAjaxRefreshCmp(sPrefix);
         }
      }

      protected void S292( )
      {
         /* 'DO UDIRECTCOPYTOLOCATION' Routine */
         returnInSub = false;
         AV76WWPForm.Load(A206WWPFormId, A207WWPFormVersionNumber);
         AV12CopyNumber = 1;
         AV8WWPFormReferenceName = StringUtil.Format( "%1Copy%2", A208WWPFormReferenceName, StringUtil.Trim( StringUtil.Str( (decimal)(AV12CopyNumber), 4, 0)), "", "", "", "", "", "", "");
         while ( ! new GeneXus.Programs.workwithplus.dynamicforms.wwp_df_validateuniquereferencename(context).executeUdp(  0,  AV8WWPFormReferenceName) )
         {
            AV12CopyNumber = (short)(AV12CopyNumber+1);
            AV8WWPFormReferenceName = StringUtil.Format( "%1Copy%2", A208WWPFormReferenceName, StringUtil.Trim( StringUtil.Str( (decimal)(AV12CopyNumber), 4, 0)), "", "", "", "", "", "", "");
         }
         AV52NewWWPForm = new SdtUForm(context);
         /* Using cursor H00B95 */
         pr_default.execute(3);
         while ( (pr_default.getStatus(3) != 101) )
         {
            A206WWPFormId = H00B95_A206WWPFormId[0];
            AV52NewWWPForm.gxTpr_Wwpformid = A206WWPFormId;
            /* Exit For each command. Update data (if necessary), close cursors & exit. */
            if (true) break;
            pr_default.readNext(3);
         }
         pr_default.close(3);
         AV52NewWWPForm.gxTpr_Wwpformid = (short)(AV52NewWWPForm.gxTpr_Wwpformid+1);
         AV52NewWWPForm.gxTpr_Wwpformversionnumber = 1;
         AV52NewWWPForm.gxTpr_Wwpformreferencename = AV8WWPFormReferenceName;
         AV52NewWWPForm.gxTpr_Wwpformtitle = AV76WWPForm.gxTpr_Wwpformtitle;
         AV52NewWWPForm.gxTpr_Wwpformiswizard = AV76WWPForm.gxTpr_Wwpformiswizard;
         AV52NewWWPForm.gxTpr_Wwpformdate = DateTimeUtil.Now( context);
         AV52NewWWPForm.gxTpr_Wwpformvalidations = AV76WWPForm.gxTpr_Wwpformvalidations;
         AV52NewWWPForm.gxTpr_Wwpformresume = AV76WWPForm.gxTpr_Wwpformresume;
         AV52NewWWPForm.gxTpr_Wwpformresumemessage = AV76WWPForm.gxTpr_Wwpformresumemessage;
         AV90GXV5 = 1;
         while ( AV90GXV5 <= AV76WWPForm.gxTpr_Element.Count )
         {
            AV19Element = ((SdtUForm_Element)AV76WWPForm.gxTpr_Element.Item(AV90GXV5));
            if ( AV19Element.gxTpr_Wwpformelementparentid >= 0 )
            {
               AV52NewWWPForm.gxTpr_Element.Add(AV19Element, 0);
            }
            AV90GXV5 = (int)(AV90GXV5+1);
         }
         if ( AV52NewWWPForm.Insert() )
         {
            context.CommitDataStores("wp_dynamicformelementuformwc",pr_default);
            if ( ! (Guid.Empty==AV5LocationId) )
            {
               AV7Trn_LocationDynamicForm = new SdtTrn_LocationDynamicForm(context);
               AV7Trn_LocationDynamicForm.gxTpr_Locationdynamicformid = Guid.NewGuid( );
               AV7Trn_LocationDynamicForm.gxTpr_Locationid = AV5LocationId;
               AV7Trn_LocationDynamicForm.gxTpr_Organisationid = AV6OrganisationId;
               AV7Trn_LocationDynamicForm.gxTpr_Wwpformid = AV52NewWWPForm.gxTpr_Wwpformid;
               AV7Trn_LocationDynamicForm.gxTpr_Wwpformversionnumber = AV52NewWWPForm.gxTpr_Wwpformversionnumber;
               if ( AV7Trn_LocationDynamicForm.Insert() )
               {
                  context.CommitDataStores("wp_dynamicformelementuformwc",pr_default);
               }
               else
               {
                  AV92GXV7 = 1;
                  AV91GXV6 = AV7Trn_LocationDynamicForm.GetMessages();
                  while ( AV92GXV7 <= AV91GXV6.Count )
                  {
                     AV50Message = ((GeneXus.Utils.SdtMessages_Message)AV91GXV6.Item(AV92GXV7));
                     GX_msglist.addItem(AV50Message.gxTpr_Description);
                     AV92GXV7 = (int)(AV92GXV7+1);
                  }
               }
            }
            GX_msglist.addItem(new WorkWithPlus.workwithplus_web.dvmessagegetbasicnotificationmsg(context).executeUdp(  "Copy form",  "The form was successfully copied.",  "success",  "",  "na",  ""));
            gxgrGrid_refresh( subGrid_Rows, AV53OrderedBy, AV55OrderedDsc, AV21FilterFullText, AV79WWPFormType, AV24GeneralDynamicFormids, AV48ManageFiltersExecutionStep, AV9ColumnsSelector, AV81Pgmname, AV67TFWWPFormTitle, AV68TFWWPFormTitle_Sel, AV65TFWWPFormReferenceName, AV66TFWWPFormReferenceName_Sel, AV61TFWWPFormDate, AV62TFWWPFormDate_To, AV69TFWWPFormVersionNumber, AV70TFWWPFormVersionNumber_To, AV63TFWWPFormLatestVersionNumber, AV64TFWWPFormLatestVersionNumber_To, AV78WWPFormIsForDynamicValidations, AV46IsAuthorized_UUpdate, AV42IsAuthorized_UDelete, AV20FilledOutForms, AV35IsAuthorized_Display, AV36IsAuthorized_ExportForm, AV38IsAuthorized_FillOutAForm, AV37IsAuthorized_FilledOutForms, AV33IsAuthorized_Copy, AV41IsAuthorized_UCopyToLocation, AV43IsAuthorized_UDirectCopyToLocation, AV77WWPFormId, AV5LocationId, AV6OrganisationId, AV44IsAuthorized_UInsert, AV40IsAuthorized_Insert, AV39IsAuthorized_ImportForm, A240WWPFormType, sPrefix) ;
            CallWebObject(formatLink("wp_locationdynamicform.aspx") );
            context.wjLocDisableFrm = 1;
         }
         else
         {
            AV94GXV9 = 1;
            AV93GXV8 = AV52NewWWPForm.GetMessages();
            while ( AV94GXV9 <= AV93GXV8.Count )
            {
               AV50Message = ((GeneXus.Utils.SdtMessages_Message)AV93GXV8.Item(AV94GXV9));
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV58ResultMsg)) )
               {
                  AV58ResultMsg += StringUtil.NewLine( );
                  AssignAttri(sPrefix, false, "AV58ResultMsg", AV58ResultMsg);
               }
               AV58ResultMsg += AV50Message.gxTpr_Description;
               AssignAttri(sPrefix, false, "AV58ResultMsg", AV58ResultMsg);
               AV94GXV9 = (int)(AV94GXV9+1);
            }
            GX_msglist.addItem(new WorkWithPlus.workwithplus_web.dvmessagegetbasicnotificationmsg(context).executeUdp(  "Error cloning a form",  AV58ResultMsg,  "error",  "",  "false",  ""));
         }
      }

      protected void S132( )
      {
         /* 'LOADGRIDSTATE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV60Session.Get(AV81Pgmname+"GridState"), "") == 0 )
         {
            AV29GridState.FromXml(new WorkWithPlus.workwithplus_web.loadgridstate(context).executeUdp(  AV81Pgmname+"GridState"), null, "", "");
         }
         else
         {
            AV29GridState.FromXml(AV60Session.Get(AV81Pgmname+"GridState"), null, "", "");
         }
         AV53OrderedBy = AV29GridState.gxTpr_Orderedby;
         AssignAttri(sPrefix, false, "AV53OrderedBy", StringUtil.LTrimStr( (decimal)(AV53OrderedBy), 4, 0));
         AV55OrderedDsc = AV29GridState.gxTpr_Ordereddsc;
         AssignAttri(sPrefix, false, "AV55OrderedDsc", AV55OrderedDsc);
         /* Execute user subroutine: 'SETDDOSORTEDSTATUS' */
         S142 ();
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
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV29GridState.gxTpr_Pagesize))) )
         {
            subGrid_Rows = (int)(Math.Round(NumberUtil.Val( AV29GridState.gxTpr_Pagesize, "."), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         }
      }

      protected void S202( )
      {
         /* 'LOADREGFILTERSSTATE' Routine */
         returnInSub = false;
         AV95GXV10 = 1;
         while ( AV95GXV10 <= AV29GridState.gxTpr_Filtervalues.Count )
         {
            AV30GridStateFilterValue = ((WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue)AV29GridState.gxTpr_Filtervalues.Item(AV95GXV10));
            if ( StringUtil.StrCmp(AV30GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV21FilterFullText = AV30GridStateFilterValue.gxTpr_Value;
               AssignAttri(sPrefix, false, "AV21FilterFullText", AV21FilterFullText);
            }
            else if ( StringUtil.StrCmp(AV30GridStateFilterValue.gxTpr_Name, "TFWWPFORMTITLE") == 0 )
            {
               AV67TFWWPFormTitle = AV30GridStateFilterValue.gxTpr_Value;
               AssignAttri(sPrefix, false, "AV67TFWWPFormTitle", AV67TFWWPFormTitle);
            }
            else if ( StringUtil.StrCmp(AV30GridStateFilterValue.gxTpr_Name, "TFWWPFORMTITLE_SEL") == 0 )
            {
               AV68TFWWPFormTitle_Sel = AV30GridStateFilterValue.gxTpr_Value;
               AssignAttri(sPrefix, false, "AV68TFWWPFormTitle_Sel", AV68TFWWPFormTitle_Sel);
            }
            else if ( StringUtil.StrCmp(AV30GridStateFilterValue.gxTpr_Name, "TFWWPFORMREFERENCENAME") == 0 )
            {
               AV65TFWWPFormReferenceName = AV30GridStateFilterValue.gxTpr_Value;
               AssignAttri(sPrefix, false, "AV65TFWWPFormReferenceName", AV65TFWWPFormReferenceName);
            }
            else if ( StringUtil.StrCmp(AV30GridStateFilterValue.gxTpr_Name, "TFWWPFORMREFERENCENAME_SEL") == 0 )
            {
               AV66TFWWPFormReferenceName_Sel = AV30GridStateFilterValue.gxTpr_Value;
               AssignAttri(sPrefix, false, "AV66TFWWPFormReferenceName_Sel", AV66TFWWPFormReferenceName_Sel);
            }
            else if ( StringUtil.StrCmp(AV30GridStateFilterValue.gxTpr_Name, "TFWWPFORMDATE") == 0 )
            {
               AV61TFWWPFormDate = context.localUtil.CToT( AV30GridStateFilterValue.gxTpr_Value, 2);
               AssignAttri(sPrefix, false, "AV61TFWWPFormDate", context.localUtil.TToC( AV61TFWWPFormDate, 8, 5, 0, 3, "/", ":", " "));
               AV62TFWWPFormDate_To = context.localUtil.CToT( AV30GridStateFilterValue.gxTpr_Valueto, 2);
               AssignAttri(sPrefix, false, "AV62TFWWPFormDate_To", context.localUtil.TToC( AV62TFWWPFormDate_To, 8, 5, 0, 3, "/", ":", " "));
               AV14DDO_WWPFormDateAuxDate = DateTimeUtil.ResetTime(AV61TFWWPFormDate);
               AssignAttri(sPrefix, false, "AV14DDO_WWPFormDateAuxDate", context.localUtil.Format(AV14DDO_WWPFormDateAuxDate, "99/99/99"));
               AV16DDO_WWPFormDateAuxDateTo = DateTimeUtil.ResetTime(AV62TFWWPFormDate_To);
               AssignAttri(sPrefix, false, "AV16DDO_WWPFormDateAuxDateTo", context.localUtil.Format(AV16DDO_WWPFormDateAuxDateTo, "99/99/99"));
            }
            else if ( StringUtil.StrCmp(AV30GridStateFilterValue.gxTpr_Name, "TFWWPFORMVERSIONNUMBER") == 0 )
            {
               AV69TFWWPFormVersionNumber = (short)(Math.Round(NumberUtil.Val( AV30GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV69TFWWPFormVersionNumber", StringUtil.LTrimStr( (decimal)(AV69TFWWPFormVersionNumber), 4, 0));
               AV70TFWWPFormVersionNumber_To = (short)(Math.Round(NumberUtil.Val( AV30GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV70TFWWPFormVersionNumber_To", StringUtil.LTrimStr( (decimal)(AV70TFWWPFormVersionNumber_To), 4, 0));
            }
            else if ( StringUtil.StrCmp(AV30GridStateFilterValue.gxTpr_Name, "TFWWPFORMLATESTVERSIONNUMBER") == 0 )
            {
               AV63TFWWPFormLatestVersionNumber = (short)(Math.Round(NumberUtil.Val( AV30GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV63TFWWPFormLatestVersionNumber", StringUtil.LTrimStr( (decimal)(AV63TFWWPFormLatestVersionNumber), 4, 0));
               AV64TFWWPFormLatestVersionNumber_To = (short)(Math.Round(NumberUtil.Val( AV30GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV64TFWWPFormLatestVersionNumber_To", StringUtil.LTrimStr( (decimal)(AV64TFWWPFormLatestVersionNumber_To), 4, 0));
            }
            AV95GXV10 = (int)(AV95GXV10+1);
         }
         GXt_char5 = "";
         new WorkWithPlus.workwithplus_web.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV68TFWWPFormTitle_Sel)),  AV68TFWWPFormTitle_Sel, out  GXt_char5) ;
         GXt_char8 = "";
         new WorkWithPlus.workwithplus_web.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV66TFWWPFormReferenceName_Sel)),  AV66TFWWPFormReferenceName_Sel, out  GXt_char8) ;
         Ddo_grid_Selectedvalue_set = GXt_char5+"|"+GXt_char8+"|||";
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "SelectedValue_set", Ddo_grid_Selectedvalue_set);
         GXt_char8 = "";
         new WorkWithPlus.workwithplus_web.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV67TFWWPFormTitle)),  AV67TFWWPFormTitle, out  GXt_char8) ;
         GXt_char5 = "";
         new WorkWithPlus.workwithplus_web.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV65TFWWPFormReferenceName)),  AV65TFWWPFormReferenceName, out  GXt_char5) ;
         Ddo_grid_Filteredtext_set = GXt_char8+"|"+GXt_char5+"|"+((DateTime.MinValue==AV61TFWWPFormDate) ? "" : context.localUtil.DToC( AV14DDO_WWPFormDateAuxDate, 2, "/"))+"|"+((0==AV69TFWWPFormVersionNumber) ? "" : StringUtil.Str( (decimal)(AV69TFWWPFormVersionNumber), 4, 0))+"|"+((0==AV63TFWWPFormLatestVersionNumber) ? "" : StringUtil.Str( (decimal)(AV63TFWWPFormLatestVersionNumber), 4, 0));
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "FilteredText_set", Ddo_grid_Filteredtext_set);
         Ddo_grid_Filteredtextto_set = "||"+((DateTime.MinValue==AV62TFWWPFormDate_To) ? "" : context.localUtil.DToC( AV16DDO_WWPFormDateAuxDateTo, 2, "/"))+"|"+((0==AV70TFWWPFormVersionNumber_To) ? "" : StringUtil.Str( (decimal)(AV70TFWWPFormVersionNumber_To), 4, 0))+"|"+((0==AV64TFWWPFormLatestVersionNumber_To) ? "" : StringUtil.Str( (decimal)(AV64TFWWPFormLatestVersionNumber_To), 4, 0));
         ucDdo_grid.SendProperty(context, sPrefix, false, Ddo_grid_Internalname, "FilteredTextTo_set", Ddo_grid_Filteredtextto_set);
      }

      protected void S162( )
      {
         /* 'SAVEGRIDSTATE' Routine */
         returnInSub = false;
         AV29GridState.FromXml(AV60Session.Get(AV81Pgmname+"GridState"), null, "", "");
         AV29GridState.gxTpr_Orderedby = AV53OrderedBy;
         AV29GridState.gxTpr_Ordereddsc = AV55OrderedDsc;
         AV29GridState.gxTpr_Filtervalues.Clear();
         new WorkWithPlus.workwithplus_web.wwp_gridstateaddfiltervalue(context ).execute( ref  AV29GridState,  "FILTERFULLTEXT",  "Main filter",  !String.IsNullOrEmpty(StringUtil.RTrim( AV21FilterFullText)),  0,  AV21FilterFullText,  AV21FilterFullText,  false,  "",  "") ;
         new WorkWithPlus.workwithplus_web.wwp_gridstateaddfiltervalueandsel(context ).execute( ref  AV29GridState,  "TFWWPFORMTITLE",  "Title",  !String.IsNullOrEmpty(StringUtil.RTrim( AV67TFWWPFormTitle)),  0,  AV67TFWWPFormTitle,  AV67TFWWPFormTitle,  false,  "",  "",  !String.IsNullOrEmpty(StringUtil.RTrim( AV68TFWWPFormTitle_Sel)),  AV68TFWWPFormTitle_Sel,  AV68TFWWPFormTitle_Sel) ;
         new WorkWithPlus.workwithplus_web.wwp_gridstateaddfiltervalueandsel(context ).execute( ref  AV29GridState,  "TFWWPFORMREFERENCENAME",  "Reference Name",  !String.IsNullOrEmpty(StringUtil.RTrim( AV65TFWWPFormReferenceName)),  0,  AV65TFWWPFormReferenceName,  AV65TFWWPFormReferenceName,  false,  "",  "",  !String.IsNullOrEmpty(StringUtil.RTrim( AV66TFWWPFormReferenceName_Sel)),  AV66TFWWPFormReferenceName_Sel,  AV66TFWWPFormReferenceName_Sel) ;
         new WorkWithPlus.workwithplus_web.wwp_gridstateaddfiltervalue(context ).execute( ref  AV29GridState,  "TFWWPFORMDATE",  "Date",  !((DateTime.MinValue==AV61TFWWPFormDate)&&(DateTime.MinValue==AV62TFWWPFormDate_To)),  0,  StringUtil.Trim( context.localUtil.TToC( AV61TFWWPFormDate, 8, 5, 0, 3, "/", ":", " ")),  ((DateTime.MinValue==AV61TFWWPFormDate) ? "" : StringUtil.Trim( context.localUtil.Format( AV61TFWWPFormDate, "99/99/99 99:99"))),  true,  StringUtil.Trim( context.localUtil.TToC( AV62TFWWPFormDate_To, 8, 5, 0, 3, "/", ":", " ")),  ((DateTime.MinValue==AV62TFWWPFormDate_To) ? "" : StringUtil.Trim( context.localUtil.Format( AV62TFWWPFormDate_To, "99/99/99 99:99")))) ;
         new WorkWithPlus.workwithplus_web.wwp_gridstateaddfiltervalue(context ).execute( ref  AV29GridState,  "TFWWPFORMVERSIONNUMBER",  "Form Version #",  !((0==AV69TFWWPFormVersionNumber)&&(0==AV70TFWWPFormVersionNumber_To)),  0,  StringUtil.Trim( StringUtil.Str( (decimal)(AV69TFWWPFormVersionNumber), 4, 0)),  ((0==AV69TFWWPFormVersionNumber) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV69TFWWPFormVersionNumber), "ZZZ9"))),  true,  StringUtil.Trim( StringUtil.Str( (decimal)(AV70TFWWPFormVersionNumber_To), 4, 0)),  ((0==AV70TFWWPFormVersionNumber_To) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV70TFWWPFormVersionNumber_To), "ZZZ9")))) ;
         new WorkWithPlus.workwithplus_web.wwp_gridstateaddfiltervalue(context ).execute( ref  AV29GridState,  "TFWWPFORMLATESTVERSIONNUMBER",  "Latest Version",  !((0==AV63TFWWPFormLatestVersionNumber)&&(0==AV64TFWWPFormLatestVersionNumber_To)),  0,  StringUtil.Trim( StringUtil.Str( (decimal)(AV63TFWWPFormLatestVersionNumber), 4, 0)),  ((0==AV63TFWWPFormLatestVersionNumber) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV63TFWWPFormLatestVersionNumber), "ZZZ9"))),  true,  StringUtil.Trim( StringUtil.Str( (decimal)(AV64TFWWPFormLatestVersionNumber_To), 4, 0)),  ((0==AV64TFWWPFormLatestVersionNumber_To) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV64TFWWPFormLatestVersionNumber_To), "ZZZ9")))) ;
         if ( ! (0==AV79WWPFormType) )
         {
            AV30GridStateFilterValue = new WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue(context);
            AV30GridStateFilterValue.gxTpr_Name = "PARM_&WWPFORMTYPE";
            AV30GridStateFilterValue.gxTpr_Value = StringUtil.Str( (decimal)(AV79WWPFormType), 1, 0);
            AV29GridState.gxTpr_Filtervalues.Add(AV30GridStateFilterValue, 0);
         }
         if ( ! (false==AV78WWPFormIsForDynamicValidations) )
         {
            AV30GridStateFilterValue = new WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue(context);
            AV30GridStateFilterValue.gxTpr_Name = "PARM_&WWPFORMISFORDYNAMICVALIDATIONS";
            AV30GridStateFilterValue.gxTpr_Value = StringUtil.BoolToStr( AV78WWPFormIsForDynamicValidations);
            AV29GridState.gxTpr_Filtervalues.Add(AV30GridStateFilterValue, 0);
         }
         AV29GridState.gxTpr_Pagesize = StringUtil.Str( (decimal)(subGrid_Rows), 10, 0);
         new WorkWithPlus.workwithplus_web.savegridstate(context ).execute(  AV81Pgmname+"GridState",  AV29GridState.ToXml(false, true, "", "")) ;
      }

      protected void S122( )
      {
         /* 'PREPARETRANSACTION' Routine */
         returnInSub = false;
         AV71TrnContext = new WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext(context);
         AV71TrnContext.gxTpr_Callerobject = AV81Pgmname;
         AV71TrnContext.gxTpr_Callerondelete = true;
         AV71TrnContext.gxTpr_Callerurl = AV31HTTPRequest.ScriptName+"?"+AV31HTTPRequest.QueryString;
         AV71TrnContext.gxTpr_Transactionname = "UForm";
         AV72TrnContextAtt = new WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext_Attribute(context);
         AV72TrnContextAtt.gxTpr_Attributename = "WWPFormType";
         AV72TrnContextAtt.gxTpr_Attributevalue = StringUtil.Str( (decimal)(AV79WWPFormType), 1, 0);
         AV71TrnContext.gxTpr_Attributes.Add(AV72TrnContextAtt, 0);
         AV60Session.Set("TrnContext", AV71TrnContext.ToXml(false, true, "", ""));
      }

      protected void E17B92( )
      {
         /* Ucopytolocation_modal_Close Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(Ucopytolocation_modal_Result, "OK") == 0 )
         {
            GX_msglist.addItem(new WorkWithPlus.workwithplus_web.dvmessagegetbasicnotificationmsg(context).executeUdp(  "Copy form",  "The form was successfully copied.",  "success",  "",  "na",  ""));
            gxgrGrid_refresh( subGrid_Rows, AV53OrderedBy, AV55OrderedDsc, AV21FilterFullText, AV79WWPFormType, AV24GeneralDynamicFormids, AV48ManageFiltersExecutionStep, AV9ColumnsSelector, AV81Pgmname, AV67TFWWPFormTitle, AV68TFWWPFormTitle_Sel, AV65TFWWPFormReferenceName, AV66TFWWPFormReferenceName_Sel, AV61TFWWPFormDate, AV62TFWWPFormDate_To, AV69TFWWPFormVersionNumber, AV70TFWWPFormVersionNumber_To, AV63TFWWPFormLatestVersionNumber, AV64TFWWPFormLatestVersionNumber_To, AV78WWPFormIsForDynamicValidations, AV46IsAuthorized_UUpdate, AV42IsAuthorized_UDelete, AV20FilledOutForms, AV35IsAuthorized_Display, AV36IsAuthorized_ExportForm, AV38IsAuthorized_FillOutAForm, AV37IsAuthorized_FilledOutForms, AV33IsAuthorized_Copy, AV41IsAuthorized_UCopyToLocation, AV43IsAuthorized_UDirectCopyToLocation, AV77WWPFormId, AV5LocationId, AV6OrganisationId, AV44IsAuthorized_UInsert, AV40IsAuthorized_Insert, AV39IsAuthorized_ImportForm, A240WWPFormType, sPrefix) ;
         }
         else
         {
            GX_msglist.addItem(new WorkWithPlus.workwithplus_web.dvmessagegetbasicnotificationmsg(context).executeUdp(  "Error cloning a form",  "Error",  "error",  "",  "false",  ""));
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV9ColumnsSelector", AV9ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV47ManageFiltersData", AV47ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV29GridState", AV29GridState);
      }

      protected void S312( )
      {
         /* 'DO UPDATE' Routine */
         returnInSub = false;
         if ( AV78WWPFormIsForDynamicValidations )
         {
            CallWebObject(formatLink("workwithplus.dynamicforms.wwp_createdynamicvalidations.aspx", new object[] {UrlEncode(StringUtil.LTrimStr(A206WWPFormId,4,0)),UrlEncode(StringUtil.RTrim("UPD"))}, new string[] {"WWPFormId","WWPDynamicFormMode"}) );
            context.wjLocDisableFrm = 1;
         }
         else
         {
         }
      }

      protected void S182( )
      {
         /* 'COUNTFILLEDOUTFORMS' Routine */
         returnInSub = false;
         AV20FilledOutForms = 0;
         AssignAttri(sPrefix, false, edtavFilledoutforms_Internalname, StringUtil.LTrimStr( (decimal)(AV20FilledOutForms), 4, 0));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFILLEDOUTFORMS"+"_"+sGXsfl_41_idx, GetSecureSignedToken( sPrefix+sGXsfl_41_idx, context.localUtil.Format( (decimal)(AV20FilledOutForms), "ZZZ9"), context));
         /* Optimized group. */
         /* Using cursor H00B96 */
         pr_default.execute(4, new Object[] {AV77WWPFormId});
         cV20FilledOutForms = H00B96_AV20FilledOutForms[0];
         AssignAttri(sPrefix, false, edtavFilledoutforms_Internalname, StringUtil.LTrimStr( (decimal)(cV20FilledOutForms), 4, 0));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFILLEDOUTFORMS"+"_"+sGXsfl_41_idx, GetSecureSignedToken( sPrefix+sGXsfl_41_idx, context.localUtil.Format( (decimal)(cV20FilledOutForms), "ZZZ9"), context));
         pr_default.close(4);
         AV20FilledOutForms = (short)(AV20FilledOutForms+cV20FilledOutForms*1);
         AssignAttri(sPrefix, false, edtavFilledoutforms_Internalname, StringUtil.LTrimStr( (decimal)(AV20FilledOutForms), 4, 0));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFILLEDOUTFORMS"+"_"+sGXsfl_41_idx, GetSecureSignedToken( sPrefix+sGXsfl_41_idx, context.localUtil.Format( (decimal)(AV20FilledOutForms), "ZZZ9"), context));
         /* End optimized group. */
      }

      protected void wb_table3_69_B92( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblTableimportform_modal_Internalname, tblTableimportform_modal_Internalname, "", "Table", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tbody>") ;
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td data-align=\"center\"  style=\""+CSSHelper.Prettify( "text-align:-khtml-center;text-align:-moz-center;text-align:-webkit-center")+"\">") ;
            /* User Defined Control */
            ucImportform_modal.SetProperty("Width", Importform_modal_Width);
            ucImportform_modal.SetProperty("Title", Importform_modal_Title);
            ucImportform_modal.SetProperty("ConfirmType", Importform_modal_Confirmtype);
            ucImportform_modal.SetProperty("BodyType", Importform_modal_Bodytype);
            ucImportform_modal.Render(context, "dvelop.gxbootstrap.confirmpanel", Importform_modal_Internalname, sPrefix+"IMPORTFORM_MODALContainer");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"IMPORTFORM_MODALContainer"+"Body"+"\" style=\"display:none;\">") ;
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            context.WriteHtmlText( "</tbody>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table3_69_B92e( true) ;
         }
         else
         {
            wb_table3_69_B92e( false) ;
         }
      }

      protected void wb_table2_64_B92( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblTableucopytolocation_modal_Internalname, tblTableucopytolocation_modal_Internalname, "", "Table", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tbody>") ;
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td data-align=\"center\"  style=\""+CSSHelper.Prettify( "text-align:-khtml-center;text-align:-moz-center;text-align:-webkit-center")+"\">") ;
            /* User Defined Control */
            ucUcopytolocation_modal.SetProperty("Width", Ucopytolocation_modal_Width);
            ucUcopytolocation_modal.SetProperty("Title", Ucopytolocation_modal_Title);
            ucUcopytolocation_modal.SetProperty("ConfirmType", Ucopytolocation_modal_Confirmtype);
            ucUcopytolocation_modal.SetProperty("BodyType", Ucopytolocation_modal_Bodytype);
            ucUcopytolocation_modal.Render(context, "dvelop.gxbootstrap.confirmpanel", Ucopytolocation_modal_Internalname, sPrefix+"UCOPYTOLOCATION_MODALContainer");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"UCOPYTOLOCATION_MODALContainer"+"Body"+"\" style=\"display:none;\">") ;
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            context.WriteHtmlText( "</tbody>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table2_64_B92e( true) ;
         }
         else
         {
            wb_table2_64_B92e( false) ;
         }
      }

      protected void wb_table1_59_B92( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblTabledvelop_confirmpanel_udelete_Internalname, tblTabledvelop_confirmpanel_udelete_Internalname, "", "Table", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tbody>") ;
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td data-align=\"center\"  style=\""+CSSHelper.Prettify( "text-align:-khtml-center;text-align:-moz-center;text-align:-webkit-center")+"\">") ;
            /* User Defined Control */
            ucDvelop_confirmpanel_udelete.SetProperty("Title", Dvelop_confirmpanel_udelete_Title);
            ucDvelop_confirmpanel_udelete.SetProperty("ConfirmationText", Dvelop_confirmpanel_udelete_Confirmationtext);
            ucDvelop_confirmpanel_udelete.SetProperty("YesButtonCaption", Dvelop_confirmpanel_udelete_Yesbuttoncaption);
            ucDvelop_confirmpanel_udelete.SetProperty("NoButtonCaption", Dvelop_confirmpanel_udelete_Nobuttoncaption);
            ucDvelop_confirmpanel_udelete.SetProperty("CancelButtonCaption", Dvelop_confirmpanel_udelete_Cancelbuttoncaption);
            ucDvelop_confirmpanel_udelete.SetProperty("YesButtonPosition", Dvelop_confirmpanel_udelete_Yesbuttonposition);
            ucDvelop_confirmpanel_udelete.SetProperty("ConfirmType", Dvelop_confirmpanel_udelete_Confirmtype);
            ucDvelop_confirmpanel_udelete.Render(context, "dvelop.gxbootstrap.confirmpanel", Dvelop_confirmpanel_udelete_Internalname, sPrefix+"DVELOP_CONFIRMPANEL_UDELETEContainer");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"DVELOP_CONFIRMPANEL_UDELETEContainer"+"Body"+"\" style=\"display:none;\">") ;
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            context.WriteHtmlText( "</tbody>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table1_59_B92e( true) ;
         }
         else
         {
            wb_table1_59_B92e( false) ;
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV79WWPFormType = Convert.ToInt16(getParm(obj,0));
         AssignAttri(sPrefix, false, "AV79WWPFormType", StringUtil.Str( (decimal)(AV79WWPFormType), 1, 0));
         AV78WWPFormIsForDynamicValidations = (bool)getParm(obj,1);
         AssignAttri(sPrefix, false, "AV78WWPFormIsForDynamicValidations", AV78WWPFormIsForDynamicValidations);
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
         PAB92( ) ;
         WSB92( ) ;
         WEB92( ) ;
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
         sCtrlAV79WWPFormType = (string)((string)getParm(obj,0));
         sCtrlAV78WWPFormIsForDynamicValidations = (string)((string)getParm(obj,1));
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PAB92( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wp_dynamicformelementuformwc", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PAB92( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
            AV79WWPFormType = Convert.ToInt16(getParm(obj,2));
            AssignAttri(sPrefix, false, "AV79WWPFormType", StringUtil.Str( (decimal)(AV79WWPFormType), 1, 0));
            AV78WWPFormIsForDynamicValidations = (bool)getParm(obj,3);
            AssignAttri(sPrefix, false, "AV78WWPFormIsForDynamicValidations", AV78WWPFormIsForDynamicValidations);
         }
         wcpOAV79WWPFormType = (short)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"wcpOAV79WWPFormType"), ".", ","), 18, MidpointRounding.ToEven));
         wcpOAV78WWPFormIsForDynamicValidations = StringUtil.StrToBool( cgiGet( sPrefix+"wcpOAV78WWPFormIsForDynamicValidations"));
         if ( ! GetJustCreated( ) && ( ( AV79WWPFormType != wcpOAV79WWPFormType ) || ( AV78WWPFormIsForDynamicValidations != wcpOAV78WWPFormIsForDynamicValidations ) ) )
         {
            setjustcreated();
         }
         wcpOAV79WWPFormType = AV79WWPFormType;
         wcpOAV78WWPFormIsForDynamicValidations = AV78WWPFormIsForDynamicValidations;
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
         sCtrlAV79WWPFormType = cgiGet( sPrefix+"AV79WWPFormType_CTRL");
         if ( StringUtil.Len( sCtrlAV79WWPFormType) > 0 )
         {
            AV79WWPFormType = (short)(Math.Round(context.localUtil.CToN( cgiGet( sCtrlAV79WWPFormType), ".", ","), 18, MidpointRounding.ToEven));
            AssignAttri(sPrefix, false, "AV79WWPFormType", StringUtil.Str( (decimal)(AV79WWPFormType), 1, 0));
         }
         else
         {
            AV79WWPFormType = (short)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"AV79WWPFormType_PARM"), ".", ","), 18, MidpointRounding.ToEven));
         }
         sCtrlAV78WWPFormIsForDynamicValidations = cgiGet( sPrefix+"AV78WWPFormIsForDynamicValidations_CTRL");
         if ( StringUtil.Len( sCtrlAV78WWPFormIsForDynamicValidations) > 0 )
         {
            AV78WWPFormIsForDynamicValidations = StringUtil.StrToBool( cgiGet( sCtrlAV78WWPFormIsForDynamicValidations));
            AssignAttri(sPrefix, false, "AV78WWPFormIsForDynamicValidations", AV78WWPFormIsForDynamicValidations);
         }
         else
         {
            AV78WWPFormIsForDynamicValidations = StringUtil.StrToBool( cgiGet( sPrefix+"AV78WWPFormIsForDynamicValidations_PARM"));
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
         PAB92( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WSB92( ) ;
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
         WSB92( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"AV79WWPFormType_PARM", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV79WWPFormType), 1, 0, ".", "")));
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV79WWPFormType)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV79WWPFormType_CTRL", StringUtil.RTrim( sCtrlAV79WWPFormType));
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"AV78WWPFormIsForDynamicValidations_PARM", StringUtil.BoolToStr( AV78WWPFormIsForDynamicValidations));
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV78WWPFormIsForDynamicValidations)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV78WWPFormIsForDynamicValidations_CTRL", StringUtil.RTrim( sCtrlAV78WWPFormIsForDynamicValidations));
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
         WEB92( ) ;
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
         AddStyleSheetFile("DVelop/Bootstrap/Shared/DVelopBootstrap.css", "");
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202542411524061", true, true);
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
         context.AddJavascriptSource("wp_dynamicformelementuformwc.js", "?202542411524064", false, true);
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
         context.AddJavascriptSource("DVelop/Bootstrap/ConfirmPanel/BootstrapConfirmPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/ConfirmPanel/BootstrapConfirmPanelRender.js", "", false, true);
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

      protected void SubsflControlProps_412( )
      {
         edtWWPFormId_Internalname = sPrefix+"WWPFORMID_"+sGXsfl_41_idx;
         edtWWPFormTitle_Internalname = sPrefix+"WWPFORMTITLE_"+sGXsfl_41_idx;
         edtWWPFormReferenceName_Internalname = sPrefix+"WWPFORMREFERENCENAME_"+sGXsfl_41_idx;
         edtWWPFormDate_Internalname = sPrefix+"WWPFORMDATE_"+sGXsfl_41_idx;
         edtWWPFormVersionNumber_Internalname = sPrefix+"WWPFORMVERSIONNUMBER_"+sGXsfl_41_idx;
         edtWWPFormLatestVersionNumber_Internalname = sPrefix+"WWPFORMLATESTVERSIONNUMBER_"+sGXsfl_41_idx;
         edtavFilledoutforms_Internalname = sPrefix+"vFILLEDOUTFORMS_"+sGXsfl_41_idx;
         cmbavGridactiongroup1_Internalname = sPrefix+"vGRIDACTIONGROUP1_"+sGXsfl_41_idx;
      }

      protected void SubsflControlProps_fel_412( )
      {
         edtWWPFormId_Internalname = sPrefix+"WWPFORMID_"+sGXsfl_41_fel_idx;
         edtWWPFormTitle_Internalname = sPrefix+"WWPFORMTITLE_"+sGXsfl_41_fel_idx;
         edtWWPFormReferenceName_Internalname = sPrefix+"WWPFORMREFERENCENAME_"+sGXsfl_41_fel_idx;
         edtWWPFormDate_Internalname = sPrefix+"WWPFORMDATE_"+sGXsfl_41_fel_idx;
         edtWWPFormVersionNumber_Internalname = sPrefix+"WWPFORMVERSIONNUMBER_"+sGXsfl_41_fel_idx;
         edtWWPFormLatestVersionNumber_Internalname = sPrefix+"WWPFORMLATESTVERSIONNUMBER_"+sGXsfl_41_fel_idx;
         edtavFilledoutforms_Internalname = sPrefix+"vFILLEDOUTFORMS_"+sGXsfl_41_fel_idx;
         cmbavGridactiongroup1_Internalname = sPrefix+"vGRIDACTIONGROUP1_"+sGXsfl_41_fel_idx;
      }

      protected void sendrow_412( )
      {
         sGXsfl_41_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_41_idx), 4, 0), 4, "0");
         SubsflControlProps_412( ) ;
         WBB90( ) ;
         if ( ( subGrid_Rows * 1 == 0 ) || ( nGXsfl_41_idx <= subGrid_fnc_Recordsperpage( ) * 1 ) )
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
               if ( ((int)((nGXsfl_41_idx) % (2))) == 0 )
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
               context.WriteHtmlText( " gxrow=\""+sGXsfl_41_idx+"\">") ;
            }
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+"display:none;"+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtWWPFormId_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(A206WWPFormId), 4, 0, ".", "")),StringUtil.LTrim( context.localUtil.Format( (decimal)(A206WWPFormId), "ZZZ9")),(string)" dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtWWPFormId_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(short)0,(short)0,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)41,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtWWPFormTitle_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtWWPFormTitle_Internalname,(string)A209WWPFormTitle,(string)"",(string)"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtWWPFormTitle_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn hidden-xs",(string)"",(int)edtWWPFormTitle_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)100,(short)0,(short)0,(short)41,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtWWPFormReferenceName_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtWWPFormReferenceName_Internalname,(string)A208WWPFormReferenceName,(string)"",(string)"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtWWPFormReferenceName_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtWWPFormReferenceName_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)100,(short)0,(short)0,(short)41,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtWWPFormDate_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtWWPFormDate_Internalname,context.localUtil.TToC( A231WWPFormDate, 10, 8, 0, 3, "/", ":", " "),context.localUtil.Format( A231WWPFormDate, "99/99/99 99:99"),(string)"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtWWPFormDate_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn hidden-xs",(string)"",(int)edtWWPFormDate_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)41,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtWWPFormVersionNumber_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtWWPFormVersionNumber_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(A207WWPFormVersionNumber), 4, 0, ".", "")),StringUtil.LTrim( context.localUtil.Format( (decimal)(A207WWPFormVersionNumber), "ZZZ9")),(string)" dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtWWPFormVersionNumber_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn hidden-xs",(string)"",(int)edtWWPFormVersionNumber_Visible,(short)0,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)41,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtWWPFormLatestVersionNumber_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtWWPFormLatestVersionNumber_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(A219WWPFormLatestVersionNumber), 4, 0, ".", "")),StringUtil.LTrim( context.localUtil.Format( (decimal)(A219WWPFormLatestVersionNumber), "ZZZ9")),(string)" dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtWWPFormLatestVersionNumber_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn hidden-xs",(string)"",(int)edtWWPFormLatestVersionNumber_Visible,(short)0,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)41,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+"display:none;"+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavFilledoutforms_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(AV20FilledOutForms), 4, 0, ".", "")),StringUtil.LTrim( ((edtavFilledoutforms_Enabled!=0) ? context.localUtil.Format( (decimal)(AV20FilledOutForms), "ZZZ9") : context.localUtil.Format( (decimal)(AV20FilledOutForms), "ZZZ9")))," dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+""+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" ",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavFilledoutforms_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(short)0,(int)edtavFilledoutforms_Enabled,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)41,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 49,'" + sPrefix + "',false,'" + sGXsfl_41_idx + "',41)\"";
            if ( ( cmbavGridactiongroup1.ItemCount == 0 ) && isAjaxCallMode( ) )
            {
               GXCCtl = "vGRIDACTIONGROUP1_" + sGXsfl_41_idx;
               cmbavGridactiongroup1.Name = GXCCtl;
               cmbavGridactiongroup1.WebTags = "";
               if ( cmbavGridactiongroup1.ItemCount > 0 )
               {
                  AV25GridActionGroup1 = (short)(Math.Round(NumberUtil.Val( cmbavGridactiongroup1.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV25GridActionGroup1), 4, 0))), "."), 18, MidpointRounding.ToEven));
                  AssignAttri(sPrefix, false, cmbavGridactiongroup1_Internalname, StringUtil.LTrimStr( (decimal)(AV25GridActionGroup1), 4, 0));
               }
            }
            /* ComboBox */
            GridRow.AddColumnProperties("combobox", 2, isAjaxCallMode( ), new Object[] {(GXCombobox)cmbavGridactiongroup1,(string)cmbavGridactiongroup1_Internalname,StringUtil.Trim( StringUtil.Str( (decimal)(AV25GridActionGroup1), 4, 0)),(short)1,(string)cmbavGridactiongroup1_Jsonclick,(short)5,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"EVGRIDACTIONGROUP1.CLICK."+sGXsfl_41_idx+"'",(string)"int",(string)"",(short)-1,(short)1,(short)0,(short)0,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)cmbavGridactiongroup1_Class,(string)"WWActionGroupColumn",(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,49);\"",(string)"",(bool)true,(short)0});
            cmbavGridactiongroup1.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV25GridActionGroup1), 4, 0));
            AssignProp(sPrefix, false, cmbavGridactiongroup1_Internalname, "Values", (string)(cmbavGridactiongroup1.ToJavascriptSource()), !bGXsfl_41_Refreshing);
            send_integrity_lvl_hashesB92( ) ;
            GridContainer.AddRow(GridRow);
            nGXsfl_41_idx = ((subGrid_Islastpage==1)&&(nGXsfl_41_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_41_idx+1);
            sGXsfl_41_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_41_idx), 4, 0), 4, "0");
            SubsflControlProps_412( ) ;
         }
         /* End function sendrow_412 */
      }

      protected void init_web_controls( )
      {
         GXCCtl = "vGRIDACTIONGROUP1_" + sGXsfl_41_idx;
         cmbavGridactiongroup1.Name = GXCCtl;
         cmbavGridactiongroup1.WebTags = "";
         if ( cmbavGridactiongroup1.ItemCount > 0 )
         {
         }
         cmbWWPFormType.Name = "WWPFORMTYPE";
         cmbWWPFormType.WebTags = "";
         cmbWWPFormType.addItem("0", "Dynamic Form", 0);
         cmbWWPFormType.addItem("1", "Dynamic Section", 0);
         if ( cmbWWPFormType.ItemCount > 0 )
         {
         }
         /* End function init_web_controls */
      }

      protected void StartGridControl41( )
      {
         if ( GridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"GridContainer"+"DivS\" data-gxgridid=\"41\">") ;
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
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtWWPFormTitle_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "Title") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtWWPFormReferenceName_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "Reference Name") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtWWPFormDate_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "Date") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtWWPFormVersionNumber_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "Form Version #") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtWWPFormLatestVersionNumber_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "Latest Version") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+cmbavGridactiongroup1_Class+"\" "+" style=\""+""+""+"\" "+">") ;
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
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( (decimal)(A206WWPFormId), 4, 0, ".", ""))));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A209WWPFormTitle));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtWWPFormTitle_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A208WWPFormReferenceName));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtWWPFormReferenceName_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( context.localUtil.TToC( A231WWPFormDate, 10, 8, 0, 3, "/", ":", " ")));
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
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( (decimal)(AV20FilledOutForms), 4, 0, ".", ""))));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavFilledoutforms_Enabled), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( (decimal)(AV25GridActionGroup1), 4, 0, ".", ""))));
            GridColumn.AddObjectProperty("Class", StringUtil.RTrim( cmbavGridactiongroup1_Class));
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
         bttBtnuinsert_Internalname = sPrefix+"BTNUINSERT";
         bttBtninsert_Internalname = sPrefix+"BTNINSERT";
         bttBtneditcolumns_Internalname = sPrefix+"BTNEDITCOLUMNS";
         bttBtnimportform_Internalname = sPrefix+"BTNIMPORTFORM";
         divTableactions_Internalname = sPrefix+"TABLEACTIONS";
         Ddo_managefilters_Internalname = sPrefix+"DDO_MANAGEFILTERS";
         edtavFilterfulltext_Internalname = sPrefix+"vFILTERFULLTEXT";
         divTablefilters_Internalname = sPrefix+"TABLEFILTERS";
         divTablerightheader_Internalname = sPrefix+"TABLERIGHTHEADER";
         divTableheadercontent_Internalname = sPrefix+"TABLEHEADERCONTENT";
         divTableheader_Internalname = sPrefix+"TABLEHEADER";
         edtWWPFormId_Internalname = sPrefix+"WWPFORMID";
         edtWWPFormTitle_Internalname = sPrefix+"WWPFORMTITLE";
         edtWWPFormReferenceName_Internalname = sPrefix+"WWPFORMREFERENCENAME";
         edtWWPFormDate_Internalname = sPrefix+"WWPFORMDATE";
         edtWWPFormVersionNumber_Internalname = sPrefix+"WWPFORMVERSIONNUMBER";
         edtWWPFormLatestVersionNumber_Internalname = sPrefix+"WWPFORMLATESTVERSIONNUMBER";
         edtavFilledoutforms_Internalname = sPrefix+"vFILLEDOUTFORMS";
         cmbavGridactiongroup1_Internalname = sPrefix+"vGRIDACTIONGROUP1";
         Gridpaginationbar_Internalname = sPrefix+"GRIDPAGINATIONBAR";
         divGridtablewithpaginationbar_Internalname = sPrefix+"GRIDTABLEWITHPAGINATIONBAR";
         divTablegridheader_Internalname = sPrefix+"TABLEGRIDHEADER";
         Ddo_grid_Internalname = sPrefix+"DDO_GRID";
         cmbWWPFormType_Internalname = sPrefix+"WWPFORMTYPE";
         Ddo_gridcolumnsselector_Internalname = sPrefix+"DDO_GRIDCOLUMNSSELECTOR";
         Dvelop_confirmpanel_udelete_Internalname = sPrefix+"DVELOP_CONFIRMPANEL_UDELETE";
         tblTabledvelop_confirmpanel_udelete_Internalname = sPrefix+"TABLEDVELOP_CONFIRMPANEL_UDELETE";
         Ucopytolocation_modal_Internalname = sPrefix+"UCOPYTOLOCATION_MODAL";
         tblTableucopytolocation_modal_Internalname = sPrefix+"TABLEUCOPYTOLOCATION_MODAL";
         Importform_modal_Internalname = sPrefix+"IMPORTFORM_MODAL";
         tblTableimportform_modal_Internalname = sPrefix+"TABLEIMPORTFORM_MODAL";
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
         cmbavGridactiongroup1_Jsonclick = "";
         cmbavGridactiongroup1_Class = "ConvertToDDO";
         edtavFilledoutforms_Jsonclick = "";
         edtavFilledoutforms_Enabled = 1;
         edtWWPFormLatestVersionNumber_Jsonclick = "";
         edtWWPFormVersionNumber_Jsonclick = "";
         edtWWPFormDate_Jsonclick = "";
         edtWWPFormReferenceName_Jsonclick = "";
         edtWWPFormTitle_Jsonclick = "";
         edtWWPFormId_Jsonclick = "";
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
         subGrid_Sortable = 0;
         edtavDdo_wwpformdateauxdatetext_Jsonclick = "";
         cmbWWPFormType_Jsonclick = "";
         cmbWWPFormType.Visible = 1;
         edtavFilterfulltext_Jsonclick = "";
         edtavFilterfulltext_Enabled = 1;
         bttBtnimportform_Visible = 1;
         bttBtninsert_Visible = 1;
         bttBtnuinsert_Visible = 1;
         Grid_empowerer_Hascolumnsselector = Convert.ToBoolean( -1);
         Grid_empowerer_Hastitlesettings = Convert.ToBoolean( -1);
         Importform_modal_Bodytype = "WebComponent";
         Importform_modal_Confirmtype = "";
         Importform_modal_Title = "Select file to import";
         Importform_modal_Width = "400";
         Ucopytolocation_modal_Bodytype = "WebComponent";
         Ucopytolocation_modal_Confirmtype = "";
         Ucopytolocation_modal_Title = "Copy To Location";
         Ucopytolocation_modal_Width = "800";
         Dvelop_confirmpanel_udelete_Confirmtype = "1";
         Dvelop_confirmpanel_udelete_Yesbuttonposition = "left";
         Dvelop_confirmpanel_udelete_Cancelbuttoncaption = "WWP_ConfirmTextCancel";
         Dvelop_confirmpanel_udelete_Nobuttoncaption = "WWP_ConfirmTextNo";
         Dvelop_confirmpanel_udelete_Yesbuttoncaption = "WWP_ConfirmTextYes";
         Dvelop_confirmpanel_udelete_Confirmationtext = "WWP_DF_DeleteFormMessage";
         Dvelop_confirmpanel_udelete_Title = "Delete dynamic form";
         Ddo_gridcolumnsselector_Titlecontrolidtoreplace = "";
         Ddo_gridcolumnsselector_Dropdownoptionstype = "GridColumnsSelector";
         Ddo_gridcolumnsselector_Cls = "ColumnsSelector hidden-xs";
         Ddo_gridcolumnsselector_Tooltip = "WWP_EditColumnsTooltip";
         Ddo_gridcolumnsselector_Caption = "Select columns";
         Ddo_gridcolumnsselector_Icon = "fas fa-cog";
         Ddo_gridcolumnsselector_Icontype = "FontIcon";
         Ddo_grid_Format = "|||4.0|4.0";
         Ddo_grid_Datalistproc = "WP_DynamicFormElementUFormWCGetFilterData";
         Ddo_grid_Datalisttype = "Dynamic|Dynamic|||";
         Ddo_grid_Includedatalist = "T|T|||";
         Ddo_grid_Filterisrange = "||P|T|T";
         Ddo_grid_Filtertype = "Character|Character|Date|Numeric|Numeric";
         Ddo_grid_Includefilter = "T";
         Ddo_grid_Includesortasc = "T|T|T|T|";
         Ddo_grid_Columnssortvalues = "2|1|3|4|";
         Ddo_grid_Columnids = "1:WWPFormTitle|2:WWPFormReferenceName|3:WWPFormDate|4:WWPFormVersionNumber|5:WWPFormLatestVersionNumber";
         Ddo_grid_Gridinternalname = "";
         Gridpaginationbar_Rowsperpagecaption = "WWP_PagingRowsPerPage";
         Gridpaginationbar_Emptygridcaption = "WWP_PagingEmptyGridCaption";
         Gridpaginationbar_Caption = "Page <CURRENT_PAGE> of <TOTAL_PAGES>";
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

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"AV24GeneralDynamicFormids","fld":"vGENERALDYNAMICFORMIDS"},{"av":"AV20FilledOutForms","fld":"vFILLEDOUTFORMS","pic":"ZZZ9","hsh":true},{"av":"sPrefix"},{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV53OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV55OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV21FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV79WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"AV81Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV65TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV66TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV61TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV62TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV69TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV70TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV63TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV64TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV78WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV77WWPFormId","fld":"vWWPFORMID","pic":"ZZZ9","hsh":true},{"av":"AV5LocationId","fld":"vLOCATIONID","hsh":true},{"av":"AV6OrganisationId","fld":"vORGANISATIONID","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"av":"cmbWWPFormType"},{"av":"A240WWPFormType","fld":"WWPFORMTYPE","pic":"9","hsh":true}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPFormTitle_Visible","ctrl":"WWPFORMTITLE","prop":"Visible"},{"av":"edtWWPFormReferenceName_Visible","ctrl":"WWPFORMREFERENCENAME","prop":"Visible"},{"av":"edtWWPFormDate_Visible","ctrl":"WWPFORMDATE","prop":"Visible"},{"av":"edtWWPFormVersionNumber_Visible","ctrl":"WWPFORMVERSIONNUMBER","prop":"Visible"},{"av":"edtWWPFormLatestVersionNumber_Visible","ctrl":"WWPFORMLATESTVERSIONNUMBER","prop":"Visible"},{"av":"AV27GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV28GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV26GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"ctrl":"BTNUINSERT","prop":"Visible"},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"ctrl":"BTNIMPORTFORM","prop":"Visible"},{"av":"AV47ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV29GridState","fld":"vGRIDSTATE"},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"}]}""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEPAGE","""{"handler":"E12B92","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV53OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV55OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV21FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV79WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"AV24GeneralDynamicFormids","fld":"vGENERALDYNAMICFORMIDS"},{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV81Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV65TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV66TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV61TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV62TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV69TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV70TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV63TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV64TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV78WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV20FilledOutForms","fld":"vFILLEDOUTFORMS","pic":"ZZZ9","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV77WWPFormId","fld":"vWWPFORMID","pic":"ZZZ9","hsh":true},{"av":"AV5LocationId","fld":"vLOCATIONID","hsh":true},{"av":"AV6OrganisationId","fld":"vORGANISATIONID","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"av":"cmbWWPFormType"},{"av":"A240WWPFormType","fld":"WWPFORMTYPE","pic":"9","hsh":true},{"av":"sPrefix"},{"av":"Gridpaginationbar_Selectedpage","ctrl":"GRIDPAGINATIONBAR","prop":"SelectedPage"}]}""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEROWSPERPAGE","""{"handler":"E13B92","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV53OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV55OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV21FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV79WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"AV24GeneralDynamicFormids","fld":"vGENERALDYNAMICFORMIDS"},{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV81Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV65TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV66TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV61TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV62TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV69TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV70TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV63TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV64TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV78WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV20FilledOutForms","fld":"vFILLEDOUTFORMS","pic":"ZZZ9","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV77WWPFormId","fld":"vWWPFORMID","pic":"ZZZ9","hsh":true},{"av":"AV5LocationId","fld":"vLOCATIONID","hsh":true},{"av":"AV6OrganisationId","fld":"vORGANISATIONID","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"av":"cmbWWPFormType"},{"av":"A240WWPFormType","fld":"WWPFORMTYPE","pic":"9","hsh":true},{"av":"sPrefix"},{"av":"Gridpaginationbar_Rowsperpageselectedvalue","ctrl":"GRIDPAGINATIONBAR","prop":"RowsPerPageSelectedValue"}]""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEROWSPERPAGE",""","oparms":[{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"}]}""");
         setEventMetadata("DDO_GRID.ONOPTIONCLICKED","""{"handler":"E14B92","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV53OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV55OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV21FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV79WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"AV24GeneralDynamicFormids","fld":"vGENERALDYNAMICFORMIDS"},{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV81Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV65TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV66TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV61TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV62TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV69TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV70TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV63TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV64TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV78WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV20FilledOutForms","fld":"vFILLEDOUTFORMS","pic":"ZZZ9","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV77WWPFormId","fld":"vWWPFORMID","pic":"ZZZ9","hsh":true},{"av":"AV5LocationId","fld":"vLOCATIONID","hsh":true},{"av":"AV6OrganisationId","fld":"vORGANISATIONID","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"av":"cmbWWPFormType"},{"av":"A240WWPFormType","fld":"WWPFORMTYPE","pic":"9","hsh":true},{"av":"sPrefix"},{"av":"Ddo_grid_Activeeventkey","ctrl":"DDO_GRID","prop":"ActiveEventKey"},{"av":"Ddo_grid_Selectedvalue_get","ctrl":"DDO_GRID","prop":"SelectedValue_get"},{"av":"Ddo_grid_Selectedcolumn","ctrl":"DDO_GRID","prop":"SelectedColumn"},{"av":"Ddo_grid_Filteredtext_get","ctrl":"DDO_GRID","prop":"FilteredText_get"},{"av":"Ddo_grid_Filteredtextto_get","ctrl":"DDO_GRID","prop":"FilteredTextTo_get"}]""");
         setEventMetadata("DDO_GRID.ONOPTIONCLICKED",""","oparms":[{"av":"AV53OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV55OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV65TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV66TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV61TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV62TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV69TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV70TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV63TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV64TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"Ddo_grid_Sortedstatus","ctrl":"DDO_GRID","prop":"SortedStatus"}]}""");
         setEventMetadata("GRID.LOAD","""{"handler":"E24B92","iparms":[{"av":"AV79WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"A206WWPFormId","fld":"WWPFORMID","pic":"ZZZ9","hsh":true},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV20FilledOutForms","fld":"vFILLEDOUTFORMS","pic":"ZZZ9","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV77WWPFormId","fld":"vWWPFORMID","pic":"ZZZ9","hsh":true}]""");
         setEventMetadata("GRID.LOAD",""","oparms":[{"av":"AV77WWPFormId","fld":"vWWPFORMID","pic":"ZZZ9","hsh":true},{"av":"cmbavGridactiongroup1"},{"av":"AV25GridActionGroup1","fld":"vGRIDACTIONGROUP1","pic":"ZZZ9"},{"av":"AV20FilledOutForms","fld":"vFILLEDOUTFORMS","pic":"ZZZ9","hsh":true}]}""");
         setEventMetadata("DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED","""{"handler":"E15B92","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV53OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV55OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV21FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV79WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"AV24GeneralDynamicFormids","fld":"vGENERALDYNAMICFORMIDS"},{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV81Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV65TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV66TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV61TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV62TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV69TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV70TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV63TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV64TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV78WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV20FilledOutForms","fld":"vFILLEDOUTFORMS","pic":"ZZZ9","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV77WWPFormId","fld":"vWWPFORMID","pic":"ZZZ9","hsh":true},{"av":"AV5LocationId","fld":"vLOCATIONID","hsh":true},{"av":"AV6OrganisationId","fld":"vORGANISATIONID","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"av":"cmbWWPFormType"},{"av":"A240WWPFormType","fld":"WWPFORMTYPE","pic":"9","hsh":true},{"av":"sPrefix"},{"av":"Ddo_gridcolumnsselector_Columnsselectorvalues","ctrl":"DDO_GRIDCOLUMNSSELECTOR","prop":"ColumnsSelectorValues"}]""");
         setEventMetadata("DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED",""","oparms":[{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"edtWWPFormTitle_Visible","ctrl":"WWPFORMTITLE","prop":"Visible"},{"av":"edtWWPFormReferenceName_Visible","ctrl":"WWPFORMREFERENCENAME","prop":"Visible"},{"av":"edtWWPFormDate_Visible","ctrl":"WWPFORMDATE","prop":"Visible"},{"av":"edtWWPFormVersionNumber_Visible","ctrl":"WWPFORMVERSIONNUMBER","prop":"Visible"},{"av":"edtWWPFormLatestVersionNumber_Visible","ctrl":"WWPFORMLATESTVERSIONNUMBER","prop":"Visible"},{"av":"AV27GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV28GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV26GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"ctrl":"BTNUINSERT","prop":"Visible"},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"ctrl":"BTNIMPORTFORM","prop":"Visible"},{"av":"AV47ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV29GridState","fld":"vGRIDSTATE"},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"}]}""");
         setEventMetadata("DDO_MANAGEFILTERS.ONOPTIONCLICKED","""{"handler":"E11B92","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV53OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV55OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV21FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV79WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"AV24GeneralDynamicFormids","fld":"vGENERALDYNAMICFORMIDS"},{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV81Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV65TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV66TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV61TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV62TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV69TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV70TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV63TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV64TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV78WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV20FilledOutForms","fld":"vFILLEDOUTFORMS","pic":"ZZZ9","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV77WWPFormId","fld":"vWWPFORMID","pic":"ZZZ9","hsh":true},{"av":"AV5LocationId","fld":"vLOCATIONID","hsh":true},{"av":"AV6OrganisationId","fld":"vORGANISATIONID","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"av":"cmbWWPFormType"},{"av":"A240WWPFormType","fld":"WWPFORMTYPE","pic":"9","hsh":true},{"av":"sPrefix"},{"av":"Ddo_managefilters_Activeeventkey","ctrl":"DDO_MANAGEFILTERS","prop":"ActiveEventKey"},{"av":"AV29GridState","fld":"vGRIDSTATE"},{"av":"AV14DDO_WWPFormDateAuxDate","fld":"vDDO_WWPFORMDATEAUXDATE"},{"av":"AV16DDO_WWPFormDateAuxDateTo","fld":"vDDO_WWPFORMDATEAUXDATETO"}]""");
         setEventMetadata("DDO_MANAGEFILTERS.ONOPTIONCLICKED",""","oparms":[{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV29GridState","fld":"vGRIDSTATE"},{"av":"AV53OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV55OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV21FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV65TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV66TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV61TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV62TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV69TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV70TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV63TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV64TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"Ddo_grid_Selectedvalue_set","ctrl":"DDO_GRID","prop":"SelectedValue_set"},{"av":"Ddo_grid_Filteredtext_set","ctrl":"DDO_GRID","prop":"FilteredText_set"},{"av":"Ddo_grid_Filteredtextto_set","ctrl":"DDO_GRID","prop":"FilteredTextTo_set"},{"av":"Ddo_grid_Sortedstatus","ctrl":"DDO_GRID","prop":"SortedStatus"},{"av":"AV14DDO_WWPFormDateAuxDate","fld":"vDDO_WWPFORMDATEAUXDATE"},{"av":"AV16DDO_WWPFormDateAuxDateTo","fld":"vDDO_WWPFORMDATEAUXDATETO"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPFormTitle_Visible","ctrl":"WWPFORMTITLE","prop":"Visible"},{"av":"edtWWPFormReferenceName_Visible","ctrl":"WWPFORMREFERENCENAME","prop":"Visible"},{"av":"edtWWPFormDate_Visible","ctrl":"WWPFORMDATE","prop":"Visible"},{"av":"edtWWPFormVersionNumber_Visible","ctrl":"WWPFORMVERSIONNUMBER","prop":"Visible"},{"av":"edtWWPFormLatestVersionNumber_Visible","ctrl":"WWPFORMLATESTVERSIONNUMBER","prop":"Visible"},{"av":"AV27GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV28GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV26GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"ctrl":"BTNUINSERT","prop":"Visible"},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"ctrl":"BTNIMPORTFORM","prop":"Visible"},{"av":"AV47ManageFiltersData","fld":"vMANAGEFILTERSDATA"}]}""");
         setEventMetadata("VGRIDACTIONGROUP1.CLICK","""{"handler":"E25B92","iparms":[{"av":"cmbavGridactiongroup1"},{"av":"AV25GridActionGroup1","fld":"vGRIDACTIONGROUP1","pic":"ZZZ9"},{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV53OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV55OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV21FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV79WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"AV24GeneralDynamicFormids","fld":"vGENERALDYNAMICFORMIDS"},{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV81Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV65TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV66TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV61TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV62TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV69TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV70TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV63TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV64TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV78WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV20FilledOutForms","fld":"vFILLEDOUTFORMS","pic":"ZZZ9","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV77WWPFormId","fld":"vWWPFORMID","pic":"ZZZ9","hsh":true},{"av":"AV5LocationId","fld":"vLOCATIONID","hsh":true},{"av":"AV6OrganisationId","fld":"vORGANISATIONID","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"av":"cmbWWPFormType"},{"av":"A240WWPFormType","fld":"WWPFORMTYPE","pic":"9","hsh":true},{"av":"sPrefix"},{"av":"A206WWPFormId","fld":"WWPFORMID","pic":"ZZZ9","hsh":true},{"av":"A209WWPFormTitle","fld":"WWPFORMTITLE","hsh":true},{"av":"A207WWPFormVersionNumber","fld":"WWPFORMVERSIONNUMBER","pic":"ZZZ9","hsh":true},{"av":"A208WWPFormReferenceName","fld":"WWPFORMREFERENCENAME","hsh":true},{"av":"AV58ResultMsg","fld":"vRESULTMSG"}]""");
         setEventMetadata("VGRIDACTIONGROUP1.CLICK",""","oparms":[{"av":"cmbavGridactiongroup1"},{"av":"AV25GridActionGroup1","fld":"vGRIDACTIONGROUP1","pic":"ZZZ9"},{"av":"Dvelop_confirmpanel_udelete_Confirmationtext","ctrl":"DVELOP_CONFIRMPANEL_UDELETE","prop":"ConfirmationText"},{"av":"AV82Wwpformid_selected","fld":"vWWPFORMID_SELECTED","pic":"9999"},{"av":"AV83Wwpformversionnumber_selected","fld":"vWWPFORMVERSIONNUMBER_SELECTED","pic":"9999"},{"av":"AV58ResultMsg","fld":"vRESULTMSG"},{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPFormTitle_Visible","ctrl":"WWPFORMTITLE","prop":"Visible"},{"av":"edtWWPFormReferenceName_Visible","ctrl":"WWPFORMREFERENCENAME","prop":"Visible"},{"av":"edtWWPFormDate_Visible","ctrl":"WWPFORMDATE","prop":"Visible"},{"av":"edtWWPFormVersionNumber_Visible","ctrl":"WWPFORMVERSIONNUMBER","prop":"Visible"},{"av":"edtWWPFormLatestVersionNumber_Visible","ctrl":"WWPFORMLATESTVERSIONNUMBER","prop":"Visible"},{"av":"AV27GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV28GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV26GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"ctrl":"BTNUINSERT","prop":"Visible"},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"ctrl":"BTNIMPORTFORM","prop":"Visible"},{"av":"AV47ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV29GridState","fld":"vGRIDSTATE"},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"}]}""");
         setEventMetadata("DVELOP_CONFIRMPANEL_UDELETE.CLOSE","""{"handler":"E16B92","iparms":[{"av":"Dvelop_confirmpanel_udelete_Result","ctrl":"DVELOP_CONFIRMPANEL_UDELETE","prop":"Result"},{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV53OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV55OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV21FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV79WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"AV24GeneralDynamicFormids","fld":"vGENERALDYNAMICFORMIDS"},{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV81Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV65TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV66TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV61TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV62TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV69TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV70TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV63TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV64TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV78WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV20FilledOutForms","fld":"vFILLEDOUTFORMS","pic":"ZZZ9","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV77WWPFormId","fld":"vWWPFORMID","pic":"ZZZ9","hsh":true},{"av":"AV5LocationId","fld":"vLOCATIONID","hsh":true},{"av":"AV6OrganisationId","fld":"vORGANISATIONID","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"av":"cmbWWPFormType"},{"av":"A240WWPFormType","fld":"WWPFORMTYPE","pic":"9","hsh":true},{"av":"sPrefix"},{"av":"AV82Wwpformid_selected","fld":"vWWPFORMID_SELECTED","pic":"9999"},{"av":"AV83Wwpformversionnumber_selected","fld":"vWWPFORMVERSIONNUMBER_SELECTED","pic":"9999"}]""");
         setEventMetadata("DVELOP_CONFIRMPANEL_UDELETE.CLOSE",""","oparms":[{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPFormTitle_Visible","ctrl":"WWPFORMTITLE","prop":"Visible"},{"av":"edtWWPFormReferenceName_Visible","ctrl":"WWPFORMREFERENCENAME","prop":"Visible"},{"av":"edtWWPFormDate_Visible","ctrl":"WWPFORMDATE","prop":"Visible"},{"av":"edtWWPFormVersionNumber_Visible","ctrl":"WWPFORMVERSIONNUMBER","prop":"Visible"},{"av":"edtWWPFormLatestVersionNumber_Visible","ctrl":"WWPFORMLATESTVERSIONNUMBER","prop":"Visible"},{"av":"AV27GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV28GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV26GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"ctrl":"BTNUINSERT","prop":"Visible"},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"ctrl":"BTNIMPORTFORM","prop":"Visible"},{"av":"AV47ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV29GridState","fld":"vGRIDSTATE"},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"}]}""");
         setEventMetadata("'DOUINSERT'","""{"handler":"E19B92","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV53OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV55OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV21FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV79WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"AV24GeneralDynamicFormids","fld":"vGENERALDYNAMICFORMIDS"},{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV81Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV65TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV66TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV61TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV62TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV69TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV70TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV63TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV64TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV78WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV20FilledOutForms","fld":"vFILLEDOUTFORMS","pic":"ZZZ9","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV77WWPFormId","fld":"vWWPFORMID","pic":"ZZZ9","hsh":true},{"av":"AV5LocationId","fld":"vLOCATIONID","hsh":true},{"av":"AV6OrganisationId","fld":"vORGANISATIONID","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"av":"cmbWWPFormType"},{"av":"A240WWPFormType","fld":"WWPFORMTYPE","pic":"9","hsh":true},{"av":"sPrefix"}]""");
         setEventMetadata("'DOUINSERT'",""","oparms":[{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPFormTitle_Visible","ctrl":"WWPFORMTITLE","prop":"Visible"},{"av":"edtWWPFormReferenceName_Visible","ctrl":"WWPFORMREFERENCENAME","prop":"Visible"},{"av":"edtWWPFormDate_Visible","ctrl":"WWPFORMDATE","prop":"Visible"},{"av":"edtWWPFormVersionNumber_Visible","ctrl":"WWPFORMVERSIONNUMBER","prop":"Visible"},{"av":"edtWWPFormLatestVersionNumber_Visible","ctrl":"WWPFORMLATESTVERSIONNUMBER","prop":"Visible"},{"av":"AV27GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV28GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV26GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"ctrl":"BTNUINSERT","prop":"Visible"},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"ctrl":"BTNIMPORTFORM","prop":"Visible"},{"av":"AV47ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV29GridState","fld":"vGRIDSTATE"},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"}]}""");
         setEventMetadata("'DOINSERT'","""{"handler":"E20B92","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV53OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV55OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV21FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV79WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"AV24GeneralDynamicFormids","fld":"vGENERALDYNAMICFORMIDS"},{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV81Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV65TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV66TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV61TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV62TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV69TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV70TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV63TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV64TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV78WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV20FilledOutForms","fld":"vFILLEDOUTFORMS","pic":"ZZZ9","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV77WWPFormId","fld":"vWWPFORMID","pic":"ZZZ9","hsh":true},{"av":"AV5LocationId","fld":"vLOCATIONID","hsh":true},{"av":"AV6OrganisationId","fld":"vORGANISATIONID","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"av":"cmbWWPFormType"},{"av":"A240WWPFormType","fld":"WWPFORMTYPE","pic":"9","hsh":true},{"av":"sPrefix"}]""");
         setEventMetadata("'DOINSERT'",""","oparms":[{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPFormTitle_Visible","ctrl":"WWPFORMTITLE","prop":"Visible"},{"av":"edtWWPFormReferenceName_Visible","ctrl":"WWPFORMREFERENCENAME","prop":"Visible"},{"av":"edtWWPFormDate_Visible","ctrl":"WWPFORMDATE","prop":"Visible"},{"av":"edtWWPFormVersionNumber_Visible","ctrl":"WWPFORMVERSIONNUMBER","prop":"Visible"},{"av":"edtWWPFormLatestVersionNumber_Visible","ctrl":"WWPFORMLATESTVERSIONNUMBER","prop":"Visible"},{"av":"AV27GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV28GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV26GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"ctrl":"BTNUINSERT","prop":"Visible"},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"ctrl":"BTNIMPORTFORM","prop":"Visible"},{"av":"AV47ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV29GridState","fld":"vGRIDSTATE"},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"}]}""");
         setEventMetadata("'DOIMPORTFORM'","""{"handler":"E21B92","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV53OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV55OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV21FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV79WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"AV24GeneralDynamicFormids","fld":"vGENERALDYNAMICFORMIDS"},{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV81Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV65TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV66TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV61TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV62TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV69TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV70TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV63TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV64TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV78WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV20FilledOutForms","fld":"vFILLEDOUTFORMS","pic":"ZZZ9","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV77WWPFormId","fld":"vWWPFORMID","pic":"ZZZ9","hsh":true},{"av":"AV5LocationId","fld":"vLOCATIONID","hsh":true},{"av":"AV6OrganisationId","fld":"vORGANISATIONID","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"av":"cmbWWPFormType"},{"av":"A240WWPFormType","fld":"WWPFORMTYPE","pic":"9","hsh":true},{"av":"sPrefix"}]""");
         setEventMetadata("'DOIMPORTFORM'",""","oparms":[{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPFormTitle_Visible","ctrl":"WWPFORMTITLE","prop":"Visible"},{"av":"edtWWPFormReferenceName_Visible","ctrl":"WWPFORMREFERENCENAME","prop":"Visible"},{"av":"edtWWPFormDate_Visible","ctrl":"WWPFORMDATE","prop":"Visible"},{"av":"edtWWPFormVersionNumber_Visible","ctrl":"WWPFORMVERSIONNUMBER","prop":"Visible"},{"av":"edtWWPFormLatestVersionNumber_Visible","ctrl":"WWPFORMLATESTVERSIONNUMBER","prop":"Visible"},{"av":"AV27GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV28GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV26GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"ctrl":"BTNUINSERT","prop":"Visible"},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"ctrl":"BTNIMPORTFORM","prop":"Visible"},{"av":"AV47ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV29GridState","fld":"vGRIDSTATE"},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"}]}""");
         setEventMetadata("IMPORTFORM_MODAL.CLOSE","""{"handler":"E18B92","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV53OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV55OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV21FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV79WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"AV24GeneralDynamicFormids","fld":"vGENERALDYNAMICFORMIDS"},{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV81Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV65TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV66TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV61TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV62TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV69TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV70TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV63TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV64TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV78WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV20FilledOutForms","fld":"vFILLEDOUTFORMS","pic":"ZZZ9","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV77WWPFormId","fld":"vWWPFORMID","pic":"ZZZ9","hsh":true},{"av":"AV5LocationId","fld":"vLOCATIONID","hsh":true},{"av":"AV6OrganisationId","fld":"vORGANISATIONID","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"av":"cmbWWPFormType"},{"av":"A240WWPFormType","fld":"WWPFORMTYPE","pic":"9","hsh":true},{"av":"sPrefix"}]""");
         setEventMetadata("IMPORTFORM_MODAL.CLOSE",""","oparms":[{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPFormTitle_Visible","ctrl":"WWPFORMTITLE","prop":"Visible"},{"av":"edtWWPFormReferenceName_Visible","ctrl":"WWPFORMREFERENCENAME","prop":"Visible"},{"av":"edtWWPFormDate_Visible","ctrl":"WWPFORMDATE","prop":"Visible"},{"av":"edtWWPFormVersionNumber_Visible","ctrl":"WWPFORMVERSIONNUMBER","prop":"Visible"},{"av":"edtWWPFormLatestVersionNumber_Visible","ctrl":"WWPFORMLATESTVERSIONNUMBER","prop":"Visible"},{"av":"AV27GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV28GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV26GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"ctrl":"BTNUINSERT","prop":"Visible"},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"ctrl":"BTNIMPORTFORM","prop":"Visible"},{"av":"AV47ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV29GridState","fld":"vGRIDSTATE"},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"}]}""");
         setEventMetadata("UCOPYTOLOCATION_MODAL.CLOSE","""{"handler":"E17B92","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV53OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV55OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV21FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV79WWPFormType","fld":"vWWPFORMTYPE","pic":"9"},{"av":"AV24GeneralDynamicFormids","fld":"vGENERALDYNAMICFORMIDS"},{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV81Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"},{"av":"AV65TFWWPFormReferenceName","fld":"vTFWWPFORMREFERENCENAME"},{"av":"AV66TFWWPFormReferenceName_Sel","fld":"vTFWWPFORMREFERENCENAME_SEL"},{"av":"AV61TFWWPFormDate","fld":"vTFWWPFORMDATE","pic":"99/99/99 99:99"},{"av":"AV62TFWWPFormDate_To","fld":"vTFWWPFORMDATE_TO","pic":"99/99/99 99:99"},{"av":"AV69TFWWPFormVersionNumber","fld":"vTFWWPFORMVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV70TFWWPFormVersionNumber_To","fld":"vTFWWPFORMVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV63TFWWPFormLatestVersionNumber","fld":"vTFWWPFORMLATESTVERSIONNUMBER","pic":"ZZZ9"},{"av":"AV64TFWWPFormLatestVersionNumber_To","fld":"vTFWWPFORMLATESTVERSIONNUMBER_TO","pic":"ZZZ9"},{"av":"AV78WWPFormIsForDynamicValidations","fld":"vWWPFORMISFORDYNAMICVALIDATIONS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV20FilledOutForms","fld":"vFILLEDOUTFORMS","pic":"ZZZ9","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV77WWPFormId","fld":"vWWPFORMID","pic":"ZZZ9","hsh":true},{"av":"AV5LocationId","fld":"vLOCATIONID","hsh":true},{"av":"AV6OrganisationId","fld":"vORGANISATIONID","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"av":"cmbWWPFormType"},{"av":"A240WWPFormType","fld":"WWPFORMTYPE","pic":"9","hsh":true},{"av":"sPrefix"},{"av":"Ucopytolocation_modal_Result","ctrl":"UCOPYTOLOCATION_MODAL","prop":"Result"}]""");
         setEventMetadata("UCOPYTOLOCATION_MODAL.CLOSE",""","oparms":[{"av":"AV48ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV9ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPFormTitle_Visible","ctrl":"WWPFORMTITLE","prop":"Visible"},{"av":"edtWWPFormReferenceName_Visible","ctrl":"WWPFORMREFERENCENAME","prop":"Visible"},{"av":"edtWWPFormDate_Visible","ctrl":"WWPFORMDATE","prop":"Visible"},{"av":"edtWWPFormVersionNumber_Visible","ctrl":"WWPFORMVERSIONNUMBER","prop":"Visible"},{"av":"edtWWPFormLatestVersionNumber_Visible","ctrl":"WWPFORMLATESTVERSIONNUMBER","prop":"Visible"},{"av":"AV27GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV28GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV26GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV46IsAuthorized_UUpdate","fld":"vISAUTHORIZED_UUPDATE","hsh":true},{"av":"AV42IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV35IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV36IsAuthorized_ExportForm","fld":"vISAUTHORIZED_EXPORTFORM","hsh":true},{"av":"AV38IsAuthorized_FillOutAForm","fld":"vISAUTHORIZED_FILLOUTAFORM","hsh":true},{"av":"AV37IsAuthorized_FilledOutForms","fld":"vISAUTHORIZED_FILLEDOUTFORMS","hsh":true},{"av":"AV33IsAuthorized_Copy","fld":"vISAUTHORIZED_COPY","hsh":true},{"av":"AV41IsAuthorized_UCopyToLocation","fld":"vISAUTHORIZED_UCOPYTOLOCATION","hsh":true},{"av":"AV43IsAuthorized_UDirectCopyToLocation","fld":"vISAUTHORIZED_UDIRECTCOPYTOLOCATION","hsh":true},{"av":"AV44IsAuthorized_UInsert","fld":"vISAUTHORIZED_UINSERT","hsh":true},{"ctrl":"BTNUINSERT","prop":"Visible"},{"av":"AV40IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV39IsAuthorized_ImportForm","fld":"vISAUTHORIZED_IMPORTFORM","hsh":true},{"ctrl":"BTNIMPORTFORM","prop":"Visible"},{"av":"AV47ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV29GridState","fld":"vGRIDSTATE"},{"av":"AV67TFWWPFormTitle","fld":"vTFWWPFORMTITLE"},{"av":"AV68TFWWPFormTitle_Sel","fld":"vTFWWPFORMTITLE_SEL"}]}""");
         setEventMetadata("VALID_WWPFORMID","""{"handler":"Valid_Wwpformid","iparms":[]}""");
         setEventMetadata("VALID_WWPFORMTITLE","""{"handler":"Valid_Wwpformtitle","iparms":[]}""");
         setEventMetadata("VALID_WWPFORMREFERENCENAME","""{"handler":"Valid_Wwpformreferencename","iparms":[]}""");
         setEventMetadata("VALID_WWPFORMVERSIONNUMBER","""{"handler":"Valid_Wwpformversionnumber","iparms":[]}""");
         setEventMetadata("VALID_WWPFORMLATESTVERSIONNUMBER","""{"handler":"Valid_Wwpformlatestversionnumber","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gridactiongroup1","iparms":[]}""");
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
         Dvelop_confirmpanel_udelete_Result = "";
         Ucopytolocation_modal_Result = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         AV21FilterFullText = "";
         AV24GeneralDynamicFormids = new GxSimpleCollection<short>();
         AV9ColumnsSelector = new WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector(context);
         AV81Pgmname = "";
         AV67TFWWPFormTitle = "";
         AV68TFWWPFormTitle_Sel = "";
         AV65TFWWPFormReferenceName = "";
         AV66TFWWPFormReferenceName_Sel = "";
         AV61TFWWPFormDate = (DateTime)(DateTime.MinValue);
         AV62TFWWPFormDate_To = (DateTime)(DateTime.MinValue);
         AV5LocationId = Guid.Empty;
         AV6OrganisationId = Guid.Empty;
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         forbiddenHiddens = new GXProperties();
         AV47ManageFiltersData = new GXBaseCollection<WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV26GridAppliedFilters = "";
         AV13DDO_TitleSettingsIcons = new WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV14DDO_WWPFormDateAuxDate = DateTime.MinValue;
         AV16DDO_WWPFormDateAuxDateTo = DateTime.MinValue;
         AV29GridState = new WorkWithPlus.workwithplus_web.SdtWWPGridState(context);
         AV58ResultMsg = "";
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
         bttBtnuinsert_Jsonclick = "";
         bttBtninsert_Jsonclick = "";
         bttBtneditcolumns_Jsonclick = "";
         bttBtnimportform_Jsonclick = "";
         ucDdo_managefilters = new GXUserControl();
         Ddo_managefilters_Caption = "";
         GridContainer = new GXWebGrid( context);
         sStyleString = "";
         ucGridpaginationbar = new GXUserControl();
         ucDdo_grid = new GXUserControl();
         ucDdo_gridcolumnsselector = new GXUserControl();
         ucGrid_empowerer = new GXUserControl();
         WebComp_Wwpaux_wc_Component = "";
         OldWwpaux_wc = "";
         AV15DDO_WWPFormDateAuxDateText = "";
         ucTfwwpformdate_rangepicker = new GXUserControl();
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         A209WWPFormTitle = "";
         A208WWPFormReferenceName = "";
         A231WWPFormDate = (DateTime)(DateTime.MinValue);
         lV67TFWWPFormTitle = "";
         lV65TFWWPFormReferenceName = "";
         H00B92_A240WWPFormType = new short[1] ;
         H00B92_A207WWPFormVersionNumber = new short[1] ;
         H00B92_A231WWPFormDate = new DateTime[] {DateTime.MinValue} ;
         H00B92_A208WWPFormReferenceName = new string[] {""} ;
         H00B92_A209WWPFormTitle = new string[] {""} ;
         H00B92_A206WWPFormId = new short[1] ;
         H00B93_A240WWPFormType = new short[1] ;
         H00B93_A207WWPFormVersionNumber = new short[1] ;
         H00B93_A231WWPFormDate = new DateTime[] {DateTime.MinValue} ;
         H00B93_A208WWPFormReferenceName = new string[] {""} ;
         H00B93_A209WWPFormTitle = new string[] {""} ;
         H00B93_A206WWPFormId = new short[1] ;
         hsh = "";
         GXt_guid2 = Guid.Empty;
         GXt_objcol_int3 = new GxSimpleCollection<short>();
         AV23GAMSession = new GeneXus.Programs.genexussecurity.SdtGAMSession(context);
         AV22GAMErrors = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "GeneXus.Programs");
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons4 = new WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV75WWPContext = new GeneXus.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV60Session = context.GetSession();
         AV11ColumnsSelectorXML = "";
         GridRow = new GXWebRow();
         AV49ManageFiltersXml = "";
         AV74UserCustomValue = "";
         AV10ColumnsSelectorAux = new WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector(context);
         GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item7 = new GXBaseCollection<WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         ucDvelop_confirmpanel_udelete = new GXUserControl();
         AV51Messages = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         AV50Message = new GeneXus.Utils.SdtMessages_Message(context);
         AV76WWPForm = new SdtUForm(context);
         AV8WWPFormReferenceName = "";
         AV52NewWWPForm = new SdtUForm(context);
         H00B94_A207WWPFormVersionNumber = new short[1] ;
         H00B94_A206WWPFormId = new short[1] ;
         AV19Element = new SdtUForm_Element(context);
         AV87GXV3 = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         H00B95_A207WWPFormVersionNumber = new short[1] ;
         H00B95_A206WWPFormId = new short[1] ;
         AV7Trn_LocationDynamicForm = new SdtTrn_LocationDynamicForm(context);
         AV91GXV6 = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         AV93GXV8 = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         AV30GridStateFilterValue = new WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue(context);
         GXt_char8 = "";
         GXt_char5 = "";
         AV71TrnContext = new WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext(context);
         AV31HTTPRequest = new GxHttpRequest( context);
         AV72TrnContextAtt = new WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext_Attribute(context);
         H00B96_AV20FilledOutForms = new short[1] ;
         ucImportform_modal = new GXUserControl();
         ucUcopytolocation_modal = new GXUserControl();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlAV79WWPFormType = "";
         sCtrlAV78WWPFormIsForDynamicValidations = "";
         subGrid_Linesclass = "";
         ROClassString = "";
         GXCCtl = "";
         GridColumn = new GXWebColumn();
         pr_datastore1 = new DataStoreProvider(context, new GeneXus.Programs.wp_dynamicformelementuformwc__datastore1(),
            new Object[][] {
            }
         );
         pr_gam = new DataStoreProvider(context, new GeneXus.Programs.wp_dynamicformelementuformwc__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.wp_dynamicformelementuformwc__default(),
            new Object[][] {
                new Object[] {
               H00B92_A240WWPFormType, H00B92_A207WWPFormVersionNumber, H00B92_A231WWPFormDate, H00B92_A208WWPFormReferenceName, H00B92_A209WWPFormTitle, H00B92_A206WWPFormId
               }
               , new Object[] {
               H00B93_A240WWPFormType, H00B93_A207WWPFormVersionNumber, H00B93_A231WWPFormDate, H00B93_A208WWPFormReferenceName, H00B93_A209WWPFormTitle, H00B93_A206WWPFormId
               }
               , new Object[] {
               H00B94_A207WWPFormVersionNumber, H00B94_A206WWPFormId
               }
               , new Object[] {
               H00B95_A207WWPFormVersionNumber, H00B95_A206WWPFormId
               }
               , new Object[] {
               H00B96_AV20FilledOutForms
               }
            }
         );
         WebComp_Wwpaux_wc = new GeneXus.Http.GXNullWebComponent();
         AV81Pgmname = "WP_DynamicFormElementUFormWC";
         /* GeneXus formulas. */
         AV81Pgmname = "WP_DynamicFormElementUFormWC";
         edtavFilledoutforms_Enabled = 0;
      }

      private short AV79WWPFormType ;
      private short wcpOAV79WWPFormType ;
      private short GRID_nEOF ;
      private short nRcdExists_5 ;
      private short nIsMod_5 ;
      private short nRcdExists_4 ;
      private short nIsMod_4 ;
      private short nRcdExists_3 ;
      private short nIsMod_3 ;
      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short AV53OrderedBy ;
      private short AV48ManageFiltersExecutionStep ;
      private short AV69TFWWPFormVersionNumber ;
      private short AV70TFWWPFormVersionNumber_To ;
      private short AV63TFWWPFormLatestVersionNumber ;
      private short AV64TFWWPFormLatestVersionNumber_To ;
      private short AV20FilledOutForms ;
      private short AV77WWPFormId ;
      private short A240WWPFormType ;
      private short AV82Wwpformid_selected ;
      private short AV83Wwpformversionnumber_selected ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short A206WWPFormId ;
      private short A207WWPFormVersionNumber ;
      private short A219WWPFormLatestVersionNumber ;
      private short AV25GridActionGroup1 ;
      private short nCmpId ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGrid_Backcolorstyle ;
      private short subGrid_Sortable ;
      private short GXt_int1 ;
      private short AV12CopyNumber ;
      private short cV20FilledOutForms ;
      private short nGXWrapped ;
      private short subGrid_Backstyle ;
      private short subGrid_Titlebackstyle ;
      private short subGrid_Allowselection ;
      private short subGrid_Allowhovering ;
      private short subGrid_Allowcollapsing ;
      private short subGrid_Collapsed ;
      private int subGrid_Rows ;
      private int Gridpaginationbar_Rowsperpageselectedvalue ;
      private int nRC_GXsfl_41 ;
      private int nGXsfl_41_idx=1 ;
      private int edtavFilledoutforms_Enabled ;
      private int Gridpaginationbar_Pagestoshow ;
      private int bttBtnuinsert_Visible ;
      private int bttBtninsert_Visible ;
      private int bttBtnimportform_Visible ;
      private int edtavFilterfulltext_Enabled ;
      private int subGrid_Islastpage ;
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
      private int AV57PageToGo ;
      private int AV84GXV1 ;
      private int AV86GXV2 ;
      private int AV88GXV4 ;
      private int AV90GXV5 ;
      private int AV92GXV7 ;
      private int AV94GXV9 ;
      private int AV95GXV10 ;
      private int idxLst ;
      private int subGrid_Backcolor ;
      private int subGrid_Allbackcolor ;
      private int subGrid_Titlebackcolor ;
      private int subGrid_Selectedindex ;
      private int subGrid_Selectioncolor ;
      private int subGrid_Hoveringcolor ;
      private long GRID_nFirstRecordOnPage ;
      private long AV27GridCurrentPage ;
      private long AV28GridPageCount ;
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
      private string Dvelop_confirmpanel_udelete_Result ;
      private string Ucopytolocation_modal_Result ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_41_idx="0001" ;
      private string AV81Pgmname ;
      private string edtavFilledoutforms_Internalname ;
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
      private string Dvelop_confirmpanel_udelete_Title ;
      private string Dvelop_confirmpanel_udelete_Confirmationtext ;
      private string Dvelop_confirmpanel_udelete_Yesbuttoncaption ;
      private string Dvelop_confirmpanel_udelete_Nobuttoncaption ;
      private string Dvelop_confirmpanel_udelete_Cancelbuttoncaption ;
      private string Dvelop_confirmpanel_udelete_Yesbuttonposition ;
      private string Dvelop_confirmpanel_udelete_Confirmtype ;
      private string Ucopytolocation_modal_Width ;
      private string Ucopytolocation_modal_Title ;
      private string Ucopytolocation_modal_Confirmtype ;
      private string Ucopytolocation_modal_Bodytype ;
      private string Importform_modal_Width ;
      private string Importform_modal_Title ;
      private string Importform_modal_Confirmtype ;
      private string Importform_modal_Bodytype ;
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
      private string bttBtnuinsert_Internalname ;
      private string bttBtnuinsert_Jsonclick ;
      private string bttBtninsert_Internalname ;
      private string bttBtninsert_Jsonclick ;
      private string bttBtneditcolumns_Internalname ;
      private string bttBtneditcolumns_Jsonclick ;
      private string bttBtnimportform_Internalname ;
      private string bttBtnimportform_Jsonclick ;
      private string divTablerightheader_Internalname ;
      private string Ddo_managefilters_Caption ;
      private string Ddo_managefilters_Internalname ;
      private string divTablefilters_Internalname ;
      private string edtavFilterfulltext_Internalname ;
      private string edtavFilterfulltext_Jsonclick ;
      private string divGridtablewithpaginationbar_Internalname ;
      private string sStyleString ;
      private string subGrid_Internalname ;
      private string Gridpaginationbar_Internalname ;
      private string divHtml_bottomauxiliarcontrols_Internalname ;
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
      private string edtWWPFormId_Internalname ;
      private string edtWWPFormTitle_Internalname ;
      private string edtWWPFormReferenceName_Internalname ;
      private string edtWWPFormDate_Internalname ;
      private string edtWWPFormVersionNumber_Internalname ;
      private string edtWWPFormLatestVersionNumber_Internalname ;
      private string cmbavGridactiongroup1_Internalname ;
      private string hsh ;
      private string cmbavGridactiongroup1_Class ;
      private string Dvelop_confirmpanel_udelete_Internalname ;
      private string GXt_char8 ;
      private string GXt_char5 ;
      private string tblTableimportform_modal_Internalname ;
      private string Importform_modal_Internalname ;
      private string tblTableucopytolocation_modal_Internalname ;
      private string Ucopytolocation_modal_Internalname ;
      private string tblTabledvelop_confirmpanel_udelete_Internalname ;
      private string sCtrlAV79WWPFormType ;
      private string sCtrlAV78WWPFormIsForDynamicValidations ;
      private string sGXsfl_41_fel_idx="0001" ;
      private string subGrid_Class ;
      private string subGrid_Linesclass ;
      private string ROClassString ;
      private string edtWWPFormId_Jsonclick ;
      private string edtWWPFormTitle_Jsonclick ;
      private string edtWWPFormReferenceName_Jsonclick ;
      private string edtWWPFormDate_Jsonclick ;
      private string edtWWPFormVersionNumber_Jsonclick ;
      private string edtWWPFormLatestVersionNumber_Jsonclick ;
      private string edtavFilledoutforms_Jsonclick ;
      private string GXCCtl ;
      private string cmbavGridactiongroup1_Jsonclick ;
      private string subGrid_Header ;
      private DateTime AV61TFWWPFormDate ;
      private DateTime AV62TFWWPFormDate_To ;
      private DateTime A231WWPFormDate ;
      private DateTime AV14DDO_WWPFormDateAuxDate ;
      private DateTime AV16DDO_WWPFormDateAuxDateTo ;
      private bool AV78WWPFormIsForDynamicValidations ;
      private bool wcpOAV78WWPFormIsForDynamicValidations ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV55OrderedDsc ;
      private bool AV46IsAuthorized_UUpdate ;
      private bool AV42IsAuthorized_UDelete ;
      private bool AV35IsAuthorized_Display ;
      private bool AV36IsAuthorized_ExportForm ;
      private bool AV38IsAuthorized_FillOutAForm ;
      private bool AV37IsAuthorized_FilledOutForms ;
      private bool AV33IsAuthorized_Copy ;
      private bool AV41IsAuthorized_UCopyToLocation ;
      private bool AV43IsAuthorized_UDirectCopyToLocation ;
      private bool AV44IsAuthorized_UInsert ;
      private bool AV40IsAuthorized_Insert ;
      private bool AV39IsAuthorized_ImportForm ;
      private bool bGXsfl_41_Refreshing=false ;
      private bool Gridpaginationbar_Showfirst ;
      private bool Gridpaginationbar_Showprevious ;
      private bool Gridpaginationbar_Shownext ;
      private bool Gridpaginationbar_Showlast ;
      private bool Gridpaginationbar_Rowsperpageselector ;
      private bool Grid_empowerer_Hastitlesettings ;
      private bool Grid_empowerer_Hascolumnsselector ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_refresh_fired ;
      private bool GXt_boolean6 ;
      private string AV11ColumnsSelectorXML ;
      private string AV49ManageFiltersXml ;
      private string AV74UserCustomValue ;
      private string AV21FilterFullText ;
      private string AV67TFWWPFormTitle ;
      private string AV68TFWWPFormTitle_Sel ;
      private string AV65TFWWPFormReferenceName ;
      private string AV66TFWWPFormReferenceName_Sel ;
      private string AV26GridAppliedFilters ;
      private string AV58ResultMsg ;
      private string AV15DDO_WWPFormDateAuxDateText ;
      private string A209WWPFormTitle ;
      private string A208WWPFormReferenceName ;
      private string lV67TFWWPFormTitle ;
      private string lV65TFWWPFormReferenceName ;
      private string AV8WWPFormReferenceName ;
      private Guid AV5LocationId ;
      private Guid AV6OrganisationId ;
      private Guid GXt_guid2 ;
      private IGxSession AV60Session ;
      private GXWebComponent WebComp_Wwpaux_wc ;
      private GXProperties forbiddenHiddens ;
      private GXWebGrid GridContainer ;
      private GXWebRow GridRow ;
      private GXWebColumn GridColumn ;
      private GXUserControl ucDdo_managefilters ;
      private GXUserControl ucGridpaginationbar ;
      private GXUserControl ucDdo_grid ;
      private GXUserControl ucDdo_gridcolumnsselector ;
      private GXUserControl ucGrid_empowerer ;
      private GXUserControl ucTfwwpformdate_rangepicker ;
      private GXUserControl ucDvelop_confirmpanel_udelete ;
      private GXUserControl ucImportform_modal ;
      private GXUserControl ucUcopytolocation_modal ;
      private GXWebForm Form ;
      private GxHttpRequest AV31HTTPRequest ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavGridactiongroup1 ;
      private GXCombobox cmbWWPFormType ;
      private GxSimpleCollection<short> AV24GeneralDynamicFormids ;
      private WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector AV9ColumnsSelector ;
      private GXBaseCollection<WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsData_Item> AV47ManageFiltersData ;
      private WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsTitleSettingsIcons AV13DDO_TitleSettingsIcons ;
      private WorkWithPlus.workwithplus_web.SdtWWPGridState AV29GridState ;
      private IDataStoreProvider pr_default ;
      private short[] H00B92_A240WWPFormType ;
      private short[] H00B92_A207WWPFormVersionNumber ;
      private DateTime[] H00B92_A231WWPFormDate ;
      private string[] H00B92_A208WWPFormReferenceName ;
      private string[] H00B92_A209WWPFormTitle ;
      private short[] H00B92_A206WWPFormId ;
      private short[] H00B93_A240WWPFormType ;
      private short[] H00B93_A207WWPFormVersionNumber ;
      private DateTime[] H00B93_A231WWPFormDate ;
      private string[] H00B93_A208WWPFormReferenceName ;
      private string[] H00B93_A209WWPFormTitle ;
      private short[] H00B93_A206WWPFormId ;
      private GxSimpleCollection<short> GXt_objcol_int3 ;
      private GeneXus.Programs.genexussecurity.SdtGAMSession AV23GAMSession ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV22GAMErrors ;
      private WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsTitleSettingsIcons GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons4 ;
      private GeneXus.Programs.wwpbaseobjects.SdtWWPContext AV75WWPContext ;
      private WorkWithPlus.workwithplus_web.SdtWWPColumnsSelector AV10ColumnsSelectorAux ;
      private GXBaseCollection<WorkWithPlus.workwithplus_web.SdtDVB_SDTDropDownOptionsData_Item> GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item7 ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV51Messages ;
      private GeneXus.Utils.SdtMessages_Message AV50Message ;
      private SdtUForm AV76WWPForm ;
      private SdtUForm AV52NewWWPForm ;
      private short[] H00B94_A207WWPFormVersionNumber ;
      private short[] H00B94_A206WWPFormId ;
      private SdtUForm_Element AV19Element ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV87GXV3 ;
      private short[] H00B95_A207WWPFormVersionNumber ;
      private short[] H00B95_A206WWPFormId ;
      private SdtTrn_LocationDynamicForm AV7Trn_LocationDynamicForm ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV91GXV6 ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV93GXV8 ;
      private WorkWithPlus.workwithplus_web.SdtWWPGridState_FilterValue AV30GridStateFilterValue ;
      private WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext AV71TrnContext ;
      private WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext_Attribute AV72TrnContextAtt ;
      private short[] H00B96_AV20FilledOutForms ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_datastore1 ;
      private IDataStoreProvider pr_gam ;
   }

   public class wp_dynamicformelementuformwc__datastore1 : DataStoreHelperBase, IDataStoreHelper
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

 public class wp_dynamicformelementuformwc__gam : DataStoreHelperBase, IDataStoreHelper
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

public class wp_dynamicformelementuformwc__default : DataStoreHelperBase, IDataStoreHelper
{
   protected Object[] conditional_H00B92( IGxContext context ,
                                          short A206WWPFormId ,
                                          GxSimpleCollection<short> AV24GeneralDynamicFormids ,
                                          string AV68TFWWPFormTitle_Sel ,
                                          string AV67TFWWPFormTitle ,
                                          string AV66TFWWPFormReferenceName_Sel ,
                                          string AV65TFWWPFormReferenceName ,
                                          DateTime AV61TFWWPFormDate ,
                                          DateTime AV62TFWWPFormDate_To ,
                                          short AV69TFWWPFormVersionNumber ,
                                          short AV70TFWWPFormVersionNumber_To ,
                                          string A209WWPFormTitle ,
                                          string A208WWPFormReferenceName ,
                                          DateTime A231WWPFormDate ,
                                          short A207WWPFormVersionNumber ,
                                          short AV53OrderedBy ,
                                          bool AV55OrderedDsc ,
                                          short A240WWPFormType ,
                                          short AV79WWPFormType ,
                                          string AV21FilterFullText ,
                                          short A219WWPFormLatestVersionNumber ,
                                          short AV63TFWWPFormLatestVersionNumber ,
                                          short AV64TFWWPFormLatestVersionNumber_To )
   {
      System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
      string scmdbuf;
      short[] GXv_int9 = new short[9];
      Object[] GXv_Object10 = new Object[2];
      scmdbuf = "SELECT WWPFormType, WWPFormVersionNumber, WWPFormDate, WWPFormReferenceName, WWPFormTitle, WWPFormId FROM WWP_Form";
      AddWhere(sWhereString, "(WWPFormType = :AV79WWPFormType)");
      AddWhere(sWhereString, "("+new GxDbmsUtils( new GxPostgreSql()).ValueList(AV24GeneralDynamicFormids, "WWPFormId IN (", ")")+")");
      if ( String.IsNullOrEmpty(StringUtil.RTrim( AV68TFWWPFormTitle_Sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV67TFWWPFormTitle)) ) )
      {
         AddWhere(sWhereString, "(WWPFormTitle like :lV67TFWWPFormTitle)");
      }
      else
      {
         GXv_int9[1] = 1;
      }
      if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV68TFWWPFormTitle_Sel)) && ! ( StringUtil.StrCmp(AV68TFWWPFormTitle_Sel, "<#Empty#>") == 0 ) )
      {
         AddWhere(sWhereString, "(WWPFormTitle = ( :AV68TFWWPFormTitle_Sel))");
      }
      else
      {
         GXv_int9[2] = 1;
      }
      if ( StringUtil.StrCmp(AV68TFWWPFormTitle_Sel, "<#Empty#>") == 0 )
      {
         AddWhere(sWhereString, "((char_length(trim(trailing ' ' from WWPFormTitle))=0))");
      }
      if ( String.IsNullOrEmpty(StringUtil.RTrim( AV66TFWWPFormReferenceName_Sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV65TFWWPFormReferenceName)) ) )
      {
         AddWhere(sWhereString, "(WWPFormReferenceName like :lV65TFWWPFormReferenceName)");
      }
      else
      {
         GXv_int9[3] = 1;
      }
      if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV66TFWWPFormReferenceName_Sel)) && ! ( StringUtil.StrCmp(AV66TFWWPFormReferenceName_Sel, "<#Empty#>") == 0 ) )
      {
         AddWhere(sWhereString, "(WWPFormReferenceName = ( :AV66TFWWPFormReferenceName_Sel))");
      }
      else
      {
         GXv_int9[4] = 1;
      }
      if ( StringUtil.StrCmp(AV66TFWWPFormReferenceName_Sel, "<#Empty#>") == 0 )
      {
         AddWhere(sWhereString, "((char_length(trim(trailing ' ' from WWPFormReferenceName))=0))");
      }
      if ( ! (DateTime.MinValue==AV61TFWWPFormDate) )
      {
         AddWhere(sWhereString, "(WWPFormDate >= :AV61TFWWPFormDate)");
      }
      else
      {
         GXv_int9[5] = 1;
      }
      if ( ! (DateTime.MinValue==AV62TFWWPFormDate_To) )
      {
         AddWhere(sWhereString, "(WWPFormDate <= :AV62TFWWPFormDate_To)");
      }
      else
      {
         GXv_int9[6] = 1;
      }
      if ( ! (0==AV69TFWWPFormVersionNumber) )
      {
         AddWhere(sWhereString, "(WWPFormVersionNumber >= :AV69TFWWPFormVersionNumber)");
      }
      else
      {
         GXv_int9[7] = 1;
      }
      if ( ! (0==AV70TFWWPFormVersionNumber_To) )
      {
         AddWhere(sWhereString, "(WWPFormVersionNumber <= :AV70TFWWPFormVersionNumber_To)");
      }
      else
      {
         GXv_int9[8] = 1;
      }
      scmdbuf += sWhereString;
      if ( ( AV53OrderedBy == 1 ) && ! AV55OrderedDsc )
      {
         scmdbuf += " ORDER BY WWPFormType, WWPFormReferenceName, WWPFormId, WWPFormVersionNumber";
      }
      else if ( ( AV53OrderedBy == 1 ) && ( AV55OrderedDsc ) )
      {
         scmdbuf += " ORDER BY WWPFormType DESC, WWPFormReferenceName DESC, WWPFormId, WWPFormVersionNumber";
      }
      else if ( ( AV53OrderedBy == 2 ) && ! AV55OrderedDsc )
      {
         scmdbuf += " ORDER BY WWPFormType, WWPFormTitle, WWPFormId, WWPFormVersionNumber";
      }
      else if ( ( AV53OrderedBy == 2 ) && ( AV55OrderedDsc ) )
      {
         scmdbuf += " ORDER BY WWPFormType DESC, WWPFormTitle DESC, WWPFormId, WWPFormVersionNumber";
      }
      else if ( ( AV53OrderedBy == 3 ) && ! AV55OrderedDsc )
      {
         scmdbuf += " ORDER BY WWPFormType, WWPFormDate, WWPFormId, WWPFormVersionNumber";
      }
      else if ( ( AV53OrderedBy == 3 ) && ( AV55OrderedDsc ) )
      {
         scmdbuf += " ORDER BY WWPFormType DESC, WWPFormDate DESC, WWPFormId, WWPFormVersionNumber";
      }
      else if ( ( AV53OrderedBy == 4 ) && ! AV55OrderedDsc )
      {
         scmdbuf += " ORDER BY WWPFormType, WWPFormVersionNumber, WWPFormId";
      }
      else if ( ( AV53OrderedBy == 4 ) && ( AV55OrderedDsc ) )
      {
         scmdbuf += " ORDER BY WWPFormType DESC, WWPFormVersionNumber DESC, WWPFormId";
      }
      GXv_Object10[0] = scmdbuf;
      GXv_Object10[1] = GXv_int9;
      return GXv_Object10 ;
   }

   protected Object[] conditional_H00B93( IGxContext context ,
                                          short A206WWPFormId ,
                                          GxSimpleCollection<short> AV24GeneralDynamicFormids ,
                                          string AV68TFWWPFormTitle_Sel ,
                                          string AV67TFWWPFormTitle ,
                                          string AV66TFWWPFormReferenceName_Sel ,
                                          string AV65TFWWPFormReferenceName ,
                                          DateTime AV61TFWWPFormDate ,
                                          DateTime AV62TFWWPFormDate_To ,
                                          short AV69TFWWPFormVersionNumber ,
                                          short AV70TFWWPFormVersionNumber_To ,
                                          string A209WWPFormTitle ,
                                          string A208WWPFormReferenceName ,
                                          DateTime A231WWPFormDate ,
                                          short A207WWPFormVersionNumber ,
                                          short AV53OrderedBy ,
                                          bool AV55OrderedDsc ,
                                          short A240WWPFormType ,
                                          short AV79WWPFormType ,
                                          string AV21FilterFullText ,
                                          short A219WWPFormLatestVersionNumber ,
                                          short AV63TFWWPFormLatestVersionNumber ,
                                          short AV64TFWWPFormLatestVersionNumber_To )
   {
      System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
      string scmdbuf;
      short[] GXv_int11 = new short[9];
      Object[] GXv_Object12 = new Object[2];
      scmdbuf = "SELECT WWPFormType, WWPFormVersionNumber, WWPFormDate, WWPFormReferenceName, WWPFormTitle, WWPFormId FROM WWP_Form";
      AddWhere(sWhereString, "(WWPFormType = :AV79WWPFormType)");
      AddWhere(sWhereString, "("+new GxDbmsUtils( new GxPostgreSql()).ValueList(AV24GeneralDynamicFormids, "WWPFormId IN (", ")")+")");
      if ( String.IsNullOrEmpty(StringUtil.RTrim( AV68TFWWPFormTitle_Sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV67TFWWPFormTitle)) ) )
      {
         AddWhere(sWhereString, "(WWPFormTitle like :lV67TFWWPFormTitle)");
      }
      else
      {
         GXv_int11[1] = 1;
      }
      if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV68TFWWPFormTitle_Sel)) && ! ( StringUtil.StrCmp(AV68TFWWPFormTitle_Sel, "<#Empty#>") == 0 ) )
      {
         AddWhere(sWhereString, "(WWPFormTitle = ( :AV68TFWWPFormTitle_Sel))");
      }
      else
      {
         GXv_int11[2] = 1;
      }
      if ( StringUtil.StrCmp(AV68TFWWPFormTitle_Sel, "<#Empty#>") == 0 )
      {
         AddWhere(sWhereString, "((char_length(trim(trailing ' ' from WWPFormTitle))=0))");
      }
      if ( String.IsNullOrEmpty(StringUtil.RTrim( AV66TFWWPFormReferenceName_Sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV65TFWWPFormReferenceName)) ) )
      {
         AddWhere(sWhereString, "(WWPFormReferenceName like :lV65TFWWPFormReferenceName)");
      }
      else
      {
         GXv_int11[3] = 1;
      }
      if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV66TFWWPFormReferenceName_Sel)) && ! ( StringUtil.StrCmp(AV66TFWWPFormReferenceName_Sel, "<#Empty#>") == 0 ) )
      {
         AddWhere(sWhereString, "(WWPFormReferenceName = ( :AV66TFWWPFormReferenceName_Sel))");
      }
      else
      {
         GXv_int11[4] = 1;
      }
      if ( StringUtil.StrCmp(AV66TFWWPFormReferenceName_Sel, "<#Empty#>") == 0 )
      {
         AddWhere(sWhereString, "((char_length(trim(trailing ' ' from WWPFormReferenceName))=0))");
      }
      if ( ! (DateTime.MinValue==AV61TFWWPFormDate) )
      {
         AddWhere(sWhereString, "(WWPFormDate >= :AV61TFWWPFormDate)");
      }
      else
      {
         GXv_int11[5] = 1;
      }
      if ( ! (DateTime.MinValue==AV62TFWWPFormDate_To) )
      {
         AddWhere(sWhereString, "(WWPFormDate <= :AV62TFWWPFormDate_To)");
      }
      else
      {
         GXv_int11[6] = 1;
      }
      if ( ! (0==AV69TFWWPFormVersionNumber) )
      {
         AddWhere(sWhereString, "(WWPFormVersionNumber >= :AV69TFWWPFormVersionNumber)");
      }
      else
      {
         GXv_int11[7] = 1;
      }
      if ( ! (0==AV70TFWWPFormVersionNumber_To) )
      {
         AddWhere(sWhereString, "(WWPFormVersionNumber <= :AV70TFWWPFormVersionNumber_To)");
      }
      else
      {
         GXv_int11[8] = 1;
      }
      scmdbuf += sWhereString;
      if ( ( AV53OrderedBy == 1 ) && ! AV55OrderedDsc )
      {
         scmdbuf += " ORDER BY WWPFormType, WWPFormReferenceName, WWPFormId, WWPFormVersionNumber";
      }
      else if ( ( AV53OrderedBy == 1 ) && ( AV55OrderedDsc ) )
      {
         scmdbuf += " ORDER BY WWPFormType DESC, WWPFormReferenceName DESC, WWPFormId, WWPFormVersionNumber";
      }
      else if ( ( AV53OrderedBy == 2 ) && ! AV55OrderedDsc )
      {
         scmdbuf += " ORDER BY WWPFormType, WWPFormTitle, WWPFormId, WWPFormVersionNumber";
      }
      else if ( ( AV53OrderedBy == 2 ) && ( AV55OrderedDsc ) )
      {
         scmdbuf += " ORDER BY WWPFormType DESC, WWPFormTitle DESC, WWPFormId, WWPFormVersionNumber";
      }
      else if ( ( AV53OrderedBy == 3 ) && ! AV55OrderedDsc )
      {
         scmdbuf += " ORDER BY WWPFormType, WWPFormDate, WWPFormId, WWPFormVersionNumber";
      }
      else if ( ( AV53OrderedBy == 3 ) && ( AV55OrderedDsc ) )
      {
         scmdbuf += " ORDER BY WWPFormType DESC, WWPFormDate DESC, WWPFormId, WWPFormVersionNumber";
      }
      else if ( ( AV53OrderedBy == 4 ) && ! AV55OrderedDsc )
      {
         scmdbuf += " ORDER BY WWPFormType, WWPFormVersionNumber, WWPFormId";
      }
      else if ( ( AV53OrderedBy == 4 ) && ( AV55OrderedDsc ) )
      {
         scmdbuf += " ORDER BY WWPFormType DESC, WWPFormVersionNumber DESC, WWPFormId";
      }
      GXv_Object12[0] = scmdbuf;
      GXv_Object12[1] = GXv_int11;
      return GXv_Object12 ;
   }

   public override Object [] getDynamicStatement( int cursor ,
                                                  IGxContext context ,
                                                  Object [] dynConstraints )
   {
      switch ( cursor )
      {
            case 0 :
                  return conditional_H00B92(context, (short)dynConstraints[0] , (GxSimpleCollection<short>)dynConstraints[1] , (string)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (string)dynConstraints[5] , (DateTime)dynConstraints[6] , (DateTime)dynConstraints[7] , (short)dynConstraints[8] , (short)dynConstraints[9] , (string)dynConstraints[10] , (string)dynConstraints[11] , (DateTime)dynConstraints[12] , (short)dynConstraints[13] , (short)dynConstraints[14] , (bool)dynConstraints[15] , (short)dynConstraints[16] , (short)dynConstraints[17] , (string)dynConstraints[18] , (short)dynConstraints[19] , (short)dynConstraints[20] , (short)dynConstraints[21] );
            case 1 :
                  return conditional_H00B93(context, (short)dynConstraints[0] , (GxSimpleCollection<short>)dynConstraints[1] , (string)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (string)dynConstraints[5] , (DateTime)dynConstraints[6] , (DateTime)dynConstraints[7] , (short)dynConstraints[8] , (short)dynConstraints[9] , (string)dynConstraints[10] , (string)dynConstraints[11] , (DateTime)dynConstraints[12] , (short)dynConstraints[13] , (short)dynConstraints[14] , (bool)dynConstraints[15] , (short)dynConstraints[16] , (short)dynConstraints[17] , (string)dynConstraints[18] , (short)dynConstraints[19] , (short)dynConstraints[20] , (short)dynConstraints[21] );
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
       Object[] prmH00B94;
       prmH00B94 = new Object[] {
       };
       Object[] prmH00B95;
       prmH00B95 = new Object[] {
       };
       Object[] prmH00B96;
       prmH00B96 = new Object[] {
       new ParDef("AV77WWPFormId",GXType.Int16,4,0)
       };
       Object[] prmH00B92;
       prmH00B92 = new Object[] {
       new ParDef("AV79WWPFormType",GXType.Int16,1,0) ,
       new ParDef("lV67TFWWPFormTitle",GXType.VarChar,100,0) ,
       new ParDef("AV68TFWWPFormTitle_Sel",GXType.VarChar,100,0) ,
       new ParDef("lV65TFWWPFormReferenceName",GXType.VarChar,100,0) ,
       new ParDef("AV66TFWWPFormReferenceName_Sel",GXType.VarChar,100,0) ,
       new ParDef("AV61TFWWPFormDate",GXType.DateTime,8,5) ,
       new ParDef("AV62TFWWPFormDate_To",GXType.DateTime,8,5) ,
       new ParDef("AV69TFWWPFormVersionNumber",GXType.Int16,4,0) ,
       new ParDef("AV70TFWWPFormVersionNumber_To",GXType.Int16,4,0)
       };
       Object[] prmH00B93;
       prmH00B93 = new Object[] {
       new ParDef("AV79WWPFormType",GXType.Int16,1,0) ,
       new ParDef("lV67TFWWPFormTitle",GXType.VarChar,100,0) ,
       new ParDef("AV68TFWWPFormTitle_Sel",GXType.VarChar,100,0) ,
       new ParDef("lV65TFWWPFormReferenceName",GXType.VarChar,100,0) ,
       new ParDef("AV66TFWWPFormReferenceName_Sel",GXType.VarChar,100,0) ,
       new ParDef("AV61TFWWPFormDate",GXType.DateTime,8,5) ,
       new ParDef("AV62TFWWPFormDate_To",GXType.DateTime,8,5) ,
       new ParDef("AV69TFWWPFormVersionNumber",GXType.Int16,4,0) ,
       new ParDef("AV70TFWWPFormVersionNumber_To",GXType.Int16,4,0)
       };
       def= new CursorDef[] {
           new CursorDef("H00B92", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH00B92,11, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("H00B93", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH00B93,11, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("H00B94", "SELECT WWPFormVersionNumber, WWPFormId FROM WWP_Form ORDER BY WWPFormId DESC ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH00B94,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("H00B95", "SELECT WWPFormVersionNumber, WWPFormId FROM WWP_Form ORDER BY WWPFormId DESC ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH00B95,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("H00B96", "SELECT COUNT(*) FROM WWP_FormInstance WHERE WWPFormId = :AV77WWPFormId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH00B96,1, GxCacheFrequency.OFF ,true,false )
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
             ((short[]) buf[0])[0] = rslt.getShort(1);
             ((short[]) buf[1])[0] = rslt.getShort(2);
             ((DateTime[]) buf[2])[0] = rslt.getGXDateTime(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((string[]) buf[4])[0] = rslt.getVarchar(5);
             ((short[]) buf[5])[0] = rslt.getShort(6);
             return;
          case 1 :
             ((short[]) buf[0])[0] = rslt.getShort(1);
             ((short[]) buf[1])[0] = rslt.getShort(2);
             ((DateTime[]) buf[2])[0] = rslt.getGXDateTime(3);
             ((string[]) buf[3])[0] = rslt.getVarchar(4);
             ((string[]) buf[4])[0] = rslt.getVarchar(5);
             ((short[]) buf[5])[0] = rslt.getShort(6);
             return;
          case 2 :
             ((short[]) buf[0])[0] = rslt.getShort(1);
             ((short[]) buf[1])[0] = rslt.getShort(2);
             return;
          case 3 :
             ((short[]) buf[0])[0] = rslt.getShort(1);
             ((short[]) buf[1])[0] = rslt.getShort(2);
             return;
          case 4 :
             ((short[]) buf[0])[0] = rslt.getShort(1);
             return;
    }
 }

}

}
