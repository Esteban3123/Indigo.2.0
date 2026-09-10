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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la concentración de un medicamento utilizado en esquemas oncológicos, según el tipo de fórmula médica indicada: por peso del paciente, por volumen, por relación peso-volumen o por unidad de administración. Recibe como parámetros el tipo de fórmula, el peso total del medicamento, el volumen total, sus respectivas unidades de medida y la unidad de medida definida en el esquema oncológico del producto. Cuando el tipo es peso-volumen, consulta la tabla maestra de unidades de medida (INUNIMEDI) para determinar si corresponde usar el valor de peso o de volumen según la unidad que coincida con la del esquema. Retorna un valor numérico que representa la concentración final del medicamento para ser usada en la dispensación y preparación de tratamientos oncológicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CalcularConcentracionMedicamentoEsquemasOncologicos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CalcularConcentracionMedicamentoEsquemasOncologicos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina la concentración del medicamento oncológico a aplicar según el tipo de fórmula médica (peso, volumen, peso-volumen o unidad de administración).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalcularConcentracionMedicamentoEsquemasOncologicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El tipo de fórmula médica debe ser uno de los valores 1 (peso), 2 (volumen), 3 (peso-volumen) o 4 (unidad de administración).; Para fórmula tipo 3, las unidades de medida de peso y/o volumen deben existir en el catálogo de unidades de medida.; Los valores numéricos de peso y volumen pueden venir con coma decimal y se convierten a punto antes de castear a float.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalcularConcentracionMedicamentoEsquemasOncologicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La concentración se devuelve siempre como numeric(18,1).; Para tipo 4 la concentración siempre es 1, independientemente de los demás parámetros.; Para tipo 3 la concentración solo se asigna si alguna unidad (peso o volumen) coincide con la unidad de medida definida en el esquema del producto.; Los separadores decimales con coma se normalizan a punto antes de la conversión a float.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalcularConcentracionMedicamentoEsquemasOncologicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento oncológico; Esquema oncológico; Concentración de medicamento; Tipo de fórmula médica (peso, volumen, peso-volumen, unidad de administración); Unidad de medida', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalcularConcentracionMedicamentoEsquemasOncologicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RETURN: Cuando tipo fórmula = 1 (peso), retorna el peso total del medicamento como concentración.; [RETURN_RESULT] RETURN: Cuando tipo fórmula = 2 (volumen), retorna el volumen total del medicamento como concentración.; [RETURN_RESULT] RETURN: Cuando tipo fórmula = 3 (peso-volumen), retorna el peso si la unidad de peso coincide con la unidad de medida del esquema, o el volumen si la unidad de volumen coincide con la unidad de medida del esquema.; [RETURN_RESULT] RETURN: Cuando tipo fórmula = 4 (unidad de administración), retorna siempre 1 como concentración.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalcularConcentracionMedicamentoEsquemasOncologicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo fórmula médica = 1 (peso) → Concentración = peso total del medicamento.; si Tipo fórmula médica = 2 (volumen) → Concentración = volumen total del medicamento.; si Tipo fórmula médica = 3 (peso-volumen) → Selecciona peso o volumen según cuál unidad de medida coincida con la unidad del esquema del producto, validando contra el catálogo de unidades de medida.; si Tipo fórmula médica = 4 (unidad de administración) → Concentración = 1 (constante).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalcularConcentracionMedicamentoEsquemasOncologicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalcularConcentracionMedicamentoEsquemasOncologicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalcularConcentracionMedicamentoEsquemasOncologicos';
GO
