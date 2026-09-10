CREATE TABLE [dbo].[AGASICITA] (
    [CODAUTONU]                INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODESPECI]                CHAR (3)                                                                         NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [FECHORAIN]                DATETIME                                                                         NOT NULL,
    [FECHORAFI]                DATETIME                                                                         NOT NULL,
    [CODIGOCON]                CHAR (6)                                                                         NULL,
    [CODACTMED]                CHAR (3)                                                                         NULL,
    [CODTIPSOL]                INT                                                                              NOT NULL,
    [CODTIPCIT]                CHAR (1)                                                                         NULL,
    [CODESTCIT]                CHAR (1)                                                                         NOT NULL,
    [CITAEXTRA]                BIT                                                                              NOT NULL,
    [OBSERVACI]                NVARCHAR (500)                                                                   NULL,
    [ESTENVSMS]                BIT                                                                              NOT NULL,
    [CODUSUASI]                CHAR (20)                                                                        NOT NULL,
    [FECREGSIS]                DATETIME                                                                         NOT NULL,
    [OBSCITPRE]                NVARCHAR (500)                                                                   NULL,
    [FECINICIT]                DATETIME                                                                         NULL,
    [FECITADES]                DATETIME                                                                         NULL,
    [TIPSOLICITU]              INT                                                                              NULL,
    [IDSALA]                   INT                                                                              NULL,
    [CANCELUSU]                CHAR (20)                                                                        NULL,
    [FECHCANCELA]              DATETIME                                                                         NULL,
    [CODCAUCAN]                CHAR (3)                                                                         NULL,
    [OBSCAUCAN]                VARCHAR (200)                                                                    NULL,
    [IDEQUIPOTRA]              INT                                                                              NULL,
    [GENGENERATESO]            BIT                                                                              NULL,
    [CODSERIPS]                CHAR (20)                                                                        NULL,
    [RELCITAINGRE]             BIT                                                                              NULL,
    [NUMINGRES]                CHAR (10)                                                                        NULL,
    [CODDIAGNO]                CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [TIPTRATAMIENTO]           INT                                                                              NULL,
    [IDTURNOSALA]              INT                                                                              NULL,
    [FASE]                     INT                                                                              NULL,
    [OTRAFASE]                 VARCHAR (50)                                                                     NULL,
    [CICLO]                    INT                                                                              NULL,
    [FECPROCT]                 DATETIME                                                                         NULL,
    [IDRIASCUPS]               INT                                                                              NULL,
    [FECHAOFERTADA]            DATETIME                                                                         NULL,
    [CODCAUINA]                CHAR (4)                                                                         NULL,
    [OBSCAUINA]                VARCHAR (200)                                                                    NULL,
    [CODUSUINA]                CHAR (20)                                                                        NULL,
    [FECHAINA]                 DATETIME                                                                         NULL,
    [CONFASIST]                CHAR (1)                                                                         CONSTRAINT [DF_AGASICITA_CONFASIST] DEFAULT ((3)) NULL,
    [GENCAREGROUP]             INT                                                                              NULL,
    [GENCONENTITY]             INT                                                                              NULL,
    [NUMAUTORI]                VARCHAR (30)                                                                     NULL,
    [TIPSERIPS]                CHAR (4)                                                                         NULL,
    [DESMOTANU]                VARCHAR (500)                                                                    NULL,
    [IDHCRADESQUEMAS]          INT                                                                              NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [IDHCORDCICLOSD]           INT                                                                              NULL,
    [CONFIRMQUIMIO]            BIT                                                                              NULL,
    [CODUSUARIOREPRO]          CHAR (20)                                                                        NULL,
    [CODMOTIVOREPRO]           CHAR (4)                                                                         NULL,
    [JUTIFICACIONREPRO]        VARCHAR (500)                                                                    NULL,
    [COMENTARIOREPRO]          VARCHAR (200)                                                                    NULL,
    [FECHAREPRO]               DATETIME                                                                         NULL,
    [USUCONFIRMQUIMIO]         CHAR (20)                                                                        NULL,
    [IDHCRADORDEN]             INT                                                                              NULL,
    [MODALIDAD]                TINYINT                                                                          NULL,
    [CONFIRMCITA]              BIT                                                                              NULL,
    [USUCONFIRM]               CHAR (20)                                                                        NULL,
    [FECHACONFIRM]             DATETIME                                                                         NULL,
    [NUMINGRESCONFIRM]         CHAR (10)                                                                        NULL,
    [IDCITAPADRE]              INT                                                                              NULL,
    [IDAGENDA]                 INT                                                                              NULL,
    [TIPOVISADO]               INT                                                                              NULL,
    [OBSVISADO]                VARCHAR (MAX)                                                                    NULL,
    [TraceabilityPaperworkId]  INT                                                                              NULL,
    [IdCitaSync]               VARCHAR (25)                                                                     NULL,
    [IndigoSyncId]             VARCHAR (36)                                                                     NULL,
    [IdFrequentlyAppointment]  UNIQUEIDENTIFIER                                                                 NULL,
    [Laterality]               INT                                                                              NULL,
    [IdHCORDPRON]              INT                                                                              NULL,
    CONSTRAINT [PK_AGASICITA] PRIMARY KEY CLUSTERED ([CODAUTONU] ASC),
    CONSTRAINT [FK_AGASICITA_HCMOANULB] FOREIGN KEY ([CODMOTIVOREPRO]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_AGASICITA_HCQUICICLOS] FOREIGN KEY ([IDHCORDCICLOSD]) REFERENCES [EHR].[HCORDCICLOSD] ([ID]),
    CONSTRAINT [FK_AGASICITA_HCRADESQUEMAS] FOREIGN KEY ([IDHCRADESQUEMAS]) REFERENCES [dbo].[HCRADESQUEMAS] ([ID]),
    CONSTRAINT [FK_AGASICITA_HCRADORDEN] FOREIGN KEY ([IDHCRADORDEN]) REFERENCES [dbo].[HCRADORDEN] ([ID]),
    CONSTRAINT [FK_AGASICITA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO


GO
ALTER TABLE [dbo].[AGASICITA] NOCHECK CONSTRAINT [FK_AGASICITA_HCQUICICLOS];


GO
ALTER TABLE [dbo].[AGASICITA] NOCHECK CONSTRAINT [FK_AGASICITA_HCRADESQUEMAS];


GO
ALTER TABLE [dbo].[AGASICITA] NOCHECK CONSTRAINT [FK_AGASICITA_HCRADORDEN];


GO
ALTER TABLE [dbo].[AGASICITA] NOCHECK CONSTRAINT [FK_AGASICITA_INPACIENT];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGASICITA].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGASICITA].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGASICITA].[CODDIAGNO]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');




GO


GO
ALTER TABLE [dbo].[AGASICITA] NOCHECK CONSTRAINT [FK_AGASICITA_HCQUICICLOS];


GO
ALTER TABLE [dbo].[AGASICITA] NOCHECK CONSTRAINT [FK_AGASICITA_HCRADESQUEMAS];


GO
ALTER TABLE [dbo].[AGASICITA] NOCHECK CONSTRAINT [FK_AGASICITA_HCRADORDEN];


GO
ALTER TABLE [dbo].[AGASICITA] NOCHECK CONSTRAINT [FK_AGASICITA_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [FECHORAIN]
    ON [dbo].[AGASICITA]([FECHORAIN] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AGASICITA_CODESPECI_TIPSOLICITU_FECHORAFI_CITAEXTRA_CODACTMED_CODESTCIT_CODIGOCON_CODPROSAL_CODUSUASI_FECHORAIN_IPCODPACI]
    ON [dbo].[AGASICITA]([CODCENATE] ASC, [CODESPECI] ASC, [TIPSOLICITU] ASC, [FECHORAFI] ASC)
    INCLUDE([CITAEXTRA], [CODACTMED], [CODESTCIT], [CODIGOCON], [CODPROSAL], [CODUSUASI], [FECHORAIN], [IPCODPACI]);


GO
CREATE NONCLUSTERED INDEX [IX_AGASICITA_CODCENATE_RELCITAINGRE_TIPSOLICITU_FECHCANCELA]
    ON [dbo].[AGASICITA]([CODCENATE] ASC, [RELCITAINGRE] ASC, [TIPSOLICITU] ASC, [FECHCANCELA] ASC)
    INCLUDE([CODACTMED], [CODESTCIT], [CODTIPCIT], [FECHORAIN], [IDEQUIPOTRA], [IDSALA], [IPCODPACI], [MODALIDAD], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_AGASICITA_CODESTCIT_FECHORAIN_FECHORAFI_CODACTMED_IDAGENDA]
    ON [dbo].[AGASICITA]([CODESTCIT] ASC)
    INCLUDE([FECHORAIN], [FECHORAFI], [CODACTMED], [IDAGENDA]);


GO
CREATE NONCLUSTERED INDEX [AGASICITA_CODCENATE_CODPROSAL_CODAUTONU_FECHORAFI_FECHORAIN]
    ON [dbo].[AGASICITA]([CODCENATE] ASC, [CODPROSAL] ASC)
    INCLUDE([CODAUTONU], [FECHORAFI], [FECHORAIN]);


GO
CREATE NONCLUSTERED INDEX [IPCODPACI]
    ON [dbo].[AGASICITA]([IPCODPACI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AGASICITA_CODESPECI_CODCENATE_TIPSOLICITU_FECHORAIN_FECHORAFI]
    ON [dbo].[AGASICITA]([CODESPECI] ASC, [CODCENATE] ASC, [TIPSOLICITU] ASC, [FECHORAIN] ASC, [FECHORAFI] ASC)
    INCLUDE([IPCODPACI], [CODPROSAL], [CODIGOCON], [CODACTMED], [CODESTCIT], [CITAEXTRA], [CODUSUASI]);


GO
CREATE NONCLUSTERED INDEX [IX_AGASICITA_IDHCRADESQUEMAS_FECHORAIN]
    ON [dbo].[AGASICITA]([IDHCRADESQUEMAS] ASC)
    INCLUDE([FECHORAIN]);


GO
CREATE NONCLUSTERED INDEX [CODPROSAL]
    ON [dbo].[AGASICITA]([CODPROSAL] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_AGASICITA_6_96771452__K4_K5_K6]
    ON [dbo].[AGASICITA]([IPCODPACI] ASC, [CODPROSAL] ASC, [FECHORAIN] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AGASICITA_IDHCORDCICLOSD_CODESTCIT_CODMOTIVOREPRO_CONFIRMQUIMIO_FECHORAFI_FECHORAIN_IDEQUIPOTRA]
    ON [dbo].[AGASICITA]([IDHCORDCICLOSD] ASC, [CODESTCIT] ASC)
    INCLUDE([CODMOTIVOREPRO], [CONFIRMQUIMIO], [FECHORAFI], [FECHORAIN], [IDEQUIPOTRA]);


GO
CREATE NONCLUSTERED INDEX [IDX_AGASICITA_CODESPECI_CODCENATE_FECHORAIN_FECHORAFI]
    ON [dbo].[AGASICITA]([CODESPECI] ASC, [CODCENATE] ASC, [FECHORAIN] ASC, [FECHORAFI] ASC)
    INCLUDE([IPCODPACI], [CODPROSAL], [CODACTMED], [CODESTCIT], [CITAEXTRA], [CODUSUASI]);


GO
CREATE NONCLUSTERED INDEX [IX_AGASICITA_CODCENATE_IDSALA_TIPSOLICITU_FECHORAFI]
    ON [dbo].[AGASICITA]([CODCENATE] ASC, [IDSALA] ASC, [TIPSOLICITU] ASC, [FECHORAFI] ASC)
    INCLUDE([CITAEXTRA], [CODACTMED], [CODESPECI], [CODESTCIT], [CODSERIPS], [CODUSUASI], [FECHORAIN], [IDDESCRIPCIONRELACIONADA], [IDRIASCUPS], [IPCODPACI]);


GO

CREATE  TRIGGER [dbo].[tgg_ActualizarCampoCODTIPCIT] 
   ON  [dbo].[AGASICITA] 
   AFTER INSERT, UPDATE
AS 
BEGIN

	SET NOCOUNT ON;

	update AGASICITA set CODTIPCIT = 0 from AGASICITA i inner join inserted t on i.CODAUTONU = t.CODAUTONU where t.idagenda is not null and t.CODTIPCIT is null 

END

--SELECT * FROM AGASICITA WHERE idagenda is not null and CODTIPCIT is null 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[Especialidad_NULL]
   ON [dbo].[AGASICITA]
   AFTER INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    if exists(select 1 from inserted where CODESPECI is null and TIPSOLICITU = 1)begin
		;throw 51000,'Estamos detectando un error en el sistema, la Especialidad no puede ser vacia - por favor contacte al administrador del sistema
					para poder continuar con el proceso.', 1
	end
END

/*
TIPSOLICITU:
	Determina el Tipo de cita:
	1 - Cita Medica
	2 - Cita Apoyo Diagnostico
	3 - Cita Tratamiento Especiales

*/
GO

CREATE TRIGGER [dbo].[Vacio_CodigoActividad] 
   ON  [dbo].[AGASICITA] 
   AFTER INSERT, UPDATE  
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	if  exists(select 1 from inserted where CODACTMED =  '' and TIPSOLICITU in ('1','2') ) begin
		    raiserror ('Estamos detectando un error en el sistema, la ACTIVIDAD no puede ser vacia - por favor contacte al administrador del sistema',10,1)
	end

end
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'UNIQUEIDENTIFIER que identifica citas recurrentes o frecuentes del paciente para seguimiento periódico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IdFrequentlyAppointment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para guardar el identificador de las citas con frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IdFrequentlyAppointment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IdFrequentlyAppointment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(25) de sincronización con sistema Servinte para integración de estadísticas Power BI en cliente San José, control de citas externas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IdCitaSync';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna de control usada para cliente san jose en la integracion con citas de servinte, con el fin de poder tener mas informacion de la cita en los estadisticos de power BI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IdCitaSync';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IdCitaSync';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK a [Authorization].TraceabilityPaperwork; se completa al asignar autorización VIE ERP, permite actualizar cabecera al cancelar cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla cabecera de las autorizaciones de vie erp    Tabla  [Authorization].TraceabilityPaperwork      Este campo se diligencia siempre y cuando se seleccione una Autorizaciones de VIE ERP al asignar la cita medica y por medio de este campo actualizamos la cabecera de VIE cuando se cancele la cita.        ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NVARCHAR(MAX) observaciones asociadas al estado de visado (validación/aprobación) de la cita médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OBSVISADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion del Visado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OBSVISADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OBSVISADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT (1=Sí/2=No) para determinar si la cita seleccionada queda en lista de revisión o visado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TIPOVISADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dejar de lista la cita seleccionada?     1 - Si  2 - No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TIPOVISADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TIPOVISADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK que relaciona la cita con la agenda médica del profesional de salud donde fue asignada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDAGENDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID que relaciona la agenda medica a la que se asigno la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDAGENDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDAGENDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK (auto-referencia AGASICITA) que vincula cita reprogramada con su cita original de consulta externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDCITAPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID que relaciona la cita padre de origen de reprogramacion de cita de consulta externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDCITAPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDCITAPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(10) número de ingreso/admission del paciente con el que se confirma la asistencia a cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'NUMINGRESCONFIRM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero ingreso con el que se confirma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'NUMINGRESCONFIRM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'NUMINGRESCONFIRM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME de confirmación de asistencia de cita de apoyo diagnóstico o consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHACONFIRM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de confirmacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHACONFIRM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHACONFIRM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20) identificador del profesional/usuario que realizó confirmación de cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'USUCONFIRM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que realiza la confirmacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'USUCONFIRM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'USUCONFIRM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT (true=confirmada/false=no confirmada) estado de confirmación de cita de apoyo diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CONFIRMCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la cita de apoyo DX ya esta confirmada.  true - confirmada  false - no confirmada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CONFIRMCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CONFIRMCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT (0=Presencial/1=Teleconsulta) canal de atención de la cita médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'MODALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modalidad, 0:Presencial, 1:Teleconsulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'MODALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'MODALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK a HCRADORDEN; referencia a orden de braquiterapia para citas de tratamiento especial radiante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID table de ordenes - tipo de braquiterapia (HCRADORDEN)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20) identificador usuario que confirma asistencia a cita de quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'USUCONFIRMQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de confirmacion de cita de Quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'USUCONFIRMQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'USUCONFIRMQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME cuando se ejecutó la reprogramación de cita médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHAREPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se hizo la repogramación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHAREPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHAREPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(200) observaciones de cambios realizados: equipos, fechas previas en reprogramación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'COMENTARIOREPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion de repogramación, se guarda equipo, y fechas en que estaba la cita ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'COMENTARIOREPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'COMENTARIOREPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(500) justificación clínica o administrativa de la reprogramación de cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'JUTIFICACIONREPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación de repogramación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'JUTIFICACIONREPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'JUTIFICACIONREPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(4) FK a HCMOANULB; código motivo/concepto por el que se reprogramó cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODMOTIVOREPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo concepto con el que  repogramo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODMOTIVOREPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODMOTIVOREPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20) identificador del usuario que ejecutó la reprogramación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODUSUARIOREPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario que  repogramo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODUSUARIOREPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODUSUARIOREPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT confirmación de asistencia a cita de quimioterapia (tratamiento especial)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CONFIRMQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si se confirmo o no la cita de quimioterapia ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CONFIRMQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CONFIRMQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK a EHR.HCORDCICLOSD; ciclo de quimioterapia asociado a la cita de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID tabla de ciclos de quimioterpaia (EHR.HCORDCICLOSD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOSD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK a contract.CUPSEntityContractDescriptions; descripción CUPS-contrato de VIE ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK a HCRADESQUEMAS; esquema/protocolo de radioterapia para cita de tratamiento especial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDHCRADESQUEMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID table de esquemas de radioterapia (HCRADESQUEMAS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDHCRADESQUEMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDHCRADESQUEMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(500) justificación clínica o motivo de anulación de cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'DESMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'DESMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'DESMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(4) código tipo de servicio RIPS; 13=permite registrar mismo número de autorización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'13: Permitir Registrar mismo Nùmero de Autorizaciòn ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(30) número de autorización/aprobación de aseguradora para cita de consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'NUMAUTORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Numero de Autorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'NUMAUTORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'NUMAUTORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT identificador de Entidad Administradora (EAPB) sin contrato seleccionada en grupo de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la Entidad Administradora del contrato, este campo solo se llena si el Grupo de atencion que seleccione es de EAPB Sin Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT ID del grupo de atención de base datos GENESIS para organización de citas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del grupo de atencion de la base de datos de GENESIS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(1) (1=Confirmado/2=Cancelado/3=Sin Definir) estado de confirmación de asistencia del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CONFASIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de Confirmacion de Asistencia --> 1= Confirmado , 2 =Cancelado , 3= Sin Definir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CONFASIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CONFASIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME cuando se registraron causas de inasistencia/no atención del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHAINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha cuando registro Causas Inatencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHAINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHAINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20) identificador usuario que registró causas de inasistencia/no atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODUSUINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario quien registro Causas Inatencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODUSUINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODUSUINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(200) observaciones/detalles sobre causas de inasistencia del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OBSCAUINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones del Causas Inatencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OBSCAUINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OBSCAUINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(4) código clasificación de causa de inasistencia o no atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODCAUINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Causas Inatencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODCAUINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODCAUINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME fecha/hora que se ofrece cita al paciente (disponibilidad inicial ofrecida)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHAOFERTADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha ofertada al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHAOFERTADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHAOFERTADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT vinculación ID RIASCUPS agenda médica; NULL si actividad no aplica o no tiene RIAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo en la cual me relaciona el ID de la RIASCUPS que tiene la agenda medica, si la actividad de agendamiento No tiene ó no aplica a RIAS pues guarda Dbnull.Value.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME fecha/hora de proceso o procedimiento relacionado a cita de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECPROCT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'se guarda la fecha de FECPROCT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECPROCT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECPROCT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT número de ciclo de quimioterapia; aplica citas tratamiento especial (no cancer hematolinfático)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciclo . Aplica para Citas Tratamiento Especial - Tipo Quimioterapia -  Diagnostico diferente a Cancer Hematolinfático', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CICLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(50) fase adicional de quimioterapia; aplica diagnóstico cancer hematolinfático', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OTRAFASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otra Fase . Aplica para Citas Tratamiento Especial - Tipo Quimioterapia -  Diagnostico  Cancer Hematolinfático', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OTRAFASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OTRAFASE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT (1=Prefase citoreducción/2=Inducción/3=Intensificación/4=Consolidación/5=Reinducción/6=Mantenimiento/7=Mantenimiento largo/8=Otra) fase quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fase . Aplica para Citas Tratamiento Especial - Tipo Quimioterapia -  Diagnostico  Cancer Hematolinfatico  1 - Prefase o citoreducción inicial  2 -Inducción  3-Intensificación  4-Consolidación  5-Reinducción  6-Mantenimiento  7-Mantenimiento largo o final  8-Otra fase de quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FASE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT ID turno de sala de diálisis o tratamiento especial asignado a cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDTURNOSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del turno de la sala, cuando se guarda una cita diálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDTURNOSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDTURNOSALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT (1=Quimioterapia/2=Radioterapia/3=Diálisis/4=Braquiterapia) tipo de tratamiento especial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TIPTRATAMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Tratamiento Especial:     1 - Quimioterapia  2 - RadioTerapia  3 - Diálisis  4 - Braquiterapia    Este campo se registra solo para citas de tratamientos especiales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TIPTRATAMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TIPTRATAMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(4) MASKED DiagnosticCode_Ofuscado; código diagnóstico CIE-10 PII de cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(10) número ingreso/hospitalización del paciente en cita (cuando está internado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número del ingreso relacionado (Cuando el paciente está hospitalizado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT (1=Sí/2=No) indicador si cita está vinculada a ingreso hospitalario activo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'RELCITAINGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacionar Cita con Ingreso (Cuando el paciente está hospitalizado)   1:Si  2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'RELCITAINGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'RELCITAINGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20) código servicio IPS RIPS; se completa para citas apoyo diagnóstico y tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del servicio IPS. se guarda para citas de apoyo DX  y Tratamiento Especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT indicador si cita generó orden de servicio desde control consulta externa GENESIS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'GENGENERATESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo especifica si el registro genero orden de servicio En Indigo Vie desde control de consulta externa, esto con el fin de que la misma cita no quede en dos Ordenes de Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'GENGENERATESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'GENGENERATESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT relación con equipos de tratamiento especial (radioterapia, braquiterapia, diálisis)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDEQUIPOTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion Con Equipos De Tratamiento. para citas de tipo Tratamiento Especiales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDEQUIPOTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDEQUIPOTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(200) comentarios/observaciones de la causa de cancelación de cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OBSCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario de cancelacion de cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OBSCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OBSCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(3) código causa/motivo de cancelación de cita médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de causa de cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME fecha/hora cuando se canceló cita médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHCANCELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la cancelacion de la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHCANCELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHCANCELA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20) identificador usuario que realizó cancelación de cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CANCELUSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Usuario de cancelacion de la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CANCELUSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CANCELUSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT relación con sala clínica para citas apoyo diagnóstico o tratamientos especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion Con la Salas. para citas de tipo de Apoyo DX o Tratamiento Especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IDSALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT (1=Cita Médica/2=Apoyo Diagnóstico/3=Tratamiento Especial) tipo de solicitud/cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TIPSOLICITU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina el Tipo de cita:  
1 - Cita Medica 
2 - Cita Apoyo Diagnostico 
3 - Cita Tratamiento Especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TIPSOLICITU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TIPSOLICITU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME fecha deseada/preferida por paciente para realizar cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECITADES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Deseada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECITADES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECITADES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME fecha/hora cita asignada luego de cambiar estado a preasignada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECINICIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora de la Cita Asignada Luego de Ser Modificada al estado preasignada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECINICIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECINICIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NVARCHAR(500) observaciones/notas de cita en estado preasignado (antes de confirmar)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OBSCITPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion de la Cita en Estado Preasignada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OBSCITPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OBSCITPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME fecha/hora registro de cita en sistema (auditoría de creación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Fecha y hora en que se registro la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20) identificador profesional/usuario que asignó/programó cita médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODUSUASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que Asigno la Cita Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODUSUASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODUSUASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT indicador envío notificación SMS/mensaje de texto al paciente sobre cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'ESTENVSMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Notificar con mensaje de texto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'ESTENVSMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'ESTENVSMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NVARCHAR(500) notas/observaciones generales de cita médica para el profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para las Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT indicador si cita es programada fuera del horario habitual (extra)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CITAEXTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Es Cita Extra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CITAEXTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CITAEXTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(1) (0=Asignada/1=Cumplida/2=Incumplida/3=Preasignada/4=Cancelada/5=Inasistencia) estado cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODESTCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Estado de la Cita  0. Asignada  1. Cumplida  2. Incumplida  3. PreAsignada  4. Cita Cancelada  5. Inatencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODESTCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODESTCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(1) (0=Primera Vez/1=Control/2=Postoperatorio/3=Cita Web) tipo/categoría cita médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODTIPCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Tipo de Cita Médica. 
   
0. Primera Vez  
1. Control  
2. Pos Operatorio   
3. Cita Web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODTIPCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODTIPCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT (0=Presencial/1=Telefónica/2=Email/3=Mensajería/4=Web/5=Requerimiento externo) canal solicitud cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODTIPSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Solicitud.    0. Presencial  1. Telefónica    --> A partir del desarrollo del PBI 23624 cambia la funcionalidad de esta columna. 
Canal de solicitud:
0 - Solicitud presencial
1 - Llamada telefónica
2 - Correo electrónico
3 - Mensajería instantánea
4 - Solicitud web
5 - Requerimiento externo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODTIPSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODTIPSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(3) código actividad/procedimiento médico a realizar en cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Actividad Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(6) código consultorio/sala clínica donde se asignó la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODIGOCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consultorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODIGOCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODIGOCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME hora final programada de cita médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHORAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora final de la Cita Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHORAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHORAFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME hora inicial programada de cita médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHORAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora inicial de la Cita Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHORAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECHORAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(25) MASKED Identification_Ofuscado; código/identificación profesional de salud PII', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(25) MASKED Identification_Ofuscado; código/identificación paciente (cédula, pasaporte) PII', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(10) código centro/institución de atención donde se programó cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(3) código especialidad médica de la cita (cardiología, pediatría, etc)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT IDENTITY PRIMARY KEY; identificador único autoincrementable de cita médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODAUTONU';


GO
CREATE NONCLUSTERED INDEX [IX_AGASICITA_IdHCORDPRON_FechOrain]
    ON [dbo].[AGASICITA]([IdHCORDPRON] ASC, [FECHORAIN] ASC, [CODAUTONU] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AGASICITA_Equipra_Fechas]
    ON [dbo].[AGASICITA]([IDEQUIPOTRA] ASC, [FECHORAIN] ASC, [FECHORAFI] ASC)
    INCLUDE([IPCODPACI], [TIPSOLICITU]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT (1=Derecha/2=Izquierda/3=Bilateral/4=Multilateral/NULL=No aplica) lateralidad anatómica para procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'Laterality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la lateralidad:

1 - Derecha 
2 - Izquierda 
3 - Bilateral 
4 - Multilateral 

Otros - No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'Laterality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'Laterality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK a HCORDPRON; orden de pronóstico para citas tratamiento especial terapia reemplazo renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IdHCORDPRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Auto de la tabla HCORDPRON para citas de tratamientos especiales de tipo terapia de remplazo renal ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IdHCORDPRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IdHCORDPRON';


GO
CREATE NONCLUSTERED INDEX [IX_AGASICITA_CITA_PAGINACION]
    ON [dbo].[AGASICITA]([CODCENATE] ASC, [FECHORAIN] ASC, [FECREGSIS] ASC)
    INCLUDE([CODAUTONU]);


GO
CREATE NONCLUSTERED INDEX [IX_AGASICITA_CENATE_FECREGSIS]
    ON [dbo].[AGASICITA]([CODCENATE] ASC, [FECREGSIS] ASC)
    INCLUDE([CODAUTONU]);


GO
CREATE NONCLUSTERED INDEX [IX_AGASICITA_CENATE_FECHORAIN]
    ON [dbo].[AGASICITA]([CODCENATE] ASC, [FECHORAIN] ASC)
    INCLUDE([CODAUTONU]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de citas y agendamiento médico de pacientes. Guarda la información de cada cita asignada, incluyendo el paciente, profesional de salud, fechas, estado, tipo de cita, servicio, diagnóstico, cancelaciones, confirmaciones y datos de tratamientos como quimioterapia u oncología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de sincronización con el sistema Indigo (GUID), usado para trazabilidad e integración entre plataformas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IndigoSyncId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IndigoSyncId';
