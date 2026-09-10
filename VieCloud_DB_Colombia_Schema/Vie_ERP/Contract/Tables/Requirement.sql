CREATE TABLE [Contract].[Requirement] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RequirementTemplateId] INT           NOT NULL,
    [Requirement]           VARCHAR (100) NOT NULL,
    [Process]               VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Requirement] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Requirement_RequirementTemplate] FOREIGN KEY ([RequirementTemplateId]) REFERENCES [Contract].[RequirementTemplate] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Trámite, procedimiento o gestión que debe ejecutarse para cumplir el requerimiento contractual. Descripción detallada del proceso administrativo o clínico asociado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement', @level2type = N'COLUMN', @level2name = N'Process';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tramite que debe hacer', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement', @level2type = N'COLUMN', @level2name = N'Process';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement', @level2type = N'COLUMN', @level2name = N'Process';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o designación del requerimiento contractual. Requisito, condición o documentación necesaria en el contrato (autorización, certificado, credencial, documentación de cumplimiento).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement', @level2type = N'COLUMN', @level2name = N'Requirement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'nombre del requerimiento  ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement', @level2type = N'COLUMN', @level2name = N'Requirement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement', @level2type = N'COLUMN', @level2name = N'Requirement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (FK) de la plantilla de requerimiento. Vinculación a la estructura predefinida o modelo de requerimiento contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement', @level2type = N'COLUMN', @level2name = N'RequirementTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la plantilla de requerimiento', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement', @level2type = N'COLUMN', @level2name = N'RequirementTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement', @level2type = N'COLUMN', @level2name = N'RequirementTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico único (PK, IDENTITY) del requerimiento individual en la tabla. Clave primaria para referencia de requerimientos en contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del requerimiento', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Requisitos específicos definidos a partir de plantillas de contrato. Registra cada requisito contractual con su descripción y el proceso asociado al que aplica dentro del módulo de contratos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement';
