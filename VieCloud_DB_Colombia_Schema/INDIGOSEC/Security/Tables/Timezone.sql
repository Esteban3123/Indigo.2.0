CREATE TABLE [Security].[Timezone] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Timezone] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la zona horaria', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Timezone', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la Zonahoraria', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Timezone', @level2type = N'COLUMN', @level2name = N'Name';

