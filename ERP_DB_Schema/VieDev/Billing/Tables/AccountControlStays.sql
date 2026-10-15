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
    CONSTRAINT [PK_AccountControlStays] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Creación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario que realizo la creación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la liquidación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'LiquidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final de la estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de la estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'StartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle de la orden de servicio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad del profesional a cargo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'ProfessionalSpecialistCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional a cargo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la descripción del contrato de la entidad del CUPS que tiene la estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del CUPS que tiene la estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'CupsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la estancia CHREGESTA', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'StayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el número de la cama donde se encuentra el paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'Bed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código que hace referencia al numero de ingreso del paciente de la tabla ADINGRESO.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'AdmissionCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro de control de cuentas de estancias.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountControlStays', @level2type = N'COLUMN', @level2name = N'Id';

