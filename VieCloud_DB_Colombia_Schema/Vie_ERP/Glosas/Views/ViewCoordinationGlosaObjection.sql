

CREATE VIEW [Glosas].[ViewCoordinationGlosaObjection]
AS
SELECT	gord.Id,
		gord.GlosaObjectionsReceptionCId,
		gor.CustomerId,
		gord.DocumentType,
		gord.PortfolioGlosaId,
		gord.InvoiceNumber,
		gpg.IngressNumber,
		gpg.PatientName,		
		gord.State,
		gpg.State StatePortfolio,
		IIF(gpg.State IN (3, 6), 1, 0) StateRecord,
		ISNULL(gpg.ValueGlosado, 0) ValueGlosado,
		ISNULL(gpg.ValueReiterated, 0) ValueReiterated,
		ISNULL(gpg.ValueAcceptedFirstInstance, 0) ValueAcceptedFirstInstance,
		ISNULL(gpg.ValueAcceptedSecondInstance, 0) ValueAcceptedSecondInstance,
		gpg.InvoiceValueEntity,
		gpg.ValueAcceptedIPSconciliation,
		gpg.InvoiceValuePacient,
		gpg.ImportunityCauseId,
		CONCAT(ic.Code, ' - ', ic.Name) ImportunityCauseCodeName
FROM Glosas.GlosaObjectionsReceptionC gor WITH (NOLOCK)
JOIN Glosas.GlosaObjectionsReceptionD gord WITH (NOLOCK) ON gor.Id = gord.GlosaObjectionsReceptionCId
JOIN Glosas.GlosaPortfolioGlosada gpg WITH (NOLOCK) ON gord.PortfolioGlosaId = gpg.Id
LEFT JOIN Glosas.ImportunityCauses ic WITH (NOLOCK) ON gpg.ImportunityCauseId = ic.Id
WHERE
(
	(gpg.State IN ('2', '3') AND gord.DocumentType = '1') 
	OR
	(gpg.State IN ('5', '6') AND gord.DocumentType = '2')
)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de coordinación del proceso de objeciones a glosas: integra los encabezados de recepciones de objeción (GlosaObjectionsReceptionC), el detalle de cada ítem glosado objetado (GlosaObjectionsReceptionD) y la cartera de facturas glosadas (GlosaPortfolioGlosada) para mostrar en una sola consulta el estado de cada glosa durante el proceso de objeción ante la aseguradora o EPS. Incluye datos del cliente (aseguradora/EPS), número de factura, número de ingreso del paciente, nombre del paciente, valores glosados, reiterados, aceptados en primera y segunda instancia, valor conciliado con la IPS y la causa de glosa (importunidad) con su código y nombre. Filtra los registros según el tipo de documento de objeción y el estado de la cartera glosada (primera o segunda instancia), e indica mediante un indicador (StateRecord) si la glosa está en estado de registro activo o cerrado. Sirve para reportería y seguimiento del proceso de glosas, objeciones y respuestas ante entidades pagadoras en el módulo de cartera y facturación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'VIEW', @level1name = N'ViewCoordinationGlosaObjection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'VIEW', @level1name = N'ViewCoordinationGlosaObjection';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para coordinación de glosas, los detalles de objeciones cruzados con la cartera glosada y la causa de importunidad, filtrando por combinaciones válidas de estado de cartera y tipo de documento (objeción inicial vs ratificación).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewCoordinationGlosaObjection';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen recepciones de objeción (cabecera y detalle) relacionadas con registros de cartera glosada; El detalle de objeción referencia un PortfolioGlosaId existente en la cartera glosada; El tipo de documento del detalle es ''1'' (objeción primera instancia) o ''2'' (objeción/ratificación segunda instancia)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewCoordinationGlosaObjection';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'DocumentType=''1'' siempre se asocia a estados de cartera 2 o 3 (primera instancia); DocumentType=''2'' siempre se asocia a estados de cartera 5 o 6 (segunda instancia/ratificación); Los valores monetarios expuestos nunca son NULL (se garantizan en 0 mediante ISNULL); Toda fila visible tiene una recepción cabecera, un detalle y un registro de cartera glosada existentes (INNER JOIN); La causa de importunidad es opcional (LEFT JOIN); su ausencia no excluye la fila', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewCoordinationGlosaObjection';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Objeción de glosa; Ratificación de glosa; Cartera glosada; Causa de importunidad; Factura; Paciente; Primera instancia; Segunda instancia; Conciliación IPS; Entidad pagadora', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewCoordinationGlosaObjection';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Glosas.ViewCoordinationGlosaObjection: Solo expone filas cuando (State de cartera ∈ {2,3} y DocumentType=''1'') o (State de cartera ∈ {5,6} y DocumentType=''2''); cualquier otra combinación se excluye; [RETURN_RESULT] Glosas.ViewCoordinationGlosaObjection: Marca StateRecord=1 cuando el State de la cartera glosada está en {3,6}; en caso contrario StateRecord=0; [RETURN_RESULT] Glosas.ViewCoordinationGlosaObjection: Normaliza valores monetarios (Glosado, Reiterated, AcceptedFirstInstance, AcceptedSecondInstance) reemplazando NULL por 0; [RETURN_RESULT] Glosas.ViewCoordinationGlosaObjection: Concatena código y nombre de la causa de importunidad (''Code - Name'') cuando existe; si no hay causa relacionada (LEFT JOIN sin match) el campo será NULL', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewCoordinationGlosaObjection';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si gpg.State IN (2,3) AND gord.DocumentType = ''1'' → Incluye la fila en el resultado (flujo de objeción de primera instancia) else Evalúa la segunda condición de tipo de documento; si gpg.State IN (5,6) AND gord.DocumentType = ''2'' → Incluye la fila en el resultado (flujo de ratificación / segunda instancia) else La fila se excluye del resultado; si gpg.State IN (3,6) → StateRecord = 1 (registro en estado consolidado/cerrado de su instancia) else StateRecord = 0', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewCoordinationGlosaObjection';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaObjectionsReceptionC; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaPortfolioGlosada; Glosas.ImportunityCauses', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewCoordinationGlosaObjection';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewCoordinationGlosaObjection';
GO
