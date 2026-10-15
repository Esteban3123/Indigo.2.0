CREATE TABLE [Inventory].[LoanMerchandiseDevolution] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]              VARCHAR (20)  NOT NULL,
    [OperatingUnitId]   INT           NOT NULL,
    [DocumentDate]      DATETIME      NOT NULL,
    [WarehouseId]       INT           NOT NULL,
    [LoanMerchandiseId] INT           NOT NULL,
    [Observation]       VARCHAR (300) NULL,
    [Status]            TINYINT       NOT NULL,
    [CreationUser]      VARCHAR (20)  CONSTRAINT [DF_LoanMerchandiseDevolution_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]      DATETIME      CONSTRAINT [DF_LoanMerchandiseDevolution_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]  VARCHAR (20)  NULL,
    [ModificationDate]  DATETIME      NULL,
    [ConfirmationUser]  VARCHAR (20)  NULL,
    [ConfirmationDate]  DATETIME      NULL,
    [AnnulmentUser]     VARCHAR (20)  NULL,
    [AnnulmentDate]     DATETIME      NULL,
    [TimeStamp]         ROWVERSION    NOT NULL,
    CONSTRAINT [PK_LoanMerchandiseDevolution__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoanMerchandiseDevolution_LoanMerchandise] FOREIGN KEY ([LoanMerchandiseId]) REFERENCES [Inventory].[LoanMerchandise] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDevolution_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDevolution_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[LoanMerchandiseDevolution] NOCHECK CONSTRAINT [FK_LoanMerchandiseDevolution_LoanMerchandise];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDevolution] NOCHECK CONSTRAINT [FK_LoanMerchandiseDevolution_OperatingUnit];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDevolution] NOCHECK CONSTRAINT [FK_LoanMerchandiseDevolution_Warehouse];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_LoanMerchandiseDevolution__Code]
    ON [Inventory].[LoanMerchandiseDevolution]([Code] ASC);


GO
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-02-08
-- Description:	Validar que los lotes correspondan con los detalles
-- =============================================
CREATE TRIGGER [Inventory].[tgg_LoanMerchandiseDevolutionDetail]
   ON  [Inventory].[LoanMerchandiseDevolution]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

    IF EXISTS 
	(
		SELECT *
		FROM INSERTED lmdev
		JOIN Inventory.LoanMerchandiseDevolutionDetail lmdevd WITH (NOLOCK) ON lmdev.Id = lmdevd.LoanMerchandiseDevolutionId
		LEFT JOIN
		(
			SELECT lmdevdbs.LoanMerchandiseDevolutionDetailId, SUM(lmdevdbs.Quantity) Quantity
			FROM INSERTED lmdev
			JOIN Inventory.LoanMerchandiseDevolutionDetail lmdevd WITH (NOLOCK) ON lmdev.Id = lmdevd.LoanMerchandiseDevolutionId
			JOIN Inventory.LoanMerchandiseDevolutionDetailBatchSerial lmdevdbs WITH (NOLOCK) ON lmdevd.Id = lmdevdbs.LoanMerchandiseDevolutionDetailId
			GROUP BY lmdevdbs.LoanMerchandiseDevolutionDetailId
		) lmdevdbs ON lmdevd.Id = lmdevdbs.LoanMerchandiseDevolutionDetailId
		WHERE lmdev.Status = 2 AND lmdevd.Quantity <> ISNULL(lmdevdbs.Quantity, 0)
	)
	BEGIN
		THROW 51000, 'Devolución de Préstamo: Error generado por control desde trigger. La cantidad devuelta del detalle no corresponde con la sumatoria devuelta de sus lotes', 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento (Registrado = 1, Confirmado = 2, Anulado = 3)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica las observaciones de la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del prestamos que se va a devolver', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha del documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'Id';

