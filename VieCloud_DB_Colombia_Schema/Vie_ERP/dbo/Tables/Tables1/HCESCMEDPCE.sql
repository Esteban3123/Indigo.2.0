CREATE TABLE [dbo].[HCESCMEDPCE] (
    [CODESCMEDPCE] INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMESCMEDPCE] VARCHAR (80) NOT NULL,
    [ESTADO]       INT          NULL,
    CONSTRAINT [PK_HCESCAMEDPCE] PRIMARY KEY CLUSTERED ([CODESCMEDPCE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro (1=Activo, 2=Inactivo). Indica si la escala de medición está habilitada o deshabilitada en el sistema. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCMEDPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1->Activo 2->inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCMEDPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCMEDPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la escala de medición clínica. Identificador descriptivo de escalas utilizadas en evaluaciones, valoraciones de pacientes, diagnósticos y procedimientos médicos. Tipo: VARCHAR(80).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCMEDPCE', @level2type = N'COLUMN', @level2name = N'NOMESCMEDPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la escala de medición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCMEDPCE', @level2type = N'COLUMN', @level2name = N'NOMESCMEDPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCMEDPCE', @level2type = N'COLUMN', @level2name = N'NOMESCMEDPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del registro de escala de medición. Identificador numérico secuencial (IDENTITY), clave primaria. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCMEDPCE', @level2type = N'COLUMN', @level2name = N'CODESCMEDPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del registo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCMEDPCE', @level2type = N'COLUMN', @level2name = N'CODESCMEDPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCMEDPCE', @level2type = N'COLUMN', @level2name = N'CODESCMEDPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de escalas médicas para el paciente en historia clínica. Registra los tipos o nombres de escalas de valoración clínica (como escalas de dolor, riesgo, funcionalidad) utilizadas en la atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCMEDPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCMEDPCE';
