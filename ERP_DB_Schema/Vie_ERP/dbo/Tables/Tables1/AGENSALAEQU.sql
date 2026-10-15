CREATE TABLE [dbo].[AGENSALAEQU] (
    [ID]           INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC]    INT NOT NULL,
    [IDAGEQUIPTRA] INT NOT NULL,
    CONSTRAINT [PK_AGENSALAEQU] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGENSALAEQU_AGENSALAC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[AGENSALAC] ([CODCONCEC]),
    CONSTRAINT [FK_AGENSALAEQU_AGEQUIPTRA] FOREIGN KEY ([IDAGEQUIPTRA]) REFERENCES [dbo].[AGEQUIPTRA] ([ID])
);


GO
ALTER TABLE [dbo].[AGENSALAEQU] NOCHECK CONSTRAINT [FK_AGENSALAEQU_AGENSALAC];


GO
ALTER TABLE [dbo].[AGENSALAEQU] NOCHECK CONSTRAINT [FK_AGENSALAEQU_AGEQUIPTRA];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_AGENSALAEQU]
    ON [dbo].[AGENSALAEQU]([CODCONCEC] ASC, [IDAGEQUIPTRA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla de equipos de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAEQU', @level2type = N'COLUMN', @level2name = N'IDAGEQUIPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Consecutivo de la Tabla de salas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAEQU', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAEQU', @level2type = N'COLUMN', @level2name = N'ID';

