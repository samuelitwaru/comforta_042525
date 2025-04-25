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
   public class prc_initservicepages : GXProcedure
   {
      public prc_initservicepages( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_initservicepages( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_AppVersionId ,
                           out SdtTrn_AppVersion_Page aP1_BC_CarePage ,
                           out SdtTrn_AppVersion_Page aP2_BC_LivingPage ,
                           out SdtTrn_AppVersion_Page aP3_BC_ServicesPage )
      {
         this.AV23AppVersionId = aP0_AppVersionId;
         this.AV15BC_CarePage = new SdtTrn_AppVersion_Page(context) ;
         this.AV18BC_LivingPage = new SdtTrn_AppVersion_Page(context) ;
         this.AV17BC_ServicesPage = new SdtTrn_AppVersion_Page(context) ;
         initialize();
         ExecuteImpl();
         aP1_BC_CarePage=this.AV15BC_CarePage;
         aP2_BC_LivingPage=this.AV18BC_LivingPage;
         aP3_BC_ServicesPage=this.AV17BC_ServicesPage;
      }

      public SdtTrn_AppVersion_Page executeUdp( Guid aP0_AppVersionId ,
                                                out SdtTrn_AppVersion_Page aP1_BC_CarePage ,
                                                out SdtTrn_AppVersion_Page aP2_BC_LivingPage )
      {
         execute(aP0_AppVersionId, out aP1_BC_CarePage, out aP2_BC_LivingPage, out aP3_BC_ServicesPage);
         return AV17BC_ServicesPage ;
      }

      public void executeSubmit( Guid aP0_AppVersionId ,
                                 out SdtTrn_AppVersion_Page aP1_BC_CarePage ,
                                 out SdtTrn_AppVersion_Page aP2_BC_LivingPage ,
                                 out SdtTrn_AppVersion_Page aP3_BC_ServicesPage )
      {
         this.AV23AppVersionId = aP0_AppVersionId;
         this.AV15BC_CarePage = new SdtTrn_AppVersion_Page(context) ;
         this.AV18BC_LivingPage = new SdtTrn_AppVersion_Page(context) ;
         this.AV17BC_ServicesPage = new SdtTrn_AppVersion_Page(context) ;
         SubmitImpl();
         aP1_BC_CarePage=this.AV15BC_CarePage;
         aP2_BC_LivingPage=this.AV18BC_LivingPage;
         aP3_BC_ServicesPage=this.AV17BC_ServicesPage;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Using cursor P00BB2 */
         pr_default.execute(0, new Object[] {AV23AppVersionId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A523AppVersionId = P00BB2_A523AppVersionId[0];
            AV25GXLvl3 = 0;
            /* Using cursor P00BB3 */
            pr_default.execute(1, new Object[] {A523AppVersionId});
            while ( (pr_default.getStatus(1) != 101) )
            {
               A525PageType = P00BB3_A525PageType[0];
               A516PageId = P00BB3_A516PageId[0];
               AV25GXLvl3 = 1;
               AV18BC_LivingPage.gxTpr_Pageid = A516PageId;
               pr_default.readNext(1);
            }
            pr_default.close(1);
            if ( AV25GXLvl3 == 0 )
            {
               AV18BC_LivingPage.gxTpr_Pageid = Guid.NewGuid( );
            }
            AV26GXLvl10 = 0;
            /* Using cursor P00BB4 */
            pr_default.execute(2, new Object[] {A523AppVersionId});
            while ( (pr_default.getStatus(2) != 101) )
            {
               A525PageType = P00BB4_A525PageType[0];
               A516PageId = P00BB4_A516PageId[0];
               AV26GXLvl10 = 1;
               AV15BC_CarePage.gxTpr_Pageid = A516PageId;
               pr_default.readNext(2);
            }
            pr_default.close(2);
            if ( AV26GXLvl10 == 0 )
            {
               AV15BC_CarePage.gxTpr_Pageid = Guid.NewGuid( );
            }
            AV27GXLvl17 = 0;
            /* Using cursor P00BB5 */
            pr_default.execute(3, new Object[] {A523AppVersionId});
            while ( (pr_default.getStatus(3) != 101) )
            {
               A525PageType = P00BB5_A525PageType[0];
               A516PageId = P00BB5_A516PageId[0];
               AV27GXLvl17 = 1;
               AV17BC_ServicesPage.gxTpr_Pageid = A516PageId;
               pr_default.readNext(3);
            }
            pr_default.close(3);
            if ( AV27GXLvl17 == 0 )
            {
               AV17BC_ServicesPage.gxTpr_Pageid = Guid.NewGuid( );
            }
            /* Exiting from a For First loop. */
            if (true) break;
         }
         pr_default.close(0);
         AV18BC_LivingPage.gxTpr_Pagename = "My Living";
         AV18BC_LivingPage.gxTpr_Ispredefined = false;
         AV18BC_LivingPage.gxTpr_Pagetype = "MyLiving";
         AV19SDT_MenuPage = new SdtSDT_MenuPage(context);
         GXt_SdtSDT_MenuPage_RowsItem_TilesItem1 = AV21TilesItem;
         new prc_createpagetile(context ).execute(  "Tile",  "#333333",  "left",  "",  0,  "accentColor",  "",  "",  "",  "", out  GXt_SdtSDT_MenuPage_RowsItem_TilesItem1) ;
         AV21TilesItem = GXt_SdtSDT_MenuPage_RowsItem_TilesItem1;
         AV22RowsItem = new SdtSDT_MenuPage_RowsItem(context);
         AV22RowsItem.gxTpr_Id = new SdtRandomStringGenerator(context).generate(15);
         AV22RowsItem.gxTpr_Tiles.Add(AV21TilesItem, 0);
         AV19SDT_MenuPage.gxTpr_Rows.Add(AV22RowsItem, 0);
         AV18BC_LivingPage.gxTpr_Pagestructure = AV19SDT_MenuPage.ToJSonString(false, true);
         AV15BC_CarePage.gxTpr_Pagename = "My Care";
         AV15BC_CarePage.gxTpr_Ispredefined = false;
         AV15BC_CarePage.gxTpr_Pagetype = "MyCare";
         AV19SDT_MenuPage = new SdtSDT_MenuPage(context);
         GXt_SdtSDT_MenuPage_RowsItem_TilesItem1 = AV21TilesItem;
         new prc_createpagetile(context ).execute(  "Tile",  "#333333",  "left",  "",  0,  "accentColor",  "",  "",  "",  "", out  GXt_SdtSDT_MenuPage_RowsItem_TilesItem1) ;
         AV21TilesItem = GXt_SdtSDT_MenuPage_RowsItem_TilesItem1;
         AV22RowsItem = new SdtSDT_MenuPage_RowsItem(context);
         AV22RowsItem.gxTpr_Id = new SdtRandomStringGenerator(context).generate(15);
         AV22RowsItem.gxTpr_Tiles.Add(AV21TilesItem, 0);
         AV19SDT_MenuPage.gxTpr_Rows.Add(AV22RowsItem, 0);
         AV15BC_CarePage.gxTpr_Pagestructure = AV19SDT_MenuPage.ToJSonString(false, true);
         AV17BC_ServicesPage.gxTpr_Pagename = "My Services";
         AV17BC_ServicesPage.gxTpr_Ispredefined = false;
         AV17BC_ServicesPage.gxTpr_Pagetype = "MyService";
         AV19SDT_MenuPage = new SdtSDT_MenuPage(context);
         GXt_SdtSDT_MenuPage_RowsItem_TilesItem1 = AV21TilesItem;
         new prc_createpagetile(context ).execute(  "Tile",  "#333333",  "left",  "",  0,  "accentColor",  "",  "",  "",  "", out  GXt_SdtSDT_MenuPage_RowsItem_TilesItem1) ;
         AV21TilesItem = GXt_SdtSDT_MenuPage_RowsItem_TilesItem1;
         AV22RowsItem = new SdtSDT_MenuPage_RowsItem(context);
         AV22RowsItem.gxTpr_Id = new SdtRandomStringGenerator(context).generate(15);
         AV22RowsItem.gxTpr_Tiles.Add(AV21TilesItem, 0);
         AV19SDT_MenuPage.gxTpr_Rows.Add(AV22RowsItem, 0);
         AV17BC_ServicesPage.gxTpr_Pagestructure = AV19SDT_MenuPage.ToJSonString(false, true);
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
         AV15BC_CarePage = new SdtTrn_AppVersion_Page(context);
         AV18BC_LivingPage = new SdtTrn_AppVersion_Page(context);
         AV17BC_ServicesPage = new SdtTrn_AppVersion_Page(context);
         P00BB2_A523AppVersionId = new Guid[] {Guid.Empty} ;
         A523AppVersionId = Guid.Empty;
         P00BB3_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00BB3_A525PageType = new string[] {""} ;
         P00BB3_A516PageId = new Guid[] {Guid.Empty} ;
         A525PageType = "";
         A516PageId = Guid.Empty;
         P00BB4_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00BB4_A525PageType = new string[] {""} ;
         P00BB4_A516PageId = new Guid[] {Guid.Empty} ;
         P00BB5_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00BB5_A525PageType = new string[] {""} ;
         P00BB5_A516PageId = new Guid[] {Guid.Empty} ;
         AV19SDT_MenuPage = new SdtSDT_MenuPage(context);
         AV21TilesItem = new SdtSDT_MenuPage_RowsItem_TilesItem(context);
         AV22RowsItem = new SdtSDT_MenuPage_RowsItem(context);
         GXt_SdtSDT_MenuPage_RowsItem_TilesItem1 = new SdtSDT_MenuPage_RowsItem_TilesItem(context);
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_initservicepages__default(),
            new Object[][] {
                new Object[] {
               P00BB2_A523AppVersionId
               }
               , new Object[] {
               P00BB3_A523AppVersionId, P00BB3_A525PageType, P00BB3_A516PageId
               }
               , new Object[] {
               P00BB4_A523AppVersionId, P00BB4_A525PageType, P00BB4_A516PageId
               }
               , new Object[] {
               P00BB5_A523AppVersionId, P00BB5_A525PageType, P00BB5_A516PageId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV25GXLvl3 ;
      private short AV26GXLvl10 ;
      private short AV27GXLvl17 ;
      private string A525PageType ;
      private Guid AV23AppVersionId ;
      private Guid A523AppVersionId ;
      private Guid A516PageId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private SdtTrn_AppVersion_Page AV15BC_CarePage ;
      private SdtTrn_AppVersion_Page AV18BC_LivingPage ;
      private SdtTrn_AppVersion_Page AV17BC_ServicesPage ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00BB2_A523AppVersionId ;
      private Guid[] P00BB3_A523AppVersionId ;
      private string[] P00BB3_A525PageType ;
      private Guid[] P00BB3_A516PageId ;
      private Guid[] P00BB4_A523AppVersionId ;
      private string[] P00BB4_A525PageType ;
      private Guid[] P00BB4_A516PageId ;
      private Guid[] P00BB5_A523AppVersionId ;
      private string[] P00BB5_A525PageType ;
      private Guid[] P00BB5_A516PageId ;
      private SdtSDT_MenuPage AV19SDT_MenuPage ;
      private SdtSDT_MenuPage_RowsItem_TilesItem AV21TilesItem ;
      private SdtSDT_MenuPage_RowsItem AV22RowsItem ;
      private SdtSDT_MenuPage_RowsItem_TilesItem GXt_SdtSDT_MenuPage_RowsItem_TilesItem1 ;
      private SdtTrn_AppVersion_Page aP1_BC_CarePage ;
      private SdtTrn_AppVersion_Page aP2_BC_LivingPage ;
      private SdtTrn_AppVersion_Page aP3_BC_ServicesPage ;
   }

   public class prc_initservicepages__default : DataStoreHelperBase, IDataStoreHelper
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
          Object[] prmP00BB2;
          prmP00BB2 = new Object[] {
          new ParDef("AV23AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00BB3;
          prmP00BB3 = new Object[] {
          new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00BB4;
          prmP00BB4 = new Object[] {
          new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00BB5;
          prmP00BB5 = new Object[] {
          new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00BB2", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :AV23AppVersionId ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00BB2,1, GxCacheFrequency.OFF ,true,true )
             ,new CursorDef("P00BB3", "SELECT AppVersionId, PageType, PageId FROM Trn_AppVersionPage WHERE (AppVersionId = :AppVersionId) AND (PageType = ( 'MyLiving')) ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00BB3,100, GxCacheFrequency.OFF ,false,false )
             ,new CursorDef("P00BB4", "SELECT AppVersionId, PageType, PageId FROM Trn_AppVersionPage WHERE (AppVersionId = :AppVersionId) AND (PageType = ( 'MyLiving')) ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00BB4,100, GxCacheFrequency.OFF ,false,false )
             ,new CursorDef("P00BB5", "SELECT AppVersionId, PageType, PageId FROM Trn_AppVersionPage WHERE (AppVersionId = :AppVersionId) AND (PageType = ( 'MyLiving')) ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00BB5,100, GxCacheFrequency.OFF ,false,false )
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
                ((Guid[]) buf[2])[0] = rslt.getGuid(3);
                return;
             case 2 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((string[]) buf[1])[0] = rslt.getVarchar(2);
                ((Guid[]) buf[2])[0] = rslt.getGuid(3);
                return;
             case 3 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((string[]) buf[1])[0] = rslt.getVarchar(2);
                ((Guid[]) buf[2])[0] = rslt.getGuid(3);
                return;
       }
    }

 }

}
