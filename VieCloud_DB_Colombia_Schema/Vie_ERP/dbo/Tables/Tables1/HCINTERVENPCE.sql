CREATE TABLE [dbo].[HCINTERVENPCE] (
    [CODINTPCE] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMINTPCE] VARCHAR (100) NOT NULL,
    [ESTADO]    INT           NULL,
    CONSTRAINT [PK_HCINTERVENPCE] PRIMARY KEY CLUSTERED ([CODINTPCE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la intervención: 1=Activo, 2=Inactivo. Indica si el registro de intervención está disponible para uso en procedimientos clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERVENPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1->Activo 2->Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERVENPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERVENPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción de la intervención quirúrgica/procedimiento. Texto que identifica el tipo de procedimiento, acto médico o intervención realizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERVENPCE', @level2type = N'COLUMN', @level2name = N'NOMINTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la intervención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERVENPCE', @level2type = N'COLUMN', @level2name = N'NOMINTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERVENPCE', @level2type = N'COLUMN', @level2name = N'NOMINTPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador de la intervención (clave primaria). Número secuencial que referencia el tipo de procedimiento, intervención o acto quirúrgico en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERVENPCE', @level2type = N'COLUMN', @level2name = N'CODINTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la intervención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERVENPCE', @level2type = N'COLUMN', @level2name = N'CODINTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERVENPCE', @level2type = N'COLUMN', @level2name = N'CODINTPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de intervenciones o procedimientos de enfermería/paciente. Registra los tipos de intervenciones clínicas disponibles para asociar a la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERVENPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERVENPCE';
