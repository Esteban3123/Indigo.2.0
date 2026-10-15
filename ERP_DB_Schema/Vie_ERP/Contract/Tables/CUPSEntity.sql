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
ALTER TABLE [Contract].[CUPSEntity] NOCHECK CONSTRAINT [FK_CUPSEntity_BillingConcept];


GO
ALTER TABLE [Contract].[CUPSEntity] NOCHECK CONSTRAINT [FK_CUPSEntity_BillingConcept_1];


GO
ALTER TABLE [Contract].[CUPSEntity] NOCHECK CONSTRAINT [FK_CUPSEntity_BillingGroup];


GO
ALTER TABLE [Contract].[CUPSEntity] NOCHECK CONSTRAINT [FK_CUPSEntity_BillingGroup_1];


GO
ALTER TABLE [Contract].[CUPSEntity] NOCHECK CONSTRAINT [FK_CupsEntity_CupsSubgroup];


GO
ALTER TABLE [Contract].[CUPSEntity] NOCHECK CONSTRAINT [FK_CUPSEntity_RIPSServices];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo para guardar los servicios de apoyo imagenológico a procedimientos ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ImageGuidanceProcedure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el procedimiento quirúrgico tiene o no informe obligatorio, Si ServiceType = 5; entonces el valor por defecto de esta columna es True', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SurgicalReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el ID del servicios RIPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si un procedimiento QX es mandatorio realizar la hoja de gastos QX, por defecto este campo esta en SI.  La afectacion que tiene es que cuando se vaya a egresar un paciente, el sistema cambiara a realizado todos los procedimientos QX que esten pendiente de confirmar. ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MandatoryQxReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bandera para determinar si se lista servicio en el dashboard de  Esto solo en el ambito ambulatorio  1: Laboratorios  2: Patologias  3: Imagenes Diagnosticas  4. Consulta Externa  5. Quimioterapias  6. Radioterapias  7. Diálisis  8. Ninguno  9. Procedimiento no Qx  10. Procedimiento Qx  11. Interconsultas  12. Otros Procedimientos  13. Braquiterapia    Este campo se agrgo para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ShowDashboardOfAmbulatory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Propiedad que indica si el CUP requiere lateralidad.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RequiresLaterality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica Si es o no un panel, 0 - No es un panel  1 - Si es un panel', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'IsPanel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solicitar Sala automáticamente', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RequestRoomAutomatically';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Financiado con Recursos de la UPC, anteriormente se llamaba POS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'FinancedResourceUPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo booleano que establece el sevricio de oxigeno 0 - No, 1 - Si', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'OxigenService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del Grupo de Facturacion si Aplica RIAS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIASBillingGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del Grupo de Servicios si Aplica RIAS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIASBillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si aplica a RIAS. Si aplica a RIAS se tienen que guardar detalles de rangos en las tablas de Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Albumina fosforo, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PhosphorusAlbumin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Albumina serica, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SerineAlbumin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PTH, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'LDL, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'LDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Colesterol total, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'TotalCholesterol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Creatinuria, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Creatinuria';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Glisemia basal, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'BasalGlycaemia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Biopsia seno por bacaf, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'BreastBiopsyBacaf';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Biopsia cervical    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'CervicalBiopsy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mamografía    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Mammography';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Planificación familiar primera vez    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'FamilyPlanningFirstTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consulta crecimiento y desarrollo primera vez    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'GrowthDevelopmentFirstTimeConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consulta por oftalmología    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'OphthalmologyConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valoración agudeza visual    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'VisualAcuityAssessment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control prenatal    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PrenatalControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control prenatal primera vez    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PrenatalControlFirstTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Baciloscopia de diagnostico    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'DiagnosticSmearMicroscopy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HDL    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'HDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Microalbuminuria    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Microalbuminuria';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hemoglobina glicosilada    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'GlycosylatedHemoglobin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Creatina    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Creatine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hemoglobina    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Hemoglobin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Elisa para VIH    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ElisaVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Serología para sifilis    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SerologySyphilis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antígeno de superficie hepatitis b en gestación    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SurfaceAntigen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TSH neonatal    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'NeonatalTSH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Asesoría pos test elsa para VIH    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AdvisoryPosTestElsaVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Asesoría pre test elsa para VIH    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AdvisoryPreTestElsaVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consulta de adulto primera vez    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AdultFirstTimeConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consulta de joven primera vez    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'YoungFirstTimeConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consulta de psicología    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'PsychologyConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consulta de nutrición    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'NutritionConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exige confirmación de realización    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RequiresConfirmationRealization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exige interpretación    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RequiresInterpretation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio seriado    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'SerialService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir diligenciar informe para la realización de procedimientos Qx menores    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AllowDiligenceReportRealizationQx';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir diligenciar resultados/interpretacion de examenes/estudios realizados en sitio de consulta    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'AllowDiligenceInPlace';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Procedimiento de terapia, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'TherapyProcedure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bandera para determinar si se lista servicio en el dashboard de  Esto solo en el ambito hospitalario  1: Laboratorios  2: Patologias  3: Imagenes Diagnosticas  4. Consulta Externa  5. Quimioterapias  6. Radioterapias  7. Diálisis  8. Ninguno  9. Procedimiento no Qx  10. Procedimiento Qx  11. Interconsultas  12. Otros Procedimientos  13. Braquiterapia    Este campo se agrgo para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ShowDashboardOf';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Servicio    1: Laboratorios  2: Patologias  3: Imagenes Diagnosticas  4: Procedimeintos no Qx  5: Procedimientos Qx  6: Interconsultas  7:Ninguno  8:Consulta Externa    Este campo se agrgo para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ShowServiceMedicalOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo:  0. Masculino  1. Femenino  2. Ambos    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Sex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad maxima, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MaximumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida para la edad maxima  1 - Años  2 - Meses  3 - Dias    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MaximumAgeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad minima, este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MinimunAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida para la edad minima  1 - Años  2 - Meses  3 - Dias    Este campo se agrego para RIAS de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'MinimunAgeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del manual tarifario  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Servicio  1: Laboratorios  2: Patologias  3: Imagenes Diagnosticas  4: Procedimeintos no Qx  5: Procedimientos Qx  6: Interconsultas  7:Ninguno  8:Consulta Externa', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'ServiceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del Grupo de Facturacion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'BillingGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del Grupo de Servicios', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo de concepto de RIPS  01 = Consultas  02 = Procedimientos de diagnósticos  03= Procedimientos terapéuticos no quirúrgicos  04= Procedimientos terapéuticos quirúrgicos  05= Procedimientos de promoción y prevención  06= Estancias  07 = Honorarios  08 = Derechos de sala  09 = Materiales e insumos  10 = Banco de sangre  11 = Prótesis y órtesis  12 = Medicamentos POS  13 = Medicamentos no POS  14 = Traslado de pacientes', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIPSConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del registro individual para la prestacion del servicio en salud (RIPS)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIPSDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del registro individual para la prestacion del servicio en salud (RIPS)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'RIPSCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del cups de la IPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del CUPS de la IPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del subgrupo del CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'CUPSSubGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntity', @level2type = N'COLUMN', @level2name = N'Id';

