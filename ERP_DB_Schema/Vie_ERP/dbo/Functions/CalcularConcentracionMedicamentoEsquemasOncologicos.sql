-- =============================================
-- Author:		<Author,Juan David Patiño Cabrera,Name>
-- Create date: <Create Date, 23-07-2020,>
-- Description:	<Description, Carga la concentracion de los medicamentos Oncologicos ,>
-- =============================================
CREATE FUNCTION [dbo].[CalcularConcentracionMedicamentoEsquemasOncologicos] 
(
	@TipoFormulaMedica As Integer,
	@PESTOTMED as numeric(18,2),
	@CODUNIPES as varchar(20),
	@VOLTOTMED as numeric(18,2),
	@CODUNIVOL as varchar(20),
	@UnidadMedidadEsquemaProducto as varchar(20)
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
				 
						set @ConcentracionMedicamento = (SELECT ConcentracionMedicamento FROM (
							   select ConcentracionMedicamento =  CASE WHEN CODUNIMED=@CODUNIPES and  @CODUNIPES = @UnidadMedidadEsquemaProducto THEN CAST(REPLACE(@PESTOTMED,',','.') AS FLOAT) 
																	   WHEN CODUNIMED=@CODUNIVOL and  @CODUNIVOL = @UnidadMedidadEsquemaProducto THEN CAST(REPLACE(@VOLTOTMED,',','.') AS FLOAT) END 
															   FROM dbo.INUNIMEDI WHERE CODUNIMED IN (@CODUNIPES,@CODUNIVOL) 
								) as TMP WHERE TMP.ConcentracionMedicamento IS NOT NULL)
 
			end If @TipoFormulaMedica = 4 begin   --unidad de administracion
						set @ConcentracionMedicamento = CAST(1 AS FLOAT) 

			end

		Return @ConcentracionMedicamento 
end