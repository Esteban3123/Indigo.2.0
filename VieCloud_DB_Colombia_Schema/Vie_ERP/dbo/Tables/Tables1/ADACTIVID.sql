CREATE TABLE [dbo].[ADACTIVID] (
    [codactivi]  CHAR (5)     NOT NULL,
    [desactivi]  CHAR (200)   NOT NULL,
    [INDAUDFOR]  NUMERIC (18) NOT NULL,
    [RIESGOAGRE] BIT          NULL,
    [ESTADO]     INT          CONSTRAINT [DF_ADACTIVID_ESTADO] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_ADactivid] PRIMARY KEY CLUSTERED ([codactivi] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la Actividad (INT, default=1). 1=Activo/Vigente, 2=Inactivo/Deshabilitado. Controla si la actividad está disponible para uso en atenciones, procedimientos y procesos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1-> Activo 2->Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Riesgo de Agresión (BIT, nullable). Bandera que indica si la actividad o servicio presenta riesgo potencial de agresión al personal o paciente (Sí/No).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'RIESGOAGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Riesgo de agresión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'RIESGOAGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'RIESGOAGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de Auditoría Forense (NUMERIC 18). Marcador numérico para trazabilidad y auditoría de cambios realizados en la actividad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o Descripción de la Actividad (CHAR 200). Etiqueta legible que detalla el tipo de actividad, procedimiento, servicio o unidad funcional asociada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'desactivi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Actividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'desactivi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'desactivi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Actividad (CHAR 5, PK). Identificador único que clasifica tipos de actividades, procedimientos o servicios en el sistema de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'codactivi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Actividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'codactivi';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'codactivi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de actividades de admisión registradas en el sistema. Permite clasificar y gestionar las diferentes actividades o procedimientos administrativos habilitados, indicando si representan riesgo agregado y si están activos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID';
