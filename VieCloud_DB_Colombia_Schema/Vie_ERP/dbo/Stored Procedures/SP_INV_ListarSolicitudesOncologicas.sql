
CREATE PROCEDURE [dbo].[SP_INV_ListarSolicitudesOncologicas]
(
@CentoAtencion varchar(5),
@FechaInicial as date,
@FechaFinal as date 
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT O.CODTIPPAC, A.ID as ConsecutivoFarmacia, A.FECHAORDE  as FechaOrden, C.IPCODPACI  AS CodigoPaciente ,RTRIM(IPNOMCOMP) AS NombrePaciente,RTRIM(UFUDESCRI) AS UnidadFuncional,  
A.ESTADO as Estado, A.CODPROSAL , P.NOMMEDICO  as NombreMedico,(select top 1 CODBODEGA from HCUNITHIS where UFUCODIGO = U.UFUCODIGO ) AS Bodega,RF.NUMINGRES AS Ingreso,S.UFUCODIGO,'' as ConsecutivoPrescripcion, '' as ConsecutivoInsumos,RTRIM(ASE.CODCENCOS) AS CentroCostos,CAST('' AS bit) AS Impresion 
FROM  
AGFARMEONCOC A INNER JOIN 
AGASICITA C ON C.CODAUTONU = A.IDCITA  INNER JOIN
INPACIENT B ON C.IPCODPACI=B.IPCODPACI LEFT JOIN 
HCONCOFICH F ON F.IPCODPACI = C.IPCODPACI AND F.ESTFICHA = 1 LEFT JOIN
ADRELFICHAON RF ON RF.IDFICHA  = F.ID  AND RF.ESTADO = 1 LEFT JOIN
ADINGRESO O ON O.NUMINGRES =RF.NUMINGRES INNER JOIN  
AGENSALAC S ON S.CODCONCEC = C.IDSALA INNER JOIN
INUNIFUNC U ON U.UFUCODIGO = S.UFUCODIGO INNER JOIN
INAREASER ASE ON ASE.ARSCODIGO = U.ARSCODIGO INNER JOIN
INPROFSAL  P on P.CODPROSAL =  A.CODPROSAL  
where C.CODCENATE = @CentoAtencion and  A.ESTADO =1 and  C.CODESTCIT = 0 AND C.FECHORAIN BETWEEN @FechaInicial AND @FechaFinal

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las solicitudes de medicamentos oncológicos ambulatorios pendientes de despacho en farmacia, filtradas por centro de atención y rango de fechas de la cita. Combina la orden farmacológica oncológica (AGFARMEONCOC) con la cita médica asociada (AGASICITA), los datos del paciente (INPACIENT), la ficha oncológica activa (HCONCOFICH), el ingreso hospitalario vinculado (ADINGRESO vía ADRELFICHAON), la sala y unidad funcional donde se atiende al paciente (AGENSALAC, INUNIFUNC, INAREASER) y el médico que generó la orden (INPROFSAL). El resultado sirve para que el servicio de farmacia oncológica identifique qué solicitudes de quimioterapia u otros medicamentos oncológicos deben prepararse o dispensarse, mostrando el paciente, médico tratante, unidad funcional, bodega asignada, número de ingreso y centro de costos correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarSolicitudesOncologicas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarSolicitudesOncologicas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las solicitudes oncológicas activas y pendientes de atención de un centro asistencial dentro de un rango de fechas, con datos del paciente, médico, ubicación y bodega asociada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarSolicitudesOncologicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las citas deben pertenecer al centro de atención especificado.; Las solicitudes oncológicas de farmacia deben estar en estado activo (ESTADO=1).; Las citas deben estar en estado pendiente/inicial (CODESTCIT=0).; La fecha/hora de inicio de la cita debe estar entre las fechas inicial y final indicadas.; Si existe ficha oncológica, debe estar activa (ESTFICHA=1) y la relación ficha-ingreso debe estar activa (ESTADO=1).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarSolicitudesOncologicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan solicitudes oncológicas con estado activo (=1).; Solo se retornan citas en estado 0 (no atendidas/pendientes).; La bodega se resuelve tomando la primera bodega asociada a la unidad funcional de la sala de la cita.; Los campos ConsecutivoPrescripcion y ConsecutivoInsumos siempre se retornan vacíos, e Impresion siempre como bit vacío.; La relación con ingreso hospitalario es opcional (LEFT JOIN), permitiendo solicitudes sin ingreso asociado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarSolicitudesOncologicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud oncológica; Cita médica; Paciente; Médico tratante; Unidad funcional; Sala de atención; Ficha oncológica; Ingreso hospitalario; Bodega de farmacia; Centro de costos; Centro de atención; Tipo de paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarSolicitudesOncologicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un resultset con datos de solicitud oncológica, paciente, médico, unidad funcional, bodega, ingreso y centro de costos cuando se cumplen las condiciones de centro, estado de solicitud, estado de cita y rango de fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarSolicitudesOncologicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGFARMEONCOC; dbo.AGASICITA; dbo.INPACIENT; dbo.HCONCOFICH; dbo.ADRELFICHAON; dbo.ADINGRESO; dbo.AGENSALAC; dbo.INUNIFUNC; dbo.INAREASER; dbo.INPROFSAL; dbo.HCUNITHIS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarSolicitudesOncologicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarSolicitudesOncologicas';
-- GO
