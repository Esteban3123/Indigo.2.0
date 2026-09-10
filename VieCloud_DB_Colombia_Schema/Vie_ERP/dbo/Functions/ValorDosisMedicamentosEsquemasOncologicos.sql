
CREATE FUNCTION [dbo].[ValorDosisMedicamentosEsquemasOncologicos] 
(
	@TipoFactor As int,
	@DosisMedicamentoParametrizado As numeric(18,2),
	@SCT As numeric(18,2),
	@Peso  As numeric(18,2),
	@IndiceMasaCorporal As numeric(18,2)
)
RETURNS numeric(18,1) --Si necesitan mas decimales cambiar aqui
AS
Begin
		declare @DosisMedicamento as numeric(18,2)

			If @TipoFactor = 1 Begin		--Superficie Corporal Total
				   set @DosisMedicamento = @SCT * @DosisMedicamentoParametrizado

			end If @TipoFactor = 2 begin   --Indice de Masa Corporal
					set @DosisMedicamento = @IndiceMasaCorporal * @DosisMedicamentoParametrizado

			end If @TipoFactor = 3 begin   --Peso
					set @DosisMedicamento = @Peso * @DosisMedicamentoParametrizado

			end If @TipoFactor = 4 begin   --Sin Factor
					set @DosisMedicamento = @DosisMedicamentoParametrizado
			end

		Return @DosisMedicamento 
end
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la dosis real de un medicamento oncológico aplicando el factor de ajuste correspondiente al paciente. Según el tipo de factor (1=Superficie Corporal Total, 2=Índice de Masa Corporal, 3=Peso, 4=Sin factor), multiplica la dosis parametrizada del esquema oncológico por el valor antropométrico del paciente para obtener la dosis personalizada. Es utilizada en la dispensación y prescripción de quimioterapia, donde la dosis varía según las características físicas del paciente. Retorna la dosis calculada en miligramos u otras unidades con un decimal de precisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ValorDosisMedicamentosEsquemasOncologicos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ValorDosisMedicamentosEsquemasOncologicos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la dosis efectiva de un medicamento de un esquema oncológico aplicando el factor antropométrico correspondiente (superficie corporal, IMC, peso o sin factor) sobre la dosis parametrizada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValorDosisMedicamentosEsquemasOncologicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse un tipo de factor válido (1, 2, 3 o 4); cualquier otro valor produce dosis nula; La dosis parametrizada debe estar definida; Si el factor es 1 se requiere superficie corporal total; si es 2 índice de masa corporal; si es 3 peso del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValorDosisMedicamentosEsquemasOncologicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado final se trunca/redondea a un decimal por el tipo de retorno numeric(18,1); Solo existen cuatro modalidades de cálculo de dosis: por SCT, por IMC, por Peso o sin factor; Si el TipoFactor no coincide con 1,2,3 ni 4 la función retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValorDosisMedicamentosEsquemasOncologicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Esquema oncológico; Dosis de medicamento; Superficie Corporal Total (SCT); Índice de Masa Corporal (IMC); Peso del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValorDosisMedicamentosEsquemasOncologicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando TipoFactor=1 retorna SCT * dosis parametrizada (cálculo por Superficie Corporal Total); [RETURN_RESULT] N/A: Cuando TipoFactor=2 retorna IMC * dosis parametrizada (cálculo por Índice de Masa Corporal); [RETURN_RESULT] N/A: Cuando TipoFactor=3 retorna Peso * dosis parametrizada (cálculo por peso del paciente); [RETURN_RESULT] N/A: Cuando TipoFactor=4 retorna la dosis parametrizada sin aplicar factor multiplicador', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValorDosisMedicamentosEsquemasOncologicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TipoFactor = 1 → Dosis = SCT * DosisParametrizada (Superficie Corporal Total); si TipoFactor = 2 → Dosis = IMC * DosisParametrizada (Índice de Masa Corporal); si TipoFactor = 3 → Dosis = Peso * DosisParametrizada; si TipoFactor = 4 → Dosis = DosisParametrizada (sin factor)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValorDosisMedicamentosEsquemasOncologicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValorDosisMedicamentosEsquemasOncologicos';
GO
