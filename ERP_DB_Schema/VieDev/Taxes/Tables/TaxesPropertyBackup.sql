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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica fecha de la última modificación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el usuario de la última modificación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la fecha de creación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el usuario de creación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el estado del respaldo.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena la longitud.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Longitude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena la latitud.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Latitude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evaluación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Appraisal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece superficie construida.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'BuiltArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena la superficie.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'LandArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el destino económico.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'EconomicDestiny';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena la comuna.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Commune';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena la dirección.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Addres';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero asociado.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo booleano que almacena si es gravado o no. 1 - Si, 0 - No.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Taxed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el tipo del respaldo.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del respaldo.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la ciudad del respaldo.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'CodeCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del departamento del respaldo.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'CodeDeparment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del respaldo del impuesto de propiedad.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyBackup', @level2type = N'COLUMN', @level2name = N'Id';

