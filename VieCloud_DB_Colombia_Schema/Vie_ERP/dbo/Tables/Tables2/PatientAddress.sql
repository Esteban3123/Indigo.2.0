CREATE TABLE [dbo].[PatientAddress] (
    [Id]           INT                                                                              IDENTITY (1, 1) NOT NULL,
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IdCountry]    INT                                                                              NOT NULL,
    [Depcodigo]    CHAR (2)                                                                         NOT NULL,
    [Depmuncod]    CHAR (5)                                                                         NOT NULL,
    [Address]      VARCHAR (200)                                                                    NOT NULL,
    [SecludedZone] BIT                                                                              NOT NULL,
    [IsMain]       BIT                                                                              NOT NULL,
    [AddressType]  INT                                                                              NULL,
    CONSTRAINT [PK_PatientAddress_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[PatientAddress].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de dirección del paciente: 1=Residencial, 2=Empresarial, 3=Extranjero, 4=Hotel. INT, clasifica domicilio de atención o contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'AddressType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de dirección: 1. Residencial, 2. Empresarial, 3. Extranjero, 4. Hotel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'AddressType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'AddressType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): 1=dirección principal/preferente del paciente, 0=dirección secundaria. Define ubicación de correspondencia y contacto prioritario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IsMain';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo check para validar si es la dirección principal del paciente. 0: No 1: Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IsMain';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IsMain';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): 1=zona rural/apartada, 0=zona urbana. Clasifica accesibilidad geográfica para atención domiciliaria o seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'SecludedZone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de check zona rural. 0: No - 1: Si ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'SecludedZone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'SecludedZone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del domicilio: calle, número, barrio, vereda. VARCHAR(200), ubicación física del paciente para contacto y atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Address';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Address';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del municipio en formato CHAR(5). Identificador geográfico administrativo municipal del domicilio del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Depmuncod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Depmuncod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Depmuncod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del departamento/provincia en formato CHAR(2). Identificador geográfico administrativo regional del domicilio del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Depcodigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Depcodigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Depcodigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT) del país de la dirección. FK a tabla de países; 1=Colombia, otros según catálogo internacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IdCountry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del país', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IdCountry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IdCountry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, equivalente a cédula/documento de identificación. VARCHAR(25) con enmascaramiento PII (partial). Identificación única del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria (INT IDENTITY). Identificador único secuencial de cada registro de dirección del paciente en la tabla PatientAddress.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Direcciones de residencia o contacto de los pacientes. Registra la ubicación geográfica (país, departamento, municipio), la dirección física, si pertenece a zona apartada y si es la dirección principal del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress';
