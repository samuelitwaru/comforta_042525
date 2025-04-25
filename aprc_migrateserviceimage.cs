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
   public class aprc_migrateserviceimage : GXProcedure
   {
      public static int Main( string[] args )
      {
         return new aprc_migrateserviceimage().MainImpl(args); ;
      }

      public int executeCmdLine( string[] args )
      {
         return ExecuteCmdLine(args); ;
      }

      protected override int ExecuteCmdLine( string[] args )
      {
         execute();
         return GX.GXRuntime.ExitCode ;
      }

      public aprc_migrateserviceimage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public aprc_migrateserviceimage( IGxContext context )
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
         /* Using cursor P00G02 */
         pr_default.execute(0);
         while ( (pr_default.getStatus(0) != 101) )
         {
            A40000ProductServiceImage_GXI = P00G02_A40000ProductServiceImage_GXI[0];
            A58ProductServiceId = P00G02_A58ProductServiceId[0];
            A29LocationId = P00G02_A29LocationId[0];
            A11OrganisationId = P00G02_A11OrganisationId[0];
            A61ProductServiceImage = P00G02_A61ProductServiceImage[0];
            /* Using cursor P00G03 */
            pr_default.execute(1, new Object[] {A58ProductServiceId});
            while ( (pr_default.getStatus(1) != 101) )
            {
               A609ServiceId = P00G03_A609ServiceId[0];
               A608ServiceImageId = P00G03_A608ServiceImageId[0];
               AV15isAlreadyAdded = true;
               /* Exit For each command. Update data (if necessary), close cursors & exit. */
               if (true) break;
               pr_default.readNext(1);
            }
            pr_default.close(1);
            if ( ! AV15isAlreadyAdded )
            {
               /*
                  INSERT RECORD ON TABLE Trn_ServiceImage

               */
               A609ServiceId = A58ProductServiceId;
               A611ServiceImage = A61ProductServiceImage;
               A40001ServiceImage_GXI = A40000ProductServiceImage_GXI;
               A608ServiceImageId = Guid.NewGuid( );
               /* Using cursor P00G04 */
               pr_default.execute(2, new Object[] {A608ServiceImageId, A609ServiceId, A611ServiceImage, A40001ServiceImage_GXI});
               pr_default.close(2);
               pr_default.SmartCacheProvider.SetUpdated("Trn_ServiceImage");
               if ( (pr_default.getStatus(2) == 1) )
               {
                  context.Gx_err = 1;
                  Gx_emsg = (string)(context.GetMessage( "GXM_noupdate", ""));
               }
               else
               {
                  context.Gx_err = 0;
                  Gx_emsg = "";
               }
               /* End Insert */
            }
            pr_default.readNext(0);
         }
         pr_default.close(0);
         /* Using cursor P00G05 */
         pr_default.execute(3);
         while ( (pr_default.getStatus(3) != 101) )
         {
            A40002LocationImage_GXI = P00G05_A40002LocationImage_GXI[0];
            A29LocationId = P00G05_A29LocationId[0];
            A11OrganisationId = P00G05_A11OrganisationId[0];
            A494LocationImage = P00G05_A494LocationImage[0];
            /* Using cursor P00G06 */
            pr_default.execute(4, new Object[] {A29LocationId});
            while ( (pr_default.getStatus(4) != 101) )
            {
               A614OrganisationLocationId = P00G06_A614OrganisationLocationId[0];
               A613LocationImageId = P00G06_A613LocationImageId[0];
               AV15isAlreadyAdded = true;
               /* Exit For each command. Update data (if necessary), close cursors & exit. */
               if (true) break;
               pr_default.readNext(4);
            }
            pr_default.close(4);
            if ( ! AV15isAlreadyAdded )
            {
               /*
                  INSERT RECORD ON TABLE Trn_LocationImage

               */
               A614OrganisationLocationId = A29LocationId;
               A615OrganisationLocationImage = A494LocationImage;
               A40003OrganisationLocationImage_GXI = A40002LocationImage_GXI;
               A613LocationImageId = Guid.NewGuid( );
               /* Using cursor P00G07 */
               pr_default.execute(5, new Object[] {A613LocationImageId, A614OrganisationLocationId, A615OrganisationLocationImage, A40003OrganisationLocationImage_GXI});
               pr_default.close(5);
               pr_default.SmartCacheProvider.SetUpdated("Trn_LocationImage");
               if ( (pr_default.getStatus(5) == 1) )
               {
                  context.Gx_err = 1;
                  Gx_emsg = (string)(context.GetMessage( "GXM_noupdate", ""));
               }
               else
               {
                  context.Gx_err = 0;
                  Gx_emsg = "";
               }
               /* End Insert */
            }
            pr_default.readNext(3);
         }
         pr_default.close(3);
         cleanup();
      }

      public override void cleanup( )
      {
         context.CommitDataStores("prc_migrateserviceimage",pr_default);
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
         ExitApp();
      }

      public override void initialize( )
      {
         P00G02_A40000ProductServiceImage_GXI = new string[] {""} ;
         P00G02_A58ProductServiceId = new Guid[] {Guid.Empty} ;
         P00G02_A29LocationId = new Guid[] {Guid.Empty} ;
         P00G02_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00G02_A61ProductServiceImage = new string[] {""} ;
         A40000ProductServiceImage_GXI = "";
         A58ProductServiceId = Guid.Empty;
         A29LocationId = Guid.Empty;
         A11OrganisationId = Guid.Empty;
         A61ProductServiceImage = "";
         P00G03_A609ServiceId = new Guid[] {Guid.Empty} ;
         P00G03_A608ServiceImageId = new Guid[] {Guid.Empty} ;
         A609ServiceId = Guid.Empty;
         A608ServiceImageId = Guid.Empty;
         A611ServiceImage = "";
         A40001ServiceImage_GXI = "";
         Gx_emsg = "";
         P00G05_A40002LocationImage_GXI = new string[] {""} ;
         P00G05_A29LocationId = new Guid[] {Guid.Empty} ;
         P00G05_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00G05_A494LocationImage = new string[] {""} ;
         A40002LocationImage_GXI = "";
         A494LocationImage = "";
         P00G06_A614OrganisationLocationId = new Guid[] {Guid.Empty} ;
         P00G06_A613LocationImageId = new Guid[] {Guid.Empty} ;
         A614OrganisationLocationId = Guid.Empty;
         A613LocationImageId = Guid.Empty;
         A615OrganisationLocationImage = "";
         A40003OrganisationLocationImage_GXI = "";
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.aprc_migrateserviceimage__default(),
            new Object[][] {
                new Object[] {
               P00G02_A40000ProductServiceImage_GXI, P00G02_A58ProductServiceId, P00G02_A29LocationId, P00G02_A11OrganisationId, P00G02_A61ProductServiceImage
               }
               , new Object[] {
               P00G03_A609ServiceId, P00G03_A608ServiceImageId
               }
               , new Object[] {
               }
               , new Object[] {
               P00G05_A40002LocationImage_GXI, P00G05_A29LocationId, P00G05_A11OrganisationId, P00G05_A494LocationImage
               }
               , new Object[] {
               P00G06_A614OrganisationLocationId, P00G06_A613LocationImageId
               }
               , new Object[] {
               }
            }
         );
         /* GeneXus formulas. */
      }

      private int GX_INS107 ;
      private int GX_INS108 ;
      private string Gx_emsg ;
      private bool AV15isAlreadyAdded ;
      private string A40000ProductServiceImage_GXI ;
      private string A40001ServiceImage_GXI ;
      private string A40002LocationImage_GXI ;
      private string A40003OrganisationLocationImage_GXI ;
      private string A61ProductServiceImage ;
      private string A611ServiceImage ;
      private string A494LocationImage ;
      private string A615OrganisationLocationImage ;
      private Guid A58ProductServiceId ;
      private Guid A29LocationId ;
      private Guid A11OrganisationId ;
      private Guid A609ServiceId ;
      private Guid A608ServiceImageId ;
      private Guid A614OrganisationLocationId ;
      private Guid A613LocationImageId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private IDataStoreProvider pr_default ;
      private string[] P00G02_A40000ProductServiceImage_GXI ;
      private Guid[] P00G02_A58ProductServiceId ;
      private Guid[] P00G02_A29LocationId ;
      private Guid[] P00G02_A11OrganisationId ;
      private string[] P00G02_A61ProductServiceImage ;
      private Guid[] P00G03_A609ServiceId ;
      private Guid[] P00G03_A608ServiceImageId ;
      private string[] P00G05_A40002LocationImage_GXI ;
      private Guid[] P00G05_A29LocationId ;
      private Guid[] P00G05_A11OrganisationId ;
      private string[] P00G05_A494LocationImage ;
      private Guid[] P00G06_A614OrganisationLocationId ;
      private Guid[] P00G06_A613LocationImageId ;
   }

   public class aprc_migrateserviceimage__default : DataStoreHelperBase, IDataStoreHelper
   {
      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
         ,new ForEachCursor(def[1])
         ,new UpdateCursor(def[2])
         ,new ForEachCursor(def[3])
         ,new ForEachCursor(def[4])
         ,new UpdateCursor(def[5])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP00G02;
          prmP00G02 = new Object[] {
          };
          Object[] prmP00G03;
          prmP00G03 = new Object[] {
          new ParDef("ProductServiceId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00G04;
          prmP00G04 = new Object[] {
          new ParDef("ServiceImageId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("ServiceId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("ServiceImage",GXType.Byte,1024,0){InDB=false} ,
          new ParDef("ServiceImage_GXI",GXType.VarChar,2048,0){AddAtt=true, ImgIdx=2, Tbl="Trn_ServiceImage", Fld="ServiceImage"}
          };
          Object[] prmP00G05;
          prmP00G05 = new Object[] {
          };
          Object[] prmP00G06;
          prmP00G06 = new Object[] {
          new ParDef("LocationId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00G07;
          prmP00G07 = new Object[] {
          new ParDef("LocationImageId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("OrganisationLocationId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("OrganisationLocationImage",GXType.Byte,1024,0){InDB=false} ,
          new ParDef("OrganisationLocationImage_GXI",GXType.VarChar,2048,0){AddAtt=true, ImgIdx=2, Tbl="Trn_LocationImage", Fld="OrganisationLocationImage"}
          };
          def= new CursorDef[] {
              new CursorDef("P00G02", "SELECT ProductServiceImage_GXI, ProductServiceId, LocationId, OrganisationId, ProductServiceImage FROM Trn_ProductService WHERE Not ( (ProductServiceImage = '') and (char_length(trim(trailing ' ' from ProductServiceImage_GXI))=0)) ORDER BY ProductServiceId, LocationId, OrganisationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00G02,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00G03", "SELECT ServiceId, ServiceImageId FROM Trn_ServiceImage WHERE ServiceId = :ProductServiceId ORDER BY ServiceId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00G03,1, GxCacheFrequency.OFF ,false,true )
             ,new CursorDef("P00G04", "SAVEPOINT gxupdate;INSERT INTO Trn_ServiceImage(ServiceImageId, ServiceId, ServiceImage, ServiceImage_GXI) VALUES(:ServiceImageId, :ServiceId, :ServiceImage, :ServiceImage_GXI);RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_MASKLOOPLOCK,prmP00G04)
             ,new CursorDef("P00G05", "SELECT LocationImage_GXI, LocationId, OrganisationId, LocationImage FROM Trn_Location WHERE Not ( (LocationImage = '') and (char_length(trim(trailing ' ' from LocationImage_GXI))=0)) ORDER BY LocationId, OrganisationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00G05,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00G06", "SELECT OrganisationLocationId, LocationImageId FROM Trn_LocationImage WHERE OrganisationLocationId = :LocationId ORDER BY LocationImageId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00G06,1, GxCacheFrequency.OFF ,false,true )
             ,new CursorDef("P00G07", "SAVEPOINT gxupdate;INSERT INTO Trn_LocationImage(LocationImageId, OrganisationLocationId, OrganisationLocationImage, OrganisationLocationImage_GXI) VALUES(:LocationImageId, :OrganisationLocationId, :OrganisationLocationImage, :OrganisationLocationImage_GXI);RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_MASKLOOPLOCK,prmP00G07)
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
                ((string[]) buf[0])[0] = rslt.getMultimediaUri(1);
                ((Guid[]) buf[1])[0] = rslt.getGuid(2);
                ((Guid[]) buf[2])[0] = rslt.getGuid(3);
                ((Guid[]) buf[3])[0] = rslt.getGuid(4);
                ((string[]) buf[4])[0] = rslt.getMultimediaFile(5, rslt.getVarchar(1));
                return;
             case 1 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((Guid[]) buf[1])[0] = rslt.getGuid(2);
                return;
             case 3 :
                ((string[]) buf[0])[0] = rslt.getMultimediaUri(1);
                ((Guid[]) buf[1])[0] = rslt.getGuid(2);
                ((Guid[]) buf[2])[0] = rslt.getGuid(3);
                ((string[]) buf[3])[0] = rslt.getMultimediaFile(4, rslt.getVarchar(1));
                return;
             case 4 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((Guid[]) buf[1])[0] = rslt.getGuid(2);
                return;
       }
    }

 }

}
