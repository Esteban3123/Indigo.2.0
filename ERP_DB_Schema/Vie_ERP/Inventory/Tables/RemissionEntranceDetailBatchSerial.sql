CREATE TABLE [Inventory].[RemissionEntranceDetailBatchSerial] (
    [Id]                        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RemissionEntranceDetailId] INT NOT NULL,
    [BatchSerialId]             INT NULL,
    [Quantity]                  INT NOT NULL,
    [OutstandingQuantity]       INT NOT NULL,
    CONSTRAINT [PK_RemissionEntranceDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_RemissionEntranceDetailBatchSerial] CHECK ([Quantity]>(0)),
    CONSTRAINT [CK_RemissionEntranceDetailBatchSerial_1] CHECK ([OutstandingQuantity]>=(0) AND [OutstandingQuantity]<=[Quantity]),
    CONSTRAINT [FK_RemissionEntranceDetailBatchSerial_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_RemissionEntranceDetailBatchSerial_RemissionEntranceDetail] FOREIGN KEY ([RemissionEntranceDetailId]) REFERENCES [Inventory].[RemissionEntranceDetail] ([Id])
);


GO
ALTER TABLE [Inventory].[RemissionEntranceDetailBatchSerial] NOCHECK CONSTRAINT [CK_RemissionEntranceDetailBatchSerial];


GO
ALTER TABLE [Inventory].[RemissionEntranceDetailBatchSerial] NOCHECK CONSTRAINT [CK_RemissionEntranceDetailBatchSerial_1];


GO
ALTER TABLE [Inventory].[RemissionEntranceDetailBatchSerial] NOCHECK CONSTRAINT [FK_RemissionEntranceDetailBatchSerial_BatchSerial];


GO
ALTER TABLE [Inventory].[RemissionEntranceDetailBatchSerial] NOCHECK CONSTRAINT [FK_RemissionEntranceDetailBatchSerial_RemissionEntranceDetail];


GO
CREATE TRIGGER [Inventory].[tgg_ValidateBeforeDeleteRemissionEntranceDetailBatchSerialIfTheRemissionEntranceIsNotConfirmed]
   ON  [Inventory].[RemissionEntranceDetailBatchSerial] 
   AFTER DELETE
AS 
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from<br>
    -- interfering with SELECT statements.<br>
    SET NOCOUNT ON;

	IF EXISTS (
		SELECT re.Id
		FROM Inventory.RemissionEntrance re
		JOIN Inventory.RemissionEntranceDetail red ON re.Id = red.RemissionEntranceId
		JOIN DELETED redbs ON red.Id = redbs.RemissionEntranceDetailId
		WHERE re.Status = 2
	)
	BEGIN
		THROW 51000, 'Error generado por control del lote de eliminacion desde trigger', 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad pendiente del Item, Cuando se crea la remision este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una devolucion de la remision o se incluya el item en una remision de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identidicacion  de la  serie de lote', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'RemissionEntranceDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';

