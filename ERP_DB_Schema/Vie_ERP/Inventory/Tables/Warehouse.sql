CREATE TABLE [Inventory].[Warehouse] (
    [Id]                            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                          VARCHAR (20)  NOT NULL,
    [Name]                          VARCHAR (100) NOT NULL,
    [Prefix]                        VARCHAR (4)   NOT NULL,
    [CostCenterId]                  INT           NOT NULL,
    [LoanThirdPartyDebitAccountId]  INT           NOT NULL,
    [LoanThirdPartyCreditAccountId] INT           NOT NULL,
    [Status]                        BIT           NOT NULL,
    [CreationUser]                  VARCHAR (20)  CONSTRAINT [DF_Warehouse_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                  DATETIME      CONSTRAINT [DF_Warehouse_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]              VARCHAR (20)  NULL,
    [ModificationDate]              DATETIME      NULL,
    [VirtualStore]                  BIT           CONSTRAINT [DF_Warehouse_VirtualStore] DEFAULT ((0)) NOT NULL,
    [SupplierId]                    INT           CONSTRAINT [DF_Warehouse_SupplierId] DEFAULT ((5129)) NOT NULL,
    [WarehouseConsignment]          BIT           CONSTRAINT [DF__Warehouse__Wareh__0FCAFA68] DEFAULT ((0)) NOT NULL,
    [CustodyStore]                  BIT           CONSTRAINT [DF_Warehouse_CustodyStore] DEFAULT ((0)) NOT NULL,
    [CodeCenterAttention]           CHAR (10)     NULL,
    [TransitStore]                  BIT           CONSTRAINT [DF_Warehouse_TransitStore] DEFAULT ((0)) NOT NULL,
    [ControlStore]                  BIT           CONSTRAINT [DF_Warehouse_ControlStore] DEFAULT ((0)) NOT NULL,
    [WareHouseType]                 TINYINT       CONSTRAINT [DF__Warehouse__WareH__7DD4055A] DEFAULT ((0)) NOT NULL,
    [HandleRestrictedProducts]      BIT           CONSTRAINT [DF_Warehouse_HandleRestrictedProducts] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Warehouse__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Warehouse_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_Warehouse_MainAccounts] FOREIGN KEY ([LoanThirdPartyDebitAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_Warehouse_MainAccounts1] FOREIGN KEY ([LoanThirdPartyCreditAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_Warehouse_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id])
);


GO
ALTER TABLE [Inventory].[Warehouse] NOCHECK CONSTRAINT [FK_Warehouse_CostCenter];


GO
ALTER TABLE [Inventory].[Warehouse] NOCHECK CONSTRAINT [FK_Warehouse_MainAccounts];


GO
ALTER TABLE [Inventory].[Warehouse] NOCHECK CONSTRAINT [FK_Warehouse_MainAccounts1];


GO
ALTER TABLE [Inventory].[Warehouse] NOCHECK CONSTRAINT [FK_Warehouse_Supplier];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Warehouse__Code]
    ON [Inventory].[Warehouse]([Code] ASC);


GO
CREATE TRIGGER [Inventory].[tgWarehouseUpdate]
   ON  [Inventory].[Warehouse]
   AFTER UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

    IF UPDATE(Prefix)
	BEGIN
		INSERT INTO [Common].[Log]
           ([TableSchema], [TableName], [Key], [ColumnName], [OldValue], [NewValue], [UpdatedBy], [UpdatedDate])
		SELECT 'Inventory', 'Warehouse', i.Id, 'Prefix', d.Prefix, i.Prefix, i.ModificationUser, GETDATE()
		FROM INSERTED i
		JOIN DELETED d ON i.Id = d.Id
		WHERE ISNULL(d.Prefix, '') <> ISNULL(i.Prefix, '')
	END

	IF UPDATE(CostCenterId)
	BEGIN
		INSERT INTO [Common].[Log]
           ([TableSchema], [TableName], [Key], [ColumnName], [OldValue], [NewValue], [UpdatedBy], [UpdatedDate])
		SELECT 'Inventory', 'Warehouse', i.Id, 'CostCenterId', d.CostCenterId, i.CostCenterId, i.ModificationUser, GETDATE()
		FROM INSERTED i
		JOIN DELETED d ON i.Id = d.Id
		WHERE ISNULL(d.CostCenterId, 0) <> ISNULL(i.CostCenterId, 0)
	END

	IF UPDATE(LoanThirdPartyDebitAccountId)
	BEGIN
		INSERT INTO [Common].[Log]
           ([TableSchema], [TableName], [Key], [ColumnName], [OldValue], [NewValue], [UpdatedBy], [UpdatedDate])
		SELECT 'Inventory', 'Warehouse', i.Id, 'LoanThirdPartyDebitAccountId', d.LoanThirdPartyDebitAccountId, i.LoanThirdPartyDebitAccountId, i.ModificationUser, GETDATE()
		FROM INSERTED i
		JOIN DELETED d ON i.Id = d.Id
		WHERE ISNULL(d.LoanThirdPartyDebitAccountId, 0) <> ISNULL(i.LoanThirdPartyDebitAccountId, 0)
	END

	IF UPDATE(LoanThirdPartyCreditAccountId)
	BEGIN
		INSERT INTO [Common].[Log]
           ([TableSchema], [TableName], [Key], [ColumnName], [OldValue], [NewValue], [UpdatedBy], [UpdatedDate])
		SELECT 'Inventory', 'Warehouse', i.Id, 'LoanThirdPartyCreditAccountId', d.LoanThirdPartyCreditAccountId, i.LoanThirdPartyCreditAccountId, i.ModificationUser, GETDATE()
		FROM INSERTED i
		JOIN DELETED d ON i.Id = d.Id
		WHERE ISNULL(d.LoanThirdPartyCreditAccountId, 0) <> ISNULL(i.LoanThirdPartyCreditAccountId, 0)
	END

	IF UPDATE(VirtualStore)
	BEGIN
		INSERT INTO [Common].[Log]
           ([TableSchema], [TableName], [Key], [ColumnName], [OldValue], [NewValue], [UpdatedBy], [UpdatedDate])
		SELECT 'Inventory', 'Warehouse', i.Id, 'VirtualStore', d.VirtualStore, i.VirtualStore, i.ModificationUser, GETDATE()
		FROM INSERTED i
		JOIN DELETED d ON i.Id = d.Id
		WHERE ISNULL(d.VirtualStore, 0) <> ISNULL(i.VirtualStore, 0)
	END

	IF UPDATE(Status)
	BEGIN
		INSERT INTO [Common].[Log]
           ([TableSchema], [TableName], [Key], [ColumnName], [OldValue], [NewValue], [UpdatedBy], [UpdatedDate])
		SELECT 'Inventory', 'Warehouse', i.Id, 'Status', d.Status, i.Status, i.ModificationUser, GETDATE()
		FROM INSERTED i
		JOIN DELETED d ON i.Id = d.Id
		WHERE ISNULL(d.Status, 0) <> ISNULL(i.Status, 0)
	END

	IF UPDATE(SupplierId)
	BEGIN
		INSERT INTO [Common].[Log]
           ([TableSchema], [TableName], [Key], [ColumnName], [OldValue], [NewValue], [UpdatedBy], [UpdatedDate])
		SELECT 'Inventory', 'Warehouse', i.Id, 'SupplierId', d.SupplierId, i.SupplierId, i.ModificationUser, GETDATE()
		FROM INSERTED i
		JOIN DELETED d ON i.Id = d.Id
		WHERE ISNULL(d.SupplierId, 0) <> ISNULL(i.SupplierId, 0)
	END

	IF UPDATE(WarehouseConsignment)
	BEGIN
		INSERT INTO [Common].[Log]
           ([TableSchema], [TableName], [Key], [ColumnName], [OldValue], [NewValue], [UpdatedBy], [UpdatedDate])
		SELECT 'Inventory', 'Warehouse', i.Id, 'WarehouseConsignment', d.WarehouseConsignment, i.WarehouseConsignment, i.ModificationUser, GETDATE()
		FROM INSERTED i
		JOIN DELETED d ON i.Id = d.Id
		WHERE ISNULL(d.WarehouseConsignment, 0) <> ISNULL(i.WarehouseConsignment, 0)
	END

	IF UPDATE(TransitStore)
	BEGIN
		INSERT INTO [Common].[Log]
           ([TableSchema], [TableName], [Key], [ColumnName], [OldValue], [NewValue], [UpdatedBy], [UpdatedDate])
		SELECT 'Inventory', 'Warehouse', i.Id, 'TransitStore', d.TransitStore, i.TransitStore, i.ModificationUser, GETDATE()
		FROM INSERTED i
		JOIN DELETED d ON i.Id = d.Id
		WHERE ISNULL(d.TransitStore, 0) <> ISNULL(i.TransitStore, 0)
	END

	IF UPDATE(ControlStore)
	BEGIN
		INSERT INTO [Common].[Log]
           ([TableSchema], [TableName], [Key], [ColumnName], [OldValue], [NewValue], [UpdatedBy], [UpdatedDate])
		SELECT 'Inventory', 'Warehouse', i.Id, 'ControlStore', d.ControlStore, i.ControlStore, i.ModificationUser, GETDATE()
		FROM INSERTED i
		JOIN DELETED d ON i.Id = d.Id
		WHERE ISNULL(d.ControlStore, 0) <> ISNULL(i.ControlStore, 0)
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el almacen tiene restricciones por productos, es decir que solo maneja algunos productos en especfico. Cuando este campo esta en 1 entonces se llena la tabla WarehouseRestrictedConditions', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'HandleRestrictedProducts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de almacen :                  (0, "Ninguna"))                  (1, "Almacén Virtual"))                  (2, "Almacén de Consignación"))                  (3, "Almacén de Custodia"))                  (4, "Almacén de Transito"))                  (5, "Almacén de Control"))                  (6, "Almacén de Remanentes"))', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'WareHouseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que indica si es un almacén de Control', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'ControlStore';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que indica si es un almacén en tránsito de forma tal que solo se se visualice en las ordenes de traslado de tipo Traslado en Tránsito', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'TransitStore';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CodeCenterAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el almacen es de custodia o no (almacena medicamentos que el paciente trae)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CustodyStore';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que indica si el almacén es para inventario en consignación de forma tal que solo se tenga en cuenta para transacciones que impliquen movimientos de este tipo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'WarehouseConsignment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Proveedor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que indica si el almacén es virtual o no, de forma tal que se tenga en cuenta para modificar el inventario físico o generar movimientos contables', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'VirtualStore';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento  1 - Activado  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta credito de prestamo a terceros', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'LoanThirdPartyCreditAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta debito de prestamo a terceros', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'LoanThirdPartyDebitAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el prefijo que se deberia usar cuando se realiza un recibo de caja o un egreso con la caja especifica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Prefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Id';

