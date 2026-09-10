CREATE TABLE [dbo].[PADORDEN] (
    [ID]                                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADCONTROL]                      INT             NOT NULL,
    [RESUMENHC]                         VARCHAR (MAX)   NULL,
    [PERTINENCIA]                       VARCHAR (MAX)   NULL,
    [HORAALDIA]                         INT             NULL,
    [DIASALMES]                         INT             NULL,
    [CAMBIOPOSICION]                    BIT             NULL,
    [SUMINISTROALIMENT]                 BIT             NULL,
    [ASEOBANOPACIENTE]                  BIT             NULL,
    [LUBRICACIONPIEL]                   BIT             NULL,
    [ASISTENCIAMOVILIZACION]            BIT             NULL,
    [ADMINISTRACIONENDOVENOSO]          BIT             NULL,
    [ALIMENTACIONPARENTAL]              BIT             NULL,
    [SONDAVESICAL]                      BIT             NULL,
    [VIGILANCIAHERIDAS]                 BIT             NULL,
    [CATETERESPERIFERICOS]              BIT             NULL,
    [OBSERVACIONENFERMERIA]             VARCHAR (MAX)   NULL,
    [VISITAMEDICA_ACTUALIZACIONFORMULA] BIT             NULL,
    [VISITAMEDICA_DIASPORMES]           INT             NULL,
    [VISITAMEDICA_OBSERVACIONES]        VARCHAR (MAX)   NULL,
    [HERIDAS_DIASPORMES]                INT             NULL,
    [HERIDAS_FRECUENCIA]                VARCHAR (200)   NULL,
    [HERIDAS_MOTIVO]                    VARCHAR (MAX)   NULL,
    [HERIDAS_OBSERVACIONES]             VARCHAR (MAX)   NULL,
    [OXILISTROS]                        DECIMAL (18, 2) NULL,
    [OXIHORASDIA]                       INT             NULL,
    [OXIDIASALMES]                      INT             NULL,
    [OXIBALADOMICILIARIA]               BIT             NULL,
    [OXIBALAPORTATIL]                   BIT             NULL,
    [OXICODVIAADM]                      CHAR (3)        NULL,
    [OXIOBSERVACIONES]                  VARCHAR (MAX)   NULL,
    [AMBTIPOAMBULANCIA]                 INT             NULL,
    [AMBTELEFONO]                       VARCHAR (100)   NULL,
    [AMBNOMBRERESPONSABLE]              VARCHAR (200)   NULL,
    [AMBOBSERVACIONES]                  VARCHAR (MAX)   NULL,
    [OBSERVACIONFORMULAMED]             VARCHAR (MAX)   NULL,
    [FECHAREGISTRO]                     DATETIME        NOT NULL,
    [CODUSUARIO]                        CHAR (20)       NOT NULL,
    CONSTRAINT [PK_PHDORDEN] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PHDORDEN_PHDCONTROL] FOREIGN KEY ([IDPADCONTROL]) REFERENCES [dbo].[PADCONTROL] ([ID])
);


GO
ALTER TABLE [dbo].[PADORDEN] NOCHECK CONSTRAINT [FK_PHDORDEN_PHDCONTROL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código usuario de creación del registro; identificador del profesional de salud que registra la orden; CHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro en la base de datos; timestamp de auditoría; DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones generales de la fórmula médica; notas clínicas sobre medicamentos, dosis y tratamientos prescritos; VARCHAR(MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OBSERVACIONFORMULAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones generales de la formula medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OBSERVACIONFORMULAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OBSERVACIONFORMULAMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones del traslado en ambulancia; detalles adicionales sobre transporte del paciente; VARCHAR(MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'AMBOBSERVACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones del traslado de ambulancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'AMBOBSERVACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'AMBOBSERVACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del responsable o conductor del traslado de paciente en ambulancia; contacto de la empresa transportadora; VARCHAR(200)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'AMBNOMBRERESPONSABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Responsable de traslado de paciente en ambulancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'AMBNOMBRERESPONSABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'AMBNOMBRERESPONSABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de contacto de la empresa de ambulancia; número para coordinación de traslado; VARCHAR(100)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'AMBTELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Telefono contacto de ambulancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'AMBTELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'AMBTELEFONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ambulancia para traslado: 1=Básica (sin equipamiento médico), 2=Medicada (con soporte vital); INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'AMBTIPOAMBULANCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Traslado de ambulacia -  tipo de ambulaciona :  1 - Basica  2 - Medicada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'AMBTIPOAMBULANCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'AMBTIPOAMBULANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones generales del tratamiento de oxígeno; notas sobre terapia oxigenoterapia domiciliaria; VARCHAR(MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIOBSERVACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones generales de tratamiento de oxigeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIOBSERVACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIOBSERVACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código vía de administración de oxígeno; catálogo: cánula, máscara, tienda; CHAR(3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXICODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo via de administracion de oxigeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXICODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXICODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de bala/cilindro de oxígeno portátil para paciente ambulatorio; true=sí, false=no; BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIBALAPORTATIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bala portatil  true: si  false: no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIBALAPORTATIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIBALAPORTATIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de bala/cilindro de oxígeno domiciliaria para terapia en casa; true=sí, false=no; BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIBALADOMICILIARIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bala domiciliaria  true - SI  false -NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIBALADOMICILIARIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIBALADOMICILIARIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días al mes que se requiere oxigenoterapia; frecuencia de uso mensual; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIDIASALMES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cantidad de dias al mes de oxigeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIDIASALMES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIDIASALMES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de horas al día de administración de oxígeno; duración diaria de la terapia; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIHORASDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horas al dia de oxigeno ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIHORASDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIHORASDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de litros por minuto de oxígeno prescrito; flujo de oxígeno requerido; DECIMAL(18,2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXILISTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cantidad de litros de oxigeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXILISTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXILISTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clínica de Heridas - Observaciones generales; notas sobre cuidado, evolución y características de la lesión; VARCHAR(MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HERIDAS_OBSERVACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clinica de Heridas  - Observaciones generales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HERIDAS_OBSERVACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HERIDAS_OBSERVACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clínica de Heridas - Motivos de solicitud; razón clínica de la intervención: úlcera por presión, post-quirúrgica, diabética; VARCHAR(MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HERIDAS_MOTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clinica de Heridas  - Motivos de solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HERIDAS_MOTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HERIDAS_MOTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clínica de Heridas - Frecuencia de atención; periodicidad de las curaciones: diaria, 2-3 veces/semana; VARCHAR(200)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HERIDAS_FRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clinica de Heridas  -Frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HERIDAS_FRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HERIDAS_FRECUENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clínica de Heridas - Cantidad de días por mes requerida; número de atenciones mensuales; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HERIDAS_DIASPORMES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clinica de Heridas  - Cantidad de dias X mes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HERIDAS_DIASPORMES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HERIDAS_DIASPORMES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visita Médica - Observaciones generales; notas del seguimiento y evaluación clínica periódica; VARCHAR(MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'VISITAMEDICA_OBSERVACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visita Medica - Observaciones generales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'VISITAMEDICA_OBSERVACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'VISITAMEDICA_OBSERVACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visita Médica - Cantidad de visitas por mes; frecuencia de seguimiento médico domiciliario; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'VISITAMEDICA_DIASPORMES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visita Medica - cuantas visitas por mes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'VISITAMEDICA_DIASPORMES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'VISITAMEDICA_DIASPORMES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visita Médica - Actualización de fórmulas; indicador de cambio en medicamentos u oxígeno; true=requiere, false=no; BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'VISITAMEDICA_ACTUALIZACIONFORMULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visita Medica - Actualizacion de formulas (medicamentos, oxigeno)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'VISITAMEDICA_ACTUALIZACIONFORMULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'VISITAMEDICA_ACTUALIZACIONFORMULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuidados Básicos - Observaciones generales de cuidados de enfermería; notas sobre atención domiciliaria integral; VARCHAR(MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OBSERVACIONENFERMERIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuidados Basicos - Observacion generales de cuidados enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OBSERVACIONENFERMERIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OBSERVACIONENFERMERIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuidados Básicos - Manejo de catéteres periféricos y su cambio; cateterismo venoso periférico y procedimientos asociados; BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'CATETERESPERIFERICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuidados Basicos - Manejo de cateteres perifericos y cambio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'CATETERESPERIFERICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'CATETERESPERIFERICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuidados Básicos - Vigilancia y cuidado de heridas; monitoreo de lesiones, curaciones y control de infecciones; BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'VIGILANCIAHERIDAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuidados Basicos - Cuidado y vigilancia de heridas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'VIGILANCIAHERIDAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'VIGILANCIAHERIDAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuidados Básicos - Manejo de sonda vesical; cateterismo urinario y cuidados periódicos; BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'SONDAVESICAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuidados Basicos - manejo de sonda vesical', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'SONDAVESICAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'SONDAVESICAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuidados Básicos - Alimentación parenteral; nutrición intravenosa total o parcial; BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ALIMENTACIONPARENTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuidados Basicos -Alimentacion parental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ALIMENTACIONPARENTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ALIMENTACIONPARENTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuidados Básicos - Administración de medicamentos endovenosos; inyecciones IV y tratamientos intravenosos; BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ADMINISTRACIONENDOVENOSO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuidados Basicos - Administtracion de medicamentos endovenosos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ADMINISTRACIONENDOVENOSO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ADMINISTRACIONENDOVENOSO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuidados Básicos - Asistencia para movilización; ayuda en cambios posturales y movimiento del paciente; BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ASISTENCIAMOVILIZACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuidados Basicos - Asistencia para movilizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ASISTENCIAMOVILIZACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ASISTENCIAMOVILIZACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuidados Básicos - Lubricación de piel; hidratación e higiene dermatológica preventiva; BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'LUBRICACIONPIEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuidados Basicos - Lubricacion de piel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'LUBRICACIONPIEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'LUBRICACIONPIEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuidados Básicos - Aseo y baño del paciente; higiene personal e higiene íntima; BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ASEOBANOPACIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuidados Basicos - aseo y baño del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ASEOBANOPACIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ASEOBANOPACIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuidados Básicos - Suministro de alimentación; provisión de comidas y nutrición oral; BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'SUMINISTROALIMENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuidados Basicos - suministri de alimentacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'SUMINISTROALIMENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'SUMINISTROALIMENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuidados Básicos - Cambios de posición; movilización periódica para prevención de úlceras por presión; BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'CAMBIOPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuidados Basicos - cambios de posicion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'CAMBIOPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'CAMBIOPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días al mes que se requiere servicio de enfermería; frecuencia mensual de atención; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'DIASALMES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cantidad de dias al mes de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'DIASALMES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'DIASALMES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de horas al día de cobertura de enfermería; duración diaria del servicio de cuidados; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HORAALDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'horas al dia de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HORAALDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HORAALDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pertinencia clínica para la Atención en Cuidados Domiciliarios (PHD) y plan de manejo; justificación médica de la orden; VARCHAR(MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'PERTINENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'pertinencia para la PHD y plan de manejo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'PERTINENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'PERTINENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resumen de Historia Clínica; condensado de diagnósticos, antecedentes y estado clínico relevante del paciente; VARCHAR(MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'RESUMENHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resumen de HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'RESUMENHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'RESUMENHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de control de PHD (Programa de Atención Domiciliaria); referencia a evaluación inicial y plan de cuidados; INT, FK → PADCONTROL.ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'IDPADCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID control de PHD (PHDCONTROL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'IDPADCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'IDPADCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico e identificador único de la tabla PADORDEN; clave primaria secuencial; INT IDENTITY(1,1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes de atención domiciliaria del paciente: registra las indicaciones médicas y de enfermería para el cuidado en casa, incluyendo necesidades de oxígeno, traslado en ambulancia, cuidado de heridas, visitas médicas, higiene, alimentación y movilización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN';
