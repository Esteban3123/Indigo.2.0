CREATE TABLE [Payroll].[PositionRoll] (
    [Id]         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdRol]      INT          NOT NULL,
    [CodeRol]    VARCHAR (20) NOT NULL,
    [IdPosition] INT          NOT NULL,
    CONSTRAINT [PK_PositionRoll] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PositionRoll_Position] FOREIGN KEY ([IdPosition]) REFERENCES [Payroll].[Position] ([Id])
);


GO
ALTER TABLE [Payroll].[PositionRoll] NOCHECK CONSTRAINT [FK_PositionRoll_Position];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Cargo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll', @level2type = N'COLUMN', @level2name = N'IdPosition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del Rol', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll', @level2type = N'COLUMN', @level2name = N'CodeRol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Rol', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll', @level2type = N'COLUMN', @level2name = N'IdRol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll', @level2type = N'COLUMN', @level2name = N'Id';

