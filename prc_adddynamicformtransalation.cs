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
   public class prc_adddynamicformtransalation : GXProcedure
   {
      public prc_adddynamicformtransalation( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_adddynamicformtransalation( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( GXBaseCollection<SdtSDT_DynamicFormTranslation> aP0_SDT_DynamicFormTranslationCollection )
      {
         this.AV21SDT_DynamicFormTranslationCollection = aP0_SDT_DynamicFormTranslationCollection;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( GXBaseCollection<SdtSDT_DynamicFormTranslation> aP0_SDT_DynamicFormTranslationCollection )
      {
         this.AV21SDT_DynamicFormTranslationCollection = aP0_SDT_DynamicFormTranslationCollection;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new prc_logtofile(context ).execute(  AV21SDT_DynamicFormTranslationCollection.ToJSonString(false)) ;
         AV9Language = context.GetLanguage( );
         if ( StringUtil.StrCmp(AV9Language, "English") == 0 )
         {
            AV12LanguageCode = "en";
         }
         else if ( StringUtil.StrCmp(AV9Language, "Dutch") == 0 )
         {
            AV12LanguageCode = "nl";
         }
         AV23GXV1 = 1;
         while ( AV23GXV1 <= AV21SDT_DynamicFormTranslationCollection.Count )
         {
            AV22SDT_DynamicFormTranslation = ((SdtSDT_DynamicFormTranslation)AV21SDT_DynamicFormTranslationCollection.Item(AV23GXV1));
            new prc_logtofile(context ).execute(  AV22SDT_DynamicFormTranslation.ToJSonString(false, true)) ;
            if ( StringUtil.StrCmp(AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationtrnname, "WWP_Form") == 0 )
            {
               AV24GXLvl17 = 0;
               /* Using cursor P00EG2 */
               pr_default.execute(0, new Object[] {AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationwwpformid, AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationwwpformversionnumber, AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationtrnname});
               while ( (pr_default.getStatus(0) != 101) )
               {
                  A591DynamicFormTranslationTrnName = P00EG2_A591DynamicFormTranslationTrnName[0];
                  A589DynamicFormTranslationWWPFormV = P00EG2_A589DynamicFormTranslationWWPFormV[0];
                  A588DynamicFormTranslationWWpFormI = P00EG2_A588DynamicFormTranslationWWpFormI[0];
                  A593DynamicFormTranslationEnglish = P00EG2_A593DynamicFormTranslationEnglish[0];
                  A594DynamicFormTranslationDutch = P00EG2_A594DynamicFormTranslationDutch[0];
                  A587DynamicFormTranslationId = P00EG2_A587DynamicFormTranslationId[0];
                  AV24GXLvl17 = 1;
                  /* Execute user subroutine: 'TRANSLATE' */
                  S111 ();
                  if ( returnInSub )
                  {
                     pr_default.close(0);
                     cleanup();
                     if (true) return;
                  }
                  A593DynamicFormTranslationEnglish = AV19DynamicTranslationEnglish;
                  A594DynamicFormTranslationDutch = AV20DynamicTranslationDutch;
                  /* Using cursor P00EG3 */
                  pr_default.execute(1, new Object[] {A593DynamicFormTranslationEnglish, A594DynamicFormTranslationDutch, A587DynamicFormTranslationId});
                  pr_default.close(1);
                  pr_default.SmartCacheProvider.SetUpdated("Trn_DynamicFormTranslation");
                  pr_default.readNext(0);
               }
               pr_default.close(0);
               if ( AV24GXLvl17 == 0 )
               {
                  /* Execute user subroutine: 'TRANSLATE' */
                  S111 ();
                  if ( returnInSub )
                  {
                     cleanup();
                     if (true) return;
                  }
                  /*
                     INSERT RECORD ON TABLE Trn_DynamicFormTranslation

                  */
                  A588DynamicFormTranslationWWpFormI = AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationwwpformid;
                  A589DynamicFormTranslationWWPFormV = AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationwwpformversionnumber;
                  A591DynamicFormTranslationTrnName = AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationtrnname;
                  A592DynamicFormTranslationAttribut = AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationattributename;
                  A593DynamicFormTranslationEnglish = AV19DynamicTranslationEnglish;
                  A594DynamicFormTranslationDutch = AV20DynamicTranslationDutch;
                  A587DynamicFormTranslationId = Guid.NewGuid( );
                  /* Using cursor P00EG4 */
                  pr_default.execute(2, new Object[] {A587DynamicFormTranslationId, A588DynamicFormTranslationWWpFormI, A589DynamicFormTranslationWWPFormV, A591DynamicFormTranslationTrnName, A592DynamicFormTranslationAttribut, A593DynamicFormTranslationEnglish, A594DynamicFormTranslationDutch});
                  pr_default.close(2);
                  pr_default.SmartCacheProvider.SetUpdated("Trn_DynamicFormTranslation");
                  if ( (pr_default.getStatus(2) == 1) )
                  {
                     context.Gx_err = 1;
                     Gx_emsg = (string)(context.GetMessage( "GXM_noupdate", ""));
                  }
                  else
                  {
                     context.Gx_err = 0;
                     Gx_emsg = "";
                  }
                  /* End Insert */
               }
            }
            if ( StringUtil.StrCmp(AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationtrnname, "WWP_Form.Element") == 0 )
            {
               AV25GXLvl43 = 0;
               /* Using cursor P00EG5 */
               pr_default.execute(3, new Object[] {AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationwwpformid, AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationwwpformversionnumber, AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationtrnname, AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationwwpformelementid});
               while ( (pr_default.getStatus(3) != 101) )
               {
                  A590DynamicFormTranslationWWPFormE = P00EG5_A590DynamicFormTranslationWWPFormE[0];
                  A591DynamicFormTranslationTrnName = P00EG5_A591DynamicFormTranslationTrnName[0];
                  A589DynamicFormTranslationWWPFormV = P00EG5_A589DynamicFormTranslationWWPFormV[0];
                  A588DynamicFormTranslationWWpFormI = P00EG5_A588DynamicFormTranslationWWpFormI[0];
                  A593DynamicFormTranslationEnglish = P00EG5_A593DynamicFormTranslationEnglish[0];
                  A594DynamicFormTranslationDutch = P00EG5_A594DynamicFormTranslationDutch[0];
                  A587DynamicFormTranslationId = P00EG5_A587DynamicFormTranslationId[0];
                  AV25GXLvl43 = 1;
                  /* Execute user subroutine: 'TRANSLATE' */
                  S111 ();
                  if ( returnInSub )
                  {
                     pr_default.close(3);
                     cleanup();
                     if (true) return;
                  }
                  A593DynamicFormTranslationEnglish = AV19DynamicTranslationEnglish;
                  A594DynamicFormTranslationDutch = AV20DynamicTranslationDutch;
                  /* Using cursor P00EG6 */
                  pr_default.execute(4, new Object[] {A593DynamicFormTranslationEnglish, A594DynamicFormTranslationDutch, A587DynamicFormTranslationId});
                  pr_default.close(4);
                  pr_default.SmartCacheProvider.SetUpdated("Trn_DynamicFormTranslation");
                  pr_default.readNext(3);
               }
               pr_default.close(3);
               if ( AV25GXLvl43 == 0 )
               {
                  /* Execute user subroutine: 'TRANSLATE' */
                  S111 ();
                  if ( returnInSub )
                  {
                     cleanup();
                     if (true) return;
                  }
                  /*
                     INSERT RECORD ON TABLE Trn_DynamicFormTranslation

                  */
                  A588DynamicFormTranslationWWpFormI = AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationwwpformid;
                  A589DynamicFormTranslationWWPFormV = AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationwwpformversionnumber;
                  A591DynamicFormTranslationTrnName = AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationtrnname;
                  A590DynamicFormTranslationWWPFormE = AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationwwpformelementid;
                  A592DynamicFormTranslationAttribut = AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationattributename;
                  A593DynamicFormTranslationEnglish = AV19DynamicTranslationEnglish;
                  A594DynamicFormTranslationDutch = AV20DynamicTranslationDutch;
                  A587DynamicFormTranslationId = Guid.NewGuid( );
                  /* Using cursor P00EG7 */
                  pr_default.execute(5, new Object[] {A587DynamicFormTranslationId, A588DynamicFormTranslationWWpFormI, A589DynamicFormTranslationWWPFormV, A590DynamicFormTranslationWWPFormE, A591DynamicFormTranslationTrnName, A592DynamicFormTranslationAttribut, A593DynamicFormTranslationEnglish, A594DynamicFormTranslationDutch});
                  pr_default.close(5);
                  pr_default.SmartCacheProvider.SetUpdated("Trn_DynamicFormTranslation");
                  if ( (pr_default.getStatus(5) == 1) )
                  {
                     context.Gx_err = 1;
                     Gx_emsg = (string)(context.GetMessage( "GXM_noupdate", ""));
                  }
                  else
                  {
                     context.Gx_err = 0;
                     Gx_emsg = "";
                  }
                  /* End Insert */
               }
            }
            AV23GXV1 = (int)(AV23GXV1+1);
         }
         context.CommitDataStores("prc_adddynamicformtransalation",pr_default);
         cleanup();
      }

      protected void S111( )
      {
         /* 'TRANSLATE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV9Language, "English") == 0 )
         {
            AV19DynamicTranslationEnglish = AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationvalue;
            GXt_char1 = AV20DynamicTranslationDutch;
            new prc_translatelanguage(context ).execute(  AV12LanguageCode,  "nl",  AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationvalue, out  GXt_char1) ;
            AV20DynamicTranslationDutch = GXt_char1;
         }
         else if ( StringUtil.StrCmp(AV9Language, "Dutch") == 0 )
         {
            AV20DynamicTranslationDutch = AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationvalue;
            GXt_char1 = AV19DynamicTranslationEnglish;
            new prc_translatelanguage(context ).execute(  AV12LanguageCode,  "en",  AV22SDT_DynamicFormTranslation.gxTpr_Dynamicformtranslationvalue, out  GXt_char1) ;
            AV19DynamicTranslationEnglish = GXt_char1;
         }
      }

      public override void cleanup( )
      {
         context.CommitDataStores("prc_adddynamicformtransalation",pr_default);
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
         ExitApp();
      }

      public override void initialize( )
      {
         AV9Language = "";
         AV12LanguageCode = "";
         AV22SDT_DynamicFormTranslation = new SdtSDT_DynamicFormTranslation(context);
         P00EG2_A591DynamicFormTranslationTrnName = new string[] {""} ;
         P00EG2_A589DynamicFormTranslationWWPFormV = new int[1] ;
         P00EG2_A588DynamicFormTranslationWWpFormI = new int[1] ;
         P00EG2_A593DynamicFormTranslationEnglish = new string[] {""} ;
         P00EG2_A594DynamicFormTranslationDutch = new string[] {""} ;
         P00EG2_A587DynamicFormTranslationId = new Guid[] {Guid.Empty} ;
         A591DynamicFormTranslationTrnName = "";
         A593DynamicFormTranslationEnglish = "";
         A594DynamicFormTranslationDutch = "";
         A587DynamicFormTranslationId = Guid.Empty;
         AV19DynamicTranslationEnglish = "";
         AV20DynamicTranslationDutch = "";
         A592DynamicFormTranslationAttribut = "";
         Gx_emsg = "";
         P00EG5_A590DynamicFormTranslationWWPFormE = new int[1] ;
         P00EG5_A591DynamicFormTranslationTrnName = new string[] {""} ;
         P00EG5_A589DynamicFormTranslationWWPFormV = new int[1] ;
         P00EG5_A588DynamicFormTranslationWWpFormI = new int[1] ;
         P00EG5_A593DynamicFormTranslationEnglish = new string[] {""} ;
         P00EG5_A594DynamicFormTranslationDutch = new string[] {""} ;
         P00EG5_A587DynamicFormTranslationId = new Guid[] {Guid.Empty} ;
         GXt_char1 = "";
         pr_datastore1 = new DataStoreProvider(context, new GeneXus.Programs.prc_adddynamicformtransalation__datastore1(),
            new Object[][] {
            }
         );
         pr_gam = new DataStoreProvider(context, new GeneXus.Programs.prc_adddynamicformtransalation__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_adddynamicformtransalation__default(),
            new Object[][] {
                new Object[] {
               P00EG2_A591DynamicFormTranslationTrnName, P00EG2_A589DynamicFormTranslationWWPFormV, P00EG2_A588DynamicFormTranslationWWpFormI, P00EG2_A593DynamicFormTranslationEnglish, P00EG2_A594DynamicFormTranslationDutch, P00EG2_A587DynamicFormTranslationId
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               P00EG5_A590DynamicFormTranslationWWPFormE, P00EG5_A591DynamicFormTranslationTrnName, P00EG5_A589DynamicFormTranslationWWPFormV, P00EG5_A588DynamicFormTranslationWWpFormI, P00EG5_A593DynamicFormTranslationEnglish, P00EG5_A594DynamicFormTranslationDutch, P00EG5_A587DynamicFormTranslationId
               }
               , new Object[] {
               }
               , new Object[] {
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV24GXLvl17 ;
      private short AV25GXLvl43 ;
      private int AV23GXV1 ;
      private int A589DynamicFormTranslationWWPFormV ;
      private int A588DynamicFormTranslationWWpFormI ;
      private int GX_INS105 ;
      private int A590DynamicFormTranslationWWPFormE ;
      private string AV12LanguageCode ;
      private string Gx_emsg ;
      private string GXt_char1 ;
      private bool returnInSub ;
      private string A593DynamicFormTranslationEnglish ;
      private string A594DynamicFormTranslationDutch ;
      private string AV19DynamicTranslationEnglish ;
      private string AV20DynamicTranslationDutch ;
      private string AV9Language ;
      private string A591DynamicFormTranslationTrnName ;
      private string A592DynamicFormTranslationAttribut ;
      private Guid A587DynamicFormTranslationId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<SdtSDT_DynamicFormTranslation> AV21SDT_DynamicFormTranslationCollection ;
      private SdtSDT_DynamicFormTranslation AV22SDT_DynamicFormTranslation ;
      private IDataStoreProvider pr_default ;
      private string[] P00EG2_A591DynamicFormTranslationTrnName ;
      private int[] P00EG2_A589DynamicFormTranslationWWPFormV ;
      private int[] P00EG2_A588DynamicFormTranslationWWpFormI ;
      private string[] P00EG2_A593DynamicFormTranslationEnglish ;
      private string[] P00EG2_A594DynamicFormTranslationDutch ;
      private Guid[] P00EG2_A587DynamicFormTranslationId ;
      private int[] P00EG5_A590DynamicFormTranslationWWPFormE ;
      private string[] P00EG5_A591DynamicFormTranslationTrnName ;
      private int[] P00EG5_A589DynamicFormTranslationWWPFormV ;
      private int[] P00EG5_A588DynamicFormTranslationWWpFormI ;
      private string[] P00EG5_A593DynamicFormTranslationEnglish ;
      private string[] P00EG5_A594DynamicFormTranslationDutch ;
      private Guid[] P00EG5_A587DynamicFormTranslationId ;
      private IDataStoreProvider pr_datastore1 ;
      private IDataStoreProvider pr_gam ;
   }

   public class prc_adddynamicformtransalation__datastore1 : DataStoreHelperBase, IDataStoreHelper
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

 public class prc_adddynamicformtransalation__gam : DataStoreHelperBase, IDataStoreHelper
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

public class prc_adddynamicformtransalation__default : DataStoreHelperBase, IDataStoreHelper
{
   public ICursor[] getCursors( )
   {
      cursorDefinitions();
      return new Cursor[] {
       new ForEachCursor(def[0])
      ,new UpdateCursor(def[1])
      ,new UpdateCursor(def[2])
      ,new ForEachCursor(def[3])
      ,new UpdateCursor(def[4])
      ,new UpdateCursor(def[5])
    };
 }

 private static CursorDef[] def;
 private void cursorDefinitions( )
 {
    if ( def == null )
    {
       Object[] prmP00EG2;
       prmP00EG2 = new Object[] {
       new ParDef("AV22SDT__1Dynamicformtranslat",GXType.Int32,6,0) ,
       new ParDef("AV22SDT__2Dynamicformtranslat",GXType.Int32,6,0) ,
       new ParDef("AV22SDT__3Dynamicformtranslat",GXType.VarChar,400,0)
       };
       Object[] prmP00EG3;
       prmP00EG3 = new Object[] {
       new ParDef("DynamicFormTranslationEnglish",GXType.LongVarChar,2097152,0) ,
       new ParDef("DynamicFormTranslationDutch",GXType.LongVarChar,2097152,0) ,
       new ParDef("DynamicFormTranslationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmP00EG4;
       prmP00EG4 = new Object[] {
       new ParDef("DynamicFormTranslationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("DynamicFormTranslationWWpFormI",GXType.Int32,6,0) ,
       new ParDef("DynamicFormTranslationWWPFormV",GXType.Int32,6,0) ,
       new ParDef("DynamicFormTranslationTrnName",GXType.VarChar,400,0) ,
       new ParDef("DynamicFormTranslationAttribut",GXType.VarChar,40,0) ,
       new ParDef("DynamicFormTranslationEnglish",GXType.LongVarChar,2097152,0) ,
       new ParDef("DynamicFormTranslationDutch",GXType.LongVarChar,2097152,0)
       };
       Object[] prmP00EG5;
       prmP00EG5 = new Object[] {
       new ParDef("AV22SDT__1Dynamicformtranslat",GXType.Int32,6,0) ,
       new ParDef("AV22SDT__2Dynamicformtranslat",GXType.Int32,6,0) ,
       new ParDef("AV22SDT__3Dynamicformtranslat",GXType.VarChar,400,0) ,
       new ParDef("AV22SDT__4Dynamicformtranslat",GXType.Int32,6,0)
       };
       Object[] prmP00EG6;
       prmP00EG6 = new Object[] {
       new ParDef("DynamicFormTranslationEnglish",GXType.LongVarChar,2097152,0) ,
       new ParDef("DynamicFormTranslationDutch",GXType.LongVarChar,2097152,0) ,
       new ParDef("DynamicFormTranslationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmP00EG7;
       prmP00EG7 = new Object[] {
       new ParDef("DynamicFormTranslationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("DynamicFormTranslationWWpFormI",GXType.Int32,6,0) ,
       new ParDef("DynamicFormTranslationWWPFormV",GXType.Int32,6,0) ,
       new ParDef("DynamicFormTranslationWWPFormE",GXType.Int32,6,0) ,
       new ParDef("DynamicFormTranslationTrnName",GXType.VarChar,400,0) ,
       new ParDef("DynamicFormTranslationAttribut",GXType.VarChar,40,0) ,
       new ParDef("DynamicFormTranslationEnglish",GXType.LongVarChar,2097152,0) ,
       new ParDef("DynamicFormTranslationDutch",GXType.LongVarChar,2097152,0)
       };
       def= new CursorDef[] {
           new CursorDef("P00EG2", "SELECT DynamicFormTranslationTrnName, DynamicFormTranslationWWPFormV, DynamicFormTranslationWWpFormI, DynamicFormTranslationEnglish, DynamicFormTranslationDutch, DynamicFormTranslationId FROM Trn_DynamicFormTranslation WHERE (DynamicFormTranslationWWpFormI = :AV22SDT__1Dynamicformtranslat) AND (DynamicFormTranslationWWPFormV = :AV22SDT__2Dynamicformtranslat) AND (DynamicFormTranslationTrnName = ( :AV22SDT__3Dynamicformtranslat)) ORDER BY DynamicFormTranslationId  FOR UPDATE OF Trn_DynamicFormTranslation",true, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00EG2,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("P00EG3", "SAVEPOINT gxupdate;UPDATE Trn_DynamicFormTranslation SET DynamicFormTranslationEnglish=:DynamicFormTranslationEnglish, DynamicFormTranslationDutch=:DynamicFormTranslationDutch  WHERE DynamicFormTranslationId = :DynamicFormTranslationId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK,prmP00EG3)
          ,new CursorDef("P00EG4", "SAVEPOINT gxupdate;INSERT INTO Trn_DynamicFormTranslation(DynamicFormTranslationId, DynamicFormTranslationWWpFormI, DynamicFormTranslationWWPFormV, DynamicFormTranslationTrnName, DynamicFormTranslationAttribut, DynamicFormTranslationEnglish, DynamicFormTranslationDutch, DynamicFormTranslationWWPFormE) VALUES(:DynamicFormTranslationId, :DynamicFormTranslationWWpFormI, :DynamicFormTranslationWWPFormV, :DynamicFormTranslationTrnName, :DynamicFormTranslationAttribut, :DynamicFormTranslationEnglish, :DynamicFormTranslationDutch, 0);RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_MASKLOOPLOCK,prmP00EG4)
          ,new CursorDef("P00EG5", "SELECT DynamicFormTranslationWWPFormE, DynamicFormTranslationTrnName, DynamicFormTranslationWWPFormV, DynamicFormTranslationWWpFormI, DynamicFormTranslationEnglish, DynamicFormTranslationDutch, DynamicFormTranslationId FROM Trn_DynamicFormTranslation WHERE (DynamicFormTranslationWWpFormI = :AV22SDT__1Dynamicformtranslat) AND (DynamicFormTranslationWWPFormV = :AV22SDT__2Dynamicformtranslat) AND (DynamicFormTranslationTrnName = ( :AV22SDT__3Dynamicformtranslat)) AND (DynamicFormTranslationWWPFormE = :AV22SDT__4Dynamicformtranslat) ORDER BY DynamicFormTranslationId  FOR UPDATE OF Trn_DynamicFormTranslation",true, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00EG5,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("P00EG6", "SAVEPOINT gxupdate;UPDATE Trn_DynamicFormTranslation SET DynamicFormTranslationEnglish=:DynamicFormTranslationEnglish, DynamicFormTranslationDutch=:DynamicFormTranslationDutch  WHERE DynamicFormTranslationId = :DynamicFormTranslationId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK,prmP00EG6)
          ,new CursorDef("P00EG7", "SAVEPOINT gxupdate;INSERT INTO Trn_DynamicFormTranslation(DynamicFormTranslationId, DynamicFormTranslationWWpFormI, DynamicFormTranslationWWPFormV, DynamicFormTranslationWWPFormE, DynamicFormTranslationTrnName, DynamicFormTranslationAttribut, DynamicFormTranslationEnglish, DynamicFormTranslationDutch) VALUES(:DynamicFormTranslationId, :DynamicFormTranslationWWpFormI, :DynamicFormTranslationWWPFormV, :DynamicFormTranslationWWPFormE, :DynamicFormTranslationTrnName, :DynamicFormTranslationAttribut, :DynamicFormTranslationEnglish, :DynamicFormTranslationDutch);RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_MASKLOOPLOCK,prmP00EG7)
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
             ((int[]) buf[1])[0] = rslt.getInt(2);
             ((int[]) buf[2])[0] = rslt.getInt(3);
             ((string[]) buf[3])[0] = rslt.getLongVarchar(4);
             ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
             ((Guid[]) buf[5])[0] = rslt.getGuid(6);
             return;
          case 3 :
             ((int[]) buf[0])[0] = rslt.getInt(1);
             ((string[]) buf[1])[0] = rslt.getVarchar(2);
             ((int[]) buf[2])[0] = rslt.getInt(3);
             ((int[]) buf[3])[0] = rslt.getInt(4);
             ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
             ((string[]) buf[5])[0] = rslt.getLongVarchar(6);
             ((Guid[]) buf[6])[0] = rslt.getGuid(7);
             return;
    }
 }

}

}
