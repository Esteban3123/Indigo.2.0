CREATE TABLE [az_func].[Leases_af6d0503bd549a0d_1877633782] (
    [Id]                           INT           NOT NULL,
    [_az_func_ChangeVersion]       BIGINT        NOT NULL,
    [_az_func_AttemptCount]        INT           NOT NULL,
    [_az_func_LeaseExpirationTime] DATETIME2 (7) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla interna de control de Azure Functions que gestiona los bloqueos (leases) de procesamiento de cambios, evitando que múltiples instancias procesen el mismo registro simultáneamente.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_af6d0503bd549a0d_1877633782';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_af6d0503bd549a0d_1877633782';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro bloqueado que está siendo procesado por una función.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_af6d0503bd549a0d_1877633782', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_af6d0503bd549a0d_1877633782', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del cambio detectado en el registro, usada para rastrear qué modificación está siendo procesada.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_af6d0503bd549a0d_1877633782', @level2type = N'COLUMN', @level2name = N'_az_func_ChangeVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_af6d0503bd549a0d_1877633782', @level2type = N'COLUMN', @level2name = N'_az_func_ChangeVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de intentos realizados para procesar el cambio, útil para detectar reintentos o fallos repetidos.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_af6d0503bd549a0d_1877633782', @level2type = N'COLUMN', @level2name = N'_az_func_AttemptCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_af6d0503bd549a0d_1877633782', @level2type = N'COLUMN', @level2name = N'_az_func_AttemptCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de vencimiento del bloqueo; cuando expira, otro proceso puede tomar el registro para procesarlo.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_af6d0503bd549a0d_1877633782', @level2type = N'COLUMN', @level2name = N'_az_func_LeaseExpirationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_af6d0503bd549a0d_1877633782', @level2type = N'COLUMN', @level2name = N'_az_func_LeaseExpirationTime';
