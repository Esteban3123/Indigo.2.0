CREATE TABLE [Portfolio].[EconomicIndicator] (
    [Id]                  INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20)   NOT NULL,
    [Year]                VARCHAR (4)    NOT NULL,
    [Month]               VARCHAR (2)    NOT NULL,
    [FinancialInterests]  DECIMAL (5, 2) NOT NULL,
    [LatePaymentInterest] DECIMAL (5, 2) NOT NULL,
    [Status]              BIT            CONSTRAINT [DF_EconomicIndicator_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]        VARCHAR (20)   NOT NULL,
    [CreationDate]        DATETIME       NOT NULL,
    [ModificationUser]    VARCHAR (20)   NULL,
    [ModificationDate]    DATETIME       NULL,
    [TimeStamp]           ROWVERSION     NOT NULL,
    CONSTRAINT [PK_EconomicIndicator__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_EconomicIndicator__Code]
    ON [Portfolio].[EconomicIndicator]([Code] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_EconomicIndicator__Year__Month]
    ON [Portfolio].[EconomicIndicator]([Year] ASC, [Month] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'EconomicIndicator', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'EconomicIndicator', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'EconomicIndicator', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'EconomicIndicator', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'EconomicIndicator', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro: True-Activo, False-Inactivo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'EconomicIndicator', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Interés de pago atrasado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'EconomicIndicator', @level2type = N'COLUMN', @level2name = N'LatePaymentInterest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de interes financiero', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'EconomicIndicator', @level2type = N'COLUMN', @level2name = N'FinancialInterests';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes del indicador', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'EconomicIndicator', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año del indicador', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'EconomicIndicator', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del indicador económico.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'EconomicIndicator', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'EconomicIndicator', @level2type = N'COLUMN', @level2name = N'Id';

