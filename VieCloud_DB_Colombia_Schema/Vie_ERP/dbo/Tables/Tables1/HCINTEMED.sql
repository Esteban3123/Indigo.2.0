CREATE TABLE [dbo].[HCINTEMED] (
    [CODPRODUA] CHAR (20)     NOT NULL,
    [CODPRODUB] CHAR (20)     NOT NULL,
    [AUTO]      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NIVRIESGO] CHAR (1)      NOT NULL,
    [OBSERVACI] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCINTEMED] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_HCINTEMED__CODPRODUA]
    ON [dbo].[HCINTEMED]([CODPRODUA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas o notas sobre interacciones medicamentosas detectadas; campo de texto libre para documentar hallazgos, contraindicaciones o recomendaciones del farmacéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de riesgo de interacción medicamentosa: 1=Graves (contraindicado), 2=Moderadas (requiere monitoreo), 3=Leves (sin intervención necesaria); escala de severidad clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'NIVRIESGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de riesgo 1-Graves 2-Moderadas 3-Leves', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'NIVRIESGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'NIVRIESGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (auto-incremental INT) de cada registro de interacción medicamentosa en la historia clínica; clave primaria de la tabla HCINTEMED.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Auto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno del segundo medicamento/producto involucrado en la interacción; referencia al producto B en comparación farmacológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'CODPRODUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Interno del Producto A', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'CODPRODUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'CODPRODUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del primer medicamento/producto involucrado en la interacción; identificador del producto A que se evalúa respecto a contraindices, duplicidades o incompatibilidades farmacéuticas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'CODPRODUA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la iteracciones medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'CODPRODUA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'CODPRODUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de interacciones entre medicamentos en historia clínica: almacena pares de productos farmacéuticos que presentan interacción, el nivel de riesgo asociado y observaciones clínicas relevantes para alertar al profesional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED';
