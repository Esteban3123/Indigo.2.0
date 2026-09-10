

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes con órdenes farmacéuticas pendientes de validación en un centro de atención y, opcionalmente, filtrando por unidad funcional. Para cada paciente actualmente hospitalizado (con estado de estancia activo), consolida información de la orden médica de despacho farmacéutico, datos del paciente, nombre del médico prescriptor, bodega asignada, cama actual y diagnóstico principal (CIE-10). Se utiliza en el módulo de atención farmacéutica para que el farmacéutico revise y valide las órdenes de medicamentos pendientes de pacientes ingresados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAtencionFarmaceuticaValidar';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAtencionFarmaceuticaValidar';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de farmacia activas de pacientes hospitalizados en un centro de atención (y opcionalmente en una unidad funcional) para validación farmacéutica, mostrando paciente, médico, cama y diagnóstico principal.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceuticaValidar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener un registro de estancia activo (REGESTADO = 1) con cama asignada en la misma unidad funcional y centro de atención de la orden; El paciente debe tener registrado un diagnóstico principal (CODDIAPRI = 1) para el ingreso; La orden de farmacia debe estar en estado ''1'' (activa); El centro de atención es obligatorio; la unidad funcional es opcional (vacío se trata como NULL)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceuticaValidar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes con estado ''1''; Solo se incluyen pacientes con estancia activa (REGESTADO=1) y cama coherente con la unidad funcional y centro de la orden; Solo se muestra el diagnóstico principal del ingreso (CODDIAPRI=1); El campo VALIDADO se normaliza a bit, asumiendo 0 cuando MEDICAMENTOVALIDADO es NULL; El campo Impresion siempre se devuelve como bit vacío (placeholder); ConsecutivoPrescripcion y ConsecutivoInsumos se devuelven como cadena vacía cuando son nulos o vacíos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceuticaValidar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden farmacéutica; Validación farmacéutica de medicamentos; Paciente hospitalizado; Unidad funcional; Centro de atención; Bodega de medicamentos; Cama hospitalaria; Diagnóstico principal (CIE); Médico tratante; Ingreso hospitalario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceuticaValidar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve órdenes farmacéuticas con ORDESTADO=''1'' del centro indicado, filtradas opcionalmente por unidad funcional, ordenadas por cama y código de paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceuticaValidar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @UnidadFuncional viene vacío o NULL (tras RTRIM y LEN=0) → Se ignora el filtro de unidad funcional y se listan órdenes de todas las unidades del centro else Se filtra por la unidad funcional indicada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceuticaValidar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPC; dbo.IHBODEGAS; dbo.INPACIENT; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.INDIAGNOP; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceuticaValidar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceuticaValidar';
-- GO
