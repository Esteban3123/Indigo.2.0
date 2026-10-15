CREATE TABLE [dbo].[HCTRASLADOS] (
    [ID]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCSOLTRASLADOSINT]      INT           NULL,
    [IDHCREFCONP]              INT           NULL,
    [TIPOTRASLADO]             INT           NOT NULL,
    [ESTADO]                   INT           NOT NULL,
    [USUARIOTRASLADO]          CHAR (20)     NOT NULL,
    [UFUCODIGOORTRASLADO]      CHAR (10)     NOT NULL,
    [CODCENATEDETRASLADO]      CHAR (10)     NOT NULL,
    [UFUCODIGODETRASLADO]      CHAR (10)     NOT NULL,
    [FECHATRASLADO]            DATETIME      NOT NULL,
    [OBSERVACIONTRASLADO]      VARCHAR (MAX) NULL,
    [IDRCVEHICTRAS]            INT           NOT NULL,
    [FECHAANULA]               DATETIME      NULL,
    [USUARIOANULA]             CHAR (20)     NULL,
    [IDHCMOANULA]              CHAR (4)      NULL,
    [JUSTIFICAANULA]           VARCHAR (MAX) NULL,
    [IDRCVEHICTRASINICIO]      INT           NULL,
    [TIPOAMBULANCIA]           INT           NULL,
    [OTROTRASPORTEASISTENCIAL] VARCHAR (100) NULL,
    [MEDIOTRASPORTE]           INT           NULL,
    [IDRCEMPTRANS]             INT           NULL,
    [OTRAEMPRESATRASPORTADORA] VARCHAR (100) NULL,
    [NOMBREQUIENRECIBE]        VARCHAR (100) NULL,
    [CARGO]                    INT           NULL,
    [OTROCARGO]                VARCHAR (100) NULL,
    [FECHAINICIOTRASLADO]      DATETIME      NULL,
    CONSTRAINT [PK_HCTRASLADOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCTRASLADOS_ADCENATEN] FOREIGN KEY ([CODCENATEDETRASLADO]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCTRASLADOS_HCMOANULB] FOREIGN KEY ([IDHCMOANULA]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCTRASLADOS_HCSOLTRASLADOSINT] FOREIGN KEY ([IDHCSOLTRASLADOSINT]) REFERENCES [dbo].[HCSOLTRASLADOSINT] ([ID]),
    CONSTRAINT [FK_HCTRASLADOS_HCTRASLADOS] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCTRASLADOS] ([ID]),
    CONSTRAINT [FK_HCTRASLADOS_HCTRASLADOS1] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCTRASLADOS] ([ID]),
    CONSTRAINT [FK_HCTRASLADOS_INUNIFUNC] FOREIGN KEY ([UFUCODIGODETRASLADO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCTRASLADOS_INUNIFUNC1] FOREIGN KEY ([UFUCODIGOORTRASLADO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCTRASLADOS_RCEMPTRANS] FOREIGN KEY ([IDRCEMPTRANS]) REFERENCES [dbo].[RCEMPTRANS] ([ID]),
    CONSTRAINT [FK_HCTRASLADOS_RCVEHICTRAS] FOREIGN KEY ([IDRCVEHICTRAS]) REFERENCES [dbo].[RCVEHICTRAS] ([Id])
);


GO
ALTER TABLE [dbo].[HCTRASLADOS] NOCHECK CONSTRAINT [FK_HCTRASLADOS_ADCENATEN];


GO
ALTER TABLE [dbo].[HCTRASLADOS] NOCHECK CONSTRAINT [FK_HCTRASLADOS_HCMOANULB];


GO
ALTER TABLE [dbo].[HCTRASLADOS] NOCHECK CONSTRAINT [FK_HCTRASLADOS_HCSOLTRASLADOSINT];


GO
ALTER TABLE [dbo].[HCTRASLADOS] NOCHECK CONSTRAINT [FK_HCTRASLADOS_HCTRASLADOS];


GO
ALTER TABLE [dbo].[HCTRASLADOS] NOCHECK CONSTRAINT [FK_HCTRASLADOS_HCTRASLADOS1];


GO
ALTER TABLE [dbo].[HCTRASLADOS] NOCHECK CONSTRAINT [FK_HCTRASLADOS_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCTRASLADOS] NOCHECK CONSTRAINT [FK_HCTRASLADOS_INUNIFUNC1];


GO
ALTER TABLE [dbo].[HCTRASLADOS] NOCHECK CONSTRAINT [FK_HCTRASLADOS_RCEMPTRANS];


GO
ALTER TABLE [dbo].[HCTRASLADOS] NOCHECK CONSTRAINT [FK_HCTRASLADOS_RCVEHICTRAS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicio de traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'FECHAINICIOTRASLADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otro cargo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'OTROCARGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cargo de la persona que recibe   1 -  Auxiliar  2 - Enfermero(a)  3 - Médico general  4 - Médico especialista  5 - Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'CARGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la persona quien recibe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'NOMBREQUIENRECIBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otras empresas trasportadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'OTRAEMPRESATRASPORTADORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id empresas transportadoras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDRCEMPTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Terrestre  2 - Maritimo y/o fluvial  3 - Aéreo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'MEDIOTRASPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda otros trasporte Asistencial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'OTROTRASPORTEASISTENCIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Básico  2 - Medicalizado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'TIPOAMBULANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id vehiculo trasporte  inicio  Ambulancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDRCVEHICTRASINICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion del motivo de anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'JUSTIFICAANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Motivo de anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDHCMOANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que Anula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'USUARIOANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'FECHAANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla de Vehiculos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDRCVEHICTRAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion del traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'OBSERVACIONTRASLADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'FECHATRASLADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad Funcional destino cuando se realizo el traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'UFUCODIGODETRASLADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro Atencón destino cuando se realizo el traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'CODCENATEDETRASLADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad Funcional orgien cuando se realizo el traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'UFUCODIGOORTRASLADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que realizo el traslado ó asignación de la ambuancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'USUARIOTRASLADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - En Proceso  2 - Anulada ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Basico  2 - redondo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'TIPOTRASLADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guiarda el Id de refrencia contra referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDHCREFCONP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Solicitud de traslado interno ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDHCSOLTRASLADOSINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'ID';

