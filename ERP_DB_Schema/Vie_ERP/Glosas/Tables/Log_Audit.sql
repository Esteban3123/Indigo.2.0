CREATE TABLE [Glosas].[Log_Audit] (
    [Id]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdRegistro]   NCHAR (10)   NULL,
    [StateOld]     INT          NULL,
    [StateNew]     INT          NULL,
    [PCName]       VARCHAR (50) NULL,
    [CreationUser] VARCHAR (50) NULL,
    [CreationDate] DATETIME     NULL,
    CONSTRAINT [PK_Log_Audit] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Log_Audit', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Log_Audit', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PCName', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Log_Audit', @level2type = N'COLUMN', @level2name = N'PCName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Nuevo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Log_Audit', @level2type = N'COLUMN', @level2name = N'StateNew';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Viejo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Log_Audit', @level2type = N'COLUMN', @level2name = N'StateOld';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de Registro', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Log_Audit', @level2type = N'COLUMN', @level2name = N'IdRegistro';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Log_Audit', @level2type = N'COLUMN', @level2name = N'Id';

