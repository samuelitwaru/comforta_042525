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
      protected override bool IntegratedSecurityEnabled
      {
         get {
            return true ;
         }

      }

      protected override GAMSecurityLevel IntegratedSecurityLevel
      {
         get {
            return GAMSecurityLevel.SecurityHigh ;
         }

      }

      protected override string ExecutePermissionPrefix
      {
         get {
            return "prc_convertmenutoinfopage_Execute" ;
         }

      }

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
            /* Using cursor P00GB3 */
            pr_default.execute(1, new Object[] {A523AppVersionId});
            while ( (pr_default.getStatus(1) != 101) )
            {
               A525PageType = P00GB3_A525PageType[0];
               A518PageStructure = P00GB3_A518PageStructure[0];
               A516PageId = P00GB3_A516PageId[0];
               AV13SDT_InfoContent = new SdtSDT_InfoContent(context);
               AV8SDT_MenuPage.FromJSonString(A518PageStructure, null);
               AV19GXV1 = 1;
               while ( AV19GXV1 <= AV8SDT_MenuPage.gxTpr_Rows.Count )
               {
                  AV9RowItem = ((SdtSDT_MenuPage_RowsItem)AV8SDT_MenuPage.gxTpr_Rows.Item(AV19GXV1));
                  AV14InfoContentItem = new SdtSDT_InfoContent_InfoContentItem(context);
                  AV14InfoContentItem.gxTpr_Infoid = new SdtRandomStringGenerator(context).generate(15);
                  AV14InfoContentItem.gxTpr_Infovalue = "RowItem";
                  AV20GXV2 = 1;
                  while ( AV20GXV2 <= AV9RowItem.gxTpr_Tiles.Count )
                  {
                     AV10TileItem = ((SdtSDT_MenuPage_RowsItem_TilesItem)AV9RowItem.gxTpr_Tiles.Item(AV20GXV2));
                     AV11SDT_InfoTile = new GXBaseCollection<SdtSDT_InfoTile_SDT_InfoTileItem>( context, "SDT_InfoTileItem", "Comforta_version20");
                     AV20GXV2 = (int)(AV20GXV2+1);
                  }
                  AV19GXV1 = (int)(AV19GXV1+1);
               }
               new prc_logtofile(context ).execute(  AV13SDT_InfoContent.ToJSonString(false, true)) ;
               AV16HttpResponse.AddString(AV13SDT_InfoContent.ToJSonString(false, true)+StringUtil.NewLine( ));
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
         A523AppVersionId = Guid.Empty;
         P00GB3_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00GB3_A525PageType = new string[] {""} ;
         P00GB3_A518PageStructure = new string[] {""} ;
         P00GB3_A516PageId = new Guid[] {Guid.Empty} ;
         A525PageType = "";
         A518PageStructure = "";
         A516PageId = Guid.Empty;
         AV13SDT_InfoContent = new SdtSDT_InfoContent(context);
         AV8SDT_MenuPage = new SdtSDT_MenuPage(context);
         AV9RowItem = new SdtSDT_MenuPage_RowsItem(context);
         AV14InfoContentItem = new SdtSDT_InfoContent_InfoContentItem(context);
         AV10TileItem = new SdtSDT_MenuPage_RowsItem_TilesItem(context);
         AV11SDT_InfoTile = new GXBaseCollection<SdtSDT_InfoTile_SDT_InfoTileItem>( context, "SDT_InfoTileItem", "Comforta_version20");
         AV16HttpResponse = new GxHttpResponse( context);
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.aprc_convertmenutoinfopage__default(),
            new Object[][] {
                new Object[] {
               P00GB2_A523AppVersionId
               }
               , new Object[] {
               P00GB3_A523AppVersionId, P00GB3_A525PageType, P00GB3_A518PageStructure, P00GB3_A516PageId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short nGotPars ;
      private short GxWebError ;
      private int AV19GXV1 ;
      private int AV20GXV2 ;
      private string GXKey ;
      private string gxfirstwebparm ;
      private bool entryPointCalled ;
      private string A518PageStructure ;
      private string A525PageType ;
      private Guid A523AppVersionId ;
      private Guid A516PageId ;
      private GxHttpResponse AV16HttpResponse ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00GB2_A523AppVersionId ;
      private Guid[] P00GB3_A523AppVersionId ;
      private string[] P00GB3_A525PageType ;
      private string[] P00GB3_A518PageStructure ;
      private Guid[] P00GB3_A516PageId ;
      private SdtSDT_InfoContent AV13SDT_InfoContent ;
      private SdtSDT_MenuPage AV8SDT_MenuPage ;
      private SdtSDT_MenuPage_RowsItem AV9RowItem ;
      private SdtSDT_InfoContent_InfoContentItem AV14InfoContentItem ;
      private SdtSDT_MenuPage_RowsItem_TilesItem AV10TileItem ;
      private GXBaseCollection<SdtSDT_InfoTile_SDT_InfoTileItem> AV11SDT_InfoTile ;
   }

   public class aprc_convertmenutoinfopage__default : DataStoreHelperBase, IDataStoreHelper
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
          Object[] prmP00GB2;
          prmP00GB2 = new Object[] {
          };
          Object[] prmP00GB3;
          prmP00GB3 = new Object[] {
          new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00GB2", "SELECT AppVersionId FROM Trn_AppVersion ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00GB2,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00GB3", "SELECT AppVersionId, PageType, PageStructure, PageId FROM Trn_AppVersionPage WHERE (AppVersionId = :AppVersionId) AND (PageType = ( 'Menu')) ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00GB3,100, GxCacheFrequency.OFF ,true,false )
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
                return;
             case 1 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((string[]) buf[1])[0] = rslt.getVarchar(2);
                ((string[]) buf[2])[0] = rslt.getLongVarchar(3);
                ((Guid[]) buf[3])[0] = rslt.getGuid(4);
                return;
       }
    }

 }

}
