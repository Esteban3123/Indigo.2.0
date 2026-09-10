CREATE TABLE [Maintenance].[MaintenanceFailureRequestDetail] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaintenanceFailureRequestId] INT           NOT NULL,
    [TransactionClass]            TINYINT       NOT NULL,
    [PhysicalAssetId]             INT           NOT NULL,
    [PhysicalAssetPartsId]        INT           NULL,
    [Description]                 VARCHAR (500) NULL,
    CONSTRAINT [PK_MaintenanceFailureRequestDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MaintenanceFailureRequestDetail_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_MaintenanceFailureRequestDetail_FixedAssetPhysicalAssetParts] FOREIGN KEY ([PhysicalAssetPartsId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAssetParts] ([Id]),
    CONSTRAINT [FK_MaintenanceFailureRequestDetail_MaintenanceFailureRequest] FOREIGN KEY ([MaintenanceFailureRequestId]) REFERENCES [Maintenance].[MaintenanceFailureRequest] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del problema o falla del activo o parte (VARCHAR 500); narrativa libre de la solicitud de mantenimiento correctivo reportado por el usuario', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del detalle de la solicitud', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la parte o componente del activo (FK a [FixedAsset].[FixedAssetPhysicalAssetParts]); se completa solo cuando TransactionClass=2 (Parte de Activo); especifica qué componente interno falla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetPartsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la parte, este solo se llena si la clase del detalle es Parte', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetPartsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetPartsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del activo fijo/equipo (FK a [FixedAsset].[FixedAssetPhysicalAsset]); se completa cuando TransactionClass=1 (Activo); referencia el equipo, máquina o infraestructura que requiere mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del Activo, este solo se llena si la clase del detalle es Activo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de detalle (TINYINT): 1=Activo/Equipo, 2=Parte/Componente del Activo; determina si se reporta falla en equipo completo o solo en componente específico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'TransactionClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la clase del detalle a solicitar mantenimiento  1 - Activo  2 - Parte de Activo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'TransactionClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'TransactionClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la solicitud de mantenimiento por falla (FK a [Maintenance].[MaintenanceFailureRequest]); agrupa los detalles de una misma solicitud de reparación/mantenimiento correctivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'MaintenanceFailureRequestId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la solicitud de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'MaintenanceFailureRequestId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'MaintenanceFailureRequestId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (INT IDENTITY) del detalle de solicitud de mantenimiento por falla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los ítems incluidos en una solicitud de falla de mantenimiento: registra cada activo físico (y opcionalmente sus partes) afectado, la clase de transacción asociada y una descripción libre del problema reportado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetail';
