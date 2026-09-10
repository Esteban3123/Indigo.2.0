CREATE TABLE [Contract].[CUPSEntity] (
    [Id]                                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CUPSSubGroupId]                         INT           NOT NULL,
    [Code]                                   VARCHAR (20)  NOT NULL,
    [Description]                            VARCHAR (300) NULL,
    [RIPSCode]                               VARCHAR (20)  NOT NULL,
    [RIPSDescription]                        VARCHAR (300) NOT NULL,
    [RIPSConcept]                            CHAR (2)      NOT NULL,
    [BillingConceptId]                       INT           NOT NULL,
    [BillingGroupId]                         INT           NOT NULL,
    [ServiceType]                            TINYINT       NOT NULL,
    [Status]                                 BIT           NOT NULL,
    [CreationUser]                           VARCHAR (20)  NOT NULL,
    [CreationDate]                           DATETIME      NOT NULL,
    [ModificationUser]                       VARCHAR (20)  NULL,
    [ModificationDate]                       DATETIME      NULL,
    [TimeStamp]                              ROWVERSION    NOT NULL,
    [MinimunAgeUnit]                         TINYINT       CONSTRAINT [DF_CUPSEntity_MinimunAgeUnit] DEFAULT ((0)) NOT NULL,
    [MinimunAge]                             INT           CONSTRAINT [DF_CUPSEntity_MinimunAge] DEFAULT ((0)) NOT NULL,
    [MaximumAgeUnit]                         TINYINT       CONSTRAINT [DF_CUPSEntity_MaximumAgeUnit] DEFAULT ((0)) NOT NULL,
    [MaximumAge]                             INT           CONSTRAINT [DF_CUPSEntity_MaximumAge] DEFAULT ((0)) NOT NULL,
    [Sex]                                    TINYINT       CONSTRAINT [DF_CUPSEntity_Sex] DEFAULT ((0)) NOT NULL,
    [ShowServiceMedicalOrder]                TINYINT       NULL,
    [ShowDashboardOf]                        TINYINT       NULL,
    [TherapyProcedure]                       BIT           NULL,
    [AllowDiligenceInPlace]                  BIT           NULL,
    [AllowDiligenceReportRealizationQx]      BIT           NULL,
    [SerialService]                          BIT           NULL,
    [RequiresInterpretation]                 BIT           NULL,
    [RequiresConfirmationRealization]        BIT           NULL,
    [NutritionConsultation]                  BIT           NULL,
    [PsychologyConsultation]                 BIT           NULL,
    [YoungFirstTimeConsultation]             BIT           NULL,
    [AdultFirstTimeConsultation]             BIT           NULL,
    [AdvisoryPreTestElsaVIH]                 BIT           NULL,
    [AdvisoryPosTestElsaVIH]                 BIT           NULL,
    [NeonatalTSH]                            BIT           NULL,
    [SurfaceAntigen]                         BIT           NULL,
    [SerologySyphilis]                       BIT           NULL,
    [ElisaVIH]                               BIT           NULL,
    [Hemoglobin]                             BIT           NULL,
    [Creatine]                               BIT           NULL,
    [GlycosylatedHemoglobin]                 BIT           NULL,
    [Microalbuminuria]                       BIT           NULL,
    [HDL]                                    BIT           NULL,
    [DiagnosticSmearMicroscopy]              BIT           NULL,
    [PrenatalControlFirstTime]               BIT           NULL,
    [PrenatalControl]                        BIT           NULL,
    [VisualAcuityAssessment]                 BIT           NULL,
    [OphthalmologyConsultation]              BIT           NULL,
    [GrowthDevelopmentFirstTimeConsultation] BIT           NULL,
    [FamilyPlanningFirstTime]                BIT           NULL,
    [Mammography]                            BIT           NULL,
    [CervicalBiopsy]                         BIT           NULL,
    [BreastBiopsyBacaf]                      BIT           NULL,
    [BasalGlycaemia]                         BIT           NULL,
    [Creatinuria]                            BIT           NULL,
    [TotalCholesterol]                       BIT           NULL,
    [LDL]                                    BIT           NULL,
    [PTH]                                    BIT           NULL,
    [SerineAlbumin]                          BIT           NULL,
    [PhosphorusAlbumin]                      BIT           NULL,
    [ApplyRIAS]                              BIT           CONSTRAINT [DF_CUPSEntity_ApplyRIAS] DEFAULT ((0)) NOT NULL,
    [RIASBillingConceptId]                   INT           NULL,
    [RIASBillingGroupId]                     INT           NULL,
    [OxigenService]                          BIT           CONSTRAINT [DF_CUPSEntity_OxigenService] DEFAULT ((0)) NOT NULL,
    [FinancedResourceUPC]                    BIT           CONSTRAINT [DF_CUPSEntity_FinancedResourceUPC] DEFAULT ((0)) NOT NULL,
    [RequestRoomAutomatically]               BIT           CONSTRAINT [DF_CUPSEntity_RequestRoomAutomatically] DEFAULT ((0)) NOT NULL,
    [IsPanel]                                TINYINT       CONSTRAINT [DF__CUPSEntit__IsPan__221C52C8] DEFAULT ((0)) NULL,
    [RequiresLaterality]                     BIT           CONSTRAINT [DF_CUPSEntity_RequiresLaterality] DEFAULT ((0)) NULL,
    [ShowDashboardOfAmbulatory]              TINYINT       NULL,
    [MandatoryQxReport]                      BIT           CONSTRAINT [DF_CUPSEntity_MandatoryQxReport] DEFAULT ((1)) NOT NULL,
    [RIPSServiceId]                          INT           NULL,
    [SurgicalReport]                         BIT           CONSTRAINT [DF_CUPSEntity_SurgicalReport] DEFAULT ((0)) NOT NULL,
    [ImageGuidanceProcedure]                 BIT           CONSTRAINT [DF_CUPSEntity_ImageGuidanceProcedure] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_CUPSEntity__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CUPSEntity_BillingConcept] FOREIGN KEY ([BillingConceptId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_CUPSEntity_BillingConcept_1] FOREIGN KEY ([RIASBillingConceptId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_CUPSEntity_BillingGroup] FOREIGN KEY ([BillingGroupId]) REFERENCES [Billing].[BillingGroup] ([Id]),
    CONSTRAINT [FK_CUPSEntity_BillingGroup_1] FOREIGN KEY ([RIASBillingGroupId]) REFERENCES [Billing].[BillingGroup] ([Id]),
    CONSTRAINT [FK_CupsEntity_CupsSubgroup] FOREIGN KEY ([CUPSSubGroupId]) REFERENCES [Contract].[CupsSubgroup] ([Id]),
    CONSTRAINT [FK_CUPSEntity_RIPSServices] FOREIGN KEY ([RIPSServiceId]) REFERENCES [Contract].[RIPSServices] ([Id])
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



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_CUPSEntity_RIPSConcept]
    ON [Contract].[CUPSEntity]([RIPSConcept] ASC)
    INCLUDE([Id], [RIPSServiceId]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CUPSEntity__Code]
    ON [Contract].[CUPSEntity]([Code] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_CUPSEntity_Status_Id_INC_ApplyRIAS_Code_CUPSSubGroupId_Description_OxigenService_RIPSCode_RIPSDescription]
    ON [Contract].[CUPSEntity]([Status] ASC, [Id] ASC)
    INCLUDE([ApplyRIAS], [Code], [CUPSSubGroupId], [Description], [OxigenService], [RIPSCode], [RIPSDescription]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Bandera para indicar si el procedimiento requiere apoyo o guía imagenológica (ecografía, fluoroscopia, tomografía) durante su realización.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ImageGuidanceProcedure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para guardar los servicios de apoyo imagenológico a procedimientos ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ImageGuidanceProcedure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ImageGuidanceProcedure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indica si el procedimiento quirúrgico (ServiceType=5) requiere informe quirúrgico obligatorio; por defecto verdadero, impacta confirmación automática al egreso.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SurgicalReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el procedimiento quirúrgico tiene o no informe obligatorio, Si ServiceType = 5; entonces el valor por defecto de esta columna es True', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SurgicalReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SurgicalReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT: Identificador del servicio RIPS asociado (FK Contract.RIPSServices), usado para mapeo de facturación y reportes de prestación de servicios en salud.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el ID del servicios RIPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Especifica si es obligatorio completar la hoja de gastos quirúrgicos (QX) antes de egresar paciente; por defecto 1 (sí), afecta procesos de confirmación de procedimientos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MandatoryQxReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si un procedimiento QX es mandatorio realizar la hoja de gastos QX, por defecto este campo esta en SI.  La afectacion que tiene es que cuando se vaya a egresar un paciente, el sistema cambiara a realizado todos los procedimientos QX que esten pendiente de confirmar. ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MandatoryQxReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MandatoryQxReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT: Categoría de servicio para visualización en dashboard ambulatorio (1=Laboratorios, 2=Patologías, 3=Imágenes, 4=Consulta, 5=Quimio, 6=Radioterapia, 7=Diálisis, 8=Ninguno, 9=Proc.NoQx, 10=Proc.Qx, 11=Interconsulta, 12=Otros, 13=Braquiterapia).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ShowDashboardOfAmbulatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bandera para determinar si se lista servicio en el dashboard de  Esto solo en el ambito ambulatorio  1: Laboratorios  2: Patologias  3: Imagenes Diagnosticas  4. Consulta Externa  5. Quimioterapias  6. Radioterapias  7. Diálisis  8. Ninguno  9. Procedimiento no Qx  10. Procedimiento Qx  11. Interconsultas  12. Otros Procedimientos  13. Braquiterapia    Este campo se agrgo para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ShowDashboardOfAmbulatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ShowDashboardOfAmbulatory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Propiedad booleana que indica si el CUPS requiere especificación de lateralidad (izquierdo/derecho/bilateral) en la orden médica o procedimiento.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RequiresLaterality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Propiedad que indica si el CUP requiere lateralidad.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RequiresLaterality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RequiresLaterality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT: Bandera que indica si el CUPS es un panel de servicios (0=No es panel, 1=Sí es panel agrupado); nulo por defecto.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'IsPanel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica Si es o no un panel, 0 - No es un panel  1 - Si es un panel', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'IsPanel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'IsPanel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Bandera que especifica si se debe solicitar automáticamente sala quirúrgica u operatoria al generar el procedimiento; por defecto 0 (no).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RequestRoomAutomatically';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Solicitar Sala automáticamente', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RequestRoomAutomatically';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RequestRoomAutomatically';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indica si el servicio se financia con recursos de UPC (antes llamado POS); booleano, por defecto 0 (no financiado por UPC).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'FinancedResourceUPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Financiado con Recursos de la UPC, anteriormente se llamaba POS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'FinancedResourceUPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'FinancedResourceUPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Bandera booleana que establece si el servicio requiere o incluye suministro de oxígeno medicinal (0=No, 1=Sí).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'OxigenService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo booleano que establece el sevricio de oxigeno 0 - No, 1 - Si', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'OxigenService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'OxigenService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT: Identificador del grupo de facturación RIAS (FK Billing.BillingGroup) cuando el CUPS aplica a régimen RIAS; nulo si no aplica.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIASBillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del Grupo de Facturacion si Aplica RIAS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIASBillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIASBillingGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT: Identificador del concepto de servicio para RIAS (FK Billing.BillingConcept); nulo si ApplyRIAS=0.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIASBillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del Grupo de Servicios si Aplica RIAS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIASBillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIASBillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Bandera que indica si el CUPS aplica a RIAS (Régimen de Integración de Servicios de Salud); si es 1, requiere configuración de rangos en Crystal; por defecto 0.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si aplica a RIAS. Si aplica a RIAS se tienen que guardar detalles de rangos en las tablas de Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para examen de fósforo en albúmina sérica; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PhosphorusAlbumin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Albumina fosforo, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PhosphorusAlbumin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PhosphorusAlbumin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para examen de albúmina sérica; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SerineAlbumin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Albumina serica, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SerineAlbumin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SerineAlbumin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para examen de hormona paratiroidea; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PTH, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para examen de lipoproteína de baja densidad; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'LDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'LDL, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'LDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'LDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para examen de colesterol total; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'TotalCholesterol';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Colesterol total, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'TotalCholesterol';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'TotalCholesterol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para examen de creatinina en orina 24h; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Creatinuria';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creatinuria, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Creatinuria';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Creatinuria';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para examen de glicemia basal (glucosa en ayuno); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'BasalGlycaemia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Glisemia basal, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'BasalGlycaemia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'BasalGlycaemia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para biopsia de mama por BACAF (biopsia asistida por vacío); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'BreastBiopsyBacaf';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Biopsia seno por bacaf, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'BreastBiopsyBacaf';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'BreastBiopsyBacaf';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para biopsia cervical uterina (diagnóstico de patología cervical); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'CervicalBiopsy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Biopsia cervical    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'CervicalBiopsy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'CervicalBiopsy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para mamografía diagnóstica o de screening (imagen de mama); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Mammography';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mamografía    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Mammography';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Mammography';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para consulta de planificación familiar en primera atención; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'FamilyPlanningFirstTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Planificación familiar primera vez    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'FamilyPlanningFirstTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'FamilyPlanningFirstTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para valoración de crecimiento y desarrollo en niños, primera consulta; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'GrowthDevelopmentFirstTimeConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consulta crecimiento y desarrollo primera vez    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'GrowthDevelopmentFirstTimeConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'GrowthDevelopmentFirstTimeConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para consulta oftalmológica (evaluación oftalmopediátrica o general); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'OphthalmologyConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consulta por oftalmología    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'OphthalmologyConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'OphthalmologyConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para valoración de agudeza visual con optometría; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'VisualAcuityAssessment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valoración agudeza visual    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'VisualAcuityAssessment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'VisualAcuityAssessment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para control prenatal subsecuente en gestante; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PrenatalControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control prenatal    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PrenatalControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PrenatalControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para control prenatal de primera vez en gestante; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PrenatalControlFirstTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control prenatal primera vez    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PrenatalControlFirstTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PrenatalControlFirstTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para baciloscopia diagnóstica (microscopía de tuberculosis); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'DiagnosticSmearMicroscopy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Baciloscopia de diagnostico    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'DiagnosticSmearMicroscopy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'DiagnosticSmearMicroscopy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para examen de lipoproteína de alta densidad; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'HDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'HDL    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'HDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'HDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para microalbuminuria en orina (biomarcador renal); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Microalbuminuria';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Microalbuminuria    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Microalbuminuria';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Microalbuminuria';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para hemoglobina glicosilada A1C (control glucémico); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'GlycosylatedHemoglobin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hemoglobina glicosilada    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'GlycosylatedHemoglobin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'GlycosylatedHemoglobin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para creatinina sérica (función renal); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Creatine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creatina    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Creatine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Creatine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para hemoglobina en sangre (cuadro hemático); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Hemoglobin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hemoglobina    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Hemoglobin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Hemoglobin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para ELISA (ensayo inmunoabsorbente) de detección VIH; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ElisaVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Elisa para VIH    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ElisaVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ElisaVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para serología de sífilis (VDRL/RPR); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SerologySyphilis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Serología para sifilis    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SerologySyphilis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SerologySyphilis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para antígeno de superficie (HBsAg) en gestación (hepatitis B); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SurfaceAntigen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antígeno de superficie hepatitis b en gestación    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SurfaceAntigen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SurfaceAntigen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para tamizaje neonatal de hormona estimulante de tiroides; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'NeonatalTSH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TSH neonatal    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'NeonatalTSH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'NeonatalTSH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para asesoría post-test en ELISA VIH (post-consejería); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AdvisoryPosTestElsaVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Asesoría pos test elsa para VIH    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AdvisoryPosTestElsaVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AdvisoryPosTestElsaVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para asesoría pre-test en ELISA VIH (pre-consejería); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AdvisoryPreTestElsaVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Asesoría pre test elsa para VIH    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AdvisoryPreTestElsaVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AdvisoryPreTestElsaVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para consulta de adulto (≥18 años) en primera atención; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AdultFirstTimeConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consulta de adulto primera vez    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AdultFirstTimeConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AdultFirstTimeConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para consulta de joven (adolescente) en primera atención; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'YoungFirstTimeConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consulta de joven primera vez    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'YoungFirstTimeConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'YoungFirstTimeConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para consulta de psicología clínica o del comportamiento; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PsychologyConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consulta de psicología    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PsychologyConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PsychologyConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para consulta de nutrición y dietética; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'NutritionConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consulta de nutrición    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'NutritionConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'NutritionConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Bandera que exige confirmación explícita de realización de servicio/examen; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RequiresConfirmationRealization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exige confirmación de realización    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RequiresConfirmationRealization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RequiresConfirmationRealization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Bandera que exige interpretación profesional de resultados (patólogo, radiólogo); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RequiresInterpretation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exige interpretación    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RequiresInterpretation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RequiresInterpretation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para servicios seriados o en sesiones múltiples (ej. quimioterapia, fisioterapia); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SerialService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio seriado    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SerialService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SerialService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Bandera que permite diligenciar informe de realización para procedimientos quirúrgicos menores; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AllowDiligenceReportRealizationQx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permitir diligenciar informe para la realización de procedimientos Qx menores    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AllowDiligenceReportRealizationQx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AllowDiligenceReportRealizationQx';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Bandera que permite registrar resultados e interpretación in situ durante la consulta/procedimiento; campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AllowDiligenceInPlace';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permitir diligenciar resultados/interpretacion de examenes/estudios realizados en sitio de consulta    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AllowDiligenceInPlace';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AllowDiligenceInPlace';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Indicador para procedimiento de terapia (rehabilitación, fisioterapia); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'TherapyProcedure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Procedimiento de terapia, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'TherapyProcedure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'TherapyProcedure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT: Categoría de servicio para visualización en dashboard hospitalario (1=Laboratorios, 2=Patologías, 3=Imágenes, 4=Consulta, 5=Quimio, 6=Radioterapia, 7=Diálisis, 8=Ninguno, 9=Proc.NoQx, 10=Proc.Qx, 11=Interconsulta, 12=Otros, 13=Braquiterapia).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ShowDashboardOf';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bandera para determinar si se lista servicio en el dashboard de  Esto solo en el ambito hospitalario  1: Laboratorios  2: Patologias  3: Imagenes Diagnosticas  4. Consulta Externa  5. Quimioterapias  6. Radioterapias  7. Diálisis  8. Ninguno  9. Procedimiento no Qx  10. Procedimiento Qx  11. Interconsultas  12. Otros Procedimientos  13. Braquiterapia    Este campo se agrgo para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ShowDashboardOf';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ShowDashboardOf';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT: Clasificación de servicio en orden médica (1=Laboratorios, 2=Patologías, 3=Imágenes, 4=Proc.NoQx, 5=Proc.Qx, 6=Interconsulta, 7=Ninguno, 8=Consulta); campo agregado para gestión RIAS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ShowServiceMedicalOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Servicio    1: Laboratorios  2: Patologias  3: Imagenes Diagnosticas  4: Procedimeintos no Qx  5: Procedimientos Qx  6: Interconsultas  7:Ninguno  8:Consulta Externa    Este campo se agrgo para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ShowServiceMedicalOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ShowServiceMedicalOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT: Restricción de sexo aplicable al CUPS (0=Masculino, 1=Femenino, 2=Ambos); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Sex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexo:  0. Masculino  1. Femenino  2. Ambos    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Sex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Sex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT: Edad máxima permitida para realizar el CUPS; debe combinarse con MaximumAgeUnit; campo agregado para gestión RIAS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad maxima, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MaximumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT: Unidad de medida para edad máxima (1=Años, 2=Meses, 3=Días); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MaximumAgeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida para la edad maxima  1 - Años  2 - Meses  3 - Dias    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MaximumAgeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MaximumAgeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT: Edad mínima permitida para realizar el CUPS; debe combinarse con MinimunAgeUnit; campo agregado para gestión RIAS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MinimunAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad minima, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MinimunAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MinimunAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT: Unidad de medida para edad mínima (1=Años, 2=Meses, 3=Días); campo agregado para gestión RIAS en Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MinimunAgeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida para la edad minima  1 - Años  2 - Meses  3 - Dias    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MinimunAgeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MinimunAgeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP: Sello de tiempo automático SQL Server que registra instante de creación, modificación o cambio de registro.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME: Fecha y hora de última modificación del registro; nulo si nunca fue editado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20): Identificador del usuario que realizó la última modificación; nulo si nunca fue editado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME: Fecha y hora de creación del registro; requerido, auditoria.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20): Identificador del usuario que creó el registro; requerido, trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT: Estado del manual tarifario/CUPS (1=Activo, 0=Inactivo); controla disponibilidad para prescripción.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del manual tarifario  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT: Clasificación tipo de servicio (1=Laboratorios, 2=Patologías, 3=Imágenes, 4=Proc.NoQx, 5=Proc.Qx, 6=Interconsulta, 7=Ninguno, 8=Consulta); impacta visualización y flujos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ServiceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Servicio  1: Laboratorios  2: Patologias  3: Imagenes Diagnosticas  4: Procedimeintos no Qx  5: Procedimientos Qx  6: Interconsultas  7:Ninguno  8:Consulta Externa', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ServiceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ServiceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT: Identificador del grupo de facturación (FK Billing.BillingGroup) para mapeo de costos y facturación estándar.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'BillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del Grupo de Facturacion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'BillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'BillingGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT: Identificador del concepto/línea de facturación (FK Billing.BillingConcept) para detalle de servicios en factura.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del Grupo de Servicios', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(2): Código de concepto RIPS (01=Consultas, 02=Proc.Diagnóstico, 03=Proc.Terapéutico.NoQx, 04=Proc.Qx, 05=Promoción/Prevención, 06=Estancias, 07=Honorarios, 08=Derechos.Sala, 09=Materiales, 10=Sangre, 11=Prótesis, 12=Med.POS, 13=Med.NoPOS, 14=Traslado); obligatorio para RIPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIPSConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo de concepto de RIPS  01 = Consultas  02 = Procedimientos de diagnósticos  03= Procedimientos terapéuticos no quirúrgicos  04= Procedimientos terapéuticos quirúrgicos  05= Procedimientos de promoción y prevención  06= Estancias  07 = Honorarios  08 = Derechos de sala  09 = Materiales e insumos  10 = Banco de sangre  11 = Prótesis y órtesis  12 = Medicamentos POS  13 = Medicamentos no POS  14 = Traslado de pacientes', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIPSConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIPSConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(300): Descripción oficial del registro individual de prestación de servicio en salud (RIPS) para reportes a asegurador.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIPSDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del registro individual para la prestacion del servicio en salud (RIPS)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIPSDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIPSDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20): Código RIPS del servicio/procedimiento; identificador estándar para facturación y reportes de prestación de servicios en salud.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIPSCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del registro individual para la prestacion del servicio en salud (RIPS)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIPSCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIPSCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(300): Descripción local/interna del CUPS según configuración IPS; complementa el código para claridad operativa.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del cups de la IPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20): Código CUPS (Código Único de Procedimientos en Salud) de la IPS; identificador principal del procedimiento/servicio.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del CUPS de la IPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT: Identificador del subgrupo CUPS (FK Contract.CupsSubgroup); agrupa códigos por categoría clínica (ej. laboratorios hematología).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'CUPSSubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del subgrupo del CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'CUPSSubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'CUPSSubGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT IDENTITY: Identificador autoincremental único de la entidad CUPS; clave primaria para referencia interna.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Id';


GO
CREATE NONCLUSTERED INDEX [IX_CupsEntity_ServiceType]
    ON [Contract].[CUPSEntity]([ServiceType] ASC)
    INCLUDE([Id]) WITH (FILLFACTOR = 90);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de servicios CUPS (Clasificación Única de Procedimientos en Salud) configurados en el sistema. Define cada procedimiento, examen, consulta o servicio con su código, descripción, clasificación RIPS, concepto de facturación, restricciones de edad y sexo, y un conjunto de indicadores clínicos y operativos que determinan cómo se comporta el servicio en la historia clínica, la facturación y los reportes.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity';
