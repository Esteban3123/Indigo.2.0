
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