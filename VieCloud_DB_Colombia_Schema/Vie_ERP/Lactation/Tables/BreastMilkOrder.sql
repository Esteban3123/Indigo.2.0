CREATE TABLE [Lactation].[BreastMilkOrder] (
    [Id]                         INT           IDENTITY (1, 1) NOT NULL,
    [IPCODPACI]                  VARCHAR (25)  NOT NULL,
    [NUMINGRES]                  CHAR (10)     NOT NULL,
    [NUMEFOLIO]                  CHAR (10)     NOT NULL,
    [HCHISPACAId]                INT           NOT NULL,
    [Status]                     TINYINT       NOT NULL,
    [CareCenterCode]             CHAR (10)     NOT NULL,
    [FunctionalUnitCode]         CHAR (10)     NOT NULL,
    [DietCode]                   CHAR (3)      NOT NULL,
    [FrequencyHours]             INT           NOT NULL,
    [FrequencyUnit]              TINYINT       NOT NULL,
    [MillilitersPerDose]         INT           NOT NULL,
    [DosesPerDay]                INT           NOT NULL,
    [AdministrationRouteCode]    VARCHAR (20)  NOT NULL,
    [AdministrationInstructions] VARCHAR (MAX) NOT NULL,
    [OrderDate]                  DATETIME      NOT NULL,
    [MedicalCancellationCode]    CHAR (20)     NULL,
    [MedicalCancellationDate]    DATETIME      NULL,
    [CancellationReason]         CHAR (4)      NULL,
    [CancellationJustification]  VARCHAR (500) NULL,
    [LactationCancellationCode]  CHAR (20)     NULL,
    [LactationCancellationDate]  DATETIME      NULL,
    [ProfessionalCode]           CHAR (20)     NOT NULL,
    [CreationDate]               DATETIME      NOT NULL,
    [MANEXTPRO]                  BIT           CONSTRAINT [DF_BreastMilkOrder_MANEXTPRO] DEFAULT ((0)) NOT NULL,
    [ObservationsGenerals]       VARCHAR (500) NULL,
    CONSTRAINT [PK__BreastMi__3214EC076666F84D] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BreastMilkOrder_ADCENATEN] FOREIGN KEY ([CareCenterCode]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_BreastMilkOrder_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_BreastMilkOrder_CancellationReason_HCMOANULB] FOREIGN KEY ([CancellationReason]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_BreastMilkOrder_CareCenterCode_ADCENATEN] FOREIGN KEY ([CareCenterCode]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_BreastMilkOrder_FunctionalUnitCode_INUNIFUNC] FOREIGN KEY ([FunctionalUnitCode]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_BreastMilkOrder_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_BreastMilkOrder_INUNIFUNC] FOREIGN KEY ([FunctionalUnitCode]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_BreastMilkOrder_IPCODPACI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_BreastMilkOrder_LactationCancellationCode_INPROFSAL] FOREIGN KEY ([LactationCancellationCode]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_BreastMilkOrder_MedicalCancellationCode_INPROFSAL] FOREIGN KEY ([MedicalCancellationCode]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_BreastMilkOrder_ProfessionalCode_INPROFSAL] FOREIGN KEY ([ProfessionalCode]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);








GO



GO



GO



GO





GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones generales VARCHAR(500), registra notas médicas consolidadas sobre todas las dietas y órdenes de leche materna del paciente; anotación general del profesional de la salud.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'ObservationsGenerals';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones generales, se toma las opbservaciones de toda las dietas, es un observación muy general del medico por todas las dietas.

', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'ObservationsGenerals';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'ObservationsGenerals';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bit, indica si la leche materna se maneja en régimen ambulatorio (0) u hospitalario (1); clasificación de nivel de atención.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'indica si la leche materna es de manejo ambulatorio o hospitalario ', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME, fecha y hora de creación/registro de la orden de leche materna; marca de auditoría inicial.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación de la orden', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20) FK→INPROFSAL, código del profesional de la salud (médico, nutriólogo) que prescribió la orden de leche materna.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código profesional del que realizó la orden de leche materna.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME nullable, fecha y hora en que el personal del lactario/banco de leche anuló/rechazó la orden desde el dashboard de lactancia.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'LactationCancellationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que el lactario anula la orden de leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'LactationCancellationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'LactationCancellationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20) FK→INPROFSAL nullable, código del profesional de lactancia que canceló la orden de leche materna desde el sistema de lactario.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'LactationCancellationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del lactario que anula la orden de leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'LactationCancellationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'LactationCancellationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(500) nullable, justificación o aclaración escrita del motivo de anulación registrada en el dashboard del lactario.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'CancellationJustification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la justificacion de la anulacion de la leche materna cuando esta es anulada desde el dashboard lactario', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'CancellationJustification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'CancellationJustification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(4) FK→HCMOANULB nullable, código del motivo de anulación (catálogo de razones de cancelación de órdenes de leche materna).', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'CancellationReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el motivo de la anulacion de la leche materna cuando esta es anulada desde el dashboard lactario', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'CancellationReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'CancellationReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME nullable, fecha y hora en que el médico anuló la orden desde la pestaña de Dietas del plan de manejo.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'MedicalCancellationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la cual el que anula la orden de leche materna desde el tab de dietas del plan manejo', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'MedicalCancellationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'MedicalCancellationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20) FK→INPROFSAL nullable, código del profesional médico que canceló la orden desde el plan de manejo/dietas.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'MedicalCancellationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del médico que anula la orden de leche materna desde el tab de dietas del plan manejo', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'MedicalCancellationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'MedicalCancellationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME, fecha y hora de emisión/prescripción de la orden de leche materna; marca temporal de la solicitud médica.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'OrderDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la fecha de la orden de leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'OrderDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'OrderDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(MAX), instrucciones detalladas para la administración segura de la leche materna (posición, velocidad, precauciones, monitoreo).', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'AdministrationInstructions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena las instrucciones de administración de la orden de leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'AdministrationInstructions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'AdministrationInstructions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20), vía de administración de la leche materna (oral, sonda nasogástrica, sonda orogástrica, entre otras); especifica cómo se suministra.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'AdministrationRouteCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la vía de administración de la orden de leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'AdministrationRouteCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'AdministrationRouteCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, número de tomas o administraciones de leche materna por día; frecuencia de administración diaria.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'DosesPerDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el número de tomas por día.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'DosesPerDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'DosesPerDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, volumen en mililitros (mL) por cada toma/dosis de leche materna; cantidad de volumen por administración.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'MillilitersPerDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Número de mililitros toma de la orden de leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'MillilitersPerDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'MillilitersPerDose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT, unidad de tiempo para la frecuencia: 1=Minutos, 2=Horas; define la unidad de medida del intervalo entre tomas.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'FrequencyUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la unidad de medida de la frecuencia de la orden de leche materna
1 = Minutos
2 = Horas', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'FrequencyUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'FrequencyUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, intervalo de frecuencia en la unidad especificada (minutos u horas); "cada X horas/minutos" entre administraciones.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'FrequencyHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena las horas de frecuencia de la orden (Campo Cada de la orden)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'FrequencyHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'FrequencyHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(3), código de la dieta o plan de nutrición relacionado con la orden de leche materna; vinculación a régimen dietético.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'DietCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código de la dieta a la que se le realizó la orden de leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'DietCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'DietCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(10) FK→INUNIFUNC, código de la unidad funcional (UCI, pediatría, neonatología, etc.) donde se prescribió la orden.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código de la unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(10) FK→ADCENATEN, código del centro de atención, institución o sede donde se realizó la prescripción de leche materna.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'CareCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código del centro de atención', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'CareCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'CareCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT, estado de la orden: 1=Solicitado por médico; 2=Asignada por personal lactario; 3=Anulada por médico; 4=Anulada por lactario; refleja ciclo de vida de la prescripción.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Solicitado medico / orden medica  

2 - Solicitud asignada por personal lactario desde dashboard 

3 - Anulada leche materna desde el medico / orden medica  

4 - Anulada leche materna desde dashboard lactario        

 ', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'Status';










GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, identificador único de la historia clínica donde se registró la orden de leche materna; referencia a registro histórico del paciente.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'HCHISPACAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el id de la historia donde se realizó la orden de leche materna ', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'HCHISPACAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'HCHISPACAId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(10), número de folio/acta donde se documentó la orden de leche materna en la pestaña de Dietas del plan de manejo.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el número del folio donde se realizó la orden de leche materna desde la pestaña de Dietas del plan de manejo', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(10) FK→ADINGRESO, número de ingreso del paciente; código del episodio de atención/hospitalización.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el número de ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(25) FK→INPACIENT, código/identificación del paciente (cédula, documento, equivalente a número de identificación PII_Identificación_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el número de identificación del paciente', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prescripciones de leche materna para dietas de lactancia. Almacena órdenes de alimentación con leche materna, incluyendo dosis, frecuencia, vías de administración, estado de la orden (solicitada, asignada, anulada) y trazabilidad de cancelaciones médicas o por lactario.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda las prescripciones de leche materna de las dietas', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno de la orden de leche materna.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkOrder', @level2type = N'COLUMN', @level2name = N'Id';
