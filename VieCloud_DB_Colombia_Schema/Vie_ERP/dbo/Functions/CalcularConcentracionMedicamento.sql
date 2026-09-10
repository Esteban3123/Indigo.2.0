CREATE FUNCTION [dbo].[CalcularConcentracionMedicamento] 
(
	@TipoFormulaMedica As Integer,
	@PESTOTMED as numeric(18,2),
	@CODUNIPES as varchar(20),
	@VOLTOTMED as numeric(18,2),
	@CODUNIVOL as varchar(20)
)
RETURNS numeric(18,1) --Si necesitan mas decimales cambiar aqui
AS
Begin
		declare @ConcentracionMedicamento as numeric(18,2)
		

			If @TipoFormulaMedica = 1 Begin		--peso
					   
				select @ConcentracionMedicamento = Cast(REPLACE(@PESTOTMED,',','.') As Float) 

			end If @TipoFormulaMedica = 2 begin   --volumen
				select @ConcentracionMedicamento = Cast(REPLACE(@VOLTOTMED,',','.') As  Float)

			end If @TipoFormulaMedica = 3 begin   --peso-volumen
				Select @ConcentracionMedicamento = (select top 1 CASE WHEN CODUNIMED=@CODUNIPES THEN CAST(REPLACE(@PESTOTMED,',','.') AS FLOAT) WHEN CODUNIMED=@CODUNIVOL THEN CAST(REPLACE(@VOLTOTMED,',','.') AS FLOAT) END FROM dbo.INUNIMEDI WHERE CODUNIMED IN (@CODUNIPES)) --@CODUNIVOL
							   
			end If @TipoFormulaMedica = 4 begin   --unidad de administracion
				set @ConcentracionMedicamento = CAST(1 AS FLOAT) 
			end

		Return @ConcentracionMedicamento 
end
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la concentración de un medicamento según el tipo de fórmula médica indicada: por peso, por volumen, por relación peso-volumen o por unidad de administración. Recibe como parámetros el peso total del medicamento, el volumen total y los códigos de sus respectivas unidades de medida (peso y volumen), y devuelve un valor numérico que representa la concentración. Consulta la tabla de unidades de medida (INUNIMEDI) para resolver la concentración correcta cuando la fórmula es del tipo peso-volumen, comparando las unidades ingresadas. Se utiliza en el proceso de formulación y dispensación de medicamentos para determinar la dosis o concentración que debe administrarse al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CalcularConcentracionMedicamento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CalcularConcentracionMedicamento';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina el valor de concentración de un medicamento según el tipo de fórmula médica (peso, volumen, peso-volumen o unidad de administración).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalcularConcentracionMedicamento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las unidades de medida referenciadas deben existir en el catálogo de unidades de medida de medicamentos; Los valores de peso y volumen deben ser numéricos válidos (admite coma decimal que se normaliza a punto); Debe indicarse un tipo de fórmula médica reconocido (1=peso, 2=volumen, 3=peso-volumen, 4=unidad de administración)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalcularConcentracionMedicamento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La concentración se interpreta de forma distinta según el tipo de fórmula médica (1=peso, 2=volumen, 3=peso-volumen, 4=unidad de administración); Para fórmulas de unidad de administración la concentración siempre es 1; Las cantidades numéricas se normalizan reemplazando coma por punto antes de convertir a número; Para tipos de fórmula no contemplados (distinto de 1,2,3,4) la función retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalcularConcentracionMedicamento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'medicamento; concentración de medicamento; fórmula médica (peso, volumen, peso-volumen, unidad de administración); unidades de medida', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalcularConcentracionMedicamento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (valor escalar): Cuando tipo de fórmula = 1, retorna el peso total convertido a numérico; [RETURN_RESULT] (valor escalar): Cuando tipo de fórmula = 2, retorna el volumen total convertido a numérico; [RETURN_RESULT] (valor escalar): Cuando tipo de fórmula = 3, consulta INUNIMEDI por la unidad de peso y retorna el peso si la unidad coincide con la de peso, o el volumen si coincide con la de volumen; [RETURN_RESULT] (valor escalar): Cuando tipo de fórmula = 4 (unidad de administración), retorna 1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalcularConcentracionMedicamento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de fórmula médica = 1 (peso) → Devuelve el peso total del medicamento como concentración; si Tipo de fórmula médica = 2 (volumen) → Devuelve el volumen total del medicamento como concentración; si Tipo de fórmula médica = 3 (peso-volumen) → Consulta la unidad en INUNIMEDI y devuelve peso si la unidad coincide con la unidad de peso, o volumen si coincide con la unidad de volumen; si Tipo de fórmula médica = 4 (unidad de administración) → Devuelve 1 como concentración fija', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalcularConcentracionMedicamento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalcularConcentracionMedicamento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalcularConcentracionMedicamento';
GO
