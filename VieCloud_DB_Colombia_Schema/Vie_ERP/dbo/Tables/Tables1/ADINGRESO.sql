CREATE TABLE [dbo].[ADINGRESO] (
    [NUMINGRES]                   CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]                   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [TIPOINGRE]                   INT                                                                              NOT NULL,
    [IINGREPOR]                   INT                                                                              NOT NULL,
    [ITIPORIES]                   INT                                                                              NOT NULL,
    [ICAUSAING]                   INT                                                                              NOT NULL,
    [CODENTIDA]                   CHAR (9)                                                                         NOT NULL,
    [CODCONTRA]                   CHAR (6)                                                                         NULL,
    [CODPANATE]                   CHAR (2)                                                                         NULL,
    [IFECHAING]                   DATETIME                                                                         NOT NULL,
    [ILIQUIDAC]                   INT                                                                              NOT NULL,
    [ICONTROLI]                   CHAR (15)                                                                        NOT NULL,
    [CODCENATE]                   CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                   CHAR (10)                                                                        NOT NULL,
    [IAUTORIZA]                   VARCHAR (20)                                                                     NULL,
    [IESTADOIN]                   CHAR (1)                                                                         NOT NULL,
    [IINGRESOA]                   CHAR (10)                                                                        NULL,
    [ISOATVALO]                   NUMERIC (18, 2)                                                                  NULL,
    [ISALCODIG]                   CHAR (3)                                                                         NULL,
    [INUMERORE]                   CHAR (15)                                                                        NULL,
    [IFECHAREM]                   DATETIME                                                                         NULL,
    [IAUTORREM]                   CHAR (15)                                                                        NULL,
    [DEPMUNCOD]                   CHAR (5)                                                                         NULL,
    [AIPSREMIS]                   CHAR (100)                                                                       NULL,
    [IOBSERVAC]                   VARCHAR (3000)                                                                   NULL,
    [IJUSTIFIC]                   CHAR (254)                                                                       NULL,
    [IREINGRES]                   INT                                                                              NOT NULL,
    [UFUINGMED]                   CHAR (10)                                                                        NULL,
    [CODPROING]                   CHAR (20)                                                                        NULL,
    [UFUEGRMED]                   CHAR (10)                                                                        NULL,
    [CODPROEGR]                   CHAR (20)                                                                        NULL,
    [UFUINGHOS]                   CHAR (10)                                                                        NULL,
    [UFUEGRHOS]                   CHAR (10)                                                                        NULL,
    [CODESPTRA]                   CHAR (3)                                                                         NULL,
    [TIPOPROFE]                   CHAR (2)                                                                         NULL,
    [UFUAACTMED]                  CHAR (10)                                                                        NULL,
    [UFUAACTHOS]                  CHAR (10)                                                                        NULL,
    [CODCAMACT]                   INT                                                                              NULL,
    [CODDIAING]                   CHAR (4)                                                                         NULL,
    [CODDIAEGR]                   CHAR (4)                                                                         NULL,
    [UFUACTPAC]                   CHAR (10)                                                                        NOT NULL,
    [CODUSUCRE]                   CHAR (20)                                                                        NULL,
    [FECREGCRE]                   DATETIME                                                                         NULL,
    [CODUSUMOD]                   CHAR (20)                                                                        NULL,
    [FECREGMOD]                   DATETIME                                                                         NULL,
    [CODUSUANU]                   CHAR (20)                                                                        NULL,
    [FECREGANU]                   DATETIME                                                                         NULL,
    [NUMINGREI]                   CHAR (10)                                                                        NULL,
    [CODICAMHO]                   CHAR (10)                                                                        NULL,
    [FECHOSPIT]                   DATETIME                                                                         NULL,
    [INDAUDFOR]                   NUMERIC (18)                                                                     NOT NULL,
    [IPRNOMBRE]                   CHAR (80)                                                                        NULL,
    [IPCODACTR]                   CHAR (15)                                                                        NULL,
    [IPEXPEDIC]                   CHAR (40)                                                                        NULL,
    [FECACTRAN]                   DATETIME                                                                         NULL,
    [HORACIDEN]                   CHAR (5)                                                                         NULL,
    [IPTELEFON]                   CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Phone_Ofuscado", 0)')             NULL,
    [OBSERACIT]                   VARCHAR (3000)                                                                   NULL,
    [OBSERAREM]                   VARCHAR (250)                                                                    NULL,
    [INGRECEXT]                   BIT                                                                              NULL,
    [PACATENDI]                   BIT                                                                              NULL,
    [ESCADOWNT]                   CHAR (1)                                                                         NULL,
    [ESCARASS]                    CHAR (1)                                                                         NULL,
    [ESCNORPAC]                   CHAR (1)                                                                         NULL,
    [SERSUSCEP]                   BIT                                                                              NULL,
    [ESCVASPAC]                   CHAR (1)                                                                         NULL,
    [ESCAPAPAC]                   CHAR (1)                                                                         NULL,
    [VIVESOLO]                    BIT                                                                              NULL,
    [NUMTRIAGEI]                  CHAR (20)                                                                        NULL,
    [GENCAREGROUP]                INT                                                                              NULL,
    [DESTINOPAC]                  TINYINT                                                                          NULL,
    [GENCONENTITY]                INT                                                                              NULL,
    [REMTRANSPOR]                 INT                                                                              NULL,
    [REMINGRES]                   INT                                                                              NULL,
    [REMACMED]                    BIT                                                                              NULL,
    [REMACAENF]                   BIT                                                                              NULL,
    [REMACENFJEF]                 BIT                                                                              NULL,
    [REMACOTRO]                   BIT                                                                              NULL,
    [REMACNINSAL]                 BIT                                                                              NULL,
    [REMACFAMILI]                 BIT                                                                              NULL,
    [REMPARACLI]                  BIT                                                                              NULL,
    [REMINFOPARA]                 VARCHAR (200)                                                                    NULL,
    [REMINCOPRINI]                INT                                                                              NULL,
    [REMFUNCREG]                  CHAR (20)                                                                        NULL,
    [TRATAESPECIA]                INT                                                                              NULL,
    [CREADOAUTOMA]                INT                                                                              NULL,
    [FECHEGRESO]                  DATETIME                                                                         NULL,
    [NUMINGREHOSP]                NCHAR (10)                                                                       NULL,
    [REQTRIAGE]                   BIT                                                                              NULL,
    [PRIMERLLA]                   BIT                                                                              NULL,
    [SEGUNDLLA]                   BIT                                                                              NULL,
    [TERCERLLA]                   BIT                                                                              NULL,
    [FECPRILLA]                   DATETIME                                                                         NULL,
    [FECSEGLLA]                   DATETIME                                                                         NULL,
    [FECTERLLA]                   DATETIME                                                                         NULL,
    [OBVAUSENT]                   VARCHAR (2000)                                                                   NULL,
    [FECAUSENT]                   DATETIME                                                                         NULL,
    [PROAUSENT]                   CHAR (20)                                                                        NULL,
    [CODTIPPAC]                   INT                                                                              NULL,
    [ESCABIERI]                   CHAR (1)                                                                         NULL,
    [GENULTLIQUI]                 DATE                                                                             NULL,
    [REINGRESOHOSP]               TINYINT                                                                          NULL,
    [PACIENTESITIOQX]             BIT                                                                              NULL,
    [CUPSPENDFAC]                 BIT                                                                              NULL,
    [FECHA]                       DATETIME                                                                         NULL,
    [LINEA]                       VARCHAR (80)                                                                     NULL,
    [ENTIDAD]                     VARCHAR (80)                                                                     NULL,
    [REGIMEN]                     VARCHAR (80)                                                                     NULL,
    [TIPOAFILIADO]                VARCHAR (80)                                                                     NULL,
    [ESTADO]                      VARCHAR (80)                                                                     NULL,
    [CONFIRMADOERP]               VARCHAR (80)                                                                     NULL,
    [REPORTAIPS]                  VARCHAR (80)                                                                     NULL,
    [SOLRESHEMO]                  BIT                                                                              CONSTRAINT [DF_ADINGRESO_SOLRESHEMO] DEFAULT ((0)) NOT NULL,
    [ESTADOMEDICAMENTOSFARMACE]   INT                                                                              NULL,
    [DEPCODIGOACC]                CHAR (2)                                                                         NULL,
    [MUNCODIGOACC]                CHAR (5)                                                                         NULL,
    [DIRECCIONACC]                VARCHAR (255)                                                                    NULL,
    [ZONAEVENTOCC]                TINYINT                                                                          NULL,
    [IdHealthPurposes]            INT                                                                              NULL,
    [IdUbication]                 INT                                                                              NULL,
    [IdEntryRoutesHealthServices] INT                                                                              NULL,
    [IdAdmissionModalities]       INT                                                                              NULL,
    [IdAdmissionType]             INT                                                                              NULL,
    [IDADTIPOIDENTIFICA]          INT                                                                              NULL,
    [Policy]                      VARCHAR (30)                                                                     NULL,
    [CareSettingCode]             INT                                                                              NULL,
    [IpsAddress] NVARCHAR(200) NULL, 
    [IpsPhone] VARCHAR(15) NULL, 
    [IpsEmail] VARCHAR(200) NULL, 
    [ReferredFromAnotherIPS] BIT NULL, 
    [VictimCondition] TINYINT NULL, 
    [VehicleTypeInvolved] TINYINT NULL, 
    CONSTRAINT [PK_ADINGRESO__NUMINGRES] PRIMARY KEY CLUSTERED ([NUMINGRES] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADINGRESO].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADINGRESO].[IPTELEFON]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Contact Info');





GO

ALTER TABLE [dbo].[ADINGRESO] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = OFF);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_SERSUSCEP_FECHEGRESO_IPCODPACI_IFECHAING_CODESPTRA_UFUACTPAC_ESCADOWNT_ESCARASS_ESCNORPAC_ESCVASPAC_ESCAPAPAC]
    ON [dbo].[ADINGRESO]([SERSUSCEP] ASC, [FECHEGRESO] ASC)
    INCLUDE([IPCODPACI], [IFECHAING], [CODESPTRA], [UFUACTPAC], [ESCADOWNT], [ESCARASS], [ESCNORPAC], [ESCVASPAC], [ESCAPAPAC]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_IESTADOIN_IPCODPACI_CODENTIDA_CODCAMACT_UFUACTPAC_CODTIPPAC]
    ON [dbo].[ADINGRESO]([IESTADOIN] ASC)
    INCLUDE([IPCODPACI], [CODENTIDA], [CODCAMACT], [UFUACTPAC], [CODTIPPAC]);


GO
CREATE NONCLUSTERED INDEX [IDX_IESTADOIN]
    ON [dbo].[ADINGRESO]([IESTADOIN] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_CODCENATE_UFUCODIGO_UFUEGRMED_IESTADOIN]
    ON [dbo].[ADINGRESO]([CODCENATE] ASC, [UFUCODIGO] ASC, [UFUEGRMED] ASC, [IESTADOIN] ASC)
    INCLUDE([IPCODPACI], [VIVESOLO], [CODTIPPAC]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_IESTADOIN_CODCAMACT_CODESPTRA_CODTIPPAC_UFUACTPAC_VIVESOLO]
    ON [dbo].[ADINGRESO]([IESTADOIN] ASC)
    INCLUDE([CODCAMACT], [CODESPTRA], [CODTIPPAC], [UFUACTPAC], [VIVESOLO]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__IPCODPACI__NUMINGRES__CODENTIDA]
    ON [dbo].[ADINGRESO]([IPCODPACI] ASC, [NUMINGRES] ASC, [IFECHAING] ASC, [CODENTIDA] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_GENCONENTITY]
    ON [dbo].[ADINGRESO]([GENCONENTITY] ASC)
    INCLUDE([IPCODPACI], [CODPANATE], [CODCENATE], [UFUCODIGO], [IAUTORIZA], [CODICAMHO], [IPTELEFON]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__IPCODPACI]
    ON [dbo].[ADINGRESO]([IPCODPACI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__NUMINGRES__CODENTIDA__IPCODPACI]
    ON [dbo].[ADINGRESO]([NUMINGRES] ASC, [CODENTIDA] ASC, [IPCODPACI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__CODDIAEGR__NUMINGRES__INC__IFECHAING]
    ON [dbo].[ADINGRESO]([CODDIAEGR] ASC, [NUMINGRES] ASC)
    INCLUDE([IFECHAING]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__IPCODPACI__NUMINGRES__UFUEGRMED__UFUINGMED__IFECHAING__INC__ICAUSAING]
    ON [dbo].[ADINGRESO]([IPCODPACI] ASC, [NUMINGRES] ASC, [UFUEGRMED] ASC, [UFUINGMED] ASC, [IFECHAING] ASC)
    INCLUDE([ICAUSAING]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__IESTADOIN__INC__CODCENATE__CODICAMHO__CODPANATE__IAUTORIZA__IPCODPACI__IPTELEFON__NUMINGRES__UFUCODIGO]
    ON [dbo].[ADINGRESO]([IESTADOIN] ASC)
    INCLUDE([NUMINGRES], [IPCODPACI], [CODPANATE], [CODCENATE], [UFUCODIGO], [IAUTORIZA], [CODICAMHO], [IPTELEFON]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__UFUCODIGO__IESTADOIN__INC__CODCENATE__CODICAMHO__CODPANATE__IAUTORIZA__IPCODPACI__IPTELEFON__NUMINGRES]
    ON [dbo].[ADINGRESO]([UFUCODIGO] ASC, [IESTADOIN] ASC)
    INCLUDE([NUMINGRES], [IPCODPACI], [CODPANATE], [CODCENATE], [IAUTORIZA], [CODICAMHO], [IPTELEFON]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__UFUACTPAC__IESTADOIN__INC__CODCAMACT__NUMINGRES]
    ON [dbo].[ADINGRESO]([UFUACTPAC] ASC, [IESTADOIN] ASC)
    INCLUDE([CODCAMACT], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_TRATAESPECIA_INC_IPCODPACI]
    ON [dbo].[ADINGRESO]([TRATAESPECIA] ASC)
    INCLUDE([IPCODPACI]);


GO
ALTER INDEX [IX_ADINGRESO_TRATAESPECIA_INC_IPCODPACI]
    ON [dbo].[ADINGRESO] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__IFECHAING__IPCODPACI__CODCENATE__NUMINGRES__UFUCODIGO__INC__AIPSREMIS__CODCAMACT__CODCONTRA__CODDIAEGR__CODDIAING_]
    ON [dbo].[ADINGRESO]([IFECHAING] ASC, [IPCODPACI] ASC, [CODCENATE] ASC, [NUMINGRES] ASC, [UFUCODIGO] ASC)
    INCLUDE([AIPSREMIS], [CODCAMACT], [CODCONTRA], [CODDIAEGR], [CODDIAING], [CODENTIDA], [CODESPTRA], [CODICAMHO], [CODPANATE], [CODPROEGR], [CODPROING], [CODUSUANU], [CODUSUCRE], [CODUSUMOD], [DEPMUNCOD], [FECACTRAN], [FECHOSPIT], [FECREGANU], [FECREGCRE], [FECREGMOD], [HORACIDEN], [IAUTORIZA], [IAUTORREM], [ICAUSAING], [ICONTROLI], [IESTADOIN], [IFECHAREM], [IINGREPOR], [IINGRESOA], [IJUSTIFIC], [ILIQUIDAC], [INDAUDFOR], [INGRECEXT], [INUMERORE], [IOBSERVAC], [IPCODACTR], [IPEXPEDIC], [IPRNOMBRE], [IPTELEFON], [IREINGRES], [ISALCODIG], [ISOATVALO], [ITIPORIES], [NUMINGREI], [OBSERACIT], [OBSERAREM], [PACATENDI], [TIPOINGRE], [TIPOPROFE], [UFUAACTHOS], [UFUAACTMED], [UFUACTPAC], [UFUEGRHOS], [UFUEGRMED], [UFUINGHOS], [UFUINGMED]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__NUMINGRES__CODESPTRA__INC__CODCAMACT__DESTINOPAC__ESCADOWNT__ESCAPAPAC__ESCARASS__ESCNORPAC__ESCVASPAC__IFECHAING_]
    ON [dbo].[ADINGRESO]([NUMINGRES] ASC, [CODESPTRA] ASC)
    INCLUDE([CODCAMACT], [DESTINOPAC], [ESCADOWNT], [ESCAPAPAC], [ESCARASS], [ESCNORPAC], [ESCVASPAC], [IFECHAING], [IPCODPACI], [SERSUSCEP], [UFUACTPAC], [VIVESOLO]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__IESTADOIN__UFUINGMED__UFUINGHOS__DESTINOPAC__INC__CODESPTRA__IFECHAING__INGRECEXT__IPCODPACI__NUMINGRES__UFUEGRMED]
    ON [dbo].[ADINGRESO]([IESTADOIN] ASC, [UFUINGMED] ASC, [UFUINGHOS] ASC, [DESTINOPAC] ASC)
    INCLUDE([CODESPTRA], [IFECHAING], [INGRECEXT], [IPCODPACI], [NUMINGRES], [UFUEGRMED], [VIVESOLO]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__IPCODPACI__IESTADOIN]
    ON [dbo].[ADINGRESO]([IPCODPACI] ASC, [IESTADOIN] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__CODCENATE__IESTADOIN__NUMINGRES__INC__IFECHAING]
    ON [dbo].[ADINGRESO]([CODCENATE] ASC, [IESTADOIN] ASC, [NUMINGRES] ASC)
    INCLUDE([IFECHAING]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_IPCODPACINUMINGRES_IFECHAING_VIVESOLO_CODTIPPAC]
    ON [dbo].[ADINGRESO]([IPCODPACI] ASC, [NUMINGRES] ASC, [IFECHAING] ASC)
    INCLUDE([CODTIPPAC], [VIVESOLO]);


GO
CREATE NONCLUSTERED INDEX [UX_ADINGRESO_GENCONENTITY]
    ON [dbo].[ADINGRESO]([GENCONENTITY] ASC)
    INCLUDE([IPCODPACI], [TIPOINGRE], [IINGREPOR], [ITIPORIES], [ICAUSAING], [CODPANATE], [IFECHAING], [ILIQUIDAC], [CODCENATE], [UFUCODIGO], [IAUTORIZA], [IESTADOIN], [CODICAMHO], [IPRNOMBRE], [IPTELEFON], [GENCAREGROUP]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_IESTADOIN_CODENTIDA_CODTIPPAC_VIVESOLO]
    ON [dbo].[ADINGRESO]([IESTADOIN] ASC)
    INCLUDE([CODENTIDA], [CODTIPPAC], [VIVESOLO]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__IESTADOIN__NUMINGRES__IPCODPACI__CODENTIDA__UFUCODIGO__CODCENATE__INC__IFECHAING__IOBSERVAC]
    ON [dbo].[ADINGRESO]([IESTADOIN] ASC, [NUMINGRES] ASC, [IPCODPACI] ASC, [CODENTIDA] ASC, [UFUCODIGO] ASC, [CODCENATE] ASC)
    INCLUDE([IFECHAING], [IOBSERVAC]);


GO

CREATE TRIGGER [dbo].[HospitalCima_Softland_Admission] 
   ON [dbo].[ADINGRESO]
   AFTER  INSERT,DELETE,UPDATE
AS 
BEGIN
	
	SET NOCOUNT ON;

	
	DECLARE @action as  int 
	declare @dataid as varchar(200)

	if exists(select NUMINGRES from inserted ) and exists(select NUMINGRES from deleted) begin
		set @action = 2 --actualizando
		set @dataid = (select top 1 NUMINGRES from inserted)
	end else if exists(select NUMINGRES from inserted ) and not exists(select NUMINGRES from deleted) begin
		set @action = 1 --insertando
		set @dataid = (select top 1 NUMINGRES from inserted)
	end else if not exists(select NUMINGRES from inserted ) and exists(select NUMINGRES from deleted) begin
		set @action = 3 --eliminando
		set @dataid = (select top 1 NUMINGRES from deleted)
	end

	if @dataid is null begin
		return
	end


	insert into [integrations].[cimahospital_softland_synch]
			   ([dataid]
			   ,[type]
			   ,[action]
			   ,[transactiondate]
			   ,[state]
			   ,[errormessage])
		 values
			   (@dataid,1,@action,GETDATE(),0,NULL)
END

-----------------------------------------------------------------------------------------------------------------------------------


/****** Object:  Trigger [dbo].[HospitalCima_Softland_Patient]    Script Date: 8/24/2021 8:37:34 AM ******/
SET ANSI_NULLS ON
GO
DISABLE TRIGGER [dbo].[HospitalCima_Softland_Admission]
    ON [dbo].[ADINGRESO];


GO
create TRIGGER [dbo].[tgg_CambiaEstadoIngresoCerradoBebes] 
   ON  [dbo].[ADINGRESO]
   AFTER INSERT
AS 
BEGIN

	SET NOCOUNT ON;

	--update ADINGRESO set IESTADOIN = ' ' where IESTADOIN = 'C' AND NUMINGRES in (select NUMINGRES from inserted where CREADOAUTOMA = 1 ) 

	update ADINGRESO set IESTADOIN = ' ' from ADINGRESO i inner join inserted t on i.NUMINGRES = t.NUMINGRES where t.CREADOAUTOMA = 1 and t.IESTADOIN = 'C'

END
GO
DISABLE TRIGGER [dbo].[tgg_CambiaEstadoIngresoCerradoBebes]
    ON [dbo].[ADINGRESO];


GO
	CREATE TRIGGER [dbo].[tgg_ActualizarCampoIdAdmissionType] 
   ON  [dbo].[ADINGRESO]
   AFTER INSERT, UPDATE
AS 
BEGIN

	SET NOCOUNT ON;

	update ADINGRESO set IdAdmissionType = 1 from ADINGRESO i inner join inserted t on i.NUMINGRES = t.NUMINGRES where t.IdAdmissionType is null
	update ADINGRESO set IdAdmissionModalities = 1 from ADINGRESO i inner join inserted t on i.NUMINGRES = t.NUMINGRES where t.IdAdmissionModalities is null
END
GO

-----------------------------------------------------------------------------------------------------------------------------------
-- Distribucion de Usuarios de Autorizaciones
-- VieCloud_DB_Colombia_Schema). Emite un evento de outbox cuando se crea un
-- ingreso Hospitalario o cuando cambia el estado (IESTADOIN) de uno ya
-- existente, para que el consumer de autorizadores evalue asignacion.

CREATE TRIGGER [dbo].[TR_ADINGRESO_AdmissionStatusChanged]
   ON [dbo].[ADINGRESO]
   AFTER INSERT, UPDATE
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		-- ---- Rama ALTA: fila nueva (no existia en deleted) creada como Hospitalario ----
		INSERT INTO [Admissions].[OutboxEvent]
			([EventType], [AggregateType], [AggregateId], [PayloadJson], [OccurredAtUtc])
		SELECT
			'clinical.admission-registered.v1',
			'Admission',
			i.[NUMINGRES],
			(
				SELECT
					i.[NUMINGRES] AS admissionNumber,
					i.[IPCODPACI] AS patientCode,
					i.[IFECHAING] AS admissionDate,
					i.[IESTADOIN] AS admissionStatus,
					i.[CODENTIDA] AS entityCode,
					i.[CODCENATE] AS careCenterCode
				FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
			),
			SYSUTCDATETIME()
		FROM inserted i
		WHERE NOT EXISTS (SELECT 1 FROM deleted d WHERE d.[NUMINGRES] = i.[NUMINGRES])
		  AND i.[TIPOINGRE] = 2; -- Hospitalario (1=Ambulatorio, 2=Hospitalario)

		-- ---- Rama CAMBIO DE ESTADO: fila existente cuyo IESTADOIN cambio ----
		IF UPDATE(IESTADOIN)
		BEGIN
			INSERT INTO [Admissions].[OutboxEvent]
				([EventType], [AggregateType], [AggregateId], [PayloadJson], [OccurredAtUtc])
			SELECT
				'clinical.admission-status-changed.v1',
				'Admission',
				i.[NUMINGRES],
				(
					SELECT
						i.[NUMINGRES] AS admissionNumber,
						i.[IPCODPACI] AS patientCode,
						i.[IFECHAING] AS admissionDate,
						i.[IESTADOIN] AS admissionStatus,
						i.[CODENTIDA] AS entityCode,
						i.[CODCENATE] AS careCenterCode
					FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
				),
				SYSUTCDATETIME()
			FROM inserted i
			INNER JOIN deleted d ON d.[NUMINGRES] = i.[NUMINGRES]
			WHERE i.[IESTADOIN] <> d.[IESTADOIN]
			  AND i.[TIPOINGRE] = 2; -- Hospitalario (1=Ambulatorio, 2=Hospitalario)
		END
	END TRY
	BEGIN CATCH
		-- Choque de UNIQUE(BusinessEventHash) (SQL 2627/2601): el evento ya fue
		-- emitido para este NUMINGRES+estado+timestamp; no es un error real, no
		-- debe abortar el INSERT/UPDATE original de ADINGRESO.
		IF ERROR_NUMBER() NOT IN (2627, 2601)
			THROW;
	END CATCH
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Póliza del grupo de atención de salud (EAPB/asegurador); identificador de cobertura del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'Policy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la poliza del grupo de atencion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'Policy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'Policy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de ingreso (ambulatorio, hospitalario, urgencia); FK a Admission.AdmissionType.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IdAdmissionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'se relaciona con la tabla tipo de ingreso Admission.AdmissionType', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IdAdmissionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IdAdmissionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de modalidades de atención (hospitalización, consulta externa, urgencias, daycare).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IdAdmissionModalities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id de modalidades de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IdAdmissionModalities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IdAdmissionModalities';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la vía de ingreso del paciente (urgencias, consulta externa, remisión, nacido en institución).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IdEntryRoutesHealthServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la vía de ingreso del maestro de "Vías ingreso servicios salud"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IdEntryRoutesHealthServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IdEntryRoutesHealthServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de ubicación o localización del paciente en la institución de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IdUbication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda elId de Ubicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IdUbication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IdUbication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la finalidad de la HC que se han parametrizado desde el maestro Finalidades tecnología de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IdHealthPurposes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Zona del evento de accidente: 1=Urbana, 2=Rural; clasificación geográfica para acidente de tránsito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ZONAEVENTOCC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Zona Evento:  1 - Urbana  2 - Rural  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ZONAEVENTOCC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ZONAEVENTOCC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección completa donde ocurrió el accidente de tránsito (calle, número, ciudad).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'DIRECCIONACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion del accidente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'DIRECCIONACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'DIRECCIONACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del municipio (INMUNICIP) de la IPS que remite o donde ocurrió el accidente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'MUNCODIGOACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del municipio (INMUNICIP)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'MUNCODIGOACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'MUNCODIGOACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del departamento (INDEPARTA) de la IPS que remite o donde ocurrió el accidente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'DEPCODIGOACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del departamento (INDEPARTA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'DEPCODIGOACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'DEPCODIGOACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado medicamentos farmacéutica: 1=modificado por médico, 0=verificado por químico/farmacéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESTADOMEDICAMENTOSFARMACE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me especifica como esta el estado de los medicamentos de la atención farmaceutica.    - Cuando el medico modifique la prescripción medica o agregue un nuevo medicamento este campo estara en 1 que significa que esta modificado.    - Cuando el Quimico desde el dashboard de Atención farmaceutica verifique estos medicamentos este campo pasara a 0     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESTADOMEDICAMENTOSFARMACE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESTADOMEDICAMENTOSFARMACE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Solicitud de reserva de hemocomponentes (sangre, plasma) para transfusión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'SOLRESHEMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Solicitud de reservas de hemoconponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'SOLRESHEMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'SOLRESHEMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo cabecera reporte factura: información de reportabilidad a la IPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REPORTAIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para el pie de pagina, para el reportes de factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REPORTAIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REPORTAIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo cabecera reporte factura: estado de confirmación en ERP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CONFIRMADOERP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para el pie de pagina, para el reportes de factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CONFIRMADOERP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CONFIRMADOERP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo cabecera reporte factura: estado actual del ingreso/atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para el pie de pagina, para el reportes de factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo cabecera reporte factura: tipo de afiliación del paciente (cotizante, beneficiario, etc).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TIPOAFILIADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para el pie de pagina, para el reportes de factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TIPOAFILIADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TIPOAFILIADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo cabecera reporte factura: régimen de salud (contributivo, subsidiado, especial).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REGIMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para el pie de pagina, para el reportes de factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REGIMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REGIMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo cabecera reporte factura: entidad aseguradora o EAPB responsable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ENTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para el pie de pagina, para el reportes de factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ENTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo cabecera reporte factura: línea o producto de la póliza.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'LINEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para el pie de pagina, para el reportes de factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'LINEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'LINEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo cabecera reporte factura: fecha de generación/referencia del documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para el pie de pagina, para el reportes de factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador pendiente facturación ambulatoria: True=CUPS realizados sin facturar, False=sin pendiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CUPSPENDFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo solo se llena para ingresos ambulatorios.    True: Cuando existen CUPS realizados durante una HC de CE o una Nota administrativa que no han sido facturados    False: cuando no hay pendiente CUPS por facturar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CUPSPENDFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CUPSPENDFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador paciente presente en quirófano: True=paciente listo para cirugía, False=pendiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'PACIENTESITIOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica que el paciente ya esta en sitio para la realizacion de la cirugia.    este campo se actualiza desde control servicios ambulatorio de VIE ERP  al momento de facturar la programacion de cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'PACIENTESITIOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'PACIENTESITIOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación reingreso hospitalario: 0=no definido, 1=sí es reingreso, 2=no es reingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REINGRESOHOSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que permite Identificar si se definio como reingreso o no.  0- no definido  1-Definido como reingreso = SI  2-Definido como reingreso = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REINGRESOHOSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REINGRESOHOSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la última liquidación de estancias/servicios (VIEW de gestión hospitalaria).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'GENULTLIQUI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de la última liquidación de estancias View', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'GENULTLIQUI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'GENULTLIQUI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo sin documentación (escala bieri); revisar con desarrolladores de la solución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCABIERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(No se encontró documentación de este campo en la solución de crystal ni información por parte de los desarrolladores.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCABIERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCABIERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código tipo de paciente/población: 1=maternas, 2=menores 5 años, 3=adultos mayores, 4=discapacitados, 5=población general.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODTIPPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Paciente-Población:  1: Maternas  2: Menores de 5 Años  3: Adultos Mayores  4: Discapacitados  5: Poblacion General', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODTIPPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODTIPPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/nombre del profesional de salud que justifica la inasistencia del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'PROAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que austenta el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'PROAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'PROAUSENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se registra la ausencia/inasistencia del paciente a la cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se austenta el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECAUSENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación/justificación de la inasistencia del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'OBVAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación de ausenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'OBVAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'OBVAUSENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del tercer llamado al paciente (aplica atención primaria).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECTERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de tercer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECTERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECTERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del segundo llamado al paciente (aplica atención primaria).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECSEGLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de segundo llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECSEGLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECSEGLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del primer llamado al paciente (aplica atención primaria).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECPRILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Primer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECPRILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECPRILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador tercer llamado: 1=realizado (solo para atención primaria).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TERCERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercer llamado (aplica solo para ingresos creados para unidad de atención primaria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TERCERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TERCERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador segundo llamado: 1=realizado (solo para atención primaria).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'SEGUNDLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo llamado (aplica solo para ingresos creados para unidad de atención primaria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'SEGUNDLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'SEGUNDLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador primer llamado: 1=realizado (solo para atención primaria).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'PRIMERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer llamado (aplica solo para ingresos creados para unidad de atención primaria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'PRIMERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'PRIMERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador requerimiento de triage: 1=requiere triage en unidad de atención prioritaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REQTRIAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que especifica si el ingreso requiere triage en caso de que se haya seleccionado una unidad de tipo atención prioritaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REQTRIAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REQTRIAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso hospitalario anterior (reingreso a cama de hospitalización).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'NUMINGREHOSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el ingreso seleccionado, cuando es un reingreso a hospitalización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'NUMINGREHOSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'NUMINGREHOSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de egreso/alta del paciente (se registra al liberar cama).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECHEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Egreso de paciente se registra cuando se libera la cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECHEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECHEGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador ingreso automático: 1=creado automáticamente desde control recién nacido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CREADOAUTOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campos que si esta en 1 quiere deciar que fue un Ingreso creado automatico desde el control de recien nacido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CREADOAUTOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CREADOAUTOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código tratamiento especializado: 1=Normal, 2=Renal, 3=Oncología-Quimio, 4=Oncología-Radio, 5=Oncología-Braqui.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TRATAESPECIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-Normal  2-Renal  3-Oncologico - Quimioterapia  4-Oncologico - Radioterapia  5-Oncologico - Braquiterapia  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TRATAESPECIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TRATAESPECIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del funcionario que registra la remisión del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMFUNCREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Funcionario que Registra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMFUNCREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMFUNCREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de inconsistencia primer nivel identificada en remisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMINCOPRINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inconsistencia primer nivel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMINCOPRINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMINCOPRINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información de exámenes paraclínicos acompañantes de la remisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMINFOPARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Información paraclinicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMINFOPARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMINFOPARA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador paraclínicos: 1=se registran exámenes paraclínicos en la remisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMPARACLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registra Paraclinicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMPARACLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMPARACLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador acompañante remisión: 1=familiar/acudiente acompaña al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACFAMILI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Personal que acompaña: Familiar acudiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACFAMILI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACFAMILI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador acompañante remisión: 1=ningún personal de salud acompaña.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACNINSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Personal que acompaña: Ningun personal de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACNINSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACNINSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador acompañante remisión: 1=otro personal de salud acompaña.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Personal que acompaña: Otro Personal Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador acompañante remisión: 1=auxiliar jefe enfermería acompaña.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACENFJEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Personal que acompaña: Auxiliar Jefe Enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACENFJEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACENFJEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador acompañante remisión: 1=auxiliar enfermería acompaña.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACAENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Personal que acompaña: Auxiliar enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACAENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACAENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador acompañante remisión: 1=médico acompaña al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Personal que acompaña: Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMACMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código medio/forma de ingreso en remisión (transporte, forma asistida).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medio Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código tipo transporte para remisión (ambulancia, particular, otro).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMTRANSPOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medio transporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMTRANSPOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REMTRANSPOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad en sistema VIE ERP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad en VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Destino paciente ambulatorio: 1=con salida y órdenes, 2=órdenes sin estancia, 3=con estancia, 4=salida parcial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'DESTINOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina el Destino del paciente cuando el ingreso es ambulatorio.  1 - Pacientes con Salida y órdenes médicas  2 - Pacientes con órdenes medicas sin definir estancia  3 - Pacientes con orden de estancia  4 - Paciente con orden salida parcial (se contempla logica de salida parcial. HCHISPACA  = INDICAPAC = 17 - salida parcial)    Campo que se registra apartir del destino del paciente segun el tablero   de la Historia clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'DESTINOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'DESTINOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de atención en sistema VIE ERP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atencion en VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o folio de triage asignado en urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'NUMTRIAGEI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'número de triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'NUMTRIAGEI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'NUMTRIAGEI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador convivencia: 1=paciente vive solo, 0=vive con otros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'VIVESOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'vive solo 1: si 0: no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'VIVESOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'VIVESOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Escala APACHE (Acute Physiology And Chronic Health Evaluation).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCAPAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'escala apache', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCAPAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCAPAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Escala VAS (Visual Analog Scale) de dolor/síntoma del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCVASPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'escala vas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCVASPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCVASPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador servicios susceptibles de autorización: 1=sí, 0=no.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'SERSUSCEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicios Susceptibles de autorizacion 1-True 0-False.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'SERSUSCEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'SERSUSCEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Escala de Norton para riesgo de úlcera por presión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCNORPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'escala norton', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCNORPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCNORPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Escala RASS (Richmond Agitation-Sedation Scale) de sedación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCARASS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'escala de rass', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCARASS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCARASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Escala Downton riesgo de caídas: 1=sí presenta riesgo, 0=no.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCADOWNT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presenta riesgo de caidas Down Ton  0: No . 1: Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCADOWNT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESCADOWNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador atención realizada: 1=paciente fue atendido, 0=no asistió.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'PACATENDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si el paciente fue atendido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'PACATENDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'PACATENDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador ingreso autogenerado desde consulta externa/historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'INGRECEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingreso Autogenerado Consulta Externa (Historia Clinica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'INGRECEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'INGRECEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones específicas para remisión del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'OBSERAREM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones para cuando es seleccionado Ingresa por Remision.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'OBSERAREM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'OBSERAREM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de datos del acompañante en accidente de tránsito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'OBSERACIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones para datos del acompañante Opcion para cuando es seleccionado accidente de transito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'OBSERACIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'OBSERACIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico (PII) del acompañante; enmascarado por seguridad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IPTELEFON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Telefonico Fijo del acompañante Opcion para cuando es seleccionado accidente de transito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IPTELEFON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IPTELEFON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora exacta del accidente de tránsito (HH:MM).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'HORACIDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora del accidente de transito Opcion para cuando es seleccionado accidente de transito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'HORACIDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'HORACIDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del accidente de tránsito (YYYY-MM-DD).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECACTRAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Accidente Transito Opcion para cuando es seleccionado accidente de transito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECACTRAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECACTRAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar de expedición del documento de identidad del acompañante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IPEXPEDIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar de Expedicion del Documento de Identificacion del Acompañante Opcion para cuando es seleccionado accidente de transito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IPEXPEDIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IPEXPEDIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación (PII) del acompañante; enmascarado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IPCODACTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion del acompante Opcion para cuando es seleccionado accidente de transito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IPCODACTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IPCODACTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del acompañante en accidente de tránsito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IPRNOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'nombre del acompañante Opcion para cuando es seleccionado accidente de transito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IPRNOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IPRNOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoria/seguimiento del ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para el registro de auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de hospitalización/asignación de cama al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECHOSPIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Hospitalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECHOSPIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECHOSPIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cama asignada al paciente hospitalizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODICAMHO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODICAMHO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODICAMHO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso relacionado/anterior en caso de reingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'NUMINGREI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso Relacionado del Reingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'NUMINGREI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'NUMINGREI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de anulación del registro de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECREGANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Anulacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECREGANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECREGANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario que anuló el ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODUSUANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario de anula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODUSUANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODUSUANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECREGMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Modificacion del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECREGMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECREGMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario que modificó el ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario que modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECREGCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creacion del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECREGCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'FECREGCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario que creó el ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario que crea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad funcional actual del paciente (últimas actividades clínicas).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUACTPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad Funcional Actual del Paciente:  Tomada de la ultima actividad historia clinica/hospitalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUACTPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUACTPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico de egreso según clasificación CIE-10.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODDIAEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico de Egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODDIAEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODDIAEGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico de ingreso según clasificación CIE-10.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODDIAING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODDIAING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODDIAING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cama actual del paciente (gestión hospitalaria).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODCAMACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gestion Hospitalaria:  Especifica el Codigo de la Cama Actual del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODCAMACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODCAMACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad funcional actual del paciente (de la cama hospitalaria).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUAACTHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gestion Hospitalaria: Especifica la Unidad Funcional Actual del Paciente  Se obtiene de la cama actual del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUAACTHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUAACTHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad funcional actual del paciente (de última historia clínica).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUAACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Historias Clinicas: Especifica la Unidad Funcional Actual del Paciente  Se obtiene de la ultima historia clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUAACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUAACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de profesional: NULL=no existe, 1=médico general, 2=médico especialista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TIPOPROFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Profesional   NULL=No Existe;  1=Medico General;  2=Medico Especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TIPOPROFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TIPOPROFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código especialidad médica tratante actual (hospitalización); actualizable por interconsultas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODESPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Historias Clinicas: Codigo de la Especialidad Tratante  Opcion valida solo para unidades funcionales de tipo Hospitalizacion - Se actualiza con Interconsultas. Muestra la especialidad acutal tratante, no se tiene opcion para guardar historico de especialidades tratantes, opcionalmente se puede obtener de HCHISPACA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODESPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODESPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad funcional donde egresa paciente (gestión hospitalaria/cama).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUEGRHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gestion Hospitalaria: Especifica la Unidad Funcional donde se Egresa el Paciente  Nota: Se obtiene cuando se egresa de la  cama.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUEGRHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUEGRHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad funcional donde se hospitaliza paciente (asignación de cama).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUINGHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gestion Hospitalaria: Especifica la Unidad Funcional donde se Hospitaliza el Paciente  Nota: Se obtiene cuando se asigna cama por primera vez para el ingreso.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUINGHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUINGHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional que realiza historia clínica final/egreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODPROEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud que Realiza la Historia Final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODPROEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODPROEGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad funcional donde egresa paciente por historia clínica (salida).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUEGRMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Historia Clinica: Especifica la Unidad Funcional por donde Egresa el Paciente  Nota: Se Obtiene de la Historia Clinica donde se da Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUEGRMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUEGRMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional que realiza historia clínica inicial/ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODPROING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Historias Clinicas: Codigo del Profesional de la Salud que Realiza la Historia Inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODPROING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODPROING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad funcional donde ingresa paciente (de historia clínica inicial).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUINGMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Historias Clinicas: Especifica la Unidad Funcional por donde Ingresa el Paciente  Corresponde a la unidad funcional donde se realiza la historia de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUINGMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUINGMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador reingreso a urgencias: 1=sí, 2=no definido, 0=no.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IREINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si es un Reingreso a urgencias  1-> Si  2-> No definido  0 -> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IREINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IREINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación de anulación del ingreso (observaciones administrativas).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IJUSTIFIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para justificar la anulacion del Ingreso si aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IJUSTIFIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IJUSTIFIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones generales administrativas del ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IOBSERVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de uso administrativo para especificar observaciones generales del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IOBSERVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IOBSERVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificador de la IPS que remite al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'AIPSREMIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica la IPS que Remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'AIPSREMIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'AIPSREMIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código municipio de la IPS que remite (INMUNICIP).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica el codigo del Municipio de la IPS que remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de autorización de la remisión (si aplica).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IAUTORREM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica el Numero Autorizacion de la Remision si aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IAUTORREM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IAUTORREM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de generación de la remisión (si aplica).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IFECHAREM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica la Fecha de la remision si aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IFECHAREM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IFECHAREM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de remisión/referencia (si aplica).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'INUMERORE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica el Numero de Remision si aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'INUMERORE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'INUMERORE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código SMLV (Salario Mínimo Legal Vigente) para liquidación de cuenta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ISALCODIG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica el Codigo del SMLV con el que se liquidará la Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ISALCODIG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ISALCODIG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor facturado de ingreso por accidente de tránsito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ISOATVALO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Facturado si es un Ingreso de un Paciente Remitido por Accidente de Transito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ISOATVALO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ISOATVALO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso relacionado en reingreso por accidente de tránsito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IINGRESOA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso Relacionado si es un reingreso por Accidente de Transito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IINGRESOA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IINGRESOA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del ingreso: '''''''' ''''''''=sin confirmar, ''''''''F''''''''=confirmado, ''''''''A''''''''=anulado, ''''''''C''''''''=cerrado, ''''''''P''''''''=parcial, ''''''''B''''''''=bloqueado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IESTADOIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica el Estado del Ingreso: '''' '''' = Sin Confirmar Hoja de Trabajo  ''''F'''' = Confirmada Hoja de Trabajo   ''''A'''' = Anulado  ''''C'''' = Cerrado  ''''P'''' = Parcial; Solo cuando es Integración con Vie queda un estado en ''''P''''  ''''B'''' = ingreso bloqueado, estado del ERP que hace este tipo de bloqueos en liquidación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IESTADOIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IESTADOIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de autorización del ingreso (requerido según contrato/UF).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IAUTORIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Autorizacion para el Ingreso, este campo solo se exigirá si en el contrato se estipula "Exigir Autorizacion" para la Unidad de Servicio seleccionada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IAUTORIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IAUTORIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional por donde ingresa el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo de la Unidad Funcional por donde Ingresa el Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención donde ingresa el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Centron de Atencion en donde Ingresa el Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control administrativo por ingreso (campo de gestión).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ICONTROLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Administrativo que se puede usar para establecer controles por ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ICONTROLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ICONTROLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador liquidación a cargo paciente: 1=copago, 2=cuota moderadora, 3=no aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ILIQUIDAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si Liqiuida Valores a Cargo del Paciente:  1 = Copago  2 = Cuota Moderadora  3 = No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ILIQUIDAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ILIQUIDAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de ingreso del paciente (YYYY-MM-DD HH:MM:SS).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IFECHAING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IFECHAING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IFECHAING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan de beneficio/cobertura del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODPANATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plan de Beneficio del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODPANATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODPANATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del contrato con EAPB/asegurador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Contrato de la EAPB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de entidad aseguradora por la que ingresa el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Entidad por la que ingresa el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de causa de atención/ingreso (maestro de causas parametrizado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ICAUSAING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica la Causa de atención (Causa del ingreso) que se parametriza desde el maestro de causas de atención ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ICAUSAING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ICAUSAING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código tipo de riesgo: 1=enfermedad general/maternidad, 2=accidente tránsito, 3=catástrofe.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ITIPORIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica el Tipo de Riesgo:  1=Enfermedad General y Maternidad   2=Accidente de Transito   3=Catastrofe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ITIPORIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ITIPORIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código forma de ingreso: 1=urgencias, 2=consulta externa, 3=nacido, 4=remitido, 5=hospitalización urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IINGREPOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica por donde ingreso el paciente:  1 = Urgencias   2 = Consulta Externa   3 = Nacido Hospital   4 = Remitido   5 = Hospitalización de Urgencias ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IINGREPOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IINGREPOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ingreso: 1=ambulatorio, 2=hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TIPOINGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Ingreso:  1 = Ambulatorio  2 = Hospitalario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TIPOINGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TIPOINGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del paciente (PII enmascarado); cédula, pasaporte o documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de ingreso/atención (formato: 00000001); clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso    Nota: Codigo 00000001', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_IPCODPACI_TRATA_ESTADO_FECHA]
    ON [dbo].[ADINGRESO]([IPCODPACI] ASC, [TRATAESPECIA] ASC, [IESTADOIN] ASC, [IFECHAING] DESC)
    INCLUDE([NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_IPCODPACI_ESTADO_FECHA]
    ON [dbo].[ADINGRESO]([IPCODPACI] ASC, [IESTADOIN] ASC, [IFECHAING] DESC)
    INCLUDE([NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_NUMINGRES_CODCENATE]
    ON [dbo].[ADINGRESO]([NUMINGRES] ASC)
    INCLUDE([CODCENATE]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_NumIngres]
    ON [dbo].[ADINGRESO]([NUMINGRES] ASC)
    INCLUDE([GENCONENTITY]);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Dirección física o postal de la IPS. Campo opcional.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ADINGRESO',
    @level2type = N'COLUMN',
    @level2name = N'IpsAddress'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Número de teléfono de la IPS.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ADINGRESO',
    @level2type = N'COLUMN',
    @level2name = N'IpsPhone'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Correo electrónico de la IPS. Campo opcional.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ADINGRESO',
    @level2type = N'COLUMN',
    @level2name = N'IpsEmail'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Indica si el paciente fue remitido desde otra IPS (1=Sí, 0=No).',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ADINGRESO',
    @level2type = N'COLUMN',
    @level2name = N'ReferredFromAnotherIPS'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Condición de la víctima en el accidente (1=Conductor, 2=Pasajero, 3=Peatón, 4=Desconocido)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ADINGRESO',
    @level2type = N'COLUMN',
    @level2name = N'VictimCondition'


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de ingresos o admisiones de pacientes: urgencias, hospitalizaciones, consulta externa y demás modalidades de atención. Cada fila representa un episodio de ingreso de un paciente a un centro de atención, con datos de identificación, aseguradora, contrato, fechas, estado, triage, remisiones y auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identidad del paciente en el momento del ingreso (cédula, tarjeta de identidad, pasaporte, registro civil, etc.), referenciado como código entero.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IDADTIPOIDENTIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IDADTIPOIDENTIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del ámbito o modalidad de atención donde se produce el ingreso (por ejemplo: hospitalario, ambulatorio, urgencias, domiciliario), usado para clasificar el entorno asistencial del episodio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CareSettingCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CareSettingCode';
