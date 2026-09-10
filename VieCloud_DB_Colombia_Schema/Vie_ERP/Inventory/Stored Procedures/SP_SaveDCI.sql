-- =============================================
-- Author:		Daniel Eduardo Arévalo
-- Create date: 10/04/2019
-- Description:	Procedimiento que se encarga de guardar, actualizar la liquidación de honorarios médicos
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SaveDCI] 
    @DCIXml as Xml,
	@ListDeleteMedicaments as Xml,
	@ListDeleteDrugActive as Xml,
	@ListDeleteLethalDoseLimits as Xml,
	@ListDeleteRisksDescription as Xml,
	@ListDeleteRiskFactor as Xml,
	@OperatingUnitId as Int,
	@CodeUser as varchar(20)
AS
BEGIN

	declare @ListDeleteMedicamentsTmp table(Id int)

	declare @ListDeleteDrugActiveTmp table(Id int)

	declare @ListDeleteLethalDoseLimitsTmp table(Id int)
	
	--Se declaran las variables para obtener la cabecera
	declare @Id int, @Code varchar(20), @Name varchar(200), @Status bit, @Combined bit, @TypeWarning bit
	
	--Tabla temporal de DrugInteraction
	declare @DrugInteractionTmp table(Id int, ATCEntityId int, RiskLevel tinyint, [Description] varchar(max), TempId INT)

	-- Tabla Temporal de DCI vs. ATC
	declare @DrugActiveTmp table(id int, ATCEntityId int, ChangeTracker varchar(10))

	--Tabla temporal HighRiskDrugs
	declare @HighRiskDrugsTmp table (Id int, InventoryRiskLevelId int, Observation varchar(500), ChangeTracker varchar(10))
	
	declare @DCIATCEntityTmp table (Id int, ATCEntityId int, ChangeTracker varchar(10))

	declare @LethalDoseLimitsTmp table (Id int, StartAge int, StartAgeUnit int, EndAge int, EndAgeUnit int, StartWeight decimal(5, 2), StartWeightUnit int, EndWeight decimal(5, 2), EndWeightUnit int, MaxDoseConcentration decimal(12, 2), MaxDoseConcentrationUnitId int, Max24HourConcentration decimal(12, 2), Max24HourConcentrationUnitId int, LethalDoseConcentration decimal(12, 2), LethalDoseConcentrationUnitId int, Lethal24HourConcentration decimal(6, 2), Lethal24HourConcentrationUnitId int, DCIId int, ChangeTracker varchar(10))

	declare @DCIRiskFactorTmp table (Id int, DCId int, RiskFactorId int, ChangeTracker varchar(10))

	declare @RisksDescriptionTmp table (Id int, DCIId int, TagRiskType int, TagIncludedDescription varchar(max), ChangeTracker varchar(10))

	declare @IHPARAMDCI table (ID int, CODDCIMED varchar(20), TIPO int, ChangeTracker varchar(10))
	
	BEGIN TRY
		
		--Se obtiene DCI
		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@Name = t.x.value('Name[1]','varchar(200)'),
			@Status = t.x.value('Status[1]','bit'),
			@Combined = t.x.value('Combined[1]','bit'),
			@TypeWarning = t.x.value('TypeWarning[1]','bit')
		FROM @DCIXml.nodes('/DCI') t(x)

		declare @IsCreated bit
		set @IsCreated = iif(@Id = 0, 1, 0)

		--Se obtiene @DrugInteractionTmp
		INSERT INTO  @DrugInteractionTmp
		SELECT 
			t.x.value('Id[1]','int'),
			t.x.value('ATCEntityId[1]','int'),
			t.x.value('RiskLevel[1]','int'),
			t.x.value('Description[1]','varchar(max)'),
			t.x.value('TempId[1]','int')
		FROM @DCIXml.nodes('/DCI/DrugInteraction') t(x)

		
		--Se obtiene @DCIATCEntityTmp
		INSERT INTO  @DrugActiveTmp
		SELECT 
			t.x.value('Id[1]','int'),
			t.x.value('ATCEntityId[1]','int'),
			t.x.value('ChangeTracker[1]','varchar(10)')
		FROM @DCIXml.nodes('/DCI/DrugActive') t(x)

		INSERT INTO  @DCIATCEntityTmp
		SELECT 
			t.x.value('Id[1]','int'),
			t.x.value('ATCEntityId[1]','int'),
			t.x.value('ChangeTracker[1]','varchar(10)')
		FROM @DCIXml.nodes('/DCI/DCIATCEntity') t(x)
		
		INSERT INTO @LethalDoseLimitsTmp
		SELECT 
			t.x.value('Id[1]', 'int'),
			t.x.value('StartAge[1]', 'int'),
			t.x.value('StartAgeUnit[1]', 'int'),
			t.x.value('EndAge[1]', 'int'),
			t.x.value('EndAgeUnit[1]', 'int'),
			CAST(REPLACE(t.x.value('StartWeight[1]', 'varchar(20)'), ',', '.') AS decimal(5,2)),
			t.x.value('StartWeightUnit[1]', 'int'),
			CAST(REPLACE(t.x.value('EndWeight[1]', 'varchar(20)'), ',', '.') AS decimal(5,2)),
			t.x.value('EndWeightUnit[1]', 'int'),
			CAST(REPLACE(t.x.value('MaxDoseConcentration[1]', 'varchar(20)'), ',', '.') AS decimal(12,2)),
			t.x.value('MaxDoseConcentrationUnitId[1]', 'int'),
			CAST(REPLACE(t.x.value('Max24HourConcentration[1]', 'varchar(20)'), ',', '.') AS decimal(12,2)),
			t.x.value('Max24HourConcentrationUnitId[1]', 'int'),
			CAST(REPLACE(t.x.value('LethalDoseConcentration[1]', 'varchar(20)'), ',', '.') AS decimal(12,2)),
			t.x.value('LethalDoseConcentrationUnitId[1]', 'int'),
			CAST(REPLACE(t.x.value('Lethal24HourConcentration[1]', 'varchar(20)'), ',', '.') AS decimal(6,2)),
			t.x.value('Lethal24HourConcentrationUnitId[1]', 'int'),
			t.x.value('DCIId[1]', 'int'),
			t.x.value('ChangeTracker[1]','varchar(10)')
		FROM @DCIXml.nodes('/DCI/LethalDoseLimits') t(x)
		
		INSERT INTO @DCIRiskFactorTmp
		SELECT 
			t.x.value('Id[1]','int'),
			t.x.value('DCIId[1]','int'),
			t.x.value('RiskFactorId[1]','int'),
			t.x.value('ChangeTracker[1]','varchar(10)')
		FROM @DCIXml.nodes('DCI/DCIRiskFactor') t(x)

		INSERT INTO @RisksDescriptionTmp
		SELECT
			t.x.value('Id[1]','int'),
			t.x.value('DCIId[1]','int'),
			t.x.value('TagRiskType[1]','int'),
			t.x.value('TagIncludedDescription[1]','varchar(max)'),
			t.x.value('ChangeTracker[1]','varchar(10)')
		FROM @DCIXml.nodes('DCI/RisksDescription') t(x)

		INSERT INTO  @HighRiskDrugsTmp
		SELECT 
			t.x.value('Id[1]','int') as Id,
			t.x.value('InventoryRiskLevelId[1]','int'),
			t.x.value('Observation[1]','varchar(500)'),		
			t.x.value('ChangeTracker[1]','varchar(10)')		
		FROM @DCIXml.nodes('/DCI/HighRiskDrugs') t(x)

		INSERT INTO  @IHPARAMDCI
		SELECT 
			t.x.value('ID[1]','int') as Id,
			t.x.value('CODDCIMED[1]','varchar(20)'),
			t.x.value('TIPO[1]','int'),		
			t.x.value('ChangeTracker[1]','varchar(10)')		
		FROM @DCIXml.nodes('/DCI/IHPARAMDCIs') t(x)
		
		--Se obtiene los detalles del xml(FixedAssetEntryItemDetailPartBook)
		INSERT INTO @ListDeleteMedicamentsTmp
		SELECT 
			t.x.value('Id[1]','int') as Id
		FROM @ListDeleteMedicaments.nodes('/ListDeleteMedicaments') t(x)

		--Se obtiene los detalles del xml(FixedAssetEntryItemDetailPartBook)
		INSERT INTO @ListDeleteDrugActiveTmp
		SELECT 
			t.x.value('Id[1]','int') as Id
		FROM @ListDeleteDrugActive.nodes('/ListDeleteDrugActive') t(x)
		
		--Eliminación DrugInteraction
		IF (select count(*) from @ListDeleteMedicamentsTmp ) > 0 BEGIN
			
			Declare @DrugInteractionId int
			Declare InfoItem1 Cursor For 			
			Select Id From @ListDeleteMedicamentsTmp

			Open InfoItem1
			Fetch Next From InfoItem1 Into @DrugInteractionId
			While @@fetch_status = 0
			Begin
				
				DECLARE @CodigoPrincipal varchar(20)
				DECLARE @CodigoSecundario VARCHAR(20)

				SELECT @CodigoPrincipal = DCI.Code
				FROM Inventory.DrugInteraction DI, Inventory.DCI DCI 
				WHERE DI.ID = @DrugInteractionId AND DCI.Id = DI.ParentDCIId

				SELECT @CodigoSecundario = ATCE.Code 
				FROM Inventory.DrugInteraction DI, Inventory.ATCEntity ATCE 
				WHERE DI.Id = @DrugInteractionId AND ATCE.Id = DI.ATCEntityId

				-- Tengo que eliminar el que tenga el Codigo Principal y Secundario
				DELETE FROM dbo.HCINTEMED WHERE CODPRODUA = @CodigoPrincipal AND CODPRODUB = @CodigoSecundario
				DELETE FROM dbo.HCINTEMED WHERE CODPRODUb = @CodigoPrincipal AND CODPRODUA = @CodigoSecundario

			Fetch Next From InfoItem1 Into @DrugInteractionId
				
			END
			Close InfoItem1
			Deallocate InfoItem1

		END
		
		DELETE Inventory.LethalDoseLimits WHERE Id in (SELECT Id from @ListDeleteLethalDoseLimitsTmp WHERE Id > 0)

		-- Ahora elimino de la Tabla de Vie
		DELETE Inventory.DrugInteraction WHERE Id in (SELECT Id FROM @ListDeleteMedicamentsTmp WHERE Id > 0)

		-- Elimino el DCI ATC Entity
		DELETE Inventory.DrugActive WHERE Id in (SELECT Id from @ListDeleteDrugActiveTmp WHERE Id > 0)
		
		IF @Id = 0 BEGIN --Si se esta insertando por primera vez se consulta la secuencia numerica y no se esta eliminando
			IF @Code = '' BEGIN
				--Consultamos si la secuencia es con O o OU
				DECLARE @scope varchar(5) = ''
				DECLARE @idSequenceDetail int
				DECLARE @pattern varchar(300)
				DECLARE @NextS int
				SELECT @scope = Scope FROM Inventory.InventorySequence WHERE IdForm = '340'

				--Se valida el scope
				IF @scope = 'O' BEGIN
					-- Consultamos la secuencia numerica del form de ingreso de activos
					SELECT @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
					FROM Inventory.InventorySequenceDetail bsd inner join Inventory.InventorySequence bs on bs.Id = bsd.InventorySequenceId inner join Common.Sequense cs on cs.Id = bsd.IdSequense
					WHERE bs.IdForm = '340'
				END ELSE BEGIN
					-- Consultamos la secuencia numerica del form de ingreso de activos
					SELECT @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
					FROM Inventory.InventorySequenceDetail bsd inner join Inventory.InventorySequence bs on bs.Id = bsd.InventorySequenceId inner join Common.Sequense cs on cs.Id = bsd.IdSequense
					WHERE bs.IdForm = '340' and bsd.IdOperatingUnit = @OperatingUnitId
				END

				IF (@idSequenceDetail is null) BEGIN
					SELECT 999 as CodeMessage, 'Secuencia no encontrada para el Formulario DCI' as Message, '' as Code, 0 as Id
					RETURN
				END

				SELECT @Code = dbo.GetSequence('',@pattern,@NextS)
				UPDATE Inventory.InventorySequenceDetail SET [Next] += 1 WHERE Id = @idSequenceDetail

			END
			
			--Se inserta la cabecera
			INSERT INTO Inventory.DCI
				(
					[Code], Name, DCICrystal, Status, Combined, TypeWarning, CreationUser, CreationDate
				)
			VALUES
				(
					@Code, @Name, @Code, @Status, @Combined, @TypeWarning, @CodeUser, [Common].[GETDATE]()
				)
		   
			--Obtengo el id de la cabcera
			SET @Id = SCOPE_IDENTITY()
		END ELSE BEGIN --Si se esta actualizando
			--Se actualiza la cabecera
			UPDATE Inventory.DCI
				SET [Code] = @Code, Name = @Name, DCICrystal = @Code, Status = @Status, Combined = @Combined, TypeWarning = @TypeWarning, ModificationUser = @CodeUser, ModificationDate = [Common].[GETDATE]()
			WHERE Id = @Id	
		END
		
		if exists(select 1 from dbo.IHDCIMEDI where CODDCIMED = @Code)
		begin
			-- Ahora actualizo en la Tabla de Crystal
			UPDATE	dbo.IHDCIMEDI
					SET DESDCIMED = @Name
			WHERE CODDCIMED = @Code
		end
		else begin
			-- Inserto ahora en las Tablas de Crystal
			INSERT INTO dbo.IHDCIMEDI(CODDCIMED, DESDCIMED)
			VALUES(@Code, @Name)
		end

		Declare @EntityId int
		Declare @TempId int
		Declare @DCIId int
		Declare @ATCEntityId int
		Declare @RiskLevel int
		Declare @Description varchar(max)

		Declare InfoItem Cursor For Select Id, TempId, ATCEntityId, RiskLevel, [Description] From @DrugInteractionTmp

		Open InfoItem
		Fetch Next From InfoItem Into @EntityId, @TempId, @ATCEntityId, @RiskLevel, @Description
		While @@fetch_status = 0
		Begin
			print '@Id --> ' + convert(varchar(5),@Id)
			if @EntityId = 0 Begin --Si se esta insertando el registro
				--Se inserta el registro

				insert into Inventory.DrugInteraction (ParentDCIId, ATCEntityId, RiskLevel, [Description])
				select @Id, @ATCEntityId, RiskLevel, [Description] FROM @DrugInteractionTmp  where Id = 0 and TempId = @TempId

				--DECLARE @IdDCI int = 0
				--select @IdDCI = DCIId FROM @DrugInteractionTmp  where Id = 0 and TempId = @TempId

				-- INSERTO EN CRYSTAL A DOBLE REGISTRO:
				DECLARE @count INT = 0
				WHILE @count < 2 BEGIN

					DECLARE @CodeATC VARCHAR(20)

					SELECT @CodeATC = Code FROM Inventory.ATCEntity WHERE Id = @ATCEntityId
					
					IF @count = 0 BEGIN
						INSERT INTO dbo.HCINTEMED(CODPRODUA, CODPRODUB, NIVRIESGO, OBSERVACI)
						VALUES(@Code, @CodeATC , @RiskLevel, @Description)
					END ELSE BEGIN
						INSERT INTO dbo.HCINTEMED(CODPRODUA, CODPRODUB, NIVRIESGO, OBSERVACI)
						VALUES(@CodeATC, @Code, @RiskLevel, @Description)
					END

					SET @count = @count + 1
				END
				
			END ELSE BEGIN
			 --Si se esta actualizando
				
				DECLARE @CodigoPrincipalActualiza varchar(20)
				DECLARE @CodigoSecundarioActualiza VARCHAR(20)

				SELECT @CodigoPrincipalActualiza = DCI.Code
				FROM Inventory.DrugInteraction DI, Inventory.DCI DCI 
				WHERE DI.ID = @EntityId AND DCI.Id = DI.ParentDCIId

				SELECT @CodigoSecundarioActualiza = ATCE.Code 
				FROM Inventory.DrugInteraction DI, Inventory.ATCEntity ATCE 
				WHERE DI.Id = @EntityId AND ATCE.Id = DI.ATCEntityId
				
				-- PRIMERO ELIMINO EN CRYSTAL LOS QUE VOY A ACTUALIZAR 
				DELETE FROM dbo.HCINTEMED WHERE CODPRODUA = @CodigoPrincipalActualiza AND CODPRODUB = @CodigoSecundarioActualiza
				DELETE FROM dbo.HCINTEMED WHERE CODPRODUb = @CodigoPrincipalActualiza AND CODPRODUA = @CodigoSecundarioActualiza

				-- INSERTO EN CRYSTAL A DOBLE REGISTRO:
				DECLARE @countUpdate INT = 0
				WHILE @countUpdate < 2 BEGIN

					DECLARE @CodeATCUpdate VARCHAR(20)

					SELECT @CodeATCUpdate = Code FROM Inventory.ATCEntity WHERE Id = @ATCEntityId
					
					IF @countUpdate = 0 BEGIN
						INSERT INTO dbo.HCINTEMED(CODPRODUA, CODPRODUB, NIVRIESGO, OBSERVACI)
						VALUES(@Code, @CodeATCUpdate , @RiskLevel, @Description)
					END ELSE BEGIN
						INSERT INTO dbo.HCINTEMED(CODPRODUA, CODPRODUB, NIVRIESGO, OBSERVACI)
						VALUES(@CodeATCUpdate, @Code, @RiskLevel, @Description)
					END

					SET @countUpdate = @countUpdate + 1
				END

				 --AHORA ACTUALIZO LA TABLA DE INDIGO VIE
				UPDATE faei
				SET faei.ParentDCIId = @Id,
						faei.ATCEntityId = faeiTemp.ATCEntityId,
						faei.RiskLevel = faeiTemp.RiskLevel,
						faei.[Description] = faeiTemp.[Description]
				FROM Inventory.DrugInteraction AS faei
				JOIN @DrugInteractionTmp AS faeiTemp
					ON faeiTemp.Id = faei.Id
				WHERE faei.Id = @EntityId
				  AND EXISTS (
						SELECT faei.ParentDCIId,  NULLIF(faei.ATCEntityId, 0), faei.RiskLevel, faei.[Description]
						EXCEPT
						SELECT @Id, NULLIF(faeiTemp.ATCEntityId, 0), faeiTemp.RiskLevel, faeiTemp.[Description]
				  );	
			END

			Fetch Next From InfoItem Into @EntityId, @TempId, @ATCEntityId, @RiskLevel, @Description
					
		End
				
		Close InfoItem
		Deallocate InfoItem	

		--DCIATC
		insert into DCIATCEntity(IdDCI, IdATCEntity)
		select @Id, ATCEntityId
		from @DCIATCEntityTmp
		where Id = 0

		update hd set IdATCEntity = thd.ATCEntityId
		from DCIATCEntity hd
		join @DCIATCEntityTmp thd on hd.ID = thd.ID 
		where thd.Id > 0 And thd.ChangeTracker = 'Modified'

		delete hd
		from DCIATCEntity hd
		join @DCIATCEntityTmp thd on hd.ID = thd.ID 
		where thd.ChangeTracker = 'Deleted'
				
		--Sustancia activa
		insert into Inventory.DrugActive (ParentDCIId, ATCEntityId)
		select @Id, ATCEntityId
		from @DrugActiveTmp
		where Id = 0

		update da set DCIId = tda.ATCEntityId
		from Inventory.DrugActive da
		join @DrugActiveTmp tda on tda.Id = da.Id 
		where tda.Id > 0 And tda.ChangeTracker = 'Modified'

		delete da
		from Inventory.DrugActive da
		join @DrugActiveTmp tda on tda.Id = da.Id  
		where tda.ChangeTracker = 'Deleted'

		--Niveles de riesgo
		insert into Inventory.HighRiskDrugs (DCIId, InventoryRiskLevelId, Observation)
		select @Id, InventoryRiskLevelId, Observation 
		from @HighRiskDrugsTmp
		where Id = 0

		update hd set  InventoryRiskLevelId = thd.InventoryRiskLevelId, Observation = thd.Observation
		from Inventory.HighRiskDrugs hd
		join @HighRiskDrugsTmp thd on hd.Id = thd.Id 
		where thd.Id > 0 And thd.ChangeTracker = 'Modified'

		delete hd
		from Inventory.HighRiskDrugs hd
		join @HighRiskDrugsTmp thd on hd.Id = thd.Id 
		where thd.ChangeTracker = 'Deleted'

		-- Parametros dosis letales
		INSERT INTO Inventory.LethalDoseLimits (StartAge, StartAgeUnit, EndAge, EndAgeUnit, StartWeight, StartWeightUnit, EndWeight, EndWeightUnit, MaxDoseConcentration, MaxDoseConcentrationUnitId, Max24HourConcentration, Max24HourConcentrationUnitId, LethalDoseConcentration, LethalDoseConcentrationUnitId, Lethal24HourConcentration, Lethal24HourConcentrationUnitId, DCIId)
		select StartAge, StartAgeUnit, EndAge, EndAgeUnit, StartWeight, StartWeightUnit, EndWeight, EndWeightUnit, MaxDoseConcentration, MaxDoseConcentrationUnitId, Max24HourConcentration, Max24HourConcentrationUnitId, LethalDoseConcentration, LethalDoseConcentrationUnitId, Lethal24HourConcentration, Lethal24HourConcentrationUnitId, @Id 
		from @LethalDoseLimitsTmp
		where id = 0

		UPDATE Inventory.LethalDoseLimits 
		SET 
			StartAge = tldl.StartAge, 
			StartAgeUnit = tldl.StartAgeUnit,
			EndAge = tldl.EndAge,
			EndAgeUnit = tldl.EndAgeUnit,
			StartWeight = tldl.StartWeight,
			StartWeightUnit = tldl.StartWeightUnit,
			EndWeight = tldl.EndWeight,
			EndWeightUnit = tldl.EndWeightUnit,
			MaxDoseConcentration = tldl.MaxDoseConcentration,
			MaxDoseConcentrationUnitId = tldl.MaxDoseConcentrationUnitId,
			Max24HourConcentration = tldl.Max24HourConcentration,
			Max24HourConcentrationUnitId = tldl.Max24HourConcentrationUnitId,
			LethalDoseConcentration = tldl.LethalDoseConcentration,
			LethalDoseConcentrationUnitId = tldl.LethalDoseConcentrationUnitId,
			Lethal24HourConcentration = tldl.Lethal24HourConcentration,
			Lethal24HourConcentrationUnitId = tldl.Lethal24HourConcentrationUnitId,
			DCIId = @Id
		from Inventory.LethalDoseLimits ldl
		join @LethalDoseLimitsTmp tldl on ldl.id = tldl.Id
		where tldl.Id > 0 and tldl.ChangeTracker = 'Modified'

		delete ldl
		from Inventory.LethalDoseLimits ldl
		join @LethalDoseLimitsTmp tldl on ldl.id = tldl.Id
		where tldl.ChangeTracker = 'Deleted'

		--Factores de riesgo
		INSERT INTO Inventory.DCIRiskFactors (DCIId, RiskFactorId)
		select @Id, RiskFactorId
		from @DCIRiskFactorTmp
		where id = 0
		
		UPDATE Inventory.DCIRiskFactors
		SET DciId = @Id, RiskFactorId = tdrf.RiskFactorId
		from Inventory.DCIRiskFactors drf
		join @DCIRiskFactorTmp tdrf on drf.id = tdrf.id
		where tdrf.id > 0 and tdrf.ChangeTracker = 'Modified'

		delete drf
		from Inventory.DCIRiskFactors drf
		join @DCIRiskFactorTmp tdrf on drf.Id = tdrf.Id
		where tdrf.ChangeTracker = 'Deleted'

		--Descripción factores de riesgo
		INSERT INTO Inventory.RisksDescription (DciId, TagRiskType, TagIncludedDescription)
		SELECT @Id, TagRiskType, TagIncludedDescription
		from  @RisksDescriptionTmp
		where Id = 0 

		 UPDATE Inventory.RisksDescription
		 set DciId = @Id, TagRiskType = trd.TagRiskType, TagIncludedDescription = trd.TagIncludedDescription
		 from Inventory.RisksDescription rd
		 join @RisksDescriptionTmp trd on rd.id = trd.Id
		 where trd.id > 0 and trd.ChangeTracker = 'Modified'

		 delete rd
		 from Inventory.RisksDescription rd
		 join @RisksDescriptionTmp trd on rd.Id = trd.Id
		 where trd.ChangeTracker = 'Deleted'

		--MEDI
		insert into IHPARAMDCI (CODDCIMEDPADRE, CODDCIMED, TIPO, FECHAREG, USUARIOREG)
		select @Code, CODDCIMED, TIPO, Common.GETDATE(), @CodeUser
		from @IHPARAMDCI
		where ID = 0

		update hd set CODDCIMED = thd.CODDCIMED, TIPO = thd.TIPO
		from IHPARAMDCI hd
		join @IHPARAMDCI thd on hd.ID = thd.ID 
		where thd.ID > 0 And thd.ChangeTracker = 'Modified'

		delete hd
		from IHPARAMDCI hd
		join @IHPARAMDCI thd on hd.ID = thd.ID 
		where thd.ChangeTracker = 'Deleted'

		if @IsCreated = 0
			select 0 as CodeMessage, 'Se actualizó correctamente el DCI' as Message, @Code as Code, @Id as Id		
		else
			select 0 as CodeMessage, 'Se guardó correctamente el DCI' as Message, @Code as Code, @Id as Id		

	END TRY
	BEGIN CATCH
		SELECT 999 as CodeMessage, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(10)) as MESSAGE, '' as Code, 0 as Id
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea o actualiza la información completa de una Denominación Común Internacional (DCI) de medicamentos, incluyendo sus interacciones farmacológicas con otras DCI o grupos ATC, principios activos asociados, límites de dosis letales y máximas por rango de edad y peso, factores de riesgo, descripciones de riesgo clínico y medicamentos de alto riesgo. Recibe toda la información mediante parámetros XML que descompone en tablas temporales para luego insertar, actualizar o eliminar registros en las tablas Inventory.DCI, Inventory.DrugInteraction, Inventory.DrugActive, Inventory.LethalDoseLimits y dbo.HCINTEMED, sincronizando el catálogo farmacológico del inventario con la historia clínica. Es el punto central de mantenimiento del catálogo de principios activos genéricos y su seguridad clínica en el sistema de farmacia e inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDCI';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDCI';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste de forma transaccional la información maestra de un DCI (Denominación Común Internacional) y todas sus colecciones asociadas (interacciones, ATC, principios activos, alto riesgo, dosis letales, factores y descripciones de riesgo), sincronizando además los catálogos legacy de historia clínica (Crystal).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML @DCIXml debe contener el nodo /DCI con Id, Code, Name, Status, Combined y TypeWarning.; Para nuevos DCI sin código, debe existir una fila en Inventory.InventorySequence con IdForm=''340'' y, si el alcance es por unidad, una InventorySequenceDetail correspondiente al @OperatingUnitId.; Los Id incluidos en las listas de eliminación deben corresponder a registros existentes en sus tablas (DrugInteraction, DrugActive, LethalDoseLimits).; Los nodos DrugInteraction deben referenciar un ATCEntityId existente en Inventory.ATCEntity para poder obtener el Code usado al replicar en dbo.HCINTEMED.; Los valores numéricos de LethalDoseLimits llegan como texto con coma decimal y se convierten reemplazando '','' por ''.''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El código del DCI se sincroniza siempre entre Inventory.DCI.Code, Inventory.DCI.DCICrystal y dbo.IHDCIMEDI.CODDCIMED.; Las interacciones farmacológicas se registran simétricamente en dbo.HCINTEMED: por cada par (A,B) se inserta también (B,A).; Antes de reinsertar una interacción modificada, se eliminan en HCINTEMED las filas previas con los códigos principal/secundario en ambos sentidos para evitar duplicados.; La secuencia automática del formulario DCI corresponde a IdForm = ''340'' en Inventory.InventorySequence.; Sólo se aplican operaciones de eliminación/actualización a registros con Id > 0; los Id = 0 se tratan como inserciones nuevas.; Toda la operación se ejecuta dentro de un TRY/CATCH que devuelve CodeMessage=999 con el mensaje y la línea del error sin propagar la excepción.; La fecha de creación/modificación se obtiene siempre vía Common.GETDATE() (no GETDATE() del servidor).; Las eliminaciones del lote (ListDelete*) se procesan ANTES de insertar/actualizar la cabecera y sus detalles.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'DCI (Denominación Común Internacional); Interacción medicamentosa; Clasificación ATC; Principio activo; Medicamentos de alto riesgo; Dosis letal / dosis máxima; Factores de riesgo del medicamento; Descripción de riesgos farmacológicos; Secuencia de numeración por formulario y unidad operativa; Catálogo de medicamentos en historia clínica (Crystal)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Id = 0 (DCI nuevo) → Si @Code está vacío se obtiene un código nuevo desde la secuencia del formulario 340 y luego se inserta en Inventory.DCI else Se actualiza Inventory.DCI con los datos recibidos y se registra ModificationUser/ModificationDate; si @scope = ''O'' en Inventory.InventorySequence para IdForm=''340'' → Se obtiene la secuencia sin filtrar por unidad operativa else Se obtiene la secuencia filtrando además por bsd.IdOperatingUnit = @OperatingUnitId; si @idSequenceDetail IS NULL tras buscar la secuencia → Retorna result set con CodeMessage=999 y mensaje ''Secuencia no encontrada para el Formulario DCI'' y termina sin insertar else Continúa generando el código con dbo.GetSequence e incrementando [Next] en InventorySequenceDetail; si EXISTS registro en dbo.IHDCIMEDI con CODDCIMED=@Code → UPDATE de la descripción DESDCIMED en IHDCIMEDI else INSERT del par (CODDCIMED, DESDCIMED) en IHDCIMEDI; si Para cada DrugInteraction: @EntityId = 0 → Se inserta en Inventory.DrugInteraction y se replica en dbo.HCINTEMED en doble sentido (A→B y B→A) else Se borran las filas existentes en HCINTEMED para ambos sentidos, se reinsertan con los nuevos valores y se actualiza Inventory.DrugInteraction sólo si EXCEPT detecta diferencias; si ChangeTracker = ''Modified'' en colecciones (DCIATCEntity, DrugActive, HighRiskDrugs, LethalDoseLimits, DCIRiskFactors, RisksDescription, IHPARAMDCI) e Id>0 → Se actualiza el registro correspondiente else Si ChangeTracker = ''Deleted'' se elimina; si Id = 0 se inserta nuevo; si @IsCreated = 0 al final del flujo → Devuelve mensaje ''Se actualizó correctamente el DCI'' else Devuelve mensaje ''Se guardó correctamente el DCI''', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetSequence; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.DCI; Inventory.DrugInteraction; Inventory.ATCEntity; Inventory.InventorySequence; Inventory.InventorySequenceDetail; Common.Sequense; dbo.IHDCIMEDI; Inventory.DrugActive; Inventory.HighRiskDrugs; Inventory.LethalDoseLimits; Inventory.DCIRiskFactors; Inventory.RisksDescription; dbo.IHPARAMDCI; dbo.HCINTEMED; Inventory.DCIATCEntity', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDCI';
-- GO
