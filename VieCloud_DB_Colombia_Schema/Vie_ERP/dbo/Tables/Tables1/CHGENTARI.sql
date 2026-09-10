CREATE TABLE [dbo].[CHGENTARI] (
    [CODCONCEC]                      INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODICAMAS]                      INT       NOT NULL,
    [CODTIPEST]                      CHAR (3)  NOT NULL,
    [TIPLIQEST]                      TINYINT   NOT NULL,
    [NUMHOREST]                      TINYINT   NOT NULL,
    [GENCUPS]                        INT       NULL,
    [GENCUPS2]                       INT       NOT NULL,
    [CODCENATE]                      CHAR (10) NOT NULL,
    [UFUCODIGO]                      CHAR (10) NOT NULL,
    [IDDESCRIPCIONRELACIONADA_CUPS]  INT       NULL,
    [IDDESCRIPCIONRELACIONADA_CUPS2] INT       NULL,
    [USUARIOCREACION]                CHAR (20) NULL,
    [FECHACREACION]                  DATETIME  NULL,
    [USUARIOMODIFICACION]            CHAR (20) NULL,
    [FECHAMODIFICACION]              DATETIME  NULL,
    CONSTRAINT [PK_CHGENTARI] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_CHGENTARI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_CHGENTARI_CHCAMASHO] FOREIGN KEY ([CODICAMAS]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_CHGENTARI_CHGENTARI] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_CHGENTARI_CHTIPESTA] FOREIGN KEY ([CODTIPEST]) REFERENCES [dbo].[CHTIPESTA] ([CODTIPEST]),
    CONSTRAINT [FK_CHGENTARI_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_CHGENTARI_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);




GO



GO



GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_CHGENTARI]
    ON [dbo].[CHGENTARI]([CODICAMAS] ASC, [CODTIPEST] ASC, [TIPLIQEST] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de configuración de estancia. DATETIME, auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro. CHAR(20), FK a SEGusuaru, auditoría y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro de configuración de estancia. DATETIME, auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de configuración de estancia. CHAR(20), FK a SEGusuaru, trazabilidad de origen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción CUPS vinculada a hospitalización (tipo liquidación 2) o hospitalización de urgencias (tipo liquidación 1). INT, referencia a CUPS facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CUPS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción relacionada de CUPS de hospitalización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CUPS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CUPS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción CUPS vinculada a observación de urgencias (tipo liquidación 1). INT, solo se completa en urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción relacionada de CUPS de observación de urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional (servicio, área) a la que pertenece la configuración de estancia. CHAR(10), FK a INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención (institución, clínica, hospital) donde aplica la configuración. CHAR(10), FK a ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS Vie para liquidación de hospitalización (tipo 2) o hospitalización de urgencias (tipo 1). INT, facturación RIPS y recaudos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'GENCUPS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo CUPS Vie  1. Si el tipo de liquidacion es 1 - Urgencias, se especifica el codigo CUPS para la liquidacion de Hospitalizacion de urgencias.  2. Si el tipo de liquidacion es 2 - Hospitalizacion, se especifica el codigo CUPS para la liquidacion de Hospitalizacion.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'GENCUPS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'GENCUPS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS Vie para liquidación de observación en urgencias (tipo liquidación 1). INT, solo se completa en tipo 1 urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'GENCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo CUPS de Vie  1. Si el tipo de liquidacion es 1 - Urgencias, se especifica el codigo CUPS para liquidacion de Observacion. Solo se diligencia este campo en tipo liquidacion 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'GENCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'GENCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número mínimo de horas de estancia requeridas para la configuración. TINYINT, política de hospitalización y facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'NUMHOREST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Horas Minima de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'NUMHOREST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'NUMHOREST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación de estancia: 1=Observación Urgencias, 2=Recuperación Post-Quirúrgica, 3=Hospitalaria. TINYINT, determina CUPS y facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'TIPLIQEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de liquidacion de estancia  1: Observacion Urgencias  2: Recuperacion Post-Quirurgico  3: Hospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'TIPLIQEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'TIPLIQEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de estancia (categoría de acogida). CHAR(3), FK a CHTIPESTA, clasificación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Tipo de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'CODTIPEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cama asociada a la configuración de estancia. INT, FK a CHCAMASHO, recurso físico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'CODICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único e irrepetible de la configuración de estancia. INT IDENTITY, clave primaria, auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de tarifas y liquidación por tipo de estancia o cama hospitalaria. Define qué servicios CUPS aplican según el tipo de cama, la unidad funcional y el centro de atención, junto con las horas y modalidad de liquidación correspondientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI';
