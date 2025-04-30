/*
				   File: type_SdtSDT_InfoTile_SDT_InfoTileItem
			Description: SDT_InfoTile
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
	[XmlRoot(ElementName="SDT_InfoTileItem")]
	[XmlType(TypeName="SDT_InfoTileItem" , Namespace="Comforta_version20" )]
	[Serializable]
	public class SdtSDT_InfoTile_SDT_InfoTileItem : GxUserType
	{
		public SdtSDT_InfoTile_SDT_InfoTileItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tileid = "";

			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tielname = "";

			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tiletext = "";

			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilecolor = "";

			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilealign = "";

			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tileicon = "";

			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilebgcolor = "";

			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilebgimageurl = "";

		}

		public SdtSDT_InfoTile_SDT_InfoTileItem(IGxContext context)
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
			AddObjectProperty("TileId", gxTpr_Tileid, false);


			AddObjectProperty("TielName", gxTpr_Tielname, false);


			AddObjectProperty("TileText", gxTpr_Tiletext, false);


			AddObjectProperty("TileColor", gxTpr_Tilecolor, false);


			AddObjectProperty("TileAlign", gxTpr_Tilealign, false);


			AddObjectProperty("TileIcon", gxTpr_Tileicon, false);


			AddObjectProperty("TileBGColor", gxTpr_Tilebgcolor, false);


			AddObjectProperty("TileBGImageUrl", gxTpr_Tilebgimageurl, false);


			AddObjectProperty("TileBGImageOpacity", gxTpr_Tilebgimageopacity, false);

			if (gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action != null)
			{
				AddObjectProperty("Action", gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="TileId")]
		[XmlElement(ElementName="TileId")]
		public string gxTpr_Tileid
		{
			get {
				return gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tileid; 
			}
			set {
				gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tileid = value;
				SetDirty("Tileid");
			}
		}




		[SoapElement(ElementName="TielName")]
		[XmlElement(ElementName="TielName")]
		public string gxTpr_Tielname
		{
			get {
				return gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tielname; 
			}
			set {
				gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tielname = value;
				SetDirty("Tielname");
			}
		}




		[SoapElement(ElementName="TileText")]
		[XmlElement(ElementName="TileText")]
		public string gxTpr_Tiletext
		{
			get {
				return gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tiletext; 
			}
			set {
				gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tiletext = value;
				SetDirty("Tiletext");
			}
		}




		[SoapElement(ElementName="TileColor")]
		[XmlElement(ElementName="TileColor")]
		public string gxTpr_Tilecolor
		{
			get {
				return gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilecolor; 
			}
			set {
				gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilecolor = value;
				SetDirty("Tilecolor");
			}
		}




		[SoapElement(ElementName="TileAlign")]
		[XmlElement(ElementName="TileAlign")]
		public string gxTpr_Tilealign
		{
			get {
				return gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilealign; 
			}
			set {
				gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilealign = value;
				SetDirty("Tilealign");
			}
		}




		[SoapElement(ElementName="TileIcon")]
		[XmlElement(ElementName="TileIcon")]
		public string gxTpr_Tileicon
		{
			get {
				return gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tileicon; 
			}
			set {
				gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tileicon = value;
				SetDirty("Tileicon");
			}
		}




		[SoapElement(ElementName="TileBGColor")]
		[XmlElement(ElementName="TileBGColor")]
		public string gxTpr_Tilebgcolor
		{
			get {
				return gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilebgcolor; 
			}
			set {
				gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilebgcolor = value;
				SetDirty("Tilebgcolor");
			}
		}




		[SoapElement(ElementName="TileBGImageUrl")]
		[XmlElement(ElementName="TileBGImageUrl")]
		public string gxTpr_Tilebgimageurl
		{
			get {
				return gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilebgimageurl; 
			}
			set {
				gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilebgimageurl = value;
				SetDirty("Tilebgimageurl");
			}
		}



		[SoapElement(ElementName="TileBGImageOpacity")]
		[XmlElement(ElementName="TileBGImageOpacity")]
		public string gxTpr_Tilebgimageopacity_double
		{
			get {
				return Convert.ToString(gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilebgimageopacity, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilebgimageopacity = (decimal)(Convert.ToDecimal(value, System.Globalization.CultureInfo.InvariantCulture));
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Tilebgimageopacity
		{
			get {
				return gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilebgimageopacity; 
			}
			set {
				gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilebgimageopacity = value;
				SetDirty("Tilebgimageopacity");
			}
		}



		[SoapElement(ElementName="Action" )]
		[XmlElement(ElementName="Action" )]
		public SdtSDT_InfoTile_SDT_InfoTileItem_Action gxTpr_Action
		{
			get {
				if ( gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action == null )
				{
					gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action = new SdtSDT_InfoTile_SDT_InfoTileItem_Action(context);
				}
				gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action_N = false;
				return gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action;
			}
			set {
				gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action_N = false;
				gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action = value;
				SetDirty("Action");
			}

		}

		public void gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action_SetNull()
		{
			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action_N = true;
			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action = null;
		}

		public bool gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action_IsNull()
		{
			return gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action == null;
		}
		public bool ShouldSerializegxTpr_Action_Json()
		{
				return (gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action != null && gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action.ShouldSerializeSdtJson());

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
			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tileid = "";
			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tielname = "";
			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tiletext = "";
			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilecolor = "";
			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilealign = "";
			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tileicon = "";
			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilebgcolor = "";
			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilebgimageurl = "";


			gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tileid;
		 

		protected string gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tielname;
		 

		protected string gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tiletext;
		 

		protected string gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilecolor;
		 

		protected string gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilealign;
		 

		protected string gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tileicon;
		 

		protected string gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilebgcolor;
		 

		protected string gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilebgimageurl;
		 

		protected decimal gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Tilebgimageopacity;
		 
		protected bool gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action_N;
		protected SdtSDT_InfoTile_SDT_InfoTileItem_Action gxTv_SdtSDT_InfoTile_SDT_InfoTileItem_Action = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"SDT_InfoTileItem", Namespace="Comforta_version20")]
	public class SdtSDT_InfoTile_SDT_InfoTileItem_RESTInterface : GxGenericCollectionItem<SdtSDT_InfoTile_SDT_InfoTileItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSDT_InfoTile_SDT_InfoTileItem_RESTInterface( ) : base()
		{	
		}

		public SdtSDT_InfoTile_SDT_InfoTileItem_RESTInterface( SdtSDT_InfoTile_SDT_InfoTileItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="TileId", Order=0)]
		public  string gxTpr_Tileid
		{
			get { 
				return sdt.gxTpr_Tileid;

			}
			set { 
				 sdt.gxTpr_Tileid = value;
			}
		}

		[DataMember(Name="TielName", Order=1)]
		public  string gxTpr_Tielname
		{
			get { 
				return sdt.gxTpr_Tielname;

			}
			set { 
				 sdt.gxTpr_Tielname = value;
			}
		}

		[DataMember(Name="TileText", Order=2)]
		public  string gxTpr_Tiletext
		{
			get { 
				return sdt.gxTpr_Tiletext;

			}
			set { 
				 sdt.gxTpr_Tiletext = value;
			}
		}

		[DataMember(Name="TileColor", Order=3)]
		public  string gxTpr_Tilecolor
		{
			get { 
				return sdt.gxTpr_Tilecolor;

			}
			set { 
				 sdt.gxTpr_Tilecolor = value;
			}
		}

		[DataMember(Name="TileAlign", Order=4)]
		public  string gxTpr_Tilealign
		{
			get { 
				return sdt.gxTpr_Tilealign;

			}
			set { 
				 sdt.gxTpr_Tilealign = value;
			}
		}

		[DataMember(Name="TileIcon", Order=5)]
		public  string gxTpr_Tileicon
		{
			get { 
				return sdt.gxTpr_Tileicon;

			}
			set { 
				 sdt.gxTpr_Tileicon = value;
			}
		}

		[DataMember(Name="TileBGColor", Order=6)]
		public  string gxTpr_Tilebgcolor
		{
			get { 
				return sdt.gxTpr_Tilebgcolor;

			}
			set { 
				 sdt.gxTpr_Tilebgcolor = value;
			}
		}

		[DataMember(Name="TileBGImageUrl", Order=7)]
		public  string gxTpr_Tilebgimageurl
		{
			get { 
				return sdt.gxTpr_Tilebgimageurl;

			}
			set { 
				 sdt.gxTpr_Tilebgimageurl = value;
			}
		}

		[DataMember(Name="TileBGImageOpacity", Order=8)]
		public  string gxTpr_Tilebgimageopacity
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Tilebgimageopacity, 10, 5));

			}
			set { 
				sdt.gxTpr_Tilebgimageopacity =  NumberUtil.Val( value, ".");
			}
		}

		[DataMember(Name="Action", Order=9, EmitDefaultValue=false)]
		public SdtSDT_InfoTile_SDT_InfoTileItem_Action_RESTInterface gxTpr_Action
		{
			get {
				if (sdt.ShouldSerializegxTpr_Action_Json())
					return new SdtSDT_InfoTile_SDT_InfoTileItem_Action_RESTInterface(sdt.gxTpr_Action);
				else
					return null;

			}

			set {
				sdt.gxTpr_Action = value.sdt;
			}

		}


		#endregion

		public SdtSDT_InfoTile_SDT_InfoTileItem sdt
		{
			get { 
				return (SdtSDT_InfoTile_SDT_InfoTileItem)Sdt;
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
				sdt = new SdtSDT_InfoTile_SDT_InfoTileItem() ;
			}
		}
	}
	#endregion
}