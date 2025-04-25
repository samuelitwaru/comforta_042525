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
   public class prc_validatemenupage : GXProcedure
   {
      public prc_validatemenupage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_validatemenupage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_AppVersionId ,
                           ref SdtSDT_MenuPage aP1_SDT_MenuPage )
      {
         this.AV13AppVersionId = aP0_AppVersionId;
         this.AV8SDT_MenuPage = aP1_SDT_MenuPage;
         initialize();
         ExecuteImpl();
         aP1_SDT_MenuPage=this.AV8SDT_MenuPage;
      }

      public SdtSDT_MenuPage executeUdp( Guid aP0_AppVersionId )
      {
         execute(aP0_AppVersionId, ref aP1_SDT_MenuPage);
         return AV8SDT_MenuPage ;
      }

      public void executeSubmit( Guid aP0_AppVersionId ,
                                 ref SdtSDT_MenuPage aP1_SDT_MenuPage )
      {
         this.AV13AppVersionId = aP0_AppVersionId;
         this.AV8SDT_MenuPage = aP1_SDT_MenuPage;
         SubmitImpl();
         aP1_SDT_MenuPage=this.AV8SDT_MenuPage;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV14GXV1 = 1;
         while ( AV14GXV1 <= AV8SDT_MenuPage.gxTpr_Rows.Count )
         {
            AV10RowItem = ((SdtSDT_MenuPage_RowsItem)AV8SDT_MenuPage.gxTpr_Rows.Item(AV14GXV1));
            AV15GXV2 = 1;
            while ( AV15GXV2 <= AV10RowItem.gxTpr_Tiles.Count )
            {
               AV11TileItem = ((SdtSDT_MenuPage_RowsItem_TilesItem)AV10RowItem.gxTpr_Tiles.Item(AV15GXV2));
               if ( ( ( StringUtil.StrCmp(AV11TileItem.gxTpr_Action.gxTpr_Objecttype, "Content") == 0 ) ) || ( ( StringUtil.StrCmp(AV11TileItem.gxTpr_Action.gxTpr_Objecttype, "Menu") == 0 ) ) )
               {
                  /* Execute user subroutine: 'ENSUREPAGEEXISTS' */
                  S111 ();
                  if ( returnInSub )
                  {
                     cleanup();
                     if (true) return;
                  }
               }
               else if ( ( ( StringUtil.StrCmp(AV11TileItem.gxTpr_Action.gxTpr_Objecttype, "Calendar") == 0 ) ) || ( ( StringUtil.StrCmp(AV11TileItem.gxTpr_Action.gxTpr_Objecttype, "MyActivity") == 0 ) ) )
               {
                  /* Execute user subroutine: 'ENSUREPAGEEXISTS' */
                  S111 ();
                  if ( returnInSub )
                  {
                     cleanup();
                     if (true) return;
                  }
               }
               AV15GXV2 = (int)(AV15GXV2+1);
            }
            AV14GXV1 = (int)(AV14GXV1+1);
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'ENSUREPAGEEXISTS' Routine */
         returnInSub = false;
         /* Using cursor P00DH2 */
         pr_default.execute(0, new Object[] {AV13AppVersionId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A523AppVersionId = P00DH2_A523AppVersionId[0];
            AV17GXLvl16 = 0;
            /* Using cursor P00DH3 */
            pr_default.execute(1, new Object[] {A523AppVersionId, AV11TileItem.gxTpr_Action.gxTpr_Objectid});
            while ( (pr_default.getStatus(1) != 101) )
            {
               A516PageId = P00DH3_A516PageId[0];
               AV17GXLvl16 = 1;
               pr_default.readNext(1);
            }
            pr_default.close(1);
            if ( AV17GXLvl16 == 0 )
            {
               AV11TileItem.gxTpr_Action.gxTpr_Objectid = "";
               AV11TileItem.gxTpr_Action.gxTpr_Objecttype = "";
               AV11TileItem.gxTpr_Action.gxTpr_Objecturl = "";
            }
            /* Exiting from a For First loop. */
            if (true) break;
         }
         pr_default.close(0);
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
         AV10RowItem = new SdtSDT_MenuPage_RowsItem(context);
         AV11TileItem = new SdtSDT_MenuPage_RowsItem_TilesItem(context);
         P00DH2_A523AppVersionId = new Guid[] {Guid.Empty} ;
         A523AppVersionId = Guid.Empty;
         P00DH3_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00DH3_A516PageId = new Guid[] {Guid.Empty} ;
         A516PageId = Guid.Empty;
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_validatemenupage__default(),
            new Object[][] {
                new Object[] {
               P00DH2_A523AppVersionId
               }
               , new Object[] {
               P00DH3_A523AppVersionId, P00DH3_A516PageId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV17GXLvl16 ;
      private int AV14GXV1 ;
      private int AV15GXV2 ;
      private bool returnInSub ;
      private Guid AV13AppVersionId ;
      private Guid A523AppVersionId ;
      private Guid A516PageId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private SdtSDT_MenuPage AV8SDT_MenuPage ;
      private SdtSDT_MenuPage aP1_SDT_MenuPage ;
      private SdtSDT_MenuPage_RowsItem AV10RowItem ;
      private SdtSDT_MenuPage_RowsItem_TilesItem AV11TileItem ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00DH2_A523AppVersionId ;
      private Guid[] P00DH3_A523AppVersionId ;
      private Guid[] P00DH3_A516PageId ;
   }

   public class prc_validatemenupage__default : DataStoreHelperBase, IDataStoreHelper
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
          Object[] prmP00DH2;
          prmP00DH2 = new Object[] {
          new ParDef("AV13AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00DH3;
          prmP00DH3 = new Object[] {
          new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV11Tile_1Action_1Objectid",GXType.VarChar,100,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00DH2", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :AV13AppVersionId ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00DH2,1, GxCacheFrequency.OFF ,true,true )
             ,new CursorDef("P00DH3", "SELECT AppVersionId, PageId FROM Trn_AppVersionPage WHERE (AppVersionId = :AppVersionId) AND (PageId = CASE WHEN (:AV11Tile_1Action_1Objectid ~ ('[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}')) THEN RTRIM(:AV11Tile_1Action_1Objectid) ELSE '00000000-0000-0000-0000-000000000000' END) ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00DH3,100, GxCacheFrequency.OFF ,false,false )
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
                ((Guid[]) buf[1])[0] = rslt.getGuid(2);
                return;
       }
    }

 }

}
