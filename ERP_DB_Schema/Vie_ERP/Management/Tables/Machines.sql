CREATE TABLE [Management].[Machines] (
    [Id]            INT           NOT NULL,
    [UID]           VARCHAR (50)  NOT NULL,
    [Name]          VARCHAR (100) NOT NULL,
    [NickName]      VARCHAR (100) NOT NULL,
    [ClientVersion] VARCHAR (12)  NULL,
    [OSName]        VARCHAR (100) NOT NULL,
    [Architecture]  VARCHAR (5)   NOT NULL,
    [IPs]           VARCHAR (500) NOT NULL,
    [MACs]          VARCHAR (500) NOT NULL,
    [RegDate]       DATETIME      NOT NULL,
    [LastUpDate]    DATETIME      NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Última actualización', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'LastUpDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'RegDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ordenador', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'MACs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Instituto prestador de salud', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'IPs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Arquitectura', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'Architecture';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del sistema operativo', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'OSName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del cliente', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'ClientVersion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Apodo del equipo', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'NickName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del equipo', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación única del equipo', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'UID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'Id';

