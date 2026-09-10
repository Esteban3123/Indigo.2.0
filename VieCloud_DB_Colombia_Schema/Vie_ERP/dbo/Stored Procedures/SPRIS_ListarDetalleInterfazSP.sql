
CREATE PROCEDURE [dbo].[SPRIS_ListarDetalleInterfazSP]
AS
BEGIN
	SET NOCOUNT ON;
SELECT CODCONSEC, CODCENATE, UFUCODIGO, IDETIPHIS, NUMEFOLIO, NUMINGRES, CODPROSAL, FECHORORD, CODPROTEC, FECHORINI, FECHORFIN, CODPRORAD, FECHORLEC, CODUSUARI, FECHORTRA 
FROM dbo.RISTRADET 
WHERE CODCONSEC=''
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que lista el detalle de registros de la interfaz de RIPS (Registros Individuales de Prestación de Servicios) desde la tabla RISTRADET. Retorna información de cada prestación: centro de atención, unidad funcional, número de ingreso, profesional que ordenó el servicio, fechas de orden, inicio, fin, lectura y transmisión, además del profesional que realizó el procedimiento y el usuario que lo registró. Se utiliza para consultar y exponer el detalle de los servicios prestados que serán enviados o ya fueron transmitidos como parte del reporte RIPS. Actualmente la consulta filtra por un código consecutivo vacío, por lo que funciona como plantilla base de extracción del detalle de interfaz RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPRIS_ListarDetalleInterfazSP';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPRIS_ListarDetalleInterfazSP';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta el detalle de trazabilidad de órdenes de imágenes diagnósticas filtrando por un identificador consecutivo vacío, devolviendo siempre un resultado vacío.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPRIS_ListarDetalleInterfazSP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla de trazabilidad de detalle debe existir y ser accesible', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPRIS_ListarDetalleInterfazSP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No modifica datos, solo lectura; El filtro por consecutivo vacío garantiza que en condiciones normales no se devuelvan registros', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPRIS_ListarDetalleInterfazSP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Trazabilidad de órdenes de imágenes diagnósticas; Orden médica; Lectura radiológica; Profesional de la salud; Folio/Ingreso del paciente; Unidad funcional; Centro de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPRIS_ListarDetalleInterfazSP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.RISTRADET: Filtra con CODCONSEC='''' (cadena vacía), por lo que retorna un conjunto vacío salvo que existan registros con consecutivo vacío', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPRIS_ListarDetalleInterfazSP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RISTRADET', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPRIS_ListarDetalleInterfazSP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPRIS_ListarDetalleInterfazSP';
-- GO
