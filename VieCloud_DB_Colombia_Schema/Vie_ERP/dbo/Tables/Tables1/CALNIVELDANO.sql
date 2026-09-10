CREATE TABLE [dbo].[CALNIVELDANO] (
    [ID]            INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]        VARCHAR (3)  NOT NULL,
    [NOMBRE]        VARCHAR (50) NOT NULL,
    [ESTADO]        BIT          NOT NULL,
    [FECHACREACION] DATETIME     NOT NULL,
    [USUARIOCREA]   CHAR (20)    NOT NULL,
    [FECHAMODIFI]   DATETIME     NULL,
    [USUMODIFICA]   CHAR (20)    NULL,
    CONSTRAINT [PK_CALNIVELDAÑO] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modificó el registro (auditoría, CHAR(20), último operador que editó el nivel de daño)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'USUMODIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario quien modifico el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'USUMODIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'USUMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de modificación del registro (DATETIME, NULL si no editado, auditoría de cambios)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'FECHAMODIFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificacion del registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'FECHAMODIFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'FECHAMODIFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro (auditoría, CHAR(20), operador inicial que registró el nivel de daño)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'USUARIOCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario quien creo el registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'USUARIOCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'USUARIOCREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro (DATETIME, trazabilidad, cuándo se originó el nivel de daño)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha creacion del registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del nivel de daño: 1=Activo, 0=Inactivo (BIT, filtro operativo, busca por estado vigente)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'True = Activo   False = Inactivo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del nivel de daño (VARCHAR(50), clasificación de severidad, lesión o patología)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre o Descripcion de Nivel de Daño', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del nivel de daño asignado por usuario (VARCHAR(3), identificador alfanumérico, registro operativo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo que registo el Usuario (codigo de Registro)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY, clave primaria, referencia interna de la tabla)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de niveles o grados de daño utilizados en la calificación clínica o administrativa (por ejemplo, severidad de lesiones, daños en salud o incapacidades). Permite clasificar el nivel de afectación en procesos de atención, auditoría o reconocimiento de siniestros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALNIVELDANO';
