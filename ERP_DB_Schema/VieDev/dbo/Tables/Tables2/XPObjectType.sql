CREATE TABLE [dbo].[XPObjectType] (
    [OID]          INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TypeName]     NVARCHAR (254) NULL,
    [AssemblyName] NVARCHAR (254) NULL,
    CONSTRAINT [PK_XPObjectType__OID] PRIMARY KEY CLUSTERED ([OID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_XPObjectType__TypeName]
    ON [dbo].[XPObjectType]([TypeName] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre ensamblado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'XPObjectType', @level2type = N'COLUMN', @level2name = N'AssemblyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre escrito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'XPObjectType', @level2type = N'COLUMN', @level2name = N'TypeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificadores de Objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'XPObjectType', @level2type = N'COLUMN', @level2name = N'OID';

