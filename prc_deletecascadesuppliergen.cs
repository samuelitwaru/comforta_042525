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
   public class prc_deletecascadesuppliergen : GXProcedure
   {
      public prc_deletecascadesuppliergen( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_deletecascadesuppliergen( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_SupplierGenId ,
                           Guid aP1_OrganisationId )
      {
         this.AV9SupplierGenId = aP0_SupplierGenId;
         this.AV8OrganisationId = aP1_OrganisationId;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( Guid aP0_SupplierGenId ,
                                 Guid aP1_OrganisationId )
      {
         this.AV9SupplierGenId = aP0_SupplierGenId;
         this.AV8OrganisationId = aP1_OrganisationId;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         pr_default.dynParam(0, new Object[]{ new Object[]{
                                              AV9SupplierGenId ,
                                              AV8OrganisationId ,
                                              A42SupplierGenId ,
                                              A11OrganisationId } ,
                                              new int[]{
                                              TypeConstants.BOOLEAN
                                              }
         });
         /* Using cursor P00C12 */
         pr_default.execute(0, new Object[] {AV9SupplierGenId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A602SG_LocationSupplierOrganisatio = P00C12_A602SG_LocationSupplierOrganisatio[0];
            n602SG_LocationSupplierOrganisatio = P00C12_n602SG_LocationSupplierOrganisatio[0];
            A601SG_LocationSupplierLocationId = P00C12_A601SG_LocationSupplierLocationId[0];
            n601SG_LocationSupplierLocationId = P00C12_n601SG_LocationSupplierLocationId[0];
            A584ActiveAppVersionId = P00C12_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P00C12_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = P00C12_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P00C12_n598PublishedActiveAppVersionId[0];
            A42SupplierGenId = P00C12_A42SupplierGenId[0];
            A584ActiveAppVersionId = P00C12_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P00C12_n584ActiveAppVersionId[0];
            A598PublishedActiveAppVersionId = P00C12_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P00C12_n598PublishedActiveAppVersionId[0];
            /* Using cursor P00C13 */
            pr_default.execute(1, new Object[] {n602SG_LocationSupplierOrganisatio, A602SG_LocationSupplierOrganisatio});
            pr_default.close(1);
            /* Using cursor P00C14 */
            pr_default.execute(2, new Object[] {n584ActiveAppVersionId, A584ActiveAppVersionId});
            pr_default.close(2);
            /* Using cursor P00C15 */
            pr_default.execute(3, new Object[] {n598PublishedActiveAppVersionId, A598PublishedActiveAppVersionId});
            pr_default.close(3);
            new prc_deletecascadeproductservice(context ).execute(  Guid.Empty,  A42SupplierGenId,  Guid.Empty,  A11OrganisationId) ;
            /* Using cursor P00C16 */
            pr_default.execute(4, new Object[] {A42SupplierGenId});
            pr_default.close(4);
            pr_default.SmartCacheProvider.SetUpdated("Trn_SupplierGen");
            pr_default.readNext(0);
         }
         pr_default.close(0);
         pr_default.close(1);
         pr_default.close(3);
         cleanup();
      }

      public override void cleanup( )
      {
         context.CommitDataStores("prc_deletecascadesuppliergen",pr_default);
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
         A42SupplierGenId = Guid.Empty;
         A11OrganisationId = Guid.Empty;
         P00C12_A602SG_LocationSupplierOrganisatio = new Guid[] {Guid.Empty} ;
         P00C12_n602SG_LocationSupplierOrganisatio = new bool[] {false} ;
         P00C12_A601SG_LocationSupplierLocationId = new Guid[] {Guid.Empty} ;
         P00C12_n601SG_LocationSupplierLocationId = new bool[] {false} ;
         P00C12_A584ActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P00C12_n584ActiveAppVersionId = new bool[] {false} ;
         P00C12_A598PublishedActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P00C12_n598PublishedActiveAppVersionId = new bool[] {false} ;
         P00C12_A42SupplierGenId = new Guid[] {Guid.Empty} ;
         A602SG_LocationSupplierOrganisatio = Guid.Empty;
         A601SG_LocationSupplierLocationId = Guid.Empty;
         A584ActiveAppVersionId = Guid.Empty;
         A598PublishedActiveAppVersionId = Guid.Empty;
         P00C13_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00C13_n11OrganisationId = new bool[] {false} ;
         P00C14_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00C15_A523AppVersionId = new Guid[] {Guid.Empty} ;
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_deletecascadesuppliergen__default(),
            new Object[][] {
                new Object[] {
               P00C12_A602SG_LocationSupplierOrganisatio, P00C12_n602SG_LocationSupplierOrganisatio, P00C12_A601SG_LocationSupplierLocationId, P00C12_n601SG_LocationSupplierLocationId, P00C12_A584ActiveAppVersionId, P00C12_n584ActiveAppVersionId, P00C12_A598PublishedActiveAppVersionId, P00C12_n598PublishedActiveAppVersionId, P00C12_A42SupplierGenId
               }
               , new Object[] {
               P00C13_A11OrganisationId
               }
               , new Object[] {
               P00C14_A523AppVersionId
               }
               , new Object[] {
               P00C15_A523AppVersionId
               }
               , new Object[] {
               }
            }
         );
         /* GeneXus formulas. */
      }

      private bool n602SG_LocationSupplierOrganisatio ;
      private bool n601SG_LocationSupplierLocationId ;
      private bool n584ActiveAppVersionId ;
      private bool n598PublishedActiveAppVersionId ;
      private Guid AV9SupplierGenId ;
      private Guid AV8OrganisationId ;
      private Guid A42SupplierGenId ;
      private Guid A11OrganisationId ;
      private Guid A602SG_LocationSupplierOrganisatio ;
      private Guid A601SG_LocationSupplierLocationId ;
      private Guid A584ActiveAppVersionId ;
      private Guid A598PublishedActiveAppVersionId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00C12_A602SG_LocationSupplierOrganisatio ;
      private bool[] P00C12_n602SG_LocationSupplierOrganisatio ;
      private Guid[] P00C12_A601SG_LocationSupplierLocationId ;
      private bool[] P00C12_n601SG_LocationSupplierLocationId ;
      private Guid[] P00C12_A584ActiveAppVersionId ;
      private bool[] P00C12_n584ActiveAppVersionId ;
      private Guid[] P00C12_A598PublishedActiveAppVersionId ;
      private bool[] P00C12_n598PublishedActiveAppVersionId ;
      private Guid[] P00C12_A42SupplierGenId ;
      private Guid[] P00C13_A11OrganisationId ;
      private bool[] P00C13_n11OrganisationId ;
      private Guid[] P00C14_A523AppVersionId ;
      private Guid[] P00C15_A523AppVersionId ;
   }

   public class prc_deletecascadesuppliergen__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P00C12( IGxContext context ,
                                             Guid AV9SupplierGenId ,
                                             Guid AV8OrganisationId ,
                                             Guid A42SupplierGenId ,
                                             Guid A11OrganisationId )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int1 = new short[1];
         Object[] GXv_Object2 = new Object[2];
         scmdbuf = "SELECT T1.SG_LocationSupplierOrganisatio AS SG_LocationSupplierOrganisatio, T1.SG_LocationSupplierLocationId AS SG_LocationSupplierLocationId, T2.ActiveAppVersionId, T2.PublishedActiveAppVersionId, T1.SupplierGenId FROM (Trn_SupplierGen T1 LEFT JOIN Trn_Location T2 ON T2.LocationId = T1.SG_LocationSupplierLocationId AND T2.OrganisationId = T1.SG_LocationSupplierOrganisatio)";
         if ( ! (Guid.Empty==AV9SupplierGenId) )
         {
            AddWhere(sWhereString, "(T1.SupplierGenId = :AV9SupplierGenId)");
         }
         else
         {
            GXv_int1[0] = 1;
         }
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY T1.SupplierGenId";
         scmdbuf += " FOR UPDATE OF T1, T1";
         GXv_Object2[0] = scmdbuf;
         GXv_Object2[1] = GXv_int1;
         return GXv_Object2 ;
      }

      public override Object [] getDynamicStatement( int cursor ,
                                                     IGxContext context ,
                                                     Object [] dynConstraints )
      {
         switch ( cursor )
         {
               case 0 :
                     return conditional_P00C12(context, (Guid)dynConstraints[0] , (Guid)dynConstraints[1] , (Guid)dynConstraints[2] , (Guid)dynConstraints[3] );
         }
         return base.getDynamicStatement(cursor, context, dynConstraints);
      }

      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
         ,new ForEachCursor(def[1])
         ,new ForEachCursor(def[2])
         ,new ForEachCursor(def[3])
         ,new UpdateCursor(def[4])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP00C13;
          prmP00C13 = new Object[] {
          new ParDef("SG_LocationSupplierOrganisatio",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP00C14;
          prmP00C14 = new Object[] {
          new ParDef("ActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP00C15;
          prmP00C15 = new Object[] {
          new ParDef("PublishedActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP00C16;
          prmP00C16 = new Object[] {
          new ParDef("SupplierGenId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00C12;
          prmP00C12 = new Object[] {
          new ParDef("AV9SupplierGenId",GXType.UniqueIdentifier,36,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00C12", "scmdbuf",true, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00C12,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00C13", "SELECT OrganisationId FROM Trn_Organisation WHERE OrganisationId = :SG_LocationSupplierOrganisatio ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00C13,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00C14", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :ActiveAppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00C14,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00C15", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :PublishedActiveAppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00C15,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00C16", "SAVEPOINT gxupdate;DELETE FROM Trn_SupplierGen  WHERE SupplierGenId = :SupplierGenId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK,prmP00C16)
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
