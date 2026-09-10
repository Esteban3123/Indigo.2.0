CREATE TABLE [Billing].[RIPSSupportRecordDetail] (
    [Id]                                     INT          IDENTITY (1, 1) NOT NULL,
    [RIPSSupportRecordId]                    INT          NOT NULL,
    [InitialDate]                            DATETIME     NOT NULL,
    [FinalDate]                              DATETIME     NOT NULL,
    [FunctionalUnitId]                       INT          NOT NULL,
    [TypeStay]                               CHAR (3)     NOT NULL,
    [CalculatedDaysStay]                     INT          NULL,
    [PerformsHealthProfessionalThirdPartyId] INT          NULL,
    [PerformsProfessionalSpecialty]          CHAR (3)     NULL,
    [MainDiagnosis]                          CHAR (4)     NOT NULL,
    [RelatedDiagnosis]                       CHAR (4)     NOT NULL,
    [DischargeCondition]                     CHAR (2)     NOT NULL,
    [DiagnosisCauseDeath]                    CHAR (4)     NULL,
    [CreationUser]                           VARCHAR (20) CONSTRAINT [DF_RIPSSupportRecordDetail_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                           DATETIME     CONSTRAINT [DF_RIPSSupportRecordDetail_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]                       VARCHAR (20) NULL,
    [ModificationDate]                       DATETIME     NULL,
    [StayCUPSEntityContractDescriptionId]    INT          NULL,
    [StayCUPSEntityId]                       INT          NULL,
    CONSTRAINT [PK_RIPSSupportRecordDetail_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RIPSSupportRecordDetail_CUPSEntity] FOREIGN KEY ([StayCUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_RIPSSupportRecordDetail_CUPSEntityContractDescriptions] FOREIGN KEY ([StayCUPSEntityContractDescriptionId]) REFERENCES [Contract].[CUPSEntityContractDescriptions] ([Id]),
    CONSTRAINT [FK_RIPSSupportRecordDetail_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_RIPSSupportRecordDetail_RIPSSupportRecord] FOREIGN KEY ([RIPSSupportRecordId]) REFERENCES [Billing].[RIPSSupportRecord] ([Id]),
    CONSTRAINT [FK_RIPSSupportRecordDetail_ThirdParty] FOREIGN KEY ([PerformsHealthProfessionalThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO





GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_RIPSSupportRecordDetail_RIPSSupportRecordId]
    ON [Billing].[RIPSSupportRecordDetail]([RIPSSupportRecordId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de detalle del registro de soporte para archivo RIPS. Almacena líneas de atención, diagnósticos, estancias y procedimientos que conforman el reporte de facturación e información de salud ante la autoridad regulatoria.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla que guarda el detalle del registro de soporte para el archivo RIPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de detalle RIPS (DATETIME). Auditoria de cambios posteriores a la creación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del detalle RIPS (VARCHAR 20). Trazabilidad de cambios en el registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del detalle del registro RIPS (DATETIME). Timestamp de ingreso inicial del registro al sistema.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el detalle del registro RIPS (VARCHAR 20, default 999). Responsable de la captura inicial del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE-10 (CHAR 4) de la causa de muerte del paciente. Solo aplica cuando condición de alta es defunción (fallecimiento/óbito).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'DiagnosisCauseDeath';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la causa de muerte del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'DiagnosisCauseDeath';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'DiagnosisCauseDeath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de condición de egreso (CHAR 2): alta médica, traslado, fallecimiento, abandono, etc. Estado final de la estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'DischargeCondition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Condición de alta de la estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'DischargeCondition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'DischargeCondition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE-10 (CHAR 4) del diagnóstico secundario, comorbilidad o condición relacionada. Diagnóstico complementario al principal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'RelatedDiagnosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del diagnóstico relacionado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'RelatedDiagnosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'RelatedDiagnosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE-10 (CHAR 4) del diagnóstico principal, motivo de consulta o ingreso. Razón primaria de la atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'MainDiagnosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del diagnóstico principal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'MainDiagnosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'MainDiagnosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica (CHAR 3) del profesional de la salud que realiza la atención. Especialidad sanitaria del prestador.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'PerformsProfessionalSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especialidad del profesional de la salud que realiza la atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'PerformsProfessionalSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'PerformsProfessionalSpecialty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del profesional de la salud/prestador (FK ThirdParty) que ejecuta la atención. Médico, odontólogo, enfermero u otro profesional sanitario.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del profesional de la salud que realiza la atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días de estancia calculada (INT). Duración en días entre fecha inicial y final de la atención/hospitalización.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'CalculatedDaysStay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Días calculados de la estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'CalculatedDaysStay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'CalculatedDaysStay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de estancia (CHAR 3): ambulatoria, urgencia, hospitalización, cirugía, etc. Modalidad asistencial de la atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'TypeStay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'TypeStay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'TypeStay';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la unidad funcional (FK FunctionalUnit) donde se presta la atención. Departamento, servicio, área clínica o centro de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la unidad funcional asociada a la atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de finalización/egreso de la atención (DATETIME). Cierre o término de la estancia del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'FinalDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de finalización de la atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'FinalDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'FinalDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio/ingreso de la atención (DATETIME). Momento de admisión del paciente a la unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio de la atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del registro de soporte RIPS padre (FK RIPSSupportRecord). Referencia a la factura, ingreso o evento clínico asociado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'RIPSSupportRecordId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro de soporte asociado al detalle.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'RIPSSupportRecordId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'RIPSSupportRecordId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle RIPS (INT IDENTITY PK). Clave primaria del registro individual de soporte RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'Id';

GO


GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de descripción del contrato asociado al código CUPS de la estancia; vincula el tipo de servicio de hospitalización con la negociación tarifaria del contrato.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'StayCUPSEntityContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'StayCUPSEntityContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad (aseguradora o pagador) relacionada con el código CUPS de la estancia; indica con qué entidad se factura el servicio de hospitalización.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'StayCUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecordDetail', @level2type = N'COLUMN', @level2name = N'StayCUPSEntityId';
