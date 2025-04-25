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
   public class prc_countlocationfilledforms : GXProcedure
   {
      public prc_countlocationfilledforms( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_countlocationfilledforms( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( out short aP0_LocationFilledFormsCount )
      {
         this.AV13LocationFilledFormsCount = 0 ;
         initialize();
         ExecuteImpl();
         aP0_LocationFilledFormsCount=this.AV13LocationFilledFormsCount;
      }

      public short executeUdp( )
      {
         execute(out aP0_LocationFilledFormsCount);
         return AV13LocationFilledFormsCount ;
      }

      public void executeSubmit( out short aP0_LocationFilledFormsCount )
      {
         this.AV13LocationFilledFormsCount = 0 ;
         SubmitImpl();
         aP0_LocationFilledFormsCount=this.AV13LocationFilledFormsCount;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV13LocationFilledFormsCount = 0;
         /* Using cursor P00EY2 */
         pr_default.execute(0);
         while ( (pr_default.getStatus(0) != 101) )
         {
            A112WWPUserExtendedId = P00EY2_A112WWPUserExtendedId[0];
            A214WWPFormInstanceId = P00EY2_A214WWPFormInstanceId[0];
            AV12WWPUserExtendedId = StringUtil.StrToGuid( A112WWPUserExtendedId);
            AV16Udparg1 = new prc_getuserlocationid(context).executeUdp( );
            /* Using cursor P00EY4 */
            pr_default.execute(1, new Object[] {AV12WWPUserExtendedId, AV16Udparg1});
            while ( (pr_default.getStatus(1) != 101) )
            {
               A11OrganisationId = P00EY4_A11OrganisationId[0];
               A29LocationId = P00EY4_A29LocationId[0];
               A62ResidentId = P00EY4_A62ResidentId[0];
               A40000GXC1 = P00EY4_A40000GXC1[0];
               n40000GXC1 = P00EY4_n40000GXC1[0];
               A40000GXC1 = P00EY4_A40000GXC1[0];
               n40000GXC1 = P00EY4_n40000GXC1[0];
               AV13LocationFilledFormsCount = (short)(A40000GXC1);
               pr_default.readNext(1);
            }
            pr_default.close(1);
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
         P00EY2_A112WWPUserExtendedId = new string[] {""} ;
         P00EY2_A214WWPFormInstanceId = new int[1] ;
         A112WWPUserExtendedId = "";
         AV12WWPUserExtendedId = Guid.Empty;
         AV16Udparg1 = Guid.Empty;
         P00EY4_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00EY4_A29LocationId = new Guid[] {Guid.Empty} ;
         P00EY4_A62ResidentId = new Guid[] {Guid.Empty} ;
         P00EY4_A40000GXC1 = new int[1] ;
         P00EY4_n40000GXC1 = new bool[] {false} ;
         A11OrganisationId = Guid.Empty;
         A29LocationId = Guid.Empty;
         A62ResidentId = Guid.Empty;
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_countlocationfilledforms__default(),
            new Object[][] {
                new Object[] {
               P00EY2_A112WWPUserExtendedId, P00EY2_A214WWPFormInstanceId
               }
               , new Object[] {
               P00EY4_A11OrganisationId, P00EY4_A29LocationId, P00EY4_A62ResidentId, P00EY4_A40000GXC1, P00EY4_n40000GXC1
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV13LocationFilledFormsCount ;
      private int A214WWPFormInstanceId ;
      private int A40000GXC1 ;
      private string A112WWPUserExtendedId ;
      private bool n40000GXC1 ;
      private Guid AV12WWPUserExtendedId ;
      private Guid AV16Udparg1 ;
      private Guid A11OrganisationId ;
      private Guid A29LocationId ;
      private Guid A62ResidentId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private IDataStoreProvider pr_default ;
      private string[] P00EY2_A112WWPUserExtendedId ;
      private int[] P00EY2_A214WWPFormInstanceId ;
      private Guid[] P00EY4_A11OrganisationId ;
      private Guid[] P00EY4_A29LocationId ;
      private Guid[] P00EY4_A62ResidentId ;
      private int[] P00EY4_A40000GXC1 ;
      private bool[] P00EY4_n40000GXC1 ;
      private short aP0_LocationFilledFormsCount ;
   }

   public class prc_countlocationfilledforms__default : DataStoreHelperBase, IDataStoreHelper
   {
      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
         ,new ForEachCursor(def[1])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP00EY2;
          prmP00EY2 = new Object[] {
          };
          Object[] prmP00EY4;
          prmP00EY4 = new Object[] {
          new ParDef("AV12WWPUserExtendedId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV16Udparg1",GXType.UniqueIdentifier,36,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00EY2", "SELECT WWPUserExtendedId, WWPFormInstanceId FROM WWP_FormInstance ORDER BY WWPFormInstanceId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00EY2,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00EY4", "SELECT T1.OrganisationId, T1.LocationId, T1.ResidentId, COALESCE( T2.GXC1, 0) AS GXC1 FROM (Trn_Resident T1 LEFT JOIN LATERAL (SELECT COUNT(*) AS GXC1, LocationId, OrganisationId FROM Trn_Resident WHERE T1.LocationId = LocationId and T1.OrganisationId = OrganisationId GROUP BY LocationId, OrganisationId ) T2 ON T2.LocationId = T1.LocationId AND T2.OrganisationId = T1.OrganisationId) WHERE T1.ResidentId = :AV12WWPUserExtendedId and T1.LocationId = :AV16Udparg1 ORDER BY T1.ResidentId, T1.LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00EY4,100, GxCacheFrequency.OFF ,false,false )
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
                ((string[]) buf[0])[0] = rslt.getString(1, 40);
                ((int[]) buf[1])[0] = rslt.getInt(2);
                return;
             case 1 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((Guid[]) buf[1])[0] = rslt.getGuid(2);
                ((Guid[]) buf[2])[0] = rslt.getGuid(3);
                ((int[]) buf[3])[0] = rslt.getInt(4);
                ((bool[]) buf[4])[0] = rslt.wasNull(4);
                return;
       }
    }

 }

}
