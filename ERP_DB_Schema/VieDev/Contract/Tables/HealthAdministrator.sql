CREATE TABLE [Contract].[HealthAdministrator] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (300) NOT NULL,
    [ThirdPartyId]     INT           NOT NULL,
    [EntityType]       TINYINT       NOT NULL,
    [HealthEntityCode] VARCHAR (6)   NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    [Regimen4505]      CHAR (1)      NULL,
    [MOSTRARWEB]       BIT           NULL,
    CONSTRAINT [PK_HealthAdministrator__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HealthAdministrator_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Contract].[HealthAdministrator] NOCHECK CONSTRAINT [FK_HealthAdministrator_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mostrar citas En Web  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '"C" para Contributivo  "S" para Subsidiado  "P" para Excepción  "E" para Especial  "N" para No asegurado    Este campo lo pidieron desde Crystal para que lo utilizara Juan David Patiño', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Regimen4505';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del manual tarifario  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de entidad  1 - EPS Contributivo  2 - EPS Subsidiado  3 - ET Vinculados Municipios  4 - ET Vinculados Departamentos  5 - ARL Riesgos Laborales  6 - MP Medicina Prepagada  7 - IPS Privada  8 - IPS Publica  9 - Regimen Especial  10 - Accidentes de transito  11 - Fosyga  12 - Otros', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'HealthAdministrator', @level2type = N'COLUMN', @level2name = N'EntityType';

