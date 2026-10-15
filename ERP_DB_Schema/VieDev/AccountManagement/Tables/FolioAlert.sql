CREATE TABLE [AccountManagement].[FolioAlert] (
    [Id]                     INT           IDENTITY (1, 1) NOT NULL,
    [FolioTransferId]        INT           NULL,
    [CreationDate]           DATE          DEFAULT (getdate()) NOT NULL,
    [Comments]               VARCHAR (500) NOT NULL,
    [ManagementAreaId]       INT           NOT NULL,
    [CreationUser]           VARCHAR (50)  NOT NULL,
    [Status]                 BIT           NOT NULL,
    [AdmissionNumber]        INT           NULL,
    [RevenueControlDetailId] INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FolioAlert_FolioTransfer] FOREIGN KEY ([FolioTransferId]) REFERENCES [AccountManagement].[FolioTransfer] ([Id]),
    CONSTRAINT [FK_FolioAlert_ManagementAreas] FOREIGN KEY ([ManagementAreaId]) REFERENCES [AccountManagement].[ManagementAreas] ([Id]),
    CONSTRAINT [FK_FolioAlert_RevenueControlDetail] FOREIGN KEY ([RevenueControlDetailId]) REFERENCES [Billing].[RevenueControlDetail] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al folio especifico', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al ingreso', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la alerta: 1 - Activa (pendiente de resolver), 0 - Resuelta', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que creó la alerta', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área de gestión a la que pertenece la alerta', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'ManagementAreaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentarios o descripción de la alerta', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación de la alerta', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al traslado de folio, si existiese traslado', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'FolioTransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle del traslado', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'Id';

