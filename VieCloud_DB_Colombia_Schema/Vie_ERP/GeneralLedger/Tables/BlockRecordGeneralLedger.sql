CREATE TABLE [GeneralLedger].[BlockRecordGeneralLedger] (
    [Id]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdForm]    INT           NOT NULL,
    [IdRecord]  INT           NOT NULL,
    [CodUser]   VARCHAR (250) NOT NULL,
    [NameUser]  VARCHAR (250) NOT NULL,
    [BlockDate] DATETIME      NOT NULL,
    CONSTRAINT [PK_BlockRecord] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta (DATETIME) en que se bloqueó el registro en el libro mayor, impidiendo modificaciones posteriores', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'BlockDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora en que se bloqueo el registro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'BlockDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'BlockDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del usuario, profesional o auditor que ejecutó el bloqueo del registro contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'NameUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del usuario quien bloquea el registro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'NameUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'NameUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR) del usuario que realizó el bloqueo, identificador del profesional o auditor en el sistema', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'CodUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario quien bloquea el registro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'CodUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'CodUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del registro contable específico que fue bloqueado en el libro mayor general', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'IdRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro que se encuentra bloqueado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'IdRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'IdRecord';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del formulario o comprobante contable que contiene el registro bloqueado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del formulario en el que se encuentra bloqueado el registro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'IdForm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del evento de bloqueo registrado en la tabla de auditoría', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de bloqueo de asientos o registros contables en el libro mayor general. Guarda quién bloqueó un registro contable, en qué formulario y cuándo, para impedir modificaciones no autorizadas.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'BlockRecordGeneralLedger';
