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