CREATE TABLE [dbo].[CHCAMAASEO] (
    [ID]             INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODICAMAS]      INT       NOT NULL,
    [ESTADO]         INT       NOT NULL,
    [FECHAINI]       DATETIME  NULL,
    [FECHAFIN]       DATETIME  NULL,
    [USUARIOINI]     CHAR (20) NULL,
    [USUARIOFIN]     CHAR (20) NULL,
    [USUARIOREG]     CHAR (20) NOT NULL,
    [FECHAREG]       DATETIME  NOT NULL,
    [USUARIOMOD]     CHAR (20) NULL,
    [FECHAMOD]       DATETIME  NULL,
    [ULTIMALIMPIEZA] DATETIME  NULL,
    [ULTIMAJORNADA]  DATETIME  NOT NULL,
    [LIMPIEZAEXTRA]  BIT       NOT NULL,
    [TIPOLIMPIEZA]   INT       NOT NULL,
    CONSTRAINT [PK_CHCAMAASEO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CHCAMAASEO_CHCAMASHO] FOREIGN KEY ([CODICAMAS]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de limpieza de cama (INT): 1=Rutinaria, 2=Extra, 3=Terminal, Resto=Otro. Clasificación del protocolo de aseo aplicado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'TIPOLIMPIEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1= Rutinaria  2= Extra  3= Terminal  Resto = Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'TIPOLIMPIEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'TIPOLIMPIEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de si se realizó limpieza extra en la cama además de la rutinaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'LIMPIEZAEXTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que indica si hubo limpieza extra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'LIMPIEZAEXTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'LIMPIEZAEXTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Última fecha y hora (DATETIME) en que se inició la jornada de aseo de la cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'ULTIMAJORNADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ultima fecha y hora que se inicio la jornada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'ULTIMAJORNADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'ULTIMAJORNADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del último evento de limpieza/aseo ejecutado en la cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'ULTIMALIMPIEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la ultima limpieza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'ULTIMALIMPIEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'ULTIMALIMPIEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro de aseo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'FECHAMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (CHAR 20) que realizó la última modificación del registro de aseo de cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'USUARIOMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'USUARIOMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'USUARIOMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación/registro inicial del documento de aseo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'FECHAREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha creacion registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'FECHAREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'FECHAREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario responsable (CHAR 20) que creó el registro de aseo de cama en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'USUARIOREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario crea registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'USUARIOREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'USUARIOREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario profesional de salud/limpieza (CHAR 20) que finalizó/completó el evento de limpieza.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'USUARIOFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que finalizo la limpieza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'USUARIOFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'USUARIOFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario profesional de salud/limpieza (CHAR 20) que inició el evento de limpieza en la cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'USUARIOINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que inicio la limpieza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'USUARIOINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'USUARIOINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final (DATETIME) del evento de limpieza de cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'FECHAFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final limpieza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'FECHAFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'FECHAFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial (DATETIME) del inicio de la jornada de aseo de cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'FECHAINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial inicio jornada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'FECHAINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'FECHAINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la cama (INT): 1=Inicia Jornada (rojo), 2=Inicia Limpieza (amarillo), 3=Finaliza Limpieza (verde). Indicador visual del progreso de aseo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Inicia Jornada (color rojo)  2 - Inicia Limpieza (color amarillo)  3 - Finaliza Limpieza (color verde)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cama (INT, FK→CHCAMASHO.CODICAMAS). Referencia a la cama hospitalaria objeto del aseo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de relacion con cama CAMASHO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'CODICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) del registro de aseo de cama en la tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de aseo y limpieza de camas hospitalarias. Controla el estado de limpieza de cada cama, las fechas de inicio y fin del proceso, el tipo de limpieza realizada y los usuarios responsables de cada actividad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMAASEO';
