CREATE TABLE [Inventory].[EntranceVoucher] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                       VARCHAR (20)    NOT NULL,
    [OperatingUnitId]            INT             NOT NULL,
    [DocumentDate]               DATE            NOT NULL,
    [SupplierId]                 INT             NOT NULL,
    [SupplierDistributionLineId] INT             NOT NULL,
    [SupplierTypeId]             INT             NOT NULL,
    [WarehouseId]                INT             NOT NULL,
    [AccountPayableId]           INT             NULL,
    [Description]                VARCHAR (500)   NOT NULL,
    [ResourceType]               TINYINT         NOT NULL,
    [RoundService]               INT             NOT NULL,
    [IcaPercentage]              NUMERIC (5, 3)  NOT NULL,
    [InvoiceNumber]              VARCHAR (100)   NOT NULL,
    [InvoiceDate]                DATETIME        NOT NULL,
    [DayPeriod]                  INT             NOT NULL,
    [FreightValue]               DECIMAL (18, 2) NOT NULL,
    [FreightValueOutstanding]    DECIMAL (18, 2) NOT NULL,
    [FreightIVAPercentage]       NUMERIC (5, 2)  NOT NULL,
    [FreightIVAValue]            DECIMAL (18, 2) NOT NULL,
    [FreightIVAValueOutstanding] DECIMAL (18, 2) NOT NULL,
    [Value]                      DECIMAL (18, 2) NOT NULL,
    [ValueDiscount]              DECIMAL (18, 2) NOT NULL,
    [ValueTax]                   DECIMAL (18, 2) NOT NULL,
    [WithholdingTax]             DECIMAL (18, 2) NOT NULL,
    [WithholdingICA]             DECIMAL (18, 2) NOT NULL,
    [RetentionSource]            DECIMAL (18, 2) NOT NULL,
    [RetentionOther]             DECIMAL (18, 2) NOT NULL,
    [DeductionOther]             DECIMAL (18, 2) NOT NULL,
    [DistrictTax]                DECIMAL (18, 2) NOT NULL,
    [TotalValue]                 DECIMAL (18, 2) NOT NULL,
    [Status]                     TINYINT         NOT NULL,
    [CreationUser]               VARCHAR (20)    NOT NULL,
    [CreationDate]               DATETIME        NOT NULL,
    [ModificationUser]           VARCHAR (20)    NULL,
    [ModificationDate]           DATETIME        NULL,
    [ConfirmationUser]           VARCHAR (20)    NULL,
    [ConfirmationDate]           DATETIME        NULL,
    [AnnulmentUser]              VARCHAR (20)    NULL,
    [AnnulmentDate]              DATETIME        NULL,
    [TimeStamp]                  ROWVERSION      NOT NULL,
    [CommitmentDetailId]         INT             NULL,
    [DocumentSupportId]          INT             NULL,
    [TaxRegistration]            TINYINT         CONSTRAINT [DF__EntranceV__TaxRe__1A9B2C91] DEFAULT ((1)) NOT NULL,
    [CurrencyId]                 INT             CONSTRAINT [DF__EntranceV__Curre__2696EF22] DEFAULT ((1)) NOT NULL,
    [EconomicActivityId]         INT             NULL,
    CONSTRAINT [PK_EntranceVoucher] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_EntranceVoucher_TotalValue] CHECK ([TotalValue]=(((((((((([Value]+[ValueTax])-[ValueDiscount])-[WithholdingTax])-[WithholdingICA])-[RetentionSource])-[RetentionOther])-[DeductionOther])-[DistrictTax])+[FreightValue])+[FreightIVAValue])),
    CONSTRAINT [FK_EntranceVoucher_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_EntranceVoucher_CommitmentDetail] FOREIGN KEY ([CommitmentDetailId]) REFERENCES [Budget].[CommitmentDetail] ([Id]),
    CONSTRAINT [FK_EntranceVoucher_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_EntranceVoucher_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_EntranceVoucher_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_EntranceVoucher_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_EntranceVoucher_SuppliersDistributionLines] FOREIGN KEY ([SupplierDistributionLineId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_EntranceVoucher_SupplierType] FOREIGN KEY ([SupplierTypeId]) REFERENCES [Common].[SupplierType] ([Id]),
    CONSTRAINT [FK_EntranceVoucher_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [CK_EntranceVoucher_TotalValue];


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [FK_EntranceVoucher_AccountPayable];


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [FK_EntranceVoucher_CommitmentDetail];


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [FK_EntranceVoucher_Currency];


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [FK_EntranceVoucher_OperatingUnit];


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [FK_EntranceVoucher_Supplier];


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [FK_EntranceVoucher_SuppliersDistributionLines];


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [FK_EntranceVoucher_SupplierType];


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [FK_EntranceVoucher_Warehouse];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_EntranceVoucher__Code]
    ON [Inventory].[EntranceVoucher]([Code] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EntranceVoucher__Status__DocumentDate__INC__Id]
    ON [Inventory].[EntranceVoucher]([Status] ASC, [DocumentDate] ASC)
    INCLUDE([Id]);


GO

-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-07-11
-- Description:	Validar que el % de retención de ICA sea el correspondiente al proveedor seleccionado
-- =============================================
CREATE TRIGGER [Inventory].[tgg_ValidateIcaPercentage]
   ON  [Inventory].[EntranceVoucher]
   AFTER INSERT, UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here

	IF EXISTS 
	(
		SELECT *
		FROM INSERTED ev
		JOIN Common.Supplier s ON ev.SupplierId = s.Id
		JOIN Common.ThirdParty tp ON s.IdThirdParty = tp.Id
		JOIN Common.SuppliersDistributionLines sdl ON ev.SupplierDistributionLineId = sdl.Id		
		JOIN Common.DistributionLinesICARetention dlir ON ev.OperatingUnitId = dlir.OperatingUnitId AND sdl.IdDistributionLine = dlir.DistributionLineId
		JOIN Payments.AccountPayableConcepts apc ON dlir.AccountPayableConceptId = apc.Id
		LEFT JOIN GeneralLedger.RetentionConcepts rc ON apc.RetentionConceptId = rc.Id
		WHERE tp.RetentionType = 2
			AND tp.Ica = 1
			AND s.SelfWithholdingICA = 0
			AND ISNULL(rc.Rate, 0) <> ev.IcaPercentage
	)
	BEGIN
		THROW 51000, 'Error generado por control desde trigger. El porcentaje de Ica del registro no corresponde con el definido en la linea de distribución', 1
	END

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica como se va a llevar el Registro del IVA  1 - IVA al Costo  2 - IVA Descontable', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'TaxRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica como se va redondear los valores del contrato  1 -- Peso  10 -- Decima  100 -- Centesima  1000 -- Milesima    La formula que se usa es Round(valor / redondeo) * redondeo  Round(2005/ 10) * 10', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'RoundService';

