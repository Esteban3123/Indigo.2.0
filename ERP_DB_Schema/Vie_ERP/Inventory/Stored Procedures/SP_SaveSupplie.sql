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