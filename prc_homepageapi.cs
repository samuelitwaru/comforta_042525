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
   public class prc_homepageapi : GXProcedure
   {
      public prc_homepageapi( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_homepageapi( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_LocationId ,
                           Guid aP1_OrganisationId ,
                           string aP2_UserId ,
                           out SdtSDT_InfoPage aP3_SDT_InfoPage )
      {
         this.AV8LocationId = aP0_LocationId;
         this.AV9OrganisationId = aP1_OrganisationId;
         this.AV10UserId = aP2_UserId;
         this.AV11SDT_InfoPage = new SdtSDT_InfoPage(context) ;
         initialize();
         ExecuteImpl();
         aP3_SDT_InfoPage=this.AV11SDT_InfoPage;
      }

      public SdtSDT_InfoPage executeUdp( Guid aP0_LocationId ,
                                         Guid aP1_OrganisationId ,
                                         string aP2_UserId )
      {
         execute(aP0_LocationId, aP1_OrganisationId, aP2_UserId, out aP3_SDT_InfoPage);
         return AV11SDT_InfoPage ;
      }

      public void executeSubmit( Guid aP0_LocationId ,
                                 Guid aP1_OrganisationId ,
                                 string aP2_UserId ,
                                 out SdtSDT_InfoPage aP3_SDT_InfoPage )
      {
         this.AV8LocationId = aP0_LocationId;
         this.AV9OrganisationId = aP1_OrganisationId;
         this.AV10UserId = aP2_UserId;
         this.AV11SDT_InfoPage = new SdtSDT_InfoPage(context) ;
         SubmitImpl();
         aP3_SDT_InfoPage=this.AV11SDT_InfoPage;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Using cursor P00GG2 */
         pr_default.execute(0, new Object[] {AV8LocationId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A29LocationId = P00GG2_A29LocationId[0];
            n29LocationId = P00GG2_n29LocationId[0];
            A598PublishedActiveAppVersionId = P00GG2_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P00GG2_n598PublishedActiveAppVersionId[0];
            A584ActiveAppVersionId = P00GG2_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P00GG2_n584ActiveAppVersionId[0];
            A11OrganisationId = P00GG2_A11OrganisationId[0];
            n11OrganisationId = P00GG2_n11OrganisationId[0];
            AV12AppVersionId = A598PublishedActiveAppVersionId;
            if ( (Guid.Empty==AV12AppVersionId) )
            {
               AV12AppVersionId = A584ActiveAppVersionId;
            }
            pr_default.readNext(0);
         }
         pr_default.close(0);
         /* Using cursor P00GG3 */
         pr_default.execute(1, new Object[] {AV8LocationId, AV9OrganisationId, AV12AppVersionId});
         while ( (pr_default.getStatus(1) != 101) )
         {
            A523AppVersionId = P00GG3_A523AppVersionId[0];
            A11OrganisationId = P00GG3_A11OrganisationId[0];
            n11OrganisationId = P00GG3_n11OrganisationId[0];
            A29LocationId = P00GG3_A29LocationId[0];
            n29LocationId = P00GG3_n29LocationId[0];
            /* Using cursor P00GG4 */
            pr_default.execute(2, new Object[] {A523AppVersionId});
            while ( (pr_default.getStatus(2) != 101) )
            {
               A517PageName = P00GG4_A517PageName[0];
               A536PagePublishedStructure = P00GG4_A536PagePublishedStructure[0];
               A516PageId = P00GG4_A516PageId[0];
               AV11SDT_InfoPage = new SdtSDT_InfoPage(context);
               AV11SDT_InfoPage.FromJSonString(A536PagePublishedStructure, null);
               AV11SDT_InfoPage.gxTpr_Pageid = A516PageId;
               AV11SDT_InfoPage.gxTpr_Pagename = A517PageName;
               pr_default.readNext(2);
            }
            pr_default.close(2);
            pr_default.readNext(1);
         }
         pr_default.close(1);
         /* Execute user subroutine: 'GETTHEMEID' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         AV20GXV1 = 1;
         while ( AV20GXV1 <= AV11SDT_InfoPage.gxTpr_Infocontent.Count )
         {
            AV14InfoContent = ((SdtSDT_InfoPage_InfoContentItem)AV11SDT_InfoPage.gxTpr_Infocontent.Item(AV20GXV1));
            if ( StringUtil.StrCmp(AV14InfoContent.gxTpr_Infotype, "Image") == 0 )
            {
               AV14InfoContent.gxTpr_Images.Add(AV14InfoContent.gxTpr_Infovalue, 0);
            }
            else if ( StringUtil.StrCmp(AV14InfoContent.gxTpr_Infotype, "Cta") == 0 )
            {
               new prc_logtoserver(context ).execute(  context.GetMessage( "ThemeId: ", "")+AV15ThemeId.ToString()) ;
               GXt_char1 = "";
               new prc_getthemecolorbyname(context ).execute(  AV15ThemeId,  AV14InfoContent.gxTpr_Ctaattributes.gxTpr_Ctacolor, out  GXt_char1) ;
               AV14InfoContent.gxTpr_Ctaattributes.gxTpr_Ctacolor = GXt_char1;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV14InfoContent.gxTpr_Ctaattributes.gxTpr_Ctabgcolor))) )
               {
                  AV14InfoContent.gxTpr_Ctaattributes.gxTpr_Ctabgcolor = context.GetMessage( "CtaColorOne", "");
               }
               GXt_char1 = "";
               new prc_getthemecolorbyname(context ).execute(  AV15ThemeId,  AV14InfoContent.gxTpr_Ctaattributes.gxTpr_Ctabgcolor, out  GXt_char1) ;
               AV14InfoContent.gxTpr_Ctaattributes.gxTpr_Ctabgcolor = GXt_char1;
               new prc_logtoserver(context ).execute(  context.GetMessage( "CTA: ", "")+AV14InfoContent.ToJSonString(false, true)) ;
            }
            else if ( StringUtil.StrCmp(AV14InfoContent.gxTpr_Infotype, "TileRow") == 0 )
            {
               AV21GXV2 = 1;
               while ( AV21GXV2 <= AV14InfoContent.gxTpr_Tiles.Count )
               {
                  AV16SDT_InfoTile = ((SdtSDT_InfoTile_SDT_InfoTileItem)AV14InfoContent.gxTpr_Tiles.Item(AV21GXV2));
                  GXt_char1 = "";
                  new prc_getthemecolorbyname(context ).execute(  AV15ThemeId,  AV16SDT_InfoTile.gxTpr_Bgcolor, out  GXt_char1) ;
                  AV16SDT_InfoTile.gxTpr_Bgcolor = GXt_char1;
                  AV16SDT_InfoTile.gxTpr_Size = (decimal)(((AV16SDT_InfoTile.gxTpr_Size==Convert.ToDecimal(0)) ? 80 : (short)(Math.Round(AV16SDT_InfoTile.gxTpr_Size, 18, MidpointRounding.ToEven))));
                  AV16SDT_InfoTile.gxTpr_Size = (decimal)(AV16SDT_InfoTile.gxTpr_Size/ (decimal)(80));
                  if ( StringUtil.StrCmp(AV16SDT_InfoTile.gxTpr_Action.gxTpr_Objecttype, "DynamicForm") == 0 )
                  {
                     /* Using cursor P00GG5 */
                     pr_default.execute(3, new Object[] {AV16SDT_InfoTile.gxTpr_Action.gxTpr_Objectid});
                     while ( (pr_default.getStatus(3) != 101) )
                     {
                        A206WWPFormId = P00GG5_A206WWPFormId[0];
                        A208WWPFormReferenceName = P00GG5_A208WWPFormReferenceName[0];
                        A207WWPFormVersionNumber = P00GG5_A207WWPFormVersionNumber[0];
                        AV16SDT_InfoTile.gxTpr_Action.gxTpr_Objecturl = A367CallToActionUrl;
                        GXt_char1 = "";
                        GXt_char2 = context.GetMessage( "Form", "");
                        new prc_getcalltoactionformurl(context ).execute( ref  GXt_char2, ref  A208WWPFormReferenceName, out  GXt_char1) ;
                        AV16SDT_InfoTile.gxTpr_Action.gxTpr_Objecturl = GXt_char1;
                        pr_default.readNext(3);
                     }
                     pr_default.close(3);
                  }
                  AV21GXV2 = (int)(AV21GXV2+1);
               }
            }
            else
            {
            }
            AV20GXV1 = (int)(AV20GXV1+1);
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'GETTHEMEID' Routine */
         returnInSub = false;
         /* Using cursor P00GG6 */
         pr_default.execute(4, new Object[] {AV8LocationId});
         while ( (pr_default.getStatus(4) != 101) )
         {
            A29LocationId = P00GG6_A29LocationId[0];
            n29LocationId = P00GG6_n29LocationId[0];
            A273Trn_ThemeId = P00GG6_A273Trn_ThemeId[0];
            n273Trn_ThemeId = P00GG6_n273Trn_ThemeId[0];
            A11OrganisationId = P00GG6_A11OrganisationId[0];
            n11OrganisationId = P00GG6_n11OrganisationId[0];
            AV15ThemeId = A273Trn_ThemeId;
            pr_default.readNext(4);
         }
         pr_default.close(4);
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
         AV11SDT_InfoPage = new SdtSDT_InfoPage(context);
         P00GG2_A29LocationId = new Guid[] {Guid.Empty} ;
         P00GG2_n29LocationId = new bool[] {false} ;
         P00GG2_A598PublishedActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P00GG2_n598PublishedActiveAppVersionId = new bool[] {false} ;
         P00GG2_A584ActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P00GG2_n584ActiveAppVersionId = new bool[] {false} ;
         P00GG2_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00GG2_n11OrganisationId = new bool[] {false} ;
         A29LocationId = Guid.Empty;
         A598PublishedActiveAppVersionId = Guid.Empty;
         A584ActiveAppVersionId = Guid.Empty;
         A11OrganisationId = Guid.Empty;
         AV12AppVersionId = Guid.Empty;
         P00GG3_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00GG3_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00GG3_n11OrganisationId = new bool[] {false} ;
         P00GG3_A29LocationId = new Guid[] {Guid.Empty} ;
         P00GG3_n29LocationId = new bool[] {false} ;
         A523AppVersionId = Guid.Empty;
         P00GG4_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00GG4_A517PageName = new string[] {""} ;
         P00GG4_A536PagePublishedStructure = new string[] {""} ;
         P00GG4_A516PageId = new Guid[] {Guid.Empty} ;
         A517PageName = "";
         A536PagePublishedStructure = "";
         A516PageId = Guid.Empty;
         AV14InfoContent = new SdtSDT_InfoPage_InfoContentItem(context);
         AV15ThemeId = Guid.Empty;
         AV16SDT_InfoTile = new SdtSDT_InfoTile_SDT_InfoTileItem(context);
         P00GG5_A206WWPFormId = new short[1] ;
         P00GG5_A208WWPFormReferenceName = new string[] {""} ;
         P00GG5_A207WWPFormVersionNumber = new short[1] ;
         A208WWPFormReferenceName = "";
         A367CallToActionUrl = "";
         GXt_char1 = "";
         GXt_char2 = "";
         P00GG6_A29LocationId = new Guid[] {Guid.Empty} ;
         P00GG6_n29LocationId = new bool[] {false} ;
         P00GG6_A273Trn_ThemeId = new Guid[] {Guid.Empty} ;
         P00GG6_n273Trn_ThemeId = new bool[] {false} ;
         P00GG6_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00GG6_n11OrganisationId = new bool[] {false} ;
         A273Trn_ThemeId = Guid.Empty;
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_homepageapi__default(),
            new Object[][] {
                new Object[] {
               P00GG2_A29LocationId, P00GG2_A598PublishedActiveAppVersionId, P00GG2_n598PublishedActiveAppVersionId, P00GG2_A584ActiveAppVersionId, P00GG2_n584ActiveAppVersionId, P00GG2_A11OrganisationId
               }
               , new Object[] {
               P00GG3_A523AppVersionId, P00GG3_A11OrganisationId, P00GG3_n11OrganisationId, P00GG3_A29LocationId, P00GG3_n29LocationId
               }
               , new Object[] {
               P00GG4_A523AppVersionId, P00GG4_A517PageName, P00GG4_A536PagePublishedStructure, P00GG4_A516PageId
               }
               , new Object[] {
               P00GG5_A206WWPFormId, P00GG5_A208WWPFormReferenceName, P00GG5_A207WWPFormVersionNumber
               }
               , new Object[] {
               P00GG6_A29LocationId, P00GG6_A273Trn_ThemeId, P00GG6_n273Trn_ThemeId, P00GG6_A11OrganisationId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short A206WWPFormId ;
      private short A207WWPFormVersionNumber ;
      private int AV20GXV1 ;
      private int AV21GXV2 ;
      private string GXt_char1 ;
      private string GXt_char2 ;
      private bool n29LocationId ;
      private bool n598PublishedActiveAppVersionId ;
      private bool n584ActiveAppVersionId ;
      private bool n11OrganisationId ;
      private bool returnInSub ;
      private bool n273Trn_ThemeId ;
      private string A536PagePublishedStructure ;
      private string AV10UserId ;
      private string A517PageName ;
      private string A208WWPFormReferenceName ;
      private string A367CallToActionUrl ;
      private Guid AV8LocationId ;
      private Guid AV9OrganisationId ;
      private Guid A29LocationId ;
      private Guid A598PublishedActiveAppVersionId ;
      private Guid A584ActiveAppVersionId ;
      private Guid A11OrganisationId ;
      private Guid AV12AppVersionId ;
      private Guid A523AppVersionId ;
      private Guid A516PageId ;
      private Guid AV15ThemeId ;
      private Guid A273Trn_ThemeId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private SdtSDT_InfoPage AV11SDT_InfoPage ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00GG2_A29LocationId ;
      private bool[] P00GG2_n29LocationId ;
      private Guid[] P00GG2_A598PublishedActiveAppVersionId ;
      private bool[] P00GG2_n598PublishedActiveAppVersionId ;
      private Guid[] P00GG2_A584ActiveAppVersionId ;
      private bool[] P00GG2_n584ActiveAppVersionId ;
      private Guid[] P00GG2_A11OrganisationId ;
      private bool[] P00GG2_n11OrganisationId ;
      private Guid[] P00GG3_A523AppVersionId ;
      private Guid[] P00GG3_A11OrganisationId ;
      private bool[] P00GG3_n11OrganisationId ;
      private Guid[] P00GG3_A29LocationId ;
      private bool[] P00GG3_n29LocationId ;
      private Guid[] P00GG4_A523AppVersionId ;
      private string[] P00GG4_A517PageName ;
      private string[] P00GG4_A536PagePublishedStructure ;
      private Guid[] P00GG4_A516PageId ;
      private SdtSDT_InfoPage_InfoContentItem AV14InfoContent ;
      private SdtSDT_InfoTile_SDT_InfoTileItem AV16SDT_InfoTile ;
      private short[] P00GG5_A206WWPFormId ;
      private string[] P00GG5_A208WWPFormReferenceName ;
      private short[] P00GG5_A207WWPFormVersionNumber ;
      private Guid[] P00GG6_A29LocationId ;
      private bool[] P00GG6_n29LocationId ;
      private Guid[] P00GG6_A273Trn_ThemeId ;
      private bool[] P00GG6_n273Trn_ThemeId ;
      private Guid[] P00GG6_A11OrganisationId ;
      private bool[] P00GG6_n11OrganisationId ;
      private SdtSDT_InfoPage aP3_SDT_InfoPage ;
   }

   public class prc_homepageapi__default : DataStoreHelperBase, IDataStoreHelper
   {
      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
         ,new ForEachCursor(def[1])
         ,new ForEachCursor(def[2])
         ,new ForEachCursor(def[3])
         ,new ForEachCursor(def[4])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP00GG2;
          prmP00GG2 = new Object[] {
          new ParDef("AV8LocationId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00GG3;
          prmP00GG3 = new Object[] {
          new ParDef("AV8LocationId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV9OrganisationId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV12AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00GG4;
          prmP00GG4 = new Object[] {
          new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00GG5;
          prmP00GG5 = new Object[] {
          new ParDef("AV16SDT__1Action_1Objectid",GXType.VarChar,100,0)
          };
          Object[] prmP00GG6;
          prmP00GG6 = new Object[] {
          new ParDef("AV8LocationId",GXType.UniqueIdentifier,36,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00GG2", "SELECT LocationId, PublishedActiveAppVersionId, ActiveAppVersionId, OrganisationId FROM Trn_Location WHERE LocationId = :AV8LocationId ORDER BY LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00GG2,100, GxCacheFrequency.OFF ,false,false )
             ,new CursorDef("P00GG3", "SELECT AppVersionId, OrganisationId, LocationId FROM Trn_AppVersion WHERE (LocationId = :AV8LocationId and OrganisationId = :AV9OrganisationId) AND (AppVersionId = :AV12AppVersionId) ORDER BY LocationId, OrganisationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00GG3,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00GG4", "SELECT AppVersionId, PageName, PagePublishedStructure, PageId FROM Trn_AppVersionPage WHERE (AppVersionId = :AppVersionId) AND (LOWER(PageName) = ( 'home')) ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00GG4,100, GxCacheFrequency.OFF ,false,false )
             ,new CursorDef("P00GG5", "SELECT WWPFormId, WWPFormReferenceName, WWPFormVersionNumber FROM WWP_Form WHERE WWPFormId = TO_NUMBER(0 || :AV16SDT__1Action_1Objectid,'9999999999999999999999999999.99999999999999') ORDER BY WWPFormId, WWPFormVersionNumber ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00GG5,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00GG6", "SELECT LocationId, Trn_ThemeId, OrganisationId FROM Trn_Location WHERE LocationId = :AV8LocationId ORDER BY LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00GG6,100, GxCacheFrequency.OFF ,false,false )
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
                ((Guid[]) buf[5])[0] = rslt.getGuid(4);
                return;
             case 1 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((Guid[]) buf[1])[0] = rslt.getGuid(2);
                ((bool[]) buf[2])[0] = rslt.wasNull(2);
                ((Guid[]) buf[3])[0] = rslt.getGuid(3);
                ((bool[]) buf[4])[0] = rslt.wasNull(3);
                return;
             case 2 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((string[]) buf[1])[0] = rslt.getVarchar(2);
                ((string[]) buf[2])[0] = rslt.getLongVarchar(3);
                ((Guid[]) buf[3])[0] = rslt.getGuid(4);
                return;
             case 3 :
                ((short[]) buf[0])[0] = rslt.getShort(1);
                ((string[]) buf[1])[0] = rslt.getVarchar(2);
                ((short[]) buf[2])[0] = rslt.getShort(3);
                return;
             case 4 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((Guid[]) buf[1])[0] = rslt.getGuid(2);
                ((bool[]) buf[2])[0] = rslt.wasNull(2);
                ((Guid[]) buf[3])[0] = rslt.getGuid(3);
                return;
       }
    }

 }

}
