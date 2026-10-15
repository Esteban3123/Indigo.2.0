
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