CREATE TABLE [Maintenance].[MaintenanceFailureRequest] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [DateFailure]      DATETIME      NOT NULL,
    [TypeRequest]      INT           NOT NULL,
    [Report]           INT           NOT NULL,
    [Observation]      VARCHAR (500) NOT NULL,
    [Status]           TINYINT       NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    [NameOther]        VARCHAR (100) NULL,
    [OperatingUnitId]  INT           NULL,
    [BranchOfficeId]   INT           NULL,
    CONSTRAINT [PK_MaintenanceFailureRequest] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MaintenanceFailureRequest_BranchOffice] FOREIGN KEY ([BranchOfficeId]) REFERENCES [Payroll].[BranchOffice] ([Id]),
    CONSTRAINT [FK_MaintenanceFailureRequest_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Maintenance].[MaintenanceFailureRequest] NOCHECK CONSTRAINT [FK_MaintenanceFailureRequest_BranchOffice];


GO
ALTER TABLE [Maintenance].[MaintenanceFailureRequest] NOCHECK CONSTRAINT [FK_MaintenanceFailureRequest_OperatingUnit];


GO
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-06-21
-- Description:	Trigger para que la notificacion cuando se confirme la solicitud
-- =============================================
CREATE TRIGGER [Maintenance].[tggMaintenanceFailureRequestNotification]
   ON  [Maintenance].[MaintenanceFailureRequest]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON

	-- Si se ha confirmado el documento
    IF EXISTS
	(
		SELECT 1
		FROM INSERTED i
		LEFT JOIN DELETED d ON i.Id = d.Id
		WHERE i.Status = 2 AND i.Status <> ISNULL(d.Status, 0)
	)
	BEGIN
		INSERT INTO [Maintenance].[MaintenanceFailureRequestDetailNotification]
		(
			MaintenanceFailureRequestDetailId, ThirdPartyId, Email, Status, CreationDate
		)
		SELECT mfrd.Id, tp.Id, e.Email, 0, Common.GETDATE()
		FROM INSERTED mfr
		JOIN Maintenance.MaintenanceFailureRequestDetail mfrd WITH (NOLOCK) ON mfr.Id = mfrd.MaintenanceFailureRequestId
		JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON mfrd.PhysicalAssetId = fapa.Id
		JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
		JOIN Maintenance.ResponsibleCatalogOfArticles mrc WITH (NOLOCK) ON fai.ItemCatalogId = mrc.ItemCatalogId
		JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON mrc.ResponsibleId = mr.Id
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON mr.ThirdPartyId = tp.Id
		JOIN Common.Email e WITH (NOLOCK) ON tp.PersonId = e.IdPerson AND e.Type = 1
		WHERE mr.ResponsibleRole IN (2,3) AND mr.Status = 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la sucursal', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequest', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequest', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de quien reporta si elige la opción otro ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequest', @level2type = N'COLUMN', @level2name = N'NameOther';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequest', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequest', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequest', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequest', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequest', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Registro   1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequest', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequest', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Quien reporta, Usuario del Sistema = 1, Otro = 2', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequest', @level2type = N'COLUMN', @level2name = N'Report';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de solicitud, Falla General = 1, Revisión = 2, Otro = 3', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequest', @level2type = N'COLUMN', @level2name = N'TypeRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cargue Hora del Sistema ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequest', @level2type = N'COLUMN', @level2name = N'DateFailure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Reporte de Fallas y Solicitud de Mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequest', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequest', @level2type = N'COLUMN', @level2name = N'Id';

