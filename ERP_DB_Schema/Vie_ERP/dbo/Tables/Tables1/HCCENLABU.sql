CREATE TABLE [dbo].[HCCENLABU] (
    [CODCENLAB] CHAR (80)     NULL,
    [DESCENLAB] VARCHAR (200) NULL,
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGOIPS] CHAR (100)    NULL,
    CONSTRAINT [PK_HCCENLABU] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCENLABU_ADCONTIPS] FOREIGN KEY ([CODIGOIPS]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS])
);


GO
ALTER TABLE [dbo].[HCCENLABU] NOCHECK CONSTRAINT [FK_HCCENLABU_ADCONTIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del ips relacionado con tabla ADCONTIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de Remision de Laboratorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU', @level2type = N'COLUMN', @level2name = N'DESCENLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Registro Centros de remision ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU', @level2type = N'COLUMN', @level2name = N'CODCENLAB';

