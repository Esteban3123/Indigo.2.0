CREATE TABLE [Billing].[AccountControlStays] (
    [Id]                              INT          IDENTITY (1, 1) NOT NULL,
    [AdmissionCode]                   VARCHAR (20) NOT NULL,
    [Bed]                             VARCHAR (10) NOT NULL,
    [StayId]                          INT          NOT NULL,
    [FunctionalUnitCode]              VARCHAR (20) NOT NULL,
    [CupsId]                          INT          NOT NULL,
    [CUPSEntityContractDescriptionId] INT          NULL,
    [ProfessionalCode]                VARCHAR (20) NOT NULL,
    [ProfessionalSpecialistCode]      VARCHAR (20) NOT NULL,
    [ServiceOrderDetailId]            INT          NULL,
    [StartDate]                       DATETIME     NOT NULL,
    [EndDate]                         DATETIME     NOT NULL,
    [LiquidationDate]                 DATETIME     NULL,
    [CreationUser]                    VARCHAR (20) NOT NULL,
    [CreationDate]                    DATETIME     NOT NULL,
    [AuthorizationEventId]            INT          NULL,
    CONSTRAINT [PK_AccountControlStays] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro en el sistema, trazabilidad de generación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario que registró el control de estancia en el sistema (auditoría).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de usuario que realizo la creación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de liquidación o facturación de la estancia, cuando se genera el documento de cobro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'LiquidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la liquidación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'LiquidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'LiquidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de fin de la estancia (DATETIME), marca cierre o alta del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final de la estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio de la estancia (DATETIME), marca apertura de internación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'StartDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio de la estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'StartDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'StartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de orden de servicio, vincula servicios solicitados y facturables.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la orden de servicio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad del profesional tratante (ej: medicina general, cirugía, pediatría).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'ProfessionalSpecialistCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la especialidad del profesional a cargo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'ProfessionalSpecialistCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'ProfessionalSpecialistCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud responsable de la atención durante la estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional a cargo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción del contrato entre entidad y CUPS, vincula facturación con acuerdos vigentes.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la descripción del contrato de la entidad del CUPS que tiene la estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del CUPS (Código Único de Procedimientos en Salud), asociado a la prestación de la estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'CupsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del CUPS que tiene la estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'CupsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'CupsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (servicio, piso, área), agrupa recursos y profesionales de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la estancia (CHREGESTA), referencia a hospitalización o internación del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'StayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la estancia CHREGESTA', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'StayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'StayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o identificador de la cama donde se ubicó el paciente durante la estancia, dato físico de ocupación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'Bed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el número de la cama donde se encuentra el paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'Bed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'Bed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de ingreso del paciente, referencia a número de admisión en tabla ADINGRESO, clave para vinculación de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'AdmissionCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código que hace referencia al numero de ingreso del paciente de la tabla ADINGRESO.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'AdmissionCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'AdmissionCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del registro de control de cuentas y facturación de estancias hospitalarias.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro de control de cuentas de estancias.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de control de estancias para facturación: almacena cada período de ocupación de cama por admisión, con el servicio CUPS liquidado, el profesional tratante y las fechas de inicio, fin y liquidación de la estancia hospitalaria.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays';
