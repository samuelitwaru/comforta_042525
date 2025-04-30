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
   public class aprc_convertcontentpages : GXWebProcedure
   {
      public override void webExecute( )
      {
         context.SetDefaultTheme("WorkWithPlusDS", true);
         initialize();
         GXKey = Crypto.GetSiteKey( );
         if ( nGotPars == 0 )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetNextPar( );
         }
         if ( GxWebError == 0 )
         {
            ExecutePrivate();
         }
         cleanup();
      }

      public aprc_convertcontentpages( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public aprc_convertcontentpages( IGxContext context )
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
         /* Using cursor P00G92 */
         pr_default.execute(0);
         while ( (pr_default.getStatus(0) != 101) )
         {
            A523AppVersionId = P00G92_A523AppVersionId[0];
            A29LocationId = P00G92_A29LocationId[0];
            n29LocationId = P00G92_n29LocationId[0];
            AV8LocationId = A29LocationId;
            /* Using cursor P00G93 */
            pr_default.execute(1, new Object[] {A523AppVersionId});
            while ( (pr_default.getStatus(1) != 101) )
            {
               GXTG93 = 0;
               A525PageType = P00G93_A525PageType[0];
               A518PageStructure = P00G93_A518PageStructure[0];
               A536PagePublishedStructure = P00G93_A536PagePublishedStructure[0];
               A516PageId = P00G93_A516PageId[0];
               AV10SDT_InfoContent.FromJSonString(A518PageStructure, null);
               AV15GXV1 = 1;
               while ( AV15GXV1 <= AV10SDT_InfoContent.gxTpr_Infocontent.Count )
               {
                  AV12InfoContent = ((SdtSDT_InfoContent_InfoContentItem)AV10SDT_InfoContent.gxTpr_Infocontent.Item(AV15GXV1));
                  if ( StringUtil.StrCmp(AV12InfoContent.gxTpr_Infotype, context.GetMessage( "TileRow", "")) == 0 )
                  {
                     AV12InfoContent.gxTpr_Tiles.Clear();
                  }
                  AV15GXV1 = (int)(AV15GXV1+1);
               }
               A518PageStructure = AV10SDT_InfoContent.ToJSonString(false, true);
               A536PagePublishedStructure = AV10SDT_InfoContent.ToJSonString(false, true);
               new prc_logtoserver(context ).execute(  A518PageStructure) ;
               GXTG93 = 1;
               /* Using cursor P00G94 */
               pr_default.execute(2, new Object[] {A518PageStructure, A536PagePublishedStructure, A523AppVersionId, A516PageId});
               pr_default.close(2);
               pr_default.SmartCacheProvider.SetUpdated("Trn_AppVersionPage");
               if ( GXTG93 == 1 )
               {
                  context.CommitDataStores("prc_convertcontentpages",pr_default);
               }
               pr_default.readNext(1);
            }
            pr_default.close(1);
            pr_default.readNext(0);
         }
         pr_default.close(0);
         if ( context.WillRedirect( ) )
         {
            context.Redirect( context.wjLoc );
            context.wjLoc = "";
         }
         cleanup();
      }

      public override void cleanup( )
      {
         context.CommitDataStores("prc_convertcontentpages",pr_default);
         CloseCursors();
         base.cleanup();
         if ( IsMain )
         {
            context.CloseConnections();
         }
         ExitApp();
      }

      public override void initialize( )
      {
         GXKey = "";
         gxfirstwebparm = "";
         P00G92_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00G92_A29LocationId = new Guid[] {Guid.Empty} ;
         P00G92_n29LocationId = new bool[] {false} ;
         A523AppVersionId = Guid.Empty;
         A29LocationId = Guid.Empty;
         AV8LocationId = Guid.Empty;
         P00G93_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00G93_A525PageType = new string[] {""} ;
         P00G93_A518PageStructure = new string[] {""} ;
         P00G93_A536PagePublishedStructure = new string[] {""} ;
         P00G93_A516PageId = new Guid[] {Guid.Empty} ;
         A525PageType = "";
         A518PageStructure = "";
         A536PagePublishedStructure = "";
         A516PageId = Guid.Empty;
         AV10SDT_InfoContent = new SdtSDT_InfoContent(context);
         AV12InfoContent = new SdtSDT_InfoContent_InfoContentItem(context);
         pr_datastore1 = new DataStoreProvider(context, new GeneXus.Programs.aprc_convertcontentpages__datastore1(),
            new Object[][] {
            }
         );
         pr_gam = new DataStoreProvider(context, new GeneXus.Programs.aprc_convertcontentpages__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.aprc_convertcontentpages__default(),
            new Object[][] {
                new Object[] {
               P00G92_A523AppVersionId, P00G92_A29LocationId, P00G92_n29LocationId
               }
               , new Object[] {
               P00G93_A523AppVersionId, P00G93_A525PageType, P00G93_A518PageStructure, P00G93_A536PagePublishedStructure, P00G93_A516PageId
               }
               , new Object[] {
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short nGotPars ;
      private short GxWebError ;
      private short GXTG93 ;
      private int AV15GXV1 ;
      private string GXKey ;
      private string gxfirstwebparm ;
      private bool entryPointCalled ;
      private bool n29LocationId ;
      private string A518PageStructure ;
      private string A536PagePublishedStructure ;
      private string A525PageType ;
      private Guid A523AppVersionId ;
      private Guid A29LocationId ;
      private Guid AV8LocationId ;
      private Guid A516PageId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00G92_A523AppVersionId ;
      private Guid[] P00G92_A29LocationId ;
      private bool[] P00G92_n29LocationId ;
      private Guid[] P00G93_A523AppVersionId ;
      private string[] P00G93_A525PageType ;
      private string[] P00G93_A518PageStructure ;
      private string[] P00G93_A536PagePublishedStructure ;
      private Guid[] P00G93_A516PageId ;
      private SdtSDT_InfoContent AV10SDT_InfoContent ;
      private SdtSDT_InfoContent_InfoContentItem AV12InfoContent ;
      private IDataStoreProvider pr_datastore1 ;
      private IDataStoreProvider pr_gam ;
   }

   public class aprc_convertcontentpages__datastore1 : DataStoreHelperBase, IDataStoreHelper
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

 public class aprc_convertcontentpages__gam : DataStoreHelperBase, IDataStoreHelper
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

public class aprc_convertcontentpages__default : DataStoreHelperBase, IDataStoreHelper
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
       Object[] prmP00G92;
       prmP00G92 = new Object[] {
       };
       Object[] prmP00G93;
       prmP00G93 = new Object[] {
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmP00G94;
       prmP00G94 = new Object[] {
       new ParDef("PageStructure",GXType.LongVarChar,2097152,0) ,
       new ParDef("PagePublishedStructure",GXType.LongVarChar,2097152,0) ,
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("PageId",GXType.UniqueIdentifier,36,0)
       };
       def= new CursorDef[] {
           new CursorDef("P00G92", "SELECT AppVersionId, LocationId FROM Trn_AppVersion ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00G92,100, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("P00G93", "SELECT AppVersionId, PageType, PageStructure, PagePublishedStructure, PageId FROM Trn_AppVersionPage WHERE (AppVersionId = :AppVersionId) AND (PageType = ( 'Information')) ORDER BY AppVersionId  FOR UPDATE OF Trn_AppVersionPage",true, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00G93,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("P00G94", "SAVEPOINT gxupdate;UPDATE Trn_AppVersionPage SET PageStructure=:PageStructure, PagePublishedStructure=:PagePublishedStructure  WHERE AppVersionId = :AppVersionId AND PageId = :PageId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK,prmP00G94)
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
             return;
          case 1 :
             ((Guid[]) buf[0])[0] = rslt.getGuid(1);
             ((string[]) buf[1])[0] = rslt.getVarchar(2);
             ((string[]) buf[2])[0] = rslt.getLongVarchar(3);
             ((string[]) buf[3])[0] = rslt.getLongVarchar(4);
             ((Guid[]) buf[4])[0] = rslt.getGuid(5);
             return;
    }
 }

}

}
