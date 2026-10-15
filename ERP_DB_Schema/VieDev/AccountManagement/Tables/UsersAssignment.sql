CREATE TABLE [AccountManagement].[UsersAssignment] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [UserCode]  VARCHAR (20)   NOT NULL,
    [FullName]  NVARCHAR (100) NOT NULL,
    [Status]    BIT            DEFAULT ((1)) NOT NULL,
    [EntryType] TINYINT        DEFAULT ((0)) NOT NULL,
    [IsRemoved] BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Ingreso, 1 = Ambulatorio, 2 = Hospitalario, 3  = Ambos', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'EntryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del usuario asignado, por defecto es 1 = Activo. 0 = Inactivo', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del usuario', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'FullName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del usuario del Tenant', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Id';

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Flag para soft-deletes' , @level0type=N'SCHEMA',@level0name=N'AccountManagement', @level1type=N'TABLE',@level1name=N'UsersAssignment', @level2type=N'COLUMN',@level2name=N'IsRemoved'

