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
   public class prc_contentpageapiv2 : GXProcedure
   {
      public prc_contentpageapiv2( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_contentpageapiv2( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_PageId ,
                           Guid aP1_LocationId ,
                           Guid aP2_OrganisationId ,
                           out SdtSDT_ContentPageV1 aP3_SDT_ContentPageV1 )
      {
         this.AV9PageId = aP0_PageId;
         this.AV10LocationId = aP1_LocationId;
         this.AV8OrganisationId = aP2_OrganisationId;
         this.AV20SDT_ContentPageV1 = new SdtSDT_ContentPageV1(context) ;
         initialize();
         ExecuteImpl();
         aP3_SDT_ContentPageV1=this.AV20SDT_ContentPageV1;
      }

      public SdtSDT_ContentPageV1 executeUdp( Guid aP0_PageId ,
                                              Guid aP1_LocationId ,
                                              Guid aP2_OrganisationId )
      {
         execute(aP0_PageId, aP1_LocationId, aP2_OrganisationId, out aP3_SDT_ContentPageV1);
         return AV20SDT_ContentPageV1 ;
      }

      public void executeSubmit( Guid aP0_PageId ,
                                 Guid aP1_LocationId ,
                                 Guid aP2_OrganisationId ,
                                 out SdtSDT_ContentPageV1 aP3_SDT_ContentPageV1 )
      {
         this.AV9PageId = aP0_PageId;
         this.AV10LocationId = aP1_LocationId;
         this.AV8OrganisationId = aP2_OrganisationId;
         this.AV20SDT_ContentPageV1 = new SdtSDT_ContentPageV1(context) ;
         SubmitImpl();
         aP3_SDT_ContentPageV1=this.AV20SDT_ContentPageV1;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Using cursor P00DM2 */
         pr_default.execute(0, new Object[] {AV10LocationId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A29LocationId = P00DM2_A29LocationId[0];
            n29LocationId = P00DM2_n29LocationId[0];
            A598PublishedActiveAppVersionId = P00DM2_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P00DM2_n598PublishedActiveAppVersionId[0];
            A584ActiveAppVersionId = P00DM2_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P00DM2_n584ActiveAppVersionId[0];
            A11OrganisationId = P00DM2_A11OrganisationId[0];
            n11OrganisationId = P00DM2_n11OrganisationId[0];
            AV24AppVersionId = A598PublishedActiveAppVersionId;
            if ( (Guid.Empty==AV24AppVersionId) )
            {
               AV24AppVersionId = A584ActiveAppVersionId;
            }
            pr_default.readNext(0);
         }
         pr_default.close(0);
         /* Using cursor P00DM3 */
         pr_default.execute(1, new Object[] {AV10LocationId, AV8OrganisationId, AV24AppVersionId});
         while ( (pr_default.getStatus(1) != 101) )
         {
            A523AppVersionId = P00DM3_A523AppVersionId[0];
            A598PublishedActiveAppVersionId = P00DM3_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P00DM3_n598PublishedActiveAppVersionId[0];
            A11OrganisationId = P00DM3_A11OrganisationId[0];
            n11OrganisationId = P00DM3_n11OrganisationId[0];
            A29LocationId = P00DM3_A29LocationId[0];
            n29LocationId = P00DM3_n29LocationId[0];
            A598PublishedActiveAppVersionId = P00DM3_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P00DM3_n598PublishedActiveAppVersionId[0];
            /* Using cursor P00DM4 */
            pr_default.execute(2, new Object[] {A523AppVersionId, AV9PageId});
            while ( (pr_default.getStatus(2) != 101) )
            {
               A525PageType = P00DM4_A525PageType[0];
               A516PageId = P00DM4_A516PageId[0];
               A517PageName = P00DM4_A517PageName[0];
               A536PagePublishedStructure = P00DM4_A536PagePublishedStructure[0];
               AV22PageName = A517PageName;
               AV11SDT_ContentPage.FromJSonString(A536PagePublishedStructure, null);
               if ( StringUtil.StrCmp(A525PageType, "Content") == 0 )
               {
                  AV12BC_Trn_ProductService.Load(AV9PageId, AV10LocationId, AV8OrganisationId);
                  AV28GXV1 = 1;
                  while ( AV28GXV1 <= AV11SDT_ContentPage.gxTpr_Content.Count )
                  {
                     AV15ContentItem = ((SdtSDT_ContentPage_ContentItem)AV11SDT_ContentPage.gxTpr_Content.Item(AV28GXV1));
                     if ( StringUtil.StrCmp(AV15ContentItem.gxTpr_Contenttype, "Image") == 0 )
                     {
                        AV15ContentItem.gxTpr_Contentvalue = AV12BC_Trn_ProductService.gxTpr_Productserviceimage_gxi;
                     }
                     else if ( StringUtil.StrCmp(AV15ContentItem.gxTpr_Contenttype, "Description") == 0 )
                     {
                        GXt_char1 = "";
                        new prc_getdynamictransation(context ).execute(  "Trn_ProductService",  AV9PageId,  "ProductServiceDescription",  "Dutch",  AV12BC_Trn_ProductService.gxTpr_Productservicedescription, out  GXt_char1) ;
                        AV15ContentItem.gxTpr_Contentvalue = GXt_char1;
                     }
                     else
                     {
                     }
                     AV28GXV1 = (int)(AV28GXV1+1);
                  }
                  AV29GXV2 = 1;
                  while ( AV29GXV2 <= AV11SDT_ContentPage.gxTpr_Cta.Count )
                  {
                     AV16CtaItem = ((SdtSDT_ContentPage_CtaItem)AV11SDT_ContentPage.gxTpr_Cta.Item(AV29GXV2));
                     AV13BC_Trn_CallToAction.Load(StringUtil.StrToGuid( AV16CtaItem.gxTpr_Ctaid));
                     /* Execute user subroutine: 'GETTHEMEID' */
                     S111 ();
                     if ( returnInSub )
                     {
                        pr_default.close(2);
                        cleanup();
                        if (true) return;
                     }
                     GXt_char1 = "";
                     new prc_getthemecolorbyname(context ).execute(  AV23ThemeId,  AV16CtaItem.gxTpr_Ctabgcolor, out  GXt_char1) ;
                     AV16CtaItem.gxTpr_Ctabgcolor = GXt_char1;
                     if ( StringUtil.StrCmp(AV13BC_Trn_CallToAction.gxTpr_Calltoactiontype, "Phone") == 0 )
                     {
                        AV16CtaItem.gxTpr_Ctaaction = AV13BC_Trn_CallToAction.gxTpr_Calltoactionphone;
                     }
                     else if ( StringUtil.StrCmp(AV13BC_Trn_CallToAction.gxTpr_Calltoactiontype, "Form") == 0 )
                     {
                        AV16CtaItem.gxTpr_Ctaaction = AV13BC_Trn_CallToAction.gxTpr_Calltoactionurl;
                     }
                     else if ( StringUtil.StrCmp(AV13BC_Trn_CallToAction.gxTpr_Calltoactiontype, "SiteUrl") == 0 )
                     {
                        AV16CtaItem.gxTpr_Ctaaction = AV13BC_Trn_CallToAction.gxTpr_Calltoactionurl;
                     }
                     else if ( StringUtil.StrCmp(AV13BC_Trn_CallToAction.gxTpr_Calltoactiontype, "Email") == 0 )
                     {
                        AV16CtaItem.gxTpr_Ctaaction = AV13BC_Trn_CallToAction.gxTpr_Calltoactionemail;
                     }
                     else
                     {
                     }
                     AV29GXV2 = (int)(AV29GXV2+1);
                  }
               }
               if ( ( ( StringUtil.StrCmp(A525PageType, "Location") == 0 ) ) || ( ( StringUtil.StrCmp(A525PageType, "Reception") == 0 ) ) )
               {
                  if ( StringUtil.StrCmp(A525PageType, "Location") == 0 )
                  {
                     AV14BC_Trn_Location.Load(AV10LocationId, AV8OrganisationId);
                     AV11SDT_ContentPage.gxTpr_Content.Clear();
                     AV15ContentItem = new SdtSDT_ContentPage_ContentItem(context);
                     AV15ContentItem.gxTpr_Contenttype = "Image";
                     AV15ContentItem.gxTpr_Contentvalue = AV14BC_Trn_Location.gxTpr_Locationimage_gxi;
                     AV11SDT_ContentPage.gxTpr_Content.Add(AV15ContentItem, 0);
                     AV15ContentItem = new SdtSDT_ContentPage_ContentItem(context);
                     AV15ContentItem.gxTpr_Contenttype = "Description";
                     GXt_char1 = "";
                     new prc_getdynamictransation(context ).execute(  "Trn_Location",  AV10LocationId,  "LocationDescription",  "Dutch",  AV14BC_Trn_Location.gxTpr_Locationdescription, out  GXt_char1) ;
                     AV15ContentItem.gxTpr_Contentvalue = GXt_char1;
                     AV11SDT_ContentPage.gxTpr_Content.Add(AV15ContentItem, 0);
                  }
                  if ( StringUtil.StrCmp(A525PageType, "Reception") == 0 )
                  {
                     AV14BC_Trn_Location.Load(AV10LocationId, AV8OrganisationId);
                     AV11SDT_ContentPage.gxTpr_Content.Clear();
                     AV15ContentItem = new SdtSDT_ContentPage_ContentItem(context);
                     AV15ContentItem.gxTpr_Contenttype = "Image";
                     AV15ContentItem.gxTpr_Contentvalue = AV14BC_Trn_Location.gxTpr_Receptionimage_gxi;
                     AV11SDT_ContentPage.gxTpr_Content.Add(AV15ContentItem, 0);
                     AV15ContentItem = new SdtSDT_ContentPage_ContentItem(context);
                     AV15ContentItem.gxTpr_Contenttype = "Description";
                     GXt_char1 = "";
                     new prc_getdynamictransation(context ).execute(  "Trn_Location",  AV10LocationId,  "ReceptionDescription",  "Dutch",  AV14BC_Trn_Location.gxTpr_Receptiondescription, out  GXt_char1) ;
                     AV15ContentItem.gxTpr_Contentvalue = GXt_char1;
                     AV11SDT_ContentPage.gxTpr_Content.Add(AV15ContentItem, 0);
                  }
                  AV30GXV3 = 1;
                  while ( AV30GXV3 <= AV11SDT_ContentPage.gxTpr_Cta.Count )
                  {
                     AV16CtaItem = ((SdtSDT_ContentPage_CtaItem)AV11SDT_ContentPage.gxTpr_Cta.Item(AV30GXV3));
                     AV13BC_Trn_CallToAction.Load(StringUtil.StrToGuid( AV16CtaItem.gxTpr_Ctaid));
                     /* Execute user subroutine: 'GETTHEMEID' */
                     S111 ();
                     if ( returnInSub )
                     {
                        pr_default.close(2);
                        cleanup();
                        if (true) return;
                     }
                     GXt_char1 = "";
                     new prc_getthemecolorbyname(context ).execute(  AV23ThemeId,  AV16CtaItem.gxTpr_Ctabgcolor, out  GXt_char1) ;
                     AV16CtaItem.gxTpr_Ctabgcolor = GXt_char1;
                     if ( StringUtil.StrCmp(AV13BC_Trn_CallToAction.gxTpr_Calltoactiontype, "Phone") == 0 )
                     {
                        AV16CtaItem.gxTpr_Ctaaction = AV14BC_Trn_Location.gxTpr_Locationphone;
                     }
                     else if ( StringUtil.StrCmp(AV13BC_Trn_CallToAction.gxTpr_Calltoactiontype, "Email") == 0 )
                     {
                        AV16CtaItem.gxTpr_Ctaaction = AV14BC_Trn_Location.gxTpr_Locationemail;
                     }
                     else
                     {
                     }
                     AV30GXV3 = (int)(AV30GXV3+1);
                  }
               }
               new prc_logtoserver(context ).execute(  "Content:3 "+AV11SDT_ContentPage.ToJSonString(false, true)) ;
               GXt_SdtSDT_ContentPageV12 = AV20SDT_ContentPageV1;
               new prc_convertnewtooldcontentstructure(context ).execute(  AV11SDT_ContentPage,  AV9PageId,  AV22PageName, out  GXt_SdtSDT_ContentPageV12) ;
               AV20SDT_ContentPageV1 = GXt_SdtSDT_ContentPageV12;
               new prc_logtoserver(context ).execute(  "Content:3 "+AV20SDT_ContentPageV1.ToJSonString(false, true)) ;
               /* Exiting from a For First loop. */
               if (true) break;
            }
            pr_default.close(2);
            pr_default.readNext(1);
         }
         pr_default.close(1);
         cleanup();
      }

      protected void S111( )
      {
         /* 'GETTHEMEID' Routine */
         returnInSub = false;
         /* Using cursor P00DM5 */
         pr_default.execute(3, new Object[] {AV10LocationId});
         while ( (pr_default.getStatus(3) != 101) )
         {
            A29LocationId = P00DM5_A29LocationId[0];
            n29LocationId = P00DM5_n29LocationId[0];
            A273Trn_ThemeId = P00DM5_A273Trn_ThemeId[0];
            n273Trn_ThemeId = P00DM5_n273Trn_ThemeId[0];
            A11OrganisationId = P00DM5_A11OrganisationId[0];
            n11OrganisationId = P00DM5_n11OrganisationId[0];
            AV23ThemeId = A273Trn_ThemeId;
            pr_default.readNext(3);
         }
         pr_default.close(3);
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
         AV20SDT_ContentPageV1 = new SdtSDT_ContentPageV1(context);
         P00DM2_A29LocationId = new Guid[] {Guid.Empty} ;
         P00DM2_n29LocationId = new bool[] {false} ;
         P00DM2_A598PublishedActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P00DM2_n598PublishedActiveAppVersionId = new bool[] {false} ;
         P00DM2_A584ActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P00DM2_n584ActiveAppVersionId = new bool[] {false} ;
         P00DM2_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00DM2_n11OrganisationId = new bool[] {false} ;
         A29LocationId = Guid.Empty;
         A598PublishedActiveAppVersionId = Guid.Empty;
         A584ActiveAppVersionId = Guid.Empty;
         A11OrganisationId = Guid.Empty;
         AV24AppVersionId = Guid.Empty;
         P00DM3_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00DM3_A598PublishedActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P00DM3_n598PublishedActiveAppVersionId = new bool[] {false} ;
         P00DM3_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00DM3_n11OrganisationId = new bool[] {false} ;
         P00DM3_A29LocationId = new Guid[] {Guid.Empty} ;
         P00DM3_n29LocationId = new bool[] {false} ;
         A523AppVersionId = Guid.Empty;
         P00DM4_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00DM4_A525PageType = new string[] {""} ;
         P00DM4_A516PageId = new Guid[] {Guid.Empty} ;
         P00DM4_A517PageName = new string[] {""} ;
         P00DM4_A536PagePublishedStructure = new string[] {""} ;
         A525PageType = "";
         A516PageId = Guid.Empty;
         A517PageName = "";
         A536PagePublishedStructure = "";
         AV22PageName = "";
         AV11SDT_ContentPage = new SdtSDT_ContentPage(context);
         AV12BC_Trn_ProductService = new SdtTrn_ProductService(context);
         AV15ContentItem = new SdtSDT_ContentPage_ContentItem(context);
         AV16CtaItem = new SdtSDT_ContentPage_CtaItem(context);
         AV13BC_Trn_CallToAction = new SdtTrn_CallToAction(context);
         AV23ThemeId = Guid.Empty;
         AV14BC_Trn_Location = new SdtTrn_Location(context);
         GXt_char1 = "";
         GXt_SdtSDT_ContentPageV12 = new SdtSDT_ContentPageV1(context);
         P00DM5_A29LocationId = new Guid[] {Guid.Empty} ;
         P00DM5_n29LocationId = new bool[] {false} ;
         P00DM5_A273Trn_ThemeId = new Guid[] {Guid.Empty} ;
         P00DM5_n273Trn_ThemeId = new bool[] {false} ;
         P00DM5_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00DM5_n11OrganisationId = new bool[] {false} ;
         A273Trn_ThemeId = Guid.Empty;
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_contentpageapiv2__default(),
            new Object[][] {
                new Object[] {
               P00DM2_A29LocationId, P00DM2_A598PublishedActiveAppVersionId, P00DM2_n598PublishedActiveAppVersionId, P00DM2_A584ActiveAppVersionId, P00DM2_n584ActiveAppVersionId, P00DM2_A11OrganisationId
               }
               , new Object[] {
               P00DM3_A523AppVersionId, P00DM3_A598PublishedActiveAppVersionId, P00DM3_n598PublishedActiveAppVersionId, P00DM3_A11OrganisationId, P00DM3_n11OrganisationId, P00DM3_A29LocationId, P00DM3_n29LocationId
               }
               , new Object[] {
               P00DM4_A523AppVersionId, P00DM4_A525PageType, P00DM4_A516PageId, P00DM4_A517PageName, P00DM4_A536PagePublishedStructure
               }
               , new Object[] {
               P00DM5_A29LocationId, P00DM5_A273Trn_ThemeId, P00DM5_n273Trn_ThemeId, P00DM5_A11OrganisationId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private int AV28GXV1 ;
      private int AV29GXV2 ;
      private int AV30GXV3 ;
      private string GXt_char1 ;
      private bool n29LocationId ;
      private bool n598PublishedActiveAppVersionId ;
      private bool n584ActiveAppVersionId ;
      private bool n11OrganisationId ;
      private bool returnInSub ;
      private bool n273Trn_ThemeId ;
      private string A536PagePublishedStructure ;
      private string A525PageType ;
      private string A517PageName ;
      private string AV22PageName ;
      private Guid AV9PageId ;
      private Guid AV10LocationId ;
      private Guid AV8OrganisationId ;
      private Guid A29LocationId ;
      private Guid A598PublishedActiveAppVersionId ;
      private Guid A584ActiveAppVersionId ;
      private Guid A11OrganisationId ;
      private Guid AV24AppVersionId ;
      private Guid A523AppVersionId ;
      private Guid A516PageId ;
      private Guid AV23ThemeId ;
      private Guid A273Trn_ThemeId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private SdtSDT_ContentPageV1 AV20SDT_ContentPageV1 ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00DM2_A29LocationId ;
      private bool[] P00DM2_n29LocationId ;
      private Guid[] P00DM2_A598PublishedActiveAppVersionId ;
      private bool[] P00DM2_n598PublishedActiveAppVersionId ;
      private Guid[] P00DM2_A584ActiveAppVersionId ;
      private bool[] P00DM2_n584ActiveAppVersionId ;
      private Guid[] P00DM2_A11OrganisationId ;
      private bool[] P00DM2_n11OrganisationId ;
      private Guid[] P00DM3_A523AppVersionId ;
      private Guid[] P00DM3_A598PublishedActiveAppVersionId ;
      private bool[] P00DM3_n598PublishedActiveAppVersionId ;
      private Guid[] P00DM3_A11OrganisationId ;
      private bool[] P00DM3_n11OrganisationId ;
      private Guid[] P00DM3_A29LocationId ;
      private bool[] P00DM3_n29LocationId ;
      private Guid[] P00DM4_A523AppVersionId ;
      private string[] P00DM4_A525PageType ;
      private Guid[] P00DM4_A516PageId ;
      private string[] P00DM4_A517PageName ;
      private string[] P00DM4_A536PagePublishedStructure ;
      private SdtSDT_ContentPage AV11SDT_ContentPage ;
      private SdtTrn_ProductService AV12BC_Trn_ProductService ;
      private SdtSDT_ContentPage_ContentItem AV15ContentItem ;
      private SdtSDT_ContentPage_CtaItem AV16CtaItem ;
      private SdtTrn_CallToAction AV13BC_Trn_CallToAction ;
      private SdtTrn_Location AV14BC_Trn_Location ;
      private SdtSDT_ContentPageV1 GXt_SdtSDT_ContentPageV12 ;
      private Guid[] P00DM5_A29LocationId ;
      private bool[] P00DM5_n29LocationId ;
      private Guid[] P00DM5_A273Trn_ThemeId ;
      private bool[] P00DM5_n273Trn_ThemeId ;
      private Guid[] P00DM5_A11OrganisationId ;
      private bool[] P00DM5_n11OrganisationId ;
      private SdtSDT_ContentPageV1 aP3_SDT_ContentPageV1 ;
   }

   public class prc_contentpageapiv2__default : DataStoreHelperBase, IDataStoreHelper
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
          Object[] prmP00DM2;
          prmP00DM2 = new Object[] {
          new ParDef("AV10LocationId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00DM3;
          prmP00DM3 = new Object[] {
          new ParDef("AV10LocationId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV8OrganisationId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV24AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00DM4;
          prmP00DM4 = new Object[] {
          new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV9PageId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00DM5;
          prmP00DM5 = new Object[] {
          new ParDef("AV10LocationId",GXType.UniqueIdentifier,36,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00DM2", "SELECT LocationId, PublishedActiveAppVersionId, ActiveAppVersionId, OrganisationId FROM Trn_Location WHERE LocationId = :AV10LocationId ORDER BY LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00DM2,100, GxCacheFrequency.OFF ,false,false )
             ,new CursorDef("P00DM3", "SELECT T1.AppVersionId, T2.PublishedActiveAppVersionId, T1.OrganisationId, T1.LocationId FROM (Trn_AppVersion T1 LEFT JOIN Trn_Location T2 ON T2.LocationId = T1.LocationId AND T2.OrganisationId = T1.OrganisationId) WHERE (T1.LocationId = :AV10LocationId and T1.OrganisationId = :AV8OrganisationId) AND (T2.PublishedActiveAppVersionId = :AV24AppVersionId) ORDER BY T1.LocationId, T1.OrganisationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00DM3,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00DM4", "SELECT AppVersionId, PageType, PageId, PageName, PagePublishedStructure FROM Trn_AppVersionPage WHERE (AppVersionId = :AppVersionId and PageId = :AV9PageId) AND (( PageType = ( 'Content')) or ( PageType = ( 'Reception')) or ( PageType = ( 'Location'))) ORDER BY AppVersionId, PageId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00DM4,1, GxCacheFrequency.OFF ,true,true )
             ,new CursorDef("P00DM5", "SELECT LocationId, Trn_ThemeId, OrganisationId FROM Trn_Location WHERE LocationId = :AV10LocationId ORDER BY LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00DM5,100, GxCacheFrequency.OFF ,false,false )
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
                ((Guid[]) buf[5])[0] = rslt.getGuid(4);
                ((bool[]) buf[6])[0] = rslt.wasNull(4);
                return;
             case 2 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((string[]) buf[1])[0] = rslt.getVarchar(2);
                ((Guid[]) buf[2])[0] = rslt.getGuid(3);
                ((string[]) buf[3])[0] = rslt.getVarchar(4);
                ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
                return;
             case 3 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((Guid[]) buf[1])[0] = rslt.getGuid(2);
                ((bool[]) buf[2])[0] = rslt.wasNull(2);
                ((Guid[]) buf[3])[0] = rslt.getGuid(3);
                return;
       }
    }

 }

}
