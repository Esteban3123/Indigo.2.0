

CREATE PROCEDURE [dbo].[SPREP_HC_ListarDiagnosticosRecienNacidos]
(
@ConsecutivoRecienNacido int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT A.CODDIAGNO AS 'CODIGO DIAGNOSTICO', B.NOMDIAGNO AS DIAGNOSTICO
FROM HCRECNADI A WITH(NOLOCK)
INNER JOIN INDIAGNOS B ON A.CODDIAGNO=B.CODDIAGNO
WHERE A.CONSECREC = @ConsecutivoRecienNacido

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los diagnósticos CIE-10 asociados a un recién nacido específico, identificado por su número consecutivo. Cruza los registros de diagnósticos de la historia clínica del recién nacido (HCRECNADI) con el catálogo maestro de diagnósticos (INDIAGNOS) para devolver el código y el nombre legible de cada diagnóstico registrado. Se utiliza en la consulta y visualización de la historia clínica neonatal, permitiendo conocer qué patologías o condiciones fueron diagnosticadas al recién nacido durante su atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_ListarDiagnosticosRecienNacidos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_ListarDiagnosticosRecienNacidos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los diagnósticos asociados a un recién nacido, devolviendo el código y el nombre del diagnóstico desde el catálogo correspondiente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarDiagnosticosRecienNacidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el consecutivo del recién nacido en HCRECNADI para retornar resultados.; Los códigos de diagnóstico registrados deben existir en el catálogo INDIAGNOS para ser listados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarDiagnosticosRecienNacidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna diagnósticos que tengan correspondencia en el catálogo de diagnósticos (INNER JOIN con INDIAGNOS).; Filtra estrictamente por el consecutivo del recién nacido recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarDiagnosticosRecienNacidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Recién nacido; Diagnóstico; Historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarDiagnosticosRecienNacidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCRECNADI: Retorna código y nombre del diagnóstico cruzando HCRECNADI con INDIAGNOS por CODDIAGNO, filtrando por CONSECREC igual al consecutivo del recién nacido recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarDiagnosticosRecienNacidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRECNADI; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarDiagnosticosRecienNacidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarDiagnosticosRecienNacidos';
-- GO
