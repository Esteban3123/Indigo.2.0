CREATE TABLE [Billing].[ConditionSalesUser] (
    [Id]               INT          IDENTITY (1, 1) NOT NULL,
    [ConditionSalesId] INT          NOT NULL,
    [UserId]           INT          NOT NULL,
    [UserCode]         VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_ConditionSalesUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConditionSalesUser_ConditionSales] FOREIGN KEY ([ConditionSalesId]) REFERENCES [Billing].[ConditionSales] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConditionSalesUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConditionSalesUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la condicion de ventas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConditionSalesUser', @level2type = N'COLUMN', @level2name = N'ConditionSalesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConditionSalesUser', @level2type = N'COLUMN', @level2name = N'Id';

