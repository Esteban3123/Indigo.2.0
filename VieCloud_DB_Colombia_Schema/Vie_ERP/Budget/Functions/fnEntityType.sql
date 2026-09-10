
create FUNCTION [Budget].[fnEntityType] (@EntityTypeId as int)
RETURNS nvarchar (30)
AS
BEGIN

declare @EntityTypeDescription nvarchar(50)

SELECT @EntityTypeDescription = case  @EntityTypeId when '1' then 'EPS Contributivo' when '2' then 'EPS Subsidiado' when '3' then 'ET Vinculados Municipios'
when '4' then  'ET Vinculados Departamentos' when '5' then 'ARL Riesgos Laborales' when '6' then 'MP Medicina Prepagada'
when '7' then 'IPS Privada' when '8' then 'IPS Publica' when '9' then 'Regimen Especial' when '10' then 'Accidentes de transito'
when '11' then 'Fosyga' when '12' then 'Otros' when '99' then 'Particulares' end

RETURN @EntityTypeDescription

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que recibe un identificador numérico de tipo de entidad pagadora y devuelve su descripción en texto. Traduce códigos internos a nombres legibles de aseguradoras y pagadores del sistema de salud colombiano, como EPS Contributivo, EPS Subsidiado, ARL Riesgos Laborales, Medicina Prepagada, Fosyga, entre otros. Se usa en el módulo de presupuesto (Budget) para mostrar el nombre del tipo de contratante o financiador en reportes y consultas, evitando manejar solo códigos numéricos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'FUNCTION', @level1name = N'fnEntityType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'FUNCTION', @level1name = N'fnEntityType';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un identificador numérico de tipo de entidad pagadora a su descripción textual según el catálogo del sistema de salud colombiano (EPS, ARL, IPS, Fosyga, etc.).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'FUNCTION', @level1name=N'fnEntityType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El catálogo de tipos de entidad está cableado en el código (no proviene de tabla); solo los IDs 1–12 y 99 tienen descripción asociada.; Si el ID no coincide con ningún valor del CASE, la función retorna NULL.; Aunque el tipo de retorno se declara nvarchar(30), la cadena ''ET Vinculados Departamentos'' tiene 28 caracteres y la variable interna se declara nvarchar(50), por lo que ningún literal se trunca.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'FUNCTION', @level1name=N'fnEntityType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'EPS Contributivo; EPS Subsidiado; Entidades Territoriales (Municipios y Departamentos); ARL (Riesgos Laborales); Medicina Prepagada; IPS Pública y Privada; Régimen Especial; Accidentes de tránsito (SOAT); Fosyga; Particulares; Tipos de entidad pagadora del sistema de salud colombiano', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'FUNCTION', @level1name=N'fnEntityType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Retorna el nombre del tipo de entidad correspondiente al ID recibido; retorna NULL si el ID no está en {1..12, 99}.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'FUNCTION', @level1name=N'fnEntityType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EntityTypeId = 1 → Devuelve ''EPS Contributivo''; si EntityTypeId = 2 → Devuelve ''EPS Subsidiado''; si EntityTypeId = 3 → Devuelve ''ET Vinculados Municipios''; si EntityTypeId = 4 → Devuelve ''ET Vinculados Departamentos''; si EntityTypeId = 5 → Devuelve ''ARL Riesgos Laborales''; si EntityTypeId = 6 → Devuelve ''MP Medicina Prepagada''; si EntityTypeId = 7 → Devuelve ''IPS Privada''; si EntityTypeId = 8 → Devuelve ''IPS Publica''; si EntityTypeId = 9 → Devuelve ''Regimen Especial''; si EntityTypeId = 10 → Devuelve ''Accidentes de transito''; si EntityTypeId = 11 → Devuelve ''Fosyga''; si EntityTypeId = 12 → Devuelve ''Otros''; si EntityTypeId = 99 → Devuelve ''Particulares'' else Para cualquier otro valor devuelve NULL (CASE sin ELSE)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'FUNCTION', @level1name=N'fnEntityType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'FUNCTION', @level1name=N'fnEntityType';
GO
