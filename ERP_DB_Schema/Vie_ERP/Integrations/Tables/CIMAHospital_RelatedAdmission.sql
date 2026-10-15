CREATE TABLE [Integrations].[CIMAHospital_RelatedAdmission] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AdmissionNumberVie]    VARCHAR (20) NOT NULL,
    [AdmissionNumberLegacy] VARCHAR (20) NOT NULL,
    [RegistrationDate]      DATETIME     CONSTRAINT [DF_CIMAHospital_RelatedAdmission_RegistrationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    CONSTRAINT [PK_CIMAHospital_RelatedAdmission] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Registro', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission', @level2type = N'COLUMN', @level2name = N'RegistrationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de admisión Legado', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission', @level2type = N'COLUMN', @level2name = N'AdmissionNumberLegacy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de admisión Vista', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission', @level2type = N'COLUMN', @level2name = N'AdmissionNumberVie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_RelatedAdmission', @level2type = N'COLUMN', @level2name = N'Id';

