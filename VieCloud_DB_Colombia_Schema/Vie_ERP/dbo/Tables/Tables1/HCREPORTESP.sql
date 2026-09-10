CREATE TABLE [dbo].[HCREPORTESP] (
    [ID]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPRMODELOHC]  INT           NOT NULL,
    [NOMBREVISTA]   VARCHAR (150) NOT NULL,
    [NOMBREREPORTE] VARCHAR (150) NOT NULL,
    [FECHACREA]     DATETIME      NOT NULL,
    [USUCREA]       CHAR (20)     NOT NULL,
    [FECHAMOD]      DATETIME      NULL,
    [USUMOD]        CHAR (20)     NULL,
    [TIPO]          INT           NULL,
    CONSTRAINT [PK_HCREPORTESP] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCREPORTESP_PRMODELOHC] FOREIGN KEY ([IDPRMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - ''modelo HC parametrizables  2 - ''Notas Administrativas  3 - ''Otros Reportes  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario que realizó la última modificación (CHAR 20 NULL). Identificación del administrador o profesional que actualizó el reporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'USUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'USUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'USUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp de última modificación (DATETIME NULL). Fecha y hora del último cambio realizado al reporte o configuración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'FECHAMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario que creó el registro (CHAR 20). Identificación del profesional o administrador que configuró el reporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'USUCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'USUCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'USUCREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp de creación del registro (DATETIME). Fecha y hora en que se registró inicialmente el reporte en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre personalizado o etiqueta del reporte visible al usuario. Título descriptivo para búsqueda y consulta de reportes de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'NOMBREREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre personalizado del reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'NOMBREREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'NOMBREREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre técnico de la vista o stored procedure que genera el reporte, generalmente prefijado con ESE_SP_. Referencia a objeto SQL ejecutable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'NOMBREVISTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la vista o stored procedure, empieza por ESE_SP_', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'NOMBREVISTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'NOMBREVISTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea a modelo de Historia Clínica especializada (PRMODELOHC). Identifica la plantilla o estructura de HC asociada al reporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'IDPRMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id modelo de HC (PRMODELOHC) - Id HC especializadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'IDPRMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'IDPRMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de reporte en Historia Clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los reportes especiales asociados a modelos de historia clínica, vinculando cada reporte con su vista de base de datos y nombre de presentación, junto con la trazabilidad de creación y modificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP';
