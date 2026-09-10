CREATE TABLE [Admissions].[PatientAddress] (
    [Id]            INT                                                                              IDENTITY (1, 1) NOT NULL,
    [IdPatient]     INT                                                                              NULL,
    [IdAddressType] INT                                                                              NOT NULL,
    [IdUbication]   INT                                                                              NOT NULL,
    [Address]       VARCHAR (200)                                                                    NOT NULL,
    [RuralArea]     VARCHAR (2)                                                                      NULL,
    [IsMain]        BIT                                                                              NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [ZipCode]       VARCHAR (20)                                                                     NULL,
    CONSTRAINT [PK_PatientAddress] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Admissions].[PatientAddress].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
CREATE NONCLUSTERED INDEX [IX_PatientAddress_IPCODPACI_IsMain_Address]
    ON [Admissions].[PatientAddress]([IPCODPACI] ASC, [IsMain] ASC)
    INCLUDE([Address]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código postal/ZIP de la ubicación; campo NULL en EHR Colombia, mantenido por homogeneidad con estructura CIMA', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'ZipCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código postal de la ubicacion (Campo que en EHR Colombia se guarda NULL y que se crea para mantener homogeneidad con la estructura BD de CIMA)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'ZipCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'ZipCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación/cédula del paciente (PII Identification_Ofuscado); dato sensible enmascarado', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Documento del paciente', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de dirección principal del paciente: 1=Sí (principal), 0=No (secundaria)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IsMain';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para validar si es la dirección principal 1: Si, 0: No', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IsMain';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IsMain';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación zona rural: 01=Zona Rural, 02=Zona Urbana; determina cobertura geográfica', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'RuralArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si es zona rural 01: Si, 02: No', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'RuralArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'RuralArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección física completa del paciente (calle, número, barrio, apto); datos PII sensibles', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Address';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo dirección', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Address';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de ubicación geográfica, referencia a tabla INUBICACI (departamento/municipio/localidad)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IdUbication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la ubicación (Tabla INUBICACI)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IdUbication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IdUbication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de dirección del paciente: 1=Residencial, 2=Empresarial, 3=Extranjero, 4=Hotel', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IdAddressType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de dirección 1: Residencial, 2: Empresarial, 3: Extranjero, 4: Hotel', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IdAddressType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IdAddressType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del paciente asociado al domicilio; campo sin uso activo en el sistema', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IdPatient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del paciente (sin utilizar)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IdPatient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IdPatient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK), consecutivo secuencial de la tabla PatientAddress', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Direcciones y ubicaciones registradas para cada paciente: domicilio, tipo de dirección (residencia, trabajo, etc.), zona rural o urbana y código postal. Permite identificar dónde vive o puede ser contactado el paciente.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress';
