-- =============================================
-- Author:      <HECTOR RODRIGUEZ RUBIANO>
-- Create Date: <14/01/2022>
-- Description: <Devuelve la descripcion del tipo de entidad de un grupo de atencion>
-- =============================================
CREATE FUNCTION [Contract].[fnCareGroupEntityType]
(
    -- Add the parameters for the function here
    @EntityType tinyint
)
RETURNS varchar(50)
AS
BEGIN
    -- Declare the return variable here
    DECLARE @Result varchar(50)

    -- Add the T-SQL statements to compute the return value here
	SELECT @Result =
	CASE @EntityType
    WHEN 1 THEN 'EPS Contributivo'
        WHEN 2 THEN 'EPS Subsidiado'
        WHEN 3 THEN 'ET Vinculados Municipios'
        WHEN 4 THEN 'ET Vinculados Departamentos'
        WHEN 5 THEN 'ARL Riesgos Laborales'
        WHEN 6 THEN 'MP Medicina Prepagada'
        WHEN 7 THEN 'IPS Privada'
        WHEN 8 THEN 'IPS Publica'
        WHEN 9 THEN 'Regimen Especial'
        WHEN 11 THEN 'Fosyga'
        WHEN 12 THEN 'Otros'
        WHEN 13 THEN 'Aseguradoras'
		ELSE CAST(@EntityType AS VARCHAR(50))
	END

    -- Return the result of the function
    RETURN @Result
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convierte el código numérico del tipo de entidad de un grupo de atención en su descripción legible para el negocio. Dado un número (por ejemplo 1, 2, 5), devuelve el nombre correspondiente: EPS Contributivo, EPS Subsidiado, ARL Riesgos Laborales, Medicina Prepagada, IPS Privada, Fosyga, entre otros. Se usa en reportes y consultas de contratos para mostrar el tipo de aseguradora o pagador al que pertenece un grupo de atención, evitando que los usuarios vean códigos crípticos en lugar de nombres reconocibles.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'fnCareGroupEntityType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'fnCareGroupEntityType';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de tipo de entidad de un grupo de atención a su descripción textual según la clasificación del sistema de salud colombiano (EPS, ARL, IPS, Fosyga, etc.).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'fnCareGroupEntityType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de entrada debe ser un tinyint (0-255) representando el código del tipo de entidad del grupo de atención', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'fnCareGroupEntityType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna un valor no nulo: si el código no está catalogado, devuelve la representación textual del código numérico recibido; El catálogo de tipos de entidad está hardcodeado dentro de la función (no consulta tablas); El código 10 no está mapeado y se devuelve como texto del número', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'fnCareGroupEntityType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Grupo de atención; Tipo de entidad; EPS Contributivo; EPS Subsidiado; Entes Territoriales (Municipios y Departamentos); ARL (Riesgos Laborales); Medicina Prepagada; IPS Privada y Pública; Régimen Especial; Fosyga; Aseguradoras', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'fnCareGroupEntityType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de entidad = 1 → Retorna ''EPS Contributivo''; si Tipo de entidad = 2 → Retorna ''EPS Subsidiado''; si Tipo de entidad = 3 → Retorna ''ET Vinculados Municipios''; si Tipo de entidad = 4 → Retorna ''ET Vinculados Departamentos''; si Tipo de entidad = 5 → Retorna ''ARL Riesgos Laborales''; si Tipo de entidad = 6 → Retorna ''MP Medicina Prepagada''; si Tipo de entidad = 7 → Retorna ''IPS Privada''; si Tipo de entidad = 8 → Retorna ''IPS Publica''; si Tipo de entidad = 9 → Retorna ''Regimen Especial''; si Tipo de entidad = 11 → Retorna ''Fosyga''; si Tipo de entidad = 12 → Retorna ''Otros''; si Tipo de entidad = 13 → Retorna ''Aseguradoras''; si Tipo de entidad no coincide con ningún valor catalogado (incluye 10 y otros) → Retorna el valor numérico recibido convertido a texto', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'fnCareGroupEntityType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'fnCareGroupEntityType';
GO
