CREATE TABLE [dbo].[COCONTARI] (
    [CODCONTRA] CHAR (6)        NOT NULL,
    [CODPLANBE] CHAR (2)        NOT NULL,
    [CODCENATE] CHAR (10)       NOT NULL,
    [UFUCODIGO] CHAR (10)       NOT NULL,
    [CODPLASER] CHAR (3)        NOT NULL,
    [CODPLAPRO] CHAR (3)        NOT NULL,
    [CODPLAHOS] CHAR (3)        NOT NULL,
    [EXIGIRAUT] BIT             NOT NULL,
    [INDAUDFOR] NUMERIC (18, 9) NOT NULL,
    CONSTRAINT [PK_COCONTARI] PRIMARY KEY CLUSTERED ([CODCONTRA] ASC, [CODPLANBE] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoria INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Establece si se exige autorizacion en el formulario de Ingresos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'EXIGIRAUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantilla de Hospitalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODPLAHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantilla de Medicamentos - Trae el valor de los medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODPLAPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantilla para Servicios IPS - Trae el valor de los servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODPLASER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODPLANBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTARI', @level2type = N'COLUMN', @level2name = N'CODCONTRA';

