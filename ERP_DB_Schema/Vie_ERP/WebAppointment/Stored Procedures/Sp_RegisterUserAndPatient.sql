
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