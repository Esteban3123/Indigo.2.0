CREATE TABLE [Payments].[AccountPayable] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                         VARCHAR (20)    NOT NULL,
    [NumberFiling]                 BIGINT          CONSTRAINT [DF_AccountPayable_NumberFiling] DEFAULT ((1)) NOT NULL,
    [EntityId]                     INT             NULL,
    [EntityCode]                   VARCHAR (20)    NULL,
    [EntityName]                   VARCHAR (250)   NULL,
    [IdSupplier]                   INT             NOT NULL,
    [IdThirdParty]                 INT             NULL,
    [IdAccount]                    INT             NOT NULL,
    [IdCostCenter]                 INT             NULL,
    [BillNumber]                   VARCHAR (100)   NOT NULL,
    [BillDate]                     DATETIME        NOT NULL,
    [DocumentDate]                 DATETIME        NOT NULL,
    [ServicePeriodDate]            DATETIME        CONSTRAINT [DF_AccountPayable_ServicePeriodDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [FilingUnitId]                 INT             NOT NULL,
    [SupplierTypeId]               INT             NOT NULL,
    [Term]                         INT             NOT NULL,
    [ExpirationDate]               DATETIME        NOT NULL,
    [Coments]                      VARCHAR (MAX)   NULL,
    [Status]                       TINYINT         NOT NULL,
    [InitialBalance]               BIT             NOT NULL,
    [IdInitialBalance]             INT             NULL,
    [PreviousBudget]               BIT             NOT NULL,
    [Shares]                       INT             NOT NULL,
    [InvoiceValue]                 DECIMAL (18, 2) CONSTRAINT [DF_AccountPayable_ValueInvoice] DEFAULT ((0.0)) NOT NULL,
    [Value]                        DECIMAL (18, 2) NOT NULL,
    [Balance]                      DECIMAL (18, 2) NOT NULL,
    [IdOperatingUnit]              INT             NULL,
    [IdSuppliersDistributionLines] INT             NULL,
    [CostDistributionDirectCostId] INT             NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    [ModificationUser]             VARCHAR (20)    NULL,
    [ModificationDate]             DATETIME        NULL,
    [ConfirmationUser]             VARCHAR (20)    NULL,
    [ConfirmationDate]             DATETIME        NULL,
    [AnnulmentUser]                VARCHAR (20)    NULL,
    [AnnulmentDate]                DATETIME        NULL,
    [TimeStamp]                    ROWVERSION      NOT NULL,
    [PositionId]                   INT             NULL,
    [Hours]                        INT             CONSTRAINT [DF_AccountPayable_Hours] DEFAULT ((0)) NOT NULL,
    [CommitmentDetailId]           INT             NULL,
    [HandlesDocumentSupport]       BIT             CONSTRAINT [DF__AccountPa__Handl__68775F13] DEFAULT ((0)) NOT NULL,
    [DocumentSupportId]            INT             NULL,
    [DeductibleIva]                BIT             NULL,
    [CurrencyId]                   INT             CONSTRAINT [DF__AccountPa__Curre__7CB5C3EF] DEFAULT ((1)) NULL,
    [TRMValue]                     NUMERIC (20, 5) CONSTRAINT [DF__AccountPa__TRMVa__7E9E0C61] DEFAULT ((1)) NOT NULL,
    [TaxRegistration]              TINYINT         NULL,
    [IdEconomicActivity]           INT             NULL,
    CONSTRAINT [PK_AccountPayable__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayable_Commitment] FOREIGN KEY ([CommitmentDetailId]) REFERENCES [Budget].[CommitmentDetail] ([Id]),
    CONSTRAINT [FK_AccountPayable_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_AccountPayable_CostDistributionDirectCost] FOREIGN KEY ([CostDistributionDirectCostId]) REFERENCES [Cost].[CostDistributionDirectCost] ([Id]),
    CONSTRAINT [FK_AccountPayable_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_AccountPayable_EconomicActivity] FOREIGN KEY ([IdEconomicActivity]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_AccountPayable_FilingUnit] FOREIGN KEY ([FilingUnitId]) REFERENCES [Payments].[FilingUnit] ([Id]),
    CONSTRAINT [FK_AccountPayable_InitialBalance] FOREIGN KEY ([IdInitialBalance]) REFERENCES [Payments].[InitialBalance] ([Id]),
    CONSTRAINT [FK_AccountPayable_MainAccounts] FOREIGN KEY ([IdAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountPayable_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_AccountPayable_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id]),
    CONSTRAINT [FK_AccountPayable_Supplier] FOREIGN KEY ([IdSupplier]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_AccountPayable_SuppliersDistributionLines] FOREIGN KEY ([IdSuppliersDistributionLines]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_AccountPayable_SupplierType] FOREIGN KEY ([SupplierTypeId]) REFERENCES [Common].[SupplierType] ([Id]),
    CONSTRAINT [FK_AccountPayable_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_Commitment];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_CostCenter];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_CostDistributionDirectCost];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_Currency];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_EconomicActivity];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_FilingUnit];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_InitialBalance];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_MainAccounts];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_OperatingUnit];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_Position];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_Supplier];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_SuppliersDistributionLines];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_SupplierType];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_ThirdParty];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AccountPayable__IdSupplier__BillNumber_Status]
    ON [Payments].[AccountPayable]([IdSupplier] ASC, [BillNumber] ASC, [Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AccountPayable_Code]
    ON [Payments].[AccountPayable]([Code] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AccountPayable__Status__IdSuppliersDistributionLines__BillDate__INC__BillNumber__Code__CreationUser__EntityCode__EntityName__]
    ON [Payments].[AccountPayable]([Status] ASC, [IdSuppliersDistributionLines] ASC, [BillDate] ASC)
    INCLUDE([BillNumber], [Code], [CreationUser], [EntityCode], [EntityName], [Id], [IdAccount], [IdCostCenter], [IdOperatingUnit], [IdSupplier], [InitialBalance], [NumberFiling], [ServicePeriodDate], [Shares], [Term], [Value]);


GO
CREATE NONCLUSTERED INDEX [IX_AccountPayable__IdAccount__Status__INC__Id__IdSupplier]
    ON [Payments].[AccountPayable]([IdAccount] ASC, [Status] ASC)
    INCLUDE([Id], [IdSupplier]);


GO

-- =============================================
-- Author:		Cristhian Mauricio Salazar Narvaez
-- Create date: 30/09/2016
-- Description:	Trigger para validar el espacio que se genera en algunas ocasiones en el codigo de la cuenta por cobrar
-- =============================================
create TRIGGER [Payments].[TriggerValidateSpaceCxP]
   ON [Payments].[AccountPayable]
   AFTER INSERT, UPDATE
AS 
BEGIN
	if (select count(*) from inserted where Code like '% %') > 0 begin
		THROW 51000, 'Error el Codigo no puede tener espacios', 1
	end
END
GO
CREATE TRIGGER [Payments].[tgg_ValidateDuplicates]
   ON  [Payments].[AccountPayable] 
   AFTER INSERT, UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF EXISTS (
		SELECT 1
		FROM [Payments].[AccountPayable] ap
		JOIN INSERTED ap2 ON ap.Code = ap2.Code AND ap.id < ap2.id
		WHERE ap.IdSupplier <> ap2.IdSupplier or ap.IdSuppliersDistributionLines <> ap2.IdSuppliersDistributionLines
	)
	BEGIN
		THROW 51000, 'Error generado por control de actualización desde trigger. No puede pasar que hayan una cuenta por pagar para dos proveedores diferentes', 1
	END 
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la actividad economica asociada al registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'IdEconomicActivity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro fiscal', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'TaxRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TRM De la transaccion de la moneda', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'TRMValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la moneda de la caja o cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Iva deductible', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'DeductibleIva';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la resolución de Documento soporte relacionado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'DocumentSupportId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0 = No maneja documento soporte  1 = Maneja Documento Soporte', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'HandlesDocumentSupport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En desuso por actualización.  Ahora se usará el Id del compromiso será definido en AccountPayableCommitments debido a que una factura puede tener múltiples rubros presupuestales asociados.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'CommitmentDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se indican las Horas laboradas  a fin de realizar correctamente la distribución del costo por actividades', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'Hours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la posicion asociada a la linea de distribución cuando este sea un proveedor independiente y la linea de distribución maneje cargos.  Además se habilita el campo Horas Laboradas', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la distribución de elementos del costo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'CostDistributionDirectCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Proveedores Líneas de distribución', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'IdSuppliersDistributionLines';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor de la factura de la cuenta por pagar, Este campo por lo general se llena en la radicacion de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'InvoiceValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de esta cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'Shares';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si la cuenta por pagar es presupuestalmente de una vigencia anterior', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'PreviousBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Saldo inicial', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'IdInitialBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si es un saldo inicial', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'InitialBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estados:  1. Registrado  2. Confirmado  3. Anulado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'Coments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de vencimineto', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'ExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plazo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'Term';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de proveedor, este se obtiene de la tabla SupplierDetailType pero en esta tabla solo estan los padres y en este campos solo se pueden seleccionar los hijos de ultimo nivel de esos registros, es decir que el tipo de proveedor que seleccionen debe estar en el ultimo nivel en la jerarquia', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'SupplierTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de radicacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'FilingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha del periodo del servicio, este campos se utuliza para reportes', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'ServicePeriodDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'BillDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'BillNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'IdAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'IdSupplier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Radicado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'NumberFiling';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la cuenta', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'Id';

