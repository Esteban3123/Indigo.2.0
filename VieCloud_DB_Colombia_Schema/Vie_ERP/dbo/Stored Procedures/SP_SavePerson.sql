-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-06-19
-- Description:	Procedimiento que se encarga de guardar, actualizar una persona
-- =============================================
CREATE PROCEDURE [dbo].[SP_SavePerson]
    @EntityXml AS XML,
	@EntityXmlEconomicActivities AS XML

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
			@Email VARCHAR(MAX)
	
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
					@Email = NULL
			FROM @EntityXml.nodes('/NewDataSet/Profesionales_Salud') t(x)

			SELECT @Email = s.USUEMAILE
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

		SELECT @IdentificationType = dbo.GetHomologationIdentificationType(@IdentificationType)
		SET @IdentificationTypeId = (SELECT top 1 ad.ID 
									from INPACIENT ipc WITH(NOLOCK)
									JOIN ADTIPOIDENTIFICA ad WITH(NOLOCK) ON ipc.IPTIPODOC = ad.CODIGO
									where ipc.IPCODPACI =@IdentificationNumber)

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
		DECLARE @CodigoPaisHomologa VARCHAR(25),
			@CodigoEstadoHomologa VARCHAR(25),
			@CodigoCiudadHomologa VARCHAR(25),
			@IdCityERP INT

		select  @CodigoPaisHomologa = Pais.UBICODIGO , @CodigoEstadoHomologa = Estado.UBICODIGO , @CodigoCiudadHomologa = Ciudad.UBICODIGO
		from INUBICACI Distrito inner join
			INUBICACI Ciudad on Ciudad.ID = Distrito.UbicationId inner join
			INUBICACI Estado on Estado.ID = Ciudad.UbicationId inner join
			INUBICACI Pais on Pais.ID = ESTADO.UbicationId  
		where Distrito.AUUBICACI =@Ubication
		
		select @IdCityERP = C.Id from Common.Country P inner join
		Common.Department D on P.Id = D.CountryId  inner join 
		Common.City C on C.DepartamentId = D.Id 
		where P.Code = @CodigoPaisHomologa and D.Code = @CodigoEstadoHomologa AND C.Code = @CodigoCiudadHomologa

		IF EXISTS (SELECT 1 FROM Common.Address WHERE IdPerson = @PersonId)
		BEGIN
			UPDATE TOP (1) a
				SET Addresss = @Address,
					DepartmentId = c.DepartamentId,
					CityId = c.Id,
					State = 1
			FROM Common.Address a
			JOIN Common.City c ON C.Id = @IdCityERP
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
			WHERE c.Id = @IdCityERP
		END

		/********************************************  CORREO ELECTRONICO ********************************************/
		
	
		-- Solo continua  si @Email no es NULL y tiene un formato adecuado
 
			IF @Email IS NOT NULL AND LTRIM(RTRIM(@Email)) <> '' AND LTRIM(RTRIM(@Email)) LIKE '%_@__%.__%'
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
						IdPerson, Email, Type, State, Synchronized
					)
					SELECT @PersonId, RTRIM(LTRIM(@Email)), 2, 1, 1
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

		print 'entro a tercero'

		/************************************************** TERCERO **************************************************/

		SET @FullName = CONCAT(@FirstName, IIF(ISNULL(@SecondName, '') = '', '', ' ' + @SecondName), ' ', @FirstLastName, IIF(ISNULL(@SecondLastName, '') = '', '', ' ' + @SecondLastName))

		IF EXISTS (SELECT 1 FROM Common.ThirdParty WHERE PersonId = @PersonId)
		BEGIN
			UPDATE tp
				SET Name = @FullName
			FROM Common.ThirdParty tp
			WHERE tp.PersonId = @PersonId

			SELECT @ThirdPartyId = tp.Id
			FROM Common.ThirdParty tp
			WHERE tp.PersonId = @PersonId;

		END
		ELSE
		BEGIN
			INSERT INTO Common.ThirdParty
			(
				PersonId,Nit,Name,PersonType,RetentionType,ContributionType,Ica,IcaPercentage,IcaTop,IcaTopValue,State,CreationDate,UserId
			)
			SELECT	@PersonId,@IdentificationNumber,@FullName,1,1,0,0,0,0,0,1,[Common].[GETDATE](),0
			SELECT @ThirdPartyId = Id FROM Common.ThirdParty WHERE PersonId = @PersonId
		END

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

					 
			--Actualización independiente de ElectronicBiller en Common.ThirdParty
			UPDATE t
			SET t.ElectronicBiller =
				(
					SELECT TOP 1
						CASE
							WHEN LOWER(T.X.value('(Principal)[1]', 'nvarchar(5)')) IN (N'true', N'1') THEN 1
							ELSE 0
						END
					FROM @EntityXmlEconomicActivities.nodes('/NewDataSet/ActividadesEconomicasPaciente') AS T(X)
				)
			FROM Common.ThirdParty t
			WHERE t.Id = @ThirdPartyId; 
			
		END

		/************************************************* RESULTADO *************************************************/

		SELECT 0 AS CodeResult, 'Guardo Correctamente' AS MessageResult
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeResult, 'SP_SavePerson: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea o actualiza el registro maestro de una persona natural en el sistema, ya sea un paciente o un profesional de la salud. Recibe los datos demográficos (número de documento o cédula, tipo de identificación, nombres, apellidos, fecha de nacimiento, sexo) junto con información de contacto (dirección, teléfono fijo, celular, correo electrónico) empaquetados en un XML. Consulta la tabla de pacientes (INPACIENT) y la de profesionales (INPROFSAL) para extraer los datos según el origen, homologa el tipo de documento usando ADTIPOIDENTIFICA y GetHomologationIdentificationType, y luego inserta o actualiza la entidad en Common.Person; también sincroniza la dirección, los teléfonos y el correo del profesional obtenido desde SEGusuaru. Sirve como punto central de sincronización de identidad entre los módulos clínicos, administrativos y de seguridad del ERP/EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SavePerson';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SavePerson';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Crea o actualiza la persona, su dirección, correo, teléfonos, tercero y actividades económicas a partir de un XML de paciente o profesional, homologando documento y ubicación entre módulos legacy y el esquema Common.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SavePerson';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entidad debe corresponder a un nodo /NewDataSet/Pacientes o /NewDataSet/Profesionales_Salud.; Para profesionales, debe existir el registro en INPROFSAL enlazado a SEGusuaru para obtener el correo.; El número de identificación no puede ser nulo ni vacío.; Deben existir los catálogos Common.PhoneType con nombres ''FIJO'' y ''MOVIL''.; La ubicación enviada debe poder homologarse vía INUBICACI hacia Common.Country/Department/City para registrar dirección.; Debe existir la función dbo.GetHomologationIdentificationType para mapear tipos de documento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SavePerson';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El nombre completo del tercero se construye concatenando nombres y apellidos, omitiendo los componentes vacíos.; Las nuevas direcciones, correos y teléfonos se crean con State=1 y Synchronized=1.; El correo solo se persiste si cumple un patrón básico de email.; Los teléfonos solo se persisten cuando el valor es numérico.; Los profesionales siempre se registran con IdentificationType=1 y sin fecha de nacimiento ni género.; Para profesionales el correo proviene del usuario asociado en SEGusuaru, no del XML.; Los terceros nuevos se crean como PersonType=1, RetentionType=1, sin ICA y activos.; Cualquier excepción es capturada y devuelta como CodeResult=999 con el mensaje y línea del error.; El resultado exitoso retorna CodeResult=0 y mensaje ''Guardo Correctamente''.; En el MERGE de actividades económicas se deduplica por (ThirdPartyId, EconomicActivityId) tomando el máximo del flag Defect.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SavePerson';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Profesional de la salud; Identificación / tipo de documento; Tercero; Actividad económica; Facturador electrónico; Dirección, teléfono fijo, móvil y correo electrónico; Homologación de ubicación geográfica (país/departamento/ciudad)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SavePerson';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El XML contiene nodo /NewDataSet/Pacientes → Toma datos demográficos, dirección y contactos desde la estructura del paciente (IPCODPACI, IPTIPODOC, etc.); si El XML contiene nodo /NewDataSet/Profesionales_Salud → Toma datos del profesional, fija IdentificationType=1, BirthDate y Gender en NULL, y obtiene el correo desde SEGusuaru vía INPROFSAL; si Número de identificación vacío o NULL tras parsear el XML → Retorna CodeResult=999 con mensaje ''No se encontró información a guardar o actualizar'' y termina else Continúa con homologación y persistencia; si No existe Person con ese IdentificationNumber → INSERT en Common.Person con State=1 else UPDATE de Common.Person preservando IdentificacionCityId si entra NULL/0 y conservando BirthDate/Gender/IdentificationTypeId si llegan NULL; si Existe Address para la persona → UPDATE de la dirección con City/Department homologados else INSERT de nueva dirección con State=1 y Synchronized=1; si @Email no es NULL/vacío y cumple patrón ''%_@__%.__%'' → Si existe email para la persona lo actualiza; en caso contrario inserta con Type=2, State=1, Synchronized=1 else No registra correo; si ISNUMERIC(@Phone)=1 → Upsert de teléfono fijo (IdPhoneType correspondiente a ''FIJO'') else Omite teléfono fijo; si ISNUMERIC(@CellPhone)=1 → Upsert de teléfono móvil (IdPhoneType correspondiente a ''MOVIL'') else Omite celular; si Existe ThirdParty para la persona → UPDATE Name con el nombre completo concatenado else INSERT en ThirdParty con PersonType=1, RetentionType=1, ContributionType=0, Ica=0, State=1 y UserId=0; si El XML de actividades económicas contiene nodos /NewDataSet/ActividadesEconomicasPaciente → MERGE sobre ThirdPartyEconomicActivities (insert/update Defect) y actualiza ElectronicBiller del ThirdParty según el flag Principal del primer registro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SavePerson';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetHomologationIdentificationType; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SavePerson';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPROFSAL; dbo.SEGusuaru; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; dbo.INUBICACI; Common.Person; Common.PhoneType; Common.Country; Common.Department; Common.City; Common.Address; Common.Email; Common.Phone; Common.ThirdParty; Common.ThirdPartyEconomicActivities', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SavePerson';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SavePerson';
-- GO
