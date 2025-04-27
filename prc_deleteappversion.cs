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
   public class prc_deleteappversion : GXProcedure
   {
      public prc_deleteappversion( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_deleteappversion( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_AppVersionId ,
                           out string aP1_result ,
                           out SdtSDT_Error aP2_SDT_Error )
      {
         this.AV10AppVersionId = aP0_AppVersionId;
         this.AV17result = "" ;
         this.AV9SDT_Error = new SdtSDT_Error(context) ;
         initialize();
         ExecuteImpl();
         aP1_result=this.AV17result;
         aP2_SDT_Error=this.AV9SDT_Error;
      }

      public SdtSDT_Error executeUdp( Guid aP0_AppVersionId ,
                                      out string aP1_result )
      {
         execute(aP0_AppVersionId, out aP1_result, out aP2_SDT_Error);
         return AV9SDT_Error ;
      }

      public void executeSubmit( Guid aP0_AppVersionId ,
                                 out string aP1_result ,
                                 out SdtSDT_Error aP2_SDT_Error )
      {
         this.AV10AppVersionId = aP0_AppVersionId;
         this.AV17result = "" ;
         this.AV9SDT_Error = new SdtSDT_Error(context) ;
         SubmitImpl();
         aP1_result=this.AV17result;
         aP2_SDT_Error=this.AV9SDT_Error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( ! new prc_isauthenticated(context).executeUdp( ) )
         {
            AV9SDT_Error.gxTpr_Status = context.GetMessage( "Error", "");
            AV9SDT_Error.gxTpr_Message = context.GetMessage( "Not Authenticated", "");
            cleanup();
            if (true) return;
         }
         GXt_guid1 = AV11LocationId;
         new prc_getuserlocationid(context ).execute( out  GXt_guid1) ;
         AV11LocationId = GXt_guid1;
         /* Using cursor P00EM2 */
         pr_default.execute(0, new Object[] {AV10AppVersionId, AV11LocationId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A523AppVersionId = P00EM2_A523AppVersionId[0];
            A29LocationId = P00EM2_A29LocationId[0];
            n29LocationId = P00EM2_n29LocationId[0];
            A535IsActive = P00EM2_A535IsActive[0];
            if ( A535IsActive )
            {
               pr_default.close(0);
               cleanup();
               if (true) return;
            }
            else
            {
               /* Optimized DELETE. */
               /* Using cursor P00EM3 */
               pr_default.execute(1, new Object[] {A523AppVersionId});
               pr_default.close(1);
               pr_default.SmartCacheProvider.SetUpdated("Trn_AppVersionPage");
               /* End optimized DELETE. */
               /* Using cursor P00EM4 */
               pr_default.execute(2, new Object[] {A523AppVersionId});
               pr_default.close(2);
               pr_default.SmartCacheProvider.SetUpdated("Trn_AppVersion");
               AV17result = context.GetMessage( "OK", "");
            }
            /* Exiting from a For First loop. */
            if (true) break;
         }
         pr_default.close(0);
         cleanup();
      }

      public override void cleanup( )
      {
         context.CommitDataStores("prc_deleteappversion",pr_default);
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
         ExitApp();
      }

      public override void initialize( )
      {
         AV17result = "";
         AV9SDT_Error = new SdtSDT_Error(context);
         AV11LocationId = Guid.Empty;
         GXt_guid1 = Guid.Empty;
         P00EM2_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00EM2_A29LocationId = new Guid[] {Guid.Empty} ;
         P00EM2_n29LocationId = new bool[] {false} ;
         P00EM2_A535IsActive = new bool[] {false} ;
         A523AppVersionId = Guid.Empty;
         A29LocationId = Guid.Empty;
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_deleteappversion__default(),
            new Object[][] {
                new Object[] {
               P00EM2_A523AppVersionId, P00EM2_A29LocationId, P00EM2_n29LocationId, P00EM2_A535IsActive
               }
               , new Object[] {
               }
               , new Object[] {
               }
            }
         );
         /* GeneXus formulas. */
      }

      private bool n29LocationId ;
      private bool A535IsActive ;
      private string AV17result ;
      private Guid AV10AppVersionId ;
      private Guid AV11LocationId ;
      private Guid GXt_guid1 ;
      private Guid A523AppVersionId ;
      private Guid A29LocationId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private SdtSDT_Error AV9SDT_Error ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00EM2_A523AppVersionId ;
      private Guid[] P00EM2_A29LocationId ;
      private bool[] P00EM2_n29LocationId ;
      private bool[] P00EM2_A535IsActive ;
      private string aP1_result ;
      private SdtSDT_Error aP2_SDT_Error ;
   }

   public class prc_deleteappversion__default : DataStoreHelperBase, IDataStoreHelper
   {
      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
         ,new UpdateCursor(def[1])
         ,new UpdateCursor(def[2])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP00EM2;
          prmP00EM2 = new Object[] {
          new ParDef("AV10AppVersionId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV11LocationId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00EM3;
          prmP00EM3 = new Object[] {
          new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00EM4;
          prmP00EM4 = new Object[] {
          new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00EM2", "SELECT AppVersionId, LocationId, IsActive FROM Trn_AppVersion WHERE (AppVersionId = :AV10AppVersionId) AND (LocationId = :AV11LocationId) ORDER BY AppVersionId  FOR UPDATE OF Trn_AppVersion",true, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00EM2,1, GxCacheFrequency.OFF ,true,true )
             ,new CursorDef("P00EM3", "DELETE FROM Trn_AppVersionPage  WHERE AppVersionId = :AppVersionId", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK,prmP00EM3)
             ,new CursorDef("P00EM4", "SAVEPOINT gxupdate;DELETE FROM Trn_AppVersion  WHERE AppVersionId = :AppVersionId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK,prmP00EM4)
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
                ((bool[]) buf[2])[0] = rslt.wasNull(2);
                ((bool[]) buf[3])[0] = rslt.getBool(3);
                return;
       }
    }

 }

}
