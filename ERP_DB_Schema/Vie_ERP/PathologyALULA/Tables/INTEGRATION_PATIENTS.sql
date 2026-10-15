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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bandera:  0 - No ledio    1 - Leido', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'synchronization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Correo electrónico del  paciente ', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Historia clínica del  paciente ', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'patient_history';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nacionalidad del paciente', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'nationality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Ciudadanía del paciente', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'citizenship';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Número telefónico de  trabajo', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'phone_number_business';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número telefónico de  residencia', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'phone_number_home';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Codigo del pais de  residencia del paciente', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'country_code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Dirección de residencia', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'patient_address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Género del paciente ', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'sex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Fecha de nacimiento del  paciente ', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'date_of_birth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo nombre  paciente', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'middle_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer nombre paciente ', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'given_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Segundo apellido del  paciente ', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'second_family_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Primer apellido del  paciente', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'family_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Identificador del paciente  en el HIS', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'alternate_patient_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Documento  CC -  Cédula de Ciudadanía  CE  -  Cédula de Extranjería  TI   -  Tarjeta de Identidad  RC  -  Registro Civil  PA  -  Pasaporte  AS  -  Adulto Sin Identificación  MS  -  Menor Sin Identificación  NU  -  Número único de identificación personal  CN  -  Certificado de Nacido Vivo  CD  -  Carnet Diplomático (Aplica para extranjeros)  SC  -  Salvoconducto (Aplica para extranjeros)  PE  -  Permiso especial de Permanencia (Aplica para extranjeros)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'type_identification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Número de identificación  del paciente ', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'patient_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATION_PATIENTS', @level2type = N'COLUMN', @level2name = N'id';

