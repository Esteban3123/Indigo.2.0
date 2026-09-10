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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Último usuario que modificó el registro de tipo de sujeción/restricción (CHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'ModifyUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ultimo usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'ModifyUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'ModifyUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Última fecha y hora de modificación del registro de restricción (DATETIME, nullable)', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'ModifyDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ultima fecha de modificacion del registro', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'ModifyDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'ModifyDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de tipo de sujeción (CHAR 20, auditoría, rastreo)', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de restricción/sujeción (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion del registro ', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo del tipo de restricción (BIT: 1=activo, 0=inactivo)', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de extremidad asociada: 1=Superior (brazo/mano), 2=Inferior (pierna/pie) (TINYINT, enfermería)', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'TypeLimb';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de extremidad:  1 - Superior  2 - Inferior', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'TypeLimb';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'TypeLimb';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la sujeción/restricción se asocia a una extremidad del cuerpo (BIT: 1=sí, 0=no, enfermería)', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'AssociatedLimb';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se asocia a una extremidad del cuerpo la sujecion', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'AssociatedLimb';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'AssociatedLimb';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del tipo de sujeción/restricción (VARCHAR 100, búsqueda enfermería)', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la sujecion', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la sujeción/restricción (VARCHAR 3, identificador rápido, protocolo enfermería)', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de sujecion ', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable de la tabla TypeRestraint (INT IDENTITY, clave primaria)', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de sujeción o restricción física utilizados en enfermería, indicando si aplica a un miembro del cuerpo específico y el tipo de extremidad involucrada.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TypeRestraint';
