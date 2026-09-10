
create FUNCTION [Budget].[fnCollectionType] (@CollectionTypeId as int)
RETURNS nvarchar (30)
AS
BEGIN

declare @CollectionTypeDescription nvarchar(50)

SELECT @CollectionTypeDescription = case  @CollectionTypeId when  '1'  then 'Cuotas a cargo de Pacientes'
when '2' then 'Cuotas a cargo de Pacientes' when '3' then 'Ventas de Contado Servicios' 
when '7' then 'Ventas de Contado Productos' else 'Otros' end

RETURN @CollectionTypeDescription

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar del módulo de Presupuesto que traduce un código numérico de tipo de recaudo a su descripción legible en español. Recibe un identificador de tipo de cobro y devuelve la categoría correspondiente: cuotas a cargo de pacientes (códigos 1 y 2), ventas de contado de servicios (código 3), ventas de contado de productos (código 7), u ''Otros'' para cualquier código no reconocido. Se utiliza para etiquetar y clasificar los tipos de recaudación o cobro en reportes financieros y de cartera del módulo de Presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'FUNCTION', @level1name = N'fnCollectionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'FUNCTION', @level1name = N'fnCollectionType';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un identificador numérico de tipo de recaudo a su descripción de negocio (cuotas de pacientes, ventas de contado de servicios o productos, u ''Otros'').', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'FUNCTION', @level1name=N'fnCollectionType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los identificadores 1 y 2 se consideran equivalentes y mapean a la misma descripción (''Cuotas a cargo de Pacientes'').; La función siempre retorna un texto no nulo: cualquier identificador no contemplado se rotula como ''Otros''.; El conjunto de tipos reconocidos está restringido a {1, 2, 3, 7}.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'FUNCTION', @level1name=N'fnCollectionType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de cobro/recaudo; Cuotas de pacientes; Ventas de contado de servicios; Ventas de contado de productos', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'FUNCTION', @level1name=N'fnCollectionType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna nvarchar(30) con la descripción del tipo de recaudo según el mapeo CASE sobre @CollectionTypeId; valores no contemplados retornan ''Otros''.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'FUNCTION', @level1name=N'fnCollectionType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CollectionTypeId = 1 o 2 → Devuelve ''Cuotas a cargo de Pacientes''; si CollectionTypeId = 3 → Devuelve ''Ventas de Contado Servicios''; si CollectionTypeId = 7 → Devuelve ''Ventas de Contado Productos''; si CollectionTypeId distinto de 1, 2, 3 o 7 → Devuelve ''Otros''', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'FUNCTION', @level1name=N'fnCollectionType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'FUNCTION', @level1name=N'fnCollectionType';
GO
