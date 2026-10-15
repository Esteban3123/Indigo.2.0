CREATE TABLE [dbo].[ADRES4505CENATEN] (
    [ID]           INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDADRES4505C] INT       NOT NULL,
    [CODCENATE]    CHAR (10) NOT NULL,
    CONSTRAINT [PK_ADRES4505CENATEN] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADRES4505CENATEN_ADRES4505C] FOREIGN KEY ([IDADRES4505C]) REFERENCES [dbo].[ADRES4505C] ([ID]),
    CONSTRAINT [FK_ADRES4505CENATEN_ADRES4505CENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);


GO
ALTER TABLE [dbo].[ADRES4505CENATEN] NOCHECK CONSTRAINT [FK_ADRES4505CENATEN_ADRES4505C];


GO
ALTER TABLE [dbo].[ADRES4505CENATEN] NOCHECK CONSTRAINT [FK_ADRES4505CENATEN_ADRES4505CENATEN];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Centro Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505CENATEN', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID que guarda en la tabla ADRES4505C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505CENATEN', @level2type = N'COLUMN', @level2name = N'IDADRES4505C';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505CENATEN', @level2type = N'COLUMN', @level2name = N'ID';

