

/*******************************************************************************************************************
Nombre: [Report].[ViewInterConsultationRequests]
Tipo:Vista
Observacion: Se crea de nuevo toda la Vista ordenes de interconsulta
Profesional:Nilsson Mieguel Galindo Lopez
Fecha:22-01-2023
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 2
Persona que modifico:Nilsson Miguel Galindo
Fecha:11-04-2024
Observaciones:Se agregan los campos de camas, el de la orden y el de la respuesta de la interconsultas.
--------------------------------------
Version 3
Persona que modifico:
Observación:
Fecha:
***********************************************************************************************************************************/

CREATE VIEW [Report].[ViewInterConsultationRequests] AS
	---****INTERCONSULTAS******

WITH 
CTE_GRUPO AS
(
	SELECT 
	CG.CODE AS CODGRUPO, 
	CE.CODE AS CUPS, 
	CASE PC.CONTRACTED WHEN 1 THEN 'SI' ELSE 'NO' END AS CONTRATADO,
	CASE PC.QUOTED WHEN 1 THEN 'SI' ELSE 'NO' END AS COTIZADO, 
	CECD.ID AS IDRELACION
	FROM 
	CONTRACT.CAREGROUP AS CG 
	INNER JOIN CONTRACT.PROCEDURETEMPLATE AS PT ON CG.PROCEDURETEMPLATEID = PT.ID
	INNER JOIN CONTRACT.PROCEDURECUPS AS PC  ON PT.ID = PC.PROCEDURESTEMPLATEID
	INNER JOIN CONTRACT.CUPSENTITY AS CE  ON PC.CUPSID = CE.ID
	LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS AS CECD  ON PC.CUPSENTITYCONTRACTDESCRIPTIONID = CECD.ID
	LEFT JOIN CONTRACT.CUPSSUBGROUP AS CSG  ON CE.CUPSSUBGROUPID = CSG.ID
),

CTE_ESTADIO AS (
	SELECT
	ROW_NUMBER ( )   
	OVER (PARTITION BY EST.NUMINGRES order by EST.NUMINGRES DESC) 'NUMERO',
	EST.NUMINGRES,
	CASE EST.ESTADIO WHEN 0 THEN 'ESTADIO CLÍNICO (EC) 0 (TUMOR IN SITU)'
					 WHEN 1 THEN 'EC I O 1'
					 WHEN 2 THEN 'EC IA O 1A'
					 WHEN 3 THEN 'EC IA1'
					 WHEN 4 THEN 'EC IA2'
					 WHEN 5 THEN 'EC IB O 1B'
					 WHEN 6 THEN 'EC IB1'
					 WHEN 7 THEN 'EC IB2'
					 WHEN 8 THEN 'EC IC O 1C'
					 WHEN 9 THEN 'EC IS O 1S'
					 WHEN 10 THEN 'EC II O 2'
					 WHEN 11 THEN 'EC IIA O 2A'
					 WHEN 12 THEN 'EC IIA1'
					 WHEN 13 THEN 'EC IIA2'
					 WHEN 14 THEN 'EC IIB O 2B'
					 WHEN 15 THEN 'EC IIC O 2C'
					 WHEN 16 THEN 'EC III O 3'
					 WHEN 17 THEN 'EC IIIA O 3A'
					 WHEN 18 THEN 'EC IIIB O 3B'
					 WHEN 19 THEN 'EC IIIC O 3C'
					 WHEN 20 THEN 'EC IV O 4'
					 WHEN 21 THEN 'EC IVA O 4A'
					 WHEN 22 THEN 'EC IVB O 4B'
					 WHEN 23 THEN 'EC IVC O 4C'
					 WHEN 24 THEN 'EC 4S (PARA NEUROBLASTOMA)'
					 WHEN 25 THEN 'EC  V O 5'
					 WHEN 26 THEN 'EC ESTADIO IAB'
					 WHEN 55 THEN 'PERSONA CON ASEGURAMIENTO (RÉGIMEN SUBSIDIADO O CONTRIBUTIVO Y QUE NO SON PPNA) QUE RECIBIÓ SERVICIOS DE SALUD POR PARTE DEL ENTE TERRITORIALDURANTE EL PERIODO DE REPORTE'
					 WHEN 93 THEN 'SIN INFORMACIÓN DE ESTADIFICACIÓN EN HISTORIA CLÍNICA'
					 WHEN 98 THEN 'NO APLICA (ES CÁNCER DE PIEL BASOCELULAR, ES CÁNCER HEMATOLÓGICO O ES CÁNCER EN SNC, EXCEPTO NEUROBLASTOMA)'
					 WHEN 99 THEN 'DESCONOCIDO, EL DATO DE ESTA VARIABLE NO SE ENCUENTRA DESCRITO EN LOS SOPORTES CLÍNICOS' ELSE ''END ESTADIO
	FROM DBO.INDIAGNOH AS EST 
	WHERE ESTADIO IS NOT NULL
)

SELECT
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
RTRIM(E.NOMCENATE) [CENTRO ATENCION],
CASE I.IPTIPODOC WHEN '1' THEN 'CC'
				 WHEN '2' THEN 'CE'
				 WHEN '3' THEN 'TI'
				 WHEN '4' THEN 'RC'
				 WHEN '5' THEN 'PA'
				 WHEN '6' THEN 'AS'
				 WHEN '7' THEN 'MS'
				 WHEN '8' THEN 'NU'
				 WHEN '9' THEN 'NV'
				 WHEN '10' THEN 'CD'
				 WHEN '11' THEN 'SC'
				 WHEN '12' THEN 'PE' 
				 WHEN '13' THEN 'PT'
				 WHEN '14' THEN 'DE'
				 WHEN '15' THEN 'SI' END AS [TIPO IDENTIFICACION], 
A.IPCODPACI AS [IDENTIFICACION], 
I.IPNOMCOMP AS [PACIENTE], 
CAST (I.IPFECNACI AS DATE) [FECHA DE NACIMIENTO],
cast((datediff(m, I.IPFECNACI, GETDATE())/12) as varchar) + ' Años ' + cast((DATEDIFF(m, I.IPFECNACI, GETDATE())%12) as varchar) + ' Meses' as EDAD,
CASE I.IPSEXOPAC WHEN '1' THEN 'MASCULINO' ELSE 'FEMENINO' END AS [SEXO],
I.IPTELEFON AS [TELEFONO FIJO], 
I.IPTELMOVI AS [TELEFONO MOVIL], 
I.IPDIRECCI AS DIRECCION,
EA.HealthEntityCode AS [CODIGO EPS/ENTIDAD TERRITORIAL],
EA.CODE + ' - ' + EA.NAME AS [ENTIDAD ADMINISTRADORA], 
GA.CODE [CODIGO GRUPO ATENCION], 
GA.CODE + ' - ' + GA.NAME [GRUPO ATENCION],
CASE GA.LIQUIDATIONTYPE WHEN 1 THEN 'PAGO POR SERVICIOS'
						WHEN 2 THEN 'PGP'
						WHEN 3 THEN 'FACTURA GLOBAL'
						WHEN 4 THEN 'CAPITACION GLOBAL'
						WHEN 5 THEN 'CONTROL'END [TIPO CONTRATO], 
A.CODSERIPS [CODIGO CUPS],
RTRIM(B.DESSERIPS) AS [DESCRIPCION SERVICIO], 
CG.CODE + '-' + CG.NAME [GRUPO], 
CSG.CODE + '-' + CSG.NAME [SUBGRUPO], 
ISNULL(CD.CODE + ' - ' + CD.NAME, '') [DESCRIPCION RELACIONADA], 
ESTA.ESTADIO,
IIF(G.CODGRUPO IS NULL, 'NO', 'SI') AS CUBIERTO, 
ISNULL(G.CONTRATADO, 'NO') AS CONTRATADO, 
ISNULL(G.COTIZADO, 'NO') AS COTIZADO, 
CASE WHEN MANEXTPRO = 0 THEN 'HOSPITALARIO'
	 ELSE 'AMBULATORIO' END AS [TIPO SOLICITUD], 
'INTER CONSULTA' [TIPO ORDEN], 
RTRIM(A.UFUCODIGO) + ' - ' + D.UFUDESCRI AS [UNIDAD FUNCIONAL SOLICITUD], 
CAMO.CODICAMAS AS [CAMA SOLICITUD],
A.NUMINGRES AS [INGRESO],
A.NUMEFOLIO AS [FOLIO DE ORDEN], 
CAST(A.FECORDMED AS DATE) AS [FECHA SOLICITUD ORDEN],
CAST(CAST(A.FECORDMED AS TIME) AS VARCHAR(8)) AS [HORA SOLICITUD ORDEN],
CASE A.ESTSERIPS WHEN 1 THEN 'Solicitado'
				 WHEN 2 THEN 'Solicitud Enviada'
				 WHEN 3 THEN 'Iinterconsulta Realizada'
				 WHEN 4 THEN 'Solicitado Extramural'
				 WHEN 5 THEN 'Anulado'
				 WHEN 6 THEN 'Pendiente Verificación especialista' END AS ESTADO,
A.CANSERIPS AS CANTIDAD, 
PRO.CODPROSAL [CODIGO PROFESIONAL ORDENAMIENTO],
PRO.NOMMEDICO AS [PROFESIONAL ORDENAMIENTO], 
ESPMED.DESESPECI AS [ESPECIALIDAD ORDENAMIENTO], 
A.OBSSERIPS AS [OBSERVACION ORDENAMIENTO],
DIAG.CODDIAGNO AS [CODIGO DX PRINCIPAL ORDENAMIENTO], 
DIAG.NOMDIAGNO AS [DX PRINCIPAL ORDENAMIENTO],
A.FECHAINT AS [FECHA INTERCONSULTA],
CAST (CAST(A.FECHAINT AS TIME) AS VARCHAR(8)) AS [HORA INTERCONSULTA],
A.CODPROINT AS [CODIGO PROFESIONAL RESPUESTA INTERCONSULTA],
PROI.NOMMEDICO AS [PROFESIONAL RESPUESTA INTERCONSULTA],
ESI.DESESPECI AS [EPECIALIDAD RESPUESTA INTERCONSULTA],
A.NUMFOLINT AS [FOLIO DE INTERCONSULTA],
CAMR.CODICAMAS AS [CAMA RESPUESTA INTERCONSULTA],
CONVERT(VARCHAR,DATEDIFF(MINUTE,A.FECORDMED,A.FECHAINT)/60)+','+REPLACE(CONVERT(VARCHAR,CONVERT(INT,ROUND((DATEDIFF(MINUTE,A.FECORDMED,A.FECHAINT)%60)/0.60,0))),'-','') AS [FECHA ORDEN VS FECHA INTERCONSULTA EN HORAS],
CAST(A.FECORDMED AS DATE) [FECHA BUSQUEDA],
 YEAR(A.FECORDMED) AS [AÑO FECHA BUSQUEDA], 
 MONTH(A.FECORDMED) AS [MES FECHA BUSQUEDA],
 CASE MONTH(A.FECORDMED) WHEN 1 THEN 'ENERO'
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
						 WHEN 12 THEN 'DICIEMBRE' END AS [MES NOMBRE FECHA BUSQUEDA], 
 DAY(A.FECORDMED) AS [DIA FECHA BUSQUEDA],
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
DBO.HCORDINTE A 
INNER JOIN DBO.ADINGRESO AS ING ON A.NUMINGRES=ING.NUMINGRES
INNER JOIN DBO.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS
INNER JOIN DBO.INPACIENT I ON A.IPCODPACI=I.IPCODPACI
INNER JOIN DBO.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
INNER JOIN DBO.ADCENATEN E ON A.CODCENATE=E.CODCENATE
INNER JOIN CONTRACT.CUPSENTITY AS CUPS ON B.CODSERIPS=CUPS.CODE
INNER JOIN CONTRACT.CUPSSUBGROUP AS CSG ON CUPS.CUPSSUBGROUPID=CSG.ID
INNER JOIN CONTRACT.CUPSGROUP AS CG ON CSG.CUPSGROUPID=CG.ID
INNER JOIN DBO.HCHISPACA HIS ON A.NUMINGRES=HIS.NUMINGRES AND A.NUMEFOLIO=HIS.NUMEFOLIO
INNER JOIN DBO.INPROFSAL PRO ON HIS.CODPROSAL=PRO.CODPROSAL
INNER JOIN DBO.INESPECIA AS ESPMED ON HIS.CODESPTRA=ESPMED.CODESPECI
INNER JOIN DBO.INDIAGNOS AS DIAG ON HIS.CODDIAGNO=DIAG.CODDIAGNO
INNER JOIN CONTRACT.CAREGROUP AS GA ON ING.GENCAREGROUP=GA.ID
INNER JOIN CONTRACT.HEALTHADMINISTRATOR AS EA ON ING.GENCONENTITY=EA.ID
INNER JOIN dbo.INESPECIA ESI ON A.CODESPECI=ESI.CODESPECI
LEFT JOIN dbo.INPROFSAL PROI ON A.CODPROINT=PROI.CODPROSAL
LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS AS CECD ON A.IDDESCRIPCIONRELACIONADA=CECD.ID
LEFT JOIN CONTRACT.CONTRACTDESCRIPTIONS AS CD  ON CECD.CONTRACTDESCRIPTIONID=CD.ID 
LEFT JOIN CTE_GRUPO AS G ON G.CODGRUPO=GA.CODE AND G.CUPS = A.CODSERIPS AND A.IDDESCRIPCIONRELACIONADA = G.IDRELACION 
LEFT JOIN CTE_ESTADIO ESTA ON A.NUMINGRES=ESTA.NUMINGRES AND ESTA.NUMERO=1
LEFT JOIN dbo.HCREGEST CAMO ON A.NUMINGRES=CAMO.NUMINGRES AND A.NUMEFOLIO=CAMO.NUMEFOLIO
LEFT JOIN dbo.HCREGEST CAMR ON A.NUMINGRES=CAMR.NUMINGRES AND A.NUMFOLINT=CAMR.NUMEFOLIO
--WHERE A.IPCODPACI='1010175618'
--50
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a consumidores analíticos y de auditoría que consolida las órdenes de interconsulta registradas en `HCORDINTE`, cruzando datos del paciente, ingreso hospitalario, profesional ordenador y profesional respondedor, junto con el procedimiento CUPS solicitado y su cobertura contractual. Incluye tiempos de respuesta (diferencia en horas entre fecha de orden y fecha de interconsulta), cama de solicitud y cama de respuesta, estado oncológico (estadio clínico), estado de la orden y campos temporales para filtros por año/mes/día.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInterConsultationRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInterConsultationRequests';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa una orden de interconsulta (HCORDINTE) con su ingreso, paciente, CUPS, profesional y diagnóstico obligatorios (joins INNER).; Solo aparece un estadio clínico por ingreso (NUMERO=1 del ROW_NUMBER por NUMINGRES).; El indicador CUBIERTO es ''SI'' únicamente si existe relación grupo-atención + CUPS + descripción relacionada en el contrato; en caso contrario es ''NO''.; CONTRATADO y COTIZADO toman ''NO'' por defecto cuando no hay match con CTE_GRUPO.; El TIPO ORDEN siempre se reporta como ''INTER CONSULTA'' (literal fijo).; ID_COMPANY se obtiene del nombre de la base de datos actual (DB_NAME()) truncado a 9 caracteres.; ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; El cálculo de tiempo entre orden e interconsulta se expresa como horas,minutos derivados de DATEDIFF en minutos entre FECORDMED y FECHAINT.; La cama de la solicitud se obtiene con NUMEFOLIO original y la cama de la respuesta con NUMFOLINT, ambas contra HCREGEST.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInterConsultationRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Interconsulta; Orden médica; Paciente; Ingreso hospitalario; CUPS; Grupo y subgrupo CUPS; Contrato / Grupo de atención; Entidad administradora (EPS); Tipo de liquidación contractual (PGP, capitación, factura global); Diagnóstico principal; Estadio clínico oncológico; Profesional de la salud / Especialidad; Unidad funcional; Cama hospitalaria; Centro de atención; Tipo de solicitud (hospitalario/ambulatorio); Cobertura contractual del CUPS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInterConsultationRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PC.CONTRACTED = 1 (en CTE_GRUPO) → Marca CONTRATADO=''SI'' else CONTRATADO=''NO''; si PC.QUOTED = 1 (en CTE_GRUPO) → Marca COTIZADO=''SI'' else COTIZADO=''NO''; si G.CODGRUPO IS NULL tras el LEFT JOIN con CTE_GRUPO usando CODGRUPO=GA.CODE, CUPS=CODSERIPS y IDRELACION=IDDESCRIPCIONRELACIONADA → CUBIERTO=''NO'' (el CUPS no está cubierto en el grupo de atención del ingreso) else CUBIERTO=''SI''; si HCORDINTE.MANEXTPRO = 0 → TIPO SOLICITUD = ''HOSPITALARIO'' else TIPO SOLICITUD = ''AMBULATORIO''; si HCORDINTE.ESTSERIPS in (1..6) → Mapea a los estados: 1=Solicitado, 2=Solicitud Enviada, 3=Interconsulta Realizada, 4=Solicitado Extramural, 5=Anulado, 6=Pendiente Verificación especialista; si INDIAGNOH.ESTADIO IS NOT NULL (en CTE_ESTADIO) → Se incluye el registro y se traduce el código numérico de estadio clínico oncológico a su descripción textual (EC 0..EC V, 4S, IAB, valores 55/93/98/99 con significados especiales); si ROW_NUMBER() OVER (PARTITION BY NUMINGRES ORDER BY NUMINGRES DESC) = 1 al unir CTE_ESTADIO → Solo se toma un único registro de estadio por ingreso (el primero según el ordenamiento); si GA.LIQUIDATIONTYPE in (1..5) → Mapea TIPO CONTRATO: 1=Pago por servicios, 2=PGP, 3=Factura global, 4=Capitación global, 5=Control; si I.IPTIPODOC en ''1''..''15'' → Traduce el tipo de documento a las siglas estandarizadas (CC, CE, TI, RC, PA, AS, MS, NU, NV, CD, SC, PE, PT, DE, SI); si I.IPSEXOPAC = ''1'' → SEXO=''MASCULINO'' else SEXO=''FEMENINO'' (cualquier otro valor se asume femenino)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInterConsultationRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'CONTRACT.CAREGROUP; CONTRACT.PROCEDURETEMPLATE; CONTRACT.PROCEDURECUPS; CONTRACT.CUPSENTITY; CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS; CONTRACT.CUPSSUBGROUP; CONTRACT.CUPSGROUP; CONTRACT.HEALTHADMINISTRATOR; CONTRACT.CONTRACTDESCRIPTIONS; DBO.INDIAGNOH; DBO.HCORDINTE; DBO.ADINGRESO; DBO.INCUPSIPS; DBO.INPACIENT; DBO.INUNIFUNC; DBO.ADCENATEN; DBO.HCHISPACA; DBO.INPROFSAL; DBO.INESPECIA; DBO.INDIAGNOS; DBO.HCREGEST', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInterConsultationRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInterConsultationRequests';
GO
