-- =============================================
-- Author:		Hector Rodriguez Rubiano
-- Create date: 13/01/2020
-- Description:	Se encarga de guardar en la tabla de insumos tanto en VIE como en Crystal
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SaveSupplie]
	@Xml xml,
	@UserCode varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Variables para asignar los valores desde el xml para poder guardar
	declare @Id int, @Code varchar(20), @SupplieName varchar(100), @RiskLevelId int, @PBSProduct bit, @SupplieStatus bit,
	@JustificationOfInputs bit, @OsteosynthesisMaterial bit, @Consumption bit, @OptometryDevice bit, @MedicalDevice bit,
	@IsParenteralNutritionSupply bit

	--Tabla en donde realizo todas las validaciones de Crystal
	declare @TableError table(MessageError varchar(200))

	--Variable para concatenar el resultado de los errores de la tabla anterior
	declare @MessageError varchar(max) = ''

	--Código del nivel de riesgo
	declare @InventoryRiskLevelCode varchar(20)

	begin try
		
		--Se obtiene la cabecera del xml(ATC)
		select 
			@Id = t.x.value('Id[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@SupplieName = t.x.value('SupplieName[1]','varchar(100)'),
			@RiskLevelId = t.x.value('RiskLevelId[1]','int'),
			@PBSProduct = t.x.value('PBSProduct[1]','bit'),
			@SupplieStatus = t.x.value('SupplieStatus[1]','bit'),
			@JustificationOfInputs = t.x.value('JustificationOfInputs[1]','bit'),
			@OsteosynthesisMaterial = t.x.value('OsteosynthesisMaterial[1]','bit'),
			@Consumption = t.x.value('Consumption[1]','bit'),
			@OptometryDevice = t.x.value('OptometryDevice[1]','bit'),
			@MedicalDevice = t.x.value('MedicalDevice[1]','bit'),
			@IsParenteralNutritionSupply = t.x.value('IsParenteralNutritionSupply[1]','bit')

		from @Xml.nodes('/Supplie') t(x)
		
		--Si se va a guardar
		if @Id = 0
		begin
			INSERT INTO [Inventory].[InventorySupplie]([Code], [SupplieName], [RiskLevelId], [PBSProduct], [SupplieStatus], [CreationUser], 
						[CreationDate], JustificationOfInputs, OsteosynthesisMaterial, Consumption, OptometryDevice, MedicalDevice, IsParenteralNutritionSupply)
			VALUES( @Code, @SupplieName, @RiskLevelId, @PBSProduct, @SupplieStatus, @UserCode, 
					[Common].[GETDATE](), @JustificationOfInputs, @OsteosynthesisMaterial, @Consumption, @OptometryDevice, @MedicalDevice, @IsParenteralNutritionSupply)

			set @Id = SCOPE_IDENTITY()
		end
		else begin --Si se va actualizar
			UPDATE [Inventory].[InventorySupplie] set [Code] = @Code, [SupplieName] = @SupplieName, [RiskLevelId] = @RiskLevelId, [PBSProduct] = @PBSProduct, [SupplieStatus] = @SupplieStatus
			, [ModificationUser] = @UserCode , [ModificationDate] = [Common].[GETDATE](), 
			JustificationOfInputs = @JustificationOfInputs, OsteosynthesisMaterial = @OsteosynthesisMaterial, Consumption = @Consumption, OptometryDevice = @OptometryDevice, MedicalDevice = @MedicalDevice,
			IsParenteralNutritionSupply = @IsParenteralNutritionSupply 
			WHERE Id = @Id
		end

		--Se actualizan todos lo productos que tengan asociado el insumo
		update Inventory.InventoryProduct set POSProduct = @PBSProduct where SupplieId = @Id
			

		--Se obtiene el código del nivel de riesgo
		select @InventoryRiskLevelCode = Code from Inventory.InventoryRiskLevel where Id = @RiskLevelId

		--Se valida si el nivel de riesgo existe en Crystal
		if(select count(1) from .INIVERIES where CODNIVRIE = @InventoryRiskLevelCode) = 0
		begin
			insert into @TableError values('El nivel de riesgo con código ' + @InventoryRiskLevelCode + ' no existe en Crystal')
		end
		
		--Si hay errores en la tabla
		if(select count(1) from @TableError) > 0
		begin
			--Se obtienen los errores de la tabla
			select @MessageError = MessageError + CHAR(13) + CHAR(10) + @MessageError from @TableError

			--Retorna el error
			select 999 as CodeResult, @MessageError as MessageResult, '' as Code
			return
		end

		--Si el insumo no existe en Crystal se crea
		if(select count(1) from .IHLISTPRO where CODPRODUC = @Code) = 0
		begin
			INSERT INTO .[IHLISTPRO]([CODPRODUC], [DESPRODUC], [NOPOSPROD], [TIPPRODUC], [MANCONPRO], [REGINVACT], 
			[PROESTADO], [PROCONTRO], [TODASPATO], [MANLOCALI], [RETRASOGE],  [CODUSUCRE], [FECUSUCRE], [JUSINMEDI], 
			[CODNIVRIE], [CODJUMEES], [MEDICAMENTONPT], [MATOSTOSIN], [CONSUMPTION], [OPTOMETRYDEVICE]  )
			VALUES(@Code, @SupplieName, case @PBSProduct when 0 then 1 else 0 end, 2, 0, 0, 
			@SupplieStatus, 0, 1, 0, 0, @UserCode, [Common].[GETDATE](), @JustificationOfInputs
			, @InventoryRiskLevelCode, 0, 0, @OsteosynthesisMaterial, @Consumption, @OptometryDevice)
		end
		else begin --Si existe se actualiza
			UPDATE .[IHLISTPRO] set [CODPRODUC] = @Code, [DESPRODUC] = @SupplieName, [NOPOSPROD] = case @PBSProduct when 0 then 1 else 0 end, [TIPPRODUC] = 2
			, [MANCONPRO] = 0, [REGINVACT] = 0,	[PROESTADO] = @SupplieStatus, [PROCONTRO] = 0, [TODASPATO] = 1, [MANLOCALI] = 0, [RETRASOGE] = 0, [CODUSUMOD] = @UserCode
			, [FECUSUMOD] = [Common].[GETDATE](), [JUSINMEDI] = @JustificationOfInputs, [CODNIVRIE] = @InventoryRiskLevelCode, [CODJUMEES] = 0, [MEDICAMENTONPT] = 0,
			MATOSTOSIN = @OsteosynthesisMaterial, CONSUMPTION = @Consumption, OPTOMETRYDEVICE = @OptometryDevice
			where CODPRODUC = @Code
			end

		--Retorna el ok
		select 0 as CodeResult, 'Se guardó correctamente' as MessageResult, @Code as Code
		return
	end try
	begin catch
		--Retorna el error
		select 999 as CodeResult, ERROR_MESSAGE() as MessageResult, '' as Code
		return
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Crea o actualiza un insumo o dispositivo médico en el catálogo maestro de inventario, sincronizando el registro simultáneamente en la base de datos VIE (tabla InventorySupplie) y en el sistema Crystal (tabla IHLISTPRO). Recibe los datos del insumo en formato XML —incluyendo código, nombre, nivel de riesgo, indicadores de producto PBS, material de osteosíntesis, dispositivo médico, dispositivo de optometría, consumo y nutrición parenteral— junto con el usuario que realiza la operación. Además de guardar el insumo, propaga el indicador de producto POS/PBS a todos los productos del inventario asociados al insumo, y valida que el nivel de riesgo asignado exista previamente en Crystal antes de continuar; si no existe, retorna un mensaje de error descriptivo. Es el procedimiento central para la gestión del maestro de insumos en el módulo de inventario hospitalario y farmacéutico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveSupplie';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveSupplie';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Crea o actualiza un insumo en el maestro de inventario y sincroniza simultáneamente la información con el sistema externo Crystal, validando previamente que el nivel de riesgo exista en ambos sistemas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveSupplie';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener un nodo /Supplie con todos los campos esperados (Id, Code, SupplieName, RiskLevelId, flags booleanos); El RiskLevelId enviado debe existir en Inventory.InventoryRiskLevel para poder obtener su Code; Debe existir conectividad y acceso a las tablas externas de Crystal (.INIVERIES y .IHLISTPRO); El UserCode debe ser un usuario válido para registrar auditoría de creación/modificación', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveSupplie';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El campo NOPOSPROD en Crystal se almacena como negación lógica de PBSProduct (PBSProduct=0 → NOPOSPROD=1, y viceversa); Toda creación/actualización de insumo propaga el flag PBSProduct hacia POSProduct en todos los productos asociados (InventoryProduct.SupplieId = Id); El insumo siempre se sincroniza en ambos sistemas: VIE (Inventory.InventorySupplie) y Crystal (IHLISTPRO); Las fechas de creación/modificación se obtienen siempre vía [Common].[GETDATE]() y nunca del cliente; Los insumos en Crystal se crean con TIPPRODUC=2 y banderas de control fijas (MANCONPRO=0, REGINVACT=0, PROCONTRO=0, TODASPATO=1, MANLOCALI=0, RETRASOGE=0, CODJUMEES=0, MEDICAMENTONPT=0); Si el nivel de riesgo no existe en Crystal, no se realiza la sincronización del producto en IHLISTPRO; El procedimiento siempre retorna un resultset con columnas (CodeResult, MessageResult, Code): 0 si éxito, 999 si error', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveSupplie';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'insumo; nivel de riesgo; producto PBS/POS; material de osteosíntesis; dispositivo médico; dispositivo de optometría; nutrición parenteral; justificación de insumos; sincronización con sistema Crystal', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveSupplie';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Inventory.InventorySupplie: Cuando Id=0 en el XML, inserta un nuevo insumo registrando CreationUser=@UserCode y CreationDate=[Common].[GETDATE](); [UPDATE] Inventory.InventorySupplie: Cuando Id<>0, actualiza el insumo por Id registrando ModificationUser=@UserCode y ModificationDate=[Common].[GETDATE](); [UPDATE] Inventory.InventoryProduct: Tras crear/actualizar el insumo, actualiza POSProduct=@PBSProduct en todos los productos cuyo SupplieId coincide con el insumo procesado; [INSERT] IHLISTPRO: Cuando no existe registro en IHLISTPRO con CODPRODUC=@Code, inserta el insumo en Crystal con NOPOSPROD invertido respecto a PBSProduct y banderas de control fijas; [UPDATE] IHLISTPRO: Cuando ya existe en IHLISTPRO con CODPRODUC=@Code, actualiza la descripción, nivel de riesgo, estado y flags clínicos del insumo en Crystal; [RETURN_RESULT] resultset: Si el nivel de riesgo no existe en INIVERIES de Crystal, retorna CodeResult=999 con mensaje ''El nivel de riesgo con código X no existe en Crystal'' y aborta; [RETURN_RESULT] resultset: Al finalizar correctamente retorna CodeResult=0, MessageResult=''Se guardó correctamente'' y el Code del insumo; [RETURN_RESULT] resultset: Ante cualquier excepción capturada en el CATCH retorna CodeResult=999 con ERROR_MESSAGE() y Code vacío', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveSupplie';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Id leído del XML = 0 → Inserta un nuevo insumo en Inventory.InventorySupplie y obtiene el Id generado vía SCOPE_IDENTITY else Actualiza el insumo existente en Inventory.InventorySupplie por Id, registrando usuario y fecha de modificación; si El nivel de riesgo (Code obtenido de InventoryRiskLevel) no existe en la tabla externa INIVERIES de Crystal → Agrega mensaje de error a la tabla temporal y retorna CodeResult=999 con el mensaje, abortando el flujo else Continúa con la sincronización del insumo hacia Crystal; si El producto (CODPRODUC = Code) no existe en la tabla IHLISTPRO de Crystal → Inserta el insumo en IHLISTPRO con valores fijos de control (TIPPRODUC=2, MANCONPRO=0, REGINVACT=0, PROCONTRO=0, TODASPATO=1, etc.) else Actualiza el registro en IHLISTPRO con los nuevos valores del insumo; si Ocurre cualquier excepción en el bloque TRY → Retorna CodeResult=999 con ERROR_MESSAGE() y Code vacío', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveSupplie';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryRiskLevel; Inventory.InventorySupplie; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveSupplie';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveSupplie';
-- GO
