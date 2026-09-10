
CREATE FUNCTION [dbo].[MezclasLiquidosPorFolioConcatenados](@CodigoPaciente as varchar(25), @NumIngreso as char(10), @NumFolio as varchar(10))
RETURNS varchar(max)
AS
BEGIN
    DECLARE @MezclasLiquidos as varchar(max)

SELECT 
	@MezclasLiquidos = STRING_AGG(
							CONCAT(
							RTRIM(MEZLIQPAC), ' - ', RTRIM(ADMMEZLIQ)), ', ')
							FROM dbo.HCINFLIQD A With(Nolock) 
							INNER JOIN dbo.INUNIFUNC B With(Nolock) ON A.UFUCODIGO=B.UFUCODIGO AND (UFUTIPUNI<>'15' AND UFUTIPUNI<>'24') 
							WHERE A.IPCODPACI= @CodigoPaciente AND NUMINGRES = @NumIngreso AND A.NUMEFOLIO= @NumFolio 
    
    RETURN @MezclasLiquidos
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que devuelve un texto concatenado con todas las mezclas de líquidos (preparaciones intravenosas o soluciones farmacéuticas) registradas para un folio específico de un paciente durante un ingreso hospitalario. Combina el nombre de la mezcla y la dosis administrada en un solo campo de texto separado por comas, consultando el detalle de mezclas de líquidos del historial clínico. Aplica un filtro sobre las unidades funcionales excluyendo ciertos tipos de unidad (tipos 15 y 24), asegurando que solo se consideren áreas de atención válidas según el catálogo de unidades funcionales. Se usa para mostrar en documentos clínicos, recetas o reportes el resumen de mezclas intravenosas asociadas a un folio de atención de un paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'MezclasLiquidosPorFolioConcatenados';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'MezclasLiquidosPorFolioConcatenados';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, en una sola cadena, las mezclas de líquidos y su forma de administración asociadas a un ingreso/folio de un paciente, excluyendo unidades funcionales no asistenciales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MezclasLiquidosPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros de líquidos/infusiones en HCINFLIQD para el paciente, ingreso y folio indicados.; Las unidades funcionales referenciadas deben existir en INUNIFUNC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MezclasLiquidosPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran mezclas asociadas a unidades funcionales cuyo tipo sea distinto de ''15'' y ''24''.; El resultado siempre concatena mezcla y administración con separador '' - '' y elementos separados por '', ''.; El filtrado siempre se restringe a la triada paciente + ingreso + folio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MezclasLiquidosPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Folio clínico; Mezcla de líquidos; Administración de mezcla; Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MezclasLiquidosPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna concatenación ''MEZLIQPAC - ADMMEZLIQ'' separada por '', '' para los registros del paciente, ingreso y folio cuya unidad funcional no sea de tipo ''15'' ni ''24''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MezclasLiquidosPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si UFUTIPUNI = ''15'' o UFUTIPUNI = ''24'' → Excluye el registro del resultado concatenado else Incluye la mezcla y su administración en la cadena de salida', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MezclasLiquidosPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCINFLIQD; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MezclasLiquidosPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MezclasLiquidosPorFolioConcatenados';
GO
