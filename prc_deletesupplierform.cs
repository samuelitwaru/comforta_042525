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
   public class prc_deletesupplierform : GXProcedure
   {
      public prc_deletesupplierform( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_deletesupplierform( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( short aP0_WWPFormId ,
                           short aP1_WWPFormVersionNumber ,
                           Guid aP2_SupplierDynamicFormId ,
                           Guid aP3_SupplierGenId ,
                           out GXBaseCollection<GeneXus.Utils.SdtMessages_Message> aP4_OutMessage )
      {
         this.A206WWPFormId = aP0_WWPFormId;
         this.A207WWPFormVersionNumber = aP1_WWPFormVersionNumber;
         this.A616SupplierDynamicFormId = aP2_SupplierDynamicFormId;
         this.A42SupplierGenId = aP3_SupplierGenId;
         this.AV9OutMessage = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus") ;
         initialize();
         ExecuteImpl();
         aP4_OutMessage=this.AV9OutMessage;
      }

      public GXBaseCollection<GeneXus.Utils.SdtMessages_Message> executeUdp( short aP0_WWPFormId ,
                                                                             short aP1_WWPFormVersionNumber ,
                                                                             Guid aP2_SupplierDynamicFormId ,
                                                                             Guid aP3_SupplierGenId )
      {
         execute(aP0_WWPFormId, aP1_WWPFormVersionNumber, aP2_SupplierDynamicFormId, aP3_SupplierGenId, out aP4_OutMessage);
         return AV9OutMessage ;
      }

      public void executeSubmit( short aP0_WWPFormId ,
                                 short aP1_WWPFormVersionNumber ,
                                 Guid aP2_SupplierDynamicFormId ,
                                 Guid aP3_SupplierGenId ,
                                 out GXBaseCollection<GeneXus.Utils.SdtMessages_Message> aP4_OutMessage )
      {
         this.A206WWPFormId = aP0_WWPFormId;
         this.A207WWPFormVersionNumber = aP1_WWPFormVersionNumber;
         this.A616SupplierDynamicFormId = aP2_SupplierDynamicFormId;
         this.A42SupplierGenId = aP3_SupplierGenId;
         this.AV9OutMessage = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus") ;
         SubmitImpl();
         aP4_OutMessage=this.AV9OutMessage;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8Trn_SupplierDynamicForm.Load(A616SupplierDynamicFormId, A42SupplierGenId);
         AV8Trn_SupplierDynamicForm.Delete();
         if ( AV8Trn_SupplierDynamicForm.Success() )
         {
            new GeneXus.Programs.workwithplus.dynamicforms.wwp_df_deleteform(context ).execute(  A206WWPFormId,  A207WWPFormVersionNumber, out  AV9OutMessage) ;
         }
         else
         {
            AV9OutMessage = AV8Trn_SupplierDynamicForm.GetMessages();
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
         AV9OutMessage = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         AV8Trn_SupplierDynamicForm = new SdtTrn_SupplierDynamicForm(context);
         /* GeneXus formulas. */
      }

      private short A206WWPFormId ;
      private short A207WWPFormVersionNumber ;
      private Guid A616SupplierDynamicFormId ;
      private Guid A42SupplierGenId ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV9OutMessage ;
      private SdtTrn_SupplierDynamicForm AV8Trn_SupplierDynamicForm ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> aP4_OutMessage ;
   }

}
