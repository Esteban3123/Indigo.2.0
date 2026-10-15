CREATE TABLE [Admissions].[GroupsERCPrecursoras] (
    [Id]               INT       IDENTITY (1, 1) NOT NULL,
    [GroupCode]        INT       NOT NULL,
    [CreationDate]     DATETIME  NOT NULL,
    [UserCreation]     CHAR (20) NOT NULL,
    [ModificationDate] DATETIME  NULL,
    [UserModification] CHAR (20) NULL,
    CONSTRAINT [PK__GroupsER__3214EC076A87D3D4] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'UserModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda la fecha cuando se modifica el registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el usuario que creo el registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'UserCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda la fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el codigo del grupo (1. Hipertensión Arterial, 2. Diabetes Mellitus y 3. Enfermedad Renal Crónica (ERC))', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'GroupCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el identificador unico de cada registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'Id';

