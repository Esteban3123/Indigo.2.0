CREATE TABLE [Nursing].[TypeRestraint] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [Code]           VARCHAR (3)   NOT NULL,
    [Name]           VARCHAR (100) NOT NULL,
    [AssociatedLimb] BIT           NOT NULL,
    [TypeLimb]       TINYINT       NULL,
    [State]          BIT           NOT NULL,
    [CreationDate]   DATETIME      NOT NULL,
    [CreationUser]   CHAR (20)     NOT NULL,
    [ModifyDate]     DATETIME      NULL,
    [ModifyUser]     CHAR (20)     NULL,
    CONSTRAINT [PK_RCTIPACCEVE] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ultimo usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'ModifyUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ultima fecha de modificacion del registro', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'ModifyDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creacion del registro ', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de extremidad:
1 - Superior
2 - Inferior', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'TypeLimb';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si se asocia a una extremidad del cuerpo la sujecion', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'AssociatedLimb';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la sujecion', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de sujecion ', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumerico identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'Id';

