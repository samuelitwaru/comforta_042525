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
   public class prc_deletepage : GXProcedure
   {
      public prc_deletepage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_deletepage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_AppVersionId ,
                           Guid aP1_PageId ,
                           out SdtSDT_AppVersion aP2_SDT_AppVersion ,
                           out SdtSDT_Error aP3_SDT_Error )
      {
         this.AV10AppVersionId = aP0_AppVersionId;
         this.AV9PageId = aP1_PageId;
         this.AV13SDT_AppVersion = new SdtSDT_AppVersion(context) ;
         this.AV8SDT_Error = new SdtSDT_Error(context) ;
         initialize();
         ExecuteImpl();
         aP2_SDT_AppVersion=this.AV13SDT_AppVersion;
         aP3_SDT_Error=this.AV8SDT_Error;
      }

      public SdtSDT_Error executeUdp( Guid aP0_AppVersionId ,
                                      Guid aP1_PageId ,
                                      out SdtSDT_AppVersion aP2_SDT_AppVersion )
      {
         execute(aP0_AppVersionId, aP1_PageId, out aP2_SDT_AppVersion, out aP3_SDT_Error);
         return AV8SDT_Error ;
      }

      public void executeSubmit( Guid aP0_AppVersionId ,
                                 Guid aP1_PageId ,
                                 out SdtSDT_AppVersion aP2_SDT_AppVersion ,
                                 out SdtSDT_Error aP3_SDT_Error )
      {
         this.AV10AppVersionId = aP0_AppVersionId;
         this.AV9PageId = aP1_PageId;
         this.AV13SDT_AppVersion = new SdtSDT_AppVersion(context) ;
         this.AV8SDT_Error = new SdtSDT_Error(context) ;
         SubmitImpl();
         aP2_SDT_AppVersion=this.AV13SDT_AppVersion;
         aP3_SDT_Error=this.AV8SDT_Error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( ! new prc_isauthenticated(context).executeUdp( ) )
         {
            AV8SDT_Error.gxTpr_Status = context.GetMessage( "Error", "");
            AV8SDT_Error.gxTpr_Message = context.GetMessage( "Not Authenticated", "");
            cleanup();
            if (true) return;
         }
         new prc_logtoserver(context ).execute(  AV10AppVersionId.ToString()) ;
         new prc_logtoserver(context ).execute(  AV9PageId.ToString()) ;
         AV14GXLvl10 = 0;
         /* Using cursor P00BK2 */
         pr_default.execute(0, new Object[] {AV10AppVersionId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A523AppVersionId = P00BK2_A523AppVersionId[0];
            AV14GXLvl10 = 1;
            AV15GXLvl12 = 0;
            /* Using cursor P00BK3 */
            pr_default.execute(1, new Object[] {A523AppVersionId, AV9PageId});
            while ( (pr_default.getStatus(1) != 101) )
            {
               GXTBK3 = 0;
               A516PageId = P00BK3_A516PageId[0];
               AV15GXLvl12 = 1;
               /* Using cursor P00BK4 */
               pr_default.execute(2, new Object[] {A523AppVersionId, A516PageId});
               pr_default.close(2);
               pr_default.SmartCacheProvider.SetUpdated("Trn_AppVersionPage");
               GXTBK3 = 1;
               AV11BC_Trn_AppVersion.Load(AV10AppVersionId);
               new prc_loadappversionsdt(context ).execute(  AV11BC_Trn_AppVersion, out  AV13SDT_AppVersion) ;
               if ( GXTBK3 == 1 )
               {
                  context.CommitDataStores("prc_deletepage",pr_default);
               }
               /* Exiting from a For First loop. */
               if (true) break;
            }
            pr_default.close(1);
            if ( AV15GXLvl12 == 0 )
            {
               AV8SDT_Error.gxTpr_Status = context.GetMessage( "Error", "");
               AV8SDT_Error.gxTpr_Message = context.GetMessage( "Version Not Found", "");
            }
            /* Exiting from a For First loop. */
            if (true) break;
         }
         pr_default.close(0);
         if ( AV14GXLvl10 == 0 )
         {
            AV8SDT_Error.gxTpr_Status = context.GetMessage( "Error", "");
            AV8SDT_Error.gxTpr_Message = context.GetMessage( "Version Not Found", "");
         }
         cleanup();
      }

      public override void cleanup( )
      {
         context.CommitDataStores("prc_deletepage",pr_default);
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
         ExitApp();
      }

      public override void initialize( )
      {
         AV13SDT_AppVersion = new SdtSDT_AppVersion(context);
         AV8SDT_Error = new SdtSDT_Error(context);
         P00BK2_A523AppVersionId = new Guid[] {Guid.Empty} ;
         A523AppVersionId = Guid.Empty;
         P00BK3_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00BK3_A516PageId = new Guid[] {Guid.Empty} ;
         A516PageId = Guid.Empty;
         AV11BC_Trn_AppVersion = new SdtTrn_AppVersion(context);
         pr_datastore1 = new DataStoreProvider(context, new GeneXus.Programs.prc_deletepage__datastore1(),
            new Object[][] {
            }
         );
         pr_gam = new DataStoreProvider(context, new GeneXus.Programs.prc_deletepage__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_deletepage__default(),
            new Object[][] {
                new Object[] {
               P00BK2_A523AppVersionId
               }
               , new Object[] {
               P00BK3_A523AppVersionId, P00BK3_A516PageId
               }
               , new Object[] {
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV14GXLvl10 ;
      private short AV15GXLvl12 ;
      private short GXTBK3 ;
      private Guid AV10AppVersionId ;
      private Guid AV9PageId ;
      private Guid A523AppVersionId ;
      private Guid A516PageId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private SdtSDT_AppVersion AV13SDT_AppVersion ;
      private SdtSDT_Error AV8SDT_Error ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00BK2_A523AppVersionId ;
      private Guid[] P00BK3_A523AppVersionId ;
      private Guid[] P00BK3_A516PageId ;
      private SdtTrn_AppVersion AV11BC_Trn_AppVersion ;
      private SdtSDT_AppVersion aP2_SDT_AppVersion ;
      private SdtSDT_Error aP3_SDT_Error ;
      private IDataStoreProvider pr_datastore1 ;
      private IDataStoreProvider pr_gam ;
   }

   public class prc_deletepage__datastore1 : DataStoreHelperBase, IDataStoreHelper
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

 public class prc_deletepage__gam : DataStoreHelperBase, IDataStoreHelper
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

public class prc_deletepage__default : DataStoreHelperBase, IDataStoreHelper
{
   public ICursor[] getCursors( )
   {
      cursorDefinitions();
      return new Cursor[] {
       new ForEachCursor(def[0])
      ,new ForEachCursor(def[1])
      ,new UpdateCursor(def[2])
    };
 }

 private static CursorDef[] def;
 private void cursorDefinitions( )
 {
    if ( def == null )
    {
       Object[] prmP00BK2;
       prmP00BK2 = new Object[] {
       new ParDef("AV10AppVersionId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmP00BK3;
       prmP00BK3 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("AV9PageId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmP00BK4;
       prmP00BK4 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("PageId",GXType.UniqueIdentifier,36,0)
       };
       def= new CursorDef[] {
           new CursorDef("P00BK2", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :AV10AppVersionId ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00BK2,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("P00BK3", "SELECT AppVersionId, PageId FROM Trn_AppVersionPage WHERE AppVersionId = :AppVersionId and PageId = :AV9PageId ORDER BY AppVersionId, PageId ",true, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00BK3,1, GxCacheFrequency.OFF ,true,true )
          ,new CursorDef("P00BK4", "SAVEPOINT gxupdate;DELETE FROM Trn_AppVersionPage  WHERE AppVersionId = :AppVersionId AND PageId = :PageId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK,prmP00BK4)
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
             return;
          case 1 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((Guid[]) buf[1])[0] = rslt.getGuid(2);
             return;
    }
 }

}

}
