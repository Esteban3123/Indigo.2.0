CREATE TABLE [AccountManagement].[FolioTransfer] (
    [Id]                     INT           IDENTITY (1, 1) NOT NULL,
    [PreviousUser]           VARCHAR (20)  NULL,
    [ReceivingUser]          VARCHAR (20)  NULL,
    [AdmissionNumber]        VARCHAR (50)  NOT NULL,
    [RevenueControlDetailId] INT           NOT NULL,
    [TransferStatus]         TINYINT       DEFAULT ((1)) NOT NULL,
    [CreationUser]           VARCHAR (20)  NOT NULL,
    [CreationDate]           DATETIME      NOT NULL,
    [ModificationUser]       VARCHAR (50)  NULL,
    [ModificationDate]       DATETIME      NULL,
    [RejectionReasonId]      INT           NULL,
    [RejectionObservation]   VARCHAR (500) NULL,
    [ManagementAreaId]       INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FolioTransfer_ManagementArea] FOREIGN KEY ([ManagementAreaId]) REFERENCES [AccountManagement].[ManagementAreas] ([Id]),
    CONSTRAINT [FK_FolioTransfer_RejectionReason] FOREIGN KEY ([RejectionReasonId]) REFERENCES [AccountManagement].[RejectionReason] ([Id]),
    CONSTRAINT [FK_FolioTransfer_RevenueControlDetail] FOREIGN KEY ([RevenueControlDetailId]) REFERENCES [Billing].[RevenueControlDetail] ([Id])
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del traslado de folio (TINYINT): 1=Sin traslado, 2=Pendiente de aceptación, 3=Aceptada, 4=Rechazada. Indica el flujo de aprobación en la transferencia entre usuarios de gestión de cuentas.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'TransferStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del traslado: 1 - Sin traslado, 2 - Pendiente de aceptación, 3 - Aceptada, 4 - Rechazada', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'TransferStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'TransferStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del folio (FK a Billing.RevenueControlDetail) que se está trasladando. Vincula la transferencia al control de ingresos y facturación asociado.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del folio que se está trasladando', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o atención (VARCHAR 50) asociado al folio en traslado. Identifica el paciente y su evento clínico relacionado.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de ingreso asociado al folio', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario destinatario (VARCHAR 20) que recibe la transferencia del folio. Profesional o gestor que asume responsabilidad de la cuenta.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'ReceivingUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario al que se transfiere el folio', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'ReceivingUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'ReceivingUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario anterior (VARCHAR 20) que tenía la custodia del folio antes de la transferencia. Trazabilidad de cambio de responsable.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'PreviousUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que tenía el folio antes de la transferencia', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'PreviousUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'PreviousUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de traslado de folio (INT IDENTITY). Clave primaria para auditoría de transferencias entre usuarios.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del traslado de folio', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del área de gestión o unidad funcional (FK a AccountManagement.ManagementAreas) destino del traslado. Define departamento responsable.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'ManagementAreaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del area de gestión a la que va dirigido el traslado.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'ManagementAreaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'ManagementAreaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de transferencias de folios de cuentas entre usuarios de gestión. Guarda el historial de traspasos de responsabilidad sobre un ingreso (admisión) de un gestor a otro, incluyendo el estado de la transferencia y los motivos de rechazo cuando aplica.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registró la transferencia de folio en el sistema.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se creó el registro de la transferencia.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación sobre el registro de la transferencia.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de transferencia.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo por el cual se rechazó la transferencia del folio.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'RejectionReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'RejectionReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación o comentario detallado que justifica el rechazo de la transferencia del folio.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'RejectionObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'FolioTransfer', @level2type = N'COLUMN', @level2name = N'RejectionObservation';
