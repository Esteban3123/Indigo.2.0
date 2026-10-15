CREATE TABLE [Billing].[BillingSequenceDetail] (
    [Id]                 INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseBillingC] INT    NOT NULL,
    [IdSequense]         INT    NOT NULL,
    [IdOperatingUnit]    INT    NULL,
    [Next]               BIGINT CONSTRAINT [DF_SequenseBillingD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseBillingD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseBillingD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseBillingD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseBillingD_SequenseBillingC] FOREIGN KEY ([IdSequenseBillingC]) REFERENCES [Billing].[BillingSequence] ([Id])
);


GO
ALTER TABLE [Billing].[BillingSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseBillingD_OperatingUnit];


GO
ALTER TABLE [Billing].[BillingSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseBillingD_Sequense];


GO
ALTER TABLE [Billing].[BillingSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseBillingD_SequenseBillingC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseBillingC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';

