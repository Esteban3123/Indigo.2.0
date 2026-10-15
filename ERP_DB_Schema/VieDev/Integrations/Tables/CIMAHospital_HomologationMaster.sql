CREATE TABLE [Integrations].[CIMAHospital_HomologationMaster] (
    [Id]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MasterType]  VARCHAR (200) NOT NULL,
    [Observation] VARCHAR (200) NULL,
    [IndigoCode]  VARCHAR (200) NOT NULL,
    [LegacyCode]  VARCHAR (200) NOT NULL,
    [State]       BIT           NOT NULL,
    CONSTRAINT [PK_HospitalCIMA_HomologationMaster] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0 - Inactive  1 - Active   ', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del maestro del sistema externo', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'LegacyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del maestro indigo', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'IndigoCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Nombre del maestro a homologar', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'MasterType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'Id';

