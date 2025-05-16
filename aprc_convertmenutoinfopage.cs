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
using GeneXus.Http.Server;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace GeneXus.Programs {
   public class aprc_convertmenutoinfopage : GXWebProcedure
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

      public aprc_convertmenutoinfopage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public aprc_convertmenutoinfopage( IGxContext context )
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
         /* Using cursor P00GB2 */
         pr_default.execute(0);
         while ( (pr_default.getStatus(0) != 101) )
         {
            A523AppVersionId = P00GB2_A523AppVersionId[0];
            A524AppVersionName = P00GB2_A524AppVersionName[0];
            new prc_logtoserver(context ).execute(  context.GetMessage( "Version: ", "")+A524AppVersionName) ;
            /* Using cursor P00GB3 */
            pr_default.execute(1, new Object[] {A523AppVersionId});
            while ( (pr_default.getStatus(1) != 101) )
            {
               A525PageType = P00GB3_A525PageType[0];
               A517PageName = P00GB3_A517PageName[0];
               A518PageStructure = P00GB3_A518PageStructure[0];
               A516PageId = P00GB3_A516PageId[0];
               new prc_logtoserver(context ).execute(  ">>>>>>>>>>>> "+A517PageName) ;
               A525PageType = "Information";
               AV13SDT_InfoContent = new SdtSDT_InfoContent(context);
               AV8SDT_MenuPage.FromJSonString(A518PageStructure, null);
               AV20GXV1 = 1;
               while ( AV20GXV1 <= AV8SDT_MenuPage.gxTpr_Rows.Count )
               {
                  AV9RowItem = ((SdtSDT_MenuPage_RowsItem)AV8SDT_MenuPage.gxTpr_Rows.Item(AV20GXV1));
                  AV14InfoContentItem = new SdtSDT_InfoContent_InfoContentItem(context);
                  AV14InfoContentItem.gxTpr_Infoid = new SdtRandomStringGenerator(context).generate(15);
                  AV14InfoContentItem.gxTpr_Infotype = "TileRow";
                  AV11SDT_InfoTile = new GXBaseCollection<SdtSDT_InfoTile_SDT_InfoTileItem>( context, "SDT_InfoTileItem", "Comforta_version20");
                  AV21GXV2 = 1;
                  while ( AV21GXV2 <= AV9RowItem.gxTpr_Tiles.Count )
                  {
                     AV10TileItem = ((SdtSDT_MenuPage_RowsItem_TilesItem)AV9RowItem.gxTpr_Tiles.Item(AV21GXV2));
                     AV17SDT_InfoTileItem = new SdtSDT_InfoTile_SDT_InfoTileItem(context);
                     AV17SDT_InfoTileItem.FromJSonString(AV10TileItem.ToJSonString(false, true), null);
                     AV11SDT_InfoTile.Add(AV17SDT_InfoTileItem, 0);
                     AV21GXV2 = (int)(AV21GXV2+1);
                  }
                  AV14InfoContentItem.gxTpr_Tiles = AV11SDT_InfoTile;
                  AV13SDT_InfoContent.gxTpr_Infocontent.Add(AV14InfoContentItem, 0);
                  AV20GXV1 = (int)(AV20GXV1+1);
               }
               new prc_logtoserver(context ).execute(  "		"+AV13SDT_InfoContent.ToJSonString(false, true)) ;
               AV16HttpResponse.AddString("		"+AV13SDT_InfoContent.ToJSonString(false, true)+StringUtil.NewLine( ));
               A518PageStructure = AV13SDT_InfoContent.ToJSonString(false, true);
               /* Using cursor P00GB4 */
               pr_default.execute(2, new Object[] {A525PageType, A518PageStructure, A523AppVersionId, A516PageId});
               pr_default.close(2);
               pr_default.SmartCacheProvider.SetUpdated("Trn_AppVersionPage");
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
         context.CommitDataStores("prc_convertmenutoinfopage",pr_default);
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
         P00GB2_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00GB2_A524AppVersionName = new string[] {""} ;
         A523AppVersionId = Guid.Empty;
         A524AppVersionName = "";
         P00GB3_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00GB3_A525PageType = new string[] {""} ;
         P00GB3_A517PageName = new string[] {""} ;
         P00GB3_A518PageStructure = new string[] {""} ;
         P00GB3_A516PageId = new Guid[] {Guid.Empty} ;
         A525PageType = "";
         A517PageName = "";
         A518PageStructure = "";
         A516PageId = Guid.Empty;
         AV13SDT_InfoContent = new SdtSDT_InfoContent(context);
         AV8SDT_MenuPage = new SdtSDT_MenuPage(context);
         AV9RowItem = new SdtSDT_MenuPage_RowsItem(context);
         AV14InfoContentItem = new SdtSDT_InfoContent_InfoContentItem(context);
         AV11SDT_InfoTile = new GXBaseCollection<SdtSDT_InfoTile_SDT_InfoTileItem>( context, "SDT_InfoTileItem", "Comforta_version20");
         AV10TileItem = new SdtSDT_MenuPage_RowsItem_TilesItem(context);
         AV17SDT_InfoTileItem = new SdtSDT_InfoTile_SDT_InfoTileItem(context);
         AV16HttpResponse = new GxHttpResponse( context);
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.aprc_convertmenutoinfopage__default(),
            new Object[][] {
                new Object[] {
               P00GB2_A523AppVersionId, P00GB2_A524AppVersionName
               }
               , new Object[] {
               P00GB3_A523AppVersionId, P00GB3_A525PageType, P00GB3_A517PageName, P00GB3_A518PageStructure, P00GB3_A516PageId
               }
               , new Object[] {
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short nGotPars ;
      private short GxWebError ;
      private int AV20GXV1 ;
      private int AV21GXV2 ;
      private string GXKey ;
      private string gxfirstwebparm ;
      private bool entryPointCalled ;
      private string A518PageStructure ;
      private string A524AppVersionName ;
      private string A525PageType ;
      private string A517PageName ;
      private Guid A523AppVersionId ;
      private Guid A516PageId ;
      private GxHttpResponse AV16HttpResponse ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00GB2_A523AppVersionId ;
      private string[] P00GB2_A524AppVersionName ;
      private Guid[] P00GB3_A523AppVersionId ;
      private string[] P00GB3_A525PageType ;
      private string[] P00GB3_A517PageName ;
      private string[] P00GB3_A518PageStructure ;
      private Guid[] P00GB3_A516PageId ;
      private SdtSDT_InfoContent AV13SDT_InfoContent ;
      private SdtSDT_MenuPage AV8SDT_MenuPage ;
      private SdtSDT_MenuPage_RowsItem AV9RowItem ;
      private SdtSDT_InfoContent_InfoContentItem AV14InfoContentItem ;
      private GXBaseCollection<SdtSDT_InfoTile_SDT_InfoTileItem> AV11SDT_InfoTile ;
      private SdtSDT_MenuPage_RowsItem_TilesItem AV10TileItem ;
      private SdtSDT_InfoTile_SDT_InfoTileItem AV17SDT_InfoTileItem ;
   }

   public class aprc_convertmenutoinfopage__default : DataStoreHelperBase, IDataStoreHelper
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
          Object[] prmP00GB2;
          prmP00GB2 = new Object[] {
          };
          Object[] prmP00GB3;
          prmP00GB3 = new Object[] {
          new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00GB4;
          prmP00GB4 = new Object[] {
          new ParDef("PageType",GXType.VarChar,40,0) ,
          new ParDef("PageStructure",GXType.LongVarChar,2097152,0) ,
          new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("PageId",GXType.UniqueIdentifier,36,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00GB2", "SELECT AppVersionId, AppVersionName FROM Trn_AppVersion ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00GB2,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00GB3", "SELECT AppVersionId, PageType, PageName, PageStructure, PageId FROM Trn_AppVersionPage WHERE (AppVersionId = :AppVersionId) AND (PageType = ( 'MyLiving') or PageType = ( 'MyCare') or PageType = ( 'MyService')) ORDER BY AppVersionId  FOR UPDATE OF Trn_AppVersionPage",true, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00GB3,1, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00GB4", "SAVEPOINT gxupdate;UPDATE Trn_AppVersionPage SET PageType=:PageType, PageStructure=:PageStructure  WHERE AppVersionId = :AppVersionId AND PageId = :PageId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK,prmP00GB4)
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
                ((string[]) buf[1])[0] = rslt.getVarchar(2);
                return;
             case 1 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((string[]) buf[1])[0] = rslt.getVarchar(2);
                ((string[]) buf[2])[0] = rslt.getVarchar(3);
                ((string[]) buf[3])[0] = rslt.getLongVarchar(4);
                ((Guid[]) buf[4])[0] = rslt.getGuid(5);
                return;
       }
    }

 }

}
