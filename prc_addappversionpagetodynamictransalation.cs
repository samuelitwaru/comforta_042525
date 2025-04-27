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
   public class prc_addappversionpagetodynamictransalation : GXProcedure
   {
      public prc_addappversionpagetodynamictransalation( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_addappversionpagetodynamictransalation( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( GXBaseCollection<SdtSDT_TrnAttributes> aP0_SDT_TrnAttributesCollection )
      {
         this.AV14SDT_TrnAttributesCollection = aP0_SDT_TrnAttributesCollection;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( GXBaseCollection<SdtSDT_TrnAttributes> aP0_SDT_TrnAttributesCollection )
      {
         this.AV14SDT_TrnAttributesCollection = aP0_SDT_TrnAttributesCollection;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9Language = context.GetLanguage( );
         if ( StringUtil.StrCmp(AV9Language, "English") == 0 )
         {
            AV12LanguageCode = "en";
         }
         else if ( StringUtil.StrCmp(AV9Language, "Dutch") == 0 )
         {
            AV12LanguageCode = "nl";
         }
         new prc_logtoserver(context ).execute(  AV14SDT_TrnAttributesCollection.ToJSonString(false)) ;
         AV21GXV1 = 1;
         while ( AV21GXV1 <= AV14SDT_TrnAttributesCollection.Count )
         {
            AV8SDT_TrnAttributes = ((SdtSDT_TrnAttributes)AV14SDT_TrnAttributesCollection.Item(AV21GXV1));
            new prc_logtoserver(context ).execute(  context.GetMessage( "&SDT_TrnAttributes: ", "")+AV8SDT_TrnAttributes.gxTpr_Transaction.gxTpr_Primarykeyid.ToString()) ;
            AV22GXLvl14 = 0;
            /* Using cursor P00E52 */
            pr_default.execute(0, new Object[] {AV8SDT_TrnAttributes.gxTpr_Transaction.gxTpr_Primarykeyid});
            while ( (pr_default.getStatus(0) != 101) )
            {
               GXTE52 = 0;
               A580DynamicTranslationPrimaryKey = P00E52_A580DynamicTranslationPrimaryKey[0];
               A582DynamicTranslationEnglish = P00E52_A582DynamicTranslationEnglish[0];
               A583DynamicTranslationDutch = P00E52_A583DynamicTranslationDutch[0];
               A578DynamicTranslationId = P00E52_A578DynamicTranslationId[0];
               AV22GXLvl14 = 1;
               new prc_logtoserver(context ).execute(  context.GetMessage( "	Found: ", "")+A580DynamicTranslationPrimaryKey.ToString()) ;
               AV23GXV2 = 1;
               while ( AV23GXV2 <= AV8SDT_TrnAttributes.gxTpr_Transaction.gxTpr_Attribute.Count )
               {
                  AV11Attribute = ((SdtSDT_TrnAttributes_Transaction_AttributeItem)AV8SDT_TrnAttributes.gxTpr_Transaction.gxTpr_Attribute.Item(AV23GXV2));
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11Attribute.gxTpr_Attributevalue)) )
                  {
                     /* Execute user subroutine: 'TRANSLATE' */
                     S111 ();
                     if ( returnInSub )
                     {
                        pr_default.close(0);
                        cleanup();
                        if (true) return;
                     }
                     A582DynamicTranslationEnglish = AV19DynamicTranslationEnglish;
                     A583DynamicTranslationDutch = AV20DynamicTranslationDutch;
                     GXTE52 = 1;
                  }
                  AV23GXV2 = (int)(AV23GXV2+1);
               }
               /* Using cursor P00E53 */
               pr_default.execute(1, new Object[] {A582DynamicTranslationEnglish, A583DynamicTranslationDutch, A578DynamicTranslationId});
               pr_default.close(1);
               pr_default.SmartCacheProvider.SetUpdated("Trn_DynamicTranslation");
               if ( GXTE52 == 1 )
               {
                  context.CommitDataStores("prc_addappversionpagetodynamictransalation",pr_default);
               }
               pr_default.readNext(0);
            }
            pr_default.close(0);
            if ( AV22GXLvl14 == 0 )
            {
               new prc_logtoserver(context ).execute(  context.GetMessage( "	Not Found: ", "")+AV8SDT_TrnAttributes.gxTpr_Transaction.gxTpr_Primarykeyid.ToString()+" : "+AV8SDT_TrnAttributes.gxTpr_Transaction.gxTpr_Pagetypeapp) ;
               AV24GXV3 = 1;
               while ( AV24GXV3 <= AV8SDT_TrnAttributes.gxTpr_Transaction.gxTpr_Attribute.Count )
               {
                  AV11Attribute = ((SdtSDT_TrnAttributes_Transaction_AttributeItem)AV8SDT_TrnAttributes.gxTpr_Transaction.gxTpr_Attribute.Item(AV24GXV3));
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11Attribute.gxTpr_Attributevalue)) )
                  {
                     /* Execute user subroutine: 'TRANSLATE' */
                     S111 ();
                     if ( returnInSub )
                     {
                        cleanup();
                        if (true) return;
                     }
                     /*
                        INSERT RECORD ON TABLE Trn_DynamicTranslation

                     */
                     A579DynamicTranslationTrnName = AV8SDT_TrnAttributes.gxTpr_Trnname;
                     A580DynamicTranslationPrimaryKey = AV8SDT_TrnAttributes.gxTpr_Transaction.gxTpr_Primarykeyid;
                     A581DynamicTranslationAttributeNam = AV11Attribute.gxTpr_Attributename;
                     A582DynamicTranslationEnglish = AV19DynamicTranslationEnglish;
                     A583DynamicTranslationDutch = AV20DynamicTranslationDutch;
                     A578DynamicTranslationId = Guid.NewGuid( );
                     /* Using cursor P00E54 */
                     pr_default.execute(2, new Object[] {A578DynamicTranslationId, A579DynamicTranslationTrnName, A580DynamicTranslationPrimaryKey, A581DynamicTranslationAttributeNam, A582DynamicTranslationEnglish, A583DynamicTranslationDutch});
                     pr_default.close(2);
                     pr_default.SmartCacheProvider.SetUpdated("Trn_DynamicTranslation");
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
                     context.CommitDataStores("prc_addappversionpagetodynamictransalation",pr_default);
                  }
                  AV24GXV3 = (int)(AV24GXV3+1);
               }
            }
            AV21GXV1 = (int)(AV21GXV1+1);
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'TRANSLATE' Routine */
         returnInSub = false;
         AV16SDT_MenuPage = new SdtSDT_MenuPage(context);
         AV16SDT_MenuPage.FromJSonString(AV11Attribute.gxTpr_Attributevalue, null);
         if ( StringUtil.StrCmp(AV9Language, "English") == 0 )
         {
            AV19DynamicTranslationEnglish = AV11Attribute.gxTpr_Attributevalue;
            AV25GXV4 = 1;
            while ( AV25GXV4 <= AV16SDT_MenuPage.gxTpr_Rows.Count )
            {
               AV15RowItem = ((SdtSDT_MenuPage_RowsItem)AV16SDT_MenuPage.gxTpr_Rows.Item(AV25GXV4));
               AV26GXV5 = 1;
               while ( AV26GXV5 <= AV15RowItem.gxTpr_Tiles.Count )
               {
                  AV17Tile = ((SdtSDT_MenuPage_RowsItem_TilesItem)AV15RowItem.gxTpr_Tiles.Item(AV26GXV5));
                  GXt_char1 = "";
                  new prc_translatelanguage(context ).execute(  AV12LanguageCode,  context.GetMessage( "nl", ""),  AV17Tile.gxTpr_Text, out  GXt_char1) ;
                  AV17Tile.gxTpr_Text = GXt_char1;
                  AV17Tile.gxTpr_Name = AV17Tile.gxTpr_Text;
                  AV26GXV5 = (int)(AV26GXV5+1);
               }
               AV25GXV4 = (int)(AV25GXV4+1);
            }
            AV20DynamicTranslationDutch = AV16SDT_MenuPage.ToJSonString(false, true);
         }
         else if ( StringUtil.StrCmp(AV9Language, "Dutch") == 0 )
         {
            AV20DynamicTranslationDutch = AV11Attribute.gxTpr_Attributevalue;
            AV27GXV6 = 1;
            while ( AV27GXV6 <= AV16SDT_MenuPage.gxTpr_Rows.Count )
            {
               AV15RowItem = ((SdtSDT_MenuPage_RowsItem)AV16SDT_MenuPage.gxTpr_Rows.Item(AV27GXV6));
               AV28GXV7 = 1;
               while ( AV28GXV7 <= AV15RowItem.gxTpr_Tiles.Count )
               {
                  AV17Tile = ((SdtSDT_MenuPage_RowsItem_TilesItem)AV15RowItem.gxTpr_Tiles.Item(AV28GXV7));
                  GXt_char1 = "";
                  new prc_translatelanguage(context ).execute(  AV12LanguageCode,  context.GetMessage( "en", ""),  AV17Tile.gxTpr_Text, out  GXt_char1) ;
                  AV17Tile.gxTpr_Text = GXt_char1;
                  AV17Tile.gxTpr_Name = AV17Tile.gxTpr_Text;
                  AV28GXV7 = (int)(AV28GXV7+1);
               }
               AV27GXV6 = (int)(AV27GXV6+1);
            }
            AV19DynamicTranslationEnglish = AV16SDT_MenuPage.ToJSonString(false, true);
         }
         new prc_logtoserver(context ).execute(  context.GetMessage( "		Translated ", "")+AV11Attribute.gxTpr_Attributename+"("+AV8SDT_TrnAttributes.gxTpr_Transaction.gxTpr_Primarykeyid.ToString()+context.GetMessage( ") from ", "")+AV9Language+" : "+AV20DynamicTranslationDutch) ;
      }

      public override void cleanup( )
      {
         context.CommitDataStores("prc_addappversionpagetodynamictransalation",pr_default);
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
         AV8SDT_TrnAttributes = new SdtSDT_TrnAttributes(context);
         P00E52_A580DynamicTranslationPrimaryKey = new Guid[] {Guid.Empty} ;
         P00E52_A582DynamicTranslationEnglish = new string[] {""} ;
         P00E52_A583DynamicTranslationDutch = new string[] {""} ;
         P00E52_A578DynamicTranslationId = new Guid[] {Guid.Empty} ;
         A580DynamicTranslationPrimaryKey = Guid.Empty;
         A582DynamicTranslationEnglish = "";
         A583DynamicTranslationDutch = "";
         A578DynamicTranslationId = Guid.Empty;
         AV11Attribute = new SdtSDT_TrnAttributes_Transaction_AttributeItem(context);
         AV19DynamicTranslationEnglish = "";
         AV20DynamicTranslationDutch = "";
         A579DynamicTranslationTrnName = "";
         A581DynamicTranslationAttributeNam = "";
         Gx_emsg = "";
         AV16SDT_MenuPage = new SdtSDT_MenuPage(context);
         AV15RowItem = new SdtSDT_MenuPage_RowsItem(context);
         AV17Tile = new SdtSDT_MenuPage_RowsItem_TilesItem(context);
         GXt_char1 = "";
         pr_datastore1 = new DataStoreProvider(context, new GeneXus.Programs.prc_addappversionpagetodynamictransalation__datastore1(),
            new Object[][] {
            }
         );
         pr_gam = new DataStoreProvider(context, new GeneXus.Programs.prc_addappversionpagetodynamictransalation__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_addappversionpagetodynamictransalation__default(),
            new Object[][] {
                new Object[] {
               P00E52_A580DynamicTranslationPrimaryKey, P00E52_A582DynamicTranslationEnglish, P00E52_A583DynamicTranslationDutch, P00E52_A578DynamicTranslationId
               }
               , new Object[] {
               }
               , new Object[] {
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV22GXLvl14 ;
      private short GXTE52 ;
      private int AV21GXV1 ;
      private int AV23GXV2 ;
      private int AV24GXV3 ;
      private int GX_INS104 ;
      private int AV25GXV4 ;
      private int AV26GXV5 ;
      private int AV27GXV6 ;
      private int AV28GXV7 ;
      private string AV12LanguageCode ;
      private string Gx_emsg ;
      private string GXt_char1 ;
      private bool returnInSub ;
      private string A582DynamicTranslationEnglish ;
      private string A583DynamicTranslationDutch ;
      private string AV19DynamicTranslationEnglish ;
      private string AV20DynamicTranslationDutch ;
      private string AV9Language ;
      private string A579DynamicTranslationTrnName ;
      private string A581DynamicTranslationAttributeNam ;
      private Guid A580DynamicTranslationPrimaryKey ;
      private Guid A578DynamicTranslationId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<SdtSDT_TrnAttributes> AV14SDT_TrnAttributesCollection ;
      private SdtSDT_TrnAttributes AV8SDT_TrnAttributes ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00E52_A580DynamicTranslationPrimaryKey ;
      private string[] P00E52_A582DynamicTranslationEnglish ;
      private string[] P00E52_A583DynamicTranslationDutch ;
      private Guid[] P00E52_A578DynamicTranslationId ;
      private SdtSDT_TrnAttributes_Transaction_AttributeItem AV11Attribute ;
      private SdtSDT_MenuPage AV16SDT_MenuPage ;
      private SdtSDT_MenuPage_RowsItem AV15RowItem ;
      private SdtSDT_MenuPage_RowsItem_TilesItem AV17Tile ;
      private IDataStoreProvider pr_datastore1 ;
      private IDataStoreProvider pr_gam ;
   }

   public class prc_addappversionpagetodynamictransalation__datastore1 : DataStoreHelperBase, IDataStoreHelper
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

 public class prc_addappversionpagetodynamictransalation__gam : DataStoreHelperBase, IDataStoreHelper
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

public class prc_addappversionpagetodynamictransalation__default : DataStoreHelperBase, IDataStoreHelper
{
   public ICursor[] getCursors( )
   {
      cursorDefinitions();
      return new Cursor[] {
       new ForEachCursor(def[0])
      ,new UpdateCursor(def[1])
      ,new UpdateCursor(def[2])
    };
 }

 private static CursorDef[] def;
 private void cursorDefinitions( )
 {
    if ( def == null )
    {
       Object[] prmP00E52;
       prmP00E52 = new Object[] {
       new ParDef("AV8SDT_T_1Transaction_1Primar",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmP00E53;
       prmP00E53 = new Object[] {
       new ParDef("DynamicTranslationEnglish",GXType.LongVarChar,2097152,0) ,
       new ParDef("DynamicTranslationDutch",GXType.LongVarChar,2097152,0) ,
       new ParDef("DynamicTranslationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmP00E54;
       prmP00E54 = new Object[] {
       new ParDef("DynamicTranslationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("DynamicTranslationTrnName",GXType.VarChar,100,0) ,
       new ParDef("DynamicTranslationPrimaryKey",GXType.UniqueIdentifier,36,0) ,
       new ParDef("DynamicTranslationAttributeNam",GXType.VarChar,100,0) ,
       new ParDef("DynamicTranslationEnglish",GXType.LongVarChar,2097152,0) ,
       new ParDef("DynamicTranslationDutch",GXType.LongVarChar,2097152,0)
       };
       def= new CursorDef[] {
           new CursorDef("P00E52", "SELECT DynamicTranslationPrimaryKey, DynamicTranslationEnglish, DynamicTranslationDutch, DynamicTranslationId FROM Trn_DynamicTranslation WHERE DynamicTranslationPrimaryKey = :AV8SDT_T_1Transaction_1Primar ORDER BY DynamicTranslationId  FOR UPDATE OF Trn_DynamicTranslation",true, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00E52,1, GxCacheFrequency.OFF ,true,false )
          ,new CursorDef("P00E53", "SAVEPOINT gxupdate;UPDATE Trn_DynamicTranslation SET DynamicTranslationEnglish=:DynamicTranslationEnglish, DynamicTranslationDutch=:DynamicTranslationDutch  WHERE DynamicTranslationId = :DynamicTranslationId;RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK,prmP00E53)
          ,new CursorDef("P00E54", "SAVEPOINT gxupdate;INSERT INTO Trn_DynamicTranslation(DynamicTranslationId, DynamicTranslationTrnName, DynamicTranslationPrimaryKey, DynamicTranslationAttributeNam, DynamicTranslationEnglish, DynamicTranslationDutch) VALUES(:DynamicTranslationId, :DynamicTranslationTrnName, :DynamicTranslationPrimaryKey, :DynamicTranslationAttributeNam, :DynamicTranslationEnglish, :DynamicTranslationDutch);RELEASE SAVEPOINT gxupdate", GxErrorMask.GX_ROLLBACKSAVEPOINT | GxErrorMask.GX_MASKLOOPLOCK,prmP00E54)
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
             ((string[]) buf[1])[0] = rslt.getLongVarchar(2);
             ((string[]) buf[2])[0] = rslt.getLongVarchar(3);
             ((Guid[]) buf[3])[0] = rslt.getGuid(4);
             return;
    }
 }

}

}
