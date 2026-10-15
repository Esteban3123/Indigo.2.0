CREATE TABLE [FixedAsset].[ValorizationDevaluation] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)  NOT NULL,
    [DocumentDate]         DATE          NOT NULL,
    [DocumentType]         TINYINT       NOT NULL,
    [Detail]               VARCHAR (100) NOT NULL,
    [PhysicalFixedAssetId] INT           NOT NULL,
    [ThirdPartyId]         INT           NULL,
    [CostCenterId]         INT           NULL,
    [AccountingAccountId]  INT           NULL,
    [RegisterValue]        NUMERIC (18)  NOT NULL,
    [AppraisedValue]       NUMERIC (18)  NOT NULL,
    [ResponsibleId]        INT           NOT NULL,
    [HistoricalCost]       NUMERIC (18)  NOT NULL,
    [Depreciation]         NUMERIC (18)  NOT NULL,
    [CumulatedAdjustment]  NUMERIC (18)  NOT NULL,
    [CarryingAmount]       NUMERIC (18)  NOT NULL,
    [Status]               TINYINT       NOT NULL,
    [CreationUser]         VARCHAR (20)  NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [AnnulmentUser]        VARCHAR (20)  NULL,
    [AnnulmentDate]        DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    CONSTRAINT [PK_ValorizationDevaluation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ValorizationDevaluation_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ValorizationDevaluation_FixedAssetResponsible] FOREIGN KEY ([ResponsibleId]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id]),
    CONSTRAINT [FK_ValorizationDevaluation_MainAccounts] FOREIGN KEY ([AccountingAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ValorizationDevaluation_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [FixedAsset].[ValorizationDevaluation] NOCHECK CONSTRAINT [FK_ValorizationDevaluation_CostCenter];


GO
ALTER TABLE [FixedAsset].[ValorizationDevaluation] NOCHECK CONSTRAINT [FK_ValorizationDevaluation_FixedAssetResponsible];


GO
ALTER TABLE [FixedAsset].[ValorizationDevaluation] NOCHECK CONSTRAINT [FK_ValorizationDevaluation_MainAccounts];


GO
ALTER TABLE [FixedAsset].[ValorizationDevaluation] NOCHECK CONSTRAINT [FK_ValorizationDevaluation_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Anulación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Anulación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado (registrado = 1,confirmado = 2,anulado = 3)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor en Libros', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'CarryingAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ajuste Acumulado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'CumulatedAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'Depreciation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo Histórico', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'HistoricalCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Responsable de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'ResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del Avaluo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'AppraisedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor a Registrar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'RegisterValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Contable con que se realiza la Valorización o Desvalorización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'AccountingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si la Cuenta Contable de Valorización o Desvalorización maneja Centros de Costo, se agrega', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si la Cuenta Contable de Valorización o Desvalorización maneja Terceros, se agrega', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Activo Fijo Físico', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'PhysicalFixedAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle de la Valorización o Desvalorización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1=Valorización, 2=Desvalorización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Documento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la Valorización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Valorización o Desvalorización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'ValorizationDevaluation', @level2type = N'COLUMN', @level2name = N'Id';

