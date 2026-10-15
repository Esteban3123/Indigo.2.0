CREATE TABLE [dbo].[HCMOANTRA] (
    [CODANUTRA] INT          NOT NULL,
    [DESANUTRA] VARCHAR (80) NOT NULL,
    [ESTANUTRA] BIT          NOT NULL,
    CONSTRAINT [PK_HCMOANTRA] PRIMARY KEY CLUSTERED ([CODANUTRA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANTRA', @level2type = N'COLUMN', @level2name = N'ESTANUTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del motivo de anulacion de la transcripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANTRA', @level2type = N'COLUMN', @level2name = N'DESANUTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Motivo de Anulacion de La Transcripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANTRA', @level2type = N'COLUMN', @level2name = N'CODANUTRA';

