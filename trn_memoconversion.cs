using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using GeneXus.Reorg;
using System.Threading;
using GeneXus.Programs;
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
using System.Xml.Serialization;
namespace GeneXus.Programs {
   public class trn_memoconversion : GXProcedure
   {
      public trn_memoconversion( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", false);
      }

      public trn_memoconversion( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( )
      {
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( )
      {
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Using cursor TRN_MEMOCO2 */
         pr_default.execute(0);
         while ( (pr_default.getStatus(0) != 101) )
         {
            A528SG_LocationId = TRN_MEMOCO2_A528SG_LocationId[0];
            A529SG_OrganisationId = TRN_MEMOCO2_A529SG_OrganisationId[0];
            A567MemoForm = TRN_MEMOCO2_A567MemoForm[0];
            A566MemoBgColorCode = TRN_MEMOCO2_A566MemoBgColorCode[0];
            A62ResidentId = TRN_MEMOCO2_A62ResidentId[0];
            A564MemoRemoveDate = TRN_MEMOCO2_A564MemoRemoveDate[0];
            A563MemoDuration = TRN_MEMOCO2_A563MemoDuration[0];
            n563MemoDuration = TRN_MEMOCO2_n563MemoDuration[0];
            A562MemoEndDateTime = TRN_MEMOCO2_A562MemoEndDateTime[0];
            n562MemoEndDateTime = TRN_MEMOCO2_n562MemoEndDateTime[0];
            A561MemoStartDateTime = TRN_MEMOCO2_A561MemoStartDateTime[0];
            n561MemoStartDateTime = TRN_MEMOCO2_n561MemoStartDateTime[0];
            A553MemoDocument = TRN_MEMOCO2_A553MemoDocument[0];
            n553MemoDocument = TRN_MEMOCO2_n553MemoDocument[0];
            A552MemoImage = TRN_MEMOCO2_A552MemoImage[0];
            n552MemoImage = TRN_MEMOCO2_n552MemoImage[0];
            A551MemoDescription = TRN_MEMOCO2_A551MemoDescription[0];
            A550MemoTitle = TRN_MEMOCO2_A550MemoTitle[0];
            A549MemoId = TRN_MEMOCO2_A549MemoId[0];
            /*
               INSERT RECORD ON TABLE GXA0100

            */
            AV2MemoId = A549MemoId;
            AV3MemoTitle = A550MemoTitle;
            AV4MemoDescription = A551MemoDescription;
            if ( TRN_MEMOCO2_n552MemoImage[0] )
            {
               AV5MemoImage = "";
               nV5MemoImage = false;
               nV5MemoImage = true;
            }
            else
            {
               AV5MemoImage = A552MemoImage;
               nV5MemoImage = false;
            }
            if ( TRN_MEMOCO2_n553MemoDocument[0] )
            {
               AV6MemoDocument = "";
               nV6MemoDocument = false;
               nV6MemoDocument = true;
            }
            else
            {
               AV6MemoDocument = A553MemoDocument;
               nV6MemoDocument = false;
            }
            if ( TRN_MEMOCO2_n561MemoStartDateTime[0] )
            {
               AV7MemoStartDateTime = (DateTime)(DateTime.MinValue);
               nV7MemoStartDateTime = false;
               nV7MemoStartDateTime = true;
            }
            else
            {
               AV7MemoStartDateTime = A561MemoStartDateTime;
               nV7MemoStartDateTime = false;
            }
            if ( TRN_MEMOCO2_n562MemoEndDateTime[0] )
            {
               AV8MemoEndDateTime = (DateTime)(DateTime.MinValue);
               nV8MemoEndDateTime = false;
               nV8MemoEndDateTime = true;
            }
            else
            {
               AV8MemoEndDateTime = A562MemoEndDateTime;
               nV8MemoEndDateTime = false;
            }
            if ( TRN_MEMOCO2_n563MemoDuration[0] )
            {
               AV9MemoDuration = 0;
               nV9MemoDuration = false;
               nV9MemoDuration = true;
            }
            else
            {
               AV9MemoDuration = A563MemoDuration;
               nV9MemoDuration = false;
            }
            AV10MemoRemoveDate = A564MemoRemoveDate;
            nV10MemoRemoveDate = false;
            AV11ResidentId = A62ResidentId;
            AV12MemoBgColorCode = A566MemoBgColorCode;
            nV12MemoBgColorCode = false;
            AV13MemoForm = A567MemoForm;
            AV14SG_OrganisationId = A529SG_OrganisationId;
            AV15SG_LocationId = A528SG_LocationId;
            AV16MemoType = " ";
            AV17MemoName = " ";
            AV18MemoLeftOffset = 0;
            AV19MemoTopOffset = 0;
            AV20MemoTitleAngle = 0;
            AV21MemoTitleScale = 0;
            /* Using cursor TRN_MEMOCO3 */
            pr_default.execute(1, new Object[] {AV2MemoId, AV3MemoTitle, AV4MemoDescription, nV5MemoImage, AV5MemoImage, nV6MemoDocument, AV6MemoDocument, nV7MemoStartDateTime, AV7MemoStartDateTime, nV8MemoEndDateTime, AV8MemoEndDateTime, nV9MemoDuration, AV9MemoDuration, nV10MemoRemoveDate, AV10MemoRemoveDate, AV11ResidentId, nV12MemoBgColorCode, AV12MemoBgColorCode, AV13MemoForm, AV14SG_OrganisationId, AV15SG_LocationId, AV16MemoType, AV17MemoName, AV18MemoLeftOffset, AV19MemoTopOffset, AV20MemoTitleAngle, AV21MemoTitleScale});
            pr_default.close(1);
            pr_default.SmartCacheProvider.SetUpdated("GXA0100");
            if ( (pr_default.getStatus(1) == 1) )
            {
               context.Gx_err = 1;
               Gx_emsg = (string)(GXResourceManager.GetMessage("GXM_noupdate"));
            }
            else
            {
               context.Gx_err = 0;
               Gx_emsg = "";
            }
            /* End Insert */
            pr_default.readNext(0);
         }
         pr_default.close(0);
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
         TRN_MEMOCO2_A528SG_LocationId = new Guid[] {Guid.Empty} ;
         TRN_MEMOCO2_A529SG_OrganisationId = new Guid[] {Guid.Empty} ;
         TRN_MEMOCO2_A567MemoForm = new string[] {""} ;
         TRN_MEMOCO2_A566MemoBgColorCode = new string[] {""} ;
         TRN_MEMOCO2_A62ResidentId = new Guid[] {Guid.Empty} ;
         TRN_MEMOCO2_A564MemoRemoveDate = new DateTime[] {DateTime.MinValue} ;
         TRN_MEMOCO2_A563MemoDuration = new short[1] ;
         TRN_MEMOCO2_n563MemoDuration = new bool[] {false} ;
         TRN_MEMOCO2_A562MemoEndDateTime = new DateTime[] {DateTime.MinValue} ;
         TRN_MEMOCO2_n562MemoEndDateTime = new bool[] {false} ;
         TRN_MEMOCO2_A561MemoStartDateTime = new DateTime[] {DateTime.MinValue} ;
         TRN_MEMOCO2_n561MemoStartDateTime = new bool[] {false} ;
         TRN_MEMOCO2_A553MemoDocument = new string[] {""} ;
         TRN_MEMOCO2_n553MemoDocument = new bool[] {false} ;
         TRN_MEMOCO2_A552MemoImage = new string[] {""} ;
         TRN_MEMOCO2_n552MemoImage = new bool[] {false} ;
         TRN_MEMOCO2_A551MemoDescription = new string[] {""} ;
         TRN_MEMOCO2_A550MemoTitle = new string[] {""} ;
         TRN_MEMOCO2_A549MemoId = new Guid[] {Guid.Empty} ;
         A528SG_LocationId = Guid.Empty;
         A529SG_OrganisationId = Guid.Empty;
         A567MemoForm = "";
         A566MemoBgColorCode = "";
         A62ResidentId = Guid.Empty;
         A564MemoRemoveDate = DateTime.MinValue;
         A562MemoEndDateTime = (DateTime)(DateTime.MinValue);
         A561MemoStartDateTime = (DateTime)(DateTime.MinValue);
         A553MemoDocument = "";
         A552MemoImage = "";
         A551MemoDescription = "";
         A550MemoTitle = "";
         A549MemoId = Guid.Empty;
         AV2MemoId = Guid.Empty;
         AV3MemoTitle = "";
         AV4MemoDescription = "";
         AV5MemoImage = "";
         AV6MemoDocument = "";
         AV7MemoStartDateTime = (DateTime)(DateTime.MinValue);
         AV8MemoEndDateTime = (DateTime)(DateTime.MinValue);
         AV10MemoRemoveDate = DateTime.MinValue;
         AV11ResidentId = Guid.Empty;
         AV12MemoBgColorCode = "";
         AV13MemoForm = "";
         AV14SG_OrganisationId = Guid.Empty;
         AV15SG_LocationId = Guid.Empty;
         AV16MemoType = "";
         AV17MemoName = "";
         Gx_emsg = "";
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.trn_memoconversion__default(),
            new Object[][] {
                new Object[] {
               TRN_MEMOCO2_A528SG_LocationId, TRN_MEMOCO2_A529SG_OrganisationId, TRN_MEMOCO2_A567MemoForm, TRN_MEMOCO2_A566MemoBgColorCode, TRN_MEMOCO2_A62ResidentId, TRN_MEMOCO2_A564MemoRemoveDate, TRN_MEMOCO2_A563MemoDuration, TRN_MEMOCO2_n563MemoDuration, TRN_MEMOCO2_A562MemoEndDateTime, TRN_MEMOCO2_n562MemoEndDateTime,
               TRN_MEMOCO2_A561MemoStartDateTime, TRN_MEMOCO2_n561MemoStartDateTime, TRN_MEMOCO2_A553MemoDocument, TRN_MEMOCO2_n553MemoDocument, TRN_MEMOCO2_A552MemoImage, TRN_MEMOCO2_n552MemoImage, TRN_MEMOCO2_A551MemoDescription, TRN_MEMOCO2_A550MemoTitle, TRN_MEMOCO2_A549MemoId
               }
               , new Object[] {
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short A563MemoDuration ;
      private short AV9MemoDuration ;
      private int GIGXA0100 ;
      private decimal AV18MemoLeftOffset ;
      private decimal AV19MemoTopOffset ;
      private decimal AV20MemoTitleAngle ;
      private decimal AV21MemoTitleScale ;
      private string A567MemoForm ;
      private string AV13MemoForm ;
      private string Gx_emsg ;
      private DateTime A562MemoEndDateTime ;
      private DateTime A561MemoStartDateTime ;
      private DateTime AV7MemoStartDateTime ;
      private DateTime AV8MemoEndDateTime ;
      private DateTime A564MemoRemoveDate ;
      private DateTime AV10MemoRemoveDate ;
      private bool n563MemoDuration ;
      private bool n562MemoEndDateTime ;
      private bool n561MemoStartDateTime ;
      private bool n553MemoDocument ;
      private bool n552MemoImage ;
      private bool nV5MemoImage ;
      private bool nV6MemoDocument ;
      private bool nV7MemoStartDateTime ;
      private bool nV8MemoEndDateTime ;
      private bool nV9MemoDuration ;
      private bool nV10MemoRemoveDate ;
      private bool nV12MemoBgColorCode ;
      private string AV5MemoImage ;
      private string A566MemoBgColorCode ;
      private string A553MemoDocument ;
      private string A552MemoImage ;
      private string A551MemoDescription ;
      private string A550MemoTitle ;
      private string AV3MemoTitle ;
      private string AV4MemoDescription ;
      private string AV6MemoDocument ;
      private string AV12MemoBgColorCode ;
      private string AV16MemoType ;
      private string AV17MemoName ;
      private Guid A528SG_LocationId ;
      private Guid A529SG_OrganisationId ;
      private Guid A62ResidentId ;
      private Guid A549MemoId ;
      private Guid AV2MemoId ;
      private Guid AV11ResidentId ;
      private Guid AV14SG_OrganisationId ;
      private Guid AV15SG_LocationId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private IDataStoreProvider pr_default ;
      private Guid[] TRN_MEMOCO2_A528SG_LocationId ;
      private Guid[] TRN_MEMOCO2_A529SG_OrganisationId ;
      private string[] TRN_MEMOCO2_A567MemoForm ;
      private string[] TRN_MEMOCO2_A566MemoBgColorCode ;
      private Guid[] TRN_MEMOCO2_A62ResidentId ;
      private DateTime[] TRN_MEMOCO2_A564MemoRemoveDate ;
      private short[] TRN_MEMOCO2_A563MemoDuration ;
      private bool[] TRN_MEMOCO2_n563MemoDuration ;
      private DateTime[] TRN_MEMOCO2_A562MemoEndDateTime ;
      private bool[] TRN_MEMOCO2_n562MemoEndDateTime ;
      private DateTime[] TRN_MEMOCO2_A561MemoStartDateTime ;
      private bool[] TRN_MEMOCO2_n561MemoStartDateTime ;
      private string[] TRN_MEMOCO2_A553MemoDocument ;
      private bool[] TRN_MEMOCO2_n553MemoDocument ;
      private string[] TRN_MEMOCO2_A552MemoImage ;
      private bool[] TRN_MEMOCO2_n552MemoImage ;
      private string[] TRN_MEMOCO2_A551MemoDescription ;
      private string[] TRN_MEMOCO2_A550MemoTitle ;
      private Guid[] TRN_MEMOCO2_A549MemoId ;
   }

   public class trn_memoconversion__default : DataStoreHelperBase, IDataStoreHelper
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
          Object[] prmTRN_MEMOCO2;
          prmTRN_MEMOCO2 = new Object[] {
          };
          Object[] prmTRN_MEMOCO3;
          prmTRN_MEMOCO3 = new Object[] {
          new ParDef("AV2MemoId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV3MemoTitle",GXType.VarChar,100,0) ,
          new ParDef("AV4MemoDescription",GXType.VarChar,200,0) ,
          new ParDef("AV5MemoImage",GXType.LongVarChar,2097152,0){Nullable=true} ,
          new ParDef("AV6MemoDocument",GXType.VarChar,200,0){Nullable=true} ,
          new ParDef("AV7MemoStartDateTime",GXType.DateTime,8,5){Nullable=true} ,
          new ParDef("AV8MemoEndDateTime",GXType.DateTime,8,5){Nullable=true} ,
          new ParDef("AV9MemoDuration",GXType.Int16,4,0){Nullable=true} ,
          new ParDef("AV10MemoRemoveDate",GXType.Date,8,0){Nullable=true} ,
          new ParDef("AV11ResidentId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV12MemoBgColorCode",GXType.VarChar,100,0){Nullable=true} ,
          new ParDef("AV13MemoForm",GXType.Char,20,0) ,
          new ParDef("AV14SG_OrganisationId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV15SG_LocationId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV16MemoType",GXType.VarChar,100,0) ,
          new ParDef("AV17MemoName",GXType.VarChar,100,0) ,
          new ParDef("AV18MemoLeftOffset",GXType.Number,10,4) ,
          new ParDef("AV19MemoTopOffset",GXType.Number,10,4) ,
          new ParDef("AV20MemoTitleAngle",GXType.Number,10,4) ,
          new ParDef("AV21MemoTitleScale",GXType.Number,10,4)
          };
          def= new CursorDef[] {
              new CursorDef("TRN_MEMOCO2", "SELECT SG_LocationId, SG_OrganisationId, MemoForm, MemoBgColorCode, ResidentId, MemoRemoveDate, MemoDuration, MemoEndDateTime, MemoStartDateTime, MemoDocument, MemoImage, MemoDescription, MemoTitle, MemoId FROM Trn_Memo ORDER BY MemoId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmTRN_MEMOCO2,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("TRN_MEMOCO3", "INSERT INTO GXA0100(MemoId, MemoTitle, MemoDescription, MemoImage, MemoDocument, MemoStartDateTime, MemoEndDateTime, MemoDuration, MemoRemoveDate, ResidentId, MemoBgColorCode, MemoForm, SG_OrganisationId, SG_LocationId, MemoType, MemoName, MemoLeftOffset, MemoTopOffset, MemoTitleAngle, MemoTitleScale) VALUES(:AV2MemoId, :AV3MemoTitle, :AV4MemoDescription, :AV5MemoImage, :AV6MemoDocument, :AV7MemoStartDateTime, :AV8MemoEndDateTime, :AV9MemoDuration, :AV10MemoRemoveDate, :AV11ResidentId, :AV12MemoBgColorCode, :AV13MemoForm, :AV14SG_OrganisationId, :AV15SG_LocationId, :AV16MemoType, :AV17MemoName, :AV18MemoLeftOffset, :AV19MemoTopOffset, :AV20MemoTitleAngle, :AV21MemoTitleScale)", GxErrorMask.GX_NOMASK,prmTRN_MEMOCO3)
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
                ((string[]) buf[2])[0] = rslt.getString(3, 20);
                ((string[]) buf[3])[0] = rslt.getVarchar(4);
                ((Guid[]) buf[4])[0] = rslt.getGuid(5);
                ((DateTime[]) buf[5])[0] = rslt.getGXDate(6);
                ((short[]) buf[6])[0] = rslt.getShort(7);
                ((bool[]) buf[7])[0] = rslt.wasNull(7);
                ((DateTime[]) buf[8])[0] = rslt.getGXDateTime(8);
                ((bool[]) buf[9])[0] = rslt.wasNull(8);
                ((DateTime[]) buf[10])[0] = rslt.getGXDateTime(9);
                ((bool[]) buf[11])[0] = rslt.wasNull(9);
                ((string[]) buf[12])[0] = rslt.getVarchar(10);
                ((bool[]) buf[13])[0] = rslt.wasNull(10);
                ((string[]) buf[14])[0] = rslt.getVarchar(11);
                ((bool[]) buf[15])[0] = rslt.wasNull(11);
                ((string[]) buf[16])[0] = rslt.getVarchar(12);
                ((string[]) buf[17])[0] = rslt.getVarchar(13);
                ((Guid[]) buf[18])[0] = rslt.getGuid(14);
                return;
       }
    }

 }

}
