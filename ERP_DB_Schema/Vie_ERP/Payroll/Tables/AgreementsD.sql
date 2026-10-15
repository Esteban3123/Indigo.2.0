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
CREATE NONCLUSTERED INDEX [IX_AgreementsD__AgreementsCId]
    ON [Payroll].[AgreementsD]([AgreementsCId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Cuota', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'StateShare';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Pago:   1. Por Nomina  2. Manual  3. Por archivo  4. Por Vacaciones  5. Liquidación de Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'TypePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Pago', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'DatePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Cuota Pagada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'ShareValuePaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera del convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'AgreementsCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'Id';

