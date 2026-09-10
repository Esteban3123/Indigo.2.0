
CREATE PROCEDURE [dbo].[ESE_SP_HC_odontologia_ModeloHC_Odontologia]
AS

exec [dbo].[SP_HC_odontologia_ModeloHC_Odontologia] '01/07/2019','08/07/2019',28 --Odontologia

--select * from ESE_HC_HISTORICO_ODONTOLOGIA
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de prueba o ejecución rápida que invoca el modelo de historia clínica odontológica para un período fijo (del 1 al 8 de julio de 2019) y el centro de atención 28. Sirve como acceso directo al reporte o procesamiento del modelo de historia clínica de odontología, delegando la lógica principal en el procedimiento SP_HC_odontologia_ModeloHC_Odontologia. Toca la entidad de historia clínica odontológica, consolidando registros de atenciones y consultas dentales del paciente en ese rango de fechas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_odontologia_ModeloHC_Odontologia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_odontologia_ModeloHC_Odontologia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que invoca el proceso de generación/extracción del modelo de historia clínica de odontología con un rango de fechas y sede fijos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_ModeloHC_Odontologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el procedimiento dbo.SP_HC_odontologia_ModeloHC_Odontologia y aceptar dos fechas y un identificador numérico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_ModeloHC_Odontologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre ejecuta con el mismo rango de fechas (01/07/2019 a 08/07/2019) y el mismo código 28; No recibe parámetros de entrada; los valores están hardcodeados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_ModeloHC_Odontologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Odontología; Historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_ModeloHC_Odontologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.SP_HC_odontologia_ModeloHC_Odontologia: Ejecuta el SP delegado con parámetros fijos ''01/07/2019'', ''08/07/2019'' y 28', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_ModeloHC_Odontologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SP_HC_odontologia_ModeloHC_Odontologia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_ModeloHC_Odontologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_ModeloHC_Odontologia';
-- GO
