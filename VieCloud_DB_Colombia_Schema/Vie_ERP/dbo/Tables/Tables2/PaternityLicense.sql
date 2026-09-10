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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de licencia para identificar licencia de maternidad y paternidad (1=Anteparto, 2=Posparto). Clasificación de la prestación económica según fase del embarazo o puerperio. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'Typeoflicense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de licencia  para la identificación de Licencia de maternidad y paternidad    1=Anteparto   2=Posparto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'Typeoflicense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'Typeoflicense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con tabla cabecera de incapacidades HCINCAPAC (FK→CODCONSEC). Vinculación del registro de licencia de maternidad/paternidad con la incapacidad laboral asociada. NUMERIC(18).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'IdHCINCAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla cabecera de incapacidades HCINCAPAC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'IdHCINCAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'IdHCINCAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de certificado de nacido vivo emitido por registraduría. Identificador oficial del recién nacido, campo de texto numérico hasta 15 dígitos. VARCHAR(15).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'liveBirthCertificateNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número certificado nacido vivo  campo de texto numérico de hasta 15 dígitos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'liveBirthCertificateNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'liveBirthCertificateNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Embarazo múltiple: True=Sí (gemelos/múltiples), False=No (embarazo simple). Indicador de gestación de más de un feto. BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'MultiplePregnancy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Embarazo múltiple - Si -> True No->False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'MultiplePregnancy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'MultiplePregnancy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional en semanas al momento del evento. Campo numérico hasta 2 dígitos enteros y 1 decimal (ej: 38.5 semanas), diligenciable manualmente. NUMERIC(18,1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'PregnancyAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad gestacional (semanas) campo numérico de hasta 2 dígitos y un decimal que podrá ser diligenciado manualmente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'PregnancyAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'PregnancyAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha probable o confirmada del parto/alumbramiento. Fecha estimada de término del embarazo para cálculo de licencia perinatal. DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'DateDelivery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha probable del parto ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'DateDelivery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'DateDelivery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de licencia de maternidad/paternidad. INT IDENTITY(1,1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de licencias de paternidad otorgadas a empleados o pacientes, incluyendo los datos del nacimiento, tipo de licencia y su vinculación con la incapacidad médica correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaternityLicense';
