
CREATE PROCEDURE [dbo].[ESE_SP_HC_odontologia_ModeloHC_HigieneOral]
AS

--exec [dbo].[SP_HC_odontologia_ModeloHC_Odontologia] '01/04/2019','30/04/2019',33 --higiene oral

select * from ESE_HC_HISTORICO_HIGIENEORAL
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el histórico completo de atenciones de higiene oral registradas en la historia clínica odontológica. Consulta directamente la tabla ESE_HC_HISTORICO_HIGIENEORAL, devolviendo todos los registros de higiene oral sin filtro de fechas ni paciente. Se utiliza para obtener el listado general de evoluciones y controles de higiene oral realizados en el módulo de odontología, sirviendo como fuente de datos para reportes clínicos y auditorías del servicio dental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_odontologia_ModeloHC_HigieneOral';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_odontologia_ModeloHC_HigieneOral';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el histórico completo de registros de higiene oral odontológica para consulta.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_ModeloHC_HigieneOral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla ESE_HC_HISTORICO_HIGIENEORAL debe existir y contener los registros históricos de higiene oral.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_ModeloHC_HigieneOral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No aplica filtros, ordenamientos ni transformaciones sobre el conjunto retornado.; No modifica datos: es de solo lectura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_ModeloHC_HigieneOral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica odontológica; Higiene oral; Histórico clínico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_ModeloHC_HigieneOral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ESE_HC_HISTORICO_HIGIENEORAL: Retorna todas las filas y columnas (SELECT *) sin filtros.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_ModeloHC_HigieneOral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ESE_HC_HISTORICO_HIGIENEORAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_ModeloHC_HigieneOral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_ModeloHC_HigieneOral';
-- GO
