CREATE TABLE [Billing].[InvoiceEntityCapitatedGroupers] (
    [Id]                       INT           IDENTITY (1, 1) NOT NULL,
    [InvoiceEntityCapitatedId] INT           NOT NULL,
    [GroupersId]               INT           NOT NULL,
    [Code]                     VARCHAR (20)  NOT NULL,
    [Description]              VARCHAR (100) NOT NULL,
    [UserMin]                  INT           NOT NULL,
    [UserMax]                  INT           NOT NULL,
    [ProjectCME]               NUMERIC (18)  NOT NULL,
    [TotalContract]            NUMERIC (18)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoiceEntityCapitatedGroupers_Groupers] FOREIGN KEY ([GroupersId]) REFERENCES [Contract].[Groupers] ([Id]),
    CONSTRAINT [FK_InvoiceEntityCapitatedGroupers_InvoiceEntityCapitated] FOREIGN KEY ([InvoiceEntityCapitatedId]) REFERENCES [Billing].[InvoiceEntityCapitated] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total del agrupador al momento de guardar la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'TotalContract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ProjectCME del agrupador al momento de guardar la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'ProjectCME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'UserMax del agrupador al momento de guardar la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'UserMax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'UserMin del agrupador al momento de guardar la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'UserMin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del agrupador al momento de guardar la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del agrupador', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del agrupador', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'GroupersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la factura de monto fijo (Capitada)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'InvoiceEntityCapitatedId';

