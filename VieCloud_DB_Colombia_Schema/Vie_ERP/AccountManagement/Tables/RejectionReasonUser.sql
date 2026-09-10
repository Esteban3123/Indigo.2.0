CREATE TABLE [AccountManagement].[RejectionReasonUser] (
    [Id]                INT          IDENTITY (1, 1) NOT NULL,
    [RejectionReasonId] INT          NOT NULL,
    [UserId]            INT          NOT NULL,
    [Usercode]          VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_RejectionReasonUser_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RejectionReasonUser_RejectionReason] FOREIGN KEY ([RejectionReasonId]) REFERENCES [AccountManagement].[RejectionReason] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario en el sistema, identificador alfanumérico (VARCHAR 20) que permite rastrear qué profesional de salud o administrador registró el rechazo.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser', @level2type = N'COLUMN', @level2name = N'Usercode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser', @level2type = N'COLUMN', @level2name = N'Usercode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser', @level2type = N'COLUMN', @level2name = N'Usercode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico del usuario (INT), clave foránea que vincula al profesional de salud o personal administrativo responsable del rechazo de la factura, glosa o documento.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del motivo de rechazo (INT), referencia a la tabla RejectionReason que especifica la causa: glosa, error administrativo, información incompleta, procedimiento no autorizado, u otro motivo de negación.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser', @level2type = N'COLUMN', @level2name = N'RejectionReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del motivo del rechazo', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser', @level2type = N'COLUMN', @level2name = N'RejectionReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser', @level2type = N'COLUMN', @level2name = N'RejectionReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria de la tabla (INT IDENTITY), identificador único que registra la relación entre usuario y motivo de rechazo para auditoría y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion de la tabla', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asociación entre usuarios del sistema y los motivos de rechazo que tienen permitido utilizar en la gestión de cuentas. Controla qué usuarios pueden aplicar cada motivo de rechazo (glosa, devolución o no pago) dentro del módulo de cartera.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'RejectionReasonUser';
