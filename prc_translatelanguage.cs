using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
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
   public class prc_translatelanguage : GXProcedure
   {
      public prc_translatelanguage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_translatelanguage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_from ,
                           string aP1_to ,
                           string aP2_LanguageFrom ,
                           out string aP3_LanguageTo )
      {
         this.AV12from = aP0_from;
         this.AV13to = aP1_to;
         this.AV14LanguageFrom = aP2_LanguageFrom;
         this.AV15LanguageTo = "" ;
         initialize();
         ExecuteImpl();
         aP3_LanguageTo=this.AV15LanguageTo;
      }

      public string executeUdp( string aP0_from ,
                                string aP1_to ,
                                string aP2_LanguageFrom )
      {
         execute(aP0_from, aP1_to, aP2_LanguageFrom, out aP3_LanguageTo);
         return AV15LanguageTo ;
      }

      public void executeSubmit( string aP0_from ,
                                 string aP1_to ,
                                 string aP2_LanguageFrom ,
                                 out string aP3_LanguageTo )
      {
         this.AV12from = aP0_from;
         this.AV13to = aP1_to;
         this.AV14LanguageFrom = aP2_LanguageFrom;
         this.AV15LanguageTo = "" ;
         SubmitImpl();
         aP3_LanguageTo=this.AV15LanguageTo;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV14LanguageFrom = AV15LanguageTo;
         AV16HttpClient.BaseURL = context.GetMessage( "https://api-b2b.backenster.com", "");
         AV16HttpClient.AddHeader(context.GetMessage( "Content-type", ""), context.GetMessage( "application/json", ""));
         AV16HttpClient.AddHeader(context.GetMessage( "Authorization", ""), context.GetMessage( "Bearer a_FPErHAYaF0j7aGdubWnroJR40Q9TvO4X7ciQCdwnQv5lw3tPDnoGVL2LlsaiIXxykUJ7uMwWpU4Co6Mv", ""));
         AV17RequestBody.gxTpr_From = AV12from;
         AV17RequestBody.gxTpr_To = AV13to;
         AV17RequestBody.gxTpr_Data = AV14LanguageFrom;
         AV17RequestBody.gxTpr_Platform = context.GetMessage( "api", "");
         AV17RequestBody.gxTpr_Translatemode = context.GetMessage( "html", "");
         AV18body = AV17RequestBody.ToJSonString(false, true);
         AV16HttpClient.AddString(AV18body);
         AV16HttpClient.Execute(context.GetMessage( "POST", ""), context.GetMessage( "/b1/api/v3/translate", ""));
         AV19responsejson = AV16HttpClient.ToString();
         if ( AV16HttpClient.StatusCode == 200 )
         {
            AV20Translated.FromJSonString(AV19responsejson, null);
            AV15LanguageTo = AV20Translated.gxTpr_Result;
         }
         else
         {
            new prc_logtofile(context ).execute(  AV20Translated.gxTpr_Err) ;
            new prc_logtofile(context ).execute(  StringUtil.Str( (decimal)(AV16HttpClient.StatusCode), 10, 2)) ;
         }
         new prc_logtoserver(context ).execute(  context.GetMessage( "				Translated: ", "")+AV12from+context.GetMessage( " to ", "")+AV13to+" >> "+AV14LanguageFrom+" >> "+AV15LanguageTo) ;
         cleanup();
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
         AV15LanguageTo = "";
         AV16HttpClient = new GxHttpClient( context);
         AV17RequestBody = new SdtSDTLanguageRequestBody(context);
         AV18body = "";
         AV19responsejson = "";
         AV20Translated = new SdtSDTLanguageResponseBody(context);
         /* GeneXus formulas. */
      }

      private string AV12from ;
      private string AV13to ;
      private string AV14LanguageFrom ;
      private string AV15LanguageTo ;
      private string AV18body ;
      private string AV19responsejson ;
      private GxHttpClient AV16HttpClient ;
      private SdtSDTLanguageRequestBody AV17RequestBody ;
      private SdtSDTLanguageResponseBody AV20Translated ;
      private string aP3_LanguageTo ;
   }

}
