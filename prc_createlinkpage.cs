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
   public class prc_createlinkpage : GXProcedure
   {
      public prc_createlinkpage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_createlinkpage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_AppVersionId ,
                           string aP1_PageName ,
                           string aP2_Url ,
                           short aP3_WWPFormId ,
                           out SdtSDT_AppVersion_PagesItem aP4_PageItem ,
                           out SdtSDT_Error aP5_SDT_Error )
      {
         this.AV8AppVersionId = aP0_AppVersionId;
         this.AV9PageName = aP1_PageName;
         this.AV24Url = aP2_Url;
         this.AV19WWPFormId = aP3_WWPFormId;
         this.AV11PageItem = new SdtSDT_AppVersion_PagesItem(context) ;
         this.AV10SDT_Error = new SdtSDT_Error(context) ;
         initialize();
         ExecuteImpl();
         aP4_PageItem=this.AV11PageItem;
         aP5_SDT_Error=this.AV10SDT_Error;
      }

      public SdtSDT_Error executeUdp( Guid aP0_AppVersionId ,
                                      string aP1_PageName ,
                                      string aP2_Url ,
                                      short aP3_WWPFormId ,
                                      out SdtSDT_AppVersion_PagesItem aP4_PageItem )
      {
         execute(aP0_AppVersionId, aP1_PageName, aP2_Url, aP3_WWPFormId, out aP4_PageItem, out aP5_SDT_Error);
         return AV10SDT_Error ;
      }

      public void executeSubmit( Guid aP0_AppVersionId ,
                                 string aP1_PageName ,
                                 string aP2_Url ,
                                 short aP3_WWPFormId ,
                                 out SdtSDT_AppVersion_PagesItem aP4_PageItem ,
                                 out SdtSDT_Error aP5_SDT_Error )
      {
         this.AV8AppVersionId = aP0_AppVersionId;
         this.AV9PageName = aP1_PageName;
         this.AV24Url = aP2_Url;
         this.AV19WWPFormId = aP3_WWPFormId;
         this.AV11PageItem = new SdtSDT_AppVersion_PagesItem(context) ;
         this.AV10SDT_Error = new SdtSDT_Error(context) ;
         SubmitImpl();
         aP4_PageItem=this.AV11PageItem;
         aP5_SDT_Error=this.AV10SDT_Error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( ! new prc_isauthenticated(context).executeUdp( ) )
         {
            AV10SDT_Error.gxTpr_Status = context.GetMessage( "Error", "");
            AV10SDT_Error.gxTpr_Message = context.GetMessage( "Not Authenticated", "");
            cleanup();
            if (true) return;
         }
         AV14BC_Trn_AppVersion.Load(AV8AppVersionId);
         new prc_logtoserver(context ).execute(  ">><<>>"+AV8AppVersionId.ToString()+" "+AV9PageName+" "+AV24Url+" "+StringUtil.Str( (decimal)(AV19WWPFormId), 4, 0)) ;
         AV13BC_Page.gxTpr_Pageid = Guid.NewGuid( );
         AV13BC_Page.gxTpr_Pagename = AV9PageName;
         AV16SDT_LinkPage = new SdtSDT_LinkPage(context);
         if ( ! (0==AV19WWPFormId) )
         {
            AV16SDT_LinkPage.gxTpr_Wwpformid = AV19WWPFormId;
            /* Using cursor P00GC2 */
            pr_default.execute(0, new Object[] {AV19WWPFormId});
            while ( (pr_default.getStatus(0) != 101) )
            {
               A206WWPFormId = P00GC2_A206WWPFormId[0];
               A207WWPFormVersionNumber = P00GC2_A207WWPFormVersionNumber[0];
               AV20GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context).get();
               AV21baseUrl = AV20GAMApplication.gxTpr_Environment.gxTpr_Url;
               AV16SDT_LinkPage.gxTpr_Url = AV21baseUrl+"utoolboxdynamicform.aspx?WWPFormId="+StringUtil.Trim( StringUtil.Str( (decimal)(AV19WWPFormId), 4, 0))+context.GetMessage( "&WWPDynamicFormMode=DSP&DefaultFormType=&WWPFormType=0", "");
               AV13BC_Page.gxTpr_Pagetype = "DynamicForm";
               pr_default.readNext(0);
            }
            pr_default.close(0);
         }
         else
         {
            AV16SDT_LinkPage.gxTpr_Url = AV24Url;
            AV13BC_Page.gxTpr_Pagetype = "WebLink";
         }
         AV13BC_Page.gxTpr_Pagestructure = AV16SDT_LinkPage.ToJSonString(false, true);
         AV14BC_Trn_AppVersion.gxTpr_Page.Add(AV13BC_Page, 0);
         AV14BC_Trn_AppVersion.Save();
         if ( AV14BC_Trn_AppVersion.Success() )
         {
            context.CommitDataStores("prc_createlinkpage",pr_default);
            AV11PageItem.FromJSonString(AV13BC_Page.ToJSonString(true, true), null);
         }
         else
         {
            AV27GXV2 = 1;
            AV26GXV1 = AV14BC_Trn_AppVersion.GetMessages();
            while ( AV27GXV2 <= AV26GXV1.Count )
            {
               AV15Message = ((GeneXus.Utils.SdtMessages_Message)AV26GXV1.Item(AV27GXV2));
               AV10SDT_Error.gxTpr_Message = AV15Message.gxTpr_Description;
               AV27GXV2 = (int)(AV27GXV2+1);
            }
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
         AV11PageItem = new SdtSDT_AppVersion_PagesItem(context);
         AV10SDT_Error = new SdtSDT_Error(context);
         AV14BC_Trn_AppVersion = new SdtTrn_AppVersion(context);
         AV13BC_Page = new SdtTrn_AppVersion_Page(context);
         AV16SDT_LinkPage = new SdtSDT_LinkPage(context);
         P00GC2_A206WWPFormId = new short[1] ;
         P00GC2_A207WWPFormVersionNumber = new short[1] ;
         AV20GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context);
         AV21baseUrl = "";
         AV26GXV1 = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         AV15Message = new GeneXus.Utils.SdtMessages_Message(context);
         pr_datastore1 = new DataStoreProvider(context, new GeneXus.Programs.prc_createlinkpage__datastore1(),
            new Object[][] {
            }
         );
         pr_gam = new DataStoreProvider(context, new GeneXus.Programs.prc_createlinkpage__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_createlinkpage__default(),
            new Object[][] {
                new Object[] {
               P00GC2_A206WWPFormId, P00GC2_A207WWPFormVersionNumber
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV19WWPFormId ;
      private short A206WWPFormId ;
      private short A207WWPFormVersionNumber ;
      private int AV27GXV2 ;
      private string AV9PageName ;
      private string AV24Url ;
      private string AV21baseUrl ;
      private Guid AV8AppVersionId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private SdtSDT_AppVersion_PagesItem AV11PageItem ;
      private SdtSDT_Error AV10SDT_Error ;
      private SdtTrn_AppVersion AV14BC_Trn_AppVersion ;
      private SdtTrn_AppVersion_Page AV13BC_Page ;
      private SdtSDT_LinkPage AV16SDT_LinkPage ;
      private IDataStoreProvider pr_default ;
      private short[] P00GC2_A206WWPFormId ;
      private short[] P00GC2_A207WWPFormVersionNumber ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplication AV20GAMApplication ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV26GXV1 ;
      private GeneXus.Utils.SdtMessages_Message AV15Message ;
      private SdtSDT_AppVersion_PagesItem aP4_PageItem ;
      private SdtSDT_Error aP5_SDT_Error ;
      private IDataStoreProvider pr_datastore1 ;
      private IDataStoreProvider pr_gam ;
   }

   public class prc_createlinkpage__datastore1 : DataStoreHelperBase, IDataStoreHelper
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

 public class prc_createlinkpage__gam : DataStoreHelperBase, IDataStoreHelper
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

public class prc_createlinkpage__default : DataStoreHelperBase, IDataStoreHelper
{
   public ICursor[] getCursors( )
   {
      cursorDefinitions();
      return new Cursor[] {
       new ForEachCursor(def[0])
    };
 }

 private static CursorDef[] def;
 private void cursorDefinitions( )
 {
    if ( def == null )
    {
       Object[] prmP00GC2;
       prmP00GC2 = new Object[] {
       new ParDef("AV19WWPFormId",GXType.Int16,4,0)
       };
       def= new CursorDef[] {
           new CursorDef("P00GC2", "SELECT WWPFormId, WWPFormVersionNumber FROM WWP_Form WHERE WWPFormId = :AV19WWPFormId ORDER BY WWPFormId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00GC2,100, GxCacheFrequency.OFF ,false,false )
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
             return;
    }
 }

}

}
