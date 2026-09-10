CREATE TABLE [dbo].[HCARRURGC] (
    [AUTOARRIB] NUMERIC (18) IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODARRURG] CHAR (3)     NOT NULL,
    [DESARRURG] CHAR (60)    NOT NULL,
    [TIPARRURG] CHAR (1)     NOT NULL,
    CONSTRAINT [PK_HCARRIURG] PRIMARY KEY CLUSTERED ([AUTOARRIB] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Arribo a Urgencias: clasificación que indica si se refiere al medio de transporte utilizado (1) o al estado físico del paciente (2) al momento de la llegada a emergencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC', @level2type = N'COLUMN', @level2name = N'TIPARRURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Arribo Urgencias   1:Medio de Transporte  2:Estado Fisico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC', @level2type = N'COLUMN', @level2name = N'TIPARRURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC', @level2type = N'COLUMN', @level2name = N'TIPARRURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del Arribo a Urgencias: detalle textual que especifica la modalidad o condición de ingreso del paciente al servicio de urgencias/emergencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC', @level2type = N'COLUMN', @level2name = N'DESARRURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Arribo Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC', @level2type = N'COLUMN', @level2name = N'DESARRURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC', @level2type = N'COLUMN', @level2name = N'DESARRURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Arribo a Urgencias: identificador único alfanumérico que clasifica el tipo de entrada o forma de llegada del paciente al servicio de urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC', @level2type = N'COLUMN', @level2name = N'CODARRURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Arribo Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC', @level2type = N'COLUMN', @level2name = N'CODARRURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC', @level2type = N'COLUMN', @level2name = N'CODARRURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (IDENTITY): clave primaria técnica generada automáticamente que identifica unívocamente cada registro de arribo a urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC', @level2type = N'COLUMN', @level2name = N'AUTOARRIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC', @level2type = N'COLUMN', @level2name = N'AUTOARRIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC', @level2type = N'COLUMN', @level2name = N'AUTOARRIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de arreglos o tipos de llegada en urgencias. Registra las diferentes modalidades o condiciones de arribo con que un paciente puede ingresar al servicio de urgencias (por ejemplo: ambulancia, caminando, en silla de ruedas).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC';
