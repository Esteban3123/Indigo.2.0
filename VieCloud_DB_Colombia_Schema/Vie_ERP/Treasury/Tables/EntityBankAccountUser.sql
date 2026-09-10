CREATE TABLE [Treasury].[EntityBankAccountUser] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEntityBankAccount] INT           NOT NULL,
    [CodUser]             VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_EntityBankAccountUser__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntityBankAccountUser_EntityBankAccounts] FOREIGN KEY ([IdEntityBankAccount]) REFERENCES [Treasury].[EntityBankAccounts] ([Id])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_EntityBankAccountUser_CodUser_IdEntiyBankAccount]
    ON [Treasury].[EntityBankAccountUser]([CodUser] ASC, [IdEntityBankAccount] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario autorizado para operar la cuenta bancaria; identificador de la persona que tiene permisos de transacción, consulta o administración (VARCHAR 250, PII - Ofuscación recomendada)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountUser', @level2type = N'COLUMN', @level2name = N'CodUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario a autorizar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountUser', @level2type = N'COLUMN', @level2name = N'CodUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountUser', @level2type = N'COLUMN', @level2name = N'CodUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta bancaria institucional vinculada; referencia FK a EntityBankAccounts que define la entidad tesorería y sus datos bancarios (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountUser', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta bancaria a autorizar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountUser', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountUser', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de autorización usuario-cuenta bancaria; clave primaria para el mapeo de permisos de transaccionalidad (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountUser', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona usuarios del sistema con cuentas bancarias de entidades, controlando qué usuarios tienen acceso o están asociados a cada cuenta bancaria registrada en tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountUser';
