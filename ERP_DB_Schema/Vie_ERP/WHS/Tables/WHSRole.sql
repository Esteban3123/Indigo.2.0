CREATE TABLE [WHS].[WHSRole] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [Name]             VARCHAR (80) NOT NULL,
    [State]            BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) CONSTRAINT [DF_WHSRole_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME     CONSTRAINT [DF_WHSRole_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    CONSTRAINT [PK_WHSRole] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ__WHSRole__A25C5AA749CA2641] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación de Usuario', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación de Usuario', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRole', @level2type = N'COLUMN', @level2name = N'Id';

