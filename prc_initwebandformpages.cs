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
   public class prc_initwebandformpages : GXProcedure
   {
      public prc_initwebandformpages( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_initwebandformpages( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_AppVersionId ,
                           out SdtTrn_AppVersion_Page aP1_BC_WebLinkPage ,
                           out SdtTrn_AppVersion_Page aP2_BC_DynamicFormPage )
      {
         this.AV12AppVersionId = aP0_AppVersionId;
         this.AV8BC_WebLinkPage = new SdtTrn_AppVersion_Page(context) ;
         this.AV10BC_DynamicFormPage = new SdtTrn_AppVersion_Page(context) ;
         initialize();
         ExecuteImpl();
         aP1_BC_WebLinkPage=this.AV8BC_WebLinkPage;
         aP2_BC_DynamicFormPage=this.AV10BC_DynamicFormPage;
      }

      public SdtTrn_AppVersion_Page executeUdp( Guid aP0_AppVersionId ,
                                                out SdtTrn_AppVersion_Page aP1_BC_WebLinkPage )
      {
         execute(aP0_AppVersionId, out aP1_BC_WebLinkPage, out aP2_BC_DynamicFormPage);
         return AV10BC_DynamicFormPage ;
      }

      public void executeSubmit( Guid aP0_AppVersionId ,
                                 out SdtTrn_AppVersion_Page aP1_BC_WebLinkPage ,
                                 out SdtTrn_AppVersion_Page aP2_BC_DynamicFormPage )
      {
         this.AV12AppVersionId = aP0_AppVersionId;
         this.AV8BC_WebLinkPage = new SdtTrn_AppVersion_Page(context) ;
         this.AV10BC_DynamicFormPage = new SdtTrn_AppVersion_Page(context) ;
         SubmitImpl();
         aP1_BC_WebLinkPage=this.AV8BC_WebLinkPage;
         aP2_BC_DynamicFormPage=this.AV10BC_DynamicFormPage;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Using cursor P00E92 */
         pr_default.execute(0, new Object[] {AV12AppVersionId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A523AppVersionId = P00E92_A523AppVersionId[0];
            /* Using cursor P00E93 */
            pr_default.execute(1, new Object[] {A523AppVersionId});
            while ( (pr_default.getStatus(1) != 101) )
            {
               A525PageType = P00E93_A525PageType[0];
               A516PageId = P00E93_A516PageId[0];
               if ( StringUtil.StrCmp(A525PageType, "WebLink") == 0 )
               {
                  AV8BC_WebLinkPage.gxTpr_Pageid = A516PageId;
               }
               else if ( StringUtil.StrCmp(A525PageType, "DynamicForm") == 0 )
               {
                  AV10BC_DynamicFormPage.gxTpr_Pageid = A516PageId;
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
         AV8BC_WebLinkPage.gxTpr_Pagename = "Web Link";
         AV8BC_WebLinkPage.gxTpr_Ispredefined = true;
         AV8BC_WebLinkPage.gxTpr_Pagetype = "WebLink";
         AV10BC_DynamicFormPage.gxTpr_Pagename = "Dynamic Form";
         AV10BC_DynamicFormPage.gxTpr_Ispredefined = true;
         AV10BC_DynamicFormPage.gxTpr_Pagetype = "DynamicForm";
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
         AV8BC_WebLinkPage = new SdtTrn_AppVersion_Page(context);
         AV10BC_DynamicFormPage = new SdtTrn_AppVersion_Page(context);
         P00E92_A523AppVersionId = new Guid[] {Guid.Empty} ;
         A523AppVersionId = Guid.Empty;
         P00E93_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00E93_A525PageType = new string[] {""} ;
         P00E93_A516PageId = new Guid[] {Guid.Empty} ;
         A525PageType = "";
         A516PageId = Guid.Empty;
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_initwebandformpages__default(),
            new Object[][] {
                new Object[] {
               P00E92_A523AppVersionId
               }
               , new Object[] {
               P00E93_A523AppVersionId, P00E93_A525PageType, P00E93_A516PageId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private string A525PageType ;
      private Guid AV12AppVersionId ;
      private Guid A523AppVersionId ;
      private Guid A516PageId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private SdtTrn_AppVersion_Page AV8BC_WebLinkPage ;
      private SdtTrn_AppVersion_Page AV10BC_DynamicFormPage ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00E92_A523AppVersionId ;
      private Guid[] P00E93_A523AppVersionId ;
      private string[] P00E93_A525PageType ;
      private Guid[] P00E93_A516PageId ;
      private SdtTrn_AppVersion_Page aP1_BC_WebLinkPage ;
      private SdtTrn_AppVersion_Page aP2_BC_DynamicFormPage ;
   }

   public class prc_initwebandformpages__default : DataStoreHelperBase, IDataStoreHelper
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
          Object[] prmP00E92;
          prmP00E92 = new Object[] {
          new ParDef("AV12AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00E93;
          prmP00E93 = new Object[] {
          new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00E92", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :AV12AppVersionId ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00E92,1, GxCacheFrequency.OFF ,true,true )
             ,new CursorDef("P00E93", "SELECT AppVersionId, PageType, PageId FROM Trn_AppVersionPage WHERE AppVersionId = :AppVersionId ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00E93,100, GxCacheFrequency.OFF ,false,false )
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
