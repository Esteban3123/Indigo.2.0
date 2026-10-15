CREATE PROCEDURE [dbo].[SP_HC_ListarMedicamentosMezclasyLiquidos]
(
@Paciente Varchar(25),
@Ingreso  Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	-------------------	   	
	with Tabla
	as
	(
			SELECT distinct
				Cast(0 As Bit) as 'NutricionParenteral',
				'' as 'LEYENDA',
				'Medicamentos' As 'Agrupador',
				CAST (1 As INT) As 'TIPO',
				CAST (0 AS Numeric ) As 'IDMEZCLAS',
				J.FECHISPAC As 'Fecha_Orden',
				RTRIM(A.CODPRODUC) As 'Codigo', 
				RTRIM(E.DESPRODUC) As 'Medicamento',
				RTRIM(CASE WHEN FORMAPRESCRIBE IS NOT NULL THEN DESADMINI ELSE CASE WHEN DOSISPRFN IS NULL THEN DESADMINI WHEN DURACIDOS='Dosis Unica' THEN RTRIM(CAST(DOSISPRFN AS CHAR)) + ' ' + RTRIM(CAST(ABRUNIMED AS CHAR)) + ' Dosis Unica ' + RTRIM(H.DESVIAADM) ELSE  RTRIM(CAST(DOSISPRFN AS CHAR)) + ' ' + RTRIM(CAST(ABRUNIMED AS CHAR)) + ' Cada ' + RTRIM(CAST(FRECUENCI AS CHAR)) + CASE UNIFRECUE WHEN '1' THEN 'M ' WHEN '2' THEN 'H ' WHEN '3' THEN 'D ' END + RTRIM(H.DESVIAADM) END END) As 'Administracion',
				RTRIM(A.DURACIDOS) As 'Duracion',
				A.CANPEDPRO As 'Cantidad', 
				Rtrim(INDAPLMED) AS 'Observaciones',
				CAST(0 AS BIT) AS 'SelActivaMedicamentos',
				RTRIM(CODDCIMED) AS 'CODDCIMED',
				TOTPROUNI AS 'Concentracion',
				CAST(A.PREESTADO AS INT) AS 'Estado',
				MANEXTPRO As 'MANEXTPRO',
				FORMUMANU As 'FORMUMANU',
				RTRIM(B.NOMMEDICO) AS 'Medico',
				RTRIM(D.UFUDESCRI) + ' - '  + RTRIM(C.NOMCENATE) AS 'Unidad',
				A.IDETIPHIS as 'Tipo Historia',
				A.NUMEFOLIO AS 'Numero Folio',
				'Folio: ' + RTRIM(NUMFOLSUS) + ' - ' + RTRIM(MOTSUSMED) AS 'Motivo Suspension',
				CAST(0 AS INT ) AS 'IDHCNOSERFOTROMED',
				NULL AS 'FECHA_OTROSMEDICAMENTOS',
				NULL AS 'USUARIO_OTROSMEDICAMENTOS',
				CASE WHEN A.PREESTADO IN ('1','6') THEN DATEDIFF(day,A.FECINIDOS,[Common].[GETDATE]()) WHEN A.PREESTADO IN (2,3,4,7) AND A.FECFINDOS IS NULL THEN DATEDIFF(day,A.FECINIDOS,[Common].[GETDATE]())  WHEN A.PREESTADO IN (2,3,4,7) THEN DATEDIFF(day,A.FECINIDOS,A.FECFINDOS) ELSE DATEDIFF(day,A.FECINIDOS,[Common].[GETDATE]()) END AS DiasTranscurridos,
				RTRIM(diag.CODDIAGNO) +' - '+ RTRIM(diag.NOMDIAGNO) As 'Diagnostico',
				CAST(0 AS int) AS DosisUnitaria,
				CAST(0 AS bit) AS Verificado
		FROM
				dbo.HCPRESCRA As A with(nolock)
				INNER JOIN dbo.INPROFSAL As B with(nolock) ON A.CODPROSAL=B.CODPROSAL 
				INNER JOIN dbo.ADcenaten As C with(nolock) ON A.CODCENATE=C.codcenate 
				INNER JOIN dbo.INUNIFUNC As D with(nolock) ON A.UFUCODIGO=D.UFUCODIGO
				INNER JOIN dbo.IHLISTPRO As E with(nolock) ON A.CODPRODUC=E.CODPRODUC 
				INNER JOIN dbo.INDIAGNOS As F with(nolock) ON A.CODDIAGNO = F.CODDIAGNO 
				LEFT OUTER JOIN dbo.INUNIMEDI As G with(nolock) ON A.CODUNIMFN = G.CODUNIMED 
				INNER JOIN dbo.HCVIAADMI As H with(nolock) ON A.CODVIAADM = H.CODVIAADM
				INNER JOIN dbo.IHFORMEDI As I with(nolock) ON A.CODFORMED = I.CODFORMED
				INNER JOIN dbo.HCHISPACA As J with(nolock) ON A.NUMEFOLIO=J.NUMEFOLIO AND A.IPCODPACI=J.IPCODPACI 
				LEFT OUTER JOIN dbo.INESPECIA As N with(nolock) ON J.CODESPTRA=N.CODESPECI 
				LEFT JOIN dbo.HCPRESCRC As V with(nolock) ON A.CODCONCEC= V.CODCONCEC 
				INNER JOIN INDIAGNOS diag ON diag.CODDIAGNO = J.CODDIAGNO
			WHERE 
				A.IPCODPACI= @Paciente 
				AND A.NUMINGRES= @Ingreso
				AND A.PREESTADO IN (1,6) 
UNION ALL
			SELECT  distinct
				Cast(0 As Bit) as 'NutricionParenteral',
				'' as 'LEYENDA',
				'Mezclas y Liquidos' As 'Agrupador',
				CAST (2 As INT) As 'TIPO',
				cab.CODCONCEC As 'IDMEZCLAS',
				cab.FECHAORDE As 'Fecha_Orden',
				RTRIM(pro.CODPRODUC) As 'Codigo',
				RTRIM(pro.DESPRODUC) As 'Medicamento',
				RTRIM(admi.ADMMEZLIQ) As 'Administracion',
				RTRIM(admi.TIPODURACION) As 'Duracion',
				med.CANPROCAL As 'Cantidad',
				rtrim(admi.MEZLIQPAC)  AS 'Observaciones',
				CAST(0 AS BIT) AS 'SelActivaMedicamentos',
				RTRIM(pro.CODDCIMED) AS 'CODDCIMED' ,
				CAST(0 AS Numeric) AS 'Concentracion' ,
				CASE admi.PREESTADO WHEN 1 THEN 1 WHEN 5 THEN 6 END AS 'Estado',
				CAST(0 AS BIT) As 'MANEXTPRO',
				CAST(0 AS BIT) As 'FORMUMANU',
				RTRIM(C.NOMMEDICO) AS 'Medico',
				RTRIM(D.UFUDESCRI) + ' - '  + RTRIM(E.NOMCENATE) AS 'Unidad',
				admi.IDETIPHIS as 'Tipo Historia',
				admi.NUMEFOLIO AS 'Numero Folio',
				'Folio: ' + RTRIM(admi.NUMFOLSUS) + ' - ' + RTRIM(admi.MOTSUSMED) AS 'Motivo Suspension', 
				CAST(0 AS INT ) AS 'IDHCNOSERFOTROMED',
				NULL AS 'FECHA_OTROSMEDICAMENTOS',
				NULL AS 'USUARIO_OTROSMEDICAMENTOS',
				DATEDIFF(day, admi.FECHAFINC, [Common].[GETDATE]()) AS DiasTranscurridos,
				RTRIM(diag.CODDIAGNO) +' - '+ RTRIM(diag.NOMDIAGNO) As 'Diagnostico',
				CAST(0 AS int) AS DosisUnitaria,
				CAST(0 AS bit) AS Verificado
			FROM 
				dbo.HCINFLIQC as cab with(nolock)
				INNER JOIN dbo.HCINFCONC as med with(nolock) ON cab.CODCONCEC = med.CODCONCEC 
				INNER JOIN dbo.IHLISTPRO as pro with(nolock) ON med.CODPRODUC = pro.CODPRODUC  
				INNER JOIN dbo.HCINFLIQA as admi with(nolock) ON cab.CODCONCEC = admi.CODCONCEC 
				INNER JOIN dbo.INPROFSAL as C with(nolock) ON admi.CODPROSAL = C.CODPROSAL
				INNER JOIN dbo.INUNIFUNC as D with(nolock) ON admi.UFUCODIGO = D.UFUCODIGO 
				INNER JOIN dbo.ADcenaten as E with(nolock) ON admi.CODCENATE = E.CODCENATE
				INNER JOIN INDIAGNOS diag ON diag.CODDIAGNO = admi.CODDIAGNO
			WHERE 
				 cab.IPCODPACI = @Paciente
				 AND cab.NUMINGRES = @Ingreso
				 AND admi.PREESTADO IN (1,5)
UNION ALL
			SELECT distinct
				Cast(0 As Bit) as 'NutricionParenteral',
				'' as 'LEYENDA',	
				'Mezclas y Liquidos' As 'Agrupador',
				CAST (2 As INT) As 'TIPO',
				cab.CODCONCEC As 'IDMEZCLAS',
				cab.FECHAORDE As 'Fecha_Orden',
				RTRIM(pro.CODPRODUC) As 'Codigo',
				RTRIM(pro.DESPRODUC) As 'Medicamento',
				RTRIM(admi.ADMMEZLIQ) As 'Administracion',
				RTRIM(admi.TIPODURACION) As 'Duracion', 
				dil.CANPROCAL As 'Cantidad',
				rtrim(admi.MEZLIQPAC)  AS 'Observaciones',
				CAST(0 AS BIT) AS 'SelActivaMedicamentos',
				RTRIM(pro.CODDCIMED) AS 'CODDCIMED' ,
				CAST(0 AS Numeric) AS 'Concentracion' ,
				CASE admi.PREESTADO WHEN 1 THEN 1 WHEN 5 THEN 6 END AS 'Estado',
				CAST(0 AS BIT) As 'MANEXTPRO',
				CAST(0 AS BIT) As 'FORMUMANU', 
				RTRIM(C.NOMMEDICO) AS 'Medico', 
				RTRIM(D.UFUDESCRI) + ' - '  + RTRIM(E.NOMCENATE) AS 'Unidad',
				admi.IDETIPHIS as 'Tipo Historia', 
				admi.NUMEFOLIO AS 'Numero Folio', 
				'Folio: ' + RTRIM(admi.NUMFOLSUS) + ' - ' + RTRIM(admi.MOTSUSMED) AS 'Motivo Suspension' ,
				CAST(0 AS INT ) AS 'IDHCNOSERFOTROMED',
				NULL AS 'FECHA_OTROSMEDICAMENTOS',
				NULL AS 'USUARIO_OTROSMEDICAMENTOS',
				DATEDIFF(day, admi.FECHAFINC, [Common].[GETDATE]()) AS DiasTranscurridos,
				RTRIM(diag.CODDIAGNO) +' - '+ RTRIM(diag.NOMDIAGNO) As 'Diagnostico',
				CAST(0 AS int) AS DosisUnitaria,
				CAST(0 AS bit) AS Verificado
			FROM 
				dbo.HCINFLIQC As cab with(nolock) 
				INNER JOIN dbo.HCINFLIQD As dil with(nolock) ON cab.CODCONCEC = dil.CODCONCEC 
				INNER JOIN dbo.IHLISTPRO As pro with(nolock) ON dil.CODPRODUC = pro.CODPRODUC 
				INNER JOIN dbo.HCINFLIQA As admi with(nolock) ON cab.CODCONCEC = admi.CODCONCEC
				INNER JOIN dbo.INPROFSAL as C with(nolock) ON admi.CODPROSAL = C.CODPROSAL
				INNER JOIN dbo.INUNIFUNC as D with(nolock) ON admi.UFUCODIGO = D.UFUCODIGO 
				INNER JOIN dbo.ADcenaten as E with(nolock) ON admi.CODCENATE = E.CODCENATE
				INNER JOIN INDIAGNOS diag ON diag.CODDIAGNO = admi.CODDIAGNO
		   WHERE 
				 cab.IPCODPACI = @Paciente
				 AND cab.NUMINGRES = @Ingreso
				 AND admi.PREESTADO IN (1,5)
UNION ALL				 
					SELECT  distinct
						Cast(1 As Bit) as 'NutricionParenteral',
						'' as 'LEYENDA',
						'Mezclas y Liquidos' AS 'Agrupador',
						CAST (100 As INT) As 'TIPO',
						A.ID AS 'IDMEZCLAS',
						A.FECHAORDEN  As 'Fecha_Orden',
						'' AS 'Codigo',
						'NPT' +' - '+ RTRIM(b.NAME) As 'Medicamento',
						'Administrar Continuamente ' + CONVERT(varchar(10), a.VOLUTOTAL) + ' ml ' + 'en infusión continua a ' + CONVERT(varchar(10), a.VELINFUSION) + ' ml/hora por ' + CONVERT(varchar(10),a.TEMPOADMIN) + ' Horas'  AS 'Administracion',
						'Dosis Unica' as 'Duracion',
						1 as 'Cantidad',
						'NPT' AS 'Observaciones',
						CAST(0 AS BIT) AS 'SelActivaMedicamentos',
						'0' AS 'CODDCIMED',
						0 AS 'Concentracion',
						0 AS 'Estado',
						0 As 'MANEXTPRO',
						0 As 'FORMUMANU',
						RTRIM(prof.NOMMEDICO) AS 'Medico',
						RTRIM(func.UFUDESCRI) + ' - '  + RTRIM(cent.NOMCENATE) AS 'Unidad',
						his.IDETIPHIS as 'Tipo Historia',
						his.NUMEFOLIO AS 'Numero Folio',
						'' AS 'Motivo Suspension',
						CAST(0 AS INT ) AS 'IDHCNOSERFOTROMED',
						NULL AS 'FECHA_OTROSMEDICAMENTOS',
						NULL AS 'USUARIO_OTROSMEDICAMENTOS',
						DATEDIFF(day, a.FECHAORDEN, [Common].[GETDATE]()) AS DiasTranscurridos,
						RTRIM(diag.CODDIAGNO) +' - '+ RTRIM(diag.NOMDIAGNO) As 'Diagnostico',
						CAST(0 AS int) AS DosisUnitaria,
						CAST(0 AS bit) AS Verificado
				FROM HCNUTPAREC AS A --Tabla cabecera de la Orden medica de nutriciones
					INNER JOIN HCHISPACA his ON his.ID = A.IDHCHISPACA 
					INNER JOIN INDIAGNOS diag ON diag.CODDIAGNO = his.CODDIAGNO
					INNER JOIN INPROFSAL prof ON his.CODPROSAL = prof.CODPROSAL
					INNER JOIN INUNIFUNC func ON func.UFUCODIGO = his.UFUCODIGO 
					INNER JOIN ADCENATEN cent ON cent.CODCENATE = his.CODCENATE
					INNER JOIN HCPARNUTC B ON A.IDHCPARNUTC = B.ID --Parametrizacion de Nutrciones
				WHERE 
					 A.IPCODPACI = @Paciente
					 AND A.NUMINGRES = @Ingreso
					 AND A.STATUS IN (1)
	 )	
	 SELECT	distinct		 
			Z.*
			,CASE WHEN inter.CODPRODUA IS NOT NULL THEN Cast(1 As int) ELSE Cast(0 As int) END AS 'Interaccion'			
	 FROM Tabla As Z
			left outer join dbo.HCINTEMED As inter ON inter.CODPRODUA in (Z.Codigo, Z.CODDCIMED) OR inter.CODPRODUB in (Z.Codigo, Z.CODDCIMED)
	Group By 
			Z.NutricionParenteral, Z.LEYENDA, Z.Administracion, Z.Agrupador, Z.Cantidad, Z.CODDCIMED, Z.Codigo, Z.Concentracion, Z.Duracion, Z.Estado, Z.Fecha_Orden,
			Z.FORMUMANU, Z.IDMEZCLAS, Z.MANEXTPRO, Z.Medicamento, Z.Medico, Z.[Motivo Suspension] , Z.[Numero Folio],
			Z.Observaciones, Z.SelActivaMedicamentos, Z.TIPO, Z.[Tipo Historia], Z.Unidad,
			Z.IDHCNOSERFOTROMED, Z.FECHA_OTROSMEDICAMENTOS, Z.USUARIO_OTROSMEDICAMENTOS,  z.DiasTranscurridos , Z.Diagnostico,z.DosisUnitaria ,z.Verificado,
			inter.CODPRODUA
	ORDER BY Z.TIPO ASC, Z.Fecha_Orden ASC, Z.Codigo ASC
END