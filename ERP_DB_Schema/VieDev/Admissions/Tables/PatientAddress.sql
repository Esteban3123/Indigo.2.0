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
CREATE NONCLUSTERED INDEX [IX_PatientAddress_IPCODPACI_IsMain_Address]
    ON [Admissions].[PatientAddress]([IPCODPACI] ASC, [IsMain] ASC)
    INCLUDE([Address]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código postal de la ubicacion (Campo que en EHR Colombia se guarda NULL y que se crea para mantener homogeneidad con la estructura BD de CIMA)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'ZipCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Documento del paciente', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para validar si es la dirección principal 1: Si, 0: No', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IsMain';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si es zona rural 01: Si, 02: No', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'RuralArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo dirección', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la ubicación (Tabla INUBICACI)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IdUbication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de dirección 1: Residencial, 2: Empresarial, 3: Extranjero, 4: Hotel', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IdAddressType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del paciente (sin utilizar)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IdPatient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Id';

