
CREATE PROCEDURE [WebAppointment].[Sp_RegisterPatient]
    @IPPRINOMB				CHAR(20),
    @IPSEGNOMB				CHAR(20)           = N'',
    @IPPRIAPEL				CHAR(20),
    @IPSEGAPEL				CHAR(20)           = N'',
    @IPNOMCOMP				CHAR(250),
    @IPSEXOPAC				INT,
    @IPFECNACI				DATETIME,
    @IPTELMOVI				VARCHAR(MAX),
    @CORELEPAC				CHAR(50),
    @IPTIPODOC				INT,
    @IPCODPACI				VARCHAR(25),
    @ESTADOPAC				BIT               = 1,
    @CODIGONIT				VARCHAR(25),
    @IPEXPEDIC				CHAR(40)          = N'',
    @IPTIPOPAC				INT,
    @IPTIPOAFI				INT,
    @CAPACIPAG				INT               = 0,
    @IPDIRECCI				VARCHAR(MAX),
    @IPTELEFON				VARCHAR(MAX)      = N' ',
    @IPESTADOC				INT               = 1,
    @TIPCOBSAL				CHAR(1)           = N'8',
    @INDAUDFOR				NUMERIC(18,0)     = 0,
    @CareGroupId			INT,
	@HealthAdministratorId	INT				  = NULL,
    @IdGenderIdentity		INT               = NULL,
    @IDENTMAMA				VARCHAR(25)       = N'',
    @IDENTOBSERVAC			VARCHAR(200)      = N'NO APLICA'
AS
BEGIN
	BEGIN TRY
    BEGIN TRANSACTION;

		DECLARE @GenConEntity INT;
		SET @GenConEntity = @HealthAdministratorId;

		IF @HealthAdministratorId IS NULL
		BEGIN
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
			 1,  -- IDPAIS fijo
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

		IF @IPDIRECCI IS NOT NULL AND @IPDIRECCI <> ''
		BEGIN
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
		END

		COMMIT TRANSACTION;

	END TRY
    BEGIN CATCH
        -- Si algo falla, hacer rollback
        ROLLBACK TRANSACTION;
        THROW
    END CATCH;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra un nuevo paciente en el sistema a partir del portal de agendamiento web, insertando todos sus datos demográficos y de identificación (nombre, apellidos, tipo y número de documento, sexo, fecha de nacimiento, teléfono, dirección) en la tabla maestra de pacientes (INPACIENT). Antes de la inserción, resuelve automáticamente la entidad pagadora (EPS/administradora de salud) asociada al grupo de atención indicado: primero buscando la administradora vinculada al contrato del grupo de atención y, si no la encuentra, haciendo una búsqueda alternativa por código; de esta forma garantiza que el paciente quede ligado a su EPS y grupo de atención correctos. Adicionalmente, si se proporciona dirección, registra también el domicilio del paciente en la tabla de direcciones de admisiones. El procedimiento existe para centralizar y asegurar la creación transaccional del paciente desde canales digitales externos.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'PROCEDURE', @level1name = N'Sp_RegisterPatient';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'PROCEDURE', @level1name = N'Sp_RegisterPatient';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra transaccionalmente un nuevo paciente desde canales digitales, vinculándolo a su administradora de salud y grupo de atención, y opcionalmente registra su dirección.', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'Sp_RegisterPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos una empresa en dbo.ADEMPRESA para obtener CODEMPRES.; El CareGroupId debe existir en Contract.CareGroup para resolver el código de grupo y la entidad administradora.; Si no se provee HealthAdministratorId, debe poder resolverse vía contrato del CareGroup o por coincidencia de código entre CareGroup y HealthAdministrator.', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'Sp_RegisterPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda la operación (resolución de entidad, inserción de paciente y de dirección) se ejecuta dentro de una única transacción; ante error se revierte completamente.; El paciente siempre queda registrado con país (IDPAIS) fijo en 1.; La fecha de creación se almacena siempre con desfase horario -05:00.; La empresa asignada (CODEMPRES) siempre proviene del primer registro de ADEMPRESA.; La dirección, cuando se crea, siempre se marca como principal (IsMain=1) con tipo 1 y ubicación 149.; El paciente queda siempre ligado al CareGroup recibido y a la entidad administradora derivada de éste cuando no se provee explícitamente.', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'Sp_RegisterPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Administradora de salud (EPS); Contrato; Grupo de atención (CareGroup); Dirección del paciente; Identidad de género; Tipo de documento; Tipo de afiliación; Capacidad de pago; Cobertura de salud', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'Sp_RegisterPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.INPACIENT: Siempre inserta el paciente con IDPAIS=1, NIVCODIGO=''04'', AUUBICACI=''1100111001'', FECREGCRE en hora con offset -05:00, CODEMPRES tomado del primer registro de ADEMPRESA, CODENTIDA=Code del CareGroup, GENCAREGROUP=CareGroupId y GENCONENTITY=HealthAdministrator resuelto.; [INSERT] Admissions.PatientAddress: Cuando @IPDIRECCI no es NULL ni cadena vacía, inserta la dirección con IdAddressType=1, IdUbication=149, RuralArea=''02'', IsMain=1 e IdPatient en NULL, asociándola por IPCODPACI.; [RAISERROR] : Si ocurre cualquier error dentro de la transacción, se hace ROLLBACK y se relanza la excepción con THROW.', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'Sp_RegisterPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @HealthAdministratorId IS NULL → Resuelve la entidad administradora consultando primero el HealthAdministrator asociado al contrato del CareGroup. else Usa directamente el HealthAdministratorId recibido como GENCONENTITY.; si Tras la búsqueda por contrato, @GenConEntity sigue NULL → Aplica fallback buscando HealthAdministrator cuyo Code coincida con el Code del CareGroup.; si @IPDIRECCI IS NOT NULL AND @IPDIRECCI <> '''' → Inserta un registro en Admissions.PatientAddress como dirección principal del paciente. else Omite la inserción de dirección.', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'Sp_RegisterPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.HealthAdministrator; Contract.Contract; Contract.CareGroup; dbo.ADEMPRESA', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'Sp_RegisterPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'Sp_RegisterPatient';
-- GO
