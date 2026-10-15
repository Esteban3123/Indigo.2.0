CREATE TABLE [Payroll].[ForeclousureDetail] (
    [Id]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdForeclousure] INT           NOT NULL,
    [ShareValuePaid] NUMERIC (18)  NOT NULL,
    [DatePayment]    DATETIME      NOT NULL,
    [TypePayment]    TINYINT       NOT NULL,
    [StateShare]     VARCHAR (250) NOT NULL,
    [Timestamp]      ROWVERSION    NOT NULL,
    CONSTRAINT [PK_ForeclousureDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ForeclousureDetail_Foreclousure] FOREIGN KEY ([IdForeclousure]) REFERENCES [Payroll].[Foreclousure] ([Id])
);


GO
ALTER TABLE [Payroll].[ForeclousureDetail] NOCHECK CONSTRAINT [FK_ForeclousureDetail_Foreclousure];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'Timestamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'StateShare';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Pago:   1. Por Nomina  2. Manual  3. Por archivo  4. Por Vacaciones  5. Por Cesantias  6. Por Primas 1er Semestre  7. Por Primas 2do Semestre', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'TypePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Pago', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'DatePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la Cuota', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'ShareValuePaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cabecera Embargos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'IdForeclousure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'Id';

