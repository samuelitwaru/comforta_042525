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
using GeneXus.Procedure;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace GeneXus.Programs {
   public class prc_deletelocationform : GXProcedure
   {
      public prc_deletelocationform( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_deletelocationform( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( short aP0_WWPFormId ,
                           short aP1_WWPFormVersionNumber ,
                           Guid aP2_LocationDynamicFormId ,
                           Guid aP3_OrganisationId ,
                           Guid aP4_LocationId ,
                           out GXBaseCollection<GeneXus.Utils.SdtMessages_Message> aP5_OutMessage )
      {
         this.A206WWPFormId = aP0_WWPFormId;
         this.A207WWPFormVersionNumber = aP1_WWPFormVersionNumber;
         this.A366LocationDynamicFormId = aP2_LocationDynamicFormId;
         this.A11OrganisationId = aP3_OrganisationId;
         this.A29LocationId = aP4_LocationId;
         this.AV9OutMessage = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus") ;
         initialize();
         ExecuteImpl();
         aP5_OutMessage=this.AV9OutMessage;
      }

      public GXBaseCollection<GeneXus.Utils.SdtMessages_Message> executeUdp( short aP0_WWPFormId ,
                                                                             short aP1_WWPFormVersionNumber ,
                                                                             Guid aP2_LocationDynamicFormId ,
                                                                             Guid aP3_OrganisationId ,
                                                                             Guid aP4_LocationId )
      {
         execute(aP0_WWPFormId, aP1_WWPFormVersionNumber, aP2_LocationDynamicFormId, aP3_OrganisationId, aP4_LocationId, out aP5_OutMessage);
         return AV9OutMessage ;
      }

      public void executeSubmit( short aP0_WWPFormId ,
                                 short aP1_WWPFormVersionNumber ,
                                 Guid aP2_LocationDynamicFormId ,
                                 Guid aP3_OrganisationId ,
                                 Guid aP4_LocationId ,
                                 out GXBaseCollection<GeneXus.Utils.SdtMessages_Message> aP5_OutMessage )
      {
         this.A206WWPFormId = aP0_WWPFormId;
         this.A207WWPFormVersionNumber = aP1_WWPFormVersionNumber;
         this.A366LocationDynamicFormId = aP2_LocationDynamicFormId;
         this.A11OrganisationId = aP3_OrganisationId;
         this.A29LocationId = aP4_LocationId;
         this.AV9OutMessage = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus") ;
         SubmitImpl();
         aP5_OutMessage=this.AV9OutMessage;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10LocationDynamicFormId = A366LocationDynamicFormId;
         /* Optimized DELETE. */
         /* Using cursor P007H2 */
         pr_default.execute(0, new Object[] {AV10LocationDynamicFormId, A11OrganisationId, A29LocationId});
         pr_default.close(0);
         pr_default.SmartCacheProvider.SetUpdated("Trn_CallToAction");
         /* End optimized DELETE. */
         AV8Trn_LocationDynamicForm.Load(A366LocationDynamicFormId, A11OrganisationId, A29LocationId);
         AV8Trn_LocationDynamicForm.Delete();
         if ( AV8Trn_LocationDynamicForm.Success() )
         {
            context.CommitDataStores("prc_deletelocationform",pr_default);
            new GeneXus.Programs.workwithplus.dynamicforms.wwp_df_deleteform(context ).execute(  A206WWPFormId,  A207WWPFormVersionNumber, out  AV9OutMessage) ;
         }
         else
         {
            new prc_logtofile(context ).execute(  context.GetMessage( "Error on location form delete", "")) ;
            AV9OutMessage = AV8Trn_LocationDynamicForm.GetMessages();
         }
         cleanup();
      }

      public override void cleanup( )
      {
         context.CommitDataStores("prc_deletelocationform",pr_default);
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
         AV10LocationDynamicFormId = Guid.Empty;
         AV8Trn_LocationDynamicForm = new SdtTrn_LocationDynamicForm(context);
         pr_datastore1 = new DataStoreProvider(context, new GeneXus.Programs.prc_deletelocationform__datastore1(),
            new Object[][] {
            }
         );
         pr_gam = new DataStoreProvider(context, new GeneXus.Programs.prc_deletelocationform__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_deletelocationform__default(),
            new Object[][] {
                new Object[] {
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short A206WWPFormId ;
      private short A207WWPFormVersionNumber ;
      private Guid A366LocationDynamicFormId ;
      private Guid A11OrganisationId ;
      private Guid A29LocationId ;
      private Guid AV10LocationDynamicFormId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV9OutMessage ;
      private IDataStoreProvider pr_default ;
      private SdtTrn_LocationDynamicForm AV8Trn_LocationDynamicForm ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> aP5_OutMessage ;
      private IDataStoreProvider pr_datastore1 ;
      private IDataStoreProvider pr_gam ;
   }

   public class prc_deletelocationform__datastore1 : DataStoreHelperBase, IDataStoreHelper
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

 public class prc_deletelocationform__gam : DataStoreHelperBase, IDataStoreHelper
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

public class prc_deletelocationform__default : DataStoreHelperBase, IDataStoreHelper
{
   public ICursor[] getCursors( )
   {
      cursorDefinitions();
      return new Cursor[] {
       new UpdateCursor(def[0])
    };
 }

 private static CursorDef[] def;
 private void cursorDefinitions( )
 {
    if ( def == null )
    {
       Object[] prmP007H2;
       prmP007H2 = new Object[] {
       new ParDef("AV10LocationDynamicFormId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("OrganisationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("LocationId",GXType.UniqueIdentifier,36,0)
       };
       def= new CursorDef[] {
           new CursorDef("P007H2", "DELETE FROM Trn_CallToAction  WHERE LocationDynamicFormId = :AV10LocationDynamicFormId and OrganisationId = :OrganisationId and LocationId = :LocationId", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK,prmP007H2)
       };
    }
 }

 public void getResults( int cursor ,
                         IFieldGetter rslt ,
                         Object[] buf )
 {
 }

}

}
