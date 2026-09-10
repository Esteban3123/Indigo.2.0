
CREATE FUNCTION [MixingStation].[CalculationQuantityMaterialRaw] 
(
	@TipoFormulaMedica as integer, --Contendra el tipo de formula 1:Peso ; 2:Volumen ; 3:Peso Volumen ; 4:Unidad de administración
	@CodigoProducto as varchar(100),    --Codigo Producto
	@DosisOrden as decimal(18,2), --Dosis Orden Peso
	@UnidadMedidaOrden as varchar(10) --Unidad Orden Peso
)
RETURNS decimal(18, 9)
AS
Begin
		declare @UnidadMedidaProducto as varchar(10) --Valor del codigo de la Unidad de Medida a la que debemos llevar el medicamento, que esta es la que esta parametrizada en el formulario de Medicamentos.
		declare @DosisNueva as decimal(18, 9) = 0 --Valor de la Dosis nueva luego de hacer la conversión.
		declare @Multiplicador Decimal(18, 6) = 1
		
		select TOP 1 @UnidadMedidaProducto = case when a.FormulationType = 1 then up.Code else uv.Code end
		from Inventory.ATC a 
		left Join Inventory.InventoryMeasurementUnit up on a.WeightMeasureUnit = up.Id  
		left join Inventory.InventoryMeasurementUnit uv on a.VolumeMeasureUnit = uv.Id
		where a.Code = @CodigoProducto

		--return @UnidadMedidaOrden
		
		If @TipoFormulaMedica = 1  Begin		--1:Peso

			If @UnidadMedidaOrden = '001' and @UnidadMedidaProducto = '002' --Gramos a Miligramos
				set @Multiplicador = 1000
			else if @UnidadMedidaOrden = '001' and @UnidadMedidaProducto = '008' --Gramos a Kilogramos
				set @Multiplicador = 0.001
			else if @UnidadMedidaOrden = '001' and @UnidadMedidaProducto = '009' --Microgramos
				set @Multiplicador = 1000000

			else if @UnidadMedidaOrden = '002' and @UnidadMedidaProducto = '001' --Miligramos a Gramos
				set @Multiplicador = 0.001
			else if @UnidadMedidaOrden = '002' and @UnidadMedidaProducto = '008' --Miligramos a Kilogramos
				set @Multiplicador = 0.000001
			else if @UnidadMedidaOrden = '002' and @UnidadMedidaProducto = '009' --Miligramos a Microgramos
				set @Multiplicador = 1000

			else if @UnidadMedidaOrden = '008' and @UnidadMedidaProducto = '001' --Kilogramos a Gramos
				set @Multiplicador = 1000
			else if @UnidadMedidaOrden = '008' and @UnidadMedidaProducto = '002' --Kilogramos a Miligramos
				set @Multiplicador = 1000000
			else if @UnidadMedidaOrden = '008' and @UnidadMedidaProducto = '001' --Kilogramos a Gramos
				set @Multiplicador = 1000000000

			else if @UnidadMedidaOrden = '009' and @UnidadMedidaProducto = '001' --Microgramos a Gramos
				set @Multiplicador = 0.000001
			else if @UnidadMedidaOrden = '009' and @UnidadMedidaProducto = '002' --Microgramos a Miligramos
				set @Multiplicador = 0.001
			else if @UnidadMedidaOrden = '009' and @UnidadMedidaProducto = '008' --Microgramos a Kilogramos
				set @Multiplicador = 0.000000001

			set @DosisNueva = @DosisOrden * @Multiplicador

			--If @UnidadMedidaProducto = '001'  --Gramo(s)
			--	set @DosisNueva = Case @UnidadMedidaOrden when '002' Then  @DosisOrden / 1000  when '009' Then @DosisOrden / 1000000 else @DosisOrden end
			--else if @UnidadMedidaProducto = '002'  --Miligramos(s)
			--	set @DosisNueva = Case @UnidadMedidaOrden when '001' Then  @DosisOrden * 1000  when '009' Then @DosisOrden / 1000 else @DosisOrden end 
			--else if @UnidadMedidaProducto = '009'  --Microgramo(s)								
			--	set @DosisNueva = Case @UnidadMedidaOrden when '001' Then  @DosisOrden * 1000000  when '002' Then @DosisOrden * 1000 else @DosisOrden end 			
			--else  
			--	set @DosisNueva = @DosisOrden  
											
		end else If @TipoFormulaMedica = 2 begin   --volumen
			If @UnidadMedidaOrden = '011' and @UnidadMedidaProducto = '019' --Litros a Mililitros
				set @Multiplicador = 1000
			else if @UnidadMedidaOrden = '011' and @UnidadMedidaProducto = '015' --Litros a cc
				set @Multiplicador = 1000
			else if @UnidadMedidaOrden = '011' and @UnidadMedidaProducto = '024' --Litros a Onzas
				set @Multiplicador = 33.814

			If @UnidadMedidaOrden = '019' and @UnidadMedidaProducto = '011' --Mililitros as Litros
				set @Multiplicador = 0.001
			If @UnidadMedidaOrden = '019' and @UnidadMedidaProducto = '015' --Mililitros as cc
				set @Multiplicador = 1
			If @UnidadMedidaOrden = '019' and @UnidadMedidaProducto = '024' --Mililitros as Onzas
				set @Multiplicador = 0.033814

			If @UnidadMedidaOrden = '015' and @UnidadMedidaProducto = '011' --cc as Litros
				set @Multiplicador = 0.001
			If @UnidadMedidaOrden = '015' and @UnidadMedidaProducto = '019' --cc as Mililitros
				set @Multiplicador = 1
			If @UnidadMedidaOrden = '015' and @UnidadMedidaProducto = '024' --cc as Onzas
				set @Multiplicador = 0.033814

			If @UnidadMedidaOrden = '024' and @UnidadMedidaProducto = '011' --Onzas as Litros
				set @Multiplicador = 0.0295735
			If @UnidadMedidaOrden = '024' and @UnidadMedidaProducto = '015' --Onzas as cc
				set @Multiplicador = 29.5735
			If @UnidadMedidaOrden = '024' and @UnidadMedidaProducto = '019' --Onzas as Mililitros
				set @Multiplicador = 29.5735

			set @DosisNueva = @DosisOrden * @Multiplicador
		end else If @TipoFormulaMedica = 3 begin   --peso-volumen
			
			--Se valida el peso
			--If @UnidadMedidaProducto = '001'  --Gramo(s)
			--	set @DosisNueva = Case @UnidadMedidaOrden when '002' Then  @DosisOrden / 1000  when '009' Then @DosisOrden / 1000000  end
			--else if @UnidadMedidaProducto = '002'  --Miligramos(s)
			--	set @DosisNueva = Case @UnidadMedidaOrden when '001' Then  @DosisOrden * 1000  when '009' Then @DosisOrden / 1000 end 
			--else if @UnidadMedidaProducto = '009'  --Microgramo(s)								
			--	set @DosisNueva = Case @UnidadMedidaOrden when '001' Then  @DosisOrden * 1000000  when '002' Then @DosisOrden * 1000 end 
			----else  
			--	--set @DosisNueva = @DosisOrden   
							
			----Se valida el volumen
			--IF @DosisNueva IS NULL or  @DosisNueva = 0 begin
			--	set @DosisNueva = @DosisOrden
			--end	
			
			If @UnidadMedidaOrden = '001' and @UnidadMedidaProducto = '002' --Gramos a Miligramos
				set @Multiplicador = 1000
			else if @UnidadMedidaOrden = '001' and @UnidadMedidaProducto = '008' --Gramos a Kilogramos
				set @Multiplicador = 0.001
			else if @UnidadMedidaOrden = '001' and @UnidadMedidaProducto = '009' --Microgramos
				set @Multiplicador = 1000000

			else if @UnidadMedidaOrden = '002' and @UnidadMedidaProducto = '001' --Miligramos a Gramos
				set @Multiplicador = 0.001
			else if @UnidadMedidaOrden = '002' and @UnidadMedidaProducto = '008' --Miligramos a Kilogramos
				set @Multiplicador = 0.000001
			else if @UnidadMedidaOrden = '002' and @UnidadMedidaProducto = '009' --Miligramos a Microgramos
				set @Multiplicador = 1000

			else if @UnidadMedidaOrden = '008' and @UnidadMedidaProducto = '001' --Kilogramos a Gramos
				set @Multiplicador = 1000
			else if @UnidadMedidaOrden = '008' and @UnidadMedidaProducto = '002' --Kilogramos a Miligramos
				set @Multiplicador = 1000000
			else if @UnidadMedidaOrden = '008' and @UnidadMedidaProducto = '001' --Kilogramos a Gramos
				set @Multiplicador = 1000000000

			else if @UnidadMedidaOrden = '009' and @UnidadMedidaProducto = '001' --Microgramos a Gramos
				set @Multiplicador = 0.000001
			else if @UnidadMedidaOrden = '009' and @UnidadMedidaProducto = '002' --Microgramos a Miligramos
				set @Multiplicador = 0.001
			else if @UnidadMedidaOrden = '009' and @UnidadMedidaProducto = '008' --Microgramos a Kilogramos
				set @Multiplicador = 0.000000001

			set @DosisNueva = @DosisOrden * @Multiplicador
							
		end else If @TipoFormulaMedica = 4 begin   --unidad de administracion
			set @DosisNueva = @DosisOrden
			--Me dicen que Nunca Aplicara esta Unidad de Medida
		end else begin --Por si son insumos - productos
			set @DosisNueva = @DosisOrden
		end 

		Return @DosisNueva  
end
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de conversión de unidades para el cálculo de la cantidad de materia prima necesaria en la estación de mezclas (MixingStation). Recibe el tipo de fórmula médica (por peso, por volumen, peso-volumen o unidad de administración), el código del medicamento o insumo, la dosis ordenada y la unidad de medida de la orden, y devuelve la dosis convertida a la unidad de medida parametrizada en el catálogo del producto (ATC de Inventario). Para determinar la unidad objetivo del medicamento, consulta el catálogo maestro de medicamentos (Inventory.ATC) junto con el catálogo de unidades de medida (Inventory.InventoryMeasurementUnit), diferenciando si el producto se maneja por peso o por volumen según su tipo de formulación. Aplica los factores de conversión correspondientes entre unidades de masa (gramos, miligramos, kilogramos, microgramos) y de volumen (litros, mililitros, cc, onzas), garantizando que la cantidad de materia prima entregada a la preparación farmacéutica sea precisa y coherente con lo prescrito en la orden médica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'FUNCTION', @level1name = N'CalculationQuantityMaterialRaw';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'FUNCTION', @level1name = N'CalculationQuantityMaterialRaw';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Convierte una dosis expresada en la unidad de la orden médica a la unidad de medida parametrizada del producto, aplicando factores de conversión según el tipo de fórmula (peso, volumen, peso-volumen o unidad de administración).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculationQuantityMaterialRaw';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El producto identificado por @CodigoProducto debe existir en Inventory.ATC para obtener su unidad de medida (de peso o volumen según FormulationType).; Los códigos de unidad de medida en Inventory.InventoryMeasurementUnit deben corresponder a la codificación esperada: ''001'' Gramos, ''002'' Miligramos, ''008'' Kilogramos, ''009'' Microgramos, ''011'' Litros, ''015'' cc, ''019'' Mililitros, ''024'' Onzas.; @TipoFormulaMedica debe indicar 1=Peso, 2=Volumen, 3=Peso-Volumen, 4=Unidad de administración.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculationQuantityMaterialRaw';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El multiplicador inicia en 1, por lo que si no se cumple ninguna combinación de unidades reconocida la dosis se devuelve sin alterar.; La unidad del producto se determina por FormulationType del ATC: tipo 1 usa WeightMeasureUnit, otros usan VolumeMeasureUnit.; Para tipo de fórmula peso o peso-volumen se usa exactamente la misma tabla de factores de conversión.; El factor para Kilogramos→Gramos aparece duplicado en el código (la segunda asignación 1000000000 está etiquetada también como ''Kilogramos a Gramos'' pero es inalcanzable por la condición previa idéntica).; En el bloque de volumen, los condicionales para los códigos ''019'', ''015'' y ''024'' están escritos con IF independientes (no else if), por lo que la última coincidencia evaluada prevalece sobre las anteriores.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculationQuantityMaterialRaw';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dosis de orden médica; Tipo de fórmula medicamentosa (peso, volumen, peso-volumen, unidad de administración); Conversión de unidades de medida farmacéuticas; Clasificación ATC de medicamentos; Unidades de peso (gramos, miligramos, microgramos, kilogramos); Unidades de volumen (litros, mililitros, cc, onzas); Central de mezclas (MixingStation)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculationQuantityMaterialRaw';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna @DosisOrden multiplicada por el factor de conversión correspondiente a la pareja (unidad orden, unidad producto); si no hay coincidencia, el multiplicador queda en 1 y se retorna la dosis original.; [RETURN_RESULT] : Cuando @TipoFormulaMedica = 4 (unidad de administración) o cualquier otro valor no contemplado (insumos/productos), retorna @DosisOrden sin conversión.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculationQuantityMaterialRaw';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Inventory.ATC.FormulationType = 1 → Toma como unidad del producto la unidad de peso (WeightMeasureUnit) else Toma como unidad del producto la unidad de volumen (VolumeMeasureUnit); si @TipoFormulaMedica = 1 (Peso) → Aplica conversiones entre Gramos/Miligramos/Kilogramos/Microgramos (códigos 001, 002, 008, 009); si @TipoFormulaMedica = 2 (Volumen) → Aplica conversiones entre Litros/Mililitros/cc/Onzas (códigos 011, 019, 015, 024) usando factores como 1000, 33.814, 29.5735, etc.; si @TipoFormulaMedica = 3 (Peso-Volumen) → Aplica las mismas conversiones de peso que el tipo 1 (códigos 001/002/008/009); si @TipoFormulaMedica = 4 (Unidad de administración) → Devuelve @DosisOrden sin conversión (según comentario: ''Nunca Aplicara esta Unidad de Medida''); si @TipoFormulaMedica no coincide con 1,2,3 ni 4 → Devuelve @DosisOrden sin conversión (caso insumos/productos)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculationQuantityMaterialRaw';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ATC; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculationQuantityMaterialRaw';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculationQuantityMaterialRaw';
GO
