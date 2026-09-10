

CREATE PROCEDURE [dbo].[SP_HC_ListProceduresTemplatesDocuments] AS
BEGIN
	SET NOCOUNT ON;

	WITH PROCEDIMIENTOS AS (
		SELECT RTRIM(CODSERIPS) AS CODSERIPS, RTRIM(TIPSERIPS) AS TIPSERIPS, RTRIM(DESSERIPS) AS DESSERIPS FROM INCUPSIPS WHERE TIPSERIPS IN (1,2,3,4,5) AND SIPSESTADO = 1
		UNION ALL
		SELECT RTRIM(CODCOMSAM) AS Codigo, 9 AS TIPSERIPS, RTRIM(DESCOMSAM) AS 'Grupo Sanguineo' FROM HCCOMSAN WHERE State = 1
	) SELECT * FROM PROCEDIMIENTOS ORDER BY TIPSERIPS ASC

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de procedimientos médicos y servicios disponibles para usar en plantillas de documentos de historia clínica. Combina los servicios CUPS/IPS activos (tipos 1 al 5, como consultas, cirugías, laboratorios, imágenes y procedimientos especiales) con los grupos sanguíneos registrados en el sistema, unificándolos en un único catálogo ordenado por tipo. Se utiliza para poblar selectores o listas desplegables al momento de crear o configurar plantillas de documentos clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListProceduresTemplatesDocuments';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListProceduresTemplatesDocuments';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida y entrega un catálogo unificado de procedimientos activos (tipos 1 a 5) junto con los grupos sanguíneos vigentes, para alimentar plantillas de documentos en historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListProceduresTemplatesDocuments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen procedimientos cuyo tipo de servicio IPS esté entre 1 y 5.; Solo se incluyen registros activos: SIPSESTADO = 1 en servicios IPS y State = 1 en grupos sanguíneos.; Los grupos sanguíneos se exponen con un tipo fijo (9) para diferenciarlos de los demás procedimientos.; Los códigos y descripciones se devuelven sin espacios finales (RTRIM).; El resultado se ordena ascendentemente por tipo de servicio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListProceduresTemplatesDocuments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Procedimientos; Servicios IPS; Grupo Sanguíneo; Plantillas de documentos de Historia Clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListProceduresTemplatesDocuments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INCUPSIPS: Cuando TIPSERIPS IN (1,2,3,4,5) y SIPSESTADO = 1, retorna el código, tipo y descripción del servicio IPS.; [RETURN_RESULT] dbo.HCCOMSAN: Cuando State = 1, retorna el código y descripción del grupo sanguíneo etiquetándolo con tipo 9 dentro del catálogo unificado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListProceduresTemplatesDocuments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INCUPSIPS; dbo.HCCOMSAN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListProceduresTemplatesDocuments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListProceduresTemplatesDocuments';
-- GO
