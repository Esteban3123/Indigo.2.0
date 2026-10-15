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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Pago:   1. Por Nomina  2. Manual  3. Por archivo  4. Por Vacaciones  5. Por Cesantias  6. Por Primas 1er Semestre  7. Por Primas 2do Semestre', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'TypePayment';

