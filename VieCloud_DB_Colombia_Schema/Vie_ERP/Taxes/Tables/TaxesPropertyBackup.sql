CREATE TABLE [Taxes].[TaxesPropertyBackup] (
    [Id]               INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CodeDeparment]    VARCHAR (2)      NOT NULL,
    [CodeCity]         VARCHAR (3)      NOT NULL,
    [Code]             VARCHAR (50)     NOT NULL,
    [Type]             TINYINT          NOT NULL,
    [Taxed]            BIT              NOT NULL,
    [ThirdPartyId]     INT              NOT NULL,
    [Addres]           VARCHAR (200)    NOT NULL,
    [Commune]          VARCHAR (5)      NOT NULL,
    [EconomicDestiny]  VARCHAR (5)      NOT NULL,
    [LandArea]         DECIMAL (18)     NOT NULL,
    [BuiltArea]        DECIMAL (18)     NOT NULL,
    [Appraisal]        DECIMAL (18)     NOT NULL,
    [Latitude]         DECIMAL (20, 14) NOT NULL,
    [Longitude]        DECIMAL (20, 14) NOT NULL,
    [Status]           TINYINT          NOT NULL,
    [CreationUser]     VARCHAR (20)     NOT NULL,
    [CreationDate]     DATETIME         NOT NULL,
    [ModificationUser] VARCHAR (20)     NULL,
    [ModificationDate] DATETIME         NULL,
    [TimeStamp]        ROWVERSION       NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo TIMESTAMP (SQL Server), registra automáticamente el instante exacto de creación, actualización o modificación del respaldo de propiedad fiscal.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro de respaldo; nulo si nunca fue editado.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica fecha de la última modificación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de usuario (VARCHAR 20) que realizó la última modificación; nulo si el registro no ha sido editado.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el usuario de la última modificación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación inicial del respaldo de propiedad fiscal.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de creación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de usuario (VARCHAR 20) que creó el registro de respaldo de impuesto de propiedad.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el usuario de creación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (TINYINT) del respaldo: código numérico que indica si está activo, inactivo, anulado o en proceso.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el estado del respaldo.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coordenada geográfica de longitud (DECIMAL 20,14) en formato decimal; punto de ubicación georreferenciado de la propiedad.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Longitude';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la longitud.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Longitude';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Longitude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coordenada geográfica de latitud (DECIMAL 20,14) en formato decimal; punto de ubicación georreferenciado de la propiedad.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Latitude';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la latitud.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Latitude';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Latitude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Avalúo catastral o valor tasado (DECIMAL 18) de la propiedad; base para cálculo de impuestos territoriales.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Appraisal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Evaluación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Appraisal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Appraisal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área o superficie construida (DECIMAL 18) en metros cuadrados de las edificaciones; excluye terreno sin construcción.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'BuiltArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece superficie construida.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'BuiltArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'BuiltArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área o superficie total del terreno (DECIMAL 18) en metros cuadrados; incluye todo el lote de tierra.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'LandArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la superficie.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'LandArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'LandArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Destino económico (VARCHAR 5); código que clasifica el uso de la propiedad: residencial, comercial, industrial, agrícola, etc.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'EconomicDestiny';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el destino económico.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'EconomicDestiny';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'EconomicDestiny';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de comuna (VARCHAR 5); identificador administrativo de la división territorial o jurisdicción local.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Commune';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la comuna.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Commune';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Commune';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección completa (VARCHAR 200) del inmueble; ubicación física, calle, número y referencias de la propiedad.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Addres';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la dirección.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Addres';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Addres';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del tercero, contribuyente o propietario asociado; clave foránea a tabla de terceros.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero asociado.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): 1=Sí gravado/sujeto a impuesto de propiedad, 0=No gravado/exento.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Taxed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo booleano que almacena si es gravado o no. 1 - Si, 0 - No.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Taxed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Taxed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de respaldo (TINYINT); código que categoriza la clase o modalidad del registro de impuesto de propiedad.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el tipo del respaldo.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único (VARCHAR 50) del respaldo de propiedad; referencia catastral o número de predial.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código identificador del respaldo.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de municipio o ciudad (VARCHAR 3); identificador territorial donde se localiza la propiedad.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'CodeCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la ciudad del respaldo.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'CodeCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'CodeCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de departamento o región (VARCHAR 2); división administrativa superior que contiene la propiedad.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'CodeDeparment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del departamento del respaldo.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'CodeDeparment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'CodeDeparment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del respaldo de impuesto de propiedad; clave primaria autoincrementada.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del respaldo del impuesto de propiedad.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respaldo histórico de predios o propiedades registradas en el módulo de impuestos. Contiene la información catastral, fiscal y geográfica de cada inmueble, incluyendo avalúo, áreas y ubicación, utilizada para liquidación y control de impuestos prediales.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup';
