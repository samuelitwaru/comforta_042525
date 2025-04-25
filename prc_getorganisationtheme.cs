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
   public class prc_getorganisationtheme : GXProcedure
   {
      public prc_getorganisationtheme( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_getorganisationtheme( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_OrganisationId ,
                           Guid aP1_LocationId ,
                           out GXBCCollection<SdtTrn_Theme> aP2_BC_Trn_ThemeCollection )
      {
         this.AV16OrganisationId = aP0_OrganisationId;
         this.AV26LocationId = aP1_LocationId;
         this.AV23BC_Trn_ThemeCollection = new GXBCCollection<SdtTrn_Theme>( context, "Trn_Theme", "Comforta_version20") ;
         initialize();
         ExecuteImpl();
         aP2_BC_Trn_ThemeCollection=this.AV23BC_Trn_ThemeCollection;
      }

      public GXBCCollection<SdtTrn_Theme> executeUdp( Guid aP0_OrganisationId ,
                                                      Guid aP1_LocationId )
      {
         execute(aP0_OrganisationId, aP1_LocationId, out aP2_BC_Trn_ThemeCollection);
         return AV23BC_Trn_ThemeCollection ;
      }

      public void executeSubmit( Guid aP0_OrganisationId ,
                                 Guid aP1_LocationId ,
                                 out GXBCCollection<SdtTrn_Theme> aP2_BC_Trn_ThemeCollection )
      {
         this.AV16OrganisationId = aP0_OrganisationId;
         this.AV26LocationId = aP1_LocationId;
         this.AV23BC_Trn_ThemeCollection = new GXBCCollection<SdtTrn_Theme>( context, "Trn_Theme", "Comforta_version20") ;
         SubmitImpl();
         aP2_BC_Trn_ThemeCollection=this.AV23BC_Trn_ThemeCollection;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Using cursor P00C32 */
         pr_default.execute(0);
         while ( (pr_default.getStatus(0) != 101) )
         {
            A577ThemeIsPredefined = P00C32_A577ThemeIsPredefined[0];
            A274Trn_ThemeName = P00C32_A274Trn_ThemeName[0];
            A273Trn_ThemeId = P00C32_A273Trn_ThemeId[0];
            n273Trn_ThemeId = P00C32_n273Trn_ThemeId[0];
            AV22BC_Trn_Theme = new SdtTrn_Theme(context);
            AV22BC_Trn_Theme.Load(A273Trn_ThemeId);
            AV22BC_Trn_Theme.gxTpr_Color.Sort("ColorName");
            AV23BC_Trn_ThemeCollection.Add(AV22BC_Trn_Theme, 0);
            pr_default.readNext(0);
         }
         pr_default.close(0);
         /* Using cursor P00C33 */
         pr_default.execute(1, new Object[] {AV26LocationId});
         while ( (pr_default.getStatus(1) != 101) )
         {
            A273Trn_ThemeId = P00C33_A273Trn_ThemeId[0];
            n273Trn_ThemeId = P00C33_n273Trn_ThemeId[0];
            A576LocationThemeId = P00C33_A576LocationThemeId[0];
            n576LocationThemeId = P00C33_n576LocationThemeId[0];
            A29LocationId = P00C33_A29LocationId[0];
            A11OrganisationId = P00C33_A11OrganisationId[0];
            /* Using cursor P00C34 */
            pr_default.execute(2, new Object[] {n576LocationThemeId, A576LocationThemeId});
            while ( (pr_default.getStatus(2) != 101) )
            {
               A273Trn_ThemeId = P00C34_A273Trn_ThemeId[0];
               n273Trn_ThemeId = P00C34_n273Trn_ThemeId[0];
               AV22BC_Trn_Theme = new SdtTrn_Theme(context);
               AV22BC_Trn_Theme.Load(A576LocationThemeId);
               AV22BC_Trn_Theme.gxTpr_Color.Sort("ColorName");
               AV23BC_Trn_ThemeCollection.Add(AV22BC_Trn_Theme, 0);
               /* Exiting from a For First loop. */
               if (true) break;
            }
            pr_default.close(2);
            pr_default.readNext(1);
         }
         pr_default.close(1);
         AV23BC_Trn_ThemeCollection.Sort("Trn_ThemeName");
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
         AV23BC_Trn_ThemeCollection = new GXBCCollection<SdtTrn_Theme>( context, "Trn_Theme", "Comforta_version20");
         P00C32_A577ThemeIsPredefined = new bool[] {false} ;
         P00C32_A274Trn_ThemeName = new string[] {""} ;
         P00C32_A273Trn_ThemeId = new Guid[] {Guid.Empty} ;
         P00C32_n273Trn_ThemeId = new bool[] {false} ;
         A274Trn_ThemeName = "";
         A273Trn_ThemeId = Guid.Empty;
         AV22BC_Trn_Theme = new SdtTrn_Theme(context);
         P00C33_A273Trn_ThemeId = new Guid[] {Guid.Empty} ;
         P00C33_n273Trn_ThemeId = new bool[] {false} ;
         P00C33_A576LocationThemeId = new Guid[] {Guid.Empty} ;
         P00C33_n576LocationThemeId = new bool[] {false} ;
         P00C33_A29LocationId = new Guid[] {Guid.Empty} ;
         P00C33_A11OrganisationId = new Guid[] {Guid.Empty} ;
         A576LocationThemeId = Guid.Empty;
         A29LocationId = Guid.Empty;
         A11OrganisationId = Guid.Empty;
         P00C34_A273Trn_ThemeId = new Guid[] {Guid.Empty} ;
         P00C34_n273Trn_ThemeId = new bool[] {false} ;
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_getorganisationtheme__default(),
            new Object[][] {
                new Object[] {
               P00C32_A577ThemeIsPredefined, P00C32_A274Trn_ThemeName, P00C32_A273Trn_ThemeId
               }
               , new Object[] {
               P00C33_A273Trn_ThemeId, P00C33_n273Trn_ThemeId, P00C33_A576LocationThemeId, P00C33_n576LocationThemeId, P00C33_A29LocationId, P00C33_A11OrganisationId
               }
               , new Object[] {
               P00C34_A273Trn_ThemeId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private bool A577ThemeIsPredefined ;
      private bool n273Trn_ThemeId ;
      private bool n576LocationThemeId ;
      private string A274Trn_ThemeName ;
      private Guid AV16OrganisationId ;
      private Guid AV26LocationId ;
      private Guid A273Trn_ThemeId ;
      private Guid A576LocationThemeId ;
      private Guid A29LocationId ;
      private Guid A11OrganisationId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXBCCollection<SdtTrn_Theme> AV23BC_Trn_ThemeCollection ;
      private IDataStoreProvider pr_default ;
      private bool[] P00C32_A577ThemeIsPredefined ;
      private string[] P00C32_A274Trn_ThemeName ;
      private Guid[] P00C32_A273Trn_ThemeId ;
      private bool[] P00C32_n273Trn_ThemeId ;
      private SdtTrn_Theme AV22BC_Trn_Theme ;
      private Guid[] P00C33_A273Trn_ThemeId ;
      private bool[] P00C33_n273Trn_ThemeId ;
      private Guid[] P00C33_A576LocationThemeId ;
      private bool[] P00C33_n576LocationThemeId ;
      private Guid[] P00C33_A29LocationId ;
      private Guid[] P00C33_A11OrganisationId ;
      private Guid[] P00C34_A273Trn_ThemeId ;
      private bool[] P00C34_n273Trn_ThemeId ;
      private GXBCCollection<SdtTrn_Theme> aP2_BC_Trn_ThemeCollection ;
   }

   public class prc_getorganisationtheme__default : DataStoreHelperBase, IDataStoreHelper
   {
      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
         ,new ForEachCursor(def[1])
         ,new ForEachCursor(def[2])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP00C32;
          prmP00C32 = new Object[] {
          };
          Object[] prmP00C33;
          prmP00C33 = new Object[] {
          new ParDef("AV26LocationId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00C34;
          prmP00C34 = new Object[] {
          new ParDef("LocationThemeId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          def= new CursorDef[] {
              new CursorDef("P00C32", "SELECT ThemeIsPredefined, Trn_ThemeName, Trn_ThemeId FROM Trn_Theme WHERE (Not (char_length(trim(trailing ' ' from RTRIM(LTRIM(Trn_ThemeName))))=0)) AND (ThemeIsPredefined = TRUE) ORDER BY Trn_ThemeName ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00C32,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00C33", "SELECT Trn_ThemeId, LocationThemeId, LocationId, OrganisationId FROM Trn_Location WHERE LocationId = :AV26LocationId ORDER BY LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00C33,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P00C34", "SELECT Trn_ThemeId FROM Trn_Theme WHERE Trn_ThemeId = :LocationThemeId ORDER BY Trn_ThemeId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00C34,1, GxCacheFrequency.OFF ,true,true )
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
                ((bool[]) buf[0])[0] = rslt.getBool(1);
                ((string[]) buf[1])[0] = rslt.getVarchar(2);
                ((Guid[]) buf[2])[0] = rslt.getGuid(3);
                return;
             case 1 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((bool[]) buf[1])[0] = rslt.wasNull(1);
                ((Guid[]) buf[2])[0] = rslt.getGuid(2);
                ((bool[]) buf[3])[0] = rslt.wasNull(2);
                ((Guid[]) buf[4])[0] = rslt.getGuid(3);
                ((Guid[]) buf[5])[0] = rslt.getGuid(4);
                return;
             case 2 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                return;
       }
    }

 }

}
