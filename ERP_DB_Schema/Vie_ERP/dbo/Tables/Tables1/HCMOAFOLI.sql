CREATE TABLE [dbo].[HCMOAFOLI] (
    [CODMOANFO] CHAR (3)      NOT NULL,
    [DESMOANFO] VARCHAR (200) NOT NULL,
    [ESTMOANFO] BIT           NOT NULL,
    CONSTRAINT [PK_HCMOAFOLI] PRIMARY KEY CLUSTERED ([CODMOANFO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Motivo Anulacion de Folios ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOAFOLI', @level2type = N'COLUMN', @level2name = N'ESTMOANFO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Motivo Anulacion de Folios ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOAFOLI', @level2type = N'COLUMN', @level2name = N'DESMOANFO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Motivo Anulacion de Folios ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOAFOLI', @level2type = N'COLUMN', @level2name = N'CODMOANFO';

