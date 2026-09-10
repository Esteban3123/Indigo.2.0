CREATE TABLE [dbo].[ADPOBESPE] (
    [ID]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]         VARCHAR (2)   NOT NULL,
    [DESCRIPCION]    VARCHAR (200) NOT NULL,
    [ESTADO]         BIT           NOT NULL,
    [RIESGO]         VARCHAR (500) NULL,
    [INTERVENCION]   VARCHAR (MAX) NULL,
    [TIPOPOBESP]     VARCHAR (3)   CONSTRAINT [DF_ADPOBESPE_TIPOPOBESP] DEFAULT ((1)) NOT NULL,
    [TIPOPOESPERIES] INT           NULL,
    CONSTRAINT [PK_ADPOBESPE] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si la población especial tiene riesgo: 1=Sí/Con riesgo, 2=No/Sin riesgo; bandera booleana para filtrado de poblaciones vulnerables.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'TIPOPOESPERIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Población con Riesgo  1-> Si  2->No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'TIPOPOESPERIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'TIPOPOESPERIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de población especial (VARCHAR 3, default=1): clasificación que identifica categorías como gestantes, adultos mayores, discapacitados, víctimas, LGBTIQ+.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'TIPOPOBESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de población', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'TIPOPOBESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'TIPOPOBESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plan o protocolo de intervención sanitaria para la población especial (VARCHAR MAX), acciones, procedimientos o estrategias de atención y control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'INTERVENCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'intervención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'INTERVENCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'INTERVENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de riesgos asociados a la población especial (VARCHAR 500), factores de vulnerabilidad, comorbilidades o condiciones de riesgo identificadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'RIESGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'RIESGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'RIESGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: 1=Activo/Vigente, 0=Inactivo/Deshabilitado; controla si la población especial está en uso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado   1: activo  0: inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la población especial con riesgo (VARCHAR 200), nombre o etiqueta de la clasificación poblacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de población especial (VARCHAR 2), clave única para clasificación de poblaciones vulnerables.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de la población especial con riesgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumérico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Poblaciones especiales de admisión: catálogo de grupos o condiciones especiales de pacientes (como gestantes, víctimas, discapacitados, adultos mayores, etc.) con sus riesgos asociados e intervenciones recomendadas. Se usa para clasificar y caracterizar pacientes en el proceso de admisión e ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE';
