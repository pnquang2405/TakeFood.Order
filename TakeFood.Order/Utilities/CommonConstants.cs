namespace Order.Utilities;

/// <summary>
/// Utility constant
/// </summary>
public static class CommonConstants
{
    /// <summary>
    /// Define place
    /// </summary>
    public const string PlaceString = "place";

    /// <summary>
    /// Authorization request header
    /// </summary>
    public const string Authen = "Authorization";

    /// <summary>
    /// Round to second
    /// </summary>
    public const long RoundTimestemp = 10000000L;

    /// <summary>
    /// Limit query
    /// </summary>
    public const int LimitQuery = 20;

    /// <summary>
    /// Max limit request
    /// </summary>
    public const int MaxLimitRequest = 500;

    /// <summary>
    /// User admin has all roles
    /// </summary>
    public const string Admin = "admin";

    /// <summary>
    /// Minimum zoom
    /// </summary>
    public const int MinZoom = 13;

    /// <summary>
    /// Maximum zoom
    /// </summary>
    public const int MaxZoom = 19;

    /// <summary>
    /// Success code
    /// </summary>
    public const string Success = "ok";

    /// <summary>
    /// Exception code
    /// </summary>
    public const string Exception = "exception";

    /// <summary>
    /// Provider google
    /// </summary>
    public const string ProviderGoogle = "ool";

    /// <summary>
    /// Provider iotlink
    /// </summary>
    public const string ProviderIotLink = "nal";

    /// <summary>
    /// Provider internal
    /// </summary>
    public const string ProviderInternal = "internal";

    /// <summary>
    /// Provider vimap
    /// </summary>
    public const string ProviderVimap = "vmp";

    /// <summary>
    /// Provider vnpost
    /// </summary>
    public const string ProviderVNPost = "vnp";

    /// <summary>
    /// Provider open street map
    /// </summary>
    public const string ProviderOSM = "oes";

    /// <summary>
    /// Provider overpass
    /// </summary>
    public const string ProviderOverpass = "opas";

    /// <summary>
    /// Provider nominatim
    /// </summary>
    public const string ProviderNominatim = "nano";

    /// <summary>
    /// Token
    /// </summary>
    public const string Token = "token";

    /// <summary>
    /// Read role
    /// </summary>
    public const string Read = "read";

    /// <summary>
    /// Write role
    /// </summary>
    public const string Write = "write";

    /// <summary>
    /// Verify role
    /// </summary>
    public const string Verify = "verify";

    /// <summary>
    /// Audience
    /// </summary>
    public const string Audience = "Iotlink";

    /// <summary>
    /// Bearer token header
    /// </summary>
    public const string Bearer = "Bearer ";

    /// <summary>
    /// Key
    /// </summary>
    public const string Key = "key";

    /// <summary>
    /// This will probably be the registered claim most often used. 
    /// This will define the expiration in NumericDate value. 
    /// The expiration MUST be after the current date/time
    /// </summary>
    public const string Expire = "exp";

    /// <summary>
    /// The display name of user
    /// </summary>
    public const string DisplayName = "display_name";

    /// <summary>
    /// The roles
    /// </summary>
    public const string Roles = "roles";

    /// <summary>
    /// The phone
    /// </summary>
    public const string Phone = "phone";

    /// <summary>
    /// The region
    /// </summary>
    public const string Region = "region";

    /// <summary>
    /// id
    /// </summary>
    public const string Id = "id";

    /// <summary>
    /// Random
    /// </summary>
    public const string Random = "r";

    /// <summary>
    /// Max radio is 500 met
    /// </summary>
    public const int MaxRadius = 150;

    /// <summary>
    /// Earth Radius In Meters
    /// </summary>
    public const int EarthRadiusInMeters = 6378137;

    /// <summary>
    /// Metter per degree latitude
    /// </summary>
    public const double MeterPerDegreeLatitude = EarthRadiusInMeters * Math.PI / 180;

    /// <summary>
    /// Keep left
    /// </summary>
    public const string KeepLeft = "keep-left";

    /// <summary>
    /// Keep right
    /// </summary>
    public const string KeepRight = "keep-right";

    /// <summary>
    /// Turn sharp right
    /// </summary>
    public const string TurnSharpRight = "turn-sharp-right";

    /// <summary>
    /// Turn sharp left
    /// </summary>
    public const string TurnSharpLeft = "turn-sharp-left";

    /// <summary>
    /// Turn left
    /// </summary>
    public const string TurnLeft = "turn-left";

    /// <summary>
    /// Turn right
    /// </summary>
    public const string TurnRight = "turn-right";

    /// <summary>
    /// Turn slight left
    /// </summary>
    public const string TurnSlightLeft = "turn-slight-left";

    /// <summary>
    /// Turn slight right
    /// </summary>
    public const string TurnSlightRight = "turn-slight-right";

    /// <summary>
    /// Straight
    /// </summary>
    public const string ContinueOnStreet = "straight";

    /// <summary>
    /// Finish
    /// </summary>
    public const string Finish = "finish";

    /// <summary>
    /// Reached via
    /// </summary>
    public const string ReachedVia = "reached-via";

    /// <summary>
    /// Roundabout left
    /// </summary>
    public const string RoundaboutLeft = "roundabout-left";

    /// <summary>
    /// Roundabout right
    /// </summary>
    public const string RoundaboutRight = "roundabout-right";

    /// <summary>
    /// Vehicles car
    /// </summary>
    public const string VehiclesCar = "car";

    /// <summary>
    /// Driving 
    /// </summary>
    public const string Driving = "driving";

    /// <summary>
    /// Vehicles bike
    /// </summary>
    public const string VehiclesBike = "bike";

    /// <summary>
    /// Cycling 
    /// </summary>
    public const string Cycling = "cycling";

    /// <summary>
    /// Vehicles foot
    /// </summary>
    public const string VehiclesFoot = "foot";

    /// <summary>
    /// Vehicles motorcycle
    /// </summary>
    public const string VehiclesMotorcycle = "motorcycle";

    /// <summary>
    /// Walking 
    /// </summary>
    public const string Walking = "walking";

    /// <summary>
    /// Mode driving
    /// </summary>
    public const string GoogleDriving = "driving";

    /// <summary>
    /// Mode walking
    /// </summary>
    public const string GoogleWalking = "walking";

    /// <summary>
    /// Mode bicycling
    /// </summary>
    public const string GoogleBicycling = "bicycling";

    /// <summary>
    /// Mode transit
    /// </summary>
    public const string GoogleTransit = "transit";

    /// <summary>
    /// Type number
    /// </summary>
    public const string Number = "number";

    /// <summary>
    /// Type link
    /// </summary>
    public const string Link = "link";

    /// <summary>
    /// Type popup
    /// </summary>
    public const string Popup = "popup";

    /// <summary>
    /// Type datetime
    /// </summary>
    public const string DateTime = "datetime";

    /// <summary>
    /// Type text
    /// </summary>
    public const string Text = "text";

    /// <summary>
    /// Prototype string
    /// </summary>
    public const string PrototypeString = "prototype";

    /// <summary>
    /// Format datetime
    /// </summary>
    public const string DateTimeFormat = "yyyy-MM-dd";

    /// <summary>
    /// Limit default radius
    /// </summary>
    public const int LimitRadius = 50000;

    /// <summary>
    /// Role all permission
    /// </summary>
    public const string RoleAdministrator = "administrator";

    /// <summary>
    /// Value fxilon minimum to compare double
    /// </summary>
    public const double Fxilon = 0.000000001;

    /// <summary>
    /// Language
    /// </summary>
    public const string Language = "language";

    /// <summary>
    /// Icon default
    /// </summary>
    public const string IconDefault = "default.png";

    /// <summary>
    /// Role user
    /// </summary>
    public const string RoleUser = "user";

    /// <summary>
    /// Distance way maximum
    /// </summary>
    public const double DistanceWay = 5400;

    /// <summary>
    /// Private action, use in permission 
    /// </summary>
    public const string NonPublicAction = "NPubA";

    /// <summary>
    /// Internal action, use in permission 
    /// </summary>
    public const string Internal = "Internal";

    /// <summary>
    /// Public action, use in permission 
    /// </summary>
    public const string PublicAction = "PubA";

    /// <summary>
    /// List place that need update tile in schedule
    /// </summary>
    public const string ListNeedUpdatePlace = "NeedUpdatePlaceList";

    /// <summary>
    /// Test Enviroment
    /// </summary>
    public const string TestEnviroment = "test";

    /// <summary>
    /// Dev Enviroment
    /// </summary>
    public const string DevEnviroment = "test";

    /// <summary>
    /// Dev Enviroment
    /// </summary>
    public const string ProductEnviroment = "production";

    /// <summary>
    /// AccessKey memory name
    /// </summary>
    public const string MemoryAccessKeys = "AccessKeys";

    /// <summary>
    /// AccessKey memory name
    /// </summary>
    public const string MemoryAccessKeyUser = "AccessKey_User";

    /// <summary>
    /// Path get maptile
    /// </summary>
    public const string PathMapTile = "/v2/api/tile";

    /// <summary>
    /// AccessKey
    /// </summary>
    public const string AccessKey = "Key";

    /// <summary>
    /// Package id
    /// </summary>
    public const string PackageId = "PackageId";

    /// <summary>
    /// Bundle id
    /// </summary>
    public const string BundleId = "BundleId";

    /// <summary>
    /// ApiPrivate Mode
    /// Hash of API@V2#Private#cuong's birthday
    /// </summary>
    public const string SwaggerPrivateName = "private";

    /// <summary>
    /// Api Internal Mode
    /// Hash of API@V2#Internal#cuong's birthday
    /// </summary>
    public const string SwaggerInternalName = "internal";

    /// <summary>
    /// Api public Mode
    /// Hash of API@V2#Public#cuong's birthday
    /// </summary>
    public const string SwaggerPublicName = "public";

    /// <summary>
    /// Swagger router prefix
    /// </summary>
    public const string SwaggerRootePrefix = "docs";

    /// <summary>
    /// Swagger Title Version
    /// </summary>
    public const string SwaggerTitleVersion = "Map4d API v2";

    /// <summary>
    /// Osm error char
    /// </summary>
    public const string OsmErrorChar = "-:-";

    /// <summary>
    /// OsmExMessage
    /// </summary>
    public const string OsmExMessage = "Quá trình lưu place vào editor.map4d.vn thất bại. Message: ";

    /// <summary>
    /// Object type decoration
    /// </summary>
    public const string ObjTypeDecoration = "decoration";

    /// <summary>
    /// Short date format
    /// </summary>
    public const string ShortDayViFormat = "dd/MM/yyyy";

    /// <summary>
    /// SDKAppId
    /// </summary>
    public const string SDKAppId = "sdk-app-id";

    /// <summary>
    /// SDKVersion
    /// </summary>
    public const string SDKVersion = "sdk-version";

    /// <summary>
    /// SDKPlatform
    /// </summary>
    public const string SDKPlatform = "sdk-platform";

    /// <summary>
    /// SDKName
    /// </summary>
    public const string SDKName = "sdk-name";

    /// <summary>
    /// SDKAppId
    /// </summary>
    public const string RequiredWebVersion = "1.3.4";

    /// <summary>
    /// SDKAppId
    /// </summary>
    public const string RequiredMobileVersion = "1.4.0";

    /// <summary>
    /// Signature
    /// </summary>
    public const string Signature = "signature";

    /// <summary>
    /// Timestamp
    /// </summary>
    public const string TimeStamp = "timestamp";

    /// <summary>
    /// Feature
    /// </summary>
    public const string Feature = "Feature";

    /// <summary>
    /// Feature Collection
    /// </summary>
    public const string FeatureCollection = "FeatureCollection";

    /// <summary>
    /// Name
    /// </summary>
    public const string Name = "name";

    /// <summary>
    /// GivenName - FirstName trong jwt trả về từ keycloak
    /// </summary>
    public const string GivenName = "given_name";

    /// <summary>
    /// Family_name - LastName trong jwt trả về từ keycloak
    /// </summary>
    public const string FamilyName = "family_name";

    /// <summary>
    /// Stroke Width
    /// </summary>
    public const string StrokeWidth = "stroke-width";

    /// <summary>
    /// PathCounter Cache Key
    /// </summary>
    public const string PathCounterCacheId = "PathCounterCache";

    /// <summary>
    /// RateLimitOptions Id
    /// </summary>
    public const string RateLimitOptionsId = "RateLimitOptions";

    /// <summary>
    /// ClientRateLimiting Id
    /// </summary>
    public const string ClientRateLimitingId = "ClientRateLimiting";

    /// <summary>
    /// IpRateLimiting Id
    /// </summary>
    public const string IpRateLimitingId = "IpRateLimiting";

    /// <summary>
    /// IpRateLimitPolicies Id
    /// </summary>
    public const string IpRateLimitPoliciesId = "IpRateLimitPolicies";

    /// <summary>
    /// ClientRateLimitPolicies Id
    /// </summary>
    public const string ClientRateLimitPoliciesId = "ClientRateLimitPolicies";

    /// <summary>
    /// RateLimitStore redis hash key
    /// </summary>
    public const string RateLimitStoreKey = "RateLimitStore";

    /// <summary>
    /// RateLimitStore redis hash key
    /// </summary>
    public const string KeyRequestHistoryToDay = "KeyRequestHistory";

    /// <summary>
    /// ByModelDefault Key
    /// </summary>
    public const string ByModelDefaultKey = "ByModelDefault";

    /// <summary>
    /// CurrentAccess Key
    /// </summary>
    public const string CurrentAccessKey = "CurrentAccessKey";

    /// <summary>
    /// CurrentAccess Key
    /// </summary>
    public const string UserId = "UserId";

    /// <summary>
    /// YYYYMMddFormat
    /// </summary>
    public const string YYYYMMddFormat = "yyyyMMdd";

    /// <summary>
    /// Storage client key
    /// </summary>
    public const string StorageHttpClientKey = "StorageHttpClient";

    /// <summary>
    /// Storage client key
    /// </summary>
    public const string KeyCloakHttpClient = "KeyCloakHttpClient";

    /// <summary>
    /// x-auth-token key
    /// </summary>
    public const string XAuthenTokenKey = "x-auth-token";

    /// <summary>
    /// ApplicationJson Accept
    /// </summary>
    public const string ApplicationJsonAccept = "application/json";

    /// <summary>
    /// AcceptStream
    /// </summary>
    public const string AcceptStream = "application/octet-stream";

    /// <summary>
    /// HttpRequestUser Agent key
    /// </summary>
    public const string UserAgentKey = "user-agent";

    /// <summary>
    /// HttpRequestUser Agent value
    /// </summary>
    public const string UserAgentValue = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36";

    /// <summary>
    /// HttpRequestUser Agent value
    /// </summary>
    public const string TileHttpClientFactory = "tileHttpClientFactory";

    /// <summary>
    /// Sort type ASC
    /// </summary>
    public const string SortTypeASC = "asc";

    /// <summary>
    /// Sort type DESC
    /// </summary>
    public const string SortTypeDESC = "desc";

    /// <summary>
    /// Cache Control header
    /// </summary>
    public static string CacheControl = "Cache-Control";

    /// <summary>
    /// SignalTraffic
    /// </summary>
    public static string SignalTraffic = "signal_traffic";

    /// <summary>
    /// POIModelName
    /// </summary>
    public const string POIModelName = "model.obj";

    /// <summary>
    /// Point
    /// </summary>
    public const string Point = "point";

    /// <summary>
    /// POIIconName
    /// </summary>
    public const string POIIconName = "icon";

    /// <summary>
    /// POITexturenName
    /// </summary>
    public const string POITexturenName = "texture";

    /// <summary>
    /// Quốc gia
    /// </summary>
    public const string AdminLevel1 = "admin_level_1";

    /// <summary>
    /// Tỉnh/ thành phố trực thuộc TW
    /// </summary>
    public const string AdminLevel2 = "admin_level_2";

    /// <summary>
    /// Quận huyện/ thành phố trực KHÔNG thuộc TW
    /// </summary>
    public const string AdminLevel3 = "admin_level_3";

    /// <summary>
    /// Xã huyện
    /// </summary>
    public const string AdminLevel4 = "admin_level_4";

    /// <summary>
    /// Hamlet
    /// </summary>
    public const string Hamlet = "hamlet";

    /// <summary>
    /// Street
    /// </summary>
    public const string Street = "street";
    /// <summary>
    /// postcode
    /// </summary>
    public const string Postcode = "postcode";

    /// <summary>
    /// Level1
    /// </summary>
    public const string Level1 = "level1";

    /// <summary>
    /// Level2
    /// </summary>
    public const string Level2 = "level2";

    /// <summary>
    /// Level3
    /// </summary>
    public const string Level3 = "level3";

    /// <summary>
    /// Level4
    /// </summary>
    public const string Level4 = "level4";

    /// <summary>
    /// Query
    /// </summary>
    public const string Query = "query";

    /// <summary>
    /// Extra. store query, name....
    /// </summary>
    public const string Extra = "extra";

    /// <summary>
    /// HouseNumber
    /// </summary>
    public const string HouseNumber = "housenumber";

    /// <summary>
    /// Realm access
    /// </summary>
    public const string RealmAccess = "realm_access";

    /// <summary>
    /// Resource access
    /// </summary>
    public const string ResourceAccess = "resource_access";

    /// <summary>
    /// SupportLanguage
    /// </summary>
    public static readonly string[] SupportLanguage = { "vi", "en", "fr", "ja" };

    /// <summary>
    /// Default DateTime
    /// </summary>
    public static readonly string DefaultDateTime = "DD/MM/YYYY";

    /// <summary>
    /// User Not Found
    /// </summary>
    public static string UserNotFound = "user_not_found";

    /// <summary>
    /// Outbound User
    /// </summary>
    public static string OutboundUser = "outbound_user";

    /// <summary>
    /// Map4D ProductName
    /// </summary>
    public static string Map4DProductName = "Map4D SDK";

    /// <summary>
    /// WeatherHttpClient
    /// </summary>
    public static string WeatherHttpClient = "WeatherHttpClient";

    /// <summary>
    /// Route client key
    /// </summary>
    public const string RouteHttpClient = "RouteHttpClient";

    /// <summary>
    /// Parser client key
    /// </summary>
    public const string ParserHttpClient = "ParserHttpClient";

    /// <summary>
    /// Google http client name
    /// </summary>
    public static string GoogleHttpClient = "GoogleHttpClient";

    /// <summary>
    /// MaxFile is 10Mb
    /// </summary>
    public static string MaxFile10Mb = "max_file_10mb";

    /// <summary>
    /// Tag for route
    /// </summary>
    public static string Route = "route";

    /// <summary>
    /// Parser Client
    /// </summary>
    public static string ParserClient = "ParserClient";

    /// <summary>
    /// Interpolation Client
    /// </summary>
    public static string InterpolationClient = "InterpolationClient";

    /// <summary>
    /// Payment Description
    /// </summary>
    public const string PaymentDescription = "payment_description";

    /// <summary>
    /// Layer Venue
    /// </summary>
    public const string Venue = "venue";

    /// <summary>
    /// Layer Address
    /// </summary>
    public const string Address = "address";

    /// <summary>
    /// Layer
    /// </summary>
    public const string Layer = "layer";

    /// <summary>
    /// ElasticSearch Factory
    /// </summary>
    public const string ElasticSearchFactory = "ElasticSearchFactory";


    /// <summary>
    /// Define object
    /// </summary>
    public const string ObjectString = "object";

    /// <summary>
    /// Define object
    /// </summary>
    public const string ObjectStringVi = "đối tượng";

    /// <summary>
    /// Define place
    /// </summary>
    public const string PlaceStringVi = "địa điểm";

    /// <summary>
    /// Method add
    /// </summary>
    public const string MethodCreate = "thêm mới";

    /// <summary>
    /// Method update
    /// </summary>
    public const string MethodUpdate = "chỉnh sửa";

    /// <summary>
    /// Method delete
    /// </summary>
    public const string MethodDelete = "xóa";


    /// <summary>
    /// Method add
    /// </summary>
    public const string Create = "Create";

    /// <summary>
    /// Method update
    /// </summary>
    public const string Update = "Update";

    /// <summary>
    /// Method delete
    /// </summary>
    public const string Delete = "Delete";

    /// <summary>
    /// Document
    /// </summary>
    public const string DocumentName = "v1";

    /// <summary>
    /// Http get
    /// </summary>
    public const string HttpGet = "get";

    /// <summary>
    /// Http post
    /// </summary>
    public const string HttpPost = "post";

    /// <summary>
    /// Http put
    /// </summary>
    public const string HttpPut = "put";

    /// <summary>
    /// Http delete
    /// </summary>
    public const string HttpDelete = "delete";

    /// <summary>
    /// Http path
    /// </summary>
    public const string HttpPath = "path";

    /// <summary>
    /// Object Verify
    /// </summary>
    public const string ObjectVerify = "object_verify";

    /// <summary>
    /// Place Verify
    /// </summary>
    public const string PlaceVerify = "place_verify";

    /// <summary>
    /// Verify permission
    /// </summary>
    public const string VerifyPermission = "VerifyPermission";

    /// <summary>
    /// Ip info client
    /// </summary>
    public const string IpInfoClient = "IpInfoClient";

    /// <summary>
    /// Fail status
    /// </summary>
    public const string Fail = "fail";

    /// <summary>
    /// SMS FPT client key
    /// </summary>
    public const string SmsFptHttpClientKey = "SmsFptHttpClientKey";

    /// <summary>
    /// Password
    /// </summary>
    public const string Password = "password";

    /// <summary>
    /// Accept english language
    /// </summary>
    public const string EnglishLanguage = "en";

}
