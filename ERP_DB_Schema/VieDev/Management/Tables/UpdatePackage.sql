CREATE TABLE [Management].[UpdatePackage] (
    [Id]              INT           NOT NULL,
    [NumberVersion]   INT           NOT NULL,
    [PackageVersion]  VARCHAR (12)  NOT NULL,
    [Description]     VARCHAR (MAX) NOT NULL,
    [BuildDate]       DATETIME      NOT NULL,
    [CreationDate]    DATETIME      NOT NULL,
    [PackageFileName] VARCHAR (100) NOT NULL,
    [Status]          BIT           NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado | 1 = Activo | 0 = Inactivo', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del archivo del paquete', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'PackageFileName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de construcción', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'BuildDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del paquete', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del paquete', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'PackageVersion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NúmeroVersión', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'NumberVersion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'Id';

