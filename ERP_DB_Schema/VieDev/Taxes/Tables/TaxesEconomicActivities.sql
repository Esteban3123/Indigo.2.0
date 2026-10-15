CREATE TABLE [Taxes].[TaxesEconomicActivities] (
    [Id]          INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]        NVARCHAR (5)   NOT NULL,
    [Description] NVARCHAR (400) NOT NULL,
    [Rate]        FLOAT (53)     NOT NULL,
    [Type]        TINYINT        CONSTRAINT [DF_TaxesActivity_Type] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TaxesActivity] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipos de actividad economica:   1 - Actividades Industriales   2 - Actividades Comerciales   3 - Actividades de Servicios   4 - Actividades del Sector Financiero   5 - Otras Actividades ', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesEconomicActivities', @level2type = N'COLUMN', @level2name = N'Type';

