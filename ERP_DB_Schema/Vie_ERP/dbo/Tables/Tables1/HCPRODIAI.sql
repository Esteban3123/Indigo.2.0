CREATE TABLE [dbo].[HCPRODIAI] (
    [CODPRODUC] CHAR (20)                                                                     NOT NULL,
    [CODDIAGNO] CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')  NOT NULL,
    [CODCONCEC] NUMERIC (18)                                                                  NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [MANEXTPRO] BIT                                                                           NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                  NOT NULL,
    CONSTRAINT [PK_HCPRODIAI] PRIMARY KEY CLUSTERED ([CODPRODUC] ASC, [CODDIAGNO] ASC, [CODCONCEC] ASC, [MANEXTPRO] ASC),
    CONSTRAINT [FK_HCPRODIAI_HCPRESCRC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCPRESCCI] ([CODCONCEC]),
    CONSTRAINT [FK_HCPRODIAI_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[HCPRODIAI] NOCHECK CONSTRAINT [FK_HCPRODIAI_HCPRESCRC];


GO
ALTER TABLE [dbo].[HCPRODIAI] NOCHECK CONSTRAINT [FK_HCPRODIAI_INDIAGNOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODIAI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde a un plan de Manejo Externo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODIAI', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODIAI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODIAI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODIAI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRODIAI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';

