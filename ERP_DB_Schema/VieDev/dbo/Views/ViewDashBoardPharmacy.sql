





CREATE VIEW [dbo].[ViewDashBoardPharmacy]
AS
	SELECT	A.CODCONCEC as Row, 
			0 AS SelectOption, 
			A.CODCENATE AS CodigoCentroAtencion, 
			A.CODCONCEC AS ConsecutivoFarmacia, 
			A.FECHAORDE AS FechaOrden, 
			A.IPCODPACI AS CodigoPaciente, 
			RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
			RTRIM(D.UFUDESCRI) AS UnidadFuncional, 
			A.ORDESTADO AS Estado, 
			RTRIM(C.NOMMEDICO) AS NombreMedico, 
			dbo.GetProfessionType(C.TIPPROFES) Profesion, 
			concat(rtrim( bo.CODBODEGA),' - ',rtrim(bo.DESBODEGA)) AS Bodega, 
			A.NUMINGRES AS Ingreso, 
			A.UFUCODIGO, 
			COALESCE (NULLIF (A.CODCONCEP, ''), '') AS ConsecutivoPrescripcion, 
			COALESCE (NULLIF (A.CODCONCES, ''), '') AS ConsecutivoInsumos, 
			RTRIM(A.CODCENCOS) AS CentroCostos, 
			A.ORDTRANUE AS Tipo, 
			A.NUMEFOLIO AS Folio, 
			A.IDETIPHIS, 
			CAST('' AS bit) AS Impresion, 
			Case When A.MEDICAMENTOVALIDADO Is Null Or A.MEDICAMENTOVALIDADO = 0 Then Cast(0 As Bit) Else Cast(1 As Bit) End As Validado, 
			isnull(A.TIPOSOLICITUD,0) as TIPOSOLICITUD,
			ISNULL(cama.NUMCAMHOS, '') AS CodigoCama, 
			A.TIPOSOLICITUD1, 
			A.PERMITEEXTRA, 
			RTRIM(LTRIM(A.CODCENATE)) + ' - ' + RTRIM(LTRIM(cent.NOMCENATE)) CareCenterDescription,
			A.PatientDischarge,
			B.IPFECNACI BirthDay,
			IIF(A.MEDICAMENTO>0, 1,0) MEDICAMENTO,
			IIF(A.INSUMOS>0, 1,0) INSUMOS,
			IIF(A.MEDICAMENTO_INSUMO>0, 1,0) MEDICAMENTO_INSUMO
	FROM [dbo].[ViewPharmacy] AS A WITH (NOLOCK) 
	JOIN dbo.IHBODEGAS as bo WITH (NOLOCK) on a.CODBODEGA = bo.CODBODEGA 
	JOIN dbo.INPACIENT AS B WITH (NOLOCK) ON A.IPCODPACI = B.IPCODPACI 
	JOIN dbo.INPROFSAL AS C WITH (NOLOCK) ON A.CODPROSAL = C.CODPROSAL 
	JOIN dbo.INUNIFUNC AS D WITH (NOLOCK) ON A.UFUCODIGO = D.UFUCODIGO 
	JOIN dbo.ADCENATEN cent WITH(NOLOCK) on cent.CODCENATE = A.CODCENATE
	LEFT JOIN CHCAMASHO cama ON ISNULL(CASE WHEN A.CODCAMACT = 0 THEN '' ELSE CAST(A.CODCAMACT AS VARCHAR(15)) END, '') = cama.CODICAMAS
	WHERE A.HasQuantityRemaining = 1