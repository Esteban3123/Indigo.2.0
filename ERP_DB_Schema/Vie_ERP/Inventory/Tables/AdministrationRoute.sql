CREATE TABLE [Inventory].[AdministrationRoute] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PharmaceuticalFormId]       INT           NULL,
    [Code]                       VARCHAR (20)  NOT NULL,
    [Name]                       VARCHAR (100) NOT NULL,
    [CrystalAdministrationRoute] VARCHAR (20)  NULL,
    [Status]                     BIT           NOT NULL,
    [CreationUser]               VARCHAR (20)  CONSTRAINT [DF_AdministrationRoute_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]               DATETIME      CONSTRAINT [DF_AdministrationRoute_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]           VARCHAR (20)  NULL,
    [ModificationDate]           DATETIME      NULL,
    CONSTRAINT [PK_AdministrationRoute] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AdministrationRoute_PharmaceuticalForm] FOREIGN KEY ([PharmaceuticalFormId]) REFERENCES [Inventory].[PharmaceuticalForm] ([Id])
);


GO
ALTER TABLE [Inventory].[AdministrationRoute] NOCHECK CONSTRAINT [FK_AdministrationRoute_PharmaceuticalForm];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdministrationRoute', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdministrationRoute', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdministrationRoute', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdministrationRoute', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento 1 - Activo 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdministrationRoute', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Via de aministracion de crystal que se obtiene de la tabla HCVIAADMI', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdministrationRoute', @level2type = N'COLUMN', @level2name = N'CrystalAdministrationRoute';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la via de administración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdministrationRoute', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la via de administracion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdministrationRoute', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la forma farmaceutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdministrationRoute', @level2type = N'COLUMN', @level2name = N'PharmaceuticalFormId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la via de administracion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdministrationRoute', @level2type = N'COLUMN', @level2name = N'Id';

