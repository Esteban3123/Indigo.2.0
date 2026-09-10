

CREATE VIEW [Report].[ViewExtramurales] AS

WITH CTE_ORDENES
AS
(
select distinct * from (
SELECT 
H.IPCODPACI, 
H.NUMINGRES, 
H.CODPROSAL, 
H.FECORDMED, 
H.CODSERIPS,
H.IDDESCRIPCIONRELACIONADA, 
H.CANSERIPS 'CANTIDAD',
H.CODDIAGNO,
CASE ESTSERIPS WHEN 1 THEN 'Ordenado' 
			   WHEN 2 THEN 'Completado' 
			   WHEN 3 THEN 'Interpretado' 
			   WHEN 4 THEN 'Completado' 
			   WHEN 5 THEN 'Anulado'
			   WHEN 6 THEN 'Sala programada' END ESTADO
FROM HCORDPRON H
WHERE CODSERIPS IN ('601T01','601T02','602T01','602T02','O201','O202','O204','O205','O207','O211','O212','O213','O214','O220','O222','O223',
'O224','O226','O227','O229','O653','O733','920105','920106','920202','920203','920204','920208','920301','920304','920410','920501','920502',
'920503','920505','920510','920601','920602','920604','920605','920606','920607','920608','920701','920702','920707','920708','920802','920804',
'920805','920806','920807','920808','920809','920811','920812','920901','920902','920903','921100','921200','921301','921302','920215','920310',
'920609','920703','920705','920706','920904','921303','921700','921800','873305','871062','883321','883322','883324','879302','879302','879902',
'922506','890287','922441','922441','922441','922441','922442','922443','922444','922445','922504','922504','922505','922201','922321','922322',
'922446','950505','951302','951303','951304','951901','951901','951902','951902','951903','951903','952101','952102','952001','903606','903607',
'954105','954301','954302','954304','954307','893201','893202','592402','H00028','890102','890283','890476','890283') 
--AND H.NUMEFOLIO=(SELECT MAX(O.NUMEFOLIO) FROM HCORDPRON O WHERE H.IPCODPACI=O.IPCODPACI)

UNION ALL

SELECT 
H.IPCODPACI, 
H.NUMINGRES, 
H.CODPROSAL, 
H.FECORDMED, 
H.CODSERIPS, 
H.IDDESCRIPCIONRELACIONADA,
H.CANSERIPS 'CANTIDAD',
H.CODDIAGNO,
CASE ESTSERIPS WHEN 1 THEN 'Solicitado' 
			   WHEN 2 THEN 'Sala Programada' 
			   WHEN 3 THEN 'Cancelado' 
			   WHEN 4 THEN 'Resultado Revisado' END ESTADO
FROM HCORDPROQ H
WHERE H.CODSERIPS IN ('601T01','601T02','602T01','602T02','O201','O202','O204','O205','O207','O211','O212','O213','O214','O220','O222','O223','O224',
'O226','O227','O229','O653','O733','920105','920106','920202','920203','920204','920208','920301','920304','920410','920501','920502','920503',
'920505','920510','920601','920602','920604','920605','920606','920607','920608','920701','920702','920707','920708','920802','920804','920805',
'920806','920807','920808','920809','920811','920812','920901','920902','920903','921100','921200','921301','921302','920215','920310','920609',
'920703','920705','920706','920904','921303','921700','921800','873305','871062','883321','883322','883324','879302','879302','879902','922506',
'890287','922441','922441','922441','922441','922442','922443','922444','922445','922504','922504','922505','922201','922321','922322','922446',
'950505','951302','951303','951304','951901','951901','951902','951902','951903','951903','952101','952102','952001','903606','903607','954105',
'954301','954302','954304','954307','893201','893202','592402','H00028','890102','890283') --AND H.NUMEFOLIO=(SELECT MAX(O.NUMEFOLIO) FROM HCORDPROQ O WHERE H.IPCODPACI=O.IPCODPACI)

UNION ALL

SELECT 
H.IPCODPACI, 
H.NUMINGRES, 
H.CODPROSAL, 
H.FECORDMED, 
LTRIM(RTRIM(H.CODSERIPS)),
H.IDDESCRIPCIONRELACIONADA, 
H.CANSERIPS 'CANTIDAD',
H.CODDIAGNO,
CASE H.ESTSERIPS WHEN 1 THEN 'Solicitado' 
				 WHEN 2 THEN 'Estudio Realizado' 
				 WHEN 3 THEN 'Imagen Procesada' 
				 WHEN 4 THEN 'Estudio Interpretado' 
				 WHEN 5 THEN 'Remitido'
				 WHEN 6 THEN 'Anulado' 
				 WHEN 7 THEN 'Extramural' END ESTADO
FROM HCORDIMAG H
WHERE H.CODSERIPS IN ('601T01','601T02','602T01','602T02','O201','O202','O204','O205','O207','O211','O212','O213','O214','O220','O222','O223','O224',
'O226','O227','O229','O653','O733','920105','920106','920202','920203','920204','920208','920301','920304','920410','920501','920502','920503 ',
'920505','920510','920601','920602','920604','920605','920606','920607','920608','920701','920702','920707','920708','920802','920804','920805',
'920806','920807','920808','920809','920811','920812','920901','920902','920903','921100','921200','921301','921302','920215','920310','920609',
'920703','920705','920706','920904','921303','921700','921800','873305','871062','883321','883322','883324','879302','879302','879902','922506',
'890287','922441','922441','922441','922441','922442','922443','922444','922445','922504','922504','922505','922201','922321','922322','922446',
'950505','951302','951303','951304','951901','951901','951902','951902','951903','951903','952101','952102','952001','903606','903607','954105',
'954301','954302','954304','954307','893201','893202','592402','H00028','890102','890283','879601') ) as a
--AND H.NUMEFOLIO=(SELECT MAX(O.NUMEFOLIO) FROM HCORDIMAG O WHERE H.IPCODPACI=O.IPCODPACI)

UNION ALL 

SELECT 
I.IPCODPACI, 
I.NUMINGRES, 
I.CODPROSAL, 
I.FECORDMED, 
I.CODSERIPS, 
I.IDDESCRIPCIONRELACIONADA,
I.CANSERIPS 'CANTIDAD',
I.CODDIAGNO,
CASE ESTSERIPS WHEN 1 THEN 'Solicitado' 
			   WHEN 2 THEN 'Solicitud Enviada' 
			   WHEN 3 THEN 'Interconsulta Realizada' 
			   WHEN 4 THEN 'Extramural' 
			   WHEN 5 THEN 'Anulado' END ESTADO

FROM HCORDINTE I
WHERE I.CODSERIPS IN ('890476','890487')

),

CTE_NOTA
AS
(

SELECT  FECHACREACION,NOMBRE, IPCODPACI, 
	CASE WHEN NUMINGRES IS NULL THEN TRIM([116]) ELSE NUMINGRES END AS NUMINGRES,[14] 'FECHA REGISTRO',[73]'OBSERVACION',CODUSUARI,
	CASE WHEN [79]='SI' THEN 'SOLICITADO'
		 WHEN [80]='SI' THEN 'EN PROCESO'
		 WHEN [81]='SI' THEN 'COMPLETADO'
		 WHEN [82]='SI' THEN 'CANCELADO' END 'ESTADO'
		 ,[118]'FECHA ORDEN MEDICA'
		 ,CASE WHEN LEN(TRIM(SUBSTRING (REPLACE([118],'/',''),0,9))) <8 THEN '0'+TRIM(SUBSTRING (REPLACE([118],'/',''),2,2)) ELSE TRIM(SUBSTRING (REPLACE([118],'/',''),3,2)) END 'MES'
		 ,SUBSTRING(LTRIM(RTRIM(CAST([117] AS CHAR (20)))),0,7) 'CUPS' 
FROM (
SELECT REG.FECHACREACION FECHACREACION,NT.NOMBRE, REG.IPCODPACI, REG.NUMINGRES,IDNTVARIABLE, DET1.VALOR,REG.CODUSUARI
FROM NTXVARIABLES VARI 
	LEFT JOIN NTADMINISTRATIVAS NT ON NT.ID=VARI.IDNTADMINISTRATIVAS
	LEFT JOIN NTNOTASADMINISTRATIVASC REG ON REG.IDNOTAADMINISTRATIVA=VARI.IDNTADMINISTRATIVAS
	LEFT JOIN NTNOTASADMINISTRATIVASD DET1 ON DET1.IDNTNOTASADMINISTRATIVASC=REG.ID 
	WHERE NT.ID=9 AND VARI.IDNTADMINISTRATIVAS=9  ) AS EXTRA
PIVOT 
(MAX(VALOR) 
	FOR IDNTVARIABLE IN ([14],[73],[79],[80],[81],[82],[116],[117],[118]) 
	)AS CONSULTA

--AND REG.IPCODPACI='1074828820'
),

CTE_ESTANCIAS
AS
(
SELECT ROW_NUMBER () OVER(PARTITION BY EST.IPCODPACI ORDER BY EST.FECREGSIS DESC) NUMES,EST.IPCODPACI, EST.NUMINGRES,EST.FECINIEST,FECFINEST, EST.CODICAMAS, CAM.DESCCAMAS,EST.FECREGSIS, EST.REGESTADO  
FROM CHREGESTA EST
INNER JOIN CHCAMASHO CAM ON CAM.CODICAMAS=EST.CODICAMAS AND EST.REGESTADO=1
)

SELECT 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,   
HA.Code 'COD. EAPB',
HA.Name 'ENTIDAD',ISNULL(EXT.NUMINGRES,'NO REGISTRA') 'INGRESO',
ORD.IPCODPACI 'ID PACIENTE',PAC.IPNOMCOMP 'NOMBRE PACIENTE',
DATEDIFF(YEAR,PAC.IPFECNACI,GETDATE()) 'EDAD (AÑOS)',ORD.NUMINGRES 'INGRESO ORDEN',ISNULL(EST.DESCCAMAS,'NA/EXTRAMURAL') 'CAMA ACTUAL', ORD.CODPROSAL 'COD. PROFESIONAL',
ORD.FECORDMED 'FECHA ORDEN', ORD.CODSERIPS 'COD. CUPS', CUPS.DESCODCUPS 'NOMBRE SERVICIO', RELA.Name 'DESCRIPCION RELACIONADA',ORD.CODDIAGNO 'COD. CIE-10' ,DIA.NOMDIAGNO 'NOMBRE DX',
ORD.CANTIDAD 'CANTIDAD', ORD.ESTADO 'ESTADO ORDEN',
CASE WHEN EXT.FECHACREACION IS NOT NULL THEN EXT.FECHACREACION ELSE '1800-01-01' END 'FECHA REGISTRO NOTA',
ISNULL(EXT.OBSERVACION,'NO REGISTRA') [OBSERVACIONES NOTA], ISNULL(EXT.ESTADO,'NO REGISTRA') 'ESTADO NOTA', ISNULL(USU.NOMUSUARI,'NO REGISTRA') 'USUARIO REGISTRA NOTA',
CAST(ORD.FECORDMED  AS date) AS 'FECHA BUSQUEDA',
YEAR(ORD.FECORDMED) AS 'AÑO BUSQUEDA',
MONTH(ORD.FECORDMED) AS 'MES BUSQUEDA',
CONCAT(FORMAT(MONTH(ORD.FECORDMED), '00') ,' - ', 
       CASE MONTH(ORD.FECORDMED) 
	    WHEN 1 THEN 'ENERO'
   	    WHEN 2 THEN 'FEBRERO'
	    WHEN 3 THEN 'MARZO'
	    WHEN 4 THEN 'ABRIL'
	    WHEN 5 THEN 'MAYO'
	    WHEN 6 THEN 'JUNIO'
	    WHEN 7 THEN 'JULIO'
	    WHEN 8 THEN 'AGOSTO'
	    WHEN 9 THEN 'SEPTIEMBRE'
	    WHEN 10 THEN 'OCTUBRE'
	    WHEN 11 THEN 'NOVIEMBRE'
	    WHEN 12 THEN 'DICIEMBRE' END) 'MES NOMBRE BUSQUEDA',
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
CTE_ORDENES ORD 
LEFT JOIN INCUPSIPS CUPS ON CUPS.CODSERIPS=ORD.CODSERIPS
LEFT JOIN INPACIENT PAC ON PAC.IPCODPACI=ORD.IPCODPACI
LEFT JOIN CONTRACT.HealthAdministrator HA ON HA.Id=PAC.GENCONENTITY
LEFT JOIN Contract.ContractDescriptions RELA ON RELA.Id=ORD.IDDESCRIPCIONRELACIONADA
LEFT JOIN CTE_ESTANCIAS EST ON  EST.IPCODPACI=ORD.IPCODPACI AND EST.NUMINGRES=ORD.NUMINGRES
LEFT JOIN INDIAGNOS DIA ON DIA.CODDIAGNO=ORD.CODDIAGNO
LEFT JOIN CTE_NOTA EXT ON EXT.NUMINGRES=ORD.NUMINGRES AND EXT.[MES]=MONTH(ORD.FECORDMED)   AND EXT.[FECHACREACION]> ORD.FECORDMED AND EXT.[CUPS]=ORD.CODSERIPS
LEFT JOIN SEGusuaru USU ON USU.CODUSUARI=EXT.CODUSUARI
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting para el seguimiento de servicios extramurales ordenados a pacientes. Consolida órdenes médicas de procedimientos, imágenes e interconsultas filtradas por códigos CUPS específicos (procedimientos extramurales), uniendo su estado de ejecución con datos del paciente, aseguradora (EAPB), cama actual, diagnóstico CIE-10 y notas administrativas de seguimiento. Está orientada a reportes de gestión y auditoría de servicios prestados fuera de la institución.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExtramurales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExtramurales';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida las órdenes médicas (procedimientos, cirugías, imágenes e interconsultas) catalogadas como extramurales y las cruza con paciente, EAPB, estancia, diagnóstico y nota administrativa de seguimiento, para reportería gerencial.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExtramurales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de la plantilla de nota administrativa con ID=9 en NTADMINISTRATIVAS y de las variables PIVOT [14],[73],[79]–[82],[116],[117],[118].; Catálogos referenciales poblados: INCUPSIPS, INDIAGNOS, INPACIENT, CONTRACT.HealthAdministrator, Contract.ContractDescriptions, SEGusuaru.; Los códigos de servicio CUPS que se consideran extramurales deben coincidir con la lista IN(...) embebida en cada subconsulta.; La zona horaria ''Pakistan Standard Time'' debe estar registrada en sys.time_zone_info del servidor SQL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExtramurales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los registros de órdenes se restringen a un catálogo cerrado de códigos CUPS considerados extramurales (procedimientos, imágenes, laboratorio, terapias) más interconsultas ''890476''/''890487''.; Solo se consideran estancias activas (CHREGESTA.REGESTADO = 1) al unir la cama actual.; Las notas administrativas provienen exclusivamente de la plantilla NTADMINISTRATIVAS.ID = 9 (variable IDNTADMINISTRATIVAS = 9).; La nota se asocia a la orden solo si: mismo NUMINGRES, mismo mes de FECORDMED, mismo CUPS y FECHACREACION posterior a FECORDMED.; La edad se calcula en años completos como DATEDIFF(YEAR, IPFECNACI, GETDATE()).; Cuando no hay cama asignada se reporta ''NA/EXTRAMURAL''; cuando faltan ingreso, observación, estado de nota o usuario se reporta ''NO REGISTRA''.; ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; ID_COMPANY se infiere del nombre de la base de datos actual (DB_NAME()).; El estado de la orden depende de la tabla origen: cada fuente (HCORDPRON, HCORDPROQ, HCORDIMAG, HCORDINTE) tiene su propia tabla de equivalencias de ESTSERIPS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExtramurales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Atención extramural; Órdenes médicas (procedimientos no quirúrgicos, quirúrgicos, imágenes diagnósticas, interconsultas); CUPS (Clasificación Única de Procedimientos en Salud); Diagnóstico CIE-10; EAPB / Administradora de salud; Estancia hospitalaria y cama asignada; Notas administrativas con variables tipo PIVOT; Paciente e ingreso; Profesional tratante / usuario registrador', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExtramurales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS de HCORDPRON ∈ {1..6} → Mapea a etiquetas: 1=Ordenado, 2/4=Completado, 3=Interpretado, 5=Anulado, 6=Sala programada; si ESTSERIPS de HCORDPROQ ∈ {1..4} → Mapea a: 1=Solicitado, 2=Sala Programada, 3=Cancelado, 4=Resultado Revisado; si ESTSERIPS de HCORDIMAG ∈ {1..7} → Mapea a: 1=Solicitado, 2=Estudio Realizado, 3=Imagen Procesada, 4=Estudio Interpretado, 5=Remitido, 6=Anulado, 7=Extramural; si ESTSERIPS de HCORDINTE ∈ {1..5} → Mapea a: 1=Solicitado, 2=Solicitud Enviada, 3=Interconsulta Realizada, 4=Extramural, 5=Anulado; si En la nota administrativa: variable [79]=''SI'' → ESTADO NOTA = ''SOLICITADO'' else Si [80]=''SI''→EN PROCESO; [81]=''SI''→COMPLETADO; [82]=''SI''→CANCELADO; si NUMINGRES de la nota es NULL → Usa el valor de la variable [116] como número de ingreso else Conserva NUMINGRES original; si Longitud de la fecha [118] sin ''/'' < 8 (día de un dígito) → Toma el mes desde posición 2 anteponiendo ''0'' else Toma el mes desde posición 3 (día de dos dígitos); si EXT.FECHACREACION IS NOT NULL → Usa la fecha real de creación de la nota else Asigna ''1800-01-01'' como FECHA REGISTRO NOTA; si Origen de la orden = HCORDINTE → Solo considera CUPS ''890476'' y ''890487'' (interconsultas extramurales) else Para HCORDPRON/HCORDPROQ/HCORDIMAG aplica el listado extenso de CUPS de procedimientos/imágenes extramurales', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExtramurales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPRON; dbo.HCORDPROQ; dbo.HCORDIMAG; dbo.HCORDINTE; dbo.NTXVARIABLES; dbo.NTADMINISTRATIVAS; dbo.NTNOTASADMINISTRATIVASC; dbo.NTNOTASADMINISTRATIVASD; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.INCUPSIPS; dbo.INPACIENT; CONTRACT.HealthAdministrator; Contract.ContractDescriptions; dbo.INDIAGNOS; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExtramurales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewExtramurales';
GO
