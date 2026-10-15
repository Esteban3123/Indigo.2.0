

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarPacientesAtencionFarmaceuticaValidar] 
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @UnidadFuncionalAux as varchar(10) = RTRIM(@UnidadFuncional)
	if @UnidadFuncionalAux IS NOT NULL AND LEN(@UnidadFuncionalAux) = 0 SET @UnidadFuncionalAux = NULL

	SELECT
		convert(bit,ISnull(A.MEDICAMENTOVALIDADO,0)) as VALIDADO,
		A.CODCONCEC as Row , 
		A.CODCENATE AS CodigoCentroAtencion, 
		A.CODCONCEC AS ConsecutivoFarmacia, 
		A.FECHAORDE AS FechaOrden, 
		A.IPCODPACI AS CodigoPaciente, 
		RTRIM(B.IPNOMCOMP) 
        AS NombrePaciente, 
		RTRIM(D.UFUDESCRI) AS UnidadFuncional, 
		A.ORDESTADO AS Estado, 
		RTRIM(C.NOMMEDICO) AS NombreMedico, 
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
		RTRIM( F.DESCCAMAS ) AS 'Cama',		
		RTRIM( H.CODDIAGNO ) + ' - ' + RTRIM( H.NOMDIAGNO ) AS 'Diagnostico'

	FROM
		dbo.HCFARMEPC AS A with(nolock)
		INNER JOIN dbo.IHBODEGAS as bo with(nolock) on a.CODBODEGA = bo.CODBODEGA 
		INNER JOIN dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI 
		INNER JOIN dbo.INPROFSAL AS C with(nolock) ON A.CODPROSAL = C.CODPROSAL 
		INNER JOIN dbo.INUNIFUNC AS D with(nolock) ON A.UFUCODIGO = D.UFUCODIGO
		INNER JOIN dbo.CHREGESTA AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES  AND E.REGESTADO =1
		INNER JOIN dbo.CHCAMASHO AS F  with (nolock) ON E.CODICAMAS = F.CODICAMAS AND A.UFUCODIGO = F.UFUCODIGO AND A.CODCENATE = F.CODCENATE
		INNER JOIN dbo.INDIAGNOP AS G  with (nolock) ON A.IPCODPACI = G.IPCODPACI AND A.NUMINGRES = G.NUMINGRES AND G.CODDIAPRI = 1
		INNER JOIN dbo.INDIAGNOS AS H  with (nolock) ON G.CODDIAGNO = H.CODDIAGNO

	WHERE
		A.CODCENATE=@CentroAtencion 
		AND (@UnidadFuncionalAux IS NULL OR (@UnidadFuncionalAux IS NOT NULL AND RTRIM(A.UFUCODIGO) = @UnidadFuncionalAux))  
		AND A.ORDESTADO = '1'

	ORDER BY F.DESCCAMAS ASC , A.IPCODPACI ASC

END