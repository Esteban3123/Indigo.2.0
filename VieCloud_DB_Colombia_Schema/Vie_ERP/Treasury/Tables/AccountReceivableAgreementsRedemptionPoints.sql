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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del recibo de caja (comprobante de pago); vinculación opcional al movimiento de tesorería que liquida esta redemción de puntos. INT, nullable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'CashReceiptsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del recibo de caja ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'CashReceiptsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'CashReceiptsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de asociación entre cuenta por cobrar y acuerdo de redemción; timestamp del registro de auditoría. DATETIME, requerido.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de creación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro (login/identificación del operario de tesorería); campo de auditoría. VARCHAR(50), requerido.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el usuario de creación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del acuerdo/convenio de redemción de puntos utilizado para generar la cuenta por cobrar; FK a Treasury.AgreementsRedemptionPoints. INT, requerido.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'AgreementsRedemptionPointsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del convio de redencion de puntos con el que se creo la cuenta por cobrar ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'AgreementsRedemptionPointsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'AgreementsRedemptionPointsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por cobrar originada en la redemción de puntos; FK a Portfolio.AccountReceivable. Trazabilidad de ingresos/facturas desde puntos. INT, requerido.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por cobrar creada a partir de redencion de puntos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial del registro de asociación entre AccountReceivable y AgreementsRedemptionPoints; Primary Key Clustered. INT IDENTITY, requerido.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona las cuentas por cobrar con los acuerdos de redención de puntos aplicados, registrando qué puntos o beneficios de convenio fueron canjeados al momento del recaudo o pago de una cuenta.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AccountReceivableAgreementsRedemptionPoints';
