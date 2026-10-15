CREATE TABLE [Contract].[CUPSEntityPanelDetail] (
    [Id]                INT          IDENTITY (1, 1) NOT NULL,
    [CUPSEntityPanelId] INT          NOT NULL,
    [CUPSEntityId]      INT          NOT NULL,
    [CreationUser]      VARCHAR (20) NOT NULL,
    [CreationDate]      DATETIME     NOT NULL,
    CONSTRAINT [PK_CUPSEntityPanelDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CUPSEntityPanel_CUPSEntityPanelId] FOREIGN KEY ([CUPSEntityPanelId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_CUPSEntityPanelDetail_CUPSEntityId] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id])
);


GO
ALTER TABLE [Contract].[CUPSEntityPanelDetail] NOCHECK CONSTRAINT [FK_CUPSEntityPanel_CUPSEntityPanelId];


GO
ALTER TABLE [Contract].[CUPSEntityPanelDetail] NOCHECK CONSTRAINT [FK_CUPSEntityPanelDetail_CUPSEntityId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la fecha de creación del detalle', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el usuario de creación del detalle', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la entidad CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del panel de entidad CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityPanelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro del detalle del panel de entidad CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'Id';

