CREATE TABLE [dbo].[INAUTORIG] (
    [PKTIPOCON] INT          NOT NULL,
    [CODGRUPOU] CHAR (3)     NOT NULL,
    [PKVALORCO] CHAR (20)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_INAUTORIG] PRIMARY KEY CLUSTERED ([PKTIPOCON] ASC, [CODGRUPOU] ASC, [PKVALORCO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría, identificador numérico para registro y trazabilidad de cambios en autorizaciones (auditoria, control, conformidad)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIG', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIG', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIG', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de autorización en tabla maestra INAUTORIG de Indigo Crystal.Net, clave primaria que vincula permisos y derechos de acceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIG', @level2type = N'COLUMN', @level2name = N'PKVALORCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla de Autorizaciones - Indigo Crystal.Net', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIG', @level2type = N'COLUMN', @level2name = N'PKVALORCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIG', @level2type = N'COLUMN', @level2name = N'PKVALORCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del grupo de usuarios, clasificación que agrupa perfiles con permisos similares en centros, unidades funcionales y módulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIG', @level2type = N'COLUMN', @level2name = N'CODGRUPOU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Grupo de Usuarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIG', @level2type = N'COLUMN', @level2name = N'CODGRUPOU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIG', @level2type = N'COLUMN', @level2name = N'CODGRUPOU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de configuración autorizable: 1=Centro de Atención 2=Unidad Funcional 3=Plantilla de Edición 4=Múltiples Ingresos 5=Entidad 6=Especialidad Agendamiento 7=Sala Agendamiento 8=Dashboard Sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIG', @level2type = N'COLUMN', @level2name = N'PKTIPOCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1: Centros de Atencion  2: Unidades Funcionales  3: Platillas de Edicion  4: Abrir mas de un Ingreso  5: Entidades  6: Agendamiento: Especialidades  7: Agendamiento: Salas  8: Dashboard: Sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIG', @level2type = N'COLUMN', @level2name = N'PKTIPOCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIG', @level2type = N'COLUMN', @level2name = N'PKTIPOCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de autorizaciones de origen o configuración de valores autorizados por tipo de contrato y grupo. Asocia tipos de contrato con grupos de usuario y valores de código autorizados, incluyendo un indicador de auditoría o formato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIG';
