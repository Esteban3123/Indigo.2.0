-- Stored Procedure

CREATE PROCEDURE [dbo].[SPHC_ListarServiciosIPSPorHemocomponentesPaciente]
(
@Ingreso Char(20)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
select SER.ID, CAST(0 AS BIT) AS Seleccione,CAST(0 AS BIT) AS Copago,CASt(0 AS BIT) AS CuotaModeradora,CAST(0 AS BIT) AS AplicaProcedimiento,CASt('' as CHAR) AS 'CodigoProcedimiento',
 'Rastreo Anticuerpos' AS TIPOSERVICIO, SER.HCORHEMCOID AS IDTABLA, RTRIM(SER.CODSERIPS) AS CODSERIPS, RTRIM(CUPS.DESSERIPS) AS DESSERIPS,
SOL.FECORDMED AS Fecha,CONVERT(VARCHAR(20),'') AS Entidad,CONVERT(VARCHAR(20),'') AS Contrato,ARSCODIGO AS AreaServicio,CONVERT(VARCHAR(200),'') AS Plantilla,
REALIZOSERVICIO = CASE WHEN SER.ESTADO = 2 THEN 'Realizado' WHEN SER.ESTADO = 3 THEN 'No Realizado' END, ING.IPCODPACI AS Paciente
from HCORHEMSER SER with(nolock)
INNER JOIN INCUPSIPS CUPS with(nolock) ON CUPS.CODSERIPS = SER.CODSERIPS
INNER JOIN HCORHEMCO SOL with(nolock) ON SOL.ID = SER.HCORHEMCOID
INNER JOIN ADINGRESO ING with(nolock) ON SOL.NUMINGRES = ING.NUMINGRES
WHERE SER.ESTADO IN (2,3) AND SER.GENORDSER = 0 --AND ING.NUMINGRES = @Ingreso 
AND SER.TIPOSERVICIO = 1
UNION
select SER.ID, CAST(0 AS BIT) AS Seleccione,CAST(0 AS BIT) AS Copago,CASt(0 AS BIT) AS CuotaModeradora,CAST(0 AS BIT) AS AplicaProcedimiento,CASt('' as CHAR) AS 'CodigoProcedimiento',
CASE WHEN SER.TIPOSERVICIO = 2 THEN 'Prueba Cruzada' WHEN SER.TIPOSERVICIO = 3 THEN 'Transfusión' END TIPOSERVICIO , 
SER.HCORHEMBOLID AS IDTABLA, RTRIM(SER.CODSERIPS) AS CODSERIPS, RTRIM(CUPS.DESSERIPS) AS DESSERIPS,
SOL.FECORDMED AS Fecha,CONVERT(VARCHAR(20),'') AS Entidad,CONVERT(VARCHAR(20),'') AS Contrato,ARSCODIGO AS AreaServicio,CONVERT(VARCHAR(200),'') AS Plantilla,
REALIZOSERVICIO = CASE WHEN SER.ESTADO = 2 THEN 'Realizado' WHEN SER.ESTADO = 3 THEN 'No Realizado' END, ING.IPCODPACI AS Paciente
from HCORHEMSER SER with(nolock)
INNER JOIN INCUPSIPS CUPS with(nolock) ON CUPS.CODSERIPS = SER.CODSERIPS
INNER JOIN HCORHEMBOL BOL with(nolock) ON BOL.ID = SER.HCORHEMBOLID
INNER JOIN HCORHEMCO SOL with(nolock) ON SOL.ID = BOL.HCORHEMCOID
INNER JOIN ADINGRESO ING with(nolock) ON SOL.NUMINGRES = ING.NUMINGRES
WHERE SER.ESTADO IN (2,3) AND SER.GENORDSER = 0 --AND ING.NUMINGRES = @Ingreso 
AND SER.TIPOSERVICIO IN(2,3)
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los servicios IPS (procedimientos CUPS) asociados a órdenes de hemoterapia y hemocomponentes para un paciente dado su número de ingreso. Combina tres tipos de servicios: rastreo de anticuerpos (ligados directamente a la orden de hemoterapia), pruebas cruzadas y transfusiones (ligadas a bolsas de hemoderivados específicas). Para cada servicio retorna el código y descripción CUPS, la fecha de la orden médica, el estado de realización (Realizado / No Realizado), el área de servicio y la cédula del paciente, permitiendo facturar o registrar en historia clínica los procedimientos ejecutados durante el proceso transfusional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarServiciosIPSPorHemocomponentesPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarServiciosIPSPorHemocomponentesPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios IPS asociados a hemocomponentes (rastreo de anticuerpos, prueba cruzada y transfusión) realizados o no realizados de un paciente, para su posterior facturación u orden.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosIPSPorHemocomponentesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas HCORHEMSER, HCORHEMCO, HCORHEMBOL, INCUPSIPS y ADINGRESO deben existir y estar relacionadas por sus llaves.; Los servicios deben tener un CODSERIPS válido en INCUPSIPS.; Los servicios deben estar en estado 2 (Realizado) o 3 (No Realizado).; Los servicios no deben tener orden ya generada (GENORDSER = 0).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosIPSPorHemocomponentesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen servicios con ESTADO en (2,3); se excluyen los pendientes o anulados.; Solo se incluyen servicios cuya orden de servicio aún no haya sido generada (GENORDSER = 0).; Cada fila pertenece a exactamente uno de los tipos: Rastreo Anticuerpos, Prueba Cruzada o Transfusión.; El paciente reportado proviene siempre del ingreso (ADINGRESO.IPCODPACI) ligado a la solicitud de hemocomponentes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosIPSPorHemocomponentesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Hemocomponentes; Rastreo de anticuerpos; Prueba cruzada; Transfusión; Servicios IPS / CUPS; Ingreso del paciente; Copago; Cuota moderadora; Área de servicio; Orden médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosIPSPorHemocomponentesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve servicios con TIPOSERVICIO=1 etiquetados como ''Rastreo Anticuerpos'' enlazados directamente a la solicitud HCORHEMCO.; [RETURN_RESULT] RESULTSET: Devuelve servicios con TIPOSERVICIO=2 como ''Prueba Cruzada'' y TIPOSERVICIO=3 como ''Transfusión'', enlazados a la bolsa HCORHEMBOL y a su solicitud HCORHEMCO.; [RETURN_RESULT] RESULTSET: Marca REALIZOSERVICIO=''Realizado'' cuando ESTADO=2 y ''No Realizado'' cuando ESTADO=3.; [RETURN_RESULT] RESULTSET: Inicializa banderas Seleccione, Copago, CuotaModeradora y AplicaProcedimiento en 0 (false), y CodigoProcedimiento, Entidad, Contrato y Plantilla vacíos para que sean diligenciadas por el consumidor.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosIPSPorHemocomponentesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SER.TIPOSERVICIO = 1 → Se etiqueta el servicio como ''Rastreo Anticuerpos'' y se usa HCORHEMCOID como IDTABLA, vinculando directamente con la solicitud HCORHEMCO.; si SER.TIPOSERVICIO IN (2,3) → Se etiqueta como ''Prueba Cruzada'' (2) o ''Transfusión'' (3), usando HCORHEMBOLID como IDTABLA y vinculando vía HCORHEMBOL hacia HCORHEMCO.; si SER.ESTADO = 2 → Se reporta ''Realizado'' else Si ESTADO = 3 se reporta ''No Realizado''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosIPSPorHemocomponentesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORHEMSER; dbo.INCUPSIPS; dbo.HCORHEMCO; dbo.HCORHEMBOL; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosIPSPorHemocomponentesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosIPSPorHemocomponentesPaciente';
-- GO
