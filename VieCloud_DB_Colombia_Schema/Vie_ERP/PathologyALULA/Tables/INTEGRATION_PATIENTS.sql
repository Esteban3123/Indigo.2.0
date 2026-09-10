CREATE TABLE [PathologyALULA].[INTEGRATION_PATIENTS] (
    [id]                    VARCHAR (16)  NOT NULL,
    [patient_id]            CHAR (15)     NOT NULL,
    [type_identification]   VARCHAR (2)   NOT NULL,
    [alternate_patient_id]  INT           NULL,
    [family_name]           VARCHAR (150) NULL,
    [second_family_name]    VARCHAR (150) NULL,
    [given_name]            VARCHAR (150) NULL,
    [middle_name]           VARCHAR (150) NULL,
    [date_of_birth]         DATETIME      NULL,
    [sex]                   VARCHAR (150) NULL,
    [patient_address]       VARCHAR (150) NULL,
    [country_code]          VARCHAR (150) NULL,
    [phone_number_home]     VARCHAR (150) NULL,
    [phone_number_business] VARCHAR (150) NULL,
    [citizenship]           VARCHAR (150) NULL,
    [nationality]           VARCHAR (150) NULL,
    [patient_history]       VARCHAR (150) NULL,
    [email]                 VARCHAR (150) NULL,
    [synchronization]       BIT           NULL,
    CONSTRAINT [PK_INTEGRATION_PATIENTS] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera de sincronización: 0=No leído, 1=Leído. Indica si el registro del paciente fue sincronizado desde el sistema origen (BIT).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'synchronization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bandera:  0 - No ledio    1 - Leido', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'synchronization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'synchronization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico del paciente. Contacto de comunicación digital, información PII sensible (VARCHAR 150).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Correo electrónico del  paciente ', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historia clínica del paciente. Referencia o identificador de registro médico-clínico (VARCHAR 150).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'patient_history';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Historia clínica del  paciente ', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'patient_history';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'patient_history';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nacionalidad del paciente. País de origen o procedencia legal del paciente (VARCHAR 150).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'nationality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nacionalidad del paciente', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'nationality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'nationality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudadanía del paciente. Estatus legal de pertenencia a un país, puede diferir de nacionalidad (VARCHAR 150).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'citizenship';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Ciudadanía del paciente', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'citizenship';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'citizenship';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico de trabajo del paciente. Contacto laboral o comercial, información de comunicación (VARCHAR 150).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'phone_number_business';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Número telefónico de  trabajo', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'phone_number_business';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'phone_number_business';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico de residencia del paciente. Teléfono domiciliario para contacto (VARCHAR 150).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'phone_number_home';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número telefónico de  residencia', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'phone_number_home';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'phone_number_home';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del país de residencia del paciente. Código ISO o estándar de país donde reside (VARCHAR 150).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'country_code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Codigo del pais de  residencia del paciente', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'country_code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'country_code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de residencia del paciente. Domicilio completo, información de ubicación geográfica (VARCHAR 150).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'patient_address';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Dirección de residencia', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'patient_address';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'patient_address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género/Sexo del paciente. Identidad sexual biológica o registrada (VARCHAR 150).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'sex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Género del paciente ', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'sex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'sex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del paciente. Dato demográfico para cálculo de edad, información PII (DATETIME).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'date_of_birth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Fecha de nacimiento del  paciente ', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'date_of_birth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'date_of_birth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre del paciente. Nombre adicional entre primer nombre y apellidos (VARCHAR 150).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'middle_name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo nombre  paciente', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'middle_name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'middle_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre del paciente. Nombre de pila principal (VARCHAR 150).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'given_name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer nombre paciente ', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'given_name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'given_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del paciente. Apellido materno o segundo apellido (VARCHAR 150).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'second_family_name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Segundo apellido del  paciente ', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'second_family_name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'second_family_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido del paciente. Apellido paterno o principal (VARCHAR 150).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'family_name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Primer apellido del  paciente', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'family_name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'family_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador alternativo del paciente en el HIS. ID secundario o número de historia en sistema de información hospitalaria (INT, FK a paciente).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'alternate_patient_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Identificador del paciente  en el HIS', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'alternate_patient_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'alternate_patient_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identificación: CC=Cédula Ciudadanía, CE=Cédula Extranjería, TI=Tarjeta Identidad, RC=Registro Civil, PA=Pasaporte, AS=Adulto Sin ID, MS=Menor Sin ID, NU=Número Único, CN=Certificado Nacido Vivo, CD=Carnet Diplomático, SC=Salvoconducto, PE=Permiso Permanencia (VARCHAR 2).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'type_identification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Documento  CC -  Cédula de Ciudadanía  CE  -  Cédula de Extranjería  TI   -  Tarjeta de Identidad  RC  -  Registro Civil  PA  -  Pasaporte  AS  -  Adulto Sin Identificación  MS  -  Menor Sin Identificación  NU  -  Número único de identificación personal  CN  -  Certificado de Nacido Vivo  CD  -  Carnet Diplomático (Aplica para extranjeros)  SC  -  Salvoconducto (Aplica para extranjeros)  PE  -  Permiso especial de Permanencia (Aplica para extranjeros)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'type_identification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'type_identification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación del paciente. Documento o cédula, identificador único PII (CHAR 15).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'patient_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Número de identificación  del paciente ', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'patient_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'patient_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementado de la tabla INTEGRATION_PATIENTS. Clave primaria técnica para integración de datos de patología ALULA (VARCHAR 16).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos demográficos y de identificación de pacientes integrados con el sistema de patología ALULA. Centraliza la información personal de cada paciente (cédula, nombre, fecha de nacimiento, contacto, nacionalidad) para sincronización entre plataformas.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS';
