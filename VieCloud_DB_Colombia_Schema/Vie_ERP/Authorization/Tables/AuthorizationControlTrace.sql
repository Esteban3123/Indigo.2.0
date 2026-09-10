-- =============================================================================
-- NUEVO (2026-06-15) — Tarea 37517.
-- Traza (historial de transiciones) del modelo integral de control de autorizaciones.
-- Registra, en append-only, cada cambio de estado de un [Authorization].[AuthorizationControl]:
-- estado anterior y nuevo, usuario, fecha, acción ejecutada y justificación.
-- Reemplaza funcionalmente la trazabilidad antes implícita en [dbo].[ADAUTOSER].
-- Ver docs/architecture/MODELO-SUSCEPTIBILIDAD-Y-CONTROL.md §6 y ADR-008 (la
-- trazabilidad del dashboard se sirve desde SQL, no desde Cosmos ni la outbox).
--
-- Patrón append-only: cada transición es una fila nueva; no se actualizan filas de
-- traza existentes. El estado vigente del control vive en la cabecera
-- ([AuthorizationControl].[Status]); esta tabla conserva el camino completo.
-- =============================================================================
CREATE TABLE [Authorization].[AuthorizationControlTrace] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationControlId] INT           NOT NULL,
    [PreviousStatus]         TINYINT       NULL,
    [NewStatus]              TINYINT       NOT NULL,
    [Action]                 TINYINT       NOT NULL,
    [Justification]          VARCHAR (MAX) NULL,
    [ActionUser]             VARCHAR (20)  NOT NULL,
    [ActionDate]             DATETIME      NOT NULL CONSTRAINT [DF_AuthorizationControlTrace_ActionDate] DEFAULT (Common.GETDATE()),
    CONSTRAINT [PK_AuthorizationControlTrace] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationControlTrace_AuthorizationControl] FOREIGN KEY ([AuthorizationControlId]) REFERENCES [Authorization].[AuthorizationControl] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AuthorizationControlTrace_ControlId]
    ON [Authorization].[AuthorizationControlTrace]([AuthorizationControlId] ASC, [ActionDate] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Traza append-only del historial de transiciones de estado de los registros de [Authorization].[AuthorizationControl]. Cada fila documenta un cambio de estado (estado anterior y nuevo), la acción ejecutada, el usuario, la fecha y la justificación. Sustenta la pestaña de Trazabilidad del dashboard (ADR-008) sin depender de Cosmos ni de la outbox clínica.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la fila de traza.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a [Authorization].[AuthorizationControl]) del registro de control cuyo cambio de estado se traza.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'AuthorizationControlId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'AuthorizationControlId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado anterior (TINYINT, NULL) del control antes de la transición. Mismos valores que [AuthorizationControl].[Status] (1=Pendiente, 2=Solicitado, 3=Autorizado, 4=Anulado). NULL en la fila inicial de creación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'PreviousStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'PreviousStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado nuevo (TINYINT) del control después de la transición. Mismos valores que [AuthorizationControl].[Status] (1=Pendiente, 2=Solicitado, 3=Autorizado, 4=Anulado).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'NewStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'NewStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Acción ejecutada (TINYINT) que originó la transición: 1=Crear, 2=Solicitar, 3=Autorizar, 4=Anular, 5=Modificar. Documenta el verbo del cambio, complementario al par estado anterior/nuevo.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'Action';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'Action';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación o nota (VARCHAR MAX, NULL) asociada a la transición (motivo de solicitud, de autorización o de anulación, según la acción).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'Justification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'Justification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario (VARCHAR 20) que ejecutó la acción que originó la transición. Trazabilidad de quién cambió el estado.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'ActionUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'ActionUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se ejecutó la acción / transición. Por defecto Common.GETDATE() para usar la hora institucional. Ordena el historial.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'ActionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'ActionDate';
