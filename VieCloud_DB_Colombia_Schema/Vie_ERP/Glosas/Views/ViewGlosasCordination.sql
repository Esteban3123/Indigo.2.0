CREATE VIEW [Glosas].[ViewGlosasCordination]
AS
SELECT 
	N0.Id
	,N0.State
	,N0.RadicatedConsecutive
	,c.Nit
	,c.Name
	,N0.DocumentNumber
	,N0.DocumentDate	
FROM(Glosas.GlosaObjectionsReceptionD N1 WITH(NOLOCK)
	INNER JOIN Glosas.GlosaPortfolioGlosada N2 WITH(NOLOCK) ON(N1.PortfolioGlosaId = N2.Id))
	INNER JOIN Glosas.GlosaObjectionsReceptionC N0 ON N0.Id = N1.GlosaObjectionsReceptionCId AND N0.State IN(N'1', N'2')
	INNER JOIN Common.Customer c ON c.Id = N0.CustomerId
	WHERE N2.State IN(N'2', N'3') AND (N1.DocumentType = 1)

UNION

SELECT 
	N0.Id
	,N0.State
	,N0.RadicatedConsecutive
	,c.Nit
	,c.Name
	,N0.DocumentNumber
	,N0.DocumentDate
FROM(Glosas.GlosaObjectionsReceptionD N3 WITH(NOLOCK)
    INNER JOIN Glosas.GlosaPortfolioGlosada N4 WITH(NOLOCK) ON(N3.PortfolioGlosaId = N4.Id))
    INNER JOIN Glosas.GlosaObjectionsReceptionC N0 ON N0.Id = N3.GlosaObjectionsReceptionCId AND N0.State IN(N'1', N'2')
	INNER JOIN Common.Customer c on c.Id = N0.CustomerId
	WHERE N4.State IN(N'5', N'6') AND (N3.DocumentType = 2)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las recepciones de objeciones a glosas que están pendientes de coordinación o en proceso activo (estados 1 y 2), combinando dos escenarios: facturas glosadas en etapa de glosa inicial o segunda instancia (estados 2 y 3 de la cartera, tipo de documento 1) y facturas en etapa de conciliación o cierre (estados 5 y 6 de la cartera, tipo de documento 2). Integra los encabezados de objeción (GlosaObjectionsReceptionC), el detalle de ítems glosados (GlosaObjectionsReceptionD), el seguimiento financiero de la cartera glosada (GlosaPortfolioGlosada) y los datos del cliente o entidad pagadora (EPS/aseguradora). Para cada objeción muestra el identificador, estado, número de radicado consecutivo, NIT y nombre de la EPS, número y fecha del documento de objeción; sirve como insumo para la gestión y coordinación del proceso de glosas entre la institución y las entidades pagadoras.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'VIEW', @level1name = N'ViewGlosasCordination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'VIEW', @level1name = N'ViewGlosasCordination';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las recepciones de objeciones de glosa pendientes de coordinación, combinando objeciones a glosas iniciales y respuestas a ratificaciones según el estado de la cartera glosada.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewGlosasCordination';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros relacionados entre GlosaObjectionsReceptionC, GlosaObjectionsReceptionD y GlosaPortfolioGlosada; El cliente (Customer) referenciado en la recepción debe existir en Common.Customer', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewGlosasCordination';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen recepciones cuyo encabezado está en estado 1 o 2 (activas/en gestión); El estado válido de la cartera glosada depende del tipo de documento: tipo 1 requiere estados 2-3, tipo 2 requiere estados 5-6; Se aplica UNION (no UNION ALL): se eliminan duplicados exactos entre ambos conjuntos; Las consultas se realizan con NOLOCK sobre tablas de detalle y cartera glosada (lectura sucia permitida)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewGlosasCordination';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Objeción de glosa; Cartera glosada; Entidad pagadora (cliente/NIT); Radicación de documento; Coordinación de glosas', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewGlosasCordination';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Glosas.ViewGlosasCordination: Cuando el detalle tiene DocumentType=1 y la cartera glosada está en State 2 o 3, se retorna la recepción si su State es 1 o 2; [RETURN_RESULT] Glosas.ViewGlosasCordination: Cuando el detalle tiene DocumentType=2 y la cartera glosada está en State 5 o 6, se retorna la recepción si su State es 1 o 2', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewGlosasCordination';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DocumentType = 1 (objeción inicial) y GlosaPortfolioGlosada.State IN (2,3) → Incluye la recepción en el resultado; si DocumentType = 2 (respuesta/ratificación) y GlosaPortfolioGlosada.State IN (5,6) → Incluye la recepción en el resultado else Se excluye del resultado', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewGlosasCordination';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaObjectionsReceptionD; Glosas.GlosaPortfolioGlosada; Glosas.GlosaObjectionsReceptionC; Common.Customer', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewGlosasCordination';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewGlosasCordination';
GO
