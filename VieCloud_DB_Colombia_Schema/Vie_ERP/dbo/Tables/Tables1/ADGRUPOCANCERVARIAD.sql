CREATE TABLE [dbo].[ADGRUPOCANCERVARIAD] (
    [ID]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDADGRUPOCANCERC] INT           NOT NULL,
    [CODIGOCAMPO]      INT           NOT NULL,
    [NOMBRECAMPO]      VARCHAR (MAX) NOT NULL,
    [VISIBLE]          BIT           NULL,
    [OBLIGATORIO]      BIT           NULL,
    CONSTRAINT [PK_ADGRUPOCANCERVARIAD] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano de obligatoriedad del campo en formulario de cáncer (1=Sí, requerido; 0=No, opcional). Tipo: BIT. Determina si el campo debe ser completado en la variedad de cáncer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Obligatorio  1 =  Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano de visibilidad del campo en interfaz de cáncer (1=Sí, visible; 0=No, oculto). Tipo: BIT. Controla si se muestra en formularios y reportes oncológicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'VISIBLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si el campo es visible 1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'VISIBLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'VISIBLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del campo clínico en variedad de cáncer. Tipo: VARCHAR(MAX). Etiqueta legible para usuarios en formularios y reportes de oncología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'NOMBRECAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el nombre de campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'NOMBRECAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'NOMBRECAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador del campo en la variedad de cáncer. Tipo: INT. Referencia técnica para búsqueda, mapeo de RIPS y validaciones de datos oncológicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'CODIGOCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Codigo del campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'CODIGOCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'CODIGOCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del grupo o categoría de cáncer padre. Tipo: INT. Vincula cada variedad de cáncer a su clasificación oncológica principal (ej: cáncer de mama, próstata, pulmón).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'IDADGRUPOCANCERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el ID de grupos canceres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'IDADGRUPOCANCERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'IDADGRUPOCANCERC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (PK) de la variedad de cáncer. Tipo: INT IDENTITY(1,1). Clave primaria auto-incremental para registro individual de configuración oncológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Variantes o campos adicionales asociados a grupos de cáncer en la historia clínica oncológica. Permite configurar qué campos personalizados son visibles y obligatorios para cada grupo de caracterización de cáncer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD';
