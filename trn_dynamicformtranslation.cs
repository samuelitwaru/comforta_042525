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
   public class trn_dynamicformtranslation : GXDataArea
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
            if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "trn_dynamicformtranslation.aspx")), "trn_dynamicformtranslation.aspx") == 0 ) )
            {
               SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "trn_dynamicformtranslation.aspx")))) ;
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
                  AV7DynamicFormTranslationId = StringUtil.StrToGuid( GetPar( "DynamicFormTranslationId"));
                  AssignAttri("", false, "AV7DynamicFormTranslationId", AV7DynamicFormTranslationId.ToString());
                  GxWebStd.gx_hidden_field( context, "gxhash_vDYNAMICFORMTRANSLATIONID", GetSecureSignedToken( "", AV7DynamicFormTranslationId, context));
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
         Form.Meta.addItem("description", context.GetMessage( "Trn_Dynamic Form Translation", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         if ( ! context.isAjaxRequest( ) )
         {
            GX_FocusControl = edtDynamicFormTranslationId_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         wbErr = false;
         context.SetDefaultTheme("WorkWithPlusDS", true);
         if ( ! context.IsLocalStorageSupported( ) )
         {
            context.PushCurrentUrl();
         }
      }

      public trn_dynamicformtranslation( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public trn_dynamicformtranslation( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_Gx_mode ,
                           Guid aP1_DynamicFormTranslationId )
      {
         this.Gx_mode = aP0_Gx_mode;
         this.AV7DynamicFormTranslationId = aP1_DynamicFormTranslationId;
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      protected override void createObjects( )
      {
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
            return "trn_dynamicformtranslation_Execute" ;
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
         GxWebStd.gx_group_start( context, grpUnnamedgroup1_Internalname, context.GetMessage( "WWP_TemplateDataPanelTitle", ""), 1, 0, "px", 0, "px", "Group", "", "HLP_Trn_DynamicFormTranslation.htm");
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
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtDynamicFormTranslationId_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtDynamicFormTranslationId_Internalname, context.GetMessage( "Translation Id", ""), "col-sm-4 AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-8 gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 21,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtDynamicFormTranslationId_Internalname, A587DynamicFormTranslationId.ToString(), A587DynamicFormTranslationId.ToString(), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,21);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtDynamicFormTranslationId_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtDynamicFormTranslationId_Enabled, 1, "text", "", 36, "chr", 1, "row", 36, 0, 0, 0, 0, 0, 0, true, "Id", "", false, "", "HLP_Trn_DynamicFormTranslation.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtDynamicFormTranslationWWpFormI_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtDynamicFormTranslationWWpFormI_Internalname, context.GetMessage( "Form Id", ""), "col-sm-4 AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-8 gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 26,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtDynamicFormTranslationWWpFormI_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(A588DynamicFormTranslationWWpFormI), 6, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtDynamicFormTranslationWWpFormI_Enabled!=0) ? context.localUtil.Format( (decimal)(A588DynamicFormTranslationWWpFormI), "ZZZZZ9") : context.localUtil.Format( (decimal)(A588DynamicFormTranslationWWpFormI), "ZZZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,26);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtDynamicFormTranslationWWpFormI_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtDynamicFormTranslationWWpFormI_Enabled, 0, "text", "1", 6, "chr", 1, "row", 6, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Trn_DynamicFormTranslation.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtDynamicFormTranslationWWPFormV_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtDynamicFormTranslationWWPFormV_Internalname, context.GetMessage( "Version Number", ""), "col-sm-4 AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-8 gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 31,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtDynamicFormTranslationWWPFormV_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(A589DynamicFormTranslationWWPFormV), 6, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtDynamicFormTranslationWWPFormV_Enabled!=0) ? context.localUtil.Format( (decimal)(A589DynamicFormTranslationWWPFormV), "ZZZZZ9") : context.localUtil.Format( (decimal)(A589DynamicFormTranslationWWPFormV), "ZZZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,31);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtDynamicFormTranslationWWPFormV_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtDynamicFormTranslationWWPFormV_Enabled, 0, "text", "1", 6, "chr", 1, "row", 6, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Trn_DynamicFormTranslation.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtDynamicFormTranslationWWPFormE_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtDynamicFormTranslationWWPFormE_Internalname, context.GetMessage( "Element Id", ""), "col-sm-4 AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-8 gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 36,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtDynamicFormTranslationWWPFormE_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(A590DynamicFormTranslationWWPFormE), 6, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtDynamicFormTranslationWWPFormE_Enabled!=0) ? context.localUtil.Format( (decimal)(A590DynamicFormTranslationWWPFormE), "ZZZZZ9") : context.localUtil.Format( (decimal)(A590DynamicFormTranslationWWPFormE), "ZZZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,36);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtDynamicFormTranslationWWPFormE_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtDynamicFormTranslationWWPFormE_Enabled, 0, "text", "1", 6, "chr", 1, "row", 6, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Trn_DynamicFormTranslation.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtDynamicFormTranslationTrnName_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtDynamicFormTranslationTrnName_Internalname, context.GetMessage( "Trn Name", ""), "col-sm-4 AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-8 gx-attribute", "start", "top", "", "", "div");
         /* Multiple line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 41,'',false,'',0)\"";
         ClassString = "Attribute";
         StyleString = "";
         ClassString = "Attribute";
         StyleString = "";
         GxWebStd.gx_html_textarea( context, edtDynamicFormTranslationTrnName_Internalname, A591DynamicFormTranslationTrnName, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,41);\"", 0, 1, edtDynamicFormTranslationTrnName_Enabled, 0, 80, "chr", 5, "row", 0, StyleString, ClassString, "", "", "400", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Trn_DynamicFormTranslation.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtDynamicFormTranslationAttribut_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtDynamicFormTranslationAttribut_Internalname, context.GetMessage( "Attribute Name", ""), "col-sm-4 AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-8 gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 46,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtDynamicFormTranslationAttribut_Internalname, A592DynamicFormTranslationAttribut, StringUtil.RTrim( context.localUtil.Format( A592DynamicFormTranslationAttribut, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,46);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtDynamicFormTranslationAttribut_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtDynamicFormTranslationAttribut_Enabled, 0, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_Trn_DynamicFormTranslation.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtDynamicFormTranslationEnglish_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtDynamicFormTranslationEnglish_Internalname, context.GetMessage( "Translation English", ""), "col-sm-4 AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-8 gx-attribute", "start", "top", "", "", "div");
         /* Multiple line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 51,'',false,'',0)\"";
         ClassString = "Attribute";
         StyleString = "";
         ClassString = "Attribute";
         StyleString = "";
         GxWebStd.gx_html_textarea( context, edtDynamicFormTranslationEnglish_Internalname, A593DynamicFormTranslationEnglish, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,51);\"", 0, 1, edtDynamicFormTranslationEnglish_Enabled, 0, 80, "chr", 10, "row", 0, StyleString, ClassString, "", "", "2097152", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Trn_DynamicFormTranslation.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtDynamicFormTranslationDutch_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtDynamicFormTranslationDutch_Internalname, context.GetMessage( "Translation Dutch", ""), "col-sm-4 AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-8 gx-attribute", "start", "top", "", "", "div");
         /* Multiple line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 56,'',false,'',0)\"";
         ClassString = "Attribute";
         StyleString = "";
         ClassString = "Attribute";
         StyleString = "";
         GxWebStd.gx_html_textarea( context, edtDynamicFormTranslationDutch_Internalname, A594DynamicFormTranslationDutch, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,56);\"", 0, 1, edtDynamicFormTranslationDutch_Enabled, 0, 80, "chr", 10, "row", 0, StyleString, ClassString, "", "", "2097152", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Trn_DynamicFormTranslation.htm");
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
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group CellMarginTop10", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 61,'',false,'',0)\"";
         ClassString = "ButtonMaterial";
         StyleString = "";
         GxWebStd.gx_button_ctrl( context, bttBtntrn_enter_Internalname, "", context.GetMessage( "GX_BtnEnter", ""), bttBtntrn_enter_Jsonclick, 5, context.GetMessage( "GX_BtnEnter", ""), "", StyleString, ClassString, bttBtntrn_enter_Visible, bttBtntrn_enter_Enabled, "standard", "'"+""+"'"+",false,"+"'"+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_Trn_DynamicFormTranslation.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 63,'',false,'',0)\"";
         ClassString = "ButtonMaterialDefault";
         StyleString = "";
         GxWebStd.gx_button_ctrl( context, bttBtntrn_cancel_Internalname, "", context.GetMessage( "GX_BtnCancel", ""), bttBtntrn_cancel_Jsonclick, 1, context.GetMessage( "GX_BtnCancel", ""), "", StyleString, ClassString, bttBtntrn_cancel_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"ECANCEL."+"'", TempTags, "", context.GetButtonType( ), "HLP_Trn_DynamicFormTranslation.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 65,'',false,'',0)\"";
         ClassString = "ButtonMaterialDefault";
         StyleString = "";
         GxWebStd.gx_button_ctrl( context, bttBtntrn_delete_Internalname, "", context.GetMessage( "GX_BtnDelete", ""), bttBtntrn_delete_Jsonclick, 5, context.GetMessage( "GX_BtnDelete", ""), "", StyleString, ClassString, bttBtntrn_delete_Visible, bttBtntrn_delete_Enabled, "standard", "'"+""+"'"+",false,"+"'"+"EDELETE."+"'", TempTags, "", context.GetButtonType( ), "HLP_Trn_DynamicFormTranslation.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
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
         E111R2 ();
         context.wbGlbDoneStart = 1;
         assign_properties_default( ) ;
         if ( AnyError == 0 )
         {
            if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
            {
               /* Read saved SDTs. */
               /* Read saved values. */
               Z587DynamicFormTranslationId = StringUtil.StrToGuid( cgiGet( "Z587DynamicFormTranslationId"));
               Z588DynamicFormTranslationWWpFormI = (int)(Math.Round(context.localUtil.CToN( cgiGet( "Z588DynamicFormTranslationWWpFormI"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Z589DynamicFormTranslationWWPFormV = (int)(Math.Round(context.localUtil.CToN( cgiGet( "Z589DynamicFormTranslationWWPFormV"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Z590DynamicFormTranslationWWPFormE = (int)(Math.Round(context.localUtil.CToN( cgiGet( "Z590DynamicFormTranslationWWPFormE"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Z591DynamicFormTranslationTrnName = cgiGet( "Z591DynamicFormTranslationTrnName");
               Z592DynamicFormTranslationAttribut = cgiGet( "Z592DynamicFormTranslationAttribut");
               IsConfirmed = (short)(Math.Round(context.localUtil.CToN( cgiGet( "IsConfirmed"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               IsModified = (short)(Math.Round(context.localUtil.CToN( cgiGet( "IsModified"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Gx_mode = cgiGet( "Mode");
               AV7DynamicFormTranslationId = StringUtil.StrToGuid( cgiGet( "vDYNAMICFORMTRANSLATIONID"));
               Gx_BScreen = (short)(Math.Round(context.localUtil.CToN( cgiGet( "vGXBSCREEN"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               /* Read variables values. */
               if ( StringUtil.StrCmp(cgiGet( edtDynamicFormTranslationId_Internalname), "") == 0 )
               {
                  A587DynamicFormTranslationId = Guid.Empty;
                  AssignAttri("", false, "A587DynamicFormTranslationId", A587DynamicFormTranslationId.ToString());
               }
               else
               {
                  try
                  {
                     A587DynamicFormTranslationId = StringUtil.StrToGuid( cgiGet( edtDynamicFormTranslationId_Internalname));
                     AssignAttri("", false, "A587DynamicFormTranslationId", A587DynamicFormTranslationId.ToString());
                  }
                  catch ( Exception  )
                  {
                     GX_msglist.addItem(context.GetMessage( "GXM_invalidguid", ""), 1, "DYNAMICFORMTRANSLATIONID");
                     AnyError = 1;
                     GX_FocusControl = edtDynamicFormTranslationId_Internalname;
                     AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                     wbErr = true;
                  }
               }
               if ( ( ( context.localUtil.CToN( cgiGet( edtDynamicFormTranslationWWpFormI_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtDynamicFormTranslationWWpFormI_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 999999 )) ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "DYNAMICFORMTRANSLATIONWWPFORMI");
                  AnyError = 1;
                  GX_FocusControl = edtDynamicFormTranslationWWpFormI_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  wbErr = true;
                  A588DynamicFormTranslationWWpFormI = 0;
                  AssignAttri("", false, "A588DynamicFormTranslationWWpFormI", StringUtil.LTrimStr( (decimal)(A588DynamicFormTranslationWWpFormI), 6, 0));
               }
               else
               {
                  A588DynamicFormTranslationWWpFormI = (int)(Math.Round(context.localUtil.CToN( cgiGet( edtDynamicFormTranslationWWpFormI_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, "A588DynamicFormTranslationWWpFormI", StringUtil.LTrimStr( (decimal)(A588DynamicFormTranslationWWpFormI), 6, 0));
               }
               if ( ( ( context.localUtil.CToN( cgiGet( edtDynamicFormTranslationWWPFormV_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtDynamicFormTranslationWWPFormV_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 999999 )) ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "DYNAMICFORMTRANSLATIONWWPFORMV");
                  AnyError = 1;
                  GX_FocusControl = edtDynamicFormTranslationWWPFormV_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  wbErr = true;
                  A589DynamicFormTranslationWWPFormV = 0;
                  AssignAttri("", false, "A589DynamicFormTranslationWWPFormV", StringUtil.LTrimStr( (decimal)(A589DynamicFormTranslationWWPFormV), 6, 0));
               }
               else
               {
                  A589DynamicFormTranslationWWPFormV = (int)(Math.Round(context.localUtil.CToN( cgiGet( edtDynamicFormTranslationWWPFormV_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, "A589DynamicFormTranslationWWPFormV", StringUtil.LTrimStr( (decimal)(A589DynamicFormTranslationWWPFormV), 6, 0));
               }
               if ( ( ( context.localUtil.CToN( cgiGet( edtDynamicFormTranslationWWPFormE_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtDynamicFormTranslationWWPFormE_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 999999 )) ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "DYNAMICFORMTRANSLATIONWWPFORME");
                  AnyError = 1;
                  GX_FocusControl = edtDynamicFormTranslationWWPFormE_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  wbErr = true;
                  A590DynamicFormTranslationWWPFormE = 0;
                  AssignAttri("", false, "A590DynamicFormTranslationWWPFormE", StringUtil.LTrimStr( (decimal)(A590DynamicFormTranslationWWPFormE), 6, 0));
               }
               else
               {
                  A590DynamicFormTranslationWWPFormE = (int)(Math.Round(context.localUtil.CToN( cgiGet( edtDynamicFormTranslationWWPFormE_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, "A590DynamicFormTranslationWWPFormE", StringUtil.LTrimStr( (decimal)(A590DynamicFormTranslationWWPFormE), 6, 0));
               }
               A591DynamicFormTranslationTrnName = cgiGet( edtDynamicFormTranslationTrnName_Internalname);
               AssignAttri("", false, "A591DynamicFormTranslationTrnName", A591DynamicFormTranslationTrnName);
               A592DynamicFormTranslationAttribut = cgiGet( edtDynamicFormTranslationAttribut_Internalname);
               AssignAttri("", false, "A592DynamicFormTranslationAttribut", A592DynamicFormTranslationAttribut);
               A593DynamicFormTranslationEnglish = cgiGet( edtDynamicFormTranslationEnglish_Internalname);
               AssignAttri("", false, "A593DynamicFormTranslationEnglish", A593DynamicFormTranslationEnglish);
               A594DynamicFormTranslationDutch = cgiGet( edtDynamicFormTranslationDutch_Internalname);
               AssignAttri("", false, "A594DynamicFormTranslationDutch", A594DynamicFormTranslationDutch);
               /* Read subfile selected row values. */
               /* Read hidden variables. */
               GXKey = Crypto.GetSiteKey( );
               forbiddenHiddens = new GXProperties();
               forbiddenHiddens.Add("hshsalt", "hsh"+"Trn_DynamicFormTranslation");
               forbiddenHiddens.Add("Gx_mode", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")));
               hsh = cgiGet( "hsh");
               if ( ( ! ( ( A587DynamicFormTranslationId != Z587DynamicFormTranslationId ) ) || ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) ) && ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
               {
                  GXUtil.WriteLogError("trn_dynamicformtranslation:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
                  GxWebError = 1;
                  context.HttpContext.Response.StatusCode = 403;
                  context.WriteHtmlText( "<title>403 Forbidden</title>") ;
                  context.WriteHtmlText( "<h1>403 Forbidden</h1>") ;
                  context.WriteHtmlText( "<p /><hr />") ;
                  GXUtil.WriteLog("send_http_error_code " + 403.ToString());
                  AnyError = 1;
                  return  ;
               }
               standaloneNotModal( ) ;
            }
            else
            {
               standaloneNotModal( ) ;
               if ( StringUtil.StrCmp(gxfirstwebparm, "viewer") == 0 )
               {
                  Gx_mode = "DSP";
                  AssignAttri("", false, "Gx_mode", Gx_mode);
                  A587DynamicFormTranslationId = StringUtil.StrToGuid( GetPar( "DynamicFormTranslationId"));
                  AssignAttri("", false, "A587DynamicFormTranslationId", A587DynamicFormTranslationId.ToString());
                  getEqualNoModal( ) ;
                  if ( ! (Guid.Empty==AV7DynamicFormTranslationId) )
                  {
                     A587DynamicFormTranslationId = AV7DynamicFormTranslationId;
                     AssignAttri("", false, "A587DynamicFormTranslationId", A587DynamicFormTranslationId.ToString());
                  }
                  else
                  {
                     if ( IsIns( )  && (Guid.Empty==A587DynamicFormTranslationId) && ( Gx_BScreen == 0 ) )
                     {
                        A587DynamicFormTranslationId = Guid.NewGuid( );
                        AssignAttri("", false, "A587DynamicFormTranslationId", A587DynamicFormTranslationId.ToString());
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
                     sMode105 = Gx_mode;
                     Gx_mode = "UPD";
                     AssignAttri("", false, "Gx_mode", Gx_mode);
                     if ( ! (Guid.Empty==AV7DynamicFormTranslationId) )
                     {
                        A587DynamicFormTranslationId = AV7DynamicFormTranslationId;
                        AssignAttri("", false, "A587DynamicFormTranslationId", A587DynamicFormTranslationId.ToString());
                     }
                     else
                     {
                        if ( IsIns( )  && (Guid.Empty==A587DynamicFormTranslationId) && ( Gx_BScreen == 0 ) )
                        {
                           A587DynamicFormTranslationId = Guid.NewGuid( );
                           AssignAttri("", false, "A587DynamicFormTranslationId", A587DynamicFormTranslationId.ToString());
                        }
                     }
                     Gx_mode = sMode105;
                     AssignAttri("", false, "Gx_mode", Gx_mode);
                  }
                  standaloneModal( ) ;
                  if ( ! IsIns( ) )
                  {
                     getByPrimaryKey( ) ;
                     if ( RcdFound105 == 1 )
                     {
                        if ( IsDlt( ) )
                        {
                           /* Confirm record */
                           CONFIRM_1R0( ) ;
                           if ( AnyError == 0 )
                           {
                              GX_FocusControl = bttBtntrn_enter_Internalname;
                              AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                           }
                        }
                     }
                     else
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_noinsert", ""), 1, "DYNAMICFORMTRANSLATIONID");
                        AnyError = 1;
                        GX_FocusControl = edtDynamicFormTranslationId_Internalname;
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
                        if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: Start */
                           E111R2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "AFTER TRN") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: After Trn */
                           E121R2 ();
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
            E121R2 ();
            trnEnded = 0;
            standaloneNotModal( ) ;
            standaloneModal( ) ;
            if ( IsIns( )  )
            {
               /* Clear variables for new insertion. */
               InitAll1R105( ) ;
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
            DisableAttributes1R105( ) ;
         }
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

      protected void CONFIRM_1R0( )
      {
         BeforeValidate1R105( ) ;
         if ( AnyError == 0 )
         {
            if ( IsDlt( ) )
            {
               OnDeleteControls1R105( ) ;
            }
            else
            {
               CheckExtendedTable1R105( ) ;
               CloseExtendedTableCursors1R105( ) ;
            }
         }
         if ( AnyError == 0 )
         {
            IsConfirmed = 1;
            AssignAttri("", false, "IsConfirmed", StringUtil.LTrimStr( (decimal)(IsConfirmed), 4, 0));
         }
      }

      protected void ResetCaption1R0( )
      {
      }

      protected void E111R2( )
      {
         /* Start Routine */
         returnInSub = false;
         divLayoutmaintable_Class = divLayoutmaintable_Class+" "+"EditForm";
         AssignProp("", false, divLayoutmaintable_Internalname, "Class", divLayoutmaintable_Class, true);
         new GeneXus.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV8WWPContext) ;
         AV11TrnContext.FromXml(AV12WebSession.Get("TrnContext"), null, "", "");
      }

      protected void E121R2( )
      {
         /* After Trn Routine */
         returnInSub = false;
         if ( ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 ) && ! AV11TrnContext.gxTpr_Callerondelete )
         {
            CallWebObject(formatLink("trn_dynamicformtranslationww.aspx") );
            context.wjLocDisableFrm = 1;
         }
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
      }

      protected void ZM1R105( short GX_JID )
      {
         if ( ( GX_JID == 5 ) || ( GX_JID == 0 ) )
         {
            if ( ! IsIns( ) )
            {
               Z588DynamicFormTranslationWWpFormI = T001R3_A588DynamicFormTranslationWWpFormI[0];
               Z589DynamicFormTranslationWWPFormV = T001R3_A589DynamicFormTranslationWWPFormV[0];
               Z590DynamicFormTranslationWWPFormE = T001R3_A590DynamicFormTranslationWWPFormE[0];
               Z591DynamicFormTranslationTrnName = T001R3_A591DynamicFormTranslationTrnName[0];
               Z592DynamicFormTranslationAttribut = T001R3_A592DynamicFormTranslationAttribut[0];
            }
            else
            {
               Z588DynamicFormTranslationWWpFormI = A588DynamicFormTranslationWWpFormI;
               Z589DynamicFormTranslationWWPFormV = A589DynamicFormTranslationWWPFormV;
               Z590DynamicFormTranslationWWPFormE = A590DynamicFormTranslationWWPFormE;
               Z591DynamicFormTranslationTrnName = A591DynamicFormTranslationTrnName;
               Z592DynamicFormTranslationAttribut = A592DynamicFormTranslationAttribut;
            }
         }
         if ( GX_JID == -5 )
         {
            Z587DynamicFormTranslationId = A587DynamicFormTranslationId;
            Z588DynamicFormTranslationWWpFormI = A588DynamicFormTranslationWWpFormI;
            Z589DynamicFormTranslationWWPFormV = A589DynamicFormTranslationWWPFormV;
            Z590DynamicFormTranslationWWPFormE = A590DynamicFormTranslationWWPFormE;
            Z591DynamicFormTranslationTrnName = A591DynamicFormTranslationTrnName;
            Z592DynamicFormTranslationAttribut = A592DynamicFormTranslationAttribut;
            Z593DynamicFormTranslationEnglish = A593DynamicFormTranslationEnglish;
            Z594DynamicFormTranslationDutch = A594DynamicFormTranslationDutch;
         }
      }

      protected void standaloneNotModal( )
      {
         Gx_BScreen = 0;
         AssignAttri("", false, "Gx_BScreen", StringUtil.Str( (decimal)(Gx_BScreen), 1, 0));
         bttBtntrn_delete_Enabled = 0;
         AssignProp("", false, bttBtntrn_delete_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(bttBtntrn_delete_Enabled), 5, 0), true);
         if ( ! (Guid.Empty==AV7DynamicFormTranslationId) )
         {
            edtDynamicFormTranslationId_Enabled = 0;
            AssignProp("", false, edtDynamicFormTranslationId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtDynamicFormTranslationId_Enabled), 5, 0), true);
         }
         else
         {
            edtDynamicFormTranslationId_Enabled = 1;
            AssignProp("", false, edtDynamicFormTranslationId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtDynamicFormTranslationId_Enabled), 5, 0), true);
         }
         if ( ! (Guid.Empty==AV7DynamicFormTranslationId) )
         {
            edtDynamicFormTranslationId_Enabled = 0;
            AssignProp("", false, edtDynamicFormTranslationId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtDynamicFormTranslationId_Enabled), 5, 0), true);
         }
      }

      protected void standaloneModal( )
      {
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
         if ( ! (Guid.Empty==AV7DynamicFormTranslationId) )
         {
            A587DynamicFormTranslationId = AV7DynamicFormTranslationId;
            AssignAttri("", false, "A587DynamicFormTranslationId", A587DynamicFormTranslationId.ToString());
         }
         else
         {
            if ( IsIns( )  && (Guid.Empty==A587DynamicFormTranslationId) && ( Gx_BScreen == 0 ) )
            {
               A587DynamicFormTranslationId = Guid.NewGuid( );
               AssignAttri("", false, "A587DynamicFormTranslationId", A587DynamicFormTranslationId.ToString());
            }
         }
         if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) && ( Gx_BScreen == 0 ) )
         {
         }
      }

      protected void Load1R105( )
      {
         /* Using cursor T001R4 */
         pr_default.execute(2, new Object[] {A587DynamicFormTranslationId});
         if ( (pr_default.getStatus(2) != 101) )
         {
            RcdFound105 = 1;
            A588DynamicFormTranslationWWpFormI = T001R4_A588DynamicFormTranslationWWpFormI[0];
            AssignAttri("", false, "A588DynamicFormTranslationWWpFormI", StringUtil.LTrimStr( (decimal)(A588DynamicFormTranslationWWpFormI), 6, 0));
            A589DynamicFormTranslationWWPFormV = T001R4_A589DynamicFormTranslationWWPFormV[0];
            AssignAttri("", false, "A589DynamicFormTranslationWWPFormV", StringUtil.LTrimStr( (decimal)(A589DynamicFormTranslationWWPFormV), 6, 0));
            A590DynamicFormTranslationWWPFormE = T001R4_A590DynamicFormTranslationWWPFormE[0];
            AssignAttri("", false, "A590DynamicFormTranslationWWPFormE", StringUtil.LTrimStr( (decimal)(A590DynamicFormTranslationWWPFormE), 6, 0));
            A591DynamicFormTranslationTrnName = T001R4_A591DynamicFormTranslationTrnName[0];
            AssignAttri("", false, "A591DynamicFormTranslationTrnName", A591DynamicFormTranslationTrnName);
            A592DynamicFormTranslationAttribut = T001R4_A592DynamicFormTranslationAttribut[0];
            AssignAttri("", false, "A592DynamicFormTranslationAttribut", A592DynamicFormTranslationAttribut);
            A593DynamicFormTranslationEnglish = T001R4_A593DynamicFormTranslationEnglish[0];
            AssignAttri("", false, "A593DynamicFormTranslationEnglish", A593DynamicFormTranslationEnglish);
            A594DynamicFormTranslationDutch = T001R4_A594DynamicFormTranslationDutch[0];
            AssignAttri("", false, "A594DynamicFormTranslationDutch", A594DynamicFormTranslationDutch);
            ZM1R105( -5) ;
         }
         pr_default.close(2);
         OnLoadActions1R105( ) ;
      }

      protected void OnLoadActions1R105( )
      {
      }

      protected void CheckExtendedTable1R105( )
      {
         Gx_BScreen = 1;
         AssignAttri("", false, "Gx_BScreen", StringUtil.Str( (decimal)(Gx_BScreen), 1, 0));
         standaloneModal( ) ;
      }

      protected void CloseExtendedTableCursors1R105( )
      {
      }

      protected void enableDisable( )
      {
      }

      protected void GetKey1R105( )
      {
         /* Using cursor T001R5 */
         pr_default.execute(3, new Object[] {A587DynamicFormTranslationId});
         if ( (pr_default.getStatus(3) != 101) )
         {
            RcdFound105 = 1;
         }
         else
         {
            RcdFound105 = 0;
         }
         pr_default.close(3);
      }

      protected void getByPrimaryKey( )
      {
         /* Using cursor T001R3 */
         pr_default.execute(1, new Object[] {A587DynamicFormTranslationId});
         if ( (pr_default.getStatus(1) != 101) )
         {
            ZM1R105( 5) ;
            RcdFound105 = 1;
            A587DynamicFormTranslationId = T001R3_A587DynamicFormTranslationId[0];
            AssignAttri("", false, "A587DynamicFormTranslationId", A587DynamicFormTranslationId.ToString());
            A588DynamicFormTranslationWWpFormI = T001R3_A588DynamicFormTranslationWWpFormI[0];
            AssignAttri("", false, "A588DynamicFormTranslationWWpFormI", StringUtil.LTrimStr( (decimal)(A588DynamicFormTranslationWWpFormI), 6, 0));
            A589DynamicFormTranslationWWPFormV = T001R3_A589DynamicFormTranslationWWPFormV[0];
            AssignAttri("", false, "A589DynamicFormTranslationWWPFormV", StringUtil.LTrimStr( (decimal)(A589DynamicFormTranslationWWPFormV), 6, 0));
            A590DynamicFormTranslationWWPFormE = T001R3_A590DynamicFormTranslationWWPFormE[0];
            AssignAttri("", false, "A590DynamicFormTranslationWWPFormE", StringUtil.LTrimStr( (decimal)(A590DynamicFormTranslationWWPFormE), 6, 0));
            A591DynamicFormTranslationTrnName = T001R3_A591DynamicFormTranslationTrnName[0];
            AssignAttri("", false, "A591DynamicFormTranslationTrnName", A591DynamicFormTranslationTrnName);
            A592DynamicFormTranslationAttribut = T001R3_A592DynamicFormTranslationAttribut[0];
            AssignAttri("", false, "A592DynamicFormTranslationAttribut", A592DynamicFormTranslationAttribut);
            A593DynamicFormTranslationEnglish = T001R3_A593DynamicFormTranslationEnglish[0];
            AssignAttri("", false, "A593DynamicFormTranslationEnglish", A593DynamicFormTranslationEnglish);
            A594DynamicFormTranslationDutch = T001R3_A594DynamicFormTranslationDutch[0];
            AssignAttri("", false, "A594DynamicFormTranslationDutch", A594DynamicFormTranslationDutch);
            Z587DynamicFormTranslationId = A587DynamicFormTranslationId;
            sMode105 = Gx_mode;
            Gx_mode = "DSP";
            AssignAttri("", false, "Gx_mode", Gx_mode);
            Load1R105( ) ;
            if ( AnyError == 1 )
            {
               RcdFound105 = 0;
               InitializeNonKey1R105( ) ;
            }
            Gx_mode = sMode105;
            AssignAttri("", false, "Gx_mode", Gx_mode);
         }
         else
         {
            RcdFound105 = 0;
            InitializeNonKey1R105( ) ;
            sMode105 = Gx_mode;
            Gx_mode = "DSP";
            AssignAttri("", false, "Gx_mode", Gx_mode);
            standaloneModal( ) ;
            Gx_mode = sMode105;
            AssignAttri("", false, "Gx_mode", Gx_mode);
         }
         pr_default.close(1);
      }

      protected void getEqualNoModal( )
      {
         GetKey1R105( ) ;
         if ( RcdFound105 == 0 )
         {
         }
         else
         {
         }
         getByPrimaryKey( ) ;
      }

      protected void move_next( )
      {
         RcdFound105 = 0;
         /* Using cursor T001R6 */
         pr_default.execute(4, new Object[] {A587DynamicFormTranslationId});
         if ( (pr_default.getStatus(4) != 101) )
         {
            while ( (pr_default.getStatus(4) != 101) && ( ( GuidUtil.Compare(T001R6_A587DynamicFormTranslationId[0], A587DynamicFormTranslationId, 0) < 0 ) ) )
            {
               pr_default.readNext(4);
            }
            if ( (pr_default.getStatus(4) != 101) && ( ( GuidUtil.Compare(T001R6_A587DynamicFormTranslationId[0], A587DynamicFormTranslationId, 0) > 0 ) ) )
            {
               A587DynamicFormTranslationId = T001R6_A587DynamicFormTranslationId[0];
               AssignAttri("", false, "A587DynamicFormTranslationId", A587DynamicFormTranslationId.ToString());
               RcdFound105 = 1;
            }
         }
         pr_default.close(4);
      }

      protected void move_previous( )
      {
         RcdFound105 = 0;
         /* Using cursor T001R7 */
         pr_default.execute(5, new Object[] {A587DynamicFormTranslationId});
         if ( (pr_default.getStatus(5) != 101) )
         {
            while ( (pr_default.getStatus(5) != 101) && ( ( GuidUtil.Compare(T001R7_A587DynamicFormTranslationId[0], A587DynamicFormTranslationId, 0) > 0 ) ) )
            {
               pr_default.readNext(5);
            }
            if ( (pr_default.getStatus(5) != 101) && ( ( GuidUtil.Compare(T001R7_A587DynamicFormTranslationId[0], A587DynamicFormTranslationId, 0) < 0 ) ) )
            {
               A587DynamicFormTranslationId = T001R7_A587DynamicFormTranslationId[0];
               AssignAttri("", false, "A587DynamicFormTranslationId", A587DynamicFormTranslationId.ToString());
               RcdFound105 = 1;
            }
         }
         pr_default.close(5);
      }

      protected void btn_enter( )
      {
         nKeyPressed = 1;
         GetKey1R105( ) ;
         if ( IsIns( ) )
         {
            /* Insert record */
            GX_FocusControl = edtDynamicFormTranslationId_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            Insert1R105( ) ;
            if ( AnyError == 1 )
            {
               GX_FocusControl = "";
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
         }
         else
         {
            if ( RcdFound105 == 1 )
            {
               if ( A587DynamicFormTranslationId != Z587DynamicFormTranslationId )
               {
                  A587DynamicFormTranslationId = Z587DynamicFormTranslationId;
                  AssignAttri("", false, "A587DynamicFormTranslationId", A587DynamicFormTranslationId.ToString());
                  GX_msglist.addItem(context.GetMessage( "GXM_getbeforeupd", ""), "CandidateKeyNotFound", 1, "DYNAMICFORMTRANSLATIONID");
                  AnyError = 1;
                  GX_FocusControl = edtDynamicFormTranslationId_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               }
               else if ( IsDlt( ) )
               {
                  delete( ) ;
                  AfterTrn( ) ;
                  GX_FocusControl = edtDynamicFormTranslationId_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               }
               else
               {
                  /* Update record */
                  Update1R105( ) ;
                  GX_FocusControl = edtDynamicFormTranslationId_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               }
            }
            else
            {
               if ( A587DynamicFormTranslationId != Z587DynamicFormTranslationId )
               {
                  /* Insert record */
                  GX_FocusControl = edtDynamicFormTranslationId_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  Insert1R105( ) ;
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
                     GX_msglist.addItem(context.GetMessage( "GXM_recdeleted", ""), 1, "DYNAMICFORMTRANSLATIONID");
                     AnyError = 1;
                     GX_FocusControl = edtDynamicFormTranslationId_Internalname;
                     AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  }
                  else
                  {
                     /* Insert record */
                     GX_FocusControl = edtDynamicFormTranslationId_Internalname;
                     AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                     Insert1R105( ) ;
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
         if ( A587DynamicFormTranslationId != Z587DynamicFormTranslationId )
         {
            A587DynamicFormTranslationId = Z587DynamicFormTranslationId;
            AssignAttri("", false, "A587DynamicFormTranslationId", A587DynamicFormTranslationId.ToString());
            GX_msglist.addItem(context.GetMessage( "GXM_getbeforedlt", ""), 1, "DYNAMICFORMTRANSLATIONID");
            AnyError = 1;
            GX_FocusControl = edtDynamicFormTranslationId_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         else
         {
            delete( ) ;
            AfterTrn( ) ;
            GX_FocusControl = edtDynamicFormTranslationId_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         if ( AnyError != 0 )
         {
         }
      }

      protected void CheckOptimisticConcurrency1R105( )
      {
         if ( ! IsIns( ) )
         {
            /* Using cursor T001R2 */
            pr_default.execute(0, new Object[] {A587DynamicFormTranslationId});
            if ( (pr_default.getStatus(0) == 103) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"Trn_DynamicFormTranslation"}), "RecordIsLocked", 1, "");
               AnyError = 1;
               return  ;
            }
            if ( (pr_default.getStatus(0) == 101) || ( Z588DynamicFormTranslationWWpFormI != T001R2_A588DynamicFormTranslationWWpFormI[0] ) || ( Z589DynamicFormTranslationWWPFormV != T001R2_A589DynamicFormTranslationWWPFormV[0] ) || ( Z590DynamicFormTranslationWWPFormE != T001R2_A590DynamicFormTranslationWWPFormE[0] ) || ( StringUtil.StrCmp(Z591DynamicFormTranslationTrnName, T001R2_A591DynamicFormTranslationTrnName[0]) != 0 ) || ( StringUtil.StrCmp(Z592DynamicFormTranslationAttribut, T001R2_A592DynamicFormTranslationAttribut[0]) != 0 ) )
            {
               if ( Z588DynamicFormTranslationWWpFormI != T001R2_A588DynamicFormTranslationWWpFormI[0] )
               {
                  GXUtil.WriteLog("trn_dynamicformtranslation:[seudo value changed for attri]"+"DynamicFormTranslationWWpFormI");
                  GXUtil.WriteLogRaw("Old: ",Z588DynamicFormTranslationWWpFormI);
                  GXUtil.WriteLogRaw("Current: ",T001R2_A588DynamicFormTranslationWWpFormI[0]);
               }
               if ( Z589DynamicFormTranslationWWPFormV != T001R2_A589DynamicFormTranslationWWPFormV[0] )
               {
                  GXUtil.WriteLog("trn_dynamicformtranslation:[seudo value changed for attri]"+"DynamicFormTranslationWWPFormV");
                  GXUtil.WriteLogRaw("Old: ",Z589DynamicFormTranslationWWPFormV);
                  GXUtil.WriteLogRaw("Current: ",T001R2_A589DynamicFormTranslationWWPFormV[0]);
               }
               if ( Z590DynamicFormTranslationWWPFormE != T001R2_A590DynamicFormTranslationWWPFormE[0] )
               {
                  GXUtil.WriteLog("trn_dynamicformtranslation:[seudo value changed for attri]"+"DynamicFormTranslationWWPFormE");
                  GXUtil.WriteLogRaw("Old: ",Z590DynamicFormTranslationWWPFormE);
                  GXUtil.WriteLogRaw("Current: ",T001R2_A590DynamicFormTranslationWWPFormE[0]);
               }
               if ( StringUtil.StrCmp(Z591DynamicFormTranslationTrnName, T001R2_A591DynamicFormTranslationTrnName[0]) != 0 )
               {
                  GXUtil.WriteLog("trn_dynamicformtranslation:[seudo value changed for attri]"+"DynamicFormTranslationTrnName");
                  GXUtil.WriteLogRaw("Old: ",Z591DynamicFormTranslationTrnName);
                  GXUtil.WriteLogRaw("Current: ",T001R2_A591DynamicFormTranslationTrnName[0]);
               }
               if ( StringUtil.StrCmp(Z592DynamicFormTranslationAttribut, T001R2_A592DynamicFormTranslationAttribut[0]) != 0 )
               {
                  GXUtil.WriteLog("trn_dynamicformtranslation:[seudo value changed for attri]"+"DynamicFormTranslationAttribut");
                  GXUtil.WriteLogRaw("Old: ",Z592DynamicFormTranslationAttribut);
                  GXUtil.WriteLogRaw("Current: ",T001R2_A592DynamicFormTranslationAttribut[0]);
               }
               GX_msglist.addItem(context.GetMessage( "GXM_waschg", new   object[]  {"Trn_DynamicFormTranslation"}), "RecordWasChanged", 1, "");
               AnyError = 1;
               return  ;
            }
         }
      }

      protected void Insert1R105( )
      {
         if ( ! IsAuthorized("trn_dynamicformtranslation_Insert") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         BeforeValidate1R105( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable1R105( ) ;
         }
         if ( AnyError == 0 )
         {
            ZM1R105( 0) ;
            CheckOptimisticConcurrency1R105( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm1R105( ) ;
               if ( AnyError == 0 )
               {
                  BeforeInsert1R105( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor T001R8 */
                     pr_default.execute(6, new Object[] {A587DynamicFormTranslationId, A588DynamicFormTranslationWWpFormI, A589DynamicFormTranslationWWPFormV, A590DynamicFormTranslationWWPFormE, A591DynamicFormTranslationTrnName, A592DynamicFormTranslationAttribut, A593DynamicFormTranslationEnglish, A594DynamicFormTranslationDutch});
                     pr_default.close(6);
                     pr_default.SmartCacheProvider.SetUpdated("Trn_DynamicFormTranslation");
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
            else
            {
               Load1R105( ) ;
            }
            EndLevel1R105( ) ;
         }
         CloseExtendedTableCursors1R105( ) ;
      }

      protected void Update1R105( )
      {
         if ( ! IsAuthorized("trn_dynamicformtranslation_Update") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         BeforeValidate1R105( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable1R105( ) ;
         }
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency1R105( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm1R105( ) ;
               if ( AnyError == 0 )
               {
                  BeforeUpdate1R105( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor T001R9 */
                     pr_default.execute(7, new Object[] {A588DynamicFormTranslationWWpFormI, A589DynamicFormTranslationWWPFormV, A590DynamicFormTranslationWWPFormE, A591DynamicFormTranslationTrnName, A592DynamicFormTranslationAttribut, A593DynamicFormTranslationEnglish, A594DynamicFormTranslationDutch, A587DynamicFormTranslationId});
                     pr_default.close(7);
                     pr_default.SmartCacheProvider.SetUpdated("Trn_DynamicFormTranslation");
                     if ( (pr_default.getStatus(7) == 103) )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"Trn_DynamicFormTranslation"}), "RecordIsLocked", 1, "");
                        AnyError = 1;
                     }
                     DeferredUpdate1R105( ) ;
                     if ( AnyError == 0 )
                     {
                        /* Start of After( update) rules */
                        /* End of After( update) rules */
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
            EndLevel1R105( ) ;
         }
         CloseExtendedTableCursors1R105( ) ;
      }

      protected void DeferredUpdate1R105( )
      {
      }

      protected void delete( )
      {
         if ( ! IsAuthorized("trn_dynamicformtranslation_Delete") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         BeforeValidate1R105( ) ;
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency1R105( ) ;
         }
         if ( AnyError == 0 )
         {
            OnDeleteControls1R105( ) ;
            AfterConfirm1R105( ) ;
            if ( AnyError == 0 )
            {
               BeforeDelete1R105( ) ;
               if ( AnyError == 0 )
               {
                  /* No cascading delete specified. */
                  /* Using cursor T001R10 */
                  pr_default.execute(8, new Object[] {A587DynamicFormTranslationId});
                  pr_default.close(8);
                  pr_default.SmartCacheProvider.SetUpdated("Trn_DynamicFormTranslation");
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
         sMode105 = Gx_mode;
         Gx_mode = "DLT";
         AssignAttri("", false, "Gx_mode", Gx_mode);
         EndLevel1R105( ) ;
         Gx_mode = sMode105;
         AssignAttri("", false, "Gx_mode", Gx_mode);
      }

      protected void OnDeleteControls1R105( )
      {
         standaloneModal( ) ;
         /* No delete mode formulas found. */
      }

      protected void EndLevel1R105( )
      {
         if ( ! IsIns( ) )
         {
            pr_default.close(0);
         }
         if ( AnyError == 0 )
         {
            BeforeComplete1R105( ) ;
         }
         if ( AnyError == 0 )
         {
            context.CommitDataStores("trn_dynamicformtranslation",pr_default);
            if ( AnyError == 0 )
            {
               ConfirmValues1R0( ) ;
            }
            /* After transaction rules */
            /* Execute 'After Trn' event if defined. */
            trnEnded = 1;
         }
         else
         {
            context.RollbackDataStores("trn_dynamicformtranslation",pr_default);
         }
         IsModified = 0;
         if ( AnyError != 0 )
         {
            context.wjLoc = "";
            context.nUserReturn = 0;
         }
      }

      public void ScanStart1R105( )
      {
         /* Scan By routine */
         /* Using cursor T001R11 */
         pr_default.execute(9);
         RcdFound105 = 0;
         if ( (pr_default.getStatus(9) != 101) )
         {
            RcdFound105 = 1;
            A587DynamicFormTranslationId = T001R11_A587DynamicFormTranslationId[0];
            AssignAttri("", false, "A587DynamicFormTranslationId", A587DynamicFormTranslationId.ToString());
         }
         /* Load Subordinate Levels */
      }

      protected void ScanNext1R105( )
      {
         /* Scan next routine */
         pr_default.readNext(9);
         RcdFound105 = 0;
         if ( (pr_default.getStatus(9) != 101) )
         {
            RcdFound105 = 1;
            A587DynamicFormTranslationId = T001R11_A587DynamicFormTranslationId[0];
            AssignAttri("", false, "A587DynamicFormTranslationId", A587DynamicFormTranslationId.ToString());
         }
      }

      protected void ScanEnd1R105( )
      {
         pr_default.close(9);
      }

      protected void AfterConfirm1R105( )
      {
         /* After Confirm Rules */
      }

      protected void BeforeInsert1R105( )
      {
         /* Before Insert Rules */
      }

      protected void BeforeUpdate1R105( )
      {
         /* Before Update Rules */
      }

      protected void BeforeDelete1R105( )
      {
         /* Before Delete Rules */
      }

      protected void BeforeComplete1R105( )
      {
         /* Before Complete Rules */
      }

      protected void BeforeValidate1R105( )
      {
         /* Before Validate Rules */
      }

      protected void DisableAttributes1R105( )
      {
         edtDynamicFormTranslationId_Enabled = 0;
         AssignProp("", false, edtDynamicFormTranslationId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtDynamicFormTranslationId_Enabled), 5, 0), true);
         edtDynamicFormTranslationWWpFormI_Enabled = 0;
         AssignProp("", false, edtDynamicFormTranslationWWpFormI_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtDynamicFormTranslationWWpFormI_Enabled), 5, 0), true);
         edtDynamicFormTranslationWWPFormV_Enabled = 0;
         AssignProp("", false, edtDynamicFormTranslationWWPFormV_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtDynamicFormTranslationWWPFormV_Enabled), 5, 0), true);
         edtDynamicFormTranslationWWPFormE_Enabled = 0;
         AssignProp("", false, edtDynamicFormTranslationWWPFormE_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtDynamicFormTranslationWWPFormE_Enabled), 5, 0), true);
         edtDynamicFormTranslationTrnName_Enabled = 0;
         AssignProp("", false, edtDynamicFormTranslationTrnName_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtDynamicFormTranslationTrnName_Enabled), 5, 0), true);
         edtDynamicFormTranslationAttribut_Enabled = 0;
         AssignProp("", false, edtDynamicFormTranslationAttribut_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtDynamicFormTranslationAttribut_Enabled), 5, 0), true);
         edtDynamicFormTranslationEnglish_Enabled = 0;
         AssignProp("", false, edtDynamicFormTranslationEnglish_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtDynamicFormTranslationEnglish_Enabled), 5, 0), true);
         edtDynamicFormTranslationDutch_Enabled = 0;
         AssignProp("", false, edtDynamicFormTranslationDutch_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtDynamicFormTranslationDutch_Enabled), 5, 0), true);
      }

      protected void send_integrity_lvl_hashes1R105( )
      {
      }

      protected void assign_properties_default( )
      {
      }

      protected void ConfirmValues1R0( )
      {
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
         GXEncryptionTmp = "trn_dynamicformtranslation.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(AV7DynamicFormTranslationId.ToString());
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("trn_dynamicformtranslation.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
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
         forbiddenHiddens.Add("hshsalt", "hsh"+"Trn_DynamicFormTranslation");
         forbiddenHiddens.Add("Gx_mode", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")));
         GxWebStd.gx_hidden_field( context, "hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("trn_dynamicformtranslation:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "Z587DynamicFormTranslationId", Z587DynamicFormTranslationId.ToString());
         GxWebStd.gx_hidden_field( context, "Z588DynamicFormTranslationWWpFormI", StringUtil.LTrim( StringUtil.NToC( (decimal)(Z588DynamicFormTranslationWWpFormI), 6, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Z589DynamicFormTranslationWWPFormV", StringUtil.LTrim( StringUtil.NToC( (decimal)(Z589DynamicFormTranslationWWPFormV), 6, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Z590DynamicFormTranslationWWPFormE", StringUtil.LTrim( StringUtil.NToC( (decimal)(Z590DynamicFormTranslationWWPFormE), 6, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Z591DynamicFormTranslationTrnName", Z591DynamicFormTranslationTrnName);
         GxWebStd.gx_hidden_field( context, "Z592DynamicFormTranslationAttribut", Z592DynamicFormTranslationAttribut);
         GxWebStd.gx_hidden_field( context, "IsConfirmed", StringUtil.LTrim( StringUtil.NToC( (decimal)(IsConfirmed), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "IsModified", StringUtil.LTrim( StringUtil.NToC( (decimal)(IsModified), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Mode", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_Mode", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         GxWebStd.gx_hidden_field( context, "vMODE", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_vMODE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vTRNCONTEXT", AV11TrnContext);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vTRNCONTEXT", AV11TrnContext);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vTRNCONTEXT", GetSecureSignedToken( "", AV11TrnContext, context));
         GxWebStd.gx_hidden_field( context, "vDYNAMICFORMTRANSLATIONID", AV7DynamicFormTranslationId.ToString());
         GxWebStd.gx_hidden_field( context, "gxhash_vDYNAMICFORMTRANSLATIONID", GetSecureSignedToken( "", AV7DynamicFormTranslationId, context));
         GxWebStd.gx_hidden_field( context, "vGXBSCREEN", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gx_BScreen), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
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
         GXEncryptionTmp = "trn_dynamicformtranslation.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(AV7DynamicFormTranslationId.ToString());
         return formatLink("trn_dynamicformtranslation.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey) ;
      }

      public override string GetPgmname( )
      {
         return "Trn_DynamicFormTranslation" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Trn_Dynamic Form Translation", "") ;
      }

      protected void InitializeNonKey1R105( )
      {
         A588DynamicFormTranslationWWpFormI = 0;
         AssignAttri("", false, "A588DynamicFormTranslationWWpFormI", StringUtil.LTrimStr( (decimal)(A588DynamicFormTranslationWWpFormI), 6, 0));
         A589DynamicFormTranslationWWPFormV = 0;
         AssignAttri("", false, "A589DynamicFormTranslationWWPFormV", StringUtil.LTrimStr( (decimal)(A589DynamicFormTranslationWWPFormV), 6, 0));
         A590DynamicFormTranslationWWPFormE = 0;
         AssignAttri("", false, "A590DynamicFormTranslationWWPFormE", StringUtil.LTrimStr( (decimal)(A590DynamicFormTranslationWWPFormE), 6, 0));
         A591DynamicFormTranslationTrnName = "";
         AssignAttri("", false, "A591DynamicFormTranslationTrnName", A591DynamicFormTranslationTrnName);
         A592DynamicFormTranslationAttribut = "";
         AssignAttri("", false, "A592DynamicFormTranslationAttribut", A592DynamicFormTranslationAttribut);
         A593DynamicFormTranslationEnglish = "";
         AssignAttri("", false, "A593DynamicFormTranslationEnglish", A593DynamicFormTranslationEnglish);
         A594DynamicFormTranslationDutch = "";
         AssignAttri("", false, "A594DynamicFormTranslationDutch", A594DynamicFormTranslationDutch);
         Z588DynamicFormTranslationWWpFormI = 0;
         Z589DynamicFormTranslationWWPFormV = 0;
         Z590DynamicFormTranslationWWPFormE = 0;
         Z591DynamicFormTranslationTrnName = "";
         Z592DynamicFormTranslationAttribut = "";
      }

      protected void InitAll1R105( )
      {
         A587DynamicFormTranslationId = Guid.NewGuid( );
         AssignAttri("", false, "A587DynamicFormTranslationId", A587DynamicFormTranslationId.ToString());
         InitializeNonKey1R105( ) ;
      }

      protected void StandaloneModalInsert( )
      {
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2025428132588", true, true);
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
         context.AddJavascriptSource("trn_dynamicformtranslation.js", "?2025428132589", false, true);
         /* End function include_jscripts */
      }

      protected void init_default_properties( )
      {
         edtDynamicFormTranslationId_Internalname = "DYNAMICFORMTRANSLATIONID";
         edtDynamicFormTranslationWWpFormI_Internalname = "DYNAMICFORMTRANSLATIONWWPFORMI";
         edtDynamicFormTranslationWWPFormV_Internalname = "DYNAMICFORMTRANSLATIONWWPFORMV";
         edtDynamicFormTranslationWWPFormE_Internalname = "DYNAMICFORMTRANSLATIONWWPFORME";
         edtDynamicFormTranslationTrnName_Internalname = "DYNAMICFORMTRANSLATIONTRNNAME";
         edtDynamicFormTranslationAttribut_Internalname = "DYNAMICFORMTRANSLATIONATTRIBUT";
         edtDynamicFormTranslationEnglish_Internalname = "DYNAMICFORMTRANSLATIONENGLISH";
         edtDynamicFormTranslationDutch_Internalname = "DYNAMICFORMTRANSLATIONDUTCH";
         divTableattributes_Internalname = "TABLEATTRIBUTES";
         divTablecontent_Internalname = "TABLECONTENT";
         grpUnnamedgroup1_Internalname = "UNNAMEDGROUP1";
         bttBtntrn_enter_Internalname = "BTNTRN_ENTER";
         bttBtntrn_cancel_Internalname = "BTNTRN_CANCEL";
         bttBtntrn_delete_Internalname = "BTNTRN_DELETE";
         divTablemain_Internalname = "TABLEMAIN";
         divLayoutmaintable_Internalname = "LAYOUTMAINTABLE";
         Form.Internalname = "FORM";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("WorkWithPlusDS", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "Trn_Dynamic Form Translation", "");
         bttBtntrn_delete_Enabled = 0;
         bttBtntrn_delete_Visible = 1;
         bttBtntrn_cancel_Visible = 1;
         bttBtntrn_enter_Enabled = 1;
         bttBtntrn_enter_Visible = 1;
         edtDynamicFormTranslationDutch_Enabled = 1;
         edtDynamicFormTranslationEnglish_Enabled = 1;
         edtDynamicFormTranslationAttribut_Jsonclick = "";
         edtDynamicFormTranslationAttribut_Enabled = 1;
         edtDynamicFormTranslationTrnName_Enabled = 1;
         edtDynamicFormTranslationWWPFormE_Jsonclick = "";
         edtDynamicFormTranslationWWPFormE_Enabled = 1;
         edtDynamicFormTranslationWWPFormV_Jsonclick = "";
         edtDynamicFormTranslationWWPFormV_Enabled = 1;
         edtDynamicFormTranslationWWpFormI_Jsonclick = "";
         edtDynamicFormTranslationWWpFormI_Enabled = 1;
         edtDynamicFormTranslationId_Jsonclick = "";
         edtDynamicFormTranslationId_Enabled = 1;
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

      protected void init_web_controls( )
      {
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

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("ENTER","""{"handler":"UserMainFullajax","iparms":[{"postForm":true},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV7DynamicFormTranslationId","fld":"vDYNAMICFORMTRANSLATIONID","hsh":true}]}""");
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV11TrnContext","fld":"vTRNCONTEXT","hsh":true},{"av":"AV7DynamicFormTranslationId","fld":"vDYNAMICFORMTRANSLATIONID","hsh":true}]}""");
         setEventMetadata("AFTER TRN","""{"handler":"E121R2","iparms":[{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV11TrnContext","fld":"vTRNCONTEXT","hsh":true}]}""");
         setEventMetadata("VALID_DYNAMICFORMTRANSLATIONID","""{"handler":"Valid_Dynamicformtranslationid","iparms":[]}""");
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
      }

      public override void initialize( )
      {
         sPrefix = "";
         wcpOGx_mode = "";
         wcpOAV7DynamicFormTranslationId = Guid.Empty;
         Z587DynamicFormTranslationId = Guid.Empty;
         Z591DynamicFormTranslationTrnName = "";
         Z592DynamicFormTranslationAttribut = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         GXKey = "";
         GXDecQS = "";
         PreviousTooltip = "";
         PreviousCaption = "";
         Form = new GXWebForm();
         GX_FocusControl = "";
         ClassString = "";
         StyleString = "";
         TempTags = "";
         A587DynamicFormTranslationId = Guid.Empty;
         A591DynamicFormTranslationTrnName = "";
         A592DynamicFormTranslationAttribut = "";
         A593DynamicFormTranslationEnglish = "";
         A594DynamicFormTranslationDutch = "";
         bttBtntrn_enter_Jsonclick = "";
         bttBtntrn_cancel_Jsonclick = "";
         bttBtntrn_delete_Jsonclick = "";
         forbiddenHiddens = new GXProperties();
         hsh = "";
         sMode105 = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         endTrnMsgTxt = "";
         endTrnMsgCod = "";
         AV8WWPContext = new GeneXus.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV11TrnContext = new WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext(context);
         AV12WebSession = context.GetSession();
         Z593DynamicFormTranslationEnglish = "";
         Z594DynamicFormTranslationDutch = "";
         T001R4_A587DynamicFormTranslationId = new Guid[] {Guid.Empty} ;
         T001R4_A588DynamicFormTranslationWWpFormI = new int[1] ;
         T001R4_A589DynamicFormTranslationWWPFormV = new int[1] ;
         T001R4_A590DynamicFormTranslationWWPFormE = new int[1] ;
         T001R4_A591DynamicFormTranslationTrnName = new string[] {""} ;
         T001R4_A592DynamicFormTranslationAttribut = new string[] {""} ;
         T001R4_A593DynamicFormTranslationEnglish = new string[] {""} ;
         T001R4_A594DynamicFormTranslationDutch = new string[] {""} ;
         T001R5_A587DynamicFormTranslationId = new Guid[] {Guid.Empty} ;
         T001R3_A587DynamicFormTranslationId = new Guid[] {Guid.Empty} ;
         T001R3_A588DynamicFormTranslationWWpFormI = new int[1] ;
         T001R3_A589DynamicFormTranslationWWPFormV = new int[1] ;
         T001R3_A590DynamicFormTranslationWWPFormE = new int[1] ;
         T001R3_A591DynamicFormTranslationTrnName = new string[] {""} ;
         T001R3_A592DynamicFormTranslationAttribut = new string[] {""} ;
         T001R3_A593DynamicFormTranslationEnglish = new string[] {""} ;
         T001R3_A594DynamicFormTranslationDutch = new string[] {""} ;
         T001R6_A587DynamicFormTranslationId = new Guid[] {Guid.Empty} ;
         T001R7_A587DynamicFormTranslationId = new Guid[] {Guid.Empty} ;
         T001R2_A587DynamicFormTranslationId = new Guid[] {Guid.Empty} ;
         T001R2_A588DynamicFormTranslationWWpFormI = new int[1] ;
         T001R2_A589DynamicFormTranslationWWPFormV = new int[1] ;
         T001R2_A590DynamicFormTranslationWWPFormE = new int[1] ;
         T001R2_A591DynamicFormTranslationTrnName = new string[] {""} ;
         T001R2_A592DynamicFormTranslationAttribut = new string[] {""} ;
         T001R2_A593DynamicFormTranslationEnglish = new string[] {""} ;
         T001R2_A594DynamicFormTranslationDutch = new string[] {""} ;
         T001R11_A587DynamicFormTranslationId = new Guid[] {Guid.Empty} ;
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXEncryptionTmp = "";
         pr_datastore1 = new DataStoreProvider(context, new GeneXus.Programs.trn_dynamicformtranslation__datastore1(),
            new Object[][] {
            }
         );
         pr_gam = new DataStoreProvider(context, new GeneXus.Programs.trn_dynamicformtranslation__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.trn_dynamicformtranslation__default(),
            new Object[][] {
                new Object[] {
               T001R2_A587DynamicFormTranslationId, T001R2_A588DynamicFormTranslationWWpFormI, T001R2_A589DynamicFormTranslationWWPFormV, T001R2_A590DynamicFormTranslationWWPFormE, T001R2_A591DynamicFormTranslationTrnName, T001R2_A592DynamicFormTranslationAttribut, T001R2_A593DynamicFormTranslationEnglish, T001R2_A594DynamicFormTranslationDutch
               }
               , new Object[] {
               T001R3_A587DynamicFormTranslationId, T001R3_A588DynamicFormTranslationWWpFormI, T001R3_A589DynamicFormTranslationWWPFormV, T001R3_A590DynamicFormTranslationWWPFormE, T001R3_A591DynamicFormTranslationTrnName, T001R3_A592DynamicFormTranslationAttribut, T001R3_A593DynamicFormTranslationEnglish, T001R3_A594DynamicFormTranslationDutch
               }
               , new Object[] {
               T001R4_A587DynamicFormTranslationId, T001R4_A588DynamicFormTranslationWWpFormI, T001R4_A589DynamicFormTranslationWWPFormV, T001R4_A590DynamicFormTranslationWWPFormE, T001R4_A591DynamicFormTranslationTrnName, T001R4_A592DynamicFormTranslationAttribut, T001R4_A593DynamicFormTranslationEnglish, T001R4_A594DynamicFormTranslationDutch
               }
               , new Object[] {
               T001R5_A587DynamicFormTranslationId
               }
               , new Object[] {
               T001R6_A587DynamicFormTranslationId
               }
               , new Object[] {
               T001R7_A587DynamicFormTranslationId
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               T001R11_A587DynamicFormTranslationId
               }
            }
         );
         Z587DynamicFormTranslationId = Guid.NewGuid( );
         A587DynamicFormTranslationId = Guid.NewGuid( );
      }

      private short GxWebError ;
      private short AnyError ;
      private short IsModified ;
      private short IsConfirmed ;
      private short nKeyPressed ;
      private short Gx_BScreen ;
      private short RcdFound105 ;
      private short gxajaxcallmode ;
      private int Z588DynamicFormTranslationWWpFormI ;
      private int Z589DynamicFormTranslationWWPFormV ;
      private int Z590DynamicFormTranslationWWPFormE ;
      private int trnEnded ;
      private int edtDynamicFormTranslationId_Enabled ;
      private int A588DynamicFormTranslationWWpFormI ;
      private int edtDynamicFormTranslationWWpFormI_Enabled ;
      private int A589DynamicFormTranslationWWPFormV ;
      private int edtDynamicFormTranslationWWPFormV_Enabled ;
      private int A590DynamicFormTranslationWWPFormE ;
      private int edtDynamicFormTranslationWWPFormE_Enabled ;
      private int edtDynamicFormTranslationTrnName_Enabled ;
      private int edtDynamicFormTranslationAttribut_Enabled ;
      private int edtDynamicFormTranslationEnglish_Enabled ;
      private int edtDynamicFormTranslationDutch_Enabled ;
      private int bttBtntrn_enter_Visible ;
      private int bttBtntrn_enter_Enabled ;
      private int bttBtntrn_cancel_Visible ;
      private int bttBtntrn_delete_Visible ;
      private int bttBtntrn_delete_Enabled ;
      private int idxLst ;
      private string sPrefix ;
      private string wcpOGx_mode ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string GXKey ;
      private string GXDecQS ;
      private string Gx_mode ;
      private string PreviousTooltip ;
      private string PreviousCaption ;
      private string GX_FocusControl ;
      private string edtDynamicFormTranslationId_Internalname ;
      private string divLayoutmaintable_Internalname ;
      private string divLayoutmaintable_Class ;
      private string divTablemain_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string grpUnnamedgroup1_Internalname ;
      private string divTablecontent_Internalname ;
      private string divTableattributes_Internalname ;
      private string TempTags ;
      private string edtDynamicFormTranslationId_Jsonclick ;
      private string edtDynamicFormTranslationWWpFormI_Internalname ;
      private string edtDynamicFormTranslationWWpFormI_Jsonclick ;
      private string edtDynamicFormTranslationWWPFormV_Internalname ;
      private string edtDynamicFormTranslationWWPFormV_Jsonclick ;
      private string edtDynamicFormTranslationWWPFormE_Internalname ;
      private string edtDynamicFormTranslationWWPFormE_Jsonclick ;
      private string edtDynamicFormTranslationTrnName_Internalname ;
      private string edtDynamicFormTranslationAttribut_Internalname ;
      private string edtDynamicFormTranslationAttribut_Jsonclick ;
      private string edtDynamicFormTranslationEnglish_Internalname ;
      private string edtDynamicFormTranslationDutch_Internalname ;
      private string bttBtntrn_enter_Internalname ;
      private string bttBtntrn_enter_Jsonclick ;
      private string bttBtntrn_cancel_Internalname ;
      private string bttBtntrn_cancel_Jsonclick ;
      private string bttBtntrn_delete_Internalname ;
      private string bttBtntrn_delete_Jsonclick ;
      private string hsh ;
      private string sMode105 ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string endTrnMsgTxt ;
      private string endTrnMsgCod ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXEncryptionTmp ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbErr ;
      private bool returnInSub ;
      private string A593DynamicFormTranslationEnglish ;
      private string A594DynamicFormTranslationDutch ;
      private string Z593DynamicFormTranslationEnglish ;
      private string Z594DynamicFormTranslationDutch ;
      private string Z591DynamicFormTranslationTrnName ;
      private string Z592DynamicFormTranslationAttribut ;
      private string A591DynamicFormTranslationTrnName ;
      private string A592DynamicFormTranslationAttribut ;
      private Guid wcpOAV7DynamicFormTranslationId ;
      private Guid Z587DynamicFormTranslationId ;
      private Guid AV7DynamicFormTranslationId ;
      private Guid A587DynamicFormTranslationId ;
      private IGxSession AV12WebSession ;
      private GXProperties forbiddenHiddens ;
      private GXWebForm Form ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GeneXus.Programs.wwpbaseobjects.SdtWWPContext AV8WWPContext ;
      private WorkWithPlus.workwithplus_commonobjects.SdtWWPTransactionContext AV11TrnContext ;
      private IDataStoreProvider pr_default ;
      private Guid[] T001R4_A587DynamicFormTranslationId ;
      private int[] T001R4_A588DynamicFormTranslationWWpFormI ;
      private int[] T001R4_A589DynamicFormTranslationWWPFormV ;
      private int[] T001R4_A590DynamicFormTranslationWWPFormE ;
      private string[] T001R4_A591DynamicFormTranslationTrnName ;
      private string[] T001R4_A592DynamicFormTranslationAttribut ;
      private string[] T001R4_A593DynamicFormTranslationEnglish ;
      private string[] T001R4_A594DynamicFormTranslationDutch ;
      private Guid[] T001R5_A587DynamicFormTranslationId ;
      private Guid[] T001R3_A587DynamicFormTranslationId ;
      private int[] T001R3_A588DynamicFormTranslationWWpFormI ;
      private int[] T001R3_A589DynamicFormTranslationWWPFormV ;
      private int[] T001R3_A590DynamicFormTranslationWWPFormE ;
      private string[] T001R3_A591DynamicFormTranslationTrnName ;
      private string[] T001R3_A592DynamicFormTranslationAttribut ;
      private string[] T001R3_A593DynamicFormTranslationEnglish ;
      private string[] T001R3_A594DynamicFormTranslationDutch ;
      private Guid[] T001R6_A587DynamicFormTranslationId ;
      private Guid[] T001R7_A587DynamicFormTranslationId ;
      private Guid[] T001R2_A587DynamicFormTranslationId ;
      private int[] T001R2_A588DynamicFormTranslationWWpFormI ;
      private int[] T001R2_A589DynamicFormTranslationWWPFormV ;
      private int[] T001R2_A590DynamicFormTranslationWWPFormE ;
      private string[] T001R2_A591DynamicFormTranslationTrnName ;
      private string[] T001R2_A592DynamicFormTranslationAttribut ;
      private string[] T001R2_A593DynamicFormTranslationEnglish ;
      private string[] T001R2_A594DynamicFormTranslationDutch ;
      private Guid[] T001R11_A587DynamicFormTranslationId ;
      private IDataStoreProvider pr_datastore1 ;
      private IDataStoreProvider pr_gam ;
   }

   public class trn_dynamicformtranslation__datastore1 : DataStoreHelperBase, IDataStoreHelper
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

 public class trn_dynamicformtranslation__gam : DataStoreHelperBase, IDataStoreHelper
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

public class trn_dynamicformtranslation__default : DataStoreHelperBase, IDataStoreHelper
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
    };
 }

 private static CursorDef[] def;
 private void cursorDefinitions( )
 {
    if ( def == null )
    {
       Object[] prmT001R2;
       prmT001R2 = new Object[] {
       new ParDef("DynamicFormTranslationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001R3;
       prmT001R3 = new Object[] {
       new ParDef("DynamicFormTranslationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001R4;
       prmT001R4 = new Object[] {
       new ParDef("DynamicFormTranslationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001R5;
       prmT001R5 = new Object[] {
       new ParDef("DynamicFormTranslationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001R6;
       prmT001R6 = new Object[] {
       new ParDef("DynamicFormTranslationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001R7;
       prmT001R7 = new Object[] {
       new ParDef("DynamicFormTranslationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001R8;
       prmT001R8 = new Object[] {
       new ParDef("DynamicFormTranslationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("DynamicFormTranslationWWpFormI",GXType.Int32,6,0) ,
       new ParDef("DynamicFormTranslationWWPFormV",GXType.Int32,6,0) ,
       new ParDef("DynamicFormTranslationWWPFormE",GXType.Int32,6,0) ,
       new ParDef("DynamicFormTranslationTrnName",GXType.VarChar,400,0) ,
       new ParDef("DynamicFormTranslationAttribut",GXType.VarChar,40,0) ,
       new ParDef("DynamicFormTranslationEnglish",GXType.LongVarChar,2097152,0) ,
       new ParDef("DynamicFormTranslationDutch",GXType.LongVarChar,2097152,0)
       };
       Object[] prmT001R9;
       prmT001R9 = new Object[] {
       new ParDef("DynamicFormTranslationWWpFormI",GXType.Int32,6,0) ,
       new ParDef("DynamicFormTranslationWWPFormV",GXType.Int32,6,0) ,
       new ParDef("DynamicFormTranslationWWPFormE",GXType.Int32,6,0) ,
       new ParDef("DynamicFormTranslationTrnName",GXType.VarChar,400,0) ,
       new ParDef("DynamicFormTranslationAttribut",GXType.VarChar,40,0) ,
       new ParDef("DynamicFormTranslationEnglish",GXType.LongVarChar,2097152,0) ,
       new ParDef("DynamicFormTranslationDutch",GXType.LongVarChar,2097152,0) ,
       new ParDef("DynamicFormTranslationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001R10;
       prmT001R10 = new Object[] {
       new ParDef("DynamicFormTranslationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmT001R11;
       prmT001R11 = new Object[] {
       };
       def= new CursorDef[] {
           new CursorDef("T001R2", "SELECT DynamicFormTranslationId, DynamicFormTranslationWWpFormI, DynamicFormTranslationWWPFormV, DynamicFormTranslationWWPFormE, DynamicFormTranslationTrnName, DynamicFormTranslationAttribut, DynamicFormTranslationEnglish, DynamicFormTranslationDutch FROM Trn_DynamicFormTranslation WHERE DynamicFormTranslationId = :DynamicFormTranslationId  FOR UPDATE OF Trn_DynamicFormTranslation NOWAIT",true, GxErrorMask.GX_NOMASK, false, this,prmT001R2,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001R3", "SELECT DynamicFormTranslationId, DynamicFormTranslationWWpFormI, DynamicFormTranslationWWPFormV, DynamicFormTranslationWWPFormE, DynamicFormTranslationTrnName, DynamicFormTranslationAttribut, DynamicFormTranslationEnglish, DynamicFormTranslationDutch FROM Trn_DynamicFormTranslation WHERE DynamicFormTranslationId = :DynamicFormTranslationId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001R3,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001R4", "SELECT TM1.DynamicFormTranslationId, TM1.DynamicFormTranslationWWpFormI, TM1.DynamicFormTranslationWWPFormV, TM1.DynamicFormTranslationWWPFormE, TM1.DynamicFormTranslationTrnName, TM1.DynamicFormTranslationAttribut, TM1.DynamicFormTranslationEnglish, TM1.DynamicFormTranslationDutch FROM Trn_DynamicFormTranslation TM1 WHERE TM1.DynamicFormTranslationId = :DynamicFormTranslationId ORDER BY TM1.DynamicFormTranslationId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001R4,100, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001R5", "SELECT DynamicFormTranslationId FROM Trn_DynamicFormTranslation WHERE DynamicFormTranslationId = :DynamicFormTranslationId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001R5,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("T001R6", "SELECT DynamicFormTranslationId FROM Trn_DynamicFormTranslation WHERE ( DynamicFormTranslationId > :DynamicFormTranslationId) ORDER BY DynamicFormTranslationId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001R6,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("T001R7", "SELECT DynamicFormTranslationId FROM Trn_DynamicFormTranslation WHERE ( DynamicFormTranslationId < :DynamicFormTranslationId) ORDER BY DynamicFormTranslationId DESC ",true, GxErrorMask.GX_NOMASK, false, this,prmT001R7,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("T001R8", "SAVEPOINT gxupdate;INSERT INTO Trn_DynamicFormTranslation(DynamicFormTranslationId, DynamicFormTranslationWWpFormI, DynamicFormTranslationWWPFormV, DynamicFormTranslationWWPFormE, DynamicFormTranslationTrnName, DynamicFormTranslationAttribut, DynamicFormTranslationEnglish, DynamicFormTranslationDutch) VALUES(:DynamicFormTranslationId, :DynamicFormTranslationWWpFormI, :DynamicFormTranslationWWPFormV, :DynamicFormTranslationWWPFormE, :DynamicFormTranslationTrnName, :DynamicFormTranslationAttribut, :DynamicFormTranslationEnglish, :DynamicFormTranslationDutch);RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT,prmT001R8)
          ,new CursorDef("T001R9", "SAVEPOINT gxupdate;UPDATE Trn_DynamicFormTranslation SET DynamicFormTranslationWWpFormI=:DynamicFormTranslationWWpFormI, DynamicFormTranslationWWPFormV=:DynamicFormTranslationWWPFormV, DynamicFormTranslationWWPFormE=:DynamicFormTranslationWWPFormE, DynamicFormTranslationTrnName=:DynamicFormTranslationTrnName, DynamicFormTranslationAttribut=:DynamicFormTranslationAttribut, DynamicFormTranslationEnglish=:DynamicFormTranslationEnglish, DynamicFormTranslationDutch=:DynamicFormTranslationDutch  WHERE DynamicFormTranslationId = :DynamicFormTranslationId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK,prmT001R9)
          ,new CursorDef("T001R10", "SAVEPOINT gxupdate;DELETE FROM Trn_DynamicFormTranslation  WHERE DynamicFormTranslationId = :DynamicFormTranslationId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK,prmT001R10)
          ,new CursorDef("T001R11", "SELECT DynamicFormTranslationId FROM Trn_DynamicFormTranslation ORDER BY DynamicFormTranslationId ",true, GxErrorMask.GX_NOMASK, false, this,prmT001R11,100, GxCacheFrequency.OFF ,true,false )
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
             ((int[]) buf[1])[0] = rslt.getInt(2);
             ((int[]) buf[2])[0] = rslt.getInt(3);
             ((int[]) buf[3])[0] = rslt.getInt(4);
             ((string[]) buf[4])[0] = rslt.getVarchar(5);
             ((string[]) buf[5])[0] = rslt.getVarchar(6);
             ((string[]) buf[6])[0] = rslt.getLongVarchar(7);
             ((string[]) buf[7])[0] = rslt.getLongVarchar(8);
             return;
          case 1 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((int[]) buf[1])[0] = rslt.getInt(2);
             ((int[]) buf[2])[0] = rslt.getInt(3);
             ((int[]) buf[3])[0] = rslt.getInt(4);
             ((string[]) buf[4])[0] = rslt.getVarchar(5);
             ((string[]) buf[5])[0] = rslt.getVarchar(6);
             ((string[]) buf[6])[0] = rslt.getLongVarchar(7);
             ((string[]) buf[7])[0] = rslt.getLongVarchar(8);
             return;
          case 2 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((int[]) buf[1])[0] = rslt.getInt(2);
             ((int[]) buf[2])[0] = rslt.getInt(3);
             ((int[]) buf[3])[0] = rslt.getInt(4);
             ((string[]) buf[4])[0] = rslt.getVarchar(5);
             ((string[]) buf[5])[0] = rslt.getVarchar(6);
             ((string[]) buf[6])[0] = rslt.getLongVarchar(7);
             ((string[]) buf[7])[0] = rslt.getLongVarchar(8);
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
          case 9 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             return;
    }
 }

}

}
