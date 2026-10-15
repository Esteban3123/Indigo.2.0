CREATE TABLE [Payroll].[PositionUser] (
    [Id]         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdUser]     INT          NOT NULL,
    [CodeUser]   VARCHAR (20) NOT NULL,
    [IdPosition] INT          NOT NULL,
    CONSTRAINT [PK_PositionUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PositionUser_Position] FOREIGN KEY ([IdPosition]) REFERENCES [Payroll].[Position] ([Id])
);


GO
ALTER TABLE [Payroll].[PositionUser] NOCHECK CONSTRAINT [FK_PositionUser_Position];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Cargo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser', @level2type = N'COLUMN', @level2name = N'IdPosition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del Usuario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser', @level2type = N'COLUMN', @level2name = N'CodeUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Usuario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser', @level2type = N'COLUMN', @level2name = N'IdUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser', @level2type = N'COLUMN', @level2name = N'Id';

