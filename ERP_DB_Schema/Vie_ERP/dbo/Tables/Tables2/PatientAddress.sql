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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de dirección: 1. Residencial, 2. Empresarial, 3. Extranjero, 4. Hotel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'AddressType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo check para validar si es la dirección principal del paciente. 0: No 1: Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IsMain';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo de check zona rural. 0: No - 1: Si ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'SecludedZone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dirección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Depmuncod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Depcodigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del país', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IdCountry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientAddress', @level2type = N'COLUMN', @level2name = N'Id';

