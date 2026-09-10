CREATE TABLE [dbo].[TUVRGrupo] (
    [CODIGO] CHAR (10) NOT NULL,
    [UVR]    CHAR (15) NOT NULL,
    [Grupo]  CHAR (5)  NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TUVRGrupo', @level2type = N'COLUMN', @level2name = N'Grupo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TUVRGrupo', @level2type = N'COLUMN', @level2name = N'UVR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TUVRGrupo', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla que asocia un código de procedimiento o servicio con una Unidad de Valor Relativo (UVR) y un grupo de clasificación. Permite agrupar procedimientos según su UVR para efectos de tarifación o liquidación en el contexto del sistema de salud colombiano. No define claves primarias ni foráneas explícitas en su DDL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUVRGrupo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUVRGrupo';
GO
