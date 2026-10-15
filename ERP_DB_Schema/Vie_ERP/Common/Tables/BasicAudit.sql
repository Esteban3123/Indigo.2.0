CREATE TABLE [Common].[BasicAudit] (
    [Id]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Tag]             INT           NOT NULL,
    [Entity]          VARCHAR (100) NOT NULL,
    [RegisterId]      VARCHAR (20)  NULL,
    [UserCode]        VARCHAR (20)  NULL,
    [UserName]        VARCHAR (200) NULL,
    [TransactionDate] DATETIME      NOT NULL,
    [Operation]       TINYINT       NOT NULL,
    [UserMachine]     VARCHAR (50)  NULL,
    [Parameters]      VARCHAR (100) NULL,
    [ReportName]      VARCHAR (50)  NULL,
    CONSTRAINT [PK_BasicAudit] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del repórte', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'ReportName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametros', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Parameters';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Máquina', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'UserMachine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Crear, 2 - Modificar, 3 - Confirmar, 4 - Anular, 5 - Eliminar', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Operation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fceha de la Operacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'TransactionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de usuario ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'UserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Registro Auditado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'RegisterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Entidad que se audito', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Entity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tag Del Formulario', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Tag';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Id';

