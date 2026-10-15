CREATE TABLE [Maintenance].[WorkOrder] (
    [Id]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Consecutive]              VARCHAR (20)  NOT NULL,
    [Description]              VARCHAR (500) NOT NULL,
    [BranchOfficeId]           INT           NOT NULL,
    [RequestDate]              DATETIME      NOT NULL,
    [ProgramDate]              DATETIME      NOT NULL,
    [ProtocolId]               INT           NULL,
    [State]                    TINYINT       NOT NULL,
    [ReversalReasonId]         INT           NULL,
    [DescriptionReversal]      VARCHAR (MAX) NULL,
    [CreationUser]             VARCHAR (20)  NOT NULL,
    [CreationDate]             DATETIME      NOT NULL,
    [TimeStamp]                ROWVERSION    NOT NULL,
    [AnullateUser]             VARCHAR (20)  NULL,
    [AnullateDate]             DATETIME      NULL,
    [MaintenanceResponsibleId] INT           NULL,
    [ModificationUser]         VARCHAR (20)  NULL,
    [ModificationDate]         DATETIME      NULL,
    [ConfirmationUser]         VARCHAR (20)  NULL,
    [ConfirmationDate]         DATETIME      NULL,
    [EntityId]                 INT           NULL,
    [EntityCode]               VARCHAR (20)  NULL,
    [EntityName]               VARCHAR (250) NULL,
    [PhysicalAssetId]          INT           NOT NULL,
    CONSTRAINT [PK_WorkOrder] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkOrder_BranchOffice] FOREIGN KEY ([BranchOfficeId]) REFERENCES [Payroll].[BranchOffice] ([Id]),
    CONSTRAINT [FK_WorkOrder_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_WorkOrder_MaintenanceAnulateReason] FOREIGN KEY ([ReversalReasonId]) REFERENCES [Maintenance].[MaintenanceAnulateReason] ([Id]),
    CONSTRAINT [FK_WorkOrder_MaintenanceProtocol] FOREIGN KEY ([ProtocolId]) REFERENCES [Maintenance].[MaintenanceProtocol] ([Id]),
    CONSTRAINT [FK_WorkOrder_MaintenanceResponsible] FOREIGN KEY ([MaintenanceResponsibleId]) REFERENCES [Maintenance].[MaintenanceResponsible] ([Id])
);


GO
ALTER TABLE [Maintenance].[WorkOrder] NOCHECK CONSTRAINT [FK_WorkOrder_BranchOffice];


GO
ALTER TABLE [Maintenance].[WorkOrder] NOCHECK CONSTRAINT [FK_WorkOrder_FixedAssetPhysicalAsset];


GO
ALTER TABLE [Maintenance].[WorkOrder] NOCHECK CONSTRAINT [FK_WorkOrder_MaintenanceAnulateReason];


GO
ALTER TABLE [Maintenance].[WorkOrder] NOCHECK CONSTRAINT [FK_WorkOrder_MaintenanceProtocol];


GO
ALTER TABLE [Maintenance].[WorkOrder] NOCHECK CONSTRAINT [FK_WorkOrder_MaintenanceResponsible];


GO
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-06-21
-- Description:	Trigger para que la notificacion de las ordenes de trabajo
-- =============================================
create TRIGGER [Maintenance].[tggWorkOrderNotification]
   ON  [Maintenance].[WorkOrder]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON

	-- Si se esta asignando o reasignando la orden de trabajo
    IF EXISTS
	(
		SELECT 1
		FROM INSERTED i
		LEFT JOIN DELETED d ON i.Id = d.Id
		WHERE i.MaintenanceResponsibleId <> ISNULL(d.MaintenanceResponsibleId, 0)
	)
	BEGIN
		INSERT INTO [Maintenance].[WorkOrderNotification]
		(
			WorkOrderId, ThirdPartyId, Type, Email, Status, CreationDate
		)
		SELECT wo.Id, tp.Id, 1, e.Email, 0, Common.GETDATE()
		FROM INSERTED wo
		JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON wo.MaintenanceResponsibleId = mr.Id
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON mr.ThirdPartyId = tp.Id
		JOIN Common.Email e WITH (NOLOCK) ON tp.PersonId = e.IdPerson AND e.Type = 1
	END

	-- Si se esta actualizando el estado
    IF EXISTS
	(
		SELECT 1
		FROM INSERTED i
		LEFT JOIN DELETED d ON i.Id = d.Id
		WHERE i.State IN (2,3,4,5) AND i.State <> ISNULL(d.State, 0)
	)
	BEGIN
		-- Si se confirma o anula la orden de trabajo
		INSERT INTO [Maintenance].[WorkOrderNotification]
		(
			WorkOrderId, ThirdPartyId, Type, Email, Status, CreationDate
		)
		SELECT wo.Id, tp.Id, wo.State, e.Email, 0, Common.GETDATE()
		FROM INSERTED wo
		JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON wo.PhysicalAssetId = fapa.Id
		JOIN FixedAsset.FixedAssetResponsible far WITH (NOLOCK) ON fapa.ResponsibleId = far.Id
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON far.ThirdPartyId = tp.Id
		JOIN Common.Email e WITH (NOLOCK) ON tp.PersonId = e.IdPerson AND e.Type = 1
		WHERE wo.State IN (2,3)

		-- Si se acepto o rechazo la orden de trabajo
		INSERT INTO [Maintenance].[WorkOrderNotification]
		(
			WorkOrderId, ThirdPartyId, Type, Email, Status, CreationDate
		)
		SELECT wo.Id, tp.Id, wo.State, e.Email, 0, Common.GETDATE()
		FROM INSERTED wo
		JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON wo.MaintenanceResponsibleId = mr.Id
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON mr.ThirdPartyId = tp.Id
		JOIN Common.Email e WITH (NOLOCK) ON tp.PersonId = e.IdPerson AND e.Type = 1
		WHERE wo.State IN (4,5)
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del Activo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del responsable de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'MaintenanceResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de anulación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'AnullateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que anula', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'AnullateUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de la anulación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'DescriptionReversal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la razón de anulación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'ReversalReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la orden  1 - Registrado  2 - Confirmado  3 - Anulado  4 - Aprobado  5 - Rechazado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del protocolo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'ProtocolId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de programacion', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'ProgramDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha solicitud', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'RequestDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la sucursal', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrder', @level2type = N'COLUMN', @level2name = N'Id';

