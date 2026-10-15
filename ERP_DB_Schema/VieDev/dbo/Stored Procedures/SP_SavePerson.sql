CREATE PROCEDURE [dbo].[SP_SavePerson]
    @EntityXml AS XML
	,@EntityXmlEconomicActivities AS XML
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables
	DECLARE @PersonId INT,
			@EconomicActivityId INT,
			@ThirdPartyId INT,
			@Defect BIT,
			@IdentificationNumber VARCHAR(25),
			@IdentificationType INT,
			@IdentificationTypeId INT,
			@IdentificacionCityId INT,
			@FirstName VARCHAR(MAX),
			@SecondName VARCHAR(MAX),
			@FirstLastName VARCHAR(MAX),
			@SecondLastName VARCHAR(MAX),
			@FullName VARCHAR(MAX),
			@BirthDate DATE,
			@Gender TINYINT,
			--------------------------------
			@Address VARCHAR(MAX),
			@Ubication VARCHAR(MAX),
			@Phone VARCHAR(MAX),
			@PhoneTypeId INT,
			@CellPhone VARCHAR(MAX),
			@CellPhoneTypeId INT,
			@Email VARCHAR(MAX),
			@IDADTIPOIDENTIFICA INT
	
	BEGIN TRY
		--Se obtienen los datos de la entidad si proviene de pacientes
		IF EXISTS (SELECT 1 FROM @EntityXml.nodes('/NewDataSet/Pacientes') t(x))
		BEGIN			
			SELECT	@IdentificationNumber = t.x.value('IPCODPACI[1]','varchar(25)'),
					@IdentificationType = t.x.value('IPTIPODOC[1]','int'),
					@IdentificacionCityId = t.x.value('GENEXPEDITIONCITY[1]','int'),
					@FirstName = t.x.value('IPPRINOMB[1]','varchar(MAX)'),
					@SecondName = t.x.value('IPSEGNOMB[1]','varchar(MAX)'),
					@FirstLastName = t.x.value('IPPRIAPEL[1]','varchar(MAX)'),
					@SecondLastName = t.x.value('IPSEGAPEL[1]','varchar(MAX)'),
					@BirthDate = t.x.value('IPFECNACI[1]','datetime'),
					@Gender = t.x.value('IPSEXOPAC[1]','tinyint'),
					-----------------------------------------------------------
					@Address = t.x.value('IPDIRECCI[1]','varchar(MAX)'),
					@Ubication = t.x.value('AUUBICACI[1]','varchar(MAX)'),
					@Phone = t.x.value('IPTELEFON[1]','varchar(MAX)'),
					@CellPhone = t.x.value('IPTELMOVI[1]','varchar(MAX)'),
					@Email = t.x.value('CORELEPAC[1]','varchar(MAX)')				
			FROM @EntityXml.nodes('/NewDataSet/Pacientes') t(x)
		END

		--Se obtienen los datos de la entidad si proviene de profesionales
		IF EXISTS (SELECT 1 FROM @EntityXml.nodes('/NewDataSet/Profesionales_Salud') t(x))
		BEGIN			
			SELECT	@IdentificationNumber = t.x.value('CODIGONIT[1]','varchar(25)'),
					@IdentificationType = 1,
					@IdentificacionCityId = NULL,
					@FirstName = t.x.value('MEDPRINOM[1]','varchar(MAX)'),
					@SecondName = t.x.value('MEDSEGNOM[1]','varchar(MAX)'),
					@FirstLastName = t.x.value('MEDPRIAPEL[1]','varchar(MAX)'),
					@SecondLastName = t.x.value('MEDSEGAPEL[1]','varchar(MAX)'),
					@BirthDate = NULL,
					@Gender = NULL,
					-----------------------------------------------------------
					@Address = t.x.value('IMDIRECCI[1]','varchar(MAX)'),
					@Ubication = t.x.value('AUUBICACI[1]','varchar(MAX)'),
					@Phone = t.x.value('IMTELEFON[1]','varchar(MAX)'),
					@CellPhone = t.x.value('IMTELMOVI[1]','varchar(MAX)'),
					@Email = NULL,
					@IDADTIPOIDENTIFICA = t.x.value('IDADTIPOIDENTIFICA[1]','int')
			FROM @EntityXml.nodes('/NewDataSet/Profesionales_Salud') t(x)

			SELECT @Email = LTRIM(RTRIM(s.USUEMAILE))
			FROM dbo.INPROFSAL p 
			JOIN dbo.SEGusuaru s ON p.CODUSUARI = s.CODUSUARI 
			WHERE p.CODIGONIT = @IdentificationNumber
		END

		/***********************************************  VALIDACIONES ***********************************************/

		IF ISNULL(@IdentificationNumber, '') = ''
		BEGIN
			SELECT 999 AS CodeResult, 'No se encontró información a guardar o actualizar' AS MessageResult
			RETURN
		END

		/********************************************** CARGUE DE DATOS **********************************************/

		SELECT @PersonId = Id
		FROM Common.Person pe
		WHERE pe.IdentificationNumber = @IdentificationNumber

		IF EXISTS (SELECT 1 FROM @EntityXml.nodes('/NewDataSet/Pacientes') t(x))
		BEGIN			

				SELECT @IdentificationType = dbo.GetHomologationIdentificationType(@IdentificationType)
				SET @IdentificationTypeId = (SELECT top 1 ad.ID 
											from INPACIENT ipc WITH(NOLOCK)
											JOIN ADTIPOIDENTIFICA ad WITH(NOLOCK) ON ipc.IPTIPODOC = ad.CODIGO
											where ipc.IPCODPACI =@IdentificationNumber)

       END
		 ELSE
		BEGIN

			SET @IdentificationTypeId = @IDADTIPOIDENTIFICA

		END


		SELECT TOP 1 @PhoneTypeId = Id
		FROM Common.PhoneType where upper([Name]) = 'FIJO'

		SELECT TOP 1 @CellPhoneTypeId = Id
		FROM Common.PhoneType where upper([Name]) = 'MOVIL'
		
		/************************************************** PERSONA **************************************************/

		IF @PersonId IS NULL
		BEGIN
			INSERT Common.Person 
			(
				IdentificationNumber,IdentificationType,IdentificacionCityId,FirstName,SecondName,FirstLastName,SecondLastName,BirthDate,Gender,State,IdentificationTypeId
			)
			SELECT @IdentificationNumber,@IdentificationType,@IdentificacionCityId,@FirstName,@SecondName,@FirstLastName,@SecondLastName,@BirthDate,@Gender,1,@IdentificationTypeId

			SET @PersonId = SCOPE_IDENTITY()
		END
		ELSE
		BEGIN
			UPDATE pe
				SET 
					IdentificacionCityId = case when @IdentificacionCityId IS NULL or @IdentificacionCityId = 0 THEN IdentificacionCityId else @IdentificacionCityId END,
					IdentificationType = @IdentificationType,
					FirstName = @FirstName,
					SecondName = @SecondName,
					FirstLastName = @FirstLastName,
					SecondLastName = @SecondLastName,
					BirthDate = ISNULL(@BirthDate, pe.BirthDate),
					Gender = ISNULL(@Gender, pe.Gender),
					IdentificationTypeId = ISNULL(@IdentificationTypeId,pe.IdentificationTypeId)
			FROM Common.Person pe
			WHERE pe.Id = @PersonId
		END

		/************************************************* DIRECCION *************************************************/
		
		IF EXISTS (SELECT 1 FROM Common.Address WHERE IdPerson = @PersonId)
		BEGIN
			UPDATE TOP (1) a
				SET Addresss = @Address,
					DepartmentId = c.DepartamentId,
					CityId = c.Id,
					State = 1
			FROM Common.Address a
			JOIN Common.City c ON SUBSTRING(@Ubication, 1, 5) = c.Code
			WHERE a.IdPerson = @PersonId
		END
		ELSE
		BEGIN
			INSERT INTO Common.Address
			(
				IdPerson,Addresss,DepartmentId,CityId,State,Synchronized
			)
			SELECT @PersonId,@Address,c.DepartamentId,c.Id,1,1
			FROM Common.City c 
			WHERE SUBSTRING(@Ubication, 1, 5) = c.Code
		END

		/********************************************  CORREO ELECTRONICO ********************************************/
		
		IF ISNULL(@Email, '') LIKE '%_@__%.__%'
		BEGIN
			IF EXISTS (SELECT 1 FROM Common.Email WHERE IdPerson = @PersonId)
			BEGIN
				UPDATE TOP (1) e
					SET Email = RTRIM(LTRIM(@Email)),
						State = 1
				FROM Common.Email e
				WHERE e.IdPerson = @PersonId
			END
			ELSE
			BEGIN
				INSERT INTO Common.Email
				(
					IdPerson,Email,Type,State,Synchronized
				)
				SELECT @PersonId,@Email,1,1,1
			END
		END

		/*************************************************  TELEFONO *************************************************/

		IF ISNUMERIC(@Phone) = 1 --AND LEN(@Phone) = 7
		BEGIN
			IF EXISTS (SELECT 1 FROM Common.Phone WHERE IdPerson = @PersonId AND IdPhoneType = @PhoneTypeId)
			BEGIN
				UPDATE TOP (1) e
					SET Phone = @Phone,
						State = 1
				FROM Common.Phone e
				WHERE e.IdPerson = @PersonId AND IdPhoneType = @PhoneTypeId
			END
			ELSE
			BEGIN
				INSERT INTO Common.Phone
				(
					IdPerson,Phone,IdPhoneType,State,Synchronized
				)
				SELECT @PersonId,@Phone,@PhoneTypeId,1,1
			END
		END

		/************************************************** CELULAR **************************************************/

		IF ISNUMERIC(@CellPhone) = 1 --AND LEN(@CellPhone) = 10
		BEGIN
			IF EXISTS (SELECT 1 FROM Common.Phone WHERE IdPerson = @PersonId AND IdPhoneType = @CellPhoneTypeId)
			BEGIN
				UPDATE TOP (1) e
					SET Phone = @CellPhone,
						State = 1
				FROM Common.Phone e
				WHERE e.IdPerson = @PersonId AND IdPhoneType = @CellPhoneTypeId
			END
			ELSE
			BEGIN
				INSERT INTO Common.Phone
				(
					IdPerson,Phone,IdPhoneType,State,Synchronized
				)
				SELECT @PersonId,@CellPhone,@CellPhoneTypeId,1,1
			END
		END

		/************************************************** TERCERO **************************************************/

		SET @FullName = CONCAT(@FirstName, IIF(ISNULL(@SecondName, '') = '', '', ' ' + @SecondName), ' ', @FirstLastName, IIF(ISNULL(@SecondLastName, '') = '', '', ' ' + @SecondLastName))

		IF EXISTS (SELECT 1 FROM Common.ThirdParty WHERE PersonId = @PersonId)
		BEGIN
			UPDATE tp
				SET Name = @FullName
			FROM Common.ThirdParty tp
			WHERE tp.PersonId = @PersonId
		END
		ELSE
		BEGIN
			INSERT INTO Common.ThirdParty
			(
				PersonId,Nit,DigitVerification,Name,PersonType,RetentionType,ContributionType,Ica,IcaPercentage,IcaTop,IcaTopValue,State,CreationDate,UserId,ElectronicBiller
			)
			SELECT	@PersonId,@IdentificationNumber,Common.fnCalculateVerificationCode(@IdentificationNumber),@FullName,1,1,0,0,0,0,0,1,[Common].[GETDATE](),0,0
		
		END

		SELECT @ThirdPartyId = Id FROM Common.ThirdParty WHERE PersonId = @PersonId

		/*********************************************ACTIVIDADES ECONOMICAS*********************************************/
		
		IF EXISTS (
			SELECT 1
			FROM @EntityXmlEconomicActivities.nodes('/NewDataSet/ActividadesEconomicasPaciente') AS T(X)
		)
		BEGIN
			;WITH src0 AS (
				SELECT
					@ThirdPartyId AS ThirdPartyId,
					T.X.value('(IdActividad)[1]', 'int') AS EconomicActivityId,
					CASE
						WHEN LOWER(T.X.value('(Principal)[1]', 'nvarchar(5)')) = N'true'  THEN CONVERT(bit, 1)
						WHEN LOWER(T.X.value('(Principal)[1]', 'nvarchar(5)')) = N'1'     THEN CONVERT(bit, 1)
						ELSE CONVERT(bit, 0)
					END AS Defect
				FROM @EntityXmlEconomicActivities.nodes('/NewDataSet/ActividadesEconomicasPaciente') AS T(X)
			),
			src AS (
				SELECT
					ThirdPartyId,
					EconomicActivityId,
					CONVERT(bit, MAX(CASE WHEN Defect = 1 THEN 1 ELSE 0 END)) AS Defect
				FROM src0
				WHERE EconomicActivityId IS NOT NULL
				GROUP BY ThirdPartyId, EconomicActivityId
			)
			
			MERGE Common.ThirdPartyEconomicActivities AS tgt
		USING src AS s
			  ON  tgt.ThirdPartyId       = s.ThirdPartyId
			  AND tgt.EconomicActivityId = s.EconomicActivityId
			WHEN MATCHED AND ISNULL(tgt.Defect, 0) <> ISNULL(s.Defect, 0)
				THEN UPDATE SET tgt.Defect = s.Defect
			WHEN NOT MATCHED BY TARGET
				THEN INSERT (ThirdPartyId, EconomicActivityId, Defect)
					 VALUES (s.ThirdPartyId, s.EconomicActivityId, s.Defect);
			
		END

		/************************************************* RESULTADO *************************************************/

		SELECT 0 AS CodeResult, 'Guardo Correctamente' AS MessageResult
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeResult, 'SP_SavePerson: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS MessageResult
	END CATCH
END