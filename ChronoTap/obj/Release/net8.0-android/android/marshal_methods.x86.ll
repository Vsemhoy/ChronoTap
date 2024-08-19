; ModuleID = 'marshal_methods.x86.ll'
source_filename = "marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [124 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [248 x i32] [
	i32 1427180, ; 0: ChronoTap.dll => 0x15c6ec => 84
	i32 42639949, ; 1: System.Threading.Thread => 0x28aa24d => 114
	i32 67008169, ; 2: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 33
	i32 72070932, ; 3: Microsoft.Maui.Graphics.dll => 0x44bb714 => 50
	i32 117431740, ; 4: System.Runtime.InteropServices => 0x6ffddbc => 109
	i32 165246403, ; 5: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 60
	i32 182336117, ; 6: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 78
	i32 195452805, ; 7: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 30
	i32 199333315, ; 8: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 31
	i32 205061960, ; 9: System.ComponentModel => 0xc38ff48 => 91
	i32 280992041, ; 10: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 2
	i32 317674968, ; 11: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 30
	i32 318968648, ; 12: Xamarin.AndroidX.Activity.dll => 0x13031348 => 56
	i32 336156722, ; 13: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 15
	i32 342366114, ; 14: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 67
	i32 347068432, ; 15: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 54
	i32 356389973, ; 16: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 14
	i32 379916513, ; 17: System.Threading.Thread.dll => 0x16a510e1 => 114
	i32 385762202, ; 18: System.Memory.dll => 0x16fe439a => 99
	i32 395744057, ; 19: _Microsoft.Android.Resource.Designer => 0x17969339 => 34
	i32 435591531, ; 20: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 26
	i32 442565967, ; 21: System.Collections => 0x1a61054f => 88
	i32 450948140, ; 22: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 66
	i32 456227837, ; 23: System.Web.HttpUtility.dll => 0x1b317bfd => 116
	i32 469710990, ; 24: System.dll => 0x1bff388e => 118
	i32 498788369, ; 25: System.ObjectModel => 0x1dbae811 => 105
	i32 500358224, ; 26: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 13
	i32 503918385, ; 27: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 7
	i32 513247710, ; 28: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 44
	i32 539058512, ; 29: Microsoft.Extensions.Logging => 0x20216150 => 41
	i32 592146354, ; 30: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 21
	i32 597488923, ; 31: CommunityToolkit.Maui => 0x239cf51b => 35
	i32 627609679, ; 32: Xamarin.AndroidX.CustomView => 0x2568904f => 64
	i32 627931235, ; 33: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 19
	i32 672442732, ; 34: System.Collections.Concurrent => 0x2814a96c => 85
	i32 688181140, ; 35: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 1
	i32 706645707, ; 36: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 16
	i32 709557578, ; 37: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 4
	i32 722857257, ; 38: System.Runtime.Loader.dll => 0x2b15ed29 => 110
	i32 748832960, ; 39: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 52
	i32 759454413, ; 40: System.Net.Requests => 0x2d445acd => 103
	i32 775507847, ; 41: System.IO.Compression => 0x2e394f87 => 96
	i32 777317022, ; 42: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 25
	i32 789151979, ; 43: Microsoft.Extensions.Options => 0x2f0980eb => 43
	i32 823281589, ; 44: System.Private.Uri.dll => 0x311247b5 => 106
	i32 830298997, ; 45: System.IO.Compression.Brotli => 0x317d5b75 => 95
	i32 904024072, ; 46: System.ComponentModel.Primitives.dll => 0x35e25008 => 89
	i32 926902833, ; 47: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 28
	i32 967690846, ; 48: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 67
	i32 992768348, ; 49: System.Collections.dll => 0x3b2c715c => 88
	i32 1012816738, ; 50: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 77
	i32 1028951442, ; 51: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 40
	i32 1029334545, ; 52: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 3
	i32 1035644815, ; 53: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 57
	i32 1044663988, ; 54: System.Linq.Expressions.dll => 0x3e444eb4 => 97
	i32 1052210849, ; 55: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 69
	i32 1082857460, ; 56: System.ComponentModel.TypeConverter => 0x408b17f4 => 90
	i32 1084122840, ; 57: Xamarin.Kotlin.StdLib => 0x409e66d8 => 82
	i32 1098259244, ; 58: System => 0x41761b2c => 118
	i32 1118262833, ; 59: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 16
	i32 1168523401, ; 60: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 22
	i32 1178241025, ; 61: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 74
	i32 1203215381, ; 62: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 20
	i32 1234928153, ; 63: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 18
	i32 1260983243, ; 64: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 2
	i32 1292207520, ; 65: SQLitePCLRaw.core.dll => 0x4d0585a0 => 53
	i32 1293217323, ; 66: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 65
	i32 1324164729, ; 67: System.Linq => 0x4eed2679 => 98
	i32 1373134921, ; 68: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 32
	i32 1376866003, ; 69: Xamarin.AndroidX.SavedState => 0x52114ed3 => 77
	i32 1406073936, ; 70: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 61
	i32 1430672901, ; 71: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 0
	i32 1461004990, ; 72: es\Microsoft.Maui.Controls.resources => 0x57152abe => 6
	i32 1461234159, ; 73: System.Collections.Immutable.dll => 0x5718a9ef => 86
	i32 1462112819, ; 74: System.IO.Compression.dll => 0x57261233 => 96
	i32 1469204771, ; 75: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 58
	i32 1470490898, ; 76: Microsoft.Extensions.Primitives => 0x57a5e912 => 44
	i32 1479771757, ; 77: System.Collections.Immutable => 0x5833866d => 86
	i32 1480492111, ; 78: System.IO.Compression.Brotli.dll => 0x583e844f => 95
	i32 1493001747, ; 79: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 10
	i32 1514721132, ; 80: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 5
	i32 1543031311, ; 81: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 113
	i32 1551623176, ; 82: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 25
	i32 1622152042, ; 83: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 71
	i32 1624863272, ; 84: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 80
	i32 1634654947, ; 85: CommunityToolkit.Maui.Core.dll => 0x616edae3 => 36
	i32 1636350590, ; 86: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 63
	i32 1639515021, ; 87: System.Net.Http.dll => 0x61b9038d => 100
	i32 1639986890, ; 88: System.Text.RegularExpressions => 0x61c036ca => 113
	i32 1657153582, ; 89: System.Runtime => 0x62c6282e => 111
	i32 1658251792, ; 90: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 81
	i32 1677501392, ; 91: System.Net.Primitives.dll => 0x63fca3d0 => 102
	i32 1679769178, ; 92: System.Security.Cryptography => 0x641f3e5a => 112
	i32 1711441057, ; 93: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 54
	i32 1729485958, ; 94: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 59
	i32 1736233607, ; 95: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 23
	i32 1743415430, ; 96: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 1
	i32 1763938596, ; 97: System.Diagnostics.TraceSource.dll => 0x69239124 => 94
	i32 1766324549, ; 98: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 78
	i32 1770582343, ; 99: Microsoft.Extensions.Logging.dll => 0x6988f147 => 41
	i32 1780572499, ; 100: Mono.Android.Runtime.dll => 0x6a216153 => 122
	i32 1782862114, ; 101: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 17
	i32 1788241197, ; 102: Xamarin.AndroidX.Fragment => 0x6a96652d => 66
	i32 1793755602, ; 103: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 9
	i32 1808609942, ; 104: Xamarin.AndroidX.Loader => 0x6bcd3296 => 71
	i32 1813058853, ; 105: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 82
	i32 1813201214, ; 106: Xamarin.Google.Android.Material => 0x6c13413e => 81
	i32 1818569960, ; 107: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 75
	i32 1828688058, ; 108: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 42
	i32 1842015223, ; 109: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 29
	i32 1853025655, ; 110: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 26
	i32 1858542181, ; 111: System.Linq.Expressions => 0x6ec71a65 => 97
	i32 1875935024, ; 112: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 8
	i32 1910275211, ; 113: System.Collections.NonGeneric.dll => 0x71dc7c8b => 87
	i32 1968388702, ; 114: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 37
	i32 2003115576, ; 115: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 5
	i32 2016477049, ; 116: ChronoTap => 0x7830ff79 => 84
	i32 2019465201, ; 117: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 69
	i32 2025202353, ; 118: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 0
	i32 2045470958, ; 119: System.Private.Xml => 0x79eb68ee => 107
	i32 2055257422, ; 120: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 68
	i32 2066184531, ; 121: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 4
	i32 2070888862, ; 122: System.Diagnostics.TraceSource => 0x7b6f419e => 94
	i32 2079903147, ; 123: System.Runtime.dll => 0x7bf8cdab => 111
	i32 2090596640, ; 124: System.Numerics.Vectors => 0x7c9bf920 => 104
	i32 2103459038, ; 125: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 55
	i32 2127167465, ; 126: System.Console => 0x7ec9ffe9 => 92
	i32 2159891885, ; 127: Microsoft.Maui => 0x80bd55ad => 48
	i32 2169148018, ; 128: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 12
	i32 2181898931, ; 129: Microsoft.Extensions.Options.dll => 0x820d22b3 => 43
	i32 2192057212, ; 130: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 42
	i32 2193016926, ; 131: System.ObjectModel.dll => 0x82b6c85e => 105
	i32 2201107256, ; 132: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 83
	i32 2201231467, ; 133: System.Net.Http => 0x8334206b => 100
	i32 2207618523, ; 134: it\Microsoft.Maui.Controls.resources => 0x839595db => 14
	i32 2266799131, ; 135: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 38
	i32 2270573516, ; 136: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 8
	i32 2279755925, ; 137: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 76
	i32 2298471582, ; 138: System.Net.Mail => 0x88ffe49e => 101
	i32 2303942373, ; 139: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 18
	i32 2305521784, ; 140: System.Private.CoreLib.dll => 0x896b7878 => 120
	i32 2340441535, ; 141: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 108
	i32 2353062107, ; 142: System.Net.Primitives => 0x8c40e0db => 102
	i32 2368005991, ; 143: System.Xml.ReaderWriter.dll => 0x8d24e767 => 117
	i32 2371007202, ; 144: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 37
	i32 2395872292, ; 145: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 13
	i32 2401565422, ; 146: System.Web.HttpUtility => 0x8f24faee => 116
	i32 2427813419, ; 147: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 10
	i32 2435356389, ; 148: System.Console.dll => 0x912896e5 => 92
	i32 2465273461, ; 149: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 52
	i32 2471841756, ; 150: netstandard.dll => 0x93554fdc => 119
	i32 2475788418, ; 151: Java.Interop.dll => 0x93918882 => 121
	i32 2480646305, ; 152: Microsoft.Maui.Controls => 0x93dba8a1 => 46
	i32 2550873716, ; 153: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 11
	i32 2593496499, ; 154: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 20
	i32 2605712449, ; 155: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 83
	i32 2617129537, ; 156: System.Private.Xml.dll => 0x9bfe3a41 => 107
	i32 2620871830, ; 157: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 63
	i32 2626831493, ; 158: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 15
	i32 2663698177, ; 159: System.Runtime.Loader => 0x9ec4cf01 => 110
	i32 2732626843, ; 160: Xamarin.AndroidX.Activity => 0xa2e0939b => 56
	i32 2737747696, ; 161: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 58
	i32 2752995522, ; 162: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 21
	i32 2758225723, ; 163: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 47
	i32 2764765095, ; 164: Microsoft.Maui.dll => 0xa4caf7a7 => 48
	i32 2778768386, ; 165: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 79
	i32 2785988530, ; 166: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 27
	i32 2801831435, ; 167: Microsoft.Maui.Graphics => 0xa7008e0b => 50
	i32 2806116107, ; 168: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 6
	i32 2810250172, ; 169: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 61
	i32 2831556043, ; 170: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 19
	i32 2853208004, ; 171: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 79
	i32 2861189240, ; 172: Microsoft.Maui.Essentials => 0xaa8a4878 => 49
	i32 2868488919, ; 173: CommunityToolkit.Maui.Core => 0xaaf9aad7 => 36
	i32 2909740682, ; 174: System.Private.CoreLib => 0xad6f1e8a => 120
	i32 2916838712, ; 175: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 80
	i32 2919462931, ; 176: System.Numerics.Vectors.dll => 0xae037813 => 104
	i32 2959614098, ; 177: System.ComponentModel.dll => 0xb0682092 => 91
	i32 2978675010, ; 178: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 65
	i32 3038032645, ; 179: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 34
	i32 3057625584, ; 180: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 72
	i32 3059408633, ; 181: Mono.Android.Runtime => 0xb65adef9 => 122
	i32 3059793426, ; 182: System.ComponentModel.Primitives => 0xb660be12 => 89
	i32 3077302341, ; 183: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 12
	i32 3178803400, ; 184: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 73
	i32 3220365878, ; 185: System.Threading => 0xbff2e236 => 115
	i32 3258312781, ; 186: Xamarin.AndroidX.CardView => 0xc235e84d => 59
	i32 3286872994, ; 187: SQLite-net.dll => 0xc3e9b3a2 => 51
	i32 3305363605, ; 188: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 7
	i32 3316684772, ; 189: System.Net.Requests.dll => 0xc5b097e4 => 103
	i32 3317135071, ; 190: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 64
	i32 3346324047, ; 191: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 74
	i32 3357674450, ; 192: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 24
	i32 3360279109, ; 193: SQLitePCLRaw.core => 0xc849ca45 => 53
	i32 3362522851, ; 194: Xamarin.AndroidX.Core => 0xc86c06e3 => 62
	i32 3366347497, ; 195: Java.Interop => 0xc8a662e9 => 121
	i32 3374999561, ; 196: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 76
	i32 3381016424, ; 197: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 3
	i32 3428513518, ; 198: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 39
	i32 3430777524, ; 199: netstandard => 0xcc7d82b4 => 119
	i32 3452344032, ; 200: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 45
	i32 3463511458, ; 201: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 11
	i32 3471940407, ; 202: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 90
	i32 3476120550, ; 203: Mono.Android => 0xcf3163e6 => 123
	i32 3479583265, ; 204: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 24
	i32 3484440000, ; 205: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 23
	i32 3580758918, ; 206: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 31
	i32 3608519521, ; 207: System.Linq.dll => 0xd715a361 => 98
	i32 3624195450, ; 208: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 108
	i32 3641597786, ; 209: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 68
	i32 3643446276, ; 210: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 28
	i32 3643854240, ; 211: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 73
	i32 3657292374, ; 212: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 38
	i32 3672681054, ; 213: Mono.Android.dll => 0xdae8aa5e => 123
	i32 3697841164, ; 214: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 33
	i32 3724971120, ; 215: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 72
	i32 3748608112, ; 216: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 93
	i32 3754567612, ; 217: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 55
	i32 3786282454, ; 218: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 60
	i32 3792276235, ; 219: System.Collections.NonGeneric => 0xe2098b0b => 87
	i32 3800979733, ; 220: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 45
	i32 3817368567, ; 221: CommunityToolkit.Maui.dll => 0xe3886bf7 => 35
	i32 3823082795, ; 222: System.Security.Cryptography.dll => 0xe3df9d2b => 112
	i32 3841636137, ; 223: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 40
	i32 3844307129, ; 224: System.Net.Mail.dll => 0xe52378b9 => 101
	i32 3849253459, ; 225: System.Runtime.InteropServices.dll => 0xe56ef253 => 109
	i32 3876362041, ; 226: SQLite-net => 0xe70c9739 => 51
	i32 3889960447, ; 227: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 32
	i32 3896106733, ; 228: System.Collections.Concurrent.dll => 0xe839deed => 85
	i32 3896760992, ; 229: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 62
	i32 3928044579, ; 230: System.Xml.ReaderWriter => 0xea213423 => 117
	i32 3931092270, ; 231: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 75
	i32 3955647286, ; 232: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 57
	i32 3980434154, ; 233: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 27
	i32 3987592930, ; 234: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 9
	i32 4025784931, ; 235: System.Memory => 0xeff49a63 => 99
	i32 4046471985, ; 236: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 47
	i32 4073602200, ; 237: System.Threading.dll => 0xf2ce3c98 => 115
	i32 4094352644, ; 238: Microsoft.Maui.Essentials.dll => 0xf40add04 => 49
	i32 4100113165, ; 239: System.Private.Uri => 0xf462c30d => 106
	i32 4102112229, ; 240: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 22
	i32 4125707920, ; 241: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 17
	i32 4126470640, ; 242: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 39
	i32 4150914736, ; 243: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 29
	i32 4182413190, ; 244: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 70
	i32 4213026141, ; 245: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 93
	i32 4271975918, ; 246: Microsoft.Maui.Controls.dll => 0xfea12dee => 46
	i32 4292120959 ; 247: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 70
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [248 x i32] [
	i32 84, ; 0
	i32 114, ; 1
	i32 33, ; 2
	i32 50, ; 3
	i32 109, ; 4
	i32 60, ; 5
	i32 78, ; 6
	i32 30, ; 7
	i32 31, ; 8
	i32 91, ; 9
	i32 2, ; 10
	i32 30, ; 11
	i32 56, ; 12
	i32 15, ; 13
	i32 67, ; 14
	i32 54, ; 15
	i32 14, ; 16
	i32 114, ; 17
	i32 99, ; 18
	i32 34, ; 19
	i32 26, ; 20
	i32 88, ; 21
	i32 66, ; 22
	i32 116, ; 23
	i32 118, ; 24
	i32 105, ; 25
	i32 13, ; 26
	i32 7, ; 27
	i32 44, ; 28
	i32 41, ; 29
	i32 21, ; 30
	i32 35, ; 31
	i32 64, ; 32
	i32 19, ; 33
	i32 85, ; 34
	i32 1, ; 35
	i32 16, ; 36
	i32 4, ; 37
	i32 110, ; 38
	i32 52, ; 39
	i32 103, ; 40
	i32 96, ; 41
	i32 25, ; 42
	i32 43, ; 43
	i32 106, ; 44
	i32 95, ; 45
	i32 89, ; 46
	i32 28, ; 47
	i32 67, ; 48
	i32 88, ; 49
	i32 77, ; 50
	i32 40, ; 51
	i32 3, ; 52
	i32 57, ; 53
	i32 97, ; 54
	i32 69, ; 55
	i32 90, ; 56
	i32 82, ; 57
	i32 118, ; 58
	i32 16, ; 59
	i32 22, ; 60
	i32 74, ; 61
	i32 20, ; 62
	i32 18, ; 63
	i32 2, ; 64
	i32 53, ; 65
	i32 65, ; 66
	i32 98, ; 67
	i32 32, ; 68
	i32 77, ; 69
	i32 61, ; 70
	i32 0, ; 71
	i32 6, ; 72
	i32 86, ; 73
	i32 96, ; 74
	i32 58, ; 75
	i32 44, ; 76
	i32 86, ; 77
	i32 95, ; 78
	i32 10, ; 79
	i32 5, ; 80
	i32 113, ; 81
	i32 25, ; 82
	i32 71, ; 83
	i32 80, ; 84
	i32 36, ; 85
	i32 63, ; 86
	i32 100, ; 87
	i32 113, ; 88
	i32 111, ; 89
	i32 81, ; 90
	i32 102, ; 91
	i32 112, ; 92
	i32 54, ; 93
	i32 59, ; 94
	i32 23, ; 95
	i32 1, ; 96
	i32 94, ; 97
	i32 78, ; 98
	i32 41, ; 99
	i32 122, ; 100
	i32 17, ; 101
	i32 66, ; 102
	i32 9, ; 103
	i32 71, ; 104
	i32 82, ; 105
	i32 81, ; 106
	i32 75, ; 107
	i32 42, ; 108
	i32 29, ; 109
	i32 26, ; 110
	i32 97, ; 111
	i32 8, ; 112
	i32 87, ; 113
	i32 37, ; 114
	i32 5, ; 115
	i32 84, ; 116
	i32 69, ; 117
	i32 0, ; 118
	i32 107, ; 119
	i32 68, ; 120
	i32 4, ; 121
	i32 94, ; 122
	i32 111, ; 123
	i32 104, ; 124
	i32 55, ; 125
	i32 92, ; 126
	i32 48, ; 127
	i32 12, ; 128
	i32 43, ; 129
	i32 42, ; 130
	i32 105, ; 131
	i32 83, ; 132
	i32 100, ; 133
	i32 14, ; 134
	i32 38, ; 135
	i32 8, ; 136
	i32 76, ; 137
	i32 101, ; 138
	i32 18, ; 139
	i32 120, ; 140
	i32 108, ; 141
	i32 102, ; 142
	i32 117, ; 143
	i32 37, ; 144
	i32 13, ; 145
	i32 116, ; 146
	i32 10, ; 147
	i32 92, ; 148
	i32 52, ; 149
	i32 119, ; 150
	i32 121, ; 151
	i32 46, ; 152
	i32 11, ; 153
	i32 20, ; 154
	i32 83, ; 155
	i32 107, ; 156
	i32 63, ; 157
	i32 15, ; 158
	i32 110, ; 159
	i32 56, ; 160
	i32 58, ; 161
	i32 21, ; 162
	i32 47, ; 163
	i32 48, ; 164
	i32 79, ; 165
	i32 27, ; 166
	i32 50, ; 167
	i32 6, ; 168
	i32 61, ; 169
	i32 19, ; 170
	i32 79, ; 171
	i32 49, ; 172
	i32 36, ; 173
	i32 120, ; 174
	i32 80, ; 175
	i32 104, ; 176
	i32 91, ; 177
	i32 65, ; 178
	i32 34, ; 179
	i32 72, ; 180
	i32 122, ; 181
	i32 89, ; 182
	i32 12, ; 183
	i32 73, ; 184
	i32 115, ; 185
	i32 59, ; 186
	i32 51, ; 187
	i32 7, ; 188
	i32 103, ; 189
	i32 64, ; 190
	i32 74, ; 191
	i32 24, ; 192
	i32 53, ; 193
	i32 62, ; 194
	i32 121, ; 195
	i32 76, ; 196
	i32 3, ; 197
	i32 39, ; 198
	i32 119, ; 199
	i32 45, ; 200
	i32 11, ; 201
	i32 90, ; 202
	i32 123, ; 203
	i32 24, ; 204
	i32 23, ; 205
	i32 31, ; 206
	i32 98, ; 207
	i32 108, ; 208
	i32 68, ; 209
	i32 28, ; 210
	i32 73, ; 211
	i32 38, ; 212
	i32 123, ; 213
	i32 33, ; 214
	i32 72, ; 215
	i32 93, ; 216
	i32 55, ; 217
	i32 60, ; 218
	i32 87, ; 219
	i32 45, ; 220
	i32 35, ; 221
	i32 112, ; 222
	i32 40, ; 223
	i32 101, ; 224
	i32 109, ; 225
	i32 51, ; 226
	i32 32, ; 227
	i32 85, ; 228
	i32 62, ; 229
	i32 117, ; 230
	i32 75, ; 231
	i32 57, ; 232
	i32 27, ; 233
	i32 9, ; 234
	i32 99, ; 235
	i32 47, ; 236
	i32 115, ; 237
	i32 49, ; 238
	i32 106, ; 239
	i32 22, ; 240
	i32 17, ; 241
	i32 39, ; 242
	i32 29, ; 243
	i32 70, ; 244
	i32 93, ; 245
	i32 46, ; 246
	i32 70 ; 247
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.2xx @ 96b6bb65e8736e45180905177aa343f0e1854ea3"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"NumRegisterParameters", i32 0}
