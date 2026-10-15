CREATE TABLE [AccountManagement].[AssignmentManagement] (
    [Id]         INT        IDENTITY (1, 1) NOT NULL,
    [LastUserId] INT        NOT NULL,
    [RowVersion] ROWVERSION NOT NULL,
    [EntryType]  TINYINT    NOT NULL,
    CONSTRAINT [FK_AssignmentManagement_UsersAssignment] FOREIGN KEY ([LastUserId]) REFERENCES [AccountManagement].[UsersAssignment] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ingreso, 1=Ambulatorio, 2=Hospitalario', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement', @level2type = N'COLUMN', @level2name = N'EntryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indice de concurrencia', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement', @level2type = N'COLUMN', @level2name = N'RowVersion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del último usuario (facturador) al que se le asignó un ingreso a facturar', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement', @level2type = N'COLUMN', @level2name = N'LastUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement', @level2type = N'COLUMN', @level2name = N'Id';

