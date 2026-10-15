CREATE TABLE [Common].[HealthProvider] (
    [Id]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                         VARCHAR (20)  NOT NULL,
    [Name]                         VARCHAR (200) NOT NULL,
    [ThirdPartyId]                 INT           NOT NULL,
    [CityId]                       INT           NOT NULL,
    [HealthProviderType]           TINYINT       NOT NULL,
    [HealthProviderClass]          TINYINT       NOT NULL,
    [RepresentativeName]           VARCHAR (100) NULL,
    [RepresentativeIdentification] VARCHAR (50)  NULL,
    [Status]                       BIT           NOT NULL,
    [CreationUser]                 VARCHAR (20)  NOT NULL,
    [CreationDate]                 DATETIME      NOT NULL,
    [ModificationUser]             VARCHAR (20)  NULL,
    [ModificationDate]             DATETIME      NULL,
    [TimeStamp]                    ROWVERSION    NOT NULL,
    CONSTRAINT [PK_HealthProvider] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HealthProvider_City] FOREIGN KEY ([CityId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_HealthProvider_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Common].[HealthProvider] NOCHECK CONSTRAINT [FK_HealthProvider_City];


GO
ALTER TABLE [Common].[HealthProvider] NOCHECK CONSTRAINT [FK_HealthProvider_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del de la entidad 1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProvider', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase de la prestadora de servicio  1 - Ambulatoria  2 - Hospitalaria  3 - Mixta', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProvider', @level2type = N'COLUMN', @level2name = N'HealthProviderClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de prestadora de servicio  1 - Tipo 1  2 - Tipo 2  3 - Tipo 3  4 - Tipo 4', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProvider', @level2type = N'COLUMN', @level2name = N'HealthProviderType';

