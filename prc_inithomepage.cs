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
   public class prc_inithomepage : GXProcedure
   {
      public prc_inithomepage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_inithomepage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_AppVersionId ,
                           out SdtTrn_AppVersion_Page aP1_BC_HomePage )
      {
         this.AV25AppVersionId = aP0_AppVersionId;
         this.AV13BC_HomePage = new SdtTrn_AppVersion_Page(context) ;
         initialize();
         ExecuteImpl();
         aP1_BC_HomePage=this.AV13BC_HomePage;
      }

      public SdtTrn_AppVersion_Page executeUdp( Guid aP0_AppVersionId )
      {
         execute(aP0_AppVersionId, out aP1_BC_HomePage);
         return AV13BC_HomePage ;
      }

      public void executeSubmit( Guid aP0_AppVersionId ,
                                 out SdtTrn_AppVersion_Page aP1_BC_HomePage )
      {
         this.AV25AppVersionId = aP0_AppVersionId;
         this.AV13BC_HomePage = new SdtTrn_AppVersion_Page(context) ;
         SubmitImpl();
         aP1_BC_HomePage=this.AV13BC_HomePage;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Using cursor P00BC2 */
         pr_default.execute(0, new Object[] {AV25AppVersionId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A523AppVersionId = P00BC2_A523AppVersionId[0];
            A29LocationId = P00BC2_A29LocationId[0];
            n29LocationId = P00BC2_n29LocationId[0];
            A11OrganisationId = P00BC2_A11OrganisationId[0];
            n11OrganisationId = P00BC2_n11OrganisationId[0];
            AV21LocationId = A29LocationId;
            AV22OrganisationId = A11OrganisationId;
            /* Using cursor P00BC3 */
            pr_default.execute(1, new Object[] {A523AppVersionId});
            while ( (pr_default.getStatus(1) != 101) )
            {
               A517PageName = P00BC3_A517PageName[0];
               A525PageType = P00BC3_A525PageType[0];
               A516PageId = P00BC3_A516PageId[0];
               if ( ( StringUtil.StrCmp(A525PageType, "Information") == 0 ) && ( StringUtil.StrCmp(A517PageName, "Reception") == 0 ) )
               {
                  AV8ReceptionPageId = A516PageId;
               }
               else if ( ( StringUtil.StrCmp(A525PageType, "Information") == 0 ) && ( StringUtil.StrCmp(A517PageName, "Location") == 0 ) )
               {
                  AV9LocationPageId = A516PageId;
               }
               else if ( StringUtil.StrCmp(A525PageType, "Calendar") == 0 )
               {
                  AV19CalendarId = A516PageId;
               }
               else if ( StringUtil.StrCmp(A525PageType, "MyActivity") == 0 )
               {
                  AV20MyActivityId = A516PageId;
               }
               else if ( StringUtil.StrCmp(A525PageType, "Map") == 0 )
               {
                  AV24MapsPageId = A516PageId;
               }
               else if ( StringUtil.StrCmp(A525PageType, "MyCare") == 0 )
               {
                  AV10CarePageId = A516PageId;
               }
               else if ( StringUtil.StrCmp(A525PageType, "MyLiving") == 0 )
               {
                  AV11LivingPageId = A516PageId;
               }
               else if ( StringUtil.StrCmp(A525PageType, "MyService") == 0 )
               {
                  AV12ServicesPageId = A516PageId;
               }
               else
               {
               }
               pr_default.readNext(1);
            }
            pr_default.close(1);
            /* Exiting from a For First loop. */
            if (true) break;
         }
         pr_default.close(0);
         AV17GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context).get();
         AV18baseUrl = AV17GAMApplication.gxTpr_Environment.gxTpr_Url;
         AV23BC_Trn_Location.Load(AV21LocationId, AV22OrganisationId);
         AV13BC_HomePage.gxTpr_Pageid = Guid.NewGuid( );
         AV13BC_HomePage.gxTpr_Pagename = "Home";
         AV13BC_HomePage.gxTpr_Ispredefined = true;
         AV13BC_HomePage.gxTpr_Pagetype = "Menu";
         /* Execute user subroutine: 'RECEPTIONTILE' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'CALENDARANDMYACTIVITYTILES' */
         S121 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'LOCATIONTILE' */
         S131 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'SERVICESTILES' */
         S141 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'MAPSTILE' */
         S151 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         AV13BC_HomePage.gxTpr_Pagestructure = AV14SDT_MenuPage.ToJSonString(false, true);
         cleanup();
      }

      protected void S111( )
      {
         /* 'RECEPTIONTILE' Routine */
         returnInSub = false;
         AV16RowsItem = new SdtSDT_MenuPage_RowsItem(context);
         AV16RowsItem.gxTpr_Id = new SdtRandomStringGenerator(context).generate(15);
         GXt_SdtSDT_MenuPage_RowsItem_TilesItem1 = AV15TilesItem;
         new prc_createpagetile(context ).execute(  context.GetMessage( "Reception", ""),  "#ffffff",  "left",  "Reception",  0,  "accentColor",  AV23BC_Trn_Location.gxTpr_Receptionimage_gxi,  AV8ReceptionPageId.ToString(),  "Information",  "", out  GXt_SdtSDT_MenuPage_RowsItem_TilesItem1) ;
         AV15TilesItem = GXt_SdtSDT_MenuPage_RowsItem_TilesItem1;
         AV16RowsItem.gxTpr_Tiles.Add(AV15TilesItem, 0);
         AV14SDT_MenuPage.gxTpr_Rows.Add(AV16RowsItem, 0);
      }

      protected void S121( )
      {
         /* 'CALENDARANDMYACTIVITYTILES' Routine */
         returnInSub = false;
         AV16RowsItem = new SdtSDT_MenuPage_RowsItem(context);
         AV16RowsItem.gxTpr_Id = new SdtRandomStringGenerator(context).generate(15);
         GXt_SdtSDT_MenuPage_RowsItem_TilesItem1 = AV15TilesItem;
         new prc_createpagetile(context ).execute(  context.GetMessage( "Agenda", ""),  "#ffffff",  "left",  "Calendar",  0,  "accentColor",  AV18baseUrl+"media/Calendar.png",  AV19CalendarId.ToString(),  "Calendar",  "", out  GXt_SdtSDT_MenuPage_RowsItem_TilesItem1) ;
         AV15TilesItem = GXt_SdtSDT_MenuPage_RowsItem_TilesItem1;
         AV16RowsItem.gxTpr_Tiles.Add(AV15TilesItem, 0);
         GXt_SdtSDT_MenuPage_RowsItem_TilesItem1 = AV15TilesItem;
         new prc_createpagetile(context ).execute(  context.GetMessage( "My Activity", ""),  "#ffffff",  "left",  "",  0,  "accentColor",  "",  AV20MyActivityId.ToString(),  "MyActivity",  "", out  GXt_SdtSDT_MenuPage_RowsItem_TilesItem1) ;
         AV15TilesItem = GXt_SdtSDT_MenuPage_RowsItem_TilesItem1;
         AV16RowsItem.gxTpr_Tiles.Add(AV15TilesItem, 0);
         AV14SDT_MenuPage.gxTpr_Rows.Add(AV16RowsItem, 0);
      }

      protected void S131( )
      {
         /* 'LOCATIONTILE' Routine */
         returnInSub = false;
         AV16RowsItem = new SdtSDT_MenuPage_RowsItem(context);
         AV16RowsItem.gxTpr_Id = new SdtRandomStringGenerator(context).generate(15);
         GXt_SdtSDT_MenuPage_RowsItem_TilesItem1 = AV15TilesItem;
         new prc_createpagetile(context ).execute(  context.GetMessage( "Location", ""),  "#ffffff",  "left",  "",  0,  "accentColor",  AV23BC_Trn_Location.gxTpr_Locationimage_gxi,  AV9LocationPageId.ToString(),  "Information",  "", out  GXt_SdtSDT_MenuPage_RowsItem_TilesItem1) ;
         AV15TilesItem = GXt_SdtSDT_MenuPage_RowsItem_TilesItem1;
         AV16RowsItem.gxTpr_Tiles.Add(AV15TilesItem, 0);
         AV14SDT_MenuPage.gxTpr_Rows.Add(AV16RowsItem, 0);
      }

      protected void S141( )
      {
         /* 'SERVICESTILES' Routine */
         returnInSub = false;
         AV16RowsItem = new SdtSDT_MenuPage_RowsItem(context);
         AV16RowsItem.gxTpr_Id = new SdtRandomStringGenerator(context).generate(15);
         GXt_SdtSDT_MenuPage_RowsItem_TilesItem1 = AV15TilesItem;
         new prc_createpagetile(context ).execute(  context.GetMessage( "My Care", ""),  "#ffffff",  "center",  "",  0,  "accentColor",  "",  AV10CarePageId.ToString(),  "Menu",  "", out  GXt_SdtSDT_MenuPage_RowsItem_TilesItem1) ;
         AV15TilesItem = GXt_SdtSDT_MenuPage_RowsItem_TilesItem1;
         AV16RowsItem.gxTpr_Tiles.Add(AV15TilesItem, 0);
         GXt_SdtSDT_MenuPage_RowsItem_TilesItem1 = AV15TilesItem;
         new prc_createpagetile(context ).execute(  context.GetMessage( "My Living", ""),  "#ffffff",  "center",  "",  0,  "accentColor",  "",  AV11LivingPageId.ToString(),  "Menu",  "", out  GXt_SdtSDT_MenuPage_RowsItem_TilesItem1) ;
         AV15TilesItem = GXt_SdtSDT_MenuPage_RowsItem_TilesItem1;
         AV16RowsItem.gxTpr_Tiles.Add(AV15TilesItem, 0);
         GXt_SdtSDT_MenuPage_RowsItem_TilesItem1 = AV15TilesItem;
         new prc_createpagetile(context ).execute(  context.GetMessage( "My Services", ""),  "#ffffff",  "center",  "",  0,  "accentColor",  "",  AV12ServicesPageId.ToString(),  "Menu",  "", out  GXt_SdtSDT_MenuPage_RowsItem_TilesItem1) ;
         AV15TilesItem = GXt_SdtSDT_MenuPage_RowsItem_TilesItem1;
         AV16RowsItem.gxTpr_Tiles.Add(AV15TilesItem, 0);
         AV14SDT_MenuPage.gxTpr_Rows.Add(AV16RowsItem, 0);
      }

      protected void S151( )
      {
         /* 'MAPSTILE' Routine */
         returnInSub = false;
         AV16RowsItem = new SdtSDT_MenuPage_RowsItem(context);
         AV16RowsItem.gxTpr_Id = new SdtRandomStringGenerator(context).generate(15);
         GXt_SdtSDT_MenuPage_RowsItem_TilesItem1 = AV15TilesItem;
         new prc_createpagetile(context ).execute(  context.GetMessage( "Maps", ""),  "#ffffff",  context.GetMessage( "left", ""),  "",  0,  "accentColor",  "",  AV24MapsPageId.ToString(),  "Map",  AV24MapsPageId.ToString(), out  GXt_SdtSDT_MenuPage_RowsItem_TilesItem1) ;
         AV15TilesItem = GXt_SdtSDT_MenuPage_RowsItem_TilesItem1;
         AV16RowsItem.gxTpr_Tiles.Add(AV15TilesItem, 0);
         AV14SDT_MenuPage.gxTpr_Rows.Add(AV16RowsItem, 0);
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
         AV13BC_HomePage = new SdtTrn_AppVersion_Page(context);
         P00BC2_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00BC2_A29LocationId = new Guid[] {Guid.Empty} ;
         P00BC2_n29LocationId = new bool[] {false} ;
         P00BC2_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00BC2_n11OrganisationId = new bool[] {false} ;
         A523AppVersionId = Guid.Empty;
         A29LocationId = Guid.Empty;
         A11OrganisationId = Guid.Empty;
         AV21LocationId = Guid.Empty;
         AV22OrganisationId = Guid.Empty;
         P00BC3_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00BC3_A517PageName = new string[] {""} ;
         P00BC3_A525PageType = new string[] {""} ;
         P00BC3_A516PageId = new Guid[] {Guid.Empty} ;
         A517PageName = "";
         A525PageType = "";
         A516PageId = Guid.Empty;
         AV8ReceptionPageId = Guid.Empty;
         AV9LocationPageId = Guid.Empty;
         AV19CalendarId = Guid.Empty;
         AV20MyActivityId = Guid.Empty;
         AV24MapsPageId = Guid.Empty;
         AV10CarePageId = Guid.Empty;
         AV11LivingPageId = Guid.Empty;
         AV12ServicesPageId = Guid.Empty;
         AV17GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context);
         AV18baseUrl = "";
         AV23BC_Trn_Location = new SdtTrn_Location(context);
         AV14SDT_MenuPage = new SdtSDT_MenuPage(context);
         AV16RowsItem = new SdtSDT_MenuPage_RowsItem(context);
         AV15TilesItem = new SdtSDT_MenuPage_RowsItem_TilesItem(context);
         GXt_SdtSDT_MenuPage_RowsItem_TilesItem1 = new SdtSDT_MenuPage_RowsItem_TilesItem(context);
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_inithomepage__default(),
            new Object[][] {
                new Object[] {
               P00BC2_A523AppVersionId, P00BC2_A29LocationId, P00BC2_n29LocationId, P00BC2_A11OrganisationId, P00BC2_n11OrganisationId
               }
               , new Object[] {
               P00BC3_A523AppVersionId, P00BC3_A517PageName, P00BC3_A525PageType, P00BC3_A516PageId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private bool n29LocationId ;
      private bool n11OrganisationId ;
      private bool returnInSub ;
      private string A517PageName ;
      private string A525PageType ;
      private string AV18baseUrl ;
      private Guid AV25AppVersionId ;
      private Guid A523AppVersionId ;
      private Guid A29LocationId ;
      private Guid A11OrganisationId ;
      private Guid AV21LocationId ;
      private Guid AV22OrganisationId ;
      private Guid A516PageId ;
      private Guid AV8ReceptionPageId ;
      private Guid AV9LocationPageId ;
      private Guid AV19CalendarId ;
      private Guid AV20MyActivityId ;
      private Guid AV24MapsPageId ;
      private Guid AV10CarePageId ;
      private Guid AV11LivingPageId ;
      private Guid AV12ServicesPageId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private SdtTrn_AppVersion_Page AV13BC_HomePage ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00BC2_A523AppVersionId ;
      private Guid[] P00BC2_A29LocationId ;
      private bool[] P00BC2_n29LocationId ;
      private Guid[] P00BC2_A11OrganisationId ;
      private bool[] P00BC2_n11OrganisationId ;
      private Guid[] P00BC3_A523AppVersionId ;
      private string[] P00BC3_A517PageName ;
      private string[] P00BC3_A525PageType ;
      private Guid[] P00BC3_A516PageId ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplication AV17GAMApplication ;
      private SdtTrn_Location AV23BC_Trn_Location ;
      private SdtSDT_MenuPage AV14SDT_MenuPage ;
      private SdtSDT_MenuPage_RowsItem AV16RowsItem ;
      private SdtSDT_MenuPage_RowsItem_TilesItem AV15TilesItem ;
      private SdtSDT_MenuPage_RowsItem_TilesItem GXt_SdtSDT_MenuPage_RowsItem_TilesItem1 ;
      private SdtTrn_AppVersion_Page aP1_BC_HomePage ;
   }

   public class prc_inithomepage__default : DataStoreHelperBase, IDataStoreHelper
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
          Object[] prmP00BC2;
          prmP00BC2 = new Object[] {
          new ParDef("AV25AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00BC3;
          prmP00BC3 = new Object[] {
          new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00BC2", "SELECT AppVersionId, LocationId, OrganisationId FROM Trn_AppVersion WHERE AppVersionId = :AV25AppVersionId ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00BC2,1, GxCacheFrequency.OFF ,true,true )
             ,new CursorDef("P00BC3", "SELECT AppVersionId, PageName, PageType, PageId FROM Trn_AppVersionPage WHERE AppVersionId = :AppVersionId ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00BC3,100, GxCacheFrequency.OFF ,false,false )
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
                ((bool[]) buf[4])[0] = rslt.wasNull(3);
                return;
             case 1 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((string[]) buf[1])[0] = rslt.getVarchar(2);
                ((string[]) buf[2])[0] = rslt.getVarchar(3);
                ((Guid[]) buf[3])[0] = rslt.getGuid(4);
                return;
       }
    }

 }

}
