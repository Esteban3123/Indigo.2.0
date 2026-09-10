CREATE TABLE [Treasury].[Checkbooks] (
    [Id]                   INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEntityBanckAccount] INT         NOT NULL,
    [InitialNumber]        BIGINT      NOT NULL,
    [EndNumber]            BIGINT      NOT NULL,
    [CurrentNumber]        BIGINT      NOT NULL,
    [Prefix]               VARCHAR (5) NOT NULL,
    [Status]               TINYINT     NOT NULL,
    [TimeStamp]            ROWVERSION  NOT NULL,
    CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Voucher_EntityBankAccount] FOREIGN KEY ([IdEntityBanckAccount]) REFERENCES [Treasury].[EntityBankAccounts] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) de auditoría: registra la creación, última modificación o evento relevante de la chequera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la chequera: 1=Activa (en uso), 2=Bloqueada (suspendida temporalmente), 3=Finalizada (agotada o cancelada)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado (Activa=1, Bloqueada=2, Finalizada=3)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prefijo alfanumérico (VARCHAR 5) que identifica o diferencia series de chequeras de una misma cuenta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'Prefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prefijo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'Prefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'Prefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número actual o próximo cheque a emitir en la chequera; indica el avance dentro del rango', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'CurrentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número actual de la chequera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'CurrentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'CurrentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número final o límite superior del rango secuencial de cheques en la chequera (BIGINT)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'EndNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número Final de la chequera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'EndNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'EndNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número inicial o de inicio del rango secuencial de cheques en la chequera (BIGINT)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'InitialNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número inicial de la chequera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'InitialNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'InitialNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta bancaria asociada (FK a EntityBankAccounts). Referencia la entidad bancaria propietaria de la chequera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'IdEntityBanckAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'IdEntityBanckAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'IdEntityBanckAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, Identity) del registro de chequera en el sistema Treasury', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de chequeras bancarias asignadas a cada cuenta bancaria de la organización. Controla el rango de números de cheques disponibles, el número actual en uso y el prefijo de cada chequera.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Checkbooks';
