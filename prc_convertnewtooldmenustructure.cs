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
   public class prc_convertnewtooldmenustructure : GXProcedure
   {
      public prc_convertnewtooldmenustructure( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_convertnewtooldmenustructure( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( SdtSDT_MenuPage aP0_SDT_MenuPage ,
                           Guid aP1_PageId ,
                           string aP2_PageName ,
                           Guid aP3_LocationId ,
                           out SdtSDT_MobilePage aP4_SDT_MobilePage )
      {
         this.AV8SDT_MenuPage = aP0_SDT_MenuPage;
         this.AV17PageId = aP1_PageId;
         this.AV18PageName = aP2_PageName;
         this.AV19LocationId = aP3_LocationId;
         this.AV9SDT_MobilePage = new SdtSDT_MobilePage(context) ;
         initialize();
         ExecuteImpl();
         aP4_SDT_MobilePage=this.AV9SDT_MobilePage;
      }

      public SdtSDT_MobilePage executeUdp( SdtSDT_MenuPage aP0_SDT_MenuPage ,
                                           Guid aP1_PageId ,
                                           string aP2_PageName ,
                                           Guid aP3_LocationId )
      {
         execute(aP0_SDT_MenuPage, aP1_PageId, aP2_PageName, aP3_LocationId, out aP4_SDT_MobilePage);
         return AV9SDT_MobilePage ;
      }

      public void executeSubmit( SdtSDT_MenuPage aP0_SDT_MenuPage ,
                                 Guid aP1_PageId ,
                                 string aP2_PageName ,
                                 Guid aP3_LocationId ,
                                 out SdtSDT_MobilePage aP4_SDT_MobilePage )
      {
         this.AV8SDT_MenuPage = aP0_SDT_MenuPage;
         this.AV17PageId = aP1_PageId;
         this.AV18PageName = aP2_PageName;
         this.AV19LocationId = aP3_LocationId;
         this.AV9SDT_MobilePage = new SdtSDT_MobilePage(context) ;
         SubmitImpl();
         aP4_SDT_MobilePage=this.AV9SDT_MobilePage;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9SDT_MobilePage = new SdtSDT_MobilePage(context);
         AV9SDT_MobilePage.gxTpr_Pageid = AV17PageId;
         AV9SDT_MobilePage.gxTpr_Pagename = AV18PageName;
         /* Using cursor P00DJ2 */
         pr_default.execute(0, new Object[] {AV19LocationId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A29LocationId = P00DJ2_A29LocationId[0];
            A11OrganisationId = P00DJ2_A11OrganisationId[0];
            A273Trn_ThemeId = P00DJ2_A273Trn_ThemeId[0];
            n273Trn_ThemeId = P00DJ2_n273Trn_ThemeId[0];
            AV22OrganisationId = A11OrganisationId;
            AV20ThemeId = A273Trn_ThemeId;
            pr_default.readNext(0);
         }
         pr_default.close(0);
         new prc_logtoserver(context ).execute(  AV9SDT_MobilePage.ToJSonString(false, true)) ;
         AV24GXV1 = 1;
         while ( AV24GXV1 <= AV8SDT_MenuPage.gxTpr_Rows.Count )
         {
            AV13RowsItem = ((SdtSDT_MenuPage_RowsItem)AV8SDT_MenuPage.gxTpr_Rows.Item(AV24GXV1));
            AV10SDT_Row = new SdtSDT_Row(context);
            AV25GXV2 = 1;
            while ( AV25GXV2 <= AV13RowsItem.gxTpr_Tiles.Count )
            {
               AV15TilesItem = ((SdtSDT_MenuPage_RowsItem_TilesItem)AV13RowsItem.gxTpr_Tiles.Item(AV25GXV2));
               AV16SDT_Col = new SdtSDT_Col(context);
               AV12SDT_Tile = new SdtSDT_Tile(context);
               AV12SDT_Tile.gxTpr_Tileid = AV15TilesItem.gxTpr_Id;
               AV12SDT_Tile.gxTpr_Tilename = AV15TilesItem.gxTpr_Name;
               AV12SDT_Tile.gxTpr_Tiletext = AV15TilesItem.gxTpr_Text;
               AV12SDT_Tile.gxTpr_Tilecolor = AV15TilesItem.gxTpr_Color;
               AV12SDT_Tile.gxTpr_Tilealignment = AV15TilesItem.gxTpr_Align;
               AV12SDT_Tile.gxTpr_Tileicon = AV15TilesItem.gxTpr_Icon;
               GXt_char1 = "";
               new prc_getthemecolorbyname(context ).execute(  AV20ThemeId,  AV15TilesItem.gxTpr_Bgcolor, out  GXt_char1) ;
               AV12SDT_Tile.gxTpr_Tilebgcolor = GXt_char1;
               AV12SDT_Tile.gxTpr_Tilebgimageurl = AV15TilesItem.gxTpr_Bgimageurl;
               AV12SDT_Tile.gxTpr_Tilebgimageopacity = AV15TilesItem.gxTpr_Opacity;
               if ( AV15TilesItem.gxTpr_Size == 0 )
               {
                  AV15TilesItem.gxTpr_Size = 80;
               }
               AV12SDT_Tile.gxTpr_Tilesize = (decimal)(AV15TilesItem.gxTpr_Size/ (decimal)(80));
               AV12SDT_Tile.gxTpr_Tileaction.gxTpr_Objecttype = AV15TilesItem.gxTpr_Action.gxTpr_Objecttype;
               AV12SDT_Tile.gxTpr_Tileaction.gxTpr_Objectid = AV15TilesItem.gxTpr_Action.gxTpr_Objectid;
               AV12SDT_Tile.gxTpr_Tileaction.gxTpr_Objecturl = AV15TilesItem.gxTpr_Action.gxTpr_Objecturl;
               if ( StringUtil.StrCmp(AV15TilesItem.gxTpr_Action.gxTpr_Objecttype, "DynamicForm") == 0 )
               {
                  new prc_logtoserver(context ).execute(  context.GetMessage( "Found form: ", "")+AV15TilesItem.gxTpr_Action.gxTpr_Objectid) ;
                  /* Using cursor P00DJ3 */
                  pr_default.execute(1, new Object[] {AV15TilesItem.gxTpr_Action.gxTpr_Objectid});
                  while ( (pr_default.getStatus(1) != 101) )
                  {
                     A206WWPFormId = P00DJ3_A206WWPFormId[0];
                     A208WWPFormReferenceName = P00DJ3_A208WWPFormReferenceName[0];
                     A207WWPFormVersionNumber = P00DJ3_A207WWPFormVersionNumber[0];
                     AV12SDT_Tile.gxTpr_Tileaction.gxTpr_Objecturl = A367CallToActionUrl;
                     GXt_char1 = "";
                     GXt_char2 = context.GetMessage( "Form", "");
                     new prc_getcalltoactionformurl(context ).execute( ref  GXt_char2, ref  A208WWPFormReferenceName, out  GXt_char1) ;
                     AV12SDT_Tile.gxTpr_Tileaction.gxTpr_Objecturl = GXt_char1;
                     pr_default.readNext(1);
                  }
                  pr_default.close(1);
               }
               AV16SDT_Col.gxTpr_Tile = AV12SDT_Tile;
               AV10SDT_Row.gxTpr_Col.Add(AV16SDT_Col, 0);
               new prc_logtoserver(context ).execute(  "		"+AV12SDT_Tile.ToJSonString(false, true)) ;
               AV25GXV2 = (int)(AV25GXV2+1);
            }
            AV9SDT_MobilePage.gxTpr_Row.Add(AV10SDT_Row, 0);
            AV24GXV1 = (int)(AV24GXV1+1);
         }
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
         AV9SDT_MobilePage = new SdtSDT_MobilePage(context);
         P00DJ2_A29LocationId = new Guid[] {Guid.Empty} ;
         P00DJ2_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00DJ2_A273Trn_ThemeId = new Guid[] {Guid.Empty} ;
         P00DJ2_n273Trn_ThemeId = new bool[] {false} ;
         A29LocationId = Guid.Empty;
         A11OrganisationId = Guid.Empty;
         A273Trn_ThemeId = Guid.Empty;
         AV22OrganisationId = Guid.Empty;
         AV20ThemeId = Guid.Empty;
         AV13RowsItem = new SdtSDT_MenuPage_RowsItem(context);
         AV10SDT_Row = new SdtSDT_Row(context);
         AV15TilesItem = new SdtSDT_MenuPage_RowsItem_TilesItem(context);
         AV16SDT_Col = new SdtSDT_Col(context);
         AV12SDT_Tile = new SdtSDT_Tile(context);
         P00DJ3_A206WWPFormId = new short[1] ;
         P00DJ3_A208WWPFormReferenceName = new string[] {""} ;
         P00DJ3_A207WWPFormVersionNumber = new short[1] ;
         A208WWPFormReferenceName = "";
         A367CallToActionUrl = "";
         GXt_char1 = "";
         GXt_char2 = "";
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_convertnewtooldmenustructure__default(),
            new Object[][] {
                new Object[] {
               P00DJ2_A29LocationId, P00DJ2_A11OrganisationId, P00DJ2_A273Trn_ThemeId, P00DJ2_n273Trn_ThemeId
               }
               , new Object[] {
               P00DJ3_A206WWPFormId, P00DJ3_A208WWPFormReferenceName, P00DJ3_A207WWPFormVersionNumber
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short A206WWPFormId ;
      private short A207WWPFormVersionNumber ;
      private int AV24GXV1 ;
      private int AV25GXV2 ;
      private string GXt_char1 ;
      private string GXt_char2 ;
      private bool n273Trn_ThemeId ;
      private string AV18PageName ;
      private string A208WWPFormReferenceName ;
      private string A367CallToActionUrl ;
      private Guid AV17PageId ;
      private Guid AV19LocationId ;
      private Guid A29LocationId ;
      private Guid A11OrganisationId ;
      private Guid A273Trn_ThemeId ;
      private Guid AV22OrganisationId ;
      private Guid AV20ThemeId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private SdtSDT_MenuPage AV8SDT_MenuPage ;
      private SdtSDT_MobilePage AV9SDT_MobilePage ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00DJ2_A29LocationId ;
      private Guid[] P00DJ2_A11OrganisationId ;
      private Guid[] P00DJ2_A273Trn_ThemeId ;
      private bool[] P00DJ2_n273Trn_ThemeId ;
      private SdtSDT_MenuPage_RowsItem AV13RowsItem ;
      private SdtSDT_Row AV10SDT_Row ;
      private SdtSDT_MenuPage_RowsItem_TilesItem AV15TilesItem ;
      private SdtSDT_Col AV16SDT_Col ;
      private SdtSDT_Tile AV12SDT_Tile ;
      private short[] P00DJ3_A206WWPFormId ;
      private string[] P00DJ3_A208WWPFormReferenceName ;
      private short[] P00DJ3_A207WWPFormVersionNumber ;
      private SdtSDT_MobilePage aP4_SDT_MobilePage ;
   }

   public class prc_convertnewtooldmenustructure__default : DataStoreHelperBase, IDataStoreHelper
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
          Object[] prmP00DJ2;
          prmP00DJ2 = new Object[] {
          new ParDef("AV19LocationId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00DJ3;
          prmP00DJ3 = new Object[] {
          new ParDef("AV15Tile_1Action_1Objectid",GXType.VarChar,100,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00DJ2", "SELECT LocationId, OrganisationId, Trn_ThemeId FROM Trn_Location WHERE LocationId = :AV19LocationId ORDER BY LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00DJ2,100, GxCacheFrequency.OFF ,false,false )
             ,new CursorDef("P00DJ3", "SELECT WWPFormId, WWPFormReferenceName, WWPFormVersionNumber FROM WWP_Form WHERE WWPFormId = TO_NUMBER(0 || :AV15Tile_1Action_1Objectid,'9999999999999999999999999999.99999999999999') ORDER BY WWPFormId, WWPFormVersionNumber ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00DJ3,100, GxCacheFrequency.OFF ,true,false )
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
                ((Guid[]) buf[2])[0] = rslt.getGuid(3);
                ((bool[]) buf[3])[0] = rslt.wasNull(3);
                return;
             case 1 :
                ((short[]) buf[0])[0] = rslt.getShort(1);
                ((string[]) buf[1])[0] = rslt.getVarchar(2);
                ((short[]) buf[2])[0] = rslt.getShort(3);
                return;
       }
    }

 }

}
