CREATE TABLE [dbo].[HCACTENFE] (
    [CODACTENF]           CHAR (3)   NOT NULL,
    [DESACTENF]           CHAR (100) NOT NULL,
    [EXIREGINS]           BIT        NOT NULL,
    [EXIRESULT]           BIT        NOT NULL,
    [ACTFACTUR]           BIT        NOT NULL,
    [CODSERIPS]           CHAR (20)  NULL,
    [ESTADO]              INT        CONSTRAINT [DF_HCACTENFE_ESTADO] DEFAULT ((1)) NOT NULL,
    [RequiresNursingPack] BIT        CONSTRAINT [DF_HCACTENFE_requiresNursingPack] DEFAULT ((0)) NOT NULL,
    [IDAGPAQUETES]        INT        NULL,
    CONSTRAINT [PK_HCACTENFE] PRIMARY KEY CLUSTERED ([CODACTENF] ASC),
    CONSTRAINT [FK_HCACTENFE_AGPAQUETES] FOREIGN KEY ([IDAGPAQUETES]) REFERENCES [dbo].[AGPAQUETES] ([ID]),
    CONSTRAINT [FK_HCACTENFE_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[HCACTENFE] NOCHECK CONSTRAINT [FK_HCACTENFE_AGPAQUETES];


GO
ALTER TABLE [dbo].[HCACTENFE] NOCHECK CONSTRAINT [FK_HCACTENFE_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del paquete de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'IDAGPAQUETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Requiere paquete de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'RequiresNursingPack';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1-> Activo 2->Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo Unico de Procedimientos y Servicios 

Este campo se dejará de usar y pasara hacer obsoleto para guardar los datos en la tabla dbo.BillableNursingActivityServices 

(PBI 16608 - Creación de descripcion relacionada en actividades de agendamiento)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si la actividad de enfermeria es facturable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'ACTFACTUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exige Resultado en procedimientos de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'EXIRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exige registro de insumos en procedimientos de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'EXIREGINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Actividad de Enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'DESACTENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Actividad de Enfermeria ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTENFE', @level2type = N'COLUMN', @level2name = N'CODACTENF';

