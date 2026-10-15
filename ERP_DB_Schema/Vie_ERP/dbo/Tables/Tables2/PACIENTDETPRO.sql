CREATE TABLE [dbo].[PACIENTDETPRO] (
    [ID]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDINPACIENTPYP]   INT           NOT NULL,
    [IDPROGRAMA]       INT           NOT NULL,
    [FECHAINSCR]       DATE          NULL,
    [FECHULTIMATEN]    DATE          NULL,
    [JUSTIINSCRIPCION] VARCHAR (MAX) NULL,
    [ESTADO]           BIT           CONSTRAINT [DF_PACIENTDETPRO_ESTADO] DEFAULT ((1)) NULL,
    [JUSTIRETIRO]      VARCHAR (MAX) NULL,
    [CITAREPROGRA]     INT           NULL,
    [JUSTICITAREPRO]   VARCHAR (MAX) NULL,
    CONSTRAINT [PK_PACIENTDETPRO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PACIENTDETPRO_HCPROGRAPYP] FOREIGN KEY ([IDPROGRAMA]) REFERENCES [dbo].[RIAS] ([ID]),
    CONSTRAINT [FK_PACIENTDETPRO_INPACIENTPYP] FOREIGN KEY ([IDINPACIENTPYP]) REFERENCES [dbo].[INPACIENTPYP] ([ID])
);


GO
ALTER TABLE [dbo].[PACIENTDETPRO] NOCHECK CONSTRAINT [FK_PACIENTDETPRO_HCPROGRAPYP];


GO
ALTER TABLE [dbo].[PACIENTDETPRO] NOCHECK CONSTRAINT [FK_PACIENTDETPRO_INPACIENTPYP];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación cita reprogramada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'JUSTICITAREPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cita reprogramada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'CITAREPROGRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación del retiro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'JUSTIRETIRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1-Activo en el programa 0-No activo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación de la inscripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'JUSTIINSCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha última entrega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'FECHULTIMATEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inscripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'FECHAINSCR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla RIAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'IDPROGRAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla INPACIENTPYP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'IDINPACIENTPYP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PACIENTDETPRO', @level2type = N'COLUMN', @level2name = N'ID';

