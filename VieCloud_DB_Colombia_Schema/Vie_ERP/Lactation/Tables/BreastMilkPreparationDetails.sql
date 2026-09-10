CREATE TABLE [Lactation].[BreastMilkPreparationDetails] (
    [Id]                                       INT             IDENTITY (1, 1) NOT NULL,
    [IdBreastMilkOrder]                        INT             NOT NULL,
    [Status]                                   INT             NOT NULL,
    [ProfessionalCodeLactationRequest]         CHAR (20)       NOT NULL,
    [DateLactationRequest]                     DATETIME        NOT NULL,
    [ProfessionalCodeFormulaPreparationStart]  CHAR (20)       NULL,
    [DateFormulaPreparationStart]              DATETIME        NULL,
    [PreparationVolume]                        NUMERIC (18, 1) NULL,
    [IdBreastMilkIntakeRecords]                INT             NULL,
    [ProfessionalCodeFormulaPreparationEnd]    CHAR (20)       NULL,
    [DateFormulaPreparationEnd]                DATETIME        NULL,
    [Batch]                                    VARCHAR (20)    NULL,
    [BatchObservations]                        VARCHAR (60)    NULL,
    [QuantityPrepared]                         INT             NULL,
    [ProfessionalCodeFormulaDelivery]          CHAR (20)       NULL,
    [DateFormulaDelivery]                      DATETIME        NULL,
    [ProfessionalCodePreparationCompleted]     CHAR (20)       NULL,
    [DatePreparationCompleted]                 DATETIME        NULL,
    [ProfessionalCodeAllPreparationsCompleted] CHAR (20)       NULL,
    [DateAllPreparationsCompleted]             DATETIME        NULL,
    [ProfessionalCodeFormulaCancellation]      CHAR (20)       NULL,
    [DateFormulaCancellation]                  DATETIME        NULL,
    [JustificationCancellation]                VARCHAR (500)   NULL,
    [IdCODMOTANU]                              CHAR (4)        NULL,
    CONSTRAINT [PK_BreastMilkPreparationDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BreastMilkPreparationDetails_IdBreastMilkOrder_BreastMilkOrder] FOREIGN KEY ([IdBreastMilkOrder]) REFERENCES [Lactation].[BreastMilkOrder] ([Id]),
    CONSTRAINT [FK_BreastMilkPreparationDetails_ProfessionalCodeAllPreparationsCompleted_INPROFSAL] FOREIGN KEY ([ProfessionalCodeAllPreparationsCompleted]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_BreastMilkPreparationDetails_ProfessionalCodeFormulaCancellation_INPROFSAL] FOREIGN KEY ([ProfessionalCodeFormulaCancellation]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_BreastMilkPreparationDetails_ProfessionalCodeFormulaDelivery_INPROFSAL] FOREIGN KEY ([ProfessionalCodeFormulaDelivery]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_BreastMilkPreparationDetails_ProfessionalCodeFormulaPreparationEnd_INPROFSAL] FOREIGN KEY ([ProfessionalCodeFormulaPreparationEnd]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_BreastMilkPreparationDetails_ProfessionalCodeFormulaPreparationStart_INPROFSAL] FOREIGN KEY ([ProfessionalCodeFormulaPreparationStart]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_BreastMilkPreparationDetails_ProfessionalCodeLactationRequest_INPROFSAL] FOREIGN KEY ([ProfessionalCodeLactationRequest]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_BreastMilkPreparationDetails_ProfessionalCodePreparationCompleted_INPROFSAL] FOREIGN KEY ([ProfessionalCodePreparationCompleted]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CODMOTANU (4 caracteres) del motivo de anulación/cancelación de la preparación de leche materna. Referencia a catálogo de motivos.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'IdCODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de la anulación', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'IdCODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'IdCODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación textual (VARCHAR 500) de la cancelación y/o anulación de la preparación de leche materna. Razón documentada por el profesional lactario.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'JustificationCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion de la cancelación  y/o anulacion', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'JustificationCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'JustificationCancellation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp (DATETIME) en que se anula/cancela la preparación de leche materna desde el dashboard lactario. Momento exacto de la anulación.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora en que se anula la preparación de leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaCancellation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (INPROFSAL.CODPROSAL) que ejecuta la anulación de la preparación. FK a tabla INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional que realiza la anulación de la preparación de leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaCancellation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp (DATETIME) en que se completan todas las preparaciones de la orden de leche materna. Fecha de cierre total del lote.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateAllPreparationsCompleted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha en que se completan todas las preparaciones', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateAllPreparationsCompleted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateAllPreparationsCompleted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (INPROFSAL.CODPROSAL) que marca todas las preparaciones como completadas. FK a tabla INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeAllPreparationsCompleted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que completa todas las preparaciones', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeAllPreparationsCompleted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeAllPreparationsCompleted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp (DATETIME) en que se completa esta preparación individual (ej: 5 de 8 teteros). Marca el fin de una porción específica.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DatePreparationCompleted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la  que se completa esta preparacion, es decir  5 de / (5/8)  "5 de 8 teteros"', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DatePreparationCompleted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DatePreparationCompleted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (INPROFSAL.CODPROSAL) que completa esta preparación individual. FK a tabla INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodePreparationCompleted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que completa esta preparacion, es decir  5 de / (5/8)  "5 de 8 teteros"', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodePreparationCompleted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodePreparationCompleted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp (DATETIME) en que se entrega la leche materna preparada al paciente/recién nacido o cuidador. Momento de entrega clínica.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaDelivery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora en que se entrega la leche materna al paciente o cuidador', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaDelivery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaDelivery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (INPROFSAL.CODPROSAL) responsable de la entrega de leche materna. FK a tabla INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaDelivery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional que realiza la entrega de la leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaDelivery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaDelivery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de porciones/teteros preparados (INT). Se registra al finalizar la preparación.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'QuantityPrepared';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad preparada, se diligencia cuando se finaliza la preparación', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'QuantityPrepared';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'QuantityPrepared';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones adicionales del lote de leche materna (VARCHAR 60). Notas sobre la calidad, incidencias o particularidades.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'BatchObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones del lote', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'BatchObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'BatchObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de lote (VARCHAR 20) asignado cuando se finaliza la preparación de leche materna. Código de trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'Batch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lote: Se asigna cuando se fianliza la leche lactea', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'Batch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'Batch';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp (DATETIME) en que finaliza la preparación de la leche materna. Cierre del proceso de elaboración.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaPreparationEnd';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora en que finaliza la preparación de la leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaPreparationEnd';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaPreparationEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (INPROFSAL.CODPROSAL) que finaliza la preparación de leche materna. FK a tabla INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaPreparationEnd';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional que finaliza la preparación de la leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaPreparationEnd';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaPreparationEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del registro de recepción/ingesta de leche materna en tabla relacionada. Relación con gestión de consumo.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'IdBreastMilkIntakeRecords';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de gesstión: Registros de recepción de leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'IdBreastMilkIntakeRecords';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'IdBreastMilkIntakeRecords';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen a preparar en mililitros (NUMERIC 18,1). Cantidad en ml de leche materna por tetero.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'PreparationVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen a perparar en ml', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'PreparationVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'PreparationVolume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp (DATETIME) en que se inicia la preparación de la leche materna. Comienzo del proceso de elaboración.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaPreparationStart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora en que se inicia la preparación de la leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaPreparationStart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaPreparationStart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (INPROFSAL.CODPROSAL) que inicia la preparación de leche materna. FK a tabla INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaPreparationStart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional que inicia la preparación de la leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaPreparationStart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaPreparationStart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp (DATETIME) en que se solicita la preparación de leche materna. Momento de la orden desde clínica.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateLactationRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora en que se solicita la preparación de leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateLactationRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateLactationRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (INPROFSAL.CODPROSAL) que solicita la preparación de leche materna. FK a tabla INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeLactationRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional que solicita la preparación de leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeLactationRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeLactationRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (INT 1-7) del ciclo de preparación: 1=Solicitud asignada por lactario, 2=Iniciar preparación, 3=Finalizar preparación, 4=Entregar preparación, 5=Completada preparación individual, 6=Completadas todas las preparaciones, 7=Anulada. Estados del dashboard lactario.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Solicitud asignada por personal lactario desde dashboard  
2 - Iniciar preparación  leche materna   
3 - Finalizar  preparación leche materna   
4 - Entregar preparacion de leche materna  
5 - Completa la preparación (Es decir este registro)
6 - Completa todas las preparaciones (Todos los registros)
7 - Anulada leche materna desde dashboard lactario        
', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la orden de leche materna asociada en tabla BreastMilkOrder. Relación con la orden padre.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'IdBreastMilkOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la orden de leche materna asociada', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'IdBreastMilkOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'IdBreastMilkOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de cada preparación de leche materna. Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único de la preparación de leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de detalles de preparación de leche materna (teteros). Gestiona el ciclo completo de solicitud, preparación, entrega y finalización de porciones de leche materna desde el dashboard lactario. Vinculada a órdenes de leche y registros de ingesta.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla de preparacion de leche materna, es decir los teteros de leche materna

Tabla que se gestiona desde el dashboard lactario', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkPreparationDetails';

