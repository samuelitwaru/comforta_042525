using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using GeneXus.Reorg;
using System.Threading;
using GeneXus.Programs;
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
using System.Xml.Serialization;
namespace GeneXus.Programs {
   public class trn_locationdynamicformconversion : GXProcedure
   {
      public trn_locationdynamicformconversion( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", false);
      }

      public trn_locationdynamicformconversion( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( )
      {
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( )
      {
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Using cursor TRN_LOCATI2 */
         pr_default.execute(0);
         while ( (pr_default.getStatus(0) != 101) )
         {
            A619FormPageName = TRN_LOCATI2_A619FormPageName[0];
            n619FormPageName = TRN_LOCATI2_n619FormPageName[0];
            A207WWPFormVersionNumber = TRN_LOCATI2_A207WWPFormVersionNumber[0];
            A206WWPFormId = TRN_LOCATI2_A206WWPFormId[0];
            A29LocationId = TRN_LOCATI2_A29LocationId[0];
            A11OrganisationId = TRN_LOCATI2_A11OrganisationId[0];
            A366LocationDynamicFormId = TRN_LOCATI2_A366LocationDynamicFormId[0];
            /*
               INSERT RECORD ON TABLE GXA0070

            */
            AV2LocationDynamicFormId = A366LocationDynamicFormId;
            AV3OrganisationId = A11OrganisationId;
            AV4LocationId = A29LocationId;
            AV5WWPFormId = A206WWPFormId;
            AV6WWPFormVersionNumber = A207WWPFormVersionNumber;
            if ( TRN_LOCATI2_n619FormPageName[0] )
            {
               AV7FormPageName = " ";
            }
            else
            {
               AV7FormPageName = A619FormPageName;
            }
            /* Using cursor TRN_LOCATI3 */
            pr_default.execute(1, new Object[] {AV2LocationDynamicFormId, AV3OrganisationId, AV4LocationId, AV5WWPFormId, AV6WWPFormVersionNumber, AV7FormPageName});
            pr_default.close(1);
            pr_default.SmartCacheProvider.SetUpdated("GXA0070");
            if ( (pr_default.getStatus(1) == 1) )
            {
               context.Gx_err = 1;
               Gx_emsg = (string)(GXResourceManager.GetMessage("GXM_noupdate"));
            }
            else
            {
               context.Gx_err = 0;
               Gx_emsg = "";
            }
            /* End Insert */
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
         TRN_LOCATI2_A619FormPageName = new string[] {""} ;
         TRN_LOCATI2_n619FormPageName = new bool[] {false} ;
         TRN_LOCATI2_A207WWPFormVersionNumber = new short[1] ;
         TRN_LOCATI2_A206WWPFormId = new short[1] ;
         TRN_LOCATI2_A29LocationId = new Guid[] {Guid.Empty} ;
         TRN_LOCATI2_A11OrganisationId = new Guid[] {Guid.Empty} ;
         TRN_LOCATI2_A366LocationDynamicFormId = new Guid[] {Guid.Empty} ;
         A619FormPageName = "";
         A29LocationId = Guid.Empty;
         A11OrganisationId = Guid.Empty;
         A366LocationDynamicFormId = Guid.Empty;
         AV2LocationDynamicFormId = Guid.Empty;
         AV3OrganisationId = Guid.Empty;
         AV4LocationId = Guid.Empty;
         AV7FormPageName = "";
         Gx_emsg = "";
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.trn_locationdynamicformconversion__default(),
            new Object[][] {
                new Object[] {
               TRN_LOCATI2_A619FormPageName, TRN_LOCATI2_n619FormPageName, TRN_LOCATI2_A207WWPFormVersionNumber, TRN_LOCATI2_A206WWPFormId, TRN_LOCATI2_A29LocationId, TRN_LOCATI2_A11OrganisationId, TRN_LOCATI2_A366LocationDynamicFormId
               }
               , new Object[] {
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short A207WWPFormVersionNumber ;
      private short A206WWPFormId ;
      private short AV5WWPFormId ;
      private short AV6WWPFormVersionNumber ;
      private int GIGXA0070 ;
      private string Gx_emsg ;
      private bool n619FormPageName ;
      private string A619FormPageName ;
      private string AV7FormPageName ;
      private Guid A29LocationId ;
      private Guid A11OrganisationId ;
      private Guid A366LocationDynamicFormId ;
      private Guid AV2LocationDynamicFormId ;
      private Guid AV3OrganisationId ;
      private Guid AV4LocationId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private IDataStoreProvider pr_default ;
      private string[] TRN_LOCATI2_A619FormPageName ;
      private bool[] TRN_LOCATI2_n619FormPageName ;
      private short[] TRN_LOCATI2_A207WWPFormVersionNumber ;
      private short[] TRN_LOCATI2_A206WWPFormId ;
      private Guid[] TRN_LOCATI2_A29LocationId ;
      private Guid[] TRN_LOCATI2_A11OrganisationId ;
      private Guid[] TRN_LOCATI2_A366LocationDynamicFormId ;
   }

   public class trn_locationdynamicformconversion__default : DataStoreHelperBase, IDataStoreHelper
   {
      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
         ,new UpdateCursor(def[1])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmTRN_LOCATI2;
          prmTRN_LOCATI2 = new Object[] {
          };
          Object[] prmTRN_LOCATI3;
          prmTRN_LOCATI3 = new Object[] {
          new ParDef("AV2LocationDynamicFormId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV3OrganisationId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV4LocationId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV5WWPFormId",GXType.Int16,4,0) ,
          new ParDef("AV6WWPFormVersionNumber",GXType.Int16,4,0) ,
          new ParDef("AV7FormPageName",GXType.VarChar,100,0)
          };
          def= new CursorDef[] {
              new CursorDef("TRN_LOCATI2", "SELECT FormPageName, WWPFormVersionNumber, WWPFormId, LocationId, OrganisationId, LocationDynamicFormId FROM Trn_LocationDynamicForm ORDER BY LocationDynamicFormId, OrganisationId, LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmTRN_LOCATI2,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("TRN_LOCATI3", "INSERT INTO GXA0070(LocationDynamicFormId, OrganisationId, LocationId, WWPFormId, WWPFormVersionNumber, FormPageName) VALUES(:AV2LocationDynamicFormId, :AV3OrganisationId, :AV4LocationId, :AV5WWPFormId, :AV6WWPFormVersionNumber, :AV7FormPageName)", GxErrorMask.GX_NOMASK,prmTRN_LOCATI3)
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
                ((string[]) buf[0])[0] = rslt.getVarchar(1);
                ((bool[]) buf[1])[0] = rslt.wasNull(1);
                ((short[]) buf[2])[0] = rslt.getShort(2);
                ((short[]) buf[3])[0] = rslt.getShort(3);
                ((Guid[]) buf[4])[0] = rslt.getGuid(4);
                ((Guid[]) buf[5])[0] = rslt.getGuid(5);
                ((Guid[]) buf[6])[0] = rslt.getGuid(6);
                return;
       }
    }

 }

}
