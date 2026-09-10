CREATE TABLE [dbo].[HCVALORAPCE] (
    [CODVALORAPCE] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMVALORAPCE] VARCHAR (100) NOT NULL,
    [ESTADO]       INT           NULL,
    CONSTRAINT [PK_HCVALORAPCE] PRIMARY KEY CLUSTERED ([CODVALORAPCE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la valoración: 1=Activo, 2=Inactivo. Indica si la valoración de APCE está disponible para uso clínico o desactivada en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALORAPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1->Activo,2->Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALORAPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALORAPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción de la valoración de APCE (Apreciación, Planeación, Ejecución). Identifica el tipo de evaluación clínica registrada en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALORAPCE', @level2type = N'COLUMN', @level2name = N'NOMVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la valoracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALORAPCE', @level2type = N'COLUMN', @level2name = N'NOMVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALORAPCE', @level2type = N'COLUMN', @level2name = N'NOMVALORAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único (PK) de la valoración de APCE. Clave primaria que referencia evaluaciones clínicas en registros de atención y procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALORAPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la valoracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALORAPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALORAPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de valores o parámetros de aprendizaje/configuración clínica (APCE), que registra los distintos tipos o categorías de valoración utilizados en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALORAPCE';
