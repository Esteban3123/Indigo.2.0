CREATE TABLE [dbo].[HCDIAGENF] (
    [CODDIAGENF]  VARCHAR (5)   NOT NULL,
    [NOMBDIAGENF] VARCHAR (150) NOT NULL,
    [ESTADO]      INT           NOT NULL,
    CONSTRAINT [PK_HCDIAGENF] PRIMARY KEY CLUSTERED ([CODDIAGENF] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de activación del diagnóstico de enfermería (1=Activo, 0=Inactivo). Tipo INT. Controla disponibilidad para registro en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGENF', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'True = Activo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGENF', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGENF', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del diagnóstico de enfermería según clasificación NANDA. Tipo VARCHAR(150). Texto libre para búsqueda en diagnósticos de atención de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGENF', @level2type = N'COLUMN', @level2name = N'NOMBDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre diagnostico Enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGENF', @level2type = N'COLUMN', @level2name = N'NOMBDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGENF', @level2type = N'COLUMN', @level2name = N'NOMBDIAGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del diagnóstico de enfermería (PK, VARCHAR 5). Identifica diagnósticos de enfermería relacionados con causas de valoración (FK HCDIAGCAUPCE). Clave para mapeo de diagnósticos clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGENF', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Causas Valoracion relacionado con tabla HCDIAGCAUPCE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGENF', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGENF', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de diagnósticos de enfermería utilizados en la historia clínica. Registra los diagnósticos propios del proceso de enfermería con su código, nombre descriptivo y estado de vigencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGENF';
