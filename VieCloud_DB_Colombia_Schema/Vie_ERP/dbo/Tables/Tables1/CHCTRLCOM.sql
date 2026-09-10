CREATE TABLE [dbo].[CHCTRLCOM] (
    [CODTIPDIE] CHAR (1)     NOT NULL,
    [HORINICOM] DATETIME     NOT NULL,
    [HORFINCOM] DATETIME     NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CHCTRLCOM] PRIMARY KEY CLUSTERED ([CODTIPDIE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría y conformidad; campo numérico reservado para registro de auditoría, trazabilidad y control de conformidad en solicitudes de alimentos/dietas. NUMERIC(18), PII auditado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora final máxima para solicitar alimentos; límite temporal después del cual no se aceptan nuevos pedidos de comida para el tipo de dieta especificado. DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM', @level2type = N'COLUMN', @level2name = N'HORFINCOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora maxima para solicitar alimentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM', @level2type = N'COLUMN', @level2name = N'HORFINCOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM', @level2type = N'COLUMN', @level2name = N'HORFINCOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora inicial para solicitar alimentos; marca el inicio del período en que el paciente o nutrición puede registrar pedidos de comida según tipo de dieta. DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM', @level2type = N'COLUMN', @level2name = N'HORINICOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora en la que se puede iniciar la solicitud de alimentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM', @level2type = N'COLUMN', @level2name = N'HORINICOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM', @level2type = N'COLUMN', @level2name = N'HORINICOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de dieta: 1=Desayuno, 2=Almuerzo, 3=Cena, 4=Complemento mañana, 5=Complemento tarde, 6=Otro. Clasificación de comidas/alimentación del paciente en centro de atención. CHAR(1), clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM', @level2type = N'COLUMN', @level2name = N'CODTIPDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de  Dieta:  1: Desayuno  2: Almuerzo  3: Cena  4: Complemento mañana  5: Complemento tarde  6: Otro  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM', @level2type = N'COLUMN', @level2name = N'CODTIPDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM', @level2type = N'COLUMN', @level2name = N'CODTIPDIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de control de comunicaciones o compromisos clínicos, que almacena el tipo de dieta o indicación, el rango horario de inicio y fin, y un indicador de auditoría o formulario auditado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM';
