CREATE TABLE [Integrations].[CIMAHospital_RelatedAdmission] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AdmissionNumberVie]    VARCHAR (20) NOT NULL,
    [AdmissionNumberLegacy] VARCHAR (20) NOT NULL,
    [RegistrationDate]      DATETIME     CONSTRAINT [DF_CIMAHospital_RelatedAdmission_RegistrationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    CONSTRAINT [PK_CIMAHospital_RelatedAdmission] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro de la relación entre admisiones (DATETIME). Marca cuándo se vinculó el ingreso legado con el nuevo sistema Indigo Vie.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission', @level2type = N'COLUMN', @level2name = N'RegistrationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission', @level2type = N'COLUMN', @level2name = N'RegistrationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission', @level2type = N'COLUMN', @level2name = N'RegistrationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de admisión del sistema legado (VARCHAR 20). Identificador único del ingreso en la base de datos anterior, usado para trazabilidad e integración histórica.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission', @level2type = N'COLUMN', @level2name = N'AdmissionNumberLegacy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de admisión Legado', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission', @level2type = N'COLUMN', @level2name = N'AdmissionNumberLegacy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission', @level2type = N'COLUMN', @level2name = N'AdmissionNumberLegacy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de admisión en Indigo Vie Cloud (VARCHAR 20). Identificador único del ingreso en el sistema nuevo, equivalente a código de atención/ingreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission', @level2type = N'COLUMN', @level2name = N'AdmissionNumberVie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de admisión Vista', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission', @level2type = N'COLUMN', @level2name = N'AdmissionNumberVie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission', @level2type = N'COLUMN', @level2name = N'AdmissionNumberVie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria de la tabla (INT IDENTITY). Identificador único interno para cada relación registrada entre admisiones legado y Vie.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de correspondencia entre admisiones del sistema Indigo Vie Cloud y admisiones del sistema legado CIMA Hospital. Permite relacionar el número de ingreso propio con el número de ingreso del sistema anterior para trazabilidad e integración.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission';
