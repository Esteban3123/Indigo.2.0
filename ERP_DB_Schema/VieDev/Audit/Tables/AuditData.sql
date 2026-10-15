CREATE TABLE [Audit].[AuditData] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [EntityName]   VARCHAR (250) NOT NULL,
    [EntityId]     INT           NOT NULL,
    [JSONData]     VARCHAR (MAX) NULL,
    [CreationUser] VARCHAR (20)  NOT NULL,
    [CreationDate] DATETIME      NOT NULL,
    CONSTRAINT [PK_AuditData] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Texto en formato Json con los cambios hechos al registro', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'JSONData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro al cual se guarda la auditoria', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la tabla a la cual se le hace auditoria', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'Id';

