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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al detalle de control de ingresos/folio específico asociado a la alerta de facturación, glosa o recaudo (FK a Billing.RevenueControlDetail, INT)', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia al folio especifico', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso, atención o admisión del paciente vinculado a la alerta (INT, clave de búsqueda de paciente/episodio)', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia al ingreso', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la alerta: 1=Activa/Pendiente de resolver, 0=Resuelta/Cerrada (BIT booleano)', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la alerta: 1 - Activa (pendiente de resolver), 0 - Resuelta', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o login del usuario/profesional que generó la alerta en el sistema (VARCHAR 50, auditoría)', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que creó la alerta', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área de gestión, unidad funcional o departamento responsable de resolver la alerta (FK a AccountManagement.ManagementAreas, INT)', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'ManagementAreaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Área de gestión a la que pertenece la alerta', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'ManagementAreaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'ManagementAreaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción, motivo, notas o comentarios detallados de la alerta, glosa, observación o pendiente (VARCHAR 500, texto libre)', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentarios o descripción de la alerta', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro/generación de la alerta en el sistema (DATE, sin hora)', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación de la alerta', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al traslado de folio asociado, si la alerta surge de un movimiento entre áreas o cambio de responsable (FK a AccountManagement.FolioTransfer, INT nullable)', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'FolioTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia al traslado de folio, si existiese traslado', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'FolioTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'FolioTransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial de la alerta de folio (INT IDENTITY, clave primaria, PK)', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del detalle del traslado', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Alertas generadas sobre folios de facturación o transferencia de cuentas. Registra observaciones, comentarios de gestión y el estado de cada alerta asociada a un ingreso o detalle de control de ingresos, permitiendo hacer seguimiento al área de gestión de cartera o cuentas.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioAlert';
