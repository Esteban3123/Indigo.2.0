CREATE TABLE [Inventory].[TransferOrder] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                   VARCHAR (20)  NOT NULL,
    [OperatingUnitId]        INT           NOT NULL,
    [DocumentDate]           DATETIME      NOT NULL,
    [OrderType]              TINYINT       NOT NULL,
    [DispatchTo]             TINYINT       NOT NULL,
    [SourceWarehouseId]      INT           NOT NULL,
    [TargetWarehouseId]      INT           NULL,
    [TargetFunctionalUnitId] INT           NULL,
    [AdjustmentConceptId]    INT           NULL,
    [ThirdPartyId]           INT           NULL,
    [Description]            VARCHAR (300) NULL,
    [Status]                 TINYINT       NOT NULL,
    [CreationUser]           VARCHAR (20)  NOT NULL,
    [CreationDate]           DATETIME      NOT NULL,
    [ModificationUser]       VARCHAR (20)  NULL,
    [ModificationDate]       DATETIME      NULL,
    [ConfirmationUser]       VARCHAR (20)  NULL,
    [ConfirmationDate]       DATETIME      NULL,
    [AnnulmentUser]          VARCHAR (20)  NULL,
    [AnnulmentDate]          DATETIME      NULL,
    [TimeStamp]              ROWVERSION    NOT NULL,
    [TransitWarehouseId]     INT           NULL,
    CONSTRAINT [PK_TransferOrder__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TransferOrder_AdjustmentConcept] FOREIGN KEY ([AdjustmentConceptId]) REFERENCES [Inventory].[AdjustmentConcept] ([Id]),
    CONSTRAINT [FK_TransferOrder_FunctionalUnit] FOREIGN KEY ([TargetFunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_TransferOrder_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_TransferOrder_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_TransferOrder_Warehouse] FOREIGN KEY ([SourceWarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id]),
    CONSTRAINT [FK_TransferOrder_Warehouse1] FOREIGN KEY ([TargetWarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id]),
    CONSTRAINT [FK_TransferOrder_Warehouse2] FOREIGN KEY ([TransitWarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[TransferOrder] NOCHECK CONSTRAINT [FK_TransferOrder_AdjustmentConcept];


GO
ALTER TABLE [Inventory].[TransferOrder] NOCHECK CONSTRAINT [FK_TransferOrder_FunctionalUnit];


GO
ALTER TABLE [Inventory].[TransferOrder] NOCHECK CONSTRAINT [FK_TransferOrder_OperatingUnit];


GO
ALTER TABLE [Inventory].[TransferOrder] NOCHECK CONSTRAINT [FK_TransferOrder_ThirdParty];


GO
ALTER TABLE [Inventory].[TransferOrder] NOCHECK CONSTRAINT [FK_TransferOrder_Warehouse];


GO
ALTER TABLE [Inventory].[TransferOrder] NOCHECK CONSTRAINT [FK_TransferOrder_Warehouse1];


GO
ALTER TABLE [Inventory].[TransferOrder] NOCHECK CONSTRAINT [FK_TransferOrder_Warehouse2];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_TransferOrder__Code]
    ON [Inventory].[TransferOrder]([Code] ASC);


GO
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2019-05-21
-- Description:	Se valida que la secuencia numerica sea consecutiva
-- =============================================
CREATE TRIGGER [Inventory].[tgg_TransferOrder_ValidateConsecutives]
   ON  [Inventory].[TransferOrder]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	DECLARE @MinId AS INT,
			@Scope AS VARCHAR(2),
			@Prefix AS VARCHAR(4) = ''

	SELECT @Scope = iis.Scope
	FROM Inventory.InventorySequence iis
	WHERE iis.IdForm = '1519' 

	SELECT @Prefix = w.Prefix
	FROM INSERTED toc
	JOIN Inventory.Warehouse w ON toc.SourceWarehouseId = w.Id
	WHERE @Scope = 'O'

	SELECT @MinId = MIN(toc.Id)
	FROM Inventory.TransferOrder toc
	WHERE toc.Code LIKE CONCAT(@Prefix, '%')

    IF EXISTS
	(
		SELECT 1
		FROM INSERTED toc
		LEFT JOIN Inventory.TransferOrder toa ON CAST(dbo.udf_GetNumeric(toc.Code) AS BIGINT) = (CAST(dbo.udf_GetNumeric(toa.Code) AS BIGINT) + 1)
		WHERE toa.Code LIKE CONCAT(@Prefix, '%')
			AND toc.Id <> ISNULL(@MinId, toc.Id) 
			AND toa.Id IS NULL
	)
	BEGIN
		THROW 51000, 'Error generado por control desde trigger. El consecutivo no sigue el orden preestablecido', 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del Almacen en transito, Este solo se habilita si el tipo de orden es de traslado en transito', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'TransitWarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado   1 - Registrado   2 - Confirmado / Entregado  3 - Anulado  4 - En Transito', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la descripcion de la orden de traslado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del tercero, este solo se solicita si es de tipo de la orden es de Consumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el concepto de movimiento de inventarios, este debe ser de tipo Traslado por consumo    Este campo solo se solicita si el tipo de la orden es Consumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'AdjustmentConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la unidad funcional, este campo solo se llena si el tipo de la orden es de consumo y si el despacho es a una unidad funcional ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'TargetFunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del Almacen de destino, Este solo se habilita si el tipo de orden es de traslado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'TargetWarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del almacen de origen, este campo solo se llena si el tipo de la orden es de traslado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'SourceWarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica hacia donde se va a despachar los items  1 - Almacen  2 - Unidad Funcional    Este campo solo se solicita cuando el tipo de la orden es consumo ya que cuando es de tipo traslado este campo se llena por defecto en 1 - Almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'DispatchTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de orden de traslado  1  - Traslado  2 - Consumo  3 - Traslado en Transito', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'OrderType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la orden de traslado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'Id';

