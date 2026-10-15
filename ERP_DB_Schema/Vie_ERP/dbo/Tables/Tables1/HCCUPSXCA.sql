CREATE TABLE [dbo].[HCCUPSXCA] (
    [ID]            INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]     CHAR (10) NOT NULL,
    [CODSERIPS]     CHAR (20) NOT NULL,
    [FECHACREACION] DATETIME  NOT NULL,
    [USUCREACION]   CHAR (20) NOT NULL,
    CONSTRAINT [PK_HCCUPSXCA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCUPSXCA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCUPSXCA_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[HCCUPSXCA] NOCHECK CONSTRAINT [FK_HCCUPSXCA_ADCENATEN];


GO
ALTER TABLE [dbo].[HCCUPSXCA] NOCHECK CONSTRAINT [FK_HCCUPSXCA_INCUPSIPS];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCCUPSXCA]
    ON [dbo].[HCCUPSXCA]([CODCENATE] ASC, [CODSERIPS] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUPSXCA', @level2type = N'COLUMN', @level2name = N'USUCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUPSXCA', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUPSXCA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo centro Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUPSXCA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumercio de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUPSXCA', @level2type = N'COLUMN', @level2name = N'ID';

