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
namespace GeneXus.Programs.workwithplus.dynamicforms {
   public class wwp_df_export : GXWebProcedure
   {
      public override void webExecute( )
      {
         context.SetDefaultTheme("WorkWithPlusDS", true);
         initialize();
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         if ( nGotPars == 0 )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetFirstPar( "WWPFormId");
            if ( ! entryPointCalled )
            {
               AV13WWPFormId = (short)(Math.Round(NumberUtil.Val( gxfirstwebparm, "."), 18, MidpointRounding.ToEven));
            }
         }
         if ( GxWebError == 0 )
         {
            ExecutePrivate();
         }
         cleanup();
      }

      public wwp_df_export( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public wwp_df_export( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( short aP0_WWPFormId )
      {
         this.AV13WWPFormId = aP0_WWPFormId;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( short aP0_WWPFormId )
      {
         this.AV13WWPFormId = aP0_WWPFormId;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Using cursor P004I2 */
         pr_default.execute(0, new Object[] {AV13WWPFormId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A206WWPFormId = P004I2_A206WWPFormId[0];
            A208WWPFormReferenceName = P004I2_A208WWPFormReferenceName[0];
            A207WWPFormVersionNumber = P004I2_A207WWPFormVersionNumber[0];
            AV9WWPForm.Load(A206WWPFormId, A207WWPFormVersionNumber);
            AV14WWPFormReferenceName = A208WWPFormReferenceName;
            /* Exit For each command. Update data (if necessary), close cursors & exit. */
            if (true) break;
            pr_default.readNext(0);
         }
         pr_default.close(0);
         if ( AV9WWPForm.Success() )
         {
            AV8TextFile.Source = AV14WWPFormReferenceName+".json";
            AV8TextFile.WriteAllText(AV9WWPForm.ToJSonString(true, true), "");
            /* Execute user subroutine: 'CHECKSTATUS' */
            S111 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
            if ( AV8TextFile.ErrCode == 0 )
            {
               if ( ! context.isAjaxRequest( ) )
               {
                  AV12HttpResponse.AppendHeader("Content-Type", "application/json");
               }
               if ( ! context.isAjaxRequest( ) )
               {
                  AV12HttpResponse.AppendHeader("Content-Disposition", StringUtil.Format( "attachment;filename=%1.json", AV14WWPFormReferenceName, "", "", "", "", "", "", "", ""));
               }
               AV12HttpResponse.AddFile(AV8TextFile.GetAbsoluteName());
            }
         }
         if ( context.WillRedirect( ) )
         {
            context.Redirect( context.wjLoc );
            context.wjLoc = "";
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'CHECKSTATUS' Routine */
         returnInSub = false;
         if ( AV8TextFile.ErrCode != 0 )
         {
            AV11Filename = "";
            AV10ErrorMessage = AV8TextFile.ErrDescription;
            AV8TextFile.Close();
            AV12HttpResponse.AddString(AV10ErrorMessage);
            context.nUserReturn = 1;
            if ( context.WillRedirect( ) )
            {
               context.Redirect( context.wjLoc );
               context.wjLoc = "";
            }
            returnInSub = true;
            if (true) return;
         }
      }

      public override void cleanup( )
      {
         CloseCursors();
         base.cleanup();
         if ( IsMain )
         {
            context.CloseConnections();
         }
         ExitApp();
      }

      public override void initialize( )
      {
         GXKey = "";
         gxfirstwebparm = "";
         P004I2_A206WWPFormId = new short[1] ;
         P004I2_A208WWPFormReferenceName = new string[] {""} ;
         P004I2_A207WWPFormVersionNumber = new short[1] ;
         A208WWPFormReferenceName = "";
         AV9WWPForm = new GeneXus.Programs.workwithplus.dynamicforms.SdtWWP_Form(context);
         AV14WWPFormReferenceName = "";
         AV8TextFile = new GxFile(context.GetPhysicalPath());
         AV12HttpResponse = new GxHttpResponse( context);
         AV11Filename = "";
         AV10ErrorMessage = "";
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.workwithplus.dynamicforms.wwp_df_export__default(),
            new Object[][] {
                new Object[] {
               P004I2_A206WWPFormId, P004I2_A208WWPFormReferenceName, P004I2_A207WWPFormVersionNumber
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short gxcookieaux ;
      private short nGotPars ;
      private short AV13WWPFormId ;
      private short GxWebError ;
      private short A206WWPFormId ;
      private short A207WWPFormVersionNumber ;
      private string GXKey ;
      private string gxfirstwebparm ;
      private bool entryPointCalled ;
      private bool returnInSub ;
      private string A208WWPFormReferenceName ;
      private string AV14WWPFormReferenceName ;
      private string AV11Filename ;
      private string AV10ErrorMessage ;
      private GxHttpResponse AV12HttpResponse ;
      private GxFile AV8TextFile ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private IDataStoreProvider pr_default ;
      private short[] P004I2_A206WWPFormId ;
      private string[] P004I2_A208WWPFormReferenceName ;
      private short[] P004I2_A207WWPFormVersionNumber ;
      private GeneXus.Programs.workwithplus.dynamicforms.SdtWWP_Form AV9WWPForm ;
   }

   public class wwp_df_export__default : DataStoreHelperBase, IDataStoreHelper
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
          Object[] prmP004I2;
          prmP004I2 = new Object[] {
          new ParDef("AV13WWPFormId",GXType.Int16,4,0)
          };
          def= new CursorDef[] {
              new CursorDef("P004I2", "SELECT WWPFormId, WWPFormReferenceName, WWPFormVersionNumber FROM WWP_Form WHERE WWPFormId = :AV13WWPFormId ORDER BY WWPFormId DESC, WWPFormVersionNumber DESC ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP004I2,1, GxCacheFrequency.OFF ,true,true )
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
                ((short[]) buf[0])[0] = rslt.getShort(1);
                ((string[]) buf[1])[0] = rslt.getVarchar(2);
                ((short[]) buf[2])[0] = rslt.getShort(3);
                return;
       }
    }

 }

}
