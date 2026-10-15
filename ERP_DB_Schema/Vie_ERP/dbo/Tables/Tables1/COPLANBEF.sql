CREATE TABLE [dbo].[COPLANBEF] (
    [CODPLANBE] CHAR (2)        NOT NULL,
    [DESPLANBE] CHAR (40)       NOT NULL,
    [CODCONFAC] CHAR (4)        NULL,
    [INDAUDFOR] NUMERIC (18, 9) NOT NULL,
    CONSTRAINT [PK_COPLANBEF] PRIMARY KEY CLUSTERED ([CODPLANBE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoria INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Concepto de Facturacion - Campo Opcional no esta definido en la arquitectura del modulo de contratos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF', @level2type = N'COLUMN', @level2name = N'CODCONFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF', @level2type = N'COLUMN', @level2name = N'DESPLANBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANBEF', @level2type = N'COLUMN', @level2name = N'CODPLANBE';

