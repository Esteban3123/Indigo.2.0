
CREATE PROCEDURE [WebAppointment].[Sp_RegisterUserAndPatient]
    @IPPRINOMB          CHAR(20),
    @IPSEGNOMB          CHAR(20)           = N'',
    @IPPRIAPEL          CHAR(20),
    @IPSEGAPEL          CHAR(20)           = N'',
    @IPNOMCOMP          CHAR(250),
    @IPSEXOPAC          INT,
    @IPFECNACI          DATETIME,
    @IPTELMOVI          VARCHAR(MAX),
    @CORELEPAC          CHAR(50),
    @IPTIPODOC          INT,
    @IPCODPACI          VARCHAR(25),
    @ESTADOPAC          BIT               = 1,
    @CODIGONIT          VARCHAR(25),
    @IPEXPEDIC          CHAR(40)          = N'',
    @IPTIPOPAC          INT,
    @IPTIPOAFI          INT,
    @CAPACIPAG          INT               = 0,
    @IPDIRECCI          VARCHAR(MAX),
    @IPTELEFON          VARCHAR(MAX)      = N' ',
    @IPESTADOC          INT               = 1,
    @TIPCOBSAL          CHAR(1)           = N'8',
    @INDAUDFOR          NUMERIC(18,0)     = 0,
    @CareGroupId        INT,
    @IdGenderIdentity   INT               = NULL,
    @IDENTMAMA          VARCHAR(25)       = N'',
    @IDENTOBSERVAC      VARCHAR(200)      = N'NO APLICA',
	@Password			VARCHAR(200) 
AS
BEGIN
    SET NOCOUNT ON;

	BEGIN TRY
    BEGIN TRANSACTION;

    -- Verificar si el usuario ya existe en SystemUser
    IF EXISTS (SELECT 1 FROM WebAppointment.SystemUser WHERE Username = @IPCODPACI)
    BEGIN
        -- Retorna 1 si el usuario ya existe en SystemUser
        SELECT 2 AS Result;
    END
    -- Verificar si el usuario ya existe en inpacient
    ELSE IF EXISTS (SELECT 1 FROM dbo.INPACIENT WHERE IPCODPACI = @IPCODPACI)
    BEGIN
        -- Retorna 2 si el usuario ya existe en inpacient
        SELECT 3 AS Result;
    END
    ELSE
    BEGIN
		-- Insertar en INPACIENT

		DECLARE @GenConEntity INT;

		-- 1) Intento principal: HealthAdministrator asociado al CareGroup
		SELECT TOP 1 
			@GenConEntity = HA.Id
		  FROM Contract.HealthAdministrator HA
		  JOIN Contract.Contract C       ON C.HealthAdministratorId = HA.Id
		  JOIN Contract.CareGroup CG     ON CG.ContractId = C.Id
		 WHERE CG.Id = @CareGroupId;

		-- 2) Si no encontró nada, fallback por código del CareGroup
		IF @GenConEntity IS NULL
		BEGIN
			SELECT 
				@GenConEntity = HA.Id
			  FROM Contract.HealthAdministrator HA
			 WHERE HA.Code = (
				   SELECT CG.Code 
					 FROM Contract.CareGroup CG 
					WHERE CG.Id = @CareGroupId
				   );
		END

		INSERT INTO dbo.INPACIENT
			(IPPRINOMB, IPSEGNOMB, IPPRIAPEL, IPSEGAPEL, IPNOMCOMP, IPSEXOPAC, IPFECNACI,
			 IPTELMOVI, CORELEPAC, IPTIPODOC, IPCODPACI, ESTADOPAC, CODIGONIT, IPEXPEDIC,
			 IPTIPOPAC, IPTIPOAFI, CAPACIPAG, IPDIRECCI, IPTELEFON, IPESTADOC, TIPCOBSAL,
			 INDAUDFOR, CODEMPRES, IDPAIS, IdGenderIdentity, IDENTMAMA, IDENTOBSERVAC,
			 CODENTIDA, GENCONENTITY, GENCAREGROUP, NIVCODIGO, AUUBICACI, FECREGCRE)
		VALUES
			(@IPPRINOMB, @IPSEGNOMB, @IPPRIAPEL, @IPSEGAPEL, @IPNOMCOMP, @IPSEXOPAC, @IPFECNACI,
			 @IPTELMOVI, @CORELEPAC, @IPTIPODOC, @IPCODPACI, @ESTADOPAC, @CODIGONIT, @IPEXPEDIC,
			 @IPTIPOPAC, @IPTIPOAFI, @CAPACIPAG, @IPDIRECCI, @IPTELEFON, @IPESTADOC, @TIPCOBSAL,
			 @INDAUDFOR,
			 (SELECT TOP 1 CODEMPRES FROM dbo.ADEMPRESA),
			 1,
			 @IdGenderIdentity,
			 @IDENTMAMA,
			 @IDENTOBSERVAC,
			 (SELECT Code FROM Contract.CareGroup WHERE Id = @CareGroupId),
			 @GenConEntity,
			 @CareGroupId,
			 '04',
			 '1100111001',
			 CONVERT(datetime, SWITCHOFFSET(SYSDATETIMEOFFSET(), '-05:00'))
			);

			INSERT INTO [Admissions].[PatientAddress]
					([IdPatient]
					,[IdAddressType]
					,[IdUbication]
					,[Address]
					,[RuralArea]
					,[IsMain]
					,[IPCODPACI]
					,[ZipCode])
				VALUES
					(null
					,1
					,149
					,@IPDIRECCI
					,'02'
					,1
					,@IPCODPACI
					,null)

			-- Insertar en SystemUser
			INSERT INTO WebAppointment.SystemUser 
			(Username, DocumentType, Password, Email, CreationDate, Status, NumMovil) 
			VALUES (@IPCODPACI, @IPTIPODOC, @Password, @CORELEPAC, GETDATE(), 1, @IPTELMOVI);

			-- Retorna 0 si ambos inserts fueron exitosos
			SELECT 1 AS Result;
			COMMIT TRANSACTION;
			END;
    END TRY
    BEGIN CATCH
        -- Si algo falla, hacer rollbackUserRegistration
        ROLLBACK TRANSACTION;
        THROW
    END CATCH;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra un nuevo paciente y su usuario de acceso al portal de agendamiento web en un solo proceso transaccional. Primero verifica que el documento (cédula o identificación) no exista ya como usuario del sistema ni como paciente en la historia clínica; si ya existe en alguno de los dos, retorna un código de error específico. Si el paciente es nuevo, lo crea en la tabla maestra de pacientes (INPACIENT) con todos sus datos demográficos (nombre, apellidos, sexo, fecha de nacimiento, teléfono, dirección, tipo de documento, tipo de paciente, afiliación, etc.), resuelve la entidad pagadora (EPS/aseguradora) a partir del grupo de atención (CareGroup) y el contrato correspondiente, registra la dirección del paciente, y finalmente crea el usuario de acceso en el sistema de agendamiento web con su contraseña, correo y número de celular. Todo ocurre dentro de una transacción: si cualquier paso falla, se revierte el proceso completo.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'PROCEDURE', @level1name = N'Sp_RegisterUserAndPatient';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'PROCEDURE', @level1name = N'Sp_RegisterUserAndPatient';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra un nuevo paciente y su usuario de acceso al portal de agendamiento web, validando previamente que no exista, resolviendo la entidad pagadora desde el grupo de atención y persistiendo todo de forma transaccional.', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'Sp_RegisterUserAndPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de paciente no debe existir previamente en WebAppointment.SystemUser ni en dbo.INPACIENT; Debe existir al menos un registro en dbo.ADEMPRESA para resolver la empresa por defecto; Debe existir un CareGroup válido referenciado, con contrato y administradora de salud asociados (o al menos coincidencia por código) para resolver la entidad pagadora; Debe proveerse contraseña para el usuario web', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'Sp_RegisterUserAndPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El alta de paciente, dirección y usuario web es atómica: o se persisten los tres o ninguno; Un mismo código de paciente nunca se duplica entre SystemUser e INPACIENT; La entidad pagadora (GENCONENTITY) del paciente siempre proviene del CareGroup, ya sea por contrato o por código; Todo paciente nuevo se crea con IDPAIS=1, NIVCODIGO=''04'' y AUUBICACI=''1100111001''; La empresa asignada (CODEMPRES) corresponde siempre al primer registro de ADEMPRESA; La fecha de creación del paciente se almacena en horario UTC-5; La dirección registrada se marca siempre como principal (IsMain=1) con tipo 1 y ubicación 149', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'Sp_RegisterUserAndPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Usuario web de agendamiento; Grupo de atención (CareGroup); Contrato; Administradora de salud (EPS/aseguradora); Entidad pagadora; Tipo de documento; Tipo de afiliación; Capacidad de pago; Cobertura de salud; Identidad de género; Dirección del paciente', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'Sp_RegisterUserAndPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] WebAppointment.SystemUser: Si ya existe un usuario con Username igual al código de paciente, retorna Result=2 y no inserta nada; [RETURN_RESULT] dbo.INPACIENT: Si el paciente ya existe en INPACIENT con el mismo IPCODPACI, retorna Result=3 y no inserta nada; [INSERT] dbo.INPACIENT: Cuando el paciente no existe en SystemUser ni en INPACIENT, inserta el nuevo paciente con datos demográficos, empresa por defecto (primer CODEMPRES de ADEMPRESA), IDPAIS=1, NIVCODIGO=''04'', AUUBICACI=''1100111001'', CODENTIDA=código del CareGroup, GENCONENTITY=HealthAdministrator resuelta y FECREGCRE en hora local UTC-5; [INSERT] Admissions.PatientAddress: Tras insertar el paciente, registra su dirección con IdAddressType=1, IdUbication=149, RuralArea=''02'', IsMain=1 e IdPatient en NULL; [INSERT] WebAppointment.SystemUser: Tras insertar paciente y dirección, crea el usuario web con Username=código paciente, Status=1, fecha de creación = GETDATE() y los datos de contacto recibidos; [RETURN_RESULT] WebAppointment.SystemUser: Si los tres inserts (INPACIENT, PatientAddress, SystemUser) son exitosos, retorna Result=1 y confirma la transacción; [RAISERROR] dbo.INPACIENT: Si cualquier paso del registro falla, ejecuta ROLLBACK TRANSACTION y relanza la excepción con THROW', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'Sp_RegisterUserAndPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe un SystemUser con Username = código del paciente → Retorna Result=2 y termina sin insertar else Continúa validando en INPACIENT; si Existe un registro en INPACIENT con el mismo código de paciente → Retorna Result=3 y termina sin insertar else Procede con el alta completa; si No se encontró HealthAdministrator vía CareGroup→Contract→HealthAdministrator (GenConEntity IS NULL) → Aplica fallback: resuelve HealthAdministrator buscando por código igual al código del CareGroup else Usa la HealthAdministrator obtenida en la consulta principal', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'Sp_RegisterUserAndPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'WebAppointment.SystemUser; dbo.INPACIENT; Contract.HealthAdministrator; Contract.Contract; Contract.CareGroup; dbo.ADEMPRESA', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'Sp_RegisterUserAndPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'Sp_RegisterUserAndPatient';
-- GO
