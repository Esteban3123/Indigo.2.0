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



GO



GO



GO



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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si el almacén maneja restricciones de productos específicos. Cuando es 1, solo ciertos productos están permitidos y se registran en WarehouseRestrictedConditions; cuando es 0, no hay restricciones.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'HandleRestrictedProducts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el almacen tiene restricciones por productos, es decir que solo maneja algunos productos en especfico. Cuando este campo esta en 1 entonces se llena la tabla WarehouseRestrictedConditions', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'HandleRestrictedProducts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'HandleRestrictedProducts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de almacén (TINYINT): 0=Ninguna, 1=Virtual, 2=Consignación, 3=Custodia, 4=Tránsito, 5=Control, 6=Remanentes. Define el comportamiento de movimientos y visibilidad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'WareHouseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de almacen :                  (0, "Ninguna"))                  (1, "Almacén Virtual"))                  (2, "Almacén de Consignación"))                  (3, "Almacén de Custodia"))                  (4, "Almacén de Transito"))                  (5, "Almacén de Control"))                  (6, "Almacén de Remanentes"))', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'WareHouseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'WareHouseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que señala si el almacén es de tipo Control, usado para auditoría y monitoreo de inventario de medicamentos y dispositivos médicos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'ControlStore';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que indica si es un almacén de Control', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'ControlStore';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'ControlStore';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que marca si el almacén es de tránsito, visible solo en órdenes de traslado tipo Traslado en Tránsito; facilita movimientos inter-sedes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'TransitStore';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que indica si es un almacén en tránsito de forma tal que solo se se visualice en las ordenes de traslado de tipo Traslado en Tránsito', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'TransitStore';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'TransitStore';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del centro de atención, clínica o unidad funcional (CHAR 10) asociada al almacén; clave para filtrar inventario por sede.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CodeCenterAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CodeCenterAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CodeCenterAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si el almacén es de custodia, almacenando medicamentos y dispositivos que el paciente o tercero aporta temporalmente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CustodyStore';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el almacen es de custodia o no (almacena medicamentos que el paciente trae)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CustodyStore';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CustodyStore';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que identifica si el almacén gestiona inventario en consignación de proveedores, considerándolo solo en transacciones de consignación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'WarehouseConsignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que indica si el almacén es para inventario en consignación de forma tal que solo se tenga en cuenta para transacciones que impliquen movimientos de este tipo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'WarehouseConsignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'WarehouseConsignment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a Common.Supplier) del proveedor principal o defecto asociado al almacén; por defecto 5129; permite vincular suministros.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Proveedor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que determina si el almacén es virtual (0=físico, 1=virtual), afectando ajustes de inventario físico y movimientos contables.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'VirtualStore';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que indica si el almacén es virtual o no, de forma tal que se tenga en cuenta para modificar el inventario físico o generar movimientos contables', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'VirtualStore';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'VirtualStore';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo (DATETIME) del último cambio registrado en el almacén; permite auditoría y trazabilidad de modificaciones.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20) del usuario que realizó la última modificación del registro del almacén; clave de auditoría.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro del almacén; generada automáticamente por Common.getdate().', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó el registro; por defecto 999 para sistema; auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del almacén (BIT): 1=Activo/Habilitado, 0=Inactivo/Deshabilitado; controla disponibilidad para transacciones.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Documento  1 - Activado  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a GeneralLedger.MainAccounts) de la cuenta contable de crédito para préstamos a terceros; asiento de pasivo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'LoanThirdPartyCreditAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta credito de prestamo a terceros', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'LoanThirdPartyCreditAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'LoanThirdPartyCreditAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a GeneralLedger.MainAccounts) de la cuenta contable de débito para préstamos a terceros; asiento de activo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'LoanThirdPartyDebitAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta debito de prestamo a terceros', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'LoanThirdPartyDebitAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'LoanThirdPartyDebitAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a Payroll.CostCenter) del centro de costo asociado; permite imputación presupuestaria y análisis financiero.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prefijo alfanumérico (VARCHAR 4) usado en numeración de recibos de caja, egresos y movimientos generados desde este almacén.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Prefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el prefijo que se deberia usar cuando se realiza un recibo de caja o un egreso con la caja especifica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Prefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Prefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del almacén (VARCHAR 100); identifica la ubicación física, virtual o lógica (ej: Farmacia Central, Almacén Quirófano).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del almacén (VARCHAR 20) usado en búsquedas, reportes RIPS, traslados y trazabilidad de medicamentos e insumos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK, IDENTITY) del almacén en el sistema; referencia para todas las transacciones de inventario y movimientos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bodegas o almacenes del módulo de inventario. Registra cada punto de almacenamiento físico o virtual de productos/medicamentos, incluyendo su clasificación contable, centro de costo, tipo y características especiales como consignación, custodia o tránsito.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse';
