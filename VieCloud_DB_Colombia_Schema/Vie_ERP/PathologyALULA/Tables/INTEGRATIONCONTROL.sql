CREATE TABLE [PathologyALULA].[INTEGRATIONCONTROL] (
    [id]                   INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [idpathology]          INT         NOT NULL,
    [CUPS]                 CHAR (20)   NOT NULL,
    [ordertype]            VARCHAR (3) NOT NULL,
    [registrationdate]     DATETIME    NOT NULL,
    [coderegistrationuser] CHAR (20)   NOT NULL,
    CONSTRAINT [PK_PATHOLOGYINTEGRATION] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del usuario profesional de la salud que registra la interfaz de integración de patología; clave de auditoría para rastrear quién cargó la orden', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'coderegistrationuser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo usuario que registra interfaz', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'coderegistrationuser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'coderegistrationuser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de integración; timestamp de cuándo se procesó la orden de patología en el sistema', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'registrationdate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha creacion registro', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'registrationdate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'registrationdate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de orden clínica: INT (Intrahospitalario/urgencia/internado) o AMB (Ambulatorio/consulta externa); clasifica el contexto de atención', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'ordertype';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Orden:  INT - Intrahospitalario  AMB - Ambulatorio', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'ordertype';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'ordertype';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (Clasificación Única de Procedimientos en Salud) enviado; identifica el procedimiento de patología, laboratorio o examen diagnostico facturado', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'CUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo CUPS enviado', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'CUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'CUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de patología origen, referencia a HCORDPATO (historia clínica) o AMBORPATO (ambulatorio); vincula el control con la orden clínica', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'idpathology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de orden de patologia puede ser de la tabla (HCORDPATO - AMBORPATO)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'idpathology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'idpathology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable de la tabla INTEGRATIONCONTROL; clave primaria para auditar cada sincronización de órdenes de patología', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico tabla', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de control de integración entre el módulo de patología ALULA y el sistema central. Guarda el historial de órdenes o servicios enviados, indicando qué examen de patología fue integrado, el código CUPS asociado, el tipo de orden y quién lo registró.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL';
