CREATE TABLE [RegulatoryEngine].[ValidationLog] (
    [Id]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [RegulatoryRuleId] BIGINT         NOT NULL,
    [EntityType]       NVARCHAR (100) NOT NULL,
    [EntityId]         BIGINT         NOT NULL,
    [ValidationDate]   DATETIME2 (7)  NOT NULL,
    [Result]           NVARCHAR (20)  NOT NULL,
    [Message]          NVARCHAR (MAX) NULL,
    [ContextJson]      NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ValidationLog_Rule] FOREIGN KEY ([RegulatoryRuleId]) REFERENCES [RegulatoryEngine].[RegulatoryRule] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de auditoría de cada ejecución del motor regulatorio. Almacena el resultado (PASS/BLOCK/WARN/AUDIT/ERROR) junto con el contexto clínico serializado (paciente, ingreso, valores del contexto). Permite trazabilidad regulatoria, auditoría de bloqueos y análisis de patrones de incumplimiento.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'ValidationLog';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de validación.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'ValidationLog', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a la regla regulatoria que produjo este log. Permite agrupar todos los resultados históricos de una misma regla para análisis de cumplimiento.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'ValidationLog', @level2type = N'COLUMN', @level2name = N'RegulatoryRuleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de entidad clínica sobre la que se ejecutó la validación (ej: Encounter, ServiceRequest, Claim). Permite filtrar logs por dominio clínico.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'ValidationLog', @level2type = N'COLUMN', @level2name = N'EntityType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad clínica concreta que fue validada (ej: Id del ingreso, Id de la solicitud de servicio). Junto con EntityType permite trazar el historial de validaciones de un registro específico.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'ValidationLog', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que se ejecutó la validación.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'ValidationLog', @level2type = N'COLUMN', @level2name = N'ValidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de la validación. Valores posibles: PASS (aprobó), BLOCK (bloqueó el proceso), WARN (advirtió sin bloquear), AUDIT (solo registró), ERROR (fallo técnico durante la ejecución).', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'ValidationLog', @level2type = N'COLUMN', @level2name = N'Result';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje descriptivo del resultado, especialmente en casos de BLOCK, WARN o ERROR. Contiene la razón del rechazo o la advertencia generada por la regla. NULL cuando el resultado es PASS sin observaciones.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'ValidationLog', @level2type = N'COLUMN', @level2name = N'Message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contexto de validación serializado en JSON tal como fue recibido en la solicitud (ej: {"UserType":3,"EntityType":12,"ServiceDate":"2026-06-20"}). Permite reproducir y auditar la validación con los datos exactos que la originaron.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'ValidationLog', @level2type = N'COLUMN', @level2name = N'ContextJson';
