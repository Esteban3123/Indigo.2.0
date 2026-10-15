CREATE TABLE [Common].[HealthProfessional] (
    [Id]                        INT          IDENTITY (1, 1) NOT NULL,
    [IdentificationTypeId]      INT          NOT NULL,
    [IdentificationNumber]      VARCHAR (25) NOT NULL,
    [FirstName]                 VARCHAR (50) NOT NULL,
    [SecondName]                VARCHAR (50) NULL,
    [FirstLastName]             VARCHAR (50) NOT NULL,
    [SecondLastName]            VARCHAR (50) NULL,
    [ProfessionalSpecialty]     VARCHAR (3)  NULL,
    [ExternalProfessional]      BIT          NOT NULL,
    [ProfessionalLicenseNumber] VARCHAR (15) NULL,
    [CreationUser]              VARCHAR (20) NOT NULL,
    [CreationDate]              DATETIME     NOT NULL,
    [ModificationUser]          VARCHAR (20) NULL,
    [ModificationDate]          DATETIME     NULL,
    [Timestamp]                 ROWVERSION   NOT NULL,
    [Status]                    BIT          NOT NULL,
    CONSTRAINT [PK_HealthProfessional__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HealthProfessional_ADTIPOIDENTIFICA] FOREIGN KEY ([IdentificationTypeId]) REFERENCES [dbo].[ADTIPOIDENTIFICA] ([ID])
);


GO
ALTER TABLE [Common].[HealthProfessional] NOCHECK CONSTRAINT [FK_HealthProfessional_ADTIPOIDENTIFICA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'Timestamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero tarjeta profesional', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'ProfessionalLicenseNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional Externo 1- si, 0-No', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'ExternalProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la especialidad Tabla INESPECIA', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'ProfessionalSpecialty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero de identificacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificacion de maestro ADTIPOIDENTIFICA', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'IdentificationTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'Id';

