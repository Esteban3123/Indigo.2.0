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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de la anulación', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'IdCODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificacion de la cancelación  y/o anulacion
', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'JustificationCancellation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se anula la preparación de fórmula', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaCancellation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional que realiza la anulación de la preparación de fórmula', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaCancellation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'fecha en que se completan todas las preparaciones', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateAllPreparationsCompleted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional que completa todas las preparaciones', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeAllPreparationsCompleted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en la  que se completa esta preparacion, es decir  5 de / (5/8)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DatePreparationCompleted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional que completa esta preparacion, es decir  5 de / (5/8)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodePreparationCompleted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se entrega la fórmula al paciente o cuidador', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaDelivery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional que realiza la entrega de la fórmula', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaDelivery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad preparada, se diligencia cuando se finaliza la preparación', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'QuantityPrepared';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones del lote', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'BatchObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lote: Se asigna cuando se fianliza la leche lactea', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'Batch';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que finaliza la preparación de la fórmula', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaPreparationEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional que finaliza la preparación de la fórmula', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaPreparationEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se inicia la preparación de la fórmula', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateFormulaPreparationStart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional que inicia la preparación de la fórmula', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeFormulaPreparationStart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realiza la solicitud de preparación de fórmula o leche materna', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'DateLactationRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional que solicita la preparación de lactario', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeLactationRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Formula lactea:

1- Solicitud  Asignada por parte de lactario desde dashboard lactario
2- En preparación (Inicia preparacion)
3- Preparación finalizada (Finalizar preparacion)
4- Preparación Entregada 
5 - Completa la preparación (Es decir este registro)
6 - Anulada leche materna desde dashboard lactario
7 - Completa todas las preparaciones (Todos los registros)








', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden original (Id de HCPRESCRA)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'IdHCPRESCRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la preparación de lactario', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de preparacion de leche compuesta ó llamada tambien leche lactea, es decir los tarros de leche. 

Tabla que se gestiona desde el dashboard lactario', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'LactealFormulaPreparationDetails';

