using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
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
   public class prc_debugappversion : GXProcedure
   {
      public prc_debugappversion( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public prc_debugappversion( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GXBaseCollection<SdtSDT_PageUrl> aP0_PageUrlList ,
                           out SdtSDT_DebugResults aP1_DebugResults ,
                           out SdtSDT_Error aP2_Error )
      {
         this.AV24PageUrlList = aP0_PageUrlList;
         this.AV9DebugResults = new SdtSDT_DebugResults(context) ;
         this.AV10Error = new SdtSDT_Error(context) ;
         initialize();
         ExecuteImpl();
         aP1_DebugResults=this.AV9DebugResults;
         aP2_Error=this.AV10Error;
      }

      public SdtSDT_Error executeUdp( GXBaseCollection<SdtSDT_PageUrl> aP0_PageUrlList ,
                                      out SdtSDT_DebugResults aP1_DebugResults )
      {
         execute(aP0_PageUrlList, out aP1_DebugResults, out aP2_Error);
         return AV10Error ;
      }

      public void executeSubmit( GXBaseCollection<SdtSDT_PageUrl> aP0_PageUrlList ,
                                 out SdtSDT_DebugResults aP1_DebugResults ,
                                 out SdtSDT_Error aP2_Error )
      {
         this.AV24PageUrlList = aP0_PageUrlList;
         this.AV9DebugResults = new SdtSDT_DebugResults(context) ;
         this.AV10Error = new SdtSDT_Error(context) ;
         SubmitImpl();
         aP1_DebugResults=this.AV9DebugResults;
         aP2_Error=this.AV10Error;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new prc_logtofile(context ).execute(  "&PageUrlList : "+AV24PageUrlList.ToJSonString(false)) ;
         AV34GXV1 = 1;
         while ( AV34GXV1 <= AV24PageUrlList.Count )
         {
            AV22pageUrl = ((SdtSDT_PageUrl)AV24PageUrlList.Item(AV34GXV1));
            AV25PageItem = new SdtSDT_DebugResults_PagesItem(context);
            AV25PageItem.gxTpr_Page = AV22pageUrl.gxTpr_Page;
            AV32UrlCheckItems.Clear();
            AV35GXV2 = 1;
            while ( AV35GXV2 <= AV22pageUrl.gxTpr_Urls.Count )
            {
               AV27pageUrlItem = ((SdtSDT_PageUrl_UrlsItem)AV22pageUrl.gxTpr_Urls.Item(AV35GXV2));
               AV28urlCheckItem = new SdtUrlCheckItem(context);
               AV28urlCheckItem.gxTpr_Url = AV27pageUrlItem.gxTpr_Url;
               AV28urlCheckItem.gxTpr_Affectedtype = AV27pageUrlItem.gxTpr_Affectedtype;
               AV28urlCheckItem.gxTpr_Affectedname = AV27pageUrlItem.gxTpr_Affectedname;
               AV32UrlCheckItems.Add(AV28urlCheckItem, 0);
               AV35GXV2 = (int)(AV35GXV2+1);
            }
            AV33UrlStatuses = AV29UrlChecker.checkurls(AV32UrlCheckItems);
            AV30Summary = AV29UrlChecker.getsummary();
            AV9DebugResults.gxTpr_Summary.gxTpr_Totalurls = (decimal)(AV9DebugResults.gxTpr_Summary.gxTpr_Totalurls+(AV30Summary.gxTpr_Totalurls));
            AV9DebugResults.gxTpr_Summary.gxTpr_Successcount = (decimal)(AV9DebugResults.gxTpr_Summary.gxTpr_Successcount+(AV30Summary.gxTpr_Totalsuccess));
            AV9DebugResults.gxTpr_Summary.gxTpr_Failurecount = (decimal)(AV9DebugResults.gxTpr_Summary.gxTpr_Failurecount+(AV30Summary.gxTpr_Totalfailed));
            AV36GXV3 = 1;
            while ( AV36GXV3 <= AV33UrlStatuses.Count )
            {
               AV31UrlStatus = ((SdtUrlStatus)AV33UrlStatuses.Item(AV36GXV3));
               AV17UrlListItem = new SdtSDT_DebugResults_PagesItem_UrlListItem(context);
               AV17UrlListItem.gxTpr_Url = AV31UrlStatus.gxTpr_Url;
               AV17UrlListItem.gxTpr_Statuscode = StringUtil.Trim( StringUtil.Str( (decimal)(AV31UrlStatus.gxTpr_Statuscode), 9, 0));
               AV17UrlListItem.gxTpr_Statusmessage = AV31UrlStatus.gxTpr_Message;
               AV17UrlListItem.gxTpr_Affectedtype = AV31UrlStatus.gxTpr_Affectedtype;
               AV17UrlListItem.gxTpr_Affectedname = AV31UrlStatus.gxTpr_Affectedname;
               AV25PageItem.gxTpr_Urllist.Add(AV17UrlListItem, 0);
               AV36GXV3 = (int)(AV36GXV3+1);
            }
            AV9DebugResults.gxTpr_Pages.Add(AV25PageItem, 0);
            AV34GXV1 = (int)(AV34GXV1+1);
         }
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
         AV9DebugResults = new SdtSDT_DebugResults(context);
         AV10Error = new SdtSDT_Error(context);
         AV22pageUrl = new SdtSDT_PageUrl(context);
         AV25PageItem = new SdtSDT_DebugResults_PagesItem(context);
         AV32UrlCheckItems = new GXExternalCollection<SdtUrlCheckItem>( context, "SdtUrlCheckItem", "GeneXus.Programs");
         AV27pageUrlItem = new SdtSDT_PageUrl_UrlsItem(context);
         AV28urlCheckItem = new SdtUrlCheckItem(context);
         AV33UrlStatuses = new GXExternalCollection<SdtUrlStatus>( context, "SdtUrlStatus", "GeneXus.Programs");
         AV29UrlChecker = new SdtUrlChecker(context);
         AV30Summary = new SdtSummary(context);
         AV31UrlStatus = new SdtUrlStatus(context);
         AV17UrlListItem = new SdtSDT_DebugResults_PagesItem_UrlListItem(context);
         /* GeneXus formulas. */
      }

      private int AV34GXV1 ;
      private int AV35GXV2 ;
      private int AV36GXV3 ;
      private GXBaseCollection<SdtSDT_PageUrl> AV24PageUrlList ;
      private SdtSDT_DebugResults AV9DebugResults ;
      private SdtSDT_Error AV10Error ;
      private SdtSDT_PageUrl AV22pageUrl ;
      private SdtSDT_DebugResults_PagesItem AV25PageItem ;
      private GXExternalCollection<SdtUrlCheckItem> AV32UrlCheckItems ;
      private SdtSDT_PageUrl_UrlsItem AV27pageUrlItem ;
      private SdtUrlCheckItem AV28urlCheckItem ;
      private GXExternalCollection<SdtUrlStatus> AV33UrlStatuses ;
      private SdtUrlChecker AV29UrlChecker ;
      private SdtSummary AV30Summary ;
      private SdtUrlStatus AV31UrlStatus ;
      private SdtSDT_DebugResults_PagesItem_UrlListItem AV17UrlListItem ;
      private SdtSDT_DebugResults aP1_DebugResults ;
      private SdtSDT_Error aP2_Error ;
   }

}
