
CREATE PROCEDURE [dbo].[ESE_SP_HC_ModeloHC_PrimeraInfancia]
AS

---exec [dbo].[SP_HC_odontologia_ModeloHC_Odontologia] '01/04/2019','01/04/2019',7  --primera infancia

select * from ESE_HC_HISTORICO_PRIMERAINFANCIA
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que consulta y retorna todos los registros del historial clínico de primera infancia almacenados en la tabla ESE_HC_HISTORICO_PRIMERAINFANCIA. Sirve para obtener el modelo de historia clínica correspondiente a la atención de niños en etapa de primera infancia, incluyendo los datos de valoración y seguimiento propios de este grupo poblacional. Es utilizado en la generación y visualización del formulario o modelo de historia clínica pediátrica de primera infancia dentro del módulo de historia clínica del ERP/EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_ModeloHC_PrimeraInfancia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_ModeloHC_PrimeraInfancia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el histórico completo de atenciones de primera infancia almacenado para consulta del modelo de historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_ModeloHC_PrimeraInfancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No aplica filtros ni transformaciones: expone el contenido íntegro de la tabla histórica.; No realiza modificaciones de datos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_ModeloHC_PrimeraInfancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Primera infancia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_ModeloHC_PrimeraInfancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ESE_HC_HISTORICO_PRIMERAINFANCIA: Retorna todas las filas y columnas (SELECT *) sin filtros.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_ModeloHC_PrimeraInfancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ESE_HC_HISTORICO_PRIMERAINFANCIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_ModeloHC_PrimeraInfancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_ModeloHC_PrimeraInfancia';
-- GO
