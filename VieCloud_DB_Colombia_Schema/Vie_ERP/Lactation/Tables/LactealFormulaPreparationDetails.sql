CREATE TABLE [Lactation].[LactealFormulaPreparationDetails] (
    [Id]                                       INT           IDENTITY (1, 1) NOT NULL,
    [IdHCPRESCRA]                              INT           NOT NULL,
    [Status]                                   INT           NOT NULL,
    [ProfessionalCodeLactationRequest]         CHAR (20)     NOT NULL,
    [DateLactationRequest]                     DATETIME      NOT NULL,
    [ProfessionalCodeFormulaPreparationStart]  CHAR (20)     NULL,
    [DateFormulaPreparationStart]              DATETIME      NULL,
    [ProfessionalCodeFormulaPreparationEnd]    CHAR (20)     NULL,
    [DateFormulaPreparationEnd]                DATETIME      NULL,
    [Batch]                                    VARCHAR (20)  NULL,
    [BatchObservations]                        VARCHAR (60)  NULL,
    [QuantityPrepared]                         INT           NULL,
    [ProfessionalCodeFormulaDelivery]          CHAR (20)     NULL,
    [DateFormulaDelivery]                      DATETIME      NULL,
    [ProfessionalCodePreparationCompleted]     CHAR (20)     NULL,
    [DatePreparationCompleted]                 DATETIME      NULL,
    [ProfessionalCodeAllPreparationsCompleted] CHAR (20)     NULL,
    [DateAllPreparationsCompleted]             DATETIME      NULL,
    [ProfessionalCodeFormulaCancellation]      CHAR (20)     NULL,
    [DateFormulaCancellation]                  DATETIME      NULL,
    [JustificationCancellation]                VARCHAR (500) NULL,
    [IdCODMOTANU]                              CHAR (4)      NULL,
    CONSTRAINT [PK_LactealFormulaPreparationDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LactealFormulaPreparationDetails_IdCODMOTANU_HCMOANULB] FOREIGN KEY ([IdCODMOTANU]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_LactealFormulaPreparationDetails_IdHCPRESCRA_HCPRESCRA] FOREIGN KEY ([IdHCPRESCRA]) REFERENCES [dbo].[HCPRESCRA] ([ID]),
    CONSTRAINT [FK_LactealFormulaPreparationDetails_ProfessionalCodeAllPreparationsCompleted_INPROFSAL] FOREIGN KEY ([ProfessionalCodeAllPreparationsCompleted]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_LactealFormulaPreparationDetails_ProfessionalCodeFormulaCancellation_INPROFSAL] FOREIGN KEY ([ProfessionalCodeFormulaCancellation]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_LactealFormulaPreparationDetails_ProfessionalCodeFormulaDelivery_INPROFSAL] FOREIGN KEY ([ProfessionalCodeFormulaDelivery]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_LactealFormulaPreparationDetails_ProfessionalCodeFormulaPreparationEnd_INPROFSAL] FOREIGN KEY ([ProfessionalCodeFormulaPreparationEnd]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_LactealFormulaPreparationDetails_ProfessionalCodeFormulaPreparationStart_INPROFSAL] FOREIGN KEY ([ProfessionalCodeFormulaPreparationStart]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_LactealFormulaPreparationDetails_ProfessionalCodeLactationRequest_INPROFSAL] FOREIGN KEY ([ProfessionalCodeLactationRequest]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_LactealFormulaPreparationDetails_ProfessionalCodePreparationCompleted_INPROFSAL] FOREIGN KEY ([ProfessionalCodePreparationCompleted]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
CREATE NONCLUSTERED INDEX [IX_LFPD_IdHCPRESCRA]
    ON [Lactation].[LactealFormulaPreparationDetails]([IdHCPRESCRA] ASC)
    INCLUDE([DateFormulaPreparationStart], [Batch], [BatchObservations]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de motivo de anulación (CHAR 4, FK → HCMOANULB.CODMOTANU). Clasificación estándar de razón de cancelación en lactario.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'IdCODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de la anulación', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'IdCODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'IdCODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación detallada (VARCHAR 500) de la anulación o cancelación de preparación de leche materna. Motivo administrativo o clínico.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'JustificationCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion de la cancelación  y/o anulacion
', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'JustificationCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'JustificationCancellation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se anula la preparación de fórmula láctea. Registro temporal de cancelación.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora en que se anula la preparación de fórmula', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaCancellation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (FK → INPROFSAL.CODPROSAL) que anula la preparación de fórmula láctea desde dashboard lactario. Auditoría de anulación.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional que realiza la anulación de la preparación de fórmula', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaCancellation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se completan todas las preparaciones de la solicitud original. Marca finalización completa del ciclo lactario.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateAllPreparationsCompleted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha en que se completan todas las preparaciones', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateAllPreparationsCompleted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateAllPreparationsCompleted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (FK → INPROFSAL.CODPROSAL) que completa la totalidad de preparaciones de la solicitud. Responsable de cierre global.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeAllPreparationsCompleted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que completa todas las preparaciones', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeAllPreparationsCompleted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeAllPreparationsCompleted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se completa este registro individual de preparación (ej: 5 de 8 tarros). Marca finalización parcial de solicitud.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DatePreparationCompleted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la  que se completa esta preparacion, es decir  5 de / (5/8)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DatePreparationCompleted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DatePreparationCompleted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (FK → INPROFSAL.CODPROSAL) que completa este registro individual de preparación (ej: 5 de 8 tarros). Auditoría por unidad.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodePreparationCompleted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que completa esta preparacion, es decir  5 de / (5/8)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodePreparationCompleted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodePreparationCompleted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se entrega la fórmula láctea al paciente o cuidador. Cierre de gestión de preparación.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaDelivery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora en que se entrega la fórmula al paciente o cuidador', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaDelivery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaDelivery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (FK → INPROFSAL.CODPROSAL) que entrega la fórmula al paciente o cuidador. Responsable de distribución.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaDelivery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional que realiza la entrega de la fórmula', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaDelivery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaDelivery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades preparadas (INT) de fórmula láctea. Se registra al finalizar la preparación en lactario.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'QuantityPrepared';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad preparada, se diligencia cuando se finaliza la preparación', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'QuantityPrepared';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'QuantityPrepared';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas o comentarios (VARCHAR 60) asociados al lote de fórmula láctea preparada. Registro de incidencias o particularidades durante preparación.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'BatchObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones del lote', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'BatchObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'BatchObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o identificador del lote (VARCHAR 20) asignado cuando se finaliza la preparación de leche lactea. Trazabilidad de lotes para control de calidad.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'Batch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lote: Se asigna cuando se fianliza la leche lactea', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'Batch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'Batch';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que finaliza la preparación de la fórmula láctea en lactario. Marca conclusión de actividad de preparación.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaPreparationEnd';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora en que finaliza la preparación de la fórmula', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaPreparationEnd';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaPreparationEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (FK → INPROFSAL.CODPROSAL) que finaliza la preparación de la fórmula. Responsable de cierre de preparación.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaPreparationEnd';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional que finaliza la preparación de la fórmula', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaPreparationEnd';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaPreparationEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se inicia la preparación de la fórmula láctea. Marca inicio efectivo de actividad en lactario.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaPreparationStart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora en que se inicia la preparación de la fórmula', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaPreparationStart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaPreparationStart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (FK → INPROFSAL.CODPROSAL) que inicia la preparación de la fórmula en lactario. Auditoría de responsable.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaPreparationStart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional que inicia la preparación de la fórmula', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaPreparationStart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaPreparationStart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se realiza la solicitud de preparación de fórmula láctea o leche materna. Marca inicio del ciclo de preparación.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateLactationRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora en que se realiza la solicitud de preparación de fórmula o leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateLactationRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateLactationRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (FK → INPROFSAL.CODPROSAL) que solicita la preparación de fórmula láctea o leche materna desde lactario.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeLactationRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional que solicita la preparación de lactario', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeLactationRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeLactationRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la fórmula láctea (INT, 1-7): 1=Solicitud Asignada, 2=En preparación, 3=Preparación finalizada, 4=Preparación Entregada, 5=Completa registro individual, 6=Anulada, 7=Completa todas las preparaciones. Control de flujo en dashboard lactario.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de Formula lactea:

1- Solicitud  Asignada por parte de lactario desde dashboard lactario
2- En preparación (Inicia preparacion)
3- Preparación finalizada (Finalizar preparacion)
4- Preparación Entregada 
5 - Completa la preparación (Es decir este registro)
6 - Anulada leche materna desde dashboard lactario
7 - Completa todas las preparaciones (Todos los registros)








', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de prescripción original (FK → HCPRESCRA.ID). Referencia a la solicitud médica que origina la preparación de leche materna o fórmula lactea.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'IdHCPRESCRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la orden original (Id de HCPRESCRA)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'IdHCPRESCRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'IdHCPRESCRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) de cada preparación de fórmula láctea en el lactario. Clave primaria para rastrear cada tarro de leche desde solicitud hasta entrega o anulación.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único de la preparación de lactario', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de preparación de fórmula láctea (leche materna y compuestos). Registra el ciclo completo de solicitud, preparación, entrega y finalización de tarros de leche desde el dashboard del lactario. Gestiona estados, auditoría de profesionales y trazabilidad de lotes.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla de preparacion de leche compuesta ó llamada tambien leche lactea, es decir los tarros de leche. 

Tabla que se gestiona desde el dashboard lactario', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails';

