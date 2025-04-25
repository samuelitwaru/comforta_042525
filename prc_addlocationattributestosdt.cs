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
   public class prc_addlocationattributestosdt : GXProcedure
   {
      public prc_addlocationattributestosdt( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_addlocationattributestosdt( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_LocationId ,
                           out SdtSDT_TrnAttributes aP1_SDT_TrnAttributes )
      {
         this.AV9LocationId = aP0_LocationId;
         this.AV10SDT_TrnAttributes = new SdtSDT_TrnAttributes(context) ;
         initialize();
         ExecuteImpl();
         aP1_SDT_TrnAttributes=this.AV10SDT_TrnAttributes;
      }

      public SdtSDT_TrnAttributes executeUdp( Guid aP0_LocationId )
      {
         execute(aP0_LocationId, out aP1_SDT_TrnAttributes);
         return AV10SDT_TrnAttributes ;
      }

      public void executeSubmit( Guid aP0_LocationId ,
                                 out SdtSDT_TrnAttributes aP1_SDT_TrnAttributes )
      {
         this.AV9LocationId = aP0_LocationId;
         this.AV10SDT_TrnAttributes = new SdtSDT_TrnAttributes(context) ;
         SubmitImpl();
         aP1_SDT_TrnAttributes=this.AV10SDT_TrnAttributes;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Using cursor P00DT2 */
         pr_default.execute(0, new Object[] {AV9LocationId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A29LocationId = P00DT2_A29LocationId[0];
            A36LocationDescription = P00DT2_A36LocationDescription[0];
            A11OrganisationId = P00DT2_A11OrganisationId[0];
            AV10SDT_TrnAttributes = new SdtSDT_TrnAttributes(context);
            AV10SDT_TrnAttributes.gxTpr_Trnname = "Trn_Location";
            AV10SDT_TrnAttributes.gxTpr_Transaction.gxTpr_Primarykeyid = AV9LocationId;
            AV8Attribute = new SdtSDT_TrnAttributes_Transaction_AttributeItem(context);
            AV8Attribute.gxTpr_Attributename = "LocationDescription";
            AV8Attribute.gxTpr_Attributevalue = A36LocationDescription;
            AV10SDT_TrnAttributes.gxTpr_Transaction.gxTpr_Attribute.Add(AV8Attribute, 0);
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
         AV10SDT_TrnAttributes = new SdtSDT_TrnAttributes(context);
         P00DT2_A29LocationId = new Guid[] {Guid.Empty} ;
         P00DT2_A36LocationDescription = new string[] {""} ;
         P00DT2_A11OrganisationId = new Guid[] {Guid.Empty} ;
         A29LocationId = Guid.Empty;
         A36LocationDescription = "";
         A11OrganisationId = Guid.Empty;
         AV8Attribute = new SdtSDT_TrnAttributes_Transaction_AttributeItem(context);
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_addlocationattributestosdt__default(),
            new Object[][] {
                new Object[] {
               P00DT2_A29LocationId, P00DT2_A36LocationDescription, P00DT2_A11OrganisationId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private string A36LocationDescription ;
      private Guid AV9LocationId ;
      private Guid A29LocationId ;
      private Guid A11OrganisationId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private SdtSDT_TrnAttributes AV10SDT_TrnAttributes ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00DT2_A29LocationId ;
      private string[] P00DT2_A36LocationDescription ;
      private Guid[] P00DT2_A11OrganisationId ;
      private SdtSDT_TrnAttributes_Transaction_AttributeItem AV8Attribute ;
      private SdtSDT_TrnAttributes aP1_SDT_TrnAttributes ;
   }

   public class prc_addlocationattributestosdt__default : DataStoreHelperBase, IDataStoreHelper
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
          Object[] prmP00DT2;
          prmP00DT2 = new Object[] {
          new ParDef("AV9LocationId",GXType.UniqueIdentifier,36,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00DT2", "SELECT LocationId, LocationDescription, OrganisationId FROM Trn_Location WHERE LocationId = :AV9LocationId ORDER BY LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00DT2,100, GxCacheFrequency.OFF ,false,false )
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
                ((Guid[]) buf[2])[0] = rslt.getGuid(3);
                return;
       }
    }

 }

}
