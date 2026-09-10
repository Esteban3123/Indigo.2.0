CREATE TABLE [Treasury].[BlockRecordTreasury] (
    [Id]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdForm]    INT           NOT NULL,
    [IdRecord]  INT           NOT NULL,
    [CodUser]   VARCHAR (250) NOT NULL,
    [NameUser]  VARCHAR (250) NOT NULL,
    [BlockDate] DATETIME      NOT NULL,
    CONSTRAINT [PK_BlockRecord] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se bloqueó el registro (timestamp de bloqueo). Tipo: DATETIME. Usado para auditoría y trazabilidad de bloqueos en tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'BlockDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora en que se bloqueo el registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'BlockDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'BlockDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del usuario o profesional de salud que realizó el bloqueo del registro. Tipo: VARCHAR(250). Referencia a identidad del bloqueador.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'NameUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del usuario quien bloquea el registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'NameUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'NameUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código, identificación o usuario (login) del profesional o administrativo que bloqueó el registro. Tipo: VARCHAR(250). Equivalente a cédula, documento, ID de usuario del sistema.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'CodUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario quien bloquea el registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'CodUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'CodUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de tesorería (movimiento, comprobante, transacción) que fue bloqueado. Tipo: INT. Referencia a registro específico bajo bloqueo.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'IdRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro que se encuentra bloqueado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'IdRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'IdRecord';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del formulario o comprobante de tesorería en el que reside el registro bloqueado. Tipo: INT. Agrupa registros dentro de un formulario (factura, glosa, recibos, RIPS).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del formulario en el que se encuentra bloqueado el registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'IdForm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la entrada de bloqueo en BlockRecordTreasury. Tipo: INT IDENTITY. Clave primaria que rastrea cada acción de bloqueo realizada.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de bloqueo de registros en módulos de tesorería: guarda qué usuario bloqueó un registro de un formulario específico y en qué fecha, evitando ediciones simultáneas o no autorizadas.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BlockRecordTreasury';
