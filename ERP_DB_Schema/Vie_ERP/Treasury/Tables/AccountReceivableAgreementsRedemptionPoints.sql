CREATE TABLE [Treasury].[AccountReceivableAgreementsRedemptionPoints] (
    [Id]                           INT          IDENTITY (1, 1) NOT NULL,
    [AccountReceivableId]          INT          NOT NULL,
    [AgreementsRedemptionPointsId] INT          NOT NULL,
    [CreationUser]                 VARCHAR (50) NOT NULL,
    [CreationDate]                 DATETIME     NOT NULL,
    [CashReceiptsId]               INT          NULL,
    CONSTRAINT [PK_AccountReceivableAgreementsRedemptionPoints] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountReceivableAgreementsRedemptionPoints_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_AccountReceivableAgreementsRedemptionPoints_AgreementsRedemptionPoints] FOREIGN KEY ([AgreementsRedemptionPointsId]) REFERENCES [Treasury].[AgreementsRedemptionPoints] ([Id])
);


GO
ALTER TABLE [Treasury].[AccountReceivableAgreementsRedemptionPoints] NOCHECK CONSTRAINT [FK_AccountReceivableAgreementsRedemptionPoints_AccountReceivable];


GO
ALTER TABLE [Treasury].[AccountReceivableAgreementsRedemptionPoints] NOCHECK CONSTRAINT [FK_AccountReceivableAgreementsRedemptionPoints_AgreementsRedemptionPoints];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del recibo de caja ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'CashReceiptsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la fecha de creación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el usuario de creación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del convio de redencion de puntos con el que se creo la cuenta por cobrar ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'AgreementsRedemptionPointsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por cobrar creada a partir de redencion de puntos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'Id';

