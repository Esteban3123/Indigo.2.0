
CREATE FUNCTION [dbo].[TipoAislamiento] (@CodigoAislamiento as int)
RETURNS nvarchar (60)
AS
BEGIN

declare @Aislamiento nvarchar(60)

--SELECT @Aislamiento = CASE @CodigoAislamiento WHEN '1' THEN 'Aerosol' WHEN '2' THEN 'Contacto' WHEN '3' THEN 'Estandar' WHEN '4' THEN 'Gota' WHEN '5' THEN 'Protector' ELSE ' ' END

	SELECT @Aislamiento  =  Nombre from CHTIPOSAISLAMIENTOS WHERE Id = @CodigoAislamiento 
	

RETURN @Aislamiento

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que, dado un código numérico de aislamiento, devuelve el nombre descriptivo del tipo de aislamiento clínico correspondiente (por ejemplo: Aerosol, Contacto, Estándar, Gota, Protector). Consulta el catálogo CHTIPOSAISLAMIENTOS para traducir el código al nombre legible. Se utiliza en módulos de hospitalización para mostrar en reportes, órdenes o pantallas el tipo de aislamiento asignado a un paciente internado, evitando manejar solo códigos numéricos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipoAislamiento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipoAislamiento';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el nombre descriptivo del tipo de aislamiento clínico a partir de su identificador.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoAislamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador recibido debe existir en el catálogo de tipos de aislamiento; en caso contrario el resultado será NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoAislamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El nombre retornado proviene exclusivamente del catálogo CHTIPOSAISLAMIENTOS, asegurando consistencia con la tabla maestra.; La longitud máxima del nombre retornado es 60 caracteres.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoAislamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de aislamiento; Catálogo clínico de aislamientos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoAislamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHTIPOSAISLAMIENTOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoAislamiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoAislamiento';
GO
