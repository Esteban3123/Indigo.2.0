CREATE TABLE [Common].[PromptPaymentDiscount] (
    [Id]           INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RangeName]    VARCHAR (30)   NOT NULL,
    [InitialRank]  INT            NOT NULL,
    [EndRank]      INT            NOT NULL,
    [DiscountRate] DECIMAL (5, 2) NOT NULL,
    [SupplierId]   INT            NOT NULL,
    CONSTRAINT [PK__PromptPa__3214EC0719BE6D50] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PromptPaymentDiscount_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id])
);


GO
ALTER TABLE [Common].[PromptPaymentDiscount] NOCHECK CONSTRAINT [FK_PromptPaymentDiscount_Supplier];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion tabla supplier', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'DiscountRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rango final', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'EndRank';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rango Inicial', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'InitialRank';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Rango', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'RangeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PromptPaymentDiscount', @level2type = N'COLUMN', @level2name = N'Id';

