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
   public class prc_initlocationpages : GXProcedure
   {
      public prc_initlocationpages( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_initlocationpages( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDataStore1 = context.GetDataStore("DataStore1");
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_LocationId ,
                           Guid aP1_OrganisationId )
      {
         this.AV8LocationId = aP0_LocationId;
         this.AV12OrganisationId = aP1_OrganisationId;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( Guid aP0_LocationId ,
                                 Guid aP1_OrganisationId )
      {
         this.AV8LocationId = aP0_LocationId;
         this.AV12OrganisationId = aP1_OrganisationId;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV25Folder = context.GetMessage( "Resources/UCGrapes1/PredefinedPages/", "");
         AV14BC_Trn_Location.Load(AV8LocationId, AV12OrganisationId);
         /* Execute user subroutine: 'CREATERECEPTIONPAGE' */
         S121 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'CREATECALENDARPAGE' */
         S131 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'CREATELOCATIONPAGE' */
         S141 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'CREATEMAILBOXPAGE' */
         S151 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'CREATEHOMEPAGE' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'CREATEWEBLINKPAGE' */
         S161 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'CREATEDYNAMICPAGE' */
         S171 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'CREATEHOMEPAGE' Routine */
         returnInSub = false;
         AV33GXLvl15 = 0;
         /* Using cursor P00A02 */
         pr_default.execute(0, new Object[] {AV8LocationId, AV12OrganisationId});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A397Trn_PageName = P00A02_A397Trn_PageName[0];
            A492PageIsPredefined = P00A02_A492PageIsPredefined[0];
            A11OrganisationId = P00A02_A11OrganisationId[0];
            A29LocationId = P00A02_A29LocationId[0];
            A392Trn_PageId = P00A02_A392Trn_PageId[0];
            AV33GXLvl15 = 1;
            AV28HomePageId = A392Trn_PageId;
            pr_default.readNext(0);
         }
         pr_default.close(0);
         if ( AV33GXLvl15 == 0 )
         {
            new prc_logtofile(context ).execute(  context.GetMessage( "creating home page..", "")) ;
            AV15File = new GxFile(context.GetPhysicalPath());
            AV15File.Source = AV25Folder+"DefaultHome.json";
            AV13PageGJSJson = AV15File.ReadAllText("");
            AV15File = new GxFile(context.GetPhysicalPath());
            AV15File.Source = AV25Folder+"PublishedHome.json";
            AV22PageJsonContent = AV15File.ReadAllText("");
            AV22PageJsonContent = StringUtil.StringReplace( AV22PageJsonContent, "{{LocationPageId}}", AV26LocationPageId.ToString());
            AV22PageJsonContent = StringUtil.StringReplace( AV22PageJsonContent, "{{ReceptionPageId}}", AV23ReceptionPageId.ToString());
            AV22PageJsonContent = StringUtil.StringReplace( AV22PageJsonContent, "{{CalendarPageId}}", AV24CalendarPageId.ToString());
            AV22PageJsonContent = StringUtil.StringReplace( AV22PageJsonContent, "{{MailboxPageId}}", AV27MailboxPageId.ToString());
            AV13PageGJSJson = StringUtil.StringReplace( AV13PageGJSJson, "{{LocationPageId}}", AV26LocationPageId.ToString());
            AV13PageGJSJson = StringUtil.StringReplace( AV13PageGJSJson, "{{ReceptionPageId}}", AV23ReceptionPageId.ToString());
            AV13PageGJSJson = StringUtil.StringReplace( AV13PageGJSJson, "{{CalendarPageId}}", AV24CalendarPageId.ToString());
            AV13PageGJSJson = StringUtil.StringReplace( AV13PageGJSJson, "{{MailboxPageId}}", AV27MailboxPageId.ToString());
            AV18BC_Trn_Page = new SdtTrn_Page(context);
            AV18BC_Trn_Page.gxTpr_Trn_pageid = Guid.NewGuid( );
            AV18BC_Trn_Page.gxTpr_Trn_pagename = "Home";
            AV18BC_Trn_Page.gxTpr_Pageiscontentpage = false;
            AV18BC_Trn_Page.gxTpr_Pageispredefined = true;
            AV18BC_Trn_Page.gxTpr_Locationid = AV14BC_Trn_Location.gxTpr_Locationid;
            AV18BC_Trn_Page.gxTpr_Organisationid = AV14BC_Trn_Location.gxTpr_Organisationid;
            AV18BC_Trn_Page.gxTpr_Pagegjsjson = AV13PageGJSJson;
            AV18BC_Trn_Page.gxTpr_Pagejsoncontent = AV22PageJsonContent;
            AV18BC_Trn_Page.gxTpr_Pageispublished = true;
            AV18BC_Trn_Page.Save();
            AV28HomePageId = AV18BC_Trn_Page.gxTpr_Trn_pageid;
            if ( AV18BC_Trn_Page.Success() )
            {
               context.CommitDataStores("prc_initlocationpages",pr_default);
            }
            else
            {
               AV35GXV2 = 1;
               AV34GXV1 = AV18BC_Trn_Page.GetMessages();
               while ( AV35GXV2 <= AV34GXV1.Count )
               {
                  AV19Message = ((GeneXus.Utils.SdtMessages_Message)AV34GXV1.Item(AV35GXV2));
                  new prc_logtofile(context ).execute(  ">>>"+AV19Message.gxTpr_Description) ;
                  AV35GXV2 = (int)(AV35GXV2+1);
               }
            }
         }
      }

      protected void S121( )
      {
         /* 'CREATERECEPTIONPAGE' Routine */
         returnInSub = false;
         AV36GXLvl64 = 0;
         /* Using cursor P00A03 */
         pr_default.execute(1, new Object[] {AV8LocationId, AV12OrganisationId});
         while ( (pr_default.getStatus(1) != 101) )
         {
            A397Trn_PageName = P00A03_A397Trn_PageName[0];
            A492PageIsPredefined = P00A03_A492PageIsPredefined[0];
            A11OrganisationId = P00A03_A11OrganisationId[0];
            A29LocationId = P00A03_A29LocationId[0];
            A392Trn_PageId = P00A03_A392Trn_PageId[0];
            AV36GXLvl64 = 1;
            AV23ReceptionPageId = A392Trn_PageId;
            pr_default.readNext(1);
         }
         pr_default.close(1);
         if ( AV36GXLvl64 == 0 )
         {
            new prc_logtofile(context ).execute(  context.GetMessage( "Creating reception...", "")) ;
            AV15File = new GxFile(context.GetPhysicalPath());
            AV15File.Source = AV25Folder+"DefaultReception.json";
            AV13PageGJSJson = AV15File.ReadAllText("");
            AV15File = new GxFile(context.GetPhysicalPath());
            AV15File.Source = AV25Folder+"PublishedReception.json";
            AV22PageJsonContent = AV15File.ReadAllText("");
            AV18BC_Trn_Page = new SdtTrn_Page(context);
            AV18BC_Trn_Page.gxTpr_Trn_pageid = Guid.NewGuid( );
            AV18BC_Trn_Page.gxTpr_Trn_pagename = "Reception";
            AV18BC_Trn_Page.gxTpr_Pageiscontentpage = true;
            AV18BC_Trn_Page.gxTpr_Pageispredefined = true;
            AV18BC_Trn_Page.gxTpr_Locationid = AV8LocationId;
            AV18BC_Trn_Page.gxTpr_Organisationid = AV12OrganisationId;
            AV18BC_Trn_Page.gxTpr_Pagegjsjson = AV13PageGJSJson;
            AV18BC_Trn_Page.gxTpr_Pagejsoncontent = AV22PageJsonContent;
            AV18BC_Trn_Page.Save();
            AV18BC_Trn_Page.gxTpr_Pageispublished = true;
            if ( AV18BC_Trn_Page.Success() )
            {
               context.CommitDataStores("prc_initlocationpages",pr_default);
               AV23ReceptionPageId = AV18BC_Trn_Page.gxTpr_Trn_pageid;
            }
            else
            {
               AV38GXV4 = 1;
               AV37GXV3 = AV18BC_Trn_Page.GetMessages();
               while ( AV38GXV4 <= AV37GXV3.Count )
               {
                  AV19Message = ((GeneXus.Utils.SdtMessages_Message)AV37GXV3.Item(AV38GXV4));
                  new prc_logtofile(context ).execute(  AV19Message.gxTpr_Description) ;
                  AV38GXV4 = (int)(AV38GXV4+1);
               }
            }
         }
      }

      protected void S131( )
      {
         /* 'CREATECALENDARPAGE' Routine */
         returnInSub = false;
         AV39GXLvl103 = 0;
         /* Using cursor P00A04 */
         pr_default.execute(2, new Object[] {AV8LocationId, AV12OrganisationId});
         while ( (pr_default.getStatus(2) != 101) )
         {
            A397Trn_PageName = P00A04_A397Trn_PageName[0];
            A492PageIsPredefined = P00A04_A492PageIsPredefined[0];
            A11OrganisationId = P00A04_A11OrganisationId[0];
            A29LocationId = P00A04_A29LocationId[0];
            A392Trn_PageId = P00A04_A392Trn_PageId[0];
            AV39GXLvl103 = 1;
            AV24CalendarPageId = A392Trn_PageId;
            pr_default.readNext(2);
         }
         pr_default.close(2);
         if ( AV39GXLvl103 == 0 )
         {
            AV15File = new GxFile(context.GetPhysicalPath());
            AV15File.Source = AV25Folder+"DefaultCalendar.json";
            AV13PageGJSJson = AV15File.ReadAllText("");
            AV15File = new GxFile(context.GetPhysicalPath());
            AV15File.Source = AV25Folder+"PublishedCalendar.json";
            AV22PageJsonContent = AV15File.ReadAllText("");
            AV18BC_Trn_Page = new SdtTrn_Page(context);
            AV18BC_Trn_Page.gxTpr_Trn_pageid = Guid.NewGuid( );
            AV18BC_Trn_Page.gxTpr_Trn_pagename = "Calendar";
            AV18BC_Trn_Page.gxTpr_Pageiscontentpage = true;
            AV18BC_Trn_Page.gxTpr_Pageispredefined = true;
            AV18BC_Trn_Page.gxTpr_Locationid = AV14BC_Trn_Location.gxTpr_Locationid;
            AV18BC_Trn_Page.gxTpr_Organisationid = AV14BC_Trn_Location.gxTpr_Organisationid;
            AV18BC_Trn_Page.gxTpr_Pagegjsjson = AV13PageGJSJson;
            AV18BC_Trn_Page.gxTpr_Pagejsoncontent = AV22PageJsonContent;
            AV18BC_Trn_Page.gxTpr_Pageispublished = true;
            AV18BC_Trn_Page.Save();
            if ( AV18BC_Trn_Page.Success() )
            {
               context.CommitDataStores("prc_initlocationpages",pr_default);
               AV24CalendarPageId = AV18BC_Trn_Page.gxTpr_Trn_pageid;
            }
            else
            {
               AV41GXV6 = 1;
               AV40GXV5 = AV18BC_Trn_Page.GetMessages();
               while ( AV41GXV6 <= AV40GXV5.Count )
               {
                  AV19Message = ((GeneXus.Utils.SdtMessages_Message)AV40GXV5.Item(AV41GXV6));
                  new prc_logtofile(context ).execute(  AV19Message.gxTpr_Description) ;
                  AV41GXV6 = (int)(AV41GXV6+1);
               }
            }
         }
      }

      protected void S141( )
      {
         /* 'CREATELOCATIONPAGE' Routine */
         returnInSub = false;
         AV42GXLvl141 = 0;
         /* Using cursor P00A05 */
         pr_default.execute(3, new Object[] {AV8LocationId, AV12OrganisationId});
         while ( (pr_default.getStatus(3) != 101) )
         {
            A397Trn_PageName = P00A05_A397Trn_PageName[0];
            A492PageIsPredefined = P00A05_A492PageIsPredefined[0];
            A11OrganisationId = P00A05_A11OrganisationId[0];
            A29LocationId = P00A05_A29LocationId[0];
            A392Trn_PageId = P00A05_A392Trn_PageId[0];
            AV42GXLvl141 = 1;
            AV26LocationPageId = A392Trn_PageId;
            pr_default.readNext(3);
         }
         pr_default.close(3);
         if ( AV42GXLvl141 == 0 )
         {
            new prc_logtofile(context ).execute(  context.GetMessage( "Creating location...", "")) ;
            AV15File = new GxFile(context.GetPhysicalPath());
            AV15File.Source = AV25Folder+"DefaultLocation.json";
            AV13PageGJSJson = AV15File.ReadAllText("");
            AV15File = new GxFile(context.GetPhysicalPath());
            AV15File.Source = AV25Folder+"PublishedLocation.json";
            AV22PageJsonContent = AV15File.ReadAllText("");
            AV22PageJsonContent = StringUtil.StringReplace( AV22PageJsonContent, "{{LocationImage}}", AV14BC_Trn_Location.gxTpr_Locationimage_gxi);
            AV22PageJsonContent = StringUtil.StringReplace( AV22PageJsonContent, "{{LocationDescription}}", AV14BC_Trn_Location.gxTpr_Locationdescription);
            AV22PageJsonContent = StringUtil.StringReplace( AV22PageJsonContent, "{{LocationTelephone}}", AV14BC_Trn_Location.gxTpr_Locationphone);
            AV22PageJsonContent = StringUtil.StringReplace( AV22PageJsonContent, "{{LocationEmail}}", AV14BC_Trn_Location.gxTpr_Locationemail);
            AV18BC_Trn_Page = new SdtTrn_Page(context);
            AV18BC_Trn_Page.gxTpr_Trn_pageid = Guid.NewGuid( );
            AV18BC_Trn_Page.gxTpr_Trn_pagename = "Location";
            AV18BC_Trn_Page.gxTpr_Pageiscontentpage = true;
            AV18BC_Trn_Page.gxTpr_Pageispredefined = true;
            AV18BC_Trn_Page.gxTpr_Locationid = AV14BC_Trn_Location.gxTpr_Locationid;
            AV18BC_Trn_Page.gxTpr_Organisationid = AV14BC_Trn_Location.gxTpr_Organisationid;
            AV18BC_Trn_Page.gxTpr_Pagegjsjson = AV13PageGJSJson;
            AV18BC_Trn_Page.gxTpr_Pagejsoncontent = AV22PageJsonContent;
            AV18BC_Trn_Page.gxTpr_Pageispublished = true;
            AV18BC_Trn_Page.Save();
            if ( AV18BC_Trn_Page.Success() )
            {
               context.CommitDataStores("prc_initlocationpages",pr_default);
               AV26LocationPageId = AV18BC_Trn_Page.gxTpr_Trn_pageid;
               new prc_logtofile(context ).execute(  context.GetMessage( "created location... ", "")+AV26LocationPageId.ToString()) ;
            }
            else
            {
               AV44GXV8 = 1;
               AV43GXV7 = AV18BC_Trn_Page.GetMessages();
               while ( AV44GXV8 <= AV43GXV7.Count )
               {
                  AV19Message = ((GeneXus.Utils.SdtMessages_Message)AV43GXV7.Item(AV44GXV8));
                  new prc_logtofile(context ).execute(  AV19Message.gxTpr_Description) ;
                  AV44GXV8 = (int)(AV44GXV8+1);
               }
            }
         }
      }

      protected void S151( )
      {
         /* 'CREATEMAILBOXPAGE' Routine */
         returnInSub = false;
         AV45GXLvl185 = 0;
         /* Using cursor P00A06 */
         pr_default.execute(4, new Object[] {AV8LocationId, AV12OrganisationId});
         while ( (pr_default.getStatus(4) != 101) )
         {
            A397Trn_PageName = P00A06_A397Trn_PageName[0];
            A492PageIsPredefined = P00A06_A492PageIsPredefined[0];
            A11OrganisationId = P00A06_A11OrganisationId[0];
            A29LocationId = P00A06_A29LocationId[0];
            A392Trn_PageId = P00A06_A392Trn_PageId[0];
            AV45GXLvl185 = 1;
            AV27MailboxPageId = A392Trn_PageId;
            pr_default.readNext(4);
         }
         pr_default.close(4);
         if ( AV45GXLvl185 == 0 )
         {
            new prc_logtofile(context ).execute(  context.GetMessage( "Creating mailbox...", "")) ;
            AV15File = new GxFile(context.GetPhysicalPath());
            AV15File.Source = AV25Folder+"DefaultMailbox.json";
            AV13PageGJSJson = AV15File.ReadAllText("");
            AV15File = new GxFile(context.GetPhysicalPath());
            AV15File.Source = AV25Folder+"PublishedMailbox.json";
            AV22PageJsonContent = AV15File.ReadAllText("");
            AV18BC_Trn_Page = new SdtTrn_Page(context);
            AV18BC_Trn_Page.gxTpr_Trn_pageid = Guid.NewGuid( );
            AV18BC_Trn_Page.gxTpr_Trn_pagename = "My Activity";
            AV18BC_Trn_Page.gxTpr_Pageiscontentpage = true;
            AV18BC_Trn_Page.gxTpr_Pageispredefined = true;
            AV18BC_Trn_Page.gxTpr_Locationid = AV14BC_Trn_Location.gxTpr_Locationid;
            AV18BC_Trn_Page.gxTpr_Organisationid = AV14BC_Trn_Location.gxTpr_Organisationid;
            AV18BC_Trn_Page.gxTpr_Pagegjsjson = AV13PageGJSJson;
            AV18BC_Trn_Page.gxTpr_Pagejsoncontent = AV22PageJsonContent;
            AV18BC_Trn_Page.gxTpr_Pageispublished = true;
            AV18BC_Trn_Page.Save();
            if ( AV18BC_Trn_Page.Success() )
            {
               context.CommitDataStores("prc_initlocationpages",pr_default);
               new prc_logtofile(context ).execute(  context.GetMessage( "created mailbox... ", "")+AV27MailboxPageId.ToString()) ;
               AV27MailboxPageId = AV18BC_Trn_Page.gxTpr_Trn_pageid;
            }
            else
            {
               AV47GXV10 = 1;
               AV46GXV9 = AV18BC_Trn_Page.GetMessages();
               while ( AV47GXV10 <= AV46GXV9.Count )
               {
                  AV19Message = ((GeneXus.Utils.SdtMessages_Message)AV46GXV9.Item(AV47GXV10));
                  new prc_logtofile(context ).execute(  AV19Message.gxTpr_Description) ;
                  AV47GXV10 = (int)(AV47GXV10+1);
               }
            }
         }
      }

      protected void S161( )
      {
         /* 'CREATEWEBLINKPAGE' Routine */
         returnInSub = false;
         AV48GXLvl225 = 0;
         /* Using cursor P00A07 */
         pr_default.execute(5, new Object[] {AV8LocationId, AV12OrganisationId});
         while ( (pr_default.getStatus(5) != 101) )
         {
            A397Trn_PageName = P00A07_A397Trn_PageName[0];
            A492PageIsPredefined = P00A07_A492PageIsPredefined[0];
            A505PageIsWebLinkPage = P00A07_A505PageIsWebLinkPage[0];
            A11OrganisationId = P00A07_A11OrganisationId[0];
            A29LocationId = P00A07_A29LocationId[0];
            A392Trn_PageId = P00A07_A392Trn_PageId[0];
            AV48GXLvl225 = 1;
            AV31WebLinkPageId = A392Trn_PageId;
            pr_default.readNext(5);
         }
         pr_default.close(5);
         if ( AV48GXLvl225 == 0 )
         {
            AV18BC_Trn_Page = new SdtTrn_Page(context);
            AV18BC_Trn_Page.gxTpr_Trn_pageid = Guid.NewGuid( );
            AV18BC_Trn_Page.gxTpr_Trn_pagename = "Web Link";
            AV18BC_Trn_Page.gxTpr_Pageisweblinkpage = true;
            AV18BC_Trn_Page.gxTpr_Pageispredefined = true;
            AV18BC_Trn_Page.gxTpr_Locationid = AV14BC_Trn_Location.gxTpr_Locationid;
            AV18BC_Trn_Page.gxTpr_Organisationid = AV14BC_Trn_Location.gxTpr_Organisationid;
            AV18BC_Trn_Page.gxTpr_Pagegjsjson = "";
            AV18BC_Trn_Page.gxTpr_Pagejsoncontent = "";
            AV18BC_Trn_Page.gxTpr_Pageispublished = true;
            AV18BC_Trn_Page.Save();
            if ( AV18BC_Trn_Page.Success() )
            {
               context.CommitDataStores("prc_initlocationpages",pr_default);
               AV31WebLinkPageId = AV18BC_Trn_Page.gxTpr_Trn_pageid;
            }
            else
            {
               AV50GXV12 = 1;
               AV49GXV11 = AV18BC_Trn_Page.GetMessages();
               while ( AV50GXV12 <= AV49GXV11.Count )
               {
                  AV19Message = ((GeneXus.Utils.SdtMessages_Message)AV49GXV11.Item(AV50GXV12));
                  new prc_logtofile(context ).execute(  AV19Message.gxTpr_Description) ;
                  AV50GXV12 = (int)(AV50GXV12+1);
               }
            }
         }
      }

      protected void S171( )
      {
         /* 'CREATEDYNAMICPAGE' Routine */
         returnInSub = false;
         AV51GXLvl256 = 0;
         /* Using cursor P00A08 */
         pr_default.execute(6, new Object[] {AV8LocationId, AV12OrganisationId});
         while ( (pr_default.getStatus(6) != 101) )
         {
            A397Trn_PageName = P00A08_A397Trn_PageName[0];
            A492PageIsPredefined = P00A08_A492PageIsPredefined[0];
            A502PageIsDynamicForm = P00A08_A502PageIsDynamicForm[0];
            A11OrganisationId = P00A08_A11OrganisationId[0];
            A29LocationId = P00A08_A29LocationId[0];
            A392Trn_PageId = P00A08_A392Trn_PageId[0];
            AV51GXLvl256 = 1;
            pr_default.readNext(6);
         }
         pr_default.close(6);
         if ( AV51GXLvl256 == 0 )
         {
            AV18BC_Trn_Page = new SdtTrn_Page(context);
            AV18BC_Trn_Page.gxTpr_Trn_pageid = Guid.NewGuid( );
            AV18BC_Trn_Page.gxTpr_Trn_pagename = "Dynamic Forms";
            AV18BC_Trn_Page.gxTpr_Pageisdynamicform = true;
            AV18BC_Trn_Page.gxTpr_Pageispredefined = true;
            AV18BC_Trn_Page.gxTpr_Locationid = AV14BC_Trn_Location.gxTpr_Locationid;
            AV18BC_Trn_Page.gxTpr_Organisationid = AV14BC_Trn_Location.gxTpr_Organisationid;
            AV18BC_Trn_Page.gxTpr_Pagegjsjson = "";
            AV18BC_Trn_Page.gxTpr_Pagejsoncontent = "";
            AV18BC_Trn_Page.gxTpr_Pageispublished = true;
            AV18BC_Trn_Page.Save();
            if ( AV18BC_Trn_Page.Success() )
            {
               context.CommitDataStores("prc_initlocationpages",pr_default);
               AV31WebLinkPageId = AV18BC_Trn_Page.gxTpr_Trn_pageid;
            }
            else
            {
               AV53GXV14 = 1;
               AV52GXV13 = AV18BC_Trn_Page.GetMessages();
               while ( AV53GXV14 <= AV52GXV13.Count )
               {
                  AV19Message = ((GeneXus.Utils.SdtMessages_Message)AV52GXV13.Item(AV53GXV14));
                  new prc_logtofile(context ).execute(  AV19Message.gxTpr_Description) ;
                  AV53GXV14 = (int)(AV53GXV14+1);
               }
            }
         }
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
         AV25Folder = "";
         AV14BC_Trn_Location = new SdtTrn_Location(context);
         P00A02_A397Trn_PageName = new string[] {""} ;
         P00A02_A492PageIsPredefined = new bool[] {false} ;
         P00A02_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00A02_A29LocationId = new Guid[] {Guid.Empty} ;
         P00A02_A392Trn_PageId = new Guid[] {Guid.Empty} ;
         A397Trn_PageName = "";
         A11OrganisationId = Guid.Empty;
         A29LocationId = Guid.Empty;
         A392Trn_PageId = Guid.Empty;
         AV28HomePageId = Guid.Empty;
         AV15File = new GxFile(context.GetPhysicalPath());
         AV13PageGJSJson = "";
         AV22PageJsonContent = "";
         AV26LocationPageId = Guid.Empty;
         AV23ReceptionPageId = Guid.Empty;
         AV24CalendarPageId = Guid.Empty;
         AV27MailboxPageId = Guid.Empty;
         AV18BC_Trn_Page = new SdtTrn_Page(context);
         AV34GXV1 = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         AV19Message = new GeneXus.Utils.SdtMessages_Message(context);
         P00A03_A397Trn_PageName = new string[] {""} ;
         P00A03_A492PageIsPredefined = new bool[] {false} ;
         P00A03_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00A03_A29LocationId = new Guid[] {Guid.Empty} ;
         P00A03_A392Trn_PageId = new Guid[] {Guid.Empty} ;
         AV37GXV3 = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         P00A04_A397Trn_PageName = new string[] {""} ;
         P00A04_A492PageIsPredefined = new bool[] {false} ;
         P00A04_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00A04_A29LocationId = new Guid[] {Guid.Empty} ;
         P00A04_A392Trn_PageId = new Guid[] {Guid.Empty} ;
         AV40GXV5 = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         P00A05_A397Trn_PageName = new string[] {""} ;
         P00A05_A492PageIsPredefined = new bool[] {false} ;
         P00A05_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00A05_A29LocationId = new Guid[] {Guid.Empty} ;
         P00A05_A392Trn_PageId = new Guid[] {Guid.Empty} ;
         AV43GXV7 = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         P00A06_A397Trn_PageName = new string[] {""} ;
         P00A06_A492PageIsPredefined = new bool[] {false} ;
         P00A06_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00A06_A29LocationId = new Guid[] {Guid.Empty} ;
         P00A06_A392Trn_PageId = new Guid[] {Guid.Empty} ;
         AV46GXV9 = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         P00A07_A397Trn_PageName = new string[] {""} ;
         P00A07_A492PageIsPredefined = new bool[] {false} ;
         P00A07_A505PageIsWebLinkPage = new bool[] {false} ;
         P00A07_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00A07_A29LocationId = new Guid[] {Guid.Empty} ;
         P00A07_A392Trn_PageId = new Guid[] {Guid.Empty} ;
         AV31WebLinkPageId = Guid.Empty;
         AV49GXV11 = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         P00A08_A397Trn_PageName = new string[] {""} ;
         P00A08_A492PageIsPredefined = new bool[] {false} ;
         P00A08_A502PageIsDynamicForm = new bool[] {false} ;
         P00A08_A11OrganisationId = new Guid[] {Guid.Empty} ;
         P00A08_A29LocationId = new Guid[] {Guid.Empty} ;
         P00A08_A392Trn_PageId = new Guid[] {Guid.Empty} ;
         AV52GXV13 = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         pr_datastore1 = new DataStoreProvider(context, new GeneXus.Programs.prc_initlocationpages__datastore1(),
            new Object[][] {
            }
         );
         pr_gam = new DataStoreProvider(context, new GeneXus.Programs.prc_initlocationpages__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new GeneXus.Programs.prc_initlocationpages__default(),
            new Object[][] {
                new Object[] {
               P00A02_A397Trn_PageName, P00A02_A492PageIsPredefined, P00A02_A11OrganisationId, P00A02_A29LocationId, P00A02_A392Trn_PageId
               }
               , new Object[] {
               P00A03_A397Trn_PageName, P00A03_A492PageIsPredefined, P00A03_A11OrganisationId, P00A03_A29LocationId, P00A03_A392Trn_PageId
               }
               , new Object[] {
               P00A04_A397Trn_PageName, P00A04_A492PageIsPredefined, P00A04_A11OrganisationId, P00A04_A29LocationId, P00A04_A392Trn_PageId
               }
               , new Object[] {
               P00A05_A397Trn_PageName, P00A05_A492PageIsPredefined, P00A05_A11OrganisationId, P00A05_A29LocationId, P00A05_A392Trn_PageId
               }
               , new Object[] {
               P00A06_A397Trn_PageName, P00A06_A492PageIsPredefined, P00A06_A11OrganisationId, P00A06_A29LocationId, P00A06_A392Trn_PageId
               }
               , new Object[] {
               P00A07_A397Trn_PageName, P00A07_A492PageIsPredefined, P00A07_A505PageIsWebLinkPage, P00A07_A11OrganisationId, P00A07_A29LocationId, P00A07_A392Trn_PageId
               }
               , new Object[] {
               P00A08_A397Trn_PageName, P00A08_A492PageIsPredefined, P00A08_A502PageIsDynamicForm, P00A08_A11OrganisationId, P00A08_A29LocationId, P00A08_A392Trn_PageId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV33GXLvl15 ;
      private short AV36GXLvl64 ;
      private short AV39GXLvl103 ;
      private short AV42GXLvl141 ;
      private short AV45GXLvl185 ;
      private short AV48GXLvl225 ;
      private short AV51GXLvl256 ;
      private int AV35GXV2 ;
      private int AV38GXV4 ;
      private int AV41GXV6 ;
      private int AV44GXV8 ;
      private int AV47GXV10 ;
      private int AV50GXV12 ;
      private int AV53GXV14 ;
      private bool returnInSub ;
      private bool A492PageIsPredefined ;
      private bool A505PageIsWebLinkPage ;
      private bool A502PageIsDynamicForm ;
      private string AV13PageGJSJson ;
      private string AV22PageJsonContent ;
      private string AV25Folder ;
      private string A397Trn_PageName ;
      private Guid AV8LocationId ;
      private Guid AV12OrganisationId ;
      private Guid A11OrganisationId ;
      private Guid A29LocationId ;
      private Guid A392Trn_PageId ;
      private Guid AV28HomePageId ;
      private Guid AV26LocationPageId ;
      private Guid AV23ReceptionPageId ;
      private Guid AV24CalendarPageId ;
      private Guid AV27MailboxPageId ;
      private Guid AV31WebLinkPageId ;
      private GxFile AV15File ;
      private IGxDataStore dsDataStore1 ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private SdtTrn_Location AV14BC_Trn_Location ;
      private IDataStoreProvider pr_default ;
      private string[] P00A02_A397Trn_PageName ;
      private bool[] P00A02_A492PageIsPredefined ;
      private Guid[] P00A02_A11OrganisationId ;
      private Guid[] P00A02_A29LocationId ;
      private Guid[] P00A02_A392Trn_PageId ;
      private SdtTrn_Page AV18BC_Trn_Page ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV34GXV1 ;
      private GeneXus.Utils.SdtMessages_Message AV19Message ;
      private string[] P00A03_A397Trn_PageName ;
      private bool[] P00A03_A492PageIsPredefined ;
      private Guid[] P00A03_A11OrganisationId ;
      private Guid[] P00A03_A29LocationId ;
      private Guid[] P00A03_A392Trn_PageId ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV37GXV3 ;
      private string[] P00A04_A397Trn_PageName ;
      private bool[] P00A04_A492PageIsPredefined ;
      private Guid[] P00A04_A11OrganisationId ;
      private Guid[] P00A04_A29LocationId ;
      private Guid[] P00A04_A392Trn_PageId ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV40GXV5 ;
      private string[] P00A05_A397Trn_PageName ;
      private bool[] P00A05_A492PageIsPredefined ;
      private Guid[] P00A05_A11OrganisationId ;
      private Guid[] P00A05_A29LocationId ;
      private Guid[] P00A05_A392Trn_PageId ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV43GXV7 ;
      private string[] P00A06_A397Trn_PageName ;
      private bool[] P00A06_A492PageIsPredefined ;
      private Guid[] P00A06_A11OrganisationId ;
      private Guid[] P00A06_A29LocationId ;
      private Guid[] P00A06_A392Trn_PageId ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV46GXV9 ;
      private string[] P00A07_A397Trn_PageName ;
      private bool[] P00A07_A492PageIsPredefined ;
      private bool[] P00A07_A505PageIsWebLinkPage ;
      private Guid[] P00A07_A11OrganisationId ;
      private Guid[] P00A07_A29LocationId ;
      private Guid[] P00A07_A392Trn_PageId ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV49GXV11 ;
      private string[] P00A08_A397Trn_PageName ;
      private bool[] P00A08_A492PageIsPredefined ;
      private bool[] P00A08_A502PageIsDynamicForm ;
      private Guid[] P00A08_A11OrganisationId ;
      private Guid[] P00A08_A29LocationId ;
      private Guid[] P00A08_A392Trn_PageId ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV52GXV13 ;
      private IDataStoreProvider pr_datastore1 ;
      private IDataStoreProvider pr_gam ;
   }

   public class prc_initlocationpages__datastore1 : DataStoreHelperBase, IDataStoreHelper
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

 public class prc_initlocationpages__gam : DataStoreHelperBase, IDataStoreHelper
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

public class prc_initlocationpages__default : DataStoreHelperBase, IDataStoreHelper
{
   public ICursor[] getCursors( )
   {
      cursorDefinitions();
      return new Cursor[] {
       new ForEachCursor(def[0])
      ,new ForEachCursor(def[1])
      ,new ForEachCursor(def[2])
      ,new ForEachCursor(def[3])
      ,new ForEachCursor(def[4])
      ,new ForEachCursor(def[5])
      ,new ForEachCursor(def[6])
    };
 }

 private static CursorDef[] def;
 private void cursorDefinitions( )
 {
    if ( def == null )
    {
       Object[] prmP00A02;
       prmP00A02 = new Object[] {
       new ParDef("AV8LocationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("AV12OrganisationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmP00A03;
       prmP00A03 = new Object[] {
       new ParDef("AV8LocationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("AV12OrganisationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmP00A04;
       prmP00A04 = new Object[] {
       new ParDef("AV8LocationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("AV12OrganisationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmP00A05;
       prmP00A05 = new Object[] {
       new ParDef("AV8LocationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("AV12OrganisationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmP00A06;
       prmP00A06 = new Object[] {
       new ParDef("AV8LocationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("AV12OrganisationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmP00A07;
       prmP00A07 = new Object[] {
       new ParDef("AV8LocationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("AV12OrganisationId",GXType.UniqueIdentifier,36,0)
       };
       Object[] prmP00A08;
       prmP00A08 = new Object[] {
       new ParDef("AV8LocationId",GXType.UniqueIdentifier,36,0) ,
       new ParDef("AV12OrganisationId",GXType.UniqueIdentifier,36,0)
       };
       def= new CursorDef[] {
           new CursorDef("P00A02", "SELECT Trn_PageName, PageIsPredefined, OrganisationId, LocationId, Trn_PageId FROM Trn_Page WHERE (LocationId = :AV8LocationId) AND (OrganisationId = :AV12OrganisationId) AND (PageIsPredefined = TRUE) AND (Trn_PageName = ( 'Home')) ORDER BY Trn_PageId, LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00A02,100, GxCacheFrequency.OFF ,false,false )
          ,new CursorDef("P00A03", "SELECT Trn_PageName, PageIsPredefined, OrganisationId, LocationId, Trn_PageId FROM Trn_Page WHERE (LocationId = :AV8LocationId) AND (OrganisationId = :AV12OrganisationId) AND (PageIsPredefined = TRUE) AND (Trn_PageName = ( 'Reception')) ORDER BY Trn_PageId, LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00A03,100, GxCacheFrequency.OFF ,false,false )
          ,new CursorDef("P00A04", "SELECT Trn_PageName, PageIsPredefined, OrganisationId, LocationId, Trn_PageId FROM Trn_Page WHERE (LocationId = :AV8LocationId) AND (OrganisationId = :AV12OrganisationId) AND (PageIsPredefined = TRUE) AND (Trn_PageName = ( 'Calendar')) ORDER BY Trn_PageId, LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00A04,100, GxCacheFrequency.OFF ,false,false )
          ,new CursorDef("P00A05", "SELECT Trn_PageName, PageIsPredefined, OrganisationId, LocationId, Trn_PageId FROM Trn_Page WHERE (LocationId = :AV8LocationId) AND (OrganisationId = :AV12OrganisationId) AND (PageIsPredefined = TRUE) AND (Trn_PageName = ( 'Location')) ORDER BY Trn_PageId, LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00A05,100, GxCacheFrequency.OFF ,false,false )
          ,new CursorDef("P00A06", "SELECT Trn_PageName, PageIsPredefined, OrganisationId, LocationId, Trn_PageId FROM Trn_Page WHERE (LocationId = :AV8LocationId) AND (OrganisationId = :AV12OrganisationId) AND (PageIsPredefined = TRUE) AND (Trn_PageName = ( 'My Activity')) ORDER BY Trn_PageId, LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00A06,100, GxCacheFrequency.OFF ,false,false )
          ,new CursorDef("P00A07", "SELECT Trn_PageName, PageIsPredefined, PageIsWebLinkPage, OrganisationId, LocationId, Trn_PageId FROM Trn_Page WHERE (LocationId = :AV8LocationId) AND (OrganisationId = :AV12OrganisationId) AND (PageIsWebLinkPage = TRUE) AND (PageIsPredefined = TRUE) AND (Trn_PageName = ( 'Web Link')) ORDER BY Trn_PageId, LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00A07,100, GxCacheFrequency.OFF ,false,false )
          ,new CursorDef("P00A08", "SELECT Trn_PageName, PageIsPredefined, PageIsDynamicForm, OrganisationId, LocationId, Trn_PageId FROM Trn_Page WHERE (LocationId = :AV8LocationId) AND (OrganisationId = :AV12OrganisationId) AND (PageIsDynamicForm = TRUE) AND (PageIsPredefined = TRUE) AND (Trn_PageName = ( 'Dynamic Forms')) ORDER BY Trn_PageId, LocationId ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00A08,100, GxCacheFrequency.OFF ,false,false )
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
             ((bool[]) buf[1])[0] = rslt.getBool(2);
             ((Guid[]) buf[2])[0] = rslt.getGuid(3);
             ((Guid[]) buf[3])[0] = rslt.getGuid(4);
             ((Guid[]) buf[4])[0] = rslt.getGuid(5);
             return;
          case 1 :
             ((string[]) buf[0])[0] = rslt.getVarchar(1);
             ((bool[]) buf[1])[0] = rslt.getBool(2);
             ((Guid[]) buf[2])[0] = rslt.getGuid(3);
             ((Guid[]) buf[3])[0] = rslt.getGuid(4);
             ((Guid[]) buf[4])[0] = rslt.getGuid(5);
             return;
          case 2 :
             ((string[]) buf[0])[0] = rslt.getVarchar(1);
             ((bool[]) buf[1])[0] = rslt.getBool(2);
             ((Guid[]) buf[2])[0] = rslt.getGuid(3);
             ((Guid[]) buf[3])[0] = rslt.getGuid(4);
             ((Guid[]) buf[4])[0] = rslt.getGuid(5);
             return;
          case 3 :
             ((string[]) buf[0])[0] = rslt.getVarchar(1);
             ((bool[]) buf[1])[0] = rslt.getBool(2);
             ((Guid[]) buf[2])[0] = rslt.getGuid(3);
             ((Guid[]) buf[3])[0] = rslt.getGuid(4);
             ((Guid[]) buf[4])[0] = rslt.getGuid(5);
             return;
          case 4 :
             ((string[]) buf[0])[0] = rslt.getVarchar(1);
             ((bool[]) buf[1])[0] = rslt.getBool(2);
             ((Guid[]) buf[2])[0] = rslt.getGuid(3);
             ((Guid[]) buf[3])[0] = rslt.getGuid(4);
             ((Guid[]) buf[4])[0] = rslt.getGuid(5);
             return;
          case 5 :
             ((string[]) buf[0])[0] = rslt.getVarchar(1);
             ((bool[]) buf[1])[0] = rslt.getBool(2);
             ((bool[]) buf[2])[0] = rslt.getBool(3);
             ((Guid[]) buf[3])[0] = rslt.getGuid(4);
             ((Guid[]) buf[4])[0] = rslt.getGuid(5);
             ((Guid[]) buf[5])[0] = rslt.getGuid(6);
             return;
          case 6 :
             ((string[]) buf[0])[0] = rslt.getVarchar(1);
             ((bool[]) buf[1])[0] = rslt.getBool(2);
             ((bool[]) buf[2])[0] = rslt.getBool(3);
             ((Guid[]) buf[3])[0] = rslt.getGuid(4);
             ((Guid[]) buf[4])[0] = rslt.getGuid(5);
             ((Guid[]) buf[5])[0] = rslt.getGuid(6);
             return;
    }
 }

}

}
