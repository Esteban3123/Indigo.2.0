CREATE TABLE [Contract].[Contract] (
    [Id]                     INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractEntityId]       INT            NULL,
    [HealthAdministratorId]  INT            NOT NULL,
    [Code]                   VARCHAR (20)   NOT NULL,
    [ContractName]           VARCHAR (100)  NULL,
    [ContractNumber]         VARCHAR (30)   NULL,
    [ContractValue]          NUMERIC (18)   NOT NULL,
    [ExecuteValue]           NUMERIC (18)   NOT NULL,
    [InitialDate]            DATETIME       NULL,
    [EndDate]                DATETIME       NULL,
    [Legalized]              BIT            NULL,
    [DateLegalization]       DATETIME       NULL,
    [Observations]           VARCHAR (MAX)  NULL,
    [ContractObject]         VARCHAR (MAX)  NOT NULL,
    [PrintingMode]           TINYINT        NULL,
    [TerminationControl]     TINYINT        NULL,
    [NotificationValueType]  TINYINT        NULL,
    [PercentageNotification] NUMERIC (5, 2) NULL,
    [NotificationValue]      NUMERIC (18)   NULL,
    [NotificationTimeType]   TINYINT        NULL,
    [NotificationDays]       INT            NULL,
    [Status]                 TINYINT        NOT NULL,
    [CreationUser]           VARCHAR (20)   NOT NULL,
    [CreationDate]           DATETIME       NOT NULL,
    [ModificationUser]       VARCHAR (20)   NULL,
    [ModificationDate]       DATETIME       NULL,
    [InForceUser]            VARCHAR (20)   NULL,
    [InForceDate]            DATETIME       NULL,
    [SuspendedUser]          VARCHAR (20)   NULL,
    [SuspendedDate]          DATETIME       NULL,
    [FinishedUser]           VARCHAR (20)   NULL,
    [FinishedDate]           DATETIME       NULL,
    CONSTRAINT [PK_Contract__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Contract_ContractEntity] FOREIGN KEY ([ContractEntityId]) REFERENCES [Contract].[ContractEntity] ([Id]),
    CONSTRAINT [FK_Contract_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id])
);






GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Contract__Code]
    ON [Contract].[Contract]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de terminación del contrato (DATETIME). Momento exacto en que finalizó o se dio por concluido el acuerdo contractual con la entidad aseguradora.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'FinishedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que finalizo el contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'FinishedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'FinishedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que finalizó el contrato (VARCHAR 20). Identificación del operario o administrador que registró la terminación del contrato en el sistema.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'FinishedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que finalizo el contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'FinishedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'FinishedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de suspensión del contrato (DATETIME). Momento exacto en que se pausó o interrumpió temporalmente la vigencia y ejecución del contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'SuspendedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se suspendio el contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'SuspendedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'SuspendedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que suspendió el contrato (VARCHAR 20). Identificación del operario que registró la suspensión del acuerdo contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'SuspendedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que suspendio el contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'SuspendedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'SuspendedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de activación a estado vigente (DATETIME). Momento en que el contrato adquirió validez y comenzó su ejecución oficial.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'InForceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se cambio a estado vigente', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'InForceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'InForceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que activó a estado vigente (VARCHAR 20). Identificación del operario que cambió el estado del contrato a vigente o activo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'InForceUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que cambio a estado vigente', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'InForceUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'InForceUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última modificación (DATETIME). Timestamp del último cambio realizado a los datos del contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modificó el contrato (VARCHAR 20). Identificación del operario que realizó la última actualización de datos contractuales.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación del contrato (DATETIME). Timestamp de registro inicial del contrato en el sistema.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el contrato (VARCHAR 20). Identificación del operario que registró el contrato por primera vez.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del contrato (TINYINT): 1=Vigente (activo), 2=Suspendido (pausado), 3=Terminado (finalizado). Código que refleja el ciclo de vida actual del acuerdo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del contrato  1 - Vigente  2 - Suspendido  3 - Terminado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de anticipación para notificación (INT). Cantidad de días previos al evento de control (fecha o valor) en que se genera alerta.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Días de notificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de notificación por tiempo (TINYINT): 1=Ninguna, 2=Días de anterioridad. Define si se alerta N días antes del vencimiento.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationTimeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de notificacion para cuando es control por fecha  1 - Ninguno  2 - Dias de anterioridad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationTimeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationTimeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de notificación (NUMERIC 18). Monto o parámetro numérico asociado al disparo de alertas de control contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la Notificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de notificación (NUMERIC 5,2). Umbral porcentual de ejecución del contrato que activa alertas o auditorías de gasto.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PercentageNotification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Notificación de porcentaje', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PercentageNotification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PercentageNotification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de notificación por valor (TINYINT): 1=Ninguna, 2=% del valor del contrato, 3=Valor fijo. Define cómo se calcula el límite de alerta.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationValueType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de notificacion para cuando el control de terminacion es de valor  1 - Ninguna  2 - % del valor del contrato  3 - Valor fijo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationValueType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationValueType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de terminación del contrato (TINYINT): 1=Ninguno, 2=Por fecha, 3=Por valor gastado, 4=Por fecha o valor. Mecanismo que determina cuándo termina.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TerminationControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control de terminacion del contrato  1- Ninguno  2 - Fecha Terminacion  3 - Valor contrato  4 - Fecha Terminacion o Valor Contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TerminationControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TerminationControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modo de impresión de reportes (TINYINT): 1=Manual Tarifario, 2=CUPS, 3=Código RIPS. Formato de facturación y reporte a entes reguladores.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PrintingMode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modo de impresion de los reportes  1 - Manual Tarifario  2 - CUPS   3 - Codigo RIPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PrintingMode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PrintingMode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Objeto del contrato (VARCHAR MAX). Descripción detallada del propósito, alcance y servicios cubiertos por el acuerdo contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractObject';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Objeto del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractObject';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractObject';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas del contrato (VARCHAR MAX). Campo de anotaciones libres con detalles adicionales, condiciones especiales o comentarios administrativos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de legalización del contrato (DATETIME). Momento en que el contrato fue formalizado legalmente y adquirió validez jurídica.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'DateLegalization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de legalizacion del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'DateLegalization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'DateLegalization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Contrato legalizado? (BIT, 0/1). Indicador booleano de si el acuerdo ha completado trámites legales y es vinculante.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Legalized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contrato esta legalizado ?', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Legalized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Legalized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final (vencimiento) del contrato (DATETIME). Fecha pactada en que termina la vigencia del acuerdo, independiente de terminación anticipada.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial del contrato (DATETIME). Fecha de inicio de la vigencia y ejecución del acuerdo contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor ejecutado del contrato (NUMERIC 18). Monto gastado o facturado hasta la fecha dentro del presupuesto contratado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ExecuteValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor ejecutado del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ExecuteValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ExecuteValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del contrato (NUMERIC 18). Monto base del acuerdo; presupuesto máximo autorizado para la vigencia contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del contrato (VARCHAR 100). Identificador único alfanumérico del contrato otorgado por la entidad aseguradora o tercero.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del contrato (VARCHAR 100). Título o denominación del acuerdo que facilita identificación y búsqueda rápida.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del contrato (VARCHAR 20). Código corto único (PK lógico) para identificación ágil en búsquedas y reportes internos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero/asegurador (INT, FK). Referencia a la administradora de salud, EPS, aseguradora o ente con quien se suscribe el contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero con quien se realiza el contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad de contrato (INT, FK). Referencia a la sede, unidad funcional o entidad jurídica interna que celebra el contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad de contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del contrato (INT, PK Identity). Clave primaria autoincremental que identifica unívocamente cada registro de contrato en el sistema.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Id';


GO
CREATE NONCLUSTERED INDEX [IX_Contract_Id_Covering]
    ON [Contract].[Contract]([Id] ASC)
    INCLUDE([Code], [ContractName]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contratos suscritos con entidades pagadoras o administradoras de salud (EPS, aseguradoras, etc.). Registra los datos principales de cada contrato: valor pactado, vigencia, estado (legalizado, vigente, suspendido, terminado) y parámetros de notificación por vencimiento o ejecución presupuestal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract';
