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
   public class prc_gettranslation : GXProcedure
   {
      public prc_gettranslation( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_gettranslation( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_TrnName ,
                           Guid aP1_primaryKey ,
                           string aP2_AttributeName ,
                           out string aP3_Translation )
      {
         this.AV11TrnName = aP0_TrnName;
         this.AV10primaryKey = aP1_primaryKey;
         this.AV12AttributeName = aP2_AttributeName;
         this.AV9Translation = "" ;
         initialize();
         ExecuteImpl();
         aP3_Translation=this.AV9Translation;
      }

      public string executeUdp( string aP0_TrnName ,
                                Guid aP1_primaryKey ,
                                string aP2_AttributeName )
      {
         execute(aP0_TrnName, aP1_primaryKey, aP2_AttributeName, out aP3_Translation);
         return AV9Translation ;
      }

      public void executeSubmit( string aP0_TrnName ,
                                 Guid aP1_primaryKey ,
                                 string aP2_AttributeName ,
                                 out string aP3_Translation )
      {
         this.AV11TrnName = aP0_TrnName;
         this.AV10primaryKey = aP1_primaryKey;
         this.AV12AttributeName = aP2_AttributeName;
         this.AV9Translation = "" ;
         SubmitImpl();
         aP3_Translation=this.AV9Translation;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV13Language = context.GetLanguage( );
         /* Using cursor P00EI2 */
         pr_default.execute(0, new Object[] {AV11TrnName, AV10primaryKey, AV12AttributeName});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A581DynamicTranslationAttributeNam = P00EI2_A581DynamicTranslationAttributeNam[0];
            A580DynamicTranslationPrimaryKey = P00EI2_A580DynamicTranslationPrimaryKey[0];
            A579DynamicTranslationTrnName = P00EI2_A579DynamicTranslationTrnName[0];
            A582DynamicTranslationEnglish = P00EI2_A582DynamicTranslationEnglish[0];
            A583DynamicTranslationDutch = P00EI2_A583DynamicTranslationDutch[0];
            A578DynamicTranslationId = P00EI2_A578DynamicTranslationId[0];
            if ( StringUtil.StrCmp(AV13Language, "English") == 0 )
            {
               AV9Translation = A582DynamicTranslationEnglish;
            }
            else if ( StringUtil.StrCmp(AV13Language, "Dutch") == 0 )
            {
               AV9Translation = A583DynamicTranslationDutch;
            }
            pr_default.readNext(0);
         }
         pr_default.close(0);
         new prc_logtofile(context ).execute(  AV9Translation) ;
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
         AV9Translation = "";
         AV13Language = "";
         P00EI2_A581DynamicTranslationAttributeNam = new string[] {""} ;
         P00EI2_A580DynamicTranslationPrimaryKey = new Guid[] {Guid.Empty} ;
         P00EI2_A579DynamicTranslationTrnName = new string[] {""} ;
         P00EI2_A582DynamicTranslationEnglish = new string[] {""} ;
         P00EI2_A583DynamicTranslationDutch = new string[] {""} ;
         P00EI2_A578DynamicTranslationId = new Guid[] {Guid.Empty} ;
         A581DynamicTranslationAttributeNam = "";
         A580DynamicTranslationPrimaryKey = Guid.Empty;
         A579DynamicTranslationTrnName = "";
         A582DynamicTranslationEnglish = "";
         A583DynamicTranslationDutch = "";
         A578DynamicTranslationId = Guid.Empty;
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_gettranslation__default(),
            new Object[][] {
                new Object[] {
               P00EI2_A581DynamicTranslationAttributeNam, P00EI2_A580DynamicTranslationPrimaryKey, P00EI2_A579DynamicTranslationTrnName, P00EI2_A582DynamicTranslationEnglish, P00EI2_A583DynamicTranslationDutch, P00EI2_A578DynamicTranslationId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private string AV13Language ;
      private string AV9Translation ;
      private string A582DynamicTranslationEnglish ;
      private string A583DynamicTranslationDutch ;
      private string AV11TrnName ;
      private string AV12AttributeName ;
      private string A581DynamicTranslationAttributeNam ;
      private string A579DynamicTranslationTrnName ;
      private Guid AV10primaryKey ;
      private Guid A580DynamicTranslationPrimaryKey ;
      private Guid A578DynamicTranslationId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private IDataStoreProvider pr_default ;
      private string[] P00EI2_A581DynamicTranslationAttributeNam ;
      private Guid[] P00EI2_A580DynamicTranslationPrimaryKey ;
      private string[] P00EI2_A579DynamicTranslationTrnName ;
      private string[] P00EI2_A582DynamicTranslationEnglish ;
      private string[] P00EI2_A583DynamicTranslationDutch ;
      private Guid[] P00EI2_A578DynamicTranslationId ;
      private string aP3_Translation ;
   }

   public class prc_gettranslation__default : DataStoreHelperBase, IDataStoreHelper
   {
      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP00EI2;
          prmP00EI2 = new Object[] {
          new ParDef("AV11TrnName",GXType.VarChar,100,0) ,
          new ParDef("AV10primaryKey",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV12AttributeName",GXType.VarChar,100,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00EI2", "SELECT DynamicTranslationAttributeNam, DynamicTranslationPrimaryKey, DynamicTranslationTrnName, DynamicTranslationEnglish, DynamicTranslationDutch, DynamicTranslationId FROM Trn_DynamicTranslation WHERE (DynamicTranslationTrnName = ( :AV11TrnName)) AND (DynamicTranslationPrimaryKey = :AV10primaryKey) AND (DynamicTranslationAttributeNam = ( :AV12AttributeName)) ORDER BY DynamicTranslationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00EI2,100, GxCacheFrequency.OFF ,false,false )
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
                ((string[]) buf[0])[0] = rslt.getVarchar(1);
                ((Guid[]) buf[1])[0] = rslt.getGuid(2);
                ((string[]) buf[2])[0] = rslt.getVarchar(3);
                ((string[]) buf[3])[0] = rslt.getLongVarchar(4);
                ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
                ((Guid[]) buf[5])[0] = rslt.getGuid(6);
                return;
       }
    }

 }

}
