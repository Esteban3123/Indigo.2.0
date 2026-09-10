CREATE TABLE [az_func].[Leases_acd820f1709380fb_477960779] (
    [Id]                           INT           NOT NULL,
    [_az_func_ChangeVersion]       BIGINT        NOT NULL,
    [_az_func_AttemptCount]        INT           NOT NULL,
    [_az_func_LeaseExpirationTime] DATETIME2 (7) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla interna de control de arrendamientos (leases) para Azure Functions. Registra el estado de procesamiento distribuido de registros, evitando que dos instancias procesen el mismo registro simultáneamente.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_477960779';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_477960779';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro bajo control de lease.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_477960779', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_477960779', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de cambio del registro; número secuencial que indica la última modificación detectada para sincronización.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_477960779', @level2type = N'COLUMN', @level2name = N'_az_func_ChangeVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_477960779', @level2type = N'COLUMN', @level2name = N'_az_func_ChangeVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de intentos de procesamiento realizados sobre este registro por la función de Azure.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_477960779', @level2type = N'COLUMN', @level2name = N'_az_func_AttemptCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_477960779', @level2type = N'COLUMN', @level2name = N'_az_func_AttemptCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de vencimiento del lease; indica hasta cuándo una instancia tiene reservado el procesamiento exclusivo del registro.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_477960779', @level2type = N'COLUMN', @level2name = N'_az_func_LeaseExpirationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_477960779', @level2type = N'COLUMN', @level2name = N'_az_func_LeaseExpirationTime';
