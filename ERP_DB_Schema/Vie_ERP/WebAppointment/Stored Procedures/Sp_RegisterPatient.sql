
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