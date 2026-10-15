CREATE TABLE [MedicalHistory].[ElectronicSignatureSigners] (
    [Id]                       INT           IDENTITY (1, 1) NOT NULL,
    [IDHCHISPACA]              INT           NULL,
    [IPCODPACI]                VARCHAR (25)  NOT NULL,
    [NUMINGRES]                CHAR (10)     NOT NULL,
    [NUMEFOLIO]                NCHAR (20)    NULL,
    [CODCENATE]                CHAR (10)     NOT NULL,
    [UFUCODIGO]                CHAR (10)     NOT NULL,
    [RegistrationDate]         DATETIME      NOT NULL,
    [SignatoryType]            INT           NOT NULL,
    [kinship]                  INT           NOT NULL,
    [LastNames]                VARCHAR (100) NOT NULL,
    [Names]                    VARCHAR (100) NOT NULL,
    [TypeIdentification]       INT           NOT NULL,
    [IdentificationNumber]     VARCHAR (25)  NOT NULL,
    [Email]                    VARCHAR (100) NULL,
    [CountryCode]              VARCHAR (5)   NULL,
    [PhoneNumber]              VARCHAR (20)  NULL,
    [OmitsElectronicSignature] BIT           CONSTRAINT [DF_ElectronicSignatureSigners_ElectronicSignatureSigners] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ElectronicSignatureDetail] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Omitir firma electronica, significa que no agregaron ningun firmante al documento y por ende no va existir firmantes, solo la firma del medico que es automatica.    1 - Significa que se va omitir firma  0 - No se va omitir es decir va existir firma electronica de los agregados', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'OmitsElectronicSignature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero celular, debe ser whatssapt.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'PhoneNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo indicativo del pais', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'CountryCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo electronico', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de identificación.   ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de identificacion, relacion con la tabla donde se crean los tipos de identificación.  ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'TypeIdentification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombres de la persona registrada', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'Names';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apellidos de la persona registrada  ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'LastNames';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parentesco    1= Padre  2= Madre  3= Esposo  4= Esposa  5= Hijo(a)  6= Hermano(a)  7= Abuelo(a)  8= Tio(a)  9= Primo(a)  10= Sobrino(a)  11= Amigo(a)  12= Otro  13= Comodín "No aplica": el cual no debe ser visible y debe postularse por default cuando el campo tipo firmante se seleccione "Paciente competente"      ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'kinship';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Firmante    1= Paciente competente   2= Acudiente   3= Tutor legal   4= Representante legal   5= Familiar o pariente cercano   6= Testigo       ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'SignatoryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de registro', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'RegistrationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad funcional', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro atencion  ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Folio generado  ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ingreso paciente', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificacion paciente', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con el tablero de historias', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'Id';

