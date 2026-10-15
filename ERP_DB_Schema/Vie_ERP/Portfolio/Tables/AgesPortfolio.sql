CREATE TABLE [Portfolio].[AgesPortfolio] (
    [Id]                      INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SettingPortfolioId]      INT            NOT NULL,
    [Name]                    VARCHAR (100)  NOT NULL,
    [InitialRange]            INT            NOT NULL,
    [EndRange]                INT            NOT NULL,
    [DeteriorationPercentage] NUMERIC (5, 2) CONSTRAINT [DF_AgesPortfolio_DegradationPercentage] DEFAULT ((0)) NOT NULL,
    [ProvisionPercentage]     NUMERIC (5, 2) CONSTRAINT [DF_AgesPortfolio_DeteriorationPercentage1] DEFAULT ((0)) NOT NULL,
    [Color]                   INT            NOT NULL,
    [CreationUser]            VARCHAR (20)   NOT NULL,
    [CreationDate]            DATETIME       NOT NULL,
    [ModificationUser]        VARCHAR (20)   NULL,
    [ModificationDate]        DATETIME       NULL,
    [TimeStamp]               ROWVERSION     NOT NULL,
    CONSTRAINT [PK_AgesPortfolio] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AgesPortfolio_SettingPortfolio] FOREIGN KEY ([SettingPortfolioId]) REFERENCES [Portfolio].[SettingPortfolio] ([Id])
);


GO
ALTER TABLE [Portfolio].[AgesPortfolio] NOCHECK CONSTRAINT [FK_AgesPortfolio_SettingPortfolio];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AgesPortfolio', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AgesPortfolio', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AgesPortfolio', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AgesPortfolio', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AgesPortfolio', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Color que se selecciono para identiicar el color del rango', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AgesPortfolio', @level2type = N'COLUMN', @level2name = N'Color';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el porcentaje de provision de cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AgesPortfolio', @level2type = N'COLUMN', @level2name = N'ProvisionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el porcentaje de deterioro de cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AgesPortfolio', @level2type = N'COLUMN', @level2name = N'DeteriorationPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor final del rango (Dias)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AgesPortfolio', @level2type = N'COLUMN', @level2name = N'EndRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor inicial del rango, Si es el primer registro este campo sera por defecto 0 (Dias)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AgesPortfolio', @level2type = N'COLUMN', @level2name = N'InitialRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del rango de la edad de cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AgesPortfolio', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del parametro de cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AgesPortfolio', @level2type = N'COLUMN', @level2name = N'SettingPortfolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del rango de edades de cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AgesPortfolio', @level2type = N'COLUMN', @level2name = N'Id';

