CREATE TABLE [MedicalHistory].[PharmaProfileDetail] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PharmaProfileHeaderId] INT             NOT NULL,
    [ProductCode]           CHAR (20)       NOT NULL,
    [Dose]                  VARCHAR (500)   NOT NULL,
    [Quantity]              INT             NOT NULL,
    [RouteofAdministration] VARCHAR (20)    NULL,
    [UnitMeasure]           VARCHAR (20)    NULL,
    [Indications]           VARCHAR (MAX)   NULL,
    [DurationFrequency]     INT             NULL,
    [FrequencyUnit]         INT             NULL,
    [DurationType]          VARCHAR (30)    NULL,
    [Weight]                NUMERIC (18, 2) NULL,
    [TotalVolume]           NUMERIC (18, 2) NULL,
    [AdministrationVolume]  NUMERIC (18, 2) NULL,
    [AdministrationTime]    INT             NULL,
    [InfusionSpeed]         NUMERIC (18, 2) NULL,
    [IDHCINFLIQC]           INT             NULL,
    CONSTRAINT [PK_PharmaProfileDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PharmaProfileDetail_HCVIAADMI] FOREIGN KEY ([RouteofAdministration]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM]),
    CONSTRAINT [FK_PharmaProfileDetail_IHLISTPRO] FOREIGN KEY ([ProductCode]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_PharmaProfileDetail_PharmaProfileHeader] FOREIGN KEY ([PharmaProfileHeaderId]) REFERENCES [MedicalHistory].[PharmaProfileHeader] ([Id])
);


GO
ALTER TABLE [MedicalHistory].[PharmaProfileDetail] NOCHECK CONSTRAINT [FK_PharmaProfileDetail_HCVIAADMI];


GO
ALTER TABLE [MedicalHistory].[PharmaProfileDetail] NOCHECK CONSTRAINT [FK_PharmaProfileDetail_IHLISTPRO];


GO
ALTER TABLE [MedicalHistory].[PharmaProfileDetail] NOCHECK CONSTRAINT [FK_PharmaProfileDetail_PharmaProfileHeader];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla cabecera de mezclas - HCINFLIQC ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'IDHCINFLIQC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Velocidad de infusion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'InfusionSpeed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo de administracion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'AdministrationTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Volumen de administracion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'AdministrationVolume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Volumen total', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'TotalVolume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de duracion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'DurationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de la frecuencia', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'FrequencyUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duracion de la frecuencia', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'DurationFrequency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicaciones', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Indications';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'UnitMeasure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Via de administracion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'RouteofAdministration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Administracion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Dose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del producto', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'ProductCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relación con la cabecera del perfil farmacoterapéutico (PharmaProfileHeader)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'PharmaProfileHeaderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo consecutivo d la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileDetail', @level2type = N'COLUMN', @level2name = N'Id';

