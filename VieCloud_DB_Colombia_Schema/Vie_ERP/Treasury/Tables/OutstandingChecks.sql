CREATE TABLE [Treasury].[OutstandingChecks] (
    [Id]          INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCheckBook] INT    NOT NULL,
    [CheckNumber] BIGINT NOT NULL,
    CONSTRAINT [PK_OutstandingChecks] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OutstandingChecks_Checkbooks] FOREIGN KEY ([IdCheckBook]) REFERENCES [Treasury].[Checkbooks] ([Id])
);
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (clave foránea) de la chequera a la que pertenece el cheque pendiente; vinculación con la tabla Treasury.Checkbooks para rastrear libro de cheques, banco y cuenta asociados. Tipo: INT, FK.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'OutstandingChecks', @level2type = N'COLUMN', @level2name = N'IdCheckBook';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la chequera asociada al cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'OutstandingChecks', @level2type = N'COLUMN', @level2name = N'IdCheckBook';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'OutstandingChecks', @level2type = N'COLUMN', @level2name = N'IdCheckBook';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de cheque pendiente en el sistema; clave primaria autoincrementable que rastrea cada cheque en estado outstanding (impago o en tránsito). Tipo: INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'OutstandingChecks', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'OutstandingChecks', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'OutstandingChecks', @level2type = N'COLUMN', @level2name = N'Id';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de cheques pendientes o en tránsito (cheques emitidos aún no cobrados o conciliados). Permite controlar qué cheques de una chequera están outstanding, es decir, girados pero no descontados del banco.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'OutstandingChecks';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'OutstandingChecks';
