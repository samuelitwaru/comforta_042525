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
   public class prc_getdisplayvalue : GXProcedure
   {
      public prc_getdisplayvalue( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_getdisplayvalue( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_AttributeValue ,
                           string aP1_TrnName ,
                           string aP2_AttributeName ,
                           Guid aP3_primaryKey ,
                           out string aP4_AttributeValueOutput )
      {
         this.AV8AttributeValue = aP0_AttributeValue;
         this.AV15TrnName = aP1_TrnName;
         this.AV14AttributeName = aP2_AttributeName;
         this.AV11primaryKey = aP3_primaryKey;
         this.AV12AttributeValueOutput = "" ;
         initialize();
         ExecuteImpl();
         aP4_AttributeValueOutput=this.AV12AttributeValueOutput;
      }

      public string executeUdp( string aP0_AttributeValue ,
                                string aP1_TrnName ,
                                string aP2_AttributeName ,
                                Guid aP3_primaryKey )
      {
         execute(aP0_AttributeValue, aP1_TrnName, aP2_AttributeName, aP3_primaryKey, out aP4_AttributeValueOutput);
         return AV12AttributeValueOutput ;
      }

      public void executeSubmit( string aP0_AttributeValue ,
                                 string aP1_TrnName ,
                                 string aP2_AttributeName ,
                                 Guid aP3_primaryKey ,
                                 out string aP4_AttributeValueOutput )
      {
         this.AV8AttributeValue = aP0_AttributeValue;
         this.AV15TrnName = aP1_TrnName;
         this.AV14AttributeName = aP2_AttributeName;
         this.AV11primaryKey = aP3_primaryKey;
         this.AV12AttributeValueOutput = "" ;
         SubmitImpl();
         aP4_AttributeValueOutput=this.AV12AttributeValueOutput;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV13GetTranslationVar = "";
         GXt_char1 = AV13GetTranslationVar;
         new prc_gettranslation(context ).execute(  AV15TrnName,  AV11primaryKey,  AV14AttributeName, out  GXt_char1) ;
         AV13GetTranslationVar = GXt_char1;
         new prc_logtofile(context ).execute(  AV13GetTranslationVar) ;
         AV12AttributeValueOutput = "";
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13GetTranslationVar)) )
         {
            AV12AttributeValueOutput = AV8AttributeValue;
         }
         else if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13GetTranslationVar)) )
         {
            AV12AttributeValueOutput = AV13GetTranslationVar;
         }
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
         AV12AttributeValueOutput = "";
         AV13GetTranslationVar = "";
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private string GXt_char1 ;
      private string AV8AttributeValue ;
      private string AV12AttributeValueOutput ;
      private string AV13GetTranslationVar ;
      private string AV15TrnName ;
      private string AV14AttributeName ;
      private Guid AV11primaryKey ;
      private string aP4_AttributeValueOutput ;
   }

}
