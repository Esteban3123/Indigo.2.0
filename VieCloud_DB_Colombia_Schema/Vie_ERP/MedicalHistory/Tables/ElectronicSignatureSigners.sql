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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de omisión de firma electrónica (BIT: 1=omitir, 0=aplicar). Cuando es 1, no se registran firmantes adicionales y solo aplica la firma automática del médico; cuando es 0, se requiere firma electrónica de acudientes, tutores o familiares designados.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'OmitsElectronicSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Omitir firma electronica, significa que no agregaron ningun firmante al documento y por ende no va existir firmantes, solo la firma del medico que es automatica.    1 - Significa que se va omitir firma  0 - No se va omitir es decir va existir firma electronica de los agregados', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'OmitsElectronicSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'OmitsElectronicSignature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico o celular del firmante (VARCHAR 20). Preferentemente WhatsApp para contacto y notificaciones de firma electrónica. Búsqueda: teléfono, celular, contacto.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'PhoneNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero celular, debe ser whatssapt.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'PhoneNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'PhoneNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código indicativo internacional del país del firmante (VARCHAR 5, ej: +57 para Colombia). Identifica procedencia geográfica y prefijo telefónico.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'CountryCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo indicativo del pais', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'CountryCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'CountryCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico del firmante (VARCHAR 100). Dirección de envío de documentos y notificaciones relacionadas con la firma electrónica. Datos sensibles/PII.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo electronico', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación del firmante: cédula, pasaporte, documento de identidad (VARCHAR 25). Identificador único del acudiente, tutor o familiar. Datos sensibles/PII.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de identificación.   ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación del firmante (INT, relación con tabla de tipos). Valores: cédula ciudadanía, cédula extranjería, pasaporte, permiso residencia, etc. Referencia a catálogo de identificaciones.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'TypeIdentification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de identificacion, relacion con la tabla donde se crean los tipos de identificación.  ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'TypeIdentification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'TypeIdentification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombres del firmante registrado en el documento de identificación (VARCHAR 100). Búsqueda: nombre, denominación, nombre de acudiente, tutor o testigo.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'Names';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombres de la persona registrada', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'Names';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'Names';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Apellidos del firmante registrado en el documento de identificación (VARCHAR 100). Búsqueda: apellido, familia, linaje del acudiente, tutor o representante legal.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'LastNames';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Apellidos de la persona registrada  ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'LastNames';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'LastNames';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grado de parentesco del firmante respecto al paciente (INT): 1=Padre, 2=Madre, 3=Esposo, 4=Esposa, 5=Hijo(a), 6=Hermano(a), 7=Abuelo(a), 8=Tío(a), 9=Primo(a), 10=Sobrino(a), 11=Amigo(a), 12=Otro, 13=No aplica (default para paciente competente).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'kinship';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parentesco    1= Padre  2= Madre  3= Esposo  4= Esposa  5= Hijo(a)  6= Hermano(a)  7= Abuelo(a)  8= Tio(a)  9= Primo(a)  10= Sobrino(a)  11= Amigo(a)  12= Otro  13= Comodín "No aplica": el cual no debe ser visible y debe postularse por default cuando el campo tipo firmante se seleccione "Paciente competente"      ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'kinship';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'kinship';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de firmante/signatario (INT): 1=Paciente competente, 2=Acudiente, 3=Tutor legal, 4=Representante legal, 5=Familiar o pariente cercano, 6=Testigo. Define rol en firma electrónica de documentos médicos.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'SignatoryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Firmante    1= Paciente competente   2= Acudiente   3= Tutor legal   4= Representante legal   5= Familiar o pariente cercano   6= Testigo       ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'SignatoryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'SignatoryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del firmante en el sistema (DATETIME). Timestamp de creación del registro de firma electrónica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'RegistrationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de registro', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'RegistrationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'RegistrationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (CHAR 10). Identifica área clínica o departamento donde se requiere la firma electrónica. Búsqueda: unidad, área, servicio, departamento.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad funcional', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10). Identifica institución, sede o establecimiento de salud donde se registra el firmante. Búsqueda: centro, institución, sede, hospital, clínica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro atencion  ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio generado (NCHAR 20). Identificador único del documento con firma electrónica asociado a este firmante.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Folio generado  ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente (CHAR 10). Referencia al episodio de atención, hospitalización o consulta. Clave de relación con proceso asistencial.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingreso paciente', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del paciente: cédula, documento o código (VARCHAR 25). Código único del paciente cuyos documentos requieren firma electrónica. Búsqueda: cédula paciente, identificación paciente, documento paciente. Datos sensibles/PII.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion paciente', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de historia clínica electrónica del paciente (INT, FK). Relación con tabla de historias médicas. Vincula el firmante al registro clínico completo del paciente.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con el tablero de historias', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY). Clave primaria secuencial de la tabla de firmantes de firma electrónica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Firmantes de firma electrónica en documentos de historia clínica. Registra las personas (paciente, familiar u otro signatario) que firman electrónicamente documentos como folios de hospitalización, consentimientos u otros registros clínicos, incluyendo sus datos de identificación y contacto.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignatureSigners';
