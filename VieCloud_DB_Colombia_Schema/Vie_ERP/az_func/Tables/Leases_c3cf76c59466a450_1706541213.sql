CREATE TABLE [az_func].[Leases_c3cf76c59466a450_1706541213] (
    [NUMINGRES]                    CHAR (10)     NOT NULL,
    [_az_func_ChangeVersion]       BIGINT        NOT NULL,
    [_az_func_AttemptCount]        INT           NOT NULL,
    [_az_func_LeaseExpirationTime] DATETIME2 (7) NULL,
    PRIMARY KEY CLUSTERED ([NUMINGRES] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de control interno de procesamiento asíncrono (Azure Functions): registra los bloqueos temporales (leases) sobre números de ingreso para garantizar que cada ingreso sea procesado una sola vez y sin conflictos entre instancias concurrentes.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_c3cf76c59466a450_1706541213';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_c3cf76c59466a450_1706541213';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente que está siendo procesado o bloqueado temporalmente.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_c3cf76c59466a450_1706541213', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_c3cf76c59466a450_1706541213', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del cambio detectado en el registro; permite rastrear qué modificación disparó el procesamiento.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_c3cf76c59466a450_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_ChangeVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_c3cf76c59466a450_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_ChangeVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de intentos realizados para procesar este ingreso; útil para detectar reintentos o fallos repetidos.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_c3cf76c59466a450_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_AttemptCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_c3cf76c59466a450_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_AttemptCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que vence el bloqueo (lease) sobre el ingreso; pasada esta hora, otro proceso puede tomarlo.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_c3cf76c59466a450_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_LeaseExpirationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_c3cf76c59466a450_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_LeaseExpirationTime';
