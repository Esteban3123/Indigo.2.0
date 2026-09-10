
/*******************************************************************************************************************
Nombre: [Report].[ViewInventarioHistoricoPrescripcion]
Tipo:Vista
Observacion: Basado en el reporte de consulta de pacientes para mostrar la trazabilidad de estancias que an tenido en su estancia por la institución.

Profesional: Nilsson Miguel Galindo Lopez

Fecha:03-05-2022

_____________________________________________________________________________

Modificaciones
_____________________________________________________________________________

Version 2
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha: 07-09-2022
Ovservaciones:Se agregan los campos de la fecha del ingreso, ultimo estado y tipo de salida
--------------------------------------
Version 3
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha: 26-10-2023
Ovservaciones: Se agrega el numero de telefono y numero de celular
____________________________________________________________________________________
Version 4
Persona que modifico: Amira Gil Meneses
Fecha: 29/01/2024
Observaciones: Se adiciona el campo Grupo de Atención
***********************************************************************************************************************************/

CREATE VIEW [Report].[ViewHistoricoEstancia] as

WITH 
CTE_ULTIMA_CAMAS_OCUPADA
----CTE PARA IDENTIFICAR LA ULTIMA CAMA ACTIVA DE ESE INGRESO
AS
(
    SELECT 
	EGR.NUMINGRES,
	EGR.FECINIEST,EGR.FECFINEST ,EGR.CODICAMAS,CAM.NUMCAMHOS ,FUN.UFUDESCRI  ,FUN.UFUTIPUNI ,FUN.UFUCODIGO,'EGRESO                   ' AS TIPINGEST, EGR.CODTIPEST, 
	TIP.DESTIPEST, EGR.GENESTLIQ,EGR.ID
    FROM CHREGESTA EGR with (nolock)
    INNER JOIN
    (
       SELECT EGR.IPCODPACI ,EGR.NUMINGRES,MAX(EGR.ID) ID  FROM CHREGESTA EGR with (nolock)
	   GROUP BY EGR.IPCODPACI ,EGR.NUMINGRES
	) AS G ON G.ID =EGR.ID
    INNER JOIN CHCAMASHO AS CAM with (nolock) ON CAM.CODICAMAS =EGR.CODICAMAS
    INNER JOIN DBO.INUNIFUNC AS FUN with (nolock) ON CAM.UFUCODIGO =FUN.UFUCODIGO
	INNER JOIN CHTIPESTA TIP ON TIP.CODTIPEST=EGR.CODTIPEST 
	WHERE EGR.FECFINEST>'1989-01-01 00:00:00.000'
),
--Tipo de salida
CTE_DESTINOS AS
(
SELECT 
ROW_NUMBER ( )   
    OVER ( PARTITION BY NUMINGRES  order by NUMINGRES,FECHISPAC DESC) 'NUMERO',
NUMINGRES,
INDICAPAC
FROM HCHISPACA
),
--IN V2
CTE_DESTINO AS
(
select 
EGR.ID,
CASE H.INDICAPAC WHEN 8 THEN 'TRASLADAR A CIRUGIA'
				 WHEN 9 THEN 'HOSPITALIZACION EN CASA' 
				 WHEN 10 THEN 'REFERENCIA'
				 WHEN 11 THEN 'MORGE' 
				 WHEN 12 THEN 'SALIDA' 
				 WHEN 13 THEN 'SALIDA'
				 WHEN 15 THEN 'RETITO VOLUNTARIO' 
				 WHEN 16 THEN 'FUGA' END AS DESTINO 
FROM
CTE_ULTIMA_CAMAS_OCUPADA EGR INNER JOIN
CTE_DESTINOS H ON EGR.NUMINGRES=H.NUMINGRES AND H.NUMERO=1
)

--FN V2
SELECT
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
ROW_NUMBER() OVER(ORDER BY P.IPTIPODOC ASC) AS #,
A.IPCODPACI AS 'IDENTIFICACIÓN',
CASE P.IPTIPODOC WHEN 1 THEN 'CC'
				 WHEN 2 THEN 'CE'
				 WHEN 3 THEN 'TI'
				 WHEN 4 THEN 'RC' 
				 WHEN 5 THEN 'PA'
				 WHEN 6 THEN 'AS' 
				 WHEN 7 THEN 'MS'
				 WHEN 8 THEN 'NU'
				 when 9 then 'CN'
				 when 10 then 'CD'
				 when 11 then 'SC'
				 when 12 then 'PE' END AS 'TIPO DE IDENTIFICACION',
RTRIM(P.IPNOMCOMP) 'PACIENTE',
CAST(P.IPFECNACI AS DATE) AS 'FECHA DE NACIMIENTO',
FLOOR((CAST(CONVERT(VARCHAR(8), A.FECINIEST, 112) AS INT) - CAST(CONVERT(VARCHAR(8), P.IPFECNACI, 112) AS INT)) / 10000) AS 'EDAD AÑOS',
CASE P.IPSEXOPAC WHEN 2 THEN 'FEMENINO'
				 WHEN 1 THEN 'MASCULINO' END AS 'GENERO',
P.IPTELEFON AS TELEFONO,
P.IPTELMOVI AS CELULAR,
A.NUMINGRES AS [INGRESO],
E.NOMENTIDA AS [ENTIDAD PACIENTE],
GA.CODE + ' - ' + GA.NAME [GRUPO ATENCION],
(SELECT TOP 1 DIG.CODDIAGNO+' - '+DIA.NOMDIAGNO FROM DBO.INDIAGNOH DIG INNER JOIN DBO.INDIAGNOS DIA ON DIG.CODDIAGNO=DIA.CODDIAGNO WHERE A.NUMINGRES=DIG.NUMINGRES AND DIG.CODDIAPRI='1') AS [DIAGNOSTICO PRINCIPAL],
CASE IPTIPOPAC WHEN 1 THEN 'CONTRIBUTIVO'
			   WHEN 2 THEN 'SUBSIDIADO'
			   WHEN 3 THEN 'VINCULADO'
			   WHEN 4 THEN 'PARTICULAR'
			   ELSE 'Otro' END AS 'COBERTURA',
YEAR(A.FECINIEST)AS AÑO,
MONTH(A.FECINIEST)AS MES,
ING.IFECHAING AS [FECHA INGRESO],
A.FECINIEST AS 'FECHA INICIAL DE ESTANCIA',
CASE WHEN A.FECFINEST>'1989-01-01 00:00:00.000' THEN A.FECFINEST ELSE NULL END AS [FECHA FINAL ESTANCIA],
CASE WHEN A.FECFINEST>'1989-01-01 00:00:00.000' THEN DATEDIFF(DAY,A.FECINIEST,A.FECFINEST) 
	 ELSE NULL END AS [DIAS DE ESTANCIA],
CASE WHEN A.FECFINEST>'1989-01-01 00:00:00.000' THEN DATEDIFF(HOUR,A.FECINIEST,A.FECFINEST) 
	 ELSE NULL END AS [HORAS DE ESTANCIA],
RTRIM(B.CODICAMAS) + ' - ' + B.DESCCAMAS AS [CAMA], 
C.DESTIPEST AS [TIPO DE ESTANCIA], 
D.UFUCODIGO AS [CODIGO UNIDAD],
D.UFUDESCRI AS [UNIDAD FUNCIONAL],
CASE A.TIPINGEST WHEN 'NU' THEN 'NUEVA UNIDAD'
				 WHEN 'TC' THEN 'TRASLADO INTERNO DE CAMA' END AS [TIPO INGRESO ESTANCIA],
CO.TIPINGEST AS [ULTIMO ESTADO],
DST.DESTINO AS [TIPO DE SALIDA],
1 as 'CANTIDAD',
CAST(A.FECREGSIS AS DATE) AS [FECHA BUSQUEDA], 
YEAR(A.FECREGSIS) AS [AÑO FECHA BUSQUEDA], 
MONTH(A.FECREGSIS) AS [MES AÑO FECHA BUSQUEDA],
CASE MONTH(A.FECREGSIS)
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
	 WHEN 12 THEN 'DICIEMBRE' END AS 'MES NOMBRE FECHA BUSQUEDA', 
FORMAT(DAY(A.FECREGSIS), '00') AS 'DIA FECHA BUSQUEDA',
CONCAT(FORMAT(MONTH(A.FECREGSIS), '00') ,' - ', 
	   CASE MONTH(A.FECREGSIS) 
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
			WHEN 12 THEN 'DICIEMBRE'
		END) MES_LABEL_INGRESO,
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
DBO.CHREGESTA A INNER JOIN 
dbo.CHCAMASHO B ON A.CODICAMAS=B.CODICAMAS INNER JOIN 
dbo.CHTIPESTA C ON A.CODTIPEST=C.CODTIPEST INNER JOIN 
dbo.INUNIFUNC D ON B.UFUCODIGO=D.UFUCODIGO INNER JOIN 
dbo.INPACIENT P ON A.IPCODPACI=P.IPCODPACI INNER JOIN
dbo.ADINGRESO ING ON A.NUMINGRES=ING.NUMINGRES INNER JOIN
dbo.INENTIDAD E ON P.CODENTIDA=E.CODENTIDA LEFT JOIN --V2
CTE_ULTIMA_CAMAS_OCUPADA CO ON A.ID=CO.ID LEFT JOIN
CTE_DESTINO DST ON CO.ID=DST.ID --V2
INNER JOIN CONTRACT.CAREGROUP GA ON GA.Id = ING.GENCAREGROUP
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a consumo analítico (BI/reporting) que consolida la trazabilidad histórica de estancias hospitalarias por paciente. Cruza datos demográficos del paciente, episodio de ingreso, cama asignada, unidad funcional, tipo y duración de estancia (días y horas), entidad aseguradora, diagnóstico principal, grupo de atención y tipo de salida. Incluye campos temporales desagregados (año, mes, día) y el último estado de cama ocupada para facilitar análisis de ocupación y flujo hospitalario.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHistoricoEstancia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHistoricoEstancia';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de trazabilidad que consolida el histórico de estancias hospitalarias por paciente, integrando datos demográficos, ingreso, cama, unidad funcional, diagnóstico principal, grupo de atención, último estado y tipo de salida.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHistoricoEstancia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada estancia (CHREGESTA) debe tener cama (CHCAMASHO), tipo de estancia (CHTIPESTA), unidad funcional (INUNIFUNC), paciente (INPACIENT), ingreso (ADINGRESO), entidad (INENTIDAD) y grupo de atención (CONTRACT.CAREGROUP) válidos para aparecer en la vista (joins INNER).; Para identificar la última cama ocupada se requiere que la estancia tenga FECFINEST > ''1989-01-01'' (centinela de fecha no nula).; El paciente debe tener entidad asignada (CODENTIDA) y el ingreso debe tener grupo de atención (GENCAREGROUP).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHistoricoEstancia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El campo CANTIDAD siempre vale 1 (cada fila representa una estancia).; ID_COMPANY se obtiene siempre de DB_NAME() truncado a 9 caracteres.; ULT_ACTUAL se calcula con GETDATE() convertido a zona ''Pakistan Standard Time''.; EDAD AÑOS se calcula como floor((yyyymmdd_FECINIEST - yyyymmdd_IPFECNACI)/10000), basada en la fecha inicial de estancia, no en la fecha actual.; FECFINEST se considera nula/no establecida cuando es <= ''1989-01-01 00:00:00.000'' (valor centinela).; El TIPO DE SALIDA solo se obtiene de la última cama ocupada con FECFINEST > ''1989-01-01'' (egresos).; Los diagnósticos no principales se descartan (solo CODDIAPRI=''1'').', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHistoricoEstancia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión hospitalaria; Estancia hospitalaria; Cama hospitalaria; Unidad funcional; Tipo de estancia; Diagnóstico principal; Entidad/aseguradora; Cobertura (contributivo, subsidiado, vinculado, particular); Grupo de atención; Tipo de salida (traslado, referencia, morgue, retiro voluntario, fuga, hospitalización en casa); Traslado interno de cama; Tipo de documento de identificación; Días y horas de estancia', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHistoricoEstancia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewHistoricoEstancia: Devuelve una fila por cada registro de estancia (CHREGESTA) que tenga cama, tipo de estancia, unidad funcional, paciente, ingreso, entidad y grupo de atención asociados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHistoricoEstancia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si P.IPTIPODOC entre 1..12 → Mapea a etiquetas CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE respectivamente; si P.IPSEXOPAC = 1 / = 2 → Etiqueta ''MASCULINO'' / ''FEMENINO''; si IPTIPOPAC = 1..4 → Cobertura ''CONTRIBUTIVO'', ''SUBSIDIADO'', ''VINCULADO'', ''PARTICULAR'' else ''Otro''; si A.FECFINEST > ''1989-01-01 00:00:00.000'' → Se reportan FECHA FINAL ESTANCIA, DIAS DE ESTANCIA (DATEDIFF DAY) y HORAS DE ESTANCIA (DATEDIFF HOUR) else NULL en esos tres campos; si A.TIPINGEST = ''NU'' / ''TC'' → Etiqueta ''NUEVA UNIDAD'' / ''TRASLADO INTERNO DE CAMA''; si HCHISPACA.INDICAPAC en (8,9,10,11,12,13,15,16) → Determina TIPO DE SALIDA: 8=TRASLADAR A CIRUGIA, 9=HOSPITALIZACION EN CASA, 10=REFERENCIA, 11=MORGE, 12/13=SALIDA, 15=RETITO VOLUNTARIO, 16=FUGA; si Selección del DIAGNÓSTICO PRINCIPAL via subquery con DIG.CODDIAPRI=''1'' → Toma TOP 1 diagnóstico marcado como principal del ingreso; si Selección de última estancia por ingreso: MAX(EGR.ID) GROUP BY IPCODPACI, NUMINGRES en CTE_ULTIMA_CAMAS_OCUPADA → Identifica la fila más reciente de CHREGESTA por paciente/ingreso para ''ULTIMO ESTADO''; si Selección de destino: ROW_NUMBER PARTITION BY NUMINGRES ORDER BY FECHISPAC DESC, NUMERO=1 → Toma el último registro de HCHISPACA por ingreso para determinar tipo de salida', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHistoricoEstancia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.CHCAMASHO; dbo.CHTIPESTA; dbo.INUNIFUNC; dbo.INPACIENT; dbo.ADINGRESO; dbo.INENTIDAD; dbo.HCHISPACA; dbo.INDIAGNOH; dbo.INDIAGNOS; CONTRACT.CAREGROUP', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHistoricoEstancia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHistoricoEstancia';
GO
