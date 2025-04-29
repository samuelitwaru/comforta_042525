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
   public class prc_infopageapi : GXProcedure
   {
      public prc_infopageapi( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_infopageapi( IGxContext context )
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
                           string aP3_UserId ,
                           out SdtSDT_InfoPage aP4_SDT_InfoPage )
      {
         this.AV12PageId = aP0_PageId;
         this.AV9LocationId = aP1_LocationId;
         this.AV11OrganisationId = aP2_OrganisationId;
         this.AV15UserId = aP3_UserId;
         this.AV8SDT_InfoPage = new SdtSDT_InfoPage(context) ;
         initialize();
         ExecuteImpl();
         aP4_SDT_InfoPage=this.AV8SDT_InfoPage;
      }

      public SdtSDT_InfoPage executeUdp( Guid aP0_PageId ,
                                         Guid aP1_LocationId ,
                                         Guid aP2_OrganisationId ,
                                         string aP3_UserId )
      {
         execute(aP0_PageId, aP1_LocationId, aP2_OrganisationId, aP3_UserId, out aP4_SDT_InfoPage);
         return AV8SDT_InfoPage ;
      }

      public void executeSubmit( Guid aP0_PageId ,
                                 Guid aP1_LocationId ,
                                 Guid aP2_OrganisationId ,
                                 string aP3_UserId ,
                                 out SdtSDT_InfoPage aP4_SDT_InfoPage )
      {
         this.AV12PageId = aP0_PageId;
         this.AV9LocationId = aP1_LocationId;
         this.AV11OrganisationId = aP2_OrganisationId;
         this.AV15UserId = aP3_UserId;
         this.AV8SDT_InfoPage = new SdtSDT_InfoPage(context) ;
         SubmitImpl();
         aP4_SDT_InfoPage=this.AV8SDT_InfoPage;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Using cursor P00G32 */
         pr_default.execute(0, new Object[] {AV9LocationId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A29LocationId = P00G32_A29LocationId[0];
            n29LocationId = P00G32_n29LocationId[0];
            A598PublishedActiveAppVersionId = P00G32_A598PublishedActiveAppVersionId[0];
            n598PublishedActiveAppVersionId = P00G32_n598PublishedActiveAppVersionId[0];
            A584ActiveAppVersionId = P00G32_A584ActiveAppVersionId[0];
            n584ActiveAppVersionId = P00G32_n584ActiveAppVersionId[0];
            A11OrganisationId = P00G32_A11OrganisationId[0];
            n11OrganisationId = P00G32_n11OrganisationId[0];
            AV10AppVersionId = A598PublishedActiveAppVersionId;
            if ( (Guid.Empty==AV10AppVersionId) )
            {
               AV10AppVersionId = A584ActiveAppVersionId;
            }
            pr_default.readNext(0);
         }
         pr_default.close(0);
         /* Using cursor P00G33 */
         pr_default.execute(1, new Object[] {AV9LocationId, AV11OrganisationId, AV10AppVersionId});
         while ( (pr_default.getStatus(1) != 101) )
         {
            A523AppVersionId = P00G33_A523AppVersionId[0];
            A11OrganisationId = P00G33_A11OrganisationId[0];
            n11OrganisationId = P00G33_n11OrganisationId[0];
            A29LocationId = P00G33_A29LocationId[0];
            n29LocationId = P00G33_n29LocationId[0];
            /* Using cursor P00G34 */
            pr_default.execute(2, new Object[] {A523AppVersionId, AV12PageId});
            while ( (pr_default.getStatus(2) != 101) )
            {
               A516PageId = P00G34_A516PageId[0];
               A536PagePublishedStructure = P00G34_A536PagePublishedStructure[0];
               A517PageName = P00G34_A517PageName[0];
               AV8SDT_InfoPage = new SdtSDT_InfoPage(context);
               AV8SDT_InfoPage.FromJSonString(A536PagePublishedStructure, null);
               AV8SDT_InfoPage.gxTpr_Pageid = A516PageId;
               AV8SDT_InfoPage.gxTpr_Pagename = A517PageName;
               /* Exiting from a For First loop. */
               if (true) break;
            }
            pr_default.close(2);
            pr_default.readNext(1);
         }
         pr_default.close(1);
         AV20GXV1 = 1;
         while ( AV20GXV1 <= AV8SDT_InfoPage.gxTpr_Infocontent.Count )
         {
            AV16InfoContent = ((SdtSDT_InfoPage_InfoContentItem)AV8SDT_InfoPage.gxTpr_Infocontent.Item(AV20GXV1));
            if ( StringUtil.StrCmp(AV16InfoContent.gxTpr_Infotype, "Image") == 0 )
            {
               AV16InfoContent.gxTpr_Images.Add(AV16InfoContent.gxTpr_Infovalue, 0);
            }
            AV20GXV1 = (int)(AV20GXV1+1);
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
         AV8SDT_InfoPage = new SdtSDT_InfoPage(context);
         P00G32_A29LocationId = new Guid[] {Guid.Empty} ;
         P00G32_n29LocationId = new bool[] {false} ;
         P00G32_A598PublishedActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P00G32_n598PublishedActiveAppVersionId = new bool[] {false} ;
         P00G32_A584ActiveAppVersionId = new Guid[] {Guid.Empty} ;
         P00G32_n584ActiveAppVersionId = new bool[] {false} ;
         P00G32_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00G32_n11OrganisationId = new bool[] {false} ;
         A29LocationId = Guid.Empty;
         A598PublishedActiveAppVersionId = Guid.Empty;
         A584ActiveAppVersionId = Guid.Empty;
         A11OrganisationId = Guid.Empty;
         AV10AppVersionId = Guid.Empty;
         P00G33_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00G33_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00G33_n11OrganisationId = new bool[] {false} ;
         P00G33_A29LocationId = new Guid[] {Guid.Empty} ;
         P00G33_n29LocationId = new bool[] {false} ;
         A523AppVersionId = Guid.Empty;
         P00G34_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00G34_A516PageId = new Guid[] {Guid.Empty} ;
         P00G34_A536PagePublishedStructure = new string[] {""} ;
         P00G34_A517PageName = new string[] {""} ;
         A516PageId = Guid.Empty;
         A536PagePublishedStructure = "";
         A517PageName = "";
         AV16InfoContent = new SdtSDT_InfoPage_InfoContentItem(context);
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_infopageapi__default(),
            new Object[][] {
                new Object[] {
               P00G32_A29LocationId, P00G32_A598PublishedActiveAppVersionId, P00G32_n598PublishedActiveAppVersionId, P00G32_A584ActiveAppVersionId, P00G32_n584ActiveAppVersionId, P00G32_A11OrganisationId
               }
               , new Object[] {
               P00G33_A523AppVersionId, P00G33_A11OrganisationId, P00G33_n11OrganisationId, P00G33_A29LocationId, P00G33_n29LocationId
               }
               , new Object[] {
               P00G34_A523AppVersionId, P00G34_A516PageId, P00G34_A536PagePublishedStructure, P00G34_A517PageName
               }
            }
         );
         /* GeneXus formulas. */
      }

      private int AV20GXV1 ;
      private bool n29LocationId ;
      private bool n598PublishedActiveAppVersionId ;
      private bool n584ActiveAppVersionId ;
      private bool n11OrganisationId ;
      private string A536PagePublishedStructure ;
      private string AV15UserId ;
      private string A517PageName ;
      private Guid AV12PageId ;
      private Guid AV9LocationId ;
      private Guid AV11OrganisationId ;
      private Guid A29LocationId ;
      private Guid A598PublishedActiveAppVersionId ;
      private Guid A584ActiveAppVersionId ;
      private Guid A11OrganisationId ;
      private Guid AV10AppVersionId ;
      private Guid A523AppVersionId ;
      private Guid A516PageId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private SdtSDT_InfoPage AV8SDT_InfoPage ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00G32_A29LocationId ;
      private bool[] P00G32_n29LocationId ;
      private Guid[] P00G32_A598PublishedActiveAppVersionId ;
      private bool[] P00G32_n598PublishedActiveAppVersionId ;
      private Guid[] P00G32_A584ActiveAppVersionId ;
      private bool[] P00G32_n584ActiveAppVersionId ;
      private Guid[] P00G32_A11OrganisationId ;
      private bool[] P00G32_n11OrganisationId ;
      private Guid[] P00G33_A523AppVersionId ;
      private Guid[] P00G33_A11OrganisationId ;
      private bool[] P00G33_n11OrganisationId ;
      private Guid[] P00G33_A29LocationId ;
      private bool[] P00G33_n29LocationId ;
      private Guid[] P00G34_A523AppVersionId ;
      private Guid[] P00G34_A516PageId ;
      private string[] P00G34_A536PagePublishedStructure ;
      private string[] P00G34_A517PageName ;
      private SdtSDT_InfoPage_InfoContentItem AV16InfoContent ;
      private SdtSDT_InfoPage aP4_SDT_InfoPage ;
   }

   public class prc_infopageapi__default : DataStoreHelperBase, IDataStoreHelper
   {
      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
         ,new ForEachCursor(def[1])
         ,new ForEachCursor(def[2])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP00G32;
          prmP00G32 = new Object[] {
          new ParDef("AV9LocationId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00G33;
          prmP00G33 = new Object[] {
          new ParDef("AV9LocationId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV11OrganisationId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV10AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00G34;
          prmP00G34 = new Object[] {
          new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV12PageId",GXType.UniqueIdentifier,36,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00G32", "SELECT LocationId, PublishedActiveAppVersionId, ActiveAppVersionId, OrganisationId FROM Trn_Location WHERE LocationId = :AV9LocationId ORDER BY LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00G32,100, GxCacheFrequency.OFF ,false,false )
             ,new CursorDef("P00G33", "SELECT AppVersionId, OrganisationId, LocationId FROM Trn_AppVersion WHERE (LocationId = :AV9LocationId and OrganisationId = :AV11OrganisationId) AND (AppVersionId = :AV10AppVersionId) ORDER BY LocationId, OrganisationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00G33,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00G34", "SELECT AppVersionId, PageId, PagePublishedStructure, PageName FROM Trn_AppVersionPage WHERE AppVersionId = :AppVersionId and PageId = :AV12PageId ORDER BY AppVersionId, PageId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00G34,1, GxCacheFrequency.OFF ,false,true )
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
                ((Guid[]) buf[1])[0] = rslt.getGuid(2);
                ((string[]) buf[2])[0] = rslt.getLongVarchar(3);
                ((string[]) buf[3])[0] = rslt.getVarchar(4);
                return;
       }
    }

 }

}
