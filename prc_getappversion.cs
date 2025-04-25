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
   public class prc_getappversion : GXProcedure
   {
      public prc_getappversion( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_getappversion( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( out SdtSDT_AppVersion aP0_SDT_AppVersion ,
                           out SdtSDT_Error aP1_SDT_Error )
      {
         this.AV13SDT_AppVersion = new SdtSDT_AppVersion(context) ;
         this.AV8SDT_Error = new SdtSDT_Error(context) ;
         initialize();
         ExecuteImpl();
         aP0_SDT_AppVersion=this.AV13SDT_AppVersion;
         aP1_SDT_Error=this.AV8SDT_Error;
      }

      public SdtSDT_Error executeUdp( out SdtSDT_AppVersion aP0_SDT_AppVersion )
      {
         execute(out aP0_SDT_AppVersion, out aP1_SDT_Error);
         return AV8SDT_Error ;
      }

      public void executeSubmit( out SdtSDT_AppVersion aP0_SDT_AppVersion ,
                                 out SdtSDT_Error aP1_SDT_Error )
      {
         this.AV13SDT_AppVersion = new SdtSDT_AppVersion(context) ;
         this.AV8SDT_Error = new SdtSDT_Error(context) ;
         SubmitImpl();
         aP0_SDT_AppVersion=this.AV13SDT_AppVersion;
         aP1_SDT_Error=this.AV8SDT_Error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( ! new prc_isauthenticated(context).executeUdp( ) )
         {
            AV8SDT_Error.gxTpr_Status = "Error";
            AV8SDT_Error.gxTpr_Message = "Not Authenticated";
            cleanup();
            if (true) return;
         }
         GXt_guid1 = AV9LocationId;
         new prc_getuserlocationid(context ).execute( out  GXt_guid1) ;
         AV9LocationId = GXt_guid1;
         /* Using cursor P00E72 */
         pr_default.execute(0, new Object[] {AV9LocationId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A523AppVersionId = P00E72_A523AppVersionId[0];
            A584ActiveAppVersionId = P00E72_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P00E72_n584ActiveAppVersionId[0];
            A29LocationId = P00E72_A29LocationId[0];
            A11OrganisationId = P00E72_A11OrganisationId[0];
            AV12BC_Trn_AppVersion.Load(A584ActiveAppVersionId);
            /* Using cursor P00E73 */
            pr_default.execute(1, new Object[] {n584ActiveAppVersionId, A584ActiveAppVersionId});
            while ( (pr_default.getStatus(1) != 101) )
            {
               A523AppVersionId = P00E73_A523AppVersionId[0];
               AV17GXLvl15 = 0;
               /* Using cursor P00E74 */
               pr_default.execute(2, new Object[] {A523AppVersionId});
               while ( (pr_default.getStatus(2) != 101) )
               {
                  A517PageName = P00E74_A517PageName[0];
                  A516PageId = P00E74_A516PageId[0];
                  AV17GXLvl15 = 1;
                  pr_default.readNext(2);
               }
               pr_default.close(2);
               if ( AV17GXLvl15 == 0 )
               {
                  GXt_SdtTrn_AppVersion_Page2 = AV14BC_HomePage;
                  new prc_inithomepage(context ).execute(  AV12BC_Trn_AppVersion.gxTpr_Appversionid, out  GXt_SdtTrn_AppVersion_Page2) ;
                  AV14BC_HomePage = GXt_SdtTrn_AppVersion_Page2;
                  AV12BC_Trn_AppVersion.gxTpr_Page.Add(AV14BC_HomePage, 0);
               }
               /* Exiting from a For First loop. */
               if (true) break;
            }
            pr_default.close(1);
            new prc_loadappversionsdt(context ).execute(  AV12BC_Trn_AppVersion, out  AV13SDT_AppVersion) ;
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
         AV13SDT_AppVersion = new SdtSDT_AppVersion(context);
         AV8SDT_Error = new SdtSDT_Error(context);
         AV9LocationId = Guid.Empty;
         GXt_guid1 = Guid.Empty;
         P00E72_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00E72_A584ActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P00E72_n584ActiveAppVersionId = new bool[] {false} ;
         P00E72_A29LocationId = new Guid[] {Guid.Empty} ;
         P00E72_A11OrganisationId = new Guid[] {Guid.Empty} ;
         A523AppVersionId = Guid.Empty;
         A584ActiveAppVersionId = Guid.Empty;
         A29LocationId = Guid.Empty;
         A11OrganisationId = Guid.Empty;
         AV12BC_Trn_AppVersion = new SdtTrn_AppVersion(context);
         P00E73_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00E74_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00E74_A517PageName = new string[] {""} ;
         P00E74_A516PageId = new Guid[] {Guid.Empty} ;
         A517PageName = "";
         A516PageId = Guid.Empty;
         AV14BC_HomePage = new SdtTrn_AppVersion_Page(context);
         GXt_SdtTrn_AppVersion_Page2 = new SdtTrn_AppVersion_Page(context);
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_getappversion__default(),
            new Object[][] {
                new Object[] {
               P00E72_A523AppVersionId, P00E72_A584ActiveAppVersionId, P00E72_n584ActiveAppVersionId, P00E72_A29LocationId, P00E72_A11OrganisationId
               }
               , new Object[] {
               P00E73_A523AppVersionId
               }
               , new Object[] {
               P00E74_A523AppVersionId, P00E74_A517PageName, P00E74_A516PageId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV17GXLvl15 ;
      private bool n584ActiveAppVersionId ;
      private string A517PageName ;
      private Guid AV9LocationId ;
      private Guid GXt_guid1 ;
      private Guid A523AppVersionId ;
      private Guid A584ActiveAppVersionId ;
      private Guid A29LocationId ;
      private Guid A11OrganisationId ;
      private Guid A516PageId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private SdtSDT_AppVersion AV13SDT_AppVersion ;
      private SdtSDT_Error AV8SDT_Error ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00E72_A523AppVersionId ;
      private Guid[] P00E72_A584ActiveAppVersionId ;
      private bool[] P00E72_n584ActiveAppVersionId ;
      private Guid[] P00E72_A29LocationId ;
      private Guid[] P00E72_A11OrganisationId ;
      private SdtTrn_AppVersion AV12BC_Trn_AppVersion ;
      private Guid[] P00E73_A523AppVersionId ;
      private Guid[] P00E74_A523AppVersionId ;
      private string[] P00E74_A517PageName ;
      private Guid[] P00E74_A516PageId ;
      private SdtTrn_AppVersion_Page AV14BC_HomePage ;
      private SdtTrn_AppVersion_Page GXt_SdtTrn_AppVersion_Page2 ;
      private SdtSDT_AppVersion aP0_SDT_AppVersion ;
      private SdtSDT_Error aP1_SDT_Error ;
   }

   public class prc_getappversion__default : DataStoreHelperBase, IDataStoreHelper
   {
      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
         ,new ForEachCursor(def[1])
         ,new ForEachCursor(def[2])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP00E72;
          prmP00E72 = new Object[] {
          new ParDef("AV9LocationId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00E73;
          prmP00E73 = new Object[] {
          new ParDef("ActiveAppVersionId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP00E74;
          prmP00E74 = new Object[] {
          new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00E72", "SELECT T2.AppVersionId, T1.ActiveAppVersionId, T1.LocationId, T1.OrganisationId FROM (Trn_Location T1 LEFT JOIN Trn_AppVersion T2 ON T2.AppVersionId = T1.ActiveAppVersionId) WHERE T1.LocationId = :AV9LocationId ORDER BY T1.LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00E72,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00E73", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :ActiveAppVersionId ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00E73,1, GxCacheFrequency.OFF ,true,true )
             ,new CursorDef("P00E74", "SELECT AppVersionId, PageName, PageId FROM Trn_AppVersionPage WHERE (AppVersionId = :AppVersionId) AND (PageName = ( 'Home')) ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00E74,100, GxCacheFrequency.OFF ,false,false )
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
                ((Guid[]) buf[3])[0] = rslt.getGuid(3);
                ((Guid[]) buf[4])[0] = rslt.getGuid(4);
                return;
             case 1 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
             case 2 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((string[]) buf[1])[0] = rslt.getVarchar(2);
                ((Guid[]) buf[2])[0] = rslt.getGuid(3);
                return;
       }
    }

 }

}
