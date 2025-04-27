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
   public class prc_getdynamictransation : GXProcedure
   {
      public prc_getdynamictransation( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_getdynamictransation( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_DynamicTranslationTrnName ,
                           Guid aP1_DynamicTranslationPrimaryKey ,
                           string aP2_DynamicTranslationAttributeName ,
                           string aP3_Language ,
                           string aP4_DefaultValue ,
                           out string aP5_TranslatedValue )
      {
         this.AV10DynamicTranslationTrnName = aP0_DynamicTranslationTrnName;
         this.AV9DynamicTranslationPrimaryKey = aP1_DynamicTranslationPrimaryKey;
         this.AV15DynamicTranslationAttributeName = aP2_DynamicTranslationAttributeName;
         this.AV13Language = aP3_Language;
         this.AV16DefaultValue = aP4_DefaultValue;
         this.AV12TranslatedValue = "" ;
         initialize();
         ExecuteImpl();
         aP5_TranslatedValue=this.AV12TranslatedValue;
      }

      public string executeUdp( string aP0_DynamicTranslationTrnName ,
                                Guid aP1_DynamicTranslationPrimaryKey ,
                                string aP2_DynamicTranslationAttributeName ,
                                string aP3_Language ,
                                string aP4_DefaultValue )
      {
         execute(aP0_DynamicTranslationTrnName, aP1_DynamicTranslationPrimaryKey, aP2_DynamicTranslationAttributeName, aP3_Language, aP4_DefaultValue, out aP5_TranslatedValue);
         return AV12TranslatedValue ;
      }

      public void executeSubmit( string aP0_DynamicTranslationTrnName ,
                                 Guid aP1_DynamicTranslationPrimaryKey ,
                                 string aP2_DynamicTranslationAttributeName ,
                                 string aP3_Language ,
                                 string aP4_DefaultValue ,
                                 out string aP5_TranslatedValue )
      {
         this.AV10DynamicTranslationTrnName = aP0_DynamicTranslationTrnName;
         this.AV9DynamicTranslationPrimaryKey = aP1_DynamicTranslationPrimaryKey;
         this.AV15DynamicTranslationAttributeName = aP2_DynamicTranslationAttributeName;
         this.AV13Language = aP3_Language;
         this.AV16DefaultValue = aP4_DefaultValue;
         this.AV12TranslatedValue = "" ;
         SubmitImpl();
         aP5_TranslatedValue=this.AV12TranslatedValue;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV13Language)) )
         {
            AV13Language = context.GetLanguage( );
         }
         new prc_logtoserver(context ).execute(  context.GetMessage( "Translating to : ", "")+AV13Language) ;
         new prc_logtoserver(context ).execute(  context.GetMessage( "				Params : ", "")+AV10DynamicTranslationTrnName+" : "+AV9DynamicTranslationPrimaryKey.ToString()+" : "+AV15DynamicTranslationAttributeName+" : "+AV13Language+" : "+AV16DefaultValue) ;
         AV12TranslatedValue = AV16DefaultValue;
         /* Using cursor P00EB2 */
         pr_default.execute(0, new Object[] {AV9DynamicTranslationPrimaryKey, AV10DynamicTranslationTrnName, AV15DynamicTranslationAttributeName});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A581DynamicTranslationAttributeNam = P00EB2_A581DynamicTranslationAttributeNam[0];
            A579DynamicTranslationTrnName = P00EB2_A579DynamicTranslationTrnName[0];
            A580DynamicTranslationPrimaryKey = P00EB2_A580DynamicTranslationPrimaryKey[0];
            A583DynamicTranslationDutch = P00EB2_A583DynamicTranslationDutch[0];
            A582DynamicTranslationEnglish = P00EB2_A582DynamicTranslationEnglish[0];
            A578DynamicTranslationId = P00EB2_A578DynamicTranslationId[0];
            AV13Language = StringUtil.Trim( AV13Language);
            if ( StringUtil.StrCmp(AV13Language, context.GetMessage( "Dutch", "")) == 0 )
            {
               AV12TranslatedValue = A583DynamicTranslationDutch;
            }
            if ( StringUtil.StrCmp(AV13Language, context.GetMessage( "English", "")) == 0 )
            {
               AV12TranslatedValue = A582DynamicTranslationEnglish;
            }
            pr_default.readNext(0);
         }
         pr_default.close(0);
         new prc_logtoserver(context ).execute(  context.GetMessage( "Final Translated Value : ", "")+AV12TranslatedValue) ;
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
         AV12TranslatedValue = "";
         P00EB2_A581DynamicTranslationAttributeNam = new string[] {""} ;
         P00EB2_A579DynamicTranslationTrnName = new string[] {""} ;
         P00EB2_A580DynamicTranslationPrimaryKey = new Guid[] {Guid.Empty} ;
         P00EB2_A583DynamicTranslationDutch = new string[] {""} ;
         P00EB2_A582DynamicTranslationEnglish = new string[] {""} ;
         P00EB2_A578DynamicTranslationId = new Guid[] {Guid.Empty} ;
         A581DynamicTranslationAttributeNam = "";
         A579DynamicTranslationTrnName = "";
         A580DynamicTranslationPrimaryKey = Guid.Empty;
         A583DynamicTranslationDutch = "";
         A582DynamicTranslationEnglish = "";
         A578DynamicTranslationId = Guid.Empty;
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_getdynamictransation__default(),
            new Object[][] {
                new Object[] {
               P00EB2_A581DynamicTranslationAttributeNam, P00EB2_A579DynamicTranslationTrnName, P00EB2_A580DynamicTranslationPrimaryKey, P00EB2_A583DynamicTranslationDutch, P00EB2_A582DynamicTranslationEnglish, P00EB2_A578DynamicTranslationId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private string AV16DefaultValue ;
      private string AV12TranslatedValue ;
      private string A583DynamicTranslationDutch ;
      private string A582DynamicTranslationEnglish ;
      private string AV10DynamicTranslationTrnName ;
      private string AV15DynamicTranslationAttributeName ;
      private string AV13Language ;
      private string A581DynamicTranslationAttributeNam ;
      private string A579DynamicTranslationTrnName ;
      private Guid AV9DynamicTranslationPrimaryKey ;
      private Guid A580DynamicTranslationPrimaryKey ;
      private Guid A578DynamicTranslationId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private IDataStoreProvider pr_default ;
      private string[] P00EB2_A581DynamicTranslationAttributeNam ;
      private string[] P00EB2_A579DynamicTranslationTrnName ;
      private Guid[] P00EB2_A580DynamicTranslationPrimaryKey ;
      private string[] P00EB2_A583DynamicTranslationDutch ;
      private string[] P00EB2_A582DynamicTranslationEnglish ;
      private Guid[] P00EB2_A578DynamicTranslationId ;
      private string aP5_TranslatedValue ;
   }

   public class prc_getdynamictransation__default : DataStoreHelperBase, IDataStoreHelper
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
          Object[] prmP00EB2;
          prmP00EB2 = new Object[] {
          new ParDef("AV9DynamicTranslationPrimaryKey",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV10DynamicTranslationTrnName",GXType.VarChar,100,0) ,
          new ParDef("AV15DynamicTranslationAttributeName",GXType.VarChar,100,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00EB2", "SELECT DynamicTranslationAttributeNam, DynamicTranslationTrnName, DynamicTranslationPrimaryKey, DynamicTranslationDutch, DynamicTranslationEnglish, DynamicTranslationId FROM Trn_DynamicTranslation WHERE (DynamicTranslationPrimaryKey = :AV9DynamicTranslationPrimaryKey) AND (DynamicTranslationTrnName = ( :AV10DynamicTranslationTrnName)) AND (DynamicTranslationAttributeNam = ( :AV15DynamicTranslationAttributeName)) ORDER BY DynamicTranslationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00EB2,100, GxCacheFrequency.OFF ,false,false )
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
                ((string[]) buf[1])[0] = rslt.getVarchar(2);
                ((Guid[]) buf[2])[0] = rslt.getGuid(3);
                ((string[]) buf[3])[0] = rslt.getLongVarchar(4);
                ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
                ((Guid[]) buf[5])[0] = rslt.getGuid(6);
                return;
       }
    }

 }

}
