CREATE TABLE [az_func].[Leases_17b4b00c88567e55_1706541213] (
    [NUMINGRES]                    CHAR (10)     NOT NULL,
    [_az_func_ChangeVersion]       BIGINT        NOT NULL,
    [_az_func_AttemptCount]        INT           NOT NULL,
    [_az_func_LeaseExpirationTime] DATETIME2 (7) NULL,
    PRIMARY KEY CLUSTERED ([NUMINGRES] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla interna de control de procesamiento asíncrono (Azure Functions) que registra los bloqueos temporales (leases) sobre ingresos de pacientes, evitando que dos procesos simultáneos procesen el mismo ingreso.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_17b4b00c88567e55_1706541213';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_17b4b00c88567e55_1706541213';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente que está siendo procesado o bloqueado por el proceso automático.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_17b4b00c88567e55_1706541213', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_17b4b00c88567e55_1706541213', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de cambio interna que identifica la revisión o modificación detectada sobre el registro, usada para control de concurrencia.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_17b4b00c88567e55_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_ChangeVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_17b4b00c88567e55_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_ChangeVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de intentos realizados por el proceso automático para procesar este ingreso, útil para detectar reintentos o fallos repetidos.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_17b4b00c88567e55_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_AttemptCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_17b4b00c88567e55_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_AttemptCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de vencimiento del bloqueo temporal (lease) sobre el ingreso; cuando expira, otro proceso puede tomarlo.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_17b4b00c88567e55_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_LeaseExpirationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_17b4b00c88567e55_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_LeaseExpirationTime';
