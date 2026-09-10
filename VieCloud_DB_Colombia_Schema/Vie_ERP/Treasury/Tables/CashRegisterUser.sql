CREATE TABLE [Treasury].[CashRegisterUser] (
    [Id]             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCashRegister] INT NOT NULL,
    [IdUser]         INT NOT NULL,
    CONSTRAINT [PK_CashRegisterUser__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashRegisterUser_CashRegister] FOREIGN KEY ([IdCashRegister]) REFERENCES [Treasury].[CashRegisters] ([Id])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CashRegisterUser__IdCashRegister__IdUser]
    ON [Treasury].[CashRegisterUser]([IdCashRegister] ASC, [IdUser] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario autorizado para operar la caja registradora; referencia a usuario del sistema con permisos de tesorería, caja o facturación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterUser', @level2type = N'COLUMN', @level2name = N'IdUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario a autorizar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterUser', @level2type = N'COLUMN', @level2name = N'IdUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterUser', @level2type = N'COLUMN', @level2name = N'IdUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la caja registradora autorizada; referencia a punto de venta, caja de recepción o módulo de facturación en el centro de atención', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterUser', @level2type = N'COLUMN', @level2name = N'IdCashRegister';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la caja a autorizar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterUser', @level2type = N'COLUMN', @level2name = N'IdCashRegister';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterUser', @level2type = N'COLUMN', @level2name = N'IdCashRegister';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de asignación usuario-caja; clave primaria de la relación de autorización', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterUser', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre cajas registradoras y los usuarios autorizados para operarlas. Indica qué cajeros o empleados tienen acceso a cada caja en el módulo de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterUser';
