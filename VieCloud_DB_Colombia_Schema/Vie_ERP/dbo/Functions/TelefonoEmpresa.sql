
CREATE function [dbo].[TelefonoEmpresa]()
RETURNS nvarchar (60)
AS
BEGIN
declare @TelefonoEmpresa nvarchar(60)

	SELECT TOP 1 @TelefonoEmpresa = INDTE1EMP FROM INEMPRESU 
	return @TelefonoEmpresa
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que retorna el número de teléfono principal de la institución prestadora de salud (IPS) registrada en el sistema. Consulta la tabla maestra de la empresa (INEMPRESU) y devuelve el primer teléfono de contacto disponible. Se utiliza para mostrar o imprimir los datos de contacto de la institución en documentos, reportes, facturas y formularios generados por el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TelefonoEmpresa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TelefonoEmpresa';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el teléfono principal registrado de la empresa configurada en el sistema.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TelefonoEmpresa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en la tabla de empresa para obtener un valor; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TelefonoEmpresa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna como máximo un valor (TOP 1).; El resultado es nvarchar(60).; No modifica datos; es función de solo lectura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TelefonoEmpresa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empresa; Teléfono de contacto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TelefonoEmpresa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna el primer teléfono encontrado (TOP 1) desde INEMPRESU sin ORDER BY explícito.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TelefonoEmpresa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INEMPRESU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TelefonoEmpresa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TelefonoEmpresa';
GO
