
-- =============================================
-- Author:		Juan F. Tamayo
-- Create date:	2016-10-07
-- Description:	Lista los Hemo componentes por numero de ingreso
-- =============================================
CREATE PROCEDURE [dbo].[SP_ListarHemocomponentesPorIngreso]
	@Ingreso varchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	select SER.ID, CAST(0 AS BIT) AS Seleccione,CAST(0 AS BIT) AS Copago,CASt(0 AS BIT) AS CuotaModeradora,CAST(0 AS BIT) AS AplicaProcedimiento,CASt('' as CHAR) AS 'CodigoProcedimiento',
	'Rastreo Anticuerpos' AS TIPOSERVICIO, SER.HCORHEMCOID AS IDTABLA, RTRIM(SER.CODSERIPS) AS CODSERIPS, RTRIM(CUPS.DESSERIPS) AS DESSERIPS,
    SOL.FECORDMED AS Fecha,CONVERT(VARCHAR(20),'') AS Entidad,CONVERT(VARCHAR(20),'') AS Contrato,ARSCODIGO AS AreaServicio,CONVERT(VARCHAR(200),'') AS Plantilla,
    REALIZOSERVICIO = CASE WHEN SER.ESTADO = 2 THEN 'Realizado' WHEN SER.ESTADO = 3 THEN 'No Realizado' END, ING.IPCODPACI AS Paciente
    from HCORHEMSER SER 
    INNER JOIN INCUPSIPS CUPS ON CUPS.CODSERIPS = SER.CODSERIPS
    INNER JOIN HCORHEMCO SOL ON SOL.ID = SER.HCORHEMCOID
    INNER JOIN ADINGRESO ING ON SOL.NUMINGRES = ING.NUMINGRES
    WHERE SER.ESTADO IN (2,3) AND SER.GENORDSER = 0 AND ING.NUMINGRES = @Ingreso AND SER.TIPOSERVICIO = 1
    UNION
    select SER.ID, CAST(0 AS BIT) AS Seleccione,CAST(0 AS BIT) AS Copago,CASt(0 AS BIT) AS CuotaModeradora,CAST(0 AS BIT) AS AplicaProcedimiento,CASt('' as CHAR) AS 'CodigoProcedimiento',
    CASE WHEN SER.TIPOSERVICIO = 2 THEN 'Prueba Cruzada' WHEN SER.TIPOSERVICIO = 3 THEN 'Transfusión' END TIPOSERVICIO , 
    SER.HCORHEMBOLID AS IDTABLA, RTRIM(SER.CODSERIPS) AS CODSERIPS, RTRIM(CUPS.DESSERIPS) AS DESSERIPS,
    SOL.FECORDMED AS Fecha,CONVERT(VARCHAR(20),'') AS Entidad,CONVERT(VARCHAR(20),'') AS Contrato,ARSCODIGO AS AreaServicio,CONVERT(VARCHAR(200),'') AS Plantilla,
    REALIZOSERVICIO = CASE WHEN SER.ESTADO = 2 THEN 'Realizado' WHEN SER.ESTADO = 3 THEN 'No Realizado' END, ING.IPCODPACI AS Paciente
    from HCORHEMSER SER 
    INNER JOIN INCUPSIPS CUPS ON CUPS.CODSERIPS = SER.CODSERIPS
    INNER JOIN HCORHEMBOL BOL ON BOL.ID = SER.HCORHEMBOLID
    INNER JOIN HCORHEMCO SOL ON SOL.ID = BOL.HCORHEMCOID
    INNER JOIN ADINGRESO ING ON SOL.NUMINGRES = ING.NUMINGRES
    WHERE SER.ESTADO IN (2,3) AND SER.GENORDSER = 0 AND ING.NUMINGRES = @Ingreso AND SER.TIPOSERVICIO IN(2,3)
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los hemocomponentes (servicios de hemoterapia) asociados a un número de ingreso hospitalario específico, combinando tres tipos de servicios: rastreo de anticuerpos, pruebas cruzadas y transfusiones. Consulta las órdenes de hemoterapia (HCORHEMCO), los servicios solicitados (HCORHEMSER), las bolsas de hemoderivados (HCORHEMBOL) y el catálogo de servicios CUPS (INCUPSIPS) para obtener el código y nombre del procedimiento, la fecha de la orden médica, el área de servicio y el estado de realización (Realizado / No Realizado). Se usa para mostrar o facturar los procedimientos de banco de sangre ejecutados durante un ingreso del paciente, identificado por su cédula (IPCODPACI) y número de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarHemocomponentesPorIngreso';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarHemocomponentesPorIngreso';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios de hemocomponentes (rastreo de anticuerpos, prueba cruzada y transfusión) realizados o no realizados asociados a un ingreso del paciente, pendientes de generar orden de servicio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarHemocomponentesPorIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe existir en ADINGRESO y estar relacionado a una solicitud en HCORHEMCO.; Los servicios deben estar en estado 2 (Realizado) o 3 (No Realizado).; Los servicios no deben tener orden de servicio generada (GENORDSER = 0).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarHemocomponentesPorIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen servicios cuyo estado sea Realizado (2) o No Realizado (3).; Solo se retornan servicios sin orden generada (GENORDSER = 0).; El rastreo de anticuerpos se ancla a la solicitud (HCORHEMCO) y la prueba cruzada/transfusión se ancla a la bolsa (HCORHEMBOL).; Los campos Seleccione, Copago, CuotaModeradora, AplicaProcedimiento, CodigoProcedimiento, Entidad, Contrato y Plantilla se devuelven con valores por defecto vacíos/false.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarHemocomponentesPorIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Hemocomponentes; Rastreo de Anticuerpos; Prueba Cruzada; Transfusión; Ingreso del paciente; Orden de servicio; Área de servicio; Código CUPS/IPS; Copago; Cuota Moderadora', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarHemocomponentesPorIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCORHEMSER: Devuelve servicios con TIPOSERVICIO=1 etiquetados como ''Rastreo Anticuerpos'' usando HCORHEMCOID como IDTABLA, unidos directamente a la solicitud HCORHEMCO.; [RETURN_RESULT] HCORHEMSER: Devuelve servicios con TIPOSERVICIO IN (2,3) etiquetados como ''Prueba Cruzada'' o ''Transfusión'' usando HCORHEMBOLID como IDTABLA, navegando vía HCORHEMBOL hacia la solicitud HCORHEMCO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarHemocomponentesPorIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SER.TIPOSERVICIO = 1 → Se clasifica el servicio como ''Rastreo Anticuerpos'' y se referencia HCORHEMCOID.; si SER.TIPOSERVICIO = 2 → Se clasifica como ''Prueba Cruzada'' y se referencia HCORHEMBOLID (bolsa).; si SER.TIPOSERVICIO = 3 → Se clasifica como ''Transfusión'' y se referencia HCORHEMBOLID (bolsa).; si SER.ESTADO = 2 → Marca REALIZOSERVICIO como ''Realizado''. else Si ESTADO = 3, marca como ''No Realizado''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarHemocomponentesPorIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORHEMSER; dbo.INCUPSIPS; dbo.HCORHEMCO; dbo.HCORHEMBOL; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarHemocomponentesPorIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarHemocomponentesPorIngreso';
-- GO
