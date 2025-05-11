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
   public class prc_savepagethumbnail : GXProcedure
   {
      public prc_savepagethumbnail( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_savepagethumbnail( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_PageId ,
                           string aP1_PageThumbnailData ,
                           out SdtSDT_Error aP2_SDT_Error )
      {
         this.AV9PageId = aP0_PageId;
         this.AV13PageThumbnailData = aP1_PageThumbnailData;
         this.AV11SDT_Error = new SdtSDT_Error(context) ;
         initialize();
         ExecuteImpl();
         aP2_SDT_Error=this.AV11SDT_Error;
      }

      public SdtSDT_Error executeUdp( Guid aP0_PageId ,
                                      string aP1_PageThumbnailData )
      {
         execute(aP0_PageId, aP1_PageThumbnailData, out aP2_SDT_Error);
         return AV11SDT_Error ;
      }

      public void executeSubmit( Guid aP0_PageId ,
                                 string aP1_PageThumbnailData ,
                                 out SdtSDT_Error aP2_SDT_Error )
      {
         this.AV9PageId = aP0_PageId;
         this.AV13PageThumbnailData = aP1_PageThumbnailData;
         this.AV11SDT_Error = new SdtSDT_Error(context) ;
         SubmitImpl();
         aP2_SDT_Error=this.AV11SDT_Error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( ! new prc_isauthenticated(context).executeUdp( ) )
         {
            AV11SDT_Error.gxTpr_Status = context.GetMessage( "Error", "");
            AV11SDT_Error.gxTpr_Message = context.GetMessage( "Not Authenticated", "");
            cleanup();
            if (true) return;
         }
         new prc_logtoserver(context ).execute(  context.GetMessage( "Thumbnail: ", "")+AV13PageThumbnailData) ;
         new prc_logtoserver(context ).execute(  context.GetMessage( "PageId: ", "")+AV9PageId.ToString()) ;
         /* Using cursor P00EN2 */
         pr_default.execute(0, new Object[] {AV9PageId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            GXTEN2 = 0;
            A516PageId = P00EN2_A516PageId[0];
            A40000PageThumbnail_GXI = P00EN2_A40000PageThumbnail_GXI[0];
            n40000PageThumbnail_GXI = P00EN2_n40000PageThumbnail_GXI[0];
            A523AppVersionId = P00EN2_A523AppVersionId[0];
            A599PageThumbnail = P00EN2_A599PageThumbnail[0];
            n599PageThumbnail = P00EN2_n599PageThumbnail[0];
            O599PageThumbnail = A599PageThumbnail;
            n599PageThumbnail = false;
            O599PageThumbnail = A599PageThumbnail;
            n599PageThumbnail = false;
            AV17GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context).get();
            AV16baseUrl = AV17GAMApplication.gxTpr_Environment.gxTpr_Url;
            AV15MediaName = A516PageId.ToString() + context.GetMessage( ".png", "");
            AV18MediaUrl = AV16baseUrl + context.GetMessage( "media/", "") + AV15MediaName;
            AV14Path = context.GetMessage( "media/", "");
            if ( StringUtil.StartsWith( AV20HttpRequest.BaseURL, context.GetMessage( "http://localhost", "")) )
            {
               AV14Path = context.GetMessage( "C:\\KBs\\Comforta_version20\\NETPostgreSQL1039\\Web\\media\\", "");
            }
            new SdtEO_Base64Image(context).saveimage(AV13PageThumbnailData, AV14Path+AV15MediaName) ;
            AV21PageThumbnail = AV18MediaUrl;
            AV25Pagethumbnail_GXI = GXDbFile.PathToUrl( AV18MediaUrl, context);
            if ( ! ( ( StringUtil.StrCmp(O599PageThumbnail, AV21PageThumbnail) == 0 ) ) )
            {
               new prc_logtoserver(context ).execute(  context.GetMessage( "MediaURL: ", "")+AV18MediaUrl) ;
               A599PageThumbnail = AV21PageThumbnail;
               n599PageThumbnail = false;
               A40000PageThumbnail_GXI = AV25Pagethumbnail_GXI;
               n40000PageThumbnail_GXI = false;
               GXTEN2 = 1;
            }
            /* Using cursor P00EN3 */
            pr_default.execute(1, new Object[] {n599PageThumbnail, A599PageThumbnail, n40000PageThumbnail_GXI, A40000PageThumbnail_GXI, A523AppVersionId, A516PageId});
            pr_default.close(1);
            pr_default.SmartCacheProvider.SetUpdated("Trn_AppVersionPage");
            if ( GXTEN2 == 1 )
            {
               context.CommitDataStores("prc_savepagethumbnail",pr_default);
            }
            pr_default.readNext(0);
         }
         pr_default.close(0);
         cleanup();
      }

      public override void cleanup( )
      {
         context.CommitDataStores("prc_savepagethumbnail",pr_default);
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
         ExitApp();
      }

      public override void initialize( )
      {
         AV11SDT_Error = new SdtSDT_Error(context);
         P00EN2_A516PageId = new Guid[] {Guid.Empty} ;
         P00EN2_A40000PageThumbnail_GXI = new string[] {""} ;
         P00EN2_n40000PageThumbnail_GXI = new bool[] {false} ;
         P00EN2_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00EN2_A599PageThumbnail = new string[] {""} ;
         P00EN2_n599PageThumbnail = new bool[] {false} ;
         A516PageId = Guid.Empty;
         A40000PageThumbnail_GXI = "";
         A523AppVersionId = Guid.Empty;
         A599PageThumbnail = "";
         O599PageThumbnail = "";
         AV17GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context);
         AV16baseUrl = "";
         AV15MediaName = "";
         AV18MediaUrl = "";
         AV14Path = "";
         AV20HttpRequest = new GxHttpRequest( context);
         AV21PageThumbnail = "";
         AV25Pagethumbnail_GXI = "";
         pr_datastore1 = new DataStoreProvider(context, new GeneXus.Programs.prc_savepagethumbnail__datastore1(),
            new Object[][] {
            }
         );
         pr_gam = new DataStoreProvider(context, new GeneXus.Programs.prc_savepagethumbnail__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_savepagethumbnail__default(),
            new Object[][] {
                new Object[] {
               P00EN2_A516PageId, P00EN2_A40000PageThumbnail_GXI, P00EN2_n40000PageThumbnail_GXI, P00EN2_A523AppVersionId, P00EN2_A599PageThumbnail, P00EN2_n599PageThumbnail
               }
               , new Object[] {
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short GXTEN2 ;
      private bool n40000PageThumbnail_GXI ;
      private bool n599PageThumbnail ;
      private string AV13PageThumbnailData ;
      private string A40000PageThumbnail_GXI ;
      private string AV16baseUrl ;
      private string AV15MediaName ;
      private string AV18MediaUrl ;
      private string AV14Path ;
      private string AV25Pagethumbnail_GXI ;
      private string A599PageThumbnail ;
      private string O599PageThumbnail ;
      private string AV21PageThumbnail ;
      private Guid AV9PageId ;
      private Guid A516PageId ;
      private Guid A523AppVersionId ;
      private GxHttpRequest AV20HttpRequest ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private SdtSDT_Error AV11SDT_Error ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00EN2_A516PageId ;
      private string[] P00EN2_A40000PageThumbnail_GXI ;
      private bool[] P00EN2_n40000PageThumbnail_GXI ;
      private Guid[] P00EN2_A523AppVersionId ;
      private string[] P00EN2_A599PageThumbnail ;
      private bool[] P00EN2_n599PageThumbnail ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplication AV17GAMApplication ;
      private SdtSDT_Error aP2_SDT_Error ;
      private IDataStoreProvider pr_datastore1 ;
      private IDataStoreProvider pr_gam ;
   }

   public class prc_savepagethumbnail__datastore1 : DataStoreHelperBase, IDataStoreHelper
   {
      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          def= new CursorDef[] {
          };
       }
    }

    public void getResults( int cursor ,
                            IFieldGetter rslt ,
                            Object[] buf )
    {
    }

    public override string getDataStoreName( )
    {
       return "DATASTORE1";
    }

 }

 public class prc_savepagethumbnail__gam : DataStoreHelperBase, IDataStoreHelper
 {
    public ICursor[] getCursors( )
    {
       cursorDefinitions();
       return new Cursor[] {
     };
  }

  private static CursorDef[] def;
  private void cursorDefinitions( )
  {
     if ( def == null )
     {
        def= new CursorDef[] {
        };
     }
  }

  public void getResults( int cursor ,
                          IFieldGetter rslt ,
                          Object[] buf )
  {
  }

  public override string getDataStoreName( )
  {
     return "GAM";
  }

}

public class prc_savepagethumbnail__default : DataStoreHelperBase, IDataStoreHelper
{
   public ICursor[] getCursors( )
   {
      cursorDefinitions();
      return new Cursor[] {
       new ForEachCursor(def[0])
      ,new UpdateCursor(def[1])
    };
 }

 private static CursorDef[] def;
 private void cursorDefinitions( )
 {
    if ( def == null )
    {
       Object[] prmP00EN2;
       prmP00EN2 = new Object[] {
       new ParDef("AV9PageId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmP00EN3;
       prmP00EN3 = new Object[] {
       new ParDef("PageThumbnail",GXType.Byte,1024,0){Nullable=true,InDB=false} ,
       new ParDef("PageThumbnail_GXI",GXType.VarChar,2048,0){Nullable=true,AddAtt=true, ImgIdx=0, Tbl="Trn_AppVersionPage", Fld="PageThumbnail"} ,
       new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("PageId",GXType.UniqueIdentifier,36,0)
       };
       def= new CursorDef[] {
           new CursorDef("P00EN2", "SELECT PageId, PageThumbnail_GXI, AppVersionId, PageThumbnail FROM Trn_AppVersionPage WHERE PageId = :AV9PageId ORDER BY AppVersionId, PageId  FOR UPDATE OF Trn_AppVersionPage",true, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00EN2,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("P00EN3", "SAVEPOINT gxupdate;UPDATE Trn_AppVersionPage SET PageThumbnail=:PageThumbnail, PageThumbnail_GXI=:PageThumbnail_GXI  WHERE AppVersionId = :AppVersionId AND PageId = :PageId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK,prmP00EN3)
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
             ((string[]) buf[1])[0] = rslt.getMultimediaUri(2);
             ((bool[]) buf[2])[0] = rslt.wasNull(2);
             ((Guid[]) buf[3])[0] = rslt.getGuid(3);
             ((string[]) buf[4])[0] = rslt.getMultimediaFile(4, rslt.getVarchar(2));
             ((bool[]) buf[5])[0] = rslt.wasNull(4);
             return;
    }
 }

}

}
