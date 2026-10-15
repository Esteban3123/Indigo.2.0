CREATE TABLE [dbo].[PaternityLicense] (
    [Id]                         INT             IDENTITY (1, 1) NOT NULL,
    [DateDelivery]               DATETIME        NULL,
    [PregnancyAge]               NUMERIC (18, 1) NULL,
    [MultiplePregnancy]          BIT             NULL,
    [liveBirthCertificateNumber] VARCHAR (15)    NULL,
    [IdHCINCAPAC]                NUMERIC (18)    NULL,
    [Typeoflicense]              INT             NULL,
    CONSTRAINT [PK__Paternit__3214EC07857D01D4] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaternityLicense_HCINCAPAC] FOREIGN KEY ([IdHCINCAPAC]) REFERENCES [dbo].[HCINCAPAC] ([CODCONSEC])
);


GO
ALTER TABLE [dbo].[PaternityLicense] NOCHECK CONSTRAINT [FK_PaternityLicense_HCINCAPAC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de licencia  para la identificación de Licencia de maternidad y paternidad    1=Anteparto   2=Posparto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'Typeoflicense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla cabecera de incapacidades HCINCAPAC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'IdHCINCAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número certificado nacido vivo  campo de texto numérico de hasta 15 dígitos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'liveBirthCertificateNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Embarazo múltiple - Si -> True No->False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'MultiplePregnancy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad gestacional (semanas) campo numérico de hasta 2 dígitos y un decimal que podrá ser diligenciado manualmente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'PregnancyAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha probable del parto ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'DateDelivery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'Id';

