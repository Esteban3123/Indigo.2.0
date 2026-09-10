

CREATE PROCEDURE [dbo].[SPLIS_ListarParametrosEnvioAlertas]
AS
BEGIN
	SET NOCOUNT ON;
     SELECT CODCONSEC, CODCENATE, TIPSECALER, ENVMAILUS, ENVSMSUSU, NOMCOMUSU, NOMCORSMS, NUMTELMOV, CORELEUSU, MENADICOR
     FROM      dbo.HCPARALERNOT
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los parámetros configurados para el envío de alertas y notificaciones clínicas por centro de atención. Devuelve información sobre los destinatarios de las alertas, incluyendo si se les notifica por correo electrónico o SMS, sus datos de contacto (nombre, correo electrónico y número de teléfono móvil), el tipo de sección o categoría de alerta, y el mensaje adicional personalizado que se incluye en la notificación. Se usa para consultar y administrar la configuración de alertas del sistema de historia clínica, permitiendo saber a quién y cómo se envían las notificaciones ante eventos clínicos relevantes en cada sede o centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarParametrosEnvioAlertas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarParametrosEnvioAlertas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los parámetros de configuración de envío de alertas y notificaciones (correo/SMS) por centro de atención y usuario destinatario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarParametrosEnvioAlertas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna el conjunto completo de la tabla de parámetros, sin filtrar por estado, centro ni usuario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarParametrosEnvioAlertas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'alertas; notificaciones; centro de atención; envío por correo electrónico; envío por SMS; usuario destinatario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarParametrosEnvioAlertas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCPARALERNOT: Devuelve todos los registros de parámetros de alertas/notificaciones sin aplicar filtros', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarParametrosEnvioAlertas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPARALERNOT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarParametrosEnvioAlertas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarParametrosEnvioAlertas';
-- GO
