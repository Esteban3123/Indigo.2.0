CREATE TABLE [Treasury].[CheckBlock] (
    [Id]          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCheckbook] INT          NOT NULL,
    [CheckNumber] BIGINT       NOT NULL,
    [CodUser]     VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_CheckBlock] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CheckBlock_Checkbooks] FOREIGN KEY ([IdCheckbook]) REFERENCES [Treasury].[Checkbooks] ([Id])
);
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador único del usuario que realiza el bloqueo del cheque. Tipo: VARCHAR(20). Campo de auditoría que registra quién bloqueó el documento bancario.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckBlock', @level2type = N'COLUMN', @level2name = N'CodUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del usuario que bloquea el cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckBlock', @level2type = N'COLUMN', @level2name = N'CodUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckBlock', @level2type = N'COLUMN', @level2name = N'CodUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la chequera (talonario) a la cual pertenece el cheque bloqueado. Clave foránea a [Treasury].[Checkbooks]. Vincula el bloqueo al libro de cheques correspondiente.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckBlock', @level2type = N'COLUMN', @level2name = N'IdCheckbook';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la chequera asociada al cheque bloqueado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckBlock', @level2type = N'COLUMN', @level2name = N'IdCheckbook';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckBlock', @level2type = N'COLUMN', @level2name = N'IdCheckbook';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de bloqueo de cheque. Tipo: INT IDENTITY. Clave primaria que identifica cada bloqueo registrado en el sistema de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckBlock', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del cheque bloqueado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckBlock', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckBlock', @level2type = N'COLUMN', @level2name = N'Id';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de cheques bloqueados o anulados dentro de una chequera. Permite controlar qué cheques han sido inhabilitados, en qué talonario y qué usuario realizó el bloqueo.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckBlock';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckBlock';
