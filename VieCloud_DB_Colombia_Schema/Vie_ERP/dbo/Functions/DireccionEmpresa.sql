
CREATE function [dbo].[DireccionEmpresa]()
RETURNS nvarchar (60)
AS
BEGIN
declare @DireccionEmpresa nvarchar(60)

	SELECT TOP 1 @DireccionEmpresa = INDDIREMP FROM INEMPRESU 
	return @DireccionEmpresa
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que retorna la dirección física de la institución prestadora de salud registrada en el sistema. Consulta la tabla maestra de la empresa (INEMPRESU) y extrae el campo de dirección principal (INDDIREMP). Se utiliza para mostrar la dirección de la IPS en documentos, reportes, facturas y formularios impresos generados por el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DireccionEmpresa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DireccionEmpresa';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la dirección registrada de la empresa desde la tabla maestra de información empresarial.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DireccionEmpresa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en INEMPRESU con el campo INDDIREMP poblado para retornar un valor no nulo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DireccionEmpresa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado nunca excede 60 caracteres (truncado por el tipo de retorno).; Solo se retorna una única fila aunque la tabla contenga múltiples registros.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DireccionEmpresa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empresa; Dirección de la empresa', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DireccionEmpresa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] INEMPRESU: Retorna el primer valor de INDDIREMP encontrado (TOP 1 sin ORDER BY) como nvarchar(60).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DireccionEmpresa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'INEMPRESU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DireccionEmpresa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DireccionEmpresa';
GO
