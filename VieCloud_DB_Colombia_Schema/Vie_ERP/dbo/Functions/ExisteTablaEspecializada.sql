
CREATE FUNCTION [dbo].[ExisteTablaEspecializada] (@Fecha as datetime)
RETURNS bit 
AS
BEGIN

	DECLARE @Mes varchar(20) = MONTH(@Fecha)
	DECLARE @nombreTablaANT varchar(20) 
	DECLARE @nombreTablaRES varchar(20)
	DECLARE @nombreTablaEXA varchar(20)
	Declare @Resultado bit

	IF LEN(@Mes) = 1 
		BEGIN
			SET @Mes = '0' + @Mes 
			SET @nombreTablaANT = CONCAT('ANTVALORES', YEAR(@Fecha) , @Mes);
			SET @nombreTablaRES = CONCAT('RSVALORES', YEAR(@Fecha) , @Mes);
			SET @nombreTablaEXA = CONCAT('EXAVALORES', YEAR(@Fecha) , @Mes);
		END
	ELSE 
		BEGIN
			SET @nombreTablaANT = CONCAT('ANTVALORES', YEAR(@Fecha) , MONTH(@Fecha));
			SET @nombreTablaRES = CONCAT('RSVALORES', YEAR(@Fecha) , MONTH(@Fecha));
			SET @nombreTablaEXA = CONCAT('EXAVALORES', YEAR(@Fecha) , MONTH(@Fecha));
		END

	IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = @nombreTablaANT) AND EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = @nombreTablaRES) AND EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = @nombreTablaEXA)
		BEGIN
			--PRINT 'Existe'
			SET	@Resultado = 1 
		END
	ELSE
		BEGIN
			--PRINT 'no existe'
			SET @Resultado = 0
		END

	RETURN @Resultado	
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Verifica si existen las tablas especializadas de valores de historia clínica (antecedentes, resultados y exámenes) correspondientes a un mes y año determinados. Recibe una fecha y construye los nombres de las tablas ANTVALORES, RSVALORES y EXAVALORES con el formato año-mes; si las tres tablas existen en la base de datos, retorna verdadero (1), de lo contrario retorna falso (0). Se usa para validar que la estructura de datos del período está disponible antes de intentar leer o escribir registros de historia clínica de ese mes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ExisteTablaEspecializada';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ExisteTablaEspecializada';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Verifica si existen las tres tablas mensuales especializadas (antecedentes, resultados y exámenes) correspondientes al período de la fecha dada, indicando si la estructura de datos del mes está disponible.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExisteTablaEspecializada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse una fecha válida para derivar año y mes del período a validar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExisteTablaEspecializada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los nombres de tablas siguen el patrón {prefijo}{YYYY}{MM} donde el mes tiene padding sólo cuando se construye en la rama de mes de un dígito (inconsistencia: rama ELSE no aplica padding); Sólo se considera existencia de objetos de tipo tabla de usuario (''U''); La función retorna verdadero únicamente si las tres tablas del período coexisten (validación atómica AND)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExisteTablaEspecializada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'historia clínica; antecedentes; resultados; exámenes; particionamiento mensual de datos clínicos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExisteTablaEspecializada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (función escalar): Si existen las tres tablas (ANTVALORES{año}{mes}, RSVALORES{año}{mes}, EXAVALORES{año}{mes}) como tipo ''U'' en sysobjects, retorna 1; en caso contrario retorna 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExisteTablaEspecializada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LEN(MONTH(@Fecha)) = 1 (meses de un dígito) → Antepone ''0'' al mes para construir nombres de tablas con formato YYYYMM (mes con dos dígitos) else Construye nombres de tablas concatenando año y mes sin padding; si Existen simultáneamente las tres tablas ANTVALORES, RSVALORES y EXAVALORES del período en sysobjects con type=''U'' → Resultado = 1 (verdadero) else Resultado = 0 (falso)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExisteTablaEspecializada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'sys.sysobjects', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExisteTablaEspecializada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExisteTablaEspecializada';
GO
