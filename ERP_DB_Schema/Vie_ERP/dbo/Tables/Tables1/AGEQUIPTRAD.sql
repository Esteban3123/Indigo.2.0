CREATE TABLE [dbo].[AGEQUIPTRAD] (
    [ID]           INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDAGEQUIPTRA] INT      NOT NULL,
    [CODACTMED]    CHAR (3) NULL,
    CONSTRAINT [PK_AGEQUIPTRAD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGEQUIPTRAD_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED]),
    CONSTRAINT [FK_AGEQUIPTRAD_AGEQUIPTRA] FOREIGN KEY ([IDAGEQUIPTRA]) REFERENCES [dbo].[AGEQUIPTRA] ([ID])
);


GO
ALTER TABLE [dbo].[AGEQUIPTRAD] NOCHECK CONSTRAINT [FK_AGEQUIPTRAD_AGACTIMED];


GO
ALTER TABLE [dbo].[AGEQUIPTRAD] NOCHECK CONSTRAINT [FK_AGEQUIPTRAD_AGEQUIPTRA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'código de la actividad médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRAD', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID relacion del equipo de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRAD', @level2type = N'COLUMN', @level2name = N'IDAGEQUIPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de detalle de equipo x servicios IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRAD', @level2type = N'COLUMN', @level2name = N'ID';

