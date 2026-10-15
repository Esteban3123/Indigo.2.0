CREATE TABLE [MedicalHistory].[TypesInfusionPumps] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Supplier]          VARCHAR (200)  NOT NULL,
    [Model]             VARCHAR (300)  NOT NULL,
    [IdPump]            INT            NOT NULL,
    [PurgeVolumenxPump] DECIMAL (7, 2) NOT NULL,
    [State]             BIT            NOT NULL,
    [Code]              VARCHAR (200)  NOT NULL,
    CONSTRAINT [PK_TypesInfusionPumps] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo Bomba De Infusión', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el estado 1 = si  0 = No', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el volumen de la bomba', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'PurgeVolumenxPump';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id de Bombas De Infusión', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'IdPump';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el modelo  Bombas De Infusión', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el proveedor tipos bombas infusion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'Supplier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'Id';

