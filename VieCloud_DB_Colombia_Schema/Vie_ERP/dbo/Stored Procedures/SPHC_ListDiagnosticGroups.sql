
-- =============================================
-- Author:		<>
-- Create date: <27/01/2026>
-- Description:	<Listar si los diagnósticos tienen grupo de cancer o erc>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListDiagnosticGroups]
(
@CodigosDiagnosticos varchar(100)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		SELECT CODDIAGNO AS CodigoDiagnostico,CODIGOGRUPO AS CodigoGrupo, CAST(1 AS tinyint) AS 'Tipo' FROM ADGRUPOCANCERDIAGND A 
			INNER JOIN ADGRUPOCANCERC B WITH(NOLOCK) ON A.IDADGRUPOCANCERC = B.ID
			WHERE A.CODDIAGNO IN (SELECT Value FROM dbo.splitstring(@CodigosDiagnosticos))
	UNION ALL
		SELECT DiagnosticCode AS CodigoDiagnostico, GroupCode AS CodigoGrupo, CAST(2 AS tinyint) AS 'Tipo' FROM Admissions.DiagnosticsGroupsERCPrecursoras A
			INNER JOIN Admissions.GroupsERCPrecursoras B WITH(NOLOCK) ON A.IdGroupsERCPrecursoras = B.Id
			WHERE A.DiagnosticCode IN (SELECT Value FROM dbo.splitstring(@CodigosDiagnosticos))
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dado un listado de códigos de diagnóstico separados por delimitador, el procedimiento verifica a cuáles grupos pertenecen: grupos de cáncer (Tipo=1), consultando tablas de diagnósticos y grupos de cáncer, o grupos de condiciones precursoras de Enfermedad Renal Crónica —ERC— (Tipo=2), usando las tablas de `DiagnosticsGroupsERCPrecursoras` y `GroupsERCPrecursoras`. Retorna el código de diagnóstico, el código de grupo y el tipo de clasificación para cada coincidencia encontrada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListDiagnosticGroups';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListDiagnosticGroups';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'"Devuelve, para una lista de códigos de diagnóstico, a qué grupos de cáncer y/o de ERC precursoras pertenecen, distinguiendo el tipo de agrupación."', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListDiagnosticGroups';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cadena de códigos de diagnóstico debe venir delimitada en formato compatible con dbo.splitstring.; Las tablas de grupos (cáncer y ERC precursoras) deben tener integridad referencial con sus tablas de detalle vía IDADGRUPOCANCERC / IdGroupsERCPrecursoras.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListDiagnosticGroups';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los diagnósticos asociados a grupos de cáncer se marcan con Tipo=1 y los asociados a grupos ERC precursoras con Tipo=2.; Solo se devuelven diagnósticos cuyo código figure en la lista de entrada y exista en alguna de las tablas de agrupación.; El resultado consolida ambas fuentes mediante UNION ALL, permitiendo que un mismo diagnóstico aparezca en ambos tipos si aplica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListDiagnosticGroups';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Diagnóstico; Grupo de cáncer; Enfermedad Renal Crónica (ERC) precursora; CIE-10', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListDiagnosticGroups';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADGRUPOCANCERDIAGND: Cuando el código de diagnóstico está en ADGRUPOCANCERDIAGND, retorna fila con CodigoGrupo del grupo de cáncer y Tipo=1.; [RETURN_RESULT] Admissions.DiagnosticsGroupsERCPrecursoras: Cuando el código de diagnóstico está en Admissions.DiagnosticsGroupsERCPrecursoras, retorna fila con CodigoGrupo del grupo ERC precursora y Tipo=2.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListDiagnosticGroups';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListDiagnosticGroups';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADGRUPOCANCERDIAGND; dbo.ADGRUPOCANCERC; Admissions.DiagnosticsGroupsERCPrecursoras; Admissions.GroupsERCPrecursoras', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListDiagnosticGroups';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListDiagnosticGroups';
-- GO
