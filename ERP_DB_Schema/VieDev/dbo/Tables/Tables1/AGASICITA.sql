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
    CONSTRAINT [PK_AGASICITA] PRIMARY KEY CLUSTERED ([CODAUTONU] ASC),
    CONSTRAINT [FK_AGASICITA_Cita] FOREIGN KEY ([IDCITAPADRE]) REFERENCES [dbo].[AGASICITA] ([CODAUTONU]),
    CONSTRAINT [FK_AGASICITA_HCMOANULB] FOREIGN KEY ([CODMOTIVOREPRO]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_AGASICITA_HCQUICICLOS] FOREIGN KEY ([IDHCORDCICLOSD]) REFERENCES [EHR].[HCORDCICLOSD] ([ID]),
    CONSTRAINT [FK_AGASICITA_HCRADESQUEMAS] FOREIGN KEY ([IDHCRADESQUEMAS]) REFERENCES [dbo].[HCRADESQUEMAS] ([ID]),
    CONSTRAINT [FK_AGASICITA_HCRADORDEN] FOREIGN KEY ([IDHCRADORDEN]) REFERENCES [dbo].[HCRADORDEN] ([ID]),
    CONSTRAINT [FK_AGASICITA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[AGASICITA] NOCHECK CONSTRAINT [FK_AGASICITA_Cita];


GO
ALTER TABLE [dbo].[AGASICITA] NOCHECK CONSTRAINT [FK_AGASICITA_HCMOANULB];


GO
ALTER TABLE [dbo].[AGASICITA] NOCHECK CONSTRAINT [FK_AGASICITA_HCQUICICLOS];


GO
ALTER TABLE [dbo].[AGASICITA] NOCHECK CONSTRAINT [FK_AGASICITA_HCRADESQUEMAS];


GO
ALTER TABLE [dbo].[AGASICITA] NOCHECK CONSTRAINT [FK_AGASICITA_HCRADORDEN];


GO
ALTER TABLE [dbo].[AGASICITA] NOCHECK CONSTRAINT [FK_AGASICITA_INPACIENT];


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
CREATE NONCLUSTERED INDEX [IX_AGASICITA_CODESPECI_CODCENATE_TIPSOLICITU_FECHORAIN_FECHORAFI]
    ON [dbo].[AGASICITA]([CODESPECI] ASC, [CODCENATE] ASC, [TIPSOLICITU] ASC, [FECHORAIN] ASC, [FECHORAFI] ASC)
    INCLUDE([IPCODPACI], [CODPROSAL], [CODIGOCON], [CODACTMED], [CODESTCIT], [CITAEXTRA], [CODUSUASI]);


GO
CREATE NONCLUSTERED INDEX [IX_AGASICITA_IDHCRADESQUEMAS_FECHORAIN]
    ON [dbo].[AGASICITA]([IDHCRADESQUEMAS] ASC)
    INCLUDE([FECHORAIN]);


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo para guardar el identificador de las citas con frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IdFrequentlyAppointment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Columna de control usada para cliente san jose en la integracion con citas de servinte, con el fin de poder tener mas informacion de la cita en los estadisticos de power BI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'IdCitaSync';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla cabecera de las autorizaciones de vie erp    Tabla  [Authorization].TraceabilityPaperwork      Este campo se diligencia siempre y cuando se seleccione una Autorizaciones de VIE ERP al asignar la cita medica y por medio de este campo actualizamos la cabecera de VIE cuando se cancele la cita.        ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dejar de lista la cita seleccionada?     1 - Si  2 - No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TIPOVISADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si la cita de apoyo DX ya esta confirmada.  true - confirmada  false - no confirmada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CONFIRMCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda la fecha de FECPROCT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FECPROCT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fase . Aplica para Citas Tratamiento Especial - Tipo Quimioterapia -  Diagnostico  Cancer Hematolinfatico  1 - Prefase o citoreducción inicial  2 -Inducción  3-Intensificación  4-Consolidación  5-Reinducción  6-Mantenimiento  7-Mantenimiento largo o final  8-Otra fase de quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'FASE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Tratamiento Especial:     1 - Quimioterapia  2 - RadioTerapia  3 - Diálisis  4 - Braquiterapia    Este campo se registra solo para citas de tratamientos especiales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TIPTRATAMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacionar Cita con Ingreso (Cuando el paciente está hospitalizado)   1:Si  2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'RELCITAINGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina el Tipo de cita:  
1 - Cita Medica 
2 - Cita Apoyo Diagnostico 
3 - Cita Tratamiento Especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'TIPSOLICITU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Estado de la Cita  0. Asignada  1. Cumplida  2. Incumplida  3. PreAsignada  4. Cita Cancelada  5. Inatencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODESTCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Tipo de Cita Médica. 
   
0. Primera Vez  
1. Control  
2. Pos Operatorio   
3. Cita Web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODTIPCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del Tipo de Solicitud.    0. Presencial  1. Telefónica    --> A partir del desarrollo del PBI 23624 cambia la funcionalidad de esta columna. 
Canal de solicitud:
0 - Solicitud presencial
1 - Llamada telefónica
2 - Correo electrónico
3 - Mensajería instantánea
4 - Solicitud web
5 - Requerimiento externo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITA', @level2type = N'COLUMN', @level2name = N'CODTIPSOL';

