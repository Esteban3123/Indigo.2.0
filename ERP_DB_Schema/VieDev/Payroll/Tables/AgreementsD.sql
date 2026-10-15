CREATE TABLE [Payroll].[AgreementsD] (
    [Id]             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AgreementsCId]  INT             NOT NULL,
    [ShareValuePaid] DECIMAL (18, 2) NOT NULL,
    [DatePayment]    DATETIME        NOT NULL,
    [TypePayment]    TINYINT         NOT NULL,
    [StateShare]     VARCHAR (250)   NOT NULL,
    [TimeStamp]      ROWVERSION      NOT NULL,
    CONSTRAINT [PK_AgreementsD__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AgreementsD_AgreementsC] FOREIGN KEY ([AgreementsCId]) REFERENCES [Payroll].[AgreementsC] ([Id])
);


GO
ALTER TABLE [Payroll].[AgreementsD] NOCHECK CONSTRAINT [FK_AgreementsD_AgreementsC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Pago:   1. Por Nomina  2. Manual  3. Por archivo  4. Por Vacaciones  5. Liquidación de Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'TypePayment';

