-- =============================================================================
--  [rda].[outbox_messages] — DEPRECADA / RETIRADA EN EL CUTOVER RDA (2026-06-16, PBI #37513).
--
--  Era el outbox LEGACY del camino RDA -> Minsalud (antipatron ADR-011):
--      EncounterCommand -> EncounterHandler -> INSERT rda.outbox_messages
--          -> Indigo.AzIHCERelayOutbox (UPDLOCK/READPAST, status Pending->Completed)
--          -> topic interoperabilidad-qa-v3 / sub-rda-handler.
--  En el cutover se ELIMINARON del codigo su productor (EncounterCommand/EncounterHandler)
--  y su relay (Indigo.AzIHCERelayOutbox); el worker IHCE consume SOLO eventos canonicos
--  (ehr-source-events/rda). Esta tabla YA NO tiene escritores ni lectores.
--
--  NO se DROPea en el DACPAC (mismo criterio que [dbo].[ADAUTOSER]): la baja fisica es una
--  accion MANUAL del DBA, tras confirmar 0 filas con processed_at IS NULL (sin mensajes en
--  vuelo al corte). Se conserva la definicion para no generar un DROP TABLE destructivo en
--  el diff de despliegue.
-- =============================================================================
CREATE TABLE [rda].[outbox_messages] (
    [id]             UNIQUEIDENTIFIER   NOT NULL,
    [event_type]     NVARCHAR (256)     NOT NULL,
    [aggregate_id]   NVARCHAR (128)     NOT NULL,
    [payload]        NVARCHAR (MAX)     NOT NULL,
    [correlation_id] NVARCHAR (128)     NOT NULL,
    [causation_id]   NVARCHAR (128)     NOT NULL,
    [created_at]     DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NOT NULL,
    [processed_at]   DATETIMEOFFSET (7) NULL,
    [retry_count]    INT                DEFAULT ((0)) NOT NULL,
    [max_retries]    INT                DEFAULT ((10)) NOT NULL,
    [next_retry_at]  DATETIMEOFFSET (7) NULL,
    [status]         NVARCHAR (32)      DEFAULT ('Pending') NOT NULL,
    [error_message]  NVARCHAR (MAX)     NULL,
    [destination]    NVARCHAR (128)     NOT NULL,
    [priority]       INT                DEFAULT ((0)) NOT NULL,
    [tenant_id]      NVARCHAR (64)      NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DEPRECADA (cutover RDA 2026-06-16, PBI #37513). Outbox LEGACY del camino RDA->Minsalud (antipatron ADR-011), reemplazado por el pipeline canonico (ehr-source-events/rda). Sin escritores ni lectores: su productor (EncounterCommand/EncounterHandler) y su relay (Indigo.AzIHCERelayOutbox) fueron eliminados del codigo. NO se DROPea en el DACPAC (igual que dbo.ADAUTOSER); baja fisica manual por DBA tras confirmar 0 filas con processed_at NULL.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'outbox_messages';
GO
