CREATE TABLE [Billing].[RCM_StateTracking] (
    [TrackingID]    INT            IDENTITY (1, 1) NOT NULL,
    [EntityName]    NVARCHAR (50)  NOT NULL,
    [EntityID]      VARCHAR (20)   NOT NULL,
    [StepID]        INT            NOT NULL,
    [ResponsibleID] INT            NOT NULL,
    [StartTime]     DATETIME2 (7)  NOT NULL,
    [EndTime]       DATETIME2 (7)  NULL,
    [Status]        NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK__RCM_Stat__3C19EDD1FC81C2D8] PRIMARY KEY CLUSTERED ([TrackingID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del documento en RCM (NVARCHAR 255): pendiente, procesando, aprobado, rechazado, glosado, cobrado, cancelado. Indica el resultado del paso de seguimiento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el estado del registro de seguimiento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo (DATETIME2) de cierre del paso actual; nulo si en progreso. Tiempo de finalización de la etapa de billing o cobro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'EndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el tiempo final del seguimiento de estado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'EndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'EndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo (DATETIME2) de inicio del seguimiento en el paso actual. Registro de cuándo la factura/glosa ingresó a la etapa RCM.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'StartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el tiempo de inicio del seguimiento de estado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'StartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'StartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del profesional o unidad funcional responsable (INT) del paso actual. Usuario, área de billing, cobros o gestor de glosas asignado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'ResponsibleID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del responsable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'ResponsibleID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'ResponsibleID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del paso o etapa del flujo RCM (INT). Corresponde a la secuencia de procesos: radicación, validación, envío a aseguradora, glosa, cobro, cierre.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'StepID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del paso.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'StepID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'StepID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad (VARCHAR 20): número de factura, glosa, reclamación o documento de facturación. Referencia única del documento en seguimiento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'EntityID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'EntityID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'EntityID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad facturada (NVARCHAR 50): factura, glosa, reclamación, contrato, RIPS u otro documento de billing. Tipo de documento bajo seguimiento RCM.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de seguimiento RCM. Clave primaria para rastrear el movimiento de facturas, glosas y documentos de facturación en el flujo de cobro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'TrackingID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del seguimiento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'TrackingID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'TrackingID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de seguimiento del ciclo de ingresos (RCM): guarda el historial de estados y pasos por los que pasa cada entidad de facturación (cuenta, factura, glosa, reclamación, etc.), indicando quién es el responsable, en qué paso del flujo se encuentra y en qué momento inició y terminó cada etapa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking';
