CREATE TABLE [Taxes].[TaxesProperty] (
    [Id]               INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CodeDeparment]    VARCHAR (2)      NOT NULL,
    [CodeCity]         VARCHAR (3)      NOT NULL,
    [Code]             VARCHAR (50)     NOT NULL,
    [Type]             TINYINT          CONSTRAINT [DF_TaxesProperty_Type] DEFAULT ((0)) NOT NULL,
    [Taxed]            BIT              NOT NULL,
    [ThirdPartyId]     INT              NOT NULL,
    [Addres]           VARCHAR (200)    NOT NULL,
    [Commune]          VARCHAR (5)      NOT NULL,
    [EconomicDestiny]  VARCHAR (5)      NOT NULL,
    [LandArea]         DECIMAL (18)     NOT NULL,
    [BuiltArea]        DECIMAL (18)     NOT NULL,
    [Appraisal]        DECIMAL (18)     NOT NULL,
    [Latitude]         DECIMAL (20, 14) CONSTRAINT [DF_TaxesProperty_Latitude] DEFAULT ((0)) NOT NULL,
    [Longitude]        DECIMAL (20, 14) CONSTRAINT [DF_TaxesProperty_Longitude] DEFAULT ((0)) NOT NULL,
    [Status]           TINYINT          NOT NULL,
    [CreationUser]     VARCHAR (20)     NOT NULL,
    [CreationDate]     DATETIME         NOT NULL,
    [ModificationUser] VARCHAR (20)     NULL,
    [ModificationDate] DATETIME         NULL,
    [TimeStamp]        ROWVERSION       NOT NULL,
    CONSTRAINT [PK_TaxesProperty__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaxesProperty_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_TaxesProperty__Code]
    ON [Taxes].[TaxesProperty]([Code] ASC);


GO
CREATE NONCLUSTERED INDEX [Taxed_Status_Addres_LandArea]
    ON [Taxes].[TaxesProperty]([Taxed] ASC, [Status] ASC, [Addres] ASC, [LandArea] ASC)
    INCLUDE([Code], [Id], [ThirdPartyId]);


GO
CREATE NONCLUSTERED INDEX [IX_TaxesProperty__Type]
    ON [Taxes].[TaxesProperty]([Type] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP (SQL Server) del evento de creación, registro o modificación del predio. Genera automáticamente el instante exacto de cualquier cambio en el registro.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de la última modificación del registro del predio. Null si no ha sido modificado desde su creación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que realizó la última modificación del registro del predio. Null si no ha sido modificado.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de creación del registro del predio en el sistema. Captura el momento exacto del registro inicial.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que creó el registro del predio en el sistema. Identifica quién registró el activo inmueble.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado TINYINT del predio: 1=Activo (tributa o está en vigencia), 0=Inactivo (no tributa o está cancelado). Controla la vigencia del registro tributario.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el estado del activo  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coordenada geográfica DECIMAL(20,14) de longitud (eje Este-Oeste) del predio. Complementa con Latitude para geolocalización exacta.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Longitude';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Longitud', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Longitude';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Longitude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coordenada geográfica DECIMAL(20,14) de latitud (eje Norte-Sur) del predio. Permite mapeo y ubicación precisa en sistemas GIS.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Latitude';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Latitud', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Latitude';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Latitude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Avalúo o valor catastral DECIMAL(18) del predio. Especifica el valor comercial o tasación del inmueble para fines tributarios.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Appraisal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Avaluo del Predio', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Appraisal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Appraisal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área construida DECIMAL(18) en metros cuadrados del predio. Especifica la superficie total de edificaciones sobre el terreno.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'BuiltArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Area Contruida del predio', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'BuiltArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'BuiltArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área del terreno DECIMAL(18) en metros cuadrados del predio. Especifica la superficie total del lote o parcela de tierra.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'LandArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Area  del terreno', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'LandArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'LandArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Destino económico VARCHAR(5) del predio: código que indica uso (residencial, comercial, industrial, agrícola, etc.). Clasifica la destinación económica del inmueble.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'EconomicDestiny';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el destino Economico', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'EconomicDestiny';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'EconomicDestiny';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comuna VARCHAR(5) o código de división administrativa donde se localiza el predio. Especifica la subdivisión geográfica menor.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Commune';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Comuna', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Commune';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Commune';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección VARCHAR(200) del predio. Especifica la ubicación física completa del inmueble (calle, número, complementos).', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Addres';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la direccion del predio', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Addres';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Addres';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT del tercero propietario o titular principal del predio. Foreign Key a [Common].[ThirdParty]. Vincula el activo al propietario.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del tercero principal o el propietario principal', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de tributación del predio: 1=Tributa (debe pagar impuesto predial), 0=No tributa (exento o sin obligación). Especifica obligación tributaria.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Taxed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el predio tributa', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Taxed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Taxed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de predio TINYINT: 1=Urbano (área de influencia municipal), 2=Rural (zona no urbanizada). Clasifica la categoría catastral del inmueble.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo del predio  1 - Urbano  2 - Rural', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR(50) único del predio catastral. Identificador único de referencia en el registro de predios (matrícula o código predial).', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de ciudad VARCHAR(3) donde se localiza el predio. Referencia códigos municipales o de división administrativa intermedia.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'CodeCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de ciudad', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'CodeCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'CodeCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de departamento VARCHAR(2) donde se localiza el predio. Referencia códigos de división administrativa mayor (región/estado).', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'CodeDeparment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de Departamento', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'CodeDeparment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'CodeDeparment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico INT IDENTITY(1,1) de la tabla TaxesProperty. Primary Key que identifica única y secuencialmente cada registro de predio.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de predios o inmuebles sujetos al impuesto predial. Guarda la información catastral, tributaria y geográfica de cada predio, incluyendo su avalúo, áreas, propietario y estado de gravamen.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesProperty';
