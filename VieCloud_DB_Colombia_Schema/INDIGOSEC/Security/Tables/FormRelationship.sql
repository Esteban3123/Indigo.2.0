CREATE TABLE [Security].[FormRelationship] (
    [Id]        INT         IDENTITY (1, 1) NOT NULL,
    [IdErpForm] VARCHAR (5) NOT NULL,
    [IdHisForm] VARCHAR (5) NOT NULL,
    CONSTRAINT [PK_FormRelationship] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación de id de formularios del HIS y ERP', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'FormRelationship';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'FormRelationship', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de formulario ERP', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'FormRelationship', @level2type = N'COLUMN', @level2name = N'IdErpForm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de formulario HIS', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'FormRelationship', @level2type = N'COLUMN', @level2name = N'IdHisForm';

