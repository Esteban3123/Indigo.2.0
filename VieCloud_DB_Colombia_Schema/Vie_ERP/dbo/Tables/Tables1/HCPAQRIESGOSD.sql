CREATE TABLE [dbo].[HCPAQRIESGOSD] (
    [ID]              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPAQORDENESC] INT NOT NULL,
    [IDPRHCEXPRES]    INT NOT NULL,
    CONSTRAINT [PK_HCPAQRIESGOSD] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con tabla de expresión de riesgos (PRHCEXPRES); clave foránea que vincula riesgos identificados en historia clínica, diagnósticos o factores de riesgo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIESGOSD', @level2type = N'COLUMN', @level2name = N'IDPRHCEXPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de expresion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIESGOSD', @level2type = N'COLUMN', @level2name = N'IDPRHCEXPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIESGOSD', @level2type = N'COLUMN', @level2name = N'IDPRHCEXPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con cabecera de paquete de órdenes (HCPAQORDENESC); clave foránea que vincula riesgos asociados a órdenes médicas, procedimientos, atenciones o servicios ordenados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIESGOSD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla cabecera de la tabla de paquetes de ordenes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIESGOSD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIESGOSD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY); clave primaria autoincrementable que identifica cada registro de riesgo en el detalle de paquete de órdenes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIESGOSD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIESGOSD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIESGOSD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la asociación entre órdenes de escala de riesgo del paciente y las expresiones o ítems de evaluación respondidos, permitiendo registrar las respuestas detalladas de herramientas de valoración de riesgo clínico (como escalas de caídas, úlceras por presión, dolor, entre otras) dentro de la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIESGOSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQRIESGOSD';
