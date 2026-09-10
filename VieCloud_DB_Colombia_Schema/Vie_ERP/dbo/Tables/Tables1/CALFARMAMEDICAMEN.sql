CREATE TABLE [dbo].[CALFARMAMEDICAMEN] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFARMACOVIGILANCIA] INT           NOT NULL,
    [SCI]                 INT           NULL,
    [CODPRODUC]           CHAR (20)     NULL,
    [INDICACIONES]        VARCHAR (MAX) NULL,
    [DOSIS]               VARCHAR (MAX) NULL,
    [CODUNIMED]           CHAR (3)      NULL,
    [CODVIAADM]           CHAR (3)      NULL,
    [FRECUENCIA]          VARCHAR (MAX) NULL,
    [FECHAINICIO]         DATETIME      NULL,
    [FECHAFIN]            DATETIME      NULL,
    CONSTRAINT [PK_CALFARMAMEDICAMEN] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALFARMAMEDICAMEN_CALFARMACOVIGILANCIA] FOREIGN KEY ([IDFARMACOVIGILANCIA]) REFERENCES [dbo].[CALFARMACOVIGILANCIA] ([ID]),
    CONSTRAINT [FK_CALFARMAMEDICAMEN_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final de administración del medicamento; cierre de tratamiento farmacológico (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'FECHAFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'FECHAFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'FECHAFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicio de administración del medicamento; comienzo de tratamiento farmacológico (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'FECHAINICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicio ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'FECHAINICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'FECHAINICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia de administración del medicamento; pauta, intervalo o ritmo de dosis (cada 8h, diario, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'FRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'FRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'FRECUENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código vía de administración del medicamento; ruta terapéutica (oral, IV, IM, tópica, etc.) (CHAR 3).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Via Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad de medida de dosis; unidad farmacéutica (mg, ml, comp, inyección, etc.) (CHAR 3).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Unidad Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis del medicamento; cantidad y concentración administrada por toma o intervalo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'DOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis del medicamentos ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'DOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'DOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones terapéuticas del medicamento; motivo, síntoma, diagnóstico o condición clínica para su prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'INDICACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'INDICACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'INDICACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto medicamento; identificador único vinculado a catálogo IHLISTPRO (FK, CHAR 20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del producto Relacionado con CODPRODUC de la tabla IHLISTPRO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Acción; código o clasificación de tipo de acción sobre el medicamento prescrito (INT, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'SCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Accion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'SCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'SCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID farmacovigilancia; referencia a evento de seguridad/monitoreo farmacológico en CALFARMACOVIGILANCIA (FK INT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'IDFARMACOVIGILANCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID que coresponde con ID de la tabla CALFARMACOVIGILANCIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'IDFARMACOVIGILANCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'IDFARMACOVIGILANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico); clave primaria de medicamento en evento de farmacovigilancia (IDENTITY INT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamentos asociados a un registro de farmacovigilancia o alerta farmacológica. Guarda el detalle de cada medicamento involucrado en un evento adverso o seguimiento farmacológico: producto, dosis, vía de administración, frecuencia y período de uso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMAMEDICAMEN';
