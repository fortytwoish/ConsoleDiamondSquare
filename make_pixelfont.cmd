@Echo Off
Cls
Title PIXELFNT.EXE example
Color 0A

If Not Exist "PIXELFNT.EXE" Call :Create_PixelFnt

PIXELFNT.EXE 1

Echo(  #     #
Echo(   #   #
Echo(  #######
Echo( ## ## ###
Echo(###########
Echo(# ####### #
Echo(# #     # #
Echo(   ## ##

Pause >Nul
Goto :Eof

:Create_PixelFnt
Rem PixelFnt v1.4
Rem Source code of pixelfnt.exe at consolesoft.com/p/bg
Rem Script made using BHX 5.3 { consolesoft.com/p/bhx }
SetLocal EnableExtensions EnableDelayedExpansion
Set "bin=PIXELFNT.CAB"
Set "size=3265"
For %%# In (
"PIXELFNT.EXE"
"!bin!" "!bin!.da" "!bin!.tmp"
) Do If Exist "%%#" (Del /A /F /Q "%%#" >Nul 2>&1
If ErrorLevel 1 Exit /B 1 )
Findstr /B /N ":+res:!bin!:" "%~f0" >"!bin!.tmp"
(Set /P "inioff=" &Set /P "endoff=") <"!bin!.tmp"
For /F "delims=:" %%# In ("!inioff!") Do Set "inioff=%%#"
For /F "delims=:" %%# In ("!endoff!") Do Set "endoff=%%#"
Set ".=ado=#adodb.stream#"
Set ".=!.!: set a=createobject(ado) : a.type=1 : a.open"
Set ".=!.!: set u=createobject(ado) : u.type=2 : u.open"
Set ".=!.!: set fs=createobject(#scripting.filesystemobject#)"
Set ".=!.!: set s=fs.opentextfile(#%~f0#,1,0,0)"
Set ".=!.!: for i=1 to !inioff! step 1 : s.readline : next"
Set ".=!.!: do while i<!endoff! : d=trim(s.readline)"
Set ".=!.!: for j=1 to len(d) step 2"
Set ".=!.!: u.writetext chrb(#&h#&mid(d,j,2))"
Set ".=!.!: next : i=i+1 : loop"
Set ".=!.!: u.position=2 : u.copyto a : u.close : set u=nothing"
Set ".=!.!: a.savetofile #!bin!#,2 : a.close : set a=nothing"
Set ".=!.!: s.close : set s=nothing : set fs=nothing"
Set ".=!.:#="!"
Echo !.!>"!bin!.da"
Set "ret=1"
Cscript.exe /B /E:vbs "!bin!.da" >Nul
For %%# In ("!bin!") Do If "%%~z#"=="!size!" Set "ret=0"
If "0"=="!ret!" Expand.exe -r "!bin!" -F:* . >Nul
If ErrorLevel 1 Set "ret=1"
Del /A /F "!bin!" "!bin!.da" "!bin!.tmp" >Nul
Exit /B !ret!

:+res:PIXELFNT.CAB:
4D53434600000000C10C0000000000002C000000000000000301010001000000
000000004900000001000312001E00000000000000004D46170D000050495845
4C464E542E455845004E82C2EF700C001E5B80808D011003E000002263563400
000F00DDFDF2CABBBCC85435E0591E5281BDA6D0B638AA5DA656B90F3B643B2A
CDE7D567779B746977DCDBB0E56C905AE7DCF7ACF5832A404989252969B62543
15A3006191429612DA1911154622B6ADF2441660A5E2E500002383803400700E
56ACFD35FF30E640CEB0286B261CE07F806A6DCC066EA02DD56CDDBA5757DA48
B7DC8E6ED9D06DDD9DDB5699AE5CAF75A388B299DD9206321005459A60BE8078
72F78958FE0020000004000A88A108EDB965D68C345551858B2E718314173AFF
3A90F8FF5FC7F45ACE22FE52624C5242A1A1984F778F02F6D6B3AAEA5D222B55
C2F57E287ABF67FF067A06BC1C14327417E126C04B3E9B007DEEE54BC744A1B9
7CFEEE6EF52F7E800AD7E1C52AAC62FCE3684C97CA9AB1300866BF4D44EA8412
E39C6FCCFE2E7EAD6D5178A06D44D3749A34C38FB76A75B70E0934D9E92A7FFA
A4983E0C927068FF6BE445EE6C42B09B8FE790236F408636B054D1404AF8DAEA
6A84E4B1E1D0CD54256A99EE8A3A98612D1C9E6B8F882A01E3065309F98B1895
9027C18F68DE30C9948554531C19D5938E98AB1C45D1E20AF7AB07EBA429B123
CA0CCD228F37369B14F2A5038A7E95734D39C65BA86B4FB5E9252A72FB28713C
C4015531D1D66386A6514257458F5319B3902C2CCE257E8223B3D01456C5A3A4
BD7CCE99BFB974AFE2B0E96C9C60C25F49833136B8B574203BB1C8DAF3D5CB96
6569E95BBAA3AD1A8BBFCC66D11B7159DC377C149C72F626FD4D98DB881989B4
C8E742073D46A61FD1FCF925D922D58AB1B3FE26A38682E8CA3197ED54169455
CDD6E5ED2CC9D1CB49B585B8B21798B396AADE4F6D045C8B91AB798E643EEEA0
5AA988B786B16B98D8EAE66534529C763E4F1B1A622D39892B1BEC4A52F827B2
F2CC6741928AD65D4ECE6FBCAD6D90489F3CF77BB9EB7DF772ACC864C7B894C1
6D634A6359FF420DA62D40D6462775E7E491D24CDB6382B4BDEE56F8D6617A83
F7619314ACA274DCAFA1445D47D33F1D9D168B0F9BCC9850A80A9BF9F179A407
DCA57031A975B6CE89E564BEB8E131E17869FB61728646A4AA8BC2DC2995941C
1E175E59613A13CEA9D09684367E5DB8398120673C049B31D1576C2DE802A7E9
F46CCD6E3096628022290A06C2FFE60BC0B2C5CFFB3C82A83E1D129DBD2F6274
3C8AFDE2CFEA5D567BEFED3D988AEB14B18FA0B914891FA477E8891A17E334CE
23961261B06611CED776C57DE0497DA6D58D78B9E445AA87F790557FD547F9DD
A9E0A4EC7CB1E44BE497913DED2D75A05C2C378FA5C2796553FA8148B4FAB138
5DDBB75AD3B2C6E8F5B4BE6514CEF0E6B3369B91A0BFE1726FBE6C1440A7D894
D7891AD562EAE909E254CB79DEC64A75BE4A8CF53C3C86A398B73E3B9BF86D7E
C2EE4A92BEF0BAAA13D9910FC595A39BE817596BCDBCADDA5F47EE6E817F302C
A74AA744317A0093D9F4AB8E67C8FE5C4CE9FEF0BB2F38B4EB57B2AD92940024
4A696E44914DF76DDA70696259DE6853B7A571DC4D899AB2C6B3621740670522
60BB81F1C55289DF98223B99417A1AF9EDD4498BE27159914DE925C40F94A8B9
AC3BF548A6A05F7C9C90497411AE97552D0B7A8C24D66894CB80C19153AE9886
2648ACB5CC82B6E34C649F2E8EF95F9620F1F9EB77EEA810D03EC5B5D0B22F2C
083A8F8BE23B1DAF9F771D440FBD052A26767060106539B98BB240E6DB59A789
33C15702C00102B890D500EC2A1379BEDB27EE9C7D6C56DF6D188C0A6F00F426
9A94C700FE11EE7894FC9F65E510B71C31C299019CCEBCC35FE06B2CB47F210A
A763662CB07433BF894BD2EFC028F669C7FC994DD01BC0087B478069879F80BB
593DEE29A9225F259511DFBC9CD126429AB0EA42753CB0F13142466BB1943929
76F7B0C0E5A76B44E04B9A59AB85BEA6B30C4D25B0B1C303439C82CF3F930AA5
64067D2B93E5B8A5C1FAF1620F3569449D21DFA6A1F0270AACB9DC059016181A
969934D08437D8B6F9DEA71A73CD02EF78273A1BFAF4D3B3229B9AF5B1302D72
E63482DFD9082257D69998CE03934290BD933D57FECF867E4CB70BD6D24AAED9
37DEE70D57CB3AC85C5FE88B50CB9BFDE0ACA3F3DDDD99C56EDD87601B7AE83F
761B36AF829FE0979F237DC59484BB3FFBA9F797DC01F66C2A1515C0B85B4865
FCB07DDD99766C4EC0B9FA0BBC82D6997E7A71F02F8639D31B3BF529D253330B
0B0C9646DF6897CC766442AAF234F3FB9FBBABE6100A09C95C3A534F2AC04A7E
D345BAA035DE69A0F636915A3D63B10C291F76D3B9D8654AB338E68D277AD6B9
D4A410A97479966B8AC88FA8147F8DDB52A125793613A6AADCF4FEC8AE0A9E1B
5C2B804F68624C9EF4E3F55A7B757451775234B3C6FC2F81663FEDABD481D457
274F89A852F1606A39D39A06774459E3E4F4BA8D7FB79CAFFDCBAE2D156F6667
A1C7839C987000304505389166A82C8C9A1FA5A47E4A9361B02E4F7FF19308DF
88CA0FB3DF54CDD577130B3CF28DC3AB40B050747B8C645E0464F7B3FDF2C0B2
55D4F9492C9B9F4E560A0A467C0AB4E3D39D79B4912BAF77528BD21A730292FF
05AC015C7937D0E9AF94CD031E1ADDF87B181B0463E8AE801BFC98633C7BCBA2
4B2EBCE8E16DDC2A459F3A457825F05710F7F9146A08DBE21C79FF2FB01349C2
74BCD034B30973B049002BB64E766D9D20A45EA91F9F5C92ECE3284C230A3CB7
52E49A5FB86A4AED876F0D65E7497C541D7E5CD5928CB88BBDAD127EDC295643
83843906AD2144B2177239220745F28BF416F10D425410B287442E103B41DC1B
427AC8B1205217642D103C45FE87C41B0230488445198B240E211703DC10BD99
7E470B68EEC156445C4F64B462948BF8138A8B27F03931D809655508990A902A
EF141B8C2E4774151757D817A97A954269282E177958C987F8E51187CC2C4F74
F35A8991842A628DB65970AC0D5747F31753BFE5D8E9C475268A5B40C4D8A09B
64549124B82D79792D3355081D0774034D0EA9C5A02F1CD87EA8CD7016622170
E950755FDBD08D3EA80ED1DE5A19E3CE60934B0C3428D0586A94DB634D638BB8
CFFE5A719D28ED8C719E28C53851CD965B9AF436E13A4E39853209E7B178B593
5E3DA861475603B3035472205643D64AF3038E5B188C59BFFA199AE340A7A271
A951EDF29E228600EDBD557E10D95E1732F163DED121517442A2FCFC80083AB6
432395BC995AACFD86B4028D1614A1111436D4F0C31648547B2F7E6FF476A1A1
72BE332BB5F7C787FDECDE2095B4F4AE177F20F9D3BE0F6FD02B493FC8CAA928
55D975A2764BFA504235436DC0220C720894EF1E0A0C6567A7A2BB083D73A654
F2906A8D9E4BA2CAF851260A8F7131AF86C56A6DCC7770381272B2CF8A619E2E
0335992A4C58A0021B15B46FB0280EB03803AF58888E033C0E030C72A8650170
07C2E406D0D642D01602E50FDE00E15131C372646414AEA8799E929D8BDF81A1
CE3C85DC7CAB272390F531DF251AA41865E893788724F50A1E3334B6E06C1740
8981A9AB50600183AC5668B88170380E6F1C248B03E102075FE310C242701CC1
580CC8A003073B1620404A885C72D1884051D31B728D7A43864EA2AD3E0D41A4
DF651A93188EE83C2A3A9A361923743C43F0191D50DBDC89C930011528A92050
85850A2A5457C1C1020A5836C86803407C07AD0D0C5BC11C0E802B1CDEC54021
69FF5E8258BD545DC871EAF78676E0910B0CD2BD473E531E554C06C3A3047C93
CD9EDA795EF612CE11F14276EEEE2256CE27576A41495F707C84C40E33F7BF42
5D174865171C25F5D4E5D120F035C3601EFA82271A78C0ECCBD1CA6F5219BECB
0068049A556FA26054012D0B717FC073F084C1580C83630CB556F8DC59A9E02D
C871215C0A5AE923F10E7D3964E8E0A483FB0E2B0DDD63FF7652B59C15CFE140
7376FA021D30244C2760DE50053C011CFB6DC7CF66849C243A01B3D3099D639E
B4DF96C30D1E43773FDF5CC847C10404EC76F4D4529A0C509487D056A0C48EC1
D536A773785048CF401C2A51572BADEBF8EBD913B26F68A24CDF1BF68739A07D
94BBDFDE559F401C1016A8D6FE96A443E422C3CD960F4D1FB8870B04BB636103
C88FCC1141F5DDF74A52C0966DF7C9909BE36C858E9393989D14F45F7E3F1CF9
1E4FADCE0A5A49AD60A15C2261E6E8C366EF70A647083437E84D0BB1E3B437CE
064C513DAD6A9395FB763CDD22C6044442DD4EC80CB01DCA04293F44F879BD1A
714F92FA87832570290B3F66B2CA6632408FCE0AEFBA78215DD5D791D2D04DD4
7B563315C88BC8C5BCD35E6487DE65AD1D50A261A7616052AF66C71E4A19224F
7F0A46ED5A44C9C340C1EA7232F3E0D0D927951BAFDBB0A1A40EFED1933FCF61
BB72CFCA2AA6C9ACF89E1B075442FEA7D663ABA3ABF39622E4E3A6FF1FDAEAF7
6532943FF71510C484E184EF612810D747B963EA8EFDE03F381E3072FF2B09E6
47
:+res:PIXELFNT.CAB:
