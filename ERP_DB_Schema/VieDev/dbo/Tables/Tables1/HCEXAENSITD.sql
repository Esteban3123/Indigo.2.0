CREATE TABLE [dbo].[HCEXAENSITD] (
    [ID]            INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCEXAENSITCID] INT       NOT NULL,
    [CODSERIPS]     CHAR (20) NOT NULL,
    [TOMEXAMEN]     TINYINT   NOT NULL,
    CONSTRAINT [PK_HCEXAENSITD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCEXAENSITD_HCEXAENSITC] FOREIGN KEY ([HCEXAENSITCID]) REFERENCES [dbo].[HCEXAENSITC] ([ID]),
    CONSTRAINT [FK_HCEXAENSITD_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[HCEXAENSITD] NOCHECK CONSTRAINT [FK_HCEXAENSITD_HCEXAENSITC];


GO
ALTER TABLE [dbo].[HCEXAENSITD] NOCHECK CONSTRAINT [FK_HCEXAENSITD_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define como toma muestra de examen   1-> En sitio  2-> Pregunta al usuario al momento de hacer la muestra  3-> No hace interfaz ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITD', @level2type = N'COLUMN', @level2name = N'TOMEXAMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del servicio IPS  Listar solo los laboratorios es decir donde TIPSERIPS = 1 de la tabla INCUPSIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';

