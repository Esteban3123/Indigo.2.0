CREATE TABLE [az_func].[Leases_acd820f1709380fb_1706541213] (
    [NUMINGRES]                    CHAR (10)     NOT NULL,
    [_az_func_ChangeVersion]       BIGINT        NOT NULL,
    [_az_func_AttemptCount]        INT           NOT NULL,
    [_az_func_LeaseExpirationTime] DATETIME2 (7) NULL,
    PRIMARY KEY CLUSTERED ([NUMINGRES] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla interna de control de procesamiento de Azure Functions que gestiona los bloqueos (leases) sobre registros de ingresos, evitando que dos procesos simultáneos procesen el mismo ingreso al mismo tiempo.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_1706541213';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_1706541213';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente que está siendo procesado o bloqueado por la función automatizada.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_1706541213', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_1706541213', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del cambio detectado en el registro; identifica qué modificación disparó el proceso de la función.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_ChangeVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_ChangeVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de intentos realizados para procesar el ingreso; útil para detectar reintentos o fallos repetidos.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_AttemptCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_AttemptCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de vencimiento del bloqueo (lease) sobre el ingreso; indica hasta cuándo el registro está reservado para un proceso específico.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_LeaseExpirationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'Leases_acd820f1709380fb_1706541213', @level2type = N'COLUMN', @level2name = N'_az_func_LeaseExpirationTime';
