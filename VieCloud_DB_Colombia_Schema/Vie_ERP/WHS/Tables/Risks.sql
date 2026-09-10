CREATE TABLE [WHS].[Risks] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [Name]             VARCHAR (80) NOT NULL,
    [State]            BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) CONSTRAINT [DF_Risks_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME     CONSTRAINT [DF_Risks_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    CONSTRAINT [PK_Risks] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ__Risks__A25C5AA72B83790D] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de última modificación del riesgo; nula si aún no ha sido editado; permite auditoría de cambios', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación del riesgo; nulo si no se ha editado desde creación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modificación de Usuario', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro del riesgo; marca de cuándo se registró inicialmente en el catálogo', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó el registro del riesgo; identificación de quién ingresó el riesgo al sistema', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación de Usuario', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del riesgo (BIT): 1=Activo, 0=Inactivo; indica si el riesgo está vigente en el catálogo de evaluación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado (1 - Activo, 0 - Inactivo)', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 80) del riesgo identificado; descripción del peligro o factor de riesgo en ambiente laboral o asistencial', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20, UQ) del riesgo; identificador alfanumérico para búsqueda y referencia en reportes de peligros y evaluación de riesgos', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del riesgo en el catálogo de riesgos de seguridad y salud ocupacional', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de riesgos clínicos o de seguridad del paciente. Registra los tipos de riesgo identificados en el sistema (por ejemplo, riesgo de caída, riesgo de úlcera, riesgo quirúrgico), con su código, nombre, estado activo/inactivo y trazabilidad de auditoría de creación y modificación.', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'Risks';
