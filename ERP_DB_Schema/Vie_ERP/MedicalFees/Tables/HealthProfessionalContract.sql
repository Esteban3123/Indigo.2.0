CREATE TABLE [MedicalFees].[HealthProfessionalContract] (
    [Id]                     INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HealthProfessionalCode] CHAR (20) NOT NULL,
    [MedicalFeesContractId]  INT       NOT NULL,
    [LiquidateDefault]       BIT       NOT NULL,
    CONSTRAINT [PK_HealthProfessionalContract] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HealthProfessionalContract_MedicalFeesContract] FOREIGN KEY ([MedicalFeesContractId]) REFERENCES [MedicalFees].[MedicalFeesContract] ([Id])
);


GO
ALTER TABLE [MedicalFees].[HealthProfessionalContract] NOCHECK CONSTRAINT [FK_HealthProfessionalContract_MedicalFeesContract];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica que el contrato se liquida por defecto', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract', @level2type = N'COLUMN', @level2name = N'LiquidateDefault';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del contrato de honorarios medicos', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract', @level2type = N'COLUMN', @level2name = N'Id';

