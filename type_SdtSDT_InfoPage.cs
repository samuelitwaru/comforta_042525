/*
				   File: type_SdtSDT_InfoPage
			Description: SDT_InfoPage
				 Author: Nemo 🐠 for C# (.NET) version 18.0.10.184260
		   Program type: Callable routine
			  Main DBMS: 
*/
using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using GeneXus.Http.Server;
using System.Reflection;
using System.Xml.Serialization;
using System.Runtime.Serialization;


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="SDT_InfoPage")]
	[XmlType(TypeName="SDT_InfoPage" , Namespace="Comforta_version20" )]
	[Serializable]
	public class SdtSDT_InfoPage : GxUserType
	{
		public SdtSDT_InfoPage( )
		{
			/* Constructor for serialization */
			gxTv_SdtSDT_InfoPage_Pagename = "";

		}

		public SdtSDT_InfoPage(IGxContext context)
		{
			this.context = context;	
			initialize();
		}

		#region Json
		private static Hashtable mapper;
		public override string JsonMap(string value)
		{
			if (mapper == null)
			{
				mapper = new Hashtable();
			}
			return (string)mapper[value]; ;
		}

		public override void ToJSON()
		{
			ToJSON(true) ;
			return;
		}

		public override void ToJSON(bool includeState)
		{
			AddObjectProperty("PageId", gxTpr_Pageid, false);


			AddObjectProperty("PageName", gxTpr_Pagename, false);

			if (gxTv_SdtSDT_InfoPage_Pageinfostructure != null)
			{
				AddObjectProperty("PageInfoStructure", gxTv_SdtSDT_InfoPage_Pageinfostructure, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="PageId")]
		[XmlElement(ElementName="PageId")]
		public Guid gxTpr_Pageid
		{
			get {
				return gxTv_SdtSDT_InfoPage_Pageid; 
			}
			set {
				gxTv_SdtSDT_InfoPage_Pageid = value;
				SetDirty("Pageid");
			}
		}




		[SoapElement(ElementName="PageName")]
		[XmlElement(ElementName="PageName")]
		public string gxTpr_Pagename
		{
			get {
				return gxTv_SdtSDT_InfoPage_Pagename; 
			}
			set {
				gxTv_SdtSDT_InfoPage_Pagename = value;
				SetDirty("Pagename");
			}
		}




		[SoapElement(ElementName="PageInfoStructure" )]
		[XmlArray(ElementName="PageInfoStructure"  )]
		[XmlArrayItemAttribute(ElementName="PageInfoStructureItem" , IsNullable=false )]
		public GXBaseCollection<SdtSDT_InfoPage_PageInfoStructureItem> gxTpr_Pageinfostructure
		{
			get {
				if ( gxTv_SdtSDT_InfoPage_Pageinfostructure == null )
				{
					gxTv_SdtSDT_InfoPage_Pageinfostructure = new GXBaseCollection<SdtSDT_InfoPage_PageInfoStructureItem>( context, "SDT_InfoPage.PageInfoStructureItem", "");
				}
				return gxTv_SdtSDT_InfoPage_Pageinfostructure;
			}
			set {
				gxTv_SdtSDT_InfoPage_Pageinfostructure_N = false;
				gxTv_SdtSDT_InfoPage_Pageinfostructure = value;
				SetDirty("Pageinfostructure");
			}
		}

		public void gxTv_SdtSDT_InfoPage_Pageinfostructure_SetNull()
		{
			gxTv_SdtSDT_InfoPage_Pageinfostructure_N = true;
			gxTv_SdtSDT_InfoPage_Pageinfostructure = null;
		}

		public bool gxTv_SdtSDT_InfoPage_Pageinfostructure_IsNull()
		{
			return gxTv_SdtSDT_InfoPage_Pageinfostructure == null;
		}
		public bool ShouldSerializegxTpr_Pageinfostructure_GxSimpleCollection_Json()
		{
			return gxTv_SdtSDT_InfoPage_Pageinfostructure != null && gxTv_SdtSDT_InfoPage_Pageinfostructure.Count > 0;

		}


		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Static Type Properties

		[XmlIgnore]
		private static GXTypeInfo _typeProps;
		protected override GXTypeInfo TypeInfo { get { return _typeProps; } set { _typeProps = value; } }

		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtSDT_InfoPage_Pagename = "";

			gxTv_SdtSDT_InfoPage_Pageinfostructure_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtSDT_InfoPage_Pageid;
		 

		protected string gxTv_SdtSDT_InfoPage_Pagename;
		 
		protected bool gxTv_SdtSDT_InfoPage_Pageinfostructure_N;
		protected GXBaseCollection<SdtSDT_InfoPage_PageInfoStructureItem> gxTv_SdtSDT_InfoPage_Pageinfostructure = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"SDT_InfoPage", Namespace="Comforta_version20")]
	public class SdtSDT_InfoPage_RESTInterface : GxGenericCollectionItem<SdtSDT_InfoPage>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSDT_InfoPage_RESTInterface( ) : base()
		{	
		}

		public SdtSDT_InfoPage_RESTInterface( SdtSDT_InfoPage psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="PageId", Order=0)]
		public Guid gxTpr_Pageid
		{
			get { 
				return sdt.gxTpr_Pageid;

			}
			set { 
				sdt.gxTpr_Pageid = value;
			}
		}

		[DataMember(Name="PageName", Order=1)]
		public  string gxTpr_Pagename
		{
			get { 
				return sdt.gxTpr_Pagename;

			}
			set { 
				 sdt.gxTpr_Pagename = value;
			}
		}

		[DataMember(Name="PageInfoStructure", Order=2, EmitDefaultValue=false)]
		public GxGenericCollection<SdtSDT_InfoPage_PageInfoStructureItem_RESTInterface> gxTpr_Pageinfostructure
		{
			get {
				if (sdt.ShouldSerializegxTpr_Pageinfostructure_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtSDT_InfoPage_PageInfoStructureItem_RESTInterface>(sdt.gxTpr_Pageinfostructure);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Pageinfostructure);
			}
		}


		#endregion

		public SdtSDT_InfoPage sdt
		{
			get { 
				return (SdtSDT_InfoPage)Sdt;
			}
			set { 
				Sdt = value;
			}
		}

		[OnDeserializing]
		void checkSdt( StreamingContext ctx )
		{
			if ( sdt == null )
			{
				sdt = new SdtSDT_InfoPage() ;
			}
		}
	}
	#endregion
}