CREATE TABLE [Portfolio].[ReportPortfolioByAgeDetail] (
    [IdIdentity]                   INT             IDENTITY (1, 1) NOT NULL,
    [Id]                           INT             NOT NULL,
    [IdReportPortfolioByAge]       INT             NOT NULL,
    [OrderBy]                      VARCHAR (MAX)   NULL,
    [OrderByAdvanceAtEnd]          INT             NOT NULL,
    [DocumentCode]                 VARCHAR (20)    NOT NULL,
    [AccountReceivableDate]        DATETIME        NOT NULL,
    [AccountReceivableType]        INT             NOT NULL,
    [PortfolioStatus]              INT             NOT NULL,
    [PortfolioStatusName]          VARCHAR (25)    NULL,
    [NumberShares]                 INT             NOT NULL,
    [Term]                         INT             NOT NULL,
    [OpeningBalance]               INT             NOT NULL,
    [ThirdPartyNit]                VARCHAR (25)    NOT NULL,
    [ThirdPartyName]               VARCHAR (300)   NOT NULL,
    [IdentificationType]           INT             NOT NULL,
    [Category]                     VARCHAR (123)   NULL,
    [Regimen]                      VARCHAR (50)    NULL,
    [CareGroupCode]                VARCHAR (20)    NULL,
    [CareGroupName]                VARCHAR (100)   NULL,
    [ContractCode]                 VARCHAR (20)    NULL,
    [ContractName]                 VARCHAR (100)   NULL,
    [AccountWithoutRadicateNumber] VARCHAR (50)    NULL,
    [RadicatedConsecutive]         INT             NULL,
    [RadicatedUser]                VARCHAR (20)    NULL,
    [RadicatedDate]                DATETIME        NULL,
    [DocumentDate]                 DATETIME        NULL,
    [RadicatedState]               CHAR (1)        NULL,
    [MainAccountNumber]            VARCHAR (50)    NULL,
    [MainAccountName]              VARCHAR (100)   NULL,
    [DocumentValue]                NUMERIC (18, 2) NOT NULL,
    [RetentionValue]               NUMERIC (38, 2) NULL,
    [InitialValue]                 NUMERIC (38, 2) NULL,
    [DebitValue]                   NUMERIC (38, 2) NULL,
    [CreditValue]                  NUMERIC (38, 2) NULL,
    [TransferValue]                DECIMAL (38, 2) NOT NULL,
    [CashReceiptValue]             DECIMAL (38, 2) NOT NULL,
    [CrossingValue]                DECIMAL (38, 2) NOT NULL,
    [Balance]                      NUMERIC (38, 2) NULL,
    [CurrentBalance]               NUMERIC (18, 2) NOT NULL,
    [CenterAttention]              CHAR (100)      NULL,
    [ExpiredDate]                  DATETIME        NULL,
    [Age]                          INT             NULL,
    [RegimenCalculated]            VARCHAR (MAX)   NULL,
    [ValueGlosado]                 MONEY           NOT NULL,
    [ValueAcceptedFirstInstance]   MONEY           NOT NULL,
    [ValueAcceptedSecondInstance]  MONEY           NOT NULL,
    [GlosaState]                   TINYINT         NULL,
    [GlosaStateName]               VARCHAR (35)    NULL,
    CONSTRAINT [PK_ReportPortfolioByAgeDetail] PRIMARY KEY CLUSTERED ([IdIdentity] ASC),
    CONSTRAINT [FK_ReportPortfolioByAgeDetail_ReportPortfolioByAge] FOREIGN KEY ([IdReportPortfolioByAge]) REFERENCES [Portfolio].[ReportPortfolioByAge] ([Id])
);


GO
ALTER TABLE [Portfolio].[ReportPortfolioByAgeDetail] NOCHECK CONSTRAINT [FK_ReportPortfolioByAgeDetail_ReportPortfolioByAge];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el nombre del estado de la glosa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'GlosaStateName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el estado de la glosa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'GlosaState';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor aceptado en segunda instancia.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ValueAcceptedSecondInstance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor aceptado en primera instancia.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ValueAcceptedFirstInstance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el valor glosado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ValueGlosado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el régimen calculado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RegimenCalculated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el periodo del detalle de la cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Age';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de caducidad.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el centro de atención.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CenterAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el saldo actual.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CurrentBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el saldo del detalle de la cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el valor del cruce.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CrossingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el valor de efectivo del recibo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CashReceiptValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del cruce de anticipo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'TransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el valor del crédito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el valor del débito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el valor inicial.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el valor de retención.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RetentionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el valor del documento.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'DocumentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la cuenta principal establecida.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'MainAccountName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de la cuenta principal establecida.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'MainAccountNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el estado del radicado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RadicatedState';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena la fecha del documento.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la fecha del radicado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el codigo de usuario que realizó la radicación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RadicatedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el número del consecutivo del radicado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'RadicatedConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la cuenta sin número de radicado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'AccountWithoutRadicateNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el nombre del contrato.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ContractName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el código del contrato.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ContractCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del grupo de atención.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CareGroupName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del grupo de atención.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'CareGroupCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el régimen.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Regimen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece la categoria del detalle.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Category';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el tipo de identificación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'IdentificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del tercero asociado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nit del tercero asociado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyNit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el saldo inicial.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'OpeningBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el término del detalle en el reporte.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Term';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece número de recursos compartidos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'NumberShares';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el nombre del estado del portfolio.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'PortfolioStatusName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el estado del portfolio 1 - Activo, 0 - Inactivo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el tipo de la cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la fecha de la cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el código del documento.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'DocumentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el número del anticipo al final por el que se desea agrupar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'OrderByAdvanceAtEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el campo por el que se desea agrupar los datos del reporte.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'OrderBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del informe de cartera por edades.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'IdReportPortfolioByAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la identificación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAgeDetail', @level2type = N'COLUMN', @level2name = N'IdIdentity';

