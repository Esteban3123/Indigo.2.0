CREATE TABLE [dbo].[ADCONCOED] (
    [CODCONCEC] NUMERIC (18) NOT NULL,
    [NUMCONCIT] CHAR (20)    NOT NULL,
    CONSTRAINT [PK_ADCONCOED] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [NUMCONCIT] ASC),
    CONSTRAINT [FK_ADCONCOED_ADCONCOEX] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[ADCONCOEX] ([CODCONCEC])
);


GO
ALTER TABLE [dbo].[ADCONCOED] NOCHECK CONSTRAINT [FK_ADCONCOED_ADCONCOEX];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo de cita en interfaz externa; identificador secuencial de la cita importada o sincronizada desde sistema externo, usado para trazabilidad de atención/consulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOED', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Consecutivo Cita en Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOED', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOED', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo interno (autonumérico); identificador único numérico autogenerado que referencia el registro maestro de citas en ADCONCOEX, clave primaria compuesta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOED', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo Interno (Autonumerico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOED', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOED', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la relación entre códigos de conciliación o conceptos de cobro y sus números de conciliación/citación asociados, usada en procesos de conciliación de cuentas o facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOED';
