CREATE TABLE [dbo].[HCPLADIAG] (
    [CODDIAGNO] CHAR (4)    NOT NULL,
    [CODCONSEC] VARCHAR (5) NOT NULL,
    [PLANTPRIN] BIT         NULL,
    [TipoFicha] INT         NULL,
    CONSTRAINT [PK_HCPLADIAG] PRIMARY KEY CLUSTERED ([CODDIAGNO] ASC, [CODCONSEC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0 - Fichas notificacion SIVIGILA  1 - Fichas distritales  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG', @level2type = N'COLUMN', @level2name = N'TipoFicha';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para marcar la plantilla principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG', @level2type = N'COLUMN', @level2name = N'PLANTPRIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG', @level2type = N'COLUMN', @level2name = N'CODCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';

