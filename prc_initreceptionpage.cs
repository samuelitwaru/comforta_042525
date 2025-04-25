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
   public class prc_initreceptionpage : GXProcedure
   {
      public prc_initreceptionpage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_initreceptionpage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( ref Guid aP0_LocationId ,
                           ref Guid aP1_OrganisationId ,
                           ref Guid aP2_AppVersionId ,
                           out SdtTrn_AppVersion_Page aP3_BC_Page )
      {
         this.AV13LocationId = aP0_LocationId;
         this.AV14OrganisationId = aP1_OrganisationId;
         this.AV15AppVersionId = aP2_AppVersionId;
         this.AV8BC_Page = new SdtTrn_AppVersion_Page(context) ;
         initialize();
         ExecuteImpl();
         aP0_LocationId=this.AV13LocationId;
         aP1_OrganisationId=this.AV14OrganisationId;
         aP2_AppVersionId=this.AV15AppVersionId;
         aP3_BC_Page=this.AV8BC_Page;
      }

      public SdtTrn_AppVersion_Page executeUdp( ref Guid aP0_LocationId ,
                                                ref Guid aP1_OrganisationId ,
                                                ref Guid aP2_AppVersionId )
      {
         execute(ref aP0_LocationId, ref aP1_OrganisationId, ref aP2_AppVersionId, out aP3_BC_Page);
         return AV8BC_Page ;
      }

      public void executeSubmit( ref Guid aP0_LocationId ,
                                 ref Guid aP1_OrganisationId ,
                                 ref Guid aP2_AppVersionId ,
                                 out SdtTrn_AppVersion_Page aP3_BC_Page )
      {
         this.AV13LocationId = aP0_LocationId;
         this.AV14OrganisationId = aP1_OrganisationId;
         this.AV15AppVersionId = aP2_AppVersionId;
         this.AV8BC_Page = new SdtTrn_AppVersion_Page(context) ;
         SubmitImpl();
         aP0_LocationId=this.AV13LocationId;
         aP1_OrganisationId=this.AV14OrganisationId;
         aP2_AppVersionId=this.AV15AppVersionId;
         aP3_BC_Page=this.AV8BC_Page;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new prc_logtoserver(context ).execute(  "AppVersionId: "+AV15AppVersionId.ToString()) ;
         /* Using cursor P00B92 */
         pr_default.execute(0, new Object[] {AV15AppVersionId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A523AppVersionId = P00B92_A523AppVersionId[0];
            AV17GXLvl5 = 0;
            /* Using cursor P00B93 */
            pr_default.execute(1, new Object[] {A523AppVersionId});
            while ( (pr_default.getStatus(1) != 101) )
            {
               A517PageName = P00B93_A517PageName[0];
               A516PageId = P00B93_A516PageId[0];
               AV17GXLvl5 = 1;
               AV8BC_Page.gxTpr_Pageid = A516PageId;
               pr_default.readNext(1);
            }
            pr_default.close(1);
            if ( AV17GXLvl5 == 0 )
            {
               AV8BC_Page.gxTpr_Pageid = Guid.NewGuid( );
            }
            /* Exiting from a For First loop. */
            if (true) break;
         }
         pr_default.close(0);
         AV9BC_Trn_Location.Load(AV13LocationId, AV14OrganisationId);
         AV8BC_Page.gxTpr_Pagename = "Reception";
         AV8BC_Page.gxTpr_Ispredefined = true;
         AV8BC_Page.gxTpr_Pagetype = "Reception";
         AV11ContentItem = new SdtSDT_ContentPage_ContentItem(context);
         AV11ContentItem.gxTpr_Contentid = new SdtRandomStringGenerator(context).generate(12);
         AV11ContentItem.gxTpr_Contenttype = "Image";
         AV11ContentItem.gxTpr_Contentvalue = AV9BC_Trn_Location.gxTpr_Locationimage_gxi;
         AV10SDT_ContentPage.gxTpr_Content.Add(AV11ContentItem, 0);
         AV11ContentItem = new SdtSDT_ContentPage_ContentItem(context);
         AV11ContentItem.gxTpr_Contentid = new SdtRandomStringGenerator(context).generate(12);
         AV11ContentItem.gxTpr_Contenttype = "Description";
         AV11ContentItem.gxTpr_Contentvalue = "Welkom bij de receptie van onze app. Hier kunt u al uw vragen stellen en krijgt u direct hulp van ons team. Of het nu gaat om technische ondersteuning, informatie over diensten, of algemene vragen, wij zijn er om u te helpen.";
         AV10SDT_ContentPage.gxTpr_Content.Add(AV11ContentItem, 0);
         AV12CtaItem = new SdtSDT_ContentPage_CtaItem(context);
         AV12CtaItem.gxTpr_Ctaid = new SdtRandomStringGenerator(context).generate(15);
         AV12CtaItem.gxTpr_Ctalabel = "CALL US";
         AV12CtaItem.gxTpr_Ctatype = "Phone";
         AV12CtaItem.gxTpr_Ctaaction = AV9BC_Trn_Location.gxTpr_Locationphone;
         AV12CtaItem.gxTpr_Ctabuttontype = "Round";
         AV12CtaItem.gxTpr_Ctabgcolor = "CtaColorOne";
         AV10SDT_ContentPage.gxTpr_Cta.Add(AV12CtaItem, 0);
         AV12CtaItem = new SdtSDT_ContentPage_CtaItem(context);
         AV12CtaItem.gxTpr_Ctaid = new SdtRandomStringGenerator(context).generate(15);
         AV12CtaItem.gxTpr_Ctalabel = "EMAIL US";
         AV12CtaItem.gxTpr_Ctatype = "Email";
         AV12CtaItem.gxTpr_Ctaaction = AV9BC_Trn_Location.gxTpr_Locationphone;
         AV12CtaItem.gxTpr_Ctabuttontype = "Round";
         AV12CtaItem.gxTpr_Ctabgcolor = "CtaColorTwo";
         AV10SDT_ContentPage.gxTpr_Cta.Add(AV12CtaItem, 0);
         AV8BC_Page.gxTpr_Pagestructure = AV10SDT_ContentPage.ToJSonString(false, true);
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
         AV8BC_Page = new SdtTrn_AppVersion_Page(context);
         P00B92_A523AppVersionId = new Guid[] {Guid.Empty} ;
         A523AppVersionId = Guid.Empty;
         P00B93_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00B93_A517PageName = new string[] {""} ;
         P00B93_A516PageId = new Guid[] {Guid.Empty} ;
         A517PageName = "";
         A516PageId = Guid.Empty;
         AV9BC_Trn_Location = new SdtTrn_Location(context);
         AV11ContentItem = new SdtSDT_ContentPage_ContentItem(context);
         AV10SDT_ContentPage = new SdtSDT_ContentPage(context);
         AV12CtaItem = new SdtSDT_ContentPage_CtaItem(context);
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_initreceptionpage__default(),
            new Object[][] {
                new Object[] {
               P00B92_A523AppVersionId
               }
               , new Object[] {
               P00B93_A523AppVersionId, P00B93_A517PageName, P00B93_A516PageId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV17GXLvl5 ;
      private string A517PageName ;
      private Guid AV13LocationId ;
      private Guid AV14OrganisationId ;
      private Guid AV15AppVersionId ;
      private Guid A523AppVersionId ;
      private Guid A516PageId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private Guid aP0_LocationId ;
      private Guid aP1_OrganisationId ;
      private Guid aP2_AppVersionId ;
      private SdtTrn_AppVersion_Page AV8BC_Page ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00B92_A523AppVersionId ;
      private Guid[] P00B93_A523AppVersionId ;
      private string[] P00B93_A517PageName ;
      private Guid[] P00B93_A516PageId ;
      private SdtTrn_Location AV9BC_Trn_Location ;
      private SdtSDT_ContentPage_ContentItem AV11ContentItem ;
      private SdtSDT_ContentPage AV10SDT_ContentPage ;
      private SdtSDT_ContentPage_CtaItem AV12CtaItem ;
      private SdtTrn_AppVersion_Page aP3_BC_Page ;
   }

   public class prc_initreceptionpage__default : DataStoreHelperBase, IDataStoreHelper
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
          Object[] prmP00B92;
          prmP00B92 = new Object[] {
          new ParDef("AV15AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00B93;
          prmP00B93 = new Object[] {
          new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00B92", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :AV15AppVersionId ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00B92,1, GxCacheFrequency.OFF ,true,true )
             ,new CursorDef("P00B93", "SELECT AppVersionId, PageName, PageId FROM Trn_AppVersionPage WHERE (AppVersionId = :AppVersionId) AND (PageName = ( 'Reception')) ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00B93,100, GxCacheFrequency.OFF ,false,false )
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
       }
    }

 }

}
