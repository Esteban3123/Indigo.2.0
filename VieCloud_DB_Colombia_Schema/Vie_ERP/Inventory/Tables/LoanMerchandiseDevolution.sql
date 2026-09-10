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



GO



GO



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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo (TIMESTAMP) de control de concurrencia. Captura automáticamente el instante de creación, modificación o cambio de estado del registro de devolución para auditoría.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se anuló el documento de devolución. Marca el instante de cancelación o reversión.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que anuló o canceló el documento de devolución de mercancía.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se confirmó la devolución de mercancía. Marca la validación y aceptación del documento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que confirmó o validó oficialmente la devolución de mercancía en el sistema.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro de devolución.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación al documento de devolución de mercancía.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se creó el registro de devolución. Marca el instante de generación del documento en el sistema.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó o registró el documento de devolución de mercancía en el sistema.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del documento de devolución (TINYINT): 1=Registrado, 2=Confirmado, 3=Anulado. Refleja el ciclo de vida de la devolución.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Documento (Registrado = 1, Confirmado = 2, Anulado = 3)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas descriptivas (VARCHAR 300) sobre la devolución de mercancía, estado del producto, condiciones o detalles relevantes del proceso.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica las observaciones de la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del préstamo de mercancía (FK a Inventory.LoanMerchandise) que se está devolviendo. Vincula la devolución con el préstamo original.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del prestamos que se va a devolver', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del almacén o bodega (FK a Inventory.Warehouse) donde se recibe o registra la devolución de mercancía en préstamo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del documento de devolución de mercancía. Marca el momento en que ocurre el evento de devolución.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha del documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa o centro de atención (FK a Common.OperatingUnit) donde se registra la devolución de mercancía.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico único (VARCHAR 20) que identifica la devolución de mercancía, utilizado para búsqueda y referencia en documentos de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la devolución de mercancía en préstamo. Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de devoluciones de mercancía prestada (préstamo de inventario). Guarda cada documento de devolución generado cuando un cliente o unidad operativa retorna artículos que fueron entregados en calidad de préstamo, incluyendo el estado del proceso y la trazabilidad de quién creó, modificó, confirmó o anuló cada devolución.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolution';
