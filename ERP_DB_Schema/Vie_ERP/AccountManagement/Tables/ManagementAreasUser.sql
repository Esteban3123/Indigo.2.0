CREATE TABLE [AccountManagement].[ManagementAreasUser] (
    [Id]                INT          IDENTITY (1, 1) NOT NULL,
    [ManagementAreasId] INT          NOT NULL,
    [UserId]            INT          NOT NULL,
    [Usercode]          VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_ManagementAreasUser_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ManagementAreasUser_ManagementAreas] FOREIGN KEY ([ManagementAreasId]) REFERENCES [AccountManagement].[ManagementAreas] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser', @level2type = N'COLUMN', @level2name = N'Usercode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del usuario', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la area de gestion', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser', @level2type = N'COLUMN', @level2name = N'ManagementAreasId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de la tabla', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'ManagementAreasUser', @level2type = N'COLUMN', @level2name = N'Id';

