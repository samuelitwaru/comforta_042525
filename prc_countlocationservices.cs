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
   public class prc_countlocationservices : GXProcedure
   {
      public prc_countlocationservices( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_countlocationservices( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( out short aP0_LocationServicesCount )
      {
         this.AV11LocationServicesCount = 0 ;
         initialize();
         ExecuteImpl();
         aP0_LocationServicesCount=this.AV11LocationServicesCount;
      }

      public short executeUdp( )
      {
         execute(out aP0_LocationServicesCount);
         return AV11LocationServicesCount ;
      }

      public void executeSubmit( out short aP0_LocationServicesCount )
      {
         this.AV11LocationServicesCount = 0 ;
         SubmitImpl();
         aP0_LocationServicesCount=this.AV11LocationServicesCount;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11LocationServicesCount = 0;
         AV13Udparg1 = new prc_getuserlocationid(context).executeUdp( );
         /* Using cursor P00F03 */
         pr_default.execute(0, new Object[] {AV13Udparg1});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A11OrganisationId = P00F03_A11OrganisationId[0];
            A29LocationId = P00F03_A29LocationId[0];
            A40000GXC1 = P00F03_A40000GXC1[0];
            n40000GXC1 = P00F03_n40000GXC1[0];
            A58ProductServiceId = P00F03_A58ProductServiceId[0];
            A40000GXC1 = P00F03_A40000GXC1[0];
            n40000GXC1 = P00F03_n40000GXC1[0];
            AV11LocationServicesCount = (short)(A40000GXC1);
            pr_default.readNext(0);
         }
         pr_default.close(0);
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
         AV13Udparg1 = Guid.Empty;
         P00F03_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00F03_A29LocationId = new Guid[] {Guid.Empty} ;
         P00F03_A40000GXC1 = new int[1] ;
         P00F03_n40000GXC1 = new bool[] {false} ;
         P00F03_A58ProductServiceId = new Guid[] {Guid.Empty} ;
         A11OrganisationId = Guid.Empty;
         A29LocationId = Guid.Empty;
         A58ProductServiceId = Guid.Empty;
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_countlocationservices__default(),
            new Object[][] {
                new Object[] {
               P00F03_A11OrganisationId, P00F03_A29LocationId, P00F03_A40000GXC1, P00F03_n40000GXC1, P00F03_A58ProductServiceId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV11LocationServicesCount ;
      private int A40000GXC1 ;
      private bool n40000GXC1 ;
      private Guid AV13Udparg1 ;
      private Guid A11OrganisationId ;
      private Guid A29LocationId ;
      private Guid A58ProductServiceId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00F03_A11OrganisationId ;
      private Guid[] P00F03_A29LocationId ;
      private int[] P00F03_A40000GXC1 ;
      private bool[] P00F03_n40000GXC1 ;
      private Guid[] P00F03_A58ProductServiceId ;
      private short aP0_LocationServicesCount ;
   }

   public class prc_countlocationservices__default : DataStoreHelperBase, IDataStoreHelper
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
          Object[] prmP00F03;
          prmP00F03 = new Object[] {
          new ParDef("AV13Udparg1",GXType.UniqueIdentifier,36,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00F03", "SELECT T1.OrganisationId, T1.LocationId, COALESCE( T2.GXC1, 0) AS GXC1, T1.ProductServiceId FROM (Trn_ProductService T1 LEFT JOIN LATERAL (SELECT COUNT(*) AS GXC1, LocationId, OrganisationId FROM Trn_ProductService WHERE T1.LocationId = LocationId and T1.OrganisationId = OrganisationId GROUP BY LocationId, OrganisationId ) T2 ON T2.LocationId = T1.LocationId AND T2.OrganisationId = T1.OrganisationId) WHERE T1.LocationId = :AV13Udparg1 ORDER BY T1.LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00F03,100, GxCacheFrequency.OFF ,false,false )
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
                ((int[]) buf[2])[0] = rslt.getInt(3);
                ((bool[]) buf[3])[0] = rslt.wasNull(3);
                ((Guid[]) buf[4])[0] = rslt.getGuid(4);
                return;
       }
    }

 }

}
