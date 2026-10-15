CREATE TABLE [Contract].[RIPSServices] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Code]             VARCHAR (4)   NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [ServiceGroup]     INT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [Status]           BIT           NOT NULL,
    CONSTRAINT [PK__RIPSServ__3214EC07A2FD2708] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RIPSServices_RIPSServiceGroups] FOREIGN KEY ([ServiceGroup]) REFERENCES [Contract].[RIPSServiceGroups] ([Id])
);


GO
ALTER TABLE [Contract].[RIPSServices] NOCHECK CONSTRAINT [FK_RIPSServices_RIPSServiceGroups];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de cada servicio RIPS,              1 - Activo              0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RIPSServices', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de última modificación del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RIPSServices', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de última modificación del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RIPSServices', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RIPSServices', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario creador del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RIPSServices', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave foránea al grupo de servicios RIPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RIPSServices', @level2type = N'COLUMN', @level2name = N'ServiceGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de cada servicio', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RIPSServices', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código numérico del servicio RIPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RIPSServices', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id autoincremental de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RIPSServices', @level2type = N'COLUMN', @level2name = N'Id';

