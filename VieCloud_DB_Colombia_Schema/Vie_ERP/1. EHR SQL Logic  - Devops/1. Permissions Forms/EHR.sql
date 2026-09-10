-- Resetea los permisos para el menú '301' en la tabla de permisos de formularios.
Update segpermif  set ioptdisen = 0 , ioptnaveg = 0 , [ioptconfi] = 0 , [ioptanula] = 0,[ioptimpri] = 0,[igricrear] = 0,[igrimodif] = 0,[igrielimi] = 0 where indidmenu = '301'

-- Resetea los permisos por rol para el menú '301'.
Update SEGpermir  set ioptdisen = 0 , ioptnaveg = 0 , [ioptconfi] = 0 , [ioptanula] = 0,[ioptimpri] = 0,[igricrear] = 0,[igrimodif] = 0,[igrielimi] = 0 where indidmenu = '301'

-- Elimina configuraciones de permisos y menú existentes para el menú '934' antes de volver a insertarlas.
delete from [SEGpermif] where indidmenu = '934'
delete from [SEGmenusu] where indidmenu = '934'

-- Inserta los permisos y la ubicación en el menú para el formulario 'CONTROL CONSULTA PRIORITARIA' (934).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '934')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('934','CONTROL CONSULTA PRIORITARIA',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '934' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('934','01','2')

-- Inserta permisos y ubicación para el formulario 'FICHA NOTIFICACIÓN' (967).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '967')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('967','FICHA NOTIFICACIÓN',0,1,0,1,0,0,0,0,1,0,1,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '967' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('967','08','0')

-- Inserta permisos y ubicación para el formulario 'DESCARTAR FICHA SIVIGILA' (969).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '969')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('969','DESCARTAR FICHA SIVIGILA',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '969' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('969','08','1')

-- Crea el nuevo módulo 'GESTION DE CALIDAD'.
IF NOT EXISTS (SELECT 1 FROM dbo.segmodulu WHERE indmodulo = '20')
INSERT INTO SEGmodulu VALUES ('20','GESTION DE CALIDAD')

-- Inserta permisos y ubicación para el formulario 'NIVELES DE DAÑO' (970).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '970')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('970','NIVELES DE DAÑO',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '970' AND indmodulo = '20')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('970','20','1')

-- Inserta permisos y ubicación para el formulario 'REPORTE DE LAS ACCIONES' (971).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '971')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('971','REPORTE DE LAS ACCIONES',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '971' AND indmodulo = '20')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('971','20','2')

-- Inserta permisos y ubicación para el formulario 'TIPO Y CLASE DE LAS ACCIONES' (972).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '972')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('972','TIPO Y CLASE DE LAS ACCIONES',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '972' AND indmodulo = '20')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('972','20','1')

-- Inserta un permiso especial para 'HABILITAR DISPONIBILIDAD PROFESIONAL PARA CIRUGIAS' (973).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '973')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('973','PERMISO HABILITAR DISPONIBILIAD PROFESIONAL PARA CIRUGIAS',1,0,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '973' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('973','09','0')

-- Inserta permisos y ubicación para el formulario 'MOTIVOS ANULACIÓN' (974).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '974')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('974','MOTIVOS ANULACIÓN',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '974' AND indmodulo = '20')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('974','20','1')

-- Inserta permisos y ubicación para el formulario 'COMITÉS-AREAS' (975).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '975')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('975','COMITÉS-AREAS',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '975' AND indmodulo = '20')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('975','20','1')

-- Inserta permisos y ubicación para el formulario de 'TECNOVIGILANCIA' (976).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '976')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('976','TECNOVIGILANCIA',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '976' AND indmodulo = '20')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('976','20','0')

-- Inserta permisos y ubicación para el formulario de 'RADICACION CIRUGIA' (978).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '978')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('978','RADICACION CIRUGIA',1,1,0,1,0,0,1,1,1,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '978' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('978','01','2')

-- Inserta permisos y ubicación para el formulario de 'FARMACOVIGILANCIA' (977).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '977')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('977','FARMACOVIGILANCIA',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '977' AND indmodulo = '20')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('977','20','0')

-- Inserta permisos y ubicación para el formulario 'IAAS QX' (979).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '979')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('979','IAAS QX',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '979' AND indmodulo = '20')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('979','20','0')

-- Inserta permisos y ubicación para el formulario 'IAAS-DISPOSITIVOS' (980).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '980')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('980','IAAS-DISPOSITIVOS',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '980' AND indmodulo = '20')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('980','20','0')

-- Inserta permisos y ubicación para el 'DASHBOARD DE SEGUIMIENTO CALIDAD' (981).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '981')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('981','DASHBOARD DE SEGUIMIENTO CALIDAD',0,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '981' AND indmodulo = '20')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('981','20','2')

-- Inserta permisos y ubicación para el formulario 'REACTIVOVIGILANCIA' (982).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '982')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('982','REACTIVOVIGILANCIA',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '982' AND indmodulo = '20')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('982','20','0')

-- Inserta permisos y ubicación para el formulario 'HISTÓRICO DE LAS ACCIONES' (983).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '983')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('983','HISTÓRICO DE LAS ACCIONES',0,1,0,0,0,0,0,0,1,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '983' AND indmodulo = '20')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('983','20','0')

-- Inserta permisos y ubicación para el formulario de 'HEMOVIGILANCIA' (984).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '984')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('984','HEMOVIGILANCIA',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '984' AND indmodulo = '20')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('984','20','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA TISS-28' (985).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '985')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('985','ESCALA TISS-28',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '985' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('985','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA BRADEN' (986).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '986')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('986','ESCALA BRADEN',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '986' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('986','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA APACHE II' (987).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '987')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('987','ESCALA APACHE II',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '987' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('987','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA KARNOFKY' (988).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '988')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('988','ESCALA KARNOFKY',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '988' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('988','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA DE ECOG' (989).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '989')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('989','ESCALA DE ECOG',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '989' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('989','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA NEMS' (990).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '990')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('990','ESCALA NEMS',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '990' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('990','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA GLASGOW' (991).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '991')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('991','ESCALA GLASGOW',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '991' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('991','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA SOFA' (992).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '992')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('992','ESCALA SOFA',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '992' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('992','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA CHARLSON' (993).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '993')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('993','ESCALA CHARLSON',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '993' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('993','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA SAPS 3' (994).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '994')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('994','ESCALA SAPS 3',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '994' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('994','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA BARTHEL' (995).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '995')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('995','ESCALA BARTHEL',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '995' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('995','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA MORSE' (996).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '996')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('996','ESCALA MORSE',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '996' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('996','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA MACDEMS' (997).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '997')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('997','ESCALA MACDEMS',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '997' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('997','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA NSRAS' (998).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '998')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('998','ESCALA NSRAS',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '998' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('998','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA MST' (999).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '999')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('999','ESCALA MST',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '999' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('999','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA SAD PERSONS' (401).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '401')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('401','ESCALA SAD PERSONS',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '401' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('401','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA BECK' (402).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '402')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('402','ESCALA BECK',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '402' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('402','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA ZARIT' (403).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '403')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('403','ESCALA ZARIT',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '403' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('403','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA RQC' (404).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '404')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('404','ESCALA RQC',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '404' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('404','08','0')

-- Inserta permisos y ubicación para 'GESTION AGENDA PROFESIONALES x QUIROFANOS' (968).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '968')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('968','GESTION AGENDA PROFESIONALES x QUIROFANOS',0,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '968' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('968','09','2')

-- Inserta un permiso especial (duplicado) para 'HABILITAR DISPONIBILIDAD PROFESIONAL PARA CIRUGIAS' (973).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '973')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('973','PERMISO HABILITAR DISPONIBILIAD PROFESIONAL PARA CIRUGIAS',1,0,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '973' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('973','09','0')

-- Inserta permisos y ubicación para el maestro 'PAQUETES QUIRURGICOS' (405).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '405')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('405','PAQUETES QUIRURGICOS',1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '405' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('405','09','1')

-- Inserta permiso para 'CONFIRMAR Y SOLICITAR PAQUETES QUIRURGICOS' (406).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '406')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('406','PERMISO CONFIRMAR Y SOLICITAR PAQUETES QUIRURGICOS',1,0,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '406' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('406','09','0')

-- Inserta permiso para 'MARCAR PACIENTE SALA ESPERA' (407).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '407')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('407','PERMISO MARCAR PACIENTE SALA ESPERA',1,0,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '407' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('407','09','0')

-- Inserta permiso para 'MARCAR PACIENTE SALA QUIRURGUICA' (408).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '408')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('408','PERMISO MARCAR PACIENTE SALA QUIRURGUICA',1,0,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '408' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('408','09','0')

-- Inserta permiso para 'MARCAR PACIENTE SALA RECUPERACION' (409).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '409')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('409','PERMISO MARCAR PACIENTE SALA RECUPERACION',1,0,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '409' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('409','09','0')

-- Inserta permiso para 'MARCAR PACIENTE CON ALTA' (410).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '410')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('410','PERMISO MARCAR PACIENTE CON ALTA',1,0,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '410' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('410','09','0')

-- Inserta permisos para el reporte 'REPORTE PROGRAMACION QX' (400).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '400')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('400','REPORTE PROGRAMACION QX',0,1,0,0,0,0,0,0,1,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '400' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('400','09','4')

-- Inserta permisos (duplicado) para 'PAQUETES QUIRURGICOS' (405).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '405')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('405','PAQUETES QUIRURGICOS',1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '405' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('405','09','1')

-- Renombra varios formularios del módulo de agendamiento para mayor claridad.
update segpermif set indmendes = 'ASIGNACION DE CITAS TRATAMIENTOS ESPECIALES' where indidmenu = 851
update segpermif set indmendes = 'DISPONIBILIDAD SALA RENAL' where indidmenu = 923
update segpermif set indmendes = 'ASIGNACION DE CITAS CONSULTA EXTERNA' where indidmenu = 206
update segpermif set indmendes = 'ASIGNACION DE CITAS APOYO DX' where indidmenu = 830
update segpermif set indmendes = 'ASIGNACION DE CITAS DIÁLISIS' where indidmenu = 922
update SEGpermif set indmendes = 'DISPONIBILIDAD QUIROFANOS' where indidmenu = 329
update segpermif set indmendes = 'GESTION AGENDA PROFESIONALES x QUIROFANOS' where indidmenu = 968
update segpermif set indmendes = 'GESTION AGENDA PROFESIONALES x CONSULTORIOS' where indidmenu = 320
update SEGpermif set indmendes = 'RIAS' where  indidmenu = '957'

-- Inserta permisos y ubicación para el maestro 'GRUPO NOTAS ADMINISTRATIVAS' (412).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '412')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('412','GRUPO NOTAS ADMINISTRATIVAS',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '412' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('412','08','0')

-- Inserta permisos y ubicación para el maestro 'VARIABLES NOTA ADMINISTRATIVAS' (413).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '413')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('413','VARIABLES NOTA ADMINISTRATIVAS',1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '413' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('413','08','0')

-- Inserta permisos y ubicación para el formulario de parametrización 'PARAMETRIZAR NOTAS ADMINISTRATIVAS' (414).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '414')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('414','PARAMETRIZAR NOTAS ADMINISTRATIVAS',1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '414' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('414','08','3')

-- Actualiza la ubicación en el menú para varios formularios existentes.
update SEGmenusu set [indopcion] = 0 where indidmenu = '959'
update SEGmenusu set [indopcion] = 0 where indidmenu = '960'
update SEGmenusu set [indopcion] = 0 where indidmenu = '961'
update SEGmenusu set [indopcion] = 0 where indidmenu = '953'
update SEGmenusu set [indopcion] = 0 where indidmenu = '955'
update SEGmenusu set [indopcion] = 0 where indidmenu = '954'

-- Inserta permisos y ubicación para el formulario de 'ESCALA INDICE DE PLACA BACTERIANA' (415).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '415')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('415','ESCALA INDICE DE PLACA BACTERIANA',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '415' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('415','08','0')

-- Inserta permisos y ubicación para el formulario 'NOTAS ADMINISTRATIVAS' (416).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '416')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('416','NOTAS ADMINISTRATIVAS',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '416' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('416','08','2')

-- Inserta permisos y ubicación para el maestro 'MOTIVOS NO CUMPLIMENTO TRATAMIENTO ODONTOLOGICO' (417).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '417')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('417','MOTIVOS NO CUMPLIMENTO TRATAMIENTO ODONTOLOGICO',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '417' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('417','08','1')

-- Inserta permisos y ubicación para el formulario 'DISPONIBILIDAD SALA APOYO DX' (925).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '925')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('925','DISPONIBILIDAD SALA APOYO DX',1,1,0,1,1,0,0,1,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '925' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('925','09','2')

-- Inserta permisos y ubicación para el formulario 'PARAMETRIZAR ESCALAS' (455).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '455')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('455','PARAMETRIZAR ESCALAS',1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '455' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('455','08','3')

-- Inserta permisos y ubicación para el maestro 'COMUNAS' (456).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '456')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('456','COMUNAS',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '456' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('456','01','1')

-- Habilita el permiso de eliminación para el formulario de Disponibilidad de Sala de Apoyo DX.
update SEGpermif set ioptelimi = 1 where indidmenu = '925'

-- Inserta permisos y ubicación para el formulario 'MAPA QUIRURGICO' (966).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '966')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('966','MAPA QUIRURGICO',0,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '966' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('966','09','2')

-- Inserta permisos y ubicación para el formulario de 'ESCALA M-CHAT MODIFICADO' (418).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '418')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('418','ESCALA M-CHAT MODIFICADO',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '418' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('418','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA WHOOLEY' (419).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '419')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('419','ESCALA WHOOLEY',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '419' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('419','08','0')

-- Inserta permisos y ubicación para el formulario de 'ESCALA SRQ' (420).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '420')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('420','ESCALA SRQ',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '420' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('420','08','0')

-- Desactiva el control automático de autorizaciones en parámetros generales.
update AGPARAMET set PRIMECONTROLAUTOMATICO =  0

-- Inserta permisos y ubicación para el maestro 'GRUPOS DE IMAGENOLOGIA' (457).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '457')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('457','GRUPOS DE IMAGENOLOGIA',1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '457' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('457','08','1')

-- Inserta permisos y ubicación para el formulario 'GESTION RIAS' (458).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '458')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('458','GESTION RIAS',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '458' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('458','08','3')

-- Inserta permisos y ubicación para el formulario 'ESCALA DE AUDIT - ALCOHOLISMO' (425).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '425')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('425','ESCALA DE AUDIT - ALCOHOLISMO',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '425' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('425','08','0')

-- Inserta permisos y ubicación para el formulario 'ESCALA DE FRAGILIDAD DE LINDA FRIED' (426).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '426')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('426','ESCALA DE FRAGILIDAD DE LINDA FRIED',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '426' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('426','08','0')

-- Inserta permisos y ubicación para el formulario 'ESCALA DE AUTONOMÍA DE LAWTON-BRODY' (427).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '427')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('427','ESCALA DE AUTONOMÍA DE LAWTON-BRODY',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '427' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('427','08','0')

-- Inserta permisos y ubicación para el formulario 'ESCALA PARA TRASTORNO DE ANSIEDAD GENERALIZADO (GAD)' (428).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '428')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('428','ESCALA PARA TRASTORNO DE ANSIEDAD GENERALIZADO (GAD)',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '428' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('428','08','0')

-- Inserta permisos y ubicación para el formulario 'ESCALA MNA SIMPLIFICADA' (429).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '429')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('429','ESCALA MNA SIMPLIFICADA (MINI NUTRITIONAL ASSESSMENT)',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '429' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('429','08','0')

-- Inserta permisos y ubicación para el formulario 'ESCALA MNA' (430).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '430')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('430','ESCALA MNA',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '430' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('430','08','0')

-- Inserta permisos y ubicación para el formulario 'VALIDACION DE DERECHOS' (459).
 IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '459')
 Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('459','VALIDACION DE DERECHOS',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '459' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('459','01','3')

-- Inserta permisos y ubicación para el formulario 'ESTADO CAMAS' (469).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '469')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('469','ESTADO CAMAS',0,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '469' AND indmodulo = '05')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('469','05','4')

-- Inserta permisos y ubicación para el 'DASHBOARD LIMPIEZA Y DESINFECCIÓN' (470).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '470')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('470','DASHBOARD LIMPIEZA Y DESINFECCIÓN',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '470' AND indmodulo = '05')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('470','05','2')

-- Inserta permisos y ubicación para el formulario 'GESTIÓN LIMPIEZA Y DESINFECCIÓN' (472).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '472')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('472','GESTIÓN LIMPIEZA Y DESINFECCIÓN',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '472' AND indmodulo = '05')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('472','05','4')

-- Cambia la ubicación en el menú del formulario de Notas Administrativas.
update  SEGmenusu set indopcion = 2  where indidmenu= '416' 

-- Agrega la opción de imprimir a varios formularios del módulo de calidad.
update SEGpermif set ioptimpri = 1 where indidmenu = '977'
update SEGpermif set ioptimpri = 1 where indidmenu = '984'
update SEGpermif set ioptimpri = 1 where indidmenu = '980'
update SEGpermif set ioptimpri = 1 where indidmenu = '979'
update SEGpermif set ioptimpri = 1 where indidmenu = '982'
update SEGpermif set ioptimpri = 1 where indidmenu = '976'
update SEGpermif set ioptimpri = 1 where indidmenu = '983'

-- Inserta permisos y ubicación para el formulario 'ESCALA NEW (NATIONAL EARLY WARNING SCORE)' (431).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '431')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('431','ESCALA NEW (NATIONAL EARLY WARNING SCORE)',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '431' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('431','08','0')

-- Inserta permisos y ubicación para el formulario 'PERFIL FARMACOTERAPEÚTICO' (460).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '460')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('460','PERFIL FARMACOTERAPEÚTICO',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '460' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('460','08','0')

-- Inserta permisos y ubicación para el formulario 'PERFIL FARMACOTERAPEÚTICO ONCOLÓGICO' (461).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '461')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('461','PERFIL FARMACOTERAPEÚTICO ONCOLÓGICO)',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '461' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('461','08','0')

-- Inserta permisos y ubicación para el maestro 'TIPOS DE AISLAMIENTOS' (462).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '462')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('462','TIPOS DE AISLAMIENTOS',1,1,0,1,1,0,0,0,1,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '462' AND indmodulo = '05')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('462','05','1')

-- Inserta permisos y ubicación para el maestro 'IDIOMAS' (302).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '302')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('302','IDIOMAS',1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '302' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('302','01','1')

-- Inserta permisos y ubicación para el maestro 'CREENCIAS' (300).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '300')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('300','CREENCIAS',1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '300' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('300','01','1')

-- Inserta permisos y ubicación para el formulario 'CONFIRMACION DE CITAS' (232).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '232')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('232','CONFIRMACION DE CITAS',0,0,0,1,0,0,1,1,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '232' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('232','09','2')

-- Inserta permisos y ubicación para el 'REPORTE RECEPCIÓN REFERENCIAS' (106).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '106')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('106','REPORTE RECEPCIÓN REFERENCIAS',0,1,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '106' AND indmodulo = '19')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('106','19','4')

-- Inserta permisos y ubicación para el formulario 'CERTIFICADOS ASISTENCIALES' (471).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '471')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('471','CERTIFICADOS ASISTENCIALES',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '471' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('471','08','2')

-- Inserta permisos y ubicación para el formulario 'CONSULTA DE CITAS' (182).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '182')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('182','CONSULTA DE CITAS',0,1,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '182' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('182','09','4')

-- Inserta permisos y ubicación para el formulario 'CARGUE MASIVO DE DOCUMENTOS' (246).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '246')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('246','CARGUE MASIVO DE DOCUMENTOS',0,1,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '246' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('246','01','3')

-- Inserta permisos y ubicación para el 'DASHBOARD SOLICITUD TRASLADOS' (248).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '248')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('248','DASHBOARD SOLICITUD TRASLADOS',0,1,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '248' AND indmodulo = '19')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('248','19','2')

-- Inserta permisos y ubicación para el 'DASHBOARD ATENCION DOMICILIARIA' (853).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '853')
INSERT INTO SEGpermif( [indidmenu] , [indmendes] , [ioptguard] , [ioptconsu] , [ioptdisen] , [ioptactua] , [ioptelimi] , [ioptnaveg] , [ioptconfi] , [ioptanula] , [ioptimpri] , [igricrear] , [igrimodif] , [igrielimi] )
VALUES('853','DASHBOARD ATENCION DOMICILIARIA',0,0,0,0,0,0,0,0,0,0,0,0)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '853' AND indmodulo = '08')
INSERT INTO SEGmenusu(indidmenu,indmodulo,indopcion)
VALUES ('853','08',2)

-- Inserta permisos y ubicación para el formulario 'REGISTRO EGRESO PAD' (005).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '005')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('005','REGISTRO EGRESO PAD',1,1,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '005' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('005','08','0')

-- Inserta permisos y ubicación para el 'INFORME ATENCION DOMICILIARIA' (854).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '854')
INSERT INTO SEGpermif( [indidmenu] , [indmendes] , [ioptguard] , [ioptconsu] , [ioptdisen] , [ioptactua] , [ioptelimi] , [ioptnaveg] , [ioptconfi] , [ioptanula] , [ioptimpri] , [igricrear] , [igrimodif] , [igrielimi] )
VALUES('854','INFORME ATENCION DOMICILIARIA',0,0,0,0,0,0,0,0,0,0,0,0)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '854' AND indmodulo = '08')
INSERT INTO SEGmenusu(indidmenu,indmodulo,indopcion)
VALUES ('854','08',4)

-- Inserta permisos y ubicación para el formulario 'CONSULTAR MIPRES' (852).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '852')
INSERT INTO SEGpermif( [indidmenu] , [indmendes] , [ioptguard] , [ioptconsu] , [ioptdisen] , [ioptactua] , [ioptelimi] , [ioptnaveg] , [ioptconfi] , [ioptanula] , [ioptimpri] , [igricrear] , [igrimodif] , [igrielimi] )
VALUES('852','CONSULTAR MIPRES',0,0,0,0,0,0,0,0,0,0,0,0)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '852' AND indmodulo = '08')
INSERT INTO SEGmenusu(indidmenu,indmodulo,indopcion)
VALUES ('852','08',3)

-- Inserta permisos y ubicación para 'PARACLINICOS PERMITEN EGRESO SIN RESULTADO' (843).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '843')
INSERT INTO SEGpermif( [indidmenu] , [indmendes] , [ioptguard] , [ioptconsu] , [ioptdisen] , [ioptactua] , [ioptelimi] , [ioptnaveg] , [ioptconfi] , [ioptanula] , [ioptimpri] , [igricrear] , [igrimodif] , [igrielimi] )
VALUES('843','PARACLINICOS PERMITEN EGRESO SIN RESULTADO',1,1,0,1,0,0,0,0,0,1,1,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '843' AND indmodulo = '08')
INSERT INTO SEGmenusu(indidmenu,indmodulo,indopcion)
VALUES ('843','08',3)

-- Crea el módulo 'INFORMES ESPECIALIZADOS' e inserta permisos y ubicación para 'REPORTES PERSONALIZADOS' (249).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '249')
INSERT INTO SEGpermif( [indidmenu] , [indmendes] , [ioptguard] , [ioptconsu] , [ioptdisen] , [ioptactua] , [ioptelimi] , [ioptnaveg] , [ioptconfi] , [ioptanula] , [ioptimpri] , [igricrear] , [igrimodif] , [igrielimi] )
VALUES('249','REPORTES PERSONALIZADOS',1,1,0,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '249' AND indmodulo = '21')
INSERT INTO SEGmenusu(indidmenu,indmodulo,indopcion)
VALUES ('249','21',2)
IF NOT EXISTS (SELECT 1 FROM dbo.segmodulu WHERE indmodulo = '21')
INSERT INTO SEGmodulu(indmodulo,indmoddes)values(21,'INFORMES ESPECIALIZADOS')

-- Inserta permisos y ubicación para 'PARAMETRIZACIÓN CONTROL DE ANTECEDENTES' (896).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '896')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('896','PARAMETRIZACIÓN CONTROL DE ANTECEDENTES',1,1,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '896' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('896','08','3')

-- Inserta permisos y ubicación para el maestro 'DIAGNOSTICOS DE ENFERMERIA' (855).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '855')
INSERT INTO SEGpermif( [indidmenu] , [indmendes] , [ioptguard] , [ioptconsu] , [ioptdisen] , [ioptactua] , [ioptelimi] , [ioptnaveg] , [ioptconfi] , [ioptanula] , [ioptimpri] , [igricrear] , [igrimodif] , [igrielimi] )
VALUES('855','DIAGNOSTICOS DE ENFERMERIA',1,1,0,1,0,0,0,0,0,0,0,0)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '855' AND indmodulo = '08')
INSERT INTO SEGmenusu(indidmenu,indmodulo,indopcion)
VALUES ('855','08',1)

-- Inserta permisos y ubicación para 'PLANES CUIDADOS DE ENFERMERIA' (856).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '856')
INSERT INTO SEGpermif( [indidmenu] , [indmendes] , [ioptguard] , [ioptconsu] , [ioptdisen] , [ioptactua] , [ioptelimi] , [ioptnaveg] , [ioptconfi] , [ioptanula] , [ioptimpri] , [igricrear] , [igrimodif] , [igrielimi] , [integracion])
VALUES('856','PLANES CUIDADOS DE ENFERMERIA',1,1,1,1,0,0,0,0,0,1,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '856' AND indmodulo = '08')
INSERT INTO SEGmenusu(indidmenu,indmodulo,indopcion)
VALUES ('856','08',3)

-- Inserta permisos y ubicación para el 'REPORTE REMISIONES' (250).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '250')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('250','REPORTE REMISIONES',0,1,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '250' AND indmodulo = '19')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('250','19','4')

-- Inserta permisos y ubicación para el 'REPORTE INTERCONSULTA' (251).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '251')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('251','REPORTE INTERCONSULTA',0,1,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '251' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('251','08','4')

-- Inserta permisos y ubicación para 'DISPONIBILIDAD EQUIPOS ESPECIALES' (858).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '858')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('858','DISPONIBILIDAD EQUIPOS ESPECIALES',1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '858' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('858','09','2')

-- Inserta permisos y ubicación para el maestro 'GRUPOS GESTION PROCEDIMIENTOS INVASIVOS' (252).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '252')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('252','GRUPOS GESTION PROCEDIMIENTOS INVASIVOS',1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '252' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('252','08','1')

-- Inserta permisos y ubicación para 'PAQUETE DE ÓRDENES' (253).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '253')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('253','PAQUETE DE ÓRDENES',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '253' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('253','08','3')

-- Inserta permisos para la aplicación de ambulancias: 'REGISTRO DE TIEMPOS' (255) y 'REGISTRO DE PRODUCTOS' (256).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '255')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('255','REGISTRO DE TIEMPOS - AMBULANCIAS APP',1,0,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '255' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('255','08','0')

IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '256')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('256','REGISTRO DE PRODUCTOS - AMBULANCIAS APP',1,0,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '256' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('256','08','0')

-- Inserta permisos y ubicación para 'CONFIGURACION TABLAS PARACLINICOS' (257).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '257')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('257','CONFIGURACION TABLAS PARACLINICOS',1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '257' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('257','08','3')

-- Inserta permisos y ubicación para 'CREAR TABLA PARACLINICOS' (258).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '258')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('258','CREAR TABLA PARACLINICOS',1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '258' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('258','08','3')

-- Inserta permisos y ubicación para 'HOMOLOGACIONES MIPRES' (859).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '859')
INSERT INTO SEGpermif( [indidmenu] , [indmendes] , [ioptguard] , [ioptconsu] , [ioptdisen] , [ioptactua] , [ioptelimi] , [ioptnaveg] , [ioptconfi] , [ioptanula] , [ioptimpri] , [igricrear] , [igrimodif] , [igrielimi] )
VALUES('859','HOMOLOGACIONES MIPRES',1,1,1,1,0,0,0,0,0,1,1,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '859' AND indmodulo = '08')
INSERT INTO SEGmenusu(indidmenu,indmodulo,indopcion)
VALUES ('859','08',1)

-- Actualiza los permisos CRUD básicos para 'GESTION AGENDA PROFESIONALES x QUIROFANOS' (968).
UPDATE SEGpermif 
SET ioptguard = 1, ioptconsu = 1, ioptdisen = 0, ioptactua = 1, ioptelimi = 1, ioptnaveg = 0, ioptconfi = 0, ioptanula = 0, ioptimpri = 0, igricrear = 0, igrimodif = 0, igrielimi = 0, integracion = 1
WHERE indidmenu = '968'

-- Inserta permisos y ubicación para 'AUTORIZACION MULTIPLE RELACION CODIGO MIPRES' (860).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '860')
INSERT INTO SEGpermif( [indidmenu] , [indmendes] , [ioptguard] , [ioptconsu] , [ioptdisen] , [ioptactua] , [ioptelimi] , [ioptnaveg] , [ioptconfi] , [ioptanula] , [ioptimpri] , [igricrear] , [igrimodif] , [igrielimi] )
VALUES('860','AUTORIZACION MULTIPLE RELACION CODIGO MIPRES',1,1,0,1,0,0,0,0,0,1,1,0)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '860' AND indmodulo = '08')
INSERT INTO SEGmenusu(indidmenu,indmodulo,indopcion)
VALUES ('860','08',3)

-- Elimina formularios que ya no se utilizan.
delete from SEGmenusu where indidmenu = 803
delete from SEGpermiu where indidmenu = 803
delete from SEGpermir  where indidmenu = 803
delete from SEGmenusu where indidmenu = 805
delete from SEGpermiu where indidmenu = 805
delete from SEGpermir  where indidmenu = 805
delete from SEGmenusu where indidmenu = 108
delete from SEGpermiu where indidmenu = 108
delete from SEGpermir  where indidmenu = 108

-- Renombra el formulario 'DASHBOARD CAC'.
update SEGpermif set indmendes = 'DASHBOARD CAC' where indidmenu = 326

-- Inserta permisos y ubicación para 'DASHBOARD RADIOTERAPIA' (861) y 'DASHBOARD QUIMIOTERAPIA' (862).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '861')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('861','DASHBOARD RADIOTERAPIA',1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '861' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('861','08','2')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '862')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('862','DASHBOARD QUIMIOTERAPIA',1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '862' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('862','08','2')

-- Renombra el formulario de 'ESQUEMAS'.
update SEGpermif set indmendes = 'ESQUEMAS' where indidmenu = '965'

-- Crea varios formularios de escalas clínicas (KILLIP, NYHA, Lawton-Brody, GAD, MNA, NEWS, CRUSADE, etc.) y les asigna permisos.
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '421')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('421','ESCALA KILLIP',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '421' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('421','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '426')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('426','ESCALA NYHA',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '426' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('426','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '427')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('427','ESCALA Lawton - Brody',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '427' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('427','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '428')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('428','ESCALA GAD',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '428' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('428','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '429')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('429','ESCALA MNA',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '429' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('429','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '430')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('430','ESCALA MNA Simplificada',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '430' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('430','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '431')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('431','ESCALA News',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '431' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('431','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '432')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('432','ESCALA CRUSADE',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '432' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('432','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '433')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('433','ESCALA CHA2DS2-VASc',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '433' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('433','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '434')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('434','ESCALA PADUA',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '434' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('434','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '435')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('435','ESCALA CAPRINI',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '435' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('435','08','0')

-- Agrega permiso de consulta al 'Informe de Atención Domiciliaria'.
UPDATE SEGpermif SET ioptconsu = 1 WHERE indidmenu = '854'

-- Renombra formularios y ajusta permisos relacionados con autorizaciones intrahospitalarias.
update SEGpermif set indmendes = 'DASHBOARD AUTORIZACION INTRAHOSPITALARIAS' WHERE indidmenu = '311'
update SEGpermif set indmendes = 'CONFIGURAR SERVICIOS SUSCEPTIBLES INTRAHOSPITALARIOS',igrimodif = 0,igrielimi = 0,ioptguard = 0 WHERE indidmenu = '312'
update SEGpermiu   set igrielimi = 0, igrimodif = 0, ioptguard  = 0,ioptactua = 0 where indidmenu = '312'
update SEGpermir   set igrielimi = 0, igrimodif = 0,ioptguard = 0,ioptactua = 0 where indidmenu = '312'

-- Inserta permisos y ubicación para el maestro de 'MAESTRO DE RADIOTERAPIA' (863).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '863')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('863','MAESTRO DE RADIOTERAPIA',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '863' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('863','08','1')

-- Inserta permisos y ubicación para el 'DASHBOARD BRAQUITERAPIA' (864).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '864')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('864','DASHBOARD BRAQUITERAPIA',1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '864' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('864','08','2')

-- Inserta permisos y ubicación para 'ESCALA MUST' (436), 'ESCALA STRONG KIDS' (437), y 'ESCALA VALORACIÓN GLOBAL SUBJETIVA' (438).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '436')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('436','ESCALA MUST',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '436' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('436','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '437')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('437','ESCALA STRONG KIDS',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '437' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('437','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '438')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('438','ESCALA VALORACIÓN GLOBAL SUBJETIVA DEL ESTADO NUTRICIONAL',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '438' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('438','08','0')

-- Agrega permiso de impresión a los formularios 'Administrar Reservas' y 'Disponibilidad de Camas'.
update SEGpermif set ioptimpri = 1 where indidmenu = '064'
update SEGpermif set ioptimpri = 1 where indidmenu = '070'

-- Inserta el permiso para 'DESMARCAR INGRESO DE TIPO TRATAMIENTO ESPECIAL' (865).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '865')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('865','DESMARCAR INGRESO DE TIPO TRATAMIENTO ESPECIAL',1,0,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '865' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('865','01','0')

-- Agrega permiso de impresión al formulario de Trazabilidad.
update SEGpermif set ioptimpri = 1 where indidmenu = '088'

-- Inserta permisos y ubicación para 'ESCALA TIMI CEST' (439), 'ESCALA WELLS TVP' (440), 'ESCALA WELLS TEP' (441), 'ESCALA NPC' (442), 'ESCALA GRACE' (443), y 'ESCALA TIMI SEST' (444).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '439')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('439','ESCALA TIMI CEST',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '439' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('439','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '440')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('440','ESCALA WELLS TVP',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '440' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('440','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '441')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('441','ESCALA WELLS TEP',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '441' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('441','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '442')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('442','ESCALA NPC',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '442' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('442','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '443')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('443','ESCALA GRACE',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '443' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('443','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '444')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('444','ESCALA TIMI SEST',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '444' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('444','08','0')

-- Quita el permiso de modificar de un formulario de seguridad.
update SEGpermir set igrimodif = 0  where indidmenu = '119'
update SEGpermiu set igrimodif = 0  where indidmenu = '119'
update SEGpermif set igrimodif = 0  where indidmenu = '119'

-- Inserta permisos especiales de flujo de trabajo para Cirugías, Quimioterapia y Citas de Apoyo DX.
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '468')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('468','PERMISO HABILITAR CANCELAR CIRUGIA QUE YA PASARON',1,0,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '468' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('468','09','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '330')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('330','PERMISO ANULAR ORDEN DE QUIMIOTERAPIA',1,0,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '330' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('330','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '331')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('331','PERMISO FINALIZAR PREMATURO ORDEN DE QUIMIOTERAPIA',1,0,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '331' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('331','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '332')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('332','PERMISO CREAR INGRESOS DESDE CONFIRMAR CITA APOYO DX',1,0,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '332' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('332','09','0')

-- Inserta permisos para el maestro 'CUPS OTROS PROCEDIMIENTOS X CENTRO ATENCION'.
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '333')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('333','CUPS OTROS PROCEDIMIENTOS X CENTRO ATENCION' ,1,1,0,1,1,0,0,0,0,0,0,1,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '333' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('333','08','3')

-- Inserta permisos para la funcionalidad 'GENERAR SOPORTES CAC'.
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '334')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('334','GENERAR SOPORTES CAC' ,0,1,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '334' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('334','08','2')

-- Habilita el permiso de impresión para un gran número de escalas clínicas.
UPDATE SEGpermir SET ioptimpri = 1  WHERE indidmenu = '989'
UPDATE SEGpermif set ioptimpri = 1  where indidmenu = '990'
update SEGpermif set ioptimpri = 1  where indidmenu = '992'
update SEGpermif set ioptimpri = 1  where indidmenu = '994'
update SEGpermif set ioptimpri = 1  where indidmenu = '995'
update SEGpermif set ioptimpri = 1 where indidmenu = '993'
update SEGpermif set ioptimpri = 1 where indidmenu = '444'
update SEGpermif set ioptimpri = 1 where indidmenu = '443'
update SEGpermif set ioptimpri = 1 where indidmenu = '442'
update SEGpermif set ioptimpri = 1 where indidmenu = '441'
update SEGpermif set ioptimpri = 1 where indidmenu = '440'
update SEGpermif set ioptimpri = 1 where indidmenu = '439'
update SEGpermif set ioptimpri = 1 where indidmenu = '438'
UPDATE SEGpermif SET ioptimpri = 1  WHERE indidmenu = '996'
UPDATE SEGpermif SET ioptimpri = 1  WHERE indidmenu = '997'
UPDATE SEGpermif SET ioptimpri = 1 WHERE indidmenu = '437'
UPDATE SEGpermif SET ioptimpri = 1  WHERE indidmenu = '998'
update SEGpermif set ioptimpri = 1  where indidmenu = '999'
update SEGpermif set ioptimpri = 1  where indidmenu = '401'
update SEGpermif set ioptimpri = 1 where indidmenu = '436'
update SEGpermif set ioptimpri = 1 where indidmenu = '991'
update SEGpermif set ioptimpri = 1 where indidmenu = '402'
update SEGpermif set ioptimpri = 1 where indidmenu = '403'
UPDATE SEGpermif SET ioptimpri = 1 WHERE indidmenu = '404'
UPDATE SEGpermif SET ioptimpri = 1 WHERE indidmenu = '425'
UPDATE SEGpermif set ioptimpri = 1 where indidmenu = '426'
UPDATE SEGpermif SET ioptimpri = 1 WHERE indidmenu = '427'
UPDATE SEGpermif set ioptimpri = 1 where indidmenu = '428'
UPDATE SEGpermif SET ioptimpri = 1 WHERE indidmenu = '430'
UPDATE SEGpermif set ioptimpri = 1 where indidmenu = '429'
UPDATE SEGpermif set ioptimpri = 1 where indidmenu = '431'
UPDATE SEGpermif set ioptimpri = 1 where indidmenu = '419'
UPDATE SEGpermif set ioptimpri = 1 where indidmenu = '418'
UPDATE SEGpermif SET ioptimpri = 1 WHERE indidmenu = '415'
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '445')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('445','ESCALA ASSIST',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '445' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('445','08','0')
UPDATE SEGpermif set ioptimpri = 1 where indidmenu = '445'
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '446')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('446','ESCALA VALE',1,1,0,1,0,0,0,0,1,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '446' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('446','08','0')
UPDATE SEGpermif set ioptimpri = 1 where indidmenu = '991'

-- Inserta permisos para el formulario 'CONTROL CITAS CONSULTA EXTERNA'.
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '448')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('448','CONTROL CITAS CONSULTA EXTERNA',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '448' AND indmodulo = '01')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('448','01','2')

-- Agrega una columna a la tabla de control de consulta externa.
ALTER TABLE dbo.ADCONCOEX ADD FECREGISTROSALA DATETIME NULL

-- Inserta permisos y ubicación para 'ESCALA ANTHONISEN', 'ESCALA DAS-28', 'ESCALA MRS', 'ESCALA HAQ', 'ESCALA ASPECT CT', 'ESCALA ABREVIADA DEL DESARROLLO V3', 'ESCALA INDICE O LEARY' y 'ESCALA NIHSS'.
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '447')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('447','ESCALA ANTHONISEN',1,1,0,1,0,0,0,0,1,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '447' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('447','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '417')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('417','ESCALA DAS-28',1,1,0,1,0,0,0,0,1,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '417' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('417','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '449')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('449','ESCALA MRS',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '449' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('449','08','0')
UPDATE SEGpermif set ioptimpri = 1 where indidmenu = '449'
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '254')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('254','ESCALA HAQ',1,1,0,1,0,0,0,0,1,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '254' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('254','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '450')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('450','ESCALA ASPECT CT',1,1,0,1,0,0,0,0,1,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '450' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('450','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '451')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('451','ESCALA ABREVIADA DEL DESARROLLO V3',1,1,0,1,0,0,0,0,1,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '451' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('451','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '452')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('452','ESCALA INDICE O LEARY',1,1,0,1,0,0,0,0,1,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '452' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('452','08','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '454')
INSERT Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('454','ESCALA NIHSS',1,1,0,1,0,0,0,0,1,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '454' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('454','08','0')

-- Actualiza y/o inserta permisos especiales para modificar, reprogramar cirugías y ver solicitudes a farmacia.
UPDATE SEGpermif SET indmendes = 'PERMISO MODIFICAR CIRUGIA' , ioptguard = 1 , ioptconsu = 1 , ioptdisen = 0, ioptactua = 1,  ioptelimi = 1, ioptnaveg = 0, ioptconfi = 0, ioptanula = 0, ioptimpri = 0, igricrear = 0, igrimodif = 0, igrielimi = 0, integracion = 1 WHERE indidmenu = '463'
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '463')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('463','PERMISO MODIFICAR CIRUGIA', 1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '463' AND indmodulo = '09')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('463','09', 0)
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '464')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('464','PERMISO RE-PROGRAMA CIRUGIA', 1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '464' AND indmodulo = '09')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('464','09', 0)
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '465')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('465', 'PERMISO VER SOLICITUDES FARMACIA', 1,1,0,1,1,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '465' AND indmodulo = '09')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('465','09', 0)

-- Inserta permisos para el formulario 'CONSENTIMIENTO INFORMADO' (453).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '453')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('453','CONSENTIMIENTO INFORMADO',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '453' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('453','08','2')



-- Inserta permisos para el 'REPORTE DE ENTREGA DE TURNOS' (028).

IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '028')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula], [ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('028','REPORTE DE ENTREGA DE TURNOS',0,1,0,0,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '028' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('028','08','4')

-------------------------------------

-- Agrega el formulario 'NOTA SERVICIO FARMACEUTICO' (259) para el químico farmacéutico.
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '259')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('259','NOTA SERVICIO FARMACEUTICO',1,1,0,1,1,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '259' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('259','08',0)
-------------------------------------

-- Crea el formulario para 'GENERAR SOPORTES GRUPO ATENCIÓN' (473).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '473')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('473','GENERAR SOPORTES GRUPO ATENCIÓN' ,0,1,0,0,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '473' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('473','08','2')

-- Adiciona la opción de imprimir a la Escala Braden (986).
UPDATE SEGpermif set ioptimpri = 1 where indidmenu = '986'

-- Habilita la impresión para el 'REPORTE DE ENTREGA DE TURNOS' (028).
UPDATE SEGpermif set ioptimpri = 1 where indidmenu = '028'

-- Habilita el permiso de consulta para el formulario 839.
update SEGpermif  set ioptconsu = 1 where indidmenu = '839'
update SEGpermir  set ioptconsu = 1 where indidmenu = '839'

-- Deshabilita el permiso de eliminación en el formulario 'MOTIVOS ANULACION FOLIOS' (217).
update SEGpermif  set ioptelimi = 0 where indidmenu = '217'
update SEGpermir  set ioptelimi = 0 where indidmenu = '217'
update SEGpermiu  set ioptelimi = 0 where indidmenu = '217'

-- Habilita la opción de imprimir en el formulario de Evoluciones (810).
update SEGpermif set ioptimpri = 1 where indidmenu = '810'

-- Define el formulario para la 'ESCALA HUMPTY DUMPTY' (456).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '456')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('456','ESCALA HUMPTY DUMPTY',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '456' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('456','08','0')

-- Define el formulario para la 'ESCALA RIESGO ENFERMEDADES POTENCIALMENTE TRANSMISIBLES' (550).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '550')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('550','ESCALA RIESGO ENFERMEDADES POTENCIALMENTE TRANSMISIBLES',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '550' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('550','08','0')

-- Define el formulario maestro 'TIPO IDENTIFICACION' (551).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '551')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('551','TIPO IDENTIFICACION',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '551' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('551','01','1')

-- Renombra el formulario de Motivos y Causas de Anulación a 'MOTIVOS Y CAUSAS GENERALES' (131).
update SEGpermif set indmendes = 'MOTIVOS Y CAUSAS GENERALES' where indidmenu  = '131'

-- Define el formulario para el 'INFORME MODIFICACIÓN CICLO DEL ESQUEMA' (474).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '474')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('474','INFORME MODIFICACIÓN CICLO DEL ESQUEMA',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '474' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('474','08','4')

-- Asegura que el permiso de eliminación esté deshabilitado para el formulario 'MOTIVOS ANULACION FOLIOS' (217).
UPDATE segpermif SET ioptelimi = 0 where indidmenu = '217' and ioptelimi = 1

-- Define el formulario para la 'ESCALA PIPP-R' (Dolor en recién nacidos) (475).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '475')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('475','ESCALA PIPP-R',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '475' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('475','08','0')

-- Define el formulario para la 'ESCALA FLACC' (Dolor en pacientes no comunicativos) (476).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '476')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('476','ESCALA FLACC',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '476' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('476','08','0')

-- Renombra el formulario 'PAQUETES QUIRURGICOS' a 'PAQUETE DE PRODUCTOS' (405).
update SEGpermif set indmendes = 'PAQUETE DE PRODUCTOS' where indidmenu  = '405'

-- Define el formulario para la 'CONSULTA REGIMEN ALIMENTARIO' (88001).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88001')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88001','CONSULTA REGIMEN ALIMENTARIO',0,1,0,0,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88001' AND indmodulo = '05')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88001','05','2')

-- Define el formulario para 'REGISTRAR PRE-TRIAGE - PRE-CONSULTA' (88003).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88003')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88003','REGISTRAR PRE-TRIAGE - PRE-COSNULTA',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88003' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88003','01','0')

-- Renombra el formulario de Parámetros de Admisiones a 'PARÁMETROS URGENCIAS' (002).
update SEGpermif set indmendes = 'PARÁMETROS URGENCIAS' WHERE indidmenu = '002'

-- Define el formulario maestro 'TIPO GRUPO POBLACIONAL' (88002).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88002')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88002','TIPO GRUPO POBLACIONAL',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88002' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88002','01','1')

-- Define el formulario para 'PARAMETROS NUTRIENTES PARENTERALES' (888).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '888')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('888','PARAMETROS NUTRIENTES PARENTERALES',1,1,0,1,0,0,0,0,1,0,1,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '888' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('888','08','3')

-- Define el formulario maestro 'TIPO DE CATETERES' (88007).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88007')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88007','TIPO DE CATETERES',1,1,0,1,1,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88007' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88007','08','1')

-- Define el formulario maestro 'FACTORES DE RIESGO' (88008).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88008')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88008','FACTORES DE RIESGO',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88008' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88008','08','1')

-- Define el formulario para 'REPORTES DE FACTORES DE RIESGO' (88009).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88009')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88009','REPORTES DE FACTORES DE RIESGO',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88009' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88009','08','2')

-- Define el 'DASHBOARD INSTRUMENTADOR QUIRÚRGICO' (88010).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88010')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88010','INSTRUMENTADOR QUIRÚRGICO',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88010' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88010','08','2')

-- Define el formulario maestro 'CAUSAS DE ATENCIÓN' (88011).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88011')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88011','CAUSAS DE ATENCIÓN',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88011' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88011','01','1')

-- Define el formulario maestro 'FINALIDADES TECNOLOGÍA DE SALUD' (88012).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88012')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88012','FINALIDADES TECNOLOGÍA DE SALUD',1,1,0,1,1,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88012' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88012','01','1')

-- Define el formulario 'REPORTE TRIAGE' (88013).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88013')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88013','Reporte triage',0,1,0,0,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88013' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88013','01','4')

-- Define el formulario para 'PARAMETRIZAR MONITOREO HEMODINÁMICO' (88830).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88830')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88830','Parametrizar monitoreo hemodinámico',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88830' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88830','08','3')

-- Define el formulario maestro 'VÍAS INGRESO SERVICIOS SALUD' (88827).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88827')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88827','Vías ingreso servicios salud',1,1,0,1,1,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88827' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88827','01','1')

-- Define el formulario para 'FORMATOS DE DOCUMENTACIÓN CLÍNICA INTRAHOSPITALARIOS' (88032).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88032')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88032','Formatos de documentación clínica intrahospitalarios',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88032' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88032','08','3')

-- Renombra y define permisos para el formulario 'PARAMETRIZAR FORMATOS DE EDUCACIÓN Y ENCUESTAS' (88005).
update segpermif set indmendes = 'PARAMETRIZAR FORMATOS DE EDUCACIÓN Y ENCUESTAS' where indidmenu = '88005'
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88005' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88005','08','3')

-- Define el formulario para la 'ESCALA CAM' (Confusion Assessment Method) (88044).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88044')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88044','ESCALA CAM',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88044' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88044','08','0')

-- Define el formulario para la 'ESCALA GIJÓN' (Valoración sociofamiliar) (88016).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88016')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88016','ESCALA GIJÓN',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88016' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88016','08','0')

-- Define el formulario para la 'ESCALA DE VALORACIÓN DEL RIESGO FARMACOLÓGICO' (88019).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88019')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('88019','ESCALA DE VALORACIÓN DEL RIESGO FARMACOLÓGICO',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88019' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88019','08','0')

-- Define el formulario para la 'ESCALA VALORACIÓN DEL RIESGO DE INFECCIÓN' (88017).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88017')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88017','ESCALA VALORACIÓN DEL RIESGO DE INFECCIÓN',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88017' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88017','08','0')

-- Define el formulario para la 'ESCALA NUMÉRICA DEL DOLOR (NRS)' (88018).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88018')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88018','ESCALA NUMÉRICA DEL DOLOR (NRS)',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88018' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88018','08','0')

-- Define el formulario para la 'ESCALA GIJÓN ORIGINAL' (88020).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88020')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('88020','ESCALA GIJÓN ORIGINAL',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88020' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88020','08','0')

-- Define el formulario para la 'ESCALA VALORACION RIESGO PSICOSOCIAL' (88024).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88024')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88024','ESCALA VALORACION RIESGO PSICOSOCIAL',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88024' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88024','08','0')

-- Define el formulario para la 'ESCALA OBSTÉTRICA DE ALERTA TEMPRANA' (88021).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88021')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88021','Escala obstétrica de alerta temprana',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88021' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88021','08','0')

-- Define el formulario para la 'ESCALA MPEWS' (Alerta Temprana Pediátrica) (88022).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88022')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88022','ESCALA MPEWS',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88022' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88022','08','0')

-- Define el formulario para la 'ESCALA BPEWS' (Alerta Temprana Pediátrica en Cama) (88023).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88023')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88023','Escala BPEWS',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88023' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88023','08','0')

-- Define el formulario para la 'ESCALA EVENTOS TROMBOEMBÓLICOS VENOSOS' (88025).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88025')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('88025','ESCALA EVENTOS TROMBOEMBÓLICOS VENOSOS',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88025' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88025','08','0')

-- Define el formulario maestro 'GRUPO LISTA CHEQUEO' (88824).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88824')
INSERT INTO dbo.SEGpermif (indidmenu, indmendes, ioptguard, ioptconsu, ioptdisen, ioptactua, ioptelimi, ioptnaveg, ioptconfi, ioptanula, ioptimpri, igricrear, igrimodif, igrielimi, integracion, esfundacional)
VALUES('88824', N'GRUPO LISTA CHEQUEO', 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, NULL);

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88824' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88824','08','0')

-- Define el formulario maestro 'VARIABLES LISTA DE CHEQUEO' (88825).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88825')
INSERT INTO dbo.SEGpermif (indidmenu, indmendes, ioptguard, ioptconsu, ioptdisen, ioptactua, ioptelimi, ioptnaveg, ioptconfi, ioptanula, ioptimpri, igricrear, igrimodif, igrielimi, integracion, esfundacional)
VALUES('88825', N'VARIABLES LISTA DE CHEQUEO', 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, NULL);

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88825' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88825','08','0')

-- Define el formulario para 'BITACORAS DE SEGUIMIENTO' (88829).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88829')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('88829','BITACORAS DE SEGUIMIENTO',0,1,0,0,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88829' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88829','08','2')

-- Define el formulario para la 'ESCALA DE BISHOP' (inducción del parto) (88026).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88026')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('88026','ESCALA DE BISHOP',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88026' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88026','08','0')

-- Define un 'Permiso para Medicamentos Adicionales' (88836), posiblemente para casos especiales.
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88836')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('88836','Permiso medicamentos adicionales',1,0,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88836' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88836','08','0')

-- Define el formulario maestro 'Parámetros FURIPS' (Formulario Único de Reclamación de SOAT) (88027).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88027')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] ) 
Values ('88027','Parámetros FURIPS',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88027' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88027','01','1')

-- Define el formulario maestro 'Modalidades de atención' (88028).
 IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88028')
 Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88028','Modalidades de atención',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88028' AND indmodulo = '01')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88028','01','1')

-- Define el formulario para la 'ESCALA OFRAS' (riesgo de caídas en obstetricia) (88046).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88046')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88046','ESCALA OFRAS',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88046' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88046','08','0')

-- Define un permiso para 'Desconfirmar FURIPS' (88029), probablemente para correcciones.
 IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88029')
 INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('88029','Permiso desconfirmar FURIPS',1,0,0,0,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88029' AND indmodulo = '01')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88029','01','0')

-- Define el formulario para la 'ESCALA FINNEGAN' (síndrome de abstinencia neonatal) (88837).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88837')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88837','ESCALA FINNEGAN',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88837' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88837','08','0')

-- Define el formulario para la 'Escala de clasificación de choque y evaluación de la respuesta' (88838).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88838')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('88838','Escala de clasificación de choque',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88838' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88838','08','0')

-- Define el formulario para 'ESCALA CUESTIONARIO EPOC' (88047).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88047')
INSERT Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88047','ESCALA CUESTIONARIO EPOC',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88047' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88047','08','0')

-- Define el formulario para la 'ESCALA GAD-2' (ansiedad) (88031).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88031')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('88031','ESCALA GAD-2',1,1,0,1,0,0,0,0,1,0,0,0,1)
 
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88031' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88031','08','0')

-- Define el formulario para la 'ESCALA ESTRATIFICACIÓN RIESGO CARDIOVASCULAR DE LA OMS' (88030).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88030')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('88030','ESCALA ESTRATIFICACIÓN RIESGO CARDIOVASCULAR DE ORGANIZACIÓN MUNDIAL DE LA SALUD',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88030' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88030','08','0')

-- Define el formulario maestro 'Tipos de admisiones' (88839).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88839')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88839','Tipos de admisiones',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88839' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88839','01','1')

-- Define el formulario para la 'ESCALA MEDICION GRADO DE TABAQUISMO' (Índice Paquetes Año) (88048).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88048')
INSERT Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88048','ESCALA MEDICION GRADO DE TABAQUISMO',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88048' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88048','08','0')

-- Define el formulario maestro 'Parámetros FURTRAN' (Formulario para Transporte) (88033).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88033')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] ) 
Values ('88033','Parámetros FURTRAN',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88033' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88033','01','1')

-- Define un permiso para crear ingresos desde la confirmación de citas de Apoyo Diagnóstico.
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '2245')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('2245','PERMISO CREAR INGRESOS DESDE CONFIRMAR CITA APOYO DX',1,0,0,0,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '2245' AND indmodulo = '09')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('2245','09','0')

-- Define el formulario maestro 'RECOMENDACIONES' (88035).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88035')
INSERT Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88035','RECOMENDACIONES',1,1,0,1,0,0,0,0,1,0,0,0,1)
 
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88035' AND indmodulo = '05')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88035','05','1')

-- Define el formulario maestro 'TIPO PRIORIDAD' (88034).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88034')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] ) 
Values ('88034','TIPO PRIORIDAD',1,1,0,1,0,0,0,0,0,0,0,0,1)
 
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88034' AND indmodulo = '05')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88034','05','1')

-- Define permisos para la funcionalidad de 'RECOMENDAR PACIENTE' y 'AGREGAR RECOMENDACIÓN' (88036, 88037).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88036')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] ) 
Values ('88036','RECOMENDAR PACIENTE',0,1,0,0,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88037')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] ) 
Values ('88037','AGREGAR RECOMENDACIÓN',1,0,0,0,0,0,0,0,0,0,0,0,1)
 
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88036' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88036','01','0')

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88037' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88037','01','0')

-- Define el formulario maestro 'TIPOS DE GENERO' (88038).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88038')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88038','TIPOS DE GENERO',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88038' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88038','01','1')

-- Define el formulario para la 'Escala de Silverman - Anderson' (dificultad respiratoria neonatal) (88840).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88840')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88840','Escala de Silverman - Anderson',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88840' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88840','08','0')

-- Define el formulario para la 'Bitácora de auditoría historia clínica' (88039).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88039')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88039','Bitácora de auditoría historia clínica',0,1,0,0,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88039' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88039','08','4')

-- Define el formulario para la 'ESCALA DOWNTON' (riesgo de caídas) (88049).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88049')
INSERT Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88049','ESCALA DOWNTON',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88049' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88049','08','0')

-- Define un permiso para la funcionalidad de 'Hoja de gasto quirúrgica' (88050).
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88050' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion]) Values ('88050','08','0')

IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88050')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] ) Values ('88050','Hoja de gasto quirúrgica',1,0,0,0,0,0,0,0,0,0,0,0,1)

-- Define el formulario maestro 'DEFINICIÓN DE MUESTRAS' para laboratorio (88040).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88040')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('88040','DEFINICIÓN DE MUESTRAS',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88040' AND indmodulo = '08')
INSERT INTO  [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88040','08','1')

-- Renombra la escala 'ESCALA DOWNTON ADAPTADA' (323).
UPDATE SEGPERMIF SET indmendes = 'ESCALA DOWNTON ADAPTADA' WHERE indidmenu = '323'

-- Define el formulario para 'Regenerar folio a PDF' (88041), una herramienta administrativa.
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88041')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88041','Regenerar folio a PDF',0,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88041' AND indmodulo = '01')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88041','01','2')

-- Define un permiso para habilitar el censo histórico general (88070).
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88070' AND indmodulo = '05')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion]) Values ('88070','05','0')
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88070')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] ) Values ('88070','Habilitar censo historico general',0,0,0,0,0,0,0,0,0,0,0,0,1)

-- Define un permiso para la 'Solicitud de medicamentos en Pre-alta hospitalaria' (88042).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88042')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88042','Solicitud de medicamentos en Pre-alta hospitalaria',1,0,0,0,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88042' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88042','08','0')

-- Define el formulario para la 'Escala ABCD2' (Riesgo de ACV) (88843).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88843')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88843','Escala ABCD2 (Riesgo de ACV despues de un ataque isquémico transitorio)',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88843' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88843','08','0')

-- Define el formulario para la 'Escala Alvarado' (diagnóstico de apendicitis) (88844).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88844')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88844','Escala Alvarado',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88844' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88844','08','0')

-- Define el formulario para la 'Escala EsGravE' (Equipo de Respuesta Rápida pediatría) (88845).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88845')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88845','Escala EsGravE (Equipo de Respuesta Rápida pediatría)',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88845' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88845','08','0')

-- Define el formulario para la 'Escala Shock Index' (88846).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88846')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88846','Escala Shock Index',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88846' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88846','08','0')

-- Renombra el formulario de 'Consultar Referencias' a 'Consultar referencia / contrarreferencia'.
update SEGpermif set indmendes = 'Consultar referencia / contrarreferencia' where indidmenu = '921'

-- Define el formulario para la 'Escala de evaluación del riesgo de preeclampsia' (88847).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88847')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88847','Escala de evaluación del riesgo de preeclampsia',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88847' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88847','08','0')

-- Define el formulario para la 'Escala APGAR Familiar para uso en niños' (88051).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88051')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88051','Escala APGAR Familiar para uso en niños',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88051' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88051','08','0')

-- Define un permiso para 'Modificar ciclos vigentes de quimioterapia' (88852).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88852')
INSERT INTO [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
VALUES ('88852','Modificar ciclos vigentes de quimioterapia',0,1,0,0,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88852' AND indmodulo = '01')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88852','01','0')

-- Define el formulario 'Enrutador Imágenes Intrahospitalarias' (88853).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88853')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88853','Enrutador Imágenes Intrahospitalarias',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88853' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88853','08','2')

-- Define el formulario maestro 'Parámetros Lactario' (88854).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88854')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88854','Parámetros Lactario',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88854' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88854','08','3')

-- Define el formulario 'Dashboard lactario' (88855).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88855')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88855','Dashboard lactario',1,1,0,1,1,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88855' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88855','08','2')



---------------------------------------------------------------  Sprint Week 42 - 43 (2025)  --------------------------------------------------------------- 
update SEGpermif set indmendes = 'Dashboard Terapias Reemplazo Renal' where indidmenu = '838' 


---  Dashboard de Gestión de Desinfección de Equipos de Dialización (31615).
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88857')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88857','Desinfección equipos de tratamiento renal',1,1,0,1,1,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88857' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88857','08','2')

--- Dashboard de Gestion Imagenes Asistidas
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88856')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88856','Gestión imágenes asistidas',1,1,0,1,1,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88856' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88856','08','2')


---------------------------------------------------------------  Sprint Week 44 - 45 (2025)  ---------------------------------------------------------------  
--- PBI-31514 2. Crear escala Nutric-Score en el sistema

IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88858')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88858','Escala NUTRIC SCORE',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88858' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88858','08','0')

--- PBI-32116 2. Crear escala Braden Q en el sistema
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88859')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88859','Escala Braden Q',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88859' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88859','08','0')

---------------------------------------------------------------  Sprint Week 48 - 49 (2025)  ---------------------------------------------------------------  

-- PBI-32350  1. Crear Maestro Tipo de riesgos
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88860')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] ) 
Values ('88860','Tipos de riesgos',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88860' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88860','01','1')

-- PBI-32355 1. Crear Maestro Tipos de alergia
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88861')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] ) 
Values ('88861','Tipos de alergia',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88861' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88861','01','1')

--PBI-32373 1. Creación de reporte "Circular 022"
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88862')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] ) 
Values ('88862','Circular 022',1,1,0,1,0,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88862' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('88862','08','4')


---------------------------------------------------------------  Sprint Week 50 - 51 (2025)  ---------------------------------------------------------------  
--Escala Evaluación de síntomas de Edmonton
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88863')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88863','Escala evaluación de síntomas de Edmonton (ESAS)',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88863' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88863','08','0')

--Escala IDSA NAC
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88864')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('88864','Escala IDSA-NAC',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88864' AND indmodulo = '08')
INSERT INTO [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
VALUES ('88864','08','0')


---------------------------------------------------------------  Sprint Week 4 - 5 (2026)  ---------------------------------------------------------------  

--Mapeo CIE-11
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '89038')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('89038','Mapeo CIE-11',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '89038' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('89038','01','1')


---------------------------------------------------------------  Product Backlog Item Sprint Week 4 - 5 (2026) ---------------------------------------------------------------  

--Mapeo Estratificación Socioeconómica
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '89039')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('89039','Estratificación socioeconómica',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '89039' AND indmodulo = '01')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('89039','01','1')


----------------------------------------------------------------  Sprint Week 16 - 17 (2026)  -----------------------------------------------------------------------

--PBI-35432 1. Crear Escala Columbia (Suicide severity rating scale)

IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '89042')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('89042','Escala Columbia',1,1,0,1,0,0,0,0,1,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '89042' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('89042','08','0')
 
-- Inserta permisos y ubicación para el formulario de 'Escala de riesgo suicida de Plutchik' (89041).

IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '89041')
Insert Into [SEGpermif] ( [indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion] )
Values ('89041','Escala de riesgo suicida de Plutchik',1,1,0,1,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '89041' AND indmodulo = '08')
Insert Into [SEGmenusu] ( [indidmenu],[indmodulo], [indopcion])
Values ('89041','08','0')

-----------------------PBI 35008 - 35693
ALTER TABLE ADINGRESO
ADD IpsAddress NVARCHAR(200) NULL;

EXEC sp_addextendedproperty
    @name       = N'MS_Description',
    @value      = N'Dirección física o postal de la IPS. Campo opcional.',
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE',  @level1name = N'ADINGRESO',
    @level2type = N'COLUMN', @level2name = N'IpsAddress';
GO

ALTER TABLE ADINGRESO
ADD IpsPhone VARCHAR(15) NULL;

EXEC sp_addextendedproperty
    @name       = N'MS_Description',
    @value      = N'Número de teléfono de la IPS.',
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE',  @level1name = N'ADINGRESO',
    @level2type = N'COLUMN', @level2name = N'IpsPhone';
GO

ALTER TABLE ADINGRESO
ADD IpsEmail VARCHAR(200) NULL;

EXEC sp_addextendedproperty
    @name       = N'MS_Description',
    @value      = N'Correo electrónico de la IPS. Campo opcional.',
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE',  @level1name = N'ADINGRESO',
    @level2type = N'COLUMN', @level2name = N'IpsEmail';
GO
ALTER TABLE ADINGRESO
ADD ReferredFromAnotherIPS BIT NULL;
EXEC sp_addextendedproperty
    @name       = N'MS_Description',
    @value      = N'Indica si el paciente fue remitido desde otra IPS (1=Sí, 0=No).',
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE',  @level1name = N'ADINGRESO',
    @level2type = N'COLUMN', @level2name = N'ReferredFromAnotherIPS';
GO
ALTER TABLE ADINGRESO
ADD VictimCondition TINYINT NULL;
EXEC sp_addextendedproperty
    @name       = N'MS_Description',
    @value      = N'Condición de la víctima en el accidente (1=Conductor, 2=Pasajero, 3=Peatón, 4=Desconocido).',
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE',  @level1name = N'ADINGRESO',
    @level2type = N'COLUMN', @level2name = N'VictimCondition';
GO
ALTER TABLE ADINGRESO
ADD VehicleTypeInvolved TINYINT NULL;
EXEC sp_addextendedproperty
    @name       = N'MS_Description',
    @value      = N'Tipo de vehículo involucrado en el accidente de tránsito. (1 Bicicleta , 2 Camión , 3 Moto, 4 bus ,5 Vehículo particular,6 Vehículo de servicio publico,7 otro )',
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE',  @level1name = N'ADINGRESO',
    @level2type = N'COLUMN', @level2name = N'VehicleTypeInvolved';

GO

----------------------------------------------------------------  Sprint Week 20 - 21 (2026)  -----------------------------------------------------------------------
-- PBI 36232 Creacion del formulario FUR
-- 20 / 05 / 2026
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '89043')
INSERT INTO [dbo].[SEGpermif] ([indidmenu] ,[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion],[esfundacional])
     VALUES ('89043' ,'FUR - Formato único de reclamaciones',1,1,0,1,1,0,0,0,0,0,0,0,1,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '89043' AND indmodulo = '01')
INSERT INTO [dbo].[SEGmenusu] ([indidmenu],[indmodulo],[indopcion])
     VALUES (89043,'01',2)

----------------------------------------------------------------  Sprint Week 24 - 25 (2026)  -----------------------------------------------------------------------
-- PBI 32756 Creacion del formulario Tipo de usuario

IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '89044')
INSERT INTO [dbo].[SEGpermif] ([indidmenu] ,[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion],[esfundacional])
VALUES ('89044' ,'Tipo de usuario',1,1,0,1,0,0,0,0,0,0,0,0,1,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '89044' AND indmodulo = '01')
INSERT INTO [dbo].[SEGmenusu] ([indidmenu],[indmodulo],[indopcion])
VALUES ('89044','01',1)

----------------------------------------------------------------  Sprint Week 26 - 27 (2026)  -----------------------------------------------------------------------
-- PBI 36565 Creacion de formulario para notas aclaratorias

IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '89047')
INSERT INTO [dbo].[SEGpermif] ([indidmenu] ,[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion],[esfundacional])
VALUES ('89047' ,'Notas aclaratorias',1,1,0,1,0,0,0,0,0,0,0,0,1,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '89047' AND indmodulo = '01')
INSERT INTO [dbo].[SEGmenusu] ([indidmenu],[indmodulo],[indopcion])
VALUES ('89047','01',1)

----------------------------------------------------------------  Sprint Week 28 - 29 (2026)  -----------------------------------------------------------------------
-- Formulario principal Dashboard Autorizaciones Intrahospitalarias
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '89045')
INSERT INTO [dbo].[SEGpermif] ([indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion])
VALUES ('89045','Dashboard Autorizaciones Intrahospitalarias',1,1,0,1,1,0,0,0,0,0,0,0,1)

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '89045' AND indmodulo = '01')
INSERT INTO [dbo].[SEGmenusu] ([indidmenu],[indmodulo],[indopcion])
VALUES ('89045','01',2)

-- Reactivar autorización intrahospitalaria
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88984')
INSERT INTO [dbo].[SEGpermif] ([indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion])
VALUES ('88984','Autorización intrahospitalaria: Reactivar',0,0,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88984' AND indmodulo = '01')
INSERT INTO [dbo].[SEGmenusu] ([indidmenu],[indmodulo],[indopcion])
VALUES ('88984','01',0)

-- Entregar servicio al área solicitante
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88985')
INSERT INTO [dbo].[SEGpermif] ([indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion])
VALUES ('88985','Autorización intrahospitalaria: Entregar servicio',0,0,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88985' AND indmodulo = '01')
INSERT INTO [dbo].[SEGmenusu] ([indidmenu],[indmodulo],[indopcion])
VALUES ('88985','01',0)

-- Generar anexo Decreto 3047
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88986')
INSERT INTO [dbo].[SEGpermif] ([indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion])
VALUES ('88986','Autorización intrahospitalaria: Generar anexo',0,0,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88986' AND indmodulo = '01')
INSERT INTO [dbo].[SEGmenusu] ([indidmenu],[indmodulo],[indopcion])
VALUES ('88986','01',0)

-- Reasignar autorización intrahospitalaria
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88987')
INSERT INTO [dbo].[SEGpermif] ([indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion])
VALUES ('88987','Autorización intrahospitalaria: Reasignar',0,0,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88987' AND indmodulo = '01')
INSERT INTO [dbo].[SEGmenusu] ([indidmenu],[indmodulo],[indopcion])
VALUES ('88987','01',0)

-- Cancelar solicitud de autorización intrahospitalaria
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88988')
INSERT INTO [dbo].[SEGpermif] ([indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion])
VALUES ('88988','Autorización intrahospitalaria: Cancelar Solicitud',0,0,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88988' AND indmodulo = '01')
INSERT INTO [dbo].[SEGmenusu] ([indidmenu],[indmodulo],[indopcion])
VALUES ('88988','01',0)

-- Agregar evento de trámite Decreto 3047
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88989')
INSERT INTO [dbo].[SEGpermif] ([indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion])
VALUES ('88989','Autorización intrahospitalaria: Agregar evento',0,0,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88989' AND indmodulo = '01')
INSERT INTO [dbo].[SEGmenusu] ([indidmenu],[indmodulo],[indopcion])
VALUES ('88989','01',0)

-- Imprimir reporte de autorización intrahospitalaria
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '88990')
INSERT INTO [dbo].[SEGpermif] ([indidmenu],[indmendes],[ioptguard],[ioptconsu],[ioptdisen],[ioptactua],[ioptelimi],[ioptnaveg],[ioptconfi],[ioptanula],[ioptimpri],[igricrear],[igrimodif],[igrielimi],[integracion])
VALUES ('88990','Autorización intrahospitalaria: Imprimir reporte',0,0,0,0,0,0,0,0,0,0,0,0,1)
IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '88990' AND indmodulo = '01')
INSERT INTO [dbo].[SEGmenusu] ([indidmenu],[indmodulo],[indopcion])
VALUES ('88990','01',0)

-- Frm parametros autorizacion
IF NOT EXISTS (SELECT 1 FROM dbo.segpermif WHERE indidmenu = '89048')
INSERT INTO [SEGpermif] ([indidmenu], [indmendes], [ioptguard], [ioptconsu], [ioptdisen], [ioptactua], [ioptelimi], [ioptnaveg], [ioptconfi], [ioptanula],[ioptimpri], [igricrear], [igrimodif], [igrielimi], [integracion])
VALUES ('89048', 'Parámetros de Gestión de autorización', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1);

IF NOT EXISTS (SELECT 1 FROM dbo.segmenusu WHERE indidmenu = '89048' AND indmodulo = '01')
INSERT INTO [SEGmenusu] ([indidmenu], [indmodulo], [indopcion])
VALUES ('89048', '01', '3');

----- se setan permisos a formulario obsoleto ( frmHCParametrosEnfermeria )

UPDATE SEGpermiu
SET ioptguard  = 0,
    ioptconsu  = 0,
    ioptdisen  = 0,
    ioptactua  = 0,
    ioptelimi  = 0,
    ioptnaveg  = 0,
    ioptconfi  = 0,
    ioptanula  = 0,
    ioptimpri  = 0,
    igricrear  = 0,
    igrimodif  = 0,
    igrielimi  = 0,
    ioptvisib  = 0
WHERE indidmenu = '123';

UPDATE SEGpermir
SET ioptguard  = 0,
    ioptconsu  = 0,
    ioptdisen  = 0,
    ioptactua  = 0,
    ioptelimi  = 0,
    ioptnaveg  = 0,
    ioptconfi  = 0,
    ioptanula  = 0,
    ioptimpri  = 0,
    igricrear  = 0,
    igrimodif  = 0,
    igrielimi  = 0,
    ioptvisib  = 0
WHERE indidmenu = '123';