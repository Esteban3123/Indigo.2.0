CREATE TABLE [Security].[FormRelationshipInt] (
    [Id]        INT         NOT NULL,
    [IdErpForm] VARCHAR (5) NOT NULL,
    [IdHisForm] CHAR (3)    NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre formularios del módulo ERP y formularios del módulo de Historia Clínica (HIS), usada para vincular pantallas o funcionalidades equivalentes entre ambos sistemas.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'FormRelationshipInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'FormRelationshipInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de relación entre formularios.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'FormRelationshipInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'FormRelationshipInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del formulario en el sistema ERP (módulo administrativo/financiero).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'FormRelationshipInt', @level2type = N'COLUMN', @level2name = N'IdErpForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'FormRelationshipInt', @level2type = N'COLUMN', @level2name = N'IdErpForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del formulario en el sistema de Historia Clínica (HIS/EHR).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'FormRelationshipInt', @level2type = N'COLUMN', @level2name = N'IdHisForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'FormRelationshipInt', @level2type = N'COLUMN', @level2name = N'IdHisForm';
