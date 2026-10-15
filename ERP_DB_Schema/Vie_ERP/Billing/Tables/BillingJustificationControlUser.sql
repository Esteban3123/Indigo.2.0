CREATE TABLE [Billing].[BillingJustificationControlUser] (
    [Id]                            INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BillingJustificationControlId] INT          NOT NULL,
    [UserId]                        INT          NOT NULL,
    [UserCode]                      VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_BillingJustificationControlUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingJustificationControlUser_BillingJustificationControl] FOREIGN KEY ([BillingJustificationControlId]) REFERENCES [Billing].[BillingJustificationControl] ([Id])
);


GO
ALTER TABLE [Billing].[BillingJustificationControlUser] NOCHECK CONSTRAINT [FK_BillingJustificationControlUser_BillingJustificationControl];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingJustificationControlUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingJustificationControlUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta hospitalaria que se esta dando permisos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingJustificationControlUser', @level2type = N'COLUMN', @level2name = N'BillingJustificationControlId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la autorizacion a usuarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingJustificationControlUser', @level2type = N'COLUMN', @level2name = N'Id';

