CREATE TABLE [Glosas].[GlosasPublicParametersInterface] (
    [Id]                              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParametersInterfaceId]           INT          NULL,
    [YearAccount]                     INT          NOT NULL,
    [DebitAccount]                    VARCHAR (15) NOT NULL,
    [AcceptanceObjectionsConcept]     VARCHAR (3)  NULL,
    [AcceptanceObjectionsConceptPast] VARCHAR (3)  NULL,
    [AcceptancReiterationsConcept]    VARCHAR (3)  NULL,
    [TimeStamp]                       ROWVERSION   NOT NULL,
    CONSTRAINT [PK_GlosasPublicParametersInterface] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosasPublicParametersInterface_GlosasParametersInterface] FOREIGN KEY ([ParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosasPublicParametersInterface] NOCHECK CONSTRAINT [FK_GlosasPublicParametersInterface_GlosasParametersInterface];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP SQL Server) del evento de glosa: instante de creación, registro o modificación del parámetro de interfaz. Auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto contable (VARCHAR 3) para aceptación de reiteraciones en glosas. Código de naturaleza deudora/acreedora del reclamo reiterado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptancReiterationsConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de aceptacion de reiteraciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptancReiterationsConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptancReiterationsConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto contable (VARCHAR 3) de aceptación de objeciones para vigencias anteriores. Clasificación contable del reclamo histórico aceptado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptanceObjectionsConceptPast';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de Aceptacion de objeciones para vigencias Anteriores', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptanceObjectionsConceptPast';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptanceObjectionsConceptPast';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto contable (VARCHAR 3) de aceptación de objeciones en glosa actual. Código deudor/acreedor del reclamo rechazado por asegurador.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptanceObjectionsConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de Aceptacion de objeciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptanceObjectionsConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptanceObjectionsConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cuenta débito (VARCHAR 15) contable. Referencia a plan de cuentas para registrar glosas aceptadas, objeciones y reiteraciones en interfaz contable.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'DebitAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Debito', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'DebitAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'DebitAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año de vigencia contable (INT) de las cuentas débito. Período fiscal para clasificación y auditoría de glosas en sistema de información.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'YearAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año de las cuentas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'YearAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'YearAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador FK (INT) de configuración de interfaz en tabla GlosasParametersInterface. Clave de asociación a parámetros generales de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo configuracion de interface', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador primario (INT IDENTITY) autonumérico de la tabla. Clave única de cada registro de parámetros públicos de interfaz de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración para la interfaz de publicación de glosas contables: define las cuentas contables de débito por año y los conceptos utilizados en aceptaciones, objeciones y reiteraciones dentro del proceso de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface';
