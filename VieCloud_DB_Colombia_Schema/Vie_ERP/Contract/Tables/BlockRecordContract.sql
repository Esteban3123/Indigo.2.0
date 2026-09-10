CREATE TABLE [Contract].[BlockRecordContract] (
    [Id]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FormId]    INT           NOT NULL,
    [RecordId]  INT           NOT NULL,
    [CodUser]   VARCHAR (250) NOT NULL,
    [NameUser]  VARCHAR (250) NOT NULL,
    [BlockDate] DATETIME      NOT NULL,
    CONSTRAINT [PK_BlockRecordContract] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se bloqueó el registro de contrato; timestamp de bloqueo, auditoría temporal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'BlockDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora en que se bloqueo el registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'BlockDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'BlockDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del usuario profesional de salud o administrativo que aplicó el bloqueo al registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'NameUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del usuario quien bloquea el registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'NameUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'NameUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único o identificador del usuario (login, cédula, matrícula) que bloqueó el registro; referencia a usuario del sistema', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'CodUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario quien bloquea el registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'CodUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'CodUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro de contrato que fue bloqueado; referencia a la transacción o documento bajo bloqueo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'RecordId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro que se encuentra bloqueado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'RecordId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'RecordId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del formulario o documento de contrato que contiene el registro bloqueado; referencia al formulario padre', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'FormId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del formulario en el que se encuentra bloqueado el registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'FormId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'FormId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY) del registro de bloqueo en la tabla BlockRecordContract; clave primaria', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los bloqueos aplicados a registros específicos de contratos, indicando qué usuario realizó el bloqueo y en qué momento, para controlar el acceso o edición de registros contractuales.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'BlockRecordContract';
