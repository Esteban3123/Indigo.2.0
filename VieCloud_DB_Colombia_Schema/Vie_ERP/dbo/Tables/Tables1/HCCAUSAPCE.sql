CREATE TABLE [dbo].[HCCAUSAPCE] (
    [CODCAUSAPCE] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMCAUSAPCE] VARCHAR (100) NOT NULL,
    [ESTADO]      INT           NULL,
    CONSTRAINT [PK_HCCAUSAPCE] PRIMARY KEY CLUSTERED ([CODCAUSAPCE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: 1=Activo, 2=Inactivo. Indica si la causa de diagnóstico está disponible para uso en planes de cuidado de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCAUSAPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 = Activo   2 = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCAUSAPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCAUSAPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la causa de diagnóstico de valoración del plan de cuidado de enfermería. Describe la condición o factor identificado en la evaluación de enfermería que justifica la intervención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCAUSAPCE', @level2type = N'COLUMN', @level2name = N'NOMCAUSAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del causas de diagnosticos de valoracion del plan cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCAUSAPCE', @level2type = N'COLUMN', @level2name = N'NOMCAUSAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCAUSAPCE', @level2type = N'COLUMN', @level2name = N'NOMCAUSAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único (INT, PK) de la causa de diagnóstico. Generado automáticamente por secuencia IDENTITY para referencia en planes de cuidado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCAUSAPCE', @level2type = N'COLUMN', @level2name = N'CODCAUSAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la causa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCAUSAPCE', @level2type = N'COLUMN', @level2name = N'CODCAUSAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCAUSAPCE', @level2type = N'COLUMN', @level2name = N'CODCAUSAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de causas de la paciente o causas de atención clínica (causas de consulta o ingreso). Permite clasificar el motivo o causa que origina un evento de salud del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCAUSAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCAUSAPCE';
