CREATE TABLE [dbo].[HCENTREGATURNO] (
    [ID]            INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]     CHAR (10)    NOT NULL,
    [UFUCODIGO]     CHAR (10)    NOT NULL,
    [JORNADA]       VARCHAR (25) NULL,
    [DASHBOARD]     VARCHAR (50) NULL,
    [FECHAREGISTRO] DATETIME     NOT NULL,
    CONSTRAINT [PK_HCENTREGATURNO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCENTREGATURNO_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCENTREGATURNO_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCENTREGATURNO] NOCHECK CONSTRAINT [FK_HCENTREGATURNO_ADCENATEN];


GO
ALTER TABLE [dbo].[HCENTREGATURNO] NOCHECK CONSTRAINT [FK_HCENTREGATURNO_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de registro de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNO', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el dashboard', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNO', @level2type = N'COLUMN', @level2name = N'DASHBOARD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la jornada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNO', @level2type = N'COLUMN', @level2name = N'JORNADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'gurada el codigo Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNO', @level2type = N'COLUMN', @level2name = N'ID';

