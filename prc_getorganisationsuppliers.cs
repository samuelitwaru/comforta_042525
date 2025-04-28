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
   public class prc_getorganisationsuppliers : GXProcedure
   {
      public prc_getorganisationsuppliers( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_getorganisationsuppliers( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_OrganisationId ,
                           out GXBCCollection<SdtTrn_SupplierGen> aP1_BC_Trn_GenSuppliers )
      {
         this.AV14OrganisationId = aP0_OrganisationId;
         this.AV26BC_Trn_GenSuppliers = new GXBCCollection<SdtTrn_SupplierGen>( context, "Trn_SupplierGen", "Comforta_version20") ;
         initialize();
         ExecuteImpl();
         aP1_BC_Trn_GenSuppliers=this.AV26BC_Trn_GenSuppliers;
      }

      public GXBCCollection<SdtTrn_SupplierGen> executeUdp( Guid aP0_OrganisationId )
      {
         execute(aP0_OrganisationId, out aP1_BC_Trn_GenSuppliers);
         return AV26BC_Trn_GenSuppliers ;
      }

      public void executeSubmit( Guid aP0_OrganisationId ,
                                 out GXBCCollection<SdtTrn_SupplierGen> aP1_BC_Trn_GenSuppliers )
      {
         this.AV14OrganisationId = aP0_OrganisationId;
         this.AV26BC_Trn_GenSuppliers = new GXBCCollection<SdtTrn_SupplierGen>( context, "Trn_SupplierGen", "Comforta_version20") ;
         SubmitImpl();
         aP1_BC_Trn_GenSuppliers=this.AV26BC_Trn_GenSuppliers;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Using cursor P00G22 */
         pr_default.execute(0);
         while ( (pr_default.getStatus(0) != 101) )
         {
            A602SG_LocationSupplierOrganisatio = P00G22_A602SG_LocationSupplierOrganisatio[0];
            n602SG_LocationSupplierOrganisatio = P00G22_n602SG_LocationSupplierOrganisatio[0];
            A601SG_LocationSupplierLocationId = P00G22_A601SG_LocationSupplierLocationId[0];
            n601SG_LocationSupplierLocationId = P00G22_n601SG_LocationSupplierLocationId[0];
            A584ActiveAppVersionId = P00G22_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P00G22_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = P00G22_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P00G22_n598PublishedActiveAppVersionId[0];
            A42SupplierGenId = P00G22_A42SupplierGenId[0];
            A584ActiveAppVersionId = P00G22_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P00G22_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = P00G22_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P00G22_n598PublishedActiveAppVersionId[0];
            /* Using cursor P00G23 */
            pr_default.execute(1, new Object[] {n602SG_LocationSupplierOrganisatio, A602SG_LocationSupplierOrganisatio});
            pr_default.close(1);
            /* Using cursor P00G24 */
            pr_default.execute(2, new Object[] {n584ActiveAppVersionId, A584ActiveAppVersionId});
            pr_default.close(2);
            /* Using cursor P00G25 */
            pr_default.execute(3, new Object[] {n598PublishedActiveAppVersionId, A598PublishedActiveAppVersionId});
            pr_default.close(3);
            AV27BC_Trn_GenSupplier = new SdtTrn_SupplierGen(context);
            AV27BC_Trn_GenSupplier.Load(A42SupplierGenId);
            AV26BC_Trn_GenSuppliers.Add(AV27BC_Trn_GenSupplier, 0);
            pr_default.readNext(0);
         }
         pr_default.close(0);
         pr_default.close(1);
         pr_default.close(3);
         AV26BC_Trn_GenSuppliers.Sort("SupplierGenCompanyName");
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

      protected override void CloseCursors( )
      {
         pr_default.close(2);
      }

      public override void initialize( )
      {
         AV26BC_Trn_GenSuppliers = new GXBCCollection<SdtTrn_SupplierGen>( context, "Trn_SupplierGen", "Comforta_version20");
         P00G22_A602SG_LocationSupplierOrganisatio = new Guid[] {Guid.Empty} ;
         P00G22_n602SG_LocationSupplierOrganisatio = new bool[] {false} ;
         P00G22_A601SG_LocationSupplierLocationId = new Guid[] {Guid.Empty} ;
         P00G22_n601SG_LocationSupplierLocationId = new bool[] {false} ;
         P00G22_A584ActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P00G22_n584ActiveAppVersionId = new bool[] {false} ;
         P00G22_A598PublishedActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P00G22_n598PublishedActiveAppVersionId = new bool[] {false} ;
         P00G22_A42SupplierGenId = new Guid[] {Guid.Empty} ;
         A602SG_LocationSupplierOrganisatio = Guid.Empty;
         A601SG_LocationSupplierLocationId = Guid.Empty;
         A584ActiveAppVersionId = Guid.Empty;
         A598PublishedActiveAppVersionId = Guid.Empty;
         A42SupplierGenId = Guid.Empty;
         P00G23_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00G23_n11OrganisationId = new bool[] {false} ;
         P00G24_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00G25_A523AppVersionId = new Guid[] {Guid.Empty} ;
         AV27BC_Trn_GenSupplier = new SdtTrn_SupplierGen(context);
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_getorganisationsuppliers__default(),
            new Object[][] {
                new Object[] {
               P00G22_A602SG_LocationSupplierOrganisatio, P00G22_n602SG_LocationSupplierOrganisatio, P00G22_A601SG_LocationSupplierLocationId, P00G22_n601SG_LocationSupplierLocationId, P00G22_A584ActiveAppVersionId, P00G22_n584ActiveAppVersionId, P00G22_A598PublishedActiveAppVersionId, P00G22_n598PublishedActiveAppVersionId, P00G22_A42SupplierGenId
               }
               , new Object[] {
               P00G23_A11OrganisationId
               }
               , new Object[] {
               P00G24_A523AppVersionId
               }
               , new Object[] {
               P00G25_A523AppVersionId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private bool n602SG_LocationSupplierOrganisatio ;
      private bool n601SG_LocationSupplierLocationId ;
      private bool n584ActiveAppVersionId ;
      private bool n598PublishedActiveAppVersionId ;
      private Guid AV14OrganisationId ;
      private Guid A602SG_LocationSupplierOrganisatio ;
      private Guid A601SG_LocationSupplierLocationId ;
      private Guid A584ActiveAppVersionId ;
      private Guid A598PublishedActiveAppVersionId ;
      private Guid A42SupplierGenId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXBCCollection<SdtTrn_SupplierGen> AV26BC_Trn_GenSuppliers ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00G22_A602SG_LocationSupplierOrganisatio ;
      private bool[] P00G22_n602SG_LocationSupplierOrganisatio ;
      private Guid[] P00G22_A601SG_LocationSupplierLocationId ;
      private bool[] P00G22_n601SG_LocationSupplierLocationId ;
      private Guid[] P00G22_A584ActiveAppVersionId ;
      private bool[] P00G22_n584ActiveAppVersionId ;
      private Guid[] P00G22_A598PublishedActiveAppVersionId ;
      private bool[] P00G22_n598PublishedActiveAppVersionId ;
      private Guid[] P00G22_A42SupplierGenId ;
      private Guid[] P00G23_A11OrganisationId ;
      private bool[] P00G23_n11OrganisationId ;
      private Guid[] P00G24_A523AppVersionId ;
      private Guid[] P00G25_A523AppVersionId ;
      private SdtTrn_SupplierGen AV27BC_Trn_GenSupplier ;
      private GXBCCollection<SdtTrn_SupplierGen> aP1_BC_Trn_GenSuppliers ;
   }

   public class prc_getorganisationsuppliers__default : DataStoreHelperBase, IDataStoreHelper
   {
      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
         ,new ForEachCursor(def[1])
         ,new ForEachCursor(def[2])
         ,new ForEachCursor(def[3])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP00G22;
          prmP00G22 = new Object[] {
          };
          Object[] prmP00G23;
          prmP00G23 = new Object[] {
          new ParDef("SG_LocationSupplierOrganisatio",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP00G24;
          prmP00G24 = new Object[] {
          new ParDef("ActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP00G25;
          prmP00G25 = new Object[] {
          new ParDef("PublishedActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          def= new CursorDef[] {
              new CursorDef("P00G22", "SELECT T1.SG_LocationSupplierOrganisatio AS SG_LocationSupplierOrganisatio, T1.SG_LocationSupplierLocationId AS SG_LocationSupplierLocationId, T2.ActiveAppVersionId, T2.PublishedActiveAppVersionId, T1.SupplierGenId FROM (Trn_SupplierGen T1 LEFT JOIN Trn_Location T2 ON T2.LocationId = T1.SG_LocationSupplierLocationId AND T2.OrganisationId = T1.SG_LocationSupplierOrganisatio) ORDER BY T1.SupplierGenId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00G22,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00G23", "SELECT OrganisationId FROM Trn_Organisation WHERE OrganisationId = :SG_LocationSupplierOrganisatio ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00G23,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00G24", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :ActiveAppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00G24,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00G25", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :PublishedActiveAppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00G25,1, GxCacheFrequency.OFF ,true,false )
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
                ((bool[]) buf[1])[0] = rslt.wasNull(1);
                ((Guid[]) buf[2])[0] = rslt.getGuid(2);
                ((bool[]) buf[3])[0] = rslt.wasNull(2);
                ((Guid[]) buf[4])[0] = rslt.getGuid(3);
                ((bool[]) buf[5])[0] = rslt.wasNull(3);
                ((Guid[]) buf[6])[0] = rslt.getGuid(4);
                ((bool[]) buf[7])[0] = rslt.wasNull(4);
                ((Guid[]) buf[8])[0] = rslt.getGuid(5);
                return;
             case 1 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
             case 2 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
             case 3 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
       }
    }

 }

}
