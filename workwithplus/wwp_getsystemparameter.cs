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
namespace GeneXus.Programs.workwithplus {
   public class wwp_getsystemparameter : GXProcedure
   {
      public wwp_getsystemparameter( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public wwp_getsystemparameter( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_WWPParameterKey ,
                           out string aP1_WWPParameterValue )
      {
         this.AV8WWPParameterKey = aP0_WWPParameterKey;
         this.AV9WWPParameterValue = "" ;
         initialize();
         ExecuteImpl();
         aP1_WWPParameterValue=this.AV9WWPParameterValue;
      }

      public string executeUdp( string aP0_WWPParameterKey )
      {
         execute(aP0_WWPParameterKey, out aP1_WWPParameterValue);
         return AV9WWPParameterValue ;
      }

      public void executeSubmit( string aP0_WWPParameterKey ,
                                 out string aP1_WWPParameterValue )
      {
         this.AV8WWPParameterKey = aP0_WWPParameterKey;
         this.AV9WWPParameterValue = "" ;
         SubmitImpl();
         aP1_WWPParameterValue=this.AV9WWPParameterValue;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Using cursor P002T2 */
         pr_default.execute(0, new Object[] {AV8WWPParameterKey});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A106WWPParameterKey = P002T2_A106WWPParameterKey[0];
            A107WWPParameterValue = P002T2_A107WWPParameterValue[0];
            AV9WWPParameterValue = A107WWPParameterValue;
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
         AV9WWPParameterValue = "";
         P002T2_A106WWPParameterKey = new string[] {""} ;
         P002T2_A107WWPParameterValue = new string[] {""} ;
         A106WWPParameterKey = "";
         A107WWPParameterValue = "";
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.workwithplus.wwp_getsystemparameter__default(),
            new Object[][] {
                new Object[] {
               P002T2_A106WWPParameterKey, P002T2_A107WWPParameterValue
               }
            }
         );
         /* GeneXus formulas. */
      }

      private string AV9WWPParameterValue ;
      private string A107WWPParameterValue ;
      private string AV8WWPParameterKey ;
      private string A106WWPParameterKey ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private IDataStoreProvider pr_default ;
      private string[] P002T2_A106WWPParameterKey ;
      private string[] P002T2_A107WWPParameterValue ;
      private string aP1_WWPParameterValue ;
   }

   public class wwp_getsystemparameter__default : DataStoreHelperBase, IDataStoreHelper
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
          Object[] prmP002T2;
          prmP002T2 = new Object[] {
          new ParDef("AV8WWPParameterKey",GXType.VarChar,300,0)
          };
          def= new CursorDef[] {
              new CursorDef("P002T2", "SELECT WWPParameterKey, WWPParameterValue FROM WWP_Parameter WHERE WWPParameterKey = ( :AV8WWPParameterKey) ORDER BY WWPParameterKey ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP002T2,1, GxCacheFrequency.OFF ,false,true )
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
                ((string[]) buf[1])[0] = rslt.getLongVarchar(2);
                return;
       }
    }

 }

}
