CREATE TABLE [dbo].[CHPARAMUF] (
    [CODCENATE] CHAR (10) NOT NULL,
    [UFUCODIGO] CHAR (10) NOT NULL,
    [EXDOSITRA] BIT       NOT NULL,
    [EXDOSIEGR] BIT       NOT NULL,
    [DESMEDTRA] CHAR (1)  NULL,
    [DESMEDEGR] CHAR (1)  NULL,
    [PERCAMCAN] BIT       NULL,
    [EXIINPARA] BIT       NOT NULL,
    CONSTRAINT [PK_CHPARAMUF] PRIMARY KEY CLUSTERED ([CODCENATE] ASC, [UFUCODIGO] ASC),
    CONSTRAINT [FK_CHPARAMUF_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_CHPARAMUF_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Exige interpretación de paraclínicos (exámenes, laboratorios, imágenes) para autorizar traslado de hospitalización. Bit (0=No exigir, 1=Exigir).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'EXIINPARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exige interpretacion de paraclinicos para el traslado de hospitalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'EXIINPARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'EXIINPARA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permite cambiar cantidades de medicamentos dispensados en recetas/órdenes. Bit (0=No permite, 1=Permite ajuste de cantidades).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'PERCAMCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permitir Cambiar Cantidades de los Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'PERCAMCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'PERCAMCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Destino obligatorio de medicamentos sobrantes en egreso del paciente. Char(1): 1=Devolutivo a farmacia, 2=Especificado por usuario (Devolutivo/Ninguno), 3=Ninguno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'DESMEDEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exigir destino de medicamentos sobrantes en Egresos  1-Exigir destino de medicamentos(Devolutivo a farmacia)  2-Exigir destino de medicamentos(Especificado por el usuario Devolutivo - Ninguno)  3-Exigir destino de medicamentos(Ninguno)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'DESMEDEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'DESMEDEGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Destino obligatorio de medicamentos sobrantes en traslado de camas entre unidades. Char(1): 1=Devolutivo a farmacia, 2=Traslado a unidad destino, 3=Especificado por usuario (Devolutivo/Traslado), 4=Ninguno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'DESMEDTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exigir destino de medicamentos sobrantes en Traslados.    1 -Exigir destino de medicamentos(Devolutivo a farmacia)  2 - Exigir destino de medicamentos(Traslado a unidad destino).  3- Exigir destino de medicamentos(Especificado por el usuario Devolutivo - Traslado).  4- Exigir destino de medicamentos(Ninguno)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'DESMEDTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'DESMEDTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Exige aplicación de dosis pendientes antes de egreso/alta del paciente. Bit (0=No exigir, 1=Exigir registro de dosis).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'EXDOSIEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exigir la aplicación de dosis pendientes en Egresos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'EXDOSIEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'EXDOSIEGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Exige aplicación de dosis pendientes antes de trasladar paciente entre camas/unidades. Bit (0=No exigir, 1=Exigir registro de dosis).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'EXDOSITRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exigir la aplicación de dosis pendientes en traslados de camas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'EXDOSITRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'EXDOSITRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional (servicio, piso, área de atención). FK a INUNIFUNC. Char(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención (hospital, clínica, sede). FK a ADCENATEN. Char(10). Identificador de la instalación de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración por unidad funcional que controlan el comportamiento del sistema de medicamentos y dosis en los procesos de traslado y egreso de pacientes dentro de cada centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF';
