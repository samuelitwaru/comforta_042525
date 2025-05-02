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
   public class prc_getlocationtheme : GXProcedure
   {
      public prc_getlocationtheme( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_getlocationtheme( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( ref Guid aP0_LocationId ,
                           ref Guid aP1_OrganisationId ,
                           out SdtSDT_Theme aP2_SDT_Theme )
      {
         this.AV8LocationId = aP0_LocationId;
         this.AV9OrganisationId = aP1_OrganisationId;
         this.AV15SDT_Theme = new SdtSDT_Theme(context) ;
         initialize();
         ExecuteImpl();
         aP0_LocationId=this.AV8LocationId;
         aP1_OrganisationId=this.AV9OrganisationId;
         aP2_SDT_Theme=this.AV15SDT_Theme;
      }

      public SdtSDT_Theme executeUdp( ref Guid aP0_LocationId ,
                                      ref Guid aP1_OrganisationId )
      {
         execute(ref aP0_LocationId, ref aP1_OrganisationId, out aP2_SDT_Theme);
         return AV15SDT_Theme ;
      }

      public void executeSubmit( ref Guid aP0_LocationId ,
                                 ref Guid aP1_OrganisationId ,
                                 out SdtSDT_Theme aP2_SDT_Theme )
      {
         this.AV8LocationId = aP0_LocationId;
         this.AV9OrganisationId = aP1_OrganisationId;
         this.AV15SDT_Theme = new SdtSDT_Theme(context) ;
         SubmitImpl();
         aP0_LocationId=this.AV8LocationId;
         aP1_OrganisationId=this.AV9OrganisationId;
         aP2_SDT_Theme=this.AV15SDT_Theme;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new prc_logtoserver(context ).execute(  AV8LocationId.ToString()) ;
         new prc_logtoserver(context ).execute(  AV9OrganisationId.ToString()) ;
         /* Using cursor P00962 */
         pr_default.execute(0, new Object[] {AV8LocationId, AV9OrganisationId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A11OrganisationId = P00962_A11OrganisationId[0];
            A29LocationId = P00962_A29LocationId[0];
            A273Trn_ThemeId = P00962_A273Trn_ThemeId[0];
            n273Trn_ThemeId = P00962_n273Trn_ThemeId[0];
            A274Trn_ThemeName = P00962_A274Trn_ThemeName[0];
            A281Trn_ThemeFontFamily = P00962_A281Trn_ThemeFontFamily[0];
            A405Trn_ThemeFontSize = P00962_A405Trn_ThemeFontSize[0];
            A274Trn_ThemeName = P00962_A274Trn_ThemeName[0];
            A281Trn_ThemeFontFamily = P00962_A281Trn_ThemeFontFamily[0];
            A405Trn_ThemeFontSize = P00962_A405Trn_ThemeFontSize[0];
            AV14ThemeId = A273Trn_ThemeId;
            AV11SDT_LocationTheme = new SdtSDT_LocationTheme(context);
            AV11SDT_LocationTheme.gxTpr_Themeid = A273Trn_ThemeId;
            AV11SDT_LocationTheme.gxTpr_Themename = A274Trn_ThemeName;
            AV11SDT_LocationTheme.gxTpr_Themefontfamily = A281Trn_ThemeFontFamily;
            AV11SDT_LocationTheme.gxTpr_Themefontsize = A405Trn_ThemeFontSize;
            /* Exiting from a For First loop. */
            if (true) break;
         }
         pr_default.close(0);
         /* Using cursor P00963 */
         pr_default.execute(1, new Object[] {AV14ThemeId});
         while ( (pr_default.getStatus(1) != 101) )
         {
            A273Trn_ThemeId = P00963_A273Trn_ThemeId[0];
            n273Trn_ThemeId = P00963_n273Trn_ThemeId[0];
            A274Trn_ThemeName = P00963_A274Trn_ThemeName[0];
            A281Trn_ThemeFontFamily = P00963_A281Trn_ThemeFontFamily[0];
            A405Trn_ThemeFontSize = P00963_A405Trn_ThemeFontSize[0];
            AV15SDT_Theme = new SdtSDT_Theme(context);
            AV15SDT_Theme.gxTpr_Themeid = A273Trn_ThemeId;
            AV15SDT_Theme.gxTpr_Themename = A274Trn_ThemeName;
            AV15SDT_Theme.gxTpr_Themefontfamily = A281Trn_ThemeFontFamily;
            AV15SDT_Theme.gxTpr_Themefontsize = A405Trn_ThemeFontSize;
            /* Using cursor P00964 */
            pr_default.execute(2, new Object[] {n273Trn_ThemeId, A273Trn_ThemeId});
            while ( (pr_default.getStatus(2) != 101) )
            {
               A282IconId = P00964_A282IconId[0];
               A443IconCategory = P00964_A443IconCategory[0];
               A283IconName = P00964_A283IconName[0];
               A284IconSVG = P00964_A284IconSVG[0];
               AV17IconsItem = new SdtSDT_Theme_IconsItem(context);
               AV17IconsItem.gxTpr_Iconid = A282IconId;
               AV17IconsItem.gxTpr_Iconcategory = A443IconCategory;
               AV17IconsItem.gxTpr_Iconname = A283IconName;
               AV17IconsItem.gxTpr_Iconsvg = A284IconSVG;
               AV15SDT_Theme.gxTpr_Icons.Add(AV17IconsItem, 0);
               pr_default.readNext(2);
            }
            pr_default.close(2);
            /* Using cursor P00965 */
            pr_default.execute(3, new Object[] {n273Trn_ThemeId, A273Trn_ThemeId});
            while ( (pr_default.getStatus(3) != 101) )
            {
               A275ColorId = P00965_A275ColorId[0];
               A276ColorName = P00965_A276ColorName[0];
               A277ColorCode = P00965_A277ColorCode[0];
               AV16ColorsItem = new SdtSDT_Theme_ColorsItem(context);
               AV16ColorsItem.gxTpr_Colorid = A275ColorId;
               AV16ColorsItem.gxTpr_Colorname = A276ColorName;
               AV16ColorsItem.gxTpr_Colorcode = A277ColorCode;
               AV15SDT_Theme.gxTpr_Colors.Add(AV16ColorsItem, 0);
               pr_default.readNext(3);
            }
            pr_default.close(3);
            /* Exiting from a For First loop. */
            if (true) break;
         }
         pr_default.close(1);
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
         AV15SDT_Theme = new SdtSDT_Theme(context);
         P00962_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00962_A29LocationId = new Guid[] {Guid.Empty} ;
         P00962_A273Trn_ThemeId = new Guid[] {Guid.Empty} ;
         P00962_n273Trn_ThemeId = new bool[] {false} ;
         P00962_A274Trn_ThemeName = new string[] {""} ;
         P00962_A281Trn_ThemeFontFamily = new string[] {""} ;
         P00962_A405Trn_ThemeFontSize = new short[1] ;
         A11OrganisationId = Guid.Empty;
         A29LocationId = Guid.Empty;
         A273Trn_ThemeId = Guid.Empty;
         A274Trn_ThemeName = "";
         A281Trn_ThemeFontFamily = "";
         AV14ThemeId = Guid.Empty;
         AV11SDT_LocationTheme = new SdtSDT_LocationTheme(context);
         P00963_A273Trn_ThemeId = new Guid[] {Guid.Empty} ;
         P00963_n273Trn_ThemeId = new bool[] {false} ;
         P00963_A274Trn_ThemeName = new string[] {""} ;
         P00963_A281Trn_ThemeFontFamily = new string[] {""} ;
         P00963_A405Trn_ThemeFontSize = new short[1] ;
         P00964_A273Trn_ThemeId = new Guid[] {Guid.Empty} ;
         P00964_n273Trn_ThemeId = new bool[] {false} ;
         P00964_A282IconId = new Guid[] {Guid.Empty} ;
         P00964_A443IconCategory = new string[] {""} ;
         P00964_A283IconName = new string[] {""} ;
         P00964_A284IconSVG = new string[] {""} ;
         A282IconId = Guid.Empty;
         A443IconCategory = "";
         A283IconName = "";
         A284IconSVG = "";
         AV17IconsItem = new SdtSDT_Theme_IconsItem(context);
         P00965_A273Trn_ThemeId = new Guid[] {Guid.Empty} ;
         P00965_n273Trn_ThemeId = new bool[] {false} ;
         P00965_A275ColorId = new Guid[] {Guid.Empty} ;
         P00965_A276ColorName = new string[] {""} ;
         P00965_A277ColorCode = new string[] {""} ;
         A275ColorId = Guid.Empty;
         A276ColorName = "";
         A277ColorCode = "";
         AV16ColorsItem = new SdtSDT_Theme_ColorsItem(context);
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_getlocationtheme__default(),
            new Object[][] {
                new Object[] {
               P00962_A11OrganisationId, P00962_A29LocationId, P00962_A273Trn_ThemeId, P00962_n273Trn_ThemeId, P00962_A274Trn_ThemeName, P00962_A281Trn_ThemeFontFamily, P00962_A405Trn_ThemeFontSize
               }
               , new Object[] {
               P00963_A273Trn_ThemeId, P00963_A274Trn_ThemeName, P00963_A281Trn_ThemeFontFamily, P00963_A405Trn_ThemeFontSize
               }
               , new Object[] {
               P00964_A273Trn_ThemeId, P00964_A282IconId, P00964_A443IconCategory, P00964_A283IconName, P00964_A284IconSVG
               }
               , new Object[] {
               P00965_A273Trn_ThemeId, P00965_A275ColorId, P00965_A276ColorName, P00965_A277ColorCode
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short A405Trn_ThemeFontSize ;
      private bool n273Trn_ThemeId ;
      private string A284IconSVG ;
      private string A274Trn_ThemeName ;
      private string A281Trn_ThemeFontFamily ;
      private string A443IconCategory ;
      private string A283IconName ;
      private string A276ColorName ;
      private string A277ColorCode ;
      private Guid AV8LocationId ;
      private Guid AV9OrganisationId ;
      private Guid A11OrganisationId ;
      private Guid A29LocationId ;
      private Guid A273Trn_ThemeId ;
      private Guid AV14ThemeId ;
      private Guid A282IconId ;
      private Guid A275ColorId ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private Guid aP0_LocationId ;
      private Guid aP1_OrganisationId ;
      private SdtSDT_Theme AV15SDT_Theme ;
      private IDataStoreProvider pr_default ;
      private Guid[] P00962_A11OrganisationId ;
      private Guid[] P00962_A29LocationId ;
      private Guid[] P00962_A273Trn_ThemeId ;
      private bool[] P00962_n273Trn_ThemeId ;
      private string[] P00962_A274Trn_ThemeName ;
      private string[] P00962_A281Trn_ThemeFontFamily ;
      private short[] P00962_A405Trn_ThemeFontSize ;
      private SdtSDT_LocationTheme AV11SDT_LocationTheme ;
      private Guid[] P00963_A273Trn_ThemeId ;
      private bool[] P00963_n273Trn_ThemeId ;
      private string[] P00963_A274Trn_ThemeName ;
      private string[] P00963_A281Trn_ThemeFontFamily ;
      private short[] P00963_A405Trn_ThemeFontSize ;
      private Guid[] P00964_A273Trn_ThemeId ;
      private bool[] P00964_n273Trn_ThemeId ;
      private Guid[] P00964_A282IconId ;
      private string[] P00964_A443IconCategory ;
      private string[] P00964_A283IconName ;
      private string[] P00964_A284IconSVG ;
      private SdtSDT_Theme_IconsItem AV17IconsItem ;
      private Guid[] P00965_A273Trn_ThemeId ;
      private bool[] P00965_n273Trn_ThemeId ;
      private Guid[] P00965_A275ColorId ;
      private string[] P00965_A276ColorName ;
      private string[] P00965_A277ColorCode ;
      private SdtSDT_Theme_ColorsItem AV16ColorsItem ;
      private SdtSDT_Theme aP2_SDT_Theme ;
   }

   public class prc_getlocationtheme__default : DataStoreHelperBase, IDataStoreHelper
   {
      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
         ,new ForEachCursor(def[1])
         ,new ForEachCursor(def[2])
         ,new ForEachCursor(def[3])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP00962;
          prmP00962 = new Object[] {
          new ParDef("AV8LocationId",GXType.UniqueIdentifier,36,0) ,
          new ParDef("AV9OrganisationId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00963;
          prmP00963 = new Object[] {
          new ParDef("AV14ThemeId",GXType.UniqueIdentifier,36,0)
          };
          Object[] prmP00964;
          prmP00964 = new Object[] {
          new ParDef("Trn_ThemeId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          Object[] prmP00965;
          prmP00965 = new Object[] {
          new ParDef("Trn_ThemeId",GXType.UniqueIdentifier,36,0){Nullable=true}
          };
          def= new CursorDef[] {
              new CursorDef("P00962", "SELECT T1.OrganisationId, T1.LocationId, T1.Trn_ThemeId, T2.Trn_ThemeName, T2.Trn_ThemeFontFamily, T2.Trn_ThemeFontSize FROM (Trn_Location T1 LEFT JOIN Trn_Theme T2 ON T2.Trn_ThemeId = T1.Trn_ThemeId) WHERE T1.LocationId = :AV8LocationId and T1.OrganisationId = :AV9OrganisationId ORDER BY T1.LocationId, T1.OrganisationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00962,1, GxCacheFrequency.OFF ,false,true )
             ,new CursorDef("P00963", "SELECT Trn_ThemeId, Trn_ThemeName, Trn_ThemeFontFamily, Trn_ThemeFontSize FROM Trn_Theme WHERE Trn_ThemeId = :AV14ThemeId ORDER BY Trn_ThemeId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00963,1, GxCacheFrequency.OFF ,true,true )
             ,new CursorDef("P00964", "SELECT Trn_ThemeId, IconId, IconCategory, IconName, IconSVG FROM Trn_ThemeIcon WHERE Trn_ThemeId = :Trn_ThemeId ORDER BY Trn_ThemeId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00964,100, GxCacheFrequency.OFF ,false,false )
             ,new CursorDef("P00965", "SELECT Trn_ThemeId, ColorId, ColorName, ColorCode FROM Trn_ThemeColor WHERE Trn_ThemeId = :Trn_ThemeId ORDER BY Trn_ThemeId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00965,100, GxCacheFrequency.OFF ,false,false )
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
                ((Guid[]) buf[2])[0] = rslt.getGuid(3);
                ((bool[]) buf[3])[0] = rslt.wasNull(3);
                ((string[]) buf[4])[0] = rslt.getVarchar(4);
                ((string[]) buf[5])[0] = rslt.getVarchar(5);
                ((short[]) buf[6])[0] = rslt.getShort(6);
                return;
             case 1 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((string[]) buf[1])[0] = rslt.getVarchar(2);
                ((string[]) buf[2])[0] = rslt.getVarchar(3);
                ((short[]) buf[3])[0] = rslt.getShort(4);
                return;
             case 2 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((Guid[]) buf[1])[0] = rslt.getGuid(2);
                ((string[]) buf[2])[0] = rslt.getVarchar(3);
                ((string[]) buf[3])[0] = rslt.getVarchar(4);
                ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
                return;
             case 3 :
                ((Guid[]) buf[0])[0] = rslt.getGuid(1);
                ((Guid[]) buf[1])[0] = rslt.getGuid(2);
                ((string[]) buf[2])[0] = rslt.getVarchar(3);
                ((string[]) buf[3])[0] = rslt.getVarchar(4);
                return;
       }
    }

 }

}
