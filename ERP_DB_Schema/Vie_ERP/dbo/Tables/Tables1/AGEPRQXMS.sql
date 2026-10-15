CREATE TABLE [dbo].[AGEPRQXMS] (
    [AUTONUMER] INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODPROQXM] INT       NOT NULL,
    [CODSERIPS] CHAR (20) NOT NULL,
    CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED ([AUTONUMER] ASC),
    CONSTRAINT [FK_AGEPRQXMS_AGEPROQXM] FOREIGN KEY ([CODPROQXM]) REFERENCES [dbo].[AGEPROQXM] ([AUTONUMER]),
    CONSTRAINT [FK_AGEPRQXMS_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[AGEPRQXMS] NOCHECK CONSTRAINT [FK_AGEPRQXMS_AGEPROQXM];


GO
ALTER TABLE [dbo].[AGEPRQXMS] NOCHECK CONSTRAINT [FK_AGEPRQXMS_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPRQXMS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CODIGO CIRUGIA PROGRAMADA MULTIPLE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPRQXMS', @level2type = N'COLUMN', @level2name = N'CODPROQXM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPRQXMS', @level2type = N'COLUMN', @level2name = N'AUTONUMER';

