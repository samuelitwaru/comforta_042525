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
   public class prc_addappversionpagesattributestosdt : GXProcedure
   {
      public prc_addappversionpagesattributestosdt( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_addappversionpagesattributestosdt( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_AppVersionId ,
                           GxSimpleCollection<Guid> aP1_PageIdCollection ,
                           out GXBaseCollection<SdtSDT_TrnAttributes> aP2_SDT_TrnAttributesCollection )
      {
         this.AV11AppVersionId = aP0_AppVersionId;
         this.AV14PageIdCollection = aP1_PageIdCollection;
         this.AV12SDT_TrnAttributesCollection = new GXBaseCollection<SdtSDT_TrnAttributes>( context, "SDT_TrnAttributes", "Comforta_version20") ;
         initialize();
         ExecuteImpl();
         aP2_SDT_TrnAttributesCollection=this.AV12SDT_TrnAttributesCollection;
      }

      public GXBaseCollection<SdtSDT_TrnAttributes> executeUdp( Guid aP0_AppVersionId ,
                                                                GxSimpleCollection<Guid> aP1_PageIdCollection )
      {
         execute(aP0_AppVersionId, aP1_PageIdCollection, out aP2_SDT_TrnAttributesCollection);
         return AV12SDT_TrnAttributesCollection ;
      }

      public void executeSubmit( Guid aP0_AppVersionId ,
                                 GxSimpleCollection<Guid> aP1_PageIdCollection ,
                                 out GXBaseCollection<SdtSDT_TrnAttributes> aP2_SDT_TrnAttributesCollection )
      {
         this.AV11AppVersionId = aP0_AppVersionId;
         this.AV14PageIdCollection = aP1_PageIdCollection;
         this.AV12SDT_TrnAttributesCollection = new GXBaseCollection<SdtSDT_TrnAttributes>( context, "SDT_TrnAttributes", "Comforta_version20") ;
         SubmitImpl();
         aP2_SDT_TrnAttributesCollection=this.AV12SDT_TrnAttributesCollection;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Using cursor P00E42 */
         pr_default.execute(0, new Object[] {AV11AppVersionId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A523AppVersionId = P00E42_A523AppVersionId[0];
            pr_default.dynParam(1, new Object[]{ new Object[]{
                                                 A516PageId ,
                                                 AV14PageIdCollection ,
                                                 A525PageType ,
                                                 A523AppVersionId } ,
                                                 new int[]{
                                                 }
            });
            /* Using cursor P00E43 */
            pr_default.execute(1, new Object[] {A523AppVersionId});
            while ( (pr_default.getStatus(1) != 101) )
            {
               A525PageType = P00E43_A525PageType[0];
               A516PageId = P00E43_A516PageId[0];
               A536PagePublishedStructure = P00E43_A536PagePublishedStructure[0];
               AV10SDT_TrnAttributes = new SdtSDT_TrnAttributes(context);
               AV10SDT_TrnAttributes.gxTpr_Trnname = "Trn_AppVersion.Page";
               AV10SDT_TrnAttributes.gxTpr_Transaction.gxTpr_Pagetypeapp = A525PageType;
               AV10SDT_TrnAttributes.gxTpr_Transaction.gxTpr_Primarykeyid = A516PageId;
               AV8Attribute = new SdtSDT_TrnAttributes_Transaction_AttributeItem(context);
               AV8Attribute.gxTpr_Attributename = "PagePublishedStructure";
               AV8Attribute.gxTpr_Attributevalue = A536PagePublishedStructure;
               AV10SDT_TrnAttributes.gxTpr_Transaction.gxTpr_Attribute.Add(AV8Attribute, 0);
               AV12SDT_TrnAttributesCollection.Add(AV10SDT_TrnAttributes, 0);
               pr_default.readNext(1);
            }
            pr_default.close(1);
            /* Exiting from a For First loop. */
            if (true) break;
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
         AV12SDT_TrnAttributesCollection = new GXBaseCollection<SdtSDT_TrnAttributes>( context, "SDT_TrnAttributes", "Comforta_version20");
         P00E42_A523AppVersionId = new Guid[] {Guid.Empty} ;
         A523AppVersionId = Guid.Empty;
         A516PageId = Guid.Empty;
         A525PageType = "";
         P00E43_A523AppVersionId = new Guid[] {Guid.Empty} ;
         P00E43_A525PageType = new string[] {""} ;
         P00E43_A516PageId = new Guid[] {Guid.Empty} ;
         P00E43_A536PagePublishedStructure = new string[] {""} ;
         A536PagePublishedStructure = "";
         AV10SDT_TrnAttributes = new SdtSDT_TrnAttributes(context);
         AV8Attribute = new SdtSDT_TrnAttributes_Transaction_AttributeItem(context);
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_addappversionpagesattributestosdt__default(),
            new Object[][] {
                new Object[] {
               P00E42_A523AppVersionId
               }
               , new Object[] {
               P00E43_A523AppVersionId, P00E43_A525PageType, P00E43_A516PageId, P00E43_A536PagePublishedStructure
               }
            }
         );
         /* GeneXus formulas. */
      }

      private string A536PagePublishedStructure ;
      private string A525PageType ;
      private Guid AV11AppVersionId ;
      private Guid A523AppVersionId ;
      private Guid A516PageId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GxSimpleCollection<Guid> AV14PageIdCollection ;
      private GXBaseCollection<SdtSDT_TrnAttributes> AV12SDT_TrnAttributesCollection ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00E42_A523AppVersionId ;
      private Guid[] P00E43_A523AppVersionId ;
      private string[] P00E43_A525PageType ;
      private Guid[] P00E43_A516PageId ;
      private string[] P00E43_A536PagePublishedStructure ;
      private SdtSDT_TrnAttributes AV10SDT_TrnAttributes ;
      private SdtSDT_TrnAttributes_Transaction_AttributeItem AV8Attribute ;
      private GXBaseCollection<SdtSDT_TrnAttributes> aP2_SDT_TrnAttributesCollection ;
   }

   public class prc_addappversionpagesattributestosdt__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P00E43( IGxContext context ,
                                             Guid A516PageId ,
                                             GxSimpleCollection<Guid> AV14PageIdCollection ,
                                             string A525PageType ,
                                             Guid A523AppVersionId )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int1 = new short[1];
         Object[] GXv_Object2 = new Object[2];
         scmdbuf = "SELECT AppVersionId, PageType, PageId, PagePublishedStructure FROM Trn_AppVersionPage";
         AddWhere(sWhereString, "(AppVersionId = :AppVersionId)");
         AddWhere(sWhereString, "("+new GxDbmsUtils( new GxPostgreSql()).ValueList(AV14PageIdCollection, "PageId IN (", ")")+")");
         AddWhere(sWhereString, "(( PageType = ( 'Menu')) or ( PageType = ( 'MyCare')) or ( PageType = ( 'MyLiving')) or ( PageType = ( 'MyService')))");
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY AppVersionId";
         GXv_Object2[0] = scmdbuf;
         GXv_Object2[1] = GXv_int1;
         return GXv_Object2 ;
      }

      public override Object [] getDynamicStatement( int cursor ,
                                                     IGxContext context ,
                                                     Object [] dynConstraints )
      {
         switch ( cursor )
         {
               case 1 :
                     return conditional_P00E43(context, (Guid)dynConstraints[0] , (GxSimpleCollection<Guid>)dynConstraints[1] , (string)dynConstraints[2] , (Guid)dynConstraints[3] );
         }
         return base.getDynamicStatement(cursor, context, dynConstraints);
      }

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
          Object[] prmP00E42;
          prmP00E42 = new Object[] {
          new ParDef("AV11AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00E43;
          prmP00E43 = new Object[] {
          new ParDef("AppVersionId",GXType.UniqueIdentifier,36,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00E42", "SELECT AppVersionId FROM Trn_AppVersion WHERE AppVersionId = :AV11AppVersionId ORDER BY AppVersionId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00E42,1, GxCacheFrequency.OFF ,true,true )
             ,new CursorDef("P00E43", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00E43,100, GxCacheFrequency.OFF ,false,false )
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
                ((string[]) buf[3])[0] = rslt.getLongVarchar(4);
                return;
       }
    }

 }

}
