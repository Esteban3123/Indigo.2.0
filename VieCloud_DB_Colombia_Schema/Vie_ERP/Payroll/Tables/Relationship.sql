CREATE TABLE [Payroll].[Relationship] (
    [Id]                   INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]           INT            NOT NULL,
    [Name]                 VARCHAR (100)  NOT NULL,
    [KinshipId]            INT            NOT NULL,
    [BirthDate]            DATE           NULL,
    [ProvidesUPC]          BIT            NOT NULL,
    [UPCValue]             NUMERIC (18)   NOT NULL,
    [Dependent]            BIT            NOT NULL,
    [DependsType]          TINYINT        NULL,
    [DependsValue]         NUMERIC (18)   NULL,
    [DependsPercentage]    DECIMAL (5, 2) NULL,
    [State]                NCHAR (10)     NULL,
    [IsEmergencyContact]   BIT            NULL,
    [PhoneNumber]          NVARCHAR (20)  NULL,
    [Gender]               TINYINT        NULL,
    [IdentificationType]   INT            NULL,
    [IdentificationNumber] VARCHAR (50)   NULL,
    CONSTRAINT [PK_Kinship] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Kinship_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_Relationship_Kinship] FOREIGN KEY ([KinshipId]) REFERENCES [Payroll].[Kinship] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación del pariente (cédula, pasaporte, documento extranjero). VARCHAR(50), PII - Identificación_Ofuscado. Búsqueda: cédula, documento, identificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de de identificacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación: 0=CC, 1=CE, 2=TI, 3=RC, 4=PA, 5=AS, 6=MS, 7=NIT, 8=NU, 9=CN, 10=CD, 11=SC, 12=PE, 13=PT, 14=DE, 15=SI. INT, FK→Payroll.Kinship', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'IdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de identificacion   0=Cédula de Ciudadanía (CC)  1=Cédula de Extranjería (CE)  2=Tarjeta de Identidad (TI)  3=Registro Civil (RC)  4=Pasaporte (PA)  5=Adulto Sin Identificación (AS)  6=Menor Sin Identificación (MS)  7= Nit (NI)  8= Número único de identificación personal (NU)  9= Cetrigicado Nacido Vivo (CN)  10= Carnet Diplomático (CD)  11= Salvoconducto (SC)  12 = Permiso especial de Permanencia (PE)  13 = Permiso por Protección Temporal (PT)  14 = Documento extranjero (DE)  15 = Sin identificacion (SI)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'IdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'IdentificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género del pariente: 1=Masculino, 2=Femenino, 3=Otro. TINYINT. Búsqueda: sexo, género', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'Gender';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Género (1- Masculino, 2 - Femenino, 3 - Otro)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'Gender';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'Gender';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono de contacto del pariente. NVARCHAR(20), PII. Búsqueda: teléfono, celular, contacto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'PhoneNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de telefono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'PhoneNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'PhoneNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si es contacto de emergencia: 1=Sí, 0=No. BIT. Búsqueda: emergencia, contacto urgencia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'IsEmergencyContact';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Es contacto de emergencia? | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'IsEmergencyContact';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'IsEmergencyContact';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: 1=Activo, 0=Inactivo. NCHAR(10). Búsqueda: estado, activo, inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1 Activo, 0 Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de dependencia económica del pariente (válido si DependsType=2). DECIMAL(5,2). Búsqueda: porcentaje dependiente, deducción', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'DependsPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de Dependiente (Si el Tipo de Dependiente (DependsType) es 2)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'DependsPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'DependsPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en pesos de conceptos dependientes para reducción en retención en la fuente (válido si DependsType=1). NUMERIC(18). Búsqueda: valor dependiente, RRF', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'DependsValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de Conceptos Dependientes para Reducción en Retención en la Fuente (Si el Tipo de Dependiente (DependsType) es 1)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'DependsValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'DependsValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de dependencia: 1=Por Valor económico, 2=Por Porcentaje. TINYINT. Búsqueda: tipo dependiente, modalidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'DependsType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Dependiente: 1 - Por Valor, 2 - Por Porcentaje.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'DependsType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'DependsType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si el pariente es persona a cargo/dependiente: 1=Sí, 0=No. BIT. Búsqueda: persona a cargo, dependiente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'Dependent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'si es persona a cargo o no', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'Dependent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'Dependent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor aporte en pesos a UPC (Unidad de Pago por Capitación) del pariente. NUMERIC(18). Búsqueda: UPC, aporte, cotización', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'UPCValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Aporte UPC', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'UPCValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'UPCValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si el pariente aporta a UPC: 1=Sí, 0=No. BIT. Búsqueda: aporte UPC, cotización salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'ProvidesUPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aporta UPC (SI/NO)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'ProvidesUPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'ProvidesUPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del pariente. DATE NULL. Búsqueda: cumpleaños, edad, nacimiento', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'BirthDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Cumpleaños', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'BirthDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'BirthDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de parentesco/relación familiar (padre, madre, hijo, cónyuge, etc). INT, FK→Payroll.Kinship. Búsqueda: parentesco, familiar, relación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'KinshipId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Parentezco', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'KinshipId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'KinshipId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del pariente del empleado. VARCHAR(100). Búsqueda: nombre pariente, familiar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Pariente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del empleado relacionado. INT, FK→Payroll.Employee. Búsqueda: empleado, trabajador', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de relación/pariente. INT IDENTITY. Búsqueda: identificador, ID relación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Beneficiarios y familiares a cargo de cada empleado: registra parentesco, fecha de nacimiento, si generan UPC o beneficio de dependiente, porcentaje o valor de ese beneficio, contacto de emergencia e identificación personal.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship';
