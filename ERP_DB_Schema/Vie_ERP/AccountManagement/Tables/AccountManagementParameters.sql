CREATE TABLE [AccountManagement].[AccountManagementParameters] (
    [Id]                  INT          IDENTITY (1, 1) NOT NULL,
    [AutomaticAssignment] BIT          NOT NULL,
    [EntryType]           VARCHAR (50) NULL,
    [StartDateAssignment] DATETIME     NULL,
    [BedClass]            VARCHAR (50) NULL,
    [CreationUser]        VARCHAR (20) NOT NULL,
    [CreationDate]        DATETIME     NOT NULL,
    [ModificationUser]    VARCHAR (50) NULL,
    [ModificationDate]    DATETIME     NULL,
    CONSTRAINT [PK_AccountManagementParameters_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase de Cama:  1: Observacion Urgencias  2: Recuperacion Post-Quirurgico  3: Hospitalaria  4:Cuna de Observación ', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementParameters', @level2type = N'COLUMN', @level2name = N'BedClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo tipo fecha definida por el usuario', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementParameters', @level2type = N'COLUMN', @level2name = N'StartDateAssignment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Ingreso:  1 = Ambulatorio  2 = Hospitalario', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementParameters', @level2type = N'COLUMN', @level2name = N'EntryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asignación automatica:  0 = No  1 = Si', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementParameters', @level2type = N'COLUMN', @level2name = N'AutomaticAssignment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de la tabla', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementParameters', @level2type = N'COLUMN', @level2name = N'Id';

