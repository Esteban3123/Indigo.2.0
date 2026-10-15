CREATE TABLE [dbo].[AGENSALAACT] (
    [ID]        INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC] INT      NOT NULL,
    [CODACTMED] CHAR (3) NOT NULL,
    CONSTRAINT [PK_AGENSALAACT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGENSALAACT_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED]),
    CONSTRAINT [FK_AGENSALAACT_AGENSALAC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[AGENSALAC] ([CODCONCEC])
);


GO
ALTER TABLE [dbo].[AGENSALAACT] NOCHECK CONSTRAINT [FK_AGENSALAACT_AGACTIMED];


GO
ALTER TABLE [dbo].[AGENSALAACT] NOCHECK CONSTRAINT [FK_AGENSALAACT_AGENSALAC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo relacion Actividades Medidas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAACT', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Relacion con tabla de salas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAACT', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAACT', @level2type = N'COLUMN', @level2name = N'ID';

